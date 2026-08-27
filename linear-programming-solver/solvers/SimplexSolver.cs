using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public static class SimplexSolver
    {
        private const int MaxIterations = 1000;

        public static SimplexResult simpleSimplexSolver(Tableau tableau)
        {
            int numRows = tableau.Matrix.GetLength(0);
            int numCols = tableau.Matrix.GetLength(1);
            int rhsColumn = numCols - 1;

            //basis used to track what are currently basic variables
            var basis = new int[tableau.NumConstraintRows];
            for (int i = 0; i < tableau.NumConstraintRows; i++)
                basis[i] = tableau.NumVariables + i;

            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                int pivotColumn = SelectPivotColumn(tableau.Matrix, rhsColumn);
                if (pivotColumn == -1)
                {
                    return SolutionBuilder.BuildResult(tableau, basis, rhsColumn);
                }

                int pivotRow = SelectPivotRow(tableau.Matrix, numRows, pivotColumn, rhsColumn);
                if (pivotRow == -1)
                {

                    throw new InvalidOperationException("LP is unbounded.");

                }

                Pivot(tableau.Matrix, numRows, numCols, pivotRow, pivotColumn);
                basis[pivotRow - 1] = pivotColumn;
                tableau.RecordIteration();
            }

            throw new InvalidOperationException($"Simplex did not converge after {MaxIterations} iterations.");
        }

       

        

        



        public static void simpleSolver(Tableau tableau)
        {
            if (HasNegativeRhs(tableau)) ;

            else
                simpleSimplexSolver(tableau);
        }

        private static int SelectPivotColumn(double[,] matrix, int rhsColumn)
        {
            int pivotColumn = -1;
            double mostNegative = 0;

            for (int j = 0; j < rhsColumn; j++)
            {
                if (matrix[0, j] < mostNegative)
                {
                    mostNegative = matrix[0, j];
                    pivotColumn = j;
                }
            }

            return pivotColumn;
        }

        private static int SelectPivotRow(double[,] matrix, int numRows, int pivotColumn, int rhsColumn)
        {
            int pivotRow = -1;
            double bestRatio = double.PositiveInfinity;

            for (int i = 1; i < numRows; i++)
            {
                double coefficient = matrix[i, pivotColumn];
                if (coefficient <= 0)
                    continue;

                double ratio = matrix[i, rhsColumn] / coefficient;
                if (ratio < bestRatio)
                {
                    bestRatio = ratio;
                    pivotRow = i;
                }
            }

            return pivotRow;
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





        private static bool HasNegativeRhs(Tableau tableau)
        {
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;

            for (int row = 1; row < tableau.Matrix.GetLength(0); row++)
            {
                if (tableau.Matrix[row, rhsColumn] < 0)
                    return true;
            }

            return false;
        }
    }
}
