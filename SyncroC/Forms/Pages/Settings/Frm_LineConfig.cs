using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;
using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Settings
{
	public class Frm_LineConfig : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
	{
		private SyncroC.Controls.Title ttl_wetEnd;
		private SyncroC.Controls.Title ttl_DryEnd;
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.Title ttl_Splicer7;
		private SyncroC.Controls.Title ttl_SingleFacer3;
		private SyncroC.Controls.Title ttl_Splicer6;
		private SyncroC.Controls.Title ttl_Splicer4;
		private SyncroC.Controls.Title ttl_SingleFacer2;
		private SyncroC.Controls.Title ttl_Splicer5;
		private SyncroC.Controls.Title ttl_Splicer2;
		private SyncroC.Controls.Title ttl_SingleFacer1;
		private SyncroC.Controls.Title ttl_SPlicer3;
		private SyncroC.Controls.Title ttl_Splicer1;
		private SyncroC.Controls.Title ttl_GlueUnit;
		private SyncroC.Controls.Title ttl_DoubleBacker;
		private SyncroC.Controls.Title ttl_Rotary;
		private SyncroC.Controls.Title ttl_SlitterScorer;
		private SyncroC.Controls.Title ttl_WebDirector;
		private SyncroC.Controls.Title ttl_Knife;
		private SyncroC.Controls.Title ttl_StackerLow;
		private SyncroC.Controls.Title ttl_StackrMid;
		private SyncroC.Controls.Title ttl_StackerHigh;
		private SyncroC.Controls.CheckBox chk_Splicer7;
		private SyncroC.Controls.CheckBox chk_Splicer1;
		private SyncroC.Controls.CheckBox chk_Rotary;
		private SyncroC.Controls.CheckBox chk_SingleFacer2;
		private SyncroC.Controls.CheckBox chk_Splicer4;
		private SyncroC.Controls.CheckBox chk_Splicer3;
		private SyncroC.Controls.CheckBox chk_SingleFacer1;
		private SyncroC.Controls.CheckBox chk_Splicer5;
		private SyncroC.Controls.CheckBox chk_Splicer2;
		private SyncroC.Controls.CheckBox chk_SingleFacer3;
		private SyncroC.Controls.CheckBox chk_DoubleBacker;
		private SyncroC.Controls.CheckBox chk_SlitterScorer;
		private SyncroC.Controls.CheckBox chk_Knife;
		private SyncroC.Controls.CheckBox chk_StackerLow;
		private SyncroC.Controls.CheckBox chk_Splicer6;
		private SyncroC.Controls.CheckBox chk_WebDirector;
		private SyncroC.Controls.CheckBox chk_StackerHigh;
		private SyncroC.Controls.CheckBox chk_GlueUnit;
		private SyncroC.Controls.StateButton stb_Splicer7;
		private SyncroC.Controls.StateButton stb_Splicer6;
		private SyncroC.Controls.StateButton stb_Splicer5;
		private SyncroC.Controls.StateButton stb_Splicer3;
		private SyncroC.Controls.StateButton stb_Splicer4;
		private SyncroC.Controls.StateButton stb_SingleFacer3;
		private SyncroC.Controls.StateButton stb_SingleFacer2;
		private SyncroC.Controls.StateButton stb_SingleFacer1;
		private SyncroC.Controls.StateButton stb_GlueUnit;
		private SyncroC.Controls.StateButton stb_DoubleBacker;
		private SyncroC.Controls.StateButton stb_Rotary;
		private SyncroC.Controls.StateButton stb_SlitterScorer;
		private SyncroC.Controls.StateButton stb_WebDirector;
		private SyncroC.Controls.StateButton stb_Knife;
		private SyncroC.Controls.StateButton stb_StackerLow;
		private SyncroC.Controls.StateButton stb_StackerHigh;
		private SyncroC.Controls.CheckBox chk_PreHeatersTern;
		private SyncroC.Controls.Title ttl_PreHeatersTern;
		private SyncroC.Controls.StateButton stb_PreHeatersTern;
		private SyncroC.Controls.CheckBox chk_StackerMid;
		private SyncroC.Controls.StateButton stb_Splicer1;
		private SyncroC.Controls.StateButton stb_Splicer2;
		private SyncroC.Controls.StateButton stb_StackerMid;
		private SyncroC.Controls.Title ttl_Various;
		private SyncroC.Controls.StateButton stb_AOC;
		private SyncroC.Controls.Title ttl_AOC;
		private SyncroC.Controls.CheckBox chk_AOC;
		private SyncroC.Controls.StateButton stb_Speed;
		private SyncroC.Controls.Title ttl_Speed;
		private SyncroC.Controls.CheckBox chk_Speed;
		private SyncroC.Controls.StateButton stb_SpeedReference;
		private SyncroC.Controls.Title ttl_SpeedRef;
		private SyncroC.Controls.CheckBox chk_SpeedReference;
        private SyncroC.Controls.StateButton stb_Syncro;
        private SyncroC.Controls.Title ttl_Syncro;
        private SyncroC.Controls.CheckBox chk_Syncro;
        private SyncroC.Controls.StateButton stb_Printers;
        private SyncroC.Controls.Title ttl_Printers;
        private SyncroC.Controls.CheckBox chk_Printers;
		private SyncroC.Controls.Title ttl_Office;
		private SyncroC.Controls.StateButton stb_Office;
		private SyncroC.Controls.CheckBox chk_Office;
    private SyncroC.Controls.StateButton stb_PRC;
    private SyncroC.Controls.CheckBox chk_PRC;
    private SyncroC.Controls.Title ttl_PRC;

		private enum State {Modify, Read};
		private State state = State.Read;

		public Frm_LineConfig()
		{
			InitializeComponent();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LineConfig));
      this.ttl_wetEnd = new SyncroC.Controls.Title();
      this.ttl_DryEnd = new SyncroC.Controls.Title();
      this.chk_Splicer7 = new SyncroC.Controls.CheckBox();
      this.chk_Splicer1 = new SyncroC.Controls.CheckBox();
      this.chk_Rotary = new SyncroC.Controls.CheckBox();
      this.chk_SingleFacer2 = new SyncroC.Controls.CheckBox();
      this.chk_Splicer4 = new SyncroC.Controls.CheckBox();
      this.chk_Splicer3 = new SyncroC.Controls.CheckBox();
      this.chk_SingleFacer1 = new SyncroC.Controls.CheckBox();
      this.chk_Splicer5 = new SyncroC.Controls.CheckBox();
      this.chk_Splicer2 = new SyncroC.Controls.CheckBox();
      this.chk_SingleFacer3 = new SyncroC.Controls.CheckBox();
      this.chk_DoubleBacker = new SyncroC.Controls.CheckBox();
      this.chk_PreHeatersTern = new SyncroC.Controls.CheckBox();
      this.chk_SlitterScorer = new SyncroC.Controls.CheckBox();
      this.chk_Knife = new SyncroC.Controls.CheckBox();
      this.chk_StackerLow = new SyncroC.Controls.CheckBox();
      this.chk_Splicer6 = new SyncroC.Controls.CheckBox();
      this.chk_WebDirector = new SyncroC.Controls.CheckBox();
      this.chk_StackerMid = new SyncroC.Controls.CheckBox();
      this.chk_StackerHigh = new SyncroC.Controls.CheckBox();
      this.ttl_Splicer7 = new SyncroC.Controls.Title();
      this.ttl_SingleFacer3 = new SyncroC.Controls.Title();
      this.ttl_Splicer6 = new SyncroC.Controls.Title();
      this.ttl_Splicer4 = new SyncroC.Controls.Title();
      this.ttl_SingleFacer2 = new SyncroC.Controls.Title();
      this.ttl_Splicer5 = new SyncroC.Controls.Title();
      this.ttl_Splicer2 = new SyncroC.Controls.Title();
      this.ttl_SingleFacer1 = new SyncroC.Controls.Title();
      this.ttl_SPlicer3 = new SyncroC.Controls.Title();
      this.ttl_GlueUnit = new SyncroC.Controls.Title();
      this.ttl_PreHeatersTern = new SyncroC.Controls.Title();
      this.ttl_Splicer1 = new SyncroC.Controls.Title();
      this.ttl_DoubleBacker = new SyncroC.Controls.Title();
      this.ttl_Rotary = new SyncroC.Controls.Title();
      this.ttl_SlitterScorer = new SyncroC.Controls.Title();
      this.ttl_WebDirector = new SyncroC.Controls.Title();
      this.ttl_Knife = new SyncroC.Controls.Title();
      this.ttl_StackerLow = new SyncroC.Controls.Title();
      this.ttl_StackrMid = new SyncroC.Controls.Title();
      this.ttl_StackerHigh = new SyncroC.Controls.Title();
      this.chk_GlueUnit = new SyncroC.Controls.CheckBox();
      this.stb_Splicer7 = new SyncroC.Controls.StateButton();
      this.stb_Splicer6 = new SyncroC.Controls.StateButton();
      this.stb_Splicer5 = new SyncroC.Controls.StateButton();
      this.stb_Splicer3 = new SyncroC.Controls.StateButton();
      this.stb_Splicer1 = new SyncroC.Controls.StateButton();
      this.stb_Splicer4 = new SyncroC.Controls.StateButton();
      this.stb_Splicer2 = new SyncroC.Controls.StateButton();
      this.stb_SingleFacer3 = new SyncroC.Controls.StateButton();
      this.stb_SingleFacer2 = new SyncroC.Controls.StateButton();
      this.stb_SingleFacer1 = new SyncroC.Controls.StateButton();
      this.stb_PreHeatersTern = new SyncroC.Controls.StateButton();
      this.stb_GlueUnit = new SyncroC.Controls.StateButton();
      this.stb_DoubleBacker = new SyncroC.Controls.StateButton();
      this.stb_Rotary = new SyncroC.Controls.StateButton();
      this.stb_SlitterScorer = new SyncroC.Controls.StateButton();
      this.stb_WebDirector = new SyncroC.Controls.StateButton();
      this.stb_Knife = new SyncroC.Controls.StateButton();
      this.stb_StackerLow = new SyncroC.Controls.StateButton();
      this.stb_StackerMid = new SyncroC.Controls.StateButton();
      this.stb_StackerHigh = new SyncroC.Controls.StateButton();
      this.ttl_Various = new SyncroC.Controls.Title();
      this.stb_AOC = new SyncroC.Controls.StateButton();
      this.ttl_AOC = new SyncroC.Controls.Title();
      this.chk_AOC = new SyncroC.Controls.CheckBox();
      this.stb_Speed = new SyncroC.Controls.StateButton();
      this.ttl_Speed = new SyncroC.Controls.Title();
      this.chk_Speed = new SyncroC.Controls.CheckBox();
      this.stb_SpeedReference = new SyncroC.Controls.StateButton();
      this.ttl_SpeedRef = new SyncroC.Controls.Title();
      this.chk_SpeedReference = new SyncroC.Controls.CheckBox();
      this.stb_Syncro = new SyncroC.Controls.StateButton();
      this.ttl_Syncro = new SyncroC.Controls.Title();
      this.chk_Syncro = new SyncroC.Controls.CheckBox();
      this.stb_Printers = new SyncroC.Controls.StateButton();
      this.ttl_Printers = new SyncroC.Controls.Title();
      this.chk_Printers = new SyncroC.Controls.CheckBox();
      this.ttl_Office = new SyncroC.Controls.Title();
      this.stb_Office = new SyncroC.Controls.StateButton();
      this.chk_Office = new SyncroC.Controls.CheckBox();
      this.stb_PRC = new SyncroC.Controls.StateButton();
      this.chk_PRC = new SyncroC.Controls.CheckBox();
      this.ttl_PRC = new SyncroC.Controls.Title();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Line Configuration";
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // ttl_wetEnd
      // 
      this.ttl_wetEnd.BackColor = System.Drawing.Color.Black;
      this.ttl_wetEnd.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_wetEnd.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_wetEnd.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_wetEnd.Location = new System.Drawing.Point(96, 193);
      this.ttl_wetEnd.Name = "ttl_wetEnd";
      this.ttl_wetEnd.Size = new System.Drawing.Size(1067, 219);
      this.ttl_wetEnd.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_wetEnd.TabIndex = 134;
      this.ttl_wetEnd.TabStop = false;
      this.ttl_wetEnd.Text = "WET END CONFIGURATION";
      this.ttl_wetEnd.TranslateText = false;
      // 
      // ttl_DryEnd
      // 
      this.ttl_DryEnd.BackColor = System.Drawing.Color.Black;
      this.ttl_DryEnd.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DryEnd.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DryEnd.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DryEnd.Location = new System.Drawing.Point(96, 432);
      this.ttl_DryEnd.Name = "ttl_DryEnd";
      this.ttl_DryEnd.Size = new System.Drawing.Size(1067, 129);
      this.ttl_DryEnd.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_DryEnd.TabIndex = 135;
      this.ttl_DryEnd.TabStop = false;
      this.ttl_DryEnd.Text = "DRY END CONFIGURATION";
      this.ttl_DryEnd.TranslateText = false;
      // 
      // chk_Splicer7
      // 
      this.chk_Splicer7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer7.Checked = true;
      this.chk_Splicer7.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer7.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer7.ImagePassword = null;
      this.chk_Splicer7.Location = new System.Drawing.Point(194, 266);
      this.chk_Splicer7.Name = "chk_Splicer7";
      this.chk_Splicer7.ReadOnly = true;
      this.chk_Splicer7.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer7.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer7.TabIndex = 0;
      this.chk_Splicer7.TabStop = false;
      this.chk_Splicer7.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer7.TranslateText = false;
      this.chk_Splicer7.OnCheckedChanged += new System.EventHandler(this.chk_Splicer7_OnCheckedChange);
      // 
      // chk_Splicer1
      // 
      this.chk_Splicer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer1.Checked = true;
      this.chk_Splicer1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer1.ImagePassword = null;
      this.chk_Splicer1.Location = new System.Drawing.Point(650, 369);
      this.chk_Splicer1.Name = "chk_Splicer1";
      this.chk_Splicer1.ReadOnly = true;
      this.chk_Splicer1.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer1.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer1.TabIndex = 9;
      this.chk_Splicer1.TabStop = false;
      this.chk_Splicer1.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer1.TranslateText = false;
      this.chk_Splicer1.OnCheckedChanged += new System.EventHandler(this.chk_Splicer1_OnCheckedChange);
      // 
      // chk_Rotary
      // 
      this.chk_Rotary.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Rotary.Checked = true;
      this.chk_Rotary.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Rotary.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Rotary.ImagePassword = null;
      this.chk_Rotary.Location = new System.Drawing.Point(193, 515);
      this.chk_Rotary.Name = "chk_Rotary";
      this.chk_Rotary.ReadOnly = true;
      this.chk_Rotary.Size = new System.Drawing.Size(44, 22);
      this.chk_Rotary.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Rotary.TabIndex = 12;
      this.chk_Rotary.TabStop = false;
      this.chk_Rotary.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Rotary.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Rotary.TranslateText = false;
      this.chk_Rotary.OnCheckedChanged += new System.EventHandler(this.chk_Rotary_OnCheckedChange);
      // 
      // chk_SingleFacer2
      // 
      this.chk_SingleFacer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SingleFacer2.Checked = true;
      this.chk_SingleFacer2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_SingleFacer2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SingleFacer2.ImagePassword = null;
      this.chk_SingleFacer2.Location = new System.Drawing.Point(802, 263);
      this.chk_SingleFacer2.Name = "chk_SingleFacer2";
      this.chk_SingleFacer2.ReadOnly = true;
      this.chk_SingleFacer2.Size = new System.Drawing.Size(44, 22);
      this.chk_SingleFacer2.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_SingleFacer2.TabIndex = 4;
      this.chk_SingleFacer2.TabStop = false;
      this.chk_SingleFacer2.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SingleFacer2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_SingleFacer2.TranslateText = false;
      this.chk_SingleFacer2.OnCheckedChanged += new System.EventHandler(this.chk_SingleFacer2_OnCheckedChange);
      // 
      // chk_Splicer4
      // 
      this.chk_Splicer4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer4.Checked = true;
      this.chk_Splicer4.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer4.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer4.ImagePassword = null;
      this.chk_Splicer4.Location = new System.Drawing.Point(954, 263);
      this.chk_Splicer4.Name = "chk_Splicer4";
      this.chk_Splicer4.ReadOnly = true;
      this.chk_Splicer4.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer4.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer4.TabIndex = 5;
      this.chk_Splicer4.TabStop = false;
      this.chk_Splicer4.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer4.TranslateText = false;
      this.chk_Splicer4.OnCheckedChanged += new System.EventHandler(this.chk_Splicer4_OnCheckedChange);
      // 
      // chk_Splicer3
      // 
      this.chk_Splicer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer3.Checked = true;
      this.chk_Splicer3.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer3.ImagePassword = null;
      this.chk_Splicer3.Location = new System.Drawing.Point(194, 369);
      this.chk_Splicer3.Name = "chk_Splicer3";
      this.chk_Splicer3.ReadOnly = true;
      this.chk_Splicer3.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer3.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer3.TabIndex = 6;
      this.chk_Splicer3.TabStop = false;
      this.chk_Splicer3.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer3.TranslateText = false;
      this.chk_Splicer3.OnCheckedChanged += new System.EventHandler(this.chk_Splicer3_OnCheckedChange);
      // 
      // chk_SingleFacer1
      // 
      this.chk_SingleFacer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SingleFacer1.Checked = true;
      this.chk_SingleFacer1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_SingleFacer1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SingleFacer1.ImagePassword = null;
      this.chk_SingleFacer1.Location = new System.Drawing.Point(346, 369);
      this.chk_SingleFacer1.Name = "chk_SingleFacer1";
      this.chk_SingleFacer1.ReadOnly = true;
      this.chk_SingleFacer1.Size = new System.Drawing.Size(44, 22);
      this.chk_SingleFacer1.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_SingleFacer1.TabIndex = 7;
      this.chk_SingleFacer1.TabStop = false;
      this.chk_SingleFacer1.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SingleFacer1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_SingleFacer1.TranslateText = false;
      this.chk_SingleFacer1.OnCheckedChanged += new System.EventHandler(this.chk_SingleFacer1_OnCheckedChange);
      // 
      // chk_Splicer5
      // 
      this.chk_Splicer5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer5.Checked = true;
      this.chk_Splicer5.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer5.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer5.ImagePassword = null;
      this.chk_Splicer5.Location = new System.Drawing.Point(650, 263);
      this.chk_Splicer5.Name = "chk_Splicer5";
      this.chk_Splicer5.ReadOnly = true;
      this.chk_Splicer5.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer5.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer5.TabIndex = 3;
      this.chk_Splicer5.TabStop = false;
      this.chk_Splicer5.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer5.TranslateText = false;
      this.chk_Splicer5.OnCheckedChanged += new System.EventHandler(this.chk_Splicer5_OnCheckedChange);
      // 
      // chk_Splicer2
      // 
      this.chk_Splicer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer2.Checked = true;
      this.chk_Splicer2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer2.ImagePassword = null;
      this.chk_Splicer2.Location = new System.Drawing.Point(498, 369);
      this.chk_Splicer2.Name = "chk_Splicer2";
      this.chk_Splicer2.ReadOnly = true;
      this.chk_Splicer2.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer2.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer2.TabIndex = 8;
      this.chk_Splicer2.TabStop = false;
      this.chk_Splicer2.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer2.TranslateText = false;
      this.chk_Splicer2.OnCheckedChanged += new System.EventHandler(this.chk_Splicer2_OnCheckedChange);
      // 
      // chk_SingleFacer3
      // 
      this.chk_SingleFacer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SingleFacer3.Checked = true;
      this.chk_SingleFacer3.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_SingleFacer3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SingleFacer3.ImagePassword = null;
      this.chk_SingleFacer3.Location = new System.Drawing.Point(346, 263);
      this.chk_SingleFacer3.Name = "chk_SingleFacer3";
      this.chk_SingleFacer3.ReadOnly = true;
      this.chk_SingleFacer3.Size = new System.Drawing.Size(44, 22);
      this.chk_SingleFacer3.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_SingleFacer3.TabIndex = 1;
      this.chk_SingleFacer3.TabStop = false;
      this.chk_SingleFacer3.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SingleFacer3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_SingleFacer3.TranslateText = false;
      this.chk_SingleFacer3.OnCheckedChanged += new System.EventHandler(this.chk_SingleFacer3_OnCheckedChange);
      // 
      // chk_DoubleBacker
      // 
      this.chk_DoubleBacker.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DoubleBacker.Checked = true;
      this.chk_DoubleBacker.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_DoubleBacker.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_DoubleBacker.ImagePassword = null;
      this.chk_DoubleBacker.Location = new System.Drawing.Point(1104, 369);
      this.chk_DoubleBacker.Name = "chk_DoubleBacker";
      this.chk_DoubleBacker.ReadOnly = true;
      this.chk_DoubleBacker.Size = new System.Drawing.Size(44, 22);
      this.chk_DoubleBacker.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_DoubleBacker.TabIndex = 11;
      this.chk_DoubleBacker.TabStop = false;
      this.chk_DoubleBacker.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DoubleBacker.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_DoubleBacker.TranslateText = false;
      this.chk_DoubleBacker.OnCheckedChanged += new System.EventHandler(this.chk_DoubleBacker_OnCheckedChange);
      // 
      // chk_PreHeatersTern
      // 
      this.chk_PreHeatersTern.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PreHeatersTern.Checked = true;
      this.chk_PreHeatersTern.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_PreHeatersTern.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_PreHeatersTern.ImagePassword = null;
      this.chk_PreHeatersTern.Location = new System.Drawing.Point(802, 369);
      this.chk_PreHeatersTern.Name = "chk_PreHeatersTern";
      this.chk_PreHeatersTern.ReadOnly = true;
      this.chk_PreHeatersTern.Size = new System.Drawing.Size(44, 22);
      this.chk_PreHeatersTern.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_PreHeatersTern.TabIndex = 10;
      this.chk_PreHeatersTern.TabStop = false;
      this.chk_PreHeatersTern.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PreHeatersTern.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_PreHeatersTern.TranslateText = false;
      this.chk_PreHeatersTern.OnCheckedChanged += new System.EventHandler(this.chk_PreHeatersTern_OnCheckedChange);
      // 
      // chk_SlitterScorer
      // 
      this.chk_SlitterScorer.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SlitterScorer.Checked = true;
      this.chk_SlitterScorer.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_SlitterScorer.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SlitterScorer.ImagePassword = null;
      this.chk_SlitterScorer.Location = new System.Drawing.Point(345, 515);
      this.chk_SlitterScorer.Name = "chk_SlitterScorer";
      this.chk_SlitterScorer.ReadOnly = true;
      this.chk_SlitterScorer.Size = new System.Drawing.Size(44, 22);
      this.chk_SlitterScorer.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_SlitterScorer.TabIndex = 13;
      this.chk_SlitterScorer.TabStop = false;
      this.chk_SlitterScorer.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SlitterScorer.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_SlitterScorer.TranslateText = false;
      this.chk_SlitterScorer.OnCheckedChanged += new System.EventHandler(this.chk_SlitterScorer_OnCheckedChange);
      // 
      // chk_Knife
      // 
      this.chk_Knife.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Knife.Checked = true;
      this.chk_Knife.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Knife.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Knife.ImagePassword = null;
      this.chk_Knife.Location = new System.Drawing.Point(649, 515);
      this.chk_Knife.Name = "chk_Knife";
      this.chk_Knife.ReadOnly = true;
      this.chk_Knife.Size = new System.Drawing.Size(44, 22);
      this.chk_Knife.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Knife.TabIndex = 14;
      this.chk_Knife.TabStop = false;
      this.chk_Knife.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Knife.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Knife.TranslateText = false;
      this.chk_Knife.OnCheckedChanged += new System.EventHandler(this.chk_Knife_OnCheckedChange);
      // 
      // chk_StackerLow
      // 
      this.chk_StackerLow.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_StackerLow.Checked = true;
      this.chk_StackerLow.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_StackerLow.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerLow.ImagePassword = null;
      this.chk_StackerLow.Location = new System.Drawing.Point(801, 515);
      this.chk_StackerLow.Name = "chk_StackerLow";
      this.chk_StackerLow.ReadOnly = true;
      this.chk_StackerLow.Size = new System.Drawing.Size(44, 22);
      this.chk_StackerLow.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_StackerLow.TabIndex = 15;
      this.chk_StackerLow.TabStop = false;
      this.chk_StackerLow.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_StackerLow.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_StackerLow.TranslateText = false;
      this.chk_StackerLow.OnCheckedChanged += new System.EventHandler(this.chk_StackerLow_OnCheckedChange);
      // 
      // chk_Splicer6
      // 
      this.chk_Splicer6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer6.Checked = true;
      this.chk_Splicer6.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Splicer6.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Splicer6.ImagePassword = null;
      this.chk_Splicer6.Location = new System.Drawing.Point(498, 263);
      this.chk_Splicer6.Name = "chk_Splicer6";
      this.chk_Splicer6.ReadOnly = true;
      this.chk_Splicer6.Size = new System.Drawing.Size(44, 22);
      this.chk_Splicer6.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Splicer6.TabIndex = 136;
      this.chk_Splicer6.TabStop = false;
      this.chk_Splicer6.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Splicer6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Splicer6.TranslateText = false;
      this.chk_Splicer6.OnCheckedChanged += new System.EventHandler(this.chk_Splicer6_OnCheckedChange);
      // 
      // chk_WebDirector
      // 
      this.chk_WebDirector.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_WebDirector.Checked = true;
      this.chk_WebDirector.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_WebDirector.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_WebDirector.ImagePassword = null;
      this.chk_WebDirector.Location = new System.Drawing.Point(497, 515);
      this.chk_WebDirector.Name = "chk_WebDirector";
      this.chk_WebDirector.ReadOnly = true;
      this.chk_WebDirector.Size = new System.Drawing.Size(44, 22);
      this.chk_WebDirector.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_WebDirector.TabIndex = 137;
      this.chk_WebDirector.TabStop = false;
      this.chk_WebDirector.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_WebDirector.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_WebDirector.TranslateText = false;
      this.chk_WebDirector.OnCheckedChanged += new System.EventHandler(this.chk_WebDirector_OnCheckedChange);
      // 
      // chk_StackerMid
      // 
      this.chk_StackerMid.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_StackerMid.Checked = true;
      this.chk_StackerMid.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_StackerMid.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerMid.ImagePassword = null;
      this.chk_StackerMid.Location = new System.Drawing.Point(953, 515);
      this.chk_StackerMid.Name = "chk_StackerMid";
      this.chk_StackerMid.ReadOnly = true;
      this.chk_StackerMid.Size = new System.Drawing.Size(44, 22);
      this.chk_StackerMid.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_StackerMid.TabIndex = 140;
      this.chk_StackerMid.TabStop = false;
      this.chk_StackerMid.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_StackerMid.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_StackerMid.TranslateText = false;
      this.chk_StackerMid.OnCheckedChanged += new System.EventHandler(this.chk_StackerMid_OnCheckedChange);
      // 
      // chk_StackerHigh
      // 
      this.chk_StackerHigh.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_StackerHigh.Checked = true;
      this.chk_StackerHigh.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_StackerHigh.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerHigh.ImagePassword = null;
      this.chk_StackerHigh.Location = new System.Drawing.Point(1105, 515);
      this.chk_StackerHigh.Name = "chk_StackerHigh";
      this.chk_StackerHigh.ReadOnly = true;
      this.chk_StackerHigh.Size = new System.Drawing.Size(44, 22);
      this.chk_StackerHigh.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_StackerHigh.TabIndex = 141;
      this.chk_StackerHigh.TabStop = false;
      this.chk_StackerHigh.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_StackerHigh.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_StackerHigh.TranslateText = false;
      this.chk_StackerHigh.OnCheckedChanged += new System.EventHandler(this.chk_StackerHigh_OnCheckedChange);
      // 
      // ttl_Splicer7
      // 
      this.ttl_Splicer7.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer7.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer7.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer7.Location = new System.Drawing.Point(108, 224);
      this.ttl_Splicer7.Name = "ttl_Splicer7";
      this.ttl_Splicer7.Size = new System.Drawing.Size(144, 92);
      this.ttl_Splicer7.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer7.TabIndex = 142;
      this.ttl_Splicer7.Text = "SPLICER 7";
      this.ttl_Splicer7.TranslateText = false;
      // 
      // ttl_SingleFacer3
      // 
      this.ttl_SingleFacer3.BackColor = System.Drawing.Color.Black;
      this.ttl_SingleFacer3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SingleFacer3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SingleFacer3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SingleFacer3.Location = new System.Drawing.Point(260, 224);
      this.ttl_SingleFacer3.Name = "ttl_SingleFacer3";
      this.ttl_SingleFacer3.Size = new System.Drawing.Size(144, 92);
      this.ttl_SingleFacer3.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SingleFacer3.TabIndex = 144;
      this.ttl_SingleFacer3.Text = "SINGLE FACER 3";
      this.ttl_SingleFacer3.TranslateText = false;
      // 
      // ttl_Splicer6
      // 
      this.ttl_Splicer6.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer6.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer6.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer6.Location = new System.Drawing.Point(412, 224);
      this.ttl_Splicer6.Name = "ttl_Splicer6";
      this.ttl_Splicer6.Size = new System.Drawing.Size(144, 92);
      this.ttl_Splicer6.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer6.TabIndex = 145;
      this.ttl_Splicer6.Text = "SPLICER 6";
      this.ttl_Splicer6.TranslateText = false;
      // 
      // ttl_Splicer4
      // 
      this.ttl_Splicer4.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer4.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer4.Location = new System.Drawing.Point(868, 224);
      this.ttl_Splicer4.Name = "ttl_Splicer4";
      this.ttl_Splicer4.Size = new System.Drawing.Size(144, 92);
      this.ttl_Splicer4.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer4.TabIndex = 148;
      this.ttl_Splicer4.Text = "SPLICER 4";
      this.ttl_Splicer4.TranslateText = false;
      // 
      // ttl_SingleFacer2
      // 
      this.ttl_SingleFacer2.BackColor = System.Drawing.Color.Black;
      this.ttl_SingleFacer2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SingleFacer2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SingleFacer2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SingleFacer2.Location = new System.Drawing.Point(716, 224);
      this.ttl_SingleFacer2.Name = "ttl_SingleFacer2";
      this.ttl_SingleFacer2.Size = new System.Drawing.Size(144, 92);
      this.ttl_SingleFacer2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SingleFacer2.TabIndex = 147;
      this.ttl_SingleFacer2.Text = "SINGLE FACER 2";
      this.ttl_SingleFacer2.TranslateText = false;
      // 
      // ttl_Splicer5
      // 
      this.ttl_Splicer5.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer5.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer5.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer5.Location = new System.Drawing.Point(564, 224);
      this.ttl_Splicer5.Name = "ttl_Splicer5";
      this.ttl_Splicer5.Size = new System.Drawing.Size(144, 92);
      this.ttl_Splicer5.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer5.TabIndex = 146;
      this.ttl_Splicer5.Text = "SPLICER 5";
      this.ttl_Splicer5.TranslateText = false;
      // 
      // ttl_Splicer2
      // 
      this.ttl_Splicer2.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer2.Location = new System.Drawing.Point(412, 322);
      this.ttl_Splicer2.Name = "ttl_Splicer2";
      this.ttl_Splicer2.Size = new System.Drawing.Size(144, 92);
      this.ttl_Splicer2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer2.TabIndex = 151;
      this.ttl_Splicer2.Text = "SPLICER 2";
      this.ttl_Splicer2.TranslateText = false;
      // 
      // ttl_SingleFacer1
      // 
      this.ttl_SingleFacer1.BackColor = System.Drawing.Color.Black;
      this.ttl_SingleFacer1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SingleFacer1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SingleFacer1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SingleFacer1.Location = new System.Drawing.Point(260, 322);
      this.ttl_SingleFacer1.Name = "ttl_SingleFacer1";
      this.ttl_SingleFacer1.Size = new System.Drawing.Size(144, 92);
      this.ttl_SingleFacer1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SingleFacer1.TabIndex = 150;
      this.ttl_SingleFacer1.Text = "SINGLE FACER 1";
      this.ttl_SingleFacer1.TranslateText = false;
      // 
      // ttl_SPlicer3
      // 
      this.ttl_SPlicer3.BackColor = System.Drawing.Color.Black;
      this.ttl_SPlicer3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SPlicer3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SPlicer3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SPlicer3.Location = new System.Drawing.Point(108, 322);
      this.ttl_SPlicer3.Name = "ttl_SPlicer3";
      this.ttl_SPlicer3.Size = new System.Drawing.Size(144, 92);
      this.ttl_SPlicer3.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SPlicer3.TabIndex = 149;
      this.ttl_SPlicer3.Text = "SPLICER 3";
      this.ttl_SPlicer3.TranslateText = false;
      // 
      // ttl_GlueUnit
      // 
      this.ttl_GlueUnit.BackColor = System.Drawing.Color.Black;
      this.ttl_GlueUnit.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GlueUnit.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_GlueUnit.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_GlueUnit.Location = new System.Drawing.Point(868, 322);
      this.ttl_GlueUnit.Name = "ttl_GlueUnit";
      this.ttl_GlueUnit.Size = new System.Drawing.Size(144, 92);
      this.ttl_GlueUnit.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_GlueUnit.TabIndex = 154;
      this.ttl_GlueUnit.Text = "GLUE UNIT";
      this.ttl_GlueUnit.TranslateText = false;
      // 
      // ttl_PreHeatersTern
      // 
      this.ttl_PreHeatersTern.BackColor = System.Drawing.Color.Black;
      this.ttl_PreHeatersTern.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreHeatersTern.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PreHeatersTern.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PreHeatersTern.Location = new System.Drawing.Point(716, 322);
      this.ttl_PreHeatersTern.Name = "ttl_PreHeatersTern";
      this.ttl_PreHeatersTern.Size = new System.Drawing.Size(144, 92);
      this.ttl_PreHeatersTern.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PreHeatersTern.TabIndex = 153;
      this.ttl_PreHeatersTern.Text = "P.H. TERN";
      this.ttl_PreHeatersTern.TranslateText = false;
      // 
      // ttl_Splicer1
      // 
      this.ttl_Splicer1.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer1.Location = new System.Drawing.Point(564, 322);
      this.ttl_Splicer1.Name = "ttl_Splicer1";
      this.ttl_Splicer1.Size = new System.Drawing.Size(144, 92);
      this.ttl_Splicer1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer1.TabIndex = 152;
      this.ttl_Splicer1.Text = "SPLICER 1";
      this.ttl_Splicer1.TranslateText = false;
      // 
      // ttl_DoubleBacker
      // 
      this.ttl_DoubleBacker.BackColor = System.Drawing.Color.Black;
      this.ttl_DoubleBacker.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DoubleBacker.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DoubleBacker.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DoubleBacker.Location = new System.Drawing.Point(1019, 322);
      this.ttl_DoubleBacker.Name = "ttl_DoubleBacker";
      this.ttl_DoubleBacker.Size = new System.Drawing.Size(144, 92);
      this.ttl_DoubleBacker.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DoubleBacker.TabIndex = 155;
      this.ttl_DoubleBacker.Text = "DOUBLE BACKER";
      this.ttl_DoubleBacker.TranslateText = false;
      // 
      // ttl_Rotary
      // 
      this.ttl_Rotary.BackColor = System.Drawing.Color.Black;
      this.ttl_Rotary.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Rotary.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Rotary.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Rotary.Location = new System.Drawing.Point(107, 469);
      this.ttl_Rotary.Name = "ttl_Rotary";
      this.ttl_Rotary.Size = new System.Drawing.Size(144, 92);
      this.ttl_Rotary.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Rotary.TabIndex = 156;
      this.ttl_Rotary.Text = "ROTARY";
      this.ttl_Rotary.TranslateText = true;
      // 
      // ttl_SlitterScorer
      // 
      this.ttl_SlitterScorer.BackColor = System.Drawing.Color.Black;
      this.ttl_SlitterScorer.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SlitterScorer.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SlitterScorer.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SlitterScorer.Location = new System.Drawing.Point(259, 469);
      this.ttl_SlitterScorer.Name = "ttl_SlitterScorer";
      this.ttl_SlitterScorer.Size = new System.Drawing.Size(144, 92);
      this.ttl_SlitterScorer.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SlitterScorer.TabIndex = 157;
      this.ttl_SlitterScorer.Text = "SLITTER SCORER";
      this.ttl_SlitterScorer.TranslateText = false;
      // 
      // ttl_WebDirector
      // 
      this.ttl_WebDirector.BackColor = System.Drawing.Color.Black;
      this.ttl_WebDirector.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_WebDirector.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_WebDirector.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_WebDirector.Location = new System.Drawing.Point(411, 469);
      this.ttl_WebDirector.Name = "ttl_WebDirector";
      this.ttl_WebDirector.Size = new System.Drawing.Size(144, 92);
      this.ttl_WebDirector.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_WebDirector.TabIndex = 158;
      this.ttl_WebDirector.Text = "WEB DIRECTOR";
      this.ttl_WebDirector.TranslateText = false;
      // 
      // ttl_Knife
      // 
      this.ttl_Knife.BackColor = System.Drawing.Color.Black;
      this.ttl_Knife.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Knife.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Knife.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Knife.Location = new System.Drawing.Point(563, 469);
      this.ttl_Knife.Name = "ttl_Knife";
      this.ttl_Knife.Size = new System.Drawing.Size(144, 92);
      this.ttl_Knife.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Knife.TabIndex = 159;
      this.ttl_Knife.Text = "KNIFE";
      this.ttl_Knife.TranslateText = false;
      // 
      // ttl_StackerLow
      // 
      this.ttl_StackerLow.BackColor = System.Drawing.Color.Black;
      this.ttl_StackerLow.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StackerLow.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_StackerLow.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_StackerLow.Location = new System.Drawing.Point(715, 469);
      this.ttl_StackerLow.Name = "ttl_StackerLow";
      this.ttl_StackerLow.Size = new System.Drawing.Size(144, 92);
      this.ttl_StackerLow.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_StackerLow.TabIndex = 160;
      this.ttl_StackerLow.Text = "STACKER 1";
      this.ttl_StackerLow.TranslateText = false;
      // 
      // ttl_StackrMid
      // 
      this.ttl_StackrMid.BackColor = System.Drawing.Color.Black;
      this.ttl_StackrMid.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StackrMid.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_StackrMid.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_StackrMid.Location = new System.Drawing.Point(867, 469);
      this.ttl_StackrMid.Name = "ttl_StackrMid";
      this.ttl_StackrMid.Size = new System.Drawing.Size(144, 92);
      this.ttl_StackrMid.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_StackrMid.TabIndex = 161;
      this.ttl_StackrMid.Text = "STACKER 2";
      this.ttl_StackrMid.TranslateText = false;
      // 
      // ttl_StackerHigh
      // 
      this.ttl_StackerHigh.BackColor = System.Drawing.Color.Black;
      this.ttl_StackerHigh.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StackerHigh.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_StackerHigh.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_StackerHigh.Location = new System.Drawing.Point(1019, 469);
      this.ttl_StackerHigh.Name = "ttl_StackerHigh";
      this.ttl_StackerHigh.Size = new System.Drawing.Size(144, 92);
      this.ttl_StackerHigh.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_StackerHigh.TabIndex = 162;
      this.ttl_StackerHigh.Text = "STACKER 3";
      this.ttl_StackerHigh.TranslateText = false;
      // 
      // chk_GlueUnit
      // 
      this.chk_GlueUnit.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_GlueUnit.Checked = true;
      this.chk_GlueUnit.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_GlueUnit.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_GlueUnit.ImagePassword = null;
      this.chk_GlueUnit.Location = new System.Drawing.Point(954, 369);
      this.chk_GlueUnit.Name = "chk_GlueUnit";
      this.chk_GlueUnit.ReadOnly = true;
      this.chk_GlueUnit.Size = new System.Drawing.Size(44, 22);
      this.chk_GlueUnit.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_GlueUnit.TabIndex = 163;
      this.chk_GlueUnit.TabStop = false;
      this.chk_GlueUnit.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_GlueUnit.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_GlueUnit.TranslateText = false;
      this.chk_GlueUnit.OnCheckedChanged += new System.EventHandler(this.chk_GlueUnit_OnCheckedChange);
      // 
      // stb_Splicer7
      // 
      this.stb_Splicer7.Active = false;
      this.stb_Splicer7.Alarmed = false;
      this.stb_Splicer7.Auto = false;
      this.stb_Splicer7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer7.FillBackColorWithImage = false;
      this.stb_Splicer7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer7.FramePresent = false;
      this.stb_Splicer7.HighLightEnable = false;
      this.stb_Splicer7.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer7.ImagePassword = null;
      this.stb_Splicer7.Inemergency = false;
      this.stb_Splicer7.InLocal = false;
      this.stb_Splicer7.IsButton = true;
      this.stb_Splicer7.IsForceToInvisible = false;
      this.stb_Splicer7.Location = new System.Drawing.Point(116, 249);
      this.stb_Splicer7.Name = "stb_Splicer7";
      this.stb_Splicer7.OnLine = false;
      this.stb_Splicer7.ShowImage = true;
      this.stb_Splicer7.ShowText = false;
      this.stb_Splicer7.Size = new System.Drawing.Size(72, 60);
      this.stb_Splicer7.Switching = false;
      this.stb_Splicer7.TabIndex = 164;
      this.stb_Splicer7.TabStop = false;
      this.stb_Splicer7.TranslateText = false;
      this.stb_Splicer7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer7.UserDefinedActiveImage")));
      this.stb_Splicer7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer7.UserDefinedImage")));
      this.stb_Splicer7.Warned = false;
      this.stb_Splicer7.Click += new System.EventHandler(this.stb_Splicer7_Click);
      // 
      // stb_Splicer6
      // 
      this.stb_Splicer6.Active = false;
      this.stb_Splicer6.Alarmed = false;
      this.stb_Splicer6.Auto = false;
      this.stb_Splicer6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer6.FillBackColorWithImage = false;
      this.stb_Splicer6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer6.FramePresent = false;
      this.stb_Splicer6.HighLightEnable = false;
      this.stb_Splicer6.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer6.ImagePassword = null;
      this.stb_Splicer6.Inemergency = false;
      this.stb_Splicer6.InLocal = false;
      this.stb_Splicer6.IsButton = true;
      this.stb_Splicer6.IsForceToInvisible = false;
      this.stb_Splicer6.Location = new System.Drawing.Point(422, 249);
      this.stb_Splicer6.Name = "stb_Splicer6";
      this.stb_Splicer6.OnLine = false;
      this.stb_Splicer6.ShowImage = true;
      this.stb_Splicer6.ShowText = false;
      this.stb_Splicer6.Size = new System.Drawing.Size(72, 60);
      this.stb_Splicer6.Switching = false;
      this.stb_Splicer6.TabIndex = 165;
      this.stb_Splicer6.TabStop = false;
      this.stb_Splicer6.TranslateText = false;
      this.stb_Splicer6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer6.UserDefinedActiveImage")));
      this.stb_Splicer6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer6.UserDefinedImage")));
      this.stb_Splicer6.Warned = false;
      this.stb_Splicer6.Click += new System.EventHandler(this.stb_Splicer6_Click);
      // 
      // stb_Splicer5
      // 
      this.stb_Splicer5.Active = false;
      this.stb_Splicer5.Alarmed = false;
      this.stb_Splicer5.Auto = false;
      this.stb_Splicer5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer5.FillBackColorWithImage = false;
      this.stb_Splicer5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer5.FramePresent = false;
      this.stb_Splicer5.HighLightEnable = false;
      this.stb_Splicer5.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer5.ImagePassword = null;
      this.stb_Splicer5.Inemergency = false;
      this.stb_Splicer5.InLocal = false;
      this.stb_Splicer5.IsButton = true;
      this.stb_Splicer5.IsForceToInvisible = false;
      this.stb_Splicer5.Location = new System.Drawing.Point(574, 249);
      this.stb_Splicer5.Name = "stb_Splicer5";
      this.stb_Splicer5.OnLine = false;
      this.stb_Splicer5.ShowImage = true;
      this.stb_Splicer5.ShowText = false;
      this.stb_Splicer5.Size = new System.Drawing.Size(72, 60);
      this.stb_Splicer5.Switching = false;
      this.stb_Splicer5.TabIndex = 166;
      this.stb_Splicer5.TabStop = false;
      this.stb_Splicer5.TranslateText = false;
      this.stb_Splicer5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer5.UserDefinedActiveImage")));
      this.stb_Splicer5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer5.UserDefinedImage")));
      this.stb_Splicer5.Warned = false;
      this.stb_Splicer5.Click += new System.EventHandler(this.stb_Splicer5_Click);
      // 
      // stb_Splicer3
      // 
      this.stb_Splicer3.Active = false;
      this.stb_Splicer3.Alarmed = false;
      this.stb_Splicer3.Auto = false;
      this.stb_Splicer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer3.FillBackColorWithImage = false;
      this.stb_Splicer3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer3.FramePresent = false;
      this.stb_Splicer3.HighLightEnable = false;
      this.stb_Splicer3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer3.ImagePassword = null;
      this.stb_Splicer3.Inemergency = false;
      this.stb_Splicer3.InLocal = false;
      this.stb_Splicer3.IsButton = true;
      this.stb_Splicer3.IsForceToInvisible = false;
      this.stb_Splicer3.Location = new System.Drawing.Point(116, 347);
      this.stb_Splicer3.Name = "stb_Splicer3";
      this.stb_Splicer3.OnLine = false;
      this.stb_Splicer3.ShowImage = true;
      this.stb_Splicer3.ShowText = false;
      this.stb_Splicer3.Size = new System.Drawing.Size(72, 69);
      this.stb_Splicer3.Switching = false;
      this.stb_Splicer3.TabIndex = 167;
      this.stb_Splicer3.TabStop = false;
      this.stb_Splicer3.TranslateText = false;
      this.stb_Splicer3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer3.UserDefinedActiveImage")));
      this.stb_Splicer3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer3.UserDefinedImage")));
      this.stb_Splicer3.Warned = false;
      this.stb_Splicer3.Click += new System.EventHandler(this.stb_Splicer3_Click);
      // 
      // stb_Splicer1
      // 
      this.stb_Splicer1.Active = false;
      this.stb_Splicer1.Alarmed = false;
      this.stb_Splicer1.Auto = false;
      this.stb_Splicer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer1.FillBackColorWithImage = false;
      this.stb_Splicer1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer1.FramePresent = false;
      this.stb_Splicer1.HighLightEnable = false;
      this.stb_Splicer1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer1.ImagePassword = null;
      this.stb_Splicer1.Inemergency = false;
      this.stb_Splicer1.InLocal = false;
      this.stb_Splicer1.IsButton = true;
      this.stb_Splicer1.IsForceToInvisible = false;
      this.stb_Splicer1.Location = new System.Drawing.Point(572, 347);
      this.stb_Splicer1.Name = "stb_Splicer1";
      this.stb_Splicer1.OnLine = false;
      this.stb_Splicer1.ShowImage = true;
      this.stb_Splicer1.ShowText = false;
      this.stb_Splicer1.Size = new System.Drawing.Size(72, 69);
      this.stb_Splicer1.Switching = false;
      this.stb_Splicer1.TabIndex = 168;
      this.stb_Splicer1.TabStop = false;
      this.stb_Splicer1.TranslateText = false;
      this.stb_Splicer1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer1.UserDefinedActiveImage")));
      this.stb_Splicer1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer1.UserDefinedImage")));
      this.stb_Splicer1.Warned = false;
      this.stb_Splicer1.Click += new System.EventHandler(this.stb_Splicer1_Click);
      // 
      // stb_Splicer4
      // 
      this.stb_Splicer4.Active = false;
      this.stb_Splicer4.Alarmed = false;
      this.stb_Splicer4.Auto = false;
      this.stb_Splicer4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer4.FillBackColorWithImage = false;
      this.stb_Splicer4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer4.FramePresent = false;
      this.stb_Splicer4.HighLightEnable = false;
      this.stb_Splicer4.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer4.ImagePassword = null;
      this.stb_Splicer4.Inemergency = false;
      this.stb_Splicer4.InLocal = false;
      this.stb_Splicer4.IsButton = true;
      this.stb_Splicer4.IsForceToInvisible = false;
      this.stb_Splicer4.Location = new System.Drawing.Point(878, 249);
      this.stb_Splicer4.Name = "stb_Splicer4";
      this.stb_Splicer4.OnLine = false;
      this.stb_Splicer4.ShowImage = true;
      this.stb_Splicer4.ShowText = false;
      this.stb_Splicer4.Size = new System.Drawing.Size(72, 60);
      this.stb_Splicer4.Switching = false;
      this.stb_Splicer4.TabIndex = 169;
      this.stb_Splicer4.TabStop = false;
      this.stb_Splicer4.TranslateText = false;
      this.stb_Splicer4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer4.UserDefinedActiveImage")));
      this.stb_Splicer4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer4.UserDefinedImage")));
      this.stb_Splicer4.Warned = false;
      this.stb_Splicer4.Click += new System.EventHandler(this.stb_Splicer4_Click);
      // 
      // stb_Splicer2
      // 
      this.stb_Splicer2.Active = false;
      this.stb_Splicer2.Alarmed = false;
      this.stb_Splicer2.Auto = false;
      this.stb_Splicer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Splicer2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Splicer2.FillBackColorWithImage = false;
      this.stb_Splicer2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Splicer2.FramePresent = false;
      this.stb_Splicer2.HighLightEnable = false;
      this.stb_Splicer2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Splicer2.ImagePassword = null;
      this.stb_Splicer2.Inemergency = false;
      this.stb_Splicer2.InLocal = false;
      this.stb_Splicer2.IsButton = true;
      this.stb_Splicer2.IsForceToInvisible = false;
      this.stb_Splicer2.Location = new System.Drawing.Point(420, 347);
      this.stb_Splicer2.Name = "stb_Splicer2";
      this.stb_Splicer2.OnLine = false;
      this.stb_Splicer2.ShowImage = true;
      this.stb_Splicer2.ShowText = false;
      this.stb_Splicer2.Size = new System.Drawing.Size(72, 69);
      this.stb_Splicer2.Switching = false;
      this.stb_Splicer2.TabIndex = 170;
      this.stb_Splicer2.TabStop = false;
      this.stb_Splicer2.TranslateText = false;
      this.stb_Splicer2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer2.UserDefinedActiveImage")));
      this.stb_Splicer2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Splicer2.UserDefinedImage")));
      this.stb_Splicer2.Warned = false;
      this.stb_Splicer2.Click += new System.EventHandler(this.stb_Splicer2_Click);
      // 
      // stb_SingleFacer3
      // 
      this.stb_SingleFacer3.Active = false;
      this.stb_SingleFacer3.Alarmed = false;
      this.stb_SingleFacer3.Auto = false;
      this.stb_SingleFacer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_SingleFacer3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SingleFacer3.FillBackColorWithImage = false;
      this.stb_SingleFacer3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_SingleFacer3.FramePresent = false;
      this.stb_SingleFacer3.HighLightEnable = false;
      this.stb_SingleFacer3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_SingleFacer3.ImagePassword = null;
      this.stb_SingleFacer3.Inemergency = false;
      this.stb_SingleFacer3.InLocal = false;
      this.stb_SingleFacer3.IsButton = true;
      this.stb_SingleFacer3.IsForceToInvisible = false;
      this.stb_SingleFacer3.Location = new System.Drawing.Point(270, 249);
      this.stb_SingleFacer3.Name = "stb_SingleFacer3";
      this.stb_SingleFacer3.OnLine = false;
      this.stb_SingleFacer3.ShowImage = true;
      this.stb_SingleFacer3.ShowText = false;
      this.stb_SingleFacer3.Size = new System.Drawing.Size(72, 60);
      this.stb_SingleFacer3.Switching = false;
      this.stb_SingleFacer3.TabIndex = 171;
      this.stb_SingleFacer3.TabStop = false;
      this.stb_SingleFacer3.TranslateText = false;
      this.stb_SingleFacer3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SingleFacer3.UserDefinedActiveImage")));
      this.stb_SingleFacer3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SingleFacer3.UserDefinedImage")));
      this.stb_SingleFacer3.Warned = false;
      this.stb_SingleFacer3.Click += new System.EventHandler(this.stb_SingleFacer3_Click);
      // 
      // stb_SingleFacer2
      // 
      this.stb_SingleFacer2.Active = false;
      this.stb_SingleFacer2.Alarmed = false;
      this.stb_SingleFacer2.Auto = false;
      this.stb_SingleFacer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_SingleFacer2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SingleFacer2.FillBackColorWithImage = false;
      this.stb_SingleFacer2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_SingleFacer2.FramePresent = false;
      this.stb_SingleFacer2.HighLightEnable = false;
      this.stb_SingleFacer2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_SingleFacer2.ImagePassword = null;
      this.stb_SingleFacer2.Inemergency = false;
      this.stb_SingleFacer2.InLocal = false;
      this.stb_SingleFacer2.IsButton = true;
      this.stb_SingleFacer2.IsForceToInvisible = false;
      this.stb_SingleFacer2.Location = new System.Drawing.Point(726, 249);
      this.stb_SingleFacer2.Name = "stb_SingleFacer2";
      this.stb_SingleFacer2.OnLine = false;
      this.stb_SingleFacer2.ShowImage = true;
      this.stb_SingleFacer2.ShowText = false;
      this.stb_SingleFacer2.Size = new System.Drawing.Size(72, 60);
      this.stb_SingleFacer2.Switching = false;
      this.stb_SingleFacer2.TabIndex = 172;
      this.stb_SingleFacer2.TabStop = false;
      this.stb_SingleFacer2.TranslateText = false;
      this.stb_SingleFacer2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SingleFacer2.UserDefinedActiveImage")));
      this.stb_SingleFacer2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SingleFacer2.UserDefinedImage")));
      this.stb_SingleFacer2.Warned = false;
      this.stb_SingleFacer2.Click += new System.EventHandler(this.stb_SingleFacer2_Click);
      // 
      // stb_SingleFacer1
      // 
      this.stb_SingleFacer1.Active = false;
      this.stb_SingleFacer1.Alarmed = false;
      this.stb_SingleFacer1.Auto = false;
      this.stb_SingleFacer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_SingleFacer1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SingleFacer1.FillBackColorWithImage = false;
      this.stb_SingleFacer1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_SingleFacer1.FramePresent = false;
      this.stb_SingleFacer1.HighLightEnable = false;
      this.stb_SingleFacer1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_SingleFacer1.ImagePassword = null;
      this.stb_SingleFacer1.Inemergency = false;
      this.stb_SingleFacer1.InLocal = false;
      this.stb_SingleFacer1.IsButton = true;
      this.stb_SingleFacer1.IsForceToInvisible = false;
      this.stb_SingleFacer1.Location = new System.Drawing.Point(268, 347);
      this.stb_SingleFacer1.Name = "stb_SingleFacer1";
      this.stb_SingleFacer1.OnLine = false;
      this.stb_SingleFacer1.ShowImage = true;
      this.stb_SingleFacer1.ShowText = false;
      this.stb_SingleFacer1.Size = new System.Drawing.Size(72, 69);
      this.stb_SingleFacer1.Switching = false;
      this.stb_SingleFacer1.TabIndex = 173;
      this.stb_SingleFacer1.TabStop = false;
      this.stb_SingleFacer1.TranslateText = false;
      this.stb_SingleFacer1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SingleFacer1.UserDefinedActiveImage")));
      this.stb_SingleFacer1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SingleFacer1.UserDefinedImage")));
      this.stb_SingleFacer1.Warned = false;
      this.stb_SingleFacer1.Click += new System.EventHandler(this.stb_SingleFacer1_Click);
      // 
      // stb_PreHeatersTern
      // 
      this.stb_PreHeatersTern.Active = false;
      this.stb_PreHeatersTern.Alarmed = false;
      this.stb_PreHeatersTern.Auto = false;
      this.stb_PreHeatersTern.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_PreHeatersTern.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_PreHeatersTern.FillBackColorWithImage = false;
      this.stb_PreHeatersTern.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_PreHeatersTern.FramePresent = false;
      this.stb_PreHeatersTern.HighLightEnable = false;
      this.stb_PreHeatersTern.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_PreHeatersTern.ImagePassword = null;
      this.stb_PreHeatersTern.Inemergency = false;
      this.stb_PreHeatersTern.InLocal = false;
      this.stb_PreHeatersTern.IsButton = true;
      this.stb_PreHeatersTern.IsForceToInvisible = false;
      this.stb_PreHeatersTern.Location = new System.Drawing.Point(724, 347);
      this.stb_PreHeatersTern.Name = "stb_PreHeatersTern";
      this.stb_PreHeatersTern.OnLine = false;
      this.stb_PreHeatersTern.ShowImage = true;
      this.stb_PreHeatersTern.ShowText = false;
      this.stb_PreHeatersTern.Size = new System.Drawing.Size(72, 69);
      this.stb_PreHeatersTern.Switching = false;
      this.stb_PreHeatersTern.TabIndex = 174;
      this.stb_PreHeatersTern.TabStop = false;
      this.stb_PreHeatersTern.TranslateText = false;
      this.stb_PreHeatersTern.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_PreHeatersTern.UserDefinedActiveImage")));
      this.stb_PreHeatersTern.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_PreHeatersTern.UserDefinedImage")));
      this.stb_PreHeatersTern.Warned = false;
      this.stb_PreHeatersTern.Click += new System.EventHandler(this.stb_PreHeatersTern_Click);
      // 
      // stb_GlueUnit
      // 
      this.stb_GlueUnit.Active = false;
      this.stb_GlueUnit.Alarmed = false;
      this.stb_GlueUnit.Auto = false;
      this.stb_GlueUnit.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_GlueUnit.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_GlueUnit.FillBackColorWithImage = false;
      this.stb_GlueUnit.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_GlueUnit.FramePresent = false;
      this.stb_GlueUnit.HighLightEnable = false;
      this.stb_GlueUnit.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_GlueUnit.ImagePassword = null;
      this.stb_GlueUnit.Inemergency = false;
      this.stb_GlueUnit.InLocal = false;
      this.stb_GlueUnit.IsButton = true;
      this.stb_GlueUnit.IsForceToInvisible = false;
      this.stb_GlueUnit.Location = new System.Drawing.Point(876, 347);
      this.stb_GlueUnit.Name = "stb_GlueUnit";
      this.stb_GlueUnit.OnLine = false;
      this.stb_GlueUnit.ShowImage = true;
      this.stb_GlueUnit.ShowText = false;
      this.stb_GlueUnit.Size = new System.Drawing.Size(72, 69);
      this.stb_GlueUnit.Switching = false;
      this.stb_GlueUnit.TabIndex = 175;
      this.stb_GlueUnit.TabStop = false;
      this.stb_GlueUnit.TranslateText = false;
      this.stb_GlueUnit.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_GlueUnit.UserDefinedActiveImage")));
      this.stb_GlueUnit.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_GlueUnit.UserDefinedImage")));
      this.stb_GlueUnit.Warned = false;
      this.stb_GlueUnit.Click += new System.EventHandler(this.stb_GlueUnit_Click);
      // 
      // stb_DoubleBacker
      // 
      this.stb_DoubleBacker.Active = false;
      this.stb_DoubleBacker.Alarmed = false;
      this.stb_DoubleBacker.Auto = false;
      this.stb_DoubleBacker.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_DoubleBacker.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_DoubleBacker.FillBackColorWithImage = false;
      this.stb_DoubleBacker.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_DoubleBacker.FramePresent = false;
      this.stb_DoubleBacker.HighLightEnable = false;
      this.stb_DoubleBacker.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_DoubleBacker.ImagePassword = null;
      this.stb_DoubleBacker.Inemergency = false;
      this.stb_DoubleBacker.InLocal = false;
      this.stb_DoubleBacker.IsButton = true;
      this.stb_DoubleBacker.IsForceToInvisible = false;
      this.stb_DoubleBacker.Location = new System.Drawing.Point(1028, 347);
      this.stb_DoubleBacker.Name = "stb_DoubleBacker";
      this.stb_DoubleBacker.OnLine = false;
      this.stb_DoubleBacker.ShowImage = true;
      this.stb_DoubleBacker.ShowText = false;
      this.stb_DoubleBacker.Size = new System.Drawing.Size(72, 69);
      this.stb_DoubleBacker.Switching = false;
      this.stb_DoubleBacker.TabIndex = 176;
      this.stb_DoubleBacker.TabStop = false;
      this.stb_DoubleBacker.TranslateText = false;
      this.stb_DoubleBacker.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_DoubleBacker.UserDefinedActiveImage")));
      this.stb_DoubleBacker.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_DoubleBacker.UserDefinedImage")));
      this.stb_DoubleBacker.Warned = false;
      this.stb_DoubleBacker.Click += new System.EventHandler(this.stb_DoubleBacker_Click);
      // 
      // stb_Rotary
      // 
      this.stb_Rotary.Active = false;
      this.stb_Rotary.Alarmed = false;
      this.stb_Rotary.Auto = false;
      this.stb_Rotary.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Rotary.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Rotary.FillBackColorWithImage = false;
      this.stb_Rotary.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Rotary.FramePresent = false;
      this.stb_Rotary.HighLightEnable = false;
      this.stb_Rotary.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Rotary.ImagePassword = null;
      this.stb_Rotary.Inemergency = false;
      this.stb_Rotary.InLocal = false;
      this.stb_Rotary.IsButton = true;
      this.stb_Rotary.IsForceToInvisible = false;
      this.stb_Rotary.Location = new System.Drawing.Point(115, 494);
      this.stb_Rotary.Name = "stb_Rotary";
      this.stb_Rotary.OnLine = false;
      this.stb_Rotary.ShowImage = true;
      this.stb_Rotary.ShowText = false;
      this.stb_Rotary.Size = new System.Drawing.Size(72, 69);
      this.stb_Rotary.Switching = false;
      this.stb_Rotary.TabIndex = 177;
      this.stb_Rotary.TabStop = false;
      this.stb_Rotary.TranslateText = false;
      this.stb_Rotary.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Rotary.UserDefinedActiveImage")));
      this.stb_Rotary.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Rotary.UserDefinedImage")));
      this.stb_Rotary.Warned = false;
      this.stb_Rotary.Click += new System.EventHandler(this.stb_Rotary_Click);
      // 
      // stb_SlitterScorer
      // 
      this.stb_SlitterScorer.Active = false;
      this.stb_SlitterScorer.Alarmed = false;
      this.stb_SlitterScorer.Auto = false;
      this.stb_SlitterScorer.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_SlitterScorer.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SlitterScorer.FillBackColorWithImage = false;
      this.stb_SlitterScorer.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_SlitterScorer.FramePresent = false;
      this.stb_SlitterScorer.HighLightEnable = false;
      this.stb_SlitterScorer.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_SlitterScorer.ImagePassword = null;
      this.stb_SlitterScorer.Inemergency = false;
      this.stb_SlitterScorer.InLocal = false;
      this.stb_SlitterScorer.IsButton = true;
      this.stb_SlitterScorer.IsForceToInvisible = false;
      this.stb_SlitterScorer.Location = new System.Drawing.Point(267, 494);
      this.stb_SlitterScorer.Name = "stb_SlitterScorer";
      this.stb_SlitterScorer.OnLine = false;
      this.stb_SlitterScorer.ShowImage = true;
      this.stb_SlitterScorer.ShowText = false;
      this.stb_SlitterScorer.Size = new System.Drawing.Size(72, 69);
      this.stb_SlitterScorer.Switching = false;
      this.stb_SlitterScorer.TabIndex = 178;
      this.stb_SlitterScorer.TabStop = false;
      this.stb_SlitterScorer.TranslateText = false;
      this.stb_SlitterScorer.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SlitterScorer.UserDefinedActiveImage")));
      this.stb_SlitterScorer.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SlitterScorer.UserDefinedImage")));
      this.stb_SlitterScorer.Warned = false;
      this.stb_SlitterScorer.Click += new System.EventHandler(this.stb_SlitterScorer_Click);
      // 
      // stb_WebDirector
      // 
      this.stb_WebDirector.Active = false;
      this.stb_WebDirector.Alarmed = false;
      this.stb_WebDirector.Auto = false;
      this.stb_WebDirector.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_WebDirector.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_WebDirector.FillBackColorWithImage = false;
      this.stb_WebDirector.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_WebDirector.FramePresent = false;
      this.stb_WebDirector.HighLightEnable = false;
      this.stb_WebDirector.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_WebDirector.ImagePassword = null;
      this.stb_WebDirector.Inemergency = false;
      this.stb_WebDirector.InLocal = false;
      this.stb_WebDirector.IsButton = true;
      this.stb_WebDirector.IsForceToInvisible = false;
      this.stb_WebDirector.Location = new System.Drawing.Point(419, 494);
      this.stb_WebDirector.Name = "stb_WebDirector";
      this.stb_WebDirector.OnLine = false;
      this.stb_WebDirector.ShowImage = true;
      this.stb_WebDirector.ShowText = false;
      this.stb_WebDirector.Size = new System.Drawing.Size(72, 69);
      this.stb_WebDirector.Switching = false;
      this.stb_WebDirector.TabIndex = 179;
      this.stb_WebDirector.TabStop = false;
      this.stb_WebDirector.TranslateText = false;
      this.stb_WebDirector.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_WebDirector.UserDefinedActiveImage")));
      this.stb_WebDirector.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_WebDirector.UserDefinedImage")));
      this.stb_WebDirector.Warned = false;
      this.stb_WebDirector.Click += new System.EventHandler(this.stb_WebDirector_Click);
      // 
      // stb_Knife
      // 
      this.stb_Knife.Active = false;
      this.stb_Knife.Alarmed = false;
      this.stb_Knife.Auto = false;
      this.stb_Knife.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Knife.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Knife.FillBackColorWithImage = false;
      this.stb_Knife.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Knife.FramePresent = false;
      this.stb_Knife.HighLightEnable = false;
      this.stb_Knife.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Knife.ImagePassword = null;
      this.stb_Knife.Inemergency = false;
      this.stb_Knife.InLocal = false;
      this.stb_Knife.IsButton = true;
      this.stb_Knife.IsForceToInvisible = false;
      this.stb_Knife.Location = new System.Drawing.Point(571, 494);
      this.stb_Knife.Name = "stb_Knife";
      this.stb_Knife.OnLine = false;
      this.stb_Knife.ShowImage = true;
      this.stb_Knife.ShowText = false;
      this.stb_Knife.Size = new System.Drawing.Size(72, 69);
      this.stb_Knife.Switching = false;
      this.stb_Knife.TabIndex = 180;
      this.stb_Knife.TabStop = false;
      this.stb_Knife.TranslateText = false;
      this.stb_Knife.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Knife.UserDefinedActiveImage")));
      this.stb_Knife.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Knife.UserDefinedImage")));
      this.stb_Knife.Warned = false;
      this.stb_Knife.Click += new System.EventHandler(this.stb_Knife_Click);
      // 
      // stb_StackerLow
      // 
      this.stb_StackerLow.Active = false;
      this.stb_StackerLow.Alarmed = false;
      this.stb_StackerLow.Auto = false;
      this.stb_StackerLow.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_StackerLow.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_StackerLow.FillBackColorWithImage = false;
      this.stb_StackerLow.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_StackerLow.FramePresent = false;
      this.stb_StackerLow.HighLightEnable = false;
      this.stb_StackerLow.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_StackerLow.ImagePassword = null;
      this.stb_StackerLow.Inemergency = false;
      this.stb_StackerLow.InLocal = false;
      this.stb_StackerLow.IsButton = true;
      this.stb_StackerLow.IsForceToInvisible = false;
      this.stb_StackerLow.Location = new System.Drawing.Point(723, 494);
      this.stb_StackerLow.Name = "stb_StackerLow";
      this.stb_StackerLow.OnLine = false;
      this.stb_StackerLow.ShowImage = true;
      this.stb_StackerLow.ShowText = false;
      this.stb_StackerLow.Size = new System.Drawing.Size(72, 69);
      this.stb_StackerLow.Switching = false;
      this.stb_StackerLow.TabIndex = 181;
      this.stb_StackerLow.TabStop = false;
      this.stb_StackerLow.TranslateText = false;
      this.stb_StackerLow.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_StackerLow.UserDefinedActiveImage")));
      this.stb_StackerLow.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_StackerLow.UserDefinedImage")));
      this.stb_StackerLow.Warned = false;
      this.stb_StackerLow.Click += new System.EventHandler(this.stb_StackerLow_Click);
      // 
      // stb_StackerMid
      // 
      this.stb_StackerMid.Active = false;
      this.stb_StackerMid.Alarmed = false;
      this.stb_StackerMid.Auto = false;
      this.stb_StackerMid.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_StackerMid.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_StackerMid.FillBackColorWithImage = false;
      this.stb_StackerMid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_StackerMid.FramePresent = false;
      this.stb_StackerMid.HighLightEnable = false;
      this.stb_StackerMid.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_StackerMid.ImagePassword = null;
      this.stb_StackerMid.Inemergency = false;
      this.stb_StackerMid.InLocal = false;
      this.stb_StackerMid.IsButton = true;
      this.stb_StackerMid.IsForceToInvisible = false;
      this.stb_StackerMid.Location = new System.Drawing.Point(875, 494);
      this.stb_StackerMid.Name = "stb_StackerMid";
      this.stb_StackerMid.OnLine = false;
      this.stb_StackerMid.ShowImage = true;
      this.stb_StackerMid.ShowText = false;
      this.stb_StackerMid.Size = new System.Drawing.Size(72, 69);
      this.stb_StackerMid.Switching = false;
      this.stb_StackerMid.TabIndex = 182;
      this.stb_StackerMid.TabStop = false;
      this.stb_StackerMid.TranslateText = false;
      this.stb_StackerMid.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_StackerMid.UserDefinedActiveImage")));
      this.stb_StackerMid.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_StackerMid.UserDefinedImage")));
      this.stb_StackerMid.Warned = false;
      this.stb_StackerMid.Click += new System.EventHandler(this.stb_StackerMid_Click);
      // 
      // stb_StackerHigh
      // 
      this.stb_StackerHigh.Active = false;
      this.stb_StackerHigh.Alarmed = false;
      this.stb_StackerHigh.Auto = false;
      this.stb_StackerHigh.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_StackerHigh.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_StackerHigh.FillBackColorWithImage = false;
      this.stb_StackerHigh.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_StackerHigh.FramePresent = false;
      this.stb_StackerHigh.HighLightEnable = false;
      this.stb_StackerHigh.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_StackerHigh.ImagePassword = null;
      this.stb_StackerHigh.Inemergency = false;
      this.stb_StackerHigh.InLocal = false;
      this.stb_StackerHigh.IsButton = true;
      this.stb_StackerHigh.IsForceToInvisible = false;
      this.stb_StackerHigh.Location = new System.Drawing.Point(1027, 494);
      this.stb_StackerHigh.Name = "stb_StackerHigh";
      this.stb_StackerHigh.OnLine = false;
      this.stb_StackerHigh.ShowImage = true;
      this.stb_StackerHigh.ShowText = false;
      this.stb_StackerHigh.Size = new System.Drawing.Size(72, 60);
      this.stb_StackerHigh.Switching = false;
      this.stb_StackerHigh.TabIndex = 183;
      this.stb_StackerHigh.TabStop = false;
      this.stb_StackerHigh.TranslateText = false;
      this.stb_StackerHigh.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_StackerHigh.UserDefinedActiveImage")));
      this.stb_StackerHigh.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_StackerHigh.UserDefinedImage")));
      this.stb_StackerHigh.Warned = false;
      this.stb_StackerHigh.Click += new System.EventHandler(this.stb_StackerHigh_Click);
      // 
      // ttl_Various
      // 
      this.ttl_Various.BackColor = System.Drawing.Color.Black;
      this.ttl_Various.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Various.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Various.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Various.Location = new System.Drawing.Point(96, 583);
      this.ttl_Various.Name = "ttl_Various";
      this.ttl_Various.Size = new System.Drawing.Size(1067, 129);
      this.ttl_Various.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Various.TabIndex = 184;
      this.ttl_Various.TabStop = false;
      this.ttl_Various.Text = "VARIOUS CONFIGURATION";
      this.ttl_Various.TranslateText = false;
      // 
      // stb_AOC
      // 
      this.stb_AOC.Active = false;
      this.stb_AOC.Alarmed = false;
      this.stb_AOC.Auto = false;
      this.stb_AOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_AOC.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_AOC.FillBackColorWithImage = false;
      this.stb_AOC.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_AOC.FramePresent = false;
      this.stb_AOC.HighLightEnable = false;
      this.stb_AOC.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_AOC.ImagePassword = null;
      this.stb_AOC.Inemergency = false;
      this.stb_AOC.InLocal = false;
      this.stb_AOC.IsButton = true;
      this.stb_AOC.IsForceToInvisible = false;
      this.stb_AOC.Location = new System.Drawing.Point(115, 643);
      this.stb_AOC.Name = "stb_AOC";
      this.stb_AOC.OnLine = false;
      this.stb_AOC.ShowImage = true;
      this.stb_AOC.ShowText = false;
      this.stb_AOC.Size = new System.Drawing.Size(72, 69);
      this.stb_AOC.Switching = false;
      this.stb_AOC.TabIndex = 187;
      this.stb_AOC.TabStop = false;
      this.stb_AOC.TranslateText = false;
      this.stb_AOC.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_AOC.UserDefinedActiveImage")));
      this.stb_AOC.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_AOC.UserDefinedImage")));
      this.stb_AOC.Warned = false;
      this.stb_AOC.Click += new System.EventHandler(this.stb_AOC_Click);
      // 
      // ttl_AOC
      // 
      this.ttl_AOC.BackColor = System.Drawing.Color.Black;
      this.ttl_AOC.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_AOC.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_AOC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_AOC.Location = new System.Drawing.Point(107, 620);
      this.ttl_AOC.Name = "ttl_AOC";
      this.ttl_AOC.Size = new System.Drawing.Size(144, 92);
      this.ttl_AOC.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_AOC.TabIndex = 186;
      this.ttl_AOC.Text = "AOC - WEC";
      this.ttl_AOC.TranslateText = false;
      // 
      // chk_AOC
      // 
      this.chk_AOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AOC.Checked = true;
      this.chk_AOC.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_AOC.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_AOC.ImagePassword = null;
      this.chk_AOC.Location = new System.Drawing.Point(193, 667);
      this.chk_AOC.Name = "chk_AOC";
      this.chk_AOC.ReadOnly = true;
      this.chk_AOC.Size = new System.Drawing.Size(44, 22);
      this.chk_AOC.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_AOC.TabIndex = 185;
      this.chk_AOC.TabStop = false;
      this.chk_AOC.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_AOC.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_AOC.TranslateText = false;
      this.chk_AOC.OnCheckedChanged += new System.EventHandler(this.chk_AOC_OnCheckedChanged);
      // 
      // stb_Speed
      // 
      this.stb_Speed.Active = false;
      this.stb_Speed.Alarmed = false;
      this.stb_Speed.Auto = false;
      this.stb_Speed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Speed.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Speed.FillBackColorWithImage = false;
      this.stb_Speed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Speed.FramePresent = false;
      this.stb_Speed.HighLightEnable = false;
      this.stb_Speed.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Speed.ImagePassword = null;
      this.stb_Speed.Inemergency = false;
      this.stb_Speed.InLocal = false;
      this.stb_Speed.IsButton = true;
      this.stb_Speed.IsForceToInvisible = false;
      this.stb_Speed.Location = new System.Drawing.Point(268, 643);
      this.stb_Speed.Name = "stb_Speed";
      this.stb_Speed.OnLine = false;
      this.stb_Speed.ShowImage = true;
      this.stb_Speed.ShowText = false;
      this.stb_Speed.Size = new System.Drawing.Size(72, 69);
      this.stb_Speed.Switching = false;
      this.stb_Speed.TabIndex = 190;
      this.stb_Speed.TabStop = false;
      this.stb_Speed.TranslateText = false;
      this.stb_Speed.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Speed.UserDefinedActiveImage")));
      this.stb_Speed.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Speed.UserDefinedImage")));
      this.stb_Speed.Warned = false;
      this.stb_Speed.Click += new System.EventHandler(this.stb_Speed_Click);
      // 
      // ttl_Speed
      // 
      this.ttl_Speed.BackColor = System.Drawing.Color.Black;
      this.ttl_Speed.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Speed.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Speed.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Speed.Location = new System.Drawing.Point(260, 620);
      this.ttl_Speed.Name = "ttl_Speed";
      this.ttl_Speed.Size = new System.Drawing.Size(144, 92);
      this.ttl_Speed.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Speed.TabIndex = 189;
      this.ttl_Speed.Text = "SPEED";
      this.ttl_Speed.TranslateText = false;
      // 
      // chk_Speed
      // 
      this.chk_Speed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Speed.Checked = true;
      this.chk_Speed.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Speed.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Speed.ImagePassword = null;
      this.chk_Speed.Location = new System.Drawing.Point(346, 667);
      this.chk_Speed.Name = "chk_Speed";
      this.chk_Speed.ReadOnly = true;
      this.chk_Speed.Size = new System.Drawing.Size(44, 22);
      this.chk_Speed.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Speed.TabIndex = 188;
      this.chk_Speed.TabStop = false;
      this.chk_Speed.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Speed.TranslateText = false;
      this.chk_Speed.OnCheckedChanged += new System.EventHandler(this.chk_Speed_OnCheckedChanged);
      // 
      // stb_SpeedReference
      // 
      this.stb_SpeedReference.Active = false;
      this.stb_SpeedReference.Alarmed = false;
      this.stb_SpeedReference.Auto = false;
      this.stb_SpeedReference.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_SpeedReference.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpeedReference.FillBackColorWithImage = false;
      this.stb_SpeedReference.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_SpeedReference.FramePresent = false;
      this.stb_SpeedReference.HighLightEnable = false;
      this.stb_SpeedReference.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_SpeedReference.ImagePassword = null;
      this.stb_SpeedReference.Inemergency = false;
      this.stb_SpeedReference.InLocal = false;
      this.stb_SpeedReference.IsButton = true;
      this.stb_SpeedReference.IsForceToInvisible = false;
      this.stb_SpeedReference.Location = new System.Drawing.Point(420, 643);
      this.stb_SpeedReference.Name = "stb_SpeedReference";
      this.stb_SpeedReference.OnLine = false;
      this.stb_SpeedReference.ShowImage = true;
      this.stb_SpeedReference.ShowText = false;
      this.stb_SpeedReference.Size = new System.Drawing.Size(72, 69);
      this.stb_SpeedReference.Switching = false;
      this.stb_SpeedReference.TabIndex = 193;
      this.stb_SpeedReference.TabStop = false;
      this.stb_SpeedReference.TranslateText = false;
      this.stb_SpeedReference.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpeedReference.UserDefinedActiveImage")));
      this.stb_SpeedReference.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpeedReference.UserDefinedImage")));
      this.stb_SpeedReference.Warned = false;
      this.stb_SpeedReference.Click += new System.EventHandler(this.stb_SpeedReference_Click);
      // 
      // ttl_SpeedRef
      // 
      this.ttl_SpeedRef.BackColor = System.Drawing.Color.Black;
      this.ttl_SpeedRef.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SpeedRef.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SpeedRef.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SpeedRef.Location = new System.Drawing.Point(412, 620);
      this.ttl_SpeedRef.Name = "ttl_SpeedRef";
      this.ttl_SpeedRef.Size = new System.Drawing.Size(144, 92);
      this.ttl_SpeedRef.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SpeedRef.TabIndex = 192;
      this.ttl_SpeedRef.Text = "SPEED REF";
      this.ttl_SpeedRef.TranslateText = false;
      // 
      // chk_SpeedReference
      // 
      this.chk_SpeedReference.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SpeedReference.Checked = true;
      this.chk_SpeedReference.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_SpeedReference.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SpeedReference.ImagePassword = null;
      this.chk_SpeedReference.Location = new System.Drawing.Point(498, 669);
      this.chk_SpeedReference.Name = "chk_SpeedReference";
      this.chk_SpeedReference.ReadOnly = true;
      this.chk_SpeedReference.Size = new System.Drawing.Size(44, 22);
      this.chk_SpeedReference.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_SpeedReference.TabIndex = 191;
      this.chk_SpeedReference.TabStop = false;
      this.chk_SpeedReference.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SpeedReference.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_SpeedReference.TranslateText = false;
      this.chk_SpeedReference.OnCheckedChanged += new System.EventHandler(this.chk_SpeedReference_OnCheckedChanged);
      // 
      // stb_Syncro
      // 
      this.stb_Syncro.Active = false;
      this.stb_Syncro.Alarmed = false;
      this.stb_Syncro.Auto = false;
      this.stb_Syncro.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Syncro.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Syncro.FillBackColorWithImage = false;
      this.stb_Syncro.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Syncro.FramePresent = false;
      this.stb_Syncro.HighLightEnable = false;
      this.stb_Syncro.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Syncro.ImagePassword = null;
      this.stb_Syncro.Inemergency = false;
      this.stb_Syncro.InLocal = false;
      this.stb_Syncro.IsButton = true;
      this.stb_Syncro.IsForceToInvisible = false;
      this.stb_Syncro.Location = new System.Drawing.Point(572, 643);
      this.stb_Syncro.Name = "stb_Syncro";
      this.stb_Syncro.OnLine = false;
      this.stb_Syncro.ShowImage = true;
      this.stb_Syncro.ShowText = false;
      this.stb_Syncro.Size = new System.Drawing.Size(72, 69);
      this.stb_Syncro.Switching = false;
      this.stb_Syncro.TabIndex = 196;
      this.stb_Syncro.TabStop = false;
      this.stb_Syncro.TranslateText = false;
      this.stb_Syncro.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Syncro.UserDefinedActiveImage")));
      this.stb_Syncro.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Syncro.UserDefinedImage")));
      this.stb_Syncro.Warned = false;
      this.stb_Syncro.Click += new System.EventHandler(this.stb_Syncro_Click);
      // 
      // ttl_Syncro
      // 
      this.ttl_Syncro.BackColor = System.Drawing.Color.Black;
      this.ttl_Syncro.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Syncro.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Syncro.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Syncro.Location = new System.Drawing.Point(564, 620);
      this.ttl_Syncro.Name = "ttl_Syncro";
      this.ttl_Syncro.Size = new System.Drawing.Size(144, 92);
      this.ttl_Syncro.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Syncro.TabIndex = 195;
      this.ttl_Syncro.Text = "SYNCRO";
      this.ttl_Syncro.TranslateText = false;
      // 
      // chk_Syncro
      // 
      this.chk_Syncro.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Syncro.Checked = true;
      this.chk_Syncro.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Syncro.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Syncro.ImagePassword = null;
      this.chk_Syncro.Location = new System.Drawing.Point(650, 669);
      this.chk_Syncro.Name = "chk_Syncro";
      this.chk_Syncro.ReadOnly = true;
      this.chk_Syncro.Size = new System.Drawing.Size(44, 22);
      this.chk_Syncro.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Syncro.TabIndex = 194;
      this.chk_Syncro.TabStop = false;
      this.chk_Syncro.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Syncro.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Syncro.TranslateText = false;
      this.chk_Syncro.OnCheckedChanged += new System.EventHandler(this.chk_Syncro_OnCheckedChanged);
      // 
      // stb_Printers
      // 
      this.stb_Printers.Active = false;
      this.stb_Printers.Alarmed = false;
      this.stb_Printers.Auto = false;
      this.stb_Printers.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Printers.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Printers.FillBackColorWithImage = false;
      this.stb_Printers.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Printers.FramePresent = false;
      this.stb_Printers.HighLightEnable = false;
      this.stb_Printers.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Printers.ImagePassword = null;
      this.stb_Printers.Inemergency = false;
      this.stb_Printers.InLocal = false;
      this.stb_Printers.IsButton = true;
      this.stb_Printers.IsForceToInvisible = false;
      this.stb_Printers.Location = new System.Drawing.Point(724, 643);
      this.stb_Printers.Name = "stb_Printers";
      this.stb_Printers.OnLine = false;
      this.stb_Printers.ShowImage = true;
      this.stb_Printers.ShowText = false;
      this.stb_Printers.Size = new System.Drawing.Size(72, 69);
      this.stb_Printers.Switching = false;
      this.stb_Printers.TabIndex = 199;
      this.stb_Printers.TabStop = false;
      this.stb_Printers.TranslateText = false;
      this.stb_Printers.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Printers.UserDefinedActiveImage")));
      this.stb_Printers.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Printers.UserDefinedImage")));
      this.stb_Printers.Warned = false;
      this.stb_Printers.Click += new System.EventHandler(this.stb_Printers_Click);
      // 
      // ttl_Printers
      // 
      this.ttl_Printers.BackColor = System.Drawing.Color.Black;
      this.ttl_Printers.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Printers.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Printers.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Printers.Location = new System.Drawing.Point(716, 620);
      this.ttl_Printers.Name = "ttl_Printers";
      this.ttl_Printers.Size = new System.Drawing.Size(144, 92);
      this.ttl_Printers.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Printers.TabIndex = 198;
      this.ttl_Printers.Text = "PRINTERS";
      this.ttl_Printers.TranslateText = false;
      // 
      // chk_Printers
      // 
      this.chk_Printers.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Printers.Checked = true;
      this.chk_Printers.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Printers.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Printers.ImagePassword = null;
      this.chk_Printers.Location = new System.Drawing.Point(802, 669);
      this.chk_Printers.Name = "chk_Printers";
      this.chk_Printers.ReadOnly = true;
      this.chk_Printers.Size = new System.Drawing.Size(44, 22);
      this.chk_Printers.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Printers.TabIndex = 197;
      this.chk_Printers.TabStop = false;
      this.chk_Printers.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Printers.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Printers.TranslateText = false;
      this.chk_Printers.OnCheckedChanged += new System.EventHandler(this.chk_Printers_OnCheckedChanged);
      // 
      // ttl_Office
      // 
      this.ttl_Office.BackColor = System.Drawing.Color.Black;
      this.ttl_Office.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Office.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Office.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Office.Location = new System.Drawing.Point(867, 620);
      this.ttl_Office.Name = "ttl_Office";
      this.ttl_Office.Size = new System.Drawing.Size(144, 92);
      this.ttl_Office.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Office.TabIndex = 200;
      this.ttl_Office.Text = "OFFICE";
      this.ttl_Office.TranslateText = false;
      // 
      // stb_Office
      // 
      this.stb_Office.Active = false;
      this.stb_Office.Alarmed = false;
      this.stb_Office.Auto = false;
      this.stb_Office.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_Office.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_Office.FillBackColorWithImage = false;
      this.stb_Office.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_Office.FramePresent = false;
      this.stb_Office.HighLightEnable = false;
      this.stb_Office.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_Office.ImagePassword = null;
      this.stb_Office.Inemergency = false;
      this.stb_Office.InLocal = false;
      this.stb_Office.IsButton = true;
      this.stb_Office.IsForceToInvisible = false;
      this.stb_Office.Location = new System.Drawing.Point(878, 643);
      this.stb_Office.Name = "stb_Office";
      this.stb_Office.OnLine = false;
      this.stb_Office.ShowImage = true;
      this.stb_Office.ShowText = false;
      this.stb_Office.Size = new System.Drawing.Size(72, 69);
      this.stb_Office.Switching = false;
      this.stb_Office.TabIndex = 202;
      this.stb_Office.TabStop = false;
      this.stb_Office.TranslateText = false;
      this.stb_Office.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_Office.UserDefinedActiveImage")));
      this.stb_Office.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_Office.UserDefinedImage")));
      this.stb_Office.Warned = false;
      this.stb_Office.Click += new System.EventHandler(this.stb_Office_Click);
      // 
      // chk_Office
      // 
      this.chk_Office.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Office.Checked = true;
      this.chk_Office.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Office.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Office.ImagePassword = null;
      this.chk_Office.Location = new System.Drawing.Point(956, 669);
      this.chk_Office.Name = "chk_Office";
      this.chk_Office.ReadOnly = true;
      this.chk_Office.Size = new System.Drawing.Size(44, 22);
      this.chk_Office.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_Office.TabIndex = 201;
      this.chk_Office.TabStop = false;
      this.chk_Office.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Office.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Office.TranslateText = false;
      // 
      // stb_PRC
      // 
      this.stb_PRC.Active = false;
      this.stb_PRC.Alarmed = false;
      this.stb_PRC.Auto = false;
      this.stb_PRC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stb_PRC.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_PRC.FillBackColorWithImage = false;
      this.stb_PRC.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_PRC.FramePresent = false;
      this.stb_PRC.HighLightEnable = false;
      this.stb_PRC.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.stb_PRC.ImagePassword = null;
      this.stb_PRC.Inemergency = false;
      this.stb_PRC.InLocal = false;
      this.stb_PRC.IsButton = true;
      this.stb_PRC.IsForceToInvisible = false;
      this.stb_PRC.Location = new System.Drawing.Point(1028, 643);
      this.stb_PRC.Name = "stb_PRC";
      this.stb_PRC.OnLine = false;
      this.stb_PRC.ShowImage = true;
      this.stb_PRC.ShowText = false;
      this.stb_PRC.Size = new System.Drawing.Size(72, 69);
      this.stb_PRC.Switching = false;
      this.stb_PRC.TabIndex = 205;
      this.stb_PRC.TabStop = false;
      this.stb_PRC.TranslateText = false;
      this.stb_PRC.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_PRC.UserDefinedActiveImage")));
      this.stb_PRC.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_PRC.UserDefinedImage")));
      this.stb_PRC.Warned = false;
      this.stb_PRC.Click += new System.EventHandler(this.stb_PRC_Click);
      // 
      // chk_PRC
      // 
      this.chk_PRC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PRC.Checked = true;
      this.chk_PRC.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_PRC.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_PRC.ImagePassword = null;
      this.chk_PRC.Location = new System.Drawing.Point(1106, 669);
      this.chk_PRC.Name = "chk_PRC";
      this.chk_PRC.ReadOnly = true;
      this.chk_PRC.Size = new System.Drawing.Size(44, 22);
      this.chk_PRC.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_PRC.TabIndex = 204;
      this.chk_PRC.TabStop = false;
      this.chk_PRC.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PRC.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_PRC.TranslateText = false;
      this.chk_PRC.OnCheckedChanged += new System.EventHandler(this.chk_PRC_OnCheckedChanged);
      // 
      // ttl_PRC
      // 
      this.ttl_PRC.BackColor = System.Drawing.Color.Black;
      this.ttl_PRC.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PRC.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PRC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PRC.Location = new System.Drawing.Point(1017, 620);
      this.ttl_PRC.Name = "ttl_PRC";
      this.ttl_PRC.Size = new System.Drawing.Size(144, 92);
      this.ttl_PRC.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PRC.TabIndex = 203;
      this.ttl_PRC.Text = "PRC";
      this.ttl_PRC.TranslateText = false;
      // 
      // Frm_LineConfig
      // 
      this.ClientSize = new System.Drawing.Size(1280, 747);
      this.Controls.Add(this.stb_PRC);
      this.Controls.Add(this.chk_PRC);
      this.Controls.Add(this.ttl_PRC);
      this.Controls.Add(this.stb_Office);
      this.Controls.Add(this.chk_Office);
      this.Controls.Add(this.ttl_Office);
      this.Controls.Add(this.stb_Printers);
      this.Controls.Add(this.ttl_Printers);
      this.Controls.Add(this.chk_Printers);
      this.Controls.Add(this.stb_Syncro);
      this.Controls.Add(this.ttl_Syncro);
      this.Controls.Add(this.chk_Syncro);
      this.Controls.Add(this.stb_SpeedReference);
      this.Controls.Add(this.ttl_SpeedRef);
      this.Controls.Add(this.chk_SpeedReference);
      this.Controls.Add(this.stb_Speed);
      this.Controls.Add(this.ttl_Speed);
      this.Controls.Add(this.chk_Speed);
      this.Controls.Add(this.stb_AOC);
      this.Controls.Add(this.ttl_AOC);
      this.Controls.Add(this.chk_AOC);
      this.Controls.Add(this.ttl_Various);
      this.Controls.Add(this.stb_StackerHigh);
      this.Controls.Add(this.stb_StackerMid);
      this.Controls.Add(this.stb_StackerLow);
      this.Controls.Add(this.stb_Knife);
      this.Controls.Add(this.stb_WebDirector);
      this.Controls.Add(this.stb_SlitterScorer);
      this.Controls.Add(this.stb_Rotary);
      this.Controls.Add(this.stb_DoubleBacker);
      this.Controls.Add(this.stb_GlueUnit);
      this.Controls.Add(this.stb_PreHeatersTern);
      this.Controls.Add(this.stb_SingleFacer1);
      this.Controls.Add(this.stb_SingleFacer2);
      this.Controls.Add(this.stb_SingleFacer3);
      this.Controls.Add(this.stb_Splicer2);
      this.Controls.Add(this.stb_Splicer4);
      this.Controls.Add(this.stb_Splicer1);
      this.Controls.Add(this.stb_Splicer3);
      this.Controls.Add(this.stb_Splicer5);
      this.Controls.Add(this.stb_Splicer6);
      this.Controls.Add(this.stb_Splicer7);
      this.Controls.Add(this.chk_GlueUnit);
      this.Controls.Add(this.ttl_StackerHigh);
      this.Controls.Add(this.ttl_StackrMid);
      this.Controls.Add(this.ttl_StackerLow);
      this.Controls.Add(this.ttl_Knife);
      this.Controls.Add(this.ttl_WebDirector);
      this.Controls.Add(this.ttl_SlitterScorer);
      this.Controls.Add(this.ttl_Rotary);
      this.Controls.Add(this.ttl_DoubleBacker);
      this.Controls.Add(this.ttl_GlueUnit);
      this.Controls.Add(this.ttl_PreHeatersTern);
      this.Controls.Add(this.ttl_Splicer1);
      this.Controls.Add(this.ttl_Splicer2);
      this.Controls.Add(this.ttl_SingleFacer1);
      this.Controls.Add(this.ttl_SPlicer3);
      this.Controls.Add(this.ttl_Splicer4);
      this.Controls.Add(this.ttl_SingleFacer2);
      this.Controls.Add(this.ttl_Splicer5);
      this.Controls.Add(this.ttl_Splicer6);
      this.Controls.Add(this.ttl_SingleFacer3);
      this.Controls.Add(this.ttl_Splicer7);
      this.Controls.Add(this.chk_StackerHigh);
      this.Controls.Add(this.chk_StackerMid);
      this.Controls.Add(this.chk_WebDirector);
      this.Controls.Add(this.chk_Splicer6);
      this.Controls.Add(this.chk_StackerLow);
      this.Controls.Add(this.chk_Knife);
      this.Controls.Add(this.chk_SlitterScorer);
      this.Controls.Add(this.chk_PreHeatersTern);
      this.Controls.Add(this.chk_DoubleBacker);
      this.Controls.Add(this.chk_SingleFacer3);
      this.Controls.Add(this.chk_Splicer2);
      this.Controls.Add(this.chk_Splicer5);
      this.Controls.Add(this.chk_SingleFacer1);
      this.Controls.Add(this.chk_Splicer3);
      this.Controls.Add(this.chk_Splicer4);
      this.Controls.Add(this.chk_SingleFacer2);
      this.Controls.Add(this.chk_Rotary);
      this.Controls.Add(this.chk_Splicer1);
      this.Controls.Add(this.chk_Splicer7);
      this.Controls.Add(this.ttl_DryEnd);
      this.Controls.Add(this.ttl_wetEnd);
      this.Name = "Frm_LineConfig";
      this.Tag = "";
      this.Text = "Frm_LineConfig";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.ttl_wetEnd, 0);
      this.Controls.SetChildIndex(this.ttl_DryEnd, 0);
      this.Controls.SetChildIndex(this.chk_Splicer7, 0);
      this.Controls.SetChildIndex(this.chk_Splicer1, 0);
      this.Controls.SetChildIndex(this.chk_Rotary, 0);
      this.Controls.SetChildIndex(this.chk_SingleFacer2, 0);
      this.Controls.SetChildIndex(this.chk_Splicer4, 0);
      this.Controls.SetChildIndex(this.chk_Splicer3, 0);
      this.Controls.SetChildIndex(this.chk_SingleFacer1, 0);
      this.Controls.SetChildIndex(this.chk_Splicer5, 0);
      this.Controls.SetChildIndex(this.chk_Splicer2, 0);
      this.Controls.SetChildIndex(this.chk_SingleFacer3, 0);
      this.Controls.SetChildIndex(this.chk_DoubleBacker, 0);
      this.Controls.SetChildIndex(this.chk_PreHeatersTern, 0);
      this.Controls.SetChildIndex(this.chk_SlitterScorer, 0);
      this.Controls.SetChildIndex(this.chk_Knife, 0);
      this.Controls.SetChildIndex(this.chk_StackerLow, 0);
      this.Controls.SetChildIndex(this.chk_Splicer6, 0);
      this.Controls.SetChildIndex(this.chk_WebDirector, 0);
      this.Controls.SetChildIndex(this.chk_StackerMid, 0);
      this.Controls.SetChildIndex(this.chk_StackerHigh, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer7, 0);
      this.Controls.SetChildIndex(this.ttl_SingleFacer3, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer6, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer5, 0);
      this.Controls.SetChildIndex(this.ttl_SingleFacer2, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer4, 0);
      this.Controls.SetChildIndex(this.ttl_SPlicer3, 0);
      this.Controls.SetChildIndex(this.ttl_SingleFacer1, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer2, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer1, 0);
      this.Controls.SetChildIndex(this.ttl_PreHeatersTern, 0);
      this.Controls.SetChildIndex(this.ttl_GlueUnit, 0);
      this.Controls.SetChildIndex(this.ttl_DoubleBacker, 0);
      this.Controls.SetChildIndex(this.ttl_Rotary, 0);
      this.Controls.SetChildIndex(this.ttl_SlitterScorer, 0);
      this.Controls.SetChildIndex(this.ttl_WebDirector, 0);
      this.Controls.SetChildIndex(this.ttl_Knife, 0);
      this.Controls.SetChildIndex(this.ttl_StackerLow, 0);
      this.Controls.SetChildIndex(this.ttl_StackrMid, 0);
      this.Controls.SetChildIndex(this.ttl_StackerHigh, 0);
      this.Controls.SetChildIndex(this.chk_GlueUnit, 0);
      this.Controls.SetChildIndex(this.stb_Splicer7, 0);
      this.Controls.SetChildIndex(this.stb_Splicer6, 0);
      this.Controls.SetChildIndex(this.stb_Splicer5, 0);
      this.Controls.SetChildIndex(this.stb_Splicer3, 0);
      this.Controls.SetChildIndex(this.stb_Splicer1, 0);
      this.Controls.SetChildIndex(this.stb_Splicer4, 0);
      this.Controls.SetChildIndex(this.stb_Splicer2, 0);
      this.Controls.SetChildIndex(this.stb_SingleFacer3, 0);
      this.Controls.SetChildIndex(this.stb_SingleFacer2, 0);
      this.Controls.SetChildIndex(this.stb_SingleFacer1, 0);
      this.Controls.SetChildIndex(this.stb_PreHeatersTern, 0);
      this.Controls.SetChildIndex(this.stb_GlueUnit, 0);
      this.Controls.SetChildIndex(this.stb_DoubleBacker, 0);
      this.Controls.SetChildIndex(this.stb_Rotary, 0);
      this.Controls.SetChildIndex(this.stb_SlitterScorer, 0);
      this.Controls.SetChildIndex(this.stb_WebDirector, 0);
      this.Controls.SetChildIndex(this.stb_Knife, 0);
      this.Controls.SetChildIndex(this.stb_StackerLow, 0);
      this.Controls.SetChildIndex(this.stb_StackerMid, 0);
      this.Controls.SetChildIndex(this.stb_StackerHigh, 0);
      this.Controls.SetChildIndex(this.ttl_Various, 0);
      this.Controls.SetChildIndex(this.chk_AOC, 0);
      this.Controls.SetChildIndex(this.ttl_AOC, 0);
      this.Controls.SetChildIndex(this.stb_AOC, 0);
      this.Controls.SetChildIndex(this.chk_Speed, 0);
      this.Controls.SetChildIndex(this.ttl_Speed, 0);
      this.Controls.SetChildIndex(this.stb_Speed, 0);
      this.Controls.SetChildIndex(this.chk_SpeedReference, 0);
      this.Controls.SetChildIndex(this.ttl_SpeedRef, 0);
      this.Controls.SetChildIndex(this.stb_SpeedReference, 0);
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
      this.Controls.SetChildIndex(this.chk_Syncro, 0);
      this.Controls.SetChildIndex(this.ttl_Syncro, 0);
      this.Controls.SetChildIndex(this.stb_Syncro, 0);
      this.Controls.SetChildIndex(this.chk_Printers, 0);
      this.Controls.SetChildIndex(this.ttl_Printers, 0);
      this.Controls.SetChildIndex(this.stb_Printers, 0);
      this.Controls.SetChildIndex(this.ttl_Office, 0);
      this.Controls.SetChildIndex(this.chk_Office, 0);
      this.Controls.SetChildIndex(this.stb_Office, 0);
      this.Controls.SetChildIndex(this.ttl_PRC, 0);
      this.Controls.SetChildIndex(this.chk_PRC, 0);
      this.Controls.SetChildIndex(this.stb_PRC, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
		}

		protected override void F2()
		{
		}

		public override void refreshData()
		{
			LineConfig lineConfig = DBManager.getDBManager().lineConfig;
			
			chk_Splicer7.Checked = lineConfig.Splicer7 ;					//0
			chk_SingleFacer3.Checked = lineConfig.SingleFacer3 ;	//1
			chk_Splicer6.Checked = lineConfig.Splicer6 ;					//2
			chk_Splicer5.Checked = lineConfig.Splicer5 ;					//3
			chk_SingleFacer2.Checked = lineConfig.SingleFacer2 ;	//4
			chk_Splicer4.Checked = lineConfig.Splicer4 ;					//5
			chk_Splicer3.Checked = lineConfig.Splicer3 ;					//6
			chk_SingleFacer1.Checked = lineConfig.SingleFacer1 ;	//7
			chk_Splicer2.Checked = lineConfig.Splicer2 ;					//8
			chk_Splicer1.Checked = lineConfig.Splicer1 ;					//9
			chk_PreHeatersTern.Checked = lineConfig.PreHeatersTern ;//10
			chk_GlueUnit.Checked = lineConfig.GlueUnit ;					//11
			chk_DoubleBacker.Checked = lineConfig.DoubleBacker ;	//12
			chk_Rotary.Checked = lineConfig.Rotary ;							//13
			chk_SlitterScorer.Checked = lineConfig.SlitterScorer ;//14
			chk_WebDirector.Checked = lineConfig.WebDirector ;		//15
			chk_Knife.Checked = lineConfig.Knife ;								//16
			chk_StackerLow.Checked = lineConfig.Stacker1 ;			//17
			chk_StackerMid.Checked = lineConfig.Stacker2 ;			//18
			chk_StackerHigh.Checked = lineConfig.Stacker3 ;		//19
			chk_AOC.Checked = lineConfig.Aoc ;                //20
      chk_PRC.Checked = lineConfig.PRC;                 //21
      chk_Speed.Checked = true;
			chk_SpeedReference.Checked  = true;
		}

		protected override void CtrlF4()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				if (state == State.Modify)
				{
					CommunicationManager cm = CommunicationManager.GetCommunicationManager();
					cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
						Command.CONF_LINE + "." + Command.CONF_CONFIG + "(" +
						this.GetName(chk_Splicer7) + "=" + chk_Splicer7.Checked.ToString() + Command.PARAMETER_SEPARATOR + //0
						this.GetName(chk_SingleFacer3) + "=" + chk_SingleFacer3.Checked.ToString() + Command.PARAMETER_SEPARATOR +	//1
						this.GetName(chk_Splicer6) + "=" + chk_Splicer6.Checked.ToString() + Command.PARAMETER_SEPARATOR + //2
						this.GetName(chk_Splicer5) + "=" + chk_Splicer5.Checked.ToString() + Command.PARAMETER_SEPARATOR + //3
						this.GetName(chk_SingleFacer2) + "=" + chk_SingleFacer2.Checked.ToString() + Command.PARAMETER_SEPARATOR +	//4
						this.GetName(chk_Splicer4) + "=" + chk_Splicer4.Checked.ToString() + Command.PARAMETER_SEPARATOR + //5
						this.GetName(chk_Splicer3) + "=" + chk_Splicer3.Checked.ToString() + Command.PARAMETER_SEPARATOR + //6
						this.GetName(chk_SingleFacer1) + "=" + chk_SingleFacer1.Checked.ToString() + Command.PARAMETER_SEPARATOR + //7
						this.GetName(chk_Splicer2) + "=" + chk_Splicer2.Checked.ToString() + Command.PARAMETER_SEPARATOR + //8
						this.GetName(chk_Splicer1) + "=" + chk_Splicer1.Checked.ToString() + Command.PARAMETER_SEPARATOR + //9
						this.GetName(chk_PreHeatersTern) + "=" + chk_PreHeatersTern.Checked.ToString() + Command.PARAMETER_SEPARATOR + //10
						this.GetName(chk_GlueUnit) + "=" + chk_GlueUnit.Checked.ToString() + Command.PARAMETER_SEPARATOR + //11
						this.GetName(chk_DoubleBacker) + "=" + chk_DoubleBacker.Checked.ToString() + Command.PARAMETER_SEPARATOR + //12
						this.GetName(chk_Rotary) + "=" + chk_Rotary.Checked.ToString() + Command.PARAMETER_SEPARATOR + //13
						this.GetName(chk_SlitterScorer) + "=" + chk_SlitterScorer.Checked.ToString() + Command.PARAMETER_SEPARATOR + //14
						this.GetName(chk_WebDirector) + "=" + chk_WebDirector.Checked.ToString() + Command.PARAMETER_SEPARATOR + //15
						this.GetName(chk_Knife) + "=" + chk_Knife.Checked.ToString() + Command.PARAMETER_SEPARATOR + //16
						this.GetName(chk_StackerLow) + "=" + chk_StackerLow.Checked.ToString() + Command.PARAMETER_SEPARATOR +	//17
						this.GetName(chk_StackerMid) + "=" + chk_StackerMid.Checked.ToString() + Command.PARAMETER_SEPARATOR +	//18
            this.GetName(chk_StackerHigh) + "=" + chk_StackerHigh.Checked.ToString() + Command.PARAMETER_SEPARATOR +	//19
            this.GetName(chk_AOC) + "=" + chk_AOC.Checked.ToString() + Command.PARAMETER_SEPARATOR +	//20
            this.GetName(chk_PRC) + "=" + chk_PRC.Checked.ToString() +																//21
						")");
					state = State.Read;
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
					readOnly(true);
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				if(state == State.Modify)
					return;
	                
				state = State.Modify;
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
				readOnly(false);
				chk_Splicer7.Focus();
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		/// <summary>
		/// Escape key behaviour: Abort the operation
		/// </summary>
		protected override void Escape()
		{
			if(state == State.Read)
				return;

			refreshData();
			state = State.Read;
			readOnly(true);
			lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
		}

		private void readOnly(bool isReadOnly)
		{
			//readonly
			chk_Splicer7.ReadOnly		= chk_SingleFacer3.ReadOnly		= chk_Splicer6.ReadOnly	= 
			chk_Splicer5.ReadOnly		= chk_SingleFacer2.ReadOnly		= chk_Splicer4.ReadOnly	= 
			chk_Splicer3.ReadOnly		= chk_SingleFacer1.ReadOnly		= chk_Splicer2.ReadOnly	= 
			chk_Splicer1.ReadOnly		= chk_PreHeatersTern.ReadOnly = chk_GlueUnit.ReadOnly	= 
			chk_DoubleBacker.ReadOnly	= 
			chk_Rotary.ReadOnly			= chk_SlitterScorer.ReadOnly	= chk_WebDirector.ReadOnly =
			chk_Knife.ReadOnly			= chk_StackerLow.ReadOnly			= chk_StackerMid.ReadOnly =
			chk_StackerHigh.ReadOnly = chk_AOC.ReadOnly						= chk_PRC.ReadOnly =
				isReadOnly;

			//tabStop
			chk_Splicer7.TabStop		= chk_SingleFacer3.TabStop		= chk_Splicer6.TabStop		=
			chk_Splicer5.TabStop		= chk_SingleFacer2.TabStop		= chk_Splicer4.TabStop		=
			chk_Splicer3.TabStop		= chk_SingleFacer1.TabStop		= chk_Splicer2.TabStop		=
			chk_Splicer1.TabStop		= chk_PreHeatersTern.TabStop  = chk_GlueUnit.TabStop	  = 
			chk_DoubleBacker.TabStop	=
			chk_Rotary.TabStop			= chk_SlitterScorer.TabStop		= chk_WebDirector.TabStop  =
			chk_Knife.TabStop				= chk_StackerLow.TabStop			= chk_StackerMid.TabStop		=
      chk_StackerHigh.TabStop = chk_AOC.TabStop             = chk_PRC.TabStop =
				! isReadOnly;
		}

		string GetName(Control c)
		{
			return c.Name.Substring(4);
		}

		#region CheckBox and PictureBox event handlers

		/* zone 3 */
		//splicer 7
		private void stb_Splicer7_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 7);
		}
		private void chk_Splicer7_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_Splicer7.Active = chk_Splicer7.Checked;
		}
		//single facer 3
		private void stb_SingleFacer3_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.SingleFacer.Settings.Frm_SingleFacerSettingsMachine), 3);
		}
		private void chk_SingleFacer3_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_SingleFacer3.Active = chk_SingleFacer3.Checked;
		}
		//splicer 6
		private void stb_Splicer6_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 6);
		}
		private void chk_Splicer6_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_Splicer6.Active = chk_Splicer6.Checked;
		}

		/* zone 2 */
		//splicer 5
		private void stb_Splicer5_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 5);
		}
		private void chk_Splicer5_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_Splicer5.Active = chk_Splicer5.Checked;
		}
		//single facer 2
		private void stb_SingleFacer2_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.SingleFacer.Settings.Frm_SingleFacerSettingsMachine), 2);
		}
		private void chk_SingleFacer2_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_SingleFacer2.Active = chk_SingleFacer2.Checked;
		}
		//splicer 4 
		private void stb_Splicer4_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 4);
		}
		private void chk_Splicer4_OnCheckedChange(object sender, System.EventArgs e)
		{
            stb_Splicer4.Active = chk_Splicer4.Checked;
		}

		/* zone 1 */
		//splicer 3
		private void stb_Splicer3_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 3);
		}
		private void chk_Splicer3_OnCheckedChange(object sender, System.EventArgs e)
		{
            stb_Splicer3.Active = chk_Splicer3.Checked;
		}
		//single facer 1
		private void stb_SingleFacer1_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.SingleFacer.Settings.Frm_SingleFacerSettingsMachine), 1);
		}
		private void chk_SingleFacer1_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_SingleFacer1.Active = chk_SingleFacer1.Checked;
		}
		//splicer 2
		private void stb_Splicer2_Click(object sender, System.EventArgs e)
		{
            askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 2);
		}
		private void chk_Splicer2_OnCheckedChange(object sender, System.EventArgs e)
		{
			stb_Splicer2.Active = chk_Splicer2.Checked;
		}

		/* zone 0 */
		//splicer1
		private void stb_Splicer1_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.Splicer.Settings.Frm_SplicerSettingsMachine), 1);
		}
		private void chk_Splicer1_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_Splicer1.Active = chk_Splicer1.Checked;
		}
		//Pre Heaters Tern
		private void stb_PreHeatersTern_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.PreHeaterTern.Settings.Frm_PreHeaterTernSettingsMachine));
		}
		private void chk_PreHeatersTern_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_PreHeatersTern.Active = chk_PreHeatersTern.Checked;
		}
		//glue unit
		private void stb_GlueUnit_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.GlueUnit.Settings.Frm_GlueUnitSettingsMachine));
		}
		private void chk_GlueUnit_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_GlueUnit.Active = chk_GlueUnit.Checked;
		}
		/* DoubleBacker */
		private void stb_DoubleBacker_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.DoubleBacker.Settings.Frm_DoubleBackerSettingsMachine));
		}
		private void chk_DoubleBacker_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_DoubleBacker.Active = chk_DoubleBacker.Checked;
		}
		/* Dry end zone */
		//Rotary
		private void stb_Rotary_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.Shear.Settings.Frm_ShearSettingsMachine));
		}
		private void chk_Rotary_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_Rotary.Active = chk_Rotary.Checked;
		}
		//Slitter scorer
		private void stb_SlitterScorer_Click(object sender, System.EventArgs e)
		{
			if (SyncroC.Utility.Settings.getSettings().configuration.IsSyncro())
				askForPage(typeof(SyncroC.Forms.Pages.SlitterScorer.Settings.Frm_SlitterScorerSettingsMachine));
			else if (SyncroC.Utility.Settings.getSettings().configuration.IsSyncro400())
				askForPage(typeof(SyncroC.Forms.Pages.Twin400.Settings.Frm_twin400_machineSettings));
		}
		private void chk_SlitterScorer_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_SlitterScorer.Active = chk_SlitterScorer.Checked;
		}
		//web director
		private void stb_WebDirector_Click(object sender, System.EventArgs e)
		{
      if (SyncroC.Utility.Settings.getSettings().configuration.IsSyncro())
        askForPage(typeof(SyncroC.Forms.Pages.SlitterScorer.Settings.Frm_SlitterScorerSettingsMachine));
      else if (SyncroC.Utility.Settings.getSettings().configuration.IsSyncro400())
        askForPage(typeof(SyncroC.Forms.Pages.Twin400.Settings.Frm_twin400_machineSettings));
    }
		private void chk_WebDirector_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_WebDirector.Active = chk_WebDirector.Checked;
		}
		//knife
		private void stb_Knife_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.Knife.Settings.Frm_KnifeSettingsMachine));
		}
		private void chk_Knife_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_Knife.Active = chk_Knife.Checked;
		}
		//stacker Low
		private void stb_StackerLow_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.Stacker.Settings.Frm_StackerSettingsMachine), (int)LevelName.Lower);
		}
		private void chk_StackerLow_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_StackerLow.Active = chk_StackerLow.Checked;
		}
		//stacker Mid
		private void stb_StackerMid_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.Stacker.Settings.Frm_StackerSettingsMachine), (int)LevelName.Middle);
		}
		private void chk_StackerMid_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_StackerMid.Active = chk_StackerMid.Checked;
		}
		//stacker High
		private void stb_StackerHigh_Click(object sender, System.EventArgs e)
		{
			askForPage(typeof(SyncroC.Forms.Pages.Stacker.Settings.Frm_StackerSettingsMachine), (int)LevelName.Upper);
		}
		private void chk_StackerHigh_OnCheckedChange(object sender, System.EventArgs e)
		{
				stb_StackerHigh.Active = chk_StackerHigh.Checked;
		}

		//AOC
		private void stb_AOC_Click(object sender, System.EventArgs e)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_AOC_WEC);

		}
		private void chk_AOC_OnCheckedChanged(object sender, EventArgs e)
		{
			stb_AOC.Active = chk_AOC.Checked;
		}

    //Speed
		private void stb_Speed_Click(object sender, EventArgs e)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Speed);
		}
		private void chk_Speed_OnCheckedChanged(object sender, EventArgs e)
		{
			stb_Speed.Active = chk_Speed.Checked;
		}

    //SpeedReference
		private void stb_SpeedReference_Click(object sender, EventArgs e)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_SpeedReference);
		}
		private void chk_SpeedReference_OnCheckedChanged(object sender, EventArgs e)
		{
			stb_SpeedReference.Active = chk_SpeedReference.Checked;
		}

    //Syncro
    private void stb_Syncro_Click(object sender, EventArgs e)
    {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Syncro);
    }
    private void chk_Syncro_OnCheckedChanged(object sender, EventArgs e)
    {
        stb_Syncro.Active = chk_Syncro.Checked;
    }

    //Printers
    private void stb_Printers_Click(object sender, EventArgs e)
    {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Printers);
    }
    private void chk_Printers_OnCheckedChanged(object sender, EventArgs e)
    {
        stb_Printers.Active = chk_Printers.Checked;
    }

		//Office
		private void stb_Office_Click(object sender, EventArgs e)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Office);
		}
		private void chk_Office_OnCheckedChanged(object sender, EventArgs e)
		{
			stb_Office.Active = chk_Office.Checked;
		}

    //PRC
    private void stb_PRC_Click(object sender, EventArgs e)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_PRC);
    }
    private void chk_PRC_OnCheckedChanged(object sender, EventArgs e)
    {
      stb_PRC.Active = chk_PRC.Checked;
    }
    #endregion
  }
}

