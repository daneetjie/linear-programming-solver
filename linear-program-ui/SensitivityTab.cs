using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;
using linear_programming_solver.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace linear_program_ui
{
    public class SensitivityTab
    {
        private readonly ComboBox cmboSensitivity;
        private readonly ComboBox cmbTarget;
        private readonly TextBox txtNewData;
        private readonly NumericUpDown numNewValue;
        private readonly DataGridView dgvResults;
        private readonly DataGridView dgvOptimalTableau;
        private LinearProgram program;
        private Tableau tableau;
        private SimplexResult lastResult;

        private Tableau lastSensitivityTableau;
        private readonly FlowLayoutPanel sensitivityIterationsPanel;
        public SensitivityTab(ComboBox cmboSensitivity, ComboBox cmbTarget, TextBox txtNewData, NumericUpDown numNewValue, DataGridView dgvResults, DataGridView dgvOptimalTableau, FlowLayoutPanel sensitivityIterationsPanel)
        {
            this.cmboSensitivity = cmboSensitivity;
            this.cmbTarget = cmbTarget;
            this.txtNewData = txtNewData;
            this.numNewValue = numNewValue;
            this.dgvResults = dgvResults;
            this.dgvOptimalTableau = dgvOptimalTableau;
            this.sensitivityIterationsPanel = sensitivityIterationsPanel;

            sensitivityIterationsPanel.FlowDirection = FlowDirection.TopDown;
            sensitivityIterationsPanel.WrapContents = false;
            sensitivityIterationsPanel.AutoScroll = true;
            sensitivityIterationsPanel.Padding = new Padding(5);
            sensitivityIterationsPanel.Resize += (_, _) => ResizeIterationGrids();
        }


        private void ShowSensitivityTableau(SimplexResult result, Tableau newTableau)
        {
            lastSensitivityTableau = newTableau;
            ShowSolutionInGrid(result);

            sensitivityIterationsPanel.Controls.Clear();

            for (int i = 0; i < newTableau.IterationHistory.Count; i++)
            {
                var label = new Label
                {
                    Text = $"Iteration {i + 1}",
                    AutoSize = true,
                    Margin = new Padding(5),
                    Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
                };

                var dgv = new DataGridView
                {
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    RowHeadersVisible = false,
                    ScrollBars = ScrollBars.Both,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                    Margin = new Padding(5),
                    Height = 220
                };

                TableDisplay.PopulateTableau(dgv, newTableau, newTableau.IterationHistory[i]);
                sensitivityIterationsPanel.Controls.Add(label);
                sensitivityIterationsPanel.Controls.Add(dgv);
            }

            ResizeIterationGrids();
            sensitivityIterationsPanel.PerformLayout();
        }

        public void SaveSensitivityIterationsToFile()
        {
            if(lastSensitivityTableau == null)
            {
                MessageBox.Show("No sensitivity iterations to save. Run a change first");
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FileName = "SensitivityAnalysisOutput.txt",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string operation = cmboSensitivity.Text;
            var target = cmbTarget.Text ;
            double value = double.Parse(numNewValue.Value.ToString());

            File.WriteAllText(dialog.FileName,$"{operation},{target},{value}, \n {TableDisplay.FormatAllIterations(lastSensitivityTableau)},---SENSITIVITY ANALYSIS ITERATIONS---");
            MessageBox.Show($"Saved to: \n {dialog.FileName}");
        }

        private void ResizeIterationGrids()
        {
            int width = Math.Max(100, sensitivityIterationsPanel.ClientSize.Width
                - sensitivityIterationsPanel.Padding.Horizontal
                - SystemInformation.VerticalScrollBarWidth - 10);

            foreach (Control control in sensitivityIterationsPanel.Controls)
            {
                if (control is DataGridView dgv)
                    dgv.Width = width;
            }
        }

        public void SetSolvedTableau(Tableau tableau, SimplexResult lastResult, LinearProgram program)
        {
            this.tableau = tableau;
            this.lastResult = lastResult;
            this.program = program;
            ShowOptimalTableau();
        }

        private void ShowOptimalTableau()
        {
            if (tableau == null)
                return;

            var matrix = tableau.IterationHistory.Count > 0
                ? tableau.IterationHistory[tableau.IterationHistory.Count - 1]
                : tableau.Matrix;
            TableDisplay.PopulateTableau(dgvOptimalTableau, tableau, matrix);
        }

        private List<string> GetNonBasicVariables()
        {
            var basisIndices = new HashSet<int>(lastResult.Basis);
            var nonBasic = new List<string>();
            for (int j = 0; j < tableau.NumVariables; j++)
            {
                if (!basisIndices.Contains(j))
                    nonBasic.Add(tableau.ColumnHeaders[j]);
            }
            return nonBasic;
        }

        private List<string> GetBasicVariables()
        {
            var basic = new List<string>();
            foreach (int idx in lastResult.Basis)
            {
                if (idx < tableau.NumVariables)
                    basic.Add(tableau.ColumnHeaders[idx]);
            }
            return basic;
        }

        private List<string> GetConstraintLabels()
        {
            var labels = new List<string>();
            for (int i = 1; i <= tableau.NumConstraints; i++)
                labels.Add($"C{i}");
            return labels;
        }

        private List<string> GetNonBasicColumnTargets()
        {
            var targets = new List<string>();
            foreach (string variable in GetNonBasicVariables())
            {
                foreach (string constraint in GetConstraintLabels())
                    targets.Add($"{variable} in {constraint}");
            }
            return targets;
        }

        private void PopulateTarget(List<string> items)
        {
            cmbTarget.Items.Clear();
            foreach (var item in items)
                cmbTarget.Items.Add(item);
            cmbTarget.Visible = cmbTarget.Items.Count > 0;
            if (cmbTarget.Items.Count > 0)
                cmbTarget.SelectedIndex = 0;
        }

        private bool TryGetSelectedTarget(out string target)
        {
            target = cmbTarget.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(target))
            {
                MessageBox.Show("Please select a target first.");
                return false;
            }
            return true;
        }

        private static string FormatBound(double value)
        {
            if (double.IsNegativeInfinity(value))
                return "-∞";
            if (double.IsPositiveInfinity(value))
                return "∞";
            return Math.Round(value, 3).ToString();
        }

        private void ShowRange(string labelHeader, string labelValue, double lower, double upper)
        {
            dgvResults.Columns.Clear();
            dgvResults.Rows.Clear();
            dgvResults.Columns.Add(labelHeader, labelHeader);
            dgvResults.Columns.Add("LowerBound", "Lower Bound");
            dgvResults.Columns.Add("UpperBound", "Upper Bound");
            dgvResults.Rows.Add(labelValue, FormatBound(lower), FormatBound(upper));
            dgvResults.AutoResizeColumns();
        }

        private void ShowNonBasicRange(string variableName)
        {
            int colIndex = tableau.ColumnHeaders.IndexOf(variableName);
            var bounds = SensitivityAnalysis.NonBasicRange(tableau, program, colIndex);
            ShowRange("Variable", variableName, bounds.Item1, bounds.Item2);
        }

        private void ShowBasicRange(string variableName)
        {
            int colIndex = tableau.ColumnHeaders.IndexOf(variableName);
            var bounds = SensitivityAnalysis.BasicRange(tableau, program, colIndex, lastResult.Basis);
            ShowRange("Variable", variableName, bounds.Item1, bounds.Item2);
        }

        private void ShowRhsRange(string constraintLabel)
        {
            int rowIndex = int.Parse(constraintLabel.Replace("C", "")) - 1;
            var (lower, upper) = SensitivityAnalysis.RhsRange(tableau, program, rowIndex);
            ShowRange("Constraint", constraintLabel, lower, upper);
        }

       

        private void DisplayShadowPrices()
        {
            var prices = SensitivityAnalysis.ShadowPrices(tableau);
            dgvResults.Columns.Clear();
            dgvResults.Rows.Clear();
            dgvResults.Columns.Add("Constraint", "Constraint");
            dgvResults.Columns.Add("ShadowPrice", "Shadow Price");
            foreach (var kvp in prices)
                dgvResults.Rows.Add(kvp.Key, kvp.Value);
            dgvResults.AutoResizeColumns();
        }

        private void AddNewActivity(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            if (parts.Length != tableau.NumConstraints + 1)
            {
                MessageBox.Show($"Expected {tableau.NumConstraints + 1} values (1 objective coeff + {tableau.NumConstraints} constraint coeffs).");
                return;
            }

            double objCoeff = parts[0];
            double[] constraintCoeff = parts.Skip(1).ToArray();
            var (result, tab) = SensitivityAnalysis.AddNewActivityWithTableau(program, objCoeff, constraintCoeff);
            ShowSensitivityTableau(result, tab);
        }

        private void AddNewConstraint(string input)
        {
            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var opIndex = Array.FindIndex(tokens, t => t == "<=" || t == ">=" || t == "=");
            if (opIndex == -1)
            {
                MessageBox.Show("Could not find operator (<=, >=, or =).");
                return;
            }

            var coeffs = tokens.Take(opIndex).Select(double.Parse).ToArray();
            string op = tokens[opIndex];
            double rhs = double.Parse(tokens[opIndex + 1]);
            var result = SensitivityAnalysis.AddNewConstraint(program, coeffs, op, rhs);
            ShowSolutionInGrid(result);
        }

        private bool TryParseColumnTarget(string target, out string variableName, out int constraintRow)
        {
            variableName = null;
            constraintRow = 0;
            var parts = target.Split(" in ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                MessageBox.Show("Select a target in the form x1 in C1.");
                return false;
            }

            variableName = parts[0].Trim();
            constraintRow = int.Parse(parts[1].Trim().Replace("C", "")) - 1;
            return true;
        }

        private void ShowNonBasicColumn(string target)
        {
            if (!TryParseColumnTarget(target, out string variableName, out int constraintRow))
                return;

            int colIndex = tableau.ColumnHeaders.IndexOf(variableName);
            var (lower, upper) = SensitivityAnalysis.NonBasicColumnRange(tableau, colIndex, constraintRow);
            ShowRange("Entry", target, lower, upper);
        }

    

        private void ShowSolutionInGrid(SimplexResult result)
        {
            dgvResults.Columns.Clear();
            dgvResults.Rows.Clear();
            dgvResults.Columns.Add("Variable", "Variable");
            dgvResults.Columns.Add("Value", "Value");
            foreach (var kvp in result.Solution)
                dgvResults.Rows.Add(kvp.Key, kvp.Value);
            dgvResults.AutoResizeColumns();
        }

        private void ShowTextResult(string title, string body)
        {
            dgvResults.Columns.Clear();
            dgvResults.Rows.Clear();
            dgvResults.Columns.Add("Result", title);
            foreach (var line in body.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                dgvResults.Rows.Add(line.TrimEnd('\r'));
            dgvResults.AutoResizeColumns();
        }

        private void ApplyDuality()
        {
            var dual = SensitivityAnalysis.BuildDual(program);
            ShowTextResult("Dual model", SensitivityAnalysis.FormatProgram(dual));
        }

        private void SolveDualModel()
        {
            var result = SensitivityAnalysis.SolveDual(program);
            ShowSolutionInGrid(result);
        }

        private void VerifyDual()
        {
            ShowTextResult("Duality", SensitivityAnalysis.VerifyDuality(program, lastResult));
        }

        public void RunSelectedOperation()
        {
            if (tableau == null || lastResult == null || program == null)
            {
                MessageBox.Show("Please solve a linear programming problem first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string op = cmboSensitivity.SelectedItem?.ToString();
            if (op == null)
                return;

            try
            {
                switch (op)
                {
                    case "Range of selected non-basic variable":
                        if (TryGetSelectedTarget(out string nbVar))
                            ShowNonBasicRange(nbVar);
                        break;
                    case "Change selected non-basic variable":
                        if (TryGetSelectedTarget(out string nbChange))
                        {
                            int colIndex = tableau.ColumnHeaders.IndexOf(nbChange);
                            var (result, tab) = SensitivityAnalysis.ApplyObjectiveCoeffChangeWithTableau(program, colIndex, (double)numNewValue.Value);
                            ShowSensitivityTableau(result, tab);
                        }
                        break;
                    case "Range of selected basic variable":
                        if (TryGetSelectedTarget(out string bVar))
                            ShowBasicRange(bVar);
                        break;
                    case "Change selected basic variable":
                        if (TryGetSelectedTarget(out string bChange))
                        {
                            int colIndex = tableau.ColumnHeaders.IndexOf(bChange);
                            var (result, tab) = SensitivityAnalysis.ApplyObjectiveCoeffChangeWithTableau(program, colIndex, (double)numNewValue.Value);
                            ShowSensitivityTableau(result, tab);
                        }
                        break;
                    case "Range of selected constraint rhs value":
                        if (TryGetSelectedTarget(out string rhsRange))
                            ShowRhsRange(rhsRange);
                        break;
                    case "Change selected constraint rhs value":
                        if (TryGetSelectedTarget(out string rhsChange))
                        {
                            int rowIndex = int.Parse(rhsChange.Replace("C", "")) - 1;
                            var (result, tab) = SensitivityAnalysis.ApplyRhsChangeWithTableau(program, rowIndex, (double)numNewValue.Value);
                            ShowSensitivityTableau(result, tab);
                        }
                            
                        break;
                    case "Range of selected variable in a non-basic variable column":
                        if (TryGetSelectedTarget(out string colRange))
                            ShowNonBasicColumn(colRange);
                        break;
                    case "Change selected variable in a non-basic variable column":
                        if (TryGetSelectedTarget(out string colChange))
                        {
                            if (TryParseColumnTarget(colChange, out string variableName, out int constraintRow))
                            {
                                int colIndex = tableau.ColumnHeaders.IndexOf(variableName);
                                var (result, tab) = SensitivityAnalysis.ApplyNonBasicColumnChangeWithTableau(program, colIndex, constraintRow, (double)numNewValue.Value);
                                ShowSensitivityTableau(result, tab);
                            }
                        }
                        break;
                    case "Add new activity to optimal solution":
                        AddNewActivity(txtNewData.Text);

                        break;
                    case "Add new constraint to optimal solution":
                        AddNewConstraint(txtNewData.Text);
                        break;
                    case "Display shadow prices":
                        DisplayShadowPrices();
                        break;
                    case "Apply duality":
                        ApplyDuality();
                        break;
                    case "Solve Dual Programming model":
                        SolveDualModel();
                        break;
                    case "Verify whether programming model has strong/weak duality":
                        VerifyDual();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sensitivity analysis error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void HandleOperationChange()
        {
            cmbTarget.Items.Clear();
            cmbTarget.Visible = false;
            txtNewData.Visible = false;
            numNewValue.Visible = false;

            if (tableau == null || lastResult == null)
                return;

            string op = cmboSensitivity.SelectedItem?.ToString();
            if (op == null)
                return;

            switch (op)
            {
                case "Range of selected non-basic variable":
                case "Change selected non-basic variable":
                    PopulateTarget(GetNonBasicVariables());
                    numNewValue.Visible = op == "Change selected non-basic variable";
                    break;
                case "Range of selected basic variable":
                case "Change selected basic variable":
                    PopulateTarget(GetBasicVariables());
                    numNewValue.Visible = op == "Change selected basic variable";
                    break;
                case "Range of selected constraint rhs value":
                case "Change selected constraint rhs value":
                    PopulateTarget(GetConstraintLabels());
                    numNewValue.Visible = op == "Change selected constraint rhs value";
                    break;
                case "Range of selected variable in a non-basic variable column":
                case "Change selected variable in a non-basic variable column":
                    PopulateTarget(GetNonBasicColumnTargets());
                    numNewValue.Visible = op == "Change selected variable in a non-basic variable column";
                    break;
                case "Add new activity to optimal solution":
                    txtNewData.Visible = true;
                    txtNewData.PlaceholderText = $"Objective coeff, then {tableau.NumConstraints} constraint coeffs — e.g. 5 2 1 3";
                    break;
                case "Add new constraint to optimal solution":
                    txtNewData.Visible = true;
                    txtNewData.PlaceholderText = $"coeffs then operator and RHS — e.g. 1 2 <= 10";
                    break;
            }
        }
    }
}
