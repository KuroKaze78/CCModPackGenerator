using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCModConfig;

namespace CCModPackGenerator
{
    public class INIGenerator
    {
        private Dictionary<String, String> FilenameToResource;
        private Dictionary<String, String> ResourceContent;
        private ModProjectConfig config;

        private const String ModPath = @"Mods\Costumes\CostumeCustomizer\";

        private static Dictionary<ModelType, String> modelToIni;
        private static Dictionary<Category, CategoryMetadata> categoryDefaults;
        private static Dictionary<BodyMeshType, String> bodyMeshTypeResName;
        private static CategoryMetadata PresetDefaults = new CategoryMetadata("0", "../shared/Chest_Icon.png", "", "");

        public struct CategoryMetadata
        {
            public String slot;
            public String defaultIcon;
            public String defaultPreview;
            public String header;

            public CategoryMetadata(String slot, String icon, String preview, String header)
            {
                this.slot = slot;
                this.defaultIcon = icon;
                this.defaultPreview = preview;
                this.header = header;
            }
        }

        private const String iconSlot = "Icon";
        private const String previewSlot = "Preview";

        private const String resourcePrefix = "Resource";

        private const String vbSuffix = "_VB";
        private const String ibSuffix = "_IB";
        private const String ps0Suffix = "_PS0";
        private const String ps1Suffix = "_PS1";
        private const String ps2Suffix = "_PS2";
        private const String svbSuffix = "_SVB";
        private const String sibSuffix = "_SIB";

        static INIGenerator()
        {
            modelToIni = new Dictionary<ModelType, String>();
            categoryDefaults = new Dictionary<Category, CategoryMetadata>();
            bodyMeshTypeResName = new Dictionary<BodyMeshType, string>();

            modelToIni[ModelType.Common] = "Common.ini";
            modelToIni[ModelType.Honoka] = "Honoka.ini";
            modelToIni[ModelType.MarieRose] = "Marie.ini";

            categoryDefaults[Category.Chest] = new CategoryMetadata("1", "../shared/Chest_Icon.png", "", "Chest");
            categoryDefaults[Category.Bra] = new CategoryMetadata("2", "../shared/Bra_Icon.png", "", "Bra");
            categoryDefaults[Category.Panty] = new CategoryMetadata("3", "../shared/Panty_Icon.png", "", "Panty");
            categoryDefaults[Category.Skirt] = new CategoryMetadata("4", "../shared/Skirt_Icon.png", "", "Skirt");
            categoryDefaults[Category.Gloves] = new CategoryMetadata("5", "../shared/Glove_Icon.png", "", "Glove");
            categoryDefaults[Category.Shoes] = new CategoryMetadata("6", "../shared/Shoe_Icon.png", "", "Shoes");
            categoryDefaults[Category.Accessory1] = new CategoryMetadata("7", "../shared/Accessory_1_Icon.png", "", "Accessory1");
            categoryDefaults[Category.Accessory2] = new CategoryMetadata("8", "../shared/Accessory_2_Icon.png", "", "Accessory2");
            categoryDefaults[Category.Accessory3] = new CategoryMetadata("9", "../shared/Accessory_3_Icon.png", "", "Accessory3");
            categoryDefaults[Category.Accessory4] = new CategoryMetadata("10", "../shared/Accessory_4_Icon.png", "", "Accessory4");

            bodyMeshTypeResName[BodyMeshType.Torso] = "BodyChest";
            bodyMeshTypeResName[BodyMeshType.Breast] = "BodyBreast";
            bodyMeshTypeResName[BodyMeshType.Groin] = "BodyGroin";
            bodyMeshTypeResName[BodyMeshType.Arm] = "BodyArms";
            bodyMeshTypeResName[BodyMeshType.Leg] = "BodyLegs";
            bodyMeshTypeResName[BodyMeshType.FingerNail] = "FingerNails";
            bodyMeshTypeResName[BodyMeshType.ToeNail] = "ToeNails";
        }



        public INIGenerator(ModProjectConfig config)
        {
            this.config = config;
            FilenameToResource = new Dictionary<string, string>();
            ResourceContent = new Dictionary<string, string>();
        }


