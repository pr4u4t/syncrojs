using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;

namespace SyncroC.Controls
{
	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.
	
	public class SlitterScorerShaftStatus : SyncroC.Controls.SyncroUserControl
  {
    public delegate void PositioningTimer(int positioningSetup);
    private const int LEFT_MARGIN = 4;
    private const int TOP_MARGIN = 30;
    private const double SHAFT_WIDTH_PERC = 0.79;
    private const double GAP_WIDTH_PERC_ON_SHAFT = 0.12;
    private const double SHAFT_HEIGHT_PERC = 0.15;
    private const double SPACE_BTWN_SHAFTS_PERC = 0.06;
    private const double SPACE_BTWN_SLITS_AND_SCORES_PERC = 0.08;

    private Color COLOR_NOT_PRESENT_SHAFT = Color.WhiteSmoke;
    private Title ttl_ShaftStatus;
    private Label lbl_GapScoreAux;
    private Label lbl_GapScore1;
    private Label lbl_GapScore2;
    private Label lbl_StatusScoreAux;
    private Label lbl_ScrA_YN;
    private Label lbl_StatusScore1;
    private Label lbl_Scr1_YN;
    private Label lbl_StatusScore2;
    private Label lbl_Scr2_YN;
    private Label lbl_StatusSlit1;
    private Label lbl_Slt1_YN;
    private Label lbl_StatusSlit2;
    private Label lbl_Slt2_YN;
    private System.ComponentModel.IContainer components = null;
    private Color[] shaftColor;
    private string[] shaftMessage;
    private ShaftStatus[] stShaft;
    private int[] stpShaft;
    private bool auxPresent;
    private bool[] shaftEnabled;
    private int[] machId;
    private Timer timPositioning;
    private int[] machMod;
    private PositioningTimer externalFunction;

