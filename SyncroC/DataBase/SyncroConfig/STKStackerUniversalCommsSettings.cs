using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    public class STKStackerUniversalCtrlProtocolRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public STKStackerUniversalCtrlProtocolRevisionConverter()
            : base()
        {
            this.protocolRevision = new string[] { "1.0", "2.0" };
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

    public class STKStackerUniversalCtrlSettings : SyncroBase
    {

        string protocolVersion = null;
        BaseComms baseComms = null;

        [Category("2. STK Stacker Universal Ctrl SYNCRO TASK SETTINGS"), TypeConverter(typeof(STKStackerUniversalCtrlProtocolRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }

        [Category("3. STK Stacker Universal Ctrl Ctrl SETTINGS"), Description("Ctrl settings")]
        public BaseComms BaseComms
        {
            get { return baseComms; }
            set { baseComms = value; }
        }

        public STKStackerUniversalCtrlSettings(string level)
            : base()
        {
            this.sectionName = "S_STK_UNIVERSAL_CTRL" + level + "_HEADER" + this.sectionName;
            this.protocolVersion = "1.0";
            this.baseComms = new BaseComms("S_STK_UNIVERSAL_CTRL" + level);
        }
    }

}
