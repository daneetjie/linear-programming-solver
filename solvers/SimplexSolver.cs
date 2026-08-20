using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public static class SimplexSolver
    {
        public static void simpleSimplexSolver(Tableau tableau)
        {
            // TODO: implement primal simplex (optimality check, pivot
            // column/row selection, pivot, repeat until optimal)
        }

        public static void simpleDualSimplexSolver(Tableau tableau)
        {
            // TODO: implement dual simplex
        }

        public static void simpleSolver(Tableau tableau)
        {
            if (HasNegativeRhs(tableau))
                Console.WriteLine("dual simplex");
            else
                Console.WriteLine("simplex");
        }

        private static bool HasNegativeRhs(Tableau tableau)
        {
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;

            // row 0 is the objective row (its RHS slot is always 0 and not
            // meaningful for feasibility) — only check constraint rows.
            for (int row = 1; row < tableau.Matrix.GetLength(0); row++)
            {
                if (tableau.Matrix[row, rhsColumn] < 0)
                    return true;
            }

            return false;
        }
    }
}
