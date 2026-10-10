using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    public class STKStackerUniversalRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public STKStackerUniversalRevisionConverter()
            : base()
        {
            this.protocolRevision = new string[] { "1.0"};
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
    public class STKStackerUniversalSettings : TaskSettings
    {
        bool enable;
        string protocolVersion = null;

        [Category("2. STK Stacker Universal TASK SETTINGS"), Description("Determine whether the STK Universal task should be automatically started.")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        [Category("2. STK Stacker Universal TASK SETTINGS"), TypeConverter(typeof(STKStackerUniversalRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }

        public STKStackerUniversalSettings()
            : base()
        {
            this.sectionName = "S_STK_UNIVERSAL" + this.sectionName;
            this.protocolVersion = "1.0";
            this.enable = true;
        }
    }
}
