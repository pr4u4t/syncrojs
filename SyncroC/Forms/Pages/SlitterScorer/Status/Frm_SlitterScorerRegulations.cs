using System;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Forms.PopUp;
using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.SlitterScorer.Status
{
	public partial class Frm_SlitterScorerRegulations : SyncroC.Forms.Pages.SlitterScorer.Status.Frm_SlitterScorerStatusSubgroup 
  {
    private bool mod=false, modTwin = false, modWeb = false;

		public Frm_SlitterScorerRegulations()
    {
      InitializeComponent();
      string[] nonScoringTypes = System.Enum.GetNames(typeof(ScorePositOptions.NonScoringScores));
      for (int i = 0; i < nonScoringTypes.Length; i++)
        this.ttl_NonScoringType.AddItem(nonScoringTypes[i]);
      DBManager.getDBManager().order.OnDBOrderChange += new Order.DBChangedHandler(this.SetupListChange);
      this.refreshData();
      this.SetReadOnly(true);
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
      }
    }

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
        //PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "(-1)");
				PageManager.getRequestCommand(typeof(Frm_SlitterScorerRegulations)) + "." + ((Control)sender).Name + "(-1)");
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
        //PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "()");
        PageManager.getRequestCommand(typeof(Frm_SlitterScorerRegulations)) + "." + ((Control)sender).Name + "()");
    }

    private void SetReadOnly(bool readOnly)
    {
			this.nud_OffsetScore1.ReadOnly = this.nud_OffsetScore2.ReadOnly =
			this.nud_OffsetScore3.ReadOnly = this.nud_OffsetWebDirector.ReadOnly =
			this.nud_GapOffsetScore1.ReadOnly = this.nud_GapOffsetScore2.ReadOnly =
			this.nud_GapOffsetScore3.ReadOnly = this.ttl_NonScoringType.ReadOnly =
			this.nud_TrimScoresDistance.ReadOnly = this.nud_MinTrim.ReadOnly =
			this.nud_TrimChutOffset.ReadOnly = readOnly;
      this.nud_OffsetScore1.Enabled = this.nud_OffsetScore2.Enabled =
      this.nud_OffsetScore3.Enabled = this.nud_OffsetWebDirector.Enabled = 
      this.nud_GapOffsetScore1.Enabled = this.nud_GapOffsetScore2.Enabled =
      this.nud_GapOffsetScore3.Enabled = this.ttl_NonScoringType.Enabled =
      this.nud_TrimScoresDistance.Enabled = this.nud_MinTrim.Enabled =
			this.nud_TrimChutOffset.Enabled = !readOnly;
      if (!readOnly) this.SetTrimScoresEnabling();
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
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      this.mod = true;
      this.SetReadOnly(false);
      this.nud_OffsetScore1.Focus();
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
					cfgSS.SlitterScorerHeader.ScoreShafts[0].RobotOffset = this.intParse(this.nud_OffsetScore1.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[1].RobotOffset = this.intParse(this.nud_OffsetScore2.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[2].RobotOffset = this.intParse(this.nud_OffsetScore3.Value);

					cfgSS.SlitterScorerHeader.ScoreShafts[0].ScoreGapOffset = this.intParse(this.nud_GapOffsetScore1.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[1].ScoreGapOffset = this.intParse(this.nud_GapOffsetScore2.Value);
					cfgSS.SlitterScorerHeader.ScoreShafts[2].ScoreGapOffset = this.intParse(this.nud_GapOffsetScore3.Value);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.NonScoringScoreType = ScorePositOptions.NonScoringScores.NoScore;
					if (this.ttl_NonScoringType.SelectedItem != string.Empty)
						cfgSS.SlitterScorerHeader.PositioningOptions.Scores.NonScoringScoreType = (ScorePositOptions.NonScoringScores)Enum.Parse(typeof(ScorePositOptions.NonScoringScores), this.ttl_NonScoringType.SelectedItem, true);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.TrimScoreDistance = this.intParse(this.nud_TrimScoresDistance.Value);
					cfgSS.SlitterScorerHeader.PositioningOptions.Scores.MinTrimForTrimScores = this.intParse(this.nud_MinTrim.Value);

					cfgSS.SlitterScorerHeader.OffsetTrimChute = this.intParse(this.nud_TrimChutOffset.Value);


          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          string cfgCommand = PageManager.getRequestCommand(typeof(Frm_SlitterScorerRegulations)) + "." +
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
      }
      this.refreshData();
      this.modTwin = false;
      this.modWeb = false;
      this.mod = false;
      this.SetReadOnly(true);
    }

    protected override void Escape()
    {
      this.modTwin = false;
      this.modWeb = false;
      this.mod = false;
      this.SetReadOnly(true);
    }

    public override void refreshData()
    {
      DBManager localDBM = DBManager.getDBManager();
      DataBase.LineConfig.SlitterScorer cfgSS = localDBM.lineConfig.SlitterScorerConfig;
      DataBase.LineConfig.WebDirector cfgWD = localDBM.lineConfig.WebDirectorConfig;
      DataBase.LineConfig.Rotary cfgRS = localDBM.lineConfig.RotaryConfig;
			this.nud_OffsetScore1.Value = cfgSS.SlitterScorerHeader.ScoreShafts[0].RobotOffset.ToString();
			this.nud_OffsetScore2.Value = cfgSS.SlitterScorerHeader.ScoreShafts[1].RobotOffset.ToString();
			this.nud_OffsetScore3.Value = cfgSS.SlitterScorerHeader.ScoreShafts[2].RobotOffset.ToString();
			
			this.nud_OffsetWebDirector.Value = cfgWD.Offset.ToString();
			//this.nud_GapOffsetScore1.Value = cfgSS.SlitterScorerHeader.ScoreShafts[0].ScoreGapOffset.ToString();
			//this.nud_GapOffsetScore2.Value = cfgSS.SlitterScorerHeader.ScoreShafts[1].ScoreGapOffset.ToString();
			//this.nud_GapOffsetScore3.Value = cfgSS.SlitterScorerHeader.ScoreShafts[2].ScoreGapOffset.ToString();

			this.ttl_NonScoringType.SelectedItem = Enum.Format(typeof(ScorePositOptions.NonScoringScores), cfgSS.SlitterScorerHeader.PositioningOptions.Scores.NonScoringScoreType, "g");
			//this.nud_TrimScoresDistance.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Scores.TrimScoreDistance.ToString();
			//this.nud_MinTrim.Value = cfgSS.SlitterScorerHeader.PositioningOptions.Scores.MinTrimForTrimScores.ToString();
			this.nud_TrimChutOffset.Value = cfgSS.SlitterScorerHeader.OffsetTrimChute.ToString();

      this.SetupListChange(null, null);
    }

    private void SetupListChange(object sender, DataBase.EventArgs.DBChangedEventArgs e)
    {
    }

    private void nud_Twin_ValueChanged(object sender, EventArgs e)
    {
      this.modTwin = this.mod;
    }

    private void nud_WedDirector_ValueChanged(object sender, EventArgs e)
    {
      this.modWeb = this.mod;
    }

    protected override bool IncludeComponentsIntoLanguageDB()
    {
      return false;
    }

  }
}

