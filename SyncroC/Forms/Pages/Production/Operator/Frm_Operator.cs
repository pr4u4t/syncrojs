using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase.LineConfig;

//PMASSARO_FI03/02/09 - ShutDown From Operator Page
using SyncroC.Forms.PopUp;

//graffaelli_FI07/11/2011_1 - Added align second display on desktop command
using System.Diagnostics;

//ABELLO_FI07-07-08_3 Aggiunta l'unità di misura sui controlli
//ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
namespace SyncroC.Forms.Pages.Production.Operator
{
	public class Frm_Operator : SyncroC.Forms.Pages.Production.Operator.Frm_OperatorSubGroup
	{
		private SyncroC.Controls.Title ttl_Settings;
		private SyncroC.Controls.TitledNumericUpDown tnud_DryEndRSHCutsRequired;
		private SyncroC.Controls.TitledNumericUpDown tnud_WetEndRSHCutsRequired;
		private SyncroC.Controls.TitledNumericUpDown tnud_operatorMaxProductionSpeed;
		private SyncroC.Controls.Title ttl_ProductionSpeed;
		private SyncroC.Controls.TitledNumericUpDown tnud_slowSpeed;
		private SyncroC.Controls.TitledNumericUpDown tnud_spliceRemovalRSHCutsRequired;
		private SyncroC.Controls.CheckBox chk_autoSpeedModeEnabled;
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.Title ttl_AOCSpeed;
		private SyncroC.Controls.TitledNumericUpDown tnud_OperatorMaxAOCSpeed;
		private SyncroC.Controls.TitledNumericUpDown tnud_OperatorSlowAOCSpeed;
		private SyncroC.Controls.CheckBox chk_AutoSpeedAtProductionSpeed;

		//flags for warnings
		private bool DryEndRSHCutsRequired_Warning = false;
		private bool WetEndRSHCutsRequired_Warning = false;
		//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
		private bool WetEndFluteChangeRSHCutsRequired = false;
		private bool spliceRemovalRSHCutsRequired_Warning = false;
		private bool operatorMaxProductionSpeed_Warning = false;
		private bool slowSpeed_Warning = false;
		private bool OperatorMaxAOCSpeed_Warning = false;
		private bool OperatorSlowAOCSpeed_Warning = false;
    //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
    private bool OperatorOrderMaxSpeedProduction_Length_Warning = false;

		//page status
		private enum State { Modify, Read };
		private State state, nextState;
    private SyncroC.Controls.Title ttl_AluminiumScrap;
    private SyncroC.Controls.CheckBox chk_aluminiumScrapAOC;
    private SyncroC.Controls.CheckBox chk_aluminiumScrapNP;
    private SyncroC.Controls.TitledNumericUpDown tnud_orderMaxProductionSpeed_Length;
		private SyncroC.Controls.TitledNumericUpDown tnud_WetEndFluteChangeRSHCutsRequired;
    private GroupBox grp_SpeedType;
    private SyncroC.Controls.CheckBox chk_AutoSpeedAtMaxSpeed;
    private SyncroC.Controls.CheckBox chk_AutoSpeedAtTargetSpeed;
    private SyncroC.Controls.CheckBox chk_shortOrderSpeed;
    private SyncroC.Controls.CheckBox chk_enableNoScrapWhenOnlyInternalSplice;
    //agrassi_FI15-11-2010_1
    private bool aluminiumVisible = false;

		public Frm_Operator()
		{
			InitializeComponent();
			this.state = State.Read;
			this.nextState = State.Read;

			if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
			{
				this.lbl_CtrlF4_YY.Text = "";
				this.lbl_CtrlF5_YY.Text = "";
			}

      //graffaelli_FI07/11/2011_1 - Added align second display on desktop command
      if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer() || (!Process.GetCurrentProcess().MainModule.ModuleName.Contains("1")))
        this.lbl_CtrlF2_YY.Text = "";
 
      //agrassi_FI29-08-09_1
      //agrassi_FI15-11-2010_1
      //bool aluminiumVisible = (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3745");
      this.aluminiumVisible = (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3745");
      this.ttl_AluminiumScrap.Visible = this.chk_aluminiumScrapAOC.Visible = this.chk_aluminiumScrapNP.Visible = aluminiumVisible;
      //agrassi_FI13-05-2011_2
      this.chk_shortOrderSpeed.Visible = (DBManager.getDBManager().lineConfig.SlitterScorer && DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		protected override void F10()
		{
		}

    //PMASSARO_FI03/02/09 - ShutDown From Operator Page
    protected override void CtrlF1()
    {
      bool isOnServer = SyncroC.Utility.Settings.getSettings().configuration.getOnServer();

      if ( isOnServer && !RTCManager.getRTCManager().IsIdleShift )
      {
        PageManager.getPageManager()._SetMessage("§SYCC_195");
        return;
      }
      else
      {
        Frm_Confirm conf = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_196#");

        if (conf.ShowDialog(this, text).Equals(DialogResult.OK))
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();

          if (isOnServer)
          {
            cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.SHUTDOWN_AND_OFF_PC_BY_OPERATOR));
          }
          else
          {
            PageManager.getPageManager()._SetMessage("§SYCC_00");
            if (cm.CanSendTCP())
            {
              //ask the server to shout down the client
              cm.SendTcp(cm.SYNCROS_COMM_MANAGER, Command.COMMUNICATION_MANAGER + "." + Command.SHUTDOWN + "()");
            }
            else
            {
              Logger.LogError(this, "TCP connection down: Client shuts down by itself.");
              //probably the server is already down... so just shout down.
            }

            System.Threading.Thread.Sleep(5000);

            Logger.LogError(this, "Client ShutDown Requested By Operator.");
            Utility.Shoutdown.ShoutdownWindows(false);
          }
        }
        else
        {
          return;
        }
      }
    }

