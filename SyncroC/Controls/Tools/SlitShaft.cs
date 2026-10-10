#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;
#endregion


namespace SyncroC.Controls.Tools
{
	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.

	public class SlitShaft : SyncroC.Controls.SyncroUserControl
	{
		public const int ANVIL_ARM_WIDTH = 1500;
		private const int ROBOT_PLC_REF = 5000;
		private double scale;
		private SlitterScorer config;
		private int number;
		private int slitCount;
		private Slit[] slits;
		private bool debug = false, scroll = false;
		private int scrollIndex = 0;
		private int[] slitLines, scoreLines, tearTapeLines;
    //agrassi_FI04-02-09
    //private int webWidth, plungedMask, dummyMask, sepMask;
    private int webWidth, plungedMask, sepMask;
    //private int[] positions, trimSlitNarrowPositions;
    private int[] positions;
    private int[] slitsAlignmentWithPlc;
		private bool working, allSlits, goodPositions = true;
		private int slitNotOKMask = 0;
		private int rightBoardEdge, leftBoardEdge;
		//private Color boardColor = System.Drawing.Color.FromArgb(255, 224, 192);
		private Color boardColor = Color.BurlyWood;
    private bool firstTime = true;
    //agrassi_FI08-09-2010_1
    private bool configured = false;
    //private bool multiSelect, wideToNarrow = false;
    private bool multiSelect;
    private MachineTools.ChangeShaftFunction changeFunction;
		private MachineTools.UpdateValuesFunction updateFunction;
		private Timer timPositioning;
		private bool blinkColor;
		private bool blockMultiSelect = false;
		private string[] numberOfGrindings;
		private bool showGrindings;

		public SlitShaft(int number)
		{
			InitializeComponent();
			this.timPositioning.Interval = 400;
			this.number = number;

			//For the designer
			this.config = new SlitterScorer();
			this.config.SlitterScorerHeader.SlitShafts[this.number].Enabled = true;
		}

		public void Config(SlitterScorer ss, bool scroll, bool multiSelect,
											MachineTools.ChangeShaftFunction changeFunction, MachineTools.UpdateValuesFunction updateFunction)
		{
			this.config = ss;
			this.slitCount = this.config.SlitterScorerHeader.SlitShafts[this.number].GetSlitCount();
			this.slits = new Slit[this.slitCount];
			for (int slt = 0; slt < this.slitCount; slt++)
				this.slits[slt] = new Slit(slt);
			this.SetSlitsAlignmentWithPlc();
			this.scroll = scroll;
			this.multiSelect = multiSelect;
			this.changeFunction = changeFunction;
			this.updateFunction = updateFunction;
      //agrassi_FI08-09-2010_1
      this.configured = true;
		}

