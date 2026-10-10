using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.Network;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Tools;

namespace SyncroC.Forms.Pages.SlitterScorer.Tools
{
	public class Frm_SlitterScorerWebDirector : SyncroC.Forms.Pages.SlitterScorer.Tools.Frm_SlitterScorerToolsSubgroup 
  {
		private int[] mask2level = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
		private int[] mask3level = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
		private bool[] maskValid = new bool[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
    private int paddleIndex = 0;
    private MachineTools.ChangeShaftFunction changeFunction;
    private System.ComponentModel.IContainer components = null;

		public Frm_SlitterScorerWebDirector()
		{
			InitializeComponent();
      //agrassi_FI12-06-08_2
      this.toolDetail.Location = new System.Drawing.Point(114, 225);
      this.toolDetail.Margin = new System.Windows.Forms.Padding(3, 2, 1, 3);
      this.toolDetail.Size = new System.Drawing.Size(1106, 506);
      this.toolDetail.TabIndex = 281;
      this.toolDetail.TranslateText = true;
      this.changeFunction = new MachineTools.ChangeShaftFunction(this.ChangePaddle);
      this.toolDetail.Config(DBManager.getDBManager().lineConfig, MachineToolsType.WebDir, true,
                              false, this.ChangePaddle, null, "", "");
      DBManager.getDBManager().order.OnDBOrderChange += new Order.DBChangedHandler(this.SetupListChange);
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
      }
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
			DBManager.getDBManager().order.OnDBOrderChange -= new Order.DBChangedHandler(this.SetupListChange);
			base.Dispose( disposing );
		}

		protected override void F12()
		{
		}


