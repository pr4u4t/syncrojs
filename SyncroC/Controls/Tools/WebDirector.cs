#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;
#endregion

namespace SyncroC.Controls.Tools
{
	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
	
	public class WebDirectorPaddles : SyncroC.Controls.SyncroUserControl
  {
    private double scale;
    private WebDirector config;
    private SlitterScorer ssConfig;
    private int levelCount;
    private int paddleCount, paddleDist, offset, webDirWidth;
    private Paddle[] paddles;
    private bool debug = false, scroll=false, maskValid;
    private int scrollIndex = 0;
    private int[] slitLines;
    private int webWidth, paddleMask2level, paddleMask3level;
    private int rightBoardEdge, leftBoardEdge;
    //private Color boardColor = Color.FromArgb(255, 224, 192);
		private Color boardColor = Color.BurlyWood;
    private Color upperColor = Color.Orange;
    private Color middleColor = Color.Yellow;
    private Color lowerColor = Color.LightGreen;
    private float paddleHeightPerc = 0.75f;
    private float paddleTop = 0.15f;
    private bool firstTime = true;
    private MachineTools.ChangeShaftFunction changeFunction;

    public WebDirectorPaddles()
    {
      InitializeComponent();

      //For the designer
      this.config = new WebDirector();
      this.ssConfig = new SlitterScorer();
    }

    public void Config(LineConfig cfg, bool scroll, MachineTools.ChangeShaftFunction changeFunction)
    {
      this.config = cfg.WebDirectorConfig;
      this.levelCount = cfg.KnifeConfig.LevelCount;
      this.paddleCount = this.config.PaddlesNum;
      this.paddleDist = this.config.PaddlesDist;
      this.offset = this.config.Offset;
      this.webDirWidth = (this.paddleCount - 1) * this.paddleDist + this.config.PaddlesWidth;
      this.ssConfig = cfg.SlitterScorerConfig;
      this.paddles = new Paddle[this.paddleCount];
      for (int pad = 0; pad < this.paddleCount; pad++)
        this.paddles[pad] = new Paddle(pad);
      this.scroll = scroll;
      this.changeFunction = changeFunction;
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
// 
// WebDirectorPaddles
// 
      this.Name = "WebDirectorPaddles";
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WebDirectorPaddles_MouseUp);
      this.SizeChanged += new System.EventHandler(this.WebDirectorPaddles_SizeChanged);

    }

    #endregion

