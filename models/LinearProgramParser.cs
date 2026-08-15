using LinearProgrammingSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearProgrammingSolver.Controllers
{
    public class LinearProgramParser
    {
        public static List<LinearProgram> Parse(string fileContents)
        {
            var programs = new List<LinearProgram>();
            var lines = fileContents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(l => l.Trim())
                                     .Where(l => !string.IsNullOrEmpty(l))
                                     .ToList();

            if (lines.Count < 3)
                throw new ArgumentException("Input file must contain at least 3 lines (objective, constraint, variable types)");

            // Parse objective (first line)
            var objectiveTokens = lines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string objectiveType = objectiveTokens[0].ToLower(); // "max" or "min"

            double[] objectiveCoefficients = objectiveTokens.Skip(1)
                .Select(token => double.Parse(token))
                .ToArray();

            // Parse variable types (last line)
            var variableTypes = lines[lines.Count - 1]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            // Validate that variable types match objective coefficients
            if (variableTypes.Length != objectiveCoefficients.Length)
                throw new ArgumentException($"Number of variable types ({variableTypes.Length}) must match objective coefficients ({objectiveCoefficients.Length})");

            // Create a single LinearProgram with all constraints
            var program = new LinearProgram
            {
                Objective = objectiveType,
                ObjectiveCoefficients = objectiveCoefficients,
                VariableTypes = variableTypes
            };

            // Parse constraints (all lines between objective and variable types)
            for (int i = 1; i < lines.Count - 1; i++)
            {
                var constraint = ParseConstraint(lines[i], objectiveCoefficients.Length);
                program.Constraints.Add(new Constraint(constraint.coefficients, constraint.op, constraint.rhs));
            }

            programs.Add(program);
            return programs;
        }
        private static (double[] coefficients, string op, double rhs) ParseConstraint(string line, int numVariables)
        {
            var tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < numVariables + 1)
                throw new ArgumentException($"Constraint must have at least {numVariables + 1} elements (coefficients + operator+RHS)");

            // Extract coefficients (first numVariables tokens)
            var coefficients = new double[numVariables];
            for (int i = 0; i < numVariables; i++)
            {
                coefficients[i] = double.Parse(tokens[i]);
            }

            // The last token contains operator and RHS together (e.g., "<=40")
            string lastToken = tokens[tokens.Length - 1];
            
            // Try to find where the operator is
            string operatorToken = "";
            string rhsString = "";

            if (lastToken.Contains("<="))
            {
                operatorToken = "<=";
                rhsString = lastToken.Replace("<=", "");
            }
            else if (lastToken.Contains(">="))
            {
                operatorToken = ">=";
                rhsString = lastToken.Replace(">=", "");
            }
            else if (lastToken.Contains("="))
            {
                operatorToken = "=";
                rhsString = lastToken.Replace("=", "");
            }
            else
            {
                throw new ArgumentException($"Cannot find operator in constraint: {line}");
            }

            if (string.IsNullOrEmpty(rhsString))
                throw new ArgumentException($"No RHS value found in constraint: {line}");

            double rhs = double.Parse(rhsString);

            return (coefficients, operatorToken, rhs);
        }

        private static bool IsValidOperator(string op)
        {
            return op == "<=" || op == ">=" || op == "=";
        }
    }
}