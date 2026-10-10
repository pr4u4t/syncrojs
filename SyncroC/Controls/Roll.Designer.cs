namespace SyncroC.Controls
{
	partial class Roll
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Roll));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tmr_rotating = new System.Windows.Forms.Timer(this.components);
			this.pct_Image = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pct_Image)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.WhiteSmoke;
			this.imageList.Images.SetKeyName(0, "READY_ROLL_DX.JPG");
			this.imageList.Images.SetKeyName(1, "ACTIVE_ROLL_DX.JPG");
			this.imageList.Images.SetKeyName(2, "ROTATING_ROLL_DX_1.JPG");
			this.imageList.Images.SetKeyName(3, "ROTATING_ROLL_DX_2.JPG");
			this.imageList.Images.SetKeyName(4, "ROTATING_ROLL_DX_3.JPG");
			this.imageList.Images.SetKeyName(5, "ROTATING_ROLL_DX_4.JPG");
			this.imageList.Images.SetKeyName(6, "READY_ROLL_SX.JPG");
			this.imageList.Images.SetKeyName(7, "ACTIVE_ROLL_SX.JPG");
			this.imageList.Images.SetKeyName(8, "ROTATING_ROLL_SX_1.JPG");
			this.imageList.Images.SetKeyName(9, "ROTATING_ROLL_SX_2.JPG");
			this.imageList.Images.SetKeyName(10, "ROTATING_ROLL_SX_3.jpg");
			this.imageList.Images.SetKeyName(11, "ROTATING_ROLL_SX_4.jpg");
			// 
			// tmr_rotating
			// 
			this.tmr_rotating.Interval = 500;
			this.tmr_rotating.Tick += new System.EventHandler(this.tmr_rotating_Tick);
			// 
			// pct_Image
			// 
			this.pct_Image.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pct_Image.Location = new System.Drawing.Point(0, 0);
			this.pct_Image.Name = "pct_Image";
			this.pct_Image.Size = new System.Drawing.Size(254, 252);
			this.pct_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pct_Image.TabIndex = 0;
			this.pct_Image.TabStop = false;
			// 
			// Roll
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.pct_Image);
			this.Name = "Roll";
			this.Size = new System.Drawing.Size(254, 252);
			this.SizeChanged += new System.EventHandler(this.Roll_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pct_Image)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pct_Image;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Timer tmr_rotating;
	}
}
