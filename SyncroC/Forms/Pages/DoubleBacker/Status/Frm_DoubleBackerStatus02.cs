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

namespace SyncroC.Forms.Pages.DoubleBacker.Status
{
  public partial class Frm_DoubleBackerStatus02 : SyncroC.Forms.Pages.DoubleBacker.Status.Frm_DoubleBackerStatusSubgroup
	{
		private bool showDebugOptions = false;

		public Frm_DoubleBackerStatus02()
		{
			InitializeComponent();
			this._setVisibility(false, false, false, false, false, false,false);

			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
        this.AutoManualColdSection.IsButton = false;
				this.AutoManualScrewJacks.IsButton = false;
				this.AutoManualSteamPressureGroup1.IsButton = false;
				this.AutoManualSteamPressureGroup2.IsButton = false;
				this.AutoManualSteamPressureGroup3.IsButton = false;
        this.AutoManualSteamPressureGroup0.IsButton = false;

				this.LocalRemoteColdSection.IsButton = false;
				this.LocalRemoteScrewJacks.IsButton = false;
				this.LocalRemoteSteamPressureGroup1.IsButton = false;
				this.LocalRemoteSteamPressureGroup2.IsButton = false;
				this.LocalRemoteSteamPressureGroup3.IsButton = false;
        this.LocalRemoteSteamPressureGroup0.IsButton = false;

				this.ColdSection.LowLevelEnabled = false;
				this.ScrewJack1.LowLevelEnabled = false;
				this.ScrewJack2.LowLevelEnabled = false;
				this.ScrewJack3.LowLevelEnabled = false;
				this.ScrewJack4.LowLevelEnabled = false;
				this.ScrewJacks.LowLevelEnabled = false;
				this.SteamPressureGroup1.LowLevelEnabled = false;
				this.SteamPressureGroup2.LowLevelEnabled = false;
				this.SteamPressureGroup3.LowLevelEnabled = false;
        this.SteamPressureGroup0.LowLevelEnabled = false;
			}
		}

		protected override void ControlD()
		{
			showDebugOptions = !showDebugOptions;

			this._setVisibility(true, true, true, true, true, true,true);

      //RFAZZI_FI09/07/2010_3. Rimossa visualizzazione del pulsante di Toggle Sezione Fredda Auto/Man visto che sull'Express nn è possibile farlo
			//this.AutoManualColdSection.Visible = showDebugOptions;
      this.AutoManualScrewJacks.Visible = showDebugOptions;
			this.AutoManualSteamPressureGroup1.Visible = showDebugOptions;
			this.AutoManualSteamPressureGroup2.Visible = showDebugOptions;
			this.AutoManualSteamPressureGroup3.Visible = showDebugOptions;
      this.AutoManualSteamPressureGroup0.Visible = showDebugOptions;

      //RFAZZI_FI12/11/2010_1. Rimossa visualizzazione del pulsante di Toggle Sezione Fredda Loc/Rem in attesa di definire se deve essere controllato da Syncro
      //this.LocalRemoteColdSection.Visible = showDebugOptions;
      this.LocalRemoteScrewJacks.Visible = showDebugOptions;
			this.LocalRemoteSteamPressureGroup1.Visible = showDebugOptions;
			this.LocalRemoteSteamPressureGroup2.Visible = showDebugOptions;
			this.LocalRemoteSteamPressureGroup3.Visible = showDebugOptions;
      this.LocalRemoteSteamPressureGroup0.Visible = showDebugOptions;

			this.ColdSection.Visible = showDebugOptions;
			this.ScrewJack1.Visible = showDebugOptions;
			this.ScrewJack2.Visible = showDebugOptions;
			this.ScrewJack3.Visible = showDebugOptions;
			this.ScrewJack4.Visible = showDebugOptions;
			this.ScrewJacks.Visible = showDebugOptions;
			this.SteamPressureGroup1.Visible = showDebugOptions;
			this.SteamPressureGroup2.Visible = showDebugOptions;
			this.SteamPressureGroup3.Visible = showDebugOptions;
      this.SteamPressureGroup0.Visible = showDebugOptions;

			this.tnud_steamRequestGroup1_UPdb.Visible = showDebugOptions;
			this.tnud_steamRequestGroup2_UPdb.Visible = showDebugOptions;
			this.tnud_steamRequestGroup3_UPdb.Visible = showDebugOptions;
      this.tnud_steamRequestGroup0_UPdb.Visible = showDebugOptions;

			this.tnud_steamValueGroup1_UPdb.Visible = showDebugOptions;
			this.tnud_steamValueGroup2_UPdb.Visible = showDebugOptions;
			this.tnud_steamValueGroup3_UPdb.Visible = showDebugOptions;
      this.tnud_steamValueGroup0_UPdb.Visible = showDebugOptions;
		}

		protected override void F11()
		{ }

