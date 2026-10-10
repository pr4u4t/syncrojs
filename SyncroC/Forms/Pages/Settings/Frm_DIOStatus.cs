using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Settings
{
	public class Frm_DIOStatus : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
	{
		private Label lbl_DIOChannel_0;
		private Label lbl_DIOChannel_1;
		private Label lbl_DIOChannel_2;
		private Label lbl_DIOChannel_3;
		private Label lbl_DIOChannel_4;
		private Label lbl_DIOChannel_6;
		private Label lbl_DIOChannel_5;
		private Label lbl_DIOChannel_8;
		private Label lbl_DIOChannel_7;
		private Label lbl_DIOChannel_10;
		private Label lbl_DIOChannel_11;
		private Label lbl_DIOChannel_9;
		private Label lbl_DIOChannel_12;
		private Label lbl_DIOStatus_0;
		private Label lbl_DIOStatus_1;
		private Label lbl_DIOStatus_2;
		private Label lbl_DIOStatus_3;
		private Label lbl_DIOStatus_4;
		private Label lbl_DIOStatus_5;
		private Label lbl_DIOStatus_6;
		private Label lbl_DIOStatus_7;
		private Label lbl_DIOStatus_8;
		private Label lbl_DIOStatus_9;
		private Label lbl_DIOStatus_11;
		private Label lbl_DIOStatus_15;
		private Label lbl_DIOStatus_14;
		private Label lbl_DIOStatus_12;
		private Label lbl_DIOStatus_10;
		private Label lbl_DIOStatus_13;
		private Label lbl_DIOChannel_13;
		private Label lbl_DIOChannel_14;
		private Label lbl_DIOChannel_15;
		private Label lbl_DIOName_0;
		private Label lbl_DIOType_0;
		private Label lbl_DIOType_1;
		private Label lbl_DIOName_1;
		private Label lbl_DIOName_3;
		private Label lbl_DIOName_2;
		private Label lbl_DIOName_11;
		private Label lbl_DIOName_4;
		private Label lbl_DIOName_5;
		private Label lbl_DIOName_6;
		private Label lbl_DIOName_7;
		private Label lbl_DIOName_12;
		private Label lbl_DIOName_13;
		private Label lbl_DIOName_14;
		private Label lbl_DIOName_15;
		private Label lbl_DIOName_8;
		private Label lbl_DIOName_9;
		private Label lbl_DIOName_10;
		private Label lbl_DIOType_7;
		private Label lbl_DIOType_8;
		private Label lbl_DIOType_9;
		private Label lbl_DIOType_10;
		private Label lbl_DIOType_11;
		private Label lbl_DIOType_12;
		private Label lbl_DIOType_13;
		private Label lbl_DIOType_14;
		private Label lbl_DIOType_15;
		private Label lbl_DIOType_2;
		private Label lbl_DIOType_3;
		private Label lbl_DIOType_4;
		private Label lbl_DIOType_5;
		private Label lbl_DIOType_6;
		private Label lbl_DIOType_22;
		private Label lbl_DIOType_21;
		private Label lbl_DIOType_20;
		private Label lbl_DIOType_19;
		private Label lbl_DIOType_18;
		private Label lbl_DIOType_31;
		private Label lbl_DIOType_30;
		private Label lbl_DIOType_29;
		private Label lbl_DIOType_28;
		private Label lbl_DIOType_27;
		private Label lbl_DIOType_26;
		private Label lbl_DIOType_25;
		private Label lbl_DIOType_24;
		private Label lbl_DIOType_23;
		private Label lbl_DIOName_26;
		private Label lbl_DIOName_25;
		private Label lbl_DIOName_24;
		private Label lbl_DIOName_31;
		private Label lbl_DIOName_30;
		private Label lbl_DIOName_29;
		private Label lbl_DIOName_28;
		private Label lbl_DIOName_23;
		private Label lbl_DIOName_22;
		private Label lbl_DIOName_21;
		private Label lbl_DIOName_20;
		private Label lbl_DIOName_27;
		private Label lbl_DIOName_18;
		private Label lbl_DIOName_19;
		private Label lbl_DIOType_17;
		private Label lbl_DIOName_17;
		private Label lbl_DIOType_16;
		private Label lbl_DIOName_16;
		private Label lbl_DIOStatus_29;
		private Label lbl_DIOChannel_29;
		private Label lbl_DIOChannel_30;
		private Label lbl_DIOChannel_31;
		private Label lbl_DIOStatus_26;
		private Label lbl_DIOStatus_28;
		private Label lbl_DIOStatus_30;
		private Label lbl_DIOStatus_31;
		private Label lbl_DIOStatus_27;
		private Label lbl_DIOStatus_25;
		private Label lbl_DIOStatus_24;
		private Label lbl_DIOStatus_23;
		private Label lbl_DIOStatus_22;
		private Label lbl_DIOStatus_21;
		private Label lbl_DIOStatus_20;
		private Label lbl_DIOStatus_19;
		private Label lbl_DIOStatus_18;
		private Label lbl_DIOStatus_17;
		private Label lbl_DIOChannel_28;
		private Label lbl_DIOChannel_25;
		private Label lbl_DIOChannel_26;
		private Label lbl_DIOChannel_23;
		private Label lbl_DIOChannel_24;
		private Label lbl_DIOChannel_21;
		private Label lbl_DIOChannel_22;
		private Label lbl_DIOChannel_16;
		private Label lbl_DIOStatus_16;
		private Label lbl_DIOChannel_17;
		private Label lbl_DIOChannel_18;
		private Label lbl_DIOChannel_19;
		private Label lbl_DIOChannel_20;
		private Label lbl_DIOChannel_27;
		private Panel pnl_DIOExpansion;
		private Label lbl_DIOType_37;
		private Label lbl_DIOType_36;
		private Label lbl_DIOType_35;
		private Label lbl_DIOType_34;
		private Label lbl_DIOType_47;
		private Label lbl_DIOType_46;
		private Label lbl_DIOType_45;
		private Label lbl_DIOType_44;
		private Label lbl_DIOType_43;
		private Label lbl_DIOType_42;
		private Label lbl_DIOType_41;
		private Label lbl_DIOType_40;
		private Label lbl_DIOType_39;
		private Label lbl_DIOName_42;
		private Label lbl_DIOName_41;
		private Label lbl_DIOName_40;
		private Label lbl_DIOName_47;
		private Label lbl_DIOName_46;
		private Label lbl_DIOName_45;
		private Label lbl_DIOName_44;
		private Label lbl_DIOName_39;
		private Label lbl_DIOName_38;
		private Label lbl_DIOName_37;
		private Label lbl_DIOName_36;
		private Label lbl_DIOName_43;
		private Label lbl_DIOName_34;
		private Label lbl_DIOName_35;
		private Label lbl_DIOType_33;
		private Label lbl_DIOName_33;
		private Label lbl_DIOStatus_45;
		private Label lbl_DIOChannel_45;
		private Label lbl_DIOChannel_46;
		private Label lbl_DIOChannel_47;
		private Label lbl_DIOStatus_42;
		private Label lbl_DIOStatus_44;
		private Label lbl_DIOStatus_46;
		private Label lbl_DIOStatus_47;
		private Label lbl_DIOStatus_43;
		private Label lbl_DIOStatus_41;
		private Label lbl_DIOStatus_40;
		private Label lbl_DIOStatus_39;
		private Label lbl_DIOStatus_38;
		private Label lbl_DIOStatus_37;
		private Label lbl_DIOStatus_36;
		private Label lbl_DIOStatus_35;
		private Label lbl_DIOStatus_34;
		private Label lbl_DIOStatus_33;
		private Label lbl_DIOChannel_44;
		private Label lbl_DIOChannel_41;
		private Label lbl_DIOChannel_42;
		private Label lbl_DIOChannel_39;
		private Label lbl_DIOChannel_40;
		private Label lbl_DIOChannel_37;
		private Label lbl_DIOChannel_38;
		private Label lbl_DIOChannel_33;
		private Label lbl_DIOChannel_34;
		private Label lbl_DIOChannel_35;
		private Label lbl_DIOChannel_36;
		private Label lbl_DIOChannel_32;
		private Label lbl_DIOChannel_43;
		private Label lbl_DIOType_32;
		private Label lbl_DIOName_32;
		private Label lbl_DIOStatus_32;
		private Label lbl_DIOType_49;
		private Label lbl_DIOType_48;
		private Label lbl_DIOName_49;
		private Label lbl_DIOName_48;
		private Label lbl_DIOChannel_48;
		private Label lbl_DIOChannel_49;
		private Label lbl_DIOStatus_48;
		private Label lbl_DIOStatus_49;
		private Label lbl_DIOType_38;
		private Label lbl_DIOType_50;
		private Label lbl_DIOType_67;
		private Label lbl_DIOType_66;
		private Label lbl_DIOName_67;
		private Label lbl_DIOName_66;
		private Label lbl_DIOChannel_66;
		private Label lbl_DIOChannel_67;
		private Label lbl_DIOStatus_66;
		private Label lbl_DIOStatus_67;
		private Label lbl_DIOType_56;
		private Label lbl_DIOType_55;
		private Label lbl_DIOType_54;
		private Label lbl_DIOType_53;
		private Label lbl_DIOType_52;
		private Label lbl_DIOType_65;
		private Label lbl_DIOType_64;
		private Label lbl_DIOType_63;
		private Label lbl_DIOType_62;
		private Label lbl_DIOType_61;
		private Label lbl_DIOType_60;
		private Label lbl_DIOType_59;
		private Label lbl_DIOType_58;
		private Label lbl_DIOType_57;
		private Label lbl_DIOName_60;
		private Label lbl_DIOName_59;
		private Label lbl_DIOName_58;
		private Label lbl_DIOName_65;
		private Label lbl_DIOName_64;
		private Label lbl_DIOName_63;
		private Label lbl_DIOName_62;
		private Label lbl_DIOName_57;
		private Label lbl_DIOName_56;
		private Label lbl_DIOName_55;
		private Label lbl_DIOName_54;
		private Label lbl_DIOName_61;
		private Label lbl_DIOName_52;
		private Label lbl_DIOName_53;
		private Label lbl_DIOType_51;
		private Label lbl_DIOName_51;
		private Label lbl_DIOStatus_63;
		private Label lbl_DIOChannel_63;
		private Label lbl_DIOName_50;
		private Label lbl_DIOChannel_64;
		private Label lbl_DIOChannel_65;
		private Label lbl_DIOStatus_60;
		private Label lbl_DIOStatus_62;
		private Label lbl_DIOStatus_64;
		private Label lbl_DIOStatus_65;
		private Label lbl_DIOStatus_61;
		private Label lbl_DIOStatus_59;
		private Label lbl_DIOStatus_58;
		private Label lbl_DIOStatus_57;
		private Label lbl_DIOStatus_56;
		private Label lbl_DIOStatus_55;
		private Label lbl_DIOStatus_54;
		private Label lbl_DIOStatus_53;
		private Label lbl_DIOStatus_52;
		private Label lbl_DIOStatus_51;
		private Label lbl_DIOChannel_62;
		private Label lbl_DIOChannel_59;
		private Label lbl_DIOChannel_60;
		private Label lbl_DIOChannel_57;
		private Label lbl_DIOChannel_58;
		private Label lbl_DIOChannel_55;
		private Label lbl_DIOChannel_56;
		private Label lbl_DIOStatus_50;
		private Label lbl_DIOChannel_51;
		private Label lbl_DIOChannel_52;
		private Label lbl_DIOChannel_53;
		private Label lbl_DIOChannel_54;
		private Label lbl_DIOChannel_50;
		private Label lbl_DIOChannel_61;
		private Panel pnl_DIOMain;
		private System.ComponentModel.IContainer components = null;

		private int lastValue = 0;

		public Frm_DIOStatus()
		{
			InitializeComponent();

      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      if (!Accessibility.getAccessibility().isLevelHigh())
      {
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
      }

			this.F10();
			this._clearDIOStatus();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		protected override void F5()
		{
		}

		protected override void F10()
		{
			this.pnl_DIOMain.Visible = true;
			this.pnl_DIOMain.BringToFront();
			this.pnl_DIOExpansion.Visible = false;
			this.pnl_DIOExpansion.SendToBack();
			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
		}

		protected override void F11()
		{
			this.pnl_DIOMain.Visible = false;
			this.pnl_DIOMain.SendToBack();
			this.pnl_DIOExpansion.Visible = true;
			this.pnl_DIOExpansion.BringToFront();
			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
		}

		protected override void CtrlF1()
		{
			Frm_AskValue askValue = new Frm_AskValue();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_26");
			FosberDialogResult ret = askValue.ShowDialog(this, text, PageManager.getPageManager().pagesHistorian.CurrentDSPModule);

			if (ret.Dialog == DialogResult.OK)
			{
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule = (int)ret.Data[0];
				SendDIOModule();
			}
		}

		protected override void CtrlF2()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				Frm_AskValue askValue = new Frm_AskValue();
				string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_23");
				FosberDialogResult ret = askValue.ShowDialog(this, text, lastValue);

				if (ret.Dialog == DialogResult.OK)
				{
					CommunicationManager cm = CommunicationManager.GetCommunicationManager();
					cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STARTER + "." + Command.PULSE_DIGOUT + "(" +
						ret.Data[0] + ")");
					this.lastValue = (int)ret.Data[0];
				}
			}
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      //else
      //{
      //  PageManager.getPageManager()._SetMessage("§SYCC_11");
      //}
		}

		protected override void CtrlF3()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				Frm_AskValue askValue = new Frm_AskValue();
				string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_24");
				FosberDialogResult ret = askValue.ShowDialog(this, text, lastValue);

				if (ret.Dialog == DialogResult.OK)
				{
					CommunicationManager cm = CommunicationManager.GetCommunicationManager();
					cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STARTER + "." + Command.SET_DIGOUT + "(" +
						ret.Data[0] + ")");
					this.lastValue = (int)ret.Data[0];
				}
			}
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      //else
      //{
      //  PageManager.getPageManager()._SetMessage("§SYCC_11");
      //}
		}

		protected override void CtrlF4()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				Frm_AskValue askValue = new Frm_AskValue();
				string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_25");
				FosberDialogResult ret = askValue.ShowDialog(this, text, lastValue);

				if (ret.Dialog == DialogResult.OK)
				{
					CommunicationManager cm = CommunicationManager.GetCommunicationManager();
					cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STARTER + "." + Command.CLEAR_DIGOUT + "(" +
						ret.Data[0] + ")");
					this.lastValue = (int)ret.Data[0];
				}
			}
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      //else
      //{
      //  PageManager.getPageManager()._SetMessage("§SYCC_11");
      //}
		}

		protected override void CtrlF5()
		{
			
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DIOStatus));
      this.lbl_DIOChannel_0 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_1 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_2 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_3 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_4 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_6 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_5 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_8 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_7 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_10 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_11 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_9 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_12 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_0 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_1 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_2 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_3 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_4 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_5 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_6 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_7 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_8 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_9 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_11 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_15 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_14 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_12 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_10 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_13 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_13 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_14 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_15 = new System.Windows.Forms.Label();
      this.lbl_DIOName_0 = new System.Windows.Forms.Label();
      this.lbl_DIOType_0 = new System.Windows.Forms.Label();
      this.lbl_DIOType_1 = new System.Windows.Forms.Label();
      this.lbl_DIOName_1 = new System.Windows.Forms.Label();
      this.lbl_DIOName_3 = new System.Windows.Forms.Label();
      this.lbl_DIOName_2 = new System.Windows.Forms.Label();
      this.lbl_DIOName_11 = new System.Windows.Forms.Label();
      this.lbl_DIOName_4 = new System.Windows.Forms.Label();
      this.lbl_DIOName_5 = new System.Windows.Forms.Label();
      this.lbl_DIOName_6 = new System.Windows.Forms.Label();
      this.lbl_DIOName_7 = new System.Windows.Forms.Label();
      this.lbl_DIOName_12 = new System.Windows.Forms.Label();
      this.lbl_DIOName_13 = new System.Windows.Forms.Label();
      this.lbl_DIOName_14 = new System.Windows.Forms.Label();
      this.lbl_DIOName_15 = new System.Windows.Forms.Label();
      this.lbl_DIOName_8 = new System.Windows.Forms.Label();
      this.lbl_DIOName_9 = new System.Windows.Forms.Label();
      this.lbl_DIOName_10 = new System.Windows.Forms.Label();
      this.lbl_DIOType_7 = new System.Windows.Forms.Label();
      this.lbl_DIOType_8 = new System.Windows.Forms.Label();
      this.lbl_DIOType_9 = new System.Windows.Forms.Label();
      this.lbl_DIOType_10 = new System.Windows.Forms.Label();
      this.lbl_DIOType_11 = new System.Windows.Forms.Label();
      this.lbl_DIOType_12 = new System.Windows.Forms.Label();
      this.lbl_DIOType_13 = new System.Windows.Forms.Label();
      this.lbl_DIOType_14 = new System.Windows.Forms.Label();
      this.lbl_DIOType_15 = new System.Windows.Forms.Label();
      this.lbl_DIOType_2 = new System.Windows.Forms.Label();
      this.lbl_DIOType_3 = new System.Windows.Forms.Label();
      this.lbl_DIOType_4 = new System.Windows.Forms.Label();
      this.lbl_DIOType_5 = new System.Windows.Forms.Label();
      this.lbl_DIOType_6 = new System.Windows.Forms.Label();
      this.lbl_DIOType_22 = new System.Windows.Forms.Label();
      this.lbl_DIOType_21 = new System.Windows.Forms.Label();
      this.lbl_DIOType_20 = new System.Windows.Forms.Label();
      this.lbl_DIOType_19 = new System.Windows.Forms.Label();
      this.lbl_DIOType_18 = new System.Windows.Forms.Label();
      this.lbl_DIOType_31 = new System.Windows.Forms.Label();
      this.lbl_DIOType_30 = new System.Windows.Forms.Label();
      this.lbl_DIOType_29 = new System.Windows.Forms.Label();
      this.lbl_DIOType_28 = new System.Windows.Forms.Label();
      this.lbl_DIOType_27 = new System.Windows.Forms.Label();
      this.lbl_DIOType_26 = new System.Windows.Forms.Label();
      this.lbl_DIOType_25 = new System.Windows.Forms.Label();
      this.lbl_DIOType_24 = new System.Windows.Forms.Label();
      this.lbl_DIOType_23 = new System.Windows.Forms.Label();
      this.lbl_DIOName_26 = new System.Windows.Forms.Label();
      this.lbl_DIOName_25 = new System.Windows.Forms.Label();
      this.lbl_DIOName_24 = new System.Windows.Forms.Label();
      this.lbl_DIOName_31 = new System.Windows.Forms.Label();
      this.lbl_DIOName_30 = new System.Windows.Forms.Label();
      this.lbl_DIOName_29 = new System.Windows.Forms.Label();
      this.lbl_DIOName_28 = new System.Windows.Forms.Label();
      this.lbl_DIOName_23 = new System.Windows.Forms.Label();
      this.lbl_DIOName_22 = new System.Windows.Forms.Label();
      this.lbl_DIOName_21 = new System.Windows.Forms.Label();
      this.lbl_DIOName_20 = new System.Windows.Forms.Label();
      this.lbl_DIOName_27 = new System.Windows.Forms.Label();
      this.lbl_DIOName_18 = new System.Windows.Forms.Label();
      this.lbl_DIOName_19 = new System.Windows.Forms.Label();
      this.lbl_DIOType_17 = new System.Windows.Forms.Label();
      this.lbl_DIOName_17 = new System.Windows.Forms.Label();
      this.lbl_DIOType_16 = new System.Windows.Forms.Label();
      this.lbl_DIOName_16 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_29 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_29 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_30 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_31 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_26 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_28 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_30 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_31 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_27 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_25 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_24 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_23 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_22 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_21 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_20 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_19 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_18 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_17 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_28 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_25 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_26 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_23 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_24 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_21 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_22 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_16 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_16 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_17 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_18 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_19 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_20 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_27 = new System.Windows.Forms.Label();
      this.pnl_DIOExpansion = new System.Windows.Forms.Panel();
      this.lbl_DIOType_50 = new System.Windows.Forms.Label();
      this.lbl_DIOType_67 = new System.Windows.Forms.Label();
      this.lbl_DIOType_66 = new System.Windows.Forms.Label();
      this.lbl_DIOName_67 = new System.Windows.Forms.Label();
      this.lbl_DIOName_66 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_66 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_67 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_66 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_67 = new System.Windows.Forms.Label();
      this.lbl_DIOType_56 = new System.Windows.Forms.Label();
      this.lbl_DIOType_55 = new System.Windows.Forms.Label();
      this.lbl_DIOType_54 = new System.Windows.Forms.Label();
      this.lbl_DIOType_53 = new System.Windows.Forms.Label();
      this.lbl_DIOType_52 = new System.Windows.Forms.Label();
      this.lbl_DIOType_65 = new System.Windows.Forms.Label();
      this.lbl_DIOType_64 = new System.Windows.Forms.Label();
      this.lbl_DIOType_63 = new System.Windows.Forms.Label();
      this.lbl_DIOType_62 = new System.Windows.Forms.Label();
      this.lbl_DIOType_61 = new System.Windows.Forms.Label();
      this.lbl_DIOType_60 = new System.Windows.Forms.Label();
      this.lbl_DIOType_59 = new System.Windows.Forms.Label();
      this.lbl_DIOType_58 = new System.Windows.Forms.Label();
      this.lbl_DIOType_57 = new System.Windows.Forms.Label();
      this.lbl_DIOName_60 = new System.Windows.Forms.Label();
      this.lbl_DIOName_59 = new System.Windows.Forms.Label();
      this.lbl_DIOName_58 = new System.Windows.Forms.Label();
      this.lbl_DIOName_65 = new System.Windows.Forms.Label();
      this.lbl_DIOName_64 = new System.Windows.Forms.Label();
      this.lbl_DIOName_63 = new System.Windows.Forms.Label();
      this.lbl_DIOName_62 = new System.Windows.Forms.Label();
      this.lbl_DIOName_57 = new System.Windows.Forms.Label();
      this.lbl_DIOName_56 = new System.Windows.Forms.Label();
      this.lbl_DIOName_55 = new System.Windows.Forms.Label();
      this.lbl_DIOName_54 = new System.Windows.Forms.Label();
      this.lbl_DIOName_61 = new System.Windows.Forms.Label();
      this.lbl_DIOName_52 = new System.Windows.Forms.Label();
      this.lbl_DIOName_53 = new System.Windows.Forms.Label();
      this.lbl_DIOType_51 = new System.Windows.Forms.Label();
      this.lbl_DIOName_51 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_63 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_63 = new System.Windows.Forms.Label();
      this.lbl_DIOName_50 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_64 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_65 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_60 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_62 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_64 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_65 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_61 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_59 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_58 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_57 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_56 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_55 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_54 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_53 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_52 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_51 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_62 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_59 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_60 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_57 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_58 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_55 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_56 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_50 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_51 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_52 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_53 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_54 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_50 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_61 = new System.Windows.Forms.Label();
      this.lbl_DIOType_32 = new System.Windows.Forms.Label();
      this.lbl_DIOType_49 = new System.Windows.Forms.Label();
      this.lbl_DIOType_48 = new System.Windows.Forms.Label();
      this.lbl_DIOName_49 = new System.Windows.Forms.Label();
      this.lbl_DIOName_48 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_48 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_49 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_48 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_49 = new System.Windows.Forms.Label();
      this.lbl_DIOType_38 = new System.Windows.Forms.Label();
      this.lbl_DIOType_37 = new System.Windows.Forms.Label();
      this.lbl_DIOType_36 = new System.Windows.Forms.Label();
      this.lbl_DIOType_35 = new System.Windows.Forms.Label();
      this.lbl_DIOType_34 = new System.Windows.Forms.Label();
      this.lbl_DIOType_47 = new System.Windows.Forms.Label();
      this.lbl_DIOType_46 = new System.Windows.Forms.Label();
      this.lbl_DIOType_45 = new System.Windows.Forms.Label();
      this.lbl_DIOType_44 = new System.Windows.Forms.Label();
      this.lbl_DIOType_43 = new System.Windows.Forms.Label();
      this.lbl_DIOType_42 = new System.Windows.Forms.Label();
      this.lbl_DIOType_41 = new System.Windows.Forms.Label();
      this.lbl_DIOType_40 = new System.Windows.Forms.Label();
      this.lbl_DIOType_39 = new System.Windows.Forms.Label();
      this.lbl_DIOName_42 = new System.Windows.Forms.Label();
      this.lbl_DIOName_41 = new System.Windows.Forms.Label();
      this.lbl_DIOName_40 = new System.Windows.Forms.Label();
      this.lbl_DIOName_47 = new System.Windows.Forms.Label();
      this.lbl_DIOName_46 = new System.Windows.Forms.Label();
      this.lbl_DIOName_45 = new System.Windows.Forms.Label();
      this.lbl_DIOName_44 = new System.Windows.Forms.Label();
      this.lbl_DIOName_39 = new System.Windows.Forms.Label();
      this.lbl_DIOName_38 = new System.Windows.Forms.Label();
      this.lbl_DIOName_37 = new System.Windows.Forms.Label();
      this.lbl_DIOName_36 = new System.Windows.Forms.Label();
      this.lbl_DIOName_43 = new System.Windows.Forms.Label();
      this.lbl_DIOName_34 = new System.Windows.Forms.Label();
      this.lbl_DIOName_35 = new System.Windows.Forms.Label();
      this.lbl_DIOType_33 = new System.Windows.Forms.Label();
      this.lbl_DIOName_33 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_45 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_45 = new System.Windows.Forms.Label();
      this.lbl_DIOName_32 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_46 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_47 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_42 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_44 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_46 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_47 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_43 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_41 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_40 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_39 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_38 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_37 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_36 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_35 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_34 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_33 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_44 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_41 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_42 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_39 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_40 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_37 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_38 = new System.Windows.Forms.Label();
      this.lbl_DIOStatus_32 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_33 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_34 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_35 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_36 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_32 = new System.Windows.Forms.Label();
      this.lbl_DIOChannel_43 = new System.Windows.Forms.Label();
      this.pnl_DIOMain = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_DIOExpansion.SuspendLayout();
      this.pnl_DIOMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab5.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabA.Text = "0 - 31";
      this.tab_TabA.TranslateText = false;
      this.tab_TabA.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "31 - 67";
      this.tab_TabB.TranslateText = false;
      this.tab_TabB.Visible = true;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Clear Digout";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "Select DSP";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Pulse Digout";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Text = "Set Digiout";
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
      this.iml_navigationImages.Images.SetKeyName(0, "");
      this.iml_navigationImages.Images.SetKeyName(1, "");
      this.iml_navigationImages.Images.SetKeyName(2, "");
      this.iml_navigationImages.Images.SetKeyName(3, "");
      // 
      // btn_Group1
      // 
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // lbl_DIOChannel_0
      // 
      this.lbl_DIOChannel_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_0.Location = new System.Drawing.Point(35, 25);
      this.lbl_DIOChannel_0.Name = "lbl_DIOChannel_0";
      this.lbl_DIOChannel_0.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_0.TabIndex = 110;
      this.lbl_DIOChannel_0.Text = "0000";
      this.lbl_DIOChannel_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_1
      // 
      this.lbl_DIOChannel_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_1.Location = new System.Drawing.Point(35, 55);
      this.lbl_DIOChannel_1.Name = "lbl_DIOChannel_1";
      this.lbl_DIOChannel_1.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_1.TabIndex = 111;
      this.lbl_DIOChannel_1.Text = "0001";
      this.lbl_DIOChannel_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_2
      // 
      this.lbl_DIOChannel_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_2.Location = new System.Drawing.Point(35, 85);
      this.lbl_DIOChannel_2.Name = "lbl_DIOChannel_2";
      this.lbl_DIOChannel_2.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_2.TabIndex = 112;
      this.lbl_DIOChannel_2.Text = "0002";
      this.lbl_DIOChannel_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_3
      // 
      this.lbl_DIOChannel_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_3.Location = new System.Drawing.Point(35, 115);
      this.lbl_DIOChannel_3.Name = "lbl_DIOChannel_3";
      this.lbl_DIOChannel_3.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_3.TabIndex = 113;
      this.lbl_DIOChannel_3.Text = "0003";
      this.lbl_DIOChannel_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_4
      // 
      this.lbl_DIOChannel_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_4.Location = new System.Drawing.Point(35, 145);
      this.lbl_DIOChannel_4.Name = "lbl_DIOChannel_4";
      this.lbl_DIOChannel_4.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_4.TabIndex = 114;
      this.lbl_DIOChannel_4.Text = "0004";
      this.lbl_DIOChannel_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_6
      // 
      this.lbl_DIOChannel_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_6.Location = new System.Drawing.Point(35, 205);
      this.lbl_DIOChannel_6.Name = "lbl_DIOChannel_6";
      this.lbl_DIOChannel_6.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_6.TabIndex = 115;
      this.lbl_DIOChannel_6.Text = "0006";
      this.lbl_DIOChannel_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_5
      // 
      this.lbl_DIOChannel_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_5.Location = new System.Drawing.Point(35, 175);
      this.lbl_DIOChannel_5.Name = "lbl_DIOChannel_5";
      this.lbl_DIOChannel_5.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_5.TabIndex = 116;
      this.lbl_DIOChannel_5.Text = "0005";
      this.lbl_DIOChannel_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_8
      // 
      this.lbl_DIOChannel_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_8.Location = new System.Drawing.Point(35, 265);
      this.lbl_DIOChannel_8.Name = "lbl_DIOChannel_8";
      this.lbl_DIOChannel_8.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_8.TabIndex = 117;
      this.lbl_DIOChannel_8.Text = "0008";
      this.lbl_DIOChannel_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_7
      // 
      this.lbl_DIOChannel_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_7.Location = new System.Drawing.Point(35, 235);
      this.lbl_DIOChannel_7.Name = "lbl_DIOChannel_7";
      this.lbl_DIOChannel_7.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_7.TabIndex = 118;
      this.lbl_DIOChannel_7.Text = "0007";
      this.lbl_DIOChannel_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_10
      // 
      this.lbl_DIOChannel_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_10.Location = new System.Drawing.Point(35, 325);
      this.lbl_DIOChannel_10.Name = "lbl_DIOChannel_10";
      this.lbl_DIOChannel_10.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_10.TabIndex = 119;
      this.lbl_DIOChannel_10.Text = "0010";
      this.lbl_DIOChannel_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_11
      // 
      this.lbl_DIOChannel_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_11.Location = new System.Drawing.Point(35, 355);
      this.lbl_DIOChannel_11.Name = "lbl_DIOChannel_11";
      this.lbl_DIOChannel_11.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_11.TabIndex = 120;
      this.lbl_DIOChannel_11.Text = "0011";
      this.lbl_DIOChannel_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_9
      // 
      this.lbl_DIOChannel_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_9.Location = new System.Drawing.Point(35, 295);
      this.lbl_DIOChannel_9.Name = "lbl_DIOChannel_9";
      this.lbl_DIOChannel_9.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_9.TabIndex = 121;
      this.lbl_DIOChannel_9.Text = "0009";
      this.lbl_DIOChannel_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_12
      // 
      this.lbl_DIOChannel_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_12.Location = new System.Drawing.Point(35, 385);
      this.lbl_DIOChannel_12.Name = "lbl_DIOChannel_12";
      this.lbl_DIOChannel_12.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_12.TabIndex = 122;
      this.lbl_DIOChannel_12.Text = "0012";
      this.lbl_DIOChannel_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_0
      // 
      this.lbl_DIOStatus_0.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_0.Location = new System.Drawing.Point(522, 25);
      this.lbl_DIOStatus_0.Name = "lbl_DIOStatus_0";
      this.lbl_DIOStatus_0.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_0.TabIndex = 123;
      // 
      // lbl_DIOStatus_1
      // 
      this.lbl_DIOStatus_1.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_1.Location = new System.Drawing.Point(522, 55);
      this.lbl_DIOStatus_1.Name = "lbl_DIOStatus_1";
      this.lbl_DIOStatus_1.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_1.TabIndex = 124;
      // 
      // lbl_DIOStatus_2
      // 
      this.lbl_DIOStatus_2.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_2.Location = new System.Drawing.Point(522, 85);
      this.lbl_DIOStatus_2.Name = "lbl_DIOStatus_2";
      this.lbl_DIOStatus_2.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_2.TabIndex = 125;
      // 
      // lbl_DIOStatus_3
      // 
      this.lbl_DIOStatus_3.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_3.Location = new System.Drawing.Point(522, 115);
      this.lbl_DIOStatus_3.Name = "lbl_DIOStatus_3";
      this.lbl_DIOStatus_3.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_3.TabIndex = 126;
      // 
      // lbl_DIOStatus_4
      // 
      this.lbl_DIOStatus_4.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_4.Location = new System.Drawing.Point(522, 145);
      this.lbl_DIOStatus_4.Name = "lbl_DIOStatus_4";
      this.lbl_DIOStatus_4.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_4.TabIndex = 127;
      // 
      // lbl_DIOStatus_5
      // 
      this.lbl_DIOStatus_5.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_5.Location = new System.Drawing.Point(522, 175);
      this.lbl_DIOStatus_5.Name = "lbl_DIOStatus_5";
      this.lbl_DIOStatus_5.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_5.TabIndex = 128;
      // 
      // lbl_DIOStatus_6
      // 
      this.lbl_DIOStatus_6.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_6.Location = new System.Drawing.Point(522, 205);
      this.lbl_DIOStatus_6.Name = "lbl_DIOStatus_6";
      this.lbl_DIOStatus_6.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_6.TabIndex = 129;
      // 
      // lbl_DIOStatus_7
      // 
      this.lbl_DIOStatus_7.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_7.Location = new System.Drawing.Point(522, 235);
      this.lbl_DIOStatus_7.Name = "lbl_DIOStatus_7";
      this.lbl_DIOStatus_7.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_7.TabIndex = 130;
      // 
      // lbl_DIOStatus_8
      // 
      this.lbl_DIOStatus_8.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_8.Location = new System.Drawing.Point(522, 265);
      this.lbl_DIOStatus_8.Name = "lbl_DIOStatus_8";
      this.lbl_DIOStatus_8.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_8.TabIndex = 131;
      // 
      // lbl_DIOStatus_9
      // 
      this.lbl_DIOStatus_9.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_9.Location = new System.Drawing.Point(522, 295);
      this.lbl_DIOStatus_9.Name = "lbl_DIOStatus_9";
      this.lbl_DIOStatus_9.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_9.TabIndex = 132;
      // 
      // lbl_DIOStatus_11
      // 
      this.lbl_DIOStatus_11.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_11.Location = new System.Drawing.Point(522, 355);
      this.lbl_DIOStatus_11.Name = "lbl_DIOStatus_11";
      this.lbl_DIOStatus_11.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_11.TabIndex = 133;
      // 
      // lbl_DIOStatus_15
      // 
      this.lbl_DIOStatus_15.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_15.Location = new System.Drawing.Point(522, 475);
      this.lbl_DIOStatus_15.Name = "lbl_DIOStatus_15";
      this.lbl_DIOStatus_15.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_15.TabIndex = 134;
      // 
      // lbl_DIOStatus_14
      // 
      this.lbl_DIOStatus_14.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_14.Location = new System.Drawing.Point(522, 445);
      this.lbl_DIOStatus_14.Name = "lbl_DIOStatus_14";
      this.lbl_DIOStatus_14.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_14.TabIndex = 135;
      // 
      // lbl_DIOStatus_12
      // 
      this.lbl_DIOStatus_12.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_12.Location = new System.Drawing.Point(522, 385);
      this.lbl_DIOStatus_12.Name = "lbl_DIOStatus_12";
      this.lbl_DIOStatus_12.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_12.TabIndex = 136;
      // 
      // lbl_DIOStatus_10
      // 
      this.lbl_DIOStatus_10.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_10.Location = new System.Drawing.Point(522, 325);
      this.lbl_DIOStatus_10.Name = "lbl_DIOStatus_10";
      this.lbl_DIOStatus_10.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_10.TabIndex = 137;
      // 
      // lbl_DIOStatus_13
      // 
      this.lbl_DIOStatus_13.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_13.Location = new System.Drawing.Point(522, 415);
      this.lbl_DIOStatus_13.Name = "lbl_DIOStatus_13";
      this.lbl_DIOStatus_13.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_13.TabIndex = 142;
      // 
      // lbl_DIOChannel_13
      // 
      this.lbl_DIOChannel_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_13.Location = new System.Drawing.Point(35, 415);
      this.lbl_DIOChannel_13.Name = "lbl_DIOChannel_13";
      this.lbl_DIOChannel_13.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_13.TabIndex = 140;
      this.lbl_DIOChannel_13.Text = "0013";
      this.lbl_DIOChannel_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_14
      // 
      this.lbl_DIOChannel_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_14.Location = new System.Drawing.Point(35, 445);
      this.lbl_DIOChannel_14.Name = "lbl_DIOChannel_14";
      this.lbl_DIOChannel_14.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_14.TabIndex = 138;
      this.lbl_DIOChannel_14.Text = "0014";
      this.lbl_DIOChannel_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_15
      // 
      this.lbl_DIOChannel_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_15.Location = new System.Drawing.Point(35, 475);
      this.lbl_DIOChannel_15.Name = "lbl_DIOChannel_15";
      this.lbl_DIOChannel_15.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_15.TabIndex = 139;
      this.lbl_DIOChannel_15.Text = "0015";
      this.lbl_DIOChannel_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_0
      // 
      this.lbl_DIOName_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_0.Location = new System.Drawing.Point(93, 25);
      this.lbl_DIOName_0.Name = "lbl_DIOName_0";
      this.lbl_DIOName_0.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_0.TabIndex = 147;
      this.lbl_DIOName_0.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_0
      // 
      this.lbl_DIOType_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_0.Location = new System.Drawing.Point(448, 25);
      this.lbl_DIOType_0.Name = "lbl_DIOType_0";
      this.lbl_DIOType_0.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_0.TabIndex = 148;
      this.lbl_DIOType_0.Text = "Enc Out";
      this.lbl_DIOType_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_1
      // 
      this.lbl_DIOType_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_1.Location = new System.Drawing.Point(448, 55);
      this.lbl_DIOType_1.Name = "lbl_DIOType_1";
      this.lbl_DIOType_1.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_1.TabIndex = 150;
      this.lbl_DIOType_1.Text = "Enc Out";
      this.lbl_DIOType_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_1
      // 
      this.lbl_DIOName_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_1.Location = new System.Drawing.Point(93, 55);
      this.lbl_DIOName_1.Name = "lbl_DIOName_1";
      this.lbl_DIOName_1.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_1.TabIndex = 149;
      this.lbl_DIOName_1.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_3
      // 
      this.lbl_DIOName_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_3.Location = new System.Drawing.Point(93, 115);
      this.lbl_DIOName_3.Name = "lbl_DIOName_3";
      this.lbl_DIOName_3.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_3.TabIndex = 151;
      this.lbl_DIOName_3.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_2
      // 
      this.lbl_DIOName_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_2.Location = new System.Drawing.Point(93, 85);
      this.lbl_DIOName_2.Name = "lbl_DIOName_2";
      this.lbl_DIOName_2.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_2.TabIndex = 152;
      this.lbl_DIOName_2.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_11
      // 
      this.lbl_DIOName_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_11.Location = new System.Drawing.Point(93, 355);
      this.lbl_DIOName_11.Name = "lbl_DIOName_11";
      this.lbl_DIOName_11.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_11.TabIndex = 153;
      this.lbl_DIOName_11.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_4
      // 
      this.lbl_DIOName_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_4.Location = new System.Drawing.Point(93, 145);
      this.lbl_DIOName_4.Name = "lbl_DIOName_4";
      this.lbl_DIOName_4.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_4.TabIndex = 154;
      this.lbl_DIOName_4.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_5
      // 
      this.lbl_DIOName_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_5.Location = new System.Drawing.Point(93, 175);
      this.lbl_DIOName_5.Name = "lbl_DIOName_5";
      this.lbl_DIOName_5.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_5.TabIndex = 157;
      this.lbl_DIOName_5.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_6
      // 
      this.lbl_DIOName_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_6.Location = new System.Drawing.Point(93, 205);
      this.lbl_DIOName_6.Name = "lbl_DIOName_6";
      this.lbl_DIOName_6.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_6.TabIndex = 158;
      this.lbl_DIOName_6.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_7
      // 
      this.lbl_DIOName_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_7.Location = new System.Drawing.Point(93, 235);
      this.lbl_DIOName_7.Name = "lbl_DIOName_7";
      this.lbl_DIOName_7.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_7.TabIndex = 159;
      this.lbl_DIOName_7.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_12
      // 
      this.lbl_DIOName_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_12.Location = new System.Drawing.Point(93, 385);
      this.lbl_DIOName_12.Name = "lbl_DIOName_12";
      this.lbl_DIOName_12.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_12.TabIndex = 160;
      this.lbl_DIOName_12.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_13
      // 
      this.lbl_DIOName_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_13.Location = new System.Drawing.Point(93, 415);
      this.lbl_DIOName_13.Name = "lbl_DIOName_13";
      this.lbl_DIOName_13.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_13.TabIndex = 161;
      this.lbl_DIOName_13.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_14
      // 
      this.lbl_DIOName_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_14.Location = new System.Drawing.Point(93, 445);
      this.lbl_DIOName_14.Name = "lbl_DIOName_14";
      this.lbl_DIOName_14.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_14.TabIndex = 162;
      this.lbl_DIOName_14.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_15
      // 
      this.lbl_DIOName_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_15.Location = new System.Drawing.Point(93, 475);
      this.lbl_DIOName_15.Name = "lbl_DIOName_15";
      this.lbl_DIOName_15.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_15.TabIndex = 163;
      this.lbl_DIOName_15.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_8
      // 
      this.lbl_DIOName_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_8.Location = new System.Drawing.Point(93, 265);
      this.lbl_DIOName_8.Name = "lbl_DIOName_8";
      this.lbl_DIOName_8.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_8.TabIndex = 164;
      this.lbl_DIOName_8.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_9
      // 
      this.lbl_DIOName_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_9.Location = new System.Drawing.Point(93, 295);
      this.lbl_DIOName_9.Name = "lbl_DIOName_9";
      this.lbl_DIOName_9.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_9.TabIndex = 165;
      this.lbl_DIOName_9.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_10
      // 
      this.lbl_DIOName_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_10.Location = new System.Drawing.Point(93, 325);
      this.lbl_DIOName_10.Name = "lbl_DIOName_10";
      this.lbl_DIOName_10.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_10.TabIndex = 166;
      this.lbl_DIOName_10.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_7
      // 
      this.lbl_DIOType_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_7.Location = new System.Drawing.Point(448, 235);
      this.lbl_DIOType_7.Name = "lbl_DIOType_7";
      this.lbl_DIOType_7.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_7.TabIndex = 167;
      this.lbl_DIOType_7.Text = "Enc Out";
      this.lbl_DIOType_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_8
      // 
      this.lbl_DIOType_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_8.Location = new System.Drawing.Point(448, 265);
      this.lbl_DIOType_8.Name = "lbl_DIOType_8";
      this.lbl_DIOType_8.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_8.TabIndex = 168;
      this.lbl_DIOType_8.Text = "Enc Out";
      this.lbl_DIOType_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_9
      // 
      this.lbl_DIOType_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_9.Location = new System.Drawing.Point(448, 295);
      this.lbl_DIOType_9.Name = "lbl_DIOType_9";
      this.lbl_DIOType_9.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_9.TabIndex = 169;
      this.lbl_DIOType_9.Text = "Enc Out";
      this.lbl_DIOType_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_10
      // 
      this.lbl_DIOType_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_10.Location = new System.Drawing.Point(448, 325);
      this.lbl_DIOType_10.Name = "lbl_DIOType_10";
      this.lbl_DIOType_10.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_10.TabIndex = 170;
      this.lbl_DIOType_10.Text = "Enc Out";
      this.lbl_DIOType_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_11
      // 
      this.lbl_DIOType_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_11.Location = new System.Drawing.Point(448, 355);
      this.lbl_DIOType_11.Name = "lbl_DIOType_11";
      this.lbl_DIOType_11.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_11.TabIndex = 171;
      this.lbl_DIOType_11.Text = "Enc Out";
      this.lbl_DIOType_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_12
      // 
      this.lbl_DIOType_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_12.Location = new System.Drawing.Point(448, 385);
      this.lbl_DIOType_12.Name = "lbl_DIOType_12";
      this.lbl_DIOType_12.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_12.TabIndex = 172;
      this.lbl_DIOType_12.Text = "Enc Out";
      this.lbl_DIOType_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_13
      // 
      this.lbl_DIOType_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_13.Location = new System.Drawing.Point(448, 415);
      this.lbl_DIOType_13.Name = "lbl_DIOType_13";
      this.lbl_DIOType_13.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_13.TabIndex = 173;
      this.lbl_DIOType_13.Text = "Enc Out";
      this.lbl_DIOType_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_14
      // 
      this.lbl_DIOType_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_14.Location = new System.Drawing.Point(448, 445);
      this.lbl_DIOType_14.Name = "lbl_DIOType_14";
      this.lbl_DIOType_14.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_14.TabIndex = 174;
      this.lbl_DIOType_14.Text = "Enc Out";
      this.lbl_DIOType_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_15
      // 
      this.lbl_DIOType_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_15.Location = new System.Drawing.Point(448, 475);
      this.lbl_DIOType_15.Name = "lbl_DIOType_15";
      this.lbl_DIOType_15.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_15.TabIndex = 175;
      this.lbl_DIOType_15.Text = "Enc Out";
      this.lbl_DIOType_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_2
      // 
      this.lbl_DIOType_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_2.Location = new System.Drawing.Point(448, 85);
      this.lbl_DIOType_2.Name = "lbl_DIOType_2";
      this.lbl_DIOType_2.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_2.TabIndex = 176;
      this.lbl_DIOType_2.Text = "Enc Out";
      this.lbl_DIOType_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_3
      // 
      this.lbl_DIOType_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_3.Location = new System.Drawing.Point(448, 115);
      this.lbl_DIOType_3.Name = "lbl_DIOType_3";
      this.lbl_DIOType_3.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_3.TabIndex = 177;
      this.lbl_DIOType_3.Text = "Enc Out";
      this.lbl_DIOType_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_4
      // 
      this.lbl_DIOType_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_4.Location = new System.Drawing.Point(448, 145);
      this.lbl_DIOType_4.Name = "lbl_DIOType_4";
      this.lbl_DIOType_4.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_4.TabIndex = 178;
      this.lbl_DIOType_4.Text = "Enc Out";
      this.lbl_DIOType_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_5
      // 
      this.lbl_DIOType_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_5.Location = new System.Drawing.Point(448, 175);
      this.lbl_DIOType_5.Name = "lbl_DIOType_5";
      this.lbl_DIOType_5.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_5.TabIndex = 179;
      this.lbl_DIOType_5.Text = "Enc Out";
      this.lbl_DIOType_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_6
      // 
      this.lbl_DIOType_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_6.Location = new System.Drawing.Point(448, 205);
      this.lbl_DIOType_6.Name = "lbl_DIOType_6";
      this.lbl_DIOType_6.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_6.TabIndex = 180;
      this.lbl_DIOType_6.Text = "Enc Out";
      this.lbl_DIOType_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_22
      // 
      this.lbl_DIOType_22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_22.Location = new System.Drawing.Point(973, 205);
      this.lbl_DIOType_22.Name = "lbl_DIOType_22";
      this.lbl_DIOType_22.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_22.TabIndex = 244;
      this.lbl_DIOType_22.Text = "Enc Out";
      this.lbl_DIOType_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_21
      // 
      this.lbl_DIOType_21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_21.Location = new System.Drawing.Point(973, 175);
      this.lbl_DIOType_21.Name = "lbl_DIOType_21";
      this.lbl_DIOType_21.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_21.TabIndex = 243;
      this.lbl_DIOType_21.Text = "Enc Out";
      this.lbl_DIOType_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_20
      // 
      this.lbl_DIOType_20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_20.Location = new System.Drawing.Point(973, 145);
      this.lbl_DIOType_20.Name = "lbl_DIOType_20";
      this.lbl_DIOType_20.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_20.TabIndex = 242;
      this.lbl_DIOType_20.Text = "Enc Out";
      this.lbl_DIOType_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_19
      // 
      this.lbl_DIOType_19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_19.Location = new System.Drawing.Point(973, 115);
      this.lbl_DIOType_19.Name = "lbl_DIOType_19";
      this.lbl_DIOType_19.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_19.TabIndex = 241;
      this.lbl_DIOType_19.Text = "Enc Out";
      this.lbl_DIOType_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_18
      // 
      this.lbl_DIOType_18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_18.Location = new System.Drawing.Point(973, 85);
      this.lbl_DIOType_18.Name = "lbl_DIOType_18";
      this.lbl_DIOType_18.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_18.TabIndex = 240;
      this.lbl_DIOType_18.Text = "Enc Out";
      this.lbl_DIOType_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_31
      // 
      this.lbl_DIOType_31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_31.Location = new System.Drawing.Point(973, 475);
      this.lbl_DIOType_31.Name = "lbl_DIOType_31";
      this.lbl_DIOType_31.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_31.TabIndex = 239;
      this.lbl_DIOType_31.Text = "Enc Out";
      this.lbl_DIOType_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_30
      // 
      this.lbl_DIOType_30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_30.Location = new System.Drawing.Point(973, 445);
      this.lbl_DIOType_30.Name = "lbl_DIOType_30";
      this.lbl_DIOType_30.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_30.TabIndex = 238;
      this.lbl_DIOType_30.Text = "Enc Out";
      this.lbl_DIOType_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_29
      // 
      this.lbl_DIOType_29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_29.Location = new System.Drawing.Point(973, 415);
      this.lbl_DIOType_29.Name = "lbl_DIOType_29";
      this.lbl_DIOType_29.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_29.TabIndex = 237;
      this.lbl_DIOType_29.Text = "Enc Out";
      this.lbl_DIOType_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_28
      // 
      this.lbl_DIOType_28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_28.Location = new System.Drawing.Point(973, 385);
      this.lbl_DIOType_28.Name = "lbl_DIOType_28";
      this.lbl_DIOType_28.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_28.TabIndex = 236;
      this.lbl_DIOType_28.Text = "Enc Out";
      this.lbl_DIOType_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_27
      // 
      this.lbl_DIOType_27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_27.Location = new System.Drawing.Point(973, 355);
      this.lbl_DIOType_27.Name = "lbl_DIOType_27";
      this.lbl_DIOType_27.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_27.TabIndex = 235;
      this.lbl_DIOType_27.Text = "Enc Out";
      this.lbl_DIOType_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_26
      // 
      this.lbl_DIOType_26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_26.Location = new System.Drawing.Point(973, 325);
      this.lbl_DIOType_26.Name = "lbl_DIOType_26";
      this.lbl_DIOType_26.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_26.TabIndex = 234;
      this.lbl_DIOType_26.Text = "Enc Out";
      this.lbl_DIOType_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_25
      // 
      this.lbl_DIOType_25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_25.Location = new System.Drawing.Point(973, 295);
      this.lbl_DIOType_25.Name = "lbl_DIOType_25";
      this.lbl_DIOType_25.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_25.TabIndex = 233;
      this.lbl_DIOType_25.Text = "Enc Out";
      this.lbl_DIOType_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_24
      // 
      this.lbl_DIOType_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_24.Location = new System.Drawing.Point(973, 265);
      this.lbl_DIOType_24.Name = "lbl_DIOType_24";
      this.lbl_DIOType_24.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_24.TabIndex = 232;
      this.lbl_DIOType_24.Text = "Enc Out";
      this.lbl_DIOType_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_23
      // 
      this.lbl_DIOType_23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_23.Location = new System.Drawing.Point(973, 235);
      this.lbl_DIOType_23.Name = "lbl_DIOType_23";
      this.lbl_DIOType_23.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_23.TabIndex = 231;
      this.lbl_DIOType_23.Text = "Enc Out";
      this.lbl_DIOType_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_26
      // 
      this.lbl_DIOName_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_26.Location = new System.Drawing.Point(618, 325);
      this.lbl_DIOName_26.Name = "lbl_DIOName_26";
      this.lbl_DIOName_26.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_26.TabIndex = 230;
      this.lbl_DIOName_26.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_25
      // 
      this.lbl_DIOName_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_25.Location = new System.Drawing.Point(618, 295);
      this.lbl_DIOName_25.Name = "lbl_DIOName_25";
      this.lbl_DIOName_25.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_25.TabIndex = 229;
      this.lbl_DIOName_25.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_24
      // 
      this.lbl_DIOName_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_24.Location = new System.Drawing.Point(618, 265);
      this.lbl_DIOName_24.Name = "lbl_DIOName_24";
      this.lbl_DIOName_24.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_24.TabIndex = 228;
      this.lbl_DIOName_24.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_31
      // 
      this.lbl_DIOName_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_31.Location = new System.Drawing.Point(618, 475);
      this.lbl_DIOName_31.Name = "lbl_DIOName_31";
      this.lbl_DIOName_31.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_31.TabIndex = 227;
      this.lbl_DIOName_31.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_30
      // 
      this.lbl_DIOName_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_30.Location = new System.Drawing.Point(618, 445);
      this.lbl_DIOName_30.Name = "lbl_DIOName_30";
      this.lbl_DIOName_30.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_30.TabIndex = 226;
      this.lbl_DIOName_30.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_29
      // 
      this.lbl_DIOName_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_29.Location = new System.Drawing.Point(618, 415);
      this.lbl_DIOName_29.Name = "lbl_DIOName_29";
      this.lbl_DIOName_29.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_29.TabIndex = 225;
      this.lbl_DIOName_29.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_28
      // 
      this.lbl_DIOName_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_28.Location = new System.Drawing.Point(618, 385);
      this.lbl_DIOName_28.Name = "lbl_DIOName_28";
      this.lbl_DIOName_28.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_28.TabIndex = 224;
      this.lbl_DIOName_28.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_23
      // 
      this.lbl_DIOName_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_23.Location = new System.Drawing.Point(618, 235);
      this.lbl_DIOName_23.Name = "lbl_DIOName_23";
      this.lbl_DIOName_23.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_23.TabIndex = 223;
      this.lbl_DIOName_23.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_22
      // 
      this.lbl_DIOName_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_22.Location = new System.Drawing.Point(618, 205);
      this.lbl_DIOName_22.Name = "lbl_DIOName_22";
      this.lbl_DIOName_22.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_22.TabIndex = 222;
      this.lbl_DIOName_22.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_21
      // 
      this.lbl_DIOName_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_21.Location = new System.Drawing.Point(618, 175);
      this.lbl_DIOName_21.Name = "lbl_DIOName_21";
      this.lbl_DIOName_21.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_21.TabIndex = 221;
      this.lbl_DIOName_21.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_20
      // 
      this.lbl_DIOName_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_20.Location = new System.Drawing.Point(618, 145);
      this.lbl_DIOName_20.Name = "lbl_DIOName_20";
      this.lbl_DIOName_20.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_20.TabIndex = 220;
      this.lbl_DIOName_20.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_27
      // 
      this.lbl_DIOName_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_27.Location = new System.Drawing.Point(618, 355);
      this.lbl_DIOName_27.Name = "lbl_DIOName_27";
      this.lbl_DIOName_27.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_27.TabIndex = 219;
      this.lbl_DIOName_27.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_18
      // 
      this.lbl_DIOName_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_18.Location = new System.Drawing.Point(618, 85);
      this.lbl_DIOName_18.Name = "lbl_DIOName_18";
      this.lbl_DIOName_18.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_18.TabIndex = 218;
      this.lbl_DIOName_18.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_19
      // 
      this.lbl_DIOName_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_19.Location = new System.Drawing.Point(618, 115);
      this.lbl_DIOName_19.Name = "lbl_DIOName_19";
      this.lbl_DIOName_19.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_19.TabIndex = 217;
      this.lbl_DIOName_19.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_17
      // 
      this.lbl_DIOType_17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_17.Location = new System.Drawing.Point(973, 55);
      this.lbl_DIOType_17.Name = "lbl_DIOType_17";
      this.lbl_DIOType_17.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_17.TabIndex = 216;
      this.lbl_DIOType_17.Text = "Enc Out";
      this.lbl_DIOType_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_17
      // 
      this.lbl_DIOName_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_17.Location = new System.Drawing.Point(618, 55);
      this.lbl_DIOName_17.Name = "lbl_DIOName_17";
      this.lbl_DIOName_17.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_17.TabIndex = 215;
      this.lbl_DIOName_17.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_16
      // 
      this.lbl_DIOType_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_16.Location = new System.Drawing.Point(973, 25);
      this.lbl_DIOType_16.Name = "lbl_DIOType_16";
      this.lbl_DIOType_16.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_16.TabIndex = 214;
      this.lbl_DIOType_16.Text = "Enc Out";
      this.lbl_DIOType_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_16
      // 
      this.lbl_DIOName_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_16.Location = new System.Drawing.Point(618, 25);
      this.lbl_DIOName_16.Name = "lbl_DIOName_16";
      this.lbl_DIOName_16.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_16.TabIndex = 213;
      this.lbl_DIOName_16.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_29
      // 
      this.lbl_DIOStatus_29.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_29.Location = new System.Drawing.Point(1047, 415);
      this.lbl_DIOStatus_29.Name = "lbl_DIOStatus_29";
      this.lbl_DIOStatus_29.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_29.TabIndex = 212;
      // 
      // lbl_DIOChannel_29
      // 
      this.lbl_DIOChannel_29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_29.Location = new System.Drawing.Point(561, 415);
      this.lbl_DIOChannel_29.Name = "lbl_DIOChannel_29";
      this.lbl_DIOChannel_29.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_29.TabIndex = 211;
      this.lbl_DIOChannel_29.Text = "0029";
      this.lbl_DIOChannel_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_30
      // 
      this.lbl_DIOChannel_30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_30.Location = new System.Drawing.Point(561, 445);
      this.lbl_DIOChannel_30.Name = "lbl_DIOChannel_30";
      this.lbl_DIOChannel_30.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_30.TabIndex = 209;
      this.lbl_DIOChannel_30.Text = "0030";
      this.lbl_DIOChannel_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_31
      // 
      this.lbl_DIOChannel_31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_31.Location = new System.Drawing.Point(561, 475);
      this.lbl_DIOChannel_31.Name = "lbl_DIOChannel_31";
      this.lbl_DIOChannel_31.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_31.TabIndex = 210;
      this.lbl_DIOChannel_31.Text = "0031";
      this.lbl_DIOChannel_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_26
      // 
      this.lbl_DIOStatus_26.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_26.Location = new System.Drawing.Point(1047, 325);
      this.lbl_DIOStatus_26.Name = "lbl_DIOStatus_26";
      this.lbl_DIOStatus_26.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_26.TabIndex = 208;
      // 
      // lbl_DIOStatus_28
      // 
      this.lbl_DIOStatus_28.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_28.Location = new System.Drawing.Point(1047, 385);
      this.lbl_DIOStatus_28.Name = "lbl_DIOStatus_28";
      this.lbl_DIOStatus_28.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_28.TabIndex = 207;
      // 
      // lbl_DIOStatus_30
      // 
      this.lbl_DIOStatus_30.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_30.Location = new System.Drawing.Point(1047, 445);
      this.lbl_DIOStatus_30.Name = "lbl_DIOStatus_30";
      this.lbl_DIOStatus_30.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_30.TabIndex = 206;
      // 
      // lbl_DIOStatus_31
      // 
      this.lbl_DIOStatus_31.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_31.Location = new System.Drawing.Point(1047, 475);
      this.lbl_DIOStatus_31.Name = "lbl_DIOStatus_31";
      this.lbl_DIOStatus_31.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_31.TabIndex = 205;
      // 
      // lbl_DIOStatus_27
      // 
      this.lbl_DIOStatus_27.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_27.Location = new System.Drawing.Point(1047, 355);
      this.lbl_DIOStatus_27.Name = "lbl_DIOStatus_27";
      this.lbl_DIOStatus_27.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_27.TabIndex = 204;
      // 
      // lbl_DIOStatus_25
      // 
      this.lbl_DIOStatus_25.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_25.Location = new System.Drawing.Point(1047, 295);
      this.lbl_DIOStatus_25.Name = "lbl_DIOStatus_25";
      this.lbl_DIOStatus_25.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_25.TabIndex = 203;
      // 
      // lbl_DIOStatus_24
      // 
      this.lbl_DIOStatus_24.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_24.Location = new System.Drawing.Point(1047, 265);
      this.lbl_DIOStatus_24.Name = "lbl_DIOStatus_24";
      this.lbl_DIOStatus_24.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_24.TabIndex = 202;
      // 
      // lbl_DIOStatus_23
      // 
      this.lbl_DIOStatus_23.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_23.Location = new System.Drawing.Point(1047, 235);
      this.lbl_DIOStatus_23.Name = "lbl_DIOStatus_23";
      this.lbl_DIOStatus_23.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_23.TabIndex = 201;
      // 
      // lbl_DIOStatus_22
      // 
      this.lbl_DIOStatus_22.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_22.Location = new System.Drawing.Point(1047, 205);
      this.lbl_DIOStatus_22.Name = "lbl_DIOStatus_22";
      this.lbl_DIOStatus_22.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_22.TabIndex = 200;
      // 
      // lbl_DIOStatus_21
      // 
      this.lbl_DIOStatus_21.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_21.Location = new System.Drawing.Point(1047, 175);
      this.lbl_DIOStatus_21.Name = "lbl_DIOStatus_21";
      this.lbl_DIOStatus_21.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_21.TabIndex = 199;
      // 
      // lbl_DIOStatus_20
      // 
      this.lbl_DIOStatus_20.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_20.Location = new System.Drawing.Point(1047, 145);
      this.lbl_DIOStatus_20.Name = "lbl_DIOStatus_20";
      this.lbl_DIOStatus_20.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_20.TabIndex = 198;
      // 
      // lbl_DIOStatus_19
      // 
      this.lbl_DIOStatus_19.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_19.Location = new System.Drawing.Point(1047, 115);
      this.lbl_DIOStatus_19.Name = "lbl_DIOStatus_19";
      this.lbl_DIOStatus_19.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_19.TabIndex = 197;
      // 
      // lbl_DIOStatus_18
      // 
      this.lbl_DIOStatus_18.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_18.Location = new System.Drawing.Point(1047, 85);
      this.lbl_DIOStatus_18.Name = "lbl_DIOStatus_18";
      this.lbl_DIOStatus_18.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_18.TabIndex = 196;
      // 
      // lbl_DIOStatus_17
      // 
      this.lbl_DIOStatus_17.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_17.Location = new System.Drawing.Point(1047, 55);
      this.lbl_DIOStatus_17.Name = "lbl_DIOStatus_17";
      this.lbl_DIOStatus_17.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_17.TabIndex = 195;
      // 
      // lbl_DIOChannel_28
      // 
      this.lbl_DIOChannel_28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_28.Location = new System.Drawing.Point(561, 385);
      this.lbl_DIOChannel_28.Name = "lbl_DIOChannel_28";
      this.lbl_DIOChannel_28.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_28.TabIndex = 193;
      this.lbl_DIOChannel_28.Text = "0028";
      this.lbl_DIOChannel_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_25
      // 
      this.lbl_DIOChannel_25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_25.Location = new System.Drawing.Point(561, 295);
      this.lbl_DIOChannel_25.Name = "lbl_DIOChannel_25";
      this.lbl_DIOChannel_25.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_25.TabIndex = 192;
      this.lbl_DIOChannel_25.Text = "0025";
      this.lbl_DIOChannel_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_26
      // 
      this.lbl_DIOChannel_26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_26.Location = new System.Drawing.Point(561, 325);
      this.lbl_DIOChannel_26.Name = "lbl_DIOChannel_26";
      this.lbl_DIOChannel_26.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_26.TabIndex = 190;
      this.lbl_DIOChannel_26.Text = "0026";
      this.lbl_DIOChannel_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_23
      // 
      this.lbl_DIOChannel_23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_23.Location = new System.Drawing.Point(561, 235);
      this.lbl_DIOChannel_23.Name = "lbl_DIOChannel_23";
      this.lbl_DIOChannel_23.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_23.TabIndex = 189;
      this.lbl_DIOChannel_23.Text = "0023";
      this.lbl_DIOChannel_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_24
      // 
      this.lbl_DIOChannel_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_24.Location = new System.Drawing.Point(561, 265);
      this.lbl_DIOChannel_24.Name = "lbl_DIOChannel_24";
      this.lbl_DIOChannel_24.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_24.TabIndex = 188;
      this.lbl_DIOChannel_24.Text = "0024";
      this.lbl_DIOChannel_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_21
      // 
      this.lbl_DIOChannel_21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_21.Location = new System.Drawing.Point(561, 175);
      this.lbl_DIOChannel_21.Name = "lbl_DIOChannel_21";
      this.lbl_DIOChannel_21.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_21.TabIndex = 187;
      this.lbl_DIOChannel_21.Text = "0021";
      this.lbl_DIOChannel_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_22
      // 
      this.lbl_DIOChannel_22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_22.Location = new System.Drawing.Point(561, 205);
      this.lbl_DIOChannel_22.Name = "lbl_DIOChannel_22";
      this.lbl_DIOChannel_22.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_22.TabIndex = 186;
      this.lbl_DIOChannel_22.Text = "0022";
      this.lbl_DIOChannel_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_16
      // 
      this.lbl_DIOChannel_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_16.Location = new System.Drawing.Point(561, 25);
      this.lbl_DIOChannel_16.Name = "lbl_DIOChannel_16";
      this.lbl_DIOChannel_16.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_16.TabIndex = 181;
      this.lbl_DIOChannel_16.Text = "0016";
      this.lbl_DIOChannel_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_16
      // 
      this.lbl_DIOStatus_16.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_16.Location = new System.Drawing.Point(1047, 25);
      this.lbl_DIOStatus_16.Name = "lbl_DIOStatus_16";
      this.lbl_DIOStatus_16.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_16.TabIndex = 194;
      // 
      // lbl_DIOChannel_17
      // 
      this.lbl_DIOChannel_17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_17.Location = new System.Drawing.Point(561, 55);
      this.lbl_DIOChannel_17.Name = "lbl_DIOChannel_17";
      this.lbl_DIOChannel_17.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_17.TabIndex = 182;
      this.lbl_DIOChannel_17.Text = "0017";
      this.lbl_DIOChannel_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_18
      // 
      this.lbl_DIOChannel_18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_18.Location = new System.Drawing.Point(561, 85);
      this.lbl_DIOChannel_18.Name = "lbl_DIOChannel_18";
      this.lbl_DIOChannel_18.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_18.TabIndex = 183;
      this.lbl_DIOChannel_18.Text = "0018";
      this.lbl_DIOChannel_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_19
      // 
      this.lbl_DIOChannel_19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_19.Location = new System.Drawing.Point(561, 115);
      this.lbl_DIOChannel_19.Name = "lbl_DIOChannel_19";
      this.lbl_DIOChannel_19.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_19.TabIndex = 184;
      this.lbl_DIOChannel_19.Text = "0019";
      this.lbl_DIOChannel_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_20
      // 
      this.lbl_DIOChannel_20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_20.Location = new System.Drawing.Point(561, 145);
      this.lbl_DIOChannel_20.Name = "lbl_DIOChannel_20";
      this.lbl_DIOChannel_20.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_20.TabIndex = 185;
      this.lbl_DIOChannel_20.Text = "0020";
      this.lbl_DIOChannel_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_27
      // 
      this.lbl_DIOChannel_27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_27.Location = new System.Drawing.Point(561, 355);
      this.lbl_DIOChannel_27.Name = "lbl_DIOChannel_27";
      this.lbl_DIOChannel_27.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_27.TabIndex = 191;
      this.lbl_DIOChannel_27.Text = "0027";
      this.lbl_DIOChannel_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnl_DIOExpansion
      // 
      this.pnl_DIOExpansion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_50);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_67);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_66);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_67);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_66);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_66);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_67);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_66);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_67);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_56);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_55);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_54);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_53);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_52);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_65);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_64);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_63);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_62);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_61);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_60);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_59);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_58);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_57);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_60);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_59);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_58);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_65);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_64);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_63);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_62);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_57);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_56);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_55);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_54);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_61);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_52);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_53);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_51);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_51);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_63);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_63);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_50);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_64);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_65);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_60);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_62);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_64);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_65);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_61);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_59);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_58);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_57);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_56);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_55);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_54);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_53);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_52);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_51);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_62);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_59);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_60);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_57);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_58);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_55);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_56);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_50);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_51);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_52);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_53);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_54);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_50);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_61);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_32);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_49);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_48);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_49);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_48);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_48);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_49);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_48);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_49);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_38);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_37);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_36);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_35);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_34);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_47);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_46);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_45);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_44);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_43);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_42);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_41);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_40);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_39);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_42);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_41);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_40);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_47);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_46);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_45);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_44);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_39);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_38);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_37);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_36);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_43);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_34);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_35);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOType_33);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_33);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_45);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_45);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOName_32);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_46);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_47);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_42);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_44);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_46);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_47);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_43);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_41);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_40);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_39);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_38);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_37);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_36);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_35);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_34);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_33);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_44);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_41);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_42);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_39);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_40);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_37);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_38);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOStatus_32);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_33);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_34);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_35);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_36);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_32);
      this.pnl_DIOExpansion.Controls.Add(this.lbl_DIOChannel_43);
      this.pnl_DIOExpansion.Location = new System.Drawing.Point(68, 173);
      this.pnl_DIOExpansion.Name = "pnl_DIOExpansion";
      this.pnl_DIOExpansion.Size = new System.Drawing.Size(1107, 565);
      this.pnl_DIOExpansion.TabIndex = 245;
      this.pnl_DIOExpansion.Visible = false;
      // 
      // lbl_DIOType_50
      // 
      this.lbl_DIOType_50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_50.Location = new System.Drawing.Point(977, 12);
      this.lbl_DIOType_50.Name = "lbl_DIOType_50";
      this.lbl_DIOType_50.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_50.TabIndex = 285;
      this.lbl_DIOType_50.Text = "Enc Out";
      this.lbl_DIOType_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_67
      // 
      this.lbl_DIOType_67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_67.Location = new System.Drawing.Point(977, 522);
      this.lbl_DIOType_67.Name = "lbl_DIOType_67";
      this.lbl_DIOType_67.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_67.TabIndex = 323;
      this.lbl_DIOType_67.Text = "Enc Out";
      this.lbl_DIOType_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_66
      // 
      this.lbl_DIOType_66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_66.Location = new System.Drawing.Point(977, 492);
      this.lbl_DIOType_66.Name = "lbl_DIOType_66";
      this.lbl_DIOType_66.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_66.TabIndex = 322;
      this.lbl_DIOType_66.Text = "Enc Out";
      this.lbl_DIOType_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_67
      // 
      this.lbl_DIOName_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_67.Location = new System.Drawing.Point(622, 522);
      this.lbl_DIOName_67.Name = "lbl_DIOName_67";
      this.lbl_DIOName_67.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_67.TabIndex = 321;
      this.lbl_DIOName_67.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_66
      // 
      this.lbl_DIOName_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_66.Location = new System.Drawing.Point(622, 492);
      this.lbl_DIOName_66.Name = "lbl_DIOName_66";
      this.lbl_DIOName_66.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_66.TabIndex = 320;
      this.lbl_DIOName_66.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_66
      // 
      this.lbl_DIOChannel_66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_66.Location = new System.Drawing.Point(563, 492);
      this.lbl_DIOChannel_66.Name = "lbl_DIOChannel_66";
      this.lbl_DIOChannel_66.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_66.TabIndex = 318;
      this.lbl_DIOChannel_66.Text = "0066";
      this.lbl_DIOChannel_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_67
      // 
      this.lbl_DIOChannel_67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_67.Location = new System.Drawing.Point(563, 522);
      this.lbl_DIOChannel_67.Name = "lbl_DIOChannel_67";
      this.lbl_DIOChannel_67.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_67.TabIndex = 319;
      this.lbl_DIOChannel_67.Text = "0067";
      this.lbl_DIOChannel_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_66
      // 
      this.lbl_DIOStatus_66.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_66.Location = new System.Drawing.Point(1051, 492);
      this.lbl_DIOStatus_66.Name = "lbl_DIOStatus_66";
      this.lbl_DIOStatus_66.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_66.TabIndex = 317;
      // 
      // lbl_DIOStatus_67
      // 
      this.lbl_DIOStatus_67.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_67.Location = new System.Drawing.Point(1051, 522);
      this.lbl_DIOStatus_67.Name = "lbl_DIOStatus_67";
      this.lbl_DIOStatus_67.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_67.TabIndex = 316;
      // 
      // lbl_DIOType_56
      // 
      this.lbl_DIOType_56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_56.Location = new System.Drawing.Point(977, 192);
      this.lbl_DIOType_56.Name = "lbl_DIOType_56";
      this.lbl_DIOType_56.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_56.TabIndex = 315;
      this.lbl_DIOType_56.Text = "Enc Out";
      this.lbl_DIOType_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_55
      // 
      this.lbl_DIOType_55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_55.Location = new System.Drawing.Point(977, 162);
      this.lbl_DIOType_55.Name = "lbl_DIOType_55";
      this.lbl_DIOType_55.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_55.TabIndex = 314;
      this.lbl_DIOType_55.Text = "Enc Out";
      this.lbl_DIOType_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_54
      // 
      this.lbl_DIOType_54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_54.Location = new System.Drawing.Point(977, 132);
      this.lbl_DIOType_54.Name = "lbl_DIOType_54";
      this.lbl_DIOType_54.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_54.TabIndex = 313;
      this.lbl_DIOType_54.Text = "Enc Out";
      this.lbl_DIOType_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_53
      // 
      this.lbl_DIOType_53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_53.Location = new System.Drawing.Point(977, 102);
      this.lbl_DIOType_53.Name = "lbl_DIOType_53";
      this.lbl_DIOType_53.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_53.TabIndex = 312;
      this.lbl_DIOType_53.Text = "Enc Out";
      this.lbl_DIOType_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_52
      // 
      this.lbl_DIOType_52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_52.Location = new System.Drawing.Point(977, 72);
      this.lbl_DIOType_52.Name = "lbl_DIOType_52";
      this.lbl_DIOType_52.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_52.TabIndex = 311;
      this.lbl_DIOType_52.Text = "Enc Out";
      this.lbl_DIOType_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_65
      // 
      this.lbl_DIOType_65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_65.Location = new System.Drawing.Point(977, 462);
      this.lbl_DIOType_65.Name = "lbl_DIOType_65";
      this.lbl_DIOType_65.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_65.TabIndex = 310;
      this.lbl_DIOType_65.Text = "Enc Out";
      this.lbl_DIOType_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_64
      // 
      this.lbl_DIOType_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_64.Location = new System.Drawing.Point(977, 432);
      this.lbl_DIOType_64.Name = "lbl_DIOType_64";
      this.lbl_DIOType_64.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_64.TabIndex = 309;
      this.lbl_DIOType_64.Text = "Enc Out";
      this.lbl_DIOType_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_63
      // 
      this.lbl_DIOType_63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_63.Location = new System.Drawing.Point(977, 402);
      this.lbl_DIOType_63.Name = "lbl_DIOType_63";
      this.lbl_DIOType_63.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_63.TabIndex = 308;
      this.lbl_DIOType_63.Text = "Enc Out";
      this.lbl_DIOType_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_62
      // 
      this.lbl_DIOType_62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_62.Location = new System.Drawing.Point(977, 372);
      this.lbl_DIOType_62.Name = "lbl_DIOType_62";
      this.lbl_DIOType_62.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_62.TabIndex = 307;
      this.lbl_DIOType_62.Text = "Enc Out";
      this.lbl_DIOType_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_61
      // 
      this.lbl_DIOType_61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_61.Location = new System.Drawing.Point(977, 342);
      this.lbl_DIOType_61.Name = "lbl_DIOType_61";
      this.lbl_DIOType_61.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_61.TabIndex = 306;
      this.lbl_DIOType_61.Text = "Enc Out";
      this.lbl_DIOType_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_60
      // 
      this.lbl_DIOType_60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_60.Location = new System.Drawing.Point(977, 312);
      this.lbl_DIOType_60.Name = "lbl_DIOType_60";
      this.lbl_DIOType_60.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_60.TabIndex = 305;
      this.lbl_DIOType_60.Text = "Enc Out";
      this.lbl_DIOType_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_59
      // 
      this.lbl_DIOType_59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_59.Location = new System.Drawing.Point(977, 282);
      this.lbl_DIOType_59.Name = "lbl_DIOType_59";
      this.lbl_DIOType_59.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_59.TabIndex = 304;
      this.lbl_DIOType_59.Text = "Enc Out";
      this.lbl_DIOType_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_58
      // 
      this.lbl_DIOType_58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_58.Location = new System.Drawing.Point(977, 252);
      this.lbl_DIOType_58.Name = "lbl_DIOType_58";
      this.lbl_DIOType_58.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_58.TabIndex = 303;
      this.lbl_DIOType_58.Text = "Enc Out";
      this.lbl_DIOType_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_57
      // 
      this.lbl_DIOType_57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_57.Location = new System.Drawing.Point(977, 222);
      this.lbl_DIOType_57.Name = "lbl_DIOType_57";
      this.lbl_DIOType_57.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_57.TabIndex = 302;
      this.lbl_DIOType_57.Text = "Enc Out";
      this.lbl_DIOType_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_60
      // 
      this.lbl_DIOName_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_60.Location = new System.Drawing.Point(622, 312);
      this.lbl_DIOName_60.Name = "lbl_DIOName_60";
      this.lbl_DIOName_60.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_60.TabIndex = 301;
      this.lbl_DIOName_60.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_59
      // 
      this.lbl_DIOName_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_59.Location = new System.Drawing.Point(622, 282);
      this.lbl_DIOName_59.Name = "lbl_DIOName_59";
      this.lbl_DIOName_59.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_59.TabIndex = 300;
      this.lbl_DIOName_59.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_58
      // 
      this.lbl_DIOName_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_58.Location = new System.Drawing.Point(622, 252);
      this.lbl_DIOName_58.Name = "lbl_DIOName_58";
      this.lbl_DIOName_58.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_58.TabIndex = 299;
      this.lbl_DIOName_58.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_65
      // 
      this.lbl_DIOName_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_65.Location = new System.Drawing.Point(622, 462);
      this.lbl_DIOName_65.Name = "lbl_DIOName_65";
      this.lbl_DIOName_65.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_65.TabIndex = 298;
      this.lbl_DIOName_65.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_64
      // 
      this.lbl_DIOName_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_64.Location = new System.Drawing.Point(622, 432);
      this.lbl_DIOName_64.Name = "lbl_DIOName_64";
      this.lbl_DIOName_64.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_64.TabIndex = 297;
      this.lbl_DIOName_64.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_63
      // 
      this.lbl_DIOName_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_63.Location = new System.Drawing.Point(622, 402);
      this.lbl_DIOName_63.Name = "lbl_DIOName_63";
      this.lbl_DIOName_63.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_63.TabIndex = 296;
      this.lbl_DIOName_63.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_62
      // 
      this.lbl_DIOName_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_62.Location = new System.Drawing.Point(622, 372);
      this.lbl_DIOName_62.Name = "lbl_DIOName_62";
      this.lbl_DIOName_62.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_62.TabIndex = 295;
      this.lbl_DIOName_62.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_57
      // 
      this.lbl_DIOName_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_57.Location = new System.Drawing.Point(622, 222);
      this.lbl_DIOName_57.Name = "lbl_DIOName_57";
      this.lbl_DIOName_57.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_57.TabIndex = 294;
      this.lbl_DIOName_57.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_56
      // 
      this.lbl_DIOName_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_56.Location = new System.Drawing.Point(622, 192);
      this.lbl_DIOName_56.Name = "lbl_DIOName_56";
      this.lbl_DIOName_56.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_56.TabIndex = 293;
      this.lbl_DIOName_56.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_55
      // 
      this.lbl_DIOName_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_55.Location = new System.Drawing.Point(622, 162);
      this.lbl_DIOName_55.Name = "lbl_DIOName_55";
      this.lbl_DIOName_55.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_55.TabIndex = 292;
      this.lbl_DIOName_55.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_54
      // 
      this.lbl_DIOName_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_54.Location = new System.Drawing.Point(622, 132);
      this.lbl_DIOName_54.Name = "lbl_DIOName_54";
      this.lbl_DIOName_54.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_54.TabIndex = 291;
      this.lbl_DIOName_54.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_61
      // 
      this.lbl_DIOName_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_61.Location = new System.Drawing.Point(622, 342);
      this.lbl_DIOName_61.Name = "lbl_DIOName_61";
      this.lbl_DIOName_61.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_61.TabIndex = 290;
      this.lbl_DIOName_61.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_52
      // 
      this.lbl_DIOName_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_52.Location = new System.Drawing.Point(622, 72);
      this.lbl_DIOName_52.Name = "lbl_DIOName_52";
      this.lbl_DIOName_52.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_52.TabIndex = 289;
      this.lbl_DIOName_52.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_53
      // 
      this.lbl_DIOName_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_53.Location = new System.Drawing.Point(622, 102);
      this.lbl_DIOName_53.Name = "lbl_DIOName_53";
      this.lbl_DIOName_53.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_53.TabIndex = 288;
      this.lbl_DIOName_53.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_51
      // 
      this.lbl_DIOType_51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_51.Location = new System.Drawing.Point(977, 42);
      this.lbl_DIOType_51.Name = "lbl_DIOType_51";
      this.lbl_DIOType_51.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_51.TabIndex = 287;
      this.lbl_DIOType_51.Text = "Enc Out";
      this.lbl_DIOType_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_51
      // 
      this.lbl_DIOName_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_51.Location = new System.Drawing.Point(622, 42);
      this.lbl_DIOName_51.Name = "lbl_DIOName_51";
      this.lbl_DIOName_51.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_51.TabIndex = 286;
      this.lbl_DIOName_51.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_63
      // 
      this.lbl_DIOStatus_63.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_63.Location = new System.Drawing.Point(1051, 402);
      this.lbl_DIOStatus_63.Name = "lbl_DIOStatus_63";
      this.lbl_DIOStatus_63.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_63.TabIndex = 283;
      // 
      // lbl_DIOChannel_63
      // 
      this.lbl_DIOChannel_63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_63.Location = new System.Drawing.Point(563, 402);
      this.lbl_DIOChannel_63.Name = "lbl_DIOChannel_63";
      this.lbl_DIOChannel_63.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_63.TabIndex = 282;
      this.lbl_DIOChannel_63.Text = "0063";
      this.lbl_DIOChannel_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_50
      // 
      this.lbl_DIOName_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_50.Location = new System.Drawing.Point(622, 12);
      this.lbl_DIOName_50.Name = "lbl_DIOName_50";
      this.lbl_DIOName_50.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_50.TabIndex = 284;
      this.lbl_DIOName_50.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_64
      // 
      this.lbl_DIOChannel_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_64.Location = new System.Drawing.Point(563, 432);
      this.lbl_DIOChannel_64.Name = "lbl_DIOChannel_64";
      this.lbl_DIOChannel_64.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_64.TabIndex = 280;
      this.lbl_DIOChannel_64.Text = "0064";
      this.lbl_DIOChannel_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_65
      // 
      this.lbl_DIOChannel_65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_65.Location = new System.Drawing.Point(563, 462);
      this.lbl_DIOChannel_65.Name = "lbl_DIOChannel_65";
      this.lbl_DIOChannel_65.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_65.TabIndex = 281;
      this.lbl_DIOChannel_65.Text = "0065";
      this.lbl_DIOChannel_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_60
      // 
      this.lbl_DIOStatus_60.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_60.Location = new System.Drawing.Point(1051, 312);
      this.lbl_DIOStatus_60.Name = "lbl_DIOStatus_60";
      this.lbl_DIOStatus_60.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_60.TabIndex = 279;
      // 
      // lbl_DIOStatus_62
      // 
      this.lbl_DIOStatus_62.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_62.Location = new System.Drawing.Point(1051, 372);
      this.lbl_DIOStatus_62.Name = "lbl_DIOStatus_62";
      this.lbl_DIOStatus_62.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_62.TabIndex = 278;
      // 
      // lbl_DIOStatus_64
      // 
      this.lbl_DIOStatus_64.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_64.Location = new System.Drawing.Point(1051, 432);
      this.lbl_DIOStatus_64.Name = "lbl_DIOStatus_64";
      this.lbl_DIOStatus_64.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_64.TabIndex = 277;
      // 
      // lbl_DIOStatus_65
      // 
      this.lbl_DIOStatus_65.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_65.Location = new System.Drawing.Point(1051, 462);
      this.lbl_DIOStatus_65.Name = "lbl_DIOStatus_65";
      this.lbl_DIOStatus_65.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_65.TabIndex = 276;
      // 
      // lbl_DIOStatus_61
      // 
      this.lbl_DIOStatus_61.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_61.Location = new System.Drawing.Point(1051, 342);
      this.lbl_DIOStatus_61.Name = "lbl_DIOStatus_61";
      this.lbl_DIOStatus_61.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_61.TabIndex = 275;
      // 
      // lbl_DIOStatus_59
      // 
      this.lbl_DIOStatus_59.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_59.Location = new System.Drawing.Point(1051, 282);
      this.lbl_DIOStatus_59.Name = "lbl_DIOStatus_59";
      this.lbl_DIOStatus_59.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_59.TabIndex = 274;
      // 
      // lbl_DIOStatus_58
      // 
      this.lbl_DIOStatus_58.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_58.Location = new System.Drawing.Point(1051, 252);
      this.lbl_DIOStatus_58.Name = "lbl_DIOStatus_58";
      this.lbl_DIOStatus_58.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_58.TabIndex = 273;
      // 
      // lbl_DIOStatus_57
      // 
      this.lbl_DIOStatus_57.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_57.Location = new System.Drawing.Point(1051, 222);
      this.lbl_DIOStatus_57.Name = "lbl_DIOStatus_57";
      this.lbl_DIOStatus_57.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_57.TabIndex = 272;
      // 
      // lbl_DIOStatus_56
      // 
      this.lbl_DIOStatus_56.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_56.Location = new System.Drawing.Point(1051, 192);
      this.lbl_DIOStatus_56.Name = "lbl_DIOStatus_56";
      this.lbl_DIOStatus_56.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_56.TabIndex = 271;
      // 
      // lbl_DIOStatus_55
      // 
      this.lbl_DIOStatus_55.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_55.Location = new System.Drawing.Point(1051, 162);
      this.lbl_DIOStatus_55.Name = "lbl_DIOStatus_55";
      this.lbl_DIOStatus_55.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_55.TabIndex = 270;
      // 
      // lbl_DIOStatus_54
      // 
      this.lbl_DIOStatus_54.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_54.Location = new System.Drawing.Point(1051, 132);
      this.lbl_DIOStatus_54.Name = "lbl_DIOStatus_54";
      this.lbl_DIOStatus_54.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_54.TabIndex = 269;
      // 
      // lbl_DIOStatus_53
      // 
      this.lbl_DIOStatus_53.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_53.Location = new System.Drawing.Point(1051, 102);
      this.lbl_DIOStatus_53.Name = "lbl_DIOStatus_53";
      this.lbl_DIOStatus_53.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_53.TabIndex = 268;
      // 
      // lbl_DIOStatus_52
      // 
      this.lbl_DIOStatus_52.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_52.Location = new System.Drawing.Point(1051, 72);
      this.lbl_DIOStatus_52.Name = "lbl_DIOStatus_52";
      this.lbl_DIOStatus_52.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_52.TabIndex = 267;
      // 
      // lbl_DIOStatus_51
      // 
      this.lbl_DIOStatus_51.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_51.Location = new System.Drawing.Point(1051, 42);
      this.lbl_DIOStatus_51.Name = "lbl_DIOStatus_51";
      this.lbl_DIOStatus_51.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_51.TabIndex = 266;
      // 
      // lbl_DIOChannel_62
      // 
      this.lbl_DIOChannel_62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_62.Location = new System.Drawing.Point(563, 372);
      this.lbl_DIOChannel_62.Name = "lbl_DIOChannel_62";
      this.lbl_DIOChannel_62.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_62.TabIndex = 264;
      this.lbl_DIOChannel_62.Text = "0062";
      this.lbl_DIOChannel_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_59
      // 
      this.lbl_DIOChannel_59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_59.Location = new System.Drawing.Point(563, 282);
      this.lbl_DIOChannel_59.Name = "lbl_DIOChannel_59";
      this.lbl_DIOChannel_59.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_59.TabIndex = 263;
      this.lbl_DIOChannel_59.Text = "0059";
      this.lbl_DIOChannel_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_60
      // 
      this.lbl_DIOChannel_60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_60.Location = new System.Drawing.Point(563, 312);
      this.lbl_DIOChannel_60.Name = "lbl_DIOChannel_60";
      this.lbl_DIOChannel_60.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_60.TabIndex = 261;
      this.lbl_DIOChannel_60.Text = "0060";
      this.lbl_DIOChannel_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_57
      // 
      this.lbl_DIOChannel_57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_57.Location = new System.Drawing.Point(563, 222);
      this.lbl_DIOChannel_57.Name = "lbl_DIOChannel_57";
      this.lbl_DIOChannel_57.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_57.TabIndex = 260;
      this.lbl_DIOChannel_57.Text = "0057";
      this.lbl_DIOChannel_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_58
      // 
      this.lbl_DIOChannel_58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_58.Location = new System.Drawing.Point(563, 252);
      this.lbl_DIOChannel_58.Name = "lbl_DIOChannel_58";
      this.lbl_DIOChannel_58.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_58.TabIndex = 259;
      this.lbl_DIOChannel_58.Text = "0058";
      this.lbl_DIOChannel_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_55
      // 
      this.lbl_DIOChannel_55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_55.Location = new System.Drawing.Point(563, 162);
      this.lbl_DIOChannel_55.Name = "lbl_DIOChannel_55";
      this.lbl_DIOChannel_55.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_55.TabIndex = 258;
      this.lbl_DIOChannel_55.Text = "0055";
      this.lbl_DIOChannel_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_56
      // 
      this.lbl_DIOChannel_56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_56.Location = new System.Drawing.Point(563, 192);
      this.lbl_DIOChannel_56.Name = "lbl_DIOChannel_56";
      this.lbl_DIOChannel_56.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_56.TabIndex = 257;
      this.lbl_DIOChannel_56.Text = "0056";
      this.lbl_DIOChannel_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_50
      // 
      this.lbl_DIOStatus_50.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_50.Location = new System.Drawing.Point(1051, 12);
      this.lbl_DIOStatus_50.Name = "lbl_DIOStatus_50";
      this.lbl_DIOStatus_50.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_50.TabIndex = 265;
      // 
      // lbl_DIOChannel_51
      // 
      this.lbl_DIOChannel_51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_51.Location = new System.Drawing.Point(563, 42);
      this.lbl_DIOChannel_51.Name = "lbl_DIOChannel_51";
      this.lbl_DIOChannel_51.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_51.TabIndex = 253;
      this.lbl_DIOChannel_51.Text = "0051";
      this.lbl_DIOChannel_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_52
      // 
      this.lbl_DIOChannel_52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_52.Location = new System.Drawing.Point(563, 72);
      this.lbl_DIOChannel_52.Name = "lbl_DIOChannel_52";
      this.lbl_DIOChannel_52.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_52.TabIndex = 254;
      this.lbl_DIOChannel_52.Text = "0052";
      this.lbl_DIOChannel_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_53
      // 
      this.lbl_DIOChannel_53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_53.Location = new System.Drawing.Point(563, 102);
      this.lbl_DIOChannel_53.Name = "lbl_DIOChannel_53";
      this.lbl_DIOChannel_53.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_53.TabIndex = 255;
      this.lbl_DIOChannel_53.Text = "0053";
      this.lbl_DIOChannel_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_54
      // 
      this.lbl_DIOChannel_54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_54.Location = new System.Drawing.Point(563, 132);
      this.lbl_DIOChannel_54.Name = "lbl_DIOChannel_54";
      this.lbl_DIOChannel_54.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_54.TabIndex = 256;
      this.lbl_DIOChannel_54.Text = "0054";
      this.lbl_DIOChannel_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_50
      // 
      this.lbl_DIOChannel_50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_50.Location = new System.Drawing.Point(563, 12);
      this.lbl_DIOChannel_50.Name = "lbl_DIOChannel_50";
      this.lbl_DIOChannel_50.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_50.TabIndex = 252;
      this.lbl_DIOChannel_50.Text = "0050";
      this.lbl_DIOChannel_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_61
      // 
      this.lbl_DIOChannel_61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_61.Location = new System.Drawing.Point(563, 342);
      this.lbl_DIOChannel_61.Name = "lbl_DIOChannel_61";
      this.lbl_DIOChannel_61.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_61.TabIndex = 262;
      this.lbl_DIOChannel_61.Text = "0061";
      this.lbl_DIOChannel_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_32
      // 
      this.lbl_DIOType_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_32.Location = new System.Drawing.Point(448, 14);
      this.lbl_DIOType_32.Name = "lbl_DIOType_32";
      this.lbl_DIOType_32.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_32.TabIndex = 213;
      this.lbl_DIOType_32.Text = "Enc Out";
      this.lbl_DIOType_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_49
      // 
      this.lbl_DIOType_49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_49.Location = new System.Drawing.Point(448, 524);
      this.lbl_DIOType_49.Name = "lbl_DIOType_49";
      this.lbl_DIOType_49.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_49.TabIndex = 251;
      this.lbl_DIOType_49.Text = "Enc Out";
      this.lbl_DIOType_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_48
      // 
      this.lbl_DIOType_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_48.Location = new System.Drawing.Point(448, 494);
      this.lbl_DIOType_48.Name = "lbl_DIOType_48";
      this.lbl_DIOType_48.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_48.TabIndex = 250;
      this.lbl_DIOType_48.Text = "Enc Out";
      this.lbl_DIOType_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_49
      // 
      this.lbl_DIOName_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_49.Location = new System.Drawing.Point(93, 524);
      this.lbl_DIOName_49.Name = "lbl_DIOName_49";
      this.lbl_DIOName_49.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_49.TabIndex = 249;
      this.lbl_DIOName_49.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_48
      // 
      this.lbl_DIOName_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_48.Location = new System.Drawing.Point(93, 494);
      this.lbl_DIOName_48.Name = "lbl_DIOName_48";
      this.lbl_DIOName_48.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_48.TabIndex = 248;
      this.lbl_DIOName_48.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_48
      // 
      this.lbl_DIOChannel_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_48.Location = new System.Drawing.Point(37, 494);
      this.lbl_DIOChannel_48.Name = "lbl_DIOChannel_48";
      this.lbl_DIOChannel_48.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_48.TabIndex = 246;
      this.lbl_DIOChannel_48.Text = "0048";
      this.lbl_DIOChannel_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_49
      // 
      this.lbl_DIOChannel_49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_49.Location = new System.Drawing.Point(37, 524);
      this.lbl_DIOChannel_49.Name = "lbl_DIOChannel_49";
      this.lbl_DIOChannel_49.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_49.TabIndex = 247;
      this.lbl_DIOChannel_49.Text = "0049";
      this.lbl_DIOChannel_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_48
      // 
      this.lbl_DIOStatus_48.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_48.Location = new System.Drawing.Point(522, 494);
      this.lbl_DIOStatus_48.Name = "lbl_DIOStatus_48";
      this.lbl_DIOStatus_48.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_48.TabIndex = 245;
      // 
      // lbl_DIOStatus_49
      // 
      this.lbl_DIOStatus_49.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_49.Location = new System.Drawing.Point(522, 524);
      this.lbl_DIOStatus_49.Name = "lbl_DIOStatus_49";
      this.lbl_DIOStatus_49.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_49.TabIndex = 244;
      // 
      // lbl_DIOType_38
      // 
      this.lbl_DIOType_38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_38.Location = new System.Drawing.Point(448, 194);
      this.lbl_DIOType_38.Name = "lbl_DIOType_38";
      this.lbl_DIOType_38.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_38.TabIndex = 243;
      this.lbl_DIOType_38.Text = "Enc Out";
      this.lbl_DIOType_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_37
      // 
      this.lbl_DIOType_37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_37.Location = new System.Drawing.Point(448, 164);
      this.lbl_DIOType_37.Name = "lbl_DIOType_37";
      this.lbl_DIOType_37.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_37.TabIndex = 242;
      this.lbl_DIOType_37.Text = "Enc Out";
      this.lbl_DIOType_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_36
      // 
      this.lbl_DIOType_36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_36.Location = new System.Drawing.Point(448, 134);
      this.lbl_DIOType_36.Name = "lbl_DIOType_36";
      this.lbl_DIOType_36.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_36.TabIndex = 241;
      this.lbl_DIOType_36.Text = "Enc Out";
      this.lbl_DIOType_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_35
      // 
      this.lbl_DIOType_35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_35.Location = new System.Drawing.Point(448, 104);
      this.lbl_DIOType_35.Name = "lbl_DIOType_35";
      this.lbl_DIOType_35.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_35.TabIndex = 240;
      this.lbl_DIOType_35.Text = "Enc Out";
      this.lbl_DIOType_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_34
      // 
      this.lbl_DIOType_34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_34.Location = new System.Drawing.Point(448, 74);
      this.lbl_DIOType_34.Name = "lbl_DIOType_34";
      this.lbl_DIOType_34.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_34.TabIndex = 239;
      this.lbl_DIOType_34.Text = "Enc Out";
      this.lbl_DIOType_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_47
      // 
      this.lbl_DIOType_47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_47.Location = new System.Drawing.Point(448, 464);
      this.lbl_DIOType_47.Name = "lbl_DIOType_47";
      this.lbl_DIOType_47.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_47.TabIndex = 238;
      this.lbl_DIOType_47.Text = "Enc Out";
      this.lbl_DIOType_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_46
      // 
      this.lbl_DIOType_46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_46.Location = new System.Drawing.Point(448, 434);
      this.lbl_DIOType_46.Name = "lbl_DIOType_46";
      this.lbl_DIOType_46.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_46.TabIndex = 237;
      this.lbl_DIOType_46.Text = "Enc Out";
      this.lbl_DIOType_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_45
      // 
      this.lbl_DIOType_45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_45.Location = new System.Drawing.Point(448, 404);
      this.lbl_DIOType_45.Name = "lbl_DIOType_45";
      this.lbl_DIOType_45.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_45.TabIndex = 236;
      this.lbl_DIOType_45.Text = "Enc Out";
      this.lbl_DIOType_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_44
      // 
      this.lbl_DIOType_44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_44.Location = new System.Drawing.Point(448, 374);
      this.lbl_DIOType_44.Name = "lbl_DIOType_44";
      this.lbl_DIOType_44.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_44.TabIndex = 235;
      this.lbl_DIOType_44.Text = "Enc Out";
      this.lbl_DIOType_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_43
      // 
      this.lbl_DIOType_43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_43.Location = new System.Drawing.Point(448, 344);
      this.lbl_DIOType_43.Name = "lbl_DIOType_43";
      this.lbl_DIOType_43.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_43.TabIndex = 234;
      this.lbl_DIOType_43.Text = "Enc Out";
      this.lbl_DIOType_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_42
      // 
      this.lbl_DIOType_42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_42.Location = new System.Drawing.Point(448, 314);
      this.lbl_DIOType_42.Name = "lbl_DIOType_42";
      this.lbl_DIOType_42.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_42.TabIndex = 233;
      this.lbl_DIOType_42.Text = "Enc Out";
      this.lbl_DIOType_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_41
      // 
      this.lbl_DIOType_41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_41.Location = new System.Drawing.Point(448, 284);
      this.lbl_DIOType_41.Name = "lbl_DIOType_41";
      this.lbl_DIOType_41.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_41.TabIndex = 232;
      this.lbl_DIOType_41.Text = "Enc Out";
      this.lbl_DIOType_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_40
      // 
      this.lbl_DIOType_40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_40.Location = new System.Drawing.Point(448, 254);
      this.lbl_DIOType_40.Name = "lbl_DIOType_40";
      this.lbl_DIOType_40.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_40.TabIndex = 231;
      this.lbl_DIOType_40.Text = "Enc Out";
      this.lbl_DIOType_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_39
      // 
      this.lbl_DIOType_39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_39.Location = new System.Drawing.Point(448, 224);
      this.lbl_DIOType_39.Name = "lbl_DIOType_39";
      this.lbl_DIOType_39.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_39.TabIndex = 230;
      this.lbl_DIOType_39.Text = "Enc Out";
      this.lbl_DIOType_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_42
      // 
      this.lbl_DIOName_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_42.Location = new System.Drawing.Point(93, 314);
      this.lbl_DIOName_42.Name = "lbl_DIOName_42";
      this.lbl_DIOName_42.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_42.TabIndex = 229;
      this.lbl_DIOName_42.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_41
      // 
      this.lbl_DIOName_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_41.Location = new System.Drawing.Point(93, 284);
      this.lbl_DIOName_41.Name = "lbl_DIOName_41";
      this.lbl_DIOName_41.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_41.TabIndex = 228;
      this.lbl_DIOName_41.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_40
      // 
      this.lbl_DIOName_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_40.Location = new System.Drawing.Point(93, 254);
      this.lbl_DIOName_40.Name = "lbl_DIOName_40";
      this.lbl_DIOName_40.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_40.TabIndex = 227;
      this.lbl_DIOName_40.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_47
      // 
      this.lbl_DIOName_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_47.Location = new System.Drawing.Point(93, 464);
      this.lbl_DIOName_47.Name = "lbl_DIOName_47";
      this.lbl_DIOName_47.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_47.TabIndex = 226;
      this.lbl_DIOName_47.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_46
      // 
      this.lbl_DIOName_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_46.Location = new System.Drawing.Point(93, 434);
      this.lbl_DIOName_46.Name = "lbl_DIOName_46";
      this.lbl_DIOName_46.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_46.TabIndex = 225;
      this.lbl_DIOName_46.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_45
      // 
      this.lbl_DIOName_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_45.Location = new System.Drawing.Point(93, 404);
      this.lbl_DIOName_45.Name = "lbl_DIOName_45";
      this.lbl_DIOName_45.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_45.TabIndex = 224;
      this.lbl_DIOName_45.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_44
      // 
      this.lbl_DIOName_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_44.Location = new System.Drawing.Point(93, 374);
      this.lbl_DIOName_44.Name = "lbl_DIOName_44";
      this.lbl_DIOName_44.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_44.TabIndex = 223;
      this.lbl_DIOName_44.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_39
      // 
      this.lbl_DIOName_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_39.Location = new System.Drawing.Point(93, 224);
      this.lbl_DIOName_39.Name = "lbl_DIOName_39";
      this.lbl_DIOName_39.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_39.TabIndex = 222;
      this.lbl_DIOName_39.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_38
      // 
      this.lbl_DIOName_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_38.Location = new System.Drawing.Point(93, 194);
      this.lbl_DIOName_38.Name = "lbl_DIOName_38";
      this.lbl_DIOName_38.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_38.TabIndex = 221;
      this.lbl_DIOName_38.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_37
      // 
      this.lbl_DIOName_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_37.Location = new System.Drawing.Point(93, 164);
      this.lbl_DIOName_37.Name = "lbl_DIOName_37";
      this.lbl_DIOName_37.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_37.TabIndex = 220;
      this.lbl_DIOName_37.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_36
      // 
      this.lbl_DIOName_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_36.Location = new System.Drawing.Point(93, 134);
      this.lbl_DIOName_36.Name = "lbl_DIOName_36";
      this.lbl_DIOName_36.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_36.TabIndex = 219;
      this.lbl_DIOName_36.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_43
      // 
      this.lbl_DIOName_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_43.Location = new System.Drawing.Point(93, 344);
      this.lbl_DIOName_43.Name = "lbl_DIOName_43";
      this.lbl_DIOName_43.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_43.TabIndex = 218;
      this.lbl_DIOName_43.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_34
      // 
      this.lbl_DIOName_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_34.Location = new System.Drawing.Point(93, 74);
      this.lbl_DIOName_34.Name = "lbl_DIOName_34";
      this.lbl_DIOName_34.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_34.TabIndex = 217;
      this.lbl_DIOName_34.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_35
      // 
      this.lbl_DIOName_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_35.Location = new System.Drawing.Point(93, 104);
      this.lbl_DIOName_35.Name = "lbl_DIOName_35";
      this.lbl_DIOName_35.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_35.TabIndex = 216;
      this.lbl_DIOName_35.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOType_33
      // 
      this.lbl_DIOType_33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOType_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOType_33.Location = new System.Drawing.Point(448, 44);
      this.lbl_DIOType_33.Name = "lbl_DIOType_33";
      this.lbl_DIOType_33.Size = new System.Drawing.Size(74, 30);
      this.lbl_DIOType_33.TabIndex = 215;
      this.lbl_DIOType_33.Text = "Enc Out";
      this.lbl_DIOType_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_33
      // 
      this.lbl_DIOName_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_33.Location = new System.Drawing.Point(93, 44);
      this.lbl_DIOName_33.Name = "lbl_DIOName_33";
      this.lbl_DIOName_33.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_33.TabIndex = 214;
      this.lbl_DIOName_33.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_45
      // 
      this.lbl_DIOStatus_45.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_45.Location = new System.Drawing.Point(522, 404);
      this.lbl_DIOStatus_45.Name = "lbl_DIOStatus_45";
      this.lbl_DIOStatus_45.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_45.TabIndex = 211;
      // 
      // lbl_DIOChannel_45
      // 
      this.lbl_DIOChannel_45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_45.Location = new System.Drawing.Point(37, 404);
      this.lbl_DIOChannel_45.Name = "lbl_DIOChannel_45";
      this.lbl_DIOChannel_45.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_45.TabIndex = 210;
      this.lbl_DIOChannel_45.Text = "0045";
      this.lbl_DIOChannel_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOName_32
      // 
      this.lbl_DIOName_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOName_32.Location = new System.Drawing.Point(93, 14);
      this.lbl_DIOName_32.Name = "lbl_DIOName_32";
      this.lbl_DIOName_32.Size = new System.Drawing.Size(356, 30);
      this.lbl_DIOName_32.TabIndex = 212;
      this.lbl_DIOName_32.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_DIOName_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_46
      // 
      this.lbl_DIOChannel_46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_46.Location = new System.Drawing.Point(37, 434);
      this.lbl_DIOChannel_46.Name = "lbl_DIOChannel_46";
      this.lbl_DIOChannel_46.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_46.TabIndex = 208;
      this.lbl_DIOChannel_46.Text = "0046";
      this.lbl_DIOChannel_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_47
      // 
      this.lbl_DIOChannel_47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_47.Location = new System.Drawing.Point(37, 464);
      this.lbl_DIOChannel_47.Name = "lbl_DIOChannel_47";
      this.lbl_DIOChannel_47.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_47.TabIndex = 209;
      this.lbl_DIOChannel_47.Text = "0047";
      this.lbl_DIOChannel_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_42
      // 
      this.lbl_DIOStatus_42.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_42.Location = new System.Drawing.Point(522, 314);
      this.lbl_DIOStatus_42.Name = "lbl_DIOStatus_42";
      this.lbl_DIOStatus_42.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_42.TabIndex = 207;
      // 
      // lbl_DIOStatus_44
      // 
      this.lbl_DIOStatus_44.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_44.Location = new System.Drawing.Point(522, 374);
      this.lbl_DIOStatus_44.Name = "lbl_DIOStatus_44";
      this.lbl_DIOStatus_44.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_44.TabIndex = 206;
      // 
      // lbl_DIOStatus_46
      // 
      this.lbl_DIOStatus_46.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_46.Location = new System.Drawing.Point(522, 434);
      this.lbl_DIOStatus_46.Name = "lbl_DIOStatus_46";
      this.lbl_DIOStatus_46.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_46.TabIndex = 205;
      // 
      // lbl_DIOStatus_47
      // 
      this.lbl_DIOStatus_47.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_47.Location = new System.Drawing.Point(522, 464);
      this.lbl_DIOStatus_47.Name = "lbl_DIOStatus_47";
      this.lbl_DIOStatus_47.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_47.TabIndex = 204;
      // 
      // lbl_DIOStatus_43
      // 
      this.lbl_DIOStatus_43.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_43.Location = new System.Drawing.Point(522, 344);
      this.lbl_DIOStatus_43.Name = "lbl_DIOStatus_43";
      this.lbl_DIOStatus_43.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_43.TabIndex = 203;
      // 
      // lbl_DIOStatus_41
      // 
      this.lbl_DIOStatus_41.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_41.Location = new System.Drawing.Point(522, 284);
      this.lbl_DIOStatus_41.Name = "lbl_DIOStatus_41";
      this.lbl_DIOStatus_41.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_41.TabIndex = 202;
      // 
      // lbl_DIOStatus_40
      // 
      this.lbl_DIOStatus_40.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_40.Location = new System.Drawing.Point(522, 254);
      this.lbl_DIOStatus_40.Name = "lbl_DIOStatus_40";
      this.lbl_DIOStatus_40.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_40.TabIndex = 201;
      // 
      // lbl_DIOStatus_39
      // 
      this.lbl_DIOStatus_39.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_39.Location = new System.Drawing.Point(522, 224);
      this.lbl_DIOStatus_39.Name = "lbl_DIOStatus_39";
      this.lbl_DIOStatus_39.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_39.TabIndex = 200;
      // 
      // lbl_DIOStatus_38
      // 
      this.lbl_DIOStatus_38.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_38.Location = new System.Drawing.Point(522, 194);
      this.lbl_DIOStatus_38.Name = "lbl_DIOStatus_38";
      this.lbl_DIOStatus_38.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_38.TabIndex = 199;
      // 
      // lbl_DIOStatus_37
      // 
      this.lbl_DIOStatus_37.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_37.Location = new System.Drawing.Point(522, 164);
      this.lbl_DIOStatus_37.Name = "lbl_DIOStatus_37";
      this.lbl_DIOStatus_37.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_37.TabIndex = 198;
      // 
      // lbl_DIOStatus_36
      // 
      this.lbl_DIOStatus_36.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_36.Location = new System.Drawing.Point(522, 134);
      this.lbl_DIOStatus_36.Name = "lbl_DIOStatus_36";
      this.lbl_DIOStatus_36.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_36.TabIndex = 197;
      // 
      // lbl_DIOStatus_35
      // 
      this.lbl_DIOStatus_35.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_35.Location = new System.Drawing.Point(522, 104);
      this.lbl_DIOStatus_35.Name = "lbl_DIOStatus_35";
      this.lbl_DIOStatus_35.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_35.TabIndex = 196;
      // 
      // lbl_DIOStatus_34
      // 
      this.lbl_DIOStatus_34.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_34.Location = new System.Drawing.Point(522, 74);
      this.lbl_DIOStatus_34.Name = "lbl_DIOStatus_34";
      this.lbl_DIOStatus_34.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_34.TabIndex = 195;
      // 
      // lbl_DIOStatus_33
      // 
      this.lbl_DIOStatus_33.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_33.Location = new System.Drawing.Point(522, 44);
      this.lbl_DIOStatus_33.Name = "lbl_DIOStatus_33";
      this.lbl_DIOStatus_33.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_33.TabIndex = 194;
      // 
      // lbl_DIOChannel_44
      // 
      this.lbl_DIOChannel_44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_44.Location = new System.Drawing.Point(37, 374);
      this.lbl_DIOChannel_44.Name = "lbl_DIOChannel_44";
      this.lbl_DIOChannel_44.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_44.TabIndex = 192;
      this.lbl_DIOChannel_44.Text = "0044";
      this.lbl_DIOChannel_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_41
      // 
      this.lbl_DIOChannel_41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_41.Location = new System.Drawing.Point(37, 284);
      this.lbl_DIOChannel_41.Name = "lbl_DIOChannel_41";
      this.lbl_DIOChannel_41.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_41.TabIndex = 191;
      this.lbl_DIOChannel_41.Text = "0041";
      this.lbl_DIOChannel_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_42
      // 
      this.lbl_DIOChannel_42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_42.Location = new System.Drawing.Point(37, 314);
      this.lbl_DIOChannel_42.Name = "lbl_DIOChannel_42";
      this.lbl_DIOChannel_42.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_42.TabIndex = 189;
      this.lbl_DIOChannel_42.Text = "0042";
      this.lbl_DIOChannel_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_39
      // 
      this.lbl_DIOChannel_39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_39.Location = new System.Drawing.Point(37, 224);
      this.lbl_DIOChannel_39.Name = "lbl_DIOChannel_39";
      this.lbl_DIOChannel_39.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_39.TabIndex = 188;
      this.lbl_DIOChannel_39.Text = "0039";
      this.lbl_DIOChannel_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_40
      // 
      this.lbl_DIOChannel_40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_40.Location = new System.Drawing.Point(37, 254);
      this.lbl_DIOChannel_40.Name = "lbl_DIOChannel_40";
      this.lbl_DIOChannel_40.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_40.TabIndex = 187;
      this.lbl_DIOChannel_40.Text = "0040";
      this.lbl_DIOChannel_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_37
      // 
      this.lbl_DIOChannel_37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_37.Location = new System.Drawing.Point(37, 164);
      this.lbl_DIOChannel_37.Name = "lbl_DIOChannel_37";
      this.lbl_DIOChannel_37.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_37.TabIndex = 186;
      this.lbl_DIOChannel_37.Text = "0037";
      this.lbl_DIOChannel_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_38
      // 
      this.lbl_DIOChannel_38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_38.Location = new System.Drawing.Point(37, 194);
      this.lbl_DIOChannel_38.Name = "lbl_DIOChannel_38";
      this.lbl_DIOChannel_38.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_38.TabIndex = 185;
      this.lbl_DIOChannel_38.Text = "0038";
      this.lbl_DIOChannel_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOStatus_32
      // 
      this.lbl_DIOStatus_32.BackColor = System.Drawing.Color.Red;
      this.lbl_DIOStatus_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOStatus_32.Location = new System.Drawing.Point(522, 14);
      this.lbl_DIOStatus_32.Name = "lbl_DIOStatus_32";
      this.lbl_DIOStatus_32.Size = new System.Drawing.Size(30, 30);
      this.lbl_DIOStatus_32.TabIndex = 193;
      // 
      // lbl_DIOChannel_33
      // 
      this.lbl_DIOChannel_33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_33.Location = new System.Drawing.Point(37, 44);
      this.lbl_DIOChannel_33.Name = "lbl_DIOChannel_33";
      this.lbl_DIOChannel_33.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_33.TabIndex = 181;
      this.lbl_DIOChannel_33.Text = "0033";
      this.lbl_DIOChannel_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_34
      // 
      this.lbl_DIOChannel_34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_34.Location = new System.Drawing.Point(37, 74);
      this.lbl_DIOChannel_34.Name = "lbl_DIOChannel_34";
      this.lbl_DIOChannel_34.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_34.TabIndex = 182;
      this.lbl_DIOChannel_34.Text = "0034";
      this.lbl_DIOChannel_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_35
      // 
      this.lbl_DIOChannel_35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_35.Location = new System.Drawing.Point(37, 104);
      this.lbl_DIOChannel_35.Name = "lbl_DIOChannel_35";
      this.lbl_DIOChannel_35.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_35.TabIndex = 183;
      this.lbl_DIOChannel_35.Text = "0035";
      this.lbl_DIOChannel_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_36
      // 
      this.lbl_DIOChannel_36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_36.Location = new System.Drawing.Point(37, 134);
      this.lbl_DIOChannel_36.Name = "lbl_DIOChannel_36";
      this.lbl_DIOChannel_36.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_36.TabIndex = 184;
      this.lbl_DIOChannel_36.Text = "0036";
      this.lbl_DIOChannel_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_32
      // 
      this.lbl_DIOChannel_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_32.Location = new System.Drawing.Point(37, 14);
      this.lbl_DIOChannel_32.Name = "lbl_DIOChannel_32";
      this.lbl_DIOChannel_32.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_32.TabIndex = 180;
      this.lbl_DIOChannel_32.Text = "0032";
      this.lbl_DIOChannel_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DIOChannel_43
      // 
      this.lbl_DIOChannel_43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_DIOChannel_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DIOChannel_43.Location = new System.Drawing.Point(37, 344);
      this.lbl_DIOChannel_43.Name = "lbl_DIOChannel_43";
      this.lbl_DIOChannel_43.Size = new System.Drawing.Size(55, 30);
      this.lbl_DIOChannel_43.TabIndex = 190;
      this.lbl_DIOChannel_43.Text = "0043";
      this.lbl_DIOChannel_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnl_DIOMain
      // 
      this.pnl_DIOMain.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_1);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_7);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_8);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_9);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_10);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_11);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_12);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_13);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_14);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_15);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_2);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_3);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_4);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_5);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_6);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_0);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_22);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_0);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_21);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_11);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_20);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_4);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_19);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_3);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_18);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_2);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_31);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_1);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_30);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_0);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_29);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_6);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_28);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_5);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_27);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_8);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_26);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_7);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_25);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_10);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_24);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_9);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_23);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_12);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_26);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_1);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_25);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_2);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_24);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_3);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_31);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_4);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_30);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_5);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_29);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_6);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_28);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_7);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_23);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_8);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_22);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_9);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_21);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_11);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_20);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_15);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_27);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_14);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_18);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_12);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_19);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_10);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_17);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_15);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_17);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_14);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOType_16);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_13);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_16);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_13);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_29);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_0);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_29);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_30);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_1);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_31);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_26);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_3);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_28);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_2);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_30);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_11);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_31);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_4);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_27);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_5);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_25);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_6);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_24);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_7);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_23);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_12);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_22);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_13);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_21);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_14);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_20);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_15);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_19);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_8);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_18);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_17);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_10);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_28);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_25);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_26);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_23);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_24);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_21);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_22);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_16);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOStatus_16);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_17);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_18);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_19);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_20);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOChannel_27);
      this.pnl_DIOMain.Controls.Add(this.lbl_DIOName_9);
      this.pnl_DIOMain.Location = new System.Drawing.Point(68, 173);
      this.pnl_DIOMain.Name = "pnl_DIOMain";
      this.pnl_DIOMain.Size = new System.Drawing.Size(1107, 542);
      this.pnl_DIOMain.TabIndex = 324;
      // 
      // Frm_DIOStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pnl_DIOMain);
      this.Controls.Add(this.pnl_DIOExpansion);
      this.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "Frm_DIOStatus";
      this.Text = "Frm_DIOStatus";
      this.Load += new System.EventHandler(this.Frm_DIOStatus_Load);
      this.Activated += new System.EventHandler(this.Frm_DIOStatus_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.pnl_DIOExpansion, 0);
      this.Controls.SetChildIndex(this.pnl_DIOMain, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab5, 0);
      this.Controls.SetChildIndex(this.tab_Tab6, 0);
      this.Controls.SetChildIndex(this.tab_Tab7, 0);
      this.Controls.SetChildIndex(this.tab_Tab8, 0);
      this.Controls.SetChildIndex(this.tab_TabA, 0);
      this.Controls.SetChildIndex(this.tab_TabD, 0);
      this.Controls.SetChildIndex(this.tab_TabC, 0);
      this.Controls.SetChildIndex(this.tab_TabB, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_DIOExpansion.ResumeLayout(false);
      this.pnl_DIOMain.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion

    private void updateControlsEnable() 
    {
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      if (!Accessibility.getAccessibility().isLevelHigh())
      {
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
      }
      else 
      {
        this.lbl_CtrlF2_YY.Text = "Pulse Digout";
        this.lbl_CtrlF3_YY.Text = "Set Digiout";
        this.lbl_CtrlF4_YY.Text = "Clear Digout";
      }
    }

    private void Frm_DIOStatus_Activated(object sender, EventArgs e)
    {
      this.updateControlsEnable(); 
    }

		private void _setErrorDIOStatus(bool error)
		{
			
			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach (FieldInfo field in fields)
			{
				if (field.Name.StartsWith("lbl_DIO"))
				{
					try
					{
						Label l = (Label)field.GetValue(this);
						if(error)
							l.ForeColor = Color.Red;
						else
							l.ForeColor = Color.Black;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _clearDIOStatus()
		{
			this.F10();
			this.tab_TabB.Visible = false;

			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach(FieldInfo field in fields)
			{
				if(field.Name.StartsWith( "lbl_DIO"))
				{	
					try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = "";
					l.BackColor = Color.WhiteSmoke;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
				}
				}
			}
		}

		private void _clearDIOStatus(int num)
		{
			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach (FieldInfo field in fields)
			{
				if (field.Name.EndsWith(num.ToString()))
				{
					try
					{
						Label l = (Label)field.GetValue(this);
						l.Text = "";
						l.BackColor = Color.Black;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _setDIOStatus(int num, int channelNumber, string name, string typeDIO, bool invert, bool status)
		{
			if(num>32)
				this.tab_TabB.Visible = true;

			//channelNumber
			Type type = this.GetType();
			FieldInfo field = type.GetField("lbl_DIOChannel_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = channelNumber.ToString();
					if (invert)
						l.BackColor = Color.YellowGreen;
					else
						l.BackColor = Color.WhiteSmoke;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
				}
			}
			//name
			type = this.GetType();
			field = type.GetField("lbl_DIOName_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = name;
					if (invert)
						l.BackColor = Color.YellowGreen;
					else
						l.BackColor = Color.WhiteSmoke;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
				}
			}
			//type
			type = this.GetType();
			field = type.GetField("lbl_DIOType_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = typeDIO;
					if (invert)
						l.BackColor = Color.YellowGreen;
					else
						l.BackColor = Color.WhiteSmoke;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
				}
			}

			//Status
			this._setDIOStatus(num, status);

		}

		private void _setDIOStatus(int num, bool status)
		{
			Type type = this.GetType();
			FieldInfo field = type.GetField("lbl_DIOStatus_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					if (status)
					{
						if (l.BackColor != Color.Red)
							l.BackColor = Color.Red;
					}
					else
					{
						if (l.BackColor != Color.Gray)
							l.BackColor = Color.Gray;
					}
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setDIOStatus - Reflection failed", ex);
				}
			}
		}

		protected override void PageDown()
		{
			if (PageManager.getPageManager().pagesHistorian.CurrentDSPModule < 60)
			{
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule++;
				SendDIOModule();
			}
		}

		protected override void PageUp()
		{
			if (PageManager.getPageManager().pagesHistorian.CurrentDSPModule > 0)
			{
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule--;
				SendDIOModule();
			}
		}

		private void Frm_DIOStatus_Load(object sender, EventArgs e)
		{
			SendDIOModule();
		}

		private void SendDIOModule()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, Command.PAGE_MANAGER + "." + Command.SET_DIO_MODULE + "(" +
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule.ToString() + ")");
			this._clearDIOStatus();
		}

	}
}

