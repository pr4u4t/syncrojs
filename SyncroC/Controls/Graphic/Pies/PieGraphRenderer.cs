using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Microsoft.Tools.Graphs.Pies
{
	/// <summary>
	/// Summary description for PieGraphRenderer.
	/// </summary>
	public class PieGraphRenderer
	{
		private double _widthPixels;
		private double _heightPixels;
		private double _drawingAreaWidthPixels;
		private double _drawingAreaHeightPixels;
		private double _totalValue;
		private double _titleHeightPixels;

		private Rectangle _totalRectangle;
		private Rectangle _drawingAreaRectangle;
		private Rectangle _pieGraphRectangle;
		private Rectangle _titleRectangle;
		
		private PieGraph _pieGraph;
		
		private double _maxTitleHeightPixels = 15.0;
		private double _margin = 5.0;
		
		public PieGraphRenderer()
		{
		}

		public Image DrawGraph(PieGraph pieGraph)
		{
			try
			{
				if (pieGraph == null)
					return null;

				_pieGraph = pieGraph;

				Bitmap bMap = new Bitmap(pieGraph.Size.Width, pieGraph.Size.Height, PixelFormat.Format64bppPArgb);
				Graphics g = Graphics.FromImage(bMap);

				g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.Clear(Color.White);
				DrawVerticalBottomGraph(ref g);
				switch (pieGraph.Alignment)
				{
					case Alignment.HorizontalLeft :
						bMap.RotateFlip(RotateFlipType.Rotate270FlipXY);
						break;

					case Alignment.HorizontalRight :
						bMap.RotateFlip(RotateFlipType.Rotate90FlipX);
						break;

					case Alignment.VerticalTop :
						bMap.RotateFlip(RotateFlipType.Rotate180FlipX);
						break;

					default :
						break;
				}
				return bMap;
			}
			catch
			{
				return null;
			}
		}

		private void DrawVerticalBottomGraph(ref Graphics g)
		{
			try
			{
				CalculateValues();
				DrawBackColorGradient(this._pieGraph.Color, this._pieGraph.ColorGradient, ref g);
				DrawPieGraph(ref g);
				DrawTitle(ref g);
				DrawBorder(ref g);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error occured: \n" + ex.ToString(), "Graphing Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CalculateValues()
		{
			this._widthPixels = this._pieGraph.Size.Width;
			this._heightPixels = this._pieGraph.Size.Height;
			this._drawingAreaWidthPixels = this._widthPixels - (2 * this._pieGraph.Border);
			this._drawingAreaHeightPixels = this._heightPixels - (2 * this._pieGraph.Border);
			if (this._pieGraph.Text != null && this._pieGraph.Text != String.Empty)
			{
				this._titleHeightPixels = this._drawingAreaHeightPixels * 0.1;
				if (this._titleHeightPixels > this._maxTitleHeightPixels)
					this._titleHeightPixels = this._maxTitleHeightPixels;

				this._drawingAreaHeightPixels = this._drawingAreaHeightPixels - this._titleHeightPixels;
			}
			this._totalValue = GetTotalValue();
			this._totalRectangle = new Rectangle(0, 0, (int)this._widthPixels, (int)this._heightPixels);
			this._drawingAreaRectangle = new Rectangle((int)this._pieGraph.Border, (int)this._pieGraph.Border + (int)this._titleHeightPixels, (int)this._drawingAreaWidthPixels, (int)this._drawingAreaHeightPixels);
			//graffaelli_FI02/11/2011_3 - Modifica per gestire il caso in cui la width/heigth del rettangolo è < 0
      //this._pieGraphRectangle = new Rectangle((int)(this._pieGraph.Border + this._margin), (int)(this._pieGraph.Border + this._titleHeightPixels + this._margin), (int)(this._drawingAreaWidthPixels - 2 * this._margin), (int)(this._drawingAreaHeightPixels - 2 * this._margin));
      int RectangleWidth = (int) (this._drawingAreaWidthPixels - 2 * this._margin);
      if (RectangleWidth <= 0)
        RectangleWidth = 1;
      int RectangleHeigth = (int)(this._drawingAreaHeightPixels - 2 * this._margin);
      if (RectangleHeigth <= 0)
        RectangleHeigth = 1;
      this._pieGraphRectangle = new Rectangle((int)(this._pieGraph.Border + this._margin), (int)(this._pieGraph.Border + this._titleHeightPixels + this._margin), RectangleWidth, (RectangleHeigth) > 0 ? RectangleHeigth : 1);
      this._titleRectangle = new Rectangle((int)this._pieGraph.Border, (int)this._pieGraph.Border, (int)this._drawingAreaWidthPixels, (int)this._titleHeightPixels);
		}

		private double GetTotalValue()
		{
			if (this._pieGraph.Slices == null)
				return 0;

			if (this._pieGraph.Slices.Count == 0)
				return 0;

			double retVal = 0;

			for (int i = 0; i < this._pieGraph.Slices.Count; i++)
				retVal += this._pieGraph.Slices[i].Value;

			return retVal;
		}

		private void DrawBackColorGradient(Color startColor, Color endColor, ref Graphics g)
		{
			Pen gPen = new Pen(Color.Black, (float)0.03);
			LinearGradientBrush lgBrush = new LinearGradientBrush(this._drawingAreaRectangle, startColor, endColor, (float)(45 * (-1)), true);

			g.FillRectangle(lgBrush, this._drawingAreaRectangle);
			gPen.Color = Color.Black;
			g.DrawRectangle(gPen, this._drawingAreaRectangle);
		}

		private void DrawBorder(ref Graphics g)
		{
			Pen gPen = new Pen(Color.Gray, (float)0.3);

			g.DrawRectangle(gPen, (int)this._totalRectangle.Left, (int)this._totalRectangle.Top, (int)this._widthPixels - 1, (int)this._heightPixels - 1);
		}

		private void DrawTitle(ref Graphics g)
		{
			if (this._pieGraph.Text == null || this._pieGraph.Text == String.Empty)
				return;

			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);

			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Near;

			float fontSize = (float)0.8 * (float)this._titleHeightPixels;

			if (fontSize > (float)10.0)
				fontSize = (float)10.0;

			if (fontSize < (float)1.0)
				fontSize = (float)1.0;

			SolidBrush gBrush = new SolidBrush(Color.Black);

			//Draw Title
			Point Middle = new Point((int)MappedXCoordinatePixels(this._drawingAreaWidthPixels / 2), (int)this._titleHeightPixels / 2);

			g.TranslateTransform(Middle.X, Middle.Y);
			g.DrawString(this._pieGraph.Text, new Font("Tahoma", fontSize, FontStyle.Bold), gBrush, 0, 0, format);
			g.TranslateTransform(-Middle.X, -Middle.Y);
		}

		// The return value is to be used as is. It is calculated w.r.t. _drawingAreaRectangle.Left
		private double MappedXCoordinatePixels(double value)
		{
			return this._drawingAreaRectangle.Left + value;
		}
		// The return value is to be used as is. It is calculated w.r.t. center of _pieGraphRectangle
		private double MappedXCoordinateCenterPixels(double value)
		{
			return (this._pieGraphRectangle.Left + this._pieGraphRectangle.Right) / 2 + value;
		}
		
		// The return value is to be used as is. It is calculated w.r.t. center of _pieGraphRectangle
		private double MappedYCoordinateCenterPixels(double value)
		{
			return (this._pieGraphRectangle.Top + this._pieGraphRectangle.Bottom) / 2 + value;
		}

		private float Round(float num, int precision)
		{
			float pow = 1;
			for(int i=0; i < precision; i++)
				pow *= 10;
			if(num > 0)
				return (int)(num * pow + .5) / pow;
			return (int)(num * pow - .5) / pow;
		}

		private void DrawPieGraph(ref Graphics g)
		{
			if (this._pieGraph.Slices == null)
				return;

			if (this._pieGraph.Slices.Count == 0)
				return;

			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);

			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Near;

			float fontSize = (float)0.51 * (float)this._titleHeightPixels;

      //ABELLO_FI26/11/2010_2 sui grafici a torta del client le scritte erano un pò piccole

      //if (fontSize > (float)10.0)
      //  fontSize = (float)10.0;

			if (fontSize < (float)12.0)
				fontSize = (float)12.0;

			Pen gPen = new Pen(Color.Black, (float)0.03);
			double startAngle = 0;

			if (this._pieGraph.Slices.Count == 1)
			{
				SolidBrush gBrush = new SolidBrush(this._pieGraph.Slices[0].Color);
				format.LineAlignment = StringAlignment.Center;

				// Fill Pie Slice
				g.FillEllipse(gBrush, this._pieGraphRectangle);
				g.DrawEllipse(gPen, this._pieGraphRectangle);

				// Write Text
				gBrush = new SolidBrush(Color.White);
				string text = this._pieGraph.Slices[0].Value.ToString() + "\n(100%)"; double x = MappedXCoordinateCenterPixels(0);
				double y = MappedYCoordinateCenterPixels(0);
				Point Middle = new Point((int)x, (int)y);
				g.TranslateTransform(Middle.X, Middle.Y);
				g.DrawString(text, new Font("Tahoma", fontSize, FontStyle.Bold), gBrush, 0, 0, format);
				g.TranslateTransform(-Middle.X, -Middle.Y);
				return;
			}

			for (int i = 0; i < this._pieGraph.Slices.Count; i++)
			{
				double sweepAngle = this._pieGraph.Slices[i].Value * 360 / this._totalValue;
				SolidBrush gBrush = new SolidBrush(this._pieGraph.Slices[i].Color);

				// Fill Pie Slice
				g.FillPie(gBrush, this._pieGraphRectangle, (float)startAngle, (float)sweepAngle); 
				g.DrawPie(gPen, this._pieGraphRectangle, (float)startAngle, (float)sweepAngle);

				// Write Text in Pie Slice
				// x = a * cos(theta)
				// y = b * sin (theta)
				gBrush = new SolidBrush(Color.White);
				double angle = startAngle + sweepAngle / 2;
				double x;
				double y;

				if (sweepAngle <= 20.0)
				{
					x = MappedXCoordinateCenterPixels((_pieGraphRectangle.Width * 0.8 / 2) * System.Math.Cos(angle * System.Math.PI / 180));
					y = MappedYCoordinateCenterPixels((_pieGraphRectangle.Height * 0.8 / 2) * System.Math.Sin(angle * System.Math.PI / 180));
				}
				else
				{
					x = MappedXCoordinateCenterPixels((_pieGraphRectangle.Width * 0.6 / 2) * System.Math.Cos(angle * System.Math.PI / 180));
					y = MappedYCoordinateCenterPixels((_pieGraphRectangle.Height * 0.6 / 2) * System.Math.Sin(angle * System.Math.PI / 180));
				}

				Point Middle = new Point((int)x, (int)y);
				g.TranslateTransform(Middle.X, Middle.Y);

				float percent = Round((float)(this._pieGraph.Slices[i].Value * 100 / this._totalValue), 1);
				string text = String.Empty;

				if(sweepAngle<=20.0)
					text = this._pieGraph.Slices[i].Value.ToString() + " (" + percent.ToString() + "%)"; 
				else
					text = this._pieGraph.Slices[i].Value.ToString() + "\n(" + percent.ToString() + "%)";
				g.DrawString(text, new Font("Tahoma", fontSize, FontStyle.Bold), gBrush, 0, 0, format);
				g.TranslateTransform(-Middle.X, -Middle.Y);

				startAngle += sweepAngle;
			}

		}

	}// class
}// namespace
