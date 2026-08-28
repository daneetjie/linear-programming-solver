using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;
using System;
using System.Collections.Generic;
using System.Text;

namespace linear_program_ui.TabControllers
{
    internal class KnapSackTabController
    {
        public KnapsackRunResult LastRun { get; set; }

        private readonly FlowLayoutPanel iterationsPanelKnapsack;
        private readonly DataGridView dgvBestCandidateKnapsack;

        public KnapSackTabController(
            FlowLayoutPanel iterationsPanelKnapsack,
            DataGridView dgvBestCandidateKnapsack)
        {
            this.iterationsPanelKnapsack = iterationsPanelKnapsack;
            this.dgvBestCandidateKnapsack = dgvBestCandidateKnapsack;
        }

        private static void StyleDataGridView(DataGridView dgv, bool isNumeric = true)
        {
            Color softBlue = Color.FromArgb(240, 248, 255);

            dgv.VirtualMode = false;
            dgv.BackgroundColor = softBlue;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;

            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = softBlue,
                Font = new Font("Consolas", 9f),
                Alignment = isNumeric
                    ? DataGridViewContentAlignment.MiddleRight
                    : DataGridViewContentAlignment.MiddleLeft
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 240, 255)
            };
        }

        private void SizeBestCandidateHeight(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0) return;

            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.PerformLayout();

            int height = dgv.ColumnHeadersHeight + 2;
            foreach (DataGridViewRow row in dgv.Rows)
                height += row.Height;

            dgv.Height = height + 6;
            dgv.ScrollBars = ScrollBars.None;
        }


        public void Run(LinearProgram linearProgram)
        {
            KnapsackRunResult runResult;

            try
            {
                runResult = KnapsackSolver.Solve(linearProgram);
                LastRun = runResult;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    $"Error: {ex.Message}",
                    "Knapsack Solver Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unexpected error: {ex.Message}",
                    "Knapsack Solver Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            iterationsPanelKnapsack.Controls.Clear();

            if (runResult == null || runResult.Nodes == null)
            {
                MessageBox.Show(
                    "The Knapsack solver returned no results.",
                    "Knapsack Solver",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            foreach (var node in runResult.Nodes)
            {
                AddNodeToPanel(node);
            }

            PopulateBestCandidate(runResult.Best);
        }

        private void AddNodeToPanel(KnapsackNodeResult node)
        {
            bool isRoot = node.Depth == 0;
            string title;
            if (isRoot) title = "Knapsack Problem";
            else if (node.IsNewBest) title = $"{node.Label} ★ NEW BEST";
            else title = node.Label;


            var headerLabel = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
                Margin = new Padding(5, 15, 5, 2)
            };

            iterationsPanelKnapsack.Controls.Add(headerLabel);

            if (isRoot) { AddCanonicalForm(node); }



            var informationLabel = new Label
            {
                Text =
                    $"Depth: {node.Depth}    " +
                    $"Current Value: {node.CurrentValue:F3}    " +
                    $"Current Weight: {node.CurrentWeight:F3}    " +
                    $"Upper Bound: {node.UpperBound:F3}",

                AutoSize = true,

                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular),

                Margin = new Padding(15, 2, 5, 2)
            };

            iterationsPanelKnapsack.Controls.Add(informationLabel);



            var statusLabel = new Label
            {
                Text = node.Status,

                AutoSize = true,

                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Italic),

                Margin = new Padding(15, 0, 5, 5)
            };

            iterationsPanelKnapsack.Controls.Add(statusLabel);



            if (node.VariableValues != null)
            {
                var dgv = new DataGridView
                {
                    Margin = new Padding(15, 0, 5, 10),

                    AllowUserToAddRows = false,

                    RowHeadersVisible = false,

                    ReadOnly = true,

                    AutoGenerateColumns = false,

                    ScrollBars = ScrollBars.None
                };

                dgv.Columns.Add("Variable", "Variable");

                dgv.Columns.Add("Value", "Value");

                for (int i = 0; i < node.VariableValues.Length; i++)
                {
                    dgv.Rows.Add($"x{i + 1}", Math.Round(node.VariableValues[i], 3));
                }
                StyleDataGridView(dgv, isNumeric: false);

                SizeGridToContents(dgv);

                iterationsPanelKnapsack.Controls.Add(dgv);
            }
        }
        private void AddCanonicalForm(KnapsackNodeResult node)
        {
            var label = new Label
            {
                Text = "Knapsack Model",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold),
                Margin = new Padding(15, 5, 5, 3)
            };

            iterationsPanelKnapsack.Controls.Add(label);


            var objective = new StringBuilder();
            objective.Append("Max Z = ");

            for (int i = 0; i < node.ItemValues.Length; i++)
            {
                if (i > 0)
                    objective.Append(" + ");

                objective.Append(
                    $"{node.ItemValues[i]:F3}x{i + 1}");
            }


            var objectiveLabel = new Label
            {
                Text = objective.ToString(),
                AutoSize = true,
                Font = new Font("Consolas", 10),
                Margin = new Padding(15, 0, 5, 3)
            };

            iterationsPanelKnapsack.Controls.Add(objectiveLabel);


            var constraint = new StringBuilder();

            for (int i = 0; i < node.ItemWeights.Length; i++)
            {
                if (i > 0)
                    constraint.Append(" + ");

                constraint.Append(
                    $"{node.ItemWeights[i]:F3}x{i + 1}");
            }

            constraint.Append(
                $" <= {node.Capacity:F3}");


            var constraintLabel = new Label
            {
                Text = constraint.ToString(),
                AutoSize = true,
                Font = new Font("Consolas", 10),
                Margin = new Padding(15, 0, 5, 3)
            };

            iterationsPanelKnapsack.Controls.Add(constraintLabel);


            var binaryLabel = new Label
            {
                Text = "x1, x2, ..., xn ∈ {0, 1}",
                AutoSize = true,
                Font = new Font("Consolas", 10),
                Margin = new Padding(15, 0, 5, 10)
            };

            iterationsPanelKnapsack.Controls.Add(binaryLabel);



            var dgv = new DataGridView
            {
                Margin = new Padding(15, 0, 5, 10),
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                ScrollBars = ScrollBars.None
            };

            dgv.Columns.Add("Variable", "Variable");
            dgv.Columns.Add("Value", "Value");
            dgv.Columns.Add("Weight", "Weight");
            dgv.Columns.Add("Ratio", "Value / Weight");

            for (int i = 0; i < node.ItemValues.Length; i++)
            {
                dgv.Rows.Add(
                    $"x{i + 1}",
                    node.ItemValues[i].ToString("F3"),
                    node.ItemWeights[i].ToString("F3"),
                    node.ItemRatios[i].ToString("F3"));
            }

            StyleDataGridView(dgv);

            SizeGridToContents(dgv);

            iterationsPanelKnapsack.Controls.Add(dgv);
        }

        private void SizeGridToContents(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.PerformLayout();

            int width = 2;
            foreach (DataGridViewColumn column in dgv.Columns)
                width += column.Width;
            width += 20;

            int height = dgv.ColumnHeadersHeight + 2;
            foreach (DataGridViewRow row in dgv.Rows)
                height += row.Height;
            height += 4;

            int maxWidth = Math.Max(850, iterationsPanelKnapsack.ClientSize.Width - 50);

            if (width > maxWidth)
            {
                dgv.Width = maxWidth;
                dgv.ScrollBars = ScrollBars.Horizontal;
                dgv.Height = height + SystemInformation.HorizontalScrollBarHeight;
            }
            else
            {
                dgv.Width = width;
                dgv.ScrollBars = ScrollBars.None;
                dgv.Height = height;
            }
        }


        // BEST CANDIDATE

        private void PopulateBestCandidate(
            KnapsackResult best)
        {
            dgvBestCandidateKnapsack.Columns.Clear();
            dgvBestCandidateKnapsack.Rows.Clear();

            dgvBestCandidateKnapsack.Columns.Add("Variable", "Variable");

            dgvBestCandidateKnapsack.Columns.Add("Value", "Value");

            if (best == null || !best.Found)
            {
                dgvBestCandidateKnapsack.Rows.Add("No feasible solution found.", "");

                return;
            }
            else
            {
                for (int j = 0; j < best.VariableValues.Length; j++)

                    dgvBestCandidateKnapsack.Rows.Add($"X{j + 1}", Math.Round(best.VariableValues[j], 3));

                dgvBestCandidateKnapsack.Rows.Add("Z", Math.Round(best.ObjectiveValue, 3));


            }
            StyleDataGridView(dgvBestCandidateKnapsack, isNumeric: false);
            dgvBestCandidateKnapsack.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SizeBestCandidateHeight(dgvBestCandidateKnapsack);




        }




        public void SaveAllOutput()
        {
            if (LastRun == null)
            {
                MessageBox.Show(
                    "Nothing to save. Run the solver first.",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter =
                    "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",

                FileName =
                    "KnapsackBranchAndBoundOutput.txt",

                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var sb = new StringBuilder();

            sb.AppendLine(
                "=== KNAPSACK BRANCH AND BOUND ===");

            sb.AppendLine();


            //adding al subproblems

            foreach (var node in LastRun.Nodes)
            {
                sb.AppendLine(node.Label);

                sb.AppendLine(
                    $"Depth = {node.Depth}");

                sb.AppendLine(
                    $"Current Value = {node.CurrentValue:F3}");

                sb.AppendLine(
                    $"Current Weight = {node.CurrentWeight:F3}");

                sb.AppendLine(
                    $"Upper Bound = {node.UpperBound:F3}");

                sb.AppendLine(
                    $"Status = {node.Status}");

                if (node.VariableValues != null)
                {
                    sb.AppendLine(
                        "Variable Values:");

                    for (int j = 0;
                         j < node.VariableValues.Length;
                         j++)
                    {
                        sb.AppendLine(
                            $"  x{j + 1} = " +
                            $"{node.VariableValues[j]:F3}");
                    }
                }

                if (node.IsNewBest)
                {
                    sb.AppendLine(
                        "*** NEW BEST SOLUTION ***");
                }

                sb.AppendLine();
            }


            //best solution

            sb.AppendLine(
                "=== BEST CANDIDATE ===");

            if (LastRun.Best != null &&
                LastRun.Best.Found)
            {
                sb.AppendLine(
                    $"Source Node = " +
                    $"{LastRun.Best.SourceNodeLabel}");

                for (int j = 0;
                     j < LastRun.Best.VariableValues.Length;
                     j++)
                {
                    sb.AppendLine(
                        $"x{j + 1} = " +
                        $"{LastRun.Best.VariableValues[j]:F3}");
                }

                sb.AppendLine(
                    $"Z = " +
                    $"{LastRun.Best.ObjectiveValue:F3}");
            }
            else
            {
                sb.AppendLine(
                    "No feasible solution found.");
            }


            File.WriteAllText(
                dialog.FileName,
                sb.ToString());

            MessageBox.Show(
                $"Output saved to {dialog.FileName}",
                "Save Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

    }
}
