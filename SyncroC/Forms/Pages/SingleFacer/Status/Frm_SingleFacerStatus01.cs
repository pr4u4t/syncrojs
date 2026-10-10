using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Controls;
using SyncroC.DataBase.LineConfig;
using SyncroC.DataBase;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Controls.IncDecButton;

namespace SyncroC.Forms.Pages.SingleFacer.Status
{
	public partial class Frm_SingleFacerStatus01 : SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatusSubgroup
	{

		private int currId = 0, currModId = 0, nextId = 0, nextModId = 0;

		public Frm_SingleFacerStatus01()
		{
			InitializeComponent();

			//PMASSARO_FI28/10/08 - Customer Personalization SAECO 3663-64 Adjusted Wrap Arm Visualization On SF
			if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3664")
			{
			  this.wa_wrapArmPreHeaterEl.Location = new System.Drawing.Point(178, 577);
			  this.wrapArmPreHeaterEl.Location = new System.Drawing.Point(314, 577);

			  this.wa_wrapArmPreHeaterIl.Location = new System.Drawing.Point(119, 432);
			  this.wrapArmPreHeaterIl.Location = new System.Drawing.Point(258, 431);
			}
			
			this.wa_wrapArmPreHeaterEl.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeaterEm.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeaterIl.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeaterIm.setDesignModePage(this.DesignMode);

			this.gg_glueGap1.setDesignModePage(this.DesignMode);

      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.SendCurrentOrder.IsButton = false;
        this.SendNextOrder.IsButton = false;
      }

			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
				this.wrapArmPreHeaterEl.LowLevelEnabled = false;
				this.wrapArmPreHeaterEm.LowLevelEnabled = false;
				this.wrapArmPreHeaterIl.LowLevelEnabled = false;
				this.wrapArmPreHeaterIm.LowLevelEnabled = false;
				this.glueGap1.LowLevelEnabled = false;

				this.wa_wrapArmPreHeaterEl.LowLevelEnabled = false;
				this.wa_wrapArmPreHeaterEm.LowLevelEnabled = false;
				this.wa_wrapArmPreHeaterIl.LowLevelEnabled = false;
				this.wa_wrapArmPreHeaterIm.LowLevelEnabled = false;
				this.gg_glueGap1.LowLevelEnabled = false;
        this.MoistureSensor.IsButton = false; 
			}

      //PMASSARO_FI07/09/11 - SyncroC: Corretta l'assegnazione dei minimi e massimi per i controlli del Glue-Gap
      /*
			this.gg_glueGap1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(this.index).Gluegap1.GlueGapRegulation.MinVal;
			this.gg_glueGap1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(this.index).Gluegap1.GlueGapRegulation.MaxVal;
      */
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerStatus01(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerStatus01 - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();

      //PMASSARO_FI07/09/11 - SyncroC: Corretta l'assegnazione dei minimi e massimi per i controlli del Glue-Gap
			this.gg_glueGap1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(this.index).Gluegap1.GlueGapRegulation.MinVal;
			this.gg_glueGap1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(this.index).Gluegap1.GlueGapRegulation.MaxVal;
		}

		protected override void F10()
		{
		}

    private void _setLocaleRemoteMachine(bool isSingleFacerRemote)
    {
      //RFAZZI_FI26/07/2011_1. Rimossa visualizzazione stato macchina Loc/Rem da pagine di stato DB e SF quando presenti Express400 e Smart400 xchè nn presenti sulla macchina. 
      if ( (DBManager.getDBManager().lineConfig.PRC ) &&
       (( !SyncroC.DBManager.getDBManager().syncroConfig.SingleFacerSmart400CTRL1Setting.Enable && index == 1 ) ||
        (!SyncroC.DBManager.getDBManager().syncroConfig.SingleFacerSmart400CTRL2Setting.Enable && index == 2) ||
        ( !SyncroC.DBManager.getDBManager().syncroConfig.SingleFacerSmart400CTRL3Setting.Enable && index == 3 ))  ) 
      {
        this.pct_LocRem_SF.Visible = true;
        this.pct_LocRem_SF.Image = isSingleFacerRemote ? SyncroC.Properties.Resources.REMOTO_ISO_1108 : SyncroC.Properties.Resources.LOCALE_ISO_1109;
      }
    }

		private void _setSFNCartridge(string cartridgeId, bool alarmed)
		{
			this.lbl_SFCartridge.Text = cartridgeId;
			this.lbl_SFCartridge.BackColor = alarmed ? Color.Red : Color.WhiteSmoke;
		}

