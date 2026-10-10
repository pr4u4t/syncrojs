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

namespace SyncroC.Forms.Pages.Production.OrdersList
{
	public class Frm_WetOrdersList : SyncroC.Forms.Pages.Production.OrdersList.Frm_OrdersListSubGroup
	{
    private SyncroC.Controls.GridView gdv_Programs;
    private SyncroC.Controls.Title lbl_WetEndTitle;
		private System.ComponentModel.IContainer components = null;

    private DataSet order;

    //colonne che avrebbe dovuto fare il designer.
    private DataGridViewColumn clm_Prog_Pos;
    private DataGridViewColumn clm_Prog_ProgramCode;
    private DataGridViewColumn clm_Prog_PaperWidth;
    private DataGridViewColumn clm_Prog_ProgramLength;
		private DataGridViewColumn clm_Prog_SFPosition;
		private DataGridViewColumn clm_Prog_ProgramNote;
    private Label lbl_ModID;
    private Label lbl_ProgramID;
    private Label lbl_DryIDs;
    private DataGridViewColumn clm_Prog_ProgramQuality;
    //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
    private DataGridViewColumn clm_Prog_Time;
    private DataGridViewColumn clm_Prog_Meters_Left;

		public Frm_WetOrdersList()
		{
			InitializeComponent();
      
			order = DBManager.getDBManager().order.order;
      this.gdv_Programs.DataSource = order.Tables[Order.WETEND_LIST];

      //
      //colonne che avrebbe dovuto fare il designer.
      //

      //gdv_Programs 
      clm_Prog_Pos = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_Pos.Name = Order.WEL_POSITION;
      clm_Prog_Pos.HeaderText = "";
      clm_Prog_Pos.DataPropertyName = "Position";
			clm_Prog_Pos.Width = 50;

      clm_Prog_ProgramCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramCode.Name = Order.WEL_PROGRAM_CODE;
      clm_Prog_ProgramCode.HeaderText = "Code";
      clm_Prog_ProgramCode.DataPropertyName = "ProgramCode";
      clm_Prog_ProgramCode.Width = 200;

			clm_Prog_SFPosition = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_SFPosition.Name = Order.WEL_PROGRAM_SF_POSITION;
			clm_Prog_SFPosition.HeaderText = "Flute";
			clm_Prog_SFPosition.DataPropertyName = "SFPosition";
			clm_Prog_SFPosition.Width = 60;

      clm_Prog_PaperWidth = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_PaperWidth.Name = Order.WEL_PAPER_WIDTH;
      clm_Prog_PaperWidth.HeaderText = "Width";
      clm_Prog_PaperWidth.DataPropertyName = "PaperWidth";
      clm_Prog_PaperWidth.Width = 80;

      clm_Prog_ProgramLength = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramLength.Name = Order.WEL_PROGRAM_LENGTH;
      clm_Prog_ProgramLength.HeaderText = "Length";
      clm_Prog_ProgramLength.DataPropertyName = "ProgramLength";
      clm_Prog_ProgramLength.Width = 120;

      clm_Prog_ProgramQuality = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramQuality.Name = Order.WEL_PROGRAM_QUALITY;
      clm_Prog_ProgramQuality.HeaderText = "Quality";
      clm_Prog_ProgramQuality.DataPropertyName = "ProgramQuality";
      clm_Prog_ProgramQuality.Width = 250;
      //clm_Prog_ProgramQuality.Width = 354;

      //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
      clm_Prog_Time = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Prog_Time.Name = "Time";
      clm_Prog_Time.HeaderText = "Time";
      clm_Prog_Time.DataPropertyName = "Time";
      clm_Prog_Time.Width = 90;

      clm_Prog_Meters_Left = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Prog_Meters_Left.Name = "Meters Left";
      clm_Prog_Meters_Left.HeaderText = "Meters Left";
      clm_Prog_Meters_Left.DataPropertyName = "Meters Left";
      clm_Prog_Meters_Left.Width = 130;

			clm_Prog_ProgramNote = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramNote.Name = Order.WEL_PROGRAM_NOTE;
			clm_Prog_ProgramNote.HeaderText = "Note";
			clm_Prog_ProgramNote.DataPropertyName = "Note";
			clm_Prog_ProgramNote.Width = 600;

      gdv_Programs.Columns.Add(clm_Prog_Pos);
      gdv_Programs.Columns.Add(clm_Prog_ProgramCode);
			gdv_Programs.Columns.Add(clm_Prog_SFPosition);
      gdv_Programs.Columns.Add(clm_Prog_PaperWidth);
      gdv_Programs.Columns.Add(clm_Prog_ProgramLength);
      gdv_Programs.Columns.Add(clm_Prog_ProgramQuality);
      //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
      gdv_Programs.Columns.Add(clm_Prog_Time);
      gdv_Programs.Columns.Add(clm_Prog_Meters_Left);
      gdv_Programs.Columns.Add(clm_Prog_ProgramNote);


			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
			{
				this.lbl_CtrlF2_YY.Text = "";
				this.lbl_CtrlF3_YY.Text = "";
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
			base.Dispose( disposing );
		}

		protected override void F12()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_WetOrdersList));
      this.gdv_Programs = new SyncroC.Controls.GridView();
      this.lbl_WetEndTitle = new SyncroC.Controls.Title();
      this.lbl_ModID = new System.Windows.Forms.Label();
      this.lbl_ProgramID = new System.Windows.Forms.Label();
      this.lbl_DryIDs = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Program List";
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Move";
      // 
      // lbl_CtrlF3_YY
      // 
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // gdv_Programs
      // 
      this.gdv_Programs.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Programs.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.NoStandardDownTimeReasonColor;
      this.gdv_Programs.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs.AutoGenerateColumns = false;
      this.gdv_Programs.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Programs.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Programs.DataMember = "";
      this.gdv_Programs.DataSource = null;
      this.gdv_Programs.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Programs.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.ImagePassword = null;
      this.gdv_Programs.Location = new System.Drawing.Point(42, 221);
      this.gdv_Programs.MultiSelect = true;
      this.gdv_Programs.Name = "gdv_Programs";
      this.gdv_Programs.RowsHeight = 30;
      this.gdv_Programs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Programs.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.NoStandardDownTimeReasonColor;
      this.gdv_Programs.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs.Size = new System.Drawing.Size(1159, 491);
      this.gdv_Programs.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Programs.TabIndex = 132;
      this.gdv_Programs.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Programs.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Programs.TranslateText = true;
      this.gdv_Programs.DoubleClick += new System.EventHandler(this.gdv_Programs_DoubleClick);
      this.gdv_Programs.SelectionChanged += new System.EventHandler(this.gdv_Programs_SelectionChanged);
      this.gdv_Programs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Programs_CellFormatting);
      // 
      // lbl_WetEndTitle
      // 
      this.lbl_WetEndTitle.BackColor = System.Drawing.Color.Black;
      this.lbl_WetEndTitle.CausesValidation = false;
      this.lbl_WetEndTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lbl_WetEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_WetEndTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_WetEndTitle.Location = new System.Drawing.Point(30, 193);
      this.lbl_WetEndTitle.Name = "lbl_WetEndTitle";
      this.lbl_WetEndTitle.Size = new System.Drawing.Size(1171, 520);
      this.lbl_WetEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.lbl_WetEndTitle.TabIndex = 131;
      this.lbl_WetEndTitle.Text = "Program List";
      this.lbl_WetEndTitle.TranslateText = true;
      // 
      // lbl_ModID
      // 
      this.lbl_ModID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ModID.CausesValidation = false;
      this.lbl_ModID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ModID.Location = new System.Drawing.Point(292, 169);
      this.lbl_ModID.Name = "lbl_ModID";
      this.lbl_ModID.Size = new System.Drawing.Size(124, 18);
      this.lbl_ModID.TabIndex = 137;
      this.lbl_ModID.Text = "ModID";
      this.lbl_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ModID.Visible = false;
      // 
      // lbl_ProgramID
      // 
      this.lbl_ProgramID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ProgramID.CausesValidation = false;
      this.lbl_ProgramID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ProgramID.Location = new System.Drawing.Point(135, 169);
      this.lbl_ProgramID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.lbl_ProgramID.Name = "lbl_ProgramID";
      this.lbl_ProgramID.Size = new System.Drawing.Size(140, 18);
      this.lbl_ProgramID.TabIndex = 136;
      this.lbl_ProgramID.Text = "ProgramID";
      this.lbl_ProgramID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ProgramID.Visible = false;
      // 
      // lbl_DryIDs
      // 
      this.lbl_DryIDs.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DryIDs.CausesValidation = false;
      this.lbl_DryIDs.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DryIDs.Location = new System.Drawing.Point(434, 169);
      this.lbl_DryIDs.Name = "lbl_DryIDs";
      this.lbl_DryIDs.Size = new System.Drawing.Size(767, 18);
      this.lbl_DryIDs.TabIndex = 138;
      this.lbl_DryIDs.Text = "DryIDs";
      this.lbl_DryIDs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_DryIDs.Visible = false;
      // 
      // Frm_WetOrdersList
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_DryIDs);
      this.Controls.Add(this.lbl_ModID);
      this.Controls.Add(this.lbl_ProgramID);
      this.Controls.Add(this.gdv_Programs);
      this.Controls.Add(this.lbl_WetEndTitle);
      this.Name = "Frm_WetOrdersList";
      this.Text = "Frm_WetOrdersList";
      this.Load += new System.EventHandler(this.Frm_WetOrdersList_Load);
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
      this.Controls.SetChildIndex(this.lbl_WetEndTitle, 0);
      this.Controls.SetChildIndex(this.gdv_Programs, 0);
      this.Controls.SetChildIndex(this.lbl_ProgramID, 0);
      this.Controls.SetChildIndex(this.lbl_ModID, 0);
      this.Controls.SetChildIndex(this.lbl_DryIDs, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Show / Hide debug info.
		/// </summary>
		protected override void ControlD()
		{
			base.ControlD();
      lbl_ProgramID.Visible = lbl_ModID.Visible = lbl_DryIDs.Visible = !lbl_ProgramID.Visible;
		}

    private void gdv_Programs_SelectionChanged(object sender, EventArgs e)
    {
			if (gdv_Programs.SelectedRows.Count == 1)
			{
				DataRow dataRow = ((DataRowView)gdv_Programs.SelectedRows[0].DataBoundItem).Row;
				if (dataRow != null && dataRow.RowState != DataRowState.Detached)
				{
					//debug info
					lbl_ProgramID.Text = "ProgramID: " + dataRow[Order.WEL_PROGRAM_ID].ToString();
					lbl_ModID.Text = "ModID: " + dataRow[Order.WEL_PROGRAM_MODID].ToString();
          lbl_DryIDs.Text = "DryIDs: " + dataRow[Order.WEL_DRY_IDs].ToString();
				}
			}
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

			if (!SyncroC.Utility.Settings.getSettings().forms.CorrugatorDirectionRight)
			{
				if (this.gdv_Programs.Columns[e.ColumnIndex].Name == Order.WEL_PROGRAM_SF_POSITION && e.Value != null)
				{
					//Reverse the Flute position in the single facer
					char[] SFPosition = e.Value.ToString().ToCharArray();
					Array.Reverse(SFPosition);
					e.Value = new string(SFPosition);
				}
			}
		}

		private void Frm_WetOrdersList_Load(object sender, EventArgs e)
		{
			if (this.gdv_Programs.Rows.Count > 0)
			{
				this.gdv_Programs.Focus();
				this.gdv_Programs.Rows[0].Selected = true;
			}
		}

    protected override void CtrlF2()
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      if (gdv_Programs.Rows.Count <= 0)
        return;
      Frm_MoveOrders movePrograms = new Frm_MoveOrders();
      int first = 0;
      int last = 0;
      if (gdv_Programs.SelectedRows.Count > 0)
      {
        first = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[gdv_Programs.SelectedRows.Count - 1].Index + 1 : 1;
        last = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[0].Index + 1 : 1;
      }
      FosberDialogResult ret = movePrograms.ShowDialog(this,
          Math.Min(first, last), Math.Max(first, last));
      if (ret.Dialog == DialogResult.OK)
      {
        //agrassi_FI21-05-08_1
        DBManager dbm = DBManager.getDBManager();
        int firstProgramToMovePos = (int)ret.Data[0];
        int lastProgramToMovePos = (int)ret.Data[1];
        int destinationPos = (int)ret.Data[2];
        if ((firstProgramToMovePos != -1) && (lastProgramToMovePos != -1) && (destinationPos != -1) &&//no conversion errors occured
          (firstProgramToMovePos >= 2) && (firstProgramToMovePos <= gdv_Programs.Rows.Count) &&				//firstOrderToMovePos is consistent
          (lastProgramToMovePos >= 1) && (lastProgramToMovePos <= gdv_Programs.Rows.Count) &&					//lastOrderToMovePos is consistent
          (firstProgramToMovePos <= lastProgramToMovePos) &&																				//firstOrderToMovePos and lastOrderToMovePos are compatible		 
          (destinationPos >= 2) && (destinationPos <= gdv_Programs.Rows.Count + 1) &&									//destinationPos is consistent
          ((destinationPos > lastProgramToMovePos+1) || (destinationPos < firstProgramToMovePos)))	//destinationPos, firstOrderToMovePos, lastOrderToMovePos are comatble
        {
          int destinationDryPos = -1;
          //agrassi_FI21-05-08_1
          string[] dryIDs = null;
          if (destinationPos <= dbm.order.order.Tables[Order.WETEND_LIST].Rows.Count)
          {
            dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[destinationPos - 1][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
            if (dryIDs.Length > 0)
            {
              DataRow dryOrd = dbm.order.order.Tables[Order.DRYEND_LIST].Rows.Find(dryIDs[0]);
              destinationDryPos = this.intParse(dryOrd[Order.DEL_POSITION].ToString());
            }
          }
          else
            destinationDryPos = dbm.order.order.Tables[Order.DRYEND_LIST].Rows.Count + 1;
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          StringBuilder command = new StringBuilder(Command.DB_ORDER + "." + Command.DB_CMD_MOVE + "(" +
              destinationDryPos.ToString() + Command.PARAMETER_SEPARATOR);
          for (int i = firstProgramToMovePos - 1; i <= lastProgramToMovePos - 1; i++)
          {
            dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[i][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
            for (int j = 0; j < dryIDs.Length; j++)
            {
              command.Append(dryIDs[j]);
              if ((i != lastProgramToMovePos - 1) || (j != dryIDs.Length - 1))
                command.Append(Command.PARAMETER_SEPARATOR);
            }
          }
          command.Append(")");
          cm.SendTcp(cm.SYNCROS_DB_MANAGER, command.ToString());
        }
        //agrassi_FI21-05-08_1
        else
        {
          Frm_Alert frm_alert = new Frm_Alert();
          frm_alert.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_10"));
        }
      }
    }

    protected override void CtrlF3()
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      if (gdv_Programs.Rows.Count <= 0)
        return;
      DBManager dbm = DBManager.getDBManager();
      StringBuilder command = new StringBuilder(Command.DB_ORDER + "." + Command.DB_CMD_DEL + "(");
      bool send = false;

      if (this.gdv_Programs.IsConsecutiveSelection())
      {
        Frm_MultipleDelete popUp = new Frm_MultipleDelete();
        int first = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[gdv_Programs.SelectedRows.Count - 1].Index + 1 : 1;
        int last = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[0].Index + 1 : 1;
        FosberDialogResult ret = popUp.ShowDialog(this, Math.Min(first, last), Math.Max(first, last));
        if (ret.Dialog == DialogResult.OK)
        {
          int firstProgramToDeletePos = (int)ret.Data[0];
          int lastProgramToDeletePos = (int)ret.Data[1];
          if ((firstProgramToDeletePos != -1) && (lastProgramToDeletePos != -1) &&										//no conversion errors occured
            (firstProgramToDeletePos >= 2) && (firstProgramToDeletePos <= gdv_Programs.Rows.Count) &&	//firstOrderToDeletePos is consistent
            (lastProgramToDeletePos >= 1) && (lastProgramToDeletePos <= gdv_Programs.Rows.Count) &&     //lastOrderToDeletePos is consistent
            (firstProgramToDeletePos <= lastProgramToDeletePos))																			//firstOrderToDeletePos and lastOrderToDeletePos are compatible		 
          {
            for (int i = firstProgramToDeletePos - 1; i <= lastProgramToDeletePos - 1; i++)
            {
              string[] dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[i][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
              for (int j = 0; j < dryIDs.Length; j++)
              {
                command.Append(dryIDs[j]);
                if ((i != lastProgramToDeletePos - 1) || (j != dryIDs.Length - 1))
                  command.Append(Command.PARAMETER_SEPARATOR);
                send = true;
              }
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
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_20")+"\n";
        DataRowCollection rows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
        for (int i = gdv_Programs.SelectedRows.Count - 1; i >= 0; i--)
        {
          text += (gdv_Programs.SelectedRows[i].Index + 1).ToString() + (i > 0 ? ", " : "");
          string[] dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[gdv_Programs.SelectedRows[i].Index][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
          for (int j = 0; j < dryIDs.Length; j++)
          {
            command.Append(dryIDs[j]);
            if ((i != 0) || (j != dryIDs.Length - 1))
              command.Append(Command.PARAMETER_SEPARATOR);
          }
        }
        send = (popUp.ShowDialog(this, text) == DialogResult.OK);
      }
      command.Append(")");
      if (send)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, command.ToString());
      }
    }

		private void gdv_Programs_DoubleClick(object sender, EventArgs e)
		{
			if (gdv_Programs.SelectedRows.Count == 1 &&
					order.Tables[Order.WETEND_LIST].Rows.Count >= gdv_Programs.SelectedRows[0].Index)
			{
				Frm_OrdersInsSubGroup.indexOrderToShowWET = gdv_Programs.SelectedRows[0].Index;
			}
			askForPage(typeof(Frm_InsWetOrder));
		}

    //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
    private void _SetProgramStartTime(string currentSystemTime, int theSpeed_UVm_m, int metersLeftCurrent)
    {
      if (this.gdv_Programs.Rows.Count <= 1)
        return;
      DateTime now = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(currentSystemTime);
      now = now.ToLocalTime();
      int metersToStart = 0;
      for (int i = 0; i < this.gdv_Programs.Rows.Count; i++)
      {
        DataRow setup = ((DataRowView)this.gdv_Programs.Rows[i].DataBoundItem).Row;
        int metersDuration = this.intParse(setup[Order.WEL_PROGRAM_LENGTH].ToString());

        if (i == 0)
        {
          metersDuration = metersLeftCurrent;
          this.gdv_Programs.Rows[i].Cells["Time"].Value = "";
          this.gdv_Programs.Rows[i].Cells["Meters Left"].Value = "";
        }
        else
        {
          string cellValue = "?";
          if (theSpeed_UVm_m > 0)
            cellValue = SyncroConverter.ConvertTimeToStrForDataGridView(now);
          this.gdv_Programs.Rows[i].Cells["Time"].Value = cellValue;
          this.gdv_Programs.Rows[i].Cells["Meters Left"].Value = metersToStart;

        }
        if (theSpeed_UVm_m > 0)
        {
          int secsDuration = (metersDuration * 60) / theSpeed_UVm_m;
          now = now.AddSeconds(secsDuration);
        }

        metersToStart += metersDuration;


      }
    }

  }
}

