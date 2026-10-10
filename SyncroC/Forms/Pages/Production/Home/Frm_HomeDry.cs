using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.Utility;
using SyncroC.Controls;

//AGrassi_FI04/07/2007_1
//allargato controllo tlb_ShiftMetresProduced per farci entrare 5 cifre
namespace SyncroC.Forms.Pages.Production.Home
{
	public class Frm_HomeDry : SyncroC.Forms.Pages.Production.Home.Frm_HomeSubGroup
	{
      private SyncroC.Controls.TitledNumericUpDown nud_DRYMetresProduced;
      private SyncroC.Controls.TitledNumericUpDown nud_MaxSpeed;
      private SyncroC.Controls.TitledNumericUpDown nud_AOCSpeed;
      private SyncroC.Controls.Title title3;
      private SyncroC.Controls.TitledLabel tlb_DRYSetupCode;
      private Label lbl_GraphicTitle_YN;
      private SyncroC.Controls.Graphic gph_Speed;
      private SyncroC.Controls.Title title2;
      private SyncroC.Controls.Title ttl_WetEnd;
      private System.ComponentModel.IContainer components = null;
      private SyncroC.Controls.TitledMaskTextBox tmt_ShiftStartTime;
      private SyncroC.Controls.Title title1;
      private SyncroC.Controls.TitledMaskTextBox tmt_DrySetupStarted;
      private Panel pnl_Order1;
      private SyncroC.Controls.TitledLabel tlb_SheetsOrder1;
      private SyncroC.Controls.TitledLabel tlb_Customer1;
      private SyncroC.Controls.TitledNumericUpDown nud_Bad_Sheets_1;
      private SyncroC.Controls.TitledNumericUpDown nud_Good_Sheets_1;
      private Label lbl_ShiftID;
      private SyncroC.Controls.TitledLabel ttl_StopTime;
      private SyncroC.Controls.TitledMaskTextBox tmt_WetEndStartTime;
      private SyncroC.Controls.TitledNumericUpDown tnud_WetEndMetersProduced;
      private Panel pnl_Order2;
      private TitledLabel tlb_SheetsOrder2;
      private TitledLabel tlb_Customer2;
      private TitledNumericUpDown nud_Bad_Sheets_2;
      private TitledNumericUpDown nud_Good_Sheets_2;
      private Panel pnl_Order3;
      private TitledLabel tlb_SheetsOrder3;
      private TitledLabel tlb_Customer3;
      private TitledNumericUpDown nud_Bad_Sheets_3;
      private TitledNumericUpDown nud_Good_Sheets_3;
      private TitledLabel tlb_OrderDetails2;
      private TitledNumericUpDown nud_DRYMetresRequired;
      private TitledLabel tlb_OrderDetails1;
      private TitledNumericUpDown tnud_WetEndMetersRequired;
      private TitledNumericUpDown tlb_ShiftMetresProduced;
      private PictureBox pct_OrderPreprint3;
      private PictureBox pct_OrderPreprint2;
      private PictureBox pct_OrderPreprint1;
      //RFAZZI_FI14/03/2011_1. Aggiunta campo "Target Speed" su pagina HomeDry. 
      private TitledNumericUpDown nud_TargetSpeed;
      private TitledNumericUpDown nud_ScrapDischargeGroup1;
      private TitledNumericUpDown nud_ScrapDischargeGroup2;
      private TitledNumericUpDown nud_ScrapDischargeGroup3;
      private TitledLabel tlb_OrderDetails3;

            private static void appendHomeXmlElement(System.Text.StringBuilder xml, string name, string value)
            {
                  xml.Append('<').Append(name).Append('>');
                  xml.Append(System.Security.SecurityElement.Escape(value ?? String.Empty));
                  xml.Append("</").Append(name).Append(">\n");
            }

            public String getHomePageXml(){
                  System.Text.StringBuilder xml = new System.Text.StringBuilder();
                  xml.Append("<NewDataSet><Home><Shift>\n");
                  appendHomeXmlElement(xml, "ID", lbl_ShiftID.Text);
                  appendHomeXmlElement(xml, "StartTime", tmt_ShiftStartTime.Value);
                  appendHomeXmlElement(xml, "MetersProduced", tlb_ShiftMetresProduced.Value);
                  appendHomeXmlElement(xml, "EndTime", ttl_StopTime.Value);
                  appendHomeXmlElement(xml, "StopTime", ttl_StopTime.Value);
                  appendHomeXmlElement(xml, "MaxSpeed", nud_MaxSpeed.Value);
                  appendHomeXmlElement(xml, "AOCSpeed", nud_AOCSpeed.Value);
                  appendHomeXmlElement(xml, "WetEndMetersProduced", tnud_WetEndMetersProduced.Value);
                  appendHomeXmlElement(xml, "DryMetersRequired", nud_DRYMetresRequired.Value);
                  appendHomeXmlElement(xml, "WetEndMetersRequired", tnud_WetEndMetersRequired.Value);
                  xml.Append("</Shift>\n");
                  appendHomeXmlElement(xml, "DrySetupCode", tlb_DRYSetupCode.Value);
                  appendHomeXmlElement(xml, "DrySetupStarted", tmt_DrySetupStarted.Value);
                  appendHomeXmlElement(xml, "WetEndStartTime", tmt_WetEndStartTime.Value);
                  appendHomeXmlElement(xml, "TargetSpeed", nud_TargetSpeed.Value);
                  xml.Append("<Orders>\n");

                  xml.Append("<Order0>\n");
                  appendHomeXmlElement(xml, "Customer", tlb_Customer1.Value);
                  appendHomeXmlElement(xml, "Sheets", tlb_SheetsOrder1.Value);
                  appendHomeXmlElement(xml, "Details", tlb_OrderDetails1.Value);
                  appendHomeXmlElement(xml, "GoodSheets", nud_Good_Sheets_1.Value);
                  appendHomeXmlElement(xml, "BadSheets", nud_Bad_Sheets_1.Value);
                  xml.Append("</Order0>\n<Order1>\n");
                  appendHomeXmlElement(xml, "Customer", tlb_Customer2.Value);
                  appendHomeXmlElement(xml, "Sheets", tlb_SheetsOrder2.Value);
                  appendHomeXmlElement(xml, "Details", tlb_OrderDetails2.Value);
                  appendHomeXmlElement(xml, "GoodSheets", nud_Good_Sheets_2.Value);
                  appendHomeXmlElement(xml, "BadSheets", nud_Bad_Sheets_2.Value);
                  xml.Append("</Order1>\n<Order2>\n");
                  appendHomeXmlElement(xml, "Customer", tlb_Customer3.Value);
                  appendHomeXmlElement(xml, "Sheets", tlb_SheetsOrder3.Value);
                  appendHomeXmlElement(xml, "Details", tlb_OrderDetails3.Value);
                  appendHomeXmlElement(xml, "GoodSheets", nud_Good_Sheets_3.Value);
                  appendHomeXmlElement(xml, "BadSheets", nud_Bad_Sheets_3.Value);
                  xml.Append("</Order2>\n</Orders>\n</Home></NewDataSet>");
                  return xml.ToString();
        }
        
        
		//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
		//private DataSet setups;
		//private Binding level1Binding, level2Binding, level3Binding;

    public Frm_HomeDry()
    {
      InitializeComponent();


      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
      if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
      {
        nud_ScrapDischargeGroup1.Visible = true;
        nud_ScrapDischargeGroup2.Visible = true;
        nud_ScrapDischargeGroup3.Visible = true;

        //Riposiziono i campi di tagli richiesti, fatti buoni e scarto
        tlb_SheetsOrder1.Width = 90;
        tlb_SheetsOrder1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        nud_Good_Sheets_1.Location = new Point(tlb_SheetsOrder1.Location.X + tlb_SheetsOrder1.Width + 5, nud_Good_Sheets_1.Location.Y);
        nud_Good_Sheets_1.Width = tlb_SheetsOrder1.Width;
        nud_Good_Sheets_1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        nud_Bad_Sheets_1.Location = new Point(nud_Good_Sheets_1.Location.X + nud_Good_Sheets_1.Width + 5, nud_Bad_Sheets_1.Location.Y);
        nud_Bad_Sheets_1.Width = 70;
        nud_Bad_Sheets_1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_SheetsOrder2.Width = 90;
        tlb_SheetsOrder2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        nud_Good_Sheets_2.Location = new Point(tlb_SheetsOrder2.Location.X + tlb_SheetsOrder2.Width + 5, nud_Good_Sheets_2.Location.Y);
        nud_Good_Sheets_2.Width = tlb_SheetsOrder2.Width;
        nud_Good_Sheets_2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        nud_Bad_Sheets_2.Location = new Point(nud_Good_Sheets_2.Location.X + nud_Good_Sheets_2.Width + 5, nud_Bad_Sheets_2.Location.Y);
        nud_Bad_Sheets_2.Width = 70;
        nud_Bad_Sheets_2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_SheetsOrder3.Width = 90;
        tlb_SheetsOrder3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        nud_Good_Sheets_3.Location = new Point(tlb_SheetsOrder3.Location.X + tlb_SheetsOrder3.Width + 5, nud_Good_Sheets_3.Location.Y);
        nud_Good_Sheets_3.Width = tlb_SheetsOrder3.Width;
        nud_Good_Sheets_3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        nud_Bad_Sheets_3.Location = new Point(nud_Good_Sheets_3.Location.X + nud_Good_Sheets_3.Width + 5, nud_Bad_Sheets_3.Location.Y);
        nud_Bad_Sheets_3.Width = 70;
        nud_Bad_Sheets_3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      }
    }
    
