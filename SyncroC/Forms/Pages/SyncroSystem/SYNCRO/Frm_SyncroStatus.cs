using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.SYNCRO
{
	public class Frm_SyncroStatus : SyncroC.Forms.Pages.SyncroSystem.SYNCRO.Frm_SyncroSubGroup
	{
		private TabPage tab_DownTimeStatus;
		private CheckBox chk_WaitingRestartDelay;
		private CheckBox chk_MicroStopElapsed;
		private CheckBox chk_DownTimeProgress;
		private SyncroC.Controls.TitledMaskTextBox tmt_LineStopped;
		private Label lbl_WaitRestartDelay;
		private Label lbl_MicroStopElapsed;
		private Label lbl_LineStarted;
		private Label lbl_lineStopped;
		private Label lbl_DownTimeProgress;
		private SyncroC.Controls.TitledMaskTextBox tmt_LineStarted;
		private TabControl tab_SyncroStatus;
		private System.ComponentModel.IContainer components = null;

		public Frm_SyncroStatus()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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
			base.Dispose(disposing);
		}

		protected override void F11()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SyncroStatus));
      this.tab_DownTimeStatus = new System.Windows.Forms.TabPage();
      this.chk_WaitingRestartDelay = new System.Windows.Forms.CheckBox();
      this.chk_MicroStopElapsed = new System.Windows.Forms.CheckBox();
      this.chk_DownTimeProgress = new System.Windows.Forms.CheckBox();
      this.tmt_LineStopped = new SyncroC.Controls.TitledMaskTextBox();
      this.lbl_WaitRestartDelay = new System.Windows.Forms.Label();
      this.lbl_MicroStopElapsed = new System.Windows.Forms.Label();
      this.lbl_LineStarted = new System.Windows.Forms.Label();
      this.lbl_lineStopped = new System.Windows.Forms.Label();
      this.lbl_DownTimeProgress = new System.Windows.Forms.Label();
      this.tmt_LineStarted = new SyncroC.Controls.TitledMaskTextBox();
      this.tab_SyncroStatus = new System.Windows.Forms.TabControl();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_DownTimeStatus.SuspendLayout();
      this.tab_SyncroStatus.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // tab_DownTimeStatus
      // 
      this.tab_DownTimeStatus.Controls.Add(this.chk_WaitingRestartDelay);
      this.tab_DownTimeStatus.Controls.Add(this.chk_MicroStopElapsed);
      this.tab_DownTimeStatus.Controls.Add(this.chk_DownTimeProgress);
      this.tab_DownTimeStatus.Controls.Add(this.tmt_LineStopped);
      this.tab_DownTimeStatus.Controls.Add(this.lbl_WaitRestartDelay);
      this.tab_DownTimeStatus.Controls.Add(this.lbl_MicroStopElapsed);
      this.tab_DownTimeStatus.Controls.Add(this.lbl_LineStarted);
      this.tab_DownTimeStatus.Controls.Add(this.lbl_lineStopped);
      this.tab_DownTimeStatus.Controls.Add(this.lbl_DownTimeProgress);
      this.tab_DownTimeStatus.Controls.Add(this.tmt_LineStarted);
      this.tab_DownTimeStatus.Location = new System.Drawing.Point(4, 22);
      this.tab_DownTimeStatus.Name = "tab_DownTimeStatus";
      this.tab_DownTimeStatus.Padding = new System.Windows.Forms.Padding(3);
      this.tab_DownTimeStatus.Size = new System.Drawing.Size(1163, 493);
      this.tab_DownTimeStatus.TabIndex = 0;
      this.tab_DownTimeStatus.Text = "DownTime Status";
      this.tab_DownTimeStatus.UseVisualStyleBackColor = true;
      // 
      // chk_WaitingRestartDelay
      // 
      this.chk_WaitingRestartDelay.AutoSize = true;
      this.chk_WaitingRestartDelay.Location = new System.Drawing.Point(423, 60);
      this.chk_WaitingRestartDelay.Name = "chk_WaitingRestartDelay";
      this.chk_WaitingRestartDelay.Size = new System.Drawing.Size(15, 14);
      this.chk_WaitingRestartDelay.TabIndex = 331;
      this.chk_WaitingRestartDelay.UseVisualStyleBackColor = true;
      // 
      // chk_MicroStopElapsed
      // 
      this.chk_MicroStopElapsed.AutoSize = true;
      this.chk_MicroStopElapsed.Location = new System.Drawing.Point(423, 40);
      this.chk_MicroStopElapsed.Name = "chk_MicroStopElapsed";
      this.chk_MicroStopElapsed.Size = new System.Drawing.Size(15, 14);
      this.chk_MicroStopElapsed.TabIndex = 330;
      this.chk_MicroStopElapsed.UseVisualStyleBackColor = true;
      // 
      // chk_DownTimeProgress
      // 
      this.chk_DownTimeProgress.AutoSize = true;
      this.chk_DownTimeProgress.Location = new System.Drawing.Point(423, 18);
      this.chk_DownTimeProgress.Name = "chk_DownTimeProgress";
      this.chk_DownTimeProgress.Size = new System.Drawing.Size(15, 14);
      this.chk_DownTimeProgress.TabIndex = 329;
      this.chk_DownTimeProgress.UseVisualStyleBackColor = true;
      // 
      // tmt_LineStopped
      // 
      this.tmt_LineStopped.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_LineStopped.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_LineStopped.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_LineStopped.ImagePassword = null;
      this.tmt_LineStopped.Location = new System.Drawing.Point(422, 56);
      this.tmt_LineStopped.Name = "tmt_LineStopped";
      this.tmt_LineStopped.PasswordChar = '\0';
      this.tmt_LineStopped.ReadOnly = true;
      this.tmt_LineStopped.Size = new System.Drawing.Size(149, 38);
      this.tmt_LineStopped.Style = SyncroC.Controls.TitledMaskTextBoxStyles.NoTitle;
      this.tmt_LineStopped.TabIndex = 327;
      this.tmt_LineStopped.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_LineStopped.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_LineStopped.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_LineStopped.TranslateText = false;
      this.tmt_LineStopped.Value = " ";
      this.tmt_LineStopped.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_LineStopped.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_LineStopped.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_LineStopped.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_LineStopped.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // lbl_WaitRestartDelay
      // 
      this.lbl_WaitRestartDelay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_WaitRestartDelay.Location = new System.Drawing.Point(6, 56);
      this.lbl_WaitRestartDelay.Name = "lbl_WaitRestartDelay";
      this.lbl_WaitRestartDelay.Size = new System.Drawing.Size(410, 20);
      this.lbl_WaitRestartDelay.TabIndex = 326;
      this.lbl_WaitRestartDelay.Text = "Waiting Restart Delay";
      this.lbl_WaitRestartDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_MicroStopElapsed
      // 
      this.lbl_MicroStopElapsed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MicroStopElapsed.Location = new System.Drawing.Point(6, 36);
      this.lbl_MicroStopElapsed.Name = "lbl_MicroStopElapsed";
      this.lbl_MicroStopElapsed.Size = new System.Drawing.Size(410, 20);
      this.lbl_MicroStopElapsed.TabIndex = 325;
      this.lbl_MicroStopElapsed.Text = "Micro Stop Elapsed";
      this.lbl_MicroStopElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_LineStarted
      // 
      this.lbl_LineStarted.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_LineStarted.Location = new System.Drawing.Point(6, 96);
      this.lbl_LineStarted.Name = "lbl_LineStarted";
      this.lbl_LineStarted.Size = new System.Drawing.Size(410, 20);
      this.lbl_LineStarted.TabIndex = 324;
      this.lbl_LineStarted.Text = "Line Started";
      this.lbl_LineStarted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_lineStopped
      // 
      this.lbl_lineStopped.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_lineStopped.Location = new System.Drawing.Point(6, 76);
      this.lbl_lineStopped.Name = "lbl_lineStopped";
      this.lbl_lineStopped.Size = new System.Drawing.Size(410, 20);
      this.lbl_lineStopped.TabIndex = 322;
      this.lbl_lineStopped.Text = "Line Stopped";
      this.lbl_lineStopped.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DownTimeProgress
      // 
      this.lbl_DownTimeProgress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DownTimeProgress.Location = new System.Drawing.Point(6, 16);
      this.lbl_DownTimeProgress.Name = "lbl_DownTimeProgress";
      this.lbl_DownTimeProgress.Size = new System.Drawing.Size(410, 20);
      this.lbl_DownTimeProgress.TabIndex = 320;
      this.lbl_DownTimeProgress.Text = "DownTime in Progress";
      this.lbl_DownTimeProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tmt_LineStarted
      // 
      this.tmt_LineStarted.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_LineStarted.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_LineStarted.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_LineStarted.ImagePassword = null;
      this.tmt_LineStarted.Location = new System.Drawing.Point(422, 76);
      this.tmt_LineStarted.Name = "tmt_LineStarted";
      this.tmt_LineStarted.PasswordChar = '\0';
      this.tmt_LineStarted.ReadOnly = true;
      this.tmt_LineStarted.Size = new System.Drawing.Size(149, 38);
      this.tmt_LineStarted.Style = SyncroC.Controls.TitledMaskTextBoxStyles.NoTitle;
      this.tmt_LineStarted.TabIndex = 328;
      this.tmt_LineStarted.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_LineStarted.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_LineStarted.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_LineStarted.TranslateText = false;
      this.tmt_LineStarted.Value = " ";
      this.tmt_LineStarted.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_LineStarted.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_LineStarted.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_LineStarted.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_LineStarted.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tab_SyncroStatus
      // 
      this.tab_SyncroStatus.Controls.Add(this.tab_DownTimeStatus);
      this.tab_SyncroStatus.Location = new System.Drawing.Point(30, 193);
      this.tab_SyncroStatus.Name = "tab_SyncroStatus";
      this.tab_SyncroStatus.SelectedIndex = 0;
      this.tab_SyncroStatus.Size = new System.Drawing.Size(1171, 519);
      this.tab_SyncroStatus.TabIndex = 110;
      // 
      // Frm_SyncroStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_SyncroStatus);
      this.Name = "Frm_SyncroStatus";
      this.Text = "Frm_SyncroStatus";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tab_SyncroStatus, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_DownTimeStatus.ResumeLayout(false);
      this.tab_DownTimeStatus.PerformLayout();
      this.tab_SyncroStatus.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion
	}
}

