namespace SyncroC.Forms.Pages.SyncroSystem.LOG
{
	partial class Frm_Log3
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Log3));
      this.tabControlProcessControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.txb_ProcessControlCurrent = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.txb_ProcessControlOld = new System.Windows.Forms.TextBox();
      this.ttl_ProcessControl = new SyncroC.Controls.Title();
      this.tmr_ProcessControl = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tabControlProcessControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // tabControlProcessControl1
      // 
      this.tabControlProcessControl1.Controls.Add(this.tabPage1);
      this.tabControlProcessControl1.Controls.Add(this.tabPage2);
      this.tabControlProcessControl1.Location = new System.Drawing.Point(42, 218);
      this.tabControlProcessControl1.Name = "tabControlProcessControl1";
      this.tabControlProcessControl1.SelectedIndex = 0;
      this.tabControlProcessControl1.Size = new System.Drawing.Size(1158, 495);
      this.tabControlProcessControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabControlProcessControl1.TabIndex = 122;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.txb_ProcessControlCurrent);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1150, 469);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "recent";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // txb_ProcessControlCurrent
      // 
      this.txb_ProcessControlCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txb_ProcessControlCurrent.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txb_ProcessControlCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txb_ProcessControlCurrent.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txb_ProcessControlCurrent.Location = new System.Drawing.Point(3, 3);
      this.txb_ProcessControlCurrent.Multiline = true;
      this.txb_ProcessControlCurrent.Name = "txb_ProcessControlCurrent";
      this.txb_ProcessControlCurrent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txb_ProcessControlCurrent.Size = new System.Drawing.Size(1144, 463);
      this.txb_ProcessControlCurrent.TabIndex = 111;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.txb_ProcessControlOld);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1029, 469);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "old";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // txb_ProcessControlOld
      // 
      this.txb_ProcessControlOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txb_ProcessControlOld.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txb_ProcessControlOld.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txb_ProcessControlOld.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txb_ProcessControlOld.Location = new System.Drawing.Point(3, 3);
      this.txb_ProcessControlOld.Multiline = true;
      this.txb_ProcessControlOld.Name = "txb_ProcessControlOld";
      this.txb_ProcessControlOld.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txb_ProcessControlOld.Size = new System.Drawing.Size(1023, 463);
      this.txb_ProcessControlOld.TabIndex = 112;
      // 
      // ttl_ProcessControl
      // 
      this.ttl_ProcessControl.BackColor = System.Drawing.Color.Black;
      this.ttl_ProcessControl.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ProcessControl.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ProcessControl.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ProcessControl.Location = new System.Drawing.Point(30, 194);
      this.ttl_ProcessControl.Name = "ttl_ProcessControl";
      this.ttl_ProcessControl.Size = new System.Drawing.Size(1171, 519);
      this.ttl_ProcessControl.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ProcessControl.TabIndex = 121;
      this.ttl_ProcessControl.Text = "PROCESS CONTROL";
      this.ttl_ProcessControl.TranslateText = false;
      // 
      // tmr_ProcessControl
      // 
      this.tmr_ProcessControl.Enabled = true;
      this.tmr_ProcessControl.Interval = 1000;
      this.tmr_ProcessControl.Tick += new System.EventHandler(this.tmr_ProcessControl_Tick);
      // 
      // Frm_Log3
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tabControlProcessControl1);
      this.Controls.Add(this.ttl_ProcessControl);
      this.Name = "Frm_Log3";
      this.Text = "Frm_Log3";
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
      this.Controls.SetChildIndex(this.ttl_ProcessControl, 0);
      this.Controls.SetChildIndex(this.tabControlProcessControl1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tabControlProcessControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

    private System.Windows.Forms.TabControl tabControlProcessControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TextBox txb_ProcessControlCurrent;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TextBox txb_ProcessControlOld;
    private SyncroC.Controls.Title ttl_ProcessControl;
    private System.Windows.Forms.Timer tmr_ProcessControl;
	}
}