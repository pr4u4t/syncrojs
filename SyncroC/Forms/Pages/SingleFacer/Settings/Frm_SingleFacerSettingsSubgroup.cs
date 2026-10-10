using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SingleFacer.Settings
{
	public partial class Frm_SingleFacerSettingsSubgroup : SyncroC.Forms.Pages.SingleFacer.Frm_SingleFacerGroup
	{
		public Frm_SingleFacerSettingsSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					pm.pagesHistorian.CurrentPageSingleFacerSettings1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSingleFacerSettings2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSingleFacerSettings3 = this.getRequestCommand(index);
					break;
			}
			base._Show();
		}

		protected override void F2()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SingleFacerSettingsMachine), index);
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SingleFacerSettingsTask), index);
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_SingleFacerSettingsSFStatus), index);
		}

    protected override void F13()
    {
        askForPage(typeof(Frm_SingleFacerSettingsBridgeStatus), index);
    }
	}
}

