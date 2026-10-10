using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
	public class OHDProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public OHDProtocolRevisionConverter()
	    : base( )
	  {
			this.protocolRevision = new string[] { "AESYS_1_0","AESYS_2_0" ,"WIBOND_1_0","WIBOND_2_0", "FOSBER_1_0"};
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

  public class OHDLanguage : StringConverter
  {
    private string[] language = null;

    public OHDLanguage()
      : base()
    {
      this.language = new string[] { "English", "Italian", "German", "Spanish", "Dutch", "Czech", "French", "Portuguese", "Polish", "Finnish", "Swedish", "Chinese", "Turkish", "Russian", "Norwegian"};
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.language);
    }
  }

	[TypeConverter(typeof(Converters))]
	public class OHDParameter : SyncroBase 
  {
    string language;
    [TypeConverter(typeof(OHDLanguage)), Description("La lingua da usare (NB usato solo per tabellone LCD FOSBER).")]
    public string Language
    {
      get { return language; }
      set { language = value; }
    }

		string protocolVersion = null;
		BaseComms baseComms=null;

		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public OHDParameter(string level)
      : base()
    {
			this.sectionName = "S_OHD" + level + "_HEADER" + this.sectionName;
			this.protocolVersion = "WIBOND_2_0";
			this.baseComms = new BaseComms("S_OHD" + level);

      this.language = "English";
    }
	}

	public class OHDSettings : SyncroBase
	{

		OHDParameter ohd_01 = null;
		OHDParameter ohd_02 = null;
		OHDParameter ohd_03 = null;
		OHDParameter ohd_04 = null;
		OHDParameter ohd_05 = null;
		OHDParameter ohd_06 = null;
		OHDParameter ohd_07 = null;
		OHDParameter ohd_08 = null;
		OHDParameter ohd_09 = null;
		OHDParameter ohd_10 = null;

		[Category("01. OHD Nr 01 Settings"), Description("OHD Nr 1 Settings")]
		public OHDParameter Ohd_01
		{
			get { return ohd_01; }
			set { ohd_01 = value; }
		}

		[Category("02. OHD Nr 02 Settings"), Description("OHD Nr 2 Settings")]
		public OHDParameter Ohd_02
		{
			get { return ohd_02; }
			set { ohd_02 = value; }
		}

		[Category("03. OHD Nr 03 Settings"), Description("OHD Nr 3 Settings")]
		public OHDParameter Ohd_03
		{
			get { return ohd_03; }
			set { ohd_03 = value; }
		}

		[Category("04. OHD Nr 04 Settings"), Description("OHD Nr 4 Settings")]
		public OHDParameter Ohd_04
		{
			get { return ohd_04; }
			set { ohd_04 = value; }
		}

		[Category("05. OHD Nr 05 Settings"), Description("OHD Nr 5 Settings")]
		public OHDParameter Ohd_05
		{
			get { return ohd_05; }
			set { ohd_05 = value; }
		}

		[Category("06. OHD Nr 06 Settings"), Description("OHD Nr 6 Settings")]
		public OHDParameter Ohd_06
		{
			get { return ohd_06; }
			set { ohd_06 = value; }
		}
		[Category("07. OHD Nr 07 Settings"), Description("OHD Nr 7 Settings")]
		public OHDParameter Ohd_07
		{
			get { return ohd_07; }
			set { ohd_07 = value; }
		}

		[Category("08. OHD Nr 08 Settings"), Description("OHD Nr 8 Settings")]
		public OHDParameter Ohd_08
		{
			get { return ohd_08; }
			set { ohd_08 = value; }
		}

		[Category("09. OHD Nr 09 Settings"), Description("OHD Nr 9 Settings")]
		public OHDParameter Ohd_09
		{
			get { return ohd_09; }
			set { ohd_09 = value; }
		}

		[Category("10. OHD Nr 10 Settings"), Description("OHD Nr 10 Settings")]
		public OHDParameter Ohd_10
		{
			get { return ohd_10; }
			set { ohd_10 = value; }
		}

		public OHDSettings()
      : base()
    {
			this.sectionName = "OHD";
			this.ohd_01 = new OHDParameter("1" );
			this.ohd_02 = new OHDParameter("2");
			this.ohd_03 = new OHDParameter("3");
			this.ohd_04 = new OHDParameter("4");
			this.ohd_05 = new OHDParameter("5");
			this.ohd_06 = new OHDParameter("6");
			this.ohd_07 = new OHDParameter("7");
			this.ohd_08 = new OHDParameter("8");
			this.ohd_09 = new OHDParameter("9");
			this.ohd_10 = new OHDParameter("10");
		}
	}
	
}
