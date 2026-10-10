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
	public class Frm_StatusBarDryEnd : SyncroC.Forms.StatusBar.Frm_StatusBar
	{
    public SyncroC.Controls.ProgBar prb_DryEnd;
		private SyncroC.Controls.TitledLabel tlb_DryEndSetup;
		private SyncroC.Controls.TitledLabel tlb_DryEndMetersLeft;
		private SyncroC.Controls.Title ttl_DryEndTitle;
    public Label lbl_AOCType_NN;
    private SyncroC.Controls.GridView gdv_SetupData;
    private DataSet setups;
		private DataView dataView;

		private DataGridViewColumn clm_setupData_Code;
    private DataGridViewColumn clm_quality_ID;
    private DataGridViewColumn clm_paper_width;
		private DataGridViewColumn clm_setup_lenght;
    private DataGridViewColumn clm_setup_Trim;
		private DataGridViewColumn clm_order1_data;
		private DataGridViewColumn clm_order2_data;
		private DataGridViewColumn clm_order3_data;
		private SyncroC.Controls.TitledNumericUpDown nud_LineSpeed;
		private SyncroC.Controls.TitledMaskTextBox tmt_AOCTimeLeft;
		public Label lbl_AOCMode_NN;
		private PictureBox pct_STK2NotRdyAOC;
		private PictureBox pct_STK2NotRdyRun;
		private PictureBox pct_STK1NotRdyAOC;
		private PictureBox pct_STK1NotRdyRun;
		private PictureBox pct_KNIFENotRdyAOC;
		private PictureBox pct_KNIFENotRdyRun;
		private PictureBox pct_RSHNotRdyAOC;
		private PictureBox pct_RSHNotRdyRun;
		private PictureBox pct_DBNotRdyAOC;
		private PictureBox pct_DBNotRdyRun;
		private PictureBox pct_SSNotRdyAOC;
		private PictureBox pct_SSNotRdyRun;
		private SyncroC.Controls.StateButton DryEnd;
		private SyncroC.Controls.StateButton Stacker2;
		private SyncroC.Controls.StateButton Stacker1;
		private SyncroC.Controls.StateButton Knife;
		private SyncroC.Controls.StateButton SlitterScorer;
		private SyncroC.Controls.StateButton Shear;
		private SyncroC.Controls.StateButton DoubleBacker;
		private PictureBox pct_BoardAtKnife;
		private PictureBox pct_BoardAtMW;
		private SyncroC.Controls.TitledLabel tlb_WetEndMetersLeft;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSTK;
		private SyncroC.Controls.StateButton btn_IncDecSpeedKN;
		private SyncroC.Controls.StateButton btn_IncDecSpeedRSH;
		private SyncroC.Controls.StateButton btn_IncDecSpeedDEConsole;
		private SyncroC.Controls.Title ttl_WetEndTitle;
		private Panel pnl_clickExtension;
		private Panel pnl_clickExtension2;
		private SyncroC.Controls.StateButton stateButton1;
		private SyncroC.Controls.StateButton btn_IncDecSpeedSS;
		private PictureBox pct_BoardAtRSH;
		private SyncroC.Controls.StateButton btn_IncDecSpeedDB;
		private PictureBox pct_RSHScrapStatus;
		private ImageList iml_RSHStatus;
		private PictureBox pct_AOCType;
		private ImageList iml_AOCStatus;
		private PictureBox pct_AOCStatus;
		private ImageList iml_AOCType;
		private SyncroC.Controls.StateButton Stacker3;
		private PictureBox pct_STK3NotRdyAOC;

		private System.ComponentModel.IContainer components = null;
		private PictureBox pct_STK3NotRdyRun;
		private SyncroC.Controls.Title ttl_SimulationTitle;
		private PictureBox pct_BoardAtKnifeLevel3;
		private PictureBox pct_maintenance;
		private PictureBox pct_SYCNotRdyAOC;
		private PictureBox pct_SYCNotRdyRun;

		private int numStacker = -1;
    private SyncroC.Controls.GroupPanel.GroupPanel grp_now_percheMiSonoFermato;
    private Label lbl_now_numeroMacchina_percheMiSonoFermato;
    private PictureBox pct_now_causa_percheMiSonoFermato;
    private PictureBox pct_now;
    private bool corrugatorDirectionLeft = false;
    private string firstStopTime = String.Empty;
    private string firstStopMachine = String.Empty;
    private string firstStopReason = String.Empty;

    private static void appendOperatorXmlValue(StringBuilder xml, string name, string value)
    {
      xml.Append('<').Append(name).Append('>');
      xml.Append(System.Security.SecurityElement.Escape(value ?? String.Empty));
      xml.Append("</").Append(name).Append(">\n");
    }

    private void appendOperatorMachineXml(StringBuilder xml, string name, SyncroC.Controls.StateButton machine, PictureBox notReadyRun, PictureBox notReadyAoc)
    {
      xml.Append("<Machine><Name>").Append(name).Append("</Name>");
      appendOperatorXmlValue(xml, "Configured", machine.Enabled.ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "ReadyToRun", (machine.Enabled && !notReadyRun.Visible).ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "ReadyToAOC", (machine.Enabled && !notReadyAoc.Visible).ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "Alarmed", machine.Alarmed.ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "Warned", machine.Warned.ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "InEmergency", machine.Inemergency.ToString().ToLowerInvariant());
      xml.Append("</Machine>\n");
    }

    private void appendOperatorAlertsXml(StringBuilder xml)
    {
      xml.Append("<ActiveAlerts>\n");
      foreach (DataTable table in RTCManager.getRTCManager().ActiveAlarms.Tables)
      {
        foreach (DataRow row in table.Rows)
        {
          if (row[RTCManager.PRESENT] == DBNull.Value || !Convert.ToBoolean(row[RTCManager.PRESENT]))
            continue;

          string codeZone = Convert.ToString(row[RTCManager.CODE_ZONE]);
          object beginValue = row[RTCManager.BEGIN_TIME];
          string beginTime = beginValue == DBNull.Value
            ? String.Empty
            : Convert.ToDateTime(beginValue).ToString("yyyy-MM-dd'T'HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

          xml.Append("<Alert>\n");
          appendOperatorXmlValue(xml, "Machine", table.TableName);
          appendOperatorXmlValue(xml, "Code", Convert.ToString(row[RTCManager.KEY]));
          appendOperatorXmlValue(xml, "Severity", String.Equals(codeZone, "Warning", StringComparison.OrdinalIgnoreCase) ? "Warning" : "Alarm");
          appendOperatorXmlValue(xml, "Zone", Convert.ToString(row[RTCManager.DESCRIPTION_ZONE]));
          appendOperatorXmlValue(xml, "Description", Convert.ToString(row[RTCManager.DESCRIPTION]));
          appendOperatorXmlValue(xml, "StartedAt", beginTime);
          xml.Append("</Alert>\n");
        }
      }
      xml.Append("</ActiveAlerts>\n");
    }

    public string getOperatorStatusXml()
    {
      StringBuilder xml = new StringBuilder();
      xml.Append("<OperatorStatus>\n");
      appendOperatorXmlValue(xml, "LineSpeed", nud_LineSpeed.Value);
      appendOperatorXmlValue(xml, "DryMetersLeft", tlb_DryEndMetersLeft.Value);
      appendOperatorXmlValue(xml, "WetEndMetersLeft", tlb_WetEndMetersLeft.Value);
      appendOperatorXmlValue(xml, "DrySetupCount", tlb_DryEndSetup.Value);
      appendOperatorXmlValue(xml, "DryProgressPercent", prb_DryEnd.Percentage.ToString());
      appendOperatorXmlValue(xml, "AocTimeLeft", tmt_AOCTimeLeft.Value);
      appendOperatorXmlValue(xml, "AocMode", lbl_AOCMode_NN.Text);
      appendOperatorXmlValue(xml, "AocType", lbl_AOCType_NN.Text);
      appendOperatorXmlValue(xml, "Simulation", ttl_SimulationTitle.Visible.ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "FirstStopTime", firstStopTime);
      appendOperatorXmlValue(xml, "FirstStopMachine", firstStopMachine);
      appendOperatorXmlValue(xml, "FirstStopReason", firstStopReason);
      appendOperatorXmlValue(xml, "ReadyToRun", (!pct_SYCNotRdyRun.Visible).ToString().ToLowerInvariant());
      appendOperatorXmlValue(xml, "ReadyToAOC", (!pct_SYCNotRdyAOC.Visible).ToString().ToLowerInvariant());
      xml.Append("<Machines>\n");
      appendOperatorMachineXml(xml, "DoubleBacker", DoubleBacker, pct_DBNotRdyRun, pct_DBNotRdyAOC);
      appendOperatorMachineXml(xml, "Shear", Shear, pct_RSHNotRdyRun, pct_RSHNotRdyAOC);
      appendOperatorMachineXml(xml, "SlitterScorer", SlitterScorer, pct_SSNotRdyRun, pct_SSNotRdyAOC);
      appendOperatorMachineXml(xml, "Knife", Knife, pct_KNIFENotRdyRun, pct_KNIFENotRdyAOC);
      appendOperatorMachineXml(xml, "Stacker1", Stacker1, pct_STK1NotRdyRun, pct_STK1NotRdyAOC);
      appendOperatorMachineXml(xml, "Stacker2", Stacker2, pct_STK2NotRdyRun, pct_STK2NotRdyAOC);
      appendOperatorMachineXml(xml, "Stacker3", Stacker3, pct_STK3NotRdyRun, pct_STK3NotRdyAOC);
      xml.Append("</Machines>\n");
      appendOperatorAlertsXml(xml);
      xml.Append("</OperatorStatus>");
      return xml.ToString();
    }

		public Frm_StatusBarDryEnd()
		{
			// This call is required by the Windows Form Designe.
			InitializeComponent();

      setups = DBManager.getDBManager().order.order;

			dataView = new DataView(
				setups.Tables[Order.DRYEND_LIST], //table
				Order.DEL_POSITION + " = 1 OR " + Order.DEL_POSITION + " = 2 OR " + Order.DEL_POSITION + " = 3", //filter for first 3 orders
				Order.DEL_POSITION + " ASC", //sort by position
				DataViewRowState.CurrentRows);	//any state
			this.gdv_SetupData.DataSource = dataView;

			//gdv_SetupData
      clm_setupData_Code = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_setupData_Code.HeaderText = "Setup Code";
      clm_setupData_Code.DataPropertyName = Order.DEL_SETUP_CODE;
      //agrassi_FI24-05-2010_2
      //clm_setupData_Code.Width = 132;
      clm_setupData_Code.Width = 182;

      //ABELLO_FI07-07-08_2 allargata la colonna dell' ID della Qualit� 
      clm_quality_ID = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_quality_ID.HeaderText = "Quality ID";
      clm_quality_ID.DataPropertyName = Order.DEL_QUALITY_ID;
      //agrassi_FI24-05-2010_2
      //clm_quality_ID.Width = 247;
      clm_quality_ID.Width = 237;

			clm_paper_width = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_paper_width.Name = Order.DEL_WEB_WIDTH;
			clm_paper_width.HeaderText = "Width";
			clm_paper_width.DataPropertyName = Order.DEL_WEB_WIDTH;
			clm_paper_width.Width = 75;

      clm_setup_Trim = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_setup_Trim.Name = Order.DEL_TRIM;
      clm_setup_Trim.HeaderText = "Trim";
      clm_setup_Trim.DataPropertyName = Order.DEL_TRIM;
      clm_setup_Trim.Width = 60;

      clm_setup_lenght = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_setup_lenght.Name = Order.DEL_FREE1;
      clm_setup_lenght.HeaderText = "Length";
      clm_setup_lenght.DataPropertyName = Order.DEL_SHEET_LENGTH_0;
      clm_setup_lenght.Width = 103;

			clm_order1_data = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_order1_data.Name = Order.DEL_SHEET_LENGTH_0;
			clm_order1_data.HeaderText = "Order 1";
			clm_order1_data.DataPropertyName = Order.DEL_SHEET_LENGTH_0;
      //agrassi_FI24-05-2010_2
      //clm_order1_data.Width = 210;
      clm_order1_data.Width = 205;

			clm_order2_data = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_order2_data.Name = Order.DEL_SHEET_LENGTH_1;
			clm_order2_data.HeaderText = "Order 2";
			clm_order2_data.DataPropertyName = Order.DEL_SHEET_LENGTH_1;
      //agrassi_FI24-05-2010_2
      //clm_order1_data.Width = 210;
      clm_order2_data.Width = 205;

			clm_order3_data = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_order3_data.Name = Order.DEL_SHEET_LENGTH_2;
			clm_order3_data.HeaderText = "Order 3";
			clm_order3_data.DataPropertyName = Order.DEL_SHEET_LENGTH_2;
      //agrassi_FI24-05-2010_2
      //clm_order3_data.Width = 210;
      clm_order3_data.Width = 180;

			gdv_SetupData.Columns.Add(clm_setupData_Code);
      gdv_SetupData.Columns.Add(clm_quality_ID);
      gdv_SetupData.Columns.Add(clm_paper_width);
      gdv_SetupData.Columns.Add(clm_setup_Trim);
      gdv_SetupData.Columns.Add(clm_setup_lenght);
			gdv_SetupData.Columns.Add(clm_order1_data);
			gdv_SetupData.Columns.Add(clm_order2_data);
			gdv_SetupData.Columns.Add(clm_order3_data);

			tmt_AOCTimeLeft.DataBindings.Add(new Binding("BackColor", tlb_DryEndMetersLeft, "BackColor"));

      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeAOCMode)
      {
        this.lbl_AOCMode_NN.Cursor = Cursors.Arrow;
        this.pnl_clickExtension2.Cursor = Cursors.Arrow;
      }

			//register to page change
			if (!this.DesignMode)
				PageManager.getPageManager().pagesHistorian.OnPageChanged += new SyncroC.PagesHistorian.PageChangedHandler(OnPageChanged);
		}

    private void _setFirstReasonOfStop(string timeOfStop, string reasonFirstOfStop)
    {

      string[] appo = reasonFirstOfStop.Split(new char[] { '-' });
      string macchina = (appo.Length >= 1) ? appo[0].Trim() : "";
      string tipodistop = (appo.Length >= 2) ? appo[1].Trim() : "";

      if (macchina == "")
      {
        this.firstStopTime = String.Empty;
        this.firstStopMachine = String.Empty;
        this.firstStopReason = String.Empty;
        this.grp_now_percheMiSonoFermato.Visible = false;

        return;
      }
      else
      {
        this.firstStopTime = timeOfStop;
        this.firstStopMachine = macchina;
        this.firstStopReason = tipodistop;
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

    public void _ShowStatusBarDryEnd(bool DBcontrolled, bool RSHcontrolled, bool SScontrolled, bool KNIFEcontrolled, bool STK1controlled, bool STK2controlled, bool STK3controlled, int numOfStacker)
		{
      //ABello_FI05/10/2007_1 Rivista e sistemata la barra di stato DRY con 3 livelli di scarico

			this._Show();

      this.DoubleBacker.Enabled = DBcontrolled;
      this.Shear.Enabled = RSHcontrolled;
      this.SlitterScorer.Enabled = SScontrolled;
      this.Knife.Enabled = KNIFEcontrolled;
      this.Stacker1.Enabled = STK1controlled;
      this.Stacker2.Enabled = STK2controlled;
      this.Stacker3.Enabled = STK3controlled;

			switch (numOfStacker)
			{
				case 0:
					if (this.numStacker != 0)
					{
						this.numStacker = 0;
						//Stacker 1
						this.Stacker1.Enabled = false;
						this.Stacker1.IsButton = false;
						//Stacker 2
						this.Stacker2.Enabled = false;
						this.Stacker2.IsButton = false;
						//Stacker 3
						this.pct_STK3NotRdyRun.Visible = false;
						this.pct_STK3NotRdyAOC.Visible = false;
						this.Stacker3.Visible = false;
						this.Stacker3.Enabled = false;
						this.Stacker3.IsButton = false;
					}
					break;
				case 1:
					if (this.numStacker != 1)
					{
						this.numStacker = 1;
						//Stacker 1
						this.Stacker1.Enabled = true;
						this.Stacker1.IsButton = true;
						//Stacker 2
						this.Stacker2.Enabled = false;
						this.Stacker2.IsButton = false;
						//Stacker 3
						this.pct_STK3NotRdyRun.Visible = false;
						this.pct_STK3NotRdyAOC.Visible = false;
						this.Stacker3.Visible = false;
						this.Stacker3.Enabled = false;
						this.Stacker3.IsButton = false;
					}
					break;
				case 3:
					if (this.numStacker != 3)
					{
						this.numStacker = 3;

            int X = 0;
            if (this.corrugatorDirectionLeft)
            {
              X = -12;
            }
            else
            {
              X = 12;
            } 
            
            int WidthBoardAtDB = this.pct_BoardAtRSH.Width - Math.Abs(X);

            ////move all
            this.pct_BoardAtRSH.Width = WidthBoardAtDB;

						this.Shear.Location = new Point((this.Shear.Location.X - X), this.Shear.Location.Y);
						this.btn_IncDecSpeedRSH.Location = new Point((this.btn_IncDecSpeedRSH.Location.X - X), this.btn_IncDecSpeedRSH.Location.Y);
						this.pct_RSHNotRdyAOC.Location = new Point((this.pct_RSHNotRdyAOC.Location.X - X), this.pct_RSHNotRdyAOC.Location.Y);
						this.pct_RSHNotRdyRun.Location = new Point((this.pct_RSHNotRdyRun.Location.X - X), this.pct_RSHNotRdyRun.Location.Y);
            this.pct_RSHScrapStatus.Location = new Point((this.pct_RSHScrapStatus.Location.X - X), this.pct_RSHScrapStatus.Location.Y);

            if (this.corrugatorDirectionLeft)
            {
              X = X - 12;
            }
            else
            {
              X = X + 12;
            }
						
            this.SlitterScorer.Location = new Point((this.SlitterScorer.Location.X - X), this.SlitterScorer.Location.Y);
						this.btn_IncDecSpeedSS.Location = new Point((this.btn_IncDecSpeedSS.Location.X - X), this.btn_IncDecSpeedSS.Location.Y);
						this.pct_SSNotRdyAOC.Location = new Point((this.pct_SSNotRdyAOC.Location.X - X), this.pct_SSNotRdyAOC.Location.Y);
						this.pct_SSNotRdyRun.Location = new Point((this.pct_SSNotRdyRun.Location.X - X), this.pct_SSNotRdyRun.Location.Y);

            int XBoardAtMW = this.pct_BoardAtRSH.Location.X + this.pct_BoardAtRSH.Width - 2;
            this.pct_BoardAtMW.Location = new Point(XBoardAtMW, this.pct_BoardAtMW.Location.Y);

            int WidthBoardAtMW = this.pct_BoardAtMW.Width - Math.Abs(2 * X);
            this.pct_BoardAtMW.Width = WidthBoardAtMW;

            if (this.corrugatorDirectionLeft)
            {
              X = X - 12;
            }
            else
            {
              X = X + 12;
            }
						
            this.Knife.Location = new Point((this.Knife.Location.X - X), this.Knife.Location.Y);
						this.btn_IncDecSpeedKN.Location = new Point((this.btn_IncDecSpeedKN.Location.X - X), this.btn_IncDecSpeedKN.Location.Y);
						this.pct_KNIFENotRdyAOC.Location = new Point((this.pct_KNIFENotRdyAOC.Location.X - X), this.pct_KNIFENotRdyAOC.Location.Y);
						this.pct_KNIFENotRdyRun.Location = new Point((this.pct_KNIFENotRdyRun.Location.X - X), this.pct_KNIFENotRdyRun.Location.Y);
            
            int XBoardAtKnife = this.pct_BoardAtMW.Location.X + this.pct_BoardAtMW.Width - 8;
            this.pct_BoardAtKnife.Location = new Point(XBoardAtKnife, this.pct_BoardAtKnife.Location.Y);

            int WidthBoardAtKnife = this.pct_BoardAtKnife.Width ;
            this.pct_BoardAtKnife.Width = WidthBoardAtKnife;

            if (this.corrugatorDirectionLeft)
            {
              X = X - 12;
            }
            else
            {
              X = X + 12;
            }
						
            this.Stacker1.Location = new Point((this.Stacker1.Location.X - X), this.Stacker1.Location.Y);
						this.btn_IncDecSpeedSTK.Location = new Point((this.btn_IncDecSpeedSTK.Location.X - X), this.btn_IncDecSpeedSTK.Location.Y);
						this.pct_STK1NotRdyAOC.Location = new Point((this.pct_STK1NotRdyAOC.Location.X - X), this.pct_STK1NotRdyAOC.Location.Y);
						this.pct_STK1NotRdyRun.Location = new Point((this.pct_STK1NotRdyRun.Location.X - X), this.pct_STK1NotRdyRun.Location.Y);

            if (this.corrugatorDirectionLeft)
            {
              X = X - 12;
            }
            else
            {
              X = X + 12;
            }
						
            this.Stacker2.Location = new Point((this.Stacker2.Location.X - X), this.Stacker2.Location.Y);
						this.pct_STK2NotRdyAOC.Location = new Point((this.pct_STK2NotRdyAOC.Location.X - X), this.pct_STK2NotRdyAOC.Location.Y);
						this.pct_STK2NotRdyRun.Location = new Point((this.pct_STK2NotRdyRun.Location.X - X), this.pct_STK2NotRdyRun.Location.Y);

            if (this.corrugatorDirectionLeft)
            {
              X = X - 12;
            }
            else
            {
              X = X + 12;
            }
						
            this.Stacker3.Location = new Point((this.Stacker3.Location.X - X), this.Stacker3.Location.Y);
						this.pct_STK3NotRdyAOC.Location = new Point((this.pct_STK3NotRdyAOC.Location.X - X), this.pct_STK3NotRdyAOC.Location.Y);
						this.pct_STK3NotRdyRun.Location = new Point((this.pct_STK3NotRdyRun.Location.X - X), this.pct_STK3NotRdyRun.Location.Y);
						this.pct_BoardAtKnifeLevel3.Location = new Point((this.pct_BoardAtKnife.Location.X + this.pct_BoardAtKnife.Size.Width - 1), this.pct_BoardAtKnife.Location.Y);
						this.pct_BoardAtKnifeLevel3.Visible = true;
						
            //Stacker 1
						this.Stacker1.Enabled = true;
						this.Stacker1.IsButton = true;
						//Stacker 2
						this.Stacker2.Enabled = true;
						this.Stacker2.IsButton = true;
						//Stacker 3
						this.Stacker3.Visible = true;
						this.Stacker3.Enabled = true;
						this.Stacker3.IsButton = true;

            //BringToFront
            this.pct_DBNotRdyAOC.BringToFront();
            this.pct_DBNotRdyRun.BringToFront();

            this.pct_RSHNotRdyAOC.BringToFront();
            this.pct_RSHNotRdyRun.BringToFront();

            this.pct_SSNotRdyAOC.BringToFront();
            this.pct_SSNotRdyRun.BringToFront();

            this.pct_KNIFENotRdyAOC.BringToFront();
            this.pct_KNIFENotRdyRun.BringToFront();

            this.pct_STK1NotRdyAOC.BringToFront();
            this.pct_STK1NotRdyRun.BringToFront();

            this.pct_STK2NotRdyAOC.BringToFront();
            this.pct_STK2NotRdyRun.BringToFront();

            this.pct_STK3NotRdyAOC.BringToFront();
            this.pct_STK3NotRdyRun.BringToFront();
					}
					break;
				default:
					break;
			}
		}

		public override void ConfigPage(Settings.Configuration config)
		{
			base.ConfigPage(config);
			if (!Settings.getSettings().configuration.WetEndControl)
				ttl_WetEndTitle.ForeColor = ttl_WetEndTitle.BackColor;
		}

		private void _setDRYStatus(bool isRSHScrapEnable, bool isRSHisScrapping, bool isAOCAuto, bool isAOCSemi, bool isAOCManual, bool isAOCNoGap, bool isAOCTandem, bool isAocKnifeOnwards, bool isWebAtRSH, bool isWebAtMW, bool isWebAtKnife)
		{
			this.pct_RSHScrapStatus.Image = isRSHScrapEnable ? this.iml_RSHStatus.Images[0] : this.iml_RSHStatus.Images[1];

      //Aggiunta icona sulla barra di stato dry end per evidenziare il tipo di cambio ordine TANDEM (abello_FI12-02-09)
			this.lbl_AOCType_NN.Text = "";
			if (isAOCTandem)
      {
        this.pct_AOCType.Image = this.iml_AOCType.Images[2];
      }
      else
      {
        this.pct_AOCType.Image = isAOCNoGap ? this.iml_AOCType.Images[0] : this.iml_AOCType.Images[1];
      }

			if (isAOCAuto)
				this.pct_AOCStatus.Image = this.iml_AOCStatus.Images[1];
			else if (isAOCSemi)
				this.pct_AOCStatus.Image = this.iml_AOCStatus.Images[0];
			else if (isAOCManual)
				this.pct_AOCStatus.Image = this.iml_AOCStatus.Images[2];

      //if(isAOCTandem)
      //  this.lbl_AOCType_NN.Text = "T ";
      //else
      //  this.lbl_AOCType_NN.Text = " ";

			if(isAocKnifeOnwards)
				this.lbl_AOCType_NN.Text += "K ";
			else
				this.lbl_AOCType_NN.Text += " ";

			this.pct_BoardAtRSH.BackColor = isWebAtRSH ? Color.White : Color.Red;
			this.pct_BoardAtMW.BackColor = isWebAtMW ? Color.White : Color.Red;
			this.pct_BoardAtKnife.BackColor = isWebAtKnife ? Color.White : Color.Red;
			this.pct_BoardAtKnifeLevel3.BackColor = isWebAtKnife ? Color.White : Color.Red;
		}

		private void gdv_SetupData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			string LEVEL_SEPARATOR = " - ";
			string MEASURE_SEPARATOR = "X";

      DataRow dataRow = ((DataRowView)gdv_SetupData.Rows[e.RowIndex].DataBoundItem).Row;

			if (this.gdv_SetupData.Columns[e.ColumnIndex].Name == Order.DEL_WEB_WIDTH
						&& e.Value != null)
			{
				try
				{
					e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_SetupData_CellFormatting - " + Order.DEL_WEB_WIDTH + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}

      if (this.gdv_SetupData.Columns[e.ColumnIndex].Name == Order.DEL_TRIM
            && e.Value != null)
      {
        try
        {
          e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_SetupData_CellFormatting - " + Order.DEL_TRIM + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
        }
      }

			if (this.gdv_SetupData.Columns[e.ColumnIndex].Name == Order.DEL_FREE1
						&& e.Value != null)
			{
				try
				{
          e.Value = ((Convert.ToInt32(e.Value) *
            ((Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_0]) /
            Convert.ToInt32(dataRow[Order.DEL_OUTS_0])))) / 10000);

				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_SetupData_CellFormatting - " + Order.DEL_FREE1 + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}


			if (this.gdv_SetupData.Columns[e.ColumnIndex].Name == Order.DEL_SHEET_LENGTH_0
						&& e.Value != null)
			{
				try
				{
					if (Convert.ToInt32(e.Value) != 0)
					{
            string level = SyncroC.Utility.FosbersoftEnums.LevelNameToNumber(setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_LEVEL_NAME_0].ToString());

						e.Value = level + LEVEL_SEPARATOR +
										SyncroConverter.ConvertString(setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + MEASURE_SEPARATOR +
										SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + MEASURE_SEPARATOR +
                    setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_OUTS_0].ToString();
					}
					else
						e.Value = "";
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_SetupData_CellFormatting - " + Order.DEL_SHEET_LENGTH_0 + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}

			}

			if (this.gdv_SetupData.Columns[e.ColumnIndex].Name == Order.DEL_SHEET_LENGTH_1
						&& e.Value != null)
			{
				try
				{
					if (Convert.ToInt32(e.Value) != 0)
					{
            string level = SyncroC.Utility.FosbersoftEnums.LevelNameToNumber(setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_LEVEL_NAME_1].ToString());

						e.Value = level + LEVEL_SEPARATOR +
										SyncroConverter.ConvertString(setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + MEASURE_SEPARATOR +
										SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + MEASURE_SEPARATOR +
                    setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_OUTS_1].ToString();
					}
					else
						e.Value = "";
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_SetupData_CellFormatting - " + Order.DEL_SHEET_LENGTH_1 + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}

			if (this.gdv_SetupData.Columns[e.ColumnIndex].Name == Order.DEL_SHEET_LENGTH_2
						&& e.Value != null)
			{
				try
				{
					if (Convert.ToInt32(e.Value) != 0)
					{
            string level = SyncroC.Utility.FosbersoftEnums.LevelNameToNumber(setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_LEVEL_NAME_2].ToString());

						e.Value = level + LEVEL_SEPARATOR +
										SyncroConverter.ConvertString(setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + MEASURE_SEPARATOR +
										SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + MEASURE_SEPARATOR +
                    setups.Tables[Order.DRYEND_LIST].Rows[e.RowIndex][Order.DEL_OUTS_2].ToString();
					}
					else
						e.Value = "";
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_SetupData_CellFormatting - " + Order.DEL_SHEET_LENGTH_2 + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}
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
			this.gdv_SetupData.DataSource = null;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatusBarDryEnd));
      this.prb_DryEnd = new SyncroC.Controls.ProgBar();
      this.tlb_DryEndSetup = new SyncroC.Controls.TitledLabel();
      this.tlb_DryEndMetersLeft = new SyncroC.Controls.TitledLabel();
      this.ttl_DryEndTitle = new SyncroC.Controls.Title();
      this.lbl_AOCType_NN = new System.Windows.Forms.Label();
      this.gdv_SetupData = new SyncroC.Controls.GridView();
      this.nud_LineSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tmt_AOCTimeLeft = new SyncroC.Controls.TitledMaskTextBox();
      this.lbl_AOCMode_NN = new System.Windows.Forms.Label();
      this.tlb_WetEndMetersLeft = new SyncroC.Controls.TitledLabel();
      this.ttl_WetEndTitle = new SyncroC.Controls.Title();
      this.pnl_clickExtension = new System.Windows.Forms.Panel();
      this.pnl_clickExtension2 = new System.Windows.Forms.Panel();
      this.stateButton1 = new SyncroC.Controls.StateButton();
      this.iml_RSHStatus = new System.Windows.Forms.ImageList(this.components);
      this.iml_AOCStatus = new System.Windows.Forms.ImageList(this.components);
      this.iml_AOCType = new System.Windows.Forms.ImageList(this.components);
      this.pct_AOCStatus = new System.Windows.Forms.PictureBox();
      this.pct_AOCType = new System.Windows.Forms.PictureBox();
      this.SlitterScorer = new SyncroC.Controls.StateButton();
      this.pct_RSHScrapStatus = new System.Windows.Forms.PictureBox();
      this.btn_IncDecSpeedDB = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSS = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedDEConsole = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedRSH = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedKN = new SyncroC.Controls.StateButton();
      this.btn_IncDecSpeedSTK = new SyncroC.Controls.StateButton();
      this.pct_DBNotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_STK2NotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_STK2NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_STK1NotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_STK1NotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_KNIFENotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_KNIFENotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_RSHNotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_RSHNotRdyRun = new System.Windows.Forms.PictureBox();
      this.pct_DBNotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_SSNotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_SSNotRdyRun = new System.Windows.Forms.PictureBox();
      this.DryEnd = new SyncroC.Controls.StateButton();
      this.Stacker2 = new SyncroC.Controls.StateButton();
      this.Stacker1 = new SyncroC.Controls.StateButton();
      this.Knife = new SyncroC.Controls.StateButton();
      this.DoubleBacker = new SyncroC.Controls.StateButton();
      this.pct_BoardAtKnife = new System.Windows.Forms.PictureBox();
      this.pct_BoardAtMW = new System.Windows.Forms.PictureBox();
      this.pct_BoardAtRSH = new System.Windows.Forms.PictureBox();
      this.Shear = new SyncroC.Controls.StateButton();
      this.Stacker3 = new SyncroC.Controls.StateButton();
      this.pct_STK3NotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_STK3NotRdyRun = new System.Windows.Forms.PictureBox();
      this.ttl_SimulationTitle = new SyncroC.Controls.Title();
      this.pct_BoardAtKnifeLevel3 = new System.Windows.Forms.PictureBox();
      this.pct_maintenance = new System.Windows.Forms.PictureBox();
      this.pct_SYCNotRdyAOC = new System.Windows.Forms.PictureBox();
      this.pct_SYCNotRdyRun = new System.Windows.Forms.PictureBox();
      this.grp_now_percheMiSonoFermato = new SyncroC.Controls.GroupPanel.GroupPanel();
      this.pct_now = new System.Windows.Forms.PictureBox();
      this.lbl_now_numeroMacchina_percheMiSonoFermato = new System.Windows.Forms.Label();
      this.pct_now_causa_percheMiSonoFermato = new System.Windows.Forms.PictureBox();
      this.pnl_clickExtension2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_AOCStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_AOCType)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RSHScrapStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DBNotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK2NotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK2NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK1NotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK1NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KNIFENotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KNIFENotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RSHNotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RSHNotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DBNotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SSNotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SSNotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtKnife)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtMW)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtRSH)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK3NotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK3NotRdyRun)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtKnifeLevel3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_maintenance)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SYCNotRdyAOC)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SYCNotRdyRun)).BeginInit();
      this.grp_now_percheMiSonoFermato.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now_causa_percheMiSonoFermato)).BeginInit();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // prb_DryEnd
      // 
      this.prb_DryEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.prb_DryEnd.BarStyle = SyncroC.Controls.BarStyles.Classic;
      this.prb_DryEnd.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.prb_DryEnd.Font = new System.Drawing.Font("Arial Unicode MS", 15F, System.Drawing.FontStyle.Bold);
      this.prb_DryEnd.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.prb_DryEnd.Location = new System.Drawing.Point(669, 25);
      this.prb_DryEnd.Name = "prb_DryEnd";
      this.prb_DryEnd.Percentage = 30;
      this.prb_DryEnd.PercentageVisible = true;
      this.prb_DryEnd.Size = new System.Drawing.Size(607, 24);
      this.prb_DryEnd.Style = SyncroC.Controls.ProgBarStyles.Big;
      this.prb_DryEnd.TabIndex = 236;
      this.prb_DryEnd.TranslateText = false;
      // 
      // tlb_DryEndSetup
      // 
      this.tlb_DryEndSetup.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_DryEndSetup.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_DryEndSetup.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_DryEndSetup.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_DryEndSetup.ImagePassword = null;
      this.tlb_DryEndSetup.Location = new System.Drawing.Point(926, 185);
      this.tlb_DryEndSetup.Name = "tlb_DryEndSetup";
      this.tlb_DryEndSetup.Size = new System.Drawing.Size(90, 83);
      this.tlb_DryEndSetup.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_DryEndSetup.TabIndex = 234;
      this.tlb_DryEndSetup.Text = "SETUP";
      this.tlb_DryEndSetup.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_DryEndSetup.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DryEndSetup.TranslateText = true;
      this.tlb_DryEndSetup.Value = "0";
      this.tlb_DryEndSetup.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DryEndSetup.ValueMultiline = true;
      // 
      // tlb_DryEndMetersLeft
      // 
      this.tlb_DryEndMetersLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_DryEndMetersLeft.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_DryEndMetersLeft.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_DryEndMetersLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_DryEndMetersLeft.ImagePassword = null;
      this.tlb_DryEndMetersLeft.Location = new System.Drawing.Point(790, 185);
      this.tlb_DryEndMetersLeft.Name = "tlb_DryEndMetersLeft";
      this.tlb_DryEndMetersLeft.Size = new System.Drawing.Size(130, 83);
      this.tlb_DryEndMetersLeft.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_DryEndMetersLeft.TabIndex = 233;
      this.tlb_DryEndMetersLeft.Text = "DRY MT LEFT";
      this.tlb_DryEndMetersLeft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_DryEndMetersLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DryEndMetersLeft.TranslateText = true;
      this.tlb_DryEndMetersLeft.Value = "1500";
      this.tlb_DryEndMetersLeft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DryEndMetersLeft.ValueMultiline = true;
      this.tlb_DryEndMetersLeft.ValueChanged += new System.EventHandler(this.orderChangeTimeLeftCalculation);
      // 
      // ttl_DryEndTitle
      // 
      this.ttl_DryEndTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.ttl_DryEndTitle.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_DryEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DryEndTitle.ForeColor = System.Drawing.Color.Aqua;
      this.ttl_DryEndTitle.Location = new System.Drawing.Point(4, 25);
      this.ttl_DryEndTitle.Name = "ttl_DryEndTitle";
      this.ttl_DryEndTitle.Size = new System.Drawing.Size(80, 243);
      this.ttl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_DryEndTitle.TabIndex = 230;
      this.ttl_DryEndTitle.Text = "DRY";
      this.ttl_DryEndTitle.TranslateText = false;
      // 
      // lbl_AOCType_NN
      // 
      this.lbl_AOCType_NN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.lbl_AOCType_NN.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AOCType_NN.ForeColor = System.Drawing.Color.Red;
      this.lbl_AOCType_NN.Location = new System.Drawing.Point(374, 25);
      this.lbl_AOCType_NN.Name = "lbl_AOCType_NN";
      this.lbl_AOCType_NN.Size = new System.Drawing.Size(291, 24);
      this.lbl_AOCType_NN.TabIndex = 229;
      this.lbl_AOCType_NN.Text = "K";
      this.lbl_AOCType_NN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // gdv_SetupData
      // 
      this.gdv_SetupData.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_SetupData.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SetupData.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_SetupData.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SetupData.AutoGenerateColumns = false;
      this.gdv_SetupData.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_SetupData.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_SetupData.DataMember = "";
      this.gdv_SetupData.DataSource = null;
      this.gdv_SetupData.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_SetupData.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.gdv_SetupData.ImagePassword = null;
      this.gdv_SetupData.Location = new System.Drawing.Point(12, 60);
      this.gdv_SetupData.MultiSelect = false;
      this.gdv_SetupData.Name = "gdv_SetupData";
      this.gdv_SetupData.RowsHeight = 24;
      this.gdv_SetupData.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.gdv_SetupData.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_SetupData.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SetupData.Size = new System.Drawing.Size(1264, 96);
      this.gdv_SetupData.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_SetupData.TabIndex = 250;
      this.gdv_SetupData.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_SetupData.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SetupData.TranslateText = true;
      this.gdv_SetupData.DoubleClick += new System.EventHandler(this.gdv_SetupData_DoubleClick);
      this.gdv_SetupData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_SetupData_CellFormatting);
      // 
      // nud_LineSpeed
      // 
      this.nud_LineSpeed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_LineSpeed.DecimalPlaces = 0;
      this.nud_LineSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LineSpeed.ForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_LineSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_LineSpeed.ImagePassword = null;
      this.nud_LineSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LineSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_LineSpeed.Location = new System.Drawing.Point(1168, 185);
      this.nud_LineSpeed.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
      this.nud_LineSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LineSpeed.Name = "nud_LineSpeed";
      this.nud_LineSpeed.ReadOnly = true;
      this.nud_LineSpeed.Size = new System.Drawing.Size(100, 83);
      this.nud_LineSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Framed;
      this.nud_LineSpeed.TabIndex = 251;
      this.nud_LineSpeed.Text = "SPEED";
      this.nud_LineSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LineSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LineSpeed.TranslateText = true;
      this.nud_LineSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_LineSpeed.UnitMeasureVisible = false;
      this.nud_LineSpeed.Value = "0";
      this.nud_LineSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_LineSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_LineSpeed.ValueChanged += new System.EventHandler(this.orderChangeTimeLeftCalculation);
      // 
      // tmt_AOCTimeLeft
      // 
      this.tmt_AOCTimeLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tmt_AOCTimeLeft.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_AOCTimeLeft.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_AOCTimeLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tmt_AOCTimeLeft.ImagePassword = null;
      this.tmt_AOCTimeLeft.Location = new System.Drawing.Point(1022, 185);
      this.tmt_AOCTimeLeft.Name = "tmt_AOCTimeLeft";
      this.tmt_AOCTimeLeft.PasswordChar = '\0';
      this.tmt_AOCTimeLeft.ReadOnly = true;
      this.tmt_AOCTimeLeft.Size = new System.Drawing.Size(140, 83);
      this.tmt_AOCTimeLeft.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Framed;
      this.tmt_AOCTimeLeft.TabIndex = 252;
      this.tmt_AOCTimeLeft.Text = "TIME LEFT";
      this.tmt_AOCTimeLeft.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
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
      // lbl_AOCMode_NN
      // 
      this.lbl_AOCMode_NN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.lbl_AOCMode_NN.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AOCMode_NN.ForeColor = System.Drawing.Color.Aqua;
      this.lbl_AOCMode_NN.Location = new System.Drawing.Point(167, 25);
      this.lbl_AOCMode_NN.Name = "lbl_AOCMode_NN";
      this.lbl_AOCMode_NN.Size = new System.Drawing.Size(203, 24);
      this.lbl_AOCMode_NN.TabIndex = 253;
      this.lbl_AOCMode_NN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_AOCMode_NN.Click += new System.EventHandler(this.lbl_AOCMode_NN_Click);
      // 
      // tlb_WetEndMetersLeft
      // 
      this.tlb_WetEndMetersLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_WetEndMetersLeft.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_WetEndMetersLeft.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_WetEndMetersLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_WetEndMetersLeft.ImagePassword = null;
      this.tlb_WetEndMetersLeft.Location = new System.Drawing.Point(654, 185);
      this.tlb_WetEndMetersLeft.Name = "tlb_WetEndMetersLeft";
      this.tlb_WetEndMetersLeft.Size = new System.Drawing.Size(130, 83);
      this.tlb_WetEndMetersLeft.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_WetEndMetersLeft.TabIndex = 272;
      this.tlb_WetEndMetersLeft.Text = "WET MT LEFT";
      this.tlb_WetEndMetersLeft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_WetEndMetersLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WetEndMetersLeft.TranslateText = true;
      this.tlb_WetEndMetersLeft.Value = "1500";
      this.tlb_WetEndMetersLeft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WetEndMetersLeft.ValueMultiline = true;
      // 
      // ttl_WetEndTitle
      // 
      this.ttl_WetEndTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(82)))));
      this.ttl_WetEndTitle.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_WetEndTitle.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ttl_WetEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_WetEndTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_WetEndTitle.Location = new System.Drawing.Point(84, 25);
      this.ttl_WetEndTitle.Name = "ttl_WetEndTitle";
      this.ttl_WetEndTitle.Size = new System.Drawing.Size(79, 24);
      this.ttl_WetEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_WetEndTitle.TabIndex = 363;
      this.ttl_WetEndTitle.Text = "WET";
      this.ttl_WetEndTitle.TranslateText = false;
      this.ttl_WetEndTitle.Click += new System.EventHandler(this.ttl_WetEndTitle_Click);
      // 
      // pnl_clickExtension
      // 
      this.pnl_clickExtension.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pnl_clickExtension.Location = new System.Drawing.Point(59, 0);
      this.pnl_clickExtension.Name = "pnl_clickExtension";
      this.pnl_clickExtension.Size = new System.Drawing.Size(101, 65);
      this.pnl_clickExtension.TabIndex = 364;
      this.pnl_clickExtension.Click += new System.EventHandler(this.ttl_WetEndTitle_Click);
      // 
      // pnl_clickExtension2
      // 
      this.pnl_clickExtension2.Controls.Add(this.stateButton1);
      this.pnl_clickExtension2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pnl_clickExtension2.Location = new System.Drawing.Point(166, 0);
      this.pnl_clickExtension2.Name = "pnl_clickExtension2";
      this.pnl_clickExtension2.Size = new System.Drawing.Size(203, 65);
      this.pnl_clickExtension2.TabIndex = 365;
      this.pnl_clickExtension2.Click += new System.EventHandler(this.lbl_AOCMode_NN_Click);
      // 
      // stateButton1
      // 
      this.stateButton1.Active = true;
      this.stateButton1.Alarmed = false;
      this.stateButton1.Auto = true;
      this.stateButton1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stateButton1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.stateButton1.FillBackColorWithImage = false;
      this.stateButton1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stateButton1.FramePresent = false;
      this.stateButton1.HighLightEnable = false;
      this.stateButton1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stateButton1.ImagePassword = null;
      this.stateButton1.Inemergency = false;
      this.stateButton1.InLocal = true;
      this.stateButton1.IsButton = false;
      this.stateButton1.IsForceToInvisible = false;
      this.stateButton1.Location = new System.Drawing.Point(62, 112);
      this.stateButton1.Name = "stateButton1";
      this.stateButton1.OnLine = true;
      this.stateButton1.ShowImage = true;
      this.stateButton1.ShowText = false;
      this.stateButton1.Size = new System.Drawing.Size(30, 23);
      this.stateButton1.Switching = false;
      this.stateButton1.TabIndex = 360;
      this.stateButton1.TranslateText = false;
      this.stateButton1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stateButton1.UserDefinedActiveImage")));
      this.stateButton1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stateButton1.UserDefinedImage")));
      this.stateButton1.Visible = false;
      this.stateButton1.Warned = false;
      // 
      // iml_RSHStatus
      // 
      this.iml_RSHStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_RSHStatus.ImageStream")));
      this.iml_RSHStatus.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_RSHStatus.Images.SetKeyName(0, "RSHScrapEnabled.bmp");
      this.iml_RSHStatus.Images.SetKeyName(1, "RSHScrapDisabled.bmp");
      // 
      // iml_AOCStatus
      // 
      this.iml_AOCStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AOCStatus.ImageStream")));
      this.iml_AOCStatus.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_AOCStatus.Images.SetKeyName(0, "AOC_Semi.bmp");
      this.iml_AOCStatus.Images.SetKeyName(1, "AOC_Auto.bmp");
      this.iml_AOCStatus.Images.SetKeyName(2, "AOC_Manual.bmp");
      // 
      // iml_AOCType
      // 
      this.iml_AOCType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AOCType.ImageStream")));
      this.iml_AOCType.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_AOCType.Images.SetKeyName(0, "AOC_NOGAP.bmp");
      this.iml_AOCType.Images.SetKeyName(1, "AOC_GAP.bmp");
      this.iml_AOCType.Images.SetKeyName(2, "AOC_TANDEM.bmp");
      // 
      // pct_AOCStatus
      // 
      this.pct_AOCStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pct_AOCStatus.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_AOCStatus.Location = new System.Drawing.Point(206, 25);
      this.pct_AOCStatus.Name = "pct_AOCStatus";
      this.pct_AOCStatus.Size = new System.Drawing.Size(93, 24);
      this.pct_AOCStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_AOCStatus.TabIndex = 375;
      this.pct_AOCStatus.TabStop = false;
      this.pct_AOCStatus.Click += new System.EventHandler(this.lbl_AOCMode_NN_Click);
      // 
      // pct_AOCType
      // 
      this.pct_AOCType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pct_AOCType.Location = new System.Drawing.Point(458, 25);
      this.pct_AOCType.Name = "pct_AOCType";
      this.pct_AOCType.Size = new System.Drawing.Size(86, 24);
      this.pct_AOCType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_AOCType.TabIndex = 374;
      this.pct_AOCType.TabStop = false;
      // 
      // SlitterScorer
      // 
      this.SlitterScorer.Active = false;
      this.SlitterScorer.Alarmed = false;
      this.SlitterScorer.Auto = false;
      this.SlitterScorer.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SlitterScorer.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SlitterScorer.FillBackColorWithImage = false;
      this.SlitterScorer.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.SlitterScorer.FramePresent = false;
      this.SlitterScorer.HighLightEnable = false;
      this.SlitterScorer.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SlitterScorer.ImagePassword = null;
      this.SlitterScorer.Inemergency = false;
      this.SlitterScorer.InLocal = false;
      this.SlitterScorer.IsButton = true;
      this.SlitterScorer.IsForceToInvisible = false;
      this.SlitterScorer.Location = new System.Drawing.Point(198, 185);
      this.SlitterScorer.Name = "SlitterScorer";
      this.SlitterScorer.OnLine = false;
      this.SlitterScorer.ShowImage = true;
      this.SlitterScorer.ShowText = false;
      this.SlitterScorer.Size = new System.Drawing.Size(72, 60);
      this.SlitterScorer.Switching = false;
      this.SlitterScorer.TabIndex = 239;
      this.SlitterScorer.TranslateText = false;
      this.SlitterScorer.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SlitterScorer.UserDefinedActiveImage")));
      this.SlitterScorer.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SlitterScorer.UserDefinedImage")));
      this.SlitterScorer.Warned = false;
      this.SlitterScorer.Click += new System.EventHandler(this.SlitterScorer_Click);
      // 
      // pct_RSHScrapStatus
      // 
      this.pct_RSHScrapStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pct_RSHScrapStatus.Location = new System.Drawing.Point(144, 165);
      this.pct_RSHScrapStatus.Name = "pct_RSHScrapStatus";
      this.pct_RSHScrapStatus.Size = new System.Drawing.Size(30, 18);
      this.pct_RSHScrapStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_RSHScrapStatus.TabIndex = 373;
      this.pct_RSHScrapStatus.TabStop = false;
      this.pct_RSHScrapStatus.Click += new System.EventHandler(this.Shear_Click);
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
      this.btn_IncDecSpeedDB.Location = new System.Drawing.Point(21, 247);
      this.btn_IncDecSpeedDB.Name = "btn_IncDecSpeedDB";
      this.btn_IncDecSpeedDB.OnLine = true;
      this.btn_IncDecSpeedDB.ShowImage = true;
      this.btn_IncDecSpeedDB.ShowText = false;
      this.btn_IncDecSpeedDB.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedDB.Switching = false;
      this.btn_IncDecSpeedDB.TabIndex = 372;
      this.btn_IncDecSpeedDB.TranslateText = false;
      this.btn_IncDecSpeedDB.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedDB.UserDefinedActiveImage")));
      this.btn_IncDecSpeedDB.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedDB.UserDefinedImage")));
      this.btn_IncDecSpeedDB.Visible = false;
      this.btn_IncDecSpeedDB.Warned = false;
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
      this.btn_IncDecSpeedSS.Location = new System.Drawing.Point(202, 246);
      this.btn_IncDecSpeedSS.Name = "btn_IncDecSpeedSS";
      this.btn_IncDecSpeedSS.OnLine = true;
      this.btn_IncDecSpeedSS.ShowImage = true;
      this.btn_IncDecSpeedSS.ShowText = false;
      this.btn_IncDecSpeedSS.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSS.Switching = false;
      this.btn_IncDecSpeedSS.TabIndex = 366;
      this.btn_IncDecSpeedSS.TranslateText = false;
      this.btn_IncDecSpeedSS.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSS.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSS.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSS.UserDefinedImage")));
      this.btn_IncDecSpeedSS.Visible = false;
      this.btn_IncDecSpeedSS.Warned = false;
      // 
      // btn_IncDecSpeedDEConsole
      // 
      this.btn_IncDecSpeedDEConsole.Active = true;
      this.btn_IncDecSpeedDEConsole.Alarmed = false;
      this.btn_IncDecSpeedDEConsole.Auto = true;
      this.btn_IncDecSpeedDEConsole.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedDEConsole.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedDEConsole.FillBackColorWithImage = false;
      this.btn_IncDecSpeedDEConsole.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedDEConsole.FramePresent = false;
      this.btn_IncDecSpeedDEConsole.HighLightEnable = false;
      this.btn_IncDecSpeedDEConsole.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedDEConsole.ImagePassword = null;
      this.btn_IncDecSpeedDEConsole.Inemergency = false;
      this.btn_IncDecSpeedDEConsole.InLocal = true;
      this.btn_IncDecSpeedDEConsole.IsButton = false;
      this.btn_IncDecSpeedDEConsole.IsForceToInvisible = false;
      this.btn_IncDecSpeedDEConsole.Location = new System.Drawing.Point(572, 246);
      this.btn_IncDecSpeedDEConsole.Name = "btn_IncDecSpeedDEConsole";
      this.btn_IncDecSpeedDEConsole.OnLine = true;
      this.btn_IncDecSpeedDEConsole.ShowImage = true;
      this.btn_IncDecSpeedDEConsole.ShowText = false;
      this.btn_IncDecSpeedDEConsole.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedDEConsole.Switching = false;
      this.btn_IncDecSpeedDEConsole.TabIndex = 362;
      this.btn_IncDecSpeedDEConsole.TranslateText = false;
      this.btn_IncDecSpeedDEConsole.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedDEConsole.UserDefinedActiveImage")));
      this.btn_IncDecSpeedDEConsole.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedDEConsole.UserDefinedImage")));
      this.btn_IncDecSpeedDEConsole.Visible = false;
      this.btn_IncDecSpeedDEConsole.Warned = false;
      // 
      // btn_IncDecSpeedRSH
      // 
      this.btn_IncDecSpeedRSH.Active = true;
      this.btn_IncDecSpeedRSH.Alarmed = false;
      this.btn_IncDecSpeedRSH.Auto = true;
      this.btn_IncDecSpeedRSH.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedRSH.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedRSH.FillBackColorWithImage = false;
      this.btn_IncDecSpeedRSH.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedRSH.FramePresent = false;
      this.btn_IncDecSpeedRSH.HighLightEnable = false;
      this.btn_IncDecSpeedRSH.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedRSH.ImagePassword = null;
      this.btn_IncDecSpeedRSH.Inemergency = false;
      this.btn_IncDecSpeedRSH.InLocal = true;
      this.btn_IncDecSpeedRSH.IsButton = false;
      this.btn_IncDecSpeedRSH.IsForceToInvisible = false;
      this.btn_IncDecSpeedRSH.Location = new System.Drawing.Point(112, 246);
      this.btn_IncDecSpeedRSH.Name = "btn_IncDecSpeedRSH";
      this.btn_IncDecSpeedRSH.OnLine = true;
      this.btn_IncDecSpeedRSH.ShowImage = true;
      this.btn_IncDecSpeedRSH.ShowText = false;
      this.btn_IncDecSpeedRSH.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedRSH.Switching = false;
      this.btn_IncDecSpeedRSH.TabIndex = 361;
      this.btn_IncDecSpeedRSH.TranslateText = false;
      this.btn_IncDecSpeedRSH.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedRSH.UserDefinedActiveImage")));
      this.btn_IncDecSpeedRSH.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedRSH.UserDefinedImage")));
      this.btn_IncDecSpeedRSH.Visible = false;
      this.btn_IncDecSpeedRSH.Warned = false;
      // 
      // btn_IncDecSpeedKN
      // 
      this.btn_IncDecSpeedKN.Active = true;
      this.btn_IncDecSpeedKN.Alarmed = false;
      this.btn_IncDecSpeedKN.Auto = true;
      this.btn_IncDecSpeedKN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedKN.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedKN.FillBackColorWithImage = false;
      this.btn_IncDecSpeedKN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedKN.FramePresent = false;
      this.btn_IncDecSpeedKN.HighLightEnable = false;
      this.btn_IncDecSpeedKN.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedKN.ImagePassword = null;
      this.btn_IncDecSpeedKN.Inemergency = false;
      this.btn_IncDecSpeedKN.InLocal = true;
      this.btn_IncDecSpeedKN.IsButton = false;
      this.btn_IncDecSpeedKN.IsForceToInvisible = false;
      this.btn_IncDecSpeedKN.Location = new System.Drawing.Point(292, 246);
      this.btn_IncDecSpeedKN.Name = "btn_IncDecSpeedKN";
      this.btn_IncDecSpeedKN.OnLine = true;
      this.btn_IncDecSpeedKN.ShowImage = true;
      this.btn_IncDecSpeedKN.ShowText = false;
      this.btn_IncDecSpeedKN.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedKN.Switching = false;
      this.btn_IncDecSpeedKN.TabIndex = 359;
      this.btn_IncDecSpeedKN.TranslateText = false;
      this.btn_IncDecSpeedKN.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedKN.UserDefinedActiveImage")));
      this.btn_IncDecSpeedKN.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedKN.UserDefinedImage")));
      this.btn_IncDecSpeedKN.Visible = false;
      this.btn_IncDecSpeedKN.Warned = false;
      // 
      // btn_IncDecSpeedSTK
      // 
      this.btn_IncDecSpeedSTK.Active = true;
      this.btn_IncDecSpeedSTK.Alarmed = false;
      this.btn_IncDecSpeedSTK.Auto = true;
      this.btn_IncDecSpeedSTK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_IncDecSpeedSTK.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_IncDecSpeedSTK.FillBackColorWithImage = false;
      this.btn_IncDecSpeedSTK.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_IncDecSpeedSTK.FramePresent = false;
      this.btn_IncDecSpeedSTK.HighLightEnable = false;
      this.btn_IncDecSpeedSTK.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_IncDecSpeedSTK.ImagePassword = null;
      this.btn_IncDecSpeedSTK.Inemergency = false;
      this.btn_IncDecSpeedSTK.InLocal = true;
      this.btn_IncDecSpeedSTK.IsButton = false;
      this.btn_IncDecSpeedSTK.IsForceToInvisible = false;
      this.btn_IncDecSpeedSTK.Location = new System.Drawing.Point(381, 246);
      this.btn_IncDecSpeedSTK.Name = "btn_IncDecSpeedSTK";
      this.btn_IncDecSpeedSTK.OnLine = true;
      this.btn_IncDecSpeedSTK.ShowImage = true;
      this.btn_IncDecSpeedSTK.ShowText = false;
      this.btn_IncDecSpeedSTK.Size = new System.Drawing.Size(30, 23);
      this.btn_IncDecSpeedSTK.Switching = false;
      this.btn_IncDecSpeedSTK.TabIndex = 358;
      this.btn_IncDecSpeedSTK.TranslateText = false;
      this.btn_IncDecSpeedSTK.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSTK.UserDefinedActiveImage")));
      this.btn_IncDecSpeedSTK.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_IncDecSpeedSTK.UserDefinedImage")));
      this.btn_IncDecSpeedSTK.Visible = false;
      this.btn_IncDecSpeedSTK.Warned = false;
      // 
      // pct_DBNotRdyRun
      // 
      this.pct_DBNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_DBNotRdyRun.Image")));
      this.pct_DBNotRdyRun.Location = new System.Drawing.Point(20, 250);
      this.pct_DBNotRdyRun.Name = "pct_DBNotRdyRun";
      this.pct_DBNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_DBNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_DBNotRdyRun.TabIndex = 256;
      this.pct_DBNotRdyRun.TabStop = false;
      // 
      // pct_STK2NotRdyAOC
      // 
      this.pct_STK2NotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_STK2NotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_STK2NotRdyAOC.Image")));
      this.pct_STK2NotRdyAOC.Location = new System.Drawing.Point(502, 251);
      this.pct_STK2NotRdyAOC.Name = "pct_STK2NotRdyAOC";
      this.pct_STK2NotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_STK2NotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_STK2NotRdyAOC.TabIndex = 265;
      this.pct_STK2NotRdyAOC.TabStop = false;
      // 
      // pct_STK2NotRdyRun
      // 
      this.pct_STK2NotRdyRun.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_STK2NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_STK2NotRdyRun.Image")));
      this.pct_STK2NotRdyRun.Location = new System.Drawing.Point(469, 251);
      this.pct_STK2NotRdyRun.Name = "pct_STK2NotRdyRun";
      this.pct_STK2NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_STK2NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_STK2NotRdyRun.TabIndex = 264;
      this.pct_STK2NotRdyRun.TabStop = false;
      // 
      // pct_STK1NotRdyAOC
      // 
      this.pct_STK1NotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_STK1NotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_STK1NotRdyAOC.Image")));
      this.pct_STK1NotRdyAOC.Location = new System.Drawing.Point(414, 251);
      this.pct_STK1NotRdyAOC.Name = "pct_STK1NotRdyAOC";
      this.pct_STK1NotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_STK1NotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_STK1NotRdyAOC.TabIndex = 263;
      this.pct_STK1NotRdyAOC.TabStop = false;
      // 
      // pct_STK1NotRdyRun
      // 
      this.pct_STK1NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_STK1NotRdyRun.Image")));
      this.pct_STK1NotRdyRun.Location = new System.Drawing.Point(381, 251);
      this.pct_STK1NotRdyRun.Name = "pct_STK1NotRdyRun";
      this.pct_STK1NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_STK1NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_STK1NotRdyRun.TabIndex = 262;
      this.pct_STK1NotRdyRun.TabStop = false;
      // 
      // pct_KNIFENotRdyAOC
      // 
      this.pct_KNIFENotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_KNIFENotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_KNIFENotRdyAOC.Image")));
      this.pct_KNIFENotRdyAOC.Location = new System.Drawing.Point(325, 251);
      this.pct_KNIFENotRdyAOC.Name = "pct_KNIFENotRdyAOC";
      this.pct_KNIFENotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_KNIFENotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_KNIFENotRdyAOC.TabIndex = 261;
      this.pct_KNIFENotRdyAOC.TabStop = false;
      // 
      // pct_KNIFENotRdyRun
      // 
      this.pct_KNIFENotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_KNIFENotRdyRun.Image")));
      this.pct_KNIFENotRdyRun.Location = new System.Drawing.Point(292, 251);
      this.pct_KNIFENotRdyRun.Name = "pct_KNIFENotRdyRun";
      this.pct_KNIFENotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_KNIFENotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_KNIFENotRdyRun.TabIndex = 260;
      this.pct_KNIFENotRdyRun.TabStop = false;
      // 
      // pct_RSHNotRdyAOC
      // 
      this.pct_RSHNotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_RSHNotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_RSHNotRdyAOC.Image")));
      this.pct_RSHNotRdyAOC.Location = new System.Drawing.Point(143, 251);
      this.pct_RSHNotRdyAOC.Name = "pct_RSHNotRdyAOC";
      this.pct_RSHNotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_RSHNotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_RSHNotRdyAOC.TabIndex = 259;
      this.pct_RSHNotRdyAOC.TabStop = false;
      // 
      // pct_RSHNotRdyRun
      // 
      this.pct_RSHNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_RSHNotRdyRun.Image")));
      this.pct_RSHNotRdyRun.Location = new System.Drawing.Point(114, 251);
      this.pct_RSHNotRdyRun.Name = "pct_RSHNotRdyRun";
      this.pct_RSHNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_RSHNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_RSHNotRdyRun.TabIndex = 258;
      this.pct_RSHNotRdyRun.TabStop = false;
      // 
      // pct_DBNotRdyAOC
      // 
      this.pct_DBNotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_DBNotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_DBNotRdyAOC.Image")));
      this.pct_DBNotRdyAOC.Location = new System.Drawing.Point(57, 251);
      this.pct_DBNotRdyAOC.Name = "pct_DBNotRdyAOC";
      this.pct_DBNotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_DBNotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_DBNotRdyAOC.TabIndex = 257;
      this.pct_DBNotRdyAOC.TabStop = false;
      // 
      // pct_SSNotRdyAOC
      // 
      this.pct_SSNotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_SSNotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_SSNotRdyAOC.Image")));
      this.pct_SSNotRdyAOC.Location = new System.Drawing.Point(235, 251);
      this.pct_SSNotRdyAOC.Name = "pct_SSNotRdyAOC";
      this.pct_SSNotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_SSNotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SSNotRdyAOC.TabIndex = 255;
      this.pct_SSNotRdyAOC.TabStop = false;
      // 
      // pct_SSNotRdyRun
      // 
      this.pct_SSNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SSNotRdyRun.Image")));
      this.pct_SSNotRdyRun.Location = new System.Drawing.Point(202, 251);
      this.pct_SSNotRdyRun.Name = "pct_SSNotRdyRun";
      this.pct_SSNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SSNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SSNotRdyRun.TabIndex = 254;
      this.pct_SSNotRdyRun.TabStop = false;
      // 
      // DryEnd
      // 
      this.DryEnd.Active = false;
      this.DryEnd.Alarmed = false;
      this.DryEnd.Auto = false;
      this.DryEnd.BackColor = System.Drawing.Color.WhiteSmoke;
      this.DryEnd.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DryEnd.FillBackColorWithImage = false;
      this.DryEnd.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.DryEnd.FramePresent = false;
      this.DryEnd.HighLightEnable = false;
      this.DryEnd.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.DryEnd.ImagePassword = null;
      this.DryEnd.Inemergency = false;
      this.DryEnd.InLocal = false;
      this.DryEnd.IsButton = true;
      this.DryEnd.IsForceToInvisible = false;
      this.DryEnd.Location = new System.Drawing.Point(566, 185);
      this.DryEnd.Name = "DryEnd";
      this.DryEnd.OnLine = false;
      this.DryEnd.ShowImage = true;
      this.DryEnd.ShowText = false;
      this.DryEnd.Size = new System.Drawing.Size(60, 60);
      this.DryEnd.Switching = false;
      this.DryEnd.TabIndex = 243;
      this.DryEnd.TranslateText = false;
      this.DryEnd.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("DryEnd.UserDefinedActiveImage")));
      this.DryEnd.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("DryEnd.UserDefinedImage")));
      this.DryEnd.Warned = false;
      this.DryEnd.Click += new System.EventHandler(this.DryEnd_Click);
      // 
      // Stacker2
      // 
      this.Stacker2.Active = false;
      this.Stacker2.Alarmed = false;
      this.Stacker2.Auto = false;
      this.Stacker2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Stacker2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Stacker2.FillBackColorWithImage = false;
      this.Stacker2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Stacker2.FramePresent = false;
      this.Stacker2.HighLightEnable = false;
      this.Stacker2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Stacker2.ImagePassword = null;
      this.Stacker2.Inemergency = false;
      this.Stacker2.InLocal = false;
      this.Stacker2.IsButton = true;
      this.Stacker2.IsForceToInvisible = false;
      this.Stacker2.Location = new System.Drawing.Point(465, 185);
      this.Stacker2.Name = "Stacker2";
      this.Stacker2.OnLine = false;
      this.Stacker2.ShowImage = true;
      this.Stacker2.ShowText = false;
      this.Stacker2.Size = new System.Drawing.Size(72, 60);
      this.Stacker2.Switching = false;
      this.Stacker2.TabIndex = 242;
      this.Stacker2.TranslateText = false;
      this.Stacker2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Stacker2.UserDefinedActiveImage")));
      this.Stacker2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Stacker2.UserDefinedImage")));
      this.Stacker2.Warned = false;
      this.Stacker2.Click += new System.EventHandler(this.Stacker2_Click);
      // 
      // Stacker1
      // 
      this.Stacker1.Active = false;
      this.Stacker1.Alarmed = false;
      this.Stacker1.Auto = false;
      this.Stacker1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Stacker1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Stacker1.FillBackColorWithImage = false;
      this.Stacker1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Stacker1.FramePresent = false;
      this.Stacker1.HighLightEnable = false;
      this.Stacker1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Stacker1.ImagePassword = null;
      this.Stacker1.Inemergency = false;
      this.Stacker1.InLocal = false;
      this.Stacker1.IsButton = true;
      this.Stacker1.IsForceToInvisible = false;
      this.Stacker1.Location = new System.Drawing.Point(376, 185);
      this.Stacker1.Name = "Stacker1";
      this.Stacker1.OnLine = false;
      this.Stacker1.ShowImage = true;
      this.Stacker1.ShowText = false;
      this.Stacker1.Size = new System.Drawing.Size(72, 60);
      this.Stacker1.Switching = false;
      this.Stacker1.TabIndex = 241;
      this.Stacker1.TranslateText = false;
      this.Stacker1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Stacker1.UserDefinedActiveImage")));
      this.Stacker1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Stacker1.UserDefinedImage")));
      this.Stacker1.Warned = false;
      this.Stacker1.Click += new System.EventHandler(this.Stacker1_Click);
      // 
      // Knife
      // 
      this.Knife.Active = false;
      this.Knife.Alarmed = false;
      this.Knife.Auto = false;
      this.Knife.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Knife.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Knife.FillBackColorWithImage = false;
      this.Knife.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Knife.FramePresent = false;
      this.Knife.HighLightEnable = false;
      this.Knife.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Knife.ImagePassword = null;
      this.Knife.Inemergency = false;
      this.Knife.InLocal = false;
      this.Knife.IsButton = true;
      this.Knife.IsForceToInvisible = false;
      this.Knife.Location = new System.Drawing.Point(287, 185);
      this.Knife.Name = "Knife";
      this.Knife.OnLine = false;
      this.Knife.ShowImage = true;
      this.Knife.ShowText = false;
      this.Knife.Size = new System.Drawing.Size(72, 60);
      this.Knife.Switching = false;
      this.Knife.TabIndex = 240;
      this.Knife.TranslateText = false;
      this.Knife.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Knife.UserDefinedActiveImage")));
      this.Knife.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Knife.UserDefinedImage")));
      this.Knife.Warned = false;
      this.Knife.Click += new System.EventHandler(this.Knife_Click);
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
      this.DoubleBacker.Location = new System.Drawing.Point(20, 185);
      this.DoubleBacker.Name = "DoubleBacker";
      this.DoubleBacker.OnLine = false;
      this.DoubleBacker.ShowImage = true;
      this.DoubleBacker.ShowText = false;
      this.DoubleBacker.Size = new System.Drawing.Size(72, 60);
      this.DoubleBacker.Switching = false;
      this.DoubleBacker.TabIndex = 237;
      this.DoubleBacker.TranslateText = false;
      this.DoubleBacker.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("DoubleBacker.UserDefinedActiveImage")));
      this.DoubleBacker.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("DoubleBacker.UserDefinedImage")));
      this.DoubleBacker.Warned = false;
      this.DoubleBacker.Click += new System.EventHandler(this.DoubleBacker_Click);
      // 
      // pct_BoardAtKnife
      // 
      this.pct_BoardAtKnife.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BoardAtKnife.Image = ((System.Drawing.Image)(resources.GetObject("pct_BoardAtKnife.Image")));
      this.pct_BoardAtKnife.Location = new System.Drawing.Point(270, 251);
      this.pct_BoardAtKnife.Name = "pct_BoardAtKnife";
      this.pct_BoardAtKnife.Size = new System.Drawing.Size(266, 18);
      this.pct_BoardAtKnife.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_BoardAtKnife.TabIndex = 271;
      this.pct_BoardAtKnife.TabStop = false;
      // 
      // pct_BoardAtMW
      // 
      this.pct_BoardAtMW.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BoardAtMW.Image = ((System.Drawing.Image)(resources.GetObject("pct_BoardAtMW.Image")));
      this.pct_BoardAtMW.Location = new System.Drawing.Point(161, 251);
      this.pct_BoardAtMW.Name = "pct_BoardAtMW";
      this.pct_BoardAtMW.Size = new System.Drawing.Size(113, 18);
      this.pct_BoardAtMW.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_BoardAtMW.TabIndex = 270;
      this.pct_BoardAtMW.TabStop = false;
      this.pct_BoardAtMW.Click += new System.EventHandler(this.pct_BoardAtMW_Click);
      // 
      // pct_BoardAtRSH
      // 
      this.pct_BoardAtRSH.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BoardAtRSH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.pct_BoardAtRSH.Image = ((System.Drawing.Image)(resources.GetObject("pct_BoardAtRSH.Image")));
      this.pct_BoardAtRSH.Location = new System.Drawing.Point(20, 251);
      this.pct_BoardAtRSH.Name = "pct_BoardAtRSH";
      this.pct_BoardAtRSH.Size = new System.Drawing.Size(143, 18);
      this.pct_BoardAtRSH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_BoardAtRSH.TabIndex = 269;
      this.pct_BoardAtRSH.TabStop = false;
      // 
      // Shear
      // 
      this.Shear.Active = false;
      this.Shear.Alarmed = false;
      this.Shear.Auto = false;
      this.Shear.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Shear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Shear.FillBackColorWithImage = false;
      this.Shear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Shear.FramePresent = false;
      this.Shear.HighLightEnable = false;
      this.Shear.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Shear.ImagePassword = null;
      this.Shear.Inemergency = false;
      this.Shear.InLocal = false;
      this.Shear.IsButton = true;
      this.Shear.IsForceToInvisible = false;
      this.Shear.Location = new System.Drawing.Point(107, 185);
      this.Shear.Name = "Shear";
      this.Shear.OnLine = false;
      this.Shear.ShowImage = true;
      this.Shear.ShowText = false;
      this.Shear.Size = new System.Drawing.Size(72, 60);
      this.Shear.Switching = false;
      this.Shear.TabIndex = 238;
      this.Shear.TranslateText = false;
      this.Shear.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Shear.UserDefinedActiveImage")));
      this.Shear.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Shear.UserDefinedImage")));
      this.Shear.Warned = false;
      this.Shear.Click += new System.EventHandler(this.Shear_Click);
      // 
      // Stacker3
      // 
      this.Stacker3.Active = false;
      this.Stacker3.Alarmed = false;
      this.Stacker3.Auto = false;
      this.Stacker3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Stacker3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Stacker3.FillBackColorWithImage = false;
      this.Stacker3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Stacker3.FramePresent = false;
      this.Stacker3.HighLightEnable = false;
      this.Stacker3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Stacker3.ImagePassword = null;
      this.Stacker3.Inemergency = false;
      this.Stacker3.InLocal = false;
      this.Stacker3.IsButton = true;
      this.Stacker3.IsForceToInvisible = false;
      this.Stacker3.Location = new System.Drawing.Point(554, 185);
      this.Stacker3.Name = "Stacker3";
      this.Stacker3.OnLine = false;
      this.Stacker3.ShowImage = true;
      this.Stacker3.ShowText = false;
      this.Stacker3.Size = new System.Drawing.Size(72, 60);
      this.Stacker3.Switching = false;
      this.Stacker3.TabIndex = 376;
      this.Stacker3.TranslateText = false;
      this.Stacker3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Stacker3.UserDefinedActiveImage")));
      this.Stacker3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Stacker3.UserDefinedImage")));
      this.Stacker3.Visible = false;
      this.Stacker3.Warned = false;
      this.Stacker3.Click += new System.EventHandler(this.Stacker3_Click);
      // 
      // pct_STK3NotRdyAOC
      // 
      this.pct_STK3NotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_STK3NotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_STK3NotRdyAOC.Image")));
      this.pct_STK3NotRdyAOC.Location = new System.Drawing.Point(557, 250);
      this.pct_STK3NotRdyAOC.Name = "pct_STK3NotRdyAOC";
      this.pct_STK3NotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_STK3NotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_STK3NotRdyAOC.TabIndex = 377;
      this.pct_STK3NotRdyAOC.TabStop = false;
      this.pct_STK3NotRdyAOC.Visible = false;
      // 
      // pct_STK3NotRdyRun
      // 
      this.pct_STK3NotRdyRun.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_STK3NotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_STK3NotRdyRun.Image")));
      this.pct_STK3NotRdyRun.Location = new System.Drawing.Point(549, 250);
      this.pct_STK3NotRdyRun.Name = "pct_STK3NotRdyRun";
      this.pct_STK3NotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_STK3NotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_STK3NotRdyRun.TabIndex = 378;
      this.pct_STK3NotRdyRun.TabStop = false;
      this.pct_STK3NotRdyRun.Visible = false;
      // 
      // ttl_SimulationTitle
      // 
      this.ttl_SimulationTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.ttl_SimulationTitle.color = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.ttl_SimulationTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SimulationTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SimulationTitle.Location = new System.Drawing.Point(657, 159);
      this.ttl_SimulationTitle.Name = "ttl_SimulationTitle";
      this.ttl_SimulationTitle.Size = new System.Drawing.Size(619, 24);
      this.ttl_SimulationTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_SimulationTitle.TabIndex = 441;
      this.ttl_SimulationTitle.Text = "SIMULATION ACTIVE";
      this.ttl_SimulationTitle.TranslateText = false;
      this.ttl_SimulationTitle.Visible = false;
      // 
      // pct_BoardAtKnifeLevel3
      // 
      this.pct_BoardAtKnifeLevel3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_BoardAtKnifeLevel3.Image = ((System.Drawing.Image)(resources.GetObject("pct_BoardAtKnifeLevel3.Image")));
      this.pct_BoardAtKnifeLevel3.Location = new System.Drawing.Point(536, 251);
      this.pct_BoardAtKnifeLevel3.Name = "pct_BoardAtKnifeLevel3";
      this.pct_BoardAtKnifeLevel3.Size = new System.Drawing.Size(41, 18);
      this.pct_BoardAtKnifeLevel3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pct_BoardAtKnifeLevel3.TabIndex = 442;
      this.pct_BoardAtKnifeLevel3.TabStop = false;
      this.pct_BoardAtKnifeLevel3.Visible = false;
      // 
      // pct_maintenance
      // 
      this.pct_maintenance.Image = global::SyncroC.Properties.Resources.MANUTENZIONE_ISO_0717;
      this.pct_maintenance.Location = new System.Drawing.Point(216, 160);
      this.pct_maintenance.Name = "pct_maintenance";
      this.pct_maintenance.Size = new System.Drawing.Size(37, 28);
      this.pct_maintenance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_maintenance.TabIndex = 444;
      this.pct_maintenance.TabStop = false;
      this.pct_maintenance.Visible = false;
      // 
      // pct_SYCNotRdyAOC
      // 
      this.pct_SYCNotRdyAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_SYCNotRdyAOC.Image = ((System.Drawing.Image)(resources.GetObject("pct_SYCNotRdyAOC.Image")));
      this.pct_SYCNotRdyAOC.Location = new System.Drawing.Point(599, 250);
      this.pct_SYCNotRdyAOC.Name = "pct_SYCNotRdyAOC";
      this.pct_SYCNotRdyAOC.Size = new System.Drawing.Size(29, 18);
      this.pct_SYCNotRdyAOC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SYCNotRdyAOC.TabIndex = 445;
      this.pct_SYCNotRdyAOC.TabStop = false;
      this.pct_SYCNotRdyAOC.Visible = false;
      // 
      // pct_SYCNotRdyRun
      // 
      this.pct_SYCNotRdyRun.Image = ((System.Drawing.Image)(resources.GetObject("pct_SYCNotRdyRun.Image")));
      this.pct_SYCNotRdyRun.Location = new System.Drawing.Point(831, 235);
      this.pct_SYCNotRdyRun.Name = "pct_SYCNotRdyRun";
      this.pct_SYCNotRdyRun.Size = new System.Drawing.Size(29, 18);
      this.pct_SYCNotRdyRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SYCNotRdyRun.TabIndex = 446;
      this.pct_SYCNotRdyRun.TabStop = false;
      this.pct_SYCNotRdyRun.Visible = false;
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
      this.grp_now_percheMiSonoFermato.Location = new System.Drawing.Point(1023, 175);
      this.grp_now_percheMiSonoFermato.Name = "grp_now_percheMiSonoFermato";
      this.grp_now_percheMiSonoFermato.PaintGroupBox = true;
      this.grp_now_percheMiSonoFermato.RoundCorners = 10;
      this.grp_now_percheMiSonoFermato.ShadowColor = System.Drawing.Color.DarkGray;
      this.grp_now_percheMiSonoFermato.ShadowControl = true;
      this.grp_now_percheMiSonoFermato.ShadowThickness = 3;
      this.grp_now_percheMiSonoFermato.Size = new System.Drawing.Size(245, 95);
      this.grp_now_percheMiSonoFermato.TabIndex = 457;
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
      // Frm_StatusBarDryEnd
      // 
      this.ClientSize = new System.Drawing.Size(1280, 286);
      this.Controls.Add(this.grp_now_percheMiSonoFermato);
      this.Controls.Add(this.pct_SYCNotRdyAOC);
      this.Controls.Add(this.pct_maintenance);
      this.Controls.Add(this.ttl_SimulationTitle);
      this.Controls.Add(this.pct_AOCStatus);
      this.Controls.Add(this.pct_AOCType);
      this.Controls.Add(this.SlitterScorer);
      this.Controls.Add(this.pct_RSHScrapStatus);
      this.Controls.Add(this.gdv_SetupData);
      this.Controls.Add(this.btn_IncDecSpeedSS);
      this.Controls.Add(this.ttl_WetEndTitle);
      this.Controls.Add(this.btn_IncDecSpeedDEConsole);
      this.Controls.Add(this.btn_IncDecSpeedRSH);
      this.Controls.Add(this.btn_IncDecSpeedKN);
      this.Controls.Add(this.tlb_WetEndMetersLeft);
      this.Controls.Add(this.pct_STK2NotRdyAOC);
      this.Controls.Add(this.pct_STK1NotRdyAOC);
      this.Controls.Add(this.pct_KNIFENotRdyAOC);
      this.Controls.Add(this.pct_KNIFENotRdyRun);
      this.Controls.Add(this.pct_RSHNotRdyAOC);
      this.Controls.Add(this.pct_RSHNotRdyRun);
      this.Controls.Add(this.pct_DBNotRdyAOC);
      this.Controls.Add(this.pct_SSNotRdyAOC);
      this.Controls.Add(this.pct_SSNotRdyRun);
      this.Controls.Add(this.lbl_AOCMode_NN);
      this.Controls.Add(this.tmt_AOCTimeLeft);
      this.Controls.Add(this.nud_LineSpeed);
      this.Controls.Add(this.DryEnd);
      this.Controls.Add(this.Stacker2);
      this.Controls.Add(this.Stacker1);
      this.Controls.Add(this.Knife);
      this.Controls.Add(this.DoubleBacker);
      this.Controls.Add(this.prb_DryEnd);
      this.Controls.Add(this.tlb_DryEndMetersLeft);
      this.Controls.Add(this.ttl_DryEndTitle);
      this.Controls.Add(this.lbl_AOCType_NN);
      this.Controls.Add(this.pct_BoardAtMW);
      this.Controls.Add(this.pnl_clickExtension2);
      this.Controls.Add(this.pnl_clickExtension);
      this.Controls.Add(this.Shear);
      this.Controls.Add(this.btn_IncDecSpeedDB);
      this.Controls.Add(this.pct_DBNotRdyRun);
      this.Controls.Add(this.pct_SYCNotRdyRun);
      this.Controls.Add(this.tlb_DryEndSetup);
      this.Controls.Add(this.pct_STK3NotRdyAOC);
      this.Controls.Add(this.pct_STK3NotRdyRun);
      this.Controls.Add(this.Stacker3);
      this.Controls.Add(this.pct_BoardAtKnifeLevel3);
      this.Controls.Add(this.btn_IncDecSpeedSTK);
      this.Controls.Add(this.pct_STK1NotRdyRun);
      this.Controls.Add(this.pct_STK2NotRdyRun);
      this.Controls.Add(this.pct_BoardAtKnife);
      this.Controls.Add(this.pct_BoardAtRSH);
      this.Location = new System.Drawing.Point(0, 0);
      this.Name = "Frm_StatusBarDryEnd";
      this.Text = "Frm_StatusBarDryEnd";
      this.pnl_clickExtension2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_AOCStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_AOCType)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RSHScrapStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DBNotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK2NotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK2NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK1NotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK1NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KNIFENotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KNIFENotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RSHNotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RSHNotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DBNotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SSNotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SSNotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtKnife)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtMW)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtRSH)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK3NotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_STK3NotRdyRun)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_BoardAtKnifeLevel3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_maintenance)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SYCNotRdyAOC)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SYCNotRdyRun)).EndInit();
      this.grp_now_percheMiSonoFermato.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_now)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_now_causa_percheMiSonoFermato)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void DryEnd_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F1]) AltF1();	
		}

		//
		private void Stacker1_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F6]) AltF6();	
		}

		//
		private void Stacker2_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F7]) AltF7();
		}

		private void Stacker3_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F8]) AltF8();	
		}

		//
		private void Knife_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F5]) AltF5();	
		}

		//
		private void SlitterScorer_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F4]) AltF4();	
		}

		private void Shear_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F3]) AltF3();	
		}

		private void DoubleBacker_Click(object sender, EventArgs e)
		{
			if (this.AltFKeyEnable[(int)FKEYS.F2]) AltF2();	
		}

		private void orderChangeTimeLeftCalculation(object sender, EventArgs e)
		{
			int metersLeft = 0;
			int lineSpeed = 0;
			try
			{
				metersLeft = Convert.ToInt32(tlb_DryEndMetersLeft.Value);
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
				lineSpeed = Convert.ToInt32(nud_LineSpeed.Value);
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

		private void gdv_SetupData_DoubleClick(object sender, EventArgs e)
		{
			if (gdv_SetupData.SelectedRows.Count == 1 &&
					setups.Tables[Order.DRYEND_LIST].Rows.Count >= gdv_SetupData.SelectedRows[0].Index)
			{
				Frm_OrdersInsSubGroup.indexOrderToShowDRY = gdv_SetupData.SelectedRows[0].Index;
			}
			askForPage(typeof(Frm_QuickInsDryOrder));
		}

		public override void CorrugatorDirectionLeft()
		{
      this.corrugatorDirectionLeft = true;
      //RFAZZI_FI20/05/2010_1. Aggiunta visualizzazione in barra di stato Wet della macchina che ha fermato la linea. Invertiti anche i campi di Metri e Tempo a Cambio Wet. 
			int leftLimit = DoubleBacker.Location.X;
			//int rightLimit = Stacker2.Location.X + Stacker2.Width;
      //int leftLimit = DryEnd.Location.X;
      int rightLimit = nud_LineSpeed.Location.X + nud_LineSpeed.Width /*- DoubleBacker.Location.X*/;

      //RFAZZI_FI20/05/2010_1. Aggiunta visualizzazione in barra di stato Wet della macchina che ha fermato la linea. Invertiti anche i campi di Metri e Tempo a Cambio Wet. 
      HorizontalLocationMirror(this.tlb_DryEndMetersLeft, leftLimit, rightLimit);
      HorizontalLocationMirror(this.tlb_WetEndMetersLeft, leftLimit, rightLimit);
      HorizontalLocationMirror(this.tmt_AOCTimeLeft, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttl_SimulationTitle, leftLimit, rightLimit);
      HorizontalLocationMirror(this.tlb_DryEndSetup, leftLimit, rightLimit);
      HorizontalLocationMirror(this.nud_LineSpeed, leftLimit, rightLimit);
      HorizontalLocationMirror(this.DryEnd, leftLimit, rightLimit);
      HorizontalLocationMirror(this.grp_now_percheMiSonoFermato, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_SYCNotRdyAOC, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_SYCNotRdyRun, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_IncDecSpeedDEConsole, leftLimit, rightLimit);

			//DoubleBacker
			HorizontalLocationMirror(DoubleBacker, leftLimit, rightLimit);
			DoubleBacker.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_DBNotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_DBNotRdyAOC, leftLimit, rightLimit);
			//Shear
			HorizontalLocationMirror(Shear, leftLimit, rightLimit);
			Shear.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_RSHNotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_RSHNotRdyAOC, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_RSHScrapStatus, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedRSH, leftLimit, rightLimit);
			//SlitterScorer
			HorizontalLocationMirror(SlitterScorer, leftLimit, rightLimit);
			SlitterScorer.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_SSNotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_SSNotRdyAOC, leftLimit, rightLimit);
      HorizontalLocationMirror(this.btn_IncDecSpeedSS, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pct_maintenance, leftLimit, rightLimit);
      this.iml_RSHStatus.Images[0].RotateFlip(RotateFlipType.RotateNoneFlipX);
			this.iml_RSHStatus.Images[1].RotateFlip(RotateFlipType.RotateNoneFlipX);
			//Knife
			HorizontalLocationMirror(Knife, leftLimit, rightLimit);
			Knife.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_KNIFENotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_KNIFENotRdyAOC, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedKN, leftLimit, rightLimit);
			//Stacker1
			HorizontalLocationMirror(Stacker1, leftLimit, rightLimit);
			Stacker1.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_STK1NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_STK1NotRdyAOC, leftLimit, rightLimit);
			HorizontalLocationMirror(this.btn_IncDecSpeedSTK, leftLimit, rightLimit);
			//Stacker2
			HorizontalLocationMirror(Stacker2, leftLimit, rightLimit);
			Stacker2.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_STK2NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_STK2NotRdyAOC, leftLimit, rightLimit);
			//Stacker3
			HorizontalLocationMirror(Stacker3, leftLimit, rightLimit);
			Stacker3.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pct_STK3NotRdyRun, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_STK3NotRdyAOC, leftLimit, rightLimit);
			//boardAt
			HorizontalLocationMirror(pct_BoardAtRSH, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_BoardAtMW, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_BoardAtKnife, leftLimit, rightLimit);
      HorizontalLocationMirror(pct_BoardAtKnifeLevel3, leftLimit, rightLimit);
		}

		/// <summary>
		/// Called everytime there is a page change.
		/// Manages the state of the buttons according to the current page group.
		/// </summary>
		protected override void OnPageChanged(object sender, OnPageChangedEventArgs args)
		{
			base.OnPageChanged(sender, args);

			string group = args.groupType.ToString();

			if (group.StartsWith("SyncroC.Forms.Pages.DryEndSupervisor") && (!DryEnd.Active))
			{
				ClearActiveGroups();
				DryEnd.Active = true;
				return;
			}

			if (group.StartsWith("SyncroC.Forms.Pages.DoubleBacker") && (!DoubleBacker.Active))
			{
				ClearActiveGroups();
				DoubleBacker.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Shear") && (!Shear.Active))
			{
				ClearActiveGroups();
				Shear.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.SlitterScorer") && (!SlitterScorer.Active))
			{
				ClearActiveGroups();
				SlitterScorer.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Knife") && (!Knife.Active))
			{
				ClearActiveGroups();
				Knife.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Stacker") && (args.index == 1) && (!Stacker1.Active))
			{
				ClearActiveGroups();
				Stacker1.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Stacker") && (args.index == 2) && (!Stacker2.Active))
			{
				ClearActiveGroups();
				Stacker2.Active = true;
				return;
			}
			if (group.StartsWith("SyncroC.Forms.Pages.Stacker") && (args.index == 3) && (!Stacker3.Active))
			{
				ClearActiveGroups();
				Stacker3.Active = true;
				return;
			}
		}

		public void _setPushButtonPressed(string theFasterButton, string theSlowerButton)
		{
			if (theFasterButton.Equals(String.Empty) && theSlowerButton.Equals(String.Empty))
			{
				this.btn_IncDecSpeedDEConsole.Visible = false;
				this.btn_IncDecSpeedRSH.Visible = false;
				this.btn_IncDecSpeedSS.Visible = false;
				this.btn_IncDecSpeedKN.Visible = false;
				this.btn_IncDecSpeedSTK.Visible = false;
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

			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedDEConsole, listFaster.Contains("CON"), listSlower.Contains("CON"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedRSH, listFaster.Contains("RSH"), listSlower.Contains("RSH"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSS, listFaster.Contains("SS"), listSlower.Contains("SS"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedKN, listFaster.Contains("KN"), listSlower.Contains("KN"));
			this._setStateButtonPushButtonPressed(this.btn_IncDecSpeedSTK, listFaster.Contains("STK"), listSlower.Contains("STK"));
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

		private void ttl_WetEndTitle_Click(object sender, EventArgs e)
		{
			if (Settings.getSettings().configuration.WetEndControl)
				askForPage(typeof(Frm_StatusBarWetEnd));
		}

		private void lbl_AOCMode_NN_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeAOCMode)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					Command.AOC + ".SwitchDryEndMode()");
		}

    private void pct_BoardAtMW_Click(object sender, EventArgs e)
    {

    }
	}
}

