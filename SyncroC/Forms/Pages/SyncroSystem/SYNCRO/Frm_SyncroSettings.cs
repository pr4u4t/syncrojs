using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.SYNCRO
{
	public class Frm_SyncroSettings : SyncroC.Forms.Pages.SyncroSystem.SYNCRO.Frm_SyncroSubGroup
	{
		private PropertyGrid CfgSYNCROGrid;
		private PropertyGrid CfgENCODERSGrid;
		private System.ComponentModel.IContainer components = null;

		private bool modSYNCRO = false;
		private bool modENCODERS = false;
		private bool modify = false;

		public Frm_SyncroSettings()
		{
			InitializeComponent();
      //this.CtrlF1();
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
		protected override void F10()
		{
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.CfgSYNCROGrid.ViewForeColor = Color.Black;
				this.CfgENCODERSGrid.ViewForeColor = Color.Black;
				this.modify = true;
				this.modSYNCRO = false;
				this.modENCODERS = false;
        lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void CtrlF4()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Save Settings modifications
				if (this.modify)
				{
					if (this.modSYNCRO)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.SYNCRO + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.Syncro)this.CfgSYNCROGrid.SelectedObject).SerializeProperties() + ")");
					}

					if (this.modENCODERS)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.ENCODERS + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.Encoders)this.CfgENCODERSGrid.SelectedObject).SerializeProperties() + ")");
					}

					this.CfgSYNCROGrid.ViewForeColor = Color.DimGray;
					this.CfgENCODERSGrid.ViewForeColor = Color.DimGray;
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
					this.modSYNCRO = false;
					this.modENCODERS = false;
					this.modify = false;
					this.refreshData();
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

    //protected override void CtrlF1()
    //{
    //  Encoders enc = DBManager.getDBManager().lineConfig.EncodersConfig;
    //  bool vis=this.IsPropertyBrowsable(enc, "MMPerPulseMeasureWheel");
    //  this.MakePropertyBrowsable(enc, "MMPerPulseMeasureWheel", !vis);
    //  this.refreshData();
    //}

		protected override void Escape()
		{
			//Esc without saving
			if (this.modify)
			{
				this.CfgSYNCROGrid.ViewForeColor = Color.DimGray;
				this.CfgENCODERSGrid.ViewForeColor = Color.DimGray;
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				this.modSYNCRO = false;
				this.modENCODERS = false;
				this.modify = false;
				this.refreshData();
			}
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SyncroSettings));
      this.CfgSYNCROGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgENCODERSGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // CfgSYNCROGrid
      // 
      this.CfgSYNCROGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSYNCROGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSYNCROGrid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CfgSYNCROGrid.Location = new System.Drawing.Point(30, 193);
      this.CfgSYNCROGrid.Name = "CfgSYNCROGrid";
      this.CfgSYNCROGrid.Size = new System.Drawing.Size(1171, 324);
      this.CfgSYNCROGrid.TabIndex = 113;
      this.CfgSYNCROGrid.ToolbarVisible = false;
      this.CfgSYNCROGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSYNCROGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSYNCROGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgSYNCROGrid_PropertyValueChanged);
      // 
      // CfgENCODERSGrid
      // 
      this.CfgENCODERSGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgENCODERSGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgENCODERSGrid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CfgENCODERSGrid.Location = new System.Drawing.Point(30, 537);
      this.CfgENCODERSGrid.Name = "CfgENCODERSGrid";
      this.CfgENCODERSGrid.Size = new System.Drawing.Size(447, 175);
      this.CfgENCODERSGrid.TabIndex = 114;
      this.CfgENCODERSGrid.ToolbarVisible = false;
      this.CfgENCODERSGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgENCODERSGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgENCODERSGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgENCODERSGrid_PropertyValueChanged);
      // 
      // Frm_SyncroSettings
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.CfgENCODERSGrid);
      this.Controls.Add(this.CfgSYNCROGrid);
      this.Name = "Frm_SyncroSettings";
      this.Text = "Frm_SyncroSettings";
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
      this.Controls.SetChildIndex(this.CfgSYNCROGrid, 0);
      this.Controls.SetChildIndex(this.CfgENCODERSGrid, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void Frm_SYNCROSettings_Load(object sender, EventArgs e)
		{
			this.refreshData();
			this.CfgSYNCROGrid.ExpandAllGridItems();
			this.CfgENCODERSGrid.ExpandAllGridItems();
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.CfgSYNCROGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Syncro)cfg.SyncroConfig.CreateCopy();
			this.CfgENCODERSGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Encoders)cfg.EncodersConfig.CreateCopy();
		}

		private void CfgSYNCROGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgSYNCROGrid))
				{
					this.modSYNCRO = true;
				}
			}
		}

		private void CfgENCODERSGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgENCODERSGrid))
				{
					this.modENCODERS = true;
				}
			}
		}
	}
}

