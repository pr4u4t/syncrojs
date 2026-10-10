#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;
using SyncroC.Controls.Colors;

#endregion


namespace SyncroC.Controls.Tools
{
	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
	
	public class ScoreShaft : SyncroC.Controls.SyncroUserControl
  {
    //CM7 19-09-2005
    const int AUX_NUMBER = 2;
    private double scale;
    private SyncroC.DataBase.LineConfig.SlitterScorer config;
    private int number;
    private int scoreCount;
    private Score[] scores;
    private Hashtable colorTable;
    private bool debug=false;
    //private Color boardColor = System.Drawing.Color.FromArgb(255, 224, 192);
		private Color boardColor = Color.BurlyWood;
		private Color[] colorList ={Color.Cyan, Color.Pink, Color.LightGreen, Color.Yellow, Color.Orange, 
                                Color.Violet, Color.Beige, Color.LightSalmon, Color.Brown, Color.Fuchsia};
    private int[] scoreLines, slitLines, tearTapeLines;
    private int webWidth;
    private bool working;
    //agrassi_FI04-02-09
    //private int[] positions, upRelPositions, measureIndex, leftPositions, scoreGap, shaftGap;
    private int[] positions;
    private int startScorer, scoringCount;
    private int rightBoardEdge, leftBoardEdge;
    private bool support;
    //agrassi_FI04-02-09
    //private PositType[] posType;
    private bool firstTime = true;

    public ScoreShaft(int number)
    {
      InitializeComponent();
      this.number = number;

      //For the designer
      this.config = new SlitterScorer();
			this.config.SlitterScorerHeader.ScoreShafts[this.number].Enabled = true;
    }

    public void Config(SlitterScorer ss, Hashtable tbl)
    {
      this.config = ss;
			this.scoreCount = this.config.SlitterScorerHeader.ScoreShafts[this.number].GetScoreCount();
      this.scores = new Score[this.scoreCount];
      for (int scr = 0; scr < this.scoreCount; scr++)
        this.scores[scr] = new Score(scr);
      this.colorTable = tbl;
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
// ScoreShaft
// 
      this.Name = "ScoreShaft";
      this.Size = new System.Drawing.Size(964, 120);
      this.SizeChanged += new System.EventHandler(this.ScoreShaft_SizeChanged);
      this.ResumeLayout(false);

    }

    #endregion

