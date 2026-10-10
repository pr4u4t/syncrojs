namespace SyncroC.Forms.PopUp
{
  partial class Frm_PRCCurve
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PRCCurve));
      this.btn_OK = new System.Windows.Forms.Button();
      this.lbl_typeOfQuery = new SyncroC.Controls.myLabel();
      this.lbl_typeOfClassification = new SyncroC.Controls.myLabel();
      this.graphLinesEditor = new SyncroC.Graph.GraphLinesEditor();
      this.btn_Abort_NN = new System.Windows.Forms.Button();
      this.chk_Modify = new SyncroC.Controls.CheckBox();
      this.btn_SaveOnQuality_NN = new System.Windows.Forms.Button();
      this.btn_SaveOnFlute_NN = new System.Windows.Forms.Button();
      this.timer_ControllaModifiche = new System.Windows.Forms.Timer(this.components);
      this.pct_Lock = new System.Windows.Forms.PictureBox();
      this.pct_PRCoverride = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Lock)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_PRCoverride)).BeginInit();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(247, 421);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 60);
      this.btn_OK.TabIndex = 2;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // lbl_typeOfQuery
      // 
      this.lbl_typeOfQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.lbl_typeOfQuery.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_typeOfQuery.Colore1 = System.Drawing.Color.White;
      this.lbl_typeOfQuery.Colore2 = System.Drawing.Color.White;
      this.lbl_typeOfQuery.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_typeOfQuery.ForeColor = System.Drawing.Color.White;
      this.lbl_typeOfQuery.Location = new System.Drawing.Point(257, 23);
      this.lbl_typeOfQuery.Name = "lbl_typeOfQuery";
      this.lbl_typeOfQuery.OutLineText = true;
      this.lbl_typeOfQuery.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_typeOfQuery.Size = new System.Drawing.Size(214, 40);
      this.lbl_typeOfQuery.TabIndex = 508;
      this.lbl_typeOfQuery.Testo = "AB VeryHeavy Kw-Kw";
      this.lbl_typeOfQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_typeOfQuery.TranslateText = false;
      this.lbl_typeOfQuery.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_typeOfQuery_Paint);
      // 
      // lbl_typeOfClassification
      // 
      this.lbl_typeOfClassification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.lbl_typeOfClassification.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_typeOfClassification.Colore1 = System.Drawing.Color.White;
      this.lbl_typeOfClassification.Colore2 = System.Drawing.Color.White;
      this.lbl_typeOfClassification.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_typeOfClassification.ForeColor = System.Drawing.Color.White;
      this.lbl_typeOfClassification.Location = new System.Drawing.Point(30, 23);
      this.lbl_typeOfClassification.Name = "lbl_typeOfClassification";
      this.lbl_typeOfClassification.OutLineText = true;
      this.lbl_typeOfClassification.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_typeOfClassification.Size = new System.Drawing.Size(214, 40);
      this.lbl_typeOfClassification.TabIndex = 507;
      this.lbl_typeOfClassification.Testo = "AB VeryHeavy Kw-Kw";
      this.lbl_typeOfClassification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_typeOfClassification.TranslateText = false;
      this.lbl_typeOfClassification.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_typeOfClassification_Paint);
      // 
      // graphLinesEditor
      // 
      this.graphLinesEditor.BackColor = System.Drawing.Color.WhiteSmoke;
      this.graphLinesEditor.Location = new System.Drawing.Point(27, 69);
      this.graphLinesEditor.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
      this.graphLinesEditor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.graphLinesEditor.MySuspendLayout = false;
      this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmGraphLinesEditor;
      this.graphLinesEditor.Name = "graphLinesEditor";
      this.graphLinesEditor.ReadOnly = true;
      this.graphLinesEditor.Size = new System.Drawing.Size(597, 346);
      this.graphLinesEditor.TabIndex = 509;
      this.graphLinesEditor.Title = "";
      // 
      // btn_Abort_NN
      // 
      this.btn_Abort_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_NN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_NN.Location = new System.Drawing.Point(247, 421);
      this.btn_Abort_NN.Name = "btn_Abort_NN";
      this.btn_Abort_NN.Size = new System.Drawing.Size(135, 60);
      this.btn_Abort_NN.TabIndex = 511;
      this.btn_Abort_NN.Text = "ABORT";
      this.btn_Abort_NN.UseVisualStyleBackColor = false;
      this.btn_Abort_NN.Visible = false;
      this.btn_Abort_NN.Click += new System.EventHandler(this.btn_Abort_YN_Click);
      // 
      // chk_Modify
      // 
      this.chk_Modify.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Modify.Checked = false;
      this.chk_Modify.Cursor = System.Windows.Forms.Cursors.Hand;
      this.chk_Modify.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_Modify.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Modify.ImagePassword = null;
      this.chk_Modify.Location = new System.Drawing.Point(551, 23);
      this.chk_Modify.Name = "chk_Modify";
      this.chk_Modify.ReadOnly = false;
      this.chk_Modify.Size = new System.Drawing.Size(74, 40);
      this.chk_Modify.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_Modify.TabIndex = 512;
      this.chk_Modify.Text = "Modify";
      this.chk_Modify.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Modify.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Modify.TranslateText = false;
      this.chk_Modify.OnCheckedChanged += new System.EventHandler(this.chk_Modify_OnCheckedChanged);
      // 
      // btn_SaveOnQuality_NN
      // 
      this.btn_SaveOnQuality_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SaveOnQuality_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_SaveOnQuality_NN.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.btn_SaveOnQuality_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_SaveOnQuality_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_SaveOnQuality_NN.Location = new System.Drawing.Point(27, 421);
      this.btn_SaveOnQuality_NN.Name = "btn_SaveOnQuality_NN";
      this.btn_SaveOnQuality_NN.Size = new System.Drawing.Size(135, 60);
      this.btn_SaveOnQuality_NN.TabIndex = 513;
      this.btn_SaveOnQuality_NN.Text = "Save (Quality)";
      this.btn_SaveOnQuality_NN.UseVisualStyleBackColor = false;
      this.btn_SaveOnQuality_NN.Visible = false;
      this.btn_SaveOnQuality_NN.Click += new System.EventHandler(this.btn_SaveOnQuality_NN_Click);
      // 
      // btn_SaveOnFlute_NN
      // 
      this.btn_SaveOnFlute_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_SaveOnFlute_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_SaveOnFlute_NN.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.btn_SaveOnFlute_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_SaveOnFlute_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_SaveOnFlute_NN.Location = new System.Drawing.Point(489, 421);
      this.btn_SaveOnFlute_NN.Name = "btn_SaveOnFlute_NN";
      this.btn_SaveOnFlute_NN.Size = new System.Drawing.Size(135, 60);
      this.btn_SaveOnFlute_NN.TabIndex = 514;
      this.btn_SaveOnFlute_NN.Text = "Save (Flute)";
      this.btn_SaveOnFlute_NN.UseVisualStyleBackColor = false;
      this.btn_SaveOnFlute_NN.Visible = false;
      this.btn_SaveOnFlute_NN.Click += new System.EventHandler(this.btn_SaveOnFlute_NN_Click);
      // 
      // timer_ControllaModifiche
      // 
      this.timer_ControllaModifiche.Tick += new System.EventHandler(this.timer_ControllaModifiche_Tick);
      // 
      // pct_Lock
      // 
      this.pct_Lock.Image = global::SyncroC.Properties.Resources.Lock_Blue;
      this.pct_Lock.Location = new System.Drawing.Point(534, 23);
      this.pct_Lock.Name = "pct_Lock";
      this.pct_Lock.Size = new System.Drawing.Size(52, 40);
      this.pct_Lock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Lock.TabIndex = 516;
      this.pct_Lock.TabStop = false;
      this.pct_Lock.Visible = false;
      // 
      // pct_PRCoverride
      // 
      this.pct_PRCoverride.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_PRCoverride.Image = global::SyncroC.Properties.Resources.ReturnToDefault;
      this.pct_PRCoverride.Location = new System.Drawing.Point(477, 23);
      this.pct_PRCoverride.Name = "pct_PRCoverride";
      this.pct_PRCoverride.Size = new System.Drawing.Size(67, 40);
      this.pct_PRCoverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_PRCoverride.TabIndex = 515;
      this.pct_PRCoverride.TabStop = false;
      this.pct_PRCoverride.Visible = false;
      this.pct_PRCoverride.Click += new System.EventHandler(this.pct_PRCoverride_Click);
      // 
      // Frm_PRCCurve
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.CancelButton = this.btn_Abort_NN;
      this.ClientSize = new System.Drawing.Size(650, 500);
      this.Controls.Add(this.pct_Lock);
      this.Controls.Add(this.pct_PRCoverride);
      this.Controls.Add(this.btn_SaveOnFlute_NN);
      this.Controls.Add(this.btn_OK);
      this.Controls.Add(this.btn_SaveOnQuality_NN);
      this.Controls.Add(this.chk_Modify);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.graphLinesEditor);
      this.Controls.Add(this.lbl_typeOfQuery);
      this.Controls.Add(this.lbl_typeOfClassification);
      this.MaximumSize = new System.Drawing.Size(650, 500);
      this.MinimumSize = new System.Drawing.Size(650, 500);
      this.Name = "Frm_PRCCurve";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Frm_PRCCurve";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Lock)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_PRCoverride)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btn_OK;
    private SyncroC.Controls.myLabel lbl_typeOfQuery;
    private SyncroC.Controls.myLabel lbl_typeOfClassification;
    private SyncroC.Graph.GraphLinesEditor graphLinesEditor;
    private System.Windows.Forms.Button btn_Abort_NN;
    private SyncroC.Controls.CheckBox chk_Modify;
    private System.Windows.Forms.Button btn_SaveOnQuality_NN;
    private System.Windows.Forms.Button btn_SaveOnFlute_NN;
    private System.Windows.Forms.PictureBox pct_PRCoverride;
    private System.Windows.Forms.Timer timer_ControllaModifiche;
    private System.Windows.Forms.PictureBox pct_Lock;
  }
}