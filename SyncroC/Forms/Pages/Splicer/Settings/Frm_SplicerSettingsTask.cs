using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;


namespace SyncroC.Forms.Pages.Splicer.Settings
{
	public partial class Frm_SplicerSettingsTask : SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsSubgroup
	{

		private bool modify = false;
		private bool modSplicer = false;
		private SyncroConfig cfg;

		public Frm_SplicerSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			if (index == 1)
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric1Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL1Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL1Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL1Setting.CreateCopy();
      }
			else if (index == 2)
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric2Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL2Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL2Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL2Setting.CreateCopy();
      }
			else if (index == 3)
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric3Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL3Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL3Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL3Setting.CreateCopy();
      }
			else if (index == 4)
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric4Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL4Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL4Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL4Setting.CreateCopy();
      }
			else if (index == 5)
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric5Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL5Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL5Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL5Setting.CreateCopy();
      }
			else if (index == 6)
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric6Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL6Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL6Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL6Setting.CreateCopy();
      }
			else 
			{
				this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericSettings)cfg.SplicerGeneric7Setting.CreateCopy();
				this.CfgSplicerCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerGenericCTRLSettings)cfg.SplicerGenericCTRL7Setting.CreateCopy();
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SplicerFosberLNK400CTRLSettings)cfg.SplicerFosberLNK400CTRL7Setting.CreateCopy();
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollStandFosberPRT400CTRLSettings)cfg.RollStandFosberPRT400CTRL7Setting.CreateCopy();
      }
		}
		
		/// <summary>
		/// Builder. Build an istance of this page for the specified Splicer.
		/// </summary>
		/// <param name="index">Index of the splicer represented on the current instance of this Form</param>
		public Frm_SplicerSettingsTask(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7))
				Logger.LogError(this, "Frm_SplicerSettingsTask - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\", \"4\", \"5\", \"6\", \"7\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
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
					if (this.modSplicer)
					{
						if (index == 1)
						{
							cfg.SplicerGeneric1Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric1Setting.Save();

							cfg.SplicerGenericCTRL1Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL1Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL1Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL1Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL1Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL1Setting.Save();

						}
						else if ( index == 2 )
						{
							cfg.SplicerGeneric2Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric2Setting.Save();

							cfg.SplicerGenericCTRL2Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL2Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL2Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL2Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL2Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL2Setting.Save();
            }
						else if ( index == 3 )
						{
							cfg.SplicerGeneric3Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric3Setting.Save();

							cfg.SplicerGenericCTRL3Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL3Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL3Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL3Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL3Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL3Setting.Save();
						}
						else if ( index == 4 )
						{
							cfg.SplicerGeneric4Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric4Setting.Save();

							cfg.SplicerGenericCTRL4Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL4Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL4Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL4Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL4Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL4Setting.Save();
						}
						else if ( index == 5 )
						{
							cfg.SplicerGeneric5Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric5Setting.Save();

							cfg.SplicerGenericCTRL5Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL5Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL5Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL5Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL5Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL5Setting.Save();
						}
						else if ( index == 6 )
						{
							cfg.SplicerGeneric6Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric6Setting.Save();

							cfg.SplicerGenericCTRL6Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL6Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL6Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL6Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL6Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL6Setting.Save();
						}
						else 
						{
							cfg.SplicerGeneric7Setting = (SplicerGenericSettings)this.CfgSplicerGrid.SelectedObject;
							cfg.SplicerGeneric7Setting.Save();

							cfg.SplicerGenericCTRL7Setting = (SplicerGenericCTRLSettings)this.CfgSplicerCTRLGrid.SelectedObject;
							cfg.SplicerGenericCTRL7Setting.Save();

							//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
							cfg.SplicerFosberLNK400CTRL7Setting = (SplicerFosberLNK400CTRLSettings)this.CfgSplicerFosberLNK400CTRLGrid.SelectedObject;
							cfg.SplicerFosberLNK400CTRL7Setting.Save();

              //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
              cfg.RollStandFosberPRT400CTRL7Setting = (RollStandFosberPRT400CTRLSettings)this.CfgRollStandFosberPRT400CTRLGrid.SelectedObject;
              cfg.RollStandFosberPRT400CTRL7Setting.Save();
						}
					}

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgSplicerGrid.ViewForeColor = Color.DimGray;
					this.CfgSplicerCTRLGrid.ViewForeColor = Color.DimGray;
					//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
					this.CfgSplicerFosberLNK400CTRLGrid.ViewForeColor = Color.DimGray;

          //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
          this.CfgRollStandFosberPRT400CTRLGrid.ViewForeColor = Color.DimGray;

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
					this.CfgSplicerGrid.ViewForeColor = Color.Black;
					this.CfgSplicerCTRLGrid.ViewForeColor = Color.Black;
					//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
					this.CfgSplicerFosberLNK400CTRLGrid.ViewForeColor = Color.Black;

          //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
          this.CfgRollStandFosberPRT400CTRLGrid.ViewForeColor = Color.Black;

					this.modify = true;
					this.modSplicer = false;

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
				this.CfgSplicerGrid.ViewForeColor = Color.DimGray;
				this.CfgSplicerCTRLGrid.ViewForeColor = Color.DimGray;
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
				this.CfgSplicerFosberLNK400CTRLGrid.ViewForeColor = Color.DimGray;
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        this.CfgRollStandFosberPRT400CTRLGrid.ViewForeColor = Color.DimGray;

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
				//RFAZZI_FI23/02/2010_1.Aggiunto protocollo software con LNK400. 
        //RFAZZI_FI12/01/2011_1. Aggiunta comunicazione con RollStand Fosber PRT400. 
        if (((PropertyGrid)s).Equals(this.CfgSplicerGrid) 
              || ((PropertyGrid)s).Equals(this.CfgSplicerCTRLGrid)
							|| ((PropertyGrid)s).Equals(this.CfgSplicerFosberLNK400CTRLGrid)
              || ((PropertyGrid)s).Equals(this.CfgRollStandFosberPRT400CTRLGrid)
          )
				{
					this.modSplicer = true;
				}
			}
		}

	}
}

