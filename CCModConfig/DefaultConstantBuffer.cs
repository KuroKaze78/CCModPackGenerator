using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CCModConfig
{
    public class DefaultConstantBuffer
    {
        public float[] HDRRate { get; set; }
        public float[] DAlphaParam { get; set; }
        public float[] TexBlendAlpha1 { get; set; }
        public float[] TexBlendAlpha2 { get; set; }
        public float[] TexBlendScaleBias1 { get; set; }
        public float[] TexBlendScaleBias2 { get; set; }
        public float[] NormalTexBlendAlpha { get; set; }
        public float[] NormalTexBlendScaleBias { get; set; }
        public float[] ShadowCsBias { get; set; }
        public float[] IDandFresnel { get; set; }
        public float[] ReflectionColor { get; set; }
        public float[] ReflectionVisibilityParam { get; set; }
        public float[] EyePosition { get; set; }
        public float[] EyePlane { get; set; }
        public float[] IblDiffuseColor { get; set; }
        public float[] IblSpecularColor { get; set; }
        public float[] VelvetyRimParam { get; set; }
        public float[] SpecularColor { get; set; }
        public float[] SpecularVisibilityParam { get; set; }
        public float[] VelvetyColor { get; set; }
        public float[] RimColor { get; set; }
        public float[] LScatterHighFreq { get; set; }
        public float[] MaterialMulColor { get; set; }
        public float[] MaterialAddColor { get; set; }
        public float[] MaterialParam { get; set; }
        public float[] MaterialParam2 { get; set; }
        private float[] RESERVED { get; set; }

        public DefaultConstantBuffer()
        {
            HDRRate = new float[4];
            DAlphaParam = new float[4];
            TexBlendAlpha1 = new float[4];
            TexBlendAlpha2 = new float[4];
            TexBlendScaleBias1 = new float[4];
            TexBlendScaleBias2 = new float[4];
            NormalTexBlendAlpha = new float[4];
            NormalTexBlendScaleBias = new float[4];
            ShadowCsBias = new float[4];
            IDandFresnel = new float[4];
            ReflectionColor = new float[4];
            ReflectionVisibilityParam = new float[4];
            EyePosition = new float[4];
            EyePlane = new float[4];
            IblDiffuseColor = new float[4];
            IblSpecularColor = new float[4];
            VelvetyRimParam = new float[4];
            SpecularColor = new float[4];
            SpecularVisibilityParam = new float[4];
            VelvetyColor = new float[4];
            RimColor = new float[4];
            LScatterHighFreq = new float[4];
            MaterialMulColor = new float[4];
            MaterialAddColor = new float[4];
            MaterialParam = new float[4];
            MaterialParam2 = new float[4];
        }

        public bool Save(String filename)
        {
            using (BinaryWriter bWriter = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate)))
            {
                try
                {
                    WriteFloat4(bWriter, HDRRate);
                    WriteFloat4(bWriter, DAlphaParam);
                    WriteFloat4(bWriter, TexBlendAlpha1);
                    WriteFloat4(bWriter, TexBlendAlpha2);
                    WriteFloat4(bWriter, TexBlendScaleBias1);
                    WriteFloat4(bWriter, TexBlendScaleBias2);
                    WriteFloat4(bWriter, NormalTexBlendAlpha);
                    WriteFloat4(bWriter, NormalTexBlendScaleBias);
                    WriteFloat4(bWriter, ShadowCsBias);
                    WriteFloat4(bWriter, IDandFresnel);
                    WriteFloat4(bWriter, ReflectionColor);
                    WriteFloat4(bWriter, ReflectionVisibilityParam);
                    WriteFloat4(bWriter, EyePosition);
                    WriteFloat4(bWriter, EyePlane);
                    WriteFloat4(bWriter, IblDiffuseColor);
                    WriteFloat4(bWriter, IblSpecularColor);
                    WriteFloat4(bWriter, VelvetyRimParam);
                    WriteFloat4(bWriter, SpecularColor);
                    WriteFloat4(bWriter, SpecularVisibilityParam);
                    WriteFloat4(bWriter, VelvetyColor);
                    WriteFloat4(bWriter, RimColor);
                    WriteFloat4(bWriter, LScatterHighFreq);
                    WriteFloat4(bWriter, MaterialMulColor);
                    WriteFloat4(bWriter, MaterialAddColor);
                    WriteFloat4(bWriter, MaterialParam);
                    WriteFloat4(bWriter, MaterialParam2);

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool Load(String filename)
        {
            using (BinaryReader bReader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                try
                {
                    HDRRate = ReadFloat4(bReader);
                    DAlphaParam = ReadFloat4(bReader);
                    TexBlendAlpha1 = ReadFloat4(bReader);
                    TexBlendAlpha2 = ReadFloat4(bReader);
                    TexBlendScaleBias1 = ReadFloat4(bReader);
                    TexBlendScaleBias2 = ReadFloat4(bReader);
                    NormalTexBlendAlpha = ReadFloat4(bReader);
                    NormalTexBlendScaleBias = ReadFloat4(bReader);
                    ShadowCsBias = ReadFloat4(bReader);
                    IDandFresnel = ReadFloat4(bReader);
                    ReflectionColor = ReadFloat4(bReader);
                    ReflectionVisibilityParam = ReadFloat4(bReader);
                    EyePosition = ReadFloat4(bReader);
                    EyePlane = ReadFloat4(bReader);
                    IblDiffuseColor = ReadFloat4(bReader);
                    IblSpecularColor = ReadFloat4(bReader);
                    VelvetyRimParam = ReadFloat4(bReader);
                    SpecularColor = ReadFloat4(bReader);
                    SpecularVisibilityParam = ReadFloat4(bReader);
                    VelvetyColor = ReadFloat4(bReader);
                    RimColor = ReadFloat4(bReader);
                    LScatterHighFreq = ReadFloat4(bReader);
                    MaterialMulColor = ReadFloat4(bReader);
                    MaterialAddColor = ReadFloat4(bReader);
                    MaterialParam = ReadFloat4(bReader);
                    MaterialParam2 = ReadFloat4(bReader);

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        private void WriteFloat4(BinaryWriter bWriter, float[] wFloat)
        {
            bWriter.Write(wFloat[0]);
            bWriter.Write(wFloat[1]);
            bWriter.Write(wFloat[2]);
            bWriter.Write(wFloat[3]);
        }

        private float[] ReadFloat4(BinaryReader bReader)
        {
            float[] newFloat4 = new float[4];
            newFloat4[0] = bReader.ReadSingle();
            newFloat4[1] = bReader.ReadSingle();
            newFloat4[2] = bReader.ReadSingle();
            newFloat4[3] = bReader.ReadSingle();
            return newFloat4;
        }

    }
}
