using System;
using System.ComponentModel;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for SingleFacer.
	/// </summary>
	
	[TypeConverter(typeof(Converters))]
	public class WebDirector : SyncroC.DataBase.LineConfig.LineBase
	{
		private int paddlesNum;
		[PropertyOrder(10), Category("Web Director"), Description("The number of paddles")]
		public int PaddlesNum
		{
			get { return paddlesNum; }
			set {paddlesNum=value;}
		}

		private int paddlesWidth;
		[PropertyOrder(20), Category("Web Director"), Description("The width of each paddle")]
		public int PaddlesWidth
		{
			get { return paddlesWidth; }
			set {paddlesWidth=value;}
		}

		private int paddlesDist;
		[PropertyOrder(30), Category("Web Director"), Description("The distance between two contiguous paddles' central lines")]
		public int PaddlesDist
		{
			get { return paddlesDist; }
			set {paddlesDist=value;}
		}

		private int offset;
		[PropertyOrder(40), Category("Web Director"), Description("Offset to compensate for mechanical disalignment (+/-) -> (dx/sx)")]
		public int Offset
		{
			get { return offset; }
			set {offset=value;}
		}

    private bool syncroInterface;
    [PropertyOrder(50), Category("Web Director"), Description("Whether the Web Director is controlled by Syncro")]
    public bool SyncroInterface
    {
      get { return this.syncroInterface; }
      set { this.syncroInterface = value; }
    }

		//RFAZZI_FI30/10/2008_2.Added PullRoll on Web Director parameter.  
		private bool pullRollPresent;																	//Presence of Pull Roll on WebDirector
		[PropertyOrder(60), Category("Web Director"), Description("Presence of Pull Roll on WebDirector")]
		public bool PullRollPresent
		{
			get { return this.pullRollPresent; }
			set { this.pullRollPresent = value; }
		}

    //PMASSARO_FI22/06/11 - Compact2: Differenziata la maschera del WEB DIRECTOR da quella inviata al PLC a quella usata per la grafica
    //firstPaddleOnLeftSide
    private bool firstPaddleOnLeftSide;																	//Presence of Pull Roll on WebDirector
    [PropertyOrder(60), Category("Web Director"), Description("First Paddle On Left Side For PLC (Not Used If TwinEV or Twin400)")]
    public bool FirstPaddleOnLeftSide
    {
      get { return this.firstPaddleOnLeftSide; }
      set { this.firstPaddleOnLeftSide = value; }
    }

    /// <summary>
    /// Builder.
		/// </summary>
		/// <param name="number"></param>
		public WebDirector():base()
		{
			sectionName="WEB_DIRECTOR";

			this.offset=0;
			this.paddlesDist=0;
			this.paddlesNum=0;
			this.paddlesWidth=0;
      this.syncroInterface = false;
			this.pullRollPresent = false;
      //PMASSARO_FI22/06/11 - Compact2: Differenziata la maschera del WEB DIRECTOR da quella inviata al PLC a quella usata per la grafica
      //firstPaddleOnLeftSide
      this.firstPaddleOnLeftSide = true;
    }
	}
}