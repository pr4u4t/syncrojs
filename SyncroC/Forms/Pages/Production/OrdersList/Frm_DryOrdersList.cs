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
using SyncroC.Forms.PopUp;
using SyncroC.Forms.Pages.Production.OrdersIns;
using SyncroC.Controls.Colors;

namespace SyncroC.Forms.Pages.Production.OrdersList
{
	public class Frm_DryOrdersList : SyncroC.Forms.Pages.Production.OrdersList.Frm_OrdersListSubGroup
	{
		private Label lbl_ModID;
		private Label lbl_SetupID;
		private SyncroC.Controls.GridView gdv_Orders;
		private SyncroC.Controls.Title lbl_DryEndTitle;
		private Label lbl_Customer2;
		private SyncroC.Controls.Title ttl_Order2;
		private SyncroC.Controls.Title ttl_Order1;
		private Label lbl_Customer0;
		private Label lbl_Customer1;
		private Label lbl_Misc0;
		private Label lbl_Sheets0;
		private Label lbl_Sheets1;
		private Label lbl_Misc1;
		private Label lbl_Sheets2;
		private Label lbl_Misc2;
		private SyncroC.Controls.Bracket brk_Details;
		private SyncroC.Controls.Title ttl_Order0;
		private System.ComponentModel.IContainer components = null;

		private DataSet order;
		private DBManager dbm;

		//grid orders columns
		private DataGridViewColumn clm_Order_Pos;
		private DataGridViewColumn clm_Order_SetupCode;
		private DataGridViewColumn clm_Order_Quality;
		private DataGridViewColumn clm_Order_WebWidth;
		private DataGridViewColumn clm_Order_Trim;
		private DataGridViewColumn clm_Order_ScoreGap;
		private DataGridViewColumn clm_Order_Meters;
		private DataGridViewColumn clm_Order_AOC;
    private Label lbl_DataToWiew0_0;
    private Label lbl_DataToWiew0_1;
    private Label lbl_DataToWiew0_3;
    private Label lbl_DataToWiew0_2;
    private Label lbl_DataToWiew1_3;
    private Label lbl_DataToWiew1_2;
    private Label lbl_DataToWiew1_1;
    private Label lbl_DataToWiew1_0;
    private Label lbl_DataToWiew2_3;
    private Label lbl_DataToWiew2_2;
    private Label lbl_DataToWiew2_1;
    private Label lbl_DataToWiew2_0;
    private PictureBox pct_OrderPreprint0;
    private PictureBox pct_OrderPreprint1;
    private PictureBox pct_OrderPreprint2;
    private Label lbl_OfficeID;
		private DataGridViewColumn clm_Order_Time;
    //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
    private DataGridViewColumn clm_Order_Meters_Left;

		public Frm_DryOrdersList()
		{
			InitializeComponent();

      this.dbm = DBManager.getDBManager();
      this.order = this.dbm.order.order;
      this.gdv_Orders.DataSource = this.order.Tables[Order.DRYEND_LIST];

			//gdv_Orders
			clm_Order_Pos = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Pos.Name = Order.DEL_POSITION;
			clm_Order_Pos.HeaderText = "";
			clm_Order_Pos.DataPropertyName = Order.DEL_POSITION;
			clm_Order_Pos.Width = 50;
			clm_Order_Pos.Frozen = true;

			clm_Order_SetupCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_SetupCode.Name = Order.DEL_SETUP_CODE;
			clm_Order_SetupCode.HeaderText = "Code";
			clm_Order_SetupCode.DataPropertyName = Order.DEL_SETUP_CODE;
			clm_Order_SetupCode.Width = 250; //200
			clm_Order_SetupCode.Frozen = true;

			clm_Order_Quality = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Quality.Name = Order.DEL_QUALITY_ID;
			clm_Order_Quality.HeaderText = "Quality";
			clm_Order_Quality.DataPropertyName = Order.DEL_QUALITY_ID;
			clm_Order_Quality.Width = 280;  //250
			clm_Order_Quality.Frozen = true;

			clm_Order_WebWidth = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_WebWidth.Name = Order.DEL_WEB_WIDTH;
			clm_Order_WebWidth.HeaderText = "Width";
			clm_Order_WebWidth.DataPropertyName = Order.DEL_WEB_WIDTH;
			clm_Order_WebWidth.Width = 70;
			clm_Order_WebWidth.Frozen = true;

			clm_Order_Trim = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Trim.Name = Order.DEL_TRIM;
			clm_Order_Trim.HeaderText = "Trim";
			clm_Order_Trim.DataPropertyName = Order.DEL_TRIM;
			clm_Order_Trim.Width = 50;
			clm_Order_Trim.Frozen = true;

			clm_Order_ScoreGap = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_ScoreGap.Name = Order.DEL_SCORE_GAP;
			clm_Order_ScoreGap.HeaderText = "Score Gap";
			clm_Order_ScoreGap.DataPropertyName = Order.DEL_SCORE_GAP;
			clm_Order_ScoreGap.Width = 50;
			clm_Order_ScoreGap.Frozen = true;

			clm_Order_Meters = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Meters.Name = Order.DEL_SHEET_LENGTH_0;
			clm_Order_Meters.HeaderText = "Length";
			clm_Order_Meters.DataPropertyName = Order.DEL_SHEET_LENGTH_0;
			clm_Order_Meters.Width = 100;
			clm_Order_Meters.Frozen = true;

			clm_Order_AOC = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_AOC.Name = Order.DEL_TANDEM;
      clm_Order_AOC.HeaderText = "AOC";
			clm_Order_AOC.DataPropertyName = Order.DEL_TANDEM;
			clm_Order_AOC.Width = 80;
			clm_Order_AOC.Frozen = true;

			clm_Order_Time = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Time.Name = "Time";
      clm_Order_Time.HeaderText = "Time";
			clm_Order_Time.DataPropertyName = "Time";
			clm_Order_Time.Width = 90;
			clm_Order_Time.Frozen = true;

      //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
      clm_Order_Meters_Left = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Meters_Left.Name = "Meters Left";
      clm_Order_Meters_Left.HeaderText = "Meters Left";
      clm_Order_Meters_Left.DataPropertyName = "Meters Left";
      clm_Order_Meters_Left.Width = 130;
      clm_Order_Meters_Left.Frozen = true;


			gdv_Orders.Columns.Add(clm_Order_Pos);
			gdv_Orders.Columns.Add(clm_Order_SetupCode);
			gdv_Orders.Columns.Add(clm_Order_Quality);
			gdv_Orders.Columns.Add(clm_Order_WebWidth);
			gdv_Orders.Columns.Add(clm_Order_Trim);
			gdv_Orders.Columns.Add(clm_Order_ScoreGap);
			gdv_Orders.Columns.Add(clm_Order_Meters);
			gdv_Orders.Columns.Add(clm_Order_AOC);
      gdv_Orders.Columns.Add(clm_Order_Time);
      //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
      gdv_Orders.Columns.Add(clm_Order_Meters_Left);
      gdv_Orders.Sort(clm_Order_Pos, ListSortDirection.Ascending);

			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
			{
				this.lbl_CtrlF1_YY.Text = "";
				this.lbl_CtrlF2_YY.Text = "";
				this.lbl_CtrlF3_YY.Text = "";
				this.lbl_CtrlF5_YY.Text = "";
			}
      //agrassi_FI08-01-09_3
      //this.dbm.order.OnDBOrderChange+=new Order.DBChangedHandler(order_OnDBOrderChange);
		}

