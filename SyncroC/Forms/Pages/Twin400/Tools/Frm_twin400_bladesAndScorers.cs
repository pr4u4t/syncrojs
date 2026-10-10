using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;
using SyncroC.Controls.Colors;
using SyncroC.Controls.Tools;


namespace SyncroC.Forms.Pages.Twin400.Tools
{
	public class Frm_twin400_bladesAndScorers : SyncroC.Forms.Pages.Twin400.Tools.Frm_twin400_toolsSubgroup
  {
    private System.ComponentModel.IContainer components = null;
		private SetupSituation[] setupSituation = new SetupSituation[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
    private bool wrongData = false;
    private Label lbl_WrongData;
    private Label lbl_NarrowToWide;
    private Label lbl_WideToNarrow;
    private Label lbl_CleanSlits;

    public Frm_twin400_bladesAndScorers()
		{
      InitializeComponent();
      //agrassi_FI12-06-08_2
      this.toolDetail.Location = new System.Drawing.Point(114, 225);
      this.toolDetail.Size = new System.Drawing.Size(1106, 506);
      this.toolDetail.Config(DBManager.getDBManager().lineConfig, MachineToolsType.Twin, false, false, null, null, "", "");
      DBManager.getDBManager().order.OnDBOrderChange += new Order.DBChangedHandler(this.SetupListChange);
			for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS; i++)
        this.setupSituation[i] = new SetupSituation();
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.lbl_CtrlF1_YY.Text = "";
      }
     }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			DBManager.getDBManager().order.OnDBOrderChange -= new Order.DBChangedHandler(this.SetupListChange);
			base.Dispose( disposing );
		}

		protected override void F10()
		{
		}

    protected override bool ShowTools(int pos)
    {
      if (!base.ShowTools(pos))
        return false;
      this.visibleSetupCount = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count;
      bool valid = bool.Parse(setup[Order.DEL_VALID].ToString());
      if (valid)
      {
        if (pos < DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS)
          this.lbl_CtrlF1_YY.Text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_73");
        else
          this.lbl_CtrlF1_YY.Text = "";
        int shaftMask;
        //agrassi_FI04-02-09
        //int[] plungedSlitMask, dummySlitMask, sepSlitMask;
        int[] plungedSlitMask, sepSlitMask;
        int[][] slitPositions, trimSlitNarrowPositions;
        //agrassi_FI04-02-09
        //bool narrowToWide = false, wideToNarrow = false, cleanSlits = false;
        bool cleanSlits = false;
        int setupSitPos = pos;
				if (pos < SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS)
        {
          if (this.setupSituation[setupSitPos].setupId != this.intParse(this.setup[Order.DEL_SETUP_ID].ToString()))
          {
            if ((bool.Parse(setup[Order.DEL_KNIFE_ONLY].ToString())))
            {
              if (setupSitPos > 0)
                setupSitPos--;
            }
            else
            {
              while ((this.setupSituation[setupSitPos].setupId != this.intParse(this.setup[Order.DEL_SETUP_ID].ToString()))
                      && (setupSitPos > 0))
                setupSitPos--;
            }
          }
        }
				bool allSlits = (pos < SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS) && (this.setupSituation[setupSitPos].filled) &&
                        (this.setupSituation[setupSitPos].setupId == this.intParse(this.setup[Order.DEL_SETUP_ID].ToString()));
        if (allSlits)
        {
          shaftMask = this.setupSituation[setupSitPos].shaftMask;
					trimSlitNarrowPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
					for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
            trimSlitNarrowPositions[i] = new int[2];
					for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
          {
            //agrassi_FI04-02-09
            //trimSlitNarrowPositions[sh][0] = int.Parse(setup[Order.DEL_SLIT_POSITION + sh.ToString() + ":0"].ToString());
            //trimSlitNarrowPositions[sh][1] = int.Parse(setup[Order.DEL_SLIT_POSITION + sh.ToString() + ":" + (DBManager.getDBManager().lineConfig.SlitterScorerConfig.GetSlitCount()-1).ToString()].ToString());
            trimSlitNarrowPositions[sh][0] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SLIT_POSITION, sh, 0));
            trimSlitNarrowPositions[sh][1] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SLIT_POSITION, sh, DBManager.getDBManager().lineConfig.SlitterScorerConfig.GetSlitCount() - 1));
          }
          slitPositions = this.setupSituation[setupSitPos].slitPositions;
          plungedSlitMask = this.setupSituation[setupSitPos].plungedSlitMask;
          sepSlitMask = this.setupSituation[setupSitPos].sepSlitMask;
          //agrassi_FI04-02-09
          //dummySlitMask = this.setupSituation[setupSitPos].dummySlitMask;
          //narrowToWide = this.setupSituation[setupSitPos].narrowToWide;
          //wideToNarrow = this.setupSituation[setupSitPos].wideToNarrow;
          cleanSlits = this.setupSituation[setupSitPos].cleanSlits;
          this.lbl_WrongData.Visible = (this.wrongData && this.toolDetail.Debug);
          //this.lbl_NarrowToWide.Visible = (narrowToWide && this.toolDetail.Debug);
          //this.lbl_WideToNarrow.Visible = (wideToNarrow && this.toolDetail.Debug);
          this.lbl_CleanSlits.Visible = (cleanSlits && this.toolDetail.Debug);
        }
        else
        {
          this.lbl_WrongData.Visible = false;
          this.lbl_NarrowToWide.Visible = false;
          this.lbl_WideToNarrow.Visible = false;
          this.lbl_CleanSlits.Visible = false;
          trimSlitNarrowPositions = null;
          //agrassi_FI04-02-09
          //wideToNarrow = false;
          shaftMask = int.Parse(setup[Order.DEL_SHAFT_MASK].ToString());
					plungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
          //agrassi_FI04-02-09
          //dummySlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
          sepSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
					slitPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
					for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
						slitPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
					for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
          {
            //agrassi_FI04-02-09
            //plungedSlitMask[sh] = int.Parse(setup[Order.DEL_PLUNGED_SLIT_MASK + sh.ToString()].ToString());
            plungedSlitMask[sh] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_PLUNGED_SLIT_MASK, sh));
            //dummySlitMask[sh] = int.Parse(setup[Order.DEL_DUMMY_SLIT_MASK + sh.ToString()].ToString());
            //sepSlitMask[sh] = int.Parse(setup[Order.DEL_SEP_SLIT_MASK + sh.ToString()].ToString());
            sepSlitMask[sh] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SEP_SLIT_MASK, sh));
            for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
              //slitPositions[sh][slt] = int.Parse(setup[Order.DEL_SLIT_POSITION + sh.ToString() + ":" + slt.ToString()].ToString());
              slitPositions[sh][slt] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SLIT_POSITION, sh, slt));
          }
        }
        int[] tearTapePositions = new int[2*SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
				int[] startScorer = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
				int[] scoreCount = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
				bool[] support = new bool[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS];
				int[][] scorePositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];
        //agrassi_FI04-02-09
        //int[][] upRelPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];
        //int[][] leftScorePositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];
        //int[][] scoreScoringGap = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];
        //int[][] scoreShaftGap = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];
        //int[][] scoreMeasureIndex = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];
        //PositType[][] scorePositType = new PositType[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS][];

        int tearTapeCount=this.intParse(setup[Order.DEL_TEAR_TAPE_COUNT].ToString());
        tearTapePositions = new int[2 * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
        for (int i = 0; i < tearTapeCount; i++)
          //tearTapePositions[i] = this.intParse(setup[Order.DEL_TEAR_TAPE_POSITION + i.ToString()].ToString());
          tearTapePositions[i] = this.intParse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_TEAR_TAPE_POSITION, i));

        for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; i++)
        {
					scorePositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
          //agrassi_FI04-02-09
          //upRelPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
          //leftScorePositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
          //scoreScoringGap[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
          //scoreShaftGap[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
          //scoreMeasureIndex[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
          //scorePositType[i] = new PositType[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT];
        }
				for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
        {
          //startScorer[sh] = this.intParse(setup[Order.DEL_START_SCORER + sh.ToString()].ToString());
          startScorer[sh] = this.intParse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_START_SCORER, sh));
          //scoreCount[sh] = this.intParse(setup[Order.DEL_SCORE_COUNT + sh.ToString()].ToString());
          scoreCount[sh] = this.intParse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SCORE_COUNT, sh));
          //support[sh] = bool.Parse(setup[Order.DEL_SUPPORT_SCORE + sh.ToString()].ToString());
          support[sh] = bool.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SUPPORT_SCORE, sh));
          for (int scr = 0; scr < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          {
            //scorePositions[sh][scr] = this.intParse(setup[Order.DEL_SCORE_POSITION + sh.ToString() + ":" + scr.ToString()].ToString());
            scorePositions[sh][scr] = this.intParse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SCORE_POSITION, sh, scr));
            //agrassi_FI04-02-09
            //upRelPositions[sh][scr] = this.intParse(setup[Order.DEL_UPREL_SCORE_POSITION + sh.ToString() + ":" + scr.ToString()].ToString());
            //leftScorePositions[sh][scr] = this.intParse(setup[Order.DEL_LEFT_SCORE_POSITION + sh.ToString() + ":" + scr.ToString()].ToString());
            //scoreScoringGap[sh][scr] = this.intParse(setup[Order.DEL_SCORE_SCORING_GAP + sh.ToString() + ":" + scr.ToString()].ToString());
            //scoreShaftGap[sh][scr] = this.intParse(setup[Order.DEL_SHAFT_GAP + sh.ToString() + ":" + scr.ToString()].ToString());
            //scoreMeasureIndex[sh][scr] = this.intParse(setup[Order.DEL_SCORE_MEASURE_INDEX + sh.ToString() + ":" + scr.ToString()].ToString());
            //scorePositType[sh][scr] = (PositType)Enum.Parse(typeof(PositType), setup[Order.DEL_SCORE_POSIT_TYPE+sh.ToString()+":"+scr.ToString()].ToString());
          }
        }

        //agrassi_FI04-02-09
        //this.toolDetail.PositionTwin(this.intParse(setup[Order.DEL_WEB_WIDTH].ToString()), shaftMask, slitPositions, 
        //                          plungedSlitMask, dummySlitMask, sepSlitMask, scorePositions, upRelPositions, 
        //                          leftScorePositions, scoreScoringGap, scoreShaftGap, startScorer, scoreCount,
        //                          support, valid, scorePositType, scoreMeasureIndex, wideToNarrow, trimSlitNarrowPositions, 
        //                          allSlits, tearTapePositions, tearTapeCount);
        this.toolDetail.PositionTwin(this.intParse(setup[Order.DEL_WEB_WIDTH].ToString()), shaftMask, slitPositions,
                                  plungedSlitMask, sepSlitMask, scorePositions, startScorer, scoreCount, support, valid, 
                                  allSlits, tearTapePositions, tearTapeCount);
      }
      else
        //agrassi_FI04-02-09
        //this.toolDetail.PositionTwin(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()), 0, null, null, null, null,
        //                           null, null, null, null, null, null, null, null, valid, null, null, false, null, 
        //                           false, null, 0);
        this.toolDetail.PositionTwin(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()), 0, null, null, null,
                                   null, null, null, null, valid, false, null, 0);
      return true;
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_bladesAndScorers));
			this.lbl_WrongData = new System.Windows.Forms.Label();
			this.lbl_NarrowToWide = new System.Windows.Forms.Label();
			this.lbl_WideToNarrow = new System.Windows.Forms.Label();
			this.lbl_CleanSlits = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// toolDetail
			// 
      //agrassi_FI12-06-08_2
      //this.toolDetail.Location = new System.Drawing.Point(114, 225);
      //this.toolDetail.Size = new System.Drawing.Size(1106, 506);
			// 
			// tab_TabA
			// 
			this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			// 
			// tab_TabB
			// 
			this.tab_TabB.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
			// 
			// lbl_CtrlF4_YY
			// 
			this.lbl_CtrlF4_YY.Text = "Previous";
			// 
			// lbl_CtrlF5_YY
			// 
			this.lbl_CtrlF5_YY.Text = "Next";
			// 
			// iml_navigationImages
			// 
			this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
			this.iml_navigationImages.Images.SetKeyName(0, "");
			this.iml_navigationImages.Images.SetKeyName(1, "");
			this.iml_navigationImages.Images.SetKeyName(2, "");
			this.iml_navigationImages.Images.SetKeyName(3, "");
			// 
			// btn_Group1
			// 
			this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
			this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
			// 
			// btn_Group2
			// 
			this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
			this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
			// 
			// btn_Group3
			// 
			this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
			this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
			// 
			// btn_Group5
			// 
			this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
			this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
			// 
			// iml_AccessibilityImages
			// 
			this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
			this.iml_AccessibilityImages.Images.SetKeyName(0, "");
			this.iml_AccessibilityImages.Images.SetKeyName(1, "");
			// 
			// lbl_WrongData
			// 
			this.lbl_WrongData.AutoSize = true;
			this.lbl_WrongData.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_WrongData.ForeColor = System.Drawing.Color.Red;
			this.lbl_WrongData.Location = new System.Drawing.Point(234, 61);
			this.lbl_WrongData.Name = "lbl_WrongData";
			this.lbl_WrongData.Size = new System.Drawing.Size(81, 18);
			this.lbl_WrongData.TabIndex = 275;
			this.lbl_WrongData.Text = "Wrong data";
			this.lbl_WrongData.Visible = false;
			// 
			// lbl_NarrowToWide
			// 
			this.lbl_NarrowToWide.AutoSize = true;
			this.lbl_NarrowToWide.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_NarrowToWide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.lbl_NarrowToWide.Location = new System.Drawing.Point(360, 61);
			this.lbl_NarrowToWide.Name = "lbl_NarrowToWide";
			this.lbl_NarrowToWide.Size = new System.Drawing.Size(141, 18);
			this.lbl_NarrowToWide.TabIndex = 276;
			this.lbl_NarrowToWide.Text = "From narrow to wide";
			this.lbl_NarrowToWide.Visible = false;
			// 
			// lbl_WideToNarrow
			// 
			this.lbl_WideToNarrow.AutoSize = true;
			this.lbl_WideToNarrow.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_WideToNarrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.lbl_WideToNarrow.Location = new System.Drawing.Point(551, 61);
			this.lbl_WideToNarrow.Name = "lbl_WideToNarrow";
			this.lbl_WideToNarrow.Size = new System.Drawing.Size(145, 18);
			this.lbl_WideToNarrow.TabIndex = 277;
			this.lbl_WideToNarrow.Text = "From wide to narrow ";
			this.lbl_WideToNarrow.Visible = false;
			// 
			// lbl_CleanSlits
			// 
			this.lbl_CleanSlits.AutoSize = true;
			this.lbl_CleanSlits.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_CleanSlits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.lbl_CleanSlits.Location = new System.Drawing.Point(738, 61);
			this.lbl_CleanSlits.Name = "lbl_CleanSlits";
			this.lbl_CleanSlits.Size = new System.Drawing.Size(72, 18);
			this.lbl_CleanSlits.TabIndex = 278;
			this.lbl_CleanSlits.Text = "Clean slits";
			this.lbl_CleanSlits.Visible = false;
			// 
			// Frm_twin400_bladesAndScorers
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Controls.Add(this.lbl_CleanSlits);
			this.Controls.Add(this.lbl_WideToNarrow);
			this.Controls.Add(this.lbl_NarrowToWide);
			this.Controls.Add(this.lbl_WrongData);
			this.Name = "Frm_twin400_bladesAndScorers";
			this.Tag = "";
			this.Text = "Frm_twin400_bladesAndScorers";
			this.SizeChanged += new System.EventHandler(this.Frm_twin400_bladesAndScorers_SizeChanged);
			this.Activated += new System.EventHandler(this.Frm_twin400_bladesAndScorers_Activated);
			this.Load += new System.EventHandler(this.Frm_twin400_bladesAndScorers_Load);
			this.Controls.SetChildIndex(this.SetupId, 0);
			this.Controls.SetChildIndex(this.ModId, 0);
      //agrassi_FI12-06-08_2
      //this.Controls.SetChildIndex(this.toolDetail, 0);
			this.Controls.SetChildIndex(this.btn_Group5, 0);
			this.Controls.SetChildIndex(this.lbl_Date, 0);
			this.Controls.SetChildIndex(this.pct_Home_NY, 0);
			this.Controls.SetChildIndex(this.pct_Back_NY, 0);
			this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
			this.Controls.SetChildIndex(this.lbl_Time, 0);
			this.Controls.SetChildIndex(this.lbl_Status, 0);
			this.Controls.SetChildIndex(this.tab_Tab1, 0);
			this.Controls.SetChildIndex(this.tab_Tab2, 0);
			this.Controls.SetChildIndex(this.tab_Tab3, 0);
			this.Controls.SetChildIndex(this.tab_Tab4, 0);
			this.Controls.SetChildIndex(this.tab_Tab5, 0);
			this.Controls.SetChildIndex(this.tab_Tab6, 0);
			this.Controls.SetChildIndex(this.tab_Tab7, 0);
			this.Controls.SetChildIndex(this.tab_Tab8, 0);
			this.Controls.SetChildIndex(this.tab_TabA, 0);
			this.Controls.SetChildIndex(this.tab_TabD, 0);
			this.Controls.SetChildIndex(this.tab_TabC, 0);
			this.Controls.SetChildIndex(this.tab_TabB, 0);
			this.Controls.SetChildIndex(this.btn_Group1, 0);
			this.Controls.SetChildIndex(this.btn_Group2, 0);
			this.Controls.SetChildIndex(this.btn_Group3, 0);
			this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
			this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
			this.Controls.SetChildIndex(this.lbl_WrongData, 0);
			this.Controls.SetChildIndex(this.lbl_NarrowToWide, 0);
			this.Controls.SetChildIndex(this.lbl_WideToNarrow, 0);
			this.Controls.SetChildIndex(this.lbl_CleanSlits, 0);
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

    }
		#endregion

    private void _UpdateSetupSlits(int stpInd, string data)
    {
			if ((stpInd < 0) || (stpInd >= SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS)) return;
      this.wrongData = !this.setupSituation[stpInd].Update(data);
      if (this.toolsPos == stpInd)
        this.ShowTools(this.toolsPos);
    }

    private void Frm_twin400_bladesAndScorers_Activated(object sender, EventArgs e)
    {
      this.Frm_twin400_tools_Activated(sender, e);
    }

    private void Frm_twin400_bladesAndScorers_Load(object sender, EventArgs e)
    {
      this.Frm_twin400_tools_Load(sender, e);
    }

    private void Frm_twin400_bladesAndScorers_SizeChanged(object sender, EventArgs e)
    {
      this.Frm_twin400_tools_SizeChanged(sender, e);
    }

    //CM8 29-09-2005
    protected override void CtrlF1()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
        return;
      if (this.toolsPos >= DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_twin400_bladesAndScorers)) + ".SendSetup" + this.toolsPos + "()");

    }
  }
}

