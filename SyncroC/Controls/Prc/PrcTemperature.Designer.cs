namespace SyncroC.Controls.Prc
{
  partial class PrcTemperature
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lbl_Temperatura = new System.Windows.Forms.Label();
      this.pct_temperatura = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_temperatura)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
      this.tableLayoutPanel1.Controls.Add(this.lbl_Temperatura, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.pct_temperatura, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(80, 30);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // lbl_Temperatura
      // 
      this.lbl_Temperatura.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl_Temperatura.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Temperatura.Location = new System.Drawing.Point(20, 0);
      this.lbl_Temperatura.Margin = new System.Windows.Forms.Padding(0);
      this.lbl_Temperatura.Name = "lbl_Temperatura";
      this.lbl_Temperatura.Size = new System.Drawing.Size(60, 30);
      this.lbl_Temperatura.TabIndex = 0;
      this.lbl_Temperatura.Text = "999";
      this.lbl_Temperatura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // pct_temperatura
      // 
      this.pct_temperatura.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pct_temperatura.Image = global::SyncroC.Properties.Resources.TEMPERATURA_CARTA;
      this.pct_temperatura.Location = new System.Drawing.Point(0, 0);
      this.pct_temperatura.Margin = new System.Windows.Forms.Padding(0);
      this.pct_temperatura.Name = "pct_temperatura";
      this.pct_temperatura.Size = new System.Drawing.Size(20, 30);
      this.pct_temperatura.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_temperatura.TabIndex = 1;
      this.pct_temperatura.TabStop = false;
      // 
      // PrcTemperature
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "PrcTemperature";
      this.Size = new System.Drawing.Size(80, 30);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_temperatura)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label lbl_Temperatura;
    private System.Windows.Forms.PictureBox pct_temperatura;
  }
}
