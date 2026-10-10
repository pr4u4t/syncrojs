using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms
{
	public class Frm_CommFailure : SyncroC.Forms.Pages.Frm_Page
	{
		private System.Windows.Forms.Label lbl_1_YN;
		private System.Windows.Forms.Label lbl_2_YN;
		private System.Windows.Forms.Label lbl_3_YN;
		private System.Windows.Forms.TextBox txt_Description_NY;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Size the page has been designed.
		/// </summary>
		public new static readonly Size ORIGINAL_SIZE = new Size(1280, 1024);

		public Frm_CommFailure()
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

		/// <summary>
		/// return the "run time" page's size 
		/// </summary>
		/// <returns></returns>
		protected override Size getSize()
		{
			return new Size(
				//width
        this.getScreenSize().Width,
				//height
        this.getScreenSize().Height);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CommFailure));
      this.lbl_1_YN = new System.Windows.Forms.Label();
      this.lbl_2_YN = new System.Windows.Forms.Label();
      this.lbl_3_YN = new System.Windows.Forms.Label();
      this.txt_Description_NY = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.CausesValidation = false;
      this.lbl_Title_YN.Text = "Communication failure";
      // 
      // lbl_Time
      // 
      this.lbl_Time.CausesValidation = false;
      // 
      // lbl_Date
      // 
      this.lbl_Date.CausesValidation = false;
      // 
      // lbl_Status
      // 
      this.lbl_Status.CausesValidation = false;
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.CausesValidation = false;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.CausesValidation = false;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.CausesValidation = false;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.CausesValidation = false;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.CausesValidation = false;
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.CausesValidation = false;
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.CausesValidation = false;
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.CausesValidation = false;
      // 
      // tab_Tab9
      // 
      this.tab_Tab9.CausesValidation = false;
      // 
      // tab_TabA
      // 
      this.tab_TabA.CausesValidation = false;
      // 
      // tab_TabD
      // 
      this.tab_TabD.CausesValidation = false;
      // 
      // tab_TabC
      // 
      this.tab_TabC.CausesValidation = false;
      // 
      // tab_TabB
      // 
      this.tab_TabB.CausesValidation = false;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.CausesValidation = false;
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.CausesValidation = false;
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.CausesValidation = false;
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.CausesValidation = false;
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.CausesValidation = false;
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.CausesValidation = false;
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.CausesValidation = false;
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.CausesValidation = false;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.CausesValidation = false;
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.CausesValidation = false;
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.CausesValidation = false;
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.CausesValidation = false;
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.CausesValidation = false;
      // 
      // btn_Group1
      // 
      this.btn_Group1.CausesValidation = false;
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.CausesValidation = false;
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.CausesValidation = false;
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.CausesValidation = false;
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // lbl_1_YN
      // 
      this.lbl_1_YN.BackColor = System.Drawing.Color.Red;
      this.lbl_1_YN.CausesValidation = false;
      this.lbl_1_YN.Font = new System.Drawing.Font("Arial Unicode MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_1_YN.Location = new System.Drawing.Point(30, 193);
      this.lbl_1_YN.Name = "lbl_1_YN";
      this.lbl_1_YN.Size = new System.Drawing.Size(1171, 96);
      this.lbl_1_YN.TabIndex = 52;
      this.lbl_1_YN.Text = "Communication failure.";
      this.lbl_1_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_2_YN
      // 
      this.lbl_2_YN.CausesValidation = false;
      this.lbl_2_YN.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_2_YN.Location = new System.Drawing.Point(30, 323);
      this.lbl_2_YN.Name = "lbl_2_YN";
      this.lbl_2_YN.Size = new System.Drawing.Size(1171, 40);
      this.lbl_2_YN.TabIndex = 54;
      this.lbl_2_YN.Text = "Please wait,";
      this.lbl_2_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_3_YN
      // 
      this.lbl_3_YN.CausesValidation = false;
      this.lbl_3_YN.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_3_YN.Location = new System.Drawing.Point(30, 368);
      this.lbl_3_YN.Name = "lbl_3_YN";
      this.lbl_3_YN.Size = new System.Drawing.Size(1171, 40);
      this.lbl_3_YN.TabIndex = 55;
      this.lbl_3_YN.Text = "Syncro is trying to restore the communication.";
      this.lbl_3_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // txt_Description_NY
      // 
      this.txt_Description_NY.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txt_Description_NY.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txt_Description_NY.CausesValidation = false;
      this.txt_Description_NY.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_Description_NY.Location = new System.Drawing.Point(30, 432);
      this.txt_Description_NY.Multiline = true;
      this.txt_Description_NY.Name = "txt_Description_NY";
      this.txt_Description_NY.ReadOnly = true;
      this.txt_Description_NY.Size = new System.Drawing.Size(1171, 280);
      this.txt_Description_NY.TabIndex = 56;
      this.txt_Description_NY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // Frm_CommFailure
      // 
      this.CausesValidation = false;
      this.ClientSize = new System.Drawing.Size(1280, 1024);
      this.Controls.Add(this.txt_Description_NY);
      this.Controls.Add(this.lbl_3_YN);
      this.Controls.Add(this.lbl_2_YN);
      this.Controls.Add(this.lbl_1_YN);
      this.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "Frm_CommFailure";
      this.Load += new System.EventHandler(this.Frm_CommFailure_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_1_YN, 0);
      this.Controls.SetChildIndex(this.lbl_2_YN, 0);
      this.Controls.SetChildIndex(this.lbl_3_YN, 0);
      this.Controls.SetChildIndex(this.txt_Description_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
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
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		#region ShifF1 .. ShiftF6 methods overrides
		/// <summary>
		/// Must do nothing on ShifF1.
		/// </summary>
		protected override void ShiftF1()
		{
		}
		/// <summary>
		/// Must do nothing on ShifF2.
		/// </summary>
		protected override void ShiftF2()
		{
		}
		/// <summary>
		/// Must do nothing on ShifF3.
		/// </summary>
		protected override void ShiftF3()
		{
		}/// <summary>
		/// Must do nothing on ShifF4.
		/// </summary>
		protected override void ShiftF4()
		{
		}/// <summary>
		/// Must do nothing on ShifF5.
		/// </summary>
		protected override void ShiftF5()
		{
		}/// <summary>
		/// Must do nothing on ShifF6.
		/// </summary>
		protected override void ShiftF6()
		{
		}
		#endregion

		/// <summary>
		/// Set the txt_Description text.
		/// </summary>
		/// <param name="description">The text to set.</param>
		/// <param name="append">if true the specified text will be appended to the current text
		/// if false the specified message will substitute the previous text.</param>
		public void setTxt_Description(string description, bool append)
		{
			if (append)
				txt_Description_NY.Text += description + "\r\n";
			else
				txt_Description_NY.Text = description + "\r\n";
			txt_Description_NY.Select(txt_Description_NY.Text.Length -1, 0);
			txt_Description_NY.ScrollToCaret();
		}

    //agrassi_FI14-12-2010_2
    private void Frm_CommFailure_Load(object sender, EventArgs e)
    {
      this.ControlBox = true;
      this.FormBorderStyle = FormBorderStyle.Sizable;
    }
	}
}

