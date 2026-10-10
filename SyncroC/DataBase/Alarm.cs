using System;
using System.Data;
using System.Runtime.InteropServices;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Contains all the alarm translations
	/// </summary>
	public class Alarm : SyncroC.DataBase.Interfaces.IDatabase
	{

#if DEBUG
		[DllImport(@"Utility.dll", CallingConvention = CallingConvention.Cdecl)]
#else
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#endif

		public static extern int FileCrc32Assembly([MarshalAs(UnmanagedType.LPTStr)] string szFilename, ref int dwCrc32);

		private readonly string FILENAME = "Alarm";
		private string DataBaseXMLFileName = "";
		private string DataBaseXSDFileName = "";

		/// <summary>Zone column index</summary>
		private int ZONE = 1;
		/// <summary>English column index</summary>
		private int ENGLISH = 2;

		public const System.ComponentModel.ListSortDirection listSortDirection = System.ComponentModel.ListSortDirection.Ascending;

		/// <summary>
		/// alarms dataset reference
		/// </summary>
		public DataSet alarm = null;

		public static AlarmZone Zone = new AlarmZone();

		public Alarm(string DataBaseFolder)
		{
			//set the file name
			DataBaseXMLFileName = DataBaseFolder + FILENAME + ".xml";
			DataBaseXSDFileName = DataBaseFolder + FILENAME + ".xsd";
			//datasets instantiation
			alarm = new DataSet();
			this.Load();
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values</param>
    public void Add(string values)
		{
			Logger.LogError(this, "The method \"Add\" has been called but it is not implemented yet.");
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
			if (alarm != null)
				alarm.Clear();
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
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_ALARM + "()");
					return;
				}
				if (values[0].Trim() != (checkSchema.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXSDFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_ALARM + "()");
					return;
				}
				//xml checking
				ret = FileCrc32Assembly(DataBaseXMLFileName, ref checkXml);
				if (ret != 0)
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXMLFileName + " error: " + ret);
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_ALARM + "()");
					return;
				}
				if (values[1].Trim() != (checkXml.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXMLFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_ALARM + "()");
					return;
				}
			}
			catch (DllNotFoundException ex)
			{
				Logger.LogError(this, "Collect - Dll for checksum calculation not found.", ex);
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_ALARM + "()");
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "Collect - Error during checksum calculation", ex);
			}
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Load the databse.
		/// </summary>
		public void Load()
		{
			alarm.Clear();
			try
			{//try read local schema
				alarm.ReadXmlSchema(DataBaseXSDFileName);
				try
				{//try read local data
					alarm.ReadXml(DataBaseXMLFileName, XmlReadMode.IgnoreSchema);
					//call the refresh data on every instantiated form
					PageManager.getPageManager().refreshAllFormsData();
				}
				catch (Exception ex)
				{
					Logger.LogError(this,
						"Load - error during reading data of alarm database", ex);
          foreach (System.Collections.DictionaryEntry de in ex.Data)
            Logger.LogError(this, de.Key.ToString()+" "+de.Value.ToString());
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(this,
					"Load - error during reading schema of Alarm database", ex);
			}
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Save permanently the database.
		/// </summary>
		public void Save()
		{
			if (alarm != null)
			{
				try
				{
					alarm.WriteXml(DataBaseXMLFileName);
					alarm.WriteXmlSchema(DataBaseXSDFileName);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Save - Error saving alarm data or schema", ex);
				}
			}
			else
				Logger.LogError(this, "Save - Dataset is null, impossible to complete the operation");
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
		/// Gets the translation into the current language of the specified key alarm.
		/// </summary>
		/// <param name="almCode">the alarm code to be translated </param>
		/// <returns></returns>
		public string getTranslatedAlarm(string almCode)
		{
			return getTranslatedAlarm(almCode, Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// Gets the translation into the specified language of the specified key alarm.
		/// </summary>
		/// <param name="almCode">the alarm code to be translated </param>
		/// <param name="language">the language to translate into the given alarm code</param>
		/// <returns></returns>
		public string getTranslatedAlarm(string almCode, string language)
		{
			return getTranslatedAlarmOrZone(almCode, language, true);
		}

		/// <summary>
		/// Gets the code of the zone of the specified alarm
		/// </summary>
		/// <param name="almCode">the alrm code to get the zone code of.</param>
		/// <returns></returns>
		public string getZoneCodeFromAlarm(string almCode)
		{
			if (alarm == null) 
			{
				Logger.LogError(this, "getZoneCodeFromAlarm - alarm dataset was null, empty string will be returned.");
				//return string.Empty;
				return " - ";
			}
			if (alarm.Tables.Count < 2)
			{
				Logger.LogError(this, "getZoneCodeFromAlarm - alarm dataset haven't the 2 needed tables (only has " + alarm.Tables.Count.ToString() + " tables), empty string will be returned.");
				//return string.Empty;
				return " - ";
			}
			if (alarm.Tables[0].Columns.Count <= ZONE)
			{
				Logger.LogError(this, "getZoneCodeFromAlarm - alarm table haven't the needed columns (only has " + alarm.Tables[0].Columns.Count.ToString() + " columns), empty string will be returned.");
				//return string.Empty;
				return " - ";
			}
			return getTranslatedAlarmOrZone(almCode, alarm.Tables[0].Columns[ZONE].ColumnName, true);
		}

		/// <summary>
		/// Gets the translation into the current language of the zone of the specified key alarm
		/// </summary>
		/// <param name="almCode">the alarm code to get the zone translation</param>
		/// <returns></returns>
		public string getTranslatedZoneFromAlarm(string almCode)
		{
			return getTranslatedZoneFromAlarm(almCode, Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// Gets the translation into the specified language of the zone of the specified key alarm
		/// </summary>
		/// <param name="almCode">the alarm code to get the zone translation</param>
		/// <param name="language">the language to translate into the zone of the given alarm code</param>
		/// <returns></returns>
		public string getTranslatedZoneFromAlarm(string almCode, string language)
		{
			return getTranslatedZone(getZoneCodeFromAlarm(almCode), language);
		}

		/// <summary>
		/// Gets the translation into the current language of the specified key zone.
		/// </summary>
		/// <param name="zoneCode">the zone code to be translated</param>
		/// <returns></returns>
		public string getTranslatedZone(string zoneCode)
		{
			return getTranslatedZone(zoneCode, Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// Gets the translation into the current language of the specified key zone.
		/// </summary>
		/// <param name="zoneCode">the zone code to be translated</param>
		/// <param name="language">the language to translate into the given zone code</param>
		/// <returns></returns>
		public string getTranslatedZone(string zoneCode, string language)
		{
			return getTranslatedAlarmOrZone(zoneCode, language, false);
		}

		/// <summary>
		/// decode a code alarm/zone and translate it into the specified language
		/// </summary>
		/// <param name="code">the code of the alarm or zone to be translated.</param>
		/// <param name="language">the language to translate into the decoded alarm or zone.</param>
		/// <param name="isAlarmCode">if true the code will be searched in the alarm table, if false into the zone table</param>
		/// <returns></returns>
		private string getTranslatedAlarmOrZone(string code, string language, bool isAlarmCode)
		{
			if (code == null || code.Equals(string.Empty))
			{
				return string.Empty;
			}
			else
			{ 
				if (alarm == null || alarm.Tables.Count < 2)
				{
					Logger.LogError(this, "getTranslatedAlarmOrZone - alarm dataset was null or it has not all needed tables. The alarm/zone code has been returned.");
					return code;
				}
				DataTable table = alarm.Tables[(isAlarmCode) ? 0 : 1];
				DataRow foundRow = table.Rows.Find(code);
				if (foundRow == null)
				{ //record is not present into the dataset
					Logger.LogLanguages(this, "getTranslatedAlarmOrZone - record (\"" + code + "\") not found into the table \"" + table.TableName + "\". The alarm/zone code has been returned.");
					return code;
				}
				//record is present into the dataset so look for the specified language
				int index = foundRow.Table.Columns.IndexOf(language);
				if (index == -1)
				{
					if (foundRow[ENGLISH].ToString().Trim().Equals(string.Empty))
					{
						Logger.LogLanguages(this, "getTranslatedAlarmOrZone - language \"" + language + "\" not present into database and english translation is empty, the code has been returned.");
						return code;
					}
					else
					{
						Logger.LogLanguages(this, "getTranslatedAlarmOrZone - language \"" + language + "\" not present into database. The English translation has been returned.");
						return foundRow[ENGLISH].ToString();
					}
				}
				//the specified language is present, check if a string has been specified
				if (foundRow[index].ToString().Equals(""))
				{
					if (foundRow[ENGLISH].ToString().Trim().Equals(string.Empty))
					{
						Logger.LogLanguages(this, "getTranslatedAlarm - Alarm: \"" + code + "\"; missing translation for language \"" + language + "\"and english translation is empty, the code has been returned.");
						return code;
					}
					else
					{
						Logger.LogLanguages(this, "getTranslatedAlarm - Alarm: \"" + code + "\"; missing translation for language \"" + language + "\"and english translation is empty, The English translation has been returned.");
						return foundRow[ENGLISH].ToString();
					}
				}
				//a string has been specified for the message and current language so it is returned
				return foundRow[index].ToString();
			}
		}
	}

	/// <summary>
	/// Provides indentifiers constant for every alarm zone.
	/// </summary>
	public class AlarmZone
	{
		#region GENERAL alarm zones
		public struct General
		{
			public General(int unuseful)
			{
				this.Generic = "Generic";
			}
			public readonly string Generic;
		}
		#endregion
		/// <summary>
		/// Provides all the codes for General alarm zones
		/// </summary>
		public SyncroC.DataBase.AlarmZone.General general;

		#region TWIN400 alarm zones
		public struct Twin400
		{
			public Twin400(int unuseful)
			{
				this.Hardware = "Hardware";
				this.PowerCabinet = "Power cabinet";
				this.LogicCabinet = "Logic cabinet";
				this.BoardMachine = "Board machine";
				this.Console = "Console";
        this.DeviceNET= "Device-Net";
        this.Warnings = "Warnings";
        this.Drives = "Drives";
				this.Software = "Software";
			}
			public readonly string Hardware;
			public readonly string PowerCabinet;
			public readonly string LogicCabinet;
			public readonly string BoardMachine;
			public readonly string Console;
      public readonly string DeviceNET;
      public readonly string Warnings;
      public readonly string Drives;
			public readonly string Software;
		}
		#endregion
		/// <summary>
		/// Provides all the codes for twin400 alarm zones
		/// </summary>
		public SyncroC.DataBase.AlarmZone.Twin400 twin400;

		#region TWINE alarm zones
		public struct TwinE
		{
			public TwinE(int unuseful)
			{
				this.Generic = "GENERIC";
				this.Hardware = "HARDWARE";
				this.Machine = "MACHINE";
				this.Power = "POWER";
				this.Cabinet = "CABINET";
				this.LogicCabinet = "LOGIC CABINET";
				this.Console = "CONSOLE";
				this.Warning = "WARNING";
				this.Comm = "COMM";
			}
			public readonly string Generic;
			public readonly string Hardware;
			public readonly string Machine;
			public readonly string Power;
			public readonly string Cabinet;
			public readonly string LogicCabinet;
			public readonly string Console;
			public readonly string Warning;
			public readonly string Comm;
		}
		#endregion
		/// <summary>
		/// Provides all the codes for twinE alarm zones
		/// </summary>
		public SyncroC.DataBase.AlarmZone.TwinE twinE;

		#region KNIFE alarm zones
		public struct Knife
		{
			public Knife(int unuseful)
			{ 
				this.Generic = "GENERIC";
				this.Hardware = "HARDWARE";
				this.EC = "EC";
				this.Machine = "MACHINE";
				this.Console = "CONSOLE";
				this.Maintenance = "MAINTENANCE";
				this.Comm = "COMM";
			}
			public readonly string Generic;
			public readonly string Hardware;
			public readonly string EC;
			public readonly string Machine;
			public readonly string Console;
			public readonly string Maintenance;
			public readonly string Comm;
		}
		#endregion
		/// <summary>
		/// Provides all the codes for Knife alarm zones
		/// </summary>
		public SyncroC.DataBase.AlarmZone.Knife knife;

		#region EXPRESS alarm zones
		public struct Express
		{
			public Express(int unuseful)
			{
				this.Generic = "GENERIC";
			}
			public readonly string Generic;
		}
		#endregion
		/// <summary>
		/// Provides all the codes for Express alarm zones
		/// </summary>
		public SyncroC.DataBase.AlarmZone.Express express;

		public AlarmZone()
		{
			this.general = new SyncroC.DataBase.AlarmZone.General(1);
			this.express = new SyncroC.DataBase.AlarmZone.Express(1);
			this.knife = new SyncroC.DataBase.AlarmZone.Knife(1);
			this.twin400 = new SyncroC.DataBase.AlarmZone.Twin400(1);
			this.twinE = new SyncroC.DataBase.AlarmZone.TwinE(1);
		}
	}
}