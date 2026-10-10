using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.Data;

using SyncroC.Forms;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.DataBase.Interfaces;
using SyncroC.DataBase.LineConfig;
using SyncroC.DataBase.SyncroConfig;
using SyncroC.Forms.PopUp; 

namespace SyncroC.Forms.Pages.Settings
{
	public class Frm_RestoreBackup : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
	{
    private SyncroC.Controls.Title ttl_BackupSourceList;
    private System.ComponentModel.IContainer components = null;
    private SyncroC.Controls.GridView gdv_SourceBackups;
    private SyncroC.Controls.StateButton RestoreAllData;
    private SyncroC.Controls.StateButton RestoreConfigurationData;
    private SyncroC.Controls.Title ttl_BackupDataList;
    private SyncroC.Controls.GridView gdv_DataBackups;
    private SyncroC.Controls.StateButton MakeDataBackup;
    private SyncroC.Controls.StateButton MakeSourceBackup;
    private SyncroC.Controls.StateButton RestoreSource;

    private DataGridViewColumn clm_MessageData;
    private DataGridViewColumn clm_MessageSource;

    private string pcHDID;

		private enum State {Modify, Read};

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
    [DllImport("kernel32.dll")]
    static extern bool PulseEvent(IntPtr hEvent);
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);

    private void upDateBackupList()
    {
      bool isClientOnServer = true;
      bool externalBackupHDFound = Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr");
      bool restoreIsPossible = false;
      //RFAZZI_FI29/10/2010_1. Bug Fix su Restore da External HD
      bool backupIsPossible = false;

      //Controllo se siamo sul PC Server
      if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
      {
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, "This page can be used only on Server !!");
        isClientOnServer = false;
      }
      else
      {
        string pcHDIDonExternalHD;
        if (externalBackupHDFound)
        {
          pcHDIDonExternalHD = Ini.GetNoRebuild("GENERAL", "ServerPCHD_ID", "0", "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\" + "ServerPC.ini");

          if (!pcHDIDonExternalHD.Equals(this.pcHDID) && !pcHDIDonExternalHD.Equals("0"))
          {
            restoreIsPossible = true;
          }
          //RFAZZI_FI29/10/2010_1. Bug Fix su Restore da External HD
          if (pcHDIDonExternalHD.Equals(this.pcHDID) || pcHDIDonExternalHD.Equals("0"))
          {
            backupIsPossible = true;
          }
        }
      }

      RestoreAllData.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;
      RestoreConfigurationData.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;
      RestoreSource.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;
      MakeDataBackup.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;
      MakeSourceBackup.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;
      gdv_SourceBackups.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;
      gdv_DataBackups.Visible = !DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound && isClientOnServer;

      RestoreAllData.Enabled = SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift && restoreIsPossible;
      RestoreConfigurationData.Enabled = SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift && restoreIsPossible;
      RestoreSource.Enabled = SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift && restoreIsPossible;

      //RFAZZI_FI29/10/2010_1. Bug Fix su Restore da External HD
      MakeDataBackup.Enabled = SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift && backupIsPossible;
      MakeSourceBackup.Enabled = SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && RTCManager.getRTCManager().IsIdleShift && backupIsPossible;

      string[] directoriesData = null;
      string[] directoriesSource = null;

      if (!isClientOnServer)
        return;

      //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
      if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1") && externalBackupHDFound)
      {
        try
        {
          //Cartelle Source
          gdv_SourceBackups.Rows.Clear();

          string lastBackupStatus;
          lastBackupStatus = Ini.GetNoRebuild("BACKUP", "SourceFolder", "Started", Path.GetFullPath(Application.StartupPath + "\\..\\..\\..\\") + "BackupStatus.ini");
          if (lastBackupStatus.Equals("Completed"))
          {

            //directoriesSource = Directory.GetDirectories("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\Source", "*.*", SearchOption.TopDirectoryOnly);
            if ( Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\Source") )
            //for (int i = 0; i < directoriesSource.Length; i++)
            //{
              gdv_SourceBackups.Rows.Add("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\Source");
            //}
          }

          directoriesSource = Directory.GetDirectories("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\_old\\Source", "*.*", SearchOption.TopDirectoryOnly);
          for (int i = 0; i < directoriesSource.Length; i++)
          {
            gdv_SourceBackups.Rows.Add(directoriesSource[i]);
          }

          //Cartelle Data
          gdv_DataBackups.Rows.Clear();

          lastBackupStatus = Ini.GetNoRebuild("BACKUP", "DataFolder", "Started", Path.GetFullPath(Application.StartupPath + "\\..\\..\\..\\") + "BackupStatus.ini");

          if (lastBackupStatus.Equals("Completed"))
          {
            //directoriesData = Directory.GetDirectories("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\Data", "*.*", SearchOption.TopDirectoryOnly);
            if (Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\Data"))
            //for (int i = 0; i < directoriesData.Length; i++)
            //{
              gdv_DataBackups.Rows.Add("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\Data");
            //}
          }
          directoriesData = Directory.GetDirectories("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\usr\\Syncro\\_old\\Data", "*.*", SearchOption.TopDirectoryOnly);
          for (int i = 0; i < directoriesData.Length; i++)
          {
            gdv_DataBackups.Rows.Add(directoriesData[i]);
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("The process failed: {0}", e.ToString());
        }
      }
    }

    public string HardDiskID()
    {
      ManagementClass partionsClass = new ManagementClass("Win32_LogicalDisk");
      ManagementObjectCollection partions = partionsClass.GetInstances();
      string hdd = string.Empty;
      foreach (ManagementObject partion in partions)
      {
        hdd = Convert.ToString(partion["VolumeSerialNumber"]);
        if (hdd != string.Empty)
          return hdd;
      }
      return hdd;
    }


		public Frm_RestoreBackup()
		{
			InitializeComponent();

      this.clm_MessageData = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_MessageData.Name = "DataBackupName";
      this.clm_MessageData.HeaderText = "Backup List";
      this.clm_MessageData.Width = this.gdv_DataBackups.Width;
      this.gdv_DataBackups.Columns.Add(this.clm_MessageData);

      this.clm_MessageSource = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_MessageSource.Name = "SourceBackupName";
      this.clm_MessageSource.HeaderText = "Backup List";
      this.clm_MessageSource.Width = this.gdv_SourceBackups.Width;
      this.gdv_SourceBackups.Columns.Add(this.clm_MessageSource);

      this.pcHDID = HardDiskID();

      upDateBackupList();
		}

    private void Frm_RestoreBackup_Activated(object sender, EventArgs e)
    {
      this.upDateBackupList();

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RestoreBackup));
      this.ttl_BackupSourceList = new SyncroC.Controls.Title();
      this.gdv_SourceBackups = new SyncroC.Controls.GridView();
      this.RestoreAllData = new SyncroC.Controls.StateButton();
      this.RestoreConfigurationData = new SyncroC.Controls.StateButton();
      this.ttl_BackupDataList = new SyncroC.Controls.Title();
      this.gdv_DataBackups = new SyncroC.Controls.GridView();
      this.RestoreSource = new SyncroC.Controls.StateButton();
      this.MakeDataBackup = new SyncroC.Controls.StateButton();
      this.MakeSourceBackup = new SyncroC.Controls.StateButton();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Restore Backup";
      // 
      // tab_Tab9
      // 
      this.tab_Tab9.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab9.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // ttl_BackupSourceList
      // 
      this.ttl_BackupSourceList.BackColor = System.Drawing.Color.Black;
      this.ttl_BackupSourceList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_BackupSourceList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_BackupSourceList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_BackupSourceList.Location = new System.Drawing.Point(31, 193);
      this.ttl_BackupSourceList.Name = "ttl_BackupSourceList";
      this.ttl_BackupSourceList.Size = new System.Drawing.Size(1169, 259);
      this.ttl_BackupSourceList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_BackupSourceList.TabIndex = 134;
      this.ttl_BackupSourceList.TabStop = false;
      this.ttl_BackupSourceList.Text = "BACKUP SOURCE LIST";
      this.ttl_BackupSourceList.TranslateText = false;
      // 
      // gdv_SourceBackups
      // 
      this.gdv_SourceBackups.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_SourceBackups.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SourceBackups.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_SourceBackups.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_SourceBackups.AutoGenerateColumns = false;
      this.gdv_SourceBackups.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_SourceBackups.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_SourceBackups.DataMember = "";
      this.gdv_SourceBackups.DataSource = null;
      this.gdv_SourceBackups.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_SourceBackups.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SourceBackups.ImagePassword = null;
      this.gdv_SourceBackups.Location = new System.Drawing.Point(43, 217);
      this.gdv_SourceBackups.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_SourceBackups.MultiSelect = true;
      this.gdv_SourceBackups.Name = "gdv_SourceBackups";
      this.gdv_SourceBackups.RowsHeight = 30;
      this.gdv_SourceBackups.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_SourceBackups.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_SourceBackups.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_SourceBackups.Size = new System.Drawing.Size(979, 233);
      this.gdv_SourceBackups.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_SourceBackups.TabIndex = 149;
      this.gdv_SourceBackups.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_SourceBackups.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_SourceBackups.TranslateText = true;
      // 
      // RestoreAllData
      // 
      this.RestoreAllData.Active = false;
      this.RestoreAllData.Alarmed = false;
      this.RestoreAllData.Auto = false;
      this.RestoreAllData.BackColor = System.Drawing.Color.WhiteSmoke;
      this.RestoreAllData.Cursor = System.Windows.Forms.Cursors.Hand;
      this.RestoreAllData.FillBackColorWithImage = false;
      this.RestoreAllData.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreAllData.ForeColor = System.Drawing.Color.Black;
      this.RestoreAllData.FramePresent = true;
      this.RestoreAllData.HighLightEnable = false;
      this.RestoreAllData.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.RestoreAllData.ImagePassword = null;
      this.RestoreAllData.Inemergency = false;
      this.RestoreAllData.InLocal = false;
      this.RestoreAllData.IsButton = true;
      this.RestoreAllData.IsForceToInvisible = false;
      this.RestoreAllData.Location = new System.Drawing.Point(1030, 485);
      this.RestoreAllData.Name = "RestoreAllData";
      this.RestoreAllData.OnLine = false;
      this.RestoreAllData.ShowImage = false;
      this.RestoreAllData.ShowText = true;
      this.RestoreAllData.Size = new System.Drawing.Size(170, 70);
      this.RestoreAllData.Switching = false;
      this.RestoreAllData.TabIndex = 150;
      this.RestoreAllData.Text = "Restore All";
      this.RestoreAllData.TranslateText = true;
      this.RestoreAllData.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("RestoreAllData.UserDefinedActiveImage")));
      this.RestoreAllData.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("RestoreAllData.UserDefinedImage")));
      this.RestoreAllData.Warned = false;
      this.RestoreAllData.Click += new System.EventHandler(this.pulseRestoreDataAll);
      // 
      // RestoreConfigurationData
      // 
      this.RestoreConfigurationData.Active = false;
      this.RestoreConfigurationData.Alarmed = false;
      this.RestoreConfigurationData.Auto = false;
      this.RestoreConfigurationData.BackColor = System.Drawing.Color.WhiteSmoke;
      this.RestoreConfigurationData.Cursor = System.Windows.Forms.Cursors.Hand;
      this.RestoreConfigurationData.FillBackColorWithImage = false;
      this.RestoreConfigurationData.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreConfigurationData.ForeColor = System.Drawing.Color.Black;
      this.RestoreConfigurationData.FramePresent = true;
      this.RestoreConfigurationData.HighLightEnable = false;
      this.RestoreConfigurationData.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.RestoreConfigurationData.ImagePassword = null;
      this.RestoreConfigurationData.Inemergency = false;
      this.RestoreConfigurationData.InLocal = false;
      this.RestoreConfigurationData.IsButton = true;
      this.RestoreConfigurationData.IsForceToInvisible = false;
      this.RestoreConfigurationData.Location = new System.Drawing.Point(1030, 564);
      this.RestoreConfigurationData.Name = "RestoreConfigurationData";
      this.RestoreConfigurationData.OnLine = false;
      this.RestoreConfigurationData.ShowImage = false;
      this.RestoreConfigurationData.ShowText = true;
      this.RestoreConfigurationData.Size = new System.Drawing.Size(170, 70);
      this.RestoreConfigurationData.Switching = false;
      this.RestoreConfigurationData.TabIndex = 151;
      this.RestoreConfigurationData.Text = "Restore Only Configuration";
      this.RestoreConfigurationData.TranslateText = true;
      this.RestoreConfigurationData.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("RestoreConfigurationData.UserDefinedActiveImage")));
      this.RestoreConfigurationData.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("RestoreConfigurationData.UserDefinedImage")));
      this.RestoreConfigurationData.Warned = false;
      this.RestoreConfigurationData.Click += new System.EventHandler(this.pulseRestoreDataConfiguration);
      // 
      // ttl_BackupDataList
      // 
      this.ttl_BackupDataList.BackColor = System.Drawing.Color.Black;
      this.ttl_BackupDataList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_BackupDataList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_BackupDataList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_BackupDataList.Location = new System.Drawing.Point(31, 458);
      this.ttl_BackupDataList.Name = "ttl_BackupDataList";
      this.ttl_BackupDataList.Size = new System.Drawing.Size(1169, 255);
      this.ttl_BackupDataList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_BackupDataList.TabIndex = 152;
      this.ttl_BackupDataList.TabStop = false;
      this.ttl_BackupDataList.Text = "BACKUP DATA LIST";
      this.ttl_BackupDataList.TranslateText = false;
      // 
      // gdv_DataBackups
      // 
      this.gdv_DataBackups.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_DataBackups.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_DataBackups.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_DataBackups.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_DataBackups.AutoGenerateColumns = false;
      this.gdv_DataBackups.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_DataBackups.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_DataBackups.DataMember = "";
      this.gdv_DataBackups.DataSource = null;
      this.gdv_DataBackups.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_DataBackups.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_DataBackups.ImagePassword = null;
      this.gdv_DataBackups.Location = new System.Drawing.Point(44, 485);
      this.gdv_DataBackups.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_DataBackups.MultiSelect = true;
      this.gdv_DataBackups.Name = "gdv_DataBackups";
      this.gdv_DataBackups.RowsHeight = 30;
      this.gdv_DataBackups.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_DataBackups.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_DataBackups.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_DataBackups.Size = new System.Drawing.Size(978, 227);
      this.gdv_DataBackups.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_DataBackups.TabIndex = 153;
      this.gdv_DataBackups.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_DataBackups.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_DataBackups.TranslateText = true;
      // 
      // RestoreSource
      // 
      this.RestoreSource.Active = false;
      this.RestoreSource.Alarmed = false;
      this.RestoreSource.Auto = false;
      this.RestoreSource.BackColor = System.Drawing.Color.WhiteSmoke;
      this.RestoreSource.Cursor = System.Windows.Forms.Cursors.Hand;
      this.RestoreSource.FillBackColorWithImage = false;
      this.RestoreSource.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreSource.ForeColor = System.Drawing.Color.Black;
      this.RestoreSource.FramePresent = true;
      this.RestoreSource.HighLightEnable = false;
      this.RestoreSource.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.RestoreSource.ImagePassword = null;
      this.RestoreSource.Inemergency = false;
      this.RestoreSource.InLocal = false;
      this.RestoreSource.IsButton = true;
      this.RestoreSource.IsForceToInvisible = false;
      this.RestoreSource.Location = new System.Drawing.Point(1030, 217);
      this.RestoreSource.Name = "RestoreSource";
      this.RestoreSource.OnLine = false;
      this.RestoreSource.ShowImage = false;
      this.RestoreSource.ShowText = true;
      this.RestoreSource.Size = new System.Drawing.Size(171, 70);
      this.RestoreSource.Switching = false;
      this.RestoreSource.TabIndex = 154;
      this.RestoreSource.Text = "Restore Backup";
      this.RestoreSource.TranslateText = true;
      this.RestoreSource.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("RestoreSource.UserDefinedActiveImage")));
      this.RestoreSource.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("RestoreSource.UserDefinedImage")));
      this.RestoreSource.Warned = false;
      this.RestoreSource.Click += new System.EventHandler(this.pulseRestoreSource);
      // 
      // MakeDataBackup
      // 
      this.MakeDataBackup.Active = false;
      this.MakeDataBackup.Alarmed = false;
      this.MakeDataBackup.Auto = false;
      this.MakeDataBackup.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MakeDataBackup.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MakeDataBackup.FillBackColorWithImage = false;
      this.MakeDataBackup.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MakeDataBackup.ForeColor = System.Drawing.Color.Black;
      this.MakeDataBackup.FramePresent = true;
      this.MakeDataBackup.HighLightEnable = false;
      this.MakeDataBackup.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MakeDataBackup.ImagePassword = null;
      this.MakeDataBackup.Inemergency = false;
      this.MakeDataBackup.InLocal = false;
      this.MakeDataBackup.IsButton = true;
      this.MakeDataBackup.IsForceToInvisible = false;
      this.MakeDataBackup.Location = new System.Drawing.Point(1030, 643);
      this.MakeDataBackup.Name = "MakeDataBackup";
      this.MakeDataBackup.OnLine = false;
      this.MakeDataBackup.ShowImage = false;
      this.MakeDataBackup.ShowText = true;
      this.MakeDataBackup.Size = new System.Drawing.Size(171, 70);
      this.MakeDataBackup.Switching = false;
      this.MakeDataBackup.TabIndex = 155;
      this.MakeDataBackup.Text = "Make Backup";
      this.MakeDataBackup.TranslateText = true;
      this.MakeDataBackup.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MakeDataBackup.UserDefinedActiveImage")));
      this.MakeDataBackup.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MakeDataBackup.UserDefinedImage")));
      this.MakeDataBackup.Warned = false;
      this.MakeDataBackup.Click += new System.EventHandler(this.pulseDataBackup);
      // 
      // MakeSourceBackup
      // 
      this.MakeSourceBackup.Active = false;
      this.MakeSourceBackup.Alarmed = false;
      this.MakeSourceBackup.Auto = false;
      this.MakeSourceBackup.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MakeSourceBackup.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MakeSourceBackup.FillBackColorWithImage = false;
      this.MakeSourceBackup.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MakeSourceBackup.ForeColor = System.Drawing.Color.Black;
      this.MakeSourceBackup.FramePresent = true;
      this.MakeSourceBackup.HighLightEnable = false;
      this.MakeSourceBackup.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MakeSourceBackup.ImagePassword = null;
      this.MakeSourceBackup.Inemergency = false;
      this.MakeSourceBackup.InLocal = false;
      this.MakeSourceBackup.IsButton = true;
      this.MakeSourceBackup.IsForceToInvisible = false;
      this.MakeSourceBackup.Location = new System.Drawing.Point(1029, 380);
      this.MakeSourceBackup.Name = "MakeSourceBackup";
      this.MakeSourceBackup.OnLine = false;
      this.MakeSourceBackup.ShowImage = false;
      this.MakeSourceBackup.ShowText = true;
      this.MakeSourceBackup.Size = new System.Drawing.Size(171, 70);
      this.MakeSourceBackup.Switching = false;
      this.MakeSourceBackup.TabIndex = 156;
      this.MakeSourceBackup.Text = "Make Backup";
      this.MakeSourceBackup.TranslateText = true;
      this.MakeSourceBackup.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MakeSourceBackup.UserDefinedActiveImage")));
      this.MakeSourceBackup.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MakeSourceBackup.UserDefinedImage")));
      this.MakeSourceBackup.Warned = false;
      this.MakeSourceBackup.Click += new System.EventHandler(this.pulseSourceBackup);
      // 
      // Frm_RestoreBackup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 747);
      this.Controls.Add(this.MakeSourceBackup);
      this.Controls.Add(this.MakeDataBackup);
      this.Controls.Add(this.RestoreSource);
      this.Controls.Add(this.gdv_DataBackups);
      this.Controls.Add(this.ttl_BackupDataList);
      this.Controls.Add(this.RestoreConfigurationData);
      this.Controls.Add(this.RestoreAllData);
      this.Controls.Add(this.gdv_SourceBackups);
      this.Controls.Add(this.ttl_BackupSourceList);
      this.Name = "Frm_RestoreBackup";
      this.Tag = "";
      this.Text = "Frm_PcReplace";
      this.Activated += new System.EventHandler(this.Frm_RestoreBackup_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.ttl_BackupSourceList, 0);
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
      this.Controls.SetChildIndex(this.gdv_SourceBackups, 0);
      this.Controls.SetChildIndex(this.RestoreAllData, 0);
      this.Controls.SetChildIndex(this.RestoreConfigurationData, 0);
      this.Controls.SetChildIndex(this.ttl_BackupDataList, 0);
      this.Controls.SetChildIndex(this.gdv_DataBackups, 0);
      this.Controls.SetChildIndex(this.RestoreSource, 0);
      this.Controls.SetChildIndex(this.MakeDataBackup, 0);
      this.Controls.SetChildIndex(this.MakeSourceBackup, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion


		protected override void F9()
		{
		}


		private void readOnly(bool isReadOnly)
		{
			//readonly

			//tabStop
		}

		string GetName(Control c)
		{
			return c.Name.Substring(4);
		}

    private bool checkInfo()
    {

      //Richiedo conferma all'operazione
      bool ok = false;
      PopUp.Frm_Confirm conf = new Frm_Confirm();
      string txtKey = "§SYCC_200";
      string text = DBManager.getDBManager().message.getTranslatedMessage(txtKey);
      DialogResult res = conf.ShowDialog(this, text);
      ok = (res == DialogResult.Yes) || (res == DialogResult.OK);
      if (!ok)
        return false;

      return true;
    }

    private void pulseDataBackup(object sender, EventArgs e)
    {
      if (checkInfo())
      {
        IntPtr _ForceDataBackupHandle = CreateEvent(IntPtr.Zero, true, false, "DataBackup");
        PulseEvent(_ForceDataBackupHandle);
        CloseHandle(_ForceDataBackupHandle);
      }
    }

    private void pulseSourceBackup(object sender, EventArgs e)
    {
      if (checkInfo())
      {
        IntPtr _ForceSourceBackupHandle = CreateEvent(IntPtr.Zero, true, false, "SourceBackup");
        PulseEvent(_ForceSourceBackupHandle);
        CloseHandle(_ForceSourceBackupHandle);
      }
    }

    private void pulseRestoreSource(object sender, EventArgs e)
    {
      string backupPathName = this.gdv_SourceBackups.SelectedRows[0].Cells["SourceBackupName"].Value.ToString().Trim();
      if (checkInfo())
      {
        Logger.LogError(this, "Restore Source");
        Process process = new System.Diagnostics.Process();

#if DEBUG
        process.StartInfo.FileName = SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Debug\\RestoreBackups.exe";

        process.StartInfo.Arguments = backupPathName + " \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source.Substring(0, SyncroC.Utility.Settings.getSettings().paths.Source.Length - 1) + "\" \"" +
                                      "sourceRestore" + "\" \"" +
                                      "false" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Debug\\S_Starter.exe" +"\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Log;
#else
        process.StartInfo.FileName = SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Release\\RestoreBackups.exe";

        process.StartInfo.Arguments = backupPathName + " \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source.Substring(0, SyncroC.Utility.Settings.getSettings().paths.Source.Length - 1) + "\" \"" +
                                      "sourceRestore" + "\" \"" +
                                      "false" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Release\\S_Starter.exe" +"\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Log;
#endif
        process.Start();
        this.CloseSyncroCompletely();

      }
    }

    private void pulseRestoreDataAll(object sender, EventArgs e)
    {
      string backupPathName = this.gdv_DataBackups.SelectedRows[0].Cells["DataBackupName"].Value.ToString().Trim();
      if (checkInfo())
      {
        Logger.LogError(this, "Restore Data");
        Process process = new System.Diagnostics.Process();

#if DEBUG
        process.StartInfo.FileName = SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Debug\\RestoreBackups.exe";

        process.StartInfo.Arguments = backupPathName + " \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Data.Substring(0, SyncroC.Utility.Settings.getSettings().paths.Data.Length - 1) + "\" \"" +
                                      "dataRestore" + "\" \"" +
                                      "allDataRestore" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Debug\\S_Starter.exe" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Log;
#else
        process.StartInfo.FileName = SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Release\\RestoreBackups.exe";

        process.StartInfo.Arguments = backupPathName + " \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Data.Substring(0, SyncroC.Utility.Settings.getSettings().paths.Data.Length - 1) + "\" \"" +
                                      "dataRestore" + "\" \"" +
                                      "allDataRestore" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Release\\S_Starter.exe" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Log;
#endif
        process.Start();
        this.CloseSyncroCompletely();
      }
    }

    private void pulseRestoreDataConfiguration(object sender, EventArgs e)
    {
      string backupPathName = this.gdv_DataBackups.SelectedRows[0].Cells["DataBackupName"].Value.ToString().Trim() + "\\Server\\Config";
      if (checkInfo())
      {
        Logger.LogError(this, "Restore Data");
        Process process = new System.Diagnostics.Process();

#if DEBUG
        process.StartInfo.FileName = SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Debug\\RestoreBackups.exe";

        process.StartInfo.Arguments = backupPathName + " \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Data +"Server\\Config" + "\" \"" +
                                      "dataRestore" + "\" \"" +
                                      "onlyDataConfiguration" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Debug\\S_Starter.exe" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Log;
#else
        process.StartInfo.FileName = SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Release\\RestoreBackups.exe";

        process.StartInfo.Arguments = backupPathName + " \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Data +"Server\\Config" + "\" \"" +
                                      "dataRestore" + "\" \"" +
                                      "onlyDataConfiguration" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Source + "_Server\\Release\\S_Starter.exe" + "\" \"" +
                                      SyncroC.Utility.Settings.getSettings().paths.Log;
#endif
        process.Start();
        this.CloseSyncroCompletely();
      }
    }
  }
}

