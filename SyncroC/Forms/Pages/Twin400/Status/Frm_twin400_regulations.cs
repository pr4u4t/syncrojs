using System;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Forms.PopUp;
using SyncroC.DataBase;

//agrassi_FI16-12-08_1
//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
//namespace SyncroC.Forms.Pages.Twin400.Production
namespace SyncroC.Forms.Pages.Twin400.Status
{
	//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
	//public partial class Frm_twin400_regulations : SyncroC.Forms.Pages.Twin400.Production.Frm_twin400_productionSubgroup
	public partial class Frm_twin400_regulations : SyncroC.Forms.Pages.Twin400.Status.Frm_twin400_statusSubgroup 
  {
    private bool mod=false, modTwin = false, modWeb = false, modRS = false;
    private bool PLCCommOK = true;

    public Frm_twin400_regulations()
    {
      InitializeComponent();
      string[] nonScoringTypes = System.Enum.GetNames(typeof(ScorePositOptions.NonScoringScores));
      for (int i = 0; i < nonScoringTypes.Length; i++)
        this.ttl_NonScoringType.AddItem(nonScoringTypes[i]);
      DBManager.getDBManager().order.OnDBOrderChange += new Order.DBChangedHandler(this.SetupListChange);
      this.refreshData();
      this.SetReadOnly(true);
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
        this.SpeedDigitalReference.IsButton = false;
        this.InternalFlap.IsButton = false;
        this.SlowPositioning.IsButton = false;
        this.PhotoeyeSlitShaft.IsButton = false;
        this.ChangeDefaultShaft.IsButton = false;
        this.AdjustScoreGapOffset.IsButton = false;
			}
    }

		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void F11()
		protected override void F13()
    {
    }

