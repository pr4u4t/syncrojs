using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Controls
{
	public partial class Roll : SyncroC.Controls.SyncroUserControl
	{
		private int SX_IMAGES_OFFSET = 6;
		private int rotatingIndexOffset = 0;
		private int ROTATING_IMAGES = 4;

		private bool sx = true;
		public bool SX
		{
			get { return sx; }
			set 
			{ 
				sx = value;
				selectImage();
			}
		}

		private bool ready = false;
		public bool Ready
		{
			get { return ready; }
			set 
			{ 
				ready = value;
				selectImage();
			}
		}

		private bool active = false;
		public bool Active
		{
			get { return active; }
			set 
			{ 
				active = value;
				selectImage();
			}
		}

		private bool rotating = false;
		public bool Rotating
		{
			get { return rotating; }
			set 
			{ 
				rotating = value;
				selectImage();
			}
		}

		private bool corrugatorDirectionLeft = false;
		public bool CorrugatorDirectionLeft
		{
			get { return corrugatorDirectionLeft; }
			set { corrugatorDirectionLeft = value; }
		}

		public Roll()
		{
			InitializeComponent();
		}

		private void selectImage()
		{
			int imageIndex = -1;

			//image selection
			if (Ready && !Active)
			{
				imageIndex = 0;
				if (tmr_rotating.Enabled)
					tmr_rotating.Stop();
			}
			if (Active && !Rotating)
			{
				imageIndex = 1;
				if (tmr_rotating.Enabled)
					tmr_rotating.Stop();
			}
			if (Active && Rotating)
			{
				imageIndex = 2;
				if (! tmr_rotating.Enabled)
					tmr_rotating.Start();
			}

			//rotating simulation
			if (imageIndex == 2)
				imageIndex += rotatingIndexOffset;

			//roll side selection
			if ((imageIndex != -1) && (!sx))
				imageIndex += SX_IMAGES_OFFSET;

			//roll image show
			if ((imageIndex >= 0) && (imageIndex < imageList.Images.Count))
			{
				pct_Image.Image = imageList.Images[imageIndex];
				if(this.CorrugatorDirectionLeft)
					pct_Image.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
				this.Visible = true;
			}
			else
			{
				this.Visible = false;
			}
		}

		private void tmr_rotating_Tick(object sender, EventArgs e)
		{
			selectImage();
			rotatingIndexOffset++;
			if (rotatingIndexOffset >= ROTATING_IMAGES)
				rotatingIndexOffset = 0;
		}

		private void Roll_SizeChanged(object sender, EventArgs e)
		{
			this.pct_Image.Size = this.Size;
		}
	}
}