    public SlitterScorerShaftStatus()
    {
      InitializeComponent();
      this.timPositioning.Interval = 400;
      this.timPositioning.Tick += new EventHandler(this.PositioningBlink);
      this.shaftColor = new Color[Enum.GetValues(typeof(ShaftStatus)).Length];
      this.shaftEnabled = new bool[Enum.GetValues(typeof(Shaft)).Length];
      this.stpShaft = new int[Enum.GetValues(typeof(Shaft)).Length];
      this.stShaft = new ShaftStatus[Enum.GetValues(typeof(Shaft)).Length];
      this.shaftMessage = new string[Enum.GetValues(typeof(ShaftStatus)).Length];
      this.machId = new int[SlitterScorerHeader.SS_SETUPS];
      this.machMod = new int[SlitterScorerHeader.SS_SETUPS];

      this.shaftColor[(int)ShaftStatus.Disabled] = Color.Red;
      this.shaftColor[(int)ShaftStatus.NotPositioned] = Color.LightGray;
      this.shaftColor[(int)ShaftStatus.PositioningNow] = Color.MediumSeaGreen;
      this.shaftColor[(int)ShaftStatus.Positioned] = Color.MediumSeaGreen;
      this.shaftColor[(int)ShaftStatus.PositionedPart1] = Color.MediumSeaGreen;
      this.shaftColor[(int)ShaftStatus.Plunged] = Color.SteelBlue;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			this.ttl_ShaftStatus = new SyncroC.Controls.Title();
			this.lbl_GapScoreAux = new System.Windows.Forms.Label();
			this.lbl_GapScore1 = new System.Windows.Forms.Label();
			this.lbl_GapScore2 = new System.Windows.Forms.Label();
			this.lbl_StatusScoreAux = new System.Windows.Forms.Label();
			this.lbl_ScrA_YN = new System.Windows.Forms.Label();
			this.lbl_StatusScore1 = new System.Windows.Forms.Label();
			this.lbl_Scr1_YN = new System.Windows.Forms.Label();
			this.lbl_StatusScore2 = new System.Windows.Forms.Label();
			this.lbl_Scr2_YN = new System.Windows.Forms.Label();
			this.lbl_StatusSlit1 = new System.Windows.Forms.Label();
			this.lbl_Slt1_YN = new System.Windows.Forms.Label();
			this.lbl_StatusSlit2 = new System.Windows.Forms.Label();
			this.lbl_Slt2_YN = new System.Windows.Forms.Label();
			this.timPositioning = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// ttl_ShaftStatus
			// 
			this.ttl_ShaftStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
			this.ttl_ShaftStatus.color = SyncroC.Controls.Colors.SyncroColor.LightGreen;
			this.ttl_ShaftStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_ShaftStatus.ForeColor = System.Drawing.Color.Black;
			this.ttl_ShaftStatus.Location = new System.Drawing.Point(0, 0);
			this.ttl_ShaftStatus.Name = "ttl_ShaftStatus";
			this.ttl_ShaftStatus.Size = new System.Drawing.Size(358, 229);
			this.ttl_ShaftStatus.Style = SyncroC.Controls.TitleStyles.Small;
			this.ttl_ShaftStatus.TabIndex = 228;
			this.ttl_ShaftStatus.Text = "SHAFTS STATUS";
			this.ttl_ShaftStatus.TranslateText = true;
			this.ttl_ShaftStatus.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_GapScoreAux
			// 
			this.lbl_GapScoreAux.BackColor = System.Drawing.Color.LightGray;
			this.lbl_GapScoreAux.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_GapScoreAux.ForeColor = System.Drawing.Color.Black;
			this.lbl_GapScoreAux.Location = new System.Drawing.Point(326, 199);
			this.lbl_GapScoreAux.Name = "lbl_GapScoreAux";
			this.lbl_GapScoreAux.Size = new System.Drawing.Size(32, 30);
			this.lbl_GapScoreAux.TabIndex = 227;
			this.lbl_GapScoreAux.Text = "600";
			this.lbl_GapScoreAux.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_GapScoreAux.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_GapScore1
			// 
			this.lbl_GapScore1.BackColor = System.Drawing.Color.SteelBlue;
			this.lbl_GapScore1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_GapScore1.ForeColor = System.Drawing.Color.Black;
			this.lbl_GapScore1.Location = new System.Drawing.Point(326, 158);
			this.lbl_GapScore1.Name = "lbl_GapScore1";
			this.lbl_GapScore1.Size = new System.Drawing.Size(32, 30);
			this.lbl_GapScore1.TabIndex = 226;
			this.lbl_GapScore1.Text = "34";
			this.lbl_GapScore1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_GapScore1.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_GapScore2
			// 
			this.lbl_GapScore2.BackColor = System.Drawing.Color.MediumSeaGreen;
			this.lbl_GapScore2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_GapScore2.ForeColor = System.Drawing.Color.Black;
			this.lbl_GapScore2.Location = new System.Drawing.Point(326, 117);
			this.lbl_GapScore2.Name = "lbl_GapScore2";
			this.lbl_GapScore2.Size = new System.Drawing.Size(32, 30);
			this.lbl_GapScore2.TabIndex = 225;
			this.lbl_GapScore2.Text = "600";
			this.lbl_GapScore2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_GapScore2.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_StatusScoreAux
			// 
			this.lbl_StatusScoreAux.BackColor = System.Drawing.Color.LightGray;
			this.lbl_StatusScoreAux.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_StatusScoreAux.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_StatusScoreAux.Location = new System.Drawing.Point(78, 199);
			this.lbl_StatusScoreAux.Name = "lbl_StatusScoreAux";
			this.lbl_StatusScoreAux.Size = new System.Drawing.Size(280, 30);
			this.lbl_StatusScoreAux.TabIndex = 224;
			this.lbl_StatusScoreAux.Text = "Not Positoned";
			this.lbl_StatusScoreAux.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_StatusScoreAux.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_ScrA_YN
			// 
			this.lbl_ScrA_YN.BackColor = System.Drawing.Color.Black;
			this.lbl_ScrA_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_ScrA_YN.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_ScrA_YN.Location = new System.Drawing.Point(4, 199);
			this.lbl_ScrA_YN.Name = "lbl_ScrA_YN";
			this.lbl_ScrA_YN.Size = new System.Drawing.Size(74, 30);
			this.lbl_ScrA_YN.TabIndex = 223;
			this.lbl_ScrA_YN.Text = "SCORE A";
			this.lbl_ScrA_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_ScrA_YN.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_StatusScore1
			// 
			this.lbl_StatusScore1.BackColor = System.Drawing.Color.SteelBlue;
			this.lbl_StatusScore1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_StatusScore1.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_StatusScore1.Location = new System.Drawing.Point(78, 158);
			this.lbl_StatusScore1.Name = "lbl_StatusScore1";
			this.lbl_StatusScore1.Size = new System.Drawing.Size(280, 30);
			this.lbl_StatusScore1.TabIndex = 222;
			this.lbl_StatusScore1.Text = "Plunged setup 387455 - 2";
			this.lbl_StatusScore1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_StatusScore1.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_Scr1_YN
			// 
			this.lbl_Scr1_YN.BackColor = System.Drawing.Color.Black;
			this.lbl_Scr1_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Scr1_YN.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_Scr1_YN.Location = new System.Drawing.Point(4, 158);
			this.lbl_Scr1_YN.Name = "lbl_Scr1_YN";
			this.lbl_Scr1_YN.Size = new System.Drawing.Size(74, 30);
			this.lbl_Scr1_YN.TabIndex = 221;
			this.lbl_Scr1_YN.Text = "SCORE 1";
			this.lbl_Scr1_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_Scr1_YN.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_StatusScore2
			// 
			this.lbl_StatusScore2.BackColor = System.Drawing.Color.MediumSeaGreen;
			this.lbl_StatusScore2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_StatusScore2.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_StatusScore2.Location = new System.Drawing.Point(78, 117);
			this.lbl_StatusScore2.Name = "lbl_StatusScore2";
			this.lbl_StatusScore2.Size = new System.Drawing.Size(280, 30);
			this.lbl_StatusScore2.TabIndex = 220;
			this.lbl_StatusScore2.Text = "Positioned setup 387455 - 3";
			this.lbl_StatusScore2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_StatusScore2.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_Scr2_YN
			// 
			this.lbl_Scr2_YN.BackColor = System.Drawing.Color.Black;
			this.lbl_Scr2_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Scr2_YN.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_Scr2_YN.Location = new System.Drawing.Point(4, 117);
			this.lbl_Scr2_YN.Name = "lbl_Scr2_YN";
			this.lbl_Scr2_YN.Size = new System.Drawing.Size(74, 30);
			this.lbl_Scr2_YN.TabIndex = 219;
			this.lbl_Scr2_YN.Text = "SCORE 2";
			this.lbl_Scr2_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_Scr2_YN.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_StatusSlit1
			// 
			this.lbl_StatusSlit1.BackColor = System.Drawing.Color.SteelBlue;
			this.lbl_StatusSlit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_StatusSlit1.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_StatusSlit1.Location = new System.Drawing.Point(78, 71);
			this.lbl_StatusSlit1.Name = "lbl_StatusSlit1";
			this.lbl_StatusSlit1.Size = new System.Drawing.Size(280, 30);
			this.lbl_StatusSlit1.TabIndex = 218;
			this.lbl_StatusSlit1.Text = "Plunged setup 387455 - 2";
			this.lbl_StatusSlit1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_StatusSlit1.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_Slt1_YN
			// 
			this.lbl_Slt1_YN.BackColor = System.Drawing.Color.Black;
			this.lbl_Slt1_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Slt1_YN.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_Slt1_YN.Location = new System.Drawing.Point(4, 71);
			this.lbl_Slt1_YN.Name = "lbl_Slt1_YN";
			this.lbl_Slt1_YN.Size = new System.Drawing.Size(74, 30);
			this.lbl_Slt1_YN.TabIndex = 217;
			this.lbl_Slt1_YN.Text = "SLIT 1";
			this.lbl_Slt1_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_Slt1_YN.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_StatusSlit2
			// 
			this.lbl_StatusSlit2.BackColor = System.Drawing.Color.MediumSeaGreen;
			this.lbl_StatusSlit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_StatusSlit2.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_StatusSlit2.Location = new System.Drawing.Point(78, 30);
			this.lbl_StatusSlit2.Name = "lbl_StatusSlit2";
			this.lbl_StatusSlit2.Size = new System.Drawing.Size(280, 30);
			this.lbl_StatusSlit2.TabIndex = 216;
			this.lbl_StatusSlit2.Text = "Positioned setup 387455 - 3";
			this.lbl_StatusSlit2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_StatusSlit2.Click += new System.EventHandler(this.label_Click);
			// 
			// lbl_Slt2_YN
			// 
			this.lbl_Slt2_YN.BackColor = System.Drawing.Color.Black;
			this.lbl_Slt2_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Slt2_YN.ForeColor = System.Drawing.Color.WhiteSmoke ;
			this.lbl_Slt2_YN.Location = new System.Drawing.Point(4, 30);
			this.lbl_Slt2_YN.Name = "lbl_Slt2_YN";
			this.lbl_Slt2_YN.Size = new System.Drawing.Size(74, 30);
			this.lbl_Slt2_YN.TabIndex = 215;
			this.lbl_Slt2_YN.Text = "SLIT 2";
			this.lbl_Slt2_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_Slt2_YN.Click += new System.EventHandler(this.label_Click);
			// 
			// SlitterScorerShaftStatus
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.ttl_ShaftStatus);
			this.Controls.Add(this.lbl_GapScoreAux);
			this.Controls.Add(this.lbl_GapScore1);
			this.Controls.Add(this.lbl_GapScore2);
			this.Controls.Add(this.lbl_StatusScoreAux);
			this.Controls.Add(this.lbl_ScrA_YN);
			this.Controls.Add(this.lbl_StatusScore1);
			this.Controls.Add(this.lbl_Scr1_YN);
			this.Controls.Add(this.lbl_StatusScore2);
			this.Controls.Add(this.lbl_Scr2_YN);
			this.Controls.Add(this.lbl_StatusSlit1);
			this.Controls.Add(this.lbl_Slt1_YN);
			this.Controls.Add(this.lbl_StatusSlit2);
			this.Controls.Add(this.lbl_Slt2_YN);
			this.Name = "SlitterScorerShaftStatus";
			this.Size = new System.Drawing.Size(358, 229);
			this.Click += new System.EventHandler(this.label_Click);
			this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
			this.ResumeLayout(false);

    }
    #endregion

