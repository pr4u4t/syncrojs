using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSTwin22_40CommsProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

    public SSTwin22_40CommsProtocolRevisionConverter()
	    : base( )
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

	public class SSTwin22_40CommsSettings : TaskSettings 
  {

		string protocolVersion = null;
    BaseSocketComms mainPlcBaseComms = null;
    BaseSocketComms kkPlcBaseComms = null;
    BaseSocketComms triaxMeasuresBaseComms = null;
    BaseSocketComms triaxSlit1BaseComms = null;
    BaseSocketComms triaxSlit2BaseComms = null;
    BaseSocketComms triaxChutesBaseComms = null;

		[Category("2. SS Twin22.40 SYNCRO TASK SETTINGS"), TypeConverter(typeof(SSTwin22_40CommsProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		private bool enable;
    [Category("2. SS Twin22.40 SYNCRO TASK SETTINGS")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

    int nodeNumberMainPLC;
    [Category("2. SS Twin22.40 Settings TASK SETTINGS"), Description("Node number of the main PLC")]
    public int NodeNumberMainPLC
    {
      get { return nodeNumberMainPLC; }
      set { nodeNumberMainPLC = value; }
    }

   int nodeNumberKKPLC;
   [Category("2. SS Twin22.40 Settings TASK SETTINGS"), Description("Node number of the KK PLC")]
   public int NodeNumberKKPLC
   {
     get { return nodeNumberKKPLC; }
     set { nodeNumberKKPLC = value; }
   }

   [Category("3. SS Twin22.40 PLC Comms SETTINGS"), Description("Main PLC Comms settings")]
		public BaseSocketComms MainPLCBaseComms
		{
			get { return mainPlcBaseComms; }
      set { mainPlcBaseComms = value; }
		}

    [Category("3. SS Twin22.40 PLC Comms SETTINGS"), Description("KK PLC Comms settings")]
    public BaseSocketComms KKPLCBaseComms
    {
      get { return kkPlcBaseComms; }
      set { kkPlcBaseComms = value; }
    }

    [Category("4. SS Twin22.40 TRIAX Comms SETTINGS"), Description("TRIAX Measures Comms settings")]
		public BaseSocketComms TRIAXMeasuresBaseComms
		{
			get { return triaxMeasuresBaseComms; }
			set { triaxMeasuresBaseComms = value; }
		}

    [Category("4. SS Twin22.40 TRIAX Comms SETTINGS"), Description("TRIAX Slit1 Comms settings")]
    public BaseSocketComms TRIAXSlit1BaseComms
    {
      get { return triaxSlit1BaseComms; }
      set { triaxSlit1BaseComms = value; }
    }

    [Category("4. SS Twin22.40 TRIAX Comms SETTINGS"), Description("TRIAX Slit2 Comms settings")]
    public BaseSocketComms TRIAXSlit2BaseComms
    {
      get { return triaxSlit2BaseComms; }
      set { triaxSlit2BaseComms = value; }
    }

    [Category("4. SS Twin22.40 TRIAX Comms SETTINGS"), Description("TRIAX chutes Comms settings")]
    public BaseSocketComms TRIAXChutesBaseComms
    {
      get { return triaxChutesBaseComms; }
      set { triaxChutesBaseComms = value; }
    }

    public SSTwin22_40CommsSettings()
      : base()
    {
			this.sectionName = "S_SS_FOSBER_22.40_Comms" + this.sectionName;
			this.protocolVersion =  "1.0";
			this.enable = false;
      this.mainPlcBaseComms = new BaseSocketComms("S_SS_FOSBER_22.40_Comms_MAIN_PLC");
      this.kkPlcBaseComms = new BaseSocketComms("S_SS_FOSBER_22.40_Comms_KK_PLC");
      this.triaxMeasuresBaseComms = new BaseSocketComms("S_SS_FOSBER_22.40_Comms_TRIAX_MEASURES");
      this.triaxSlit1BaseComms = new BaseSocketComms("S_SS_FOSBER_22.40_Comms_TRIAX_SLIT_1");
      this.triaxSlit2BaseComms = new BaseSocketComms("S_SS_FOSBER_22.40_Comms_TRIAX_SLIT_2");
      this.triaxChutesBaseComms = new BaseSocketComms("S_SS_FOSBER_22.40_Comms_TRIAX_CHUTES");
    }
	}
	
}