        public String CorrectResourceName(String originalName)
        {
            return originalName.Replace(" ", "_").Replace("&", "").Replace("@", "").Replace("$", "").Replace("#", "").Replace("^", "").Replace("&", "");
        }

        public void CacheTextureResource(String filename, String name)
        {
            CacheResource(false, filename, "filename=" + filename, CorrectResourceName(name));
        }

        public void CacheVBResource(String filename, UInt32? stride, String name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("type=Buffer");
            sb.AppendLine("stride=" + stride);
            sb.AppendLine("filename=" + filename);

            CacheResource(false, filename, sb.ToString(), CorrectResourceName(name));
        }

        public void CacheIBResource(String filename, String format, String name)
        {
            if (filename == null || format == null)
            {
                throw new Exception("Missing format for " + filename);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("type=Buffer");
            sb.AppendLine("format=" + format);
            sb.AppendLine("filename=" + filename);

            CacheResource(false, filename, sb.ToString(), CorrectResourceName(name));
        }

        public void CacheRequiredTextureResource(String filename, String name, String defaultFilename)
        {
            String content = "";
            if (String.IsNullOrEmpty(filename))
            {
                filename = defaultFilename;
            }
            if (!String.IsNullOrEmpty(filename))
            {
                content = "filename=" + filename;
            }
            CacheResource(true, filename, content, name);
        }
        public void CacheResource(bool required, String key, String resourceContent, String name)
        {
            if (!required && FilenameToResource.ContainsKey(key))
            {
                // Nothing to insert because the file is already covered.
                return;
            }

            String resourceName = resourcePrefix + name;
            if (required)
            {
                if (ResourceContent.ContainsKey(resourceName))
                {
                    throw new Exception("Duplicate on required resource name.");
                }
            }
            else
            {
                int incrementer = 0;
                String testResourceName = resourceName;
                while (ResourceContent.ContainsKey(testResourceName))
                {
                    incrementer++;
                    testResourceName = resourceName + incrementer;
                }
                resourceName = testResourceName;
            }

            FilenameToResource[key] = resourceName;
            ResourceContent[resourceName] = resourceContent;
        }

        public void CacheResources(CharacterProject charaProj)
        {
            ResourceContent.Clear();
            FilenameToResource.Clear();

            CacheRequiredTextureResource(charaProj.IconFile, iconSlot + PresetDefaults.slot, PresetDefaults.defaultIcon);
            CacheRequiredTextureResource(charaProj.PreviewFile, previewSlot + PresetDefaults.slot, PresetDefaults.defaultPreview);

            foreach (Item item in charaProj.Items)
            {
                CacheRequiredTextureResource(item.Icon, iconSlot + categoryDefaults[item.ItemCategory].slot, categoryDefaults[item.ItemCategory].defaultIcon);
                CacheRequiredTextureResource(item.Preview, previewSlot + categoryDefaults[item.ItemCategory].slot, categoryDefaults[item.ItemCategory].defaultPreview);
            }

            foreach (Item item in charaProj.Items)
            {
                foreach (Option option in item.Options)
                {
                    int i = 0;
                    foreach (MeshSlot solidMesh in option.SolidMeshes)
                    {
                        i++;
                        if (solidMesh.NormalMesh != null)
                        {
                            CacheIBResource(solidMesh.NormalMesh.IndexBuffer, solidMesh.NormalMesh.Format, option.Name + "S" + i + ibSuffix);
                            CacheVBResource(solidMesh.NormalMesh.VertexBuffer, solidMesh.NormalMesh.Stride, option.Name + "S" + i + vbSuffix);
                        }
                        if (solidMesh.ShadowMesh != null && !solidMesh.ShadowMesh.IsNull && !solidMesh.ShadowMesh.IsDefault)
                        {
                            CacheIBResource(solidMesh.ShadowMesh.IndexBuffer, solidMesh.ShadowMesh.Format, option.Name + "S" + i + sibSuffix);
                            CacheVBResource(solidMesh.ShadowMesh.VertexBuffer, solidMesh.ShadowMesh.Stride, option.Name + "S" + i + svbSuffix);
                        }
                        CacheTextureResource(solidMesh.PS0Texture, option.Name + "S" + i + ps0Suffix);
                        CacheTextureResource(solidMesh.PS1Texture, option.Name + "S" + i + ps1Suffix);
                        CacheTextureResource(solidMesh.PS2Texture, option.Name + "S" + i + ps2Suffix);
                    }

                    i = 0;
                    foreach (MeshSlot alphaMesh in option.AlphaMeshes)
                    {
                        i++;
                        if (alphaMesh.NormalMesh != null)
                        {
                            CacheIBResource(alphaMesh.NormalMesh.IndexBuffer, alphaMesh.NormalMesh.Format, option.Name + "A" + i + ibSuffix);
                            CacheVBResource(alphaMesh.NormalMesh.VertexBuffer, alphaMesh.NormalMesh.Stride, option.Name + "A" + i + vbSuffix);
                        }
                        if (alphaMesh.ShadowMesh != null && !alphaMesh.ShadowMesh.IsNull && !alphaMesh.ShadowMesh.IsDefault)
                        {
                            CacheIBResource(alphaMesh.ShadowMesh.IndexBuffer, alphaMesh.ShadowMesh.Format, option.Name + "A" + i + ibSuffix);
                            CacheVBResource(alphaMesh.ShadowMesh.VertexBuffer, alphaMesh.ShadowMesh.Stride, option.Name + "A" + i + vbSuffix);
                        }
                        CacheTextureResource(alphaMesh.PS0Texture, option.Name + "A" + i + ps0Suffix);
                        CacheTextureResource(alphaMesh.PS1Texture, option.Name + "A" + i + ps1Suffix);
                        CacheTextureResource(alphaMesh.PS2Texture, option.Name + "A" + i + ps2Suffix);
                    }

                    foreach (BodyMesh bodyMesh in option.BodyMeshes)
                    {
                        if (!bodyMesh.IsNull && !bodyMesh.IsDefault)
                        {
                            CacheIBResource(bodyMesh.IndexBuffer, bodyMesh.Format, option.Name + bodyMeshTypeResName[bodyMesh.MeshType] + ibSuffix);
                            CacheVBResource(bodyMesh.VertexBuffer, bodyMesh.Stride, option.Name + bodyMeshTypeResName[bodyMesh.MeshType] + vbSuffix);
                        }
                    }
                }
            }
        }

        public void GenerateINIs(String workspaceDirectory)
        {
            foreach (CharacterProject charaProj in config.CharacterMods)
            {
                String referenceINI = modelToIni[charaProj.Model];

                CacheResources(charaProj);

                StringBuilder sb = new StringBuilder();

                // Default Placeholder resources
                sb.AppendLine(@"
[ResourceIcon]
[ResourcePreview]

; Check options based on available
[CommandListSelect]
if x7 == 0
  run = CommandListSelect0
endif
if x7 == 1
  run = CommandListSelect1
endif
if x7 == 2
  run = CommandListSelect2
endif
if x7 == 3
  run = CommandListSelect3
endif
if x7 == 4
  run = CommandListSelect4
endif
if x7 == 5
  run = CommandListSelect5
endif
if x7 == 6
  run = CommandListSelect6
endif
if x7 == 7
  run = CommandListSelect7
endif
if x7 == 8
  run = CommandListSelect8
endif
if x7 == 9
  run = CommandListSelect9
endif
if x7 == 10
  run = CommandListSelect10
endif

");

                sb.AppendLine("[CommandListSelect0]");
                sb.AppendLine("if y7 == 12");
                sb.AppendLine("  ; Option 12");

                // Set Preset 12
                AppendPreset(charaProj, 12, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 11");
                sb.AppendLine("  ; Option 11");

                // Set Preset 11
                AppendPreset(charaProj, 11, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 10");
                sb.AppendLine("  ; Option 10");

                // Set Preset 10
                AppendPreset(charaProj, 10, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 9");
                sb.AppendLine("  ; Option 9");

                // Set Preset 9
                AppendPreset(charaProj, 9, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 8");
                sb.AppendLine("  ; Option 8");

                // Set Preset 8
                AppendPreset(charaProj, 8, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 7");
                sb.AppendLine("  ; Option 7");

                // Set Preset 7
                AppendPreset(charaProj, 7, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 6");
                sb.AppendLine("  ; Option 6");

                // Set Preset 6
                AppendPreset(charaProj, 6, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 5");
                sb.AppendLine("  ; Option 5");

                // Set Preset 5
                AppendPreset(charaProj, 4, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 4");
                sb.AppendLine("  ; Option 4");

                // Set Preset 4
                AppendPreset(charaProj, 4, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 3");
                sb.AppendLine("  ; Option 3");

                // Set Preset 3
                AppendPreset(charaProj, 3, sb);

                sb.AppendLine("else");
                sb.AppendLine("if y7 == 2");
                sb.AppendLine("  ; Option 2");

                // Set Preset 2
                AppendPreset(charaProj, 2, sb);

                sb.AppendLine("else");
                sb.AppendLine("  ; Default");

                // Default
                AppendPreset(charaProj, 1, sb);

                sb.AppendLine(@"
endif
endif
endif
endif
endif
endif
endif
endif
endif
endif
endif");
                // Select Items
                foreach (Item item in charaProj.Items)
                {
                    AppendItem(ModPath + referenceINI, item, sb);
                }

                // Resources
                foreach (KeyValuePair<String, String> resource in ResourceContent)
                {
                    sb.AppendLine();
                    sb.AppendLine("[" + resource.Key + "]");
                    sb.AppendLine(resource.Value);
                }

                System.IO.File.WriteAllText(System.IO.Path.Combine(workspaceDirectory, referenceINI), sb.ToString());
            }
        }

        public void AppendPreset(CharacterProject charaProj, int slot, StringBuilder sb)
        {
            if (charaProj.Presets.Count >= slot && slot > 0)
            {
                Preset curPreset = charaProj.Presets[slot - 1];
                sb.AppendLine("; Preset " + slot + " - " + curPreset.Name);
                foreach (PresetOption curPresetOption in curPreset.PresetOptions)
                {
                    sb.AppendLine("  y7 = " + curPresetOption.Option);
                    sb.AppendLine("  run = CommandListSelect" + categoryDefaults[curPresetOption.PresetItem].slot);
                }
            }
        }

        public void AppendItem(String resourceHeader, Item item, StringBuilder sb)
        {
            sb.AppendLine("; " + categoryDefaults[item.ItemCategory].header);
            sb.AppendLine("[CommandListSelect" + categoryDefaults[item.ItemCategory].slot + "]");

            for (int i = 1; i <= item.Options.Count; i++)
            {
                Option currentOption = item.Options[i-1];
                sb.AppendLine("if y7 == " + i);
                sb.AppendLine("  ; Option " + i + " - " + currentOption.Name);
                
                foreach (BodyMesh bodyMesh in currentOption.BodyMeshes)
                {
                    if (!bodyMesh.IsDefault)
                    {
                        if (bodyMesh.IsNull)
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + bodyMeshTypeResName[bodyMesh.MeshType] + "IB = null");
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + bodyMeshTypeResName[bodyMesh.MeshType] + "VB = null");
                        }
                        else
                        {
                            if (FilenameToResource.ContainsKey(bodyMesh.IndexBuffer))
                            {
                                sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + bodyMeshTypeResName[bodyMesh.MeshType] + "IB = ref " + FilenameToResource[bodyMesh.IndexBuffer]);
                            }

                            if (FilenameToResource.ContainsKey(bodyMesh.VertexBuffer))
                            {
                                sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + bodyMeshTypeResName[bodyMesh.MeshType] + "VB = ref " + FilenameToResource[bodyMesh.VertexBuffer]);
                            }
                        }
                    }
                }

                int j = 0;
                foreach (MeshSlot solidMesh in currentOption.SolidMeshes)
                {
                    j++;

                    // IB
                    if (solidMesh.NormalMesh != null && FilenameToResource.ContainsKey(solidMesh.NormalMesh.IndexBuffer))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "IB = ref " + FilenameToResource[solidMesh.NormalMesh.IndexBuffer]);
                    }

                    // VB
                    if (solidMesh.NormalMesh != null && FilenameToResource.ContainsKey(solidMesh.NormalMesh.IndexBuffer))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "VB = ref " + FilenameToResource[solidMesh.NormalMesh.VertexBuffer]);
                    }

                    // SIB
                    if (solidMesh.ShadowMesh == null || solidMesh.ShadowMesh.IsDefault)
                    {
                        // Default
                        // IB -> SIB
                        if (solidMesh.NormalMesh != null && FilenameToResource.ContainsKey(solidMesh.NormalMesh.IndexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "SIB = ref " + FilenameToResource[solidMesh.NormalMesh.IndexBuffer]);
                        }

                        // VB -> SVB
                        if (solidMesh.NormalMesh != null && FilenameToResource.ContainsKey(solidMesh.NormalMesh.IndexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "SVB = ref " + FilenameToResource[solidMesh.NormalMesh.VertexBuffer]);
                        }
                    }
                    else if (!solidMesh.ShadowMesh.IsNull)
                    {
                        // Custom
                        if (FilenameToResource.ContainsKey(solidMesh.NormalMesh.IndexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "IB = ref " + FilenameToResource[solidMesh.NormalMesh.IndexBuffer]);
                        }

                        if (FilenameToResource.ContainsKey(solidMesh.NormalMesh.VertexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "VB = ref " + FilenameToResource[solidMesh.NormalMesh.VertexBuffer]);
                        }
                    }

                    if (!String.IsNullOrEmpty(solidMesh.PS0Texture))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "PS0 = " + FilenameToResource[solidMesh.PS0Texture]);
                    }
                    if (!String.IsNullOrEmpty(solidMesh.PS1Texture))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "PS1 = " + FilenameToResource[solidMesh.PS1Texture]);
                    }
                    if (!String.IsNullOrEmpty(solidMesh.PS2Texture))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "S" + j + "PS2 = " + FilenameToResource[solidMesh.PS2Texture]);
                    }
                    sb.AppendLine();
                }