    private void OnSizeChanged(object sender, EventArgs e)
    {
      this.ttl_ShaftStatus.Width = this.Width;
      this.ttl_ShaftStatus.Height = this.Height;
      int shaftWidth = Approx((this.Width - LEFT_MARGIN) * SHAFT_WIDTH_PERC);
      int labelWidth = this.Width - shaftWidth - LEFT_MARGIN;
      this.lbl_Slt2_YN.Width = this.lbl_Slt1_YN.Width = 
      this.lbl_Scr2_YN.Width = this.lbl_Scr1_YN.Width = 
      this.lbl_ScrA_YN.Width = labelWidth;
      int shaftLeft = LEFT_MARGIN + labelWidth;
      this.lbl_StatusSlit2.Left = this.lbl_StatusSlit1.Left = 
      this.lbl_StatusScore2.Left = this.lbl_StatusScore1.Left = 
      this.lbl_StatusScoreAux.Left = shaftLeft;
      this.lbl_StatusSlit2.Width = this.lbl_StatusSlit1.Width = 
      this.lbl_StatusScore2.Width = this.lbl_StatusScore1.Width = 
      this.lbl_StatusScoreAux.Width = shaftWidth;
      int gapWidth = Approx(shaftWidth * GAP_WIDTH_PERC_ON_SHAFT);
      this.lbl_GapScore2.Left = this.lbl_GapScore1.Left = 
      this.lbl_GapScoreAux.Left = this.Width - gapWidth;
      this.lbl_GapScore2.Width = this.lbl_GapScore1.Width =
      this.lbl_GapScoreAux.Width = gapWidth;

      int shaftHeight = Approx((this.Height-TOP_MARGIN)*SHAFT_HEIGHT_PERC);
      int spaceBetweenShafts = Approx((this.Height - TOP_MARGIN) * SPACE_BTWN_SHAFTS_PERC);
      int spaceBetweenSlitsAndScores = Approx((this.Height - TOP_MARGIN) * SPACE_BTWN_SLITS_AND_SCORES_PERC);
      this.lbl_Slt2_YN.Height = this.lbl_Slt1_YN.Height =
      this.lbl_Scr2_YN.Height = this.lbl_Scr1_YN.Height = this.lbl_ScrA_YN.Height =
      this.lbl_StatusSlit2.Height = this.lbl_StatusSlit1.Height =
      this.lbl_StatusScore2.Height = this.lbl_StatusScore1.Height =
      this.lbl_StatusScoreAux.Height =
      this.lbl_GapScore2.Height = this.lbl_GapScore1.Height =
      this.lbl_GapScoreAux.Height = shaftHeight;

      int y = this.Height - shaftHeight;
      this.lbl_ScrA_YN.Top = this.lbl_StatusScoreAux.Top = this.lbl_GapScoreAux.Top = y;
      y -= spaceBetweenShafts + shaftHeight;
      this.lbl_Scr1_YN.Top = this.lbl_StatusScore1.Top = this.lbl_GapScore1.Top = y;
      y -= spaceBetweenShafts + shaftHeight;
      this.lbl_Scr2_YN.Top = this.lbl_StatusScore2.Top = this.lbl_GapScore2.Top = y;
      y -= spaceBetweenSlitsAndScores + shaftHeight;
      this.lbl_Slt1_YN.Top = this.lbl_StatusSlit1.Top = y;
      y -= spaceBetweenShafts + shaftHeight;
      this.lbl_Slt2_YN.Top = this.lbl_StatusSlit2.Top = y;
    }

