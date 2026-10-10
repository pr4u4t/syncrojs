using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.GlueUnit.Settings
{
	public partial class Frm_GlueUnitSettingsSubgroup : SyncroC.Forms.Pages.GlueUnit.Frm_GlueUnitGroup
	{
		public Frm_GlueUnitSettingsSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageGlueUnitSettings = this.getRequestCommand();
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
			askForPage(typeof(Frm_GlueUnitSettingsMachine));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_GlueUnitSettingsTask));
		}
	}
}

