using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.Controls;

namespace SyncroC.Forms.Pages.Production.Alarms
{
	public class Frm_Alarm1 : SyncroC.Forms.Pages.Production.Alarms.Frm_AlarmsSubGroup
	{
		private SyncroC.Controls.GridView gdv_alarm;
		private System.ComponentModel.IContainer components = null;

		private Label lbl_Present;
		private Label lbl_codeZone;
		private Label lbl_present_Description;
		private Label lbl_codeZone_Description;
		private Label lbl_Key;
		private Label lbl_Key_Description;
		private SyncroC.Controls.Title ttl_SyncroActiveAlarms;

		private DataGridViewColumn clm_Zone;
		private DataGridViewColumn clm_Description;
		private DataGridViewColumn clm_BeginStart;
		private DataGridViewColumn clm_Key;

		private DataTable SYC_SW_Alarms;
    private int COLUMN_KEY_CHANGE_VIEW_WIDTH = 250;
    private PictureBox pct_now_causa_percheMiSonoFermato;
    private Label lbl_now_numeroMacchina_percheMiSonoFermato;
    private Label lbl_last_numeroMacchina_percheMiSonoFermato;
    private PictureBox pct_last_causa_percheMiSonoFermato;
    private SyncroC.Controls.GroupPanel.GroupPanel grp_last_percheMiSonoFermato;
    private SyncroC.Controls.GroupPanel.GroupPanel grp_now_percheMiSonoFermato;
    private PictureBox pct_now;
    private PictureBox pct_last; 
		private bool showDebugOption = false;

		public Frm_Alarm1()
		{
			InitializeComponent();

			SYC_SW_Alarms = RTCManager.getRTCManager().ActiveAlarms.Tables[RTCManager.SYC_SW];
			DataView dv = new DataView(SYC_SW_Alarms);
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
      //dv.RowFilter = RTCManager.PRESENT + " = true" ;
      dv.RowFilter = RTCManager.PRESENT + " = 'true'" + " AND " + RTCManager.CODE_ZONE + " <> 'Warning'";
      this.gdv_alarm.DataSource = dv;

			clm_Zone = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Zone.HeaderText = "Zone";
			clm_Zone.Name = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.DataPropertyName = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.Width = 250;

			clm_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Description.HeaderText = "Description";
			clm_Description.Name = RTCManager.DESCRIPTION;
			clm_Description.DataPropertyName = RTCManager.DESCRIPTION;
			//clm_Description.Width = 595;
      clm_Description.Width = 699;

			clm_BeginStart = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_BeginStart.HeaderText = "Begin";
			clm_BeginStart.Name = RTCManager.BEGIN_TIME;
			clm_BeginStart.DataPropertyName = RTCManager.BEGIN_TIME;
			clm_BeginStart.Width = 190;

			clm_Key = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Key.HeaderText = "Code";
			clm_Key.Name = RTCManager.KEY;
			clm_Key.DataPropertyName = RTCManager.KEY;
			clm_Key.Visible = false;
			clm_Key.MinimumWidth = COLUMN_KEY_CHANGE_VIEW_WIDTH;

			gdv_alarm.Columns.Add(clm_Zone);
			gdv_alarm.Columns.Add(clm_Description);
			gdv_alarm.Columns.Add(clm_BeginStart);
			gdv_alarm.Columns.Add(clm_Key);

			gdv_alarm.Sort(clm_BeginStart, SyncroC.DataBase.Alarm.listSortDirection);
		}

