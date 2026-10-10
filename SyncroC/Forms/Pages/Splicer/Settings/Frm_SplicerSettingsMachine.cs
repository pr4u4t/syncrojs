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

namespace SyncroC.Forms.Pages.Splicer.Settings
{
	public partial class Frm_SplicerSettingsMachine : SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsSubgroup
	{
        private bool modSplicer = false;
        private bool modify = false;
        
        public Frm_SplicerSettingsMachine()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified Splicer.
		/// </summary>
		/// <param name="index">Index of the splicer represented on the current instance of this Form</param>
		public Frm_SplicerSettingsMachine(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7))
				Logger.LogError(this, "Frm_SplicerSettingsMachine - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\", \"4\", \"5\", \"6\", \"7\" values are allowed.");
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

				//Splicer
				this.CfgSplicerGrid.ViewForeColor = Color.Black;
				this.modSplicer = false;

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
					//Splicer
					if (this.modSplicer)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.SPLICER + index.ToString()+ "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.Splicer)this.CfgSplicerGrid.SelectedObject).SerializeProperties() + ")");
					}
					this.CfgSplicerGrid.ViewForeColor = Color.DimGray;
					this.modSplicer = false;

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
				//SPLICER
				this.CfgSplicerGrid.ViewForeColor = Color.DimGray;
				this.modSplicer = false;

				this.modify = false;
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				this.refreshData();
			}
		}

		private void Frm_SplicerSettingsMachine_Load(object sender, EventArgs e)
		{
			this.refreshData();
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			switch (index)
			{
				case 1:
					this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer1Config.CreateCopy();
					break;
				case 2:
          this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer2Config.CreateCopy();
					break;
        case 3:
          this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer3Config.CreateCopy();
          break;
        case 4:
          this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer4Config.CreateCopy();
          break;
        case 5:
					this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer5Config.CreateCopy();
           break;
        case 6:
          this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer6Config.CreateCopy();
          break;
        case 7:
          this.CfgSplicerGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Splicer)cfg.Splicer7Config.CreateCopy();
          break;
      }
		}

		private void CfgSplicerGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgSplicerGrid))
				{
					this.modSplicer = true;
				}
			}
		}
	}
}

