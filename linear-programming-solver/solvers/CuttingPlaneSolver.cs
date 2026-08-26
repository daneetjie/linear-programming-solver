using System;
using System.Collections.Generic;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
   
    public static class CuttingPlaneSolver
    {
        private const double Tolerance = 1e-6;
        private const int MaxCuts = 50;

        public static void Solve(LinearProgram program)
        {
            var tableau = new Tableau(program);
            SolveRelaxation(tableau);

            var matrix = ToJagged(tableau.Matrix);
            var headers = new List<string>(tableau.ColumnHeaders);
            int numVars = tableau.NumVariables;

            Console.WriteLine();
            Console.WriteLine("Initial (LP relaxation) optimal tableau:");
            PrintMatrix(matrix, headers);

            for (int cut = 1; cut <= MaxCuts; cut++)
            {
                int fracRow = FindFractionalBasicRow(matrix, numVars);
                if (fracRow == -1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Integer-feasible solution reached.");
                    PrintSolution(matrix, headers, numVars);
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Cut {cut}: generating Gomory cut from row {fracRow} (basic variable {headers[BasicColumnForRow(matrix, fracRow, numVars)]}).");

                AddGomoryCut(matrix, headers, fracRow);
                PrintMatrix(matrix, headers);

                if (!DualPivotToFeasible(matrix, headers))
                {
                    Console.WriteLine("Model is infeasible after this cut.");
                    return;
                }

                PrintMatrix(matrix, headers);
            }

            Console.WriteLine($"Cutting plane did not converge within {MaxCuts} cuts.");
        }

        // solve the LP relaxation using their existing solvers ---

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

            for (int i = 1; i < matrix.Count; i++)
            {
                int basicCol = BasicColumnForRow(matrix, i, numVars, decisionVarsOnly: true);
                if (basicCol == -1) continue;

                double rhs = matrix[i][rhsCol];
                if (Math.Abs(rhs - Math.Round(rhs)) > Tolerance)
                    return i;
            }

            return -1;
        }

        // Returns the column that is basic (unit column, the 1 in this row)
        // for row i, or -1 if none is found / it's not a decision variable.
        private static int BasicColumnForRow(List<List<double>> matrix, int row, int numVars, bool decisionVarsOnly = false)
        {
            int rhsCol = matrix[0].Count - 1;
            int upperBound = decisionVarsOnly ? numVars : rhsCol;

            for (int j = 0; j < upperBound; j++)
            {
                if (Math.Abs(matrix[row][j] - 1.0) < Tolerance && IsUnitColumn(matrix, j, row))
                    return j;
            }

            return -1;
        }

        private static bool IsUnitColumn(List<List<double>> matrix, int col, int expectedOneRow)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                double v = matrix[i][col];
                if (i == expectedOneRow)
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
            for (int j = 0; j < rhsCol; j++)
            {
                double frac = Frac(source[j]);
                cutRow[j] = -frac; // -frac(a_ij) x_j ... + g = -frac(b_i)
            }
            double rhsFrac = Frac(source[rhsCol]);
            cutRow[rhsCol] = -rhsFrac;

            // Insert new "cut slack" column just before RHS, in every row.
            foreach (var row in matrix)
                row.Insert(rhsCol, 0.0);
            cutRow.Insert(rhsCol, 1.0); // this row's own cut-slack coefficient

            matrix.Add(cutRow);
            headers.Insert(headers.Count - 1, $"g{matrix.Count - 1}");
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
                for (int i = 1; i < matrix.Count; i++)
                {
                    if (matrix[i][rhsCol] < mostNegative)
                    {
                        mostNegative = matrix[i][rhsCol];
                        pivotRow = i;
                    }
                }

                if (pivotRow == -1)
                    return true; // primal feasible again - done

                int pivotCol = -1;
                double bestRatio = double.PositiveInfinity;
                for (int j = 0; j < rhsCol; j++)
                {
                    double a = matrix[pivotRow][j];
                    if (a >= -Tolerance) continue;

                    double ratio = matrix[0][j] / a;
                    if (ratio < bestRatio)
                    {
                        bestRatio = ratio;
                        pivotCol = j;
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

            for (int j = 0; j < numCols; j++)
                matrix[pivotRow][j] /= pivotValue;

            for (int i = 0; i < matrix.Count; i++)
            {
                if (i == pivotRow) continue;
                double factor = matrix[i][pivotCol];
                if (Math.Abs(factor) < Tolerance) continue;

                for (int j = 0; j < numCols; j++)
                    matrix[i][j] -= factor * matrix[pivotRow][j];
            }
        }

        // --- Utilities ---

        private static List<List<double>> ToJagged(double[,] source)
        {
            int rows = source.GetLength(0);
            int cols = source.GetLength(1);
            var result = new List<List<double>>(rows);

            for (int i = 0; i < rows; i++)
            {
                var row = new List<double>(cols);
                for (int j = 0; j < cols; j++)
                    row.Add(source[i, j]);
                result.Add(row);
            }

            return result;
        }

        private static void PrintMatrix(List<List<double>> matrix, List<string> headers)
        {
            foreach (var h in headers)
                Console.Write(h.PadLeft(10));
            Console.WriteLine();

            foreach (var row in matrix)
            {
                foreach (var v in row)
                    Console.Write(Math.Round(v, 3).ToString().PadLeft(10));
                Console.WriteLine();
            }
        }

        private static void PrintSolution(List<List<double>> matrix, List<string> headers, int numVars)
        {
            int rhsCol = matrix[0].Count - 1;
            var values = new double[numVars];

            for (int j = 0; j < numVars; j++)
            {
                int basicRow = -1;
                bool isBasic = true;
                for (int i = 0; i < matrix.Count; i++)
                {
                    double v = matrix[i][j];
                    if (Math.Abs(v - 1.0) < Tolerance) basicRow = i;
                    else if (Math.Abs(v) > Tolerance) { isBasic = false; break; }
                }
                values[j] = (isBasic && basicRow > 0) ? matrix[basicRow][rhsCol] : 0.0;
            }

            Console.WriteLine("Optimal integer solution:");
            for (int j = 0; j < numVars; j++)
                Console.WriteLine($"  {headers[j]} = {values[j]:F3}");
            Console.WriteLine($"  Z = {matrix[0][rhsCol]:F3}");
        }
    }
}
