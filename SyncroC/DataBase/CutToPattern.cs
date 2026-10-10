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
	/// Summary description for PastRoll.
	/// </summary>
	/// 

	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.

	public class CutToPattern : SyncroC.DataBase.Interfaces.IDatabase
	{
		private const string XML_FILE_NAME = "CutToPattern.xml";
		private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

		#region table constants

    /// <summary>Dataset table name.</summary>
    public static readonly string CUTTOPATTERN_LIST = "CutToPattern";
    /// <summary>Column name for data: "CutToPattern.PatternID".</summary>
    public static readonly string PATTERN_ID = "PatternID";
    // Pattern description
    public static readonly string	PATTERN_TEXT = "PatternText";
    // Distance from start of pattern to each mark (maximum of 10)
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm1 = "MarkDistance_UDdmm1";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm2 = "MarkDistance_UDdmm2";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm3 = "MarkDistance_UDdmm3";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm4 = "MarkDistance_UDdmm4";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm5 = "MarkDistance_UDdmm5";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm6 = "MarkDistance_UDdmm6";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm7 = "MarkDistance_UDdmm7";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm8 = "MarkDistance_UDdmm8";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm9 = "MarkDistance_UDdmm9";
    public static readonly string PATTERN_MARK_DISTANCE_UDdmm10 = "MarkDistance_UDdmm10";
    public readonly static string PATTERN_MARK_DISTANCE_UDdmm_SPARE1 = "MarkDistance_UDdmm_Spare1";
    public readonly static string PATTERN_MARK_DISTANCE_UDdmm_SPARE2 = "MarkDistance_UDdmm_Spare2";
    public readonly static string PATTERN_MARK_DISTANCE_UDdmm_SPARE3 = "MarkDistance_UDdmm_Spare3";
    public readonly static string PATTERN_MARK_DISTANCE_UDdmm_SPARE4 = "MarkDistance_UDdmm_Spare4";
    public readonly static string PATTERN_MARK_DISTANCE_UDdmm_SPARE5 = "MarkDistance_UDdmm_Spare5";
    // Value indicating the setup type of the photo eye looking for the pattern
    public static readonly string PATTERN_EYE_CONTRAST = "EyeContrast";
    // Value to indicate if the first mark of the pattern is light or dark
    public static readonly string PATTERN_FIRST_MARK_ID = "FirstMarkId";
    // Position where cut is made from the end of the pattern
    public static readonly string PATTERN_CUT_OFFSET_UDdmm = "CutOffset_UDdmm";
    // Distance before the start of the pattern to look for the pattern itself
    public static readonly string PATTERN_WINDOW_BEFORE_PATTERN_UDdmm = "WindowBeforePattern_UDdmm";
    // Distance after the start of the pattern to look for the pattern itself
    public static readonly string PATTERN_WINDOW_AFTER_PATTERN_UDdmm = "WindowAfterPattern_UDdmm";
		//RFAZZI_FI12/08/2008_1.Added lateral position of pattern
		// Lateral Distance from the left trim slit and the Mark
		public static readonly string PATTERN_LATERAL_POSITION_UDdmm = "PatternLateralPosition_UDdmm";
		/// <summary>Column name.</summary>
    public readonly static string SPARE1 = "SPARE1";
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
		/// cutToPattern dataset reference
		/// </summary>
		private DataSet cutToPattern = null;

		/// <summary>
		/// Delegate for handling OnDBChange events.
		/// </summary>
		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

		/// <summary>
		/// Event raised when a change (delete, add or modify of a record) occur into CutToPattern database.
		/// </summary>
		public event DBChangedHandler OnDBCutToPatternChange;

    public CutToPattern()
    {
      fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
    }

    private DataSet generateDataSet()
    {
      DataSet cutToPattern = new DataSet();
      cutToPattern.CaseSensitive = true;

      #region CutToPatternList table

      cutToPattern.Tables.Add(CUTTOPATTERN_LIST);

      //columns
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_ID);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_TEXT);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm1);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm2);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm3);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm4);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm5);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm6);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm7);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm8);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm9);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm10);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm_SPARE1);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm_SPARE2);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm_SPARE3);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm_SPARE4);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_MARK_DISTANCE_UDdmm_SPARE5);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_EYE_CONTRAST);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_FIRST_MARK_ID);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_CUT_OFFSET_UDdmm);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_WINDOW_BEFORE_PATTERN_UDdmm);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_WINDOW_AFTER_PATTERN_UDdmm);
      //RFAZZI_FI12/08/2008_1.Added lateral position of pattern
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(PATTERN_LATERAL_POSITION_UDdmm);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE1);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE2);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE3);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE4);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE5);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE6);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE7);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE8);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE9);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE10);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE11);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE12);
      cutToPattern.Tables[CUTTOPATTERN_LIST].Columns.Add(SPARE13);

      //key
      cutToPattern.Tables[CUTTOPATTERN_LIST].PrimaryKey =
        new DataColumn[] { cutToPattern.Tables[CUTTOPATTERN_LIST].Columns[PATTERN_ID] };

      #endregion

      return cutToPattern;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();

        return this.cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".cuttopattern LIMIT 2000;");
      }
    }

    public DataTable getTable()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();

        return this.cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".cuttopattern ORDER BY patternid ASC LIMIT 2000;");
        dt.TableName = CutToPattern.CUTTOPATTERN_LIST;
        return dt;
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

        if (this.OnDBCutToPatternChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBCutToPatternChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();
      }

			if (cutToPattern == null || !cutToPattern.Tables.Contains(CutToPattern.CUTTOPATTERN_LIST))
			{
				Logger.LogError(this, "Add - Cannot add record because the dataset is null or does not contain the table " + CutToPattern.CUTTOPATTERN_LIST);
				return;
			}
			string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Columns.Count != elements.Length)
			{
				Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table \"" + CutToPattern.CUTTOPATTERN_LIST + "\"");
				return;
			}
			try
			{
				cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Rows.Add(elements);
				if (OnDBCutToPatternChange != null)
					OnDBCutToPatternChange(this, new DBChangedEventArgs(elements[0], DBChangement.Insert));
			}
			catch (System.Data.ConstraintException ex)
			{
				string log = "Add (CutToPattern DB) - Error adding record to the CutToPattern database for ConstraintException.";
				Logger.LogError(this, log, ex);
			}
			catch (Exception ex)
			{
				string log = "Add (CutToPattern DB) - Error adding new recordto CutToPattern database";
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

        if (this.OnDBCutToPatternChange != null)
        {
          this.OnDBCutToPatternChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
        }

        return;
      }
      else
      {
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();
      }

			if (cutToPattern == null || !cutToPattern.Tables.Contains(CutToPattern.CUTTOPATTERN_LIST))
			{
				Logger.LogError(this, "Del - Cannot delete record because the dataset is null or does not contain the table \"" + CutToPattern.CUTTOPATTERN_LIST + "\"");
				return;
			}
			try
			{
				DataTable dt = cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST];
				DataRow dr = null;
				if (dt != null) dr = dt.Rows.Find(key);
				if (dr != null) dr.Delete();
				if (OnDBCutToPatternChange != null)
					OnDBCutToPatternChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
			}
			catch (Exception ex)
			{
				string log = "Del (CutToPattern DB) - Error deleting record.";
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

        if (this.OnDBCutToPatternChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBCutToPatternChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Modify));
        }

        return;
      }
      else
      {
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();
      }

			if (cutToPattern == null || !cutToPattern.Tables.Contains(CutToPattern.CUTTOPATTERN_LIST))
			{
				Logger.LogError(this, "Mod - Cannot modify record because the dataset is null or does not contain the table \"" + CutToPattern.CUTTOPATTERN_LIST + "\"");
				return;
			}
			string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Columns.Count != elements.Length)
			{
				Logger.LogError(this, "Mod - Cannot modify record because the number of the parameters is incompatible with the table");
				return;
			}
			try
			{
        //agrassi_FI15-01-09_1
        //cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Rows.Find(elements[0]).ItemArray = elements;
        DataRow r=cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Rows.Find(elements[0]);
        if (r != null)
        {
          r.ItemArray = elements;
          if (OnDBCutToPatternChange != null)
            OnDBCutToPatternChange(this, new DBChangedEventArgs(elements[0], DBChangement.Modify));
        }
			}
			catch (Exception ex)
			{
				string log = "Mod (CutToPattern DB) - Error modifing record.";
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
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();
      }

			if (cutToPattern != null)
			{
				cutToPattern.Clear();
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
        if (this.cutToPattern == null)
          this.cutToPattern = this.generateDataSet();
      }

			if (cutToPattern != null)
			{
				cutToPattern.Clear();
				try
				{
					cutToPattern.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
					//call the refresh data on every instantiated form
					PageManager.getPageManager().refreshAllFormsData();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - Error during reading local cutToPattern database", ex);
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
        if (cutToPattern != null)
        {
          try
          {
            cutToPattern.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the cutToPattern database;", ex);
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
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the cutToPattern database;", ex);
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