    /*public String getHomePageXml()
    {
        String ret = "<NewDataSet>\n";
        ret += "<Home>\n";
        ret += "<Shift>\n";
        ret += "<ID>" + lbl_ShiftID.Text + "</ID>\n";
        ret += "<StartTime>" + tmt_ShiftStartTime.Value + "</StartTime>\n";
        ret += "<EndTime>" + ttl_StopTime.Value + "</EndTime>\n";
        ret += "<MetersProduced>" + tlb_ShiftMetresProduced.Value + "</MetersProduced>";
        ret += "</Shift>\n";
        ret += "<Orders>\n";
        ret += "<Order0>\n";
        ret += "<Customer>" + tlb_Customer1.Value + "</Customer>\n";
        ret += "<Sheets>" + tlb_SheetsOrder1.Value + "</Sheets>\n";
        ret += "<Details>" + tlb_OrderDetails1.Value + "</Details>\n";
        ret += "<GoodSheets>" + nud_Good_Sheets_1.Value + "</GoodSheets>\n";
        ret += "<BadSheets>" + nud_Bad_Sheets_1.Value + "</BadSheets>\n";
        ret += "</Order0>\n";
        ret += "<Order1>\n";
        ret += "<Customer>" + tlb_Customer2.Value + "</Customer>\n";
        ret += "<Sheets>" + tlb_SheetsOrder2.Value + "</Sheets>\n";
        ret += "<Details>" + tlb_OrderDetails2.Value + "</Details>\n";
        ret += "<GoodSheets>" + nud_Good_Sheets_2.Value + "</GoodSheets>\n";
        ret += "<BadSheets>" + nud_Bad_Sheets_2.Value + "</BadSheets>\n";
        ret += "</Order1>\n";
        ret += "<Order2>\n";
        ret += "<Customer>" + tlb_Customer3.Value + "</Customer>\n";
        ret += "<Sheets>" + tlb_SheetsOrder3.Value + "</Sheets>\n";
        ret += "<Details>" + tlb_OrderDetails3.Value + "</Details>\n";
        ret += "<GoodSheets>" + nud_Good_Sheets_3.Value + "</GoodSheets>\n";
        ret += "<BadSheets>" + nud_Bad_Sheets_3.Value + "</BadSheets>\n";
        ret += "</Order2>\n";
        ret += "</Orders>\n";
        ret += "</Home>\n";
        ret += "</NewDataSet>";
        return ret;
    }*/


