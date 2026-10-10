using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Debug
{
	public class Frm_DebugINI : SyncroC.Forms.Pages.Debug.Frm_DebugGroup
	{
		private PropertyGrid pyg_Network;
		private PropertyGrid pyg_Forms;
		private PropertyGrid pyg_Logger;
		private PropertyGrid pyg_Database;
		private PropertyGrid pyg_Languages;
		private PropertyGrid pyg_Configuration;
		private SyncroC.Controls.TitledLabel ttl_INIFullPath;
		private PropertyGrid pyg_Facility;
		private System.ComponentModel.IContainer components = null;

		public Frm_DebugINI()
		{
			InitializeComponent();

			SyncroC.Utility.Settings s = SyncroC.Utility.Settings.getSettings();

			ttl_INIFullPath.Value = s.getFileIniFullPath();

			this.pyg_Network.SelectedObject = s.network;
			this.pyg_Forms.SelectedObject = s.forms;
			this.pyg_Logger.SelectedObject = s.logger;
			this.pyg_Configuration.SelectedObject = s.configuration;
			this.pyg_Database.SelectedObject = s.dataBase;
			this.pyg_Languages.SelectedObject = s.languages;
			this.pyg_Facility.SelectedObject = s.facility;
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

		protected override void F2()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugINI));
      this.pyg_Network = new System.Windows.Forms.PropertyGrid();
      this.pyg_Forms = new System.Windows.Forms.PropertyGrid();
      this.pyg_Logger = new System.Windows.Forms.PropertyGrid();
      this.pyg_Database = new System.Windows.Forms.PropertyGrid();
      this.pyg_Languages = new System.Windows.Forms.PropertyGrid();
      this.pyg_Configuration = new System.Windows.Forms.PropertyGrid();
      this.ttl_INIFullPath = new SyncroC.Controls.TitledLabel();
      this.pyg_Facility = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Title_YN.Text = "SETTINGS FROM INI FILE";
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
      // tab_Tab2
      // 
      this.tab_Tab2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
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
      // pyg_Network
      // 
      this.pyg_Network.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Network.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Network.Location = new System.Drawing.Point(679, 193);
      this.pyg_Network.Name = "pyg_Network";
      this.pyg_Network.Size = new System.Drawing.Size(250, 250);
      this.pyg_Network.TabIndex = 110;
      this.pyg_Network.ToolbarVisible = false;
      this.pyg_Network.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // pyg_Forms
      // 
      this.pyg_Forms.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Forms.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Forms.Location = new System.Drawing.Point(951, 193);
      this.pyg_Forms.Name = "pyg_Forms";
      this.pyg_Forms.Size = new System.Drawing.Size(250, 250);
      this.pyg_Forms.TabIndex = 111;
      this.pyg_Forms.ToolbarVisible = false;
      this.pyg_Forms.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // pyg_Logger
      // 
      this.pyg_Logger.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Logger.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Logger.Location = new System.Drawing.Point(134, 462);
      this.pyg_Logger.Name = "pyg_Logger";
      this.pyg_Logger.Size = new System.Drawing.Size(250, 250);
      this.pyg_Logger.TabIndex = 112;
      this.pyg_Logger.ToolbarVisible = false;
      this.pyg_Logger.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // pyg_Database
      // 
      this.pyg_Database.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Database.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Database.Location = new System.Drawing.Point(407, 462);
      this.pyg_Database.Name = "pyg_Database";
      this.pyg_Database.Size = new System.Drawing.Size(250, 250);
      this.pyg_Database.TabIndex = 113;
      this.pyg_Database.ToolbarVisible = false;
      this.pyg_Database.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // pyg_Languages
      // 
      this.pyg_Languages.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Languages.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Languages.Location = new System.Drawing.Point(679, 462);
      this.pyg_Languages.Name = "pyg_Languages";
      this.pyg_Languages.Size = new System.Drawing.Size(250, 250);
      this.pyg_Languages.TabIndex = 114;
      this.pyg_Languages.ToolbarVisible = false;
      this.pyg_Languages.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // pyg_Configuration
      // 
      this.pyg_Configuration.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Configuration.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Configuration.Location = new System.Drawing.Point(951, 462);
      this.pyg_Configuration.Name = "pyg_Configuration";
      this.pyg_Configuration.Size = new System.Drawing.Size(250, 250);
      this.pyg_Configuration.TabIndex = 115;
      this.pyg_Configuration.ToolbarVisible = false;
      this.pyg_Configuration.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // ttl_INIFullPath
      // 
      this.ttl_INIFullPath.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_INIFullPath.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_INIFullPath.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_INIFullPath.Location = new System.Drawing.Point(134, 195);
      this.ttl_INIFullPath.Name = "ttl_INIFullPath";
      this.ttl_INIFullPath.Size = new System.Drawing.Size(268, 193);
      this.ttl_INIFullPath.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_INIFullPath.TabIndex = 116;
      this.ttl_INIFullPath.Text = "INI full path";
      this.ttl_INIFullPath.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_INIFullPath.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_INIFullPath.TranslateText = false;
      this.ttl_INIFullPath.Value = "";
      this.ttl_INIFullPath.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_INIFullPath.ValueMultiline = true;
      // 
      // pyg_Facility
      // 
      this.pyg_Facility.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Facility.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.pyg_Facility.Location = new System.Drawing.Point(408, 193);
      this.pyg_Facility.Name = "pyg_Facility";
      this.pyg_Facility.Size = new System.Drawing.Size(250, 250);
      this.pyg_Facility.TabIndex = 117;
      this.pyg_Facility.ToolbarVisible = false;
      this.pyg_Facility.ViewForeColor = System.Drawing.Color.DimGray;
      // 
      // Frm_DebugINI
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pyg_Facility);
      this.Controls.Add(this.ttl_INIFullPath);
      this.Controls.Add(this.pyg_Configuration);
      this.Controls.Add(this.pyg_Languages);
      this.Controls.Add(this.pyg_Database);
      this.Controls.Add(this.pyg_Logger);
      this.Controls.Add(this.pyg_Forms);
      this.Controls.Add(this.pyg_Network);
      this.DesignGridHorizontalLines = 1;
      this.DesignGridVerticalLines = 3;
      this.Name = "Frm_DebugINI";
      this.Text = "Frm_DebugINI";
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
      this.Controls.SetChildIndex(this.pyg_Network, 0);
      this.Controls.SetChildIndex(this.pyg_Forms, 0);
      this.Controls.SetChildIndex(this.pyg_Logger, 0);
      this.Controls.SetChildIndex(this.pyg_Database, 0);
      this.Controls.SetChildIndex(this.pyg_Languages, 0);
      this.Controls.SetChildIndex(this.pyg_Configuration, 0);
      this.Controls.SetChildIndex(this.ttl_INIFullPath, 0);
      this.Controls.SetChildIndex(this.pyg_Facility, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

	}
}

