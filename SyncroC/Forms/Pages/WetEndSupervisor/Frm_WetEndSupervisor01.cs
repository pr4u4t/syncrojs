using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Forms.Pages.SingleFacer.Status;
using SyncroC.Forms.Pages.GlueUnit;
using SyncroC.Forms.Pages.PreHeaterTern;
//RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
using SyncroC.Forms.Pages.DoubleBacker.Status;

namespace SyncroC.Forms.Pages.WetEndSupervisor
{
  public class Frm_WetEndSupervisor01 : SyncroC.Forms.Pages.WetEndSupervisor.Frm_WetEndSupervisorGroup
  {
    private Label lbl_SF2;
    private SyncroC.Controls.LowLevelControl gg_glueGap21;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater14;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater24;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater25;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater15;
    private Label lbl_SF1;
    private SyncroC.Controls.TitledNumericUpDown tnud_SpeedSF2_UVmm_m;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater11;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater1;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater2;
    private SyncroC.Controls.TitledNumericUpDown tnud_SpeedSF1_UVmm_m;
    private SyncroC.Controls.LowLevelControl gg_glueGap11;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater12;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater22;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater23;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater13;
    private System.ComponentModel.IContainer components = null;
    private Panel panel1;
    private Panel panel2;
    private Panel panel3;
    private PictureBox pct_sf1;
    private PictureBox pct_sf2;
    private PictureBox pictureBox1;
    private SyncroC.Controls.TitledNumericUpDown tnud_SpeedGlueUnit_UVmm_m;
    private SyncroC.Controls.LowLevelControl gg_glueGap1;
    private SyncroC.Controls.LowLevelControl gg_glueGap2;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater20;
    private Panel panel4;
    private PictureBox pictureBox2;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater3;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater30;
    private SyncroC.Controls.LowLevelControl gg_glueGap3;
    private Panel panel5;
    private PictureBox pct_sf3;
    private SyncroC.Controls.LowLevelControl gg_glueGap31;
    private SyncroC.Controls.TitledNumericUpDown tnud_SpeedSF3_UVmm_m;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater27;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater26;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater16;
    private Label lbl_SF3;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater17;
    private SyncroC.Controls.WrapArm wa_wrapArmPreHeater31;
    private SyncroC.Controls.TitledNumericUpDown tnud_SpeedDoubleBacker_UVmm_m;
    //RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).

