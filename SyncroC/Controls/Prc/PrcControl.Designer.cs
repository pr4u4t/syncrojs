namespace SyncroC.Controls.Prc
{
  partial class PrcControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lbl_setpoint = new System.Windows.Forms.Label();
      this.lbl_WarpCorrection = new System.Windows.Forms.Label();
      this.lbl_UserOffset = new System.Windows.Forms.Label();
      this.btn_Graph = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lbl_setpoint
      // 
      this.lbl_setpoint.BackColor = System.Drawing.Color.Black;
      this.lbl_setpoint.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_setpoint.ForeColor = System.Drawing.Color.White;
      this.lbl_setpoint.Location = new System.Drawing.Point(132, 75);
      this.lbl_setpoint.Name = "lbl_setpoint";
      this.lbl_setpoint.Size = new System.Drawing.Size(40, 23);
      this.lbl_setpoint.TabIndex = 470;
      this.lbl_setpoint.Text = "0";
      this.lbl_setpoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_setpoint.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_setpoint_Paint);
      this.lbl_setpoint.Click += new System.EventHandler(this.lbl_LowLevelControl_Click);
      // 
      // lbl_WarpCorrection
      // 
      this.lbl_WarpCorrection.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.lbl_WarpCorrection.BackColor = System.Drawing.Color.Gold;
      this.lbl_WarpCorrection.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_WarpCorrection.Location = new System.Drawing.Point(132, 17);
      this.lbl_WarpCorrection.Name = "lbl_WarpCorrection";
      this.lbl_WarpCorrection.Size = new System.Drawing.Size(40, 16);
      this.lbl_WarpCorrection.TabIndex = 472;
      this.lbl_WarpCorrection.Text = "0";
      this.lbl_WarpCorrection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_WarpCorrection.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_N_Paint);
      this.lbl_WarpCorrection.Click += new System.EventHandler(this.lbl_LowLevelControl_Click);
      // 
      // lbl_UserOffset
      // 
      this.lbl_UserOffset.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.lbl_UserOffset.BackColor = System.Drawing.Color.Lime;
      this.lbl_UserOffset.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_UserOffset.Location = new System.Drawing.Point(132, 40);
      this.lbl_UserOffset.Name = "lbl_UserOffset";
      this.lbl_UserOffset.Size = new System.Drawing.Size(40, 16);
      this.lbl_UserOffset.TabIndex = 471;
      this.lbl_UserOffset.Text = "0";
      this.lbl_UserOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_UserOffset.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_N_Paint);
      this.lbl_UserOffset.Click += new System.EventHandler(this.lbl_LowLevelControl_Click);
      // 
      // btn_Graph
      // 
      this.btn_Graph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Graph.BackColor = System.Drawing.Color.White;
      this.btn_Graph.BackgroundImage = global::SyncroC.Properties.Resources.Graph;
      this.btn_Graph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btn_Graph.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Graph.Location = new System.Drawing.Point(136, 102);
      this.btn_Graph.Name = "btn_Graph";
      this.btn_Graph.Size = new System.Drawing.Size(36, 30);
      this.btn_Graph.TabIndex = 473;
      this.btn_Graph.UseVisualStyleBackColor = false;
      this.btn_Graph.Click += new System.EventHandler(this.btn_Graph_Click);
      // 
      // PrcControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btn_Graph);
      this.Controls.Add(this.lbl_WarpCorrection);
      this.Controls.Add(this.lbl_UserOffset);
      this.Controls.Add(this.lbl_setpoint);
      this.Name = "PrcControl";
      this.Size = new System.Drawing.Size(175, 135);
      this.Click += new System.EventHandler(this.lbl_LowLevelControl_Click);
      this.SizeChanged += new System.EventHandler(this.PrcControl_SizeChanged);
      this.Controls.SetChildIndex(this.lbl_setpoint, 0);
      this.Controls.SetChildIndex(this.lbl_UserOffset, 0);
      this.Controls.SetChildIndex(this.lbl_WarpCorrection, 0);
      this.Controls.SetChildIndex(this.btn_Graph, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lbl_setpoint;
    private System.Windows.Forms.Label lbl_WarpCorrection;
    private System.Windows.Forms.Label lbl_UserOffset;
    private System.Windows.Forms.Button btn_Graph;
  }
}
