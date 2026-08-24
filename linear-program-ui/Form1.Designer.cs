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
            richTextBox5 = new RichTextBox();
            tp_cuttingPlane = new TabPage();
            richTextBox4 = new RichTextBox();
            tp_branchBound = new TabPage();
            richTextBox3 = new RichTextBox();
            tp_revisedPrimal = new TabPage();
            richTextBox2 = new RichTextBox();
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
            tabControl1 = new TabControl();
            tp_sensitivity = new TabPage();
            panelSensitivityIterations = new FlowLayoutPanel();
            btnSaveAnalysis = new Button();
            lblOptimalTableau = new Label();
            dgvSensitivityTableau = new DataGridView();
            btnRunSensitivity = new Button();
            dgvResults = new DataGridView();
            numNewValue = new NumericUpDown();
            txtNewData = new TextBox();
            cmbTarget = new ComboBox();
            cmboSensitivity = new ComboBox();
            tp_branchBoundKnap.SuspendLayout();
            tp_cuttingPlane.SuspendLayout();
            tp_branchBound.SuspendLayout();
            tp_revisedPrimal.SuspendLayout();
            tp_primalSimplex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOptimal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)canonicalDgvPrimal).BeginInit();
            tabControl1.SuspendLayout();
            tp_sensitivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensitivityTableau).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNewValue).BeginInit();
            SuspendLayout();
            // 
            // tp_branchBoundKnap
            // 
            tp_branchBoundKnap.AccessibleName = "tp_branchBoundKnap";
            tp_branchBoundKnap.Controls.Add(richTextBox5);
            tp_branchBoundKnap.Location = new Point(4, 24);
            tp_branchBoundKnap.Name = "tp_branchBoundKnap";
            tp_branchBoundKnap.Padding = new Padding(3);
            tp_branchBoundKnap.Size = new Size(1491, 809);
            tp_branchBoundKnap.TabIndex = 4;
            tp_branchBoundKnap.Text = "Branch And Bound Knapsack";
            tp_branchBoundKnap.UseVisualStyleBackColor = true;
            // 
            // richTextBox5
            // 
            richTextBox5.Location = new Point(43, 29);
            richTextBox5.Name = "richTextBox5";
            richTextBox5.Size = new Size(281, 282);
            richTextBox5.TabIndex = 0;
            richTextBox5.Text = "";
            // 
            // tp_cuttingPlane
            // 
            tp_cuttingPlane.AccessibleName = "tp_cuttingPlane";
            tp_cuttingPlane.Controls.Add(richTextBox4);
            tp_cuttingPlane.Location = new Point(4, 24);
            tp_cuttingPlane.Name = "tp_cuttingPlane";
            tp_cuttingPlane.Padding = new Padding(3);
            tp_cuttingPlane.Size = new Size(1491, 809);
            tp_cuttingPlane.TabIndex = 3;
            tp_cuttingPlane.Text = "Cutting Plane";
            tp_cuttingPlane.UseVisualStyleBackColor = true;
            // 
            // richTextBox4
            // 
            richTextBox4.Location = new Point(40, 15);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.Size = new Size(532, 309);
            richTextBox4.TabIndex = 0;
            richTextBox4.Text = "";
            // 
            // tp_branchBound
            // 
            tp_branchBound.AccessibleName = "tp_branchBound";
            tp_branchBound.Controls.Add(richTextBox3);
            tp_branchBound.Location = new Point(4, 24);
            tp_branchBound.Name = "tp_branchBound";
            tp_branchBound.Padding = new Padding(3);
            tp_branchBound.RightToLeft = RightToLeft.Yes;
            tp_branchBound.Size = new Size(1491, 809);
            tp_branchBound.TabIndex = 2;
            tp_branchBound.Text = "Branch And Bound";
            tp_branchBound.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            richTextBox3.Location = new Point(60, 27);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new Size(471, 313);
            richTextBox3.TabIndex = 0;
            richTextBox3.Text = "";
            // 
            // tp_revisedPrimal
            // 
            tp_revisedPrimal.AccessibleName = "tp_revisedPrimal";
            tp_revisedPrimal.BackColor = Color.AliceBlue;
            tp_revisedPrimal.Controls.Add(richTextBox2);
            tp_revisedPrimal.Location = new Point(4, 24);
            tp_revisedPrimal.Name = "tp_revisedPrimal";
            tp_revisedPrimal.Padding = new Padding(3);
            tp_revisedPrimal.Size = new Size(1491, 809);
            tp_revisedPrimal.TabIndex = 1;
            tp_revisedPrimal.Text = "Revised Primal Simplex";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(18, 36);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(532, 257);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
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
            dgvOptimal.BackgroundColor = SystemColors.ActiveCaption;
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
            // 
            // iterationsPanelPrimal
            // 
            iterationsPanelPrimal.AccessibleName = "iterationsPanelPrimal";
            iterationsPanelPrimal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            iterationsPanelPrimal.AutoScroll = true;
            iterationsPanelPrimal.BackColor = SystemColors.ActiveCaption;
            iterationsPanelPrimal.FlowDirection = FlowDirection.TopDown;
            iterationsPanelPrimal.Location = new Point(584, 6);
            iterationsPanelPrimal.Name = "iterationsPanelPrimal";
            iterationsPanelPrimal.Size = new Size(899, 795);
            iterationsPanelPrimal.TabIndex = 1;
            iterationsPanelPrimal.WrapContents = false;
            // 
            // canonicalDgvPrimal
            // 
            canonicalDgvPrimal.AccessibleName = "canonicalDgvPrimal";
            canonicalDgvPrimal.BackgroundColor = SystemColors.ActiveCaption;
            canonicalDgvPrimal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            canonicalDgvPrimal.Location = new Point(8, 32);
            canonicalDgvPrimal.Name = "canonicalDgvPrimal";
            canonicalDgvPrimal.Size = new Size(557, 468);
            canonicalDgvPrimal.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tp_primalSimplex);
            tabControl1.Controls.Add(tp_revisedPrimal);
            tabControl1.Controls.Add(tp_branchBound);
            tabControl1.Controls.Add(tp_cuttingPlane);
            tabControl1.Controls.Add(tp_branchBoundKnap);
            tabControl1.Controls.Add(tp_sensitivity);
            tabControl1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1499, 837);
            tabControl1.TabIndex = 3;
            // 
            // tp_sensitivity
            // 
            tp_sensitivity.AccessibleName = "tp_sensitivity";
            tp_sensitivity.BackColor = Color.AliceBlue;
            tp_sensitivity.Controls.Add(panelSensitivityIterations);
            tp_sensitivity.Controls.Add(btnSaveAnalysis);
            tp_sensitivity.Controls.Add(lblOptimalTableau);
            tp_sensitivity.Controls.Add(dgvSensitivityTableau);
            tp_sensitivity.Controls.Add(btnRunSensitivity);
            tp_sensitivity.Controls.Add(dgvResults);
            tp_sensitivity.Controls.Add(numNewValue);
            tp_sensitivity.Controls.Add(txtNewData);
            tp_sensitivity.Controls.Add(cmbTarget);
            tp_sensitivity.Controls.Add(cmboSensitivity);
            tp_sensitivity.Location = new Point(4, 24);
            tp_sensitivity.Name = "tp_sensitivity";
            tp_sensitivity.Padding = new Padding(3);
            tp_sensitivity.Size = new Size(1491, 809);
            tp_sensitivity.TabIndex = 5;
            tp_sensitivity.Text = "Sensitivity Analysis";
            // 
            // panelSensitivityIterations
            // 
            panelSensitivityIterations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSensitivityIterations.BackColor = SystemColors.ActiveCaption;
            panelSensitivityIterations.FlowDirection = FlowDirection.TopDown;
            panelSensitivityIterations.Location = new Point(758, 7);
            panelSensitivityIterations.Name = "panelSensitivityIterations";
            panelSensitivityIterations.Size = new Size(727, 794);
            panelSensitivityIterations.TabIndex = 25;
            // 
            // btnSaveAnalysis
            // 
            btnSaveAnalysis.AccessibleName = "btnSaveAnalysis";
            btnSaveAnalysis.BackColor = SystemColors.ActiveCaption;
            btnSaveAnalysis.Location = new Point(8, 226);
            btnSaveAnalysis.Name = "btnSaveAnalysis";
            btnSaveAnalysis.Size = new Size(330, 23);
            btnSaveAnalysis.TabIndex = 24;
            btnSaveAnalysis.Text = "Save Output To Text File";
            btnSaveAnalysis.UseVisualStyleBackColor = false;
            // 
            // lblOptimalTableau
            // 
            lblOptimalTableau.Anchor = AnchorStyles.Bottom;
            lblOptimalTableau.AutoSize = true;
            lblOptimalTableau.Location = new Point(502, 390);
            lblOptimalTableau.Name = "lblOptimalTableau";
            lblOptimalTableau.Size = new Size(98, 15);
            lblOptimalTableau.TabIndex = 23;
            lblOptimalTableau.Text = "Optimal tableau:";
            // 
            // dgvSensitivityTableau
            // 
            dgvSensitivityTableau.Anchor = AnchorStyles.Bottom;
            dgvSensitivityTableau.BackgroundColor = SystemColors.ActiveCaption;
            dgvSensitivityTableau.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSensitivityTableau.Location = new Point(355, 408);
            dgvSensitivityTableau.Name = "dgvSensitivityTableau";
            dgvSensitivityTableau.ReadOnly = true;
            dgvSensitivityTableau.Size = new Size(397, 393);
            dgvSensitivityTableau.TabIndex = 22;
            // 
            // btnRunSensitivity
            // 
            btnRunSensitivity.AccessibleName = "btnRunSensitivity";
            btnRunSensitivity.BackColor = SystemColors.ActiveCaption;
            btnRunSensitivity.Location = new Point(6, 183);
            btnRunSensitivity.Name = "btnRunSensitivity";
            btnRunSensitivity.Size = new Size(330, 23);
            btnRunSensitivity.TabIndex = 21;
            btnRunSensitivity.Text = "Run Analysis";
            btnRunSensitivity.UseVisualStyleBackColor = false;
            btnRunSensitivity.Click += btnRunSensitivity_Click;
            // 
            // dgvResults
            // 
            dgvResults.Anchor = AnchorStyles.Top;
            dgvResults.BackgroundColor = SystemColors.ActiveCaption;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(355, 7);
            dgvResults.Name = "dgvResults";
            dgvResults.Size = new Size(397, 375);
            dgvResults.TabIndex = 20;
            // 
            // numNewValue
            // 
            numNewValue.BackColor = SystemColors.ActiveCaption;
            numNewValue.Location = new Point(6, 137);
            numNewValue.Name = "numNewValue";
            numNewValue.Size = new Size(330, 23);
            numNewValue.TabIndex = 19;
            numNewValue.Visible = false;
            // 
            // txtNewData
            // 
            txtNewData.BackColor = SystemColors.ActiveCaption;
            txtNewData.Location = new Point(6, 97);
            txtNewData.Name = "txtNewData";
            txtNewData.Size = new Size(331, 23);
            txtNewData.TabIndex = 18;
            txtNewData.Visible = false;
            // 
            // cmbTarget
            // 
            cmbTarget.BackColor = SystemColors.ActiveCaption;
            cmbTarget.FormattingEnabled = true;
            cmbTarget.Location = new Point(6, 54);
            cmbTarget.Name = "cmbTarget";
            cmbTarget.Size = new Size(331, 23);
            cmbTarget.TabIndex = 17;
            cmbTarget.Visible = false;
            // 
            // cmboSensitivity
            // 
            cmboSensitivity.AccessibleName = "cmboSensitivity";
            cmboSensitivity.BackColor = SystemColors.ActiveCaption;
            cmboSensitivity.FormattingEnabled = true;
            cmboSensitivity.Items.AddRange(new object[] { "Range of selected non-basic variable", "Change selected non-basic variable", "Range of selected basic variable", "Change selected basic variable", "Range of selected constraint rhs value", "Change selected constraint rhs value", "Range of selected variable in a non-basic variable column", "Change selected variable in a non-basic variable column", "Add new activity to optimal solution", "Add new constraint to optimal solution", "Display shadow prices", "Apply duality", "Solve Dual Programming model", "Verify whether programming model has strong/weak duality" });
            cmboSensitivity.Location = new Point(6, 7);
            cmboSensitivity.Name = "cmboSensitivity";
            cmboSensitivity.Size = new Size(330, 23);
            cmboSensitivity.TabIndex = 16;
            cmboSensitivity.Text = "Select operation";
            cmboSensitivity.SelectedIndexChanged += cmboSensitivity_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1499, 837);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Linear Programming Solver";
            TransparencyKey = Color.LightCyan;
            WindowState = FormWindowState.Maximized;
            tp_branchBoundKnap.ResumeLayout(false);
            tp_cuttingPlane.ResumeLayout(false);
            tp_branchBound.ResumeLayout(false);
            tp_revisedPrimal.ResumeLayout(false);
            tp_primalSimplex.ResumeLayout(false);
            tp_primalSimplex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOptimal).EndInit();
            ((System.ComponentModel.ISupportInitialize)canonicalDgvPrimal).EndInit();
            tabControl1.ResumeLayout(false);
            tp_sensitivity.ResumeLayout(false);
            tp_sensitivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensitivityTableau).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNewValue).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabPage tp_branchBoundKnap;
        private RichTextBox richTextBox5;
        private TabPage tp_cuttingPlane;
        private RichTextBox richTextBox4;
        private TabPage tp_branchBound;
        private RichTextBox richTextBox3;
        private TabPage tp_revisedPrimal;
        private RichTextBox richTextBox2;
        private TabPage tp_primalSimplex;
        private TabControl tabControl1;
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
    }
}
