using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace SyncroC.Utility
{
	/// <summary>
	/// Provides various functionality for logging.
	/// If the Logger needs to log something then it uses the System Console 
	/// and the Windows event log.
	/// </summary>
	public class Logger
	{
		/// <summary>Needed to realize the singleton pattern.</summary>
		private static Logger logger = null;

		//console log
		/// <summary>Prefix to be used before every log on the console.</summary>
		private static string CONSOLE_LOG_PREFIX = "* * * LOGGER ERROR:\n";

		/// <summary>string containing eventual errors occured during creation of the 
		/// windows log and source. This string will be tried to be logged.</summary>
		private string lastLoggerError = "";

		//GenericError log file info
		/// <summary>Name of the file for generic error log.</summary>
		private static string GENERIC_ERROR_FILENAME = "SyncroC";
		/// <summary>Header of the file for generic error log.</summary>
		private static string GENERIC_ERROR_HEADER = "SyncroC GENERIC ERROR log file automatically generated on " 
			+ DateTime.Now.ToLongDateString() + "\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * ";
		/// <summary>Main xml tag for generic error log.</summary>
		//private static string GENERIC_ERROR_MAIN_TAG = "ErrorLog";

		//Languages log file info
		/// <summary>Name of the file for languages log.</summary>
		private static string LANGUAGES_FILENAME = "Languages";
		/// <summary>Header of the file for languages log.</summary>
		private static string LANGUAGES_HEADER = "SyncroC LANGUAGES ERROR log file automatically generated on "
			+ DateTime.Now.ToLongDateString() + "\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * ";

		private static string ORDER_TCP_FILENAME = "OrderTCP";
		private static string ORDER_TCP_HEADER = "SyncroC order TCP diagnostic log automatically generated on "
			+ DateTime.Now.ToLongDateString() + "\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * ";

		//log file info
		/// <summary>PostFix to be added to every old log file name.</summary>
		private static string OLD_LOGFILE_POSTFIX = "_OLD";
		/// <summary></summary>
		private static string LOG_STR_SEP = " | ";

		/// <summary>full path of the Generic error log file</summary>
		private static string GenericErrorFullPath = null;
		/// <summary>full path of the languages log file</summary>
		private static string LanguagesFullPath = null;
		private static string OrderTcpFullPath = null;

		/// <summary>Stream to be used to write into log files.</summary>
		private static StreamWriter streamWriter = null;

		/// <summary>Max number of bytes a log file can grow up</summary>
		private static long logFileMaxSize;
    //agrassi_FI19-12-08_1
    private System.Threading.Mutex logMutex;

		/// <summary>
		/// Builder.
		/// </summary>
		private Logger()
		{
			Settings s = Settings.getSettings();
			logFileMaxSize = s.logger.MaxLogFileSize;

			//log folder management
      string LogFileFolder = Settings.getSettings().paths.Log;
				
			if (! Directory.Exists(LogFileFolder)) 
			{	//the folder does not exist: creation
				try
				{
					Directory.CreateDirectory(LogFileFolder);
				}
				catch (Exception ex)
				{
					string errorMsg = "Error creating folder; " + ex.Message;
					Console.WriteLine(CONSOLE_LOG_PREFIX + "Logger - " + errorMsg);
				}
			}

			//GenericError management
			GenericErrorFullPath = LogFileFolder + GENERIC_ERROR_FILENAME + ".txt";
			if (! File.Exists(GenericErrorFullPath))
				CreateEmptyLogFile(GenericErrorFullPath, GENERIC_ERROR_HEADER);
			//LanguagesManagement
			LanguagesFullPath = LogFileFolder + LANGUAGES_FILENAME + ".txt";
			if (! File.Exists(LanguagesFullPath))
				CreateEmptyLogFile(LanguagesFullPath, LANGUAGES_HEADER);
			OrderTcpFullPath = LogFileFolder + ORDER_TCP_FILENAME + ".txt";
			if (! File.Exists(OrderTcpFullPath))
				CreateEmptyLogFile(OrderTcpFullPath, ORDER_TCP_HEADER);

			//if an error occured during windows log event source creation try to log it...
			if (lastLoggerError != null && ! lastLoggerError.Equals(""))
			{
				Console.WriteLine(CONSOLE_LOG_PREFIX + lastLoggerError);
				LogError(this, lastLoggerError); 
				lastLoggerError = "";
			}
      //agrassi_FI19-12-08_1
      this.logMutex=new System.Threading.Mutex();
		}

		/// <summary>
		/// Create a new "log" file and initialize it
		/// </summary>
		/// <param name="fullPath">full path of the file to be created.</param>
		/// <param name="header">the header to use for the new file initialization.</param>
		private static void CreateEmptyLogFile(string fullPath, string header)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(fullPath);
				fileInfo.Create();
				streamWriter = fileInfo.AppendText();
				streamWriter.WriteLine(header);		
			}
			catch (Exception ex)
			{
				string errorMsg = "Error creating an empty file; " + ex.Message;
				Console.WriteLine(CONSOLE_LOG_PREFIX + "CreateEmptyLogFile - " + errorMsg);
			}
			finally
			{
				if (streamWriter != null) 
				{
					try
					{
						streamWriter.Close();
					}
					catch (Exception e)
					{
						string errorMsg = "Error closing the StremWriter; " + e.Message;
						Console.WriteLine(CONSOLE_LOG_PREFIX + "CreateEmptyLogFile - " + errorMsg);
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		~Logger()
		{
			if (streamWriter != null) 
			{
				try
				{
					streamWriter.Close();
				}
				catch (Exception e)
				{
					string errorMsg = "Error closing the StremWriter; " + e.Message;
					Console.WriteLine(CONSOLE_LOG_PREFIX + "CreateEmptyLogFile - " + errorMsg);
				}
			}
		}

		/// <summary>
		/// Start the log stuff. If no log is enabled no log is started.
		/// </summary>
		/// <returns></returns>
		public static Logger startLogger()
		{
			if (logger == null &&
					Settings.getSettings().logger.Enabled)
				logger = new Logger();
			return logger;
		}

		/// <summary>
		/// Check if the log file exists, if it doesn't create it.
		/// Check dimensions, if too big move data in a _OLD file.
		/// </summary>
		/// <param name="fullPathFile">The full path of the file to check.</param>
		/// <param name="header">The header to use in case of creation of the file.</param>
		/// <returns>a file stream for the specified file already opened in "write" mode.</returns>
		private static StreamWriter getFileStream(string fullPathFile, string header)
		{
			FileInfo fileInfo = new FileInfo(fullPathFile);
			if (File.Exists(fullPathFile))
			{
				
				if (fileInfo.Length >= logFileMaxSize)
				{//the file is too big
					string fullPathOldFile = "";
					int dotIndex = fullPathFile.LastIndexOf('.');
					if (dotIndex != -1)
						fullPathOldFile =
							fullPathFile.Substring(0, dotIndex) + OLD_LOGFILE_POSTFIX + fullPathFile.Substring(dotIndex);
					else
						fullPathOldFile += OLD_LOGFILE_POSTFIX;
					try
					{
						fileInfo.CopyTo(fullPathOldFile, true);
					}
					catch (Exception ex)
					{
						string errorMsg = "Error coping a log file to an _OLD log file; " + ex.Message;
						Console.WriteLine(CONSOLE_LOG_PREFIX + "getFileStream - " + errorMsg);
					}
				}
				else
					//file exists and it is not too big.
					return fileInfo.AppendText();
			}
			//the file does not exist or it was too big: creation, initialization and return the file stream.
			CreateEmptyLogFile(fullPathFile, header);//parametrizzare l'header
			return fileInfo.AppendText();
		}

		/// <summary>
		/// Log an error in the specified file
		/// </summary>
		/// <param name="senderStr">string descibing the object is using the log system </param>
		/// <param name="msg">the message to be logged</param>
		/// <param name="fullPathFile">the file where to log</param>
		/// <param name="mainTag">a string representing the main tag to use into the log file</param>
		/// /// <param name="header">the header to eventually use in case of creation of file.</param>
		private static void Log(string senderStr, string msg, string fullPathFile, string header)
		{
			if (logger == null) 
				return; //the logger has not been started or, if it has, it was not enebled.	
      //agrassi_FI19-12-08_1
      if (logger!=null)
        logger.logMutex.WaitOne();
			try
			{
				streamWriter = getFileStream(fullPathFile, header);
				streamWriter.WriteLine(generateTag(senderStr, msg));
			}
			catch (Exception ex)
			{
				string errorMsg = "Error writing into a log file; " + ex.Message;
				Console.WriteLine(CONSOLE_LOG_PREFIX + "Log - " + errorMsg);
			}
			finally
			{
				if (streamWriter != null) 
				{
					try
					{
						streamWriter.Close();
					}
					catch (Exception e)
					{
						string errorMsg = "Error closing the StremWriter; " + e.Message;
						Console.WriteLine(CONSOLE_LOG_PREFIX + "CreateEmptyLogFile - " + errorMsg);
					}
				}
			}
      //agrassi_FI19-12-08_1
      if (logger != null)
        logger.logMutex.ReleaseMutex();
    }

		/// <summary>
		/// Log a generic error.
		/// </summary>
		/// <param name="sender">the object is using the log system (usually the "this" pointer is specified)</param>
		/// <param name="msg">the message to be logged</param>
		public static void LogError(Object sender, string msg)
		{
			string senderStr = "";
			if (sender != null)
				senderStr = sender.ToString();
			Log(senderStr, msg, GenericErrorFullPath, GENERIC_ERROR_HEADER);
		}

		/// <summary>
		/// Log a generic error.
		/// </summary>
		/// <param name="sender">the object is using the log system (usually the "this" pointer is specified)</param>
		/// <param name="msg">the message to be logged</param>
		/// <param name="exception">an exception eventually occured into the code</param>
		public static void LogError(Object sender, string msg, Exception exception)
		{
			string senderStr = "";
			if (sender != null)
				senderStr = sender.ToString();
      Log(senderStr, msg + "; "+GetExceptionInfo(exception) , GenericErrorFullPath, GENERIC_ERROR_HEADER);
    }

		/// <summary>
		/// Log a generic error.
		/// </summary>
		/// <param name="sender">the object is using the log system (usually the "this" pointer is specified)</param>
		/// <param name="exception">an exception eventually occured into the code</param>
		public static void LogError(Object sender, Exception exception)
		{
			string senderStr = "";
			if (sender != null)
				senderStr = sender.ToString();
			Log(senderStr, GetExceptionInfo(exception), GenericErrorFullPath, GENERIC_ERROR_HEADER);
    }

		/// <summary>
		/// Log a generic error.
		/// </summary>
		/// <param name="sender">string that descibes the object is using the log system</param>
		/// <param name="msg">the message to be logged</param>
		public static void LogError(string senderStr, string msg)
		{
			Log(senderStr, msg, GenericErrorFullPath, GENERIC_ERROR_HEADER);
		}

		/// <summary>
		/// Log a generic error.
		/// </summary>
		/// <param name="sender">string that descibes the object is using the log system</param>
		/// <param name="msg">the message to be logged</param>
		/// <param name="exception">an exception eventually occured into the code</param>
		public static void LogError(string senderStr, string msg, Exception exception)
		{
			Log(senderStr, msg + "; " + GetExceptionInfo(exception), GenericErrorFullPath, GENERIC_ERROR_HEADER);
    }

		/// <summary>
		/// Log a generic error.
		/// </summary>
		/// <param name="sender">string that descibes the object is using the log system</param>
		/// <param name="exception">an exception eventually occured into the code</param>
		public static void LogError(string senderStr, Exception exception)
		{
			Log(senderStr, GetExceptionInfo(exception), GenericErrorFullPath, GENERIC_ERROR_HEADER);
    }

		public static void LogOrderTcp(string message)
		{
			Log("OrderTCP", message, OrderTcpFullPath, ORDER_TCP_HEADER);
		}

		public static void LogOrderTcp(string message, Exception exception)
		{
			Log("OrderTCP", message + "; " + GetExceptionInfo(exception), OrderTcpFullPath, ORDER_TCP_HEADER);
		}

		/// <summary>
		/// Log a LanguageDB error.
		/// </summary>
		/// <param name="sender">the object is using the log system (usually the "this" pointer is specified)</param>
		/// <param name="msg">the message to be logged</param>
		public static void LogLanguages(Object sender, string msg)
		{
			string senderStr = "";
			if (sender != null)
				senderStr = sender.ToString();
			Log(senderStr, msg, LanguagesFullPath, LANGUAGES_HEADER);
		}

		/// <summary>
		/// Log a LanguageDB error.
		/// </summary>
		/// <param name="sender">the object is using the log system (usually the "this" pointer is specified)</param>
		/// <param name="msg">the message to be logged</param>
		public static void LogLanguages(Object sender, string msg, Exception exception)
		{
			string senderStr = "";
			if (sender != null)
				senderStr = sender.ToString();
			Log(senderStr, msg + "; " + exception.Message, LanguagesFullPath, LANGUAGES_HEADER);
		}

		/// <summary>
		/// Log a LanguageDB error.
		/// </summary>
		/// <param name="sender">the object is using the log system (usually the "this" pointer is specified)</param>
		/// <param name="msg">the message to be logged</param>
		public static void LogLanguages(Object sender, Exception exception)
		{
			string senderStr = "";
			if (sender != null)
				senderStr = sender.ToString();
			Log(senderStr, exception.Message, LanguagesFullPath, LANGUAGES_HEADER);
		}

		/// <summary>
		/// Generate an xml tag to log one message.
		/// </summary>
		/// <param name="sender">The object that requested the log.</param>
		/// <param name="msg">the message to log.</param>
		/// <param name="mainTag">the main tag to use.</param>
		/// <returns>the string to put into the xml log file.</returns>
		private static string generateTag(string senderStr, string msg)
		{
			if (!senderStr.IndexOf(',').Equals(-1))
				senderStr = senderStr.Substring(0, senderStr.IndexOf(','));
			DateTime now = DateTime.Now;
			return now.ToShortDateString() + LOG_STR_SEP +
				now.ToLongTimeString() + "." + FormatMilliseconds(now.Millisecond) + LOG_STR_SEP +
				msg;
		}

		private static string FormatMilliseconds(int milliseconds)
		{
			System.Text.StringBuilder s = new System.Text.StringBuilder(milliseconds.ToString());
			while (s.Length < 3)
				s.Insert(0, "0");
			return s.ToString();
		}

    private static string GetExceptionInfo(Exception e)
    {
      Exception ex = e;
      while (ex.InnerException != null)
        ex = ex.InnerException;
      string s = ex.Message;
			if (ex.StackTrace != null)
			{
				int indLine = ex.StackTrace.IndexOf(":line ");
				int indAt = 0;
				if (indLine > 0)
					indAt = ex.StackTrace.IndexOf("at", indLine) - 1;
				if (indAt >= 0)
					s += ", " + ex.StackTrace.Substring(0, indAt);
			}
      return s;
    }
  }
}
