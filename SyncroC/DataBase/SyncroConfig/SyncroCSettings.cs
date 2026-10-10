using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
	[TypeConverter(typeof(Converters))]
	public class SyncroCParameter : SyncroBase
	{

		private string clientIP = null;
    [Description("Indirizzo IP del PC su cui viene lanciato il client grafico. Per il PC server di Syncro, deve essere impostato '127.0.0.1'")]
		public string ClientIP
		{
			get { return this.clientIP; }
			set { this.clientIP = value; }
		}

		private string firstPage = null;
    [Description("Riporta il nome della pagina da visualizzare all'invio del client. Tipicamente si imposta 'production_homeWET' oppure 'production_homeDRY' oppure 'production_homePRC' oppure 'twin400_console'.")]
		public string FirstPage
		{
			get { return this.firstPage; }
			set { this.firstPage = value; }
		}

		private bool  corrugatorDirectionRight;
    [Description("se TRUE il client rappresenta la linea con i Single Facer a SX e lo Stacker a DX.")]
		public bool  CorrugatorDirectionRight
		{
			get { return this.corrugatorDirectionRight; }
			set { this.corrugatorDirectionRight = value; }
		}

		//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
		private bool machineDirectionVisualizationInverted;
    [Description("se TRUE il client inverte la disposizione della pagina grafica considerando di avere i piani caldi davanti a se anziché alle sue spalle. Questo parametro è richiesto per il client a bordo macchina del Twin400.")]
		public bool MachineDirectionVisualizationInverted
		{
			get { return this.machineDirectionVisualizationInverted; }
			set { this.machineDirectionVisualizationInverted = value; }
		}

		private bool  dryEndControl;
    [Description("se TRUE il client è abilitato alla visualizzazione delle pagine e barra di stato DryEnd.")]
		public bool  DryEndControl
		{
			get { return this.dryEndControl; }
			set { this.dryEndControl = value; }
		}

		private bool  wetEndControl;
    [Description("se TRUE il client è abilitato alla visualizzazione delle pagine e barra di stato WetEnd.")]
		public bool  WetEndControl
		{
			get { return this.wetEndControl; }
			set { this.wetEndControl = value; }
		}

		private bool  pmac400;
    [Description("se TRUE il client è abilitato alla visualizzazione delle pagine del Twin400.")]
		public bool  PMac400
		{
			get { return this.pmac400; }
			set { this.pmac400 = value; }
		}

		private bool  editSetupQueue;
    [Description("se TRUE il client è abilitato alla modifica degli ordini da produrre.")]
		public bool  EditSetupQueue
		{
			get { return this.editSetupQueue; }
			set { this.editSetupQueue = value; }
		}

		private bool  changeAOCMode;
    [Description("se TRUE il client è abilitato a modificare la modalità di AOC ( Manual/Semi/Auto ).")]
		public bool  ChangeAOCMode
		{
			get { return this.changeAOCMode; }
			set { this.changeAOCMode = value; }
		}

		private bool  editOperatorSettings;
    [Description("se TRUE il client è abilitato alla modifica dei parametri presenti nelle pagine ingegneristiche.")]
		public bool  EditOperatorSettings
		{
			get { return this.editOperatorSettings; }
			set { this.editOperatorSettings = value; }
		}

		private bool  assignDowntimes;
    [Description("se TRUE il client è abilitato all'inserimento delle causali di fermata.")]
		public bool  AssignDowntimes
		{
			get { return this.assignDowntimes; }
			set { this.assignDowntimes = value; }
		}

		private bool  changeShift;
    [Description("se TRUE il client è abilitato ad eseguire la procedura di Cambio Turno.")]
		public bool  ChangeShift
		{
			get { return this.changeShift; }
			set { this.changeShift = value; }
		}

		private bool  changeWETAutoManMode;
    [Description("se TRUE il client è abilitato a modificare lo stato del controllo WetEnd ( Auto/Man ) sia a livello generale che di zona.")]
		public bool  ChangeWETAutoManMode
		{
			get { return this.changeWETAutoManMode; }
			set { this.changeWETAutoManMode = value; }
		}

		private bool  lowLevelEnabled;
    [Description("se TRUE il client è abilitato alla remotazione dei comandi presenti per le varie macchine.")]
		public bool  LowLevelEnabled
		{
			get { return this.lowLevelEnabled; }
			set { this.lowLevelEnabled = value; }
		}

		//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale. Parametro nn più utilizzato
		//private int clientID;
		//public int ClientID
		//{
		//  get { return this.clientID; }
		//  set { this.clientID = value; }
		//}

		private bool  manualOrdersSending;
    [Description("se TRUE il client è abilitato all'invio manuale degli ordini alle macchine.")]
		public bool  ManualOrdersSending
		{
			get { return this.manualOrdersSending; }
			set { this.manualOrdersSending = value; }
		}

		private bool  prc;
    [Description("se TRUE il client è abilitato alla visualizzazione delle pagine relative al Process.")]
		public bool  PRC
		{
			get { return this.prc; }
			set { this.prc = value; }
		}

		public SyncroCParameter(string sectionName)
			: base()
		{
			this.sectionName = sectionName;
			this.clientIP = "127.0.0.1";
			this.firstPage ="production_homeDRY";
			this.corrugatorDirectionRight=true;
			this.dryEndControl=true;
			this.wetEndControl=false;
			this.pmac400=false;
			this.editSetupQueue=true;
			this.changeAOCMode=true;
			this.editOperatorSettings=true;
			this.assignDowntimes=true;
			this.changeShift = true;
			this.changeWETAutoManMode=true;
			this.lowLevelEnabled=true;
			//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale. Parametro nn più utilizzato
			//this.clientID = 1;
			this.manualOrdersSending=true;
			this.prc =false;
			//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
			this.machineDirectionVisualizationInverted = false;
		}

	}

	public class SyncroCSettings : SyncroBase
  {
		SyncroCParameter syncroC1 = null;
		SyncroCParameter syncroC2 = null;
		SyncroCParameter syncroC3 = null;
		SyncroCParameter syncroC4 = null;
		SyncroCParameter syncroC5 = null;
		SyncroCParameter syncroC6 = null;
		SyncroCParameter syncroC7 = null;
		SyncroCParameter syncroC8 = null;
		SyncroCParameter syncroC9 = null;
		SyncroCParameter syncroC10 = null;
		SyncroCParameter syncroC11 = null;
		SyncroCParameter syncroC12 = null;
		SyncroCParameter syncroC13 = null;
		SyncroCParameter syncroC14 = null;
		SyncroCParameter syncroC15 = null;
		SyncroCParameter syncroC16 = null;
		SyncroCParameter syncroC17 = null;
		SyncroCParameter syncroC18 = null;
		SyncroCParameter syncroC19 = null;
		SyncroCParameter syncroC20 = null;

		[Category("01. SyncroC Nr 01 Settings"), Description("Syncro Client Nr 1 Settings")]
		public SyncroCParameter SyncroC1
		{
			get { return syncroC1; }
			set { syncroC1 = value; }
		}

		[Category("02. SyncroC Nr 02 Settings"), Description("Syncro Client Nr 2 Settings")]
		public SyncroCParameter SyncroC2
		{
			get { return syncroC2; }
			set { syncroC2 = value; }
		}

		[Category("03. SyncroC Nr 03 Settings"), Description("Syncro Client Nr 3 Settings")]
		public SyncroCParameter SyncroC3
		{
			get { return syncroC3; }
			set { syncroC3 = value; }
		}

		[Category("04. SyncroC Nr 04 Settings"), Description("Syncro Client Nr 4 Settings")]
		public SyncroCParameter SyncroC4
		{
			get { return syncroC4; }
			set { syncroC4 = value; }
		}

		[Category("05. SyncroC Nr 05 Settings"), Description("Syncro Client Nr 5 Settings")]
		public SyncroCParameter SyncroC5
		{
			get { return syncroC5; }
			set { syncroC5 = value; }
		}

		[Category("06. SyncroC Nr 06 Settings"), Description("Syncro Client Nr 6 Settings")]
		public SyncroCParameter SyncroC6
		{
			get { return syncroC6; }
			set { syncroC6 = value; }
		}

		[Category("07. SyncroC Nr 07 Settings"), Description("Syncro Client Nr 7 Settings")]
		public SyncroCParameter SyncroC7
		{
			get { return syncroC7; }
			set { syncroC7 = value; }
		}

		[Category("08. SyncroC Nr 08 Settings"), Description("Syncro Client Nr 8 Settings")]
		public SyncroCParameter SyncroC8
		{
			get { return syncroC8; }
			set { syncroC8 = value; }
		}

		[Category("09. SyncroC Nr 09 Settings"), Description("Syncro Client Nr 9 Settings")]
		public SyncroCParameter SyncroC9
		{
			get { return syncroC9; }
			set { syncroC9 = value; }
		}

		[Category("10. SyncroC Nr 10 Settings"), Description("Syncro Client Nr 10 Settings")]
		public SyncroCParameter SyncroC10
		{
			get { return syncroC10; }
			set { syncroC10 = value; }
		}

		[Category("11. SyncroC Nr 11 Settings"), Description("Syncro Client Nr 11 Settings")]
		public SyncroCParameter SyncroC11
		{
			get { return syncroC11; }
			set { syncroC11 = value; }
		}

		[Category("12. SyncroC Nr 12 Settings"), Description("Syncro Client Nr 12 Settings")]
		public SyncroCParameter SyncroC12
		{
			get { return syncroC12; }
			set { syncroC12 = value; }
		}

		[Category("13. SyncroC Nr 13 Settings"), Description("Syncro Client Nr 13 Settings")]
		public SyncroCParameter SyncroC13
		{
			get { return syncroC13; }
			set { syncroC13 = value; }
		}

		[Category("14. SyncroC Nr 14 Settings"), Description("Syncro Client Nr 14 Settings")]
		public SyncroCParameter SyncroC14
		{
			get { return syncroC14; }
			set { syncroC14 = value; }
		}

		[Category("15. SyncroC Nr 15 Settings"), Description("Syncro Client Nr 15 Settings")]
		public SyncroCParameter SyncroC15
		{
			get { return syncroC15; }
			set { syncroC15 = value; }
		}

		[Category("16. SyncroC Nr 16 Settings"), Description("Syncro Client Nr 16 Settings")]
		public SyncroCParameter SyncroC16
		{
			get { return syncroC16; }
			set { syncroC16 = value; }
		}

		[Category("17. SyncroC Nr 17 Settings"), Description("Syncro Client Nr 17 Settings")]
		public SyncroCParameter SyncroC17
		{
			get { return syncroC17; }
			set { syncroC17 = value; }
		}

		[Category("18. SyncroC Nr 18 Settings"), Description("Syncro Client Nr 18 Settings")]
		public SyncroCParameter SyncroC18
		{
			get { return syncroC18; }
			set { syncroC18 = value; }
		}

		[Category("19. SyncroC Nr 19 Settings"), Description("Syncro Client Nr 19 Settings")]
		public SyncroCParameter SyncroC19
		{
			get { return syncroC19; }
			set { syncroC19 = value; }
		}

		[Category("20. SyncroC Nr 20 Settings"), Description("Syncro Client Nr 20 Settings")]
		public SyncroCParameter SyncroC20
		{
			get { return syncroC20; }
			set { syncroC20 = value; }
		}
		
		public SyncroCSettings()
      : base()
    {
      this.sectionName = "SYNCRO_C" + this.sectionName;
			this.SyncroC1 = new SyncroCParameter("CLIENT_1");
			this.SyncroC2 = new SyncroCParameter("CLIENT_2");
			this.SyncroC3 = new SyncroCParameter("CLIENT_3");
			this.SyncroC4 = new SyncroCParameter("CLIENT_4");
			this.SyncroC5 = new SyncroCParameter("CLIENT_5");
			this.SyncroC6 = new SyncroCParameter("CLIENT_6");
			this.SyncroC7 = new SyncroCParameter("CLIENT_7");
			this.SyncroC8 = new SyncroCParameter("CLIENT_8");
			this.SyncroC9 = new SyncroCParameter("CLIENT_9");
			this.SyncroC10 = new SyncroCParameter("CLIENT_10");
			this.SyncroC11 = new SyncroCParameter("CLIENT_11");
			this.SyncroC12 = new SyncroCParameter("CLIENT_12");
			this.SyncroC13 = new SyncroCParameter("CLIENT_13");
			this.SyncroC14 = new SyncroCParameter("CLIENT_14");
			this.SyncroC15 = new SyncroCParameter("CLIENT_15");
			this.SyncroC16 = new SyncroCParameter("CLIENT_16");
			this.SyncroC17 = new SyncroCParameter("CLIENT_17");
			this.SyncroC18 = new SyncroCParameter("CLIENT_18");
			this.SyncroC19 = new SyncroCParameter("CLIENT_19");
			this.SyncroC20 = new SyncroCParameter("CLIENT_20");
		}
  }
}
