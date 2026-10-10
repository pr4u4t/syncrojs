using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.Controls.ControlEventArgs;
using SyncroC.DataBase.LineConfig;
using SyncroC.Forms.Pages.Twin400.Orders;

namespace SyncroC.Controls
{
	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.

	public class SlitterScorerSetupStatus : SyncroC.Controls.SyncroUserControl
  {
    public delegate Color SetupColorFunction(int setupInd);
 		public delegate void SlitterScorerStatusHandler(object sender, SlitterScorerStatusEventArgs args);
    public new event SlitterScorerStatusHandler Click;
    private const int LEFT_MARGIN = 4;
    private const int TOP_MARGIN = 26;
    private const double SETUP_POS_PERC = 0.03;
    private const double SETUP_CODE_PERC = 0.22;
    private const double SETUP_HEADER_PERC = 0.25;
    private const double SETUP_DATA_PERC = 0.50;
    private const double SETUP_HEIGHT_PERC = 0.29;
    private const double SPACE_BTWN_SETUPS_PERC = 0.064;

    private Label lbl_SetupHeader2;
    private Label lbl_SetupHeader1;
    private Label lbl_SetupHeader0;
    private Title ttl_MachineSetups;
    private Label lbl_Setup0;
    private Label lbl_Setup1;
    private Label lbl_Setup2;
    private Label lbl_SetupData1;
    private Label lbl_SetupData2;
    private Label lbl_SetupData0;
    private Label lbl_SetupCode2;
    private Label lbl_SetupCode1;
    private Label lbl_SetupCode0;
		private System.ComponentModel.IContainer components = null;
    private int[] machId;
    private int[] machMod;
    private MachineStatus machStatus;
    private SetupColorFunction getSetupColor;
    private Color[] setupColor;
    private bool[] setupAligned;
    private Timer timPositioningFailed;
    private Timer timWaitForManualSendingCurrent;
    private Timer timWaitForManualSendingNext;
    private int shaftPositioningFailed;

