using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
	public class Frm_AskValue : SyncroC.Forms.PopUp.Frm_PopUp
	{
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Abort_YN;
		private SyncroC.Controls.TitledNumericUpDown tnd_Value;
    private SyncroC.Controls.IncDecButton.IncDecButton IncreaseButton;
    private SyncroC.Controls.IncDecButton.IncDecButton DecreaseButton;
		private System.ComponentModel.IContainer components = null;

		public Frm_AskValue()
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
			tnd_Value.Text = text;
			tnd_Value.Value = value.ToString();
			return new FosberDialogResult(this.ShowDialog(owner), this.data);
		}

    //ABELLO_FI30/10/2008_1 Messi i pulsanti Inc/Dec sulle PopUp che chiedono un valore numerico.
    public FosberDialogResult ShowDialog(IWin32Window owner, string text, int value, int minValue, int maxValue)
    {
      this.tnd_Value.Minimum = minValue;
      this.tnd_Value.Maximum = maxValue;
      return ShowDialog(owner, text, value);
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
      this.tnd_Value = new SyncroC.Controls.TitledNumericUpDown();
      this.IncreaseButton = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.DecreaseButton = new SyncroC.Controls.IncDecButton.IncDecButton();
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
      // tnd_Value
      // 
      this.tnd_Value.DecimalPlaces = 0;
      this.tnd_Value.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_Value.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_Value.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_Value.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_Value.Location = new System.Drawing.Point(100, 25);
      this.tnd_Value.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.tnd_Value.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
      this.tnd_Value.Name = "tnd_Value";
      this.tnd_Value.ReadOnly = false;
      this.tnd_Value.Size = new System.Drawing.Size(149, 73);
      this.tnd_Value.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnd_Value.TabIndex = 0;
      this.tnd_Value.Text = "Value";
      this.tnd_Value.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tnd_Value.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_Value.TranslateText = true;
      this.tnd_Value.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_Value.Value = "0";
      this.tnd_Value.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // IncreaseButton
      // 
      this.IncreaseButton.BackColor = System.Drawing.Color.WhiteSmoke;
      this.IncreaseButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.IncreaseButton.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.IncreaseButton.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.UpArrow;
      this.IncreaseButton.Location = new System.Drawing.Point(255, 23);
      this.IncreaseButton.LowLevelEnabled = true;
      this.IncreaseButton.Name = "IncreaseButton";
      this.IncreaseButton.Size = new System.Drawing.Size(69, 75);
      this.IncreaseButton.TabIndex = 3;
      this.IncreaseButton.TimerInterval = 300;
      this.IncreaseButton.TranslateText = false;
      this.IncreaseButton.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.IncreaseButton_OnIncDecButtonDown);
      // 
      // DecreaseButton
      // 
      this.DecreaseButton.BackColor = System.Drawing.Color.WhiteSmoke;
      this.DecreaseButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DecreaseButton.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DecreaseButton.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.DownArrow;
      this.DecreaseButton.Location = new System.Drawing.Point(25, 23);
      this.DecreaseButton.LowLevelEnabled = true;
      this.DecreaseButton.Name = "DecreaseButton";
      this.DecreaseButton.Size = new System.Drawing.Size(69, 75);
      this.DecreaseButton.TabIndex = 4;
      this.DecreaseButton.TimerInterval = 300;
      this.DecreaseButton.TranslateText = false;
      this.DecreaseButton.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.DecreaseButton_OnIncDecButtonDown);
      // 
      // Frm_AskValue
      // 
      this.AcceptButton = this.btn_OK;
      this.BackColor = System.Drawing.Color.LightGray;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.DecreaseButton);
      this.Controls.Add(this.IncreaseButton);
      this.Controls.Add(this.tnd_Value);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_AskValue";
      this.Text = "Frm_AskValue";
      this.ResumeLayout(false);

		}
		#endregion

		private void btn_OK_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.data = new object[] { Convert.ToInt32(tnd_Value.Value) };
			}
			catch (Exception)
			{
				this.data = new object[] { -1 };
			}
			this.Dispose();
		}

		private void btn_Abort_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

    private void DecreaseButton_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {

      if (args.Frequency != -2)
      {
        this.DecreaseButton._signal(true);
        try
        {
          this.tnd_Value.Value = (Convert.ToInt32(this.tnd_Value.Value) - 1).ToString();
        }
        catch (Exception)
        {

        }
      }
      else
      {
        this.DecreaseButton._signal(false);
      }
    }

    private void IncreaseButton_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (args.Frequency != -2)
      {
        this.IncreaseButton._signal(true);
        try
        {
          this.tnd_Value.Value = (Convert.ToInt32(this.tnd_Value.Value) + 1).ToString();
        }
        catch (Exception)
        {

        }
      }
      else
      {
        this.IncreaseButton._signal(false);
      }
    }
	}
}

