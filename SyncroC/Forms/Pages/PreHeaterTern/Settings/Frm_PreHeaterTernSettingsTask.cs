using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.PreHeaterTern.Settings
{
	public partial class Frm_PreHeaterTernSettingsTask : SyncroC.Forms.Pages.PreHeaterTern.Settings.Frm_PreHeaterTernSettingsSubgroup
	{
		private bool modify = false;
		private bool modThermostack = false;
		private SyncroConfig cfg;

		public Frm_PreHeaterTernSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			this.CfgThermostackGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.PHGenericSettings)cfg.ThermostackSetting.CreateCopy();
			this.CfgThermostackCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.PHGenericCTRLSettings)cfg.ThermostackCTRLSetting.CreateCopy();
		}

		protected override void F11()
		{
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
					if (this.modThermostack )
					{
						cfg.ThermostackSetting = (PHGenericSettings)this.CfgThermostackGrid.SelectedObject;
						cfg.ThermostackSetting.Save();

						cfg.ThermostackCTRLSetting = (PHGenericCTRLSettings)this.CfgThermostackCTRLGrid.SelectedObject;
						cfg.ThermostackCTRLSetting.Save();
					}

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgThermostackGrid.ViewForeColor = Color.DimGray;
					this.CfgThermostackCTRLGrid.ViewForeColor = Color.DimGray;
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
					this.CfgThermostackGrid.ViewForeColor = Color.Black;
					this.CfgThermostackCTRLGrid.ViewForeColor = Color.Black;

					this.modify = true;
					this.modThermostack = false;

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
				this.CfgThermostackGrid.ViewForeColor = Color.DimGray;
				this.CfgThermostackCTRLGrid.ViewForeColor = Color.DimGray;
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
				if (((PropertyGrid)s).Equals(this.CfgThermostackGrid) || ((PropertyGrid)s).Equals(this.CfgThermostackCTRLGrid))
				{
					this.modThermostack = true;
				}
			}
		}

	}
}

