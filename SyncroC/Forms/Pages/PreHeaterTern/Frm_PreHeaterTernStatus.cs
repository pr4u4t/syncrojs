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
using SyncroC.Forms.Pages.DoubleBacker.Status;


namespace SyncroC.Forms.Pages.PreHeaterTern
{
	public partial class Frm_PreHeaterTernStatus : SyncroC.Forms.Pages.PreHeaterTern.Frm_PreHeaterTernGroup
	{
		private bool showDebugOptions = false;

		public Frm_PreHeaterTernStatus()
		{
			InitializeComponent();

			this.wa_wrapArmPreHeater1.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeater11.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeater2.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeater20.setDesignModePage(this.DesignMode);

			this.pt_paperTension21.setDesignModePage(this.DesignMode);
			this.pt_paperTension22.setDesignModePage(this.DesignMode);

      //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea
      this.st_wrapArmSteam11.setDesignModePage(this.DesignMode);
      this.st_wrapArmSteam1.setDesignModePage(this.DesignMode);

      //RFAZZI_FI24/08/2011_2. Aggiunta la terza zona in Thermotack Status Page. 
      this.wa_wrapArmPreHeater3.setDesignModePage(this.DesignMode);
      this.pt_paperTension23.setDesignModePage(this.DesignMode);

			this.pt_paperTension21.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.PaperTensionRegulation.MinVal;
			this.pt_paperTension21.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.PaperTensionRegulation.MaxVal;
			this.pt_paperTension22.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.PaperTensionRegulation.MinVal;
			this.pt_paperTension22.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.PaperTensionRegulation.MaxVal;
      //RFAZZI_FI24/08/2011_2. Aggiunta la terza zona in Thermotack Status Page. 
      this.pt_paperTension23.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Bridge3Config.PaperTension.PaperTensionRegulation.MinVal;
      this.pt_paperTension23.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Bridge3Config.PaperTension.PaperTensionRegulation.MaxVal;

      //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
      this.st_wrapArmSteam11.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.PreHeatersTernConfig.WrapArmSteam11.WrapArmSteamRegulation.MaxVal;
      this.st_wrapArmSteam11.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.PreHeatersTernConfig.WrapArmSteam11.WrapArmSteamRegulation.MinVal;
      this.st_wrapArmSteam1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.PreHeatersTernConfig.WrapArmSteam11.WrapArmSteamRegulation.MaxVal;
      this.st_wrapArmSteam1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.PreHeatersTernConfig.WrapArmSteam11.WrapArmSteamRegulation.MinVal;

			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
				this.wa_wrapArmPreHeater11.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater1.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater2.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater20.LowLevelEnabled = false;
				this.wrapArmPreHeater11.LowLevelEnabled = false;
				this.wrapArmPreHeater1.LowLevelEnabled = false;
				this.wrapArmPreHeater2.LowLevelEnabled = false;
				this.wrapArmPreHeater20.LowLevelEnabled = false;

				this.pt_paperTension21.LowLevelEnabled = false;
				this.pt_paperTension22.LowLevelEnabled = false;

        //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
        this.st_wrapArmSteam11.LowLevelEnabled = false;
        this.st_wrapArmSteam1.LowLevelEnabled = false;

        //RFAZZI_FI24/08/2011_2. Aggiunta la terza zona in Thermotack Status Page. 
        this.wa_wrapArmPreHeater3.LowLevelEnabled = false;
        this.wrapArmPreHeater3.LowLevelEnabled = false;
        this.pt_paperTension23.LowLevelEnabled = false;

      }
		}

		protected override void F3()
		{
		}

		protected override void ControlD()
		{
			showDebugOptions = !showDebugOptions;

			this.wa_wrapArmPreHeater1.Visible = showDebugOptions;
			this.wa_wrapArmPreHeater11.Visible = showDebugOptions;
			this.wa_wrapArmPreHeater2.Visible = showDebugOptions;
			this.wa_wrapArmPreHeater20.Visible = showDebugOptions;

			this.pt_paperTension21.Visible = showDebugOptions;
			this.pt_paperTension22.Visible = showDebugOptions;

      //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
      this.st_wrapArmSteam11.Visible = showDebugOptions;
      this.st_wrapArmSteam1.Visible = showDebugOptions;

      //RFAZZI_FI24/08/2011_2. Aggiunta la terza zona in Thermotack Status Page. 
      this.wa_wrapArmPreHeater3.Visible = showDebugOptions;
      this.pt_paperTension23.Visible = showDebugOptions;

		}

		private void wa_wrapArmPreHeater_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				string cmd = ((Control)sender).Name.Replace("wa_", args.AutoMan ? "AutoManual" : "LocalRemote");

				//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				string cmd = ((Control)sender).Name.Replace("wa_", "");

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void wrapArmPreHeater_OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." +
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" +
				args.Frequency + ")");
		}

		public override void CorrugatorDirectionLeft()
		{
			//Pre-Heaters Tern Picture
			this.pct_Machine.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.lbl_Title_YN.Location.X;
			int rightLimit = this.tab_TabA.Location.X;

			HorizontalLocationMirror(this.wa_wrapArmPreHeater1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeater2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeater20, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeater11, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater20, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater11, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_Machine, leftLimit, rightLimit);

			HorizontalLocationMirror(this.pt_paperTension21, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pt_paperTension22, leftLimit, rightLimit);

      //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
      HorizontalLocationMirror(this.st_wrapArmSteam11, leftLimit, rightLimit);
      HorizontalLocationMirror(this.st_wrapArmSteam1, leftLimit, rightLimit);

      //RFAZZI_FI24/08/2011_2. Aggiunta la terza zona in Thermotack Status Page. 
      HorizontalLocationMirror(this.wa_wrapArmPreHeater3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wrapArmPreHeater3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pt_paperTension23, leftLimit, rightLimit);


		}

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    /********************************
     * WrapArm Steam
     * *******************************/

    private void pt_wrapArmSteam11_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "WrapArmSteam11";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAPARM_STEAM + "(" + args.SetPoint.ToString() + ")");
      }
    }
    private void pt_wrapArmSteam1_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "WrapArmSteam1";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAPARM_STEAM + "(" + args.SetPoint.ToString() + ")");
      }
    }

		/********************************
		 * Paper Tension
		 * *******************************/

		private void pt_paperTension21_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTensionBridge21";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void pt_paperTension22_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTensionBridge22";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

    //RFAZZI_FI24/08/2011_2. Aggiunta la terza zona in Thermotack Status Page. 
    private void pt_paperTension23_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "PaperTensionBridge23";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
      }

    }

	}
}

