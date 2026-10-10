using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;

namespace SyncroC.Forms.Pages.SlitterScorer.Status
{
	public class Frm_SlitterScorerConsole : SyncroC.Forms.Pages.SlitterScorer.Status.Frm_SlitterScorerStatusSubgroup
	{
		private SyncroC.Controls.Title ttl_manualSendButtons;
		private SyncroC.Controls.StateButton SendNextP3Order;
		private SyncroC.Controls.StateButton SendNextP2Order;
		private SyncroC.Controls.StateButton SendNextP1Order;
		private SyncroC.Controls.StateButton SendNextOrder;
		private SyncroC.Controls.StateButton SendCurrentOrder;
		private System.ComponentModel.IContainer components = null;

		public Frm_SlitterScorerConsole()
		{
			InitializeComponent();

			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
			{
				this.SendCurrentOrder.IsButton = false;
				this.SendNextOrder.IsButton = false;
				this.SendNextP1Order.IsButton = false;
				this.SendNextP2Order.IsButton = false;
				this.SendNextP3Order.IsButton = false;
			}
      //agrassi_FI25-05-2011_1
      if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsCompact2() ||
          DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin2())
      {
        this.SendNextP1Order.Visible = this.SendNextP2Order.Visible = this.SendNextP3Order.Visible = false;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerConsole));
      this.ttl_manualSendButtons = new SyncroC.Controls.Title();
      this.SendNextP3Order = new SyncroC.Controls.StateButton();
      this.SendNextP2Order = new SyncroC.Controls.StateButton();
      this.SendNextP1Order = new SyncroC.Controls.StateButton();
      this.SendNextOrder = new SyncroC.Controls.StateButton();
      this.SendCurrentOrder = new SyncroC.Controls.StateButton();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // ttl_manualSendButtons
      // 
      this.ttl_manualSendButtons.BackColor = System.Drawing.Color.Black;
      this.ttl_manualSendButtons.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_manualSendButtons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_manualSendButtons.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_manualSendButtons.Location = new System.Drawing.Point(30, 196);
      this.ttl_manualSendButtons.Name = "ttl_manualSendButtons";
      this.ttl_manualSendButtons.Size = new System.Drawing.Size(1171, 173);
      this.ttl_manualSendButtons.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_manualSendButtons.TabIndex = 208;
      this.ttl_manualSendButtons.Text = "Manual order sending";
      this.ttl_manualSendButtons.TranslateText = true;
      // 
      // SendNextP3Order
      // 
      this.SendNextP3Order.Active = false;
      this.SendNextP3Order.Alarmed = false;
      this.SendNextP3Order.Auto = false;
      this.SendNextP3Order.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextP3Order.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextP3Order.FillBackColorWithImage = true;
      this.SendNextP3Order.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextP3Order.ForeColor = System.Drawing.Color.Black;
      this.SendNextP3Order.FramePresent = true;
      this.SendNextP3Order.HighLightEnable = false;
      this.SendNextP3Order.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextP3Order.ImagePassword = null;
      this.SendNextP3Order.Inemergency = false;
      this.SendNextP3Order.InLocal = false;
      this.SendNextP3Order.IsButton = true;
      this.SendNextP3Order.IsForceToInvisible = false;
      this.SendNextP3Order.Location = new System.Drawing.Point(982, 253);
      this.SendNextP3Order.Name = "SendNextP3Order";
      this.SendNextP3Order.OnLine = false;
      this.SendNextP3Order.ShowImage = true;
      this.SendNextP3Order.ShowText = true;
      this.SendNextP3Order.Size = new System.Drawing.Size(120, 90);
      this.SendNextP3Order.Switching = false;
      this.SendNextP3Order.TabIndex = 207;
      this.SendNextP3Order.Text = "5";
      this.SendNextP3Order.TranslateText = false;
      this.SendNextP3Order.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextP3Order.UserDefinedActiveImage")));
      this.SendNextP3Order.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextP3Order.UserDefinedImage")));
      this.SendNextP3Order.Warned = false;
      this.SendNextP3Order.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendNextP2Order
      // 
      this.SendNextP2Order.Active = false;
      this.SendNextP2Order.Alarmed = false;
      this.SendNextP2Order.Auto = false;
      this.SendNextP2Order.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextP2Order.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextP2Order.FillBackColorWithImage = true;
      this.SendNextP2Order.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextP2Order.ForeColor = System.Drawing.Color.Black;
      this.SendNextP2Order.FramePresent = true;
      this.SendNextP2Order.HighLightEnable = false;
      this.SendNextP2Order.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextP2Order.ImagePassword = null;
      this.SendNextP2Order.Inemergency = false;
      this.SendNextP2Order.InLocal = false;
      this.SendNextP2Order.IsButton = true;
      this.SendNextP2Order.IsForceToInvisible = false;
      this.SendNextP2Order.Location = new System.Drawing.Point(769, 253);
      this.SendNextP2Order.Name = "SendNextP2Order";
      this.SendNextP2Order.OnLine = false;
      this.SendNextP2Order.ShowImage = true;
      this.SendNextP2Order.ShowText = true;
      this.SendNextP2Order.Size = new System.Drawing.Size(120, 90);
      this.SendNextP2Order.Switching = false;
      this.SendNextP2Order.TabIndex = 206;
      this.SendNextP2Order.Text = "4";
      this.SendNextP2Order.TranslateText = false;
      this.SendNextP2Order.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextP2Order.UserDefinedActiveImage")));
      this.SendNextP2Order.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextP2Order.UserDefinedImage")));
      this.SendNextP2Order.Warned = false;
      this.SendNextP2Order.Click += new System.EventHandler(this.OnButtonPush);
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
      this.SendNextP1Order.Location = new System.Drawing.Point(556, 253);
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
      this.SendNextOrder.Location = new System.Drawing.Point(343, 253);
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
      this.SendCurrentOrder.Location = new System.Drawing.Point(130, 253);
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
      // Frm_SlitterScorerConsole
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_manualSendButtons);
      this.Controls.Add(this.SendNextP3Order);
      this.Controls.Add(this.SendNextP2Order);
      this.Controls.Add(this.SendNextP1Order);
      this.Controls.Add(this.SendNextOrder);
      this.Controls.Add(this.SendCurrentOrder);
      this.Name = "Frm_SlitterScorerConsole";
      this.Load += new System.EventHandler(this.Frm_SlitterScorerConsole_Load);
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
      this.Controls.SetChildIndex(this.SendNextP2Order, 0);
      this.Controls.SetChildIndex(this.SendNextP3Order, 0);
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
		/// <summary>
		/// Handles event raised by generic button that are in the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.SLITTER_SCORER + "." + ((Control)sender).Name + "()");
		}

		private void Frm_SlitterScorerConsole_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
			this.SendNextP1Order.Alarmed = isError;
			this.SendNextP2Order.Alarmed = isError;
			this.SendNextP3Order.Alarmed = isError;
		}
	}
}

