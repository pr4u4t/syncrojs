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
	/// Summary description for Quality.
	/// </summary>
	public class Quality : SyncroC.DataBase.Interfaces.IDatabase
	{
		private const string XML_FILE_NAME = "Quality.xml";
		private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

    #region QualityList table constants
    
    //constants
		/// <summary>Dataset table name.</summary>
		public readonly static string QUALITY_LIST = "QualityList";

		/// <summary>Column name.</summary>
		public readonly static string QUALITY_ID = "QualityId";
		/// <summary>Column name.</summary>
		public readonly static string QUALITY_TEXT = "QualityText";
		/// <summary>Column name.</summary>
		public readonly static string FLUTE_ID = Flute.FLUTE_ID;
		//DRY DATA
		/// <summary>Column name.</summary>
		public static readonly string SCOREGAP_UDdmm = Flute.SCOREGAP_UDdmm;
		/// <summary>Column name.</summary>
		public static readonly string GRINDING_PARAMETER_UDm = Flute.GRINDING_PARAMETER_UDm;
		/// <summary>Column name.</summary>
		public static readonly string LUBRICATION_PARAMETER_UDm = Flute.LUBRICATION_PARAMETER_UDm;
		/// <summary>Column name.</summary>
		public static readonly string TRIM_GRINDINGS_MULTIPLIER = Flute.TRIM_GRINDINGS_MULTIPLIER;
		/// <summary>Column name.</summary>
    public static readonly string GRINDING_DISTANCE_UDm = Flute.GRINDING_DISTANCE_UDm;
		/// <summary>Column name.</summary>
		public static readonly string LUBRICATION_DURATION_UTms = Flute.LUBRICATION_DURATION_UTms;
		/// <summary>Column name.</summary>
		public static readonly string PRODUCTION_SPEED_UVm_m = Flute.PRODUCTION_SPEED_UVm_m;
		/// <summary>Column name.</summary>
		public static readonly string AOC_SPEED_UVm_m = Flute.AOC_SPEED_UVm_m;
		/// <summary>Column name.</summary>
		public static readonly string THICKNESS_UDdmm = Flute.THICKNESS_UDdmm;
		/// <summary>Column name.</summary>
		public readonly static string SPARE_1 = "Spare_1";
		/// <summary>Column name.</summary>
		public readonly static string BUNDLE_LAYERS_PER_STACK = "BundleLayersPerStack";
		///// <summary>Column name.</summary>
		//public readonly static string BUNDLE_SHEETS_PER_BUNDLE = "BundleSheetsPerBundle";
		/// <summary>Column name.</summary>
		public readonly static string BUNDLE_REVERSE_MODE = "BundleReverseMode";
		//WET DATA
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_0 = "Paper1Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_0 = "Paper1WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_1 = "Paper2Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_1 = "Paper2WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_2 = "Paper3Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_2 = "Paper3WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_3 = "Paper4Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_3 = "Paper4WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_4 = "Paper5Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_4 = "Paper5WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_5 = "Paper6Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_5 = "Paper6WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PAPER_ID_6 = "Paper7Id";
		/// <summary>Column name.</summary>
    public readonly static string WEIGHT_FACTOR_6 = "Paper7WeightFactor_UWg_m";
		/// <summary>Column name.</summary>
		public readonly static string WEIGHT_FACTOR = "WeightFactorUWg_m";
		/// <summary>Column name.</summary>
		public readonly static string PREPRINT = "PrePrint";
		//PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
		public readonly static string INCLINATELASTBELT = "InclinateLastBelt";
		//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
		public static readonly string TWIN400_INTERNAL_FLAP_DISABLED = "TWIN400InternalFlapDisabled";
		/// <summary>Column name.</summary>
    public readonly static string CRUISING_SPEED_UVm_m = "CruisingSpeed_UVm_m";
		/// <summary>Column name.</summary>
		public readonly static string SPARE2 = "SPARE2";
		/// <summary>Column name.</summary>
		public readonly static string SPARE3 = "SPARE3";
		/// <summary>Column name.</summary>
		public readonly static string SPARE4 = "SPARE4";
		/// <summary>Column name.</summary>
		public readonly static string SPARE5 = "SPARE5";
		/// <summary>Column name.</summary>
		public readonly static string SPARE6 = "SPARE6";
		/// <summary>Column name.</summary>
		public readonly static string SPARE7 = "SPARE7";
		/// <summary>Column name.</summary>
		public readonly static string SPARE8 = "SPARE8";
		/// <summary>Column name.</summary>
		public readonly static string SPARE9 = "SPARE9";
		/// <summary>Column name.</summary>
		public readonly static string SPARE10 = "SPARE10";
		/// <summary>Column name.</summary>
		public readonly static string SPARE11 = "SPARE11";
		/// <summary>Column name.</summary>
		public readonly static string SPARE12 = "SPARE12";
		/// <summary>Column name.</summary>
		public readonly static string SPARE13 = "SPARE13";

    #endregion

    /// <summary>
		/// quality dataset reference
		/// </summary>
		private DataSet quality = null;

		/// <summary>
		/// Delegate for handling OnDBChange events.
		/// </summary>
		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

		/// <summary>
		/// Event raised when a change (delete, add or modify of a record) occur into Quality database.
		/// </summary>
		public event DBChangedHandler OnDBQualityChange;

		public Quality()
		{
      this.fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
		}

    private DataSet generateDataSet()
    {
      DataSet quality = new DataSet();
      quality.CaseSensitive = true;

      quality.Tables.Add(Quality.QUALITY_LIST);

      #region QualityList table

      //columns
      quality.Tables[QUALITY_LIST].Columns.Add(QUALITY_ID); //0
      quality.Tables[QUALITY_LIST].Columns.Add(QUALITY_TEXT); //1

      quality.Tables[QUALITY_LIST].Columns.Add(FLUTE_ID); //2
      quality.Tables[QUALITY_LIST].Columns.Add(SCOREGAP_UDdmm); //3
      quality.Tables[QUALITY_LIST].Columns.Add(GRINDING_PARAMETER_UDm); //4
      quality.Tables[QUALITY_LIST].Columns.Add(LUBRICATION_PARAMETER_UDm); //5
      quality.Tables[QUALITY_LIST].Columns.Add(TRIM_GRINDINGS_MULTIPLIER); //6
      quality.Tables[QUALITY_LIST].Columns.Add(GRINDING_DISTANCE_UDm); //7
      quality.Tables[QUALITY_LIST].Columns.Add(LUBRICATION_DURATION_UTms); //8
      quality.Tables[QUALITY_LIST].Columns.Add(PRODUCTION_SPEED_UVm_m); //9
      quality.Tables[QUALITY_LIST].Columns.Add(AOC_SPEED_UVm_m); //10
      quality.Tables[QUALITY_LIST].Columns.Add(THICKNESS_UDdmm); //11
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE_1); //12
      quality.Tables[QUALITY_LIST].Columns.Add(BUNDLE_LAYERS_PER_STACK); //13
      //quality.Tables[QUALITY_LIST].Columns.Add(BUNDLE_SHEETS_PER_BUNDLE); //14
      quality.Tables[QUALITY_LIST].Columns.Add("Spare"); //14
      quality.Tables[QUALITY_LIST].Columns.Add(BUNDLE_REVERSE_MODE); //15

      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_0); //16
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_0); //17
      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_1); //18
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_1);
      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_2);
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_2);
      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_3);
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_3);
      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_4);
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_4);
      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_5);
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_5);
      quality.Tables[QUALITY_LIST].Columns.Add(PAPER_ID_6);
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR_6);
      quality.Tables[QUALITY_LIST].Columns.Add(WEIGHT_FACTOR);
      quality.Tables[QUALITY_LIST].Columns.Add(PREPRINT);
      //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
      //quality.Tables[QUALITY_LIST].Columns.Add(SPARE1);
      quality.Tables[QUALITY_LIST].Columns.Add(INCLINATELASTBELT);
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      quality.Tables[QUALITY_LIST].Columns.Add(TWIN400_INTERNAL_FLAP_DISABLED);
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      quality.Tables[QUALITY_LIST].Columns.Add(CRUISING_SPEED_UVm_m);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE4);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE5);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE6);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE7);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE8);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE9);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE10);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE11);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE12);
      quality.Tables[QUALITY_LIST].Columns.Add(SPARE13);

      //key
      quality.Tables[QUALITY_LIST].PrimaryKey =
        new DataColumn[] { quality.Tables[QUALITY_LIST].Columns[QUALITY_ID] };

      #endregion

      return quality;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();

        return this.quality.Tables[Quality.QUALITY_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".quality LIMIT 2000;");
      }
    }

    public DataTable getTable(string filter)
    {
      if (filter.Trim() == "")
        return this.getTable();

      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();

        return this.quality.Tables[Quality.QUALITY_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".quality WHERE (" + filter + ") ORDER BY qualityid ASC LIMIT 2000;");
        dt.TableName = Quality.QUALITY_LIST;
        return dt;
      }
    }

    public DataTable getTable()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();

        return this.quality.Tables[Quality.QUALITY_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".quality ORDER BY qualityid ASC LIMIT 2000;");
        dt.TableName = Quality.QUALITY_LIST;
        return dt;
      }
    }

		public DataRow getRow(string qualityID)
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();

        DataRow row = null;
        try
        {
          row = quality.Tables[Quality.QUALITY_LIST].Rows.Find(qualityID);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "getRow - error getting quality row (id:" + qualityID + ")", ex);
        }
        return row;
      }
      else
      {
        string select = Quality.QUALITY_ID + " = '" + qualityID + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".quality WHERE (" + select + ") ORDER BY qualityid ASC LIMIT 2000;");
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

        if (this.OnDBQualityChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBQualityChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();
      }

			string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (quality.Tables[QUALITY_LIST].Columns.Count != elements.Length)
			{
				Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
				return;
			}
			try
			{
				quality.Tables[QUALITY_LIST].Rows.Add(elements);
				if (OnDBQualityChange != null)
          OnDBQualityChange(this, new DBChangedEventArgs(elements[0], DBChangement.Insert));
			}
			catch (System.Data.ConstraintException ex)
			{
				string log = "Add (Quality DB) - Error adding record to the Quality database for ConstraintException.";
				Logger.LogError(this, log, ex);
			}
			catch (Exception ex)
			{
				string log = "Add (Quality DB) - Error adding new record.";
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

        if (this.OnDBQualityChange != null)
          this.OnDBQualityChange(this, new DBChangedEventArgs(key, DBChangement.Delete));

        return;
      }
      else
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();
      }

			try
			{
				DataTable dt = quality.Tables[QUALITY_LIST];
				DataRow dr = null;
				if (dt != null) dr = dt.Rows.Find(key);
				if (dr != null) dr.Delete();
				if (OnDBQualityChange != null)
					OnDBQualityChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
			}
			catch (Exception ex)
			{
				string log = "Del (Quality DB) - Error deleting record.";
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

        if (this.OnDBQualityChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBQualityChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Modify));
        }

        return;
      }
      else
      {
        if (this.quality == null)
          this.quality = this.generateDataSet();
      }

			string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (quality.Tables[QUALITY_LIST].Columns.Count != elements.Length)
			{
				Logger.LogError(this, "Mod - Cannot modify record because the number of the parameters is incompatible with the table");
				return;
			}
			try
			{
				quality.Tables[QUALITY_LIST].Rows.Find(elements[0]).ItemArray = elements;
				if (OnDBQualityChange != null)
					OnDBQualityChange(this, new DBChangedEventArgs(elements[0], DBChangement.Modify));
			}
			catch (Exception ex)
			{
				string log = "Mod (Quality DB) - Error modifing record.";
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
        if (this.quality == null)
          this.quality = this.generateDataSet();
      }

			if (quality != null)
			{
				quality.Clear();
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
        if (this.quality == null)
          this.quality = this.generateDataSet();
      }

			if (quality != null)
			{
				quality.Clear();
				try
				{
					quality.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
					//call the refresh data on every instantiated form
					PageManager.getPageManager().refreshAllFormsData();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - Error during quality local paper database", ex);
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
        if (this.quality == null)
          this.quality = this.generateDataSet();

        if (quality != null)
        {
          try
          {
            quality.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the quality database;", ex);
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
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the quality database;", ex);
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