		private void OnStateButtonKitDown(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus02)) + "." + ((Control)sender).Name + "." + Command.TOGGLE_WITH_TIMEOUT);
		}

		private void OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus02)) + "." +
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" +
				args.Frequency + ")");
		}

    private void _setVisibility(bool theSteamPressureGroup1, bool theSteamPressureGroup2, bool theSteamPressureGroup3, bool theSteamPressureGroup4, bool theColdSection, bool theScrewJacks, bool theMaxValueScrewJack)
		{
			if (showDebugOptions)
			{
				theSteamPressureGroup1 = true;
				theSteamPressureGroup2 = true;
				theSteamPressureGroup3 = true;
				theColdSection = true;
				theScrewJacks = true;
        theMaxValueScrewJack = true;
			}

			//SteamPressureGroup1
			this.pnl_Group1.Visible = theSteamPressureGroup1;
			//SteamPressureGroup2
			this.pnl_Group2.Visible = theSteamPressureGroup2;
			//SteamPressureGroup3
			this.pnl_Group3.Visible = theSteamPressureGroup3;
      //SteamPressureGroup4
      this.pnl_Group0.Visible = theSteamPressureGroup4;
			//ColdSection
			this.pnl_ColdSection.Visible = theColdSection;
			//ScrewJacks
			this.pnl_ScrewJacks.Visible = theScrewJacks;

      this.lbl_MaxValScrewJack1.Visible = this.lbl_MaxValScrewJack2.Visible = this.lbl_MaxValScrewJack3.Visible = this.lbl_MaxValScrewJack4.Visible = theMaxValueScrewJack;

      if ( theMaxValueScrewJack )    
        this.ScrewJack1.Height = this.ScrewJack2.Height = this.ScrewJack3.Height = this.ScrewJack4.Height = 130;

      if (this.AutoManualSteamPressureGroup1.Auto)
      {
        this.tnud_steamRequestGroup1_UPdb.InternalUnitMeasure = UnitMeasure.DeciBar;
        this.tnud_steamRequestGroup1_UPdb.UnitMeasure = UnitMeasure.DeciBar;
      }
      else
      {
        this.tnud_steamRequestGroup1_UPdb.InternalUnitMeasure = UnitMeasure.Percentage;
        this.tnud_steamRequestGroup1_UPdb.UnitMeasure = UnitMeasure.Percentage;
      }

      if (this.AutoManualSteamPressureGroup2.Auto)
      {
        this.tnud_steamRequestGroup2_UPdb.InternalUnitMeasure = UnitMeasure.DeciBar;
        this.tnud_steamRequestGroup2_UPdb.UnitMeasure = UnitMeasure.DeciBar;
      }
      else
      {
        this.tnud_steamRequestGroup2_UPdb.InternalUnitMeasure = UnitMeasure.Percentage;
        this.tnud_steamRequestGroup2_UPdb.UnitMeasure = UnitMeasure.Percentage;
      }

      if (this.AutoManualSteamPressureGroup3.Auto)
      {
        this.tnud_steamRequestGroup3_UPdb.InternalUnitMeasure = UnitMeasure.DeciBar;
        this.tnud_steamRequestGroup3_UPdb.UnitMeasure = UnitMeasure.DeciBar;
      }
      else
      {
        this.tnud_steamRequestGroup3_UPdb.InternalUnitMeasure = UnitMeasure.Percentage;
        this.tnud_steamRequestGroup3_UPdb.UnitMeasure = UnitMeasure.Percentage;
      }

      if (this.AutoManualSteamPressureGroup0.Auto)
      {
        this.tnud_steamRequestGroup0_UPdb.InternalUnitMeasure = UnitMeasure.DeciBar;
        this.tnud_steamRequestGroup0_UPdb.UnitMeasure = UnitMeasure.DeciBar;
      }
      else
      {
        this.tnud_steamRequestGroup0_UPdb.InternalUnitMeasure = UnitMeasure.Percentage;
        this.tnud_steamRequestGroup0_UPdb.UnitMeasure = UnitMeasure.Percentage;
      }

			if (!this.DesignMode)
			{
				if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
				{
					this.LocalRemoteScrewJacks.Visible = false;
					this.LocalRemoteColdSection.Visible = false;
					this.LocalRemoteSteamPressureGroup1.Visible = false;
					this.LocalRemoteSteamPressureGroup2.Visible = false;
					this.LocalRemoteSteamPressureGroup3.Visible = false;
          this.LocalRemoteSteamPressureGroup0.Visible = false;
				}
			}
		}

		public override void CorrugatorDirectionLeft()
		{
			//DoubleBacker Image
			this.pct_DoubleBacker.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.pct_DoubleBacker.Location.X;
			int rightLimit = this.pct_DoubleBacker.Location.X + this.pct_DoubleBacker.Width;

			//ScrewJacks & ColdSection
			HorizontalLocationMirror(this.pnl_ScrewJacks, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_ColdSection, leftLimit, rightLimit);

			leftLimit = this.pct_DoubleBacker.Location.X;
			rightLimit = this.pct_DoubleBacker.Location.X + this.pct_DoubleBacker.Width;

      //Group1 & Group2 & Group3 & Group4
			HorizontalLocationMirror(this.pnl_Group1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_Group2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_Group3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pnl_Group0, leftLimit, rightLimit);

			leftLimit = this.tlt_ScrewJack1.Location.X;
			rightLimit = this.tlt_ScrewJack4.Location.X + this.tlt_ScrewJack4.Width;

			HorizontalLocationMirror(this.tlt_ScrewJack1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tlt_ScrewJack2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tlt_ScrewJack3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tlt_ScrewJack4, leftLimit, rightLimit);

			leftLimit = this.ScrewJack1.Location.X;
			rightLimit = this.ScrewJack4.Location.X + this.ScrewJack4.Width;

			HorizontalLocationMirror(this.ScrewJack1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ScrewJack2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ScrewJack3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ScrewJack4, leftLimit, rightLimit);
		}
	}
}