using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;

namespace linear_program_ui.TabControllers
{
    internal class CuttingPlaneTabController
    {
        public CuttingPlaneRunResult LastRun { get; set; }

        private readonly FlowLayoutPanel iterationsPanelCuttingPlane;
        private readonly DataGridView dgvBestCandidateCuttingPlane;

        public CuttingPlaneTabController(FlowLayoutPanel iterationsPanelCuttingPlane, DataGridView dgvBestCandidateCuttingPlane)
        {
            this.iterationsPanelCuttingPlane = iterationsPanelCuttingPlane;
            this.dgvBestCandidateCuttingPlane = dgvBestCandidateCuttingPlane;
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



        public void Run(LinearProgram linearProgram)
        {
            CuttingPlaneRunResult runResult;

            try
            {
                runResult = CuttingPlaneSolver.Solve(linearProgram);
                LastRun = runResult;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Cutting Plane Solver Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Cutting Plane Solver Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            iterationsPanelCuttingPlane.Controls.Clear();

            //display every cutting plane iteration

            foreach (var iteration in runResult.Iterations)
            {
                var headerLabel = new Label
                {
                    Text = iteration.IsIntergerSolution ? $"{iteration.Label} \u2605 Interger Solution" : iteration.Label,
                    AutoSize = true,
                    Font = new Font(
                        "Segoe UI",
                        11,
                        FontStyle.Bold),
                    Margin = new Padding(5, 15, 5, 2)


                };

                iterationsPanelCuttingPlane.Controls.Add(headerLabel);


                var statusLabel = new Label
                {
                    Text = iteration.Status,
                    AutoSize = true,
                    Font = new Font(
                        "Segoe UI",
                        9,
                        FontStyle.Italic),

                    Margin = new Padding(5, 0, 5, 5)
                };

                iterationsPanelCuttingPlane.Controls.Add(statusLabel);

                //Tableau

                var dgv = new DataGridView
                {
                    Margin = new Padding(15, 0, 5, 10),
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.None
                };


                PopulateCuttingPlaneTable(dgv, iteration.Matrix, iteration.ColumnHeaders);
                StyleDataGridView(dgv);
                SizeGridToContents(dgv);
                iterationsPanelCuttingPlane.Controls.Add(dgv);


                if (iteration.VariableValues != null)
                {
                    var solutionLabel = new Label
                    {
                        Text = $"Z = {iteration.ObjectiveValue:F3}",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Margin = new Padding(15, 0, 5, 10)
                    };

                    iterationsPanelCuttingPlane.Controls.Add(solutionLabel);
                }
            }

            PopulateBestCandidate(runResult.Best);
        }

        private void PopulateCuttingPlaneTable(DataGridView dgv, List<List<double>> matrix, List<string> headers)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            if (matrix == null || matrix.Count == 0)
            {
                return;
            }

            for (int col = 0; col < headers.Count; col++)
            {
                string header = headers[col];

                dgv.Columns.Add($"C{col}", header);
            }

            for (int row = 0; row < matrix.Count; row++)
            {
                object[] values = new object[matrix[row].Count];


                for (int col = 0; col < matrix[row].Count; col++)
                {
                    values[col] = Math.Round(matrix[row][col], 3);
                }

                dgv.Rows.Add(values);
            }

            if (dgv.Rows.Count > 0)
            {
                dgv.Rows[0].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
            }
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
            int maxWidth = Math.Max(850, iterationsPanelCuttingPlane.ClientSize.Width - 50);

            if (width > maxWidth)
            {
                dgv.Width = maxWidth;
                dgv.ScrollBars = ScrollBars.Horizontal;
                dgv.Height = height + SystemInformation.HorizontalScrollBarHeight;
            }
            else
            {
                dgv.Width = Math.Max(width, maxWidth - 60);
                dgv.ScrollBars = ScrollBars.None;
                dgv.Height = height;
            }
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
        private void PopulateBestCandidate(CuttingPlaneResult best)
        {
            dgvBestCandidateCuttingPlane.Columns.Clear();
            dgvBestCandidateCuttingPlane.Rows.Clear();
            dgvBestCandidateCuttingPlane.Columns.Add("Variable", "Variable");
            dgvBestCandidateCuttingPlane.Columns.Add("Value", "Value");

            if (best == null || !best.Found)
            {
                dgvBestCandidateCuttingPlane.Rows.Add("No interger-feasible solution found", "");
                StyleDataGridView(dgvBestCandidateCuttingPlane, isNumeric: false);
                SizeBestCandidateHeight(dgvBestCandidateCuttingPlane);
                return;
            }

            if (best.VariableValues != null)
            {
                for (int j = 0; j < best.VariableValues.Length; j++)
                {
                    dgvBestCandidateCuttingPlane.Rows.Add($"X{j + 1}", Math.Round(best.VariableValues[j], 3));
                }
            }

            dgvBestCandidateCuttingPlane.Rows.Add("Z", Math.Round(best.ObjectiveValue, 3));
            StyleDataGridView(dgvBestCandidateCuttingPlane, isNumeric: false);
            SizeBestCandidateHeight(dgvBestCandidateCuttingPlane);
        }


        public void SaveAllOutput()
        {
            if (LastRun == null)
            {
                MessageBox.Show("Nothing to save. Run solver first", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FileName = "CuttingPlaneOutput.txt",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var sb = new StringBuilder();

            sb.AppendLine("----Cutting Plane Solver---");
            sb.AppendLine();

            foreach (var iteration in LastRun.Iterations)
            {
                sb.AppendLine(iteration.Label);
                sb.AppendLine(iteration.Status);
                sb.AppendLine();

                foreach (string header in iteration.ColumnHeaders)
                {
                    sb.Append(header.PadLeft(10));
                }
                sb.AppendLine();

                foreach (var row in iteration.Matrix)
                {
                    foreach (double value in row)
                    {
                        sb.Append(value.ToString("F3").PadLeft(10));
                    }
                    sb.AppendLine();
                }

                if (iteration.VariableValues != null)
                {
                    sb.AppendLine("Solution: ");
                    for (int j = 0; j < iteration.VariableValues.Length; j++)
                    {
                        sb.AppendLine($"x{j + 1}=" + $"{iteration.VariableValues[j]:F3}");
                    }

                    sb.AppendLine($"Z={iteration.ObjectiveValue:F3}");
                }

                sb.AppendLine();
                sb.AppendLine("=======================================================");
                sb.AppendLine();
            }

            //best candidate

            sb.AppendLine("---Best candidate---");
            if (LastRun.Best != null && LastRun.Best.Found)
            {
                for (int j = 0; j < LastRun.Best.VariableValues.Length; j++)
                {
                    sb.AppendLine($"x{j + 1}= " + $"{LastRun.Best.VariableValues[j]:F3}");
                }

                sb.AppendLine($"Z = " + $"{LastRun.Best.ObjectiveValue:F3}");
                sb.AppendLine($"Found at: " + $"{LastRun.Best.SourceIterationLabel}");
            }

            else
            {
                sb.AppendLine("No interger-feasible solution");
            }

            File.WriteAllText(dialog.FileName, sb.ToString());

            MessageBox.Show($"Output save to {dialog.FileName}", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

