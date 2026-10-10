using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase.SyncroConfig
{
  //RFazzi_FI17/01/2008_1.Added task configurations on graphics
  public class TextToSpeechLanguage : StringConverter
  {
    private string[] language = null;

    public TextToSpeechLanguage()
      : base()
    {
      //English
      //Italian
      //German
      //Spanish
      //Dutch
      //Czech
      //French
      //Portuguese
      //Polish
      //Finnish
      //Swedish
      //Chinese
      //Turkish
      //Russian
      //Norwegian

      this.language = new string[] { "English", "Italian" };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.language);
    }
  }

	public class GeneralSettings : SyncroBase
	{
		//control
		bool wetEndControl;
    [Category("01. SYNCRO SYSTEM CONTROL"), Description("Se TRUE è abilitato il modulo WetEnd Control.")]
		public bool WetEndControl
		{
			get { return wetEndControl; }
			set { wetEndControl = value; }
		}

		bool dryEndControl;
    [Category("01. SYNCRO SYSTEM CONTROL"), Description("Se TRUE è abilitato il modulo DryEnd Control.")]
		public bool DryEndControl
		{
			get { return dryEndControl; }
			set { dryEndControl = value; }
		}

		//job & commessa
		string job;
		[Category("02. FOSBER IDENTIFIERS"), Description("Fosber \"job\".")]
		public string Job
		{
			get { return job; }
			set { job = value; }
		}

		string comm;
		[Category("02. FOSBER IDENTIFIERS"), Description("Fosber \"commessa\".")]
		public string Comm
		{
			get { return comm; }
			set { comm = value; }
		}

    //agrassi_FI05-03-09_3
    //agrassi_FI19-08-2010_1
    //string softwareSTD;
    //[Category("2. FOSBER IDENTIFIERS"), Description("If \"a\", the version is standard. Otherwise it indicates that some changes have been done on the standard version.")]
    //public string SoftwareSTD
    //{
    //  get { return this.softwareSTD; }
    //  set { this.softwareSTD = value; }
    //}

		string dspUserTask;
    [Category("02. FOSBER IDENTIFIERS"), Description("Impostare il numero della revisione del task installato sui DSP400.")]
		public string DSPUserTask
		{
			get { return dspUserTask; }
			set { dspUserTask = value; }
		}
    
    //agrassi_FI19-03-08
    bool  logDiskUTF8;
    [Category("03. LOG"), Description("Lasciare a TRUE se non richiesto da UTP.")]
    public bool LogDiskUTF8
    {
      get { return logDiskUTF8; }
      set { logDiskUTF8 = value; }
    }

    //agrassi_FI20-01-09
    int maxOfficeClients;
    [Category("04. LICENCES FOR OFFICE CLIENTS"), Description("Impostare il numero di client grafici collegabili a Syncro dalla rete aziendale secondo quanto riportato su GM/PST.")]
    public int MaxOfficeClients
    {
      get { return maxOfficeClients; }
      set { maxOfficeClients = value; }
    }

    //ABELLO mysql
    bool sQL_USED;
    [Category("05. Sql"), Description("Se TRUE Syncro storicizza su Dbase PostGres i dati degli ordini prodotti e parametri utilizzati dalle macchine interfacciate.")]
    public bool SQL_USED
    {
      get { return sQL_USED; }
      set { sQL_USED = value; }
    }

    bool sQL_USED_FOR_DATABASE_MANAGEMENT;
    [Category("05. Sql"), Description("Se TRUE Syncro utilizza i dati memorizzati sul dabase PostGres per la visualizzazione a grafica. Questo deve essere abilitato ogni volta che è presente il modulo Process o è venduta l'optional “Record” ( vedi configurazione CNF o PST ).")]
    public bool SQL_USED_FOR_DATABASE_MANAGEMENT
    {
      get { return sQL_USED_FOR_DATABASE_MANAGEMENT; }
      set { sQL_USED_FOR_DATABASE_MANAGEMENT = value; }
    }

    string sQL_SERVER_IP;
    [Category("05. Sql"), Description("Ip Address. Lasciare '127.0.0.1'")]
    public string SQL_SERVER_IP
    {
      get { return sQL_SERVER_IP; }
      set { sQL_SERVER_IP = value; }
    }

    string sQL_USER;
    [Category("05. Sql"), Description("Qualora è configurato a TRUE il parametro Sql_Used, in questo campo deve essere inserito 'postgres'.")]
    public string SQL_USER
    {
      get { return sQL_USER; }
      set { sQL_USER = value; }
    }

    string sQL_PASSWORD;
    [Category("05. Sql"), Description("Qualora è configurato a TRUE il parametro Sql_Used, in questo campo deve essere inserito 'fosber55060'.")]
    public string SQL_PASSWORD
    {
      get { return sQL_PASSWORD; }
      set { sQL_PASSWORD = value; }
    }

    string sQL_DATABASE;
    [Category("05. Sql"), Description("Qualora è configurato a 'TRUE' il parametro Sql_Used, in questo campo deve essere inserito 'syncro_db'."), ReadOnly(true)]
    public string SQL_DATABASE
    {
      get { return (this.sQL_DATABASE); }
      set { this.sQL_DATABASE = value; }
    }

    public string getSQL_DATABASE()
    {
      return this.sQL_DATABASE + "_" + this.job;
    }

    //PMASSARO_FI16/12/09 - New ShutDown Managment
    bool enableShutDownUsingPowerInput;
    [Category("06. PC ShutDown Managment"), Description("Mettere a TRUE solo se la configurazione degli I/O della consolle di Syncro prevede già la struttura prevista negli I/O rev8.")]
    public bool EnableShutDownUsingPowerInput
    {
      get { return enableShutDownUsingPowerInput; }
      set { enableShutDownUsingPowerInput = value; }
    }
    
    //PMASSARO_FI16/12/09 - New ShutDown Managment
    int timeToWaitForPowerOFF;
    [Category("07. PC ShutDown Managment"), Description("Se il parametro EnableShutdownUsingPowerInput è 'TRUE', Impostare il tempo di attesa prima di iniziare la procedura di ShoutDown da quando viene spenta la consolle di Syncro ( default '30000' ).")]
    public int TimeToWaitForPowerOFF
    {
      get { return timeToWaitForPowerOFF; }
      set { timeToWaitForPowerOFF = value; }
    }

    //PMASSARO_FI10/02/09 - Try To Restart Failed Task
    bool enableAutomaticTasksRestart;
    [Category("08. Automatic Tasks Restart"), Description("Lasciare come di default impostato se non per diverse indicazioni da UTP.")]
    public bool EnableAutomaticTasksRestart
    {
      get { return enableAutomaticTasksRestart; }
      set { enableAutomaticTasksRestart = value; }
    }

		//RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
		string externalBackupHDIp;
    [Category("09. Backup HD"), Description("Indirizzo IP del disco di rete ( vedi KPCSYC ). '127.0.0.1' indica che l'HD non è presente.")]
		public string ExternalBackupHDIp
		{
			get { return externalBackupHDIp; }
			set { externalBackupHDIp = value; }
		}

    //RFAZZI_FI19/11/2010_1. Aggiunta gestione per differenziazione password di alto livello fra "installazione" e "assistenza"
    bool passwordInstallationEnabled;
    [Category("10. Password Installation Enabled"), Description("TRUE To Enable the 'Installation' High Level Password.")]
    public bool PasswordInstallationEnabled
    {
      get { return passwordInstallationEnabled; }
      set { passwordInstallationEnabled = value; }
    }

    //ABELLO_FI13/01/2011_1 Aggiunta gestione TextToSpeech
    bool tEXT_TO_SPEECH_USED;
    [Category("11. Text To Speech"), Description("Se TRUE Syncro parlerà con l'operatore.")]
    public bool TEXT_TO_SPEECH_USED
    {
      get { return tEXT_TO_SPEECH_USED; }
      set { tEXT_TO_SPEECH_USED = value; }
    }

    string tEXT_TO_SPEECH_LANGUAGE;
    [Category("11. Text To Speech"), TypeConverter(typeof(TextToSpeechLanguage)), Description("La lingua da usare.")]
    public string TEXT_TO_SPEECH_LANGUAGE
    {
      get { return tEXT_TO_SPEECH_LANGUAGE; }
      set { tEXT_TO_SPEECH_LANGUAGE = value; }
    }

    int tEXT_TO_SPEECH_VOLUME;
    [Category("11. Text To Speech"), TypeConverter(typeof(TextToSpeechLanguage)), Description("Volume da utilizzare.")]
    public int TEXT_TO_SPEECH_VOLUME
    {
      get { return tEXT_TO_SPEECH_VOLUME; }
      set { tEXT_TO_SPEECH_VOLUME = value; }
    }

    //PMASSARO_FI10/06/11 - Aggiornamento della cartella 'Update'
    //EnableAutomaticCopyInUpdateFolder;
    bool enableAutomaticCopyInUpdateFolder;
    [Category("12. Copy Files In UPDATE Folder"), Description("Mettere a TRUE se si desidera che STARTER esegua a tempo l'aggiornamento della cartella UPDATE")]
    public bool EnableAutomaticCopyInUpdateFolder
    {
      get { return enableAutomaticCopyInUpdateFolder; }
      set { enableAutomaticCopyInUpdateFolder = value; }
    }

    public GeneralSettings() : base()
    {
			this.sectionName = "GENERAL";

			this.wetEndControl = true;
			this.dryEndControl = true;
			this.job = string.Empty;
			this.comm = string.Empty;
			this.dspUserTask = string.Empty;
      //agrassi_FI19-03-08
      this.logDiskUTF8 = true;
      //agrassi_FI20-01-09
      this.maxOfficeClients = 1;
      this.sQL_USED = false;
      this.sQL_USED_FOR_DATABASE_MANAGEMENT = false;
      this.sQL_SERVER_IP = "127.0.0.1";
      this.sQL_USER = "root";
      this.sQL_PASSWORD = "root";
      this.sQL_DATABASE = "syncro_db";
      //agrassi_FI05-03-09_3
      //agrassi_FI19-08-2010_1
      //this.softwareSTD = "a";
      
      //PMASSARO_FI16/12/09 - New ShutDown Managment
      this.enableShutDownUsingPowerInput = false;
      this.timeToWaitForPowerOFF = 30000;
      //PMASSARO_FI10/02/09 - Try To Restart Failed Task
      this.enableAutomaticTasksRestart = false;
			//RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
			this.externalBackupHDIp = "127.0.0.1";
      //RFAZZI_FI19/11/2010_1. Aggiunta gestione per differenziazione password di alto livello fra "installazione" e "assistenza"
      this.passwordInstallationEnabled = false;

      this.tEXT_TO_SPEECH_USED = false;
      this.tEXT_TO_SPEECH_LANGUAGE = "Italian";
      this.tEXT_TO_SPEECH_VOLUME = 90;

      //PMASSARO_FI10/06/11 - Aggiornamento della cartella 'Update'
      this.enableAutomaticCopyInUpdateFolder = false;
		}
	}
}
