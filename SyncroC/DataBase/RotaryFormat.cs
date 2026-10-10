using System;
using System.IO;
using System.ComponentModel;


using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Summary description for Rotary formats.
	/// </summary>
	
	public class RotaryFormat
	{
		private const string FILE_NAME = "RotaryFormat.ini"; 
		public const int FORMAT_COUNT=10;
		private string fullPathFile = null;
		private RotaryFormatType[] formats;
		private int count;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

		public RotaryFormat()
		{
			Settings s = Settings.getSettings();
      fullPathFile = s.paths.ClientData + FILE_NAME;

			formats=new RotaryFormatType[FORMAT_COUNT];
			for (int i=0; i< FORMAT_COUNT; i++)
				this.formats[i]= new RotaryFormatType(FILE_NAME, i);
			count=0;
		}

    [Category("Rotary Formats"), Description("Rotary Formats"), TypeConverter(typeof(FosberArrayConverter))]
		public RotaryFormatType[] Formats
		{
			get{return this.formats;}
		}

		public void Add(string values)
		{
      this.formats[count++].StartSurveyProperties(ref values, ScorerProfileType.ConfigBaseOperation.PropertiesFromStringAdd); 
				PageManager.getPageManager().refreshAllFormsData();
		}

		public void Clr()
		{
			count=0;
			this.formats.Initialize();
		}

		public void Load()
		{
			string s=string.Empty;
			for (int i=0; i< FORMAT_COUNT; i++)
        this.formats[i].StartSurveyProperties(ref s, ScorerProfileType.ConfigBaseOperation.PropertiesFromINI);

		}

		public void Save()
		{
			string s=string.Empty;
			for (int i=0; i< FORMAT_COUNT; i++)
        this.formats[i].StartSurveyProperties(ref s, ScorerProfileType.ConfigBaseOperation.PropertiesIntoINI);

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

		public RotaryFormat CreateCopy()
		{
			RotaryFormat copy=(RotaryFormat)this.MemberwiseClone();
			copy.formats=new RotaryFormatType[FORMAT_COUNT];
			for (int i=0; i< FORMAT_COUNT; i++)
				copy.formats[i]= (RotaryFormatType)this.formats[i].CreateCopy();
			return copy;
		}

	}
}
