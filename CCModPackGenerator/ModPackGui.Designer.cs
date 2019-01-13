namespace CCModPackGenerator
{
    partial class ModPackGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModPackGui));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWorkspace = new System.Windows.Forms.TextBox();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.commonTab = new System.Windows.Forms.TabPage();
            this.costumeBuilderCommon = new CCModPackGenerator.CostumeBuilder();
            this.honokaTab = new System.Windows.Forms.TabPage();
            this.costumeBuilderHonoka = new CCModPackGenerator.CostumeBuilder();
            this.marieTab = new System.Windows.Forms.TabPage();
            this.costumeBuilderMarieRose = new CCModPackGenerator.CostumeBuilder();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.constantBufferEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.commonTab.SuspendLayout();
            this.honokaTab.SuspendLayout();
            this.marieTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.generalTab);
            this.tabControl1.Controls.Add(this.commonTab);
            this.tabControl1.Controls.Add(this.honokaTab);
            this.tabControl1.Controls.Add(this.marieTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 706);
            this.tabControl1.TabIndex = 0;
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.label6);
            this.generalTab.Controls.Add(this.textBox2);
            this.generalTab.Controls.Add(this.label5);
            this.generalTab.Controls.Add(this.label4);
            this.generalTab.Controls.Add(this.label3);
            this.generalTab.Controls.Add(this.label2);
            this.generalTab.Controls.Add(this.textBox1);
            this.generalTab.Controls.Add(this.label1);
            this.generalTab.Controls.Add(this.textBoxWorkspace);
            this.generalTab.Controls.Add(this.textBoxVersion);
            this.generalTab.Controls.Add(this.textBoxName);
            this.generalTab.Location = new System.Drawing.Point(4, 22);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(1000, 680);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "General";
            this.generalTab.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(470, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Quick Guide";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(467, 75);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(525, 571);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(994, 41);
            this.label5.TabIndex = 4;
            this.label5.Text = "Costume Customizer Mod Pack Generator";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "CostumeCustomizer Mod Folder";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 655);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Current Workspace Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Version";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 166);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(435, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // textBoxWorkspace
            // 
            this.textBoxWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWorkspace.Location = new System.Drawing.Point(140, 652);
            this.textBoxWorkspace.Name = "textBoxWorkspace";
            this.textBoxWorkspace.ReadOnly = true;
            this.textBoxWorkspace.Size = new System.Drawing.Size(857, 22);
            this.textBoxWorkspace.TabIndex = 0;
            this.textBoxWorkspace.Leave += new System.EventHandler(this.textBoxWorkspace_Leave);
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(10, 112);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(436, 22);
            this.textBoxVersion.TabIndex = 0;
            this.textBoxVersion.Leave += new System.EventHandler(this.textBoxVersion_Leave);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(10, 61);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(436, 22);
            this.textBoxName.TabIndex = 0;
            this.textBoxName.Leave += new System.EventHandler(this.textBoxName_Leave);
            // 
            // commonTab
            // 
            this.commonTab.Controls.Add(this.costumeBuilderCommon);
            this.commonTab.Location = new System.Drawing.Point(4, 22);
            this.commonTab.Name = "commonTab";
            this.commonTab.Size = new System.Drawing.Size(1000, 680);
            this.commonTab.TabIndex = 1;
            this.commonTab.Text = "Common Model";
            this.commonTab.UseVisualStyleBackColor = true;
            // 
            // costumeBuilderCommon
            // 
            this.costumeBuilderCommon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.costumeBuilderCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costumeBuilderCommon.Location = new System.Drawing.Point(0, 0);
            this.costumeBuilderCommon.Name = "costumeBuilderCommon";
            this.costumeBuilderCommon.Size = new System.Drawing.Size(1000, 680);
            this.costumeBuilderCommon.TabIndex = 0;
            // 
            // honokaTab
            // 
            this.honokaTab.Controls.Add(this.costumeBuilderHonoka);
            this.honokaTab.Location = new System.Drawing.Point(4, 22);
            this.honokaTab.Name = "honokaTab";
            this.honokaTab.Size = new System.Drawing.Size(1000, 680);
            this.honokaTab.TabIndex = 2;
            this.honokaTab.Text = "Honoka/Luna Model";
            this.honokaTab.UseVisualStyleBackColor = true;
            // 
            // costumeBuilderHonoka
            // 
            this.costumeBuilderHonoka.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.costumeBuilderHonoka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costumeBuilderHonoka.Location = new System.Drawing.Point(0, 0);
            this.costumeBuilderHonoka.Name = "costumeBuilderHonoka";
            this.costumeBuilderHonoka.Size = new System.Drawing.Size(1000, 680);
            this.costumeBuilderHonoka.TabIndex = 0;
            // 
            // marieTab
            // 
            this.marieTab.Controls.Add(this.costumeBuilderMarieRose);
            this.marieTab.Location = new System.Drawing.Point(4, 22);
            this.marieTab.Name = "marieTab";
            this.marieTab.Size = new System.Drawing.Size(1000, 680);
            this.marieTab.TabIndex = 3;
            this.marieTab.Text = "Marie Rose Model";
            this.marieTab.UseVisualStyleBackColor = true;
            // 
            // costumeBuilderMarieRose
            // 
            this.costumeBuilderMarieRose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.costumeBuilderMarieRose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costumeBuilderMarieRose.Location = new System.Drawing.Point(0, 0);
            this.costumeBuilderMarieRose.Name = "costumeBuilderMarieRose";
            this.costumeBuilderMarieRose.Size = new System.Drawing.Size(1000, 680);
            this.costumeBuilderMarieRose.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.constantBufferEditorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // constantBufferEditorToolStripMenuItem
            // 
            this.constantBufferEditorToolStripMenuItem.Name = "constantBufferEditorToolStripMenuItem";
            this.constantBufferEditorToolStripMenuItem.Size = new System.Drawing.Size(133, 20);
            this.constantBufferEditorToolStripMenuItem.Text = "ConstantBuffer Editor";
            this.constantBufferEditorToolStripMenuItem.Click += new System.EventHandler(this.constantBufferEditorToolStripMenuItem_Click);
            // 
            // ModPackGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ModPackGui";
            this.Text = "CostumeCustomizer Mod Pack Generator";
            this.tabControl1.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.generalTab.PerformLayout();
            this.commonTab.ResumeLayout(false);
            this.honokaTab.ResumeLayout(false);
            this.marieTab.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.TabPage commonTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TabPage honokaTab;
        private System.Windows.Forms.TabPage marieTab;
        private CostumeBuilder costumeBuilderCommon;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private CostumeBuilder costumeBuilderHonoka;
        private CostumeBuilder costumeBuilderMarieRose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWorkspace;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem constantBufferEditorToolStripMenuItem;
    }
}

