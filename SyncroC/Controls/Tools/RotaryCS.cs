using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;

namespace SyncroC.Controls.Tools
{
	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.

	public class RotarySectors : SyncroC.Controls.SyncroUserControl
	{
    private double scale;
    private Rotary config;
    private SlitterScorer ssConfig;
    private int levelCount;
    private int sectorsNum, sectorWidth, edgeSectorWidth, sectorsDist, offset, rcsWidth;
    private Sector[] sectors;
    private bool debug = false, maskValid;
    private int[] prevSlitLines, nextSlitLines;
    //agrassiFI27-08-2010_1
    private int[] nonSepPrevSlitLines, nonSepNextSlitLines;
    private LevelName[] prevLevels, nextLevels;
    private bool prevLeftSepSlit, prevRightSepSlit, nextLeftSepSlit, nextRightSepSlit;
    private int prevWebWidth, nextWebWidth, sectorMask;
    private int prevRightBoardEdge, nextRightBoardEdge, prevLeftBoardEdge, nextLeftBoardEdge;
    //private Color boardColor = Color.FromArgb(255, 224, 192);
		private Color boardColor = Color.BurlyWood;
		private Color[] levelColor;
    private float sectorHeightPerc = 0.1f;
    private float sectorTop = 0.45f;
    private float sectorTopVariationPerc = 0.009f;
    private bool firstTime = true;
    private string notValidSectorsMask, missingNextSetup;

    private System.ComponentModel.IContainer components = null;

    public RotarySectors()
    {
			InitializeComponent();
      //For the designer
      this.config = new Rotary();
      this.ssConfig = new SlitterScorer();
      this.levelColor=new Color[(int)LevelName.Upper+1];
      this.levelColor[(int)LevelName.Nolevel] = Color.WhiteSmoke;
      this.levelColor[(int)LevelName.Lower] = Color.LightGreen;
      this.levelColor[(int)LevelName.Middle] = Color.Yellow;
      this.levelColor[(int)LevelName.Upper] = Color.Orange;
    }

    public void Config(LineConfig cfg, string notValidSectorsMask, string missingNextSetup)
    {
      this.config = cfg.RotaryConfig;
      this.ssConfig = cfg.SlitterScorerConfig;
      this.levelCount = cfg.KnifeConfig.LevelCount;
      this.sectorsNum = this.config.FormatData.SectorsNum;
      this.sectorWidth = this.config.FormatData.SectorWidth;
      this.edgeSectorWidth = this.config.FormatData.EdgeSectorWidth;
      this.sectorsDist = this.config.FormatData.SectorsDist;
      this.offset = this.config.Offset;
      this.rcsWidth = (this.sectorsNum - 2) * this.sectorWidth +2*this.edgeSectorWidth+
              (this.sectorsNum - 1) * this.sectorsDist;
      this.sectors = new Sector[this.sectorsNum];
      for (int sec = 0; sec < this.sectorsNum; sec++)
        this.sectors[sec] = new Sector(sec);
      this.notValidSectorsMask = notValidSectorsMask;
      this.missingNextSetup = missingNextSetup;
    }

