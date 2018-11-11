using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCModConfig;
using System.IO;

namespace CCModPackGenerator
{
    public partial class CostumeBuilder : UserControl
    {
        public class ItemPresetOption : INotifyPropertyChanged
        {
            public int Value { get; set; }
            private String name;

            public String Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                    NodityPropertyChanged("Name");
                }
            }

            public ItemPresetOption(int value, String name)
            {
                this.Value = value;
                this.Name = name;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NodityPropertyChanged(String name)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private CCModConfig.CharacterProject CharacterConfig { get; set; }

        private BindingList<Preset> Presets { get; set; }

        private BindingList<Option> ChestOptionList { get; set; }
        private BindingList<Option> BraOptionList { get; set; }
        private BindingList<Option> PantyOptionList { get; set; }
        private BindingList<Option> SkirtOptionList { get; set; }
        private BindingList<Option> GlovesOptionList { get; set; }
        private BindingList<Option> ShoesOptionList { get; set; }
        private BindingList<Option> Acc1OptionList { get; set; }
        private BindingList<Option> Acc2OptionList { get; set; }
        private BindingList<Option> Acc3OptionList { get; set; }
        private BindingList<Option> Acc4OptionList { get; set; }

        public CostumeBuilder()
        {
            InitializeComponent();

            Presets = new BindingList<Preset>();

            ChestOptionList = new BindingList<Option>();
            BraOptionList = new BindingList<Option>();
            PantyOptionList = new BindingList<Option>();
            SkirtOptionList = new BindingList<Option>();
            GlovesOptionList = new BindingList<Option>();
            ShoesOptionList = new BindingList<Option>();
            Acc1OptionList = new BindingList<Option>();
            Acc2OptionList = new BindingList<Option>();
            Acc3OptionList = new BindingList<Option>();
            Acc4OptionList = new BindingList<Option>();

            this.itemBuilderChest.SetBindingList(ChestOptionList);
            this.itemBuilderBra.SetBindingList(BraOptionList);
            this.itemBuilderPanties.SetBindingList(PantyOptionList);
            this.itemBuilderSkirt.SetBindingList(SkirtOptionList);
            this.itemBuilderGloves.SetBindingList(GlovesOptionList);
            this.itemBuilderShoes.SetBindingList(ShoesOptionList);
            this.itemBuilderAcc1.SetBindingList(Acc1OptionList);
            this.itemBuilderAcc2.SetBindingList(Acc2OptionList);
            this.itemBuilderAcc3.SetBindingList(Acc3OptionList);
            this.itemBuilderAcc4.SetBindingList(Acc4OptionList);
        }

        public void SetConfig(CCModConfig.CharacterProject characterConfig)
        {
            CharacterConfig = characterConfig;

            Item chestItem = new Item(Category.Chest);
            Item braItem = new Item(Category.Bra);
            Item pantyItem = new Item(Category.Panty);
            Item skirtItem = new Item(Category.Skirt);
            Item glovesItem = new Item(Category.Gloves);
            Item shoesItem = new Item(Category.Shoes);
            Item acc1Item = new Item(Category.Accessory1);
            Item acc2Item = new Item(Category.Accessory2);
            Item acc3Item = new Item(Category.Accessory3);
            Item acc4Item = new Item(Category.Accessory4);

            foreach (CCModConfig.Item item in characterConfig.Items)
            {
                switch (item.ItemCategory)
                {
                    case Category.Chest:
                        chestItem = item;
                        break;
                    case Category.Bra:
                        braItem = item;
                        break;
                    case Category.Panty:
                        pantyItem = item;
                        break;
                    case Category.Skirt:
                        skirtItem = item;
                        break;
                    case Category.Gloves:
                        glovesItem = item;
                        break;
                    case Category.Shoes:
                        shoesItem = item;
                        break;
                    case Category.Accessory1:
                        acc1Item = item;
                        break;
                    case Category.Accessory2:
                        acc2Item = item;
                        break;
                    case Category.Accessory3:
                        acc3Item = item;
                        break;
                    case Category.Accessory4:
                        acc4Item = item;
                        break;
                }
            }

            this.itemBuilderChest.SetConfig(chestItem);
            this.itemBuilderBra.SetConfig(braItem);
            this.itemBuilderPanties.SetConfig(pantyItem);
            this.itemBuilderSkirt.SetConfig(skirtItem);
            this.itemBuilderGloves.SetConfig(glovesItem);
            this.itemBuilderShoes.SetConfig(shoesItem);
            this.itemBuilderAcc1.SetConfig(acc1Item);
            this.itemBuilderAcc2.SetConfig(acc2Item);
            this.itemBuilderAcc3.SetConfig(acc3Item);
            this.itemBuilderAcc4.SetConfig(acc4Item);

            Presets.Clear();
            foreach (Preset aPreset in characterConfig.Presets)
            {
                Presets.Add(aPreset);
            }

            textBoxIcon.Text = characterConfig.IconFile;
            UpdateIcon();
            textBoxPreview.Text = characterConfig.PreviewFile;
            UpdatePreview();

            // Combo Boxes must be synced before its bound.
            SyncComboBoxes();

            BindingSource presetBindSource = new BindingSource();
            presetBindSource.DataSource = Presets;
            this.listBox1.DataSource = presetBindSource;
            this.listBox1.DisplayMember = "Name";
        }

        public CCModConfig.CharacterProject UpdateConfig()
        {
            CharacterConfig.Presets.Clear();
            // Update all presets
            foreach (Preset preset in Presets)
            {
                CharacterConfig.Presets.Add(preset);
            }

            CharacterConfig.Items.Clear();
            // Update all Items
            CharacterConfig.Items.Add(this.itemBuilderChest.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderBra.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderPanties.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderSkirt.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderGloves.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderShoes.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderAcc1.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderAcc2.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderAcc3.UpdateConfig());
            CharacterConfig.Items.Add(this.itemBuilderAcc4.UpdateConfig());

            return CharacterConfig;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["presetPage"])
            {
                SyncComboBoxes();
            }
        }

