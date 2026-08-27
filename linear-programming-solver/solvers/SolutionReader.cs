using System;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public enum LpStatus
    {
        Optimal,
        Unbounded,
        // Can't detect infeasibility yet. SimplexSolver/DualSolver only print it,
        // don't return it. "Optimal" here means converged, not feasible.
    }

    public class LpResult
    {
        public LpStatus Status { get; set; }
        public double ObjectiveValue { get; set; }
        public double[] VariableValues { get; set; } // indexed 0..NumVariables-1, matches x1..xn
    }

    public static class SolutionReader
    {
        private const double Tolerance = 1e-6;

        public static LpResult ExtractSolution(Tableau tableau)
        {
            int numRows = tableau.Matrix.GetLength(0);
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;

            // Unbounded check: a negative obj-row entry whose column is <= 0
            // everywhere means no leaving row would ever be found - re-derived
            // here instead of relying on SimplexSolver to tell us.
            for (int j = 0; j < rhsColumn; j++)
            {
                if (tableau.Matrix[0, j] < -Tolerance)
                {
                    bool allNonPositive = true;
                    for (int i = 1; i < numRows; i++)
                    {
                        if (tableau.Matrix[i, j] > Tolerance)
                        {
                            allNonPositive = false;
                            break;
                        }
                    }

                    if (allNonPositive)
                        return new LpResult { Status = LpStatus.Unbounded };
                }
            }

            var values = new double[tableau.NumVariables];

            for (int j = 0; j < tableau.NumVariables; j++)
            {
                int basicRow = -1;
                int oneCount = 0;
                bool isBasic = true;

                for (int i = 0; i < numRows; i++)
                {
                    double v = tableau.Matrix[i, j];

                    if (Math.Abs(v - 1.0) < Tolerance)
                    {
                        oneCount++;
                        basicRow = i;
                    }
                    else if (Math.Abs(v) > Tolerance)
                    {
                        isBasic = false;
                        break;
                    }
                }

                // A basic variable's column is a unit column with the single
                // 1 in one of the constraint rows (row 0 is the objective row).
                values[j] = (isBasic && oneCount == 1 && basicRow > 0) ? tableau.Matrix[basicRow, rhsColumn]: 0.0;
            }

            return new LpResult
            {
                Status = LpStatus.Optimal,
                ObjectiveValue = tableau.Matrix[0, rhsColumn],
                VariableValues = values
            };
        }

        // Convenience check for your Branch & Bound integer test.
        public static int FindFractionalVariable(LpResult result, double tolerance = 1e-6)
        {
            for (int j = 0; j < result.VariableValues.Length; j++)
            {
                double v = result.VariableValues[j];
                if (Math.Abs(v - Math.Round(v)) > tolerance)
                    return j; // index of first fractional variable found
            }

            return -1; // all integer
        }
    }
}
