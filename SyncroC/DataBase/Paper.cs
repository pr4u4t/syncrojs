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
  /// Summary description for Paper.
  /// </summary>
  public class Paper : SyncroC.DataBase.Interfaces.IDatabase
  {
    private const string XML_FILE_NAME = "Paper.xml";
    private string fullPathXmlFile = null;

    /// <summary>String to be sent as version in case of error</summary>
    public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

    #region PaperList table constants

    //constants
    /// <summary>Dataset table name.</summary>
    public readonly static string PAPER_LIST = "PaperList";

    /// <summary>Column name.</summary>
    public readonly static string PAPER_ID = "paperid";
    /// <summary>Column name.</summary>
    public readonly static string CORE_DIAMETER = "CoreDiameter_UDdmm";
    /// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR = "WeightFactor_UWg_m";
    /// <summary>Column name.</summary>
    public readonly static string TEXT = "Text";
    /// <summary>Column name.</summary>
    public readonly static string DEFAULT_THICKNESS = "DefaultThickness_UDdmmm";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_0 = "maxSpliceDBSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_1 = "maxSpliceSF1LinerSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_2 = "maxSpliceSF1FlutingSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_3 = "maxSpliceSF2LinerSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_4 = "maxSpliceSF2FlutingSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_5 = "maxSpliceSF3LinerSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string MAX_SPLICE_SPEED_6 = "maxSpliceSF3FlutingSpeed_UVm_m";
    /// <summary>Column name.</summary>
    public readonly static string PAPER_CLASSIFICATION = "paperClassification";

    #endregion

    /// <summary>
    /// papers dataset reference
    /// </summary>
    private DataSet paper = null;

    /// <summary>
    /// Delegate for handling OnDBChange events.
    /// </summary>
    public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

    /// <summary>
    /// Event raised when a change (delete, add or modify of a record) occur into Paper database.
    /// </summary>
    public event DBChangedHandler OnDBPaperChange;

    public Paper()
    {
      fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
    }

    private DataSet generateDataSet()
    {
      DataSet paper = new DataSet();
      paper.CaseSensitive = true;

      #region PaperList table
      paper.Tables.Add(PAPER_LIST);
      //columns
      paper.Tables[PAPER_LIST].Columns.Add(PAPER_ID);
      paper.Tables[PAPER_LIST].Columns.Add(CORE_DIAMETER);
      paper.Tables[PAPER_LIST].Columns.Add(WEIGHT_FACTOR);
      paper.Tables[PAPER_LIST].Columns.Add(TEXT);
      paper.Tables[PAPER_LIST].Columns.Add(DEFAULT_THICKNESS);
      paper.Tables[PAPER_LIST].Columns.Add(PAPER_CLASSIFICATION);
      paper.Tables[PAPER_LIST].Columns.Add("SPARE_2");
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_0);
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_1);
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_2);
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_3);
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_4);
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_5);
      paper.Tables[PAPER_LIST].Columns.Add(MAX_SPLICE_SPEED_6);
      paper.Tables[PAPER_LIST].Columns.Add("SPARE_3");
      paper.Tables[PAPER_LIST].Columns.Add("SPARE_4");
      paper.Tables[PAPER_LIST].Columns.Add("SPARE_5");

      //key
      paper.Tables[PAPER_LIST].PrimaryKey =
        new DataColumn[] { paper.Tables[PAPER_LIST].Columns[PAPER_ID], paper.Tables[PAPER_LIST].Columns[WEIGHT_FACTOR] };

      #endregion

      return paper;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();

        return this.paper.Tables[Paper.PAPER_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".papers LIMIT 2000;");
      }
    }

    public DataTable getTable()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();

        return this.paper.Tables[Paper.PAPER_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".papers ORDER BY paperid ASC LIMIT 2000;");
        dt.TableName = Paper.PAPER_LIST;
        return dt;
      }
    }

    public DataRow getRow(string paperId, int paperWeigthFactor)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();

        DataRow row = null;
        try
        {
          row = this.paper.Tables[Paper.PAPER_LIST].Rows.Find(new object[] { paperId, paperWeigthFactor });
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "getRow - error getting row(id: " + paperId + ", weigthFactor: " + paperWeigthFactor.ToString() + ")", ex);
        }
        return row;
      }
      else
      {
        string select = Paper.PAPER_ID + " = '" + paperId + "' AND " + Paper.WEIGHT_FACTOR + " = '" + paperWeigthFactor.ToString() + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".papers WHERE (" + select + ") ORDER BY paperid ASC LIMIT 2000;");
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

        if (this.OnDBPaperChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBPaperChange(this, new DBChangedEventArgs(sqlelements[0] + Command.PARAMETER_SEPARATOR + sqlelements[2], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();
      }

      string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (paper.Tables[PAPER_LIST].Columns.Count != elements.Length)
      {
        Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
        return;
      }
      try
      {
        paper.Tables[PAPER_LIST].Rows.Add(elements);
        if (OnDBPaperChange != null)
          OnDBPaperChange(this, new DBChangedEventArgs(elements[0] + Command.PARAMETER_SEPARATOR + elements[2], DBChangement.Insert));
      }
      catch (System.Data.ConstraintException ex)
      {
        string log = "Add (Paper DB) - Error adding record to the Paper database for ConstraintException.";
        Logger.LogError(this, log, ex);
      }
      catch (Exception ex)
      {
        string log = "Add (Paper DB) - Error adding new record.";
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
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();

        if (this.OnDBPaperChange != null)
        {
          this.OnDBPaperChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
        }

        return;
      }
      else
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();
      }

      string[] elements = key.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (elements.Length != 2)
      {
        string log = "Del (Paper DB) - Cannot delete record, key (" + key + ") is invalid.";
        Logger.LogError(this, log);
        ManageError(log);
        return;
      }
      try
      {
        DataTable dt = null;
        DataRow dr = null;
        dt = paper.Tables[PAPER_LIST];
        if (dt != null)
        {
          dr = dt.Rows.Find(new Object[] { elements[0], elements[1] });
          if (dr != null)
          {
            dr.Delete();
            if (OnDBPaperChange != null)
              OnDBPaperChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
          }
          else
          {
            string log = "Del (Paper DB) - Cannot delete record, record (" + key + ") not found.";
            Logger.LogError(this, log);
            ManageError(log);
            return;
          }
        }
        else
        {
          string log = "Del (Paper DB) - Cannot delete record, table of papers not found.";
          Logger.LogError(this, log);
          ManageError(log);
          return;
        }
      }
      catch (Exception ex)
      {
        string log = "Del (Paper DB) - Error deleting record.";
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
    /// Modify an existing record.
    /// </summary>
    /// <param name="values">a string representing the new values</param>
    public void Mod(string values)
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();

        if (this.OnDBPaperChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBPaperChange(this, new DBChangedEventArgs(sqlelements[0] + Command.PARAMETER_SEPARATOR + sqlelements[2], DBChangement.Modify));
        }

        return;
      }
      else
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();
      }

      string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (paper.Tables[PAPER_LIST].Columns.Count != elements.Length)
      {
        string log = "Mod (Paper DB) - Cannot update record because the number of the parameters is incompatible with the table";
        Logger.LogError(this, log);
        ManageError(log);
        return;
      }
      try
      {
        DataTable dt = null;
        DataRow dr = null;
        dt = paper.Tables[PAPER_LIST];
        if (dt != null)
        {
          dr = dt.Rows.Find(new object[] { elements[0], elements[2] }); //id + weigth factor
          if (dr != null)
          {
            dr.ItemArray = elements;
            if (OnDBPaperChange != null)
              OnDBPaperChange(this, new DBChangedEventArgs(elements[0] + Command.PARAMETER_SEPARATOR + elements[2], DBChangement.Modify));
          }
          else
          {
            string log = "Mod (Paper DB) - Error modifing record, record (" + values + ") not found.";
            Logger.LogError(this, log);
            ManageError(log);
            return;
          }
        }
        else
        {
          string log = "Mod (Paper DB) - Error modifing record, table not found.";
          Logger.LogError(this, log);
          ManageError(log);
          return;
        }
      }
      catch (Exception ex)
      {
        string log = "Mod (Paper DB) - Error modifing record.";
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
    /// Clean the dataset.
    /// </summary>
    public void Clr()
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();
        return;
      }
      else
      {
        if (this.paper == null)
          this.paper = this.generateDataSet();
      }

      if (paper != null)
      {
        paper.Clear();
        //call the refresh data on every instantiated form
        PageManager.getPageManager().refreshAllFormsData();
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
        if (this.paper == null)
          this.paper = this.generateDataSet();
      }

      if (paper != null)
      {
        paper.Clear();
        try
        {
          paper.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
          //call the refresh data on every instantiated form
          PageManager.getPageManager().refreshAllFormsData();
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "Load - Error during reading local paper database", ex);
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
        if (this.paper == null)
          this.paper = this.generateDataSet();

        if (paper != null)
        {
          try
          {
            paper.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the paper database;", ex);
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
          this.getTable().WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
        }
        catch (SecurityException ex)
        {
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the paper database;", ex);
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
