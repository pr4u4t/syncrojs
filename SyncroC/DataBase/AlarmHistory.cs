using System;
using System.Data;
using System.Security;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Enumerates the options to be used in time filter's database
	/// </summary>
	public enum TimeFilterOptions
	{ 
		LastTenMinutes,
		LastHour,
		LastDay,
		Always
	}

	/// <summary>
	/// Summary description for Alarm.
	/// </summary>
	public class AlarmHistory : SyncroC.DataBase.Interfaces.IDatabase
	{
    //agrassi_FI06-03-08
    //private const string XML_FILE_NAME = "AlarmHistory.xml";
    public const string XML_FILE_NAME = "AlarmHistory.xml";
    private const string DATASET_NAME = "AlarmHistory";
		private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

		#region AlarmHistoryList table constants

		//MACHINES TABLE NAMES
		//syncro
		public readonly static string ALARM_HISTORY_LIST_SYC_HW = "SYCHW";
		public readonly static string ALARM_HISTORY_LIST_SYC_SW = "SYCSW";
		//wet end
		public readonly static string ALARM_HISTORY_LIST_SPLICER7 = "SPL7";
		public readonly static string ALARM_HISTORY_LIST_SF3 = "SF3";
		public readonly static string ALARM_HISTORY_LIST_SPLICER6 = "SPL6";
		public readonly static string ALARM_HISTORY_LIST_SPLICER5 = "SPL5";
		public readonly static string ALARM_HISTORY_LIST_SF2 = "SF2";
		public readonly static string ALARM_HISTORY_LIST_SPLICER4 = "SPL4";
		public readonly static string ALARM_HISTORY_LIST_SPLICER3 = "SPL3";
		public readonly static string ALARM_HISTORY_LIST_SF1 = "SF1";
		public readonly static string ALARM_HISTORY_LIST_SPLICER2 = "SPL2";
		public readonly static string ALARM_HISTORY_LIST_SPLICER1 = "SPL1";
		public readonly static string ALARM_HISTORY_LIST_TERN = "PHT";
		public readonly static string ALARM_HISTORY_LIST_GLUEUNIT = "GU";
		public readonly static string ALARM_HISTORY_LIST_DOUBLEBACKER = "DB";
		//dry end
		public readonly static string ALARM_HISTORY_LIST_ROTARY = "RSH";
		public readonly static string ALARM_HISTORY_LIST_SLITTERSCORER = "SS";
		public readonly static string ALARM_HISTORY_LIST_KNIFE = "KN";
		public readonly static string ALARM_HISTORY_LIST_STACKER1 = "STK1";
		public readonly static string ALARM_HISTORY_LIST_STACKER2 = "STK2";
		public readonly static string ALARM_HISTORY_LIST_STACKER3 = "STK3";
		public readonly static string ALARM_HISTORY_LIST_MTH = "MTH";
		public readonly static string ALARM_HISTORY_LIST_EXTERNAL_WEC = "EXTWEC";
		//VERSIONS TABLE NAME
		public readonly static string ALARM_HISTORY_VERSIONS = "VERSIONS";
	
		//MACHINE COLUMNS
		/// <summary>AlarmHistoryList column name for the Alarm Code.</summary>
		public readonly static string KEY = "Key";
		/// <summary>AlarmHistoryList column name for the Alarm Description.</summary>
		public readonly static string DESCRIPTION = "Description";
		/// <summary>AlarmHistoryList column name for the Code of the Zone.</summary>
		public readonly static string CODE_ZONE = "CodeZone";
		/// <summary>AlarmHistoryList column name for the Description of the Zone.</summary>
		public readonly static string DESCRIPTION_ZONE = "DescriptionZone";
		/// <summary>AlarmHistoryList column name for Begin Time.</summary>
		public readonly static string BEGIN_TIME = "BeginTime";
		/// <summary>AlarmHistoryList column name for End Time.</summary>
		public readonly static string END_TIME = "EndTime";

		#endregion

		/// <summary>Used to load data from the local file</summary>
		private XmlDocument XmlDoc;

		/// <summary>
		/// AlarmHistory dataset reference
		/// </summary>
		public DataSet alarmHistory = null;

		/// <summary>
		/// Delegate for handling OnDBChange events.
		/// </summary>
		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

		/// <summary>
		/// Event raised when a changement (delete, add or modify of a record) occurs into AlarmHistory database.
		/// </summary>
		public event DBChangedHandler OnDBAlarmHistoryChange;

		public AlarmHistory()
		{
			Settings s = Settings.getSettings();
      fullPathXmlFile = s.paths.ClientData + XML_FILE_NAME;

			alarmHistory = new DataSet();
			alarmHistory.CaseSensitive = true;
			alarmHistory.DataSetName = DATASET_NAME;

			#region AlarmHistoryList table

			//MACHINES TABLE
			//syncro
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SYC_HW);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SYC_SW);
			//wet end
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER7);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SF3);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER6);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER5);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SF2);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER4);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER3);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SF1);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER2);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SPLICER1);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_TERN);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_GLUEUNIT);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_DOUBLEBACKER);
			//dry end
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_ROTARY);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_SLITTERSCORER);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_KNIFE);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_STACKER1);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_STACKER2);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_STACKER3);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_MTH);
			alarmHistory.Tables.Add(ALARM_HISTORY_LIST_EXTERNAL_WEC);
			
			foreach (DataTable dataTable in alarmHistory.Tables)
			{
				//MACHINE COLUMNS
				dataTable.Columns.Add(AlarmHistory.KEY); //from AlarmsHistory.xml
				dataTable.Columns[AlarmHistory.KEY].AllowDBNull = false;
				dataTable.Columns.Add(AlarmHistory.DESCRIPTION);//from Alarm.xml translations file
				dataTable.Columns.Add(AlarmHistory.CODE_ZONE);//from Alarm.xml translations file
				dataTable.Columns.Add(AlarmHistory.DESCRIPTION_ZONE);//from Message.xml translations file
				dataTable.Columns.Add(new DataColumn(AlarmHistory.BEGIN_TIME, typeof(DateTime)));//from AlarmsHistory.xml
				dataTable.Columns.Add(new DataColumn(AlarmHistory.END_TIME, typeof(DateTime)));//from AlarmsHistory.xml
			}

			//VERSIONS TABLE
			alarmHistory.Tables.Add(ALARM_HISTORY_VERSIONS);
			//VERSIONS COLUMNS
			//syncro
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SYC_HW);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SYC_SW);
			//wet end
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER7);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SF3);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER6);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER5);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SF2);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER4);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER3);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SF1);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER2);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SPLICER1);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_TERN);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_GLUEUNIT);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_DOUBLEBACKER);
			//dry end
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_ROTARY);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_SLITTERSCORER);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_KNIFE);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_STACKER1);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_STACKER2);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_STACKER3);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_MTH);
			alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Add(ALARM_HISTORY_LIST_EXTERNAL_WEC);

			#endregion

			this.Load();
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values, preceeded by the machine name.</param>
		public void Add(string values)
		{
			int index = values.IndexOf(Command.PARAMETER_SEPARATOR);
			if (!(index > 0 || index < values.Length))
			{
				Logger.LogError(this, "Add - wrong parameter \"" + values + "\"");
				return;
			}
			string machineName = values.Substring(0, index).ToUpper();
			values = values.Remove(0, index + 1);
			if (alarmHistory == null || !alarmHistory.Tables.Contains(machineName) || alarmHistory.Tables[machineName] == null)
			{
				Logger.LogError(this, "Add - \"" + machineName + "\" is not present into the dataset.");
				return;
			}
			string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (parameters.Length != this.alarmHistory.Tables[machineName].Columns.Count - 3) //minus 3 because columns Description, CodeZone and DescriptionZone are not included in parameters list
			{
				Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
				return;
			}
			try
			{
				Alarm alm = DBManager.getDBManager().alarm;
				//columns Zone and Description need to be added
				object[] elements = new object[parameters.Length + 3];
				elements[0] = parameters[0]; //key
				elements[1] = alm.getTranslatedAlarm(parameters[0]); //description
				elements[2] = alm.getZoneCodeFromAlarm(parameters[0]);//code zone
				elements[3] = alm.getTranslatedZoneFromAlarm(parameters[0]);//description zone
				elements[4] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[1]);//begin time
				elements[5] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[2]);//end time

				this.alarmHistory.Tables[machineName].Rows.Add(elements);
				if (this.OnDBAlarmHistoryChange != null)
					this.OnDBAlarmHistoryChange(this, new DBChangedEventArgs(parameters[0], DBChangement.Insert));
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "Add - error adding new record", ex);
			}
			finally
			{
				Application.DoEvents();
			}
		}

    //agrassi_FI07-07-08
    public void TranslateAlarms()
    {
      Alarm alm = DBManager.getDBManager().alarm;
      foreach (DataTable dt in this.alarmHistory.Tables)
      {
        if (dt.TableName != ALARM_HISTORY_VERSIONS)
        {
          foreach (DataRow dr in dt.Rows)
          {
            dr[DESCRIPTION] = alm.getTranslatedAlarm(dr[KEY].ToString());
            dr[DESCRIPTION_ZONE] = alm.getTranslatedZoneFromAlarm(dr[KEY].ToString());
          }
        }
      }
    }

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Delete the specified record.
		/// </summary>
		/// <param name="key">Key of the record to be deleted.</param>
		public void Del(string key)
		{
			Logger.LogError(this, "The method \"Del\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Modify an existing record.
		/// </summary>
		/// <param name="values">a string representing the new values</param>
		public void Mod(string values)
		{
			Logger.LogError(this, "The method \"Mod\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Clean the dataset.
		/// </summary>
		public void Clr()
		{
			if (this.alarmHistory != null)
			{
				this.alarmHistory.Clear();
				Application.DoEvents();
			}
		}

		/// <summary>
		/// Clean the table.
		/// </summary>
		public void ClrMachine(string machineName)
		{
			if ((this.alarmHistory == null) || !alarmHistory.Tables.Contains(machineName) || (this.alarmHistory.Tables[machineName] == null))
			{
				Logger.LogError(this, "ClrMachine - \"" + machineName + "\" is not present into the dataset.");
				return;
			}
			this.alarmHistory.Tables[machineName].Clear();
			PageManager.getPageManager().refreshAllFormsData();
			Application.DoEvents();
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Compare the given cheksum with the database checksum.
		/// </summary>
		/// <param name="checksum">the checksum to compare</param>
		public void Chk(string checksum)
		{
			Logger.LogError(this, "The method \"Chk\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Load the databse.
		/// </summary>
		public void Load()
		{
			if (this.alarmHistory == null || !this.alarmHistory.Tables.Contains(ALARM_HISTORY_VERSIONS) || this.alarmHistory.Tables[ALARM_HISTORY_VERSIONS] == null)
			{
				Logger.LogError(this, "Load - \"" + ALARM_HISTORY_VERSIONS + "\", which is important, is not present into the dataset.");
				return;
			}
			bool err = false;
			XmlDoc = new XmlDocument();
			this.alarmHistory.Clear();
			string XPath = "//" + ALARM_HISTORY_VERSIONS; //select the only node containing the versions of each table present into the dataset
			try
			{
				XmlDoc.Load(fullPathXmlFile);
				XmlNode VersionsNode = XmlDoc.SelectSingleNode(XPath);
				this.alarmHistory.ReadXml(new XmlNodeReader(VersionsNode), XmlReadMode.IgnoreSchema);
			}
			catch (XmlException XmlEx)
			{	//maybe thrown by the XmlDoc.Load method
				Logger.LogError(this, "Load - Error during reading local AlarmHistory database, the XMLDocument and the dataset are still empty.", XmlEx);
				err = true;
			}
			catch (XPathException XPathEx)
			{	//maybe thrown processing the XPath expression
				Logger.LogError(this, "Load - Error processing the XPath expression \"" + XPath + "\"", XPathEx);
				err = true;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "Load - Error during execution of XPath \"" + XPath + "\" on XmlDocument or reading the node\"" + ALARM_HISTORY_VERSIONS + "\" into the dataset.", ex);
				err = true;
			}
			finally
			{
				Application.DoEvents();
			}
			if (err)
			{
				alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows.Add(alarmHistory.Tables[ALARM_HISTORY_VERSIONS].NewRow());
				if (alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows.Count <= 0)
				{
					Logger.LogError(this, "Load - Error adding record  to the \"" + ALARM_HISTORY_VERSIONS + "\" table");
				}
				else
				{
					foreach (DataColumn columns in alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns)
						alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows[0][columns] = AlarmHistory.WRONG_VERSION;
				}
			}
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Load the databse.
		/// </summary>
		public void LoadMachine(string machineName)
		{
			machineName = machineName.ToUpper();
			if (alarmHistory == null || !alarmHistory.Tables.Contains(machineName) || alarmHistory.Tables[machineName] == null)
			{
				Logger.LogError(this, "LoadMachine - \"" + machineName + "\" is not present into the dataset.");
				return;
			}
			bool err = false;
			this.alarmHistory.Tables[machineName].Clear();
			string XPath = "//" + DATASET_NAME + "/" + machineName; //select all the nodes about the machine specified as parameter
			try
			{
				XmlNodeList machineNodeList = XmlDoc.SelectNodes(XPath);
				foreach (XmlNode xmlNode in machineNodeList)
					this.alarmHistory.ReadXml(new XmlNodeReader(xmlNode));
				if (alarmHistory.Tables[machineName].Columns.Contains(CODE_ZONE) && alarmHistory.Tables[machineName].Columns.Contains(DESCRIPTION))
				{
					Alarm alm = DBManager.getDBManager().alarm;
					foreach (DataRow row in alarmHistory.Tables[machineName].Rows)
					{
						row[DESCRIPTION] = alm.getTranslatedAlarm(row[KEY].ToString());
						row[CODE_ZONE] = alm.getZoneCodeFromAlarm(row[KEY].ToString());
						row[DESCRIPTION_ZONE] = alm.getTranslatedZoneFromAlarm(row[KEY].ToString());
					}
				}
				PageManager.getPageManager().refreshAllFormsData();
			}
			catch (XPathException XPathEx)
			{	//maybe thrown processing the XPath expression
				Logger.LogError(this, "LoadMachine - Error processing the XPath expression \"" + XPath + "\"", XPathEx);
				err = true;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "LoadMachine - Error during execution of XPath \"" + XPath + "\" on XmlDocument or reading the nodes\"" + machineName + "\" into the dataset.", ex);
				err = true;
			}
			finally
			{
				Application.DoEvents();
			}
			if (err)
			{
				if (alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows.Count <= 0)
				{
					Logger.LogError(this, "LoadMachine - Error adding record  to the \"" + ALARM_HISTORY_VERSIONS + "\" table");
				}
				else
				{
					alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows[0][machineName] = AlarmHistory.WRONG_VERSION;
				}
			}
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Save permanently the database.
		/// </summary>
		public void Save()
		{
			if (alarmHistory == null)
			{
				Logger.LogError(this, "Save - Cannot complete, dataset is null");
				return;
			}
			try
			{	//
				//TODO: Description, Zone and DescriptionZone should not serialized, but it cannot be done removing columns
				//before the WriteXml() call because in this way after the save that columns are no more available
				//and it causes problems to pages which accesses them.
				//foreach (DataTable dataTable in alarmHistory.Tables)
				//  if (!dataTable.TableName.Equals(ALARM_HISTORY_VERSIONS))
				//    if (dataTable.Columns.Contains(CODE_ZONE) && dataTable.Columns.Contains(DESCRIPTION))
				//    {
				//      dataTable.Columns.Remove(DESCRIPTION);
				//      dataTable.Columns.Remove(CODE_ZONE);
				//      dataTable.Columns.Remove(DESCRIPTION_ZONE);
				//    }				
				this.alarmHistory.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
			}
			catch (SecurityException ex)
			{
				Logger.LogError(this, "Save - an error occured during XML serialization of the Alarm History database;", ex);
			}
			finally
			{
				Application.DoEvents();
			}
		}

		/// <summary>
		/// Set the database version.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <param name="ver">a Syncro style DateTime string that represent the date when the version was created, 
		/// preceeded from the machine name the version refers to.</param>
		public void SetVer(string ver)
		{
			string machineName = "ALL";
			try
			{
				if (ver != "ALL")
				{
					int index = ver.IndexOf(Command.PARAMETER_SEPARATOR);
					if (!(index > 0 || index < ver.Length))
					{
						Logger.LogError(this, "SetVer - wrong parameter \"" + ver + "\"");
						return;
					}
					machineName = ver.Substring(0, index);
					if (alarmHistory == null || !alarmHistory.Tables.Contains(ALARM_HISTORY_VERSIONS)
						|| alarmHistory.Tables[ALARM_HISTORY_VERSIONS] == null || !alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Contains(machineName))
					{
						Logger.LogError(this, "SetVer - \"" + machineName + "\" is not present into the\"" + ALARM_HISTORY_VERSIONS + "\"table.");
						return;
					}
					ver = ver.Remove(0, index + 1);
					if (ver != this.GetVer(machineName))
						this.alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows[0][machineName] = ver;
				}
				else
					this.Save();
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "SetVer - error trying to set the date of the table \"" + machineName + "\".", ex);
				return;
			}
			finally
			{
				Application.DoEvents();
			}
		}

		/// <summary>
		/// Get the database version as a string.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <returns>a Syncro style DateTime string that represent the date when the version was created.
		/// In case of error return the following string "1 1 1 1 1 1 1 1"</returns>
		public string GetVer()
		{
			return AlarmHistory.WRONG_VERSION;
		}

		/// <summary>
		/// Get the database version as a string.
		/// </summary>
		/// <returns>a SYSTEMTIME style string that represent the date when the version was created.
		/// In case of error return the following string "1 1 1 1 1 1 1 1"</returns>
		public string GetVer(string machineName)
		{
			if (alarmHistory == null || !alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Columns.Contains(machineName) || alarmHistory.Tables[machineName] == null)
			{
				Logger.LogError(this, "GetVer - \"" + machineName + "\" is not present into the\"" + ALARM_HISTORY_VERSIONS + "\"table.");
				return AlarmHistory.WRONG_VERSION;
			}
			try
			{
				return this.alarmHistory.Tables[ALARM_HISTORY_VERSIONS].Rows[0][machineName].ToString();
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "GetVer - error trying to get the date of the table \"" + machineName + "\".", ex);
				return AlarmHistory.WRONG_VERSION;
			}
			finally
			{
				Application.DoEvents();
			}
		}
	}
}
