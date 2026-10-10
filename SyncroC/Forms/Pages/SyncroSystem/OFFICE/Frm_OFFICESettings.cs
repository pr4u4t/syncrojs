using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.OFFICE
{
	public partial class Frm_OFFICESettings : SyncroC.Forms.Pages.SyncroSystem.OFFICE.Frm_OFFICESubGroup 
	{

		private bool modOFFICE = false;
		private bool modify = false;

		public Frm_OFFICESettings()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				//Enable Settings modifications
				this.CfgOFFICEGrid.ViewForeColor = Color.Black;
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.Black;
				this.modify = true;
				this.modOFFICE = false;
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
					if (this.modOFFICE)
					{
						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.OFFICE  + "." + Command.CONF_CONFIG + "("
							+ ((SyncroC.DataBase.LineConfig.OFFICE)this.CfgOFFICEGrid.SelectedObject).SerializeProperties() + ")");
					}

					this.CfgOFFICEGrid.ViewForeColor = Color.DimGray;
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
					this.modOFFICE = false;
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
				this.CfgOFFICEGrid.ViewForeColor = Color.DimGray;
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				this.modOFFICE = false;
				this.modify = false;
				this.refreshData();
			}
		}

		private void Frm_OFFICESettings_Load(object sender, EventArgs e)
		{
			this.refreshData();
			this.CfgOFFICEGrid.ExpandAllGridItems();
		}

		public override void refreshData()
		{
			LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.CfgOFFICEGrid.SelectedObject = (SyncroC.DataBase.LineConfig.OFFICE)cfg.OFFICEConfig.CreateCopy();
		}

		private void CfgOFFICEGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.CfgOFFICEGrid))
				{
					this.modOFFICE = true;
				}
			}
		}
	}
}