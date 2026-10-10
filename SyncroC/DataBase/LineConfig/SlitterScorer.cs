using System;
using System.Reflection;
using System.Drawing.Design;
using System.ComponentModel;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	enum Shaft: int
	{
		NoShaft=-1,
		Slit1=0,
		Slit2=1,
		Score1=2,
		Score2=3,
		ScoreAux=4
	}

  public enum ParityPriorityType: int
  {
    LowParityPriority = 0,
    MidParityPriority = 1,
    HighParityPriority = 2
  };

  public enum ShaftGroup : int 
  { 
    NoGroup=0,
    Group1=1,
    Group2=2,
    Group1A=3,
    Group2A=4,
    BothGroups=5
  };

  public enum MachineOperatorSide: int 
  {
    LeftSide = 0,
    RightSide = 1
  };

  //agrassi_FI12-02-09
  public enum Group: int
  { 
    Group1=0,
    Group2=1
  };

	[Description("Slit types"), TypeConverter(typeof(Converters))]
	public class SlitType: SyncroC.DataBase.LineConfig.LineBase
	{
		private char letter;					//identification character
		private int width;						//slit's width
		private bool anvilPresent;		//presence of anvil over the blade
		private int bladeFromLeft;		//distance between the blade and the left edge
    int handleWidth;			        //the width of the handle (the blade is in the middle of it for the Twin Ev, not for the Twin 400)
    int handleFromLeft;		        //distance between the left edge of the chamfer and the left edge (meaningful for Twin 400)
    int rightGrabIndentation;		  //distance of indentation for external right grab from chamfer center(only for the last slit of the Twin 400)
    private bool withChamfer;     //if true (like for the Twin400) there is no handle to grab the slit, but a chamfer that the finger has to enter
    private int maxBladeDiameter;
		private int minBladeDiameter;
		private int supportDiameter;


		public SlitType():base()
		{
			this.sectionName="SS_SLIT_TYPE_";
			
			this.letter=' ';
			this.width=0;
			this.anvilPresent=false;
			this.bladeFromLeft=0;
			this.handleWidth=0;
      this.handleFromLeft = 0;
      this.rightGrabIndentation = 0;
      this.withChamfer = false;
      this.maxBladeDiameter=0;
			this.minBladeDiameter=0;
			this.supportDiameter=0;
		}

    [ReadOnly(true), PropertyOrder(10), Category("SlitType"), Description("Identification character: K is shaft 1 slit type, M is shaft 2 slit type, L and N are slits with anvils.")]
		public char Letter
		{
			get{return this.letter;}
			set{this.letter=value;}
		}

    [ReadOnly(true), PropertyOrder(20), Category("SlitType"), Description("Slit's width in mm/10")]
		public int Width
		{
			get{return this.width;}
			set{this.width=value;}
		}

    [ReadOnly(true), PropertyOrder(30), Category("SlitType"), Description("Presence of anvil over the blade")]
		public bool AnvilPresent
		{
			get{return this.anvilPresent;}
			set{this.anvilPresent=value;}
		}

    [ReadOnly(true), PropertyOrder(40), Category("SlitType"), Description("The distance between the blade and the left edge in mm/10")]
		public int BladeFromLeft
		{
			get{return this.bladeFromLeft;}
			set{this.bladeFromLeft=value;}
		}

    //agrassi_FI07-04-08_9
    //[ReadOnly(true), PropertyOrder(50), Category("SlitType"), Description("The width of the handle or chamfer in mm/10 (the blade is in the middle of it for the Twin Ev, not for the Twin 400)")]
    [ReadOnly(false), PropertyOrder(50), Category("SlitType"), Description("The width of the handle or chamfer in mm/10 (the blade is in the middle of it for the Twin Ev, not for the Twin 400)")]
    public int HandleWidth
		{
			get{return this.handleWidth;}
			set{this.handleWidth=value;}
		}

    //agrassi_FI07-04-08_9
    //[ReadOnly(true), PropertyOrder(52), Category("SlitType"), Description("Distance between the left edge of the chamfer and the left edge in mm/10 (meaningful for Twin 400)")]
    [ReadOnly(false), PropertyOrder(52), Category("SlitType"), Description("Distance between the left edge of the chamfer and the left edge in mm/10 (meaningful for Twin 400)")]
    public int HandleFromLeft
    {
      get { return this.handleFromLeft; }
      set { this.handleFromLeft = value; }
    }

    [ReadOnly(true), PropertyOrder(53), Category("SlitType"), Description("Distance of indentation for external right grab from chamfer center in mm/10 (only for the last slit of the Twin 400)")]
    public int RightGrabIndentation
    {
      get { return this.rightGrabIndentation; }
      set { this.rightGrabIndentation = value; }
    }

    [ReadOnly(true), PropertyOrder(55), Category("SlitType"), Description("If true (like for the Twin400) there is no handle to grab the slit, but a chamfer that the finger has to enter")]
    public bool WithChamfer
    {
      get { return this.withChamfer; }
      set { this.withChamfer = value; }
    }

    [ReadOnly(true), PropertyOrder(60), Category("SlitType"), Description("The maximum diameter of the blade in mm/10")]
    public int MaxBladeDiameter
		{
			get{return this.maxBladeDiameter;}
			set{this.maxBladeDiameter=value;}
		}

    [ReadOnly(true), PropertyOrder(70), Category("SlitType"), Description("The minimum diameter of the blade in mm/10")]
    public int MinBladeDiameter
		{
			get{return this.minBladeDiameter;}
			set{this.minBladeDiameter=value;}
		}

    [ReadOnly(true), PropertyOrder(80), Category("SlitType"), Description("The slit support diameter in mm/10")]
    public int SupportDiameter
		{
			get{return this.supportDiameter;}
			set{this.supportDiameter=value;}
		}
	}


	[Description("Score types"), TypeConverter(typeof(Converters))]
	public class ScoreType: SyncroC.DataBase.LineConfig.LineBase
	{
		private const int NUM_POSIT_TYPES=6;

		char letter;															//identification letter
		ScorerProfileType scorer;									//distance between the far valley and the center axis (for the Linpac scores)
		ScoreGapData[] gapData;										//scoreGapOffset, minScoreGap

		public ScoreType():base()
		{
			this.sectionName="SS_SCORE_TYPE_";
			
			this.letter=' ';
			this.scorer=new ScorerProfileType();
			this.gapData=new ScoreGapData[NUM_POSIT_TYPES];
			for (int i=0; i<NUM_POSIT_TYPES; i++)
				this.gapData[i]=new ScoreGapData(scorer.SClass);
		}
		
		[PropertyOrder(10), Category("ScoreType"), Description("Identification character")]
		public char Letter
		{
			get{return ((this.letter==0)?' ':this.letter);}
			set
      {
        this.letter=value;
        if (this.letter == 0)
          this.letter = ' ';
      }
    }

		[PropertyOrder(20), ReadOnly(false), Category("ScoreType"), Description("Scorer profile data")]
		public ScorerProfileType Scorer
		{
			get{return this.scorer;}
			set{this.scorer=value;}
		}

    [PropertyOrder(30), Category("ScoreType"), Description("Score gap data for each positioning type available on this scorer"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public ScoreGapData[] GapData
		{
			get{return this.gapData;}
			set{this.gapData=value;}
		}
	}

	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
	[Description("Score gap data"), TypeConverter(typeof(Converters))]
	public class ScoreGapData: SyncroC.DataBase.LineConfig.LineBase
	{
		public ScoreClass sClass;
		PositType positioning;
		int scoreGapOffset;		//offset to achieve the same board crush between different positioning types and between different score types
		int[] minScoreGap;//minimum score gap to avoid collision between the upper and lower profiles

		public ScoreGapData(ScoreClass sClass):base()
		{
			this.sectionName="SS_SCORE_TYPE_";
			this.sClass=sClass;

			this.positioning=PositType.NoPositType;
      this.minScoreGap = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      for (int sh=0; sh<SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
			  this.minScoreGap[sh]=0; 
			this.scoreGapOffset=0;
		}
		
		[PropertyOrder(5), ReadOnly(true), Category("ScoreGapData"), Description("Score positioning type")]
		public PositType Positioning
		{
			get{return this.positioning;}
			set{this.positioning=value;}
		}

    [PropertyOrder(10), Category("ScoreGapData"), Description("Score gap offset in mm/10")]
    public int ScoreGapOffset
		{
			get{return this.scoreGapOffset;}
			set{this.scoreGapOffset=value;}
		}

    [PropertyOrder(20), ReadOnly(true), Category("ScoreGapData"), Description("Minimum score angle in tenths of degree for each shaft: [0]=shaft 1, [1]=shaft 2, [2]=aux shaft"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] MinScoreGap
		{
			get{return this.minScoreGap;}
			set{this.minScoreGap=value;}
		}

		protected override void SurveyProperties(ref string s, int index, int parentIndex, int level, ConfigBaseOperation op)
		{
			int count=0;
			PositType p;
			for (p=PositType.MaleFemaleP; p<PositType.TearTape; p++)
				if (ScorerProfileType.IsPositioningAvailable(this.sClass, p))
					if (count++==index) break;;
			if (p>=PositType.TearTape) return;
			this.positioning=p;
			string section=this.sectionName;
			if (parentIndex>=0) section+=(parentIndex+1).ToString();
			PropertyInfo[] properties = this.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				string mydefault = null;
				object val = properties[i].GetValue(this, null);
				if ((val != null) && (!properties[i].Name.Equals("Positioning")))
				{
          string sInd=string.Empty;
  				bool isArray = properties[i].PropertyType.IsArray;
          int len=1;
          System.Array arrayVal = null;
          if (isArray)
					  len = ((System.Array)properties[i].GetValue(this, null)).Length;
				  for (int ind = 0; ind < len; ind++)
				  {
            val = properties[i].GetValue(this, null);
					  if (isArray)
					  {
						  val = ((System.Array)val).GetValue(ind);
						  sInd = "_" + (ind + 1).ToString();
					  }
            if (isArray && (arrayVal == null))
              arrayVal = this.GetArray(val.GetType(), len);
					  mydefault = this.ConvertToString(val, val.GetType());
					  string stringValue=string.Empty;
					  if (op==ConfigBaseOperation.PropertiesFromINI)
						  stringValue = this.GetFileIniString(section, properties[i].Name+"_"+p+sInd, mydefault);
					  else if (op==ConfigBaseOperation.PropertiesIntoString)
						  s+=properties[i].Name+"_"+p+sInd+"="+mydefault+Command.PARAMETER_SEPARATOR;
					  else if (op==ConfigBaseOperation.PropertiesIntoINI)
						  this.SetFileIniString(section, properties[i].Name+"_"+p+sInd, mydefault);
					  else if (op==ConfigBaseOperation.PropertiesFromString)
              stringValue = this.SearchStringForItem(s, section, properties[i].Name + "_" + p+sInd);
            else if ((op == ConfigBaseOperation.PropertiesToDocumentation) && (parentIndex == 0) && (index == 0))
              s += this.WriteDocumentationItem(properties[i], "");
            if ((op == ConfigBaseOperation.PropertiesFromINI) || (op == ConfigBaseOperation.PropertiesFromString))
					  {
						  if (!stringValue.Equals(string.Empty))
						  {
                if (isArray && arrayVal != null)
                  arrayVal.SetValue(Convert.ChangeType(stringValue, val.GetType()), ind);
                else
                {
                  object realValue = this.ConvertFromString(stringValue, val.GetType());
                  properties[i].SetValue(this, realValue, null);
                }
						  }
					  }
          }
          if ((op == ConfigBaseOperation.PropertiesFromINI) || (op == ConfigBaseOperation.PropertiesFromString))
            if (isArray && (arrayVal != null))
              properties[i].SetValue(this, arrayVal, null);
        }
      }
    }
	}

	[Description("Scorer profile"), TypeConverter(typeof(Converters))]
	public class ScorerProfileType: SyncroC.DataBase.LineConfig.LineBase
	{
		string scoreId;
		ScoreClass sClass;										//score class it belongs to
		int width_UDdmm;											//score's width
		int profileWidth_UDdmm;								//profile's width
		int tipHeight_UDdmm;									//tip's height from profile base (zero line)
		int valleyDepth_UDdmm;								//valley's depth from profile base (zero line)
		int tipOffset_UDdmm;									//distance between the tip and the center axis 
		int farValleyOffset_UDdmm;
		int upperDiameter_UDdmm;
		int lowerDiameter_UDdmm;

		public ScorerProfileType():base()
		{
			this.sectionName="SS_SCORE_TYPE_";
			this.Init();
		}

		public ScorerProfileType(string file, int ind):base()
		{
			this.fullPathIniFile=this.fullPathIniFile.Substring(0, this.fullPathIniFile.LastIndexOf("\\")+1)+file;
			this.sectionName="PROFILE_"+(ind+1);
			this.Init();
		}

		private void Init()
		{
			this.scoreId=" ";
			this.sClass=ScoreClass.NoClass;
			this.width_UDdmm=0;
			this.profileWidth_UDdmm=0;
			this.tipHeight_UDdmm=0;
			this.valleyDepth_UDdmm=0;
			this.tipOffset_UDdmm=0;
			this.farValleyOffset_UDdmm=0;
			this.upperDiameter_UDdmm=0;
			this.lowerDiameter_UDdmm=0;
		}

		[PropertyOrder(10), TypeConverter(typeof(ScorerProfileConverter)), Category("ScorerProfileType"), Description("Identification string")]
		public string ScoreId
		{
			get{return this.scoreId;}
			set{this.scoreId=value;}
		}

		[PropertyOrder(20), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile class")]
		public ScoreClass SClass
		{
			get{return this.sClass;}
			set{this.sClass=value;}
		}

    [PropertyOrder(30), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer width in mm/10")]
    public int Width
		{
			get{return this.width_UDdmm;}
			set{this.width_UDdmm=value;}
		}

    [PropertyOrder(40), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile width in mm/10")]
    public int ProfileWidth
		{
			get{return this.profileWidth_UDdmm;}
			set{this.profileWidth_UDdmm=value;}
		}

    [PropertyOrder(50), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile tip height in mm/10")]
    public int TipHeight
		{
			get{return this.tipHeight_UDdmm;}
			set{this.tipHeight_UDdmm=value;}
		}

    [PropertyOrder(60), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile valley depth in mm/10")]
    public int ValleyDepth
		{
			get{return this.valleyDepth_UDdmm;}
			set{this.valleyDepth_UDdmm=value;}
		}

    [PropertyOrder(70), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile tip offset in mm/10")]
    public int TipOffset
		{
			get{return this.tipOffset_UDdmm;}
			set{this.tipOffset_UDdmm=value;}
		}

    [PropertyOrder(80), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile far valley offset in mm/10")]
    public int FarValleyOffset
		{
			get{return this.farValleyOffset_UDdmm;}
			set{this.farValleyOffset_UDdmm=value;}
		}

    [PropertyOrder(90), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile upper diameter in mm/10")]
    public int UpperDiameter
		{
			get{return this.upperDiameter_UDdmm;}
			set{this.upperDiameter_UDdmm=value;}
		}

    [PropertyOrder(100), ReadOnly(true), Category("ScorerProfileType"), Description("Scorer profile lower diameter in mm/10")]
    public int LowerDiameter
		{
			get{return this.lowerDiameter_UDdmm;}
			set{this.lowerDiameter_UDdmm=value;}
		}

		protected override void SurveyProperties(ref string s, int index, int parentIndex, int level, ConfigBaseOperation op)
		{
			string section=this.sectionName;
			if (parentIndex>=0) section+=(parentIndex+1).ToString();
			PropertyInfo[] properties = this.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				object val = properties[i].GetValue(this, null);
				if (val != null)
				{
					string mydefault=string.Empty;
					if (properties[i].PropertyType.IsEnum)
					{
						if (op==ConfigBaseOperation.PropertiesIntoINI)
							mydefault =System.Enum.Format(properties[i].PropertyType, val, "g");
						else
							mydefault= Convert.ToInt32(val).ToString();
					}
					else
						mydefault = this.ConvertToString(val, val.GetType());
					string stringValue=string.Empty;
					if (op==ConfigBaseOperation.PropertiesFromINI)
						stringValue = this.GetFileIniString(section, properties[i].Name, mydefault);
					else if (op==ConfigBaseOperation.PropertiesIntoString)
						s+=properties[i].Name+"="+mydefault+Command.PARAMETER_SEPARATOR;
					else if (op==ConfigBaseOperation.PropertiesIntoINI)
						this.SetFileIniString(section, properties[i].Name, mydefault);
					else if (op==ConfigBaseOperation.PropertiesFromString)
						stringValue=this.SearchStringForItem(s, section, properties[i].Name);
					else if (op==ConfigBaseOperation.PropertiesFromStringAdd)
						stringValue=this.SearchStringForItem(s, null, properties[i].Name);
          else if ((op == ConfigBaseOperation.PropertiesToDocumentation) && (parentIndex == 0))
            s += this.WriteDocumentationItem(properties[i], "");
          if ((op == ConfigBaseOperation.PropertiesFromINI) || (op == ConfigBaseOperation.PropertiesFromString)
						|| (op==ConfigBaseOperation.PropertiesFromStringAdd))
					{
            if ((!stringValue.Equals(string.Empty)) || (properties[i].PropertyType == typeof(char)) || (properties[i].PropertyType == typeof(string)))
            {
							object realValue=null;
              if (properties[i].PropertyType.IsEnum)
              {
                int r;
                object v = (int.TryParse(stringValue, out r) ? (object)r : stringValue);
                if (System.Enum.IsDefined(properties[i].PropertyType, v))
                  realValue = System.Enum.Parse(properties[i].PropertyType, v.ToString(), true);
                else
                  realValue = System.Enum.GetNames(properties[i].PropertyType)[0];
              }
              else
                realValue = this.ConvertFromString(stringValue, val.GetType());
              properties[i].SetValue(this, realValue, null);
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Determine if the specified PositType is possible with the specified ScoreClass. 
		/// </summary>
		/// <param name="sClass"></param>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool IsPositioningAvailable(ScoreClass sClass, PositType p)
		{
			if (p==PositType.TearTape) return false;
			switch(sClass)
			{
				case ScoreClass.MaleFemale:
					return ((p == PositType.MaleFemaleP) || (p == PositType.NoPositType));
				case ScoreClass.Offset:
				case ScoreClass.Linpac:
					return ((p != PositType.MaleFemaleP) && (p != PositType.TeletwinP));
				case ScoreClass.Western:
					return ((p == PositType.MaleFemaleP) || (p == PositType.PointToPoint) || (p == PositType.NoPositType));
				case ScoreClass.Teletwin:
				case ScoreClass.TeletwinUSA:
					return (p != PositType.MaleFemaleP);
				default:
					return false;
			}
		}

		public static int PositioningAvailableCount(ScoreClass s)
		{
			int count=0;
			for (PositType p=PositType.MaleFemaleP; p<PositType.TearTape; p++)
				if (IsPositioningAvailable(s, p)) count++;
			return count;
		}

	}

	[Description("Slit shaft"), TypeConverter(typeof(Converters))]
	public class SlitShaft: SyncroC.DataBase.LineConfig.LineBase
	{
		string config;												//configuration string
    int robotOffset;  										//to compensate for mechanical postioning problems
    int grindingOffset;
		int brokenSlits;											//bit mask of the broken slits for the two slit shafts
		//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
    int bidentDisalignement;
		//int bidentDisalignementWideBody;
		//int bidentDisalignementNarrowBody;
		////RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
		//int safetyDistSides;
		//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
		//int leftMechanicalStop;               //distance between the left mechanical stop and the left sideframe
		//int rightMechanicalStop;               //distance between the right mechanical stop and the right sideframe
		int leftMechanicalStopWideBody;         //distance between the left mechanical stop and the left sideframe
		//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
		int leftMechanicalStopStandardBody;        //distance between the left mechanical stop and the left sideframe
		int rightMechanicalStopWideBody;         //distance between the right mechanical stop and the right sideframe
		//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
		int rightMechanicalStopStandardBody;       //distance between the right mechanical stop and the right sideframe
		int leftSlitters;												//Number of slit on left storage. Used for old Fosber SS

    bool enabled;

		public SlitShaft():base()
		{
			this.sectionName="SS_SLIT_SHAFT_";

			this.config=" ";
			this.robotOffset=0;
      this.grindingOffset = 0;
			this.brokenSlits=0;
			//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
			this.bidentDisalignement = 0;
			//this.bidentDisalignementNarrowBody = 0;
			//this.bidentDisalignementWideBody = 0;
			//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
			//this.safetyDistSides = 0;
			//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
			//this.leftMechanicalStop = 0;
			//this.rightMechanicalStop = 0;
			//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
			this.leftMechanicalStopStandardBody = 0;
			this.leftMechanicalStopWideBody = 0;
			//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
			this.rightMechanicalStopStandardBody = 0;
			this.rightMechanicalStopWideBody = 0;
			this.leftSlitters = 0;
      this.enabled=false;

		}
		
		[PropertyOrder(10), Category("SlitShaft"), Description("Configuration of the shaft")]
		public string Configuration
		{
			get{return this.config;}
			set{this.config=value;}
		}

    [PropertyOrder(20), Browsable(false), Category("SlitShaft"), Description("Offset for the tools' positions in mm/10")]
    public int RobotOffset
		{
			get{return this.robotOffset;}
			set{this.robotOffset=value;}
		}

    [PropertyOrder(25), Category("SlitShaft"), Description("Offset for the grinding in mm/10")]
    public int GrindingOffset
    {
      get { return this.grindingOffset; }
      set { this.grindingOffset = value; }
    }

    [PropertyOrder(30), Category("SlitShaft"), Description("Enable status of the shaft")]
		public bool Enabled
		{
			get{return this.enabled;}
			set{this.enabled=value;}
		}

		[PropertyOrder(40), Browsable(false), Category("SlitShaft"), Description("Bit mask of the broken blades in the shaft")]
		public int BrokenBlades
		{
			get{return this.brokenSlits;}
			set{this.brokenSlits=value;}
		}

		//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
		[ReadOnly(true), PropertyOrder(50), Category("SlitShaft"), Description("The disalignment between this shaft's bident and the score shafts' bident in mm/10")]
		public int BidentDisalignment
		{
			get { return this.bidentDisalignement; }
			set { this.bidentDisalignement = value; }
		}

		////RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
		//[ReadOnly(true), PropertyOrder(60), Category("SlitShaft"), Description("The safety distance between the tools and the shaft's edge in mm/10")]
		//public int SafetyDistSides
		//{
		//  get { return this.safetyDistSides; }
		//  set { this.safetyDistSides = value; }
		//}

		//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
		//[ReadOnly(false), PropertyOrder(70), Category("SlitShaft"), Description("The distance between the left edge of the leftmost slit (when it is has been brought to the left mechanical stop) and the left sideframe in mm/10")]
		//public int LeftMechanicalStop
		//{
		//  get { return this.leftMechanicalStop; }
		//  set { this.leftMechanicalStop = value; }
		//}

		//[ReadOnly(false), PropertyOrder(70), Category("SlitShaft"), Description("The distance between the right edge of the rightmost slit (when it has been brought to the right mechanical stop) and the right sideframe in mm/10")]
		//public int RightMechanicalStop
		//{
		//  get { return this.rightMechanicalStop; }
		//  set { this.rightMechanicalStop = value; }
		//}

		//[Category("Wide Body"), PropertyOrder(10), ReadOnly(true), Description("The disalignment between this shaft's bident and the score shafts' bident in mm/10")]
		//public int BidentDisalignmentWideBody
		//{
		//  get { return this.bidentDisalignementWideBody; }
		//  set { this.bidentDisalignementWideBody = value; }
		//}

		[Category("SlitShaft"), PropertyOrder(90), ReadOnly(false), Description("The distance between the left edge of the leftmost slit (when it is has been brought to the left mechanical stop) and the left sideframe in mm/10")]
		public int LeftMechanicalStopWideBody
		{
			get { return this.leftMechanicalStopWideBody ; }
			set { this.leftMechanicalStopWideBody = value; }
		}

		[Category("SlitShaft"), PropertyOrder(100), ReadOnly(false), Description("The distance between the right edge of the rightmost slit (when it has been brought to the right mechanical stop) and the right sideframe in mm/10")]
		public int RightMechanicalStopWideBody
		{
			get { return this.rightMechanicalStopWideBody; }
			set { this.rightMechanicalStopWideBody = value; }
		}

		//[Category("Narrow Body"), PropertyOrder(40), ReadOnly(true), Description("The disalignment between this shaft's bident and the score shafts' bident in mm/10")]
		//public int BidentDisalignmentNarrowBody
		//{
		//  get { return this.bidentDisalignementNarrowBody; }
		//  set { this.bidentDisalignementNarrowBody = value; }
		//}
		
		[Category("SlitShaft"), PropertyOrder(60), Description("number of Slits on left storage. Used for split the Slit Storage with old Fosber SS ( must set to 0 for TwinE and  Twin400 )")]
		public int LeftSlitters
		{
			get { return this.leftSlitters; }
			set { this.leftSlitters = value; }
		}

		//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
		[Category("SlitShaft"), PropertyOrder(70), ReadOnly(false), Description("The distance between the left edge of the leftmost slit (when it is has been brought to the left mechanical stop) and the left sideframe in mm/10")]
		public int LeftMechanicalStopStandardBody
		{
			get { return this.leftMechanicalStopStandardBody; }
			set { this.leftMechanicalStopStandardBody = value; }
		}

		//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
		[Category("SlitShaft"), PropertyOrder(80), ReadOnly(false), Description("The distance between the right edge of the rightmost slit (when it has been brought to the right mechanical stop) and the right sideframe in mm/10")]
		public int RightMechanicalStopStandardBody
		{
			get { return this.rightMechanicalStopStandardBody; }
			set { this.rightMechanicalStopStandardBody = value; }
		}

    public int GetSlitCount()
    {
      int count = 0, partCount = 0;
      for (int cIdx = 0; cIdx < this.Configuration.Length; cIdx++)
      {
        if (char.IsDigit(this.Configuration[cIdx]))
        {
          partCount *= 10;
          partCount += (this.Configuration[cIdx] - '0');
        }
        if (!char.IsDigit(this.Configuration[cIdx]) || (cIdx == this.Configuration.Length - 1))
        {
          count += partCount;
          partCount = 0;
        }
      }
      return count;
    }

	  public bool IsSlitInTheShaft(char letter)
	  {
		  for (int i=0; i<this.config.Length; i++)
			  if (this.config.ToCharArray()[i]==letter)
				  return true;
		  return false;
	  }
  }

  [Description("Score shaft"), TypeConverter(typeof(Converters))]
	public class ScoreShaft: SyncroC.DataBase.LineConfig.LineBase
	{
		string config;														//configuration string
		string parity;														//parity string
		int leftScores;														//number of home scores on the left
    int robotOffset;  										    //to compensate for mechanical postioning problems on both upper and lower robots
    int lowerRobotOffset;  								    //to compensate for mechanical postioning problems on lower robot
    int scoreGapOffset;												//to compensate for mechanical plunge problems
    int safetyDistSides;
    bool uncouple;
    bool enabled;

		public ScoreShaft():base()
		{
			this.sectionName="SS_SCORE_SHAFT_";

			this.config=" ";
			this.parity=" ";
			this.leftScores=0;
			this.robotOffset=0;
      this.lowerRobotOffset = 0;
			this.scoreGapOffset=0;
      this.safetyDistSides = 0;
      this.uncouple = false;
      this.enabled=false;
		}
		
		[PropertyOrder(10), Category("ScoreShaft"), Description("Configuration of the shaft: sequence of scorer groups of the shaft from left to right. Each group is represented by the number of scorers and the scorer type letter.")]
		public string Configuration
		{
			get{return this.config;}
			set{this.config=value;}
		}

    [PropertyOrder(20), Category("ScoreShaft"), Description("Parity sequence of characters: each character represents the parity of one scorer of the shaft: O=odd scorer, E=even scorer, X=no parity scorer")]
		public string Parity
		{
			get{return this.parity;}
			set{this.parity=value;}
		}

		[PropertyOrder(30), Category("ScoreShaft"), Description("Scores on the left of the shaft")]
		public int LeftScores
		{
			get{return this.leftScores;}
			set{this.leftScores=value;}
		}

    [PropertyOrder(40), Category("ScoreShaft"), Description("Offset for the tools' positions of both upper and lower robots in mm/10")]
    public int RobotOffset
		{
			get{return this.robotOffset;}
      set { this.robotOffset = value; }
		}

    [PropertyOrder(45), Category("ScoreShaft"), Description("Offset for the tools' positions of lower robot in mm/10")]
    public int LowerRobotOffset
    {
      get { return this.lowerRobotOffset; }
      set { this.lowerRobotOffset = value; }
    }

    [PropertyOrder(50), Category("ScoreShaft"), Description("Offset for the score gap in mm/10")]
    public int ScoreGapOffset
		{
			get{return this.scoreGapOffset;}
			set{this.scoreGapOffset=value;}
		}

    //agrassi_FI08-07-2010_1
    [PropertyOrder(52), ReadOnly(false), Category("ScoreShaft"), Description("The safety distance between the tools and the shaft's edge in mm/10")]
    public int SafetyDistSides
    {
      get { return this.safetyDistSides; }
      set { this.safetyDistSides = value; }
    }

    [PropertyOrder(56), Category("ScoreShaft"), Description("Whether the stores of scores are uncoupled or not")]
		public bool Uncouple
		{
			get{return this.uncouple;}
			set{this.uncouple=value;}
		}

		[PropertyOrder(60), Category("ScoreShaft"), Description("Enable status of the shaft")]
		public bool Enabled
		{
			get{return this.enabled;}
			set{this.enabled=value;}
		}

    public int GetScoreCount()
    {
      int count = 0, partCount = 0;
      for (int cIdx = 0; cIdx < this.Configuration.Length; cIdx++)
      {
        if (char.IsDigit(this.Configuration[cIdx]))
        {
          partCount *= 10;
          partCount += (this.Configuration[cIdx] - '0');
        }
        if (!char.IsDigit(this.Configuration[cIdx]) || (cIdx == this.Configuration.Length - 1))
        {
          count += partCount;
          partCount = 0;
        }
      }
      return count;
    }
  }

	[Description("Positioning options for the slits"), TypeConverter(typeof(Converters))]
	public class SlitPositOptions : SyncroC.DataBase.LineConfig.LineBase
	{
		bool enableTandem;
		bool wearEqualize;						//enable the wear optimization (just for twin evolution)
		bool twoPassesPositioning;
    int positionTolerance;
    bool enableGrinding;
    //agrassi_FI23-05-08_1
    int metersForFirstGrinding;
    bool enableLubrication;
    //AGrassi_FI06/12/2007_1
    bool enableAutomaticTeflonCycle;
    int teflonCycleMeters;
		//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
		int grindMetersFromRegulationCost;		
		int grindMetersFromSubstitutionCost;

		//RFAZZI_FI26/01/09_1.Added new Twin400 parameters for have a Slit Tandem Positioning where the paper grammage and boxes required are over this values. 
		int qualityWeightForForceTandem;		// Quality weigth for force the Tandem Positioning
		int boxesForForceTandem;							// Number of boxes for force the Tandem Positioning

    public SlitPositOptions():base()
		{
			this.sectionName="SS_SLIT_POSITIONING_OPTIONS";
			
			this.enableTandem=false;
			this.wearEqualize=false;
			this.twoPassesPositioning=false;
      this.enableGrinding = false;
      //agrassi_FI23-05-08_1
      this.metersForFirstGrinding=0;
      this.enableLubrication = false;
      //AGrassi_FI06/12/2007_1
      this.enableAutomaticTeflonCycle = false;
      this.teflonCycleMeters = 0;
			//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
			this.grindMetersFromRegulationCost = 0;
			this.grindMetersFromSubstitutionCost = 0;
	
			//RFAZZI_FI26/01/09_1.Added new Twin400 parameters for have a Slit Tandem Positioning where the paper grammage and boxes required are over this values. 
			this.qualityWeightForForceTandem = 1700;		
			this.boxesForForceTandem = 4;							

		}

		[PropertyOrder(10), Category("SlitPositOptions"), Description("Enable the slit tandem")]
		public bool EnableTandem
		{
			get{return this.enableTandem;}
			set{this.enableTandem=value;}
		}

		//RFAZZI_FI26/01/09_1.Changed the parameter description. 		
    [PropertyOrder(20), Browsable(true), Category("SlitPositOptions"), Description("Enable the wear equalization ( only for TwinE/Twin400 )")]
    public bool WearEqualize
		{
			get{return this.wearEqualize;}
			set{this.wearEqualize=value;}
		}

		[PropertyOrder(30), Browsable(false), Category("SlitPositOptions"), Description("Makes the positioning to be in two passes ( only Twin400 )")]
		public bool TwoPassesPositioning
		{
			get{return this.twoPassesPositioning;}
			set{this.twoPassesPositioning=value;}
		}

		//RFAZZI_FI26/01/09_1.Added new Twin400 parameters for have a Slit Tandem Positioning where the paper grammage and boxes required are over this values. 
		[PropertyOrder(32), Browsable(true), Category("SlitPositOptions"), Description("From this quality weight Syncro force the Tandem Positioning when the boxes match with the below parameter ( only for Twin400 )")]
		public int QualityWeightForForceTandem
    {
			get { return this.qualityWeightForForceTandem; }
			set { this.qualityWeightForForceTandem = value; }
    }

		[PropertyOrder(34), Browsable(true), Category("SlitPositOptions"), Description("From this number of boxes Syncro force the Tandem Positioning when the Quality Grammage match with the above parameter ( only for Twin400 )")]
		public int BoxesForForceTandem
    {
			get { return this.boxesForForceTandem; }
			set { this.boxesForForceTandem = value; }
    }

    [PropertyOrder(40), Browsable(true), Category("PositionTolerance"), Description("Tolerance in the slit positions in mm/10")]
    public int PositionTolerance
    {
      get { return this.positionTolerance; }
      set { this.positionTolerance = value; }
    }

    [PropertyOrder(50), Browsable(true), Category("PositionTolerance"), Description("True if automatic slit grinding is enabled")]
    public bool EnableGrinding
    {
      get { return this.enableGrinding; }
      set { this.enableGrinding = value; }
    }

    //agrassi_FI23-05-08_1
		[PropertyOrder(55), Browsable(true), Category("PositionTolerance"), Description("The minimum number of meters run by the blades to make the first grinding ( only Twin400 )")]
    public int MetersForFirstGrinding
    {
      get { return this.metersForFirstGrinding; }
      set { this.metersForFirstGrinding = value; }
    }

    [PropertyOrder(60), Browsable(true), Category("PositionTolerance"), Description("True if automatic slit lubrication is enabled")]
    public bool EnableLubrication
    {
      get { return this.enableLubrication; }
      set { this.enableLubrication = value; }
    }

    //AGrassi_FI06/12/2007_1
		[PropertyOrder(70), Browsable(true), Category("PositionTolerance"), Description("True if automatic start of teflon cycle is enabled ( only Twin400 )")]
    public bool EnableAutomaticTeflonCycle
    {
      get { return this.enableAutomaticTeflonCycle; }
      set { this.enableAutomaticTeflonCycle = value; }
    }

		[PropertyOrder(80), Browsable(true), Category("PositionTolerance"), Description("Value of the meters run for the automatic start of teflon cycle ( only Twin400 )")]
    public int TeflonCycleMeters
    {
      get { return this.teflonCycleMeters; }
      set { this.teflonCycleMeters = value; }
    }

		//RFAZZI_FI28/11/2008_1.Added new slit optimization based on slit grinding counters from last substitution and last regulation. 
		[PropertyOrder(90), Browsable(true), Category("PositionTolerance"), Description("Value of the grind meters from Slit Substitution ( only for Twin400 from PLC Interface Version >= 5 )")]
		public int GrindMetersFromSubstitutionCost
    {
			get { return this.grindMetersFromSubstitutionCost; }
			set { this.grindMetersFromSubstitutionCost = value; }
    }

		[PropertyOrder(100), Browsable(true), Category("PositionTolerance"), Description("Value of the grind meters from Slit Regulation ( only for Twin400 from PLC Interface Version >= 5 )")]
		public int GrindMetersFromRegulationCost
    {
			get { return this.grindMetersFromRegulationCost; }
			set { this.grindMetersFromRegulationCost = value; }
    }			

  }

  [Description("Positioning options for the scores"), TypeConverter(typeof(Converters))]
	public class ScorePositOptions: SyncroC.DataBase.LineConfig.LineBase
	{
		public enum NonScoringScores
		{
      NoScore=0,
			SupportScores=1,
			TrimScores=2
		};

		int scoreSlitOffset;						//offset between scores and slits when slits are in correct positions but scores are always off by the same distance, no matter what score shaft
		bool enableTandem;
		bool multipleProfilesOnShaft;		//if true, it is possible to position multiple types of scores (or multiple positioning types) on the same shaft
		int gapTolerance;								//gap tolerance for the compatibility of scores/positioning types (reverse, point to point, ...)
    ParityPriorityType parityPriority;//if true choose to respect parity instead of shaft optimization
		bool cleanToLeft;								//if true, cleaning will clean to the left
		bool alwaysPositionHalf;				//if true and there are 4 score robots, the left and right will each always position half the tools required on the shaft
		bool twoPassesPositioning;
		NonScoringScores nonScoringScoreType;				//enable support scores, trim scores or neither
    int minTrimForTrimScores;
    int trimScoreDistance;
    bool showNormalReverse;

		public ScorePositOptions(): base()
		{
			this.sectionName="SS_SCORE_POSITIONING_OPTIONS";
			
			this.scoreSlitOffset=0;
			this.enableTandem=false;
			this.multipleProfilesOnShaft=false;
			this.gapTolerance=0;
			this.parityPriority=ParityPriorityType.LowParityPriority;
			this.cleanToLeft=false;
			this.alwaysPositionHalf=false;
			this.twoPassesPositioning=false;
			this.nonScoringScoreType=NonScoringScores.SupportScores;
      this.minTrimForTrimScores=0;
      this.trimScoreDistance=0;
      this.showNormalReverse = false;
    }

    [PropertyOrder(10), Category("ScorePositOptions"), Description("Offset between slits and scores in mm/10")]
    public int ScoreSlitOffset
		{
			get{return this.scoreSlitOffset;}
			set{this.scoreSlitOffset=value;}
		}

		[PropertyOrder(20), Category("ScorePositOptions"), Description("Enable the score tandem ")]
		public bool EnableTandem
		{
			get{return this.enableTandem;}
			set{this.enableTandem=value;}
		}

		[PropertyOrder(30), Category("ScorePositOptions"), Description("Give priority to the parity respect to the shafts envolved")]
    public ParityPriorityType ParityPriority
		{
			get{return this.parityPriority;}
			set{this.parityPriority=value;}
		}

		[PropertyOrder(40), Category("ScorePositOptions"), Description("Type of non scoring scores in case of no-score setup")]
		public NonScoringScores NonScoringScoreType
		{
			get{return this.nonScoringScoreType;}
			set{this.nonScoringScoreType=value;}
		}

		[PropertyOrder(43), Category("ScorePositOptions"), Description("Minimum total trim in tenths of mm to put scores on the trim: if the trim is less than this, support scores will be used.")]
		public int MinTrimForTrimScores
		{
			get{return this.minTrimForTrimScores;}
			set{this.minTrimForTrimScores=value;}
		}

    [PropertyOrder(46), Category("ScorePositOptions"), Description("Distance in tenths of mm between the scoring point and the trim slit for scores on the trim.")]
		public int TrimScoreDistance
		{
			get{return this.trimScoreDistance;}
			set{this.trimScoreDistance=value;}
		}

    [PropertyOrder(50), Browsable(true), Category("ScorePositOptions"), Description("Enables the possibility to position multiple profiles on the same shaft")]
		public bool MultipleProfilesOnShaft
		{
			get{return this.multipleProfilesOnShaft;}
			set{this.multipleProfilesOnShaft=value;}
		}

    [PropertyOrder(55), Browsable(false), Category("ScorePositOptions"), Description("Whether NormalReverse positioning type has to be shown amongst the others")]
    public bool ShowNormalReverse
    {
      get { return this.showNormalReverse; }
      set { this.showNormalReverse = value; }
    }

    [PropertyOrder(60), Browsable(false), Category("ScorePositOptions"), Description("Set the cleaning side to the left of the shaft")]
    public bool CleanToLeft
		{
			get{return this.cleanToLeft;}
			set{this.cleanToLeft=value;}
		}

		[PropertyOrder(70), Browsable(false), Category("ScorePositOptions"), Description("")]
		public bool AlwaysPositionHalf
		{
			get{return this.alwaysPositionHalf;}
			set{this.alwaysPositionHalf=value;}
		}

		[PropertyOrder(80), Browsable(false), Category("ScorePositOptions"), Description("Enables the positioning in two passes")]
		public bool TwoPassesPositioning
		{
			get{return this.twoPassesPositioning;}
			set{this.twoPassesPositioning=value;}
		}

    [PropertyOrder(90), Browsable(true), Category("ScorePositOptions"), Description("The maximum difference in the profile gap between scores for them to be positioned on the same shaft in mm/10")]
    public int GapTolerance
		{
			get{return this.gapTolerance;}
			set{this.gapTolerance=value;}
		}
	}
	
  //agrassi_FI20-05-2011_1
  [Description("Robot speeds"), TypeConverter(typeof(Converters))]
  public class RobotSpeeds : SyncroC.DataBase.LineConfig.LineBase
  {
    int[][] robotSpeed_UV_mm_s;
    int[][] robotAcceleration_UA_mm_s2;
    int[][] robotDeceleration_UA_mm_s2;
    int finalSpeed_UV_mm_s;
    int finalAccDec_UA_mm_s2;
    int finalDist_UD_mm;
    int metersForFastSpeed;
    int timeToDeflateAndInflate_UT_ms;
    int bidentMovingTime_UT_ms;

    public RobotSpeeds():base()
    {
      this.sectionName = "SS_TWINE_400_ROBOT_SPEEDS";
      this.robotSpeed_UV_mm_s = new int[4][];
      for (int i = 0; i < 4; i++)
        this.robotSpeed_UV_mm_s[i] = new int[2];
      this.robotAcceleration_UA_mm_s2 = new int[4][];
      for (int i = 0; i < 4; i++)
        this.robotAcceleration_UA_mm_s2[i] = new int[2];
      this.robotDeceleration_UA_mm_s2 = new int[4][];
      for (int i = 0; i < 4; i++)
        this.robotDeceleration_UA_mm_s2[i] = new int[2];
    }

    [PropertyOrder(10), Category("Robot speed"), Description("Velocità del robot in mm/s. Primo indice:[0]=posizionamento lento, [1]=posizionamento veloce. Secondo indice: [0]=spostamento di cordoni, [1]=spostamento di un coltello, [2]=spostamento di più coltelli, [3]=salto utensile"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[][] RobotSpeed_UV_mm_s
    {
      get { return this.robotSpeed_UV_mm_s; }
      set { this.robotSpeed_UV_mm_s = value; }
    }

    [PropertyOrder(20), Category("Robot speed"), Description("Accelerazione del robot in mm/s2. Primo indice:[0]=posizionamento lento, [1]=posizionamento veloce. Secondo indice: [0]=spostamento di cordoni, [1]=spostamento di un coltello, [2]=spostamento di più coltelli, [3]=salto utensile"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[][] RobotAcceleration_UA_mm_s2
    {
      get { return this.robotAcceleration_UA_mm_s2; }
      set { this.robotAcceleration_UA_mm_s2 = value; }
    }

    [PropertyOrder(30), Category("Robot speed"), Description("Decelerazione del robot in mm/s2. Primo indice:[0]=posizionamento lento, [1]=posizionamento veloce. Secondo indice: [0]=spostamento di cordoni, [1]=spostamento di un coltello, [2]=spostamento di più coltelli, [3]=salto utensile"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[][] RobotDeceleration_UA_mm_s2
    {
      get { return this.robotDeceleration_UA_mm_s2; }
      set { this.robotDeceleration_UA_mm_s2 = value; }
    }

    [PropertyOrder(40), Category("Robot speed"), Description("Velocità del robot in mm/s alla fine di ogni movimento")]
    public int FinalSpeed_UV_mm_s
    {
      get { return this.finalSpeed_UV_mm_s; }
      set { this.finalSpeed_UV_mm_s = value; }
    }

    [PropertyOrder(50), Category("Robot speed"), Description("Accelerazione e decelerazione del robot in mm/s2 alla fine di ogni movimento")]
    public int FinalAccDec_UA_mm_s2
    {
      get { return this.finalAccDec_UA_mm_s2; }
      set { this.finalAccDec_UA_mm_s2 = value; }
    }

    [PropertyOrder(60), Category("Robot speed"), Description("Distanza rimanente del movimento in mm per attivare la velocità, l'accelerazione e la decelerazione finale")]
    public int FinalDist_UD_mm
    {
      get { return this.finalDist_UD_mm; }
      set { this.finalDist_UD_mm = value; }
    }

    [PropertyOrder(70), Category("Robot speed"), Description("Tempo in ms che impiega il bidente o il trascinatore per uscire od entrare")]
    public int BidentMovingTime_UT_ms
    {
      get { return this.bidentMovingTime_UT_ms; }
      set { this.bidentMovingTime_UT_ms = value; }
    }

    [PropertyOrder(80), Category("Robot speed"), Description("Tempo in ms necessario per sgonfiare e rigonfiare le camere d'aria")]
    public int TimeToDeflateAndInflate_UT_ms
    {
      get { return this.timeToDeflateAndInflate_UT_ms; }
      set { this.timeToDeflateAndInflate_UT_ms = value; }
    }

    [PropertyOrder(90), Category("Robot speed"), Description("Metri rimanenti al cambio ordine al di sotto dei quali il posizionamento è veloce")]
    public int MetersForFastSpeed
    {
      get { return this.metersForFastSpeed; }
      set { this.metersForFastSpeed = value; }
    }

  }

	[Description("Positioning options"), TypeConverter(typeof(Converters))]
	public class PositOptions: SyncroC.DataBase.LineConfig.LineBase
	{
		SlitPositOptions slits;			//
		ScorePositOptions scores;		//multipleProfilesOnShaft, gapTolerance
    //agrassi_FI20-05-2011_1
    RobotSpeeds robotSpeed;

		public PositOptions():base()
		{
			this.sectionName="SS_POSIT_OPTIONS";

			this.slits = new SlitPositOptions();
			this.scores = new ScorePositOptions();
      //agrassi_FI20-05-2011_1
      this.robotSpeed = new RobotSpeeds();

		}
		
		[PropertyOrder(10), Category("PositOptions"), Description("Positioning options for the slits")]
		public SlitPositOptions Slits
		{
			get{return this.slits;}
			set{this.slits=value;}
		}

		[PropertyOrder(20), Category("PositOptions"), Description("Positioning options for the scores")]
		public ScorePositOptions Scores
		{
			get{return this.scores;}
			set{this.scores=value;}
		}

    //agrassi_FI20-05-2011_1
    [PropertyOrder(30), Category("PositOptions"), Description("Velocità ed accelerazioni del robot")]
    public RobotSpeeds RobotSpeed
    {
      get { return this.robotSpeed; }
      set { this.robotSpeed = value; }
    }
  }

  [Description("Configuration data for the plunges' calibration"), TypeConverter(typeof(Converters))]
  public class Twin400Plunges: SyncroC.DataBase.LineConfig.LineBase
  {
    //agrassi_FI18-05-2010_3
    //public const int BLOCK_HEIGHTS_COUNT = 5;
    public const int BLOCK_HEIGHTS_COUNT = 9;

    int[] angleParameter;				//angle parameter for each shaft in hundreths of degree
	  int[] gapParameter;					//gap parameter for each shaft in tenths of mm
	  int[] plungeAngle1;					//first plunge angle (in hundreths of degree) for the calibration
	  int[] plungeAngle2;					//second plunge angle (in hundreths of degree) for the calibration
	  int[] rollDistance1;				//first roll distance (in tenths of mm) for the calibration
	  int[] rollDistance2;				//second roll distance (in tenths of mm) for the calibration
	  int[] blockHeights;					//the heights (in tenths of mm) of the little blocks used to calibrate the plunges

		public Twin400Plunges(): base()
    {
			this.sectionName="SS_TWIN400_PLUNGES";

			//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
			this.angleParameter = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      this.gapParameter = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      this.plungeAngle1 = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      this.plungeAngle2 = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      this.rollDistance1 = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      this.rollDistance2 = new int[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
      this.blockHeights = new int[BLOCK_HEIGHTS_COUNT];
      for (int sh = 0; sh < SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
      {
        this.angleParameter[sh] = 0;
        this.gapParameter[sh] = 0;
        this.plungeAngle1[sh] = 0;
        this.plungeAngle2[sh] = 0;
        this.rollDistance1[sh] = 0;
        this.rollDistance2[sh] = 0;
      }
      for (int i = 0; i < BLOCK_HEIGHTS_COUNT; i++)
        this.blockHeights[i] = 0;
    }

    [PropertyOrder(10), ReadOnly(true), Category("Punges' calibration data"), Description("The angle parameters in hundreths of degree"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] AngleParameter
    {
      get { return this.angleParameter; }
      set { this.angleParameter = value; }
    }

    [PropertyOrder(20), ReadOnly(true), Category("Punges' calibration data"), Description("The gap parameter for each shaft in tenths of mm"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] GapParameter
    {
      get { return this.gapParameter; }
      set { this.gapParameter = value; }
    }

    [PropertyOrder(30), Browsable(false), Category("Punges' calibration data"), Description("The first plunge angle for each shaft in hundreths of degree"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] PlungeAngle1
    {
      get { return this.plungeAngle1; }
      set { this.plungeAngle1 = value; }
    }

    [PropertyOrder(40), Browsable(false), Category("Punges' calibration data"), Description("The second plunge angle for each shaft in hundreths of degree"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] PlungeAngle2
    {
      get { return this.plungeAngle2; }
      set { this.plungeAngle2 = value; }
    }

    [PropertyOrder(50), Browsable(false), Category("Punges' calibration data"), Description("The first roll distance for each shaft in tenths of mm"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] RollDistance1
    {
      get { return this.rollDistance1; }
      set { this.rollDistance1 = value; }
    }

    [PropertyOrder(60), Browsable(false), Category("Punges' calibration data"), Description("The second roll distance for each shaft in tenths of mm"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] RollDistance2
    {
      get { return this.rollDistance2; }
      set { this.rollDistance2 = value; }
    }

    [PropertyOrder(70), Browsable(false), Category("Punges' calibration data"), Description("The height of the blocks used for the calibration in tenths of mm"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public int[] BlockHeights
    {
      get { return this.blockHeights; }
      set { this.blockHeights = value; }
    }
  }

  [Description("Configuration data for the PLC"), TypeConverter(typeof(Converters))]
  public class Twin400PlcConfigData : SyncroC.DataBase.LineConfig.LineBase
  {
    public enum SecurityRuleType { CE = 0, UL = 1 };

		bool edgeGuidePresent;
		bool prePrintPresent;
		//RFAZZI_FI30/10/2008_1. Added DustRemovalSystem parameter
		bool dustRemovalSystemPresent;
		bool conditionedCabinet;
    SecurityRuleType securityRule;															
		//bool standAloneConsole;
		int score1ToMWDistance;
		int webDirToMWDistance;
		int slit1RaiseTime;
		int slit1PlungeTime;
		int slit2RaiseTime;
		int slit2PlungeTime;
		int scoreAnticipation;
		int chutesAnticipation;
		int webDirAnticipation;
		int mwNumberOfPulses;
    int chutesResetPosition;
    int maxLineSpeed;
    int gainForSpeedRef;
		int offsetForSpeedRef;
    int loadTeflonMeters;

		public Twin400PlcConfigData():base()
		{
			this.sectionName="SS_TWIN400_PLC_CONFIG";
			
			this.edgeGuidePresent=false;
			this.prePrintPresent=false;;
			//RFAZZI_FI30/10/2008_1. Added DustRemovalSystem parameter
			this.dustRemovalSystemPresent = false;
			this.conditionedCabinet=false;
			this.securityRule=SecurityRuleType.CE;
			//this.standAloneConsole=false;
			this.score1ToMWDistance=0;
			this.webDirToMWDistance=0;
			this.slit1RaiseTime=0;
			this.slit1PlungeTime=0;
			this.slit2RaiseTime=0;
			this.slit2PlungeTime=0;
			this.scoreAnticipation=0;
			this.chutesAnticipation=0;
			this.webDirAnticipation=0;
			this.mwNumberOfPulses=0;
			this.chutesResetPosition=0;
			this.maxLineSpeed=0;
			this.gainForSpeedRef=0;
			this.offsetForSpeedRef=0;
      this.loadTeflonMeters = 0;
		}
		
		[PropertyOrder(10), Category("Plc configuration data"), Description("Presence of edge guide")]
		public bool EdgeGuidePresent
		{
			get{return this.edgeGuidePresent;}
			set{this.edgeGuidePresent=value;}
		}

		[PropertyOrder(20), Category("Plc configuration data"), Description("Presence of pre-print")]
		public bool PrePrintPresent
		{
			get{return this.prePrintPresent;}
			set{this.prePrintPresent=value;}
		}

		//RFAZZI_FI30/10/2008_1. Added DustRemovalSystem parameter
		[PropertyOrder(25), Category("Plc configuration data"), Description("Presence of Dust Removal System")]
		public bool DustRemovalSystemPresent
		{
			get { return this.dustRemovalSystemPresent; }
			set { this.dustRemovalSystemPresent = value; }
		}

		[PropertyOrder(30), Category("Plc configuration data"), Description("The cabinets are conditioned")]
		public bool ConditionedCabinet
		{
			get{return this.conditionedCabinet;}
			set{this.conditionedCabinet=value;}
		}

		[PropertyOrder(40), Category("Plc configuration data"), Description("Standard security law")]
    public SecurityRuleType SecurityRule
		{
			get{return this.securityRule;}
			set{this.securityRule=value;}
		}

    //[PropertyOrder(50), Category("Plc configuration data"), Description("The console is not Syncro400, but it is Pmac400")]
    //public bool StandAloneConsole
    //{
    //  get{return this.standAloneConsole;}
    //  set{this.standAloneConsole=value;}
    //}

		[PropertyOrder(60), Category("Plc configuration data"), Description("Distance between score shaft 1 and the measuring wheel in mm")]
		public int Score1ToMWDistance
		{
			get{return this.score1ToMWDistance;}
			set{this.score1ToMWDistance=value;}
		}

		[PropertyOrder(70), Category("Plc configuration data"), Description("Distance between Web Director and the measuring wheel in mm")]
		public int WebDirToMWDistance
		{
			get{return this.webDirToMWDistance;}
			set{this.webDirToMWDistance=value;}
		}

		[PropertyOrder(80), Category("Plc configuration data"), Description("In work anticipation time for slit shaft 1 in ms")]
		public int Slit1InWorkTime
		{
			get{return this.slit1RaiseTime;}
			set{this.slit1RaiseTime=value;}
		}

		[PropertyOrder(90), Category("Plc configuration data"), Description("Out of work anticipation time for slit shaft 1 in ms")]
		public int Slit1OutOfWorkTime
		{
			get{return this.slit1PlungeTime;}
			set{this.slit1PlungeTime=value;}
		}

		[PropertyOrder(100), Category("Plc configuration data"), Description("In work anticipation time for slit shaft 2 in ms")]
		public int Slit2InWorkTime
		{
			get{return this.slit2RaiseTime;}
			set{this.slit2RaiseTime=value;}
		}

		[PropertyOrder(110), Category("Plc configuration data"), Description("Out of work anticipation time for slit shaft 2 in ms")]
		public int Slit2OutOfWorkTime
		{
			get{return this.slit2PlungeTime;}
			set{this.slit2PlungeTime=value;}
		}

		[PropertyOrder(120), Category("Plc configuration data"), Description("The anticipation to give the score shafts in millimeters (if PLCVersion >= 5 then usually is 1200)")]
		public int ScoreAnticipation
		{
			get{return this.scoreAnticipation;}
			set{this.scoreAnticipation=value;}
		}

		[PropertyOrder(130), Category("Plc configuration data"), Description("The anticipation to give the chutes in ms")]
		public int ChutesAnticipation
		{
			get{return this.chutesAnticipation;}
			set{this.chutesAnticipation=value;}
		}
    //PMASSARO_FI05/05/11 - Rimossa la precedente modifica //agrassi_FI05-01-2011_2 che rivedeva la descrizione del parametro 'WebDirAnticipation' 
    //agrassi_FI05-01-2011_2
    //[PropertyOrder(140), Category("Plc configuration data"), Description("The anticipation to give the Web Director in ms")]
    //[PropertyOrder(140), Category("Plc configuration data"), Description("Attenzione! Questa è la circonferenza della ruota di misura in mm. Normalmente deve rimanere a 500.")]
    [PropertyOrder(140), Category("Plc configuration data"), Description("Anticipo da dare al Web Director in fase di cambio ordine (msec)")]
    public int WebDirAnticipation
		{
			get{return this.webDirAnticipation;}
			set{this.webDirAnticipation=value;}
		}

		[PropertyOrder(150), Category("Plc configuration data"), Description("The number of pulses per revolution of the measure wheel encoder")]
		public int MWNumberOfPulses
		{
			get{return this.mwNumberOfPulses;}
      set { this.mwNumberOfPulses = value; }
    }

    [Browsable(false), PropertyOrder(160), Category("Plc configuration data"), Description("The reset position of the chutes in mm")]
    public int ChutesResetPosition
		{
			get{return this.chutesResetPosition;}
			set{this.chutesResetPosition=value;}
		}

		[PropertyOrder(170), Category("Plc configuration data"), Description("Maximum speed in mm/min")]
		public int MaxLineSpeed
		{
			get{return this.maxLineSpeed;}
			set{this.maxLineSpeed=value;}
		}

		[PropertyOrder(180), Category("Plc configuration data"), Description("The gain for speed reference")]
		public int GainForSpeedRef
		{
			get{return this.gainForSpeedRef;}
			set{this.gainForSpeedRef=value;}
		}

		[PropertyOrder(190), Category("Plc configuration data"), Description("The offset for speed reference in m/min")]
		public int OffsetForSpeedRef
		{
			get{return this.offsetForSpeedRef;}
			set{this.offsetForSpeedRef=value;}
		}

    [PropertyOrder(200), Category("Plc configuration data"), Description("The distance in m after which the teflon is automatically loaded in m")]
    public int LoadTeflonMeters
    {
      get { return this.loadTeflonMeters; }
      set { this.loadTeflonMeters = value; }
    }

  }


	/// <summary>
	/// Summary description for SingleFacer.
	/// </summary>
	///
//  [TypeConverter(typeof(Converters))]
//  public class SlitterScorer : SyncroC.DataBase.LineConfig.LineBase
//  {
//    public const int MAX_SLIT_TYPES_MACHINE=4;
//    public const int MAX_SCORE_TYPES_MACHINE=10;
//    public const int NUM_SLIT_SHAFTS=2;
//    public const int MAX_NUM_SCORE_SHAFTS=3;
//    public const int MAX_SLITS_PER_SHAFT=10;
//    //RFAZZI_FI29/02/2008_1.//Created new data revision. Modified SetupData increasing the MAX_SCORES_PER_SHAFT from 26 to 36 ( Twin400 Featherston )
//    public const int MAX_SCORES_PER_SHAFT = 36;
//    public const int SS_SETUPS = 3;
//    public const int MAINTENANCE_SETUP_ID = 999;
//    //AGrassi_FI06/12/2007_1
//    public const int TEFLON_SETUP_ID = 998;
//    //AGrassi_FI06/12/2007_1
//    //RFAZZI_FI29/02/2008_2.Created new Twin400 PLC Protocol Version for WideBody and TopSlitter
//    //RFAZZI_FI19/08/2008_1Added New Twin400 PLC Protocol 6. 
//    public enum MachineVersion { Ver1 = 1, Ver2 = 2, Ver3 = 3, Ver4 = 4, Ver5 = 5, Ver6 = 6 };
//    public enum MachineFormat	{Fmt2500=2500,Fmt2800=2800};
//    public enum Robots {OneRobots=1, TwoRobots=2, ThreeRobots=3/*, FiveRobots=5*/};

//    string name;																		//machine's name
//    MachineVersion plcInterfaceVersion;
//    MachineFormat format;			  										//machine's format
//    int width;																			//width of the shafts
//    Robots robotCount;															//number of robots (1 or 2 for Twin Ev, 3 or 5 for Twin 400)
//    bool cutFromBottom;
//    bool auxPresent;																//presence of auxiliary shaft
//    bool anvilsPresent;
//    bool acceptNarrowTrim;													//if true it accepts the trim below the minimum
//    int anvilsArmWidth;															//distance between the blade and the inner edge of the anvil's arm
//    int sharpeningMinDistance;											//minimum distance between the adjacent edges of consecutive slitters to allow the sharpening
//    int slitBidentSafetyDistance;
//    int scoreBidentSafetyDistance;
//    bool boardCenterFromPST;												//true if board center has been determined, otherwise the best board center will be automatically be chosen
//    int boardCenterOffset;													//distance between the machine's center and board's center (negative if board's center is on the left of the machine's center, otherwise positive)
//    //umbro 22-08-06
//    //int minWebWidth;																//minimum web width (it depends on the minimum inner distance between the trim chutes)
//    int minChutesPosition;
//    int maxWebWidth;																//maximum web width (it depends on the scores configuration)
//    //int sideOffset;																	//distance between the score shaft side and the slit shaft side
//    int maxTrim;																		//maximum trim width
//    int minTrim;																		//minimum trim width
//    int kBladeConsumption;                          //radius reduction because of a sharpening (in micrometers)
//    int slitBidentWidth;
//    int scoreBidentWidth;
//    int scoreShaftDiameter;
//    int slitSideMargin;															//margin for the external limit positions of the slits in mm/10
//    //RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
//    int safetySlitsDistSides;
//    int sideframesDistance;													//distance between the sideframes of the machine in mm/10
//    int minimumScoreGap;
//    int defCurrentGroup;														//group of the current setup when queue is empty
//    bool plcStateLog;
//    bool communicatesWithSyncro;
//    //agrassi_FI30-05-08
//    bool enableScorerOptionsInsertion;							//makes visible the scorer option form in the form of the insertion of orders
//    //RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
//    int offsetTrimChute; 
//    MachineOperatorSide operatorSide;
//    SlitType[] slitTypes;														//letter, width, ...
//    ScoreType[] scoreTypes;													//letter, scoreClass. ...
//    SlitShaft[] slitShafts;													//config, offset
//    ScoreShaft[] scoreShafts;												//config, leftScores, ....
//    PositOptions posit;															//SlitPositOptions, ...
//    Plunges plunges;                                //data for the plunges' calibration
//    PlcConfigData plcConfig;


//    /// <summary>
//    /// Builder.
//    /// </summary>
//    /// <param name="number"></param>
//    public SlitterScorer():base()
//    {
//      this.sectionName="SS_HEADER";
//      //Default values
//      this.name=" ";
//      this.plcInterfaceVersion = MachineVersion.Ver1;
//      this.format=MachineFormat.Fmt2500;
//      this.width=0;
//      this.robotCount=Robots.ThreeRobots;
//      this.cutFromBottom = false;
//      this.auxPresent=false;
//      this.anvilsPresent=false;
//      this.acceptNarrowTrim=false;
//      this.anvilsArmWidth=0;
//      this.sharpeningMinDistance=0;
//      this.slitBidentSafetyDistance = 0;
//      this.scoreBidentSafetyDistance = 0;
//      this.boardCenterFromPST = false;
//      this.boardCenterOffset = 0;
//      //umbro 22-08-06
//      //this.minWebWidth = 0;
//      this.minChutesPosition = 0;
//      this.maxWebWidth=0;
//      //this.sideOffset=0;
//      this.maxTrim=0;
//      this.minTrim=0;
//      this.kBladeConsumption = 0;
//      this.slitBidentWidth = 0;
//      this.scoreBidentWidth = 0;
//      this.scoreShaftDiameter=0;
//      this.slitSideMargin=0;
//      //RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
//      this.safetySlitsDistSides = 0;
//      this.sideframesDistance = 0;
//      this.minimumScoreGap = 0;
//      this.defCurrentGroup = 0;
//      this.plcStateLog = false;
//      this.communicatesWithSyncro = false;
//      //agrassi_FI30-05-08
//      this.enableScorerOptionsInsertion=false;							//makes visible the scorer option form in the form of the insertion of orders
//      //RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
//      this.offsetTrimChute = 0;
//      this.operatorSide = MachineOperatorSide.RightSide;
//      this.slitTypes=new SlitType[MAX_SLIT_TYPES_MACHINE];
//      for (int i=0; i<MAX_SLIT_TYPES_MACHINE; i++)
//        this.slitTypes[i]=new SlitType();
//      this.scoreTypes=new ScoreType[MAX_SCORE_TYPES_MACHINE];
//      for (int i=0; i<MAX_SCORE_TYPES_MACHINE; i++)
//        this.scoreTypes[i]=new ScoreType();
//      this.slitShafts=new SlitShaft[NUM_SLIT_SHAFTS];
//      for (int i=0; i<NUM_SLIT_SHAFTS; i++)
//        this.slitShafts[i]=new SlitShaft();
//      this.scoreShafts=new ScoreShaft[MAX_NUM_SCORE_SHAFTS];
//      for (int i=0; i<MAX_NUM_SCORE_SHAFTS; i++)
//        this.scoreShafts[i]=new ScoreShaft();
//      this.posit=new PositOptions();
//      this.plunges = new Plunges();
//      this.plcConfig=new PlcConfigData();
//    }


//    //RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
//    public bool isTwin400Wide()
//    {
//      return this.width == 37500;
//    }


//    [Category("1. General"), PropertyOrder(10), TypeConverter(typeof(SSNameConverter)), Description("The name of the machine")]
//    public string Name
//    {
//      get{return this.name;}
//      set{this.name=value;}
//    }

//    [Category("1. General"), PropertyOrder(15), Description("The version of the interface with PLC")]
//    public MachineVersion PLCInterfaceVersion
//    {
//      get { return this.plcInterfaceVersion; }
//      set { this.plcInterfaceVersion = value; }
//    }

//    [Category("1. General"), PropertyOrder(20), Description("The format of the machine")]
//    public MachineFormat Format
//    {
//      get{return this.format;}
//      set{this.format=value;}
//    }

//    //RFAZZI_FI29/02/2008_2.Created new Twin400 PLC Protocol Version for WideBody and TopSlitter
//    [Category("1. General"), ReadOnly(false), PropertyOrder(30), TypeConverter(typeof(SSWidthConverter)), Description("The width of score shafts used by the scorers in mm/10; it is 34500 for the Twin400 Narrow Body, 37500 for the Twin400 Wide Body and 35000 for the TwinE.")]
//    public int MachineWidth
//    {
//      get{return this.width;}
//      set{this.width=value;}
//    }

//    //RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
//    [Browsable(false), Category("1. General"), ReadOnly(false), PropertyOrder(33), Description("The distance between the sideframes of the machine in mm/10")]
//    public int SideframesDistance
//    {
//      get { return this.sideframesDistance; }
//      set { this.sideframesDistance = value; }
//    }
//    //[Browsable(false), Category("1. General"), ReadOnly(false), PropertyOrder(33), Description("The distance between the sideframes of the machine in mm/10")]
//    //public int SideframesDistanceWideBody
//    //{
//    //  get { return this.sideframesDistanceWideBody; }
//    //  set { this.sideframesDistanceWideBody = value; }
//    //}

//    //[Browsable(false), Category("1. General"), ReadOnly(false), PropertyOrder(35), Description("The distance between the sideframes of the machine in mm/10")]
//    //public int SideframesDistanceNarrowBody
//    //{
//    //  get { return this.sideframesDistanceNarrowBody; }
//    //  set { this.sideframesDistanceNarrowBody = value; }
//    //}

//    [Category("1. General"), ReadOnly(false), PropertyOrder(36), Description("The margin for the external limit positions of the slits in mm/10")]
//    public int SlitSideMargin
//    {
//      get { return this.slitSideMargin; }
//      set { this.slitSideMargin = value; }
//    }

//    //RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
//    [ReadOnly(true), PropertyOrder(40), Category("1. General"), Description("The safety distance between the tools and the shaft's edge in mm/10")]
//    public int SafetySlitsDistSides
//    {
//      get { return this.safetySlitsDistSides; }
//      set { this.safetySlitsDistSides = value; }
//    }

//    //agrassi_FI07-04-08_9
//    //[Browsable(false), Category("1. General"), PropertyOrder(40), Description("Minimum distance between the adjacent edges of consecutive slitters to allow the sharpening in mm/10")]
//    //public int SharpeningMinDistance
//    //{
//    //  get{return this.sharpeningMinDistance;}
//    //  set{this.sharpeningMinDistance=value;}
//    //}

//    [Category("1. General"), PropertyOrder(60), Description("Maximum total trim width in mm/10")]
//    public int MaxTrim
//    {
//      get{return this.maxTrim;}
//      set{this.maxTrim=value;}
//    }

//    [Category("1. General"), PropertyOrder(70), Description("Minimum total trim width in mm/10")]
//    public int MinTrim
//    {
//      get{return this.minTrim;}
//      set{this.minTrim=value;}
//    }

//    [Browsable(false), Category("1. General"), PropertyOrder(80), Description("The radius reduction of a blade because of a sharpening in \u03BCm")]
//    public int KBladeConsumption
//    {
//      get { return this.kBladeConsumption; }
//      set { this.kBladeConsumption = value; }
//    }

//    [Browsable(false), Category("1. General"), PropertyOrder(90), Description("The diameter of all the score shafts in mm/10")]
//    public int ScoreShaftDiameter
//    {
//      get{return this.scoreShaftDiameter;}
//      set{this.scoreShaftDiameter=value;}
//    }

//    [Browsable(true), Category("1. General"), PropertyOrder(100), Description("The minimum score gap in tenths of mm, valid for each shaft, each score typoe and each positioning type.")]
//    public int MinimumScoreGap
//    {
//      get { return this.minimumScoreGap; }
//      set { this.minimumScoreGap = value; }
//    }

//    //RFAZZI_FI12/06/2008_3.Added eng. verb. for Trim Chute Offset
//    [Category("1. General"), PropertyOrder(105), Description("The Trim Chute Offset position in 1/10mm")]
//    public int OffsetTrimChute
//    {
//      get { return this.offsetTrimChute; }
//      set { this.offsetTrimChute = value; }
//    }

//    [Category("2. Machine options"), PropertyOrder(110), Description("The total number of robots in the machine")]
//    public Robots RobotCount
//    {
//      get { return this.robotCount; }
//      set { this.robotCount = value; }
//    }

//    [Category("2. Machine options"), PropertyOrder(115), Description("Whether the blades cut from the bottom")]
//    public bool CutFromBottom
//    {
//      get { return this.cutFromBottom; }
//      set { this.cutFromBottom = value; }
//    }

//    [Category("2. Machine options"), PropertyOrder(120), Description("Presence of auxiliary shaft")]
//    public bool AuxPresent
//    {
//      get { return this.auxPresent; }
//      set { this.auxPresent = value; }
//    }

//    [Browsable(false), Category("2. Machine options"), PropertyOrder(130), Description("Presence of anvils on trim slitters")]
//    public bool AnvilsPresent
//    {
//      get { return this.anvilsPresent; }
//      set { this.anvilsPresent = value; }
//    }

//    [Browsable(false), Category("2. Machine options"), PropertyOrder(140), Description("Whether the machine accepts the trim below the minimum")]
//    public bool AcceptNarrowTrim
//    {
//      get { return this.acceptNarrowTrim; }
//      set { this.acceptNarrowTrim = value; }
//    }

//    [Browsable(false), Category("2. Machine options"), PropertyOrder(150), Description("Distance between the blade and the inner edge of the anvil's arm in mm/10")]
//    public int AnvilsArmWidth
//    {
//      get { return this.anvilsArmWidth; }
//      set { this.anvilsArmWidth = value; }
//    }

//    [Browsable(true), Category("2. Machine options"), PropertyOrder(160), Description("The group of the current setup when queue is empty")]
//    public int DefCurrentGroup
//    {
//      get{return this.defCurrentGroup;}
//      set{this.defCurrentGroup=value;}
//    }

//    [Browsable(true), Category("2. Machine options"), PropertyOrder(170), Description("Enabling of the PLC state log on file")]
//    public bool PLCStateLog
//    {
//      get { return this.plcStateLog; }
//      set { this.plcStateLog = value; }
//    }

//    [Browsable(true), Category("2. Machine options"), PropertyOrder(180), Description("Whether is a Pmac400 only in communication with a Fosber Syncro")]
//    public bool CommunicatesWithSyncro
//    {
//      get { return this.communicatesWithSyncro; }
//      set { this.communicatesWithSyncro = value; }
//    }

//    //agrassi_FI30-05-08
//    [Browsable(true), Category("2. Machine options"), PropertyOrder(185), Description("Whether the form for the insertion of the scorers options in the insert orders page is visibile or not")]
//    public bool EnableScorerOptionsInsertion
//    {
//      get { return this.enableScorerOptionsInsertion; }
//      set { this.enableScorerOptionsInsertion = value; }
//    }

//    [Browsable(true), Category("2. Machine options"), PropertyOrder(190), Description("The operator side of the machine")]
//    public MachineOperatorSide OperatorSide
//    {
//      get { return this.operatorSide; }
//      set { this.operatorSide = value; }
//    }

//    //agrassi_FI07-04-08_9
//    [Browsable(true), Category("2. Machine options"), PropertyOrder(195), Description("Minimum distance between the adjacent edges of consecutive slitters to allow the sharpening in mm/10 (for Twin400 set to 0)")]
//    public int SharpeningMinDistance
//    {
//      get { return this.sharpeningMinDistance; }
//      set { this.sharpeningMinDistance = value; }
//    }

//    [Category("3. Board Position and Size"), PropertyOrder(200), Description("If the board center has been determined by the PST is true; if the best board center has to be automatically calculated is false.")]
//    public bool BoardCenterFromPST
//    {
//      get { return this.boardCenterFromPST; }
//      set { this.boardCenterFromPST = value; }
//    }

//    [Category("3. Board Position and Size"), PropertyOrder(210), Description("Distance between the machine's center and board's center in mm/10 (negative if board's center is on the left of the machine's center, otherwise positive)")]
//    public int BoardCenterOffset
//    {
//      get { return this.boardCenterOffset; }
//      set { this.boardCenterOffset = value; }
//    }

//    //umbro 22-08-06
//    //[Category("3. Board Position and Size"), PropertyOrder(220), Description("Minimum runnable web width in mm/10")]
//    //public int MinWebWidth
//    //{
//    //  get { return this.minWebWidth; }
//    //  set { this.minWebWidth = value; }
//    //}

//    [Category("3. Board Position and Size"), PropertyOrder(220), Description("The minimum position of the chutes in mm/10")]
//    public int MinChutesPosition
//    {
//      get { return this.minChutesPosition; }
//      set { this.minChutesPosition = value; }
//    }

//    [Category("3. Board Position and Size"), PropertyOrder(230), ReadOnly(true), Description("Maximum runnable web width in mm/10")]
//    public int MaxWebWidth
//    {
//      get { return this.maxWebWidth; }
//      set { this.maxWebWidth = value; }
//    }

//    //agrassi_FI07-04-08_9
//    //[Category("4. Bidents"), ReadOnly(true), PropertyOrder(240), Description("The width of slit bident in mm/10")]
//    [Category("4. Bidents"), ReadOnly(false), PropertyOrder(240), Description("The width of slit bident in mm/10")]
//    public int SlitBidentWidth
//    {
//      get { return this.slitBidentWidth; }
//      set { this.slitBidentWidth = value; }
//    }

//    //agrassi_FI07-04-08_9
//    //[Category("4. Bidents"), ReadOnly(true), PropertyOrder(250), Description("The width of score bident in mm/10")]
//    [Category("4. Bidents"), ReadOnly(false), PropertyOrder(250), Description("The width of score bident in mm/10")]
//    public int ScoreBidentWidth
//    {
//      get { return this.scoreBidentWidth; }
//      set { this.scoreBidentWidth = value; }
//    }

//    //agrassi_FI07-04-08_9
//    //[Browsable(true), Category("4. Bidents"), ReadOnly(true), PropertyOrder(260), Description("The safety distance between the bident and the slit's grab position in mm/10")]
//    [Browsable(true), Category("4. Bidents"), ReadOnly(false), PropertyOrder(260), Description("The safety distance between the bident and the slit's grab position in mm/10")]
//    public int SlitBidentSafetyDistance
//    {
//      get { return this.slitBidentSafetyDistance; }
//      set { this.slitBidentSafetyDistance = value; }
//    }

//    //agrassi_FI07-04-08_9
//    //[Browsable(true), Category("4. Bidents"), ReadOnly(true), PropertyOrder(270), Description("The safety distance between the bident and the score's grab position in mm/10")]
//    [Browsable(true), Category("4. Bidents"), ReadOnly(false), PropertyOrder(270), Description("The safety distance between the bident and the score's grab position in mm/10")]
//    public int ScoreBidentSafetyDistance
//    {
//      get { return this.scoreBidentSafetyDistance; }
//      set { this.scoreBidentSafetyDistance = value; }
//    }

//    [Category("5. Positioning opitions"), PropertyOrder(280), Description("Positioning options")]
//    public PositOptions PositioningOptions
//    {
//      get{return this.posit;}
//      set{this.posit=value;}
//    }

//    [Category("6. Tools"), PropertyOrder(290), Description("Slit types"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
//    public SlitType[] SlitTypes
//    {
//      get{return this.slitTypes;}
//      set{this.slitTypes=value;}
//    }

//    [Category("6. Tools"), PropertyOrder(300), Description("Score types"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
//    public ScoreType[] ScoreTypes
//    {
//      get{return this.scoreTypes;}
//      set{this.scoreTypes=value;}
//    }

//    [Category("7. Shafts"), PropertyOrder(310), Description("Slit shafts: [0]=shaft 1, [1]=shaft 2"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
//    public SlitShaft[] SlitShafts
//    {
//      get{return this.slitShafts;}
//      set{this.slitShafts=value;}
//    }

//    [Category("7. Shafts"), PropertyOrder(320), Description("Score shafts: [0]=shaft 1, [1]=shaft 2, [2]=aux shaft"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
//    public ScoreShaft[] ScoreShafts
//    {
//      get{return this.scoreShafts;}
//      set{this.scoreShafts=value;}
//    }

//    [Category("8. Plunges' calibration"), PropertyOrder(330), Description("Data for the plunges' calibration")]
//    public Plunges Plunges
//    {
//      get { return this.plunges; }
//      set { this.plunges = value; }
//    }

//    [Category("9. PLC configuration"), PropertyOrder(340), Description("Configuration data for the PLC")]
//    public PlcConfigData PlcConfig
//    {
//      get{return this.plcConfig;}
//      set{this.plcConfig=value;}
//    }

//    public void SetBrokenBlades(string brString)
//    {
//      string[] broken=brString.Split(Command.PARAMETER_SEPARATOR);
//      for (Shaft sh=Shaft.Slit1; sh<=Shaft.Slit2; sh++)
//        this.slitShafts[(int)sh].BrokenBlades=int.Parse(broken[(int)sh]);
//      PageManager.getPageManager().refreshAllFormsData();
//    }

//    public void SetDefaultShaft(string defShaft)
//    {
//      this.defCurrentGroup = int.Parse(defShaft);
//      PageManager.getPageManager().refreshAllFormsData();
//    }

//    public static ShaftGroup GetShaftGroup(int shaftMask)
//    {
//      if ((ShaftUsed(shaftMask, Shaft.Slit1) || ShaftUsed(shaftMask, Shaft.Score1)	|| ShaftUsed(shaftMask, Shaft.ScoreAux))
//        && (!(ShaftUsed(shaftMask, Shaft.Slit2) || ShaftUsed(shaftMask, Shaft.Score2))))
//      {
//        if (ShaftUsed(shaftMask, Shaft.ScoreAux))
//          return ShaftGroup.Group1A;
//        else
//          return ShaftGroup.Group1;
//      }
//      if ((ShaftUsed(shaftMask, Shaft.Slit2) || ShaftUsed(shaftMask, Shaft.Score2))
//        && (!(ShaftUsed(shaftMask, Shaft.Slit1) || ShaftUsed(shaftMask, Shaft.Score1))))
//      {
//        if (ShaftUsed(shaftMask, Shaft.ScoreAux))
//          return ShaftGroup.Group2A;
//        else
//          return ShaftGroup.Group2;
//      }
//      if (!(ShaftUsed(shaftMask, Shaft.Slit1) || ShaftUsed(shaftMask, Shaft.Slit2) || 
//        ShaftUsed(shaftMask, Shaft.Score1) || ShaftUsed(shaftMask, Shaft.Score2) ||
//        ShaftUsed(shaftMask, Shaft.ScoreAux)))
//        return ShaftGroup.NoGroup;
//      return ShaftGroup.BothGroups;
//    }
		
//    public int GetSlitCount()
//    {
//      int count=0;
//      for (int cIdx=0; cIdx<this.slitShafts[0].Configuration.Length; cIdx++)
//      {
//        char c=this.slitShafts[0].Configuration.ToCharArray(cIdx, 1)[0];
//        if (Char.IsDigit(c))
//          count+=(c-'0');
//      }
//      return count;
//    }

//    public bool IsTwin400()
//    {
//      return (this.name == "Twin400");
//    }

//    public char GetSlitType(int shaftIndex, int slitIndex)
//    {
//      int count = 0;
//      for (int cIdx = 0; cIdx < this.slitShafts[shaftIndex].Configuration.Length; cIdx++)
//      {
//        //char n = this.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx, 1)[0];
//        //char c = this.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx+1, 1)[0];
//        char n = ' ';
//        char c = ' ';
//        if (this.slitShafts.Length > shaftIndex)
//        {
//          char[] chars;
//          if (this.slitShafts[shaftIndex].Configuration.Length > cIdx)
//          {
//            chars = this.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx, 1);
//            if (chars.Length > 0)
//              n = chars[0];
//          }
//          if (this.slitShafts[shaftIndex].Configuration.Length > cIdx + 1)
//          {
//            chars = this.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx + 1, 1);
//            if (chars.Length > 0)
//              c = chars[0];
//          }
//        }
//        if (Char.IsDigit(n))
//          count += (n - '0');
//        if (slitIndex < count) return c;
//      }
//      return ' ';
//    }

//    public static int BIT(int n)
//    {
//      int b=1;
//      return b<<=n;
//    }

//    public static string AbbreviateShaftName(string name)
//    {
//      string shortName = name.Substring(0, 2);
//      shortName += name.Substring(3, 1);
//      if (char.IsDigit(name, name.Length - 1))
//        shortName += name.Substring(name.Length - 1);
//      else
//        shortName += name.Substring(name.Length - 3, 1);
//      return shortName;
//    }

//    static bool ShaftUsed(int shaftMask, Shaft sh)
//    {
//      bool used=((shaftMask & BIT((int)sh))>0);
//      return used;
//    }

//    public static int MachShaft(int grpSh, int group)
//    {
//      if (grpSh==2) return grpSh;
//      return Math.Abs(grpSh - group);
//    }

//    public void CheckScoreProfiles(object s, int i)
//    {
//      SlitterScorer ss=(SlitterScorer)s;
////			if (!this.scoreTypes[i].Scorer.ScoreId.Equals(ss.scoreTypes[i].Scorer.ScoreId))
////			{
//        int prIdx;
//        ScorerProfile sp=DBManager.getDBManager().scorerProfile;
//        for (prIdx=0; prIdx<ScorerProfile.PROFILE_COUNT; prIdx++)
//          if (sp.Profiles[prIdx].ScoreId.Equals(ss.scoreTypes[i].Scorer.ScoreId))
//            break;
//        if (prIdx<ScorerProfile.PROFILE_COUNT)
//        {
//          ScoreClass sOld=ss.scoreTypes[i].Scorer.SClass;
//          ss.scoreTypes[i].Scorer.SClass=sp.Profiles[prIdx].SClass;
//          ss.scoreTypes[i].Scorer.Width=sp.Profiles[prIdx].Width;
//          ss.scoreTypes[i].Scorer.ProfileWidth=sp.Profiles[prIdx].ProfileWidth;
//          ss.scoreTypes[i].Scorer.TipHeight=sp.Profiles[prIdx].TipHeight;
//          ss.scoreTypes[i].Scorer.ValleyDepth=sp.Profiles[prIdx].ValleyDepth;
//          ss.scoreTypes[i].Scorer.TipOffset=sp.Profiles[prIdx].TipOffset;
//          ss.scoreTypes[i].Scorer.FarValleyOffset=sp.Profiles[prIdx].FarValleyOffset;
//          ss.scoreTypes[i].Scorer.UpperDiameter=sp.Profiles[prIdx].UpperDiameter;
//          ss.scoreTypes[i].Scorer.LowerDiameter=sp.Profiles[prIdx].LowerDiameter;
//          if (sOld!=ss.scoreTypes[i].Scorer.SClass)
//          {
//            int positCount=ScorerProfileType.PositioningAvailableCount(ss.scoreTypes[i].Scorer.SClass);
//            ss.scoreTypes[i].GapData=new ScoreGapData[positCount];
//            for (int j=0; j<ss.scoreTypes[i].GapData.Length; j++)
//              ss.scoreTypes[i].GapData[j]=new ScoreGapData(ss.scoreTypes[i].Scorer.SClass);
//          }
//        }
////			}
//    }
//	}

	[Description("Compact1 Parameters"), TypeConverter(typeof(Converters))]
	public class Compact1 : SyncroC.DataBase.LineConfig.LineBase
	{
	}

	[Description("Compact2 Parameters"), TypeConverter(typeof(Converters))]
	public class Compact2 : SyncroC.DataBase.LineConfig.LineBase
	{
    //agrassi_FI09-06-2010_2
    //public enum MachineVersion { Ver1 = 1 };
    public enum MachineVersion { Ver0 = 0, Ver1 = 1 };
    public enum MachineHome { Fmt1800 = 27100, Fmt2200 = 32750, Fmt2500 = 36500 };

		MachineVersion plcInterfaceVersion;
		MachineHome homePositionValue;									// Machine Home Position Value
		int homeRollDistance;			  										// lateral distance between Home and roll
		int maxWebWidth;
		bool autoAuxPresent;														// presence of auto auxiliary shaft
		bool kleenKutSlitters;													// presence of Kleen Kut Slitters
		bool offSetScoring;															// presence of OffSet Scoring System
		int bladeGrindOffset;														// distance between blade and grind
		MachineOperatorSide robotsHomeSide;							// Robots Home Side

    //PMASSARO_FI19/06/11 - Compact2: Aggiunto calcolo della quota bocchette come su vecchi supervisori DOS (SID)
    bool calculateTrimChuteAsDOSprograms;             //Use simplified calculation as SID 

		public Compact2()
			: base()
		{
			this.sectionName="SS_COMPACT2";
			//Default values
			this.plcInterfaceVersion = MachineVersion.Ver1;
			this.homePositionValue = MachineHome.Fmt2500;
			this.homeRollDistance = 0;
			this.maxWebWidth = 25000;
			this.autoAuxPresent = false;
			this.kleenKutSlitters = false;
			this.offSetScoring = false;
			this.bladeGrindOffset = 0;
			this.robotsHomeSide = MachineOperatorSide.RightSide;
      //PMASSARO_FI19/06/11 - Compact2: Aggiunto calcolo della quota bocchette come su vecchi supervisori DOS (SID)
      this.calculateTrimChuteAsDOSprograms = false;
		}

    //agrassi_FI09-06-2010_2
		[Category("COMPACT2. General"), PropertyOrder(15), Description("The version of the interface with PLC. Ver0: the manual lubrication is not possible, the lubrication is done togheter with sharpening. Ver1: the manual lubrication is also possible")]
		public MachineVersion PLCInterfaceVersion
		{
			get { return this.plcInterfaceVersion; }
			set { this.plcInterfaceVersion = value; }
		}

		[Category("COMPACT2. General"), PropertyOrder(18), Description("Machine Home Position value related to the machine format")]
		public MachineHome HomePositionValue
		{
			get { return this.homePositionValue; }
			set { this.homePositionValue = value; }
		}

		[Category("COMPACT2. General"), PropertyOrder(20), Description("Lateral distance between Robot Home and the Rolls")]
		public int HomeRollDistance
		{
			get { return this.homeRollDistance; }
			set { this.homeRollDistance = value; }
		}

		[Category("COMPACT2. General"), PropertyOrder(30), Description("Max Web width in mm")]
		public int MaxWebWidth
		{
			get { return this.maxWebWidth; }
			set { this.maxWebWidth = value; }
		}

		[Category("COMPACT2. General"), PropertyOrder(40), Description("Distance between Blade and grinding group in 1/10 mm")]
		public int BladeGrindOffset
		{
			get { return this.bladeGrindOffset; }
			set { this.bladeGrindOffset = value; }
		}


		[Category("COMPACT2. Machine options"), PropertyOrder(130), Description("Presence of automatic auxiliary shaft")]
		public bool AutoAuxPresent
		{
			get { return this.autoAuxPresent; }
			set { this.autoAuxPresent = value; }
		}

		[Category("COMPACT2. Machine options"), PropertyOrder(140), Description("Presence of Kleen Kut Slitters on the machine")]
		public bool KleenKutSlitters
		{
			get { return this.kleenKutSlitters; }
			set { this.kleenKutSlitters = value; }
		}

		[Category("COMPACT2. Machine options"), PropertyOrder(150), Description("Presence of OffSet Scoring System for Point to Point positioning")]
		public bool OffSetScoring
		{
			get { return this.offSetScoring; }
			set { this.offSetScoring = value; }
		}

		[Category("COMPACT2. Machine options"), PropertyOrder(160), Description("The machine side of the robots related to the runnig paper")]
		public MachineOperatorSide RobotsHomeSide
		{
			get { return this.robotsHomeSide; }
			set { this.robotsHomeSide = value; }
		}

    //PMASSARO_FI19/06/11 - Compact2: Aggiunto calcolo della quota bocchette come su vecchi supervisori DOS (SID)
    [Category("COMPACT2. Machine options"), PropertyOrder(130), Description("TRUE when SYNCRO sobstitute a DOS Compact Supervisor (Es. SID)")]
    public bool CalculateTrimChuteAsDOSprograms
    {
      get { return this.calculateTrimChuteAsDOSprograms; }
      set { this.calculateTrimChuteAsDOSprograms = value; }
    }

	}

	[Description("Twin1 Parameters"), TypeConverter(typeof(Converters))]
	public class Twin1 : SyncroC.DataBase.LineConfig.LineBase
	{
	}

	[Description("Twin2 Parameters"), TypeConverter(typeof(Converters))]
	public class Twin2 : SyncroC.DataBase.LineConfig.LineBase
	{
    //agrassi_FI25-05-2011_1
    public enum MachineVersion { Ver0 = 0 };

    MachineVersion plcInterfaceVersion;
    int maxWebWidth_UDmm;														// Max Web Width
    bool kleenKutPresent;														// presence of KK Slitters
    int bladeGrindOffset_UDdmm;											// Offset between blade and Grind
    int shearToAuxNearDist_UDmm;
    int shearToAuxFarDist_UDmm;
    int shearToScoreNearDist_UDmm;
    int shearToScoreFarDist_UDmm;
    int shearToSlitNearDist_UDmm;
    int shearToSlitFarDist_UDmm;
    int shearToChutesDist_UDmm;
    int shearToWebDirDist_UDmm;
    int webDirAnticipation_UTms;
    int numAuxShaft;
    int slitLowerTime_UTms;
    int slitRaiseTime_UTms;
    int scoreLowerTime_UTms;
    int scoreRaiseTime_UTms;
    int originSlits;													
    int originScores;
    int rifMeasures;
    int penetration;
    bool oldStyleKKDataToPLC;
    int lubesBetweenGrindings;                      //numero di lubrificazioni tra due affilature consecutive (usato solo se oldStyleKKDataToPLC=false)
    int supportScoreGap;
    int scoreGapResolution;
    int slitToScoreOffset;
    double mmPerPulseMW;

    public Twin2(): base()
		{
			this.sectionName="SS_TWIN2";
			//Default values
			this.plcInterfaceVersion = MachineVersion.Ver0;
      this.maxWebWidth_UDmm = 25500;
      this.kleenKutPresent = false;
		}

    [Category("1 - TWIN2. General"), PropertyOrder(10), Description("The machine side of the robots related to the runnig paper")]
    public MachineVersion PLCInterfaceVersion
    {
      get { return this.plcInterfaceVersion; }
      set { this.plcInterfaceVersion = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(15), Description("Posizione di appacchettamento dei coltelli in decimi di mm")]
    public int OriginSlits
    {
      get { return this.originSlits; }
      set { this.originSlits = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(20), Description("Posizione di appacchettamento dei cordoni in decimi di mm")]
    public int OriginScores
    {
      get { return this.originScores; }
      set { this.originScores = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(25), Description("Riferimento in decimi di mm per il posizionamento degli utensili in lavoro e di quelii che vengono portati nel magazzino destro; si applica anche alle posizioni dei coltelli per l'affilatura.")]
    public int RifMeasures
    {
      get { return this.rifMeasures; }
      set { this.rifMeasures = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(30), Description("Massima bobina lavorabile")]
    public int MaxWebWidth_UDmm
    {
      get { return this.maxWebWidth_UDmm; }
      set { this.maxWebWidth_UDmm = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(40), Description("Offset per l'affilatura")]
    public int BladeGrindOffset_UDdmm
    {
      get { return this.bladeGrindOffset_UDdmm; }
      set { this.bladeGrindOffset_UDdmm = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(50), Description("Compenetrazione coltelli")]
    public int Penetration
    {
      get { return this.penetration; }
      set { this.penetration = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(60), Description("Numero di lubrificazioni tra due affilature consecutive")]
    public int LubesBetweenGrindings
    {
      get { return this.lubesBetweenGrindings; }
      set { this.lubesBetweenGrindings = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(70), Description("Gap di cordonatura quando ci sono i cordoni di supporto")]
    public int SupportScoreGap
    {
      get { return this.supportScoreGap; }
      set { this.supportScoreGap = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(80), Description("Risoluzione encoder bielle")]
    public int ScoreGapResolution
    {
      get { return this.scoreGapResolution; }
      set { this.scoreGapResolution = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(85), Description("Spostamento coltelli in decimi di mm")]
    public int SlitToScoreOffset
    {
      get { return this.slitToScoreOffset; }
      set { this.slitToScoreOffset = value; }
    }

    [Category("1 - TWIN2. General"), PropertyOrder(88), Description("Risoluzione ruota di misura per il Twin2 in mm/pulse (se diversa da quella di Syncro)")]
    public double MMPerPulseMW
    {
      get { return this.mmPerPulseMW; }
      set { this.mmPerPulseMW = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(90), Description("Distanza Rotary-primo asse ausiliario")]
    public int ShearToAuxNearDist_UDmm
    {
      get { return this.shearToAuxNearDist_UDmm; }
      set { this.shearToAuxNearDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(100), Description("Distanza Rotary-secondo asse ausiliario")]
    public int ShearToAuxFarDist_UDmm
    {
      get { return this.shearToAuxFarDist_UDmm; }
      set { this.shearToAuxFarDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(110), Description("Distanza Rotary-primo asse cordoni")]
    public int ShearToScoreNearDist_UDmm
    {
      get { return this.shearToScoreNearDist_UDmm; }
      set { this.shearToScoreNearDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(120), Description("Distanza Rotary-secondo asse cordoni")]
    public int ShearToScoreFarDist_UDmm
    {
      get { return this.shearToScoreFarDist_UDmm; }
      set { this.shearToScoreFarDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(130), Description("Distanza Rotary-primo asse coltelli")]
    public int ShearToSlitNearDist_UDmm
    {
      get { return this.shearToSlitNearDist_UDmm; }
      set { this.shearToSlitNearDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(140), Description("Distanza Rotary-secondo asse coltelli")]
    public int ShearToSlitFarDist_UDmm
    {
      get { return this.shearToSlitFarDist_UDmm; }
      set { this.shearToSlitFarDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(150), Description("Distanza Rotary-bocchette")]
    public int ShearToChutesDist_UDmm
    {
      get { return this.shearToChutesDist_UDmm; }
      set { this.shearToChutesDist_UDmm = value; }
    }

    [Category("2 - TWIN2. Distances"), PropertyOrder(160), Description("Distanza Rotary-web director")]
    public int ShearToWebDirDist_UDmm
    {
      get { return this.shearToWebDirDist_UDmm; }
      set { this.shearToWebDirDist_UDmm = value; }
    }

    [Category("3 - TWIN2. Times"), PropertyOrder(170), Description("Anticipo web director")]
    public int WebDirAnticipation_UTms
    {
      get { return this.webDirAnticipation_UTms; }
      set { this.webDirAnticipation_UTms = value; }
    }

    [Category("3 - TWIN2. Times"), PropertyOrder(180), Description("Anticipo entrata in lavoro coltelli")]
    public int SlitLowerTime_UTms
    {
      get { return this.slitLowerTime_UTms; }
      set { this.slitLowerTime_UTms = value; }
    }

    [Category("3 - TWIN2. Times"), PropertyOrder(190), Description("Anticipo uscita dal lavoro coltelli")]
    public int SlitRaiseTime_UTms
    {
      get { return this.slitRaiseTime_UTms; }
      set { this.slitRaiseTime_UTms = value; }
    }

    [Category("3 - TWIN2. Times"), PropertyOrder(200), Description("Anticipo entrata in lavoro cordoni")]
    public int ScoreLowerTime_UTms
    {
      get { return this.scoreLowerTime_UTms; }
      set { this.scoreLowerTime_UTms = value; }
    }

    [Category("3 - TWIN2. Times"), PropertyOrder(210), Description("Anticipo uscita dal lavoro cordoni")]
    public int ScoreRaiseTime_UTms
    {
      get { return this.scoreRaiseTime_UTms; }
      set { this.scoreRaiseTime_UTms = value; }
    }

    [Category("4 - TWIN2. Options"), PropertyOrder(220), Description("Presenza del sistema di affilatura e lubrificazione")]
    public bool KleenKutPresent
    {
      get { return this.kleenKutPresent; }
      set { this.kleenKutPresent = value; }
    }

    [Category("4 - TWIN2. Options"), PropertyOrder(230), Description("Invio parametri KK al PLC nel vecchio modo (p. es Barco e Carmel)")]
    public bool OldStyleKKDataToPLC
    {
      get { return this.oldStyleKKDataToPLC; }
      set { this.oldStyleKKDataToPLC = value; }
    }

    [Category("4 - TWIN2. Options"), PropertyOrder(240), Description("Numero di assi ausiliari (1 o 2)")]
    public int NumAuxShaft
    {
      get { return this.numAuxShaft; }
      set { this.numAuxShaft = value; }
    }
  }

  //[Description("TwinE Parameters"), TypeConverter(typeof(Converters))]
  //public class TwinE : SyncroC.DataBase.LineConfig.LineBase
  //{

  //}

	[Description("Twin400 Parameters"), TypeConverter(typeof(Converters))]
	public class Twin400 : SyncroC.DataBase.LineConfig.LineBase
	{
		public enum MachineVersion { Ver1 = 1, Ver2 = 2, Ver3 = 3, Ver4 = 4, Ver5 = 5, Ver6 = 6 };
		public enum MachineFormat { Fmt2500 = 2500, Fmt2800 = 2800 };
		public enum Robots { OneRobots = 1, TwoRobots = 2, ThreeRobots = 3/*, FiveRobots=5*/};

		MachineVersion plcInterfaceVersion;
		MachineFormat format;			  										//machine's format
		Robots robotCount;															//number of robots (1 or 2 for Twin Ev, 3 for Twin 400)
		bool cutFromBottom;
		bool anvilsPresent;
		bool acceptNarrowTrim;													//if true it accepts the trim below the minimum
		int anvilsArmWidth;															//distance between the blade and the inner edge of the anvil's arm
		int sharpeningMinDistance;											//minimum distance between the adjacent edges of consecutive slitters to allow the sharpening
		bool boardCenterFromPST;												//true if board center has been determined, otherwise the best board center will be automatically be chosen
		int kBladeConsumption;                          //radius reduction because of a sharpening (in micrometers)
		int scoreShaftDiameter;
		int slitSideMargin;															//margin for the external limit positions of the slits in mm/10
		//RFAZZI_FI10/11/2008_1. Splitted the "SafetySlitDistance" for side
		//int safetySlitsDistSides;
		int safetySlitsDistLeftSides;
		int safetySlitsDistRightSides;
		int sideframesDistance;													//distance between the sideframes of the machine in mm/10
    //agrassi_FI12-02-09
    //int defCurrentGroup;														//group of the current setup when queue is empty
    Group defCurrentGroup;														//group of the current setup when queue is empty
    bool plcStateLog;
		bool communicatesWithSyncro;
		bool enableScorerOptionsInsertion;							//makes visible the scorer option form in the form of the insertion of orders
		MachineOperatorSide operatorSide;
		Twin400Plunges plunges;                                //data for the plunges' calibration
		Twin400PlcConfigData plcConfig;

		public Twin400():base()
		{
			this.sectionName="SS_TWIN400";
			//Default values
      this.plcInterfaceVersion = MachineVersion.Ver6;
      this.format=MachineFormat.Fmt2500;
      this.robotCount=Robots.ThreeRobots;
      this.cutFromBottom = false;
			this.anvilsPresent=false;
			this.acceptNarrowTrim=false;
			this.anvilsArmWidth=0;
			this.sharpeningMinDistance=0;
      this.boardCenterFromPST = false;
      this.kBladeConsumption = 0;
			this.scoreShaftDiameter = 0;
      this.slitSideMargin=0;
			//RFAZZI_FI10/11/2008_1. Splitted the "SafetySlitDistance" for side
			//this.safetySlitsDistSides = 0;
			this.safetySlitsDistLeftSides = 0;
			this.safetySlitsDistRightSides = 0;
			this.sideframesDistance = 0;
      this.defCurrentGroup = 0;
      this.plcStateLog = false;
      this.communicatesWithSyncro = false;
      this.enableScorerOptionsInsertion=false;							//makes visible the scorer option form in the form of the insertion of orders
			this.operatorSide = MachineOperatorSide.RightSide;
			this.plunges = new Twin400Plunges();
			this.plcConfig = new Twin400PlcConfigData();
		}


    [Category("TWIN400. General"), PropertyOrder(15), Description("The version of the interface with PLC: from Ver1 to Ver4 for Twin400 V0, form Ver5 for Twin400 V1")]
    public MachineVersion PLCInterfaceVersion
    {
      get { return this.plcInterfaceVersion; }
      set { this.plcInterfaceVersion = value; }
    }

    [Category("TWIN400. General"), PropertyOrder(20), Description("The format of the machine")]
    public MachineFormat Format
		{
			get{return this.format;}
			set{this.format=value;}
		}

		[Browsable(false), Category("TWIN400. General"), ReadOnly(false), PropertyOrder(33), Description("The distance between the sideframes of the machine in mm/10")]
		public int SideframesDistance
		{
			get { return this.sideframesDistance; }
			set { this.sideframesDistance = value; }
		}

    [Category("TWIN400. General"), ReadOnly(false), PropertyOrder(36), Description("The margin for the external limit positions of the slits in mm/10")]
    public int SlitSideMargin
    {
      get { return this.slitSideMargin; }
      set { this.slitSideMargin = value; }
    }

		//RFAZZI_FI10/11/2008_1. Splitted the "SafetySlitDistance" for side
		//[ReadOnly(true), PropertyOrder(40), Category("TWIN400. General"), Description("The safety distance between the tools and the shaft's edge in mm/10")]
		//public int SafetySlitsDistSides
		//{
		//  get { return this.safetySlitsDistSides; }
		//  set { this.safetySlitsDistSides = value; }
		//}
		[ReadOnly(true), PropertyOrder(40), Category("TWIN400. General"), Description("The safety distance between the tools and the shaft's edge on the left side in mm/10")]
		public int SafetySlitsDistLeftSides
		{
			get { return this.safetySlitsDistLeftSides; }
			set { this.safetySlitsDistLeftSides = value; }
		}

		[ReadOnly(true), PropertyOrder(50), Category("TWIN400. General"), Description("The safety distance between the tools and the shaft's edge on the right side in mm/10")]
		public int SafetySlitsDistRightSides
		{
			get { return this.safetySlitsDistRightSides; }
			set { this.safetySlitsDistRightSides = value; }
		}

    [Browsable(false), Category("TWIN400. General"), PropertyOrder(80), Description("The radius reduction of a blade because of a sharpening in \u03BCm")]
    public int KBladeConsumption
    {
      get { return this.kBladeConsumption; }
      set { this.kBladeConsumption = value; }
    }

    [Browsable(false), Category("TWIN400. General"), PropertyOrder(90), Description("The diameter of all the score shafts in mm/10")]
    public int ScoreShaftDiameter
		{
			get{return this.scoreShaftDiameter;}
			set{this.scoreShaftDiameter=value;}
		}

		[Browsable(true), Category("TWIN400. General"), PropertyOrder(100), Description("The operator side of the machine")]
		public MachineOperatorSide OperatorSide
		{
			get { return this.operatorSide; }
			set { this.operatorSide = value; }
		}

    [Browsable(false), Category("TWIN400. Machine options"), PropertyOrder(110), Description("The total number of robots in the machine")]
    public Robots RobotCount
    {
      get { return this.robotCount; }
      set { this.robotCount = value; }
    }

    [Category("TWIN400. Machine options"), PropertyOrder(115), Description("Whether the blades cut from the bottom")]
    public bool CutFromBottom
    {
      get { return this.cutFromBottom; }
      set { this.cutFromBottom = value; }
    }

    [Browsable(false), Category("TWIN400. Machine options"), PropertyOrder(130), Description("Presence of anvils on trim slitters")]
    public bool AnvilsPresent
    {
      get { return this.anvilsPresent; }
      set { this.anvilsPresent = value; }
    }

    [Browsable(false), Category("TWIN400. Machine options"), PropertyOrder(140), Description("Whether the machine accepts the trim below the minimum")]
    public bool AcceptNarrowTrim
    {
      get { return this.acceptNarrowTrim; }
      set { this.acceptNarrowTrim = value; }
    }

    [Browsable(false), Category("TWIN400. Machine options"), PropertyOrder(150), Description("Distance between the blade and the inner edge of the anvil's arm in mm/10")]
    public int AnvilsArmWidth
    {
      get { return this.anvilsArmWidth; }
      set { this.anvilsArmWidth = value; }
    }

    [Browsable(true), Category("TWIN400. Machine options"), PropertyOrder(160), Description("The group of the current setup when queue is empty")]
    //agrassi_FI12-02-09
    //public int DefCurrentGroup
    public Group DefCurrentGroup
    {
			get{return this.defCurrentGroup;}
			set{this.defCurrentGroup=value;}
		}

    [Browsable(false), Category("TWIN400. Machine options"), PropertyOrder(170), Description("Enabling of the PLC state log on file")]
    public bool PLCStateLog
    {
      get { return this.plcStateLog; }
      set { this.plcStateLog = value; }
    }

    [Browsable(true), Category("TWIN400. Machine options"), PropertyOrder(180), Description("Used only for Pmac400 stand-alone. True if the supervisor is Fosber Syncro2 or Spde; False otherwise.")]
    public bool CommunicatesWithSyncro
    {
      get { return this.communicatesWithSyncro; }
      set { this.communicatesWithSyncro = value; }
    }

    [Browsable(true), Category("TWIN400. Machine options"), PropertyOrder(185), Description("Whether the form for the insertion of the scorers options in the insert orders page is visibile or not")]
    public bool EnableScorerOptionsInsertion
    {
      get { return this.enableScorerOptionsInsertion; }
      set { this.enableScorerOptionsInsertion = value; }
    }

    [Browsable(true), Category("TWIN400. Machine options"), PropertyOrder(195), Description("Minimum distance between the adjacent edges of consecutive slitters to allow the sharpening in mm/10 (for Twin400 set to 0)")]
    public int SharpeningMinDistance
    {
      get { return this.sharpeningMinDistance; }
      set { this.sharpeningMinDistance = value; }
    }

    [Category("TWIN400. Board Position and Size"), PropertyOrder(200), Description("If the board center has been determined by the PST is true; if the best board center has to be automatically calculated is false.")]
    public bool BoardCenterFromPST
    {
      get { return this.boardCenterFromPST; }
      set { this.boardCenterFromPST = value; }
    }

    [Browsable(false), Category("TWIN400. Plunges' calibration"), PropertyOrder(330), Description("Data for the plunges' calibration")]
		public Twin400Plunges Plunges
    {
      get { return this.plunges; }
      set { this.plunges = value; }
    }

    [Category("TWIN400. PLC configuration"), PropertyOrder(340), Description("Configuration data for the PLC")]
		public Twin400PlcConfigData PlcConfig
		{
			get{return this.plcConfig;}
			set{this.plcConfig=value;}
		}

    //agrassi_FI03-01-2010_2
    public void SetDefaultShaft(string defShaft)
    {
      this.defCurrentGroup = (Group)int.Parse(defShaft);
      PageManager.getPageManager().refreshAllFormsData();
    }
	}

	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
	[TypeConverter(typeof(Converters))]
	public class SlitterScorerHeader : SyncroC.DataBase.LineConfig.LineBase
	{
		public const int MAX_SLIT_TYPES_MACHINE = 4;
		public const int MAX_SCORE_TYPES_MACHINE = 10;
		public const int NUM_SLIT_SHAFTS = 2;
		public const int MAX_NUM_SCORE_SHAFTS = 3;
		public const int MAX_SLITS_PER_SHAFT = 10;
		public const int MAX_SCORES_PER_SHAFT = 36;
		public const int SS_SETUPS = 3;
		public const int OLD_SS_SETUPS = 1;
		public const int MAINTENANCE_SETUP_ID = 999;
		public const int TEFLON_SETUP_ID = 998;

		public string name;																		//machine's name
		public int width;																			//width of the shafts
		public int maxWebWidth;																//maximum web width (it depends on the scores configuration)
		public int maxTrim;																		//maximum trim width
		public int minTrim;																		//minimum trim width
		public int minimumScoreGap;
		public int minChutesPosition;
		public int offsetTrimChute;
		public int boardCenterOffset;													//distance between the machine's center and board's center (negative if board's center is on the left of the machine's center, otherwise positive)
		public bool auxPresent;																//presence of auxiliary shaft

		//PMASSARO_FI04/11/08 - In case of OldSS and external control is not possible accept 3 orders on same level
		public bool machineControlIntegratedInSyncro;								//true if the control of the machine is intergrated in Syncro		
		
		public int slitBidentWidth;
		public int scoreBidentWidth;
		public int slitBidentSafetyDistance;
		public int scoreBidentSafetyDistance;
		public SlitType[] slitTypes;														//letter, width, ...
		public ScoreType[] scoreTypes;													//letter, scoreClass. ...
		public SlitShaft[] slitShafts;													//config, offset
		public ScoreShaft[] scoreShafts;												//config, leftScores, ....
		PositOptions posit;																			//SlitPositOptions, ...

		public SlitterScorerHeader()
			: base()
		{
			this.sectionName="SS_HEADER";
			//Default values
			this.name=" ";
			this.width=0;
      this.maxWebWidth=0;
			this.maxTrim=0;
			this.minTrim = 0;
			this.minimumScoreGap = 0;
			this.minChutesPosition = 0;
			this.offsetTrimChute = 0;
			this.boardCenterOffset = 0;
			this.auxPresent = false;

			//PMASSARO_FI04/11/08 - In case of OldSS and external control is not possible accept 3 orders on same level
			this.machineControlIntegratedInSyncro = false;								//true if the control of the machine is intergrated in Syncro
			
			this.slitBidentWidth = 0;
			this.scoreBidentWidth = 0;
			this.slitBidentSafetyDistance = 0;
			this.scoreBidentSafetyDistance = 0;
			this.slitTypes = new SlitType[MAX_SLIT_TYPES_MACHINE];
			for (int i=0; i<MAX_SLIT_TYPES_MACHINE; i++)
				this.slitTypes[i]=new SlitType();
			this.scoreTypes=new ScoreType[MAX_SCORE_TYPES_MACHINE];
			for (int i=0; i<MAX_SCORE_TYPES_MACHINE; i++)
				this.scoreTypes[i]=new ScoreType();
			this.slitShafts=new SlitShaft[NUM_SLIT_SHAFTS];
			for (int i=0; i<NUM_SLIT_SHAFTS; i++)
				this.slitShafts[i]=new SlitShaft();
			this.scoreShafts=new ScoreShaft[MAX_NUM_SCORE_SHAFTS];
			for (int i=0; i<MAX_NUM_SCORE_SHAFTS; i++)
				this.scoreShafts[i]=new ScoreShaft();
			this.posit = new PositOptions();

		}

		[Category("1. General"), PropertyOrder(10), TypeConverter(typeof(SSNameConverter)), Description("The name of the machine")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		//PMASSARO_FI04/11/08 - In case of OldSS and external control is not possible accept 3 orders on same level
		[Category("1. General"), PropertyOrder(20), Description("Usato solo per il Compact. True if Syncro is the Software Supervisor of the machine (False in case is present a SID or PMACDOS etc etc)")]
		public bool MachineControlIntegratedInSyncro
		{
			get { return this.machineControlIntegratedInSyncro; }
			set { this.machineControlIntegratedInSyncro = value; }
		}

		//RFAZZI_FI29/02/2008_2.Created new Twin400 PLC Protocol Version for WideBody and TopSlitter
		[Category("1. General"), ReadOnly(false), PropertyOrder(30), TypeConverter(typeof(SSWidthConverter)), Description("The width of score shafts used by the scorers in mm/10; it is 34500 for the Twin400 Standard Body, 37500 for the Twin400 Wide Body and 35000 for the TwinE and Twin2.")]
		public int MachineWidth
		{
			get { return this.width; }
			set { this.width = value; }
		}

		[Category("1. General"), PropertyOrder(60), Description("Maximum total trim width in mm/10")]
		public int MaxTrim
		{
			get { return this.maxTrim; }
			set { this.maxTrim = value; }
		}

		[Category("1. General"), PropertyOrder(70), Description("Minimum total trim width in mm/10")]
		public int MinTrim
		{
			get { return this.minTrim; }
			set { this.minTrim = value; }
		}

		[Browsable(true), Category("1. General"), PropertyOrder(100), Description("The minimum score gap in tenths of a mm: this value will be multiplied by 2 for positionings with compenetration, will be divided by 1.33 for point-to-point positionings.")]
		public int MinimumScoreGap
		{
			get { return this.minimumScoreGap; }
			set { this.minimumScoreGap = value; }
		}

    //agrassi_FI16-09-2010_1
		[Category("1. General"), PropertyOrder(105), Description("The Trim Chute span offset in 1/10mm: insert the value related to one side")]
		public int OffsetTrimChute
		{
			get { return this.offsetTrimChute; }
			set { this.offsetTrimChute = value; }
		}

		[Category("1. General"), PropertyOrder(120), Description("Presence of auxiliary shaft")]
		public bool AuxPresent
		{
			get { return this.auxPresent; }
			set { this.auxPresent = value; }
		}

		[Category("1. General"), PropertyOrder(130), Description("The width of slit bident in mm/10")]
		public int SlitBidentWidth
		{
			get { return this.slitBidentWidth; }
			set { this.slitBidentWidth = value; }
		}

		[Category("1. General"), PropertyOrder(135), Description("The safety distance between the bident and the slit's grab position in mm/10")]
		public int SlitBidentSafetyDistance
		{
			get { return this.slitBidentSafetyDistance; }
			set { this.slitBidentSafetyDistance = value; }
		}

		[Category("1. General"), PropertyOrder(140), Description("The safety distance between the bident and the score's grab position in mm/10")]
		public int ScoreBidentSafetyDistance
		{
			get { return this.scoreBidentSafetyDistance; }
			set { this.scoreBidentSafetyDistance = value; }
		}

		[Category("1. General"), PropertyOrder(145), Description("The width of score bident in mm/10")]
		public int ScoreBidentWidth
		{
			get { return this.scoreBidentWidth; }
			set { this.scoreBidentWidth = value; }
		}

		[Category("2. Board Position and Size"), PropertyOrder(210), Description("Distance between the machine's center and board's center in mm/10 (negative if board's center is on the left of the machine's center, otherwise positive)")]
		public int BoardCenterOffset
		{
			get { return this.boardCenterOffset; }
			set { this.boardCenterOffset = value; }
		}

		[Category("2. Board Position and Size"), PropertyOrder(220), Description("The minimum position of the chutes in mm/10")]
		public int MinChutesPosition
		{
			get { return this.minChutesPosition; }
			set { this.minChutesPosition = value; }
		}


		[Category("2. Board Position and Size"), PropertyOrder(230), ReadOnly(true), Description("Maximum runnable web width in mm/10")]
		public int MaxWebWidth
		{
			get { return this.maxWebWidth; }
			set { this.maxWebWidth = value; }
		}

		[Category("3. Tools"), PropertyOrder(290), Description("Slit types"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public SlitType[] SlitTypes
		{
			get { return this.slitTypes; }
			set { this.slitTypes = value; }
		}

		[Category("3. Tools"), PropertyOrder(300), Description("Score types"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public ScoreType[] ScoreTypes
		{
			get { return this.scoreTypes; }
			set { this.scoreTypes = value; }
		}

		[Category("4. Shafts"), PropertyOrder(310), Description("Slit shafts: [0]=shaft 1, [1]=shaft 2"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public SlitShaft[] SlitShafts
		{
			get { return this.slitShafts; }
			set { this.slitShafts = value; }
		}

		[Category("4. Shafts"), PropertyOrder(320), Description("Score shafts: [0]=shaft 1, [1]=shaft 2, [2]=aux shaft"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public ScoreShaft[] ScoreShafts
		{
			get { return this.scoreShafts; }
			set { this.scoreShafts = value; }
		}

		[Category("5. Positioning opitions"), PropertyOrder(330), Description("Positioning options")]
		public PositOptions PositioningOptions
		{
			get { return this.posit; }
			set { this.posit = value; }
		}

    //agrassi_07-01-09
    public bool IsScorerUsed(char scorerLetter)
    {
      for (int s = 0; s < MAX_NUM_SCORE_SHAFTS-(this.auxPresent?0:1); s++)
      {
        if (this.scoreShafts[s].Configuration.IndexOf(scorerLetter) >= 0)
          return true;
      }
      return false;
    }
  }

	[TypeConverter(typeof(Converters))]
	public class SlitterScorer : SyncroC.DataBase.LineConfig.LineBase
	{

		SlitterScorerHeader slitterScorerHeader;
		Compact2 compact2;
    //agrassi_FI25-05-2011_1
    Twin2 twin2;
		//TwinE twinE;
		Twin400 twin400;


		/// <summary>
		/// Builder.
		/// </summary>
		/// <param name="number"></param>
		public SlitterScorer():base()
		{
			//Default values
			this.slitterScorerHeader = new SlitterScorerHeader();
			this.compact2 = new Compact2();
      //agrassi_FI25-05-2011_1
      this.twin2=new Twin2();
			//this.twinE = new TwinE();
			this.twin400 = new Twin400();
		}


		//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
		public bool isTwin400Wide()
		{
			return this.slitterScorerHeader.width == 37500;
		}

		[Category("1. General Parameters"), PropertyOrder(1), Description("Machine's general parameters")]
		public SlitterScorerHeader SlitterScorerHeader
		{
			get { return this.slitterScorerHeader; }
			set { this.slitterScorerHeader = value; }
		}

		[Category("5. Machine's Dedicated Parameters"), PropertyOrder(340), Description("Compact2 Settings")]
		public Compact2 Compact2
		{
			get { return this.compact2; }
			set { this.compact2 = value; }
		}

    //agrassi_FI25-05-2011_1
    [Category("5. Machine's Dedicated Parameters"), PropertyOrder(350), Description("Twin2 Settings")]
    public Twin2 Twin2
    {
      get { return this.twin2; }
      set { this.twin2 = value; }
    }

		//[Category("5. Machine's Dedicated Parameters"), PropertyOrder(370), Description("TwinE Settings")]
		//public TwinE TwinE
		//{
		//  get { return this.twinE; }
		//  set { this.twinE = value; }
		//}

		[Category("5. Machine's Dedicated Parameters"), PropertyOrder(380), Description("Twin400 Settings")]
		public Twin400 Twin400
		{
			get { return this.twin400; }
			set { this.twin400 = value; }
		}


		public void SetBrokenBlades(string brString)
		{
			string[] broken=brString.Split(Command.PARAMETER_SEPARATOR);
			for (Shaft sh=Shaft.Slit1; sh<=Shaft.Slit2; sh++)
				this.slitterScorerHeader.slitShafts[(int)sh].BrokenBlades = int.Parse(broken[(int)sh]);
			PageManager.getPageManager().refreshAllFormsData();
		}

    public static ShaftGroup GetShaftGroup(int shaftMask)
		{
			if ((ShaftUsed(shaftMask, Shaft.Slit1) || ShaftUsed(shaftMask, Shaft.Score1)	|| ShaftUsed(shaftMask, Shaft.ScoreAux))
				&& (!(ShaftUsed(shaftMask, Shaft.Slit2) || ShaftUsed(shaftMask, Shaft.Score2))))
      {
        if (ShaftUsed(shaftMask, Shaft.ScoreAux))
          return ShaftGroup.Group1A;
        else
          return ShaftGroup.Group1;
      }
			if ((ShaftUsed(shaftMask, Shaft.Slit2) || ShaftUsed(shaftMask, Shaft.Score2))
				&& (!(ShaftUsed(shaftMask, Shaft.Slit1) || ShaftUsed(shaftMask, Shaft.Score1))))
      {
        if (ShaftUsed(shaftMask, Shaft.ScoreAux))
          return ShaftGroup.Group2A;
        else
          return ShaftGroup.Group2;
      }
      if (!(ShaftUsed(shaftMask, Shaft.Slit1) || ShaftUsed(shaftMask, Shaft.Slit2) || 
				ShaftUsed(shaftMask, Shaft.Score1) || ShaftUsed(shaftMask, Shaft.Score2) ||
				ShaftUsed(shaftMask, Shaft.ScoreAux)))
        return ShaftGroup.NoGroup;
      return ShaftGroup.BothGroups;
		}
		
		public int GetSlitCount()
		{
			int count=0;
			for (int cIdx = 0; cIdx < this.slitterScorerHeader.slitShafts[0].Configuration.Length; cIdx++)
			{
				char c = this.slitterScorerHeader.slitShafts[0].Configuration.ToCharArray(cIdx, 1)[0];
				if (Char.IsDigit(c))
					count+=(c-'0');
			}
			return count;
		}

    public bool IsTwin400()
    {
			return (this.slitterScorerHeader.name == "Twin400");
    }

    public bool IsTwin400v1()
    {
      return (this.slitterScorerHeader.name == "Twin400" && this.Twin400.PLCInterfaceVersion > Twin400.MachineVersion.Ver4 );
    }

		public bool IsTwinE()
		{
			return (this.slitterScorerHeader.name == "TwinE");
		}

		public bool IsCompact2()
		{
			return (this.slitterScorerHeader.name == "Compact2");
		}

    //agrassi_FI25-05-2011_1
    public bool IsTwin2()
    {
      return (this.slitterScorerHeader.name == "Twin2");
    }
    
    public char GetSlitType(int shaftIndex, int slitIndex)
    {
      int count = 0;
			for (int cIdx = 0; cIdx < this.slitterScorerHeader.slitShafts[shaftIndex].Configuration.Length; cIdx++)
      {
				//char n = this.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx, 1)[0];
				//char c = this.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx+1, 1)[0];
				char n = ' ';
				char c = ' ';
				if (this.slitterScorerHeader.slitShafts.Length > shaftIndex)
				{
					char[] chars;
					if (this.slitterScorerHeader.slitShafts[shaftIndex].Configuration.Length > cIdx)
					{
						chars = this.slitterScorerHeader.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx, 1);
						if (chars.Length > 0)
							n = chars[0];
					}
					if (this.slitterScorerHeader.slitShafts[shaftIndex].Configuration.Length > cIdx + 1)
					{
						chars = this.slitterScorerHeader.slitShafts[shaftIndex].Configuration.ToCharArray(cIdx + 1, 1);
						if (chars.Length > 0)
							c = chars[0];
					}
				}
        if (Char.IsDigit(n))
          count += (n - '0');
        if (slitIndex < count) return c;
      }
      return ' ';
    }

    public static int BIT(int n)
		{
			int b=1;
			return b<<=n;
		}

    public static string AbbreviateShaftName(string name)
    {
      string shortName = name.Substring(0, 2);
      shortName += name.Substring(3, 1);
      if (char.IsDigit(name, name.Length - 1))
        shortName += name.Substring(name.Length - 1);
      else
        shortName += name.Substring(name.Length - 3, 1);
      return shortName;
    }

    static bool ShaftUsed(int shaftMask, Shaft sh)
		{
			bool used=((shaftMask & BIT((int)sh))>0);
			return used;
		}

    public static int MachShaft(int grpSh, int group)
    {
      if (grpSh==2) return grpSh;
      return Math.Abs(grpSh - group);
    }

    public void CheckScoreProfiles(object s, int i)
		{
      //agrassi_FI04-12-08
      //SlitterScorer ss = (SlitterScorer)s;
      //int prIdx;
      //ScorerProfile sp = DBManager.getDBManager().scorerProfile;
      //for (prIdx = 0; prIdx < ScorerProfile.PROFILE_COUNT; prIdx++)
      //  if (sp.Profiles[prIdx].ScoreId.Equals(ss.slitterScorerHeader.scoreTypes[i].Scorer.ScoreId))
      //    break;
      //if (prIdx < ScorerProfile.PROFILE_COUNT)
      //{
      //  ScoreClass sOld = ss.slitterScorerHeader.scoreTypes[i].Scorer.SClass;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.SClass = sp.Profiles[prIdx].SClass;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.Width = sp.Profiles[prIdx].Width;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.ProfileWidth = sp.Profiles[prIdx].ProfileWidth;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.TipHeight = sp.Profiles[prIdx].TipHeight;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.ValleyDepth = sp.Profiles[prIdx].ValleyDepth;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.TipOffset = sp.Profiles[prIdx].TipOffset;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.FarValleyOffset = sp.Profiles[prIdx].FarValleyOffset;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.UpperDiameter = sp.Profiles[prIdx].UpperDiameter;
      //  ss.slitterScorerHeader.scoreTypes[i].Scorer.LowerDiameter = sp.Profiles[prIdx].LowerDiameter;
      //  if (sOld != ss.slitterScorerHeader.scoreTypes[i].Scorer.SClass)
      //  {
      //    int positCount = ScorerProfileType.PositioningAvailableCount(ss.slitterScorerHeader.scoreTypes[i].Scorer.SClass);
      //    ss.slitterScorerHeader.scoreTypes[i].GapData = new ScoreGapData[positCount];
      //    for (int j = 0; j < ss.slitterScorerHeader.scoreTypes[i].GapData.Length; j++)
      //      ss.slitterScorerHeader.scoreTypes[i].GapData[j] = new ScoreGapData(ss.slitterScorerHeader.scoreTypes[i].Scorer.SClass);
      //  }
      //}
      SlitterScorerHeader ss = (SlitterScorerHeader)s;
      int prIdx;
			ScorerProfile sp=DBManager.getDBManager().scorerProfile;
			for (prIdx=0; prIdx<ScorerProfile.PROFILE_COUNT; prIdx++)
				if (sp.Profiles[prIdx].ScoreId.Equals(ss.scoreTypes[i].Scorer.ScoreId))
					break;
			if (prIdx<ScorerProfile.PROFILE_COUNT)
			{
				ScoreClass sOld = ss.scoreTypes[i].Scorer.SClass;
				ss.scoreTypes[i].Scorer.SClass = sp.Profiles[prIdx].SClass;
				ss.scoreTypes[i].Scorer.Width = sp.Profiles[prIdx].Width;
				ss.scoreTypes[i].Scorer.ProfileWidth = sp.Profiles[prIdx].ProfileWidth;
				ss.scoreTypes[i].Scorer.TipHeight = sp.Profiles[prIdx].TipHeight;
				ss.scoreTypes[i].Scorer.ValleyDepth = sp.Profiles[prIdx].ValleyDepth;
				ss.scoreTypes[i].Scorer.TipOffset = sp.Profiles[prIdx].TipOffset;
				ss.scoreTypes[i].Scorer.FarValleyOffset = sp.Profiles[prIdx].FarValleyOffset;
				ss.scoreTypes[i].Scorer.UpperDiameter = sp.Profiles[prIdx].UpperDiameter;
				ss.scoreTypes[i].Scorer.LowerDiameter = sp.Profiles[prIdx].LowerDiameter;
				if (sOld != ss.scoreTypes[i].Scorer.SClass)
				{
					int positCount = ScorerProfileType.PositioningAvailableCount(ss.scoreTypes[i].Scorer.SClass);
					ss.scoreTypes[i].GapData = new ScoreGapData[positCount];
					for (int j = 0; j < ss.scoreTypes[i].GapData.Length; j++)
						ss.scoreTypes[i].GapData[j] = new ScoreGapData(ss.scoreTypes[i].Scorer.SClass);
				}
			}
		}
	}
}
