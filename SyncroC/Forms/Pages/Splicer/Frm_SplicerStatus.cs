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
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Splicer
{
  public partial class Frm_SplicerStatus : SyncroC.Forms.Pages.Splicer.Frm_SplicerGroup
	{
    private const int ROLL_STAND_COUNT = 2;
    private const int ROLL_LIST_SIZE = 2;
		private bool viewMetres = false;
    private Frm_RollData frm_rollData = null;
		private RollData rollData1 = new RollData();
		private RollData rollData2 = new RollData();
		private RollData rollData3 = new RollData();
		private RollData rollData4 = new RollData();

		public Frm_SplicerStatus()
		{
			InitializeComponent();

      //ABELLO questo è giusto non farlo perchè la modifica si può fare solo per la bobina corrente 
      //if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      //{
      //  this.btn_CoreDiameterModify.Visible = false;
      //  this.btn_SpliceSpeedModify.Visible = false;
      //}

			this.pt_paperTension.setDesignModePage(this.DesignMode);
			this.pt_paperTension.setDesignModePage(this.DesignMode);

			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
				this.pt_paperTension.LowLevelEnabled = false;
			}

		}

    public override void refreshData()
    {
      this.configureMinAndMax();
    }

    private void configureMinAndMax()
    {

      switch (index)
      {
        case 1:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
        case 2:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
        case 3:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
        case 4:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
        case 5:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
        case 6:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer6Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer6Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer6Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
        case 7:
          this.pt_paperTension.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer7Config.PaperTension.PaperTensionRegulation.MinVal;
          this.pt_paperTension.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer7Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.pt_paperTension.LowLevelEnabled = SyncroC.DBManager.getDBManager().lineConfig.Splicer7Config.PaperTension.Present &&
                                                  SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled;
          break;
      }

    }

    /// <summary>
    /// Builder. Build an istance of this page for the specified Splicer.
    /// </summary>
    /// <param name="index">Index of the splicer represented on the current instance of this Form</param>
    public Frm_SplicerStatus(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7))
				Logger.LogError(this, "Frm_SplicerStatus - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\", \"4\", \"5\", \"6\", \"7\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();

			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
			{
				this.ToggleAutoMan.IsButton = false;
				this.RollChange.IsButton = false;
			}
		}

		protected override void F3()
		{
		}

		protected override void CtrlF5()
		{
			viewMetres = ! viewMetres;
			if (viewMetres)
				tnud_MeterLeftNextSplice.BringToFront();
			else
				tnud_ProductionLeftNextSplice.BringToFront();
		}

		protected override void ControlD()
		{
			showDebugOptions = !showDebugOptions;

			this.pt_paperTension.Visible = showDebugOptions;
		}

		private void ToggleAutoMan_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					Command.SPLICER + index.ToString() + "." + ((Control)sender).Name + "()");
		}

		private void RollChange_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;
      //agrassi_FI14-04-08
      //RFAZZI_FI03/03/2010_3.Rimosso controllo sul codice bobina inserito per la pressione del pulsante di "forbici" nella pagina stato giuntatrice
      //if ((this.rollData1.rollId.Trim() == "") || (this.rollData1.paperType.Trim() == ""))
      if ( this.rollData1.paperType.Trim() == "" )
      {
        Frm_Alert frm_Alert = new Frm_Alert();
        string message = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_184");
        frm_Alert.ShowDialog(this, message);
        return;
      }
      //agrassi_FI03-03-08
      Frm_Confirm frm_confirm = new Frm_Confirm();
      //agrassi_FI14-04-08
      //string message = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_181") + " " +
      //                this.rollData1.rollId + " (" + this.rollData1.paperType +
      //                ") " + DBManager.getDBManager().message.getTranslatedMessage("§SYCC_182") + " " +
      //                this.rollData2.rollId + " (" + this.rollData2.paperType + ")?";
      string mess = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_181") + " " +
                      (this.rollData1.rollId.Trim()!=""?this.rollData1.rollId:"????") + 
                      DBManager.getDBManager().message.getTranslatedMessage("§SYCC_182") + " " +
                      (this.rollData2.rollId.Trim() != "" ? this.rollData2.rollId : "????") + "?";
      DialogResult res = frm_confirm.ShowDialog(this, mess);
      if (res == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            Command.SPLICER + index.ToString() + "." + ((Control)sender).Name + "()");
      }
		}

		public void _setTimeLeftNextSplice(int timeLeftSecondsNextSplice)
		{
			if (timeLeftSecondsNextSplice >= 0)
				tlb_TimeLeftNextSplice.Value = SyncroConverter.ConvertToFormattedTime(timeLeftSecondsNextSplice);
			else
				tlb_TimeLeftNextSplice.Value = "---";
		}

		private void chk_NextSpliceEndOfRoll_OnCheckedChanged(object sender, EventArgs e)
		{
			chk_NextSpliceQualityChange.Checked = !chk_NextSpliceEndOfRoll.Checked;
		}

    public void _setRollsStatus(bool readyToSplice, bool RollAActive, bool Rotating)
    {
			if (index == 1 || index == 3 || index == 5 || index == 7)
			{
				roll_SX.Active = !RollAActive;
				roll_DX.Active = RollAActive;
			}
			else
			{
				roll_SX.Active = RollAActive;
				roll_DX.Active = !RollAActive;
			}
			roll_SX.Ready = roll_DX.Ready = readyToSplice;
			roll_SX.Rotating = roll_DX.Rotating = Rotating;
    }

		private void btn_SpliceSpeedModify_Click(object sender, EventArgs e)
		{
			//FOS2 ABello
			Frm_AskValue askValue = new Frm_AskValue();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_74");
			FosberDialogResult ret = askValue.ShowDialog(this, text, (Convert.ToInt32(this.tnud_Speed.Value)/1000));

			if (ret.Dialog == DialogResult.OK)
			{
				int res = (int)ret.Data[0] * 1000; // m/min -> mm/min
				if (res < 0)
					res = DBManager.getDBManager().lineConfig.getSplicerNumber(index).DefaultSpliceSpeed_UVmm_m;

				if (res > DBManager.getDBManager().lineConfig.getSplicerNumber(index).MaxSpliceSpeed_UVmm_m)
					res = DBManager.getDBManager().lineConfig.getSplicerNumber(index).DefaultSpliceSpeed_UVmm_m;
		
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.SPLICER + index.ToString() + ".EditSpliceSpeed(" + res + ")");

				if (SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				{
					Frm_Confirm confirm = new Frm_Confirm();
					text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_156");
					DialogResult result = confirm.ShowDialog(this, text);
					if (result == DialogResult.OK)
					{
						updatePaper(true, res);
						updateQualities();
					}
				}
			}
		}

		private void btn_CoreDiameterModify_Click(object sender, EventArgs e)
		{
			//FOS2 ABello
			Frm_AskValue askValue = new Frm_AskValue();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_74");
			FosberDialogResult ret = askValue.ShowDialog(this, text, (Convert.ToInt32(this.tnud_CoreDiameter.Value)/10));

			if (ret.Dialog == DialogResult.OK)
			{
				int res = (int)ret.Data[0]*10; // mm -> dmm
				if (res < 500)
					return;

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.SPLICER + index.ToString() + ".EditCoreDiameter(" + res + ")");

				if (SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				{
					Frm_Confirm confirm = new Frm_Confirm();
					text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_156");
					DialogResult result = confirm.ShowDialog(this, text);
					if (result == DialogResult.OK)
					{
						updatePaper(false, res);
						updateQualities();
					}
				}
			}
		}

		private void updatePaper(bool isSpliceSpeed, int value)
		{
			if (isSpliceSpeed)
				value = value / 1000; //mm/m -> m/m

			DataRow row = null;
			try
			{
				row = DBManager.getDBManager().paper.getRow(this.tlb_CurrentPaperID.Value, Convert.ToInt32(this.tnud_CurrentPaperWF.Value));
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "updatePaper - error getting row.", ex);
			}
			if (row != null)
			{
				string toSend = Command.DB_PAPER + "." + Command.DB_CMD_MOD+ "(";

				toSend += row[Paper.PAPER_ID].ToString() + Command.PARAMETER_SEPARATOR;
				if (isSpliceSpeed)
					toSend += row[Paper.CORE_DIAMETER].ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				toSend += row[Paper.WEIGHT_FACTOR].ToString() + Command.PARAMETER_SEPARATOR;
				toSend += row[Paper.TEXT].ToString() + Command.PARAMETER_SEPARATOR;
				toSend += row[Paper.DEFAULT_THICKNESS].ToString() + Command.PARAMETER_SEPARATOR;
        //agrassi_FI02-09-2011_1
        //toSend += row[Paper.PAPER_CLASSIFICATION].ToString() + Command.PARAMETER_SEPARATOR;
        toSend += this.NotEmptyString(row[Paper.PAPER_CLASSIFICATION].ToString())+ Command.PARAMETER_SEPARATOR;
        toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
				if (isSpliceSpeed && index == 1)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_0].ToString() + Command.PARAMETER_SEPARATOR;

				if (isSpliceSpeed && index == 2)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_1].ToString() + Command.PARAMETER_SEPARATOR;

				if (isSpliceSpeed && index == 3)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_2].ToString() + Command.PARAMETER_SEPARATOR;

				if (isSpliceSpeed && index == 4)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_3].ToString() + Command.PARAMETER_SEPARATOR;

				if (isSpliceSpeed && index == 5)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_4].ToString() + Command.PARAMETER_SEPARATOR;

				if (isSpliceSpeed && index == 6)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_5].ToString() + Command.PARAMETER_SEPARATOR;

				if (isSpliceSpeed && index == 7)
					toSend += value.ToString() + Command.PARAMETER_SEPARATOR;
				else
					toSend += row[Paper.MAX_SPLICE_SPEED_6].ToString() + Command.PARAMETER_SEPARATOR;

				toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
				toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
				toSend += "SPARE";
				toSend += ")";

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, toSend);
			}
		}
	
		private void updateQualities()
		{
			DataTable quality = DBManager.getDBManager().quality.getTable();
			for (int i = 0; i < quality.Rows.Count; i++)
			{
				if ((quality.Rows[i][Quality.PAPER_ID_0].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_0].ToString().Equals(this.tnud_CurrentPaperWF.Value))
					|| (quality.Rows[i][Quality.PAPER_ID_1].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_1].ToString().Equals(this.tnud_CurrentPaperWF.Value))
					|| (quality.Rows[i][Quality.PAPER_ID_2].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_2].ToString().Equals(this.tnud_CurrentPaperWF.Value))
					|| (quality.Rows[i][Quality.PAPER_ID_3].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_3].ToString().Equals(this.tnud_CurrentPaperWF.Value))
					|| (quality.Rows[i][Quality.PAPER_ID_4].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_4].ToString().Equals(this.tnud_CurrentPaperWF.Value))
					|| (quality.Rows[i][Quality.PAPER_ID_5].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_5].ToString().Equals(this.tnud_CurrentPaperWF.Value))
					|| (quality.Rows[i][Quality.PAPER_ID_6].Equals(this.tlb_CurrentPaperID.Value) &&
              quality.Rows[i][Quality.WEIGHT_FACTOR_6].ToString().Equals(this.tnud_CurrentPaperWF.Value)))
				{//then quality uses the updated paper
					//
          //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
          /*
          string toSend = Command.DB_QUALITY + "." + Command.DB_CMD_MOD + "WithSetup" + "(";
          */
          string toSend = Command.DB_QUALITY + "." + Command.DB_CMD_MOD + "WithSetup_NoPressure" + "(";

          toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_ID].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_TEXT].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.FLUTE_ID].ToString()) + Command.PARAMETER_SEPARATOR;
					//DRY DATA
					toSend += this.NotEmptyString(quality.Rows[i][Quality.SCOREGAP_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.TRIM_GRINDINGS_MULTIPLIER].ToString()) + Command.PARAMETER_SEPARATOR;
          toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_DISTANCE_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_DURATION_UTms].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PRODUCTION_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.AOC_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.THICKNESS_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += "0" + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_LAYERS_PER_STACK].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_REVERSE_MODE].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_0].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_0].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_1].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_1].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_2].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_2].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_3].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_3].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_4].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_4].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_5].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_5].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_6].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_6].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.PREPRINT].ToString()) + Command.PARAMETER_SEPARATOR;
					//PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
					//toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += this.NotEmptyString(quality.Rows[i][Quality.INCLINATELASTBELT].ToString()) + Command.PARAMETER_SEPARATOR;
          //PMASSARO_FI09/05/09 - Bug Fix. The Internal Flap Check Was Resetted After Paper Parameters Modify
          //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
          toSend += this.NotEmptyString(quality.Rows[i][Quality.TWIN400_INTERNAL_FLAP_DISABLED].ToString()) + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
					toSend += "SPARE";
					toSend += ")";

					CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_DB_MANAGER, toSend);
					
          System.Threading.Thread.Sleep(400);
				}
			}//end for loop
			PopUp.Frm_Alert alert = new Frm_Alert();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_142");
			alert.ShowDialog(text);
		}

		public override void CorrugatorDirectionLeft()
		{
			//Pre-Heaters Tern Picture
			this.pct_Machine.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.pct_Machine.Location.X;
			int rightLimit = this.pct_Machine.Location.X + this.pct_Machine.Width;

			HorizontalLocationMirror(this.roll_DX, leftLimit, rightLimit);
			HorizontalLocationMirror(this.roll_SX, leftLimit, rightLimit);

			this.roll_DX.CorrugatorDirectionLeft = true;
			this.roll_SX.CorrugatorDirectionLeft = true;
		}

		/********************************
		 * Paper Tension
		 * *******************************/

		private void pt_paperTension_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTension";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), index) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), index) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

    private void ManageRoll(FosberDialogResult res)
    {
			RollData myRoll = null;
			int splicerNumber = 0;
			int rollQueue = 0;

			try
			{
				myRoll = (RollData)res.Data[2];
				splicerNumber = (int)res.Data[0];
				rollQueue = (int)res.Data[1];
			}
			catch (Exception ex)
			{
				Logger.LogError(this, ex);
				myRoll = null;
				splicerNumber = 0;
				rollQueue = 0;
			}

			if ((splicerNumber == 0) || (rollQueue == 0) || (myRoll==null))
				return;

			//Insert
			string rollId = this.NotEmptyString(myRoll.rollId);
			string paperType = this.NotEmptyString(myRoll.paperType);
		  int width = myRoll.width_UDdmm;
		  int suppliedDiameter = myRoll.suppliedDiameter_UDdmm;
		  int currentDiameter = myRoll.diameter_UDdmm;
		  int coreSize = myRoll.coreDiameter_UDdmm;
		  int suppliedLength = myRoll.suppliedLength_UDmm;
		  int currentLength = myRoll.length_UDmm;
		  int grammage = myRoll.weightFactor_UWg_m;
		  int thickness = myRoll.thickness_UDdmmm;
		  string description = this.NotEmptyString(myRoll.description);
			string dataToPrint = this.NotEmptyString(myRoll.dataToPrint);

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
		  cm.SendTcp(cm.SYNCROS_DB_MANAGER,
		      "pastRolls.UpdateRollData(" + 
																										splicerNumber + Command.PARAMETER_SEPARATOR +
																										rollQueue + Command.PARAMETER_SEPARATOR +
																										rollId + Command.PARAMETER_SEPARATOR +
																										paperType + Command.PARAMETER_SEPARATOR +
		                                                width.ToString()+Command.PARAMETER_SEPARATOR+
		                                                suppliedDiameter.ToString()+Command.PARAMETER_SEPARATOR+
		                                                coreSize.ToString()+Command.PARAMETER_SEPARATOR+
		                                                suppliedLength.ToString() + Command.PARAMETER_SEPARATOR +
		                                                currentLength.ToString() + Command.PARAMETER_SEPARATOR +
		                                                grammage.ToString()+Command.PARAMETER_SEPARATOR+
		                                                thickness.ToString()+Command.PARAMETER_SEPARATOR+
		                                                description+Command.PARAMETER_SEPARATOR+
																										dataToPrint + 
		                                                 ")");

			//RFAZZI_FI18/01/2010_1. Bug Fix quando viene modificato il valore di "Diametro Giunta" non si aggiorna la Shared "Roll" e quindi si cambia quando i metri da produrre sono diversi da 0
			if ( rollQueue == 1 )
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.SPLICER + index.ToString() + ".EditCoreDiameter(" + coreSize.ToString() + ")");
			
    }

    private void btn_ViewRoll1_Click(object sender, EventArgs e)
    {
      this.frm_rollData = new Frm_RollData();
      FosberDialogResult res=this.frm_rollData.ShowDialog(this, this.index, 1, this.rollData1);
      if (res.Dialog == DialogResult.Yes)
        this.ManageRoll(res);
    }

    private void btn_ViewRoll2_Click(object sender, EventArgs e)
    {
      this.frm_rollData = new Frm_RollData();
      FosberDialogResult res = this.frm_rollData.ShowDialog(this, this.index, 2, this.rollData2);
			if (res.Dialog == DialogResult.Yes)
        this.ManageRoll(res);
    }

    private void btn_ViewRoll3_Click(object sender, EventArgs e)
    {
      this.frm_rollData = new Frm_RollData();
      FosberDialogResult res = this.frm_rollData.ShowDialog(this, this.index, 3, this.rollData3);
			if (res.Dialog == DialogResult.Yes)
        this.ManageRoll(res);
    }

    private void btn_ViewRoll4_Click(object sender, EventArgs e)
    {
      this.frm_rollData = new Frm_RollData();
      FosberDialogResult res = this.frm_rollData.ShowDialog(this, this.index, 4, this.rollData4);
			if (res.Dialog == DialogResult.Yes)
        this.ManageRoll(res);
    }

    private RollData ConvertRollData(string data)
    {
      string[] arrayData=data.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      RollData rollData = new RollData();
      if (arrayData.Length == 13)
      {
        rollData.rollId = arrayData[0].Trim();
        rollData.paperType = arrayData[1].Trim();
        rollData.width_UDdmm = this.intParse(arrayData[2]);
        rollData.suppliedDiameter_UDdmm = this.intParse(arrayData[3]);	
			
        rollData.diameter_UDdmm = this.intParse(arrayData[4]);
        rollData.coreDiameter_UDdmm = this.intParse(arrayData[5]);
        
        rollData.suppliedLength_UDmm = this.intParse(arrayData[6]);
        rollData.length_UDmm = this.intParse(arrayData[7]);
        rollData.weightFactor_UWg_m = this.intParse(arrayData[8]);
        rollData.thickness_UDdmmm = this.intParse(arrayData[9]);
        rollData.description = arrayData[10].Trim();
				rollData.rollDataEntered = Convert.ToBoolean(arrayData[11]);
				rollData.dataToPrint = arrayData[12].Trim();
      }
      return rollData;
    }

    public void _UpdateRollData(int splicerNumber, int rollQueue, string data)
    {
      if (this.index != splicerNumber)
        return;

			switch (rollQueue)
			{
				case 1:
					this.rollData1 = this.ConvertRollData(data);

					this.tlb_1RollID.Value = this.rollData1.rollId;
					this.tlb_1RollPaper.Value = this.rollData1.paperType;

					if (!this.rollData1.rollDataEntered)
						this.pct_RollScan1.Image = this.imageListRollScan.Images[0];
					else
						this.pct_RollScan1.Image = this.imageListRollScan.Images[1];

					if ((this.frm_rollData != null) && (this.frm_rollData.RollQueue == 1))
						this.frm_rollData.UpdateRollData(this.rollData1);
					break;
				case 2:
					this.rollData2 = this.ConvertRollData(data);

					this.tlb_2RollID.Value = this.rollData2.rollId;
					this.tlb_2RollPaper.Value = this.rollData2.paperType;

					if (!this.rollData2.rollDataEntered)
						this.pct_RollScan2.Image = this.imageListRollScan.Images[0];
					else
						this.pct_RollScan2.Image = this.imageListRollScan.Images[1];

					if ((this.frm_rollData != null) && (this.frm_rollData.RollQueue == 2))
						this.frm_rollData.UpdateRollData(this.rollData2);
					break;
				case 3:
					this.rollData3 = this.ConvertRollData(data);

					this.tlb_3RollID.Value = this.rollData3.rollId;
					this.tlb_3RollPaper.Value = this.rollData3.paperType;

					if (!this.rollData3.rollDataEntered)
						this.pct_RollScan3.Image = this.imageListRollScan.Images[0];
					else
						this.pct_RollScan3.Image = this.imageListRollScan.Images[1];

					if ((this.frm_rollData != null) && (this.frm_rollData.RollQueue == 3))
						this.frm_rollData.UpdateRollData(this.rollData3);
					break;
				case 4:
					this.rollData4 = this.ConvertRollData(data);

					this.tlb_4RollID.Value = this.rollData4.rollId;
					this.tlb_4RollPaper.Value = this.rollData4.paperType;

					if (!this.rollData4.rollDataEntered)
						this.pct_RollScan4.Image = this.imageListRollScan.Images[0];
					else
						this.pct_RollScan4.Image = this.imageListRollScan.Images[1];

					if ((this.frm_rollData != null) && (this.frm_rollData.RollQueue == 4))
						this.frm_rollData.UpdateRollData(this.rollData4);
					break;
				default:
					break;
			}
    }
	}

	public class RollData
	{
		public string rollId;
		public string paperType;
		public int width_UDmm;
		public int diameter_UDdmm;
		public int suppliedDiameter_UDdmm;
		public int length_UDmm;
		public int suppliedLength_UDmm;
		public int coreDiameter_UDdmm;															// Diameter to splice at	From paper database or perhaps roll stock
		public int width_UDdmm;																		// Width of roll
		public int weightFactor_UWg_m;															// Weight factor of paper
		public int thickness_UDdmmm;																// Paper caliper 
		public string description;
		public string dataToPrint;
		public bool rollDataEntered;																// By operator

		public RollData()
		{
			this.rollId = "";
			this.paperType = "";
			this.description = "";
			this.dataToPrint = "";
		}
	}
}

