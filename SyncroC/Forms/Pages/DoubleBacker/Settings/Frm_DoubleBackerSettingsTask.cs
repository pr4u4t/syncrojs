using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.DoubleBacker.Settings
{
	public partial class Frm_DoubleBackerSettingsTask : SyncroC.Forms.Pages.DoubleBacker.Settings.Frm_DoubleBackerSettingsSubgroup
	{

		private bool modify = false;
		private bool modDBGeneric = false;
    private bool modDBExpress = false;
    private bool modDBCarteam = false; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
    private bool modDBExpress400 = false; //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
    private SyncroConfig cfg;

		public Frm_DoubleBackerSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;

		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			this.CfgDBGenericGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBGenericSettings )cfg.DBGenericSetting.CreateCopy();
			this.CfgDBGenericCtrlGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBGenericCtrlSettings )cfg.DBGenericCtrlSetting.CreateCopy();
			this.CfgDBExpressGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBGenericSettings)cfg.DBGenericSetting.CreateCopy();
			this.CfgDBExpressCtrlGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBExpressCtrlSettings)cfg.DBExpressCtrlSetting.CreateCopy();

      //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
      this.CfgDBCarteamGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBGenericSettings)cfg.DBGenericSetting.CreateCopy();
      this.CfgDBCarteamCtrlGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBCarteamCtrlSettings)cfg.DBCarteamCtrlSetting.CreateCopy();

      //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
      this.CfgDBExpress400Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBGenericSettings)cfg.DBGenericSetting.CreateCopy();
      this.CfgDBExpress400CtrlGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBExpress400CtrlSettings)cfg.DBExpress400CtrlSetting.CreateCopy();
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
					if (this.modDBGeneric )
					{
						cfg.DBGenericSetting = (DBGenericSettings )this.CfgDBGenericGrid.SelectedObject;
						cfg.DBGenericSetting.Save();
						cfg.DBGenericCtrlSetting = (DBGenericCtrlSettings)this.CfgDBGenericCtrlGrid.SelectedObject;
						cfg.DBGenericCtrlSetting.Save();
					}

					if (this.modDBExpress )
					{
						cfg.DBGenericSetting = (DBGenericSettings)this.CfgDBExpressGrid.SelectedObject;
						cfg.DBGenericSetting.Save();
						cfg.DBExpressCtrlSetting = (DBExpressCtrlSettings)this.CfgDBExpressCtrlGrid.SelectedObject;
						cfg.DBExpressCtrlSetting.Save();
					}

          //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
          if (this.modDBCarteam)
          {
            cfg.DBGenericSetting = (DBGenericSettings)this.CfgDBCarteamGrid.SelectedObject;
            cfg.DBGenericSetting.Save();
            cfg.DBCarteamCtrlSetting = (DBCarteamCtrlSettings)this.CfgDBCarteamCtrlGrid.SelectedObject;
            cfg.DBCarteamCtrlSetting.Save();
          }

          //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
          if (this.modDBExpress400)
          {
            cfg.DBGenericSetting = (DBGenericSettings)this.CfgDBExpress400Grid.SelectedObject;
            cfg.DBGenericSetting.Save();
            cfg.DBExpress400CtrlSetting = (DBExpress400CtrlSettings)this.CfgDBExpress400CtrlGrid.SelectedObject;
            cfg.DBExpress400CtrlSetting.Save();
          }

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgDBGenericGrid.ViewForeColor = Color.DimGray;
					this.CfgDBGenericCtrlGrid.ViewForeColor = Color.DimGray;
					this.CfgDBExpressGrid.ViewForeColor = Color.DimGray;
					this.CfgDBExpressCtrlGrid.ViewForeColor = Color.DimGray;
          this.CfgDBCarteamGrid.ViewForeColor = Color.DimGray; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
          this.CfgDBCarteamCtrlGrid.ViewForeColor = Color.DimGray; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
          //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
          this.CfgDBExpress400Grid.ViewForeColor = Color.DimGray;
          this.CfgDBExpress400CtrlGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI18/02/11 - Corretta visualizzazione del 'Background Tasti' nella pagina dei settaggi task DB
          //lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
          lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(152, 153, 199);

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
					this.CfgDBGenericGrid.ViewForeColor = Color.Black;
					this.CfgDBGenericCtrlGrid.ViewForeColor = Color.Black;
					this.CfgDBExpressGrid.ViewForeColor = Color.Black;
					this.CfgDBExpressCtrlGrid.ViewForeColor = Color.Black;
          this.CfgDBCarteamGrid.ViewForeColor = Color.Black; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
          this.CfgDBCarteamCtrlGrid.ViewForeColor = Color.Black; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
          //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
          this.CfgDBExpress400Grid.ViewForeColor = Color.Black;
          this.CfgDBExpress400CtrlGrid.ViewForeColor = Color.Black;
          
					this.modify = true;
					this.modDBGeneric  = false;

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
				this.CfgDBGenericGrid.ViewForeColor = Color.DimGray;
				this.CfgDBGenericCtrlGrid.ViewForeColor = Color.DimGray;
				this.CfgDBExpressGrid.ViewForeColor = Color.DimGray;
				this.CfgDBExpressCtrlGrid.ViewForeColor = Color.DimGray;
        this.CfgDBCarteamGrid.ViewForeColor = Color.DimGray; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
        this.CfgDBCarteamCtrlGrid.ViewForeColor = Color.DimGray; //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
        //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
        this.CfgDBExpress400Grid.ViewForeColor = Color.DimGray;
        this.CfgDBExpress400CtrlGrid.ViewForeColor = Color.DimGray;

        //PMASSARO_FI18/02/11 - Corretta visualizzazione del 'Background Tasti' nella pagina dei settaggi task DB
        //lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
        lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(152, 153, 199);
				this.refreshData();
			}
		}

		private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgDBGenericGrid) || ((PropertyGrid)s).Equals(this.CfgDBGenericCtrlGrid))
				{
					this.modDBGeneric  = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgDBExpressGrid) || ((PropertyGrid)s).Equals(this.CfgDBExpressCtrlGrid))
				{
					this.modDBExpress = true;
				}
        //PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam
        if (((PropertyGrid)s).Equals(this.CfgDBCarteamGrid) || ((PropertyGrid)s).Equals(this.CfgDBCarteamCtrlGrid))
        {
          this.modDBCarteam = true;
        }
        //RFAZZI_FI22/02/2011_2. Aggiunto task per Express400. 
        if (((PropertyGrid)s).Equals(this.CfgDBExpress400Grid) || ((PropertyGrid)s).Equals(this.CfgDBExpress400CtrlGrid))
        {
          this.modDBExpress400 = true;
        }

			}
		}

	}
}

