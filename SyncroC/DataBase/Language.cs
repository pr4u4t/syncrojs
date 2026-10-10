using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{

	/// <summary>
	/// Contains all the text Language translation except for the alarms and messages.
	/// </summary>
	public class Language : SyncroC.DataBase.Interfaces.IDatabase
	{

#if DEBUG 
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#else
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#endif

		public static extern int FileCrc32Assembly([MarshalAs(UnmanagedType.LPTStr)] string szFilename, ref int dwCrc32);
		
		private readonly string FILENAME = "Language";
		private string DataBaseXMLFileName = "";
		private string DataBaseXSDFileName = "";
    private string DataBaseXMLFileNameOnServer = "";
    private string DataBaseXSDFileNameOnServer = "";

		/// <summary>
		/// languages dataset reference
		/// </summary>
		public DataSet language = null;
    //agrassi_FI04-07-08
    public DataSet bckLanguage = null;

		public Language(string DataBaseFolder)
		{
			//set the file name
			DataBaseXMLFileName=DataBaseFolder + FILENAME + ".xml";
			DataBaseXSDFileName=DataBaseFolder + FILENAME + ".xsd";
      DataBaseXMLFileNameOnServer = Settings.getSettings().paths.Translations + FILENAME + ".xml";
      DataBaseXSDFileNameOnServer = Settings.getSettings().paths.Translations + FILENAME + ".xsd";
      //datasets instantiation
			language = new DataSet();
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
			if (language != null)
				language.Clear();
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
			Settings settings = Settings.getSettings();
			if (settings.dataBase.RestoreDBLanguages && settings.configuration.getOnServer())
				//se sto ricostruendo esco subito senza chiedere niente al server
				return;

			string[] values = checksum.Split(new Char[] {','});
			if (values.Length!=2)
			{
				Logger.LogError(this, "Chk - Invalid Parameter" );
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
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_LANGUAGE + "()");
					return;
				}
				if (values[0].Trim() != (checkSchema.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXSDFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_LANGUAGE + "()");
					return;
				}
				//xml checking
				ret = FileCrc32Assembly(DataBaseXMLFileName, ref checkXml);
				if (ret != 0)
				{
					Logger.LogError(this, "Chk - file: \"" + DataBaseXMLFileName + "\" not found.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_LANGUAGE + "()");
					return;
				}
				if (values[1].Trim()!=(checkXml.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: \"" + DataBaseXMLFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_LANGUAGE + "()");
					return;
				}
			}
			catch (DllNotFoundException ex)
			{
				Logger.LogError(this, "Collect - Dll for checksum calculation not found.", ex);
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_LANGUAGE + "()");
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
			if (this.language != null)
			{

				this.language.Clear();
				try
				{
          //tenta la lettura dello schema salvato sul client.
          if (File.Exists(DataBaseXSDFileName))
            if (this.language.Tables.Count == 0)
              this.language.ReadXmlSchema(DataBaseXSDFileName);

					try
					{
            //tenta la lettura dei dati salvati sul client.
            if (File.Exists(DataBaseXMLFileName))
            {
              this.language.ReadXml(DataBaseXMLFileName, XmlReadMode.IgnoreSchema);

              //call the refresh data on every instantiated form
              PageManager.getPageManager().refreshAllFormsData();
            }
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "Load - error during reading data of Language database", ex);
					}

				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - error during reading schema of Language database", ex);
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
			if (language != null)
			{
				try
				{
          language.WriteXml(DataBaseXMLFileName);
          language.WriteXmlSchema(DataBaseXSDFileName);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Save - Error saving database", ex);
				}
			}
			else
				Logger.LogError(this, "Save - Cannot complete, dataset was null");
		}

    public void Save(bool onServer)
    {
      this.Save();
      if (onServer && Settings.getSettings().configuration.getOnServer() && language!=null)
      {
        try
        {
          bool restoreAttributes = false;
          FileAttributes fa=File.GetAttributes(DataBaseXMLFileNameOnServer);
          if ((fa & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
          {
            File.SetAttributes(DataBaseXMLFileNameOnServer, (FileAttributes)(fa - FileAttributes.ReadOnly));
            restoreAttributes = true;
          }
          language.WriteXml(DataBaseXMLFileNameOnServer);
          if (restoreAttributes)
            File.SetAttributes(DataBaseXMLFileNameOnServer, fa);
          restoreAttributes = false;
          fa = File.GetAttributes(DataBaseXSDFileNameOnServer);
          if ((fa & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
          {
            File.SetAttributes(DataBaseXSDFileNameOnServer, (FileAttributes)(fa - FileAttributes.ReadOnly));
            restoreAttributes = true;
          }
          language.WriteXmlSchema(DataBaseXSDFileNameOnServer);
          if (restoreAttributes)
            File.SetAttributes(DataBaseXSDFileNameOnServer, fa);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "Save(true) - Error saving database", ex);
        }
      }
    }

		/// <summary>
		/// Call the PageManager.SetUpAllForms() method to update the dataset language and then save the dataset on disk.
		/// </summary>
		/// <returns>true if no errors occured, false otherwise</returns>
		public bool Update()
		{
      //agrassi_FI04-07-08
      this.bckLanguage = this.language.Copy();
      this.language.Clear();
			if (! GenerateEnumTranslations()) 
				return false;
			if (! PageManager.getPageManager().SetUpAllForms())
				return false;
			this.Save(true);
			//this.Load();
      //agrassi_FI04-07-08
      this.bckLanguage.Dispose();
      this.bckLanguage = null;
      return true;
		}

    //agrassi_FI04-07-08
    public void AddItem(string key, string translation)
    {
      DataRow newRow = null, newRowBck = null;
      if (this.bckLanguage != null)
      {
        newRowBck = this.bckLanguage.Tables[0].Rows.Find(key);
        if (newRowBck != null)
        {
          newRow = this.language.Tables[0].NewRow();
          newRow.ItemArray = newRowBck.ItemArray;
          this.language.Tables[0].Rows.Add(newRow);
          return;
        }
      }

      newRow = this.language.Tables[0].NewRow();
      if (newRow == null || newRow.Table.Columns.Count < 2)
      {
        Logger.LogLanguages(this, "AddItem - impossible to create a new record for the language dataset or the language dataset schema is wrong (update failed).");
        return;
      }
      newRow[0] = key;
      newRow[1] = translation;
      for (int i = 2; i < newRow.ItemArray.Length; i++)
        newRow[i] = "";
      this.language.Tables[0].Rows.Add(newRow);
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
		/// Reflect the FosbersoftEnums class in order to insert records for enum constants 
		/// translation into the languiage database.
		/// </summary>
		/// <returns></returns>
		private bool GenerateEnumTranslations()
		{
			if (language.Tables.Count > 0 && language.Tables[0] != null)
			{
				DataTable dt = language.Tables[0];
				FieldInfo[] fieldsInfo = typeof(FosbersoftEnums).GetFields(
					BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (FieldInfo fieldInfo in fieldsInfo)
				{
					if (fieldInfo.FieldType.IsEnum)
					{
						string[] names = Enum.GetNames(fieldInfo.FieldType);
						foreach (string name in names)
						{
							string key = fieldInfo.FieldType.Name + "." + name;
							DataRow row = dt.Rows.Find(key);
							if (row == null)
							{//record is missing 
                //agrassi_FI04-07-08
                //row = dt.NewRow();
                //row[0] = key;
                //row[1] = name;
                //for (int i = 2; i < row.ItemArray.Length; i++)
                //  row[i] = string.Empty;
                //language.Tables[0].Rows.Add(row);
                this.AddItem(key, name);
							}
						}
					}
				}
				return true;
			}
			else
			{
				Logger.LogError(this, "GenerateEnumTranslations - table not found in language database.");
				return false;
			}
		}

		/// <summary>
		/// Gets the translation for the specified key into the current language.
		/// </summary>
		/// <param name="key">The key to get the translation for.</param>
		/// <returns></returns>
		public string getTranslation(string key)
		{
			return getTranslation(key, Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// Gets the translation for the specified key into the specified language.
		/// </summary>
		/// <param name="key">The key to get the translation for.</param>
		/// <param name="lang">the language to translate into the key.</param>
		/// <returns></returns>
		public string getTranslation(string key, string lang)
		{
			if (language == null || language.Tables.Count == 0)
			{
				Logger.LogError(this, "getTranslation - language dataset was null or it has no tables. The key has been returned.");
				return key;
			}
			DataRow foundRow = language.Tables[0].Rows.Find(key);
			if (foundRow == null)
			{ //record is not present into the dataset
				Logger.LogLanguages(this, "getTranslation - record (\"" + key + "\") not found. The key has been returned.");
				return key;
			}
			//record is present into the dataset so look for the specified language
			int index = foundRow.Table.Columns.IndexOf(lang);
			if (index == -1)
			{
				if (foundRow[1].ToString().Trim().Equals(string.Empty))
				{
					Logger.LogLanguages(this, "getTranslation - language \"" + lang + "\" not present into database, the English translation is empty: key has been returned.");
					return key;
				}
				else
				{
					Logger.LogLanguages(this, "getTranslation - language \"" + lang + "\" not present into database. The English translation has been returned.");
					return foundRow[1].ToString();
				}
			}
			//the specified language is present, check if a string has been specified
			if (foundRow[index].ToString().Equals(""))
			{
				if (foundRow[1].ToString().Trim().Equals(string.Empty))
				{
					Logger.LogLanguages(this, "getTranslation - Key: \"" + key + "\"; missing translation for language \"" + lang + "\", the english translation is empty : the key has been returned.");
					return key;
				}
				else
				{
					Logger.LogLanguages(this, "getTranslation - Key: \"" + key + "\"; missing translation for language \"" + lang + "\". The english translation has been returned.");
					return foundRow[1].ToString();
				}
			}
			//a string has been specified for the key and current language so it is returned
			return foundRow[index].ToString();
		}
	}
}
