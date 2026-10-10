using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Forms.Pages.Production.OrdersIns;

namespace SyncroC.Forms.StatusBar
{
	public class Frm_StatusBarWetEnd : SyncroC.Forms.StatusBar.Frm_StatusBar
	{
		private SyncroC.Controls.StateButton WetEnd;
		private SyncroC.Controls.StateButton DoubleBacker;
		private SyncroC.Controls.StateButton GlueUnit;
		private SyncroC.Controls.StateButton Splicer1;
		private SyncroC.Controls.StateButton Splicer2;
		private SyncroC.Controls.StateButton SingleFacer1;
		private SyncroC.Controls.StateButton Splicer3;
		private SyncroC.Controls.StateButton Splicer4;
		private SyncroC.Controls.StateButton SingleFacer2;
		private SyncroC.Controls.StateButton Splicer5;
		public SyncroC.Controls.ProgBar prb_WetEnd;
		private SyncroC.Controls.Title ttl_DryEndTitle;
		public Label lbl_WetEndMode;
		private SyncroC.Controls.TitledLabel tlb_WetEndMetersLeft;
		private SyncroC.Controls.TitledMaskTextBox tmt_AOCTimeLeft;
		private SyncroC.Controls.TitledLabel tlb_WetEndProgram;
		private SyncroC.Controls.GridView gdv_Programs;
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    private SyncroC.Controls.GridView gdv_Programs3SF;
    private System.ComponentModel.IContainer components = null;

		private DataGridViewColumn clm_Prog_ProgramQuality;
		private DataGridViewColumn clm_Prog_ProgramCode;
		private DataGridViewColumn clm_Prog_PaperWidth;
		private DataGridViewColumn clm_Prog_ProgramLength;
		private PictureBox pct_DBNotRdyRun;
		private PictureBox pct_GUNotRdyRun;
		private PictureBox pct_SPL1NotRdyRun;
		private PictureBox pct_SPL2NotRdyRun;
		private PictureBox pct_SF1NotRdyRun;
		private PictureBox pct_SPL3NotRdyRun;
		private PictureBox pct_SPL4NotRdyRun;
		private PictureBox pct_SF2NotRdyRun;
		private PictureBox pct_SPL5NotRdyRun;
		private SyncroC.Controls.StateButton ToggleAutoManZone2;
		private SyncroC.Controls.StateButton ToggleAutoManZone1;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSPL5;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSF2;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSPL4;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSPL3;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSF1;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSPL2;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSPL1;
		private SyncroC.Controls.StateButton btn_IncDecSpeedGU;
		private SyncroC.Controls.StateButton btn_IncDecSpeedDB;
		private SyncroC.Controls.StateButton btn_IncDecSpeedWEConsole;
		private SyncroC.Controls.StateButton btn_IncDecSpeedPHT;
		private PictureBox pct_PHTNotRdyRun;
		private SyncroC.Controls.StateButton PreHeaterTern;
		private SyncroC.Controls.Title ttl_WetEndTitle;
		private Panel pnl_clickExtension;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSS;
		private SyncroC.Controls.StateButton ToggleAutoManWetEnd;
    private PictureBox pct_Bridge2;
		private PictureBox pct_Bridge1;
		private SyncroC.Controls.TitledNumericUpDown nud_SpeedLine;
    private SyncroC.Controls.TitledNumericUpDown nud_SpeedZone1;
		private SyncroC.Controls.TitledNumericUpDown nud_S1Meters;
		private SyncroC.Controls.TitledNumericUpDown nud_S2Meters;
		private SyncroC.Controls.TitledNumericUpDown nud_S3Meters;
		private SyncroC.Controls.TitledNumericUpDown nud_S4Meters;
		private SyncroC.Controls.TitledNumericUpDown nud_S5Meters;
		private Label lbl_zone2Active;
		private Label lbl_zone1Active;
		private Label lbl_zoneDBActive;
		private Label lbl_SF2Cartridge;
		private Label lbl_SF1Cartridge;
		public SyncroC.Controls.ProgBar prb_BridgeContents2;
		public SyncroC.Controls.ProgBar prb_BridgeContents1;
		private Panel pnl_clickExtension2;
		private SyncroC.Controls.StateButton AutoManS5;
		private SyncroC.Controls.StateButton AutoManS4;
		private SyncroC.Controls.StateButton AutoManS3;
		private SyncroC.Controls.StateButton AutoManS2;
		private SyncroC.Controls.StateButton AutoManS1;
		private SyncroC.Controls.StateButton btn_SYCIncDecSpeedSF2;
		private SyncroC.Controls.StateButton btn_SYCIncDecBandSpeedSF2;
		private SyncroC.Controls.StateButton btn_SYCIncDecBandSpeedSF1;
		private SyncroC.Controls.StateButton btn_SYCIncDecSpeedSF1;
		private SyncroC.Controls.StateButton btn_SYCIncDecBandSpeedDB;
		private SyncroC.Controls.StateButton btn_SYCIncDecSpeedDB;
		private Label lbl_BridgeContents2;
		private Label lbl_BridgeContents1;
		public Label lbl_WECType_YY;
		private PictureBox pct_BridgeError1;
		private PictureBox pct_BridgeError2;
		private PictureBox pct_WETAOCStatus;
		private ImageList iml_WETAOCStatus;
    private SyncroC.Controls.Title ttl_SimulationTitle;
    private Label lbl_Splicer5Roll_B_Active;
    private Label lbl_Splicer5Roll_A_Active;
    private Label lbl_Splicer4Roll_B_Active;
    private Label lbl_Splicer4Roll_A_Active;
    private Label lbl_Splicer3Roll_A_Active;
    private Label lbl_Splicer3Roll_B_Active;
    private Label lbl_Splicer2Roll_B_Active;
    private Label lbl_Splicer2Roll_A_Active;
    private Label lbl_Splicer1Roll_A_Active;
    private Label lbl_Splicer1Roll_B_Active;
    private PictureBox pct_notReadyToSplice_5;
    private PictureBox pct_notReadyToSplice_4;
    private PictureBox pct_notReadyToSplice_3;
    private PictureBox pct_notReadyToSplice_2;
    private PictureBox pct_notReadyToSplice_1;
    private SyncroC.Controls.GroupPanel.GroupPanel grp_now_percheMiSonoFermato;
    private PictureBox pct_now;
    private Label lbl_now_numeroMacchina_percheMiSonoFermato;
    private PictureBox pct_now_causa_percheMiSonoFermato;
    private SyncroC.Controls.StateButton Splicer6;
    private SyncroC.Controls.StateButton btn_IncDecSpeedSPL6;
    private Label lbl_Splicer6Roll_B_Active;
    private Label lbl_Splicer6Roll_A_Active;
    private PictureBox pct_notReadyToSplice_6;
    private SyncroC.Controls.TitledNumericUpDown nud_S6Meters;
    private SyncroC.Controls.StateButton AutoManS6;
    private PictureBox pct_SPL6NotRdyRun;
    private SyncroC.Controls.StateButton SingleFacer3;
    private Label lbl_SF3Cartridge;
    private SyncroC.Controls.StateButton btn_IncDecSpeedSF3;
    private PictureBox pct_SF3NotRdyRun;
    private SyncroC.Controls.TitledNumericUpDown nud_S7Meters;
    private SyncroC.Controls.StateButton Splicer7;
    private Label lbl_Splicer7Roll_A_Active;
    private Label lbl_Splicer7Roll_B_Active;
    private SyncroC.Controls.StateButton btn_IncDecSpeedSPL7;
    private PictureBox pct_notReadyToSplice_7;
    private SyncroC.Controls.StateButton AutoManS7;
    private PictureBox pct_SPL7NotRdyRun;
    private PictureBox pct_Bridge3;
    private SyncroC.Controls.StateButton btn_SYCIncDecSpeedSF3;
    private SyncroC.Controls.StateButton btn_SYCIncDecBandSpeedSF3;
    private PictureBox pct_BridgeError3;
    private Label lbl_zone3Active;
    private SyncroC.Controls.StateButton ToggleAutoManZone3;
    public SyncroC.Controls.ProgBar prb_BridgeContents3;
    private Label lbl_BridgeContents3;
    private SyncroC.Controls.TitledNumericUpDown nud_SpeedZone2;
    private SyncroC.Controls.TitledNumericUpDown nud_SpeedZone3;

		private DataSet order;

		public Frm_StatusBarWetEnd()
		{
			InitializeComponent();

			order = DBManager.getDBManager().order.order;

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (SyncroC.DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        this.nud_S4Meters.Visible = true;
        this.nud_S5Meters.Visible = true;
      }

      if (!SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3)
      {

        this.gdv_Programs.DataSource = order.Tables[Order.WETEND_LIST];
        this.gdv_Programs3SF.Visible = false;
        this.gdv_Programs.Visible = true;

        //
        //colonne che avrebbe dovuto fare il designer.
        //

        //gdv_Programs 

        clm_Prog_ProgramCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Prog_ProgramCode.Name = Order.WEL_PROGRAM_CODE;
        clm_Prog_ProgramCode.HeaderText = "Code";
        clm_Prog_ProgramCode.DataPropertyName = "ProgramCode";
        clm_Prog_ProgramCode.Width = 180;

        clm_Prog_PaperWidth = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Prog_PaperWidth.Name = Order.WEL_PAPER_WIDTH;
        clm_Prog_PaperWidth.HeaderText = "Width";
        clm_Prog_PaperWidth.DataPropertyName = "PaperWidth";
        clm_Prog_PaperWidth.Width = 90;

        clm_Prog_ProgramLength = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Prog_ProgramLength.Name = Order.WEL_PROGRAM_LENGTH;
        clm_Prog_ProgramLength.HeaderText = "Length";
        clm_Prog_ProgramLength.DataPropertyName = "ProgramLength";
        clm_Prog_ProgramLength.Width = 100;

        clm_Prog_ProgramQuality = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Prog_ProgramQuality.Name = Order.WEL_PROGRAM_QUALITY;
        clm_Prog_ProgramQuality.HeaderText = "Quality";
        clm_Prog_ProgramQuality.DataPropertyName = "ProgramQuality";
        clm_Prog_ProgramQuality.Width = 600; 
        
        gdv_Programs.Columns.Add(clm_Prog_ProgramCode);
        gdv_Programs.Columns.Add(clm_Prog_PaperWidth);
        gdv_Programs.Columns.Add(clm_Prog_ProgramLength);
        gdv_Programs.Columns.Add(clm_Prog_ProgramQuality);

        tmt_AOCTimeLeft.DataBindings.Add(
			    new Binding("BackColor", tlb_WetEndMetersLeft, "BackColor"));

      }
      else 
      {

        this.gdv_Programs3SF.DataSource = order.Tables[Order.WETEND_LIST];
        this.gdv_Programs3SF.Visible = true;
        this.gdv_Programs.Visible = false;

        clm_Prog_ProgramQuality = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Prog_ProgramQuality.Name = Order.WEL_PROGRAM_QUALITY;
        clm_Prog_ProgramQuality.HeaderText = "Quality";
        clm_Prog_ProgramQuality.DataPropertyName = "ProgramQuality";
        clm_Prog_ProgramQuality.Width = 370;
        
        gdv_Programs3SF.Columns.Add(clm_Prog_ProgramQuality);

        this.tmt_AOCTimeLeft.Visible = false;
        this.tlb_WetEndProgram.Visible = false;
        this.tlb_WetEndMetersLeft.Location = new Point(this.gdv_Programs.Location.X,this.tlb_WetEndMetersLeft.Location.Y);
        this.WetEnd.Location = new Point(this.tlb_WetEndMetersLeft.Location.X + this.tlb_WetEndMetersLeft.Width + 22, this.WetEnd.Location.Y);
        this.btn_IncDecSpeedWEConsole.Location = new Point(this.WetEnd.Location.X, this.btn_IncDecSpeedWEConsole.Location.Y);
        this.grp_now_percheMiSonoFermato.Width = this.WetEnd.Location.X + this.WetEnd.Width - this.tlb_WetEndMetersLeft.Location.X;

        this.Splicer6.Visible = true;
        this.AutoManS6.Visible = true;
        this.nud_S6Meters.Visible = true;

        this.Splicer7.Visible = true;
        this.AutoManS7.Visible = true;
        this.nud_S7Meters.Visible = true;

        this.SingleFacer3.Visible = true;
        this.lbl_SF3Cartridge.Visible = true;
        this.pct_SF3NotRdyRun.Visible = true;
        
        this.ToggleAutoManZone3.Visible = true;

        this.nud_SpeedZone3.Visible = true;

        this.lbl_BridgeContents3.Visible = true;
        this.pct_Bridge3.Visible = true;
        this.prb_BridgeContents3.Visible = true;
      }


		  //register to page change
		  if (!this.DesignMode)
			  PageManager.getPageManager().pagesHistorian.OnPageChanged += new SyncroC.PagesHistorian.PageChangedHandler(OnPageChanged);

		  if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
		  {
			  this.ToggleAutoManWetEnd.IsButton = false;
			  this.ToggleAutoManZone1.IsButton = false;
			  this.ToggleAutoManZone2.IsButton = false;
        //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
        this.ToggleAutoManZone3.IsButton = false;
        this.pct_WETAOCStatus.Cursor = Cursors.Arrow;
        this.pnl_clickExtension2.Cursor = Cursors.Arrow;
		  }
		}

    //RFAZZI_FI20/05/2010_1. Aggiunta visualizzazione in barra di stato Wet della macchina che ha fermato la linea. Invertiti anche i campi di Metri e Tempo a Cambio Wet. 
    private void _setFirstReasonOfStop(string timeOfStop, string reasonFirstOfStop)
    {

      string[] appo = reasonFirstOfStop.Split(new char[] { '-' });
      string macchina = (appo.Length >= 1) ? appo[0].Trim() : "";
      string tipodistop = (appo.Length >= 2) ? appo[1].Trim() : "";

      if (macchina == "")
      {
        this.grp_now_percheMiSonoFermato.Visible = false;

        return;
      }
      else
      {
        this.grp_now_percheMiSonoFermato.GroupTitle = SyncroC.Utility.SyncroConverter.ConvertDateTimeWithMillisecondsToStrForDataGridView(SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(timeOfStop).ToLocalTime());

        this.grp_now_percheMiSonoFermato.Visible = true;

        switch (tipodistop)
        {
          case "NS":
            this.pct_now_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.OStop;
            break;
          case "ES":
            this.pct_now_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.NStop;
            break;
          case "NOT_READY_TO_RUN":
            this.pct_now_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.Not_Ready_To_Run;
            break;
          case "FUNGO":
            this.pct_now_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.fungo;
            break;
        }

        switch (macchina)
        {
          case "STK1":
            this.pct_now.Image = SyncroC.Properties.Resources.Terminal;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "STK2":
            this.pct_now.Image = SyncroC.Properties.Resources.Terminal;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "STK3":
            this.pct_now.Image = SyncroC.Properties.Resources.Terminal;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "KN1":
            this.pct_now.Image = SyncroC.Properties.Resources.Master;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "KN2":
            this.pct_now.Image = SyncroC.Properties.Resources.Master;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "KN3":
            this.pct_now.Image = SyncroC.Properties.Resources.Master;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SS":
            this.pct_now.Image = SyncroC.Properties.Resources.twin;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "RSH":
            this.pct_now.Image = SyncroC.Properties.Resources.Rotary;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "DB":
            this.pct_now.Image = SyncroC.Properties.Resources.Piani;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "SYC":
            this.pct_now.Image = SyncroC.Properties.Resources.wet_dry;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "PHT":
            this.pct_now.Image = SyncroC.Properties.Resources.Terna;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "GU":
            this.pct_now.Image = SyncroC.Properties.Resources.Glue_unit;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "SPL1":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL2":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL3":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL4":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "4";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL5":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "5";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL6":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "6";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL7":
            this.pct_now.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "7";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SF1":
            this.pct_now.Image = SyncroC.Properties.Resources.SingleFacer1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SF2":
            this.pct_now.Image = SyncroC.Properties.Resources.SingleFacer1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SF3":
            this.pct_now.Image = SyncroC.Properties.Resources.SingleFacer1;
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          default:
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_now_numeroMacchina_percheMiSonoFermato.Visible = false;
            this.grp_now_percheMiSonoFermato.BackgroundGradientColor = Color.Green;
            break;
        }
      }
    }

    public void _ShowStatusBarWetEnd(bool DBcontrolled, bool GUcontrolled, bool PHTcontrolled, bool SF1controlled, bool SF2controlled, bool SF3controlled, bool SPL1controlled, bool SPL2controlled, bool SPL3controlled, bool SPL4controlled, bool SPL5controlled, bool SPL6controlled, bool SPL7controlled, bool SPL12controlled, bool SPL14controlled, bool SPL16controlled)
    {
      this._Show();

      this.DoubleBacker.Enabled = DBcontrolled;
      this.GlueUnit.Enabled = GUcontrolled;
      this.PreHeaterTern.Enabled = PHTcontrolled;
      this.SingleFacer1.Enabled = SF1controlled;
      this.SingleFacer2.Enabled = SF2controlled;
      this.Splicer1.Enabled = SPL1controlled;
      this.Splicer2.Enabled = SPL2controlled;
      this.Splicer3.Enabled = SPL3controlled;
      this.Splicer4.Enabled = SPL4controlled;
      this.Splicer5.Enabled = SPL5controlled;
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      this.Splicer6.Enabled = SPL6controlled;
      this.Splicer7.Enabled = SPL7controlled;

      //ABELLO_FI11/02/15_2 Gestita un pò meglio la configurazione con un solo SingleFacer
      if (!SF1controlled)
      {
        this.ToggleAutoManZone1.Enabled = false;
        this.lbl_zone1Active.Visible = false;
        this.pct_Bridge1.Image = SyncroImage.ConvertToBlacknWhite(this.pct_Bridge1.Image);
      }

      if (!SF2controlled)
      {
        this.ToggleAutoManZone2.Enabled = false;
        this.lbl_zone2Active.Visible = false;
        this.pct_Bridge2.Image = SyncroImage.ConvertToBlacknWhite(this.pct_Bridge2.Image);
      }

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (!SF3controlled)
      {
        this.ToggleAutoManZone3.Enabled = false;
        this.lbl_zone3Active.Visible = false;
        this.pct_Bridge3.Image = SyncroImage.ConvertToBlacknWhite(this.pct_Bridge3.Image);
      }

      //TODO: Dual Arch
    }

		public override void ConfigPage(Settings.Configuration config)
		{
			base.ConfigPage(config);
			if (! Settings.getSettings().configuration.DryEndControl)
				ttl_DryEndTitle.ForeColor = ttl_DryEndTitle.BackColor;
		}

    //PMASSARO - MANANGEMENT OF ACTIVE ROLL FOR SPLICERS
    private void _setSplicerRollActive(bool splicer1Roll_A_Active, bool splicer2Roll_A_Active, bool splicer3Roll_A_Active, bool splicer4Roll_A_Active, bool splicer5Roll_A_Active, bool splicer6Roll_A_Active, bool splicer7Roll_A_Active)
    {
      lbl_Splicer1Roll_A_Active.Visible = splicer1Roll_A_Active;
      lbl_Splicer1Roll_B_Active.Visible = !splicer1Roll_A_Active;

      lbl_Splicer2Roll_A_Active.Visible = splicer2Roll_A_Active;
      lbl_Splicer2Roll_B_Active.Visible = !splicer2Roll_A_Active;
      lbl_Splicer3Roll_A_Active.Visible = splicer3Roll_A_Active;
      lbl_Splicer3Roll_B_Active.Visible = !splicer3Roll_A_Active;
      if (DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        lbl_Splicer4Roll_A_Active.Visible = splicer4Roll_A_Active;
        lbl_Splicer4Roll_B_Active.Visible = !splicer4Roll_A_Active;
        lbl_Splicer5Roll_A_Active.Visible = splicer5Roll_A_Active;
        lbl_Splicer5Roll_B_Active.Visible = !splicer5Roll_A_Active;
      };

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        lbl_Splicer6Roll_A_Active.Visible = splicer6Roll_A_Active;
        lbl_Splicer6Roll_B_Active.Visible = !splicer6Roll_A_Active;
        lbl_Splicer7Roll_A_Active.Visible = splicer7Roll_A_Active;
        lbl_Splicer7Roll_B_Active.Visible = !splicer7Roll_A_Active;
      };
      return;
    }

		private void _setWETStatus(string theWETAOCStatus)
		{
			if(theWETAOCStatus == "Manual")
				this.pct_WETAOCStatus.Image = this.iml_WETAOCStatus.Images[0];
			else if (theWETAOCStatus == "Auto") 
				this.pct_WETAOCStatus.Image = this.iml_WETAOCStatus.Images[1];
			else
				this.pct_WETAOCStatus.Image = this.iml_WETAOCStatus.Images[2];
		}

		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (!SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3)
        this.gdv_Programs.DataSource = null;
      else
        this.gdv_Programs3SF.DataSource = null;

