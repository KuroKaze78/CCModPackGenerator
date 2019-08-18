using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CCModConfig
{
    [XmlRoot()]
    public class ModProjectConfig
    {
        [XmlElement("Title")]
        public String Title { get; set; }

        [XmlElement("ModVersion")]
        public String Version { get; set; }

        [XmlElement("CostumeCustomizerMod")]
        public String CostumeCustomizerMod { get; set; }

        [XmlElement("ModelConfig")]
        public List<CharacterProject> CharacterMods { get; set; }

        public ModProjectConfig()
        {
            CharacterMods = new List<CharacterProject>();
        }

        public static ModProjectConfig ParseConfig(String filepath)
        {
            ModProjectConfig loadConfig = new ModProjectConfig();

            using (XmlReader reader = XmlReader.Create(filepath))
            {
                XmlSerializer serializer = new XmlSerializer(loadConfig.GetType());
                loadConfig = serializer.Deserialize(reader) as ModProjectConfig;
            }

            return loadConfig;
        }

        public void SaveConfig(String filepath)
        {
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = " ";
            xmlSettings.NewLineHandling = NewLineHandling.Entitize;

            using (XmlWriter writer = XmlWriter.Create(filepath, xmlSettings))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
            }
        }
    }

    public enum ModelType
    {
        [XmlEnum("Common")]
        Common,
        [XmlEnum("Honoka")]
        Honoka,
        [XmlEnum("MarieRose")]
        MarieRose
    }

    public class CharacterProject
    {
        [XmlElement("ModelType")]
        public ModelType Model { get; set; }

        [XmlElement("IconFile")]
        public String IconFile { get; set; }
        [XmlElement("PreviewFile")]
        public String PreviewFile { get; set; }

        [XmlArray("Presets")]
        [XmlArrayItem("Preset")]
        public List<Preset> Presets { get; set; }

        [XmlElement("Item")]
        public List<Item> Items { get; set; }

        public CharacterProject()
        {
            Presets = new List<Preset>();
            Items = new List<Item>();
        }

        public CharacterProject(ModelType modelType) : this()
        {
            Model = modelType;
        }
    }

    public enum Category
    {
        [XmlEnum("Chest")]
        Chest,
        [XmlEnum("Bra")]
        Bra,
        [XmlEnum("Panty")]
        Panty,
        [XmlEnum("Skirt")]
        Skirt,
        [XmlEnum("Gloves")]
        Gloves,
        [XmlEnum("Shoes")]
        Shoes,
        [XmlEnum("Accessory1")]
        Accessory1,
        [XmlEnum("Accessory2")]
        Accessory2,
        [XmlEnum("Accessory3")]
        Accessory3,
        [XmlEnum("Accessory4")]
        Accessory4
    }

    public class Item
    {
        [XmlAttribute("ItemCategory")]
        public Category ItemCategory { get; set; }

        [XmlElement("ItemName")]
        public String ItemName { get; set; }

        [XmlElement("Icon")]
        public String Icon { get; set; }

        [XmlElement("Preview")]
        public String Preview { get; set; }

        [XmlElement("Option")]
        public List<Option> Options { get; set; }

        public Item()
        {
            Options = new List<Option>();
        }

        public Item(Category category) : this()
        {
            ItemCategory = category;
        }
    }

    public class Option : INotifyPropertyChanged
    {
        [XmlIgnore()]
        private String name;

        [XmlElement("Name")]
        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                this.name = value;
                NodityPropertyChanged("Name");
            }
        }

        [XmlElement("SolidMesh")]
        public List<MeshSlot> SolidMeshes { get; set; }

        [XmlElement("AlphaMesh")]
        public List<MeshSlot> AlphaMeshes { get; set; }

        [XmlElement("BodyMesh")]
        public List<BodyMesh> BodyMeshes { get; set; }

        public Option()
        {
            SolidMeshes = new List<MeshSlot>();
            AlphaMeshes = new List<MeshSlot>();
            BodyMeshes = new List<BodyMesh>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NodityPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public Option Clone()
        {
            Option cloneOption = new Option();
            cloneOption.Name = this.Name;
            foreach (MeshSlot solidMesh in SolidMeshes)
            {
                cloneOption.SolidMeshes.Add(solidMesh.Clone());
            }

            foreach (MeshSlot alphaMesh in AlphaMeshes)
            {
                cloneOption.AlphaMeshes.Add(alphaMesh.Clone());
            }

            foreach (BodyMesh bodyMesh in BodyMeshes)
            {
                cloneOption.BodyMeshes.Add(bodyMesh.Clone() as BodyMesh);
            }

            return cloneOption;
        }
    }

    public class MeshSlot
    {
        [XmlElement("NormalMesh")]
        public Mesh NormalMesh { get; set; }
        [XmlElement("ShadowMesh")]
        public Mesh ShadowMesh { get; set; }

        [XmlElement("PST0Texture")]
        public String PS0Texture { get; set; }
        [XmlElement("PST1Texture")]
        public String PS1Texture { get; set; }
        [XmlElement("PST2Texture")]
        public String PS2Texture { get; set; }
        [XmlElement("PSCB2Buffer")]
        public String PSCB2Buffer { get; set; }

        public MeshSlot()
        {
            NormalMesh = new Mesh();
            ShadowMesh = new Mesh();
            ShadowMesh.IsDefault = true;
        }

        public MeshSlot Clone()
        {
            MeshSlot cloneMeshSlot = new MeshSlot();
            if (NormalMesh != null)
            {
                cloneMeshSlot.NormalMesh = NormalMesh.Clone();
            }
            if (ShadowMesh != null)
            {
                cloneMeshSlot.ShadowMesh = ShadowMesh.Clone();
            }
            cloneMeshSlot.PS0Texture = PS0Texture;
            cloneMeshSlot.PS1Texture = PS1Texture;
            cloneMeshSlot.PS2Texture = PS2Texture;
            cloneMeshSlot.PSCB2Buffer = PSCB2Buffer;

            return cloneMeshSlot;
        }
    }

    public enum BodyMeshType
    {
        [XmlEnum("Torso")]
        Torso,
        [XmlEnum("Breast")]
        Breast,
        [XmlEnum("Groin")]
        Groin,
        [XmlEnum("Arm")]
        Arm,
        [XmlEnum("Leg")]
        Leg,
        [XmlEnum("FingerNail")]
        FingerNail,
        [XmlEnum("ToeNail")]
        ToeNail
    }

    public class BodyMesh : Mesh
    {
        public BodyMeshType MeshType { get; set; }

        [XmlElement("SkinTexture")]
        public List<SkinTexture> SkinTextures { get; set; }

        [XmlElement("TanTexture")]
        public String TanTexture { get; set; }

        public BodyMesh() : base()
        {
            SkinTextures = new List<SkinTexture>();
        }

        public BodyMesh(BodyMeshType meshType) : this()
        {
            MeshType = meshType;
            this.IsDefault = true;
            SkinTextures = new List<SkinTexture>();
        }

        
        public override Mesh Clone()
        {
            BodyMesh cloneBodyMesh = new BodyMesh(MeshType);

            cloneBodyMesh.IsNull = IsNull;
            cloneBodyMesh.IsDefault = IsDefault;
            cloneBodyMesh.IndexBuffer = IndexBuffer;
            cloneBodyMesh.Format = Format;
            cloneBodyMesh.VertexBuffer = VertexBuffer;
            cloneBodyMesh.Stride = Stride;

            cloneBodyMesh.TanTexture = TanTexture;
            foreach (SkinTexture aSkinTexture in SkinTextures)
            {
                cloneBodyMesh.SkinTextures.Add(aSkinTexture.Clone());
            }

            return cloneBodyMesh;
        }
    }

    public class SkinTexture
    {
        [XmlAttribute("SkinSlot")]
        public String SkinSlot { get; set; }

        [XmlAttribute("Filename")]
        public String Filename { get; set; }

        public SkinTexture Clone()
        {
            SkinTexture cloneSkinTexture = new SkinTexture();
            cloneSkinTexture.SkinSlot = SkinSlot;
            cloneSkinTexture.Filename = Filename;

            return cloneSkinTexture;
        }
    }

    public class Mesh
    {
        [XmlAttribute("IsNull")]
        public Boolean IsNull { get; set; }

        [XmlAttribute("IsDefault")]
        public Boolean IsDefault { get; set; }

        [XmlElement("IndexBuffer")]
        public String IndexBuffer { get; set; }
        [XmlElement("Format")]
        public String Format { get; set; }
        [XmlElement("VertexBuffer")]
        public String VertexBuffer { get; set; }
        [XmlElement("Stride")]
        public UInt32? Stride { get; set; }

        public Mesh()
        {
        }

        public virtual Mesh Clone()
        {
            Mesh cloneMesh = new Mesh();
            cloneMesh.IsNull = IsNull;
            cloneMesh.IsDefault = IsDefault;
            cloneMesh.IndexBuffer = IndexBuffer;
            cloneMesh.Format = Format;
            cloneMesh.VertexBuffer = VertexBuffer;
            cloneMesh.Stride = Stride;

            return cloneMesh;
        }
    }

    public class Preset : INotifyPropertyChanged
    {
        [XmlIgnore()]
        private String name;

        [XmlAttribute("Name")]
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

        [XmlElement("PresetOption")]
        public List<PresetOption> PresetOptions { get; set; }

        public Preset()
        {
            PresetOptions = new List<PresetOption>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NodityPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public Preset Clone()
        {
            Preset newPreset = new Preset();
            foreach (PresetOption pOption in this.PresetOptions)
            {
                newPreset.PresetOptions.Add(pOption.Clone());
            }

            return newPreset;
        }
    }

    public class PresetOption
    {
        [XmlAttribute("Category")]
        public Category PresetItem { get; set; }
        [XmlAttribute("Option")]
        public int Option { get; set; }

        public PresetOption()
        {
            Option = -1;
        }

        public PresetOption Clone()
        {
            PresetOption newPOption = new PresetOption();
            newPOption.PresetItem = this.PresetItem;
            newPOption.Option = this.Option;

            return newPOption;
        }
    }
}