    //agrassi_FI08-01-09_3
    //private void order_OnDBOrderChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs e)
    //{
    //  this.gdv_Orders.Invalidate();
    //}

		private void gdv_Orders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
      DataRow dataRow = ((DataRowView)gdv_Orders.Rows[e.RowIndex].DataBoundItem).Row;

			if (e.Value != null)
			{
        //bool valid = false;
        //agrassi_FI08-01-09_3
        try
        {
          gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = (((e.RowIndex % 2) == 0) ? ColConv.Convert(SyncroColor.White) : ColConv.Convert(SyncroColor.LightGray));
          gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColConv.Convert(SyncroColor.Green);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - Exception ", ex);
        }
				try
				{
					bool valid = Convert.ToBoolean(dataRow[Order.DEL_VALID]);
          if (!valid)
          {
            gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Violet;
          }
          //else
          //  gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = (((e.RowIndex % 2) == 0) ? ColConv.Convert(SyncroColor.White) : ColConv.Convert(SyncroColor.LightGray));
        }
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_VALID + " - Error converting \"" + e.Value.ToString() + "\" to bool", ex);
				}
        //agrassi_FI28-10-08
        try
        {
          if (e.RowIndex < gdv_Orders.Rows.Count - 1)
          {
            bool tandemOCWithoutEnabling = Convert.ToBoolean(((DataRowView)gdv_Orders.Rows[e.RowIndex + 1].DataBoundItem).Row[Order.DEL_TANDEM]) && (!DBManager.getDBManager().lineConfig.AOCConfig.AOCTandemEnable);
            if (tandemOCWithoutEnabling)
            {
              gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
              gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.DarkSalmon;
            }
          }
          //else
          //  gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = (((e.RowIndex % 2) == 0) ? ColConv.Convert(SyncroColor.White) : ColConv.Convert(SyncroColor.LightGray));
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_TANDEM + " - Error converting \"" + e.Value.ToString() + "\" to bool", ex);
        }
      }

			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_QUALITY_ID
						&& e.Value != null)
			{
				e.Value = e.Value.ToString().Trim();
			}
			
			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_WEB_WIDTH
						&& e.Value != null)
			{
				try
				{
					e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_WEB_WIDTH + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}

			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_TRIM
						&& e.Value != null)
			{
				try
				{
					e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_TRIM + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}

			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_SHEET_LENGTH_0
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
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_SHEET_LENGTH_0 + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}

			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_TANDEM
						&& e.Value != null)
			{
				

				bool tandem = false;
				bool cs = false;
				bool masterTerminal = false;
				bool NoAOCTerminal1 = false;
				bool NoAOCTerminal2 = false;
				bool NoAOCTerminal3 = false;

				DataRow dataRowNext = null;
				try
				{
          //agrassi_FI28-10-08
          e.Value = " ";
          if (e.RowIndex < gdv_Orders.Rows.Count - 1)
          {
            dataRowNext = ((DataRowView)gdv_Orders.Rows[e.RowIndex + 1].DataBoundItem).Row;
            //Aggiunti colori sulla lista ordini per evidenziare il tipo di cambio ordine (abello_FI12-02-09)
            Color myColor = e.CellStyle.BackColor;

            tandem = Convert.ToBoolean(dataRowNext[Order.DEL_TANDEM]);
            cs = Convert.ToBoolean(dataRowNext[Order.DEL_CONSTANT_SYSTEM]);
            masterTerminal = Convert.ToBoolean(dataRowNext[Order.DEL_KNIFE_ONLY]);
            NoAOCTerminal1 = Convert.ToBoolean(dataRowNext[Order.DEL_NOCHANGE_STK_0]);
            NoAOCTerminal2 = Convert.ToBoolean(dataRowNext[Order.DEL_NOCHANGE_STK_1]);
            NoAOCTerminal3 = Convert.ToBoolean(dataRowNext[Order.DEL_NOCHANGE_STK_2]);
            string typeAOC = "G";
            if (tandem)
            {
              myColor = Color.Yellow;
              typeAOC = "T";
            }
            else
            {
              if (cs)
              {
                typeAOC = "N";
              }
              else
              {
                if (masterTerminal)
                {
                  myColor = Color.Orange;
                  typeAOC = "K";
                }
              }
            }

            if (NoAOCTerminal1)
              typeAOC = typeAOC + "1";
            if (NoAOCTerminal2)
              typeAOC = typeAOC + "2";
            if (NoAOCTerminal3)
              typeAOC = typeAOC + "3";

            e.Value = typeAOC;
            e.CellStyle.BackColor = myColor;
          }
				}
				catch (Exception ex)
				{
					if (dataRowNext == null)
						e.Value = " ";
					else
						Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_TRIM + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
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
			this.gdv_Orders.DataSource = null;
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DryOrdersList));
      this.lbl_ModID = new System.Windows.Forms.Label();
      this.lbl_SetupID = new System.Windows.Forms.Label();
      this.gdv_Orders = new SyncroC.Controls.GridView();
      this.lbl_DryEndTitle = new SyncroC.Controls.Title();
      this.lbl_Customer2 = new System.Windows.Forms.Label();
      this.ttl_Order2 = new SyncroC.Controls.Title();
      this.ttl_Order1 = new SyncroC.Controls.Title();
      this.lbl_Customer0 = new System.Windows.Forms.Label();
      this.lbl_Customer1 = new System.Windows.Forms.Label();
      this.lbl_Misc0 = new System.Windows.Forms.Label();
      this.lbl_Sheets0 = new System.Windows.Forms.Label();
      this.lbl_Sheets1 = new System.Windows.Forms.Label();
      this.lbl_Misc1 = new System.Windows.Forms.Label();
      this.lbl_Sheets2 = new System.Windows.Forms.Label();
      this.lbl_Misc2 = new System.Windows.Forms.Label();
      this.ttl_Order0 = new SyncroC.Controls.Title();
      this.brk_Details = new SyncroC.Controls.Bracket();
      this.lbl_DataToWiew0_0 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_1 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_3 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_3 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_1 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_0 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_3 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_1 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_0 = new System.Windows.Forms.Label();
      this.pct_OrderPreprint0 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint1 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint2 = new System.Windows.Forms.PictureBox();
      this.lbl_OfficeID = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint0)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).BeginInit();
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
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 2);
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF2_YY.Text = "Move";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF5_YY.Text = "Erase All";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF3_YY.Text = "Erase";
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // lbl_ModID
      // 
      this.lbl_ModID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ModID.CausesValidation = false;
      this.lbl_ModID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ModID.Location = new System.Drawing.Point(913, 172);
      this.lbl_ModID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_ModID.Name = "lbl_ModID";
      this.lbl_ModID.Size = new System.Drawing.Size(162, 18);
      this.lbl_ModID.TabIndex = 152;
      this.lbl_ModID.Text = "ModID";
      this.lbl_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ModID.Visible = false;
      // 
      // lbl_SetupID
      // 
      this.lbl_SetupID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupID.CausesValidation = false;
      this.lbl_SetupID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupID.Location = new System.Drawing.Point(744, 173);
      this.lbl_SetupID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_SetupID.Name = "lbl_SetupID";
      this.lbl_SetupID.Size = new System.Drawing.Size(162, 18);
      this.lbl_SetupID.TabIndex = 151;
      this.lbl_SetupID.Text = "SetUpID";
      this.lbl_SetupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_SetupID.Visible = false;
      // 
      // gdv_Orders
      // 
      this.gdv_Orders.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Orders.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Orders.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Orders.AutoGenerateColumns = false;
      this.gdv_Orders.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Orders.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Orders.DataMember = "";
      this.gdv_Orders.DataSource = null;
      this.gdv_Orders.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Orders.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.ImagePassword = null;
      this.gdv_Orders.Location = new System.Drawing.Point(42, 224);
      this.gdv_Orders.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Orders.MultiSelect = true;
      this.gdv_Orders.Name = "gdv_Orders";
      this.gdv_Orders.RowsHeight = 30;
      this.gdv_Orders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Orders.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Orders.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Orders.Size = new System.Drawing.Size(1159, 317);
      this.gdv_Orders.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Orders.TabIndex = 148;
      this.gdv_Orders.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Orders.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.TranslateText = true;
      this.gdv_Orders.DoubleClick += new System.EventHandler(this.gdv_Orders_DoubleClick);
      this.gdv_Orders.SelectionChanged += new System.EventHandler(this.gdv_Orders_SelectionChanged);
      this.gdv_Orders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Orders_CellFormatting);
      // 
      // lbl_DryEndTitle
      // 
      this.lbl_DryEndTitle.BackColor = System.Drawing.Color.Black;
      this.lbl_DryEndTitle.CausesValidation = false;
      this.lbl_DryEndTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lbl_DryEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DryEndTitle.ForeColor = System.Drawing.Color.White;
      this.lbl_DryEndTitle.Location = new System.Drawing.Point(30, 193);
      this.lbl_DryEndTitle.Name = "lbl_DryEndTitle";
      this.lbl_DryEndTitle.Size = new System.Drawing.Size(1171, 348);
      this.lbl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.lbl_DryEndTitle.TabIndex = 147;
      this.lbl_DryEndTitle.Text = "Dry End Orders List";
      this.lbl_DryEndTitle.TranslateText = true;
      // 
      // lbl_Customer2
      // 
      this.lbl_Customer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer2.CausesValidation = false;
      this.lbl_Customer2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer2.Location = new System.Drawing.Point(824, 602);
      this.lbl_Customer2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.lbl_Customer2.Name = "lbl_Customer2";
      this.lbl_Customer2.Size = new System.Drawing.Size(365, 30);
      this.lbl_Customer2.TabIndex = 146;
      this.lbl_Customer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ttl_Order2
      // 
      this.ttl_Order2.BackColor = System.Drawing.Color.Black;
      this.ttl_Order2.CausesValidation = false;
      this.ttl_Order2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order2.ForeColor = System.Drawing.Color.White;
      this.ttl_Order2.Location = new System.Drawing.Point(816, 575);
      this.ttl_Order2.Name = "ttl_Order2";
      this.ttl_Order2.Size = new System.Drawing.Size(374, 154);
      this.ttl_Order2.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order2.TabIndex = 145;
      this.ttl_Order2.TranslateText = false;
      // 
      // ttl_Order1
      // 
      this.ttl_Order1.BackColor = System.Drawing.Color.Black;
      this.ttl_Order1.CausesValidation = false;
      this.ttl_Order1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order1.ForeColor = System.Drawing.Color.White;
      this.ttl_Order1.Location = new System.Drawing.Point(429, 575);
      this.ttl_Order1.Name = "ttl_Order1";
      this.ttl_Order1.Size = new System.Drawing.Size(374, 154);
      this.ttl_Order1.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order1.TabIndex = 144;
      this.ttl_Order1.TranslateText = false;
      // 
      // lbl_Customer0
      // 
      this.lbl_Customer0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer0.CausesValidation = false;
      this.lbl_Customer0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer0.Location = new System.Drawing.Point(50, 602);
      this.lbl_Customer0.Name = "lbl_Customer0";
      this.lbl_Customer0.Size = new System.Drawing.Size(365, 30);
      this.lbl_Customer0.TabIndex = 136;
      this.lbl_Customer0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Customer1
      // 
      this.lbl_Customer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer1.CausesValidation = false;
      this.lbl_Customer1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer1.Location = new System.Drawing.Point(437, 602);
      this.lbl_Customer1.Name = "lbl_Customer1";
      this.lbl_Customer1.Size = new System.Drawing.Size(365, 30);
      this.lbl_Customer1.TabIndex = 138;
      this.lbl_Customer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Misc0
      // 
      this.lbl_Misc0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc0.CausesValidation = false;
      this.lbl_Misc0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc0.Location = new System.Drawing.Point(50, 635);
      this.lbl_Misc0.Name = "lbl_Misc0";
      this.lbl_Misc0.Size = new System.Drawing.Size(234, 32);
      this.lbl_Misc0.TabIndex = 153;
      this.lbl_Misc0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Sheets0
      // 
      this.lbl_Sheets0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Sheets0.CausesValidation = false;
      this.lbl_Sheets0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Sheets0.Location = new System.Drawing.Point(317, 632);
      this.lbl_Sheets0.Name = "lbl_Sheets0";
      this.lbl_Sheets0.Size = new System.Drawing.Size(99, 32);
      this.lbl_Sheets0.TabIndex = 155;
      this.lbl_Sheets0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Sheets1
      // 
      this.lbl_Sheets1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Sheets1.CausesValidation = false;
      this.lbl_Sheets1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Sheets1.Location = new System.Drawing.Point(704, 635);
      this.lbl_Sheets1.Name = "lbl_Sheets1";
      this.lbl_Sheets1.Size = new System.Drawing.Size(99, 32);
      this.lbl_Sheets1.TabIndex = 158;
      this.lbl_Sheets1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Misc1
      // 
      this.lbl_Misc1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc1.CausesValidation = false;
      this.lbl_Misc1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc1.Location = new System.Drawing.Point(437, 635);
      this.lbl_Misc1.Name = "lbl_Misc1";
      this.lbl_Misc1.Size = new System.Drawing.Size(234, 32);
      this.lbl_Misc1.TabIndex = 156;
      this.lbl_Misc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Sheets2
      // 
      this.lbl_Sheets2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Sheets2.CausesValidation = false;
      this.lbl_Sheets2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Sheets2.Location = new System.Drawing.Point(1091, 635);
      this.lbl_Sheets2.Name = "lbl_Sheets2";
      this.lbl_Sheets2.Size = new System.Drawing.Size(99, 32);
      this.lbl_Sheets2.TabIndex = 161;
      this.lbl_Sheets2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Misc2
      // 
      this.lbl_Misc2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc2.CausesValidation = false;
      this.lbl_Misc2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc2.Location = new System.Drawing.Point(824, 635);
      this.lbl_Misc2.Name = "lbl_Misc2";
      this.lbl_Misc2.Size = new System.Drawing.Size(234, 32);
      this.lbl_Misc2.TabIndex = 159;
      this.lbl_Misc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ttl_Order0
      // 
      this.ttl_Order0.BackColor = System.Drawing.Color.Black;
      this.ttl_Order0.CausesValidation = false;
      this.ttl_Order0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order0.ForeColor = System.Drawing.Color.White;
      this.ttl_Order0.Location = new System.Drawing.Point(42, 575);
      this.ttl_Order0.Name = "ttl_Order0";
      this.ttl_Order0.Size = new System.Drawing.Size(374, 154);
      this.ttl_Order0.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order0.TabIndex = 149;
      this.ttl_Order0.TranslateText = false;
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.SystemColors.Window;
      this.brk_Details.Location = new System.Drawing.Point(30, 547);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.brk_Details.ShowArrow = false;
      this.brk_Details.Size = new System.Drawing.Size(1171, 182);
      this.brk_Details.TabIndex = 162;
      this.brk_Details.Text = "Details";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
      // 
      // lbl_DataToWiew0_0
      // 
      this.lbl_DataToWiew0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_0.CausesValidation = false;
      this.lbl_DataToWiew0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_0.Location = new System.Drawing.Point(50, 671);
      this.lbl_DataToWiew0_0.Name = "lbl_DataToWiew0_0";
      this.lbl_DataToWiew0_0.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew0_0.TabIndex = 163;
      this.lbl_DataToWiew0_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_1
      // 
      this.lbl_DataToWiew0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_1.CausesValidation = false;
      this.lbl_DataToWiew0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_1.Location = new System.Drawing.Point(257, 671);
      this.lbl_DataToWiew0_1.Name = "lbl_DataToWiew0_1";
      this.lbl_DataToWiew0_1.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew0_1.TabIndex = 164;
      this.lbl_DataToWiew0_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_3
      // 
      this.lbl_DataToWiew0_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_3.CausesValidation = false;
      this.lbl_DataToWiew0_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_3.Location = new System.Drawing.Point(257, 702);
      this.lbl_DataToWiew0_3.Name = "lbl_DataToWiew0_3";
      this.lbl_DataToWiew0_3.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew0_3.TabIndex = 166;
      this.lbl_DataToWiew0_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_2
      // 
      this.lbl_DataToWiew0_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_2.CausesValidation = false;
      this.lbl_DataToWiew0_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_2.Location = new System.Drawing.Point(50, 702);
      this.lbl_DataToWiew0_2.Name = "lbl_DataToWiew0_2";
      this.lbl_DataToWiew0_2.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew0_2.TabIndex = 165;
      this.lbl_DataToWiew0_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_3
      // 
      this.lbl_DataToWiew1_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_3.CausesValidation = false;
      this.lbl_DataToWiew1_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_3.Location = new System.Drawing.Point(644, 702);
      this.lbl_DataToWiew1_3.Name = "lbl_DataToWiew1_3";
      this.lbl_DataToWiew1_3.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew1_3.TabIndex = 170;
      this.lbl_DataToWiew1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_2
      // 
      this.lbl_DataToWiew1_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_2.CausesValidation = false;
      this.lbl_DataToWiew1_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_2.Location = new System.Drawing.Point(437, 702);
      this.lbl_DataToWiew1_2.Name = "lbl_DataToWiew1_2";
      this.lbl_DataToWiew1_2.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew1_2.TabIndex = 169;
      this.lbl_DataToWiew1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_1
      // 
      this.lbl_DataToWiew1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_1.CausesValidation = false;
      this.lbl_DataToWiew1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_1.Location = new System.Drawing.Point(644, 671);
      this.lbl_DataToWiew1_1.Name = "lbl_DataToWiew1_1";
      this.lbl_DataToWiew1_1.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew1_1.TabIndex = 168;
      this.lbl_DataToWiew1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_0
      // 
      this.lbl_DataToWiew1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_0.CausesValidation = false;
      this.lbl_DataToWiew1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_0.Location = new System.Drawing.Point(437, 671);
      this.lbl_DataToWiew1_0.Name = "lbl_DataToWiew1_0";
      this.lbl_DataToWiew1_0.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew1_0.TabIndex = 167;
      this.lbl_DataToWiew1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_3
      // 
      this.lbl_DataToWiew2_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_3.CausesValidation = false;
      this.lbl_DataToWiew2_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_3.Location = new System.Drawing.Point(1031, 702);
      this.lbl_DataToWiew2_3.Name = "lbl_DataToWiew2_3";
      this.lbl_DataToWiew2_3.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew2_3.TabIndex = 174;
      this.lbl_DataToWiew2_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_2
      // 
      this.lbl_DataToWiew2_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_2.CausesValidation = false;
      this.lbl_DataToWiew2_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_2.Location = new System.Drawing.Point(824, 702);
      this.lbl_DataToWiew2_2.Name = "lbl_DataToWiew2_2";
      this.lbl_DataToWiew2_2.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew2_2.TabIndex = 173;
      this.lbl_DataToWiew2_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_1
      // 
      this.lbl_DataToWiew2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_1.CausesValidation = false;
      this.lbl_DataToWiew2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_1.Location = new System.Drawing.Point(1031, 671);
      this.lbl_DataToWiew2_1.Name = "lbl_DataToWiew2_1";
      this.lbl_DataToWiew2_1.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew2_1.TabIndex = 172;
      this.lbl_DataToWiew2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_0
      // 
      this.lbl_DataToWiew2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_0.CausesValidation = false;
      this.lbl_DataToWiew2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_0.Location = new System.Drawing.Point(824, 671);
      this.lbl_DataToWiew2_0.Name = "lbl_DataToWiew2_0";
      this.lbl_DataToWiew2_0.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew2_0.TabIndex = 171;
      this.lbl_DataToWiew2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_OrderPreprint0
      // 
      this.pct_OrderPreprint0.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint0.Image")));
      this.pct_OrderPreprint0.Location = new System.Drawing.Point(381, 578);
      this.pct_OrderPreprint0.Name = "pct_OrderPreprint0";
      this.pct_OrderPreprint0.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint0.TabIndex = 260;
      this.pct_OrderPreprint0.TabStop = false;
      this.pct_OrderPreprint0.Visible = false;
      // 
      // pct_OrderPreprint1
      // 
      this.pct_OrderPreprint1.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint1.Image")));
      this.pct_OrderPreprint1.Location = new System.Drawing.Point(769, 578);
      this.pct_OrderPreprint1.Name = "pct_OrderPreprint1";
      this.pct_OrderPreprint1.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint1.TabIndex = 262;
      this.pct_OrderPreprint1.TabStop = false;
      this.pct_OrderPreprint1.Visible = false;
      // 
      // pct_OrderPreprint2
      // 
      this.pct_OrderPreprint2.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint2.Image")));
      this.pct_OrderPreprint2.Location = new System.Drawing.Point(1155, 578);
      this.pct_OrderPreprint2.Name = "pct_OrderPreprint2";
      this.pct_OrderPreprint2.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint2.TabIndex = 261;
      this.pct_OrderPreprint2.TabStop = false;
      this.pct_OrderPreprint2.Visible = false;
      // 
      // lbl_OfficeID
      // 
      this.lbl_OfficeID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_OfficeID.CausesValidation = false;
      this.lbl_OfficeID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OfficeID.Location = new System.Drawing.Point(562, 172);
      this.lbl_OfficeID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_OfficeID.Name = "lbl_OfficeID";
      this.lbl_OfficeID.Size = new System.Drawing.Size(162, 18);
      this.lbl_OfficeID.TabIndex = 263;
      this.lbl_OfficeID.Text = "OfficeID";
      this.lbl_OfficeID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_OfficeID.Visible = false;
      // 
      // Frm_DryOrdersList
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_OfficeID);
      this.Controls.Add(this.pct_OrderPreprint1);
      this.Controls.Add(this.pct_OrderPreprint2);
      this.Controls.Add(this.pct_OrderPreprint0);
      this.Controls.Add(this.lbl_DataToWiew2_3);
      this.Controls.Add(this.lbl_DataToWiew2_2);
      this.Controls.Add(this.lbl_DataToWiew2_1);
      this.Controls.Add(this.lbl_DataToWiew2_0);
      this.Controls.Add(this.lbl_DataToWiew1_3);
      this.Controls.Add(this.lbl_DataToWiew1_2);
      this.Controls.Add(this.lbl_DataToWiew1_1);
      this.Controls.Add(this.lbl_DataToWiew1_0);
      this.Controls.Add(this.lbl_DataToWiew0_3);
      this.Controls.Add(this.lbl_DataToWiew0_2);
      this.Controls.Add(this.lbl_DataToWiew0_1);
      this.Controls.Add(this.lbl_DataToWiew0_0);
      this.Controls.Add(this.lbl_ModID);
      this.Controls.Add(this.lbl_SetupID);
      this.Controls.Add(this.gdv_Orders);
      this.Controls.Add(this.lbl_DryEndTitle);
      this.Controls.Add(this.brk_Details);
      this.Controls.Add(this.ttl_Order0);
      this.Controls.Add(this.lbl_Sheets0);
      this.Controls.Add(this.lbl_Misc0);
      this.Controls.Add(this.ttl_Order1);
      this.Controls.Add(this.lbl_Misc1);
      this.Controls.Add(this.lbl_Sheets1);
      this.Controls.Add(this.lbl_Customer1);
      this.Controls.Add(this.ttl_Order2);
      this.Controls.Add(this.lbl_Sheets2);
      this.Controls.Add(this.lbl_Misc2);
      this.Controls.Add(this.lbl_Customer0);
      this.Controls.Add(this.lbl_Customer2);
      this.Name = "Frm_DryOrdersList";
      this.Text = "Frm_DryOrdersList";
      this.Load += new System.EventHandler(this.Frm_DryOrdersList_Load);
      this.Activated += new System.EventHandler(this.Frm_DryOrdersList_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lbl_Customer2, 0);
      this.Controls.SetChildIndex(this.lbl_Customer0, 0);
      this.Controls.SetChildIndex(this.lbl_Misc2, 0);
      this.Controls.SetChildIndex(this.lbl_Sheets2, 0);
      this.Controls.SetChildIndex(this.ttl_Order2, 0);
      this.Controls.SetChildIndex(this.lbl_Customer1, 0);
      this.Controls.SetChildIndex(this.lbl_Sheets1, 0);
      this.Controls.SetChildIndex(this.lbl_Misc1, 0);
      this.Controls.SetChildIndex(this.ttl_Order1, 0);
      this.Controls.SetChildIndex(this.lbl_Misc0, 0);
      this.Controls.SetChildIndex(this.lbl_Sheets0, 0);
      this.Controls.SetChildIndex(this.ttl_Order0, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      this.Controls.SetChildIndex(this.lbl_DryEndTitle, 0);
      this.Controls.SetChildIndex(this.gdv_Orders, 0);
      this.Controls.SetChildIndex(this.lbl_SetupID, 0);
      this.Controls.SetChildIndex(this.lbl_ModID, 0);
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
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_0, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_2, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_3, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_0, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_2, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_3, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_0, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_2, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_3, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint0, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint2, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint1, 0);
      this.Controls.SetChildIndex(this.lbl_OfficeID, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint0)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Move selected group of orders in a different position.
		/// </summary>
		protected override void CtrlF2()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

			if (gdv_Orders.Rows.Count <= 0)
				return;
			Frm_MoveOrders moveOrders = new Frm_MoveOrders();
			int first = 0;
			int last = 0;
			if (gdv_Orders.SelectedRows.Count > 0)
			{
				first = gdv_Orders.SelectedRows.Count > 0 ? gdv_Orders.SelectedRows[gdv_Orders.SelectedRows.Count - 1].Index + 1 : 1;
				last = gdv_Orders.SelectedRows.Count > 0 ? gdv_Orders.SelectedRows[0].Index + 1 : 1;
			}
			FosberDialogResult ret = moveOrders.ShowDialog(this,
					Math.Min(first, last), Math.Max(first, last));
			if (ret.Dialog == DialogResult.OK)
			{
        //agrassi_FI21-05-08_1
        DBManager dbm = DBManager.getDBManager();
        int firstOrderToMovePos = (int)ret.Data[0];
				int lastOrderToMovePos = (int)ret.Data[1];
				int destinationPos = (int)ret.Data[2];
				if ((firstOrderToMovePos != -1) && (lastOrderToMovePos != -1) && (destinationPos != -1) &&//no conversion errors occured
					(firstOrderToMovePos >= 2) && (firstOrderToMovePos <= gdv_Orders.Rows.Count) &&				//firstOrderToMovePos is consistent
					(lastOrderToMovePos >= 1) && (lastOrderToMovePos <= gdv_Orders.Rows.Count) &&					//lastOrderToMovePos is consistent
					(firstOrderToMovePos <= lastOrderToMovePos) &&																				//firstOrderToMovePos and lastOrderToMovePos are compatible		 
					(destinationPos >= 2) && (destinationPos <= gdv_Orders.Rows.Count + 1) &&									//destinationPos is consistent
					((destinationPos > lastOrderToMovePos+1) || (destinationPos < firstOrderToMovePos)))	//destinationPos, firstOrderToMovePos, lastOrderToMovePos are comatble
				{
					CommunicationManager cm = CommunicationManager.GetCommunicationManager();
					StringBuilder command = new StringBuilder(Command.DB_ORDER + "." + Command.DB_CMD_MOVE + "(" +
							destinationPos.ToString() + Command.PARAMETER_SEPARATOR);
					for (int i = firstOrderToMovePos - 1; i <= lastOrderToMovePos - 1; i++)
					{
						command.Append(dbm.order.order.Tables[Order.DRYEND_LIST].Rows[i][Order.DEL_SETUP_ID].ToString());
						if (i != lastOrderToMovePos - 1)
							command.Append(Command.PARAMETER_SEPARATOR);
					}
					command.Append(")");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, command.ToString());
					CleanOrderDetails(true, true, true);
				}
        //agrassi_FI21-05-08_1
        else
        {
          Frm_Alert frm_alert = new Frm_Alert();
          frm_alert.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_10"));
        }
      }
		}

		/// <summary>
		/// Delete
		/// </summary>
		protected override void CtrlF3()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

			if (gdv_Orders.Rows.Count <= 0)
				return;
			DBManager dbm = DBManager.getDBManager();
			StringBuilder command = new StringBuilder(Command.DB_ORDER + "." + Command.DB_CMD_DEL + "(");
			bool send = false;

			if (this.gdv_Orders.IsConsecutiveSelection())
			{
				Frm_MultipleDelete popUp = new Frm_MultipleDelete();
				int first = gdv_Orders.SelectedRows.Count > 0 ? gdv_Orders.SelectedRows[gdv_Orders.SelectedRows.Count - 1].Index + 1 : 1;
				int last = gdv_Orders.SelectedRows.Count > 0 ? gdv_Orders.SelectedRows[0].Index + 1 : 1;
				FosberDialogResult ret = popUp.ShowDialog(this, Math.Min(first, last), Math.Max(first, last));
				if (ret.Dialog == DialogResult.OK)
				{
					int firstOrderToDeletePos = (int)ret.Data[0];
					int lastOrderToDeletePos = (int)ret.Data[1];
					if ((firstOrderToDeletePos != -1) && (lastOrderToDeletePos != -1) &&										//no conversion errors occured
						(firstOrderToDeletePos >= 2) && (firstOrderToDeletePos <= gdv_Orders.Rows.Count) &&	//firstOrderToDeletePos is consistent
						(lastOrderToDeletePos >= 1) && (lastOrderToDeletePos <= gdv_Orders.Rows.Count) &&     //lastOrderToDeletePos is consistent
						(firstOrderToDeletePos <= lastOrderToDeletePos))																			//firstOrderToDeletePos and lastOrderToDeletePos are compatible		 
					{
						Frm_OrdersInsSubGroup.indexOrderToShowDRY = firstOrderToDeletePos - 2; //-1 (from pos to index) -1 (because we want to select the previous)
						//specified interval of the pop up is considered because it could be different from the selection.
						for (int i = firstOrderToDeletePos - 1; i <= lastOrderToDeletePos - 1; i++)
						{
							command.Append(dbm.order.order.Tables[Order.DRYEND_LIST].Rows[i][Order.DEL_SETUP_ID].ToString());
							if (i != lastOrderToDeletePos - 1)
								command.Append(Command.PARAMETER_SEPARATOR);
							send = true;
						}
					}
          //agrassi_FI21-05-08_1
          else
          {
            Frm_Alert frm_alert = new Frm_Alert();
            frm_alert.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_10"));
          }
        }
			}
			else
			{
				Frm_Confirm popUp = new Frm_Confirm();
				string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_20");
				DataRowCollection rows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
				for (int i = gdv_Orders.SelectedRows.Count - 1; i >= 0; i--)
				{
					text += (gdv_Orders.SelectedRows[i].Index + 1).ToString() + (i > 0 ? ", " : "");
					if (i == 0)
						Frm_OrdersInsSubGroup.indexOrderToShowDRY = gdv_Orders.SelectedRows[i].Index;
					command.Append(rows[gdv_Orders.SelectedRows[i].Index][Order.DEL_SETUP_ID].ToString());
					if (i != 0)
						command.Append(Command.PARAMETER_SEPARATOR);
				}
				send = (popUp.ShowDialog(this, text) == DialogResult.OK);
			}
			command.Append(")");
			if (send)
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, command.ToString());
				CleanOrderDetails(true, true, true);
				if (this.gdv_Orders.Rows.Count > Frm_OrdersInsSubGroup.indexOrderToShowDRY)
				{
					foreach (DataGridViewRow row in this.gdv_Orders.SelectedRows)
						row.Selected = false;
					this.gdv_Orders.Rows[Frm_OrdersInsSubGroup.indexOrderToShowDRY].Selected = true;
				}
			}
		}

		/// <summary>
		/// Delete all orders.
		/// </summary>
		protected override void CtrlF5()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

			if (gdv_Orders.Rows.Count <= 0)
				return;
			Frm_Confirm eraseAll = new Frm_Confirm();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_21");
			DialogResult ret = eraseAll.ShowDialog(this, text);
			if (ret == DialogResult.OK)
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_ORDER + "." + Command.DB_CMD_CLR + "()");
				CleanOrderDetails(true, true, true);
			}
		}

    private string getDataToView(int level, int number, DataRow dataRow)
    {
      string res = "";
      switch (number)
      {
        case 0:
        default:
          res = dataRow[Order.DEL_MTH_CODE_n + level.ToString()].ToString();
          break;
        case 1:
          res = dataRow[Order.DEL_MTH_NEXT_MACHINE_n + level.ToString()].ToString();
          break;
        case 2:
          res = dataRow[Order.DEL_STACKER_NOTE_n + level.ToString()].ToString();
          break;
        case 3:
          res = dataRow[Order.DEL_CUT_TO_PATTERN_n + level.ToString()].ToString();
          break;
        case 4:
          res = dataRow[Order.DEL_DATA_TO_PRINT_n_m + level.ToString() + "_1"].ToString();
          break;
        case 5:
          res = dataRow[Order.DEL_DATA_TO_PRINT_n_m + level.ToString() + "_2"].ToString();
          break;
        case 6:
          res = dataRow[Order.DEL_DATA_TO_PRINT_n_m + level.ToString() + "_3"].ToString();
          break;
        case 7:
          res = dataRow[Order.DEL_DATA_TO_PRINT_n_m + level.ToString() + "_4"].ToString();
          break;
      }
      return res;
    }

		/// <summary>
		/// Fill the dry end details with new selected order
		/// </summary>
		private void gdv_Orders_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_Orders.SelectedRows.Count == 1)
			{
				DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
				if (dataRow != null && dataRow.RowState != DataRowState.Detached)
				{
					try
					{
						//order 0
						ttl_Order0.Text = dbm.language.getTranslation(
							typeof(LevelName).Name + "." +  dataRow[Order.DEL_LEVEL_NAME_0].ToString()) + 
							" - " + dataRow[Order.DEL_ORDER_NUMBER_0].ToString();
						lbl_Misc0.Text = SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
							" X " + SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
							" X " + dataRow[Order.DEL_OUTS_0].ToString();
						int sheets = 0;
						try
						{
							sheets = Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_0].ToString());
						}
						catch (Exception ex)
						{
							Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting Order.DEL_SEGMENT_SHEET_COUNT_0 or Order.DEL_OUTS_0", ex);
						}
						lbl_Sheets0.Text = sheets.ToString();
						lbl_Customer0.Text = dataRow[Order.DEL_CUSTOMER_NAME_0].ToString();

            this.lbl_DataToWiew0_0.Text = this.getDataToView(0, 0, dataRow);
            this.lbl_DataToWiew0_1.Text = this.getDataToView(0, 1, dataRow);
            this.lbl_DataToWiew0_2.Text = this.getDataToView(0, 2, dataRow);
            this.lbl_DataToWiew0_3.Text = this.getDataToView(0, 3, dataRow);
                        //ZETA 01
                        this.pct_OrderPreprint0.Visible = Convert.ToBoolean(dataRow[Order.DEL_PREPRINT_0].ToString());
						
            //order 1 
						if (Convert.ToInt64(dataRow[Order.DEL_SHEET_LENGTH_1].ToString()) != 0)
						{
							ttl_Order1.Text = dbm.language.getTranslation(
							typeof(LevelName).Name + "." + dataRow[Order.DEL_LEVEL_NAME_1].ToString()) + 
							" - " +	dataRow[Order.DEL_ORDER_NUMBER_1].ToString();
							lbl_Misc1.Text = SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
								" X " + SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
								" X " + dataRow[Order.DEL_OUTS_1].ToString();
							sheets = 0;
							try
							{
								sheets = Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_1].ToString());
							}
							catch (Exception ex)
							{
								Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting Order.DEL_SEGMENT_SHEET_COUNT_1 or Order.DEL_OUTS_1", ex);
							}
							lbl_Sheets1.Text = sheets.ToString();
							lbl_Customer1.Text = dataRow[Order.DEL_CUSTOMER_NAME_1].ToString();
                            //ZETA 01
              this.lbl_DataToWiew1_0.Text = this.getDataToView(1, 0, dataRow);
              this.lbl_DataToWiew1_1.Text = this.getDataToView(1, 1, dataRow);
              this.lbl_DataToWiew1_2.Text = this.getDataToView(1, 2, dataRow);
              this.lbl_DataToWiew1_3.Text = this.getDataToView(1, 3, dataRow);
              //ZETA 01
              this.pct_OrderPreprint1.Visible = Convert.ToBoolean(dataRow[Order.DEL_PREPRINT_1].ToString());

						}
						else
						{
							CleanOrderDetails(false, true, false);
						}
						//order 2
						if (Convert.ToInt64(dataRow[Order.DEL_SHEET_LENGTH_2].ToString()) != 0)
						{
							ttl_Order2.Text = dbm.language.getTranslation(
								typeof(LevelName).Name + "." + dataRow[Order.DEL_LEVEL_NAME_2].ToString()) + 
								" - " + dataRow[Order.DEL_ORDER_NUMBER_2].ToString();
							lbl_Misc2.Text = SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
								" X " + SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
								" X " + dataRow[Order.DEL_OUTS_2].ToString();
							sheets = 0;
							try
							{
								sheets = Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_2].ToString());
							}
							catch (Exception ex)
							{
								Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting Order.DEL_SEGMENT_SHEET_COUNT_2 or Order.DEL_OUTS_2", ex);
							}
							lbl_Sheets2.Text = sheets.ToString();
							lbl_Customer2.Text = dataRow[Order.DEL_CUSTOMER_NAME_2].ToString();
                            //ZETA 01
              this.lbl_DataToWiew2_0.Text = this.getDataToView(2, 0, dataRow);
              this.lbl_DataToWiew2_1.Text = this.getDataToView(2, 1, dataRow);
              this.lbl_DataToWiew2_2.Text = this.getDataToView(2, 2, dataRow);
              this.lbl_DataToWiew2_3.Text = this.getDataToView(2, 3, dataRow);
              //ZETA 01
              this.pct_OrderPreprint2.Visible = Convert.ToBoolean(dataRow[Order.DEL_PREPRINT_2].ToString());

						}
						else
						{
							CleanOrderDetails(false, false, true);
						}
						//debug info
						lbl_SetupID.Text = "SetUpID: " + dataRow[Order.DEL_SETUP_ID].ToString();
            lbl_ModID.Text = "ModID: " + dataRow[Order.DEL_SETUP_MODID].ToString();
            //agrassi_FI26-05-08_1
            lbl_OfficeID.Text = "OfficeID: " + dataRow[Order.DEL_OFFICE_ID].ToString();
          }
					catch (RowNotInTableException ex)
					{
						Logger.LogError(this, "gdv_Orders_SelectionChanged - the rowState is not Detached (it is \"" + dataRow.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
					}
				}
			}
			else
			{
				CleanOrderDetails(true, true, true);
			}
		}

		/// <summary>
		/// Clean all controls for order details
		/// </summary>
		private void CleanOrderDetails(bool level0, bool level1, bool level2)
		{
      if (level0)
      {
        ttl_Order0.Text = lbl_Customer0.Text = lbl_Misc0.Text = lbl_Sheets0.Text = String.Empty;
        this.lbl_DataToWiew0_0.Text = String.Empty;
        this.lbl_DataToWiew0_1.Text = String.Empty;
        this.lbl_DataToWiew0_2.Text = String.Empty;
        this.lbl_DataToWiew0_3.Text = String.Empty;
        this.pct_OrderPreprint0.Visible = false;
      }
      if (level1)
      {
        ttl_Order1.Text = lbl_Customer1.Text = lbl_Misc1.Text = lbl_Sheets1.Text = String.Empty;
        this.lbl_DataToWiew1_0.Text = String.Empty;
        this.lbl_DataToWiew1_1.Text = String.Empty;
        this.lbl_DataToWiew1_2.Text = String.Empty;
        this.lbl_DataToWiew1_3.Text = String.Empty;
        this.pct_OrderPreprint1.Visible = false;
      }
      if (level2)
      {
        ttl_Order2.Text = lbl_Customer2.Text = lbl_Misc2.Text = lbl_Sheets2.Text = String.Empty;
        this.lbl_DataToWiew2_0.Text = String.Empty;
        this.lbl_DataToWiew2_1.Text = String.Empty;
        this.lbl_DataToWiew2_2.Text = String.Empty;
        this.lbl_DataToWiew2_3.Text = String.Empty;
        this.pct_OrderPreprint2.Visible = false;
      }
			if (level0 && level1 && level2)
			{//debug info
				lbl_SetupID.Text = "SetupID: ";
        lbl_ModID.Text = "ModID: ";
        //agrassi_FI26-05-08_1
        lbl_OfficeID.Text = "OfficeID: ";
      }
		}

		/// <summary>
		/// Show / Hide debug info.
		/// </summary>
		protected override void ControlD()
		{
			base.ControlD();
      //agrassi_FI26-05-08_1
      lbl_SetupID.Visible = lbl_ModID.Visible = lbl_OfficeID.Visible = (!lbl_SetupID.Visible);
      //lbl_SetupID.Visible = lbl_ModID.Visible = !lbl_SetupID.Visible;
    }

		private void Frm_DryOrdersList_Activated(object sender, EventArgs e)
		{
			if (this.gdv_Orders.Rows.Count > Frm_OrdersInsSubGroup.indexOrderToShowDRY)
			{
				foreach (DataGridViewRow row in this.gdv_Orders.SelectedRows)
					row.Selected = false;
				this.gdv_Orders.Rows[Frm_OrdersInsSubGroup.indexOrderToShowDRY].Selected = true;
			}
      //agrassi_FI08-01-09_3
      //this.gdv_Orders.Invalidate();
		}

		private void gdv_Orders_DoubleClick(object sender, EventArgs e)
		{
			if (gdv_Orders.SelectedRows.Count == 1 &&
					order.Tables[Order.DRYEND_LIST].Rows.Count >= gdv_Orders.SelectedRows[0].Index)
			{
				Frm_OrdersInsSubGroup.indexOrderToShowDRY = gdv_Orders.SelectedRows[0].Index;
			}
			askForPage(typeof(Frm_QuickInsDryOrder));
		}

		private void _SetSetupStartTime(string currentSystemTime, int theSpeed_UVm_m, int metersLeftCurrent)
		{
      if (this.gdv_Orders.Rows.Count <= 1)
        return;
			DateTime now = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(currentSystemTime);
			now = now.ToLocalTime();

      //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
      int metersToStart = 0;
      
      for (int i = 0; i < this.gdv_Orders.Rows.Count; i++)
      {
        DataRow setup = ((DataRowView)this.gdv_Orders.Rows[i].DataBoundItem).Row;
        int cutsDuration = this.intParse(setup[Order.DEL_SEGMENT_SHEET_COUNT_0].ToString()) / this.intParse(setup[Order.DEL_OUTS_0].ToString());
        int metersDuration = (cutsDuration * this.intParse(setup[Order.DEL_SHEET_LENGTH_0].ToString())) / 10000;
        
        if (i == 0)
        {
          metersDuration = metersLeftCurrent;
          this.gdv_Orders.Rows[i].Cells["Time"].Value = "";

          //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
          this.gdv_Orders.Rows[i].Cells["Meters Left"].Value = "";
        }
        else
        {
          string cellValue = "?";
          if (theSpeed_UVm_m > 0)
            //cellValue = now.ToLongTimeString();
            //cellValue = SyncroConverter.ConvertDateTimeToStrForDataGridView(now);
            cellValue = SyncroConverter.ConvertTimeToStrForDataGridView(now);
          this.gdv_Orders.Rows[i].Cells["Time"].Value = cellValue;

          //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
          this.gdv_Orders.Rows[i].Cells["Meters Left"].Value = metersToStart;

        }
        if (theSpeed_UVm_m > 0)
        {
          int secsDuration = (metersDuration * 60) / theSpeed_UVm_m;
          now = now.AddSeconds(secsDuration);
        }

        //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
        metersToStart += metersDuration;


      }
		}

    private void Frm_DryOrdersList_Load(object sender, EventArgs e)
		{
			if (gdv_Orders.Rows.Count > 0)
			{
				this.gdv_Orders.Focus();
				gdv_Orders.Rows[0].Selected = true;
			}
		}
	}
}

