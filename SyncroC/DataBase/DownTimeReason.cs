using System;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
	/// <summary>
	/// 
	/// </summary>
	public class DownTimeReason : SyncroC.DataBase.Interfaces.IDatabase
	{

#if DEBUG
		[DllImport(@"Utility.dll", CallingConvention = CallingConvention.Cdecl)]
#else
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#endif

		public static extern int FileCrc32Assembly([MarshalAs(UnmanagedType.LPTStr)] string szFilename, ref int dwCrc32);

    public const string XML_FILE_NAME_WITHOUT_EXT = "DownTimeReason";
		private string DataBaseXMLFileName = "";
		private string DataBaseXSDFileName = "";
		private string DataBaseXMLFileName_server = "";
    //agrassi_FI17-04-08_1
    private string DataBaseXMLFileName_serverStd = "";

		//tables indexes
		public static readonly int INDEX_TABLE_ZONE = 0;
		public static readonly int INDEX_TABLE_REASON = 1;

		//column indexes (Zone table)
		public static readonly int INDEX_ZONE_KEY = 0;
		public static readonly int INDEX_ZONE_CUSTOMER_KEY = 1;

		public static readonly int INDEX_ZONE_STANDARD = 2;
		public static readonly int INDEX_ZONE_AVAILABLE = 3;

		public static readonly int INDEX_ZONE_ENGLISH = 4;
		public static readonly int INDEX_ZONE_ITALIAN = 5;
		public static readonly int INDEX_ZONE_GERMAN = 6;
		public static readonly int INDEX_ZONE_SPANISH = 7;
		public static readonly int INDEX_ZONE_DUTCH = 8;
		public static readonly int INDEX_ZONE_CZECH = 9;
		public static readonly int INDEX_ZONE_FRENCH = 10;
		public static readonly int INDEX_ZONE_PORTUGUESE = 11;
		public static readonly int INDEX_ZONE_POLISH = 12;
		public static readonly int INDEX_ZONE_FINNISH = 13;
		public static readonly int INDEX_ZONE_SWEDISH = 14;
		public static readonly int INDEX_ZONE_CHINESE = 15;
		public static readonly int INDEX_ZONE_TURKISH = 16;
    public static readonly int INDEX_ZONE_RUSSIAN = 17;
    //aggiunta flag Fosber
    public static readonly int INDEX_ZONE_FOSBER = 18;
    //PMASSARO_FI08/05/09 - Added Norwegian language
    public static readonly int INDEX_ZONE_NORWEGIAN = 19;
    //agrassi_FI19-08-2010_2
    public static readonly int INDEX_ZONE_ARABIC = 20;

		//column indexes (Reason table)
		public static readonly int INDEX_REASON_KEY = 0;
		public static readonly int INDEX_REASON_CUSTOMER_KEY = 1;
		public static readonly int INDEX_REASON_ZONEKEY = 2;

		public static readonly int INDEX_REASON_STANDARD = 3;
		public static readonly int INDEX_REASON_AVAILABLE = 4;

		public static readonly int INDEX_REASON_ENGLISH = 5;
		public static readonly int INDEX_REASON_ITALIAN = 6;
		public static readonly int INDEX_REASON_GERMAN = 7;
		public static readonly int INDEX_REASON_SPANISH = 8;
		public static readonly int INDEX_REASON_DUTCH = 9;
		public static readonly int INDEX_REASON_CZECH = 10;
		public static readonly int INDEX_REASON_FRENCH = 11;
		public static readonly int INDEX_REASON_PORTUGUESE = 12;
		public static readonly int INDEX_REASON_POLISH = 13;
		public static readonly int INDEX_REASON_FINNISH = 14;
		public static readonly int INDEX_REASON_SWEDISH = 15;
		public static readonly int INDEX_REASON_CHINESE = 16;
		public static readonly int INDEX_REASON_TURKISH = 17;
    public static readonly int INDEX_REASON_RUSSIAN = 18;
    //aggiunta flag Fosber
    public static readonly int INDEX_REASON_FOSBER = 19;
    //PMASSARO_FI08/05/09 - Added Norwegian language
    public static readonly int INDEX_REASON_NORWEGIAN = 20;
    //agrassi_FI19-08-2010_2
    public static readonly int INDEX_REASON_ARABIC = 21;
    /// <summary>
		/// alarms dataset reference
		/// </summary>
		public DataSet downTimeReason = null;
    public event EventHandler DowntimeReasonLoad;

		public DownTimeReason(string DataBaseFolder)
		{
			//set the file name
      DataBaseXMLFileName = DataBaseFolder + XML_FILE_NAME_WITHOUT_EXT + ".xml";
      //aggiunta flag Fosber
      DataBaseXMLFileName_server = Settings.getSettings().paths.ServerConfig + XML_FILE_NAME_WITHOUT_EXT + ".xml";
      DataBaseXSDFileName = DataBaseFolder + XML_FILE_NAME_WITHOUT_EXT + ".xsd";
      //agrassi_FI17-04-08_1
      DataBaseXMLFileName_serverStd = Settings.getSettings().paths.Translations + XML_FILE_NAME_WITHOUT_EXT + ".xml";
			//datasets instantiation
      //downTimeReason = new DataSet();
			this.Load();
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values</param>
		public void Add(string values)
		{
			//non ? possibile fare inserimenti in questo database, 
			//sono possibili solo modifiche dei record non standard
			Logger.LogError(this, "The method \"Add\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Delete the specified record.
		/// </summary>
		/// <param name="key">Key of the record to be deleted.</param>
		public void Del(string key)
		{
			//non ? possibile fare del in questo database, 
			//sono possibili solo modifiche dei record non standard
			Logger.LogError(this, "The method \"Del\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Modify an existing record.
		/// </summary>
		/// <param name="values">a string representing the new values</param>
		public void Mod(string values)
		{
			//sono possibili solo modifiche dei record non standard
			//ma non vengono fatte dal server bensi dal client residente sul server
			//e salvate direttamente nella directory _server poi con un tasto sincronizza
			//sara il server a riallineare tutti gli altri client inviandogli il db
			Logger.LogError(this, "The method \"Mod\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Clean the dataset.
		/// </summary>
		public void Clr()
		{
			if (downTimeReason != null)
				downTimeReason.Clear();
			else
				Logger.LogError(this, "Clr - Dataset is null, impossible to complete the operation");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Compare the given cheksum with the database checksum.
		/// </summary>
		/// <param name="checksum">the checksum to compare</param>
		public void Chk(string checksum)
		{
			string[] values = checksum.Split(new Char[] { ',' });
			if (values.Length != 2)
			{
				Logger.LogError(this, "Chk - Invalid Parameter");
				return;
			}

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			int checkSchema = 0;
			int checkXml = 0;
			try
			{
				//xsd checking
				int ret = FileCrc32Assembly(DataBaseXSDFileName, ref checkSchema);
				if (ret != 0)
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXSDFileName + " error: " + ret);
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + ".downTimeReason()");
					return;
				}
				if (values[0].Trim() != (checkSchema.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXSDFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_DOWNTIMEREASON + "()");
					return;
				}
				//xml checking
				ret = FileCrc32Assembly(DataBaseXMLFileName, ref checkXml);
				if (ret != 0)
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXMLFileName + " error: " + ret);
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_DOWNTIMEREASON + "()");
					return;
				}
				if (values[1].Trim() != (checkXml.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXMLFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_DOWNTIMEREASON + "()");
					return;
				}
			}
			catch (DllNotFoundException ex)
			{
				Logger.LogError(this, "Collect - Dll for checksum calculation not found.", ex);
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_DOWNTIMEREASON + "()");
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "Chk - Error during checksum calculation", ex);
			}
		}

    //agrassi_FI17-04-08_1
    private void CheckCustomerZoneAndReasonPresence()
    {
      if (Settings.getSettings().configuration.getOnServer() && File.Exists(DataBaseXMLFileName_server))
      {
        DataSet downtimeReasonServer = new DataSet();
        try
		    {
          downtimeReasonServer.ReadXmlSchema(DataBaseXSDFileName);
			    try
			    {
            downtimeReasonServer.ReadXml(DataBaseXMLFileName_server, XmlReadMode.IgnoreSchema);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "Load from Server - error during reading data of downtime reason database", ex);
          }
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "Load from Server - error during reading schema of downtime reason database", ex);
        }
        bool updateZones = (downtimeReasonServer.Tables[INDEX_TABLE_ZONE].Rows.Find("00") == null);
        bool updateReasons = (downtimeReasonServer.Tables[INDEX_TABLE_REASON].Rows.Find("0000") == null);
        if (updateZones || updateReasons)
        {
          DataSet downtimeReasonServerStd = new DataSet();
          try
          {
            downtimeReasonServerStd.ReadXmlSchema(DataBaseXSDFileName);
            try
            {
              downtimeReasonServerStd.ReadXml(DataBaseXMLFileName_serverStd, XmlReadMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
              Logger.LogError(this, "Load from Server Std - error during reading data of downtime reason database", ex);
            }
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "Load from Server Std - error during reading schema of downtime reason database", ex);
          }
          if (updateZones)
          { 
            DataRow d = downtimeReasonServerStd.Tables[INDEX_TABLE_ZONE].Rows.Find("00");
            DataRow newRow = downtimeReasonServer.Tables[INDEX_TABLE_ZONE].NewRow();
            newRow.ItemArray = d.ItemArray;
            downtimeReasonServer.Tables[INDEX_TABLE_ZONE].Rows.Add(newRow);
            Logger.LogError(this, "Added zone 00 to server DowntimeReason db");
          }
          if (updateReasons)
          {
            DataRow d = downtimeReasonServerStd.Tables[INDEX_TABLE_REASON].Rows.Find("0000");
            DataRow newRow = downtimeReasonServer.Tables[INDEX_TABLE_REASON].NewRow();
            newRow.ItemArray = d.ItemArray;
            downtimeReasonServer.Tables[INDEX_TABLE_REASON].Rows.Add(newRow);
            Logger.LogError(this, "Added reason 0000 to server DowntimeReason db");
          }
          downtimeReasonServerStd.Dispose();
				  try
				  {
					  downtimeReasonServer.WriteXml(DataBaseXMLFileName_server);
				  }
				  catch (Exception ex)
				  {
					  Logger.LogError(this, "Save to Server - Error saving downTimeReason data", ex);
				  }
			  }
        downtimeReasonServer.Dispose();
      }
    }

    /// <summary>
		/// Implementation of the IDatabase interface.
		/// Load the databse.
		/// </summary>
		public void Load()
		{
       if (downTimeReason != null)
          downTimeReason.Dispose();
        downTimeReason = new DataSet();
        try
		    {
			    downTimeReason.ReadXmlSchema(DataBaseXSDFileName);
			    try
			    {
				    downTimeReason.ReadXml(DataBaseXMLFileName, XmlReadMode.IgnoreSchema);
            //agrassi_FI17-04-08_1
            this.CheckCustomerZoneAndReasonPresence();
            if (this.DowntimeReasonLoad != null)
                this.DowntimeReasonLoad(this, new System.EventArgs());
			    }
			    catch (Exception ex)
			    {
				    Logger.LogError(this, "Load - error during reading data of downtime reason database", ex);
			    }
		    }
		    catch (Exception ex)
		    {
          Logger.LogError(this, "Load - error during reading schema of downtime reason database", ex);
		    }
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Save permanently the database.
		/// </summary>
		public void Save()
		{
			if (downTimeReason != null)
			{
				try
				{
					downTimeReason.WriteXml(DataBaseXMLFileName);
					if (Settings.getSettings().configuration.getOnServer())
					{
						downTimeReason.WriteXml(DataBaseXMLFileName_server);

						//TODO: il client onServer oltre a salvare anche nella directory _server
						//deve dire al server con un comando (che ancora non c'e') di allineare tutti gli altri client
						//(stesso comando da aggiungere anche alla pagina di debug databases...)

						DBManager.getDBManager().syncroConfig.SendRelodSettings();
					}

				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Save - Error saving downTimeReason data or schema", ex);
				}
			}
			else
				Logger.LogError(this, "Save - Dataset downtime reason is null, impossible to complete the operation");
		}

		/// <summary>
		/// Set the database version.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <param name="ver">the version to set as a string.</param>
		public void SetVer(string ver)
		{
			Logger.LogError(this, "The method \"SetVer\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Get the database version as a string.
		/// Implementation of the IDatabase interface.
		/// </summary>
		public string GetVer()
		{
			Logger.LogError(this, "The method \"GetVer\" has been called but it is not implemented yet.");
			return String.Empty;
		}

		/// <summary>
		/// Gets the column index of the zone table for the first language (which is different from the current)
		/// </summary>
		public int getIndexZoneFromFirstLanguage()
		{
			return getIndexZoneFromLanguage(Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// Gets the column index of the zone table for the specified language
		/// </summary>
		public int getIndexZoneFromLanguage(string language)
		{
			return getIndexFromLanguage(language, true);
		}

		/// <summary>
		/// Gets the column index of the reason table for the first language (which is different from the current)
		/// </summary>
		public int getIndexReasonFromFirstLanguage()
		{
			return getIndexReasonFromLanguage(Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// Gets the column index of the reason table for the specified language
		/// </summary>
		public int getIndexReasonFromLanguage(string language)
		{
			return getIndexFromLanguage(language, false);
		}

		private int getIndexFromLanguage(string language, bool zone)
		{
			switch (language)
			{
				case "English":
					return (zone) ? INDEX_ZONE_ENGLISH : INDEX_REASON_ENGLISH;
				case "Italian":
					return (zone) ? INDEX_ZONE_ITALIAN: INDEX_REASON_ITALIAN;
				case "German":
					return (zone) ? INDEX_ZONE_GERMAN : INDEX_REASON_GERMAN;
				case "Spanish":
					return (zone) ? INDEX_ZONE_SPANISH : INDEX_REASON_SPANISH;
				case "Dutch":
					return (zone) ? INDEX_ZONE_DUTCH : INDEX_REASON_DUTCH;
				case "Czech":
					return (zone) ? INDEX_ZONE_CZECH : INDEX_REASON_CZECH;
				case "French":
					return (zone) ? INDEX_ZONE_FRENCH : INDEX_REASON_FRENCH;
				case "Portuguese":
					return (zone) ? INDEX_ZONE_PORTUGUESE : INDEX_REASON_PORTUGUESE;
				case "Polish":
					return (zone) ? INDEX_ZONE_POLISH : INDEX_REASON_POLISH;
				case "Finnish":
					return (zone) ? INDEX_ZONE_FINNISH : INDEX_REASON_FINNISH;
				case "Swedish":
					return (zone) ? INDEX_ZONE_SWEDISH : INDEX_REASON_SWEDISH;
				case "Chinese":
					return (zone) ? INDEX_ZONE_CHINESE : INDEX_REASON_CHINESE;
				case "Turkish":
					return (zone) ? INDEX_ZONE_TURKISH : INDEX_REASON_TURKISH;
				case "Russian":
					return (zone) ? INDEX_ZONE_RUSSIAN : INDEX_REASON_RUSSIAN;
        //PMASSARO_FI08/05/09 - Added Norwegian language
        case "Norwegian":
          return (zone) ? INDEX_ZONE_NORWEGIAN : INDEX_REASON_NORWEGIAN;
        //agrassi_FI19-08-2010_2
        case "Arabic":
          return (zone) ? INDEX_ZONE_ARABIC : INDEX_REASON_ARABIC;
        default:
					return (zone) ? INDEX_ZONE_ENGLISH : INDEX_REASON_ENGLISH;
			}
		}
	}
}