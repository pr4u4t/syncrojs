using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.Controls.IncDecButton;

namespace SyncroC.Forms.Pages.SingleFacer.Console
{
	public partial class Frm_SingleFacerConsole01 : SyncroC.Forms.Pages.SingleFacer.Console.Frm_SingleFacerConsoleSubgroup
	{
		public Frm_SingleFacerConsole01()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerConsole01(int index)
			: this()
		{
			if (! (index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerConsole01 - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();

			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
			{
				this.SendCurrentOrder.IsButton = false;
				this.SendNextOrder.IsButton = false;
				this.SendNextP1Order.IsButton = false;
			}
		}

		protected override void F10()
		{
		}

		private void Frm_SingleFacerConsole01_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SingleFacerConsole01), index) + "." + ((Control)sender).Name + "()");
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
			this.SendNextP1Order.Alarmed = isError;
		}
	}
}