		public SlitterScorerSetupStatus()
		{
			InitializeComponent();
      this.machId = new int[SlitterScorerHeader.SS_SETUPS];
      this.machMod = new int[SlitterScorerHeader.SS_SETUPS];
      this.setupAligned = new bool[SlitterScorerHeader.SS_SETUPS];
      this.setupColor = new Color[Enum.GetValues(typeof(SetupStatus)).Length];
      this.setupColor[(int)SetupStatus.Tranferred] = Color.Gray;
      this.setupColor[(int)SetupStatus.PositioningNow] = Color.MediumSeaGreen;
      this.setupColor[(int)SetupStatus.NotAligned] = Color.Red;
      this.setupColor[(int)SetupStatus.NotValid] = Color.Violet;
      this.timWaitForManualSendingCurrent.Interval = 400;
      this.timWaitForManualSendingNext.Interval = 400;
      this.timPositioningFailed.Interval = 400;
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
      this.components = new System.ComponentModel.Container();
      this.lbl_SetupHeader2 = new System.Windows.Forms.Label();
      this.lbl_SetupHeader1 = new System.Windows.Forms.Label();
      this.lbl_SetupHeader0 = new System.Windows.Forms.Label();
      this.ttl_MachineSetups = new SyncroC.Controls.Title();
      this.lbl_Setup0 = new System.Windows.Forms.Label();
      this.lbl_Setup1 = new System.Windows.Forms.Label();
      this.lbl_Setup2 = new System.Windows.Forms.Label();
      this.lbl_SetupData1 = new System.Windows.Forms.Label();
      this.lbl_SetupData2 = new System.Windows.Forms.Label();
      this.lbl_SetupData0 = new System.Windows.Forms.Label();
      this.lbl_SetupCode2 = new System.Windows.Forms.Label();
      this.lbl_SetupCode1 = new System.Windows.Forms.Label();
      this.lbl_SetupCode0 = new System.Windows.Forms.Label();
      this.timPositioningFailed = new System.Windows.Forms.Timer(this.components);
      this.timWaitForManualSendingCurrent = new System.Windows.Forms.Timer(this.components);
      this.timWaitForManualSendingNext = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // lbl_SetupHeader2
      // 
      this.lbl_SetupHeader2.BackColor = System.Drawing.Color.LightCyan;
      this.lbl_SetupHeader2.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupHeader2.ForeColor = System.Drawing.Color.Black;
      this.lbl_SetupHeader2.Location = new System.Drawing.Point(143, 126);
      this.lbl_SetupHeader2.Name = "lbl_SetupHeader2";
      this.lbl_SetupHeader2.Size = new System.Drawing.Size(142, 41);
      this.lbl_SetupHeader2.TabIndex = 231;
      this.lbl_SetupHeader2.Text = "Gap 30, Trim 105 WebWidth 2380, Flute E";
      this.lbl_SetupHeader2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupHeader2.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupHeader1
      // 
      this.lbl_SetupHeader1.BackColor = System.Drawing.Color.LightCyan;
      this.lbl_SetupHeader1.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupHeader1.ForeColor = System.Drawing.Color.Black;
      this.lbl_SetupHeader1.Location = new System.Drawing.Point(143, 76);
      this.lbl_SetupHeader1.Name = "lbl_SetupHeader1";
      this.lbl_SetupHeader1.Size = new System.Drawing.Size(142, 41);
      this.lbl_SetupHeader1.TabIndex = 230;
      this.lbl_SetupHeader1.Text = "Gap 26, Trim 90 WebWidth 2400, Flute A";
      this.lbl_SetupHeader1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupHeader1.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupHeader0
      // 
      this.lbl_SetupHeader0.BackColor = System.Drawing.Color.LightCyan;
      this.lbl_SetupHeader0.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupHeader0.ForeColor = System.Drawing.Color.Black;
      this.lbl_SetupHeader0.Location = new System.Drawing.Point(143, 26);
      this.lbl_SetupHeader0.Name = "lbl_SetupHeader0";
      this.lbl_SetupHeader0.Size = new System.Drawing.Size(142, 41);
      this.lbl_SetupHeader0.TabIndex = 229;
      this.lbl_SetupHeader0.Text = "Gap 55, Trim 50 WebWidth 2500, Flute B";
      this.lbl_SetupHeader0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupHeader0.Click += new System.EventHandler(this.Setup_Click);
      // 
      // ttl_MachineSetups
      // 
      this.ttl_MachineSetups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_MachineSetups.color = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.ttl_MachineSetups.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_MachineSetups.ForeColor = System.Drawing.Color.Black;
      this.ttl_MachineSetups.Location = new System.Drawing.Point(0, 0);
      this.ttl_MachineSetups.Name = "ttl_MachineSetups";
      this.ttl_MachineSetups.Size = new System.Drawing.Size(563, 169);
      this.ttl_MachineSetups.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_MachineSetups.TabIndex = 228;
      this.ttl_MachineSetups.Text = "MACHINE SETUPS";
      this.ttl_MachineSetups.TranslateText = true;
      // 
      // lbl_Setup0
      // 
      this.lbl_Setup0.BackColor = System.Drawing.Color.Black;
      this.lbl_Setup0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Setup0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Setup0.Location = new System.Drawing.Point(4, 26);
      this.lbl_Setup0.Name = "lbl_Setup0";
      this.lbl_Setup0.Size = new System.Drawing.Size(17, 41);
      this.lbl_Setup0.TabIndex = 219;
      this.lbl_Setup0.Text = "1";
      this.lbl_Setup0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_Setup0.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_Setup1
      // 
      this.lbl_Setup1.BackColor = System.Drawing.Color.Black;
      this.lbl_Setup1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Setup1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Setup1.Location = new System.Drawing.Point(4, 76);
      this.lbl_Setup1.Name = "lbl_Setup1";
      this.lbl_Setup1.Size = new System.Drawing.Size(17, 41);
      this.lbl_Setup1.TabIndex = 221;
      this.lbl_Setup1.Text = "2";
      this.lbl_Setup1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_Setup1.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_Setup2
      // 
      this.lbl_Setup2.BackColor = System.Drawing.Color.Black;
      this.lbl_Setup2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Setup2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Setup2.Location = new System.Drawing.Point(4, 126);
      this.lbl_Setup2.Name = "lbl_Setup2";
      this.lbl_Setup2.Size = new System.Drawing.Size(17, 41);
      this.lbl_Setup2.TabIndex = 223;
      this.lbl_Setup2.Text = "3";
      this.lbl_Setup2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_Setup2.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupData1
      // 
      this.lbl_SetupData1.AutoEllipsis = true;
      this.lbl_SetupData1.BackColor = System.Drawing.Color.Ivory;
      this.lbl_SetupData1.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupData1.ForeColor = System.Drawing.Color.Black;
      this.lbl_SetupData1.Location = new System.Drawing.Point(285, 76);
      this.lbl_SetupData1.Name = "lbl_SetupData1";
      this.lbl_SetupData1.Size = new System.Drawing.Size(278, 41);
      this.lbl_SetupData1.TabIndex = 227;
      this.lbl_SetupData1.Text = "1 x 2100";
      this.lbl_SetupData1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupData1.UseCompatibleTextRendering = true;
      this.lbl_SetupData1.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupData2
      // 
      this.lbl_SetupData2.AutoEllipsis = true;
      this.lbl_SetupData2.BackColor = System.Drawing.Color.Honeydew;
      this.lbl_SetupData2.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupData2.ForeColor = System.Drawing.Color.Black;
      this.lbl_SetupData2.Location = new System.Drawing.Point(285, 126);
      this.lbl_SetupData2.Name = "lbl_SetupData2";
      this.lbl_SetupData2.Size = new System.Drawing.Size(278, 41);
      this.lbl_SetupData2.TabIndex = 226;
      this.lbl_SetupData2.Text = "2 x 240 100 240 - B MaleFemale\r\n1 x 150 150 700 150 150 - A PointToPoint";
      this.lbl_SetupData2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupData2.UseCompatibleTextRendering = true;
      this.lbl_SetupData2.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupData0
      // 
      this.lbl_SetupData0.AutoEllipsis = true;
      this.lbl_SetupData0.BackColor = System.Drawing.Color.Honeydew;
      this.lbl_SetupData0.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupData0.ForeColor = System.Drawing.Color.Black;
      this.lbl_SetupData0.Location = new System.Drawing.Point(285, 26);
      this.lbl_SetupData0.Name = "lbl_SetupData0";
      this.lbl_SetupData0.Size = new System.Drawing.Size(278, 41);
      this.lbl_SetupData0.TabIndex = 225;
      this.lbl_SetupData0.Text = "3 x 120 280 120 100 50 400 200  - A NormalOffset\r\n1 x 560 ";
      this.lbl_SetupData0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupData0.UseCompatibleTextRendering = true;
      this.lbl_SetupData0.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupCode2
      // 
      this.lbl_SetupCode2.BackColor = System.Drawing.Color.LightGray;
      this.lbl_SetupCode2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupCode2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupCode2.Location = new System.Drawing.Point(21, 126);
      this.lbl_SetupCode2.Name = "lbl_SetupCode2";
      this.lbl_SetupCode2.Size = new System.Drawing.Size(122, 41);
      this.lbl_SetupCode2.TabIndex = 224;
      this.lbl_SetupCode2.Text = "3876028441";
      this.lbl_SetupCode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupCode2.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupCode1
      // 
      this.lbl_SetupCode1.BackColor = System.Drawing.Color.MediumSeaGreen;
      this.lbl_SetupCode1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupCode1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupCode1.Location = new System.Drawing.Point(21, 76);
      this.lbl_SetupCode1.Name = "lbl_SetupCode1";
      this.lbl_SetupCode1.Size = new System.Drawing.Size(122, 41);
      this.lbl_SetupCode1.TabIndex = 222;
      this.lbl_SetupCode1.Text = "3874505 - 3";
      this.lbl_SetupCode1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupCode1.Click += new System.EventHandler(this.Setup_Click);
      // 
      // lbl_SetupCode0
      // 
      this.lbl_SetupCode0.BackColor = System.Drawing.Color.SteelBlue;
      this.lbl_SetupCode0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupCode0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupCode0.Location = new System.Drawing.Point(21, 26);
      this.lbl_SetupCode0.Name = "lbl_SetupCode0";
      this.lbl_SetupCode0.Size = new System.Drawing.Size(122, 41);
      this.lbl_SetupCode0.TabIndex = 220;
      this.lbl_SetupCode0.Text = "3874551 - 2";
      this.lbl_SetupCode0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SetupCode0.Click += new System.EventHandler(this.Setup_Click);
      // 
      // timPositioningFailed
      // 
      this.timPositioningFailed.Tick += new System.EventHandler(this.timPositioningFailed_Tick);
      // 
      // timWaitForManualSendingCurrent
      // 
      this.timWaitForManualSendingCurrent.Tick += new System.EventHandler(this.timWaitForManualSendingCurr_Tick);
      // 
      // timWaitForManualSendingNext
      // 
      this.timWaitForManualSendingNext.Tick += new System.EventHandler(this.timWaitForManualSendingNext_Tick);
      // 
      // SlitterScorerSetupStatus
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Controls.Add(this.lbl_SetupHeader2);
      this.Controls.Add(this.lbl_SetupHeader1);
      this.Controls.Add(this.lbl_SetupHeader0);
      this.Controls.Add(this.ttl_MachineSetups);
      this.Controls.Add(this.lbl_Setup0);
      this.Controls.Add(this.lbl_Setup1);
      this.Controls.Add(this.lbl_Setup2);
      this.Controls.Add(this.lbl_SetupData1);
      this.Controls.Add(this.lbl_SetupData2);
      this.Controls.Add(this.lbl_SetupData0);
      this.Controls.Add(this.lbl_SetupCode2);
      this.Controls.Add(this.lbl_SetupCode1);
      this.Controls.Add(this.lbl_SetupCode0);
      this.Name = "SlitterScorerSetupStatus";
      this.Size = new System.Drawing.Size(563, 167);
      this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
      this.ResumeLayout(false);

		}
		#endregion

