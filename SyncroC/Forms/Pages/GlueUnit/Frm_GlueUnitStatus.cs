using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls.IncDecButton;

namespace SyncroC.Forms.Pages.GlueUnit
{
	public partial class Frm_GlueUnitStatus : SyncroC.Forms.Pages.GlueUnit.Frm_GlueUnitGroup
	{
		private bool showDebugOptions = false;

		public Frm_GlueUnitStatus()
		{
			InitializeComponent();

			this.wa_wrapArmPreHeater21.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeater4.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeater5.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater6.setDesignModePage(this.DesignMode);

			this.gg_glueGap1.setDesignModePage(this.DesignMode);
			this.gg_glueGap2.setDesignModePage(this.DesignMode);

			this.gg_glueGap1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MinVal;
			this.gg_glueGap1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MaxVal;

			this.gg_glueGap2.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MinVal;
			this.gg_glueGap2.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MaxVal;

      //RFAZZI_FI24/08/2011_1. Aggiunta la terza zona in GlueUnit Status Page. 
      this.gg_glueGap3.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.GlueGapRegulation.MinVal;
      this.gg_glueGap3.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.GlueGapRegulation.MaxVal;

			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
				this.wa_wrapArmPreHeater21.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater4.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater5.LowLevelEnabled = false;
				this.wrapArmPreHeater21.LowLevelEnabled = false;
				this.wrapArmPreHeater4.LowLevelEnabled = false;
				this.wrapArmPreHeater5.LowLevelEnabled = false;

				this.gg_glueGap1.LowLevelEnabled = false;
				this.gg_glueGap2.LowLevelEnabled = false;
				this.glueGap1.LowLevelEnabled = false;
				this.glueGap2.LowLevelEnabled = false;

        //RFAZZI_FI24/08/2011_1. Aggiunta la terza zona in GlueUnit Status Page. 
        this.wrapArmPreHeater6.LowLevelEnabled = false;
        this.glueGap3.LowLevelEnabled = false;
      }
      //agrassi_FI16-05-2011_3
      this.ttb_thickness1.Visible = this.ttb_thickness2.Visible = this.ttb_thickness3.Visible =
        this.ttb_PressorGluerGap1.Visible = this.ttb_PressorGluerGap2.Visible = this.ttb_PressorGluerGap3.Visible = false;
      if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3763")
      {
        this.ttb_thickness1.Visible = this.ttb_thickness2.Visible =
          this.ttb_PressorGluerGap1.Visible = this.ttb_PressorGluerGap2.Visible = true;
      }

      if (!SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        //ridisegno la grafica per due livelli
        this.pct_Machine.Image = SyncroC.Properties.Resources.CREST;
        this.gg_glueGap1.Location = new Point(590, 427);
        this.glueGap1.Location = new Point(776, this.gg_glueGap1.Location.Y);
        this.ttb_thickness1.Location = new Point(879, this.gg_glueGap1.Location.Y);
        this.ttb_PressorGluerGap1.Location = new Point(this.ttb_thickness1.Location.X, this.ttb_thickness1.Location.Y + 27);

        this.gg_glueGap2.Location = new Point(590, 246);
        this.glueGap2.Location = new Point(776, this.gg_glueGap2.Location.Y);
        this.ttb_thickness2.Location = new Point(879, this.gg_glueGap2.Location.Y);
        this.ttb_PressorGluerGap2.Location = new Point(this.ttb_thickness2.Location.X, this.ttb_thickness2.Location.Y + 27);
      }
    }

		protected override void ControlD()
		{
			showDebugOptions = !showDebugOptions;

			this.wa_wrapArmPreHeater21.Visible = showDebugOptions;
			this.wa_wrapArmPreHeater4.Visible = showDebugOptions;
			this.wa_wrapArmPreHeater5.Visible = showDebugOptions;
			this.wrapArmPreHeater4.Visible = showDebugOptions;
			this.wrapArmPreHeater5.Visible = showDebugOptions;
			this.wrapArmPreHeater21.Visible = showDebugOptions;

			this.gg_glueGap1.Visible = showDebugOptions;
			this.gg_glueGap2.Visible = showDebugOptions;
			this.glueGap1.Visible = showDebugOptions;
			this.glueGap2.Visible = showDebugOptions;
      //RFAZZI_FI24/08/2011_1. Aggiunta la terza zona in GlueUnit Status Page. 
      this.gg_glueGap3.Visible = showDebugOptions;
      this.glueGap3.Visible = showDebugOptions;
      this.wrapArmPreHeater6.Visible = showDebugOptions;
    }

		protected override void F3()
		{
		}

		private void wa_wrapArm_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				string cmd = ((Control)sender).Name.Replace("wa_", args.AutoMan ? "AutoManual" : "LocalRemote");

				//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				string cmd = ((Control)sender).Name.Replace("wa_", "");

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void gg_glueGap_OnGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				string cmd = ((Control)sender).Name.Replace("gg_", args.AutoMan ? "AutoManual" : "LocalRemote");

				//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				string cmd = ((Control)sender).Name.Replace("gg_", "");

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void glueUnit_OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." +
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" +
				args.Frequency + ")");
		}

		public override void CorrugatorDirectionLeft()
		{
			//GlueUnit Picture
			this.pct_Machine.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.btn_Group1.Location.X;
			int rightLimit = this.tab_TabA.Location.X;

			HorizontalLocationMirror(this.wa_wrapArmPreHeater4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeater5, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeater21, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater5, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater21, leftLimit, rightLimit);

			HorizontalLocationMirror(this.pct_Machine, leftLimit, rightLimit);

			HorizontalLocationMirror(this.glueGap1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.glueGap2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.gg_glueGap1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.gg_glueGap2, leftLimit, rightLimit);

      //RFAZZI_FI24/08/2011_1. Aggiunta la terza zona in GlueUnit Status Page. 
      HorizontalLocationMirror(this.glueGap3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.gg_glueGap3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wrapArmPreHeater6, leftLimit, rightLimit);

      HorizontalLocationMirror(this.ttb_thickness1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttb_thickness2, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttb_thickness3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttb_PressorGluerGap1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttb_PressorGluerGap2, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttb_PressorGluerGap3, leftLimit, rightLimit);
    
    }
	}
}

