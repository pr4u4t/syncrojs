using System;
using System.Data;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;

using SyncroC.Forms;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.DataBase.Interfaces;
using SyncroC.DataBase.LineConfig;
using SyncroC.DataBase.SyncroConfig;

namespace SyncroC
{
	/// <summary>
	/// Database manager. Manage all the client data and provides an inerface to them for the client and server.
	/// </summary>
	public class DBManager
	{	
		//Databases
		/// <summary>
		/// Language translations database.
		/// </summary>
		public Language language;

		/// <summary>
		/// Message translations database.
		/// </summary>
		public SyncroC.DataBase.Message message;

		/// <summary>
		/// Alarm translations database.
		/// </summary>
		public Alarm alarm;

		/// <summary>
		/// DownTimeReason translations database.
		/// </summary>
		public DownTimeReason downTimeReason;

		/// <summary>
		/// LineConfig reference.
		/// </summary>
		public LineConfig lineConfig;

		/// <summary>
		/// SyncroConfig reference.
		/// </summary>
		public SyncroConfig syncroConfig;

		/// <summary>
		/// order database
		/// </summary>
		public Order order;

		/// <summary>
		/// flute database.
		/// </summary>
		public Flute flute;

		/// <summary>
		/// paper database.
		/// </summary>
		public Paper paper;

		/// <summary>
		/// quality database.
		/// </summary>
		public Quality quality;

		/// <summary>
		/// pastOrder database.
		/// </summary>
		public PastOrder pastOrder;

		/// <summary>
		/// scorer database.
		/// </summary>
		public ScorerProfile scorerProfile;

		/// <summary>
		/// rotary format database.
		/// </summary>
		public RotaryFormat rotaryFormat;

		/// <summary>
		/// Alarm History database.
		/// </summary>
		public AlarmHistory alarmHistory;

		/// <summary>
		/// Shift database.
		/// </summary>
		public Shift shift;

		/// <summary>
		/// DownTime database.
		/// </summary>
		public DownTime downTime;

		/// <summary>
		/// CutToMark database.
		/// </summary>
		public CutToPattern cutToPattern;

		/// <summary>
		/// Roll database.
		/// </summary>
		public PastRoll pastRolls;

		/// <summary>Needed to realize the singleton pattern.</summary>
		private static DBManager dbManager;
		/// <summary>Absolute path of the folder where database should be stored.</summary>
		private string DataBaseFolder = "";

		/// <summary>Used to periodically send the alignment command</summary>
		private System.Windows.Forms.Timer timer;

		private int AlignmentPeriodMillisecs = 300000;//every 5 minutes

