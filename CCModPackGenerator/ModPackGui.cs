using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using CCModConfig;

namespace CCModPackGenerator
{
    public partial class ModPackGui : Form
    {
        public static ModProjectConfig ModConfig { get; set; }
        public static String WorkspaceDirectory { get; set; }


        public static BindingList<String> vbList;
        public static BindingList<String> ibList;
        public static BindingList<String> fingerNailIBList;
        public static BindingList<String> fingerNailVBList;
        public static BindingList<String> toeNailIBList;
        public static BindingList<String> toeNailVBList;
        public static BindingList<String> chestIBList;
        public static BindingList<String> chestVBList;
        public static BindingList<String> breastIBList;
        public static BindingList<String> breastVBList;
        public static BindingList<String> groinIBList;
        public static BindingList<String> groinVBList;
        public static BindingList<String> armIBList;
        public static BindingList<String> armVBList;
        public static BindingList<String> legIBList;
        public static BindingList<String> legVBList;

        public static BindingList<String> textureList;
        public static BindingList<String> constantBufferList;

        public static BindingList<String> commonSkinList;
        public static BindingList<String> commonTanList;
        public static BindingList<String> honokaSkinList;
        public static BindingList<String> honokaTanList;
        public static BindingList<String> marieSkinList;
        public static BindingList<String> marieTanList;

        public static BindingList<String> formatList;

        public ModPackGui()
        {
            InitializeComponent();

            vbList = new BindingList<string>();
            ibList = new BindingList<string>();
            fingerNailIBList = new BindingList<string>();
            fingerNailVBList = new BindingList<string>();
            toeNailIBList = new BindingList<string>();
            toeNailVBList = new BindingList<string>();
            chestIBList = new BindingList<string>();
            chestVBList = new BindingList<string>();
            breastIBList = new BindingList<string>();
            breastVBList = new BindingList<string>();
            groinIBList = new BindingList<string>();
            groinVBList = new BindingList<string>();
            armIBList = new BindingList<string>();
            armVBList = new BindingList<string>();
            legIBList = new BindingList<string>();
            legVBList = new BindingList<string>();
            textureList = new BindingList<string>();
            constantBufferList = new BindingList<String>();
            formatList = new BindingList<string>();

            commonSkinList = new BindingList<string>();
            commonTanList = new BindingList<string>();
            marieSkinList = new BindingList<string>();
            marieTanList = new BindingList<string>();
            honokaSkinList = new BindingList<string>();
            honokaTanList = new BindingList<string>();

            formatList.Add("DXGI_FORMAT_R16_UINT");
        }