    /// <summary>
    /// Handles event raised by StateButton associated with commands with duration that are in the page, 
    /// build the proper command and send it to the RTC_MANAGER.
    /// </summary>
    private void OnStateButtonPushDuration(object sender, System.EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "(-1)");
    }

    /// <summary>
    /// Handles event raised by generic button that are in the page, 
    /// build the proper command and send it to the RTC_MANAGER.
    /// </summary>
    private void OnButtonPush(object sender, System.EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "()");
    }

    private void SetReadOnly(bool readOnly)
    {

			DBManager localDBM = DBManager.getDBManager();
			DataBase.LineConfig.SlitterScorer cfgSS = localDBM.lineConfig.SlitterScorerConfig;

			this.nud_OffsetSlit1.ReadOnly = this.nud_OffsetSlit2.ReadOnly =
			this.nud_OffsetScore1.ReadOnly = this.nud_OffsetScore2.ReadOnly =
			this.nud_OffsetScore3.ReadOnly = this.nud_OffsetSlitsScores.ReadOnly =
			this.nud_OffsetWebDirector.ReadOnly = this.nud_OffsetRotary.ReadOnly =
			this.nud_GapOffsetScore1.ReadOnly = this.nud_GapOffsetScore2.ReadOnly =
			this.nud_GapOffsetScore3.ReadOnly = this.ttl_NonScoringType.ReadOnly =
			this.nud_TrimScoresDistance.ReadOnly = this.nud_MinTrim.ReadOnly =
				//RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
			this.nud_TrimChutOffset.ReadOnly = readOnly;
      //AGrassi_FI06/12/2007_1
      this.nud_LoadTeflonMeters.ReadOnly = this.nud_TeflonCycleMeters.ReadOnly = readOnly;
      this.nud_OffsetSlit1.Enabled = this.nud_OffsetSlit2.Enabled =
      this.nud_OffsetScore1.Enabled = this.nud_OffsetScore2.Enabled =
      this.nud_OffsetScore3.Enabled = this.nud_OffsetSlitsScores.Enabled =
      this.nud_OffsetWebDirector.Enabled = this.nud_OffsetRotary.Enabled =
      this.nud_GapOffsetScore1.Enabled = this.nud_GapOffsetScore2.Enabled =
      this.nud_GapOffsetScore3.Enabled = this.ttl_NonScoringType.Enabled =
      this.nud_TrimScoresDistance.Enabled = this.nud_MinTrim.Enabled =
			//RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
			this.nud_TrimChutOffset.Enabled = !readOnly;
			//AGrassi_FI06/12/2007_1
      this.nud_LoadTeflonMeters.Enabled = this.nud_TeflonCycleMeters.Enabled = !readOnly;
      if (!readOnly) this.SetTrimScoresEnabling();
			//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
			if ( cfgSS.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5 )
			{
				this.scrolBarSlitConsumtionOpt.Visible = true;
				this.lbl_SlitRegulation_YN.Visible = true;
				this.lbl_SlitSubstitution_YN.Visible = true;
				this.lbl_SlitConsumptionOpt_YN.Visible = true;

				if (cfgSS.SlitterScorerHeader.PositioningOptions.Slits.WearEqualize)
					this.scrolBarSlitConsumtionOpt.Enabled = true;
				else
					this.scrolBarSlitConsumtionOpt.Enabled = false;
			}
		}

    private void SetTrimScoresEnabling()
    {
      bool enab = (this.ttl_NonScoringType.SelectedItem == Enum.Format(typeof(ScorePositOptions.NonScoringScores), ScorePositOptions.NonScoringScores.TrimScores, "g"));
      this.nud_TrimScoresDistance.Enabled = this.nud_MinTrim.Enabled=enab;  
    }

    private void ttl_NonScoringType_SelectedItemChanged(object sender, EventArgs e)
    {
      this.modTwin = this.mod;
      this.SetTrimScoresEnabling();
    }

    protected override void CtrlF5()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      //agrassi_FI14-01-2010_2
      //if (this.PLCCommOK)
      //{
        this.mod = true;
        this.SetReadOnly(false);
        this.nud_OffsetSlit1.Focus();
      //}
      //else
      //{
      //  PageManager.getPageManager()._SetMessage("Communication with PLC is necessary!");
      //}
    }

    protected override void CtrlF4()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      if (!this.mod)
        return;
      Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_172");
      if (conf.ShowDialog(this, text).Equals(DialogResult.OK))
      {
        if (this.modTwin)
        {
          DataBase.LineConfig.SlitterScorer cfgSS = (DataBase.LineConfig.SlitterScorer)DBManager.getDBManager().lineConfig.SlitterScorerConfig.CreateCopy();
					cfgSS.SlitterScorerHeader.SlitShafts[0].RobotOffset = this.intParse(this.nud_OffsetSlit1.Value);
					cfgSS.SlitterScorerHeader.SlitShafts[1].RobotOffset = this.intParse(this.nud_OffsetSlit2.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[0].RobotOffset = this.intParse(this.nud_OffsetScore1.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[1].RobotOffset = this.intParse(this.nud_OffsetScore2.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[2].RobotOffset = this.intParse(this.nud_OffsetScore3.Value);
					//RFAZZI PROVA
					//cfgSS.PositioningOptions.Scores.ScoreSlitOffset = this.intParse(this.nud_OffsetSlitsScores.Value);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.ScoreSlitOffset = this.intParse(this.nud_OffsetSlitsScores.Value);

					cfgSS.SlitterScorerHeader.ScoreShafts[0].ScoreGapOffset = this.intParse(this.nud_GapOffsetScore1.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[1].ScoreGapOffset = this.intParse(this.nud_GapOffsetScore2.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[2].ScoreGapOffset = this.intParse(this.nud_GapOffsetScore3.Value);
					//RFAZZI PROVA
					//cfgSS.PositioningOptions.Scores.NonScoringScoreType = ScorePositOptions.NonScoringScores.NoScore;
					//if (this.ttl_NonScoringType.SelectedItem != string.Empty)
					//  cfgSS.PositioningOptions.Scores.NonScoringScoreType = (ScorePositOptions.NonScoringScores)Enum.Parse(typeof(ScorePositOptions.NonScoringScores), this.ttl_NonScoringType.SelectedItem, true);
					//cfgSS.PositioningOptions.Scores.TrimScoreDistance = this.intParse(this.nud_TrimScoresDistance.Value);
					//cfgSS.PositioningOptions.Scores.MinTrimForTrimScores = this.intParse(this.nud_MinTrim.Value);
					//cfgSS.PlcConfig.LoadTeflonMeters = this.intParse(this.nud_LoadTeflonMeters.Value);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.NonScoringScoreType = ScorePositOptions.NonScoringScores.NoScore;
					if (this.ttl_NonScoringType.SelectedItem != string.Empty)
						cfgSS.SlitterScorerHeader.PositioningOptions.Scores.NonScoringScoreType = (ScorePositOptions.NonScoringScores)Enum.Parse(typeof(ScorePositOptions.NonScoringScores), this.ttl_NonScoringType.SelectedItem, true);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.TrimScoreDistance = this.intParse(this.nud_TrimScoresDistance.Value);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.MinTrimForTrimScores = this.intParse(this.nud_MinTrim.Value);
					cfgSS.Twin400.PlcConfig.LoadTeflonMeters = this.intParse(this.nud_LoadTeflonMeters.Value);

					//RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
					cfgSS.SlitterScorerHeader.OffsetTrimChute = this.intParse(this.nud_TrimChutOffset.Value);

          //AGrassi_FI06/12/2007_1
					//RFAZZI PROVA
					//cfgSS.PositioningOptions.Slits.TeflonCycleMeters = this.intParse(this.nud_TeflonCycleMeters.Value);
					cfgSS.SlitterScorerHeader.PositioningOptions.Slits.TeflonCycleMeters = this.intParse(this.nud_TeflonCycleMeters.Value);

					//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
					cfgSS.SlitterScorerHeader.PositioningOptions.Slits.GrindMetersFromSubstitutionCost = this.scrolBarSlitConsumtionOpt.Maximum - this.scrolBarSlitConsumtionOpt.Value;
					cfgSS.SlitterScorerHeader.PositioningOptions.Slits.GrindMetersFromRegulationCost = this.scrolBarSlitConsumtionOpt.Value;

          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          string cfgCommand = PageManager.getRequestCommand(typeof(Frm_twin400_regulations)) + "." +
                      Command.CONF_CONFIG + "(" + cfgSS.SerializeProperties() + ")";
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER, cfgCommand);
        }
        if (this.modWeb)
        {
          DataBase.LineConfig.WebDirector cfgWD = (DataBase.LineConfig.WebDirector)DBManager.getDBManager().lineConfig.WebDirectorConfig.CreateCopy();
          cfgWD.Offset = this.intParse(this.nud_OffsetWebDirector.Value);
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          string cfgCommand = Command.FRM_WEBDIRECTOR_MACHINESETTINGS + "." +
                      Command.CONF_CONFIG + "(" + cfgWD.SerializeProperties() + ")";
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER, cfgCommand);
        }
        if (this.modRS)
        {
          DataBase.LineConfig.Rotary cfgRS = (DataBase.LineConfig.Rotary)DBManager.getDBManager().lineConfig.RotaryConfig.CreateCopy();
          cfgRS.Offset = this.intParse(this.nud_OffsetRotary.Value);
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          string cfgCommand = Command.FRM_ROTARY_MACHINESETTINGS + "." +
                      Command.CONF_CONFIG + "(" + cfgRS.SerializeProperties() + ")";
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER, cfgCommand);
        }
      }
      this.refreshData();
      this.modTwin = false;
      this.modWeb = false;
      this.modRS = false;
      this.mod = false;
      this.SetReadOnly(true);
    }

    protected override void Escape()
    {
      this.modTwin = false;
      this.modWeb = false;
      this.modRS = false;
      this.mod = false;
      this.SetReadOnly(true);
    }

    public override void refreshData()
    {
      //agrassi_FI14-01-2010_2
      if (this.mod) return;
      DBManager localDBM = DBManager.getDBManager();
      this.nud_OffsetRotary.Visible = SyncroC.Utility.Settings.getSettings().configuration.IsPmac400();
      DataBase.LineConfig.SlitterScorer cfgSS = localDBM.lineConfig.SlitterScorerConfig;
      DataBase.LineConfig.WebDirector cfgWD = localDBM.lineConfig.WebDirectorConfig;
      DataBase.LineConfig.Rotary cfgRS = localDBM.lineConfig.RotaryConfig;
      //this.nud_OffsetSlit1.Value = cfgSS.SlitShafts[0].RobotOffset.ToString();
      //this.nud_OffsetSlit2.Value = cfgSS.SlitShafts[1].RobotOffset.ToString();
			this.nud_OffsetScore1.Value = cfgSS.SlitterScorerHeader.ScoreShafts[0].RobotOffset.ToString();
			this.nud_OffsetScore2.Value = cfgSS.SlitterScorerHeader.ScoreShafts[1].RobotOffset.ToString();
			this.nud_OffsetScore3.Value = cfgSS.SlitterScorerHeader.ScoreShafts[2].RobotOffset.ToString();
			//RFAZZI PROVA
			//this.nud_OffsetSlitsScores.Value = cfgSS.PositioningOptions.Scores.ScoreSlitOffset.ToString();
			this.nud_OffsetSlitsScores.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Scores.ScoreSlitOffset.ToString();
			
			this.nud_OffsetWebDirector.Value = cfgWD.Offset.ToString();
      this.nud_OffsetRotary.Value = cfgRS.Offset.ToString();
			this.nud_GapOffsetScore1.Value = cfgSS.SlitterScorerHeader.ScoreShafts[0].ScoreGapOffset.ToString();
			this.nud_GapOffsetScore2.Value = cfgSS.SlitterScorerHeader.ScoreShafts[1].ScoreGapOffset.ToString();
			this.nud_GapOffsetScore3.Value = cfgSS.SlitterScorerHeader.ScoreShafts[2].ScoreGapOffset.ToString();


			//RFAZZI PROVA
			//this.ttl_NonScoringType.SelectedItem = Enum.Format(typeof(ScorePositOptions.NonScoringScores), cfgSS.PositioningOptions.Scores.NonScoringScoreType, "g");
			//this.nud_TrimScoresDistance.Value = cfgSS.PositioningOptions.Scores.TrimScoreDistance.ToString();
			//this.nud_MinTrim.Value = cfgSS.PositioningOptions.Scores.MinTrimForTrimScores.ToString();
			//this.nud_LoadTeflonMeters.Value = cfgSS.PlcConfig.LoadTeflonMeters.ToString();
			////RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
			//this.nud_TrimChutOffset.Value = cfgSS.OffsetTrimChute.ToString();

			////AGrassi_FI06/12/2007_1
			//this.nud_TeflonCycleMeters.Value= cfgSS.PositioningOptions.Slits.TeflonCycleMeters.ToString();
      //this.ChangeDefaultShaft.Text = "Current on Shaft " + (localDBM.lineConfig.SlitterScorerConfig.DefCurrentGroup + 1).ToString();
			this.ttl_NonScoringType.SelectedItem = Enum.Format(typeof(ScorePositOptions.NonScoringScores), cfgSS.SlitterScorerHeader.PositioningOptions.Scores.NonScoringScoreType, "g");
			this.nud_TrimScoresDistance.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Scores.TrimScoreDistance.ToString();
			this.nud_MinTrim.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Scores.MinTrimForTrimScores.ToString();
			this.nud_LoadTeflonMeters.Value = cfgSS.Twin400.PlcConfig.LoadTeflonMeters.ToString();
			//RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
			this.nud_TrimChutOffset.Value = cfgSS.SlitterScorerHeader.OffsetTrimChute.ToString();

			//AGrassi_FI06/12/2007_1
			this.nud_TeflonCycleMeters.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Slits.TeflonCycleMeters.ToString();
			this.ChangeDefaultShaft.Text = "Current on Shaft " + ((int)localDBM.lineConfig.SlitterScorerConfig.Twin400.DefCurrentGroup + 1).ToString();

			//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
			this.scrolBarSlitConsumtionOpt.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Slits.GrindMetersFromRegulationCost;


      this.SetupListChange(null, null);
    }

    private void SetupListChange(object sender, DataBase.EventArgs.DBChangedEventArgs e)
    {
      DBManager localDBM = DBManager.getDBManager();
      this.ChangeDefaultShaft.Enabled = (localDBM.order.order.Tables[Order.DRYEND_LIST].Rows.Count == 0);
    }

    private void nud_Twin_ValueChanged(object sender, EventArgs e)
    {
			//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
			DBManager localDBM = DBManager.getDBManager();
			DataBase.LineConfig.SlitterScorer cfgSS = localDBM.lineConfig.SlitterScorerConfig;
			if (!this.mod)
				this.scrolBarSlitConsumtionOpt.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Slits.GrindMetersFromRegulationCost;

      this.modTwin = this.mod;
    }

    private void nud_WedDirector_ValueChanged(object sender, EventArgs e)
    {
      this.modWeb = this.mod;
    }

    private void nud_Rotary_ValueChanged(object sender, EventArgs e)
    {
      this.modRS = this.mod;
    }

    //agrassi_FI16-12-08_1
    //protected override bool IncludeComponentsIntoLanguageDB()
    //{
    //  return false;
    //}

    private void _SetPLCComm(bool val)
    {
      this.PLCCommOK = val;
    }
  }
}

