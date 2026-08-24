using linear_programming_solver.UI;
using LinearProgrammingSolver.Controllers;
using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;
using System.Diagnostics;
using System.Security.Policy;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace linear_program_ui
{
    public partial class Form1 : Form
    {
        private PrimalSimplexTab primalTab;

        private SensitivityTab sensitivityTab;
        public Form1()
        {
            InitializeComponent();
            primalTab = new PrimalSimplexTab(canonicalDgvPrimal, iterationsPanelPrimal, dgvOptimal);
            sensitivityTab = new SensitivityTab(cmboSensitivity, cmbTarget, txtNewData, numNewValue, dgvResults, dgvSensitivityTableau, panelSensitivityIterations);
            tp_sensitivity.Parent = null;
        }





        private LinearProgram currentProgram;
        private void btn_Load_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var lines = new FileModel().ReadFile(dialog.FileName);
                var programs = LinearProgramParser.Parse(string.Join("\n", lines));
                currentProgram = programs[0];

                lblFileLoaded.Text = "File loaded: " + dialog.FileName;
                lblFileLoaded.Visible = true;

            }

        }


        private void btn_Solve_Click(object sender, EventArgs e)
        {
            if (currentProgram == null)
            {
                MessageBox.Show("Please load an input file first.");
                return;
            }

            switch (tabControl1.SelectedTab.Text)
            {
                case "Primal Simplex":
                    primalTab.Run(currentProgram);
                    sensitivityTab.SetSolvedTableau(primalTab.LastTableau, primalTab.LastResult, currentProgram);
                    if (!tabControl1.TabPages.Contains(tp_sensitivity))
                    {
                        tabControl1.TabPages.Add(tp_sensitivity);
                    }
                    break;
                case "Revised Primal Simplex":
                    //RunRevisedPrimalSimplex();
                    //if (!tabControl1.TabPages.Contains(tp_sensitivity))
                    //{
                    //    tabControl1.TabPages.Add(tp_sensitivity);
                    //}
                    break;

                case "Branch And Bound":
                    //RunBranchAndBound();
                    //if (!tabControl1.TabPages.Contains(tp_sensitivity))
                    //{
                    //    tabControl1.TabPages.Add(tp_sensitivity);
                    //}
                    break;
                case "Cutting Plane":
                    //RunCuttinPlane();
                    //if (!tabControl1.TabPages.Contains(tp_sensitivity))
                    //{
                    //    tabControl1.TabPages.Add(tp_sensitivity);
                    //}
                    break;
                case "Branch And Bound Knapsack":
                    //RunKnapsack();
                    //if (!tabControl1.TabPages.Contains(tp_sensitivity))
                    //{
                    //    tabControl1.TabPages.Add(tp_sensitivity);
                    //}
                    break;
                default:
                    MessageBox.Show("Please select a valid solving method.");
                    break;
            }

        }

        private void cmboSensitivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            sensitivityTab.HandleOperationChange();

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRunSensitivity_Click(object sender, EventArgs e)
        {
            sensitivityTab.RunSelectedOperation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            primalTab.SaveAllOutput();
        }

        private void btnSaveAnalysis_Click(object sender, EventArgs e)
        {
            sensitivityTab.SaveSensitivityIterationsToFile();
        }
    }
}
