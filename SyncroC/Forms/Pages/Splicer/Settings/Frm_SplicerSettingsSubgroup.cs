using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Splicer.Settings
{
	public partial class Frm_SplicerSettingsSubgroup : SyncroC.Forms.Pages.Splicer.Frm_SplicerGroup
	{
		public Frm_SplicerSettingsSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					pm.pagesHistorian.CurrentPageSplicerSettings1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSplicerSettings2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSplicerSettings3 = this.getRequestCommand(index);
					break;
				case 4:
					pm.pagesHistorian.CurrentPageSplicerSettings4 = this.getRequestCommand(index);
					break;
				case 5:
					pm.pagesHistorian.CurrentPageSplicerSettings5 = this.getRequestCommand(index);
					break;
				case 6:
					pm.pagesHistorian.CurrentPageSplicerSettings6 = this.getRequestCommand(index);
					break;
				case 7:
					pm.pagesHistorian.CurrentPageSplicerSettings7 = this.getRequestCommand(index);
					break;
			}
			base._Show();
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

		protected override void F2()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SplicerSettingsMachine), index);
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SplicerSettingsTask), index);
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_SplicerSettingsStatus), index);
		}
	}
}

