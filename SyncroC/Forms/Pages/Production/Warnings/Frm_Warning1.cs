using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.Controls;

namespace SyncroC.Forms.Pages.Production.Warnings
{
	public class Frm_Warning1 : SyncroC.Forms.Pages.Production.Warnings.Frm_WarningsSubGroup
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
		private bool showDebugOption = false;

    public Frm_Warning1()
		{
			InitializeComponent();

			SYC_SW_Alarms = RTCManager.getRTCManager().ActiveAlarms.Tables[RTCManager.SYC_SW];
			DataView dv = new DataView(SYC_SW_Alarms);
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
      //dv.RowFilter = RTCManager.PRESENT + " = 'true'";
      dv.RowFilter = RTCManager.PRESENT + " = 'true'" + " AND " + RTCManager.CODE_ZONE + " = 'Warning'"; 
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Warning1));
      this.gdv_alarm = new SyncroC.Controls.GridView();
      this.lbl_Present = new System.Windows.Forms.Label();
      this.lbl_codeZone = new System.Windows.Forms.Label();
      this.lbl_present_Description = new System.Windows.Forms.Label();
      this.lbl_codeZone_Description = new System.Windows.Forms.Label();
      this.ttl_SyncroActiveAlarms = new SyncroC.Controls.Title();
      this.lbl_Key = new System.Windows.Forms.Label();
      this.lbl_Key_Description = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.SelectedWarning;
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(0)))));
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
      this.gdv_alarm.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_alarm.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.AutoGenerateColumns = false;
      this.gdv_alarm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_alarm.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_alarm.DataMember = "";
      this.gdv_alarm.DataSource = null;
      this.gdv_alarm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_alarm.ForeColor = System.Drawing.Color.Black;
      this.gdv_alarm.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.ImagePassword = null;
      this.gdv_alarm.Location = new System.Drawing.Point(39, 218);
      this.gdv_alarm.MultiSelect = false;
      this.gdv_alarm.Name = "gdv_alarm";
      this.gdv_alarm.RowsHeight = 32;
      this.gdv_alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarm.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.Size = new System.Drawing.Size(1161, 493);
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
      this.ttl_SyncroActiveAlarms.Location = new System.Drawing.Point(29, 192);
      this.ttl_SyncroActiveAlarms.Name = "ttl_SyncroActiveAlarms";
      this.ttl_SyncroActiveAlarms.Size = new System.Drawing.Size(1171, 519);
      this.ttl_SyncroActiveAlarms.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_SyncroActiveAlarms.TabIndex = 116;
      this.ttl_SyncroActiveAlarms.Text = "Syncro warnings";
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
      // Frm_Warning1
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_Key);
      this.Controls.Add(this.lbl_Key_Description);
      this.Controls.Add(this.lbl_Present);
      this.Controls.Add(this.lbl_codeZone);
      this.Controls.Add(this.lbl_present_Description);
      this.Controls.Add(this.lbl_codeZone_Description);
      this.Controls.Add(this.ttl_SyncroActiveAlarms);
      this.Controls.Add(this.gdv_alarm);
      this.Name = "Frm_Warning1";
      this.Text = "Frm_Warning1";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.gdv_alarm, 0);
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
      this.Controls.SetChildIndex(this.ttl_SyncroActiveAlarms, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone_Description, 0);
      this.Controls.SetChildIndex(this.lbl_present_Description, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone, 0);
      this.Controls.SetChildIndex(this.lbl_Present, 0);
      this.Controls.SetChildIndex(this.lbl_Key_Description, 0);
      this.Controls.SetChildIndex(this.lbl_Key, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
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

