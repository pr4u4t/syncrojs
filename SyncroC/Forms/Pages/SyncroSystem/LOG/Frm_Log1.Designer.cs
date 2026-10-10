namespace SyncroC.Forms.Pages.SyncroSystem.LOG
{
	partial class Frm_Log1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Log1));
      this.tmr_Configuration = new System.Windows.Forms.Timer(this.components);
      this.tabControlConfiguration1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.txb_ConfigurationCurrent = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.txb_ConfigurationOld = new System.Windows.Forms.TextBox();
      this.ttl_Configuration = new SyncroC.Controls.Title();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tabControlConfiguration1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "CALIBRATION USER LOG";
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // tmr_Configuration
      // 
      this.tmr_Configuration.Enabled = true;
      this.tmr_Configuration.Interval = 1000;
      this.tmr_Configuration.Tick += new System.EventHandler(this.tmr_Configuration_Tick);
      // 
      // tabControlConfiguration1
      // 
      this.tabControlConfiguration1.Controls.Add(this.tabPage1);
      this.tabControlConfiguration1.Controls.Add(this.tabPage2);
      this.tabControlConfiguration1.Location = new System.Drawing.Point(42, 216);
      this.tabControlConfiguration1.Name = "tabControlConfiguration1";
      this.tabControlConfiguration1.SelectedIndex = 0;
      this.tabControlConfiguration1.Size = new System.Drawing.Size(1158, 495);
      this.tabControlConfiguration1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabControlConfiguration1.TabIndex = 120;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.txb_ConfigurationCurrent);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1150, 469);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "recent";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // txb_ConfigurationCurrent
      // 
      this.txb_ConfigurationCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txb_ConfigurationCurrent.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txb_ConfigurationCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txb_ConfigurationCurrent.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txb_ConfigurationCurrent.Location = new System.Drawing.Point(3, 3);
      this.txb_ConfigurationCurrent.Multiline = true;
      this.txb_ConfigurationCurrent.Name = "txb_ConfigurationCurrent";
      this.txb_ConfigurationCurrent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txb_ConfigurationCurrent.Size = new System.Drawing.Size(1144, 463);
      this.txb_ConfigurationCurrent.TabIndex = 111;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.txb_ConfigurationOld);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1029, 469);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "old";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // txb_ConfigurationOld
      // 
      this.txb_ConfigurationOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txb_ConfigurationOld.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txb_ConfigurationOld.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txb_ConfigurationOld.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txb_ConfigurationOld.Location = new System.Drawing.Point(3, 3);
      this.txb_ConfigurationOld.Multiline = true;
      this.txb_ConfigurationOld.Name = "txb_ConfigurationOld";
      this.txb_ConfigurationOld.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txb_ConfigurationOld.Size = new System.Drawing.Size(1023, 463);
      this.txb_ConfigurationOld.TabIndex = 112;
      // 
      // ttl_Configuration
      // 
      this.ttl_Configuration.BackColor = System.Drawing.Color.Black;
      this.ttl_Configuration.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Configuration.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Configuration.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Configuration.Location = new System.Drawing.Point(30, 192);
      this.ttl_Configuration.Name = "ttl_Configuration";
      this.ttl_Configuration.Size = new System.Drawing.Size(1171, 519);
      this.ttl_Configuration.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Configuration.TabIndex = 119;
      this.ttl_Configuration.Text = "CONFIGURATION";
      this.ttl_Configuration.TranslateText = false;
      // 
      // Frm_Log1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tabControlConfiguration1);
      this.Controls.Add(this.ttl_Configuration);
      this.Name = "Frm_Log1";
      this.Text = "Frm_Log1";
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
      this.Controls.SetChildIndex(this.ttl_Configuration, 0);
      this.Controls.SetChildIndex(this.tabControlConfiguration1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tabControlConfiguration1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer tmr_Configuration;
		private System.Windows.Forms.TabControl tabControlConfiguration1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox txb_ConfigurationCurrent;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txb_ConfigurationOld;
		private SyncroC.Controls.Title ttl_Configuration;
	}
}