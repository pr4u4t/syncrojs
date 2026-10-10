using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.Forms.Pages.Debug.Twin400
{
	public class Frm_DebugTwin400_positioning : SyncroC.Forms.Pages.Debug.Twin400.Frm_DebugTwin400Subgroup 
	{
    private SyncroC.Controls.Tools.MachineTools toolDetailTheoretical;
    private Label lbl_TheoreticalPosit_YN;
    private Label lbl_MachinePosit_YN;
    private SyncroC.Controls.Tools.MachineTools toolDetailMachine;
		private System.ComponentModel.IContainer components = null;
    private int[][] machinePositions, theoreticalPositions;
    private int[] slittersNotOKMask;
    private int[] plungedSlitMask;
    private Label lbl_ToleranceText_YN;
    private Label lbl_ToleranceValue;
    private Label lbl_PosCounters;
    private Label lbl_PosCounter0;
    private Label lbl_PosCounter1;
    //CM1 19-08-2005
    private bool[] positioningOK ={true, true};

		public Frm_DebugTwin400_positioning()
		{
			InitializeComponent();
      LineConfig lineConfig=DBManager.getDBManager().lineConfig;
      this.toolDetailTheoretical.Config(lineConfig, SyncroC.Controls.Tools.MachineToolsType.OnlySlits,
                                false, false, null, null, "", "");
      this.toolDetailTheoretical.PositionOnlySlits(null, null, new bool[]{false, false});
      this.toolDetailMachine.Config(lineConfig, SyncroC.Controls.Tools.MachineToolsType.OnlySlits,
                                false, false, null, null, "", "");
      this.toolDetailMachine.PositionOnlySlits(null, null, new bool[] { false, false });
			this.machinePositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
			this.theoreticalPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
			this.slittersNotOKMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.plungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.positioningOK = new bool[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
      {
				this.machinePositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
				this.theoreticalPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
      }
			//agrassi_fi29-09-08_2
			this.lbl_PosCounters.Visible = true;
			this.lbl_PosCounter0.Visible = true;
			this.lbl_PosCounter1.Visible = true;
			this.toolDetailMachine.Debug = true;
			this.toolDetailTheoretical.Debug =true;

      this.refreshData();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugTwin400_positioning));
      this.toolDetailTheoretical = new SyncroC.Controls.Tools.MachineTools();
      this.lbl_TheoreticalPosit_YN = new System.Windows.Forms.Label();
      this.lbl_MachinePosit_YN = new System.Windows.Forms.Label();
      this.toolDetailMachine = new SyncroC.Controls.Tools.MachineTools();
      this.lbl_ToleranceText_YN = new System.Windows.Forms.Label();
      this.lbl_ToleranceValue = new System.Windows.Forms.Label();
      this.lbl_PosCounters = new System.Windows.Forms.Label();
      this.lbl_PosCounter0 = new System.Windows.Forms.Label();
      this.lbl_PosCounter1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "TWIN 400 SLIT POSITIONING";
      // 
      // tab_TabD
      // 
      this.tab_TabD.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageSize = new System.Drawing.Size(79, 28);
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // toolDetailTheoretical
      // 
      this.toolDetailTheoretical.BackColor = System.Drawing.Color.WhiteSmoke;
      this.toolDetailTheoretical.Debug = false;
      this.toolDetailTheoretical.Location = new System.Drawing.Point(134, 219);
      this.toolDetailTheoretical.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.toolDetailTheoretical.Name = "toolDetailTheoretical";
      this.toolDetailTheoretical.Size = new System.Drawing.Size(1067, 215);
      this.toolDetailTheoretical.TabIndex = 205;
      this.toolDetailTheoretical.TabStop = false;
      this.toolDetailTheoretical.TranslateText = false;
      // 
      // lbl_TheoreticalPosit_YN
      // 
      this.lbl_TheoreticalPosit_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_TheoreticalPosit_YN.Location = new System.Drawing.Point(134, 194);
      this.lbl_TheoreticalPosit_YN.Name = "lbl_TheoreticalPosit_YN";
      this.lbl_TheoreticalPosit_YN.Size = new System.Drawing.Size(424, 24);
      this.lbl_TheoreticalPosit_YN.TabIndex = 206;
      this.lbl_TheoreticalPosit_YN.Text = "Theoretical Slit Positions";
      // 
      // lbl_MachinePosit_YN
      // 
      this.lbl_MachinePosit_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MachinePosit_YN.ForeColor = System.Drawing.Color.Green;
      this.lbl_MachinePosit_YN.Location = new System.Drawing.Point(134, 470);
      this.lbl_MachinePosit_YN.Name = "lbl_MachinePosit_YN";
      this.lbl_MachinePosit_YN.Size = new System.Drawing.Size(424, 24);
      this.lbl_MachinePosit_YN.TabIndex = 208;
      this.lbl_MachinePosit_YN.Text = "Machine Slit Positions";
      // 
      // toolDetailMachine
      // 
      this.toolDetailMachine.BackColor = System.Drawing.Color.WhiteSmoke;
      this.toolDetailMachine.Debug = false;
      this.toolDetailMachine.Location = new System.Drawing.Point(134, 495);
      this.toolDetailMachine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.toolDetailMachine.Name = "toolDetailMachine";
      this.toolDetailMachine.Size = new System.Drawing.Size(1067, 215);
      this.toolDetailMachine.TabIndex = 207;
      this.toolDetailMachine.TabStop = false;
      this.toolDetailMachine.TranslateText = false;
      // 
      // lbl_ToleranceText_YN
      // 
      this.lbl_ToleranceText_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ToleranceText_YN.Location = new System.Drawing.Point(816, 194);
      this.lbl_ToleranceText_YN.Name = "lbl_ToleranceText_YN";
      this.lbl_ToleranceText_YN.Size = new System.Drawing.Size(324, 24);
      this.lbl_ToleranceText_YN.TabIndex = 209;
      this.lbl_ToleranceText_YN.Text = "Slit positions tolerance (mm/10)";
      // 
      // lbl_ToleranceValue
      // 
      this.lbl_ToleranceValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ToleranceValue.Location = new System.Drawing.Point(1148, 193);
      this.lbl_ToleranceValue.Name = "lbl_ToleranceValue";
      this.lbl_ToleranceValue.Size = new System.Drawing.Size(39, 24);
      this.lbl_ToleranceValue.TabIndex = 210;
      // 
      // lbl_PosCounters
      // 
      this.lbl_PosCounters.AutoSize = true;
      this.lbl_PosCounters.Location = new System.Drawing.Point(579, 454);
      this.lbl_PosCounters.Name = "lbl_PosCounters";
      this.lbl_PosCounters.Size = new System.Drawing.Size(105, 13);
      this.lbl_PosCounters.TabIndex = 211;
      this.lbl_PosCounters.Text = "Positioning counters:";
      this.lbl_PosCounters.Visible = false;
      // 
      // lbl_PosCounter0
      // 
      this.lbl_PosCounter0.BackColor = System.Drawing.Color.Yellow;
      this.lbl_PosCounter0.Location = new System.Drawing.Point(730, 451);
      this.lbl_PosCounter0.Name = "lbl_PosCounter0";
      this.lbl_PosCounter0.Size = new System.Drawing.Size(62, 16);
      this.lbl_PosCounter0.TabIndex = 212;
      this.lbl_PosCounter0.Visible = false;
      // 
      // lbl_PosCounter1
      // 
      this.lbl_PosCounter1.BackColor = System.Drawing.Color.Yellow;
      this.lbl_PosCounter1.Location = new System.Drawing.Point(832, 451);
      this.lbl_PosCounter1.Name = "lbl_PosCounter1";
      this.lbl_PosCounter1.Size = new System.Drawing.Size(61, 16);
      this.lbl_PosCounter1.TabIndex = 213;
      this.lbl_PosCounter1.Visible = false;
      // 
      // Frm_DebugTwin400_positioning
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_PosCounter1);
      this.Controls.Add(this.lbl_PosCounter0);
      this.Controls.Add(this.lbl_PosCounters);
      this.Controls.Add(this.lbl_ToleranceValue);
      this.Controls.Add(this.lbl_ToleranceText_YN);
      this.Controls.Add(this.lbl_MachinePosit_YN);
      this.Controls.Add(this.toolDetailMachine);
      this.Controls.Add(this.lbl_TheoreticalPosit_YN);
      this.Controls.Add(this.toolDetailTheoretical);
      this.Name = "Frm_DebugTwin400_positioning";
      this.Text = "Frm_twin400_positioning";
      this.Activated += new System.EventHandler(this.Frm_twin400_positioning_Activated);
      this.Controls.SetChildIndex(this.lab_path, 0);
      this.Controls.SetChildIndex(this.lab_Time, 0);
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
      this.Controls.SetChildIndex(this.toolDetailTheoretical, 0);
      this.Controls.SetChildIndex(this.lbl_TheoreticalPosit_YN, 0);
      this.Controls.SetChildIndex(this.toolDetailMachine, 0);
      this.Controls.SetChildIndex(this.lbl_MachinePosit_YN, 0);
      this.Controls.SetChildIndex(this.lbl_ToleranceText_YN, 0);
      this.Controls.SetChildIndex(this.lbl_ToleranceValue, 0);
      this.Controls.SetChildIndex(this.lbl_PosCounters, 0);
      this.Controls.SetChildIndex(this.lbl_PosCounter0, 0);
      this.Controls.SetChildIndex(this.lbl_PosCounter1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

    private void _SetPositioningCheck(string data)
    {
      string[] sData = data.Split(Command.PARAMETER_SEPARATOR);
			if (sData.Length != (4 + 2 * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT))
        return;
      Shaft sh;
			int[] theorPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
			int[] machPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
      int sltNotOKMask = 0;
      int slitMask;
      sh = (Shaft)this.intParse(sData[0]);
      slitMask = this.intParse(sData[1]);
      sltNotOKMask = this.intParse(sData[2]);
			for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; i++)
      {
        theorPositions[i] = this.intParse(sData[3 + i * 2 + 0]);
        machPositions[i] = this.intParse(sData[3 + i * 2 + 1]);
      }
      this.positioningOK[(int)sh] = (sltNotOKMask==0);
      this.plungedSlitMask[(int)sh] = slitMask;
      this.theoreticalPositions[(int)sh] = theorPositions;
      this.machinePositions[(int)sh] = machPositions;
      this.slittersNotOKMask[(int)sh] = sltNotOKMask;
      this.UpdatePositioningStatus();
    }

    private void UpdatePositioningStatus()
    {
      bool ok = this.positioningOK[(int)Shaft.Slit1] && this.positioningOK[(int)Shaft.Slit2];
      this.lbl_MachinePosit_YN.ForeColor = (ok ? Color.Green : Color.Red);
      this.toolDetailTheoretical.PositionOnlySlits(this.theoreticalPositions, this.plungedSlitMask, new bool[] { true, true });
      this.toolDetailMachine.PositionOnlySlits(this.machinePositions, this.plungedSlitMask, this.slittersNotOKMask);
    }

    //CM1 19-08-2005
    private void Frm_twin400_positioning_Activated(object sender, EventArgs e)
    {
      this.UpdatePositioningStatus();
    }

    public override void refreshData()
    {
			//RFAZZI PROVA
			//this.lbl_ToleranceValue.Text = (4 * DBManager.getDBManager().lineConfig.SlitterScorerConfig.PositioningOptions.Slits.PositionTolerance).ToString();
			this.lbl_ToleranceValue.Text = (4 * DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.PositionTolerance).ToString();
		}

    //CM4 01-09-2005
    protected override void ControlD()
    {
      this.lbl_PosCounters.Visible = !this.lbl_PosCounters.Visible;
      this.lbl_PosCounter0.Visible = !this.lbl_PosCounter0.Visible;
      this.lbl_PosCounter1.Visible = !this.lbl_PosCounter1.Visible;
      base.ControlD();
      this.toolDetailMachine.Debug = !this.toolDetailMachine.Debug;
      this.toolDetailTheoretical.Debug = !this.toolDetailTheoretical.Debug;
    }
	}
}