    public void Position(int webWidth, int paddleMask2level, int paddleMask3level, int[] slitLines, bool maskValid, bool debug)
    {
      this.slitLines = slitLines;
      this.webWidth = webWidth;
			this.leftBoardEdge = (this.ssConfig.SlitterScorerHeader.MachineWidth - webWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset;
			this.rightBoardEdge = (this.ssConfig.SlitterScorerHeader.MachineWidth + webWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset;
      this.paddleMask2level = paddleMask2level;
      this.paddleMask3level = paddleMask3level;
      this.debug = debug;
      this.maskValid = maskValid;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      //agrassi_FI18-06-08
      e.Graphics.Clear(Color.WhiteSmoke);
			this.scale = (double)this.ssConfig.SlitterScorerHeader.MachineWidth / (double)this.Width;
      //Paint board
      int bWidth = this.Conv(this.webWidth);
      e.Graphics.FillRectangle(new SolidBrush(this.boardColor), this.Conv(this.leftBoardEdge), 0, bWidth, this.Height);
      //Paint slit lines
      if (slitLines != null)
        for (int lin = 0; lin < slitLines.Length; lin++)
          if (slitLines[lin] > 0)
          {
            int pos = this.Conv(slitLines[lin]);
            e.Graphics.DrawLine(new Pen(Color.WhiteSmoke , 2.3f), pos, 0, pos, this.Height);
          }
      //Configure paddles
      if (this.firstTime)
      {
        this.ConfigPaddles();
        this.firstTime = false;
      }
      //Paint paddles
      int legendHeight = this.ConfigWebDirector(e.Graphics);
      for (int pad = 0; pad < this.paddleCount; pad++)
      {
        //agrassi_FI11-03-09_1
        //int realLeftPos = (this.ssConfig.SlitterScorerHeader.MachineWidth - this.webDirWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset + this.paddleDist * pad + this.offset;
        int realLeftPos = (this.ssConfig.SlitterScorerHeader.MachineWidth - this.webDirWidth) / 2 + this.ssConfig.SlitterScorerHeader.BoardCenterOffset + this.paddleDist * pad - this.offset;
        Color cFill = (this.maskValid ? this.lowerColor : Color.LightGray);
        if (this.maskValid)
        {
          if ((this.paddleMask2level & SlitterScorer.BIT(pad)) != 0)
          {
            cFill = this.middleColor;
            if ((this.levelCount==2) || ((this.paddleMask3level & SlitterScorer.BIT(pad)) != 0)) 
              cFill = this.upperColor;
          }
        }
        this.paddles[pad].Left = this.Conv(realLeftPos);
        this.paddles[pad].Position(realLeftPos, cFill);
        this.PaintPaddle(this.paddles[pad], e.Graphics);
      }
      if (debug)
      {
        string mask2lev = "2-level=" + this.paddleMask2level.ToString();
        SizeF s2 = e.Graphics.MeasureString(mask2lev, new Font("Arial", 8));
        e.Graphics.DrawString(mask2lev, new Font("Arial", 8), new SolidBrush(Color.Black), 4, legendHeight + 10);
        string mask2levh = "(0x" + this.paddleMask2level.ToString("X")+")";
        SizeF s2h = e.Graphics.MeasureString(mask2levh, new Font("Arial", 8));
        e.Graphics.DrawString(mask2levh, new Font("Arial", 8), new SolidBrush(Color.Black), 4, legendHeight + 10 + s2.Height);
        if (this.levelCount==3) 
        {
          string mask3lev = "3-level=" + this.paddleMask3level.ToString();
          SizeF s3 = e.Graphics.MeasureString(mask3lev, new Font("Arial", 8));
          e.Graphics.DrawString(mask3lev, new Font("Arial", 8), new SolidBrush(Color.Black), 4, legendHeight + 10 + s2.Height + s2h.Height);
          string mask3levh = "(0x" + this.paddleMask3level.ToString("X")+")";
          SizeF s3h = e.Graphics.MeasureString(mask3levh, new Font("Arial", 8));
          e.Graphics.DrawString(mask3levh, new Font("Arial", 8), new SolidBrush(Color.Black), 4, legendHeight + 10 + s2.Height + s2h.Height + s3.Height);
        }
      }
    }

    private void ConfigPaddles()
    {
      for (int pad = 0; pad < this.paddleCount; pad++)
      {
        this.paddles[pad].Config(this.config.PaddlesWidth);
        this.paddles[pad].Width = this.Conv(this.paddles[pad].configWidth);
        this.paddles[pad].Top = (int)(this.Height * paddleTop);
        this.paddles[pad].Height = (int)(this.Height * paddleHeightPerc);
      }
    }

    private void PaintPaddle(Paddle p, Graphics g)
    {
      g.FillRectangle(new SolidBrush(p.levelColor), p.Left, p.Top, p.Width, p.Height);
      SizeF num = g.MeasureString((p.number + 1).ToString(), new Font("Arial", 9));
      g.DrawString((p.number + 1).ToString(), new Font("Arial", 9), new SolidBrush(Color.Black), 
                    p.Left + (p.Width- num.Width) / 2, p.Top + (p.Height - num.Height) / 2);
      if (this.scroll && (p.number == this.scrollIndex))
      {
        Point A = new Point(p.Left, (int)(p.Top + p.Height));
        Point B = new Point(p.Left, (int)p.Top);
        Point C = new Point(p.Left + p.Width, (int)p.Top);
        Point D = new Point(p.Left + p.Width, (int)(p.Top + p.Height));
        g.DrawLines(new Pen(Color.Red, 2.3f), new Point[] { A, B, C, D });
      }
      if (debug)
      {
        SizeF pl = g.MeasureString(p.leftPosition.ToString(), new Font("Arial", 7));
        g.DrawString(p.leftPosition.ToString(), new Font("Arial", 7), new SolidBrush(Color.Blue), 
                              p.Left - pl.Width / 2, p.Top - pl.Height);
        SizeF pr = g.MeasureString((p.leftPosition + p.configWidth).ToString(), new Font("Arial", 7));
        g.DrawString((p.leftPosition + p.configWidth).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), 
                              p.Left + p.Width - pr.Width / 2, p.Top - pl.Height - pr.Height);
      }
    }

    private int ConfigWebDirector(Graphics g)
    {
      const int UP_EDGE = 8, LEFT_EDGE = 6, SQUARE_SIDE=20, PAD=5, ARROW_PAD=5, ARROW_HEIGHT=12, ARROW_WIDTH=15;
      int top = UP_EDGE;
      g.FillRectangle(new SolidBrush(this.upperColor), LEFT_EDGE, top, SQUARE_SIDE, SQUARE_SIDE);
      if (this.levelCount==3)
      {
        top += PAD + SQUARE_SIDE;
        g.FillRectangle(new SolidBrush(this.middleColor), LEFT_EDGE, top, SQUARE_SIDE, SQUARE_SIDE);
      }
      top += PAD + SQUARE_SIDE;
      g.FillRectangle(new SolidBrush(this.lowerColor), LEFT_EDGE, top, SQUARE_SIDE, SQUARE_SIDE);
      Point A = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD, UP_EDGE + ARROW_HEIGHT);
      Point B = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD + ARROW_WIDTH / 2, UP_EDGE);
      Point C = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD + ARROW_WIDTH, UP_EDGE + ARROW_HEIGHT);
      Point D = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD + ARROW_WIDTH, top + SQUARE_SIDE);
      Point E = new Point(LEFT_EDGE + SQUARE_SIDE + ARROW_PAD , top + SQUARE_SIDE);
      g.FillPolygon(new SolidBrush(Color.Black), new Point[] { A, B, C, D, E });
      return E.Y;
    }

    private void WebDirectorPaddles_SizeChanged(object sender, EventArgs e)
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
      if (this.scrollIndex < this.paddleCount - 1)
      {
        this.scrollIndex++;
        this.Invalidate();
      }
      return this.scrollIndex;
    }

    private int Conv(int dim)
    {
      if (this.scale <= 0) return 0;
      return (int)Math.Round((double)dim / this.scale);
    }

    private void WebDirectorPaddles_MouseUp(object sender, MouseEventArgs e)
    {
      for (int pad = 0; pad < this.paddleCount; pad++)
      {
        if ((e.X >= this.paddles[pad].Left) && (e.X <= this.paddles[pad].Left + this.paddles[pad].Width) &&
            (e.Y >= this.paddles[pad].Top) && (e.Y <= this.paddles[pad].Top + this.paddles[pad].Height))
        {
          this.scrollIndex = pad;
          if (this.changeFunction != null)
            this.changeFunction(pad, 0);
          this.Invalidate();
          break;
        }
      }
    }
  }
}