		private bool dbError = false;
		public bool DBError
		{
			get { return dbError; }
			set { dbError = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler OrderValidationSucceded;

		private delegate string SetMsgDelegate(string msg, bool error, bool still);

		/// <summary>
		/// Builder.
		/// </summary>
		private DBManager()
		{
			this.timer = new System.Windows.Forms.Timer();
			this.timer.Enabled = false;
			this.timer.Interval = AlignmentPeriodMillisecs;//every 5 minutes
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			Settings sett = Settings.getSettings();
			//DataBase folder management
      DataBaseFolder = sett.paths.ClientData;
			if (! Directory.Exists(DataBaseFolder)) 
			{	//the folder does not exist: creation
				try
				{
					Directory.CreateDirectory(DataBaseFolder);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, ex);
				}
			}
			//database creation 
			//language, message, alarm, downtimereason load local database during building
			language = new Language(DataBaseFolder);
			message = new SyncroC.DataBase.Message(DataBaseFolder);
			alarm = new SyncroC.DataBase.Alarm(DataBaseFolder);
			downTimeReason = new SyncroC.DataBase.DownTimeReason(DataBaseFolder);
			//only creation
			lineConfig = new LineConfig(DataBaseFolder);
			syncroConfig = new SyncroConfig(DataBaseFolder);
			order = new Order();
			flute = new SyncroC.DataBase.Flute();
			paper = new SyncroC.DataBase.Paper();
			quality = new Quality();
			scorerProfile = new ScorerProfile();
			rotaryFormat = new RotaryFormat();
			pastOrder = new PastOrder();
			this.alarmHistory = new AlarmHistory();
			this.shift = new Shift();
			this.downTime = new DownTime();
			this.cutToPattern = new CutToPattern();
      this.pastRolls = new PastRoll();

      this.downTimeReason.DowntimeReasonLoad+=new EventHandler(downTimeReason_DowntimeReasonLoad);

			//sends db versions to the server	
      //agrassi_FI02-03-2010_2
      if ((!Settings.getSettings().dataBase.RestoreDBLanguages) || (!Settings.getSettings().configuration.getOnServer()))
      {
        this.SendVersions();
        this.timer.Enabled = true;
      }
		}

		/// <summary>
		/// Factory.
		/// </summary>
		/// <returns>The one and only DBManager object of the application.</returns>
		public static DBManager getDBManager()
		{
			if (dbManager == null) 
					dbManager = new DBManager();	
			return dbManager;
		}

		/// <summary>
		/// Conditional factory.
		/// If the DBManager has been already instanciated this method return it.
		/// If it has not then if true was passed the DBManager will be instantiated and returned, 
		/// if false was passed, the null reference will be returned. 
		/// </summary>
		/// <param name="build">determine if the DBManager singleton has to be created if null.</param>
		public static DBManager getDBManager(bool build)
		{
			if (build) 
				return getDBManager();
			return dbManager;
		}

		/// <summary>
		/// Sends to the server all the database versions so that the server will:
		/// version is the same --> call the load on the database
		/// version is different --> send all the data and then call the load on the database
		/// </summary>
		public void SendVersions()
		{
			this.SendVersions(true, true, true, true, true, true, true, true, true, true);
		}

		/// <summary>
		/// Sends to the server all the database versions so that the server will:
		/// version is the same --> call the load on the database
		/// version is different --> send all the data and then call the load on the database.
		/// Wrong version of the database can be sent specifing "false" in the correspondent parameter
		/// </summary>
		/// <param name="Order">if false the wrong version will be sent anyway for Order database.</param>
		/// <param name="Flute">if false the wrong version will be sent anyway for Flute database.</param>
		/// <param name="Paper">if false the wrong version will be sent anyway for Paper database.</param>
		/// <param name="Quality">if false the wrong version will be sent anyway for Quality database.</param>
		/// <param name="PastOrder">if false the wrong version will be sent anyway for PastOrder database.</param>
		/// <param name="Shift">if false the wrong version will be sent anyway for Shift database.</param>
		/// <param name="DownTime">if false the wrong version will be sent anyway for Dowtime database.</param>
		/// <param name="DownTime">if false the wrong version will be sent anyway for Roll database.</param>
		/// <param name="RTCDatabases">if false the version of every RTC databases will not be sent.</param>
		public void SendVersions(bool Order, bool Flute, bool Paper, bool Quality,
			bool PastOrder,	bool Shift, bool DownTime, bool CutToPattern, bool Roll, bool RTCDatabases)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_CMD_VER + "(" +
				((Order) ? order.GetVer() : SyncroC.DataBase.Order.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((Flute) ? flute.GetVer() : SyncroC.DataBase.Flute.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((Paper) ? paper.GetVer() : SyncroC.DataBase.Paper.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((Quality) ? quality.GetVer() : SyncroC.DataBase.Quality.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((PastOrder) ? pastOrder.GetVer() : SyncroC.DataBase.PastOrder.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((Shift) ? shift.GetVer() : SyncroC.DataBase.Shift.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((DownTime) ? downTime.GetVer() : SyncroC.DataBase.DownTime.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((CutToPattern) ? cutToPattern.GetVer() : SyncroC.DataBase.CutToPattern.WRONG_VERSION) + Command.PARAMETER_SEPARATOR +
				((Roll) ? pastRolls.GetVer() : SyncroC.DataBase.PastRoll.WRONG_VERSION) + ")");

			if (RTCDatabases)
			{
					cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.RTC_MANAGER + "." + Command.DB_CMD_VER + "(" +

					/*1*/lineConfig.GetVer() + Command.PARAMETER_SEPARATOR +

					//aggiungere syncroConfig

					/*2*/scorerProfile.GetVer() + Command.PARAMETER_SEPARATOR +
					/*3*/rotaryFormat.GetVer() + Command.PARAMETER_SEPARATOR +

					/*4*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SYC_HW) + Command.PARAMETER_SEPARATOR +
					/*5*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SYC_SW) + Command.PARAMETER_SEPARATOR +

					/*6*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_STACKER1) + Command.PARAMETER_SEPARATOR +
					/*7*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_STACKER2) + Command.PARAMETER_SEPARATOR +
					/*8*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_STACKER3) + Command.PARAMETER_SEPARATOR +
					/*9*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_KNIFE) + Command.PARAMETER_SEPARATOR +
					/*10*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SLITTERSCORER) + Command.PARAMETER_SEPARATOR +
					/*11*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_ROTARY) + Command.PARAMETER_SEPARATOR +

					/*12*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_DOUBLEBACKER) + Command.PARAMETER_SEPARATOR +
					/*13*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_GLUEUNIT) + Command.PARAMETER_SEPARATOR +
					/*14*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_TERN) + Command.PARAMETER_SEPARATOR +
					/*15*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SF1) + Command.PARAMETER_SEPARATOR +
					/*16*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SF2) + Command.PARAMETER_SEPARATOR +
					/*17*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SF3) + Command.PARAMETER_SEPARATOR +
					/*18*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER1) + Command.PARAMETER_SEPARATOR +
					/*19*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER2) + Command.PARAMETER_SEPARATOR +
					/*20*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER3) + Command.PARAMETER_SEPARATOR +
					/*21*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER4) + Command.PARAMETER_SEPARATOR +
					/*22*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER5) + Command.PARAMETER_SEPARATOR +
					/*23*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER6) + Command.PARAMETER_SEPARATOR +
					/*24*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_SPLICER7) + Command.PARAMETER_SEPARATOR +
					/*25*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_MTH) + Command.PARAMETER_SEPARATOR +
					/*26*/this.alarmHistory.GetVer(AlarmHistory.ALARM_HISTORY_LIST_EXTERNAL_WEC)
					+ ")");
			}
		}

    private void downTimeReason_DowntimeReasonLoad(object sender, EventArgs e)
    {
      this.downTime.UpdateFosberFlags();
    }

		private void timer_Tick(object sender, System.EventArgs e)
		{
      if (Settings.getSettings().dataBase.RestoreDBLanguages)
        return;
			//send the Alignment command to the server in order to make all db versions periodically saved
			if (((TimeSpan)(DateTime.Now - CommunicationManager.GetCommunicationManager().LastCommError)).TotalMilliseconds
				< AlignmentPeriodMillisecs)
			{
				Logger.LogError(this, "timer_Tick - SendAlignmentCommand avoided because of a recent comm error"); 
			}
			else
			{
				if (!DBError)
				{
          //ABELLO_FI09/11/2010_1 se i dataset degli ordini da eseguire sono vuoti allora richiedo un aggiornamento
          if (DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count == 0)
          {
            Logger.LogError(this, "timer_Tick - Richiedo un allineamento perchè non ho ordini da fare e questo è molto strano");
            DBManager.getDBManager().SendVersions(false, true, true, true, true, true, true, true, true, false);
          }
          else
            SendAlignmentCommand(false);
				}
				else
				{
					Logger.LogError(this, "timer_Tick - SendAlignmentCommand avoided becase databases are not correctly aligned because of a database error."); 
				}
			}
		}

		public void SendAlignmentCommand(bool saveReport)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_COMM_MANAGER, Command.COMMUNICATION_MANAGER + "." + Command.DB_ALIGNMENT + "(" + saveReport.ToString() + ")");
		}

		/// <summary>
		/// Clear all the database that can be updated by the server during the execution of the program
		/// such as "order", "alarm", "paper", "flute", .....
		/// because if the syncroS task fall down then the database could be not consistent so 
		/// when this kind of error occurs the client clean them and the server resend the records.
		/// </summary>
		public void ClearDatabase()
		{
			if (order != null)
				order.Clr();
			if (flute != null)
				flute.Clr();
			if (paper != null)
				paper.Clr();
			if (quality != null)
				quality.Clr();
			if (pastOrder != null)
				pastOrder.Clr();
			if (shift != null)
				shift.Clr();
			if (downTime != null)
				downTime.Clr();
			//todo: ?
			//if (lineConfig != null)
			//  lineConfig.Clr();
			if (scorerProfile != null)
				scorerProfile.Clr();
			if (rotaryFormat != null)
				rotaryFormat.Clr();
			if (alarmHistory != null)
				alarmHistory.Clr();
			if (cutToPattern != null)
				cutToPattern.Clr();
			if (pastRolls != null)
				pastRolls.Clr();
			//call the refresh data on every instantiated form
			PageManager.getPageManager().refreshAllFormsData();
		}

		private void CopyReport()
		{
			Settings settings = Settings.getSettings();
			string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
			date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
      string clientDataPath = Settings.getSettings().paths.ClientData;
      //agrassi_FI24-09-08
      //string savedReportPath = Settings.getSettings().paths.ReportWithoutDate + " (" + date + ")\\";
      string savedReportPath = Settings.getSettings().paths.ReportZip;
      //agrassi_FI19-08-2010_1
      //string reportCompleteName = "ReportSYC_" + this.syncroConfig.GeneralSetting.Job + this.syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + PageManager.getPageManager().getSyncroSoftwareStandard() + " (" + date + ").zip";
      string reportCompleteName = "ReportSYC_" + this.syncroConfig.GeneralSetting.Job + this.syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + " (" + date + ").zip";

      string savedReportZip = savedReportPath + reportCompleteName;

      //Directory.CreateDirectory(savedReportPath);
      //Directory.CreateDirectory(savedReportPath);
      //agrassi_FI03-11-08
      SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      Frm_Base.CopyZipDialogDelegate CopyZipDialog = new Frm_Base.CopyZipDialogDelegate(PageManager.getPageManager().getCurrentPage().CopyZipDialog);
      bool copyToUSBDrive = false, copyError = false;
      int drv;
      DriveInfo[] di = DriveInfo.GetDrives();
      for (drv = 0; drv < di.Length; drv++)
      {
        if ((di[drv].IsReady) &&
            ((di[drv].DriveType == DriveType.Removable) || ((di[drv].DriveFormat != "NTFS") &&
                (di[drv].RootDirectory.FullName.ToUpper() != "C:\\") && (di[drv].DriveType != DriveType.CDRom))))
        {
          DialogResult res = (DialogResult)PageManager.getPageManager().getCurrentPage().Invoke(CopyZipDialog, new object[] {PageManager.getPageManager().getCurrentPage(),
                DBManager.getDBManager().message.getTranslatedMessage("§SYCC_192") + 
                "\r\n" + di[drv].RootDirectory.FullName + "Syncro\\"+reportCompleteName, true});
          if (res == DialogResult.OK)
            copyToUSBDrive = true;
          break;
        }
      }

      ArrayList filePaths = new ArrayList();
      filePaths.Add(clientDataPath + PastOrder.XML_FILE_NAME);
      filePaths.Add(clientDataPath + DownTime.XML_FILE_NAME);
      filePaths.Add(clientDataPath + PastRoll.XML_FILE_NAME);
      filePaths.Add(clientDataPath + AlarmHistory.XML_FILE_NAME);
      try
      {
        if (!Directory.Exists(savedReportPath))
          Directory.CreateDirectory(savedReportPath);
        System.Threading.Thread.Sleep(2000);

        PageManager.getPageManager().getCurrentPage().CreateZipFile(savedReportZip, filePaths);
        PageManager.getPageManager().getCurrentPage().CheckZips(savedReportPath, "Report");
        //agrassi_FI03-11-08
        if (copyToUSBDrive)
        {
          FileInfo fi = new FileInfo(savedReportZip);
          if ((di[drv].AvailableFreeSpace > fi.Length) && di[drv].IsReady)
          {
            if (!Directory.Exists(di[drv].RootDirectory.FullName + "Syncro"))
              Directory.CreateDirectory(di[drv].RootDirectory.FullName + "Syncro");
            System.IO.File.Copy(savedReportZip, di[drv].RootDirectory.FullName + "Syncro\\" + reportCompleteName);
          }
          else
          {
            PageManager.getPageManager().Invoke(SetMsg, new object[] { DBManager.getDBManager().message.getTranslatedMessage("§SYCC_193"), true, false });
            copyError = true;
          }
          PageManager.getPageManager().getCurrentPage().Invoke(CopyZipDialog, new object[] { PageManager.getPageManager().getCurrentPage(), DBManager.getDBManager().message.getTranslatedMessage("§SYCC_194"), false });
        }

        //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
        if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1"))
        {
          try
          {
            if (!Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedReportPath.Substring(3, savedReportPath.Length - 3)))
              Directory.CreateDirectory("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedReportPath.Substring(3, savedReportPath.Length - 3));
            savedReportPath = "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedReportPath.Substring(3, savedReportPath.Length - 3);
            System.IO.File.Copy(savedReportZip, savedReportPath + reportCompleteName);
          }
          catch (Exception e)
          {
            Console.WriteLine("The process failed: {0}", e.ToString());
          }
        }
      }
      catch (Exception e)
      {
        Logger.LogError(this, e);
      }

      //File.Copy(clientDataPath + PastOrder.XML_FILE_NAME, savedReportPath + PastOrder.XML_FILE_NAME);
      //File.Copy(clientDataPath + DownTime.XML_FILE_NAME, savedReportPath + DownTime.XML_FILE_NAME);
      ////agrassi_FI06-03-08
      ////File.Copy(clientDataPath + Shift.XML_FILE_NAME, savedReportPath + Shift.XML_FILE_NAME);
      //File.Copy(clientDataPath + PastRoll.XML_FILE_NAME, savedReportPath + PastRoll.XML_FILE_NAME);
      //File.Copy(clientDataPath + AlarmHistory.XML_FILE_NAME, savedReportPath + AlarmHistory.XML_FILE_NAME);
      //agrassi_FI03-11-08
      if (!copyError)
      {
        PageManager.getPageManager().Invoke(SetMsg, new object[] { "Report files succesfully saved and copied.", false, false });
      }
		}

		/// <summary>
		/// DBManager collector for commands to be interpreted by the reflection.
		/// </summary>
		/// <param name="msg">a string representing the command to be interpreted.</param>
		public void Reflect(string msg)
		{ 
			//check for msg correctness and parsing
			if (msg == null
				|| msg.Equals(""))
			{
				Logger.LogError(this, "Reflect - msg was null or empty.");
				return;
			}

      //graffaelli_FI09/11/2011_2 - Aggiunto log su client
      Logger.LogError(this, "Reflect - DB - msg parameter: " + msg);

			int parentesisPosition = msg.IndexOf('(');
			if (parentesisPosition == -1 || ! msg.EndsWith(")"))
			{
				Logger.LogError(this, "Reflect - invalid command (wrong parentesis)\"" + msg + "\".");
				return;
			}

			string parametres = msg.Substring(parentesisPosition + 1, msg.Length - parentesisPosition - 2);
			string[] tokens = msg.Substring(0, parentesisPosition).Split(new Char[]{'.'});
			if (tokens.Length < 2)
			{
				Logger.LogError(this, "Reflect - invalid command \"" + msg + "\".");
				return;
			}
			//look for the database
			//database could be nested for example:
			//LineConfig.SingleFacer1.Mod() as many times are needed
			object obj = this; //obj is the DBManager itself and then it can be any class that implement the IDataBase interface or the LineConfig class

			for (int i = 0; i < tokens.Length - 1; i++)
			{
				try
				{
					FieldInfo field = obj.GetType().GetField(tokens[i]);
					if (field != null)
						obj = field.GetValue(obj);
					else
					{
						PropertyInfo property = obj.GetType().GetProperty(tokens[i]);
						if (property != null)
							obj = property.GetValue(obj, null);
						else
						{
							Logger.LogError(this, "cannot find a field or a property named \"" + tokens[i] + "\" into the class \"" + obj.ToString() + "\".");
							return;
						}
					}
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Reflect - error looking for the database or lineConfig \"" + tokens[i] + "\"", ex);
					return;
				}
			}

			//look for the method
			if (obj != null)
			{
				MethodInfo method = null;
				try 
				{
					Type objType = obj.GetType();
					method = objType.GetMethod(tokens[tokens.Length - 1]);
				}
				catch(Exception ex)
				{
					Logger.LogError(this, "Reflect - error looking for method \"" + tokens[tokens.Length - 1] + "\"", ex);
					return;
				}

				if (method != null)
				{//invocation
					try
					{
						if (parametres.Length == 0)
							method.Invoke(obj, null);
						else
							method.Invoke(obj, new Object[]{parametres});
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "Reflect - error invoking method \"" + tokens[tokens.Length - 1] + "\"", ex);
						return;
					}
				}
			}
		}

		/// <summary>
		/// Interpretes throw a switch commands for database.
		/// </summary>
		/// <param name="msg">Command to be interpreted.</param>
		public void Collect(string msg)
		{
			if (msg == null
				|| msg.Equals(""))
			{
				Logger.LogError(this, "Collect - msg was null or empty.");
				return;
			}

			//interpretazione del comando da eseguire
			int delimiterPosition = msg.IndexOf('(');
			if (delimiterPosition == -1 || ! msg.EndsWith(")"))
			{
				Logger.LogError(this, "Collect - invalid command \"" + msg + "\"; '(' or ')' is missing");
				return;
			}
			string command = msg.Substring(0, delimiterPosition);
			string parameters = msg.Substring(delimiterPosition + 1, msg.Length - delimiterPosition - 2);

			switch (command)
			{
				case "ClearAll"://svuotamento di tutti i database
					//call the Clr() method for every database
					ClearDatabase();
					break;
				case "DBAlignment"://the server has asked every database version
					//call the SendVersions() method 
					SendVersions();
					break;
				case "OrderValidationSucceded"://the server has succesfully completed an order validation
					if (OrderValidationSucceded != null)
						OrderValidationSucceded(this, new EventArgs());
					break;
				case "SaveReport"://the server has asked to save all the reports file
					//the server must send the following command after have sent the save commands for each database.
					CopyReport();
					break;
				default://comando inesistente
					Logger.LogError(this, "Collect - invalid command \"" + command + "\".");
					break;
			}
		}
	}
}
