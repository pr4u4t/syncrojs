using System;
using System.Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;

namespace SyncroC.DataBase.SyncroConfig
{
  public class SyncroConfig : SyncroC.DataBase.SyncroConfig.SyncroBase
  {
#if DEBUG
    [DllImport(@"Utility.dll", CallingConvention = CallingConvention.Cdecl)]
#else
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#endif
    public static extern int FileCrc32Assembly([MarshalAs(UnmanagedType.LPTStr)] string szFilename, ref int dwCrc32);

    /// <summary>String to be sent as version in case of error</summary>
    public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;


    private DBMSSettings dbmsSetting;
    public DBMSSettings DbmsSetting
    {
      get { return dbmsSetting; }
      set { dbmsSetting = value; }
    }

    private GeneralSettings generalSetting;
    public GeneralSettings GeneralSetting
    {
      get { return generalSetting; }
      set { generalSetting = value; }
    }

    private WECSyncroSettings wecSyncroSetting;
    public WECSyncroSettings WecSyncroSetting
    {
      get { return wecSyncroSetting; }
      set { wecSyncroSetting = value; }
    }

    private WECBMUSettings wecBMUSetting;
    public WECBMUSettings WecBMUSetting
    {
      get { return wecBMUSetting; }
      set { wecBMUSetting = value; }
    }

    private MTHDuckerSettings mthDuckerSetting;
    public MTHDuckerSettings MTHDuckerSetting
    {
      get { return mthDuckerSetting; }
      set { mthDuckerSetting = value; }
    }

		//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
		private MTHSeemiSettings mthSeemiSetting;
		public MTHSeemiSettings MTHSeemiSetting
		{
			get { return mthSeemiSetting; }
			set { mthSeemiSetting = value; }
		}

    private MTHCommsSettings mthCommsSetting;
    public MTHCommsSettings MTHCommsSetting
    {
      get { return mthCommsSetting; }
      set { mthCommsSetting = value; }
    }

    //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
    private MTHFosberStandardCommsSettings mthFosberStandardCommsSettings;
    public MTHFosberStandardCommsSettings MthFosberStandardCommsSettings
    {
      get { return mthFosberStandardCommsSettings; }
      set { mthFosberStandardCommsSettings = value; }
    }

    //agrassi_FI22-10-08
    private MTHFosberMindaCommsSettings mthFosberMindaCommsSetting;
    public MTHFosberMindaCommsSettings MthFosberMindaCommsSetting
    {
      get { return mthFosberMindaCommsSetting; }
      set { mthFosberMindaCommsSetting = value; }
    }

    //RFazzi_FI17/01/2008_1.Added task configurations on graphics
    private KNIFE21_11DigitecCommsSettings knifeDigitec21_11CommsSetting;
    public KNIFE21_11DigitecCommsSettings KNIFEDigitec21_11CommsSetting
    {
      get { return knifeDigitec21_11CommsSetting; }
      set { knifeDigitec21_11CommsSetting = value; }
    }

    private KNIFE21_11DigitecSettings knifeDigitec21_11Setting;
    public KNIFE21_11DigitecSettings KNIFEDigitec21_11Setting
    {
      get { return knifeDigitec21_11Setting; }
      set { knifeDigitec21_11Setting = value; }
    }

    private KNIFE21_20DigitecSettings knifeDigitec21_20Setting;
    public KNIFE21_20DigitecSettings KNIFEDigitec21_20Setting
    {
      get { return knifeDigitec21_20Setting; }
      set { knifeDigitec21_20Setting = value; }
    }

    private KNIFE21_20DigitecCommsSettings knifeDigitec21_20CommsSetting;
    public KNIFE21_20DigitecCommsSettings KNIFEDigitec21_20CommsSetting
    {
      get { return knifeDigitec21_20CommsSetting; }
      set { knifeDigitec21_20CommsSetting = value; }
    }

    private KNIFE21_30Settings knife21_30Setting;
    public KNIFE21_30Settings KNIFE21_30Setting
    {
      get { return knife21_30Setting; }
      set { knife21_30Setting = value; }
    }

    private KNIFE21_30CommsSettings knife21_30CommsSetting;
    public KNIFE21_30CommsSettings KNIFE21_30CommsSetting
    {
      get { return knife21_30CommsSetting; }
      set { knife21_30CommsSetting = value; }
    }

    //agrassi_FI18-03-2010_1
    private KnifeBSCOMGapoCommsSettings knifeBSCOMGapoCommsSetting;
    public KnifeBSCOMGapoCommsSettings KnifeBSCOMGapoCommsSetting
    {
      get { return knifeBSCOMGapoCommsSetting; }
      set { knifeBSCOMGapoCommsSetting = value; }
    }
    //mlamioni_FI30-04-2010_1
    private KnifeBHSSettings knifeBHSSetting;
    public KnifeBHSSettings KnifeBHSSetting
    {
      get { return knifeBHSSetting; }
      set { knifeBHSSetting = value; }
    }

    private RSHFosber23_XXSettings rshFosber23_XXSetting;
    public RSHFosber23_XXSettings RSHFosber23_XXSetting
    {
      get { return rshFosber23_XXSetting; }
      set { rshFosber23_XXSetting = value; }
    }

    //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
    private RSHMhi_Settings rshMhi_Setting;
    public RSHMhi_Settings RSHMhi_Setting
    {
      get { return rshMhi_Setting; }
      set { rshMhi_Setting = value; }
    }

    //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
    private KNIFE_MarquipDeccCommsSettings knifeMarquipDeccCommsSetting;
    public KNIFE_MarquipDeccCommsSettings KNIFEMarquipDeccCommsSetting
    {
      get { return knifeMarquipDeccCommsSetting; }
      set { knifeMarquipDeccCommsSetting = value; }
    }

    //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC - Electrical Task
    private KNIFE_MarquipSettings knifeMarquipSetting;
    public KNIFE_MarquipSettings KNIFEMarquipSetting
    {
      get { return knifeMarquipSetting; }
      set { knifeMarquipSetting = value; }
    }

