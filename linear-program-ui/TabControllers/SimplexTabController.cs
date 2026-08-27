using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;
using System;
using System.Collections.Generic;
using System.Text;

namespace linear_program_ui.TabControllers
{
    public class SimplexTabController
    {
        public Tableau LastTableau { get; private set; }
        public SimplexResult LastResult { get; private set; }

        private readonly DataGridView canonicalDgv;
        private readonly FlowLayoutPanel iterationsPanel;
        private readonly DataGridView optimalDgv;
        private readonly Func<Tableau, SimplexResult> solve;
        private readonly string errorTitle;
        private readonly string outputHeader;

        public SimplexTabController(
            DataGridView canonicalDgv,
            FlowLayoutPanel iterationsPanel,
            DataGridView optimalDgv,
            Func<Tableau, SimplexResult> solve,
            string errorTitle,
            string outputHeader)
        {
            this.canonicalDgv = canonicalDgv;
            this.iterationsPanel = iterationsPanel;
            this.optimalDgv = optimalDgv;
            this.solve = solve;
            this.errorTitle = errorTitle;
            this.outputHeader = outputHeader;
        }

        private static void StyleDataGridView(DataGridView dgv, bool isNumeric = true)
        {
            Color softBlue = Color.FromArgb(240, 248, 255);

            dgv.BackgroundColor = softBlue;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(70, 130, 180),   
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Cell style
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = softBlue,
                Font = new Font("Consolas", 9f),
                Alignment = isNumeric
                    ? DataGridViewContentAlignment.MiddleRight
                    : DataGridViewContentAlignment.MiddleLeft
            };

            // Alternating row colour
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 248, 255)
            };
        }

        public void Run(LinearProgram linearProgram)
        {
            var tableau = new Tableau(linearProgram);
            LastTableau = tableau;
            TableDisplay.PopulateTableau(canonicalDgv, tableau, tableau.IterationHistory[0]);
            StyleDataGridView(canonicalDgv);
            canonicalDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            SizeGridToContentHeight(canonicalDgv);

            SimplexResult result;
            try
            {
                result = solve(tableau);
                LastResult = result;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            iterationsPanel.Controls.Clear();
            for (int i = 1; i < tableau.IterationHistory.Count; i++)
            {
                var label = new Label
                {
                    Text = $"Iteration {i}",
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                    Margin = new Padding(5, 10, 5, 3)
                };

                var dgv = new DataGridView
                {
                    Margin = new Padding(5),
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    ScrollBars = ScrollBars.None,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    EnableHeadersVisualStyles = false,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.SteelBlue,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Consolas", 9)
                    }
                };
                TableDisplay.PopulateTableau(dgv, tableau, tableau.IterationHistory[i]);
                SizeGridToContents(dgv);

                iterationsPanel.Controls.Add(label);
                iterationsPanel.Controls.Add(dgv);
            }

            if (result != null)
                PopulateOptimalSolution(result.Solution);
        }

        private static void SizeGridToContentHeight(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0) return;

            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.PerformLayout();

            int height = dgv.ColumnHeadersHeight + 2;

            foreach (DataGridViewRow row in dgv.Rows)
                height += row.Height;


            height += 4;

            dgv.Height = height;
            dgv.ScrollBars = ScrollBars.None;
            dgv.AllowUserToResizeRows = false;
        }



        private void SizeGridToContents(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.PerformLayout();

            int width = dgv.RowHeadersVisible ? dgv.RowHeadersWidth : 2;
            foreach (DataGridViewColumn column in dgv.Columns)
                width += column.Width;
            width += 20;

            int height = dgv.ColumnHeadersHeight + 2;
            foreach (DataGridViewRow row in dgv.Rows)
                height += row.Height;

            int maxWidth = Math.Max(600, iterationsPanel.ClientSize.Width - 40);
            if (width > maxWidth)
            {
                dgv.Width = maxWidth;
                dgv.ScrollBars = ScrollBars.Horizontal;
                dgv.Height = height + SystemInformation.HorizontalScrollBarHeight + 4;
            }
            else
            {
                dgv.Width = Math.Max(width, maxWidth - 50);
                dgv.ScrollBars = ScrollBars.None;
                dgv.Height = height + 4;
            }

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void PopulateOptimalSolution(Dictionary<string, double> solution)
        {


            optimalDgv.Columns.Clear();
            optimalDgv.Rows.Clear();
            optimalDgv.Columns.Add("Variable", "Variable");
            optimalDgv.Columns.Add("Value", "Value");

            foreach (var kvp in solution)
                optimalDgv.Rows.Add(kvp.Key, kvp.Value);

            StyleDataGridView(optimalDgv, isNumeric: false);

            optimalDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            optimalDgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            optimalDgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            SizeGridToContentHeight(optimalDgv);
        }

        public void SaveAllOutput()
        {
            if (LastTableau == null)
            {
                MessageBox.Show("Nothing to save. Please run the solver first.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FileName = $"{outputHeader.Replace(" ", "")}Output.txt",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var sb = new StringBuilder();
            sb.Append(TableDisplay.FormatAllIterations(LastTableau, $"---{outputHeader} – ALL ITERATIONS---"));
            sb.AppendLine();

            if (LastResult != null)
            {
                sb.AppendLine("---Optimal Tableau---");
                foreach (var kvp in LastResult.Solution)
                    sb.AppendLine($"{kvp.Key}: {kvp.Value}");
                sb.AppendLine();
            }

            File.WriteAllText(dialog.FileName, sb.ToString());
            MessageBox.Show($"Output saved to {dialog.FileName}", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