			tmt_AOCTimeLeft.DataBindings.Clear();
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatusBarWetEnd));
      this.prb_WetEnd = new SyncroC.Controls.ProgBar();
      this.ttl_DryEndTitle = new SyncroC.Controls.Title();
      this.lbl_WetEndMode = new System.Windows.Forms.Label();
      this.tlb_WetEndMetersLeft = new SyncroC.Controls.TitledLabel();
      this.tmt_AOCTimeLeft = new SyncroC.Controls.TitledMaskTextBox();
      this.tlb_WetEndProgram = new SyncroC.Controls.TitledLabel();
      this.gdv_Programs = new SyncroC.Controls.GridView();
      this.gdv_Programs3SF = new SyncroC.Controls.GridView();
      this.ttl_WetEndTitle = new SyncroC.Controls.Title();
      this.pnl_clickExtension = new System.Windows.Forms.Panel();
      this.btn_IncDecSpeedSS = new SyncroC.Controls.StateButton();
      this.nud_SpeedLine = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SpeedZone1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SpeedZone2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_S1Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_S2Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_S3Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_S4Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_S5Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_zone2Active = new System.Windows.Forms.Label();
      this.lbl_zone1Active = new System.Windows.Forms.Label();
      this.lbl_zoneDBActive = new System.Windows.Forms.Label();
      this.lbl_SF2Cartridge = new System.Windows.Forms.Label();
      this.lbl_SF1Cartridge = new System.Windows.Forms.Label();
      this.prb_BridgeContents2 = new SyncroC.Controls.ProgBar();
      this.prb_BridgeContents1 = new SyncroC.Controls.ProgBar();
      this.pnl_clickExtension2 = new System.Windows.Forms.Panel();
      this.lbl_WECType_YY = new System.Windows.Forms.Label();
      this.lbl_BridgeContents2 = new System.Windows.Forms.Label();
      this.lbl_BridgeContents1 = new System.Windows.Forms.Label();
      this.iml_WETAOCStatus = new System.Windows.Forms.ImageList(this.components);
      this.ttl_SimulationTitle = new SyncroC.Controls.Title();
      this.lbl_Splicer5Roll_B_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer5Roll_A_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer4Roll_B_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer4Roll_A_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer3Roll_A_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer3Roll_B_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer2Roll_B_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer2Roll_A_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer1Roll_A_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer1Roll_B_Active = new System.Windows.Forms.Label();
      this.pct_notReadyToSplice_5 = new System.Windows.Forms.PictureBox();
      this.pct_WETAOCStatus = new System.Windows.Forms.PictureBox();
      this.pct_BridgeError2 = new System.Windows.Forms.PictureBox();
      this.pct_BridgeError1 = new System.Windows.Forms.PictureBox();
      this.btn_SYCIncDecBandSpeedDB = new SyncroC.Controls.StateButton();
      this.btn_SYCIncDecSpeedDB = new SyncroC.Controls.StateButton();
      this.btn_SYCIncDecBandSpeedSF1 = new SyncroC.Controls.StateButton();
      this.btn_SYCIncDecSpeedSF1 = new SyncroC.Controls.StateButton();
      this.btn_SYCIncDecSpeedSF2 = new SyncroC.Controls.StateButton();
      this.AutoManS1 = new SyncroC.Controls.StateButton();
      this.AutoManS2 = new SyncroC.Controls.StateButton();
      this.AutoManS3 = new SyncroC.Controls.StateButton();
      this.AutoManS4 = new SyncroC.Controls.StateButton();
      this.AutoManS5 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedPHT = new SyncroC.Controls.StateButton();
      this.pct_PHTNotRdyRun = new System.Windows.Forms.PictureBox();
      this.PreHeaterTern = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedWEConsole = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedDB = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedGU = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSPL1 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSPL2 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSF1 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSPL3 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSPL4 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSF2 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSPL5 = new SyncroC.Controls.StateButton();
      this.pct_SPL5NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_SF2NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_SPL4NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_SPL3NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_SF1NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_SPL2NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_SPL1NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_GUNotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_DBNotRdyRun = new System.Windows.Forms.PictureBox();
      this.WetEnd = new SyncroC.Controls.StateButton();
      this.DoubleBacker = new SyncroC.Controls.StateButton();
      this.GlueUnit = new SyncroC.Controls.StateButton();
      this.Splicer1 = new SyncroC.Controls.StateButton();
      this.Splicer2 = new SyncroC.Controls.StateButton();
      this.SingleFacer1 = new SyncroC.Controls.StateButton();
      this.Splicer3 = new SyncroC.Controls.StateButton();
      this.Splicer4 = new SyncroC.Controls.StateButton();
      this.SingleFacer2 = new SyncroC.Controls.StateButton();
      this.Splicer5 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManZone2 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManWetEnd = new SyncroC.Controls.StateButton();
      this.ToggleAutoManZone1 = new SyncroC.Controls.StateButton();
      this.pct_Bridge1 = new System.Windows.Forms.PictureBox();
      this.btn_SYCIncDecBandSpeedSF2 = new SyncroC.Controls.StateButton();
      this.pct_Bridge2 = new System.Windows.Forms.PictureBox();
      this.pct_notReadyToSplice_4 = new System.Windows.Forms.PictureBox();
      this.pct_notReadyToSplice_3 = new System.Windows.Forms.PictureBox();
      this.pct_notReadyToSplice_2 = new System.Windows.Forms.PictureBox();
      this.pct_notReadyToSplice_1 = new System.Windows.Forms.PictureBox();
      this.grp_now_percheMiSonoFermato = new SyncroC.Controls.GroupPanel.GroupPanel();
      this.pct_now = new System.Windows.Forms.PictureBox();
      this.lbl_now_numeroMacchina_percheMiSonoFermato = new System.Windows.Forms.Label();
      this.pct_now_causa_percheMiSonoFermato = new System.Windows.Forms.PictureBox();
      this.Splicer6 = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSPL6 = new SyncroC.Controls.StateButton();
      this.lbl_Splicer6Roll_B_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer6Roll_A_Active = new System.Windows.Forms.Label();
      this.pct_notReadyToSplice_6 = new System.Windows.Forms.PictureBox();
      this.nud_S6Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.AutoManS6 = new SyncroC.Controls.StateButton();
      this.pct_SPL6NotRdyRun = new System.Windows.Forms.PictureBox();
      this.SingleFacer3 = new SyncroC.Controls.StateButton();
      this.lbl_SF3Cartridge = new System.Windows.Forms.Label();
      this.btn_IncDecSpeedSF3 = new SyncroC.Controls.StateButton();
      this.pct_SF3NotRdyRun = new System.Windows.Forms.PictureBox();
      this.nud_S7Meters = new SyncroC.Controls.TitledNumericUpDown();
      this.Splicer7 = new SyncroC.Controls.StateButton();
      this.lbl_Splicer7Roll_A_Active = new System.Windows.Forms.Label();
      this.lbl_Splicer7Roll_B_Active = new System.Windows.Forms.Label();
      this.btn_IncDecSpeedSPL7 = new SyncroC.Controls.StateButton();
      this.pct_notReadyToSplice_7 = new System.Windows.Forms.PictureBox();
      this.AutoManS7 = new SyncroC.Controls.StateButton();
      this.pct_SPL7NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_Bridge3 = new System.Windows.Forms.PictureBox();
      this.btn_SYCIncDecSpeedSF3 = new SyncroC.Controls.StateButton();
      this.btn_SYCIncDecBandSpeedSF3 = new SyncroC.Controls.StateButton();
      this.pct_BridgeError3 = new System.Windows.Forms.PictureBox();
      this.lbl_zone3Active = new System.Windows.Forms.Label();
      this.ToggleAutoManZone3 = new SyncroC.Controls.StateButton();
      this.prb_BridgeContents3 = new SyncroC.Controls.ProgBar();
      this.lbl_BridgeContents3 = new System.Windows.Forms.Label();
      this.nud_SpeedZone3 = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_clickExtension.SuspendLayout();
      this.pnl_clickExtension2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_5)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_WETAOCStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BridgeError2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BridgeError1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_PHTNotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL5NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SF2NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL4NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL3NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SF1NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL2NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL1NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_GUNotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DBNotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Bridge1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Bridge2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_1)).BeginInit();
      this.grp_now_percheMiSonoFermato.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now_causa_percheMiSonoFermato)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_6)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL6NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SF3NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_7)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL7NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Bridge3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BridgeError3)).BeginInit();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // prb_WetEnd
      // 
      this.prb_WetEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.prb_WetEnd.BarStyle = SyncroC.Controls.BarStyles.Classic;
      this.prb_WetEnd.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.prb_WetEnd.Font = new System.Drawing.Font("Arial Unicode MS", 15F, System.Drawing.FontStyle.Bold);
      this.prb_WetEnd.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.prb_WetEnd.Location = new System.Drawing.Point(669, 25);
      this.prb_WetEnd.Name = "prb_WetEnd";
      this.prb_WetEnd.Percentage = 80;
      this.prb_WetEnd.PercentageVisible = true;
      this.prb_WetEnd.Size = new System.Drawing.Size(607, 24);
      this.prb_WetEnd.Style = SyncroC.Controls.ProgBarStyles.Big;
      this.prb_WetEnd.TabIndex = 232;
      this.prb_WetEnd.TranslateText = false;
      // 
      // ttl_DryEndTitle
      // 
      this.ttl_DryEndTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.ttl_DryEndTitle.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_DryEndTitle.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ttl_DryEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DryEndTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_DryEndTitle.Location = new System.Drawing.Point(4, 25);
      this.ttl_DryEndTitle.Name = "ttl_DryEndTitle";
      this.ttl_DryEndTitle.Size = new System.Drawing.Size(80, 243);
      this.ttl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_DryEndTitle.TabIndex = 228;
      this.ttl_DryEndTitle.Text = "DRY";
      this.ttl_DryEndTitle.TranslateText = false;
      this.ttl_DryEndTitle.Click += new System.EventHandler(this.ttl_DryEndTitle_Click);
      // 
      // lbl_WetEndMode
      // 
      this.lbl_WetEndMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.lbl_WetEndMode.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold);
      this.lbl_WetEndMode.ForeColor = System.Drawing.Color.Aqua;
      this.lbl_WetEndMode.Location = new System.Drawing.Point(167, 25);
      this.lbl_WetEndMode.Name = "lbl_WetEndMode";
      this.lbl_WetEndMode.Size = new System.Drawing.Size(203, 24);
      this.lbl_WetEndMode.TabIndex = 224;
      this.lbl_WetEndMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_WetEndMode.Click += new System.EventHandler(this.lbl_WetEndMode_Click);
      // 
      // tlb_WetEndMetersLeft
      // 
      this.tlb_WetEndMetersLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_WetEndMetersLeft.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_WetEndMetersLeft.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_WetEndMetersLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_WetEndMetersLeft.ImagePassword = null;
      this.tlb_WetEndMetersLeft.Location = new System.Drawing.Point(280, 185);
      this.tlb_WetEndMetersLeft.Name = "tlb_WetEndMetersLeft";
      this.tlb_WetEndMetersLeft.Size = new System.Drawing.Size(143, 83);
      this.tlb_WetEndMetersLeft.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_WetEndMetersLeft.TabIndex = 308;
      this.tlb_WetEndMetersLeft.Text = "WET MT LEFT";
      this.tlb_WetEndMetersLeft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_WetEndMetersLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WetEndMetersLeft.TranslateText = true;
      this.tlb_WetEndMetersLeft.Value = "15000";
      this.tlb_WetEndMetersLeft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WetEndMetersLeft.ValueMultiline = true;
      this.tlb_WetEndMetersLeft.ValueChanged += new System.EventHandler(this.orderChangeTimeLeftCalculation);
      // 
      // tmt_AOCTimeLeft
      // 
      this.tmt_AOCTimeLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tmt_AOCTimeLeft.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_AOCTimeLeft.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_AOCTimeLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tmt_AOCTimeLeft.ImagePassword = null;
      this.tmt_AOCTimeLeft.Location = new System.Drawing.Point(121, 185);
      this.tmt_AOCTimeLeft.Name = "tmt_AOCTimeLeft";
      this.tmt_AOCTimeLeft.PasswordChar = '\0';
      this.tmt_AOCTimeLeft.ReadOnly = true;
      this.tmt_AOCTimeLeft.Size = new System.Drawing.Size(153, 83);
      this.tmt_AOCTimeLeft.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Framed;
      this.tmt_AOCTimeLeft.TabIndex = 310;
      this.tmt_AOCTimeLeft.Text = "TIME LEFT";
      this.tmt_AOCTimeLeft.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.Time;
      this.tmt_AOCTimeLeft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tmt_AOCTimeLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_AOCTimeLeft.TranslateText = true;
      this.tmt_AOCTimeLeft.Value = " ";
      this.tmt_AOCTimeLeft.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_AOCTimeLeft.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_AOCTimeLeft.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_AOCTimeLeft.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_AOCTimeLeft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tlb_WetEndProgram
      // 
      this.tlb_WetEndProgram.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_WetEndProgram.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_WetEndProgram.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_WetEndProgram.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_WetEndProgram.ImagePassword = null;
      this.tlb_WetEndProgram.Location = new System.Drawing.Point(13, 185);
      this.tlb_WetEndProgram.Name = "tlb_WetEndProgram";
      this.tlb_WetEndProgram.Size = new System.Drawing.Size(102, 83);
      this.tlb_WetEndProgram.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_WetEndProgram.TabIndex = 309;
      this.tlb_WetEndProgram.Text = "PROGRAM";
      this.tlb_WetEndProgram.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_WetEndProgram.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WetEndProgram.TranslateText = true;
      this.tlb_WetEndProgram.Value = "0";
      this.tlb_WetEndProgram.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WetEndProgram.ValueMultiline = true;
      // 
      // gdv_Programs
      // 
      this.gdv_Programs.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.AutoGenerateColumns = false;
      this.gdv_Programs.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Programs.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_Programs.DataMember = "";
      this.gdv_Programs.DataSource = null;
      this.gdv_Programs.Font = new System.Drawing.Font("Arial Unicode MS", 14F, System.Drawing.FontStyle.Bold);
      this.gdv_Programs.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.gdv_Programs.ImagePassword = null;
      this.gdv_Programs.Location = new System.Drawing.Point(14, 60);
      this.gdv_Programs.MultiSelect = false;
      this.gdv_Programs.Name = "gdv_Programs";
      this.gdv_Programs.RowsHeight = 24;
      this.gdv_Programs.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.gdv_Programs.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.Size = new System.Drawing.Size(635, 96);
      this.gdv_Programs.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Programs.TabIndex = 311;
      this.gdv_Programs.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Programs.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.TranslateText = true;
      this.gdv_Programs.DoubleClick += new System.EventHandler(this.gdv_Programs_DoubleClick);
      this.gdv_Programs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Programs_CellFormatting);
      // 
      // gdv_Programs3SF
      // 
      this.gdv_Programs3SF.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs3SF.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs3SF.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs3SF.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs3SF.AutoGenerateColumns = false;
      this.gdv_Programs3SF.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Programs3SF.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_Programs3SF.DataMember = "";
      this.gdv_Programs3SF.DataSource = null;
      this.gdv_Programs3SF.Font = new System.Drawing.Font("Arial Unicode MS", 14F, System.Drawing.FontStyle.Bold);
      this.gdv_Programs3SF.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.gdv_Programs3SF.ImagePassword = null;
      this.gdv_Programs3SF.Location = new System.Drawing.Point(14, 60);
      this.gdv_Programs3SF.MultiSelect = false;
      this.gdv_Programs3SF.Name = "gdv_Programs3SF";
      this.gdv_Programs3SF.RowsHeight = 24;
      this.gdv_Programs3SF.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.gdv_Programs3SF.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs3SF.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs3SF.Size = new System.Drawing.Size(370, 96);
      this.gdv_Programs3SF.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Programs3SF.TabIndex = 311;
      this.gdv_Programs3SF.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Programs3SF.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs3SF.TranslateText = true;
      this.gdv_Programs3SF.DoubleClick += new System.EventHandler(this.gdv_Programs3SF_DoubleClick);
      this.gdv_Programs3SF.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Programs3SF_CellFormatting);
      // 
      // ttl_WetEndTitle
      // 
      this.ttl_WetEndTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.ttl_WetEndTitle.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_WetEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_WetEndTitle.ForeColor = System.Drawing.Color.Aqua;
      this.ttl_WetEndTitle.Location = new System.Drawing.Point(84, 25);
      this.ttl_WetEndTitle.Name = "ttl_WetEndTitle";
      this.ttl_WetEndTitle.Size = new System.Drawing.Size(79, 24);
      this.ttl_WetEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_WetEndTitle.TabIndex = 378;
      this.ttl_WetEndTitle.Text = "WET";
      this.ttl_WetEndTitle.TranslateText = false;
      // 
      // pnl_clickExtension
      // 
      this.pnl_clickExtension.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pnl_clickExtension.Controls.Add(this.btn_IncDecSpeedSS);
      this.pnl_clickExtension.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pnl_clickExtension.Location = new System.Drawing.Point(0, 0);
      this.pnl_clickExtension.Name = "pnl_clickExtension";
      this.pnl_clickExtension.Size = new System.Drawing.Size(111, 137);
      this.pnl_clickExtension.TabIndex = 379;
      this.pnl_clickExtension.Click += new System.EventHandler(this.ttl_DryEndTitle_Click);
      // 
      // btn_IncDecSpeedSS
      // 
      this.btn_IncDecSpeedSS.Active = true;
      this.btn_IncDecSpeedSS.Alarmed = false;
      this.btn_IncDecSpeedSS.Auto = true;
      this.btn_IncDecSpeedSS.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSS.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSS.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSS.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSS.FramePresent = false;
      this.btn_IncDecSpeedSS.HighLightEnable = false;
      this.btn_IncDecSpeedSS.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSS.ImagePassword = null;
      this.btn_IncDecSpeedSS.Inemergency = false;
      this.btn_IncDecSpeedSS.InLocal = true;
      this.btn_IncDecSpeedSS.IsButton = false;
      this.btn_IncDecSpeedSS.IsForceToInvisible = false;
      this.btn_IncDecSpeedSS.Location = new System.Drawing.Point(62, 112);
      this.btn_IncDecSpeedSS.Name = "btn_IncDecSpeedSS";
      this.btn_IncDecSpeedSS.OnLine = true;
      this.btn_IncDecSpeedSS.ShowImage = true;
      this.btn_IncDecSpeedSS.ShowText = false;
      this.btn_IncDecSpeedSS.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSS.Switching = false;
      this.btn_IncDecSpeedSS.TabIndex = 360;
      this.btn_IncDecSpeedSS.TranslateText = false;
      this.btn_IncDecSpeedSS.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSS.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSS.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSS.UserDefinedImage")));
      this.btn_IncDecSpeedSS.Visible = false;
      this.btn_IncDecSpeedSS.Warned = false;
      // 
      // nud_SpeedLine
      // 
      this.nud_SpeedLine.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_SpeedLine.DecimalPlaces = 0;
      this.nud_SpeedLine.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SpeedLine.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedLine.ImagePassword = null;
      this.nud_SpeedLine.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SpeedLine.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_SpeedLine.Location = new System.Drawing.Point(1120, 136);
      this.nud_SpeedLine.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.nud_SpeedLine.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SpeedLine.Name = "nud_SpeedLine";
      this.nud_SpeedLine.ReadOnly = true;
      this.nud_SpeedLine.Size = new System.Drawing.Size(82, 33);
      this.nud_SpeedLine.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SpeedLine.TabIndex = 407;
      this.nud_SpeedLine.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedLine.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedLine.TranslateText = false;
      this.nud_SpeedLine.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_SpeedLine.UnitMeasureVisible = false;
      this.nud_SpeedLine.Value = "100000";
      this.nud_SpeedLine.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedLine.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_SpeedLine.ValueChanged += new System.EventHandler(this.orderChangeTimeLeftCalculation);
      // 
      // nud_SpeedZone1
      // 
      this.nud_SpeedZone1.DecimalPlaces = 0;
      this.nud_SpeedZone1.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SpeedZone1.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone1.ImagePassword = null;
      this.nud_SpeedZone1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SpeedZone1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_SpeedZone1.Location = new System.Drawing.Point(936, 112);
      this.nud_SpeedZone1.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.nud_SpeedZone1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SpeedZone1.Name = "nud_SpeedZone1";
      this.nud_SpeedZone1.ReadOnly = true;
      this.nud_SpeedZone1.Size = new System.Drawing.Size(90, 33);
      this.nud_SpeedZone1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SpeedZone1.TabIndex = 408;
      this.nud_SpeedZone1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone1.TranslateText = false;
      this.nud_SpeedZone1.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_SpeedZone1.UnitMeasureVisible = false;
      this.nud_SpeedZone1.Value = "100000";
      this.nud_SpeedZone1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_SpeedZone2
      // 
      this.nud_SpeedZone2.DecimalPlaces = 0;
      this.nud_SpeedZone2.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SpeedZone2.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone2.ImagePassword = null;
      this.nud_SpeedZone2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SpeedZone2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_SpeedZone2.Location = new System.Drawing.Point(722, 87);
      this.nud_SpeedZone2.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.nud_SpeedZone2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SpeedZone2.Name = "nud_SpeedZone2";
      this.nud_SpeedZone2.ReadOnly = true;
      this.nud_SpeedZone2.Size = new System.Drawing.Size(82, 33);
      this.nud_SpeedZone2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SpeedZone2.TabIndex = 409;
      this.nud_SpeedZone2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone2.TranslateText = false;
      this.nud_SpeedZone2.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_SpeedZone2.UnitMeasureVisible = false;
      this.nud_SpeedZone2.Value = "100000";
      this.nud_SpeedZone2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_S1Meters
      // 
      this.nud_S1Meters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_S1Meters.DecimalPlaces = 0;
      this.nud_S1Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S1Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S1Meters.ImagePassword = null;
      this.nud_S1Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S1Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S1Meters.Location = new System.Drawing.Point(977, 161);
      this.nud_S1Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S1Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S1Meters.Name = "nud_S1Meters";
      this.nud_S1Meters.ReadOnly = true;
      this.nud_S1Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S1Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S1Meters.TabIndex = 410;
      this.nud_S1Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S1Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S1Meters.TranslateText = false;
      this.nud_S1Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S1Meters.UnitMeasureVisible = false;
      this.nud_S1Meters.Value = "325000000";
      this.nud_S1Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S1Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_S2Meters
      // 
      this.nud_S2Meters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_S2Meters.DecimalPlaces = 0;
      this.nud_S2Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S2Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S2Meters.ImagePassword = null;
      this.nud_S2Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S2Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S2Meters.Location = new System.Drawing.Point(888, 161);
      this.nud_S2Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S2Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S2Meters.Name = "nud_S2Meters";
      this.nud_S2Meters.ReadOnly = true;
      this.nud_S2Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S2Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S2Meters.TabIndex = 411;
      this.nud_S2Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S2Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S2Meters.TranslateText = false;
      this.nud_S2Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S2Meters.UnitMeasureVisible = false;
      this.nud_S2Meters.Value = "325000000";
      this.nud_S2Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S2Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_S3Meters
      // 
      this.nud_S3Meters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_S3Meters.DecimalPlaces = 0;
      this.nud_S3Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S3Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S3Meters.ImagePassword = null;
      this.nud_S3Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S3Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S3Meters.Location = new System.Drawing.Point(744, 161);
      this.nud_S3Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S3Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S3Meters.Name = "nud_S3Meters";
      this.nud_S3Meters.ReadOnly = true;
      this.nud_S3Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S3Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S3Meters.TabIndex = 412;
      this.nud_S3Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S3Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S3Meters.TranslateText = false;
      this.nud_S3Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S3Meters.UnitMeasureVisible = false;
      this.nud_S3Meters.Value = "325000000";
      this.nud_S3Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S3Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_S4Meters
      // 
      this.nud_S4Meters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_S4Meters.DecimalPlaces = 0;
      this.nud_S4Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S4Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S4Meters.ImagePassword = null;
      this.nud_S4Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S4Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S4Meters.Location = new System.Drawing.Point(655, 161);
      this.nud_S4Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S4Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S4Meters.Name = "nud_S4Meters";
      this.nud_S4Meters.ReadOnly = true;
      this.nud_S4Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S4Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S4Meters.TabIndex = 413;
      this.nud_S4Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S4Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S4Meters.TranslateText = false;
      this.nud_S4Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S4Meters.UnitMeasureVisible = false;
      this.nud_S4Meters.Value = "325000000";
      this.nud_S4Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S4Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_S4Meters.Visible = false;
      // 
      // nud_S5Meters
      // 
      this.nud_S5Meters.DecimalPlaces = 0;
      this.nud_S5Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S5Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S5Meters.ImagePassword = null;
      this.nud_S5Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S5Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S5Meters.Location = new System.Drawing.Point(511, 161);
      this.nud_S5Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S5Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S5Meters.Name = "nud_S5Meters";
      this.nud_S5Meters.ReadOnly = true;
      this.nud_S5Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S5Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S5Meters.TabIndex = 414;
      this.nud_S5Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S5Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S5Meters.TranslateText = false;
      this.nud_S5Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S5Meters.UnitMeasureVisible = false;
      this.nud_S5Meters.Value = "325000000";
      this.nud_S5Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S5Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_S5Meters.Visible = false;
      // 
      // lbl_zone2Active
      // 
      this.lbl_zone2Active.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_zone2Active.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_zone2Active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.lbl_zone2Active.Location = new System.Drawing.Point(737, 84);
      this.lbl_zone2Active.Name = "lbl_zone2Active";
      this.lbl_zone2Active.Size = new System.Drawing.Size(57, 47);
      this.lbl_zone2Active.TabIndex = 415;
      this.lbl_zone2Active.Text = "X";
      this.lbl_zone2Active.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_zone2Active.Visible = false;
      // 
      // lbl_zone1Active
      // 
      this.lbl_zone1Active.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_zone1Active.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_zone1Active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.lbl_zone1Active.Location = new System.Drawing.Point(949, 109);
      this.lbl_zone1Active.Name = "lbl_zone1Active";
      this.lbl_zone1Active.Size = new System.Drawing.Size(57, 47);
      this.lbl_zone1Active.TabIndex = 416;
      this.lbl_zone1Active.Text = "X";
      this.lbl_zone1Active.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_zone1Active.Visible = false;
      // 
      // lbl_zoneDBActive
      // 
      this.lbl_zoneDBActive.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_zoneDBActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.lbl_zoneDBActive.Location = new System.Drawing.Point(1210, 133);
      this.lbl_zoneDBActive.Name = "lbl_zoneDBActive";
      this.lbl_zoneDBActive.Size = new System.Drawing.Size(57, 47);
      this.lbl_zoneDBActive.TabIndex = 417;
      this.lbl_zoneDBActive.Text = "X";
      this.lbl_zoneDBActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_zoneDBActive.Visible = false;
      // 
      // lbl_SF2Cartridge
      // 
      this.lbl_SF2Cartridge.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SF2Cartridge.ForeColor = System.Drawing.Color.Blue;
      this.lbl_SF2Cartridge.Location = new System.Drawing.Point(614, 159);
      this.lbl_SF2Cartridge.Name = "lbl_SF2Cartridge";
      this.lbl_SF2Cartridge.Size = new System.Drawing.Size(39, 28);
      this.lbl_SF2Cartridge.TabIndex = 418;
      this.lbl_SF2Cartridge.Text = "-";
      this.lbl_SF2Cartridge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_SF1Cartridge
      // 
      this.lbl_SF1Cartridge.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SF1Cartridge.ForeColor = System.Drawing.Color.Blue;
      this.lbl_SF1Cartridge.Location = new System.Drawing.Point(844, 159);
      this.lbl_SF1Cartridge.Name = "lbl_SF1Cartridge";
      this.lbl_SF1Cartridge.Size = new System.Drawing.Size(39, 28);
      this.lbl_SF1Cartridge.TabIndex = 419;
      this.lbl_SF1Cartridge.Text = "-";
      this.lbl_SF1Cartridge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // prb_BridgeContents2
      // 
      this.prb_BridgeContents2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.prb_BridgeContents2.BarStyle = SyncroC.Controls.BarStyles.Classic;
      this.prb_BridgeContents2.color = SyncroC.Controls.Colors.SyncroColor.White;
      this.prb_BridgeContents2.Font = new System.Drawing.Font("Arial Unicode MS", 15F, System.Drawing.FontStyle.Bold);
      this.prb_BridgeContents2.ForeColor = System.Drawing.Color.Black;
      this.prb_BridgeContents2.Location = new System.Drawing.Point(712, 57);
      this.prb_BridgeContents2.Name = "prb_BridgeContents2";
      this.prb_BridgeContents2.Percentage = 80;
      this.prb_BridgeContents2.PercentageVisible = false;
      this.prb_BridgeContents2.Size = new System.Drawing.Size(161, 12);
      this.prb_BridgeContents2.Style = SyncroC.Controls.ProgBarStyles.VerySmall;
      this.prb_BridgeContents2.TabIndex = 420;
      this.prb_BridgeContents2.TranslateText = false;
      // 
      // prb_BridgeContents1
      // 
      this.prb_BridgeContents1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.prb_BridgeContents1.BarStyle = SyncroC.Controls.BarStyles.Classic;
      this.prb_BridgeContents1.color = SyncroC.Controls.Colors.SyncroColor.White;
      this.prb_BridgeContents1.Font = new System.Drawing.Font("Arial Unicode MS", 15F, System.Drawing.FontStyle.Bold);
      this.prb_BridgeContents1.ForeColor = System.Drawing.Color.Black;
      this.prb_BridgeContents1.Location = new System.Drawing.Point(926, 80);
      this.prb_BridgeContents1.Name = "prb_BridgeContents1";
      this.prb_BridgeContents1.Percentage = 80;
      this.prb_BridgeContents1.PercentageVisible = false;
      this.prb_BridgeContents1.Size = new System.Drawing.Size(161, 12);
      this.prb_BridgeContents1.Style = SyncroC.Controls.ProgBarStyles.VerySmall;
      this.prb_BridgeContents1.TabIndex = 421;
      this.prb_BridgeContents1.TranslateText = false;
      // 
      // pnl_clickExtension2
      // 
      this.pnl_clickExtension2.Controls.Add(this.lbl_WECType_YY);
      this.pnl_clickExtension2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pnl_clickExtension2.Location = new System.Drawing.Point(169, 0);
      this.pnl_clickExtension2.Name = "pnl_clickExtension2";
      this.pnl_clickExtension2.Size = new System.Drawing.Size(510, 60);
      this.pnl_clickExtension2.TabIndex = 380;
      this.pnl_clickExtension2.Click += new System.EventHandler(this.lbl_WetEndMode_Click);
      // 
      // lbl_WECType_YY
      // 
      this.lbl_WECType_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.lbl_WECType_YY.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_WECType_YY.ForeColor = System.Drawing.Color.Aqua;
      this.lbl_WECType_YY.Location = new System.Drawing.Point(206, 25);
      this.lbl_WECType_YY.Name = "lbl_WECType_YY";
      this.lbl_WECType_YY.Size = new System.Drawing.Size(290, 24);
      this.lbl_WECType_YY.TabIndex = 231;
      this.lbl_WECType_YY.Text = "GAP LESS";
      this.lbl_WECType_YY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_BridgeContents2
      // 
      this.lbl_BridgeContents2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.lbl_BridgeContents2.Location = new System.Drawing.Point(874, 50);
      this.lbl_BridgeContents2.Name = "lbl_BridgeContents2";
      this.lbl_BridgeContents2.Size = new System.Drawing.Size(50, 23);
      this.lbl_BridgeContents2.TabIndex = 435;
      this.lbl_BridgeContents2.Text = "999";
      this.lbl_BridgeContents2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbl_BridgeContents1
      // 
      this.lbl_BridgeContents1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.lbl_BridgeContents1.Location = new System.Drawing.Point(1089, 75);
      this.lbl_BridgeContents1.Name = "lbl_BridgeContents1";
      this.lbl_BridgeContents1.Size = new System.Drawing.Size(50, 23);
      this.lbl_BridgeContents1.TabIndex = 436;
      this.lbl_BridgeContents1.Text = "999";
      this.lbl_BridgeContents1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // iml_WETAOCStatus
      // 
      this.iml_WETAOCStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_WETAOCStatus.ImageStream")));
      this.iml_WETAOCStatus.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_WETAOCStatus.Images.SetKeyName(0, "WETAOC_Manual.bmp");
      this.iml_WETAOCStatus.Images.SetKeyName(1, "WETAOC_Auto.bmp");
      this.iml_WETAOCStatus.Images.SetKeyName(2, "WETAOC_Override.bmp");
      // 
      // ttl_SimulationTitle
      // 
      this.ttl_SimulationTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.ttl_SimulationTitle.color = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.ttl_SimulationTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SimulationTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SimulationTitle.Location = new System.Drawing.Point(14, 159);
      this.ttl_SimulationTitle.Name = "ttl_SimulationTitle";
      this.ttl_SimulationTitle.Size = new System.Drawing.Size(260, 24);
      this.ttl_SimulationTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_SimulationTitle.TabIndex = 440;
      this.ttl_SimulationTitle.Text = "SIMULATION ACTIVE";
      this.ttl_SimulationTitle.TranslateText = false;
      this.ttl_SimulationTitle.Visible = false;
      // 
      // lbl_Splicer5Roll_B_Active
      // 
      this.lbl_Splicer5Roll_B_Active.AutoSize = true;
      this.lbl_Splicer5Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer5Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer5Roll_B_Active.Location = new System.Drawing.Point(536, 236);
      this.lbl_Splicer5Roll_B_Active.Name = "lbl_Splicer5Roll_B_Active";
      this.lbl_Splicer5Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer5Roll_B_Active.TabIndex = 441;
      this.lbl_Splicer5Roll_B_Active.Text = "        ";
      this.lbl_Splicer5Roll_B_Active.Visible = false;
      this.lbl_Splicer5Roll_B_Active.Click += new System.EventHandler(this.Splicer5_Click);
      // 
      // lbl_Splicer5Roll_A_Active
      // 
      this.lbl_Splicer5Roll_A_Active.AutoSize = true;
      this.lbl_Splicer5Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer5Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer5Roll_A_Active.Location = new System.Drawing.Point(560, 236);
      this.lbl_Splicer5Roll_A_Active.Name = "lbl_Splicer5Roll_A_Active";
      this.lbl_Splicer5Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer5Roll_A_Active.TabIndex = 442;
      this.lbl_Splicer5Roll_A_Active.Text = "        ";
      this.lbl_Splicer5Roll_A_Active.Visible = false;
      this.lbl_Splicer5Roll_A_Active.Click += new System.EventHandler(this.Splicer5_Click);
      // 
      // lbl_Splicer4Roll_B_Active
      // 
      this.lbl_Splicer4Roll_B_Active.AutoSize = true;
      this.lbl_Splicer4Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer4Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer4Roll_B_Active.Location = new System.Drawing.Point(704, 236);
      this.lbl_Splicer4Roll_B_Active.Name = "lbl_Splicer4Roll_B_Active";
      this.lbl_Splicer4Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer4Roll_B_Active.TabIndex = 444;
      this.lbl_Splicer4Roll_B_Active.Text = "        ";
      this.lbl_Splicer4Roll_B_Active.Visible = false;
      this.lbl_Splicer4Roll_B_Active.Click += new System.EventHandler(this.Splicer4_Click);
      // 
      // lbl_Splicer4Roll_A_Active
      // 
      this.lbl_Splicer4Roll_A_Active.AutoSize = true;
      this.lbl_Splicer4Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer4Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer4Roll_A_Active.Location = new System.Drawing.Point(680, 236);
      this.lbl_Splicer4Roll_A_Active.Name = "lbl_Splicer4Roll_A_Active";
      this.lbl_Splicer4Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer4Roll_A_Active.TabIndex = 443;
      this.lbl_Splicer4Roll_A_Active.Text = "        ";
      this.lbl_Splicer4Roll_A_Active.Visible = false;
      this.lbl_Splicer4Roll_A_Active.Click += new System.EventHandler(this.Splicer4_Click);
      // 
      // lbl_Splicer3Roll_A_Active
      // 
      this.lbl_Splicer3Roll_A_Active.AutoSize = true;
      this.lbl_Splicer3Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer3Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer3Roll_A_Active.Location = new System.Drawing.Point(793, 236);
      this.lbl_Splicer3Roll_A_Active.Name = "lbl_Splicer3Roll_A_Active";
      this.lbl_Splicer3Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer3Roll_A_Active.TabIndex = 446;
      this.lbl_Splicer3Roll_A_Active.Text = "        ";
      this.lbl_Splicer3Roll_A_Active.Visible = false;
      this.lbl_Splicer3Roll_A_Active.Click += new System.EventHandler(this.Splicer3_Click);
      // 
      // lbl_Splicer3Roll_B_Active
      // 
      this.lbl_Splicer3Roll_B_Active.AutoSize = true;
      this.lbl_Splicer3Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer3Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer3Roll_B_Active.Location = new System.Drawing.Point(769, 236);
      this.lbl_Splicer3Roll_B_Active.Name = "lbl_Splicer3Roll_B_Active";
      this.lbl_Splicer3Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer3Roll_B_Active.TabIndex = 445;
      this.lbl_Splicer3Roll_B_Active.Text = "        ";
      this.lbl_Splicer3Roll_B_Active.Visible = false;
      this.lbl_Splicer3Roll_B_Active.Click += new System.EventHandler(this.Splicer3_Click);
      // 
      // lbl_Splicer2Roll_B_Active
      // 
      this.lbl_Splicer2Roll_B_Active.AutoSize = true;
      this.lbl_Splicer2Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer2Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer2Roll_B_Active.Location = new System.Drawing.Point(937, 236);
      this.lbl_Splicer2Roll_B_Active.Name = "lbl_Splicer2Roll_B_Active";
      this.lbl_Splicer2Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer2Roll_B_Active.TabIndex = 448;
      this.lbl_Splicer2Roll_B_Active.Text = "        ";
      this.lbl_Splicer2Roll_B_Active.Visible = false;
      this.lbl_Splicer2Roll_B_Active.Click += new System.EventHandler(this.Splicer2_Click);
      // 
      // lbl_Splicer2Roll_A_Active
      // 
      this.lbl_Splicer2Roll_A_Active.AutoSize = true;
      this.lbl_Splicer2Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer2Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer2Roll_A_Active.Location = new System.Drawing.Point(913, 236);
      this.lbl_Splicer2Roll_A_Active.Name = "lbl_Splicer2Roll_A_Active";
      this.lbl_Splicer2Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer2Roll_A_Active.TabIndex = 447;
      this.lbl_Splicer2Roll_A_Active.Text = "        ";
      this.lbl_Splicer2Roll_A_Active.Visible = false;
      this.lbl_Splicer2Roll_A_Active.Click += new System.EventHandler(this.Splicer2_Click);
      // 
      // lbl_Splicer1Roll_A_Active
      // 
      this.lbl_Splicer1Roll_A_Active.AutoSize = true;
      this.lbl_Splicer1Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer1Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer1Roll_A_Active.Location = new System.Drawing.Point(1026, 236);
      this.lbl_Splicer1Roll_A_Active.Name = "lbl_Splicer1Roll_A_Active";
      this.lbl_Splicer1Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer1Roll_A_Active.TabIndex = 450;
      this.lbl_Splicer1Roll_A_Active.Text = "        ";
      this.lbl_Splicer1Roll_A_Active.Visible = false;
      this.lbl_Splicer1Roll_A_Active.Click += new System.EventHandler(this.Splicer1_Click);
      // 
      // lbl_Splicer1Roll_B_Active
      // 
      this.lbl_Splicer1Roll_B_Active.AutoSize = true;
      this.lbl_Splicer1Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer1Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer1Roll_B_Active.Location = new System.Drawing.Point(1002, 236);
      this.lbl_Splicer1Roll_B_Active.Name = "lbl_Splicer1Roll_B_Active";
      this.lbl_Splicer1Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer1Roll_B_Active.TabIndex = 449;
      this.lbl_Splicer1Roll_B_Active.Text = "        ";
      this.lbl_Splicer1Roll_B_Active.Visible = false;
      this.lbl_Splicer1Roll_B_Active.Click += new System.EventHandler(this.Splicer1_Click);
      // 
      // pct_notReadyToSplice_5
      // 
      this.pct_notReadyToSplice_5.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_5.Location = new System.Drawing.Point(559, 248);
      this.pct_notReadyToSplice_5.Name = "pct_notReadyToSplice_5";
      this.pct_notReadyToSplice_5.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_5.TabIndex = 451;
      this.pct_notReadyToSplice_5.TabStop = false;
      this.pct_notReadyToSplice_5.Visible = false;
      // 
      // pct_WETAOCStatus
      // 
      this.pct_WETAOCStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pct_WETAOCStatus.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_WETAOCStatus.Location = new System.Drawing.Point(229, 25);
      this.pct_WETAOCStatus.Name = "pct_WETAOCStatus";
      this.pct_WETAOCStatus.Size = new System.Drawing.Size(62, 24);
      this.pct_WETAOCStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_WETAOCStatus.TabIndex = 439;
      this.pct_WETAOCStatus.TabStop = false;
      this.pct_WETAOCStatus.Click += new System.EventHandler(this.lbl_WetEndMode_Click);
      // 
      // pct_BridgeError2
      // 
      this.pct_BridgeError2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BridgeError2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_BridgeError2.Image = global::SyncroC.Properties.Resources.Esclamativo;
      this.pct_BridgeError2.Location = new System.Drawing.Point(686, 118);
      this.pct_BridgeError2.Name = "pct_BridgeError2";
      this.pct_BridgeError2.Size = new System.Drawing.Size(32, 32);
      this.pct_BridgeError2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_BridgeError2.TabIndex = 438;
      this.pct_BridgeError2.TabStop = false;
      this.pct_BridgeError2.Visible = false;
      this.pct_BridgeError2.Click += new System.EventHandler(this.pct_BridgeError2_Click);
      // 
      // pct_BridgeError1
      // 
      this.pct_BridgeError1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BridgeError1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_BridgeError1.Image = global::SyncroC.Properties.Resources.Esclamativo;
      this.pct_BridgeError1.Location = new System.Drawing.Point(923, 129);
      this.pct_BridgeError1.Name = "pct_BridgeError1";
      this.pct_BridgeError1.Size = new System.Drawing.Size(32, 32);
      this.pct_BridgeError1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_BridgeError1.TabIndex = 437;
      this.pct_BridgeError1.TabStop = false;
      this.pct_BridgeError1.Visible = false;
      this.pct_BridgeError1.Click += new System.EventHandler(this.pct_BridgeError1_Click);
      // 
      // btn_SYCIncDecBandSpeedDB
      // 
      this.btn_SYCIncDecBandSpeedDB.Active = true;
      this.btn_SYCIncDecBandSpeedDB.Alarmed = false;
      this.btn_SYCIncDecBandSpeedDB.Auto = true;
      this.btn_SYCIncDecBandSpeedDB.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecBandSpeedDB.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecBandSpeedDB.FillBackColorWithImage = false;
      this.btn_SYCIncDecBandSpeedDB.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecBandSpeedDB.FramePresent = false;
      this.btn_SYCIncDecBandSpeedDB.HighLightEnable = false;
      this.btn_SYCIncDecBandSpeedDB.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecBandSpeedDB.ImagePassword = null;
      this.btn_SYCIncDecBandSpeedDB.Inemergency = false;
      this.btn_SYCIncDecBandSpeedDB.InLocal = true;
      this.btn_SYCIncDecBandSpeedDB.IsButton = false;
      this.btn_SYCIncDecBandSpeedDB.IsForceToInvisible = false;
      this.btn_SYCIncDecBandSpeedDB.Location = new System.Drawing.Point(1146, 116);
      this.btn_SYCIncDecBandSpeedDB.Name = "btn_SYCIncDecBandSpeedDB";
      this.btn_SYCIncDecBandSpeedDB.OnLine = true;
      this.btn_SYCIncDecBandSpeedDB.ShowImage = true;
      this.btn_SYCIncDecBandSpeedDB.ShowText = false;
      this.btn_SYCIncDecBandSpeedDB.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecBandSpeedDB.Switching = false;
      this.btn_SYCIncDecBandSpeedDB.TabIndex = 432;
      this.btn_SYCIncDecBandSpeedDB.TranslateText = false;
      this.btn_SYCIncDecBandSpeedDB.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedDB.UserDefinedActiveImage")));
      this.btn_SYCIncDecBandSpeedDB.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedDB.UserDefinedImage")));
      this.btn_SYCIncDecBandSpeedDB.Visible = false;
      this.btn_SYCIncDecBandSpeedDB.Warned = false;
      // 
      // btn_SYCIncDecSpeedDB
      // 
      this.btn_SYCIncDecSpeedDB.Active = true;
      this.btn_SYCIncDecSpeedDB.Alarmed = false;
      this.btn_SYCIncDecSpeedDB.Auto = true;
      this.btn_SYCIncDecSpeedDB.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecSpeedDB.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecSpeedDB.FillBackColorWithImage = false;
      this.btn_SYCIncDecSpeedDB.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecSpeedDB.FramePresent = false;
      this.btn_SYCIncDecSpeedDB.HighLightEnable = false;
      this.btn_SYCIncDecSpeedDB.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecSpeedDB.ImagePassword = null;
      this.btn_SYCIncDecSpeedDB.Inemergency = false;
      this.btn_SYCIncDecSpeedDB.InLocal = true;
      this.btn_SYCIncDecSpeedDB.IsButton = false;
      this.btn_SYCIncDecSpeedDB.IsForceToInvisible = false;
      this.btn_SYCIncDecSpeedDB.Location = new System.Drawing.Point(1146, 97);
      this.btn_SYCIncDecSpeedDB.Name = "btn_SYCIncDecSpeedDB";
      this.btn_SYCIncDecSpeedDB.OnLine = true;
      this.btn_SYCIncDecSpeedDB.ShowImage = true;
      this.btn_SYCIncDecSpeedDB.ShowText = false;
      this.btn_SYCIncDecSpeedDB.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecSpeedDB.Switching = false;
      this.btn_SYCIncDecSpeedDB.TabIndex = 431;
      this.btn_SYCIncDecSpeedDB.TranslateText = false;
      this.btn_SYCIncDecSpeedDB.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedDB.UserDefinedActiveImage")));
      this.btn_SYCIncDecSpeedDB.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedDB.UserDefinedImage")));
      this.btn_SYCIncDecSpeedDB.Visible = false;
      this.btn_SYCIncDecSpeedDB.Warned = false;
      // 
      // btn_SYCIncDecBandSpeedSF1
      // 
      this.btn_SYCIncDecBandSpeedSF1.Active = true;
      this.btn_SYCIncDecBandSpeedSF1.Alarmed = false;
      this.btn_SYCIncDecBandSpeedSF1.Auto = true;
      this.btn_SYCIncDecBandSpeedSF1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecBandSpeedSF1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecBandSpeedSF1.FillBackColorWithImage = false;
      this.btn_SYCIncDecBandSpeedSF1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecBandSpeedSF1.FramePresent = false;
      this.btn_SYCIncDecBandSpeedSF1.HighLightEnable = false;
      this.btn_SYCIncDecBandSpeedSF1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecBandSpeedSF1.ImagePassword = null;
      this.btn_SYCIncDecBandSpeedSF1.Inemergency = false;
      this.btn_SYCIncDecBandSpeedSF1.InLocal = true;
      this.btn_SYCIncDecBandSpeedSF1.IsButton = false;
      this.btn_SYCIncDecBandSpeedSF1.IsForceToInvisible = false;
      this.btn_SYCIncDecBandSpeedSF1.Location = new System.Drawing.Point(870, 115);
      this.btn_SYCIncDecBandSpeedSF1.Name = "btn_SYCIncDecBandSpeedSF1";
      this.btn_SYCIncDecBandSpeedSF1.OnLine = true;
      this.btn_SYCIncDecBandSpeedSF1.ShowImage = true;
      this.btn_SYCIncDecBandSpeedSF1.ShowText = false;
      this.btn_SYCIncDecBandSpeedSF1.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecBandSpeedSF1.Switching = false;
      this.btn_SYCIncDecBandSpeedSF1.TabIndex = 430;
      this.btn_SYCIncDecBandSpeedSF1.TranslateText = false;
      this.btn_SYCIncDecBandSpeedSF1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedSF1.UserDefinedActiveImage")));
      this.btn_SYCIncDecBandSpeedSF1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedSF1.UserDefinedImage")));
      this.btn_SYCIncDecBandSpeedSF1.Visible = false;
      this.btn_SYCIncDecBandSpeedSF1.Warned = false;
      // 
      // btn_SYCIncDecSpeedSF1
      // 
      this.btn_SYCIncDecSpeedSF1.Active = true;
      this.btn_SYCIncDecSpeedSF1.Alarmed = false;
      this.btn_SYCIncDecSpeedSF1.Auto = true;
      this.btn_SYCIncDecSpeedSF1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecSpeedSF1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecSpeedSF1.FillBackColorWithImage = false;
      this.btn_SYCIncDecSpeedSF1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecSpeedSF1.FramePresent = false;
      this.btn_SYCIncDecSpeedSF1.HighLightEnable = false;
      this.btn_SYCIncDecSpeedSF1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecSpeedSF1.ImagePassword = null;
      this.btn_SYCIncDecSpeedSF1.Inemergency = false;
      this.btn_SYCIncDecSpeedSF1.InLocal = true;
      this.btn_SYCIncDecSpeedSF1.IsButton = false;
      this.btn_SYCIncDecSpeedSF1.IsForceToInvisible = false;
      this.btn_SYCIncDecSpeedSF1.Location = new System.Drawing.Point(870, 94);
      this.btn_SYCIncDecSpeedSF1.Name = "btn_SYCIncDecSpeedSF1";
      this.btn_SYCIncDecSpeedSF1.OnLine = true;
      this.btn_SYCIncDecSpeedSF1.ShowImage = true;
      this.btn_SYCIncDecSpeedSF1.ShowText = false;
      this.btn_SYCIncDecSpeedSF1.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecSpeedSF1.Switching = false;
      this.btn_SYCIncDecSpeedSF1.TabIndex = 429;
      this.btn_SYCIncDecSpeedSF1.TranslateText = false;
      this.btn_SYCIncDecSpeedSF1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedSF1.UserDefinedActiveImage")));
      this.btn_SYCIncDecSpeedSF1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedSF1.UserDefinedImage")));
      this.btn_SYCIncDecSpeedSF1.Visible = false;
      this.btn_SYCIncDecSpeedSF1.Warned = false;
      // 
      // btn_SYCIncDecSpeedSF2
      // 
      this.btn_SYCIncDecSpeedSF2.Active = true;
      this.btn_SYCIncDecSpeedSF2.Alarmed = false;
      this.btn_SYCIncDecSpeedSF2.Auto = true;
      this.btn_SYCIncDecSpeedSF2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecSpeedSF2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecSpeedSF2.FillBackColorWithImage = false;
      this.btn_SYCIncDecSpeedSF2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecSpeedSF2.FramePresent = false;
      this.btn_SYCIncDecSpeedSF2.HighLightEnable = false;
      this.btn_SYCIncDecSpeedSF2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecSpeedSF2.ImagePassword = null;
      this.btn_SYCIncDecSpeedSF2.Inemergency = false;
      this.btn_SYCIncDecSpeedSF2.InLocal = true;
      this.btn_SYCIncDecSpeedSF2.IsButton = false;
      this.btn_SYCIncDecSpeedSF2.IsForceToInvisible = false;
      this.btn_SYCIncDecSpeedSF2.Location = new System.Drawing.Point(649, 63);
      this.btn_SYCIncDecSpeedSF2.Name = "btn_SYCIncDecSpeedSF2";
      this.btn_SYCIncDecSpeedSF2.OnLine = true;
      this.btn_SYCIncDecSpeedSF2.ShowImage = true;
      this.btn_SYCIncDecSpeedSF2.ShowText = false;
      this.btn_SYCIncDecSpeedSF2.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecSpeedSF2.Switching = false;
      this.btn_SYCIncDecSpeedSF2.TabIndex = 427;
      this.btn_SYCIncDecSpeedSF2.TranslateText = false;
      this.btn_SYCIncDecSpeedSF2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedSF2.UserDefinedActiveImage")));
      this.btn_SYCIncDecSpeedSF2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedSF2.UserDefinedImage")));
      this.btn_SYCIncDecSpeedSF2.Visible = false;
      this.btn_SYCIncDecSpeedSF2.Warned = false;
      // 
      // AutoManS1
      // 
      this.AutoManS1.Active = false;
      this.AutoManS1.Alarmed = false;
      this.AutoManS1.Auto = false;
      this.AutoManS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS1.FillBackColorWithImage = false;
      this.AutoManS1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS1.FramePresent = false;
      this.AutoManS1.HighLightEnable = false;
      this.AutoManS1.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS1.ImagePassword = null;
      this.AutoManS1.Inemergency = false;
      this.AutoManS1.InLocal = false;
      this.AutoManS1.IsButton = false;
      this.AutoManS1.IsForceToInvisible = false;
      this.AutoManS1.Location = new System.Drawing.Point(1030, 245);
      this.AutoManS1.Name = "AutoManS1";
      this.AutoManS1.OnLine = false;
      this.AutoManS1.ShowImage = true;
      this.AutoManS1.ShowText = false;
      this.AutoManS1.Size = new System.Drawing.Size(30, 23);
      this.AutoManS1.Switching = false;
      this.AutoManS1.TabIndex = 426;
      this.AutoManS1.TranslateText = false;
      this.AutoManS1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS1.UserDefinedActiveImage")));
      this.AutoManS1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS1.UserDefinedImage")));
      this.AutoManS1.Warned = false;
      // 
      // AutoManS2
      // 
      this.AutoManS2.Active = false;
      this.AutoManS2.Alarmed = false;
      this.AutoManS2.Auto = false;
      this.AutoManS2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS2.FillBackColorWithImage = false;
      this.AutoManS2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS2.FramePresent = false;
      this.AutoManS2.HighLightEnable = false;
      this.AutoManS2.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS2.ImagePassword = null;
      this.AutoManS2.Inemergency = false;
      this.AutoManS2.InLocal = false;
      this.AutoManS2.IsButton = false;
      this.AutoManS2.IsForceToInvisible = false;
      this.AutoManS2.Location = new System.Drawing.Point(938, 245);
      this.AutoManS2.Name = "AutoManS2";
      this.AutoManS2.OnLine = false;
      this.AutoManS2.ShowImage = true;
      this.AutoManS2.ShowText = false;
      this.AutoManS2.Size = new System.Drawing.Size(30, 23);
      this.AutoManS2.Switching = false;
      this.AutoManS2.TabIndex = 425;
      this.AutoManS2.TranslateText = false;
      this.AutoManS2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS2.UserDefinedActiveImage")));
      this.AutoManS2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS2.UserDefinedImage")));
      this.AutoManS2.Warned = false;
      // 
      // AutoManS3
      // 
      this.AutoManS3.Active = false;
      this.AutoManS3.Alarmed = false;
      this.AutoManS3.Auto = false;
      this.AutoManS3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS3.FillBackColorWithImage = false;
      this.AutoManS3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS3.FramePresent = false;
      this.AutoManS3.HighLightEnable = false;
      this.AutoManS3.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS3.ImagePassword = null;
      this.AutoManS3.Inemergency = false;
      this.AutoManS3.InLocal = false;
      this.AutoManS3.IsButton = false;
      this.AutoManS3.IsForceToInvisible = false;
      this.AutoManS3.Location = new System.Drawing.Point(797, 245);
      this.AutoManS3.Name = "AutoManS3";
      this.AutoManS3.OnLine = false;
      this.AutoManS3.ShowImage = true;
      this.AutoManS3.ShowText = false;
      this.AutoManS3.Size = new System.Drawing.Size(30, 23);
      this.AutoManS3.Switching = false;
      this.AutoManS3.TabIndex = 424;
      this.AutoManS3.TranslateText = false;
      this.AutoManS3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS3.UserDefinedActiveImage")));
      this.AutoManS3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS3.UserDefinedImage")));
      this.AutoManS3.Warned = false;
      // 
      // AutoManS4
      // 
      this.AutoManS4.Active = false;
      this.AutoManS4.Alarmed = false;
      this.AutoManS4.Auto = false;
      this.AutoManS4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS4.FillBackColorWithImage = false;
      this.AutoManS4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS4.FramePresent = false;
      this.AutoManS4.HighLightEnable = false;
      this.AutoManS4.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS4.ImagePassword = null;
      this.AutoManS4.Inemergency = false;
      this.AutoManS4.InLocal = false;
      this.AutoManS4.IsButton = false;
      this.AutoManS4.IsForceToInvisible = false;
      this.AutoManS4.Location = new System.Drawing.Point(708, 245);
      this.AutoManS4.Name = "AutoManS4";
      this.AutoManS4.OnLine = false;
      this.AutoManS4.ShowImage = true;
      this.AutoManS4.ShowText = false;
      this.AutoManS4.Size = new System.Drawing.Size(30, 23);
      this.AutoManS4.Switching = false;
      this.AutoManS4.TabIndex = 423;
      this.AutoManS4.TranslateText = false;
      this.AutoManS4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS4.UserDefinedActiveImage")));
      this.AutoManS4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS4.UserDefinedImage")));
      this.AutoManS4.Warned = false;
      // 
      // AutoManS5
      // 
      this.AutoManS5.Active = false;
      this.AutoManS5.Alarmed = false;
      this.AutoManS5.Auto = false;
      this.AutoManS5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS5.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS5.FillBackColorWithImage = false;
      this.AutoManS5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS5.FramePresent = false;
      this.AutoManS5.HighLightEnable = false;
      this.AutoManS5.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS5.ImagePassword = null;
      this.AutoManS5.Inemergency = false;
      this.AutoManS5.InLocal = false;
      this.AutoManS5.IsButton = false;
      this.AutoManS5.IsForceToInvisible = false;
      this.AutoManS5.Location = new System.Drawing.Point(564, 245);
      this.AutoManS5.Name = "AutoManS5";
      this.AutoManS5.OnLine = false;
      this.AutoManS5.ShowImage = true;
      this.AutoManS5.ShowText = false;
      this.AutoManS5.Size = new System.Drawing.Size(30, 23);
      this.AutoManS5.Switching = false;
      this.AutoManS5.TabIndex = 422;
      this.AutoManS5.TranslateText = false;
      this.AutoManS5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS5.UserDefinedActiveImage")));
      this.AutoManS5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS5.UserDefinedImage")));
      this.AutoManS5.Warned = false;
      // 
      // btn_IncDecSpeedPHT
      // 
      this.btn_IncDecSpeedPHT.Active = true;
      this.btn_IncDecSpeedPHT.Alarmed = false;
      this.btn_IncDecSpeedPHT.Auto = true;
      this.btn_IncDecSpeedPHT.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedPHT.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedPHT.FillBackColorWithImage = false;
      this.btn_IncDecSpeedPHT.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedPHT.FramePresent = false;
      this.btn_IncDecSpeedPHT.HighLightEnable = false;
      this.btn_IncDecSpeedPHT.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedPHT.ImagePassword = null;
      this.btn_IncDecSpeedPHT.Inemergency = false;
      this.btn_IncDecSpeedPHT.InLocal = true;
      this.btn_IncDecSpeedPHT.IsButton = false;
      this.btn_IncDecSpeedPHT.IsForceToInvisible = false;
      this.btn_IncDecSpeedPHT.Location = new System.Drawing.Point(1062, 245);
      this.btn_IncDecSpeedPHT.Name = "btn_IncDecSpeedPHT";
      this.btn_IncDecSpeedPHT.OnLine = true;
      this.btn_IncDecSpeedPHT.ShowImage = true;
      this.btn_IncDecSpeedPHT.ShowText = false;
      this.btn_IncDecSpeedPHT.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedPHT.Switching = false;
      this.btn_IncDecSpeedPHT.TabIndex = 377;
      this.btn_IncDecSpeedPHT.TranslateText = false;
      this.btn_IncDecSpeedPHT.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedPHT.UserDefinedActiveImage")));
      this.btn_IncDecSpeedPHT.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedPHT.UserDefinedImage")));
      this.btn_IncDecSpeedPHT.Visible = false;
      this.btn_IncDecSpeedPHT.Warned = false;
      // 
      // pct_PHTNotRdyRun
      // 
      this.pct_PHTNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_PHTNotRdyRun.Image")));
      this.pct_PHTNotRdyRun.Location = new System.Drawing.Point(1064, 248);
      this.pct_PHTNotRdyRun.Name = "pct_PHTNotRdyRun";
      this.pct_PHTNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_PHTNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_PHTNotRdyRun.TabIndex = 376;
      this.pct_PHTNotRdyRun.TabStop = false;
      this.pct_PHTNotRdyRun.Visible = false;
      // 
      // PreHeaterTern
      // 
      this.PreHeaterTern.Active = false;
      this.PreHeaterTern.Alarmed = false;
      this.PreHeaterTern.Auto = false;
      this.PreHeaterTern.BackColor = System.Drawing.Color.WhiteSmoke;
      this.PreHeaterTern.Cursor = System.Windows.Forms.Cursors.Hand;
      this.PreHeaterTern.FillBackColorWithImage = false;
      this.PreHeaterTern.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.PreHeaterTern.FramePresent = false;
      this.PreHeaterTern.HighLightEnable = false;
      this.PreHeaterTern.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.PreHeaterTern.ImagePassword = null;
      this.PreHeaterTern.Inemergency = false;
      this.PreHeaterTern.InLocal = false;
      this.PreHeaterTern.IsButton = true;
      this.PreHeaterTern.IsForceToInvisible = false;
      this.PreHeaterTern.Location = new System.Drawing.Point(1060, 185);
      this.PreHeaterTern.Name = "PreHeaterTern";
      this.PreHeaterTern.OnLine = false;
      this.PreHeaterTern.ShowImage = true;
      this.PreHeaterTern.ShowText = false;
      this.PreHeaterTern.Size = new System.Drawing.Size(72, 60);
      this.PreHeaterTern.Switching = false;
      this.PreHeaterTern.TabIndex = 375;
      this.PreHeaterTern.TranslateText = false;
      this.PreHeaterTern.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("PreHeaterTern.UserDefinedActiveImage")));
      this.PreHeaterTern.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("PreHeaterTern.UserDefinedImage")));
      this.PreHeaterTern.Warned = false;
      this.PreHeaterTern.Click += new System.EventHandler(this.PreHeaterTern_Click);
      // 
      // btn_IncDecSpeedWEConsole
      // 
      this.btn_IncDecSpeedWEConsole.Active = true;
      this.btn_IncDecSpeedWEConsole.Alarmed = false;
      this.btn_IncDecSpeedWEConsole.Auto = true;
      this.btn_IncDecSpeedWEConsole.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedWEConsole.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedWEConsole.FillBackColorWithImage = false;
      this.btn_IncDecSpeedWEConsole.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedWEConsole.FramePresent = false;
      this.btn_IncDecSpeedWEConsole.HighLightEnable = false;
      this.btn_IncDecSpeedWEConsole.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedWEConsole.ImagePassword = null;
      this.btn_IncDecSpeedWEConsole.Inemergency = false;
      this.btn_IncDecSpeedWEConsole.InLocal = true;
      this.btn_IncDecSpeedWEConsole.IsButton = false;
      this.btn_IncDecSpeedWEConsole.IsForceToInvisible = false;
      this.btn_IncDecSpeedWEConsole.Location = new System.Drawing.Point(445, 245);
      this.btn_IncDecSpeedWEConsole.Name = "btn_IncDecSpeedWEConsole";
      this.btn_IncDecSpeedWEConsole.OnLine = true;
      this.btn_IncDecSpeedWEConsole.ShowImage = true;
      this.btn_IncDecSpeedWEConsole.ShowText = false;
      this.btn_IncDecSpeedWEConsole.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedWEConsole.Switching = false;
      this.btn_IncDecSpeedWEConsole.TabIndex = 372;
      this.btn_IncDecSpeedWEConsole.TranslateText = false;
      this.btn_IncDecSpeedWEConsole.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedWEConsole.UserDefinedActiveImage")));
      this.btn_IncDecSpeedWEConsole.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedWEConsole.UserDefinedImage")));
      this.btn_IncDecSpeedWEConsole.Visible = false;
      this.btn_IncDecSpeedWEConsole.Warned = false;
      // 
      // btn_IncDecSpeedDB
      // 
      this.btn_IncDecSpeedDB.Active = true;
      this.btn_IncDecSpeedDB.Alarmed = false;
      this.btn_IncDecSpeedDB.Auto = true;
      this.btn_IncDecSpeedDB.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedDB.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedDB.FillBackColorWithImage = false;
      this.btn_IncDecSpeedDB.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedDB.FramePresent = false;
      this.btn_IncDecSpeedDB.HighLightEnable = false;
      this.btn_IncDecSpeedDB.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedDB.ImagePassword = null;
      this.btn_IncDecSpeedDB.Inemergency = false;
      this.btn_IncDecSpeedDB.InLocal = true;
      this.btn_IncDecSpeedDB.IsButton = false;
      this.btn_IncDecSpeedDB.IsForceToInvisible = false;
      this.btn_IncDecSpeedDB.Location = new System.Drawing.Point(1206, 245);
      this.btn_IncDecSpeedDB.Name = "btn_IncDecSpeedDB";
      this.btn_IncDecSpeedDB.OnLine = true;
      this.btn_IncDecSpeedDB.ShowImage = true;
      this.btn_IncDecSpeedDB.ShowText = false;
      this.btn_IncDecSpeedDB.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedDB.Switching = false;
      this.btn_IncDecSpeedDB.TabIndex = 371;
      this.btn_IncDecSpeedDB.TranslateText = false;
      this.btn_IncDecSpeedDB.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedDB.UserDefinedActiveImage")));
      this.btn_IncDecSpeedDB.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedDB.UserDefinedImage")));
      this.btn_IncDecSpeedDB.Visible = false;
      this.btn_IncDecSpeedDB.Warned = false;
      // 
      // btn_IncDecSpeedGU
      // 
      this.btn_IncDecSpeedGU.Active = true;
      this.btn_IncDecSpeedGU.Alarmed = false;
      this.btn_IncDecSpeedGU.Auto = true;
      this.btn_IncDecSpeedGU.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedGU.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedGU.FillBackColorWithImage = false;
      this.btn_IncDecSpeedGU.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedGU.FramePresent = false;
      this.btn_IncDecSpeedGU.HighLightEnable = false;
      this.btn_IncDecSpeedGU.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedGU.ImagePassword = null;
      this.btn_IncDecSpeedGU.Inemergency = false;
      this.btn_IncDecSpeedGU.InLocal = true;
      this.btn_IncDecSpeedGU.IsButton = false;
      this.btn_IncDecSpeedGU.IsForceToInvisible = false;
      this.btn_IncDecSpeedGU.Location = new System.Drawing.Point(1134, 245);
      this.btn_IncDecSpeedGU.Name = "btn_IncDecSpeedGU";
      this.btn_IncDecSpeedGU.OnLine = true;
      this.btn_IncDecSpeedGU.ShowImage = true;
      this.btn_IncDecSpeedGU.ShowText = false;
      this.btn_IncDecSpeedGU.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedGU.Switching = false;
      this.btn_IncDecSpeedGU.TabIndex = 370;
      this.btn_IncDecSpeedGU.TranslateText = false;
      this.btn_IncDecSpeedGU.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedGU.UserDefinedActiveImage")));
      this.btn_IncDecSpeedGU.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedGU.UserDefinedImage")));
      this.btn_IncDecSpeedGU.Visible = false;
      this.btn_IncDecSpeedGU.Warned = false;
      // 
      // btn_IncDecSpeedSPL1
      // 
      this.btn_IncDecSpeedSPL1.Active = true;
      this.btn_IncDecSpeedSPL1.Alarmed = false;
      this.btn_IncDecSpeedSPL1.Auto = true;
      this.btn_IncDecSpeedSPL1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL1.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL1.FramePresent = false;
      this.btn_IncDecSpeedSPL1.HighLightEnable = false;
      this.btn_IncDecSpeedSPL1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL1.ImagePassword = null;
      this.btn_IncDecSpeedSPL1.Inemergency = false;
      this.btn_IncDecSpeedSPL1.InLocal = true;
      this.btn_IncDecSpeedSPL1.IsButton = false;
      this.btn_IncDecSpeedSPL1.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL1.Location = new System.Drawing.Point(991, 245);
      this.btn_IncDecSpeedSPL1.Name = "btn_IncDecSpeedSPL1";
      this.btn_IncDecSpeedSPL1.OnLine = true;
      this.btn_IncDecSpeedSPL1.ShowImage = true;
      this.btn_IncDecSpeedSPL1.ShowText = false;
      this.btn_IncDecSpeedSPL1.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL1.Switching = false;
      this.btn_IncDecSpeedSPL1.TabIndex = 369;
      this.btn_IncDecSpeedSPL1.TranslateText = false;
      this.btn_IncDecSpeedSPL1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL1.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL1.UserDefinedImage")));
      this.btn_IncDecSpeedSPL1.Visible = false;
      this.btn_IncDecSpeedSPL1.Warned = false;
      // 
      // btn_IncDecSpeedSPL2
      // 
      this.btn_IncDecSpeedSPL2.Active = true;
      this.btn_IncDecSpeedSPL2.Alarmed = false;
      this.btn_IncDecSpeedSPL2.Auto = true;
      this.btn_IncDecSpeedSPL2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL2.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL2.FramePresent = false;
      this.btn_IncDecSpeedSPL2.HighLightEnable = false;
      this.btn_IncDecSpeedSPL2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL2.ImagePassword = null;
      this.btn_IncDecSpeedSPL2.Inemergency = false;
      this.btn_IncDecSpeedSPL2.InLocal = true;
      this.btn_IncDecSpeedSPL2.IsButton = false;
      this.btn_IncDecSpeedSPL2.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL2.Location = new System.Drawing.Point(901, 245);
      this.btn_IncDecSpeedSPL2.Name = "btn_IncDecSpeedSPL2";
      this.btn_IncDecSpeedSPL2.OnLine = true;
      this.btn_IncDecSpeedSPL2.ShowImage = true;
      this.btn_IncDecSpeedSPL2.ShowText = false;
      this.btn_IncDecSpeedSPL2.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL2.Switching = false;
      this.btn_IncDecSpeedSPL2.TabIndex = 368;
      this.btn_IncDecSpeedSPL2.TranslateText = false;
      this.btn_IncDecSpeedSPL2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL2.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL2.UserDefinedImage")));
      this.btn_IncDecSpeedSPL2.Visible = false;
      this.btn_IncDecSpeedSPL2.Warned = false;
      // 
      // btn_IncDecSpeedSF1
      // 
      this.btn_IncDecSpeedSF1.Active = true;
      this.btn_IncDecSpeedSF1.Alarmed = false;
      this.btn_IncDecSpeedSF1.Auto = true;
      this.btn_IncDecSpeedSF1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSF1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSF1.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSF1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSF1.FramePresent = false;
      this.btn_IncDecSpeedSF1.HighLightEnable = false;
      this.btn_IncDecSpeedSF1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSF1.ImagePassword = null;
      this.btn_IncDecSpeedSF1.Inemergency = false;
      this.btn_IncDecSpeedSF1.InLocal = true;
      this.btn_IncDecSpeedSF1.IsButton = false;
      this.btn_IncDecSpeedSF1.IsForceToInvisible = false;
      this.btn_IncDecSpeedSF1.Location = new System.Drawing.Point(828, 245);
      this.btn_IncDecSpeedSF1.Name = "btn_IncDecSpeedSF1";
      this.btn_IncDecSpeedSF1.OnLine = true;
      this.btn_IncDecSpeedSF1.ShowImage = true;
      this.btn_IncDecSpeedSF1.ShowText = false;
      this.btn_IncDecSpeedSF1.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSF1.Switching = false;
      this.btn_IncDecSpeedSF1.TabIndex = 367;
      this.btn_IncDecSpeedSF1.TranslateText = false;
      this.btn_IncDecSpeedSF1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSF1.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSF1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSF1.UserDefinedImage")));
      this.btn_IncDecSpeedSF1.Visible = false;
      this.btn_IncDecSpeedSF1.Warned = false;
      // 
      // btn_IncDecSpeedSPL3
      // 
      this.btn_IncDecSpeedSPL3.Active = true;
      this.btn_IncDecSpeedSPL3.Alarmed = false;
      this.btn_IncDecSpeedSPL3.Auto = true;
      this.btn_IncDecSpeedSPL3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL3.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL3.FramePresent = false;
      this.btn_IncDecSpeedSPL3.HighLightEnable = false;
      this.btn_IncDecSpeedSPL3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL3.ImagePassword = null;
      this.btn_IncDecSpeedSPL3.Inemergency = false;
      this.btn_IncDecSpeedSPL3.InLocal = true;
      this.btn_IncDecSpeedSPL3.IsButton = false;
      this.btn_IncDecSpeedSPL3.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL3.Location = new System.Drawing.Point(755, 245);
      this.btn_IncDecSpeedSPL3.Name = "btn_IncDecSpeedSPL3";
      this.btn_IncDecSpeedSPL3.OnLine = true;
      this.btn_IncDecSpeedSPL3.ShowImage = true;
      this.btn_IncDecSpeedSPL3.ShowText = false;
      this.btn_IncDecSpeedSPL3.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL3.Switching = false;
      this.btn_IncDecSpeedSPL3.TabIndex = 366;
      this.btn_IncDecSpeedSPL3.TranslateText = false;
      this.btn_IncDecSpeedSPL3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL3.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL3.UserDefinedImage")));
      this.btn_IncDecSpeedSPL3.Visible = false;
      this.btn_IncDecSpeedSPL3.Warned = false;
      // 
      // btn_IncDecSpeedSPL4
      // 
      this.btn_IncDecSpeedSPL4.Active = true;
      this.btn_IncDecSpeedSPL4.Alarmed = false;
      this.btn_IncDecSpeedSPL4.Auto = true;
      this.btn_IncDecSpeedSPL4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL4.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL4.FramePresent = false;
      this.btn_IncDecSpeedSPL4.HighLightEnable = false;
      this.btn_IncDecSpeedSPL4.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL4.ImagePassword = null;
      this.btn_IncDecSpeedSPL4.Inemergency = false;
      this.btn_IncDecSpeedSPL4.InLocal = true;
      this.btn_IncDecSpeedSPL4.IsButton = false;
      this.btn_IncDecSpeedSPL4.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL4.Location = new System.Drawing.Point(669, 245);
      this.btn_IncDecSpeedSPL4.Name = "btn_IncDecSpeedSPL4";
      this.btn_IncDecSpeedSPL4.OnLine = true;
      this.btn_IncDecSpeedSPL4.ShowImage = true;
      this.btn_IncDecSpeedSPL4.ShowText = false;
      this.btn_IncDecSpeedSPL4.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL4.Switching = false;
      this.btn_IncDecSpeedSPL4.TabIndex = 365;
      this.btn_IncDecSpeedSPL4.TranslateText = false;
      this.btn_IncDecSpeedSPL4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL4.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL4.UserDefinedImage")));
      this.btn_IncDecSpeedSPL4.Visible = false;
      this.btn_IncDecSpeedSPL4.Warned = false;
      // 
      // btn_IncDecSpeedSF2
      // 
      this.btn_IncDecSpeedSF2.Active = true;
      this.btn_IncDecSpeedSF2.Alarmed = false;
      this.btn_IncDecSpeedSF2.Auto = true;
      this.btn_IncDecSpeedSF2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSF2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSF2.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSF2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSF2.FramePresent = false;
      this.btn_IncDecSpeedSF2.HighLightEnable = false;
      this.btn_IncDecSpeedSF2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSF2.ImagePassword = null;
      this.btn_IncDecSpeedSF2.Inemergency = false;
      this.btn_IncDecSpeedSF2.InLocal = true;
      this.btn_IncDecSpeedSF2.IsButton = false;
      this.btn_IncDecSpeedSF2.IsForceToInvisible = false;
      this.btn_IncDecSpeedSF2.Location = new System.Drawing.Point(593, 245);
      this.btn_IncDecSpeedSF2.Name = "btn_IncDecSpeedSF2";
      this.btn_IncDecSpeedSF2.OnLine = true;
      this.btn_IncDecSpeedSF2.ShowImage = true;
      this.btn_IncDecSpeedSF2.ShowText = false;
      this.btn_IncDecSpeedSF2.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSF2.Switching = false;
      this.btn_IncDecSpeedSF2.TabIndex = 364;
      this.btn_IncDecSpeedSF2.TranslateText = false;
      this.btn_IncDecSpeedSF2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSF2.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSF2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSF2.UserDefinedImage")));
      this.btn_IncDecSpeedSF2.Visible = false;
      this.btn_IncDecSpeedSF2.Warned = false;
      // 
      // btn_IncDecSpeedSPL5
      // 
      this.btn_IncDecSpeedSPL5.Active = true;
      this.btn_IncDecSpeedSPL5.Alarmed = false;
      this.btn_IncDecSpeedSPL5.Auto = true;
      this.btn_IncDecSpeedSPL5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL5.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL5.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL5.FramePresent = false;
      this.btn_IncDecSpeedSPL5.HighLightEnable = false;
      this.btn_IncDecSpeedSPL5.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL5.ImagePassword = null;
      this.btn_IncDecSpeedSPL5.Inemergency = false;
      this.btn_IncDecSpeedSPL5.InLocal = true;
      this.btn_IncDecSpeedSPL5.IsButton = false;
      this.btn_IncDecSpeedSPL5.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL5.Location = new System.Drawing.Point(522, 245);
      this.btn_IncDecSpeedSPL5.Name = "btn_IncDecSpeedSPL5";
      this.btn_IncDecSpeedSPL5.OnLine = true;
      this.btn_IncDecSpeedSPL5.ShowImage = true;
      this.btn_IncDecSpeedSPL5.ShowText = false;
      this.btn_IncDecSpeedSPL5.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL5.Switching = false;
      this.btn_IncDecSpeedSPL5.TabIndex = 357;
      this.btn_IncDecSpeedSPL5.TranslateText = false;
      this.btn_IncDecSpeedSPL5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL5.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL5.UserDefinedImage")));
      this.btn_IncDecSpeedSPL5.Visible = false;
      this.btn_IncDecSpeedSPL5.Warned = false;
      // 
      // pct_SPL5NotRdyRun
      // 
      this.pct_SPL5NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL5NotRdyRun.Image")));
      this.pct_SPL5NotRdyRun.Location = new System.Drawing.Point(526, 248);
      this.pct_SPL5NotRdyRun.Name = "pct_SPL5NotRdyRun";
      this.pct_SPL5NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL5NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL5NotRdyRun.TabIndex = 334;
      this.pct_SPL5NotRdyRun.TabStop = false;
      this.pct_SPL5NotRdyRun.Visible = false;
      // 
      // pct_SF2NotRdyRun
      // 
      this.pct_SF2NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SF2NotRdyRun.Image")));
      this.pct_SF2NotRdyRun.Location = new System.Drawing.Point(598, 248);
      this.pct_SF2NotRdyRun.Name = "pct_SF2NotRdyRun";
      this.pct_SF2NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SF2NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SF2NotRdyRun.TabIndex = 332;
      this.pct_SF2NotRdyRun.TabStop = false;
      // 
      // pct_SPL4NotRdyRun
      // 
      this.pct_SPL4NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL4NotRdyRun.Image")));
      this.pct_SPL4NotRdyRun.Location = new System.Drawing.Point(670, 248);
      this.pct_SPL4NotRdyRun.Name = "pct_SPL4NotRdyRun";
      this.pct_SPL4NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL4NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL4NotRdyRun.TabIndex = 330;
      this.pct_SPL4NotRdyRun.TabStop = false;
      this.pct_SPL4NotRdyRun.Visible = false;
      // 
      // pct_SPL3NotRdyRun
      // 
      this.pct_SPL3NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL3NotRdyRun.Image")));
      this.pct_SPL3NotRdyRun.Location = new System.Drawing.Point(759, 248);
      this.pct_SPL3NotRdyRun.Name = "pct_SPL3NotRdyRun";
      this.pct_SPL3NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL3NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL3NotRdyRun.TabIndex = 328;
      this.pct_SPL3NotRdyRun.TabStop = false;
      this.pct_SPL3NotRdyRun.Visible = false;
      // 
      // pct_SF1NotRdyRun
      // 
      this.pct_SF1NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SF1NotRdyRun.Image")));
      this.pct_SF1NotRdyRun.Location = new System.Drawing.Point(831, 248);
      this.pct_SF1NotRdyRun.Name = "pct_SF1NotRdyRun";
      this.pct_SF1NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SF1NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SF1NotRdyRun.TabIndex = 326;
      this.pct_SF1NotRdyRun.TabStop = false;
      this.pct_SF1NotRdyRun.Visible = false;
      // 
      // pct_SPL2NotRdyRun
      // 
      this.pct_SPL2NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL2NotRdyRun.Image")));
      this.pct_SPL2NotRdyRun.Location = new System.Drawing.Point(903, 248);
      this.pct_SPL2NotRdyRun.Name = "pct_SPL2NotRdyRun";
      this.pct_SPL2NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL2NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL2NotRdyRun.TabIndex = 324;
      this.pct_SPL2NotRdyRun.TabStop = false;
      this.pct_SPL2NotRdyRun.Visible = false;
      // 
      // pct_SPL1NotRdyRun
      // 
      this.pct_SPL1NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL1NotRdyRun.Image")));
      this.pct_SPL1NotRdyRun.Location = new System.Drawing.Point(992, 248);
      this.pct_SPL1NotRdyRun.Name = "pct_SPL1NotRdyRun";
      this.pct_SPL1NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL1NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL1NotRdyRun.TabIndex = 322;
      this.pct_SPL1NotRdyRun.TabStop = false;
      this.pct_SPL1NotRdyRun.Visible = false;
      // 
      // pct_GUNotRdyRun
      // 
      this.pct_GUNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_GUNotRdyRun.Image")));
      this.pct_GUNotRdyRun.Location = new System.Drawing.Point(1136, 250);
      this.pct_GUNotRdyRun.Name = "pct_GUNotRdyRun";
      this.pct_GUNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_GUNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_GUNotRdyRun.TabIndex = 320;
      this.pct_GUNotRdyRun.TabStop = false;
      this.pct_GUNotRdyRun.Visible = false;
      // 
      // pct_DBNotRdyRun
      // 
      this.pct_DBNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_DBNotRdyRun.Image")));
      this.pct_DBNotRdyRun.Location = new System.Drawing.Point(1208, 251);
      this.pct_DBNotRdyRun.Name = "pct_DBNotRdyRun";
      this.pct_DBNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_DBNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_DBNotRdyRun.TabIndex = 266;
      this.pct_DBNotRdyRun.TabStop = false;
      this.pct_DBNotRdyRun.Visible = false;
      // 
      // WetEnd
      // 
      this.WetEnd.Active = false;
      this.WetEnd.Alarmed = false;
      this.WetEnd.Auto = false;
      this.WetEnd.BackColor = System.Drawing.Color.WhiteSmoke;
      this.WetEnd.Cursor = System.Windows.Forms.Cursors.Hand;
      this.WetEnd.FillBackColorWithImage = false;
      this.WetEnd.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.WetEnd.FramePresent = false;
      this.WetEnd.HighLightEnable = false;
      this.WetEnd.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.WetEnd.ImagePassword = null;
      this.WetEnd.Inemergency = false;
      this.WetEnd.InLocal = false;
      this.WetEnd.IsButton = true;
      this.WetEnd.IsForceToInvisible = false;
      this.WetEnd.Location = new System.Drawing.Point(445, 185);
      this.WetEnd.Name = "WetEnd";
      this.WetEnd.OnLine = false;
      this.WetEnd.ShowImage = true;
      this.WetEnd.ShowText = false;
      this.WetEnd.Size = new System.Drawing.Size(60, 60);
      this.WetEnd.Switching = false;
      this.WetEnd.TabIndex = 243;
      this.WetEnd.TranslateText = false;
      this.WetEnd.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("WetEnd.UserDefinedActiveImage")));
      this.WetEnd.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("WetEnd.UserDefinedImage")));
      this.WetEnd.Warned = false;
      this.WetEnd.Click += new System.EventHandler(this.WetEnd_Click);
      // 
      // DoubleBacker
      // 
      this.DoubleBacker.Active = false;
      this.DoubleBacker.Alarmed = false;
      this.DoubleBacker.Auto = false;
      this.DoubleBacker.BackColor = System.Drawing.Color.WhiteSmoke;
      this.DoubleBacker.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DoubleBacker.FillBackColorWithImage = false;
      this.DoubleBacker.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.DoubleBacker.FramePresent = false;
      this.DoubleBacker.HighLightEnable = false;
      this.DoubleBacker.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.DoubleBacker.ImagePassword = null;
      this.DoubleBacker.Inemergency = false;
      this.DoubleBacker.InLocal = false;
      this.DoubleBacker.IsButton = true;
      this.DoubleBacker.IsForceToInvisible = false;
      this.DoubleBacker.Location = new System.Drawing.Point(1204, 185);
      this.DoubleBacker.Name = "DoubleBacker";
      this.DoubleBacker.OnLine = false;
      this.DoubleBacker.ShowImage = true;
      this.DoubleBacker.ShowText = false;
      this.DoubleBacker.Size = new System.Drawing.Size(72, 60);
      this.DoubleBacker.Switching = false;
      this.DoubleBacker.TabIndex = 242;
      this.DoubleBacker.TranslateText = false;
      this.DoubleBacker.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("DoubleBacker.UserDefinedActiveImage")));
      this.DoubleBacker.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("DoubleBacker.UserDefinedImage")));
      this.DoubleBacker.Warned = false;
      this.DoubleBacker.Click += new System.EventHandler(this.DoubleBacker_Click);
      // 
      // GlueUnit
      // 
      this.GlueUnit.Active = false;
      this.GlueUnit.Alarmed = false;
      this.GlueUnit.Auto = false;
      this.GlueUnit.BackColor = System.Drawing.Color.WhiteSmoke;
      this.GlueUnit.Cursor = System.Windows.Forms.Cursors.Hand;
      this.GlueUnit.FillBackColorWithImage = false;
      this.GlueUnit.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.GlueUnit.FramePresent = false;
      this.GlueUnit.HighLightEnable = false;
      this.GlueUnit.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.GlueUnit.ImagePassword = null;
      this.GlueUnit.Inemergency = false;
      this.GlueUnit.InLocal = false;
      this.GlueUnit.IsButton = true;
      this.GlueUnit.IsForceToInvisible = false;
      this.GlueUnit.Location = new System.Drawing.Point(1132, 185);
      this.GlueUnit.Name = "GlueUnit";
      this.GlueUnit.OnLine = false;
      this.GlueUnit.ShowImage = true;
      this.GlueUnit.ShowText = false;
      this.GlueUnit.Size = new System.Drawing.Size(72, 60);
      this.GlueUnit.Switching = false;
      this.GlueUnit.TabIndex = 241;
      this.GlueUnit.TranslateText = false;
      this.GlueUnit.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("GlueUnit.UserDefinedActiveImage")));
      this.GlueUnit.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("GlueUnit.UserDefinedImage")));
      this.GlueUnit.Warned = false;
      this.GlueUnit.Click += new System.EventHandler(this.GlueUnit_Click);
      // 
      // Splicer1
      // 
      this.Splicer1.Active = false;
      this.Splicer1.Alarmed = false;
      this.Splicer1.Auto = false;
      this.Splicer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer1.FillBackColorWithImage = false;
      this.Splicer1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer1.FramePresent = false;
      this.Splicer1.HighLightEnable = false;
      this.Splicer1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer1.ImagePassword = null;
      this.Splicer1.Inemergency = false;
      this.Splicer1.InLocal = false;
      this.Splicer1.IsButton = true;
      this.Splicer1.IsForceToInvisible = false;
      this.Splicer1.Location = new System.Drawing.Point(988, 185);
      this.Splicer1.Name = "Splicer1";
      this.Splicer1.OnLine = false;
      this.Splicer1.ShowImage = true;
      this.Splicer1.ShowText = false;
      this.Splicer1.Size = new System.Drawing.Size(72, 60);
      this.Splicer1.Switching = false;
      this.Splicer1.TabIndex = 240;
      this.Splicer1.TranslateText = false;
      this.Splicer1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer1.UserDefinedActiveImage")));
      this.Splicer1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer1.UserDefinedImage")));
      this.Splicer1.Warned = false;
      this.Splicer1.Click += new System.EventHandler(this.Splicer1_Click);
      // 
      // Splicer2
      // 
      this.Splicer2.Active = false;
      this.Splicer2.Alarmed = false;
      this.Splicer2.Auto = false;
      this.Splicer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer2.FillBackColorWithImage = false;
      this.Splicer2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer2.FramePresent = false;
      this.Splicer2.HighLightEnable = false;
      this.Splicer2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer2.ImagePassword = null;
      this.Splicer2.Inemergency = false;
      this.Splicer2.InLocal = false;
      this.Splicer2.IsButton = true;
      this.Splicer2.IsForceToInvisible = false;
      this.Splicer2.Location = new System.Drawing.Point(899, 185);
      this.Splicer2.Name = "Splicer2";
      this.Splicer2.OnLine = false;
      this.Splicer2.ShowImage = true;
      this.Splicer2.ShowText = false;
      this.Splicer2.Size = new System.Drawing.Size(72, 60);
      this.Splicer2.Switching = false;
      this.Splicer2.TabIndex = 239;
      this.Splicer2.TranslateText = false;
      this.Splicer2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer2.UserDefinedActiveImage")));
      this.Splicer2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer2.UserDefinedImage")));
      this.Splicer2.Warned = false;
      this.Splicer2.Click += new System.EventHandler(this.Splicer2_Click);
      // 
      // SingleFacer1
      // 
      this.SingleFacer1.Active = false;
      this.SingleFacer1.Alarmed = false;
      this.SingleFacer1.Auto = false;
      this.SingleFacer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SingleFacer1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SingleFacer1.FillBackColorWithImage = false;
      this.SingleFacer1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.SingleFacer1.FramePresent = false;
      this.SingleFacer1.HighLightEnable = false;
      this.SingleFacer1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SingleFacer1.ImagePassword = null;
      this.SingleFacer1.Inemergency = false;
      this.SingleFacer1.InLocal = false;
      this.SingleFacer1.IsButton = true;
      this.SingleFacer1.IsForceToInvisible = false;
      this.SingleFacer1.Location = new System.Drawing.Point(827, 185);
      this.SingleFacer1.Name = "SingleFacer1";
      this.SingleFacer1.OnLine = false;
      this.SingleFacer1.ShowImage = true;
      this.SingleFacer1.ShowText = false;
      this.SingleFacer1.Size = new System.Drawing.Size(72, 60);
      this.SingleFacer1.Switching = false;
      this.SingleFacer1.TabIndex = 238;
      this.SingleFacer1.TranslateText = false;
      this.SingleFacer1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SingleFacer1.UserDefinedActiveImage")));
      this.SingleFacer1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SingleFacer1.UserDefinedImage")));
      this.SingleFacer1.Warned = false;
      this.SingleFacer1.Click += new System.EventHandler(this.SingleFacer1_Click);
      // 
      // Splicer3
      // 
      this.Splicer3.Active = false;
      this.Splicer3.Alarmed = false;
      this.Splicer3.Auto = false;
      this.Splicer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer3.FillBackColorWithImage = false;
      this.Splicer3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer3.FramePresent = false;
      this.Splicer3.HighLightEnable = false;
      this.Splicer3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer3.ImagePassword = null;
      this.Splicer3.Inemergency = false;
      this.Splicer3.InLocal = false;
      this.Splicer3.IsButton = true;
      this.Splicer3.IsForceToInvisible = false;
      this.Splicer3.Location = new System.Drawing.Point(755, 185);
      this.Splicer3.Name = "Splicer3";
      this.Splicer3.OnLine = false;
      this.Splicer3.ShowImage = true;
      this.Splicer3.ShowText = false;
      this.Splicer3.Size = new System.Drawing.Size(72, 60);
      this.Splicer3.Switching = false;
      this.Splicer3.TabIndex = 237;
      this.Splicer3.TranslateText = false;
      this.Splicer3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer3.UserDefinedActiveImage")));
      this.Splicer3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer3.UserDefinedImage")));
      this.Splicer3.Warned = false;
      this.Splicer3.Click += new System.EventHandler(this.Splicer3_Click);
      // 
      // Splicer4
      // 
      this.Splicer4.Active = false;
      this.Splicer4.Alarmed = false;
      this.Splicer4.Auto = false;
      this.Splicer4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer4.FillBackColorWithImage = false;
      this.Splicer4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer4.FramePresent = false;
      this.Splicer4.HighLightEnable = false;
      this.Splicer4.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer4.ImagePassword = null;
      this.Splicer4.Inemergency = false;
      this.Splicer4.InLocal = false;
      this.Splicer4.IsButton = true;
      this.Splicer4.IsForceToInvisible = false;
      this.Splicer4.Location = new System.Drawing.Point(666, 185);
      this.Splicer4.Name = "Splicer4";
      this.Splicer4.OnLine = false;
      this.Splicer4.ShowImage = true;
      this.Splicer4.ShowText = false;
      this.Splicer4.Size = new System.Drawing.Size(72, 60);
      this.Splicer4.Switching = false;
      this.Splicer4.TabIndex = 236;
      this.Splicer4.TranslateText = false;
      this.Splicer4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer4.UserDefinedActiveImage")));
      this.Splicer4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer4.UserDefinedImage")));
      this.Splicer4.Warned = false;
      this.Splicer4.Click += new System.EventHandler(this.Splicer4_Click);
      // 
      // SingleFacer2
      // 
      this.SingleFacer2.Active = false;
      this.SingleFacer2.Alarmed = false;
      this.SingleFacer2.Auto = false;
      this.SingleFacer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SingleFacer2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SingleFacer2.FillBackColorWithImage = false;
      this.SingleFacer2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.SingleFacer2.FramePresent = false;
      this.SingleFacer2.HighLightEnable = false;
      this.SingleFacer2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SingleFacer2.ImagePassword = null;
      this.SingleFacer2.Inemergency = false;
      this.SingleFacer2.InLocal = false;
      this.SingleFacer2.IsButton = true;
      this.SingleFacer2.IsForceToInvisible = false;
      this.SingleFacer2.Location = new System.Drawing.Point(594, 185);
      this.SingleFacer2.Name = "SingleFacer2";
      this.SingleFacer2.OnLine = false;
      this.SingleFacer2.ShowImage = true;
      this.SingleFacer2.ShowText = false;
      this.SingleFacer2.Size = new System.Drawing.Size(72, 60);
      this.SingleFacer2.Switching = false;
      this.SingleFacer2.TabIndex = 235;
      this.SingleFacer2.TranslateText = false;
      this.SingleFacer2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SingleFacer2.UserDefinedActiveImage")));
      this.SingleFacer2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SingleFacer2.UserDefinedImage")));
      this.SingleFacer2.Warned = false;
      this.SingleFacer2.Click += new System.EventHandler(this.SingleFacer2_Click);
      // 
      // Splicer5
      // 
      this.Splicer5.Active = false;
      this.Splicer5.Alarmed = false;
      this.Splicer5.Auto = false;
      this.Splicer5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer5.FillBackColorWithImage = false;
      this.Splicer5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer5.FramePresent = false;
      this.Splicer5.HighLightEnable = false;
      this.Splicer5.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer5.ImagePassword = null;
      this.Splicer5.Inemergency = false;
      this.Splicer5.InLocal = false;
      this.Splicer5.IsButton = true;
      this.Splicer5.IsForceToInvisible = false;
      this.Splicer5.Location = new System.Drawing.Point(522, 185);
      this.Splicer5.Name = "Splicer5";
      this.Splicer5.OnLine = false;
      this.Splicer5.ShowImage = true;
      this.Splicer5.ShowText = false;
      this.Splicer5.Size = new System.Drawing.Size(72, 60);
      this.Splicer5.Switching = false;
      this.Splicer5.TabIndex = 233;
      this.Splicer5.TranslateText = false;
      this.Splicer5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer5.UserDefinedActiveImage")));
      this.Splicer5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer5.UserDefinedImage")));
      this.Splicer5.Warned = false;
      this.Splicer5.Click += new System.EventHandler(this.Splicer5_Click);
      // 
      // ToggleAutoManZone2
      // 
      this.ToggleAutoManZone2.Active = false;
      this.ToggleAutoManZone2.Alarmed = false;
      this.ToggleAutoManZone2.Auto = false;
      this.ToggleAutoManZone2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManZone2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManZone2.FillBackColorWithImage = false;
      this.ToggleAutoManZone2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManZone2.FramePresent = true;
      this.ToggleAutoManZone2.HighLightEnable = false;
      this.ToggleAutoManZone2.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManZone2.ImagePassword = null;
      this.ToggleAutoManZone2.Inemergency = false;
      this.ToggleAutoManZone2.InLocal = false;
      this.ToggleAutoManZone2.IsButton = true;
      this.ToggleAutoManZone2.IsForceToInvisible = false;
      this.ToggleAutoManZone2.Location = new System.Drawing.Point(801, 86);
      this.ToggleAutoManZone2.Name = "ToggleAutoManZone2";
      this.ToggleAutoManZone2.OnLine = false;
      this.ToggleAutoManZone2.ShowImage = true;
      this.ToggleAutoManZone2.ShowText = false;
      this.ToggleAutoManZone2.Size = new System.Drawing.Size(57, 47);
      this.ToggleAutoManZone2.Switching = false;
      this.ToggleAutoManZone2.TabIndex = 345;
      this.ToggleAutoManZone2.TranslateText = false;
      this.ToggleAutoManZone2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManZone2.UserDefinedActiveImage")));
      this.ToggleAutoManZone2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManZone2.UserDefinedImage")));
      this.ToggleAutoManZone2.Warned = false;
      this.ToggleAutoManZone2.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ToggleAutoManWetEnd
      // 
      this.ToggleAutoManWetEnd.Active = false;
      this.ToggleAutoManWetEnd.Alarmed = false;
      this.ToggleAutoManWetEnd.Auto = false;
      this.ToggleAutoManWetEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManWetEnd.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManWetEnd.FillBackColorWithImage = false;
      this.ToggleAutoManWetEnd.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManWetEnd.FramePresent = true;
      this.ToggleAutoManWetEnd.HighLightEnable = false;
      this.ToggleAutoManWetEnd.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManWetEnd.ImagePassword = null;
      this.ToggleAutoManWetEnd.Inemergency = false;
      this.ToggleAutoManWetEnd.InLocal = false;
      this.ToggleAutoManWetEnd.IsButton = true;
      this.ToggleAutoManWetEnd.IsForceToInvisible = false;
      this.ToggleAutoManWetEnd.Location = new System.Drawing.Point(1210, 133);
      this.ToggleAutoManWetEnd.Name = "ToggleAutoManWetEnd";
      this.ToggleAutoManWetEnd.OnLine = false;
      this.ToggleAutoManWetEnd.ShowImage = true;
      this.ToggleAutoManWetEnd.ShowText = false;
      this.ToggleAutoManWetEnd.Size = new System.Drawing.Size(57, 47);
      this.ToggleAutoManWetEnd.Switching = false;
      this.ToggleAutoManWetEnd.TabIndex = 382;
      this.ToggleAutoManWetEnd.TranslateText = false;
      this.ToggleAutoManWetEnd.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManWetEnd.UserDefinedActiveImage")));
      this.ToggleAutoManWetEnd.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManWetEnd.UserDefinedImage")));
      this.ToggleAutoManWetEnd.Warned = false;
      this.ToggleAutoManWetEnd.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ToggleAutoManZone1
      // 
      this.ToggleAutoManZone1.Active = false;
      this.ToggleAutoManZone1.Alarmed = false;
      this.ToggleAutoManZone1.Auto = false;
      this.ToggleAutoManZone1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManZone1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManZone1.FillBackColorWithImage = false;
      this.ToggleAutoManZone1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManZone1.FramePresent = true;
      this.ToggleAutoManZone1.HighLightEnable = false;
      this.ToggleAutoManZone1.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManZone1.ImagePassword = null;
      this.ToggleAutoManZone1.Inemergency = false;
      this.ToggleAutoManZone1.InLocal = false;
      this.ToggleAutoManZone1.IsButton = true;
      this.ToggleAutoManZone1.IsForceToInvisible = false;
      this.ToggleAutoManZone1.Location = new System.Drawing.Point(1026, 109);
      this.ToggleAutoManZone1.Name = "ToggleAutoManZone1";
      this.ToggleAutoManZone1.OnLine = false;
      this.ToggleAutoManZone1.ShowImage = true;
      this.ToggleAutoManZone1.ShowText = false;
      this.ToggleAutoManZone1.Size = new System.Drawing.Size(57, 47);
      this.ToggleAutoManZone1.Switching = false;
      this.ToggleAutoManZone1.TabIndex = 346;
      this.ToggleAutoManZone1.TranslateText = false;
      this.ToggleAutoManZone1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManZone1.UserDefinedActiveImage")));
      this.ToggleAutoManZone1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManZone1.UserDefinedImage")));
      this.ToggleAutoManZone1.Warned = false;
      this.ToggleAutoManZone1.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // pct_Bridge1
      // 
      this.pct_Bridge1.Image = ((System.Drawing.Image)(resources.GetObject("pct_Bridge1.Image")));
      this.pct_Bridge1.Location = new System.Drawing.Point(863, 95);
      this.pct_Bridge1.Name = "pct_Bridge1";
      this.pct_Bridge1.Size = new System.Drawing.Size(224, 71);
      this.pct_Bridge1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pct_Bridge1.TabIndex = 401;
      this.pct_Bridge1.TabStop = false;
      // 
      // btn_SYCIncDecBandSpeedSF2
      // 
      this.btn_SYCIncDecBandSpeedSF2.Active = true;
      this.btn_SYCIncDecBandSpeedSF2.Alarmed = false;
      this.btn_SYCIncDecBandSpeedSF2.Auto = true;
      this.btn_SYCIncDecBandSpeedSF2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecBandSpeedSF2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecBandSpeedSF2.FillBackColorWithImage = false;
      this.btn_SYCIncDecBandSpeedSF2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecBandSpeedSF2.FramePresent = false;
      this.btn_SYCIncDecBandSpeedSF2.HighLightEnable = false;
      this.btn_SYCIncDecBandSpeedSF2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecBandSpeedSF2.ImagePassword = null;
      this.btn_SYCIncDecBandSpeedSF2.Inemergency = false;
      this.btn_SYCIncDecBandSpeedSF2.InLocal = true;
      this.btn_SYCIncDecBandSpeedSF2.IsButton = false;
      this.btn_SYCIncDecBandSpeedSF2.IsForceToInvisible = false;
      this.btn_SYCIncDecBandSpeedSF2.Location = new System.Drawing.Point(649, 84);
      this.btn_SYCIncDecBandSpeedSF2.Name = "btn_SYCIncDecBandSpeedSF2";
      this.btn_SYCIncDecBandSpeedSF2.OnLine = true;
      this.btn_SYCIncDecBandSpeedSF2.ShowImage = true;
      this.btn_SYCIncDecBandSpeedSF2.ShowText = false;
      this.btn_SYCIncDecBandSpeedSF2.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecBandSpeedSF2.Switching = false;
      this.btn_SYCIncDecBandSpeedSF2.TabIndex = 428;
      this.btn_SYCIncDecBandSpeedSF2.TranslateText = false;
      this.btn_SYCIncDecBandSpeedSF2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedSF2.UserDefinedActiveImage")));
      this.btn_SYCIncDecBandSpeedSF2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedSF2.UserDefinedImage")));
      this.btn_SYCIncDecBandSpeedSF2.Visible = false;
      this.btn_SYCIncDecBandSpeedSF2.Warned = false;
      // 
      // pct_Bridge2
      // 
      this.pct_Bridge2.Image = ((System.Drawing.Image)(resources.GetObject("pct_Bridge2.Image")));
      this.pct_Bridge2.Location = new System.Drawing.Point(649, 73);
      this.pct_Bridge2.Name = "pct_Bridge2";
      this.pct_Bridge2.Size = new System.Drawing.Size(224, 87);
      this.pct_Bridge2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pct_Bridge2.TabIndex = 400;
      this.pct_Bridge2.TabStop = false;
      // 
      // pct_notReadyToSplice_4
      // 
      this.pct_notReadyToSplice_4.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_4.Location = new System.Drawing.Point(702, 248);
      this.pct_notReadyToSplice_4.Name = "pct_notReadyToSplice_4";
      this.pct_notReadyToSplice_4.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_4.TabIndex = 452;
      this.pct_notReadyToSplice_4.TabStop = false;
      this.pct_notReadyToSplice_4.Visible = false;
      // 
      // pct_notReadyToSplice_3
      // 
      this.pct_notReadyToSplice_3.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_3.Location = new System.Drawing.Point(793, 248);
      this.pct_notReadyToSplice_3.Name = "pct_notReadyToSplice_3";
      this.pct_notReadyToSplice_3.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_3.TabIndex = 453;
      this.pct_notReadyToSplice_3.TabStop = false;
      this.pct_notReadyToSplice_3.Visible = false;
      // 
      // pct_notReadyToSplice_2
      // 
      this.pct_notReadyToSplice_2.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_2.Location = new System.Drawing.Point(936, 248);
      this.pct_notReadyToSplice_2.Name = "pct_notReadyToSplice_2";
      this.pct_notReadyToSplice_2.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_2.TabIndex = 454;
      this.pct_notReadyToSplice_2.TabStop = false;
      this.pct_notReadyToSplice_2.Visible = false;
      // 
      // pct_notReadyToSplice_1
      // 
      this.pct_notReadyToSplice_1.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_1.Location = new System.Drawing.Point(1027, 248);
      this.pct_notReadyToSplice_1.Name = "pct_notReadyToSplice_1";
      this.pct_notReadyToSplice_1.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_1.TabIndex = 455;
      this.pct_notReadyToSplice_1.TabStop = false;
      this.pct_notReadyToSplice_1.Visible = false;
      // 
      // grp_now_percheMiSonoFermato
      // 
      this.grp_now_percheMiSonoFermato.BackgroundColor = System.Drawing.Color.White;
      this.grp_now_percheMiSonoFermato.BackgroundGradientColor = System.Drawing.Color.Red;
      this.grp_now_percheMiSonoFermato.BackgroundGradientMode = SyncroC.Controls.GroupPanel.GroupPanel.GroupBoxGradientMode.ForwardDiagonal;
      this.grp_now_percheMiSonoFermato.BorderColor = System.Drawing.Color.Black;
      this.grp_now_percheMiSonoFermato.BorderThickness = 1F;
      this.grp_now_percheMiSonoFermato.Controls.Add(this.pct_now);
      this.grp_now_percheMiSonoFermato.Controls.Add(this.lbl_now_numeroMacchina_percheMiSonoFermato);
      this.grp_now_percheMiSonoFermato.Controls.Add(this.pct_now_causa_percheMiSonoFermato);
      this.grp_now_percheMiSonoFermato.CustomGroupBoxColor = System.Drawing.Color.Gray;
      this.grp_now_percheMiSonoFermato.Font = new System.Drawing.Font("Arial Unicode MS", 12F);
      this.grp_now_percheMiSonoFermato.ForeColor = System.Drawing.Color.White;
      this.grp_now_percheMiSonoFermato.GroupImage = null;
      this.grp_now_percheMiSonoFermato.GroupTitle = "24/12/1974 00:00:00:000";
      this.grp_now_percheMiSonoFermato.Location = new System.Drawing.Point(13, 175);
      this.grp_now_percheMiSonoFermato.Name = "grp_now_percheMiSonoFermato";
      this.grp_now_percheMiSonoFermato.PaintGroupBox = true;
      this.grp_now_percheMiSonoFermato.RoundCorners = 10;
      this.grp_now_percheMiSonoFermato.ShadowColor = System.Drawing.Color.DarkGray;
      this.grp_now_percheMiSonoFermato.ShadowControl = true;
      this.grp_now_percheMiSonoFermato.ShadowThickness = 3;
      this.grp_now_percheMiSonoFermato.Size = new System.Drawing.Size(261, 95);
      this.grp_now_percheMiSonoFermato.TabIndex = 458;
      this.grp_now_percheMiSonoFermato.TabStop = false;
      this.grp_now_percheMiSonoFermato.Visible = false;
      // 
      // pct_now
      // 
      this.pct_now.Image = global::SyncroC.Properties.Resources.Terna;
      this.pct_now.Location = new System.Drawing.Point(29, 29);
      this.pct_now.Name = "pct_now";
      this.pct_now.Size = new System.Drawing.Size(72, 60);
      this.pct_now.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_now.TabIndex = 4;
      this.pct_now.TabStop = false;
      // 
      // lbl_now_numeroMacchina_percheMiSonoFermato
      // 
      this.lbl_now_numeroMacchina_percheMiSonoFermato.BackColor = System.Drawing.Color.Transparent;
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_now_numeroMacchina_percheMiSonoFermato.ForeColor = System.Drawing.Color.Black;
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Location = new System.Drawing.Point(110, 20);
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Name = "lbl_now_numeroMacchina_percheMiSonoFermato";
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Size = new System.Drawing.Size(25, 35);
      this.lbl_now_numeroMacchina_percheMiSonoFermato.TabIndex = 2;
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "9";
      this.lbl_now_numeroMacchina_percheMiSonoFermato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_now_causa_percheMiSonoFermato
      // 
      this.pct_now_causa_percheMiSonoFermato.BackColor = System.Drawing.Color.Transparent;
      this.pct_now_causa_percheMiSonoFermato.Image = global::SyncroC.Properties.Resources.NStop;
      this.pct_now_causa_percheMiSonoFermato.Location = new System.Drawing.Point(152, 29);
      this.pct_now_causa_percheMiSonoFermato.Name = "pct_now_causa_percheMiSonoFermato";
      this.pct_now_causa_percheMiSonoFermato.Size = new System.Drawing.Size(72, 60);
      this.pct_now_causa_percheMiSonoFermato.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_now_causa_percheMiSonoFermato.TabIndex = 1;
      this.pct_now_causa_percheMiSonoFermato.TabStop = false;
      // 
      // Splicer6
      // 
      this.Splicer6.Active = false;
      this.Splicer6.Alarmed = false;
      this.Splicer6.Auto = false;
      this.Splicer6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer6.FillBackColorWithImage = false;
      this.Splicer6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer6.FramePresent = false;
      this.Splicer6.HighLightEnable = false;
      this.Splicer6.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer6.ImagePassword = null;
      this.Splicer6.Inemergency = false;
      this.Splicer6.InLocal = false;
      this.Splicer6.IsButton = true;
      this.Splicer6.IsForceToInvisible = false;
      this.Splicer6.Location = new System.Drawing.Point(433, 185);
      this.Splicer6.Name = "Splicer6";
      this.Splicer6.OnLine = false;
      this.Splicer6.ShowImage = true;
      this.Splicer6.ShowText = false;
      this.Splicer6.Size = new System.Drawing.Size(72, 60);
      this.Splicer6.Switching = false;
      this.Splicer6.TabIndex = 459;
      this.Splicer6.TranslateText = false;
      this.Splicer6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer6.UserDefinedActiveImage")));
      this.Splicer6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer6.UserDefinedImage")));
      this.Splicer6.Visible = false;
      this.Splicer6.Warned = false;
      this.Splicer6.Click += new System.EventHandler(this.Splicer6_Click);
      // 
      // btn_IncDecSpeedSPL6
      // 
      this.btn_IncDecSpeedSPL6.Active = true;
      this.btn_IncDecSpeedSPL6.Alarmed = false;
      this.btn_IncDecSpeedSPL6.Auto = true;
      this.btn_IncDecSpeedSPL6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL6.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL6.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL6.FramePresent = false;
      this.btn_IncDecSpeedSPL6.HighLightEnable = false;
      this.btn_IncDecSpeedSPL6.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL6.ImagePassword = null;
      this.btn_IncDecSpeedSPL6.Inemergency = false;
      this.btn_IncDecSpeedSPL6.InLocal = true;
      this.btn_IncDecSpeedSPL6.IsButton = false;
      this.btn_IncDecSpeedSPL6.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL6.Location = new System.Drawing.Point(433, 245);
      this.btn_IncDecSpeedSPL6.Name = "btn_IncDecSpeedSPL6";
      this.btn_IncDecSpeedSPL6.OnLine = true;
      this.btn_IncDecSpeedSPL6.ShowImage = true;
      this.btn_IncDecSpeedSPL6.ShowText = false;
      this.btn_IncDecSpeedSPL6.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL6.Switching = false;
      this.btn_IncDecSpeedSPL6.TabIndex = 460;
      this.btn_IncDecSpeedSPL6.TranslateText = false;
      this.btn_IncDecSpeedSPL6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL6.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL6.UserDefinedImage")));
      this.btn_IncDecSpeedSPL6.Visible = false;
      this.btn_IncDecSpeedSPL6.Warned = false;
      // 
      // lbl_Splicer6Roll_B_Active
      // 
      this.lbl_Splicer6Roll_B_Active.AutoSize = true;
      this.lbl_Splicer6Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer6Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer6Roll_B_Active.Location = new System.Drawing.Point(471, 236);
      this.lbl_Splicer6Roll_B_Active.Name = "lbl_Splicer6Roll_B_Active";
      this.lbl_Splicer6Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer6Roll_B_Active.TabIndex = 461;
      this.lbl_Splicer6Roll_B_Active.Text = "        ";
      this.lbl_Splicer6Roll_B_Active.Visible = false;
      // 
      // lbl_Splicer6Roll_A_Active
      // 
      this.lbl_Splicer6Roll_A_Active.AutoSize = true;
      this.lbl_Splicer6Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer6Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer6Roll_A_Active.Location = new System.Drawing.Point(447, 236);
      this.lbl_Splicer6Roll_A_Active.Name = "lbl_Splicer6Roll_A_Active";
      this.lbl_Splicer6Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer6Roll_A_Active.TabIndex = 462;
      this.lbl_Splicer6Roll_A_Active.Text = "        ";
      this.lbl_Splicer6Roll_A_Active.Visible = false;
      // 
      // pct_notReadyToSplice_6
      // 
      this.pct_notReadyToSplice_6.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_6.Location = new System.Drawing.Point(469, 248);
      this.pct_notReadyToSplice_6.Name = "pct_notReadyToSplice_6";
      this.pct_notReadyToSplice_6.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_6.TabIndex = 463;
      this.pct_notReadyToSplice_6.TabStop = false;
      this.pct_notReadyToSplice_6.Visible = false;
      // 
      // nud_S6Meters
      // 
      this.nud_S6Meters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_S6Meters.DecimalPlaces = 0;
      this.nud_S6Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S6Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S6Meters.ImagePassword = null;
      this.nud_S6Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S6Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S6Meters.Location = new System.Drawing.Point(422, 161);
      this.nud_S6Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S6Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S6Meters.Name = "nud_S6Meters";
      this.nud_S6Meters.ReadOnly = true;
      this.nud_S6Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S6Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S6Meters.TabIndex = 464;
      this.nud_S6Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S6Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S6Meters.TranslateText = false;
      this.nud_S6Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S6Meters.UnitMeasureVisible = false;
      this.nud_S6Meters.Value = "325000000";
      this.nud_S6Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S6Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_S6Meters.Visible = false;
      // 
      // AutoManS6
      // 
      this.AutoManS6.Active = false;
      this.AutoManS6.Alarmed = false;
      this.AutoManS6.Auto = false;
      this.AutoManS6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS6.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS6.FillBackColorWithImage = false;
      this.AutoManS6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS6.FramePresent = false;
      this.AutoManS6.HighLightEnable = false;
      this.AutoManS6.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS6.ImagePassword = null;
      this.AutoManS6.Inemergency = false;
      this.AutoManS6.InLocal = false;
      this.AutoManS6.IsButton = false;
      this.AutoManS6.IsForceToInvisible = false;
      this.AutoManS6.Location = new System.Drawing.Point(475, 245);
      this.AutoManS6.Name = "AutoManS6";
      this.AutoManS6.OnLine = false;
      this.AutoManS6.ShowImage = true;
      this.AutoManS6.ShowText = false;
      this.AutoManS6.Size = new System.Drawing.Size(30, 23);
      this.AutoManS6.Switching = false;
      this.AutoManS6.TabIndex = 465;
      this.AutoManS6.TranslateText = false;
      this.AutoManS6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS6.UserDefinedActiveImage")));
      this.AutoManS6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS6.UserDefinedImage")));
      this.AutoManS6.Visible = false;
      this.AutoManS6.Warned = false;
      // 
      // pct_SPL6NotRdyRun
      // 
      this.pct_SPL6NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL6NotRdyRun.Image")));
      this.pct_SPL6NotRdyRun.Location = new System.Drawing.Point(437, 248);
      this.pct_SPL6NotRdyRun.Name = "pct_SPL6NotRdyRun";
      this.pct_SPL6NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL6NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL6NotRdyRun.TabIndex = 466;
      this.pct_SPL6NotRdyRun.TabStop = false;
      this.pct_SPL6NotRdyRun.Visible = false;
      // 
      // SingleFacer3
      // 
      this.SingleFacer3.Active = false;
      this.SingleFacer3.Alarmed = false;
      this.SingleFacer3.Auto = false;
      this.SingleFacer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SingleFacer3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SingleFacer3.FillBackColorWithImage = false;
      this.SingleFacer3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.SingleFacer3.FramePresent = false;
      this.SingleFacer3.HighLightEnable = false;
      this.SingleFacer3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SingleFacer3.ImagePassword = null;
      this.SingleFacer3.Inemergency = false;
      this.SingleFacer3.InLocal = false;
      this.SingleFacer3.IsButton = true;
      this.SingleFacer3.IsForceToInvisible = false;
      this.SingleFacer3.Location = new System.Drawing.Point(361, 185);
      this.SingleFacer3.Name = "SingleFacer3";
      this.SingleFacer3.OnLine = false;
      this.SingleFacer3.ShowImage = true;
      this.SingleFacer3.ShowText = false;
      this.SingleFacer3.Size = new System.Drawing.Size(72, 60);
      this.SingleFacer3.Switching = false;
      this.SingleFacer3.TabIndex = 467;
      this.SingleFacer3.TranslateText = false;
      this.SingleFacer3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SingleFacer3.UserDefinedActiveImage")));
      this.SingleFacer3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SingleFacer3.UserDefinedImage")));
      this.SingleFacer3.Visible = false;
      this.SingleFacer3.Warned = false;
      this.SingleFacer3.Click += new System.EventHandler(this.SingleFacer3_Click);
      // 
      // lbl_SF3Cartridge
      // 
      this.lbl_SF3Cartridge.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SF3Cartridge.ForeColor = System.Drawing.Color.Blue;
      this.lbl_SF3Cartridge.Location = new System.Drawing.Point(381, 159);
      this.lbl_SF3Cartridge.Name = "lbl_SF3Cartridge";
      this.lbl_SF3Cartridge.Size = new System.Drawing.Size(39, 28);
      this.lbl_SF3Cartridge.TabIndex = 468;
      this.lbl_SF3Cartridge.Text = "-";
      this.lbl_SF3Cartridge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SF3Cartridge.Visible = false;
      // 
      // btn_IncDecSpeedSF3
      // 
      this.btn_IncDecSpeedSF3.Active = true;
      this.btn_IncDecSpeedSF3.Alarmed = false;
      this.btn_IncDecSpeedSF3.Auto = true;
      this.btn_IncDecSpeedSF3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSF3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSF3.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSF3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSF3.FramePresent = false;
      this.btn_IncDecSpeedSF3.HighLightEnable = false;
      this.btn_IncDecSpeedSF3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSF3.ImagePassword = null;
      this.btn_IncDecSpeedSF3.Inemergency = false;
      this.btn_IncDecSpeedSF3.InLocal = true;
      this.btn_IncDecSpeedSF3.IsButton = false;
      this.btn_IncDecSpeedSF3.IsForceToInvisible = false;
      this.btn_IncDecSpeedSF3.Location = new System.Drawing.Point(360, 245);
      this.btn_IncDecSpeedSF3.Name = "btn_IncDecSpeedSF3";
      this.btn_IncDecSpeedSF3.OnLine = true;
      this.btn_IncDecSpeedSF3.ShowImage = true;
      this.btn_IncDecSpeedSF3.ShowText = false;
      this.btn_IncDecSpeedSF3.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSF3.Switching = false;
      this.btn_IncDecSpeedSF3.TabIndex = 469;
      this.btn_IncDecSpeedSF3.TranslateText = false;
      this.btn_IncDecSpeedSF3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSF3.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSF3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSF3.UserDefinedImage")));
      this.btn_IncDecSpeedSF3.Visible = false;
      this.btn_IncDecSpeedSF3.Warned = false;
      // 
      // pct_SF3NotRdyRun
      // 
      this.pct_SF3NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SF3NotRdyRun.Image")));
      this.pct_SF3NotRdyRun.Location = new System.Drawing.Point(365, 248);
      this.pct_SF3NotRdyRun.Name = "pct_SF3NotRdyRun";
      this.pct_SF3NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SF3NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SF3NotRdyRun.TabIndex = 470;
      this.pct_SF3NotRdyRun.TabStop = false;
      this.pct_SF3NotRdyRun.Visible = false;
      // 
      // nud_S7Meters
      // 
      this.nud_S7Meters.DecimalPlaces = 0;
      this.nud_S7Meters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.nud_S7Meters.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S7Meters.ImagePassword = null;
      this.nud_S7Meters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_S7Meters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_S7Meters.Location = new System.Drawing.Point(278, 161);
      this.nud_S7Meters.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
      this.nud_S7Meters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_S7Meters.Name = "nud_S7Meters";
      this.nud_S7Meters.ReadOnly = true;
      this.nud_S7Meters.Size = new System.Drawing.Size(94, 24);
      this.nud_S7Meters.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_S7Meters.TabIndex = 471;
      this.nud_S7Meters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S7Meters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S7Meters.TranslateText = false;
      this.nud_S7Meters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_S7Meters.UnitMeasureVisible = false;
      this.nud_S7Meters.Value = "325000000";
      this.nud_S7Meters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_S7Meters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_S7Meters.Visible = false;
      // 
      // Splicer7
      // 
      this.Splicer7.Active = false;
      this.Splicer7.Alarmed = false;
      this.Splicer7.Auto = false;
      this.Splicer7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Splicer7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Splicer7.FillBackColorWithImage = false;
      this.Splicer7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Splicer7.FramePresent = false;
      this.Splicer7.HighLightEnable = false;
      this.Splicer7.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Splicer7.ImagePassword = null;
      this.Splicer7.Inemergency = false;
      this.Splicer7.InLocal = false;
      this.Splicer7.IsButton = true;
      this.Splicer7.IsForceToInvisible = false;
      this.Splicer7.Location = new System.Drawing.Point(289, 185);
      this.Splicer7.Name = "Splicer7";
      this.Splicer7.OnLine = false;
      this.Splicer7.ShowImage = true;
      this.Splicer7.ShowText = false;
      this.Splicer7.Size = new System.Drawing.Size(72, 60);
      this.Splicer7.Switching = false;
      this.Splicer7.TabIndex = 472;
      this.Splicer7.TranslateText = false;
      this.Splicer7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Splicer7.UserDefinedActiveImage")));
      this.Splicer7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Splicer7.UserDefinedImage")));
      this.Splicer7.Visible = false;
      this.Splicer7.Warned = false;
      this.Splicer7.Click += new System.EventHandler(this.Splicer7_Click);
      // 
      // lbl_Splicer7Roll_A_Active
      // 
      this.lbl_Splicer7Roll_A_Active.AutoSize = true;
      this.lbl_Splicer7Roll_A_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer7Roll_A_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer7Roll_A_Active.Location = new System.Drawing.Point(327, 236);
      this.lbl_Splicer7Roll_A_Active.Name = "lbl_Splicer7Roll_A_Active";
      this.lbl_Splicer7Roll_A_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer7Roll_A_Active.TabIndex = 473;
      this.lbl_Splicer7Roll_A_Active.Text = "        ";
      this.lbl_Splicer7Roll_A_Active.Visible = false;
      // 
      // lbl_Splicer7Roll_B_Active
      // 
      this.lbl_Splicer7Roll_B_Active.AutoSize = true;
      this.lbl_Splicer7Roll_B_Active.BackColor = System.Drawing.Color.Green;
      this.lbl_Splicer7Roll_B_Active.Font = new System.Drawing.Font("Arial Unicode MS", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Splicer7Roll_B_Active.Location = new System.Drawing.Point(303, 236);
      this.lbl_Splicer7Roll_B_Active.Name = "lbl_Splicer7Roll_B_Active";
      this.lbl_Splicer7Roll_B_Active.Size = new System.Drawing.Size(21, 9);
      this.lbl_Splicer7Roll_B_Active.TabIndex = 474;
      this.lbl_Splicer7Roll_B_Active.Text = "        ";
      this.lbl_Splicer7Roll_B_Active.Visible = false;
      // 
      // btn_IncDecSpeedSPL7
      // 
      this.btn_IncDecSpeedSPL7.Active = true;
      this.btn_IncDecSpeedSPL7.Alarmed = false;
      this.btn_IncDecSpeedSPL7.Auto = true;
      this.btn_IncDecSpeedSPL7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSPL7.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSPL7.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSPL7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSPL7.FramePresent = false;
      this.btn_IncDecSpeedSPL7.HighLightEnable = false;
      this.btn_IncDecSpeedSPL7.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSPL7.ImagePassword = null;
      this.btn_IncDecSpeedSPL7.Inemergency = false;
      this.btn_IncDecSpeedSPL7.InLocal = true;
      this.btn_IncDecSpeedSPL7.IsButton = false;
      this.btn_IncDecSpeedSPL7.IsForceToInvisible = false;
      this.btn_IncDecSpeedSPL7.Location = new System.Drawing.Point(289, 245);
      this.btn_IncDecSpeedSPL7.Name = "btn_IncDecSpeedSPL7";
      this.btn_IncDecSpeedSPL7.OnLine = true;
      this.btn_IncDecSpeedSPL7.ShowImage = true;
      this.btn_IncDecSpeedSPL7.ShowText = false;
      this.btn_IncDecSpeedSPL7.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSPL7.Switching = false;
      this.btn_IncDecSpeedSPL7.TabIndex = 475;
      this.btn_IncDecSpeedSPL7.TranslateText = false;
      this.btn_IncDecSpeedSPL7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL7.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSPL7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSPL7.UserDefinedImage")));
      this.btn_IncDecSpeedSPL7.Visible = false;
      this.btn_IncDecSpeedSPL7.Warned = false;
      // 
      // pct_notReadyToSplice_7
      // 
      this.pct_notReadyToSplice_7.Image = global::SyncroC.Properties.Resources.Not_Ready_To_Splice;
      this.pct_notReadyToSplice_7.Location = new System.Drawing.Point(326, 248);
      this.pct_notReadyToSplice_7.Name = "pct_notReadyToSplice_7";
      this.pct_notReadyToSplice_7.Size = new System.Drawing.Size(29, 18);
      this.pct_notReadyToSplice_7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_notReadyToSplice_7.TabIndex = 476;
      this.pct_notReadyToSplice_7.TabStop = false;
      this.pct_notReadyToSplice_7.Visible = false;
      // 
      // AutoManS7
      // 
      this.AutoManS7.Active = false;
      this.AutoManS7.Alarmed = false;
      this.AutoManS7.Auto = false;
      this.AutoManS7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.AutoManS7.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.AutoManS7.FillBackColorWithImage = false;
      this.AutoManS7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AutoManS7.FramePresent = false;
      this.AutoManS7.HighLightEnable = false;
      this.AutoManS7.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.AutoManS7.ImagePassword = null;
      this.AutoManS7.Inemergency = false;
      this.AutoManS7.InLocal = false;
      this.AutoManS7.IsButton = false;
      this.AutoManS7.IsForceToInvisible = false;
      this.AutoManS7.Location = new System.Drawing.Point(331, 245);
      this.AutoManS7.Name = "AutoManS7";
      this.AutoManS7.OnLine = false;
      this.AutoManS7.ShowImage = true;
      this.AutoManS7.ShowText = false;
      this.AutoManS7.Size = new System.Drawing.Size(30, 23);
      this.AutoManS7.Switching = false;
      this.AutoManS7.TabIndex = 477;
      this.AutoManS7.TranslateText = false;
      this.AutoManS7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AutoManS7.UserDefinedActiveImage")));
      this.AutoManS7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AutoManS7.UserDefinedImage")));
      this.AutoManS7.Visible = false;
      this.AutoManS7.Warned = false;
      // 
      // pct_SPL7NotRdyRun
      // 
      this.pct_SPL7NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SPL7NotRdyRun.Image")));
      this.pct_SPL7NotRdyRun.Location = new System.Drawing.Point(293, 248);
      this.pct_SPL7NotRdyRun.Name = "pct_SPL7NotRdyRun";
      this.pct_SPL7NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SPL7NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SPL7NotRdyRun.TabIndex = 478;
      this.pct_SPL7NotRdyRun.TabStop = false;
      this.pct_SPL7NotRdyRun.Visible = false;
      // 
      // pct_Bridge3
      // 
      this.pct_Bridge3.Image = ((System.Drawing.Image)(resources.GetObject("pct_Bridge3.Image")));
      this.pct_Bridge3.Location = new System.Drawing.Point(394, 73);
      this.pct_Bridge3.Name = "pct_Bridge3";
      this.pct_Bridge3.Size = new System.Drawing.Size(224, 87);
      this.pct_Bridge3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pct_Bridge3.TabIndex = 479;
      this.pct_Bridge3.TabStop = false;
      this.pct_Bridge3.Visible = false;
      // 
      // btn_SYCIncDecSpeedSF3
      // 
      this.btn_SYCIncDecSpeedSF3.Active = true;
      this.btn_SYCIncDecSpeedSF3.Alarmed = false;
      this.btn_SYCIncDecSpeedSF3.Auto = true;
      this.btn_SYCIncDecSpeedSF3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecSpeedSF3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecSpeedSF3.FillBackColorWithImage = false;
      this.btn_SYCIncDecSpeedSF3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecSpeedSF3.FramePresent = false;
      this.btn_SYCIncDecSpeedSF3.HighLightEnable = false;
      this.btn_SYCIncDecSpeedSF3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecSpeedSF3.ImagePassword = null;
      this.btn_SYCIncDecSpeedSF3.Inemergency = false;
      this.btn_SYCIncDecSpeedSF3.InLocal = true;
      this.btn_SYCIncDecSpeedSF3.IsButton = false;
      this.btn_SYCIncDecSpeedSF3.IsForceToInvisible = false;
      this.btn_SYCIncDecSpeedSF3.Location = new System.Drawing.Point(394, 63);
      this.btn_SYCIncDecSpeedSF3.Name = "btn_SYCIncDecSpeedSF3";
      this.btn_SYCIncDecSpeedSF3.OnLine = true;
      this.btn_SYCIncDecSpeedSF3.ShowImage = true;
      this.btn_SYCIncDecSpeedSF3.ShowText = false;
      this.btn_SYCIncDecSpeedSF3.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecSpeedSF3.Switching = false;
      this.btn_SYCIncDecSpeedSF3.TabIndex = 480;
      this.btn_SYCIncDecSpeedSF3.TranslateText = false;
      this.btn_SYCIncDecSpeedSF3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedSF3.UserDefinedActiveImage")));
      this.btn_SYCIncDecSpeedSF3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecSpeedSF3.UserDefinedImage")));
      this.btn_SYCIncDecSpeedSF3.Visible = false;
      this.btn_SYCIncDecSpeedSF3.Warned = false;
      // 
      // btn_SYCIncDecBandSpeedSF3
      // 
      this.btn_SYCIncDecBandSpeedSF3.Active = true;
      this.btn_SYCIncDecBandSpeedSF3.Alarmed = false;
      this.btn_SYCIncDecBandSpeedSF3.Auto = true;
      this.btn_SYCIncDecBandSpeedSF3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SYCIncDecBandSpeedSF3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_SYCIncDecBandSpeedSF3.FillBackColorWithImage = false;
      this.btn_SYCIncDecBandSpeedSF3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_SYCIncDecBandSpeedSF3.FramePresent = false;
      this.btn_SYCIncDecBandSpeedSF3.HighLightEnable = false;
      this.btn_SYCIncDecBandSpeedSF3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_SYCIncDecBandSpeedSF3.ImagePassword = null;
      this.btn_SYCIncDecBandSpeedSF3.Inemergency = false;
      this.btn_SYCIncDecBandSpeedSF3.InLocal = true;
      this.btn_SYCIncDecBandSpeedSF3.IsButton = false;
      this.btn_SYCIncDecBandSpeedSF3.IsForceToInvisible = false;
      this.btn_SYCIncDecBandSpeedSF3.Location = new System.Drawing.Point(394, 84);
      this.btn_SYCIncDecBandSpeedSF3.Name = "btn_SYCIncDecBandSpeedSF3";
      this.btn_SYCIncDecBandSpeedSF3.OnLine = true;
      this.btn_SYCIncDecBandSpeedSF3.ShowImage = true;
      this.btn_SYCIncDecBandSpeedSF3.ShowText = false;
      this.btn_SYCIncDecBandSpeedSF3.Size = new System.Drawing.Size(30, 23);
      this.btn_SYCIncDecBandSpeedSF3.Switching = false;
      this.btn_SYCIncDecBandSpeedSF3.TabIndex = 481;
      this.btn_SYCIncDecBandSpeedSF3.TranslateText = false;
      this.btn_SYCIncDecBandSpeedSF3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedSF3.UserDefinedActiveImage")));
      this.btn_SYCIncDecBandSpeedSF3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_SYCIncDecBandSpeedSF3.UserDefinedImage")));
      this.btn_SYCIncDecBandSpeedSF3.Visible = false;
      this.btn_SYCIncDecBandSpeedSF3.Warned = false;
      // 
      // pct_BridgeError3
      // 
      this.pct_BridgeError3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BridgeError3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_BridgeError3.Image = global::SyncroC.Properties.Resources.Esclamativo;
      this.pct_BridgeError3.Location = new System.Drawing.Point(431, 118);
      this.pct_BridgeError3.Name = "pct_BridgeError3";
      this.pct_BridgeError3.Size = new System.Drawing.Size(32, 32);
      this.pct_BridgeError3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_BridgeError3.TabIndex = 482;
      this.pct_BridgeError3.TabStop = false;
      this.pct_BridgeError3.Visible = false;
      this.pct_BridgeError3.Click += new System.EventHandler(this.pct_BridgeError3_Click);
      // 
      // lbl_zone3Active
      // 
      this.lbl_zone3Active.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_zone3Active.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_zone3Active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.lbl_zone3Active.Location = new System.Drawing.Point(486, 84);
      this.lbl_zone3Active.Name = "lbl_zone3Active";
      this.lbl_zone3Active.Size = new System.Drawing.Size(57, 47);
      this.lbl_zone3Active.TabIndex = 483;
      this.lbl_zone3Active.Text = "X";
      this.lbl_zone3Active.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_zone3Active.Visible = false;
      // 
      // ToggleAutoManZone3
      // 
      this.ToggleAutoManZone3.Active = false;
      this.ToggleAutoManZone3.Alarmed = false;
      this.ToggleAutoManZone3.Auto = false;
      this.ToggleAutoManZone3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManZone3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManZone3.FillBackColorWithImage = false;
      this.ToggleAutoManZone3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManZone3.FramePresent = true;
      this.ToggleAutoManZone3.HighLightEnable = false;
      this.ToggleAutoManZone3.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManZone3.ImagePassword = null;
      this.ToggleAutoManZone3.Inemergency = false;
      this.ToggleAutoManZone3.InLocal = false;
      this.ToggleAutoManZone3.IsButton = true;
      this.ToggleAutoManZone3.IsForceToInvisible = false;
      this.ToggleAutoManZone3.Location = new System.Drawing.Point(559, 86);
      this.ToggleAutoManZone3.Name = "ToggleAutoManZone3";
      this.ToggleAutoManZone3.OnLine = false;
      this.ToggleAutoManZone3.ShowImage = true;
      this.ToggleAutoManZone3.ShowText = false;
      this.ToggleAutoManZone3.Size = new System.Drawing.Size(57, 47);
      this.ToggleAutoManZone3.Switching = false;
      this.ToggleAutoManZone3.TabIndex = 484;
      this.ToggleAutoManZone3.TranslateText = false;
      this.ToggleAutoManZone3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManZone3.UserDefinedActiveImage")));
      this.ToggleAutoManZone3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManZone3.UserDefinedImage")));
      this.ToggleAutoManZone3.Visible = false;
      this.ToggleAutoManZone3.Warned = false;
      this.ToggleAutoManZone3.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // prb_BridgeContents3
      // 
      this.prb_BridgeContents3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.prb_BridgeContents3.BarStyle = SyncroC.Controls.BarStyles.Classic;
      this.prb_BridgeContents3.color = SyncroC.Controls.Colors.SyncroColor.White;
      this.prb_BridgeContents3.Font = new System.Drawing.Font("Arial Unicode MS", 15F, System.Drawing.FontStyle.Bold);
      this.prb_BridgeContents3.ForeColor = System.Drawing.Color.Black;
      this.prb_BridgeContents3.Location = new System.Drawing.Point(455, 57);
      this.prb_BridgeContents3.Name = "prb_BridgeContents3";
      this.prb_BridgeContents3.Percentage = 80;
      this.prb_BridgeContents3.PercentageVisible = false;
      this.prb_BridgeContents3.Size = new System.Drawing.Size(161, 12);
      this.prb_BridgeContents3.Style = SyncroC.Controls.ProgBarStyles.VerySmall;
      this.prb_BridgeContents3.TabIndex = 485;
      this.prb_BridgeContents3.TranslateText = false;
      this.prb_BridgeContents3.Visible = false;
      // 
      // lbl_BridgeContents3
      // 
      this.lbl_BridgeContents3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.lbl_BridgeContents3.Location = new System.Drawing.Point(610, 50);
      this.lbl_BridgeContents3.Name = "lbl_BridgeContents3";
      this.lbl_BridgeContents3.Size = new System.Drawing.Size(55, 23);
      this.lbl_BridgeContents3.TabIndex = 486;
      this.lbl_BridgeContents3.Text = "999";
      this.lbl_BridgeContents3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_BridgeContents3.Visible = false;
      // 
      // nud_SpeedZone3
      // 
      this.nud_SpeedZone3.DecimalPlaces = 0;
      this.nud_SpeedZone3.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SpeedZone3.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone3.ImagePassword = null;
      this.nud_SpeedZone3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SpeedZone3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_SpeedZone3.Location = new System.Drawing.Point(475, 87);
      this.nud_SpeedZone3.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.nud_SpeedZone3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SpeedZone3.Name = "nud_SpeedZone3";
      this.nud_SpeedZone3.ReadOnly = true;
      this.nud_SpeedZone3.Size = new System.Drawing.Size(82, 33);
      this.nud_SpeedZone3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SpeedZone3.TabIndex = 487;
      this.nud_SpeedZone3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone3.TranslateText = false;
      this.nud_SpeedZone3.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_SpeedZone3.UnitMeasureVisible = false;
      this.nud_SpeedZone3.Value = "100000";
      this.nud_SpeedZone3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SpeedZone3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_SpeedZone3.Visible = false;
      // 
      // Frm_StatusBarWetEnd
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(1280, 286);
      this.Controls.Add(this.lbl_BridgeContents3);
      this.Controls.Add(this.prb_BridgeContents3);
      this.Controls.Add(this.ToggleAutoManZone3);
      this.Controls.Add(this.lbl_zone3Active);
      this.Controls.Add(this.pct_BridgeError3);
      this.Controls.Add(this.btn_SYCIncDecBandSpeedSF3);
      this.Controls.Add(this.btn_SYCIncDecSpeedSF3);
      this.Controls.Add(this.pct_Bridge3);
      this.Controls.Add(this.pct_notReadyToSplice_7);
      this.Controls.Add(this.btn_IncDecSpeedSPL7);
      this.Controls.Add(this.lbl_Splicer7Roll_B_Active);
      this.Controls.Add(this.lbl_Splicer7Roll_A_Active);
      this.Controls.Add(this.Splicer7);
      this.Controls.Add(this.nud_S7Meters);
      this.Controls.Add(this.btn_IncDecSpeedSF3);
      this.Controls.Add(this.lbl_SF3Cartridge);
      this.Controls.Add(this.SingleFacer3);
      this.Controls.Add(this.AutoManS6);
      this.Controls.Add(this.nud_S6Meters);
      this.Controls.Add(this.lbl_Splicer6Roll_A_Active);
      this.Controls.Add(this.grp_now_percheMiSonoFermato);
      this.Controls.Add(this.pct_notReadyToSplice_1);
      this.Controls.Add(this.pct_notReadyToSplice_2);
      this.Controls.Add(this.pct_notReadyToSplice_3);
      this.Controls.Add(this.pct_notReadyToSplice_4);
      this.Controls.Add(this.pct_notReadyToSplice_5);
      this.Controls.Add(this.lbl_Splicer1Roll_A_Active);
      this.Controls.Add(this.lbl_Splicer1Roll_B_Active);
      this.Controls.Add(this.lbl_Splicer2Roll_B_Active);
      this.Controls.Add(this.lbl_Splicer2Roll_A_Active);
      this.Controls.Add(this.lbl_Splicer3Roll_A_Active);
      this.Controls.Add(this.lbl_Splicer3Roll_B_Active);
      this.Controls.Add(this.lbl_Splicer4Roll_B_Active);
      this.Controls.Add(this.lbl_Splicer4Roll_A_Active);
      this.Controls.Add(this.lbl_Splicer5Roll_A_Active);
      this.Controls.Add(this.lbl_Splicer5Roll_B_Active);
      this.Controls.Add(this.ttl_SimulationTitle);
      this.Controls.Add(this.pct_WETAOCStatus);
      this.Controls.Add(this.pct_BridgeError2);
      this.Controls.Add(this.pct_BridgeError1);
      this.Controls.Add(this.lbl_BridgeContents2);
      this.Controls.Add(this.btn_SYCIncDecBandSpeedDB);
      this.Controls.Add(this.btn_SYCIncDecSpeedDB);
      this.Controls.Add(this.btn_SYCIncDecBandSpeedSF1);
      this.Controls.Add(this.btn_SYCIncDecSpeedSF1);
      this.Controls.Add(this.btn_SYCIncDecSpeedSF2);
      this.Controls.Add(this.AutoManS1);
      this.Controls.Add(this.AutoManS2);
      this.Controls.Add(this.AutoManS3);
      this.Controls.Add(this.AutoManS4);
      this.Controls.Add(this.AutoManS5);
      this.Controls.Add(this.prb_BridgeContents1);
      this.Controls.Add(this.prb_BridgeContents2);
      this.Controls.Add(this.lbl_zone2Active);
      this.Controls.Add(this.lbl_zone1Active);
      this.Controls.Add(this.lbl_SF1Cartridge);
      this.Controls.Add(this.lbl_SF2Cartridge);
      this.Controls.Add(this.nud_SpeedZone2);
      this.Controls.Add(this.nud_SpeedZone1);
      this.Controls.Add(this.nud_SpeedLine);
      this.Controls.Add(this.ttl_WetEndTitle);
      this.Controls.Add(this.btn_IncDecSpeedPHT);
      this.Controls.Add(this.pct_PHTNotRdyRun);
      this.Controls.Add(this.PreHeaterTern);
      this.Controls.Add(this.btn_IncDecSpeedDB);
      this.Controls.Add(this.btn_IncDecSpeedGU);
      this.Controls.Add(this.btn_IncDecSpeedSPL1);
      this.Controls.Add(this.btn_IncDecSpeedSPL2);
      this.Controls.Add(this.btn_IncDecSpeedSF1);
      this.Controls.Add(this.btn_IncDecSpeedSPL3);
      this.Controls.Add(this.btn_IncDecSpeedSPL4);
      this.Controls.Add(this.btn_IncDecSpeedSF2);
      this.Controls.Add(this.btn_IncDecSpeedSPL5);
      this.Controls.Add(this.pct_SPL5NotRdyRun);
      this.Controls.Add(this.pct_SF2NotRdyRun);
      this.Controls.Add(this.pct_SPL4NotRdyRun);
      this.Controls.Add(this.pct_SPL3NotRdyRun);
      this.Controls.Add(this.pct_SF1NotRdyRun);
      this.Controls.Add(this.pct_SPL2NotRdyRun);
      this.Controls.Add(this.pct_SPL1NotRdyRun);
      this.Controls.Add(this.pct_GUNotRdyRun);
      this.Controls.Add(this.gdv_Programs);
      this.Controls.Add(this.gdv_Programs3SF);
      this.Controls.Add(this.tmt_AOCTimeLeft);
      this.Controls.Add(this.tlb_WetEndProgram);
      this.Controls.Add(this.pct_DBNotRdyRun);
      this.Controls.Add(this.WetEnd);
      this.Controls.Add(this.DoubleBacker);
      this.Controls.Add(this.GlueUnit);
      this.Controls.Add(this.Splicer1);
      this.Controls.Add(this.Splicer2);
      this.Controls.Add(this.SingleFacer1);
      this.Controls.Add(this.Splicer3);
      this.Controls.Add(this.Splicer4);
      this.Controls.Add(this.SingleFacer2);
      this.Controls.Add(this.Splicer5);
      this.Controls.Add(this.prb_WetEnd);
      this.Controls.Add(this.ttl_DryEndTitle);
      this.Controls.Add(this.lbl_WetEndMode);
      this.Controls.Add(this.ToggleAutoManZone2);
      this.Controls.Add(this.ToggleAutoManWetEnd);
      this.Controls.Add(this.lbl_zoneDBActive);
      this.Controls.Add(this.ToggleAutoManZone1);
      this.Controls.Add(this.pct_Bridge1);
      this.Controls.Add(this.pnl_clickExtension);
      this.Controls.Add(this.pnl_clickExtension2);
      this.Controls.Add(this.lbl_BridgeContents1);
      this.Controls.Add(this.nud_S3Meters);
      this.Controls.Add(this.nud_S4Meters);
      this.Controls.Add(this.nud_S2Meters);
      this.Controls.Add(this.nud_S1Meters);
      this.Controls.Add(this.nud_S5Meters);
      this.Controls.Add(this.btn_SYCIncDecBandSpeedSF2);
      this.Controls.Add(this.pct_Bridge2);
      this.Controls.Add(this.btn_IncDecSpeedWEConsole);
      this.Controls.Add(this.Splicer6);
      this.Controls.Add(this.btn_IncDecSpeedSPL6);
      this.Controls.Add(this.lbl_Splicer6Roll_B_Active);
      this.Controls.Add(this.pct_notReadyToSplice_6);
      this.Controls.Add(this.pct_SPL6NotRdyRun);
      this.Controls.Add(this.pct_SF3NotRdyRun);
      this.Controls.Add(this.AutoManS7);
      this.Controls.Add(this.pct_SPL7NotRdyRun);
      this.Controls.Add(this.nud_SpeedZone3);
      this.Controls.Add(this.tlb_WetEndMetersLeft);
      this.DesignGridHorizontalLines = 3;
      this.DesignGridVerticalLines = 6;
      this.Location = new System.Drawing.Point(1023, 175);
      this.Name = "Frm_StatusBarWetEnd";
      this.Text = "Frm_StatusBarWetEnd";
      this.pnl_clickExtension.ResumeLayout(false);
      this.pnl_clickExtension2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_5)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_WETAOCStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BridgeError2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BridgeError1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_PHTNotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL5NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SF2NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL4NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL3NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SF1NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL2NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL1NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_GUNotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DBNotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Bridge1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Bridge2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_1)).EndInit();
      this.grp_now_percheMiSonoFermato.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_now)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now_causa_percheMiSonoFermato)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_6)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL6NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SF3NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_notReadyToSplice_7)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SPL7NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Bridge3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BridgeError3)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		public override void CorrugatorDirectionLeft()
		{
      int leftLimit = tlb_WetEndProgram.Location.X;
      int rightLimit = DoubleBacker.Location.X + DoubleBacker.Width;

      HorizontalLocationMirror(this.tlb_WetEndProgram, leftLimit, rightLimit);
      HorizontalLocationMirror(this.tlb_WetEndMetersLeft, leftLimit, rightLimit);
      HorizontalLocationMirror(this.tmt_AOCTimeLeft, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttl_SimulationTitle, leftLimit, rightLimit);
      //RFAZZI_FI20/05/2010_1. Aggiunta visualizzazione in barra di stato Wet della macchina che ha fermato la linea. Invertiti anche i campi di Metri e Tempo a Cambio Wet. 
      HorizontalLocationMirror(this.grp_now_percheMiSonoFermato, leftLimit, rightLimit);

      //wet End supervisor
			HorizontalLocationMirror(this.WetEnd, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedWEConsole, leftLimit, rightLimit);
			//DoubleBacker
			HorizontalLocationMirror(DoubleBacker, leftLimit, rightLimit);
			DoubleBacker.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_DBNotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedDB, leftLimit, rightLimit);
			HorizontalLocationMirror(this.lbl_zoneDBActive, leftLimit, rightLimit);
			//Splicer1
			HorizontalLocationMirror(Splicer1, leftLimit, rightLimit);
			Splicer1.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SPL1NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSPL1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_S1Meters, leftLimit, rightLimit);
			HorizontalLocationMirror(this.AutoManS1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_1, leftLimit, rightLimit);
			//Splicer2
			HorizontalLocationMirror(Splicer2, leftLimit, rightLimit);
			Splicer2.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SPL2NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSPL2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_S2Meters, leftLimit, rightLimit);
			HorizontalLocationMirror(this.AutoManS2, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_2, leftLimit, rightLimit);
			//Splicer3
			HorizontalLocationMirror(Splicer3, leftLimit, rightLimit);
			Splicer3.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SPL3NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSPL3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_S3Meters, leftLimit, rightLimit);
			HorizontalLocationMirror(this.AutoManS3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_3, leftLimit, rightLimit);
			//Splicer4
			HorizontalLocationMirror(Splicer4, leftLimit, rightLimit);
			Splicer4.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SPL4NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSPL4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_S4Meters, leftLimit, rightLimit);
			HorizontalLocationMirror(this.AutoManS4, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_4, leftLimit, rightLimit);
			//Splicer5
			HorizontalLocationMirror(Splicer5, leftLimit, rightLimit);
			Splicer5.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SPL5NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSPL5, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_S5Meters, leftLimit, rightLimit);
			HorizontalLocationMirror(this.AutoManS5, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_5, leftLimit, rightLimit);

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      //Splicer6
      HorizontalLocationMirror(Splicer6, leftLimit, rightLimit);
      Splicer6.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(pct_SPL6NotRdyRun, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_IncDecSpeedSPL6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.nud_S6Meters, leftLimit, rightLimit);
      HorizontalLocationMirror(this.AutoManS6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_6, leftLimit, rightLimit);

      //Splicer7
      HorizontalLocationMirror(Splicer7, leftLimit, rightLimit);
      Splicer7.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(pct_SPL7NotRdyRun, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_IncDecSpeedSPL7, leftLimit, rightLimit);
      HorizontalLocationMirror(this.nud_S7Meters, leftLimit, rightLimit);
      HorizontalLocationMirror(this.AutoManS7, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_notReadyToSplice_7, leftLimit, rightLimit);
      
      //SingleFacer1
			HorizontalLocationMirror(SingleFacer1, leftLimit, rightLimit);
			SingleFacer1.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SF1NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSF1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.lbl_SF1Cartridge, leftLimit, rightLimit);
			//SingleFacer2
			HorizontalLocationMirror(SingleFacer2, leftLimit, rightLimit);
			SingleFacer2.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SF2NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSF2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.lbl_SF2Cartridge, leftLimit, rightLimit);
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      //SingleFacer3
      HorizontalLocationMirror(SingleFacer3, leftLimit, rightLimit);
      SingleFacer3.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(pct_SF3NotRdyRun, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_IncDecSpeedSF3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_SF3Cartridge, leftLimit, rightLimit);
      //GlueUnit
			HorizontalLocationMirror(GlueUnit, leftLimit, rightLimit);
			GlueUnit.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_GUNotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedGU, leftLimit, rightLimit);
			//PHT
			HorizontalLocationMirror(PreHeaterTern, leftLimit, rightLimit);
			PreHeaterTern.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_PHTNotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedPHT, leftLimit, rightLimit);

			//leftLimit = gdv_Programs.Location.X;
			//rightLimit = ToggleAutoManWetEnd.Location.X + ToggleAutoManWetEnd.Width;

			HorizontalLocationMirror(this.gdv_Programs, leftLimit, rightLimit);
      //Bridge1
			HorizontalLocationMirror(this.lbl_BridgeContents1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.prb_BridgeContents1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_SYCIncDecSpeedSF1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_SYCIncDecBandSpeedSF1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.lbl_zone1Active, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ToggleAutoManZone1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_BridgeError1, leftLimit, rightLimit);
			this.pct_Bridge1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(this.pct_Bridge1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_SpeedZone1, leftLimit, rightLimit);
			//Bridge2
			HorizontalLocationMirror(this.lbl_BridgeContents2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.prb_BridgeContents2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_SYCIncDecSpeedSF2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_SYCIncDecBandSpeedSF2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.lbl_zone2Active, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ToggleAutoManZone2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_BridgeError2, leftLimit, rightLimit);
			this.pct_Bridge2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(this.pct_Bridge2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_SpeedZone2, leftLimit, rightLimit);

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      //Bridge3
      HorizontalLocationMirror(this.lbl_BridgeContents3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.prb_BridgeContents3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_SYCIncDecSpeedSF3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_SYCIncDecBandSpeedSF3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_zone3Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ToggleAutoManZone3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_BridgeError3, leftLimit, rightLimit);
      this.pct_Bridge3.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(this.pct_Bridge3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.nud_SpeedZone3, leftLimit, rightLimit);
      //Other
			HorizontalLocationMirror(this.ToggleAutoManWetEnd, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_SpeedLine, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_SYCIncDecBandSpeedDB, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_SYCIncDecSpeedDB, leftLimit, rightLimit);
      HorizontalLocationMirror(this.gdv_Programs3SF, leftLimit, rightLimit);
      //PMASSARO - MANANGEMENT OF ACTIVE ROLL FOR SPLICERS (Left side)
      HorizontalLocationMirror(this.lbl_Splicer1Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer1Roll_B_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer2Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer2Roll_B_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer3Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer3Roll_B_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer4Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer4Roll_B_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer5Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer5Roll_B_Active, leftLimit, rightLimit);
      
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      HorizontalLocationMirror(this.lbl_Splicer6Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer6Roll_B_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer7Roll_A_Active, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_Splicer7Roll_B_Active, leftLimit, rightLimit);
		}

		private void WetEnd_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F1]) AltF1();	
		}

		private void DoubleBacker_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F11]) AltF11();	
		}

		private void GlueUnit_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F10]) AltF10();	
		}

		private void PreHeaterTern_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F9]) AltF9();	
		}

		private void Splicer1_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F8]) AltF8();	
		}

		private void Splicer2_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F7]) AltF7();	
		}

		private void SingleFacer1_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F6]) AltF6();	
		}

		private void Splicer3_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F5]) AltF5();	
		}

		private void Splicer4_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F4]) AltF4();	
		}

		private void SingleFacer2_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F3]) AltF3();	
		}

		private void Splicer5_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F2]) AltF2();	
		}

		private void Bridge1_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F6]) AltShiftF6();
		}

		private void Bridge2_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F3]) AltShiftF3();
		}
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    private void Splicer6_Click(object sender, EventArgs e)
    {
      if (this.AltFKeyEnable[(int)FKEYS.F12]) AltF12();
    }

    private void SingleFacer3_Click(object sender, EventArgs e)
    {
      if (this.ShiftFKeyEnable[(int)FKEYS.F2]) AltShiftF2();
    }

    private void Splicer7_Click(object sender, EventArgs e)
    {
      if (this.ShiftFKeyEnable[(int)FKEYS.F1]) AltShiftF1();
    }
    private void Bridge3_Click(object sender, EventArgs e)
    {
      if (this.AltFKeyEnable[(int)FKEYS.F4]) AltShiftF4();
    }


    private void ttl_DryEndTitle_Click(object sender, EventArgs e)
    {
      if (Settings.getSettings().configuration.DryEndControl)
        askForPage(typeof(Frm_StatusBarDryEnd));
    }

		private void gdv_Programs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataRow dataRow = ((DataRowView)gdv_Programs.Rows[e.RowIndex].DataBoundItem).Row;

			if (this.gdv_Programs.Columns[e.ColumnIndex].Name == Order.WEL_PAPER_WIDTH
						&& e.Value != null)
			{
				try
				{
					e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Programs_CellFormatting - " + Order.WEL_PAPER_WIDTH + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}
		}

		private void gdv_Programs_DoubleClick(object sender, EventArgs e)
		{
			if (gdv_Programs.SelectedRows.Count == 1 &&
					DBManager.getDBManager().order.order.Tables[Order.WETEND_LIST].Rows.Count >= gdv_Programs.SelectedRows[0].Index)
			{
				Frm_OrdersInsSubGroup.indexOrderToShowWET = gdv_Programs.SelectedRows[0].Index;
			}
			askForPage(typeof(Frm_InsWetOrder));
		}

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    private void gdv_Programs3SF_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      DataRow dataRow = ((DataRowView)gdv_Programs3SF.Rows[e.RowIndex].DataBoundItem).Row;

      if (this.gdv_Programs3SF.Columns[e.ColumnIndex].Name == Order.WEL_PAPER_WIDTH
            && e.Value != null)
      {
        try
        {
          e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Programs3SF_CellFormatting - " + Order.WEL_PAPER_WIDTH + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
        }
      }
    }

    private void gdv_Programs3SF_DoubleClick(object sender, EventArgs e)
    {
      if (gdv_Programs3SF.SelectedRows.Count == 1 &&
          DBManager.getDBManager().order.order.Tables[Order.WETEND_LIST].Rows.Count >= gdv_Programs3SF.SelectedRows[0].Index)
      {
        Frm_OrdersInsSubGroup.indexOrderToShowWET = gdv_Programs3SF.SelectedRows[0].Index;
      }
      askForPage(typeof(Frm_InsWetOrder));
    }

    private void orderChangeTimeLeftCalculation(object sender, EventArgs e)
    {
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (!SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        int metersLeft = 0;
        int lineSpeed = 0;
        try
        {
          metersLeft = Convert.ToInt32(tlb_WetEndMetersLeft.Value);
        }
        catch (FormatException ex)
        {
          Logger.LogError(this, "orderChangeTimeLeftCalculation - format error converting metersLeft.", ex);
          tmt_AOCTimeLeft.Value = "err";
          return;
        }
        catch (OverflowException ex)
        {
          Logger.LogError(this, "orderChangeTimeLeftCalculation - overflow converting metersLeft.", ex);
          tmt_AOCTimeLeft.Value = "err";
          return;
        }

        try
        {
          lineSpeed = Convert.ToInt32(this.nud_SpeedLine.Value);
        }
        catch (FormatException ex)
        {
          Logger.LogError(this, "orderChangeTimeLeftCalculation - format error converting lineSpeed.", ex);
          tmt_AOCTimeLeft.Value = "err";
          return;
        }
        catch (OverflowException ex)
        {
          Logger.LogError(this, "orderChangeTimeLeftCalculation - overflow converting lineSpeed.", ex);
          tmt_AOCTimeLeft.Value = "err";
          return;
        }
        if (lineSpeed <= 0)
        {
          tmt_AOCTimeLeft.Value = " --- ";
        }
        else
        {
          int timeLeftSeconds = (metersLeft * 1000) / (lineSpeed / 60);
          tmt_AOCTimeLeft.Value = SyncroConverter.ConvertToFormattedTime(timeLeftSeconds);
        }

      }
      else
      {
        this.tlb_WetEndProgram.Visible = false;
      }
    }
		/// <summary>
		/// Called everytime there is a page change.
		/// Manages the state of the buttons according to the current page group.
		/// </summary>
		protected override void OnPageChanged(object sender, OnPageChangedEventArgs args)
		{
			base.OnPageChanged(sender, args);

			string group = args.groupType.ToString();

			if (group.StartsWith("SyncroC.Forms.Pages.WetEndSupervisor") && (!WetEnd.Active))
			{
				ClearActiveGroups();
				WetEnd.Active = true;
				return;
			}

			if (group.StartsWith("SyncroC.Forms.Pages.DoubleBacker") && (!DoubleBacker.Active))
			{
				ClearActiveGroups();
				DoubleBacker.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.GlueUnit") && (!GlueUnit.Active))
			{
				ClearActiveGroups();
				GlueUnit.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.PreHeaterTern") && (!PreHeaterTern.Active))
			{
				ClearActiveGroups();
				PreHeaterTern.Active = true;
				return;
			}

			//Single Facer
			if (group.StartsWith("SyncroC.Forms.Pages.SingleFacer") && (args.index == 1) && (!SingleFacer1.Active))
			{
				ClearActiveGroups();
				SingleFacer1.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.SingleFacer") && (args.index == 2) && (!SingleFacer2.Active))
			{
				ClearActiveGroups();
				SingleFacer2.Active = true;
				return;
			}
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (group.StartsWith("SyncroC.Forms.Pages.SingleFacer") && (args.index == 3) && (!SingleFacer3.Active))
      {
        ClearActiveGroups();
        SingleFacer3.Active = true;
        return;
      }

			//Splicer
			if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 1) && (!Splicer1.Active))
			{
				ClearActiveGroups();
				Splicer1.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 2) && (!Splicer2.Active))
			{
				ClearActiveGroups();
				Splicer2.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 3) && (!Splicer3.Active))
			{
				ClearActiveGroups();
				Splicer3.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 4) && (!Splicer4.Active))
			{
				ClearActiveGroups();
				Splicer4.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 5) && (!Splicer5.Active))
			{
				ClearActiveGroups();
				Splicer5.Active = true;
				return;
			}
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 6) && (!Splicer6.Active))
      {
        ClearActiveGroups();
        Splicer6.Active = true;
        return;
      }
      if (group.StartsWith("SyncroC.Forms.Pages.Splicer") && (args.index == 7) && (!Splicer7.Active))
      {
        ClearActiveGroups();
        Splicer7.Active = true;
        return;
      }
    }

		public void _setPushButtonPressed(string theFasterButton, string theSlowerButton)
		{
			if (theFasterButton.Equals(String.Empty) && theSlowerButton.Equals(String.Empty))
			{
				this.btn_IncDecSpeedWEConsole.Visible = false;
				this.btn_IncDecSpeedPHT.Visible = false;
				this.btn_IncDecSpeedGU.Visible = false;
				this.btn_IncDecSpeedSF1.Visible = false;
				this.btn_IncDecSpeedSF2.Visible = false;
				this.btn_IncDecSpeedSPL1.Visible = false;
				this.btn_IncDecSpeedSPL2.Visible = false;
				this.btn_IncDecSpeedSPL3.Visible = false;
				this.btn_IncDecSpeedSPL4.Visible = false;
				this.btn_IncDecSpeedSPL5.Visible = false;
        //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
        this.btn_IncDecSpeedSPL6.Visible = false;
        this.btn_IncDecSpeedSPL7.Visible = false;
        return;
			}

			//FASTER
			string[] appoFaster = theFasterButton.Split(Command.PARAMETER_SEPARATOR);

			System.Collections.SortedList listFaster = new SortedList();
			foreach (string str in appoFaster)
			{
				if (!str.Equals(String.Empty))
					listFaster.Add(str, str);
			}

			//SLOWER
			string[] appoSlower = theSlowerButton.Split(Command.PARAMETER_SEPARATOR);

			System.Collections.SortedList listSlower = new SortedList();
			foreach (string str in appoSlower)
			{
				if (!str.Equals(String.Empty))
					listSlower.Add(str, str);
			}

			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedWEConsole, listFaster.Contains("CON"), listSlower.Contains("CON"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedPHT, listFaster.Contains("PHT"), listSlower.Contains("PHT"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedGU, listFaster.Contains("GU"), listSlower.Contains("GU"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSF1, listFaster.Contains("SF1"), listSlower.Contains("SF1"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSF2, listFaster.Contains("SF2"), listSlower.Contains("SF2"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL1, listFaster.Contains("SPL1"), listSlower.Contains("SPL1"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL2, listFaster.Contains("SPL2"), listSlower.Contains("SPL2"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL3, listFaster.Contains("SPL3"), listSlower.Contains("SPL3"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL4, listFaster.Contains("SPL4"), listSlower.Contains("SPL4"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL5, listFaster.Contains("SPL5"), listSlower.Contains("SPL5"));
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL6, listFaster.Contains("SPL6"), listSlower.Contains("SPL6"));
      this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSF3, listFaster.Contains("SF3"), listSlower.Contains("SF3"));
      this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSPL7, listFaster.Contains("SPL7"), listSlower.Contains("SPL7"));
    }

		public void _setStateButtonPushButtonPressed(SyncroC.Controls.StateButton theButton, bool isInFaster, bool isInSlower)
		{
			if (!isInFaster && !isInSlower)
			{
				if (theButton.Visible)
				{
					theButton.Visible = false;
					theButton.Active = false;
				}
				return;
			}

			if (isInFaster)
				theButton.Active = true;
			else
				theButton.Active = false;

			if (!theButton.Visible)
				theButton.Visible = true;
		}

		public void _setStateButtonSYCIncDec(string machine, string speedCommand)
		{
			SyncroC.Controls.StateButton buttonLow = null;
			SyncroC.Controls.StateButton buttonHigh = null;

			if (machine != null)
			{
        //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
        switch (machine)
				{
          case "SF3":
            buttonLow = btn_SYCIncDecBandSpeedSF3;
            buttonHigh = btn_SYCIncDecSpeedSF3;
            break;
          case "SF2":
						buttonLow = btn_SYCIncDecBandSpeedSF2;
						buttonHigh = btn_SYCIncDecSpeedSF2;
						break;
					case "SF1":
						buttonLow = btn_SYCIncDecBandSpeedSF1;
						buttonHigh = btn_SYCIncDecSpeedSF1;
						break;
					case "DB":
						buttonLow = btn_SYCIncDecBandSpeedDB;
						buttonHigh = btn_SYCIncDecSpeedDB;
						break;
				}
			}

			if (buttonLow != null && buttonHigh != null)
			{
				if (speedCommand != null)
				{
					switch (speedCommand)
					{
						case "SPEED_COMMAND_FASTER":
							buttonLow.Active = true;
							buttonHigh.Active = true;
							buttonLow.Visible = true;
							buttonHigh.Visible = true;
							break;
						case "SPEED_COMMAND_FASTER_IN_BAND":
							buttonLow.Active = true;
							buttonLow.Visible = true;
							buttonHigh.Visible = false;
							break;
						case "SPEED_COMMAND_SLOWER":
							buttonLow.Active = false;
							buttonHigh.Active = false;
							buttonLow.Visible = true;
							buttonHigh.Visible = true;
							break;
						case "SPEED_COMMAND_SLOWER_IN_BAND":
							buttonLow.Active = false;
							buttonLow.Visible = true;
							buttonHigh.Visible = false;
							break;
						case "SPEED_COMMAND_IDLE":
						default:
							buttonLow.Visible = false;
							buttonHigh.Visible = false;
							break;
					}
				}
				else
				{
					Logger.LogError(this, "_setStateButtonSYCIncDec - Invalid parameter SpeedCommand null");
					buttonLow.Visible = false;
					buttonHigh.Visible = false;
				}
			}
			else
			{
				Logger.LogError(this, "_setStateButtonSYCIncDec - Invalid parameter machine.");			
			}
			
		}

    private void OnButtonPush(object sender, EventArgs e)
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					Command.WEC + "." + ((Control)sender).Name + "()");
    }

		private void lbl_WetEndMode_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			ToggleAutoManWetEnd.PerformClick();
		}

		private void _setSFNCartridge(int sfNumber, string cartridgeId, bool alarmed)
		{
			if (sfNumber == 1)
			{
				this.lbl_SF1Cartridge.Text = cartridgeId;
				this.lbl_SF1Cartridge.BackColor = alarmed ? Color.Red : Color.WhiteSmoke ;
			}
			if (sfNumber == 2)
			{
				this.lbl_SF2Cartridge.Text = cartridgeId;
				this.lbl_SF2Cartridge.BackColor = alarmed ? Color.Red : Color.WhiteSmoke ;
			}
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (sfNumber == 3)
      {
        this.lbl_SF3Cartridge.Text = cartridgeId;
        this.lbl_SF3Cartridge.BackColor = alarmed ? Color.Red : Color.WhiteSmoke;
      }
    }

    //ABELLO_FI07/05/10 Aggiunta gestione del non pronta alla giunta della giuntatrice sulla barra di stato WET.
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    private void _setNotReadyToSplice(bool spl1, bool spl2, bool spl3, bool spl4, bool spl5, bool spl6, bool spl7)
    {
      this.pct_notReadyToSplice_1.Visible = spl1;
      this.pct_notReadyToSplice_2.Visible = spl2;
      this.pct_notReadyToSplice_3.Visible = spl3;
      this.pct_notReadyToSplice_4.Visible = spl4;
      this.pct_notReadyToSplice_5.Visible = spl5;

      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      this.pct_notReadyToSplice_6.Visible = spl6;
      this.pct_notReadyToSplice_7.Visible = spl7;
    }

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    private void _setActive(bool zoneDBActive, bool zone1Active, bool zone2Active, bool zone3Active)
		{
			if (zoneDBActive)
			{
				if (this.lbl_zoneDBActive.Visible)
					this.lbl_zoneDBActive.Visible = false;
			}
			else
			{
				if (!this.lbl_zoneDBActive.Visible)
					this.lbl_zoneDBActive.Visible = true;
			}

			if (zone1Active)
			{
				if (this.lbl_zone1Active.Visible)
				{
					this.lbl_zone1Active.Visible = false;
					this.lbl_zone1Active.BringToFront();
				}
				if (!this.prb_BridgeContents1.Visible)
				{
					this.prb_BridgeContents1.Visible = true;
					this.lbl_BridgeContents1.Visible = true;
				}
			}
			else
			{
				if (!this.lbl_zone1Active.Visible)
					this.lbl_zone1Active.Visible = true;
				if (this.prb_BridgeContents1.Visible)
				{
					this.prb_BridgeContents1.Visible = false;
					this.lbl_BridgeContents1.Visible = false;
				}
			}

			if (zone2Active)
			{
				if (this.lbl_zone2Active.Visible)
				{
					this.lbl_zone2Active.Visible = false;
					this.lbl_zone2Active.BringToFront();
				}
				if (!this.prb_BridgeContents2.Visible)
				{
					this.prb_BridgeContents2.Visible = true;
					this.lbl_BridgeContents2.Visible = true;
				}
			}
			else
			{
				if (!this.lbl_zone2Active.Visible)
					this.lbl_zone2Active.Visible = true;
				if (this.prb_BridgeContents2.Visible)
				{
					this.prb_BridgeContents2.Visible = false;
					this.lbl_BridgeContents2.Visible = false;
				}
			}
    
      //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
      if (SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        if (zone3Active)
        {
          if (this.lbl_zone3Active.Visible)
          {
            this.lbl_zone3Active.Visible = false;
            this.lbl_zone3Active.BringToFront();
          }
          if (!this.prb_BridgeContents3.Visible)
          {
            this.prb_BridgeContents3.Visible = true;
            this.lbl_BridgeContents3.Visible = true;
          }
        }
        else
        {
          if (!this.lbl_zone3Active.Visible)
            this.lbl_zone3Active.Visible = true;
          if (this.prb_BridgeContents3.Visible)
          {
            this.prb_BridgeContents3.Visible = false;
            this.lbl_BridgeContents3.Visible = false;
          }
        }
      }
    }

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    private void pct_BridgeError3_Click(object sender, EventArgs e)
    {
      PageManager pm = PageManager.getPageManager();
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Alarms.Frm_SingleFacerAlarmActive), 3));
    }
    
    private void pct_BridgeError2_Click(object sender, EventArgs e)
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Alarms.Frm_SingleFacerAlarmActive), 2));
		}

		private void pct_BridgeError1_Click(object sender, EventArgs e)
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Alarms.Frm_SingleFacerAlarmActive), 1));
		}

  }
}

