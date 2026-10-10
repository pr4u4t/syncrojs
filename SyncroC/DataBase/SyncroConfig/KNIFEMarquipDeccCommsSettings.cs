using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
    public class KnifeMarquipDeccProtocolRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public KnifeMarquipDeccProtocolRevisionConverter()
            : base()
        {
            this.protocolRevision = new string[] { "0.1" };
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

    public class KNIFE_MarquipDeccCommsSettings : SyncroBase
    {

        //RFazzi_FI17/01/2008_1.Added task configurations on graphics
        string protocolVersion = null;
        BaseComms baseComms = null;

        [Category("2. KNIFEMarquipDecc Comms Settings TASK SETTINGS"), TypeConverter(typeof(KnifeMarquipDeccProtocolRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }

        [Category("3. KNIFEMarquipDecc Comms SETTINGS"), Description("Comms settings")]
        public BaseComms BaseComms
        {
            get { return baseComms; }
            set { baseComms = value; }
        }

        public KNIFE_MarquipDeccCommsSettings()
            : base()
        {
            this.sectionName = "S_KNIFE_MARQUIP_DECC_Comms" + this.sectionName;
            //RFazzi_FI17/01/2008_1.Added task configurations on graphics
            this.protocolVersion = "0.1";
            this.baseComms = new BaseComms("S_KNIFE_MARQUIP_DECC_Comms");
        }
    }

}
