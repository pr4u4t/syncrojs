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

namespace SyncroC.Forms.Pages.SingleFacer.Status
{
	public partial class Frm_SingleFacerStatus02 : SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatusSubgroup
	{
		private bool showDebugOptions = false;

		public Frm_SingleFacerStatus02()
		{
			InitializeComponent();

			//ABello_FI10/07/2007_2 I pulsanti di Loc/Rem sulle pagine operatore si vedevano anche se il PRC era disabilitato
			if (!this.DesignMode)
			{
				if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
				{
          this.LocalRemoteSfMainSteam.Visible = false;
					this.LocalRemoteSteamShower.Visible = false;
				}
			}
      //ABELLO_FI20/04/2010_1 Tolta dalla pagina del Single Facer Stato 2 la gestione low level del Rullo Pressore.
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
				this.Torpress.LowLevelEnabled = false;
				this.SfMainSteam.LowLevelEnabled = false;
				this.SteamShower.LowLevelEnabled = false;

				this.AutoManualMainSteam.IsButton = false;
				this.AutoManualSteamShower.IsButton = false;
				this.AutoManualTorpress.IsButton = false;
        this.stb_steamShowerOnOff.IsButton = false;
        this.LocalRemoteSfMainSteam.IsButton = false;
				this.LocalRemoteSteamShower.IsButton = false;
			}
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerStatus02(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerStatus02 - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
		}

		protected override void ControlD()
		{
			showDebugOptions = !showDebugOptions;

			this.pnl_pressureRoll.Visible = showDebugOptions;
			this.pnl_torpress.Visible = showDebugOptions;
			this.pnl_MainSteam.Visible = showDebugOptions;
			this.pnl_steamShower.Visible = showDebugOptions;

			this.tnud_requestedMainSteamPressure_UPdb.Visible = showDebugOptions;
      this.tnud_feedBackMainSteamPressure_UPdb.Visible = showDebugOptions;
			this.tnud_pressRollNoOp_UPerc.Visible = showDebugOptions;
			this.tnud_pressRollNoOp_UPKg_cm.Visible = showDebugOptions;
			this.tnud_pressRollOp_UPerc.Visible = showDebugOptions;
			this.tnud_pressRollOp_UPKg_cm.Visible = showDebugOptions;
			this.tnud_steamShower_UPerc.Visible = showDebugOptions;
			this.tnud_torpressNoOp_UPdb.Visible = showDebugOptions;
			this.tnud_torpressOp_UPdb.Visible = showDebugOptions;
			this.tnud_torpressPressureRoll_UPdb.Visible = showDebugOptions;
      this.LocalRemoteSfMainSteam.Visible = showDebugOptions;
			this.LocalRemoteSteamShower.Visible = showDebugOptions;
		}

		protected override void F11()
		{
		}

		private void OnStateButtonKitDown(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), index) + "." + ((Control)sender).Name + "." + Command.TOGGLE_WITH_TIMEOUT);
		}

		private void OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), index) + "." +
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" +
				args.Frequency + ")");
		}

		private void _CommunicationError(bool isError)
		{
			//TODO: mettere qualcosa che fà capire che c'è errore di comunicazione
		}

		private void Frm_SingleFacerStatus02_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		public override void CorrugatorDirectionLeft()
		{
			//Single Facer Picture
			this.pct_machine.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.pnl_pressureRoll.Location.X;
			int rightLimit = this.pnl_torpress.Location.X + this.pnl_torpress.Width;

			HorizontalLocationMirror(this.pnl_pressureRoll, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_torpress, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_MainSteam, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_steamShower, leftLimit, rightLimit);
    }

    private void stb_steamShowerOnOff_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), index) + "." + "OnOffSteamShower" + "." + Command.TOGGLE_WITH_TIMEOUT);
    }
	}
}

