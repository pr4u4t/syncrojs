namespace SyncroC.Forms.Pages.Settings.Simulation
{
  partial class Frm_SimulationGeneral
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SimulationGeneral));
      this.Simulation = new SyncroC.Controls.StateButton();
      this.tnud_Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_SF1Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_SF2Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_SF3Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chk_SSIsRdyChange = new System.Windows.Forms.CheckBox();
      this.chk_KnifeHasChanged = new System.Windows.Forms.CheckBox();
      this.chk_SSHasChanged = new System.Windows.Forms.CheckBox();
      this.chk_RSHHasCut = new System.Windows.Forms.CheckBox();
      this.lab_WarnSimulation = new System.Windows.Forms.Label();
      this.PrepareForSimulation = new SyncroC.Controls.StateButton();
      this.TerminateSimulation = new SyncroC.Controls.StateButton();
      this.tim_Sim = new System.Windows.Forms.Timer(this.components);
      this.lab_Remember = new System.Windows.Forms.Label();
      this.lab_NewShift = new System.Windows.Forms.Label();
      this.lab_EnableSimulation = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      this.Simulation.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Simulation.ForeColor = System.Drawing.Color.Black;
      this.Simulation.FramePresent = true;
      this.Simulation.HighLightEnable = false;
      this.Simulation.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Simulation.ImagePassword = null;
      this.Simulation.Inemergency = false;
      this.Simulation.InLocal = false;
      this.Simulation.IsButton = true;
      this.Simulation.IsForceToInvisible = false;
      this.Simulation.Location = new System.Drawing.Point(948, 296);
      this.Simulation.Name = "Simulation";
      this.Simulation.OnLine = false;
      this.Simulation.ShowImage = false;
      this.Simulation.ShowText = true;
      this.Simulation.Size = new System.Drawing.Size(187, 140);
      this.Simulation.Switching = false;
      this.Simulation.TabIndex = 115;
      this.Simulation.Text = "Start Simulation";
      this.Simulation.TranslateText = false;
      this.Simulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Simulation.UserDefinedActiveImage")));
      this.Simulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Simulation.UserDefinedImage")));
      this.Simulation.Warned = false;
      this.Simulation.Click += new System.EventHandler(this.btn_Simulation_Click);
      // 
      // tnud_Speed
      // 
      this.tnud_Speed.DecimalPlaces = 0;
      this.tnud_Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_Speed.ImagePassword = null;
      this.tnud_Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Speed.Location = new System.Drawing.Point(81, 268);
      this.tnud_Speed.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
      this.tnud_Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Speed.Name = "tnud_Speed";
      this.tnud_Speed.ReadOnly = false;
      this.tnud_Speed.Size = new System.Drawing.Size(115, 58);
      this.tnud_Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Framed;
      this.tnud_Speed.TabIndex = 116;
      this.tnud_Speed.Text = "Line speed";
      this.tnud_Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed.TranslateText = false;
      this.tnud_Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Speed.Value = "0";
      this.tnud_Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_SF1Speed
      // 
      this.tnud_SF1Speed.DecimalPlaces = 0;
      this.tnud_SF1Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_SF1Speed.ImagePassword = null;
      this.tnud_SF1Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SF1Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SF1Speed.Location = new System.Drawing.Point(206, 268);
      this.tnud_SF1Speed.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
      this.tnud_SF1Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SF1Speed.Name = "tnud_SF1Speed";
      this.tnud_SF1Speed.ReadOnly = false;
      this.tnud_SF1Speed.Size = new System.Drawing.Size(115, 58);
      this.tnud_SF1Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Framed;
      this.tnud_SF1Speed.TabIndex = 118;
      this.tnud_SF1Speed.Text = "SF1 speed";
      this.tnud_SF1Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_SF1Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SF1Speed.TranslateText = false;
      this.tnud_SF1Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SF1Speed.Value = "0";
      this.tnud_SF1Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SF1Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_SF2Speed
      // 
      this.tnud_SF2Speed.DecimalPlaces = 0;
      this.tnud_SF2Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_SF2Speed.ImagePassword = null;
      this.tnud_SF2Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SF2Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SF2Speed.Location = new System.Drawing.Point(330, 268);
      this.tnud_SF2Speed.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
      this.tnud_SF2Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SF2Speed.Name = "tnud_SF2Speed";
      this.tnud_SF2Speed.ReadOnly = false;
      this.tnud_SF2Speed.Size = new System.Drawing.Size(115, 58);
      this.tnud_SF2Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Framed;
      this.tnud_SF2Speed.TabIndex = 119;
      this.tnud_SF2Speed.Text = "SF2 speed";
      this.tnud_SF2Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_SF2Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SF2Speed.TranslateText = false;
      this.tnud_SF2Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SF2Speed.Value = "0";
      this.tnud_SF2Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SF2Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_SF3Speed
      // 
      this.tnud_SF3Speed.DecimalPlaces = 0;
      this.tnud_SF3Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_SF3Speed.ImagePassword = null;
      this.tnud_SF3Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SF3Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SF3Speed.Location = new System.Drawing.Point(454, 268);
      this.tnud_SF3Speed.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
      this.tnud_SF3Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SF3Speed.Name = "tnud_SF3Speed";
      this.tnud_SF3Speed.ReadOnly = false;
      this.tnud_SF3Speed.Size = new System.Drawing.Size(115, 58);
      this.tnud_SF3Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Framed;
      this.tnud_SF3Speed.TabIndex = 120;
      this.tnud_SF3Speed.Text = "SF3 speed";
      this.tnud_SF3Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.tnud_SF3Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SF3Speed.TranslateText = false;
      this.tnud_SF3Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SF3Speed.Value = "0";
      this.tnud_SF3Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SF3Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chk_SSIsRdyChange);
      this.groupBox1.Controls.Add(this.chk_KnifeHasChanged);
      this.groupBox1.Controls.Add(this.chk_SSHasChanged);
      this.groupBox1.Controls.Add(this.chk_RSHHasCut);
      this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(81, 351);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(616, 214);
      this.groupBox1.TabIndex = 121;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Simulation Signals";
      // 
      // chk_SSIsRdyChange
      // 
      this.chk_SSIsRdyChange.AutoSize = true;
      this.chk_SSIsRdyChange.Location = new System.Drawing.Point(16, 25);
      this.chk_SSIsRdyChange.Name = "chk_SSIsRdyChange";
      this.chk_SSIsRdyChange.Size = new System.Drawing.Size(185, 23);
      this.chk_SSIsRdyChange.TabIndex = 127;
      this.chk_SSIsRdyChange.Text = "SS is Rdy to Change";
      this.chk_SSIsRdyChange.UseVisualStyleBackColor = true;
      // 
      // chk_KnifeHasChanged
      // 
      this.chk_KnifeHasChanged.AutoSize = true;
      this.chk_KnifeHasChanged.Location = new System.Drawing.Point(16, 116);
      this.chk_KnifeHasChanged.Name = "chk_KnifeHasChanged";
      this.chk_KnifeHasChanged.Size = new System.Drawing.Size(494, 23);
      this.chk_KnifeHasChanged.TabIndex = 126;
      this.chk_KnifeHasChanged.Text = "Knife Has Changed ( required when the signals are available )";
      this.chk_KnifeHasChanged.UseVisualStyleBackColor = true;
      // 
      // chk_SSHasChanged
      // 
      this.chk_SSHasChanged.AutoSize = true;
      this.chk_SSHasChanged.Location = new System.Drawing.Point(16, 87);
      this.chk_SSHasChanged.Name = "chk_SSHasChanged";
      this.chk_SSHasChanged.Size = new System.Drawing.Size(158, 23);
      this.chk_SSHasChanged.TabIndex = 125;
      this.chk_SSHasChanged.Text = "SS Has Changed";
      this.chk_SSHasChanged.UseVisualStyleBackColor = true;
      // 
      // chk_RSHHasCut
      // 
      this.chk_RSHHasCut.AutoSize = true;
      this.chk_RSHHasCut.Location = new System.Drawing.Point(16, 58);
      this.chk_RSHHasCut.Name = "chk_RSHHasCut";
      this.chk_RSHHasCut.Size = new System.Drawing.Size(128, 23);
      this.chk_RSHHasCut.TabIndex = 124;
      this.chk_RSHHasCut.Text = "RSH Has Cut";
      this.chk_RSHHasCut.UseVisualStyleBackColor = true;
      // 
      // lab_WarnSimulation
      // 
      this.lab_WarnSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_WarnSimulation.ForeColor = System.Drawing.Color.Red;
      this.lab_WarnSimulation.Location = new System.Drawing.Point(766, 450);
      this.lab_WarnSimulation.Name = "lab_WarnSimulation";
      this.lab_WarnSimulation.Size = new System.Drawing.Size(347, 52);
      this.lab_WarnSimulation.TabIndex = 122;
      this.lab_WarnSimulation.Text = "WARNING !  The communication with Office is disabled  ! ";
      this.lab_WarnSimulation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_WarnSimulation.Visible = false;
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
      this.PrepareForSimulation.Location = new System.Drawing.Point(748, 296);
      this.PrepareForSimulation.Name = "PrepareForSimulation";
      this.PrepareForSimulation.OnLine = false;
      this.PrepareForSimulation.ShowImage = false;
      this.PrepareForSimulation.ShowText = true;
      this.PrepareForSimulation.Size = new System.Drawing.Size(185, 70);
      this.PrepareForSimulation.Switching = false;
      this.PrepareForSimulation.TabIndex = 123;
      this.PrepareForSimulation.Text = "Prepare For Simulation";
      this.PrepareForSimulation.TranslateText = false;
      this.PrepareForSimulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("PrepareForSimulation.UserDefinedActiveImage")));
      this.PrepareForSimulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("PrepareForSimulation.UserDefinedImage")));
      this.PrepareForSimulation.Warned = false;
      this.PrepareForSimulation.Click += new System.EventHandler(this.PrepareForSimulation_Click);
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
      this.TerminateSimulation.Location = new System.Drawing.Point(748, 366);
      this.TerminateSimulation.Name = "TerminateSimulation";
      this.TerminateSimulation.OnLine = false;
      this.TerminateSimulation.ShowImage = false;
      this.TerminateSimulation.ShowText = true;
      this.TerminateSimulation.Size = new System.Drawing.Size(185, 70);
      this.TerminateSimulation.Switching = false;
      this.TerminateSimulation.TabIndex = 124;
      this.TerminateSimulation.Text = "Terminate Simulation";
      this.TerminateSimulation.TranslateText = false;
      this.TerminateSimulation.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TerminateSimulation.UserDefinedActiveImage")));
      this.TerminateSimulation.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TerminateSimulation.UserDefinedImage")));
      this.TerminateSimulation.Warned = false;
      this.TerminateSimulation.Click += new System.EventHandler(this.TerminateSimulation_Click);
      // 
      // tim_Sim
      // 
      this.tim_Sim.Enabled = true;
      this.tim_Sim.Interval = 1000;
      this.tim_Sim.Tick += new System.EventHandler(this.tim_Sim_Tick);
      // 
      // lab_Remember
      // 
      this.lab_Remember.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_Remember.ForeColor = System.Drawing.Color.Red;
      this.lab_Remember.Location = new System.Drawing.Point(684, 564);
      this.lab_Remember.Name = "lab_Remember";
      this.lab_Remember.Size = new System.Drawing.Size(516, 52);
      this.lab_Remember.TabIndex = 125;
      this.lab_Remember.Text = "REMEMBER TO PRESS \'Terminate Simulation\' AT THE END !";
      this.lab_Remember.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lab_NewShift
      // 
      this.lab_NewShift.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_NewShift.ForeColor = System.Drawing.Color.Blue;
      this.lab_NewShift.Location = new System.Drawing.Point(781, 509);
      this.lab_NewShift.Name = "lab_NewShift";
      this.lab_NewShift.Size = new System.Drawing.Size(347, 52);
      this.lab_NewShift.TabIndex = 126;
      this.lab_NewShift.Text = "Start a new shift to have Syncro completely enabled.";
      this.lab_NewShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_NewShift.Visible = false;
      // 
      // lab_EnableSimulation
      // 
      this.lab_EnableSimulation.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_EnableSimulation.ForeColor = System.Drawing.Color.Blue;
      this.lab_EnableSimulation.Location = new System.Drawing.Point(723, 243);
      this.lab_EnableSimulation.Name = "lab_EnableSimulation";
      this.lab_EnableSimulation.Size = new System.Drawing.Size(426, 54);
      this.lab_EnableSimulation.TabIndex = 127;
      this.lab_EnableSimulation.Text = "To enable simulation, Syncro must be in idle shift !";
      this.lab_EnableSimulation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_EnableSimulation.Visible = false;
      // 
      // Frm_SimulationGeneral
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lab_EnableSimulation);
      this.Controls.Add(this.lab_NewShift);
      this.Controls.Add(this.lab_Remember);
      this.Controls.Add(this.TerminateSimulation);
      this.Controls.Add(this.PrepareForSimulation);
      this.Controls.Add(this.lab_WarnSimulation);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.tnud_SF3Speed);
      this.Controls.Add(this.tnud_SF2Speed);
      this.Controls.Add(this.tnud_SF1Speed);
      this.Controls.Add(this.tnud_Speed);
      this.Controls.Add(this.Simulation);
      this.Name = "Frm_SimulationGeneral";
      this.Text = "Frm_SimulationGeneral";
      this.Load += new System.EventHandler(this.Frm_SimulationGeneral_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.Simulation, 0);
      this.Controls.SetChildIndex(this.tnud_Speed, 0);
      this.Controls.SetChildIndex(this.tnud_SF1Speed, 0);
      this.Controls.SetChildIndex(this.tnud_SF2Speed, 0);
      this.Controls.SetChildIndex(this.tnud_SF3Speed, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.lab_WarnSimulation, 0);
      this.Controls.SetChildIndex(this.PrepareForSimulation, 0);
      this.Controls.SetChildIndex(this.TerminateSimulation, 0);
      this.Controls.SetChildIndex(this.lab_Remember, 0);
      this.Controls.SetChildIndex(this.lab_NewShift, 0);
      this.Controls.SetChildIndex(this.lab_EnableSimulation, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private SyncroC.Controls.StateButton Simulation;
    private SyncroC.Controls.TitledNumericUpDown tnud_Speed;
    private SyncroC.Controls.TitledNumericUpDown tnud_SF1Speed;
    private SyncroC.Controls.TitledNumericUpDown tnud_SF2Speed;
    private SyncroC.Controls.TitledNumericUpDown tnud_SF3Speed;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chk_SSHasChanged;
		private System.Windows.Forms.CheckBox chk_RSHHasCut;
		private System.Windows.Forms.CheckBox chk_KnifeHasChanged;
		private System.Windows.Forms.CheckBox chk_SSIsRdyChange;
    private System.Windows.Forms.Label lab_WarnSimulation;
    private SyncroC.Controls.StateButton PrepareForSimulation;
    private SyncroC.Controls.StateButton TerminateSimulation;
    private System.Windows.Forms.Timer tim_Sim;
    private System.Windows.Forms.Label lab_Remember;
    private System.Windows.Forms.Label lab_NewShift;
    private System.Windows.Forms.Label lab_EnableSimulation;
  }
}