    /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
// 
// RotarySectors
// 
      this.Name = "RotarySectors";
      this.SizeChanged += new System.EventHandler(this.RotarySectors_SizeChanged);

    }
		#endregion
    //agrassiFI27-08-2010_1
    //public void Position(int sectorMask, int prevWebWidth, int nextWebWidth, int[] prevSepSlitLines, 
    //                          int[] nextSepSlitLines, LevelName[] prevLevels, LevelName[] nextLevels,
    //                          bool prevLeftSepSlit, bool prevRightSepSlit, bool nextLeftSepSlit, 
    //                          bool nextRightSepSlit, bool maskValid, bool debug)
    public void Position(int sectorMask, int prevWebWidth, int nextWebWidth, int[] prevSepSlitLines,
                              int[] nextSepSlitLines, int[] nonSepPrevSlitLines, int[] nonSepNextSlitLines, LevelName[] prevLevels, LevelName[] nextLevels,
                              bool prevLeftSepSlit, bool prevRightSepSlit, bool nextLeftSepSlit, 
                              bool nextRightSepSlit, bool maskValid, bool debug)
    {
      this.prevSlitLines = prevSepSlitLines;
      this.nextSlitLines = nextSepSlitLines;
      //agrassiFI27-08-2010_1
      this.nonSepPrevSlitLines = nonSepPrevSlitLines;
      this.nonSepNextSlitLines = nonSepNextSlitLines;
      this.prevWebWidth = prevWebWidth;
      this.nextWebWidth = nextWebWidth;
      this.prevLevels = prevLevels;
      this.nextLevels = nextLevels;
      this.prevLeftSepSlit = prevLeftSepSlit;
      this.prevRightSepSlit = prevRightSepSlit;
      this.nextLeftSepSlit = prevRightSepSlit;
      this.nextRightSepSlit = nextRightSepSlit;
			this.prevLeftBoardEdge = (this.ssConfig.SlitterScorerHeader.MachineWidth - prevWebWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset;
			this.nextLeftBoardEdge = (this.ssConfig.SlitterScorerHeader.MachineWidth - nextWebWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset;
			this.prevRightBoardEdge = (this.ssConfig.SlitterScorerHeader.MachineWidth + prevWebWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset;
			this.nextRightBoardEdge = (this.ssConfig.SlitterScorerHeader.MachineWidth + nextWebWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset;

      this.sectorMask = sectorMask;
      this.maskValid = maskValid;
      this.debug = debug;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      //agrassi_FI18-06-08
      e.Graphics.Clear(Color.WhiteSmoke);
			this.scale = (double)this.ssConfig.SlitterScorerHeader.MachineWidth / (double)this.Width;
      //Paint board with levels
      int OCLine = (int)(this.Height * this.sectorTop);
      int effLines=0;
      int[] effectiveSlitLines;
      //Previous
      if ((this.prevSlitLines != null) && (this.prevLevels != null))
      {
        int prevBWidth = this.Conv(this.prevWebWidth);
        effectiveSlitLines = new int[this.prevSlitLines.Length+2];
        if (!this.prevLeftSepSlit)
          effectiveSlitLines[effLines++] = this.prevLeftBoardEdge;
        int cnt=0;
        while ((this.prevSlitLines[cnt] > 0) && (cnt<this.prevSlitLines.Length))
          effectiveSlitLines[effLines++] = this.prevSlitLines[cnt++];
        if (!this.prevRightSepSlit)
          effectiveSlitLines[effLines++] = this.prevRightBoardEdge;
        if (effLines == 0)
        {
          effLines = 2;
          effectiveSlitLines[0] = this.prevLeftBoardEdge;
          effectiveSlitLines[1] = this.prevRightBoardEdge;
        }
        e.Graphics.FillRectangle(new SolidBrush(this.boardColor), this.Conv(this.prevLeftBoardEdge), 0,
                    this.Conv(effectiveSlitLines[0] - this.prevLeftBoardEdge), OCLine);
        for (int lin = 0; lin < effLines-1; lin++)
          e.Graphics.FillRectangle(new SolidBrush(this.levelColor[(int)this.prevLevels[lin]]), this.Conv(effectiveSlitLines[lin]), 0,
                   this.Conv(effectiveSlitLines[lin + 1] - effectiveSlitLines[lin]), OCLine);
        e.Graphics.FillRectangle(new SolidBrush(this.boardColor), this.Conv(effectiveSlitLines[effLines-1]), 0,
                    this.Conv(this.prevRightBoardEdge - effectiveSlitLines[effLines-1]), OCLine);
        cnt = 0;
        while (this.prevSlitLines[cnt] > 0)
        {
          int pos = this.Conv(this.prevSlitLines[cnt]);
          e.Graphics.DrawLine(new Pen(Color.WhiteSmoke , 2.0f), pos, 0, pos, OCLine);
          if (this.debug)
          {
            SizeF s = e.Graphics.MeasureString(this.prevSlitLines[cnt].ToString(), new Font("Arial", 8));
            e.Graphics.DrawString(this.prevSlitLines[cnt].ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, 0);
          }
          cnt++;
        }
        //agrassiFI27-08-2010_1
        cnt = 0;
        while (this.nonSepPrevSlitLines[cnt] > 0)
        {
          int pos = this.Conv(this.nonSepPrevSlitLines[cnt]);
          e.Graphics.DrawLine(new Pen(Color.WhiteSmoke, 1.5f), pos, 0, pos, OCLine);
          if (this.debug)
          {
            SizeF s = e.Graphics.MeasureString(this.nonSepPrevSlitLines[cnt].ToString(), new Font("Arial", 8));
            e.Graphics.DrawString(this.nonSepPrevSlitLines[cnt].ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, 0);
          }
          cnt++;
        }
        if (this.debug)
        {
          int pos = this.Conv(this.prevLeftBoardEdge);
          SizeF s = e.Graphics.MeasureString(this.prevLeftBoardEdge.ToString(), new Font("Arial", 8));
          e.Graphics.DrawString(this.prevLeftBoardEdge.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, 35);
          pos = this.Conv(this.prevRightBoardEdge);
          s = e.Graphics.MeasureString(this.prevRightBoardEdge.ToString(), new Font("Arial", 8));
          e.Graphics.DrawString(this.prevRightBoardEdge.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, 35);
        }
      }
      //Next
      effLines = 0;
      if ((this.nextSlitLines != null) && (this.nextLevels != null))
      {
        int nextBWidth = this.Conv(this.nextWebWidth);
        effectiveSlitLines = new int[this.nextSlitLines.Length + 2];
        if (!this.nextLeftSepSlit)
          effectiveSlitLines[effLines++] = this.nextLeftBoardEdge;
        int cnt = 0;
        while ((this.nextSlitLines[cnt] > 0) && (cnt < this.nextSlitLines.Length))
          effectiveSlitLines[effLines++] = this.nextSlitLines[cnt++];
        if (!this.nextRightSepSlit)
          effectiveSlitLines[effLines++] = this.nextRightBoardEdge;
        if (effLines == 0)
        {
          effLines = 2;
          effectiveSlitLines[0] = this.nextLeftBoardEdge;
          effectiveSlitLines[1] = this.nextRightBoardEdge;
        }
        e.Graphics.FillRectangle(new SolidBrush(this.boardColor), this.Conv(this.nextLeftBoardEdge), OCLine,
                    this.Conv(effectiveSlitLines[0] - this.nextLeftBoardEdge), this.Height - OCLine);
        for (int lin = 0; lin < effLines - 1; lin++)
          e.Graphics.FillRectangle(new SolidBrush(this.levelColor[(int)this.nextLevels[lin]]), this.Conv(effectiveSlitLines[lin]), OCLine,
                   this.Conv(effectiveSlitLines[lin + 1] - effectiveSlitLines[lin]), this.Height - OCLine);
        e.Graphics.FillRectangle(new SolidBrush(this.boardColor), this.Conv(effectiveSlitLines[effLines - 1]), OCLine,
                    this.Conv(this.nextRightBoardEdge - effectiveSlitLines[effLines - 1]), this.Height - OCLine);
        cnt = 0;
        while (this.nextSlitLines[cnt] > 0)
        {
          int pos = this.Conv(this.nextSlitLines[cnt]);
          e.Graphics.DrawLine(new Pen(Color.WhiteSmoke , 2.0f), pos, OCLine, pos, this.Height);
          if (this.debug)
          {
            SizeF s = e.Graphics.MeasureString(this.nextSlitLines[cnt].ToString(), new Font("Arial", 8));
            e.Graphics.DrawString(this.nextSlitLines[cnt].ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, this.Height - s.Height);
          }
          cnt++;
        }
        //agrassiFI27-08-2010_1
        cnt = 0;
        while (this.nonSepNextSlitLines[cnt] > 0)
        {
          int pos = this.Conv(this.nonSepNextSlitLines[cnt]);
          e.Graphics.DrawLine(new Pen(Color.WhiteSmoke, 1.5f), pos, OCLine, pos, this.Height);
          if (this.debug)
          {
            SizeF s = e.Graphics.MeasureString(this.nonSepNextSlitLines[cnt].ToString(), new Font("Arial", 8));
            e.Graphics.DrawString(this.nonSepNextSlitLines[cnt].ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, this.Height - s.Height);
          }
          cnt++;
        }
        if (this.debug)
        {
          int pos = this.Conv(this.nextLeftBoardEdge);
          SizeF s = e.Graphics.MeasureString(this.nextLeftBoardEdge.ToString(), new Font("Arial", 8));
          e.Graphics.DrawString(this.nextLeftBoardEdge.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, this.Height -s.Height- 35);
          pos = this.Conv(this.nextRightBoardEdge);
          s = e.Graphics.MeasureString(this.nextRightBoardEdge.ToString(), new Font("Arial", 8));
          e.Graphics.DrawString(this.nextRightBoardEdge.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos - s.Width / 2, this.Height - s.Height- 35);
        }
      }
      //Configure sectors
      if (this.firstTime)
      {
        this.ConfigSectors();
        this.firstTime = false;
      }
      //Paint sectors
      int legendHeight = this.ConfigRCS(e.Graphics);
      if (this.maskValid)
      {
				int realLeftPos = (this.ssConfig.SlitterScorerHeader.MachineWidth - this.rcsWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset + this.offset;
				for (int sec = 0; sec < this.sectorsNum; sec++)
        {
          int secWidth = ((sec == 0) || (sec == this.sectorsNum - 1)) ? this.edgeSectorWidth : this.sectorWidth;
          bool cutting = ((this.sectorMask & SlitterScorer.BIT(sec)) != 0);
          this.sectors[sec].Left = this.Conv(realLeftPos);
          this.sectors[sec].Position(realLeftPos, cutting);
          this.PaintSector(this.sectors[sec], e.Graphics);
          realLeftPos += secWidth + this.sectorsDist;
        }
        if (debug)
        {
          string sectorMask = "mask=" + this.sectorMask.ToString();
          SizeF sm = e.Graphics.MeasureString(sectorMask, new Font("Arial", 8));
          e.Graphics.DrawString(sectorMask, new Font("Arial", 8), new SolidBrush(Color.Black), 4, legendHeight + 10);
          string sectorMaskh = "(0x" + this.sectorMask.ToString("X") + ")";
          SizeF smh = e.Graphics.MeasureString(sectorMaskh, new Font("Arial", 8));
          e.Graphics.DrawString(sectorMaskh, new Font("Arial", 8), new SolidBrush(Color.Black), 4, legendHeight + 10 + sm.Height);
        }
      }
      else
      {
        string msg = this.notValidSectorsMask;
        if (this.nextLevels == null)
          msg = this.missingNextSetup;
				int bco = this.ssConfig.SlitterScorerHeader.MachineWidth / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset + this.offset;
				SizeF s = e.Graphics.MeasureString(msg, new Font("Arial", 14));
        e.Graphics.DrawString(msg, new Font("Arial", 14), new SolidBrush(Color.Red), this.Conv(bco) - s.Width / 2, OCLine);
      }
    }

    private void ConfigSectors()
    {
      int midLeftSector = (this.sectorsNum-1) / 2;
      int midRightSector = this.sectorsNum / 2;
      for (int sec = 0; sec < this.sectorsNum; sec++)
      {
        int secWidth = ((sec == 0) || (sec == this.sectorsNum - 1)) ? this.edgeSectorWidth : this.sectorWidth;
        this.sectors[sec].Config(secWidth);
        this.sectors[sec].Width = this.Conv(this.sectors[sec].configWidth);
        this.sectors[sec].centerDist = ((sec <= midLeftSector) ? midLeftSector : midRightSector) - sec;
        this.sectors[sec].Top = (int)(this.Height * (this.sectorTop - Math.Abs(this.sectors[sec].centerDist) * this.sectorTopVariationPerc));
        this.sectors[sec].Height = (int)(this.Height * this.sectorHeightPerc);
      }
    }

    private void PaintSector(Sector s, Graphics g)
    {
      if (s.cutting)
      {
        g.FillRectangle(new SolidBrush(Color.LightGray), s.Left, s.Top, s.Width, s.Height);
        g.DrawRectangle(new Pen(Color.Black), s.Left, s.Top, s.Width, s.Height);
        SizeF num = g.MeasureString((s.number + 1).ToString(), new Font("Arial", 9));
        g.DrawString((s.number + 1).ToString(), new Font("Arial", 9), new SolidBrush(Color.Black),
                      s.Left + (s.Width - num.Width) / 2, s.Top + (s.Height - num.Height) / 2);
        if (debug)
        {
          SizeF pl = g.MeasureString(s.leftPosition.ToString(), new Font("Arial", 7));
          float top = (s.centerDist >= 0) ? s.Top + s.Height + pl.Height : s.Top - 2*pl.Height;
          g.DrawString(s.leftPosition.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black),
                                s.Left - pl.Width / 2, top);
          if (s.number==this.config.FormatData.SectorsNum-1)
            g.DrawString((s.leftPosition+s.configWidth).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black),
                                  s.Left + s.Width - pl.Width / 2, s.Top - 2 * pl.Height);
        }
      }
    }

    private int ConfigRCS(Graphics g)
    {
      const int UP_EDGE = 8, LEFT_EDGE = 6, SQUARE_SIDE = 20, PAD = 5, ARROW_PAD = 5, ARROW_HEIGHT = 12, ARROW_WIDTH = 15;
      int top = UP_EDGE;
      g.FillRectangle(new SolidBrush(this.levelColor[(int)LevelName.Upper]), LEFT_EDGE, top, SQUARE_SIDE, SQUARE_SIDE);
      if (this.levelCount == 3)
      {
        top += PAD + SQUARE_SIDE;
        g.FillRectangle(new SolidBrush(this.levelColor[(int)LevelName.Middle]), LEFT_EDGE, top, SQUARE_SIDE, SQUARE_SIDE);
      }
      top += PAD + SQUARE_SIDE;
      g.FillRectangle(new SolidBrush(this.levelColor[(int)LevelName.Lower]), LEFT_EDGE, top, SQUARE_SIDE, SQUARE_SIDE);
      Point A = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD, UP_EDGE + ARROW_HEIGHT);
      Point B = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD + ARROW_WIDTH / 2, UP_EDGE);
      Point C = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD + ARROW_WIDTH, UP_EDGE + ARROW_HEIGHT);
      Point D = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD + ARROW_WIDTH, top + SQUARE_SIDE);
      Point E = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD, top + SQUARE_SIDE);
      g.FillPolygon(new SolidBrush(Color.Black), new Point[] { A, B, C, D, E });
      return E.Y;
    }

    private void RotarySectors_SizeChanged(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private int Conv(int dim)
    {
      if (this.scale <= 0) return 0;
      return (int)Math.Round((double)dim / this.scale);
    }
  }
}