		private void _setCurrentProgram(int currentProgramId, int currentProgramModId)
		{
			this.currId = currentProgramId;
			this.currModId = currentProgramModId;
			//if (this.currId == 0)
			//  return;
			DataRow row = null;
			SyncroC.DataBase.Message msg = DBManager.getDBManager().message;

			if (this.currId != 0)
				row = DBManager.getDBManager().order.WETGetRow(currentProgramId.ToString(), currentProgramModId.ToString());

			if (row != null)
			{//valorizzazione con ordine
				ttl_CurrentProgram.Text = msg.getTranslatedMessage("§SYCC_94") + " " + row[Order.WEL_PROGRAM_CODE].ToString();
				tlb_CurrentQuality.Value = row[Order.WEL_PROGRAM_QUALITY].ToString();
				tlb_CurrentWidth.Value = row[Order.WEL_PAPER_WIDTH].ToString();
				tlb_CurrentLength.Value = row[Order.WEL_PROGRAM_LENGTH].ToString();
			}
			else
			{//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
				ttl_CurrentProgram.Text = msg.getTranslatedMessage("§SYCC_94");
				tlb_CurrentQuality.Value = tlb_CurrentLength.Value = tlb_CurrentWidth.Value = string.Empty;
			}
		}

		private void _setNextProgram(int nextProgramIdSetup, int nextProgramModId)
		{
			this.nextId = nextProgramIdSetup;
			this.nextModId = nextProgramModId;
			DataRow row = null;
			SyncroC.DataBase.Message msg = DBManager.getDBManager().message;

			if (this.nextId != 0)
				row = DBManager.getDBManager().order.WETGetRow(nextProgramIdSetup.ToString(), nextProgramModId.ToString());

			if (row != null)
			{//valorizzazione con ordine
				ttl_NextProgram.Text = msg.getTranslatedMessage("§SYCC_95") + " " + row[Order.WEL_PROGRAM_CODE].ToString();
				tlb_NextQuality.Value = row[Order.WEL_PROGRAM_QUALITY].ToString();
                //PMASSARO_FI27/02/08 - Reviewed Conversion
                /* tlb_NextWidth.Value = SyncroConverter.ConvertString(row[Order.WEL_PAPER_WIDTH].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre); */
                tlb_NextWidth.Value = row[Order.WEL_PAPER_WIDTH].ToString();
                tlb_NextLength.Value = row[Order.WEL_PROGRAM_LENGTH].ToString();
			}
			else
			{//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
				ttl_NextProgram.Text = msg.getTranslatedMessage("§SYCC_95");
				tlb_NextQuality.Value = tlb_NextLength.Value = tlb_NextWidth.Value = string.Empty;
			}
		}

		private void wa_wrapArmPreHeater_OnWrapArmButtonPressed(object sender, WrapArmButtonEventArgs args)
		{
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				string cmd = ((Control)sender).Name.Replace("wa_", args.AutoMan ? "AutoManual" : "LocalRemote");

				//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), index) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				string cmd = ((Control)sender).Name.Replace("wa_", "");

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), index) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void gg_glueGap_OnGlueGapButtonPressed(object sender, LowLevelControlButtonEventArgs args)
		{
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				string cmd = ((Control)sender).Name.Replace("gg_", args.AutoMan ? "AutoManual" : "LocalRemote");

				//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), index) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				string cmd = ((Control)sender).Name.Replace("gg_", "");

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), index) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void wrapArmPreHeater_OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), index) + "." +
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" +
				args.Frequency + ")");
		}


		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
		}

		private void Frm_SingleFacerStatus01_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		public override void CorrugatorDirectionLeft()
		{
			//GlueUnit Picture
			this.pct_machine.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.pnl_currentOrder.Location.X;
			int rightLimit = this.pnl_nextOrder.Location.X + this.pnl_nextOrder.Width;

			HorizontalLocationMirror(this.pnl_currentOrder, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_nextOrder, leftLimit, rightLimit);

			HorizontalLocationMirror(this.lbl_SFCartridge, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tnud_Speed_UVmm_m, leftLimit, rightLimit);

      //ABELLO_FI25/11/2010_4 [PRC] non si invertiva la Locale/remoto se la linea e Sx
      HorizontalLocationMirror(this.pct_LocRem_SF, leftLimit, rightLimit);

			HorizontalLocationMirror(this.wa_wrapArmPreHeaterEl, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeaterEm, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeaterIl, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeaterIm, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeaterEl, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeaterEm, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeaterIl, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeaterIm, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ParaffinMachineLiner, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ParaffinMachineMedium, leftLimit, rightLimit);

      HorizontalLocationMirror(this.MoistureSensor, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_MoistureSensorGreen, leftLimit, rightLimit);
      HorizontalLocationMirror(this.lbl_MoistureSensorRed, leftLimit, rightLimit);

			HorizontalLocationMirror(this.glueGap1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.gg_glueGap1, leftLimit, rightLimit);
		}

    private void MoistureSensor_Click(object sender, EventArgs e)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus02), index) + "." + "RecalibrateBridgeNow" + "." + Command.TOGGLE_WITH_TIMEOUT);
    }

    private void ParaffinMachineMedium_Click(object sender, EventArgs e)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus02), index) + "." + "ParaffinMachineMedium" + "." + Command.TOGGLE_WITH_TIMEOUT);
    }

    private void ParaffinMachineLiner_Click(object sender, EventArgs e)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus02), index) + "." + "ParaffinMachineLiner" + "." + Command.TOGGLE_WITH_TIMEOUT);
    }

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Console.Frm_SingleFacerConsole01), index) + "." + ((Control)sender).Name + "()");
		}

	}
}