    private int Approx(double val)
    {
      return (int)Math.Round(val);
    }

    public void Config(bool auxPresent, bool brokenSlits1, bool brokenSlits2, 
                       bool slt1Enab, bool slt2Enab, bool scr1Enab, bool scr2Enab, bool scrAEnab)
    {
      this.auxPresent = auxPresent;
      this.lbl_ScrA_YN.BackColor = (auxPresent ? Color.Black : Color.LightGray);
      this.lbl_StatusScoreAux.BackColor =
      this.lbl_GapScoreAux.BackColor = (auxPresent ? this.shaftColor[(int)ShaftStatus.NotPositioned] : COLOR_NOT_PRESENT_SHAFT);
      if (!auxPresent)
        this.lbl_StatusScoreAux.Text = this.lbl_GapScoreAux.Text = string.Empty;
      this.lbl_Slt1_YN.ForeColor = (brokenSlits1 ? Color.Red : Color.WhiteSmoke );
      this.lbl_Slt2_YN.ForeColor = (brokenSlits2 ? Color.Red : Color.WhiteSmoke );
      this.shaftEnabled[(int)Shaft.Slit1] = slt1Enab;
      this.shaftEnabled[(int)Shaft.Slit2] = slt2Enab;
      this.shaftEnabled[(int)Shaft.Score1] = scr1Enab;
      this.shaftEnabled[(int)Shaft.Score2] = scr2Enab;
      this.shaftEnabled[(int)Shaft.ScoreAux] = scrAEnab;
      this.ShowEnabling(true);
      this.Refresh();
    }

