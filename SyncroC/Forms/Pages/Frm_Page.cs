using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;  

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Forms;
using SyncroC.Forms.StatusBar;

namespace SyncroC.Forms.Pages
{
	public class Frm_Page : SyncroC.Forms.Frm_Base
	{   
		/// <summary>
		/// Size the page has been designed.
		/// </summary>
		public static readonly Size ORIGINAL_SIZE = new Size(1280, 738);

		/// <summary>System.Windows.Forms.Message WParam</summary>
		private const int WM_LBUTTONDOWN = 0x0201;
		/// <summary>System.Windows.Forms.Message Msg</summary>
		private const int WM_PARENTNOTIFY = 0x210;

    private const int BORDER = 24;

    protected System.Windows.Forms.Label lbl_Title_YN;
		protected System.Windows.Forms.Label lbl_Time;
		protected System.Windows.Forms.Label lbl_Date;
		public SyncroC.Controls.myLabel lbl_Status;
		protected SyncroC.Controls.Tab tab_Tab1;
		protected SyncroC.Controls.Tab tab_Tab2;
		protected SyncroC.Controls.Tab tab_Tab3;
		protected SyncroC.Controls.Tab tab_Tab4;
		protected SyncroC.Controls.Tab tab_Tab5;
		protected SyncroC.Controls.Tab tab_Tab6;
		protected SyncroC.Controls.Tab tab_Tab7;
		protected SyncroC.Controls.Tab tab_Tab8;
    protected SyncroC.Controls.Tab tab_Tab9;
    protected SyncroC.Controls.Tab tab_TabA;
		protected SyncroC.Controls.Tab tab_TabD;
		protected SyncroC.Controls.Tab tab_TabC;
    protected SyncroC.Controls.Tab tab_TabB;
		protected System.Windows.Forms.PictureBox pct_Home_NY;
		protected System.Windows.Forms.PictureBox pct_Back_NY;
		protected System.Windows.Forms.PictureBox pct_Forward_NY;
		protected System.Windows.Forms.Label lbl_CtrlF3;
		protected System.Windows.Forms.Label lbl_CtrlF5;
		protected System.Windows.Forms.Label lbl_CtrlF2;
		protected System.Windows.Forms.Label lbl_CtrlF1;
		protected System.Windows.Forms.Label lbl_CtrlF4;
		protected System.Windows.Forms.Label lbl_CtrlF4_YY;
		protected System.Windows.Forms.Label lbl_CtrlF1_YY;
		protected System.Windows.Forms.Label lbl_CtrlF2_YY;
		protected System.Windows.Forms.Label lbl_CtrlF5_YY;
		protected System.Windows.Forms.Label lbl_CtrlF3_YY;
		protected System.Windows.Forms.ImageList iml_navigationImages;
		protected SyncroC.Controls.StateButton btn_Group1;
		protected SyncroC.Controls.StateButton btn_Group2;
    protected SyncroC.Controls.StateButton btn_Group3;
		protected SyncroC.Controls.StateButton btn_Group5;
		private System.Windows.Forms.PictureBox pct_FosberLogo;
    protected Point internalLeftHigh, internalLeftLow, internalRightHigh, internalRightLow;
    protected Point leftHigh, leftLow, rightHigh, rightLow;

    private System.ComponentModel.IContainer components = null;
		private PictureBox pct_OnServer;
    private PictureBox pct_FosberLogoAlto;

		/// <summary>Determine the visibility of the Debug Group pages</summary>
		private static bool debugGroupVisibility;

		/// <summary>
		/// Builder.
		/// </summary>
		public Frm_Page()
		{
			InitializeComponent();
		
			if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
				this.pct_OnServer.Visible = true;
		}
	
		/// <summary>
		/// set the bindings for time and date
		/// NOTE: to be called only when the program is running
		/// </summary>
		private void setBindings()
		{		
			PageManager pm = PageManager.getPageManager();			
			try
			{
				lbl_Time.DataBindings.Add(
					new Binding("Text", pm.lbl_Time, "Text"));
				lbl_Date.DataBindings.Add(
					new Binding("Text", pm.lbl_Date, "Text"));
				lbl_Status.DataBindings.Add(
					new Binding("Testo", pm.lbl_Message_NY, "Text"));
        lbl_Status.DataBindings.Add(
          new Binding("Colore1", pm.lbl_Message_NY, "ForeColor"));
			}
			catch (ArgumentNullException ex)
			{
				Logger.LogError(this, "setBindings - one of the binding with the pageManager was null and failed", ex);
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "setBindings - one of the binding with the pageManager failed", ex);
			}

			//BINDING AUTOMATICO CON OGGETTO NON GRAFICO NON FUNZIONA QUINDI VIENE GENERATO UN EVENTO
			//SUL CAMBIAMENTO VALORE DELLA PROPERTY DI RTC A CUI SI VOLEVA REALIZZARE IL BINDING, QUA
			//SI CATTURA L'EVENTO, SI CONTROLLA CHE SIA RELATIVO ALLA PROPERTY DI NOSTRO INTERESSE E
			//SI ASSEGNA IL NUOVO VALORE DELLA PROPERTY (SI TROVA IN ARGS) AL CONTROLLO (VEDI METODO
			//"Frm_Page_OnRTCManagerPropertyChanged"

			RTCManager rtcM = RTCManager.getRTCManager();
			//try
			//{
			//  btn_Group2.DataBindings.Add(
			//    new Binding("Alarmed", rtcM, "IsIdleShift"));
			//}
			//catch (ArgumentNullException ex)
			//{
			//  Logger.LogError(this, "setBindings - one of the binding with the RTCManager was null and failed", ex);
			//}
			//catch (Exception ex)
			//{
			//  Logger.LogError(this, "setBindings - one of the binding with the RTCManager failed", ex);
			//}

      btn_Group1.Alarmed = (rtcM.getAlarmPresentOnMachine(RTCManager.SYC_SW) > 0) || (rtcM.getAlarmPresentOnMachine(RTCManager.SYC_HW) > 0);
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro.       
      btn_Group1.Warned = (rtcM.getWarningPresentOnMachine(RTCManager.SYC_SW) > 0) || (rtcM.getWarningPresentOnMachine(RTCManager.SYC_HW) > 0);

			btn_Group2.Alarmed = rtcM.IsIdleShift;
			rtcM.OnRTCManagerPropertyChanged += new RTCManagerPropertyChangedHandler(Frm_Page_OnRTCManagerPropertyChanged);
    }

