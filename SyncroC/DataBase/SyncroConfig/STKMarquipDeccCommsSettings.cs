using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
    //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
    public class StkMarquipDeccProtocolRevisionConverter : StringConverter
    {
        private string[] protocolRevision = null;

        public StkMarquipDeccProtocolRevisionConverter()
            : base()
        {
            this.protocolRevision = new string[] { "0.1" , "0.2"};
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

    public class STK_MarquipDeccCommsSettings : SyncroBase
    {
        string protocolVersion = null;
        bool[] level_enable = new bool[] { true, false, false };
        BaseComms baseComms = null;

        [Category("2. STACKERMarquipDecc Comms Settings TASK SETTINGS"), TypeConverter(typeof(StkMarquipDeccProtocolRevisionConverter)), Description("Protocol Revision")]
        public string ProtocolVersion
        {
            get { return this.protocolVersion; }
            set { this.protocolVersion = value; }
        }

        [Category("2. STACKERMarquipDecc Comms Settings TASK SETTINGS"), Description("Enable Level 1")]
        public bool EnableLevel1
        {
          get { return this.level_enable[0]; }
          set { this.level_enable[0] = value; }
        }

        [Category("2. STACKERMarquipDecc Comms Settings TASK SETTINGS"), Description("Enable Level 2")]
        public bool EnableLevel2
        {
          get { return this.level_enable[1]; }
          set { this.level_enable[1] = value; }
        }

        [Category("2. STACKERMarquipDecc Comms Settings TASK SETTINGS"), Description("Enable Level 3")]
        public bool EnableLevel3
        {
          get { return this.level_enable[2]; }
          set { this.level_enable[2] = value; }
        }

        [Category("3. STACKERMarquipDecc Comms SETTINGS"), Description("Comms settings")]
        public BaseComms BaseComms
        {
            get { return baseComms; }
            set { baseComms = value; }
        }

        public STK_MarquipDeccCommsSettings()
            : base()
        {
            this.sectionName = "S_STK_MARQUIP_DECC_Comms_HEADER" + this.sectionName;
            //RFazzi_FI17/01/2008_1.Added task configurations on graphics
            this.protocolVersion = "0.1";
            this.level_enable[0] = true;
            this.level_enable[1] = false;
            this.level_enable[2] = false;
            this.baseComms = new BaseComms("S_STK_MARQUIP_DECC_Comms");
        }
    }

}