		private void SetSlitsAlignmentWithPlc()
		{
			int slt, center;
			this.slitsAlignmentWithPlc = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS];
			for (int sh = 0; sh < SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
			{
				for (slt = 0; slt < SlitterScorerHeader.MAX_SLIT_TYPES_MACHINE; slt++)
					if (this.config.SlitterScorerHeader.SlitShafts[sh].IsSlitInTheShaft(this.config.SlitterScorerHeader.SlitTypes[slt].Letter))
						break;
				if (slt < SlitterScorerHeader.MAX_SLIT_TYPES_MACHINE)
				{
					//RFAZZI PROVA
					//center = (this.config.SlitTypes[slt].HandleWidth - this.config.SlitBidentWidth) / 2;
					center = (this.config.SlitterScorerHeader.SlitTypes[slt].HandleWidth - this.config.SlitterScorerHeader.SlitBidentWidth) / 2;

					//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
					//this.slitsAlignmentWithPlc[sh] = this.config.SlitShafts[sh].BidentDisalignment + ROBOT_PLC_REF +
					//      this.config.SlitTypes[slt].BladeFromLeft - this.config.SlitTypes[slt].HandleFromLeft - center;

					//RFAZZI PROVA
					//this.slitsAlignmentWithPlc[sh] = this.config.SlitShafts[sh].BidentDisalignment + this.config.SafetySlitsDistSides + ROBOT_PLC_REF +
					//      this.config.SlitTypes[slt].BladeFromLeft - this.config.SlitTypes[slt].HandleFromLeft - center;
					//RFAZZI_FI10/11/2008_1. Splitted the "SafetySlitDistance" for side
					//this.slitsAlignmentWithPlc[sh] = this.config.SlitterScorerHeader.SlitShafts[sh].BidentDisalignment + this.config.Twin400.SafetySlitsDistSides + ROBOT_PLC_REF +
					//      this.config.SlitterScorerHeader.SlitTypes[slt].BladeFromLeft - this.config.SlitterScorerHeader.SlitTypes[slt].HandleFromLeft - center;
					this.slitsAlignmentWithPlc[sh] = this.config.SlitterScorerHeader.SlitShafts[sh].BidentDisalignment + this.config.Twin400.SafetySlitsDistLeftSides + ROBOT_PLC_REF +
								this.config.SlitterScorerHeader.SlitTypes[slt].BladeFromLeft - this.config.SlitterScorerHeader.SlitTypes[slt].HandleFromLeft - center;

				}
			}
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
			this.components = new System.ComponentModel.Container();
			this.timPositioning = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timPositioning
			// 
			this.timPositioning.Tick += new System.EventHandler(this.timPositioning_Tick);
			// 
			// SlitShaft
			// 
			this.Name = "SlitShaft";
			this.Size = new System.Drawing.Size(964, 120);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SlitShaft_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SlitShaft_MouseDoubleClick);
			this.SizeChanged += new System.EventHandler(this.SlitShaft_SizeChanged);
			this.ResumeLayout(false);

		}
		#endregion

