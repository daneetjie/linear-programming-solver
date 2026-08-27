using System;
using System.Collections.Generic;
using System.Linq;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public class KnapsackNodeResult
    {
        public string Label { get; set; }
        public int Depth { get; set; }
        public double CurrentValue { get; set; }
        public double CurrentWeight { get; set; }
        public double UpperBound { get; set; }
        public double[] VariableValues { get; set; }
        public string Status { get; set; }
        public bool IsNewBest { get; set; }
        public double[] ItemValues { get; set; }
        public double[] ItemWeights { get; set; }
        public double[] ItemRatios { get; set; }
        public double Capacity { get; set; }
    }

    public class KnapsackResult
    {
        public bool Found { get; set; }
        public double ObjectiveValue { get; set; }
        public double[] VariableValues { get; set; }
        public string SourceNodeLabel { get; set; }
    }

    public class KnapsackRunResult
    {
        public List<KnapsackNodeResult> Nodes { get; set; } = new();

        public KnapsackResult Best { get; set; }
    }
    public static class KnapsackSolver
    {
        private const double Tolerance = 1e-9;

        private class Item
        {
            public int OriginalIndex;
            public double Value;
            public double Weight;
            public double Ratio => Weight <= 0 ? double.PositiveInfinity : Value / Weight;
        }

        private class Node
        {
            public int ItemsDecided { get; set; }
            public double CurrentValue { get; set; }
            public double CurrentWeight { get; set; }
            public bool[] Included { get; set; }
            public int Depth { get; set; }
            public string Label { get; set; }

            public Node Clone() => new Node
            {
                ItemsDecided = ItemsDecided,
                CurrentValue = CurrentValue,
                CurrentWeight = CurrentWeight,
                Included = (bool[])Included.Clone(),
                Depth = Depth,
                Label = Label
            };
        }

        public static KnapsackRunResult Solve(LinearProgram program)
        {
            var runResult = new KnapsackRunResult();

            if (!IsKnapSackModel(program))
            {
                runResult.Best = new KnapsackResult
                {
                    Found = false
                };

                runResult.Nodes.Add(new KnapsackNodeResult
                {
                    Label = "Root",
                    Depth = 0,
                    Status = "Model is not a standard 0/1 knapsack problem."
                });

                return runResult;
            }


            int n = program.ObjectiveCoefficients.Length;
            var constraint = program.Constraints[0];
            double capacity = constraint.RightHandSide;

            var items = new List<Item>(n);
            for (int i = 0; i < n; i++)
            {
                items.Add(new Item
                {
                    OriginalIndex = i,
                    Value = program.ObjectiveCoefficients[i],
                    Weight = constraint.Coefficients[i]
                });
            }

            var sorted = items.OrderByDescending(it => it.Ratio).ToList();

            var stack = new Stack<Node>();
            stack.Push(new Node { ItemsDecided = 0, CurrentValue = 0, CurrentWeight = 0, Included = new bool[n], Label = "Root" });

            double bestValue = double.NegativeInfinity;
            bool[] bestIncluded = null;
            string bestLabel = null;
            int nodeCounter = 0;

            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                nodeCounter++;

                double bound = ComputeBound(sorted, node, capacity);
                var nodeResult = new KnapsackNodeResult
                {
                    Label = $"Sub-problem {nodeCounter}: {node.Label}",
                    Depth = node.Depth,
                    CurrentValue = node.CurrentValue,
                    CurrentWeight = node.CurrentWeight,
                    UpperBound = bound,
                    VariableValues = ConvertToVariableValues(node.Included),
                    ItemValues = items.Select(x => x.Value).ToArray(),
                    ItemWeights = items.Select(x => x.Weight).ToArray(),
                    ItemRatios = items.Select(x => x.Ratio).ToArray(),
                    Capacity = capacity,
                    IsNewBest = false
                };



                if (bound <= bestValue + 1e-9)
                {
                    nodeResult.Status = $"Fathomed - upper bound {bound:F3} " + $"cannot beat current best {bestValue:F3}.";
                    runResult.Nodes.Add(nodeResult);
                    continue;
                }

                if (node.ItemsDecided == n)
                {
                    if (node.CurrentValue > bestValue + Tolerance)
                    {
                        bestValue = node.CurrentValue;
                        bestIncluded = (bool[])node.Included.Clone();
                        bestLabel = nodeResult.Label;
                        nodeResult.IsNewBest = true;
                        nodeResult.Status = $"Integer-feasible solution - NEW BEST. " + $"Z = {bestValue:F3}";


                    }
                    else
                    {
                        nodeResult.Status = $"Integer-feasible solution, " + $"but not better than current best " + $"Z = {bestValue:F3}.";
                    }

                    runResult.Nodes.Add(nodeResult);
                    continue;
                }

                Item item = sorted[node.ItemsDecided];
                string variableName = $"x{item.OriginalIndex + 1}";

                nodeResult.Status =
                    $"Branch on {variableName} " +
                    $"(value = {item.Value:F3}, " +
                    $"weight = {item.Weight:F3}, " +
                    $"ratio = {item.Ratio:F3}). " +
                    $"Upper bound = {bound:F3}.";

                runResult.Nodes.Add(nodeResult);

                // Explore "include" first - since items are sorted by ratio,
                // this tends to reach good candidates sooner.
                Node excludeNode = node.Clone();
                excludeNode.ItemsDecided++;
                excludeNode.Depth++;
                excludeNode.Label = $"{node.Label} -> x{item.OriginalIndex + 1} = 0";

                Node includeNode = node.Clone();

                includeNode.ItemsDecided++;
                includeNode.Depth++;
                includeNode.Label = $"{node.Label} -> x{item.OriginalIndex + 1} = 1";
                includeNode.Included[item.OriginalIndex] = true;
                includeNode.CurrentValue += item.Value;
                includeNode.CurrentWeight += item.Weight;

                stack.Push(excludeNode);

                if (includeNode.CurrentWeight <= capacity + Tolerance)
                {
                    stack.Push(includeNode);
                }
                else
                {
                    runResult.Nodes.Add(new KnapsackNodeResult
                    {
                        Label = $"Sub-problem {nodeCounter}.1: " + $"{includeNode.Label}",
                        Depth = includeNode.Depth,
                        CurrentValue = includeNode.CurrentValue,
                        CurrentWeight = includeNode.CurrentWeight,
                        UpperBound = includeNode.CurrentValue,
                        VariableValues = ConvertToVariableValues(includeNode.Included),
                        Status = $"Fathomed - {variableName} = 1 " + $"would exceed capacity " + $"({includeNode.CurrentWeight:F3} > " + $"{capacity:F3})."
                    });
                }
            }

            if (bestIncluded != null)
            {
                runResult.Best = new KnapsackResult
                {
                    Found = true,
                    ObjectiveValue = bestValue,
                    VariableValues = ConvertToVariableValues(bestIncluded),
                    SourceNodeLabel = bestLabel
                };
            }
            else
            {
                runResult.Best = new KnapsackResult
                {
                    Found = false
                };
            }

            return runResult;
        }

        private static bool IsKnapSackModel(LinearProgram program)
        {
            if (program == null) return false;

            if (program.Constraints == null || program.Constraints.Count != 1) return false;

            if (program.Constraints[0].Operator != "<=") return false;

            if (program.VariableTypes == null || program.VariableTypes.Any(type => type != "bin")) return false;

            return true;

        }







        private static double ComputeBound(List<Item> sorted, Node node, double capacity)
        {
            double remainingCapacity = capacity - node.CurrentWeight;
            double bound = node.CurrentValue;

            if (remainingCapacity < -Tolerance) return double.NegativeInfinity;


            for (int i = node.ItemsDecided; i < sorted.Count; i++)
            {
                Item item = sorted[i];
                if (item.Weight <= 0)
                {
                    if (item.Value > 0)
                        bound += item.Value;
                    continue;
                }
                if (item.Weight <= remainingCapacity + Tolerance)
                {
                    bound += item.Value;
                    remainingCapacity -= item.Weight;
                }
                else
                {

                    bound += item.Ratio * Math.Max(0, remainingCapacity);
                    break;
                }
            }

            return bound;
        }

        //convert bool[] to x1,x2 ect

        private static double[] ConvertToVariableValues(bool[] included)
        {
            var values = new double[included.Length];
            for (int i = 0; i < included.Length; i++)
            {
                values[i] = included[i] ? 1.0 : 0.0;
            }
            return values;
        }
    }
}
