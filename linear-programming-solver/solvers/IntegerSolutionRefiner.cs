using System;
using System.Collections.Generic;
using System.Linq;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public static class IntegerSolutionRefiner
    {
        private const double Tolerance = 1e-6;

        // Simplex optimises the continuous relaxation, so a 'bin' or 'int' model can come back
        // fractional (x5 = 0.2). This branches that relaxation to integrality and returns the
        // discrete solution, leaving a genuinely continuous model untouched.
        public static Dictionary<string, double>? Refine(LinearProgram? program, SimplexResult? relaxation)
        {
            if (program == null || relaxation?.Solution == null)
                return relaxation?.Solution;

            if (!HasDiscreteVariables(program))
                return relaxation.Solution;

            if (!HasFractionalValue(program, relaxation.Solution))
                return relaxation.Solution;

            var runResult = BranchAndBoundSolver.Solve(program);
            if (runResult?.Best == null || !runResult.Best.Found || runResult.Best.VariableValues == null)
                return relaxation.Solution;

            var solution = new Dictionary<string, double>();
            for (int j = 0; j < runResult.Best.VariableValues.Length; j++)
                solution[$"x{j + 1}"] = Math.Round(runResult.Best.VariableValues[j], 3);

            solution["Z"] = Math.Round(runResult.Best.ObjectiveValue, 3);
            return solution;
        }

        // True when the model declares at least one 'bin' or 'int' variable.
        public static bool HasDiscreteVariables(LinearProgram? program) =>
            program?.VariableTypes != null && program.VariableTypes.Any(IsDiscrete);

        private static bool IsDiscrete(string? variableType) =>
            string.Equals(variableType, "bin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(variableType, "int", StringComparison.OrdinalIgnoreCase);

        // True when any variable the model declares discrete came back with a fractional value.
        private static bool HasFractionalValue(LinearProgram program, Dictionary<string, double> solution)
        {
            for (int j = 0; j < program.VariableTypes.Length; j++)
            {
                if (!IsDiscrete(program.VariableTypes[j]))
                    continue;

                if (!solution.TryGetValue($"x{j + 1}", out double value))
                    continue;

                if (Math.Abs(value - Math.Round(value)) > Tolerance)
                    return true;
            }

            return false;
        }
    }
}