    //agrassi_FI04-02-09
    //public void Position(int webWidth, int[] positions, int[] upRelPositions, int[] leftPositions,
    //                        int[] scoreGap, int[] shaftGap, int startScorer, int scoringCount, int[] slitLines,
    //                        int[] scoreLines, int[] tearTapeLines, bool support, PositType[] posType, int[] measureIndex, bool debug)
    public void Position(int webWidth, int[] positions, int startScorer, int scoringCount, int[] slitLines,
                            int[] scoreLines, int[] tearTapeLines, bool support, bool debug)
    {
      this.webWidth = webWidth;
			this.leftBoardEdge = (this.config.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
			this.rightBoardEdge = (this.config.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
      this.slitLines = slitLines;
      this.scoreLines = scoreLines;
      this.positions = positions;
      //agrassi_FI04-02-09
      //this.upRelPositions = upRelPositions;
      this.startScorer = startScorer;
      this.scoringCount = scoringCount;
      this.support = support;
      //this.posType = posType;
      //this.measureIndex = measureIndex;
      //this.leftPositions = leftPositions;
      //this.scoreGap = scoreGap;
      //this.shaftGap = shaftGap;
      this.debug = debug;
      this.working = true;
      this.tearTapeLines = tearTapeLines;
      this.Invalidate();
    }

    public void NotWorkingShaft(int webWidth, int[] slitLines, int[] scoreLines, int[] tearTapeLines, bool debug)
    {
      this.webWidth = webWidth;
			this.leftBoardEdge = (this.config.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
			this.rightBoardEdge = (this.config.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.config.SlitterScorerHeader.BoardCenterOffset;
      this.positions = null;
      //agrassi_FI04-02-09
      //this.upRelPositions = null;
      this.startScorer = 0;
      this.scoringCount = 0;
      this.debug = debug;
      this.working = false;
      this.slitLines = slitLines;
      this.scoreLines = scoreLines;
      this.tearTapeLines = tearTapeLines;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
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
				int pos = this.Conv(this.config.SlitterScorerHeader.MachineWidth / 2 + this.config.SlitterScorerHeader.BoardCenterOffset);
        e.Graphics.DrawLine(new Pen(Color.Violet), pos, 0, pos, this.Height);
      }
      //Paint shaft
      int shaftHeight = (int)Math.Round(this.Height * 0.5);
      //CM7 19-09-2005
			Color shaftColor = (this.config.SlitterScorerHeader.ScoreShafts[this.number].Enabled ? (this.working ? Color.Silver : Color.FromArgb(230, 230, 230)) : Color.Red);
			if ((this.number == AUX_NUMBER) && (!this.config.SlitterScorerHeader.AuxPresent))
        shaftColor = Color.WhiteSmoke;
      e.Graphics.FillRectangle(new SolidBrush(shaftColor), 0, (this.Height - shaftHeight) / 2, this.Width, shaftHeight);
      string shaftNum = (this.number < AUX_NUMBER ? (this.number + 1).ToString() : "A");
      SizeF n = e.Graphics.MeasureString(shaftNum, new Font("Arial", 10, FontStyle.Bold));
      e.Graphics.DrawString(shaftNum, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black),
                              0, (this.Height - n.Height) / 2);
      //Configure scores
      if (this.firstTime)
      {
        this.ConfigScores();
        this.firstTime = false;
      }
      //Paint scores
      if (this.working)
      {
        for (int scr = 0; scr < this.scoreCount; scr++)
        {
          this.scores[scr].Left = this.Conv(positions[scr]) - this.scores[scr].Width / 2;
          //agrassi_FI04-02-09
          //this.scores[scr].Position(this.positions[scr], this.upRelPositions[scr], this.leftPositions[scr], 
          //                          this.scoreGap[scr], this.shaftGap[scr], this.posType[scr], this.measureIndex[scr]);
          this.scores[scr].Position(this.positions[scr]);
          this.PaintScore(this.scores[scr], e.Graphics);
        }
      }
    }

    private void ConfigScores()
    {
      int typ = 0, seq = 0, cnt = 0, num = 0;
      for (int scr = 0; scr < this.scoreCount; scr++)
      {
        if (scr >= cnt)
        {
          num = 0;
          do
          {
						char cfgChar = this.config.SlitterScorerHeader.ScoreShafts[number].Configuration.ToCharArray(seq, 1)[0];
            if (char.IsDigit(cfgChar))
            {
              num *= 10;
              num += cfgChar - '0';
            }
            else
            {
              if (!this.colorTable.Contains(cfgChar))
                this.colorTable.Add(cfgChar, colorList[this.colorTable.Count]);
              for (typ = 0; typ < SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE; typ++)
								if (this.config.SlitterScorerHeader.ScoreTypes[typ].Letter == cfgChar)
                  break;
              cnt += num;
            }
          }
					while (char.IsDigit(this.config.SlitterScorerHeader.ScoreShafts[number].Configuration.ToCharArray(seq++, 1)[0]));
        }
        if (typ < SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE)
        {
          char parity='X';
					if (this.config.SlitterScorerHeader.ScoreShafts[this.number].Parity.Length > scr)
						parity = this.config.SlitterScorerHeader.ScoreShafts[this.number].Parity.ToCharArray(scr, 1)[0];
					this.scores[scr].Config(this.config.SlitterScorerHeader.ScoreTypes[typ], parity, this.colorTable);
					this.scores[scr].Width = this.Conv(this.config.SlitterScorerHeader.ScoreTypes[typ].Scorer.Width);
          this.scores[scr].Height = (int)(this.Height * 0.6);
          this.scores[scr].Top = (this.Height - this.scores[scr].Height) / 2;
        }
      }
    }

    private void PaintScore(Score s, Graphics g)
    {
      Color boardColor = System.Drawing.Color.FromArgb(255, 224, 192);
      SolidBrush boardBrush = new SolidBrush(boardColor);
      SolidBrush whiteBrush = new SolidBrush(Color.WhiteSmoke );
      Color c=Color.WhiteSmoke ;
      if (s.colorTable.Contains(s.config.Letter))
        c = (Color)s.colorTable[s.config.Letter];
      Brush scoreBrush = new SolidBrush(c);
      if ((this.support) && (s.number >= this.startScorer) && (s.number < this.startScorer + this.scoringCount))
        scoreBrush = new HatchBrush(HatchStyle.Percent25, c, Color.Transparent);
      g.FillRectangle(scoreBrush, s.Left, s.Top, s.Width, s.Height);
      c = Color.Black;
      if (debug)
      {
        if (s.parity == 'O')
          c = Color.Blue;
        else if (s.parity == 'E')
          c = Color.Brown;
      }
      float thick = 0.6f;
      //agrassi_FI04-02-09
      //if (debug && (!this.support) && (s.number >= this.startScorer) && (s.number < this.startScorer + this.scoringCount) && (s.upRelPosition != 0))
      //  thick = 2.8f;
      g.DrawRectangle(new Pen(c, thick), s.Left, s.Top, s.Width - 1, s.Height - 1);
      if (this.debug)
      {
        float top = s.Top;
        SizeF sz = g.MeasureString(s.config.Letter.ToString(), new Font("Arial", 9));
        g.DrawString(s.config.Letter.ToString(), new Font("Arial", 9), new SolidBrush(Color.Black), s.Left +(s.Width - sz.Width) / 2, top);
        top += sz.Height;
        SizeF t = g.MeasureString(s.number.ToString(), new Font("Arial", 7));
        g.DrawString(s.number.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), s.Left + (s.Width - t.Width) / 2, top);
        top += t.Height;
        //agrassi_FI04-02-09
        //if (s.measureIndex >= 0)
        //{
        //  SizeF gp = g.MeasureString(s.scoreGap.ToString(), new Font("Arial", 7));
        //  g.DrawString(s.scoreGap.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), s.Left + (s.Width - gp.Width) / 2, top);
        //  top += gp.Height;
        //  SizeF pt = g.MeasureString(ScoreMeasure.ConvertPositTypeToString(s.posType), new Font("Arial", 7));
        //  g.DrawString(ScoreMeasure.ConvertPositTypeToString(s.posType), new Font("Arial", 7), new SolidBrush(Color.Black), s.Left + (s.Width - pt.Width) / 2, top);
        //  top += pt.Height;
        //  SizeF m = g.MeasureString(s.measureIndex.ToString(), new Font("Arial", 7));
        //  g.DrawString(s.measureIndex.ToString(), new Font("Arial", 7), new SolidBrush(Color.Red), s.Left + (s.Width - m.Width) / 2, top);
        //}
      }
      SizeF tb = g.MeasureString(s.position.ToString(), new Font("Arial", 8), 100);
      if ((s.number >= this.startScorer - 1) && (s.number <= this.startScorer + this.scoringCount))
      {
        if ((s.number % 2) == 0)
        {
          //agrassi_FI07-04-08_8
          //g.DrawString(s.position.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), s.Left + (s.Width - tb.Width) / 2, s.Top - tb.Height);
          if (debug)
          {
            g.DrawString(s.position.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), s.Left + (s.Width - tb.Width) / 2, s.Top - tb.Height);
            //agrassi_FI04-02-09
            //SizeF sz = g.MeasureString(s.leftPosition.ToString(), new Font("Arial", 6.5f));
            //g.DrawString(s.leftPosition.ToString(), new Font("Arial", 6.5f), new SolidBrush(Color.Green), s.Left + (s.Width - sz.Width) / 2, s.Top + s.Height);
            //if ((s.number >= this.startScorer) && (s.number < this.startScorer + this.scoringCount))
            //{
            //  SizeF sx = g.MeasureString(s.shaftGap.ToString(), new Font("Arial", 6.5f));
            //  g.DrawString(s.shaftGap.ToString(), new Font("Arial", 6.5f), new SolidBrush(Color.Red), s.Left + (s.Width - sx.Width) / 2, s.Top + s.Height + sz.Height);
            //}
          }
        }
        else
        {
          //agrassi_FI07-04-08_8
          //g.DrawString(s.position.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), s.Left + (s.Width - tb.Width) / 2, s.Top + s.Height);
          if (debug)
          {
            g.DrawString(s.position.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), s.Left + (s.Width - tb.Width) / 2, s.Top + s.Height);
            //agrassi_FI04-02-09
            //SizeF sz = g.MeasureString(s.leftPosition.ToString(), new Font("Arial", 6.5f));
            //g.DrawString(s.leftPosition.ToString(), new Font("Arial", 6.5f), new SolidBrush(Color.Green), s.Left + (s.Width - sz.Width) / 2, s.Top - sz.Height);
            //if ((s.number >= this.startScorer) && (s.number < this.startScorer + this.scoringCount))
            //{
            //  SizeF sx = g.MeasureString(s.shaftGap.ToString(), new Font("Arial", 6.5f));
            //  g.DrawString(s.shaftGap.ToString(), new Font("Arial", 6.5f), new SolidBrush(Color.Black), s.Left + (s.Width - sx.Width) / 2, s.Top - sz.Height - sx.Height);
            //}
          }
        }
      }
    }

    private void ScoreShaft_SizeChanged(object sender, EventArgs e)
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
