using System;
using System.Data;
using System.Runtime.InteropServices;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Contains all the Messages translations
	/// </summary>
	public class Message : SyncroC.DataBase.Interfaces.IDatabase
	{

#if DEBUG 
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#else
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#endif

		public static extern int FileCrc32Assembly([MarshalAs(UnmanagedType.LPTStr)] string szFilename, ref int dwCrc32);
		
		private readonly string FILENAME = "Message";
		private string DataBaseXMLFileName = "";
		private string DataBaseXSDFileName = "";

		/// <summary>
		/// languages dataset reference
		/// </summary>
		public DataSet message = null;

		public Message(string DataBaseFolder)
		{
			//set the file name
			DataBaseXMLFileName=DataBaseFolder + FILENAME + ".xml";
			DataBaseXSDFileName=DataBaseFolder + FILENAME + ".xsd";
			//datasets instantiation
			message = new DataSet();
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
			if (message != null)
				message.Clear();
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
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_MESSAGE + "()");
					return;
				}
				if (values[0].Trim() != (checkSchema.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXSDFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_MESSAGE + "()");
					return;
				}
				//xml checking
				ret = FileCrc32Assembly(DataBaseXMLFileName, ref checkXml);
				if (ret != 0)
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXMLFileName + " error: " + ret);
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_MESSAGE + "()");
					return;
				}
				if (values[1].Trim()!=(checkXml.ToString("x")))
				{
					Logger.LogError(this, "Chk - file: " + DataBaseXMLFileName + " wrong checksum.");
					cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_MESSAGE + "()");
					return;
				}
			}
			catch (DllNotFoundException ex)
			{
				Logger.LogError(this, "Collect - Dll for checksum calculation not found.", ex);
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_MANAGER + "." + Command.DB_MESSAGE + "()");
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
			if (message != null)
			{
				message.Clear();
				try
				{//tenta la lettura dello schema salvato sul client.
					message.ReadXmlSchema(DataBaseXSDFileName);
					try
					{//tenta la lettura dei dati salvati sul client.
						message.ReadXml(DataBaseXMLFileName, XmlReadMode.IgnoreSchema);
						//call the refresh data on every instantiated form
						PageManager.getPageManager().refreshAllFormsData();
					}
					catch (Exception ex)
					{
						Logger.LogError(this,
							"Load - error during reading data of Message database", ex);
					}
				}
				catch (Exception ex)
				{
					Logger.LogError(this,
						"Load - error during reading schema of Message database", ex);
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
			if (message != null)
			{
			try
			{
				message.WriteXml(DataBaseXMLFileName);
				message.WriteXmlSchema(DataBaseXSDFileName);
			}
			catch (Exception ex)
			{
				Logger.LogError(this, ex);
			}
		}
		else
			Logger.LogError(this, "Save - Cannot complete, dataset was null");
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
		/// decode a string of code messages and translate them into the current language.
		/// </summary>
		/// <param name="msgCode">the string to be decoded.</param>
		/// <returns></returns>
		public string getTranslatedMessage(string msgCode)
		{
			return getTranslatedMessage(msgCode, Settings.getSettings().CurrentLanguage);
		}

		/// <summary>
		/// decode a string of code messages and translate them into the specified language.
		/// </summary>
		/// <param name="msgCode">the string to be decoded.</param>
		/// <param name="language">the language to translate into the decoded messages.</param>
		/// <returns></returns>
		public string getTranslatedMessage(string msgCode, string language)
		{
			string decodedMsg = string.Empty;
			while (msgCode != null && ! msgCode.Equals("") &&
				(msgCode.StartsWith(PageManager.PRE_MSG_CODE) || msgCode.StartsWith(PageManager.PRE_MSG_ATTRIBUTE)))
			{
				int indexCode = msgCode.IndexOf(PageManager.PRE_MSG_CODE, 1);
				int indexAttribute = msgCode.IndexOf(PageManager.PRE_MSG_ATTRIBUTE, 1);
				if (indexCode == -1)
				{
					if (indexAttribute == -1)
					{//last loop, there are not any other string to decode
						decodedMsg += decodeMsg(msgCode, language);
						msgCode = "";
					}
					else
					{//next string to be decoded is an msgAttribute and there are no more msgCode
						decodedMsg += decodeMsg(msgCode.Substring(0, indexAttribute), language) + " ";
						msgCode = msgCode.Substring(indexAttribute);
					}
				}
				else
				{
					if (indexAttribute == -1)
					{//next string to be decoded is an msgCode and there are no more msgAttribute
						decodedMsg += decodeMsg(msgCode.Substring(0, indexCode), language) + " ";
						msgCode = msgCode.Substring(indexCode);
					}
					else
					{
						int min = Math.Min(indexCode, indexAttribute);
						decodedMsg += decodeMsg(msgCode.Substring(0, min), language) + " ";
						msgCode = msgCode.Substring(min);
					}
				}
			}
			return (decodedMsg.Equals(string.Empty)) ? msgCode : decodedMsg;
		}

		/// <summary>
		/// decode a code message and translate it into the specified language.
		/// utility for the method getTranslatedMessage.
		/// </summary>
		/// <param name="msgCode">the code to be decoded.</param>
		/// <param name="language">the language to translate into the decoded message.</param>
		/// <returns></returns>
		private string decodeMsg(string msgCode, string language)
		{
			if (msgCode.StartsWith(PageManager.PRE_MSG_ATTRIBUTE))
			{	//return the attribute
				return msgCode.Substring(PageManager.PRE_MSG_ATTRIBUTE.Length);
			}
			
			if (msgCode.StartsWith(PageManager.PRE_MSG_CODE))
			{//decoding and translation
				msgCode = msgCode.Substring(PageManager.PRE_MSG_CODE.Length); 
				if (message == null || message.Tables.Count == 0)
				{
					Logger.LogError(this, "getTranslatedMessage - messages dataset was null or it has no tables. The message code has been returned.");
					return msgCode;
				}
				DataRow foundRow = message.Tables[0].Rows.Find(msgCode);
				if (foundRow == null)
				{ //record is not present into the dataset
					Logger.LogLanguages(this, "getTranslatedMessage - record (\"" + msgCode + "\") not found. The message code has been returned.");
					return msgCode;
				}
				//record is present into the dataset so look for the specified language
				int index = foundRow.Table.Columns.IndexOf(language);
				if (index == -1)
				{
					if (foundRow[1].ToString().Trim().Equals(string.Empty))
					{
						Logger.LogLanguages(this, "getTranslatedMessage - language \"" + language + "\" not present into database, the English translation is empty: msg code has been returned.");
						return msgCode;
					}
					else
					{
						Logger.LogLanguages(this, "getTranslatedMessage - language \"" + language + "\" not present into database. The English translation has been returned.");
						return foundRow[1].ToString();
					}
				}
				//the specified language is present, check if a string has been specified
				if (foundRow[index].ToString().Equals(""))
				{
					if (foundRow[1].ToString().Trim().Equals(string.Empty))
					{
						Logger.LogLanguages(this, "getTranslatedMessage - Message: \"" + msgCode + "\"; missing translation for language \"" + language + "\", the english translation is empty : the msg code has been returned.");
						return msgCode;
					}
					else
					{
						Logger.LogLanguages(this, "getTranslatedMessage - Message: \"" + msgCode + "\"; missing translation for language \"" + language + "\". The english translation has been returned.");
						return foundRow[1].ToString();
					}
				}
				//a string has been specified for the message and current language so it is returned
				return foundRow[index].ToString();
			}
			return "";
		}
	}
}