    public Frm_WetEndSupervisor01()
    {
      InitializeComponent();

      //PMASSARO_FI28/10/08 - Customer Personalization SAECO 3663-64 Adjusted Wrap Arm Visualization On SF
      if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3664")
      {
        this.wa_wrapArmPreHeater13.Location = new System.Drawing.Point(10, 379);
        this.wa_wrapArmPreHeater15.Location = new System.Drawing.Point(10, 379);

        this.wa_wrapArmPreHeater23.Location = new System.Drawing.Point(10, 99);
        this.wa_wrapArmPreHeater25.Location = new System.Drawing.Point(10, 99);
      }

      this.wa_wrapArmPreHeater1.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater11.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater12.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater13.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater14.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater15.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater2.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater20.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater22.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater23.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater24.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater25.setDesignModePage(this.DesignMode);
      //RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
      this.wa_wrapArmPreHeater31.setDesignModePage(this.DesignMode);

      this.gg_glueGap1.setDesignModePage(this.DesignMode);
      this.gg_glueGap11.setDesignModePage(this.DesignMode);
      this.gg_glueGap2.setDesignModePage(this.DesignMode);
      this.gg_glueGap21.setDesignModePage(this.DesignMode);

      this.gg_glueGap1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MinVal;
			this.gg_glueGap1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MaxVal;
			this.gg_glueGap2.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MinVal;
			this.gg_glueGap2.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MaxVal;
			this.gg_glueGap11.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).Gluegap1.GlueGapRegulation.MinVal;
			this.gg_glueGap11.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).Gluegap1.GlueGapRegulation.MaxVal;
			this.gg_glueGap21.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).Gluegap1.GlueGapRegulation.MinVal;
			this.gg_glueGap21.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).Gluegap1.GlueGapRegulation.MaxVal;

      //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
      this.gg_glueGap3.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.GlueGapRegulation.MinVal;
      this.gg_glueGap3.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.GlueGapRegulation.MaxVal;
      this.gg_glueGap31.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(3).Gluegap1.GlueGapRegulation.MinVal;
      this.gg_glueGap31.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(3).Gluegap1.GlueGapRegulation.MaxVal;
      this.wa_wrapArmPreHeater3.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater30.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater26.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater27.setDesignModePage(this.DesignMode); 
      this.wa_wrapArmPreHeater26.setDesignModePage(this.DesignMode);
      this.wa_wrapArmPreHeater27.setDesignModePage(this.DesignMode);

      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.wa_wrapArmPreHeater11.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater1.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater2.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater20.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater12.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater13.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater22.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater23.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater14.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater15.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater24.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater25.LowLevelEnabled = false;
        //RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
        this.wa_wrapArmPreHeater31.LowLevelEnabled = false;

        this.gg_glueGap1.LowLevelEnabled = false;
        this.gg_glueGap2.LowLevelEnabled = false;
        this.gg_glueGap11.LowLevelEnabled = false;
        this.gg_glueGap21.LowLevelEnabled = false;

        //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
        this.wa_wrapArmPreHeater16.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater17.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater26.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater27.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater3.LowLevelEnabled = false;
        this.wa_wrapArmPreHeater30.LowLevelEnabled = false;
        this.gg_glueGap3.LowLevelEnabled = false;
        this.gg_glueGap31.LowLevelEnabled = false;
      }

      //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
      if (!DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        this.panel5.Visible = false;

        //Ridisegno la grafica con le dimensioni precedenti all'introduzione della terza zona
        Size oldLowLevelControlSize = new Size(133, 135);

        //Zona DB
        this.panel4.Size = new Size(149, 519);
        this.panel4.Location = new Point(1027, 194);

        this.pictureBox2.Size = new Size(108, 36);
        this.pictureBox2.Location = new Point(22, 11);

        this.tnud_SpeedDoubleBacker_UVmm_m.Size = new Size(133, 40);
        this.tnud_SpeedDoubleBacker_UVmm_m.Location = new Point(8, 53);

        this.wa_wrapArmPreHeater31.Size = oldLowLevelControlSize;
        this.wa_wrapArmPreHeater31.Location = new Point(8, 379);

        //Zona Terna e Crest
        this.panel3.Size = new Size(302, 519);
        this.panel3.Location = new Point(703, 194);

        this.pictureBox1.Size = new Size(115, 48);
        this.pictureBox1.Location = new Point(95, 0);

        this.tnud_SpeedGlueUnit_UVmm_m.Size = new Size(284, 40);
        this.tnud_SpeedGlueUnit_UVmm_m.Location = new Point(282, 40);

        this.wa_wrapArmPreHeater11.Size = this.wa_wrapArmPreHeater1.Size = this.wa_wrapArmPreHeater2.Size = this.wa_wrapArmPreHeater20.Size = oldLowLevelControlSize;
        this.wa_wrapArmPreHeater11.Location = new Point(10, 379);
        this.wa_wrapArmPreHeater1.Location = new Point(10, 239);
        this.wa_wrapArmPreHeater2.Location = new Point(10, 99);
        this.wa_wrapArmPreHeater20.Location = new Point(149, 99);

        this.gg_glueGap1.Size = this.gg_glueGap2.Size = oldLowLevelControlSize;
        this.gg_glueGap1.Location = new Point(161, this.wa_wrapArmPreHeater11.Location.Y);
        this.gg_glueGap2.Location = new Point(161, this.wa_wrapArmPreHeater1.Location.Y);

        //Zona SF1
        this.panel2.Size = new Size(300, 519);
        this.panel2.Location = new Point(381, 194);

        this.pct_sf1.Size = new Size(115, 48);
        this.pct_sf1.Location = new Point(101, 0);

        this.tnud_SpeedSF1_UVmm_m.Size = new Size(282, 40);
        this.tnud_SpeedSF1_UVmm_m.Location = new Point(10, 53);

        this.wa_wrapArmPreHeater12.Size = this.wa_wrapArmPreHeater13.Size = this.wa_wrapArmPreHeater22.Size = this.wa_wrapArmPreHeater23.Size = oldLowLevelControlSize;
        this.wa_wrapArmPreHeater12.Location = new Point(159, 99);
        this.wa_wrapArmPreHeater13.Location = new Point(10, 99);
        this.wa_wrapArmPreHeater22.Location = new Point(159, 379);
        this.wa_wrapArmPreHeater23.Location = new Point(10, 379);

        this.gg_glueGap11.Size = oldLowLevelControlSize;
        this.gg_glueGap11.Location = new Point(86, 239);

        //Zona SF2
        this.panel1.Size = this.panel2.Size;
        this.panel1.Location = new Point(51, 194);

        this.pct_sf2.Size = this.pct_sf1.Size;
        this.pct_sf2.Location = new Point(101, 0);

        this.tnud_SpeedSF2_UVmm_m.Size = this.tnud_SpeedSF1_UVmm_m.Size;
        this.tnud_SpeedSF2_UVmm_m.Location = new Point(10, 53);

        this.wa_wrapArmPreHeater14.Size = this.wa_wrapArmPreHeater15.Size = this.wa_wrapArmPreHeater24.Size = this.wa_wrapArmPreHeater25.Size = oldLowLevelControlSize;
        this.wa_wrapArmPreHeater14.Location = new Point(159, 99);
        this.wa_wrapArmPreHeater15.Location = new Point(10, 99);
        this.wa_wrapArmPreHeater24.Location = new Point(159, 379);
        this.wa_wrapArmPreHeater25.Location = new Point(10, 379);

        this.gg_glueGap21.Size = oldLowLevelControlSize;
        this.gg_glueGap21.Location = new Point(86, 239);

      }
    }

    public override void _Show()
    {
      base._Show();

      //ABELLO_FI11/02/15_2 Gestita un pò meglio la configurazione con un solo SingleFacer
      if (!DBManager.getDBManager().lineConfig.SingleFacer1)
      {
        this.panel2.Visible = false;
      }

      if (!DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        this.panel1.Visible = false;
      }

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_WetEndSupervisor01));
      this.lbl_SF2 = new System.Windows.Forms.Label();
      this.gg_glueGap21 = new SyncroC.Controls.LowLevelControl();
      this.wa_wrapArmPreHeater14 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater24 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater25 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater15 = new SyncroC.Controls.WrapArm();
      this.lbl_SF1 = new System.Windows.Forms.Label();
      this.tnud_SpeedSF2_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.wa_wrapArmPreHeater11 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater1 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater2 = new SyncroC.Controls.WrapArm();
      this.tnud_SpeedSF1_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.gg_glueGap11 = new SyncroC.Controls.LowLevelControl();
      this.wa_wrapArmPreHeater12 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater22 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater23 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater13 = new SyncroC.Controls.WrapArm();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pct_sf2 = new System.Windows.Forms.PictureBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.pct_sf1 = new System.Windows.Forms.PictureBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.wa_wrapArmPreHeater30 = new SyncroC.Controls.WrapArm();
      this.gg_glueGap3 = new SyncroC.Controls.LowLevelControl();
      this.wa_wrapArmPreHeater3 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater20 = new SyncroC.Controls.WrapArm();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.tnud_SpeedGlueUnit_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.gg_glueGap1 = new SyncroC.Controls.LowLevelControl();
      this.gg_glueGap2 = new SyncroC.Controls.LowLevelControl();
      this.panel4 = new System.Windows.Forms.Panel();
      this.wa_wrapArmPreHeater31 = new SyncroC.Controls.WrapArm();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.tnud_SpeedDoubleBacker_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.panel5 = new System.Windows.Forms.Panel();
      this.pct_sf3 = new System.Windows.Forms.PictureBox();
      this.gg_glueGap31 = new SyncroC.Controls.LowLevelControl();
      this.tnud_SpeedSF3_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.wa_wrapArmPreHeater27 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater26 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater16 = new SyncroC.Controls.WrapArm();
      this.lbl_SF3 = new System.Windows.Forms.Label();
      this.wa_wrapArmPreHeater17 = new SyncroC.Controls.WrapArm();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_sf2)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_sf1)).BeginInit();
      this.panel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.panel5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_sf3)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // lbl_SF2
      // 
      this.lbl_SF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
      this.lbl_SF2.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SF2.Location = new System.Drawing.Point(196, 0);
      this.lbl_SF2.Name = "lbl_SF2";
      this.lbl_SF2.Size = new System.Drawing.Size(48, 37);
      this.lbl_SF2.TabIndex = 111;
      this.lbl_SF2.Text = "2";
      this.lbl_SF2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // gg_glueGap21
      // 
      this.gg_glueGap21.Auto = false;
      this.gg_glueGap21.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap21.InLocal = false;
      this.gg_glueGap21.IsMaxPos = false;
      this.gg_glueGap21.IsMinPos = false;
      this.gg_glueGap21.Location = new System.Drawing.Point(79, 253);
      this.gg_glueGap21.LowLevelControlName = "1";
      this.gg_glueGap21.LowLevelEnabled = true;
      this.gg_glueGap21.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap21.MaxSetPointValue = 100;
      this.gg_glueGap21.MinSetPointValue = 0;
      this.gg_glueGap21.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap21.Name = "gg_glueGap21";
      this.gg_glueGap21.Position = "0";
      this.gg_glueGap21.Size = new System.Drawing.Size(100, 102);
      this.gg_glueGap21.TabIndex = 229;
      this.gg_glueGap21.TranslateText = false;
      this.gg_glueGap21.Visible = false;
      this.gg_glueGap21.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.gg_glueGap21_OnGlueGapButtonPressed);
      // 
      // wa_wrapArmPreHeater14
      // 
      this.wa_wrapArmPreHeater14.Auto = false;
      this.wa_wrapArmPreHeater14.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater14.InLocal = false;
      this.wa_wrapArmPreHeater14.IsMaxPos = false;
      this.wa_wrapArmPreHeater14.IsMinPos = false;
      this.wa_wrapArmPreHeater14.IsUnder = false;
      this.wa_wrapArmPreHeater14.Location = new System.Drawing.Point(143, 98);
      this.wa_wrapArmPreHeater14.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater14.Name = "wa_wrapArmPreHeater14";
      this.wa_wrapArmPreHeater14.Position = "0";
      this.wa_wrapArmPreHeater14.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater14.TabIndex = 228;
      this.wa_wrapArmPreHeater14.Temperature = 0;
      this.wa_wrapArmPreHeater14.TranslateText = false;
      this.wa_wrapArmPreHeater14.Visible = false;
      this.wa_wrapArmPreHeater14.WrapName = "Em";
      this.wa_wrapArmPreHeater14.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater14_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater24
      // 
      this.wa_wrapArmPreHeater24.Auto = false;
      this.wa_wrapArmPreHeater24.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater24.InLocal = false;
      this.wa_wrapArmPreHeater24.IsMaxPos = false;
      this.wa_wrapArmPreHeater24.IsMinPos = false;
      this.wa_wrapArmPreHeater24.IsUnder = false;
      this.wa_wrapArmPreHeater24.Location = new System.Drawing.Point(143, 412);
      this.wa_wrapArmPreHeater24.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater24.Name = "wa_wrapArmPreHeater24";
      this.wa_wrapArmPreHeater24.Position = "0";
      this.wa_wrapArmPreHeater24.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater24.TabIndex = 227;
      this.wa_wrapArmPreHeater24.Temperature = 0;
      this.wa_wrapArmPreHeater24.TranslateText = false;
      this.wa_wrapArmPreHeater24.Visible = false;
      this.wa_wrapArmPreHeater24.WrapName = "Im";
      this.wa_wrapArmPreHeater24.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater24_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater25
      // 
      this.wa_wrapArmPreHeater25.Auto = false;
      this.wa_wrapArmPreHeater25.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater25.InLocal = false;
      this.wa_wrapArmPreHeater25.IsMaxPos = false;
      this.wa_wrapArmPreHeater25.IsMinPos = false;
      this.wa_wrapArmPreHeater25.IsUnder = false;
      this.wa_wrapArmPreHeater25.Location = new System.Drawing.Point(10, 412);
      this.wa_wrapArmPreHeater25.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater25.Name = "wa_wrapArmPreHeater25";
      this.wa_wrapArmPreHeater25.Position = "0";
      this.wa_wrapArmPreHeater25.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater25.TabIndex = 226;
      this.wa_wrapArmPreHeater25.Temperature = 0;
      this.wa_wrapArmPreHeater25.TranslateText = false;
      this.wa_wrapArmPreHeater25.Visible = false;
      this.wa_wrapArmPreHeater25.WrapName = "Il";
      this.wa_wrapArmPreHeater25.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater25_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater15
      // 
      this.wa_wrapArmPreHeater15.Auto = false;
      this.wa_wrapArmPreHeater15.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater15.InLocal = false;
      this.wa_wrapArmPreHeater15.IsMaxPos = false;
      this.wa_wrapArmPreHeater15.IsMinPos = false;
      this.wa_wrapArmPreHeater15.IsUnder = false;
      this.wa_wrapArmPreHeater15.Location = new System.Drawing.Point(10, 98);
      this.wa_wrapArmPreHeater15.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater15.Name = "wa_wrapArmPreHeater15";
      this.wa_wrapArmPreHeater15.Position = "0";
      this.wa_wrapArmPreHeater15.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater15.TabIndex = 225;
      this.wa_wrapArmPreHeater15.Temperature = 0;
      this.wa_wrapArmPreHeater15.TranslateText = false;
      this.wa_wrapArmPreHeater15.Visible = false;
      this.wa_wrapArmPreHeater15.WrapName = "El";
      this.wa_wrapArmPreHeater15.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater15_OnWrapArmButtonPressed);
      // 
      // lbl_SF1
      // 
      this.lbl_SF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
      this.lbl_SF1.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SF1.Location = new System.Drawing.Point(196, 0);
      this.lbl_SF1.Name = "lbl_SF1";
      this.lbl_SF1.Size = new System.Drawing.Size(48, 37);
      this.lbl_SF1.TabIndex = 231;
      this.lbl_SF1.Text = "1";
      this.lbl_SF1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tnud_SpeedSF2_UVmm_m
      // 
      this.tnud_SpeedSF2_UVmm_m.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tnud_SpeedSF2_UVmm_m.DecimalPlaces = 0;
      this.tnud_SpeedSF2_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SpeedSF2_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSF2_UVmm_m.ImagePassword = null;
      this.tnud_SpeedSF2_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SpeedSF2_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedSF2_UVmm_m.Location = new System.Drawing.Point(10, 40);
      this.tnud_SpeedSF2_UVmm_m.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.tnud_SpeedSF2_UVmm_m.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SpeedSF2_UVmm_m.Name = "tnud_SpeedSF2_UVmm_m";
      this.tnud_SpeedSF2_UVmm_m.ReadOnly = true;
      this.tnud_SpeedSF2_UVmm_m.Size = new System.Drawing.Size(233, 40);
      this.tnud_SpeedSF2_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_SpeedSF2_UVmm_m.TabIndex = 232;
      this.tnud_SpeedSF2_UVmm_m.TabStop = false;
      this.tnud_SpeedSF2_UVmm_m.Text = "Speed";
      this.tnud_SpeedSF2_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSF2_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSF2_UVmm_m.TranslateText = false;
      this.tnud_SpeedSF2_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SpeedSF2_UVmm_m.UnitMeasureVisible = false;
      this.tnud_SpeedSF2_UVmm_m.Value = "400000";
      this.tnud_SpeedSF2_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSF2_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_SpeedSF2_UVmm_m.Visible = false;
      // 
      // wa_wrapArmPreHeater11
      // 
      this.wa_wrapArmPreHeater11.Auto = false;
      this.wa_wrapArmPreHeater11.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater11.InLocal = false;
      this.wa_wrapArmPreHeater11.IsMaxPos = false;
      this.wa_wrapArmPreHeater11.IsMinPos = false;
      this.wa_wrapArmPreHeater11.IsUnder = false;
      this.wa_wrapArmPreHeater11.Location = new System.Drawing.Point(8, 412);
      this.wa_wrapArmPreHeater11.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater11.Name = "wa_wrapArmPreHeater11";
      this.wa_wrapArmPreHeater11.Position = "0";
      this.wa_wrapArmPreHeater11.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater11.TabIndex = 243;
      this.wa_wrapArmPreHeater11.Temperature = 0;
      this.wa_wrapArmPreHeater11.TranslateText = false;
      this.wa_wrapArmPreHeater11.Visible = false;
      this.wa_wrapArmPreHeater11.WrapName = "11";
      this.wa_wrapArmPreHeater11.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.OnPreHeatersTernWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater1
      // 
      this.wa_wrapArmPreHeater1.Auto = false;
      this.wa_wrapArmPreHeater1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater1.InLocal = false;
      this.wa_wrapArmPreHeater1.IsMaxPos = false;
      this.wa_wrapArmPreHeater1.IsMinPos = false;
      this.wa_wrapArmPreHeater1.IsUnder = false;
      this.wa_wrapArmPreHeater1.Location = new System.Drawing.Point(8, 304);
      this.wa_wrapArmPreHeater1.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater1.Name = "wa_wrapArmPreHeater1";
      this.wa_wrapArmPreHeater1.Position = "0";
      this.wa_wrapArmPreHeater1.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater1.TabIndex = 242;
      this.wa_wrapArmPreHeater1.Temperature = 0;
      this.wa_wrapArmPreHeater1.TranslateText = false;
      this.wa_wrapArmPreHeater1.Visible = false;
      this.wa_wrapArmPreHeater1.WrapName = "1";
      this.wa_wrapArmPreHeater1.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.OnPreHeatersTernWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater2
      // 
      this.wa_wrapArmPreHeater2.Auto = false;
      this.wa_wrapArmPreHeater2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater2.InLocal = false;
      this.wa_wrapArmPreHeater2.IsMaxPos = false;
      this.wa_wrapArmPreHeater2.IsMinPos = false;
      this.wa_wrapArmPreHeater2.IsUnder = false;
      this.wa_wrapArmPreHeater2.Location = new System.Drawing.Point(8, 196);
      this.wa_wrapArmPreHeater2.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater2.Name = "wa_wrapArmPreHeater2";
      this.wa_wrapArmPreHeater2.Position = "0";
      this.wa_wrapArmPreHeater2.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater2.TabIndex = 241;
      this.wa_wrapArmPreHeater2.Temperature = 0;
      this.wa_wrapArmPreHeater2.TranslateText = false;
      this.wa_wrapArmPreHeater2.Visible = false;
      this.wa_wrapArmPreHeater2.WrapName = "2";
      this.wa_wrapArmPreHeater2.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.OnPreHeatersTernWrapArmButtonPressed);
      // 
      // tnud_SpeedSF1_UVmm_m
      // 
      this.tnud_SpeedSF1_UVmm_m.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tnud_SpeedSF1_UVmm_m.DecimalPlaces = 0;
      this.tnud_SpeedSF1_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SpeedSF1_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSF1_UVmm_m.ImagePassword = null;
      this.tnud_SpeedSF1_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SpeedSF1_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedSF1_UVmm_m.Location = new System.Drawing.Point(10, 40);
      this.tnud_SpeedSF1_UVmm_m.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.tnud_SpeedSF1_UVmm_m.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SpeedSF1_UVmm_m.Name = "tnud_SpeedSF1_UVmm_m";
      this.tnud_SpeedSF1_UVmm_m.ReadOnly = true;
      this.tnud_SpeedSF1_UVmm_m.Size = new System.Drawing.Size(234, 40);
      this.tnud_SpeedSF1_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_SpeedSF1_UVmm_m.TabIndex = 249;
      this.tnud_SpeedSF1_UVmm_m.TabStop = false;
      this.tnud_SpeedSF1_UVmm_m.Text = "Speed";
      this.tnud_SpeedSF1_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSF1_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSF1_UVmm_m.TranslateText = false;
      this.tnud_SpeedSF1_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SpeedSF1_UVmm_m.UnitMeasureVisible = false;
      this.tnud_SpeedSF1_UVmm_m.Value = "400000";
      this.tnud_SpeedSF1_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSF1_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_SpeedSF1_UVmm_m.Visible = false;
      // 
      // gg_glueGap11
      // 
      this.gg_glueGap11.Auto = false;
      this.gg_glueGap11.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap11.InLocal = false;
      this.gg_glueGap11.IsMaxPos = false;
      this.gg_glueGap11.IsMinPos = false;
      this.gg_glueGap11.Location = new System.Drawing.Point(79, 253);
      this.gg_glueGap11.LowLevelControlName = "1";
      this.gg_glueGap11.LowLevelEnabled = true;
      this.gg_glueGap11.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap11.MaxSetPointValue = 100;
      this.gg_glueGap11.MinSetPointValue = 0;
      this.gg_glueGap11.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap11.Name = "gg_glueGap11";
      this.gg_glueGap11.Position = "0";
      this.gg_glueGap11.Size = new System.Drawing.Size(100, 102);
      this.gg_glueGap11.TabIndex = 248;
      this.gg_glueGap11.TranslateText = false;
      this.gg_glueGap11.Visible = false;
      this.gg_glueGap11.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.gg_glueGap11_OnGlueGapButtonPressed);
      // 
      // wa_wrapArmPreHeater12
      // 
      this.wa_wrapArmPreHeater12.Auto = false;
      this.wa_wrapArmPreHeater12.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater12.InLocal = false;
      this.wa_wrapArmPreHeater12.IsMaxPos = false;
      this.wa_wrapArmPreHeater12.IsMinPos = false;
      this.wa_wrapArmPreHeater12.IsUnder = false;
      this.wa_wrapArmPreHeater12.Location = new System.Drawing.Point(144, 98);
      this.wa_wrapArmPreHeater12.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater12.Name = "wa_wrapArmPreHeater12";
      this.wa_wrapArmPreHeater12.Position = "0";
      this.wa_wrapArmPreHeater12.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater12.TabIndex = 247;
      this.wa_wrapArmPreHeater12.Temperature = 0;
      this.wa_wrapArmPreHeater12.TranslateText = false;
      this.wa_wrapArmPreHeater12.Visible = false;
      this.wa_wrapArmPreHeater12.WrapName = "Em";
      this.wa_wrapArmPreHeater12.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater12_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater22
      // 
      this.wa_wrapArmPreHeater22.Auto = false;
      this.wa_wrapArmPreHeater22.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater22.InLocal = false;
      this.wa_wrapArmPreHeater22.IsMaxPos = false;
      this.wa_wrapArmPreHeater22.IsMinPos = false;
      this.wa_wrapArmPreHeater22.IsUnder = false;
      this.wa_wrapArmPreHeater22.Location = new System.Drawing.Point(144, 412);
      this.wa_wrapArmPreHeater22.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater22.Name = "wa_wrapArmPreHeater22";
      this.wa_wrapArmPreHeater22.Position = "0";
      this.wa_wrapArmPreHeater22.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater22.TabIndex = 246;
      this.wa_wrapArmPreHeater22.Temperature = 0;
      this.wa_wrapArmPreHeater22.TranslateText = false;
      this.wa_wrapArmPreHeater22.Visible = false;
      this.wa_wrapArmPreHeater22.WrapName = "Im";
      this.wa_wrapArmPreHeater22.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater22_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater23
      // 
      this.wa_wrapArmPreHeater23.Auto = false;
      this.wa_wrapArmPreHeater23.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater23.InLocal = false;
      this.wa_wrapArmPreHeater23.IsMaxPos = false;
      this.wa_wrapArmPreHeater23.IsMinPos = false;
      this.wa_wrapArmPreHeater23.IsUnder = false;
      this.wa_wrapArmPreHeater23.Location = new System.Drawing.Point(10, 412);
      this.wa_wrapArmPreHeater23.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater23.Name = "wa_wrapArmPreHeater23";
      this.wa_wrapArmPreHeater23.Position = "0";
      this.wa_wrapArmPreHeater23.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater23.TabIndex = 245;
      this.wa_wrapArmPreHeater23.Temperature = 0;
      this.wa_wrapArmPreHeater23.TranslateText = false;
      this.wa_wrapArmPreHeater23.Visible = false;
      this.wa_wrapArmPreHeater23.WrapName = "Il";
      this.wa_wrapArmPreHeater23.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater23_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater13
      // 
      this.wa_wrapArmPreHeater13.Auto = false;
      this.wa_wrapArmPreHeater13.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater13.InLocal = false;
      this.wa_wrapArmPreHeater13.IsMaxPos = false;
      this.wa_wrapArmPreHeater13.IsMinPos = false;
      this.wa_wrapArmPreHeater13.IsUnder = false;
      this.wa_wrapArmPreHeater13.Location = new System.Drawing.Point(10, 99);
      this.wa_wrapArmPreHeater13.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater13.Name = "wa_wrapArmPreHeater13";
      this.wa_wrapArmPreHeater13.Position = "0";
      this.wa_wrapArmPreHeater13.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater13.TabIndex = 244;
      this.wa_wrapArmPreHeater13.Temperature = 0;
      this.wa_wrapArmPreHeater13.TranslateText = false;
      this.wa_wrapArmPreHeater13.Visible = false;
      this.wa_wrapArmPreHeater13.WrapName = "El";
      this.wa_wrapArmPreHeater13.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater13_OnWrapArmButtonPressed);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
      this.panel1.Controls.Add(this.pct_sf2);
      this.panel1.Controls.Add(this.tnud_SpeedSF2_UVmm_m);
      this.panel1.Controls.Add(this.lbl_SF2);
      this.panel1.Controls.Add(this.wa_wrapArmPreHeater15);
      this.panel1.Controls.Add(this.wa_wrapArmPreHeater14);
      this.panel1.Controls.Add(this.gg_glueGap21);
      this.panel1.Controls.Add(this.wa_wrapArmPreHeater25);
      this.panel1.Controls.Add(this.wa_wrapArmPreHeater24);
      this.panel1.Location = new System.Drawing.Point(299, 194);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(253, 519);
      this.panel1.TabIndex = 253;
      // 
      // pct_sf2
      // 
      this.pct_sf2.Image = ((System.Drawing.Image)(resources.GetObject("pct_sf2.Image")));
      this.pct_sf2.Location = new System.Drawing.Point(81, 0);
      this.pct_sf2.Name = "pct_sf2";
      this.pct_sf2.Size = new System.Drawing.Size(100, 32);
      this.pct_sf2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_sf2.TabIndex = 251;
      this.pct_sf2.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
      this.panel2.Controls.Add(this.pct_sf1);
      this.panel2.Controls.Add(this.gg_glueGap11);
      this.panel2.Controls.Add(this.tnud_SpeedSF1_UVmm_m);
      this.panel2.Controls.Add(this.wa_wrapArmPreHeater23);
      this.panel2.Controls.Add(this.wa_wrapArmPreHeater22);
      this.panel2.Controls.Add(this.wa_wrapArmPreHeater12);
      this.panel2.Controls.Add(this.lbl_SF1);
      this.panel2.Controls.Add(this.wa_wrapArmPreHeater13);
      this.panel2.Location = new System.Drawing.Point(561, 194);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(253, 519);
      this.panel2.TabIndex = 254;
      // 
      // pct_sf1
      // 
      this.pct_sf1.Image = ((System.Drawing.Image)(resources.GetObject("pct_sf1.Image")));
      this.pct_sf1.Location = new System.Drawing.Point(81, 0);
      this.pct_sf1.Name = "pct_sf1";
      this.pct_sf1.Size = new System.Drawing.Size(100, 32);
      this.pct_sf1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_sf1.TabIndex = 250;
      this.pct_sf1.TabStop = false;
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(127)))), ((int)(((byte)(129)))));
      this.panel3.Controls.Add(this.wa_wrapArmPreHeater30);
      this.panel3.Controls.Add(this.gg_glueGap3);
      this.panel3.Controls.Add(this.wa_wrapArmPreHeater3);
      this.panel3.Controls.Add(this.wa_wrapArmPreHeater20);
      this.panel3.Controls.Add(this.pictureBox1);
      this.panel3.Controls.Add(this.tnud_SpeedGlueUnit_UVmm_m);
      this.panel3.Controls.Add(this.gg_glueGap1);
      this.panel3.Controls.Add(this.gg_glueGap2);
      this.panel3.Controls.Add(this.wa_wrapArmPreHeater11);
      this.panel3.Controls.Add(this.wa_wrapArmPreHeater2);
      this.panel3.Controls.Add(this.wa_wrapArmPreHeater1);
      this.panel3.Location = new System.Drawing.Point(824, 194);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(245, 519);
      this.panel3.TabIndex = 255;
      // 
      // wa_wrapArmPreHeater30
      // 
      this.wa_wrapArmPreHeater30.Auto = false;
      this.wa_wrapArmPreHeater30.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater30.InLocal = false;
      this.wa_wrapArmPreHeater30.IsMaxPos = false;
      this.wa_wrapArmPreHeater30.IsMinPos = false;
      this.wa_wrapArmPreHeater30.IsUnder = false;
      this.wa_wrapArmPreHeater30.Location = new System.Drawing.Point(117, 86);
      this.wa_wrapArmPreHeater30.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater30.Name = "wa_wrapArmPreHeater30";
      this.wa_wrapArmPreHeater30.Position = "0";
      this.wa_wrapArmPreHeater30.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater30.TabIndex = 257;
      this.wa_wrapArmPreHeater30.Temperature = 0;
      this.wa_wrapArmPreHeater30.TranslateText = false;
      this.wa_wrapArmPreHeater30.Visible = false;
      this.wa_wrapArmPreHeater30.WrapName = "30";
      this.wa_wrapArmPreHeater30.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.OnPreHeatersTernWrapArmButtonPressed);
      // 
      // gg_glueGap3
      // 
      this.gg_glueGap3.Auto = false;
      this.gg_glueGap3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap3.InLocal = false;
      this.gg_glueGap3.IsMaxPos = false;
      this.gg_glueGap3.IsMinPos = false;
      this.gg_glueGap3.Location = new System.Drawing.Point(137, 196);
      this.gg_glueGap3.LowLevelControlName = "3";
      this.gg_glueGap3.LowLevelEnabled = true;
      this.gg_glueGap3.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap3.MaxSetPointValue = 100;
      this.gg_glueGap3.MinSetPointValue = 0;
      this.gg_glueGap3.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap3.Name = "gg_glueGap3";
      this.gg_glueGap3.Position = "0";
      this.gg_glueGap3.Size = new System.Drawing.Size(100, 102);
      this.gg_glueGap3.TabIndex = 256;
      this.gg_glueGap3.TranslateText = false;
      this.gg_glueGap3.Visible = false;
      this.gg_glueGap3.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.OnGlueUnitGlueGapButtonPressed);
      // 
      // wa_wrapArmPreHeater3
      // 
      this.wa_wrapArmPreHeater3.Auto = false;
      this.wa_wrapArmPreHeater3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater3.InLocal = false;
      this.wa_wrapArmPreHeater3.IsMaxPos = false;
      this.wa_wrapArmPreHeater3.IsMinPos = false;
      this.wa_wrapArmPreHeater3.IsUnder = false;
      this.wa_wrapArmPreHeater3.Location = new System.Drawing.Point(8, 86);
      this.wa_wrapArmPreHeater3.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater3.Name = "wa_wrapArmPreHeater3";
      this.wa_wrapArmPreHeater3.Position = "0";
      this.wa_wrapArmPreHeater3.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater3.TabIndex = 255;
      this.wa_wrapArmPreHeater3.Temperature = 0;
      this.wa_wrapArmPreHeater3.TranslateText = false;
      this.wa_wrapArmPreHeater3.Visible = false;
      this.wa_wrapArmPreHeater3.WrapName = "3";
      this.wa_wrapArmPreHeater3.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.OnPreHeatersTernWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater20
      // 
      this.wa_wrapArmPreHeater20.Auto = false;
      this.wa_wrapArmPreHeater20.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater20.InLocal = false;
      this.wa_wrapArmPreHeater20.IsMaxPos = false;
      this.wa_wrapArmPreHeater20.IsMinPos = false;
      this.wa_wrapArmPreHeater20.IsUnder = false;
      this.wa_wrapArmPreHeater20.Location = new System.Drawing.Point(117, 196);
      this.wa_wrapArmPreHeater20.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater20.Name = "wa_wrapArmPreHeater20";
      this.wa_wrapArmPreHeater20.Position = "0";
      this.wa_wrapArmPreHeater20.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater20.TabIndex = 254;
      this.wa_wrapArmPreHeater20.Temperature = 0;
      this.wa_wrapArmPreHeater20.TranslateText = false;
      this.wa_wrapArmPreHeater20.Visible = false;
      this.wa_wrapArmPreHeater20.WrapName = "20";
      this.wa_wrapArmPreHeater20.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.OnPreHeatersTernWrapArmButtonPressed);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(65, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(115, 32);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 253;
      this.pictureBox1.TabStop = false;
      // 
      // tnud_SpeedGlueUnit_UVmm_m
      // 
      this.tnud_SpeedGlueUnit_UVmm_m.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tnud_SpeedGlueUnit_UVmm_m.DecimalPlaces = 0;
      this.tnud_SpeedGlueUnit_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SpeedGlueUnit_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedGlueUnit_UVmm_m.ImagePassword = null;
      this.tnud_SpeedGlueUnit_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SpeedGlueUnit_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedGlueUnit_UVmm_m.Location = new System.Drawing.Point(8, 40);
      this.tnud_SpeedGlueUnit_UVmm_m.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.tnud_SpeedGlueUnit_UVmm_m.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SpeedGlueUnit_UVmm_m.Name = "tnud_SpeedGlueUnit_UVmm_m";
      this.tnud_SpeedGlueUnit_UVmm_m.ReadOnly = true;
      this.tnud_SpeedGlueUnit_UVmm_m.Size = new System.Drawing.Size(229, 40);
      this.tnud_SpeedGlueUnit_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_SpeedGlueUnit_UVmm_m.TabIndex = 252;
      this.tnud_SpeedGlueUnit_UVmm_m.TabStop = false;
      this.tnud_SpeedGlueUnit_UVmm_m.Text = "Speed";
      this.tnud_SpeedGlueUnit_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedGlueUnit_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedGlueUnit_UVmm_m.TranslateText = false;
      this.tnud_SpeedGlueUnit_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SpeedGlueUnit_UVmm_m.UnitMeasureVisible = false;
      this.tnud_SpeedGlueUnit_UVmm_m.Value = "400000";
      this.tnud_SpeedGlueUnit_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedGlueUnit_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_SpeedGlueUnit_UVmm_m.Visible = false;
      // 
      // gg_glueGap1
      // 
      this.gg_glueGap1.Auto = false;
      this.gg_glueGap1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap1.InLocal = false;
      this.gg_glueGap1.IsMaxPos = false;
      this.gg_glueGap1.IsMinPos = false;
      this.gg_glueGap1.Location = new System.Drawing.Point(137, 412);
      this.gg_glueGap1.LowLevelControlName = "1";
      this.gg_glueGap1.LowLevelEnabled = true;
      this.gg_glueGap1.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap1.MaxSetPointValue = 100;
      this.gg_glueGap1.MinSetPointValue = 0;
      this.gg_glueGap1.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap1.Name = "gg_glueGap1";
      this.gg_glueGap1.Position = "0";
      this.gg_glueGap1.Size = new System.Drawing.Size(100, 102);
      this.gg_glueGap1.TabIndex = 251;
      this.gg_glueGap1.TranslateText = false;
      this.gg_glueGap1.Visible = false;
      this.gg_glueGap1.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.OnGlueUnitGlueGapButtonPressed);
      // 
      // gg_glueGap2
      // 
      this.gg_glueGap2.Auto = false;
      this.gg_glueGap2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap2.InLocal = false;
      this.gg_glueGap2.IsMaxPos = false;
      this.gg_glueGap2.IsMinPos = false;
      this.gg_glueGap2.Location = new System.Drawing.Point(137, 304);
      this.gg_glueGap2.LowLevelControlName = "2";
      this.gg_glueGap2.LowLevelEnabled = true;
      this.gg_glueGap2.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap2.MaxSetPointValue = 100;
      this.gg_glueGap2.MinSetPointValue = 0;
      this.gg_glueGap2.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap2.Name = "gg_glueGap2";
      this.gg_glueGap2.Position = "0";
      this.gg_glueGap2.Size = new System.Drawing.Size(100, 102);
      this.gg_glueGap2.TabIndex = 250;
      this.gg_glueGap2.TranslateText = false;
      this.gg_glueGap2.Visible = false;
      this.gg_glueGap2.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.OnGlueUnitGlueGapButtonPressed);
      // 
      // panel4
      // 
      this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      this.panel4.Controls.Add(this.wa_wrapArmPreHeater31);
      this.panel4.Controls.Add(this.pictureBox2);
      this.panel4.Controls.Add(this.tnud_SpeedDoubleBacker_UVmm_m);
      this.panel4.Location = new System.Drawing.Point(1076, 194);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(117, 519);
      this.panel4.TabIndex = 257;
      // 
      // wa_wrapArmPreHeater31
      // 
      this.wa_wrapArmPreHeater31.Auto = false;
      this.wa_wrapArmPreHeater31.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater31.InLocal = false;
      this.wa_wrapArmPreHeater31.IsMaxPos = false;
      this.wa_wrapArmPreHeater31.IsMinPos = false;
      this.wa_wrapArmPreHeater31.IsUnder = false;
      this.wa_wrapArmPreHeater31.Location = new System.Drawing.Point(8, 412);
      this.wa_wrapArmPreHeater31.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater31.Name = "wa_wrapArmPreHeater31";
      this.wa_wrapArmPreHeater31.Position = "0";
      this.wa_wrapArmPreHeater31.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater31.TabIndex = 258;
      this.wa_wrapArmPreHeater31.Temperature = 0;
      this.wa_wrapArmPreHeater31.TranslateText = false;
      this.wa_wrapArmPreHeater31.Visible = false;
      this.wa_wrapArmPreHeater31.WrapName = "31";
      this.wa_wrapArmPreHeater31.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater31_OnWrapArmButtonPressed);
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::SyncroC.Properties.Resources.Express_01;
      this.pictureBox2.Location = new System.Drawing.Point(18, 3);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(90, 29);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 253;
      this.pictureBox2.TabStop = false;
      // 
      // tnud_SpeedDoubleBacker_UVmm_m
      // 
      this.tnud_SpeedDoubleBacker_UVmm_m.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tnud_SpeedDoubleBacker_UVmm_m.DecimalPlaces = 0;
      this.tnud_SpeedDoubleBacker_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SpeedDoubleBacker_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedDoubleBacker_UVmm_m.ImagePassword = null;
      this.tnud_SpeedDoubleBacker_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SpeedDoubleBacker_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedDoubleBacker_UVmm_m.Location = new System.Drawing.Point(8, 40);
      this.tnud_SpeedDoubleBacker_UVmm_m.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.tnud_SpeedDoubleBacker_UVmm_m.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SpeedDoubleBacker_UVmm_m.Name = "tnud_SpeedDoubleBacker_UVmm_m";
      this.tnud_SpeedDoubleBacker_UVmm_m.ReadOnly = true;
      this.tnud_SpeedDoubleBacker_UVmm_m.Size = new System.Drawing.Size(100, 40);
      this.tnud_SpeedDoubleBacker_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_SpeedDoubleBacker_UVmm_m.TabIndex = 252;
      this.tnud_SpeedDoubleBacker_UVmm_m.TabStop = false;
      this.tnud_SpeedDoubleBacker_UVmm_m.Text = "Speed";
      this.tnud_SpeedDoubleBacker_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedDoubleBacker_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedDoubleBacker_UVmm_m.TranslateText = false;
      this.tnud_SpeedDoubleBacker_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SpeedDoubleBacker_UVmm_m.UnitMeasureVisible = false;
      this.tnud_SpeedDoubleBacker_UVmm_m.Value = "400000";
      this.tnud_SpeedDoubleBacker_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedDoubleBacker_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_SpeedDoubleBacker_UVmm_m.Visible = false;
      // 
      // panel5
      // 
      this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
      this.panel5.Controls.Add(this.pct_sf3);
      this.panel5.Controls.Add(this.gg_glueGap31);
      this.panel5.Controls.Add(this.tnud_SpeedSF3_UVmm_m);
      this.panel5.Controls.Add(this.wa_wrapArmPreHeater27);
      this.panel5.Controls.Add(this.wa_wrapArmPreHeater26);
      this.panel5.Controls.Add(this.wa_wrapArmPreHeater16);
      this.panel5.Controls.Add(this.lbl_SF3);
      this.panel5.Controls.Add(this.wa_wrapArmPreHeater17);
      this.panel5.Location = new System.Drawing.Point(37, 194);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(253, 519);
      this.panel5.TabIndex = 258;
      // 
      // pct_sf3
      // 
      this.pct_sf3.Image = ((System.Drawing.Image)(resources.GetObject("pct_sf3.Image")));
      this.pct_sf3.Location = new System.Drawing.Point(81, 0);
      this.pct_sf3.Name = "pct_sf3";
      this.pct_sf3.Size = new System.Drawing.Size(100, 32);
      this.pct_sf3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_sf3.TabIndex = 250;
      this.pct_sf3.TabStop = false;
      // 
      // gg_glueGap31
      // 
      this.gg_glueGap31.Auto = false;
      this.gg_glueGap31.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap31.InLocal = false;
      this.gg_glueGap31.IsMaxPos = false;
      this.gg_glueGap31.IsMinPos = false;
      this.gg_glueGap31.Location = new System.Drawing.Point(79, 253);
      this.gg_glueGap31.LowLevelControlName = "1";
      this.gg_glueGap31.LowLevelEnabled = true;
      this.gg_glueGap31.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap31.MaxSetPointValue = 100;
      this.gg_glueGap31.MinSetPointValue = 0;
      this.gg_glueGap31.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap31.Name = "gg_glueGap31";
      this.gg_glueGap31.Position = "0";
      this.gg_glueGap31.Size = new System.Drawing.Size(100, 102);
      this.gg_glueGap31.TabIndex = 248;
      this.gg_glueGap31.TranslateText = false;
      this.gg_glueGap31.Visible = false;
      this.gg_glueGap31.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.gg_glueGap31_OnGlueGapButtonPressed);
      // 
      // tnud_SpeedSF3_UVmm_m
      // 
      this.tnud_SpeedSF3_UVmm_m.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tnud_SpeedSF3_UVmm_m.DecimalPlaces = 0;
      this.tnud_SpeedSF3_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SpeedSF3_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSF3_UVmm_m.ImagePassword = null;
      this.tnud_SpeedSF3_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SpeedSF3_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedSF3_UVmm_m.Location = new System.Drawing.Point(10, 40);
      this.tnud_SpeedSF3_UVmm_m.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.tnud_SpeedSF3_UVmm_m.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SpeedSF3_UVmm_m.Name = "tnud_SpeedSF3_UVmm_m";
      this.tnud_SpeedSF3_UVmm_m.ReadOnly = true;
      this.tnud_SpeedSF3_UVmm_m.Size = new System.Drawing.Size(234, 40);
      this.tnud_SpeedSF3_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_SpeedSF3_UVmm_m.TabIndex = 249;
      this.tnud_SpeedSF3_UVmm_m.TabStop = false;
      this.tnud_SpeedSF3_UVmm_m.Text = "Speed";
      this.tnud_SpeedSF3_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSF3_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSF3_UVmm_m.TranslateText = false;
      this.tnud_SpeedSF3_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_SpeedSF3_UVmm_m.UnitMeasureVisible = false;
      this.tnud_SpeedSF3_UVmm_m.Value = "400000";
      this.tnud_SpeedSF3_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSF3_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_SpeedSF3_UVmm_m.Visible = false;
      // 
      // wa_wrapArmPreHeater27
      // 
      this.wa_wrapArmPreHeater27.Auto = false;
      this.wa_wrapArmPreHeater27.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater27.InLocal = false;
      this.wa_wrapArmPreHeater27.IsMaxPos = false;
      this.wa_wrapArmPreHeater27.IsMinPos = false;
      this.wa_wrapArmPreHeater27.IsUnder = false;
      this.wa_wrapArmPreHeater27.Location = new System.Drawing.Point(10, 412);
      this.wa_wrapArmPreHeater27.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater27.Name = "wa_wrapArmPreHeater27";
      this.wa_wrapArmPreHeater27.Position = "0";
      this.wa_wrapArmPreHeater27.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater27.TabIndex = 245;
      this.wa_wrapArmPreHeater27.Temperature = 0;
      this.wa_wrapArmPreHeater27.TranslateText = false;
      this.wa_wrapArmPreHeater27.Visible = false;
      this.wa_wrapArmPreHeater27.WrapName = "Il";
      this.wa_wrapArmPreHeater27.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater27_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater26
      // 
      this.wa_wrapArmPreHeater26.Auto = false;
      this.wa_wrapArmPreHeater26.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater26.InLocal = false;
      this.wa_wrapArmPreHeater26.IsMaxPos = false;
      this.wa_wrapArmPreHeater26.IsMinPos = false;
      this.wa_wrapArmPreHeater26.IsUnder = false;
      this.wa_wrapArmPreHeater26.Location = new System.Drawing.Point(144, 412);
      this.wa_wrapArmPreHeater26.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater26.Name = "wa_wrapArmPreHeater26";
      this.wa_wrapArmPreHeater26.Position = "0";
      this.wa_wrapArmPreHeater26.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater26.TabIndex = 246;
      this.wa_wrapArmPreHeater26.Temperature = 0;
      this.wa_wrapArmPreHeater26.TranslateText = false;
      this.wa_wrapArmPreHeater26.Visible = false;
      this.wa_wrapArmPreHeater26.WrapName = "Im";
      this.wa_wrapArmPreHeater26.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater26_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater16
      // 
      this.wa_wrapArmPreHeater16.Auto = false;
      this.wa_wrapArmPreHeater16.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater16.InLocal = false;
      this.wa_wrapArmPreHeater16.IsMaxPos = false;
      this.wa_wrapArmPreHeater16.IsMinPos = false;
      this.wa_wrapArmPreHeater16.IsUnder = false;
      this.wa_wrapArmPreHeater16.Location = new System.Drawing.Point(144, 98);
      this.wa_wrapArmPreHeater16.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater16.Name = "wa_wrapArmPreHeater16";
      this.wa_wrapArmPreHeater16.Position = "0";
      this.wa_wrapArmPreHeater16.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater16.TabIndex = 247;
      this.wa_wrapArmPreHeater16.Temperature = 0;
      this.wa_wrapArmPreHeater16.TranslateText = false;
      this.wa_wrapArmPreHeater16.Visible = false;
      this.wa_wrapArmPreHeater16.WrapName = "Em";
      this.wa_wrapArmPreHeater16.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater16_OnWrapArmButtonPressed);
      // 
      // lbl_SF3
      // 
      this.lbl_SF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
      this.lbl_SF3.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SF3.Location = new System.Drawing.Point(196, 0);
      this.lbl_SF3.Name = "lbl_SF3";
      this.lbl_SF3.Size = new System.Drawing.Size(48, 37);
      this.lbl_SF3.TabIndex = 231;
      this.lbl_SF3.Text = "3";
      this.lbl_SF3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // wa_wrapArmPreHeater17
      // 
      this.wa_wrapArmPreHeater17.Auto = false;
      this.wa_wrapArmPreHeater17.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater17.InLocal = false;
      this.wa_wrapArmPreHeater17.IsMaxPos = false;
      this.wa_wrapArmPreHeater17.IsMinPos = false;
      this.wa_wrapArmPreHeater17.IsUnder = false;
      this.wa_wrapArmPreHeater17.Location = new System.Drawing.Point(10, 99);
      this.wa_wrapArmPreHeater17.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater17.Name = "wa_wrapArmPreHeater17";
      this.wa_wrapArmPreHeater17.Position = "0";
      this.wa_wrapArmPreHeater17.Size = new System.Drawing.Size(100, 102);
      this.wa_wrapArmPreHeater17.TabIndex = 244;
      this.wa_wrapArmPreHeater17.Temperature = 0;
      this.wa_wrapArmPreHeater17.TranslateText = false;
      this.wa_wrapArmPreHeater17.Visible = false;
      this.wa_wrapArmPreHeater17.WrapName = "El";
      this.wa_wrapArmPreHeater17.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater17_OnWrapArmButtonPressed);
      // 
      // Frm_WetEndSupervisor01
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.panel4);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel5);
      this.Controls.Add(this.panel1);
      this.Name = "Frm_WetEndSupervisor01";
      this.Text = "Frm_WetEndSupervisor01";
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.panel5, 0);
      this.Controls.SetChildIndex(this.panel2, 0);
      this.Controls.SetChildIndex(this.panel3, 0);
      this.Controls.SetChildIndex(this.panel4, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_sf2)).EndInit();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_sf1)).EndInit();
      this.panel3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panel4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.panel5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_sf3)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    protected override void F1()
    {
    }

    public override void CorrugatorDirectionLeft()
    {
      //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
      int leftLimit=this.panel1.Location.X;
      if (DBManager.getDBManager().lineConfig.SingleFacer3)
        leftLimit = this.panel5.Location.X;
      //RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
      //int rightLimit = this.panel3.Location.X + this.panel3.Width;
      int rightLimit = this.panel4.Location.X + this.panel4.Width;
      HorizontalLocationMirror(this.panel1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.panel2, leftLimit, rightLimit);
      HorizontalLocationMirror(this.panel3, leftLimit, rightLimit);
      //RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
      HorizontalLocationMirror(this.panel4, leftLimit, rightLimit);
      //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
      HorizontalLocationMirror(this.panel5, leftLimit, rightLimit);

      leftLimit = this.wa_wrapArmPreHeater15.Location.X;
      rightLimit = this.wa_wrapArmPreHeater14.Location.X + this.wa_wrapArmPreHeater14.Width;
      HorizontalLocationMirror(this.wa_wrapArmPreHeater15, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater14, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater25, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater24, leftLimit, rightLimit);

      leftLimit = this.wa_wrapArmPreHeater13.Location.X;
      rightLimit = this.wa_wrapArmPreHeater12.Location.X + this.wa_wrapArmPreHeater12.Width;
      HorizontalLocationMirror(this.wa_wrapArmPreHeater13, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater12, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater23, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater22, leftLimit, rightLimit);

      leftLimit = this.wa_wrapArmPreHeater11.Location.X;
      rightLimit = this.gg_glueGap1.Location.X + this.gg_glueGap1.Width;
      HorizontalLocationMirror(this.wa_wrapArmPreHeater11, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater2, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater20, leftLimit, rightLimit);
      HorizontalLocationMirror(this.gg_glueGap1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.gg_glueGap2, leftLimit, rightLimit);
      //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
      HorizontalLocationMirror(this.wa_wrapArmPreHeater3, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater30, leftLimit, rightLimit);
      HorizontalLocationMirror(this.gg_glueGap3, leftLimit, rightLimit);

      //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
      leftLimit = this.wa_wrapArmPreHeater17.Location.X;
      rightLimit = this.wa_wrapArmPreHeater16.Location.X + this.wa_wrapArmPreHeater16.Width;
      HorizontalLocationMirror(this.wa_wrapArmPreHeater17, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater16, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater27, leftLimit, rightLimit);
      HorizontalLocationMirror(this.wa_wrapArmPreHeater26, leftLimit, rightLimit);
    }


    //RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
    /*************************
     * DOUBLE BACKER
     * ***********************/
    /// <summary>
    /// Pre heaters tern "wrap arm" click management
    /// </summary>
    private void wa_wrapArmPreHeater31_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = ((Control)sender).Name.Replace("wa_", args.AutoMan ? "AutoManual" : "LocalRemote");

        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        string cmd = ((Control)sender).Name.Replace("wa_", "");

        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }


    /*************************
     * PRE HEATERS TERN
     * ***********************/
    /// <summary>
    /// Pre heaters tern "wrap arm" click management
    /// </summary>
    private void OnPreHeatersTernWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        string cmd = ((Control)sender).Name.Replace("wa_", args.AutoMan ? "AutoManual" : "LocalRemote");
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        string cmd = ((Control)sender).Name.Replace("wa_", "");
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }




    /****************************
     * GLUE UNIT
     * **************************/
    /// <summary>
    /// Glue Unit "glue gap" click management
    /// </summary>
    private void OnGlueUnitGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        string cmd = ((Control)sender).Name.Replace("gg_", args.AutoMan ? "AutoManual" : "LocalRemote");
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        string cmd = ((Control)sender).Name.Replace("gg_", "");
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }




    /********************************
     * SINGLE FACER 1
     * *******************************/
    private void wa_wrapArmPreHeater13_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater12_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater23_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater22_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void gg_glueGap11_OnGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "glueGap1";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }





    /********************************
     * SINGLE FACER 2
     * *******************************/
    private void wa_wrapArmPreHeater15_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater14_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater25_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater24_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }


    private void gg_glueGap21_OnGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "glueGap1";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        //ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
    /********************************
     * SINGLE FACER 3
     * *******************************/
    private void wa_wrapArmPreHeater17_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater16_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void wa_wrapArmPreHeater27_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {

    }

    private void wa_wrapArmPreHeater26_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }


    private void gg_glueGap31_OnGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "glueGap1";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }


  }
}

