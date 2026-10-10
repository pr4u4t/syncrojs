using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.GlueUnit.Settings
{
	public partial class Frm_GlueUnitSettingsTask : SyncroC.Forms.Pages.GlueUnit.Settings.Frm_GlueUnitSettingsSubgroup
	{
		private bool modify = false;
		private bool modGlueUnit = false;
    //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
    private bool modGlueUnitKohler = false;

		private SyncroConfig cfg;

		public Frm_GlueUnitSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			this.CfgCrestGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.GUGenericSettings)cfg.GlueUnitSetting.CreateCopy();
			this.CfgCrestCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.GUGenericCTRLSettings)cfg.GlueUnitCTRLSetting.CreateCopy();

      //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
      this.CfgKohlerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.GUGenericSettings)cfg.GlueUnitSetting.CreateCopy();
      this.CfgKohlerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.GU_KohlerCtrlSettings)cfg.GlueUnitKohlerCTRLSetting.CreateCopy();

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
					if (this.modGlueUnit )
					{
						cfg.GlueUnitSetting  = (GUGenericSettings)this.CfgCrestGrid.SelectedObject;
						cfg.GlueUnitSetting.Save();

						cfg.GlueUnitCTRLSetting  = (GUGenericCTRLSettings)this.CfgCrestCTRLGrid.SelectedObject;
						cfg.GlueUnitCTRLSetting.Save();
					}

          //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
          if (this.modGlueUnitKohler)
          {
            cfg.GlueUnitSetting = (GUGenericSettings)this.CfgCrestGrid.SelectedObject;
            cfg.GlueUnitSetting.Save();

            cfg.GlueUnitKohlerCTRLSetting = (GU_KohlerCtrlSettings)this.CfgKohlerCTRLGrid.SelectedObject;
            cfg.GlueUnitKohlerCTRLSetting.Save();
          }

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgCrestGrid.ViewForeColor = Color.DimGray;
					this.CfgCrestCTRLGrid.ViewForeColor = Color.DimGray;

          //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
          this.CfgKohlerGrid.ViewForeColor = Color.DimGray;
          this.CfgKohlerCTRLGrid.ViewForeColor = Color.DimGray;

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
					this.CfgCrestGrid.ViewForeColor = Color.Black;
					this.CfgCrestCTRLGrid.ViewForeColor = Color.Black;

          //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
          this.CfgKohlerGrid.ViewForeColor = Color.Black;
          this.CfgKohlerCTRLGrid.ViewForeColor = Color.Black;

					this.modify = true;
					this.modGlueUnit  = false;
          //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
          this.modGlueUnitKohler = false;

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
				this.CfgCrestGrid.ViewForeColor = Color.DimGray;
				this.CfgCrestCTRLGrid.ViewForeColor = Color.DimGray;
        //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
        this.CfgKohlerGrid.ViewForeColor = Color.DimGray;
        this.CfgKohlerCTRLGrid.ViewForeColor = Color.DimGray;

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
				if (((PropertyGrid)s).Equals(this.CfgCrestGrid) || ((PropertyGrid)s).Equals(this.CfgCrestCTRLGrid))
				{
					this.modGlueUnit = true;
				}
        //RFAZZI_FI23/02/2011_1. Aggiunto task CTRL di comunicazione con Glue Unit Kohler
        if (((PropertyGrid)s).Equals(this.CfgKohlerGrid) || ((PropertyGrid)s).Equals(this.CfgKohlerCTRLGrid))
        {
          this.modGlueUnitKohler = true;
        }

			}
		}

		private void CfgCrestGrid_Click(object sender, EventArgs e)
		{

		}

	}
}

