using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class Knife21_20DigitecProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public Knife21_20DigitecProtocolRevisionConverter()
	    : base( )
	  {
      //agrassi_FI18-01-2010_1
      //this.protocolRevision = new string[] { "0.3", "0.4" };
      this.protocolRevision = new string[] { "0.2", "0.3", "0.4" };
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

	public class KNIFE21_20DigitecCommsSettings:TaskSettings 
  {

		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
		bool enable;
		bool enableLevel1_2;
		bool enableLevel3;
		int defaultCutLenght;
		int ticksBeforeSendNext;
		string protocolVersion = null;

		ThredBaseComms baseLevel1_2 = null;
		ThredBaseComms baseLevel3 = null;

		[Category("2. KNIFE21_20 Comms Settings TASK SETTINGS"), Description("Determine whether the KNIFE21_20 Comms Digitec task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFE21_20 Comms Settings TASK SETTINGS"), TypeConverter(typeof(Knife21_20DigitecProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("2. KNIFE21_20 Comms Settings TASK SETTINGS"), Description("Level1 and 2 Present.")]
		public bool EnableLevel1And2
		{
			get { return enableLevel1_2; }
			set { enableLevel1_2 = value; }
		}

		[Category("2. KNIFE21_20 Comms Settings TASK SETTINGS"), Description("Level3 Present.")]
		public bool EnableLevel3
		{
			get { return enableLevel3; }
			set { enableLevel3 = value; }
		}

		[Category("2. KNIFE21_20 Comms Settings TASK SETTINGS"), Description("Cut Lenght when the level not working")]
		public int DefaultCutLenght
		{
			get { return defaultCutLenght; }
			set { defaultCutLenght = value; }
		}

		[Category("2. KNIFE21_20 Comms Settings TASK SETTINGS"), Description("Delay for send next order to the machine")]
		public int TicksBeforeSendNext
		{
			get { return ticksBeforeSendNext; }
			set { ticksBeforeSendNext = value; }
		}

		[Category("3. KNIFE21_20 LEVEL 1&2 SYNCRO TASK SETTINGS"), Description("Comms Settings for Level 1")]
		public ThredBaseComms BaseLevel1_2
		{
			get { return baseLevel1_2; }
			set { baseLevel1_2 = value; }
		}

		[Category("4. KNIFE21_20 LEVEL 3 SYNCRO TASK SETTINGS"), Description("Comms Settings for Level 3")]
		public ThredBaseComms BaseLevel3
		{
			get { return baseLevel3; }
			set { baseLevel3 = value; }
		}

		public KNIFE21_20DigitecCommsSettings()
      : base()
    {
			this.sectionName = "S_KNIFE_FOSBER_21.20_Ctrl" + this.sectionName;
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			this.enable = false;
			this.protocolVersion =  "2.0";
			this.enableLevel1_2=true;
			this.enableLevel3 = false;
			this.defaultCutLenght = 300;
			this.ticksBeforeSendNext = 2000;
			this.baseLevel1_2 = new ThredBaseComms("S_KNIFE1And2_FOSBER_21.20_Ctrl");
			this.baseLevel3 = new ThredBaseComms("S_KNIFE3_FOSBER_21.20_Ctrl");
			
    }
	}
	
}
