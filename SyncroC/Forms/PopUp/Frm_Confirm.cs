using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.PopUp
{
	public class Frm_Confirm : SyncroC.Forms.PopUp.Frm_PopUp
	{
		private Button btn_Abort_YN;
		private Button btn_OK;
		private Label lbl_Confirmation_YN;
		private System.ComponentModel.IContainer components = null;

		public Frm_Confirm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Show the form as a modal dialog
		/// </summary>
		/// <param name="owner">Window that will own the modal dialog</param>
		/// <param name="action">message describing the action to be confirmed</param>
		public DialogResult ShowDialog(IWin32Window owner, string action)
		{
			lbl_Confirmation_YN.Text = action;
			return this.ShowDialog(owner);
		}

    //agrassi_FI11-11-2010_1
    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="img">image that shows the operation</param>
    public DialogResult ShowDialog(IWin32Window owner, Image img)
    {
      lbl_Confirmation_YN.Image = img;
      return this.ShowDialog(owner);
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Confirm));
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.lbl_Confirmation_YN = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // btn_Abort_YN
      // 
      this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_YN.Location = new System.Drawing.Point(189, 121);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_YN.TabIndex = 1;
      this.btn_Abort_YN.Text = "ABORT";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(25, 121);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 0;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // lbl_Confirmation_YN
      // 
      this.lbl_Confirmation_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Confirmation_YN.Location = new System.Drawing.Point(26, 19);
      this.lbl_Confirmation_YN.Name = "lbl_Confirmation_YN";
      this.lbl_Confirmation_YN.Size = new System.Drawing.Size(297, 92);
      this.lbl_Confirmation_YN.TabIndex = 6;
      this.lbl_Confirmation_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Frm_Confirm
      // 
      this.AcceptButton = this.btn_OK;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.lbl_Confirmation_YN);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_Confirm";
      this.Text = "Frm_Confirm";
      this.ResumeLayout(false);

		}
		#endregion

		private void btn_Abort_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}

