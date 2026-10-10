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

namespace SyncroC.Forms.Pages.DoubleBacker.Settings
{
	public partial class Frm_DoubleBackerSettingsMachine : SyncroC.Forms.Pages.DoubleBacker.Settings.Frm_DoubleBackerSettingsSubgroup
	{
		private bool modDoubleBacker = false;
		private bool modify = false;

		public Frm_DoubleBackerSettingsMachine()
		{
			InitializeComponent();
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.CfgDoubleBackerGrid.ViewForeColor = Color.Black;
				this.modify = true;
				this.modDoubleBacker = false;
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
					if (this.modDoubleBacker)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_DoubleBackerSettingsMachine)) + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.DoubleBacker)this.CfgDoubleBackerGrid.SelectedObject).SerializeProperties() + ")");
					}
					this.CfgDoubleBackerGrid.ViewForeColor = Color.DimGray;
					this.modDoubleBacker = false;
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
				this.CfgDoubleBackerGrid.ViewForeColor = Color.DimGray;
				this.modDoubleBacker = false;
				this.modify = false;
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				this.refreshData();
			}
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.CfgDoubleBackerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.DoubleBacker)cfg.DoubleBackerConfig.CreateCopy();
		}

		protected override void F10()
		{
		}

		private void Frm_DoubleBackerSettingsMachine_Load(object sender, EventArgs e)
		{
			this.refreshData();
		}

		private void CfgDoubleBackerGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgDoubleBackerGrid))
				{
					this.modDoubleBacker = true;
				}
			}
		}
	}
}

