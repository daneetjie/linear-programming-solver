using LinearProgrammingSolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinearProgrammingSolver.Solvers
{
    public static class SensitivityAnalysis
    {
        private static LinearProgram CloneProgram(LinearProgram program)
        {
            var clone = new LinearProgram
            {
                Objective = program.Objective,
                ObjectiveCoefficients = (double[])program.ObjectiveCoefficients.Clone(),
                VariableTypes = (string[])program.VariableTypes.Clone()
            };

            foreach (var constraint in program.Constraints)
            {
                clone.Constraints.Add(new Constraint((double[])constraint.Coefficients.Clone(), constraint.Operator, constraint.RightHandSide));
            }
            return clone;
        }

        public static SimplexResult SolveProgram(LinearProgram program)
        {
            return SolveProgramWithTableau(program).Result;
        }

        public static (SimplexResult Result, Tableau Tableau) SolveProgramWithTableau(LinearProgram program)
        {
            var clone = CloneProgram(program);
            bool isMin = clone.Objective.Equals("min", StringComparison.OrdinalIgnoreCase);
            if (isMin)
            {
                for (int j = 0; j < clone.ObjectiveCoefficients.Length; j++)
                    clone.ObjectiveCoefficients[j] = -clone.ObjectiveCoefficients[j];
                clone.Objective = "max";
            }

            var tableau = new Tableau(clone);
            var result = SimplexSolver.simpleSimplexSolver(tableau);
            if (isMin && result.Solution.ContainsKey("Z"))
                result.Solution["Z"] = -result.Solution["Z"];
            return (result, tableau);
        }

        public static (double Lower, double Upper) NonBasicRange(Tableau tableau, LinearProgram program, int colIndex)
        {
            double reducedCost = tableau.Matrix[0, colIndex];
            double currentCoeff = program.ObjectiveCoefficients[colIndex];
            return (double.NegativeInfinity, currentCoeff + reducedCost);
        }

        public static Dictionary<string, double> ShadowPrices(Tableau tableau)
        {
            var result = new Dictionary<string, double>();
            for (int i = 0; i < tableau.NumConstraints; i++)
            {
                int slackColumn = tableau.NumVariables + i;
                result[$"C{i + 1}"] = Math.Round(tableau.Matrix[0, slackColumn], 3);
            }
            return result;
        }

        public static (double Lower, double Upper) BasicRange(Tableau tableau, LinearProgram program, int colIndex, int[] basis)
        {
            int row = Array.IndexOf(basis, colIndex);
            if (row == -1)
                throw new InvalidOperationException("The specified variable is not a basic variable.");

            row += 1;

            double currentCoeff = program.ObjectiveCoefficients[colIndex];
            double lowerDelta = double.NegativeInfinity;
            double upperDelta = double.PositiveInfinity;
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;

            for (int j = 0; j < rhsColumn; j++)
            {
                if (Array.IndexOf(basis, j) != -1)
                    continue;

                double reducedCost = tableau.Matrix[0, j];
                double coeff = tableau.Matrix[row, j];
                if (coeff == 0)
                    continue;

                double ratio = reducedCost / coeff;
                if (coeff > 0)
                    upperDelta = Math.Min(upperDelta, ratio);
                else
                    lowerDelta = Math.Max(lowerDelta, ratio);
            }

            return (currentCoeff + lowerDelta, currentCoeff + upperDelta);
        }

        public static (double Lower, double Upper) RhsRange(Tableau tableau, LinearProgram program, int rowIndex)
        {
            int slackColumn = tableau.NumVariables + rowIndex;
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;
            double currentRhs = program.Constraints[rowIndex].RightHandSide;
            double lowerDelta = double.NegativeInfinity;
            double upperDelta = double.PositiveInfinity;

            for (int row = 1; row < tableau.Matrix.GetLength(0); row++)
            {
                double a = tableau.Matrix[row, slackColumn];
                double rhs = tableau.Matrix[row, rhsColumn];
                if (a == 0)
                    continue;

                double ratio = -rhs / a;
                if (a > 0)
                    lowerDelta = Math.Max(lowerDelta, ratio);
                else
                    upperDelta = Math.Min(upperDelta, ratio);
            }

            return (currentRhs + lowerDelta, currentRhs + upperDelta);
        }

        public static SimplexResult ApplyRhsChange(LinearProgram program, int rowIndex, double newValue)
        {
            return ApplyRhsChangeWithTableau(program, rowIndex, newValue).Result;
        }

        public static (SimplexResult Result, Tableau Tableau) ApplyRhsChangeWithTableau(LinearProgram program, int rowIndex, double newValue)
        {
            var modified = CloneProgram(program);
            modified.Constraints[rowIndex].RightHandSide = newValue;
            return SolveProgramWithTableau(modified);
        }

        public static SimplexResult ApplyObjectiveCoeffChange(LinearProgram program, int colIndex, double newValue)
        {
            return ApplyObjectiveCoeffChangeWithTableau(program, colIndex, newValue).Result;
        }

        public static (SimplexResult Result, Tableau Tableau) ApplyObjectiveCoeffChangeWithTableau(LinearProgram program, int colIndex, double newValue)
        {
            var modified = CloneProgram(program);
            modified.ObjectiveCoefficients[colIndex] = newValue;
            return SolveProgramWithTableau(modified);
        }

        public static SimplexResult ApplyNonBasicColumnChange(LinearProgram program, int variableIndex, int constraintIndex, double newValue)
        {
            return ApplyNonBasicColumnChangeWithTableau(program, variableIndex, constraintIndex, newValue).Result;
        }

        public static (SimplexResult Result, Tableau Tableau) ApplyNonBasicColumnChangeWithTableau(LinearProgram program, int variableIndex, int constraintIndex, double newValue)
        {
            var modified = CloneProgram(program);
            modified.Constraints[constraintIndex].Coefficients[variableIndex] = newValue;
            return SolveProgramWithTableau(modified);
        }

        public static SimplexResult AddNewActivity(LinearProgram program, double objectiveCoeff, double[] constraintCoeffs)
        {
            return AddNewActivityWithTableau(program, objectiveCoeff, constraintCoeffs).Result;
        }

        public static (SimplexResult Result, Tableau Tableau) AddNewActivityWithTableau(LinearProgram program, double objectiveCoeff, double[] constraintCoeffs)
        {
            if (constraintCoeffs.Length != program.Constraints.Count)
                throw new ArgumentException("Number of coefficients must be equal to the number of constraints.");

            var modified = CloneProgram(program);

            var newObjectiveCoeffs = new double[modified.ObjectiveCoefficients.Length + 1];
            Array.Copy(modified.ObjectiveCoefficients, newObjectiveCoeffs, modified.ObjectiveCoefficients.Length);
            newObjectiveCoeffs[^1] = objectiveCoeff;
            modified.ObjectiveCoefficients = newObjectiveCoeffs;

            for (int i = 0; i < modified.Constraints.Count; i++)
            {
                var oldCoeffs = modified.Constraints[i].Coefficients;
                var newCoeffs = new double[oldCoeffs.Length + 1];
                Array.Copy(oldCoeffs, newCoeffs, oldCoeffs.Length);
                newCoeffs[^1] = constraintCoeffs[i];
                modified.Constraints[i].Coefficients = newCoeffs;
            }

            var newVariableTypes = new string[modified.VariableTypes.Length + 1];
            Array.Copy(modified.VariableTypes, newVariableTypes, modified.VariableTypes.Length);
            newVariableTypes[^1] = "+";
            modified.VariableTypes = newVariableTypes;

            return SolveProgramWithTableau(modified);
        }

        public static SimplexResult AddNewConstraint(LinearProgram program, double[] constraintCoeffs, string op, double rhs)
        {
            if (constraintCoeffs.Length != program.ObjectiveCoefficients.Length)
                throw new ArgumentException("Number of coefficients must be equal to the number of variables.");

            var modified = CloneProgram(program);
            modified.Constraints.Add(new Constraint(constraintCoeffs, op, rhs));
            return SolveProgram(modified);
        }

        public static (double Lower, double Upper) NonBasicColumnRange(Tableau tableau, int colIndex, int constraintRow)
        {
            double reducedCost = tableau.Matrix[0, colIndex];
            var shadowPrices = ShadowPrices(tableau);
            double shadowPrice = shadowPrices[$"C{constraintRow + 1}"];

            if (shadowPrice == 0)
                return (double.NegativeInfinity, double.PositiveInfinity);

            double delta = reducedCost / shadowPrice;
            return shadowPrice > 0
                ? (double.NegativeInfinity, delta)
                : (delta, double.PositiveInfinity);
        }

        public static LinearProgram BuildDual(LinearProgram primal)
        {
            bool primalIsMax = primal.Objective.Equals("max", StringComparison.OrdinalIgnoreCase);
            int m = primal.Constraints.Count;
            int n = primal.ObjectiveCoefficients.Length;

            var dual = new LinearProgram
            {
                Objective = primalIsMax ? "min" : "max",
                ObjectiveCoefficients = new double[m],
                VariableTypes = new string[m]
            };

            for (int i = 0; i < m; i++)
            {
                dual.ObjectiveCoefficients[i] = primal.Constraints[i].RightHandSide;
                string op = primal.Constraints[i].Operator;
                if (primalIsMax)
                {
                    //max primal
                    dual.VariableTypes[i] = op == "<=" ? "+" : (op == ">=" ? "-" : "urs");
                }
                else
                {
                    // min primal
                    dual.VariableTypes[i] = op == ">=" ? "+" : (op == "<=" ? "-" : "urs");
                }
            }

            for (int j = 0; j < n; j++)
            {
                var coeffs = new double[m];
                for (int i = 0; i < m; i++)
                    coeffs[i] = primal.Constraints[i].Coefficients[j];

                string primalVarType = j < primal.VariableTypes.Length ? primal.VariableTypes[j] : "+";
                string dualOp;

                if (primalIsMax)
                {
                    // max primal
                    dualOp = primalVarType switch
                    {
                        "+" or "bin" => ">=",   // x ≥ 0  ->  dual constraint ≥
                        "-" => "<=",   // x ≤ 0  ->  dual constraint ≤
                        _ => "="     // free    ->  dual constraint =
                    };
                }
                else
                {
                    // min primal
                    dualOp = primalVarType switch
                    {
                        "+" or "bin" => "<=",
                        "-" => ">=",
                        _ => "="
                    };
                }

                dual.Constraints.Add(new Constraint(coeffs, dualOp, primal.ObjectiveCoefficients[j]));
            }

            return dual;
        }

        public static string FormatProgram(LinearProgram program)
        {
            var sb = new StringBuilder();
            sb.Append(program.Objective.ToUpper());
            for (int j = 0; j < program.ObjectiveCoefficients.Length; j++)
            {
                double c = program.ObjectiveCoefficients[j];
                if (j == 0) sb.Append(c >= 0 ? $" +{c} y{j + 1}" : $" {c} y{j + 1}");
                else sb.Append(c >= 0 ? $" + {c} y{j + 1}" : $" - {Math.Abs(c)} y{j + 1}");
            }
            sb.AppendLine();
            //constraints
            for (int i = 0; i < program.Constraints.Count; i++)
            {
                var constraint = program.Constraints[i];
                for (int j = 0; j < constraint.Coefficients.Length; j++)
                {
                    double a = constraint.Coefficients[j];
                    if (j == 0) sb.Append(a >= 0 ? $"{a} y{j + 1}" : $"{a} y{j + 1}");
                    else sb.Append(a >= 0 ? $" + {a} y{j + 1}" : $" - {Math.Abs(a)} y{j + 1}");
                }
                sb.AppendLine($" {constraint.Operator} {constraint.RightHandSide}");
            }

            sb.AppendLine(string.Join(" ", program.VariableTypes));
            return sb.ToString();
        }

        public static SimplexResult SolveDual(LinearProgram primal)
        {
            // Solve the primal first (we need its optimal tableau)
            var (primalResult, primalTableau) = SolveProgramWithTableau(primal);

            // The dual solution is exactly the shadow prices
            var shadowPrices = ShadowPrices(primalTableau);

            var dualSolution = new Dictionary<string, double>();
            double dualZ = 0;

            int i = 0;
            foreach (var kvp in shadowPrices)
            {
                // y1, y2, ... 
                string dualVarName = $"y{i + 1}";
                dualSolution[dualVarName] = kvp.Value;
                dualZ += kvp.Value * primal.Constraints[i].RightHandSide;
                i++;
            }

            dualSolution["Z"] = Math.Round(dualZ, 3);

            return new SimplexResult
            {
                Solution = dualSolution,
                // you can leave Basis / other fields null or empty
            };
        }

        public static string VerifyDuality(LinearProgram primal, SimplexResult primalResult)
        {
            if (primalResult?.Solution == null || !primalResult.Solution.ContainsKey("Z"))
                return "Primal solution is missing, so duality cannot be verified.";

            try
            {
                var dualResult = SolveDual(primal);
                double zp = primalResult.Solution["Z"];
                double zd = dualResult.Solution["Z"];
                double gap = Math.Abs(zp - zd);

                if (gap <= 0.001)
                    return $"Strong duality holds. Primal Z = {zp}, Dual Z = {zd}.";

                return $"Weak duality: both models are feasible but the objectives differ. Primal Z = {zp}, Dual Z = {zd}, gap = {Math.Round(gap, 3)}.";
            }
            catch (InvalidOperationException ex)
            {
                return $"Could not verify duality: {ex.Message}";
            }
        }
    }
}
