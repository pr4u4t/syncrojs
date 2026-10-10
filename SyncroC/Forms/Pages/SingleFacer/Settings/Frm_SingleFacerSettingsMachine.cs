using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase.LineConfig;

namespace SyncroC.Forms.Pages.SingleFacer.Settings
{
	public partial class Frm_SingleFacerSettingsMachine : SyncroC.Forms.Pages.SingleFacer.Settings.Frm_SingleFacerSettingsSubgroup
	{
		private bool modBridge = false;
		private bool modSingleFacer = false;
		private bool modify = false;

		public Frm_SingleFacerSettingsMachine()
		{
			InitializeComponent();

		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerSettingsMachine(int index)
			: this()
		{
			if (! (index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerSettingsMachine - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
		}

		protected override void F10()
		{
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.modify = true;

				//BRIDGE
				this.CfgBridgeGrid.ViewForeColor = Color.Black;
				this.modBridge = false;
				//SINGLE FACER
				this.CfgSingleFacerGrid.ViewForeColor = Color.Black;
				this.modSingleFacer = false;

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
					//BRIDGE
					if (this.modBridge)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.BRIDGE + index.ToString() + ".B_" + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.Bridge)this.CfgBridgeGrid.SelectedObject).SerializeProperties() + ")");
					}
					this.CfgBridgeGrid.ViewForeColor = Color.DimGray;
					this.modBridge = false;

					//SINGLE FACER
					if (this.modSingleFacer)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.SINGLE_FACER + index.ToString()+ "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.SingleFacer)this.CfgSingleFacerGrid.SelectedObject).SerializeProperties() + ")");
					}
					this.CfgSingleFacerGrid.ViewForeColor = Color.DimGray;
					this.modSingleFacer = false;

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
				//BRIDGE
				this.CfgBridgeGrid.ViewForeColor = Color.DimGray;
				this.modBridge = false;

				//SINGLE FACER
				this.CfgSingleFacerGrid.ViewForeColor = Color.DimGray;
				this.modSingleFacer = false;

				this.modify = false;
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				this.refreshData();
			}
		}

		private void Frm_SingleFacerSettingsMachine_Load(object sender, EventArgs e)
		{
			this.refreshData();
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			switch (index)
			{
				case 1:
					this.CfgBridgeGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Bridge)cfg.Bridge1Config.CreateCopy();
					this.CfgSingleFacerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.SingleFacer)cfg.SingleFacer1Config.CreateCopy();
					break;
				case 2:
					this.CfgBridgeGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Bridge)cfg.Bridge2Config.CreateCopy();
					this.CfgSingleFacerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.SingleFacer)cfg.SingleFacer2Config.CreateCopy();
					break;
				case 3:
					this.CfgBridgeGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Bridge)cfg.Bridge3Config.CreateCopy();
					this.CfgSingleFacerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.SingleFacer)cfg.SingleFacer3Config.CreateCopy();
					break;
			}
		}

		private void CfgBridgerGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgBridgeGrid))
				{
					this.modBridge = true;
				}
			}
		}

		private void CfgSingleFacerGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgSingleFacerGrid))
				{
					this.modSingleFacer = true;
				}
			}
		}

	}
}

