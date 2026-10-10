using System;

namespace SyncroC.Utility
{
	
	/// <summary>
	/// Contains all the Enums which constants names need to be translated.
	/// </summary>
	public class FosbersoftEnums
	{
		private TrimType trimType;
    private LevelName levelName;
    private KnifeLevel knifeLevel;
    private PositType positType;
		private ScoreClass scoreClass;
		private PalletSending palletSending;
		private ShaftStatus shaftStatus;
		private ScorerShaft scorerShaft;
		private SetupStatus setupStatus;
		private MachineStatus machineStatus;
		private OrderChangeStatus orderChangeStatus;
    private SlitPositionsStatus slitPositionsStatus;
    private SlitPositioningStatus slitPositioningStatus;
    //agrassi_FI24-02-2010_3
    private DischargeSide dischargeSide;
    private Balance balance;


		/// <summary>
		/// does not do anything but it is useful to avoid some warnings.
		/// </summary>
		private void avoidWarnings()
		{
			//avoid warning
			if (trimType == TrimType.NormalTrim) trimType = TrimType.NormalTrim;
      if (levelName == LevelName.Nolevel) levelName = LevelName.Nolevel;
      if (knifeLevel == KnifeLevel.NoLevel) knifeLevel = KnifeLevel.NoLevel;
      if (positType == PositType.NoPositType) positType = PositType.NoPositType;
			if (scoreClass == ScoreClass.NoClass) scoreClass = ScoreClass.NoClass;
			if (palletSending == PalletSending.Delivery) palletSending = PalletSending.Delivery;
			if (shaftStatus == ShaftStatus.NotPositioned) shaftStatus = ShaftStatus.NotPositioned;
			if (scorerShaft == ScorerShaft.Auto) scorerShaft = ScorerShaft.Auto;
			if (setupStatus == SetupStatus.NotValid) setupStatus = SetupStatus.NotValid;
			if (machineStatus == MachineStatus.AOCSimulation) machineStatus = MachineStatus.AOCSimulation;
			if (orderChangeStatus == OrderChangeStatus.AOCCompleted) orderChangeStatus = OrderChangeStatus.AOCCompleted;
      if (slitPositionsStatus == SlitPositionsStatus.PositionsOK) slitPositionsStatus = SlitPositionsStatus.WaitForPositions;
      if (slitPositioningStatus == SlitPositioningStatus.PositioningOK) slitPositioningStatus = SlitPositioningStatus.RemoveBoard;
      //agrassi_FI24-02-2010_3
      if (dischargeSide==DischargeSide.Front) dischargeSide=DischargeSide.Left;
      if (balance == Balance.Partial) balance = Balance.Total;
    }

    public static string LevelNameToChar(string levelName)
		{
			string level = "?";

      level = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + levelName);

      if (level != null)
        if (level.Length > 0)
          level = level[0].ToString();

      return level;
    }

    public static string LevelNameToArrow(string levelName)
    {
      string level = "?";

      if (levelName.Length > 1)
        levelName = levelName[0].ToString();

      switch (levelName.ToUpper())
      {
        case "L":
          level = "\u21D9";
          break;
        case "M":
          level = "\u21D0";
          break;
        case "U":    
          level = "\u21D9";
          break;
        default:
          level = "?";
          break;
      }

      return level;
    }

		public static string LevelNameToNumber(string levelName)
		{
			string level = "?";

			if (levelName.Length > 1)
				levelName = levelName[0].ToString();

			switch (levelName.ToUpper())
			{
				case "L":
					level = "1";
					break;
				case "M":
					switch (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount)
					{
						case 1:
							level = "1";
							break;
						case 2:
							level = "2";
							break;
						case 3:
							level = "2";
							break;
						default:
							level = "?";
							break;
					}
					break;
				case "U":
					switch (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount)
					{
						case 1:
							level = "1";
							break;
						case 2:
							level = "2";
							break;
						case 3:
							level = "3";
							break;
						default:
							level = "?";
							break;
					}
					break;
				default:
					level = "?";
					break;
			}

			return level;
		}
	}

	public enum TrimType
	{
		NormalTrim=0,
		NoTrim=1
	};

  //agrassi_FI24-02-2010_3
  public enum DischargeSide
  {
    Right = 0,
    Left = 1,
    Front = 2
  };

  public enum Balance
  {
	  Total='T',
	  Partial='P',
	  Scrap='X'
  };


	public enum LevelName
	{
		Nolevel=0,
		Lower=1,
		Middle=2,
		Upper=3
	};

  public enum KnifeLevel
  { 
    NoLevel = 0,
    Level1 = 1,
    Level2 = 2,
    Level3 = 3
  };

  public enum PositType
	{
		NoPositType=0,
		MaleFemaleP=1,
		NormalOffset=2,
		ReverseOffset=3,
		PointToPoint=4,
		NormalReverse=5,
		TeletwinP=6,
		TearTape=7
	};
	
	public enum ScoreClass
	{
		NoClass=0,
		MaleFemale=1,
		Offset=2,
		Linpac=3,
		Western=4,
		Teletwin=5,
		TeletwinUSA=6
	};

	public enum PalletSending
	{
		Transform='T',
		Delivery='D',
		Other='O'
	};
	
	public enum ShaftStatus: int
	{
    NotPositioned = 0,
    PositioningNow = 1,
    PositionedPart1 = 2,
    Positioned = 3,
    Plunged = 4,
    Disabled = 5
  }

	public enum ScorerShaft : int
	{
		Auto = 0,
		Sh1 = 1,
		Sh2 = 2,
		ShAux = 3
	}

  //CM6 13-09-2005
	public enum SetupStatus : int
  {
    Tranferred=0,
    PositioningNow=1,
    NotAligned=2,
    NotValid=3
  }

  public enum MachineStatus
  {
    Configured = 0,
    Emergency = 1,
    Restored = 2,
    ResetRunning = 3,
    ResetDone = 4,
    Started = 5,
    ScanningSlits = 6,
    Positioning = 7,
    ReadyToRun = 8,
    Running = 9,
    Grinding = 10,
    NormalStop = 12,
    BoardPresent = 15,
    Maintenance = 16,
    Simulation = 17,
    AOCSimulation = 18,
    NoGapSimulation = 19,
    TestIO = 20, 
    PLCNotRunning = 21,
    PLCCommError = 22,
    PositioningFailed = 23,
    Calibration = 24,
    TeflonCycle = 25
  }

  enum OrderChangeStatus
  {
    ReadyForAOC = 0,
    PreparingAOC = 1,
    ResetAOC = 2,
    AOCRunning = 3,
    NoGapAOC = 4,
    TandemAOC = 5,
    AOCCompleted = 6,
    NotReadyForAOC = 7
  }

  enum SlitPositionsStatus
  {
    PositionsOK = 0,
    WaitForPositions = 1,
    Shaft1NotOK = 2,
    Shaft2NotOK = 3
  };

  enum SlitPositioningStatus
  {
    PositioningOK = 0,
    RemoveBoard = 1,
    NotOKShaft1 = 2,
    NotOKShaft2 = 3,
    SimulatePLCActive = 4
  };
}