                // Alpha Meshes
                j = 0;
                foreach (MeshSlot alphaMesh in currentOption.AlphaMeshes)
                {
                    j++;

                    // IB
                    if (alphaMesh.NormalMesh != null && FilenameToResource.ContainsKey(alphaMesh.NormalMesh.IndexBuffer))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "IB = ref " + FilenameToResource[alphaMesh.NormalMesh.IndexBuffer]);
                    }

                    // VB
                    if (alphaMesh.NormalMesh != null && FilenameToResource.ContainsKey(alphaMesh.NormalMesh.IndexBuffer))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "VB = ref " + FilenameToResource[alphaMesh.NormalMesh.VertexBuffer]);
                    }

                    // SIB
                    if (alphaMesh.ShadowMesh == null || alphaMesh.ShadowMesh.IsDefault)
                    {
                        // Default
                        // IB -> SIB
                        if (alphaMesh.NormalMesh != null && FilenameToResource.ContainsKey(alphaMesh.NormalMesh.IndexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "SIB = ref " + FilenameToResource[alphaMesh.NormalMesh.IndexBuffer]);
                        }

                        // VB -> SVB
                        if (alphaMesh.NormalMesh != null && FilenameToResource.ContainsKey(alphaMesh.NormalMesh.IndexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "SVB = ref " + FilenameToResource[alphaMesh.NormalMesh.VertexBuffer]);
                        }
                    }
                    else if (!alphaMesh.ShadowMesh.IsNull)
                    {
                        // Custom
                        if (FilenameToResource.ContainsKey(alphaMesh.NormalMesh.IndexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "IB = ref " + FilenameToResource[alphaMesh.NormalMesh.IndexBuffer]);
                        }

                        if (FilenameToResource.ContainsKey(alphaMesh.NormalMesh.VertexBuffer))
                        {
                            sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "VB = ref " + FilenameToResource[alphaMesh.NormalMesh.VertexBuffer]);
                        }
                    }

                    if (!String.IsNullOrEmpty(alphaMesh.PS0Texture))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "PS0 = " + FilenameToResource[alphaMesh.PS0Texture]);
                    }
                    if (!String.IsNullOrEmpty(alphaMesh.PS1Texture))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "PS1 = " + FilenameToResource[alphaMesh.PS1Texture]);
                    }
                    if (!String.IsNullOrEmpty(alphaMesh.PS2Texture))
                    {
                        sb.AppendLine(@"  Resource\" + resourceHeader + @"\" + categoryDefaults[item.ItemCategory].header + "A" + j + "PS2 = " + FilenameToResource[alphaMesh.PS2Texture]);
                    }
                    sb.AppendLine();
                }
                
                sb.AppendLine("endif");
                sb.AppendLine();
            }
        }
    }
}