    private void ShowEnabling(bool conf)
    {
      bool anyDisabled = false;
      if (!shaftEnabled[(int)Shaft.Slit1])
      {
        this.lbl_StatusSlit1.BackColor = this.shaftColor[(int)ShaftStatus.Disabled];
        this.lbl_StatusSlit1.Text = this.GetShaftStatusText(ShaftStatus.Disabled, 0, false);
        anyDisabled = true;
      }
      if (!shaftEnabled[(int)Shaft.Slit2])
      {
        this.lbl_StatusSlit2.BackColor = this.shaftColor[(int)ShaftStatus.Disabled];
        this.lbl_StatusSlit2.Text = this.GetShaftStatusText(ShaftStatus.Disabled, 0, false);
        anyDisabled = true;
      }
      if (!shaftEnabled[(int)Shaft.Score1])
      {
        this.lbl_StatusScore1.BackColor = this.lbl_GapScore1.BackColor = this.shaftColor[(int)ShaftStatus.Disabled];
        this.lbl_StatusScore1.Text = this.GetShaftStatusText(ShaftStatus.Disabled, 0, false);
        anyDisabled = true;
      }
      if (!shaftEnabled[(int)Shaft.Score2])
      {
        this.lbl_StatusScore2.BackColor = this.lbl_GapScore2.BackColor = this.shaftColor[(int)ShaftStatus.Disabled];
        this.lbl_StatusScore2.Text = this.GetShaftStatusText(ShaftStatus.Disabled, 0, false);
        anyDisabled = true;
      }
      if (this.auxPresent)
      {
        if (!shaftEnabled[(int)Shaft.ScoreAux])
        {
          this.lbl_StatusScoreAux.BackColor = this.lbl_GapScoreAux.BackColor = this.shaftColor[(int)ShaftStatus.Disabled];
          this.lbl_StatusScoreAux.Text = this.GetShaftStatusText(ShaftStatus.Disabled, 0, false);
          anyDisabled = true;
        }
      }
      if ((!anyDisabled) && conf)
        this.WriteShaftStatus();
    }

