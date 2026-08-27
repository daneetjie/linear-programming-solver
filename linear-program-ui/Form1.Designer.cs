namespace linear_program_ui
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tp_branchBoundKnap = new TabPage();
            btnSaveKnapSack = new Button();
            label8 = new Label();
            btn_SolveKnapSack = new Button();
            lblFileLoadedKnapSack = new Label();
            btn_LoadKnapSack = new Button();
            dgvBestCandidateKnapsack = new DataGridView();
            iterationsPanelKnapSack = new FlowLayoutPanel();
            tp_cuttingPlane = new TabPage();
            btnSaveCuttingPlane = new Button();
            label7 = new Label();
            btn_SolveCuttingPlane = new Button();
            lblFileLoadedCuttingPlane = new Label();
            btn_LoadCuttingPlane = new Button();
            dgvBestCandidateCuttingPlane = new DataGridView();
            iterationsPanelCuttingPlane = new FlowLayoutPanel();
            tp_branchBound = new TabPage();
            btnSaveBranchBound = new Button();
            label5 = new Label();
            btn_SolveBranchBound = new Button();
            lblFileLoadedBranchBound = new Label();
            btn_LoadBranchBound = new Button();
            dgvBestCandidateBranchBound = new DataGridView();
            iterationsPanelBranchBound = new FlowLayoutPanel();
            tp_revisedPrimal = new TabPage();
            btnSaveRevised = new Button();
            label4 = new Label();
            btn_SolveRevised = new Button();
            lblFileLoadedRevised = new Label();
            btn_LoadRevised = new Button();
            dgvOptimalRevised = new DataGridView();
            label6 = new Label();
            iterationsPanelPrimalRevised = new FlowLayoutPanel();
            canonicalDgvPrimalRevised = new DataGridView();
            tp_primalSimplex = new TabPage();
            btnSave = new Button();
            label3 = new Label();
            btn_Solve = new Button();
            lblFileLoaded = new Label();
            btn_Load = new Button();
            dgvOptimal = new DataGridView();
            label1 = new Label();
            iterationsPanelPrimal = new FlowLayoutPanel();
            canonicalDgvPrimal = new DataGridView();
            TabControl = new TabControl();
            tp_sensitivity = new TabPage();
            panelSensitivityIterations = new FlowLayoutPanel();
            lblIterationHistory = new Label();
            btnSaveAnalysis = new Button();
            lblOptimalTableau = new Label();
            dgvSensitivityTableau = new DataGridView();
            btnRunSensitivity = new Button();
            lblResults = new Label();
            dgvResults = new DataGridView();
            numNewValue = new NumericUpDown();
            txtNewData = new TextBox();
            lblExtraInput = new Label();
            cmbTarget = new ComboBox();
            lblTarget = new Label();
            cmboSensitivity = new ComboBox();
            lblOperation = new Label();
            tp_branchBoundKnap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBestCandidateKnapsack).BeginInit();
            tp_cuttingPlane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBestCandidateCuttingPlane).BeginInit();
            tp_branchBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBestCandidateBranchBound).BeginInit();
            tp_revisedPrimal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOptimalRevised).BeginInit();
            ((System.ComponentModel.ISupportInitialize)canonicalDgvPrimalRevised).BeginInit();
            tp_primalSimplex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOptimal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)canonicalDgvPrimal).BeginInit();
            TabControl.SuspendLayout();
            tp_sensitivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensitivityTableau).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNewValue).BeginInit();
            SuspendLayout();
            // 
            // tp_branchBoundKnap
            // 
            tp_branchBoundKnap.AccessibleName = "tp_branchBoundKnap";
            tp_branchBoundKnap.Controls.Add(btnSaveKnapSack);
            tp_branchBoundKnap.Controls.Add(label8);
            tp_branchBoundKnap.Controls.Add(btn_SolveKnapSack);
            tp_branchBoundKnap.Controls.Add(lblFileLoadedKnapSack);
            tp_branchBoundKnap.Controls.Add(btn_LoadKnapSack);
            tp_branchBoundKnap.Controls.Add(dgvBestCandidateKnapsack);
            tp_branchBoundKnap.Controls.Add(iterationsPanelKnapSack);
            tp_branchBoundKnap.Location = new Point(4, 24);
            tp_branchBoundKnap.Name = "tp_branchBoundKnap";
            tp_branchBoundKnap.Padding = new Padding(3);
            tp_branchBoundKnap.Size = new Size(1491, 809);
            tp_branchBoundKnap.TabIndex = 4;
            tp_branchBoundKnap.Text = "Branch And Bound Knapsack";
            tp_branchBoundKnap.UseVisualStyleBackColor = true;
            // 
            // btnSaveKnapSack
            // 
            btnSaveKnapSack.AccessibleName = "btnSaveKnapSack";
            btnSaveKnapSack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveKnapSack.BackColor = SystemColors.ActiveCaption;
            btnSaveKnapSack.Cursor = Cursors.AppStarting;
            btnSaveKnapSack.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveKnapSack.Location = new Point(429, 769);
            btnSaveKnapSack.Name = "btnSaveKnapSack";
            btnSaveKnapSack.Size = new Size(137, 33);
            btnSaveKnapSack.TabIndex = 41;
            btnSaveKnapSack.Text = "Save To TextFile";
            btnSaveKnapSack.UseVisualStyleBackColor = false;
            btnSaveKnapSack.Click += btnSaveKnapSack_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new Point(6, 16);
            label8.Name = "label8";
            label8.Size = new Size(90, 15);
            label8.TabIndex = 40;
            label8.Text = "Best Candidate";
            label8.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_SolveKnapSack
            // 
            btn_SolveKnapSack.AccessibleName = "btn_SolveKnapSack";
            btn_SolveKnapSack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_SolveKnapSack.BackColor = SystemColors.ActiveCaption;
            btn_SolveKnapSack.Cursor = Cursors.AppStarting;
            btn_SolveKnapSack.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_SolveKnapSack.Location = new Point(9, 769);
            btn_SolveKnapSack.Name = "btn_SolveKnapSack";
            btn_SolveKnapSack.Size = new Size(80, 33);
            btn_SolveKnapSack.TabIndex = 37;
            btn_SolveKnapSack.Text = "Solve";
            btn_SolveKnapSack.UseVisualStyleBackColor = false;
            btn_SolveKnapSack.Click += btn_SolveKnapSack_Click;
            // 
            // lblFileLoadedKnapSack
            // 
            lblFileLoadedKnapSack.AccessibleName = "lblFileLoadedKnapSack";
            lblFileLoadedKnapSack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileLoadedKnapSack.AutoSize = true;
            lblFileLoadedKnapSack.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold | FontStyle.Italic);
            lblFileLoadedKnapSack.Location = new Point(9, 749);
            lblFileLoadedKnapSack.Name = "lblFileLoadedKnapSack";
            lblFileLoadedKnapSack.Size = new Size(36, 13);
            lblFileLoadedKnapSack.TabIndex = 38;
            lblFileLoadedKnapSack.Text = "label1";
            lblFileLoadedKnapSack.Visible = false;
            lblFileLoadedKnapSack.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblFileLoadedKnapSack.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_LoadKnapSack
            // 
            btn_LoadKnapSack.AccessibleName = "btn_LoadKnapSack";
            btn_LoadKnapSack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_LoadKnapSack.BackColor = SystemColors.ActiveCaption;
            btn_LoadKnapSack.Cursor = Cursors.AppStarting;
            btn_LoadKnapSack.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_LoadKnapSack.Location = new Point(9, 713);
            btn_LoadKnapSack.Name = "btn_LoadKnapSack";
            btn_LoadKnapSack.Size = new Size(80, 33);
            btn_LoadKnapSack.TabIndex = 39;
            btn_LoadKnapSack.Text = "Load File";
            btn_LoadKnapSack.UseVisualStyleBackColor = false;
            btn_LoadKnapSack.Click += btn_LoadKnapSack_Click;
            // 
            // dgvBestCandidateKnapsack
            // 
            dgvBestCandidateKnapsack.AccessibleName = "dgvBestCandidateKnapsack";
            dgvBestCandidateKnapsack.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvBestCandidateKnapsack.BackgroundColor = SystemColors.ActiveCaption;
            dgvBestCandidateKnapsack.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBestCandidateKnapsack.Location = new Point(8, 34);
            dgvBestCandidateKnapsack.Name = "dgvBestCandidateKnapsack";
            dgvBestCandidateKnapsack.Size = new Size(557, 663);
            dgvBestCandidateKnapsack.TabIndex = 36;
            // 
            // iterationsPanelKnapSack
            // 
            iterationsPanelKnapSack.AccessibleName = "iterationsPanelKnapSack";
            iterationsPanelKnapSack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            iterationsPanelKnapSack.AutoScroll = true;
            iterationsPanelKnapSack.BackColor = Color.FromArgb(240, 248, 255);
            iterationsPanelKnapSack.FlowDirection = FlowDirection.TopDown;
            iterationsPanelKnapSack.Location = new Point(585, 7);
            iterationsPanelKnapSack.Name = "iterationsPanelKnapSack";
            iterationsPanelKnapSack.Padding = new Padding(10);
            iterationsPanelKnapSack.Size = new Size(899, 783);
            iterationsPanelKnapSack.TabIndex = 35;
            iterationsPanelKnapSack.WrapContents = false;
            // 
            // tp_cuttingPlane
            // 
            tp_cuttingPlane.AccessibleName = "tp_cuttingPlane";
            tp_cuttingPlane.Controls.Add(btnSaveCuttingPlane);
            tp_cuttingPlane.Controls.Add(label7);
            tp_cuttingPlane.Controls.Add(btn_SolveCuttingPlane);
            tp_cuttingPlane.Controls.Add(lblFileLoadedCuttingPlane);
            tp_cuttingPlane.Controls.Add(btn_LoadCuttingPlane);
            tp_cuttingPlane.Controls.Add(dgvBestCandidateCuttingPlane);
            tp_cuttingPlane.Controls.Add(iterationsPanelCuttingPlane);
            tp_cuttingPlane.Location = new Point(4, 24);
            tp_cuttingPlane.Name = "tp_cuttingPlane";
            tp_cuttingPlane.Padding = new Padding(3);
            tp_cuttingPlane.Size = new Size(1491, 809);
            tp_cuttingPlane.TabIndex = 3;
            tp_cuttingPlane.Text = "Cutting Plane";
            tp_cuttingPlane.UseVisualStyleBackColor = true;
            // 
            // btnSaveCuttingPlane
            // 
            btnSaveCuttingPlane.AccessibleName = "btnSaveCuttingPlane";
            btnSaveCuttingPlane.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveCuttingPlane.BackColor = SystemColors.ActiveCaption;
            btnSaveCuttingPlane.Cursor = Cursors.AppStarting;
            btnSaveCuttingPlane.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveCuttingPlane.Location = new Point(429, 769);
            btnSaveCuttingPlane.Name = "btnSaveCuttingPlane";
            btnSaveCuttingPlane.Size = new Size(137, 33);
            btnSaveCuttingPlane.TabIndex = 34;
            btnSaveCuttingPlane.Text = "Save To TextFile";
            btnSaveCuttingPlane.UseVisualStyleBackColor = false;
            btnSaveCuttingPlane.Click += btnSaveCuttingPlane_Click;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(6, 16);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 33;
            label7.Text = "Best Candidate";
            label7.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_SolveCuttingPlane
            // 
            btn_SolveCuttingPlane.AccessibleName = "btn_SolveCuttingPlane";
            btn_SolveCuttingPlane.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_SolveCuttingPlane.BackColor = SystemColors.ActiveCaption;
            btn_SolveCuttingPlane.Cursor = Cursors.AppStarting;
            btn_SolveCuttingPlane.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_SolveCuttingPlane.Location = new Point(9, 769);
            btn_SolveCuttingPlane.Name = "btn_SolveCuttingPlane";
            btn_SolveCuttingPlane.Size = new Size(80, 33);
            btn_SolveCuttingPlane.TabIndex = 30;
            btn_SolveCuttingPlane.Text = "Solve";
            btn_SolveCuttingPlane.UseVisualStyleBackColor = false;
            btn_SolveCuttingPlane.Click += btn_SolveCuttingPlane_Click;
            // 
            // lblFileLoadedCuttingPlane
            // 
            lblFileLoadedCuttingPlane.AccessibleName = "lblFileLoadedCuttingPlane";
            lblFileLoadedCuttingPlane.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileLoadedCuttingPlane.AutoSize = true;
            lblFileLoadedCuttingPlane.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold | FontStyle.Italic);
            lblFileLoadedCuttingPlane.Location = new Point(9, 749);
            lblFileLoadedCuttingPlane.Name = "lblFileLoadedCuttingPlane";
            lblFileLoadedCuttingPlane.Size = new Size(36, 13);
            lblFileLoadedCuttingPlane.TabIndex = 31;
            lblFileLoadedCuttingPlane.Text = "label1";
            lblFileLoadedCuttingPlane.Visible = false;
            lblFileLoadedCuttingPlane.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblFileLoadedCuttingPlane.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_LoadCuttingPlane
            // 
            btn_LoadCuttingPlane.AccessibleName = "btn_LoadCuttingPlane";
            btn_LoadCuttingPlane.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_LoadCuttingPlane.BackColor = SystemColors.ActiveCaption;
            btn_LoadCuttingPlane.Cursor = Cursors.AppStarting;
            btn_LoadCuttingPlane.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_LoadCuttingPlane.Location = new Point(9, 713);
            btn_LoadCuttingPlane.Name = "btn_LoadCuttingPlane";
            btn_LoadCuttingPlane.Size = new Size(80, 33);
            btn_LoadCuttingPlane.TabIndex = 32;
            btn_LoadCuttingPlane.Text = "Load File";
            btn_LoadCuttingPlane.UseVisualStyleBackColor = false;
            btn_LoadCuttingPlane.Click += btn_LoadCuttingPlane_Click;
            // 
            // dgvBestCandidateCuttingPlane
            // 
            dgvBestCandidateCuttingPlane.AccessibleName = "dgvBestCandidateCuttingPlane";
            dgvBestCandidateCuttingPlane.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvBestCandidateCuttingPlane.BackgroundColor = SystemColors.ActiveCaption;
            dgvBestCandidateCuttingPlane.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBestCandidateCuttingPlane.Location = new Point(8, 34);
            dgvBestCandidateCuttingPlane.Name = "dgvBestCandidateCuttingPlane";
            dgvBestCandidateCuttingPlane.Size = new Size(557, 663);
            dgvBestCandidateCuttingPlane.TabIndex = 29;
            // 
            // iterationsPanelCuttingPlane
            // 
            iterationsPanelCuttingPlane.AccessibleName = "iterationsPanelCuttingPlane";
            iterationsPanelCuttingPlane.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            iterationsPanelCuttingPlane.AutoScroll = true;
            iterationsPanelCuttingPlane.BackColor = Color.FromArgb(240, 248, 255);
            iterationsPanelCuttingPlane.FlowDirection = FlowDirection.TopDown;
            iterationsPanelCuttingPlane.Location = new Point(585, 7);
            iterationsPanelCuttingPlane.Name = "iterationsPanelCuttingPlane";
            iterationsPanelCuttingPlane.Padding = new Padding(10);
            iterationsPanelCuttingPlane.Size = new Size(899, 783);
            iterationsPanelCuttingPlane.TabIndex = 28;
            iterationsPanelCuttingPlane.WrapContents = false;
            // 
            // tp_branchBound
            // 
            tp_branchBound.AccessibleName = "tp_branchBound";
            tp_branchBound.Controls.Add(btnSaveBranchBound);
            tp_branchBound.Controls.Add(label5);
            tp_branchBound.Controls.Add(btn_SolveBranchBound);
            tp_branchBound.Controls.Add(lblFileLoadedBranchBound);
            tp_branchBound.Controls.Add(btn_LoadBranchBound);
            tp_branchBound.Controls.Add(dgvBestCandidateBranchBound);
            tp_branchBound.Controls.Add(iterationsPanelBranchBound);
            tp_branchBound.Location = new Point(4, 24);
            tp_branchBound.Name = "tp_branchBound";
            tp_branchBound.Padding = new Padding(3);
            tp_branchBound.RightToLeft = RightToLeft.No;
            tp_branchBound.Size = new Size(1491, 809);
            tp_branchBound.TabIndex = 2;
            tp_branchBound.Text = "Branch And Bound";
            tp_branchBound.UseVisualStyleBackColor = true;
            // 
            // btnSaveBranchBound
            // 
            btnSaveBranchBound.AccessibleName = "btnSaveBranchBound";
            btnSaveBranchBound.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveBranchBound.BackColor = SystemColors.ActiveCaption;
            btnSaveBranchBound.Cursor = Cursors.AppStarting;
            btnSaveBranchBound.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveBranchBound.Location = new Point(429, 769);
            btnSaveBranchBound.Name = "btnSaveBranchBound";
            btnSaveBranchBound.Size = new Size(137, 33);
            btnSaveBranchBound.TabIndex = 27;
            btnSaveBranchBound.Text = "Save To TextFile";
            btnSaveBranchBound.UseVisualStyleBackColor = false;
            btnSaveBranchBound.Click += btnSaveBranchBound_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(6, 16);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 26;
            label5.Text = "Best Candidate";
            label5.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_SolveBranchBound
            // 
            btn_SolveBranchBound.AccessibleName = "btn_SolveBranchBound";
            btn_SolveBranchBound.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_SolveBranchBound.BackColor = SystemColors.ActiveCaption;
            btn_SolveBranchBound.Cursor = Cursors.AppStarting;
            btn_SolveBranchBound.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_SolveBranchBound.Location = new Point(9, 769);
            btn_SolveBranchBound.Name = "btn_SolveBranchBound";
            btn_SolveBranchBound.Size = new Size(80, 33);
            btn_SolveBranchBound.TabIndex = 23;
            btn_SolveBranchBound.Text = "Solve";
            btn_SolveBranchBound.UseVisualStyleBackColor = false;
            btn_SolveBranchBound.Click += btn_SolveBranchBound_Click;
            // 
            // lblFileLoadedBranchBound
            // 
            lblFileLoadedBranchBound.AccessibleName = "lblFileLoadedBranchBound";
            lblFileLoadedBranchBound.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileLoadedBranchBound.AutoSize = true;
            lblFileLoadedBranchBound.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold | FontStyle.Italic);
            lblFileLoadedBranchBound.Location = new Point(9, 749);
            lblFileLoadedBranchBound.Name = "lblFileLoadedBranchBound";
            lblFileLoadedBranchBound.Size = new Size(36, 13);
            lblFileLoadedBranchBound.TabIndex = 24;
            lblFileLoadedBranchBound.Text = "label1";
            lblFileLoadedBranchBound.Visible = false;
            lblFileLoadedBranchBound.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblFileLoadedBranchBound.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_LoadBranchBound
            // 
            btn_LoadBranchBound.AccessibleName = "btn_LoadBranchBound";
            btn_LoadBranchBound.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_LoadBranchBound.BackColor = SystemColors.ActiveCaption;
            btn_LoadBranchBound.Cursor = Cursors.AppStarting;
            btn_LoadBranchBound.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_LoadBranchBound.Location = new Point(9, 713);
            btn_LoadBranchBound.Name = "btn_LoadBranchBound";
            btn_LoadBranchBound.Size = new Size(80, 33);
            btn_LoadBranchBound.TabIndex = 25;
            btn_LoadBranchBound.Text = "Load File";
            btn_LoadBranchBound.UseVisualStyleBackColor = false;
            btn_LoadBranchBound.Click += btn_LoadBranchBound_Click;
            // 
            // dgvBestCandidateBranchBound
            // 
            dgvBestCandidateBranchBound.AccessibleName = "dgvBestCandidateBranchBound";
            dgvBestCandidateBranchBound.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvBestCandidateBranchBound.BackgroundColor = SystemColors.ActiveCaption;
            dgvBestCandidateBranchBound.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBestCandidateBranchBound.Location = new Point(8, 34);
            dgvBestCandidateBranchBound.Name = "dgvBestCandidateBranchBound";
            dgvBestCandidateBranchBound.Size = new Size(557, 663);
            dgvBestCandidateBranchBound.TabIndex = 22;
            // 
            // iterationsPanelBranchBound
            // 
            iterationsPanelBranchBound.AccessibleName = "iterationsPanelBranchBound";
            iterationsPanelBranchBound.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            iterationsPanelBranchBound.AutoScroll = true;
            iterationsPanelBranchBound.BackColor = Color.FromArgb(240, 248, 255);
            iterationsPanelBranchBound.FlowDirection = FlowDirection.TopDown;
            iterationsPanelBranchBound.Location = new Point(585, 7);
            iterationsPanelBranchBound.Name = "iterationsPanelBranchBound";
            iterationsPanelBranchBound.Size = new Size(899, 783);
            iterationsPanelBranchBound.TabIndex = 20;
            iterationsPanelBranchBound.WrapContents = false;
            iterationsPanelBranchBound.Padding = new Padding(10);
            // 
            // tp_revisedPrimal
            // 
            tp_revisedPrimal.AccessibleName = "tp_revisedPrimal";
            tp_revisedPrimal.BackColor = Color.AliceBlue;
            tp_revisedPrimal.Controls.Add(btnSaveRevised);
            tp_revisedPrimal.Controls.Add(label4);
            tp_revisedPrimal.Controls.Add(btn_SolveRevised);
            tp_revisedPrimal.Controls.Add(lblFileLoadedRevised);
            tp_revisedPrimal.Controls.Add(btn_LoadRevised);
            tp_revisedPrimal.Controls.Add(dgvOptimalRevised);
            tp_revisedPrimal.Controls.Add(label6);
            tp_revisedPrimal.Controls.Add(iterationsPanelPrimalRevised);
            tp_revisedPrimal.Controls.Add(canonicalDgvPrimalRevised);
            tp_revisedPrimal.Location = new Point(4, 24);
            tp_revisedPrimal.Name = "tp_revisedPrimal";
            tp_revisedPrimal.Padding = new Padding(3);
            tp_revisedPrimal.Size = new Size(1491, 809);
            tp_revisedPrimal.TabIndex = 1;
            tp_revisedPrimal.Text = "Revised Primal Simplex";
            // 
            // btnSaveRevised
            // 
            btnSaveRevised.AccessibleName = "btnSaveRevised";
            btnSaveRevised.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveRevised.BackColor = SystemColors.ActiveCaption;
            btnSaveRevised.Cursor = Cursors.AppStarting;
            btnSaveRevised.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveRevised.Location = new Point(429, 769);
            btnSaveRevised.Name = "btnSaveRevised";
            btnSaveRevised.Size = new Size(137, 33);
            btnSaveRevised.TabIndex = 18;
            btnSaveRevised.Text = "Save To TextFile";
            btnSaveRevised.UseVisualStyleBackColor = false;
            btnSaveRevised.Click += btnSaveRevised_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(7, 528);
            label4.Name = "label4";
            label4.Size = new Size(73, 15);
            label4.TabIndex = 17;
            label4.Text = "Final Values:";
            label4.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_SolveRevised
            // 
            btn_SolveRevised.AccessibleName = "btn_SolveRevised";
            btn_SolveRevised.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_SolveRevised.BackColor = SystemColors.ActiveCaption;
            btn_SolveRevised.Cursor = Cursors.AppStarting;
            btn_SolveRevised.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_SolveRevised.Location = new Point(9, 769);
            btn_SolveRevised.Name = "btn_SolveRevised";
            btn_SolveRevised.Size = new Size(80, 33);
            btn_SolveRevised.TabIndex = 14;
            btn_SolveRevised.Text = "Solve";
            btn_SolveRevised.UseVisualStyleBackColor = false;
            btn_SolveRevised.Click += btn_SolveRevised_Click;
            // 
            // lblFileLoadedRevised
            // 
            lblFileLoadedRevised.AccessibleName = "lblFileLoadedRevised";
            lblFileLoadedRevised.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileLoadedRevised.AutoSize = true;
            lblFileLoadedRevised.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold | FontStyle.Italic);
            lblFileLoadedRevised.Location = new Point(9, 749);
            lblFileLoadedRevised.Name = "lblFileLoadedRevised";
            lblFileLoadedRevised.Size = new Size(36, 13);
            lblFileLoadedRevised.TabIndex = 15;
            lblFileLoadedRevised.Text = "label1";
            lblFileLoadedRevised.Visible = false;
            lblFileLoadedRevised.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblFileLoadedRevised.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_LoadRevised
            // 
            btn_LoadRevised.AccessibleName = "btn_LoadRevised";
            btn_LoadRevised.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_LoadRevised.BackColor = SystemColors.ActiveCaption;
            btn_LoadRevised.Cursor = Cursors.AppStarting;
            btn_LoadRevised.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_LoadRevised.Location = new Point(9, 713);
            btn_LoadRevised.Name = "btn_LoadRevised";
            btn_LoadRevised.Size = new Size(80, 33);
            btn_LoadRevised.TabIndex = 16;
            btn_LoadRevised.Text = "Load File";
            btn_LoadRevised.UseVisualStyleBackColor = false;
            btn_LoadRevised.Click += btn_LoadRevised_Click;
            // 
            // dgvOptimalRevised
            // 
            dgvOptimalRevised.AccessibleName = "dgvOptimalRevised";
            dgvOptimalRevised.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvOptimalRevised.BackgroundColor = Color.White;
            dgvOptimalRevised.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptimalRevised.Location = new Point(9, 546);
            dgvOptimalRevised.Name = "dgvOptimalRevised";
            dgvOptimalRevised.Size = new Size(557, 161);
            dgvOptimalRevised.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 7);
            label6.Name = "label6";
            label6.Size = new Size(94, 15);
            label6.TabIndex = 12;
            label6.Text = "Canonical Form:";
            label6.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // iterationsPanelPrimalRevised
            // 
            iterationsPanelPrimalRevised.AccessibleName = "iterationsPanelPrimalRevised";
            iterationsPanelPrimalRevised.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            iterationsPanelPrimalRevised.AutoScroll = true;
            iterationsPanelPrimalRevised.BackColor = Color.FromArgb(240, 248, 255);
            iterationsPanelPrimalRevised.FlowDirection = FlowDirection.TopDown;
            iterationsPanelPrimalRevised.Location = new Point(585, 7);
            iterationsPanelPrimalRevised.Name = "iterationsPanelPrimalRevised";
            iterationsPanelPrimalRevised.Padding = new Padding(10);
            iterationsPanelPrimalRevised.Size = new Size(899, 795);
            iterationsPanelPrimalRevised.TabIndex = 11;
            iterationsPanelPrimalRevised.WrapContents = false;
            // 
            // canonicalDgvPrimalRevised
            // 
            canonicalDgvPrimalRevised.AccessibleName = "canonicalDgvPrimalRevised";
            canonicalDgvPrimalRevised.BackgroundColor = Color.White;
            canonicalDgvPrimalRevised.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            canonicalDgvPrimalRevised.Location = new Point(9, 33);
            canonicalDgvPrimalRevised.Name = "canonicalDgvPrimalRevised";
            canonicalDgvPrimalRevised.Size = new Size(557, 468);
            canonicalDgvPrimalRevised.TabIndex = 10;
            // 
            // tp_primalSimplex
            // 
            tp_primalSimplex.AccessibleName = "tp_primalSimplex";
            tp_primalSimplex.BackColor = Color.AliceBlue;
            tp_primalSimplex.Controls.Add(btnSave);
            tp_primalSimplex.Controls.Add(label3);
            tp_primalSimplex.Controls.Add(btn_Solve);
            tp_primalSimplex.Controls.Add(lblFileLoaded);
            tp_primalSimplex.Controls.Add(btn_Load);
            tp_primalSimplex.Controls.Add(dgvOptimal);
            tp_primalSimplex.Controls.Add(label1);
            tp_primalSimplex.Controls.Add(iterationsPanelPrimal);
            tp_primalSimplex.Controls.Add(canonicalDgvPrimal);
            tp_primalSimplex.Location = new Point(4, 24);
            tp_primalSimplex.Name = "tp_primalSimplex";
            tp_primalSimplex.Padding = new Padding(3);
            tp_primalSimplex.Size = new Size(1491, 809);
            tp_primalSimplex.TabIndex = 0;
            tp_primalSimplex.Text = "Primal Simplex";
            // 
            // btnSave
            // 
            btnSave.AccessibleName = "btnSave";
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.BackColor = SystemColors.ActiveCaption;
            btnSave.Cursor = Cursors.AppStarting;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(428, 768);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(137, 33);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save To TextFile";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(6, 527);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 7;
            label3.Text = "Final Values:";
            label3.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_Solve
            // 
            btn_Solve.AccessibleName = "btn_Solve";
            btn_Solve.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_Solve.BackColor = SystemColors.ActiveCaption;
            btn_Solve.Cursor = Cursors.AppStarting;
            btn_Solve.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Solve.Location = new Point(8, 768);
            btn_Solve.Name = "btn_Solve";
            btn_Solve.Size = new Size(80, 33);
            btn_Solve.TabIndex = 4;
            btn_Solve.Text = "Solve";
            btn_Solve.UseVisualStyleBackColor = false;
            btn_Solve.Click += btn_Solve_Click;
            // 
            // lblFileLoaded
            // 
            lblFileLoaded.AccessibleName = "lblFileLoaded";
            lblFileLoaded.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileLoaded.AutoSize = true;
            lblFileLoaded.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold | FontStyle.Italic);
            lblFileLoaded.Location = new Point(8, 748);
            lblFileLoaded.Name = "lblFileLoaded";
            lblFileLoaded.Size = new Size(36, 13);
            lblFileLoaded.TabIndex = 5;
            lblFileLoaded.Text = "label1";
            lblFileLoaded.Visible = false;
            lblFileLoaded.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblFileLoaded.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btn_Load
            // 
            btn_Load.AccessibleName = "btn_Load";
            btn_Load.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_Load.BackColor = SystemColors.ActiveCaption;
            btn_Load.Cursor = Cursors.AppStarting;
            btn_Load.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Load.Location = new Point(8, 712);
            btn_Load.Name = "btn_Load";
            btn_Load.Size = new Size(80, 33);
            btn_Load.TabIndex = 6;
            btn_Load.Text = "Load File";
            btn_Load.UseVisualStyleBackColor = false;
            btn_Load.Click += btn_Load_Click;
            // 
            // dgvOptimal
            // 
            dgvOptimal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvOptimal.BackgroundColor = Color.White;
            dgvOptimal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptimal.Location = new Point(8, 545);
            dgvOptimal.Name = "dgvOptimal";
            dgvOptimal.Size = new Size(557, 161);
            dgvOptimal.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 6);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 2;
            label1.Text = "Canonical Form:";
            label1.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // iterationsPanelPrimal
            // 
            iterationsPanelPrimal.AccessibleName = "iterationsPanelPrimal";
            iterationsPanelPrimal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            iterationsPanelPrimal.AutoScroll = true;
            iterationsPanelPrimal.BackColor = Color.FromArgb(240, 248, 255);
            iterationsPanelPrimal.FlowDirection = FlowDirection.TopDown;
            iterationsPanelPrimal.Location = new Point(584, 6);
            iterationsPanelPrimal.Name = "iterationsPanelPrimal";
            iterationsPanelPrimal.Padding = new Padding(10);
            iterationsPanelPrimal.Size = new Size(899, 795);
            iterationsPanelPrimal.TabIndex = 1;
            iterationsPanelPrimal.WrapContents = false;
            // 
            // canonicalDgvPrimal
            // 
            canonicalDgvPrimal.AccessibleName = "canonicalDgvPrimal";
            canonicalDgvPrimal.BackgroundColor = Color.White;
            canonicalDgvPrimal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            canonicalDgvPrimal.Location = new Point(8, 32);
            canonicalDgvPrimal.Name = "canonicalDgvPrimal";
            canonicalDgvPrimal.Size = new Size(557, 468);
            canonicalDgvPrimal.TabIndex = 0;
            // 
            // TabControl
            // 
            TabControl.AccessibleName = "TabControl";
            TabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TabControl.Controls.Add(tp_primalSimplex);
            TabControl.Controls.Add(tp_revisedPrimal);
            TabControl.Controls.Add(tp_branchBound);
            TabControl.Controls.Add(tp_cuttingPlane);
            TabControl.Controls.Add(tp_branchBoundKnap);
            TabControl.Controls.Add(tp_sensitivity);
            TabControl.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            TabControl.Location = new Point(0, 0);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(1499, 837);
            TabControl.TabIndex = 3;
            TabControl.ItemSize = new Size(140, 32);   
            TabControl.SizeMode = TabSizeMode.Fixed;
            // 
            // tp_sensitivity
            // 
            tp_sensitivity.AccessibleName = "tp_sensitivity";
            tp_sensitivity.BackColor = Color.AliceBlue;
            tp_sensitivity.Controls.Add(panelSensitivityIterations);
            tp_sensitivity.Controls.Add(lblIterationHistory);
            tp_sensitivity.Controls.Add(btnSaveAnalysis);
            tp_sensitivity.Controls.Add(lblOptimalTableau);
            tp_sensitivity.Controls.Add(dgvSensitivityTableau);
            tp_sensitivity.Controls.Add(btnRunSensitivity);
            tp_sensitivity.Controls.Add(lblResults);
            tp_sensitivity.Controls.Add(dgvResults);
            tp_sensitivity.Controls.Add(numNewValue);
            tp_sensitivity.Controls.Add(txtNewData);
            tp_sensitivity.Controls.Add(lblExtraInput);
            tp_sensitivity.Controls.Add(cmbTarget);
            tp_sensitivity.Controls.Add(lblTarget);
            tp_sensitivity.Controls.Add(cmboSensitivity);
            tp_sensitivity.Controls.Add(lblOperation);
            tp_sensitivity.Location = new Point(4, 24);
            tp_sensitivity.Name = "tp_sensitivity";
            tp_sensitivity.Padding = new Padding(3);
            tp_sensitivity.Size = new Size(1491, 809);
            tp_sensitivity.TabIndex = 5;
            tp_sensitivity.Text = "Sensitivity Analysis";
            // 
            // panelSensitivityIterations
            // 
            panelSensitivityIterations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelSensitivityIterations.BackColor = Color.FromArgb(240, 248, 255); 
            panelSensitivityIterations.FlowDirection = FlowDirection.TopDown;
            panelSensitivityIterations.Location = new Point(758, 30);
            panelSensitivityIterations.Name = "panelSensitivityIterations";
            panelSensitivityIterations.Padding = new Padding(10);
            panelSensitivityIterations.Size = new Size(727, 771);
            panelSensitivityIterations.TabIndex = 25;
            iterationsPanelPrimalRevised.WrapContents = false;
            // 
            // lblIterationHistory
            // 
            lblIterationHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblIterationHistory.AutoSize = true;
            lblIterationHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblIterationHistory.Location = new Point(758, 8);
            lblIterationHistory.Name = "lblIterationHistory";
            lblIterationHistory.Size = new Size(102, 15);
            lblIterationHistory.TabIndex = 26;
            lblIterationHistory.Text = "Iteration History:";
            lblIterationHistory.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblIterationHistory.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // btnSaveAnalysis
            // 
            btnSaveAnalysis.AccessibleName = "btnSaveAnalysis";
            btnSaveAnalysis.BackColor = SystemColors.ActiveCaption;
            btnSaveAnalysis.Location = new Point(6, 240);
            btnSaveAnalysis.Name = "btnSaveAnalysis";
            btnSaveAnalysis.Size = new Size(331, 28);
            btnSaveAnalysis.TabIndex = 24;
            btnSaveAnalysis.Text = "Save Output To Text File";
            btnSaveAnalysis.UseVisualStyleBackColor = false;
            btnSaveAnalysis.Click += btnSaveAnalysis_Click;
            // 
            // lblOptimalTableau
            // 
            lblOptimalTableau.Anchor = AnchorStyles.Top;
            lblOptimalTableau.AutoSize = true;
            lblOptimalTableau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOptimalTableau.Location = new Point(355, 355);
            lblOptimalTableau.Name = "lblOptimalTableau";
            lblOptimalTableau.Size = new Size(98, 15);
            lblOptimalTableau.TabIndex = 23;
            lblOptimalTableau.Text = "Optimal tableau:";
            lblOptimalTableau.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblOptimalTableau.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // dgvSensitivityTableau
            // 
            dgvSensitivityTableau.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dgvSensitivityTableau.BackgroundColor = SystemColors.ActiveCaption;
            dgvSensitivityTableau.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSensitivityTableau.Location = new Point(355, 375);
            dgvSensitivityTableau.Name = "dgvSensitivityTableau";
            dgvSensitivityTableau.ReadOnly = true;
            dgvSensitivityTableau.Size = new Size(397, 426);
            dgvSensitivityTableau.TabIndex = 22;
            // 
            // btnRunSensitivity
            // 
            btnRunSensitivity.AccessibleName = "btnRunSensitivity";
            btnRunSensitivity.BackColor = SystemColors.ActiveCaption;
            btnRunSensitivity.Location = new Point(6, 200);
            btnRunSensitivity.Name = "btnRunSensitivity";
            btnRunSensitivity.Size = new Size(331, 30);
            btnRunSensitivity.TabIndex = 21;
            btnRunSensitivity.Text = "Run Analysis";
            btnRunSensitivity.UseVisualStyleBackColor = false;
            btnRunSensitivity.Click += btnRunSensitivity_Click;
            // 
            // lblResults
            // 
            lblResults.Anchor = AnchorStyles.Top;
            lblResults.AutoSize = true;
            lblResults.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResults.Location = new Point(355, 8);
            lblResults.Name = "lblResults";
            lblResults.Size = new Size(50, 15);
            lblResults.TabIndex = 28;
            lblResults.Text = "Results:";
            lblResults.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblResults.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // dgvResults
            // 
            dgvResults.Anchor = AnchorStyles.Top;
            dgvResults.BackgroundColor = SystemColors.ActiveCaption;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(355, 28);
            dgvResults.Name = "dgvResults";
            dgvResults.Size = new Size(397, 320);
            dgvResults.TabIndex = 20;
            // 
            // numNewValue
            // 
            numNewValue.BackColor = SystemColors.ActiveCaption;
            numNewValue.Location = new Point(6, 156);
            numNewValue.Name = "numNewValue";
            numNewValue.Size = new Size(331, 23);
            numNewValue.TabIndex = 19;
            numNewValue.Visible = false;
            // 
            // txtNewData
            // 
            txtNewData.BackColor = SystemColors.ActiveCaption;
            txtNewData.Location = new Point(6, 156);
            txtNewData.Name = "txtNewData";
            txtNewData.Size = new Size(331, 23);
            txtNewData.TabIndex = 18;
            txtNewData.Visible = false;
            // 
            // lblExtraInput
            // 
            lblExtraInput.AutoSize = true;
            lblExtraInput.Location = new Point(6, 138);
            lblExtraInput.Name = "lblExtraInput";
            lblExtraInput.Size = new Size(98, 15);
            lblExtraInput.TabIndex = 29;
            lblExtraInput.Text = "Additional input:";
            lblExtraInput.Visible = false;
            lblExtraInput.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblExtraInput.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // cmbTarget
            // 
            cmbTarget.BackColor = SystemColors.ActiveCaption;
            cmbTarget.FormattingEnabled = true;
            cmbTarget.Location = new Point(6, 78);
            cmbTarget.Name = "cmbTarget";
            cmbTarget.Size = new Size(331, 23);
            cmbTarget.TabIndex = 17;
            cmbTarget.Visible = false;
            // 
            // lblTarget
            // 
            lblTarget.AutoSize = true;
            lblTarget.Location = new Point(6, 60);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(46, 15);
            lblTarget.TabIndex = 30;
            lblTarget.Text = "Target:";
            lblTarget.Visible = false;
            lblTarget.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblTarget.ForeColor = Color.FromArgb(70, 130, 180);
            // 

            // cmboSensitivity
            // 
            cmboSensitivity.AccessibleName = "cmboSensitivity";
            cmboSensitivity.BackColor = SystemColors.ActiveCaption;
            cmboSensitivity.FormattingEnabled = true;
            cmboSensitivity.Items.AddRange(new object[] { "Range of selected non-basic variable", "Change selected non-basic variable", "Range of selected basic variable", "Change selected basic variable", "Range of selected constraint rhs value", "Change selected constraint rhs value", "Range of selected variable in a non-basic variable column", "Change selected variable in a non-basic variable column", "Add new activity to optimal solution", "Add new constraint to optimal solution", "Display shadow prices", "Apply duality", "Solve Dual Programming model", "Verify whether programming model has strong/weak duality" });
            cmboSensitivity.Location = new Point(6, 26);
            cmboSensitivity.Name = "cmboSensitivity";
            cmboSensitivity.Size = new Size(331, 23);
            cmboSensitivity.TabIndex = 16;
            cmboSensitivity.Text = "Select operation";
            cmboSensitivity.SelectedIndexChanged += cmboSensitivity_SelectedIndexChanged;
            // 
            // lblOperation
            // 
            lblOperation.AutoSize = true;
            lblOperation.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOperation.Location = new Point(6, 8);
            lblOperation.Name = "lblOperation";
            lblOperation.Size = new Size(66, 15);
            lblOperation.TabIndex = 27;
            lblOperation.Text = "Operation:";
            lblOperation.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblOperation.ForeColor = Color.FromArgb(70, 130, 180);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1499, 837);
            Controls.Add(TabControl);
            Name = "Form1";
            Text = "Linear Programming Solver";
            TransparencyKey = Color.LightCyan;
            WindowState = FormWindowState.Maximized;
            tp_branchBoundKnap.ResumeLayout(false);
            tp_branchBoundKnap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBestCandidateKnapsack).EndInit();
            tp_cuttingPlane.ResumeLayout(false);
            tp_cuttingPlane.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBestCandidateCuttingPlane).EndInit();
            tp_branchBound.ResumeLayout(false);
            tp_branchBound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBestCandidateBranchBound).EndInit();
            tp_revisedPrimal.ResumeLayout(false);
            tp_revisedPrimal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOptimalRevised).EndInit();
            ((System.ComponentModel.ISupportInitialize)canonicalDgvPrimalRevised).EndInit();
            tp_primalSimplex.ResumeLayout(false);
            tp_primalSimplex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOptimal).EndInit();
            ((System.ComponentModel.ISupportInitialize)canonicalDgvPrimal).EndInit();
            TabControl.ResumeLayout(false);
            tp_sensitivity.ResumeLayout(false);
            tp_sensitivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensitivityTableau).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNewValue).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabPage tp_branchBoundKnap;
        private TabPage tp_cuttingPlane;
        private TabPage tp_branchBound;
        private TabPage tp_revisedPrimal;
        private TabPage tp_primalSimplex;
        private TabControl TabControl;
        private Button btn_Solve;
        private Panel pnl_inputFile;
        private Button btn_Back;
        private Button btn_Next;
        private Label label2;
        private TabPage tabPage2;
        private Label lblFileLoaded;
        private Button btn_Load;
        private FlowLayoutPanel iterationsPanelPrimal;
        private DataGridView canonicalDgvPrimal;
        private TabPage tp_sensitivity;
        private DataGridView dgvOptimal;
        private Label label1;
        private ComboBox cmboSensitivity;
        private TextBox txtNewData;
        private ComboBox cmbTarget;
        private NumericUpDown numNewValue;
        private DataGridView dgvResults;
        private Button btnRunSensitivity;
        private DataGridView dgvSensitivityTableau;
        private Label lblOptimalTableau;
        private Label label3;
        private Button btnSave;
        private Button btnSaveAnalysis;
        private FlowLayoutPanel panelSensitivityIterations;
        private Label lblOperation;
        private Label lblTarget;
        private Label lblExtraInput;
        private Label lblResults;
        private Label lblIterationHistory;
        private Button btnSaveRevised;
        private Label label4;
        private Button btn_SolveRevised;
        private Label lblFileLoadedRevised;
        private Button btn_LoadRevised;
        private DataGridView dgvOptimalRevised;
        private Label label6;
        private FlowLayoutPanel iterationsPanelPrimalRevised;
        private DataGridView canonicalDgvPrimalRevised;
        private Button btnSaveBranchBound;
        private Label label5;
        private Button btn_SolveBranchBound;
        private Label lblFileLoadedBranchBound;
        private Button btn_LoadBranchBound;
        private DataGridView dgvBestCandidateBranchBound;
        private FlowLayoutPanel iterationsPanelBranchBound;
        private Button btnSaveCuttingPlane;
        private Label label7;
        private Button btn_SolveCuttingPlane;
        private Label lblFileLoadedCuttingPlane;
        private Button btn_LoadCuttingPlane;
        private DataGridView dgvBestCandidateCuttingPlane;
        private FlowLayoutPanel iterationsPanelCuttingPlane;
        private Button btnSaveKnapSack;
        private Label label8;
        private Button btn_SolveKnapSack;
        private Label lblFileLoadedKnapSack;
        private Button btn_LoadKnapSack;
        private DataGridView dgvBestCandidateKnapsack;
        private FlowLayoutPanel iterationsPanelKnapSack;
    }
}
