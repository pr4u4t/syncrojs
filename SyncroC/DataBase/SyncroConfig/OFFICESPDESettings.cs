using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class OFFICESPDERevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public OFFICESPDERevisionConverter()
	    : base( )
	    {
        //PMASSARO_FI22/09/09 - Added command SB as in SPDE program
        /*
          this.protocolRevision = new string[] { "0.0", "0.1", "0.2" };
        */
        //agrassi_FI02-02-2010_1
        //this.protocolRevision = new string[] { "0.0", "0.1", "0.2", "0.3" };
        this.protocolRevision = new string[] { "0.0", "0.1", "0.2", "0.3", "0.4"};
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

	public class OFFICESPDESettings : SyncroBase 
  {

		string protocolVersion = null;
    //agrassi_FI22-10-08
    string coding = null;
		BaseComms baseComms=null;

		[Category("2. OFFICE SPDE TASK SETTINGS"), TypeConverter(typeof(OFFICESPDERevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. OFFICE SPDE Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

    //agrassi_FI22-10-08
    [Category("2. OFFICE SPDE TASK SETTINGS"), Description("ASCII, UNICODE or a CodePage number")]
    public string Coding
    {
      get { return this.coding; }
      set { this.coding = value; }
    }

		public OFFICESPDESettings()
      : base()
    {
			this.sectionName = "S_OFFICESPDE_HEADER" + this.sectionName;
      //PMASSARO_FI22/09/09 - Added command SB as in SPDE program
			/*
      this.protocolVersion =  "0.1";
      */
      this.protocolVersion = "0.3";
      //agrassi_FI22-10-08
      this.coding = "UNICODE";
			this.baseComms = new BaseComms("S_OFFICESPDE");
    }
	}
	
}