    protected bool ShowTools(int pos,int axUsed)
    {
      if (!base.ShowTools(pos))
        return false;
      bool valid = bool.Parse(setup[Order.DEL_VALID].ToString());
      if (valid)
      {
        //agrassi_FI04-02-09
        //int[] plungedSlitMask, sepSlitMask, dummySlitMask;
        int[] plungedSlitMask, sepSlitMask;
        int[][] slitPositions;
				plungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
        //agrassi_FI04-02-09
        //dummySlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
				sepSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
				slitPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
				for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
					slitPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];

        //agrassi_FI04-02-09
        //plungedSlitMask[axUsed] = int.Parse(setup[Order.DEL_PLUNGED_SLIT_MASK + axUsed.ToString()].ToString());
        //agrassi_FI25-05-2011_1
        //plungedSlitMask[axUsed] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_PLUNGED_SLIT_MASK, axUsed));
        plungedSlitMask[axUsed] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_PLUNGED_SLIT_MASK, axUsed))/2;
        //dummySlitMask[axUsed] = int.Parse(setup[Order.DEL_DUMMY_SLIT_MASK + axUsed.ToString()].ToString());
        //sepSlitMask[axUsed] = int.Parse(setup[Order.DEL_SEP_SLIT_MASK + axUsed.ToString()].ToString());
        //agrassi_FI25-05-2011_1
        //sepSlitMask[axUsed] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SEP_SLIT_MASK, axUsed));
        sepSlitMask[axUsed] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SEP_SLIT_MASK, axUsed))/2;
        for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
          //slitPositions[axUsed][slt] = int.Parse(setup[Order.DEL_SLIT_POSITION + axUsed.ToString() + ":" + slt.ToString()].ToString());
          slitPositions[axUsed][slt] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SLIT_POSITION, axUsed, slt));

        //agrassi_FI04-02-09
        //this.toolDetail.PositionWebDir(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()),
        //                          this.mask2level[pos], this.mask3level[pos], this.maskValid[pos],
        //                          int.Parse(setup[Order.DEL_SHAFT_MASK].ToString()), slitPositions,
        //                          plungedSlitMask, dummySlitMask,sepSlitMask , valid);
        //agrassi_FI25-05-2011_1
        //this.toolDetail.PositionWebDir(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()),
        //                          this.mask2level[pos], this.mask3level[pos], this.maskValid[pos],
        //                          int.Parse(setup[Order.DEL_SHAFT_MASK].ToString()), slitPositions,
        //                          plungedSlitMask, sepSlitMask, valid);
        this.toolDetail.PositionWebDir(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()),
                                  this.mask2level[pos], this.mask3level[pos], this.maskValid[pos],
                                  SyncroC.DataBase.LineConfig.SlitterScorer.BIT(axUsed), slitPositions,
                                  plungedSlitMask, sepSlitMask, valid);
      }
      else
        //agrassi_FI04-02-09
        //this.toolDetail.PositionWebDir(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()), 0, 0, false, 0,
        //                                null, null, null, null, false);
        this.toolDetail.PositionWebDir(int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()), 0, 0, false, 0,
                                        null, null, null, false);
      return true;
    }


		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerWebDirector));
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// toolDetail
			// 
      //agrassi_FI12-06-08_2
      //this.toolDetail.Location = new System.Drawing.Point(114, 225);
      //this.toolDetail.Margin = new System.Windows.Forms.Padding(3, 2, 1, 3);
      //this.toolDetail.Size = new System.Drawing.Size(1106, 506);
      //this.toolDetail.TabIndex = 281;
      //this.toolDetail.TranslateText = true;
			// 
			// lbl_Title_YN
			// 
			this.lbl_Title_YN.Text = "WEB DIRECTOR";
			// 
			// tab_TabC
			// 
			this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			// 
			// lbl_CtrlF4_YY
			// 
			this.lbl_CtrlF4_YY.Text = "";
			// 
			// lbl_CtrlF1_YY
			// 
			this.lbl_CtrlF1_YY.Text = "Toggle";
			// 
			// lbl_CtrlF2_YY
			// 
			this.lbl_CtrlF2_YY.Text = "All up";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Text = "All down";
      // 
			// lbl_CtrlF5_YY
			// 
			this.lbl_CtrlF5_YY.Text = "";
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
			// Frm_SlitterScorerWebDirector
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Name = "Frm_SlitterScorerWebDirector";
			this.Tag = "";
			this.Text = "Frm_SlitterScorerWebDirector";
			this.SizeChanged += new System.EventHandler(this.Frm_SlitterScorerWebDirector_SizeChanged);
			this.Activated += new System.EventHandler(this.Frm_SlitterScorerWebDirector_Activated);
			this.Load += new System.EventHandler(this.Frm_SlitterScorerWebDirector_Load);
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

    }
		#endregion

    protected override void LeftArrow()
    {
      int dummy=0;
      this.paddleIndex=this.toolDetail.ScrollLeft(ref dummy);
    }

    protected override void RightArrow()
    {
      int dummy=0;
      this.paddleIndex = this.toolDetail.ScrollRight(ref dummy);
    }

    private void ChangePaddle(int paddleIndex, int dummy)
    {
      this.paddleIndex = paddleIndex;
    }

    private void _UpdateWebDirMask(int stpInd, int mask2level, int mask3level, bool valid, int axUsed)
    {
			if ((stpInd < 0) || (stpInd >= SyncroC.DataBase.LineConfig.SlitterScorerHeader.OLD_SS_SETUPS)) return;
      this.mask2level[stpInd] = mask2level;
      this.mask3level[stpInd] = mask3level;
      this.maskValid[stpInd] = valid;
			this.axUsed = axUsed;
      if (this.toolsPos == stpInd)
        this.ShowTools(this.toolsPos, this.axUsed);
    }

		private void Frm_SlitterScorerWebDirector_Activated(object sender, EventArgs e)
    {
			this.Frm_SlitterScorerToolsSubgroup_Activated(sender, e);
    }

		private void Frm_SlitterScorerWebDirector_Load(object sender, EventArgs e)
    {
			this.Frm_SlitterScorerToolsSubgroup_Load(sender, e);
    }

		private void Frm_SlitterScorerWebDirector_SizeChanged(object sender, EventArgs e)
    {
      this.Frm_SlitterScorerToolsSubgroup_SizeChanged(sender, e);
    }

    protected override void CtrlF1()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if (this.toolsPos != 0)
      {
				PageManager.getPageManager()._SetMessage("§SYCC_17", true);
        return;
      }
      if (!this.maskValid[0])
      {
				PageManager.getPageManager()._SetMessage("§SYCC_18", true);
        return;
      }
      int levelCount = DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount;
      int newMask2level = this.mask2level[0], newMask3level = this.mask3level[0];
      if (levelCount == 2)
      {
        //agrassi_FI12-06-08_1
        //if ((this.mask2level[0] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex)) != 0)
				newMask2level ^= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex);
      }
      else if (levelCount == 3)
      {
        if ((this.mask3level[0] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex)) == 0)
        {
          if ((this.mask2level[0] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex)) != 0)
          {
            newMask2level ^= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex);
          }
          //agrassi_FI12-06-08_1
          else
          {
            newMask2level ^= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex);
            newMask3level ^= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex);
          }
        }
        else
        {
          newMask3level ^= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.paddleIndex);
        }
      }
      if ((newMask2level != this.mask2level[0]) || (newMask3level != this.mask3level[0]))
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SlitterScorerWebDirector)) + "." + Command.MOD_WEB_DIR +
          "(" + newMask2level.ToString() + "," + newMask3level.ToString() + ")");
      }
      else
				PageManager.getPageManager()._SetMessage("§SYCC_19");
    }

    //agrassi_FI12-06-08_1
    protected override void CtrlF2() //All paddles UP
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if (this.toolsPos != 0)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_17", true);
        return;
      }
      if (!this.maskValid[0])
      {
        PageManager.getPageManager()._SetMessage("§SYCC_18", true);
        return;
      }
      int levelCount = DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount;
      //agrassi_FI12-06-08_1
      int paddleCount = DBManager.getDBManager().lineConfig.WebDirectorConfig.PaddlesNum;
      int allPaddlesUp = 0;
      for (int i=0; i<paddleCount; i++)
        allPaddlesUp |= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(i);
      int newMask2level = this.mask2level[0], newMask3level = this.mask3level[0];
      if (levelCount == 2)
      {
          newMask2level = allPaddlesUp;
      }
      else if (levelCount == 3)
      {
        newMask2level = allPaddlesUp;
        if ((this.mask3level[0] != 0) || (this.mask2level[0]==allPaddlesUp))
          newMask3level = allPaddlesUp;
      }
      if ((newMask2level != this.mask2level[0]) || (newMask3level != this.mask3level[0]))
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SlitterScorerWebDirector)) + "." + Command.MOD_WEB_DIR +
          "(" + newMask2level.ToString() + "," + newMask3level.ToString() + ")");
      }
      else
        PageManager.getPageManager()._SetMessage("§SYCC_19");
    }

    //agrassi_FI12-06-08_1
    protected override void CtrlF3()  //All paddles DOWN
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if (this.toolsPos != 0)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_17", true);
        return;
      }
      if (!this.maskValid[0])
      {
        PageManager.getPageManager()._SetMessage("§SYCC_18", true);
        return;
      }
      int levelCount = DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount;
      //agrassi_FI12-06-08_1
      int paddleCount = DBManager.getDBManager().lineConfig.WebDirectorConfig.PaddlesNum;
      int allPaddlesUp = 0;
      for (int i = 0; i < paddleCount; i++)
        allPaddlesUp |= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(i);
      int newMask2level = this.mask2level[0], newMask3level = this.mask3level[0];
      if (levelCount == 2)
      {
        newMask2level = 0;
      }
      else if (levelCount == 3)
      {
        newMask3level = 0;
        if ((this.mask2level[0] != allPaddlesUp) || (this.mask3level[0] == 0))
          newMask2level = 0;
      }
      if ((newMask2level != this.mask2level[0]) || (newMask3level != this.mask3level[0]))
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SlitterScorerWebDirector)) + "." + Command.MOD_WEB_DIR +
          "(" + newMask2level.ToString() + "," + newMask3level.ToString() + ")");
      }
      else
        PageManager.getPageManager()._SetMessage("§SYCC_19");
    }
  }
}

