using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.PRINTER
{
    public partial class Frm_PRINTERSSettings : SyncroC.Forms.Pages.SyncroSystem.PRINTER.Frm_PRINTERSSubGroup
    {
			private bool modPRINTERS = false;
			private bool modify = false;

      public Frm_PRINTERSSettings()
      {
          InitializeComponent();
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
					this.CfgPRINTERSGrid.ViewForeColor = Color.Black;
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.Black;
					this.modify = true;
					this.modPRINTERS = false;
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
						if (this.modPRINTERS)
						{
							CommunicationManager cm = CommunicationManager.GetCommunicationManager();
							cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.PRINTERS + "." + Command.CONF_CONFIG + "("
								+ ((SyncroC.DataBase.LineConfig.PRINTERS)this.CfgPRINTERSGrid.SelectedObject).SerializeProperties() + ")");
						}

						this.CfgPRINTERSGrid.ViewForeColor = Color.DimGray;
						lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
						this.modPRINTERS = false;
						this.modify = false;
						this.refreshData();
					}
				}
				else
				{
					PageManager.getPageManager()._SetMessage("§SYCC_11");
				}
			}

			protected override void Escape()
			{
				//Esc without saving
				if (this.modify)
				{
					this.CfgPRINTERSGrid.ViewForeColor = Color.DimGray;
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
					this.modPRINTERS = false;
					this.modify = false;
					this.refreshData();
				}
			}

			private void Frm_PRINTERSSettings_Load(object sender, EventArgs e)
			{
				this.refreshData();
				this.CfgPRINTERSGrid.ExpandAllGridItems();
			}

			public override void refreshData()
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				this.CfgPRINTERSGrid.SelectedObject = (SyncroC.DataBase.LineConfig.PRINTERS)cfg.PRINTERSConfig.CreateCopy();
			}

			private void CfgPRINTERSGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
			{
				if (!this.modify)
					this.refreshData();
				else
				{
					LineConfig cfg = DBManager.getDBManager().lineConfig;
					if (((PropertyGrid)s).Equals(this.CfgPRINTERSGrid))
					{
						this.modPRINTERS = true;
					}
				}
			}

		}

}