    //ABorelli_FI01/06/2008_1
    private KNIFE21_11BmbCommsSettings knifeBmb21_11CommsSetting;
    public KNIFE21_11BmbCommsSettings KNIFEBmb21_11CommsSetting
    {
      get { return knifeBmb21_11CommsSetting; }
      set { knifeBmb21_11CommsSetting = value; }
    }

    private KNIFE21_11BmbSettings knifeBmb21_11Setting;
    public KNIFE21_11BmbSettings KNIFEBmb21_11Setting
    {
      get { return knifeBmb21_11Setting; }
      set { knifeBmb21_11Setting = value; }
    }
    private OFFICESPDESettings officeSPDESetting;
    public OFFICESPDESettings OfficeSPDESetting
    {
      get { return officeSPDESetting; }
      set { officeSPDESetting = value; }
    }

    //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
    private FanFoldHOETTENCommsSettings fanfoldHoettenCommsSettings1;
    public FanFoldHOETTENCommsSettings FanFoldHoettenCommsSettings1
    {
      get { return fanfoldHoettenCommsSettings1; }
      set { fanfoldHoettenCommsSettings1 = value; }
    }
    
    //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
    private FanFoldHOETTENCommsSettings fanfoldHoettenCommsSettings2;
    public FanFoldHOETTENCommsSettings FanFoldHoettenCommsSettings2
    {
      get { return fanfoldHoettenCommsSettings2; }
      set { fanfoldHoettenCommsSettings2 = value; }
    }
    
    //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
    private FanFoldHOETTENCommsSettings fanfoldHoettenCommsSettings3;
    public FanFoldHOETTENCommsSettings FanFoldHoettenCommsSettings3
    {
      get { return fanfoldHoettenCommsSettings3; }
      set { fanfoldHoettenCommsSettings3 = value; }
    }

    //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
    private KnifeLogimacComms_Settings knifeLogimacComms_Settings;
    public KnifeLogimacComms_Settings KNIFELogimacComms_Settings
    {
      get { return knifeLogimacComms_Settings; }
      set { knifeLogimacComms_Settings = value; }
    }

    //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
    private KnifeLogimac_Settings knifeLogimac_Setting;
    public KnifeLogimac_Settings KNIFELogimac_Setting
    {
      get { return knifeLogimac_Setting; }
      set { knifeLogimac_Setting = value; }
    }

    //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
    private KNIFE_MHI_58E_CtrlSettings knife_mhi_58E_CtrlSettings;
    public KNIFE_MHI_58E_CtrlSettings KNIFE_Mhi_58E_CtrlSettings
    {
      get { return knife_mhi_58E_CtrlSettings; }
      set { knife_mhi_58E_CtrlSettings = value; }
    }

    //agrassi_FI27-02-09_1
    private RSMSettings rsmSettings;
    public RSMSettings RSMSettings
    {
      get { return this.rsmSettings; }
      set { this.rsmSettings = value; }
    }

		//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
		private RollScanSettings rollScanSettings;
		public RollScanSettings RollScanSettings
		{
			get { return this.rollScanSettings; }
			set { this.rollScanSettings = value; }
		}

    private STKTerminal20_20Settings stkTerminal20_20Settings;
    public STKTerminal20_20Settings StkTerminal20_20Settings
    {
      get { return stkTerminal20_20Settings; }
      set { stkTerminal20_20Settings = value; }
    }


    //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
    private STKStackerUniversalSettings stkStackerUniversalSettings;
    public STKStackerUniversalSettings StkStackerUniversalSettings
    {
      get { return stkStackerUniversalSettings; }
      set { stkStackerUniversalSettings = value; }
    }

    ////PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
    //private STKStackerUniversalCtrlSettings stkStackerUniversalCtrlSettings;
    //public STKStackerUniversalCtrlSettings StkStackerUniversalCtrlSettings
    //{
    //  get { return stkStackerUniversalCtrlSettings; }
    //  set { stkStackerUniversalCtrlSettings = value; }
    //}

    //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
    private STKStackerUniversalCtrlSettings stkStackerUniversalLevel1CtrlSettings;
    public STKStackerUniversalCtrlSettings StkStackerUniversalLevel1CtrlSettings
    {
      get { return stkStackerUniversalLevel1CtrlSettings; }
      set { stkStackerUniversalLevel1CtrlSettings = value; }
    }

    //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
    private STKStackerUniversalCtrlSettings stkStackerUniversalLevel2CtrlSettings;
    public STKStackerUniversalCtrlSettings StkStackerUniversalLevel2CtrlSettings
    {
      get { return stkStackerUniversalLevel2CtrlSettings; }
      set { stkStackerUniversalLevel2CtrlSettings = value; }
    }

    //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
    private STKStackerUniversalCtrlSettings stkStackerUniversalLevel3CtrlSettings;
    public STKStackerUniversalCtrlSettings StkStackerUniversalLevel3CtrlSettings
    {
      get { return stkStackerUniversalLevel3CtrlSettings; }
      set { stkStackerUniversalLevel3CtrlSettings = value; }
    }

    //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
    private STK_GenericCTRLSettings stkGenericCTRL1Settings;
    public STK_GenericCTRLSettings STKGenericCTRL1Settings
    {
      get { return stkGenericCTRL1Settings; }
      set { stkGenericCTRL1Settings = value; }
    }

    //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
    private STK_GenericCTRLSettings stkGenericCTRL2Settings;
    public STK_GenericCTRLSettings STKGenericCTRL2Settings
    {
      get { return stkGenericCTRL2Settings; }
      set { stkGenericCTRL2Settings = value; }
    }

    //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
    private STK_GenericCTRLSettings stkGenericCTRL3Settings;
    public STK_GenericCTRLSettings STKGenericCTRL3Settings
    {
      get { return stkGenericCTRL3Settings; }
      set { stkGenericCTRL3Settings = value; }
    }