    //AGrassi_FI06/12/2007_1
    private string GetShaftStatusText(ShaftStatus st, int setup, bool teflonShow)
    {
      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      if ((st == ShaftStatus.NotPositioned) || (st == ShaftStatus.Disabled))
        return this.shaftMessage[(int)st];
      int ind = 0;
      string code = string.Empty;
      if ((setup == 0) && (this.machId[setup] == SlitterScorerHeader.MAINTENANCE_SETUP_ID))
      {
        ind = 0;
        code = msg.getTranslatedMessage("§SYCC_49") + "_" + this.machMod[setup].ToString();
      }
      //AGrassi_FI06/12/2007_1
      else if ((setup == 1) && teflonShow && (this.machId[setup] == SlitterScorerHeader.TEFLON_SETUP_ID))
      {
        ind = 0;
        code = msg.getTranslatedMessage("§SYCC_180");
      }
      else
      {
        if (setup >= 0)
        {
          for (ind = setup; ind < setupRows.Count; ind++)
            if ((setupRows[ind][Order.DEL_SETUP_ID].ToString().Equals(this.machId[setup].ToString())))
              break;
        }
        if ((st <= ShaftStatus.NotPositioned) || (ind >= setupRows.Count) ||
            (setupRows.Count <= ind) || (setup < 0))
          return this.shaftMessage[(int)st];
        string part1 = string.Empty;
        if (st == ShaftStatus.PositionedPart1)
          part1 = " " + msg.getTranslatedMessage("§SYCC_56") + " 1 ";
        string setupInd = (ind + 1).ToString();
        if (ind == 0) setupInd = "§SYCC_62";
        else if (ind == 1) setupInd = "§SYCC_63";
        code = setupRows[ind][Order.DEL_SETUP_CODE].ToString() + " [" + msg.getTranslatedMessage(setupInd) + "] " + part1;
      }
      return code;
    }

    public void SetGap(int gap1, int gap2, int gapA, int actGap1, int actGap2, int actGapA)
    {
      this.lbl_GapScore1.Text = gap1.ToString();
      if (gap1 != actGap1) this.lbl_GapScore1.Text += " (" + (actGap1 - gap1).ToString() + ")";
      this.lbl_GapScore2.Text = gap2.ToString();
      if (gap2 != actGap2) this.lbl_GapScore2.Text += " (" + (actGap2 - gap2).ToString() + ")";
      if (this.auxPresent)
      {
        this.lbl_GapScoreAux.Text = gapA.ToString();
        if (gapA != actGapA) this.lbl_GapScoreAux.Text += " (" + (actGapA - gapA).ToString() + ")";
      }
      this.Refresh();
    }

    public void SetStatus(int stSlt1, int stSlt2, int stScr1, int stScr2, int stScrA,
                          int stpSlt1, int stpSlt2, int stpScr1, int stpScr2, int stpScrA,
                          int[] machId, int [] machMod)
    {
      this.stShaft[(int)Shaft.Slit1] = (ShaftStatus)stSlt1;
      this.stShaft[(int)Shaft.Slit2] = (ShaftStatus)stSlt2;
      this.stShaft[(int)Shaft.Score1] = (ShaftStatus)stScr1;
      this.stShaft[(int)Shaft.Score2] = (ShaftStatus)stScr2;
      this.stShaft[(int)Shaft.ScoreAux] = (ShaftStatus)stScrA;
      this.stpShaft[(int)Shaft.Slit1] = stpSlt1;
      this.stpShaft[(int)Shaft.Slit2] = stpSlt2;
      this.stpShaft[(int)Shaft.Score1] = stpScr1;
      this.stpShaft[(int)Shaft.Score2] = stpScr2;
      this.stpShaft[(int)Shaft.ScoreAux] = stpScrA;
      this.machId = machId;
      this.machMod = machMod;
      this.WriteShaftStatus();
      this.ShowEnabling(false);
      this.Refresh();
    }

