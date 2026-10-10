using System;
using System.IO;
//using System.Security;
using System.ComponentModel;


using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Summary description for Scorer.
	/// </summary>
	
	public class ScorerProfile// : SyncroC.DataBase.Interfaces.IDatabase
	{
		//private const string XML_FILE_NAME = "Scorer.xml"; 
		private const string FILE_NAME = "ScorerProfile.ini";
		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;
		public const int PROFILE_COUNT=80;
		private string fullPathFile = null;
		private ScorerProfileType[] profiles;
		private int count;

		//constants
//		/// <summary>Dataset table name.</summary>
//		public readonly static string SCORER_LIST = "ScorerList";
//
//		/// <summary>Column name.</summary>
//		public readonly static string SCORER_ID = "ScorerID";
//		/// <summary>Column name.</summary>
//		public readonly static string SCLASS = "sClass";
//		/// <summary>Column name.</summary>
//		public readonly static string WIDTH = "Width_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string PROFILE_WIDTH = "ProfileWidth_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string TIP_HEIGHT = "TipHeight_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string VALLEY_DEPTH = "ValleyDepth_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string TIP_OFFSET = "TipOffset_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string FAR_VALLEY_OFFSET = "FarValleyOffset_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string UPPER_DIAMETER = "upperDiameter_UDdmm";
//		/// <summary>Column name.</summary>
//		public readonly static string LOWER_DIAMETER = "lowerDiameter_UDdmm";

		
		/// <summary>
		/// papers dataset reference
		/// </summary>
//		public DataSet scorer = null;
//
//		/// <summary>
//		/// Delegate for handling OnDBChange events.
//		/// </summary>
//		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);
//
//		/// <summary>
//		/// Event raised when a change (delete, add or modify of a record) occur into Paper database.
//		/// </summary>
//		public event DBChangedHandler OnDBScorerChange;

		public ScorerProfile()
		{
			Settings s = Settings.getSettings();
      fullPathFile = s.paths.ClientData + FILE_NAME;

			profiles=new ScorerProfileType[PROFILE_COUNT];
			for (int i=0; i< PROFILE_COUNT; i++)
				this.profiles[i]= new ScorerProfileType(FILE_NAME, i);

			count=0;
//			scorer = new DataSet();
//			scorer.CaseSensitive=true;

//			#region ScorerList table
//			scorer.Tables.Add(SCORER_LIST);
//			//columns
//			scorer.Tables[SCORER_LIST].Columns.Add(SCORER_ID);
//			scorer.Tables[SCORER_LIST].Columns.Add(SCLASS);
//			scorer.Tables[SCORER_LIST].Columns.Add(WIDTH);
//			scorer.Tables[SCORER_LIST].Columns.Add(PROFILE_WIDTH);
//			scorer.Tables[SCORER_LIST].Columns.Add(TIP_HEIGHT);
//			scorer.Tables[SCORER_LIST].Columns.Add(VALLEY_DEPTH);
//			scorer.Tables[SCORER_LIST].Columns.Add(TIP_OFFSET);
//			scorer.Tables[SCORER_LIST].Columns.Add(FAR_VALLEY_OFFSET);
//			scorer.Tables[SCORER_LIST].Columns.Add(UPPER_DIAMETER);
//			scorer.Tables[SCORER_LIST].Columns.Add(LOWER_DIAMETER);
//
//			//key
//			scorer.Tables[SCORER_LIST].PrimaryKey = 
//				new DataColumn[]{scorer.Tables[SCORER_LIST].Columns[SCORER_ID]};
//
//			#endregion
		}

    [Category("Scorer Profiles"), Description("Scorer Profiles"), TypeConverter(typeof(FosberArrayConverter))]
		public ScorerProfileType[] Profiles
		{
			get{return this.profiles;}
		}

		public ScorerProfile CreateCopy()
		{
			ScorerProfile copy=(ScorerProfile)this.MemberwiseClone();
			copy.profiles=new ScorerProfileType[PROFILE_COUNT];
			for (int i=0; i< PROFILE_COUNT; i++)
				copy.profiles[i]= (ScorerProfileType)this.profiles[i].CreateCopy();
			return copy;
		}

		#region IDataBase Members

		public void Add(string values)
		{
//			string[] elements = values.Split(new Char[] {Command.PARAMETER_SEPARATOR}); 
//			if (scorer.Tables[SCORER_LIST].Columns.Count != elements.Length)
//			{
//				Logger.LogError(this, "Add - Cannot add record because the number of the parameters is incompatible with the table");
//				return;
//			}
//			try 
//			{
//				scorer.Tables[SCORER_LIST].Rows.Add(elements);
//				if (OnDBScorerChange != null)
//					OnDBScorerChange(this, new DBChangedEventArgs(elements[0], DBChangement.Insert));
//			}
//			catch (Exception ex)
//			{
//				Logger.LogError(this, "Add - ", ex);
//			}
				this.profiles[count++].StartSurveyProperties(ref values,ScorerProfileType.ConfigBaseOperation.PropertiesFromStringAdd); 
				PageManager.getPageManager().refreshAllFormsData();
		}
//
//		public void Del(string key)
//		{
//			try 
//			{
//				DataTable dt = scorer.Tables[SCORER_LIST];
//				DataRow dr = null;
//				if (dt != null) dr = dt.Rows.Find(key);
//				if (dr != null) dr.Delete();
//				if (OnDBScorerChange != null)
//					OnDBScorerChange(this, new DBChangedEventArgs(key, DBChangement.Delete));
//			}
//			catch (Exception ex)
//			{
//				Logger.LogError(this, "Del", ex);
//			}
//		}
//
//		public void Mod(string values)
//		{
//			string[] elements = values.Split(new Char[] {Command.PARAMETER_SEPARATOR}); 
//			if (scorer.Tables[SCORER_LIST].Columns.Count != elements.Length)
//			{
//				Logger.LogError(this, "Mod - Cannot add record because the number of the parameters is incompatible with the table");
//				return;
//			}
//			try 
//			{
//				scorer.Tables[SCORER_LIST].Rows.Find(elements[0]).ItemArray = elements;
//				if (OnDBScorerChange != null)
//					OnDBScorerChange(this, new DBChangedEventArgs(elements[0], DBChangement.Modify));
//			}
//			catch (Exception ex)
//			{
//				Logger.LogError(this, "Mod - ", ex);
//			}
//		}
//
		public void Clr()
		{
//			if (scorer != null)
//				scorer.Clear();
//			//call the refresh data on every instantiated form
//			PageManager.getPageManager().refreshAllFormsData();
			count=0;
			this.profiles.Initialize();
		}
//
//		public void Chk(string checksum)
//		{
//			Logger.LogError(this, "The method \"Chk\" has been called but it is not implemented yet.");
//		}

		public void Load()
		{
//			scorer.Clear();
//			try 
//			{
//				scorer.ReadXml(fullPathXmlFile);
//				//call the refresh data on every instantiated form
//				PageManager.getPageManager().refreshAllFormsData();
//			}
//			catch (Exception ex)
//			{
//				Logger.LogError(this, "Load - Error during reading local paper database", ex);
//			}
			string s=string.Empty;
			for (int i=0; i< PROFILE_COUNT; i++)
        this.profiles[i].StartSurveyProperties(ref s, ScorerProfileType.ConfigBaseOperation.PropertiesFromINI);

		}

		public void Save()
		{
//			try
//			{
//				scorer.WriteXml(fullPathXmlFile);
//			}
//			catch (SecurityException ex)
//			{
//				Logger.LogError(this, "Save - an error occured during XML serialization of the paper database;", ex);
//			}
			string s=string.Empty;
			for (int i=0; i< PROFILE_COUNT; i++)
        this.profiles[i].StartSurveyProperties(ref s, ScorerProfileType.ConfigBaseOperation.PropertiesIntoINI);

		}

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
					File.SetLastWriteTimeUtc(fullPathFile, data);
				}
				catch(FileNotFoundException ex)
				{
					Logger.LogError(this, "SetVer - can't find the file \"" + fullPathFile + "\".", ex);
					return;
				}
				catch(UnauthorizedAccessException ex)
				{
					Logger.LogError(this, "SetVer - can't access the file \"" + fullPathFile + "\".", ex);
					return;
				}
				catch(Exception ex)
				{
					Logger.LogError(this, "SetVer - error trying to change the date of the file \"" + fullPathFile + "\".", ex);
					return;
				}
			}
		}

		public string GetVer()
		{
			DateTime data;
			try
			{
				data = File.GetLastWriteTimeUtc(fullPathFile);
			}
			catch (IOException ex)
			{
				Logger.LogError(this, "GetVer - can't find the file \"" + fullPathFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch(UnauthorizedAccessException ex)
			{
				Logger.LogError(this, "GetVer - can't access the file \"" + fullPathFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch(Exception ex)
			{
				Logger.LogError(this, "GetVer - error trying to get the date of the file \"" + fullPathFile + "\".", ex);
				return WRONG_VERSION;
			}
			return SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeStringNoZerosInFront(data);
		}

		#endregion
	}
}
