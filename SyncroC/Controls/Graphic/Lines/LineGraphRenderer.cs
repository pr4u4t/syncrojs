using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Microsoft.Tools.Graphs.Lines
{
	/// <summary>
	/// Summary description for LineGraphRenderer.
	/// </summary>
	public class LineGraphRenderer
	{
		private double _widthPixels;
		private double _heightPixels;
		private double _drawingAreaWidthPixels;
		private double _drawingAreaHeightPixels;
		private double _minimumYValue;
		private double _maximumYValue;
		private double _totalYValue;
		private double _positiveHeightPixels;	// In Drawing Area
		private double _negativeHeightPixels;	// In Drawing Area
		private double _gridHeightValue;
		private double _gridHeightPixels;
		private double _titleHeightPixels;
		private double _totalXAxisIntervals;
		private double _maxXValue;
		private double _xAxisIntervalValue;

		private Rectangle _totalRectangle;
		private Rectangle _drawingAreaRectangle;
		private Rectangle _titleRectangle;

		private LineGraph _lineGraph;

		private int _marginForTextOnXAxis;
		private double _marginForPhaseLinesText;
		private int _marginForNumbersOnYAxis = 15;
		private double _maxTitleHeightPixels = 15.0;

		public LineGraphRenderer()
		{
		}

		public Image DrawGraph(LineGraph lineGraph)
		{
			try
			{
				if (lineGraph == null)
					return null;

				_lineGraph = lineGraph;

				Bitmap bMap = new Bitmap(lineGraph.Size.Width, lineGraph.Size.Height, PixelFormat.Format64bppPArgb);
				Graphics g = Graphics.FromImage(bMap);

				g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.Clear(Color.White);
				DrawVerticalBottomGraph(ref g);
				switch (lineGraph.Alignment)
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
				//DrawBackColor(ref g);
				DrawBackColorGradient(this._lineGraph.Color, this._lineGraph.ColorGradient, ref g);
				//DrawCustomBackColorGradient(Color.White, Color.DarkGray, ref g);
				DrawXAxisText(ref g);
				DrawPhaseLines(ref g);
				DrawGrid(ref g);
				for (int i = 0; i < this._lineGraph.Lines.Count; i++)
					DrawLineGraph(this._lineGraph.Lines[i], ref g);
				DrawTrendLine(ref g);
				DrawXAxisSectionMarks(ref g);
				DrawTitle(ref g);
				DrawBorder(ref g);
        DrawHighlightPoint(ref g);
        DrawYAxisText(ref g);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error occured: \n" + ex.ToString(), "Graphing Message", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
		}

		private void CalculateValues()
		{
			this._marginForTextOnXAxis = this._lineGraph.MarginForTextOnAxis;

			this._widthPixels = this._lineGraph.Size.Width;
			this._heightPixels = this._lineGraph.Size.Height;
			
			this._drawingAreaWidthPixels = this._widthPixels - (2 * this._lineGraph.Border) - this._marginForNumbersOnYAxis;
			this._drawingAreaHeightPixels = this._heightPixels - (2 * this._lineGraph.Border) - this._marginForTextOnXAxis;
			if (this._lineGraph.Text != null && this._lineGraph.Text != String.Empty)
			{
				this._titleHeightPixels = this._drawingAreaHeightPixels * 0.1;
				if(this._titleHeightPixels>this._maxTitleHeightPixels)
					this._titleHeightPixels = this._maxTitleHeightPixels;
				this._drawingAreaHeightPixels = this._drawingAreaHeightPixels - this._titleHeightPixels;
			}

			if (this._lineGraph.PhaseLines != null && this._lineGraph.PhaseLines.Count > 0)
			{
				this._marginForPhaseLinesText = this._drawingAreaHeightPixels * 0.05;
				if (this._marginForPhaseLinesText > 10)
					this._marginForPhaseLinesText = 10;
				this._drawingAreaHeightPixels = this._drawingAreaHeightPixels - this._marginForPhaseLinesText;
			}

			this._minimumYValue = GetMinimumYValue();
			this._maximumYValue = GetMaximumYValue();
			this._totalYValue = GetTotalYValue();
			
			this._positiveHeightPixels = GetPositiveHeightPixels();
			this._negativeHeightPixels = GetNegativeHeightPixels();

			this._gridHeightValue = GetGridHeightValue();
			this._gridHeightPixels = GetGridHeightPixels();
			
			this._totalXAxisIntervals = this._lineGraph.TotalXAxisIntervals;
			this._maxXValue = GetMaxXValue();
			this._xAxisIntervalValue = this._lineGraph.XAxisIntervalValue;

			this._totalRectangle = new Rectangle(0, 0, (int)this._widthPixels, (int)this._heightPixels);
			this._drawingAreaRectangle = new Rectangle((int)this._lineGraph.Border + this._marginForNumbersOnYAxis, (int)this._lineGraph.Border + (int)this._titleHeightPixels + (int)this._marginForPhaseLinesText, (int)this._drawingAreaWidthPixels, (int)this._drawingAreaHeightPixels);
			this._titleRectangle = new Rectangle((int)this._lineGraph.Border, (int)this._lineGraph.Border, (int)this._drawingAreaWidthPixels, (int)this._titleHeightPixels);
		}

		private double GetMinimumYValue()
		{
			double minValue = Double.MaxValue;
			for (int i = 0; i < this._lineGraph.Lines.Count; i++)
			{
				Line line = this._lineGraph.Lines[i];
				for (int j = 0; j < line.Points.Count; j++)
				{
					LinePoint p = line.Points[j];
					if(p.YValue<minValue)
						minValue = p.YValue;
				}
			}

			if ((this._lineGraph.TrendLine != null) && (this._lineGraph.TrendLine.Points!=null) && (this._lineGraph.TrendLine.Points.Count > 0))
			{
				for (int j = 0; j < this._lineGraph.TrendLine.Points.Count; j++)
				{
					LinePoint p = this._lineGraph.TrendLine.Points[j];

					if (p.YValue < minValue)
						minValue = p.YValue;
				}
			}

			if (minValue > 0.0)
				minValue = 0.0;

			if (this._lineGraph.RoundOffGridHeight)
				if (minValue < 0.0 && minValue > -10.0)
					minValue = -10.0;

			return minValue;
		}

		private double GetMaximumYValue()
		{
			double maxValue = Double.MinValue;
			for (int i = 0; i < this._lineGraph.Lines.Count; i++)
			{
				Line line = this._lineGraph.Lines[i];
				for (int j = 0; j < line.Points.Count; j++)
				{
					LinePoint p = line.Points[j];
					if (p.YValue > maxValue)
						maxValue = p.YValue;
				}
			}

			if ((this._lineGraph.TrendLine != null) && (this._lineGraph.TrendLine.Points != null) && (this._lineGraph.TrendLine.Points.Count > 0))
			{
				for (int j = 0; j < this._lineGraph.TrendLine.Points.Count; j++)
				{
					LinePoint p = this._lineGraph.TrendLine.Points[j];

					if (p.YValue > maxValue)
						maxValue = p.YValue;
				}
			}

			if (this._lineGraph.RoundOffGridHeight)
				if (maxValue < 10.0)
					maxValue = 10.0;

			if(this._lineGraph.HonorScale && maxValue < this._lineGraph.MaxScaleValue)
				return this._lineGraph.MaxScaleValue;
			else
				return maxValue;
		}

		private double GetTotalYValue()
		{
			double retVal = this._maximumYValue - this._minimumYValue;
			if(retVal < 0.0)
				retVal = retVal * (-1.0);

			return retVal;
		}

		private double GetMinimumXValue()
		{
			double minValue = Double.MaxValue;

			for (int i = 0; i < this._lineGraph.Lines.Count; i++)
			{
				Line line = this._lineGraph.Lines[i];

				for (int j = 0; j < line.Points.Count; j++)
				{
					LinePoint p = line.Points[j];

					if (p.XValue < minValue)
						minValue = p.XValue;
				}
			}

			return minValue;
		}

		private double GetMaximumXValue()
		{
			double maxValue = Double.MinValue;

			for (int i = 0; i < this._lineGraph.Lines.Count; i++)
			{
				Line line = this._lineGraph.Lines[i];

				for (int j = 0; j < line.Points.Count; j++)
				{
					LinePoint p = line.Points[j];

					if (p.XValue > maxValue)
						maxValue = p.XValue;
				}
			}

			return maxValue;
		}

		private double GetPositiveHeightPixels()
		{
			if(this._minimumYValue>=0.0 && this._maximumYValue>=0)
				return this._drawingAreaHeightPixels;

			if (this._minimumYValue <= 0.0 && this._maximumYValue <= 0)
				return 0.0;

			if (this._minimumYValue > this._maximumYValue)
				throw new Exception("Error: Minimum value is more than Maximum value");

			return (this._drawingAreaHeightPixels * this._maximumYValue) / this._totalYValue;
		}

		private double GetNegativeHeightPixels()
		{
			if (this._minimumYValue >= 0.0 && this._maximumYValue >= 0)
				return 0.0;

			if (this._minimumYValue <= 0.0 && this._maximumYValue <= 0)
				return this._drawingAreaHeightPixels;

			if (this._minimumYValue > this._maximumYValue)
				throw new Exception("Error: Minimum value is more than Maximum value");

			double retVal = (this._drawingAreaHeightPixels * this._minimumYValue) / this._totalYValue;
			if(retVal < 0.0)
				retVal = retVal * (-1);
			return retVal;

		}

		private double GetGridHeightValue()
		{
			if (this._lineGraph.GridSpacingValue != 0)
				return this._lineGraph.GridSpacingValue;

			double retVal;
			double max = this._maximumYValue;
			double min = this._minimumYValue;

			if (this._maximumYValue < 0.0)
				max = this._maximumYValue * (-1);

			if (this._minimumYValue < 0.0)
				min = this._minimumYValue * (-1);

			if (max > min)
				retVal = max / 10;
			else
				retVal = min / 10;

			if (this._lineGraph.RoundOffGridHeight)
				retVal = Math.Ceiling(retVal);

			if (retVal >= 3.0)
			{
				double temp = retVal % 5;
				retVal = retVal + (5.0 - temp);
			}

			return retVal;
		}

		private double GetGridHeightPixels()
		{
			return (this._gridHeightValue * this._drawingAreaHeightPixels) / this._totalYValue;
		}

		// The return value is to be used as is. It is calculated w.r.t. _drawingAreaRectangle.Left
		private double MappedXCoordinatePixels(double value)
		{
			return this._drawingAreaRectangle.Left + value;
		}

		// The return value is to be used as is. It is calculated w.r.t. _drawingAreaRectangle.Left
		private double MappedXCoordinateValue(double value)
		{
			if (value < 0)
				return this._drawingAreaRectangle.Left;

			double retVal = this._drawingAreaRectangle.Left + (this._drawingAreaWidthPixels * value / this._maxXValue);

			if (retVal > this._drawingAreaRectangle.Right)
				retVal = this._drawingAreaRectangle.Right;

			return retVal;
		}

		// The return value is to be used as is. It is calculated w.r.t. _drawingAreaRectangle.Top
		private double MappedYCoordinateValue(double value) 
		{
			if(value==0.0)
				return this._drawingAreaRectangle.Top + this._positiveHeightPixels;
			
			double retVal;

			if (value > 0.0) // value is positive
			{
				if (this._minimumYValue >= 0.0 && this._maximumYValue >= 0) // both positive
				{
					double diff = this._totalYValue;
					retVal = (value * this._drawingAreaHeightPixels) / diff;	
					retVal = this._drawingAreaHeightPixels - retVal;
				}
				else if (this._minimumYValue <= 0.0 && this._maximumYValue <= 0) // both negative
				{
					throw new Exception("Error: Value is positive while both Max and Min values are negative");
				}
				else if (this._minimumYValue > this._maximumYValue) // error
				{
					throw new Exception("Error: Minimum value is more than Maximum value");
				}
				else // Max positive and Min negative
				{
					double diff = this._totalYValue;
					retVal = (value * this._drawingAreaHeightPixels) / diff;
					retVal = this._positiveHeightPixels - retVal;
				}
			}
			else // value is negative
			{
				value = value * (-1);
				if (this._minimumYValue >= 0.0 && this._maximumYValue >= 0) // both positive
				{
					throw new Exception("Error: Value is negative while both Max and Mix values are positive");
				}
				else if (this._minimumYValue <= 0.0 && this._maximumYValue <= 0) // both negative
				{
					double diff = this._totalYValue;
					retVal = (value * this._drawingAreaHeightPixels) / diff;
				}
				else if (this._minimumYValue > this._maximumYValue) // error
				{
					throw new Exception("Error: Minimum value is more than Maximum value");
				}
				else // Max positive and Min negative
				{
					double diff = this._totalYValue;
					retVal = (value * this._drawingAreaHeightPixels) / diff;
					retVal = this._positiveHeightPixels + retVal;
				}
			}

			return _drawingAreaRectangle.Top + retVal;
		}

		private void DrawLineGraph(Line line, ref Graphics g)
		{
			Pen gPen = new Pen(line.Color, line.Width);
			DrawLineGraph(line, gPen, ref g);
		}

		private void DrawLineGraph(Line line, Pen gPen, ref Graphics g)
		{
			if (line.Points.Count == 0)
				return;

			double pointSpacing = this._drawingAreaWidthPixels / (line.Points.Count - 1);
			double oldX = MappedXCoordinateValue(line.Points[0].XValue);
			double oldY = MappedYCoordinateValue(line.Points[0].YValue);
			double newX = 0;
			double newY = 0;

      //ABELLO
      if ((line.Points[0].Text != null) && (line.Points[0].Text.Trim() != String.Empty))
      {
        double wX = oldX;
        double wY = oldY;

        if (line.Points[0].XValue == this._maxXValue)
          wX = oldX + 5;

        if (line.Points[0].YValue == this._minimumYValue)
          wY = oldY - new Font("Tahoma", 10, FontStyle.Regular).Height - 5;

        g.DrawString(line.Points[0].Text, new Font("Tahoma", 10, FontStyle.Regular), Brushes.Black, (float)wX, (float)wY);
      }

			for (int i = 1; i < line.Points.Count; i++)
			{
				newX = MappedXCoordinateValue(line.Points[i].XValue);
				newY = MappedYCoordinateValue(line.Points[i].YValue);
				g.DrawLine(gPen, (float)oldX, (float)oldY, (float)newX, (float)newY);

        //ABELLO
        if (i != (line.Points.Count - 1))
        {
          if ((line.Points[i].Text != null) && (line.Points[i].Text.Trim() != String.Empty))
          {
            double wX = newX;
            double wY = newY;

            if (line.Points[i].XValue == this._maxXValue)
              wX = newX - 5;

            if (line.Points[i].YValue == this._minimumYValue)
              wY = newY - new Font("Tahoma", 10, FontStyle.Regular).Height -5;

            g.DrawString(line.Points[i].Text, new Font("Tahoma", 10, FontStyle.Regular), Brushes.Black, (float)wX, (float)wY);
          }
        }

				oldX = newX;
				oldY = newY;
			}

			// Draw Projected Line
			if (this._lineGraph.ShowProjectedTrend && gPen.DashStyle == DashStyle.Solid) // Means Line is not a Trend Line
			{
				Pen gDashPen = new Pen(gPen.Color, gPen.Width);
				gDashPen.DashStyle = DashStyle.DashDot;
				gDashPen.DashCap = DashCap.Triangle;

				double deltaX = line.Points[line.Points.Count - 1].XValue - line.Points[0].XValue;
				double deltaY = line.Points[line.Points.Count - 1].YValue - line.Points[0].YValue;
				double progress = deltaY / deltaX;
				double deltaProjectedProgress = (this._maxXValue - deltaX) * progress;
				double finalYValue = progress + deltaProjectedProgress;

				newX = MappedXCoordinateValue(this._maxXValue);
				newY = MappedYCoordinateValue(finalYValue);

				// Guard against the boundaries
				double m = (newY - oldY) / (newX - oldX); // (y2-y1)/(x2-x1)

				if (newY < this._drawingAreaRectangle.Top)
				{
					newY = this._drawingAreaRectangle.Top;
					newX = (this._drawingAreaRectangle.Top - oldY) / m + oldX;
				}
				else if(newY > this._drawingAreaRectangle.Bottom)
				{
					newY = this._drawingAreaRectangle.Bottom;
					newX = (this._drawingAreaRectangle.Bottom - oldY) / m + oldX;
				}

				g.DrawLine(gDashPen, (float)oldX, (float)oldY, (float)newX, (float)newY);
			}
		}


		private void DrawGrid(ref Graphics g)
		{
			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);

			format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Center;

			float fontSize = (float)0.5 * (float)this._gridHeightPixels;

			if (fontSize > (float)10.0)
				fontSize = (float)10.0;

			if (fontSize < (float)1.0)
				fontSize = (float)1.0;

			SolidBrush gBrush = new SolidBrush(Color.Black);

			// Center line
			Pen gPen = new Pen(Color.Black, (float)2.0F);
			if (this._minimumYValue <= 0.0 && this._maximumYValue >= 0.0)
			{
				g.DrawLine(gPen, (float)MappedXCoordinatePixels(0.0), (float)MappedYCoordinateValue(0.0), (float)MappedXCoordinatePixels(this._drawingAreaWidthPixels), (float)MappedYCoordinateValue(0.0));
				g.DrawString("0", new Font("Tahoma", fontSize), gBrush, (float)MappedXCoordinatePixels(-2), (float)MappedYCoordinateValue(0), format);
			}
			
			if(!this._lineGraph.ShowGrid)
				return;

			// +ve grid lines
			gPen = new Pen(Color.Gray, (float)0.03);
			int gridCount = (int)(this._positiveHeightPixels / this._gridHeightPixels);
			if(gridCount>0)
				gridCount++;
			for (int i = 1; i <= gridCount; i++)
			{
				if(MappedYCoordinateValue(this._gridHeightValue * i)>this._drawingAreaRectangle.Bottom || MappedYCoordinateValue(this._gridHeightValue * i)<this._drawingAreaRectangle.Top)
					continue;
				g.DrawLine(gPen, (float)MappedXCoordinatePixels(0.0), (float)MappedYCoordinateValue(this._gridHeightValue * i), (float)MappedXCoordinatePixels(this._drawingAreaWidthPixels), (float)MappedYCoordinateValue(this._gridHeightValue * i));
				float value = (float)this._gridHeightValue * i;
				g.DrawString(value.ToString(), new Font("Tahoma", fontSize), gBrush, (float)MappedXCoordinatePixels(-2), (float)MappedYCoordinateValue(value), format);
			}

			// -ve grid lines
			gridCount = (int)(this._negativeHeightPixels / this._gridHeightPixels);
			if(gridCount>0)
				gridCount++;
			for (int i = 1; i <= gridCount; i++)
			{
				if (MappedYCoordinateValue(this._gridHeightValue * i * (-1)) > this._drawingAreaRectangle.Bottom || MappedYCoordinateValue(this._gridHeightValue * i) < this._drawingAreaRectangle.Top)
					continue;
				g.DrawLine(gPen, (float)MappedXCoordinatePixels(0.0), (float)MappedYCoordinateValue(this._gridHeightValue * i * (-1)), (float)MappedXCoordinatePixels(this._drawingAreaWidthPixels), (float)MappedYCoordinateValue(this._gridHeightValue * i * (-1)));
				float value = (float)this._gridHeightValue * i * (-1);
				g.DrawString(value.ToString(), new Font("Tahoma", fontSize), gBrush, (float)MappedXCoordinatePixels(-2), (float)MappedYCoordinateValue(value), format);
			}
		}

		private void DrawTitle(ref Graphics g)
		{
			if(this._lineGraph.Text==null || this._lineGraph.Text==String.Empty)
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
			Point Middle = new Point((int)MappedXCoordinatePixels(this._drawingAreaWidthPixels/2), (int)this._titleHeightPixels/2);
			g.TranslateTransform(Middle.X, Middle.Y);
			g.DrawString(this._lineGraph.Text, new Font("Tahoma", fontSize, FontStyle.Bold), gBrush, 0, 0, format);
			g.TranslateTransform(-Middle.X, -Middle.Y);
		}

		private void DrawXAxisText(ref Graphics g)
		{
			if(this._lineGraph.XAxisTextCollection==null)
				return;

			if (this._lineGraph.XAxisTextCollection.Count == 0) return;

			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);

			SolidBrush gBrushFont = new SolidBrush(Color.Black);

			// Set up Pen and Brush for lines
			Pen gPen = new Pen(Color.Gray, (float)0.03);
			SolidBrush gBrush = new SolidBrush(this._lineGraph.Color);

			double xStart;
			double xEnd;

			for (int i = 0; i < this._lineGraph.XAxisTextCollection.Count; i++)
			{
				xStart = this._lineGraph.XAxisTextCollection[i].XValueStart;
				xEnd = this._lineGraph.XAxisTextCollection[i].XValueEnd;
				g.DrawLine(gPen, (int)MappedXCoordinateValue(xStart), (int)this._drawingAreaRectangle.Top, (int)MappedXCoordinateValue(xStart), (int)this._drawingAreaRectangle.Bottom);
				g.DrawLine(gPen, (int)MappedXCoordinateValue(xEnd), (int)this._drawingAreaRectangle.Top, (int)MappedXCoordinateValue(xEnd), (int)this._drawingAreaRectangle.Bottom);
				if (xStart == xEnd)
				{
					float fontSize = (float)0.5 * (float)this._gridHeightPixels;

					if (fontSize > (float)9.0)
						fontSize = (float)9.0;

					if (fontSize < (float)1.0)
						fontSize = (float)1.0;

					//Draw Text
					Point Middle = new Point((int)MappedXCoordinateValue(xStart), (int)this._drawingAreaRectangle.Bottom + 4);

					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Center;
					
					g.TranslateTransform(Middle.X, Middle.Y);
					g.RotateTransform(35 * (-1));
					g.DrawString(this._lineGraph.XAxisTextCollection[i].Text.Trim(), new Font("Tahoma", fontSize), gBrushFont, 0, 0, format);
					g.RotateTransform(35);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
				else
				{
					float fontSize = (float)0.5 * (float)this._gridHeightPixels;

					if (fontSize > (float)10.0)
						fontSize = (float)10.0;

					if (fontSize < (float)1.0)
						fontSize = (float)1.0;

					format.Alignment = StringAlignment.Center; 
					format.LineAlignment = StringAlignment.Near;
				
					//Draw Text
					Point Middle = new Point((int)((MappedXCoordinateValue(xStart) + MappedXCoordinateValue(xEnd)) / 2), (int)this._drawingAreaRectangle.Bottom + 2);

					g.TranslateTransform(Middle.X, Middle.Y);
					g.DrawString(this._lineGraph.XAxisTextCollection[i].Text.Trim(), new Font("Tahoma", fontSize), gBrushFont, 0, 0, format);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
			}
		}

    private void DrawYAxisText(ref Graphics g)
    {
      if (this._lineGraph.YAxisText.Trim() == "")
        return;

      // Set up Font
      StringFormat format = new StringFormat(StringFormatFlags.NoWrap);

      SolidBrush gBrushFont = new SolidBrush(Color.Black);

      // Set up Pen and Brush for lines
      Pen gPen = new Pen(Color.Gray, (float)0.03);
      SolidBrush gBrush = new SolidBrush(this._lineGraph.Color);

      float fontSize = (float)0.5 * (float)this._gridHeightPixels;

      if (fontSize > (float)9.0)
        fontSize = (float)9.0;

      if (fontSize < (float)1.0)
        fontSize = (float)1.0;

      Font myFont = new Font("Arial Unicode MS", fontSize);

      Point PositionForUnitMeasure = new Point(5, ((int)MappedYCoordinateValue(this.GetMaximumYValue())) - myFont.Height - 8);

      g.DrawString(this._lineGraph.YAxisText.Trim(), myFont, gBrushFont, PositionForUnitMeasure.X, PositionForUnitMeasure.Y, format);
    }

    private void DrawHighlightPoint(ref Graphics g)
    {
      if (this._lineGraph.PointTohighlight == null)
        return;

      if ((this._lineGraph.PointTohighlight.X < 0) || (this._lineGraph.PointTohighlight.Y < 0))
        return;

      double newX = MappedXCoordinateValue(this._lineGraph.PointTohighlight.X)-6;
      double newY = MappedYCoordinateValue(this._lineGraph.PointTohighlight.Y)-6;

      Pen gPen = new Pen(Color.Black, 1);

      g.DrawRectangle(gPen, (int)newX, (int)newY, 12, 12);
      g.FillEllipse(Brushes.Black, (int)newX + 3, (int)newY + 3, 6, 6);
    }

		private void DrawPhaseLines(ref Graphics g)
		{
			if (this._lineGraph.PhaseLines == null)
				return;

			if (this._lineGraph.PhaseLines.Count<= 0) 
				return;

			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);
			SolidBrush gBrushFont = new SolidBrush(Color.Red);

			// Set up Pen and Brush for lines
			Pen gPen = new Pen(Color.Red, (float)0.03);
			double xStart;
			double xEnd;

			for (int i = 0; i < this._lineGraph.PhaseLines.Count; i++)
			{
				xStart = this._lineGraph.PhaseLines[i].XValueStart;
				xEnd = this._lineGraph.PhaseLines[i].XValueEnd;
				g.DrawLine(gPen, (int)MappedXCoordinateValue(xStart), (int)this._drawingAreaRectangle.Top, (int)MappedXCoordinateValue(xStart), (int)this._drawingAreaRectangle.Bottom);
				g.DrawLine(gPen, (int)MappedXCoordinateValue(xEnd), (int)this._drawingAreaRectangle.Top, (int)MappedXCoordinateValue(xEnd), (int)this._drawingAreaRectangle.Bottom);
				if (xStart == xEnd)
				{
					float fontSize = (float)0.9 * (float)this._marginForPhaseLinesText;

					if (fontSize > (float)8.0)
						fontSize = (float)8.0;

					if (fontSize < (float)1.0)
						fontSize = (float)1.0;

					//Draw Text
					Point Middle = new Point((int)MappedXCoordinateValue(xEnd), (int)(this._drawingAreaRectangle.Top - 1));

					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Far;
					g.TranslateTransform(Middle.X, Middle.Y);
					g.DrawString(this._lineGraph.PhaseLines[i].Text.Trim(), new Font("Tahoma", fontSize), gBrushFont, 0, 0, format);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
				else
				{
					float fontSize = (float)0.9 * (float)this._marginForPhaseLinesText;

					if (fontSize > (float)8.0)
						fontSize = (float)8.0;

					if (fontSize < (float)1.0)
						fontSize = (float)1.0;

					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Far;

					//Draw Text
					Point Middle = new Point((int)((MappedXCoordinateValue(xStart) + MappedXCoordinateValue(xEnd)) / 2), (int)(this._drawingAreaRectangle.Top - 1));

					g.TranslateTransform(Middle.X, Middle.Y);
					g.DrawString(this._lineGraph.PhaseLines[i].Text.Trim(), new Font("Tahoma", fontSize), gBrushFont, 0, 0, format);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
			}
		}


		private void DrawBackColor(ref Graphics g)
		{
			Pen gPen = new Pen(Color.Black, (float)0.03);
			SolidBrush gBrush = new SolidBrush(this._lineGraph.Color);

			g.FillRectangle(gBrush, this._drawingAreaRectangle);
			g.DrawRectangle(gPen, this._drawingAreaRectangle);
		}

		private void DrawBackColorGradient(Color startColor, Color endColor, ref Graphics g)
		{
			Pen gPen = new Pen(Color.Black, (float)0.03);
			LinearGradientBrush lgBrush = new LinearGradientBrush(this._drawingAreaRectangle, startColor, endColor, (float)(45 * (-1)), true);

			g.FillRectangle(lgBrush, this._drawingAreaRectangle);
			gPen.Color = Color.Black;
			g.DrawRectangle(gPen, this._drawingAreaRectangle);
		}

		private void DrawCustomBackColorGradient(Color startColor, Color endColor, ref Graphics g)
		{
			int startR = (int)startColor.R;
			int startG = (int)startColor.G;
			int startB = (int)startColor.B;
			int endR = (int)endColor.R;
			int endG = (int)endColor.G;
			int endB = (int)endColor.B;
			double deltaR = (endR - startR) / this._drawingAreaWidthPixels;
			double deltaG = (endG - startG) / this._drawingAreaWidthPixels;
			double deltaB = (endB - startB) / this._drawingAreaWidthPixels;
			int currentR = startR;
			int currentG = startG;
			int currentB = startB;

			Pen gPen = new Pen(Color.Black, (float)0.03);

			for (int i = 0; i < this._drawingAreaWidthPixels; i++)
			{
				gPen.Color = Color.FromArgb(currentR + (int)(deltaR * i), currentG + (int)(deltaG * i), currentB + (int)(deltaB * i));
				g.DrawLine(gPen, (int)this._drawingAreaRectangle.Left + i, (int)this._drawingAreaRectangle.Top, (int)this._drawingAreaRectangle.Left + i, (int)this._drawingAreaRectangle.Bottom);
			}

			gPen.Color = Color.Black;
			g.DrawRectangle(gPen, this._drawingAreaRectangle);
		}

		private void DrawTrendLine(ref Graphics g)
		{
			if (this._lineGraph.TrendLine == null || this._lineGraph.TrendLine.Points == null || this._lineGraph.TrendLine.Points.Count <= 0)
				return;

			Pen gPen = new Pen(this._lineGraph.TrendLine.Color, this._lineGraph.TrendLine.Width);
			gPen.DashStyle = DashStyle.Dash;
			gPen.DashCap = DashCap.Triangle;
			DrawLineGraph(this._lineGraph.TrendLine, gPen, ref g);
		}

		private void DrawBorder(ref Graphics g)
		{
			Pen gPen = new Pen(Color.Gray, (float)0.3);
			g.DrawRectangle(gPen, (int)this._totalRectangle.Left, (int)this._totalRectangle.Top, (int)this._widthPixels - 1, (int)this._heightPixels - 1);
		}

		private double GetMaxXValue()
		{
			if (this._lineGraph.XAxisTextCollection == null)
				return 0.0;

			if (this._lineGraph.XAxisTextCollection.Count == 0)
				return 0.0;

			double maxValue = Double.MinValue;

			for (int i = 0; i < this._lineGraph.XAxisTextCollection.Count; i++)
			{
				double x;

				if (this._lineGraph.XAxisTextCollection[i].XValueStart == this._lineGraph.XAxisTextCollection[i].XValueEnd)
					x = this._lineGraph.XAxisTextCollection[i].XValueStart;
				else
					x = (this._lineGraph.XAxisTextCollection[i].XValueStart > this._lineGraph.XAxisTextCollection[i].XValueEnd) ? this._lineGraph.XAxisTextCollection[i].XValueStart : this._lineGraph.XAxisTextCollection[i].XValueEnd;

				if (x > maxValue)
					maxValue = x;
			}

			return maxValue;
		}

		private void DrawXAxisSectionMarks(ref Graphics g)
		{
			// Set up Pen and Brush for lines
			Pen gPen = new Pen(Color.Gray, (float)0.03);
			double currentX = 0.0;
			double height = this._gridHeightPixels / 10;

			if (height > 5.0)
				height = 5.0;

			while (currentX <= this._drawingAreaWidthPixels)
			{
				g.DrawLine(gPen, (int)MappedXCoordinateValue(currentX), (int)(this._drawingAreaRectangle.Bottom - height), (int)MappedXCoordinateValue(currentX), (int)this._drawingAreaRectangle.Bottom);
				currentX += this._xAxisIntervalValue;
			}
		}

	}// class
}// namespace
