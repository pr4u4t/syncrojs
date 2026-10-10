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
	public class Frm_AIOStatus : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
	{
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.Title ttl_Value;
		private SyncroC.Controls.Title ttl_Type;
		private SyncroC.Controls.Title ttl_Name;
		private SyncroC.Controls.Title ttl_Channel;
		private Label lbl_AIOValue_0;
		private Label lbl_AIOType_0;
		private Label lbl_AIOChannel_0;
		private Label lbl_AIOName_0;
		private Panel pnl_00To16;
		private Label lbl_AIOChannel_4;
		private Label lbl_AIOValue_4;
		private Label lbl_AIOName_4;
		private Label lbl_AIOType_4;
		private Label lbl_AIOChannel_5;
		private Label lbl_AIOValue_5;
		private Label lbl_AIOName_5;
		private Label lbl_AIOType_5;
		private Label lbl_AIOChannel_6;
		private Label lbl_AIOValue_6;
		private Label lbl_AIOName_6;
		private Label lbl_AIOType_6;
		private Label lbl_AIOChannel_7;
		private Label lbl_AIOValue_7;
		private Label lbl_AIOName_7;
		private Label lbl_AIOType_7;
		private Label lbl_AIOChannel_2;
		private Label lbl_AIOValue_2;
		private Label lbl_AIOName_2;
		private Label lbl_AIOType_2;
		private Label lbl_AIOChannel_3;
		private Label lbl_AIOValue_3;
		private Label lbl_AIOName_3;
		private Label lbl_AIOType_3;
		private Label lbl_AIOChannel_1;
		private Label lbl_AIOValue_1;
		private Label lbl_AIOName_1;
		private Label lbl_AIOType_1;
		private Label lbl_AIOChannel_12;
		private Label lbl_AIOValue_12;
		private Label lbl_AIOName_12;
		private Label lbl_AIOType_12;
		private Label lbl_AIOChannel_11;
		private Label lbl_AIOValue_11;
		private Label lbl_AIOName_11;
		private Label lbl_AIOType_11;
		private Label lbl_AIOChannel_10;
		private Label lbl_AIOValue_10;
		private Label lbl_AIOName_10;
		private Label lbl_AIOType_10;
		private Label lbl_AIOChannel_9;
		private Label lbl_AIOValue_9;
		private Label lbl_AIOName_9;
		private Label lbl_AIOType_9;
		private Label lbl_AIOChannel_8;
		private Label lbl_AIOValue_8;
		private Label lbl_AIOName_8;
		private Label lbl_AIOType_8;
		private Label lbl_AIOChannel_14;
		private Label lbl_AIOValue_14;
		private Label lbl_AIOName_14;
		private Label lbl_AIOType_14;
		private Label lbl_AIOChannel_13;
		private Label lbl_AIOValue_13;
		private Label lbl_AIOName_13;
		private Label lbl_AIOType_13;
		private Label lbl_AIOChannel_15;
		private Label lbl_AIOValue_15;
		private Label lbl_AIOName_15;
		private Label lbl_AIOType_15;
		private Label lbl_AIOChannel_16;
		private Label lbl_AIOValue_16;
		private Label lbl_AIOName_16;
		private Label lbl_AIOType_16;
		private Panel pnl_17To33;
		private Label lbl_AIOChannel_33;
		private Label lbl_AIOValue_33;
		private Label lbl_AIOName_33;
		private Label lbl_AIOType_33;
		private Label lbl_AIOChannel_32;
		private Label lbl_AIOValue_32;
		private Label lbl_AIOName_32;
		private Label lbl_AIOType_32;
		private Label lbl_AIOChannel_31;
		private Label lbl_AIOValue_31;
		private Label lbl_AIOName_31;
		private Label lbl_AIOType_31;
		private Label lbl_AIOChannel_30;
		private Label lbl_AIOValue_30;
		private Label lbl_AIOName_30;
		private Label lbl_AIOType_30;
		private Label lbl_AIOChannel_29;
		private Label lbl_AIOValue_29;
		private Label lbl_AIOName_29;
		private Label lbl_AIOType_29;
		private Label lbl_AIOChannel_28;
		private Label lbl_AIOValue_28;
		private Label lbl_AIOName_28;
		private Label lbl_AIOType_28;
		private Label lbl_AIOChannel_27;
		private Label lbl_AIOValue_27;
		private Label lbl_AIOName_27;
		private Label lbl_AIOType_27;
		private Label lbl_AIOChannel_26;
		private Label lbl_AIOValue_26;
		private Label lbl_AIOName_26;
		private Label lbl_AIOType_26;
		private Label lbl_AIOChannel_25;
		private Label lbl_AIOValue_25;
		private Label lbl_AIOName_25;
		private Label lbl_AIOType_25;
		private Label lbl_AIOChannel_21;
		private Label lbl_AIOValue_21;
		private Label lbl_AIOName_21;
		private Label lbl_AIOType_21;
		private Label lbl_AIOChannel_22;
		private Label lbl_AIOValue_22;
		private Label lbl_AIOName_22;
		private Label lbl_AIOType_22;
		private Label lbl_AIOChannel_23;
		private Label lbl_AIOValue_23;
		private Label lbl_AIOName_23;
		private Label lbl_AIOType_23;
		private Label lbl_AIOChannel_24;
		private Label lbl_AIOValue_24;
		private Label lbl_AIOName_24;
		private Label lbl_AIOType_24;
		private Label lbl_AIOChannel_19;
		private Label lbl_AIOValue_19;
		private Label lbl_AIOName_19;
		private Label lbl_AIOType_19;
		private Label lbl_AIOChannel_20;
		private Label lbl_AIOValue_20;
		private Label lbl_AIOName_20;
		private Label lbl_AIOType_20;
		private Label lbl_AIOChannel_18;
		private Label lbl_AIOValue_18;
		private Label lbl_AIOName_18;
		private Label lbl_AIOType_18;
		private Label lbl_AIOChannel_17;
		private Label lbl_AIOValue_17;
		private Label lbl_AIOName_17;
		private Label lbl_AIOType_17;
		private Panel pnl_34To50;
		private Label lbl_AIOChannel_50;
		private Label lbl_AIOValue_50;
		private Label lbl_AIOName_50;
		private Label lbl_AIOType_50;
		private Label lbl_AIOChannel_49;
		private Label lbl_AIOValue_49;
		private Label lbl_AIOName_49;
		private Label lbl_AIOType_49;
		private Label lbl_AIOChannel_48;
		private Label lbl_AIOValue_48;
		private Label lbl_AIOName_48;
		private Label lbl_AIOType_48;
		private Label lbl_AIOChannel_47;
		private Label lbl_AIOValue_47;
		private Label lbl_AIOName_47;
		private Label lbl_AIOType_47;
		private Label lbl_AIOChannel_46;
		private Label lbl_AIOValue_46;
		private Label lbl_AIOName_46;
		private Label lbl_AIOType_46;
		private Label lbl_AIOChannel_45;
		private Label lbl_AIOValue_45;
		private Label lbl_AIOName_45;
		private Label lbl_AIOType_45;
		private Label lbl_AIOChannel_44;
		private Label lbl_AIOValue_44;
		private Label lbl_AIOName_44;
		private Label lbl_AIOType_44;
		private Label lbl_AIOChannel_43;
		private Label lbl_AIOValue_43;
		private Label lbl_AIOName_43;
		private Label lbl_AIOType_43;
		private Label lbl_AIOChannel_42;
		private Label lbl_AIOValue_42;
		private Label lbl_AIOName_42;
		private Label lbl_AIOType_42;
		private Label lbl_AIOChannel_38;
		private Label lbl_AIOValue_38;
		private Label lbl_AIOName_38;
		private Label lbl_AIOType_38;
		private Label lbl_AIOChannel_39;
		private Label lbl_AIOValue_39;
		private Label lbl_AIOName_39;
		private Label lbl_AIOType_39;
		private Label lbl_AIOChannel_40;
		private Label lbl_AIOValue_40;
		private Label lbl_AIOName_40;
		private Label lbl_AIOType_40;
		private Label lbl_AIOChannel_41;
		private Label lbl_AIOValue_41;
		private Label lbl_AIOName_41;
		private Label lbl_AIOType_41;
		private Label lbl_AIOChannel_36;
		private Label lbl_AIOValue_36;
		private Label lbl_AIOName_36;
		private Label lbl_AIOType_36;
		private Label lbl_AIOChannel_37;
		private Label lbl_AIOValue_37;
		private Label lbl_AIOName_37;
		private Label lbl_AIOType_37;
		private Label lbl_AIOChannel_35;
		private Label lbl_AIOValue_35;
		private Label lbl_AIOName_35;
		private Label lbl_AIOType_35;
		private Label lbl_AIOChannel_34;
		private Label lbl_AIOValue_34;
		private Label lbl_AIOType_34;
    private Label lbl_AIOName_34;
    private Panel pnl_51To67;
    private Label lbl_AIOChannel_67;
    private Label lbl_AIOValue_67;
    private Label lbl_AIOName_67;
    private Label lbl_AIOType_67;
    private Label lbl_AIOChannel_66;
    private Label lbl_AIOValue_66;
    private Label lbl_AIOName_66;
    private Label lbl_AIOType_66;
    private Label lbl_AIOChannel_65;
    private Label lbl_AIOValue_65;
    private Label lbl_AIOName_65;
    private Label lbl_AIOType_65;
    private Label lbl_AIOChannel_64;
    private Label lbl_AIOValue_64;
    private Label lbl_AIOName_64;
    private Label lbl_AIOType_64;
    private Label lbl_AIOChannel_63;
    private Label lbl_AIOValue_63;
    private Label lbl_AIOName_63;
    private Label lbl_AIOType_63;
    private Label lbl_AIOChannel_62;
    private Label lbl_AIOValue_62;
    private Label lbl_AIOName_62;
    private Label lbl_AIOType_62;
    private Label lbl_AIOChannel_61;
    private Label lbl_AIOValue_61;
    private Label lbl_AIOName_61;
    private Label lbl_AIOType_61;
    private Label lbl_AIOChannel_60;
    private Label lbl_AIOValue_60;
    private Label lbl_AIOName_60;
    private Label lbl_AIOType_60;
    private Label lbl_AIOChannel_59;
    private Label lbl_AIOValue_59;
    private Label lbl_AIOName_59;
    private Label lbl_AIOType_59;
    private Label lbl_AIOChannel_55;
    private Label lbl_AIOValue_55;
    private Label lbl_AIOName_55;
    private Label lbl_AIOType_55;
    private Label lbl_AIOChannel_56;
    private Label lbl_AIOValue_56;
    private Label lbl_AIOName_56;
    private Label lbl_AIOType_56;
    private Label lbl_AIOChannel_57;
    private Label lbl_AIOValue_57;
    private Label lbl_AIOName_57;
    private Label lbl_AIOType_57;
    private Label lbl_AIOChannel_58;
    private Label lbl_AIOValue_58;
    private Label lbl_AIOName_58;
    private Label lbl_AIOType_58;
    private Label lbl_AIOChannel_53;
    private Label lbl_AIOValue_53;
    private Label lbl_AIOName_53;
    private Label lbl_AIOType_53;
    private Label lbl_AIOChannel_54;
    private Label lbl_AIOValue_54;
    private Label lbl_AIOName_54;
    private Label lbl_AIOType_54;
    private Label lbl_AIOChannel_52;
    private Label lbl_AIOValue_52;
    private Label lbl_AIOName_52;
    private Label lbl_AIOType_52;
    private Label lbl_AIOChannel_51;
    private Label lbl_AIOValue_51;
    private Label lbl_AIOType_51;
    private Label lbl_AIOName_51;

    private int lastValue = 0;

		public Frm_AIOStatus()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      if (!Accessibility.getAccessibility().isLevelHigh())
      {
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
      }

			this.F10();
			this._clearAIOStatus();
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

    private void Frm_AIOStatus_Activated(object sender, EventArgs e)
    {
      this.updateControlsEnable();
    }

		protected override void F7()
		{
		}

		protected override void F10()
		{
			this.pnl_00To16.Visible = true;
			this.pnl_00To16.BringToFront();
			this.pnl_17To33.Visible = false;
			this.pnl_17To33.SendToBack();
			this.pnl_34To50.Visible = false;
			this.pnl_34To50.SendToBack();
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.pnl_51To67.Visible = false;
      this.pnl_51To67.SendToBack();

			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
			this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;

		}

		protected override void F11()
		{
			this.pnl_00To16.Visible = false;
			this.pnl_00To16.SendToBack();
			this.pnl_17To33.Visible = true;
			this.pnl_17To33.BringToFront();
			this.pnl_34To50.Visible = false;
			this.pnl_34To50.SendToBack();
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.pnl_51To67.Visible = false;
      this.pnl_51To67.SendToBack();

			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
    }

		protected override void F12()
		{
			this.pnl_00To16.Visible = false;
			this.pnl_00To16.SendToBack();
			this.pnl_17To33.Visible = false;
			this.pnl_17To33.SendToBack();
			this.pnl_34To50.Visible = true;
			this.pnl_34To50.BringToFront();
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.pnl_51To67.Visible = false;
      this.pnl_51To67.SendToBack();

			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
			this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
    }

    //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
    protected override void F13()
    {
      this.pnl_00To16.Visible = false;
      this.pnl_00To16.SendToBack();
      this.pnl_17To33.Visible = false;
      this.pnl_17To33.SendToBack();
      this.pnl_34To50.Visible = false;
      this.pnl_34To50.SendToBack();
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.pnl_51To67.Visible = true;
      this.pnl_51To67.BringToFront();

      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      //RFAZZI_FI18/08/2010_1. Aumentato il numero di AIO visualizzabile a grafica a 67
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
    }

    //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
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
    //  else
    //  {
    //    PageManager.getPageManager()._SetMessage("§SYCC_11");
    //  }
    }

    //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
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
    //  else
    //  {
    //    PageManager.getPageManager()._SetMessage("§SYCC_11");
    //  }
    }

    //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AIOStatus));
      this.ttl_Value = new SyncroC.Controls.Title();
      this.ttl_Type = new SyncroC.Controls.Title();
      this.ttl_Name = new SyncroC.Controls.Title();
      this.ttl_Channel = new SyncroC.Controls.Title();
      this.lbl_AIOValue_0 = new System.Windows.Forms.Label();
      this.lbl_AIOType_0 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_0 = new System.Windows.Forms.Label();
      this.lbl_AIOName_0 = new System.Windows.Forms.Label();
      this.pnl_00To16 = new System.Windows.Forms.Panel();
      this.lbl_AIOChannel_16 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_16 = new System.Windows.Forms.Label();
      this.lbl_AIOName_16 = new System.Windows.Forms.Label();
      this.lbl_AIOType_16 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_15 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_15 = new System.Windows.Forms.Label();
      this.lbl_AIOName_15 = new System.Windows.Forms.Label();
      this.lbl_AIOType_15 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_14 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_14 = new System.Windows.Forms.Label();
      this.lbl_AIOName_14 = new System.Windows.Forms.Label();
      this.lbl_AIOType_14 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_13 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_13 = new System.Windows.Forms.Label();
      this.lbl_AIOName_13 = new System.Windows.Forms.Label();
      this.lbl_AIOType_13 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_12 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_12 = new System.Windows.Forms.Label();
      this.lbl_AIOName_12 = new System.Windows.Forms.Label();
      this.lbl_AIOType_12 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_11 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_11 = new System.Windows.Forms.Label();
      this.lbl_AIOName_11 = new System.Windows.Forms.Label();
      this.lbl_AIOType_11 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_10 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_10 = new System.Windows.Forms.Label();
      this.lbl_AIOName_10 = new System.Windows.Forms.Label();
      this.lbl_AIOType_10 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_9 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_9 = new System.Windows.Forms.Label();
      this.lbl_AIOName_9 = new System.Windows.Forms.Label();
      this.lbl_AIOType_9 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_8 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_8 = new System.Windows.Forms.Label();
      this.lbl_AIOName_8 = new System.Windows.Forms.Label();
      this.lbl_AIOType_8 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_4 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_4 = new System.Windows.Forms.Label();
      this.lbl_AIOName_4 = new System.Windows.Forms.Label();
      this.lbl_AIOType_4 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_5 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_5 = new System.Windows.Forms.Label();
      this.lbl_AIOName_5 = new System.Windows.Forms.Label();
      this.lbl_AIOType_5 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_6 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_6 = new System.Windows.Forms.Label();
      this.lbl_AIOName_6 = new System.Windows.Forms.Label();
      this.lbl_AIOType_6 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_7 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_7 = new System.Windows.Forms.Label();
      this.lbl_AIOName_7 = new System.Windows.Forms.Label();
      this.lbl_AIOType_7 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_2 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_2 = new System.Windows.Forms.Label();
      this.lbl_AIOName_2 = new System.Windows.Forms.Label();
      this.lbl_AIOType_2 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_3 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_3 = new System.Windows.Forms.Label();
      this.lbl_AIOName_3 = new System.Windows.Forms.Label();
      this.lbl_AIOType_3 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_1 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_1 = new System.Windows.Forms.Label();
      this.lbl_AIOName_1 = new System.Windows.Forms.Label();
      this.lbl_AIOType_1 = new System.Windows.Forms.Label();
      this.pnl_17To33 = new System.Windows.Forms.Panel();
      this.lbl_AIOValue_33 = new System.Windows.Forms.Label();
      this.lbl_AIOName_33 = new System.Windows.Forms.Label();
      this.lbl_AIOType_33 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_32 = new System.Windows.Forms.Label();
      this.lbl_AIOName_32 = new System.Windows.Forms.Label();
      this.lbl_AIOType_32 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_31 = new System.Windows.Forms.Label();
      this.lbl_AIOName_31 = new System.Windows.Forms.Label();
      this.lbl_AIOType_31 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_30 = new System.Windows.Forms.Label();
      this.lbl_AIOName_30 = new System.Windows.Forms.Label();
      this.lbl_AIOType_30 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_29 = new System.Windows.Forms.Label();
      this.lbl_AIOName_29 = new System.Windows.Forms.Label();
      this.lbl_AIOType_29 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_28 = new System.Windows.Forms.Label();
      this.lbl_AIOName_28 = new System.Windows.Forms.Label();
      this.lbl_AIOType_28 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_27 = new System.Windows.Forms.Label();
      this.lbl_AIOName_27 = new System.Windows.Forms.Label();
      this.lbl_AIOType_27 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_26 = new System.Windows.Forms.Label();
      this.lbl_AIOName_26 = new System.Windows.Forms.Label();
      this.lbl_AIOType_26 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_25 = new System.Windows.Forms.Label();
      this.lbl_AIOName_25 = new System.Windows.Forms.Label();
      this.lbl_AIOType_25 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_21 = new System.Windows.Forms.Label();
      this.lbl_AIOName_21 = new System.Windows.Forms.Label();
      this.lbl_AIOType_21 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_22 = new System.Windows.Forms.Label();
      this.lbl_AIOName_22 = new System.Windows.Forms.Label();
      this.lbl_AIOType_22 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_23 = new System.Windows.Forms.Label();
      this.lbl_AIOName_23 = new System.Windows.Forms.Label();
      this.lbl_AIOType_23 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_24 = new System.Windows.Forms.Label();
      this.lbl_AIOName_24 = new System.Windows.Forms.Label();
      this.lbl_AIOType_24 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_19 = new System.Windows.Forms.Label();
      this.lbl_AIOName_19 = new System.Windows.Forms.Label();
      this.lbl_AIOType_19 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_20 = new System.Windows.Forms.Label();
      this.lbl_AIOName_20 = new System.Windows.Forms.Label();
      this.lbl_AIOType_20 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_18 = new System.Windows.Forms.Label();
      this.lbl_AIOName_18 = new System.Windows.Forms.Label();
      this.lbl_AIOType_18 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_17 = new System.Windows.Forms.Label();
      this.lbl_AIOType_17 = new System.Windows.Forms.Label();
      this.lbl_AIOName_17 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_33 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_32 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_31 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_30 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_29 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_28 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_27 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_26 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_25 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_21 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_22 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_23 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_24 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_19 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_20 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_18 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_17 = new System.Windows.Forms.Label();
      this.pnl_34To50 = new System.Windows.Forms.Panel();
      this.lbl_AIOChannel_50 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_50 = new System.Windows.Forms.Label();
      this.lbl_AIOName_50 = new System.Windows.Forms.Label();
      this.lbl_AIOType_50 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_49 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_49 = new System.Windows.Forms.Label();
      this.lbl_AIOName_49 = new System.Windows.Forms.Label();
      this.lbl_AIOType_49 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_48 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_48 = new System.Windows.Forms.Label();
      this.lbl_AIOName_48 = new System.Windows.Forms.Label();
      this.lbl_AIOType_48 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_47 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_47 = new System.Windows.Forms.Label();
      this.lbl_AIOName_47 = new System.Windows.Forms.Label();
      this.lbl_AIOType_47 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_46 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_46 = new System.Windows.Forms.Label();
      this.lbl_AIOName_46 = new System.Windows.Forms.Label();
      this.lbl_AIOType_46 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_45 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_45 = new System.Windows.Forms.Label();
      this.lbl_AIOName_45 = new System.Windows.Forms.Label();
      this.lbl_AIOType_45 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_44 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_44 = new System.Windows.Forms.Label();
      this.lbl_AIOName_44 = new System.Windows.Forms.Label();
      this.lbl_AIOType_44 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_43 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_43 = new System.Windows.Forms.Label();
      this.lbl_AIOName_43 = new System.Windows.Forms.Label();
      this.lbl_AIOType_43 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_42 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_42 = new System.Windows.Forms.Label();
      this.lbl_AIOName_42 = new System.Windows.Forms.Label();
      this.lbl_AIOType_42 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_38 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_38 = new System.Windows.Forms.Label();
      this.lbl_AIOName_38 = new System.Windows.Forms.Label();
      this.lbl_AIOType_38 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_39 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_39 = new System.Windows.Forms.Label();
      this.lbl_AIOName_39 = new System.Windows.Forms.Label();
      this.lbl_AIOType_39 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_40 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_40 = new System.Windows.Forms.Label();
      this.lbl_AIOName_40 = new System.Windows.Forms.Label();
      this.lbl_AIOType_40 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_41 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_41 = new System.Windows.Forms.Label();
      this.lbl_AIOName_41 = new System.Windows.Forms.Label();
      this.lbl_AIOType_41 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_36 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_36 = new System.Windows.Forms.Label();
      this.lbl_AIOName_36 = new System.Windows.Forms.Label();
      this.lbl_AIOType_36 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_37 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_37 = new System.Windows.Forms.Label();
      this.lbl_AIOName_37 = new System.Windows.Forms.Label();
      this.lbl_AIOType_37 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_35 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_35 = new System.Windows.Forms.Label();
      this.lbl_AIOName_35 = new System.Windows.Forms.Label();
      this.lbl_AIOType_35 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_34 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_34 = new System.Windows.Forms.Label();
      this.lbl_AIOType_34 = new System.Windows.Forms.Label();
      this.lbl_AIOName_34 = new System.Windows.Forms.Label();
      this.pnl_51To67 = new System.Windows.Forms.Panel();
      this.lbl_AIOChannel_67 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_67 = new System.Windows.Forms.Label();
      this.lbl_AIOName_67 = new System.Windows.Forms.Label();
      this.lbl_AIOType_67 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_66 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_66 = new System.Windows.Forms.Label();
      this.lbl_AIOName_66 = new System.Windows.Forms.Label();
      this.lbl_AIOType_66 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_65 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_65 = new System.Windows.Forms.Label();
      this.lbl_AIOName_65 = new System.Windows.Forms.Label();
      this.lbl_AIOType_65 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_64 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_64 = new System.Windows.Forms.Label();
      this.lbl_AIOName_64 = new System.Windows.Forms.Label();
      this.lbl_AIOType_64 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_63 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_63 = new System.Windows.Forms.Label();
      this.lbl_AIOName_63 = new System.Windows.Forms.Label();
      this.lbl_AIOType_63 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_62 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_62 = new System.Windows.Forms.Label();
      this.lbl_AIOName_62 = new System.Windows.Forms.Label();
      this.lbl_AIOType_62 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_61 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_61 = new System.Windows.Forms.Label();
      this.lbl_AIOName_61 = new System.Windows.Forms.Label();
      this.lbl_AIOType_61 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_60 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_60 = new System.Windows.Forms.Label();
      this.lbl_AIOName_60 = new System.Windows.Forms.Label();
      this.lbl_AIOType_60 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_59 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_59 = new System.Windows.Forms.Label();
      this.lbl_AIOName_59 = new System.Windows.Forms.Label();
      this.lbl_AIOType_59 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_55 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_55 = new System.Windows.Forms.Label();
      this.lbl_AIOName_55 = new System.Windows.Forms.Label();
      this.lbl_AIOType_55 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_56 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_56 = new System.Windows.Forms.Label();
      this.lbl_AIOName_56 = new System.Windows.Forms.Label();
      this.lbl_AIOType_56 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_57 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_57 = new System.Windows.Forms.Label();
      this.lbl_AIOName_57 = new System.Windows.Forms.Label();
      this.lbl_AIOType_57 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_58 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_58 = new System.Windows.Forms.Label();
      this.lbl_AIOName_58 = new System.Windows.Forms.Label();
      this.lbl_AIOType_58 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_53 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_53 = new System.Windows.Forms.Label();
      this.lbl_AIOName_53 = new System.Windows.Forms.Label();
      this.lbl_AIOType_53 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_54 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_54 = new System.Windows.Forms.Label();
      this.lbl_AIOName_54 = new System.Windows.Forms.Label();
      this.lbl_AIOType_54 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_52 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_52 = new System.Windows.Forms.Label();
      this.lbl_AIOName_52 = new System.Windows.Forms.Label();
      this.lbl_AIOType_52 = new System.Windows.Forms.Label();
      this.lbl_AIOChannel_51 = new System.Windows.Forms.Label();
      this.lbl_AIOValue_51 = new System.Windows.Forms.Label();
      this.lbl_AIOType_51 = new System.Windows.Forms.Label();
      this.lbl_AIOName_51 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_00To16.SuspendLayout();
      this.pnl_17To33.SuspendLayout();
      this.pnl_34To50.SuspendLayout();
      this.pnl_51To67.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab7.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabA.Text = "0 - 16";
      this.tab_TabA.TranslateText = false;
      this.tab_TabA.Visible = true;
      // 
      // tab_TabD
      // 
      this.tab_TabD.Text = "51-67";
      this.tab_TabD.Visible = true;
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "34 - 50";
      this.tab_TabC.TranslateText = false;
      this.tab_TabC.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "17 - 33";
      this.tab_TabB.TranslateText = false;
      this.tab_TabB.Visible = true;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Clear Digout";
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
      // ttl_Value
      // 
      this.ttl_Value.BackColor = System.Drawing.Color.Black;
      this.ttl_Value.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Value.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Value.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Value.Location = new System.Drawing.Point(875, 194);
      this.ttl_Value.Name = "ttl_Value";
      this.ttl_Value.Size = new System.Drawing.Size(145, 20);
      this.ttl_Value.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Value.TabIndex = 266;
      this.ttl_Value.Text = "Value";
      this.ttl_Value.TranslateText = true;
      // 
      // ttl_Type
      // 
      this.ttl_Type.BackColor = System.Drawing.Color.Black;
      this.ttl_Type.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Type.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Type.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Type.Location = new System.Drawing.Point(654, 194);
      this.ttl_Type.Name = "ttl_Type";
      this.ttl_Type.Size = new System.Drawing.Size(221, 20);
      this.ttl_Type.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Type.TabIndex = 265;
      this.ttl_Type.Text = "Type";
      this.ttl_Type.TranslateText = true;
      // 
      // ttl_Name
      // 
      this.ttl_Name.BackColor = System.Drawing.Color.Black;
      this.ttl_Name.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Name.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Name.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Name.Location = new System.Drawing.Point(274, 194);
      this.ttl_Name.Name = "ttl_Name";
      this.ttl_Name.Size = new System.Drawing.Size(380, 20);
      this.ttl_Name.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Name.TabIndex = 264;
      this.ttl_Name.Text = "Name";
      this.ttl_Name.TranslateText = true;
      // 
      // ttl_Channel
      // 
      this.ttl_Channel.BackColor = System.Drawing.Color.Black;
      this.ttl_Channel.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Channel.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Channel.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Channel.Location = new System.Drawing.Point(135, 194);
      this.ttl_Channel.Name = "ttl_Channel";
      this.ttl_Channel.Size = new System.Drawing.Size(139, 519);
      this.ttl_Channel.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Channel.TabIndex = 263;
      this.ttl_Channel.Text = "Channel";
      this.ttl_Channel.TranslateText = true;
      // 
      // lbl_AIOValue_0
      // 
      this.lbl_AIOValue_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_0.Location = new System.Drawing.Point(726, 7);
      this.lbl_AIOValue_0.Name = "lbl_AIOValue_0";
      this.lbl_AIOValue_0.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_0.TabIndex = 270;
      this.lbl_AIOValue_0.Text = "00.0000";
      this.lbl_AIOValue_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_0
      // 
      this.lbl_AIOType_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_0.Location = new System.Drawing.Point(470, 7);
      this.lbl_AIOType_0.Name = "lbl_AIOType_0";
      this.lbl_AIOType_0.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_0.TabIndex = 269;
      this.lbl_AIOType_0.Text = "DSP400_DAC";
      this.lbl_AIOType_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_0
      // 
      this.lbl_AIOChannel_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_0.Location = new System.Drawing.Point(6, 7);
      this.lbl_AIOChannel_0.Name = "lbl_AIOChannel_0";
      this.lbl_AIOChannel_0.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_0.TabIndex = 267;
      this.lbl_AIOChannel_0.Text = "0000";
      this.lbl_AIOChannel_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_0
      // 
      this.lbl_AIOName_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_0.Location = new System.Drawing.Point(75, 7);
      this.lbl_AIOName_0.Name = "lbl_AIOName_0";
      this.lbl_AIOName_0.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_0.TabIndex = 268;
      this.lbl_AIOName_0.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnl_00To16
      // 
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_16);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_16);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_16);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_16);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_15);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_15);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_15);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_15);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_14);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_14);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_14);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_14);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_13);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_13);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_13);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_13);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_12);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_12);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_12);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_12);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_11);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_11);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_11);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_11);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_10);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_10);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_10);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_10);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_9);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_9);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_9);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_9);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_8);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_8);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_8);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_8);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_4);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_4);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_4);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_4);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_5);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_5);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_5);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_5);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_6);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_6);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_6);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_6);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_7);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_7);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_7);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_7);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_2);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_2);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_2);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_2);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_3);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_3);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_3);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_3);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_1);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_1);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_1);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_1);
      this.pnl_00To16.Controls.Add(this.lbl_AIOChannel_0);
      this.pnl_00To16.Controls.Add(this.lbl_AIOValue_0);
      this.pnl_00To16.Controls.Add(this.lbl_AIOName_0);
      this.pnl_00To16.Controls.Add(this.lbl_AIOType_0);
      this.pnl_00To16.Location = new System.Drawing.Point(142, 214);
      this.pnl_00To16.Name = "pnl_00To16";
      this.pnl_00To16.Size = new System.Drawing.Size(879, 512);
      this.pnl_00To16.TabIndex = 271;
      // 
      // lbl_AIOChannel_16
      // 
      this.lbl_AIOChannel_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_16.Location = new System.Drawing.Point(6, 471);
      this.lbl_AIOChannel_16.Name = "lbl_AIOChannel_16";
      this.lbl_AIOChannel_16.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_16.TabIndex = 331;
      this.lbl_AIOChannel_16.Text = "0016";
      this.lbl_AIOChannel_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_16
      // 
      this.lbl_AIOValue_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_16.Location = new System.Drawing.Point(726, 471);
      this.lbl_AIOValue_16.Name = "lbl_AIOValue_16";
      this.lbl_AIOValue_16.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_16.TabIndex = 334;
      this.lbl_AIOValue_16.Text = "00.0000";
      this.lbl_AIOValue_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_16
      // 
      this.lbl_AIOName_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_16.Location = new System.Drawing.Point(75, 471);
      this.lbl_AIOName_16.Name = "lbl_AIOName_16";
      this.lbl_AIOName_16.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_16.TabIndex = 332;
      this.lbl_AIOName_16.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_16
      // 
      this.lbl_AIOType_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_16.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_16.Location = new System.Drawing.Point(470, 471);
      this.lbl_AIOType_16.Name = "lbl_AIOType_16";
      this.lbl_AIOType_16.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_16.TabIndex = 333;
      this.lbl_AIOType_16.Text = "DSP400_DAC";
      this.lbl_AIOType_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_15
      // 
      this.lbl_AIOChannel_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_15.Location = new System.Drawing.Point(6, 442);
      this.lbl_AIOChannel_15.Name = "lbl_AIOChannel_15";
      this.lbl_AIOChannel_15.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_15.TabIndex = 327;
      this.lbl_AIOChannel_15.Text = "0015";
      this.lbl_AIOChannel_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_15
      // 
      this.lbl_AIOValue_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_15.Location = new System.Drawing.Point(726, 442);
      this.lbl_AIOValue_15.Name = "lbl_AIOValue_15";
      this.lbl_AIOValue_15.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_15.TabIndex = 330;
      this.lbl_AIOValue_15.Text = "00.0000";
      this.lbl_AIOValue_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_15
      // 
      this.lbl_AIOName_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_15.Location = new System.Drawing.Point(75, 442);
      this.lbl_AIOName_15.Name = "lbl_AIOName_15";
      this.lbl_AIOName_15.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_15.TabIndex = 328;
      this.lbl_AIOName_15.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_15
      // 
      this.lbl_AIOType_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_15.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_15.Location = new System.Drawing.Point(470, 442);
      this.lbl_AIOType_15.Name = "lbl_AIOType_15";
      this.lbl_AIOType_15.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_15.TabIndex = 329;
      this.lbl_AIOType_15.Text = "DSP400_DAC";
      this.lbl_AIOType_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_14
      // 
      this.lbl_AIOChannel_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_14.Location = new System.Drawing.Point(6, 413);
      this.lbl_AIOChannel_14.Name = "lbl_AIOChannel_14";
      this.lbl_AIOChannel_14.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_14.TabIndex = 323;
      this.lbl_AIOChannel_14.Text = "0014";
      this.lbl_AIOChannel_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_14
      // 
      this.lbl_AIOValue_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_14.Location = new System.Drawing.Point(726, 413);
      this.lbl_AIOValue_14.Name = "lbl_AIOValue_14";
      this.lbl_AIOValue_14.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_14.TabIndex = 326;
      this.lbl_AIOValue_14.Text = "00.0000";
      this.lbl_AIOValue_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_14
      // 
      this.lbl_AIOName_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_14.Location = new System.Drawing.Point(75, 413);
      this.lbl_AIOName_14.Name = "lbl_AIOName_14";
      this.lbl_AIOName_14.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_14.TabIndex = 324;
      this.lbl_AIOName_14.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_14
      // 
      this.lbl_AIOType_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_14.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_14.Location = new System.Drawing.Point(470, 413);
      this.lbl_AIOType_14.Name = "lbl_AIOType_14";
      this.lbl_AIOType_14.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_14.TabIndex = 325;
      this.lbl_AIOType_14.Text = "DSP400_DAC";
      this.lbl_AIOType_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_13
      // 
      this.lbl_AIOChannel_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_13.Location = new System.Drawing.Point(6, 384);
      this.lbl_AIOChannel_13.Name = "lbl_AIOChannel_13";
      this.lbl_AIOChannel_13.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_13.TabIndex = 319;
      this.lbl_AIOChannel_13.Text = "0013";
      this.lbl_AIOChannel_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_13
      // 
      this.lbl_AIOValue_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_13.Location = new System.Drawing.Point(726, 384);
      this.lbl_AIOValue_13.Name = "lbl_AIOValue_13";
      this.lbl_AIOValue_13.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_13.TabIndex = 322;
      this.lbl_AIOValue_13.Text = "00.0000";
      this.lbl_AIOValue_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_13
      // 
      this.lbl_AIOName_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_13.Location = new System.Drawing.Point(75, 384);
      this.lbl_AIOName_13.Name = "lbl_AIOName_13";
      this.lbl_AIOName_13.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_13.TabIndex = 320;
      this.lbl_AIOName_13.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_13
      // 
      this.lbl_AIOType_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_13.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_13.Location = new System.Drawing.Point(470, 384);
      this.lbl_AIOType_13.Name = "lbl_AIOType_13";
      this.lbl_AIOType_13.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_13.TabIndex = 321;
      this.lbl_AIOType_13.Text = "DSP400_DAC";
      this.lbl_AIOType_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_12
      // 
      this.lbl_AIOChannel_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_12.Location = new System.Drawing.Point(6, 355);
      this.lbl_AIOChannel_12.Name = "lbl_AIOChannel_12";
      this.lbl_AIOChannel_12.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_12.TabIndex = 315;
      this.lbl_AIOChannel_12.Text = "0012";
      this.lbl_AIOChannel_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_12
      // 
      this.lbl_AIOValue_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_12.Location = new System.Drawing.Point(726, 355);
      this.lbl_AIOValue_12.Name = "lbl_AIOValue_12";
      this.lbl_AIOValue_12.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_12.TabIndex = 318;
      this.lbl_AIOValue_12.Text = "00.0000";
      this.lbl_AIOValue_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_12
      // 
      this.lbl_AIOName_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_12.Location = new System.Drawing.Point(75, 355);
      this.lbl_AIOName_12.Name = "lbl_AIOName_12";
      this.lbl_AIOName_12.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_12.TabIndex = 316;
      this.lbl_AIOName_12.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_12
      // 
      this.lbl_AIOType_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_12.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_12.Location = new System.Drawing.Point(470, 355);
      this.lbl_AIOType_12.Name = "lbl_AIOType_12";
      this.lbl_AIOType_12.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_12.TabIndex = 317;
      this.lbl_AIOType_12.Text = "DSP400_DAC";
      this.lbl_AIOType_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_11
      // 
      this.lbl_AIOChannel_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_11.Location = new System.Drawing.Point(6, 326);
      this.lbl_AIOChannel_11.Name = "lbl_AIOChannel_11";
      this.lbl_AIOChannel_11.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_11.TabIndex = 311;
      this.lbl_AIOChannel_11.Text = "0011";
      this.lbl_AIOChannel_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_11
      // 
      this.lbl_AIOValue_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_11.Location = new System.Drawing.Point(726, 326);
      this.lbl_AIOValue_11.Name = "lbl_AIOValue_11";
      this.lbl_AIOValue_11.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_11.TabIndex = 314;
      this.lbl_AIOValue_11.Text = "00.0000";
      this.lbl_AIOValue_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_11
      // 
      this.lbl_AIOName_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_11.Location = new System.Drawing.Point(75, 326);
      this.lbl_AIOName_11.Name = "lbl_AIOName_11";
      this.lbl_AIOName_11.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_11.TabIndex = 312;
      this.lbl_AIOName_11.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_11
      // 
      this.lbl_AIOType_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_11.Location = new System.Drawing.Point(470, 326);
      this.lbl_AIOType_11.Name = "lbl_AIOType_11";
      this.lbl_AIOType_11.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_11.TabIndex = 313;
      this.lbl_AIOType_11.Text = "DSP400_DAC";
      this.lbl_AIOType_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_10
      // 
      this.lbl_AIOChannel_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_10.Location = new System.Drawing.Point(6, 297);
      this.lbl_AIOChannel_10.Name = "lbl_AIOChannel_10";
      this.lbl_AIOChannel_10.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_10.TabIndex = 307;
      this.lbl_AIOChannel_10.Text = "0010";
      this.lbl_AIOChannel_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_10
      // 
      this.lbl_AIOValue_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_10.Location = new System.Drawing.Point(726, 297);
      this.lbl_AIOValue_10.Name = "lbl_AIOValue_10";
      this.lbl_AIOValue_10.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_10.TabIndex = 310;
      this.lbl_AIOValue_10.Text = "00.0000";
      this.lbl_AIOValue_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_10
      // 
      this.lbl_AIOName_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_10.Location = new System.Drawing.Point(75, 297);
      this.lbl_AIOName_10.Name = "lbl_AIOName_10";
      this.lbl_AIOName_10.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_10.TabIndex = 308;
      this.lbl_AIOName_10.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_10
      // 
      this.lbl_AIOType_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_10.Location = new System.Drawing.Point(470, 297);
      this.lbl_AIOType_10.Name = "lbl_AIOType_10";
      this.lbl_AIOType_10.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_10.TabIndex = 309;
      this.lbl_AIOType_10.Text = "DSP400_DAC";
      this.lbl_AIOType_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_9
      // 
      this.lbl_AIOChannel_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_9.Location = new System.Drawing.Point(6, 268);
      this.lbl_AIOChannel_9.Name = "lbl_AIOChannel_9";
      this.lbl_AIOChannel_9.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_9.TabIndex = 303;
      this.lbl_AIOChannel_9.Text = "0009";
      this.lbl_AIOChannel_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_9
      // 
      this.lbl_AIOValue_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_9.Location = new System.Drawing.Point(726, 268);
      this.lbl_AIOValue_9.Name = "lbl_AIOValue_9";
      this.lbl_AIOValue_9.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_9.TabIndex = 306;
      this.lbl_AIOValue_9.Text = "00.0000";
      this.lbl_AIOValue_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_9
      // 
      this.lbl_AIOName_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_9.Location = new System.Drawing.Point(75, 268);
      this.lbl_AIOName_9.Name = "lbl_AIOName_9";
      this.lbl_AIOName_9.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_9.TabIndex = 304;
      this.lbl_AIOName_9.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_9
      // 
      this.lbl_AIOType_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_9.Location = new System.Drawing.Point(470, 268);
      this.lbl_AIOType_9.Name = "lbl_AIOType_9";
      this.lbl_AIOType_9.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_9.TabIndex = 305;
      this.lbl_AIOType_9.Text = "DSP400_DAC";
      this.lbl_AIOType_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_8
      // 
      this.lbl_AIOChannel_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_8.Location = new System.Drawing.Point(6, 239);
      this.lbl_AIOChannel_8.Name = "lbl_AIOChannel_8";
      this.lbl_AIOChannel_8.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_8.TabIndex = 299;
      this.lbl_AIOChannel_8.Text = "0008";
      this.lbl_AIOChannel_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_8
      // 
      this.lbl_AIOValue_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_8.Location = new System.Drawing.Point(726, 239);
      this.lbl_AIOValue_8.Name = "lbl_AIOValue_8";
      this.lbl_AIOValue_8.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_8.TabIndex = 302;
      this.lbl_AIOValue_8.Text = "00.0000";
      this.lbl_AIOValue_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_8
      // 
      this.lbl_AIOName_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_8.Location = new System.Drawing.Point(75, 239);
      this.lbl_AIOName_8.Name = "lbl_AIOName_8";
      this.lbl_AIOName_8.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_8.TabIndex = 300;
      this.lbl_AIOName_8.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_8
      // 
      this.lbl_AIOType_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_8.Location = new System.Drawing.Point(470, 239);
      this.lbl_AIOType_8.Name = "lbl_AIOType_8";
      this.lbl_AIOType_8.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_8.TabIndex = 301;
      this.lbl_AIOType_8.Text = "DSP400_DAC";
      this.lbl_AIOType_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_4
      // 
      this.lbl_AIOChannel_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_4.Location = new System.Drawing.Point(6, 123);
      this.lbl_AIOChannel_4.Name = "lbl_AIOChannel_4";
      this.lbl_AIOChannel_4.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_4.TabIndex = 295;
      this.lbl_AIOChannel_4.Text = "0004";
      this.lbl_AIOChannel_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_4
      // 
      this.lbl_AIOValue_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_4.Location = new System.Drawing.Point(726, 123);
      this.lbl_AIOValue_4.Name = "lbl_AIOValue_4";
      this.lbl_AIOValue_4.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_4.TabIndex = 298;
      this.lbl_AIOValue_4.Text = "00.0000";
      this.lbl_AIOValue_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_4
      // 
      this.lbl_AIOName_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_4.Location = new System.Drawing.Point(75, 123);
      this.lbl_AIOName_4.Name = "lbl_AIOName_4";
      this.lbl_AIOName_4.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_4.TabIndex = 296;
      this.lbl_AIOName_4.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_4
      // 
      this.lbl_AIOType_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_4.Location = new System.Drawing.Point(470, 123);
      this.lbl_AIOType_4.Name = "lbl_AIOType_4";
      this.lbl_AIOType_4.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_4.TabIndex = 297;
      this.lbl_AIOType_4.Text = "DSP400_DAC";
      this.lbl_AIOType_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_5
      // 
      this.lbl_AIOChannel_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_5.Location = new System.Drawing.Point(6, 152);
      this.lbl_AIOChannel_5.Name = "lbl_AIOChannel_5";
      this.lbl_AIOChannel_5.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_5.TabIndex = 291;
      this.lbl_AIOChannel_5.Text = "0005";
      this.lbl_AIOChannel_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_5
      // 
      this.lbl_AIOValue_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_5.Location = new System.Drawing.Point(726, 152);
      this.lbl_AIOValue_5.Name = "lbl_AIOValue_5";
      this.lbl_AIOValue_5.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_5.TabIndex = 294;
      this.lbl_AIOValue_5.Text = "00.0000";
      this.lbl_AIOValue_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_5
      // 
      this.lbl_AIOName_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_5.Location = new System.Drawing.Point(75, 152);
      this.lbl_AIOName_5.Name = "lbl_AIOName_5";
      this.lbl_AIOName_5.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_5.TabIndex = 292;
      this.lbl_AIOName_5.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_5
      // 
      this.lbl_AIOType_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_5.Location = new System.Drawing.Point(470, 152);
      this.lbl_AIOType_5.Name = "lbl_AIOType_5";
      this.lbl_AIOType_5.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_5.TabIndex = 293;
      this.lbl_AIOType_5.Text = "DSP400_DAC";
      this.lbl_AIOType_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_6
      // 
      this.lbl_AIOChannel_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_6.Location = new System.Drawing.Point(6, 181);
      this.lbl_AIOChannel_6.Name = "lbl_AIOChannel_6";
      this.lbl_AIOChannel_6.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_6.TabIndex = 287;
      this.lbl_AIOChannel_6.Text = "0006";
      this.lbl_AIOChannel_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_6
      // 
      this.lbl_AIOValue_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_6.Location = new System.Drawing.Point(726, 181);
      this.lbl_AIOValue_6.Name = "lbl_AIOValue_6";
      this.lbl_AIOValue_6.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_6.TabIndex = 290;
      this.lbl_AIOValue_6.Text = "00.0000";
      this.lbl_AIOValue_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_6
      // 
      this.lbl_AIOName_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_6.Location = new System.Drawing.Point(75, 181);
      this.lbl_AIOName_6.Name = "lbl_AIOName_6";
      this.lbl_AIOName_6.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_6.TabIndex = 288;
      this.lbl_AIOName_6.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_6
      // 
      this.lbl_AIOType_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_6.Location = new System.Drawing.Point(470, 181);
      this.lbl_AIOType_6.Name = "lbl_AIOType_6";
      this.lbl_AIOType_6.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_6.TabIndex = 289;
      this.lbl_AIOType_6.Text = "DSP400_DAC";
      this.lbl_AIOType_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_7
      // 
      this.lbl_AIOChannel_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_7.Location = new System.Drawing.Point(6, 210);
      this.lbl_AIOChannel_7.Name = "lbl_AIOChannel_7";
      this.lbl_AIOChannel_7.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_7.TabIndex = 283;
      this.lbl_AIOChannel_7.Text = "0007";
      this.lbl_AIOChannel_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_7
      // 
      this.lbl_AIOValue_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_7.Location = new System.Drawing.Point(726, 210);
      this.lbl_AIOValue_7.Name = "lbl_AIOValue_7";
      this.lbl_AIOValue_7.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_7.TabIndex = 286;
      this.lbl_AIOValue_7.Text = "00.0000";
      this.lbl_AIOValue_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_7
      // 
      this.lbl_AIOName_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_7.Location = new System.Drawing.Point(75, 210);
      this.lbl_AIOName_7.Name = "lbl_AIOName_7";
      this.lbl_AIOName_7.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_7.TabIndex = 284;
      this.lbl_AIOName_7.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_7
      // 
      this.lbl_AIOType_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_7.Location = new System.Drawing.Point(470, 210);
      this.lbl_AIOType_7.Name = "lbl_AIOType_7";
      this.lbl_AIOType_7.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_7.TabIndex = 285;
      this.lbl_AIOType_7.Text = "DSP400_DAC";
      this.lbl_AIOType_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_2
      // 
      this.lbl_AIOChannel_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_2.Location = new System.Drawing.Point(6, 65);
      this.lbl_AIOChannel_2.Name = "lbl_AIOChannel_2";
      this.lbl_AIOChannel_2.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_2.TabIndex = 279;
      this.lbl_AIOChannel_2.Text = "0002";
      this.lbl_AIOChannel_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_2
      // 
      this.lbl_AIOValue_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_2.Location = new System.Drawing.Point(726, 65);
      this.lbl_AIOValue_2.Name = "lbl_AIOValue_2";
      this.lbl_AIOValue_2.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_2.TabIndex = 282;
      this.lbl_AIOValue_2.Text = "00.0000";
      this.lbl_AIOValue_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_2
      // 
      this.lbl_AIOName_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_2.Location = new System.Drawing.Point(75, 65);
      this.lbl_AIOName_2.Name = "lbl_AIOName_2";
      this.lbl_AIOName_2.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_2.TabIndex = 280;
      this.lbl_AIOName_2.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_2
      // 
      this.lbl_AIOType_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_2.Location = new System.Drawing.Point(470, 65);
      this.lbl_AIOType_2.Name = "lbl_AIOType_2";
      this.lbl_AIOType_2.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_2.TabIndex = 281;
      this.lbl_AIOType_2.Text = "DSP400_DAC";
      this.lbl_AIOType_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_3
      // 
      this.lbl_AIOChannel_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_3.Location = new System.Drawing.Point(6, 94);
      this.lbl_AIOChannel_3.Name = "lbl_AIOChannel_3";
      this.lbl_AIOChannel_3.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_3.TabIndex = 275;
      this.lbl_AIOChannel_3.Text = "0003";
      this.lbl_AIOChannel_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_3
      // 
      this.lbl_AIOValue_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_3.Location = new System.Drawing.Point(726, 94);
      this.lbl_AIOValue_3.Name = "lbl_AIOValue_3";
      this.lbl_AIOValue_3.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_3.TabIndex = 278;
      this.lbl_AIOValue_3.Text = "00.0000";
      this.lbl_AIOValue_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_3
      // 
      this.lbl_AIOName_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_3.Location = new System.Drawing.Point(75, 94);
      this.lbl_AIOName_3.Name = "lbl_AIOName_3";
      this.lbl_AIOName_3.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_3.TabIndex = 276;
      this.lbl_AIOName_3.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_3
      // 
      this.lbl_AIOType_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_3.Location = new System.Drawing.Point(470, 94);
      this.lbl_AIOType_3.Name = "lbl_AIOType_3";
      this.lbl_AIOType_3.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_3.TabIndex = 277;
      this.lbl_AIOType_3.Text = "DSP400_DAC";
      this.lbl_AIOType_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_1
      // 
      this.lbl_AIOChannel_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_1.Location = new System.Drawing.Point(6, 36);
      this.lbl_AIOChannel_1.Name = "lbl_AIOChannel_1";
      this.lbl_AIOChannel_1.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_1.TabIndex = 271;
      this.lbl_AIOChannel_1.Text = "0001";
      this.lbl_AIOChannel_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_1
      // 
      this.lbl_AIOValue_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_1.Location = new System.Drawing.Point(726, 36);
      this.lbl_AIOValue_1.Name = "lbl_AIOValue_1";
      this.lbl_AIOValue_1.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_1.TabIndex = 274;
      this.lbl_AIOValue_1.Text = "00.0000";
      this.lbl_AIOValue_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_1
      // 
      this.lbl_AIOName_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_1.Location = new System.Drawing.Point(75, 36);
      this.lbl_AIOName_1.Name = "lbl_AIOName_1";
      this.lbl_AIOName_1.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_1.TabIndex = 272;
      this.lbl_AIOName_1.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_1
      // 
      this.lbl_AIOType_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_1.Location = new System.Drawing.Point(470, 36);
      this.lbl_AIOType_1.Name = "lbl_AIOType_1";
      this.lbl_AIOType_1.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_1.TabIndex = 273;
      this.lbl_AIOType_1.Text = "DSP400_DAC";
      this.lbl_AIOType_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnl_17To33
      // 
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_33);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_33);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_33);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_32);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_32);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_32);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_31);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_31);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_31);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_30);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_30);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_30);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_29);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_29);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_29);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_28);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_28);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_28);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_27);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_27);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_27);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_26);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_26);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_26);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_25);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_25);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_25);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_21);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_21);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_21);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_22);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_22);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_22);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_23);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_23);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_23);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_24);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_24);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_24);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_19);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_19);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_19);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_20);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_20);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_20);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_18);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_18);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_18);
      this.pnl_17To33.Controls.Add(this.lbl_AIOValue_17);
      this.pnl_17To33.Controls.Add(this.lbl_AIOType_17);
      this.pnl_17To33.Controls.Add(this.lbl_AIOName_17);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_33);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_32);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_31);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_30);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_29);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_28);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_27);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_26);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_25);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_21);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_22);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_23);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_24);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_19);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_20);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_18);
      this.pnl_17To33.Controls.Add(this.lbl_AIOChannel_17);
      this.pnl_17To33.Location = new System.Drawing.Point(142, 214);
      this.pnl_17To33.Name = "pnl_17To33";
      this.pnl_17To33.Size = new System.Drawing.Size(879, 512);
      this.pnl_17To33.TabIndex = 272;
      this.pnl_17To33.Visible = false;
      // 
      // lbl_AIOValue_33
      // 
      this.lbl_AIOValue_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_33.Location = new System.Drawing.Point(726, 471);
      this.lbl_AIOValue_33.Name = "lbl_AIOValue_33";
      this.lbl_AIOValue_33.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_33.TabIndex = 334;
      this.lbl_AIOValue_33.Text = "00.0000";
      this.lbl_AIOValue_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_33
      // 
      this.lbl_AIOName_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_33.Location = new System.Drawing.Point(75, 471);
      this.lbl_AIOName_33.Name = "lbl_AIOName_33";
      this.lbl_AIOName_33.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_33.TabIndex = 332;
      this.lbl_AIOName_33.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_33
      // 
      this.lbl_AIOType_33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_33.Location = new System.Drawing.Point(470, 471);
      this.lbl_AIOType_33.Name = "lbl_AIOType_33";
      this.lbl_AIOType_33.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_33.TabIndex = 333;
      this.lbl_AIOType_33.Text = "DSP400_DAC";
      this.lbl_AIOType_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_32
      // 
      this.lbl_AIOValue_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_32.Location = new System.Drawing.Point(726, 442);
      this.lbl_AIOValue_32.Name = "lbl_AIOValue_32";
      this.lbl_AIOValue_32.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_32.TabIndex = 330;
      this.lbl_AIOValue_32.Text = "00.0000";
      this.lbl_AIOValue_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_32
      // 
      this.lbl_AIOName_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_32.Location = new System.Drawing.Point(75, 442);
      this.lbl_AIOName_32.Name = "lbl_AIOName_32";
      this.lbl_AIOName_32.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_32.TabIndex = 328;
      this.lbl_AIOName_32.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_32
      // 
      this.lbl_AIOType_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_32.Location = new System.Drawing.Point(470, 442);
      this.lbl_AIOType_32.Name = "lbl_AIOType_32";
      this.lbl_AIOType_32.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_32.TabIndex = 329;
      this.lbl_AIOType_32.Text = "DSP400_DAC";
      this.lbl_AIOType_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_31
      // 
      this.lbl_AIOValue_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_31.Location = new System.Drawing.Point(726, 413);
      this.lbl_AIOValue_31.Name = "lbl_AIOValue_31";
      this.lbl_AIOValue_31.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_31.TabIndex = 326;
      this.lbl_AIOValue_31.Text = "00.0000";
      this.lbl_AIOValue_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_31
      // 
      this.lbl_AIOName_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_31.Location = new System.Drawing.Point(75, 413);
      this.lbl_AIOName_31.Name = "lbl_AIOName_31";
      this.lbl_AIOName_31.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_31.TabIndex = 324;
      this.lbl_AIOName_31.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_31
      // 
      this.lbl_AIOType_31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_31.Location = new System.Drawing.Point(470, 413);
      this.lbl_AIOType_31.Name = "lbl_AIOType_31";
      this.lbl_AIOType_31.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_31.TabIndex = 325;
      this.lbl_AIOType_31.Text = "DSP400_DAC";
      this.lbl_AIOType_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_30
      // 
      this.lbl_AIOValue_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_30.Location = new System.Drawing.Point(726, 384);
      this.lbl_AIOValue_30.Name = "lbl_AIOValue_30";
      this.lbl_AIOValue_30.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_30.TabIndex = 322;
      this.lbl_AIOValue_30.Text = "00.0000";
      this.lbl_AIOValue_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_30
      // 
      this.lbl_AIOName_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_30.Location = new System.Drawing.Point(75, 384);
      this.lbl_AIOName_30.Name = "lbl_AIOName_30";
      this.lbl_AIOName_30.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_30.TabIndex = 320;
      this.lbl_AIOName_30.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_30
      // 
      this.lbl_AIOType_30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_30.Location = new System.Drawing.Point(470, 384);
      this.lbl_AIOType_30.Name = "lbl_AIOType_30";
      this.lbl_AIOType_30.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_30.TabIndex = 321;
      this.lbl_AIOType_30.Text = "DSP400_DAC";
      this.lbl_AIOType_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_29
      // 
      this.lbl_AIOValue_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_29.Location = new System.Drawing.Point(726, 355);
      this.lbl_AIOValue_29.Name = "lbl_AIOValue_29";
      this.lbl_AIOValue_29.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_29.TabIndex = 318;
      this.lbl_AIOValue_29.Text = "00.0000";
      this.lbl_AIOValue_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_29
      // 
      this.lbl_AIOName_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_29.Location = new System.Drawing.Point(75, 355);
      this.lbl_AIOName_29.Name = "lbl_AIOName_29";
      this.lbl_AIOName_29.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_29.TabIndex = 316;
      this.lbl_AIOName_29.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_29
      // 
      this.lbl_AIOType_29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_29.Location = new System.Drawing.Point(470, 355);
      this.lbl_AIOType_29.Name = "lbl_AIOType_29";
      this.lbl_AIOType_29.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_29.TabIndex = 317;
      this.lbl_AIOType_29.Text = "DSP400_DAC";
      this.lbl_AIOType_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_28
      // 
      this.lbl_AIOValue_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_28.Location = new System.Drawing.Point(726, 326);
      this.lbl_AIOValue_28.Name = "lbl_AIOValue_28";
      this.lbl_AIOValue_28.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_28.TabIndex = 314;
      this.lbl_AIOValue_28.Text = "00.0000";
      this.lbl_AIOValue_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_28
      // 
      this.lbl_AIOName_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_28.Location = new System.Drawing.Point(75, 326);
      this.lbl_AIOName_28.Name = "lbl_AIOName_28";
      this.lbl_AIOName_28.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_28.TabIndex = 312;
      this.lbl_AIOName_28.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_28
      // 
      this.lbl_AIOType_28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_28.Location = new System.Drawing.Point(470, 326);
      this.lbl_AIOType_28.Name = "lbl_AIOType_28";
      this.lbl_AIOType_28.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_28.TabIndex = 313;
      this.lbl_AIOType_28.Text = "DSP400_DAC";
      this.lbl_AIOType_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_27
      // 
      this.lbl_AIOValue_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_27.Location = new System.Drawing.Point(726, 297);
      this.lbl_AIOValue_27.Name = "lbl_AIOValue_27";
      this.lbl_AIOValue_27.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_27.TabIndex = 310;
      this.lbl_AIOValue_27.Text = "00.0000";
      this.lbl_AIOValue_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_27
      // 
      this.lbl_AIOName_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_27.Location = new System.Drawing.Point(75, 297);
      this.lbl_AIOName_27.Name = "lbl_AIOName_27";
      this.lbl_AIOName_27.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_27.TabIndex = 308;
      this.lbl_AIOName_27.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_27
      // 
      this.lbl_AIOType_27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_27.Location = new System.Drawing.Point(470, 297);
      this.lbl_AIOType_27.Name = "lbl_AIOType_27";
      this.lbl_AIOType_27.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_27.TabIndex = 309;
      this.lbl_AIOType_27.Text = "DSP400_DAC";
      this.lbl_AIOType_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_26
      // 
      this.lbl_AIOValue_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_26.Location = new System.Drawing.Point(726, 268);
      this.lbl_AIOValue_26.Name = "lbl_AIOValue_26";
      this.lbl_AIOValue_26.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_26.TabIndex = 306;
      this.lbl_AIOValue_26.Text = "00.0000";
      this.lbl_AIOValue_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_26
      // 
      this.lbl_AIOName_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_26.Location = new System.Drawing.Point(75, 268);
      this.lbl_AIOName_26.Name = "lbl_AIOName_26";
      this.lbl_AIOName_26.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_26.TabIndex = 304;
      this.lbl_AIOName_26.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_26
      // 
      this.lbl_AIOType_26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_26.Location = new System.Drawing.Point(470, 268);
      this.lbl_AIOType_26.Name = "lbl_AIOType_26";
      this.lbl_AIOType_26.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_26.TabIndex = 305;
      this.lbl_AIOType_26.Text = "DSP400_DAC";
      this.lbl_AIOType_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_25
      // 
      this.lbl_AIOValue_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_25.Location = new System.Drawing.Point(726, 239);
      this.lbl_AIOValue_25.Name = "lbl_AIOValue_25";
      this.lbl_AIOValue_25.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_25.TabIndex = 302;
      this.lbl_AIOValue_25.Text = "00.0000";
      this.lbl_AIOValue_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_25
      // 
      this.lbl_AIOName_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_25.Location = new System.Drawing.Point(75, 239);
      this.lbl_AIOName_25.Name = "lbl_AIOName_25";
      this.lbl_AIOName_25.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_25.TabIndex = 300;
      this.lbl_AIOName_25.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_25
      // 
      this.lbl_AIOType_25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_25.Location = new System.Drawing.Point(470, 239);
      this.lbl_AIOType_25.Name = "lbl_AIOType_25";
      this.lbl_AIOType_25.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_25.TabIndex = 301;
      this.lbl_AIOType_25.Text = "DSP400_DAC";
      this.lbl_AIOType_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_21
      // 
      this.lbl_AIOValue_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_21.Location = new System.Drawing.Point(726, 123);
      this.lbl_AIOValue_21.Name = "lbl_AIOValue_21";
      this.lbl_AIOValue_21.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_21.TabIndex = 298;
      this.lbl_AIOValue_21.Text = "00.0000";
      this.lbl_AIOValue_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_21
      // 
      this.lbl_AIOName_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_21.Location = new System.Drawing.Point(75, 123);
      this.lbl_AIOName_21.Name = "lbl_AIOName_21";
      this.lbl_AIOName_21.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_21.TabIndex = 296;
      this.lbl_AIOName_21.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_21
      // 
      this.lbl_AIOType_21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_21.Location = new System.Drawing.Point(470, 123);
      this.lbl_AIOType_21.Name = "lbl_AIOType_21";
      this.lbl_AIOType_21.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_21.TabIndex = 297;
      this.lbl_AIOType_21.Text = "DSP400_DAC";
      this.lbl_AIOType_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_22
      // 
      this.lbl_AIOValue_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_22.Location = new System.Drawing.Point(726, 152);
      this.lbl_AIOValue_22.Name = "lbl_AIOValue_22";
      this.lbl_AIOValue_22.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_22.TabIndex = 294;
      this.lbl_AIOValue_22.Text = "00.0000";
      this.lbl_AIOValue_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_22
      // 
      this.lbl_AIOName_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_22.Location = new System.Drawing.Point(75, 152);
      this.lbl_AIOName_22.Name = "lbl_AIOName_22";
      this.lbl_AIOName_22.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_22.TabIndex = 292;
      this.lbl_AIOName_22.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_22
      // 
      this.lbl_AIOType_22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_22.Location = new System.Drawing.Point(470, 152);
      this.lbl_AIOType_22.Name = "lbl_AIOType_22";
      this.lbl_AIOType_22.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_22.TabIndex = 293;
      this.lbl_AIOType_22.Text = "DSP400_DAC";
      this.lbl_AIOType_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_23
      // 
      this.lbl_AIOValue_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_23.Location = new System.Drawing.Point(726, 181);
      this.lbl_AIOValue_23.Name = "lbl_AIOValue_23";
      this.lbl_AIOValue_23.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_23.TabIndex = 290;
      this.lbl_AIOValue_23.Text = "00.0000";
      this.lbl_AIOValue_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_23
      // 
      this.lbl_AIOName_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_23.Location = new System.Drawing.Point(75, 181);
      this.lbl_AIOName_23.Name = "lbl_AIOName_23";
      this.lbl_AIOName_23.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_23.TabIndex = 288;
      this.lbl_AIOName_23.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_23
      // 
      this.lbl_AIOType_23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_23.Location = new System.Drawing.Point(470, 181);
      this.lbl_AIOType_23.Name = "lbl_AIOType_23";
      this.lbl_AIOType_23.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_23.TabIndex = 289;
      this.lbl_AIOType_23.Text = "DSP400_DAC";
      this.lbl_AIOType_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_24
      // 
      this.lbl_AIOValue_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_24.Location = new System.Drawing.Point(726, 210);
      this.lbl_AIOValue_24.Name = "lbl_AIOValue_24";
      this.lbl_AIOValue_24.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_24.TabIndex = 286;
      this.lbl_AIOValue_24.Text = "00.0000";
      this.lbl_AIOValue_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_24
      // 
      this.lbl_AIOName_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_24.Location = new System.Drawing.Point(75, 210);
      this.lbl_AIOName_24.Name = "lbl_AIOName_24";
      this.lbl_AIOName_24.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_24.TabIndex = 284;
      this.lbl_AIOName_24.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_24
      // 
      this.lbl_AIOType_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_24.Location = new System.Drawing.Point(470, 210);
      this.lbl_AIOType_24.Name = "lbl_AIOType_24";
      this.lbl_AIOType_24.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_24.TabIndex = 285;
      this.lbl_AIOType_24.Text = "DSP400_DAC";
      this.lbl_AIOType_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_19
      // 
      this.lbl_AIOValue_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_19.Location = new System.Drawing.Point(726, 65);
      this.lbl_AIOValue_19.Name = "lbl_AIOValue_19";
      this.lbl_AIOValue_19.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_19.TabIndex = 282;
      this.lbl_AIOValue_19.Text = "00.0000";
      this.lbl_AIOValue_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_19
      // 
      this.lbl_AIOName_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_19.Location = new System.Drawing.Point(75, 65);
      this.lbl_AIOName_19.Name = "lbl_AIOName_19";
      this.lbl_AIOName_19.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_19.TabIndex = 280;
      this.lbl_AIOName_19.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_19
      // 
      this.lbl_AIOType_19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_19.Location = new System.Drawing.Point(470, 65);
      this.lbl_AIOType_19.Name = "lbl_AIOType_19";
      this.lbl_AIOType_19.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_19.TabIndex = 281;
      this.lbl_AIOType_19.Text = "DSP400_DAC";
      this.lbl_AIOType_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_20
      // 
      this.lbl_AIOValue_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_20.Location = new System.Drawing.Point(726, 94);
      this.lbl_AIOValue_20.Name = "lbl_AIOValue_20";
      this.lbl_AIOValue_20.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_20.TabIndex = 278;
      this.lbl_AIOValue_20.Text = "00.0000";
      this.lbl_AIOValue_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_20
      // 
      this.lbl_AIOName_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_20.Location = new System.Drawing.Point(75, 94);
      this.lbl_AIOName_20.Name = "lbl_AIOName_20";
      this.lbl_AIOName_20.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_20.TabIndex = 276;
      this.lbl_AIOName_20.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_20
      // 
      this.lbl_AIOType_20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_20.Location = new System.Drawing.Point(470, 94);
      this.lbl_AIOType_20.Name = "lbl_AIOType_20";
      this.lbl_AIOType_20.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_20.TabIndex = 277;
      this.lbl_AIOType_20.Text = "DSP400_DAC";
      this.lbl_AIOType_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_18
      // 
      this.lbl_AIOValue_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_18.Location = new System.Drawing.Point(726, 36);
      this.lbl_AIOValue_18.Name = "lbl_AIOValue_18";
      this.lbl_AIOValue_18.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_18.TabIndex = 274;
      this.lbl_AIOValue_18.Text = "00.0000";
      this.lbl_AIOValue_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_18
      // 
      this.lbl_AIOName_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_18.Location = new System.Drawing.Point(75, 36);
      this.lbl_AIOName_18.Name = "lbl_AIOName_18";
      this.lbl_AIOName_18.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_18.TabIndex = 272;
      this.lbl_AIOName_18.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_18
      // 
      this.lbl_AIOType_18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_18.Location = new System.Drawing.Point(470, 36);
      this.lbl_AIOType_18.Name = "lbl_AIOType_18";
      this.lbl_AIOType_18.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_18.TabIndex = 273;
      this.lbl_AIOType_18.Text = "DSP400_DAC";
      this.lbl_AIOType_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_17
      // 
      this.lbl_AIOValue_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_17.Location = new System.Drawing.Point(726, 7);
      this.lbl_AIOValue_17.Name = "lbl_AIOValue_17";
      this.lbl_AIOValue_17.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_17.TabIndex = 270;
      this.lbl_AIOValue_17.Text = "00.0000";
      this.lbl_AIOValue_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_17
      // 
      this.lbl_AIOType_17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_17.Location = new System.Drawing.Point(470, 7);
      this.lbl_AIOType_17.Name = "lbl_AIOType_17";
      this.lbl_AIOType_17.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_17.TabIndex = 269;
      this.lbl_AIOType_17.Text = "DSP400_DAC";
      this.lbl_AIOType_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_17
      // 
      this.lbl_AIOName_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_17.Location = new System.Drawing.Point(75, 7);
      this.lbl_AIOName_17.Name = "lbl_AIOName_17";
      this.lbl_AIOName_17.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_17.TabIndex = 268;
      this.lbl_AIOName_17.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_33
      // 
      this.lbl_AIOChannel_33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_33.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_33.Location = new System.Drawing.Point(6, 471);
      this.lbl_AIOChannel_33.Name = "lbl_AIOChannel_33";
      this.lbl_AIOChannel_33.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_33.TabIndex = 331;
      this.lbl_AIOChannel_33.Text = "0033";
      this.lbl_AIOChannel_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_32
      // 
      this.lbl_AIOChannel_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_32.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_32.Location = new System.Drawing.Point(6, 442);
      this.lbl_AIOChannel_32.Name = "lbl_AIOChannel_32";
      this.lbl_AIOChannel_32.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_32.TabIndex = 327;
      this.lbl_AIOChannel_32.Text = "0032";
      this.lbl_AIOChannel_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_31
      // 
      this.lbl_AIOChannel_31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_31.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_31.Location = new System.Drawing.Point(6, 413);
      this.lbl_AIOChannel_31.Name = "lbl_AIOChannel_31";
      this.lbl_AIOChannel_31.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_31.TabIndex = 323;
      this.lbl_AIOChannel_31.Text = "0031";
      this.lbl_AIOChannel_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_30
      // 
      this.lbl_AIOChannel_30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_30.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_30.Location = new System.Drawing.Point(6, 384);
      this.lbl_AIOChannel_30.Name = "lbl_AIOChannel_30";
      this.lbl_AIOChannel_30.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_30.TabIndex = 319;
      this.lbl_AIOChannel_30.Text = "0030";
      this.lbl_AIOChannel_30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_29
      // 
      this.lbl_AIOChannel_29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_29.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_29.Location = new System.Drawing.Point(6, 355);
      this.lbl_AIOChannel_29.Name = "lbl_AIOChannel_29";
      this.lbl_AIOChannel_29.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_29.TabIndex = 315;
      this.lbl_AIOChannel_29.Text = "0029";
      this.lbl_AIOChannel_29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_28
      // 
      this.lbl_AIOChannel_28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_28.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_28.Location = new System.Drawing.Point(6, 326);
      this.lbl_AIOChannel_28.Name = "lbl_AIOChannel_28";
      this.lbl_AIOChannel_28.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_28.TabIndex = 311;
      this.lbl_AIOChannel_28.Text = "0028";
      this.lbl_AIOChannel_28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_27
      // 
      this.lbl_AIOChannel_27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_27.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_27.Location = new System.Drawing.Point(6, 297);
      this.lbl_AIOChannel_27.Name = "lbl_AIOChannel_27";
      this.lbl_AIOChannel_27.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_27.TabIndex = 307;
      this.lbl_AIOChannel_27.Text = "0027";
      this.lbl_AIOChannel_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_26
      // 
      this.lbl_AIOChannel_26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_26.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_26.Location = new System.Drawing.Point(6, 268);
      this.lbl_AIOChannel_26.Name = "lbl_AIOChannel_26";
      this.lbl_AIOChannel_26.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_26.TabIndex = 303;
      this.lbl_AIOChannel_26.Text = "0026";
      this.lbl_AIOChannel_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_25
      // 
      this.lbl_AIOChannel_25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_25.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_25.Location = new System.Drawing.Point(6, 239);
      this.lbl_AIOChannel_25.Name = "lbl_AIOChannel_25";
      this.lbl_AIOChannel_25.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_25.TabIndex = 299;
      this.lbl_AIOChannel_25.Text = "0025";
      this.lbl_AIOChannel_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_21
      // 
      this.lbl_AIOChannel_21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_21.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_21.Location = new System.Drawing.Point(6, 123);
      this.lbl_AIOChannel_21.Name = "lbl_AIOChannel_21";
      this.lbl_AIOChannel_21.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_21.TabIndex = 295;
      this.lbl_AIOChannel_21.Text = "0021";
      this.lbl_AIOChannel_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_22
      // 
      this.lbl_AIOChannel_22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_22.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_22.Location = new System.Drawing.Point(6, 152);
      this.lbl_AIOChannel_22.Name = "lbl_AIOChannel_22";
      this.lbl_AIOChannel_22.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_22.TabIndex = 291;
      this.lbl_AIOChannel_22.Text = "0022";
      this.lbl_AIOChannel_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_23
      // 
      this.lbl_AIOChannel_23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_23.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_23.Location = new System.Drawing.Point(6, 181);
      this.lbl_AIOChannel_23.Name = "lbl_AIOChannel_23";
      this.lbl_AIOChannel_23.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_23.TabIndex = 287;
      this.lbl_AIOChannel_23.Text = "0023";
      this.lbl_AIOChannel_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_24
      // 
      this.lbl_AIOChannel_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_24.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_24.Location = new System.Drawing.Point(6, 210);
      this.lbl_AIOChannel_24.Name = "lbl_AIOChannel_24";
      this.lbl_AIOChannel_24.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_24.TabIndex = 283;
      this.lbl_AIOChannel_24.Text = "0024";
      this.lbl_AIOChannel_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_19
      // 
      this.lbl_AIOChannel_19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_19.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_19.Location = new System.Drawing.Point(6, 65);
      this.lbl_AIOChannel_19.Name = "lbl_AIOChannel_19";
      this.lbl_AIOChannel_19.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_19.TabIndex = 279;
      this.lbl_AIOChannel_19.Text = "0019";
      this.lbl_AIOChannel_19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_20
      // 
      this.lbl_AIOChannel_20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_20.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_20.Location = new System.Drawing.Point(6, 94);
      this.lbl_AIOChannel_20.Name = "lbl_AIOChannel_20";
      this.lbl_AIOChannel_20.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_20.TabIndex = 275;
      this.lbl_AIOChannel_20.Text = "0020";
      this.lbl_AIOChannel_20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_18
      // 
      this.lbl_AIOChannel_18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_18.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_18.Location = new System.Drawing.Point(6, 36);
      this.lbl_AIOChannel_18.Name = "lbl_AIOChannel_18";
      this.lbl_AIOChannel_18.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_18.TabIndex = 271;
      this.lbl_AIOChannel_18.Text = "0018";
      this.lbl_AIOChannel_18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_17
      // 
      this.lbl_AIOChannel_17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_17.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_17.Location = new System.Drawing.Point(6, 7);
      this.lbl_AIOChannel_17.Name = "lbl_AIOChannel_17";
      this.lbl_AIOChannel_17.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_17.TabIndex = 267;
      this.lbl_AIOChannel_17.Text = "0017";
      this.lbl_AIOChannel_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnl_34To50
      // 
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_50);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_50);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_50);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_50);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_49);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_49);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_49);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_49);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_48);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_48);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_48);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_48);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_47);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_47);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_47);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_47);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_46);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_46);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_46);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_46);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_45);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_45);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_45);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_45);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_44);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_44);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_44);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_44);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_43);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_43);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_43);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_43);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_42);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_42);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_42);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_42);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_38);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_38);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_38);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_38);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_39);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_39);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_39);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_39);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_40);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_40);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_40);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_40);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_41);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_41);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_41);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_41);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_36);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_36);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_36);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_36);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_37);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_37);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_37);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_37);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_35);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_35);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_35);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_35);
      this.pnl_34To50.Controls.Add(this.lbl_AIOChannel_34);
      this.pnl_34To50.Controls.Add(this.lbl_AIOValue_34);
      this.pnl_34To50.Controls.Add(this.lbl_AIOType_34);
      this.pnl_34To50.Controls.Add(this.lbl_AIOName_34);
      this.pnl_34To50.Location = new System.Drawing.Point(142, 214);
      this.pnl_34To50.Name = "pnl_34To50";
      this.pnl_34To50.Size = new System.Drawing.Size(879, 512);
      this.pnl_34To50.TabIndex = 273;
      this.pnl_34To50.Visible = false;
      // 
      // lbl_AIOChannel_50
      // 
      this.lbl_AIOChannel_50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_50.Location = new System.Drawing.Point(6, 471);
      this.lbl_AIOChannel_50.Name = "lbl_AIOChannel_50";
      this.lbl_AIOChannel_50.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_50.TabIndex = 331;
      this.lbl_AIOChannel_50.Text = "0050";
      this.lbl_AIOChannel_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_50
      // 
      this.lbl_AIOValue_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_50.Location = new System.Drawing.Point(726, 471);
      this.lbl_AIOValue_50.Name = "lbl_AIOValue_50";
      this.lbl_AIOValue_50.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_50.TabIndex = 334;
      this.lbl_AIOValue_50.Text = "00.0000";
      this.lbl_AIOValue_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_50
      // 
      this.lbl_AIOName_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_50.Location = new System.Drawing.Point(75, 471);
      this.lbl_AIOName_50.Name = "lbl_AIOName_50";
      this.lbl_AIOName_50.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_50.TabIndex = 332;
      this.lbl_AIOName_50.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_50
      // 
      this.lbl_AIOType_50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_50.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_50.Location = new System.Drawing.Point(470, 471);
      this.lbl_AIOType_50.Name = "lbl_AIOType_50";
      this.lbl_AIOType_50.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_50.TabIndex = 333;
      this.lbl_AIOType_50.Text = "DSP400_DAC";
      this.lbl_AIOType_50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_49
      // 
      this.lbl_AIOChannel_49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_49.Location = new System.Drawing.Point(6, 442);
      this.lbl_AIOChannel_49.Name = "lbl_AIOChannel_49";
      this.lbl_AIOChannel_49.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_49.TabIndex = 327;
      this.lbl_AIOChannel_49.Text = "0049";
      this.lbl_AIOChannel_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_49
      // 
      this.lbl_AIOValue_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_49.Location = new System.Drawing.Point(726, 442);
      this.lbl_AIOValue_49.Name = "lbl_AIOValue_49";
      this.lbl_AIOValue_49.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_49.TabIndex = 330;
      this.lbl_AIOValue_49.Text = "00.0000";
      this.lbl_AIOValue_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_49
      // 
      this.lbl_AIOName_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_49.Location = new System.Drawing.Point(75, 442);
      this.lbl_AIOName_49.Name = "lbl_AIOName_49";
      this.lbl_AIOName_49.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_49.TabIndex = 328;
      this.lbl_AIOName_49.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_49
      // 
      this.lbl_AIOType_49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_49.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_49.Location = new System.Drawing.Point(470, 442);
      this.lbl_AIOType_49.Name = "lbl_AIOType_49";
      this.lbl_AIOType_49.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_49.TabIndex = 329;
      this.lbl_AIOType_49.Text = "DSP400_DAC";
      this.lbl_AIOType_49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_48
      // 
      this.lbl_AIOChannel_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_48.Location = new System.Drawing.Point(6, 413);
      this.lbl_AIOChannel_48.Name = "lbl_AIOChannel_48";
      this.lbl_AIOChannel_48.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_48.TabIndex = 323;
      this.lbl_AIOChannel_48.Text = "0048";
      this.lbl_AIOChannel_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_48
      // 
      this.lbl_AIOValue_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_48.Location = new System.Drawing.Point(726, 413);
      this.lbl_AIOValue_48.Name = "lbl_AIOValue_48";
      this.lbl_AIOValue_48.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_48.TabIndex = 326;
      this.lbl_AIOValue_48.Text = "00.0000";
      this.lbl_AIOValue_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_48
      // 
      this.lbl_AIOName_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_48.Location = new System.Drawing.Point(75, 413);
      this.lbl_AIOName_48.Name = "lbl_AIOName_48";
      this.lbl_AIOName_48.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_48.TabIndex = 324;
      this.lbl_AIOName_48.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_48
      // 
      this.lbl_AIOType_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_48.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_48.Location = new System.Drawing.Point(470, 413);
      this.lbl_AIOType_48.Name = "lbl_AIOType_48";
      this.lbl_AIOType_48.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_48.TabIndex = 325;
      this.lbl_AIOType_48.Text = "DSP400_DAC";
      this.lbl_AIOType_48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_47
      // 
      this.lbl_AIOChannel_47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_47.Location = new System.Drawing.Point(6, 384);
      this.lbl_AIOChannel_47.Name = "lbl_AIOChannel_47";
      this.lbl_AIOChannel_47.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_47.TabIndex = 319;
      this.lbl_AIOChannel_47.Text = "0047";
      this.lbl_AIOChannel_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_47
      // 
      this.lbl_AIOValue_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_47.Location = new System.Drawing.Point(726, 384);
      this.lbl_AIOValue_47.Name = "lbl_AIOValue_47";
      this.lbl_AIOValue_47.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_47.TabIndex = 322;
      this.lbl_AIOValue_47.Text = "00.0000";
      this.lbl_AIOValue_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_47
      // 
      this.lbl_AIOName_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_47.Location = new System.Drawing.Point(75, 384);
      this.lbl_AIOName_47.Name = "lbl_AIOName_47";
      this.lbl_AIOName_47.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_47.TabIndex = 320;
      this.lbl_AIOName_47.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_47
      // 
      this.lbl_AIOType_47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_47.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_47.Location = new System.Drawing.Point(470, 384);
      this.lbl_AIOType_47.Name = "lbl_AIOType_47";
      this.lbl_AIOType_47.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_47.TabIndex = 321;
      this.lbl_AIOType_47.Text = "DSP400_DAC";
      this.lbl_AIOType_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_46
      // 
      this.lbl_AIOChannel_46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_46.Location = new System.Drawing.Point(6, 355);
      this.lbl_AIOChannel_46.Name = "lbl_AIOChannel_46";
      this.lbl_AIOChannel_46.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_46.TabIndex = 315;
      this.lbl_AIOChannel_46.Text = "0046";
      this.lbl_AIOChannel_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_46
      // 
      this.lbl_AIOValue_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_46.Location = new System.Drawing.Point(726, 355);
      this.lbl_AIOValue_46.Name = "lbl_AIOValue_46";
      this.lbl_AIOValue_46.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_46.TabIndex = 318;
      this.lbl_AIOValue_46.Text = "00.0000";
      this.lbl_AIOValue_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_46
      // 
      this.lbl_AIOName_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_46.Location = new System.Drawing.Point(75, 355);
      this.lbl_AIOName_46.Name = "lbl_AIOName_46";
      this.lbl_AIOName_46.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_46.TabIndex = 316;
      this.lbl_AIOName_46.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_46
      // 
      this.lbl_AIOType_46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_46.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_46.Location = new System.Drawing.Point(470, 355);
      this.lbl_AIOType_46.Name = "lbl_AIOType_46";
      this.lbl_AIOType_46.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_46.TabIndex = 317;
      this.lbl_AIOType_46.Text = "DSP400_DAC";
      this.lbl_AIOType_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_45
      // 
      this.lbl_AIOChannel_45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_45.Location = new System.Drawing.Point(6, 326);
      this.lbl_AIOChannel_45.Name = "lbl_AIOChannel_45";
      this.lbl_AIOChannel_45.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_45.TabIndex = 311;
      this.lbl_AIOChannel_45.Text = "0045";
      this.lbl_AIOChannel_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_45
      // 
      this.lbl_AIOValue_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_45.Location = new System.Drawing.Point(726, 326);
      this.lbl_AIOValue_45.Name = "lbl_AIOValue_45";
      this.lbl_AIOValue_45.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_45.TabIndex = 314;
      this.lbl_AIOValue_45.Text = "00.0000";
      this.lbl_AIOValue_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_45
      // 
      this.lbl_AIOName_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_45.Location = new System.Drawing.Point(75, 326);
      this.lbl_AIOName_45.Name = "lbl_AIOName_45";
      this.lbl_AIOName_45.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_45.TabIndex = 312;
      this.lbl_AIOName_45.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_45
      // 
      this.lbl_AIOType_45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_45.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_45.Location = new System.Drawing.Point(470, 326);
      this.lbl_AIOType_45.Name = "lbl_AIOType_45";
      this.lbl_AIOType_45.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_45.TabIndex = 313;
      this.lbl_AIOType_45.Text = "DSP400_DAC";
      this.lbl_AIOType_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_44
      // 
      this.lbl_AIOChannel_44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_44.Location = new System.Drawing.Point(6, 297);
      this.lbl_AIOChannel_44.Name = "lbl_AIOChannel_44";
      this.lbl_AIOChannel_44.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_44.TabIndex = 307;
      this.lbl_AIOChannel_44.Text = "0044";
      this.lbl_AIOChannel_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_44
      // 
      this.lbl_AIOValue_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_44.Location = new System.Drawing.Point(726, 297);
      this.lbl_AIOValue_44.Name = "lbl_AIOValue_44";
      this.lbl_AIOValue_44.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_44.TabIndex = 310;
      this.lbl_AIOValue_44.Text = "00.0000";
      this.lbl_AIOValue_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_44
      // 
      this.lbl_AIOName_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_44.Location = new System.Drawing.Point(75, 297);
      this.lbl_AIOName_44.Name = "lbl_AIOName_44";
      this.lbl_AIOName_44.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_44.TabIndex = 308;
      this.lbl_AIOName_44.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_44
      // 
      this.lbl_AIOType_44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_44.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_44.Location = new System.Drawing.Point(470, 297);
      this.lbl_AIOType_44.Name = "lbl_AIOType_44";
      this.lbl_AIOType_44.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_44.TabIndex = 309;
      this.lbl_AIOType_44.Text = "DSP400_DAC";
      this.lbl_AIOType_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_43
      // 
      this.lbl_AIOChannel_43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_43.Location = new System.Drawing.Point(6, 268);
      this.lbl_AIOChannel_43.Name = "lbl_AIOChannel_43";
      this.lbl_AIOChannel_43.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_43.TabIndex = 303;
      this.lbl_AIOChannel_43.Text = "0043";
      this.lbl_AIOChannel_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_43
      // 
      this.lbl_AIOValue_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_43.Location = new System.Drawing.Point(726, 268);
      this.lbl_AIOValue_43.Name = "lbl_AIOValue_43";
      this.lbl_AIOValue_43.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_43.TabIndex = 306;
      this.lbl_AIOValue_43.Text = "00.0000";
      this.lbl_AIOValue_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_43
      // 
      this.lbl_AIOName_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_43.Location = new System.Drawing.Point(75, 268);
      this.lbl_AIOName_43.Name = "lbl_AIOName_43";
      this.lbl_AIOName_43.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_43.TabIndex = 304;
      this.lbl_AIOName_43.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_43
      // 
      this.lbl_AIOType_43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_43.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_43.Location = new System.Drawing.Point(470, 268);
      this.lbl_AIOType_43.Name = "lbl_AIOType_43";
      this.lbl_AIOType_43.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_43.TabIndex = 305;
      this.lbl_AIOType_43.Text = "DSP400_DAC";
      this.lbl_AIOType_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_42
      // 
      this.lbl_AIOChannel_42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_42.Location = new System.Drawing.Point(6, 239);
      this.lbl_AIOChannel_42.Name = "lbl_AIOChannel_42";
      this.lbl_AIOChannel_42.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_42.TabIndex = 299;
      this.lbl_AIOChannel_42.Text = "0042";
      this.lbl_AIOChannel_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_42
      // 
      this.lbl_AIOValue_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_42.Location = new System.Drawing.Point(726, 239);
      this.lbl_AIOValue_42.Name = "lbl_AIOValue_42";
      this.lbl_AIOValue_42.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_42.TabIndex = 302;
      this.lbl_AIOValue_42.Text = "00.0000";
      this.lbl_AIOValue_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_42
      // 
      this.lbl_AIOName_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_42.Location = new System.Drawing.Point(75, 239);
      this.lbl_AIOName_42.Name = "lbl_AIOName_42";
      this.lbl_AIOName_42.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_42.TabIndex = 300;
      this.lbl_AIOName_42.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_42
      // 
      this.lbl_AIOType_42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_42.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_42.Location = new System.Drawing.Point(470, 239);
      this.lbl_AIOType_42.Name = "lbl_AIOType_42";
      this.lbl_AIOType_42.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_42.TabIndex = 301;
      this.lbl_AIOType_42.Text = "DSP400_DAC";
      this.lbl_AIOType_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_38
      // 
      this.lbl_AIOChannel_38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_38.Location = new System.Drawing.Point(6, 123);
      this.lbl_AIOChannel_38.Name = "lbl_AIOChannel_38";
      this.lbl_AIOChannel_38.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_38.TabIndex = 295;
      this.lbl_AIOChannel_38.Text = "0038";
      this.lbl_AIOChannel_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_38
      // 
      this.lbl_AIOValue_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_38.Location = new System.Drawing.Point(726, 123);
      this.lbl_AIOValue_38.Name = "lbl_AIOValue_38";
      this.lbl_AIOValue_38.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_38.TabIndex = 298;
      this.lbl_AIOValue_38.Text = "00.0000";
      this.lbl_AIOValue_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_38
      // 
      this.lbl_AIOName_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_38.Location = new System.Drawing.Point(75, 123);
      this.lbl_AIOName_38.Name = "lbl_AIOName_38";
      this.lbl_AIOName_38.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_38.TabIndex = 296;
      this.lbl_AIOName_38.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_38
      // 
      this.lbl_AIOType_38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_38.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_38.Location = new System.Drawing.Point(470, 123);
      this.lbl_AIOType_38.Name = "lbl_AIOType_38";
      this.lbl_AIOType_38.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_38.TabIndex = 297;
      this.lbl_AIOType_38.Text = "DSP400_DAC";
      this.lbl_AIOType_38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_39
      // 
      this.lbl_AIOChannel_39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_39.Location = new System.Drawing.Point(6, 152);
      this.lbl_AIOChannel_39.Name = "lbl_AIOChannel_39";
      this.lbl_AIOChannel_39.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_39.TabIndex = 291;
      this.lbl_AIOChannel_39.Text = "0039";
      this.lbl_AIOChannel_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_39
      // 
      this.lbl_AIOValue_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_39.Location = new System.Drawing.Point(726, 152);
      this.lbl_AIOValue_39.Name = "lbl_AIOValue_39";
      this.lbl_AIOValue_39.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_39.TabIndex = 294;
      this.lbl_AIOValue_39.Text = "00.0000";
      this.lbl_AIOValue_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_39
      // 
      this.lbl_AIOName_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_39.Location = new System.Drawing.Point(75, 152);
      this.lbl_AIOName_39.Name = "lbl_AIOName_39";
      this.lbl_AIOName_39.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_39.TabIndex = 292;
      this.lbl_AIOName_39.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_39
      // 
      this.lbl_AIOType_39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_39.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_39.Location = new System.Drawing.Point(470, 152);
      this.lbl_AIOType_39.Name = "lbl_AIOType_39";
      this.lbl_AIOType_39.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_39.TabIndex = 293;
      this.lbl_AIOType_39.Text = "DSP400_DAC";
      this.lbl_AIOType_39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_40
      // 
      this.lbl_AIOChannel_40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_40.Location = new System.Drawing.Point(6, 181);
      this.lbl_AIOChannel_40.Name = "lbl_AIOChannel_40";
      this.lbl_AIOChannel_40.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_40.TabIndex = 287;
      this.lbl_AIOChannel_40.Text = "0040";
      this.lbl_AIOChannel_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_40
      // 
      this.lbl_AIOValue_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_40.Location = new System.Drawing.Point(726, 181);
      this.lbl_AIOValue_40.Name = "lbl_AIOValue_40";
      this.lbl_AIOValue_40.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_40.TabIndex = 290;
      this.lbl_AIOValue_40.Text = "00.0000";
      this.lbl_AIOValue_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_40
      // 
      this.lbl_AIOName_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_40.Location = new System.Drawing.Point(75, 181);
      this.lbl_AIOName_40.Name = "lbl_AIOName_40";
      this.lbl_AIOName_40.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_40.TabIndex = 288;
      this.lbl_AIOName_40.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_40
      // 
      this.lbl_AIOType_40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_40.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_40.Location = new System.Drawing.Point(470, 181);
      this.lbl_AIOType_40.Name = "lbl_AIOType_40";
      this.lbl_AIOType_40.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_40.TabIndex = 289;
      this.lbl_AIOType_40.Text = "DSP400_DAC";
      this.lbl_AIOType_40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_41
      // 
      this.lbl_AIOChannel_41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_41.Location = new System.Drawing.Point(6, 210);
      this.lbl_AIOChannel_41.Name = "lbl_AIOChannel_41";
      this.lbl_AIOChannel_41.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_41.TabIndex = 283;
      this.lbl_AIOChannel_41.Text = "0041";
      this.lbl_AIOChannel_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_41
      // 
      this.lbl_AIOValue_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_41.Location = new System.Drawing.Point(726, 210);
      this.lbl_AIOValue_41.Name = "lbl_AIOValue_41";
      this.lbl_AIOValue_41.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_41.TabIndex = 286;
      this.lbl_AIOValue_41.Text = "00.0000";
      this.lbl_AIOValue_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_41
      // 
      this.lbl_AIOName_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_41.Location = new System.Drawing.Point(75, 210);
      this.lbl_AIOName_41.Name = "lbl_AIOName_41";
      this.lbl_AIOName_41.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_41.TabIndex = 284;
      this.lbl_AIOName_41.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_41
      // 
      this.lbl_AIOType_41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_41.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_41.Location = new System.Drawing.Point(470, 210);
      this.lbl_AIOType_41.Name = "lbl_AIOType_41";
      this.lbl_AIOType_41.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_41.TabIndex = 285;
      this.lbl_AIOType_41.Text = "DSP400_DAC";
      this.lbl_AIOType_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_36
      // 
      this.lbl_AIOChannel_36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_36.Location = new System.Drawing.Point(6, 65);
      this.lbl_AIOChannel_36.Name = "lbl_AIOChannel_36";
      this.lbl_AIOChannel_36.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_36.TabIndex = 279;
      this.lbl_AIOChannel_36.Text = "0036";
      this.lbl_AIOChannel_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_36
      // 
      this.lbl_AIOValue_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_36.Location = new System.Drawing.Point(726, 65);
      this.lbl_AIOValue_36.Name = "lbl_AIOValue_36";
      this.lbl_AIOValue_36.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_36.TabIndex = 282;
      this.lbl_AIOValue_36.Text = "00.0000";
      this.lbl_AIOValue_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_36
      // 
      this.lbl_AIOName_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_36.Location = new System.Drawing.Point(75, 65);
      this.lbl_AIOName_36.Name = "lbl_AIOName_36";
      this.lbl_AIOName_36.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_36.TabIndex = 280;
      this.lbl_AIOName_36.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_36
      // 
      this.lbl_AIOType_36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_36.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_36.Location = new System.Drawing.Point(470, 65);
      this.lbl_AIOType_36.Name = "lbl_AIOType_36";
      this.lbl_AIOType_36.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_36.TabIndex = 281;
      this.lbl_AIOType_36.Text = "DSP400_DAC";
      this.lbl_AIOType_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_37
      // 
      this.lbl_AIOChannel_37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_37.Location = new System.Drawing.Point(6, 94);
      this.lbl_AIOChannel_37.Name = "lbl_AIOChannel_37";
      this.lbl_AIOChannel_37.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_37.TabIndex = 275;
      this.lbl_AIOChannel_37.Text = "0037";
      this.lbl_AIOChannel_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_37
      // 
      this.lbl_AIOValue_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_37.Location = new System.Drawing.Point(726, 94);
      this.lbl_AIOValue_37.Name = "lbl_AIOValue_37";
      this.lbl_AIOValue_37.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_37.TabIndex = 278;
      this.lbl_AIOValue_37.Text = "00.0000";
      this.lbl_AIOValue_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_37
      // 
      this.lbl_AIOName_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_37.Location = new System.Drawing.Point(75, 94);
      this.lbl_AIOName_37.Name = "lbl_AIOName_37";
      this.lbl_AIOName_37.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_37.TabIndex = 276;
      this.lbl_AIOName_37.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_37
      // 
      this.lbl_AIOType_37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_37.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_37.Location = new System.Drawing.Point(470, 94);
      this.lbl_AIOType_37.Name = "lbl_AIOType_37";
      this.lbl_AIOType_37.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_37.TabIndex = 277;
      this.lbl_AIOType_37.Text = "DSP400_DAC";
      this.lbl_AIOType_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_35
      // 
      this.lbl_AIOChannel_35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_35.Location = new System.Drawing.Point(6, 36);
      this.lbl_AIOChannel_35.Name = "lbl_AIOChannel_35";
      this.lbl_AIOChannel_35.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_35.TabIndex = 271;
      this.lbl_AIOChannel_35.Text = "0035";
      this.lbl_AIOChannel_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_35
      // 
      this.lbl_AIOValue_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_35.Location = new System.Drawing.Point(726, 36);
      this.lbl_AIOValue_35.Name = "lbl_AIOValue_35";
      this.lbl_AIOValue_35.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_35.TabIndex = 274;
      this.lbl_AIOValue_35.Text = "00.0000";
      this.lbl_AIOValue_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_35
      // 
      this.lbl_AIOName_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_35.Location = new System.Drawing.Point(75, 36);
      this.lbl_AIOName_35.Name = "lbl_AIOName_35";
      this.lbl_AIOName_35.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_35.TabIndex = 272;
      this.lbl_AIOName_35.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_35
      // 
      this.lbl_AIOType_35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_35.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_35.Location = new System.Drawing.Point(470, 36);
      this.lbl_AIOType_35.Name = "lbl_AIOType_35";
      this.lbl_AIOType_35.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_35.TabIndex = 273;
      this.lbl_AIOType_35.Text = "DSP400_DAC";
      this.lbl_AIOType_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_34
      // 
      this.lbl_AIOChannel_34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_34.Location = new System.Drawing.Point(6, 7);
      this.lbl_AIOChannel_34.Name = "lbl_AIOChannel_34";
      this.lbl_AIOChannel_34.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_34.TabIndex = 267;
      this.lbl_AIOChannel_34.Text = "0034";
      this.lbl_AIOChannel_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_34
      // 
      this.lbl_AIOValue_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_34.Location = new System.Drawing.Point(726, 7);
      this.lbl_AIOValue_34.Name = "lbl_AIOValue_34";
      this.lbl_AIOValue_34.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_34.TabIndex = 270;
      this.lbl_AIOValue_34.Text = "00.0000";
      this.lbl_AIOValue_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_34
      // 
      this.lbl_AIOType_34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_34.Location = new System.Drawing.Point(470, 7);
      this.lbl_AIOType_34.Name = "lbl_AIOType_34";
      this.lbl_AIOType_34.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_34.TabIndex = 269;
      this.lbl_AIOType_34.Text = "DSP400_DAC";
      this.lbl_AIOType_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_34
      // 
      this.lbl_AIOName_34.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_34.Location = new System.Drawing.Point(75, 7);
      this.lbl_AIOName_34.Name = "lbl_AIOName_34";
      this.lbl_AIOName_34.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_34.TabIndex = 268;
      this.lbl_AIOName_34.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnl_51To67
      // 
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_67);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_67);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_67);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_67);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_66);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_66);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_66);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_66);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_65);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_65);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_65);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_65);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_64);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_64);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_64);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_64);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_63);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_63);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_63);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_63);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_62);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_62);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_62);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_62);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_61);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_61);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_61);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_61);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_60);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_60);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_60);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_60);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_59);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_59);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_59);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_59);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_55);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_55);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_55);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_55);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_56);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_56);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_56);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_56);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_57);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_57);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_57);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_57);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_58);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_58);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_58);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_58);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_53);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_53);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_53);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_53);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_54);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_54);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_54);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_54);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_52);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_52);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_52);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_52);
      this.pnl_51To67.Controls.Add(this.lbl_AIOChannel_51);
      this.pnl_51To67.Controls.Add(this.lbl_AIOValue_51);
      this.pnl_51To67.Controls.Add(this.lbl_AIOType_51);
      this.pnl_51To67.Controls.Add(this.lbl_AIOName_51);
      this.pnl_51To67.Location = new System.Drawing.Point(142, 214);
      this.pnl_51To67.Name = "pnl_51To67";
      this.pnl_51To67.Size = new System.Drawing.Size(879, 512);
      this.pnl_51To67.TabIndex = 336;
      this.pnl_51To67.Visible = false;
      // 
      // lbl_AIOChannel_67
      // 
      this.lbl_AIOChannel_67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_67.Location = new System.Drawing.Point(6, 471);
      this.lbl_AIOChannel_67.Name = "lbl_AIOChannel_67";
      this.lbl_AIOChannel_67.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_67.TabIndex = 331;
      this.lbl_AIOChannel_67.Text = "0067";
      this.lbl_AIOChannel_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_67
      // 
      this.lbl_AIOValue_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_67.Location = new System.Drawing.Point(726, 471);
      this.lbl_AIOValue_67.Name = "lbl_AIOValue_67";
      this.lbl_AIOValue_67.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_67.TabIndex = 334;
      this.lbl_AIOValue_67.Text = "00.0000";
      this.lbl_AIOValue_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_67
      // 
      this.lbl_AIOName_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_67.Location = new System.Drawing.Point(75, 471);
      this.lbl_AIOName_67.Name = "lbl_AIOName_67";
      this.lbl_AIOName_67.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_67.TabIndex = 332;
      this.lbl_AIOName_67.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_67
      // 
      this.lbl_AIOType_67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_67.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_67.Location = new System.Drawing.Point(470, 471);
      this.lbl_AIOType_67.Name = "lbl_AIOType_67";
      this.lbl_AIOType_67.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_67.TabIndex = 333;
      this.lbl_AIOType_67.Text = "DSP400_DAC";
      this.lbl_AIOType_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_66
      // 
      this.lbl_AIOChannel_66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_66.Location = new System.Drawing.Point(6, 442);
      this.lbl_AIOChannel_66.Name = "lbl_AIOChannel_66";
      this.lbl_AIOChannel_66.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_66.TabIndex = 327;
      this.lbl_AIOChannel_66.Text = "0066";
      this.lbl_AIOChannel_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_66
      // 
      this.lbl_AIOValue_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_66.Location = new System.Drawing.Point(726, 442);
      this.lbl_AIOValue_66.Name = "lbl_AIOValue_66";
      this.lbl_AIOValue_66.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_66.TabIndex = 330;
      this.lbl_AIOValue_66.Text = "00.0000";
      this.lbl_AIOValue_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_66
      // 
      this.lbl_AIOName_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_66.Location = new System.Drawing.Point(75, 442);
      this.lbl_AIOName_66.Name = "lbl_AIOName_66";
      this.lbl_AIOName_66.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_66.TabIndex = 328;
      this.lbl_AIOName_66.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_66
      // 
      this.lbl_AIOType_66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_66.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_66.Location = new System.Drawing.Point(470, 442);
      this.lbl_AIOType_66.Name = "lbl_AIOType_66";
      this.lbl_AIOType_66.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_66.TabIndex = 329;
      this.lbl_AIOType_66.Text = "DSP400_DAC";
      this.lbl_AIOType_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_65
      // 
      this.lbl_AIOChannel_65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_65.Location = new System.Drawing.Point(6, 413);
      this.lbl_AIOChannel_65.Name = "lbl_AIOChannel_65";
      this.lbl_AIOChannel_65.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_65.TabIndex = 323;
      this.lbl_AIOChannel_65.Text = "0065";
      this.lbl_AIOChannel_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_65
      // 
      this.lbl_AIOValue_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_65.Location = new System.Drawing.Point(726, 413);
      this.lbl_AIOValue_65.Name = "lbl_AIOValue_65";
      this.lbl_AIOValue_65.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_65.TabIndex = 326;
      this.lbl_AIOValue_65.Text = "00.0000";
      this.lbl_AIOValue_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_65
      // 
      this.lbl_AIOName_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_65.Location = new System.Drawing.Point(75, 413);
      this.lbl_AIOName_65.Name = "lbl_AIOName_65";
      this.lbl_AIOName_65.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_65.TabIndex = 324;
      this.lbl_AIOName_65.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_65
      // 
      this.lbl_AIOType_65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_65.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_65.Location = new System.Drawing.Point(470, 413);
      this.lbl_AIOType_65.Name = "lbl_AIOType_65";
      this.lbl_AIOType_65.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_65.TabIndex = 325;
      this.lbl_AIOType_65.Text = "DSP400_DAC";
      this.lbl_AIOType_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_64
      // 
      this.lbl_AIOChannel_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_64.Location = new System.Drawing.Point(6, 384);
      this.lbl_AIOChannel_64.Name = "lbl_AIOChannel_64";
      this.lbl_AIOChannel_64.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_64.TabIndex = 319;
      this.lbl_AIOChannel_64.Text = "0064";
      this.lbl_AIOChannel_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_64
      // 
      this.lbl_AIOValue_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_64.Location = new System.Drawing.Point(726, 384);
      this.lbl_AIOValue_64.Name = "lbl_AIOValue_64";
      this.lbl_AIOValue_64.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_64.TabIndex = 322;
      this.lbl_AIOValue_64.Text = "00.0000";
      this.lbl_AIOValue_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_64
      // 
      this.lbl_AIOName_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_64.Location = new System.Drawing.Point(75, 384);
      this.lbl_AIOName_64.Name = "lbl_AIOName_64";
      this.lbl_AIOName_64.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_64.TabIndex = 320;
      this.lbl_AIOName_64.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_64
      // 
      this.lbl_AIOType_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_64.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_64.Location = new System.Drawing.Point(470, 384);
      this.lbl_AIOType_64.Name = "lbl_AIOType_64";
      this.lbl_AIOType_64.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_64.TabIndex = 321;
      this.lbl_AIOType_64.Text = "DSP400_DAC";
      this.lbl_AIOType_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_63
      // 
      this.lbl_AIOChannel_63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_63.Location = new System.Drawing.Point(6, 355);
      this.lbl_AIOChannel_63.Name = "lbl_AIOChannel_63";
      this.lbl_AIOChannel_63.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_63.TabIndex = 315;
      this.lbl_AIOChannel_63.Text = "0063";
      this.lbl_AIOChannel_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_63
      // 
      this.lbl_AIOValue_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_63.Location = new System.Drawing.Point(726, 355);
      this.lbl_AIOValue_63.Name = "lbl_AIOValue_63";
      this.lbl_AIOValue_63.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_63.TabIndex = 318;
      this.lbl_AIOValue_63.Text = "00.0000";
      this.lbl_AIOValue_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_63
      // 
      this.lbl_AIOName_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_63.Location = new System.Drawing.Point(75, 355);
      this.lbl_AIOName_63.Name = "lbl_AIOName_63";
      this.lbl_AIOName_63.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_63.TabIndex = 316;
      this.lbl_AIOName_63.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_63
      // 
      this.lbl_AIOType_63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_63.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_63.Location = new System.Drawing.Point(470, 355);
      this.lbl_AIOType_63.Name = "lbl_AIOType_63";
      this.lbl_AIOType_63.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_63.TabIndex = 317;
      this.lbl_AIOType_63.Text = "DSP400_DAC";
      this.lbl_AIOType_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_62
      // 
      this.lbl_AIOChannel_62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_62.Location = new System.Drawing.Point(6, 326);
      this.lbl_AIOChannel_62.Name = "lbl_AIOChannel_62";
      this.lbl_AIOChannel_62.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_62.TabIndex = 311;
      this.lbl_AIOChannel_62.Text = "0062";
      this.lbl_AIOChannel_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_62
      // 
      this.lbl_AIOValue_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_62.Location = new System.Drawing.Point(726, 326);
      this.lbl_AIOValue_62.Name = "lbl_AIOValue_62";
      this.lbl_AIOValue_62.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_62.TabIndex = 314;
      this.lbl_AIOValue_62.Text = "00.0000";
      this.lbl_AIOValue_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_62
      // 
      this.lbl_AIOName_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_62.Location = new System.Drawing.Point(75, 326);
      this.lbl_AIOName_62.Name = "lbl_AIOName_62";
      this.lbl_AIOName_62.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_62.TabIndex = 312;
      this.lbl_AIOName_62.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_62
      // 
      this.lbl_AIOType_62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_62.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_62.Location = new System.Drawing.Point(470, 326);
      this.lbl_AIOType_62.Name = "lbl_AIOType_62";
      this.lbl_AIOType_62.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_62.TabIndex = 313;
      this.lbl_AIOType_62.Text = "DSP400_DAC";
      this.lbl_AIOType_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_61
      // 
      this.lbl_AIOChannel_61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_61.Location = new System.Drawing.Point(6, 297);
      this.lbl_AIOChannel_61.Name = "lbl_AIOChannel_61";
      this.lbl_AIOChannel_61.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_61.TabIndex = 307;
      this.lbl_AIOChannel_61.Text = "0061";
      this.lbl_AIOChannel_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_61
      // 
      this.lbl_AIOValue_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_61.Location = new System.Drawing.Point(726, 297);
      this.lbl_AIOValue_61.Name = "lbl_AIOValue_61";
      this.lbl_AIOValue_61.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_61.TabIndex = 310;
      this.lbl_AIOValue_61.Text = "00.0000";
      this.lbl_AIOValue_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_61
      // 
      this.lbl_AIOName_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_61.Location = new System.Drawing.Point(75, 297);
      this.lbl_AIOName_61.Name = "lbl_AIOName_61";
      this.lbl_AIOName_61.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_61.TabIndex = 308;
      this.lbl_AIOName_61.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_61
      // 
      this.lbl_AIOType_61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_61.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_61.Location = new System.Drawing.Point(470, 297);
      this.lbl_AIOType_61.Name = "lbl_AIOType_61";
      this.lbl_AIOType_61.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_61.TabIndex = 309;
      this.lbl_AIOType_61.Text = "DSP400_DAC";
      this.lbl_AIOType_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_60
      // 
      this.lbl_AIOChannel_60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_60.Location = new System.Drawing.Point(6, 268);
      this.lbl_AIOChannel_60.Name = "lbl_AIOChannel_60";
      this.lbl_AIOChannel_60.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_60.TabIndex = 303;
      this.lbl_AIOChannel_60.Text = "0060";
      this.lbl_AIOChannel_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_60
      // 
      this.lbl_AIOValue_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_60.Location = new System.Drawing.Point(726, 268);
      this.lbl_AIOValue_60.Name = "lbl_AIOValue_60";
      this.lbl_AIOValue_60.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_60.TabIndex = 306;
      this.lbl_AIOValue_60.Text = "00.0000";
      this.lbl_AIOValue_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_60
      // 
      this.lbl_AIOName_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_60.Location = new System.Drawing.Point(75, 268);
      this.lbl_AIOName_60.Name = "lbl_AIOName_60";
      this.lbl_AIOName_60.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_60.TabIndex = 304;
      this.lbl_AIOName_60.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_60
      // 
      this.lbl_AIOType_60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_60.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_60.Location = new System.Drawing.Point(470, 268);
      this.lbl_AIOType_60.Name = "lbl_AIOType_60";
      this.lbl_AIOType_60.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_60.TabIndex = 305;
      this.lbl_AIOType_60.Text = "DSP400_DAC";
      this.lbl_AIOType_60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_59
      // 
      this.lbl_AIOChannel_59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_59.Location = new System.Drawing.Point(6, 239);
      this.lbl_AIOChannel_59.Name = "lbl_AIOChannel_59";
      this.lbl_AIOChannel_59.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_59.TabIndex = 299;
      this.lbl_AIOChannel_59.Text = "0059";
      this.lbl_AIOChannel_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_59
      // 
      this.lbl_AIOValue_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_59.Location = new System.Drawing.Point(726, 239);
      this.lbl_AIOValue_59.Name = "lbl_AIOValue_59";
      this.lbl_AIOValue_59.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_59.TabIndex = 302;
      this.lbl_AIOValue_59.Text = "00.0000";
      this.lbl_AIOValue_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_59
      // 
      this.lbl_AIOName_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_59.Location = new System.Drawing.Point(75, 239);
      this.lbl_AIOName_59.Name = "lbl_AIOName_59";
      this.lbl_AIOName_59.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_59.TabIndex = 300;
      this.lbl_AIOName_59.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_59
      // 
      this.lbl_AIOType_59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_59.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_59.Location = new System.Drawing.Point(470, 239);
      this.lbl_AIOType_59.Name = "lbl_AIOType_59";
      this.lbl_AIOType_59.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_59.TabIndex = 301;
      this.lbl_AIOType_59.Text = "DSP400_DAC";
      this.lbl_AIOType_59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_55
      // 
      this.lbl_AIOChannel_55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_55.Location = new System.Drawing.Point(6, 123);
      this.lbl_AIOChannel_55.Name = "lbl_AIOChannel_55";
      this.lbl_AIOChannel_55.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_55.TabIndex = 295;
      this.lbl_AIOChannel_55.Text = "0055";
      this.lbl_AIOChannel_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_55
      // 
      this.lbl_AIOValue_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_55.Location = new System.Drawing.Point(726, 123);
      this.lbl_AIOValue_55.Name = "lbl_AIOValue_55";
      this.lbl_AIOValue_55.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_55.TabIndex = 298;
      this.lbl_AIOValue_55.Text = "00.0000";
      this.lbl_AIOValue_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_55
      // 
      this.lbl_AIOName_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_55.Location = new System.Drawing.Point(75, 123);
      this.lbl_AIOName_55.Name = "lbl_AIOName_55";
      this.lbl_AIOName_55.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_55.TabIndex = 296;
      this.lbl_AIOName_55.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_55
      // 
      this.lbl_AIOType_55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_55.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_55.Location = new System.Drawing.Point(470, 123);
      this.lbl_AIOType_55.Name = "lbl_AIOType_55";
      this.lbl_AIOType_55.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_55.TabIndex = 297;
      this.lbl_AIOType_55.Text = "DSP400_DAC";
      this.lbl_AIOType_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_56
      // 
      this.lbl_AIOChannel_56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_56.Location = new System.Drawing.Point(6, 152);
      this.lbl_AIOChannel_56.Name = "lbl_AIOChannel_56";
      this.lbl_AIOChannel_56.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_56.TabIndex = 291;
      this.lbl_AIOChannel_56.Text = "0056";
      this.lbl_AIOChannel_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_56
      // 
      this.lbl_AIOValue_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_56.Location = new System.Drawing.Point(726, 152);
      this.lbl_AIOValue_56.Name = "lbl_AIOValue_56";
      this.lbl_AIOValue_56.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_56.TabIndex = 294;
      this.lbl_AIOValue_56.Text = "00.0000";
      this.lbl_AIOValue_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_56
      // 
      this.lbl_AIOName_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_56.Location = new System.Drawing.Point(75, 152);
      this.lbl_AIOName_56.Name = "lbl_AIOName_56";
      this.lbl_AIOName_56.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_56.TabIndex = 292;
      this.lbl_AIOName_56.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_56
      // 
      this.lbl_AIOType_56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_56.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_56.Location = new System.Drawing.Point(470, 152);
      this.lbl_AIOType_56.Name = "lbl_AIOType_56";
      this.lbl_AIOType_56.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_56.TabIndex = 293;
      this.lbl_AIOType_56.Text = "DSP400_DAC";
      this.lbl_AIOType_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_57
      // 
      this.lbl_AIOChannel_57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_57.Location = new System.Drawing.Point(6, 181);
      this.lbl_AIOChannel_57.Name = "lbl_AIOChannel_57";
      this.lbl_AIOChannel_57.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_57.TabIndex = 287;
      this.lbl_AIOChannel_57.Text = "0057";
      this.lbl_AIOChannel_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_57
      // 
      this.lbl_AIOValue_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_57.Location = new System.Drawing.Point(726, 181);
      this.lbl_AIOValue_57.Name = "lbl_AIOValue_57";
      this.lbl_AIOValue_57.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_57.TabIndex = 290;
      this.lbl_AIOValue_57.Text = "00.0000";
      this.lbl_AIOValue_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_57
      // 
      this.lbl_AIOName_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_57.Location = new System.Drawing.Point(75, 181);
      this.lbl_AIOName_57.Name = "lbl_AIOName_57";
      this.lbl_AIOName_57.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_57.TabIndex = 288;
      this.lbl_AIOName_57.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_57
      // 
      this.lbl_AIOType_57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_57.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_57.Location = new System.Drawing.Point(470, 181);
      this.lbl_AIOType_57.Name = "lbl_AIOType_57";
      this.lbl_AIOType_57.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_57.TabIndex = 289;
      this.lbl_AIOType_57.Text = "DSP400_DAC";
      this.lbl_AIOType_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_58
      // 
      this.lbl_AIOChannel_58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_58.Location = new System.Drawing.Point(6, 210);
      this.lbl_AIOChannel_58.Name = "lbl_AIOChannel_58";
      this.lbl_AIOChannel_58.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_58.TabIndex = 283;
      this.lbl_AIOChannel_58.Text = "0058";
      this.lbl_AIOChannel_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_58
      // 
      this.lbl_AIOValue_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_58.Location = new System.Drawing.Point(726, 210);
      this.lbl_AIOValue_58.Name = "lbl_AIOValue_58";
      this.lbl_AIOValue_58.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_58.TabIndex = 286;
      this.lbl_AIOValue_58.Text = "00.0000";
      this.lbl_AIOValue_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_58
      // 
      this.lbl_AIOName_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_58.Location = new System.Drawing.Point(75, 210);
      this.lbl_AIOName_58.Name = "lbl_AIOName_58";
      this.lbl_AIOName_58.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_58.TabIndex = 284;
      this.lbl_AIOName_58.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_58
      // 
      this.lbl_AIOType_58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_58.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_58.Location = new System.Drawing.Point(470, 210);
      this.lbl_AIOType_58.Name = "lbl_AIOType_58";
      this.lbl_AIOType_58.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_58.TabIndex = 285;
      this.lbl_AIOType_58.Text = "DSP400_DAC";
      this.lbl_AIOType_58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_53
      // 
      this.lbl_AIOChannel_53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_53.Location = new System.Drawing.Point(6, 65);
      this.lbl_AIOChannel_53.Name = "lbl_AIOChannel_53";
      this.lbl_AIOChannel_53.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_53.TabIndex = 279;
      this.lbl_AIOChannel_53.Text = "0053";
      this.lbl_AIOChannel_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_53
      // 
      this.lbl_AIOValue_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_53.Location = new System.Drawing.Point(726, 65);
      this.lbl_AIOValue_53.Name = "lbl_AIOValue_53";
      this.lbl_AIOValue_53.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_53.TabIndex = 282;
      this.lbl_AIOValue_53.Text = "00.0000";
      this.lbl_AIOValue_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_53
      // 
      this.lbl_AIOName_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_53.Location = new System.Drawing.Point(75, 65);
      this.lbl_AIOName_53.Name = "lbl_AIOName_53";
      this.lbl_AIOName_53.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_53.TabIndex = 280;
      this.lbl_AIOName_53.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_53
      // 
      this.lbl_AIOType_53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_53.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_53.Location = new System.Drawing.Point(470, 65);
      this.lbl_AIOType_53.Name = "lbl_AIOType_53";
      this.lbl_AIOType_53.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_53.TabIndex = 281;
      this.lbl_AIOType_53.Text = "DSP400_DAC";
      this.lbl_AIOType_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_54
      // 
      this.lbl_AIOChannel_54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_54.Location = new System.Drawing.Point(6, 94);
      this.lbl_AIOChannel_54.Name = "lbl_AIOChannel_54";
      this.lbl_AIOChannel_54.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_54.TabIndex = 275;
      this.lbl_AIOChannel_54.Text = "0054";
      this.lbl_AIOChannel_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_54
      // 
      this.lbl_AIOValue_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_54.Location = new System.Drawing.Point(726, 94);
      this.lbl_AIOValue_54.Name = "lbl_AIOValue_54";
      this.lbl_AIOValue_54.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_54.TabIndex = 278;
      this.lbl_AIOValue_54.Text = "00.0000";
      this.lbl_AIOValue_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_54
      // 
      this.lbl_AIOName_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_54.Location = new System.Drawing.Point(75, 94);
      this.lbl_AIOName_54.Name = "lbl_AIOName_54";
      this.lbl_AIOName_54.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_54.TabIndex = 276;
      this.lbl_AIOName_54.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_54
      // 
      this.lbl_AIOType_54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_54.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_54.Location = new System.Drawing.Point(470, 94);
      this.lbl_AIOType_54.Name = "lbl_AIOType_54";
      this.lbl_AIOType_54.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_54.TabIndex = 277;
      this.lbl_AIOType_54.Text = "DSP400_DAC";
      this.lbl_AIOType_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_52
      // 
      this.lbl_AIOChannel_52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_52.Location = new System.Drawing.Point(6, 36);
      this.lbl_AIOChannel_52.Name = "lbl_AIOChannel_52";
      this.lbl_AIOChannel_52.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_52.TabIndex = 271;
      this.lbl_AIOChannel_52.Text = "0052";
      this.lbl_AIOChannel_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_52
      // 
      this.lbl_AIOValue_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_52.Location = new System.Drawing.Point(726, 36);
      this.lbl_AIOValue_52.Name = "lbl_AIOValue_52";
      this.lbl_AIOValue_52.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_52.TabIndex = 274;
      this.lbl_AIOValue_52.Text = "00.0000";
      this.lbl_AIOValue_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_52
      // 
      this.lbl_AIOName_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_52.Location = new System.Drawing.Point(75, 36);
      this.lbl_AIOName_52.Name = "lbl_AIOName_52";
      this.lbl_AIOName_52.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_52.TabIndex = 272;
      this.lbl_AIOName_52.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_52
      // 
      this.lbl_AIOType_52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_52.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_52.Location = new System.Drawing.Point(470, 36);
      this.lbl_AIOType_52.Name = "lbl_AIOType_52";
      this.lbl_AIOType_52.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_52.TabIndex = 273;
      this.lbl_AIOType_52.Text = "DSP400_DAC";
      this.lbl_AIOType_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOChannel_51
      // 
      this.lbl_AIOChannel_51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOChannel_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOChannel_51.Location = new System.Drawing.Point(6, 7);
      this.lbl_AIOChannel_51.Name = "lbl_AIOChannel_51";
      this.lbl_AIOChannel_51.Size = new System.Drawing.Size(55, 30);
      this.lbl_AIOChannel_51.TabIndex = 267;
      this.lbl_AIOChannel_51.Text = "0051";
      this.lbl_AIOChannel_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOValue_51
      // 
      this.lbl_AIOValue_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOValue_51.Location = new System.Drawing.Point(726, 7);
      this.lbl_AIOValue_51.Name = "lbl_AIOValue_51";
      this.lbl_AIOValue_51.Size = new System.Drawing.Size(81, 30);
      this.lbl_AIOValue_51.TabIndex = 270;
      this.lbl_AIOValue_51.Text = "00.0000";
      this.lbl_AIOValue_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOType_51
      // 
      this.lbl_AIOType_51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_AIOType_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOType_51.Location = new System.Drawing.Point(470, 7);
      this.lbl_AIOType_51.Name = "lbl_AIOType_51";
      this.lbl_AIOType_51.Size = new System.Drawing.Size(140, 30);
      this.lbl_AIOType_51.TabIndex = 269;
      this.lbl_AIOType_51.Text = "DSP400_DAC";
      this.lbl_AIOType_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AIOName_51
      // 
      this.lbl_AIOName_51.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AIOName_51.Location = new System.Drawing.Point(75, 7);
      this.lbl_AIOName_51.Name = "lbl_AIOName_51";
      this.lbl_AIOName_51.Size = new System.Drawing.Size(356, 30);
      this.lbl_AIOName_51.TabIndex = 268;
      this.lbl_AIOName_51.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_AIOName_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // Frm_AIOStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_Value);
      this.Controls.Add(this.ttl_Type);
      this.Controls.Add(this.ttl_Name);
      this.Controls.Add(this.ttl_Channel);
      this.Controls.Add(this.pnl_51To67);
      this.Controls.Add(this.pnl_34To50);
      this.Controls.Add(this.pnl_17To33);
      this.Controls.Add(this.pnl_00To16);
      this.Name = "Frm_AIOStatus";
      this.Text = "Frm_AIOStatus";
      this.Activated += new System.EventHandler(this.Frm_AIOStatus_Activated);
      this.Controls.SetChildIndex(this.pnl_00To16, 0);
      this.Controls.SetChildIndex(this.pnl_17To33, 0);
      this.Controls.SetChildIndex(this.pnl_34To50, 0);
      this.Controls.SetChildIndex(this.pnl_51To67, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.ttl_Channel, 0);
      this.Controls.SetChildIndex(this.ttl_Name, 0);
      this.Controls.SetChildIndex(this.ttl_Type, 0);
      this.Controls.SetChildIndex(this.ttl_Value, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_00To16.ResumeLayout(false);
      this.pnl_17To33.ResumeLayout(false);
      this.pnl_34To50.ResumeLayout(false);
      this.pnl_51To67.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion

		private void _setErrorAIOStatus(bool error)
		{

			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach (FieldInfo field in fields)
			{
				if (field.Name.StartsWith("lbl_AIO"))
				{
					try
					{
						Label l = (Label)field.GetValue(this);
						if (error)
							l.ForeColor = Color.Red;
						else
							l.ForeColor = Color.Black;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _clearAIOStatus()
		{
			this.F10();

			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach (FieldInfo field in fields)
			{
				if (field.Name.StartsWith("lbl_AIO"))
				{
					try
					{
						Label l = (Label)field.GetValue(this);
						l.Text = "";
						l.BackColor = Color.WhiteSmoke;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _clearAIOStatus(int num)
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
						l.BorderStyle = BorderStyle.None;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _setAIOStatus(int num, int channel, string name, string typeAIO, string value)
		{
			//channel
			Type type = this.GetType();
			FieldInfo field = type.GetField("lbl_AIOChannel_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = channel.ToString();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
				}
			}

			//name
			type = this.GetType();
			field = type.GetField("lbl_AIOName_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = name;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
				}
			}

			//typeAIO
			type = this.GetType();
			field = type.GetField("lbl_AIOType_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = typeAIO;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
				}
			}

			//Status
			this._setAIOStatus(num, value);
		}

		private void _setAIOStatus(int num, string value)
		{
			//Value
			Type type = this.GetType();
			FieldInfo field = type.GetField("lbl_AIOValue_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = value;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setAIOStatus - Reflection failed", ex);
				}
			}
		}

	}
}

