using System;
using LinearProgrammingSolver.Models;
using System.Collections.Generic;

namespace LinearProgrammingSolver.Solvers
{
    public static class DualSolver
    {
        private const int MaxIterations = 1000;

        public static void simpleDualSimplexSolver(Tableau tableau)
        {
            int numRows = tableau.Matrix.GetLength(0);
            int numCols = tableau.Matrix.GetLength(1);
            int rhsColumn = numCols - 1;

            // basis used to track what are currently basic variables
            var basis = new int[tableau.NumConstraintRows];
            for (int i = 0; i < tableau.NumConstraintRows; i++)
                basis[i] = tableau.NumVariables + i;

            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                int pivotRow = SelectPivotRowDual(tableau.Matrix, rhsColumn);
                if (pivotRow == -1)
                {
                    // No negative RHS; primal feasible. Dual-simplex preserves dual feasibility -> optimal
                    PrintIterations(tableau);
                    PrintSolution(tableau, basis, rhsColumn);
                    return;
                }

                int pivotColumn = SelectPivotColumnDual(tableau.Matrix, rhsColumn, pivotRow);
                if (pivotColumn == -1)
                {
                    PrintIterations(tableau);
                    Console.WriteLine("LP is infeasible (dual simplex cannot find entering variable).");
                    return;
                }

                Pivot(tableau.Matrix, numRows, numCols, pivotRow, pivotColumn);
                basis[pivotRow - 1] = pivotColumn;
                tableau.RecordIteration();
            }

            PrintIterations(tableau);
            Console.WriteLine($"Dual simplex did not converge after {MaxIterations} iterations.");
        }

        private static int SelectPivotRowDual(double[,] matrix, int rhsColumn)
        {
            int pivotRow = -1;
            double mostNegative = 0;

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                double value = matrix[i, rhsColumn];
                if (value < mostNegative)
                {
                    mostNegative = value;
                    pivotRow = i;
                }
            }

            return pivotRow;
        }

        private static int SelectPivotColumnDual(double[,] matrix, int rhsColumn, int pivotRow)
        {
            int pivotColumn = -1;
            double bestRatio = double.PositiveInfinity;

            for (int j = 0; j < rhsColumn; j++)
            {
                double a = matrix[pivotRow, j];
                if (a >= 0)
                    continue; // need negative coefficient to increase RHS toward non-negative

                double cj = matrix[0, j];
                // ratio = cj / a  (a < 0)
                double ratio = cj / a;
                if (ratio < bestRatio)
                {
                    bestRatio = ratio;
                    pivotColumn = j;
                }
            }

            return pivotColumn;
        }

        private static void Pivot(double[,] matrix, int numRows, int numCols, int pivotRow, int pivotColumn)
        {
            double pivotValue = matrix[pivotRow, pivotColumn];

            for (int j = 0; j < numCols; j++)
                matrix[pivotRow, j] /= pivotValue;

            for (int i = 0; i < numRows; i++)
            {
                if (i == pivotRow)
                    continue;

                double factor = matrix[i, pivotColumn];
                if (factor == 0)
                    continue;

                for (int j = 0; j < numCols; j++)
                    matrix[i, j] -= factor * matrix[pivotRow, j];
            }
        }

        private static void PrintIterations(Tableau tableau)
        {
            var history = tableau.IterationHistory;
            for (int i = 0; i < history.Count; i++)
            {
                string label = i == 0 ? "Initial Tableau" : $"Iteration {i}";
                Console.WriteLine($"{label}:");
                Console.WriteLine(Tableau.Format(tableau.ColumnHeaders, history[i]));
            }
        }

        private static void PrintSolution(Tableau tableau, int[] basis, int rhsColumn)
        {
            var values = new double[tableau.NumVariables];

            for (int i = 0; i < basis.Length; i++)
            {
                if (basis[i] < tableau.NumVariables)
                    values[basis[i]] = tableau.Matrix[i + 1, rhsColumn];
            }

            Console.WriteLine("Optimal solution found (primal feasible):");
            for (int j = 0; j < tableau.NumVariables; j++)
                Console.WriteLine($"  x{j + 1} = {values[j]}");

            Console.WriteLine($"  Z = {tableau.Matrix[0, rhsColumn]}");
        }
    }
}
