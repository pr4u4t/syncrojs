namespace SyncroC.Graph
{
  partial class GraphLines
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
      this.pct_Graph = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Graph)).BeginInit();
      this.SuspendLayout();
      // 
      // pct_Graph
      // 
      this.pct_Graph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pct_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pct_Graph.Location = new System.Drawing.Point(0, 0);
      this.pct_Graph.Name = "pct_Graph";
      this.pct_Graph.Size = new System.Drawing.Size(300, 300);
      this.pct_Graph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Graph.TabIndex = 22;
      this.pct_Graph.TabStop = false;
      // 
      // GraphLines
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pct_Graph);
      this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "GraphLines";
      this.Size = new System.Drawing.Size(300, 300);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Graph)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pct_Graph;
  }
}
