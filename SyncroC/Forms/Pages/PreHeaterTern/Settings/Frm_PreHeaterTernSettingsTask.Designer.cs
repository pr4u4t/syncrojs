namespace SyncroC.Forms.Pages.PreHeaterTern.Settings
{
	partial class Frm_PreHeaterTernSettingsTask
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PreHeaterTernSettingsTask));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_Thermostack = new System.Windows.Forms.TabPage();
      this.CfgThermostackCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgThermostackGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_Thermostack.SuspendLayout();
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
      this.tab_Settings.Controls.Add(this.tab_Thermostack);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(32, 194);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1169, 519);
      this.tab_Settings.TabIndex = 121;
      // 
      // tab_Thermostack
      // 
      this.tab_Thermostack.Controls.Add(this.CfgThermostackCTRLGrid);
      this.tab_Thermostack.Controls.Add(this.CfgThermostackGrid);
      this.tab_Thermostack.Location = new System.Drawing.Point(4, 25);
      this.tab_Thermostack.Name = "tab_Thermostack";
      this.tab_Thermostack.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Thermostack.Size = new System.Drawing.Size(1161, 490);
      this.tab_Thermostack.TabIndex = 1;
      this.tab_Thermostack.Text = "Thermostack";
      this.tab_Thermostack.UseVisualStyleBackColor = true;
      // 
      // CfgThermostackCTRLGrid
      // 
      this.CfgThermostackCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgThermostackCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgThermostackCTRLGrid.Location = new System.Drawing.Point(580, 45);
      this.CfgThermostackCTRLGrid.Name = "CfgThermostackCTRLGrid";
      this.CfgThermostackCTRLGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgThermostackCTRLGrid.Size = new System.Drawing.Size(491, 399);
      this.CfgThermostackCTRLGrid.TabIndex = 121;
      this.CfgThermostackCTRLGrid.ToolbarVisible = false;
      this.CfgThermostackCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgThermostackCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgThermostackCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgThermostackGrid
      // 
      this.CfgThermostackGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgThermostackGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgThermostackGrid.Location = new System.Drawing.Point(90, 44);
      this.CfgThermostackGrid.Name = "CfgThermostackGrid";
      this.CfgThermostackGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgThermostackGrid.Size = new System.Drawing.Size(462, 399);
      this.CfgThermostackGrid.TabIndex = 119;
      this.CfgThermostackGrid.ToolbarVisible = false;
      this.CfgThermostackGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgThermostackGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgThermostackGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_PreHeaterTernSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_PreHeaterTernSettingsTask";
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
      this.tab_Thermostack.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tab_Settings;
		private System.Windows.Forms.TabPage tab_Thermostack;
		private System.Windows.Forms.PropertyGrid CfgThermostackGrid;
		private System.Windows.Forms.PropertyGrid CfgThermostackCTRLGrid;
	}
}
