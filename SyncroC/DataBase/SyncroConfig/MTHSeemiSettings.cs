using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class MTHSeemiProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public MTHSeemiProtocolRevisionConverter()
	    : base( )
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

	public class MTHSeemiSettings : SyncroBase 
  {

		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
		bool level1Present;
		bool level2Present;
		bool level3Present;
		string protocolVersion = null;
		BaseComms baseComms=null;


		[Category("2. MTH SEEMI SYNCRO TASK SETTINGS"), Description("Level1 Present.")]
		public bool Level1Present
		{
			get { return level1Present; }
			set { level1Present = value; }
		}

		[Category("2. MTH SEEMI SYNCRO TASK SETTINGS"), Description("Level2 Present.")]
		public bool Level2Present
		{
			get { return level2Present; }
			set { level2Present = value; }
		}

		[Category("2. MTH SEEMI SYNCRO TASK SETTINGS"), Description("Level3 Present.")]
		public bool Level3Present
		{
			get { return level3Present; }
			set { level3Present = value; }
		}

		[Category("2. MTH SEEMI SYNCRO TASK SETTINGS"), TypeConverter(typeof(MTHSeemiProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. MTH SEEMI Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public MTHSeemiSettings()
      : base()
    {
			this.sectionName = "S_MTH_SEEMI_Comms_HEADER"+ this.sectionName;
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			this.level1Present = true;
			this.level2Present = true;
			this.level3Present = false;
			this.protocolVersion =  "1.0";
			this.baseComms = new BaseComms("S_MTH_SEEMI_Comms");
    }
	}
	
}
