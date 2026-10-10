using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for SPEED Reference
	/// </summary>
	/// 
	[TypeConverter(typeof(Converters))]
	public class ReferenceType : SyncroC.DataBase.LineConfig.LineBase
	{
		private int	maxSpeedUVmm_m;					

		private double	factor;
		private double pullrollFactor;

		public ReferenceType( int offset ):base()
		{
			this.sectionName = "ReferenceType_" + offset.ToString();

			this.maxSpeedUVmm_m = 300000;
			this.factor = 1.0;
			this.pullrollFactor = 1.0;
		}

    [Description("Massima velocità (mm / min) a cui viene scalato il riferimento di velocità ( Max Speed = 10 Vdc ) ")]
		public int MaxSpeedUVmm_m
		{
			get { return this.maxSpeedUVmm_m ; }
			set { this.maxSpeedUVmm_m = value; }
		}

    [Description("Fattore moltiplicativo del riferimento analogico di velocità (0-10V). (e.g. Ingresso 5V, Factor 1.00, Output 5V)")]
		public double ReferenceFactor
		{
			get { return this.factor; }
			set { this.factor = value; }
		}

    [Description("Fattore moltiplicativo applicato al riferimento usando i pullrolls. Può essere usato per dare un fattore di sovravelocità per il Tagliacordona e Taglierina Trasversale. QUESTO PARAMETRO DEVE ESSERE SEMPRE LASCIATO UGUALE A 1.0")]
		public double PullRollFactor
		{
			get { return this.pullrollFactor; }
			set { this.pullrollFactor = value; }
		}
	
	};

	[TypeConverter(typeof(Converters))]
	public class ASRDryEndType : SyncroC.DataBase.LineConfig.LineBase
	{
		public const int MAX_ASR_DRYEND_REF_NUM = 10;

		private	ReferenceType[] reference;

		public ASRDryEndType(): base()
		{
			this.sectionName = "ASRDryEndType";
			this.reference = new ReferenceType[MAX_ASR_DRYEND_REF_NUM];

			for (int i = 0; i < MAX_ASR_DRYEND_REF_NUM; i++)
			{
				this.reference[i] = new ReferenceType( i + ASRExitBeltType.MAX_ASR_EXIT_BELT_REF_NUM + ASRWetEndType.MAX_ASR_WETEND_REF_NUM );
			}
		}

		[Description("Dry End references. 2=SlitterScorer, 5=Knife, 7=Stacker"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public ReferenceType[] Reference
		{
			get { return this.reference; }
			set { this.reference = value; }
		}
	};

	[TypeConverter(typeof(Converters))]
	public class ASRWetEndType : SyncroC.DataBase.LineConfig.LineBase
	{
		public static int MAX_ASR_WETEND_REF_NUM = 7;

		private	ReferenceType[] reference;

		public ASRWetEndType(): base()
		{
			this.sectionName = "ASRWetEndType";
			this.reference = new ReferenceType[MAX_ASR_WETEND_REF_NUM];

			for (int i = 0; i < MAX_ASR_WETEND_REF_NUM; i++)
			{
				this.reference[i] = new ReferenceType( i + ASRExitBeltType.MAX_ASR_EXIT_BELT_REF_NUM );
			}
		}

    [Description("Wet End references. 0=Rotary, 1=GlueUnit, 2=Tern, 3=Splicer1, 4=Tern (opt), 5=SF1, 6=SF2"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public ReferenceType[] Reference
		{
			get { return this.reference; }
			set { this.reference = value; }
		}
	};

	[TypeConverter(typeof(Converters))]
	public class ExitBeltDataType : SyncroC.DataBase.LineConfig.LineBase
	{
		private int lengthUDmm;
		private int gapShortSheetsUDmm;
		private int gapLongSheetsUDmm;
		private int pullrollOverspeedUVm_min;

		public ExitBeltDataType(int offset): base()
		{
			this.sectionName = "ExitBeltData_" + offset.ToString();

			this.lengthUDmm = 0;
			this.gapShortSheetsUDmm = 0;
			this.gapLongSheetsUDmm = 0;
			this.pullrollOverspeedUVm_min = 0;
		}

    [Category("ExitBeltData"), Description("Lunghezza sandwich in mm.")]
		public int ExitBeltLengthUDmm
		{
			get { return this.lengthUDmm; }
			set { this.lengthUDmm = value; }
		}

    [Category("ExitBeltData"), Description("Gap (in mm) richiesto fra i fogli quando la lunghezza di taglio è inferiore alla dimensione del sandwich.")]
		public int ExitBeltGapShortSheetsLengthUDmm
		{
			get { return this.gapShortSheetsUDmm; }
			set { this.gapShortSheetsUDmm = value; }
		}

    [Category("ExitBeltData"), Description("Gap (in mm) richiesto fra i fogli quando la lunghezza di taglio è superiore alla dimensione del sandwich.")]
		public int ExitBeltGapLongSheetsLengthUDmm
		{
			get { return this.gapLongSheetsUDmm; }
			set { this.gapLongSheetsUDmm = value; }
		}

    [Category("ExitBeltData"), Description("Sovravelocità dei sandwich in mt/min.")]
		public int ExitBeltPullrollOverspeedUVm_min
		{
			get { return this.pullrollOverspeedUVm_min; }
			set { this.pullrollOverspeedUVm_min = value; }
		}
	};



	[TypeConverter(typeof(Converters))]
	public class ASRExitBeltType : SyncroC.DataBase.LineConfig.LineBase
	{
		public static int MAX_ASR_EXIT_BELT_REF_NUM = 3;

		private	ReferenceType[] reference;
		private ExitBeltDataType[] exitBeltData;

		public ASRExitBeltType(): base()
		{
			this.sectionName = "ASRExitBeltType";
			this.reference = new ReferenceType[MAX_ASR_EXIT_BELT_REF_NUM];
			this.exitBeltData = new ExitBeltDataType[MAX_ASR_EXIT_BELT_REF_NUM];

			for (int i = 0; i < MAX_ASR_EXIT_BELT_REF_NUM; i++)
			{
				this.reference[i] = new ReferenceType( i );
				this.exitBeltData[i] = new ExitBeltDataType(i);
			}
		}

		[Description("Reference"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public ReferenceType[] Reference
		{
			get { return this.reference; }
			set { this.reference = value; }
		}

		[Description("Exit Belt Data"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public ExitBeltDataType[] ExitBeltData
		{
			get { return this.exitBeltData; }
			set { this.exitBeltData = value; }
		}	
	};

  [TypeConverter(typeof(Converters))]
  public class SPEEDReference : SyncroC.DataBase.LineConfig.LineBase
	{
		private	ASRExitBeltType asrExitBelt;
		private ASRDryEndType asrDryEndRef;
		private ASRWetEndType asrWetEndRef;
		
		public SPEEDReference(): base()
		{
			this.sectionName = "SPEEDReference";
			this.asrDryEndRef = new ASRDryEndType();
			this.asrWetEndRef = new ASRWetEndType();
			this.asrExitBelt = new ASRExitBeltType();
		}

		[Category("1. DRY END SPEED REFERENCES"), Description("Dry end speed references")]
		public ASRDryEndType ASR_DryEndRef
		{
			get { return this.asrDryEndRef; }
			set { this.asrDryEndRef = value; }
		}

		[Category("3. WET END SPEED REFERENCES"), Description("Wet end speed references")]
		public ASRWetEndType ASR_WetEndRef
		{
			get { return this.asrWetEndRef; }
			set { this.asrWetEndRef = value; }
		}

		[Category("2. EXIT BELT SPEED REFERENCES"), Description("Exit belt speed references")]
		public ASRExitBeltType ASR_ExitBeltRef
		{
			get { return this.asrExitBelt; }
			set { this.asrExitBelt = value; }
		}
	}
}