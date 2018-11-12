using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CCModPackGenerator
{
    public partial class ResourceBuilder : UserControl
    {
        String currentValue;
        public String FileFilter { get; set; }
        public EventHandler<StringEventArgs> ResourceUpdated { get; set; }

        public class StringEventArgs : EventArgs
        {
            public String value;

            public StringEventArgs(String value)
            {
                this.value = value;
            }
        }

        public ResourceBuilder()
        {
            InitializeComponent();
            currentValue = "";
        }

        public void Clear()
        {
            filenameBox.Text = "";
            currentValue = "";
        }

        public void SetBindingList(ref BindingList<String> bindingList)
        {
            BindingSource bSource = new BindingSource();
            bSource.DataSource = bindingList;
            bSource.ListChanged += BSource_ListChanged;
            String currentValue = this.filenameBox.Text;
            ModPackGui.CacheString(currentValue, ref bindingList);
            this.filenameBox.DataSource = bSource;
            this.filenameBox.SelectedItem = currentValue;
        }

        private void BSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            // Switch the box back to the last value;
            filenameBox.Text = currentValue;
        }

        public void SetValue(String value)
        {
            currentValue = value;
            filenameBox.Text = value;
        }

        public String GetValue()
        {
            return filenameBox.Text;
        }

        private void filenameBox_Leave(object sender, EventArgs e)
        {
            ResourceUpdated?.Invoke(this, new StringEventArgs(filenameBox.Text));
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = FileFilter;
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

                ResourceUpdated?.Invoke(this, new StringEventArgs(filepath));
                SetValue(filepath);
            }
        }
    }
}
