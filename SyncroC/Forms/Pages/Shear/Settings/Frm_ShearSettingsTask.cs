using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Shear.Settings
{
	public class Frm_ShearSettingsTask : SyncroC.Forms.Pages.Shear.Settings.Frm_ShearSettingsSubgroup
	{
		private TabPage tab_RSH_Fosber23_XX;
		private PropertyGrid CfgFosber23_XXGrid;
		private TabControl tab_Settings;
		private System.ComponentModel.IContainer components = null;
		private bool modify = false;
		private bool modFosber23_XX = false;
        private TabPage tab_RSH_Mhi;
        private PropertyGrid CfgMhi_Grid;

        //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
        private bool modMhi = false;		

        private SyncroConfig cfg;

		public Frm_ShearSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			this.CfgFosber23_XXGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RSHFosber23_XXSettings)cfg.RSHFosber23_XXSetting.CreateCopy();
      //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
      this.CfgMhi_Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RSHMhi_Settings)cfg.RSHMhi_Setting.CreateCopy();
		}
		
		/// 
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ShearSettingsTask));
      this.tab_RSH_Fosber23_XX = new System.Windows.Forms.TabPage();
      this.CfgFosber23_XXGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_RSH_Mhi = new System.Windows.Forms.TabPage();
      this.CfgMhi_Grid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_RSH_Fosber23_XX.SuspendLayout();
      this.tab_Settings.SuspendLayout();
      this.tab_RSH_Mhi.SuspendLayout();
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
      // tab_RSH_Fosber23_XX
      // 
      this.tab_RSH_Fosber23_XX.Controls.Add(this.CfgFosber23_XXGrid);
      this.tab_RSH_Fosber23_XX.Location = new System.Drawing.Point(4, 27);
      this.tab_RSH_Fosber23_XX.Name = "tab_RSH_Fosber23_XX";
      this.tab_RSH_Fosber23_XX.Padding = new System.Windows.Forms.Padding(3);
      this.tab_RSH_Fosber23_XX.Size = new System.Drawing.Size(1163, 488);
      this.tab_RSH_Fosber23_XX.TabIndex = 1;
      this.tab_RSH_Fosber23_XX.Text = "Fosber 23.XX";
      this.tab_RSH_Fosber23_XX.UseVisualStyleBackColor = true;
      // 
      // CfgFosber23_XXGrid
      // 
      this.CfgFosber23_XXGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber23_XXGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber23_XXGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgFosber23_XXGrid.Name = "CfgFosber23_XXGrid";
      this.CfgFosber23_XXGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber23_XXGrid.Size = new System.Drawing.Size(967, 399);
      this.CfgFosber23_XXGrid.TabIndex = 119;
      this.CfgFosber23_XXGrid.ToolbarVisible = false;
      this.CfgFosber23_XXGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber23_XXGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber23_XXGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Settings
      // 
      this.tab_Settings.Controls.Add(this.tab_RSH_Fosber23_XX);
      this.tab_Settings.Controls.Add(this.tab_RSH_Mhi);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(30, 192);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 519);
      this.tab_Settings.TabIndex = 120;
      // 
      // tab_RSH_Mhi
      // 
      this.tab_RSH_Mhi.Controls.Add(this.CfgMhi_Grid);
      this.tab_RSH_Mhi.Location = new System.Drawing.Point(4, 27);
      this.tab_RSH_Mhi.Name = "tab_RSH_Mhi";
      this.tab_RSH_Mhi.Size = new System.Drawing.Size(1163, 488);
      this.tab_RSH_Mhi.TabIndex = 2;
      this.tab_RSH_Mhi.Text = "RSH Mhi";
      this.tab_RSH_Mhi.UseVisualStyleBackColor = true;
      // 
      // CfgMhi_Grid
      // 
      this.CfgMhi_Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMhi_Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMhi_Grid.Location = new System.Drawing.Point(46, 46);
      this.CfgMhi_Grid.Name = "CfgMhi_Grid";
      this.CfgMhi_Grid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgMhi_Grid.Size = new System.Drawing.Size(967, 399);
      this.CfgMhi_Grid.TabIndex = 120;
      this.CfgMhi_Grid.ToolbarVisible = false;
      this.CfgMhi_Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMhi_Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMhi_Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_ShearSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_ShearSettingsTask";
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
      this.tab_RSH_Fosber23_XX.ResumeLayout(false);
      this.tab_Settings.ResumeLayout(false);
      this.tab_RSH_Mhi.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion
		public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
		{
			if (!config.getOnServer())
			{
				lbl_CtrlF4_YY.Text = lbl_CtrlF5_YY.Text = string.Empty;
			}
		}

		protected override void CtrlF4()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				if (this.modify)
				{
					if (this.modFosber23_XX )
					{
						cfg.RSHFosber23_XXSetting = (RSHFosber23_XXSettings)this.CfgFosber23_XXGrid.SelectedObject;
						cfg.RSHFosber23_XXSetting.Save();
					}
          else if (this.modMhi) //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
          {
              cfg.RSHMhi_Setting = (RSHMhi_Settings)this.CfgMhi_Grid.SelectedObject;
              cfg.RSHMhi_Setting.Save();
          }

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgFosber23_XXGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
          this.CfgMhi_Grid.ViewForeColor = Color.DimGray;

          lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);

					this.modify = false;
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				Utility.Settings s = Utility.Settings.getSettings();
				if (s.configuration.getOnServer())
				{
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.Black;
					this.CfgFosber23_XXGrid.ViewForeColor = Color.Black;
					this.modify = true;
					this.modFosber23_XX = false;

          //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
          this.CfgMhi_Grid.ViewForeColor = Color.Black;                    
          this.modMhi = false;
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		/// <summary>
		/// Escape key behaviour: Abort the operation
		/// </summary>
		protected override void Escape()
		{
			if (this.modify)
			{
				this.modify = false;
				this.CfgFosber23_XXGrid.ViewForeColor = Color.DimGray;

        //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
        this.CfgMhi_Grid.ViewForeColor = Color.DimGray;

				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				this.refreshData();
			}
		}

		private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgFosber23_XXGrid) )
				{
					this.modFosber23_XX = true;
				}
        else if (((PropertyGrid)s).Equals(this.CfgMhi_Grid)) //PMASSARO_FI29/06/10 - Aggiunto Task RSH MHI
        {
            this.modMhi = true;
        }
			}
		}
	}
}

