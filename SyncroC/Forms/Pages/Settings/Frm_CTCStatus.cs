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
	public class Frm_CTCStatus : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
	{
		private System.ComponentModel.IContainer components = null;
		private Label lbl_CTCType_0;
		private Label lbl_CTCCtr_0;
		private Label lbl_CTCName_0;
		private Label lbl_CTCSource_0;
		private Label lbl_CTCConfig_0;
		private Label lbl_CTCValue_0;
		private Label lbl_CTCCount_0;
		private Label lbl_CTCInc_0;
		private Label lbl_CTCSignal_0;
		private Label lbl_CTCSignal_3;
		private Label lbl_CTCInc_3;
		private Label lbl_CTCCount_3;
		private Label lbl_CTCValue_3;
		private Label lbl_CTCConfig_3;
		private Label lbl_CTCSource_3;
		private Label lbl_CTCType_3;
		private Label lbl_CTCCtr_3;
		private Label lbl_CTCName_3;
		private Label lbl_CTCSignal_1;
		private Label lbl_CTCInc_1;
		private Label lbl_CTCCount_1;
		private Label lbl_CTCValue_1;
		private Label lbl_CTCConfig_1;
		private Label lbl_CTCSource_1;
		private Label lbl_CTCType_1;
		private Label lbl_CTCCtr_1;
		private Label lbl_CTCName_1;
		private Label lbl_CTCSignal_2;
		private Label lbl_CTCInc_2;
		private Label lbl_CTCCount_2;
		private Label lbl_CTCValue_2;
		private Label lbl_CTCConfig_2;
		private Label lbl_CTCSource_2;
		private Label lbl_CTCType_2;
		private Label lbl_CTCCtr_2;
		private Label lbl_CTCName_2;
		private Label lbl_CTCSignal_6;
		private Label lbl_CTCInc_6;
		private Label lbl_CTCCount_6;
		private Label lbl_CTCValue_6;
		private Label lbl_CTCConfig_6;
		private Label lbl_CTCSource_6;
		private Label lbl_CTCType_6;
		private Label lbl_CTCCtr_6;
		private Label lbl_CTCName_6;
		private Label lbl_CTCSignal_5;
		private Label lbl_CTCInc_5;
		private Label lbl_CTCCount_5;
		private Label lbl_CTCValue_5;
		private Label lbl_CTCConfig_5;
		private Label lbl_CTCSource_5;
		private Label lbl_CTCType_5;
		private Label lbl_CTCCtr_5;
		private Label lbl_CTCName_5;
		private Label lbl_CTCSignal_7;
		private Label lbl_CTCInc_7;
		private Label lbl_CTCCount_7;
		private Label lbl_CTCValue_7;
		private Label lbl_CTCConfig_7;
		private Label lbl_CTCSource_7;
		private Label lbl_CTCType_7;
		private Label lbl_CTCCtr_7;
		private Label lbl_CTCName_7;
		private Label lbl_CTCSignal_4;
		private Label lbl_CTCInc_4;
		private Label lbl_CTCCount_4;
		private Label lbl_CTCValue_4;
		private Label lbl_CTCConfig_4;
		private Label lbl_CTCSource_4;
		private Label lbl_CTCType_4;
		private Label lbl_CTCCtr_4;
		private Label lbl_CTCName_4;
		private Label lbl_CTCSignal_10;
		private Label lbl_CTCInc_10;
		private Label lbl_CTCCount_10;
		private Label lbl_CTCValue_10;
		private Label lbl_CTCConfig_10;
		private Label lbl_CTCSource_10;
		private Label lbl_CTCType_10;
		private Label lbl_CTCCtr_10;
		private Label lbl_CTCName_10;
		private Label lbl_CTCSignal_9;
		private Label lbl_CTCInc_9;
		private Label lbl_CTCCount_9;
		private Label lbl_CTCValue_9;
		private Label lbl_CTCConfig_9;
		private Label lbl_CTCSource_9;
		private Label lbl_CTCType_9;
		private Label lbl_CTCCtr_9;
		private Label lbl_CTCName_9;
		private Label lbl_CTCSignal_11;
		private Label lbl_CTCInc_11;
		private Label lbl_CTCCount_11;
		private Label lbl_CTCValue_11;
		private Label lbl_CTCConfig_11;
		private Label lbl_CTCSource_11;
		private Label lbl_CTCType_11;
		private Label lbl_CTCCtr_11;
		private Label lbl_CTCName_11;
		private Label lbl_CTCSignal_8;
		private Label lbl_CTCInc_8;
		private Label lbl_CTCCount_8;
		private Label lbl_CTCValue_8;
		private Label lbl_CTCConfig_8;
		private Label lbl_CTCSource_8;
		private Label lbl_CTCType_8;
		private Label lbl_CTCCtr_8;
		private Label lbl_CTCName_8;
		private SyncroC.Controls.Title ttl_Ctr;
		private SyncroC.Controls.Title ttl_Name;
		private SyncroC.Controls.Title ttl_Type;
		private SyncroC.Controls.Title ttl_Source;
		private SyncroC.Controls.Title ttl_Config;
		private SyncroC.Controls.Title ttl_Value;
		private SyncroC.Controls.Title ttl_Count;
		private SyncroC.Controls.Title ttl_Incr;
		private SyncroC.Controls.Title ttl_Signal;

    private int lastValue = 0;

		public Frm_CTCStatus()
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

			this._clearCTCStatus();
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

    private void Frm_CTCStatus_Activated(object sender, EventArgs e)
    {
      this.updateControlsEnable();
    }

		protected override void F6()
		{
		}

		protected override void CtrlF1()
		{
			Frm_AskValue askValue = new Frm_AskValue();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_26");
			FosberDialogResult ret = askValue.ShowDialog(this, text, PageManager.getPageManager().pagesHistorian.CurrentDSPModule);

			if (ret.Dialog == DialogResult.OK)
			{
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule = (int)ret.Data[0];
				SendCTCModule();
			}
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
    //  else
    //  {
    //    PageManager.getPageManager()._SetMessage("§SYCC_11");
    //  }
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CTCStatus));
      this.lbl_CTCType_0 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_0 = new System.Windows.Forms.Label();
      this.lbl_CTCName_0 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_0 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_0 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_0 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_0 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_0 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_0 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_3 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_3 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_3 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_3 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_3 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_3 = new System.Windows.Forms.Label();
      this.lbl_CTCType_3 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_3 = new System.Windows.Forms.Label();
      this.lbl_CTCName_3 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_1 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_1 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_1 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_1 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_1 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_1 = new System.Windows.Forms.Label();
      this.lbl_CTCType_1 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_1 = new System.Windows.Forms.Label();
      this.lbl_CTCName_1 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_2 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_2 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_2 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_2 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_2 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_2 = new System.Windows.Forms.Label();
      this.lbl_CTCType_2 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_2 = new System.Windows.Forms.Label();
      this.lbl_CTCName_2 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_6 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_6 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_6 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_6 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_6 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_6 = new System.Windows.Forms.Label();
      this.lbl_CTCType_6 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_6 = new System.Windows.Forms.Label();
      this.lbl_CTCName_6 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_5 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_5 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_5 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_5 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_5 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_5 = new System.Windows.Forms.Label();
      this.lbl_CTCType_5 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_5 = new System.Windows.Forms.Label();
      this.lbl_CTCName_5 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_7 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_7 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_7 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_7 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_7 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_7 = new System.Windows.Forms.Label();
      this.lbl_CTCType_7 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_7 = new System.Windows.Forms.Label();
      this.lbl_CTCName_7 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_4 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_4 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_4 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_4 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_4 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_4 = new System.Windows.Forms.Label();
      this.lbl_CTCType_4 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_4 = new System.Windows.Forms.Label();
      this.lbl_CTCName_4 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_10 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_10 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_10 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_10 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_10 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_10 = new System.Windows.Forms.Label();
      this.lbl_CTCType_10 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_10 = new System.Windows.Forms.Label();
      this.lbl_CTCName_10 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_9 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_9 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_9 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_9 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_9 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_9 = new System.Windows.Forms.Label();
      this.lbl_CTCType_9 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_9 = new System.Windows.Forms.Label();
      this.lbl_CTCName_9 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_11 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_11 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_11 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_11 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_11 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_11 = new System.Windows.Forms.Label();
      this.lbl_CTCType_11 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_11 = new System.Windows.Forms.Label();
      this.lbl_CTCName_11 = new System.Windows.Forms.Label();
      this.lbl_CTCSignal_8 = new System.Windows.Forms.Label();
      this.lbl_CTCInc_8 = new System.Windows.Forms.Label();
      this.lbl_CTCCount_8 = new System.Windows.Forms.Label();
      this.lbl_CTCValue_8 = new System.Windows.Forms.Label();
      this.lbl_CTCConfig_8 = new System.Windows.Forms.Label();
      this.lbl_CTCSource_8 = new System.Windows.Forms.Label();
      this.lbl_CTCType_8 = new System.Windows.Forms.Label();
      this.lbl_CTCCtr_8 = new System.Windows.Forms.Label();
      this.lbl_CTCName_8 = new System.Windows.Forms.Label();
      this.ttl_Ctr = new SyncroC.Controls.Title();
      this.ttl_Name = new SyncroC.Controls.Title();
      this.ttl_Type = new SyncroC.Controls.Title();
      this.ttl_Source = new SyncroC.Controls.Title();
      this.ttl_Config = new SyncroC.Controls.Title();
      this.ttl_Value = new SyncroC.Controls.Title();
      this.ttl_Count = new SyncroC.Controls.Title();
      this.ttl_Incr = new SyncroC.Controls.Title();
      this.ttl_Signal = new SyncroC.Controls.Title();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab6.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // lbl_CTCType_0
      // 
      this.lbl_CTCType_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_0.Location = new System.Drawing.Point(579, 239);
      this.lbl_CTCType_0.Name = "lbl_CTCType_0";
      this.lbl_CTCType_0.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_0.TabIndex = 151;
      this.lbl_CTCType_0.Text = "Countdown";
      this.lbl_CTCType_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_0
      // 
      this.lbl_CTCCtr_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_0.Location = new System.Drawing.Point(141, 239);
      this.lbl_CTCCtr_0.Name = "lbl_CTCCtr_0";
      this.lbl_CTCCtr_0.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_0.TabIndex = 149;
      this.lbl_CTCCtr_0.Text = "0000";
      this.lbl_CTCCtr_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_0
      // 
      this.lbl_CTCName_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_0.Location = new System.Drawing.Point(204, 239);
      this.lbl_CTCName_0.Name = "lbl_CTCName_0";
      this.lbl_CTCName_0.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_0.TabIndex = 150;
      this.lbl_CTCName_0.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_0
      // 
      this.lbl_CTCSource_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_0.Location = new System.Drawing.Point(706, 239);
      this.lbl_CTCSource_0.Name = "lbl_CTCSource_0";
      this.lbl_CTCSource_0.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_0.TabIndex = 152;
      this.lbl_CTCSource_0.Text = "000000";
      this.lbl_CTCSource_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_0
      // 
      this.lbl_CTCConfig_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_0.Location = new System.Drawing.Point(783, 239);
      this.lbl_CTCConfig_0.Name = "lbl_CTCConfig_0";
      this.lbl_CTCConfig_0.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_0.TabIndex = 153;
      this.lbl_CTCConfig_0.Text = "000000";
      this.lbl_CTCConfig_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_0
      // 
      this.lbl_CTCValue_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_0.Location = new System.Drawing.Point(864, 239);
      this.lbl_CTCValue_0.Name = "lbl_CTCValue_0";
      this.lbl_CTCValue_0.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_0.TabIndex = 154;
      this.lbl_CTCValue_0.Text = "000000";
      this.lbl_CTCValue_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_0
      // 
      this.lbl_CTCCount_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_0.Location = new System.Drawing.Point(941, 239);
      this.lbl_CTCCount_0.Name = "lbl_CTCCount_0";
      this.lbl_CTCCount_0.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_0.TabIndex = 155;
      this.lbl_CTCCount_0.Text = "000000";
      this.lbl_CTCCount_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_0
      // 
      this.lbl_CTCInc_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_0.Location = new System.Drawing.Point(1018, 239);
      this.lbl_CTCInc_0.Name = "lbl_CTCInc_0";
      this.lbl_CTCInc_0.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_0.TabIndex = 156;
      this.lbl_CTCInc_0.Text = "000000";
      this.lbl_CTCInc_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_0
      // 
      this.lbl_CTCSignal_0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_0.Location = new System.Drawing.Point(1099, 239);
      this.lbl_CTCSignal_0.Name = "lbl_CTCSignal_0";
      this.lbl_CTCSignal_0.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_0.TabIndex = 157;
      this.lbl_CTCSignal_0.Text = "XXX";
      this.lbl_CTCSignal_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_3
      // 
      this.lbl_CTCSignal_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_3.Location = new System.Drawing.Point(1099, 352);
      this.lbl_CTCSignal_3.Name = "lbl_CTCSignal_3";
      this.lbl_CTCSignal_3.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_3.TabIndex = 166;
      this.lbl_CTCSignal_3.Text = "XXX";
      this.lbl_CTCSignal_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_3
      // 
      this.lbl_CTCInc_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_3.Location = new System.Drawing.Point(1018, 352);
      this.lbl_CTCInc_3.Name = "lbl_CTCInc_3";
      this.lbl_CTCInc_3.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_3.TabIndex = 165;
      this.lbl_CTCInc_3.Text = "000000";
      this.lbl_CTCInc_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_3
      // 
      this.lbl_CTCCount_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_3.Location = new System.Drawing.Point(941, 352);
      this.lbl_CTCCount_3.Name = "lbl_CTCCount_3";
      this.lbl_CTCCount_3.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_3.TabIndex = 164;
      this.lbl_CTCCount_3.Text = "000000";
      this.lbl_CTCCount_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_3
      // 
      this.lbl_CTCValue_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_3.Location = new System.Drawing.Point(864, 352);
      this.lbl_CTCValue_3.Name = "lbl_CTCValue_3";
      this.lbl_CTCValue_3.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_3.TabIndex = 163;
      this.lbl_CTCValue_3.Text = "000000";
      this.lbl_CTCValue_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_3
      // 
      this.lbl_CTCConfig_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_3.Location = new System.Drawing.Point(783, 352);
      this.lbl_CTCConfig_3.Name = "lbl_CTCConfig_3";
      this.lbl_CTCConfig_3.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_3.TabIndex = 162;
      this.lbl_CTCConfig_3.Text = "000000";
      this.lbl_CTCConfig_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_3
      // 
      this.lbl_CTCSource_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_3.Location = new System.Drawing.Point(706, 352);
      this.lbl_CTCSource_3.Name = "lbl_CTCSource_3";
      this.lbl_CTCSource_3.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_3.TabIndex = 161;
      this.lbl_CTCSource_3.Text = "000000";
      this.lbl_CTCSource_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_3
      // 
      this.lbl_CTCType_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_3.Location = new System.Drawing.Point(579, 352);
      this.lbl_CTCType_3.Name = "lbl_CTCType_3";
      this.lbl_CTCType_3.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_3.TabIndex = 160;
      this.lbl_CTCType_3.Text = "Countdown";
      this.lbl_CTCType_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_3
      // 
      this.lbl_CTCCtr_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_3.Location = new System.Drawing.Point(141, 352);
      this.lbl_CTCCtr_3.Name = "lbl_CTCCtr_3";
      this.lbl_CTCCtr_3.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_3.TabIndex = 158;
      this.lbl_CTCCtr_3.Text = "0003";
      this.lbl_CTCCtr_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_3
      // 
      this.lbl_CTCName_3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_3.Location = new System.Drawing.Point(204, 352);
      this.lbl_CTCName_3.Name = "lbl_CTCName_3";
      this.lbl_CTCName_3.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_3.TabIndex = 159;
      this.lbl_CTCName_3.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_1
      // 
      this.lbl_CTCSignal_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_1.Location = new System.Drawing.Point(1099, 276);
      this.lbl_CTCSignal_1.Name = "lbl_CTCSignal_1";
      this.lbl_CTCSignal_1.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_1.TabIndex = 175;
      this.lbl_CTCSignal_1.Text = "XXX";
      this.lbl_CTCSignal_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_1
      // 
      this.lbl_CTCInc_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_1.Location = new System.Drawing.Point(1018, 276);
      this.lbl_CTCInc_1.Name = "lbl_CTCInc_1";
      this.lbl_CTCInc_1.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_1.TabIndex = 174;
      this.lbl_CTCInc_1.Text = "000000";
      this.lbl_CTCInc_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_1
      // 
      this.lbl_CTCCount_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_1.Location = new System.Drawing.Point(941, 276);
      this.lbl_CTCCount_1.Name = "lbl_CTCCount_1";
      this.lbl_CTCCount_1.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_1.TabIndex = 173;
      this.lbl_CTCCount_1.Text = "000000";
      this.lbl_CTCCount_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_1
      // 
      this.lbl_CTCValue_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_1.Location = new System.Drawing.Point(864, 276);
      this.lbl_CTCValue_1.Name = "lbl_CTCValue_1";
      this.lbl_CTCValue_1.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_1.TabIndex = 172;
      this.lbl_CTCValue_1.Text = "000000";
      this.lbl_CTCValue_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_1
      // 
      this.lbl_CTCConfig_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_1.Location = new System.Drawing.Point(783, 276);
      this.lbl_CTCConfig_1.Name = "lbl_CTCConfig_1";
      this.lbl_CTCConfig_1.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_1.TabIndex = 171;
      this.lbl_CTCConfig_1.Text = "000000";
      this.lbl_CTCConfig_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_1
      // 
      this.lbl_CTCSource_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_1.Location = new System.Drawing.Point(706, 276);
      this.lbl_CTCSource_1.Name = "lbl_CTCSource_1";
      this.lbl_CTCSource_1.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_1.TabIndex = 170;
      this.lbl_CTCSource_1.Text = "000000";
      this.lbl_CTCSource_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_1
      // 
      this.lbl_CTCType_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_1.Location = new System.Drawing.Point(579, 276);
      this.lbl_CTCType_1.Name = "lbl_CTCType_1";
      this.lbl_CTCType_1.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_1.TabIndex = 169;
      this.lbl_CTCType_1.Text = "Countdown";
      this.lbl_CTCType_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_1
      // 
      this.lbl_CTCCtr_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_1.Location = new System.Drawing.Point(141, 276);
      this.lbl_CTCCtr_1.Name = "lbl_CTCCtr_1";
      this.lbl_CTCCtr_1.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_1.TabIndex = 167;
      this.lbl_CTCCtr_1.Text = "0001";
      this.lbl_CTCCtr_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_1
      // 
      this.lbl_CTCName_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_1.Location = new System.Drawing.Point(204, 276);
      this.lbl_CTCName_1.Name = "lbl_CTCName_1";
      this.lbl_CTCName_1.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_1.TabIndex = 168;
      this.lbl_CTCName_1.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_2
      // 
      this.lbl_CTCSignal_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_2.Location = new System.Drawing.Point(1099, 314);
      this.lbl_CTCSignal_2.Name = "lbl_CTCSignal_2";
      this.lbl_CTCSignal_2.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_2.TabIndex = 184;
      this.lbl_CTCSignal_2.Text = "XXX";
      this.lbl_CTCSignal_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_2
      // 
      this.lbl_CTCInc_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_2.Location = new System.Drawing.Point(1018, 314);
      this.lbl_CTCInc_2.Name = "lbl_CTCInc_2";
      this.lbl_CTCInc_2.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_2.TabIndex = 183;
      this.lbl_CTCInc_2.Text = "000000";
      this.lbl_CTCInc_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_2
      // 
      this.lbl_CTCCount_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_2.Location = new System.Drawing.Point(941, 314);
      this.lbl_CTCCount_2.Name = "lbl_CTCCount_2";
      this.lbl_CTCCount_2.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_2.TabIndex = 182;
      this.lbl_CTCCount_2.Text = "000000";
      this.lbl_CTCCount_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_2
      // 
      this.lbl_CTCValue_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_2.Location = new System.Drawing.Point(864, 314);
      this.lbl_CTCValue_2.Name = "lbl_CTCValue_2";
      this.lbl_CTCValue_2.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_2.TabIndex = 181;
      this.lbl_CTCValue_2.Text = "000000";
      this.lbl_CTCValue_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_2
      // 
      this.lbl_CTCConfig_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_2.Location = new System.Drawing.Point(783, 314);
      this.lbl_CTCConfig_2.Name = "lbl_CTCConfig_2";
      this.lbl_CTCConfig_2.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_2.TabIndex = 180;
      this.lbl_CTCConfig_2.Text = "000000";
      this.lbl_CTCConfig_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_2
      // 
      this.lbl_CTCSource_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_2.Location = new System.Drawing.Point(706, 314);
      this.lbl_CTCSource_2.Name = "lbl_CTCSource_2";
      this.lbl_CTCSource_2.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_2.TabIndex = 179;
      this.lbl_CTCSource_2.Text = "000000";
      this.lbl_CTCSource_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_2
      // 
      this.lbl_CTCType_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_2.Location = new System.Drawing.Point(579, 314);
      this.lbl_CTCType_2.Name = "lbl_CTCType_2";
      this.lbl_CTCType_2.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_2.TabIndex = 178;
      this.lbl_CTCType_2.Text = "Countdown";
      this.lbl_CTCType_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_2
      // 
      this.lbl_CTCCtr_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_2.Location = new System.Drawing.Point(141, 314);
      this.lbl_CTCCtr_2.Name = "lbl_CTCCtr_2";
      this.lbl_CTCCtr_2.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_2.TabIndex = 176;
      this.lbl_CTCCtr_2.Text = "0002";
      this.lbl_CTCCtr_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_2
      // 
      this.lbl_CTCName_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_2.Location = new System.Drawing.Point(204, 314);
      this.lbl_CTCName_2.Name = "lbl_CTCName_2";
      this.lbl_CTCName_2.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_2.TabIndex = 177;
      this.lbl_CTCName_2.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_6
      // 
      this.lbl_CTCSignal_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_6.Location = new System.Drawing.Point(1099, 464);
      this.lbl_CTCSignal_6.Name = "lbl_CTCSignal_6";
      this.lbl_CTCSignal_6.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_6.TabIndex = 220;
      this.lbl_CTCSignal_6.Text = "XXX";
      this.lbl_CTCSignal_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_6
      // 
      this.lbl_CTCInc_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_6.Location = new System.Drawing.Point(1018, 464);
      this.lbl_CTCInc_6.Name = "lbl_CTCInc_6";
      this.lbl_CTCInc_6.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_6.TabIndex = 219;
      this.lbl_CTCInc_6.Text = "000000";
      this.lbl_CTCInc_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_6
      // 
      this.lbl_CTCCount_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_6.Location = new System.Drawing.Point(941, 464);
      this.lbl_CTCCount_6.Name = "lbl_CTCCount_6";
      this.lbl_CTCCount_6.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_6.TabIndex = 218;
      this.lbl_CTCCount_6.Text = "000000";
      this.lbl_CTCCount_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_6
      // 
      this.lbl_CTCValue_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_6.Location = new System.Drawing.Point(864, 464);
      this.lbl_CTCValue_6.Name = "lbl_CTCValue_6";
      this.lbl_CTCValue_6.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_6.TabIndex = 217;
      this.lbl_CTCValue_6.Text = "000000";
      this.lbl_CTCValue_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_6
      // 
      this.lbl_CTCConfig_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_6.Location = new System.Drawing.Point(783, 464);
      this.lbl_CTCConfig_6.Name = "lbl_CTCConfig_6";
      this.lbl_CTCConfig_6.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_6.TabIndex = 216;
      this.lbl_CTCConfig_6.Text = "000000";
      this.lbl_CTCConfig_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_6
      // 
      this.lbl_CTCSource_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_6.Location = new System.Drawing.Point(706, 464);
      this.lbl_CTCSource_6.Name = "lbl_CTCSource_6";
      this.lbl_CTCSource_6.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_6.TabIndex = 215;
      this.lbl_CTCSource_6.Text = "000000";
      this.lbl_CTCSource_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_6
      // 
      this.lbl_CTCType_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_6.Location = new System.Drawing.Point(579, 464);
      this.lbl_CTCType_6.Name = "lbl_CTCType_6";
      this.lbl_CTCType_6.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_6.TabIndex = 214;
      this.lbl_CTCType_6.Text = "Countdown";
      this.lbl_CTCType_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_6
      // 
      this.lbl_CTCCtr_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_6.Location = new System.Drawing.Point(141, 464);
      this.lbl_CTCCtr_6.Name = "lbl_CTCCtr_6";
      this.lbl_CTCCtr_6.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_6.TabIndex = 212;
      this.lbl_CTCCtr_6.Text = "0006";
      this.lbl_CTCCtr_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_6
      // 
      this.lbl_CTCName_6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_6.Location = new System.Drawing.Point(204, 464);
      this.lbl_CTCName_6.Name = "lbl_CTCName_6";
      this.lbl_CTCName_6.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_6.TabIndex = 213;
      this.lbl_CTCName_6.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_5
      // 
      this.lbl_CTCSignal_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_5.Location = new System.Drawing.Point(1099, 426);
      this.lbl_CTCSignal_5.Name = "lbl_CTCSignal_5";
      this.lbl_CTCSignal_5.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_5.TabIndex = 211;
      this.lbl_CTCSignal_5.Text = "XXX";
      this.lbl_CTCSignal_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_5
      // 
      this.lbl_CTCInc_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_5.Location = new System.Drawing.Point(1018, 426);
      this.lbl_CTCInc_5.Name = "lbl_CTCInc_5";
      this.lbl_CTCInc_5.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_5.TabIndex = 210;
      this.lbl_CTCInc_5.Text = "000000";
      this.lbl_CTCInc_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_5
      // 
      this.lbl_CTCCount_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_5.Location = new System.Drawing.Point(941, 426);
      this.lbl_CTCCount_5.Name = "lbl_CTCCount_5";
      this.lbl_CTCCount_5.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_5.TabIndex = 209;
      this.lbl_CTCCount_5.Text = "000000";
      this.lbl_CTCCount_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_5
      // 
      this.lbl_CTCValue_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_5.Location = new System.Drawing.Point(864, 426);
      this.lbl_CTCValue_5.Name = "lbl_CTCValue_5";
      this.lbl_CTCValue_5.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_5.TabIndex = 208;
      this.lbl_CTCValue_5.Text = "000000";
      this.lbl_CTCValue_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_5
      // 
      this.lbl_CTCConfig_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_5.Location = new System.Drawing.Point(783, 426);
      this.lbl_CTCConfig_5.Name = "lbl_CTCConfig_5";
      this.lbl_CTCConfig_5.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_5.TabIndex = 207;
      this.lbl_CTCConfig_5.Text = "000000";
      this.lbl_CTCConfig_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_5
      // 
      this.lbl_CTCSource_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_5.Location = new System.Drawing.Point(706, 426);
      this.lbl_CTCSource_5.Name = "lbl_CTCSource_5";
      this.lbl_CTCSource_5.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_5.TabIndex = 206;
      this.lbl_CTCSource_5.Text = "000000";
      this.lbl_CTCSource_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_5
      // 
      this.lbl_CTCType_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_5.Location = new System.Drawing.Point(579, 426);
      this.lbl_CTCType_5.Name = "lbl_CTCType_5";
      this.lbl_CTCType_5.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_5.TabIndex = 205;
      this.lbl_CTCType_5.Text = "Countdown";
      this.lbl_CTCType_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_5
      // 
      this.lbl_CTCCtr_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_5.Location = new System.Drawing.Point(141, 426);
      this.lbl_CTCCtr_5.Name = "lbl_CTCCtr_5";
      this.lbl_CTCCtr_5.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_5.TabIndex = 203;
      this.lbl_CTCCtr_5.Text = "0005";
      this.lbl_CTCCtr_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_5
      // 
      this.lbl_CTCName_5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_5.Location = new System.Drawing.Point(204, 426);
      this.lbl_CTCName_5.Name = "lbl_CTCName_5";
      this.lbl_CTCName_5.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_5.TabIndex = 204;
      this.lbl_CTCName_5.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_7
      // 
      this.lbl_CTCSignal_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_7.Location = new System.Drawing.Point(1099, 502);
      this.lbl_CTCSignal_7.Name = "lbl_CTCSignal_7";
      this.lbl_CTCSignal_7.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_7.TabIndex = 202;
      this.lbl_CTCSignal_7.Text = "XXX";
      this.lbl_CTCSignal_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_7
      // 
      this.lbl_CTCInc_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_7.Location = new System.Drawing.Point(1018, 502);
      this.lbl_CTCInc_7.Name = "lbl_CTCInc_7";
      this.lbl_CTCInc_7.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_7.TabIndex = 201;
      this.lbl_CTCInc_7.Text = "000000";
      this.lbl_CTCInc_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_7
      // 
      this.lbl_CTCCount_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_7.Location = new System.Drawing.Point(941, 502);
      this.lbl_CTCCount_7.Name = "lbl_CTCCount_7";
      this.lbl_CTCCount_7.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_7.TabIndex = 200;
      this.lbl_CTCCount_7.Text = "000000";
      this.lbl_CTCCount_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_7
      // 
      this.lbl_CTCValue_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_7.Location = new System.Drawing.Point(864, 502);
      this.lbl_CTCValue_7.Name = "lbl_CTCValue_7";
      this.lbl_CTCValue_7.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_7.TabIndex = 199;
      this.lbl_CTCValue_7.Text = "000000";
      this.lbl_CTCValue_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_7
      // 
      this.lbl_CTCConfig_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_7.Location = new System.Drawing.Point(783, 502);
      this.lbl_CTCConfig_7.Name = "lbl_CTCConfig_7";
      this.lbl_CTCConfig_7.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_7.TabIndex = 198;
      this.lbl_CTCConfig_7.Text = "000000";
      this.lbl_CTCConfig_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_7
      // 
      this.lbl_CTCSource_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_7.Location = new System.Drawing.Point(706, 502);
      this.lbl_CTCSource_7.Name = "lbl_CTCSource_7";
      this.lbl_CTCSource_7.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_7.TabIndex = 197;
      this.lbl_CTCSource_7.Text = "000000";
      this.lbl_CTCSource_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_7
      // 
      this.lbl_CTCType_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_7.Location = new System.Drawing.Point(579, 502);
      this.lbl_CTCType_7.Name = "lbl_CTCType_7";
      this.lbl_CTCType_7.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_7.TabIndex = 196;
      this.lbl_CTCType_7.Text = "Countdown";
      this.lbl_CTCType_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_7
      // 
      this.lbl_CTCCtr_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_7.Location = new System.Drawing.Point(141, 502);
      this.lbl_CTCCtr_7.Name = "lbl_CTCCtr_7";
      this.lbl_CTCCtr_7.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_7.TabIndex = 194;
      this.lbl_CTCCtr_7.Text = "0007";
      this.lbl_CTCCtr_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_7
      // 
      this.lbl_CTCName_7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_7.Location = new System.Drawing.Point(204, 502);
      this.lbl_CTCName_7.Name = "lbl_CTCName_7";
      this.lbl_CTCName_7.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_7.TabIndex = 195;
      this.lbl_CTCName_7.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_4
      // 
      this.lbl_CTCSignal_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_4.Location = new System.Drawing.Point(1099, 389);
      this.lbl_CTCSignal_4.Name = "lbl_CTCSignal_4";
      this.lbl_CTCSignal_4.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_4.TabIndex = 193;
      this.lbl_CTCSignal_4.Text = "XXX";
      this.lbl_CTCSignal_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_4
      // 
      this.lbl_CTCInc_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_4.Location = new System.Drawing.Point(1018, 389);
      this.lbl_CTCInc_4.Name = "lbl_CTCInc_4";
      this.lbl_CTCInc_4.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_4.TabIndex = 192;
      this.lbl_CTCInc_4.Text = "000000";
      this.lbl_CTCInc_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_4
      // 
      this.lbl_CTCCount_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_4.Location = new System.Drawing.Point(941, 389);
      this.lbl_CTCCount_4.Name = "lbl_CTCCount_4";
      this.lbl_CTCCount_4.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_4.TabIndex = 191;
      this.lbl_CTCCount_4.Text = "000000";
      this.lbl_CTCCount_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_4
      // 
      this.lbl_CTCValue_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_4.Location = new System.Drawing.Point(864, 389);
      this.lbl_CTCValue_4.Name = "lbl_CTCValue_4";
      this.lbl_CTCValue_4.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_4.TabIndex = 190;
      this.lbl_CTCValue_4.Text = "000000";
      this.lbl_CTCValue_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_4
      // 
      this.lbl_CTCConfig_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_4.Location = new System.Drawing.Point(783, 389);
      this.lbl_CTCConfig_4.Name = "lbl_CTCConfig_4";
      this.lbl_CTCConfig_4.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_4.TabIndex = 189;
      this.lbl_CTCConfig_4.Text = "000000";
      this.lbl_CTCConfig_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_4
      // 
      this.lbl_CTCSource_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_4.Location = new System.Drawing.Point(706, 389);
      this.lbl_CTCSource_4.Name = "lbl_CTCSource_4";
      this.lbl_CTCSource_4.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_4.TabIndex = 188;
      this.lbl_CTCSource_4.Text = "000000";
      this.lbl_CTCSource_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_4
      // 
      this.lbl_CTCType_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_4.Location = new System.Drawing.Point(579, 389);
      this.lbl_CTCType_4.Name = "lbl_CTCType_4";
      this.lbl_CTCType_4.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_4.TabIndex = 187;
      this.lbl_CTCType_4.Text = "Countdown";
      this.lbl_CTCType_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_4
      // 
      this.lbl_CTCCtr_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_4.Location = new System.Drawing.Point(141, 389);
      this.lbl_CTCCtr_4.Name = "lbl_CTCCtr_4";
      this.lbl_CTCCtr_4.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_4.TabIndex = 185;
      this.lbl_CTCCtr_4.Text = "0004";
      this.lbl_CTCCtr_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_4
      // 
      this.lbl_CTCName_4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_4.Location = new System.Drawing.Point(204, 389);
      this.lbl_CTCName_4.Name = "lbl_CTCName_4";
      this.lbl_CTCName_4.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_4.TabIndex = 186;
      this.lbl_CTCName_4.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_10
      // 
      this.lbl_CTCSignal_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_10.Location = new System.Drawing.Point(1099, 616);
      this.lbl_CTCSignal_10.Name = "lbl_CTCSignal_10";
      this.lbl_CTCSignal_10.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_10.TabIndex = 256;
      this.lbl_CTCSignal_10.Text = "XXX";
      this.lbl_CTCSignal_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_10
      // 
      this.lbl_CTCInc_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_10.Location = new System.Drawing.Point(1018, 616);
      this.lbl_CTCInc_10.Name = "lbl_CTCInc_10";
      this.lbl_CTCInc_10.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_10.TabIndex = 255;
      this.lbl_CTCInc_10.Text = "000000";
      this.lbl_CTCInc_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_10
      // 
      this.lbl_CTCCount_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_10.Location = new System.Drawing.Point(941, 616);
      this.lbl_CTCCount_10.Name = "lbl_CTCCount_10";
      this.lbl_CTCCount_10.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_10.TabIndex = 254;
      this.lbl_CTCCount_10.Text = "000000";
      this.lbl_CTCCount_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_10
      // 
      this.lbl_CTCValue_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_10.Location = new System.Drawing.Point(864, 616);
      this.lbl_CTCValue_10.Name = "lbl_CTCValue_10";
      this.lbl_CTCValue_10.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_10.TabIndex = 253;
      this.lbl_CTCValue_10.Text = "000000";
      this.lbl_CTCValue_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_10
      // 
      this.lbl_CTCConfig_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_10.Location = new System.Drawing.Point(783, 616);
      this.lbl_CTCConfig_10.Name = "lbl_CTCConfig_10";
      this.lbl_CTCConfig_10.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_10.TabIndex = 252;
      this.lbl_CTCConfig_10.Text = "000000";
      this.lbl_CTCConfig_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_10
      // 
      this.lbl_CTCSource_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_10.Location = new System.Drawing.Point(706, 616);
      this.lbl_CTCSource_10.Name = "lbl_CTCSource_10";
      this.lbl_CTCSource_10.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_10.TabIndex = 251;
      this.lbl_CTCSource_10.Text = "000000";
      this.lbl_CTCSource_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_10
      // 
      this.lbl_CTCType_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_10.Location = new System.Drawing.Point(579, 616);
      this.lbl_CTCType_10.Name = "lbl_CTCType_10";
      this.lbl_CTCType_10.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_10.TabIndex = 250;
      this.lbl_CTCType_10.Text = "Countdown";
      this.lbl_CTCType_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_10
      // 
      this.lbl_CTCCtr_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_10.Location = new System.Drawing.Point(141, 616);
      this.lbl_CTCCtr_10.Name = "lbl_CTCCtr_10";
      this.lbl_CTCCtr_10.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_10.TabIndex = 248;
      this.lbl_CTCCtr_10.Text = "0010";
      this.lbl_CTCCtr_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_10
      // 
      this.lbl_CTCName_10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_10.Location = new System.Drawing.Point(204, 616);
      this.lbl_CTCName_10.Name = "lbl_CTCName_10";
      this.lbl_CTCName_10.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_10.TabIndex = 249;
      this.lbl_CTCName_10.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_9
      // 
      this.lbl_CTCSignal_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_9.Location = new System.Drawing.Point(1099, 578);
      this.lbl_CTCSignal_9.Name = "lbl_CTCSignal_9";
      this.lbl_CTCSignal_9.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_9.TabIndex = 247;
      this.lbl_CTCSignal_9.Text = "XXX";
      this.lbl_CTCSignal_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_9
      // 
      this.lbl_CTCInc_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_9.Location = new System.Drawing.Point(1018, 578);
      this.lbl_CTCInc_9.Name = "lbl_CTCInc_9";
      this.lbl_CTCInc_9.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_9.TabIndex = 246;
      this.lbl_CTCInc_9.Text = "000000";
      this.lbl_CTCInc_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_9
      // 
      this.lbl_CTCCount_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_9.Location = new System.Drawing.Point(941, 578);
      this.lbl_CTCCount_9.Name = "lbl_CTCCount_9";
      this.lbl_CTCCount_9.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_9.TabIndex = 245;
      this.lbl_CTCCount_9.Text = "000000";
      this.lbl_CTCCount_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_9
      // 
      this.lbl_CTCValue_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_9.Location = new System.Drawing.Point(864, 578);
      this.lbl_CTCValue_9.Name = "lbl_CTCValue_9";
      this.lbl_CTCValue_9.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_9.TabIndex = 244;
      this.lbl_CTCValue_9.Text = "000000";
      this.lbl_CTCValue_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_9
      // 
      this.lbl_CTCConfig_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_9.Location = new System.Drawing.Point(783, 578);
      this.lbl_CTCConfig_9.Name = "lbl_CTCConfig_9";
      this.lbl_CTCConfig_9.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_9.TabIndex = 243;
      this.lbl_CTCConfig_9.Text = "000000";
      this.lbl_CTCConfig_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_9
      // 
      this.lbl_CTCSource_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_9.Location = new System.Drawing.Point(706, 578);
      this.lbl_CTCSource_9.Name = "lbl_CTCSource_9";
      this.lbl_CTCSource_9.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_9.TabIndex = 242;
      this.lbl_CTCSource_9.Text = "000000";
      this.lbl_CTCSource_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_9
      // 
      this.lbl_CTCType_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_9.Location = new System.Drawing.Point(579, 578);
      this.lbl_CTCType_9.Name = "lbl_CTCType_9";
      this.lbl_CTCType_9.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_9.TabIndex = 241;
      this.lbl_CTCType_9.Text = "Countdown";
      this.lbl_CTCType_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_9
      // 
      this.lbl_CTCCtr_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_9.Location = new System.Drawing.Point(141, 578);
      this.lbl_CTCCtr_9.Name = "lbl_CTCCtr_9";
      this.lbl_CTCCtr_9.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_9.TabIndex = 239;
      this.lbl_CTCCtr_9.Text = "0009";
      this.lbl_CTCCtr_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_9
      // 
      this.lbl_CTCName_9.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_9.Location = new System.Drawing.Point(204, 578);
      this.lbl_CTCName_9.Name = "lbl_CTCName_9";
      this.lbl_CTCName_9.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_9.TabIndex = 240;
      this.lbl_CTCName_9.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_11
      // 
      this.lbl_CTCSignal_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_11.Location = new System.Drawing.Point(1099, 654);
      this.lbl_CTCSignal_11.Name = "lbl_CTCSignal_11";
      this.lbl_CTCSignal_11.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_11.TabIndex = 238;
      this.lbl_CTCSignal_11.Text = "XXX";
      this.lbl_CTCSignal_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_11
      // 
      this.lbl_CTCInc_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_11.Location = new System.Drawing.Point(1018, 654);
      this.lbl_CTCInc_11.Name = "lbl_CTCInc_11";
      this.lbl_CTCInc_11.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_11.TabIndex = 237;
      this.lbl_CTCInc_11.Text = "000000";
      this.lbl_CTCInc_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_11
      // 
      this.lbl_CTCCount_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_11.Location = new System.Drawing.Point(941, 654);
      this.lbl_CTCCount_11.Name = "lbl_CTCCount_11";
      this.lbl_CTCCount_11.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_11.TabIndex = 236;
      this.lbl_CTCCount_11.Text = "000000";
      this.lbl_CTCCount_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_11
      // 
      this.lbl_CTCValue_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_11.Location = new System.Drawing.Point(864, 654);
      this.lbl_CTCValue_11.Name = "lbl_CTCValue_11";
      this.lbl_CTCValue_11.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_11.TabIndex = 235;
      this.lbl_CTCValue_11.Text = "000000";
      this.lbl_CTCValue_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_11
      // 
      this.lbl_CTCConfig_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_11.Location = new System.Drawing.Point(783, 654);
      this.lbl_CTCConfig_11.Name = "lbl_CTCConfig_11";
      this.lbl_CTCConfig_11.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_11.TabIndex = 234;
      this.lbl_CTCConfig_11.Text = "000000";
      this.lbl_CTCConfig_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_11
      // 
      this.lbl_CTCSource_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_11.Location = new System.Drawing.Point(706, 654);
      this.lbl_CTCSource_11.Name = "lbl_CTCSource_11";
      this.lbl_CTCSource_11.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_11.TabIndex = 233;
      this.lbl_CTCSource_11.Text = "000000";
      this.lbl_CTCSource_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_11
      // 
      this.lbl_CTCType_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_11.Location = new System.Drawing.Point(579, 654);
      this.lbl_CTCType_11.Name = "lbl_CTCType_11";
      this.lbl_CTCType_11.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_11.TabIndex = 232;
      this.lbl_CTCType_11.Text = "Countdown";
      this.lbl_CTCType_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_11
      // 
      this.lbl_CTCCtr_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_11.Location = new System.Drawing.Point(141, 654);
      this.lbl_CTCCtr_11.Name = "lbl_CTCCtr_11";
      this.lbl_CTCCtr_11.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_11.TabIndex = 230;
      this.lbl_CTCCtr_11.Text = "0011";
      this.lbl_CTCCtr_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_11
      // 
      this.lbl_CTCName_11.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_11.Location = new System.Drawing.Point(204, 654);
      this.lbl_CTCName_11.Name = "lbl_CTCName_11";
      this.lbl_CTCName_11.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_11.TabIndex = 231;
      this.lbl_CTCName_11.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSignal_8
      // 
      this.lbl_CTCSignal_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSignal_8.Location = new System.Drawing.Point(1099, 541);
      this.lbl_CTCSignal_8.Name = "lbl_CTCSignal_8";
      this.lbl_CTCSignal_8.Size = new System.Drawing.Size(39, 30);
      this.lbl_CTCSignal_8.TabIndex = 229;
      this.lbl_CTCSignal_8.Text = "XXX";
      this.lbl_CTCSignal_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCInc_8
      // 
      this.lbl_CTCInc_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCInc_8.Location = new System.Drawing.Point(1018, 541);
      this.lbl_CTCInc_8.Name = "lbl_CTCInc_8";
      this.lbl_CTCInc_8.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCInc_8.TabIndex = 228;
      this.lbl_CTCInc_8.Text = "000000";
      this.lbl_CTCInc_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCount_8
      // 
      this.lbl_CTCCount_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCount_8.Location = new System.Drawing.Point(941, 541);
      this.lbl_CTCCount_8.Name = "lbl_CTCCount_8";
      this.lbl_CTCCount_8.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCCount_8.TabIndex = 227;
      this.lbl_CTCCount_8.Text = "000000";
      this.lbl_CTCCount_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCValue_8
      // 
      this.lbl_CTCValue_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCValue_8.Location = new System.Drawing.Point(864, 541);
      this.lbl_CTCValue_8.Name = "lbl_CTCValue_8";
      this.lbl_CTCValue_8.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCValue_8.TabIndex = 226;
      this.lbl_CTCValue_8.Text = "000000";
      this.lbl_CTCValue_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCConfig_8
      // 
      this.lbl_CTCConfig_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCConfig_8.Location = new System.Drawing.Point(783, 541);
      this.lbl_CTCConfig_8.Name = "lbl_CTCConfig_8";
      this.lbl_CTCConfig_8.Size = new System.Drawing.Size(64, 30);
      this.lbl_CTCConfig_8.TabIndex = 225;
      this.lbl_CTCConfig_8.Text = "000000";
      this.lbl_CTCConfig_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCSource_8
      // 
      this.lbl_CTCSource_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCSource_8.Location = new System.Drawing.Point(706, 541);
      this.lbl_CTCSource_8.Name = "lbl_CTCSource_8";
      this.lbl_CTCSource_8.Size = new System.Drawing.Size(65, 30);
      this.lbl_CTCSource_8.TabIndex = 224;
      this.lbl_CTCSource_8.Text = "000000";
      this.lbl_CTCSource_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCType_8
      // 
      this.lbl_CTCType_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCType_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCType_8.Location = new System.Drawing.Point(579, 541);
      this.lbl_CTCType_8.Name = "lbl_CTCType_8";
      this.lbl_CTCType_8.Size = new System.Drawing.Size(106, 30);
      this.lbl_CTCType_8.TabIndex = 223;
      this.lbl_CTCType_8.Text = "Countdown";
      this.lbl_CTCType_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCCtr_8
      // 
      this.lbl_CTCCtr_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_CTCCtr_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCCtr_8.Location = new System.Drawing.Point(141, 541);
      this.lbl_CTCCtr_8.Name = "lbl_CTCCtr_8";
      this.lbl_CTCCtr_8.Size = new System.Drawing.Size(55, 30);
      this.lbl_CTCCtr_8.TabIndex = 221;
      this.lbl_CTCCtr_8.Text = "0008";
      this.lbl_CTCCtr_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_CTCName_8
      // 
      this.lbl_CTCName_8.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CTCName_8.Location = new System.Drawing.Point(204, 541);
      this.lbl_CTCName_8.Name = "lbl_CTCName_8";
      this.lbl_CTCName_8.Size = new System.Drawing.Size(356, 30);
      this.lbl_CTCName_8.TabIndex = 222;
      this.lbl_CTCName_8.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
      this.lbl_CTCName_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ttl_Ctr
      // 
      this.ttl_Ctr.BackColor = System.Drawing.Color.Black;
      this.ttl_Ctr.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Ctr.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Ctr.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Ctr.Location = new System.Drawing.Point(127, 208);
      this.ttl_Ctr.Name = "ttl_Ctr";
      this.ttl_Ctr.Size = new System.Drawing.Size(79, 487);
      this.ttl_Ctr.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Ctr.TabIndex = 257;
      this.ttl_Ctr.Text = "Ctr";
      this.ttl_Ctr.TranslateText = false;
      // 
      // ttl_Name
      // 
      this.ttl_Name.BackColor = System.Drawing.Color.Black;
      this.ttl_Name.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Name.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Name.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Name.Location = new System.Drawing.Point(206, 208);
      this.ttl_Name.Name = "ttl_Name";
      this.ttl_Name.Size = new System.Drawing.Size(374, 20);
      this.ttl_Name.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Name.TabIndex = 258;
      this.ttl_Name.Text = "Name";
      this.ttl_Name.TranslateText = false;
      // 
      // ttl_Type
      // 
      this.ttl_Type.BackColor = System.Drawing.Color.Black;
      this.ttl_Type.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Type.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Type.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Type.Location = new System.Drawing.Point(580, 208);
      this.ttl_Type.Name = "ttl_Type";
      this.ttl_Type.Size = new System.Drawing.Size(129, 20);
      this.ttl_Type.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Type.TabIndex = 259;
      this.ttl_Type.Text = "Type";
      this.ttl_Type.TranslateText = false;
      // 
      // ttl_Source
      // 
      this.ttl_Source.BackColor = System.Drawing.Color.Black;
      this.ttl_Source.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Source.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Source.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Source.Location = new System.Drawing.Point(709, 208);
      this.ttl_Source.Name = "ttl_Source";
      this.ttl_Source.Size = new System.Drawing.Size(77, 20);
      this.ttl_Source.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Source.TabIndex = 260;
      this.ttl_Source.Text = "Source";
      this.ttl_Source.TranslateText = false;
      // 
      // ttl_Config
      // 
      this.ttl_Config.BackColor = System.Drawing.Color.Black;
      this.ttl_Config.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Config.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Config.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Config.Location = new System.Drawing.Point(786, 208);
      this.ttl_Config.Name = "ttl_Config";
      this.ttl_Config.Size = new System.Drawing.Size(81, 20);
      this.ttl_Config.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Config.TabIndex = 261;
      this.ttl_Config.Text = "Config";
      this.ttl_Config.TranslateText = false;
      // 
      // ttl_Value
      // 
      this.ttl_Value.BackColor = System.Drawing.Color.Black;
      this.ttl_Value.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Value.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Value.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Value.Location = new System.Drawing.Point(867, 208);
      this.ttl_Value.Name = "ttl_Value";
      this.ttl_Value.Size = new System.Drawing.Size(76, 20);
      this.ttl_Value.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Value.TabIndex = 262;
      this.ttl_Value.Text = "Value";
      this.ttl_Value.TranslateText = false;
      // 
      // ttl_Count
      // 
      this.ttl_Count.BackColor = System.Drawing.Color.Black;
      this.ttl_Count.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Count.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Count.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Count.Location = new System.Drawing.Point(943, 208);
      this.ttl_Count.Name = "ttl_Count";
      this.ttl_Count.Size = new System.Drawing.Size(76, 20);
      this.ttl_Count.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Count.TabIndex = 263;
      this.ttl_Count.Text = "Count";
      this.ttl_Count.TranslateText = false;
      // 
      // ttl_Incr
      // 
      this.ttl_Incr.BackColor = System.Drawing.Color.Black;
      this.ttl_Incr.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Incr.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Incr.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Incr.Location = new System.Drawing.Point(1019, 208);
      this.ttl_Incr.Name = "ttl_Incr";
      this.ttl_Incr.Size = new System.Drawing.Size(71, 20);
      this.ttl_Incr.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Incr.TabIndex = 264;
      this.ttl_Incr.Text = "Incr";
      this.ttl_Incr.TranslateText = false;
      // 
      // ttl_Signal
      // 
      this.ttl_Signal.BackColor = System.Drawing.Color.Black;
      this.ttl_Signal.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Signal.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Signal.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Signal.Location = new System.Drawing.Point(1090, 208);
      this.ttl_Signal.Name = "ttl_Signal";
      this.ttl_Signal.Size = new System.Drawing.Size(61, 20);
      this.ttl_Signal.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Signal.TabIndex = 265;
      this.ttl_Signal.Text = "Signal";
      this.ttl_Signal.TranslateText = false;
      // 
      // Frm_CTCStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_Signal);
      this.Controls.Add(this.ttl_Incr);
      this.Controls.Add(this.ttl_Count);
      this.Controls.Add(this.ttl_Value);
      this.Controls.Add(this.ttl_Config);
      this.Controls.Add(this.ttl_Source);
      this.Controls.Add(this.ttl_Type);
      this.Controls.Add(this.ttl_Name);
      this.Controls.Add(this.ttl_Ctr);
      this.Controls.Add(this.lbl_CTCSignal_10);
      this.Controls.Add(this.lbl_CTCInc_10);
      this.Controls.Add(this.lbl_CTCCount_10);
      this.Controls.Add(this.lbl_CTCValue_10);
      this.Controls.Add(this.lbl_CTCConfig_10);
      this.Controls.Add(this.lbl_CTCSource_10);
      this.Controls.Add(this.lbl_CTCType_10);
      this.Controls.Add(this.lbl_CTCCtr_10);
      this.Controls.Add(this.lbl_CTCName_10);
      this.Controls.Add(this.lbl_CTCSignal_9);
      this.Controls.Add(this.lbl_CTCInc_9);
      this.Controls.Add(this.lbl_CTCCount_9);
      this.Controls.Add(this.lbl_CTCValue_9);
      this.Controls.Add(this.lbl_CTCConfig_9);
      this.Controls.Add(this.lbl_CTCSource_9);
      this.Controls.Add(this.lbl_CTCType_9);
      this.Controls.Add(this.lbl_CTCCtr_9);
      this.Controls.Add(this.lbl_CTCName_9);
      this.Controls.Add(this.lbl_CTCSignal_11);
      this.Controls.Add(this.lbl_CTCInc_11);
      this.Controls.Add(this.lbl_CTCCount_11);
      this.Controls.Add(this.lbl_CTCValue_11);
      this.Controls.Add(this.lbl_CTCConfig_11);
      this.Controls.Add(this.lbl_CTCSource_11);
      this.Controls.Add(this.lbl_CTCType_11);
      this.Controls.Add(this.lbl_CTCCtr_11);
      this.Controls.Add(this.lbl_CTCName_11);
      this.Controls.Add(this.lbl_CTCSignal_8);
      this.Controls.Add(this.lbl_CTCInc_8);
      this.Controls.Add(this.lbl_CTCCount_8);
      this.Controls.Add(this.lbl_CTCValue_8);
      this.Controls.Add(this.lbl_CTCConfig_8);
      this.Controls.Add(this.lbl_CTCSource_8);
      this.Controls.Add(this.lbl_CTCType_8);
      this.Controls.Add(this.lbl_CTCCtr_8);
      this.Controls.Add(this.lbl_CTCName_8);
      this.Controls.Add(this.lbl_CTCSignal_6);
      this.Controls.Add(this.lbl_CTCInc_6);
      this.Controls.Add(this.lbl_CTCCount_6);
      this.Controls.Add(this.lbl_CTCValue_6);
      this.Controls.Add(this.lbl_CTCConfig_6);
      this.Controls.Add(this.lbl_CTCSource_6);
      this.Controls.Add(this.lbl_CTCType_6);
      this.Controls.Add(this.lbl_CTCCtr_6);
      this.Controls.Add(this.lbl_CTCName_6);
      this.Controls.Add(this.lbl_CTCSignal_5);
      this.Controls.Add(this.lbl_CTCInc_5);
      this.Controls.Add(this.lbl_CTCCount_5);
      this.Controls.Add(this.lbl_CTCValue_5);
      this.Controls.Add(this.lbl_CTCConfig_5);
      this.Controls.Add(this.lbl_CTCSource_5);
      this.Controls.Add(this.lbl_CTCType_5);
      this.Controls.Add(this.lbl_CTCCtr_5);
      this.Controls.Add(this.lbl_CTCName_5);
      this.Controls.Add(this.lbl_CTCSignal_7);
      this.Controls.Add(this.lbl_CTCInc_7);
      this.Controls.Add(this.lbl_CTCCount_7);
      this.Controls.Add(this.lbl_CTCValue_7);
      this.Controls.Add(this.lbl_CTCConfig_7);
      this.Controls.Add(this.lbl_CTCSource_7);
      this.Controls.Add(this.lbl_CTCType_7);
      this.Controls.Add(this.lbl_CTCCtr_7);
      this.Controls.Add(this.lbl_CTCName_7);
      this.Controls.Add(this.lbl_CTCSignal_4);
      this.Controls.Add(this.lbl_CTCInc_4);
      this.Controls.Add(this.lbl_CTCCount_4);
      this.Controls.Add(this.lbl_CTCValue_4);
      this.Controls.Add(this.lbl_CTCConfig_4);
      this.Controls.Add(this.lbl_CTCSource_4);
      this.Controls.Add(this.lbl_CTCType_4);
      this.Controls.Add(this.lbl_CTCCtr_4);
      this.Controls.Add(this.lbl_CTCName_4);
      this.Controls.Add(this.lbl_CTCSignal_2);
      this.Controls.Add(this.lbl_CTCInc_2);
      this.Controls.Add(this.lbl_CTCCount_2);
      this.Controls.Add(this.lbl_CTCValue_2);
      this.Controls.Add(this.lbl_CTCConfig_2);
      this.Controls.Add(this.lbl_CTCSource_2);
      this.Controls.Add(this.lbl_CTCType_2);
      this.Controls.Add(this.lbl_CTCCtr_2);
      this.Controls.Add(this.lbl_CTCName_2);
      this.Controls.Add(this.lbl_CTCSignal_1);
      this.Controls.Add(this.lbl_CTCInc_1);
      this.Controls.Add(this.lbl_CTCCount_1);
      this.Controls.Add(this.lbl_CTCValue_1);
      this.Controls.Add(this.lbl_CTCConfig_1);
      this.Controls.Add(this.lbl_CTCSource_1);
      this.Controls.Add(this.lbl_CTCType_1);
      this.Controls.Add(this.lbl_CTCCtr_1);
      this.Controls.Add(this.lbl_CTCName_1);
      this.Controls.Add(this.lbl_CTCSignal_3);
      this.Controls.Add(this.lbl_CTCInc_3);
      this.Controls.Add(this.lbl_CTCCount_3);
      this.Controls.Add(this.lbl_CTCValue_3);
      this.Controls.Add(this.lbl_CTCConfig_3);
      this.Controls.Add(this.lbl_CTCSource_3);
      this.Controls.Add(this.lbl_CTCType_3);
      this.Controls.Add(this.lbl_CTCCtr_3);
      this.Controls.Add(this.lbl_CTCName_3);
      this.Controls.Add(this.lbl_CTCSignal_0);
      this.Controls.Add(this.lbl_CTCInc_0);
      this.Controls.Add(this.lbl_CTCCount_0);
      this.Controls.Add(this.lbl_CTCValue_0);
      this.Controls.Add(this.lbl_CTCConfig_0);
      this.Controls.Add(this.lbl_CTCSource_0);
      this.Controls.Add(this.lbl_CTCType_0);
      this.Controls.Add(this.lbl_CTCCtr_0);
      this.Controls.Add(this.lbl_CTCName_0);
      this.Name = "Frm_CTCStatus";
      this.Text = "Frm_CTCStatus";
      this.Load += new System.EventHandler(this.Frm_CTCStatus_Load);
      this.Activated += new System.EventHandler(this.Frm_CTCStatus_Activated);
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
      this.Controls.SetChildIndex(this.lbl_CTCName_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_0, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_3, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_1, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_2, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_4, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_7, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_5, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_6, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_8, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_11, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_9, 0);
      this.Controls.SetChildIndex(this.lbl_CTCName_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCtr_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCType_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSource_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCConfig_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCValue_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCCount_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCInc_10, 0);
      this.Controls.SetChildIndex(this.lbl_CTCSignal_10, 0);
      this.Controls.SetChildIndex(this.ttl_Ctr, 0);
      this.Controls.SetChildIndex(this.ttl_Name, 0);
      this.Controls.SetChildIndex(this.ttl_Type, 0);
      this.Controls.SetChildIndex(this.ttl_Source, 0);
      this.Controls.SetChildIndex(this.ttl_Config, 0);
      this.Controls.SetChildIndex(this.ttl_Value, 0);
      this.Controls.SetChildIndex(this.ttl_Count, 0);
      this.Controls.SetChildIndex(this.ttl_Incr, 0);
      this.Controls.SetChildIndex(this.ttl_Signal, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion


		private void _setErrorCTCStatus(bool error)
		{

			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach (FieldInfo field in fields)
			{
				if (field.Name.StartsWith("lbl_CTC"))
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
						Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _clearCTCStatus()
		{
			Type type = this.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			foreach (FieldInfo field in fields)
			{
				if (field.Name.StartsWith("lbl_CTC"))
				{
					try
					{
						Label l = (Label)field.GetValue(this);
						l.Text = "";
						l.BackColor = Color.WhiteSmoke;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _clearCTCStatus(int num)
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
						Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
					}
				}
			}
		}

		private void _setCTCStatus(int num, int ctr, string name, string typeCTC, string source, string config, string value, string count, string inc, string signal)
		{
			//Ctr
			Type type = this.GetType();
			FieldInfo field = type.GetField("lbl_CTCCtr_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = ctr.ToString();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			//name
			type = this.GetType();
			field = type.GetField("lbl_CTCName_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = name;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			//typeCTC
			type = this.GetType();
			field = type.GetField("lbl_CTCType_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = typeCTC;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			//Source
			type = this.GetType();
			field = type.GetField("lbl_CTCSource_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = source;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			//Config
			type = this.GetType();
			field = type.GetField("lbl_CTCConfig_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = config;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			
			//Increment
			type = this.GetType();
			field = type.GetField("lbl_CTCInc_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = inc;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			//Signal
			type = this.GetType();
			field = type.GetField("lbl_CTCSignal_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = signal;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}

			//Status
			this._setCTCStatus(num, value, count);
		}

		private void _setCTCStatus(int num, string value, string count)
		{
			//Value
			Type type = this.GetType();
			FieldInfo field = type.GetField("lbl_CTCValue_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = value;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
			//Count
			type = this.GetType();
			field = type.GetField("lbl_CTCCount_" + num, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

			if (field != null)
			{
				try
				{
					Label l = (Label)field.GetValue(this);
					l.Text = count;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "_setCTCStatus - Reflection failed", ex);
				}
			}
		}

		protected override void PageDown()
		{
			if (PageManager.getPageManager().pagesHistorian.CurrentDSPModule < 60)
			{
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule++;
				SendCTCModule();
			}
		}

		protected override void PageUp()
		{
			if (PageManager.getPageManager().pagesHistorian.CurrentDSPModule > 0)
			{
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule--;
				SendCTCModule();
			}
		}

		private void Frm_CTCStatus_Load(object sender, EventArgs e)
		{
			SendCTCModule();
		}

		private void SendCTCModule()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, Command.PAGE_MANAGER + "." + Command.SET_CTC_MODULE + "(" +
				PageManager.getPageManager().pagesHistorian.CurrentDSPModule.ToString() + ")");
			this._clearCTCStatus();
		}
	}
}