        public static void ResetLists()
        {
            vbList.Clear();
            vbList.Add("");
            ibList.Clear();
            ibList.Add("");
            fingerNailIBList.Clear();
            fingerNailIBList.Add("");
            fingerNailVBList.Clear();
            fingerNailVBList.Add("");
            toeNailIBList.Clear();
            toeNailIBList.Add("");
            toeNailVBList.Clear();
            toeNailVBList.Add("");
            chestIBList.Clear();
            chestIBList.Add("");
            chestVBList.Clear();
            chestVBList.Add("");
            breastIBList.Clear();
            breastIBList.Add("");
            breastVBList.Clear();
            breastVBList.Add("");
            groinIBList.Clear();
            groinIBList.Add("");
            groinVBList.Clear();
            groinVBList.Add("");
            armIBList.Clear();
            armIBList.Add("");
            armVBList.Clear();
            armVBList.Add("");
            legIBList.Clear();
            legIBList.Add("");
            legVBList.Clear();
            legVBList.Add("");
            textureList.Clear();
            textureList.Add("");
            constantBufferList.Clear();
            constantBufferList.Add("");

            commonSkinList.Clear();
            commonSkinList.Add("");
            commonTanList.Clear();
            commonTanList.Add("");
            marieSkinList.Clear();
            marieSkinList.Add("");
            marieTanList.Clear();
            marieTanList.Add("");
            honokaSkinList.Clear();
            honokaSkinList.Add("");
            honokaTanList.Clear();
            honokaTanList.Add("");

            formatList.Clear();
            formatList.Add("");
            formatList.Add("DXGI_FORMAT_R16_UINT");

            foreach (CharacterProject charProject in ModConfig.CharacterMods)
            {
                foreach (Item item in charProject.Items)
                {
                    foreach (Option option in item.Options)
                    {
                        foreach (MeshSlot meshSlot in option.SolidMeshes)
                        {
                            CacheMeshSlot(meshSlot);
                        }
                        foreach (MeshSlot meshSlot in option.AlphaMeshes)
                        {
                            CacheMeshSlot(meshSlot);
                        }
                        foreach (BodyMesh bodyMesh in option.BodyMeshes)
                        {
                            if (charProject.Model == ModelType.Honoka)
                            {
                                CacheString(bodyMesh.TanTexture, ref honokaTanList);
                                foreach (SkinTexture aSkinTexture in bodyMesh.SkinTextures)
                                {
                                    CacheString(aSkinTexture.Filename, ref honokaSkinList);
                                }
                            }
                            else if (charProject.Model == ModelType.MarieRose)
                            {
                                CacheString(bodyMesh.TanTexture, ref marieTanList);
                                foreach (SkinTexture aSkinTexture in bodyMesh.SkinTextures)
                                {
                                    CacheString(aSkinTexture.Filename, ref marieSkinList);
                                }
                            }
                            else
                            {
                                CacheString(bodyMesh.TanTexture, ref commonTanList);
                                foreach (SkinTexture aSkinTexture in bodyMesh.SkinTextures)
                                {
                                    CacheString(aSkinTexture.Filename, ref commonSkinList);
                                }
                            }

                            switch (bodyMesh.MeshType)
                            {
                                case BodyMeshType.Torso:
                                    CacheMesh(bodyMesh, ref chestIBList, ref chestVBList, ref formatList);
                                    break;
                                case BodyMeshType.Breast:
                                    CacheMesh(bodyMesh, ref breastIBList, ref breastVBList, ref formatList);
                                    break;
                                case BodyMeshType.Groin:
                                    CacheMesh(bodyMesh, ref groinIBList, ref groinVBList, ref formatList);
                                    break;
                                case BodyMeshType.Arm:
                                    CacheMesh(bodyMesh, ref armIBList, ref armVBList, ref formatList);
                                    break;
                                case BodyMeshType.Leg:
                                    CacheMesh(bodyMesh, ref legIBList, ref legVBList, ref formatList);
                                    break;
                                case BodyMeshType.FingerNail:
                                    CacheMesh(bodyMesh, ref fingerNailIBList, ref fingerNailVBList, ref formatList);
                                    break;
                                case BodyMeshType.ToeNail:
                                    CacheMesh(bodyMesh, ref toeNailIBList, ref toeNailVBList, ref formatList);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static void CacheMeshSlot(MeshSlot meshSlot)
        {
            if (meshSlot != null)
            {
                CacheMesh(meshSlot.NormalMesh, ref ibList, ref vbList, ref formatList);
                CacheMesh(meshSlot.ShadowMesh, ref ibList, ref vbList, ref formatList);
                CacheString(meshSlot.PS0Texture, ref textureList);
                CacheString(meshSlot.PS1Texture, ref textureList);
                CacheString(meshSlot.PS2Texture, ref textureList);
                CacheString(meshSlot.PSCB2Buffer, ref constantBufferList);
            }
        }

        public static void CacheMesh(Mesh mesh, ref BindingList<String> refIbBindingList, ref BindingList<String> refVbBindingList, ref BindingList<String> refFormatBindingList)
        {
            if (mesh != null)
            {
                CacheString(mesh.IndexBuffer, ref refIbBindingList);
                CacheString(mesh.VertexBuffer, ref refVbBindingList);
                CacheString(mesh.Format, ref refFormatBindingList);
            }
        }

        public static void CacheString(String filename, ref BindingList<String> refStringList)
        {
            if (!String.IsNullOrEmpty(filename) && !refStringList.Contains(filename))
            {
                refStringList.Add(filename);
            }
        }

        private ModProjectConfig UpdateConfig()
        {
            ModConfig.CharacterMods.Clear();
            ModConfig.CharacterMods.Add(this.costumeBuilderCommon.UpdateConfig());
            ModConfig.CharacterMods.Add(this.costumeBuilderHonoka.UpdateConfig());
            ModConfig.CharacterMods.Add(this.costumeBuilderMarieRose.UpdateConfig());

            return ModConfig;
        }

        private void SetConfig(String workspaceFolder, ModProjectConfig projectConfig)
        {
            ModConfig = projectConfig;
            WorkspaceDirectory = workspaceFolder;
            // Reload cache based off the config.
            ResetLists();

            // create any possible missing characters
            CharacterProject commonProject = new CharacterProject();
            commonProject.Model = ModelType.Common;
            CharacterProject honokaProject = new CharacterProject();
            honokaProject.Model = ModelType.Honoka;
            CharacterProject marieProject = new CharacterProject();
            marieProject.Model = ModelType.MarieRose;

            for (int i = 0; i < projectConfig.CharacterMods.Count; i++)
            {
                switch (projectConfig.CharacterMods[i].Model)
                {
                    case ModelType.Common:
                        commonProject = projectConfig.CharacterMods[i];
                        break;
                    case ModelType.Honoka:
                        honokaProject = projectConfig.CharacterMods[i];
                        break;
                    case ModelType.MarieRose:
                        marieProject = projectConfig.CharacterMods[i];
                        break;
                }
            }

            // Update UI
            this.textBoxName.Text = projectConfig.Title;
            this.textBoxVersion.Text = projectConfig.Version;
            this.textBox1.Text = projectConfig.CostumeCustomizerMod;

            this.textBoxWorkspace.Text = workspaceFolder;
            this.textBoxWorkspace.Enabled = false;

            this.costumeBuilderCommon.SetConfig(commonProject);
            this.costumeBuilderHonoka.SetConfig(honokaProject);
            this.costumeBuilderMarieRose.SetConfig(marieProject);

            this.tabControl1.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Costume Customizer Mod Pack Project (CostumeCustomizerModConfig.xml)|CostumeCustomizerModConfig.xml";
            openFileDialog1.DefaultExt = "CostumeCustomizerModConfig.xml";
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DialogResult openResult = openFileDialog1.ShowDialog();

            if (openResult == DialogResult.OK)
            {
                try
                {
                    ModProjectConfig originalConfig = ModProjectConfig.ParseConfig(openFileDialog1.FileName);
                    SetConfig(Path.GetDirectoryName(openFileDialog1.FileName), ModProjectConfig.ParseConfig(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading " + openFileDialog1.FileName + Environment.NewLine + "Exception: " + ex.ToString(), "CC Mod Pack Generator", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DialogResult folderResult = folderBrowserDialog1.ShowDialog();

            if (folderResult == DialogResult.OK)
            {
                SetConfig(folderBrowserDialog1.SelectedPath, new ModProjectConfig());
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            {
                ModProjectConfig updatedConfig = UpdateConfig();
                updatedConfig.SaveConfig(Path.Combine(WorkspaceDirectory, "CostumeCustomizerModConfig.xml"));
                MessageBox.Show("Successfully saved Project Config.");
            }
            //catch (Exception ex)
            {
            //    MessageBox.Show("Failed to save config.\nException: " + ex.ToString());
#if DEBUG
            //    throw ex;
#endif
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            ModConfig.Title = this.textBoxName.Text;
        }

        private void textBoxVersion_Leave(object sender, EventArgs e)
        {
            ModConfig.Version = this.textBoxVersion.Text;
        }

        private void textBoxWorkspace_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ModConfig.CostumeCustomizerMod = this.textBox1.Text;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ModProjectConfig currentConfig = UpdateConfig();
                INIGenerator generator = new INIGenerator(currentConfig);
                generator.GenerateINIs(WorkspaceDirectory);
                MessageBox.Show("Successfully export INI files.", "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to export INI files.\nException: " + e.ToString(), "CC Mod Pack Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if DEBUG
                throw new Exception("Uncaught exception during export.", ex);
#endif
            }
        }

        private void constantBufferEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConstantBuffer2Editor editor = new ConstantBuffer2Editor();
            editor.Show();
        }
    }
}
