#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;
#endregion

namespace SyncroC.Controls.Tools
{
	public enum MachineToolsType
	{
		Twin,
		OnlySlits,
		WebDir,
		Rotary
	}

	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
	
	public class MachineTools : SyncroC.Controls.SyncroUserControl
	{
		public delegate void ChangeShaftFunction(int slitIndex, int shaftIndex);
		public delegate void UpdateValuesFunction();
		private SlitterScorer config;
		private SlitShaft[] slitShafts;
		private ScoreShaft[] scoreShafts;
		private WebDirectorPaddles webDir;
		private RotarySectors rcs;
		private double scale;
		private Hashtable colorTable;
		private bool debug = false, scroll = false;
		private int activeScrollShaft = 0;
		private int webWidth, prevWebWidth, shaftMask, prevShaftMask;
    //agrassi_FI04-02-09
    //private int[][] slitPositions, prevSlitPositions, scorePositions, upRelScorePositions, measureIndex, leftPositions, trimSlitNarrowPositions;
    private int[][] slitPositions, prevSlitPositions, scorePositions;
    private int[] slitNotOKMask = null;
    //agrassi_FI04-02-09
    //private int[][] scoreGap, shaftGap;
    //private PositType[][] posType;
    //agrassi_FI04-02-09
    //private int[] plungedSlitMask, dummySlitMask, sepSlitMask, prevSepSlitMask, startScorer, scoringCount;
    //agrassiFI27-08-2010_1
    //private int[] plungedSlitMask, sepSlitMask, prevSepSlitMask, startScorer, scoringCount;
    private int[] prevPlungedSlitMask, plungedSlitMask, sepSlitMask, prevSepSlitMask, startScorer, scoringCount;
    private bool[] goodSlitPositions;
		private bool[] support;
		private int[] tearTapePosition;
		private int tearTapeCount;
		private int mask2level, mask3level, sectorMask;
    //private bool maskValid, wideToNarrow = false;
    private bool maskValid;
    private bool valid, allSlits, multiSelect;
		private bool prevLeftSepSlit, prevRightSepSlit, nextLeftSepSlit, nextRightSepSlit;
		private LevelName[] prevLevels, nextLevels;
		MachineToolsType machType;
		private bool firstTime = true;
		private string[] numberOfGrindings;
		private int shaftToGrind = -1;
		private ChangeShaftFunction changeFunction;
		private UpdateValuesFunction updateFunction;
    //agrassi_FI08-09-2010_1
    private bool configured = false;
    