    private void WriteShaftStatus()
    {
      this.lbl_StatusSlit1.BackColor = this.shaftColor[(int)this.stShaft[(int)Shaft.Slit1]];
      this.lbl_StatusSlit2.BackColor = this.shaftColor[(int)this.stShaft[(int)Shaft.Slit2]];
      this.lbl_StatusScore1.BackColor = this.lbl_GapScore1.BackColor = this.shaftColor[(int)this.stShaft[(int)Shaft.Score1]];
      this.lbl_StatusScore2.BackColor = this.lbl_GapScore2.BackColor = this.shaftColor[(int)this.stShaft[(int)Shaft.Score2]];
      if (this.auxPresent)
        this.lbl_StatusScoreAux.BackColor = this.lbl_GapScoreAux.BackColor = this.shaftColor[(int)this.stShaft[(int)Shaft.ScoreAux]];
      //AGrassi_FI06/12/2007_1
      this.lbl_StatusSlit1.Text = this.GetShaftStatusText(this.stShaft[(int)Shaft.Slit1], this.stpShaft[(int)Shaft.Slit1], true);
      this.lbl_StatusSlit2.Text = this.GetShaftStatusText(this.stShaft[(int)Shaft.Slit2], this.stpShaft[(int)Shaft.Slit2], true);
      this.lbl_StatusScore1.Text = this.GetShaftStatusText(this.stShaft[(int)Shaft.Score1], this.stpShaft[(int)Shaft.Score1], false);
      this.lbl_StatusScore2.Text = this.GetShaftStatusText(this.stShaft[(int)Shaft.Score2], this.stpShaft[(int)Shaft.Score2], false);
      if (this.auxPresent)
        this.lbl_StatusScoreAux.Text = this.GetShaftStatusText(this.stShaft[(int)Shaft.ScoreAux], this.stpShaft[(int)Shaft.ScoreAux], false);
      Shaft sh;
      for (sh = Shaft.ScoreAux; sh >= Shaft.Slit1; sh--)
        if (this.stShaft[(int)sh] == ShaftStatus.PositioningNow) break;
      if (sh > Shaft.NoShaft)
        this.timPositioning.Start();
    }
    
    private void PositioningBlink(object sender, EventArgs e)
    {
      int setupPositioning = -1;
      Color presentColor = Color.WhiteSmoke ;
      for (Shaft sh = Shaft.Slit1; sh <= Shaft.ScoreAux; sh++)
      {
        if (this.stShaft[(int)sh] == ShaftStatus.PositioningNow)
        {
          string shName = Enum.Format(typeof(Shaft), sh, "g");
          Label labShaft = ((Label)this.GetType().GetField("lbl_Status" + shName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
          presentColor = (labShaft.BackColor == this.shaftColor[(int)ShaftStatus.PositioningNow] ? Color.Gold : this.shaftColor[(int)ShaftStatus.PositioningNow]);
          labShaft.BackColor = presentColor;
          if (sh >= Shaft.Score1)
          {
            Label labGap = ((Label)this.GetType().GetField("lbl_Gap" + shName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
            labGap.BackColor = presentColor;
          }
          setupPositioning = this.stpShaft[(int)sh];
        }
      }
      if (setupPositioning >= 0)
      {
        if (this.externalFunction != null)
          this.externalFunction(setupPositioning);
      }
      else
        this.timPositioning.Stop();
    }

    public void SetDescriptions(string disabled, string notPositioned, string positioning, 
                                string positioned, string positionedPart1, string plunged)
    {
      this.shaftMessage[(int)ShaftStatus.NotPositioned] = notPositioned; 
      this.shaftMessage[(int)ShaftStatus.PositioningNow] = positioning;
      this.shaftMessage[(int)ShaftStatus.PositionedPart1] = positionedPart1;
      this.shaftMessage[(int)ShaftStatus.Positioned] = positioned;
      this.shaftMessage[(int)ShaftStatus.Plunged] = plunged;
      this.shaftMessage[(int)ShaftStatus.Disabled] = disabled;
      this.Refresh();
    }

    public void SetExternalTimerFunction(PositioningTimer externalFunction)
    {
      this.externalFunction = externalFunction;
    }

    public event EventHandler ClickAny;

    private void label_Click(object sender, System.EventArgs e)
    {
      if (this.ClickAny != null)
        this.ClickAny(this, e);
    }

    public Color GetSetupColor(int setupInd)
    {
      Shaft sh;
      for (sh = Shaft.Slit1; sh <= Shaft.ScoreAux; sh++)
        if (this.stpShaft[(int)sh] == setupInd) break;
      if (sh <= Shaft.ScoreAux)
        return this.shaftColor[(int)this.stShaft[(int)sh]];
      else
        return Color.Transparent;
    }
  }
}

