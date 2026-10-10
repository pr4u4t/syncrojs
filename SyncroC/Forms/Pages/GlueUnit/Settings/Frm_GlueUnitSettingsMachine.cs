using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;


namespace SyncroC.Forms.Pages.GlueUnit.Settings
{
	public partial class Frm_GlueUnitSettingsMachine : SyncroC.Forms.Pages.GlueUnit.Settings.Frm_GlueUnitSettingsSubgroup
	{
		private bool modGlueUnit = false;
		private bool modify = false;

		public Frm_GlueUnitSettingsMachine()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
		}

		private void CfgGlueUnitGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgGlueUnitGrid))
				{
					this.modGlueUnit = true;
				}
			}
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.CfgGlueUnitGrid.ViewForeColor = Color.Black;
				this.modify = true;
				this.modGlueUnit = false;
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
					if (this.modGlueUnit)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_GlueUnitSettingsMachine)) + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.GlueUnit)this.CfgGlueUnitGrid.SelectedObject).SerializeProperties() + ")");
					}
					this.CfgGlueUnitGrid.ViewForeColor = Color.DimGray;
					this.modGlueUnit = false;
					this.modify = false;
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
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
				this.CfgGlueUnitGrid.ViewForeColor = Color.DimGray;
				this.modGlueUnit = false;
				this.modify = false;
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				this.refreshData();
			}
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.CfgGlueUnitGrid.SelectedObject = (SyncroC.DataBase.LineConfig.GlueUnit)cfg.GlueUnitConfig.CreateCopy();
		}

		private void Frm_GlueUnitSettingsMachine_Load(object sender, EventArgs e)
		{
			this.refreshData();
		}

	}
}

