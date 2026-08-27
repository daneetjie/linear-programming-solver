using linear_program_ui.TabControllers;
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
        private SimplexTabController primalController;
        private SimplexTabController revisedController;
        private BranchAndBoundTabController branchAndBoundTabController;
        private CuttingPlaneTabController cuttingPlaneTabController;
        private KnapSackTabController knapSackTabController;

        private SensitivityTab sensitivityTab;
        public Form1()
        {
            InitializeComponent();
            
            TabControl.ItemSize = new Size(155, 32);
            TabControl.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            TabControl.SizeMode = TabSizeMode.Normal;
            ApplyModernStyling();
            this.BackColor = Color.FromArgb(240, 248, 255);
            TabControl.BackColor = Color.FromArgb(240, 248, 255);
            foreach (TabPage page in TabControl.TabPages)
                page.BackColor = Color.FromArgb(240, 248, 255);
            primalController = new SimplexTabController(canonicalDgvPrimal, iterationsPanelPrimal, dgvOptimal, SimplexSolver.simpleSimplexSolver, "Primal Simplex Solver Error", "PRIMAL SIMPLEX");

            revisedController = new SimplexTabController(canonicalDgvPrimalRevised, iterationsPanelPrimalRevised, dgvOptimalRevised, RevisedSimplex.revisedSimplexSolver, "Revised Simplex Solver Error", "REVISED SIMPLEX");

            sensitivityTab = new SensitivityTab(cmboSensitivity, cmbTarget, txtNewData, numNewValue, dgvResults, dgvSensitivityTableau, panelSensitivityIterations, lblTarget, lblExtraInput);

            branchAndBoundTabController = new BranchAndBoundTabController(iterationsPanelBranchBound, dgvBestCandidateBranchBound);

            cuttingPlaneTabController = new CuttingPlaneTabController(iterationsPanelCuttingPlane, dgvBestCandidateCuttingPlane);

            knapSackTabController = new KnapSackTabController(iterationsPanelKnapSack, dgvBestCandidateKnapsack);
            tp_sensitivity.Parent = null;
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(70, 130, 180);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(8, 4, 8, 4);

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(100, 149, 237);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(70, 130, 180);
        }

        // You can also put the ApplyModernStyling method right under it
        private void ApplyModernStyling()
        {
            // soft background
            this.BackColor = Color.FromArgb(240, 248, 255);
            TabControl.BackColor = Color.FromArgb(240, 248, 255);

            foreach (TabPage page in TabControl.TabPages)
                page.BackColor = Color.FromArgb(240, 248, 255);

            // style every button
            StyleButton(btn_Load);
            StyleButton(btn_Solve);
            StyleButton(btnSave);
            StyleButton(btn_LoadRevised);
            StyleButton(btn_SolveRevised);
            StyleButton(btnSaveRevised);
            StyleButton(btn_LoadBranchBound);
            StyleButton(btn_SolveBranchBound);
            StyleButton(btnSaveBranchBound);
            StyleButton(btn_LoadCuttingPlane);
            StyleButton(btn_SolveCuttingPlane);
            StyleButton(btnSaveCuttingPlane);
            StyleButton(btn_LoadKnapSack);
            StyleButton(btn_SolveKnapSack);
            StyleButton(btnSaveKnapSack);
            StyleButton(btnRunSensitivity);
            StyleButton(btnSaveAnalysis);
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

        private void btn_LoadRevised_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var lines = new FileModel().ReadFile(dialog.FileName);
                var programs = LinearProgramParser.Parse(string.Join("\n", lines));
                currentProgram = programs[0];

                lblFileLoadedRevised.Text = "File loaded: " + dialog.FileName;
                lblFileLoadedRevised.Visible = true;

            }
        }

        private void btn_LoadBranchBound_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var lines = new FileModel().ReadFile(dialog.FileName);
                var programs = LinearProgramParser.Parse(string.Join("\n", lines));
                currentProgram = programs[0];

                lblFileLoadedBranchBound.Text = "File Loaded: " + dialog.FileName;
                lblFileLoadedBranchBound.Visible = true;
            }
        }

        private void btn_LoadCuttingPlane_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var lines = new FileModel().ReadFile(dialog.FileName);
                var programs = LinearProgramParser.Parse(string.Join("\n", lines));
                currentProgram = programs[0];

                lblFileLoadedCuttingPlane.Text = "File Loaded: " + dialog.FileName;
                lblFileLoadedCuttingPlane.Visible = true;
            }
        }

        private void btn_LoadKnapSack_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var lines = new FileModel().ReadFile(dialog.FileName);
                var programs = LinearProgramParser.Parse(string.Join("\n", lines));
                currentProgram = programs[0];

                lblFileLoadedKnapSack.Text = "File Loaded: " + dialog.FileName;
                lblFileLoadedKnapSack.Visible = true;
            }

        }

        private void btn_Solve_Click(object sender, EventArgs e)
        {
            if (currentProgram == null)
            {
                MessageBox.Show("Please load an input file first.");
                return;
            }


            primalController.Run(currentProgram);
            sensitivityTab.SetSolvedTableau(primalController.LastTableau, primalController.LastResult, currentProgram);
            if (!TabControl.TabPages.Contains(tp_sensitivity))
            {
                TabControl.TabPages.Add(tp_sensitivity);
            }



        }

        private void btn_SolveRevised_Click(object sender, EventArgs e)
        {
            if (currentProgram == null)
            {
                MessageBox.Show("Please load an input file first.");
                return;
            }
            revisedController.Run(currentProgram);
            sensitivityTab.SetSolvedTableau(revisedController.LastTableau, revisedController.LastResult, currentProgram);
            if (!TabControl.TabPages.Contains(tp_sensitivity))
            {
                TabControl.TabPages.Add(tp_sensitivity);
            }

        }

        private void btn_SolveBranchBound_Click(object sender, EventArgs e)
        {
            if (currentProgram == null)
            {
                MessageBox.Show("Please load an input file first.");
                return;
            }
            branchAndBoundTabController.Run(currentProgram);


        }

        private void btn_SolveCuttingPlane_Click(object sender, EventArgs e)
        {
            if (currentProgram == null)
            {
                MessageBox.Show("Please load an input file first.");
                return;
            }
            cuttingPlaneTabController.Run(currentProgram);
        }

        private void btn_SolveKnapSack_Click(object sender, EventArgs e)
        {
            if (currentProgram == null)
            {
                MessageBox.Show("Please load an input file first.");
            }
            knapSackTabController.Run(currentProgram);
        }

        private void cmboSensitivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            sensitivityTab.HandleOperationChange();

        }
        private void btnRunSensitivity_Click(object sender, EventArgs e)
        {
            sensitivityTab.RunSelectedOperation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            revisedController.SaveAllOutput();

        }

        private void btnSaveAnalysis_Click(object sender, EventArgs e)
        {
            sensitivityTab.SaveSensitivityIterationsToFile();
        }



        private void btnSaveRevised_Click(object sender, EventArgs e)
        {
            revisedController.SaveAllOutput();
        }

        private void btnSaveBranchBound_Click(object sender, EventArgs e)
        {
            branchAndBoundTabController.SaveAllOutput();
        }

        private void btnSaveCuttingPlane_Click(object sender, EventArgs e)
        {
            cuttingPlaneTabController.SaveAllOutput();
        }

        private void btnSaveKnapSack_Click(object sender, EventArgs e)
        {
            knapSackTabController.SaveAllOutput();
        }
    }
}
