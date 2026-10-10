namespace SyncroC.Forms.Pages.Splicer.Settings
{
	partial class Frm_SplicerSettingsTask
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SplicerSettingsTask));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_SplicerGeneral = new System.Windows.Forms.TabPage();
      this.CfgSplicerCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgSplicerGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Link400 = new System.Windows.Forms.TabPage();
      this.CfgSplicerFosberLNK400CTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_RollStand400 = new System.Windows.Forms.TabPage();
      this.CfgRollStandFosberPRT400CTRLGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_SplicerGeneral.SuspendLayout();
      this.tab_Link400.SuspendLayout();
      this.tab_RollStand400.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // tab_Settings
      // 
      this.tab_Settings.Controls.Add(this.tab_SplicerGeneral);
      this.tab_Settings.Controls.Add(this.tab_Link400);
      this.tab_Settings.Controls.Add(this.tab_RollStand400);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(31, 194);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1170, 519);
      this.tab_Settings.TabIndex = 123;
      // 
      // tab_SplicerGeneral
      // 
      this.tab_SplicerGeneral.Controls.Add(this.CfgSplicerCTRLGrid);
      this.tab_SplicerGeneral.Controls.Add(this.CfgSplicerGrid);
      this.tab_SplicerGeneral.Location = new System.Drawing.Point(4, 27);
      this.tab_SplicerGeneral.Name = "tab_SplicerGeneral";
      this.tab_SplicerGeneral.Padding = new System.Windows.Forms.Padding(3);
      this.tab_SplicerGeneral.Size = new System.Drawing.Size(1162, 488);
      this.tab_SplicerGeneral.TabIndex = 1;
      this.tab_SplicerGeneral.Text = "Splicer Generic";
      this.tab_SplicerGeneral.UseVisualStyleBackColor = true;
      // 
      // CfgSplicerCTRLGrid
      // 
      this.CfgSplicerCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSplicerCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSplicerCTRLGrid.Location = new System.Drawing.Point(582, 42);
      this.CfgSplicerCTRLGrid.Name = "CfgSplicerCTRLGrid";
      this.CfgSplicerCTRLGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSplicerCTRLGrid.Size = new System.Drawing.Size(491, 399);
      this.CfgSplicerCTRLGrid.TabIndex = 121;
      this.CfgSplicerCTRLGrid.ToolbarVisible = false;
      this.CfgSplicerCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSplicerCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSplicerCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgSplicerGrid
      // 
      this.CfgSplicerGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSplicerGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSplicerGrid.Location = new System.Drawing.Point(89, 42);
      this.CfgSplicerGrid.Name = "CfgSplicerGrid";
      this.CfgSplicerGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSplicerGrid.Size = new System.Drawing.Size(475, 399);
      this.CfgSplicerGrid.TabIndex = 119;
      this.CfgSplicerGrid.ToolbarVisible = false;
      this.CfgSplicerGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSplicerGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSplicerGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Link400
      // 
      this.tab_Link400.Controls.Add(this.CfgSplicerFosberLNK400CTRLGrid);
      this.tab_Link400.Location = new System.Drawing.Point(4, 27);
      this.tab_Link400.Name = "tab_Link400";
      this.tab_Link400.Size = new System.Drawing.Size(1162, 488);
      this.tab_Link400.TabIndex = 2;
      this.tab_Link400.Text = "LNK400";
      this.tab_Link400.UseVisualStyleBackColor = true;
      // 
      // CfgSplicerFosberLNK400CTRLGrid
      // 
      this.CfgSplicerFosberLNK400CTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSplicerFosberLNK400CTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSplicerFosberLNK400CTRLGrid.Location = new System.Drawing.Point(336, 42);
      this.CfgSplicerFosberLNK400CTRLGrid.Name = "CfgSplicerFosberLNK400CTRLGrid";
      this.CfgSplicerFosberLNK400CTRLGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSplicerFosberLNK400CTRLGrid.Size = new System.Drawing.Size(491, 399);
      this.CfgSplicerFosberLNK400CTRLGrid.TabIndex = 122;
      this.CfgSplicerFosberLNK400CTRLGrid.ToolbarVisible = false;
      this.CfgSplicerFosberLNK400CTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSplicerFosberLNK400CTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSplicerFosberLNK400CTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_RollStand400
      // 
      this.tab_RollStand400.Controls.Add(this.CfgRollStandFosberPRT400CTRLGrid);
      this.tab_RollStand400.Location = new System.Drawing.Point(4, 27);
      this.tab_RollStand400.Name = "tab_RollStand400";
      this.tab_RollStand400.Size = new System.Drawing.Size(1162, 488);
      this.tab_RollStand400.TabIndex = 3;
      this.tab_RollStand400.Text = "PRT400";
      this.tab_RollStand400.UseVisualStyleBackColor = true;
      // 
      // CfgRollStandFosberPRT400CTRLGrid
      // 
      this.CfgRollStandFosberPRT400CTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRollStandFosberPRT400CTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRollStandFosberPRT400CTRLGrid.Location = new System.Drawing.Point(336, 45);
      this.CfgRollStandFosberPRT400CTRLGrid.Name = "CfgRollStandFosberPRT400CTRLGrid";
      this.CfgRollStandFosberPRT400CTRLGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgRollStandFosberPRT400CTRLGrid.Size = new System.Drawing.Size(491, 399);
      this.CfgRollStandFosberPRT400CTRLGrid.TabIndex = 123;
      this.CfgRollStandFosberPRT400CTRLGrid.ToolbarVisible = false;
      this.CfgRollStandFosberPRT400CTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgRollStandFosberPRT400CTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgRollStandFosberPRT400CTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_SplicerSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_SplicerSettingsTask";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tab_Settings, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
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
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_Settings.ResumeLayout(false);
      this.tab_SplicerGeneral.ResumeLayout(false);
      this.tab_Link400.ResumeLayout(false);
      this.tab_RollStand400.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tab_Settings;
		private System.Windows.Forms.TabPage tab_SplicerGeneral;
		private System.Windows.Forms.PropertyGrid CfgSplicerCTRLGrid;
		private System.Windows.Forms.PropertyGrid CfgSplicerGrid;
		private System.Windows.Forms.TabPage tab_Link400;
		private System.Windows.Forms.PropertyGrid CfgSplicerFosberLNK400CTRLGrid;
    private System.Windows.Forms.TabPage tab_RollStand400;
    private System.Windows.Forms.PropertyGrid CfgRollStandFosberPRT400CTRLGrid;
	}
}