    //agrassi_FI04-02-09
    //public void Position(int webWidth, int[] positions, int plungedMask, int dummyMask, int sepMask,
    //                          int[] slitLines, int[] scoreLines, int[] tearTapeLines, bool debug, bool wideToNarrow,
    //                          int[] trimSlitNarrowPositions, bool allSlits, bool goodPositions,
    //                          int slitNotOKMask, string[] numberOfGrindings, bool showGrindings)
		public void Position(int webWidth, int[] positions, int plungedMask, int sepMask,
															int[] slitLines, int[] scoreLines, int[] tearTapeLines, bool debug, 
															bool allSlits, bool goodPositions, int slitNotOKMask, string[] numberOfGrindings, bool showGrindings)
		{
			this.positions = new int[] { 7000, 10000, 13000, 16000, 19000, 22000, 25000, 28000, 30000, 31400 };
			if ((positions != null) && (positions[0] != 0) && (positions[0] != positions[1]))
				this.positions = positions;
			this.slitLines = slitLines;
			this.scoreLines = scoreLines;
			this.webWidth = webWidth;
			this.leftBoardEdge = (this.config.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
			this.rightBoardEdge = (this.config.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
      //this.trimSlitNarrowPositions = trimSlitNarrowPositions;
      //this.wideToNarrow = wideToNarrow;
			this.plungedMask = plungedMask;
      //agrassi_FI04-02-09
      //this.dummyMask = dummyMask;
			this.sepMask = sepMask;
			this.debug = debug;
			this.allSlits = allSlits;
			this.goodPositions = goodPositions;
			this.working = true;
			this.slitNotOKMask = slitNotOKMask;
			this.tearTapeLines = tearTapeLines;
			this.numberOfGrindings = numberOfGrindings;
			this.showGrindings = showGrindings;
			this.Invalidate();
		}

		public void NotWorkingShaft(int webWidth, int[] slitLines, int[] scoreLines, int[] tearTapeLines, bool debug)
		{
			this.slitLines = slitLines;
			this.scoreLines = scoreLines;
			this.webWidth = webWidth;
			this.leftBoardEdge = (this.config.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
			this.rightBoardEdge = (this.config.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
			this.positions = null;
      //this.trimSlitNarrowPositions = null;
      //this.wideToNarrow = false;
			this.plungedMask = 0;
			this.debug = debug;
			this.working = false;
			this.goodPositions = true;
			this.tearTapeLines = tearTapeLines;
			this.slitNotOKMask = 0;
			this.numberOfGrindings = null;
			this.showGrindings = false;
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
      //agrassi_FI08-09-2010_1
      if (!this.configured)
      {
        e.Graphics.FillRectangle(new SolidBrush(Color.Red), 0, 0, 100, 100);
        return;
      }
      base.OnPaint(e);
      //agrassi_FI18-06-08
      e.Graphics.Clear(Color.WhiteSmoke);
			this.scale = (double)this.config.SlitterScorerHeader.MachineWidth / (double)this.Width;
      //Paint board
			int bWidth = this.Conv(this.webWidth);
			e.Graphics.FillRectangle(new SolidBrush(this.boardColor), this.Conv(this.leftBoardEdge), 0, bWidth, this.Height);
			//Paint slit, score and tear tape lines
			if (this.slitLines != null)
				for (int lin = 0; lin < this.slitLines.Length; lin++)
					if (this.slitLines[lin] > 0)
					{
						int pos = this.Conv(this.slitLines[lin]);
						e.Graphics.DrawLine(new Pen(Color.WhiteSmoke , 2.3f), pos, 0, pos, this.Height);
					}
			if (this.scoreLines != null)
				for (int lin = 0; lin < this.scoreLines.Length; lin++)
					if (this.scoreLines[lin] > 0)
					{
						int pos = this.Conv(this.scoreLines[lin]);
						e.Graphics.DrawLine(new Pen(Color.WhiteSmoke , 0.8f), pos, 0, pos, this.Height);
					}
			if (this.tearTapeLines != null)
				for (int lin = 0; lin < this.tearTapeLines.Length; lin++)
					if (this.tearTapeLines[lin] > 0)
					{
						int pos = this.Conv(this.tearTapeLines[lin]);
						e.Graphics.DrawLine(new Pen(Color.Red, 2.5f), pos, 0, pos, this.Height);
					}
			if (this.debug)
			{
				int bcp = this.config.SlitterScorerHeader.MachineWidth / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
				int pos = this.Conv(bcp);
				e.Graphics.DrawLine(new Pen(Color.Violet), pos, 0, pos, this.Height);
				if (this.number == 0)
				{
					SizeF bcs = e.Graphics.MeasureString(bcp.ToString(), new Font("Arial", 8));
					e.Graphics.DrawString(bcp.ToString(), new Font("Arial", 8), new SolidBrush(Color.Green), pos - bcs.Width / 2, 0);
				}
				//RFAZZI PROVA
				//int realToVirtual = (this.config.SideframesDistance - this.config.MachineWidth) / 2;
				int realToVirtual = (this.config.Twin400.SideframesDistance - this.config.SlitterScorerHeader.MachineWidth) / 2;

				//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
				//int margins = this.config.SlitShafts[this.number].SafetyDistSides;

				//RFAZZI PROVA
				//int margins = this.config.SafetySlitsDistSides;
				//RFAZZI_FI10/11/2008_1. Splitted the "SafetySlitDistance" for side
				//int margins = this.config.Twin400.SafetySlitsDistSides;
				int margins = this.config.Twin400.SafetySlitsDistLeftSides;

				//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
				//int leftLimit = this.config.SlitShafts[this.number].LeftMechanicalStop - realToVirtual + margins;
				int leftLimit;
				if (this.config.isTwin400Wide())
					leftLimit = this.config.SlitterScorerHeader.SlitShafts[this.number].LeftMechanicalStopWideBody - realToVirtual + margins;
				else
					//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
					leftLimit = this.config.SlitterScorerHeader.SlitShafts[this.number].LeftMechanicalStopStandardBody - realToVirtual + margins;
				pos = this.Conv(leftLimit);
				e.Graphics.DrawLine(new Pen(Color.Red), pos, 0, pos, this.Height);
				SizeF marginS = e.Graphics.MeasureString(leftLimit.ToString(), new Font("Arial", 8));
				e.Graphics.DrawString(leftLimit.ToString(), new Font("Arial", 8), new SolidBrush(Color.Green), pos - marginS.Width / 2, 0);
				//RFAZZI_FI03/06/2008.Added dedicated parameters for Twin400 Wide/Norrow body
				//int rightLimit = this.config.MachineWidth - (this.config.SlitShafts[this.number].RightMechanicalStop - realToVirtual + margins);
				int rightLimit;
				if (this.config.isTwin400Wide())
					rightLimit = this.config.SlitterScorerHeader.MachineWidth - (this.config.SlitterScorerHeader.SlitShafts[this.number].RightMechanicalStopWideBody - realToVirtual + margins);
				else
					//RFAZZI_FI18/11/08_1.Changed the Narrow Body eng.verb into StandardBody x TWIN400. 
					rightLimit = this.config.SlitterScorerHeader.MachineWidth - (this.config.SlitterScorerHeader.SlitShafts[this.number].RightMechanicalStopStandardBody - realToVirtual + margins);
				pos = this.Conv(rightLimit);
				e.Graphics.DrawLine(new Pen(Color.Red), pos, 0, pos, this.Height);
				marginS = e.Graphics.MeasureString(rightLimit.ToString(), new Font("Arial", 8));
				e.Graphics.DrawString(rightLimit.ToString(), new Font("Arial", 8), new SolidBrush(Color.Green), pos - marginS.Width / 2, 0);
			}
			//Paint shaft
			int shaftHeight = (int)Math.Round(this.Height * 0.2);
			Color shaftColor = (this.config.SlitterScorerHeader.SlitShafts[this.number].Enabled ? (this.working ? Color.Silver : Color.FromArgb(230, 230, 230)) : Color.Red);
			if (this.blinkColor)
				shaftColor = Color.Gold;
			e.Graphics.FillRectangle(new SolidBrush(shaftColor), 0, (this.Height - shaftHeight) / 2, this.Width, shaftHeight);
			SizeF n = e.Graphics.MeasureString((this.number + 1).ToString(), new Font("Arial", 10, FontStyle.Bold));
			e.Graphics.DrawString((this.number + 1).ToString(), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black),
															0, (this.Height - n.Height) / 2);
			//Configure slits
			if (this.firstTime)
			{
        this.ConfigSlits();
        this.firstTime = false;
			}
			//Paint slits
      if (this.working)
      {
        for (int slt = 0; slt < this.slitCount; slt++)
				{
          //agrassi_FI04-02-09
          //bool locWideToNarrow = ((slt == 0) || (slt == this.slitCount - 1)) && this.wideToNarrow && (this.trimSlitNarrowPositions != null);
          //int narrowPos = positions[slt];
          //if (locWideToNarrow)
          //{
          //  if (slt == 0)
          //    narrowPos = this.trimSlitNarrowPositions[0];
          //  else
          //    narrowPos = this.trimSlitNarrowPositions[1];
          //  this.slits[slt].LeftNarrow = this.Conv(narrowPos - this.slits[slt].config.BladeFromLeft);
          //}
          //agrassi_FI08-09-2010_1
          if (this.slits[slt].config != null)
          {
            this.slits[slt].Left = this.Conv(positions[slt] - this.slits[slt].config.BladeFromLeft);
            bool sltNotOk = ((this.slitNotOKMask & SlitterScorer.BIT(slt)) != 0);
            //agrassi_FI04-02-09
            //this.slits[slt].Position(positions[slt], this.showGrindings ? int.Parse(this.numberOfGrindings[slt]) : this.GetPlcPosition(positions[slt]),
            //                          ((plungedMask & SlitterScorer.BIT(slt)) != 0),
            //                          ((dummyMask & SlitterScorer.BIT(slt)) != 0),
            //                          ((sepMask & SlitterScorer.BIT(slt)) != 0),
            //                          ((this.config.SlitterScorerHeader.SlitShafts[this.number].BrokenBlades & SlitterScorer.BIT(slt)) != 0),
            //                          (this.config.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset,
            //                          (this.config.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset,
            //                          locWideToNarrow, narrowPos, sltNotOk);
            this.slits[slt].Position(positions[slt], this.showGrindings ? int.Parse(this.numberOfGrindings[slt]) : this.GetPlcPosition(positions[slt]),
                                      ((plungedMask & SlitterScorer.BIT(slt)) != 0),
                                      ((sepMask & SlitterScorer.BIT(slt)) != 0),
                                      ((this.config.SlitterScorerHeader.SlitShafts[this.number].BrokenBlades & SlitterScorer.BIT(slt)) != 0),
                                      (this.config.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset,
                                      (this.config.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset,
                                      sltNotOk);

            if ((this.slits[slt].plunged) || this.debug || this.allSlits)
            {
              this.PaintSlit(this.slits[slt], e.Graphics);
              //agrassi_FI04-02-09
              //if (locWideToNarrow)
              //  this.PaintSlit(this.slits[slt], e.Graphics, true);
            }
          }
          //agrassi_FI08-09-2010_1
          else
          {
            this.ConfigSlits();
            this.Invalidate();
            return;
          }
				}
			}
		}

		private void ConfigSlits()
		{
			int typ = 0, seq = 0, cnt = 0, num = 0;
			for (int slt = 0; slt < this.slitCount; slt++)
			{
				if (slt >= cnt)
				{
					num = 0;
					do
					{
						char cfgChar = this.config.SlitterScorerHeader.SlitShafts[this.number].Configuration.ToCharArray(seq, 1)[0];
						if (char.IsDigit(cfgChar))
						{
							num *= 10;
							num += cfgChar - '0';
						}
						else
						{
							for (typ = 0; typ < SlitterScorerHeader.MAX_SLIT_TYPES_MACHINE; typ++)
								if (this.config.SlitterScorerHeader.SlitTypes[typ].Letter == cfgChar)
									break;
							cnt += num;
						}
					}
					while (char.IsDigit(this.config.SlitterScorerHeader.SlitShafts[this.number].Configuration.ToCharArray(seq++, 1)[0]));
				}
				this.slits[slt].Config(this.config.SlitterScorerHeader.SlitTypes[typ]);
				this.slits[slt].Width = this.Conv(this.config.SlitterScorerHeader.SlitTypes[typ].Width);
				this.slits[slt].Height = this.Height / 2;
				this.slits[slt].Top = (this.Height - this.slits[slt].Height) / 2;
        //agrassi_FI08-09-2010_1
        this.configured = true;
			}
		}

    //private void PaintSlit(Slit s, Graphics g, bool widePos)
 		private void PaintSlit(Slit s, Graphics g)
    {
			Color boardColor = System.Drawing.Color.FromArgb(255, 224, 192);
			Color plungedColor = Color.Green;
			Color notPlungedColor = Color.Yellow;
      //agrassi_FI04-02-09
      //Color dummyColor = Color.Azure;
			Color selectedColor = Color.Orange;
			Color selectedBrokenColor = Color.SteelBlue;
			Color notValidPositionColor = Color.LightYellow;
      //Color widePositionColor = Color.WhiteSmoke;
			SolidBrush boardBrush = new SolidBrush(boardColor);
			SolidBrush whiteBrush = new SolidBrush(Color.WhiteSmoke );
			Color slitColor = (s.notOK ? Color.Orange : Color.LightSteelBlue);
			slitColor = (s.broken ? Color.Red : slitColor);
			if (debug)
			{
				if (s.broken)
					slitColor = Color.Red;
				else if (s.plunged)
					slitColor = plungedColor;
        //agrassi_FI04-02-09
        //else if (s.dummy)
        //  slitColor = dummyColor;
				else
					slitColor = notPlungedColor;
			}
      //else if (s.wideToNarrow && widePos)
      //  slitColor = widePositionColor;
			else if ((this.scroll || this.multiSelect) && (s.selected))
				slitColor = (s.broken ? selectedBrokenColor : selectedColor);
			else if (!this.goodPositions && (!s.broken))
				slitColor = notValidPositionColor;
			int leftPos = s.Left;
			int slitPos = s.position;
      //agrassi_FI04-02-09
      //if (s.wideToNarrow && (!widePos))
      //{
      //  leftPos = s.LeftNarrow;
      //  slitPos = s.narrowPosition;
      //}
      Brush slitBrush = new SolidBrush(slitColor);
      //if (s.wideToNarrow && widePos)
      //  slitBrush = new HatchBrush(HatchStyle.Percent50, slitColor, Color.Transparent);
			Pen blackPen = new Pen(Color.Black);
			Pen blackThickPen = new Pen(Color.Black, 2.5f);
			int bladeFromLeft = this.Conv(s.config.BladeFromLeft);
			g.FillRectangle(slitBrush, leftPos, s.Top, s.Width, s.Height);
			Pen pen = (s.sep && debug) ? blackThickPen : blackPen;
			if (this.scroll && (s.number == this.scrollIndex))
				pen = new Pen(Color.Green, 2.3f);
			g.DrawRectangle(pen, leftPos, s.Top, s.Width - 1, s.Height - 1);
			if (this.debug || this.allSlits)
			{
				SizeF sz = g.MeasureString(s.number.ToString(), new Font("Arial", 10));
				g.DrawString((s.number + (this.debug ? 0 : 1)).ToString(), new Font("Arial", 10), new SolidBrush(Color.Black), leftPos + (s.Width - sz.Width) / 2, s.Top + (s.Height - sz.Height) / 2);
			}
			SizeF tb = g.MeasureString(slitPos.ToString(), new Font("Arial", 8), s.Width);
			SizeF plcTb = g.MeasureString(s.plcPosition.ToString(), new Font("Arial", 8), s.Width);
			Color cPos = Color.Black;
			if (s.notOK)
				cPos = Color.Red;
			if (this.number == 0)
			{
        //agrassi_FI07-04-08_8
        if (this.debug)
				  g.DrawString(slitPos.ToString(), new Font("Arial", 8), new SolidBrush(cPos), leftPos + (s.Width - tb.Width) / 2, s.Top + s.Height);
				if (this.debug || this.showGrindings)
					g.DrawString(s.plcPosition.ToString(), new Font("Arial", 8), new SolidBrush(s.notOK ? Color.Red : Color.Blue), leftPos + (s.Width - plcTb.Width) / 2, Math.Max(Math.Min(s.Top + s.Height + tb.Height, 0), this.Height-plcTb.Height));
			}
			else
			{
        //agrassi_FI07-04-08_8
        if (this.debug)
          g.DrawString(slitPos.ToString(), new Font("Arial", 8), new SolidBrush(cPos), leftPos + (s.Width - tb.Width) / 2, s.Top - tb.Height);
				if (this.debug || this.showGrindings)
          g.DrawString(s.plcPosition.ToString(), new Font("Arial", 8), new SolidBrush(s.notOK ? Color.Red : Color.Blue), leftPos + (s.Width - plcTb.Width) / 2, Math.Max(Math.Min(s.Top - tb.Height - plcTb.Height, 0), 0));
			}
			if (s.plunged)
			{
				int bladeProtrusion = s.Top / 3 + 1;
				if (this.number == 0)
					g.DrawLine(blackThickPen, leftPos + bladeFromLeft, s.Top, leftPos + bladeFromLeft, s.Top - bladeProtrusion);
				else
					g.DrawLine(blackThickPen, leftPos + bladeFromLeft, s.Top + s.Height, leftPos + bladeFromLeft, s.Top + s.Height + bladeProtrusion);
			}
      //agrassi_FI04-02-09
      //if (s.config.AnvilPresent && (this.allSlits || this.debug))
      //{
      //  int anvilHeight = (int)Math.Round(s.Height * 0.18);
      //  int anvilWidth = (int)Math.Round(s.Width * 0.2);
      //  int anvilBorder = 6;
      //  Point A, B, C, D;
      //  if (s.number == 0)
      //  {
      //    int hindrance = this.Conv(ANVIL_ARM_WIDTH - s.config.BladeFromLeft);
      //    if (this.number == 0)
      //    {
      //      A = new Point(leftPos, s.Top);
      //      B = new Point(leftPos - hindrance, s.Top);
      //      C = new Point(leftPos - hindrance, anvilBorder);
      //      D = new Point(leftPos + bladeFromLeft - anvilWidth / 2, anvilBorder);
      //    }
      //    else
      //    {
      //      A = new Point(leftPos, s.Top + s.Height - 1);
      //      B = new Point(leftPos - hindrance, s.Top + s.Height - 1);
      //      C = new Point(leftPos - hindrance, this.Height - anvilBorder);
      //      D = new Point(leftPos + bladeFromLeft - anvilWidth / 2, this.Height - anvilBorder);
      //    }
      //    g.DrawLine(blackPen, A, B);
      //    g.DrawLine(blackPen, B, C);
      //    g.DrawLine(blackPen, C, D);
      //  }
      //  else
      //  {
      //    int hindrance = this.Conv(ANVIL_ARM_WIDTH + s.config.BladeFromLeft - s.config.Width);
      //    if (this.number == 0)
      //    {
      //      A = new Point(leftPos + s.Width, s.Top);
      //      B = new Point(leftPos + s.Width + hindrance, s.Top);
      //      C = new Point(leftPos + s.Width + hindrance, anvilBorder);
      //      D = new Point(leftPos + bladeFromLeft + anvilWidth / 2, anvilBorder);
      //    }
      //    else
      //    {
      //      A = new Point(leftPos + s.Width, s.Top + s.Height - 1);
      //      B = new Point(leftPos + s.Width + hindrance, s.Top + s.Height - 1);
      //      C = new Point(leftPos + s.Width + hindrance, this.Height - anvilBorder);
      //      D = new Point(leftPos + bladeFromLeft + anvilWidth / 2, this.Height - anvilBorder);
      //    }
      //    g.DrawLine(blackPen, A, B);
      //    g.DrawLine(blackPen, B, C);
      //    g.DrawLine(blackPen, C, D);
      //  }
      //  if (this.number == 0)
      //  {
      //    g.DrawRectangle(blackPen, leftPos + bladeFromLeft - anvilWidth / 2, anvilBorder, anvilWidth, anvilHeight);
      //    g.FillRectangle(slitBrush, leftPos + bladeFromLeft - anvilWidth / 2, anvilBorder, anvilWidth, anvilHeight);
      //  }
      //  else
      //  {
      //    g.DrawRectangle(blackPen, leftPos + bladeFromLeft - anvilWidth / 2, this.Height - anvilBorder - anvilHeight, anvilWidth, anvilHeight);
      //    g.FillRectangle(slitBrush, leftPos + bladeFromLeft - anvilWidth / 2, this.Height - anvilBorder - anvilHeight, anvilWidth, anvilHeight);
      //  }
      //}
		}

		private int GetPlcPosition(int position)
		{
			return position - this.slitsAlignmentWithPlc[this.number];
		}

		private void SlitShaft_SizeChanged(object sender, EventArgs e)
		{
			this.Invalidate();
		}

		public int ScrollLeft()
		{
			if (!this.scroll) return -1;
			if (this.scrollIndex > 0)
			{
				this.scrollIndex--;
				this.Invalidate();
			}
			return this.scrollIndex;
		}

		public int ScrollRight()
		{
			if (!this.scroll) return -1;
			if (this.scrollIndex < this.slitCount - 1)
			{
				this.scrollIndex++;
				this.Invalidate();
			}
			return this.scrollIndex;
		}

		public int SetScrollLeft()
		{
			if (!this.scroll) return -1;
			this.scrollIndex = 0;
			this.Invalidate();
			return this.scrollIndex;
		}

		public int SetScrollRight()
		{
			if (!this.scroll) return -1;
			this.scrollIndex = this.slitCount - 1;
			this.Invalidate();
			return this.scrollIndex;
		}

		public bool ScrollLeftEnabled
		{ get { return (this.working && (this.scrollIndex > 0)); } }

		public bool ScrollRightEnabled
		{ get { return (this.working && (this.scrollIndex < this.slitCount - 1)); } }

		public bool ActiveScroll
		{
			set
			{
				this.scroll = value;
				this.Invalidate();
			}
		}

		public int GetScrollIndex()
		{
			return this.scrollIndex;
		}

		public void SetScrollIndex(int index)
		{
			if ((index >= 0) && (index < this.slitCount))
			{
				this.scrollIndex = index;
				this.Invalidate();
			}
		}

		private int Conv(int dim)
		{
			if (this.scale <= 0) return 0;
			return (int)Math.Round((double)dim / this.scale);
		}

		private void SlitShaft_MouseClick(object sender, MouseEventArgs e)
		{
			for (int slt = 0; slt < this.slitCount; slt++)
			{
				if ((e.X >= this.slits[slt].Left) && (e.X <= this.slits[slt].Left + this.slits[slt].Width) &&
						(e.Y >= this.slits[slt].Top) && (e.Y <= this.slits[slt].Top + this.slits[slt].Height))
				{
					this.scrollIndex = slt;
					if (this.changeFunction != null)
						this.changeFunction(slt, this.number);
					this.Invalidate();
					break;
				}
			}
		}

		private void SlitShaft_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			for (int slt = 0; slt < this.slitCount; slt++)
			{
				if ((e.X >= this.slits[slt].Left) && (e.X <= this.slits[slt].Left + this.slits[slt].Width) &&
						(e.Y >= this.slits[slt].Top) && (e.Y <= this.slits[slt].Top + this.slits[slt].Height))
				{
					if (this.multiSelect && (!this.blockMultiSelect))
					{
						this.slits[slt].selected = !this.slits[slt].selected;
						this.updateFunction();
						this.Invalidate();
						break;
					}
				}
			}
		}

		public int GetSelectedSlitMask()
		{
			int selectedMask = 0;
			for (int slt = 0; slt < this.slitCount; slt++)
				if (this.slits[slt].selected)
					selectedMask |= SlitterScorer.BIT(slt);
			return selectedMask;
		}

		public void ToggleSelection(int slt)
		{
			if ((slt >= 0) && (slt < this.slitCount))
			{
				if (this.multiSelect)
				{
					this.slits[slt].selected = !this.slits[slt].selected;
					this.updateFunction();
					this.Invalidate();
				}
			}
		}

		public void EnableMultiSelect(bool en)
		{
			this.multiSelect = en;
		}

		public void ResetMultiSelection()
		{
			for (int slt = 0; slt < this.slitCount; slt++)
				this.slits[slt].selected = false;
			this.Invalidate();
		}

		public void BlockMultiSelect(bool block)
		{
			this.blockMultiSelect = block;
		}

		//RFAZZI_FI01/07/2008_1.Added control for refresh the slitter only when needed
		public bool getIsBlockMultiSelectActive()
		{
			return this.blockMultiSelect;
		}

		public void SetPositioning(bool val)
		{
			this.timPositioning.Enabled = val;
			if ((!val) && (this.blinkColor))
			{
				this.blinkColor = false;
				this.Invalidate();
			}
		}

		private void timPositioning_Tick(object sender, EventArgs e)
		{
			this.blinkColor = !this.blinkColor;
			this.Invalidate();
		}
	}
}
