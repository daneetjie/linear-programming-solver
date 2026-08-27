using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinearProgrammingSolver.Solvers
{
    internal class SolutionBuilder
    {
        public static SimplexResult BuildResult(Tableau tableau, int[] basis, int rhsColumn)
        {
            var values = new double[tableau.NumVariables];
            for (int i = 0; i < basis.Length; i++)
                if (basis[i] < tableau.NumVariables)
                    values[basis[i]] = tableau.Matrix[i + 1, rhsColumn];

            var solution = new Dictionary<string, double>();
            for (int j = 0; j < tableau.NumVariables; j++)
                solution[tableau.ColumnHeaders[j]] = Math.Round(values[j], 3);
            solution["Z"] = Math.Round(tableau.Matrix[0, rhsColumn], 3);

            return new SimplexResult { Solution = solution, Basis = basis };
        }
    }
}

