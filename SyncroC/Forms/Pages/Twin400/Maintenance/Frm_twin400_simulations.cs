using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using SyncroC.Network;
using SyncroC.Forms.PopUp;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Twin400.Maintenance
{
	public class Frm_twin400_simulations : SyncroC.Forms.Pages.Twin400.Maintenance.Frm_twin400_maintenanceSubgroup
	{
		private SyncroC.Controls.StateButton Simulation;
		private SyncroC.Controls.TitledTextBox txt_SimSpeed;
		private SyncroC.Controls.StateButton AOCSimulation;
		private SyncroC.Controls.TitledTextBox txt_Cycle;
		private SyncroC.Controls.CheckBox chk_Nogap;
		private SyncroC.Controls.Title title1;
		private SyncroC.Controls.Title title2;
		private Label lbl_inputSpeed;
		private Label lbl_inputTime;
		private Label label1;
		private Label label2;
		private SyncroC.Controls.TitledLabel tlb_TimeUntilAOC;
		private SyncroC.Controls.CheckBox chk_Grinding;
		private SyncroC.Controls.CheckBox chk_Lube;
		private Label lbl_MachineRestored;
		private Label lbl_MachineNotRunning;
		private GroupBox grp_SyncroSimulation;
		private Label lab_EnableSimulation;
		private Label lab_Remember;
		private SyncroC.Controls.StateButton TerminateSimulation;
		private SyncroC.Controls.StateButton PrepareForSimulation;
		private Label lab_WarnSimulation;
		private System.ComponentModel.IContainer components = null;
		string serverProductionPath;
		private Timer tim_Sim;
		private string startPath;
		private Label lab_NewShift;
		private const string TWIN400_TAG = "twin400";

		public Frm_twin400_simulations()
		{
			InitializeComponent();
			this.CheckSimulationState();
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

		protected override void F13()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_simulations));
      this.Simulation = new SyncroC.Controls.StateButton();
      this.txt_SimSpeed = new SyncroC.Controls.TitledTextBox();
      this.AOCSimulation = new SyncroC.Controls.StateButton();
      this.txt_Cycle = new SyncroC.Controls.TitledTextBox();
      this.chk_Nogap = new SyncroC.Controls.CheckBox();
      this.title1 = new SyncroC.Controls.Title();
      this.title2 = new SyncroC.Controls.Title();
      this.lbl_inputSpeed = new System.Windows.Forms.Label();
      this.lbl_inputTime = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tlb_TimeUntilAOC = new SyncroC.Controls.TitledLabel();
      this.chk_Grinding = new SyncroC.Controls.CheckBox();
      this.chk_Lube = new SyncroC.Controls.CheckBox();
      this.lbl_MachineRestored = new System.Windows.Forms.Label();
      this.lbl_MachineNotRunning = new System.Windows.Forms.Label();
      this.grp_SyncroSimulation = new System.Windows.Forms.GroupBox();
      this.lab_NewShift = new System.Windows.Forms.Label();
      this.lab_EnableSimulation = new System.Windows.Forms.Label();
      this.lab_Remember = new System.Windows.Forms.Label();
      this.TerminateSimulation = new SyncroC.Controls.StateButton();
      this.PrepareForSimulation = new SyncroC.Controls.StateButton();
      this.lab_WarnSimulation = new System.Windows.Forms.Label();
      this.tim_Sim = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_SyncroSimulation.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
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
      // tab_TabD
      // 
      this.tab_TabD.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // Simulation
      // 
      this.Simulation.Active = false;
      this.Simulation.Alarmed = false;
      this.Simulation.Auto = false;
      this.Simulation.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Simulation.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Simulation.FillBackColorWithImage = false;
      this.Simulation.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Simulation.FramePresent = true;
      this.Simulation.HighLightEnable = false;
      this.Simulation.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Simulation.ImagePassword = null;
      this.Simulation.Inemergency = false;
      this.Simulation.InLocal = false;
      this.Simulation.IsButton = true;
      this.Simulation.IsForceToInvisible = false;
      this.Simulation.Location = new System.Drawing.Point(970, 260);
      this.Simulation.Name = "Simulation";
      this.Simulation.OnLine = false;
      this.Simulation.ShowImage = false;
      this.Simulation.ShowText = true;
      this.Simulation.Size = new System.Drawing.Size(156, 88);
      this.Simulation.Switching = false;
      this.Simulation.TabIndex = 105;
      this.Simulation.Text = "Start Simulation";
      this.Simulation.TranslateText = false;
      this.Simulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Simulation.UserDefinedActiveImage")));
      this.Simulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Simulation.UserDefinedImage")));
      this.Simulation.Warned = false;
      this.Simulation.Click += new System.EventHandler(this.OnToggleSimulationPush);
      // 
      // txt_SimSpeed
      // 
      this.txt_SimSpeed.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txt_SimSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SimSpeed.ImagePassword = null;
      this.txt_SimSpeed.Location = new System.Drawing.Point(630, 269);
      this.txt_SimSpeed.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.txt_SimSpeed.MaxChar = 32767;
      this.txt_SimSpeed.Multiline = false;
      this.txt_SimSpeed.Name = "txt_SimSpeed";
      this.txt_SimSpeed.PasswordChar = '\0';
      this.txt_SimSpeed.ReadOnly = false;
      this.txt_SimSpeed.Size = new System.Drawing.Size(79, 57);
      this.txt_SimSpeed.Style = SyncroC.Controls.TitledTextBoxStyles.Framed;
      this.txt_SimSpeed.TabIndex = 106;
      this.txt_SimSpeed.Text = "Speed";
      this.txt_SimSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SimSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.txt_SimSpeed.TranslateText = false;
      this.txt_SimSpeed.Value = " ";
      this.txt_SimSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // AOCSimulation
      // 
      this.AOCSimulation.Active = false;
      this.AOCSimulation.Alarmed = false;
      this.AOCSimulation.Auto = false;
      this.AOCSimulation.BackColor = System.Drawing.Color.WhiteSmoke;
      this.AOCSimulation.Cursor = System.Windows.Forms.Cursors.Hand;
      this.AOCSimulation.FillBackColorWithImage = false;
      this.AOCSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.AOCSimulation.FramePresent = true;
      this.AOCSimulation.HighLightEnable = false;
      this.AOCSimulation.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.AOCSimulation.ImagePassword = null;
      this.AOCSimulation.Inemergency = false;
      this.AOCSimulation.InLocal = false;
      this.AOCSimulation.IsButton = true;
      this.AOCSimulation.IsForceToInvisible = false;
      this.AOCSimulation.Location = new System.Drawing.Point(943, 523);
      this.AOCSimulation.Name = "AOCSimulation";
      this.AOCSimulation.OnLine = false;
      this.AOCSimulation.ShowImage = false;
      this.AOCSimulation.ShowText = true;
      this.AOCSimulation.Size = new System.Drawing.Size(184, 88);
      this.AOCSimulation.Switching = false;
      this.AOCSimulation.TabIndex = 107;
      this.AOCSimulation.Text = "Start AOC Simulation";
      this.AOCSimulation.TranslateText = false;
      this.AOCSimulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AOCSimulation.UserDefinedActiveImage")));
      this.AOCSimulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AOCSimulation.UserDefinedImage")));
      this.AOCSimulation.Warned = false;
      this.AOCSimulation.Click += new System.EventHandler(this.OnToggleAOCSimulationPush);
      // 
      // txt_Cycle
      // 
      this.txt_Cycle.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txt_Cycle.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_Cycle.ImagePassword = null;
      this.txt_Cycle.Location = new System.Drawing.Point(656, 534);
      this.txt_Cycle.MaxChar = 32767;
      this.txt_Cycle.Multiline = false;
      this.txt_Cycle.Name = "txt_Cycle";
      this.txt_Cycle.PasswordChar = '\0';
      this.txt_Cycle.ReadOnly = false;
      this.txt_Cycle.Size = new System.Drawing.Size(94, 56);
      this.txt_Cycle.Style = SyncroC.Controls.TitledTextBoxStyles.Framed;
      this.txt_Cycle.TabIndex = 108;
      this.txt_Cycle.Text = "Cycle time";
      this.txt_Cycle.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_Cycle.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.txt_Cycle.TranslateText = false;
      this.txt_Cycle.Value = " ";
      this.txt_Cycle.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_Nogap
      // 
      this.chk_Nogap.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Nogap.Checked = false;
      this.chk_Nogap.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Nogap.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Nogap.ImagePassword = null;
      this.chk_Nogap.Location = new System.Drawing.Point(839, 533);
      this.chk_Nogap.Name = "chk_Nogap";
      this.chk_Nogap.ReadOnly = false;
      this.chk_Nogap.Size = new System.Drawing.Size(80, 57);
      this.chk_Nogap.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_Nogap.TabIndex = 109;
      this.chk_Nogap.Text = "Nogap";
      this.chk_Nogap.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Nogap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Nogap.TranslateText = false;
      // 
      // title1
      // 
      this.title1.BackColor = System.Drawing.Color.Black;
      this.title1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title1.Location = new System.Drawing.Point(600, 219);
      this.title1.Name = "title1";
      this.title1.Size = new System.Drawing.Size(526, 188);
      this.title1.Style = SyncroC.Controls.TitleStyles.Big;
      this.title1.TabIndex = 110;
      this.title1.Text = "SIMULATION";
      this.title1.TranslateText = false;
      // 
      // title2
      // 
      this.title2.BackColor = System.Drawing.Color.Black;
      this.title2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title2.Location = new System.Drawing.Point(601, 483);
      this.title2.Name = "title2";
      this.title2.Size = new System.Drawing.Size(526, 188);
      this.title2.Style = SyncroC.Controls.TitleStyles.Big;
      this.title2.TabIndex = 111;
      this.title2.Text = "ORDER CHANGE SIMULATION";
      this.title2.TranslateText = false;
      // 
      // lbl_inputSpeed
      // 
      this.lbl_inputSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_inputSpeed.ForeColor = System.Drawing.Color.Red;
      this.lbl_inputSpeed.Location = new System.Drawing.Point(630, 361);
      this.lbl_inputSpeed.Name = "lbl_inputSpeed";
      this.lbl_inputSpeed.Size = new System.Drawing.Size(273, 46);
      this.lbl_inputSpeed.TabIndex = 112;
      this.lbl_inputSpeed.Text = "Insert speed (20 to 300) before to start simulation!";
      this.lbl_inputSpeed.Visible = false;
      // 
      // lbl_inputTime
      // 
      this.lbl_inputTime.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_inputTime.ForeColor = System.Drawing.Color.Red;
      this.lbl_inputTime.Location = new System.Drawing.Point(662, 625);
      this.lbl_inputTime.Name = "lbl_inputTime";
      this.lbl_inputTime.Size = new System.Drawing.Size(257, 46);
      this.lbl_inputTime.TabIndex = 113;
      this.lbl_inputTime.Text = "Insert cycle time (2 to 600) before to start aoc simulation!";
      this.lbl_inputTime.Visible = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(712, 300);
      this.label1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 15);
      this.label1.TabIndex = 114;
      this.label1.Text = "m/min";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(765, 564);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(26, 15);
      this.label2.TabIndex = 115;
      this.label2.Text = "sec";
      // 
      // tlb_TimeUntilAOC
      // 
      this.tlb_TimeUntilAOC.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_TimeUntilAOC.ImagePassword = null;
      this.tlb_TimeUntilAOC.Location = new System.Drawing.Point(971, 617);
      this.tlb_TimeUntilAOC.Name = "tlb_TimeUntilAOC";
      this.tlb_TimeUntilAOC.Size = new System.Drawing.Size(131, 54);
      this.tlb_TimeUntilAOC.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_TimeUntilAOC.TabIndex = 116;
      this.tlb_TimeUntilAOC.Text = "Time until AOC";
      this.tlb_TimeUntilAOC.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_TimeUntilAOC.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_TimeUntilAOC.TranslateText = false;
      this.tlb_TimeUntilAOC.Value = "0";
      this.tlb_TimeUntilAOC.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_TimeUntilAOC.ValueMultiline = true;
      // 
      // chk_Grinding
      // 
      this.chk_Grinding.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Grinding.Checked = false;
      this.chk_Grinding.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Grinding.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Grinding.ImagePassword = null;
      this.chk_Grinding.Location = new System.Drawing.Point(782, 269);
      this.chk_Grinding.Name = "chk_Grinding";
      this.chk_Grinding.ReadOnly = false;
      this.chk_Grinding.Size = new System.Drawing.Size(80, 57);
      this.chk_Grinding.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_Grinding.TabIndex = 117;
      this.chk_Grinding.Text = "Grinding";
      this.chk_Grinding.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Grinding.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Grinding.TranslateText = false;
      // 
      // chk_Lube
      // 
      this.chk_Lube.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Lube.Checked = false;
      this.chk_Lube.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Lube.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Lube.ImagePassword = null;
      this.chk_Lube.Location = new System.Drawing.Point(878, 269);
      this.chk_Lube.Name = "chk_Lube";
      this.chk_Lube.ReadOnly = false;
      this.chk_Lube.Size = new System.Drawing.Size(80, 57);
      this.chk_Lube.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_Lube.TabIndex = 118;
      this.chk_Lube.Text = "Lubrication";
      this.chk_Lube.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Lube.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Lube.TranslateText = false;
      // 
      // lbl_MachineRestored
      // 
      this.lbl_MachineRestored.BackColor = System.Drawing.Color.LightGreen;
      this.lbl_MachineRestored.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_MachineRestored.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MachineRestored.Location = new System.Drawing.Point(982, 422);
      this.lbl_MachineRestored.Name = "lbl_MachineRestored";
      this.lbl_MachineRestored.Size = new System.Drawing.Size(129, 26);
      this.lbl_MachineRestored.TabIndex = 207;
      this.lbl_MachineRestored.Text = "Machine restored";
      this.lbl_MachineRestored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_MachineNotRunning
      // 
      this.lbl_MachineNotRunning.BackColor = System.Drawing.Color.LightGreen;
      this.lbl_MachineNotRunning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_MachineNotRunning.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MachineNotRunning.Location = new System.Drawing.Point(982, 380);
      this.lbl_MachineNotRunning.Name = "lbl_MachineNotRunning";
      this.lbl_MachineNotRunning.Size = new System.Drawing.Size(129, 26);
      this.lbl_MachineNotRunning.TabIndex = 206;
      this.lbl_MachineNotRunning.Text = "Machine not running";
      this.lbl_MachineNotRunning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // grp_SyncroSimulation
      // 
      this.grp_SyncroSimulation.Controls.Add(this.lab_NewShift);
      this.grp_SyncroSimulation.Controls.Add(this.lab_EnableSimulation);
      this.grp_SyncroSimulation.Controls.Add(this.lab_Remember);
      this.grp_SyncroSimulation.Controls.Add(this.TerminateSimulation);
      this.grp_SyncroSimulation.Controls.Add(this.PrepareForSimulation);
      this.grp_SyncroSimulation.Controls.Add(this.lab_WarnSimulation);
      this.grp_SyncroSimulation.Location = new System.Drawing.Point(116, 251);
      this.grp_SyncroSimulation.Name = "grp_SyncroSimulation";
      this.grp_SyncroSimulation.Size = new System.Drawing.Size(428, 391);
      this.grp_SyncroSimulation.TabIndex = 208;
      this.grp_SyncroSimulation.TabStop = false;
      this.grp_SyncroSimulation.Text = "Syncro Simulation";
      // 
      // lab_NewShift
      // 
      this.lab_NewShift.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_NewShift.ForeColor = System.Drawing.Color.Blue;
      this.lab_NewShift.Location = new System.Drawing.Point(38, 323);
      this.lab_NewShift.Name = "lab_NewShift";
      this.lab_NewShift.Size = new System.Drawing.Size(347, 52);
      this.lab_NewShift.TabIndex = 135;
      this.lab_NewShift.Text = "Start a new shift to make order changes.";
      this.lab_NewShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_NewShift.Visible = false;
      // 
      // lab_EnableSimulation
      // 
      this.lab_EnableSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_EnableSimulation.ForeColor = System.Drawing.Color.Blue;
      this.lab_EnableSimulation.Location = new System.Drawing.Point(6, 13);
      this.lab_EnableSimulation.Name = "lab_EnableSimulation";
      this.lab_EnableSimulation.Size = new System.Drawing.Size(421, 54);
      this.lab_EnableSimulation.TabIndex = 134;
      this.lab_EnableSimulation.Text = "To enable simulation, Syncro must be in idle shift !";
      this.lab_EnableSimulation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_EnableSimulation.Visible = false;
      // 
      // lab_Remember
      // 
      this.lab_Remember.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_Remember.ForeColor = System.Drawing.Color.Red;
      this.lab_Remember.Location = new System.Drawing.Point(38, 271);
      this.lab_Remember.Name = "lab_Remember";
      this.lab_Remember.Size = new System.Drawing.Size(347, 52);
      this.lab_Remember.TabIndex = 132;
      this.lab_Remember.Text = "REMEMBER TO PRESS \'Terminate Simulation\' AT THE END !";
      this.lab_Remember.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_Remember.Visible = false;
      // 
      // TerminateSimulation
      // 
      this.TerminateSimulation.Active = false;
      this.TerminateSimulation.Alarmed = false;
      this.TerminateSimulation.Auto = false;
      this.TerminateSimulation.BackColor = System.Drawing.Color.WhiteSmoke;
      this.TerminateSimulation.Cursor = System.Windows.Forms.Cursors.Hand;
      this.TerminateSimulation.FillBackColorWithImage = false;
      this.TerminateSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TerminateSimulation.ForeColor = System.Drawing.Color.Black;
      this.TerminateSimulation.FramePresent = true;
      this.TerminateSimulation.HighLightEnable = true;
      this.TerminateSimulation.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.TerminateSimulation.ImagePassword = null;
      this.TerminateSimulation.Inemergency = false;
      this.TerminateSimulation.InLocal = false;
      this.TerminateSimulation.IsButton = true;
      this.TerminateSimulation.IsForceToInvisible = false;
      this.TerminateSimulation.Location = new System.Drawing.Point(112, 140);
      this.TerminateSimulation.Name = "TerminateSimulation";
      this.TerminateSimulation.OnLine = false;
      this.TerminateSimulation.ShowImage = false;
      this.TerminateSimulation.ShowText = true;
      this.TerminateSimulation.Size = new System.Drawing.Size(185, 70);
      this.TerminateSimulation.Switching = false;
      this.TerminateSimulation.TabIndex = 131;
      this.TerminateSimulation.Text = "Terminate Simulation";
      this.TerminateSimulation.TranslateText = false;
      this.TerminateSimulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TerminateSimulation.UserDefinedActiveImage")));
      this.TerminateSimulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TerminateSimulation.UserDefinedImage")));
      this.TerminateSimulation.Warned = false;
      this.TerminateSimulation.Click += new System.EventHandler(this.TerminateSimulation_Click);
      // 
      // PrepareForSimulation
      // 
      this.PrepareForSimulation.Active = false;
      this.PrepareForSimulation.Alarmed = false;
      this.PrepareForSimulation.Auto = false;
      this.PrepareForSimulation.BackColor = System.Drawing.Color.WhiteSmoke;
      this.PrepareForSimulation.Cursor = System.Windows.Forms.Cursors.Hand;
      this.PrepareForSimulation.FillBackColorWithImage = false;
      this.PrepareForSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PrepareForSimulation.ForeColor = System.Drawing.Color.Black;
      this.PrepareForSimulation.FramePresent = true;
      this.PrepareForSimulation.HighLightEnable = false;
      this.PrepareForSimulation.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.PrepareForSimulation.ImagePassword = null;
      this.PrepareForSimulation.Inemergency = false;
      this.PrepareForSimulation.InLocal = false;
      this.PrepareForSimulation.IsButton = true;
      this.PrepareForSimulation.IsForceToInvisible = false;
      this.PrepareForSimulation.Location = new System.Drawing.Point(112, 70);
      this.PrepareForSimulation.Name = "PrepareForSimulation";
      this.PrepareForSimulation.OnLine = false;
      this.PrepareForSimulation.ShowImage = false;
      this.PrepareForSimulation.ShowText = true;
      this.PrepareForSimulation.Size = new System.Drawing.Size(185, 70);
      this.PrepareForSimulation.Switching = false;
      this.PrepareForSimulation.TabIndex = 130;
      this.PrepareForSimulation.Text = "Prepare For Simulation";
      this.PrepareForSimulation.TranslateText = false;
      this.PrepareForSimulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("PrepareForSimulation.UserDefinedActiveImage")));
      this.PrepareForSimulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("PrepareForSimulation.UserDefinedImage")));
      this.PrepareForSimulation.Warned = false;
      this.PrepareForSimulation.Click += new System.EventHandler(this.PrepareForSimulation_Click);
      // 
      // lab_WarnSimulation
      // 
      this.lab_WarnSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_WarnSimulation.ForeColor = System.Drawing.Color.Red;
      this.lab_WarnSimulation.Location = new System.Drawing.Point(38, 219);
      this.lab_WarnSimulation.Name = "lab_WarnSimulation";
      this.lab_WarnSimulation.Size = new System.Drawing.Size(347, 52);
      this.lab_WarnSimulation.TabIndex = 129;
      this.lab_WarnSimulation.Text = "WARNING !  The communication with Office is disabled  ! ";
      this.lab_WarnSimulation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_WarnSimulation.Visible = false;
      // 
      // tim_Sim
      // 
      this.tim_Sim.Enabled = true;
      this.tim_Sim.Interval = 1000;
      this.tim_Sim.Tick += new System.EventHandler(this.tim_Sim_Tick);
      // 
      // Frm_twin400_simulations
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.grp_SyncroSimulation);
      this.Controls.Add(this.lbl_MachineRestored);
      this.Controls.Add(this.lbl_MachineNotRunning);
      this.Controls.Add(this.chk_Lube);
      this.Controls.Add(this.chk_Grinding);
      this.Controls.Add(this.tlb_TimeUntilAOC);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lbl_inputTime);
      this.Controls.Add(this.lbl_inputSpeed);
      this.Controls.Add(this.title2);
      this.Controls.Add(this.title1);
      this.Controls.Add(this.chk_Nogap);
      this.Controls.Add(this.txt_Cycle);
      this.Controls.Add(this.AOCSimulation);
      this.Controls.Add(this.txt_SimSpeed);
      this.Controls.Add(this.Simulation);
      this.Name = "Frm_twin400_simulations";
      this.Tag = "";
      this.Text = "Frm_twin400_simulations";
      this.Load += new System.EventHandler(this.Frm_twin400_simulations_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.Simulation, 0);
      this.Controls.SetChildIndex(this.txt_SimSpeed, 0);
      this.Controls.SetChildIndex(this.AOCSimulation, 0);
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
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
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
      this.Controls.SetChildIndex(this.txt_Cycle, 0);
      this.Controls.SetChildIndex(this.chk_Nogap, 0);
      this.Controls.SetChildIndex(this.title1, 0);
      this.Controls.SetChildIndex(this.title2, 0);
      this.Controls.SetChildIndex(this.lbl_inputSpeed, 0);
      this.Controls.SetChildIndex(this.lbl_inputTime, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.tlb_TimeUntilAOC, 0);
      this.Controls.SetChildIndex(this.chk_Grinding, 0);
      this.Controls.SetChildIndex(this.chk_Lube, 0);
      this.Controls.SetChildIndex(this.lbl_MachineNotRunning, 0);
      this.Controls.SetChildIndex(this.lbl_MachineRestored, 0);
      this.Controls.SetChildIndex(this.grp_SyncroSimulation, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_SyncroSimulation.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		private void OnToggleSimulationPush(object sender, System.EventArgs e)
		{
			if ((!DBManager.getDBManager().syncroConfig.IsTwin400Only()) && (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer()))
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}

			if (Accessibility.getAccessibility().isLevelHigh())
			{
				if (Directory.Exists(this.serverProductionPath + Command.BACKUP_EXTENSION) ||
						DBManager.getDBManager().syncroConfig.IsTwin400Only())
				{

					int speed = this.intParse(this.txt_SimSpeed.Value);
					if (((speed < 20) || (speed > 300)) && (!this.Simulation.Active))
					{
						this.lbl_inputSpeed.Visible = true;
						return;
					}
					this.lbl_inputSpeed.Visible = false;
					CommunicationManager cm = CommunicationManager.GetCommunicationManager();
					cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_simulations)) +
							"." + ((Control)sender).Name + "(" + speed.ToString() + "," + (this.chk_Grinding.Checked ? "1" : "0") +
							"," + (this.chk_Lube.Checked ? "1" : "0") + ")");
				}
				else
				{
					PageManager.getPageManager()._SetMessage("The simulation has not been prepared!", true);
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		private void OnToggleAOCSimulationPush(object sender, System.EventArgs e)
		{
			if ((!DBManager.getDBManager().syncroConfig.IsTwin400Only()) && (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer()))
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			if (Accessibility.getAccessibility().isLevelHigh())
			{
        if (!DBManager.getDBManager().syncroConfig.IsTwin400Only() && RTCManager.getRTCManager().IsIdleShift)
        {
          Frm_Alert frm_alert = new Frm_Alert();
          frm_alert.ShowDialog(this, "You have to start a shift, otherwise Syncro cannot perform order change!");
          return;
        }
				int cycle = this.intParse(this.txt_Cycle.Value);
				string nogap = (this.chk_Nogap.Checked ? "1" : "0");
				if (((cycle < 2) || (cycle > 600)) && (!this.AOCSimulation.Active))
				{
					this.lbl_inputTime.Visible = true;
					return;
				}
				this.lbl_inputTime.Visible = false;
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_simulations)) +
								"." + ((Control)sender).Name + "(" + cycle + "," + nogap + ")");
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
		}

		private void Frm_twin400_simulations_Load(object sender, EventArgs e)
		{
			if (DBManager.getDBManager().syncroConfig.IsTwin400Only())
			{
				this.grp_SyncroSimulation.Enabled = false;
				return;
			}
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			this.serverProductionPath = SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Substring(0, SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Length - 1);
			this.startPath = SyncroC.Utility.Settings.getSettings().paths.Source + "_server\\";
#if DEBUG
			startPath += "debug\\";
#else
            startPath += "release\\";
#endif
		}

		private void CheckSimulationState()
		{
			if (DBManager.getDBManager().syncroConfig.IsTwin400Only())
				return;
			bool isIdleShift = RTCManager.getRTCManager().IsIdleShift;
			bool dirExists = Directory.Exists(this.serverProductionPath + Command.BACKUP_EXTENSION);
			this.TerminateSimulation.Enabled = dirExists;
			this.PrepareForSimulation.Enabled = ((!dirExists) && isIdleShift);
      this.lab_EnableSimulation.Visible = (!dirExists) && (!isIdleShift);
      //AGrassi_FI06/12/2007_1
      this.lab_NewShift.Visible = dirExists && isIdleShift;
      this.lab_WarnSimulation.Visible = dirExists;
			if (dirExists)
				this.lab_Remember.Visible = !this.lab_Remember.Visible;
		}

		private void PrepareForSimulation_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			Frm_Confirm frm_conf = new Frm_Confirm();
      //agrassi_FI30-10-08
      //DialogResult res = frm_conf.ShowDialog(this, "Warning! Syncro will be closed, made a copy of the production data and then started again.");
      DialogResult res = frm_conf.ShowDialog(this, "Warning! A copy of the production data will be made and communication with office will be interrupted.");
      if (res == DialogResult.OK)
			{
				Logger.LogError(this, "Prepare for Simulation");
				Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = this.startPath + "StartEndSimulation.exe";

        //PMASSARO_FI09/02/09 - Added Logs Into StartEndSimulation
        /*
        process.StartInfo.Arguments = "start \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION + " " + TWIN400_TAG;
        */
        process.StartInfo.Arguments = "start \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION + " " + TWIN400_TAG + " " + SyncroC.Utility.Settings.getSettings().paths.Log;
        
				process.Start();
        //agrassi_FI30-10-08
        //this.CloseSyncroCompletely();
			}
		}

		private void TerminateSimulation_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			Frm_Confirm frm_conf = new Frm_Confirm();
			DialogResult res = frm_conf.ShowDialog(this, "Syncro will be closed, restored the production data and then started again.");
			if (res == DialogResult.OK)
			{
				Logger.LogError(this, "Terminate Simulation");
				Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = this.startPath + "StartEndSimulation.exe";

        //PMASSARO_FI09/02/09 - Added Logs Into StartEndSimulation
        /*
        process.StartInfo.Arguments = "end \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION;
        */
        process.StartInfo.Arguments = "end \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION + " " + TWIN400_TAG + " " + SyncroC.Utility.Settings.getSettings().paths.Log;
        
				process.Start();
				this.CloseSyncroCompletely();
			}
		}

		private void tim_Sim_Tick(object sender, EventArgs e)
		{
			this.CheckSimulationState();
		}
	}
}

