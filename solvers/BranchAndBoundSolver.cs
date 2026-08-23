using System;
using System.Collections.Generic;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public class BranchAndBoundNode
    {
        public LinearProgram Program { get; }
        public int Depth { get; }
        public string Label { get; }

        public BranchAndBoundNode(LinearProgram program, int depth, string label)
        {
            Program = program;
            Depth = depth;
            Label = label;
        }
    }

    public class BranchAndBoundResult
    {
        public bool Found { get; set; }
        public double ObjectiveValue { get; set; }
        public double[] VariableValues { get; set; }
        public string SourceNodeLabel { get; set; }
    }

    public static class BranchAndBoundSolver
    {
        public static BranchAndBoundResult Solve(LinearProgram rootProgram)
        {
            // Stack = depth-first with backtracking built in - fathoming a
            // branch just means popping the next one off the stack.

            var stack = new Stack<BranchAndBoundNode>();
            stack.Push(new BranchAndBoundNode(rootProgram, 0, "Root"));

            BranchAndBoundResult best = null;
            int nodeCounter = 0;

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                nodeCounter++;

                Console.WriteLine();
                Console.WriteLine($"--- Sub-problem {nodeCounter}: {node.Label} (depth {node.Depth}) ---");

                var tableau = new Tableau(node.Program);
                Solve(tableau); // prints canonical form + all iterations itself

                var result = SolutionReader.ExtractSolution(tableau);

                if (result.Status == LpStatus.Unbounded)
                {
                    Console.WriteLine($"Sub-problem {nodeCounter} fathomed - relaxation is unbounded.");
                    continue;
                }

                // Fathom by bound: relaxation can't beat the current best.
                if (best != null && result.ObjectiveValue <= best.ObjectiveValue + 1e-9)
                {
                    Console.WriteLine($"Sub-problem {nodeCounter} fathomed - bound {result.ObjectiveValue:F3} cannot beat current best {best.ObjectiveValue:F3}.");
                    continue;
                }

                int fractionalIndex = SolutionReader.FindFractionalVariable(result);

                if (fractionalIndex == -1)
                {
                    Console.WriteLine($"Sub-problem {nodeCounter} is integer-feasible - candidate found, Z = {result.ObjectiveValue:F3}");

                    if (best == null || result.ObjectiveValue > best.ObjectiveValue)
                    {
                        best = new BranchAndBoundResult
                        {
                            Found = true,
                            ObjectiveValue = result.ObjectiveValue,
                            VariableValues = result.VariableValues,
                            SourceNodeLabel = node.Label
                        };
                        Console.WriteLine($"New best candidate: Z = {best.ObjectiveValue:F3}");
                    }

                    continue; // fathomed: integer-feasible leaf
                }

                double fractionalValue = result.VariableValues[fractionalIndex];
                double floorBound = Math.Floor(fractionalValue);
                double ceilBound = Math.Ceiling(fractionalValue);

                Console.WriteLine($"Sub-problem {nodeCounter} fractional on x{fractionalIndex + 1} = {fractionalValue:F3} - branching.");

                var childLow = LinearProgramCloner.WithExtraBound(node.Program, fractionalIndex, "<=", floorBound);
                var childHigh = LinearProgramCloner.WithExtraBound(node.Program, fractionalIndex, ">=", ceilBound);

                // Push both children onto the stack. LIFO order gives
                // depth-first exploration with backtracking for free.
                stack.Push(new BranchAndBoundNode(childHigh, node.Depth + 1,
                    $"{node.Label} + x{fractionalIndex + 1} >= {ceilBound:F0}"));
                stack.Push(new BranchAndBoundNode(childLow, node.Depth + 1,
                    $"{node.Label} + x{fractionalIndex + 1} <= {floorBound:F0}"));
            }

            Console.WriteLine();
            if (best != null)
            {
                Console.WriteLine($"Best candidate found at [{best.SourceNodeLabel}]: Z = {best.ObjectiveValue:F3}");
                for (int j = 0; j < best.VariableValues.Length; j++)
                    Console.WriteLine($"  x{j + 1} = {best.VariableValues[j]:F3}");
            }
            else
            {
                Console.WriteLine("No integer-feasible solution found.");
            }

            return best ?? new BranchAndBoundResult { Found = false };
        }


        private static void Solve(Tableau tableau)
        {
            if (HasNegativeRhs(tableau))
                DualSolver.simpleDualSimplexSolver(tableau);
            else
                SimplexSolver.simpleSimplexSolver(tableau);
        }

        private static bool HasNegativeRhs(Tableau tableau)
        {
            int rhsColumn = tableau.Matrix.GetLength(1) - 1;
            for (int row = 1; row < tableau.Matrix.GetLength(0); row++)
                if (tableau.Matrix[row, rhsColumn] < 0)
                    return true;
            return false;
        }
    }
}
