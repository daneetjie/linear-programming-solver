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

    //one entry per sub-problem explored

    public class BranchAndBoundNodeResult
    {
        public string Label { get; set; }
        public int Depth { get; set; }
        public Tableau Tableau { get; set; }
        public string Status { get; set; }
        public bool IsNewBest { get; set; }
    }

    //put all sub-problems togeth + best candidate
    public class BranchAndBoundRunResult
    {
        public List<BranchAndBoundNodeResult> Nodes { get; set; } = new();

        public BranchAndBoundResult Best { get; set; }
    }




    public static class BranchAndBoundSolver
    {
        public static BranchAndBoundRunResult Solve(LinearProgram rootProgram)
        {
            // Stack = depth-first with backtracking built in - fathoming a
            // branch just means popping the next one off the stack.

            var stack = new Stack<BranchAndBoundNode>();
            stack.Push(new BranchAndBoundNode(rootProgram, 0, "Root"));
            var runResult = new BranchAndBoundRunResult();
            BranchAndBoundResult best = null;
            int nodeCounter = 0;

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                nodeCounter++;


                var tableau = new Tableau(node.Program);
                var solveStatus = Solve(tableau);

                var nodeResult = new BranchAndBoundNodeResult
                {
                    Label = $"Sub-problem {nodeCounter}: {node.Label}",
                    Depth = node.Depth,
                    Tableau = tableau
                };

                if (solveStatus == LpStatus.Infeasible)
                {
                    nodeResult.Status = "fathomed - relaxation is infeasible.";
                    runResult.Nodes.Add(nodeResult);
                    continue;
                }

                var result = SolutionReader.ExtractSolution(tableau);

                if (result.Status == LpStatus.Unbounded)
                {
                    nodeResult.Status = "fathomed - relaxation is unbounded.";
                    runResult.Nodes.Add(nodeResult);
                    continue;
                }

                // Fathom by bound: relaxation can't beat the current best.
                if (best != null && result.ObjectiveValue <= best.ObjectiveValue + 1e-9)
                {
                    nodeResult.Status = $"fathomed - bound {result.ObjectiveValue:F3} cannot beat current best {best.ObjectiveValue:F3}.";
                    runResult.Nodes.Add(nodeResult);
                    continue;
                }

                int fractionalIndex = SolutionReader.FindFractionalVariable(result);

                if (fractionalIndex == -1)
                {
                    nodeResult.Status = $"Integer-feasible - candidate found, Z = {result.ObjectiveValue:F3}";

                    if (best == null || result.ObjectiveValue > best.ObjectiveValue)
                    {
                        best = new BranchAndBoundResult
                        {
                            Found = true,
                            ObjectiveValue = result.ObjectiveValue,
                            VariableValues = result.VariableValues,
                            SourceNodeLabel = node.Label
                        };
                        nodeResult.IsNewBest = true;
                    }
                    runResult.Nodes.Add(nodeResult);
                    continue; // fathomed: integer-feasible leaf
                }

                double fractionalValue = result.VariableValues[fractionalIndex];
                double floorBound = Math.Floor(fractionalValue);
                double ceilBound = Math.Ceiling(fractionalValue);

                nodeResult.Status = $"Fractional on x{fractionalIndex + 1} = {fractionalValue:F3} - branching.";

                var childLow = LinearProgramCloner.WithExtraBound(node.Program, fractionalIndex, "<=", floorBound);
                var childHigh = LinearProgramCloner.WithExtraBound(node.Program, fractionalIndex, ">=", ceilBound);

                // Push both children onto the stack. LIFO order gives
                // depth-first exploration with backtracking for free.
                stack.Push(new BranchAndBoundNode(childHigh, node.Depth + 1,
                    $"{node.Label} + x{fractionalIndex + 1} >= {ceilBound:F0}"));
                stack.Push(new BranchAndBoundNode(childLow, node.Depth + 1,
                    $"{node.Label} + x{fractionalIndex + 1} <= {floorBound:F0}"));
            }

            runResult.Best = best ?? new BranchAndBoundResult { Found = false };

            return runResult;
        }


        // A branch bound such as x >= 1 leaves a negative RHS, so dual simplex runs first to restore
        // feasibility - but it stops there, with row 0 possibly still negative. Primal simplex has to
        // finish the job, or the node reports an understated bound and its branch is wrongly fathomed.
        private static LpStatus Solve(Tableau tableau)
        {
            if (HasNegativeRhs(tableau))
            {
                if (DualSolver.simpleDualSimplexSolver(tableau) == LpStatus.Infeasible)
                    return LpStatus.Infeasible;

                if (HasNegativeRhs(tableau))
                    return LpStatus.Infeasible;
            }

            try
            {
                SimplexSolver.simpleSimplexSolver(tableau);
            }
            catch (InvalidOperationException)
            {
                // Unbounded or non-converging node - SolutionReader detects and fathoms it.
            }

            return LpStatus.Optimal;
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
