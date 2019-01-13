using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCModConfig;

namespace CCModPackGenerator
{
    public partial class MeshBuilder : UserControl
    {
        public EventHandler RemoveClicked { get; set; }
        public EventHandler UpClicked { get; set; }
        public EventHandler DownClicked { get; set; }
        public EventHandler<MeshSlotParameterEventArgs> MeshSlotUpdated { get; set; }
        public MeshSlotType meshSlotType { get; set; }

        public String normalFormatValue;
        public String shadowFormatValue;

        public class MeshSlotParameterEventArgs : EventArgs
        {
            public MeshSlotParameter meshSlotParam;
            public String value;

            public MeshSlotParameterEventArgs(MeshSlotParameter param, String value)
            {
                this.meshSlotParam = param;
                this.value = value;
            }
        }

        public enum MeshSlotParameter
        {
            IB,
            IB_FORMAT,
            VB,
            VB_STRIDE,
            SHADOW,
            SIB,
            SIB_FORMAT,
            SVB,
            SVB_STRIDE,
            PS0,
            PS1,
            PS2,
            PSCB2
        }


        public enum MeshSlotType
        {
            Solid,
            Alpha
        }

        public MeshBuilder()
        {
            InitializeComponent();

            this.ibResource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.vbResource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.sibResource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.svbResource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.ps0Resource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.ps1Resource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.ps2Resource.ResourceUpdated = MeshSlotFilenameUpdated;
            this.pscb2Resource.ResourceUpdated = MeshSlotFilenameUpdated;
            normalFormatValue = "";
            shadowFormatValue = "";
        }

        public void SetMeshSlot(MeshSlot meshSlot)
        {
            this.ibResource.SetValue(meshSlot.NormalMesh.IndexBuffer);
            SetNormalFormatText(meshSlot.NormalMesh.Format);
            this.vbResource.SetValue(meshSlot.NormalMesh.VertexBuffer);
            this.normalStride.Text = meshSlot.NormalMesh.Stride.ToString();

            if (meshSlot.ShadowMesh.IsDefault)
            {
                this.enableShadows.Checked = true;
            }
            else if (meshSlot.ShadowMesh.IsNull)
            {
                this.disableShadows.Checked = true;
            }
            else
            {
                this.customShadows.Checked = true;
                this.sibResource.SetValue(meshSlot.ShadowMesh.IndexBuffer);
                SetShadowFormatText(meshSlot.ShadowMesh.Format);
                this.svbResource.SetValue(meshSlot.ShadowMesh.VertexBuffer);
                this.ShadowStride.Text = meshSlot.ShadowMesh.Stride.ToString();
            }

            this.ps0Resource.SetValue(meshSlot.PS0Texture);
            this.ps1Resource.SetValue(meshSlot.PS1Texture);
            this.ps2Resource.SetValue(meshSlot.PS2Texture);
            this.pscb2Resource.SetValue(meshSlot.PSCB2Buffer);

            BindingSource bNFSource = new BindingSource();
            bNFSource.DataSource = ModPackGui.formatList;
            bNFSource.ListChanged += BNFSource_NormalListChanged;

            BindingSource bSFSource = new BindingSource();
            bSFSource.DataSource = ModPackGui.formatList;
            bSFSource.ListChanged += BNFSource_ShadowListChanged;

            this.vbResource.SetBindingList(ref ModPackGui.vbList);
            this.svbResource.SetBindingList(ref ModPackGui.vbList);

            this.ibResource.SetBindingList(ref ModPackGui.ibList);
            this.sibResource.SetBindingList(ref ModPackGui.ibList);

            this.ps0Resource.SetBindingList(ref ModPackGui.textureList);
            this.ps1Resource.SetBindingList(ref ModPackGui.textureList);
            this.ps2Resource.SetBindingList(ref ModPackGui.textureList);
            this.pscb2Resource.SetBindingList(ref ModPackGui.constantBufferList);
        }

        public void SetNormalFormatText(String value)
        {
            normalFormatValue = value;
            this.normalFormat.Text = value;
        }
        private void BNFSource_NormalListChanged(object sender, ListChangedEventArgs e)
        {
            this.normalFormat.Text = normalFormatValue;
        }

        public void SetShadowFormatText(String value)
        {
            shadowFormatValue = value;
            this.shadowFormat.Text = value;
        }
        private void BNFSource_ShadowListChanged(object sender, ListChangedEventArgs e)
        {
            this.shadowFormat.Text = shadowFormatValue;
        }

