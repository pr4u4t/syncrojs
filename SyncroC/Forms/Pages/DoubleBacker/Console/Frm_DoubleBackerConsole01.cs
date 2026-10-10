using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.DoubleBacker.Console
{
	public partial class Frm_DoubleBackerConsole01 : Frm_DoubleBackerConsoleSubgroup
	{
		public Frm_DoubleBackerConsole01()
		{
			InitializeComponent();

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

		private void Frm_DoubleBackerConsole01_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_DoubleBackerConsole01)) + "." + ((Control)sender).Name + "()");
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
			this.SendNextP1Order.Alarmed = isError;
		}
	}
}