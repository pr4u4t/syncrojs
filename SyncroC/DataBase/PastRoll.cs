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
	public class PastRoll : SyncroC.DataBase.Interfaces.IDatabase
	{
    //agrassi_FI06-03-08
    //private const string XML_FILE_NAME = "PastRolls.xml";
    public const string XML_FILE_NAME = "PastRolls.xml";
    private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;
		private const int MAX_PAST_ROLLS = 500;

		#region table constants

		/// <summary>Dataset table name.</summary>
		public static readonly string PASTROLL_LIST = "PastRollList";

		/// <summary>Column name.</summary>
		public readonly static string PAPER_TYPE = "paperType";													// Code for the paper
		public readonly static string CORE_DIAMETER_UDdmm = "coreDiameter_UDdmm";				// Diameter to splice at	From paper database or perhaps roll stock
		public readonly static string WIDTH_UDdmm = "width_UDdmm";											// Width of roll
		public readonly static string WEIGHT_FACTOR_UWg_m = "weightFactor_UWg_m";				// Weight factor of paper
		public readonly static string SYNCRO_SPLICE = "syncroSplice";										// If Syncro gave the splice command		- Previous roll only
		public readonly static string TIME_REMOVED = "timeRemoved";											// Either by splice or signal from RDisp
		public readonly static string REMAINING_DIAMETER_UDdmm = "remainingDiameter_UDdmm";	// Diameter of roll
		public readonly static string RUN_UDmm = "run_UDmm";														// Length run from roll
		public readonly static string REMAINING_UDmm = "remaining_UDmm";								// Length left on roll
    public readonly static string THICKNESS_UDdmmm = "thickness_UDdmmm";						// Paper caliper
    public readonly static string SPLICER_NUMBER = "splicerNumber";     						// Splicer number
    //agrassi_FI17-03-08
    public readonly static string SHIFT_ID = "shiftId";  						                // ID of the shift
    public readonly static string SHIFT_START_TIME = "shiftStartTime";  						// Start time of the shift
    public readonly static string TIME_ENTERED = "timeEntered";											//
    public readonly static string PROGRAM_CODE = "programCode";											//
    public readonly static string ROLL_DATA_ENTERED = "rollDataEntered";						//
    public readonly static string JOB = "job";                          						//
    public readonly static string COMM = "comm";                         						//
    public readonly static string ROLL_ID = "RollId";                               // Identificativo della Bobina
    public readonly static string DATA_TO_PRINT = "DataToPrint";                    // Dati da stampare della Bobina
    public readonly static string DESCRIPTION = "Description";                      // Descrizione della Bobina
    //agrassi_FI15-07-2010_1
    public readonly static string SPLICE_SPEED = "spliceSpeed_UDmm_min";            //speed during the splice (in mm/min)                               
    public readonly static string SPLICE_DONE = "spliceDone";                       //indicate if the roll report has been closed because of a splice and not for a shift change or so on                             



		#endregion

		/// <summary>
		/// flute dataset reference
		/// </summary>
		private DataSet pastRoll = null;

		/// <summary>
		/// Delegate for handling OnDBChange events.
		/// </summary>
		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

		/// <summary>
		/// Event raised when a change (delete, add or modify of a record) occur into Flute database.
		/// </summary>
		public event DBChangedHandler OnDBPastRollChange;

		public PastRoll()
		{
			fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;			
		}

    private DataSet generateDataSet()
    {
      DataSet pastRoll = new DataSet();
      pastRoll.CaseSensitive = true;

      #region PastRollList table

      pastRoll.Tables.Add(PASTROLL_LIST);
      //columns

      pastRoll.Tables[PASTROLL_LIST].Columns.Add(PAPER_TYPE); //0
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(CORE_DIAMETER_UDdmm); //1
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(WIDTH_UDdmm); //2
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(WEIGHT_FACTOR_UWg_m); //3
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(SYNCRO_SPLICE); //4
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(new DataColumn(TIME_REMOVED, typeof(DateTime))); //5
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(REMAINING_DIAMETER_UDdmm); //6
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(RUN_UDmm); //7
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(REMAINING_UDmm); //8
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(THICKNESS_UDdmmm); //9
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(SPLICER_NUMBER); //10
      //agrassi_FI17-03-08
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(new DataColumn(SHIFT_START_TIME, typeof(DateTime))); //11
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(SHIFT_ID); //12
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(new DataColumn(TIME_ENTERED, typeof(DateTime))); //13
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(PROGRAM_CODE); //14
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(ROLL_DATA_ENTERED); //15
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(ROLL_ID); //16
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(DESCRIPTION); //17
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(DATA_TO_PRINT); //18
      //agrassi_FI15-07-2010_1
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(SPLICE_SPEED); //19
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(SPLICE_DONE); //19

      //ABELLO_FI04/06/2008_1
      //agrassi_FI15-07-2010_1
      //pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE10"); //19 SPARE
      //pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE11"); //20 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE12"); //21 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE13"); //22 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE14"); //23 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE15"); //24 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE16"); //25 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE17"); //26 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE18"); //27 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE19"); //28 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE20"); //29 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE21"); //30 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE22"); //31 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE23"); //32 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE24"); //33 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE25"); //34 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE26"); //35 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE27"); //36 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE28"); //37 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE29"); //38 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE30"); //39 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE31"); //40 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE32"); //41 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE33"); //42 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE34"); //43 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE35"); //44 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE36"); //45 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE37"); //46 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE38"); //47 SPARE
      pastRoll.Tables[PASTROLL_LIST].Columns.Add("SPARE39"); //48 SPARE

      pastRoll.Tables[PASTROLL_LIST].Columns.Add(JOB); //49
      pastRoll.Tables[PASTROLL_LIST].Columns.Add(COMM); //50

      //key


      #endregion

      return pastRoll;
    }

    public long getCount()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();

        return this.pastRoll.Tables[PastRoll.PASTROLL_LIST].Rows.Count;
      }
      else
      {
        return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastrolls LIMIT 500;");
      }
    }

    public DataTable getTable(string filter)
    {
      if (filter.Trim() == "")
        return this.getTable();

      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();

        return this.pastRoll.Tables[PastRoll.PASTROLL_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastrolls WHERE (" + filter + ") ORDER BY timeremoved DESC LIMIT 500;");
        dt.TableName = PastRoll.PASTROLL_LIST;
        return dt;
      }
    }

    public DataTable getTable()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();

        return this.pastRoll.Tables[PastRoll.PASTROLL_LIST];
      }
      else
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastrolls p ORDER BY timeremoved DESC LIMIT 500;");
        dt.TableName = PastRoll.PASTROLL_LIST;
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

        if (this.OnDBPastRollChange != null)
        {
          string[] sqlelements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
          this.OnDBPastRollChange(this, new DBChangedEventArgs(sqlelements[0], DBChangement.Insert));
        }

        return;
      }
      else
      {
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();
      }

			string[] elements = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
      if (pastRoll.Tables[PASTROLL_LIST].Columns.Count != elements.Length)
      {
			  Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
			  return;
			}
      object[] elementsObjects = new object[elements.Length];
      Array.Copy(elements, 0, elementsObjects, 0, elements.Length);
			try
			{
        elementsObjects[5] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(elements[5]);
        //agrassi_FI17-03-08
        elementsObjects[11] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(elements[11]);
        elementsObjects[13] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(elements[13]);
      }
			catch (Exception ex)
			{
				string log = "Add (pastRoll DB) - Invalid parameter encountered.";
				Logger.LogError(this, log, ex);
				ManageError(log);
			}
      //agrassi_FI17-03-08
      elementsObjects[49] = DBManager.getDBManager().syncroConfig.GeneralSetting.Job;
      elementsObjects[50] = DBManager.getDBManager().syncroConfig.GeneralSetting.Comm;

			if (pastRoll.Tables[PASTROLL_LIST].Rows.Count >= PastRoll.MAX_PAST_ROLLS)
			{
				DataRow[] deletingRows = null;
				string expression = TIME_REMOVED + " > '" + DateTime.MinValue.ToShortDateString() + "'";
				string sortOrder = TIME_REMOVED + " ASC";

				deletingRows = pastRoll.Tables[PASTROLL_LIST].Select(expression, sortOrder);
				if (deletingRows != null && deletingRows.Length > 0)
				{
					pastRoll.Tables[PASTROLL_LIST].Rows.Remove(deletingRows[0]);
				}
			}

			try
			{
				pastRoll.Tables[PASTROLL_LIST].Rows.Add(elementsObjects);
			}
			catch (Exception ex)
			{
				string log = "Add (pastRoll DB) - Error adding new record.";
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
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();
      }

			if (pastRoll != null)
			{
				pastRoll.Clear();
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
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();
      }

			if (pastRoll != null)
			{
				pastRoll.Clear();
				try
				{
					pastRoll.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
					//call the refresh data on every instantiated form
					PageManager.getPageManager().refreshAllFormsData();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - Error during reading local pastRoll database", ex);
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
        if (this.pastRoll == null)
          this.pastRoll = this.generateDataSet();

        if (pastRoll != null)
        {
          try
          {
            pastRoll.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the pastRoll database;", ex);
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
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the pastRoll database;", ex);
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