    //ABELLO_FI04/11/2008_1 Aggiunta la PopUp per il cambio di Data e Ora.
    private void lbl_TimeAndDate_Click(object sender, EventArgs e)
    {
      this.setUpTimeZone();
      if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift)
      {
        SyncroC.Forms.PopUp.Frm_TimeSetup ts = new SyncroC.Forms.PopUp.Frm_TimeSetup();
        FosberDialogResult ret = ts.ShowDialog(this,"");

        if (ret.Dialog == DialogResult.OK)
        {
          //qui modificare l'orario di sistema
          try
          {
            if (ret.Data[0] != null)
            {
              SyncroC.Utility.TimeZoneInformation.SYSTEMTIME lpSystemTime = (SyncroC.Utility.TimeZoneInformation.SYSTEMTIME)ret.Data[0];
              //N.B. the System Time is the GMT orario
              if(RTCManager.getRTCManager().IsIdleShift)
                SyncroC.Utility.TimeZoneInformation.NativeMethods.SetSystemTime(ref lpSystemTime);
            }

            if (ret.Data[1] != null)
            {
              SyncroC.Utility.TimeZoneInformation.TIME_ZONE_INFORMATION lpTimeZoneInformation = (SyncroC.Utility.TimeZoneInformation.TIME_ZONE_INFORMATION)ret.Data[1];
              //Qui ora va settata
              if (RTCManager.getRTCManager().IsIdleShift)
              {
                SyncroC.Utility.TimeZoneInformation.NativeMethods.SetTimeZoneInformation(ref lpTimeZoneInformation);
                System.Globalization. CultureInfo.CurrentCulture.ClearCachedData();
              }
            }
          }
          catch (Exception ex)
          {
            SyncroC.Utility.Logger.LogError(this, "Manual SetDateTime - CANNOT SET DATE TIME ", ex);
          }
        }
      }
    }

    private void setUpTimeZone()
    {
      if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift)
      {
        this.lbl_Time.Cursor = Cursors.Hand;
        this.lbl_Date.Cursor = Cursors.Hand;
      }
      else
      {
        this.lbl_Time.Cursor = Cursors.Arrow;
        this.lbl_Date.Cursor = Cursors.Arrow;
      }
    }

		/// <summary>
		/// Manual binding...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void Frm_Page_OnRTCManagerPropertyChanged(object sender, RTCManagerPropertyChangedEventArgs args)
		{
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
      if (args.propertyName.Equals(RTCManager.SYC_SW + "Warnings") || args.propertyName.Equals(RTCManager.SYC_HW + "Warnings"))
        btn_Group1.Warned = ((int)(args.newValue)) > 0;
      if (args.propertyName.Equals(RTCManager.SYC_SW + "Alarms") || args.propertyName.Equals(RTCManager.SYC_HW + "Alarms"))
				btn_Group1.Alarmed = ((int)(args.newValue)) > 0;
			else if (args.propertyName.Equals("IsIdleShift"))
				btn_Group2.Alarmed = (bool)(args.newValue);

      this.setUpTimeZone();
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
			if (lbl_Time != null)
				lbl_Time.DataBindings.Clear();
			if (lbl_Date != null)
				lbl_Date.DataBindings.Clear();
			if (lbl_Status != null)
				lbl_Status.DataBindings.Clear();
			base.Dispose( disposing );
		}

		/// <summary>
		/// Toggle debug information visibility
		/// </summary>
		protected override void ControlAltD()
		{
			debugGroupVisibility = ! debugGroupVisibility;
			this.ShiftFKeyEnable[(int)FKEYS.F5] = this.btn_Group5.Visible = debugGroupVisibility;
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Page));
      this.lbl_Title_YN = new System.Windows.Forms.Label();
      this.lbl_Time = new System.Windows.Forms.Label();
      this.lbl_Date = new System.Windows.Forms.Label();
      this.lbl_Status = new SyncroC.Controls.myLabel();
      this.tab_Tab1 = new SyncroC.Controls.Tab();
      this.tab_Tab2 = new SyncroC.Controls.Tab();
      this.tab_Tab3 = new SyncroC.Controls.Tab();
      this.tab_Tab4 = new SyncroC.Controls.Tab();
      this.tab_Tab5 = new SyncroC.Controls.Tab();
      this.tab_Tab6 = new SyncroC.Controls.Tab();
      this.tab_Tab7 = new SyncroC.Controls.Tab();
      this.tab_Tab8 = new SyncroC.Controls.Tab();
      this.tab_Tab9 = new SyncroC.Controls.Tab();
      this.tab_TabA = new SyncroC.Controls.Tab();
      this.tab_TabD = new SyncroC.Controls.Tab();
      this.tab_TabC = new SyncroC.Controls.Tab();
      this.tab_TabB = new SyncroC.Controls.Tab();
      this.pct_Home_NY = new System.Windows.Forms.PictureBox();
      this.pct_Back_NY = new System.Windows.Forms.PictureBox();
      this.pct_Forward_NY = new System.Windows.Forms.PictureBox();
      this.lbl_CtrlF3 = new System.Windows.Forms.Label();
      this.lbl_CtrlF5 = new System.Windows.Forms.Label();
      this.lbl_CtrlF2 = new System.Windows.Forms.Label();
      this.lbl_CtrlF1 = new System.Windows.Forms.Label();
      this.lbl_CtrlF4 = new System.Windows.Forms.Label();
      this.lbl_CtrlF4_YY = new System.Windows.Forms.Label();
      this.lbl_CtrlF1_YY = new System.Windows.Forms.Label();
      this.lbl_CtrlF2_YY = new System.Windows.Forms.Label();
      this.lbl_CtrlF5_YY = new System.Windows.Forms.Label();
      this.lbl_CtrlF3_YY = new System.Windows.Forms.Label();
      this.iml_navigationImages = new System.Windows.Forms.ImageList(this.components);
      this.btn_Group1 = new SyncroC.Controls.StateButton();
      this.btn_Group2 = new SyncroC.Controls.StateButton();
      this.btn_Group3 = new SyncroC.Controls.StateButton();
      this.pct_FosberLogo = new System.Windows.Forms.PictureBox();
      this.btn_Group5 = new SyncroC.Controls.StateButton();
      this.pct_OnServer = new System.Windows.Forms.PictureBox();
      this.pct_FosberLogoAlto = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_FosberLogo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OnServer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_FosberLogoAlto)).BeginInit();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_Title_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Title_YN.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Title_YN.Location = new System.Drawing.Point(6, 138);
      this.lbl_Title_YN.Name = "lbl_Title_YN";
      this.lbl_Title_YN.Size = new System.Drawing.Size(616, 30);
      this.lbl_Title_YN.TabIndex = 18;
      this.lbl_Title_YN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.Silver;
      this.lbl_Time.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Time.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Time.Location = new System.Drawing.Point(1206, 24);
      this.lbl_Time.Name = "lbl_Time";
      this.lbl_Time.Size = new System.Drawing.Size(70, 14);
      this.lbl_Time.TabIndex = 36;
      this.lbl_Time.Text = "12:24:51";
      this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_Time.Click += new System.EventHandler(this.lbl_TimeAndDate_Click);
      // 
      // lbl_Date
      // 
      this.lbl_Date.BackColor = System.Drawing.Color.Silver;
      this.lbl_Date.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Date.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Date.Location = new System.Drawing.Point(1206, 40);
      this.lbl_Date.Name = "lbl_Date";
      this.lbl_Date.Size = new System.Drawing.Size(70, 14);
      this.lbl_Date.TabIndex = 37;
      this.lbl_Date.Text = "30 08 76";
      this.lbl_Date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_Date.Click += new System.EventHandler(this.lbl_TimeAndDate_Click);
      // 
      // lbl_Status
      // 
      this.lbl_Status.BackColor = System.Drawing.Color.Silver;
      this.lbl_Status.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_Status.Colore1 = System.Drawing.Color.White;
      this.lbl_Status.Colore2 = System.Drawing.Color.White;
      this.lbl_Status.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Status.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Status.Location = new System.Drawing.Point(228, 24);
      this.lbl_Status.Name = "lbl_Status";
      this.lbl_Status.OutLineText = true;
      this.lbl_Status.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_Status.Size = new System.Drawing.Size(711, 30);
      this.lbl_Status.TabIndex = 67;
      this.lbl_Status.Testo = "";
      this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_Status.TranslateText = false;
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab1.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab1.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab1.Image = null;
      this.tab_Tab1.Location = new System.Drawing.Point(4, 82);
      this.tab_Tab1.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab1.Name = "tab_Tab1";
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab1.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab1.TabIndex = 68;
      this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab1.TabStop = false;
      this.tab_Tab1.TranslateText = true;
      this.tab_Tab1.Vertical = false;
      this.tab_Tab1.Visible = false;
      this.tab_Tab1.Click += new System.EventHandler(this.tab_Tab1_Click);
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab2.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab2.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab2.Image = null;
      this.tab_Tab2.Location = new System.Drawing.Point(140, 82);
      this.tab_Tab2.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab2.Name = "tab_Tab2";
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab2.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab2.TabIndex = 69;
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab2.TabStop = false;
      this.tab_Tab2.TranslateText = true;
      this.tab_Tab2.Vertical = false;
      this.tab_Tab2.Visible = false;
      this.tab_Tab2.Click += new System.EventHandler(this.tab_Tab2_Click);
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab3.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab3.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab3.Image = null;
      this.tab_Tab3.Location = new System.Drawing.Point(276, 82);
      this.tab_Tab3.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab3.Name = "tab_Tab3";
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab3.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab3.TabIndex = 70;
      this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab3.TabStop = false;
      this.tab_Tab3.TranslateText = true;
      this.tab_Tab3.Vertical = false;
      this.tab_Tab3.Visible = false;
      this.tab_Tab3.Click += new System.EventHandler(this.tab_Tab3_Click);
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab4.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab4.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab4.Image = null;
      this.tab_Tab4.Location = new System.Drawing.Point(412, 82);
      this.tab_Tab4.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab4.Name = "tab_Tab4";
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab4.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab4.TabIndex = 71;
      this.tab_Tab4.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab4.TabStop = false;
      this.tab_Tab4.TranslateText = true;
      this.tab_Tab4.Vertical = false;
      this.tab_Tab4.Visible = false;
      this.tab_Tab4.Click += new System.EventHandler(this.tab_Tab4_Click);
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab5.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab5.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab5.Image = null;
      this.tab_Tab5.Location = new System.Drawing.Point(548, 82);
      this.tab_Tab5.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab5.Name = "tab_Tab5";
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab5.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab5.TabIndex = 72;
      this.tab_Tab5.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab5.TabStop = false;
      this.tab_Tab5.TranslateText = true;
      this.tab_Tab5.Vertical = false;
      this.tab_Tab5.Visible = false;
      this.tab_Tab5.Click += new System.EventHandler(this.tab_Tab5_Click);
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab6.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab6.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab6.Image = null;
      this.tab_Tab6.Location = new System.Drawing.Point(684, 82);
      this.tab_Tab6.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab6.Name = "tab_Tab6";
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab6.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab6.TabIndex = 73;
      this.tab_Tab6.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab6.TabStop = false;
      this.tab_Tab6.TranslateText = true;
      this.tab_Tab6.Vertical = false;
      this.tab_Tab6.Visible = false;
      this.tab_Tab6.Click += new System.EventHandler(this.tab_Tab6_Click);
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab7.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab7.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab7.Image = null;
      this.tab_Tab7.Location = new System.Drawing.Point(820, 82);
      this.tab_Tab7.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab7.Name = "tab_Tab7";
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab7.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab7.TabIndex = 74;
      this.tab_Tab7.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab7.TabStop = false;
      this.tab_Tab7.TranslateText = true;
      this.tab_Tab7.Vertical = false;
      this.tab_Tab7.Visible = false;
      this.tab_Tab7.Click += new System.EventHandler(this.tab_Tab7_Click);
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab8.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab8.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab8.Image = null;
      this.tab_Tab8.Location = new System.Drawing.Point(956, 82);
      this.tab_Tab8.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab8.Name = "tab_Tab8";
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab8.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab8.TabIndex = 75;
      this.tab_Tab8.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab8.TabStop = false;
      this.tab_Tab8.TranslateText = true;
      this.tab_Tab8.Vertical = false;
      this.tab_Tab8.Visible = false;
      this.tab_Tab8.Click += new System.EventHandler(this.tab_Tab8_Click);
      // 
      // tab_Tab9
      // 
      this.tab_Tab9.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_Tab9.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Tab9.ForeColor = System.Drawing.Color.Black;
      this.tab_Tab9.Image = null;
      this.tab_Tab9.Location = new System.Drawing.Point(1093, 82);
      this.tab_Tab9.Margin = new System.Windows.Forms.Padding(2);
      this.tab_Tab9.Name = "tab_Tab9";
      this.tab_Tab9.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_Tab9.Size = new System.Drawing.Size(132, 50);
      this.tab_Tab9.TabIndex = 112;
      this.tab_Tab9.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_Tab9.TabStop = false;
      this.tab_Tab9.TranslateText = true;
      this.tab_Tab9.Vertical = false;
      this.tab_Tab9.Visible = false;
      this.tab_Tab9.Click += new System.EventHandler(this.tab_Tab9_Click);
      // 
      // tab_TabA
      // 
      this.tab_TabA.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_TabA.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_TabA.ForeColor = System.Drawing.Color.Black;
      this.tab_TabA.Image = null;
      this.tab_TabA.Location = new System.Drawing.Point(1226, 168);
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(2);
      this.tab_TabA.Name = "tab_TabA";
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_TabA.Size = new System.Drawing.Size(50, 139);
      this.tab_TabA.TabIndex = 76;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.TabStop = false;
      this.tab_TabA.TranslateText = true;
      this.tab_TabA.Vertical = true;
      this.tab_TabA.Visible = false;
      this.tab_TabA.Click += new System.EventHandler(this.tab_TabA_Click);
      // 
      // tab_TabD
      // 
      this.tab_TabD.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_TabD.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_TabD.ForeColor = System.Drawing.Color.Black;
      this.tab_TabD.Image = null;
      this.tab_TabD.Location = new System.Drawing.Point(1226, 598);
      this.tab_TabD.Margin = new System.Windows.Forms.Padding(2);
      this.tab_TabD.Name = "tab_TabD";
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_TabD.Size = new System.Drawing.Size(50, 139);
      this.tab_TabD.TabIndex = 77;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabD.TabStop = false;
      this.tab_TabD.TranslateText = true;
      this.tab_TabD.Vertical = true;
      this.tab_TabD.Visible = false;
      this.tab_TabD.Click += new System.EventHandler(this.tab_TabD_Click);
      // 
      // tab_TabC
      // 
      this.tab_TabC.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_TabC.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_TabC.ForeColor = System.Drawing.Color.Black;
      this.tab_TabC.Image = null;
      this.tab_TabC.Location = new System.Drawing.Point(1226, 454);
      this.tab_TabC.Margin = new System.Windows.Forms.Padding(2);
      this.tab_TabC.Name = "tab_TabC";
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_TabC.Size = new System.Drawing.Size(50, 139);
      this.tab_TabC.TabIndex = 78;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabC.TabStop = false;
      this.tab_TabC.TranslateText = true;
      this.tab_TabC.Vertical = true;
      this.tab_TabC.Visible = false;
      this.tab_TabC.Click += new System.EventHandler(this.tab_TabC_Click);
      // 
      // tab_TabB
      // 
      this.tab_TabB.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tab_TabB.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_TabB.ForeColor = System.Drawing.Color.Black;
      this.tab_TabB.Image = null;
      this.tab_TabB.Location = new System.Drawing.Point(1226, 311);
      this.tab_TabB.Margin = new System.Windows.Forms.Padding(2);
      this.tab_TabB.Name = "tab_TabB";
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.Empty;
      this.tab_TabB.Size = new System.Drawing.Size(50, 139);
      this.tab_TabB.TabIndex = 79;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabB.TabStop = false;
      this.tab_TabB.TranslateText = true;
      this.tab_TabB.Vertical = true;
      this.tab_TabB.Visible = false;
      this.tab_TabB.Click += new System.EventHandler(this.tab_TabB_Click);
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.BackColor = System.Drawing.Color.Silver;
      this.pct_Home_NY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_Home_NY.Location = new System.Drawing.Point(1106, 24);
      this.pct_Home_NY.Name = "pct_Home_NY";
      this.pct_Home_NY.Size = new System.Drawing.Size(98, 30);
      this.pct_Home_NY.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Home_NY.TabIndex = 89;
      this.pct_Home_NY.TabStop = false;
      this.pct_Home_NY.Click += new System.EventHandler(this.pct_Home_NY_Click);
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.BackColor = System.Drawing.Color.Silver;
      this.pct_Back_NY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_Back_NY.Location = new System.Drawing.Point(942, 24);
      this.pct_Back_NY.Name = "pct_Back_NY";
      this.pct_Back_NY.Size = new System.Drawing.Size(80, 30);
      this.pct_Back_NY.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Back_NY.TabIndex = 90;
      this.pct_Back_NY.TabStop = false;
      this.pct_Back_NY.Click += new System.EventHandler(this.pct_Back_NY_Click);
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.BackColor = System.Drawing.Color.Silver;
      this.pct_Forward_NY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_Forward_NY.Location = new System.Drawing.Point(1024, 24);
      this.pct_Forward_NY.Name = "pct_Forward_NY";
      this.pct_Forward_NY.Size = new System.Drawing.Size(80, 30);
      this.pct_Forward_NY.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Forward_NY.TabIndex = 91;
      this.pct_Forward_NY.TabStop = false;
      this.pct_Forward_NY.Click += new System.EventHandler(this.pct_Forward_NY_Click);
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CtrlF3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF3.Location = new System.Drawing.Point(864, 138);
      this.lbl_CtrlF3.Name = "lbl_CtrlF3";
      this.lbl_CtrlF3.Size = new System.Drawing.Size(118, 12);
      this.lbl_CtrlF3.TabIndex = 93;
      this.lbl_CtrlF3.Text = "Ctrl F3";
      this.lbl_CtrlF3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF3.Click += new System.EventHandler(this.lbl_CtrlF3_Click);
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF5.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CtrlF5.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF5.Location = new System.Drawing.Point(1104, 138);
      this.lbl_CtrlF5.Name = "lbl_CtrlF5";
      this.lbl_CtrlF5.Size = new System.Drawing.Size(120, 12);
      this.lbl_CtrlF5.TabIndex = 94;
      this.lbl_CtrlF5.Text = "Ctrl F5";
      this.lbl_CtrlF5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF5.Click += new System.EventHandler(this.lbl_CtrlF5_Click);
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CtrlF2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF2.Location = new System.Drawing.Point(744, 138);
      this.lbl_CtrlF2.Name = "lbl_CtrlF2";
      this.lbl_CtrlF2.Size = new System.Drawing.Size(118, 12);
      this.lbl_CtrlF2.TabIndex = 95;
      this.lbl_CtrlF2.Text = "Ctrl F2";
      this.lbl_CtrlF2.Click += new System.EventHandler(this.lbl_CtrlF2_Click);
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CtrlF1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF1.Location = new System.Drawing.Point(624, 138);
      this.lbl_CtrlF1.Name = "lbl_CtrlF1";
      this.lbl_CtrlF1.Size = new System.Drawing.Size(118, 12);
      this.lbl_CtrlF1.TabIndex = 96;
      this.lbl_CtrlF1.Text = "Ctrl F1";
      this.lbl_CtrlF1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF1.Click += new System.EventHandler(this.lbl_CtrlF1_Click);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF4.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CtrlF4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF4.Location = new System.Drawing.Point(984, 138);
      this.lbl_CtrlF4.Name = "lbl_CtrlF4";
      this.lbl_CtrlF4.Size = new System.Drawing.Size(118, 12);
      this.lbl_CtrlF4.TabIndex = 97;
      this.lbl_CtrlF4.Text = "Ctrl F4";
      this.lbl_CtrlF4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF4.Click += new System.EventHandler(this.lbl_CtrlF4_Click);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF4_YY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF4_YY.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_CtrlF4_YY.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF4_YY.Location = new System.Drawing.Point(984, 150);
      this.lbl_CtrlF4_YY.Name = "lbl_CtrlF4_YY";
      this.lbl_CtrlF4_YY.Size = new System.Drawing.Size(118, 18);
      this.lbl_CtrlF4_YY.TabIndex = 102;
      this.lbl_CtrlF4_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF4_YY.Click += new System.EventHandler(this.lbl_CtrlF4_Click);
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF1_YY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF1_YY.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_CtrlF1_YY.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF1_YY.Location = new System.Drawing.Point(624, 150);
      this.lbl_CtrlF1_YY.Name = "lbl_CtrlF1_YY";
      this.lbl_CtrlF1_YY.Size = new System.Drawing.Size(118, 18);
      this.lbl_CtrlF1_YY.TabIndex = 101;
      this.lbl_CtrlF1_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF1_YY.Click += new System.EventHandler(this.lbl_CtrlF1_Click);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF2_YY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF2_YY.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_CtrlF2_YY.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF2_YY.Location = new System.Drawing.Point(744, 150);
      this.lbl_CtrlF2_YY.Name = "lbl_CtrlF2_YY";
      this.lbl_CtrlF2_YY.Size = new System.Drawing.Size(118, 18);
      this.lbl_CtrlF2_YY.TabIndex = 100;
      this.lbl_CtrlF2_YY.Click += new System.EventHandler(this.lbl_CtrlF2_Click);
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF5_YY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF5_YY.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_CtrlF5_YY.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF5_YY.Location = new System.Drawing.Point(1104, 150);
      this.lbl_CtrlF5_YY.Name = "lbl_CtrlF5_YY";
      this.lbl_CtrlF5_YY.Size = new System.Drawing.Size(120, 18);
      this.lbl_CtrlF5_YY.TabIndex = 99;
      this.lbl_CtrlF5_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF5_YY.Click += new System.EventHandler(this.lbl_CtrlF5_Click);
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lbl_CtrlF3_YY.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_CtrlF3_YY.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_CtrlF3_YY.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_CtrlF3_YY.Location = new System.Drawing.Point(864, 150);
      this.lbl_CtrlF3_YY.Name = "lbl_CtrlF3_YY";
      this.lbl_CtrlF3_YY.Size = new System.Drawing.Size(118, 18);
      this.lbl_CtrlF3_YY.TabIndex = 98;
      this.lbl_CtrlF3_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_CtrlF3_YY.Click += new System.EventHandler(this.lbl_CtrlF3_Click);
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.iml_navigationImages.ImageSize = new System.Drawing.Size(100, 30);
      this.iml_navigationImages.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // btn_Group1
      // 
      this.btn_Group1.Active = false;
      this.btn_Group1.Alarmed = false;
      this.btn_Group1.Auto = false;
      this.btn_Group1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Group1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Group1.FillBackColorWithImage = false;
      this.btn_Group1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_Group1.FramePresent = false;
      this.btn_Group1.HighLightEnable = false;
      this.btn_Group1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_Group1.ImagePassword = null;
      this.btn_Group1.Inemergency = false;
      this.btn_Group1.InLocal = false;
      this.btn_Group1.IsButton = true;
      this.btn_Group1.IsForceToInvisible = false;
      this.btn_Group1.Location = new System.Drawing.Point(0, 8);
      this.btn_Group1.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Group1.Name = "btn_Group1";
      this.btn_Group1.OnLine = false;
      this.btn_Group1.ShowImage = true;
      this.btn_Group1.ShowText = false;
      this.btn_Group1.Size = new System.Drawing.Size(72, 60);
      this.btn_Group1.Switching = false;
      this.btn_Group1.TabIndex = 105;
      this.btn_Group1.TabStop = false;
      this.btn_Group1.TranslateText = false;
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      this.btn_Group1.Warned = false;
      this.btn_Group1.Click += new System.EventHandler(this.btn_Group1_NY_Click);
      // 
      // btn_Group2
      // 
      this.btn_Group2.Active = false;
      this.btn_Group2.Alarmed = false;
      this.btn_Group2.Auto = false;
      this.btn_Group2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Group2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Group2.FillBackColorWithImage = false;
      this.btn_Group2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_Group2.FramePresent = false;
      this.btn_Group2.HighLightEnable = false;
      this.btn_Group2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_Group2.ImagePassword = null;
      this.btn_Group2.Inemergency = false;
      this.btn_Group2.InLocal = false;
      this.btn_Group2.IsButton = true;
      this.btn_Group2.IsForceToInvisible = false;
      this.btn_Group2.Location = new System.Drawing.Point(76, 8);
      this.btn_Group2.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Group2.Name = "btn_Group2";
      this.btn_Group2.OnLine = false;
      this.btn_Group2.ShowImage = true;
      this.btn_Group2.ShowText = false;
      this.btn_Group2.Size = new System.Drawing.Size(72, 60);
      this.btn_Group2.Switching = false;
      this.btn_Group2.TabIndex = 106;
      this.btn_Group2.TabStop = false;
      this.btn_Group2.TranslateText = false;
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      this.btn_Group2.Warned = false;
      this.btn_Group2.Click += new System.EventHandler(this.btn_Group2_NY_Click);
      // 
      // btn_Group3
      // 
      this.btn_Group3.Active = false;
      this.btn_Group3.Alarmed = false;
      this.btn_Group3.Auto = false;
      this.btn_Group3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Group3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Group3.FillBackColorWithImage = false;
      this.btn_Group3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_Group3.FramePresent = false;
      this.btn_Group3.HighLightEnable = false;
      this.btn_Group3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_Group3.ImagePassword = null;
      this.btn_Group3.Inemergency = false;
      this.btn_Group3.InLocal = false;
      this.btn_Group3.IsButton = true;
      this.btn_Group3.IsForceToInvisible = false;
      this.btn_Group3.Location = new System.Drawing.Point(152, 8);
      this.btn_Group3.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Group3.Name = "btn_Group3";
      this.btn_Group3.OnLine = false;
      this.btn_Group3.ShowImage = true;
      this.btn_Group3.ShowText = false;
      this.btn_Group3.Size = new System.Drawing.Size(72, 60);
      this.btn_Group3.Switching = false;
      this.btn_Group3.TabIndex = 107;
      this.btn_Group3.TabStop = false;
      this.btn_Group3.TranslateText = false;
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      this.btn_Group3.Warned = false;
      this.btn_Group3.Click += new System.EventHandler(this.btn_Group3_NY_Click);
      // 
      // pct_FosberLogo
      // 
      this.pct_FosberLogo.Location = new System.Drawing.Point(0, 719);
      this.pct_FosberLogo.Name = "pct_FosberLogo";
      this.pct_FosberLogo.Size = new System.Drawing.Size(4, 19);
      this.pct_FosberLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_FosberLogo.TabIndex = 0;
      this.pct_FosberLogo.TabStop = false;
      this.pct_FosberLogo.Visible = false;
      // 
      // btn_Group5
      // 
      this.btn_Group5.Active = false;
      this.btn_Group5.Alarmed = false;
      this.btn_Group5.Auto = false;
      this.btn_Group5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Group5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Group5.FillBackColorWithImage = false;
      this.btn_Group5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_Group5.FramePresent = false;
      this.btn_Group5.HighLightEnable = false;
      this.btn_Group5.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.btn_Group5.ImagePassword = null;
      this.btn_Group5.Inemergency = false;
      this.btn_Group5.InLocal = false;
      this.btn_Group5.IsButton = true;
      this.btn_Group5.IsForceToInvisible = false;
      this.btn_Group5.Location = new System.Drawing.Point(228, 8);
      this.btn_Group5.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Group5.Name = "btn_Group5";
      this.btn_Group5.OnLine = false;
      this.btn_Group5.ShowImage = true;
      this.btn_Group5.ShowText = false;
      this.btn_Group5.Size = new System.Drawing.Size(72, 60);
      this.btn_Group5.Switching = false;
      this.btn_Group5.TabIndex = 109;
      this.btn_Group5.TabStop = false;
      this.btn_Group5.TranslateText = false;
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      this.btn_Group5.Visible = false;
      this.btn_Group5.Warned = false;
      this.btn_Group5.Click += new System.EventHandler(this.btn_Group5_Click);
      // 
      // pct_OnServer
      // 
      this.pct_OnServer.Image = ((System.Drawing.Image)(resources.GetObject("pct_OnServer.Image")));
      this.pct_OnServer.Location = new System.Drawing.Point(1182, 4);
      this.pct_OnServer.Name = "pct_OnServer";
      this.pct_OnServer.Size = new System.Drawing.Size(18, 16);
      this.pct_OnServer.TabIndex = 110;
      this.pct_OnServer.TabStop = false;
      this.pct_OnServer.Visible = false;
      // 
      // pct_FosberLogoAlto
      // 
      this.pct_FosberLogoAlto.Image = global::SyncroC.Properties.Resources.logo_Fosber;
      this.pct_FosberLogoAlto.Location = new System.Drawing.Point(1206, 4);
      this.pct_FosberLogoAlto.Name = "pct_FosberLogoAlto";
      this.pct_FosberLogoAlto.Size = new System.Drawing.Size(70, 16);
      this.pct_FosberLogoAlto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_FosberLogoAlto.TabIndex = 111;
      this.pct_FosberLogoAlto.TabStop = false;
      // 
      // Frm_Page
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pct_FosberLogoAlto);
      this.Controls.Add(this.lbl_CtrlF1);
      this.Controls.Add(this.pct_OnServer);
      this.Controls.Add(this.btn_Group5);
      this.Controls.Add(this.pct_FosberLogo);
      this.Controls.Add(this.btn_Group3);
      this.Controls.Add(this.btn_Group2);
      this.Controls.Add(this.btn_Group1);
      this.Controls.Add(this.lbl_CtrlF4_YY);
      this.Controls.Add(this.lbl_CtrlF2_YY);
      this.Controls.Add(this.lbl_CtrlF5_YY);
      this.Controls.Add(this.lbl_CtrlF3_YY);
      this.Controls.Add(this.lbl_CtrlF4);
      this.Controls.Add(this.lbl_CtrlF2);
      this.Controls.Add(this.lbl_CtrlF5);
      this.Controls.Add(this.lbl_CtrlF3);
      this.Controls.Add(this.lbl_Title_YN);
      this.Controls.Add(this.pct_Forward_NY);
      this.Controls.Add(this.pct_Back_NY);
      this.Controls.Add(this.pct_Home_NY);
      this.Controls.Add(this.tab_TabB);
      this.Controls.Add(this.tab_TabC);
      this.Controls.Add(this.tab_TabD);
      this.Controls.Add(this.tab_TabA);
      this.Controls.Add(this.tab_Tab9);
      this.Controls.Add(this.tab_Tab8);
      this.Controls.Add(this.tab_Tab7);
      this.Controls.Add(this.tab_Tab6);
      this.Controls.Add(this.tab_Tab5);
      this.Controls.Add(this.tab_Tab4);
      this.Controls.Add(this.tab_Tab3);
      this.Controls.Add(this.tab_Tab2);
      this.Controls.Add(this.tab_Tab1);
      this.Controls.Add(this.lbl_Status);
      this.Controls.Add(this.lbl_Time);
      this.Controls.Add(this.lbl_Date);
      this.Controls.Add(this.lbl_CtrlF1_YY);
      this.DesignGridHorizontalLines = 7;
      this.DesignGridVerticalLines = 5;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "Frm_Page";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Frm_Page";
      this.Load += new System.EventHandler(this.Frm_Page_Load);
      this.Activated += new System.EventHandler(this.Frm_Page_Activated);
      this.CausesValidationChanged += new System.EventHandler(this.Frm_Page_CausesValidationChanged);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_FosberLogo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OnServer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_FosberLogoAlto)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Determine whether a form can be removed from the cache.
		/// Must be overridden by inheritors which need not to be removed in some cases
		/// </summary>
		public virtual bool IsRemovableFromCache()
		{
			return true;
		}

		/// <summary>
		/// return the position the page should take on the screen: top left corner
		/// </summary>
		/// <returns></returns>
		protected override Point getLocation()
		{
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			return new Point(myScreen.Bounds.Left, 0);
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// </summary>
		public override void _Show()
		{	

      this.lbl_CtrlF1_YY.Cursor = (this.lbl_CtrlF1_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF1.Cursor = (this.lbl_CtrlF1_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF2_YY.Cursor = (this.lbl_CtrlF2_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF2.Cursor = (this.lbl_CtrlF2_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF3_YY.Cursor = (this.lbl_CtrlF3_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF3.Cursor = (this.lbl_CtrlF3_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF4_YY.Cursor = (this.lbl_CtrlF4_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF4.Cursor = (this.lbl_CtrlF4_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF5_YY.Cursor = (this.lbl_CtrlF5_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;
      this.lbl_CtrlF5.Cursor = (this.lbl_CtrlF5_YY.Text == "") ? Cursors.Arrow : Cursors.Hand;

      this.tab_Tab1.Cursor = ((this.tab_Tab1.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab1.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab2.Cursor = ((this.tab_Tab2.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab2.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab3.Cursor = ((this.tab_Tab3.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab3.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab4.Cursor = ((this.tab_Tab4.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab4.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab5.Cursor = ((this.tab_Tab5.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab5.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab6.Cursor = ((this.tab_Tab6.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab6.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab7.Cursor = ((this.tab_Tab7.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab7.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab8.Cursor = ((this.tab_Tab8.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab8.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_Tab9.Cursor = ((this.tab_Tab9.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_Tab9.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;

      this.tab_TabA.Cursor = ((this.tab_TabA.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_TabA.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_TabB.Cursor = ((this.tab_TabB.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_TabB.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_TabC.Cursor = ((this.tab_TabC.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_TabC.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;
      this.tab_TabD.Cursor = ((this.tab_TabD.TabSelection == SyncroC.Controls.TabSelectionType.Selected) || (this.tab_TabD.TabSelection == SyncroC.Controls.TabSelectionType.SelectedAlarm)) ? Cursors.Arrow : Cursors.Hand;

			base._Show();
		}

		/// <summary>
		/// return the "run time" page's size 
		/// </summary>
		/// <returns></returns>
		protected override Size getSize()
		{
			if (Accessibility.getAccessibility().ConsoleEnabled)
			{
				return new Size(
					//width
						this.getScreenSize().Width,
					Convert.ToInt16(
					//height
						((this.getOriginalSize().Height * this.getScreenSize().Height) /
						(this.getOriginalSize().Height + Frm_StatusBar.ORIGINAL_SIZE.Height)) + 1));
			}
			else
			{
				return new Size(
					//width
						this.getScreenSize().Width,
					Convert.ToInt16(
					//height
						((this.getOriginalSize().Height * this.getScreenSize().Height) /
						(this.getOriginalSize().Height + Frm_StatusBar.ORIGINAL_SIZE.Height)) + getConsoleDisabledOffset()));
			}
		}

		/// <summary>
		/// return the "design time" page's size
		/// </summary>
		/// <returns></returns>
		protected override Size getOriginalSize()
		{
			return ORIGINAL_SIZE;
		}

		/// <summary>
		/// Home key behaviour.
		/// </summary>
		protected override void Home()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.FirstPage);
      //agrassi_FI25-08-08
      //agrassi_FI23-09-08
      PageManager pm = PageManager.getPageManager(); 
      SyncroC.Utility.Settings.Configuration conf = SyncroC.Utility.Settings.getSettings().configuration;
      SyncroC.Utility.Settings.Forms forms = SyncroC.Utility.Settings.getSettings().forms;
      //if (SyncroC.Utility.Settings.getSettings().configuration.IsSyncro400())
      if ((SyncroC.Utility.Settings.getSettings().configuration.IsSyncro400() &&
          (!conf.getOnServer()) && (forms.FirstPage == "twin400_console")) ||
          SyncroC.Utility.Settings.getSettings().configuration.IsPmac400())
      {
        //PageManager pm = PageManager.getPageManager();
        //SyncroC.Utility.Settings.Configuration conf = SyncroC.Utility.Settings.getSettings().configuration;
        //SyncroC.Utility.Settings.Forms forms = SyncroC.Utility.Settings.getSettings().forms;
        if ((!conf.getOnServer()) && (forms.FirstPage == "twin400_console"))
        {
          if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarTwin400)))
            askForPage(typeof(Frm_StatusBarTwin400));
        }
      }
      //ABELLO_FI05/05/09_1 - questo da parecchia noia sui cambi di pagina
      //agrassi_FI23-09-08
      //else if (forms.FirstPage == "production_homeWET")
      //{
      //  if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
      //    askForPage(typeof(Frm_StatusBarWetEnd));
      //}
      //else
      //{
      //  if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
      //    askForPage(typeof(Frm_StatusBarDryEnd));
      //}
      //}
    }

		#region Click ClickPreview handles
		//Home
		/// <summary>Handle definition for pct_Home_NY click.</summary>
		private void pct_Home_NY_Click(object sender, System.EventArgs e)
		{
			Home();
		}

		//Back Forward
		/// <summary>
		/// Handle for pct_Back_NY click.
		/// </summary>
		private void pct_Back_NY_Click(object sender, System.EventArgs e)
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			pm.pagesHistorian.BackPressed();
			string backPage = pm.pagesHistorian.BackPage();
			if (! backPage.Equals(""))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, backPage);
		}
		/// <summary>
		/// Handle for pct_Forward_NY click.
		/// </summary>
		private void pct_Forward_NY_Click(object sender, System.EventArgs e)
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			pm.pagesHistorian.ForwardPressed();
			string forwardPage = pm.pagesHistorian.ForwardPage();
			if (! forwardPage.Equals(""))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, forwardPage);
		}

		//Tabs
		/// <summary>Handle for tab_Tab1 click.</summary>
		private void tab_Tab1_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F1]) F1();
		}
		/// <summary>Handle for tab_Tab2 click.</summary>
		private void tab_Tab2_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F2]) F2();
		}
		/// <summary>Handle for tab_Tab3 click.</summary>
		private void tab_Tab3_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F3]) F3();
		}
		/// <summary>Handle for tab_Tab4 click.</summary>
		private void tab_Tab4_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F4]) F4();
		}
		/// <summary>Handle for tab_Tab5 click.</summary>
		private void tab_Tab5_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F5]) F5();
		}
		/// <summary>Handle for tab_Tab6 click.</summary>
		private void tab_Tab6_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F6]) F6();
		}
		/// <summary>Handle for tab_Tab7 click.</summary>
		private void tab_Tab7_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F7]) F7();
		}
		/// <summary>Handle for tab_Tab8 click.</summary>
		private void tab_Tab8_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F8]) F8();
		}
    /// <summary>Handle for tab_Tab8 click.</summary>
    private void tab_Tab9_Click(object sender, System.EventArgs e)
    {
      if (this.FKeyEnable[(int)FKEYS.F9]) F9();
    }		/// <summary>Handle for tab_TabA click.</summary>
		private void tab_TabA_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F10]) F10();
		}
		/// <summary>Handle for tab_TabB click.</summary>
		private void tab_TabB_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F11]) F11();
		}
		/// <summary>Handle for tab_TabC click.</summary>
		private void tab_TabC_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F12]) F12();
		}
		/// <summary>Handle for tab_TabD click.</summary>
		private void tab_TabD_Click(object sender, System.EventArgs e)
		{
			if (this.FKeyEnable[(int)FKEYS.F13]) F13();
		}
		//page grup buttons
		/// <summary>Handle for btn_Group1_NY clickPreview.</summary>
		private void btn_Group1_NY_ClickPreview()
		{
			this.CausesValidation = false;
		}
		/// <summary>Handle for btn_Group2_NY clickPreview.</summary>
		private void btn_Group2_NY_ClickPreview()
		{
			this.CausesValidation = false;
		}
		/// <summary>Handle for btn_Group3_NY clickPreview.</summary>
		private void btn_Group3_NY_ClickPreview()
		{
			this.CausesValidation = false;
		}
		/// <summary>Handle for btn_Group4_NY clickPreview.</summary>
		private void btn_Group4_NY_ClickPreview()
		{
			this.CausesValidation = false;
		}
		/// <summary>Handle for btn_Group5_NY clickPreview.</summary>
		private void btn_Group5_NY_ClickPreview()
		{
			this.CausesValidation = false;
		}
		/// <summary>Handle for btn_Group6_NY clickPreview.</summary>
		private void btn_Group6_NY_ClickPreview()
		{
			this.CausesValidation = false;
		}

		/// <summary>Handle for btn_Group1_NY click.</summary>
		private void btn_Group1_NY_Click(object sender, System.EventArgs e)
		{
			if (this.ShiftFKeyEnable[(int)FKEYS.F1]) ShiftF1();
		}
		/// <summary>Handle for btn_Group2_NY click.</summary>
		private void btn_Group2_NY_Click(object sender, System.EventArgs e)
		{
			if (this.ShiftFKeyEnable[(int)FKEYS.F2]) ShiftF2();
		}
		/// <summary>Handle for btn_Group3_NY click.</summary>
		private void btn_Group3_NY_Click(object sender, System.EventArgs e)
		{
			if (this.ShiftFKeyEnable[(int)FKEYS.F3]) ShiftF3();
		}
		/// <summary>Handle for btn_Group4_NY click.</summary>
		private void btn_Group4_NY_Click(object sender, System.EventArgs e)
		{
			if (this.ShiftFKeyEnable[(int)FKEYS.F4]) ShiftF4();
		}
		/// <summary>Handle for btn_Group5_NY click.</summary>
		private void btn_Group5_Click(object sender, System.EventArgs e)
		{
			if (this.ShiftFKeyEnable[(int)FKEYS.F5]) ShiftF5();
		}
		/// <summary>Handle for btn_Group6_NY click.</summary>
		private void btn_Group6_NY_Click(object sender, System.EventArgs e)
		{
			if (this.ShiftFKeyEnable[(int)FKEYS.F6]) ShiftF6();
		}
		//function buttons
		/// <summary>Handle for lbl_CtrlF1_NY and lbl_CtrlF1_YY</summary>
		private void lbl_CtrlF1_Click(object sender, System.EventArgs e)
		{
			if (this.CtrlFKeyEnable[(int)FKEYS.F1]) CtrlF1();
		}
		/// <summary>Handle for lbl_CtrlF2_NY and lbl_CtrlF2_YY</summary>
		private void lbl_CtrlF2_Click(object sender, System.EventArgs e)
		{
			if (this.CtrlFKeyEnable[(int)FKEYS.F2]) CtrlF2();
		}
		/// <summary>Handle for lbl_CtrlF3_NY and lbl_CtrlF3_YY</summary>
		private void lbl_CtrlF3_Click(object sender, System.EventArgs e)
		{
			if (this.CtrlFKeyEnable[(int)FKEYS.F3]) CtrlF3();
		}
		/// <summary>Handle for lbl_CtrlF4_NY and lbl_CtrlF4_YY</summary>
		private void lbl_CtrlF4_Click(object sender, System.EventArgs e)
		{
			if (this.CtrlFKeyEnable[(int)FKEYS.F4]) CtrlF4();
		}
		/// <summary>Handle for lbl_CtrlF5_NY and lbl_CtrlF5_YY</summary>
		private void lbl_CtrlF5_Click(object sender, System.EventArgs e)
		{
			if (this.CtrlFKeyEnable[(int)FKEYS.F5]) CtrlF5();
		}
		#endregion

		#region ShiftF1 .. ShiftF6, Escape methods overrides

		/// <summary>
		/// Requests a status bar choosing  from the statusBarCorrugator, StatusBarDryEnd, StatusBarWetEnd
		/// </summary>
		private void RequestNewProperStatusBar()
		{
			PageManager pm = PageManager.getPageManager();
			SyncroC.Utility.Settings.Configuration conf = SyncroC.Utility.Settings.getSettings().configuration;
      if (conf.IsDryEndControlOnly() || conf.IsDryEndControlAndPMac400())
      {//dry only
				if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
					askForPage(typeof(Frm_StatusBarDryEnd));
			}
			else if (conf.IsWetEndControlOnly())
			{//wet only
				if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
					askForPage(typeof(Frm_StatusBarWetEnd));
			}
			else
			{//corrugator
				if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)) &&
				!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
					askForPage(typeof(Frm_StatusBarDryEnd));
			}
		}

		/// <summary>
		///ShiftF1 key behaviour: go to the first group main page (home page).
		///Must be overridden by every page of the first group in order to do nothing.
		/// </summary>
		protected override void ShiftF1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			RequestNewProperStatusBar();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction);
		}

		/// <summary>
		/// ShiftF2 key behaviour: go to the second group main page (report page).
		/// Must be overridden by every page of the second group in order to do nothing.
		/// </summary>
		protected override void ShiftF2()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			RequestNewProperStatusBar();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageStatistics);
		}

		/// <summary>
		/// ShiftF3 key behaviour: go to the third group main page (settings page).
		/// Must be overridden by every page of the third group in order to do nothing.
		/// </summary>
		protected override void ShiftF3()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			RequestNewProperStatusBar();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSettings);
		}

		/// <summary>
		/// ShiftF4 key behaviour: go to the fourth group main page ().
		/// Must be overridden by every page of the fourth group in order to do nothing.
		/// </summary>
		protected override void ShiftF4()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			PageManager pm = PageManager.getPageManager();
			//request Twin400 statusBar
      //if (! pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarTwin400)))
      //  askForPage(typeof(Frm_StatusBarTwin400));
			//request Twin 400 current page 
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageTwin400);
		}

		/// <summary>
		/// ShiftF4 key behaviour: go to the fifth group main page ().
		/// Must be overridden by every page of the fifth group in order to do nothing.
		/// </summary>
		protected override void ShiftF5()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			PageManager pm = PageManager.getPageManager();
			//request Debug current page 
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageDebug);
		}

		/// <summary>
		/// ShiftF4 key behaviour: go to the sixth group main page ().
		/// Must be overridden by every page of the sixth group in order to do nothing.
		/// </summary>
		protected override void ShiftF6()
		{
		}

		///// <summary>
		///// Escape key behaviour: set doValidation variable false.
		///// 
		///// </summary>
		//protected override void Escape()
		//{
		//  PagesHistorian historian = PageManager.getPageManager().pagesHistorian;
		//  string pageToGoBack = historian.GetPreviousPage();
		//  if (pageToGoBack == null || pageToGoBack.Equals(""))
		//    pageToGoBack = historian.FirstPage;
		//  CommunicationManager cm = CommunicationManager.GetCommunicationManager();
		//  cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pageToGoBack);
		//}
		#endregion

		/// <summary>
		/// Draw the cards border.
		/// </summary>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
      base.OnPaint(e);
      Color borderColor = lbl_Status.BackColor;
			int topBorderWidth = 6;
			int leftRightLowBorderWidth = 2;
			Graphics g = e.Graphics;
			leftHigh = new Point(
				tab_Tab1.Location.X + 1, //x
				tab_Tab1.Location.Y + tab_Tab1.Size.Height); //y
			rightHigh = new Point(
				tab_TabA.Location.X - leftRightLowBorderWidth + 1, //x
				tab_Tab1.Location.Y + tab_Tab1.Size.Height); //y
			leftLow = new Point(
				tab_Tab1.Location.X + 1, //x
				/*this.Height*/pct_FosberLogo.Location.Y + pct_FosberLogo.Height - leftRightLowBorderWidth); //y
			rightLow = new Point(
				tab_TabA.Location.X - leftRightLowBorderWidth + 1, //x
				/*this.Height*/pct_FosberLogo.Location.Y + pct_FosberLogo.Height - leftRightLowBorderWidth); //y

			g.DrawLines(new Pen(new SolidBrush(borderColor), leftRightLowBorderWidth),
				new Point[]{
                leftHigh, leftLow, rightLow, rightHigh
      });

      g.DrawLine(new Pen(new SolidBrush(borderColor), topBorderWidth),
        new Point(leftHigh.X, leftHigh.Y + Convert.ToInt32(topBorderWidth / 2)), //left
        new Point(rightHigh.X, rightHigh.Y + Convert.ToInt32(topBorderWidth / 2)));//right

      leftHigh.Y += topBorderWidth + lbl_Title_YN.Height;
      rightHigh.Y += topBorderWidth + lbl_Title_YN.Height;
      internalLeftHigh = new Point(
                     leftHigh.X + BORDER,
                     leftHigh.Y + BORDER);
      internalLeftLow = new Point(
                     leftLow.X + BORDER,
                     leftLow.Y - BORDER);
      internalRightLow = new Point(
                     rightLow.X - BORDER,
                     rightLow.Y - BORDER);
      internalRightHigh = new Point(
                     rightHigh.X - BORDER,
                     rightHigh.Y + BORDER);			//draw design grid.
      if (this.DesignMode && showDesignGrid)
      {
        Pen pen = new Pen(new SolidBrush(Color.Red), 1);
				//draw borders
				g.DrawLines(pen,
          new Point[] { internalLeftHigh, internalLeftLow, internalRightLow, internalRightHigh, internalLeftHigh });

        //draw grid (vertical lines)
				for (int i = 1; i <= designGridVerticalLines; i++)
				{
          int X = Convert.ToInt32(internalLeftHigh.X + (internalRightHigh.X - internalLeftHigh.X) / (designGridVerticalLines + 1) * i);
          g.DrawLine(pen,
						new Point(
						X,
            internalLeftHigh.Y),
            new Point(
						X,
            internalLeftLow.Y));
        }

				//draw grid (horizontal lines)
				for (int i = 1; i <= designGridHorizontalLines; i++)
				{
          int Y = Convert.ToInt32(internalLeftHigh.Y + (internalLeftLow.Y - internalLeftHigh.Y) / (designGridHorizontalLines + 1) * i);
          g.DrawLine(pen,
						new Point(
            internalLeftHigh.X,
            Y),
						new Point(
            internalRightHigh.X,
            Y));
				}
			}
		}

		/// <summary>
		/// Set the bindings for the time and date.
		/// </summary>
		private void Frm_Page_Load(object sender, System.EventArgs e)
		{
			//set the bindings only when the program is running because the designer isn't able to do that 
			if (! this.DesignMode)
				setBindings();
		}

		/// <summary>
		/// Set the CausesValidation variable True because it is the default value every time 
		/// a page is accessed.
		/// Only the Escape() method (called on click of pct_Esc and on KeyUp of Esc key)
		/// can set it to false to avoid validations.
		/// </summary>
		private void Frm_Page_Activated(object sender, System.EventArgs e)
		{
			this.CausesValidation = true;
		}

		/// <summary>
		/// 
		/// </summary>
		private void Frm_Page_CausesValidationChanged(object sender, System.EventArgs e)
		{
			foreach (Control c in this.Controls)
				c.CausesValidation = this.CausesValidation;
		}		

		public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
		{
			base.ConfigPage(config);
			this.ShiftFKeyEnable[(int)FKEYS.F1]=this.btn_Group1.Visible=
					this.ShiftFKeyEnable[(int)FKEYS.F2]=this.btn_Group2.Visible=
					this.ShiftFKeyEnable[(int)FKEYS.F3]=this.btn_Group3.Visible=
					(config.IsSyncro() || config.IsSyncro400());

      if (!this.btn_Group1.Visible )
      {
        this.lbl_Status.Width = this.lbl_Status.Width + this.lbl_Status.Location.X - this.pct_FosberLogo.Width;
        this.lbl_Status.Location = new System.Drawing.Point(this.pct_FosberLogo.Width, this.lbl_Status.Location.Y);
      }

      this.setUpTimeZone();

			//set visibility of debug group
			this.ShiftFKeyEnable[(int)FKEYS.F5] = this.btn_Group5.Visible = debugGroupVisibility;
		}
	}
}