    //graffaelli_FI07/11/2011_1 - Added align second display on desktop command
    protected override void CtrlF2()
    {
      bool isOnServer = SyncroC.Utility.Settings.getSettings().configuration.getOnServer();

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();

      if (isOnServer)
      {
        Logger.LogError(this, "Sent Second Cliend Alignement on Desktop Requested By Operator");
        cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.ALIGN_SECOND_DISPLAY_ON_DESKTOP));
      }
      else
      {
        Logger.LogError(this, "Server is not ON: Second Cliend Alignement on Desktop Requested By Operator.");
      }
    }

		protected override void CtrlF5()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
				return;

			SetPageStatus(State.Modify);
		}

		protected override void CtrlF4()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
			{
				SetPageStatus(State.Read);
				return;
			}

			//Save Settings modifications
			if (state.Equals(State.Modify) )
			{
				if (DryEndRSHCutsRequired_Warning ||
					WetEndRSHCutsRequired_Warning ||
					//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
					WetEndFluteChangeRSHCutsRequired ||
					spliceRemovalRSHCutsRequired_Warning ||
					operatorMaxProductionSpeed_Warning ||
					slowSpeed_Warning ||
					OperatorMaxAOCSpeed_Warning ||
          OperatorOrderMaxSpeedProduction_Length_Warning || //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
					OperatorSlowAOCSpeed_Warning)
				{//warning present, save only after confirmation
					PopUp.Frm_Confirm conf = new PopUp.Frm_Confirm();
					string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_86");//Unusual values, save anyway?
					DialogResult res = conf.ShowDialog(this, text);
					if ((res == DialogResult.Yes) || (res == DialogResult.OK))
					{
						saveData();
						SetPageStatus(State.Read);
					}
				}
				else
				{	//any warning present: save data without any confirmation
					saveData();
					SetPageStatus(State.Read);
				}
			}
		}

		private void saveData()
		{
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      int typeAutoSpeedToProductionSpeed = 0;
      if(this.chk_AutoSpeedAtProductionSpeed.Checked)
        typeAutoSpeedToProductionSpeed = 0;
      else if (this.chk_AutoSpeedAtTargetSpeed.Checked)
        typeAutoSpeedToProductionSpeed = 1;
      else if (this.chk_AutoSpeedAtMaxSpeed.Checked)
        typeAutoSpeedToProductionSpeed = 2;

      //agrassi_FI13-05-2011_2
      bool isThereTwin400 = (DBManager.getDBManager().lineConfig.SlitterScorer && DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400());
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER, "operator." + Command.CONF_CONFIG + "([OPERATOR]" + Command.PARAMETER_SEPARATOR +
					"DryEndRSHCutsRequired=" + this.tnud_DryEndRSHCutsRequired.Value + Command.PARAMETER_SEPARATOR +
					"WetEndRSHCutsRequired=" + this.tnud_WetEndRSHCutsRequired.Value + Command.PARAMETER_SEPARATOR +
					//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
					"WetEndFluteChangeRSHCutsRequired=" + this.tnud_WetEndFluteChangeRSHCutsRequired.Value + Command.PARAMETER_SEPARATOR +
					"SpliceRemovalRSHCutsRequired=" + this.tnud_spliceRemovalRSHCutsRequired.Value + Command.PARAMETER_SEPARATOR +
					"OperatorMaxProductionSpeed=" + this.tnud_operatorMaxProductionSpeed.Value + Command.PARAMETER_SEPARATOR +
					"SlowSpeed_UVmm_m=" + this.tnud_slowSpeed.Value + Command.PARAMETER_SEPARATOR +
					"MaxAOCSpeed_UVmm_m=" + this.tnud_OperatorMaxAOCSpeed.Value + Command.PARAMETER_SEPARATOR +
					"SlowAOCSpeed_UVmm_m=" + this.tnud_OperatorSlowAOCSpeed.Value + Command.PARAMETER_SEPARATOR +
          "TypeAutoSpeedToProductionSpeed=" + typeAutoSpeedToProductionSpeed + Command.PARAMETER_SEPARATOR +
          "AutoSpeedMode=" + this.chk_autoSpeedModeEnabled.Checked 
          + Command.PARAMETER_SEPARATOR + //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
          //agrassi_FI23-03-2011_3
          //agrassi_FI13-05-2011_2
          //"limitateSpeedAccordingToPosTime=" + this.chk_shortOrderSpeed.Checked + Command.PARAMETER_SEPARATOR +
          "limitateSpeedAccordingToPosTime=" + (this.chk_shortOrderSpeed.Checked && isThereTwin400).ToString() + Command.PARAMETER_SEPARATOR +
          //RFAZZI_FI12/09/2011_1. . Aggiunta funzionalità di "Disabilita scarto giunte interne al cartone". Se abilitato, Syncro esegue lo scarto al cambio qualità solo se presente almeno una giunta sulle copertini del cartone altrimenti eseguirà lo scarto quantificato come da pagina operatore. 
          "EnableNoScrapWhenOnlyInternalSplice=" + (this.chk_enableNoScrapWhenOnlyInternalSplice.Checked).ToString() + Command.PARAMETER_SEPARATOR +
          "OrderMaxProductionSpeed_Length_UDm=" + this.tnud_orderMaxProductionSpeed_Length.Value + //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
          ")");
      //agrassi_FI15-11-2010_1
      if (this.aluminiumVisible || true)
      {
        //agrassi_FI29-08-09_1
        string oldConf = "", newConf = "";
        SyncroC.DataBase.LineConfig.Syncro cfgSyncro = DBManager.getDBManager().lineConfig.SyncroConfig;
        oldConf = cfgSyncro.SerializeProperties();
        for (int i = 1; i <= 4; i++)
        {
          int indItem = -1, indEqual = -1, indSep = -1;
          string item = "EnableScrapForSpliceDetected_" + i.ToString();
          indItem = oldConf.IndexOf(item, StringComparison.InvariantCultureIgnoreCase);
          if (indItem >= 0)
          {
            indEqual = oldConf.IndexOf("=", indItem);
            if (indEqual >= 0)
            {
              indSep = oldConf.IndexOf(Command.PARAMETER_SEPARATOR, indEqual);
              if (indSep >= 0)
                newConf = oldConf.Substring(0, indEqual + 1) + this.chk_aluminiumScrapNP.Checked.ToString() + oldConf.Substring(indSep);
              else
                newConf = oldConf.Substring(0, indEqual + 1) + this.chk_aluminiumScrapNP.Checked.ToString();
            }
            else
              Logger.LogError(this, "Impossible to find \"equal\" in " + newConf + " after " + item);
          }
          else
            Logger.LogError(this, "Impossible to find " + item + " in " + newConf);
          oldConf = newConf;

          indItem = -1; indEqual = -1; indSep = -1;
          item = "EnableScrapAOCForSpliceDetected_" + i.ToString();
          indItem = oldConf.IndexOf(item, StringComparison.InvariantCultureIgnoreCase);
          if (indItem >= 0)
          {
            indEqual = oldConf.IndexOf("=", indItem);
            if (indEqual >= 0)
            {
              indSep = oldConf.IndexOf(Command.PARAMETER_SEPARATOR, indEqual);
              if (indSep >= 0)
                newConf = oldConf.Substring(0, indEqual + 1) + this.chk_aluminiumScrapAOC.Checked.ToString() + oldConf.Substring(indSep);
              else
                newConf = oldConf.Substring(0, indEqual + 1) + this.chk_aluminiumScrapAOC.Checked.ToString();
            }
            else
              Logger.LogError(this, "Impossible to find \"equal\" in " + newConf + " after " + item);
          }
          else
            Logger.LogError(this, "Impossible to find " + item + " in " + newConf);
          oldConf = newConf;
        }
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER, "syncro." + Command.CONF_CONFIG + "(" + oldConf + ")");
      }
    }

		protected override void Escape()
		{
			//Esc without saving
			if (state.Equals(State.Modify))
			{
				SetPageStatus(State.Read);
        //agrassi_FI11-03-2010_2
        this.refreshData();
			}
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Operator));
      this.ttl_Settings = new SyncroC.Controls.Title();
      this.tnud_DryEndRSHCutsRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WetEndRSHCutsRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_operatorMaxProductionSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_ProductionSpeed = new SyncroC.Controls.Title();
      this.tnud_slowSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_spliceRemovalRSHCutsRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_autoSpeedModeEnabled = new SyncroC.Controls.CheckBox();
      this.ttl_AOCSpeed = new SyncroC.Controls.Title();
      this.tnud_OperatorMaxAOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_OperatorSlowAOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_AutoSpeedAtProductionSpeed = new SyncroC.Controls.CheckBox();
      this.tnud_WetEndFluteChangeRSHCutsRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_AluminiumScrap = new SyncroC.Controls.Title();
      this.chk_aluminiumScrapAOC = new SyncroC.Controls.CheckBox();
      this.chk_aluminiumScrapNP = new SyncroC.Controls.CheckBox();
      this.tnud_orderMaxProductionSpeed_Length = new SyncroC.Controls.TitledNumericUpDown();
      this.grp_SpeedType = new System.Windows.Forms.GroupBox();
      this.chk_shortOrderSpeed = new SyncroC.Controls.CheckBox();
      this.chk_AutoSpeedAtMaxSpeed = new SyncroC.Controls.CheckBox();
      this.chk_AutoSpeedAtTargetSpeed = new SyncroC.Controls.CheckBox();
      this.chk_enableNoScrapWhenOnlyInternalSplice = new SyncroC.Controls.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_SpeedType.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "OPERATOR SETTINGS";
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.Leave += new System.EventHandler(this.tab_Tab3_Leave);
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "Shudown PC";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Client2 Align";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
      this.iml_navigationImages.Images.SetKeyName(0, "");
      this.iml_navigationImages.Images.SetKeyName(1, "");
      this.iml_navigationImages.Images.SetKeyName(2, "");
      this.iml_navigationImages.Images.SetKeyName(3, "");
      // 
      // btn_Group1
      // 
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // ttl_Settings
      // 
      this.ttl_Settings.BackColor = System.Drawing.Color.Black;
      this.ttl_Settings.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Settings.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Settings.Location = new System.Drawing.Point(140, 193);
      this.ttl_Settings.Name = "ttl_Settings";
      this.ttl_Settings.Size = new System.Drawing.Size(948, 140);
      this.ttl_Settings.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Settings.TabIndex = 111;
      this.ttl_Settings.TabStop = false;
      this.ttl_Settings.Text = "RSH Cuts required";
      this.ttl_Settings.TranslateText = true;
      // 
      // tnud_DryEndRSHCutsRequired
      // 
      this.tnud_DryEndRSHCutsRequired.DecimalPlaces = 0;
      this.tnud_DryEndRSHCutsRequired.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_DryEndRSHCutsRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_DryEndRSHCutsRequired.ImagePassword = null;
      this.tnud_DryEndRSHCutsRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_DryEndRSHCutsRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_DryEndRSHCutsRequired.Location = new System.Drawing.Point(154, 235);
      this.tnud_DryEndRSHCutsRequired.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.tnud_DryEndRSHCutsRequired.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_DryEndRSHCutsRequired.Name = "tnud_DryEndRSHCutsRequired";
      this.tnud_DryEndRSHCutsRequired.ReadOnly = true;
      this.tnud_DryEndRSHCutsRequired.Size = new System.Drawing.Size(166, 78);
      this.tnud_DryEndRSHCutsRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_DryEndRSHCutsRequired.TabIndex = 1;
      this.tnud_DryEndRSHCutsRequired.Text = "Dry End";
      this.tnud_DryEndRSHCutsRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_DryEndRSHCutsRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_DryEndRSHCutsRequired.TranslateText = true;
      this.tnud_DryEndRSHCutsRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_DryEndRSHCutsRequired.UnitMeasureVisible = false;
      this.tnud_DryEndRSHCutsRequired.Value = "1";
      this.tnud_DryEndRSHCutsRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_DryEndRSHCutsRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_DryEndRSHCutsRequired.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_DryEndRSHCutsRequired_Validating);
      // 
      // tnud_WetEndRSHCutsRequired
      // 
      this.tnud_WetEndRSHCutsRequired.DecimalPlaces = 0;
      this.tnud_WetEndRSHCutsRequired.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_WetEndRSHCutsRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndRSHCutsRequired.ImagePassword = null;
      this.tnud_WetEndRSHCutsRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndRSHCutsRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WetEndRSHCutsRequired.Location = new System.Drawing.Point(333, 235);
      this.tnud_WetEndRSHCutsRequired.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.tnud_WetEndRSHCutsRequired.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndRSHCutsRequired.Name = "tnud_WetEndRSHCutsRequired";
      this.tnud_WetEndRSHCutsRequired.ReadOnly = true;
      this.tnud_WetEndRSHCutsRequired.Size = new System.Drawing.Size(166, 78);
      this.tnud_WetEndRSHCutsRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WetEndRSHCutsRequired.TabIndex = 2;
      this.tnud_WetEndRSHCutsRequired.Text = "Normal Quality Change";
      this.tnud_WetEndRSHCutsRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndRSHCutsRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndRSHCutsRequired.TranslateText = true;
      this.tnud_WetEndRSHCutsRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WetEndRSHCutsRequired.UnitMeasureVisible = false;
      this.tnud_WetEndRSHCutsRequired.Value = "1";
      this.tnud_WetEndRSHCutsRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndRSHCutsRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_WetEndRSHCutsRequired.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_WetEndRSHCutsRequired_Validating);
      // 
      // tnud_operatorMaxProductionSpeed
      // 
      this.tnud_operatorMaxProductionSpeed.DecimalPlaces = 0;
      this.tnud_operatorMaxProductionSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_operatorMaxProductionSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_operatorMaxProductionSpeed.ImagePassword = null;
      this.tnud_operatorMaxProductionSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_operatorMaxProductionSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_operatorMaxProductionSpeed.Location = new System.Drawing.Point(153, 386);
      this.tnud_operatorMaxProductionSpeed.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
      this.tnud_operatorMaxProductionSpeed.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.tnud_operatorMaxProductionSpeed.Name = "tnud_operatorMaxProductionSpeed";
      this.tnud_operatorMaxProductionSpeed.ReadOnly = true;
      this.tnud_operatorMaxProductionSpeed.Size = new System.Drawing.Size(166, 78);
      this.tnud_operatorMaxProductionSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_operatorMaxProductionSpeed.TabIndex = 10;
      this.tnud_operatorMaxProductionSpeed.Text = "Max Speed";
      this.tnud_operatorMaxProductionSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_operatorMaxProductionSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_operatorMaxProductionSpeed.TranslateText = true;
      this.tnud_operatorMaxProductionSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_operatorMaxProductionSpeed.Value = "5000";
      this.tnud_operatorMaxProductionSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_operatorMaxProductionSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_operatorMaxProductionSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_operatorMaxProductionSpeed_Validating);
      // 
      // ttl_ProductionSpeed
      // 
      this.ttl_ProductionSpeed.BackColor = System.Drawing.Color.Black;
      this.ttl_ProductionSpeed.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ProductionSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ProductionSpeed.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ProductionSpeed.Location = new System.Drawing.Point(140, 338);
      this.ttl_ProductionSpeed.Name = "ttl_ProductionSpeed";
      this.ttl_ProductionSpeed.Size = new System.Drawing.Size(948, 231);
      this.ttl_ProductionSpeed.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ProductionSpeed.TabIndex = 215;
      this.ttl_ProductionSpeed.TabStop = false;
      this.ttl_ProductionSpeed.Text = "Production Speed";
      this.ttl_ProductionSpeed.TranslateText = true;
      // 
      // tnud_slowSpeed
      // 
      this.tnud_slowSpeed.DecimalPlaces = 0;
      this.tnud_slowSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_slowSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_slowSpeed.ImagePassword = null;
      this.tnud_slowSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_slowSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_slowSpeed.Location = new System.Drawing.Point(330, 386);
      this.tnud_slowSpeed.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
      this.tnud_slowSpeed.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.tnud_slowSpeed.Name = "tnud_slowSpeed";
      this.tnud_slowSpeed.ReadOnly = true;
      this.tnud_slowSpeed.Size = new System.Drawing.Size(166, 78);
      this.tnud_slowSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_slowSpeed.TabIndex = 11;
      this.tnud_slowSpeed.Text = "Slow Speed";
      this.tnud_slowSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_slowSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_slowSpeed.TranslateText = true;
      this.tnud_slowSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_slowSpeed.Value = "5000";
      this.tnud_slowSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_slowSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_slowSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_slowSpeed_Validating);
      // 
      // tnud_spliceRemovalRSHCutsRequired
      // 
      this.tnud_spliceRemovalRSHCutsRequired.DecimalPlaces = 0;
      this.tnud_spliceRemovalRSHCutsRequired.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_spliceRemovalRSHCutsRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_spliceRemovalRSHCutsRequired.ImagePassword = null;
      this.tnud_spliceRemovalRSHCutsRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_spliceRemovalRSHCutsRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_spliceRemovalRSHCutsRequired.Location = new System.Drawing.Point(918, 235);
      this.tnud_spliceRemovalRSHCutsRequired.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.tnud_spliceRemovalRSHCutsRequired.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_spliceRemovalRSHCutsRequired.Name = "tnud_spliceRemovalRSHCutsRequired";
      this.tnud_spliceRemovalRSHCutsRequired.ReadOnly = true;
      this.tnud_spliceRemovalRSHCutsRequired.Size = new System.Drawing.Size(166, 78);
      this.tnud_spliceRemovalRSHCutsRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_spliceRemovalRSHCutsRequired.TabIndex = 3;
      this.tnud_spliceRemovalRSHCutsRequired.Text = "Splice Removal";
      this.tnud_spliceRemovalRSHCutsRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_spliceRemovalRSHCutsRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_spliceRemovalRSHCutsRequired.TranslateText = true;
      this.tnud_spliceRemovalRSHCutsRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_spliceRemovalRSHCutsRequired.UnitMeasureVisible = false;
      this.tnud_spliceRemovalRSHCutsRequired.Value = "1";
      this.tnud_spliceRemovalRSHCutsRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_spliceRemovalRSHCutsRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_spliceRemovalRSHCutsRequired.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_spliceRemovalRSHCutsRequired_Validating);
      // 
      // chk_autoSpeedModeEnabled
      // 
      this.chk_autoSpeedModeEnabled.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.chk_autoSpeedModeEnabled.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_autoSpeedModeEnabled.Checked = true;
      this.chk_autoSpeedModeEnabled.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_autoSpeedModeEnabled.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_autoSpeedModeEnabled.ImagePassword = null;
      this.chk_autoSpeedModeEnabled.Location = new System.Drawing.Point(17, 76);
      this.chk_autoSpeedModeEnabled.Name = "chk_autoSpeedModeEnabled";
      this.chk_autoSpeedModeEnabled.ReadOnly = true;
      this.chk_autoSpeedModeEnabled.Size = new System.Drawing.Size(124, 78);
      this.chk_autoSpeedModeEnabled.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_autoSpeedModeEnabled.TabIndex = 15;
      this.chk_autoSpeedModeEnabled.Text = "Auto Speed";
      this.chk_autoSpeedModeEnabled.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_autoSpeedModeEnabled.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_autoSpeedModeEnabled.TranslateText = true;
      this.chk_autoSpeedModeEnabled.OnCheckedChanged += new System.EventHandler(this.chk_autoSpeedModeEnabled_OnCheckedChanged);
      // 
      // ttl_AOCSpeed
      // 
      this.ttl_AOCSpeed.BackColor = System.Drawing.Color.Black;
      this.ttl_AOCSpeed.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_AOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_AOCSpeed.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_AOCSpeed.Location = new System.Drawing.Point(140, 572);
      this.ttl_AOCSpeed.Name = "ttl_AOCSpeed";
      this.ttl_AOCSpeed.Size = new System.Drawing.Size(372, 140);
      this.ttl_AOCSpeed.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_AOCSpeed.TabIndex = 276;
      this.ttl_AOCSpeed.TabStop = false;
      this.ttl_AOCSpeed.Text = "AOC Speed";
      this.ttl_AOCSpeed.TranslateText = true;
      // 
      // tnud_OperatorMaxAOCSpeed
      // 
      this.tnud_OperatorMaxAOCSpeed.DecimalPlaces = 0;
      this.tnud_OperatorMaxAOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_OperatorMaxAOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_OperatorMaxAOCSpeed.ImagePassword = null;
      this.tnud_OperatorMaxAOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_OperatorMaxAOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_OperatorMaxAOCSpeed.Location = new System.Drawing.Point(153, 614);
      this.tnud_OperatorMaxAOCSpeed.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
      this.tnud_OperatorMaxAOCSpeed.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.tnud_OperatorMaxAOCSpeed.Name = "tnud_OperatorMaxAOCSpeed";
      this.tnud_OperatorMaxAOCSpeed.ReadOnly = true;
      this.tnud_OperatorMaxAOCSpeed.Size = new System.Drawing.Size(166, 78);
      this.tnud_OperatorMaxAOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_OperatorMaxAOCSpeed.TabIndex = 20;
      this.tnud_OperatorMaxAOCSpeed.Text = "Max Speed";
      this.tnud_OperatorMaxAOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_OperatorMaxAOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_OperatorMaxAOCSpeed.TranslateText = true;
      this.tnud_OperatorMaxAOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_OperatorMaxAOCSpeed.Value = "5000";
      this.tnud_OperatorMaxAOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_OperatorMaxAOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_OperatorMaxAOCSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_OperatorMaxAOCSpeed_Validating);
      // 
      // tnud_OperatorSlowAOCSpeed
      // 
      this.tnud_OperatorSlowAOCSpeed.DecimalPlaces = 0;
      this.tnud_OperatorSlowAOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_OperatorSlowAOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_OperatorSlowAOCSpeed.ImagePassword = null;
      this.tnud_OperatorSlowAOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_OperatorSlowAOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_OperatorSlowAOCSpeed.Location = new System.Drawing.Point(331, 614);
      this.tnud_OperatorSlowAOCSpeed.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
      this.tnud_OperatorSlowAOCSpeed.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.tnud_OperatorSlowAOCSpeed.Name = "tnud_OperatorSlowAOCSpeed";
      this.tnud_OperatorSlowAOCSpeed.ReadOnly = true;
      this.tnud_OperatorSlowAOCSpeed.Size = new System.Drawing.Size(166, 78);
      this.tnud_OperatorSlowAOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_OperatorSlowAOCSpeed.TabIndex = 21;
      this.tnud_OperatorSlowAOCSpeed.Text = "Slow Speed";
      this.tnud_OperatorSlowAOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_OperatorSlowAOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_OperatorSlowAOCSpeed.TranslateText = true;
      this.tnud_OperatorSlowAOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_OperatorSlowAOCSpeed.Value = "5000";
      this.tnud_OperatorSlowAOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_OperatorSlowAOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_OperatorSlowAOCSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_OperatorSlowAOCSpeed_Validating);
      // 
      // chk_AutoSpeedAtProductionSpeed
      // 
      this.chk_AutoSpeedAtProductionSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.chk_AutoSpeedAtProductionSpeed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AutoSpeedAtProductionSpeed.Checked = true;
      this.chk_AutoSpeedAtProductionSpeed.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_AutoSpeedAtProductionSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_AutoSpeedAtProductionSpeed.ImagePassword = null;
      this.chk_AutoSpeedAtProductionSpeed.Location = new System.Drawing.Point(158, 8);
      this.chk_AutoSpeedAtProductionSpeed.Name = "chk_AutoSpeedAtProductionSpeed";
      this.chk_AutoSpeedAtProductionSpeed.ReadOnly = true;
      this.chk_AutoSpeedAtProductionSpeed.Size = new System.Drawing.Size(246, 62);
      this.chk_AutoSpeedAtProductionSpeed.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_AutoSpeedAtProductionSpeed.TabIndex = 16;
      this.chk_AutoSpeedAtProductionSpeed.Text = "At Production";
      this.chk_AutoSpeedAtProductionSpeed.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AutoSpeedAtProductionSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_AutoSpeedAtProductionSpeed.TranslateText = true;
      this.chk_AutoSpeedAtProductionSpeed.OnCheckedChanged += new System.EventHandler(this.chk_AutoSpeedAtProductionSpeed_OnCheckedChanged);
      // 
      // tnud_WetEndFluteChangeRSHCutsRequired
      // 
      this.tnud_WetEndFluteChangeRSHCutsRequired.DecimalPlaces = 0;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_WetEndFluteChangeRSHCutsRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndFluteChangeRSHCutsRequired.ImagePassword = null;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndFluteChangeRSHCutsRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Location = new System.Drawing.Point(740, 235);
      this.tnud_WetEndFluteChangeRSHCutsRequired.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.tnud_WetEndFluteChangeRSHCutsRequired.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.tnud_WetEndFluteChangeRSHCutsRequired.Name = "tnud_WetEndFluteChangeRSHCutsRequired";
      this.tnud_WetEndFluteChangeRSHCutsRequired.ReadOnly = true;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Size = new System.Drawing.Size(166, 78);
      this.tnud_WetEndFluteChangeRSHCutsRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WetEndFluteChangeRSHCutsRequired.TabIndex = 277;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Text = "Flute/Deckle Change";
      this.tnud_WetEndFluteChangeRSHCutsRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndFluteChangeRSHCutsRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndFluteChangeRSHCutsRequired.TranslateText = true;
      this.tnud_WetEndFluteChangeRSHCutsRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WetEndFluteChangeRSHCutsRequired.UnitMeasureVisible = false;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Value = "5";
      this.tnud_WetEndFluteChangeRSHCutsRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndFluteChangeRSHCutsRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_WetEndFluteChangeRSHCutsRequired.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_WetEndFluteChangeRSHCutsRequired_Validation);
      // 
      // ttl_AluminiumScrap
      // 
      this.ttl_AluminiumScrap.BackColor = System.Drawing.Color.Black;
      this.ttl_AluminiumScrap.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_AluminiumScrap.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_AluminiumScrap.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_AluminiumScrap.Location = new System.Drawing.Point(515, 572);
      this.ttl_AluminiumScrap.Name = "ttl_AluminiumScrap";
      this.ttl_AluminiumScrap.Size = new System.Drawing.Size(573, 140);
      this.ttl_AluminiumScrap.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_AluminiumScrap.TabIndex = 278;
      this.ttl_AluminiumScrap.TabStop = false;
      this.ttl_AluminiumScrap.Text = "Aluminium Scrap";
      this.ttl_AluminiumScrap.TranslateText = true;
      // 
      // chk_aluminiumScrapAOC
      // 
      this.chk_aluminiumScrapAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_aluminiumScrapAOC.Checked = true;
      this.chk_aluminiumScrapAOC.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_aluminiumScrapAOC.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_aluminiumScrapAOC.ImagePassword = null;
      this.chk_aluminiumScrapAOC.Location = new System.Drawing.Point(533, 614);
      this.chk_aluminiumScrapAOC.Name = "chk_aluminiumScrapAOC";
      this.chk_aluminiumScrapAOC.ReadOnly = true;
      this.chk_aluminiumScrapAOC.Size = new System.Drawing.Size(166, 78);
      this.chk_aluminiumScrapAOC.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_aluminiumScrapAOC.TabIndex = 279;
      this.chk_aluminiumScrapAOC.Text = "At Order Change";
      this.chk_aluminiumScrapAOC.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_aluminiumScrapAOC.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_aluminiumScrapAOC.TranslateText = true;
      // 
      // chk_aluminiumScrapNP
      // 
      this.chk_aluminiumScrapNP.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_aluminiumScrapNP.Checked = true;
      this.chk_aluminiumScrapNP.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_aluminiumScrapNP.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_aluminiumScrapNP.ImagePassword = null;
      this.chk_aluminiumScrapNP.Location = new System.Drawing.Point(711, 614);
      this.chk_aluminiumScrapNP.Name = "chk_aluminiumScrapNP";
      this.chk_aluminiumScrapNP.ReadOnly = true;
      this.chk_aluminiumScrapNP.Size = new System.Drawing.Size(166, 78);
      this.chk_aluminiumScrapNP.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_aluminiumScrapNP.TabIndex = 280;
      this.chk_aluminiumScrapNP.Text = "Normal Production";
      this.chk_aluminiumScrapNP.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_aluminiumScrapNP.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_aluminiumScrapNP.TranslateText = true;
      // 
      // tnud_orderMaxProductionSpeed_Length
      // 
      this.tnud_orderMaxProductionSpeed_Length.DecimalPlaces = 0;
      this.tnud_orderMaxProductionSpeed_Length.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.tnud_orderMaxProductionSpeed_Length.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_orderMaxProductionSpeed_Length.ImagePassword = null;
      this.tnud_orderMaxProductionSpeed_Length.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_orderMaxProductionSpeed_Length.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_orderMaxProductionSpeed_Length.Location = new System.Drawing.Point(152, 470);
      this.tnud_orderMaxProductionSpeed_Length.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
      this.tnud_orderMaxProductionSpeed_Length.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_orderMaxProductionSpeed_Length.Name = "tnud_orderMaxProductionSpeed_Length";
      this.tnud_orderMaxProductionSpeed_Length.ReadOnly = true;
      this.tnud_orderMaxProductionSpeed_Length.Size = new System.Drawing.Size(166, 78);
      this.tnud_orderMaxProductionSpeed_Length.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_orderMaxProductionSpeed_Length.TabIndex = 282;
      this.tnud_orderMaxProductionSpeed_Length.Text = "Short Order Length";
      this.tnud_orderMaxProductionSpeed_Length.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_orderMaxProductionSpeed_Length.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_orderMaxProductionSpeed_Length.TranslateText = true;
      this.tnud_orderMaxProductionSpeed_Length.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_orderMaxProductionSpeed_Length.Value = "0";
      this.tnud_orderMaxProductionSpeed_Length.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_orderMaxProductionSpeed_Length.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_orderMaxProductionSpeed_Length.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_OperatorOrderLengthMaxSpeedProduction_Validating);
      // 
      // grp_SpeedType
      // 
      this.grp_SpeedType.Controls.Add(this.chk_shortOrderSpeed);
      this.grp_SpeedType.Controls.Add(this.chk_AutoSpeedAtMaxSpeed);
      this.grp_SpeedType.Controls.Add(this.chk_AutoSpeedAtTargetSpeed);
      this.grp_SpeedType.Controls.Add(this.chk_AutoSpeedAtProductionSpeed);
      this.grp_SpeedType.Controls.Add(this.chk_autoSpeedModeEnabled);
      this.grp_SpeedType.Location = new System.Drawing.Point(515, 360);
      this.grp_SpeedType.Name = "grp_SpeedType";
      this.grp_SpeedType.Size = new System.Drawing.Size(573, 210);
      this.grp_SpeedType.TabIndex = 283;
      this.grp_SpeedType.TabStop = false;
      // 
      // chk_shortOrderSpeed
      // 
      this.chk_shortOrderSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.chk_shortOrderSpeed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_shortOrderSpeed.Checked = false;
      this.chk_shortOrderSpeed.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_shortOrderSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_shortOrderSpeed.ImagePassword = null;
      this.chk_shortOrderSpeed.Location = new System.Drawing.Point(427, 76);
      this.chk_shortOrderSpeed.Name = "chk_shortOrderSpeed";
      this.chk_shortOrderSpeed.ReadOnly = true;
      this.chk_shortOrderSpeed.Size = new System.Drawing.Size(143, 62);
      this.chk_shortOrderSpeed.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_shortOrderSpeed.TabIndex = 19;
      this.chk_shortOrderSpeed.Text = "Short Order Speed";
      this.chk_shortOrderSpeed.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_shortOrderSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_shortOrderSpeed.TranslateText = true;
      // 
      // chk_AutoSpeedAtMaxSpeed
      // 
      this.chk_AutoSpeedAtMaxSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.chk_AutoSpeedAtMaxSpeed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AutoSpeedAtMaxSpeed.Checked = false;
      this.chk_AutoSpeedAtMaxSpeed.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_AutoSpeedAtMaxSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_AutoSpeedAtMaxSpeed.ImagePassword = null;
      this.chk_AutoSpeedAtMaxSpeed.Location = new System.Drawing.Point(158, 139);
      this.chk_AutoSpeedAtMaxSpeed.Name = "chk_AutoSpeedAtMaxSpeed";
      this.chk_AutoSpeedAtMaxSpeed.ReadOnly = true;
      this.chk_AutoSpeedAtMaxSpeed.Size = new System.Drawing.Size(246, 62);
      this.chk_AutoSpeedAtMaxSpeed.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_AutoSpeedAtMaxSpeed.TabIndex = 18;
      this.chk_AutoSpeedAtMaxSpeed.Text = "At Max";
      this.chk_AutoSpeedAtMaxSpeed.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AutoSpeedAtMaxSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_AutoSpeedAtMaxSpeed.TranslateText = true;
      this.chk_AutoSpeedAtMaxSpeed.OnCheckedChanged += new System.EventHandler(this.chk_AutoSpeedAtMaxSpeed_OnCheckedChanged);
      // 
      // chk_AutoSpeedAtTargetSpeed
      // 
      this.chk_AutoSpeedAtTargetSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.chk_AutoSpeedAtTargetSpeed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AutoSpeedAtTargetSpeed.Checked = false;
      this.chk_AutoSpeedAtTargetSpeed.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_AutoSpeedAtTargetSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_AutoSpeedAtTargetSpeed.ImagePassword = null;
      this.chk_AutoSpeedAtTargetSpeed.Location = new System.Drawing.Point(158, 76);
      this.chk_AutoSpeedAtTargetSpeed.Name = "chk_AutoSpeedAtTargetSpeed";
      this.chk_AutoSpeedAtTargetSpeed.ReadOnly = true;
      this.chk_AutoSpeedAtTargetSpeed.Size = new System.Drawing.Size(246, 62);
      this.chk_AutoSpeedAtTargetSpeed.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_AutoSpeedAtTargetSpeed.TabIndex = 17;
      this.chk_AutoSpeedAtTargetSpeed.Text = "At Target";
      this.chk_AutoSpeedAtTargetSpeed.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AutoSpeedAtTargetSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_AutoSpeedAtTargetSpeed.TranslateText = true;
      this.chk_AutoSpeedAtTargetSpeed.OnCheckedChanged += new System.EventHandler(this.chk_AutoSpeedAtTargetSpeed_OnCheckedChanged);
      // 
      // chk_enableNoScrapWhenOnlyInternalSplice
      // 
      this.chk_enableNoScrapWhenOnlyInternalSplice.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.chk_enableNoScrapWhenOnlyInternalSplice.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_enableNoScrapWhenOnlyInternalSplice.Checked = true;
      this.chk_enableNoScrapWhenOnlyInternalSplice.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_enableNoScrapWhenOnlyInternalSplice.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_enableNoScrapWhenOnlyInternalSplice.ImagePassword = null;
      this.chk_enableNoScrapWhenOnlyInternalSplice.Location = new System.Drawing.Point(511, 236);
      this.chk_enableNoScrapWhenOnlyInternalSplice.Name = "chk_enableNoScrapWhenOnlyInternalSplice";
      this.chk_enableNoScrapWhenOnlyInternalSplice.ReadOnly = true;
      this.chk_enableNoScrapWhenOnlyInternalSplice.Size = new System.Drawing.Size(220, 78);
      this.chk_enableNoScrapWhenOnlyInternalSplice.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_enableNoScrapWhenOnlyInternalSplice.TabIndex = 20;
      this.chk_enableNoScrapWhenOnlyInternalSplice.Text = "No Scrap without liner splicers";
      this.chk_enableNoScrapWhenOnlyInternalSplice.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_enableNoScrapWhenOnlyInternalSplice.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_enableNoScrapWhenOnlyInternalSplice.TranslateText = true;
      // 
      // Frm_Operator
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.chk_enableNoScrapWhenOnlyInternalSplice);
      this.Controls.Add(this.grp_SpeedType);
      this.Controls.Add(this.tnud_orderMaxProductionSpeed_Length);
      this.Controls.Add(this.chk_aluminiumScrapNP);
      this.Controls.Add(this.chk_aluminiumScrapAOC);
      this.Controls.Add(this.ttl_AluminiumScrap);
      this.Controls.Add(this.tnud_WetEndFluteChangeRSHCutsRequired);
      this.Controls.Add(this.tnud_OperatorSlowAOCSpeed);
      this.Controls.Add(this.tnud_OperatorMaxAOCSpeed);
      this.Controls.Add(this.ttl_AOCSpeed);
      this.Controls.Add(this.tnud_spliceRemovalRSHCutsRequired);
      this.Controls.Add(this.tnud_slowSpeed);
      this.Controls.Add(this.tnud_operatorMaxProductionSpeed);
      this.Controls.Add(this.ttl_ProductionSpeed);
      this.Controls.Add(this.tnud_WetEndRSHCutsRequired);
      this.Controls.Add(this.tnud_DryEndRSHCutsRequired);
      this.Controls.Add(this.ttl_Settings);
      this.DesignGridHorizontalLines = 2;
      this.Name = "Frm_Operator";
      this.Text = "Frm_Operator";
      this.Activated += new System.EventHandler(this.Frm_Operator_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.ttl_Settings, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab5, 0);
      this.Controls.SetChildIndex(this.tab_Tab6, 0);
      this.Controls.SetChildIndex(this.tab_Tab7, 0);
      this.Controls.SetChildIndex(this.tab_Tab8, 0);
      this.Controls.SetChildIndex(this.tab_TabA, 0);
      this.Controls.SetChildIndex(this.tab_TabD, 0);
      this.Controls.SetChildIndex(this.tab_TabC, 0);
      this.Controls.SetChildIndex(this.tab_TabB, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.tnud_DryEndRSHCutsRequired, 0);
      this.Controls.SetChildIndex(this.tnud_WetEndRSHCutsRequired, 0);
      this.Controls.SetChildIndex(this.ttl_ProductionSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_operatorMaxProductionSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_slowSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_spliceRemovalRSHCutsRequired, 0);
      this.Controls.SetChildIndex(this.ttl_AOCSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_OperatorMaxAOCSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_OperatorSlowAOCSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_WetEndFluteChangeRSHCutsRequired, 0);
      this.Controls.SetChildIndex(this.ttl_AluminiumScrap, 0);
      this.Controls.SetChildIndex(this.chk_aluminiumScrapAOC, 0);
      this.Controls.SetChildIndex(this.chk_aluminiumScrapNP, 0);
      this.Controls.SetChildIndex(this.tnud_orderMaxProductionSpeed_Length, 0);
      this.Controls.SetChildIndex(this.grp_SpeedType, 0);
      this.Controls.SetChildIndex(this.chk_enableNoScrapWhenOnlyInternalSplice, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_SpeedType.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion

		private void readOnly(bool theReadOnly)
		{
			//readonly
			this.tnud_DryEndRSHCutsRequired.ReadOnly = theReadOnly;
			this.tnud_WetEndRSHCutsRequired.ReadOnly = theReadOnly;
			//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
			this.tnud_WetEndFluteChangeRSHCutsRequired.ReadOnly = theReadOnly;
			this.tnud_spliceRemovalRSHCutsRequired.ReadOnly = theReadOnly;
			this.tnud_operatorMaxProductionSpeed.ReadOnly = theReadOnly;
			this.tnud_slowSpeed.ReadOnly = theReadOnly;
			this.tnud_OperatorMaxAOCSpeed.ReadOnly = theReadOnly;
			this.tnud_OperatorSlowAOCSpeed.ReadOnly = theReadOnly;
			this.chk_autoSpeedModeEnabled.ReadOnly = theReadOnly;
			this.chk_AutoSpeedAtProductionSpeed.ReadOnly = theReadOnly;
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      this.chk_AutoSpeedAtMaxSpeed.ReadOnly = theReadOnly;
      this.chk_AutoSpeedAtTargetSpeed.ReadOnly = theReadOnly;
      //agrassi_FI29-08-09_1
      this.chk_aluminiumScrapNP.ReadOnly = theReadOnly;
      this.chk_aluminiumScrapAOC.ReadOnly = theReadOnly;
      //tabstop
			this.tnud_DryEndRSHCutsRequired.TabStop = !theReadOnly;
			this.tnud_WetEndRSHCutsRequired.TabStop = !theReadOnly;
			//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
			this.tnud_WetEndFluteChangeRSHCutsRequired.TabStop = !theReadOnly;
			this.tnud_spliceRemovalRSHCutsRequired.TabStop = !theReadOnly;
			this.tnud_operatorMaxProductionSpeed.TabStop = !theReadOnly;
			this.tnud_slowSpeed.TabStop = !theReadOnly;
			this.tnud_OperatorMaxAOCSpeed.TabStop = !theReadOnly;
			this.tnud_OperatorSlowAOCSpeed.TabStop = !theReadOnly;
			this.chk_autoSpeedModeEnabled.TabStop = !theReadOnly;
			this.chk_AutoSpeedAtProductionSpeed.TabStop = !theReadOnly;
      //agrassi_FI29-08-09_1
      this.chk_aluminiumScrapNP.TabStop = !theReadOnly;
      this.chk_aluminiumScrapAOC.TabStop = !theReadOnly;
      //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
      this.tnud_orderMaxProductionSpeed_Length.ReadOnly = theReadOnly;
      this.tnud_orderMaxProductionSpeed_Length.TabStop = !theReadOnly;
      //agrassi_FI23-03-2011_3
      this.chk_shortOrderSpeed.TabStop = !theReadOnly;
      this.chk_shortOrderSpeed.ReadOnly = theReadOnly;
      //RFAZZI_FI12/09/2011_1. . Aggiunta funzionalità di "Disabilita scarto giunte interne al cartone". Se abilitato, Syncro esegue lo scarto al cambio qualità solo se presente almeno una giunta sulle copertini del cartone altrimenti eseguirà lo scarto quantificato come da pagina operatore. 
      this.chk_enableNoScrapWhenOnlyInternalSplice.TabStop = !theReadOnly;
      this.chk_enableNoScrapWhenOnlyInternalSplice.ReadOnly = theReadOnly;
    }

		public override void refreshData()
		{
			SyncroC.DataBase.LineConfig.Operator cfgOperator = DBManager.getDBManager().lineConfig.OperatorConfig;
      //agrassi_FI29-08-09_1
      SyncroC.DataBase.LineConfig.Syncro cfgSyncro = DBManager.getDBManager().lineConfig.SyncroConfig;

			this.tnud_DryEndRSHCutsRequired.Value = cfgOperator.DryEndRSHCutsRequired.ToString();
			this.tnud_WetEndRSHCutsRequired.Value = cfgOperator.WetEndRSHCutsRequired.ToString();
			//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
			this.tnud_WetEndFluteChangeRSHCutsRequired.Value = cfgOperator.WetEndFluteChangeRSHCutsRequired.ToString();
			this.tnud_spliceRemovalRSHCutsRequired.Value = cfgOperator.SpliceRemovalRSHCutsRequired.ToString();
			this.tnud_operatorMaxProductionSpeed.Value = cfgOperator.OperatorMaxProductionSpeed.ToString();
			this.tnud_slowSpeed.Value = cfgOperator.SlowSpeed_UVmm_m.ToString();
			this.tnud_OperatorMaxAOCSpeed.Value = cfgOperator.MaxAOCSpeed_UVmm_m.ToString();
			this.tnud_OperatorSlowAOCSpeed.Value = cfgOperator.SlowAOCSpeed_UVmm_m.ToString();
			this.chk_autoSpeedModeEnabled.Checked = cfgOperator.AutoSpeedMode;

      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
			this.chk_AutoSpeedAtProductionSpeed.Checked = (cfgOperator.TypeAutoSpeedToProductionSpeed == 0);
      this.chk_AutoSpeedAtTargetSpeed.Checked = (cfgOperator.TypeAutoSpeedToProductionSpeed == 1);
      this.chk_AutoSpeedAtMaxSpeed.Checked = (cfgOperator.TypeAutoSpeedToProductionSpeed == 2);
      
      //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
      this.tnud_orderMaxProductionSpeed_Length.Value = cfgOperator.OrderMaxProductionSpeed_Length_UDm.ToString();
      //agrassi_FI23-03-2011_3
      this.tnud_orderMaxProductionSpeed_Length.Visible = false;
      this.chk_shortOrderSpeed.Checked = cfgOperator.LimitateSpeedAccordingToPosTime;

      //agrassi_FI29-08-09_1
      bool allNPTrue=true, atLeastOneNPTrue=false;
      bool allAOCTrue=true, atLeastOneAOCTrue=false;
      for (int i=0; i<4; i++)
      {
        allNPTrue=allNPTrue && cfgSyncro.ScrapSpliceDetected[i].EnableScrapForSpliceDetected;
        atLeastOneNPTrue=atLeastOneNPTrue || cfgSyncro.ScrapSpliceDetected[i].EnableScrapForSpliceDetected;
        allAOCTrue=allAOCTrue && cfgSyncro.ScrapSpliceDetected[i].EnableScrapAOCForSpliceDetected;
        atLeastOneAOCTrue=atLeastOneAOCTrue || cfgSyncro.ScrapSpliceDetected[i].EnableScrapAOCForSpliceDetected;
      }
      if (allNPTrue)
      {
        this.chk_aluminiumScrapNP.Checked = true;
        this.chk_aluminiumScrapNP.BackColor = Color.WhiteSmoke;
      }
      else if (!atLeastOneNPTrue)
      {
        this.chk_aluminiumScrapNP.Checked = false;
        this.chk_aluminiumScrapNP.BackColor = Color.WhiteSmoke;
      }
      else
      {
        this.chk_aluminiumScrapNP.Checked = true;
        this.chk_aluminiumScrapNP.BackColor = Color.LightGray;
      }
      if (allAOCTrue)
      {
        this.chk_aluminiumScrapAOC.Checked = true;
        this.chk_aluminiumScrapAOC.BackColor = Color.WhiteSmoke;
      }
      else if (!atLeastOneAOCTrue)
      {
        this.chk_aluminiumScrapAOC.Checked = false;
        this.chk_aluminiumScrapAOC.BackColor = Color.WhiteSmoke;
      }
      else
      {
        this.chk_aluminiumScrapAOC.Checked = true;
        this.chk_aluminiumScrapAOC.BackColor = Color.LightGray;
      }

      //RFAZZI_FI12/09/2011_1. . Aggiunta funzionalità di "Disabilita scarto giunte interne al cartone". Se abilitato, Syncro esegue lo scarto al cambio qualità solo se presente almeno una giunta sulle copertini del cartone altrimenti eseguirà lo scarto quantificato come da pagina operatore. 
      this.chk_enableNoScrapWhenOnlyInternalSplice.Checked = cfgOperator.EnableNoScrapWhenOnlyInternalSplice;

    }

		private void tnud_DryEndRSHCutsRequired_Validating(object sender, CancelEventArgs e)
		{
			DryEndRSHCutsRequired_Warning = 
				(tnud_DryEndRSHCutsRequired.getValueAsInt() > LocalValidationLimits.DryEndRSHCutsRequired_WarningMaxLimit);

			if (DryEndRSHCutsRequired_Warning)
			{
				string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
				tnud_DryEndRSHCutsRequired.SetError(text);
			}
			else
				tnud_DryEndRSHCutsRequired.SetError(string.Empty);
		}

		private void tnud_WetEndRSHCutsRequired_Validating(object sender, CancelEventArgs e)
		{
			WetEndRSHCutsRequired_Warning =
				((tnud_WetEndRSHCutsRequired.getValueAsInt() > LocalValidationLimits.WetEndRSHCutsRequired_WarningMaxLimit) ||
				(tnud_WetEndRSHCutsRequired.getValueAsInt() < LocalValidationLimits.WetEndRSHCutsRequired_WarningMinLimit));

			if (WetEndRSHCutsRequired_Warning)
			{
				string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
				tnud_WetEndRSHCutsRequired.SetError(text);
			}
			else
				tnud_WetEndRSHCutsRequired.SetError(string.Empty);
		}

		//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
		private void tnud_WetEndFluteChangeRSHCutsRequired_Validation(object sender, CancelEventArgs e)
		{
			WetEndFluteChangeRSHCutsRequired =
				((tnud_WetEndFluteChangeRSHCutsRequired.getValueAsInt() > LocalValidationLimits.WetEndRSHCutsRequired_WarningMaxLimit) ||
				(tnud_WetEndFluteChangeRSHCutsRequired.getValueAsInt() < LocalValidationLimits.WetEndRSHCutsRequired_WarningMinLimit));

			if (WetEndFluteChangeRSHCutsRequired )
			{
				string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
				tnud_WetEndFluteChangeRSHCutsRequired.SetError(text);
			}
			else
				tnud_WetEndFluteChangeRSHCutsRequired.SetError(string.Empty);
		}

			private void tnud_spliceRemovalRSHCutsRequired_Validating(object sender, CancelEventArgs e)
			{
				spliceRemovalRSHCutsRequired_Warning =
					((tnud_spliceRemovalRSHCutsRequired.getValueAsInt() > LocalValidationLimits.spliceRemovalRSHCutsRequired_WarningMaxLimit) ||
					(tnud_spliceRemovalRSHCutsRequired.getValueAsInt() < LocalValidationLimits.spliceRemovalRSHCutsRequired_WarningMinLimit));

				if (spliceRemovalRSHCutsRequired_Warning)
				{
					string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
					tnud_spliceRemovalRSHCutsRequired.SetError(text);
				}
				else
					tnud_spliceRemovalRSHCutsRequired.SetError(string.Empty);
			}

			private void tnud_operatorMaxProductionSpeed_Validating(object sender, CancelEventArgs e)
			{
				operatorMaxProductionSpeed_Warning =
					(tnud_operatorMaxProductionSpeed.getValueAsInt() < LocalValidationLimits.operatorMaxProductionSpeed_WarningMinLimit_UVmm_m);

				if (operatorMaxProductionSpeed_Warning)
				{
					string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
					tnud_operatorMaxProductionSpeed.SetError(text);
				}
				else
					tnud_operatorMaxProductionSpeed.SetError(string.Empty);
			}

			private void tnud_slowSpeed_Validating(object sender, CancelEventArgs e)
			{
				slowSpeed_Warning =
					(tnud_slowSpeed.getValueAsInt() < LocalValidationLimits.slowSpeed_WarningMinLimit_UVmm_m);

				if (slowSpeed_Warning)
				{
					string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
					tnud_slowSpeed.SetError(text);
				}
				else
					tnud_slowSpeed.SetError(string.Empty);
			}

			private void tnud_OperatorMaxAOCSpeed_Validating(object sender, CancelEventArgs e)
			{
				OperatorMaxAOCSpeed_Warning =
					(tnud_OperatorMaxAOCSpeed.getValueAsInt() < LocalValidationLimits.OperatorMaxAOCSpeed_WarningMinLimit_UVmm_m);

				if (OperatorMaxAOCSpeed_Warning)
				{
					string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
					tnud_OperatorMaxAOCSpeed.SetError(text);
				}
				else
					tnud_OperatorMaxAOCSpeed.SetError(string.Empty);
			}

			private void tnud_OperatorSlowAOCSpeed_Validating(object sender, CancelEventArgs e)
			{
				OperatorSlowAOCSpeed_Warning =
					(tnud_OperatorSlowAOCSpeed.getValueAsInt() < LocalValidationLimits.OperatorSlowAOCSpeed_WarningMinLimit_UVmm_m);

				if (OperatorSlowAOCSpeed_Warning)
				{
					string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
					tnud_OperatorSlowAOCSpeed.SetError(text);
				}
				else
					tnud_OperatorSlowAOCSpeed.SetError(string.Empty);
			}

    //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
    private void tnud_OperatorOrderLengthMaxSpeedProduction_Validating(object sender, CancelEventArgs e)
    {
      OperatorOrderMaxSpeedProduction_Length_Warning =
        (tnud_orderMaxProductionSpeed_Length.getValueAsInt() > LocalValidationLimits.OperatorOrderLengthForMaxProductionSpeed_maxLength_m);

      if (OperatorOrderMaxSpeedProduction_Length_Warning)
      {
        string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
        tnud_orderMaxProductionSpeed_Length.SetError(text);
      }
      else
        tnud_orderMaxProductionSpeed_Length.SetError(string.Empty);
    }

		private void CleanErrorProvider()
		{
			tnud_DryEndRSHCutsRequired.SetError(string.Empty);
			tnud_WetEndRSHCutsRequired.SetError(string.Empty);
			//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
			tnud_WetEndFluteChangeRSHCutsRequired.SetError(string.Empty);
			tnud_spliceRemovalRSHCutsRequired.SetError(string.Empty);
			tnud_operatorMaxProductionSpeed.SetError(string.Empty);
			tnud_slowSpeed.SetError(string.Empty);
			tnud_OperatorMaxAOCSpeed.SetError(string.Empty);
			tnud_OperatorSlowAOCSpeed.SetError(string.Empty);
		}

		private void SetPageStatus(State newState)
		{
			this.nextState = newState;
			switch (newState)
			{
				case State.Modify:
					this.readOnly(false);
					tnud_DryEndRSHCutsRequired.Focus();
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
					break;
				case State.Read:
					this.readOnly(true);
					this.refreshData();
					tab_Tab3.Focus();
					this.CleanErrorProvider();
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
					break;
			}
			state = newState;
		}

		private void tab_Tab3_Leave(object sender, EventArgs e)
		{
			if (state.Equals(State.Read) && (nextState.Equals(State.Read)))
				tab_Tab3.Focus();
		}

		private void Frm_Operator_Activated(object sender, EventArgs e)
		{
			if (state.Equals(State.Read))
			{
				this.tab_Tab3.Focus();
			}
		}

    private void chk_autoSpeedModeEnabled_OnCheckedChanged(object sender, EventArgs e)
    {
      this.chk_AutoSpeedAtMaxSpeed.Visible = 
      this.chk_AutoSpeedAtTargetSpeed.Visible = 
      this.chk_AutoSpeedAtProductionSpeed.Visible = this.chk_autoSpeedModeEnabled.Checked;
      //agrassi_FI23-03-2011_3
      //agrassi_FI13-05-2011_2
      //this.chk_shortOrderSpeed.Visible = this.chk_autoSpeedModeEnabled.Checked;
      this.chk_shortOrderSpeed.Visible = this.chk_autoSpeedModeEnabled.Checked && 
              DBManager.getDBManager().lineConfig.SlitterScorer && 
              DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400();

    }

    private void chk_AutoSpeedAtProductionSpeed_OnCheckedChanged(object sender, EventArgs e)
    {
      if (!(this.chk_AutoSpeedAtMaxSpeed.Checked || this.chk_AutoSpeedAtTargetSpeed.Checked || this.chk_AutoSpeedAtProductionSpeed.Checked))
      {
        this.chk_AutoSpeedAtProductionSpeed.Checked = true;
      }
      else
      {
        if(this.chk_AutoSpeedAtProductionSpeed.Checked)
        {
          this.chk_AutoSpeedAtMaxSpeed.Checked = false;
          this.chk_AutoSpeedAtTargetSpeed.Checked = false;
        }          
      }

    }

    private void chk_AutoSpeedAtTargetSpeed_OnCheckedChanged(object sender, EventArgs e)
    {
      if (!(this.chk_AutoSpeedAtMaxSpeed.Checked || this.chk_AutoSpeedAtTargetSpeed.Checked || this.chk_AutoSpeedAtProductionSpeed.Checked))
      {
        this.chk_AutoSpeedAtProductionSpeed.Checked = true;
      }
      else
      {
        if (this.chk_AutoSpeedAtTargetSpeed.Checked)
        {
          this.chk_AutoSpeedAtMaxSpeed.Checked = false;
          this.chk_AutoSpeedAtProductionSpeed.Checked = false;
        }  
      }
    }

    private void chk_AutoSpeedAtMaxSpeed_OnCheckedChanged(object sender, EventArgs e)
    {
      if (!(this.chk_AutoSpeedAtMaxSpeed.Checked || this.chk_AutoSpeedAtTargetSpeed.Checked || this.chk_AutoSpeedAtProductionSpeed.Checked))
      {
        this.chk_AutoSpeedAtProductionSpeed.Checked = true;
      }
      else
      {
        if (this.chk_AutoSpeedAtMaxSpeed.Checked)
        {
          this.chk_AutoSpeedAtTargetSpeed.Checked = false;
          this.chk_AutoSpeedAtProductionSpeed.Checked = false;
        }
      }
    }
  }
}

