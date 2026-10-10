using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.PreHeaterTern.Settings
{
	public partial class Frm_PreHeaterTernSettingsSubgroup : SyncroC.Forms.Pages.PreHeaterTern.Frm_PreHeaterTernGroup
	{
		public Frm_PreHeaterTernSettingsSubgroup()
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
			pm.pagesHistorian.CurrentPagePreHeaterTernSettings = this.getRequestCommand();
			base._Show();
		}

		protected override void F2()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_PreHeaterTernSettingsMachine));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_PreHeaterTernSettingsTask));
		}
	}
}

