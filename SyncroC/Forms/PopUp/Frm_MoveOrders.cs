using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
  public class Frm_MoveOrders : SyncroC.Forms.PopUp.Frm_PopUp
  {
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Button btn_Abort_YN;
    private SyncroC.Controls.TitledNumericUpDown tnd_MoveFrom;
    private SyncroC.Controls.TitledNumericUpDown tnd_MoveTo;
    private SyncroC.Controls.TitledNumericUpDown tnd_Destination;
    private System.ComponentModel.IContainer components = null;

    /// <summary>Constant used as default destination position for moving orders.</summary>
    private int DEFAULT_DESTINATION = 5;

    public Frm_MoveOrders()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="firstOrderToMovePos">Position of first order of the group to move</param>
    /// <param name="lastOrderToMovePos">Position of last order of the group to move</param>
    public FosberDialogResult ShowDialog(IWin32Window owner, int firstOrderToMovePos, int lastOrderToMovePos)
    {
      tnd_MoveFrom.Value = firstOrderToMovePos.ToString();
      tnd_MoveTo.Value = lastOrderToMovePos.ToString();
      tnd_Destination.Value = DEFAULT_DESTINATION.ToString();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MoveOrders));
      this.btn_OK = new System.Windows.Forms.Button();
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.tnd_MoveFrom = new SyncroC.Controls.TitledNumericUpDown();
      this.tnd_MoveTo = new SyncroC.Controls.TitledNumericUpDown();
      this.tnd_Destination = new SyncroC.Controls.TitledNumericUpDown();
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
      this.btn_OK.Location = new System.Drawing.Point(25, 129);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 45);
      this.btn_OK.TabIndex = 3;
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
      this.btn_Abort_YN.Location = new System.Drawing.Point(189, 129);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 45);
      this.btn_Abort_YN.TabIndex = 4;
      this.btn_Abort_YN.Text = "ABORT";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // tnd_MoveFrom
      // 
      this.tnd_MoveFrom.DecimalPlaces = 0;
      this.tnd_MoveFrom.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_MoveFrom.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_MoveFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_MoveFrom.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_MoveFrom.Location = new System.Drawing.Point(25, 25);
      this.tnd_MoveFrom.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnd_MoveFrom.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnd_MoveFrom.Name = "tnd_MoveFrom";
      this.tnd_MoveFrom.ReadOnly = false;
      this.tnd_MoveFrom.Size = new System.Drawing.Size(140, 48);
      this.tnd_MoveFrom.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnd_MoveFrom.TabIndex = 0;
      this.tnd_MoveFrom.Text = "From";
      this.tnd_MoveFrom.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tnd_MoveFrom.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_MoveFrom.TranslateText = true;
      this.tnd_MoveFrom.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_MoveFrom.Value = "0";
      this.tnd_MoveFrom.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // tnd_MoveTo
      // 
      this.tnd_MoveTo.DecimalPlaces = 0;
      this.tnd_MoveTo.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_MoveTo.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_MoveTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_MoveTo.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_MoveTo.Location = new System.Drawing.Point(182, 25);
      this.tnd_MoveTo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnd_MoveTo.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnd_MoveTo.Name = "tnd_MoveTo";
      this.tnd_MoveTo.ReadOnly = false;
      this.tnd_MoveTo.Size = new System.Drawing.Size(140, 48);
      this.tnd_MoveTo.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnd_MoveTo.TabIndex = 1;
      this.tnd_MoveTo.Text = "To";
      this.tnd_MoveTo.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tnd_MoveTo.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_MoveTo.TranslateText = true;
      this.tnd_MoveTo.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_MoveTo.Value = "0";
      this.tnd_MoveTo.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // tnd_Destination
      // 
      this.tnd_Destination.DecimalPlaces = 0;
      this.tnd_Destination.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_Destination.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_Destination.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_Destination.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_Destination.Location = new System.Drawing.Point(25, 76);
      this.tnd_Destination.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnd_Destination.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnd_Destination.Name = "tnd_Destination";
      this.tnd_Destination.ReadOnly = false;
      this.tnd_Destination.Size = new System.Drawing.Size(140, 48);
      this.tnd_Destination.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnd_Destination.TabIndex = 2;
      this.tnd_Destination.Text = "Destination";
      this.tnd_Destination.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tnd_Destination.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_Destination.TranslateText = true;
      this.tnd_Destination.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_Destination.Value = "0";
      this.tnd_Destination.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // Frm_MoveOrders
      // 
      this.AcceptButton = this.btn_OK;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.tnd_Destination);
      this.Controls.Add(this.tnd_MoveTo);
      this.Controls.Add(this.tnd_MoveFrom);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_MoveOrders";
      this.Text = "Frm_MoveOrders";
      this.ResumeLayout(false);

    }
    #endregion

    private void btn_OK_Click(object sender, System.EventArgs e)
    {
      try
      {
        this.data = new object[] { Convert.ToInt32(tnd_MoveFrom.Value), Convert.ToInt32(tnd_MoveTo.Value), Convert.ToInt32(tnd_Destination.Value) };
      }
      catch (Exception)
      {
        this.data = new object[] { -1, -1, -1 };
      }
      this.Dispose();
    }

    private void btn_Abort_Click(object sender, System.EventArgs e)
    {
      this.Dispose();
    }
  }
}
