//agrassi_FI19-08-2010_1
using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase;

namespace SyncroC.Controls
{
  public delegate void Translate(string language);
	public class LanguageAndAccessibilitySettings : SyncroC.Controls.SyncroUserControl
	{
    private System.Windows.Forms.ColumnHeader columnLanguage1;
    private System.Windows.Forms.ColumnHeader columnLanguage2;
    private TitledTextBox ttb_pwd;
		private TitledLabel tlb_Level;
    private TitledLabel tlb_Platform;
		private TitledLabel tlb_ProductVersion;
    private Title tit_Info;
    private Title tit_PageAccessibility;
    private CheckBox chk_RapidTranslation;
    private TitledLabel ttl_SecondLanguage;
    private TitledLabel ttl_FirstLanguage;
    private Title tit_SecondLanguage;
    private Grid grd_SecondLanguage;
    private Grid grd_FirstLanguage;
    private Title tit_FirstLanguage;
    private Translate translateFun;
    private Accessibility accessibility;
    private Settings settings;
    private Language language;
		private Label lbl_FosberAddress;
		private PictureBox pct_Fosber;
		private PictureBox pct_SyncroLogo;
		private PictureBox pct_Twin400Logo;
		private CheckBox chk_Console;
		private TitledLabel tlb_ProductDataVersion;
		private TitledLabel tlb_DSPUserTaskVersion;
    private TitledLabel tlb_ITEVersion;
    private System.ComponentModel.IContainer components = null;