    //ABELLO_FI26/08/2010_1 Aggiunto il trace degli Stop
    private void _setHistoryFirstReasonOfStop(string timeOfStop, string reasonFirstOfStop)
    {
      string[] appo = reasonFirstOfStop.Split(new char[] { '-' });
      string macchina = (appo.Length >= 1) ? appo[0].Trim() : "";
      string tipodistop = (appo.Length >= 2) ? appo[1].Trim() : "";

      if (macchina == "")
      {
        this.grp_last_percheMiSonoFermato.Visible = false;
        this.grp_last_percheMiSonoFermato.GroupTitle = "";
        this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
        this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
        return;
      }
      else
      {
        this.grp_last_percheMiSonoFermato.GroupTitle = SyncroC.Utility.SyncroConverter.ConvertDateTimeWithMillisecondsToStrForDataGridView(SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(timeOfStop).ToLocalTime());

        this.grp_last_percheMiSonoFermato.Visible = true;

        switch (tipodistop)
        {
          case "NS":
            this.pct_last_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.OStop;
            break;
          case "ES":
            this.pct_last_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.NStop;
            break;
          case "NOT_READY_TO_RUN":
            this.pct_last_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.Not_Ready_To_Run;
            break;
          case "FUNGO":
            this.pct_last_causa_percheMiSonoFermato.Image = SyncroC.Properties.Resources.fungo;
            break;
        }

        switch (macchina)
        {
          case "STK1":
            this.pct_last.Image = SyncroC.Properties.Resources.Terminal;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "STK2":
            this.pct_last.Image = SyncroC.Properties.Resources.Terminal;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "STK3":
            this.pct_last.Image = SyncroC.Properties.Resources.Terminal;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "KN1":
            this.pct_last.Image = SyncroC.Properties.Resources.Master;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "KN2":
            this.pct_last.Image = SyncroC.Properties.Resources.Master;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "KN3":
            this.pct_last.Image = SyncroC.Properties.Resources.Master;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SS":
            this.pct_last.Image = SyncroC.Properties.Resources.twin;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "RSH":
            this.pct_last.Image = SyncroC.Properties.Resources.Rotary;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "DB":
            this.pct_last.Image = SyncroC.Properties.Resources.Piani;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "SYC":
            this.pct_last.Image = SyncroC.Properties.Resources.wet_dry;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "PHT":
            this.pct_last.Image = SyncroC.Properties.Resources.Terna;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "GU":
            this.pct_last.Image = SyncroC.Properties.Resources.Glue_unit;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
          case "SPL1":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL2":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL3":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL4":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "4";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL5":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "5";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL6":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "6";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SPL7":
            this.pct_last.Image = SyncroC.Properties.Resources.link1_1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "7";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SF1":
            this.pct_last.Image = SyncroC.Properties.Resources.SingleFacer1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "1";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SF2":
            this.pct_last.Image = SyncroC.Properties.Resources.SingleFacer1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "2";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          case "SF3":
            this.pct_last.Image = SyncroC.Properties.Resources.SingleFacer1;
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "3";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = true;
            break;
          default:
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "";
            this.lbl_last_numeroMacchina_percheMiSonoFermato.Visible = false;
            break;
        }
      }
    }

