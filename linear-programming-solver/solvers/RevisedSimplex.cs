using System;
using System.Collections.Generic;
using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;

namespace LinearProgrammingSolver.Solvers
{
    public static class RevisedSimplex
    {
        private const int MaxIterations = 1000;

        // Entry point for the revised simplex solver. Assumes tableau is in a form with feasible RHS.
        public static SimplexResult revisedSimplexSolver(Tableau tableau)
        {
            int numRows = tableau.Matrix.GetLength(0);
            int numCols = tableau.Matrix.GetLength(1);
            int rhsColumn = numCols - 1;

            // If RHS has negatives, dual simplex should be used
            for (int i = 1; i < numRows; i++)
            {
                if (tableau.Matrix[i, rhsColumn] < 0)
                {
                    throw new InvalidOperationException("Tableau has negative RHS values. Use dual simplex instead.");

                }
            }

            // Capture original objective coefficients ONCE, before any pivoting happens
            double[] originalC = new double[rhsColumn];
            for (int j = 0; j < rhsColumn; j++)
                originalC[j] = -tableau.Matrix[0, j];

            // Initialize basis to slack columns (like SimplexSolver)
            var basis = new int[tableau.NumConstraintRows];
            for (int i = 0; i < tableau.NumConstraintRows; i++)
                basis[i] = tableau.NumVariables + i;

            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                // Build B matrix and invert
                var B = BuildBasisMatrix(tableau.Matrix, basis);
                var Binv = InvertMatrix(B);

                // RHS vector b
                var b = GetRhsVector(tableau.Matrix);

                // Compute basic solution x_B = B^{-1} * b
                var xB = MultiplyMatrixVector(Binv, b);

                // Compute reduced costs and find entering variable
                int rhsCols = rhsColumn;
                double[] reducedCosts = new double[rhsCols];
                double mostPositive = 0;
                int enteringCol = -1;

                double[] c = originalC;

                // c_B
                double[] cB = new double[basis.Length];
                for (int i = 0; i < basis.Length; i++)
                    cB[i] = c[basis[i]];

                // Compute pi = cB^T * B^{-1}
                var pi = MultiplyVectorMatrix(cB, Binv);

                for (int j = 0; j < rhsCols; j++)
                {
                    var aj = GetColumn(tableau.Matrix, j, startRow: 1);
                    var term = Dot(pi, aj);
                    reducedCosts[j] = c[j] - term;
                    if (reducedCosts[j] > mostPositive)
                    {
                        mostPositive = reducedCosts[j];
                        enteringCol = j;
                    }
                }

                if (enteringCol == -1)
                {
                    // Optimal
                    return SolutionBuilder.BuildResult(tableau, basis, rhsColumn);

                }

                // Compute direction d = B^{-1} * a_enter
                var aEnter = GetColumn(tableau.Matrix, enteringCol, startRow: 1);
                var d = MultiplyMatrixVector(Binv, aEnter);

                // Ratio test
                double minRatio = double.PositiveInfinity;
                int leavingIndex = -1;
                for (int i = 0; i < d.Length; i++)
                {
                    if (d[i] > 1e-12)
                    {
                        double ratio = xB[i] / d[i];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            leavingIndex = i;
                        }
                    }
                }

                if (leavingIndex == -1)
                {
                    throw new InvalidOperationException("LP is unbounded (revised simplex).");
                }

                int pivotRowInTableau = leavingIndex + 1; // tableau rows start at 1 for constraints
                Pivot(tableau.Matrix, numRows, numCols, pivotRowInTableau, enteringCol);
                basis[leavingIndex] = enteringCol;
                tableau.RecordIteration();
            }

            throw new InvalidOperationException($"Revised simplex did not converge after {MaxIterations} iterations.");
        }

        // Helper: extract RHS vector (excluding objective row)
        private static double[] GetRhsVector(double[,] matrix)
        {
            int m = matrix.GetLength(0) - 1;
            int n = matrix.GetLength(1);
            var rhs = new double[m];
            for (int i = 0; i < m; i++)
                rhs[i] = matrix[i + 1, n - 1];
            return rhs;
        }

        // Helper: get column vector from matrix starting at row 'startRow' (inclusive) to end
        private static double[] GetColumn(double[,] matrix, int colIndex, int startRow = 0)
        {
            int rows = matrix.GetLength(0);
            int len = rows - startRow;
            var col = new double[len];
            for (int i = startRow; i < rows; i++)
                col[i - startRow] = matrix[i, colIndex];
            return col;
        }

        // Build basis matrix B (m x m) using columns specified in basis (these are column indices into tableau.Matrix)
        private static double[,] BuildBasisMatrix(double[,] matrix, int[] basis)
        {
            int m = basis.Length;
            var B = new double[m, m];
            for (int col = 0; col < m; col++)
            {
                var column = GetColumn(matrix, basis[col], startRow: 1); // skip objective row
                for (int row = 0; row < m; row++)
                    B[row, col] = column[row];
            }
            return B;
        }

        private static double Dot(double[] v1, double[] v2)
        {
            double s = 0;
            for (int i = 0; i < v1.Length; i++) s += v1[i] * v2[i];
            return s;
        }

        private static double[] MultiplyMatrixVector(double[,] A, double[] v)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            var r = new double[m];
            for (int i = 0; i < m; i++)
            {
                double s = 0;
                for (int j = 0; j < n; j++) s += A[i, j] * v[j];
                r[i] = s;
            }
            return r;
        }

        private static double[] MultiplyVectorMatrix(double[] v, double[,] A)
        {
            int n = A.GetLength(1);
            int m = A.GetLength(0);
            var r = new double[n];
            for (int j = 0; j < n; j++)
            {
                double s = 0;
                for (int i = 0; i < m; i++) s += v[i] * A[i, j];
                r[j] = s;
            }
            return r;
        }

        // Invert square matrix using Gauss-Jordan elimination
        private static double[,] InvertMatrix(double[,] A)
        {
            int n = A.GetLength(0);
            var m = new double[n, n * 2];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    m[i, j] = A[i, j];

            for (int i = 0; i < n; i++)
                m[i, n + i] = 1.0;

            for (int col = 0; col < n; col++)
            {
                // find pivot
                int pivot = col;
                double max = Math.Abs(m[pivot, col]);
                for (int row = col + 1; row < n; row++)
                {
                    double val = Math.Abs(m[row, col]);
                    if (val > max)
                    {
                        max = val;
                        pivot = row;
                    }
                }

                if (Math.Abs(m[pivot, col]) < 1e-15)
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

                // swap rows
                if (pivot != col)
                {
                    for (int j = 0; j < 2 * n; j++)
                    {
                        double tmp = m[col, j];
                        m[col, j] = m[pivot, j];
                        m[pivot, j] = tmp;
                    }
                }

                // normalize pivot row
                double pivotVal = m[col, col];
                for (int j = 0; j < 2 * n; j++) m[col, j] /= pivotVal;

                // eliminate other rows
                for (int row = 0; row < n; row++)
                {
                    if (row == col) continue;
                    double factor = m[row, col];
                    if (factor == 0) continue;
                    for (int j = 0; j < 2 * n; j++) m[row, j] -= factor * m[col, j];
                }
            }

            var inv = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inv[i, j] = m[i, n + j];

            return inv;
        }

        // Reuse pivot method from SimplexSolver-like implementation (same logic)
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


    }
}
