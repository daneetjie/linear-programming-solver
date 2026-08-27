using System;
using System.Collections.Generic;
using System.Diagnostics;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{

    public class CuttingPlaneIterationResult
    {
        public string Label { get; set; }
        public int CutNumber { get; set; }


        //SNAPSHOT OF TABLEAU AT THIS STAGE
        public List<List<double>> Matrix { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public string Status { get; set; }
        public bool IsIntergerSolution { get; set; }
        public double ObjectiveValue { get; set; }
        public double[] VariableValues { get; set; }
    }

    public class CuttingPlaneRunResult
    {
        public List<CuttingPlaneIterationResult> Iterations { get; set; } = new();

        public CuttingPlaneResult Best { get; set; }

    }

    public class CuttingPlaneResult
    {
        public bool Found { get; set; }
        public double ObjectiveValue { get; set; }
        public double[] VariableValues { get; set; }
        public string SourceIterationLabel { get; set; }
    }

    public static class CuttingPlaneSolver
    {
        private const double Tolerance = 1e-6;
        private const int MaxCuts = 50;

        public static CuttingPlaneRunResult Solve(LinearProgram program)
        {

            var runResult = new CuttingPlaneRunResult();
            var tableau = new Tableau(program);
            var canonicalMatrix = ToJagged(tableau.Matrix);
            var canonicalHeaders = new List<string>(tableau.ColumnHeaders);

            int numVars = tableau.NumVariables;

            SolveRelaxation(tableau);

            var matrix = ToJagged(tableau.Matrix);
            var headers = new List<string>(tableau.ColumnHeaders);


            var initialSolution = ExtractSolution(matrix, headers, numVars);

            var canonicalIteration = new CuttingPlaneIterationResult
            {
                Label = "Canonical Form",
                CutNumber = 0,
                Matrix = CloneMatrix(canonicalMatrix),
                ColumnHeaders = new List<string>(canonicalHeaders),
                Status = "Initial canonical form before solving the LP relaxation.",
                IsIntergerSolution = false,
                ObjectiveValue = 0,
                VariableValues = null
            };
            runResult.Iterations.Add(canonicalIteration);

            var initialIteration = new CuttingPlaneIterationResult
            {
                Label = "Initial LP relaxtion",
                CutNumber = 0,
                Matrix = CloneMatrix(matrix),
                ColumnHeaders = new List<string>(headers),
                ObjectiveValue = initialSolution.ObjectiveValue,
                VariableValues = initialSolution.VariableValues
            };

            if (initialSolution.IsInterger)
            {
                initialIteration.Status = $"Integer-feasible solution found immediately. Z = {initialSolution.ObjectiveValue:F3}";

                initialIteration.IsIntergerSolution = true;
                runResult.Iterations.Add(initialIteration);
                runResult.Best = new CuttingPlaneResult
                {
                    Found = true,
                    ObjectiveValue = initialSolution.ObjectiveValue,
                    VariableValues = initialSolution.VariableValues,
                    SourceIterationLabel = initialIteration.Label
                };

                return runResult;
            }

            initialIteration.Status = $"LP relaxation solved. Fractional solution found. Z = {initialSolution.ObjectiveValue:F3}";
            runResult.Iterations.Add(initialIteration);



            for (int cut = 1; cut <= MaxCuts; cut++)
            {
                int fracRow = FindFractionalBasicRow(matrix, numVars);
                if (fracRow == -1)
                {
                    var finalSolution = ExtractSolution(matrix, headers, numVars);

                    var finalIteration = new CuttingPlaneIterationResult
                    {
                        Label = $"Cut {cut - 1} - Final",
                        CutNumber = cut - 1,
                        Matrix = CloneMatrix(matrix),
                        ColumnHeaders = new List<string>(headers),
                        Status = $"Integer-feasible solution reached. Z = {finalSolution.ObjectiveValue:F3}",
                        IsIntergerSolution = true,
                        ObjectiveValue = finalSolution.ObjectiveValue,
                        VariableValues = finalSolution.VariableValues
                    };

                    runResult.Iterations.Add(finalIteration);

                    runResult.Best = new CuttingPlaneResult
                    {
                        Found = true,
                        ObjectiveValue = finalSolution.ObjectiveValue,
                        VariableValues = finalSolution.VariableValues,
                        SourceIterationLabel = finalIteration.Label
                    };

                    return runResult;
                }


                int basicColumn = BasicColumnForRow(matrix, fracRow, numVars, true);

                string basicVariable = basicColumn >= 0 ? headers[basicColumn] : $"Row{fracRow}";

                AddGomoryCut(matrix, headers, fracRow);


                // solve the LP relaxation using their existing solvers ---

                bool feasible = DualPivotToFeasible(matrix, headers);

                if (!feasible)
                {
                    var infeasibleIteration = new CuttingPlaneIterationResult
                    {
                        Label = $"Cut {cut}",
                        CutNumber = cut,
                        Matrix = CloneMatrix(matrix),
                        ColumnHeaders = new List<string>(headers),
                        Status = $"Cut {cut}: model becasme infeasible after generating the Gomory cut from {basicVariable}."
                    };

                    runResult.Iterations.Add(infeasibleIteration);

                    runResult.Best = new CuttingPlaneResult { Found = false };

                    return runResult;
                }

                var solution = ExtractSolution(matrix, headers, numVars);

                var iteration = new CuttingPlaneIterationResult
                {
                    Label = $"Cut {cut}",
                    CutNumber = cut,
                    Matrix = CloneMatrix(matrix),
                    ColumnHeaders = new List<string>(headers),
                    ObjectiveValue = solution.ObjectiveValue,
                    VariableValues = solution.VariableValues,
                    IsIntergerSolution = solution.IsInterger,
                    Status = $"Cut {cut}: Gomory cut generated from {basicVariable}. " +
                            $"New LP solution Z = {solution.ObjectiveValue:F3}"
                };

                runResult.Iterations.Add(iteration);

                if (solution.IsInterger)
                {
                    iteration.Status = $"Integer-feasible solution found after Cut {cut}. " + $"Z = {solution.ObjectiveValue:F3}";

                    runResult.Best = new CuttingPlaneResult
                    {
                        Found = true,
                        ObjectiveValue = solution.ObjectiveValue,
                        VariableValues = solution.VariableValues,
                        SourceIterationLabel = iteration.Label
                    };

                    return runResult;
                }
            }

            runResult.Best = new CuttingPlaneResult { Found = false };

            return runResult;
        }

        private static void SolveRelaxation(Tableau tableau)
        {
            if (HasNegativeRhs(tableau))
                DualSolver.simpleDualSimplexSolver(tableau);
            else
                SimplexSolver.simpleSimplexSolver(tableau);
        }

        private static bool HasNegativeRhs(Tableau tableau)
        {
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;
            for (int row = 1; row < tableau.Matrix.GetLength(0); row++)
                if (tableau.Matrix[row, rhsColumn] < 0)
                    return true;
            return false;
        }

        // --- Cut generation ---
        // Finds a row whose basic variable is an original decision variable
        // (index < numVars) with a fractional RHS - the row we'll cut from.
        private static int FindFractionalBasicRow(List<List<double>> matrix, int numVars)
        {
            int rhsCol = matrix[0].Count - 1;

            for (int row = 1; row < matrix.Count; row++)
            {
                int basicCol = BasicColumnForRow(matrix, row, numVars, decisionVarsOnly: true);


                if (basicCol == -1) continue;

                double rhs = matrix[row][rhsCol];

                if (Math.Abs(rhs - Math.Round(rhs)) > Tolerance)
                    return row;
            }

            return -1;
        }

        // Returns the column that is basic (unit column, the 1 in this row)
        // for row i, or -1 if none is found / it's not a decision variable.
        private static int BasicColumnForRow(List<List<double>> matrix, int row, int numVars, bool decisionVarsOnly = false)
        {
            int rhsCol = matrix[0].Count - 1;
            int upperBound = decisionVarsOnly ? numVars : rhsCol;

            for (int col = 0; col < upperBound; col++)
            {
                if (Math.Abs(matrix[row][col] - 1.0) < Tolerance && IsUnitColumn(matrix, col, row))
                    return col;
            }

            return -1;
        }

        private static bool IsUnitColumn(List<List<double>> matrix, int col, int expectedOneRow)
        {
            for (int row = 0; row < matrix.Count; row++)
            {
                double v = matrix[row][col];
                if (row == expectedOneRow)
                {
                    if (Math.Abs(v - 1.0) > Tolerance) return false;
                }
                else if (Math.Abs(v) > Tolerance)
                {
                    return false;
                }
            }
            return true;
        }

        private static void AddGomoryCut(List<List<double>> matrix, List<string> headers, int sourceRow)
        {
            int rhsCol = matrix[0].Count - 1;
            var source = matrix[sourceRow];

            var cutRow = new List<double>(new double[matrix[0].Count]);
            for (int col = 0; col < rhsCol; col++)
            {
                double frac = Frac(source[col]);
                cutRow[col] = -frac;
            }
            double rhsFrac = Frac(source[rhsCol]);
            cutRow[rhsCol] = -rhsFrac;

            // Insert new "cut slack" column just before RHS, in every row.
            foreach (var row in matrix)
                row.Insert(rhsCol, 0.0);
            cutRow.Insert(rhsCol, 1.0); // this row's own cut-slack coefficient

            matrix.Add(cutRow);
            headers.Insert(headers.Count - 1, $"s{matrix.Count - 1}");
        }

        private static double Frac(double v)
        {
            double f = v - Math.Floor(v);
            return f < 0 ? f + 1 : f;
        }

        // Dual simplex pivoting, re-hosted from DualSolver's logic to
        // work on our growable List<List<double>> instead of Tableau.Matrix 
        private static bool DualPivotToFeasible(List<List<double>> matrix, List<string> headers)
        {
            int rhsCol = matrix[0].Count - 1;

            for (int iteration = 0; iteration < 1000; iteration++)
            {
                int pivotRow = -1;
                double mostNegative = -Tolerance;
                //most negative value
                for (int i = 1; i < matrix.Count; i++)
                {
                    if (matrix[i][rhsCol] < mostNegative)
                    {
                        mostNegative = matrix[i][rhsCol];
                        pivotRow = i;
                    }
                }
                //all rhs values are feasible
                if (pivotRow == -1)
                    return true; // primal feasible again - done

                int pivotCol = -1;
                double bestRatio = double.PositiveInfinity;
                //for entering var
                for (int col = 0; col < rhsCol; col++)
                {
                    double coeff = matrix[pivotRow][col];
                    if (coeff >= -Tolerance) continue;

                    double ratio = matrix[0][col] / coeff;
                    if (ratio < bestRatio)
                    {
                        bestRatio = ratio;
                        pivotCol = col;
                    }
                }

                if (pivotCol == -1)
                    return false; // infeasible

                Pivot(matrix, pivotRow, pivotCol);
            }

            return false;
        }

        private static void Pivot(List<List<double>> matrix, int pivotRow, int pivotCol)
        {
            double pivotValue = matrix[pivotRow][pivotCol];
            int numCols = matrix[0].Count;

            for (int col = 0; col < numCols; col++)
                matrix[pivotRow][col] /= pivotValue;

            for (int row = 0; row < matrix.Count; row++)
            {
                if (row == pivotRow) continue;
                double factor = matrix[row][pivotCol];
                if (Math.Abs(factor) < Tolerance) continue;

                for (int col = 0; col < numCols; col++)
                    matrix[row][col] -= factor * matrix[pivotRow][col];
            }
        }

        private class ExtractedSolution
        {
            public bool IsInterger { get; set; }
            public double ObjectiveValue { get; set; }
            public double[] VariableValues { get; set; }
        }

        private static ExtractedSolution ExtractSolution(List<List<double>> matrix, List<string> headers, int numVars)
        {
            int rhsCol = matrix[0].Count - 1;

            var values = new double[numVars];

            for (int variable = 0;
                 variable < numVars;
                 variable++)
            {
                int basicRow = -1;
                int oneCount = 0;
                bool isBasic = true;

                for (int row = 0;
                     row < matrix.Count;
                     row++)
                {
                    double value = matrix[row][variable];

                    if (Math.Abs(value - 1.0) < Tolerance)
                    {
                        oneCount++;
                        basicRow = row;
                    }
                    else if (Math.Abs(value) > Tolerance)
                    {
                        isBasic = false;
                        break;
                    }
                }

                if (isBasic &&
                    oneCount == 1 &&
                    basicRow > 0)
                {
                    values[variable] =
                        matrix[basicRow][rhsCol];
                }
                else
                {
                    values[variable] = 0.0;
                }
            }

            bool isInteger = true;

            foreach (double value in values)
            {
                if (Math.Abs(value - Math.Round(value)) > Tolerance)
                {
                    isInteger = false;
                    break;
                }
            }

            return new ExtractedSolution
            {
                IsInterger = isInteger,
                ObjectiveValue = matrix[0][rhsCol],
                VariableValues = values
            };
        }



        // --- Utilities ---

        private static List<List<double>> ToJagged(double[,] source)
        {
            int rows = source.GetLength(0);
            int cols = source.GetLength(1);
            var result = new List<List<double>>(rows);

            for (int row = 0; row < rows; row++)
            {
                var newRow = new List<double>(cols);
                for (int col = 0; col < cols; col++)
                    newRow.Add(source[row, col]);
                result.Add(newRow);
            }

            return result;
        }

        private static List<List<double>> CloneMatrix(List<List<double>> source)
        {
            var clone = new List<List<double>>();

            foreach (var row in source)
            {
                clone.Add(new List<double>(row));
            }

            return clone;
        }
    }
}
