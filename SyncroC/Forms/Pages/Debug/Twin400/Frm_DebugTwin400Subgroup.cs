using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Debug.Twin400
{
	public class Frm_DebugTwin400Subgroup : SyncroC.Forms.Pages.Debug.Frm_DebugGroup
	{
		private System.ComponentModel.IContainer components = null;
    protected Label lab_Time;
    protected Label lab_path;
    protected Timer tim_Log;
    protected Twin400LogAnalyzer logAnalyzer;

		public Frm_DebugTwin400Subgroup()
		{
			InitializeComponent();
      this.logAnalyzer = new Twin400LogAnalyzer(LogType.LogState, this.lab_path, this.lab_Time, this.lbl_Title_YN, this.tim_Log, this);
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
      this.logAnalyzer.Destroy();
			base.Dispose( disposing );
		}

    /// <summary>
    /// To be called by the server and invoked by the client throw the reflection to show a new page.
    /// Last visited page (PagesHistorian.CurrentPageDebugTwin400) for the DebugTwin400 group is set here.
    /// </summary>
    public override void _Show()
    {
      PageManager pm = PageManager.getPageManager();
      pm.pagesHistorian.CurrentPageDebugTwin400 = this.getRequestCommand();
      base._Show();
    }


    private void tim_Log_Tick(object sender, EventArgs e)
    {
      this.logAnalyzer.tim_Log_Tick();
    }

    protected override void F3()
    {
    }

    protected override void F10()
    {
      askForPage(typeof(Frm_DebugTwin400Status1));
    }

    protected override void F11()
    {
      askForPage(typeof(Frm_DebugTwin400Status2));
    }

    protected override void F12()
    {
      askForPage(typeof(Frm_DebugTwin400SetupSituation));
    }

		protected override void F13()
		{
			askForPage(typeof(Frm_DebugTwin400_positioning));
		}
    #region Designer generated code
    /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugTwin400Subgroup));
			this.lab_Time = new System.Windows.Forms.Label();
			this.lab_path = new System.Windows.Forms.Label();
			this.tim_Log = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// tab_Tab3
			// 
			this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			// 
			// tab_TabA
			// 
			this.tab_TabA.Text = "Machine Status";
			this.tab_TabA.Visible = true;
			// 
			// tab_TabD
			// 
			this.tab_TabD.Text = "Positioning";
			this.tab_TabD.Visible = true;
			// 
			// tab_TabC
			// 
			this.tab_TabC.Text = "Setup Status";
			this.tab_TabC.Visible = true;
			// 
			// tab_TabB
			// 
			this.tab_TabB.Text = "Task Status";
			this.tab_TabB.Visible = true;
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
			// lab_Time
			// 
			this.lab_Time.Location = new System.Drawing.Point(757, 57);
			this.lab_Time.Name = "lab_Time";
			this.lab_Time.Size = new System.Drawing.Size(79, 24);
			this.lab_Time.TabIndex = 135;
			this.lab_Time.Visible = false;
			// 
			// lab_path
			// 
			this.lab_path.Location = new System.Drawing.Point(111, 57);
			this.lab_path.Name = "lab_path";
			this.lab_path.Size = new System.Drawing.Size(613, 22);
			this.lab_path.TabIndex = 134;
			this.lab_path.Visible = false;
			// 
			// tim_Log
			// 
			this.tim_Log.Interval = 1000;
			this.tim_Log.Tick += new System.EventHandler(this.tim_Log_Tick);
			// 
			// Frm_DebugTwin400Subgroup
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Controls.Add(this.lab_Time);
			this.Controls.Add(this.lab_path);
			this.Name = "Frm_DebugTwin400Subgroup";
			this.Text = "Frm_DebugTwin400Subgroup";
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
			this.Controls.SetChildIndex(this.lab_path, 0);
			this.Controls.SetChildIndex(this.lab_Time, 0);
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

    }
		#endregion
	}
}

