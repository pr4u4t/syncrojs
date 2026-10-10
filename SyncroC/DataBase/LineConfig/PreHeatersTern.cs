using System;
using System.ComponentModel;
using System.Globalization;
using SyncroC.Utility;

//ABELLO_FI19/08/2010_1 La nostra Terna NON ha la gestione del Locale/Remote per preriscaldatore quindi i preriscaldatori vanno messi in manuale se sono in remoto.

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for SingleFacer.
	/// </summary>
	public class PreHeatersTern : SyncroC.DataBase.LineConfig.LineBase
	{
    bool preHeaterManualIfRemote;
    [PropertyOrder(0), Category("PreHeatersTern"), Description("Our PreHeatersTern Vers.1 has no management LOCAL/REMOTE for preheater. Preheaters then be put in MANUAL if they are REMOTE.")]
    public bool PreHeaterManualIfRemote
    {
      get { return preHeaterManualIfRemote; }
      set { preHeaterManualIfRemote = value; }
    }

		WrapArm preheater11;
		[PropertyOrder(10), Category("PreHeatersTern"), Description("Preheater11")]
		public WrapArm Preheater11
		{
			get { return preheater11; }
			set { preheater11 = value; }
		}

		WrapArm preheater1;
		[PropertyOrder(20), Category("PreHeatersTern"), Description("Preheater1")]
		public WrapArm Preheater1
		{
			get { return preheater1; }
			set { preheater1 = value; }
		}

		WrapArm preheater2;
		[PropertyOrder(30), Category("PreHeatersTern"), Description("Preheater2")]
		public WrapArm Preheater2
		{
			get { return preheater2; }
			set { preheater2 = value; }
		}

		WrapArm preheater3;
		[PropertyOrder(40), Category("PreHeatersTern"), Description("Preheater3")]
		public WrapArm Preheater3
		{
			get { return preheater3; }
			set { preheater3 = value; }
		}

		WrapArm preheater61;
		[PropertyOrder(50), Category("PreHeatersTern"), Description("Preheater61")]
		public WrapArm Preheater61
		{
			get { return preheater61; }
			set { preheater61 = value; }
		}

		WrapArm preheater10;
		[PropertyOrder(60), Category("PreHeatersTern"), Description("Preheater10")]
		public WrapArm Preheater10
		{
			get { return preheater10; }
			set { preheater10 = value; }
		}

		WrapArm preheater20;
		[PropertyOrder(70), Category("PreHeatersTern"), Description("Preheater20")]
		public WrapArm Preheater20
		{
			get { return preheater20; }
			set { preheater20 = value; }
		}

		WrapArm preheater30;
		[PropertyOrder(80), Category("PreHeatersTern"), Description("Preheater30")]
		public WrapArm Preheater30
		{
			get { return preheater30; }
			set { preheater30 = value; }
		}

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    WrapArmSteam wrapArmSteam11;
    [PropertyOrder(100), Category("WrapArm Steam"), Description("WrapArm Steam 11")]
    public WrapArmSteam WrapArmSteam11
    {
      get { return wrapArmSteam11; }
      set { wrapArmSteam11 = value; }
    }

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    WrapArmSteam wrapArmSteam1;
    [PropertyOrder(110), Category("WrapArm Steam"), Description("WrapArm Steam 1")]
    public WrapArmSteam WrapArmSteam1
    {
      get { return wrapArmSteam1; }
      set { wrapArmSteam1 = value; }
    }

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    WrapArmSteam wrapArmSteam10;
    [PropertyOrder(120), Category("WrapArm Steam"), Description("WrapArm Steam 10")]
    public WrapArmSteam WrapArmSteam10
    {
      get { return wrapArmSteam10; }
      set { wrapArmSteam10 = value; }
    }

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    WrapArmSteam wrapArmSteam2;
    [PropertyOrder(130), Category("WrapArm Steam"), Description("WrapArm Steam 2")]
    public WrapArmSteam WrapArmSteam2
    {
      get { return wrapArmSteam2; }
      set { wrapArmSteam2 = value; }
    }

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    WrapArmSteam wrapArmSteam20;
    [PropertyOrder(140), Category("WrapArm Steam"), Description("WrapArm Steam 20")]
    public WrapArmSteam WrapArmSteam20
    {
      get { return wrapArmSteam20; }
      set { wrapArmSteam20 = value; }
    }

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    WrapArmSteam wrapArmSteam3;
    [PropertyOrder(150), Category("WrapArm Steam"), Description("WrapArm Steam 3")]
    public WrapArmSteam WrapArmSteam3
    {
      get { return wrapArmSteam3; }
      set { wrapArmSteam3 = value; }
    }

		public PreHeatersTern()
			: base()
		{
			sectionName = "Pre-HeatersTern";

      this.preHeaterManualIfRemote = false;

			this.preheater11 = new WrapArm (11);
			this.preheater1 = new WrapArm(1);
			this.preheater2 = new WrapArm(2);
			this.preheater3 = new WrapArm(3);
			this.preheater61 = new WrapArm(61);
			this.preheater10 = new WrapArm(10);
			this.preheater20 = new WrapArm(20);
			this.preheater30 = new WrapArm(30);

      //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
      this.wrapArmSteam11 = new WrapArmSteam(11);
      this.wrapArmSteam1 = new WrapArmSteam(1);
      this.wrapArmSteam10 = new WrapArmSteam(10);
      this.wrapArmSteam2 = new WrapArmSteam(2);
      this.wrapArmSteam20 = new WrapArmSteam(20);
      this.wrapArmSteam3 = new WrapArmSteam(3);
    }
	}
}