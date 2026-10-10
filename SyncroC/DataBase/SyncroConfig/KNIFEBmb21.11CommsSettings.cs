using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class Knife21_11BmbProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public Knife21_11BmbProtocolRevisionConverter()
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

	public class KNIFE21_11BmbCommsSettings:TaskSettings 
  {

		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
		bool enable;
		bool enableLevel1;
		bool enableLevel2;
		bool enableLevel3;
		int defaultCutLenght;
		string protocolVersion = null;

		ThredBaseComms baseLevel1 = null;
		ThredBaseComms baseLevel2 = null;
		ThredBaseComms baseLevel3 = null;

		[Category("2. KNIFE21_11 Bmb Comms Settings TASK SETTINGS"), Description("Determine whether the KNIFE21_11 Comms Bmb task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFE21_11 Bmb Comms Settings TASK SETTINGS"), TypeConverter(typeof(Knife21_11BmbProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("2. KNIFE21_11 Bmb Comms Settings TASK SETTINGS"), Description("Level1 Present.")]
		public bool EnableLevel1
		{
			get { return enableLevel1; }
			set { enableLevel1 = value; }
		}

		[Category("2. KNIFE21_11 Bmb Comms Settings TASK SETTINGS"), Description("Level2 Present.")]
		public bool EnableLevel2
		{
			get { return enableLevel2; }
			set { enableLevel2 = value; }
		}

		[Category("2. KNIFE21_11 Bmb Comms Settings TASK SETTINGS"), Description("Level3 Present.")]
		public bool EnableLevel3
		{
			get { return enableLevel3; }
			set { enableLevel3 = value; }
		}

		[Category("2. KNIFE21_11 Bmb Comms Settings TASK SETTINGS"), Description("Cut Lenght when the level not working")]
		public int DefaultCutLenght
		{
			get { return defaultCutLenght; }
			set { defaultCutLenght = value; }
		}		
		
		[Category("3. KNIFE BMB 21.11 LEVEL 1 SYNCRO TASK SETTINGS"), Description("Comms Settings for Level 1")]
		public ThredBaseComms BaseLevel1
		{
			get { return baseLevel1; }
			set { baseLevel1 = value; }
		}

		[Category("4. KNIFE BMB 21.11 LEVEL 2 SYNCRO TASK SETTINGS"), Description("Comms Settings for Level 2")]
		public ThredBaseComms BaseLevel2
		{
			get { return baseLevel2; }
			set { baseLevel2 = value; }
		}

		[Category("5. KNIFE BMB 21.11 LEVEL 3 SYNCRO TASK SETTINGS"), Description("Comms Settings for Level 3")]
		public ThredBaseComms BaseLevel3
		{
			get { return baseLevel3; }
			set { baseLevel3 = value; }
		}

		public KNIFE21_11BmbCommsSettings()
      : base()
    {
            this.sectionName = "S_KNIFE_FOSBER_21.11_BMB_Comms" + this.sectionName;
			this.enable = false;
			this.protocolVersion =  "1.0";
			this.enableLevel1=true;
			this.enableLevel2 = true;
			this.enableLevel3 = false;
			this.defaultCutLenght = 1000;
            this.baseLevel1 = new ThredBaseComms("S_KNIFE1_FOSBER_21.11_BMB_Comms");
            this.baseLevel2 = new ThredBaseComms("S_KNIFE2_FOSBER_21.11_BMB_Comms");
            this.baseLevel3 = new ThredBaseComms("S_KNIFE3_FOSBER_21.11_BMB_Comms");
			
    }
	}
	
}
