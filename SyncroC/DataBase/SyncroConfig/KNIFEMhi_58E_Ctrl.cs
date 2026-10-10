using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

//PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
namespace SyncroC.DataBase.SyncroConfig
{
  public class KNIFE_MHI_58E_CtrlProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public KNIFE_MHI_58E_CtrlProtocolRevisionConverter()
      : base()
    {
      this.protocolRevision = new string[] { "0.0" };
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

  public class KNIFE_MHI_58E_CtrlSettings:TaskSettings 
  {
    string protocolVersion = null;
    bool enable;
    int defaultCutLenght;
    int ticksBeforeSendNext;
    bool enableLevel1;
    bool enableLevel2;

    [Category("2. KNIFE MHI 58E Ctrl Settings TASK SETTINGS"), TypeConverter(typeof(KNIFE_MHI_58E_CtrlProtocolRevisionConverter)), Description("Protocol Revision")]
    public string ProtocolVersion
    {
      get { return this.protocolVersion; }
      set { this.protocolVersion = value; }
    }

    [Category("2. KNIFE MHI 58E Ctrl Settings TASK SETTINGS"), TypeConverter(typeof(Boolean)), Description("Enable Task")]
    public bool Enable
    {
      get { return this.enable; }
      set { this.enable = value; }
    }

    [Category("2. KNIFE MHI 58E Ctrl Settings TASK SETTINGS"), TypeConverter(typeof(int)), Description("Default Cut Length")]
    public int DefaultCutLenght
    {
      get { return this.defaultCutLenght; }
      set { this.defaultCutLenght = value; }
    }

    [Category("2. KNIFE MHI 58E Ctrl Settings TASK SETTINGS"), TypeConverter(typeof(int)), Description("Ticks Before Send Next")]
    public int TicksBeforeSendNext
    {
      get { return this.ticksBeforeSendNext; }
      set { this.ticksBeforeSendNext = value; }
    }

    [Category("2. KNIFE MHI 58E Ctrl Settings TASK SETTINGS"), TypeConverter(typeof(Boolean)), Description("Enable Level 1 - LOWER")]
    public bool EnableLevel1
    {
      get { return this.enableLevel1; }
      set { this.enableLevel1 = value; }
    }

    [Category("2. KNIFE MHI 58E Ctrl Settings TASK SETTINGS"), TypeConverter(typeof(Boolean)), Description("Enable Level 2 - UPPER")]
    public bool EnableLevel2
    {
      get { return this.enableLevel2; }
      set { this.enableLevel2 = value; }
    }

    public KNIFE_MHI_58E_CtrlSettings()
      : base()
    {
      this.sectionName = "S_KNIFE_MHI_58E_Ctrl_HEADER";
      this.protocolVersion = "0.0";
      this.enable = false;
      this.ticksBeforeSendNext = 2000;
      this.defaultCutLenght = 738;
      this.enableLevel1 = true;
      this.enableLevel2 = false;
    }
  }
}
