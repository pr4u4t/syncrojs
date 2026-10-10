using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class RSMRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

    public RSMRevisionConverter(): base( )
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

	public class RSMSettings : SyncroBase 
  {

		string protocolVersion = null;
    string coding = null;
		BaseComms baseComms=null;
    bool removeLeadingZerosFromRollID = false;

		[Category("2. RSM TASK SETTINGS"), TypeConverter(typeof(RSMRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. RSM Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

    [Category("2. RSM TASK SETTINGS"), Description("ASCII, UNICODE or a CodePage number")]
    public string Coding
    {
      get { return this.coding; }
      set { this.coding = value; }
    }

    [Category("2. RSM TASK SETTINGS"), Description("True to remove the leading zeros from the roll ID read by the bar code reader")]
    public bool RemoveLeadingZerosFromRollID
    {
      get { return this.removeLeadingZerosFromRollID; }
      set { this.removeLeadingZerosFromRollID = value; }
    }

    public RSMSettings(): base()
    {
      this.sectionName = "S_ROLL_STOCK_SYNCRO_HEADER" + this.sectionName;
			this.protocolVersion =  "0.1";
      this.coding = "ASCII";
      this.baseComms = new BaseComms("S_ROLL_STOCK_SYNCRO");
      this.removeLeadingZerosFromRollID = false;
    }
	}
	
}
