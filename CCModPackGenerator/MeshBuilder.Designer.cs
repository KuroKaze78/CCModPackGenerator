namespace CCModPackGenerator
{
    partial class MeshBuilder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.enableShadows = new System.Windows.Forms.RadioButton();
            this.disableShadows = new System.Windows.Forms.RadioButton();
            this.customShadows = new System.Windows.Forms.RadioButton();
            this.labelIndexBuffer = new System.Windows.Forms.Label();
            this.labelVertexBuffer = new System.Windows.Forms.Label();
            this.labelTexture = new System.Windows.Forms.Label();
            this.labelNormalMap = new System.Windows.Forms.Label();
            this.labelSpecularMap = new System.Windows.Forms.Label();
            this.labelShadowRadio = new System.Windows.Forms.Label();
            this.labelShadowIB = new System.Windows.Forms.Label();
            this.labelShadowVB = new System.Windows.Forms.Label();
            this.svbResource = new CCModPackGenerator.ResourceBuilder();
            this.sibResource = new CCModPackGenerator.ResourceBuilder();
            this.ps2Resource = new CCModPackGenerator.ResourceBuilder();
            this.ps1Resource = new CCModPackGenerator.ResourceBuilder();
            this.ps0Resource = new CCModPackGenerator.ResourceBuilder();
            this.vbResource = new CCModPackGenerator.ResourceBuilder();
            this.ibResource = new CCModPackGenerator.ResourceBuilder();
            this.downOrder = new System.Windows.Forms.Button();
            this.upOrder = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.ShadowStride = new System.Windows.Forms.TextBox();
            this.labelShadowFormat = new System.Windows.Forms.Label();
            this.shadowFormat = new System.Windows.Forms.ComboBox();
            this.labelShadowStride = new System.Windows.Forms.Label();
            this.normalFormat = new System.Windows.Forms.ComboBox();
            this.normalStride = new System.Windows.Forms.TextBox();
            this.labelStride = new System.Windows.Forms.Label();
            this.labelFormat = new System.Windows.Forms.Label();
            this.labelCB = new System.Windows.Forms.Label();
            this.pscb2Resource = new CCModPackGenerator.ResourceBuilder();
            this.SuspendLayout();
            // 
            // enableShadows
            // 
            this.enableShadows.AutoSize = true;
            this.enableShadows.Location = new System.Drawing.Point(105, 276);
            this.enableShadows.Name = "enableShadows";
            this.enableShadows.Size = new System.Drawing.Size(58, 17);
            this.enableShadows.TabIndex = 5;
            this.enableShadows.TabStop = true;
            this.enableShadows.Text = "Enable";
            this.enableShadows.UseVisualStyleBackColor = true;
            this.enableShadows.CheckedChanged += new System.EventHandler(this.radioShadows_CheckedChanged);
            // 
            // disableShadows
            // 
            this.disableShadows.AutoSize = true;
            this.disableShadows.Location = new System.Drawing.Point(196, 276);
            this.disableShadows.Name = "disableShadows";
            this.disableShadows.Size = new System.Drawing.Size(60, 17);
            this.disableShadows.TabIndex = 6;
            this.disableShadows.TabStop = true;
            this.disableShadows.Text = "Disable";
            this.disableShadows.UseVisualStyleBackColor = true;
            this.disableShadows.CheckedChanged += new System.EventHandler(this.radioShadows_CheckedChanged);
            // 
            // customShadows
            // 
            this.customShadows.AutoSize = true;
            this.customShadows.Location = new System.Drawing.Point(288, 276);
            this.customShadows.Name = "customShadows";
            this.customShadows.Size = new System.Drawing.Size(60, 17);
            this.customShadows.TabIndex = 7;
            this.customShadows.TabStop = true;
            this.customShadows.Text = "Custom";
            this.customShadows.UseVisualStyleBackColor = true;
            this.customShadows.CheckedChanged += new System.EventHandler(this.radioShadows_CheckedChanged);
            // 
            // labelIndexBuffer
            // 
            this.labelIndexBuffer.Location = new System.Drawing.Point(8, 39);
            this.labelIndexBuffer.Name = "labelIndexBuffer";
            this.labelIndexBuffer.Size = new System.Drawing.Size(86, 13);
            this.labelIndexBuffer.TabIndex = 11;
            this.labelIndexBuffer.Text = "Index Buffer:";
            this.labelIndexBuffer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelVertexBuffer
            // 
            this.labelVertexBuffer.Location = new System.Drawing.Point(8, 101);
            this.labelVertexBuffer.Name = "labelVertexBuffer";
            this.labelVertexBuffer.Size = new System.Drawing.Size(86, 13);
            this.labelVertexBuffer.TabIndex = 12;
            this.labelVertexBuffer.Text = "Vertex Buffer:";
            this.labelVertexBuffer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTexture
            // 
            this.labelTexture.Location = new System.Drawing.Point(8, 137);
            this.labelTexture.Name = "labelTexture";
            this.labelTexture.Size = new System.Drawing.Size(86, 13);
            this.labelTexture.TabIndex = 13;
            this.labelTexture.Text = "PS-T0:";
            this.labelTexture.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelNormalMap
            // 
            this.labelNormalMap.Location = new System.Drawing.Point(8, 173);
            this.labelNormalMap.Name = "labelNormalMap";
            this.labelNormalMap.Size = new System.Drawing.Size(86, 13);
            this.labelNormalMap.TabIndex = 14;
            this.labelNormalMap.Text = "PS-T1:";
            this.labelNormalMap.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSpecularMap
            // 
            this.labelSpecularMap.Location = new System.Drawing.Point(8, 210);
            this.labelSpecularMap.Name = "labelSpecularMap";
            this.labelSpecularMap.Size = new System.Drawing.Size(86, 13);
            this.labelSpecularMap.TabIndex = 15;
            this.labelSpecularMap.Text = "PS-T2:";
            this.labelSpecularMap.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelShadowRadio
            // 
            this.labelShadowRadio.Location = new System.Drawing.Point(8, 278);
            this.labelShadowRadio.Name = "labelShadowRadio";
            this.labelShadowRadio.Size = new System.Drawing.Size(86, 13);
            this.labelShadowRadio.TabIndex = 16;
            this.labelShadowRadio.Text = "Shadows:";
            this.labelShadowRadio.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelShadowIB
            // 
            this.labelShadowIB.Location = new System.Drawing.Point(8, 308);
            this.labelShadowIB.Name = "labelShadowIB";
            this.labelShadowIB.Size = new System.Drawing.Size(86, 13);
            this.labelShadowIB.TabIndex = 17;
            this.labelShadowIB.Text = "Shadow IB:";
            this.labelShadowIB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelShadowVB
            // 
            this.labelShadowVB.Location = new System.Drawing.Point(8, 372);
            this.labelShadowVB.Name = "labelShadowVB";
            this.labelShadowVB.Size = new System.Drawing.Size(86, 13);
            this.labelShadowVB.TabIndex = 18;
            this.labelShadowVB.Text = "Shadow VB:";
            this.labelShadowVB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // svbResource
            // 
            this.svbResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.svbResource.FileFilter = "Vertex Buffer (*.vb)|*.vb";
            this.svbResource.Location = new System.Drawing.Point(95, 365);
            this.svbResource.Name = "svbResource";
            this.svbResource.ResourceUpdated = null;
            this.svbResource.Size = new System.Drawing.Size(280, 30);
            this.svbResource.TabIndex = 10;
            // 
            // sibResource
            // 
            this.sibResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sibResource.FileFilter = "Index Buffer (*.ib)|*.ib";
            this.sibResource.Location = new System.Drawing.Point(95, 301);
            this.sibResource.Name = "sibResource";
            this.sibResource.ResourceUpdated = null;
            this.sibResource.Size = new System.Drawing.Size(280, 30);
            this.sibResource.TabIndex = 9;
            // 
            // ps2Resource
            // 
            this.ps2Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ps2Resource.FileFilter = "DDS (*.dds)|*.dds|PNG (*.png)|*.png";
            this.ps2Resource.Location = new System.Drawing.Point(95, 203);
            this.ps2Resource.Name = "ps2Resource";
            this.ps2Resource.ResourceUpdated = null;
            this.ps2Resource.Size = new System.Drawing.Size(280, 30);
            this.ps2Resource.TabIndex = 8;
            // 
            // ps1Resource
            // 
            this.ps1Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ps1Resource.FileFilter = "DDS (*.dds)|*.dds|PNG (*.png)|*.png";
            this.ps1Resource.Location = new System.Drawing.Point(95, 166);
            this.ps1Resource.Name = "ps1Resource";
            this.ps1Resource.ResourceUpdated = null;
            this.ps1Resource.Size = new System.Drawing.Size(280, 30);
            this.ps1Resource.TabIndex = 4;
            // 
            // ps0Resource
            // 
            this.ps0Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ps0Resource.FileFilter = "DDS (*.dds)|*.dds|PNG (*.png)|*.png";
            this.ps0Resource.Location = new System.Drawing.Point(95, 130);
            this.ps0Resource.Name = "ps0Resource";
            this.ps0Resource.ResourceUpdated = null;
            this.ps0Resource.Size = new System.Drawing.Size(280, 30);
            this.ps0Resource.TabIndex = 3;
            // 
            // vbResource
            // 
            this.vbResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbResource.FileFilter = "Vertex Buffer (*.vb)|*.vb";
            this.vbResource.Location = new System.Drawing.Point(95, 94);
            this.vbResource.Name = "vbResource";
            this.vbResource.ResourceUpdated = null;
            this.vbResource.Size = new System.Drawing.Size(280, 30);
            this.vbResource.TabIndex = 2;
            // 
            // ibResource
            // 
            this.ibResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibResource.FileFilter = "Index Buffer (*.ib)|*.ib";
            this.ibResource.Location = new System.Drawing.Point(95, 31);
            this.ibResource.Name = "ibResource";
            this.ibResource.ResourceUpdated = null;
            this.ibResource.Size = new System.Drawing.Size(280, 30);
            this.ibResource.TabIndex = 1;
            // 
            // downOrder
            // 
            this.downOrder.Location = new System.Drawing.Point(255, 2);
            this.downOrder.Name = "downOrder";
            this.downOrder.Size = new System.Drawing.Size(27, 23);
            this.downOrder.TabIndex = 19;
            this.downOrder.Text = "▼";
            this.downOrder.UseVisualStyleBackColor = true;
            this.downOrder.Visible = false;
            this.downOrder.Click += new System.EventHandler(this.downOrder_Click);
            // 
            // upOrder
            // 
            this.upOrder.Location = new System.Drawing.Point(222, 2);
            this.upOrder.Name = "upOrder";
            this.upOrder.Size = new System.Drawing.Size(27, 23);
            this.upOrder.TabIndex = 19;
            this.upOrder.Text = "▲";
            this.upOrder.UseVisualStyleBackColor = true;
            this.upOrder.Visible = false;
            this.upOrder.Click += new System.EventHandler(this.upOrder_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTitle.Location = new System.Drawing.Point(8, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(65, 20);
            this.labelTitle.TabIndex = 20;
            this.labelTitle.Text = "Slot: S1";
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(288, 2);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 21;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // ShadowStride
            // 
            this.ShadowStride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShadowStride.Location = new System.Drawing.Point(325, 337);
            this.ShadowStride.Name = "ShadowStride";
            this.ShadowStride.Size = new System.Drawing.Size(40, 20);
            this.ShadowStride.TabIndex = 22;
            this.ShadowStride.Leave += new System.EventHandler(this.MeshSlotSVBStrideUpdated);
            // 
            // labelShadowFormat
            // 
            this.labelShadowFormat.Location = new System.Drawing.Point(49, 340);
            this.labelShadowFormat.Name = "labelShadowFormat";
            this.labelShadowFormat.Size = new System.Drawing.Size(45, 13);
            this.labelShadowFormat.TabIndex = 18;
            this.labelShadowFormat.Text = "Format:";
            this.labelShadowFormat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // shadowFormat
            // 
            this.shadowFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadowFormat.FormattingEnabled = true;
            this.shadowFormat.Items.AddRange(new object[] {
            "",
            "DXGI_FORMAT_R16_UINT"});
            this.shadowFormat.Location = new System.Drawing.Point(100, 337);
            this.shadowFormat.Name = "shadowFormat";
            this.shadowFormat.Size = new System.Drawing.Size(173, 21);
            this.shadowFormat.TabIndex = 23;
            this.shadowFormat.Leave += new System.EventHandler(this.MeshSlotSIBFormatUpdated);
            // 
            // labelShadowStride
            // 
            this.labelShadowStride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShadowStride.Location = new System.Drawing.Point(274, 340);
            this.labelShadowStride.Name = "labelShadowStride";
            this.labelShadowStride.Size = new System.Drawing.Size(45, 13);
            this.labelShadowStride.TabIndex = 18;
            this.labelShadowStride.Text = "Stride:";
            this.labelShadowStride.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // normalFormat
            // 
            this.normalFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.normalFormat.FormattingEnabled = true;
            this.normalFormat.Items.AddRange(new object[] {
            "",
            "DXGI_FORMAT_R16_UINT"});
            this.normalFormat.Location = new System.Drawing.Point(100, 67);
            this.normalFormat.Name = "normalFormat";
            this.normalFormat.Size = new System.Drawing.Size(173, 21);
            this.normalFormat.TabIndex = 27;
            this.normalFormat.Leave += new System.EventHandler(this.MeshSlotIBFormatUpdated);
            // 
            // normalStride
            // 
            this.normalStride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.normalStride.Location = new System.Drawing.Point(325, 67);
            this.normalStride.Name = "normalStride";
            this.normalStride.Size = new System.Drawing.Size(40, 20);
            this.normalStride.TabIndex = 26;
            this.normalStride.Leave += new System.EventHandler(this.MeshSlotVBStrideUpdated);
            // 
            // labelStride
            // 
            this.labelStride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStride.Location = new System.Drawing.Point(274, 70);
            this.labelStride.Name = "labelStride";
            this.labelStride.Size = new System.Drawing.Size(45, 13);
            this.labelStride.TabIndex = 24;
            this.labelStride.Text = "Stride:";
            this.labelStride.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelFormat
            // 
            this.labelFormat.Location = new System.Drawing.Point(49, 70);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(45, 13);
            this.labelFormat.TabIndex = 25;
            this.labelFormat.Text = "Format:";
            this.labelFormat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCB
            // 
            this.labelCB.Location = new System.Drawing.Point(8, 246);
            this.labelCB.Name = "labelCB";
            this.labelCB.Size = new System.Drawing.Size(86, 13);
            this.labelCB.TabIndex = 29;
            this.labelCB.Text = "PS-CB2:";
            this.labelCB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pscb2Resource
            // 
            this.pscb2Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pscb2Resource.FileFilter = "Buffer (*.buf)|*.buf";
            this.pscb2Resource.Location = new System.Drawing.Point(95, 239);
            this.pscb2Resource.Name = "pscb2Resource";
            this.pscb2Resource.ResourceUpdated = null;
            this.pscb2Resource.Size = new System.Drawing.Size(280, 30);
            this.pscb2Resource.TabIndex = 28;
            // 
            // MeshBuilder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.labelCB);
            this.Controls.Add(this.pscb2Resource);
            this.Controls.Add(this.normalFormat);
            this.Controls.Add(this.normalStride);
            this.Controls.Add(this.labelStride);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.shadowFormat);
            this.Controls.Add(this.ShadowStride);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.upOrder);
            this.Controls.Add(this.downOrder);
            this.Controls.Add(this.labelShadowStride);
            this.Controls.Add(this.labelShadowFormat);
            this.Controls.Add(this.labelShadowVB);
            this.Controls.Add(this.labelShadowIB);
            this.Controls.Add(this.labelShadowRadio);
            this.Controls.Add(this.labelSpecularMap);
            this.Controls.Add(this.labelNormalMap);
            this.Controls.Add(this.labelTexture);
            this.Controls.Add(this.labelVertexBuffer);
            this.Controls.Add(this.labelIndexBuffer);
            this.Controls.Add(this.svbResource);
            this.Controls.Add(this.sibResource);
            this.Controls.Add(this.ps2Resource);
            this.Controls.Add(this.customShadows);
            this.Controls.Add(this.disableShadows);
            this.Controls.Add(this.enableShadows);
            this.Controls.Add(this.ps1Resource);
            this.Controls.Add(this.ps0Resource);
            this.Controls.Add(this.vbResource);
            this.Controls.Add(this.ibResource);
            this.Name = "MeshBuilder";
            this.Size = new System.Drawing.Size(378, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ResourceBuilder ibResource;
        private ResourceBuilder vbResource;
        private ResourceBuilder ps0Resource;
        private ResourceBuilder ps1Resource;
        private System.Windows.Forms.RadioButton enableShadows;
        private System.Windows.Forms.RadioButton disableShadows;
        private System.Windows.Forms.RadioButton customShadows;
        private ResourceBuilder ps2Resource;
        private ResourceBuilder sibResource;
        private ResourceBuilder svbResource;
        private System.Windows.Forms.Label labelIndexBuffer;
        private System.Windows.Forms.Label labelVertexBuffer;
        private System.Windows.Forms.Label labelTexture;
        private System.Windows.Forms.Label labelNormalMap;
        private System.Windows.Forms.Label labelSpecularMap;
        private System.Windows.Forms.Label labelShadowRadio;
        private System.Windows.Forms.Label labelShadowIB;
        private System.Windows.Forms.Label labelShadowVB;
        private System.Windows.Forms.Button downOrder;
        private System.Windows.Forms.Button upOrder;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TextBox ShadowStride;
        private System.Windows.Forms.Label labelShadowFormat;
        private System.Windows.Forms.ComboBox shadowFormat;
        private System.Windows.Forms.Label labelShadowStride;
        private System.Windows.Forms.ComboBox normalFormat;
        private System.Windows.Forms.TextBox normalStride;
        private System.Windows.Forms.Label labelStride;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.Label labelCB;
        private ResourceBuilder pscb2Resource;
    }
}
