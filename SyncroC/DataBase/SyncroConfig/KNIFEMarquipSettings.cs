using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC - Electrical Task
    public class KNIFEMarquipProtocolRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public KNIFEMarquipProtocolRevisionConverter()
            : base()
        {
            this.protocolRevision = new string[] { "1.0" };
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(this.protocolRevision);
        }
    }

    public class KNIFE_MarquipSettings : TaskSettings
    {
        bool enable;
        string protocolVersion;

        [Category("2. KNIFEMarquip Settings TASK SETTINGS"), Description("Determine whether the KNIFEMarquip task should be automatically started.")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        [Category("2. KNIFEMarquip Settings TASK SETTINGS"), TypeConverter(typeof(KNIFEMarquipProtocolRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }
        public KNIFE_MarquipSettings()
            : base()
        {
            this.sectionName = "S_KNIFE_MARQUIP" + this.sectionName;
            this.enable = false;
            this.ProtocolVersion = "1.0";
        }
    }
}
