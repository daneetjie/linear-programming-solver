using System.Collections.Generic;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public static class LinearProgramCloner
    {
        public static LinearProgram Clone(LinearProgram source)
        {
            var clone = new LinearProgram
            {
                Objective = source.Objective,
                ObjectiveCoefficients = (double[])source.ObjectiveCoefficients.Clone(),
                VariableTypes = (string[])source.VariableTypes.Clone(),
                Constraints = new List<Constraint>(source.Constraints.Count)
            };

            foreach (var c in source.Constraints)
            {
                clone.Constraints.Add(new Constraint((double[])c.Coefficients.Clone(), c.Operator, c.RightHandSide));
            }

            return clone;
        }

        // Adds a new bound constraint on a single decision variable
        public static LinearProgram WithExtraBound(LinearProgram source, int variableIndex, string op, double rhs)
        {
            var clone = Clone(source);

            var coefficients = new double[source.ObjectiveCoefficients.Length];
            coefficients[variableIndex] = 1;

            clone.Constraints.Add(new Constraint(coefficients, op, rhs));

            return clone;
        }
    }
}
