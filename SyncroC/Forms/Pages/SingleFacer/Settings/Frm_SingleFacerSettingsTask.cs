using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase.SyncroConfig;

namespace SyncroC.Forms.Pages.SingleFacer.Settings
{
	public partial class Frm_SingleFacerSettingsTask : SyncroC.Forms.Pages.SingleFacer.Settings.Frm_SingleFacerSettingsSubgroup
	{
		private bool modify = false;
		private bool modSingleFacer = false;
		private bool modSmart = false;
    private bool modSmart400 = false;
		private bool modBridge = false;

		private SyncroConfig cfg;


		public Frm_SingleFacerSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;

		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerSettingsTask(int index)
			: this()
		{
			if (! (index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerSettingsTask - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
		}

		protected override void F11()
		{
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			if (index == 1)
			{
				this.CfgSingleFacerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric1Setting.CreateCopy();
				this.CfgSingleFacerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericCTRLSettings)cfg.SingleFacerGenericCTRL1Setting.CreateCopy();
				this.CfgSmartGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric1Setting.CreateCopy();
				this.CfgSmartCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SFSmartCTRLSettings)cfg.SingleFacerSmartCTRL1Setting.CreateCopy();
        this.CfgSmart400Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric1Setting.CreateCopy();
        this.CfgSmart400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SFSmart400CTRLSettings)cfg.SingleFacerSmart400CTRL1Setting.CreateCopy();
				this.CfgBridgeGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.BRIDGESettings)cfg.Bridge1Setting.CreateCopy();
			}
			else if (index == 2)
			{
				this.CfgSingleFacerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric2Setting.CreateCopy();
				this.CfgSingleFacerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericCTRLSettings)cfg.SingleFacerGenericCTRL2Setting.CreateCopy();
				this.CfgSmartGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric2Setting.CreateCopy();
				this.CfgSmartCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SFSmartCTRLSettings)cfg.SingleFacerSmartCTRL2Setting.CreateCopy();
        this.CfgSmart400Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric2Setting.CreateCopy();
        this.CfgSmart400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SFSmart400CTRLSettings)cfg.SingleFacerSmart400CTRL2Setting.CreateCopy();
				this.CfgBridgeGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.BRIDGESettings)cfg.Bridge2Setting.CreateCopy();
			}
			else
			{
				this.CfgSingleFacerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric3Setting.CreateCopy();
				this.CfgSingleFacerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericCTRLSettings)cfg.SingleFacerGenericCTRL3Setting.CreateCopy();
				this.CfgSmartGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric3Setting.CreateCopy();
				this.CfgSmartCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SFSmartCTRLSettings)cfg.SingleFacerSmartCTRL3Setting.CreateCopy();
        this.CfgSmart400Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SingleFacerGenericSettings)cfg.SingleFacerGeneric3Setting.CreateCopy();
        this.CfgSmart400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SFSmart400CTRLSettings)cfg.SingleFacerSmart400CTRL3Setting.CreateCopy();
				this.CfgBridgeGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.BRIDGESettings)cfg.Bridge3Setting.CreateCopy();
			}
		}

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
					if (this.modSingleFacer )
					{
						if (index == 1)
						{
							cfg.SingleFacerGeneric1Setting = (SingleFacerGenericSettings)this.CfgSingleFacerGrid.SelectedObject;
							cfg.SingleFacerGeneric1Setting.Save();
							cfg.SingleFacerGenericCTRL1Setting = (SingleFacerGenericCTRLSettings)this.CfgSingleFacerCTRLGrid.SelectedObject;
							cfg.SingleFacerGenericCTRL1Setting.Save();
						}
						else if (index == 2)
						{
							cfg.SingleFacerGeneric2Setting = (SingleFacerGenericSettings)this.CfgSingleFacerGrid.SelectedObject;
							cfg.SingleFacerGeneric2Setting.Save();
							cfg.SingleFacerGenericCTRL2Setting = (SingleFacerGenericCTRLSettings)this.CfgSingleFacerCTRLGrid.SelectedObject;
							cfg.SingleFacerGenericCTRL2Setting.Save();
						}
						else
						{
							cfg.SingleFacerGeneric3Setting = (SingleFacerGenericSettings)this.CfgSingleFacerGrid.SelectedObject;
							cfg.SingleFacerGeneric3Setting.Save();
							cfg.SingleFacerGenericCTRL3Setting = (SingleFacerGenericCTRLSettings)this.CfgSingleFacerCTRLGrid.SelectedObject;
							cfg.SingleFacerGenericCTRL3Setting.Save();
						}
					}

					if (this.modSmart )
					{
						if (index == 1)
						{
							cfg.SingleFacerGeneric1Setting = (SingleFacerGenericSettings)this.CfgSmartGrid.SelectedObject;
							cfg.SingleFacerGeneric1Setting.Save();
							cfg.SingleFacerSmartCTRL1Setting  = (SFSmartCTRLSettings )this.CfgSmartCTRLGrid.SelectedObject;
							cfg.SingleFacerSmartCTRL1Setting.Save();
						}
						else if (index == 2)
						{
							cfg.SingleFacerGeneric2Setting = (SingleFacerGenericSettings)this.CfgSmartGrid.SelectedObject;
							cfg.SingleFacerGeneric2Setting.Save();
							cfg.SingleFacerSmartCTRL2Setting = (SFSmartCTRLSettings)this.CfgSmartCTRLGrid.SelectedObject;
							cfg.SingleFacerSmartCTRL2Setting.Save();
						}
						else
						{
							cfg.SingleFacerGeneric3Setting = (SingleFacerGenericSettings)this.CfgSmartGrid.SelectedObject;
							cfg.SingleFacerGeneric3Setting.Save();
							cfg.SingleFacerSmartCTRL3Setting = (SFSmartCTRLSettings)this.CfgSmartCTRLGrid.SelectedObject;
							cfg.SingleFacerSmartCTRL3Setting.Save();
						}
					}

          if (this.modSmart400)
          {
            if (index == 1)
            {
              cfg.SingleFacerGeneric1Setting = (SingleFacerGenericSettings)this.CfgSmart400Grid.SelectedObject;
              cfg.SingleFacerGeneric1Setting.Save();
              cfg.SingleFacerSmart400CTRL1Setting = (SFSmart400CTRLSettings)this.CfgSmart400CTRLGrid.SelectedObject;
              cfg.SingleFacerSmart400CTRL1Setting.Save();
            }
            else if (index == 2)
            {
              cfg.SingleFacerGeneric2Setting = (SingleFacerGenericSettings)this.CfgSmart400Grid.SelectedObject;
              cfg.SingleFacerGeneric2Setting.Save();
              cfg.SingleFacerSmart400CTRL2Setting = (SFSmart400CTRLSettings)this.CfgSmart400CTRLGrid.SelectedObject;
              cfg.SingleFacerSmart400CTRL2Setting.Save();
            }
            else
            {
              cfg.SingleFacerGeneric3Setting = (SingleFacerGenericSettings)this.CfgSmart400Grid.SelectedObject;
              cfg.SingleFacerGeneric3Setting.Save();
              cfg.SingleFacerSmart400CTRL3Setting = (SFSmart400CTRLSettings)this.CfgSmart400CTRLGrid.SelectedObject;
              cfg.SingleFacerSmart400CTRL3Setting.Save();
            }
          }

					if (this.modBridge)
					{
						if (index == 1)
						{
							cfg.Bridge1Setting  = (BRIDGESettings )this.CfgBridgeGrid.SelectedObject;
							cfg.Bridge1Setting.Save();
						}
						else if (index == 2)
						{
							cfg.Bridge2Setting = (BRIDGESettings)this.CfgBridgeGrid.SelectedObject;
							cfg.Bridge2Setting.Save();
						}
						else
						{
							cfg.Bridge3Setting = (BRIDGESettings)this.CfgBridgeGrid.SelectedObject;
							cfg.Bridge3Setting.Save();
						}
					}

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgSingleFacerGrid.ViewForeColor = Color.DimGray;
					this.CfgSingleFacerCTRLGrid.ViewForeColor = Color.DimGray;
					this.CfgSmartGrid.ViewForeColor = Color.DimGray;
					this.CfgSmartCTRLGrid.ViewForeColor = Color.DimGray;
          this.CfgSmart400Grid.ViewForeColor = Color.DimGray;
          this.CfgSmart400CTRLGrid.ViewForeColor = Color.DimGray;
					this.CfgBridgeGrid.ViewForeColor = Color.DimGray;

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

					this.CfgSingleFacerGrid.ViewForeColor = Color.Black ;
					this.CfgSingleFacerCTRLGrid.ViewForeColor = Color.Black ;
					this.CfgSmartGrid.ViewForeColor = Color.Black ;
					this.CfgSmartCTRLGrid.ViewForeColor = Color.Black ;
          this.CfgSmart400Grid.ViewForeColor = Color.Black;
          this.CfgSmart400CTRLGrid.ViewForeColor = Color.Black;
					this.CfgBridgeGrid.ViewForeColor = Color.Black ;

					this.modify = true;
					this.modSingleFacer  = false;
					this.modSmart  = false;
					this.modBridge  = false;
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
				this.CfgSingleFacerGrid.ViewForeColor = Color.DimGray;
				this.CfgSingleFacerCTRLGrid.ViewForeColor = Color.DimGray;
				this.CfgSmartGrid.ViewForeColor = Color.DimGray;
				this.CfgSmartCTRLGrid.ViewForeColor = Color.DimGray;
        this.CfgSmart400Grid.ViewForeColor = Color.DimGray;
        this.CfgSmart400CTRLGrid.ViewForeColor = Color.DimGray;
				this.CfgBridgeGrid.ViewForeColor = Color.DimGray;

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
				if (((PropertyGrid)s).Equals(this.CfgSingleFacerGrid ) || ((PropertyGrid)s).Equals(this.CfgSingleFacerCTRLGrid ))
				{
					this.modSingleFacer  = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgSmartGrid ) || ((PropertyGrid)s).Equals(this.CfgSmartCTRLGrid ))
				{
					this.modSmart  = true;
				}
        if (((PropertyGrid)s).Equals(this.CfgSmart400Grid) || ((PropertyGrid)s).Equals(this.CfgSmart400CTRLGrid))
        {
          this.modSmart400 = true;
        }
				if (((PropertyGrid)s).Equals(this.CfgBridgeGrid ) )
				{
					this.modBridge  = true;
				}
			}
		}
	}
}