    private STKTerminal20_20CommsSettings stkTerminal20_20Level1Settings;
    public STKTerminal20_20CommsSettings StkTerminal20_20Level1Settings
    {
      get { return stkTerminal20_20Level1Settings; }
      set { stkTerminal20_20Level1Settings = value; }
    }
    private STKTerminal20_20CommsSettings stkTerminal20_20Level2Settings;
    public STKTerminal20_20CommsSettings StkTerminal20_20Level2Settings
    {
      get { return stkTerminal20_20Level2Settings; }
      set { stkTerminal20_20Level2Settings = value; }
    }
    private STKTerminal20_20CommsSettings stkTerminal20_20Level3Settings;
    public STKTerminal20_20CommsSettings StkTerminal20_20Level3Settings
    {
      get { return stkTerminal20_20Level3Settings; }
      set { stkTerminal20_20Level3Settings = value; }
    }
    private STKTerminal20_11CommsSettings stkTerminal20_11Level1Settings;
    public STKTerminal20_11CommsSettings StkTerminal20_11Level1Settings
    {
      get { return stkTerminal20_11Level1Settings; }
      set { stkTerminal20_11Level1Settings = value; }
    }
    private STKTerminal20_11CommsSettings stkTerminal20_11Level2Settings;
    public STKTerminal20_11CommsSettings StkTerminal20_11Level2Settings
    {
      get { return stkTerminal20_11Level2Settings; }
      set { stkTerminal20_11Level2Settings = value; }
    }
    private STKTerminal20_11CommsSettings stkTerminal20_11Level3Settings;
    public STKTerminal20_11CommsSettings StkTerminal20_11Level3Settings
    {
      get { return stkTerminal20_11Level3Settings; }
      set { stkTerminal20_11Level3Settings = value; }
    }
    private STKTerminal400CommsSettings stkTerminal400Level1Settings;
    public STKTerminal400CommsSettings StkTerminal400Level1Settings
    {
      get { return stkTerminal400Level1Settings; }
      set { stkTerminal400Level1Settings = value; }
    }
    private STKTerminal400CommsSettings stkTerminal400Level2Settings;
    public STKTerminal400CommsSettings StkTerminal400Level2Settings
    {
      get { return stkTerminal400Level2Settings; }
      set { stkTerminal400Level2Settings = value; }
    }
    private STKTerminal400CommsSettings stkTerminal400Level3Settings;
    public STKTerminal400CommsSettings StkTerminal400Level3Settings
    {
      get { return stkTerminal400Level3Settings; }
      set { stkTerminal400Level3Settings = value; }
    }
    private STK_NE_CommsSettings stk_NE_Level1Settings;
    public STK_NE_CommsSettings Stk_NE_Level1Settings
    {
      get { return stk_NE_Level1Settings; }
      set { stk_NE_Level1Settings = value; }
    }
    private STK_NE_CommsSettings stk_NE_Level2Settings;
    public STK_NE_CommsSettings Stk_NE_Level2Settings
    {
      get { return stk_NE_Level2Settings; }
      set { stk_NE_Level2Settings = value; }
    }
    private STK_NE_CommsSettings stk_NE_Level3Settings;
    public STK_NE_CommsSettings Stk_NE_Level3Settings
    {
      get { return stk_NE_Level3Settings; }
      set { stk_NE_Level3Settings = value; }
    }
    //ABorelli_FI01/06/2008_1
    private STK_LRE_CommsSettings stk_LRE_Level1Settings;
    public STK_LRE_CommsSettings Stk_LRE_Level1Settings
    {
      get { return stk_LRE_Level1Settings; }
      set { stk_LRE_Level1Settings = value; }
    }
    private STK_LRE_CommsSettings stk_LRE_Level2Settings;
    public STK_LRE_CommsSettings Stk_LRE_Level2Settings
    {
      get { return stk_LRE_Level2Settings; }
      set { stk_LRE_Level2Settings = value; }
    }
    private STK_LRE_CommsSettings stk_LRE_Level3Settings;
    public STK_LRE_CommsSettings Stk_LRE_Level3Settings
    {
      get { return stk_LRE_Level3Settings; }
      set { stk_LRE_Level3Settings = value; }
    }
    //agrassi_FI26-04-2010_1
    private STKBSCOMGapoCommsSettings stkBSCOMGapoCommsSetting;
    public STKBSCOMGapoCommsSettings StkBSCOMGapoCommsSetting
    {
      get { return stkBSCOMGapoCommsSetting; }
      set { stkBSCOMGapoCommsSetting = value; }
    }

    //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
    private STK_MarquipDeccCommsSettings stkMarquipCommsSetting;
    public STK_MarquipDeccCommsSettings StkMarquipCommsSetting
    {
      get { return stkMarquipCommsSetting; }
      set { stkMarquipCommsSetting = value; }
    }

    private PRINTERSettings printerSetting;
    public PRINTERSettings PrinterSetting
    {
      get { return printerSetting; }
      set { printerSetting = value; }
    }

    private OHDSettings ohdSetting;
    public OHDSettings OhdSetting
    {
      get { return ohdSetting; }
      set { ohdSetting = value; }
    }

    private DBGenericSettings dbGenericSetting;
    public DBGenericSettings DBGenericSetting
    {
      get { return dbGenericSetting; }
      set { dbGenericSetting = value; }
    }

    private DBGenericCtrlSettings dbGenericCtrlSetting;
    public DBGenericCtrlSettings DBGenericCtrlSetting
    {
      get { return dbGenericCtrlSetting; }
      set { dbGenericCtrlSetting = value; }
    }

    private DBExpressCtrlSettings dbExpressCtrlSetting;
    public DBExpressCtrlSettings DBExpressCtrlSetting
    {
      get { return dbExpressCtrlSetting; }
      set { dbExpressCtrlSetting = value; }
    }

    //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
    private DBCarteamCtrlSettings dbCarteamCtrlSetting;
    public DBCarteamCtrlSettings DBCarteamCtrlSetting
    {
      get { return dbCarteamCtrlSetting; }
      set { dbCarteamCtrlSetting = value; }
    }

    //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
    private DBExpress400CtrlSettings dbExpress400CtrlSetting;
    public DBExpress400CtrlSettings DBExpress400CtrlSetting
    {
      get { return dbExpress400CtrlSetting; }
      set { dbExpress400CtrlSetting = value; }
    }

