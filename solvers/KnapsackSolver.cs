using System;
using System.Collections.Generic;
using System.Linq;
using LinearProgrammingSolver.Models;

namespace LinearProgrammingSolver.Solvers
{
    public static class KnapsackSolver
    {
        private class Item
        {
            public int OriginalIndex;
            public double Value;
            public double Weight;
            public double Ratio => Weight <= 0 ? double.PositiveInfinity : Value / Weight;
        }

        private class Node
        {
            public int ItemsDecided;
            public double CurrentValue;
            public double CurrentWeight;
            public bool[] Included;
            public string Label;

            public Node Clone() => new Node
            {
                ItemsDecided = ItemsDecided,
                CurrentValue = CurrentValue,
                CurrentWeight = CurrentWeight,
                Included = (bool[])Included.Clone(),
                Label = Label
            };
        }

        public static void Solve(LinearProgram program)
        {
            if (program.Constraints.Count != 1 || program.Constraints[0].Operator != "<="
                || program.VariableTypes.Any(t => t != "bin"))
            {
                Console.WriteLine("Warning: this model isn't a standard single-constraint 0/1 knapsack " +
                                   "(one '<=' constraint, all 'bin' variables). Proceeding using the first " +
                                   "constraint as the capacity anyway - results may not be meaningful.");
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
            int nodeCounter = 0;

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                nodeCounter++;

                double bound = ComputeBound(sorted, node, capacity);

                Console.WriteLine();
                Console.WriteLine($"--- Sub-problem {nodeCounter}: {node.Label} | value so far = {node.CurrentValue:F3}, weight so far = {node.CurrentWeight:F3}, bound = {bound:F3} ---");

                if (bound <= bestValue + 1e-9)
                {
                    Console.WriteLine($"Fathomed - bound {bound:F3} cannot beat current best {bestValue:F3}.");
                    continue;
                }

                if (node.ItemsDecided == n)
                {
                    if (node.CurrentValue > bestValue)
                    {
                        bestValue = node.CurrentValue;
                        bestIncluded = (bool[])node.Included.Clone();
                        Console.WriteLine($"New best candidate: Z = {bestValue:F3}");
                    }
                    else
                    {
                        Console.WriteLine("Complete assignment, not better than current best - fathomed.");
                    }
                    continue;
                }

                var item = sorted[node.ItemsDecided];
                string varName = $"x{item.OriginalIndex + 1}";

                // Explore "include" first - since items are sorted by ratio,
                // this tends to reach good candidates sooner.
                var excludeNode = node.Clone();
                excludeNode.ItemsDecided++;
                excludeNode.Label = $"{node.Label} -> exclude {varName}";
                stack.Push(excludeNode);

                if (node.CurrentWeight + item.Weight <= capacity + 1e-9)
                {
                    var includeNode = node.Clone();
                    includeNode.Included[item.OriginalIndex] = true;
                    includeNode.CurrentValue += item.Value;
                    includeNode.CurrentWeight += item.Weight;
                    includeNode.ItemsDecided++;
                    includeNode.Label = $"{node.Label} -> include {varName}";
                    stack.Push(includeNode);
                }
                else
                {
                    Console.WriteLine($"Include-branch for {varName} fathomed immediately - exceeds capacity.");
                }
            }

            Console.WriteLine();
            if (bestIncluded != null)
            {
                Console.WriteLine($"Best candidate: Z = {bestValue:F3}");
                for (int j = 0; j < n; j++)
                    Console.WriteLine($"  x{j + 1} = {(bestIncluded[j] ? 1 : 0)}");
            }
            else
            {
                Console.WriteLine("No feasible solution found.");
            }
        }
            // Dantzig bound: greedily fill remaining capacity by ratio, taking a
            // fractional slice of the item that doesn't fully fit - upper bound on
            // what this branch could reach.
        
        private static double ComputeBound(List<Item> sorted, Node node, double capacity)
        {
            double remainingCapacity = capacity - node.CurrentWeight;
            double bound = node.CurrentValue;

            for (int i = node.ItemsDecided; i < sorted.Count; i++)
            {
                var item = sorted[i];
                if (item.Weight <= remainingCapacity)
                {
                    bound += item.Value;
                    remainingCapacity -= item.Weight;
                }
                else
                {
                    if (item.Weight > 0)
                        bound += item.Ratio * remainingCapacity;
                    break;
                }
            }

            return bound;
        }
    }
}
