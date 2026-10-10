using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.Controls.Tools;
using SyncroC.DataBase.LineConfig;

namespace SyncroC.Forms.Pages.Twin400.Tools
{
	public class Frm_twin400_rotary : SyncroC.Forms.Pages.Twin400.Tools.Frm_twin400_toolsSubgroup
  {
		private int[] sectorMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS - 1];
		private bool[] maskValid = new bool[SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS - 1];
    private System.ComponentModel.IContainer components = null;

    public Frm_twin400_rotary()
		{
			InitializeComponent();
      //agrassi_FI12-06-08_2
      this.toolDetail.Location = new System.Drawing.Point(114, 225);
      this.toolDetail.Size = new System.Drawing.Size(1106, 506);
      this.toolDetail.TranslateText = true;
      string notValidSectorsMask = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_160");
      string nextSetupMissing = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_161");
      this.toolDetail.Config(DBManager.getDBManager().lineConfig, MachineToolsType.Rotary, false, false, null, null,
                                notValidSectorsMask, nextSetupMissing);
      DBManager.getDBManager().order.OnDBOrderChange += new Order.DBChangedHandler(this.SetupListChange);
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

		protected override void F11()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_rotary));
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
      //this.toolDetail.TranslateText = true;
			// 
			// tab_TabB
			// 
			this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
			// Frm_twin400_rotary
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Name = "Frm_twin400_rotary";
			this.Tag = "";
			this.Text = "Frm_twin400_rotary";
			this.SizeChanged += new System.EventHandler(this.Frm_twin400_rotary_SizeChanged);
			this.Activated += new System.EventHandler(this.Frm_twin400_rotary_Activated);
			this.Load += new System.EventHandler(this.Frm_twin400_rotary_Load);
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

    }
		#endregion

    protected override bool ShowTools(int pos)
    {
      DataRow nextSetup = null;
      if (!base.ShowTools(pos))
        return false;
			this.visibleSetupCount = SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS - 1;
      if (pos >= this.visibleSetupCount)
        return false;
      bool valid = (pos + 1 < DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count);
      if (valid)
      {
        nextSetup = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows[pos + 1];
        valid = bool.Parse(setup[Order.DEL_VALID].ToString()) && bool.Parse(nextSetup[Order.DEL_VALID].ToString());
      }
      if (valid)
      {
        //agrassiFI27-08-2010_1
        int[] prevPlungedSlitMask, plungedSlitMask;
        int[] prevSepSlitMask, nextSepSlitMask;
        LevelName[] prevLevels, nextLevels;
        int[][] prevSlitPositions, nextSlitPositions;
				prevSepSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
				nextSepSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
        //agrassiFI27-08-2010_1
        prevPlungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
        plungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
        prevLevels = new LevelName[3];//TODO: mettere la costante giusta
        nextLevels = new LevelName[3];
				prevSlitPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
				nextSlitPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
				for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
        {
					prevSlitPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
					nextSlitPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
        }
				for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
        {
          //agrassi_FI04-02-09
          //prevSepSlitMask[sh] = int.Parse(setup[Order.DEL_SEP_SLIT_MASK + sh.ToString()].ToString());
          //nextSepSlitMask[sh] = int.Parse(nextSetup[Order.DEL_SEP_SLIT_MASK + sh.ToString()].ToString());
          prevSepSlitMask[sh] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SEP_SLIT_MASK, sh));
          nextSepSlitMask[sh] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos+1, Order.DEL_SEP_SLIT_MASK, sh));
          //agrassiFI27-08-2010_1
          prevPlungedSlitMask[sh] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_PLUNGED_SLIT_MASK, sh));
          plungedSlitMask[sh] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos+1, Order.DEL_PLUNGED_SLIT_MASK, sh));
          for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
          {
            //agrassi_FI04-02-09
            //prevSlitPositions[sh][slt] = int.Parse(setup[Order.DEL_SLIT_POSITION + sh.ToString() + ":" + slt.ToString()].ToString());
            //nextSlitPositions[sh][slt] = int.Parse(nextSetup[Order.DEL_SLIT_POSITION + sh.ToString() + ":" + slt.ToString()].ToString());
            prevSlitPositions[sh][slt] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos, Order.DEL_SLIT_POSITION, sh, slt));
            nextSlitPositions[sh][slt] = int.Parse(DBManager.getDBManager().order.GetDryOrderField(pos+1, Order.DEL_SLIT_POSITION, sh, slt));
          }
        }
        prevLevels[0] = (LevelName)Enum.Parse(typeof(LevelName), setup[Order.DEL_LEVEL_NAME_0].ToString());
        prevLevels[1] = (LevelName)Enum.Parse(typeof(LevelName), setup[Order.DEL_LEVEL_NAME_1].ToString());
        prevLevels[2] = (LevelName)Enum.Parse(typeof(LevelName), setup[Order.DEL_LEVEL_NAME_2].ToString());
        nextLevels[0] = (LevelName)Enum.Parse(typeof(LevelName), nextSetup[Order.DEL_LEVEL_NAME_0].ToString());
        nextLevels[1] = (LevelName)Enum.Parse(typeof(LevelName), nextSetup[Order.DEL_LEVEL_NAME_1].ToString());
        nextLevels[2] = (LevelName)Enum.Parse(typeof(LevelName), nextSetup[Order.DEL_LEVEL_NAME_2].ToString());
        //agrassiFI27-08-2010_1
        //this.toolDetail.PositionRotary(this.sectorMask[pos], int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()),
        //          int.Parse(nextSetup[Order.DEL_WEB_WIDTH].ToString()), int.Parse(setup[Order.DEL_SHAFT_MASK].ToString()),
        //          int.Parse(nextSetup[Order.DEL_SHAFT_MASK].ToString()), prevSlitPositions, nextSlitPositions,
        //          prevSepSlitMask, nextSepSlitMask, prevLevels, nextLevels, true, true, true, true, /*TODO: mettere i giusti valori*/
        //          this.maskValid[pos], true);
        this.toolDetail.PositionRotary(this.sectorMask[pos], int.Parse(setup[Order.DEL_WEB_WIDTH].ToString()),
                  int.Parse(nextSetup[Order.DEL_WEB_WIDTH].ToString()), int.Parse(setup[Order.DEL_SHAFT_MASK].ToString()),
                  int.Parse(nextSetup[Order.DEL_SHAFT_MASK].ToString()), prevSlitPositions, nextSlitPositions, prevPlungedSlitMask, plungedSlitMask, 
                  prevSepSlitMask, nextSepSlitMask, prevLevels, nextLevels, true, true, true, true, /*TODO: mettere i giusti valori*/
                  this.maskValid[pos], true);
      }
      else
        //agrassiFI27-08-2010_1
        //this.toolDetail.PositionRotary(0, 0, 0, 0, 0, null, null, null, null, null, null, false, false, false, false, false, true);
        this.toolDetail.PositionRotary(0, 0, 0, 0, 0, null, null, null, null, null, null, null, null, false, false, false, false, false, true);
      return true;
    }

    private void _UpdateRCSMask(int stpInd, int sectorMask, bool valid)
    {
			if ((stpInd < 0) || (stpInd >= SyncroC.DataBase.LineConfig.SlitterScorerHeader.SS_SETUPS - 1)) return;
      //agrassi_FI26-08-2010_2
      DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      if (setupRows != null)
      {
        if (!setupRows[stpInd+1][Order.DEL_CONSTANT_SYSTEM].ToString().Equals(bool.TrueString))
        {
          for (int i = 0; i < DBManager.getDBManager().lineConfig.RotaryConfig.FormatData.SectorsNum; i++)
            sectorMask |= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(i);
        }
      }
      this.sectorMask[stpInd] = sectorMask;
      this.maskValid[stpInd] = valid;
      if (this.toolsPos == stpInd)
        this.ShowTools(this.toolsPos);
    }

    private void Frm_twin400_rotary_Activated(object sender, EventArgs e)
    {
      this.Frm_twin400_tools_Activated(sender, e);
    }

    private void Frm_twin400_rotary_Load(object sender, EventArgs e)
    {
      this.Frm_twin400_tools_Load(sender, e);
    }

    private void Frm_twin400_rotary_SizeChanged(object sender, EventArgs e)
    {
      this.Frm_twin400_tools_SizeChanged(sender, e); 
    }
	}
}

