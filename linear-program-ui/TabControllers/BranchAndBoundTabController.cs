using LinearProgrammingSolver.Solvers;
using System;
using System.Collections.Generic;
using System.Text;
using LinearProgrammingSolver.Models;
using System.Windows.Forms;
using System.IO;

namespace linear_program_ui.TabControllers
{
    internal class BranchAndBoundTabController
    {
        public BranchAndBoundRunResult LastRun { get; set; }

        private readonly FlowLayoutPanel iterationsPanelBranchBound;
        private readonly DataGridView dgvBestCandidateBranchBound;


        public BranchAndBoundTabController(FlowLayoutPanel iterationsPanelBranchBound, DataGridView dgvBestCandidateBranchBound)
        {
            this.iterationsPanelBranchBound = iterationsPanelBranchBound;
            this.dgvBestCandidateBranchBound = dgvBestCandidateBranchBound;
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

        public void Run(LinearProgram linearprogram)
        {
            BranchAndBoundRunResult runResult;
            try
            {
                runResult = BranchAndBoundSolver.Solve(linearprogram);
                LastRun = runResult;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Branch and Bound Solver Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            iterationsPanelBranchBound.Controls.Clear();

            foreach (var node in runResult.Nodes)
            {
                var headerLabel = new Label
                {
                    Text = node.IsNewBest ? $"{node.Label} \u2605 NEW BEST" : node.Label,
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
                    Margin = new Padding(5, 15, 5, 2)
                };
                iterationsPanelBranchBound.Controls.Add(headerLabel);

                var statusLabel = new Label
                {
                    Text = node.Status,
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Italic),
                    Margin = new Padding(5, 0, 5, 5)
                };
                iterationsPanelBranchBound.Controls.Add(statusLabel);

                var tableau = node.Tableau;
                for (int i = 0; i < tableau.IterationHistory.Count; i++)
                {
                    var iterLabel = new Label
                    {
                        Text = i == 0 ? "Canonical Form" : $"Iteration {i}",
                        AutoSize = true,
                        Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                        Margin = new Padding(15, 5, 5, 3)
                    };

                    var dgv = new DataGridView
                    {
                        Margin = new Padding(15, 0, 5, 10),
                        AllowUserToAddRows = false,
                        RowHeadersVisible = false,
                        ScrollBars = ScrollBars.None
                    };

                    TableDisplay.PopulateTableau(dgv, tableau, tableau.IterationHistory[i]);
                    StyleDataGridView(dgv);
                    SizeGridToContents(dgv);

                    iterationsPanelBranchBound.Controls.Add(iterLabel);
                    iterationsPanelBranchBound.Controls.Add(dgv);
                }


            }

            PopulateBestCandidate(runResult.Best);
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

            int maxWidth = Math.Max(850, iterationsPanelBranchBound.ClientSize.Width - 50);

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


        private void PopulateBestCandidate(BranchAndBoundResult best)
        {
            dgvBestCandidateBranchBound.Columns.Clear();
            dgvBestCandidateBranchBound.Rows.Clear();
            dgvBestCandidateBranchBound.Columns.Add("Variable", "Variable");
            dgvBestCandidateBranchBound.Columns.Add("Value", "Value");

            if (best == null || !best.Found)
            {
                dgvBestCandidateBranchBound.Rows.Add("No interger-feasible solution found,", "");
                return;
            }
            else
            {

                for (int j = 0; j < best.VariableValues.Length; j++)

                    dgvBestCandidateBranchBound.Rows.Add($"X{j + 1}", Math.Round(best.VariableValues[j], 3));

                dgvBestCandidateBranchBound.Rows.Add("Z", Math.Round(best.ObjectiveValue, 3));
            }

            StyleDataGridView(dgvBestCandidateBranchBound, isNumeric: false);
            dgvBestCandidateBranchBound.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SizeGridToContents(dgvBestCandidateBranchBound);

        }

        public void SaveAllOutput()
        {
            if (LastRun == null)
            {
                MessageBox.Show("Nothing to save/ Run solver first", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FileName = "BranchAndBoundOutput.txt",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var sb = new StringBuilder();
            sb.AppendLine("---Branch And Bound Simplex---");
            sb.AppendLine();

            foreach (var node in LastRun.Nodes)
            {
                sb.AppendLine(node.Label);
                sb.AppendLine(node.Status);
                sb.Append(TableDisplay.FormatAllIterations(node.Tableau, " All iterations"));
                sb.AppendLine();
            }

            sb.AppendLine("===BEST candidate===");
            if (LastRun.Best != null && LastRun.Best.Found)
            {
                for (int j = 0; j < LastRun.Best.VariableValues.Length; j++)
                    sb.AppendLine($"x{j + 1}={LastRun.Best.VariableValues[j]:F3}");
                sb.AppendLine($"Z= {LastRun.Best.ObjectiveValue:F3}");

            }
            else
            {
                sb.AppendLine("No interger-feasible solution found.");
            }

            File.WriteAllText(dialog.FileName, sb.ToString());
            MessageBox.Show($"Output save to {dialog.FileName}", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



    }

}