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


namespace SyncroC.Forms.Pages.PreHeaterTern.Settings
{
	public partial class Frm_PreHeaterTernSettingsMachine : SyncroC.Forms.Pages.PreHeaterTern.Settings.Frm_PreHeaterTernSettingsSubgroup
	{
		private bool modPreHeatersTern = false;
		private bool modify = false;

		public Frm_PreHeaterTernSettingsMachine()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
		}

		private void CfgPreHeatersTernGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgPreHeatersTernGrid))
				{
					this.modPreHeatersTern = true;
				}
			}
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.CfgPreHeatersTernGrid.ViewForeColor = Color.Black;
				this.modify = true;
				this.modPreHeatersTern = false;
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
					if (this.modPreHeatersTern)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_PreHeaterTernSettingsMachine)) + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.PreHeatersTern)this.CfgPreHeatersTernGrid.SelectedObject).SerializeProperties() + ")");
					}
					this.CfgPreHeatersTernGrid.ViewForeColor = Color.DimGray;
					this.modPreHeatersTern = false;
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
				this.CfgPreHeatersTernGrid.ViewForeColor = Color.DimGray;
				this.modPreHeatersTern = false;
				this.modify = false;
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				this.refreshData();
			}
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.CfgPreHeatersTernGrid.SelectedObject = (SyncroC.DataBase.LineConfig.PreHeatersTern)cfg.PreHeatersTernConfig.CreateCopy();
		}

		private void Frm_PreHeaterTernSettingsMachine_Load(object sender, EventArgs e)
		{
			this.refreshData();
		}
	}
}

