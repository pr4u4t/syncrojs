using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.Forms.Pages.Debug.Twin400
{
	public class Frm_DebugTwin400SetupSituation : SyncroC.Forms.Pages.Debug.Twin400.Frm_DebugTwin400Subgroup
	{
    Label[][][] slitPos;
    //agrassi_FI19-01-2011_1
    //Label[][] plungedSlitMask, sepSlitMask, dummySlitMask;
    //Panel[] wideToNarrow, narrowToWide, cleanSlits, valid, sendToPlc, knifeOnwards; 
    Label[][] plungedSlitMask, sepSlitMask, dummySlitMask, posTimes;
    Panel[] cleanSlits, valid, sendToPlc, knifeOnwards;
    //agrassi_FI19-01-2011_1
    //Label[] webDir2LevelMask, webDir3LevelMask, rcsMask, shaftMask, setupId, modId, configVersion;
    Label[] webDir2LevelMask, webDir3LevelMask, rcsMask, shaftMask, setupId, modId, configVersion, actPosTime;
    private GroupBox grp_setup0;
    private GroupBox grp_shaft0_0;
    private Panel pnl_valid0;
    private Label lbl_validTit0;
    private Label lbl_sendToPlcTit0;
    private Panel pnl_sendToPlc0;
    private Label lbl_slitPosTit0_0;
    private Label lbl_slitPos0_0_0;
    private Label lbl_slitPos0_0_1;
    private Label lbl_slitPos0_0_2;
    private Label lbl_slitPos0_0_3;
    private Label lbl_slitPos0_0_4;
    private Label lbl_slitPos0_0_5;
    private Label lbl_slitPos0_0_6;
    private Label lbl_slitPos0_0_7;
    private Label lbl_slitPos0_0_8;
    private Label lbl_slitPos0_0_9;
    private Label lbl_plungedSlitMaskTit0_0;
    private Label lbl_plungedSlitMask0_0;
    private Label lbl_sepSlitMask0_0;
    private Label lbl_spSlitMaskTit0_0;
    private Label lbl_dummySlitMask0_0;
    private Label lbl_dummySlitMaskTit0_0;
    private GroupBox grp_shaft0_1;
    private Label lbl_dummySlitMask0_1;
    private Label lbl_dummySlitMaskTit0_1;
    private Label lbl_sepSlitMask0_1;
    private Label lbl_sepSlitMaskTit0_1;
    private Label lbl_plungedSlitMask0_1;
    private Label lbl_plungedSlitMaskTit0_1;
    private Label lbl_slitPos0_1_9;
    private Label lbl_slitPos0_1_8;
    private Label lbl_slitPos0_1_7;
    private Label lbl_slitPos0_1_6;
    private Label lbl_slitPos0_1_5;
    private Label lbl_slitPos0_1_4;
    private Label lbl_slitPos0_1_3;
    private Label lbl_slitPos0_1_2;
    private Label lbl_slitPos0_1_1;
    private Label lbl_slitPos0_1_0;
    private Label lbl_slitPosTit0_1;
    private Label lbl_webDir2LevelMask0;
    private Label lbl_webDir2LevelMaskTit0;
    private Label lbl_webDir3LevelMask0;
    private Label lbl_webDir3LevelMaskTit0;
    private Label lbl_rcsMask0;
    private Label lbl_rcsMaskTit0;
    private Label lbl_cleanSlitsTit0;
    private Panel pnl_cleanSlits0;
    private GroupBox grp_setup1;
    private Label lbl_cleanSlitsTit1;
    private Panel pnl_cleanSlits1;
    private Label lbl_rcsMask1;
    private Label lbl_rcsMaskTit1;
    private Label lbl_webDir3LevelMask1;
    private Label lbl_webDir3LevelMaskTit1;
    private Label lbl_webDir2LevelMask1;
    private Label lbl_webDir2LevelMaskTit1;
    private GroupBox grp_shaft1_1;
    private Label lbl_dummySlitMask1_1;
    private Label lbl_dummySlitMaskTit1_1;
    private Label lbl_sepSlitMask1_1;
    private Label lbl_sepSlitMaskTit1_1;
    private Label lbl_plungedSlitMask1_1;
    private Label lbl_plungedSlitMaskTit1_1;
    private Label lbl_slitPos1_1_9;
    private Label lbl_slitPos1_1_8;
    private Label lbl_slitPos1_1_7;
    private Label lbl_slitPos1_1_6;
    private Label lbl_slitPos1_1_5;
    private Label lbl_slitPos1_1_4;
    private Label lbl_slitPos1_1_3;
    private Label lbl_slitPos1_1_2;
    private Label lbl_slitPos1_1_1;
    private Label lbl_slitPos1_1_0;
    private Label lbl_slitPosTit1_1;
    private Label lbl_sendToPlcTit1;
    private Panel pnl_sendToPlc1;
    private Label lbl_validTit1;
    private Panel pnl_valid1;
    private GroupBox grp_shaft1_0;
    private Label lbl_dummySlitMask1_0;
    private Label lbl_dummySlitMaskTit1_0;
    private Label lbl_sepSlitMask1_0;
    private Label lbl_sepSlitMaskTit1_0;
    private Label lbl_plungedSlitMask1_0;
    private Label lbl_plungedSlitMaskTit1_0;
    private Label lbl_slitPos1_0_9;
    private Label lbl_slitPos1_0_8;
    private Label lbl_slitPos1_0_7;
    private Label lbl_slitPos1_0_6;
    private Label lbl_slitPos1_0_5;
    private Label lbl_slitPos1_0_4;
    private Label lbl_slitPos1_0_3;
    private Label lbl_slitPos1_0_2;
    private Label lbl_slitPos1_0_1;
    private Label lbl_slitPos1_0_0;
    private Label lbl_slitPosTit1_0;
    private GroupBox grp_setup2;
    private Label lbl_cleanSlitsTit2;
    private Panel pnl_cleanSlits2;
    private Label lbl_rcsMask2;
    private Label lbl_rcsMaskTit2;
    private Label lbl_webDir3LevelMask2;
    private Label lbl_webDir3LevelMaskTit2;
    private Label lbl_webDir2LevelMask2;
    private Label lbl_webDir2LevelMaskTit2;
    private GroupBox grp_shaft2_1;
    private Label lbl_dummySlitMask2_1;
    private Label lbl_dummySlitMaskTit2_1;
    private Label lbl_sepSlitMask2_1;
    private Label lbl_sepSlitMaskTit2_1;
    private Label lbl_plungedSlitMask2_1;
    private Label lbl_plungedSlitMaskTit2_1;
    private Label lbl_slitPos2_1_9;
    private Label lbl_slitPos2_1_8;
    private Label lbl_slitPos2_1_7;
    private Label lbl_slitPos2_1_6;
    private Label lbl_slitPos2_1_5;
    private Label lbl_slitPos2_1_4;
    private Label lbl_slitPos2_1_3;
    private Label lbl_slitPos2_1_2;
    private Label lbl_slitPos2_1_1;
    private Label lbl_slitPos2_1_0;
    private Label lbl_slitPosTit2_1;
    private Label lbl_sendToPlcTit2;
    private Panel pnl_sendToPlc2;
    private Label lbl_validTit2;
    private Panel pnl_valid2;
    private GroupBox grp_shaft2_0;
    private Label lbl_dummySlitMask2_0;
    private Label lbl_dummySlitMaskTit2_0;
    private Label lbl_sepSlitMask2_0;
    private Label lbl_sepSlitMaskTit2_0;
    private Label lbl_plungedSlitMask2_0;
    private Label lbl_plungedSlitMaskTit2_0;
    private Label lbl_slitPos2_0_9;
    private Label lbl_slitPos2_0_8;
    private Label lbl_slitPos2_0_7;
    private Label lbl_slitPos2_0_6;
    private Label lbl_slitPos2_0_5;
    private Label lbl_slitPos2_0_4;
    private Label lbl_slitPos2_0_3;
    private Label lbl_slitPos2_0_2;
    private Label lbl_slitPos2_0_1;
    private Label lbl_slitPos2_0_0;
    private Label lbl_slitPosTit2_0;
    private Label lbl_knifeOnwardsTit0;
    private Panel pnl_knifeOnwards0;
    private Label lbl_knifeOnwardsTit1;
    private Panel pnl_knifeOnwards1;
    private Label lbl_knifeOnwardsTit2;
    private Panel pnl_knifeOnwards2;
    private Label lbl_setupId0;
    private Label lbl_setupId1;
    private Label lbl_setupId2;
    private Label lbl_ConfigVersion0;
    private Label lbl_ConfigVersion1;
    private Label lbl_ConfigVersion2;
    private Label lbl_modId0;
    private Label lbl_modId1;
    private Label lbl_modId2;
    private Label lbl_shaftMask0;
    private Label lbl_shaftMaskTit0;
    private Label lbl_shaftMask1;
    private Label lbl_shaftMaskTit1;
    private Label lbl_shaftMask2;
    private Label lbl_shaftMaskTit2;
    private Label lbl_posTime0_1;
    private Label lbl_posTime0_0;
    private Label label1;
    private Label lbl_posTime1_1;
    private Label lbl_posTime1_0;
    private Label label4;
    private Label lbl_posTime2_1;
    private Label lbl_posTime2_0;
    private Label label5;
    private Label label2;
    private Label lbl_actPosTime0;
    private Label lbl_actPosTime1;
    private Label label6;
    private Label lbl_actPosTime2;
    private Label label7;
    private System.ComponentModel.IContainer components = null;

    public Frm_DebugTwin400SetupSituation()
		{
			InitializeComponent();
      Type t = this.GetType();
      FieldInfo f = null;
			this.slitPos = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS][][];
			this.plungedSlitMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS][];
			this.sepSlitMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS][];
			this.dummySlitMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS][];
			this.webDir2LevelMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
			this.webDir3LevelMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      this.rcsMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      this.shaftMask = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      this.setupId = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      this.modId = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      this.configVersion = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      //agrassi_FI19-01-2011_1
      //this.wideToNarrow = new Panel[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      //this.narrowToWide = new Panel[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
      this.posTimes = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS][];
      this.actPosTime = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
			this.cleanSlits = new Panel[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
			this.sendToPlc = new Panel[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
			this.knifeOnwards = new Panel[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
			this.valid = new Panel[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS];
			for (int stp = 0; stp < SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS; stp++)
      {
				this.slitPos[stp] = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
				this.plungedSlitMask[stp] = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
				this.sepSlitMask[stp] = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
				this.dummySlitMask[stp] = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
        //agrassi_FI19-01-2011_1
        f = t.GetField("lbl_actPosTime" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.actPosTime[stp] = (Label)f.GetValue(this);
        this.posTimes[stp] = new Label[2];
        for (int i = 0; i < 2; i++)
        {
          f = t.GetField("lbl_posTime" + stp.ToString() + "_" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
          this.posTimes[stp][i] = (Label)f.GetValue(this);
        }
				for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
        {
					this.slitPos[stp][sh] = new Label[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
					for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
          {
            f = t.GetField("lbl_slitPos" + stp.ToString() + "_" + sh.ToString() + "_" + slt.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
            this.slitPos[stp][sh][slt] = (Label)f.GetValue(this);
          }
          f = t.GetField("lbl_plungedSlitMask" + stp.ToString() + "_" + sh.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
          this.plungedSlitMask[stp][sh] = (Label)f.GetValue(this);
          f = t.GetField("lbl_sepSlitMask" + stp.ToString() + "_" + sh.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
          this.sepSlitMask[stp][sh] = (Label)f.GetValue(this);
          f = t.GetField("lbl_dummySlitMask" + stp.ToString() + "_" + sh.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
          this.dummySlitMask[stp][sh] = (Label)f.GetValue(this);
        }
        f = t.GetField("lbl_webDir2LevelMask" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.webDir2LevelMask[stp] = (Label)f.GetValue(this);
        f = t.GetField("lbl_webDir3LevelMask" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.webDir3LevelMask[stp] = (Label)f.GetValue(this);
        f = t.GetField("lbl_rcsMask" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.rcsMask[stp] = (Label)f.GetValue(this);
        //CM7 19-09-2005
        f = t.GetField("lbl_shaftMask" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.shaftMask[stp] = (Label)f.GetValue(this);
        f = t.GetField("lbl_setupId" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.setupId[stp] = (Label)f.GetValue(this);
        //CM7 19-09-2005
        f = t.GetField("lbl_modId" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.modId[stp] = (Label)f.GetValue(this);
        f = t.GetField("lbl_ConfigVersion" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.configVersion[stp] = (Label)f.GetValue(this);
        //agrassi_FI19-01-2011_1
        //f = t.GetField("pnl_wideToNarrow" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        //this.wideToNarrow[stp] = (Panel)f.GetValue(this);
        //f = t.GetField("pnl_narrowToWide" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        //this.narrowToWide[stp] = (Panel)f.GetValue(this);
        f = t.GetField("pnl_cleanSlits" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.cleanSlits[stp] = (Panel)f.GetValue(this);
        f = t.GetField("pnl_sendToPlc" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.sendToPlc[stp] = (Panel)f.GetValue(this);
        f = t.GetField("pnl_knifeOnwards" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.knifeOnwards[stp] = (Panel)f.GetValue(this);
        f = t.GetField("pnl_valid" + stp.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.valid[stp] = (Panel)f.GetValue(this);
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
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugTwin400SetupSituation));
      this.grp_setup0 = new System.Windows.Forms.GroupBox();
      this.lbl_posTime0_1 = new System.Windows.Forms.Label();
      this.lbl_posTime0_0 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lbl_shaftMask0 = new System.Windows.Forms.Label();
      this.lbl_shaftMaskTit0 = new System.Windows.Forms.Label();
      this.lbl_modId0 = new System.Windows.Forms.Label();
      this.lbl_ConfigVersion0 = new System.Windows.Forms.Label();
      this.lbl_setupId0 = new System.Windows.Forms.Label();
      this.lbl_knifeOnwardsTit0 = new System.Windows.Forms.Label();
      this.pnl_knifeOnwards0 = new System.Windows.Forms.Panel();
      this.lbl_cleanSlitsTit0 = new System.Windows.Forms.Label();
      this.pnl_cleanSlits0 = new System.Windows.Forms.Panel();
      this.lbl_rcsMask0 = new System.Windows.Forms.Label();
      this.lbl_rcsMaskTit0 = new System.Windows.Forms.Label();
      this.lbl_webDir3LevelMask0 = new System.Windows.Forms.Label();
      this.lbl_webDir3LevelMaskTit0 = new System.Windows.Forms.Label();
      this.lbl_webDir2LevelMask0 = new System.Windows.Forms.Label();
      this.lbl_webDir2LevelMaskTit0 = new System.Windows.Forms.Label();
      this.grp_shaft0_1 = new System.Windows.Forms.GroupBox();
      this.lbl_dummySlitMask0_1 = new System.Windows.Forms.Label();
      this.lbl_dummySlitMaskTit0_1 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMask0_1 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMaskTit0_1 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMask0_1 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMaskTit0_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_9 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_8 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_7 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_6 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_5 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_4 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_3 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_2 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_1_0 = new System.Windows.Forms.Label();
      this.lbl_slitPosTit0_1 = new System.Windows.Forms.Label();
      this.lbl_sendToPlcTit0 = new System.Windows.Forms.Label();
      this.pnl_sendToPlc0 = new System.Windows.Forms.Panel();
      this.lbl_validTit0 = new System.Windows.Forms.Label();
      this.pnl_valid0 = new System.Windows.Forms.Panel();
      this.grp_shaft0_0 = new System.Windows.Forms.GroupBox();
      this.lbl_dummySlitMask0_0 = new System.Windows.Forms.Label();
      this.lbl_dummySlitMaskTit0_0 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMask0_0 = new System.Windows.Forms.Label();
      this.lbl_spSlitMaskTit0_0 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMask0_0 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMaskTit0_0 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_9 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_8 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_7 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_6 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_5 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_4 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_3 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_2 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos0_0_0 = new System.Windows.Forms.Label();
      this.lbl_slitPosTit0_0 = new System.Windows.Forms.Label();
      this.grp_setup1 = new System.Windows.Forms.GroupBox();
      this.lbl_posTime1_1 = new System.Windows.Forms.Label();
      this.lbl_posTime1_0 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.lbl_shaftMask1 = new System.Windows.Forms.Label();
      this.lbl_shaftMaskTit1 = new System.Windows.Forms.Label();
      this.lbl_modId1 = new System.Windows.Forms.Label();
      this.lbl_ConfigVersion1 = new System.Windows.Forms.Label();
      this.lbl_setupId1 = new System.Windows.Forms.Label();
      this.lbl_knifeOnwardsTit1 = new System.Windows.Forms.Label();
      this.pnl_knifeOnwards1 = new System.Windows.Forms.Panel();
      this.lbl_cleanSlitsTit1 = new System.Windows.Forms.Label();
      this.pnl_cleanSlits1 = new System.Windows.Forms.Panel();
      this.lbl_rcsMask1 = new System.Windows.Forms.Label();
      this.lbl_rcsMaskTit1 = new System.Windows.Forms.Label();
      this.lbl_webDir3LevelMask1 = new System.Windows.Forms.Label();
      this.lbl_webDir3LevelMaskTit1 = new System.Windows.Forms.Label();
      this.lbl_webDir2LevelMask1 = new System.Windows.Forms.Label();
      this.lbl_webDir2LevelMaskTit1 = new System.Windows.Forms.Label();
      this.grp_shaft1_1 = new System.Windows.Forms.GroupBox();
      this.lbl_dummySlitMask1_1 = new System.Windows.Forms.Label();
      this.lbl_dummySlitMaskTit1_1 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMask1_1 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMaskTit1_1 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMask1_1 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMaskTit1_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_9 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_8 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_7 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_6 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_5 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_4 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_3 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_2 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_1_0 = new System.Windows.Forms.Label();
      this.lbl_slitPosTit1_1 = new System.Windows.Forms.Label();
      this.lbl_sendToPlcTit1 = new System.Windows.Forms.Label();
      this.pnl_sendToPlc1 = new System.Windows.Forms.Panel();
      this.lbl_validTit1 = new System.Windows.Forms.Label();
      this.pnl_valid1 = new System.Windows.Forms.Panel();
      this.grp_shaft1_0 = new System.Windows.Forms.GroupBox();
      this.lbl_dummySlitMask1_0 = new System.Windows.Forms.Label();
      this.lbl_dummySlitMaskTit1_0 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMask1_0 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMaskTit1_0 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMask1_0 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMaskTit1_0 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_9 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_8 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_7 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_6 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_5 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_4 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_3 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_2 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos1_0_0 = new System.Windows.Forms.Label();
      this.lbl_slitPosTit1_0 = new System.Windows.Forms.Label();
      this.grp_setup2 = new System.Windows.Forms.GroupBox();
      this.lbl_posTime2_1 = new System.Windows.Forms.Label();
      this.lbl_posTime2_0 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.lbl_shaftMask2 = new System.Windows.Forms.Label();
      this.lbl_shaftMaskTit2 = new System.Windows.Forms.Label();
      this.lbl_modId2 = new System.Windows.Forms.Label();
      this.lbl_ConfigVersion2 = new System.Windows.Forms.Label();
      this.lbl_setupId2 = new System.Windows.Forms.Label();
      this.lbl_knifeOnwardsTit2 = new System.Windows.Forms.Label();
      this.pnl_knifeOnwards2 = new System.Windows.Forms.Panel();
      this.lbl_cleanSlitsTit2 = new System.Windows.Forms.Label();
      this.pnl_cleanSlits2 = new System.Windows.Forms.Panel();
      this.lbl_rcsMask2 = new System.Windows.Forms.Label();
      this.lbl_rcsMaskTit2 = new System.Windows.Forms.Label();
      this.lbl_webDir3LevelMask2 = new System.Windows.Forms.Label();
      this.lbl_webDir3LevelMaskTit2 = new System.Windows.Forms.Label();
      this.lbl_webDir2LevelMask2 = new System.Windows.Forms.Label();
      this.lbl_webDir2LevelMaskTit2 = new System.Windows.Forms.Label();
      this.grp_shaft2_1 = new System.Windows.Forms.GroupBox();
      this.lbl_dummySlitMask2_1 = new System.Windows.Forms.Label();
      this.lbl_dummySlitMaskTit2_1 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMask2_1 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMaskTit2_1 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMask2_1 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMaskTit2_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_9 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_8 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_7 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_6 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_5 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_4 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_3 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_2 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_1_0 = new System.Windows.Forms.Label();
      this.lbl_slitPosTit2_1 = new System.Windows.Forms.Label();
      this.lbl_sendToPlcTit2 = new System.Windows.Forms.Label();
      this.pnl_sendToPlc2 = new System.Windows.Forms.Panel();
      this.lbl_validTit2 = new System.Windows.Forms.Label();
      this.pnl_valid2 = new System.Windows.Forms.Panel();
      this.grp_shaft2_0 = new System.Windows.Forms.GroupBox();
      this.lbl_dummySlitMask2_0 = new System.Windows.Forms.Label();
      this.lbl_dummySlitMaskTit2_0 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMask2_0 = new System.Windows.Forms.Label();
      this.lbl_sepSlitMaskTit2_0 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMask2_0 = new System.Windows.Forms.Label();
      this.lbl_plungedSlitMaskTit2_0 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_9 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_8 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_7 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_6 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_5 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_4 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_3 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_2 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_1 = new System.Windows.Forms.Label();
      this.lbl_slitPos2_0_0 = new System.Windows.Forms.Label();
      this.lbl_slitPosTit2_0 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lbl_actPosTime0 = new System.Windows.Forms.Label();
      this.lbl_actPosTime1 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.lbl_actPosTime2 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_setup0.SuspendLayout();
      this.grp_shaft0_1.SuspendLayout();
      this.grp_shaft0_0.SuspendLayout();
      this.grp_setup1.SuspendLayout();
      this.grp_shaft1_1.SuspendLayout();
      this.grp_shaft1_0.SuspendLayout();
      this.grp_setup2.SuspendLayout();
      this.grp_shaft2_1.SuspendLayout();
      this.grp_shaft2_0.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Title_YN.Text = "PLC SETUP SITUATION";
      // 
      // lbl_Time
      // 
      this.lbl_Time.Margin = new System.Windows.Forms.Padding(1, 3, 3, 1);
      // 
      // lbl_Date
      // 
      this.lbl_Date.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
      // 
      // lbl_Status
      // 
      this.lbl_Status.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // tab_TabA
      // 
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // grp_setup0
      // 
      this.grp_setup0.Controls.Add(this.lbl_actPosTime0);
      this.grp_setup0.Controls.Add(this.label2);
      this.grp_setup0.Controls.Add(this.lbl_posTime0_1);
      this.grp_setup0.Controls.Add(this.lbl_posTime0_0);
      this.grp_setup0.Controls.Add(this.label1);
      this.grp_setup0.Controls.Add(this.lbl_shaftMask0);
      this.grp_setup0.Controls.Add(this.lbl_shaftMaskTit0);
      this.grp_setup0.Controls.Add(this.lbl_modId0);
      this.grp_setup0.Controls.Add(this.lbl_ConfigVersion0);
      this.grp_setup0.Controls.Add(this.lbl_setupId0);
      this.grp_setup0.Controls.Add(this.lbl_knifeOnwardsTit0);
      this.grp_setup0.Controls.Add(this.pnl_knifeOnwards0);
      this.grp_setup0.Controls.Add(this.lbl_cleanSlitsTit0);
      this.grp_setup0.Controls.Add(this.pnl_cleanSlits0);
      this.grp_setup0.Controls.Add(this.lbl_rcsMask0);
      this.grp_setup0.Controls.Add(this.lbl_rcsMaskTit0);
      this.grp_setup0.Controls.Add(this.lbl_webDir3LevelMask0);
      this.grp_setup0.Controls.Add(this.lbl_webDir3LevelMaskTit0);
      this.grp_setup0.Controls.Add(this.lbl_webDir2LevelMask0);
      this.grp_setup0.Controls.Add(this.lbl_webDir2LevelMaskTit0);
      this.grp_setup0.Controls.Add(this.grp_shaft0_1);
      this.grp_setup0.Controls.Add(this.lbl_sendToPlcTit0);
      this.grp_setup0.Controls.Add(this.pnl_sendToPlc0);
      this.grp_setup0.Controls.Add(this.lbl_validTit0);
      this.grp_setup0.Controls.Add(this.pnl_valid0);
      this.grp_setup0.Controls.Add(this.grp_shaft0_0);
      this.grp_setup0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_setup0.Location = new System.Drawing.Point(110, 168);
      this.grp_setup0.Name = "grp_setup0";
      this.grp_setup0.Size = new System.Drawing.Size(372, 570);
      this.grp_setup0.TabIndex = 110;
      this.grp_setup0.TabStop = false;
      this.grp_setup0.Text = "Setup 1";
      // 
      // lbl_posTime0_1
      // 
      this.lbl_posTime0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_posTime0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_posTime0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_posTime0_1.Location = new System.Drawing.Point(316, 498);
      this.lbl_posTime0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_posTime0_1.Name = "lbl_posTime0_1";
      this.lbl_posTime0_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_posTime0_1.TabIndex = 37;
      // 
      // lbl_posTime0_0
      // 
      this.lbl_posTime0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_posTime0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_posTime0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_posTime0_0.Location = new System.Drawing.Point(234, 498);
      this.lbl_posTime0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_posTime0_0.Name = "lbl_posTime0_0";
      this.lbl_posTime0_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_posTime0_0.TabIndex = 36;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(7, 498);
      this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(194, 20);
      this.label1.TabIndex = 35;
      this.label1.Text = "Extim pos times (Slow - Fast):";
      // 
      // lbl_shaftMask0
      // 
      this.lbl_shaftMask0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_shaftMask0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_shaftMask0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_shaftMask0.Location = new System.Drawing.Point(255, 477);
      this.lbl_shaftMask0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_shaftMask0.Name = "lbl_shaftMask0";
      this.lbl_shaftMask0.Size = new System.Drawing.Size(111, 20);
      this.lbl_shaftMask0.TabIndex = 34;
      // 
      // lbl_shaftMaskTit0
      // 
      this.lbl_shaftMaskTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_shaftMaskTit0.Location = new System.Drawing.Point(7, 477);
      this.lbl_shaftMaskTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_shaftMaskTit0.Name = "lbl_shaftMaskTit0";
      this.lbl_shaftMaskTit0.Size = new System.Drawing.Size(173, 20);
      this.lbl_shaftMaskTit0.TabIndex = 33;
      this.lbl_shaftMaskTit0.Text = "Shaft Mask:";
      // 
      // lbl_modId0
      // 
      this.lbl_modId0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_modId0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_modId0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_modId0.Location = new System.Drawing.Point(69, 21);
      this.lbl_modId0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_modId0.Name = "lbl_modId0";
      this.lbl_modId0.Size = new System.Drawing.Size(26, 20);
      this.lbl_modId0.TabIndex = 32;
      // 
      // lbl_ConfigVersion0
      // 
      this.lbl_ConfigVersion0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ConfigVersion0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ConfigVersion0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_ConfigVersion0.Location = new System.Drawing.Point(118, 21);
      this.lbl_ConfigVersion0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_ConfigVersion0.Name = "lbl_ConfigVersion0";
      this.lbl_ConfigVersion0.Size = new System.Drawing.Size(48, 20);
      this.lbl_ConfigVersion0.TabIndex = 31;
      // 
      // lbl_setupId0
      // 
      this.lbl_setupId0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_setupId0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_setupId0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_setupId0.Location = new System.Drawing.Point(5, 21);
      this.lbl_setupId0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_setupId0.Name = "lbl_setupId0";
      this.lbl_setupId0.Size = new System.Drawing.Size(48, 20);
      this.lbl_setupId0.TabIndex = 30;
      // 
      // lbl_knifeOnwardsTit0
      // 
      this.lbl_knifeOnwardsTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_knifeOnwardsTit0.Location = new System.Drawing.Point(237, 546);
      this.lbl_knifeOnwardsTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_knifeOnwardsTit0.Name = "lbl_knifeOnwardsTit0";
      this.lbl_knifeOnwardsTit0.Size = new System.Drawing.Size(127, 20);
      this.lbl_knifeOnwardsTit0.TabIndex = 28;
      this.lbl_knifeOnwardsTit0.Text = "First of Knife Onwds";
      // 
      // pnl_knifeOnwards0
      // 
      this.pnl_knifeOnwards0.BackColor = System.Drawing.Color.LightGray;
      this.pnl_knifeOnwards0.Location = new System.Drawing.Point(212, 546);
      this.pnl_knifeOnwards0.Name = "pnl_knifeOnwards0";
      this.pnl_knifeOnwards0.Size = new System.Drawing.Size(18, 16);
      this.pnl_knifeOnwards0.TabIndex = 29;
      // 
      // lbl_cleanSlitsTit0
      // 
      this.lbl_cleanSlitsTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_cleanSlitsTit0.Location = new System.Drawing.Point(34, 546);
      this.lbl_cleanSlitsTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_cleanSlitsTit0.Name = "lbl_cleanSlitsTit0";
      this.lbl_cleanSlitsTit0.Size = new System.Drawing.Size(120, 20);
      this.lbl_cleanSlitsTit0.TabIndex = 26;
      this.lbl_cleanSlitsTit0.Text = "Clean Slits";
      // 
      // pnl_cleanSlits0
      // 
      this.pnl_cleanSlits0.BackColor = System.Drawing.Color.LightGray;
      this.pnl_cleanSlits0.Location = new System.Drawing.Point(9, 546);
      this.pnl_cleanSlits0.Name = "pnl_cleanSlits0";
      this.pnl_cleanSlits0.Size = new System.Drawing.Size(18, 16);
      this.pnl_cleanSlits0.TabIndex = 27;
      // 
      // lbl_rcsMask0
      // 
      this.lbl_rcsMask0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_rcsMask0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_rcsMask0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_rcsMask0.Location = new System.Drawing.Point(255, 455);
      this.lbl_rcsMask0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_rcsMask0.Name = "lbl_rcsMask0";
      this.lbl_rcsMask0.Size = new System.Drawing.Size(111, 20);
      this.lbl_rcsMask0.TabIndex = 21;
      // 
      // lbl_rcsMaskTit0
      // 
      this.lbl_rcsMaskTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_rcsMaskTit0.Location = new System.Drawing.Point(7, 455);
      this.lbl_rcsMaskTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_rcsMaskTit0.Name = "lbl_rcsMaskTit0";
      this.lbl_rcsMaskTit0.Size = new System.Drawing.Size(173, 20);
      this.lbl_rcsMaskTit0.TabIndex = 20;
      this.lbl_rcsMaskTit0.Text = "Rotary CS Mask:";
      // 
      // lbl_webDir3LevelMask0
      // 
      this.lbl_webDir3LevelMask0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_webDir3LevelMask0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir3LevelMask0.ForeColor = System.Drawing.Color.DimGray;
      this.lbl_webDir3LevelMask0.Location = new System.Drawing.Point(255, 433);
      this.lbl_webDir3LevelMask0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir3LevelMask0.Name = "lbl_webDir3LevelMask0";
      this.lbl_webDir3LevelMask0.Size = new System.Drawing.Size(111, 20);
      this.lbl_webDir3LevelMask0.TabIndex = 19;
      // 
      // lbl_webDir3LevelMaskTit0
      // 
      this.lbl_webDir3LevelMaskTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir3LevelMaskTit0.Location = new System.Drawing.Point(7, 433);
      this.lbl_webDir3LevelMaskTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir3LevelMaskTit0.Name = "lbl_webDir3LevelMaskTit0";
      this.lbl_webDir3LevelMaskTit0.Size = new System.Drawing.Size(173, 20);
      this.lbl_webDir3LevelMaskTit0.TabIndex = 18;
      this.lbl_webDir3LevelMaskTit0.Text = "3-level Web Director Mask:";
      // 
      // lbl_webDir2LevelMask0
      // 
      this.lbl_webDir2LevelMask0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_webDir2LevelMask0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir2LevelMask0.ForeColor = System.Drawing.Color.DimGray;
      this.lbl_webDir2LevelMask0.Location = new System.Drawing.Point(255, 411);
      this.lbl_webDir2LevelMask0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir2LevelMask0.Name = "lbl_webDir2LevelMask0";
      this.lbl_webDir2LevelMask0.Size = new System.Drawing.Size(111, 20);
      this.lbl_webDir2LevelMask0.TabIndex = 17;
      // 
      // lbl_webDir2LevelMaskTit0
      // 
      this.lbl_webDir2LevelMaskTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir2LevelMaskTit0.Location = new System.Drawing.Point(7, 411);
      this.lbl_webDir2LevelMaskTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir2LevelMaskTit0.Name = "lbl_webDir2LevelMaskTit0";
      this.lbl_webDir2LevelMaskTit0.Size = new System.Drawing.Size(173, 20);
      this.lbl_webDir2LevelMaskTit0.TabIndex = 16;
      this.lbl_webDir2LevelMaskTit0.Text = "2-level Web Director Mask:";
      // 
      // grp_shaft0_1
      // 
      this.grp_shaft0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.grp_shaft0_1.Controls.Add(this.lbl_dummySlitMask0_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_dummySlitMaskTit0_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_sepSlitMask0_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_sepSlitMaskTit0_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_plungedSlitMask0_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_plungedSlitMaskTit0_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_9);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_8);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_7);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_6);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_5);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_4);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_3);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_2);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_1);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPos0_1_0);
      this.grp_shaft0_1.Controls.Add(this.lbl_slitPosTit0_1);
      this.grp_shaft0_1.Location = new System.Drawing.Point(0, 235);
      this.grp_shaft0_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.grp_shaft0_1.Name = "grp_shaft0_1";
      this.grp_shaft0_1.Size = new System.Drawing.Size(372, 169);
      this.grp_shaft0_1.TabIndex = 4;
      this.grp_shaft0_1.TabStop = false;
      this.grp_shaft0_1.Text = "Slit Shaft 2";
      // 
      // lbl_dummySlitMask0_1
      // 
      this.lbl_dummySlitMask0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_dummySlitMask0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMask0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_dummySlitMask0_1.Location = new System.Drawing.Point(255, 138);
      this.lbl_dummySlitMask0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMask0_1.Name = "lbl_dummySlitMask0_1";
      this.lbl_dummySlitMask0_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_dummySlitMask0_1.TabIndex = 19;
      // 
      // lbl_dummySlitMaskTit0_1
      // 
      this.lbl_dummySlitMaskTit0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMaskTit0_1.Location = new System.Drawing.Point(7, 138);
      this.lbl_dummySlitMaskTit0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMaskTit0_1.Name = "lbl_dummySlitMaskTit0_1";
      this.lbl_dummySlitMaskTit0_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_dummySlitMaskTit0_1.TabIndex = 18;
      this.lbl_dummySlitMaskTit0_1.Text = "Dummy Slit Mask:";
      // 
      // lbl_sepSlitMask0_1
      // 
      this.lbl_sepSlitMask0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_sepSlitMask0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMask0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_sepSlitMask0_1.Location = new System.Drawing.Point(255, 113);
      this.lbl_sepSlitMask0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMask0_1.Name = "lbl_sepSlitMask0_1";
      this.lbl_sepSlitMask0_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_sepSlitMask0_1.TabIndex = 17;
      // 
      // lbl_sepSlitMaskTit0_1
      // 
      this.lbl_sepSlitMaskTit0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMaskTit0_1.Location = new System.Drawing.Point(7, 113);
      this.lbl_sepSlitMaskTit0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMaskTit0_1.Name = "lbl_sepSlitMaskTit0_1";
      this.lbl_sepSlitMaskTit0_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_sepSlitMaskTit0_1.TabIndex = 16;
      this.lbl_sepSlitMaskTit0_1.Text = "Separation Slit Mask:";
      // 
      // lbl_plungedSlitMask0_1
      // 
      this.lbl_plungedSlitMask0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_plungedSlitMask0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMask0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_plungedSlitMask0_1.Location = new System.Drawing.Point(255, 88);
      this.lbl_plungedSlitMask0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMask0_1.Name = "lbl_plungedSlitMask0_1";
      this.lbl_plungedSlitMask0_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_plungedSlitMask0_1.TabIndex = 15;
      // 
      // lbl_plungedSlitMaskTit0_1
      // 
      this.lbl_plungedSlitMaskTit0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMaskTit0_1.Location = new System.Drawing.Point(7, 88);
      this.lbl_plungedSlitMaskTit0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMaskTit0_1.Name = "lbl_plungedSlitMaskTit0_1";
      this.lbl_plungedSlitMaskTit0_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_plungedSlitMaskTit0_1.TabIndex = 14;
      this.lbl_plungedSlitMaskTit0_1.Text = "Plunged Slit Mask:";
      // 
      // lbl_slitPos0_1_9
      // 
      this.lbl_slitPos0_1_9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_9.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_9.Location = new System.Drawing.Point(318, 48);
      this.lbl_slitPos0_1_9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_9.Name = "lbl_slitPos0_1_9";
      this.lbl_slitPos0_1_9.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_9.TabIndex = 13;
      // 
      // lbl_slitPos0_1_8
      // 
      this.lbl_slitPos0_1_8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_8.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_8.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_8.Location = new System.Drawing.Point(255, 48);
      this.lbl_slitPos0_1_8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_8.Name = "lbl_slitPos0_1_8";
      this.lbl_slitPos0_1_8.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_8.TabIndex = 12;
      // 
      // lbl_slitPos0_1_7
      // 
      this.lbl_slitPos0_1_7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_7.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_7.Location = new System.Drawing.Point(193, 48);
      this.lbl_slitPos0_1_7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_7.Name = "lbl_slitPos0_1_7";
      this.lbl_slitPos0_1_7.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_7.TabIndex = 11;
      // 
      // lbl_slitPos0_1_6
      // 
      this.lbl_slitPos0_1_6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_6.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_6.Location = new System.Drawing.Point(132, 48);
      this.lbl_slitPos0_1_6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_6.Name = "lbl_slitPos0_1_6";
      this.lbl_slitPos0_1_6.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_6.TabIndex = 10;
      // 
      // lbl_slitPos0_1_5
      // 
      this.lbl_slitPos0_1_5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_5.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_5.Location = new System.Drawing.Point(68, 48);
      this.lbl_slitPos0_1_5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_5.Name = "lbl_slitPos0_1_5";
      this.lbl_slitPos0_1_5.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_5.TabIndex = 9;
      // 
      // lbl_slitPos0_1_4
      // 
      this.lbl_slitPos0_1_4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_4.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_4.Location = new System.Drawing.Point(7, 48);
      this.lbl_slitPos0_1_4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_4.Name = "lbl_slitPos0_1_4";
      this.lbl_slitPos0_1_4.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_4.TabIndex = 8;
      // 
      // lbl_slitPos0_1_3
      // 
      this.lbl_slitPos0_1_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_3.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_3.Location = new System.Drawing.Point(318, 20);
      this.lbl_slitPos0_1_3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_3.Name = "lbl_slitPos0_1_3";
      this.lbl_slitPos0_1_3.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_3.TabIndex = 7;
      // 
      // lbl_slitPos0_1_2
      // 
      this.lbl_slitPos0_1_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_2.Location = new System.Drawing.Point(255, 20);
      this.lbl_slitPos0_1_2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_2.Name = "lbl_slitPos0_1_2";
      this.lbl_slitPos0_1_2.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_2.TabIndex = 6;
      // 
      // lbl_slitPos0_1_1
      // 
      this.lbl_slitPos0_1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_1.Location = new System.Drawing.Point(193, 20);
      this.lbl_slitPos0_1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_1.Name = "lbl_slitPos0_1_1";
      this.lbl_slitPos0_1_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_1.TabIndex = 5;
      // 
      // lbl_slitPos0_1_0
      // 
      this.lbl_slitPos0_1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_1_0.Location = new System.Drawing.Point(132, 20);
      this.lbl_slitPos0_1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_1_0.Name = "lbl_slitPos0_1_0";
      this.lbl_slitPos0_1_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_1_0.TabIndex = 4;
      // 
      // lbl_slitPosTit0_1
      // 
      this.lbl_slitPosTit0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPosTit0_1.Location = new System.Drawing.Point(4, 20);
      this.lbl_slitPosTit0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPosTit0_1.Name = "lbl_slitPosTit0_1";
      this.lbl_slitPosTit0_1.Size = new System.Drawing.Size(112, 20);
      this.lbl_slitPosTit0_1.TabIndex = 3;
      this.lbl_slitPosTit0_1.Text = "Slit Positions:";
      // 
      // lbl_sendToPlcTit0
      // 
      this.lbl_sendToPlcTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sendToPlcTit0.Location = new System.Drawing.Point(286, 21);
      this.lbl_sendToPlcTit0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sendToPlcTit0.Name = "lbl_sendToPlcTit0";
      this.lbl_sendToPlcTit0.Size = new System.Drawing.Size(83, 20);
      this.lbl_sendToPlcTit0.TabIndex = 2;
      this.lbl_sendToPlcTit0.Text = "Send to PLC";
      // 
      // pnl_sendToPlc0
      // 
      this.pnl_sendToPlc0.BackColor = System.Drawing.Color.LightGray;
      this.pnl_sendToPlc0.Location = new System.Drawing.Point(264, 21);
      this.pnl_sendToPlc0.Name = "pnl_sendToPlc0";
      this.pnl_sendToPlc0.Size = new System.Drawing.Size(18, 16);
      this.pnl_sendToPlc0.TabIndex = 3;
      // 
      // lbl_validTit0
      // 
      this.lbl_validTit0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_validTit0.Location = new System.Drawing.Point(204, 21);
      this.lbl_validTit0.Name = "lbl_validTit0";
      this.lbl_validTit0.Size = new System.Drawing.Size(49, 20);
      this.lbl_validTit0.TabIndex = 0;
      this.lbl_validTit0.Text = "Valid";
      // 
      // pnl_valid0
      // 
      this.pnl_valid0.BackColor = System.Drawing.Color.LightGray;
      this.pnl_valid0.Location = new System.Drawing.Point(183, 21);
      this.pnl_valid0.Name = "pnl_valid0";
      this.pnl_valid0.Size = new System.Drawing.Size(18, 16);
      this.pnl_valid0.TabIndex = 1;
      // 
      // grp_shaft0_0
      // 
      this.grp_shaft0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.grp_shaft0_0.Controls.Add(this.lbl_dummySlitMask0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_dummySlitMaskTit0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_sepSlitMask0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_spSlitMaskTit0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_plungedSlitMask0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_plungedSlitMaskTit0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_9);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_8);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_7);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_6);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_5);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_4);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_3);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_2);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_1);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPos0_0_0);
      this.grp_shaft0_0.Controls.Add(this.lbl_slitPosTit0_0);
      this.grp_shaft0_0.Location = new System.Drawing.Point(0, 53);
      this.grp_shaft0_0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.grp_shaft0_0.Name = "grp_shaft0_0";
      this.grp_shaft0_0.Size = new System.Drawing.Size(372, 169);
      this.grp_shaft0_0.TabIndex = 0;
      this.grp_shaft0_0.TabStop = false;
      this.grp_shaft0_0.Text = "Slit Shaft 1";
      // 
      // lbl_dummySlitMask0_0
      // 
      this.lbl_dummySlitMask0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_dummySlitMask0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMask0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_dummySlitMask0_0.Location = new System.Drawing.Point(255, 138);
      this.lbl_dummySlitMask0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMask0_0.Name = "lbl_dummySlitMask0_0";
      this.lbl_dummySlitMask0_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_dummySlitMask0_0.TabIndex = 19;
      // 
      // lbl_dummySlitMaskTit0_0
      // 
      this.lbl_dummySlitMaskTit0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMaskTit0_0.Location = new System.Drawing.Point(7, 138);
      this.lbl_dummySlitMaskTit0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMaskTit0_0.Name = "lbl_dummySlitMaskTit0_0";
      this.lbl_dummySlitMaskTit0_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_dummySlitMaskTit0_0.TabIndex = 18;
      this.lbl_dummySlitMaskTit0_0.Text = "Dummy Slit Mask:";
      // 
      // lbl_sepSlitMask0_0
      // 
      this.lbl_sepSlitMask0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_sepSlitMask0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMask0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_sepSlitMask0_0.Location = new System.Drawing.Point(255, 113);
      this.lbl_sepSlitMask0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMask0_0.Name = "lbl_sepSlitMask0_0";
      this.lbl_sepSlitMask0_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_sepSlitMask0_0.TabIndex = 17;
      // 
      // lbl_spSlitMaskTit0_0
      // 
      this.lbl_spSlitMaskTit0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_spSlitMaskTit0_0.Location = new System.Drawing.Point(7, 113);
      this.lbl_spSlitMaskTit0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_spSlitMaskTit0_0.Name = "lbl_spSlitMaskTit0_0";
      this.lbl_spSlitMaskTit0_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_spSlitMaskTit0_0.TabIndex = 16;
      this.lbl_spSlitMaskTit0_0.Text = "Separation Slit Mask:";
      // 
      // lbl_plungedSlitMask0_0
      // 
      this.lbl_plungedSlitMask0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_plungedSlitMask0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMask0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_plungedSlitMask0_0.Location = new System.Drawing.Point(255, 88);
      this.lbl_plungedSlitMask0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMask0_0.Name = "lbl_plungedSlitMask0_0";
      this.lbl_plungedSlitMask0_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_plungedSlitMask0_0.TabIndex = 15;
      // 
      // lbl_plungedSlitMaskTit0_0
      // 
      this.lbl_plungedSlitMaskTit0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMaskTit0_0.Location = new System.Drawing.Point(7, 88);
      this.lbl_plungedSlitMaskTit0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMaskTit0_0.Name = "lbl_plungedSlitMaskTit0_0";
      this.lbl_plungedSlitMaskTit0_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_plungedSlitMaskTit0_0.TabIndex = 14;
      this.lbl_plungedSlitMaskTit0_0.Text = "Plunged Slit Mask:";
      // 
      // lbl_slitPos0_0_9
      // 
      this.lbl_slitPos0_0_9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_9.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_9.Location = new System.Drawing.Point(318, 48);
      this.lbl_slitPos0_0_9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_9.Name = "lbl_slitPos0_0_9";
      this.lbl_slitPos0_0_9.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_9.TabIndex = 13;
      // 
      // lbl_slitPos0_0_8
      // 
      this.lbl_slitPos0_0_8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_8.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_8.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_8.Location = new System.Drawing.Point(255, 48);
      this.lbl_slitPos0_0_8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_8.Name = "lbl_slitPos0_0_8";
      this.lbl_slitPos0_0_8.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_8.TabIndex = 12;
      // 
      // lbl_slitPos0_0_7
      // 
      this.lbl_slitPos0_0_7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_7.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_7.Location = new System.Drawing.Point(193, 48);
      this.lbl_slitPos0_0_7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_7.Name = "lbl_slitPos0_0_7";
      this.lbl_slitPos0_0_7.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_7.TabIndex = 11;
      // 
      // lbl_slitPos0_0_6
      // 
      this.lbl_slitPos0_0_6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_6.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_6.Location = new System.Drawing.Point(132, 48);
      this.lbl_slitPos0_0_6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_6.Name = "lbl_slitPos0_0_6";
      this.lbl_slitPos0_0_6.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_6.TabIndex = 10;
      // 
      // lbl_slitPos0_0_5
      // 
      this.lbl_slitPos0_0_5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_5.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_5.Location = new System.Drawing.Point(68, 48);
      this.lbl_slitPos0_0_5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_5.Name = "lbl_slitPos0_0_5";
      this.lbl_slitPos0_0_5.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_5.TabIndex = 9;
      // 
      // lbl_slitPos0_0_4
      // 
      this.lbl_slitPos0_0_4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_4.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_4.Location = new System.Drawing.Point(7, 48);
      this.lbl_slitPos0_0_4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_4.Name = "lbl_slitPos0_0_4";
      this.lbl_slitPos0_0_4.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_4.TabIndex = 8;
      // 
      // lbl_slitPos0_0_3
      // 
      this.lbl_slitPos0_0_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_3.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_3.Location = new System.Drawing.Point(318, 20);
      this.lbl_slitPos0_0_3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_3.Name = "lbl_slitPos0_0_3";
      this.lbl_slitPos0_0_3.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_3.TabIndex = 7;
      // 
      // lbl_slitPos0_0_2
      // 
      this.lbl_slitPos0_0_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_2.Location = new System.Drawing.Point(255, 20);
      this.lbl_slitPos0_0_2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_2.Name = "lbl_slitPos0_0_2";
      this.lbl_slitPos0_0_2.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_2.TabIndex = 6;
      // 
      // lbl_slitPos0_0_1
      // 
      this.lbl_slitPos0_0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_1.Location = new System.Drawing.Point(193, 20);
      this.lbl_slitPos0_0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_1.Name = "lbl_slitPos0_0_1";
      this.lbl_slitPos0_0_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_1.TabIndex = 5;
      // 
      // lbl_slitPos0_0_0
      // 
      this.lbl_slitPos0_0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos0_0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos0_0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos0_0_0.Location = new System.Drawing.Point(132, 20);
      this.lbl_slitPos0_0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos0_0_0.Name = "lbl_slitPos0_0_0";
      this.lbl_slitPos0_0_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos0_0_0.TabIndex = 4;
      // 
      // lbl_slitPosTit0_0
      // 
      this.lbl_slitPosTit0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPosTit0_0.Location = new System.Drawing.Point(4, 20);
      this.lbl_slitPosTit0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPosTit0_0.Name = "lbl_slitPosTit0_0";
      this.lbl_slitPosTit0_0.Size = new System.Drawing.Size(112, 20);
      this.lbl_slitPosTit0_0.TabIndex = 3;
      this.lbl_slitPosTit0_0.Text = "Slit Positions:";
      // 
      // grp_setup1
      // 
      this.grp_setup1.Controls.Add(this.lbl_actPosTime1);
      this.grp_setup1.Controls.Add(this.label6);
      this.grp_setup1.Controls.Add(this.lbl_posTime1_1);
      this.grp_setup1.Controls.Add(this.lbl_posTime1_0);
      this.grp_setup1.Controls.Add(this.label4);
      this.grp_setup1.Controls.Add(this.lbl_shaftMask1);
      this.grp_setup1.Controls.Add(this.lbl_shaftMaskTit1);
      this.grp_setup1.Controls.Add(this.lbl_modId1);
      this.grp_setup1.Controls.Add(this.lbl_ConfigVersion1);
      this.grp_setup1.Controls.Add(this.lbl_setupId1);
      this.grp_setup1.Controls.Add(this.lbl_knifeOnwardsTit1);
      this.grp_setup1.Controls.Add(this.pnl_knifeOnwards1);
      this.grp_setup1.Controls.Add(this.lbl_cleanSlitsTit1);
      this.grp_setup1.Controls.Add(this.pnl_cleanSlits1);
      this.grp_setup1.Controls.Add(this.lbl_rcsMask1);
      this.grp_setup1.Controls.Add(this.lbl_rcsMaskTit1);
      this.grp_setup1.Controls.Add(this.lbl_webDir3LevelMask1);
      this.grp_setup1.Controls.Add(this.lbl_webDir3LevelMaskTit1);
      this.grp_setup1.Controls.Add(this.lbl_webDir2LevelMask1);
      this.grp_setup1.Controls.Add(this.lbl_webDir2LevelMaskTit1);
      this.grp_setup1.Controls.Add(this.grp_shaft1_1);
      this.grp_setup1.Controls.Add(this.lbl_sendToPlcTit1);
      this.grp_setup1.Controls.Add(this.pnl_sendToPlc1);
      this.grp_setup1.Controls.Add(this.lbl_validTit1);
      this.grp_setup1.Controls.Add(this.pnl_valid1);
      this.grp_setup1.Controls.Add(this.grp_shaft1_0);
      this.grp_setup1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_setup1.Location = new System.Drawing.Point(482, 168);
      this.grp_setup1.Name = "grp_setup1";
      this.grp_setup1.Size = new System.Drawing.Size(372, 570);
      this.grp_setup1.TabIndex = 111;
      this.grp_setup1.TabStop = false;
      this.grp_setup1.Text = "Setup 2";
      // 
      // lbl_posTime1_1
      // 
      this.lbl_posTime1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_posTime1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_posTime1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_posTime1_1.Location = new System.Drawing.Point(316, 498);
      this.lbl_posTime1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_posTime1_1.Name = "lbl_posTime1_1";
      this.lbl_posTime1_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_posTime1_1.TabIndex = 40;
      // 
      // lbl_posTime1_0
      // 
      this.lbl_posTime1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_posTime1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_posTime1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_posTime1_0.Location = new System.Drawing.Point(234, 498);
      this.lbl_posTime1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_posTime1_0.Name = "lbl_posTime1_0";
      this.lbl_posTime1_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_posTime1_0.TabIndex = 39;
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(7, 498);
      this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(191, 20);
      this.label4.TabIndex = 38;
      this.label4.Text = "Extim pos times (Slow - Fast):";
      // 
      // lbl_shaftMask1
      // 
      this.lbl_shaftMask1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_shaftMask1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_shaftMask1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_shaftMask1.Location = new System.Drawing.Point(255, 477);
      this.lbl_shaftMask1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_shaftMask1.Name = "lbl_shaftMask1";
      this.lbl_shaftMask1.Size = new System.Drawing.Size(111, 20);
      this.lbl_shaftMask1.TabIndex = 36;
      // 
      // lbl_shaftMaskTit1
      // 
      this.lbl_shaftMaskTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_shaftMaskTit1.Location = new System.Drawing.Point(7, 477);
      this.lbl_shaftMaskTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_shaftMaskTit1.Name = "lbl_shaftMaskTit1";
      this.lbl_shaftMaskTit1.Size = new System.Drawing.Size(173, 20);
      this.lbl_shaftMaskTit1.TabIndex = 35;
      this.lbl_shaftMaskTit1.Text = "Shaft Mask:";
      // 
      // lbl_modId1
      // 
      this.lbl_modId1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_modId1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_modId1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_modId1.Location = new System.Drawing.Point(68, 21);
      this.lbl_modId1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_modId1.Name = "lbl_modId1";
      this.lbl_modId1.Size = new System.Drawing.Size(26, 20);
      this.lbl_modId1.TabIndex = 34;
      // 
      // lbl_ConfigVersion1
      // 
      this.lbl_ConfigVersion1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ConfigVersion1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ConfigVersion1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_ConfigVersion1.Location = new System.Drawing.Point(118, 21);
      this.lbl_ConfigVersion1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_ConfigVersion1.Name = "lbl_ConfigVersion1";
      this.lbl_ConfigVersion1.Size = new System.Drawing.Size(48, 20);
      this.lbl_ConfigVersion1.TabIndex = 33;
      // 
      // lbl_setupId1
      // 
      this.lbl_setupId1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_setupId1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_setupId1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_setupId1.Location = new System.Drawing.Point(7, 21);
      this.lbl_setupId1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_setupId1.Name = "lbl_setupId1";
      this.lbl_setupId1.Size = new System.Drawing.Size(48, 20);
      this.lbl_setupId1.TabIndex = 32;
      // 
      // lbl_knifeOnwardsTit1
      // 
      this.lbl_knifeOnwardsTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_knifeOnwardsTit1.Location = new System.Drawing.Point(237, 546);
      this.lbl_knifeOnwardsTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_knifeOnwardsTit1.Name = "lbl_knifeOnwardsTit1";
      this.lbl_knifeOnwardsTit1.Size = new System.Drawing.Size(127, 20);
      this.lbl_knifeOnwardsTit1.TabIndex = 30;
      this.lbl_knifeOnwardsTit1.Text = "First of Knife Onwds";
      // 
      // pnl_knifeOnwards1
      // 
      this.pnl_knifeOnwards1.BackColor = System.Drawing.Color.LightGray;
      this.pnl_knifeOnwards1.Location = new System.Drawing.Point(212, 546);
      this.pnl_knifeOnwards1.Name = "pnl_knifeOnwards1";
      this.pnl_knifeOnwards1.Size = new System.Drawing.Size(18, 16);
      this.pnl_knifeOnwards1.TabIndex = 31;
      // 
      // lbl_cleanSlitsTit1
      // 
      this.lbl_cleanSlitsTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_cleanSlitsTit1.Location = new System.Drawing.Point(34, 546);
      this.lbl_cleanSlitsTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_cleanSlitsTit1.Name = "lbl_cleanSlitsTit1";
      this.lbl_cleanSlitsTit1.Size = new System.Drawing.Size(120, 20);
      this.lbl_cleanSlitsTit1.TabIndex = 26;
      this.lbl_cleanSlitsTit1.Text = "Clean Slits";
      // 
      // pnl_cleanSlits1
      // 
      this.pnl_cleanSlits1.BackColor = System.Drawing.Color.LightGray;
      this.pnl_cleanSlits1.Location = new System.Drawing.Point(9, 546);
      this.pnl_cleanSlits1.Name = "pnl_cleanSlits1";
      this.pnl_cleanSlits1.Size = new System.Drawing.Size(18, 16);
      this.pnl_cleanSlits1.TabIndex = 27;
      // 
      // lbl_rcsMask1
      // 
      this.lbl_rcsMask1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_rcsMask1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_rcsMask1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_rcsMask1.Location = new System.Drawing.Point(255, 455);
      this.lbl_rcsMask1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_rcsMask1.Name = "lbl_rcsMask1";
      this.lbl_rcsMask1.Size = new System.Drawing.Size(111, 20);
      this.lbl_rcsMask1.TabIndex = 21;
      // 
      // lbl_rcsMaskTit1
      // 
      this.lbl_rcsMaskTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_rcsMaskTit1.Location = new System.Drawing.Point(7, 455);
      this.lbl_rcsMaskTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_rcsMaskTit1.Name = "lbl_rcsMaskTit1";
      this.lbl_rcsMaskTit1.Size = new System.Drawing.Size(173, 20);
      this.lbl_rcsMaskTit1.TabIndex = 20;
      this.lbl_rcsMaskTit1.Text = "Rotary CS Mask:";
      // 
      // lbl_webDir3LevelMask1
      // 
      this.lbl_webDir3LevelMask1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_webDir3LevelMask1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir3LevelMask1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_webDir3LevelMask1.Location = new System.Drawing.Point(255, 433);
      this.lbl_webDir3LevelMask1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir3LevelMask1.Name = "lbl_webDir3LevelMask1";
      this.lbl_webDir3LevelMask1.Size = new System.Drawing.Size(111, 20);
      this.lbl_webDir3LevelMask1.TabIndex = 19;
      // 
      // lbl_webDir3LevelMaskTit1
      // 
      this.lbl_webDir3LevelMaskTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir3LevelMaskTit1.Location = new System.Drawing.Point(7, 433);
      this.lbl_webDir3LevelMaskTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir3LevelMaskTit1.Name = "lbl_webDir3LevelMaskTit1";
      this.lbl_webDir3LevelMaskTit1.Size = new System.Drawing.Size(173, 20);
      this.lbl_webDir3LevelMaskTit1.TabIndex = 18;
      this.lbl_webDir3LevelMaskTit1.Text = "3-level Web Director Mask:";
      // 
      // lbl_webDir2LevelMask1
      // 
      this.lbl_webDir2LevelMask1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_webDir2LevelMask1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir2LevelMask1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_webDir2LevelMask1.Location = new System.Drawing.Point(255, 411);
      this.lbl_webDir2LevelMask1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir2LevelMask1.Name = "lbl_webDir2LevelMask1";
      this.lbl_webDir2LevelMask1.Size = new System.Drawing.Size(111, 20);
      this.lbl_webDir2LevelMask1.TabIndex = 17;
      // 
      // lbl_webDir2LevelMaskTit1
      // 
      this.lbl_webDir2LevelMaskTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir2LevelMaskTit1.Location = new System.Drawing.Point(7, 411);
      this.lbl_webDir2LevelMaskTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir2LevelMaskTit1.Name = "lbl_webDir2LevelMaskTit1";
      this.lbl_webDir2LevelMaskTit1.Size = new System.Drawing.Size(173, 20);
      this.lbl_webDir2LevelMaskTit1.TabIndex = 16;
      this.lbl_webDir2LevelMaskTit1.Text = "2-level Web Director Mask:";
      // 
      // grp_shaft1_1
      // 
      this.grp_shaft1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.grp_shaft1_1.Controls.Add(this.lbl_dummySlitMask1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_dummySlitMaskTit1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_sepSlitMask1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_sepSlitMaskTit1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_plungedSlitMask1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_plungedSlitMaskTit1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_9);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_8);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_7);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_6);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_5);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_4);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_3);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_2);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_1);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPos1_1_0);
      this.grp_shaft1_1.Controls.Add(this.lbl_slitPosTit1_1);
      this.grp_shaft1_1.Location = new System.Drawing.Point(0, 235);
      this.grp_shaft1_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.grp_shaft1_1.Name = "grp_shaft1_1";
      this.grp_shaft1_1.Size = new System.Drawing.Size(372, 169);
      this.grp_shaft1_1.TabIndex = 4;
      this.grp_shaft1_1.TabStop = false;
      this.grp_shaft1_1.Text = "Slit Shaft 2";
      // 
      // lbl_dummySlitMask1_1
      // 
      this.lbl_dummySlitMask1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_dummySlitMask1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMask1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_dummySlitMask1_1.Location = new System.Drawing.Point(255, 138);
      this.lbl_dummySlitMask1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMask1_1.Name = "lbl_dummySlitMask1_1";
      this.lbl_dummySlitMask1_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_dummySlitMask1_1.TabIndex = 19;
      // 
      // lbl_dummySlitMaskTit1_1
      // 
      this.lbl_dummySlitMaskTit1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMaskTit1_1.Location = new System.Drawing.Point(7, 138);
      this.lbl_dummySlitMaskTit1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMaskTit1_1.Name = "lbl_dummySlitMaskTit1_1";
      this.lbl_dummySlitMaskTit1_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_dummySlitMaskTit1_1.TabIndex = 18;
      this.lbl_dummySlitMaskTit1_1.Text = "Dummy Slit Mask:";
      // 
      // lbl_sepSlitMask1_1
      // 
      this.lbl_sepSlitMask1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_sepSlitMask1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMask1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_sepSlitMask1_1.Location = new System.Drawing.Point(255, 113);
      this.lbl_sepSlitMask1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMask1_1.Name = "lbl_sepSlitMask1_1";
      this.lbl_sepSlitMask1_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_sepSlitMask1_1.TabIndex = 17;
      // 
      // lbl_sepSlitMaskTit1_1
      // 
      this.lbl_sepSlitMaskTit1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMaskTit1_1.Location = new System.Drawing.Point(7, 113);
      this.lbl_sepSlitMaskTit1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMaskTit1_1.Name = "lbl_sepSlitMaskTit1_1";
      this.lbl_sepSlitMaskTit1_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_sepSlitMaskTit1_1.TabIndex = 16;
      this.lbl_sepSlitMaskTit1_1.Text = "Separation Slit Mask:";
      // 
      // lbl_plungedSlitMask1_1
      // 
      this.lbl_plungedSlitMask1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_plungedSlitMask1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMask1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_plungedSlitMask1_1.Location = new System.Drawing.Point(255, 88);
      this.lbl_plungedSlitMask1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMask1_1.Name = "lbl_plungedSlitMask1_1";
      this.lbl_plungedSlitMask1_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_plungedSlitMask1_1.TabIndex = 15;
      // 
      // lbl_plungedSlitMaskTit1_1
      // 
      this.lbl_plungedSlitMaskTit1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMaskTit1_1.Location = new System.Drawing.Point(7, 88);
      this.lbl_plungedSlitMaskTit1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMaskTit1_1.Name = "lbl_plungedSlitMaskTit1_1";
      this.lbl_plungedSlitMaskTit1_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_plungedSlitMaskTit1_1.TabIndex = 14;
      this.lbl_plungedSlitMaskTit1_1.Text = "Plunged Slit Mask:";
      // 
      // lbl_slitPos1_1_9
      // 
      this.lbl_slitPos1_1_9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_9.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_9.Location = new System.Drawing.Point(318, 48);
      this.lbl_slitPos1_1_9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_9.Name = "lbl_slitPos1_1_9";
      this.lbl_slitPos1_1_9.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_9.TabIndex = 13;
      // 
      // lbl_slitPos1_1_8
      // 
      this.lbl_slitPos1_1_8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_8.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_8.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_8.Location = new System.Drawing.Point(255, 48);
      this.lbl_slitPos1_1_8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_8.Name = "lbl_slitPos1_1_8";
      this.lbl_slitPos1_1_8.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_8.TabIndex = 12;
      // 
      // lbl_slitPos1_1_7
      // 
      this.lbl_slitPos1_1_7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_7.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_7.Location = new System.Drawing.Point(193, 48);
      this.lbl_slitPos1_1_7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_7.Name = "lbl_slitPos1_1_7";
      this.lbl_slitPos1_1_7.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_7.TabIndex = 11;
      // 
      // lbl_slitPos1_1_6
      // 
      this.lbl_slitPos1_1_6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_6.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_6.Location = new System.Drawing.Point(132, 48);
      this.lbl_slitPos1_1_6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_6.Name = "lbl_slitPos1_1_6";
      this.lbl_slitPos1_1_6.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_6.TabIndex = 10;
      // 
      // lbl_slitPos1_1_5
      // 
      this.lbl_slitPos1_1_5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_5.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_5.Location = new System.Drawing.Point(68, 48);
      this.lbl_slitPos1_1_5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_5.Name = "lbl_slitPos1_1_5";
      this.lbl_slitPos1_1_5.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_5.TabIndex = 9;
      // 
      // lbl_slitPos1_1_4
      // 
      this.lbl_slitPos1_1_4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_4.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_4.Location = new System.Drawing.Point(7, 48);
      this.lbl_slitPos1_1_4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_4.Name = "lbl_slitPos1_1_4";
      this.lbl_slitPos1_1_4.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_4.TabIndex = 8;
      // 
      // lbl_slitPos1_1_3
      // 
      this.lbl_slitPos1_1_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_3.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_3.Location = new System.Drawing.Point(318, 20);
      this.lbl_slitPos1_1_3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_3.Name = "lbl_slitPos1_1_3";
      this.lbl_slitPos1_1_3.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_3.TabIndex = 7;
      // 
      // lbl_slitPos1_1_2
      // 
      this.lbl_slitPos1_1_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_2.Location = new System.Drawing.Point(255, 20);
      this.lbl_slitPos1_1_2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_2.Name = "lbl_slitPos1_1_2";
      this.lbl_slitPos1_1_2.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_2.TabIndex = 6;
      // 
      // lbl_slitPos1_1_1
      // 
      this.lbl_slitPos1_1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_1.Location = new System.Drawing.Point(193, 20);
      this.lbl_slitPos1_1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_1.Name = "lbl_slitPos1_1_1";
      this.lbl_slitPos1_1_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_1.TabIndex = 5;
      // 
      // lbl_slitPos1_1_0
      // 
      this.lbl_slitPos1_1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_1_0.Location = new System.Drawing.Point(132, 20);
      this.lbl_slitPos1_1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_1_0.Name = "lbl_slitPos1_1_0";
      this.lbl_slitPos1_1_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_1_0.TabIndex = 4;
      // 
      // lbl_slitPosTit1_1
      // 
      this.lbl_slitPosTit1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPosTit1_1.Location = new System.Drawing.Point(4, 20);
      this.lbl_slitPosTit1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPosTit1_1.Name = "lbl_slitPosTit1_1";
      this.lbl_slitPosTit1_1.Size = new System.Drawing.Size(112, 20);
      this.lbl_slitPosTit1_1.TabIndex = 3;
      this.lbl_slitPosTit1_1.Text = "Slit Positions:";
      // 
      // lbl_sendToPlcTit1
      // 
      this.lbl_sendToPlcTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sendToPlcTit1.Location = new System.Drawing.Point(286, 21);
      this.lbl_sendToPlcTit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sendToPlcTit1.Name = "lbl_sendToPlcTit1";
      this.lbl_sendToPlcTit1.Size = new System.Drawing.Size(84, 20);
      this.lbl_sendToPlcTit1.TabIndex = 2;
      this.lbl_sendToPlcTit1.Text = "Send to PLC";
      // 
      // pnl_sendToPlc1
      // 
      this.pnl_sendToPlc1.BackColor = System.Drawing.Color.LightGray;
      this.pnl_sendToPlc1.Location = new System.Drawing.Point(264, 21);
      this.pnl_sendToPlc1.Name = "pnl_sendToPlc1";
      this.pnl_sendToPlc1.Size = new System.Drawing.Size(18, 16);
      this.pnl_sendToPlc1.TabIndex = 3;
      // 
      // lbl_validTit1
      // 
      this.lbl_validTit1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_validTit1.Location = new System.Drawing.Point(204, 21);
      this.lbl_validTit1.Name = "lbl_validTit1";
      this.lbl_validTit1.Size = new System.Drawing.Size(49, 20);
      this.lbl_validTit1.TabIndex = 0;
      this.lbl_validTit1.Text = "Valid";
      // 
      // pnl_valid1
      // 
      this.pnl_valid1.BackColor = System.Drawing.Color.LightGray;
      this.pnl_valid1.Location = new System.Drawing.Point(183, 21);
      this.pnl_valid1.Name = "pnl_valid1";
      this.pnl_valid1.Size = new System.Drawing.Size(18, 16);
      this.pnl_valid1.TabIndex = 1;
      // 
      // grp_shaft1_0
      // 
      this.grp_shaft1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.grp_shaft1_0.Controls.Add(this.lbl_dummySlitMask1_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_dummySlitMaskTit1_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_sepSlitMask1_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_sepSlitMaskTit1_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_plungedSlitMask1_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_plungedSlitMaskTit1_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_9);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_8);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_7);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_6);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_5);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_4);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_3);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_2);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_1);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPos1_0_0);
      this.grp_shaft1_0.Controls.Add(this.lbl_slitPosTit1_0);
      this.grp_shaft1_0.Location = new System.Drawing.Point(0, 53);
      this.grp_shaft1_0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.grp_shaft1_0.Name = "grp_shaft1_0";
      this.grp_shaft1_0.Size = new System.Drawing.Size(372, 169);
      this.grp_shaft1_0.TabIndex = 0;
      this.grp_shaft1_0.TabStop = false;
      this.grp_shaft1_0.Text = "Slit Shaft 1";
      // 
      // lbl_dummySlitMask1_0
      // 
      this.lbl_dummySlitMask1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_dummySlitMask1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMask1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_dummySlitMask1_0.Location = new System.Drawing.Point(255, 138);
      this.lbl_dummySlitMask1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMask1_0.Name = "lbl_dummySlitMask1_0";
      this.lbl_dummySlitMask1_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_dummySlitMask1_0.TabIndex = 19;
      // 
      // lbl_dummySlitMaskTit1_0
      // 
      this.lbl_dummySlitMaskTit1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMaskTit1_0.Location = new System.Drawing.Point(7, 138);
      this.lbl_dummySlitMaskTit1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMaskTit1_0.Name = "lbl_dummySlitMaskTit1_0";
      this.lbl_dummySlitMaskTit1_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_dummySlitMaskTit1_0.TabIndex = 18;
      this.lbl_dummySlitMaskTit1_0.Text = "Dummy Slit Mask:";
      // 
      // lbl_sepSlitMask1_0
      // 
      this.lbl_sepSlitMask1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_sepSlitMask1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMask1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_sepSlitMask1_0.Location = new System.Drawing.Point(255, 113);
      this.lbl_sepSlitMask1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMask1_0.Name = "lbl_sepSlitMask1_0";
      this.lbl_sepSlitMask1_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_sepSlitMask1_0.TabIndex = 17;
      // 
      // lbl_sepSlitMaskTit1_0
      // 
      this.lbl_sepSlitMaskTit1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMaskTit1_0.Location = new System.Drawing.Point(7, 113);
      this.lbl_sepSlitMaskTit1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMaskTit1_0.Name = "lbl_sepSlitMaskTit1_0";
      this.lbl_sepSlitMaskTit1_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_sepSlitMaskTit1_0.TabIndex = 16;
      this.lbl_sepSlitMaskTit1_0.Text = "Separation Slit Mask:";
      // 
      // lbl_plungedSlitMask1_0
      // 
      this.lbl_plungedSlitMask1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_plungedSlitMask1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMask1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_plungedSlitMask1_0.Location = new System.Drawing.Point(255, 88);
      this.lbl_plungedSlitMask1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMask1_0.Name = "lbl_plungedSlitMask1_0";
      this.lbl_plungedSlitMask1_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_plungedSlitMask1_0.TabIndex = 15;
      // 
      // lbl_plungedSlitMaskTit1_0
      // 
      this.lbl_plungedSlitMaskTit1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMaskTit1_0.Location = new System.Drawing.Point(7, 88);
      this.lbl_plungedSlitMaskTit1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMaskTit1_0.Name = "lbl_plungedSlitMaskTit1_0";
      this.lbl_plungedSlitMaskTit1_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_plungedSlitMaskTit1_0.TabIndex = 14;
      this.lbl_plungedSlitMaskTit1_0.Text = "Plunged Slit Mask:";
      // 
      // lbl_slitPos1_0_9
      // 
      this.lbl_slitPos1_0_9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_9.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_9.Location = new System.Drawing.Point(318, 48);
      this.lbl_slitPos1_0_9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_9.Name = "lbl_slitPos1_0_9";
      this.lbl_slitPos1_0_9.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_9.TabIndex = 13;
      // 
      // lbl_slitPos1_0_8
      // 
      this.lbl_slitPos1_0_8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_8.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_8.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_8.Location = new System.Drawing.Point(255, 48);
      this.lbl_slitPos1_0_8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_8.Name = "lbl_slitPos1_0_8";
      this.lbl_slitPos1_0_8.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_8.TabIndex = 12;
      // 
      // lbl_slitPos1_0_7
      // 
      this.lbl_slitPos1_0_7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_7.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_7.Location = new System.Drawing.Point(193, 48);
      this.lbl_slitPos1_0_7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_7.Name = "lbl_slitPos1_0_7";
      this.lbl_slitPos1_0_7.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_7.TabIndex = 11;
      // 
      // lbl_slitPos1_0_6
      // 
      this.lbl_slitPos1_0_6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_6.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_6.Location = new System.Drawing.Point(132, 48);
      this.lbl_slitPos1_0_6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_6.Name = "lbl_slitPos1_0_6";
      this.lbl_slitPos1_0_6.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_6.TabIndex = 10;
      // 
      // lbl_slitPos1_0_5
      // 
      this.lbl_slitPos1_0_5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_5.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_5.Location = new System.Drawing.Point(68, 48);
      this.lbl_slitPos1_0_5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_5.Name = "lbl_slitPos1_0_5";
      this.lbl_slitPos1_0_5.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_5.TabIndex = 9;
      // 
      // lbl_slitPos1_0_4
      // 
      this.lbl_slitPos1_0_4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_4.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_4.Location = new System.Drawing.Point(7, 48);
      this.lbl_slitPos1_0_4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_4.Name = "lbl_slitPos1_0_4";
      this.lbl_slitPos1_0_4.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_4.TabIndex = 8;
      // 
      // lbl_slitPos1_0_3
      // 
      this.lbl_slitPos1_0_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_3.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_3.Location = new System.Drawing.Point(318, 20);
      this.lbl_slitPos1_0_3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_3.Name = "lbl_slitPos1_0_3";
      this.lbl_slitPos1_0_3.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_3.TabIndex = 7;
      // 
      // lbl_slitPos1_0_2
      // 
      this.lbl_slitPos1_0_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_2.Location = new System.Drawing.Point(255, 20);
      this.lbl_slitPos1_0_2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_2.Name = "lbl_slitPos1_0_2";
      this.lbl_slitPos1_0_2.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_2.TabIndex = 6;
      // 
      // lbl_slitPos1_0_1
      // 
      this.lbl_slitPos1_0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_1.Location = new System.Drawing.Point(193, 20);
      this.lbl_slitPos1_0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_1.Name = "lbl_slitPos1_0_1";
      this.lbl_slitPos1_0_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_1.TabIndex = 5;
      // 
      // lbl_slitPos1_0_0
      // 
      this.lbl_slitPos1_0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos1_0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos1_0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos1_0_0.Location = new System.Drawing.Point(132, 20);
      this.lbl_slitPos1_0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos1_0_0.Name = "lbl_slitPos1_0_0";
      this.lbl_slitPos1_0_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos1_0_0.TabIndex = 4;
      // 
      // lbl_slitPosTit1_0
      // 
      this.lbl_slitPosTit1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPosTit1_0.Location = new System.Drawing.Point(4, 20);
      this.lbl_slitPosTit1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPosTit1_0.Name = "lbl_slitPosTit1_0";
      this.lbl_slitPosTit1_0.Size = new System.Drawing.Size(112, 20);
      this.lbl_slitPosTit1_0.TabIndex = 3;
      this.lbl_slitPosTit1_0.Text = "Slit Positions:";
      // 
      // grp_setup2
      // 
      this.grp_setup2.Controls.Add(this.lbl_actPosTime2);
      this.grp_setup2.Controls.Add(this.label7);
      this.grp_setup2.Controls.Add(this.lbl_posTime2_1);
      this.grp_setup2.Controls.Add(this.lbl_posTime2_0);
      this.grp_setup2.Controls.Add(this.label5);
      this.grp_setup2.Controls.Add(this.lbl_shaftMask2);
      this.grp_setup2.Controls.Add(this.lbl_shaftMaskTit2);
      this.grp_setup2.Controls.Add(this.lbl_modId2);
      this.grp_setup2.Controls.Add(this.lbl_ConfigVersion2);
      this.grp_setup2.Controls.Add(this.lbl_setupId2);
      this.grp_setup2.Controls.Add(this.lbl_knifeOnwardsTit2);
      this.grp_setup2.Controls.Add(this.pnl_knifeOnwards2);
      this.grp_setup2.Controls.Add(this.lbl_cleanSlitsTit2);
      this.grp_setup2.Controls.Add(this.pnl_cleanSlits2);
      this.grp_setup2.Controls.Add(this.lbl_rcsMask2);
      this.grp_setup2.Controls.Add(this.lbl_rcsMaskTit2);
      this.grp_setup2.Controls.Add(this.lbl_webDir3LevelMask2);
      this.grp_setup2.Controls.Add(this.lbl_webDir3LevelMaskTit2);
      this.grp_setup2.Controls.Add(this.lbl_webDir2LevelMask2);
      this.grp_setup2.Controls.Add(this.lbl_webDir2LevelMaskTit2);
      this.grp_setup2.Controls.Add(this.grp_shaft2_1);
      this.grp_setup2.Controls.Add(this.lbl_sendToPlcTit2);
      this.grp_setup2.Controls.Add(this.pnl_sendToPlc2);
      this.grp_setup2.Controls.Add(this.lbl_validTit2);
      this.grp_setup2.Controls.Add(this.pnl_valid2);
      this.grp_setup2.Controls.Add(this.grp_shaft2_0);
      this.grp_setup2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_setup2.Location = new System.Drawing.Point(854, 168);
      this.grp_setup2.Name = "grp_setup2";
      this.grp_setup2.Size = new System.Drawing.Size(372, 570);
      this.grp_setup2.TabIndex = 112;
      this.grp_setup2.TabStop = false;
      this.grp_setup2.Text = "Setup 3";
      // 
      // lbl_posTime2_1
      // 
      this.lbl_posTime2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_posTime2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_posTime2_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_posTime2_1.Location = new System.Drawing.Point(316, 498);
      this.lbl_posTime2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_posTime2_1.Name = "lbl_posTime2_1";
      this.lbl_posTime2_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_posTime2_1.TabIndex = 43;
      // 
      // lbl_posTime2_0
      // 
      this.lbl_posTime2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_posTime2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_posTime2_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_posTime2_0.Location = new System.Drawing.Point(234, 498);
      this.lbl_posTime2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_posTime2_0.Name = "lbl_posTime2_0";
      this.lbl_posTime2_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_posTime2_0.TabIndex = 42;
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(10, 498);
      this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(190, 20);
      this.label5.TabIndex = 41;
      this.label5.Text = "Extim pos times (Slow - Fast):";
      // 
      // lbl_shaftMask2
      // 
      this.lbl_shaftMask2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_shaftMask2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_shaftMask2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_shaftMask2.Location = new System.Drawing.Point(257, 477);
      this.lbl_shaftMask2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_shaftMask2.Name = "lbl_shaftMask2";
      this.lbl_shaftMask2.Size = new System.Drawing.Size(111, 20);
      this.lbl_shaftMask2.TabIndex = 38;
      // 
      // lbl_shaftMaskTit2
      // 
      this.lbl_shaftMaskTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_shaftMaskTit2.Location = new System.Drawing.Point(9, 477);
      this.lbl_shaftMaskTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_shaftMaskTit2.Name = "lbl_shaftMaskTit2";
      this.lbl_shaftMaskTit2.Size = new System.Drawing.Size(173, 20);
      this.lbl_shaftMaskTit2.TabIndex = 37;
      this.lbl_shaftMaskTit2.Text = "Shaft Mask:";
      // 
      // lbl_modId2
      // 
      this.lbl_modId2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_modId2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_modId2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_modId2.Location = new System.Drawing.Point(68, 21);
      this.lbl_modId2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_modId2.Name = "lbl_modId2";
      this.lbl_modId2.Size = new System.Drawing.Size(26, 20);
      this.lbl_modId2.TabIndex = 36;
      // 
      // lbl_ConfigVersion2
      // 
      this.lbl_ConfigVersion2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ConfigVersion2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ConfigVersion2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_ConfigVersion2.Location = new System.Drawing.Point(118, 21);
      this.lbl_ConfigVersion2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_ConfigVersion2.Name = "lbl_ConfigVersion2";
      this.lbl_ConfigVersion2.Size = new System.Drawing.Size(48, 20);
      this.lbl_ConfigVersion2.TabIndex = 35;
      // 
      // lbl_setupId2
      // 
      this.lbl_setupId2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_setupId2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_setupId2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_setupId2.Location = new System.Drawing.Point(10, 21);
      this.lbl_setupId2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_setupId2.Name = "lbl_setupId2";
      this.lbl_setupId2.Size = new System.Drawing.Size(48, 20);
      this.lbl_setupId2.TabIndex = 34;
      // 
      // lbl_knifeOnwardsTit2
      // 
      this.lbl_knifeOnwardsTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_knifeOnwardsTit2.Location = new System.Drawing.Point(237, 546);
      this.lbl_knifeOnwardsTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_knifeOnwardsTit2.Name = "lbl_knifeOnwardsTit2";
      this.lbl_knifeOnwardsTit2.Size = new System.Drawing.Size(127, 20);
      this.lbl_knifeOnwardsTit2.TabIndex = 32;
      this.lbl_knifeOnwardsTit2.Text = "First of Knife Onwds";
      // 
      // pnl_knifeOnwards2
      // 
      this.pnl_knifeOnwards2.BackColor = System.Drawing.Color.LightGray;
      this.pnl_knifeOnwards2.Location = new System.Drawing.Point(212, 546);
      this.pnl_knifeOnwards2.Name = "pnl_knifeOnwards2";
      this.pnl_knifeOnwards2.Size = new System.Drawing.Size(18, 16);
      this.pnl_knifeOnwards2.TabIndex = 33;
      // 
      // lbl_cleanSlitsTit2
      // 
      this.lbl_cleanSlitsTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_cleanSlitsTit2.Location = new System.Drawing.Point(34, 546);
      this.lbl_cleanSlitsTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_cleanSlitsTit2.Name = "lbl_cleanSlitsTit2";
      this.lbl_cleanSlitsTit2.Size = new System.Drawing.Size(120, 20);
      this.lbl_cleanSlitsTit2.TabIndex = 26;
      this.lbl_cleanSlitsTit2.Text = "Clean Slits";
      // 
      // pnl_cleanSlits2
      // 
      this.pnl_cleanSlits2.BackColor = System.Drawing.Color.LightGray;
      this.pnl_cleanSlits2.Location = new System.Drawing.Point(9, 546);
      this.pnl_cleanSlits2.Name = "pnl_cleanSlits2";
      this.pnl_cleanSlits2.Size = new System.Drawing.Size(18, 16);
      this.pnl_cleanSlits2.TabIndex = 27;
      // 
      // lbl_rcsMask2
      // 
      this.lbl_rcsMask2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_rcsMask2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_rcsMask2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_rcsMask2.Location = new System.Drawing.Point(255, 455);
      this.lbl_rcsMask2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_rcsMask2.Name = "lbl_rcsMask2";
      this.lbl_rcsMask2.Size = new System.Drawing.Size(111, 20);
      this.lbl_rcsMask2.TabIndex = 21;
      // 
      // lbl_rcsMaskTit2
      // 
      this.lbl_rcsMaskTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_rcsMaskTit2.Location = new System.Drawing.Point(7, 455);
      this.lbl_rcsMaskTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_rcsMaskTit2.Name = "lbl_rcsMaskTit2";
      this.lbl_rcsMaskTit2.Size = new System.Drawing.Size(173, 20);
      this.lbl_rcsMaskTit2.TabIndex = 20;
      this.lbl_rcsMaskTit2.Text = "Rotary CS Mask:";
      // 
      // lbl_webDir3LevelMask2
      // 
      this.lbl_webDir3LevelMask2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_webDir3LevelMask2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir3LevelMask2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_webDir3LevelMask2.Location = new System.Drawing.Point(255, 433);
      this.lbl_webDir3LevelMask2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir3LevelMask2.Name = "lbl_webDir3LevelMask2";
      this.lbl_webDir3LevelMask2.Size = new System.Drawing.Size(111, 20);
      this.lbl_webDir3LevelMask2.TabIndex = 19;
      // 
      // lbl_webDir3LevelMaskTit2
      // 
      this.lbl_webDir3LevelMaskTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir3LevelMaskTit2.Location = new System.Drawing.Point(7, 433);
      this.lbl_webDir3LevelMaskTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir3LevelMaskTit2.Name = "lbl_webDir3LevelMaskTit2";
      this.lbl_webDir3LevelMaskTit2.Size = new System.Drawing.Size(173, 20);
      this.lbl_webDir3LevelMaskTit2.TabIndex = 18;
      this.lbl_webDir3LevelMaskTit2.Text = "3-level Web Director Mask:";
      // 
      // lbl_webDir2LevelMask2
      // 
      this.lbl_webDir2LevelMask2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_webDir2LevelMask2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir2LevelMask2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_webDir2LevelMask2.Location = new System.Drawing.Point(255, 411);
      this.lbl_webDir2LevelMask2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir2LevelMask2.Name = "lbl_webDir2LevelMask2";
      this.lbl_webDir2LevelMask2.Size = new System.Drawing.Size(111, 20);
      this.lbl_webDir2LevelMask2.TabIndex = 17;
      // 
      // lbl_webDir2LevelMaskTit2
      // 
      this.lbl_webDir2LevelMaskTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_webDir2LevelMaskTit2.Location = new System.Drawing.Point(7, 411);
      this.lbl_webDir2LevelMaskTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_webDir2LevelMaskTit2.Name = "lbl_webDir2LevelMaskTit2";
      this.lbl_webDir2LevelMaskTit2.Size = new System.Drawing.Size(173, 20);
      this.lbl_webDir2LevelMaskTit2.TabIndex = 16;
      this.lbl_webDir2LevelMaskTit2.Text = "2-level Web Director Mask:";
      // 
      // grp_shaft2_1
      // 
      this.grp_shaft2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.grp_shaft2_1.Controls.Add(this.lbl_dummySlitMask2_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_dummySlitMaskTit2_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_sepSlitMask2_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_sepSlitMaskTit2_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_plungedSlitMask2_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_plungedSlitMaskTit2_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_9);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_8);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_7);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_6);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_5);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_4);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_3);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_2);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_1);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPos2_1_0);
      this.grp_shaft2_1.Controls.Add(this.lbl_slitPosTit2_1);
      this.grp_shaft2_1.Location = new System.Drawing.Point(0, 235);
      this.grp_shaft2_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.grp_shaft2_1.Name = "grp_shaft2_1";
      this.grp_shaft2_1.Size = new System.Drawing.Size(372, 169);
      this.grp_shaft2_1.TabIndex = 4;
      this.grp_shaft2_1.TabStop = false;
      this.grp_shaft2_1.Text = "Slit Shaft 2";
      // 
      // lbl_dummySlitMask2_1
      // 
      this.lbl_dummySlitMask2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_dummySlitMask2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMask2_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_dummySlitMask2_1.Location = new System.Drawing.Point(255, 138);
      this.lbl_dummySlitMask2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMask2_1.Name = "lbl_dummySlitMask2_1";
      this.lbl_dummySlitMask2_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_dummySlitMask2_1.TabIndex = 19;
      // 
      // lbl_dummySlitMaskTit2_1
      // 
      this.lbl_dummySlitMaskTit2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMaskTit2_1.Location = new System.Drawing.Point(7, 138);
      this.lbl_dummySlitMaskTit2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMaskTit2_1.Name = "lbl_dummySlitMaskTit2_1";
      this.lbl_dummySlitMaskTit2_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_dummySlitMaskTit2_1.TabIndex = 18;
      this.lbl_dummySlitMaskTit2_1.Text = "Dummy Slit Mask:";
      // 
      // lbl_sepSlitMask2_1
      // 
      this.lbl_sepSlitMask2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_sepSlitMask2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMask2_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_sepSlitMask2_1.Location = new System.Drawing.Point(255, 113);
      this.lbl_sepSlitMask2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMask2_1.Name = "lbl_sepSlitMask2_1";
      this.lbl_sepSlitMask2_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_sepSlitMask2_1.TabIndex = 17;
      // 
      // lbl_sepSlitMaskTit2_1
      // 
      this.lbl_sepSlitMaskTit2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMaskTit2_1.Location = new System.Drawing.Point(7, 113);
      this.lbl_sepSlitMaskTit2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMaskTit2_1.Name = "lbl_sepSlitMaskTit2_1";
      this.lbl_sepSlitMaskTit2_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_sepSlitMaskTit2_1.TabIndex = 16;
      this.lbl_sepSlitMaskTit2_1.Text = "Separation Slit Mask:";
      // 
      // lbl_plungedSlitMask2_1
      // 
      this.lbl_plungedSlitMask2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_plungedSlitMask2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMask2_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_plungedSlitMask2_1.Location = new System.Drawing.Point(255, 88);
      this.lbl_plungedSlitMask2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMask2_1.Name = "lbl_plungedSlitMask2_1";
      this.lbl_plungedSlitMask2_1.Size = new System.Drawing.Size(111, 20);
      this.lbl_plungedSlitMask2_1.TabIndex = 15;
      // 
      // lbl_plungedSlitMaskTit2_1
      // 
      this.lbl_plungedSlitMaskTit2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMaskTit2_1.Location = new System.Drawing.Point(7, 88);
      this.lbl_plungedSlitMaskTit2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMaskTit2_1.Name = "lbl_plungedSlitMaskTit2_1";
      this.lbl_plungedSlitMaskTit2_1.Size = new System.Drawing.Size(158, 20);
      this.lbl_plungedSlitMaskTit2_1.TabIndex = 14;
      this.lbl_plungedSlitMaskTit2_1.Text = "Plunged Slit Mask:";
      // 
      // lbl_slitPos2_1_9
      // 
      this.lbl_slitPos2_1_9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_9.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_9.Location = new System.Drawing.Point(318, 48);
      this.lbl_slitPos2_1_9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_9.Name = "lbl_slitPos2_1_9";
      this.lbl_slitPos2_1_9.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_9.TabIndex = 13;
      // 
      // lbl_slitPos2_1_8
      // 
      this.lbl_slitPos2_1_8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_8.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_8.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_8.Location = new System.Drawing.Point(255, 48);
      this.lbl_slitPos2_1_8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_8.Name = "lbl_slitPos2_1_8";
      this.lbl_slitPos2_1_8.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_8.TabIndex = 12;
      // 
      // lbl_slitPos2_1_7
      // 
      this.lbl_slitPos2_1_7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_7.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_7.Location = new System.Drawing.Point(193, 48);
      this.lbl_slitPos2_1_7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_7.Name = "lbl_slitPos2_1_7";
      this.lbl_slitPos2_1_7.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_7.TabIndex = 11;
      // 
      // lbl_slitPos2_1_6
      // 
      this.lbl_slitPos2_1_6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_6.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_6.Location = new System.Drawing.Point(132, 48);
      this.lbl_slitPos2_1_6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_6.Name = "lbl_slitPos2_1_6";
      this.lbl_slitPos2_1_6.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_6.TabIndex = 10;
      // 
      // lbl_slitPos2_1_5
      // 
      this.lbl_slitPos2_1_5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_5.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_5.Location = new System.Drawing.Point(68, 48);
      this.lbl_slitPos2_1_5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_5.Name = "lbl_slitPos2_1_5";
      this.lbl_slitPos2_1_5.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_5.TabIndex = 9;
      // 
      // lbl_slitPos2_1_4
      // 
      this.lbl_slitPos2_1_4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_4.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_4.Location = new System.Drawing.Point(7, 48);
      this.lbl_slitPos2_1_4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_4.Name = "lbl_slitPos2_1_4";
      this.lbl_slitPos2_1_4.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_4.TabIndex = 8;
      // 
      // lbl_slitPos2_1_3
      // 
      this.lbl_slitPos2_1_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_3.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_3.Location = new System.Drawing.Point(318, 20);
      this.lbl_slitPos2_1_3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_3.Name = "lbl_slitPos2_1_3";
      this.lbl_slitPos2_1_3.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_3.TabIndex = 7;
      // 
      // lbl_slitPos2_1_2
      // 
      this.lbl_slitPos2_1_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_2.Location = new System.Drawing.Point(255, 20);
      this.lbl_slitPos2_1_2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_2.Name = "lbl_slitPos2_1_2";
      this.lbl_slitPos2_1_2.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_2.TabIndex = 6;
      // 
      // lbl_slitPos2_1_1
      // 
      this.lbl_slitPos2_1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_1.Location = new System.Drawing.Point(193, 20);
      this.lbl_slitPos2_1_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_1.Name = "lbl_slitPos2_1_1";
      this.lbl_slitPos2_1_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_1.TabIndex = 5;
      // 
      // lbl_slitPos2_1_0
      // 
      this.lbl_slitPos2_1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_1_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_1_0.Location = new System.Drawing.Point(132, 20);
      this.lbl_slitPos2_1_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_1_0.Name = "lbl_slitPos2_1_0";
      this.lbl_slitPos2_1_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_1_0.TabIndex = 4;
      // 
      // lbl_slitPosTit2_1
      // 
      this.lbl_slitPosTit2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPosTit2_1.Location = new System.Drawing.Point(4, 20);
      this.lbl_slitPosTit2_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPosTit2_1.Name = "lbl_slitPosTit2_1";
      this.lbl_slitPosTit2_1.Size = new System.Drawing.Size(112, 20);
      this.lbl_slitPosTit2_1.TabIndex = 3;
      this.lbl_slitPosTit2_1.Text = "Slit Positions:";
      // 
      // lbl_sendToPlcTit2
      // 
      this.lbl_sendToPlcTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sendToPlcTit2.Location = new System.Drawing.Point(286, 21);
      this.lbl_sendToPlcTit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sendToPlcTit2.Name = "lbl_sendToPlcTit2";
      this.lbl_sendToPlcTit2.Size = new System.Drawing.Size(81, 20);
      this.lbl_sendToPlcTit2.TabIndex = 2;
      this.lbl_sendToPlcTit2.Text = "Send to PLC";
      // 
      // pnl_sendToPlc2
      // 
      this.pnl_sendToPlc2.BackColor = System.Drawing.Color.LightGray;
      this.pnl_sendToPlc2.Location = new System.Drawing.Point(264, 21);
      this.pnl_sendToPlc2.Name = "pnl_sendToPlc2";
      this.pnl_sendToPlc2.Size = new System.Drawing.Size(18, 16);
      this.pnl_sendToPlc2.TabIndex = 3;
      // 
      // lbl_validTit2
      // 
      this.lbl_validTit2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_validTit2.Location = new System.Drawing.Point(204, 21);
      this.lbl_validTit2.Name = "lbl_validTit2";
      this.lbl_validTit2.Size = new System.Drawing.Size(49, 20);
      this.lbl_validTit2.TabIndex = 0;
      this.lbl_validTit2.Text = "Valid";
      // 
      // pnl_valid2
      // 
      this.pnl_valid2.BackColor = System.Drawing.Color.LightGray;
      this.pnl_valid2.Location = new System.Drawing.Point(183, 21);
      this.pnl_valid2.Name = "pnl_valid2";
      this.pnl_valid2.Size = new System.Drawing.Size(18, 16);
      this.pnl_valid2.TabIndex = 1;
      // 
      // grp_shaft2_0
      // 
      this.grp_shaft2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.grp_shaft2_0.Controls.Add(this.lbl_dummySlitMask2_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_dummySlitMaskTit2_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_sepSlitMask2_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_sepSlitMaskTit2_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_plungedSlitMask2_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_plungedSlitMaskTit2_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_9);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_8);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_7);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_6);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_5);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_4);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_3);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_2);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_1);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPos2_0_0);
      this.grp_shaft2_0.Controls.Add(this.lbl_slitPosTit2_0);
      this.grp_shaft2_0.Location = new System.Drawing.Point(0, 53);
      this.grp_shaft2_0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.grp_shaft2_0.Name = "grp_shaft2_0";
      this.grp_shaft2_0.Size = new System.Drawing.Size(372, 169);
      this.grp_shaft2_0.TabIndex = 0;
      this.grp_shaft2_0.TabStop = false;
      this.grp_shaft2_0.Text = "Slit Shaft 1";
      // 
      // lbl_dummySlitMask2_0
      // 
      this.lbl_dummySlitMask2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_dummySlitMask2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMask2_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_dummySlitMask2_0.Location = new System.Drawing.Point(255, 138);
      this.lbl_dummySlitMask2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMask2_0.Name = "lbl_dummySlitMask2_0";
      this.lbl_dummySlitMask2_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_dummySlitMask2_0.TabIndex = 19;
      // 
      // lbl_dummySlitMaskTit2_0
      // 
      this.lbl_dummySlitMaskTit2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_dummySlitMaskTit2_0.Location = new System.Drawing.Point(7, 138);
      this.lbl_dummySlitMaskTit2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_dummySlitMaskTit2_0.Name = "lbl_dummySlitMaskTit2_0";
      this.lbl_dummySlitMaskTit2_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_dummySlitMaskTit2_0.TabIndex = 18;
      this.lbl_dummySlitMaskTit2_0.Text = "Dummy Slit Mask:";
      // 
      // lbl_sepSlitMask2_0
      // 
      this.lbl_sepSlitMask2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_sepSlitMask2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMask2_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_sepSlitMask2_0.Location = new System.Drawing.Point(255, 113);
      this.lbl_sepSlitMask2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMask2_0.Name = "lbl_sepSlitMask2_0";
      this.lbl_sepSlitMask2_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_sepSlitMask2_0.TabIndex = 17;
      // 
      // lbl_sepSlitMaskTit2_0
      // 
      this.lbl_sepSlitMaskTit2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_sepSlitMaskTit2_0.Location = new System.Drawing.Point(7, 113);
      this.lbl_sepSlitMaskTit2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_sepSlitMaskTit2_0.Name = "lbl_sepSlitMaskTit2_0";
      this.lbl_sepSlitMaskTit2_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_sepSlitMaskTit2_0.TabIndex = 16;
      this.lbl_sepSlitMaskTit2_0.Text = "Separation Slit Mask:";
      // 
      // lbl_plungedSlitMask2_0
      // 
      this.lbl_plungedSlitMask2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_plungedSlitMask2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMask2_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_plungedSlitMask2_0.Location = new System.Drawing.Point(255, 88);
      this.lbl_plungedSlitMask2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMask2_0.Name = "lbl_plungedSlitMask2_0";
      this.lbl_plungedSlitMask2_0.Size = new System.Drawing.Size(111, 20);
      this.lbl_plungedSlitMask2_0.TabIndex = 15;
      // 
      // lbl_plungedSlitMaskTit2_0
      // 
      this.lbl_plungedSlitMaskTit2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_plungedSlitMaskTit2_0.Location = new System.Drawing.Point(7, 88);
      this.lbl_plungedSlitMaskTit2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_plungedSlitMaskTit2_0.Name = "lbl_plungedSlitMaskTit2_0";
      this.lbl_plungedSlitMaskTit2_0.Size = new System.Drawing.Size(158, 20);
      this.lbl_plungedSlitMaskTit2_0.TabIndex = 14;
      this.lbl_plungedSlitMaskTit2_0.Text = "Plunged Slit Mask:";
      // 
      // lbl_slitPos2_0_9
      // 
      this.lbl_slitPos2_0_9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_9.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_9.Location = new System.Drawing.Point(318, 48);
      this.lbl_slitPos2_0_9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_9.Name = "lbl_slitPos2_0_9";
      this.lbl_slitPos2_0_9.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_9.TabIndex = 13;
      // 
      // lbl_slitPos2_0_8
      // 
      this.lbl_slitPos2_0_8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_8.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_8.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_8.Location = new System.Drawing.Point(255, 48);
      this.lbl_slitPos2_0_8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_8.Name = "lbl_slitPos2_0_8";
      this.lbl_slitPos2_0_8.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_8.TabIndex = 12;
      // 
      // lbl_slitPos2_0_7
      // 
      this.lbl_slitPos2_0_7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_7.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_7.Location = new System.Drawing.Point(193, 48);
      this.lbl_slitPos2_0_7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_7.Name = "lbl_slitPos2_0_7";
      this.lbl_slitPos2_0_7.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_7.TabIndex = 11;
      // 
      // lbl_slitPos2_0_6
      // 
      this.lbl_slitPos2_0_6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_6.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_6.Location = new System.Drawing.Point(132, 48);
      this.lbl_slitPos2_0_6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_6.Name = "lbl_slitPos2_0_6";
      this.lbl_slitPos2_0_6.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_6.TabIndex = 10;
      // 
      // lbl_slitPos2_0_5
      // 
      this.lbl_slitPos2_0_5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_5.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_5.Location = new System.Drawing.Point(68, 48);
      this.lbl_slitPos2_0_5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_5.Name = "lbl_slitPos2_0_5";
      this.lbl_slitPos2_0_5.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_5.TabIndex = 9;
      // 
      // lbl_slitPos2_0_4
      // 
      this.lbl_slitPos2_0_4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_4.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_4.Location = new System.Drawing.Point(7, 48);
      this.lbl_slitPos2_0_4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_4.Name = "lbl_slitPos2_0_4";
      this.lbl_slitPos2_0_4.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_4.TabIndex = 8;
      // 
      // lbl_slitPos2_0_3
      // 
      this.lbl_slitPos2_0_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_3.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_3.Location = new System.Drawing.Point(318, 20);
      this.lbl_slitPos2_0_3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_3.Name = "lbl_slitPos2_0_3";
      this.lbl_slitPos2_0_3.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_3.TabIndex = 7;
      // 
      // lbl_slitPos2_0_2
      // 
      this.lbl_slitPos2_0_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_2.Location = new System.Drawing.Point(255, 20);
      this.lbl_slitPos2_0_2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_2.Name = "lbl_slitPos2_0_2";
      this.lbl_slitPos2_0_2.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_2.TabIndex = 6;
      // 
      // lbl_slitPos2_0_1
      // 
      this.lbl_slitPos2_0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_1.Location = new System.Drawing.Point(193, 20);
      this.lbl_slitPos2_0_1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_1.Name = "lbl_slitPos2_0_1";
      this.lbl_slitPos2_0_1.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_1.TabIndex = 5;
      // 
      // lbl_slitPos2_0_0
      // 
      this.lbl_slitPos2_0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_slitPos2_0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPos2_0_0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_slitPos2_0_0.Location = new System.Drawing.Point(132, 20);
      this.lbl_slitPos2_0_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPos2_0_0.Name = "lbl_slitPos2_0_0";
      this.lbl_slitPos2_0_0.Size = new System.Drawing.Size(48, 20);
      this.lbl_slitPos2_0_0.TabIndex = 4;
      // 
      // lbl_slitPosTit2_0
      // 
      this.lbl_slitPosTit2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_slitPosTit2_0.Location = new System.Drawing.Point(4, 20);
      this.lbl_slitPosTit2_0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_slitPosTit2_0.Name = "lbl_slitPosTit2_0";
      this.lbl_slitPosTit2_0.Size = new System.Drawing.Size(112, 20);
      this.lbl_slitPosTit2_0.TabIndex = 3;
      this.lbl_slitPosTit2_0.Text = "Slit Positions:";
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(7, 521);
      this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 20);
      this.label2.TabIndex = 38;
      this.label2.Text = "Act pos time:";
      // 
      // lbl_actPosTime0
      // 
      this.lbl_actPosTime0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_actPosTime0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_actPosTime0.ForeColor = System.Drawing.Color.Blue;
      this.lbl_actPosTime0.Location = new System.Drawing.Point(316, 521);
      this.lbl_actPosTime0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_actPosTime0.Name = "lbl_actPosTime0";
      this.lbl_actPosTime0.Size = new System.Drawing.Size(48, 20);
      this.lbl_actPosTime0.TabIndex = 39;
      // 
      // lbl_actPosTime1
      // 
      this.lbl_actPosTime1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_actPosTime1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_actPosTime1.ForeColor = System.Drawing.Color.Blue;
      this.lbl_actPosTime1.Location = new System.Drawing.Point(318, 521);
      this.lbl_actPosTime1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_actPosTime1.Name = "lbl_actPosTime1";
      this.lbl_actPosTime1.Size = new System.Drawing.Size(48, 20);
      this.lbl_actPosTime1.TabIndex = 42;
      // 
      // label6
      // 
      this.label6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(9, 521);
      this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(107, 20);
      this.label6.TabIndex = 41;
      this.label6.Text = "Act pos time:";
      // 
      // lbl_actPosTime2
      // 
      this.lbl_actPosTime2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_actPosTime2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_actPosTime2.ForeColor = System.Drawing.Color.Blue;
      this.lbl_actPosTime2.Location = new System.Drawing.Point(318, 521);
      this.lbl_actPosTime2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lbl_actPosTime2.Name = "lbl_actPosTime2";
      this.lbl_actPosTime2.Size = new System.Drawing.Size(48, 20);
      this.lbl_actPosTime2.TabIndex = 45;
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(9, 521);
      this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(107, 20);
      this.label7.TabIndex = 44;
      this.label7.Text = "Act pos time:";
      // 
      // Frm_DebugTwin400SetupSituation
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.grp_setup2);
      this.Controls.Add(this.grp_setup1);
      this.Controls.Add(this.grp_setup0);
      this.Name = "Frm_DebugTwin400SetupSituation";
      this.Text = "Frm_DebugTwin400SetupSituation";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lab_path, 0);
      this.Controls.SetChildIndex(this.lab_Time, 0);
      this.Controls.SetChildIndex(this.grp_setup0, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
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
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.grp_setup1, 0);
      this.Controls.SetChildIndex(this.grp_setup2, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_setup0.ResumeLayout(false);
      this.grp_shaft0_1.ResumeLayout(false);
      this.grp_shaft0_0.ResumeLayout(false);
      this.grp_setup1.ResumeLayout(false);
      this.grp_shaft1_1.ResumeLayout(false);
      this.grp_shaft1_0.ResumeLayout(false);
      this.grp_setup2.ResumeLayout(false);
      this.grp_shaft2_1.ResumeLayout(false);
      this.grp_shaft2_0.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void _RefreshSetupSituation(string sData)
    {
      int cnt = 0;
      //CM7 19-09-2005
      //agrassi_FI19-01-2011_1
      //int dataLen = SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS * (13 + SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * (3 + SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT)) + 1;
      int dataLen = SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS * (14 + SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * (3 + SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT)) + 1;
      string[] data = sData.Split(Command.PARAMETER_SEPARATOR);
      if (data.Length != dataLen)
        return;
			for (int stp = 0; stp < SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS; stp++)
      {
				for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
        {
					for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
            this.slitPos[stp][sh][slt].Text = data[cnt++];
          this.plungedSlitMask[stp][sh].Text = data[cnt++];
          this.sepSlitMask[stp][sh].Text = data[cnt++];
          this.dummySlitMask[stp][sh].Text = data[cnt++];
        }
        this.webDir2LevelMask[stp].Text = data[cnt++];
        this.webDir3LevelMask[stp].Text = data[cnt++];
        this.rcsMask[stp].Text = data[cnt++];
        //CM7 19-09-2005
        this.shaftMask[stp].Text = data[cnt++];
        //agrassi_FI19-01-2011_1
        //this.wideToNarrow[stp].BackColor = bool.Parse(data[cnt++])?Color.Green:Color.LightGray;
        //this.narrowToWide[stp].BackColor = bool.Parse(data[cnt++]) ? Color.Green : Color.LightGray;
        for (int i=0; i<2; i++)
          this.posTimes[stp][i].Text = data[cnt++];
        this.actPosTime[stp].Text = data[cnt++];
        this.cleanSlits[stp].BackColor = bool.Parse(data[cnt++]) ? Color.Green : Color.LightGray;
        this.sendToPlc[stp].BackColor = bool.Parse(data[cnt++]) ? Color.Green : Color.LightGray;
        this.knifeOnwards[stp].BackColor = bool.Parse(data[cnt++]) ? Color.Green : Color.LightGray;
        this.setupId[stp].Text = data[cnt++];
        //CM7 19-09-2005
        this.modId[stp].Text = data[cnt++];
        this.configVersion[stp].Text = data[cnt++];
        this.valid[stp].BackColor = bool.Parse(data[cnt++]) ? Color.Green : Color.LightGray;
      }
    }
  }
}