		/// <summary>
		/// set the bindings for time and date
		/// NOTE: to be called only when the program is running
		/// </summary>
		private void setBindings()
		{
			//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
			//setups = DBManager.getDBManager().order.order;

			//tlb_DRYSetupCode.DataBindings.Add(
			//  new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_SETUP_CODE));

			//level1Binding = new Binding("Text", setups, Order.DRYEND_LIST + "." + Order.DEL_LEVEL_NAME_0);
			//level1Binding.Format += new ConvertEventHandler(this.levelBinding_Format0);
			//tlb_OrderDetails1.DataBindings.Add(level1Binding);
			//tlb_Customer1.DataBindings.Add(
			//  new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_CUSTOMER_NAME_0));
			//tlb_SheetsOrder1.DataBindings.Add(
			//  new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_SEGMENT_SHEET_COUNT_0, true, DataSourceUpdateMode.OnPropertyChanged));

			//level2Binding = new Binding("Text", setups, Order.DRYEND_LIST + "." + Order.DEL_LEVEL_NAME_1);
			//level2Binding.Format += new ConvertEventHandler(this.levelBinding_Format1);
			//tlb_OrderDetails2.DataBindings.Add(level2Binding);
			//tlb_Customer2.DataBindings.Add(
			//          new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_CUSTOMER_NAME_1));
			//tlb_SheetsOrder2.DataBindings.Add(
			//  new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_SEGMENT_SHEET_COUNT_1));

			//level3Binding = new Binding("Text", setups, Order.DRYEND_LIST + "." + Order.DEL_LEVEL_NAME_2);
			//level3Binding.Format += new ConvertEventHandler(this.levelBinding_Format2);
			//tlb_OrderDetails3.DataBindings.Add(level3Binding);
			//tlb_Customer3.DataBindings.Add(
			//          new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_CUSTOMER_NAME_2));
			//tlb_SheetsOrder3.DataBindings.Add(
			//  new Binding("Value", setups, Order.DRYEND_LIST + "." + Order.DEL_SEGMENT_SHEET_COUNT_2));

			this.nud_Good_Sheets_2.DataBindings.Add(
				new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
			this.nud_Good_Sheets_3.DataBindings.Add(
				new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
			this.nud_Bad_Sheets_1.DataBindings.Add(
				new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
			this.nud_Bad_Sheets_2.DataBindings.Add(
				new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
			this.nud_Bad_Sheets_3.DataBindings.Add(
				new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));

      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
      if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
      {
        this.nud_ScrapDischargeGroup1.DataBindings.Add(
          new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
        this.nud_ScrapDischargeGroup2.DataBindings.Add(
          new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
        this.nud_ScrapDischargeGroup3.DataBindings.Add(
          new Binding("BackColor", this.nud_Good_Sheets_1, "BackColor"));
      }
      DBManager.getDBManager().shift.OnDBShiftChange += new Shift.DBChangedHandler(shift_OnDBShiftChange);
		}

		private void shift_OnDBShiftChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
		{
			DataTable shift = DBManager.getDBManager().shift.getTableShift();
			int[] ShiftAverageSpeeds = new int[gph_Speed.MaxShiftChangeVisible];
			if ( shift == null || shift.Rows.Count <= 0)
			{
				ShiftAverageSpeeds.Initialize();
				Logger.LogError(this, "shift_OnDBShiftChange - table \"" + Shift.SHIFT_LIST + "\" not present into the Shift database or it is empty.");
			}
			else
			{
				int i = 0;
				while (i < ShiftAverageSpeeds.Length)
				{
					if (i < shift.Rows.Count)
					{
						try
						{
							ShiftAverageSpeeds[i] = Convert.ToInt32(shift.Rows[shift.Rows.Count - 1 - i][Shift.SHIFT_AVG_SPEED].ToString());
						}
						catch (Exception ex)
						{
							ShiftAverageSpeeds[i] = 0;
							Logger.LogError(this, "shift_OnDBShiftChange - Error converting shift average speed \"" + shift.Rows[i].ToString() + "\"", ex);
						}
					}
					else
					{
						ShiftAverageSpeeds[i] = 0;
					}
					i++;
				}
			}
			try
			{
				gph_Speed.setPastShiftAverageSpeeds(ShiftAverageSpeeds);
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "shift_OnDBShiftChange - average speed shifts for the speed graphic has not been properly set", ex);
			}
		}

    public void _SetIsStopShift(bool isStopShift)
    {
      this.title2.BackColor = isStopShift ? Color.Red : Color.Black;
    }

    public bool getIsStopShift() { return this.title2.BackColor == Color.Red;  }

    public override void _Show()
    {
      base._Show();
      shift_OnDBShiftChange(null, null);
    }

		public void _SetCurrentShiftAvgSpeed(int currentAvgSpeed)
		{
      //shift_OnDBShiftChange(null, null);
			gph_Speed.SetCurrentShiftAvgSpeed(currentAvgSpeed);
		}

		public void _SetShiftStopTime(int seconds)
		{ 
			ttl_StopTime.Value = SyncroConverter.ConvertToFormattedTime(seconds);
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
			DBManager.getDBManager().shift.OnDBShiftChange -= new Shift.DBChangedHandler(shift_OnDBShiftChange);

			//tlb_DRYSetupCode.DataBindings.Clear();

			//tlb_OrderDetails1.DataBindings.Clear();
			//tlb_Customer1.DataBindings.Clear();
			//tlb_SheetsOrder1.DataBindings.Clear();

			//tlb_OrderDetails2.DataBindings.Clear();
			//tlb_Customer2.DataBindings.Clear();
			//tlb_SheetsOrder2.DataBindings.Clear();

			//tlb_OrderDetails3.DataBindings.Clear();
			//tlb_Customer3.DataBindings.Clear();
			//tlb_SheetsOrder3.DataBindings.Clear();

      nud_Good_Sheets_2.DataBindings.Clear();
			nud_Good_Sheets_3.DataBindings.Clear();
			nud_Bad_Sheets_1.DataBindings.Clear();
			nud_Bad_Sheets_2.DataBindings.Clear();
			nud_Bad_Sheets_3.DataBindings.Clear();

      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
      if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
      {
        nud_ScrapDischargeGroup1.DataBindings.Clear();
        nud_ScrapDischargeGroup2.DataBindings.Clear();
        nud_ScrapDischargeGroup3.DataBindings.Clear();
      }

      base.Dispose(disposing);
		}

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_HomeDry));
      this.nud_DRYMetresProduced = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_MaxSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_AOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.title3 = new SyncroC.Controls.Title();
      this.tlb_DRYSetupCode = new SyncroC.Controls.TitledLabel();
      this.lbl_GraphicTitle_YN = new System.Windows.Forms.Label();
      this.gph_Speed = new SyncroC.Controls.Graphic();
      this.title2 = new SyncroC.Controls.Title();
      this.ttl_WetEnd = new SyncroC.Controls.Title();
      this.tmt_ShiftStartTime = new SyncroC.Controls.TitledMaskTextBox();
      this.title1 = new SyncroC.Controls.Title();
      this.tmt_DrySetupStarted = new SyncroC.Controls.TitledMaskTextBox();
      this.pnl_Order1 = new System.Windows.Forms.Panel();
      this.tlb_SheetsOrder1 = new SyncroC.Controls.TitledLabel();
      this.tlb_Customer1 = new SyncroC.Controls.TitledLabel();
      this.nud_Bad_Sheets_1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Good_Sheets_1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScrapDischargeGroup1 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_ShiftID = new System.Windows.Forms.Label();
      this.ttl_StopTime = new SyncroC.Controls.TitledLabel();
      this.tmt_WetEndStartTime = new SyncroC.Controls.TitledMaskTextBox();
      this.tnud_WetEndMetersProduced = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_Order2 = new System.Windows.Forms.Panel();
      this.tlb_SheetsOrder2 = new SyncroC.Controls.TitledLabel();
      this.tlb_Customer2 = new SyncroC.Controls.TitledLabel();
      this.nud_Bad_Sheets_2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Good_Sheets_2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScrapDischargeGroup2 = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_Order3 = new System.Windows.Forms.Panel();
      this.tlb_SheetsOrder3 = new SyncroC.Controls.TitledLabel();
      this.tlb_Customer3 = new SyncroC.Controls.TitledLabel();
      this.nud_Bad_Sheets_3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Good_Sheets_3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScrapDischargeGroup3 = new SyncroC.Controls.TitledNumericUpDown();
      this.tlb_OrderDetails2 = new SyncroC.Controls.TitledLabel();
      this.tlb_OrderDetails3 = new SyncroC.Controls.TitledLabel();
      this.nud_DRYMetresRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WetEndMetersRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.tlb_OrderDetails1 = new SyncroC.Controls.TitledLabel();
      this.tlb_ShiftMetresProduced = new SyncroC.Controls.TitledNumericUpDown();
      this.pct_OrderPreprint3 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint2 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint1 = new System.Windows.Forms.PictureBox();
      this.nud_TargetSpeed = new SyncroC.Controls.TitledNumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_Order1.SuspendLayout();
      this.pnl_Order2.SuspendLayout();
      this.pnl_Order3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_Time
      // 
      this.lbl_Time.Margin = new System.Windows.Forms.Padding(1, 3, 3, 1);
      // 
      // lbl_Date
      // 
      this.lbl_Date.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
      // 
      // lbl_Status
      // 
      this.lbl_Status.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      // nud_DRYMetresProduced
      // 
      this.nud_DRYMetresProduced.DecimalPlaces = 0;
      this.nud_DRYMetresProduced.Font = new System.Drawing.Font("Arial Unicode MS", 22F, System.Drawing.FontStyle.Bold);
      this.nud_DRYMetresProduced.ForeColor = System.Drawing.SystemColors.ControlText;
      this.nud_DRYMetresProduced.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DRYMetresProduced.ImagePassword = null;
      this.nud_DRYMetresProduced.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DRYMetresProduced.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_DRYMetresProduced.Location = new System.Drawing.Point(893, 504);
      this.nud_DRYMetresProduced.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DRYMetresProduced.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DRYMetresProduced.Name = "nud_DRYMetresProduced";
      this.nud_DRYMetresProduced.ReadOnly = true;
      this.nud_DRYMetresProduced.Size = new System.Drawing.Size(308, 64);
      this.nud_DRYMetresProduced.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DRYMetresProduced.TabIndex = 178;
      this.nud_DRYMetresProduced.TabStop = false;
      this.nud_DRYMetresProduced.Text = "Produced";
      this.nud_DRYMetresProduced.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_DRYMetresProduced.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DRYMetresProduced.TranslateText = true;
      this.nud_DRYMetresProduced.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_DRYMetresProduced.Value = "0";
      this.nud_DRYMetresProduced.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_DRYMetresProduced.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_MaxSpeed
      // 
      this.nud_MaxSpeed.DecimalPlaces = 0;
      this.nud_MaxSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 16F, System.Drawing.FontStyle.Bold);
      this.nud_MaxSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MaxSpeed.ImagePassword = null;
      this.nud_MaxSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_MaxSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_MaxSpeed.Location = new System.Drawing.Point(976, 312);
      this.nud_MaxSpeed.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_MaxSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_MaxSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_MaxSpeed.Name = "nud_MaxSpeed";
      this.nud_MaxSpeed.ReadOnly = true;
      this.nud_MaxSpeed.Size = new System.Drawing.Size(109, 64);
      this.nud_MaxSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_MaxSpeed.TabIndex = 177;
      this.nud_MaxSpeed.TabStop = false;
      this.nud_MaxSpeed.Text = "Max Speed";
      this.nud_MaxSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_MaxSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MaxSpeed.TranslateText = true;
      this.nud_MaxSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_MaxSpeed.Value = "350000";
      this.nud_MaxSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_MaxSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_AOCSpeed
      // 
      this.nud_AOCSpeed.DecimalPlaces = 0;
      this.nud_AOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_AOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_AOCSpeed.ImagePassword = null;
      this.nud_AOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_AOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_AOCSpeed.Location = new System.Drawing.Point(863, 312);
      this.nud_AOCSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_AOCSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_AOCSpeed.Name = "nud_AOCSpeed";
      this.nud_AOCSpeed.ReadOnly = true;
      this.nud_AOCSpeed.Size = new System.Drawing.Size(109, 64);
      this.nud_AOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_AOCSpeed.TabIndex = 176;
      this.nud_AOCSpeed.TabStop = false;
      this.nud_AOCSpeed.Text = "AOC Speed";
      this.nud_AOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_AOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_AOCSpeed.TranslateText = true;
      this.nud_AOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_AOCSpeed.Value = "350000";
      this.nud_AOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_AOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // title3
      // 
      this.title3.BackColor = System.Drawing.Color.Black;
      this.title3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title3.Location = new System.Drawing.Point(856, 193);
      this.title3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.title3.Name = "title3";
      this.title3.Size = new System.Drawing.Size(344, 184);
      this.title3.Style = SyncroC.Controls.TitleStyles.Small;
      this.title3.TabIndex = 175;
      this.title3.TabStop = false;
      this.title3.Text = "CHANGE";
      this.title3.TranslateText = true;
      // 
      // tlb_DRYSetupCode
      // 
      this.tlb_DRYSetupCode.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_DRYSetupCode.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_DRYSetupCode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DRYSetupCode.ImagePassword = null;
      this.tlb_DRYSetupCode.Location = new System.Drawing.Point(863, 224);
      this.tlb_DRYSetupCode.Name = "tlb_DRYSetupCode";
      this.tlb_DRYSetupCode.Size = new System.Drawing.Size(338, 64);
      this.tlb_DRYSetupCode.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_DRYSetupCode.TabIndex = 174;
      this.tlb_DRYSetupCode.TabStop = false;
      this.tlb_DRYSetupCode.Text = "Order Code";
      this.tlb_DRYSetupCode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DRYSetupCode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DRYSetupCode.TranslateText = true;
      this.tlb_DRYSetupCode.Value = "1548765154 - 02";
      this.tlb_DRYSetupCode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DRYSetupCode.ValueMultiline = true;
      // 
      // lbl_GraphicTitle_YN
      // 
      this.lbl_GraphicTitle_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_GraphicTitle_YN.Location = new System.Drawing.Point(325, 216);
      this.lbl_GraphicTitle_YN.Name = "lbl_GraphicTitle_YN";
      this.lbl_GraphicTitle_YN.Size = new System.Drawing.Size(499, 16);
      this.lbl_GraphicTitle_YN.TabIndex = 173;
      this.lbl_GraphicTitle_YN.Text = "Production Speed";
      this.lbl_GraphicTitle_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // gph_Speed
      // 
      this.gph_Speed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gph_Speed.ElementsXhour = 20;
      this.gph_Speed.Hours = 8;
      this.gph_Speed.Location = new System.Drawing.Point(325, 240);
      this.gph_Speed.Name = "gph_Speed";
      this.gph_Speed.Size = new System.Drawing.Size(525, 136);
      this.gph_Speed.Style = SyncroC.Controls.GraphicStyle.FilledAndLines;
      this.gph_Speed.TabIndex = 171;
      this.gph_Speed.TabStop = false;
      this.gph_Speed.TranslateText = false;
      // 
      // title2
      // 
      this.title2.BackColor = System.Drawing.Color.Black;
      this.title2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title2.Location = new System.Drawing.Point(30, 193);
      this.title2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.title2.Name = "title2";
      this.title2.Size = new System.Drawing.Size(820, 184);
      this.title2.Style = SyncroC.Controls.TitleStyles.Small;
      this.title2.TabIndex = 172;
      this.title2.TabStop = false;
      this.title2.Text = "SHIFT";
      this.title2.TranslateText = true;
      // 
      // ttl_WetEnd
      // 
      this.ttl_WetEnd.BackColor = System.Drawing.Color.Black;
      this.ttl_WetEnd.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_WetEnd.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_WetEnd.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_WetEnd.Location = new System.Drawing.Point(30, 382);
      this.ttl_WetEnd.Name = "ttl_WetEnd";
      this.ttl_WetEnd.Size = new System.Drawing.Size(1171, 89);
      this.ttl_WetEnd.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_WetEnd.TabIndex = 159;
      this.ttl_WetEnd.Text = "WET END";
      this.ttl_WetEnd.TranslateText = false;
      // 
      // tmt_ShiftStartTime
      // 
      this.tmt_ShiftStartTime.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_ShiftStartTime.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_ShiftStartTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_ShiftStartTime.ImagePassword = null;
      this.tmt_ShiftStartTime.Location = new System.Drawing.Point(37, 224);
      this.tmt_ShiftStartTime.Name = "tmt_ShiftStartTime";
      this.tmt_ShiftStartTime.PasswordChar = '\0';
      this.tmt_ShiftStartTime.ReadOnly = true;
      this.tmt_ShiftStartTime.Size = new System.Drawing.Size(268, 64);
      this.tmt_ShiftStartTime.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_ShiftStartTime.TabIndex = 198;
      this.tmt_ShiftStartTime.TabStop = false;
      this.tmt_ShiftStartTime.Text = "Shift Start Time";
      this.tmt_ShiftStartTime.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_ShiftStartTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_ShiftStartTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_ShiftStartTime.TranslateText = true;
      this.tmt_ShiftStartTime.Value = " ";
      this.tmt_ShiftStartTime.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_ShiftStartTime.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_ShiftStartTime.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_ShiftStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_ShiftStartTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // title1
      // 
      this.title1.BackColor = System.Drawing.Color.Black;
      this.title1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title1.Location = new System.Drawing.Point(30, 478);
      this.title1.Name = "title1";
      this.title1.Size = new System.Drawing.Size(1171, 234);
      this.title1.Style = SyncroC.Controls.TitleStyles.Small;
      this.title1.TabIndex = 201;
      this.title1.Text = "DRY END";
      this.title1.TranslateText = false;
      // 
      // tmt_DrySetupStarted
      // 
      this.tmt_DrySetupStarted.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_DrySetupStarted.Font = new System.Drawing.Font("Arial Unicode MS", 22F, System.Drawing.FontStyle.Bold);
      this.tmt_DrySetupStarted.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_DrySetupStarted.ImagePassword = null;
      this.tmt_DrySetupStarted.Location = new System.Drawing.Point(37, 504);
      this.tmt_DrySetupStarted.Name = "tmt_DrySetupStarted";
      this.tmt_DrySetupStarted.PasswordChar = '\0';
      this.tmt_DrySetupStarted.ReadOnly = true;
      this.tmt_DrySetupStarted.Size = new System.Drawing.Size(504, 64);
      this.tmt_DrySetupStarted.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_DrySetupStarted.TabIndex = 202;
      this.tmt_DrySetupStarted.TabStop = false;
      this.tmt_DrySetupStarted.Text = "Order Start Time";
      this.tmt_DrySetupStarted.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_DrySetupStarted.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_DrySetupStarted.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_DrySetupStarted.TranslateText = true;
      this.tmt_DrySetupStarted.Value = " ";
      this.tmt_DrySetupStarted.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_DrySetupStarted.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_DrySetupStarted.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_DrySetupStarted.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_DrySetupStarted.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // pnl_Order1
      // 
      this.pnl_Order1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pnl_Order1.Controls.Add(this.tlb_SheetsOrder1);
      this.pnl_Order1.Controls.Add(this.tlb_Customer1);
      this.pnl_Order1.Controls.Add(this.nud_Bad_Sheets_1);
      this.pnl_Order1.Controls.Add(this.nud_Good_Sheets_1);
      this.pnl_Order1.Controls.Add(this.nud_ScrapDischargeGroup1);
      this.pnl_Order1.Location = new System.Drawing.Point(45, 599);
      this.pnl_Order1.Name = "pnl_Order1";
      this.pnl_Order1.Size = new System.Drawing.Size(369, 113);
      this.pnl_Order1.TabIndex = 204;
      // 
      // tlb_SheetsOrder1
      // 
      this.tlb_SheetsOrder1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_SheetsOrder1.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_SheetsOrder1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SheetsOrder1.ImagePassword = null;
      this.tlb_SheetsOrder1.Location = new System.Drawing.Point(0, 50);
      this.tlb_SheetsOrder1.Name = "tlb_SheetsOrder1";
      this.tlb_SheetsOrder1.Size = new System.Drawing.Size(132, 61);
      this.tlb_SheetsOrder1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_SheetsOrder1.TabIndex = 198;
      this.tlb_SheetsOrder1.TabStop = false;
      this.tlb_SheetsOrder1.Text = "Required";
      this.tlb_SheetsOrder1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SheetsOrder1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SheetsOrder1.TranslateText = true;
      this.tlb_SheetsOrder1.Value = "0";
      this.tlb_SheetsOrder1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SheetsOrder1.ValueMultiline = true;
      // 
      // tlb_Customer1
      // 
      this.tlb_Customer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Customer1.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_Customer1.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer1.ImagePassword = null;
      this.tlb_Customer1.Location = new System.Drawing.Point(0, 2);
      this.tlb_Customer1.Name = "tlb_Customer1";
      this.tlb_Customer1.Size = new System.Drawing.Size(366, 37);
      this.tlb_Customer1.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.tlb_Customer1.TabIndex = 197;
      this.tlb_Customer1.TabStop = false;
      this.tlb_Customer1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer1.TranslateText = false;
      this.tlb_Customer1.Value = "";
      this.tlb_Customer1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer1.ValueMultiline = true;
      // 
      // nud_Bad_Sheets_1
      // 
      this.nud_Bad_Sheets_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_Bad_Sheets_1.DecimalPlaces = 0;
      this.nud_Bad_Sheets_1.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Bad_Sheets_1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Bad_Sheets_1.ImagePassword = null;
      this.nud_Bad_Sheets_1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Bad_Sheets_1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Bad_Sheets_1.Location = new System.Drawing.Point(280, 50);
      this.nud_Bad_Sheets_1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Bad_Sheets_1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Bad_Sheets_1.Name = "nud_Bad_Sheets_1";
      this.nud_Bad_Sheets_1.ReadOnly = true;
      this.nud_Bad_Sheets_1.Size = new System.Drawing.Size(86, 61);
      this.nud_Bad_Sheets_1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Bad_Sheets_1.TabIndex = 196;
      this.nud_Bad_Sheets_1.TabStop = false;
      this.nud_Bad_Sheets_1.Text = "Bad ";
      this.nud_Bad_Sheets_1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Bad_Sheets_1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Bad_Sheets_1.TranslateText = true;
      this.nud_Bad_Sheets_1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Bad_Sheets_1.Value = "";
      this.nud_Bad_Sheets_1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Bad_Sheets_1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_Good_Sheets_1
      // 
      this.nud_Good_Sheets_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_Good_Sheets_1.DecimalPlaces = 0;
      this.nud_Good_Sheets_1.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Good_Sheets_1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Good_Sheets_1.ImagePassword = null;
      this.nud_Good_Sheets_1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Good_Sheets_1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Good_Sheets_1.Location = new System.Drawing.Point(140, 50);
      this.nud_Good_Sheets_1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Good_Sheets_1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Good_Sheets_1.Name = "nud_Good_Sheets_1";
      this.nud_Good_Sheets_1.ReadOnly = true;
      this.nud_Good_Sheets_1.Size = new System.Drawing.Size(132, 61);
      this.nud_Good_Sheets_1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Good_Sheets_1.TabIndex = 195;
      this.nud_Good_Sheets_1.TabStop = false;
      this.nud_Good_Sheets_1.Text = "Good";
      this.nud_Good_Sheets_1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Good_Sheets_1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Good_Sheets_1.TranslateText = true;
      this.nud_Good_Sheets_1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Good_Sheets_1.Value = "0";
      this.nud_Good_Sheets_1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Good_Sheets_1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_ScrapDischargeGroup1
      // 
      this.nud_ScrapDischargeGroup1.DecimalPlaces = 0;
      this.nud_ScrapDischargeGroup1.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScrapDischargeGroup1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup1.ImagePassword = null;
      this.nud_ScrapDischargeGroup1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup1.Location = new System.Drawing.Point(265, 50);
      this.nud_ScrapDischargeGroup1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
      this.nud_ScrapDischargeGroup1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup1.Name = "nud_ScrapDischargeGroup1";
      this.nud_ScrapDischargeGroup1.ReadOnly = true;
      this.nud_ScrapDischargeGroup1.Size = new System.Drawing.Size(105, 61);
      this.nud_ScrapDischargeGroup1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScrapDischargeGroup1.TabIndex = 479;
      this.nud_ScrapDischargeGroup1.TabStop = false;
      this.nud_ScrapDischargeGroup1.Text = "Scrap Group";
      this.nud_ScrapDischargeGroup1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup1.TranslateText = true;
      this.nud_ScrapDischargeGroup1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup1.Value = "11500";
      this.nud_ScrapDischargeGroup1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScrapDischargeGroup1.Visible = false;
      // 
      // lbl_ShiftID
      // 
      this.lbl_ShiftID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.lbl_ShiftID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lbl_ShiftID.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ShiftID.ForeColor = System.Drawing.Color.Yellow;
      this.lbl_ShiftID.Location = new System.Drawing.Point(160, 193);
      this.lbl_ShiftID.Name = "lbl_ShiftID";
      this.lbl_ShiftID.Size = new System.Drawing.Size(52, 19);
      this.lbl_ShiftID.TabIndex = 206;
      this.lbl_ShiftID.Text = "A";
      this.lbl_ShiftID.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // ttl_StopTime
      // 
      this.ttl_StopTime.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_StopTime.Font = new System.Drawing.Font("Arial Unicode MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_StopTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StopTime.ImagePassword = null;
      this.ttl_StopTime.Location = new System.Drawing.Point(186, 312);
      this.ttl_StopTime.Name = "ttl_StopTime";
      this.ttl_StopTime.Size = new System.Drawing.Size(119, 64);
      this.ttl_StopTime.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_StopTime.TabIndex = 207;
      this.ttl_StopTime.TabStop = false;
      this.ttl_StopTime.Text = "Stop time";
      this.ttl_StopTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_StopTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StopTime.TranslateText = true;
      this.ttl_StopTime.Value = "00:00:00";
      this.ttl_StopTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_StopTime.ValueMultiline = true;
      // 
      // tmt_WetEndStartTime
      // 
      this.tmt_WetEndStartTime.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_WetEndStartTime.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_WetEndStartTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_WetEndStartTime.ImagePassword = null;
      this.tmt_WetEndStartTime.Location = new System.Drawing.Point(37, 408);
      this.tmt_WetEndStartTime.Name = "tmt_WetEndStartTime";
      this.tmt_WetEndStartTime.PasswordChar = '\0';
      this.tmt_WetEndStartTime.ReadOnly = true;
      this.tmt_WetEndStartTime.Size = new System.Drawing.Size(504, 64);
      this.tmt_WetEndStartTime.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_WetEndStartTime.TabIndex = 208;
      this.tmt_WetEndStartTime.TabStop = false;
      this.tmt_WetEndStartTime.Text = "Quality Start Time";
      this.tmt_WetEndStartTime.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_WetEndStartTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_WetEndStartTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_WetEndStartTime.TranslateText = true;
      this.tmt_WetEndStartTime.Value = " ";
      this.tmt_WetEndStartTime.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_WetEndStartTime.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_WetEndStartTime.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_WetEndStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_WetEndStartTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_WetEndMetersProduced
      // 
      this.tnud_WetEndMetersProduced.DecimalPlaces = 0;
      this.tnud_WetEndMetersProduced.Font = new System.Drawing.Font("Arial Unicode MS", 22F, System.Drawing.FontStyle.Bold);
      this.tnud_WetEndMetersProduced.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_WetEndMetersProduced.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersProduced.ImagePassword = null;
      this.tnud_WetEndMetersProduced.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndMetersProduced.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersProduced.Location = new System.Drawing.Point(893, 408);
      this.tnud_WetEndMetersProduced.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_WetEndMetersProduced.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WetEndMetersProduced.Name = "tnud_WetEndMetersProduced";
      this.tnud_WetEndMetersProduced.ReadOnly = true;
      this.tnud_WetEndMetersProduced.Size = new System.Drawing.Size(308, 64);
      this.tnud_WetEndMetersProduced.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WetEndMetersProduced.TabIndex = 209;
      this.tnud_WetEndMetersProduced.TabStop = false;
      this.tnud_WetEndMetersProduced.Text = "Produced";
      this.tnud_WetEndMetersProduced.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersProduced.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersProduced.TranslateText = true;
      this.tnud_WetEndMetersProduced.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersProduced.Value = "0";
      this.tnud_WetEndMetersProduced.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersProduced.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_Order2
      // 
      this.pnl_Order2.Controls.Add(this.tlb_SheetsOrder2);
      this.pnl_Order2.Controls.Add(this.tlb_Customer2);
      this.pnl_Order2.Controls.Add(this.nud_Bad_Sheets_2);
      this.pnl_Order2.Controls.Add(this.nud_Good_Sheets_2);
      this.pnl_Order2.Controls.Add(this.nud_ScrapDischargeGroup2);
      this.pnl_Order2.Location = new System.Drawing.Point(437, 599);
      this.pnl_Order2.Name = "pnl_Order2";
      this.pnl_Order2.Size = new System.Drawing.Size(369, 113);
      this.pnl_Order2.TabIndex = 210;
      // 
      // tlb_SheetsOrder2
      // 
      this.tlb_SheetsOrder2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_SheetsOrder2.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_SheetsOrder2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SheetsOrder2.ImagePassword = null;
      this.tlb_SheetsOrder2.Location = new System.Drawing.Point(0, 50);
      this.tlb_SheetsOrder2.Name = "tlb_SheetsOrder2";
      this.tlb_SheetsOrder2.Size = new System.Drawing.Size(132, 61);
      this.tlb_SheetsOrder2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_SheetsOrder2.TabIndex = 198;
      this.tlb_SheetsOrder2.TabStop = false;
      this.tlb_SheetsOrder2.Text = "Required";
      this.tlb_SheetsOrder2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SheetsOrder2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SheetsOrder2.TranslateText = true;
      this.tlb_SheetsOrder2.Value = "";
      this.tlb_SheetsOrder2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SheetsOrder2.ValueMultiline = true;
      // 
      // tlb_Customer2
      // 
      this.tlb_Customer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Customer2.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_Customer2.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer2.ImagePassword = null;
      this.tlb_Customer2.Location = new System.Drawing.Point(0, 4);
      this.tlb_Customer2.Name = "tlb_Customer2";
      this.tlb_Customer2.Size = new System.Drawing.Size(366, 37);
      this.tlb_Customer2.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.tlb_Customer2.TabIndex = 197;
      this.tlb_Customer2.TabStop = false;
      this.tlb_Customer2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Customer2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer2.TranslateText = false;
      this.tlb_Customer2.Value = "";
      this.tlb_Customer2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer2.ValueMultiline = true;
      // 
      // nud_Bad_Sheets_2
      // 
      this.nud_Bad_Sheets_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_Bad_Sheets_2.DecimalPlaces = 0;
      this.nud_Bad_Sheets_2.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Bad_Sheets_2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Bad_Sheets_2.ImagePassword = null;
      this.nud_Bad_Sheets_2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Bad_Sheets_2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Bad_Sheets_2.Location = new System.Drawing.Point(280, 50);
      this.nud_Bad_Sheets_2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Bad_Sheets_2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Bad_Sheets_2.Name = "nud_Bad_Sheets_2";
      this.nud_Bad_Sheets_2.ReadOnly = true;
      this.nud_Bad_Sheets_2.Size = new System.Drawing.Size(86, 61);
      this.nud_Bad_Sheets_2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Bad_Sheets_2.TabIndex = 196;
      this.nud_Bad_Sheets_2.TabStop = false;
      this.nud_Bad_Sheets_2.Text = "Bad ";
      this.nud_Bad_Sheets_2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Bad_Sheets_2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Bad_Sheets_2.TranslateText = true;
      this.nud_Bad_Sheets_2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Bad_Sheets_2.Value = "";
      this.nud_Bad_Sheets_2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Bad_Sheets_2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_Good_Sheets_2
      // 
      this.nud_Good_Sheets_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_Good_Sheets_2.DecimalPlaces = 0;
      this.nud_Good_Sheets_2.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Good_Sheets_2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Good_Sheets_2.ImagePassword = null;
      this.nud_Good_Sheets_2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Good_Sheets_2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Good_Sheets_2.Location = new System.Drawing.Point(140, 50);
      this.nud_Good_Sheets_2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Good_Sheets_2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Good_Sheets_2.Name = "nud_Good_Sheets_2";
      this.nud_Good_Sheets_2.ReadOnly = true;
      this.nud_Good_Sheets_2.Size = new System.Drawing.Size(132, 61);
      this.nud_Good_Sheets_2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Good_Sheets_2.TabIndex = 195;
      this.nud_Good_Sheets_2.TabStop = false;
      this.nud_Good_Sheets_2.Text = "Good ";
      this.nud_Good_Sheets_2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Good_Sheets_2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Good_Sheets_2.TranslateText = true;
      this.nud_Good_Sheets_2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Good_Sheets_2.Value = "";
      this.nud_Good_Sheets_2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Good_Sheets_2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_ScrapDischargeGroup2
      // 
      this.nud_ScrapDischargeGroup2.DecimalPlaces = 0;
      this.nud_ScrapDischargeGroup2.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScrapDischargeGroup2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup2.ImagePassword = null;
      this.nud_ScrapDischargeGroup2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup2.Location = new System.Drawing.Point(265, 50);
      this.nud_ScrapDischargeGroup2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
      this.nud_ScrapDischargeGroup2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup2.Name = "nud_ScrapDischargeGroup2";
      this.nud_ScrapDischargeGroup2.ReadOnly = true;
      this.nud_ScrapDischargeGroup2.Size = new System.Drawing.Size(105, 61);
      this.nud_ScrapDischargeGroup2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScrapDischargeGroup2.TabIndex = 480;
      this.nud_ScrapDischargeGroup2.TabStop = false;
      this.nud_ScrapDischargeGroup2.Text = "Scrap Group";
      this.nud_ScrapDischargeGroup2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup2.TranslateText = true;
      this.nud_ScrapDischargeGroup2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup2.Value = "11500";
      this.nud_ScrapDischargeGroup2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScrapDischargeGroup2.Visible = false;
      // 
      // pnl_Order3
      // 
      this.pnl_Order3.Controls.Add(this.tlb_SheetsOrder3);
      this.pnl_Order3.Controls.Add(this.tlb_Customer3);
      this.pnl_Order3.Controls.Add(this.nud_Bad_Sheets_3);
      this.pnl_Order3.Controls.Add(this.nud_Good_Sheets_3);
      this.pnl_Order3.Controls.Add(this.nud_ScrapDischargeGroup3);
      this.pnl_Order3.Location = new System.Drawing.Point(829, 599);
      this.pnl_Order3.Name = "pnl_Order3";
      this.pnl_Order3.Size = new System.Drawing.Size(369, 113);
      this.pnl_Order3.TabIndex = 211;
      // 
      // tlb_SheetsOrder3
      // 
      this.tlb_SheetsOrder3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_SheetsOrder3.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_SheetsOrder3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SheetsOrder3.ImagePassword = null;
      this.tlb_SheetsOrder3.Location = new System.Drawing.Point(0, 50);
      this.tlb_SheetsOrder3.Name = "tlb_SheetsOrder3";
      this.tlb_SheetsOrder3.Size = new System.Drawing.Size(132, 61);
      this.tlb_SheetsOrder3.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_SheetsOrder3.TabIndex = 198;
      this.tlb_SheetsOrder3.TabStop = false;
      this.tlb_SheetsOrder3.Text = "Required";
      this.tlb_SheetsOrder3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SheetsOrder3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SheetsOrder3.TranslateText = true;
      this.tlb_SheetsOrder3.Value = "";
      this.tlb_SheetsOrder3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SheetsOrder3.ValueMultiline = true;
      // 
      // tlb_Customer3
      // 
      this.tlb_Customer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Customer3.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_Customer3.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer3.ImagePassword = null;
      this.tlb_Customer3.Location = new System.Drawing.Point(0, 4);
      this.tlb_Customer3.Name = "tlb_Customer3";
      this.tlb_Customer3.Size = new System.Drawing.Size(366, 37);
      this.tlb_Customer3.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.tlb_Customer3.TabIndex = 197;
      this.tlb_Customer3.TabStop = false;
      this.tlb_Customer3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Customer3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer3.TranslateText = false;
      this.tlb_Customer3.Value = "";
      this.tlb_Customer3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Customer3.ValueMultiline = true;
      // 
      // nud_Bad_Sheets_3
      // 
      this.nud_Bad_Sheets_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_Bad_Sheets_3.DecimalPlaces = 0;
      this.nud_Bad_Sheets_3.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Bad_Sheets_3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Bad_Sheets_3.ImagePassword = null;
      this.nud_Bad_Sheets_3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Bad_Sheets_3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Bad_Sheets_3.Location = new System.Drawing.Point(280, 50);
      this.nud_Bad_Sheets_3.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Bad_Sheets_3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Bad_Sheets_3.Name = "nud_Bad_Sheets_3";
      this.nud_Bad_Sheets_3.ReadOnly = true;
      this.nud_Bad_Sheets_3.Size = new System.Drawing.Size(86, 61);
      this.nud_Bad_Sheets_3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Bad_Sheets_3.TabIndex = 196;
      this.nud_Bad_Sheets_3.TabStop = false;
      this.nud_Bad_Sheets_3.Text = "Bad ";
      this.nud_Bad_Sheets_3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Bad_Sheets_3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Bad_Sheets_3.TranslateText = true;
      this.nud_Bad_Sheets_3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Bad_Sheets_3.Value = "";
      this.nud_Bad_Sheets_3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Bad_Sheets_3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_Good_Sheets_3
      // 
      this.nud_Good_Sheets_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nud_Good_Sheets_3.DecimalPlaces = 0;
      this.nud_Good_Sheets_3.Font = new System.Drawing.Font("Arial Unicode MS", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Good_Sheets_3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Good_Sheets_3.ImagePassword = null;
      this.nud_Good_Sheets_3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Good_Sheets_3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Good_Sheets_3.Location = new System.Drawing.Point(140, 50);
      this.nud_Good_Sheets_3.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Good_Sheets_3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Good_Sheets_3.Name = "nud_Good_Sheets_3";
      this.nud_Good_Sheets_3.ReadOnly = true;
      this.nud_Good_Sheets_3.Size = new System.Drawing.Size(132, 61);
      this.nud_Good_Sheets_3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Good_Sheets_3.TabIndex = 195;
      this.nud_Good_Sheets_3.TabStop = false;
      this.nud_Good_Sheets_3.Text = "Good ";
      this.nud_Good_Sheets_3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Good_Sheets_3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Good_Sheets_3.TranslateText = true;
      this.nud_Good_Sheets_3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Good_Sheets_3.Value = "";
      this.nud_Good_Sheets_3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Good_Sheets_3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_ScrapDischargeGroup3
      // 
      this.nud_ScrapDischargeGroup3.DecimalPlaces = 0;
      this.nud_ScrapDischargeGroup3.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScrapDischargeGroup3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup3.ImagePassword = null;
      this.nud_ScrapDischargeGroup3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup3.Location = new System.Drawing.Point(265, 50);
      this.nud_ScrapDischargeGroup3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
      this.nud_ScrapDischargeGroup3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup3.Name = "nud_ScrapDischargeGroup3";
      this.nud_ScrapDischargeGroup3.ReadOnly = true;
      this.nud_ScrapDischargeGroup3.Size = new System.Drawing.Size(105, 61);
      this.nud_ScrapDischargeGroup3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScrapDischargeGroup3.TabIndex = 481;
      this.nud_ScrapDischargeGroup3.TabStop = false;
      this.nud_ScrapDischargeGroup3.Text = "Scrap Group";
      this.nud_ScrapDischargeGroup3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup3.TranslateText = true;
      this.nud_ScrapDischargeGroup3.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup3.Value = "11500";
      this.nud_ScrapDischargeGroup3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScrapDischargeGroup3.Visible = false;
      // 
      // tlb_OrderDetails2
      // 
      this.tlb_OrderDetails2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_OrderDetails2.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_OrderDetails2.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_OrderDetails2.ImagePassword = null;
      this.tlb_OrderDetails2.Location = new System.Drawing.Point(429, 574);
      this.tlb_OrderDetails2.Name = "tlb_OrderDetails2";
      this.tlb_OrderDetails2.Size = new System.Drawing.Size(377, 138);
      this.tlb_OrderDetails2.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_OrderDetails2.TabIndex = 213;
      this.tlb_OrderDetails2.TabStop = false;
      this.tlb_OrderDetails2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderDetails2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderDetails2.TranslateText = false;
      this.tlb_OrderDetails2.Value = "";
      this.tlb_OrderDetails2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderDetails2.ValueMultiline = true;
      // 
      // tlb_OrderDetails3
      // 
      this.tlb_OrderDetails3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_OrderDetails3.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_OrderDetails3.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_OrderDetails3.ImagePassword = null;
      this.tlb_OrderDetails3.Location = new System.Drawing.Point(821, 574);
      this.tlb_OrderDetails3.Name = "tlb_OrderDetails3";
      this.tlb_OrderDetails3.Size = new System.Drawing.Size(377, 138);
      this.tlb_OrderDetails3.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_OrderDetails3.TabIndex = 214;
      this.tlb_OrderDetails3.TabStop = false;
      this.tlb_OrderDetails3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderDetails3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderDetails3.TranslateText = false;
      this.tlb_OrderDetails3.Value = "";
      this.tlb_OrderDetails3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderDetails3.ValueMultiline = true;
      // 
      // nud_DRYMetresRequired
      // 
      this.nud_DRYMetresRequired.DecimalPlaces = 0;
      this.nud_DRYMetresRequired.Font = new System.Drawing.Font("Arial Unicode MS", 22F, System.Drawing.FontStyle.Bold);
      this.nud_DRYMetresRequired.ForeColor = System.Drawing.SystemColors.ControlText;
      this.nud_DRYMetresRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DRYMetresRequired.ImagePassword = null;
      this.nud_DRYMetresRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DRYMetresRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_DRYMetresRequired.Location = new System.Drawing.Point(563, 504);
      this.nud_DRYMetresRequired.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DRYMetresRequired.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DRYMetresRequired.Name = "nud_DRYMetresRequired";
      this.nud_DRYMetresRequired.ReadOnly = true;
      this.nud_DRYMetresRequired.Size = new System.Drawing.Size(306, 64);
      this.nud_DRYMetresRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DRYMetresRequired.TabIndex = 215;
      this.nud_DRYMetresRequired.TabStop = false;
      this.nud_DRYMetresRequired.Text = "Requested";
      this.nud_DRYMetresRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_DRYMetresRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DRYMetresRequired.TranslateText = true;
      this.nud_DRYMetresRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_DRYMetresRequired.Value = "0";
      this.nud_DRYMetresRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_DRYMetresRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WetEndMetersRequired
      // 
      this.tnud_WetEndMetersRequired.DecimalPlaces = 0;
      this.tnud_WetEndMetersRequired.Font = new System.Drawing.Font("Arial Unicode MS", 22F, System.Drawing.FontStyle.Bold);
      this.tnud_WetEndMetersRequired.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_WetEndMetersRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersRequired.ImagePassword = null;
      this.tnud_WetEndMetersRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndMetersRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersRequired.Location = new System.Drawing.Point(563, 408);
      this.tnud_WetEndMetersRequired.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_WetEndMetersRequired.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WetEndMetersRequired.Name = "tnud_WetEndMetersRequired";
      this.tnud_WetEndMetersRequired.ReadOnly = true;
      this.tnud_WetEndMetersRequired.Size = new System.Drawing.Size(306, 64);
      this.tnud_WetEndMetersRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WetEndMetersRequired.TabIndex = 216;
      this.tnud_WetEndMetersRequired.TabStop = false;
      this.tnud_WetEndMetersRequired.Text = "Requested";
      this.tnud_WetEndMetersRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersRequired.TranslateText = true;
      this.tnud_WetEndMetersRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersRequired.Value = "0";
      this.tnud_WetEndMetersRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tlb_OrderDetails1
      // 
      this.tlb_OrderDetails1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_OrderDetails1.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_OrderDetails1.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tlb_OrderDetails1.ImagePassword = null;
      this.tlb_OrderDetails1.Location = new System.Drawing.Point(37, 574);
      this.tlb_OrderDetails1.Name = "tlb_OrderDetails1";
      this.tlb_OrderDetails1.Size = new System.Drawing.Size(377, 138);
      this.tlb_OrderDetails1.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_OrderDetails1.TabIndex = 217;
      this.tlb_OrderDetails1.TabStop = false;
      this.tlb_OrderDetails1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderDetails1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderDetails1.TranslateText = false;
      this.tlb_OrderDetails1.Value = "";
      this.tlb_OrderDetails1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderDetails1.ValueMultiline = true;
      // 
      // tlb_ShiftMetresProduced
      // 
      this.tlb_ShiftMetresProduced.DecimalPlaces = 0;
      this.tlb_ShiftMetresProduced.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_ShiftMetresProduced.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_ShiftMetresProduced.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ShiftMetresProduced.ImagePassword = null;
      this.tlb_ShiftMetresProduced.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_ShiftMetresProduced.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_ShiftMetresProduced.Location = new System.Drawing.Point(37, 312);
      this.tlb_ShiftMetresProduced.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tlb_ShiftMetresProduced.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tlb_ShiftMetresProduced.Name = "tlb_ShiftMetresProduced";
      this.tlb_ShiftMetresProduced.ReadOnly = true;
      this.tlb_ShiftMetresProduced.Size = new System.Drawing.Size(144, 64);
      this.tlb_ShiftMetresProduced.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_ShiftMetresProduced.TabIndex = 256;
      this.tlb_ShiftMetresProduced.TabStop = false;
      this.tlb_ShiftMetresProduced.Text = "Produced";
      this.tlb_ShiftMetresProduced.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ShiftMetresProduced.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ShiftMetresProduced.TranslateText = true;
      this.tlb_ShiftMetresProduced.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_ShiftMetresProduced.Value = "999999";
      this.tlb_ShiftMetresProduced.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ShiftMetresProduced.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pct_OrderPreprint3
      // 
      this.pct_OrderPreprint3.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint3.Image")));
      this.pct_OrderPreprint3.Location = new System.Drawing.Point(1171, 576);
      this.pct_OrderPreprint3.Name = "pct_OrderPreprint3";
      this.pct_OrderPreprint3.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint3.TabIndex = 257;
      this.pct_OrderPreprint3.TabStop = false;
      this.pct_OrderPreprint3.Visible = false;
      // 
      // pct_OrderPreprint2
      // 
      this.pct_OrderPreprint2.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint2.Image")));
      this.pct_OrderPreprint2.Location = new System.Drawing.Point(776, 576);
      this.pct_OrderPreprint2.Name = "pct_OrderPreprint2";
      this.pct_OrderPreprint2.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint2.TabIndex = 258;
      this.pct_OrderPreprint2.TabStop = false;
      this.pct_OrderPreprint2.Visible = false;
      // 
      // pct_OrderPreprint1
      // 
      this.pct_OrderPreprint1.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint1.Image")));
      this.pct_OrderPreprint1.Location = new System.Drawing.Point(384, 576);
      this.pct_OrderPreprint1.Name = "pct_OrderPreprint1";
      this.pct_OrderPreprint1.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint1.TabIndex = 259;
      this.pct_OrderPreprint1.TabStop = false;
      this.pct_OrderPreprint1.Visible = false;
      // 
      // nud_TargetSpeed
      // 
      this.nud_TargetSpeed.DecimalPlaces = 0;
      this.nud_TargetSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 16F, System.Drawing.FontStyle.Bold);
      this.nud_TargetSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TargetSpeed.ImagePassword = null;
      this.nud_TargetSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TargetSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_TargetSpeed.Location = new System.Drawing.Point(1090, 312);
      this.nud_TargetSpeed.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_TargetSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TargetSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TargetSpeed.Name = "nud_TargetSpeed";
      this.nud_TargetSpeed.ReadOnly = true;
      this.nud_TargetSpeed.Size = new System.Drawing.Size(109, 64);
      this.nud_TargetSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_TargetSpeed.TabIndex = 260;
      this.nud_TargetSpeed.TabStop = false;
      this.nud_TargetSpeed.Text = "Target Speed";
      this.nud_TargetSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TargetSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TargetSpeed.TranslateText = true;
      this.nud_TargetSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_TargetSpeed.Value = "350000";
      this.nud_TargetSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TargetSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // Frm_HomeDry
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.nud_TargetSpeed);
      this.Controls.Add(this.pct_OrderPreprint1);
      this.Controls.Add(this.pct_OrderPreprint2);
      this.Controls.Add(this.pct_OrderPreprint3);
      this.Controls.Add(this.tlb_ShiftMetresProduced);
      this.Controls.Add(this.tnud_WetEndMetersRequired);
      this.Controls.Add(this.nud_DRYMetresRequired);
      this.Controls.Add(this.pnl_Order3);
      this.Controls.Add(this.pnl_Order2);
      this.Controls.Add(this.tnud_WetEndMetersProduced);
      this.Controls.Add(this.tmt_WetEndStartTime);
      this.Controls.Add(this.ttl_StopTime);
      this.Controls.Add(this.lbl_ShiftID);
      this.Controls.Add(this.pnl_Order1);
      this.Controls.Add(this.tmt_DrySetupStarted);
      this.Controls.Add(this.title1);
      this.Controls.Add(this.tmt_ShiftStartTime);
      this.Controls.Add(this.nud_DRYMetresProduced);
      this.Controls.Add(this.nud_MaxSpeed);
      this.Controls.Add(this.nud_AOCSpeed);
      this.Controls.Add(this.title3);
      this.Controls.Add(this.tlb_DRYSetupCode);
      this.Controls.Add(this.lbl_GraphicTitle_YN);
      this.Controls.Add(this.gph_Speed);
      this.Controls.Add(this.ttl_WetEnd);
      this.Controls.Add(this.title2);
      this.Controls.Add(this.tlb_OrderDetails2);
      this.Controls.Add(this.tlb_OrderDetails3);
      this.Controls.Add(this.tlb_OrderDetails1);
      this.Name = "Frm_HomeDry";
      this.Text = "Frm_HomeDry";
      this.Load += new System.EventHandler(this.Frm_HomeDry_Load);
      this.Activated += new System.EventHandler(this.Frm_HomeDry_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tlb_OrderDetails1, 0);
      this.Controls.SetChildIndex(this.tlb_OrderDetails3, 0);
      this.Controls.SetChildIndex(this.tlb_OrderDetails2, 0);
      this.Controls.SetChildIndex(this.title2, 0);
      this.Controls.SetChildIndex(this.ttl_WetEnd, 0);
      this.Controls.SetChildIndex(this.gph_Speed, 0);
      this.Controls.SetChildIndex(this.lbl_GraphicTitle_YN, 0);
      this.Controls.SetChildIndex(this.tlb_DRYSetupCode, 0);
      this.Controls.SetChildIndex(this.title3, 0);
      this.Controls.SetChildIndex(this.nud_AOCSpeed, 0);
      this.Controls.SetChildIndex(this.nud_MaxSpeed, 0);
      this.Controls.SetChildIndex(this.nud_DRYMetresProduced, 0);
      this.Controls.SetChildIndex(this.tmt_ShiftStartTime, 0);
      this.Controls.SetChildIndex(this.title1, 0);
      this.Controls.SetChildIndex(this.tmt_DrySetupStarted, 0);
      this.Controls.SetChildIndex(this.pnl_Order1, 0);
      this.Controls.SetChildIndex(this.lbl_ShiftID, 0);
      this.Controls.SetChildIndex(this.ttl_StopTime, 0);
      this.Controls.SetChildIndex(this.tmt_WetEndStartTime, 0);
      this.Controls.SetChildIndex(this.tnud_WetEndMetersProduced, 0);
      this.Controls.SetChildIndex(this.pnl_Order2, 0);
      this.Controls.SetChildIndex(this.pnl_Order3, 0);
      this.Controls.SetChildIndex(this.nud_DRYMetresRequired, 0);
      this.Controls.SetChildIndex(this.tnud_WetEndMetersRequired, 0);
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
      this.Controls.SetChildIndex(this.tlb_ShiftMetresProduced, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint3, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint2, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint1, 0);
      this.Controls.SetChildIndex(this.nud_TargetSpeed, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_Order1.ResumeLayout(false);
      this.pnl_Order2.ResumeLayout(false);
      this.pnl_Order3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void Frm_HomeDry_Load(object sender, EventArgs e)
		{
			//set the bindings only when the program is running because the designer isn't able to do that 
			if (!this.DesignMode)
				setBindings();
		}

		/// <summary>
		/// Set the CausesValidation variable True because it is the default value every time 
		/// a page is accessed.
		/// Only the Escape() method (called on click of pct_Esc and on KeyUp of Esc key)
		/// can set it to false to avoid validations.
		/// </summary>
		private void Frm_HomeDry_Activated(object sender, System.EventArgs e)
		{
			this.CausesValidation = true;
		}
		
		//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
		public void _setLevelBinding_Format(int level, string customer, int sheetsOrder, string theLevelName, string formatNumber)
		{
			TitledLabel tlb_OrderDetails;
			TitledLabel tlb_Customer;
			TitledLabel tlb_SheetsOrder;

			switch(level)
			{
				case 1:
				default:
					tlb_OrderDetails = this.tlb_OrderDetails1;
					tlb_Customer = this.tlb_Customer1;
					tlb_SheetsOrder = this.tlb_SheetsOrder1;
					break;
				case 2:
					tlb_OrderDetails = this.tlb_OrderDetails2;
					tlb_Customer = this.tlb_Customer2;
					tlb_SheetsOrder = this.tlb_SheetsOrder2;
					break;
				case 3:
					tlb_OrderDetails = this.tlb_OrderDetails3;
					tlb_Customer = this.tlb_Customer3;
					tlb_SheetsOrder = this.tlb_SheetsOrder3;
					break;
			}
      //agrassi_FI04-11-2009_1
      //theLevelName = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), theLevelName), "g"));
      LevelName levN = LevelName.Nolevel;
      if ((theLevelName.Length==1) && char.IsNumber(theLevelName, 0) && (this.intParse(theLevelName)>=(int)LevelName.Nolevel) && (this.intParse(theLevelName)<=(int)LevelName.Upper))
        levN=(LevelName)Enum.Parse(typeof(LevelName), theLevelName);
      theLevelName = (Enum.Format(typeof(LevelName), levN, "g"));
      Language lang = DBManager.getDBManager().language;
			tlb_OrderDetails.Text = lang.getTranslation(typeof(LevelName).Name + "." + theLevelName) + " - " + formatNumber;

			tlb_Customer.Value = customer;
			tlb_SheetsOrder.Value = sheetsOrder.ToString();			
		}

		//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
		//private void levelBinding_Format(object sender, ConvertEventArgs e, string formatNumber)
		//{
		//  if (e.Value.ToString().Equals(string.Empty))
		//    return;

		//  Language lang = DBManager.getDBManager().language;
		//  e.Value = lang.getTranslation(typeof(LevelName).Name + "." + e.Value) + " - " + formatNumber;
		//}

		//private void levelBinding_Format0(object sender, ConvertEventArgs e)
		//{
		//  DataRow dataRow = setups.Tables[Order.DRYEND_LIST].Rows[0];
		//  try
		//  {
		//    if (setups.Tables[Order.DRYEND_LIST].Rows.Count > 0 && (dataRow != null) && (dataRow.RowState != DataRowState.Detached))
		//      levelBinding_Format(sender, e, dataRow[Order.DEL_ORDER_NUMBER_0].ToString());
		//    else
		//      levelBinding_Format(sender, e, "");
		//  }
		//  catch (RowNotInTableException ex)
		//  {
		//    Logger.LogError(this, "levelBinding_Format0 - the rowState is not Detached ( it is \"" + dataRow.RowState.ToString() + "\"but the RowNotInTableException has been thrown anyway ....", ex);
		//  }
		//}

		//private void levelBinding_Format1(object sender, ConvertEventArgs e)
		//{
		//  DataRow dataRow = setups.Tables[Order.DRYEND_LIST].Rows[0];
		//  try
		//  {
		//    if (setups.Tables[Order.DRYEND_LIST].Rows.Count > 0 && (dataRow != null) && (dataRow.RowState != DataRowState.Detached))
		//      levelBinding_Format(sender, e, dataRow[Order.DEL_ORDER_NUMBER_1].ToString());
		//    else
		//      levelBinding_Format(sender, e, "");
		//  }
		//  catch (RowNotInTableException ex)
		//  {
		//    Logger.LogError(this, "levelBinding_Format1 - the rowState is not Detached ( it is \"" + dataRow.RowState.ToString() + "\"but the RowNotInTableException has been thrown anyway ....", ex);
		//  }
		//}

		//private void levelBinding_Format2(object sender, ConvertEventArgs e)
		//{
		//  DataRow dataRow = setups.Tables[Order.DRYEND_LIST].Rows[0];
		//  try
		//  {
		//    if (setups.Tables[Order.DRYEND_LIST].Rows.Count > 0 && (dataRow != null) && (dataRow.RowState != DataRowState.Detached))
		//      levelBinding_Format(sender, e, dataRow[Order.DEL_ORDER_NUMBER_2].ToString());
		//    else
		//      levelBinding_Format(sender, e, "");
		//  }
		//  catch (RowNotInTableException ex)
		//  {
		//    Logger.LogError(this, "levelBinding_Format2 - the rowState is not Detached ( it is \"" + dataRow.RowState.ToString() + "\"but the RowNotInTableException has been thrown anyway ....", ex);
		//  }
		//}
	}
}

