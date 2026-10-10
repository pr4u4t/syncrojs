using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    public class SSCompact22_2XMacCommsProtocolRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public SSCompact22_2XMacCommsProtocolRevisionConverter()
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

    public class SSCompact22_2XMacCommsSettings : SyncroBase
    {
        BaseComms baseComms = null;
        string protocolVersion = null;

        private bool enable;
        [Category("2. SS Compact 22.2X Mac Comms TASK SETTINGS")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        [Category("3. SS Compact 22.2X Mac Comms TASK SETTINGS"), TypeConverter(typeof(SSCompact22_2XMacCommsProtocolRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }

        [Category("4. SS Compact 22.2X Mac Comms TASK SETTINGS"), Description("Comms settings")]
        public BaseComms BaseComms
        {
            get { return baseComms; }
            set { baseComms = value; }
        }

        public SSCompact22_2XMacCommsSettings()
            : base()
        {
            this.sectionName = "S_SS_FOSBER_COMPACT_Comms" + this.sectionName;
            this.protocolVersion = "1.0";
            this.enable = false;
            this.baseComms = new BaseComms("S_SS_FOSBER_COMPACT_Comms");
        }
    }

}