    private void OnSizeChanged(object sender, EventArgs e)
    {
      this.ttl_MachineSetups.Width = this.Width;
      this.ttl_MachineSetups.Height = this.Height;
      int posWidth = Approx((this.Width - LEFT_MARGIN) * SETUP_POS_PERC);
      this.lbl_Setup0.Width = this.lbl_Setup1.Width =
      this.lbl_Setup2.Width = posWidth;
      int posLeft = LEFT_MARGIN;
      this.lbl_Setup0.Left = this.lbl_Setup1.Left =
      this.lbl_Setup2.Left = posLeft;
      int codeWidth = Approx((this.Width - LEFT_MARGIN) * SETUP_CODE_PERC);
      this.lbl_SetupCode0.Width = this.lbl_SetupCode1.Width =
      this.lbl_SetupCode2.Width = codeWidth;
      int codeLeft = this.lbl_Setup0.Width + this.lbl_Setup0.Left;
      this.lbl_SetupCode0.Left = this.lbl_SetupCode1.Left =
      this.lbl_SetupCode2.Left = codeLeft;
      int headerWidth = Approx((this.Width - LEFT_MARGIN) * SETUP_HEADER_PERC);
      this.lbl_SetupHeader0.Width = this.lbl_SetupHeader1.Width =
      this.lbl_SetupHeader2.Width = headerWidth;
      int headerLeft = this.lbl_SetupCode0.Width + this.lbl_SetupCode0.Left;
      this.lbl_SetupHeader0.Left = this.lbl_SetupHeader1.Left =
      this.lbl_SetupHeader2.Left = headerLeft;
      int dataWidth = Approx((this.Width - LEFT_MARGIN) * SETUP_DATA_PERC);
      this.lbl_SetupData0.Width = this.lbl_SetupData1.Width =
      this.lbl_SetupData2.Width = dataWidth;
      int dataLeft = this.lbl_SetupHeader0.Width + this.lbl_SetupHeader0.Left;
      this.lbl_SetupData0.Left = this.lbl_SetupData1.Left =
      this.lbl_SetupData2.Left = dataLeft;

      int setupHeight = Approx((this.Height - TOP_MARGIN) * SETUP_HEIGHT_PERC);
      int spaceBetweenSetups = Approx((this.Height - TOP_MARGIN) * SPACE_BTWN_SETUPS_PERC);
      this.lbl_Setup0.Height = this.lbl_SetupCode0.Height =
      this.lbl_SetupHeader0.Height = this.lbl_SetupData0.Height =
      this.lbl_Setup1.Height = this.lbl_SetupCode1.Height =
      this.lbl_SetupHeader1.Height = this.lbl_SetupData1.Height =
      this.lbl_Setup2.Height = this.lbl_SetupCode2.Height =
      this.lbl_SetupHeader2.Height = this.lbl_SetupData2.Height = setupHeight;
      int y = TOP_MARGIN;
      this.lbl_Setup0.Top = this.lbl_SetupCode0.Top = this.lbl_SetupHeader0.Top = this.lbl_SetupData0.Top = y;
      y += spaceBetweenSetups + setupHeight;
      this.lbl_Setup1.Top = this.lbl_SetupCode1.Top = this.lbl_SetupHeader1.Top = this.lbl_SetupData1.Top = y;
      y += spaceBetweenSetups + setupHeight;
      this.lbl_Setup2.Top = this.lbl_SetupCode2.Top = this.lbl_SetupHeader2.Top = this.lbl_SetupData2.Top = y;
    }