    private void _setFirstReasonOfStop(string timeOfStop, string reasonFirstOfStop)
    {

      string[] appo = reasonFirstOfStop.Split(new char[] { '-' });
      string macchina = (appo.Length >= 1) ? appo[0].Trim() : "";
      string tipodistop = (appo.Length >= 2) ? appo[1].Trim() : "";

      if (macchina == "")
      {
        this.grp_now_percheMiSonoFermato.GroupTitle = "";
        this.lbl_now_numeroMacchina_percheMiSonoFermato.Text = "";
        this.grp_now_percheMiSonoFermato.BackgroundGradientColor = Color.Green;

        foreach (Control c in this.grp_now_percheMiSonoFermato.Controls)
          c.Visible = false;

        return;
      }
      else
      {
        this.grp_now_percheMiSonoFermato.GroupTitle = SyncroC.Utility.SyncroConverter.ConvertDateTimeWithMillisecondsToStrForDataGridView(SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(timeOfStop).ToLocalTime());

        this.grp_now_percheMiSonoFermato.BackgroundGradientColor = Color.Red;

        foreach (Control c in this.grp_now_percheMiSonoFermato.Controls)
          c.Visible = true;

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
			this.gdv_alarm.DataSource = null;//unbind
			base.Dispose(disposing);
		}

		/// <summary>
		/// F10 key behaviour: must not do anything.
		/// </summary>
		protected override void F10()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Alarm1));
      this.gdv_alarm = new SyncroC.Controls.GridView();
      this.lbl_Present = new System.Windows.Forms.Label();
      this.lbl_codeZone = new System.Windows.Forms.Label();
      this.lbl_present_Description = new System.Windows.Forms.Label();
      this.lbl_codeZone_Description = new System.Windows.Forms.Label();
      this.ttl_SyncroActiveAlarms = new SyncroC.Controls.Title();
      this.lbl_Key = new System.Windows.Forms.Label();
      this.lbl_Key_Description = new System.Windows.Forms.Label();
      this.lbl_now_numeroMacchina_percheMiSonoFermato = new System.Windows.Forms.Label();
      this.pct_now_causa_percheMiSonoFermato = new System.Windows.Forms.PictureBox();
      this.lbl_last_numeroMacchina_percheMiSonoFermato = new System.Windows.Forms.Label();
      this.pct_last_causa_percheMiSonoFermato = new System.Windows.Forms.PictureBox();
      this.grp_last_percheMiSonoFermato = new SyncroC.Controls.GroupPanel.GroupPanel();
      this.grp_now_percheMiSonoFermato = new SyncroC.Controls.GroupPanel.GroupPanel();
      this.pct_now = new System.Windows.Forms.PictureBox();
      this.pct_last = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now_causa_percheMiSonoFermato)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_last_causa_percheMiSonoFermato)).BeginInit();
      this.grp_last_percheMiSonoFermato.SuspendLayout();
      this.grp_now_percheMiSonoFermato.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_last)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.SelectedAlarm;
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "Change View";
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
      // gdv_alarm
      // 
      this.gdv_alarm.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_alarm.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_alarm.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.AutoGenerateColumns = false;
      this.gdv_alarm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_alarm.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_alarm.DataMember = "";
      this.gdv_alarm.DataSource = null;
      this.gdv_alarm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.gdv_alarm.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.ImagePassword = null;
      this.gdv_alarm.Location = new System.Drawing.Point(39, 328);
      this.gdv_alarm.MultiSelect = false;
      this.gdv_alarm.Name = "gdv_alarm";
      this.gdv_alarm.RowsHeight = 32;
      this.gdv_alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarm.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.Size = new System.Drawing.Size(1161, 383);
      this.gdv_alarm.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarm.TabIndex = 110;
      this.gdv_alarm.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.TranslateText = true;
      this.gdv_alarm.SelectionChanged += new System.EventHandler(this.gdv_alarm_SelectionChanged);
      this.gdv_alarm.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_alarm_CellFormatting);
      // 
      // lbl_Present
      // 
      this.lbl_Present.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Present.Location = new System.Drawing.Point(842, 66);
      this.lbl_Present.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_Present.Name = "lbl_Present";
      this.lbl_Present.Size = new System.Drawing.Size(68, 23);
      this.lbl_Present.TabIndex = 120;
      this.lbl_Present.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Present.Visible = false;
      // 
      // lbl_codeZone
      // 
      this.lbl_codeZone.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_codeZone.Location = new System.Drawing.Point(173, 66);
      this.lbl_codeZone.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_codeZone.Name = "lbl_codeZone";
      this.lbl_codeZone.Size = new System.Drawing.Size(269, 23);
      this.lbl_codeZone.TabIndex = 119;
      this.lbl_codeZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_codeZone.Visible = false;
      // 
      // lbl_present_Description
      // 
      this.lbl_present_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_present_Description.Location = new System.Drawing.Point(788, 66);
      this.lbl_present_Description.Name = "lbl_present_Description";
      this.lbl_present_Description.Size = new System.Drawing.Size(54, 23);
      this.lbl_present_Description.TabIndex = 118;
      this.lbl_present_Description.Text = "Present:";
      this.lbl_present_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_present_Description.Visible = false;
      // 
      // lbl_codeZone_Description
      // 
      this.lbl_codeZone_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_codeZone_Description.Location = new System.Drawing.Point(108, 66);
      this.lbl_codeZone_Description.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_codeZone_Description.Name = "lbl_codeZone_Description";
      this.lbl_codeZone_Description.Size = new System.Drawing.Size(65, 23);
      this.lbl_codeZone_Description.TabIndex = 117;
      this.lbl_codeZone_Description.Text = "Code Zone:";
      this.lbl_codeZone_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_codeZone_Description.Visible = false;
      // 
      // ttl_SyncroActiveAlarms
      // 
      this.ttl_SyncroActiveAlarms.BackColor = System.Drawing.Color.Black;
      this.ttl_SyncroActiveAlarms.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SyncroActiveAlarms.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SyncroActiveAlarms.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SyncroActiveAlarms.Location = new System.Drawing.Point(29, 300);
      this.ttl_SyncroActiveAlarms.Name = "ttl_SyncroActiveAlarms";
      this.ttl_SyncroActiveAlarms.Size = new System.Drawing.Size(1171, 411);
      this.ttl_SyncroActiveAlarms.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_SyncroActiveAlarms.TabIndex = 116;
      this.ttl_SyncroActiveAlarms.Text = "Syncro alarms";
      this.ttl_SyncroActiveAlarms.TranslateText = true;
      // 
      // lbl_Key
      // 
      this.lbl_Key.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Key.Location = new System.Drawing.Point(513, 66);
      this.lbl_Key.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_Key.Name = "lbl_Key";
      this.lbl_Key.Size = new System.Drawing.Size(269, 23);
      this.lbl_Key.TabIndex = 122;
      this.lbl_Key.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Key.Visible = false;
      // 
      // lbl_Key_Description
      // 
      this.lbl_Key_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Key_Description.Location = new System.Drawing.Point(448, 66);
      this.lbl_Key_Description.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Key_Description.Name = "lbl_Key_Description";
      this.lbl_Key_Description.Size = new System.Drawing.Size(65, 23);
      this.lbl_Key_Description.TabIndex = 121;
      this.lbl_Key_Description.Text = "Key:";
      this.lbl_Key_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_Key_Description.Visible = false;
      // 
      // lbl_now_numeroMacchina_percheMiSonoFermato
      // 
      this.lbl_now_numeroMacchina_percheMiSonoFermato.BackColor = System.Drawing.Color.Transparent;
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_now_numeroMacchina_percheMiSonoFermato.ForeColor = System.Drawing.Color.Black;
      this.lbl_now_numeroMacchina_percheMiSonoFermato.Location = new System.Drawing.Point(108, 32);
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
      this.pct_now_causa_percheMiSonoFermato.Location = new System.Drawing.Point(150, 41);
      this.pct_now_causa_percheMiSonoFermato.Name = "pct_now_causa_percheMiSonoFermato";
      this.pct_now_causa_percheMiSonoFermato.Size = new System.Drawing.Size(72, 60);
      this.pct_now_causa_percheMiSonoFermato.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_now_causa_percheMiSonoFermato.TabIndex = 1;
      this.pct_now_causa_percheMiSonoFermato.TabStop = false;
      // 
      // lbl_last_numeroMacchina_percheMiSonoFermato
      // 
      this.lbl_last_numeroMacchina_percheMiSonoFermato.BackColor = System.Drawing.Color.Transparent;
      this.lbl_last_numeroMacchina_percheMiSonoFermato.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_last_numeroMacchina_percheMiSonoFermato.ForeColor = System.Drawing.Color.Black;
      this.lbl_last_numeroMacchina_percheMiSonoFermato.Location = new System.Drawing.Point(108, 32);
      this.lbl_last_numeroMacchina_percheMiSonoFermato.Name = "lbl_last_numeroMacchina_percheMiSonoFermato";
      this.lbl_last_numeroMacchina_percheMiSonoFermato.Size = new System.Drawing.Size(25, 35);
      this.lbl_last_numeroMacchina_percheMiSonoFermato.TabIndex = 2;
      this.lbl_last_numeroMacchina_percheMiSonoFermato.Text = "9";
      this.lbl_last_numeroMacchina_percheMiSonoFermato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_last_causa_percheMiSonoFermato
      // 
      this.pct_last_causa_percheMiSonoFermato.BackColor = System.Drawing.Color.Transparent;
      this.pct_last_causa_percheMiSonoFermato.Image = global::SyncroC.Properties.Resources.fungo;
      this.pct_last_causa_percheMiSonoFermato.Location = new System.Drawing.Point(150, 41);
      this.pct_last_causa_percheMiSonoFermato.Name = "pct_last_causa_percheMiSonoFermato";
      this.pct_last_causa_percheMiSonoFermato.Size = new System.Drawing.Size(72, 60);
      this.pct_last_causa_percheMiSonoFermato.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_last_causa_percheMiSonoFermato.TabIndex = 1;
      this.pct_last_causa_percheMiSonoFermato.TabStop = false;
      // 
      // grp_last_percheMiSonoFermato
      // 
      this.grp_last_percheMiSonoFermato.BackgroundColor = System.Drawing.Color.White;
      this.grp_last_percheMiSonoFermato.BackgroundGradientColor = System.Drawing.Color.RoyalBlue;
      this.grp_last_percheMiSonoFermato.BackgroundGradientMode = SyncroC.Controls.GroupPanel.GroupPanel.GroupBoxGradientMode.BackwardDiagonal;
      this.grp_last_percheMiSonoFermato.BorderColor = System.Drawing.Color.Black;
      this.grp_last_percheMiSonoFermato.BorderThickness = 1F;
      this.grp_last_percheMiSonoFermato.Controls.Add(this.pct_last);
      this.grp_last_percheMiSonoFermato.Controls.Add(this.lbl_last_numeroMacchina_percheMiSonoFermato);
      this.grp_last_percheMiSonoFermato.Controls.Add(this.pct_last_causa_percheMiSonoFermato);
      this.grp_last_percheMiSonoFermato.CustomGroupBoxColor = System.Drawing.Color.Gray;
      this.grp_last_percheMiSonoFermato.Font = new System.Drawing.Font("Arial Unicode MS", 12F);
      this.grp_last_percheMiSonoFermato.ForeColor = System.Drawing.Color.White;
      this.grp_last_percheMiSonoFermato.GroupImage = null;
      this.grp_last_percheMiSonoFermato.GroupTitle = "24/12/1974 00:00:00:000";
      this.grp_last_percheMiSonoFermato.Location = new System.Drawing.Point(297, 181);
      this.grp_last_percheMiSonoFermato.Name = "grp_last_percheMiSonoFermato";
      this.grp_last_percheMiSonoFermato.Padding = new System.Windows.Forms.Padding(20);
      this.grp_last_percheMiSonoFermato.PaintGroupBox = true;
      this.grp_last_percheMiSonoFermato.RoundCorners = 10;
      this.grp_last_percheMiSonoFermato.ShadowColor = System.Drawing.Color.DarkGray;
      this.grp_last_percheMiSonoFermato.ShadowControl = true;
      this.grp_last_percheMiSonoFermato.ShadowThickness = 3;
      this.grp_last_percheMiSonoFermato.Size = new System.Drawing.Size(247, 113);
      this.grp_last_percheMiSonoFermato.TabIndex = 125;
      this.grp_last_percheMiSonoFermato.TabStop = false;
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
      this.grp_now_percheMiSonoFermato.Location = new System.Drawing.Point(705, 181);
      this.grp_now_percheMiSonoFermato.Name = "grp_now_percheMiSonoFermato";
      this.grp_now_percheMiSonoFermato.PaintGroupBox = true;
      this.grp_now_percheMiSonoFermato.RoundCorners = 10;
      this.grp_now_percheMiSonoFermato.ShadowColor = System.Drawing.Color.DarkGray;
      this.grp_now_percheMiSonoFermato.ShadowControl = true;
      this.grp_now_percheMiSonoFermato.ShadowThickness = 3;
      this.grp_now_percheMiSonoFermato.Size = new System.Drawing.Size(247, 113);
      this.grp_now_percheMiSonoFermato.TabIndex = 126;
      this.grp_now_percheMiSonoFermato.TabStop = false;
      // 
      // pct_now
      // 
      this.pct_now.Image = global::SyncroC.Properties.Resources.Terna;
      this.pct_now.Location = new System.Drawing.Point(27, 41);
      this.pct_now.Name = "pct_now";
      this.pct_now.Size = new System.Drawing.Size(72, 60);
      this.pct_now.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_now.TabIndex = 3;
      this.pct_now.TabStop = false;
      // 
      // pct_last
      // 
      this.pct_last.Image = global::SyncroC.Properties.Resources.Terna;
      this.pct_last.Location = new System.Drawing.Point(27, 41);
      this.pct_last.Name = "pct_last";
      this.pct_last.Size = new System.Drawing.Size(72, 60);
      this.pct_last.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_last.TabIndex = 4;
      this.pct_last.TabStop = false;
      // 
      // Frm_Alarm1
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.grp_now_percheMiSonoFermato);
      this.Controls.Add(this.lbl_Key);
      this.Controls.Add(this.lbl_Key_Description);
      this.Controls.Add(this.lbl_Present);
      this.Controls.Add(this.lbl_codeZone);
      this.Controls.Add(this.lbl_present_Description);
      this.Controls.Add(this.lbl_codeZone_Description);
      this.Controls.Add(this.ttl_SyncroActiveAlarms);
      this.Controls.Add(this.grp_last_percheMiSonoFermato);
      this.Controls.Add(this.gdv_alarm);
      this.Name = "Frm_Alarm1";
      this.Text = "Frm_Alarm1";
      this.Controls.SetChildIndex(this.gdv_alarm, 0);
      this.Controls.SetChildIndex(this.grp_last_percheMiSonoFermato, 0);
      this.Controls.SetChildIndex(this.ttl_SyncroActiveAlarms, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone_Description, 0);
      this.Controls.SetChildIndex(this.lbl_present_Description, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone, 0);
      this.Controls.SetChildIndex(this.lbl_Present, 0);
      this.Controls.SetChildIndex(this.lbl_Key_Description, 0);
      this.Controls.SetChildIndex(this.lbl_Key, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.grp_now_percheMiSonoFermato, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now_causa_percheMiSonoFermato)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_last_causa_percheMiSonoFermato)).EndInit();
      this.grp_last_percheMiSonoFermato.ResumeLayout(false);
      this.grp_now_percheMiSonoFermato.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_now)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_last)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		protected override void CtrlF1()
		{
			if (!clm_Key.Visible)
			{//wiew without key, so show key column
				clm_Description.Width -= COLUMN_KEY_CHANGE_VIEW_WIDTH;
				clm_Key.Visible = true;
			}
			else
			{//view with key, so hide key column
				clm_Description.Width += COLUMN_KEY_CHANGE_VIEW_WIDTH;
				clm_Key.Visible = false;
			}
			gdv_alarm.Invalidate();
		}

		protected override void ControlD()
		{
			base.ControlD();
			showDebugOption = !showDebugOption;
			gdv_alarm.SelectionBackColor = (showDebugOption) ? SyncroC.Controls.Colors.SyncroColor.Green : SyncroC.Controls.Colors.SyncroColor.White;
			gdv_alarm.AlternateSelectionBackColor = (showDebugOption) ? SyncroC.Controls.Colors.SyncroColor.Green : SyncroC.Controls.Colors.SyncroColor.LightGray;
			lbl_Key_Description.Visible = lbl_Key.Visible =
			lbl_codeZone_Description.Visible = lbl_codeZone.Visible =
				lbl_present_Description.Visible = lbl_Present.Visible = showDebugOption;
		}

		private void gdv_alarm_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_alarm.SelectedRows.Count == 1)
			{
				DataRow dataRow = ((DataRowView)gdv_alarm.SelectedRows[0].DataBoundItem).Row;
				if (dataRow != null)
				{
					lbl_Key.Text = dataRow[RTCManager.KEY].ToString();
					lbl_codeZone.Text = dataRow[RTCManager.CODE_ZONE].ToString();
					lbl_Present.Text = dataRow[RTCManager.PRESENT].ToString();
				}
				else
				{
					lbl_codeZone.Text = lbl_Present.Text = lbl_Key.Text = string.Empty;
				}
			}
			else
			{
				lbl_codeZone.Text = lbl_Present.Text = lbl_Key.Text = string.Empty;
			}
		}

		private void gdv_alarm_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (this.gdv_alarm.Columns[e.ColumnIndex].Name == RTCManager.BEGIN_TIME
						&& e.Value != null)
			{
				try
				{
					//FOS2 ABello
					//e.Value =  ((DateTime)e.Value).ToLocalTime().ToString().Replace('.', ':');
					e.Value = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(((DateTime)e.Value).ToLocalTime());
				}
				catch (InvalidCastException ex)
				{
					Logger.LogError(this, "gdv_alarm_CellFormatting - " + RTCManager.BEGIN_TIME, ex);
				}
			}
		}
	}
}

