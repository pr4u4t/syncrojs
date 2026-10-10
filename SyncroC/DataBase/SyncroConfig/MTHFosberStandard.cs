using System; //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
  public class MTHFosberStandardCommsSettingsProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public MTHFosberStandardCommsSettingsProtocolRevisionConverter()
      : base( )
    {
      //PMASSARO_GRAFFAELLI_FI_17/06/2011_1 - Aggiunto protocollo comunicazione per inviare i dati a EMMEPI GROUP
      //this.protocolRevision = new string[] { "1.0" };
      this.protocolRevision = new string[] { "1.0", "MP_V0.0" };
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

  [TypeConverter(typeof(Converters))]
  public class MTHFosberStandardParameter : SyncroBase
  {

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
      set { baseComms = value ; }
    }

    public MTHFosberStandardParameter(string level)
      : base()
      {
        this.sectionName = "S_MTH_FOSBER_COMMS" + level + "_HEADER" + this.sectionName;
        this.protocolVersion = "1.0";
        this.baseComms = new BaseComms("S_MTH_FOSBER_COMMS" + level);
      }
   }

  public class MTHFosberStandardCommsSettings : SyncroBase
  {

    MTHFosberStandardParameter mth_01 = null;
    MTHFosberStandardParameter mth_02 = null;
    MTHFosberStandardParameter mth_03 = null;

    [Category("01. MTH Fosber Standard Comms SETTINGS Level 1"), Description("MTH Fosber Standard Settings Level 1")]
    public MTHFosberStandardParameter Mth_01
    {
      get { return mth_01; }
      set { mth_01 = value; }
    }

    [Category("02. MTH Fosber Standard Comms SETTINGS Level 2"), Description("MTH Fosber Standard Settings Level 2")]
    public MTHFosberStandardParameter Mth_02
    {
      get { return mth_02; }
      set { mth_02 = value; }
    }

    [Category("03. MTH Fosber Standard Comms SETTINGS Level 3"), Description("MTH Fosber Standard Settings Level 3")]
    public MTHFosberStandardParameter Mth_03
    {
      get { return mth_03; }
      set { mth_03 = value; }
    }

    public MTHFosberStandardCommsSettings()
      : base()
    {
      this.sectionName = "MTH_FOSBER";
      this.mth_01 = new MTHFosberStandardParameter("1");
      this.mth_02 = new MTHFosberStandardParameter("2");
      this.mth_03 = new MTHFosberStandardParameter("3");
		}
  }
}