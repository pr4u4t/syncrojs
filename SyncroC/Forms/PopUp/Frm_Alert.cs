using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
	public class Frm_Alert : SyncroC.Forms.PopUp.Frm_PopUp
	{
		private System.Windows.Forms.Button btn_OK;
		private Label lbl_Text;
		private System.ComponentModel.IContainer components = null;

		public Frm_Alert()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Show the form as a modal dialog
		/// </summary>
		/// <param name="owner">Window that will own the modal dialog</param>
		public DialogResult ShowDialog(IWin32Window owner, string text)
		{
			lbl_Text.Text = text;
			return this.ShowDialog(owner);
		}

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    public DialogResult ShowDialog(string text)
    {
      lbl_Text.Text = text;
      return this.ShowDialog();
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
      this.btn_OK = new System.Windows.Forms.Button();
      this.lbl_Text = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.iml_AccessibilityImages.ImageSize = new System.Drawing.Size(30, 30);
      this.iml_AccessibilityImages.ImageStream = null;
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(100, 117);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(149, 57);
      this.btn_OK.TabIndex = 1;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // lbl_Text
      // 
      this.lbl_Text.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_Text.Location = new System.Drawing.Point(25, 25);
      this.lbl_Text.Name = "lbl_Text";
      this.lbl_Text.Size = new System.Drawing.Size(299, 89);
      this.lbl_Text.TabIndex = 2;
      this.lbl_Text.Text = "Text";
      this.lbl_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Frm_Alert
      // 
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.lbl_Text);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_Alert";
      this.Text = "Frm_Alert";
      this.ResumeLayout(false);

		}
		#endregion

		private void btn_OK_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}
	}
}


