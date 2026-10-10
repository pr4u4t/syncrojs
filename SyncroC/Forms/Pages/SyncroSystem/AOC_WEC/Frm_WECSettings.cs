using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.AOC_WEC
{
	public partial class Frm_WECSettings : SyncroC.Forms.Pages.SyncroSystem.AOC_WEC.Frm_AOC_WECSubGroup
	{
		private bool modWEC = false;
		private bool modify = false;

		public Frm_WECSettings()
		{
			InitializeComponent();
		}

		protected override void F12()
		{
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.CfgWECGrid.ViewForeColor = Color.Black;
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.Black;
				this.modify = true;
				this.modWEC = false;
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
					if (this.modWEC)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.WEC + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.WEC)this.CfgWECGrid.SelectedObject).SerializeProperties() + ")");
					}

					this.CfgWECGrid.ViewForeColor = Color.DimGray;
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
					this.modWEC = false;
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
				this.CfgWECGrid.ViewForeColor = Color.DimGray;
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				this.modWEC = false;
				this.modify = false;
				this.refreshData();
			}
		}

		private void Frm_WECSettings_Load(object sender, EventArgs e)
		{
			this.refreshData();
			this.CfgWECGrid.ExpandAllGridItems();
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.CfgWECGrid.SelectedObject = (SyncroC.DataBase.LineConfig.WEC)cfg.WECConfig.CreateCopy();
		}

		private void CfgWECGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgWECGrid))
				{
					this.modWEC = true;
				}
			}
		}
	}
}