    private int Approx(double val)
    {
      return (int)Math.Round(val);
    }

    public void Config(SetupColorFunction getSetupColor)
    {
      this.getSetupColor = getSetupColor;
    }

    public void SetStatus(int[] id, int[] mod, MachineStatus machStatus)
    {
      this.machId = id;
      this.machMod = mod;
      this.machStatus = machStatus;
      this.WriteSetupStatus();
      this.Refresh();
    }

    private void WriteSetupStatus()
    {
      Language lang = DBManager.getDBManager().language;
      DataBase.Message msg = DBManager.getDBManager().message;
      DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      int setupCount = setupRows.Count;
      int cntPlcSetup = 0;
      //per mariestad
      Color prevSetupColor=Color.Transparent;
      for (int i = 0; i < SlitterScorerHeader.SS_SETUPS; i++)
      {
        bool pres = (setupCount > i);
        Label labCode = (Label)this.GetType().GetField("lbl_SetupCode" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        Label labHeader = (Label)this.GetType().GetField("lbl_SetupHeader" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        Label labInfo = (Label)this.GetType().GetField("lbl_SetupData" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        if (pres)
        {
          labCode.Text = setupRows[i][Order.DEL_SETUP_CODE].ToString();
          string fluteID = "";
          DataRow qualityRow = DBManager.getDBManager().quality.getRow(setupRows[i][Order.DEL_QUALITY_ID].ToString());
          if (qualityRow != null)
            fluteID = qualityRow[Quality.FLUTE_ID].ToString();
          bool[] notScoredOrder =new bool[3];
          //AGrassi_FI08/11/2007_1
          notScoredOrder[0] = setupRows[i][Order.DEL_SCORE_MEASURES_0].ToString().Trim().Equals(setupRows[i][Order.DEL_SHEET_WIDTH_0].ToString().Trim());
          bool notScoredSetup = notScoredOrder[0];
          if (int.Parse(setupRows[i][Order.DEL_OUTS_1].ToString()) > 0)
          {
            notScoredOrder[1] = setupRows[i][Order.DEL_SCORE_MEASURES_1].ToString().Trim().Equals(setupRows[i][Order.DEL_SHEET_WIDTH_1].ToString().Trim());
            notScoredSetup = notScoredSetup && notScoredOrder[1];
            if (int.Parse(setupRows[i][Order.DEL_OUTS_2].ToString()) > 0)
            {
              notScoredOrder[2] = setupRows[i][Order.DEL_SCORE_MEASURES_2].ToString().Trim().Equals(setupRows[i][Order.DEL_SHEET_WIDTH_2].ToString().Trim());
                notScoredSetup = notScoredSetup && notScoredOrder[2];
            }
          }
          ShaftGroup grp = SlitterScorer.GetShaftGroup(int.Parse(setupRows[i][Order.DEL_SHAFT_MASK].ToString()));
          Color cBackGroup = Color.Red, cForeGroup=Color.Black;
          string grpS = "";
          if ((grp == ShaftGroup.Group1) || (grp == ShaftGroup.Group1A))
          {
            cBackGroup = Color.Honeydew;
            grpS = "1";
          }
          else if ((grp == ShaftGroup.Group2) || (grp == ShaftGroup.Group2A))
          {
            cBackGroup = Color.Ivory;
            grpS = "2";
          }
          else if (grp == ShaftGroup.BothGroups)
          {
            cBackGroup = Color.Aquamarine;
            grpS = "T";
          }
          if ((grp == ShaftGroup.Group1A) || (grp == ShaftGroup.Group2A))
            cForeGroup = Color.Blue;
          labHeader.Text =grpS + " - " + (notScoredSetup ? "            " : (msg.getTranslatedMessage("§SYCC_57") + " " + setupRows[i][Order.DEL_SCORE_GAP].ToString() + ", ")) +
                    msg.getTranslatedMessage("§SYCC_58") + " " +
                    SyncroConverter.Convert(int.Parse(setupRows[i][Order.DEL_TRIM].ToString()), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + "\n" +
                    msg.getTranslatedMessage("§SYCC_59") + " " +
                    SyncroConverter.Convert(int.Parse(setupRows[i][Order.DEL_WEB_WIDTH].ToString()), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                    ",  " + msg.getTranslatedMessage("§SYCC_64") + " " + fluteID;
          labInfo.BackColor = cBackGroup;
          labInfo.ForeColor = cForeGroup;
          labInfo.Text = setupRows[i][Order.DEL_OUTS_0].ToString() + " x " +
                    SyncroConverter.ConvertScoreMeasures(setupRows[i][Order.DEL_SCORE_MEASURES_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
          if (!notScoredOrder[0])
              labInfo.Text += " (" + SyncroConverter.Convert(int.Parse(setupRows[i][Order.DEL_SHEET_WIDTH_0].ToString()), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + ") "+
                    " - " + setupRows[i][Order.DEL_SCORE_TYPE_0].ToString() + " " +
                    lang.getTranslation(typeof(PositType).Name + "." + setupRows[i][Order.DEL_POSIT_TYPE_0].ToString());
          if (int.Parse(setupRows[i][Order.DEL_OUTS_1].ToString()) > 0)
          {
            labInfo.Text += "\n" + setupRows[i][Order.DEL_OUTS_1].ToString() + " x " +
                    SyncroConverter.ConvertScoreMeasures(setupRows[i][Order.DEL_SCORE_MEASURES_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
            if (!notScoredOrder[1])
              labInfo.Text += " (" + SyncroConverter.Convert(int.Parse(setupRows[i][Order.DEL_SHEET_WIDTH_1].ToString()), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + ") "+
                  " - " + setupRows[i][Order.DEL_SCORE_TYPE_1].ToString() + " " +
                  lang.getTranslation(typeof(PositType).Name + "." + setupRows[i][Order.DEL_POSIT_TYPE_1].ToString());
            if (int.Parse(setupRows[i][Order.DEL_OUTS_2].ToString()) > 0)
              labInfo.Text += " [...]";
          }
          bool knifeOnwards = (setupRows[i][Order.DEL_KNIFE_ONLY].Equals(bool.TrueString) && (i > 0));
          if (setupRows[i][Order.DEL_VALID].ToString().Equals(bool.TrueString))
          {
            if ((knifeOnwards) && (this.machStatus != MachineStatus.Maintenance))
            {
              labCode.BackColor = prevSetupColor;
            }
            else if ((int.Parse(setupRows[i][Order.DEL_SETUP_ID].ToString()) == this.machId[cntPlcSetup]) &&
                (int.Parse(setupRows[i][Order.DEL_SETUP_MODID].ToString()) == this.machMod[cntPlcSetup])) 
            {
              this.setupAligned[i] = true;
              Color setupColor = Color.Yellow;
              if (this.getSetupColor!=null)
                setupColor=this.getSetupColor(cntPlcSetup);
              if (setupColor != Color.Transparent)
                labCode.BackColor = setupColor;
              else
                labCode.BackColor = this.setupColor[(int)SetupStatus.Tranferred];
            }
            else
            {
              this.setupAligned[i] = false;
              if ((i == 0) && (this.machStatus == MachineStatus.Maintenance) && (this.machId[i] == SlitterScorerHeader.MAINTENANCE_SETUP_ID))
              {
                labCode.BackColor = Color.Turquoise;
                labCode.Text = this.machId[i].ToString() + "-" + this.machMod[i].ToString("00");
                labInfo.Text = msg.getTranslatedMessage("§SYCC_49") + " " + this.machMod[i].ToString();
                labHeader.Text = labInfo.Text;
              }
              //AGrassi_FI06/12/2007_1
              else if ((i == 1) && (this.machStatus == MachineStatus.TeflonCycle) && (this.machId[i] == SlitterScorerHeader.TEFLON_SETUP_ID))
              {
                labCode.BackColor = Color.Gold;
                labCode.Text = this.machId[i].ToString() + "-" + this.machMod[i].ToString("00");
                labInfo.Text = msg.getTranslatedMessage("§SYCC_180");
                labHeader.Text = labInfo.Text;
              }
              else
                labCode.BackColor = this.setupColor[(int)SetupStatus.NotAligned];
            }
          }
          else
            labCode.BackColor = this.setupColor[(int)SetupStatus.NotValid];
          //if (!setupRows[i][Order.DEL_KNIFE_ONLY].Equals(bool.TrueString) || (i == 0)) cntPlcSetup++;
          if (((i+1<setupCount) && (!setupRows[i+1][Order.DEL_KNIFE_ONLY].Equals(bool.TrueString))) || (i+1==setupCount))
            cntPlcSetup++;
        }
        else
        {
          labCode.Text = string.Empty;
          labCode.BackColor = Color.LightGray;
          labInfo.Text = string.Empty;
          labHeader.Text = string.Empty;
        }
        prevSetupColor = labCode.BackColor;
      }
    }

    public void PositioningBlink(int positioningSetup)
    {
      DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      if (positioningSetup >= setupRows.Count)
        return;
      int actualPositioningSetup=positioningSetup;
      if (positioningSetup == 1)
      {
        if (setupRows[1][Order.DEL_KNIFE_ONLY].Equals(bool.TrueString))
        {
          if (setupRows.Count >= 3)
          {
            if (setupRows[2][Order.DEL_KNIFE_ONLY].Equals(bool.FalseString))
              actualPositioningSetup = 2;
            else
              return;
          }
          else
            return;
        }
      }
      //per mariestad
      int lastKnifeOnlySetup;
      int lastSetup=Math.Min(SlitterScorerHeader.SS_SETUPS, setupRows.Count);
      for (lastKnifeOnlySetup = actualPositioningSetup; lastKnifeOnlySetup < lastSetup-1; lastKnifeOnlySetup++)
      {
        if (!setupRows[lastKnifeOnlySetup+1][Order.DEL_KNIFE_ONLY].Equals(bool.TrueString))
          break;
      }
      Color presentColor = Color.WhiteSmoke ;
      if (this.setupAligned[actualPositioningSetup])
      {
        //per mariestad
        for (int i = actualPositioningSetup; i <= lastKnifeOnlySetup; i++)
        {
          Label labSetup = ((Label)this.GetType().GetField("lbl_SetupCode" + i, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
          presentColor = (labSetup.BackColor == this.setupColor[(int)SetupStatus.PositioningNow] ? Color.Gold : this.setupColor[(int)SetupStatus.PositioningNow]);
          labSetup.BackColor = presentColor;
        }
      }
    }

    private void Setup_Click(object sender, EventArgs e)
    {
      int pos = int.Parse(((Control)sender).Name.Substring(((Control)sender).Name.Length - 1));
      if (this.Click != null)
        this.Click(this, new SlitterScorerStatusEventArgs(pos));
    }

    private void BlinkSetupLabel(int pos)
    {
      if ((pos < 0) || (pos > 1))
        return;
      Color cNormal = this.setupColor[(int)SetupStatus.NotAligned];
      Color cBlink = Color.Orange;
      DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      //per mariestad
      int lastKnifeOnlySetup;
      int lastSetup = Math.Min(SlitterScorerHeader.SS_SETUPS, setupRows.Count);
      for (lastKnifeOnlySetup = pos; lastKnifeOnlySetup < lastSetup - 1; lastKnifeOnlySetup++)
      {
        if (!setupRows[lastKnifeOnlySetup + 1][Order.DEL_KNIFE_ONLY].Equals(bool.TrueString))
          break;
      }
      for (int i = pos; i <= lastKnifeOnlySetup; i++)
      {
        Label labCode = (Label)this.GetType().GetField("lbl_SetupCode" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        if (labCode.BackColor.Equals(cBlink))
          labCode.BackColor = cNormal;
        else
          labCode.BackColor = cBlink;
      }
    }

    public void SetWaitForManualCommandCurr(bool flag)
    {
      this.timWaitForManualSendingCurrent.Enabled = flag;
    }

    public void SetWaitForManualCommandNext(bool flag)
    {
      this.timWaitForManualSendingNext.Enabled = flag;
    }

    public void SetPositioningFailed(bool flag)
    {
      if (this.machId[0] == 0)
        this.shaftPositioningFailed = 0;
      else
        this.shaftPositioningFailed = 1;
      this.timPositioningFailed.Enabled = flag;
    }

    private void timWaitForManualSendingCurr_Tick(object sender, EventArgs e)
    {
      this.BlinkSetupLabel(0);
    }

    private void timWaitForManualSendingNext_Tick(object sender, EventArgs e)
    {
      this.BlinkSetupLabel(1);
    }

    private void timPositioningFailed_Tick(object sender, EventArgs e)
    {
      this.BlinkSetupLabel(this.shaftPositioningFailed);
    }

    public void SetOrderSendingEnabled(bool val)
    {
      this.ttl_MachineSetups.ForeColor=(val?Color.Black:Color.Red);
    }
  }
}

