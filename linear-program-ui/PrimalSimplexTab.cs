using LinearProgrammingSolver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LinearProgrammingSolver.Solvers;
using System.Windows.Forms;
using linear_programming_solver.UI;

namespace linear_program_ui
{
    public class PrimalSimplexTab
    {

        public Tableau LastTableau { get; private set; }

        public SimplexResult LastResult { get; private set; }
        private readonly DataGridView canonicalDgv;
        private readonly FlowLayoutPanel iterationsPanel;
        private readonly DataGridView optimalDgv;

        public PrimalSimplexTab(DataGridView canonicalDgv, FlowLayoutPanel iterationsPanel, DataGridView optimalDgv)
        {
            this.canonicalDgv = canonicalDgv;
            this.iterationsPanel = iterationsPanel;
            this.optimalDgv = optimalDgv;


        }

        public void Run(LinearProgram linearProgram)
        {
            var tableau = new Tableau(linearProgram);
            LastTableau=tableau;
            TableDisplay.PopulateTableau(canonicalDgv, tableau, tableau.IterationHistory[0]);

            SimplexResult result;

            try
            {
                result = SimplexSolver.simpleSimplexSolver(tableau);
                LastResult= result;
            }

            catch(InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Simplex Solver Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ScrollBars = ScrollBars.None
                };
                TableDisplay.PopulateTableau(dgv, tableau, tableau.IterationHistory[i]);
                SizeGridToContents(dgv);

                iterationsPanel.Controls.Add(label);
                iterationsPanel.Controls.Add(dgv);
            }

            if(result != null)
            PopulateOptimalSolution(result.Solution);
        }

        private void SizeGridToContents(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            int width = dgv.RowHeadersVisible ? dgv.RowHeadersWidth : 2;
            foreach (DataGridViewColumn column in dgv.Columns)
                width += column.Width;
            width += SystemInformation.VerticalScrollBarWidth;

            int height = dgv.ColumnHeadersHeight + 2;
            foreach (DataGridViewRow row in dgv.Rows)
                height += row.Height;

            int maxWidth = Math.Max(400, iterationsPanel.ClientSize.Width - 30);
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

        private void PopulateOptimalSolution(Dictionary<string, double> solution)
        {
            optimalDgv.Columns.Clear();
            optimalDgv.Rows.Clear();
            optimalDgv.Columns.Add("Variable", "Variable");
            optimalDgv.Columns.Add("Value", "Value");

            foreach (var kvp in solution)
            {
                optimalDgv.Rows.Add(kvp.Key, kvp.Value);
            }
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
                FileName = "PrimalSimplexOutput.txt",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var sb = new StringBuilder();

            //appending all iterations
            sb.Append(TableDisplay.FormatAllIterations(LastTableau, "---PRIMAL SIMPLEX – ALL ITERATIONS---"));
            sb.AppendLine();

            //final tableau

            if(LastTableau != null)
            {
                sb.AppendLine("---Optimal Tableau---");
                foreach (var kvp in LastResult.Solution)
                {
                    sb.AppendLine($"{kvp.Key}: {kvp.Value}");
                }
                sb.AppendLine();
            }

            File.WriteAllText(dialog.FileName, sb.ToString());
            MessageBox.Show($"Output saved to {dialog.FileName}", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


    }
}