    private WECSettings wecSetting;
    public WECSettings WecSetting
    {
      get { return wecSetting; }
      set { wecSetting = value; }
    }

    private StarterSettings starterSetting;
    public StarterSettings StarterSetting
    {
      get { return starterSetting; }
      set { starterSetting = value; }
    }
    private RTCSettings rtcSetting;
    public RTCSettings RtcSetting
    {
      get { return rtcSetting; }
      set { rtcSetting = value; }
    }
    private FCTSettings fctSetting;
    public FCTSettings FctSetting
    {
      get { return fctSetting; }
      set { fctSetting = value; }
    }
    private DIOSettings dioSetting;
    public DIOSettings DioSetting
    {
      get { return dioSetting; }
      set { dioSetting = value; }
    }
    private CTCSettings ctcSetting;
    public CTCSettings CtcSetting
    {
      get { return ctcSetting; }
      set { ctcSetting = value; }
    }
    private AIOSettings aioSetting;
    public AIOSettings AioSetting
    {
      get { return aioSetting; }
      set { aioSetting = value; }
    }
    private ALARMSSettings alarmsSetting;
    public ALARMSSettings AlarmsSetting
    {
      get { return alarmsSetting; }
      set { alarmsSetting = value; }
    }
    private DATABRIDGESettings dataBridgeSetting;
    public DATABRIDGESettings DataBridgeSetting
    {
      get { return dataBridgeSetting; }
      set { dataBridgeSetting = value; }
    }
    private EVENTLOGSettings eventLogSetting;
    public EVENTLOGSettings EventLogSetting
    {
      get { return eventLogSetting; }
      set { eventLogSetting = value; }
    }
    private SPEEDSettings speedSetting;
    public SPEEDSettings SpeedSetting
    {
      get { return speedSetting; }
      set { speedSetting = value; }
    }
    private SPEED_ASRSettings speedASRSetting;
    public SPEED_ASRSettings SpeedASRSetting
    {
      get { return speedASRSetting; }
      set { speedASRSetting = value; }
    }
    private AllenBradleyCommsSettings twin400MacSetting;
    public AllenBradleyCommsSettings Twin400MacSetting
    {
      get { return twin400MacSetting; }
      set { twin400MacSetting = value; }
    }

    private BaseComms twin400MacCommsSetting;
    public BaseComms Twin400MacCommsSetting
    {
      get { return twin400MacCommsSetting; }
      set { twin400MacCommsSetting = value; }
    }

    private BaseComms twin400MacCommsSPDESetting;
    public BaseComms Twin400MacCommsSPDESetting
    {
      get { return twin400MacCommsSPDESetting; }
      set { twin400MacCommsSPDESetting = value; }
    }

    //agrassi_FI16-11-2010_2
    private Twin400MacCommsBHSSettings twin400MacCommsBHSSetting;
    public Twin400MacCommsBHSSettings Twin400MacCommsBHSSetting
    {
      get { return twin400MacCommsBHSSetting; }
      set { twin400MacCommsBHSSetting = value; }
    }

    private SSTwinESettings twinESetting;
    public SSTwinESettings TwinESetting
    {
      get { return twinESetting; }
      set { twinESetting = value; }
    }

    private SSTwinE22_50CommsSettings twinECommsSetting;
    public SSTwinE22_50CommsSettings TwinECommsSetting
    {
      get { return twinECommsSetting; }
      set { twinECommsSetting = value; }
    }

    //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
    private SSCompact22_2XMacCommsSettings compact2MacCommsSetting;
    public SSCompact22_2XMacCommsSettings Compact2MacCommsSetting
    {
      get { return compact2MacCommsSetting; }
      set { compact2MacCommsSetting = value; }
    }


    private SSCompact22_2XSettings compact2Setting;
    public SSCompact22_2XSettings Compact2Setting
    {
      get { return compact2Setting; }
      set { compact2Setting = value; }
    }

    private SSCompact22_2XCommsSettings compact2CommsSetting;
    public SSCompact22_2XCommsSettings Compact2CommsSetting
    {
      get { return compact2CommsSetting; }
      set { compact2CommsSetting = value; }
    }
    private SSTwin22_60Settings twin22_60Setting;
    public SSTwin22_60Settings Twin22_60Setting
    {
      get { return twin22_60Setting; }
      set { twin22_60Setting = value; }
    }

    private SSTwin22_60CommsSettings twin22_60CommsSetting;
    public SSTwin22_60CommsSettings Twin22_60CommsSetting
    {
      get { return twin22_60CommsSetting; }
      set { twin22_60CommsSetting = value; }
    }

    //agrassi_FI25-05-2011_1
    private SSTwin22_40Settings twin22_40Setting;
    public SSTwin22_40Settings Twin22_40Setting
    {
      get { return twin22_40Setting; }
      set { twin22_40Setting = value; }
    }

    private SSTwin22_40CommsSettings twin22_40CommsSetting;
    public SSTwin22_40CommsSettings Twin22_40CommsSetting
    {
      get { return twin22_40CommsSetting; }
      set { twin22_40CommsSetting = value; }
    }

    private SSPaddlesSettings paddlesSettings;
    public SSPaddlesSettings PaddlesSettings
    {
      get { return paddlesSettings; }
      set { paddlesSettings = value; }
    }

    private PHGenericSettings thermostackSetting;
    public PHGenericSettings ThermostackSetting
    {
      get { return thermostackSetting; }
      set { thermostackSetting = value; }
    }

    private PHGenericCTRLSettings thermostackCTRLSetting;
    public PHGenericCTRLSettings ThermostackCTRLSetting
    {
      get { return thermostackCTRLSetting; }
      set { thermostackCTRLSetting = value; }
    }

    private GUGenericSettings glueUnitSetting;
    public GUGenericSettings GlueUnitSetting
    {
      get { return glueUnitSetting; }
      set { glueUnitSetting = value; }
    }

    private GUGenericCTRLSettings glueUnitCTRLSetting;
    public GUGenericCTRLSettings GlueUnitCTRLSetting
    {
      get { return glueUnitCTRLSetting; }
      set { glueUnitCTRLSetting = value; }
    }

