using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCModConfig;

namespace CCModPackGenerator
{
    public partial class ConstantBuffer2Editor : Form
    {
        private String CurrentFilename;
        private DefaultConstantBuffer ConstantBuffer;

        private DataGridViewRow HDRRow;
        private DataGridViewRow IDandFresnelRow;
        private DataGridViewRow VelvetyRimParamRow;
        private DataGridViewRow SpecularColorRow;
        private DataGridViewRow VelvetyColorRow;
        private DataGridViewRow RimColorRow;
        private DataGridViewRow LScatterHighFreqRow;
        private DataGridViewRow MaterialMulColorRow;
        private DataGridViewRow MaterialAddColorRow;
        private DataGridViewRow MaterialParamRow;
        private DataGridViewRow MaterialParam2Row;

        public ConstantBuffer2Editor()
        {
            InitializeComponent();

            this.SuspendLayout();

            CurrentFilename = null;
            ConstantBuffer = new DefaultConstantBuffer();

            HDRRow = new DataGridViewRow();
            HDRRow.CreateCells(dataGridView1, "HDR Rate");
            dataGridView1.Rows.Add(HDRRow);

            IDandFresnelRow = new DataGridViewRow();
            IDandFresnelRow.CreateCells(dataGridView1, "ID and Fresnel");
            dataGridView1.Rows.Add(IDandFresnelRow);

            VelvetyRimParamRow = new DataGridViewRow();
            VelvetyRimParamRow.CreateCells(dataGridView1, "Velvety Rim Param");
            dataGridView1.Rows.Add(VelvetyRimParamRow);

            SpecularColorRow = new DataGridViewRow();
            SpecularColorRow.CreateCells(dataGridView1, "Specular Color");
            dataGridView1.Rows.Add(SpecularColorRow);

            VelvetyColorRow = new DataGridViewRow();
            VelvetyColorRow.CreateCells(dataGridView1, "Velvety Color");
            dataGridView1.Rows.Add(VelvetyColorRow);

            RimColorRow = new DataGridViewRow();
            RimColorRow.CreateCells(dataGridView1, "Rim Color");
            dataGridView1.Rows.Add(RimColorRow);

            LScatterHighFreqRow = new DataGridViewRow();
            LScatterHighFreqRow.CreateCells(dataGridView1, "Light Scatter High Frequency");
            dataGridView1.Rows.Add(LScatterHighFreqRow);

            MaterialMulColorRow = new DataGridViewRow();
            MaterialMulColorRow.CreateCells(dataGridView1, "Material Multiply Color");
            dataGridView1.Rows.Add(MaterialMulColorRow);

            MaterialAddColorRow = new DataGridViewRow();
            MaterialAddColorRow.CreateCells(dataGridView1, "Material Add Color");
            dataGridView1.Rows.Add(MaterialAddColorRow);

            MaterialParamRow = new DataGridViewRow();
            MaterialParamRow.CreateCells(dataGridView1, "Material Parameter");
            dataGridView1.Rows.Add(MaterialParamRow);

            MaterialParam2Row = new DataGridViewRow();
            MaterialParam2Row.CreateCells(dataGridView1, "Material Parameter 2");
            dataGridView1.Rows.Add(MaterialParam2Row);

            this.ResumeLayout();
        }

        private void SetGridValues(DataGridViewRow dRow, float[] values)
        {
            dRow.Cells[ColumnX.Index].Value = values[0];
            dRow.Cells[ColumnX.Index].ToolTipText = System.BitConverter.ToString(System.BitConverter.GetBytes(values[0]));
            dRow.Cells[ColumnY.Index].Value = values[1];
            dRow.Cells[ColumnY.Index].ToolTipText = System.BitConverter.ToString(System.BitConverter.GetBytes(values[1]));
            dRow.Cells[ColumnZ.Index].Value = values[2];
            dRow.Cells[ColumnZ.Index].ToolTipText = System.BitConverter.ToString(System.BitConverter.GetBytes(values[2]));
            dRow.Cells[ColumnW.Index].Value = values[3];
            dRow.Cells[ColumnW.Index].ToolTipText = System.BitConverter.ToString(System.BitConverter.GetBytes(values[3]));
        }

        private float[] GetGridValues(DataGridViewRow dRow)
        {
            float[] storedFloats = new float[4];
            storedFloats[0] = Single.Parse(dRow.Cells[ColumnX.Index].Value.ToString());
            storedFloats[1] = Single.Parse(dRow.Cells[ColumnY.Index].Value.ToString());
            storedFloats[2] = Single.Parse(dRow.Cells[ColumnZ.Index].Value.ToString());
            storedFloats[3] = Single.Parse(dRow.Cells[ColumnW.Index].Value.ToString());

            return storedFloats;
        }

        private void StoreGrid()
        {
            ConstantBuffer.HDRRate = GetGridValues(HDRRow);
            ConstantBuffer.IDandFresnel = GetGridValues(IDandFresnelRow);
            ConstantBuffer.VelvetyRimParam = GetGridValues(VelvetyRimParamRow);
            ConstantBuffer.SpecularColor = GetGridValues(SpecularColorRow);
            ConstantBuffer.VelvetyColor = GetGridValues(VelvetyColorRow);
            ConstantBuffer.RimColor = GetGridValues(RimColorRow);
            ConstantBuffer.LScatterHighFreq = GetGridValues(LScatterHighFreqRow);
            ConstantBuffer.MaterialMulColor = GetGridValues(MaterialMulColorRow);
            ConstantBuffer.MaterialAddColor = GetGridValues(MaterialAddColorRow);
            ConstantBuffer.MaterialParam = GetGridValues(MaterialParamRow);
            ConstantBuffer.MaterialParam2 = GetGridValues(MaterialParam2Row);
        }

        private void LoadGrid()
        {
            SetGridValues(HDRRow, ConstantBuffer.HDRRate);
            SetGridValues(IDandFresnelRow, ConstantBuffer.IDandFresnel);
            SetGridValues(VelvetyRimParamRow, ConstantBuffer.VelvetyRimParam);
            SetGridValues(SpecularColorRow, ConstantBuffer.SpecularColor);
            SetGridValues(VelvetyColorRow, ConstantBuffer.VelvetyColor);
            SetGridValues(RimColorRow, ConstantBuffer.RimColor);
            SetGridValues(LScatterHighFreqRow, ConstantBuffer.LScatterHighFreq);
            SetGridValues(MaterialMulColorRow, ConstantBuffer.MaterialMulColor);
            SetGridValues(MaterialAddColorRow, ConstantBuffer.MaterialAddColor);
            SetGridValues(MaterialParamRow, ConstantBuffer.MaterialParam);
            SetGridValues(MaterialParam2Row, ConstantBuffer.MaterialParam2);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                CurrentFilename = openFileDialog1.FileName;
                if (!ConstantBuffer.Load(openFileDialog1.FileName))
                {
                    MessageBox.Show("Could not load: " + openFileDialog1.FileName);
                    return;
                }
                LoadGrid();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                CurrentFilename = saveFileDialog1.FileName;
                SaveFile();
            }
        }

        private void SaveFile()
        {
            try
            {
                StoreGrid();
                ConstantBuffer.Save(CurrentFilename);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not save: " + CurrentFilename);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
