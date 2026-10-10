using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.Pages.Stacker
{
	public class Frm_StackerConsole : SyncroC.Forms.Pages.Stacker.Frm_StackerGroup
	{
		private SyncroC.Controls.Title ttl_manualSendButtons;
		private SyncroC.Controls.StateButton SendNextP1Order;
		private SyncroC.Controls.StateButton SendNextOrder;
		private SyncroC.Controls.StateButton SendCurrentOrder;
		private System.ComponentModel.IContainer components = null;

		public Frm_StackerConsole()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified Stacker.
		/// </summary>
		/// <param name="index">Index of the stacker represented on the current instance of this Form</param>
		public Frm_StackerConsole(int index) : this()
		{
			if (! (index == 1 || index == 2 || index == 3)) 
				Logger.LogError(this, "Frm_StackerConsole - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			//lbl_Title_YN.Text += " " + index.ToString();
      DBManager dbm = DBManager.getDBManager();
      level = (LevelName)index;
      if ((dbm.lineConfig.KnifeConfig.LevelCount == 2) && (index == 2))
          level = LevelName.Upper;

      lbl_Title_YN.Text += " " + level.ToString();

			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
			{
				this.SendCurrentOrder.IsButton = false;
				this.SendNextOrder.IsButton = false;
				this.SendNextP1Order.IsButton = false;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StackerConsole));
      this.ttl_manualSendButtons = new SyncroC.Controls.Title();
      this.SendNextP1Order = new SyncroC.Controls.StateButton();
      this.SendNextOrder = new SyncroC.Controls.StateButton();
      this.SendCurrentOrder = new SyncroC.Controls.StateButton();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab4.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // ttl_manualSendButtons
      // 
      this.ttl_manualSendButtons.BackColor = System.Drawing.Color.Black;
      this.ttl_manualSendButtons.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_manualSendButtons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_manualSendButtons.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_manualSendButtons.Location = new System.Drawing.Point(31, 193);
      this.ttl_manualSendButtons.Name = "ttl_manualSendButtons";
      this.ttl_manualSendButtons.Size = new System.Drawing.Size(1170, 173);
      this.ttl_manualSendButtons.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_manualSendButtons.TabIndex = 206;
      this.ttl_manualSendButtons.Text = "Manual order sending";
      this.ttl_manualSendButtons.TranslateText = true;
      // 
      // SendNextP1Order
      // 
      this.SendNextP1Order.Active = false;
      this.SendNextP1Order.Alarmed = false;
      this.SendNextP1Order.Auto = false;
      this.SendNextP1Order.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextP1Order.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextP1Order.FillBackColorWithImage = true;
      this.SendNextP1Order.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextP1Order.ForeColor = System.Drawing.Color.Black;
      this.SendNextP1Order.FramePresent = true;
      this.SendNextP1Order.HighLightEnable = false;
      this.SendNextP1Order.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextP1Order.ImagePassword = null;
      this.SendNextP1Order.Inemergency = false;
      this.SendNextP1Order.InLocal = false;
      this.SendNextP1Order.IsButton = true;
      this.SendNextP1Order.IsForceToInvisible = false;
      this.SendNextP1Order.Location = new System.Drawing.Point(725, 246);
      this.SendNextP1Order.Name = "SendNextP1Order";
      this.SendNextP1Order.OnLine = false;
      this.SendNextP1Order.ShowImage = true;
      this.SendNextP1Order.ShowText = true;
      this.SendNextP1Order.Size = new System.Drawing.Size(120, 90);
      this.SendNextP1Order.Switching = false;
      this.SendNextP1Order.TabIndex = 205;
      this.SendNextP1Order.Text = "3";
      this.SendNextP1Order.TranslateText = false;
      this.SendNextP1Order.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextP1Order.UserDefinedActiveImage")));
      this.SendNextP1Order.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextP1Order.UserDefinedImage")));
      this.SendNextP1Order.Warned = false;
      this.SendNextP1Order.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendNextOrder
      // 
      this.SendNextOrder.Active = false;
      this.SendNextOrder.Alarmed = false;
      this.SendNextOrder.Auto = false;
      this.SendNextOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextOrder.FillBackColorWithImage = true;
      this.SendNextOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextOrder.ForeColor = System.Drawing.Color.Black;
      this.SendNextOrder.FramePresent = true;
      this.SendNextOrder.HighLightEnable = false;
      this.SendNextOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextOrder.ImagePassword = null;
      this.SendNextOrder.Inemergency = false;
      this.SendNextOrder.InLocal = false;
      this.SendNextOrder.IsButton = true;
      this.SendNextOrder.IsForceToInvisible = false;
      this.SendNextOrder.Location = new System.Drawing.Point(512, 246);
      this.SendNextOrder.Name = "SendNextOrder";
      this.SendNextOrder.OnLine = false;
      this.SendNextOrder.ShowImage = true;
      this.SendNextOrder.ShowText = true;
      this.SendNextOrder.Size = new System.Drawing.Size(120, 90);
      this.SendNextOrder.Switching = false;
      this.SendNextOrder.TabIndex = 204;
      this.SendNextOrder.Text = "2";
      this.SendNextOrder.TranslateText = false;
      this.SendNextOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedActiveImage")));
      this.SendNextOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedImage")));
      this.SendNextOrder.Warned = false;
      this.SendNextOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendCurrentOrder
      // 
      this.SendCurrentOrder.Active = false;
      this.SendCurrentOrder.Alarmed = false;
      this.SendCurrentOrder.Auto = false;
      this.SendCurrentOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendCurrentOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendCurrentOrder.FillBackColorWithImage = true;
      this.SendCurrentOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendCurrentOrder.ForeColor = System.Drawing.Color.Black;
      this.SendCurrentOrder.FramePresent = true;
      this.SendCurrentOrder.HighLightEnable = false;
      this.SendCurrentOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendCurrentOrder.ImagePassword = null;
      this.SendCurrentOrder.Inemergency = false;
      this.SendCurrentOrder.InLocal = false;
      this.SendCurrentOrder.IsButton = true;
      this.SendCurrentOrder.IsForceToInvisible = false;
      this.SendCurrentOrder.Location = new System.Drawing.Point(299, 246);
      this.SendCurrentOrder.Name = "SendCurrentOrder";
      this.SendCurrentOrder.OnLine = false;
      this.SendCurrentOrder.ShowImage = true;
      this.SendCurrentOrder.ShowText = true;
      this.SendCurrentOrder.Size = new System.Drawing.Size(120, 90);
      this.SendCurrentOrder.Switching = false;
      this.SendCurrentOrder.TabIndex = 203;
      this.SendCurrentOrder.Text = "1";
      this.SendCurrentOrder.TranslateText = false;
      this.SendCurrentOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedActiveImage")));
      this.SendCurrentOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedImage")));
      this.SendCurrentOrder.Warned = false;
      this.SendCurrentOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // Frm_StackerConsole
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_manualSendButtons);
      this.Controls.Add(this.SendNextP1Order);
      this.Controls.Add(this.SendNextOrder);
      this.Controls.Add(this.SendCurrentOrder);
      this.DesignGridHorizontalLines = 2;
      this.Name = "Frm_StackerConsole";
      this.Text = "Frm_StackerConsole";
      this.Load += new System.EventHandler(this.Frm_StackerConsole_Load);
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
      this.Controls.SetChildIndex(this.SendCurrentOrder, 0);
      this.Controls.SetChildIndex(this.SendNextOrder, 0);
      this.Controls.SetChildIndex(this.SendNextP1Order, 0);
      this.Controls.SetChildIndex(this.ttl_manualSendButtons, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion


		protected override void F4()
		{
		}

    //PMASSARO_FI02/09/10 - Aggiunto controllo sulla possibilità di inviare gli ordini allo stacker (da grafica)
    private bool isManualOrderSendingDisabledForThisMachine(int numberOfOrderToSend)
    {
      //numberOfOrderToSend può essere utilizzato per successivi controlli relativi ad invio ordine corrente, successivo, ....
      bool disabledFunctionality = false;
      switch (level)
      {
        case LevelName.Lower:
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 1)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings1.Enable; }
          break;
        case LevelName.Middle:
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 3)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings2.Enable; }
          break;
        case LevelName.Upper:
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 2)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings2.Enable; }
          else if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 3)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings3.Enable; }
          break;
      }

      return (disabledFunctionality);
    }

		/// <summary>
		/// Handles event raised by generic button that are in the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

      //PMASSARO_FI02/09/10 - Aggiunto controllo sulla possibilità di inviare gli ordini allo stacker (da grafica)
      int numberOfOrderToSend = 1;
      if (sender.ToString().Contains("SendNext"))
        numberOfOrderToSend = 2;
      if (sender.ToString().Contains("SendNextP1"))
        numberOfOrderToSend = 3;
      if (isManualOrderSendingDisabledForThisMachine(numberOfOrderToSend))
      {
        PageManager.getPageManager()._SetMessage("§SYCC_201");
        return;
      }

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.STACKER + index.ToString() + "." + ((Control)sender).Name + "()");
		}

		private void Frm_StackerConsole_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
			this.SendNextP1Order.Alarmed = isError;
		}

	}
}