        public void SetMeshSlotTitle(String meshSlotTitle)
        {
            this.labelTitle.Text = meshSlotTitle;
        }

        private void upOrder_Click(object sender, EventArgs e)
        {
            UpClicked?.Invoke(this, e);
        }

        private void downOrder_Click(object sender, EventArgs e)
        {
            DownClicked?.Invoke(this, e);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            RemoveClicked?.Invoke(this, e);
        }

        private void MeshSlotFilenameUpdated(object sender, ResourceBuilder.StringEventArgs e)
        {
            MeshSlotParameter updatedParam = MeshSlotParameter.IB;
            if (sender == ibResource)
            {
                updatedParam = MeshSlotParameter.IB;
                ModPackGui.CacheString(e.value, ref ModPackGui.ibList);
            }
            else if (sender == vbResource)
            {
                updatedParam = MeshSlotParameter.VB;
                ModPackGui.CacheString(e.value, ref ModPackGui.vbList);
            }
            else if (sender == sibResource)
            {
                updatedParam = MeshSlotParameter.SIB;
                ModPackGui.CacheString(e.value, ref ModPackGui.ibList);
            }
            else if (sender == svbResource)
            {
                updatedParam = MeshSlotParameter.SVB;
                ModPackGui.CacheString(e.value, ref ModPackGui.vbList);
            }
            else if (sender == ps0Resource)
            {
                updatedParam = MeshSlotParameter.PS0;
                ModPackGui.CacheString(e.value, ref ModPackGui.textureList);
            }
            else if (sender == ps1Resource)
            {
                updatedParam = MeshSlotParameter.PS1;
                ModPackGui.CacheString(e.value, ref ModPackGui.textureList);
            }
            else if (sender == ps2Resource)
            {
                updatedParam = MeshSlotParameter.PS2;
                ModPackGui.CacheString(e.value, ref ModPackGui.textureList);
            }
            else if (sender == pscb2Resource)
            {
                updatedParam = MeshSlotParameter.PSCB2;
                ModPackGui.CacheString(e.value, ref ModPackGui.constantBufferList);
            }
            if (sender is ResourceBuilder)
            {
                (sender as ResourceBuilder).SetValue(e.value);
            }

            MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(updatedParam, e.value));
            
        }

        private void MeshSlotIBFormatUpdated(object sender, EventArgs e)
        {
            normalFormatValue = this.normalFormat.Text;
            ModPackGui.CacheString(normalFormatValue, ref ModPackGui.formatList);

            MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.IB_FORMAT, this.normalFormat.Text));
        }

        private void MeshSlotVBStrideUpdated(object sender, EventArgs e)
        {
            MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.VB_STRIDE, this.normalStride.Text));
        }

        private void MeshSlotSIBFormatUpdated(object sender, EventArgs e)
        {
            shadowFormatValue = this.shadowFormat.Text;
            ModPackGui.CacheString(shadowFormatValue, ref ModPackGui.formatList);

            MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.SIB_FORMAT, this.shadowFormat.Text));
        }

        private void MeshSlotSVBStrideUpdated(object sender, EventArgs e)
        {
            MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.SVB_STRIDE, this.ShadowStride.Text));
        }

        private void radioShadows_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioOption = sender as RadioButton;
            if (radioOption != null && radioOption.Checked)
            {
                if (sender == customShadows)
                {
                    this.sibResource.Enabled = true;
                    this.svbResource.Enabled = true;
                    this.shadowFormat.Enabled = true;
                    this.ShadowStride.Enabled = true;

                    MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.SHADOW, "CUSTOM"));
                }
                else if (sender == enableShadows)
                {
                    this.sibResource.SetValue("");
                    this.sibResource.Enabled = false;
                    this.svbResource.SetValue("");
                    this.svbResource.Enabled = false;
                    this.shadowFormat.Text = "";
                    this.shadowFormat.Enabled = false;
                    this.ShadowStride.Text = "";
                    this.ShadowStride.Enabled = false;

                    MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.SHADOW, "DEFAULT"));
                }
                else if (sender == disableShadows)
                {
                    this.sibResource.Enabled = false;
                    this.svbResource.SetValue("");
                    this.svbResource.Enabled = false;
                    this.shadowFormat.Text = "";
                    this.shadowFormat.Enabled = false;
                    this.ShadowStride.Text = "";
                    this.ShadowStride.Enabled = false;

                    MeshSlotUpdated?.Invoke(this, new MeshSlotParameterEventArgs(MeshSlotParameter.SHADOW, null));
                }
            }
        }
    }
}
