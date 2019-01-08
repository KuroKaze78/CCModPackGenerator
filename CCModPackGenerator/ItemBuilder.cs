using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CCModConfig;

namespace CCModPackGenerator
{
    public partial class ItemBuilder : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CCModConfig.Item ModItem { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<Option> OptionList { get; set; }

        private ModelType currentModelType { get; set; }

        public ItemBuilder()
        {
            InitializeComponent();
            this.optionsBox.DisplayMember = "Name";
            //OptionList = new BindingList<Option>();

            this.resourceSkinIB.ResourceUpdated = MeshSlotFilenameUpdated;
            this.resourceSkinVB.ResourceUpdated = MeshSlotFilenameUpdated;
            this.resourceNailIB.ResourceUpdated = MeshSlotFilenameUpdated;
            this.resourceNailVB.ResourceUpdated = MeshSlotFilenameUpdated;
        }

        public void SetBindingList(BindingList<Option> optionList)
        {
            OptionList = optionList;

            BindingSource bSource = new BindingSource();
            bSource.DataSource = OptionList;
            this.optionsBox.DataSource = bSource;
        }

        public void SetConfig(CCModConfig.Item modItem, ModelType modelType)
        {
            ModItem = modItem;
            currentModelType = modelType;

            BindingSource bodyFormatSource = new BindingSource();
            bodyFormatSource.DataSource = ModPackGui.formatList;
            this.comboBoxBodyFormat.DataSource = bodyFormatSource;

            BindingSource nailFormatSource = new BindingSource();
            nailFormatSource.DataSource = ModPackGui.formatList;
            this.comboNailFormat.DataSource = nailFormatSource;

            this.optionsBox.SelectedItem = null;
            OptionList.Clear();

            foreach (Option opt in ModItem.Options)
            {
                OptionList.Add(opt);
            }

            this.textBoxIcon.Text = ModItem.Icon;
            if (ModItem.Icon != null)
            {
                this.iconGraphic.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, ModItem.Icon);
            }
            this.textBoxPreview.Text = ModItem.Preview;
            if (ModItem.Preview != null)
            {
                this.previewGraphic.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, ModItem.Preview);
            }
        }

        public CCModConfig.Item UpdateConfig()
        {
            ModItem.Options.Clear();
            foreach (Option opt in OptionList)
            {
                foreach (BodyMesh aBodyMesh in opt.BodyMeshes)
                {
                    if (aBodyMesh.MeshType == BodyMeshType.Torso)
                    {
                        // null Torso body mesh default is the same as default
                        if (aBodyMesh.IsNull)
                        {
                            aBodyMesh.IsNull = false;
                            aBodyMesh.IsDefault = true;
                        }
                    }
                }
                ModItem.Options.Add(opt);
            }

            return ModItem;
        }

        public String GetPreview()
        {
            return ModItem.Preview;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Option currentOption = optionsBox.SelectedItem as Option;
            if (currentOption != null)
            {
                addOption(currentOption.Clone());
            }
        }

        private void addOptionButton_Click(object sender, EventArgs e)
        {
            addOption(new Option());
        }

        private void addOption(Option newOpt)
        {
            switch (ModItem.ItemCategory)
            {
                case Category.Panty:
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.Torso));
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.Groin));
                    break;
                case Category.Bra:
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.Breast));
                    break;
                case Category.Gloves:
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.Arm));
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.FingerNail));
                    break;
                case Category.Shoes:
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.Leg));
                    newOpt.BodyMeshes.Add(new BodyMesh(BodyMeshType.ToeNail));
                    break;
            }
            newOpt.Name = "Option_" + (OptionList.Count + 1);
            OptionList.Add(newOpt);
            this.optionsBox.SelectedItem = newOpt;
            UpdateDisplayForItem();
        }

        private void removeOptionButton_Click(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                int index = this.optionsBox.SelectedIndex;
                OptionList.RemoveAt(index);
                this.optionsBox.Items.Remove(selectedOption);
            }
            UpdateDisplayForItem();
        }

        private void optionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplayForItem();
        }

        private void EnsureBodyMesh(Option currentOption, BodyMeshType requiredBodyMeshType)
        {
            foreach (BodyMesh aBodyMesh in currentOption.BodyMeshes)
            {
                if (aBodyMesh.MeshType == requiredBodyMeshType)
                {
                    return;
                }
            }

            // option is missing an expected Body Mesh
            currentOption.BodyMeshes.Add(new BodyMesh(requiredBodyMeshType));
        }

        private void UpdateDisplayForItem()
        {
            this.textBoxName.Text = "";
            this.buttonAddSolid.Enabled = false;
            this.buttonAddAlpha.Enabled = false;
            while (this.tableLayoutPanel1.Controls.Count > 2)
            {
                this.tableLayoutPanel1.Controls.RemoveAt(2);
                this.tableLayoutPanel1.RowStyles.RemoveAt(2);
                this.tableLayoutPanel1.RowCount -= 1;
            }

            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                this.textBoxName.Text = selectedOption.Name;
                this.buttonAddSolid.Enabled = true;
                this.buttonAddAlpha.Enabled = true;

                BodyMesh skinBodyMesh = null;
                BodyMesh extraBodyMesh = null;

                // Validate to see if missing meshes.
                switch(ModItem.ItemCategory)
                {
                    case Category.Panty:
                        EnsureBodyMesh(selectedOption, BodyMeshType.Torso);
                        EnsureBodyMesh(selectedOption, BodyMeshType.Groin);
                        break;
                    case Category.Bra:
                        EnsureBodyMesh(selectedOption, BodyMeshType.Breast);
                        break;
                    case Category.Gloves:
                        EnsureBodyMesh(selectedOption, BodyMeshType.Arm);
                        EnsureBodyMesh(selectedOption, BodyMeshType.FingerNail);
                        break;
                    case Category.Shoes:
                        EnsureBodyMesh(selectedOption, BodyMeshType.Leg);
                        EnsureBodyMesh(selectedOption, BodyMeshType.ToeNail);
                        break;
                }

                foreach (BodyMesh bMesh in selectedOption.BodyMeshes)
                {
                    switch (bMesh.MeshType)
                    {
                        case BodyMeshType.Torso:
                            extraBodyMesh = bMesh;
                            this.resourceNailIB.SetBindingList(ref ModPackGui.chestIBList);
                            this.resourceNailVB.SetBindingList(ref ModPackGui.chestVBList);
                            break;
                        case BodyMeshType.Breast:
                            skinBodyMesh = bMesh;
                            this.resourceSkinIB.SetBindingList(ref ModPackGui.breastIBList);
                            this.resourceSkinVB.SetBindingList(ref ModPackGui.breastVBList);
                            break;
                        case BodyMeshType.Groin:
                            skinBodyMesh = bMesh;
                            this.resourceSkinIB.SetBindingList(ref ModPackGui.groinIBList);
                            this.resourceSkinVB.SetBindingList(ref ModPackGui.groinVBList);
                            break;
                        case BodyMeshType.Arm:
                            skinBodyMesh = bMesh;
                            this.resourceSkinIB.SetBindingList(ref ModPackGui.armIBList);
                            this.resourceSkinVB.SetBindingList(ref ModPackGui.armVBList);
                            break;
                        case BodyMeshType.Leg:
                            skinBodyMesh = bMesh;
                            this.resourceSkinIB.SetBindingList(ref ModPackGui.legIBList);
                            this.resourceSkinVB.SetBindingList(ref ModPackGui.legVBList);
                            break;
                        case BodyMeshType.FingerNail:
                            extraBodyMesh = bMesh;
                            this.resourceNailIB.SetBindingList(ref ModPackGui.fingerNailIBList);
                            this.resourceNailVB.SetBindingList(ref ModPackGui.fingerNailVBList);
                            break;
                        case BodyMeshType.ToeNail:
                            extraBodyMesh = bMesh;
                            this.resourceNailIB.SetBindingList(ref ModPackGui.toeNailIBList);
                            this.resourceNailVB.SetBindingList(ref ModPackGui.toeNailVBList);
                            break;
                    }
                }

                this.SuspendLayout();

                this.resourceNailIB.Clear();
                this.resourceNailVB.Clear();
                this.textBoxNailStride.Text = "";
                this.comboNailFormat.SelectedItem = "";

                if (extraBodyMesh != null)
                {
                    this.nailLabel.Text = extraBodyMesh.MeshType.ToString();
                    this.nailGroup.Text = extraBodyMesh.MeshType.ToString();

                    if (!extraBodyMesh.IsDefault && !extraBodyMesh.IsNull)
                    {
                        this.radioButtonNailCustom.Select();

                        this.resourceNailIB.Enabled = true;
                        this.resourceNailVB.Enabled = true;
                        this.textBoxNailStride.Enabled = true;
                        this.comboNailFormat.Enabled = true;
                        this.nailSkinButton.Enabled = true;

                        this.resourceNailIB.SetValue(extraBodyMesh.IndexBuffer);
                        this.resourceNailVB.SetValue(extraBodyMesh.VertexBuffer);
                        this.comboNailFormat.SelectedItem = extraBodyMesh.Format;
                        if (extraBodyMesh.Stride != 0)
                        {
                            this.textBoxNailStride.Text = extraBodyMesh.Stride.ToString();
                        }
                    }
                    else
                    {
                        if (extraBodyMesh.IsNull)
                        {
                            this.radioButtonNailOff.Select();
                        }
                        else
                        {
                            this.radioButtonNailDefault.Select();
                        }

                        this.resourceNailIB.Enabled = false;
                        this.resourceNailVB.Enabled = false;
                        this.textBoxNailStride.Enabled = false;
                        this.comboNailFormat.Enabled = false;
                        this.nailSkinButton.Enabled = false;
                    }
                }
                else
                {
                    this.nailGroup.Visible = false;
                }

                this.resourceSkinIB.Clear();
                this.resourceSkinVB.Clear();
                this.textBoxBodyStride.Text = "";
                this.comboBoxBodyFormat.SelectedItem = "";

                if (skinBodyMesh != null)
                {
                    this.bodyGroup.Visible = true;

                    this.labelBody.Text = skinBodyMesh.MeshType.ToString();
                    this.bodyGroup.Text = skinBodyMesh.MeshType.ToString();

                    if (!skinBodyMesh.IsDefault && !skinBodyMesh.IsNull)
                    {
                        this.radioButtonBodyCustom.Select();

                        this.resourceSkinIB.Enabled = true;
                        this.resourceSkinVB.Enabled = true;
                        this.textBoxBodyStride.Enabled = true;
                        this.comboBoxBodyFormat.Enabled = true;
                        this.bodySkinButton.Enabled = true;

                        this.resourceSkinIB.SetValue(skinBodyMesh.IndexBuffer);
                        this.resourceSkinVB.SetValue(skinBodyMesh.VertexBuffer);
                        this.comboBoxBodyFormat.SelectedItem = skinBodyMesh.Format;
                        if (skinBodyMesh.Stride != 0)
                        {
                            this.textBoxBodyStride.Text = skinBodyMesh.Stride.ToString();
                        }
                    }
                    else
                    {
                        if (skinBodyMesh.IsNull)
                        {
                            this.radioButtonBodyOff.Select();
                        }
                        else
                        {
                            this.radioButtonBodyDefault.Select();
                        }

                        this.resourceSkinIB.Enabled = false;
                        this.resourceSkinVB.Enabled = false;
                        this.textBoxBodyStride.Enabled = false;
                        this.comboBoxBodyFormat.Enabled = false;
                        this.bodySkinButton.Enabled = false;
                    }
                }
                else
                {
                    this.bodyGroup.Visible = false;
                }

                foreach (MeshSlot solidSlots in selectedOption.SolidMeshes)
                {
                    MeshBuilder meshBuilder = new MeshBuilder();
                    meshBuilder.Dock = DockStyle.Top;
                    meshBuilder.SetMeshSlot(solidSlots);
                    meshBuilder.meshSlotType = MeshBuilder.MeshSlotType.Solid;
                    meshBuilder.MeshSlotUpdated = this.UpdatedMeshSlot;
                    meshBuilder.RemoveClicked = this.buttonRemoveMeshSlot;
                    meshBuilder.SetMeshSlotTitle("Solid Slot");

                    this.tableLayoutPanel1.RowCount++;
                    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    this.tableLayoutPanel1.Controls.Add(meshBuilder);
                }
                foreach (MeshSlot alphaSlots in selectedOption.AlphaMeshes)
                {
                    MeshBuilder meshBuilder = new MeshBuilder();
                    meshBuilder.Dock = DockStyle.Top;
                    meshBuilder.SetMeshSlot(alphaSlots);
                    meshBuilder.meshSlotType = MeshBuilder.MeshSlotType.Alpha;
                    meshBuilder.MeshSlotUpdated = this.UpdatedMeshSlot;
                    meshBuilder.RemoveClicked = this.buttonRemoveMeshSlot;
                    meshBuilder.SetMeshSlotTitle("Alpha Slot");

                    this.tableLayoutPanel1.RowCount++;
                    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    this.tableLayoutPanel1.Controls.Add(meshBuilder);
                }

                this.ResumeLayout();

                this.selectionDefinitionSplits.Panel2.Enabled = true;
            }
            else
            {
                this.selectionDefinitionSplits.Panel2.Enabled = false;
            }
        }

        private void textBoxIcon_Leave(object sender, EventArgs e)
        {
            UpdateIcon();
        }

        private void textBoxPreview_Leave(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void UpdateIcon()
        {
            ModItem.Icon = this.textBoxIcon.Text;
            this.iconGraphic.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, this.textBoxIcon.Text);
        }

        private void UpdatePreview()
        {
            ModItem.Preview = this.textBoxPreview.Text;
            this.previewGraphic.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, this.textBoxPreview.Text);
        }

        private String browseLocalResource()
        {
            openFileDialog1.Filter = "PNG (*.png)|*.png|DDS (*.dds)|*.dds";
            openFileDialog1.InitialDirectory = ModPackGui.WorkspaceDirectory;

            DialogResult openResult = openFileDialog1.ShowDialog();

            if (openResult == DialogResult.OK)
            {
                String filepath = openFileDialog1.FileName;
                if (!filepath.StartsWith(ModPackGui.WorkspaceDirectory))
                {
                    String filename = Path.GetFileName(filepath);
                    System.IO.File.Copy(filepath, Path.Combine(ModPackGui.WorkspaceDirectory, filename));
                    filepath = filename;
                }
                else
                {
                    filepath = filepath.Replace(ModPackGui.WorkspaceDirectory, "");
                    char[] removeChars = { '/', '\\' };
                    filepath = filepath.TrimStart(removeChars);
                }
                return filepath;
            }
            return null;
        }

        private void iconBrowse_Click(object sender, EventArgs e)
        {
            String newIcon = browseLocalResource();
            if (newIcon != null)
            {
                textBoxIcon.Text = newIcon;
                UpdateIcon();
            }
        }

        private void previewBrowse_Click(object sender, EventArgs e)
        {
            String newPreview = browseLocalResource();
            if (newPreview != null)
            {
                textBoxPreview.Text = newPreview;
                UpdatePreview();
            }
        }

        private void buttonAddAlpha_Click(object sender, EventArgs e)
        {
            Option currentOption = optionsBox.SelectedItem as Option;

            if (currentOption != null)
            {
                if (currentOption.AlphaMeshes.Count < 3)
                {
                    this.SuspendLayout();
                    MeshSlot alphaMesh = new MeshSlot();
                    currentOption.AlphaMeshes.Add(alphaMesh);

                    MeshBuilder meshBuilder = new MeshBuilder();
                    meshBuilder.Dock = DockStyle.Top;
                    meshBuilder.SetMeshSlot(alphaMesh);
                    meshBuilder.meshSlotType = MeshBuilder.MeshSlotType.Alpha;
                    meshBuilder.MeshSlotUpdated = this.UpdatedMeshSlot;
                    meshBuilder.RemoveClicked = this.buttonRemoveMeshSlot;
                    meshBuilder.SetMeshSlotTitle("Alpha Slot");

                    this.tableLayoutPanel1.RowCount++;
                    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    this.tableLayoutPanel1.Controls.Add(meshBuilder);
                    meshBuilder.Invalidate(false);
                    this.ResumeLayout();
                }
                else
                {
                    MessageBox.Show("Cannot have more than 3 Alpha Mesh Pieces.", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonAddSolid_Click(object sender, EventArgs e)
        {
            Option currentOption = optionsBox.SelectedItem as Option;

            if (currentOption != null)
            {
                if (currentOption.SolidMeshes.Count < 4)
                {
                    this.SuspendLayout();
                    MeshSlot solidMesh = new MeshSlot();
                    currentOption.SolidMeshes.Add(solidMesh);

                    MeshBuilder meshBuilder = new MeshBuilder();
                    meshBuilder.Dock = DockStyle.Top;
                    meshBuilder.SetMeshSlot(solidMesh);
                    meshBuilder.meshSlotType = MeshBuilder.MeshSlotType.Solid;
                    meshBuilder.MeshSlotUpdated = this.UpdatedMeshSlot;
                    meshBuilder.RemoveClicked = this.buttonRemoveMeshSlot;
                    meshBuilder.SetMeshSlotTitle("Solid Slot");

                    this.tableLayoutPanel1.RowCount++;
                    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    this.tableLayoutPanel1.Controls.Add(meshBuilder);
                    meshBuilder.Invalidate(false);
                    this.ResumeLayout();
                }
                else
                {
                    MessageBox.Show("Cannot have more than 4 Solid Mesh Pieces.", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonRemoveMeshSlot(object sender, EventArgs e)
        {
            MeshBuilder builder = sender as MeshBuilder;

            if (builder != null)
            {
                int currentIndex = 0;
                int i = 0;
                for (i = 0; i < this.tableLayoutPanel1.Controls.Count; i++)
                {
                    MeshBuilder testMeshBuilder = this.tableLayoutPanel1.Controls[i] as MeshBuilder;
                    if (testMeshBuilder != null)
                    {
                        if (testMeshBuilder == builder)
                        {
                            break;
                        }

                        if (testMeshBuilder.meshSlotType == builder.meshSlotType)
                        {
                            currentIndex++;
                        }
                    }
                }

                // currentIndex is the currentIndex in the respective array
                Option currentOption = this.optionsBox.SelectedItem as Option;
                if (currentOption != null)
                {
                    this.SuspendLayout();
                    if (builder.meshSlotType == MeshBuilder.MeshSlotType.Alpha)
                    {
                        currentOption.AlphaMeshes.RemoveAt(currentIndex);
                    }
                    else
                    {
                        currentOption.SolidMeshes.RemoveAt(currentIndex);
                    }
                    this.tableLayoutPanel1.RowStyles.RemoveAt(i);
                    this.tableLayoutPanel1.Controls.Remove(builder);
                    this.tableLayoutPanel1.RowCount -= 1;
                    this.ResumeLayout();
                }
            }
        }

        private void UpdatedMeshSlot(object sender, MeshBuilder.MeshSlotParameterEventArgs e)
        {
            MeshBuilder meshBuilder = sender as MeshBuilder;
            if (meshBuilder != null)
            {
                MeshBuilder.MeshSlotType slotType = meshBuilder.meshSlotType;

                int currentIndex = 0;
                int i = 0;
                for (i = 0; i < this.tableLayoutPanel1.Controls.Count; i++)
                {
                    MeshBuilder testMeshBuilder = this.tableLayoutPanel1.Controls[i] as MeshBuilder;
                    if (testMeshBuilder != null)
                    {
                        if (testMeshBuilder == meshBuilder)
                        {
                            break;
                        }

                        if (testMeshBuilder.meshSlotType == meshBuilder.meshSlotType)
                        {
                            currentIndex++;
                        }
                    }
                }

                Option currentOption = this.optionsBox.SelectedItem as Option;
                MeshSlot targetMeshSlot = null;

                if (slotType == MeshBuilder.MeshSlotType.Alpha)
                {
                    targetMeshSlot = currentOption.AlphaMeshes[currentIndex];
                }
                else
                {
                    targetMeshSlot = currentOption.SolidMeshes[currentIndex];
                }

                if (targetMeshSlot != null)
                {
                    switch (e.meshSlotParam)
                    {
                        case MeshBuilder.MeshSlotParameter.IB:
                            targetMeshSlot.NormalMesh.IndexBuffer = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.IB_FORMAT:
                            targetMeshSlot.NormalMesh.Format = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.VB:
                            targetMeshSlot.NormalMesh.VertexBuffer = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.VB_STRIDE:
                            try
                            {
                                if (String.IsNullOrEmpty(e.value))
                                {
                                    targetMeshSlot.NormalMesh.Stride = null;
                                }
                                else
                                {
                                    targetMeshSlot.NormalMesh.Stride = UInt32.Parse(e.value);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid number for Stride", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                            break;
                        case MeshBuilder.MeshSlotParameter.SHADOW:
                            if (String.IsNullOrEmpty(e.value))
                            {
                                // NO Shadow
                                targetMeshSlot.ShadowMesh.IsNull = true;
                                targetMeshSlot.ShadowMesh.IsDefault = false;

                                targetMeshSlot.ShadowMesh.VertexBuffer = null;
                                targetMeshSlot.ShadowMesh.Stride = null;
                                targetMeshSlot.ShadowMesh.IndexBuffer = null;
                                targetMeshSlot.ShadowMesh.Format = null;
                            }
                            else if ("DEFAULT".Equals(e.value, StringComparison.OrdinalIgnoreCase))
                            {
                                // DEFAULT SHADOW
                                targetMeshSlot.ShadowMesh.IsNull = false;
                                targetMeshSlot.ShadowMesh.IsDefault = true;

                                targetMeshSlot.ShadowMesh.VertexBuffer = null;
                                targetMeshSlot.ShadowMesh.Stride = null;
                                targetMeshSlot.ShadowMesh.IndexBuffer = null;
                                targetMeshSlot.ShadowMesh.Format = null;
                            }
                            else
                            {
                                targetMeshSlot.ShadowMesh.IsNull = false;
                                targetMeshSlot.ShadowMesh.IsDefault = false;
                            }
                            break;
                        case MeshBuilder.MeshSlotParameter.SIB:
                            targetMeshSlot.ShadowMesh.IndexBuffer = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.SIB_FORMAT:
                            targetMeshSlot.ShadowMesh.Format = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.SVB:
                            targetMeshSlot.ShadowMesh.VertexBuffer = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.SVB_STRIDE:
                            try
                            {
                                if (String.IsNullOrEmpty(e.value))
                                {
                                    targetMeshSlot.ShadowMesh.Stride = null;
                                }
                                else
                                {
                                    targetMeshSlot.ShadowMesh.Stride = UInt32.Parse(e.value);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid number for Stride", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case MeshBuilder.MeshSlotParameter.PS0:
                            targetMeshSlot.PS0Texture = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.PS1:
                            targetMeshSlot.PS1Texture = e.value;
                            break;
                        case MeshBuilder.MeshSlotParameter.PS2:
                            targetMeshSlot.PS2Texture = e.value;
                            break;
                    }
                }
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                selectedOption.Name = this.textBoxName.Text;
                this.optionsBox.Refresh();
            }
        }

        private void radioButtonBodyDefault_CheckedChanged(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh bodyMesh in selectedOption.BodyMeshes)
                {
                    if (bodyMesh.MeshType == BodyMeshType.Breast || bodyMesh.MeshType == BodyMeshType.Groin || bodyMesh.MeshType == BodyMeshType.Arm || bodyMesh.MeshType == BodyMeshType.Leg)
                    {
                        bodyMesh.IsDefault = true;
                        bodyMesh.IsNull = false;

                        bodyMesh.VertexBuffer = null;
                        bodyMesh.IndexBuffer = null;
                        bodyMesh.Stride = null;
                        bodyMesh.Format = null;
                    }
                }
            }
            this.comboBoxBodyFormat.Enabled = false;
            this.textBoxBodyStride.Enabled = false;
            this.resourceSkinIB.SetValue("");
            this.resourceSkinIB.Enabled = false;
            this.resourceSkinVB.SetValue("");
            this.resourceSkinVB.Enabled = false;
        }

        private void radioButtonBodyOff_CheckedChanged(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh aBodyMesh in selectedOption.BodyMeshes)
                {
                    if (aBodyMesh.MeshType == BodyMeshType.Breast || aBodyMesh.MeshType == BodyMeshType.Groin || aBodyMesh.MeshType == BodyMeshType.Arm || aBodyMesh.MeshType == BodyMeshType.Leg)
                    {
                        aBodyMesh.IsDefault = false;
                        aBodyMesh.IsNull = true;

                        aBodyMesh.VertexBuffer = null;
                        aBodyMesh.IndexBuffer = null;
                        aBodyMesh.Stride = null;
                        aBodyMesh.Format = null;
                    }
                }
            }
            this.comboBoxBodyFormat.Enabled = false;
            this.textBoxBodyStride.Enabled = false;
            this.resourceSkinIB.SetValue("");
            this.resourceSkinIB.Enabled = false;
            this.resourceSkinVB.SetValue("");
            this.resourceSkinVB.Enabled = false;
        }

        private void radioButtonBodyCustom_CheckedChanged(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh bodyMesh in selectedOption.BodyMeshes)
                {
                    if (bodyMesh.MeshType == BodyMeshType.Breast || bodyMesh.MeshType == BodyMeshType.Groin || bodyMesh.MeshType == BodyMeshType.Arm || bodyMesh.MeshType == BodyMeshType.Leg)
                    {
                        bodyMesh.IsDefault = false;
                        bodyMesh.IsNull = false;
                    }
                }
            }
            this.comboBoxBodyFormat.Enabled = true;
            this.textBoxBodyStride.Enabled = true;
            this.resourceSkinIB.Enabled = true;
            this.resourceSkinVB.Enabled = true;
        }

        private void radioButtonNailDefault_CheckedChanged(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh nailMesh in selectedOption.BodyMeshes)
                {
                    if (nailMesh.MeshType == BodyMeshType.Torso || nailMesh.MeshType == BodyMeshType.FingerNail || nailMesh.MeshType == BodyMeshType.ToeNail)
                    {
                        nailMesh.IsDefault = true;
                        nailMesh.IsNull = false;

                        nailMesh.VertexBuffer = null;
                        nailMesh.IndexBuffer = null;
                        nailMesh.Stride = null;
                        nailMesh.Format = null;
                    }
                }
            }
            this.comboNailFormat.Enabled = false;
            this.textBoxNailStride.Enabled = false;
            this.resourceNailIB.SetValue("");
            this.resourceNailIB.Enabled = false;
            this.resourceNailVB.SetValue("");
            this.resourceNailVB.Enabled = false;
        }

        private void radioButtonNailOff_CheckedChanged(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh nailMesh in selectedOption.BodyMeshes)
                {
                    if (nailMesh.MeshType == BodyMeshType.Torso || nailMesh.MeshType == BodyMeshType.FingerNail || nailMesh.MeshType == BodyMeshType.ToeNail)
                    {
                        nailMesh.IsDefault = false;
                        nailMesh.IsNull = true;

                        nailMesh.VertexBuffer = null;
                        nailMesh.IndexBuffer = null;
                        nailMesh.Stride = null;
                        nailMesh.Format = null;
                    }
                }
            }
            this.comboNailFormat.Enabled = false;
            this.textBoxNailStride.Enabled = false;
            this.resourceNailIB.SetValue("");
            this.resourceNailIB.Enabled = false;
            this.resourceNailVB.SetValue("");
            this.resourceNailVB.Enabled = false;
        }

        private void radioButtonNailCustom_CheckedChanged(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh nailMesh in selectedOption.BodyMeshes)
                {
                    if (nailMesh.MeshType == BodyMeshType.Torso || nailMesh.MeshType == BodyMeshType.FingerNail || nailMesh.MeshType == BodyMeshType.ToeNail)
                    {
                        nailMesh.IsDefault = false;
                        nailMesh.IsNull = false;
                    }
                }
            }
            this.comboNailFormat.Enabled = true;
            this.textBoxNailStride.Enabled = true;
            this.resourceNailIB.Enabled = true;
            this.resourceNailVB.Enabled = true;
        }

        private void comboBoxBodyFormat_Leave(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh bodyMesh in selectedOption.BodyMeshes)
                {
                    if (bodyMesh.MeshType == BodyMeshType.Breast || bodyMesh.MeshType == BodyMeshType.Groin || bodyMesh.MeshType == BodyMeshType.Arm || bodyMesh.MeshType == BodyMeshType.Leg)
                    {
                        bodyMesh.Format = this.comboBoxBodyFormat.Text;
                        ModPackGui.CacheString(bodyMesh.Format, ref ModPackGui.formatList);
                    }
                }
            }
        }

        private void textBoxBodyStride_Leave(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh bodyMesh in selectedOption.BodyMeshes)
                {
                    if (bodyMesh.MeshType == BodyMeshType.Breast || bodyMesh.MeshType == BodyMeshType.Groin || bodyMesh.MeshType == BodyMeshType.Arm || bodyMesh.MeshType == BodyMeshType.Leg)
                    {
                        try
                        {
                            if (String.IsNullOrEmpty(textBoxBodyStride.Text))
                            {
                                bodyMesh.Stride = null;
                            }
                            else
                            {
                                bodyMesh.Stride = UInt32.Parse(this.textBoxBodyStride.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid number for Stride", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void comboNailFormat_Leave(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh nailMesh in selectedOption.BodyMeshes)
                {
                    if (nailMesh.MeshType == BodyMeshType.Torso || nailMesh.MeshType == BodyMeshType.FingerNail || nailMesh.MeshType == BodyMeshType.ToeNail)
                    {
                        nailMesh.Format = this.comboNailFormat.Text;
                        ModPackGui.CacheString(nailMesh.Format, ref ModPackGui.formatList);
                    }
                }
            }
        }

        private void textBoxNailStride_Leave(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh nailMesh in selectedOption.BodyMeshes)
                {
                    if (nailMesh.MeshType == BodyMeshType.Torso || nailMesh.MeshType == BodyMeshType.FingerNail || nailMesh.MeshType == BodyMeshType.ToeNail)
                    {
                        try
                        {
                            if (String.IsNullOrEmpty(textBoxNailStride.Text))
                            {
                                nailMesh.Stride = null;
                            }
                            else
                            {
                                nailMesh.Stride = UInt32.Parse(this.textBoxNailStride.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid number for Stride", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void MeshSlotFilenameUpdated(object sender, ResourceBuilder.StringEventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            if (selectedOption != null)
            {
                foreach (BodyMesh bodyMesh in selectedOption.BodyMeshes)
                {
                    switch (bodyMesh.MeshType)
                    {
                        case BodyMeshType.Torso:
                            if (sender == resourceNailIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.chestIBList);
                            }
                            else if (sender == resourceNailVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.chestVBList);
                            }
                            break;
                        case BodyMeshType.Breast:
                            if (sender == resourceSkinIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.breastIBList);
                            }
                            else if (sender == resourceSkinVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.breastVBList);
                            }
                            break;
                        case BodyMeshType.Groin:
                            if (sender == resourceSkinIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.groinIBList);
                            }
                            else if (sender == resourceSkinVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.groinVBList);
                            }
                            break;
                        case BodyMeshType.Arm:
                            if (sender == resourceSkinIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.armIBList);
                            }
                            else if (sender == resourceSkinVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.armVBList);
                            }
                            break;
                        case BodyMeshType.Leg:
                            if (sender == resourceSkinIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.legIBList);
                            }
                            else if (sender == resourceSkinVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.legVBList);
                            }
                            break;
                        case BodyMeshType.FingerNail:
                            if (sender == resourceNailIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.fingerNailIBList);
                            }
                            else if (sender == resourceNailVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.fingerNailVBList);
                            }
                            break;
                        case BodyMeshType.ToeNail:
                            if (sender == resourceNailIB)
                            {
                                bodyMesh.IndexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.IndexBuffer, ref ModPackGui.toeNailIBList);
                            }
                            else if (sender == resourceNailVB)
                            {
                                bodyMesh.VertexBuffer = e.value;
                                ModPackGui.CacheString(bodyMesh.VertexBuffer, ref ModPackGui.toeNailVBList);
                            }
                            break;
                    }
                }
            }
        }

        private void bodySkinButton_Click(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            foreach (BodyMesh bMesh in selectedOption.BodyMeshes)
            {
                switch (bMesh.MeshType)
                {
                    case BodyMeshType.Breast:
                    case BodyMeshType.Groin:
                    case BodyMeshType.Arm:
                    case BodyMeshType.Leg:
                        SkinCustomizer skinCustomizer = new SkinCustomizer(bMesh, currentModelType);
                        skinCustomizer.ShowDialog();
                        bMesh.TanTexture = skinCustomizer.TanTexture;
                        bMesh.SkinTextures.Clear();
                        foreach (KeyValuePair<String, String> textureSlots in skinCustomizer.SkinTextures)
                        {
                            SkinTexture newSkinTexture = new SkinTexture();
                            newSkinTexture.SkinSlot = textureSlots.Key;
                            newSkinTexture.Filename = textureSlots.Value;
                            bMesh.SkinTextures.Add(newSkinTexture);
                        }
                        break;
                }
            }
        }

        private void nailSkinButton_Click(object sender, EventArgs e)
        {
            Option selectedOption = this.optionsBox.SelectedItem as Option;
            foreach (BodyMesh bMesh in selectedOption.BodyMeshes)
            {
                switch (bMesh.MeshType)
                {
                    case BodyMeshType.Torso:
                    case BodyMeshType.FingerNail:
                    case BodyMeshType.ToeNail:
                        SkinCustomizer skinCustomizer = new SkinCustomizer(bMesh, currentModelType);
                        skinCustomizer.ShowDialog();
                        bMesh.TanTexture = skinCustomizer.TanTexture;
                        bMesh.SkinTextures.Clear();
                        foreach (KeyValuePair<String, String> textureSlots in skinCustomizer.SkinTextures)
                        {
                            SkinTexture newSkinTexture = new SkinTexture();
                            newSkinTexture.SkinSlot = textureSlots.Key;
                            newSkinTexture.Filename = textureSlots.Value;
                            bMesh.SkinTextures.Add(newSkinTexture);
                        }
                        break;
                }
            }
        }
    }
}
