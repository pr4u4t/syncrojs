using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.SyncroConfig
{
  //agrassi_FI18-03-2010_1
  public class TaskSettingsLogLevelConverter : StringConverter
  {
    private string[] logLevel = null;

    public TaskSettingsLogLevelConverter()
      : base()
    {
      this.logLevel = new string[] { "Severe", "Warning", "Info", "Fine" };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.logLevel);
    }
  }


	public class TaskSettings : SyncroBase
	{
		bool debugMode;
    [Category("1. SYNCRO TASK GENERIC SETTINGS"), Description("Rappresenta la modalità di avvio del task. Deve essere sempre valorizzato a 'False'.")]
		public bool DebugMode
		{
			get { return debugMode; }
			set { debugMode = value; }
		}

		string logLevel;
    [Category("1. SYNCRO TASK GENERIC SETTINGS"), TypeConverter(typeof(TaskSettingsLogLevelConverter)), Description("Rappresenta il livello di Log che è usato nella generazione dei messaggi di Log. Il livello di log ha vari livelli e il massimo è 'Fine'. Tipicamente è impostato a 'Info'.")]
		public string LogLevel
		{
			get { return logLevel; }
			set { logLevel = value; }
		}

		bool diskLogEnable;
    [Category("1. SYNCRO TASK GENERIC SETTINGS"), Description("Abilita il salvataggio su file dei messaggi di log. Tipicamente è configurato su 'True'.")]
		public bool DiskLogEnable
		{
			get { return diskLogEnable; }
			set { diskLogEnable = value; }
		}

		int maxLogFileSize;
    [Category("1. SYNCRO TASK GENERIC SETTINGS"), Description("Rappresenta la dimensione del file di log oltre la quale si salva sul punto old. Tipicamente è dimensionato a 5000000 (5MB).")]
		public int MaxLogFileSize
		{
			get { return maxLogFileSize; }
			set { maxLogFileSize = value; }
		}

		bool socketLogEnable;
    [Category("1. SYNCRO TASK GENERIC SETTINGS"), Description("Abilita l'invio del messaggio su socket.")]
		public bool SocketLogEnable
		{
			get { return socketLogEnable; }
			set { socketLogEnable = value; }
		}

		string socketLogIP;
    [Category("1. SYNCRO TASK GENERIC SETTINGS"), Description("Indirizzo IP a cui viene mandato via socket il messaggio di log ( 127.0.0.1 per visualizzazione su Logger quando avviato sul PC Server di Syncro ).")]
		public string SocketLogIP
		{
			get { return socketLogIP; }
			set { socketLogIP = value; }
		}

		protected TaskSettings()
		{
			this.sectionName = "_HEADER";

			debugMode = false;
			logLevel = string.Empty;
			diskLogEnable = false;
			maxLogFileSize = 5000000;
			socketLogEnable = false; ;
			socketLogIP = "127.0.0.1";
		}

	}
}
