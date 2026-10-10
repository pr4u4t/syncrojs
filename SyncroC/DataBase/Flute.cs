using System;
using System.Data;
using System.Security;
using System.IO;
using System.Windows.Forms;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
  /// <summary>
  /// Summary description for Flute.
  /// </summary>
  public class Flute : SyncroC.DataBase.Interfaces.IDatabase
  {
    private const string XML_FILE_NAME = "Flute.xml";
    private string fullPathXmlFile = null;

    /// <summary>String to be sent as version in case of error</summary>
    public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

    #region table constants
    /// <summary>Dataset table name.</summary>
    public static readonly string FLUTE_LIST = "FluteList";

    /// <summary>Column name for data: "Flute.FluteId".</summary>
    public static readonly string FLUTE_ID = "Flute";
    /// <summary>Column name for data: "Flute.SFSPosition".</summary>
    public static readonly string SFS_POSITION = "SFSPosition";
    /// <summary>Column name for data: "Flute.FluteText".</summary>
    public static readonly string TEXT = "flutetext";
    //dry data
    /// <summary>Column name for data: "Flute.ScoreGap_UDdmm".</summary>
    public static readonly string SCOREGAP_UDdmm = "ScoreGap_UDdmm";
    /// <summary>Column name for data: "Flute.GrindingParameter_UDm".</summary>
    public static readonly string GRINDING_PARAMETER_UDm = "GrindingParameter_UDm";
    /// <summary>Column name for data: "Flute.LubricationParameter_UDm".</summary>
    public static readonly string LUBRICATION_PARAMETER_UDm = "LubricationParameter_UDm";
    /// <summary>Column name for data: "Flute.TrimGrindingsMultiplier".</summary>
    public static readonly string TRIM_GRINDINGS_MULTIPLIER = "TrimGrindingsMultiplier";
    /// <summary>Column name for data: "Flute.GrindingTime_UTs".</summary>
    public static readonly string GRINDING_DISTANCE_UDm = "grindingdistance_udm";
    /// <summary>Column name for data: "Flute.LubricationTime_UTs".</summary>
    public static readonly string LUBRICATION_DURATION_UTms = "LubricationTime_UTds";
    /// <summary>Column name for data: "Flute.ProductionSpeed_UVm_m".</summary>
    public static readonly string PRODUCTION_SPEED_UVm_m = "ProductionSpeed_UVm_m";
    /// <summary>Column name for data: "Flute.AOCSpeed_UVm_m".</summary>
    public static readonly string AOC_SPEED_UVm_m = "AOCSpeed_UVm_m";
    /// <summary>Column name for data: "Flute.Thickness_UDdmm".</summary>
    public static readonly string THICKNESS_UDdmm = "Thickness_UDdmm";
    /// <summary>Column name for data: "Flute.TerminalFluteCode".</summary>
    public static readonly string TERMINAL_FLUTE_CODE = "TerminalFluteCode";
    //wet data
    //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
    /// <summary>Column name for data: "Flute.TWIN400_INTERNAL_FLAP_DISABLED".</summary>
    public static readonly string TWIN400_INTERNAL_FLAP_DISABLED = "TWIN400InternalFlapDisabled";

    /// <summary>Column name for data: "Flute.SPARE2".</summary>
    public static readonly string CRUISING_SPEED_UVm_m = "CruisingSpeed_UVm_m";
    /// <summary>Column name for data: "Flute.SPARE3".</summary>
    public static readonly string SPARE3 = "SPARE3";
    /// <summary>Column name for data: "Flute.SPARE4".</summary>
    public static readonly string SPARE4 = "SPARE4";
    /// <summary>Column name for data: "Flute.SPARE5".</summary>
    public static readonly string SPARE5 = "SPARE5";
    /// <summary>Column name for data: "Flute.SPARE6".</summary>
    public static readonly string SPARE6 = "SPARE6";
    /// <summary>Column name for data: "Flute.SPARE7".</summary>
    public static readonly string SPARE7 = "SPARE7";
    /// <summary>Column name for data: "Flute.SPARE8".</summary>
    public static readonly string SPARE8 = "SPARE8";
    /// <summary>Column name for data: "Flute.SPARE9".</summary>
    public static readonly string SPARE9 = "SPARE9";
    /// <summary>Column name for data: "Flute.SPARE10".</summary>
    public static readonly string SPARE10 = "SPARE10";
    /// <summary>Column name for data: "Flute.SPARE11".</summary>
    public static readonly string SPARE11 = "SPARE11";
    /// <summary>Column name for data: "Flute.SPARE12".</summary>
    public static readonly string SPARE12 = "SPARE12";
    /// <summary>Column name for data: "Flute.SPARE13".</summary>
    public static readonly string SPARE13 = "SPARE13";

    #endregion

    /// <summary>
    /// flute dataset reference
    /// </summary>
    private DataSet flute = null;

    /// <summary>
    /// Delegate for handling OnDBChange events.
    /// </summary>
    public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

    /// <summary>
    /// Event raised when a change (delete, add or modify of a record) occur into Flute database.
    /// </summary>
    public event DBChangedHandler OnDBFluteChange;

    public Flute()
    {
      fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
    }

    private DataSet generateDataSet()
    {
      DataSet flute = new DataSet();
      flute.CaseSensitive = true;

      #region FluteList table

      flute.Tables.Add(FLUTE_LIST);
      //columns
      flute.Tables[FLUTE_LIST].Columns.Add(FLUTE_ID);
      flute.Tables[FLUTE_LIST].Columns.Add(SFS_POSITION);
      flute.Tables[FLUTE_LIST].Columns.Add(TEXT);
      flute.Tables[FLUTE_LIST].Columns.Add(SCOREGAP_UDdmm);
      flute.Tables[FLUTE_LIST].Columns.Add(GRINDING_PARAMETER_UDm);
      flute.Tables[FLUTE_LIST].Columns.Add(LUBRICATION_PARAMETER_UDm);
      flute.Tables[FLUTE_LIST].Columns.Add(TRIM_GRINDINGS_MULTIPLIER);
      flute.Tables[FLUTE_LIST].Columns.Add(GRINDING_DISTANCE_UDm);
      flute.Tables[FLUTE_LIST].Columns.Add(LUBRICATION_DURATION_UTms);
      flute.Tables[FLUTE_LIST].Columns.Add(PRODUCTION_SPEED_UVm_m);
      flute.Tables[FLUTE_LIST].Columns.Add(AOC_SPEED_UVm_m);
      flute.Tables[FLUTE_LIST].Columns.Add(THICKNESS_UDdmm);
      flute.Tables[FLUTE_LIST].Columns.Add(TERMINAL_FLUTE_CODE);
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      //flute.Tables[FLUTE_LIST].Columns.Add(SPARE1);
      flute.Tables[FLUTE_LIST].Columns.Add(TWIN400_INTERNAL_FLAP_DISABLED);
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      flute.Tables[FLUTE_LIST].Columns.Add(CRUISING_SPEED_UVm_m);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE3);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE4);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE5);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE6);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE7);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE8);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE9);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE10);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE11);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE12);
      flute.Tables[FLUTE_LIST].Columns.Add(SPARE13);

      //key
      flute.Tables[FLUTE_LIST].PrimaryKey =
        new DataColumn[] { flute.Tables[FLUTE_LIST].Columns[FLUTE_ID] };

      #endregion

      return flute;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.flute == null)
          this.flute = this.generateDataSet();

        return this.flute.Tables[Flute.FLUTE_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".flute LIMIT 2000;");
      }
    }

    public DataTable getTable()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.flute == null)
          this.flute = this.generateDataSet();

        return this.flute.Tables[Flute.FLUTE_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".flute ORDER BY flute ASC LIMIT 2000;");
        dt.TableName = Flute.FLUTE_LIST;
        return dt;
      }
    }

    public DataRow getRow(string fluteId)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.flute == null)
          this.flute = this.generateDataSet();

        DataRow row = null;
        try
        {
          row = this.flute.Tables[Flute.FLUTE_LIST].Rows.Find(new object[] { fluteId });
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "getRow - error getting row(id: " + fluteId + ")", ex);
        }
        return row;
      }
      else
      {
        string select = Flute.FLUTE_ID + " = '" + fluteId + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".flute WHERE (" + select + ") ORDER BY flute ASC LIMIT 2000;");
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

        if (this.OnDBFluteChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBFluteChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.flute == null)
          this.flute = this.generateDataSet();
      }

      if (flute == null || !flute.Tables.Contains(Flute.FLUTE_LIST))
      {
        Logger.LogError(this, "Add - Cannot add record because the dataset is null or does not contain the table " + Flute.FLUTE_LIST);
        return;
      }
      string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (flute.Tables[Flute.FLUTE_LIST].Columns.Count != elements.Length)
      {
        Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table \"" + Flute.FLUTE_LIST + "\"");
        return;
      }
      try
      {
        flute.Tables[Flute.FLUTE_LIST].Rows.Add(elements);
        if (OnDBFluteChange != null)
          OnDBFluteChange(this, new DBChangedEventArgs(elements[0], DBChangement.Insert));
      }
      catch (System.Data.ConstraintException ex)
      {
        string log = "Add (Flute DB) - Error adding record to the Flute database for ConstraintException.";
        Logger.LogError(this, log, ex);
      }
      catch (Exception ex)
      {
        string log = "Add (Flute DB) - Error adding new recordto Flute database";
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

        if (this.OnDBFluteChange != null)
        {
          this.OnDBFluteChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
        }

        return;
      }
      else
      {
        if (this.flute == null)
          this.flute = this.generateDataSet();
      }

      if (flute == null || !flute.Tables.Contains(Flute.FLUTE_LIST))
      {
        Logger.LogError(this, "Del - Cannot delete record because the dataset is null or does not contain the table \"" + Flute.FLUTE_LIST + "\"");
        return;
      }
      try
      {
        DataTable dt = flute.Tables[Flute.FLUTE_LIST];
        DataRow dr = null;
        if (dt != null) dr = dt.Rows.Find(key);
        if (dr != null) dr.Delete();
        if (OnDBFluteChange != null)
          OnDBFluteChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
      }
      catch (Exception ex)
      {
        string log = "Del (Flute DB) - Error deleting record.";
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

        if (this.OnDBFluteChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBFluteChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Modify));
        }

        return;
      }
      else
      {
        if (this.flute == null)
          this.flute = this.generateDataSet();
      }

      if (flute == null || !flute.Tables.Contains(Flute.FLUTE_LIST))
      {
        Logger.LogError(this, "Mod - Cannot modify record because the dataset is null or does not contain the table \"" + Flute.FLUTE_LIST + "\"");
        return;
      }
      string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (flute.Tables[Flute.FLUTE_LIST].Columns.Count != elements.Length)
      {
        Logger.LogError(this, "Mod - Cannot modify record because the number of the parameters is incompatible with the table");
        return;
      }
      try
      {
        flute.Tables[FLUTE_LIST].Rows.Find(elements[0]).ItemArray = elements;
        if (OnDBFluteChange != null)
          OnDBFluteChange(this, new DBChangedEventArgs(elements[0], DBChangement.Modify));
      }
      catch (Exception ex)
      {
        string log = "Mod (Flute DB) - Error modifing record.";
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
        if (this.flute == null)
          this.flute = this.generateDataSet();
      }

      if (flute != null)
      {
        flute.Clear();
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
        if (this.flute == null)
          this.flute = this.generateDataSet();
      }

      if (flute != null)
      {
        flute.Clear();
        try
        {
          flute.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
          //call the refresh data on every instantiated form
          PageManager.getPageManager().refreshAllFormsData();
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "Load - Error during reading local flute database", ex);
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
        if (this.flute == null)
          this.flute = this.generateDataSet();

        if (flute != null)
        {
          try
          {
            flute.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the flute database;", ex);
          }
          finally
          {
            Application.DoEvents();
          }
        }
        else
          Logger.LogError(this, "Save - Cannot complete, dataset was null");
      }
      else
      {
        try
        {
          this.getTable().WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
        }
        catch (SecurityException ex)
        {
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the flute database;", ex);
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
    /// <returns>a Syncro style DateTime string represent the date when the version was created.
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
