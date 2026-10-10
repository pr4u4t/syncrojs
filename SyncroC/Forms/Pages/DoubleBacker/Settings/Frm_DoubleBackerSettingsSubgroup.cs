using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.DoubleBacker.Settings
{
	public partial class Frm_DoubleBackerSettingsSubgroup : SyncroC.Forms.Pages.DoubleBacker.Frm_DoubleBackerGroup
	{
		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
		}

		public Frm_DoubleBackerSettingsSubgroup()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_DoubleBackerSettingsMachine));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_DoubleBackerSettingsTask));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_DoubleBackerSettingsStatus));
		}
	}
}

