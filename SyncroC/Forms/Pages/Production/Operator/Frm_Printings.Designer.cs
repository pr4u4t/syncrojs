namespace SyncroC.Forms.Pages.Production.Operator
{
    partial class Frm_Printings
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Printings));
          this.pct_Bitmap = new System.Windows.Forms.PictureBox();
          this.FileNames = new System.Windows.Forms.ColumnHeader();
          this.grd_Bitmap = new SyncroC.Controls.Grid();
          this.tim_Refresh = new System.Windows.Forms.Timer(this.components);
          this.grp_Bitmap = new System.Windows.Forms.GroupBox();
          this.lbl_DateAndTimeValue = new System.Windows.Forms.Label();
          this.lbl_DateAndTime_YN = new System.Windows.Forms.Label();
          this.lbl_NumberValue = new System.Windows.Forms.Label();
          this.lbl_Number = new System.Windows.Forms.Label();
          this.lbl_LevelOrDateValue = new System.Windows.Forms.Label();
          this.lbl_LevelOrDate = new System.Windows.Forms.Label();
          this.lbl_CodeOrShiftIdValue = new System.Windows.Forms.Label();
          this.lbl_CodeOrShiftId = new System.Windows.Forms.Label();
          this.lbl_Type = new System.Windows.Forms.Label();
          this.lbl_PrintingIdxM_NN = new System.Windows.Forms.Label();
          this.lbl_PrintingIdxL_NN = new System.Windows.Forms.Label();
          this.lbl_PrintingIdxU_NN = new System.Windows.Forms.Label();
          this.btn_PrintU_NN = new System.Windows.Forms.Button();
          this.btn_PrintM_NN = new System.Windows.Forms.Button();
          this.btn_PrintL_NN = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Bitmap)).BeginInit();
          this.grp_Bitmap.SuspendLayout();
          this.SuspendLayout();
          // 
          // tab_TabB
          // 
          this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
          // 
          // lbl_CtrlF4_YY
          // 
          this.lbl_CtrlF4_YY.Text = "Adjust Ind. M";
          // 
          // lbl_CtrlF1_YY
          // 
          this.lbl_CtrlF1_YY.Text = "Print";
          // 
          // lbl_CtrlF2_YY
          // 
          this.lbl_CtrlF2_YY.Text = "Refresh";
          // 
          // lbl_CtrlF5_YY
          // 
          this.lbl_CtrlF5_YY.Text = "Adjust Ind. U";
          // 
          // lbl_CtrlF3_YY
          // 
          this.lbl_CtrlF3_YY.Text = "Adjust Ind. L";
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
          // pct_Bitmap
          // 
          this.pct_Bitmap.BackColor = System.Drawing.Color.LightGray;
          this.pct_Bitmap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.pct_Bitmap.Location = new System.Drawing.Point(822, 168);
          this.pct_Bitmap.Name = "pct_Bitmap";
          this.pct_Bitmap.Size = new System.Drawing.Size(401, 567);
          this.pct_Bitmap.TabIndex = 110;
          this.pct_Bitmap.TabStop = false;
          // 
          // FileNames
          // 
          this.FileNames.Text = "Available Bitmaps";
          this.FileNames.Width = 277;
          // 
          // grd_Bitmap
          // 
          this.grd_Bitmap.BackColor = System.Drawing.Color.LightGray;
          this.grd_Bitmap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNames});
          this.grd_Bitmap.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Narrow;
          this.grd_Bitmap.EvenItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.grd_Bitmap.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.grd_Bitmap.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.grd_Bitmap.HideSelection = false;
          this.grd_Bitmap.ImagePassword = null;
          this.grd_Bitmap.Location = new System.Drawing.Point(30, 169);
          this.grd_Bitmap.MultiSelect = true;
          this.grd_Bitmap.Name = "grd_Bitmap";
          this.grd_Bitmap.OddItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.grd_Bitmap.ShowForecolorItem = false;
          this.grd_Bitmap.ShowIndexesItem = false;
          this.grd_Bitmap.Size = new System.Drawing.Size(355, 563);
          this.grd_Bitmap.Sorting = System.Windows.Forms.SortOrder.None;
          this.grd_Bitmap.Style = SyncroC.Controls.GridStyles.Light;
          this.grd_Bitmap.TabIndex = 106;
          this.grd_Bitmap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.grd_Bitmap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.grd_Bitmap.TranslateText = false;
          this.grd_Bitmap.LostFocus += new System.EventHandler(this.grd_Bitmap_LostFocus);
          this.grd_Bitmap.SelectedIndexChanged += new System.EventHandler(this.grd_Bitmap_SelectedIndexChanged);
          // 
          // tim_Refresh
          // 
          this.tim_Refresh.Interval = 300000;
          this.tim_Refresh.Tick += new System.EventHandler(this.tim_Refresh_Tick);
          // 
          // grp_Bitmap
          // 
          this.grp_Bitmap.Controls.Add(this.lbl_DateAndTimeValue);
          this.grp_Bitmap.Controls.Add(this.lbl_DateAndTime_YN);
          this.grp_Bitmap.Controls.Add(this.lbl_NumberValue);
          this.grp_Bitmap.Controls.Add(this.lbl_Number);
          this.grp_Bitmap.Controls.Add(this.lbl_LevelOrDateValue);
          this.grp_Bitmap.Controls.Add(this.lbl_LevelOrDate);
          this.grp_Bitmap.Controls.Add(this.lbl_CodeOrShiftIdValue);
          this.grp_Bitmap.Controls.Add(this.lbl_CodeOrShiftId);
          this.grp_Bitmap.Controls.Add(this.lbl_Type);
          this.grp_Bitmap.Location = new System.Drawing.Point(391, 341);
          this.grp_Bitmap.Name = "grp_Bitmap";
          this.grp_Bitmap.Size = new System.Drawing.Size(431, 214);
          this.grp_Bitmap.TabIndex = 111;
          this.grp_Bitmap.TabStop = false;
          // 
          // lbl_DateAndTimeValue
          // 
          this.lbl_DateAndTimeValue.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_DateAndTimeValue.Location = new System.Drawing.Point(216, 74);
          this.lbl_DateAndTimeValue.Name = "lbl_DateAndTimeValue";
          this.lbl_DateAndTimeValue.Size = new System.Drawing.Size(193, 21);
          this.lbl_DateAndTimeValue.TabIndex = 8;
          this.lbl_DateAndTimeValue.Text = "12/02/2006 13:21:55";
          // 
          // lbl_DateAndTime_YN
          // 
          this.lbl_DateAndTime_YN.Location = new System.Drawing.Point(23, 77);
          this.lbl_DateAndTime_YN.Name = "lbl_DateAndTime_YN";
          this.lbl_DateAndTime_YN.Size = new System.Drawing.Size(169, 21);
          this.lbl_DateAndTime_YN.TabIndex = 7;
          this.lbl_DateAndTime_YN.Text = "Printing Date and Time";
          // 
          // lbl_NumberValue
          // 
          this.lbl_NumberValue.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_NumberValue.Location = new System.Drawing.Point(134, 170);
          this.lbl_NumberValue.Name = "lbl_NumberValue";
          this.lbl_NumberValue.Size = new System.Drawing.Size(58, 21);
          this.lbl_NumberValue.TabIndex = 6;
          this.lbl_NumberValue.Text = "25";
          // 
          // lbl_Number
          // 
          this.lbl_Number.Location = new System.Drawing.Point(23, 173);
          this.lbl_Number.Name = "lbl_Number";
          this.lbl_Number.Size = new System.Drawing.Size(110, 21);
          this.lbl_Number.TabIndex = 5;
          this.lbl_Number.Text = "Label Number";
          // 
          // lbl_LevelOrDateValue
          // 
          this.lbl_LevelOrDateValue.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_LevelOrDateValue.Location = new System.Drawing.Point(321, 124);
          this.lbl_LevelOrDateValue.Name = "lbl_LevelOrDateValue";
          this.lbl_LevelOrDateValue.Size = new System.Drawing.Size(108, 21);
          this.lbl_LevelOrDateValue.TabIndex = 4;
          this.lbl_LevelOrDateValue.Text = "Upper";
          // 
          // lbl_LevelOrDate
          // 
          this.lbl_LevelOrDate.Location = new System.Drawing.Point(259, 127);
          this.lbl_LevelOrDate.Name = "lbl_LevelOrDate";
          this.lbl_LevelOrDate.Size = new System.Drawing.Size(69, 21);
          this.lbl_LevelOrDate.TabIndex = 3;
          this.lbl_LevelOrDate.Text = "Level";
          // 
          // lbl_CodeOrShiftIdValue
          // 
          this.lbl_CodeOrShiftIdValue.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_CodeOrShiftIdValue.Location = new System.Drawing.Point(117, 124);
          this.lbl_CodeOrShiftIdValue.Name = "lbl_CodeOrShiftIdValue";
          this.lbl_CodeOrShiftIdValue.Size = new System.Drawing.Size(110, 21);
          this.lbl_CodeOrShiftIdValue.TabIndex = 2;
          this.lbl_CodeOrShiftIdValue.Text = "801589701";
          // 
          // lbl_CodeOrShiftId
          // 
          this.lbl_CodeOrShiftId.Location = new System.Drawing.Point(23, 125);
          this.lbl_CodeOrShiftId.Name = "lbl_CodeOrShiftId";
          this.lbl_CodeOrShiftId.Size = new System.Drawing.Size(81, 29);
          this.lbl_CodeOrShiftId.TabIndex = 1;
          this.lbl_CodeOrShiftId.Text = "Setup Code";
          // 
          // lbl_Type
          // 
          this.lbl_Type.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Type.Location = new System.Drawing.Point(75, 22);
          this.lbl_Type.Name = "lbl_Type";
          this.lbl_Type.Size = new System.Drawing.Size(296, 33);
          this.lbl_Type.TabIndex = 0;
          this.lbl_Type.Text = "Pallet Label";
          this.lbl_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // lbl_PrintingIdxM_NN
          // 
          this.lbl_PrintingIdxM_NN.AutoSize = true;
          this.lbl_PrintingIdxM_NN.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_PrintingIdxM_NN.ForeColor = System.Drawing.Color.Navy;
          this.lbl_PrintingIdxM_NN.Location = new System.Drawing.Point(456, 248);
          this.lbl_PrintingIdxM_NN.Name = "lbl_PrintingIdxM_NN";
          this.lbl_PrintingIdxM_NN.Size = new System.Drawing.Size(128, 15);
          this.lbl_PrintingIdxM_NN.TabIndex = 117;
          this.lbl_PrintingIdxM_NN.Text = "Printing Index Medium:";
          this.lbl_PrintingIdxM_NN.Visible = false;
          // 
          // lbl_PrintingIdxL_NN
          // 
          this.lbl_PrintingIdxL_NN.AutoSize = true;
          this.lbl_PrintingIdxL_NN.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_PrintingIdxL_NN.ForeColor = System.Drawing.Color.Navy;
          this.lbl_PrintingIdxL_NN.Location = new System.Drawing.Point(456, 292);
          this.lbl_PrintingIdxL_NN.Name = "lbl_PrintingIdxL_NN";
          this.lbl_PrintingIdxL_NN.Size = new System.Drawing.Size(119, 15);
          this.lbl_PrintingIdxL_NN.TabIndex = 116;
          this.lbl_PrintingIdxL_NN.Text = "Printing Index Lower:";
          this.lbl_PrintingIdxL_NN.Visible = false;
          // 
          // lbl_PrintingIdxU_NN
          // 
          this.lbl_PrintingIdxU_NN.AutoSize = true;
          this.lbl_PrintingIdxU_NN.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_PrintingIdxU_NN.ForeColor = System.Drawing.Color.Navy;
          this.lbl_PrintingIdxU_NN.Location = new System.Drawing.Point(456, 204);
          this.lbl_PrintingIdxU_NN.Name = "lbl_PrintingIdxU_NN";
          this.lbl_PrintingIdxU_NN.Size = new System.Drawing.Size(119, 15);
          this.lbl_PrintingIdxU_NN.TabIndex = 118;
          this.lbl_PrintingIdxU_NN.Text = "Printing Index Upper:";
          this.lbl_PrintingIdxU_NN.Visible = false;
          // 
          // btn_PrintU_NN
          // 
          this.btn_PrintU_NN.Image = global::SyncroC.Properties.Resources.PrinterIcon;
          this.btn_PrintU_NN.Location = new System.Drawing.Point(395, 198);
          this.btn_PrintU_NN.Name = "btn_PrintU_NN";
          this.btn_PrintU_NN.Size = new System.Drawing.Size(52, 31);
          this.btn_PrintU_NN.TabIndex = 121;
          this.btn_PrintU_NN.Text = "\r\n";
          this.btn_PrintU_NN.UseVisualStyleBackColor = true;
          this.btn_PrintU_NN.Visible = false;
          this.btn_PrintU_NN.Click += new System.EventHandler(this.MakeAPrint_U);
          // 
          // btn_PrintM_NN
          // 
          this.btn_PrintM_NN.Image = global::SyncroC.Properties.Resources.PrinterIcon;
          this.btn_PrintM_NN.Location = new System.Drawing.Point(395, 241);
          this.btn_PrintM_NN.Name = "btn_PrintM_NN";
          this.btn_PrintM_NN.Size = new System.Drawing.Size(52, 31);
          this.btn_PrintM_NN.TabIndex = 120;
          this.btn_PrintM_NN.UseVisualStyleBackColor = true;
          this.btn_PrintM_NN.Visible = false;
          this.btn_PrintM_NN.Click += new System.EventHandler(this.MakeAPrint_M);
          // 
          // btn_PrintL_NN
          // 
          this.btn_PrintL_NN.Image = global::SyncroC.Properties.Resources.PrinterIcon;
          this.btn_PrintL_NN.Location = new System.Drawing.Point(395, 284);
          this.btn_PrintL_NN.Name = "btn_PrintL_NN";
          this.btn_PrintL_NN.Size = new System.Drawing.Size(52, 31);
          this.btn_PrintL_NN.TabIndex = 119;
          this.btn_PrintL_NN.UseVisualStyleBackColor = true;
          this.btn_PrintL_NN.Visible = false;
          this.btn_PrintL_NN.Click += new System.EventHandler(this.MakeAPrint_L);
          // 
          // Frm_Printings
          // 
          this.ClientSize = new System.Drawing.Size(1280, 738);
          this.Controls.Add(this.btn_PrintU_NN);
          this.Controls.Add(this.btn_PrintM_NN);
          this.Controls.Add(this.btn_PrintL_NN);
          this.Controls.Add(this.lbl_PrintingIdxM_NN);
          this.Controls.Add(this.lbl_PrintingIdxL_NN);
          this.Controls.Add(this.lbl_PrintingIdxU_NN);
          this.Controls.Add(this.grd_Bitmap);
          this.Controls.Add(this.pct_Bitmap);
          this.Controls.Add(this.grp_Bitmap);
          this.Name = "Frm_Printings";
          this.Activated += new System.EventHandler(this.Frm_Printings_Activated);
          this.Controls.SetChildIndex(this.tab_Tab9, 0);
          this.Controls.SetChildIndex(this.grp_Bitmap, 0);
          this.Controls.SetChildIndex(this.pct_Bitmap, 0);
          this.Controls.SetChildIndex(this.grd_Bitmap, 0);
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
          this.Controls.SetChildIndex(this.lbl_PrintingIdxU_NN, 0);
          this.Controls.SetChildIndex(this.lbl_PrintingIdxL_NN, 0);
          this.Controls.SetChildIndex(this.lbl_PrintingIdxM_NN, 0);
          this.Controls.SetChildIndex(this.btn_PrintL_NN, 0);
          this.Controls.SetChildIndex(this.btn_PrintM_NN, 0);
          this.Controls.SetChildIndex(this.btn_PrintU_NN, 0);
          ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Bitmap)).EndInit();
          this.grp_Bitmap.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pct_Bitmap;
        private SyncroC.Controls.Grid grd_Bitmap;
        private System.Windows.Forms.ColumnHeader FileNames;
      private System.Windows.Forms.Timer tim_Refresh;
      private System.Windows.Forms.GroupBox grp_Bitmap;
      private System.Windows.Forms.Label lbl_Type;
      private System.Windows.Forms.Label lbl_CodeOrShiftId;
      private System.Windows.Forms.Label lbl_CodeOrShiftIdValue;
      private System.Windows.Forms.Label lbl_LevelOrDateValue;
      private System.Windows.Forms.Label lbl_LevelOrDate;
      private System.Windows.Forms.Label lbl_NumberValue;
      private System.Windows.Forms.Label lbl_Number;
      private System.Windows.Forms.Label lbl_DateAndTimeValue;
      private System.Windows.Forms.Label lbl_DateAndTime_YN;
        private System.Windows.Forms.Label lbl_PrintingIdxM_NN;
        private System.Windows.Forms.Label lbl_PrintingIdxL_NN;
        private System.Windows.Forms.Label lbl_PrintingIdxU_NN;
        private System.Windows.Forms.Button btn_PrintU_NN;
        private System.Windows.Forms.Button btn_PrintM_NN;
        private System.Windows.Forms.Button btn_PrintL_NN;

    }
}
