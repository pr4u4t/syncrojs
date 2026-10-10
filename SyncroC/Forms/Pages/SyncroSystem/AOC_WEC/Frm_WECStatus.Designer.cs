namespace SyncroC.Forms.Pages.SyncroSystem.AOC_WEC
{
	partial class Frm_WECStatus
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_WECStatus));
      this.grp_WECControl = new System.Windows.Forms.GroupBox();
      this.stb_WECMode = new SyncroC.Controls.StateButton();
      this.tlb_SyncModeStr = new SyncroC.Controls.TitledLabel();
      this.tlb_WECModeStr = new SyncroC.Controls.TitledLabel();
      this.grp_WECMonitor = new System.Windows.Forms.GroupBox();
      this.ttl_LengthRemaining = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_LengthTarget = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_LengthProduced = new SyncroC.Controls.TitledNumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_WECControl.SuspendLayout();
      this.grp_WECMonitor.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabD
      // 
      this.tab_TabD.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // grp_WECControl
      // 
      this.grp_WECControl.Controls.Add(this.stb_WECMode);
      this.grp_WECControl.Controls.Add(this.tlb_SyncModeStr);
      this.grp_WECControl.Controls.Add(this.tlb_WECModeStr);
      this.grp_WECControl.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_WECControl.Location = new System.Drawing.Point(30, 182);
      this.grp_WECControl.Name = "grp_WECControl";
      this.grp_WECControl.Size = new System.Drawing.Size(422, 256);
      this.grp_WECControl.TabIndex = 121;
      this.grp_WECControl.TabStop = false;
      this.grp_WECControl.Text = "Control";
      // 
      // stb_WECMode
      // 
      this.stb_WECMode.Active = false;
      this.stb_WECMode.Alarmed = false;
      this.stb_WECMode.Auto = false;
      this.stb_WECMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_WECMode.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.stb_WECMode.FillBackColorWithImage = false;
      this.stb_WECMode.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_WECMode.FramePresent = false;
      this.stb_WECMode.HighLightEnable = false;
      this.stb_WECMode.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_WECMode.ImagePassword = null;
      this.stb_WECMode.Inemergency = false;
      this.stb_WECMode.InLocal = false;
      this.stb_WECMode.IsButton = false;
      this.stb_WECMode.IsForceToInvisible = false;
      this.stb_WECMode.Location = new System.Drawing.Point(338, 28);
      this.stb_WECMode.Name = "stb_WECMode";
      this.stb_WECMode.OnLine = false;
      this.stb_WECMode.ShowImage = true;
      this.stb_WECMode.ShowText = false;
      this.stb_WECMode.Size = new System.Drawing.Size(65, 65);
      this.stb_WECMode.Switching = false;
      this.stb_WECMode.TabIndex = 383;
      this.stb_WECMode.Text = "stateButton2";
      this.stb_WECMode.TranslateText = false;
      this.stb_WECMode.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_WECMode.UserDefinedActiveImage")));
      this.stb_WECMode.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_WECMode.UserDefinedImage")));
      this.stb_WECMode.Warned = false;
      // 
      // tlb_SyncModeStr
      // 
      this.tlb_SyncModeStr.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_SyncModeStr.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SyncModeStr.ImagePassword = null;
      this.tlb_SyncModeStr.Location = new System.Drawing.Point(16, 112);
      this.tlb_SyncModeStr.Name = "tlb_SyncModeStr";
      this.tlb_SyncModeStr.Size = new System.Drawing.Size(387, 65);
      this.tlb_SyncModeStr.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_SyncModeStr.TabIndex = 3;
      this.tlb_SyncModeStr.Text = "Syncronization Mode";
      this.tlb_SyncModeStr.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SyncModeStr.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SyncModeStr.TranslateText = false;
      this.tlb_SyncModeStr.Value = "";
      this.tlb_SyncModeStr.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SyncModeStr.ValueMultiline = true;
      // 
      // tlb_WECModeStr
      // 
      this.tlb_WECModeStr.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_WECModeStr.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WECModeStr.ImagePassword = null;
      this.tlb_WECModeStr.Location = new System.Drawing.Point(16, 28);
      this.tlb_WECModeStr.Name = "tlb_WECModeStr";
      this.tlb_WECModeStr.Size = new System.Drawing.Size(306, 65);
      this.tlb_WECModeStr.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_WECModeStr.TabIndex = 2;
      this.tlb_WECModeStr.Text = "WEC Mode";
      this.tlb_WECModeStr.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WECModeStr.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WECModeStr.TranslateText = false;
      this.tlb_WECModeStr.Value = "";
      this.tlb_WECModeStr.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WECModeStr.ValueMultiline = true;
      // 
      // grp_WECMonitor
      // 
      this.grp_WECMonitor.Controls.Add(this.ttl_LengthRemaining);
      this.grp_WECMonitor.Controls.Add(this.ttl_LengthTarget);
      this.grp_WECMonitor.Controls.Add(this.ttl_LengthProduced);
      this.grp_WECMonitor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_WECMonitor.Location = new System.Drawing.Point(779, 182);
      this.grp_WECMonitor.Name = "grp_WECMonitor";
      this.grp_WECMonitor.Size = new System.Drawing.Size(422, 256);
      this.grp_WECMonitor.TabIndex = 120;
      this.grp_WECMonitor.TabStop = false;
      this.grp_WECMonitor.Text = "Monitor";
      // 
      // ttl_LengthRemaining
      // 
      this.ttl_LengthRemaining.DecimalPlaces = 0;
      this.ttl_LengthRemaining.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_LengthRemaining.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LengthRemaining.ImagePassword = null;
      this.ttl_LengthRemaining.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_LengthRemaining.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_LengthRemaining.Location = new System.Drawing.Point(6, 112);
      this.ttl_LengthRemaining.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_LengthRemaining.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_LengthRemaining.Name = "ttl_LengthRemaining";
      this.ttl_LengthRemaining.ReadOnly = true;
      this.ttl_LengthRemaining.Size = new System.Drawing.Size(197, 64);
      this.ttl_LengthRemaining.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_LengthRemaining.TabIndex = 3;
      this.ttl_LengthRemaining.Text = "Length Reamining";
      this.ttl_LengthRemaining.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LengthRemaining.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LengthRemaining.TranslateText = false;
      this.ttl_LengthRemaining.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_LengthRemaining.Value = "0";
      this.ttl_LengthRemaining.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LengthRemaining.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_LengthTarget
      // 
      this.ttl_LengthTarget.DecimalPlaces = 0;
      this.ttl_LengthTarget.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_LengthTarget.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LengthTarget.ImagePassword = null;
      this.ttl_LengthTarget.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_LengthTarget.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_LengthTarget.Location = new System.Drawing.Point(219, 28);
      this.ttl_LengthTarget.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_LengthTarget.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_LengthTarget.Name = "ttl_LengthTarget";
      this.ttl_LengthTarget.ReadOnly = true;
      this.ttl_LengthTarget.Size = new System.Drawing.Size(197, 64);
      this.ttl_LengthTarget.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_LengthTarget.TabIndex = 2;
      this.ttl_LengthTarget.Text = "Length Target";
      this.ttl_LengthTarget.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LengthTarget.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LengthTarget.TranslateText = false;
      this.ttl_LengthTarget.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_LengthTarget.Value = "0";
      this.ttl_LengthTarget.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LengthTarget.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_LengthProduced
      // 
      this.ttl_LengthProduced.DecimalPlaces = 0;
      this.ttl_LengthProduced.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_LengthProduced.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LengthProduced.ImagePassword = null;
      this.ttl_LengthProduced.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_LengthProduced.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_LengthProduced.Location = new System.Drawing.Point(6, 28);
      this.ttl_LengthProduced.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_LengthProduced.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_LengthProduced.Name = "ttl_LengthProduced";
      this.ttl_LengthProduced.ReadOnly = true;
      this.ttl_LengthProduced.Size = new System.Drawing.Size(197, 64);
      this.ttl_LengthProduced.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_LengthProduced.TabIndex = 1;
      this.ttl_LengthProduced.Text = "Length Produced";
      this.ttl_LengthProduced.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LengthProduced.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LengthProduced.TranslateText = false;
      this.ttl_LengthProduced.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_LengthProduced.Value = "0";
      this.ttl_LengthProduced.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LengthProduced.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // Frm_WECStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.grp_WECControl);
      this.Controls.Add(this.grp_WECMonitor);
      this.Name = "Frm_WECStatus";
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
      this.Controls.SetChildIndex(this.grp_WECMonitor, 0);
      this.Controls.SetChildIndex(this.grp_WECControl, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_WECControl.ResumeLayout(false);
      this.grp_WECMonitor.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grp_WECControl;
		private SyncroC.Controls.TitledLabel tlb_WECModeStr;
		private System.Windows.Forms.GroupBox grp_WECMonitor;
		private SyncroC.Controls.TitledLabel tlb_SyncModeStr;
		private SyncroC.Controls.TitledNumericUpDown ttl_LengthRemaining;
		private SyncroC.Controls.TitledNumericUpDown ttl_LengthTarget;
		private SyncroC.Controls.TitledNumericUpDown ttl_LengthProduced;
		private SyncroC.Controls.StateButton stb_WECMode;
	}
}
