namespace SyncroC.Forms.Pages.GlueUnit.Settings
{
	partial class Frm_GlueUnitSettingsTask
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_GlueUnitSettingsTask));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_Crest = new System.Windows.Forms.TabPage();
      this.CfgCrestCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgCrestGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Kohler = new System.Windows.Forms.TabPage();
      this.CfgKohlerCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgKohlerGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_Crest.SuspendLayout();
      this.tab_Kohler.SuspendLayout();
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
      this.tab_Settings.Controls.Add(this.tab_Crest);
      this.tab_Settings.Controls.Add(this.tab_Kohler);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(30, 193);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 519);
      this.tab_Settings.TabIndex = 122;
      // 
      // tab_Crest
      // 
      this.tab_Crest.Controls.Add(this.CfgCrestCTRLGrid);
      this.tab_Crest.Controls.Add(this.CfgCrestGrid);
      this.tab_Crest.Location = new System.Drawing.Point(4, 27);
      this.tab_Crest.Name = "tab_Crest";
      this.tab_Crest.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Crest.Size = new System.Drawing.Size(1163, 488);
      this.tab_Crest.TabIndex = 1;
      this.tab_Crest.Text = "Glue Unit";
      this.tab_Crest.UseVisualStyleBackColor = true;
      // 
      // CfgCrestCTRLGrid
      // 
      this.CfgCrestCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgCrestCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgCrestCTRLGrid.Location = new System.Drawing.Point(582, 42);
      this.CfgCrestCTRLGrid.Name = "CfgCrestCTRLGrid";
      this.CfgCrestCTRLGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgCrestCTRLGrid.Size = new System.Drawing.Size(491, 399);
      this.CfgCrestCTRLGrid.TabIndex = 121;
      this.CfgCrestCTRLGrid.ToolbarVisible = false;
      this.CfgCrestCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgCrestCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgCrestCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgCrestGrid
      // 
      this.CfgCrestGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgCrestGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgCrestGrid.Location = new System.Drawing.Point(89, 42);
      this.CfgCrestGrid.Name = "CfgCrestGrid";
      this.CfgCrestGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgCrestGrid.Size = new System.Drawing.Size(475, 399);
      this.CfgCrestGrid.TabIndex = 119;
      this.CfgCrestGrid.ToolbarVisible = false;
      this.CfgCrestGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgCrestGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgCrestGrid.Click += new System.EventHandler(this.CfgCrestGrid_Click);
      this.CfgCrestGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Kohler
      // 
      this.tab_Kohler.Controls.Add(this.CfgKohlerCTRLGrid);
      this.tab_Kohler.Controls.Add(this.CfgKohlerGrid);
      this.tab_Kohler.Location = new System.Drawing.Point(4, 27);
      this.tab_Kohler.Name = "tab_Kohler";
      this.tab_Kohler.Size = new System.Drawing.Size(1163, 488);
      this.tab_Kohler.TabIndex = 2;
      this.tab_Kohler.Text = "GU Kohler";
      this.tab_Kohler.UseVisualStyleBackColor = true;
      // 
      // CfgKohlerCTRLGrid
      // 
      this.CfgKohlerCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgKohlerCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgKohlerCTRLGrid.Location = new System.Drawing.Point(582, 45);
      this.CfgKohlerCTRLGrid.Name = "CfgKohlerCTRLGrid";
      this.CfgKohlerCTRLGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgKohlerCTRLGrid.Size = new System.Drawing.Size(491, 399);
      this.CfgKohlerCTRLGrid.TabIndex = 123;
      this.CfgKohlerCTRLGrid.ToolbarVisible = false;
      this.CfgKohlerCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgKohlerCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgKohlerCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgKohlerGrid
      // 
      this.CfgKohlerGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgKohlerGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgKohlerGrid.Location = new System.Drawing.Point(89, 45);
      this.CfgKohlerGrid.Name = "CfgKohlerGrid";
      this.CfgKohlerGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgKohlerGrid.Size = new System.Drawing.Size(475, 399);
      this.CfgKohlerGrid.TabIndex = 122;
      this.CfgKohlerGrid.ToolbarVisible = false;
      this.CfgKohlerGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgKohlerGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgKohlerGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_GlueUnitSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_GlueUnitSettingsTask";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.tab_Settings, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_Settings.ResumeLayout(false);
      this.tab_Crest.ResumeLayout(false);
      this.tab_Kohler.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tab_Settings;
		private System.Windows.Forms.TabPage tab_Crest;
		private System.Windows.Forms.PropertyGrid CfgCrestGrid;
		private System.Windows.Forms.PropertyGrid CfgCrestCTRLGrid;
    private System.Windows.Forms.TabPage tab_Kohler;
    private System.Windows.Forms.PropertyGrid CfgKohlerCTRLGrid;
    private System.Windows.Forms.PropertyGrid CfgKohlerGrid;
	}
}
