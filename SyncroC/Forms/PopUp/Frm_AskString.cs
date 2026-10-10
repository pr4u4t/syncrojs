using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
	public class Frm_AskString : SyncroC.Forms.PopUp.Frm_PopUp
	{
		private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Button btn_Abort_YN;
		private System.ComponentModel.IContainer components = null;
    private SyncroC.Controls.TitledTextBox ttb_Value;

		public Frm_AskString()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Show the form as a modal dialog
		/// </summary>
		/// <param name="owner">Window that will own the modal dialog</param>
		/// <param name="value"></param>
		public FosberDialogResult ShowDialog(IWin32Window owner, string text, int value)
		{
			ttb_Value.Text = text;
			ttb_Value.Value = value.ToString();
      this.ttb_Value.Select();
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
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
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.ttb_Value = new SyncroC.Controls.TitledTextBox();
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
      this.btn_OK.Location = new System.Drawing.Point(25, 117);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 1;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // btn_Abort_YN
      // 
      this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_YN.Location = new System.Drawing.Point(189, 117);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_YN.TabIndex = 2;
      this.btn_Abort_YN.Text = "ABORT";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // ttb_Value
      // 
      this.ttb_Value.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Value.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Value.Location = new System.Drawing.Point(35, 25);
      this.ttb_Value.MaxChar = 32767;
      this.ttb_Value.Multiline = false;
      this.ttb_Value.Name = "ttb_Value";
      this.ttb_Value.PasswordChar = '\0';
      this.ttb_Value.ReadOnly = false;
      this.ttb_Value.Size = new System.Drawing.Size(279, 75);
      this.ttb_Value.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Value.TabIndex = 3;
      this.ttb_Value.Text = "Value";
      this.ttb_Value.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Value.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Value.TranslateText = false;
      this.ttb_Value.Value = " ";
      this.ttb_Value.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // Frm_AskString
      // 
      this.AcceptButton = this.btn_OK;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.ttb_Value);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_AskString";
      this.Text = "Frm_AskString";
      this.ResumeLayout(false);

		}
		#endregion

		private void btn_OK_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.data = new object[] { ttb_Value.Value };
			}
			catch (Exception)
			{
				this.data = new object[] { "" };
			}
			this.Dispose();
		}

		private void btn_Abort_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}
	}
}