    //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
    private GU_KohlerCtrlSettings glueUnitKohlerCTRLSetting;
    public GU_KohlerCtrlSettings GlueUnitKohlerCTRLSetting
    {
      get { return glueUnitKohlerCTRLSetting; }
      set { glueUnitKohlerCTRLSetting = value; }
    }

    private SplicerGenericSettings splicerGeneric1Setting;
    public SplicerGenericSettings SplicerGeneric1Setting
    {
      get { return splicerGeneric1Setting; }
      set { splicerGeneric1Setting = value; }
    }

		private SplicerGenericCTRLSettings splicerGenericCTRL1Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL1Setting
    {
      get { return splicerGenericCTRL1Setting; }
      set { splicerGenericCTRL1Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL1Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL1Setting
		{
			get { return splicerFosberLNK400CTRL1Setting; }
			set { splicerFosberLNK400CTRL1Setting = value; }
		}

		private SplicerGenericSettings splicerGeneric2Setting;
    public SplicerGenericSettings SplicerGeneric2Setting
    {
      get { return splicerGeneric2Setting; }
      set { splicerGeneric2Setting = value; }
    }
    private SplicerGenericCTRLSettings splicerGenericCTRL2Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL2Setting
    {
      get { return splicerGenericCTRL2Setting; }
      set { splicerGenericCTRL2Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL2Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL2Setting
		{
			get { return splicerFosberLNK400CTRL2Setting; }
			set { splicerFosberLNK400CTRL2Setting = value; }
		}

		private SplicerGenericSettings splicerGeneric3Setting;
    public SplicerGenericSettings SplicerGeneric3Setting
    {
      get { return splicerGeneric3Setting; }
      set { splicerGeneric3Setting = value; }
    }
    private SplicerGenericCTRLSettings splicerGenericCTRL3Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL3Setting
    {
      get { return splicerGenericCTRL3Setting; }
      set { splicerGenericCTRL3Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL3Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL3Setting
		{
			get { return splicerFosberLNK400CTRL3Setting; }
			set { splicerFosberLNK400CTRL3Setting = value; }
		}

		private SplicerGenericSettings splicerGeneric4Setting;
    public SplicerGenericSettings SplicerGeneric4Setting
    {
      get { return splicerGeneric4Setting; }
      set { splicerGeneric4Setting = value; }
    }
    private SplicerGenericCTRLSettings splicerGenericCTRL4Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL4Setting
    {
      get { return splicerGenericCTRL4Setting; }
      set { splicerGenericCTRL4Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL4Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL4Setting
		{
			get { return splicerFosberLNK400CTRL4Setting; }
			set { splicerFosberLNK400CTRL4Setting = value; }
		}

		private SplicerGenericSettings splicerGeneric5Setting;
    public SplicerGenericSettings SplicerGeneric5Setting
    {
      get { return splicerGeneric5Setting; }
      set { splicerGeneric5Setting = value; }
    }
    private SplicerGenericCTRLSettings splicerGenericCTRL5Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL5Setting
    {
      get { return splicerGenericCTRL5Setting; }
      set { splicerGenericCTRL5Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL5Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL5Setting
		{
			get { return splicerFosberLNK400CTRL5Setting; }
			set { splicerFosberLNK400CTRL5Setting = value; }
		}

		private SplicerGenericSettings splicerGeneric6Setting;
    public SplicerGenericSettings SplicerGeneric6Setting
    {
      get { return splicerGeneric6Setting; }
      set { splicerGeneric6Setting = value; }
    }
    private SplicerGenericCTRLSettings splicerGenericCTRL6Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL6Setting
    {
      get { return splicerGenericCTRL6Setting; }
      set { splicerGenericCTRL6Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL6Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL6Setting
		{
			get { return splicerFosberLNK400CTRL6Setting; }
			set { splicerFosberLNK400CTRL6Setting = value; }
		}

		private SplicerGenericSettings splicerGeneric7Setting;
    public SplicerGenericSettings SplicerGeneric7Setting
    {
      get { return splicerGeneric7Setting; }
      set { splicerGeneric7Setting = value; }
    }
    private SplicerGenericCTRLSettings splicerGenericCTRL7Setting;
    public SplicerGenericCTRLSettings SplicerGenericCTRL7Setting
    {
      get { return splicerGenericCTRL7Setting; }
      set { splicerGenericCTRL7Setting = value; }
    }
		//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
		private SplicerFosberLNK400CTRLSettings splicerFosberLNK400CTRL7Setting;
		public SplicerFosberLNK400CTRLSettings SplicerFosberLNK400CTRL7Setting
		{
			get { return splicerFosberLNK400CTRL7Setting; }
			set { splicerFosberLNK400CTRL7Setting = value; }
		}

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL1Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL1Setting
    {
      get { return rollStandFosberPRT400CTRL1Setting; }
      set { rollStandFosberPRT400CTRL1Setting = value; }
    }

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL2Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL2Setting
    {
      get { return rollStandFosberPRT400CTRL2Setting; }
      set { rollStandFosberPRT400CTRL2Setting = value; }
    }

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL3Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL3Setting
    {
      get { return rollStandFosberPRT400CTRL3Setting; }
      set { rollStandFosberPRT400CTRL3Setting = value; }
    }

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL4Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL4Setting
    {
      get { return rollStandFosberPRT400CTRL4Setting; }
      set { rollStandFosberPRT400CTRL4Setting = value; }
    }

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL5Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL5Setting
    {
      get { return rollStandFosberPRT400CTRL5Setting; }
      set { rollStandFosberPRT400CTRL5Setting = value; }
    }

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL6Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL6Setting
    {
      get { return rollStandFosberPRT400CTRL6Setting; }
      set { rollStandFosberPRT400CTRL6Setting = value; }
    }

    //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
    private RollStandFosberPRT400CTRLSettings rollStandFosberPRT400CTRL7Setting;
    public RollStandFosberPRT400CTRLSettings RollStandFosberPRT400CTRL7Setting
    {
      get { return rollStandFosberPRT400CTRL7Setting; }
      set { rollStandFosberPRT400CTRL7Setting = value; }
    }

    private BRIDGESettings bridge1Setting;
    public BRIDGESettings Bridge1Setting
    {
      get { return bridge1Setting; }
      set { bridge1Setting = value; }
    }
    private BRIDGESettings bridge2Setting;
    public BRIDGESettings Bridge2Setting
    {
      get { return bridge2Setting; }
      set { bridge2Setting = value; }
    }
    private BRIDGESettings bridge3Setting;
    public BRIDGESettings Bridge3Setting
    {
      get { return bridge3Setting; }
      set { bridge3Setting = value; }
    }

    private SingleFacerGenericSettings singleFacerGeneric1Setting;
    public SingleFacerGenericSettings SingleFacerGeneric1Setting
    {
      get { return singleFacerGeneric1Setting; }
      set { singleFacerGeneric1Setting = value; }
    }
    private SingleFacerGenericSettings singleFacerGeneric2Setting;
    public SingleFacerGenericSettings SingleFacerGeneric2Setting
    {
      get { return singleFacerGeneric2Setting; }
      set { singleFacerGeneric2Setting = value; }
    }
    private SingleFacerGenericSettings singleFacerGeneric3Setting;
    public SingleFacerGenericSettings SingleFacerGeneric3Setting
    {
      get { return singleFacerGeneric3Setting; }
      set { singleFacerGeneric3Setting = value; }
    }

    private SingleFacerGenericCTRLSettings singleFacerGenericCTRL1Setting;
    public SingleFacerGenericCTRLSettings SingleFacerGenericCTRL1Setting
    {
      get { return singleFacerGenericCTRL1Setting; }
      set { singleFacerGenericCTRL1Setting = value; }
    }
    private SingleFacerGenericCTRLSettings singleFacerGenericCTRL2Setting;
    public SingleFacerGenericCTRLSettings SingleFacerGenericCTRL2Setting
    {
      get { return singleFacerGenericCTRL2Setting; }
      set { singleFacerGenericCTRL2Setting = value; }
    }
    private SingleFacerGenericCTRLSettings singleFacerGenericCTRL3Setting;
    public SingleFacerGenericCTRLSettings SingleFacerGenericCTRL3Setting
    {
      get { return singleFacerGenericCTRL3Setting; }
      set { singleFacerGenericCTRL3Setting = value; }
    }

    private SFSmartCTRLSettings singleFacerSmartCTRL1Setting;
    public SFSmartCTRLSettings SingleFacerSmartCTRL1Setting
    {
      get { return singleFacerSmartCTRL1Setting; }
      set { singleFacerSmartCTRL1Setting = value; }
    }
    private SFSmartCTRLSettings singleFacerSmartCTRL2Setting;
    public SFSmartCTRLSettings SingleFacerSmartCTRL2Setting
    {
      get { return singleFacerSmartCTRL2Setting; }
      set { singleFacerSmartCTRL2Setting = value; }
    }
    private SFSmartCTRLSettings singleFacerSmartCTRL3Setting;
    public SFSmartCTRLSettings SingleFacerSmartCTRL3Setting
    {
      get { return singleFacerSmartCTRL3Setting; }
      set { singleFacerSmartCTRL3Setting = value; }
    }

    private SFSmart400CTRLSettings singleFacerSmart400CTRL1Setting;
    public SFSmart400CTRLSettings SingleFacerSmart400CTRL1Setting
    {
      get { return singleFacerSmart400CTRL1Setting; }
      set { singleFacerSmart400CTRL1Setting = value; }
    }
    private SFSmart400CTRLSettings singleFacerSmart400CTRL2Setting;
    public SFSmart400CTRLSettings SingleFacerSmart400CTRL2Setting
    {
      get { return singleFacerSmart400CTRL2Setting; }
      set { singleFacerSmart400CTRL2Setting = value; }
    }
    private SFSmart400CTRLSettings singleFacerSmart400CTRL3Setting;
    public SFSmart400CTRLSettings SingleFacerSmart400CTRL3Setting
    {
      get { return singleFacerSmart400CTRL3Setting; }
      set { singleFacerSmart400CTRL3Setting = value; }
    }

    private SyncroCSettings syncroCSetting;
    public SyncroCSettings SyncroCSetting
    {
      get { return syncroCSetting; }
      set { syncroCSetting = value; }
    }

    //agrassi_FI24-02-2010_1
    private MonitorSettings monitorSettings;
    public MonitorSettings MonitorSetting
    {
      get { return monitorSettings; }
      set { monitorSettings = value; }
    }


    /// <summary>
    /// Builder
    /// </summary>
    /// <param name="DataBaseFolder"></param>
    public SyncroConfig(string DataBaseFolder)
      : base()
    {
      this.dbmsSetting = new DBMSSettings();
      this.generalSetting = new GeneralSettings();
      this.wecSetting = new WECSettings();
      this.wecSyncroSetting = new WECSyncroSettings();
      this.wecBMUSetting = new WECBMUSettings();
      this.mthCommsSetting = new MTHCommsSettings();
      this.mthDuckerSetting = new MTHDuckerSettings();
			//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
			this.mthSeemiSetting = new MTHSeemiSettings();
      //agrassi_FI19-11-08
      this.MthFosberMindaCommsSetting = new MTHFosberMindaCommsSettings();
      //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
      this.MthFosberStandardCommsSettings = new MTHFosberStandardCommsSettings();
      this.starterSetting = new StarterSettings();
      this.rtcSetting = new RTCSettings();
      this.fctSetting = new FCTSettings();
      this.dioSetting = new DIOSettings();
      this.ctcSetting = new CTCSettings();
      this.aioSetting = new AIOSettings();
      this.twin400MacSetting = new AllenBradleyCommsSettings("S_SS_FOSBER_22.60_Mac");
      this.twin400MacCommsSetting = new BaseComms("S_SS_FOSBER_22.60_Mac_Comms");
      this.twin400MacCommsSPDESetting = new BaseComms("S_SS_FOSBER_22.60_Mac_Comms_Spde");
      //agrassi_FI16-11-2010_2
      this.twin400MacCommsBHSSetting = new Twin400MacCommsBHSSettings();
      this.knifeDigitec21_11CommsSetting = new KNIFE21_11DigitecCommsSettings();
      this.knifeDigitec21_11Setting = new KNIFE21_11DigitecSettings();
      this.knifeDigitec21_20Setting = new KNIFE21_20DigitecSettings();
      this.knifeDigitec21_20CommsSetting = new KNIFE21_20DigitecCommsSettings();
      this.knife21_30Setting = new KNIFE21_30Settings();
      this.knife21_30CommsSetting = new KNIFE21_30CommsSettings();
      //agrassi_FI18-03-2010_1
      this.knifeBSCOMGapoCommsSetting=new KnifeBSCOMGapoCommsSettings();
      //alamioni_FI30-03-2010_1
      this.knifeBHSSetting = new KnifeBHSSettings();
      //PMASSARO_FI21/03/08 - ADDED FOR MAQUIP DECC
      this.knifeMarquipDeccCommsSetting = new KNIFE_MarquipDeccCommsSettings();
      this.knifeMarquipSetting = new KNIFE_MarquipSettings();

      //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
      this.knifeLogimacComms_Settings = new KnifeLogimacComms_Settings();
      //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
      this.knifeLogimac_Setting = new KnifeLogimac_Settings();

      //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
      this.knife_mhi_58E_CtrlSettings = new KNIFE_MHI_58E_CtrlSettings();

      //ABorelli_FI01/06/2008_1
      this.knifeBmb21_11CommsSetting = new KNIFE21_11BmbCommsSettings();
      this.knifeBmb21_11Setting = new KNIFE21_11BmbSettings();
      this.rshFosber23_XXSetting = new RSHFosber23_XXSettings();

      //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
      this.rshMhi_Setting = new RSHMhi_Settings();

      this.dbGenericSetting = new DBGenericSettings();
      this.dbGenericCtrlSetting = new DBGenericCtrlSettings();
      this.DBExpressCtrlSetting = new DBExpressCtrlSettings();
      this.DBCarteamCtrlSetting = new DBCarteamCtrlSettings(); //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
      //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
      this.DBExpress400CtrlSetting = new DBExpress400CtrlSettings();

      this.alarmsSetting = new ALARMSSettings();
      this.eventLogSetting = new EVENTLOGSettings();
      this.dataBridgeSetting = new DATABRIDGESettings();
      this.speedSetting = new SPEEDSettings();
      this.speedASRSetting = new SPEED_ASRSettings();
      this.officeSPDESetting = new OFFICESPDESettings();

      //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
      this.fanfoldHoettenCommsSettings1 = new FanFoldHOETTENCommsSettings("1");
      this.fanfoldHoettenCommsSettings2 = new FanFoldHOETTENCommsSettings("2");
      this.fanfoldHoettenCommsSettings3 = new FanFoldHOETTENCommsSettings("3");

      //agrassi_FI27-02-09_1
      this.rsmSettings = new RSMSettings();

			//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
      this.rollScanSettings = new RollScanSettings();

      this.stkTerminal20_20Settings = new STKTerminal20_20Settings();
      //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
      this.STKGenericCTRL1Settings = new STK_GenericCTRLSettings("1");
      this.STKGenericCTRL2Settings = new STK_GenericCTRLSettings("2");
      this.STKGenericCTRL3Settings = new STK_GenericCTRLSettings("3");
      //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
      this.stkStackerUniversalSettings = new STKStackerUniversalSettings();
      //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
      this.stkStackerUniversalLevel1CtrlSettings = new STKStackerUniversalCtrlSettings("1");
      this.stkStackerUniversalLevel2CtrlSettings = new STKStackerUniversalCtrlSettings("2");
      this.stkStackerUniversalLevel3CtrlSettings = new STKStackerUniversalCtrlSettings("3");
      this.stkTerminal20_11Level1Settings = new STKTerminal20_11CommsSettings("1");
      this.stkTerminal20_11Level2Settings = new STKTerminal20_11CommsSettings("2");
      this.stkTerminal20_11Level3Settings = new STKTerminal20_11CommsSettings("3");
      this.stkTerminal20_20Level1Settings = new STKTerminal20_20CommsSettings("1");
      this.stkTerminal20_20Level2Settings = new STKTerminal20_20CommsSettings("2");
      this.stkTerminal20_20Level3Settings = new STKTerminal20_20CommsSettings("3");
      this.stkTerminal400Level1Settings = new STKTerminal400CommsSettings("1");
      this.stkTerminal400Level2Settings = new STKTerminal400CommsSettings("2");
      this.stkTerminal400Level3Settings = new STKTerminal400CommsSettings("3");
      this.stk_NE_Level1Settings = new STK_NE_CommsSettings("1");
      this.stk_NE_Level2Settings = new STK_NE_CommsSettings("2");
      this.stk_NE_Level3Settings = new STK_NE_CommsSettings("3");

      //ABorelli_FI01/06/2008_1
      this.stk_LRE_Level1Settings = new STK_LRE_CommsSettings("1");
      this.stk_LRE_Level2Settings = new STK_LRE_CommsSettings("2");
      this.stk_LRE_Level3Settings = new STK_LRE_CommsSettings("3");
      //agrassi_FI26-04-2010_1
      this.stkBSCOMGapoCommsSetting = new STKBSCOMGapoCommsSettings();
      this.printerSetting = new PRINTERSettings();

      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      this.StkMarquipCommsSetting = new STK_MarquipDeccCommsSettings();

      this.OhdSetting = new OHDSettings();

      this.twinESetting = new SSTwinESettings();
      this.twinECommsSetting = new SSTwinE22_50CommsSettings();
      this.twin22_60Setting = new SSTwin22_60Settings();
      this.twin22_60CommsSetting = new SSTwin22_60CommsSettings();
      this.compact2Setting = new SSCompact22_2XSettings();
      this.compact2CommsSetting = new SSCompact22_2XCommsSettings();
      //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
      this.compact2MacCommsSetting = new SSCompact22_2XMacCommsSettings();
      //agrassi_FI25-05-2011_1
      this.twin22_40Setting = new SSTwin22_40Settings();
      this.twin22_40CommsSetting = new SSTwin22_40CommsSettings();
      this.paddlesSettings = new SSPaddlesSettings();

      this.thermostackSetting = new PHGenericSettings();
      this.thermostackCTRLSetting = new PHGenericCTRLSettings();
      this.glueUnitSetting = new GUGenericSettings();
      this.glueUnitCTRLSetting = new GUGenericCTRLSettings();
      //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
      this.glueUnitKohlerCTRLSetting = new GU_KohlerCtrlSettings();


      this.splicerGeneric1Setting = new SplicerGenericSettings("1");
      this.splicerGenericCTRL1Setting = new SplicerGenericCTRLSettings("1");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL1Setting = new SplicerFosberLNK400CTRLSettings("1");

			this.splicerGeneric2Setting = new SplicerGenericSettings("2");
      this.splicerGenericCTRL2Setting = new SplicerGenericCTRLSettings("2");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL2Setting = new SplicerFosberLNK400CTRLSettings("2");
			
			this.splicerGeneric3Setting = new SplicerGenericSettings("3");
      this.splicerGenericCTRL3Setting = new SplicerGenericCTRLSettings("3");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL3Setting = new SplicerFosberLNK400CTRLSettings("3");
			
			this.splicerGeneric4Setting = new SplicerGenericSettings("4");
      this.splicerGenericCTRL4Setting = new SplicerGenericCTRLSettings("4");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL4Setting = new SplicerFosberLNK400CTRLSettings("4");
			
			this.splicerGeneric5Setting = new SplicerGenericSettings("5");
      this.splicerGenericCTRL5Setting = new SplicerGenericCTRLSettings("5");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL5Setting = new SplicerFosberLNK400CTRLSettings("5");
			
			this.splicerGeneric6Setting = new SplicerGenericSettings("6");
      this.splicerGenericCTRL6Setting = new SplicerGenericCTRLSettings("6");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL6Setting = new SplicerFosberLNK400CTRLSettings("6");
			
			this.splicerGeneric7Setting = new SplicerGenericSettings("7");
      this.splicerGenericCTRL7Setting = new SplicerGenericCTRLSettings("7");
			//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
			this.splicerFosberLNK400CTRL7Setting = new SplicerFosberLNK400CTRLSettings("7");

      //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
      this.rollStandFosberPRT400CTRL1Setting = new RollStandFosberPRT400CTRLSettings("1");
      this.rollStandFosberPRT400CTRL2Setting = new RollStandFosberPRT400CTRLSettings("2");
      this.rollStandFosberPRT400CTRL3Setting = new RollStandFosberPRT400CTRLSettings("3");
      this.rollStandFosberPRT400CTRL4Setting = new RollStandFosberPRT400CTRLSettings("4");
      this.rollStandFosberPRT400CTRL5Setting = new RollStandFosberPRT400CTRLSettings("5");
      this.rollStandFosberPRT400CTRL6Setting = new RollStandFosberPRT400CTRLSettings("6");
      this.rollStandFosberPRT400CTRL7Setting = new RollStandFosberPRT400CTRLSettings("7");

			this.bridge1Setting = new BRIDGESettings("1");
      this.bridge2Setting = new BRIDGESettings("2");
      this.bridge3Setting = new BRIDGESettings("3");

      this.singleFacerGeneric1Setting = new SingleFacerGenericSettings("1");
      this.singleFacerGeneric2Setting = new SingleFacerGenericSettings("2");
      this.singleFacerGeneric3Setting = new SingleFacerGenericSettings("3");

      this.singleFacerGenericCTRL1Setting = new SingleFacerGenericCTRLSettings("1");
      this.singleFacerGenericCTRL2Setting = new SingleFacerGenericCTRLSettings("2");
      this.singleFacerGenericCTRL3Setting = new SingleFacerGenericCTRLSettings("3");

      this.singleFacerSmartCTRL1Setting = new SFSmartCTRLSettings("1");
      this.singleFacerSmartCTRL2Setting = new SFSmartCTRLSettings("2");
      this.singleFacerSmartCTRL3Setting = new SFSmartCTRLSettings("3");

      this.singleFacerSmart400CTRL1Setting = new SFSmart400CTRLSettings("1");
      this.singleFacerSmart400CTRL2Setting = new SFSmart400CTRLSettings("2");
      this.singleFacerSmart400CTRL3Setting = new SFSmart400CTRLSettings("3");

      //agrassi_FI24-02-2010_1
      this.monitorSettings = new MonitorSettings();
      this.syncroCSetting = new SyncroCSettings();

      this.Load();
    }

    /// <summary>
    /// Compare the given cheksum with the database checksum.
    /// </summary>
    /// <param name="checksum">the checksum to compare</param>
    public void Chk(string checksum)
    {
      Settings settings = Settings.getSettings();

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      int checkIni = 0;
      try
      {
        //ini checking
        int ret = FileCrc32Assembly(SyncroC.Utility.Settings.getSettings().paths.ClientData + this.FILE_INI_NAME, ref checkIni);
        if (ret != 0)
        {
          Logger.LogError(this, "Chk - file: " + this.FILE_INI_NAME + " error: " + ret);
          cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_SYNCRO_CONFIG + "()");
          return;
        }
        if (checksum.Trim() != (checkIni.ToString("x")))
        {
          Logger.LogError(this, "Chk - file: " + this.FILE_INI_NAME + " wrong checksum.");
          cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_SYNCRO_CONFIG + "()");
          return;
        }
      }
      catch (DllNotFoundException ex)
      {
        Logger.LogError(this, "Collect - Dll for checksum calculation not found.", ex);
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_SYNCRO_CONFIG + "()");
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "Collect - Error during checksum calculation", ex);
      }
    }

    public bool IsTwin400Only()
    {
      return (this.twin400MacSetting.Enable && (!this.generalSetting.DryEndControl) && (!this.generalSetting.WetEndControl));
    }
  }
}