		public LanguageAndAccessibilitySettings()
		{
			InitializeComponent();
			
			this.SetInfo();

			Accessibility.getAccessibility().OnAccessibilityChanged +=
				new SyncroC.Utility.Accessibility.AccessibilityChangedHandler(OnAccessibilityChanged);
    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">The Accessibility object.</param>
		/// <param name="args"></param>
		private void OnAccessibilityChanged(object sender, SyncroC.Utility.EventArgs.AccessibilityChangedEventArgs args)
		{
			chk_Console.ReadOnly = !args.accessibilityLevel.Equals(AccessibilityLevel.High);
		}

    public void Config(ref Accessibility accessibility, ref Settings settings, ref Language language, Translate translateFun)
    {
      this.accessibility=accessibility;
      this.settings=settings;
			LauyoutComponents();
      this.language=language;
      this.translateFun=translateFun;
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
      this.ttb_pwd = new SyncroC.Controls.TitledTextBox();
      this.tlb_Level = new SyncroC.Controls.TitledLabel();
      this.tlb_Platform = new SyncroC.Controls.TitledLabel();
      this.tlb_ProductVersion = new SyncroC.Controls.TitledLabel();
      this.tit_Info = new SyncroC.Controls.Title();
      this.tit_PageAccessibility = new SyncroC.Controls.Title();
      this.chk_RapidTranslation = new SyncroC.Controls.CheckBox();
      this.ttl_SecondLanguage = new SyncroC.Controls.TitledLabel();
      this.ttl_FirstLanguage = new SyncroC.Controls.TitledLabel();
      this.tit_SecondLanguage = new SyncroC.Controls.Title();
      this.grd_SecondLanguage = new SyncroC.Controls.Grid();
      this.columnLanguage2 = new System.Windows.Forms.ColumnHeader();
      this.grd_FirstLanguage = new SyncroC.Controls.Grid();
      this.columnLanguage1 = new System.Windows.Forms.ColumnHeader();
      this.tit_FirstLanguage = new SyncroC.Controls.Title();
      this.lbl_FosberAddress = new System.Windows.Forms.Label();
      this.chk_Console = new SyncroC.Controls.CheckBox();
      this.tlb_ProductDataVersion = new SyncroC.Controls.TitledLabel();
      this.tlb_DSPUserTaskVersion = new SyncroC.Controls.TitledLabel();
      this.tlb_ITEVersion = new SyncroC.Controls.TitledLabel();
      this.pct_Twin400Logo = new System.Windows.Forms.PictureBox();
      this.pct_Fosber = new System.Windows.Forms.PictureBox();
      this.pct_SyncroLogo = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Twin400Logo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Fosber)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SyncroLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // ttb_pwd
      // 
      this.ttb_pwd.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_pwd.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttb_pwd.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_pwd.ImagePassword = null;
      this.ttb_pwd.Location = new System.Drawing.Point(725, 73);
      this.ttb_pwd.MaxChar = 32767;
      this.ttb_pwd.Multiline = false;
      this.ttb_pwd.Name = "ttb_pwd";
      this.ttb_pwd.PasswordChar = '*';
      this.ttb_pwd.ReadOnly = false;
      this.ttb_pwd.Size = new System.Drawing.Size(446, 48);
      this.ttb_pwd.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_pwd.TabIndex = 154;
      this.ttb_pwd.Text = "Reserved password";
      this.ttb_pwd.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_pwd.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_pwd.TranslateText = true;
      this.ttb_pwd.Value = " ";
      this.ttb_pwd.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tlb_Level
      // 
      this.tlb_Level.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Level.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_Level.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Level.ImagePassword = null;
      this.tlb_Level.Location = new System.Drawing.Point(725, 23);
      this.tlb_Level.Name = "tlb_Level";
      this.tlb_Level.Size = new System.Drawing.Size(274, 48);
      this.tlb_Level.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_Level.TabIndex = 153;
      this.tlb_Level.Text = "Level";
      this.tlb_Level.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Level.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Level.TranslateText = true;
      this.tlb_Level.Value = "";
      this.tlb_Level.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Level.ValueMultiline = true;
      // 
      // tlb_Platform
      // 
      this.tlb_Platform.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Platform.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_Platform.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Platform.ImagePassword = null;
      this.tlb_Platform.Location = new System.Drawing.Point(725, 362);
      this.tlb_Platform.Name = "tlb_Platform";
      this.tlb_Platform.Size = new System.Drawing.Size(446, 48);
      this.tlb_Platform.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_Platform.TabIndex = 150;
      this.tlb_Platform.Text = "Platform";
      this.tlb_Platform.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Platform.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Platform.TranslateText = false;
      this.tlb_Platform.Value = "";
      this.tlb_Platform.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Platform.ValueMultiline = true;
      // 
      // tlb_ProductVersion
      // 
      this.tlb_ProductVersion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_ProductVersion.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_ProductVersion.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ProductVersion.ImagePassword = null;
      this.tlb_ProductVersion.Location = new System.Drawing.Point(725, 418);
      this.tlb_ProductVersion.Name = "tlb_ProductVersion";
      this.tlb_ProductVersion.Size = new System.Drawing.Size(180, 48);
      this.tlb_ProductVersion.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_ProductVersion.TabIndex = 149;
      this.tlb_ProductVersion.Text = "Product version";
      this.tlb_ProductVersion.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ProductVersion.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ProductVersion.TranslateText = false;
      this.tlb_ProductVersion.Value = "";
      this.tlb_ProductVersion.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ProductVersion.ValueMultiline = true;
      // 
      // tit_Info
      // 
      this.tit_Info.BackColor = System.Drawing.Color.Black;
      this.tit_Info.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tit_Info.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tit_Info.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tit_Info.Location = new System.Drawing.Point(717, 125);
      this.tit_Info.Name = "tit_Info";
      this.tit_Info.Size = new System.Drawing.Size(454, 394);
      this.tit_Info.Style = SyncroC.Controls.TitleStyles.Small;
      this.tit_Info.TabIndex = 147;
      this.tit_Info.Text = "INFO";
      this.tit_Info.TranslateText = true;
      // 
      // tit_PageAccessibility
      // 
      this.tit_PageAccessibility.BackColor = System.Drawing.Color.Black;
      this.tit_PageAccessibility.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tit_PageAccessibility.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tit_PageAccessibility.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tit_PageAccessibility.Location = new System.Drawing.Point(717, 0);
      this.tit_PageAccessibility.Name = "tit_PageAccessibility";
      this.tit_PageAccessibility.Size = new System.Drawing.Size(454, 121);
      this.tit_PageAccessibility.Style = SyncroC.Controls.TitleStyles.Small;
      this.tit_PageAccessibility.TabIndex = 146;
      this.tit_PageAccessibility.Text = "PAGES ACCESSIBILITY";
      this.tit_PageAccessibility.TranslateText = true;
      // 
      // chk_RapidTranslation
      // 
      this.chk_RapidTranslation.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_RapidTranslation.Checked = true;
      this.chk_RapidTranslation.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_RapidTranslation.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_RapidTranslation.ImagePassword = null;
      this.chk_RapidTranslation.Location = new System.Drawing.Point(368, 73);
      this.chk_RapidTranslation.Name = "chk_RapidTranslation";
      this.chk_RapidTranslation.ReadOnly = false;
      this.chk_RapidTranslation.Size = new System.Drawing.Size(338, 48);
      this.chk_RapidTranslation.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_RapidTranslation.TabIndex = 145;
      this.chk_RapidTranslation.Text = "Rapid translation";
      this.chk_RapidTranslation.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_RapidTranslation.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_RapidTranslation.TranslateText = true;
      // 
      // ttl_SecondLanguage
      // 
      this.ttl_SecondLanguage.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SecondLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_SecondLanguage.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SecondLanguage.ImagePassword = null;
      this.ttl_SecondLanguage.Location = new System.Drawing.Point(368, 23);
      this.ttl_SecondLanguage.Name = "ttl_SecondLanguage";
      this.ttl_SecondLanguage.Size = new System.Drawing.Size(338, 48);
      this.ttl_SecondLanguage.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_SecondLanguage.TabIndex = 144;
      this.ttl_SecondLanguage.Text = "Second language";
      this.ttl_SecondLanguage.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SecondLanguage.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SecondLanguage.TranslateText = true;
      this.ttl_SecondLanguage.Value = "";
      this.ttl_SecondLanguage.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SecondLanguage.ValueMultiline = true;
      // 
      // ttl_FirstLanguage
      // 
      this.ttl_FirstLanguage.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_FirstLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_FirstLanguage.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_FirstLanguage.ImagePassword = null;
      this.ttl_FirstLanguage.Location = new System.Drawing.Point(8, 24);
      this.ttl_FirstLanguage.Name = "ttl_FirstLanguage";
      this.ttl_FirstLanguage.Size = new System.Drawing.Size(339, 48);
      this.ttl_FirstLanguage.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_FirstLanguage.TabIndex = 143;
      this.ttl_FirstLanguage.Text = "First language";
      this.ttl_FirstLanguage.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_FirstLanguage.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_FirstLanguage.TranslateText = true;
      this.ttl_FirstLanguage.Value = "";
      this.ttl_FirstLanguage.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_FirstLanguage.ValueMultiline = true;
      // 
      // tit_SecondLanguage
      // 
      this.tit_SecondLanguage.BackColor = System.Drawing.Color.Black;
      this.tit_SecondLanguage.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tit_SecondLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tit_SecondLanguage.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tit_SecondLanguage.Location = new System.Drawing.Point(359, 0);
      this.tit_SecondLanguage.Name = "tit_SecondLanguage";
      this.tit_SecondLanguage.Size = new System.Drawing.Size(347, 520);
      this.tit_SecondLanguage.Style = SyncroC.Controls.TitleStyles.Small;
      this.tit_SecondLanguage.TabIndex = 142;
      this.tit_SecondLanguage.Text = "SECOND LANGUAGE";
      this.tit_SecondLanguage.TranslateText = true;
      // 
      // grd_SecondLanguage
      // 
      this.grd_SecondLanguage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnLanguage2});
      this.grd_SecondLanguage.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Narrow;
      this.grd_SecondLanguage.EvenItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grd_SecondLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grd_SecondLanguage.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grd_SecondLanguage.HideSelection = true;
      this.grd_SecondLanguage.ImagePassword = null;
      this.grd_SecondLanguage.Location = new System.Drawing.Point(368, 125);
      this.grd_SecondLanguage.MultiSelect = false;
      this.grd_SecondLanguage.Name = "grd_SecondLanguage";
      this.grd_SecondLanguage.OddItemsColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grd_SecondLanguage.ShowForecolorItem = false;
      this.grd_SecondLanguage.ShowIndexesItem = false;
      this.grd_SecondLanguage.Size = new System.Drawing.Size(338, 392);
      this.grd_SecondLanguage.Sorting = System.Windows.Forms.SortOrder.None;
      this.grd_SecondLanguage.Style = SyncroC.Controls.GridStyles.Light;
      this.grd_SecondLanguage.TabIndex = 141;
      this.grd_SecondLanguage.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grd_SecondLanguage.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grd_SecondLanguage.TranslateText = true;
      this.grd_SecondLanguage.SelectedIndexChanged += new System.EventHandler(this.grd_SecondLanguage_SelectedIndexChanged);
      // 
      // columnLanguage2
      // 
      this.columnLanguage2.Text = "Languages";
      this.columnLanguage2.Width = 325;
      // 
      // grd_FirstLanguage
      // 
      this.grd_FirstLanguage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnLanguage1});
      this.grd_FirstLanguage.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Narrow;
      this.grd_FirstLanguage.EvenItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grd_FirstLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grd_FirstLanguage.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grd_FirstLanguage.HideSelection = true;
      this.grd_FirstLanguage.ImagePassword = null;
      this.grd_FirstLanguage.Location = new System.Drawing.Point(8, 125);
      this.grd_FirstLanguage.MultiSelect = false;
      this.grd_FirstLanguage.Name = "grd_FirstLanguage";
      this.grd_FirstLanguage.OddItemsColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grd_FirstLanguage.ShowForecolorItem = false;
      this.grd_FirstLanguage.ShowIndexesItem = false;
      this.grd_FirstLanguage.Size = new System.Drawing.Size(339, 392);
      this.grd_FirstLanguage.Sorting = System.Windows.Forms.SortOrder.None;
      this.grd_FirstLanguage.Style = SyncroC.Controls.GridStyles.Light;
      this.grd_FirstLanguage.TabIndex = 140;
      this.grd_FirstLanguage.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grd_FirstLanguage.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grd_FirstLanguage.TranslateText = true;
      this.grd_FirstLanguage.SelectedIndexChanged += new System.EventHandler(this.grd_FirstLanguage_SelectedIndexChanged);
      // 
      // columnLanguage1
      // 
      this.columnLanguage1.Text = "Languages";
      this.columnLanguage1.Width = 325;
      // 
      // tit_FirstLanguage
      // 
      this.tit_FirstLanguage.BackColor = System.Drawing.Color.Black;
      this.tit_FirstLanguage.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tit_FirstLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tit_FirstLanguage.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tit_FirstLanguage.Location = new System.Drawing.Point(0, 0);
      this.tit_FirstLanguage.Name = "tit_FirstLanguage";
      this.tit_FirstLanguage.Size = new System.Drawing.Size(347, 520);
      this.tit_FirstLanguage.Style = SyncroC.Controls.TitleStyles.Small;
      this.tit_FirstLanguage.TabIndex = 139;
      this.tit_FirstLanguage.Text = "FIRST LANGUAGE";
      this.tit_FirstLanguage.TranslateText = true;
      // 
      // lbl_FosberAddress
      // 
      this.lbl_FosberAddress.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_FosberAddress.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_FosberAddress.Location = new System.Drawing.Point(725, 195);
      this.lbl_FosberAddress.Name = "lbl_FosberAddress";
      this.lbl_FosberAddress.Size = new System.Drawing.Size(446, 93);
      this.lbl_FosberAddress.TabIndex = 155;
      this.lbl_FosberAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // chk_Console
      // 
      this.chk_Console.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Console.Checked = true;
      this.chk_Console.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_Console.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Console.ImagePassword = null;
      this.chk_Console.Location = new System.Drawing.Point(1005, 24);
      this.chk_Console.Name = "chk_Console";
      this.chk_Console.ReadOnly = true;
      this.chk_Console.Size = new System.Drawing.Size(166, 48);
      this.chk_Console.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_Console.TabIndex = 158;
      this.chk_Console.Text = "Console";
      this.chk_Console.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Console.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_Console.TranslateText = true;
      this.chk_Console.OnCheckedChanged += new System.EventHandler(this.chk_Console_OnCheckedChanged);
      // 
      // tlb_ProductDataVersion
      // 
      this.tlb_ProductDataVersion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_ProductDataVersion.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_ProductDataVersion.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ProductDataVersion.ImagePassword = null;
      this.tlb_ProductDataVersion.Location = new System.Drawing.Point(915, 417);
      this.tlb_ProductDataVersion.Name = "tlb_ProductDataVersion";
      this.tlb_ProductDataVersion.Size = new System.Drawing.Size(256, 48);
      this.tlb_ProductDataVersion.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_ProductDataVersion.TabIndex = 159;
      this.tlb_ProductDataVersion.Text = "Product Data version";
      this.tlb_ProductDataVersion.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ProductDataVersion.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ProductDataVersion.TranslateText = false;
      this.tlb_ProductDataVersion.Value = "";
      this.tlb_ProductDataVersion.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ProductDataVersion.ValueMultiline = true;
      // 
      // tlb_DSPUserTaskVersion
      // 
      this.tlb_DSPUserTaskVersion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_DSPUserTaskVersion.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_DSPUserTaskVersion.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DSPUserTaskVersion.ImagePassword = null;
      this.tlb_DSPUserTaskVersion.Location = new System.Drawing.Point(725, 471);
      this.tlb_DSPUserTaskVersion.Name = "tlb_DSPUserTaskVersion";
      this.tlb_DSPUserTaskVersion.Size = new System.Drawing.Size(166, 48);
      this.tlb_DSPUserTaskVersion.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_DSPUserTaskVersion.TabIndex = 160;
      this.tlb_DSPUserTaskVersion.Text = "DSP User Task version";
      this.tlb_DSPUserTaskVersion.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DSPUserTaskVersion.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DSPUserTaskVersion.TranslateText = false;
      this.tlb_DSPUserTaskVersion.Value = "";
      this.tlb_DSPUserTaskVersion.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DSPUserTaskVersion.ValueMultiline = true;
      // 
      // tlb_ITEVersion
      // 
      this.tlb_ITEVersion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_ITEVersion.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_ITEVersion.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ITEVersion.ImagePassword = null;
      this.tlb_ITEVersion.Location = new System.Drawing.Point(1019, 471);
      this.tlb_ITEVersion.Name = "tlb_ITEVersion";
      this.tlb_ITEVersion.Size = new System.Drawing.Size(151, 48);
      this.tlb_ITEVersion.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_ITEVersion.TabIndex = 161;
      this.tlb_ITEVersion.Text = "ITE version";
      this.tlb_ITEVersion.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ITEVersion.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ITEVersion.TranslateText = false;
      this.tlb_ITEVersion.Value = "";
      this.tlb_ITEVersion.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ITEVersion.ValueMultiline = true;
      // 
      // pct_Twin400Logo
      // 
      this.pct_Twin400Logo.Image = global::SyncroC.Properties.Resources.twin_01;
      this.pct_Twin400Logo.Location = new System.Drawing.Point(1022, 300);
      this.pct_Twin400Logo.Name = "pct_Twin400Logo";
      this.pct_Twin400Logo.Size = new System.Drawing.Size(129, 56);
      this.pct_Twin400Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Twin400Logo.TabIndex = 157;
      this.pct_Twin400Logo.TabStop = false;
      this.pct_Twin400Logo.DoubleClick += new System.EventHandler(this.pct_System_ForcePassword);
      // 
      // pct_Fosber
      // 
      this.pct_Fosber.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_Fosber.Image = global::SyncroC.Properties.Resources.logo_Fosber;
      this.pct_Fosber.Location = new System.Drawing.Point(898, 151);
      this.pct_Fosber.Name = "pct_Fosber";
      this.pct_Fosber.Size = new System.Drawing.Size(113, 31);
      this.pct_Fosber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Fosber.TabIndex = 156;
      this.pct_Fosber.TabStop = false;
      this.pct_Fosber.DoubleClick += new System.EventHandler(this.pct_Fosber_ForcePassword);
      // 
      // pct_SyncroLogo
      // 
      this.pct_SyncroLogo.Image = global::SyncroC.Properties.Resources.LogoSyncro;
      this.pct_SyncroLogo.Location = new System.Drawing.Point(739, 300);
      this.pct_SyncroLogo.Name = "pct_SyncroLogo";
      this.pct_SyncroLogo.Size = new System.Drawing.Size(131, 56);
      this.pct_SyncroLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_SyncroLogo.TabIndex = 148;
      this.pct_SyncroLogo.TabStop = false;
      this.pct_SyncroLogo.DoubleClick += new System.EventHandler(this.pct_Fosber_ForcePassword);
      // 
      // LanguageAndAccessibilitySettings
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Controls.Add(this.tlb_ITEVersion);
      this.Controls.Add(this.tlb_DSPUserTaskVersion);
      this.Controls.Add(this.tlb_ProductDataVersion);
      this.Controls.Add(this.chk_Console);
      this.Controls.Add(this.pct_Twin400Logo);
      this.Controls.Add(this.pct_Fosber);
      this.Controls.Add(this.lbl_FosberAddress);
      this.Controls.Add(this.ttb_pwd);
      this.Controls.Add(this.tlb_Level);
      this.Controls.Add(this.tlb_Platform);
      this.Controls.Add(this.tlb_ProductVersion);
      this.Controls.Add(this.pct_SyncroLogo);
      this.Controls.Add(this.tit_SecondLanguage);
      this.Controls.Add(this.tit_FirstLanguage);
      this.Controls.Add(this.chk_RapidTranslation);
      this.Controls.Add(this.ttl_SecondLanguage);
      this.Controls.Add(this.ttl_FirstLanguage);
      this.Controls.Add(this.grd_SecondLanguage);
      this.Controls.Add(this.grd_FirstLanguage);
      this.Controls.Add(this.tit_Info);
      this.Controls.Add(this.tit_PageAccessibility);
      this.Name = "LanguageAndAccessibilitySettings";
      this.Size = new System.Drawing.Size(1171, 519);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Twin400Logo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Fosber)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SyncroLogo)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void SetInfo()
    {
			lbl_FosberAddress.Text = PageManager.FOSBER_INFO;
      this.tlb_ProductVersion.Value = PageManager.getPageManager().getSyncroVersion();
			this.tlb_ProductDataVersion.Value = PageManager.getPageManager().getSyncroDataVersion();
			this.tlb_DSPUserTaskVersion.Value = DBManager.getDBManager().syncroConfig.GeneralSetting.DSPUserTask.ToString();
			this.tlb_ITEVersion.Value = PageManager.getPageManager().getITEVersion();
			this.tlb_Platform.Value = Environment.OSVersion.Platform.ToString() + "  ver. " + Environment.OSVersion.Version.ToString();
			//RFAZZI_FI04/02/08
      //agrassi_FI19-08-2010_1
			//this.tlb_SyncroSoftwareStandard.Value = PageManager.getPageManager().getSyncroSoftwareStandard();
    }

    public void RefreshData()
    {
      this.tlb_Level.Value =this.accessibility.Level.ToString();
			this.chk_Console.Checked = this.accessibility.ConsoleEnabled;

      //truncate all the records from the grids.
      try
      {
        this.grd_FirstLanguage.ClearItems();
        this.grd_SecondLanguage.ClearItems();
      }
      catch (Exception e)
      {
        Logger.LogError(this, "refreshData - error clearing grid items", e);
      }

      //sets the checkBox chk_RapidTranslation according to settings
      this.chk_RapidTranslation.Checked = this.settings.languages.RapidTranslationEnabled;

      //set list boxes according to the languages database		
      DataSet languages =this.language.language;
      if (languages == null
        || !(languages.Tables.Count > 0))
        Logger.LogError(this, "refreshData - error on database languages.");
      else
      {//database is fine
        DataTable tableLanguages = languages.Tables[0];
        for (int i = 1; i < tableLanguages.Columns.Count; i++)
        {
          string language = tableLanguages.Columns[i].ColumnName;
          ListViewItem firstLanguageNewItem = new ListViewItem(language);
          ListViewItem secondLanguageNewItem = new ListViewItem(language);
          this.grd_FirstLanguage.AddItem(firstLanguageNewItem);
          this.grd_SecondLanguage.AddItem(secondLanguageNewItem);
          if (settings.languages.FirstLanguage.Equals(language))
            this.ttl_FirstLanguage.Value = language;
          if (settings.languages.SecondLanguage.Equals(language))
            this.ttl_SecondLanguage.Value = language;
        }
      }
    }

    private void grd_SecondLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.grd_SecondLanguage.getSelectedItems().Count != 0 &&
        this.grd_SecondLanguage.getSelectedItems()[0].SubItems.Count != 0)
        this.ttl_SecondLanguage.Value = grd_SecondLanguage.getSelectedItems()[0].SubItems[0].Text;
    }

    private void grd_FirstLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.grd_FirstLanguage.getSelectedItems().Count != 0 &&
        this.grd_FirstLanguage.getSelectedItems()[0].SubItems.Count != 0)
        this.ttl_FirstLanguage.Value = grd_FirstLanguage.getSelectedItems()[0].SubItems[0].Text;

    }

    public void Save()
    {
      //save FirstLanguage settings and translate every application form
      string firstLanguageSelected = this.ttl_FirstLanguage.Value;
      if (!firstLanguageSelected.Equals(this.settings.languages.FirstLanguage))
      {
        this.settings.languages.FirstLanguage = firstLanguageSelected;
        this.settings.CurrentLanguage = firstLanguageSelected;
        this.translateFun(this.settings.CurrentLanguage);
        //agrassi_FI07-07-08
        this.Cursor = Cursors.WaitCursor;
        RTCManager.getRTCManager().TranslateAlarms();
        DBManager.getDBManager().alarmHistory.TranslateAlarms();
        DBManager.getDBManager().downTime.TranslateDowntimes();
        this.Cursor = Cursors.Arrow;
      }
      //save SecondLanguage settings
      string secondLanguageSelected = this.ttl_SecondLanguage.Value;
      if (!secondLanguageSelected.Equals(this.settings.languages.SecondLanguage))
        this.settings.languages.SecondLanguage = secondLanguageSelected;

      //save the rapid translation enabled/disbled
      if (!this.chk_RapidTranslation.Checked.Equals(this.settings.languages.RapidTranslationEnabled))
        this.settings.languages.RapidTranslationEnabled = this.chk_RapidTranslation.Checked;
    }

    public void SetAccessibility()
    {
      //set Accessibility level
      AccessibilityLevel level = this.accessibility.setAccessibilityLevel(this.ttb_pwd.Value);
      this.tlb_Level.Value = level.ToString();
      this.ttb_pwd.Value = string.Empty;
    }

    public void ResetAccessibility()
    {
      //reset Accessibility level
      AccessibilityLevel level = this.accessibility.resetAccessibilityLevel();
      this.tlb_Level.Value = level.ToString();
      this.ttb_pwd.Value = string.Empty;
    }

		private void LauyoutComponents()
		{
			Settings.Configuration conf = settings.configuration;
			pct_SyncroLogo.Visible = conf.DryEndControl || conf.WetEndControl;
			pct_Twin400Logo.Visible = conf.PMac400;

			if ((conf.DryEndControl || conf.WetEndControl) && !conf.PMac400)
			{
				pct_SyncroLogo.Location = new Point(
					Convert.ToInt32(tit_Info.Location.X + (tit_Info.Width - pct_SyncroLogo.Width) / 2), pct_SyncroLogo.Location.Y);

			}
			if (!(conf.DryEndControl || conf.WetEndControl) && conf.PMac400)
			{
				pct_Twin400Logo.Location = new Point(
					Convert.ToInt32(tit_Info.Location.X + (tit_Info.Width - pct_Twin400Logo.Width) / 2), pct_Twin400Logo.Location.Y);
			}
		}

		private void chk_Console_OnCheckedChanged(object sender, EventArgs e)
		{
			accessibility.ConsoleEnabled = chk_Console.Checked;
		}

		private void pct_Fosber_ForcePassword(object sender, EventArgs e)
		{
			//set Accessibility level
			AccessibilityLevel level = this.accessibility.setAccessibilityLevel("fosber");
			this.tlb_Level.Value = level.ToString();
			this.ttb_pwd.Value = string.Empty;
		}

		private void pct_System_ForcePassword(object sender, EventArgs e)
		{
			//set Accessibility level
      //if (Accessibility.getAccessibility().isLevelMedium())
      //{
      //  AccessibilityLevel level = this.accessibility.setAccessibilityLevel("fosbersoft");
      //  this.tlb_Level.Value = level.ToString();
      //  this.ttb_pwd.Value = string.Empty;
      //}
		}

	}
}