    public MachineTools()
		{
			InitializeComponent();
			this.goodSlitPositions = new bool[] { true, true };
			this.colorTable = new Hashtable();
			this.slitShafts = new SlitShaft[SlitterScorerHeader.NUM_SLIT_SHAFTS];
			for (int sh = 0; sh < SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
			{
				this.slitShafts[sh] = new SlitShaft(sh);
				this.slitShafts[sh].Visible = false;
				this.Controls.Add(this.slitShafts[sh]);
			}
			this.scoreShafts = new ScoreShaft[SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
			for (int sh = 0; sh < SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
			{
				this.scoreShafts[sh] = new ScoreShaft(sh);
				this.scoreShafts[sh].Visible = false;
				this.Controls.Add(this.scoreShafts[sh]);
			}
			this.webDir = new WebDirectorPaddles();
			this.webDir.Visible = false;
			this.Controls.Add(this.webDir);

			this.rcs = new RotarySectors();
			this.rcs.Visible = false;
			this.Controls.Add(this.rcs);

			//For the designer
			this.config = new SlitterScorer();

			this.config.SlitterScorerHeader.AuxPresent = true;

			this.plungedSlitMask = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.slitPositions = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS][];
			for (int i = 0; i < SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
				slitPositions[i] = new int[SlitterScorerHeader.MAX_SLITS_PER_SHAFT];

			this.slitShafts[0].ActiveScroll = true;
			this.slitShafts[1].ActiveScroll = false;

		}

		public void Config(LineConfig cfg, MachineToolsType type, bool scroll, bool multiSelect,
													ChangeShaftFunction changeFunction, UpdateValuesFunction updateFunction,
													string notValidSectorsMask, string missingNextSetup)
		{
			this.config = cfg.SlitterScorerConfig;
			this.machType = type;
			this.scroll = scroll;
			this.multiSelect = multiSelect;
			this.changeFunction = null;
			this.updateFunction = null;
			if ((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.OnlySlits)
					|| (this.machType == MachineToolsType.WebDir))
			{
				if (this.machType == MachineToolsType.OnlySlits)
				{
					this.changeFunction = changeFunction;
					if (this.multiSelect) this.updateFunction = updateFunction;
				}
				for (int sh = 0; sh < SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
				{
					this.slitShafts[sh].Config(this.config, scroll && (this.machType == MachineToolsType.OnlySlits) && (sh == 0),
																			this.multiSelect && (this.machType == MachineToolsType.OnlySlits),
																			this.changeFunction, this.updateFunction);
					this.slitShafts[sh].Visible = true;
				}
			}
			if (this.machType == MachineToolsType.Twin)
			{
				for (int sh = 0; sh < SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
				{
					this.scoreShafts[sh].Config(this.config, this.colorTable);
					this.scoreShafts[sh].Visible = true;
				}
			}
			else if (this.machType == MachineToolsType.WebDir)
			{
				this.changeFunction = changeFunction;
				this.webDir.Config(cfg, scroll, this.changeFunction);
				this.webDir.Visible = true;
			}
			else if (this.machType == MachineToolsType.Rotary)
			{
				this.rcs.Config(cfg, notValidSectorsMask, missingNextSetup);
				this.rcs.Visible = true;
			}
      //agrassi_FI08-09-2010_1
      this.configured = true;
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MachineTools
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Name = "MachineTools";
			this.Size = new System.Drawing.Size(972, 398);
			this.SizeChanged += new System.EventHandler(this.MachineTools_SizeChanged);
			this.ResumeLayout(false);

		}
		#endregion

    //agrassi_FI04-02-09
    //public void PositionTwin(int webWidth, int shaftMask, int[][] slitPositions, int[] plungedSlitMask, int[] dummySlitMask,
    //                      int[] sepSlitMask, int[][] scorePositions, int[][] upRelScorePositions, int[][] leftPositions,
    //                      int[][] scoreGap, int[][] shaftGap, int[] startScorer, int[] scoringCount,
    //                      bool[] support, bool valid, PositType[][] posType, int[][] measureIndex,
    //                      bool wideToNarrow, int[][] trimSlitNarrowPositions, bool allSlits,
    //                      int[] tearTapePosition, int tearTapeCount)
    public void PositionTwin(int webWidth, int shaftMask, int[][] slitPositions, int[] plungedSlitMask, int[] sepSlitMask, int[][] scorePositions, 
                             int[] startScorer, int[] scoringCount, bool[] support, bool valid, bool allSlits, 
                              int[] tearTapePosition, int tearTapeCount)
		{
			this.Invalidate();
			this.webWidth = webWidth;
			this.shaftMask = shaftMask;
			this.slitPositions = slitPositions;
      //this.trimSlitNarrowPositions = trimSlitNarrowPositions;
      //this.wideToNarrow = wideToNarrow;
			this.plungedSlitMask = plungedSlitMask;
      //agrassi_FI04-02-09
      //this.dummySlitMask = dummySlitMask;
      this.sepSlitMask = sepSlitMask;
			this.scorePositions = scorePositions;
      //this.upRelScorePositions = upRelScorePositions;
			this.startScorer = startScorer;
			this.scoringCount = scoringCount;
			this.support = support;
			this.valid = valid;
      //this.posType = posType;
      //this.measureIndex = measureIndex;
      //this.leftPositions = leftPositions;
      //this.scoreGap = scoreGap;
      //this.shaftGap = shaftGap;
			this.allSlits = allSlits;
			this.tearTapePosition = tearTapePosition;
			this.tearTapeCount = tearTapeCount;
			this.mask2level = 0;
			this.mask3level = 0;
			this.maskValid = false;
			this.goodSlitPositions = new bool[] { true, true };
			this.slitNotOKMask = null;
			this.numberOfGrindings = null;
			this.shaftToGrind = -1;
		}

		public void PositionOnlySlits(int[][] slitPositions, int[] plungedSlitMask, bool[] goodSlitPositions)
		{
			this.PositionOnlySlits(slitPositions, plungedSlitMask, null, goodSlitPositions);
		}
		public void PositionOnlySlits(int[][] slitPositions, int[] plungedSlitMask, int[] slitNotOKMask)
		{
			this.PositionOnlySlits(slitPositions, plungedSlitMask, slitNotOKMask, null);
		}

		public void PositionOnlySlits()
		{
			this.PositionOnlySlits(null, null, null, null);
		}

		public void PositionOnlySlits(int[][] slitPositions, int[] plungedSlitMask, int[] slitNotOKMask, bool[] goodSlitPositions)
		{
			this.Invalidate();
			this.webWidth = 0;
			this.shaftMask = 0xF;
			this.slitPositions = slitPositions;
			this.plungedSlitMask = new int[] { 0xFFFF, 0xFFFF };
			if (plungedSlitMask != null)
				this.plungedSlitMask = plungedSlitMask;
			if (goodSlitPositions != null)
				this.goodSlitPositions = goodSlitPositions;
			else
				this.goodSlitPositions = new bool[] { true, true };
      //agrassi_FI04-02-09
      //this.dummySlitMask = new int[] { 0, 0 };
      this.sepSlitMask = new int[] { 0, 0 };
      //this.trimSlitNarrowPositions = null;
      //this.wideToNarrow = false;
			this.scorePositions = null;
      //this.upRelScorePositions = null;
			this.startScorer = null;
			this.scoringCount = null;
			this.support = null;
			this.valid = true;
      //this.posType = null;
      //this.measureIndex = null;
      //this.leftPositions = null;
      //this.scoreGap = null;
      //this.shaftGap = null;
			this.allSlits = true;
			this.mask2level = 0;
			this.mask3level = 0;
			this.maskValid = false;
			this.slitNotOKMask = slitNotOKMask;
			this.tearTapePosition = null;
			this.tearTapeCount = 0;
		}

		public void PositionOnlySlitsGrinding(string[] numberOfGrindings, int shaftToGrind)
		{
			this.Invalidate();
			this.numberOfGrindings = numberOfGrindings;
			this.shaftToGrind = shaftToGrind;
		}

    //agrassi_FI04-02-09
    //public void PositionWebDir(int webWidth, int mask2level, int mask3level, bool maskValid, int shaftMask, int[][] slitPositions,
    //                            int[] plungedSlitMask, int[] dummySlitMask, int[] sepSlitMask, bool valid)
    public void PositionWebDir(int webWidth, int mask2level, int mask3level, bool maskValid, int shaftMask, int[][] slitPositions,
																int[] plungedSlitMask, int[] sepSlitMask, bool valid)
		{
			this.Invalidate();
			this.webWidth = webWidth;
			this.shaftMask = shaftMask;
			this.slitPositions = slitPositions;
			this.plungedSlitMask = plungedSlitMask;
      //agrassi_FI04-02-09
      //this.dummySlitMask = dummySlitMask;
      this.sepSlitMask = sepSlitMask;
			this.scorePositions = null;
      //this.trimSlitNarrowPositions = null;
      //this.wideToNarrow = false;
      //this.upRelScorePositions = null;
			this.startScorer = null;
			this.scoringCount = null;
			this.support = null;
			this.valid = valid;
      //this.posType = null;
      //this.measureIndex = null;
      //this.leftPositions = null;
      //this.scoreGap = null;
      //this.shaftGap = null;
			this.allSlits = false;
			this.mask2level = mask2level;
			this.mask3level = mask3level;
			this.maskValid = maskValid;
			this.tearTapePosition = null;
			this.tearTapeCount = 0;
			this.numberOfGrindings = null;
			this.shaftToGrind = -1;
		}

    //agrassiFI27-08-2010_1
    //public void PositionRotary(int sectorMask, int prevWebWidth, int nextWebWidth, int prevShaftMask, int nextShaftMask,
    //                           int[][] prevSlitPositions, int[][] nextSlitPositions, int[] prevSepSlitMask,
    //                           int[] nextSepSlitMask, LevelName[] prevLevels, LevelName[] nextLevels, bool prevLeftSepSlit,
    //                           bool prevRightSepSlit, bool nextLeftSepSlit, bool nextRightSepSlit, bool maskValid, bool valid)
		public void PositionRotary(int sectorMask, int prevWebWidth, int nextWebWidth, int prevShaftMask, int nextShaftMask,
                               int[][] prevSlitPositions, int[][] nextSlitPositions, int[] prevPlungedSlitMask, int[] plungedSlitMask, int[] prevSepSlitMask,
															 int[] nextSepSlitMask, LevelName[] prevLevels, LevelName[] nextLevels, bool prevLeftSepSlit,
															 bool prevRightSepSlit, bool nextLeftSepSlit, bool nextRightSepSlit, bool maskValid, bool valid)
		{
			this.Invalidate();
			this.sectorMask = sectorMask;
			this.prevWebWidth = prevWebWidth;
			this.webWidth = nextWebWidth;
			this.prevShaftMask = prevShaftMask;
			this.shaftMask = nextShaftMask;
			this.prevSlitPositions = prevSlitPositions;
			this.slitPositions = nextSlitPositions;
      //this.trimSlitNarrowPositions = null;
      //this.wideToNarrow = false;
      //agrassiFI27-08-2010_1
      //this.plungedSlitMask = null;
      this.plungedSlitMask = plungedSlitMask;
      this.prevPlungedSlitMask = prevPlungedSlitMask;
      //agrassi_FI04-02-09
      //this.dummySlitMask = null;
			this.prevSepSlitMask = prevSepSlitMask;
      this.sepSlitMask = nextSepSlitMask;
			this.prevLevels = prevLevels;
			this.nextLevels = nextLevels;
			this.prevLeftSepSlit = prevLeftSepSlit;
			this.nextLeftSepSlit = nextLeftSepSlit;
			this.prevRightSepSlit = prevRightSepSlit;
			this.nextRightSepSlit = nextRightSepSlit;
			this.scorePositions = null;
      //this.upRelScorePositions = null;
			this.startScorer = null;
			this.scoringCount = null;
			this.support = null;
			this.valid = valid;
      //this.posType = null;
      //this.measureIndex = null;
      //this.leftPositions = null;
      //this.scoreGap = null;
      //this.shaftGap = null;
			this.allSlits = false;
			this.mask2level = 0;
			this.mask3level = 0;
			this.maskValid = maskValid;
			//  this.debug = debug;//cc same variable generate a warning
			this.tearTapePosition = null;
			this.tearTapeCount = 0;
			this.numberOfGrindings = null;
			this.shaftToGrind = -1;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
      //agrassi_FI08-09-2010_1
      if (!this.configured)
        return;
      base.OnPaint(e);
      //agrassi_FI18-06-08
      e.Graphics.Clear(Color.WhiteSmoke);
			this.scale = (double)this.config.SlitterScorerHeader.MachineWidth / (double)this.Width;
			if (this.firstTime)
			{
				this.ConfigShafts();
				this.firstTime = false;
			}
			if (this.valid)
			{
				int[] slitLines = null, prevSlitLines = null, scoreLines = null, tearTapeLines = null;
        //agrassiFI27-08-2010_1
        int[] nonSepSlitLines = null, nonSepPrevSlitLines = null;
				if (((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.WebDir) ||
						 (this.machType == MachineToolsType.Rotary)) && (this.slitPositions != null))
				{
					slitLines = new int[2 * this.config.GetSlitCount()];
          //agrassiFI27-08-2010_1
          nonSepSlitLines = new int[2 * this.config.GetSlitCount()];
          int nsCnt = 0;
          int sltCnt = 0;
					for (Shaft sh = Shaft.Slit1; sh <= Shaft.Slit2; sh++)
					{
						if (((shaftMask & SlitterScorer.BIT((int)sh)) != 0) &&
								(this.slitPositions[sh - Shaft.Slit1] != null))
						{
							for (int slt = 0; slt < this.config.GetSlitCount(); slt++)
							{
								if (((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.WebDir))
										 && (this.plungedSlitMask != null))
								{
									if ((plungedSlitMask[sh - Shaft.Slit1] & SlitterScorer.BIT(slt)) != 0)
									{
										slitLines[sltCnt] = this.slitPositions[sh - Shaft.Slit1][slt];
                    //agrassi_FI04-02-09
                    //if (this.wideToNarrow && (this.trimSlitNarrowPositions != null) &&
                    //    ((slt == 0) || (slt == this.config.GetSlitCount() - 1)))
                    //  slitLines[sltCnt] = this.trimSlitNarrowPositions[sh - Shaft.Slit1][(slt == 0) ? slt : 1];
										sltCnt++;
									}
								}
								else
								{
									if ((sepSlitMask[sh - Shaft.Slit1] & SlitterScorer.BIT(slt)) != 0)
										slitLines[sltCnt++] = slitPositions[sh - Shaft.Slit1][slt];
                  //agrassiFI27-08-2010_1
                  else if ((plungedSlitMask[sh - Shaft.Slit1] & SlitterScorer.BIT(slt)) != 0)
                    nonSepSlitLines[nsCnt++] = slitPositions[sh - Shaft.Slit1][slt];
								}
							}
						}
					}
				}
				if ((this.machType == MachineToolsType.Rotary) && (this.prevSepSlitMask != null) && (this.prevSlitPositions != null))
				{
          //agrassiFI27-08-2010_1
          int nsCnt = 0;
          nonSepPrevSlitLines = new int[2 * this.config.GetSlitCount()];
          prevSlitLines = new int[2 * this.config.GetSlitCount()];
					int sltCnt = 0;
					for (Shaft sh = Shaft.Slit1; sh <= Shaft.Slit2; sh++)
					{
						if ((prevShaftMask & SlitterScorer.BIT((int)sh)) != 0)
						{
							for (int slt = 0; slt < this.config.GetSlitCount(); slt++)
							{
								if ((prevSepSlitMask[sh - Shaft.Slit1] & SlitterScorer.BIT(slt)) != 0)
									prevSlitLines[sltCnt++] = prevSlitPositions[sh - Shaft.Slit1][slt];
                //agrassiFI27-08-2010_1
                else if ((prevPlungedSlitMask[sh - Shaft.Slit1] & SlitterScorer.BIT(slt)) != 0) 
                  nonSepPrevSlitLines[nsCnt++] = prevSlitPositions[sh - Shaft.Slit1][slt];
              }
						}
					}
				}
				if ((this.machType == MachineToolsType.Twin) && (this.startScorer != null) && (this.scorePositions != null))
				{
					scoreLines = new int[this.config.SlitterScorerHeader.ScoreShafts[0].GetScoreCount() +
															this.config.SlitterScorerHeader.ScoreShafts[1].GetScoreCount() +
														(this.config.SlitterScorerHeader.AuxPresent ? this.config.SlitterScorerHeader.ScoreShafts[2].GetScoreCount() : 0)];

					int scrCnt = 0;
					for (Shaft sh = Shaft.Score1; sh <= Shaft.ScoreAux; sh++)
					{
						if (((shaftMask & SlitterScorer.BIT((int)sh)) != 0) && (this.scorePositions[sh - Shaft.Score1] != null))
						{
							for (int scr = 0; scr < this.config.SlitterScorerHeader.ScoreShafts[sh - Shaft.Score1].GetScoreCount(); scr++)
							{
								if ((scr >= startScorer[sh - Shaft.Score1]) && (scr < startScorer[sh - Shaft.Score1] + scoringCount[sh - Shaft.Score1]))
									if (!support[sh - Shaft.Score1])
										scoreLines[scrCnt++] = scorePositions[sh - Shaft.Score1][scr];
							}
						}
					}
				}
				if (this.machType == MachineToolsType.Twin)
				{
					if ((this.tearTapeCount > 0) && (this.tearTapePosition != null))
					{
						tearTapeLines = new int[this.tearTapeCount];
						for (int tt = 0; tt < this.tearTapeCount; tt++)
							tearTapeLines[tt] = this.tearTapePosition[tt];
					}
				}

        //agrassi_FI04-02-09
        //if (((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.OnlySlits)
        //    || (this.machType == MachineToolsType.WebDir)) &&
        //    (this.plungedSlitMask != null) && (this.dummySlitMask != null) && (this.sepSlitMask != null) &&
        //    (this.goodSlitPositions != null))
				if (((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.OnlySlits)
						|| (this.machType == MachineToolsType.WebDir)) &&
						(this.plungedSlitMask != null) && (this.sepSlitMask != null) &&
						(this.goodSlitPositions != null))
				{
					for (Shaft sh = Shaft.Slit1; sh <= Shaft.Slit2; sh++)
					{
						if (((shaftMask & SlitterScorer.BIT((int)sh)) != 0) && valid)
						{
              //agrassi_FI04-02-09
              //int[] narrowSlitPos = null;
              //if (this.trimSlitNarrowPositions != null)
              //  narrowSlitPos = this.trimSlitNarrowPositions[sh - Shaft.Slit1];
							int slitNotOKMask = 0;
							if (this.slitNotOKMask != null)
								slitNotOKMask = this.slitNotOKMask[sh - Shaft.Slit1];
							int[] slitPos = null;
							if (this.slitPositions != null)
								slitPos = slitPositions[sh - Shaft.Slit1];
              //agrassi_FI04-02-09
              //this.slitShafts[sh - Shaft.Slit1].Position(webWidth, slitPos, plungedSlitMask[sh - Shaft.Slit1],
              //                                dummySlitMask[sh - Shaft.Slit1], sepSlitMask[sh - Shaft.Slit1], slitLines, scoreLines, tearTapeLines,
              //                                this.debug, this.wideToNarrow, narrowSlitPos, this.allSlits, this.goodSlitPositions[sh - Shaft.Slit1],
              //                                slitNotOKMask, this.numberOfGrindings, this.shaftToGrind == (int)sh);
              this.slitShafts[sh - Shaft.Slit1].Position(webWidth, slitPos, plungedSlitMask[sh - Shaft.Slit1],
                                              sepSlitMask[sh - Shaft.Slit1], slitLines, scoreLines, tearTapeLines,
                                              this.debug, this.allSlits, this.goodSlitPositions[sh - Shaft.Slit1],
                                              slitNotOKMask, this.numberOfGrindings, this.shaftToGrind == (int)sh);
            }
						else
							this.slitShafts[sh - Shaft.Slit1].NotWorkingShaft(webWidth, slitLines, scoreLines, tearTapeLines, this.debug);
					}
				}
        //agrassi_FI04-02-09
        //if ((this.machType == MachineToolsType.Twin) && (scorePositions != null) && (upRelScorePositions != null) &&
        //    (leftPositions != null) && (scoreGap != null) && (shaftGap != null) && (startScorer != null) &&
        //    (scoringCount != null) && (support != null) && (posType != null) && (measureIndex != null))
				if ((this.machType == MachineToolsType.Twin) && (scorePositions != null) && (startScorer != null) &&
						(scoringCount != null) && (support != null))
				{
					for (Shaft sh = Shaft.Score1; sh <= Shaft.ScoreAux; sh++)
					{
						if (((shaftMask & SlitterScorer.BIT((int)sh)) != 0) && valid)
              //agrassi_FI04-02-09
              //this.scoreShafts[sh - Shaft.Score1].Position(webWidth, scorePositions[sh - Shaft.Score1],
              //              upRelScorePositions[sh - Shaft.Score1], leftPositions[sh - Shaft.Score1], scoreGap[sh - Shaft.Score1],
              //              shaftGap[sh - Shaft.Score1], startScorer[sh - Shaft.Score1],
              //              scoringCount[sh - Shaft.Score1], slitLines, scoreLines, tearTapeLines, support[sh - Shaft.Score1],
              //              posType[sh - Shaft.Score1], measureIndex[sh - Shaft.Score1], this.debug);
							this.scoreShafts[sh - Shaft.Score1].Position(webWidth, scorePositions[sh - Shaft.Score1], startScorer[sh - Shaft.Score1],
														scoringCount[sh - Shaft.Score1], slitLines, scoreLines, tearTapeLines, support[sh - Shaft.Score1], this.debug);
						else
							this.scoreShafts[sh - Shaft.Score1].NotWorkingShaft(webWidth, slitLines, scoreLines, tearTapeLines, this.debug);
					}
				}
				else if (this.machType == MachineToolsType.WebDir)
				{
					this.webDir.Position(webWidth, mask2level, mask3level, slitLines, maskValid, debug);
				}
				else if (this.machType == MachineToolsType.Rotary)
				{
          //agrassiFI27-08-2010_1
          //this.rcs.Position(sectorMask, prevWebWidth, webWidth, prevSlitLines, slitLines, prevLevels, nextLevels,
          //    prevLeftSepSlit, prevRightSepSlit, nextLeftSepSlit, nextRightSepSlit, maskValid, debug);
          this.rcs.Position(sectorMask, prevWebWidth, webWidth, prevSlitLines, slitLines, nonSepPrevSlitLines, nonSepSlitLines, prevLevels, nextLevels,
              prevLeftSepSlit, prevRightSepSlit, nextLeftSepSlit, nextRightSepSlit, maskValid, debug);
        }
			}
		}

		private void ConfigShafts()
		{
			int lastVertic = 0;
			if (this.machType == MachineToolsType.WebDir)
			{
				this.webDir.Left = 0;
				this.webDir.Top = lastVertic;
				this.webDir.Size = new Size(this.Width, (int)Math.Round(this.Height * 0.6));
				lastVertic += this.webDir.Height;
			}
			if ((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.OnlySlits)
					|| (this.machType == MachineToolsType.WebDir))
			{
        //agrassi_FI08-09-2010_1
        float heightPerc = ((this.machType == MachineToolsType.OnlySlits) ? 0.5f : 0.2f);
        for (int sh = SlitterScorerHeader.NUM_SLIT_SHAFTS; sh > 0; sh--)
				{
					this.slitShafts[sh - 1].Left = 0;
					this.slitShafts[sh - 1].Top = lastVertic;
          //float heightPerc = ((this.machType == MachineToolsType.OnlySlits) ? 0.5f : 0.2f);
          this.slitShafts[sh - 1].Size = new Size(this.Width, (int)Math.Round(this.Height * heightPerc));
					lastVertic += this.slitShafts[sh - 1].Height;
				}
			}
			if (this.machType == MachineToolsType.Twin)
			{
				for (int sh = SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS - 1; sh > 0; sh--)
				{
					this.scoreShafts[sh - 1].Left = 0;
					this.scoreShafts[sh - 1].Top = lastVertic;
					this.scoreShafts[sh - 1].Size = new Size(this.Width, (int)Math.Round(this.Height * 0.2));
					lastVertic += this.scoreShafts[sh - 1].Height;
				}
				//CM7 19-09-2005
				//if (this.config.AuxPresent)
				//{
				this.scoreShafts[2].Left = 0;
				this.scoreShafts[2].Top = lastVertic;
				this.scoreShafts[2].Size = new Size(this.Width, (int)Math.Round(this.Height * 0.2));
				//}
				//else
				//  this.scoreShafts[2].Visible = false;
			}
			if (this.machType == MachineToolsType.Rotary)
			{
				this.rcs.Left = 0;
				this.rcs.Top = lastVertic;
				this.rcs.Size = new Size(this.Width, this.Height);
				lastVertic += this.rcs.Height;
			}
		}

		public bool Debug
		{
			get { return this.debug; }
			set
			{
				if (this.debug != value)
				{
					this.debug = value;
					this.Invalidate();
				}
			}
		}

		private void MachineTools_SizeChanged(object sender, EventArgs e)
		{
      //agrassi_FI08-09-2010_1
      this.firstTime = true;
			this.Invalidate();
		}

		public int ScrollLeft(ref int activeScrollSh)
		{
			if (!this.scroll) return -1;
			activeScrollSh = this.activeScrollShaft;
			if ((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.OnlySlits))
			{
				if ((this.activeScrollShaft == 0) && this.slitShafts[0].ScrollLeftEnabled)
					return this.slitShafts[0].ScrollLeft();
				else if ((this.activeScrollShaft == 0) && (!this.slitShafts[0].ScrollLeftEnabled))
				{
					this.activeScrollShaft = 1;
					activeScrollSh = this.activeScrollShaft;
					this.slitShafts[0].ActiveScroll = false;
					this.slitShafts[1].ActiveScroll = true;
					return this.slitShafts[1].SetScrollRight();
				}
				else if ((this.activeScrollShaft == 1) && this.slitShafts[1].ScrollLeftEnabled)
					return this.slitShafts[1].ScrollLeft();
				else if ((this.activeScrollShaft == 1) && (!this.slitShafts[1].ScrollLeftEnabled))
				{
					this.activeScrollShaft = 0;
					activeScrollSh = this.activeScrollShaft;
					this.slitShafts[0].ActiveScroll = true;
					this.slitShafts[1].ActiveScroll = false;
					return this.slitShafts[0].SetScrollRight();
				}
				else
					return -1;
			}
			else if (this.machType == MachineToolsType.WebDir)
				return this.webDir.ScrollLeft();
			else
				return -1;
		}

		public int ScrollRight(ref int activeScrollSh)
		{
			if (!this.scroll) return -1;
			activeScrollSh = this.activeScrollShaft;
			if ((this.machType == MachineToolsType.Twin) || (this.machType == MachineToolsType.OnlySlits))
			{
				if ((this.activeScrollShaft == 0) && this.slitShafts[0].ScrollRightEnabled)
					return this.slitShafts[0].ScrollRight();
				else if ((this.activeScrollShaft == 0) && (!this.slitShafts[0].ScrollRightEnabled))
				{
					this.activeScrollShaft = 1;
					activeScrollSh = this.activeScrollShaft;
					this.slitShafts[0].ActiveScroll = false;
					this.slitShafts[1].ActiveScroll = true;
					return this.slitShafts[1].SetScrollLeft();
				}
				else if ((this.activeScrollShaft == 1) && this.slitShafts[1].ScrollRightEnabled)
					return this.slitShafts[1].ScrollRight();
				else if ((this.activeScrollShaft == 1) && (!this.slitShafts[1].ScrollRightEnabled))
				{
					this.activeScrollShaft = 0;
					activeScrollSh = this.activeScrollShaft;
					this.slitShafts[0].ActiveScroll = true;
					this.slitShafts[1].ActiveScroll = false;
					return this.slitShafts[0].SetScrollLeft();
				}
				else
					return -1;
			}
			else
				return this.webDir.ScrollRight();
		}

		public int ChangeShaft(bool keepScrollIndex)
		{
			if (this.activeScrollShaft == 0)
			{
				this.activeScrollShaft = 1;
				this.slitShafts[0].ActiveScroll = false;
				this.slitShafts[1].ActiveScroll = true;
				if (keepScrollIndex)
					this.slitShafts[1].SetScrollIndex(this.slitShafts[0].GetScrollIndex());
			}
			else
			{
				this.activeScrollShaft = 0;
				this.slitShafts[0].ActiveScroll = true;
				this.slitShafts[1].ActiveScroll = false;
				if (keepScrollIndex)
					this.slitShafts[0].SetScrollIndex(this.slitShafts[1].GetScrollIndex());
			}
			return this.activeScrollShaft;
		}

		public void ToggleSelection(int shaft, int slt)
		{
			if ((shaft >= 0) && (shaft < SlitterScorerHeader.NUM_SLIT_SHAFTS))
			{
				if ((slt >= 0) && (slt < this.config.GetSlitCount()))
				{
					if (this.multiSelect)
					{
						this.slitShafts[shaft].ToggleSelection(slt);
					}
				}
			}
		}

		public void EnableMultiSelect(bool en)
		{
			this.multiSelect = en;
			for (int slt = 0; slt < SlitterScorerHeader.NUM_SLIT_SHAFTS; slt++)
				this.slitShafts[slt].EnableMultiSelect(en);
		}

		public void BlockMultiSelect(bool block)
		{
			for (int slt = 0; slt < SlitterScorerHeader.NUM_SLIT_SHAFTS; slt++)
				this.slitShafts[slt].BlockMultiSelect(block);
		}

		//RFAZZI_FI01/07/2008_1.Added control for refresh the slitter only when needed
		public bool getIsBlockMultiSelectActive()
		{
			bool multiSelectActive=false;
			for (int slt = 0; slt < SlitterScorerHeader.NUM_SLIT_SHAFTS; slt++)
			{
				if (this.slitShafts[slt].getIsBlockMultiSelectActive())
				{
					multiSelectActive = true;
					break;
				}
			}
			return multiSelectActive;
		}

		public void ResetMultiSelection()
		{
			for (int slt = 0; slt < SlitterScorerHeader.NUM_SLIT_SHAFTS; slt++)
				this.slitShafts[slt].ResetMultiSelection();
		}

		public int GetSelectedSlitMask(int shaft)
		{
			if ((shaft < 0) || (shaft >= this.config.GetSlitCount()))
				return 0;
			return this.slitShafts[shaft].GetSelectedSlitMask();
		}

		//CM7 19-09-2005
		public void SetPositioning(bool val, int shaft)
		{
			if ((shaft < 0) || (shaft > 1))
				return;
			if (this.machType == MachineToolsType.OnlySlits)
			{
				this.slitShafts[shaft].SetPositioning(val);
			}
		}

		private int Conv(int dim)
		{
			if (this.scale <= 0) return 0;
			return (int)Math.Round((double)dim / this.scale);
		}
	}
}
