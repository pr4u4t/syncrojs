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
  /// Shift database.
  /// </summary>
  public class Shift : SyncroC.DataBase.Interfaces.IDatabase
  {
    public const string XML_FILE_NAME = "Shift.xml";
    private string fullPathXmlFile = null;

    /// <summary>String to be sent as version in case of error</summary>
    public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

    #region Shift table constants
    /// <summary>Shift table name.</summary>
    public readonly static string SHIFT_LIST = "ShiftList";

    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_ID = "ID";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_START_TIME = "StartTime";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_FINISH_TIME = "FinishTime";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_RUN_TIME = "RunTime";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_STOP_TIME = "StopTime";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_NUMBER_OF_STOP = "NumberOfStop";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SETUP_CHANGES = "SetupChanges";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_QUALITY_CHANGES = "QualityChanges";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_MANAGER = "ShiftManager";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_PEOPLE = "People";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_AVG_SPEED = "AvgSpeed";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_JOB = "Job";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_COMM = "Comm";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SPARE_1 = "Spare1";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SPARE_2 = "Spare2";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SPARE_3 = "Spare3";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SPARE_4 = "Spare4";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SPARE_5 = "Spare5";
    /// <summary>Column name (shift).</summary>
    public readonly static string SHIFT_SPARE_6 = "Spare6";
    #endregion

    #region Flute table constants
    /// <summary>Flutes on shifts table name.</summary>
    public readonly static string FLUTE_LIST = "shiftflutelist";

    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_ID_SHIFT = "IDShift";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_START_TIME_SHIFT = "StartTime";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_FLUTE = "Flute";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_DRYEND_PRODUCED_ULm = "DryEnd";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_SHEAR_SCRAP_ULm = "ShearScrap";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_DRYEND_TOTAL_ULm = "DryEndTotal";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_DRYEND_PRODUCED_UAm2 = "DryEndM2";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_DRYEND_SCRAP_UAm2 = "ScrapM2";
    /// <summary>Column name (flutes on shift).</summary>
    public readonly static string FLUTE_DRYEND_TOTAL_UAm2 = "DryEndTotalM2";
    /// <summary>Column name (shift).</summary>
    public readonly static string FLUTE_JOB = "Job";
    /// <summary>Column name (shift).</summary>
    public readonly static string FLUTE_COMM = "Comm";
    /// <summary>Column name (shift).</summary>
    public readonly static string FLUTE_SPARE_1 = "Spare1";
    /// <summary>Column name (shift).</summary>
    public readonly static string FLUTE_SPARE_2 = "Spare2";
    /// <summary>Column name (shift).</summary>
    public readonly static string FLUTE_SPARE_3 = "Spare3";
    /// <summary>Column name (shift).</summary>
    public readonly static string FLUTE_SPARE_4 = "Spare4";
    #endregion

    /// <summary>
    /// Shift dataset reference
    /// </summary>
    private DataSet shift = null;

    /// <summary>
    /// Delegate for handling OnDBChange events.
    /// </summary>
    public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

    /// <summary>
    /// Event raised when a change (delete, add or modify of a record) occurs into Shift database.
    /// </summary>
    public event DBChangedHandler OnDBShiftChange;

    public Shift()
    {
      fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
    }

    private DataSet generateDataSet()
    {
      DataSet shift = new DataSet();
      shift.CaseSensitive = true;

      #region ShiftList table

      shift.Tables.Add(Shift.SHIFT_LIST);
      //columns
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_ID);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(new DataColumn(Shift.SHIFT_START_TIME, typeof(DateTime)));
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(new DataColumn(Shift.SHIFT_FINISH_TIME, typeof(DateTime)));
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_RUN_TIME);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_STOP_TIME);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_NUMBER_OF_STOP);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SETUP_CHANGES);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_QUALITY_CHANGES);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_MANAGER);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_PEOPLE);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_AVG_SPEED);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_JOB);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_COMM);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SPARE_1);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SPARE_2);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SPARE_3);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SPARE_4);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SPARE_5);
      shift.Tables[Shift.SHIFT_LIST].Columns.Add(Shift.SHIFT_SPARE_6);

      //key
      shift.Tables[Shift.SHIFT_LIST].PrimaryKey =
        new DataColumn[] { shift.Tables[Shift.SHIFT_LIST].Columns[Shift.SHIFT_ID], shift.Tables[Shift.SHIFT_LIST].Columns[Shift.SHIFT_START_TIME] };

      #endregion

      shift.Tables.Add(this.generateDataTableFluteList());

      return shift;
    }

    private DataTable generateDataTableFluteList()
    {
      DataTable dt = new DataTable(Shift.FLUTE_LIST);

      #region FluteList table

      //columns
      dt.Columns.Add(Shift.FLUTE_ID_SHIFT);
      dt.Columns.Add(new DataColumn(Shift.FLUTE_START_TIME_SHIFT, typeof(DateTime)));
      dt.Columns.Add(Shift.FLUTE_FLUTE);
      dt.Columns.Add(Shift.FLUTE_DRYEND_PRODUCED_ULm);
      dt.Columns.Add(Shift.FLUTE_SHEAR_SCRAP_ULm);
      dt.Columns.Add(Shift.FLUTE_DRYEND_TOTAL_ULm);
      dt.Columns.Add(Shift.FLUTE_DRYEND_PRODUCED_UAm2);
      dt.Columns.Add(Shift.FLUTE_DRYEND_SCRAP_UAm2);
      dt.Columns.Add(Shift.FLUTE_DRYEND_TOTAL_UAm2);
      dt.Columns.Add(Shift.FLUTE_JOB);
      dt.Columns.Add(Shift.FLUTE_COMM);
      dt.Columns.Add(Shift.FLUTE_SPARE_1);
      dt.Columns.Add(Shift.FLUTE_SPARE_2);
      dt.Columns.Add(Shift.FLUTE_SPARE_3);
      dt.Columns.Add(Shift.FLUTE_SPARE_4);

      //key
      dt.PrimaryKey =
        new DataColumn[] { dt.Columns[Shift.FLUTE_ID_SHIFT], dt.Columns[Shift.FLUTE_START_TIME_SHIFT], dt.Columns[Shift.FLUTE_FLUTE] };

      #endregion

      return dt;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();

        return this.shift.Tables[Shift.SHIFT_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".ShiftList LIMIT 2000;");
      }
    }

    public DataTable getTableShift()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();

        return this.shift.Tables[Shift.SHIFT_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".ShiftList ORDER BY StartTime DESC LIMIT 2000;");
        dt.TableName = Shift.SHIFT_LIST;
        return dt;
      }
    }

    public DataTable getTableShiftFlute()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();

        return this.shift.Tables[Shift.FLUTE_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".ShiftFluteList ORDER BY StartTime DESC LIMIT 2000;");
        dt.TableName = Shift.FLUTE_LIST;
        return dt;
      }
    }

    public DataRow getRowFromTableShift(string shiftId, DateTime shiftStartTime)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();

        return this.shift.Tables[Shift.SHIFT_LIST].Rows.Find(new object[] { shiftId, shiftStartTime });
      }
      else
      {
        string select = Shift.SHIFT_ID + " = '" + shiftId + "' AND " + Shift.SHIFT_START_TIME + " = '" + SqlManager.dateTimeToSQLDateTime(shiftStartTime) + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".ShiftList WHERE (" + select + ") ORDER BY StartTime ASC LIMIT 2000;");
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

    public DataTable getRowFromTableShiftFlute(string shiftId, DateTime shiftStartTime)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();

        DataTable dt = this.generateDataTableFluteList();
        foreach (DataRow row in this.shift.Tables[Shift.FLUTE_LIST].Rows)
        {
          if (row[Shift.FLUTE_ID_SHIFT].ToString().Equals(shiftId) &&
            row[Shift.FLUTE_START_TIME_SHIFT].ToString().Equals(shiftStartTime.ToString()))
          {
            dt.ImportRow(row);
          }
        }

        return dt;
      }
      else
      {
        string select = Shift.FLUTE_ID_SHIFT + " = '" + shiftId + "' AND " + Shift.FLUTE_START_TIME_SHIFT + " = '" + SqlManager.dateTimeToSQLDateTime(shiftStartTime) + "'";
        return SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".ShiftFluteList WHERE (" + select + ") ORDER BY StartTime ASC LIMIT 2000;");        
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
      CommunicationManager.GetCommunicationManager().ShutDown(false, true);
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

        if (this.OnDBShiftChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBShiftChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();
      }

      string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      //check number of parameters that must be 2 minus of the columns.count 
      //because in the table there is also di "job", "comm" columns which are valorized from the SyncroC
      if (shift.Tables[Shift.SHIFT_LIST].Columns.Count - 2 != parameters.Length)
      {
        Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
        return;
      }

      object[] elements = new object[parameters.Length + 2];
      elements[0] = parameters[0];

      DateTime startTime = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[1].ToString());
      DateTime finishTime = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[2].ToString());

      if (startTime >= finishTime)
      {
        string log = "Add (Shift DB) - Error adding new record. The Start date is <= of stop Date";
        Logger.LogError(this, log);
        return;
      }

      elements[1] = startTime;
      elements[2] = finishTime;

      Array.Copy(parameters, 3, elements, 3, 8);

      elements[11] = DBManager.getDBManager().syncroConfig.GeneralSetting.Job;
      elements[12] = DBManager.getDBManager().syncroConfig.GeneralSetting.Comm;

      Array.Copy(parameters, 11, elements, 13, 6);

      try
      {
        shift.Tables[Shift.SHIFT_LIST].Rows.Add(elements);
        if (OnDBShiftChange != null)
          OnDBShiftChange(this, new DBChangedEventArgs(elements[0].ToString(), DBChangement.Insert));//todo: la key  non è esatta..
      }
      catch (System.Data.ConstraintException ex)
      {
        string log = "Add (Shift DB) - Error adding record to the Shift database for ConstraintException.";
        Logger.LogError(this, log, ex);
      }
      catch (Exception ex)
      {
        string log = "Add (Shift DB) - Error adding new record.";
        Logger.LogError(this, log, ex);
        ManageError(log);
      }
      finally
      {
        Application.DoEvents();
      }
    }

    public void AddFlute(string values)
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();

        if (this.OnDBShiftChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBShiftChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();
      }

      string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (shift.Tables[Shift.FLUTE_LIST].Columns.Count - 4 != elements.Length) //minus 4 because DRYEND_TOTAL_ULm, DRYEND_TOTAL_UAm2, Job, Comm columns are locally calculated
      {
        Logger.LogError(this, "AddFlutes - Cannot add record because the number of the parameters is incompatible with the table");
        return;
      }
      if (shift.Tables[Shift.SHIFT_LIST].Rows.Find(new object[] { elements[0], SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(elements[1]) }) == null)
      {
        Logger.LogError(this, "AddFlute - Cannot add flute (key: " + elements[0] + ", " + elements[1] + ") to the Shift database because there is no matching Shift");
        return;
      }
      try
      {
        shift.Tables[Shift.FLUTE_LIST].Rows.Add(CreateFluteElementsArray(elements));
        if (OnDBShiftChange != null)
          OnDBShiftChange(this, new DBChangedEventArgs(elements[0], DBChangement.Insert));//todo: la key  non è esatta..
      }
      catch (Exception ex)
      {
        string log = "Add (Shift DB) - Error adding a new flute data on a shift.";
        Logger.LogError(this, log, ex);
        ManageError(log);
      }
      finally
      {
        Application.DoEvents();
      }
    }

    private object[] CreateFluteElementsArray(string[] oldElements)
    {
      object[] newElements = new object[oldElements.Length + 4]; //plus 4 because DRYEND_TOTAL_ULm, DRYEND_TOTAL_UAm2, Job, Comm columns are locally calculated
      newElements[0] = oldElements[0];
      newElements[1] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(oldElements[1]);
      Array.Copy(oldElements, 2, newElements, 2, 3);
      int tot = 0;
      try
      {
        tot = Convert.ToInt32(oldElements[3]) + Convert.ToInt32(oldElements[4]);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "CreateFluteElementsArray - Error converting values \"" + oldElements[3] + "\", \"" + oldElements[4] + "\" to int32", ex);
      }
      newElements[5] = tot.ToString();
      Array.Copy(oldElements, 5, newElements, 6, 2);
      tot = 0;
      try
      {
        tot = Convert.ToInt32(oldElements[5]) + Convert.ToInt32(oldElements[6]);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "CreateFluteElementsArray - Error converting values \"" + oldElements[5] + "\", \"" + oldElements[6] + "\" to int32", ex);
      }

      newElements[8] = tot;

      newElements[9] = DBManager.getDBManager().syncroConfig.GeneralSetting.Job;
      newElements[10] = DBManager.getDBManager().syncroConfig.GeneralSetting.Comm;

      Array.Copy(oldElements, 7, newElements, 11, 4);

      return newElements;
    }

    /// <summary>
    /// Implementation of the IDatabase interface.
    /// Delete the specified record.
    /// </summary>
    /// <param name="key">Key of the record to be deleted.</param>
    public void Del(string key)
    {
      //			try
      //			{
      //				DataTable dt = paper.Tables[PAPER_LIST];
      //				DataRow dr = null;
      //				if (dt != null) dr = dt.Rows.Find(key);
      //				if (dr != null) dr.Delete();
      //				if (OnDBPaperChange != null)
      //					OnDBPaperChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
      //			}
      //			catch (Exception ex)
      //			{
      //				Logger.LogError(this, "Del", ex);
      //			}
    }

    /// <summary>
    /// Implementation of the IDatabase interface.
    /// Modify an existing record.
    /// </summary>
    /// <param name="values">a string representing the new values</param>
    public void Mod(string values)
    {
      //			string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      //			if (paper.Tables[PAPER_LIST].Columns.Count != elements.Length)
      //			{
      //				Logger.LogError(this, "Mod - Cannot add record because the number of the parameters is incompatible with the table");
      //				return;
      //			}
      //			try
      //			{
      //				paper.Tables[PAPER_LIST].Rows.Find(elements[0]).ItemArray = elements;
      //				if (OnDBPaperChange != null)
      //					OnDBPaperChange(this, new DBChangedEventArgs(elements[0], DBChangement.Modify));
      //			}
      //			catch (Exception ex)
      //			{
      //				Logger.LogError(this, "Mod - ", ex);
      //			}
    }

    /// <summary>
    /// Implementation of the IDatabase interface.
    /// Clean the dataset.
    /// </summary>
    public void Clr()
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;
      else
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();
      }

      if (shift != null)
      {
        shift.Clear();
        //call the refresh data on every instantiated form
        PageManager.getPageManager().refreshAllFormsData();
        Application.DoEvents();
      }
      else
        Logger.LogError(this, "Clr - Cannot complete, dataset was null");
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
        if (this.shift == null)
          this.shift = this.generateDataSet();
      }

      if (shift != null)
      {
        shift.Clear();
        try
        {
          shift.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
          //call the refresh data on every instantiated form
          PageManager.getPageManager().refreshAllFormsData();
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "Load - Error during reading local shift database", ex);
        }
        finally
        {
          Application.DoEvents();
        }
      }
      else
        Logger.LogError(this, "Load - Cannot complete, dataset was null");
    }

    /// <summary>
    /// Implementation of the IDatabase interface.
    /// Save permanently the database.
    /// </summary>
    public void Save()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.shift == null)
          this.shift = this.generateDataSet();

        if (shift == null)
        {
          Logger.LogError(this, "Save - Cannot complete, dataset is null");
          return;
        }
        try
        {//TODO: Calculated totals should not serialized, but it cannot be done removing columns
          //before the WriteXml() call because in this way after the save that columns are no more available
          //and it causes problems to pages which accesses them.
          //shift.Tables[Shift.FLUTE_LIST].Columns.Remove(Shift.DRYEND_TOTAL_ULm);
          //shift.Tables[Shift.FLUTE_LIST].Columns.Remove(Shift.DRYEND_TOTAL_UAm2);
          shift.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
        }
        catch (SecurityException ex)
        {
          Logger.LogError(this, "Save - an error occured during XML serialization of the Shift database;", ex);
        }
        finally
        {
          Application.DoEvents();
        }
      }
      else
      {
        try
        {
          DataSet shiftMySql = new DataSet();
          shiftMySql.Tables.Add(this.getTableShift());
          shiftMySql.Tables.Add(this.getTableShiftFlute());
          shiftMySql.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
        }
        catch (SecurityException ex)
        {
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the Shift database;", ex);
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
      if (!ver.Equals(GetVer()))
      {
        //firstly save the database
        Save();
        //update file date
        DateTime data = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(ver);
        try
        {
          File.SetLastWriteTimeUtc(fullPathXmlFile, data);
        }
        catch (FileNotFoundException ex)
        {
          Logger.LogError(this, "SetVer - can't find the file \"" + fullPathXmlFile + "\".", ex);
          return;
        }
        catch (UnauthorizedAccessException ex)
        {
          Logger.LogError(this, "SetVer - can't access the file \"" + fullPathXmlFile + "\".", ex);
          return;
        }
        catch (Exception ex)
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
      catch (UnauthorizedAccessException ex)
      {
        Logger.LogError(this, "GetVer - can't access the file \"" + fullPathXmlFile + "\".", ex);
        return WRONG_VERSION;
      }
      catch (Exception ex)
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
  }
}