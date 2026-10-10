using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
    public class STK_GenericCTRLProtocolRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public STK_GenericCTRLProtocolRevisionConverter()
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

    public class STK_GenericCTRLSettings : TaskSettings
    {
        bool enable;
        string protocolVersion;

        [Category("2. STACKER_GenericCTRL Settings TASK SETTINGS"), Description("Determine whether the STACKER_GenericCTRL task should be automatically started.")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        [Category("2. STACKER_GenericCTRL Settings TASK SETTINGS"), TypeConverter(typeof(STK_GenericCTRLProtocolRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }
        public STK_GenericCTRLSettings(string level)
            : base()
        {
            this.sectionName = "S_STK_GENERIC_CTRL" + level + this.sectionName;
            this.enable = false;
            this.ProtocolVersion = "0.0";
        }
    }
}