        private void SyncComboBoxes()
        {
            // Reload Text for combo boxes
            SyncComboBox(this.ChestOptionList, this.comboBoxChest);
            SyncComboBox(this.BraOptionList, this.comboBoxBra);
            SyncComboBox(this.PantyOptionList, this.comboBoxPanty);
            SyncComboBox(this.SkirtOptionList, this.comboBoxSkirt);
            SyncComboBox(this.GlovesOptionList, this.comboBoxGloves);
            SyncComboBox(this.ShoesOptionList, this.comboBoxShoes);
            SyncComboBox(this.Acc1OptionList, this.comboBoxAcc1);
            SyncComboBox(this.Acc2OptionList, this.comboBoxAcc2);
            SyncComboBox(this.Acc3OptionList, this.comboBoxAcc3);
            SyncComboBox(this.Acc4OptionList, this.comboBoxAcc4);
        }

        private void SyncComboBox(BindingList<Option> optionList, ComboBox control)
        {
            int selectedIndex = control.SelectedIndex;
            control.SelectedIndex = -1;

            control.Items.Clear();
            control.Items.Add("None");
            control.Items.Add("1");
            control.Items.Add("2");
            control.Items.Add("3");
            control.Items.Add("4");
            control.Items.Add("5");
            control.Items.Add("6");
            control.Items.Add("7");
            control.Items.Add("8");
            control.Items.Add("9");
            control.Items.Add("10");
            control.Items.Add("11");
            control.Items.Add("12");

            for (int i = 0; i < optionList.Count; i++)
            {
                int j = i + 1;
                if (j < control.Items.Count)
                {
                    control.Items[j] = j + " - " + optionList[i].Name;
                }
            }

            control.Refresh();
            control.SelectedIndex = selectedIndex;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            Preset currentPreset = this.listBox1.SelectedItem as Preset;

            if (currentPreset != null)
            {
                this.splitContainer2.Panel1.Enabled = true;
                textBoxPresetName.Text = currentPreset.Name;

                foreach (PresetOption option in currentPreset.PresetOptions)
                {
                    switch (option.PresetItem)
                    {
                        case Category.Chest:
                            if (comboBoxChest.Items.Count > option.Option)
                            {
                                comboBoxChest.SelectedIndex = (int) option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Bra:
                            if (comboBoxBra.Items.Count > option.Option)
                            {
                                comboBoxBra.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Panty:
                            if (comboBoxPanty.Items.Count > option.Option)
                            {
                                comboBoxPanty.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Skirt:
                            if (comboBoxSkirt.Items.Count > option.Option)
                            {
                                comboBoxSkirt.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Gloves:
                            if (comboBoxGloves.Items.Count > option.Option)
                            {
                                comboBoxGloves.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Shoes:
                            if (comboBoxShoes.Items.Count > option.Option)
                            {
                                comboBoxShoes.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Accessory1:
                            if (comboBoxAcc1.Items.Count > option.Option)
                            {
                                comboBoxAcc1.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Accessory2:
                            if (comboBoxAcc2.Items.Count > option.Option)
                            {
                                comboBoxAcc2.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Accessory3:
                            if (comboBoxAcc3.Items.Count > option.Option)
                            {
                                comboBoxAcc3.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                        case Category.Accessory4:
                            if (comboBoxAcc4.Items.Count > option.Option)
                            {
                                comboBoxAcc4.SelectedIndex = (int)option.Option;
                            }
                            else
                            {
                                option.Option = 0;
                            }
                            break;
                    }
                }
            }
            else
            {
                comboBoxChest.SelectedIndex = -1;
                comboBoxBra.SelectedIndex = -1;
                comboBoxPanty.SelectedIndex = -1;
                comboBoxSkirt.SelectedIndex = -1;
                comboBoxGloves.SelectedIndex = -1;
                comboBoxShoes.SelectedIndex = -1;
                comboBoxAcc1.SelectedIndex = -1;
                comboBoxAcc2.SelectedIndex = -1;
                comboBoxAcc3.SelectedIndex = -1;
                comboBoxAcc4.SelectedIndex = -1;
                textBoxPresetName.Text = "";

                this.splitContainer2.Panel1.Enabled = false;
            }
        }

        private void comboBox_Select(object sender, EventArgs e)
        {
            Preset currentPreset = this.listBox1.SelectedItem as Preset;

            if (currentPreset != null)
            {
                foreach (PresetOption option in currentPreset.PresetOptions)
                {
                    if (option.PresetItem == Category.Chest && sender == comboBoxChest)
                    {
                        option.Option = comboBoxChest.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Bra && sender == comboBoxBra)
                    {
                        option.Option = comboBoxBra.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Panty && sender == comboBoxPanty)
                    {
                        option.Option = comboBoxPanty.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Skirt && sender == comboBoxSkirt)
                    {
                        option.Option = comboBoxSkirt.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Gloves && sender == comboBoxGloves)
                    {
                        option.Option = comboBoxGloves.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Shoes && sender == comboBoxShoes)
                    {
                        option.Option = comboBoxShoes.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Accessory1 && sender == comboBoxAcc1)
                    {
                        option.Option = comboBoxAcc1.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Accessory2 && sender == comboBoxAcc2)
                    {
                        option.Option = comboBoxAcc2.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Accessory3 && sender == comboBoxAcc3)
                    {
                        option.Option = comboBoxAcc3.SelectedIndex;
                    }
                    else if (option.PresetItem == Category.Accessory4 && sender == comboBoxAcc4)
                    {
                        option.Option = comboBoxAcc4.SelectedIndex;
                    }
                }
            }
        }

        private void comboBoxChest_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderChest.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxBra_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderBra.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxPanty_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderPanties.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxSkirt_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderSkirt.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxGloves_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderGloves.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxShoes_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderShoes.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxAcc1_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderAcc1.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxAcc2_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderAcc2.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxAcc3_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderAcc3.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
            }
        }

        private void comboBoxAcc4_DropDown(object sender, EventArgs e)
        {
            String preview = itemBuilderAcc4.GetPreview();
            if (preview != null)
            {
                this.pictureBoxItemPreview.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, preview);
            }
            else
            {
                this.pictureBoxItemPreview.ImageLocation = "";
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

        private void UpdateIcon()
        {
            CharacterConfig.IconFile = this.textBoxIcon.Text;
            this.iconGraphic.ImageLocation = Path.Combine(ModPackGui.WorkspaceDirectory, this.textBoxIcon.Text);
        }

        private void UpdatePreview()
        {
            CharacterConfig.PreviewFile = this.textBoxPreview.Text;
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

        private void buttonNewPreset_Click(object sender, EventArgs e)
        {
            Preset newPreset = new Preset();
            foreach (Category aCategory in Enum.GetValues(typeof(Category)))
            {
                PresetOption newPresetOption = new PresetOption();
                newPresetOption.PresetItem = aCategory;
                newPresetOption.Option = 0;
                newPreset.PresetOptions.Add(newPresetOption);
            }
            AddPreset(newPreset);
        }

        private void buttonDuplicatePreset_Click(object sender, EventArgs e)
        {
            Preset currentPreset = this.listBox1.SelectedItem as Preset;
            if (currentPreset != null)
            {
                AddPreset(currentPreset.Clone());
            }
        }

        private void AddPreset(Preset newPreset)
        {
            newPreset.Name = "Preset " + (Presets.Count + 1);
            Presets.Add(newPreset);
            this.listBox1.SelectedItem = newPreset;
            UpdateDisplay();
        }

        private void buttonDeletePreset_Click(object sender, EventArgs e)
        {
            Preset selectedPreset = this.listBox1.SelectedItem as Preset;
            if (selectedPreset != null)
            {
                int index = this.listBox1.SelectedIndex;
                Presets.RemoveAt(index);
                this.listBox1.Items.Remove(selectedPreset);
            }
            UpdateDisplay();
        }

        private void textBoxPresetName_Leave(object sender, EventArgs e)
        {
            Preset selectedPreset = this.listBox1.SelectedItem as Preset;
            if (selectedPreset != null)
            {
                selectedPreset.Name = this.textBoxPresetName.Text;
            }
        }
    }
}
