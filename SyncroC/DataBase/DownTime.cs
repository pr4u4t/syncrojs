using System;
using System.Data;
using System.IO;
using System.Security;
using System.Windows.Forms;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
	/// <summary>
	/// 
	/// </summary>
	public class DownTime: SyncroC.DataBase.Interfaces.IDatabase
	{
		public const string XML_FILE_NAME = "DownTime.xml"; 
		private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

		#region downtime table constants

		/// <summary>Dataset table name.</summary>
		public readonly static string DOWNTIME_LIST = "DownTime";
		/// <summary>Column name.</summary>
		public readonly static string SHIFT_ID = "ShiftID";
		/// <summary>Column name.</summary>
		public readonly static string SHIFT_START_TIME = "ShiftStartTime";
		/// <summary>Column name.</summary>
		public readonly static string START_TIME = "StartTime";
		/// <summary>Column name.</summary>
		public readonly static string FINISH_TIME = "FinishTime";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_DATETIME_1 = "Spare1";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_DATETIME_2 = "Spare2";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_DATETIME_3 = "Spare3";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_DATETIME_4 = "Spare4";
		/// <summary>Column name.</summary>
		public readonly static string REASON_KEY = "ReasonKey";
		/// <summary>Column name.</summary>
		public readonly static string REASON_DESCRIPTION = "ReasonDescription";
		/// <summary>Column name.</summary>
		public readonly static string ZONE_KEY = "ZoneKey";
		/// <summary>Column name.</summary>
		public readonly static string ZONE_DESCRIPTION = "ZoneDescription";
		/// <summary>Column name.</summary>
		public readonly static string TEXT = "Text";
		/// <summary>Column name.</summary>
		public readonly static string SETUP_ID = "SetupID";
		/// <summary>Column name.</summary>
		public readonly static string SETUP_MODID = "SetupModID";
		/// <summary>Column name.</summary>
		public readonly static string JOB = "Job";
		/// <summary>Column name.</summary>
		public readonly static string COMM = "Comm";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_5 = "Spare5";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_6 = "Spare6";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_7 = "Spare7";
		/// <summary>Column name.</summary>
		public readonly static string SPARE_8 = "Spare8";
    /// <summary>Column name.</summary>
    public readonly static string ZONE_ENGLISH = "ZoneEnglish";
    /// <summary>Column name.</summary>
    public readonly static string REASON_ENGLISH = "ReasonEnglish";
    /// <summary>Column name.</summary>
    public readonly static string ZONE_FOSBER = "ZoneFosber";
    /// <summary>Column name.</summary>
    public readonly static string REASON_FOSBER = "ReasonFosber";

		#endregion

		/// <summary>
		/// downTime dataset reference
		/// </summary>
		private DataSet downTime = null;

    /// <summary>
    /// Delegate for handling OnDBChange events.
    /// </summary>
    public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

    /// <summary>
    /// Event raised when a change (delete, add or modify of a record) occur into Paper database.
    /// </summary>
    public event DBChangedHandler OnDBDowntimeChange;

		public DownTime()
		{
      fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
    }

    private DataSet generateDataSet()
    {
      DataSet downTime = new DataSet();
      downTime.CaseSensitive = true;

      #region DownTimeList table

      downTime.Tables.Add(DownTime.DOWNTIME_LIST);
      //columns
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SHIFT_ID);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.SHIFT_START_TIME, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.START_TIME, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.FINISH_TIME, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.SPARE_DATETIME_1, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.SPARE_DATETIME_2, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.SPARE_DATETIME_3, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.SPARE_DATETIME_4, typeof(DateTime)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.REASON_KEY);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.REASON_DESCRIPTION);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.ZONE_KEY);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.ZONE_DESCRIPTION);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.TEXT);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SETUP_ID);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SETUP_MODID);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.JOB);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.COMM);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SPARE_5);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SPARE_6);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SPARE_7);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.SPARE_8);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.ZONE_ENGLISH);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(DownTime.REASON_ENGLISH);
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.ZONE_FOSBER, typeof(bool)));
      downTime.Tables[DOWNTIME_LIST].Columns.Add(new DataColumn(DownTime.REASON_FOSBER, typeof(bool)));

      //key
      downTime.Tables[DownTime.DOWNTIME_LIST].PrimaryKey =
        new DataColumn[] { 
					downTime.Tables[DownTime.DOWNTIME_LIST].Columns[DownTime.SHIFT_ID], 
					downTime.Tables[DownTime.DOWNTIME_LIST].Columns[DownTime.START_TIME] };

      #endregion

      return downTime;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();

        return this.downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".downtimes LIMIT 2000;");
      }
    }

    public DataTable getTable(bool forSave)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();

        return this.downTime.Tables[DownTime.DOWNTIME_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".downtimes ORDER BY starttime DESC LIMIT 2000;");
        dt.TableName = DownTime.DOWNTIME_LIST;

        if (forSave)
        {
          dt.Columns.Add(DownTime.ZONE_ENGLISH);
          dt.Columns.Add(DownTime.REASON_ENGLISH);
          dt.Columns.Add(new DataColumn(DownTime.ZONE_FOSBER, typeof(bool)));
          dt.Columns.Add(new DataColumn(DownTime.REASON_FOSBER, typeof(bool)));

          foreach (DataRow row in dt.Rows)
          {
            row[DownTime.ZONE_ENGLISH] = "";
            row[DownTime.REASON_ENGLISH] = "";
            row[DownTime.ZONE_FOSBER] = false;
            row[DownTime.REASON_FOSBER] = false;

            string zoneKey = (string)row[DownTime.ZONE_KEY];
            string reasonKey = (string)row[DownTime.REASON_KEY];

            if ((!zoneKey.Equals(string.Empty)) && (!reasonKey.Equals(string.Empty)))
            {
              if (DBManager.getDBManager().downTimeReason.downTimeReason.Tables.Count >= 2)
              {
                DataRow rowZone = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE].Rows.Find(zoneKey);
                DataRow rowReason = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON].Rows.Find(reasonKey);
                if ((rowZone != null) && (rowReason != null))
                {
                  row[DownTime.ZONE_ENGLISH] = rowZone[DownTimeReason.INDEX_ZONE_ENGLISH];
                  row[DownTime.REASON_ENGLISH] = rowReason[DownTimeReason.INDEX_REASON_ENGLISH];
                  try
                  {
                    row[DownTime.ZONE_FOSBER] = rowZone[DownTimeReason.INDEX_ZONE_FOSBER];
                    row[DownTime.REASON_FOSBER] = rowReason[DownTimeReason.INDEX_REASON_FOSBER];
                  }
                  catch (Exception e)
                  {
                    Logger.LogError(this, e);
                    row[DownTime.ZONE_FOSBER] = false;
                    row[DownTime.REASON_FOSBER] = false;
                  }
                }
              }
            }
          }
        }

        return dt;
      }
    }

    public DataRow getRow(string shiftID, DateTime downtimestarttime)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();

        DataRow row = null;
        try
        {
          row = downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Find(new object[] { shiftID, downtimestarttime });
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "getRow - error getting downTime row (id:" + shiftID + ", " + downtimestarttime.ToString() + ")", ex);
        }
        return row;
      }
      else
      {
        string select = DownTime.SHIFT_ID + " = '" + shiftID + "' AND " + DownTime.START_TIME + " = '" + SqlManager.dateTimeToSQLDateTime(downtimestarttime) + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".downtimes WHERE (" + select + ") ORDER BY starttime ASC LIMIT 2000;");
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            return dt.Rows[0];
          }
        }
        return null;
      }
    }

		private void ManageError(string text)
		{
			DBManager.getDBManager().DBError = true;
			try
			{
				File.Delete(fullPathXmlFile);
			}
			catch (Exception)
			{
			}
			DialogResult ret = MessageBox.Show(text, "SYNCRO DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			CommunicationManager.GetCommunicationManager().ShutDown(false,true);
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values</param>
		public void Add(string values)
		{
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();

        if (this.OnDBDowntimeChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBDowntimeChange(this, new DBChangedEventArgs(sqlelements[0] + Command.PARAMETER_SEPARATOR + sqlelements[1], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();
      }

			string[] strings = values.Split(new Char[] {Command.PARAMETER_SEPARATOR});
      if (downTime.Tables[DownTime.DOWNTIME_LIST].Columns.Count - 6 != strings.Length)//minus 6 because job, comm, ZoneEnglish, ReasonEnglish, ZoneFosber, ReasonFosber are not in the protocol but locally calculated
			{
				Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
				return;
			}
			object[] elements = CreateElementsArray(strings);
			try 
			{
				downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Add(elements);
			}
			catch (ConstraintException ex)
			{
				DataRow alreadyPresentRow = downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Find(new object[] { elements[0], elements[2] });
				string log = "Add (DownTime DB) - Error adding new record. Attempted to add row:\n" + values;
				if (alreadyPresentRow != null)
					log += "\n but " + alreadyPresentRow.ToString() + " is already into the database.";
				Logger.LogError(this, log, ex);
				//ManageError(log);
			}
			catch (Exception ex)
			{
				string log = "Add (DownTime DB) - Error adding new record.";
				Logger.LogError(this, log, ex);
				ManageError(log);
			}
			finally
			{
				Application.DoEvents();
			}
		}
		
		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Delete the specified record.
		/// </summary>
		/// <param name="key">Key of the record to be deleted.</param>
		public void Del(string key)
		{
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Modify an existing record.
		/// </summary>
		/// <param name="values">a string representing the new values</param>
		public void Mod(string values)
		{
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();

        if (this.OnDBDowntimeChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBDowntimeChange(this, new DBChangedEventArgs(sqlelements[0] + Command.PARAMETER_SEPARATOR + sqlelements[1], DBChangement.Modify));
        }

        return;
      }
      else
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();
      }

			string[] strings = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (downTime.Tables[DownTime.DOWNTIME_LIST].Columns.Count - 6 != strings.Length)//minus 6 because job, comm, ZoneEnglish, ReasonEnglish, ZoneFosber, ReasonFosber are not in the protocol but locally calculated
			{
				Logger.LogError(this, "Mod - Cannot update record because the number of the parameters is incompatible with the table");
				return;
			}
			try 
			{
				object[] objects = CreateElementsArray(strings);

				DateTime starttime = (DateTime) objects[2];
				DateTime finishTime = (DateTime)objects[3];

				if ((starttime >= finishTime) && (finishTime != DateTime.MinValue))
				{
					string log = "Add (DownTime DB) - Error adding new record. The Start date is <= of stop Date";
					Logger.LogError(this, log);
					downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Find(new object[] { objects[0], objects[2] }).Delete();
					return;
				}

				downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Find(
					new object[] { objects[0], objects[2] }).ItemArray = objects;
			}
			catch (Exception ex)
			{
				string log = "Mod (DownTime DB) - Error modifing record.";
				Logger.LogError(this, log, ex);
				ManageError(log);
			}
			finally
			{
				Application.DoEvents();
			}
		}

		private object[] CreateElementsArray(string[] strings)
		{
      object[] objects = new object[strings.Length + 6]; //plus 6; job, comm, ZoneEnglish, ReasonEnglish, ZoneFosber, ReasonFosber: which are not into the protocol but just set from syncroC
			objects[0] = strings[0];
			for (int k = 1; k <= 7; k++)
				objects[k] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime((string)strings[k]);
      Array.Copy(strings, 8, objects, 8, 7);
      //agrassi_FI13-03-08_1
      if ((strings[8].Trim() == "") || ((strings[10].Trim() == "")))
      {
        objects[8] = "0000";
        objects[10] = "00";
        DataRowCollection reasons = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON].Rows;
        DataRowCollection zones = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE].Rows;
        DataRow reasonCustRec = reasons.Find(objects[8]);
        if (reasonCustRec != null)
          objects[9] = reasonCustRec[DBManager.getDBManager().downTimeReason.getIndexReasonFromFirstLanguage()].ToString();
        DataRow zoneCustRec = zones.Find(objects[10]);
        if (zoneCustRec != null)
          objects[11] = zoneCustRec[DBManager.getDBManager().downTimeReason.getIndexZoneFromFirstLanguage()].ToString();
      }
			objects[15] = DBManager.getDBManager().syncroConfig.GeneralSetting.Job;
			objects[16] = DBManager.getDBManager().syncroConfig.GeneralSetting.Comm;
			Array.Copy(strings, 15, objects, 17, 4);
      objects[21] = "";
      objects[22] = "";
      objects[23] = false;
      objects[24] = false;
      this.SetFosberFlags(objects);
      return objects;
		}

    private void SetFosberFlags(object[] objects)
    {
      if ((objects[8] != null) && (objects[10] != null))
      {
        string zoneKey = (string)objects[10];
        string reasonKey = (string)objects[8];
        if ((!zoneKey.Equals(string.Empty)) && (!reasonKey.Equals(string.Empty)))
        {
          if (DBManager.getDBManager().downTimeReason.downTimeReason.Tables.Count >= 2)
          {
            DataRow rowZone = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE].Rows.Find(zoneKey);
            DataRow rowReason = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON].Rows.Find(reasonKey);
            if ((rowZone != null) && (rowReason != null))
            {
              objects[21] = rowZone[DownTimeReason.INDEX_ZONE_ENGLISH];
              objects[22] = rowReason[DownTimeReason.INDEX_REASON_ENGLISH];
              try
              {
                objects[23] = rowZone[DownTimeReason.INDEX_ZONE_FOSBER];
                //agrassi_FI05-05-2010_1
                //objects[24] = rowReason[DownTimeReason.INDEX_REASON_FOSBER];
                objects[24] = bool.Parse(rowReason[DownTimeReason.INDEX_REASON_FOSBER].ToString()) && bool.Parse(rowZone[DownTimeReason.INDEX_ZONE_FOSBER].ToString());
              }
              catch (Exception e)
              {
                Logger.LogError(this, e);
                objects[23] = false;
                objects[24] = false;
              }
            }
          }
        }
      }
    }

    public void UpdateFosberFlags()
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;
      else
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();
      }

      if (this.downTime.Tables.Count>=1)
      {
        for (int i = 0; i < this.downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Count; i++)
        {
          object[] objects = this.downTime.Tables[DownTime.DOWNTIME_LIST].Rows[i].ItemArray;
          this.SetFosberFlags(objects);
          this.downTime.Tables[DownTime.DOWNTIME_LIST].Rows[i].ItemArray = objects;
        }
      }
    }

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Clean the dataset.
		/// </summary>
		public void Clr()
		{
			if (downTime != null)
			{
				downTime.Clear();
				Application.DoEvents();
			}
			else
			{
				Logger.LogError(this, "Clr - Cannot complete, dataset was null");
			}
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
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;
      else
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();
      }

			if (downTime != null)
			{
				downTime.Clear();
				try
				{
					downTime.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - Error during reading local downTime database", ex);
				}
				finally
				{
					Application.DoEvents();
				}
			}
			else
			{
				Logger.LogError(this, "Load - Cannot complete, dataset was null");
			}
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Save permanently the database.
		/// </summary>
		public void Save()
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (downTime != null)
        {
          try
          {
            downTime.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the downTime database;", ex);
          }
          finally
          {
            Application.DoEvents();
          }
        }
        else
        {
          Logger.LogError(this, "Save - Cannot complete, dataset was null");
        }
      }
      else
      {
        try
        {
          DataSet downTimeMySql = new DataSet();
          downTimeMySql.Tables.Add(this.getTable(true));
          downTimeMySql.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
        }
        catch (SecurityException ex)
        {
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the downTime database;", ex);
        }
        finally
        {
          Application.DoEvents();
        }
      }
		}

		/// <summary>
		/// Set the database version.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <param name="ver">a Syncro style DateTime string that represent the date when the version was created.</param>
		public void SetVer(string ver)
		{
			if (! ver.Equals(GetVer()))
			{
				//firstly save the database
				Save();
				//update file date
				DateTime data = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(ver);
				try
				{
					File.SetLastWriteTimeUtc(fullPathXmlFile, data);
				}
				catch(FileNotFoundException ex)
				{
					Logger.LogError(this, "SetVer - can't find the file \"" + fullPathXmlFile + "\".", ex);
					return;
				}
				catch(UnauthorizedAccessException ex)
				{
					Logger.LogError(this, "SetVer - can't access the file \"" + fullPathXmlFile + "\".", ex);
					return;
				}
				catch(Exception ex)
				{
					Logger.LogError(this, "SetVer - error trying to change the date of the file \"" + fullPathXmlFile + "\".", ex);
					return;
				}
				finally
				{
					Application.DoEvents();
				}
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
			DateTime data;
			try
			{
				data = File.GetLastWriteTimeUtc(fullPathXmlFile);
			}
			catch (IOException ex)
			{
				Logger.LogError(this, "GetVer - can't find the file \"" + fullPathXmlFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch(UnauthorizedAccessException ex)
			{
				Logger.LogError(this, "GetVer - can't access the file \"" + fullPathXmlFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch(Exception ex)
			{
				Logger.LogError(this, "GetVer - error trying to get the date of the file \"" + fullPathXmlFile + "\".", ex);
				return WRONG_VERSION;
			}
			finally
			{
				Application.DoEvents();
			}
			return SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeStringNoZerosInFront(data);
		}

		public bool UnassignedExist()
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.downTime == null)
          this.downTime = this.generateDataSet();

        if (downTime == null && !downTime.Tables.Contains(DownTime.DOWNTIME_LIST))
        {
          Logger.LogError(this, "UnassignedExist - dataset or table invalid.");
          return true;
        }
        else
        {
          foreach (DataRow row in downTime.Tables[DownTime.DOWNTIME_LIST].Rows)
          {
            //agrassi_FI13-03-08_1
            //if (row[DownTime.ZONE_KEY].ToString().Equals(String.Empty) ||
            //  row[DownTime.REASON_KEY].ToString().Equals(String.Empty))
            if (row[DownTime.ZONE_KEY].ToString().Equals(String.Empty) ||
                row[DownTime.REASON_KEY].ToString().Equals(String.Empty) ||
                row[DownTime.ZONE_KEY].ToString().Equals("00"))
              return true;
          }
        }
        return false;
      }
      else
      {
        string select = DownTime.ZONE_KEY + " = '' OR " + DownTime.ZONE_KEY + " = '00' OR " + DownTime.REASON_KEY + " = ''";
        long res = SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".downtimes WHERE (" + select + ") LIMIT 2000;");
        return (res != 0);
      }
		}

    //agrassi_FI07-07-08
    public void TranslateDowntimes()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        //MLAMIONI_FI18-04-2011_Se non esiste ancora alcun downtime non proseguo (altrimenti crashava)
        //inizio
        if (this.downTime == null)
          this.downTime = this.generateDataSet();
        //fine
        DataRowCollection reasons = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON].Rows;
        DataRowCollection zones = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE].Rows;
        foreach (DataRow dr in this.downTime.Tables[0].Rows)
        {
          DataRow reasonCustRec = reasons.Find(dr[REASON_KEY]);
          if (reasonCustRec != null)
            //agrassiFI02-07-09_1
            if (DBManager.getDBManager().downTimeReason.getIndexReasonFromFirstLanguage() < reasonCustRec.ItemArray.Length)
              dr[ZONE_DESCRIPTION] = reasonCustRec[DBManager.getDBManager().downTimeReason.getIndexReasonFromFirstLanguage()].ToString();
          DataRow zoneCustRec = zones.Find(dr[ZONE_KEY]);
          if (zoneCustRec != null)
            //agrassiFI02-07-09_1
            if (DBManager.getDBManager().downTimeReason.getIndexZoneFromFirstLanguage() < zoneCustRec.ItemArray.Length)
              dr[REASON_DESCRIPTION] = zoneCustRec[DBManager.getDBManager().downTimeReason.getIndexZoneFromFirstLanguage()].ToString();
        }
      }
    }
	}
}

