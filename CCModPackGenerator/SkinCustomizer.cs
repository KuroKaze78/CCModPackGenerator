using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CCModPackGenerator
{
    public partial class SkinCustomizer : Form
    {
        public Dictionary<String, String> SkinTextures { get; private set; }
        public String TanTexture { get; set; }

        public SkinCustomizer(CCModConfig.BodyMesh bMesh, CCModConfig.ModelType modelType)
        {
            InitializeComponent();
            SkinTextures = new Dictionary<string, string>();

            TanTexture = bMesh.TanTexture;
            resourceBuilderTan.SetValue(TanTexture);
            resourceBuilderTan.ResourceUpdated = delegate (object sender, ResourceBuilder.StringEventArgs e)
            {
                TanTexture = e.value;
                if (modelType == CCModConfig.ModelType.Honoka)
                {
                    ModPackGui.CacheString(e.value, ref ModPackGui.honokaTanList);
                }
                else if (modelType == CCModConfig.ModelType.MarieRose)
                {
                    ModPackGui.CacheString(e.value, ref ModPackGui.marieTanList);
                }
                else
                {
                    ModPackGui.CacheString(e.value, ref ModPackGui.commonTanList);
                }
            };

            StringCollection textureSlots = Properties.Settings.Default.CommonSkins;
            if (modelType == CCModConfig.ModelType.Honoka)
            {
                resourceBuilderTan.SetBindingList(ref ModPackGui.honokaTanList);
                textureSlots = Properties.Settings.Default.HonokaSkins;
            }
            else if (modelType == CCModConfig.ModelType.MarieRose)
            {
                resourceBuilderTan.SetBindingList(ref ModPackGui.marieTanList);
                textureSlots = Properties.Settings.Default.MarieSkins;
            }
            else
            {
                resourceBuilderTan.SetBindingList(ref ModPackGui.commonTanList);
            }

            foreach (CCModConfig.SkinTexture aSkinTexture in bMesh.SkinTextures)
            {
                SkinTextures[aSkinTexture.SkinSlot] = aSkinTexture.Filename;
            }

            this.SuspendLayout();
            foreach (String aTextureSlot in textureSlots)
            {
                if (!SkinTextures.ContainsKey(aTextureSlot))
                {
                    SkinTextures[aTextureSlot] = "";
                }

                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40.0f));

                ResourceBuilder textureSlotResource = new ResourceBuilder();
                textureSlotResource.FileFilter = "DDS (*.dds)|*.dds|PNG (*.png)|*.png";
                textureSlotResource.Dock = DockStyle.Top;
                if (modelType == CCModConfig.ModelType.Honoka)
                {
                    textureSlotResource.SetBindingList(ref ModPackGui.honokaSkinList);
                }
                else if (modelType == CCModConfig.ModelType.MarieRose)
                {
                    textureSlotResource.SetBindingList(ref ModPackGui.marieSkinList);
                }
                else
                {
                    textureSlotResource.SetBindingList(ref ModPackGui.commonSkinList);
                }
                textureSlotResource.SetValue(SkinTextures[aTextureSlot]);

                textureSlotResource.ResourceUpdated = delegate(object sender, ResourceBuilder.StringEventArgs e)
                {
                    SkinTextures[aTextureSlot] = e.value;
                    if (modelType == CCModConfig.ModelType.Honoka)
                    {
                        ModPackGui.CacheString(e.value, ref ModPackGui.honokaSkinList);
                    }
                    else if (modelType == CCModConfig.ModelType.MarieRose)
                    {
                        ModPackGui.CacheString(e.value, ref ModPackGui.marieSkinList);
                    }
                    else
                    {
                        ModPackGui.CacheString(e.value, ref ModPackGui.commonSkinList);
                    }
                };

                Label textureLabel = new Label();
                textureLabel.Text = aTextureSlot;
                textureLabel.Dock = DockStyle.Fill;
                textureLabel.AutoSize = false;
                textureLabel.TextAlign = ContentAlignment.MiddleCenter;

                this.tableLayoutPanel1.Controls.Add(textureLabel);
                this.tableLayoutPanel1.Controls.Add(textureSlotResource);
            }

            this.tableLayoutPanel1.RowCount++;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40.0f));

            Button okButton = new Button();
            okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(okButton, 2);
            okButton.Location = new System.Drawing.Point(367, 453);
            okButton.Margin = new System.Windows.Forms.Padding(3, 3, 20, 10);
            okButton.Size = new System.Drawing.Size(91, 29);
            okButton.TabIndex = 100;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += new System.EventHandler(this.buttonOK_Click);
            this.tableLayoutPanel1.Controls.Add(okButton);

            this.ResumeLayout();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
