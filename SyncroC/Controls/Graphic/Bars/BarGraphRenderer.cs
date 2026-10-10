using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Microsoft.Tools.Graphs.Bars
{
	/// <summary>
	/// Summary description for BarGraphRenderer.
	/// </summary>
	internal class BarGraphRenderer
	{

		private double _widthPixels;
		private double _heightPixels;
		private double _drawingAreaWidthPixels;
		private double _drawingAreaHeightPixels;
		private double _minimumValue;
		private double _maximumValue;
		private double _totalValue;
		private double _positiveHeightPixels;	// In Drawing Area
		private double _negativeHeightPixels;	// In Drawing Area
		private double _gridHeightValue;
		private double _gridHeightPixels;
		private double _titleHeightPixels;
		private int _totalSlices;
		private double _barSliceAllotedWidth;
		private double _barGap;
		private double _barSliceWidth;
		
		private Rectangle _totalRectangle;
		private Rectangle _drawingAreaRectangle;
		private Rectangle _titleRectangle;
		
		private BarGraph _barGraph;
		
		private int _marginForTextOnXAxis;
		private int _marginForNumbersOnYAxis = 15;
		private double _maxTitleHeightPixels = 15.0;
		private float _maxFontSizeVerticalBottom = 10.0F;
		private float _maxFontSizeHorizontalLeft = 7.0F;
		private float _maxFontSize;


		public BarGraphRenderer()
		{
		}

		public Image DrawGraph(BarGraph barGraph)
		{
			if(barGraph==null)
				return null;

			_barGraph = barGraph;

			Bitmap bMap = null;
			if(this._barGraph.Alignment==Alignment.VerticalBottom)
				bMap = new Bitmap(barGraph.Size.Width, barGraph.Size.Height, PixelFormat.Format64bppPArgb);
			else if(this._barGraph.Alignment==Alignment.HorizontalLeft)
				bMap = new Bitmap(barGraph.Size.Height, barGraph.Size.Width, PixelFormat.Format64bppPArgb);

			Graphics g = Graphics.FromImage(bMap);

			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.Clear(Color.White);
			DrawVerticalBottomGraph(ref g);
			switch (barGraph.Alignment)
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

		private void DrawVerticalBottomGraph(ref Graphics g)
		{
			try
			{
				CalculateValues();
				//DrawBackColor(ref g);
				DrawBackColorGradient(this._barGraph.Color, this._barGraph.ColorGradient, ref g);
				//DrawCustomBackColorGradient(Color.White, Color.DarkGray, ref g);
				DrawBarGraph(ref g);
				DrawGrid(ref g);
				DrawCutOff(ref g);
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
			this._marginForTextOnXAxis = this._barGraph.MarginForTextOnAxis;

			if (this._barGraph.Alignment == Alignment.VerticalBottom)
			{
				this._widthPixels = this._barGraph.Size.Width;
				this._heightPixels = this._barGraph.Size.Height;
			}
			else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
			{
				this._widthPixels = this._barGraph.Size.Height;
				this._heightPixels = this._barGraph.Size.Width;
			}

			this._drawingAreaWidthPixels = this._widthPixels - (2 * this._barGraph.Border) - this._marginForNumbersOnYAxis;
			this._drawingAreaHeightPixels = this._heightPixels - (2 * this._barGraph.Border) - this._marginForTextOnXAxis;
			if (this._barGraph.Text != null && this._barGraph.Text != String.Empty)
			{
				if (this._barGraph.Alignment == Alignment.VerticalBottom)
				{
					this._titleHeightPixels = this._drawingAreaHeightPixels * 0.1;
					if (this._titleHeightPixels > this._maxTitleHeightPixels)
						this._titleHeightPixels = this._maxTitleHeightPixels;
					this._drawingAreaHeightPixels = this._drawingAreaHeightPixels - this._titleHeightPixels;
				}
				else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
				{
					this._titleHeightPixels = this._drawingAreaWidthPixels * 0.1;
					if (this._titleHeightPixels > this._maxTitleHeightPixels)
						this._titleHeightPixels = this._maxTitleHeightPixels;
					this._drawingAreaWidthPixels = this._drawingAreaWidthPixels - this._titleHeightPixels;
				}
			}

			this._minimumValue = GetMinimumValue();
			this._maximumValue = GetMaximumValue();
			this._totalValue = GetTotalValue();

			this._positiveHeightPixels = GetPositiveHeightPixels();
			this._negativeHeightPixels = GetNegativeHeightPixels();

			this._gridHeightValue = GetGridHeightValue();
			this._gridHeightPixels = GetGridHeightPixels();

			this._totalSlices = GetTotalSlices();
			this._barSliceAllotedWidth = GetBarSliceAllotedWidth();
			this._barGap = this._barGraph.BarGap;
			this._barSliceWidth = GetBarSliceWidth();

			this._totalRectangle = new Rectangle(0, 0, (int)this._widthPixels, (int)this._heightPixels);

			if (this._barGraph.Alignment == Alignment.VerticalBottom)
			{
				this._drawingAreaRectangle = new Rectangle((int)this._barGraph.Border + this._marginForNumbersOnYAxis, (int)this._barGraph.Border + (int)this._titleHeightPixels, (int)this._drawingAreaWidthPixels, (int)this._drawingAreaHeightPixels);
				this._titleRectangle = new Rectangle((int)this._barGraph.Border, (int)this._barGraph.Border, (int)this._drawingAreaWidthPixels, (int)this._titleHeightPixels);
			}
			else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
			{
				this._drawingAreaRectangle = new Rectangle((int)this._barGraph.Border + this._marginForNumbersOnYAxis, (int)this._barGraph.Border, (int)this._drawingAreaWidthPixels, (int)this._drawingAreaHeightPixels);
				this._titleRectangle = new Rectangle((int)this._drawingAreaRectangle.Right, (int)this._barGraph.Border, (int)this._titleHeightPixels, (int)this._drawingAreaHeightPixels);
			}

			this._maxFontSize = GetMaxFontSize();
		}

		private float GetMaxFontSize()
		{
			switch (this._barGraph.Alignment)
			{
				case Alignment.VerticalBottom:
					return this._maxFontSizeVerticalBottom;
				case Alignment.HorizontalLeft:
					return this._maxFontSizeHorizontalLeft;
				default:
					return 8.0F;
			}
		}

		private double GetMinimumValue()
		{
			double minValue = Double.MaxValue;

			BarSliceCollection barSliceCollection = this._barGraph.BarSliceCollection;

			if (barSliceCollection == null || barSliceCollection.Count == 0)
				return 0.0;

			if (this._barGraph.MultiBarDisplayStyle == MultiBarDisplayStyle.SingleBar)
			{
				foreach (BarSlice barSlice in barSliceCollection)
				{
					if (barSlice.Value < minValue)
						minValue = barSlice.Value;
				}
			}
			else
			{
				foreach (BarSlice barSlice in barSliceCollection)
				{
					if (barSlice.GetType().Name == "BarSlice")
					{
						if (barSlice.Value < minValue)
							minValue = barSlice.Value;
					}
					else
					{
						if (((MultipleBarSlice)barSlice).MinValue < minValue)
							minValue = ((MultipleBarSlice)barSlice).MinValue;
					}
				}
			}

			if (minValue > 0)
				minValue = 0;

			if (this._barGraph.RoundOffGridHeight)
				if (minValue < 0.0 && minValue > -10.0)
					minValue = -10.0;

			return minValue;
		}

		private double GetMaximumValue()
		{
			double maxValue = Double.MinValue;

			BarSliceCollection barSliceCollection = this._barGraph.BarSliceCollection;

			if (barSliceCollection == null || barSliceCollection.Count == 0)
				return 0;

			if (this._barGraph.MultiBarDisplayStyle == MultiBarDisplayStyle.SingleBar)
			{
				foreach (BarSlice barSlice in barSliceCollection)
				{
					if (barSlice.Value > maxValue)
						maxValue = barSlice.Value;
				}
			}
			else
			{
				foreach (BarSlice barSlice in barSliceCollection)
				{
					if (barSlice.GetType().Name == "BarSlice")
					{
						if (barSlice.Value > maxValue)
							maxValue = barSlice.Value;
					}
					else
					{
						if (((MultipleBarSlice)barSlice).MaxValue > maxValue)
							maxValue = ((MultipleBarSlice)barSlice).MaxValue;
					}
				}
			}

			if (this._barGraph.RoundOffGridHeight)
				if (maxValue < 10.0)
					maxValue = 10.0;

			if (this._barGraph.HonorScale && maxValue < this._barGraph.MaxScaleValue) return this._barGraph.MaxScaleValue;
			else
				return maxValue;
		}

		private double GetTotalValue()
		{
			double retVal = this._maximumValue - this._minimumValue;

			if (retVal < 0.0)
				retVal = retVal * (-1.0);

			return retVal;
		}

		private double GetPositiveHeightPixels()
		{
			if (this._minimumValue >= 0.0 && this._maximumValue >= 0)
				return this._drawingAreaHeightPixels;

			if (this._minimumValue <= 0.0 && this._maximumValue <= 0)
				return 0.0;

			if (this._minimumValue > this._maximumValue)
				throw new Exception("Error: Minimum value is more than Maximum value");

			return (this._drawingAreaHeightPixels * this._maximumValue) / this._totalValue;
		}

		private double GetNegativeHeightPixels()
		{
			if (this._minimumValue >= 0.0 && this._maximumValue >= 0)
				return 0.0;

			if (this._minimumValue <= 0.0 && this._maximumValue <= 0)
				return this._drawingAreaHeightPixels;

			if (this._minimumValue > this._maximumValue)
				throw new Exception("Error: Minimum value is more than Maximum value");

			double retVal = (this._drawingAreaHeightPixels * this._minimumValue) / this._totalValue;

			if (retVal < 0.0)
				retVal = retVal * (-1);

			return retVal;
		}

		private double GetGridHeightValue()
		{
			if (this._barGraph.GridSpacingValue != 0)
				return this._barGraph.GridSpacingValue;

			double retVal;
			double max = this._maximumValue;
			double min = this._minimumValue;

			if (this._maximumValue < 0.0)
				max = this._maximumValue * (-1);

			if (this._minimumValue < 0.0)
				min = this._minimumValue * (-1);

			if (max > min)
				retVal = max / 10;
			else
				retVal = min / 10;

			if (this._barGraph.RoundOffGridHeight)
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
			return (this._gridHeightValue * this._drawingAreaHeightPixels) / this._totalValue;
		}

		// The return value is to be used as is. It is calculated w.r.t. _drawingAreaRectangle.Left
		private double MappedXCoordinate(double value)
		{
			return this._drawingAreaRectangle.Left + value;
		}

		// The return value is to be used as is. It is calculated w.r.t. _drawingAreaRectangle.Top
		private double MappedYCoordinate(double value)
		{
			if (value == 0.0)
				return this._drawingAreaRectangle.Top + this._positiveHeightPixels;

			double retVal;

			if (value > 0.0) // value is positive
			{
				if (this._minimumValue >= 0.0 && this._maximumValue >= 0) // both positive
				{
					double diff = this._totalValue;

					retVal = (value * this._drawingAreaHeightPixels) / diff;
					retVal = this._drawingAreaHeightPixels - retVal;
				}
				else if (this._minimumValue <= 0.0 && this._maximumValue <= 0) // both negative
				{
					throw new Exception("Error: Value is positive while both Max and Mix values are negative");
				}
				else if (this._minimumValue > this._maximumValue) // error
				{
					throw new Exception("Error: Minimum value is more than Maximum value");
				}
				else // Max positive and Min negative
				{
					double diff = this._totalValue;

					retVal = (value * this._drawingAreaHeightPixels) / diff;
					retVal = this._positiveHeightPixels - retVal;
				}
			}
			else // value is negative
			{
				value = value * (-1);
				if (this._minimumValue >= 0.0 && this._maximumValue >= 0) // both positive
				{
					throw new Exception("Error: Value is negative while both Max and Mix values are positive");
				}
				else if (this._minimumValue <= 0.0 && this._maximumValue <= 0) // both negative
				{
					double diff = this._totalValue;

					retVal = (value * this._drawingAreaHeightPixels) / diff;
				}
				else if (this._minimumValue > this._maximumValue) // error
				{
					throw new Exception("Error: Minimum value is more than Maximum value");
				}
				else // Max positive and Min negative
				{
					double diff = this._totalValue;

					retVal = (value * this._drawingAreaHeightPixels) / diff;
					retVal = this._positiveHeightPixels + retVal;
				}
			}

			return _drawingAreaRectangle.Top + retVal;
		}

		private int GetTotalSlices()
		{
			if(this._barGraph.BarSliceCollection==null)
				return 0;
			else
				return this._barGraph.BarSliceCollection.Count;
		}

		private double GetBarSliceAllotedWidth()
		{
			if(this._totalSlices==0.0)
				return this._drawingAreaWidthPixels;
			else
				return this._drawingAreaWidthPixels / this._totalSlices;
		}

		private double GetBarSliceWidth()
		{
			double retVal = this._barSliceAllotedWidth - this._barGap;
			if (retVal <= 0.0) // -ve
			{
				if (this._barSliceAllotedWidth >= 1.0)
				{
					retVal = 1.0;
					this._barGap = this._barSliceAllotedWidth - 1.0;
					return retVal;
				}
				else
				{
					throw new Exception("Bar Slice Width is less than or equal to 1. Please increase the BarGraph Width");
				}
			}
			else // +ve
			{
				/*if(retVal<this._barGraph.MaxBarSliceWidth)
					return retVal;
				else
					return this._barGraph.MaxBarSliceWidth;*/
				if (retVal > this._barGraph.MaxBarSliceWidth)
					return this._barGraph.MaxBarSliceWidth;
				else
					return retVal;
			}
		}

		private void DrawBarGraph(ref Graphics g)
		{
			if(this._totalSlices==0)
				return;

			for (int i = 0; i < this._totalSlices; i++)
			{
				DrawBarSlice(i, ref g);
				DrawText(i, ref g);
			}
		}

		private void DrawBarSlice(int index, ref Graphics g)
		{
			BarSlice bar = this._barGraph.BarSliceCollection[index];
			double width = this._barSliceWidth;
			if(width>bar.MaxWidth)
				width = bar.MaxWidth;

			Pen gPen = new Pen(Color.Black, (float)0.03);
			SolidBrush gBrush = new SolidBrush(this._barGraph.Color);
			LinearGradientBrush lgBrush = null; 

			int startX = (int)((this._barSliceAllotedWidth * index) + (this._barSliceAllotedWidth / 2) - (width / 2));
			Rectangle rect;
			Rectangle shadowRect;

			if (bar.GetType().Name != "MultipleBarSlice")
			{
				if(bar.Value>=0)
				{
					rect = new Rectangle((int)MappedXCoordinate(startX), (int)MappedYCoordinate(bar.Value), (int)width, (int)((bar.Value * this._drawingAreaHeightPixels) / this._totalValue));
					shadowRect = new Rectangle((int)MappedXCoordinate(startX) + 2, (int)MappedYCoordinate(bar.Value) - 3, (int)width, (int)((bar.Value * this._drawingAreaHeightPixels) / this._totalValue));
					try{
						lgBrush = new LinearGradientBrush(rect, bar.Color, bar.ColorGradient, (float)(45 * (-1)), true);
					}catch { };
				}
				else
				{
					rect = new Rectangle((int)MappedXCoordinate(startX), (int)MappedYCoordinate(0.0), (int)width, (int)((bar.Value * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
					shadowRect = new Rectangle((int)MappedXCoordinate(startX) + 2, (int)MappedYCoordinate(0.0) + 3, (int)width, (int)((bar.Value * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
					try
					{
						lgBrush = new LinearGradientBrush(rect, bar.Color, bar.ColorGradient, (float)(45), true);
					}catch { };
				}

				if (lgBrush != null)
				{
					gBrush.Color = Color.LightGray;
					g.FillRectangle(gBrush, shadowRect);
					gBrush.Color = bar.Color;

					//g.FillRectangle(gBrush, rect);
					g.FillRectangle(lgBrush, rect);
					g.DrawRectangle(gPen, rect);
				}
			}
			else
			{
				MultipleBarSlice mbs = (MultipleBarSlice)bar;
				if(mbs.PartialValues==null)
					return;
				if(mbs.PartialValues.Length==0)
					return;

				if (this._barGraph.MultiBarDisplayStyle == MultiBarDisplayStyle.SeparateBars) // SeparateBars
				{
					double partialValue = 0;
					double cumulativeValue = 0;

					for (int i = 0; i < mbs.PartialValues.Length; i++)
					{
						partialValue = mbs.PartialValues[i];
						cumulativeValue += mbs.PartialValues[i];
						if (partialValue >= 0)
						{
							rect = new Rectangle((int)MappedXCoordinate(startX + (i * width/mbs.PartialValues.Length)), (int)MappedYCoordinate(partialValue), (int)(width/mbs.PartialValues.Length), (int)((partialValue * this._drawingAreaHeightPixels) / this._totalValue));
							shadowRect = new Rectangle((int)MappedXCoordinate(startX + (i * width/mbs.PartialValues.Length)) + 2, (int)MappedYCoordinate(partialValue) - 3, (int)(width/mbs.PartialValues.Length), (int)((partialValue * this._drawingAreaHeightPixels) / this._totalValue));
						}
						else
						{
							rect = new Rectangle((int)MappedXCoordinate(startX + (i * width/mbs.PartialValues.Length)), (int)MappedYCoordinate(0), (int)(width/mbs.PartialValues.Length), (int)((partialValue * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
							shadowRect = new Rectangle((int)MappedXCoordinate(startX + (i * width/mbs.PartialValues.Length)) + 2, (int)MappedYCoordinate(0) + 3, (int)(width/mbs.PartialValues.Length), (int)((partialValue * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
						}

						gBrush.Color = Color.LightGray;
						g.FillRectangle(gBrush, shadowRect);
						gBrush.Color = mbs.PartialColors[i];
						g.FillRectangle(gBrush, rect);
						g.DrawRectangle(gPen, rect);
					}// for
				} // if SeparateBars

				else // SingleBar
				{
					double partialValue = 0;
					double cumulativeValue = 0;

					for (int i = 0; i < mbs.PartialValues.Length; i++)
					{
						partialValue = mbs.PartialValues[i];
						cumulativeValue += mbs.PartialValues[i];
						if (partialValue >= 0)
						{
							rect = new Rectangle((int)MappedXCoordinate(startX), (int)MappedYCoordinate(cumulativeValue), (int)width, (int)((partialValue * this._drawingAreaHeightPixels) / this._totalValue));
							shadowRect = new Rectangle((int)MappedXCoordinate(startX) + 2, (int)MappedYCoordinate(cumulativeValue) - 3, (int)width, (int)((partialValue * this._drawingAreaHeightPixels) / this._totalValue));
						}
						else
						{
							rect = new Rectangle((int)MappedXCoordinate(startX), (int)MappedYCoordinate(cumulativeValue - partialValue), (int)width, (int)((partialValue * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
							shadowRect = new Rectangle((int)MappedXCoordinate(startX) + 2, (int)MappedYCoordinate(cumulativeValue - partialValue) + 3, (int)width, (int)((partialValue * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
						}

						gBrush.Color = Color.LightGray;
						g.FillRectangle(gBrush, shadowRect);
						gBrush.Color = mbs.PartialColors[i];
						g.FillRectangle(gBrush, rect);
						g.DrawRectangle(gPen, rect);

					} // for

				} // else SingleBar

			}// else -> MultipleBarSlice

		}// DrawBarSlice function

		private void DrawText(int index, ref Graphics g)
		{
			// Set up Font
			SolidBrush gBrush = new SolidBrush(Color.Black);
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);

			float fontSize = (float)0.5 * (float)this._gridHeightPixels;

			if (fontSize > (float)7.0)
				fontSize = (float)7.0;

			if (fontSize < (float)1.0)
				fontSize = (float)1.0;

			BarSlice bar = this._barGraph.BarSliceCollection[index];
			double width = this._barSliceWidth;
			if (width > bar.MaxWidth)
				width = bar.MaxWidth;

			Rectangle rect;
			Rectangle shadowRect;
			int startX = (int)((this._barSliceAllotedWidth * index) + (this._barSliceAllotedWidth / 2) - (width / 2));
			if (bar.Value >= 0)
			{
				rect = new Rectangle((int)MappedXCoordinate(startX), (int)MappedYCoordinate(bar.Value), (int)width, (int)((bar.Value * this._drawingAreaHeightPixels) / this._totalValue));
				shadowRect = new Rectangle((int)MappedXCoordinate(startX) + 2, (int)MappedYCoordinate(bar.Value) - 3, (int)width, (int)((bar.Value * this._drawingAreaHeightPixels) / this._totalValue));
				
				if (this._barGraph.Alignment == Alignment.VerticalBottom)
				{
					//Draw Text
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Center;

					Point Middle = new Point(rect.Left + (int)(rect.Width / 2), rect.Bottom + 4);

					g.TranslateTransform(Middle.X, Middle.Y);
					g.RotateTransform((float)45.0 * -1);
					g.DrawString(bar.Text, new Font("Tahoma", fontSize), gBrush, 0, 0, format);
					g.RotateTransform((float)45.0);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
				else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
				{
					//Draw Text
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Center;

					Point Middle = new Point(rect.Left + (int)(rect.Width / 2), rect.Bottom + 4);

					g.TranslateTransform(Middle.X, Middle.Y);
					g.RotateTransform((float)245.0);
					g.DrawString(bar.Text, new Font("Tahoma", fontSize), gBrush, 0, 0, format);
					g.RotateTransform((float)245.0 * -1);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
			}
			else
			{
				rect = new Rectangle((int)MappedXCoordinate(startX), (int)MappedYCoordinate(0.0), (int)width, (int)((bar.Value * (-1) * this._drawingAreaHeightPixels) / this._totalValue));
				shadowRect = new Rectangle((int)MappedXCoordinate(startX) + 2, (int)MappedYCoordinate(0.0) + 3, (int)width, (int)((bar.Value * (-1) * this._drawingAreaHeightPixels) / this._totalValue));

				if (this._barGraph.Alignment == Alignment.VerticalBottom)
				{
					//Draw Text
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Center;

					Point Middle = new Point(rect.Left + (int)(rect.Width / 2), rect.Top - 4);

					g.TranslateTransform(Middle.X, Middle.Y);
					g.RotateTransform((float)45.0 * -1);
					g.DrawString(bar.Text, new Font("Tahoma", fontSize), gBrush, 0, 0, format);
					g.RotateTransform((float)45.0);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
				else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
				{
					//Draw Text
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Center;

					Point Middle = new Point(rect.Left + (int)(rect.Width / 2), rect.Top - 4);

					g.TranslateTransform(Middle.X, Middle.Y);
					g.RotateTransform((float)245.0);
					g.DrawString(bar.Text, new Font("Tahoma", fontSize), gBrush, 0, 0, format);
					g.RotateTransform((float)245.0 * -1);
					g.TranslateTransform(-Middle.X, -Middle.Y);
				}
			}
		}

		private void DrawGrid(ref Graphics g)
		{
			// Center line
			Pen gPen = new Pen(Color.Black, (float)2.0F);

			if (this._minimumValue <= 0.0 && this._maximumValue >= 0.0)
			{
				g.DrawLine(gPen, (float)MappedXCoordinate(0.0), (float)MappedYCoordinate(0.0), (float)MappedXCoordinate(this._drawingAreaWidthPixels), (float)MappedYCoordinate(0.0));
				DrawNumberNextToGrid(0, Color.Black, ref g);
			}

			if (!this._barGraph.ShowGrid)
				return;

			// +ve grid lines
			gPen = new Pen(Color.Gray, (float)0.03);

			int gridCount = (int)(this._positiveHeightPixels / this._gridHeightPixels);

			if (gridCount > 0)
				gridCount++;

			for (int i = 1; i <= gridCount; i++)
			{
				if (MappedYCoordinate(this._gridHeightValue * i) > this._drawingAreaRectangle.Bottom || MappedYCoordinate(this._gridHeightValue * i) < this._drawingAreaRectangle.Top)
					continue;

				g.DrawLine(gPen, (float)MappedXCoordinate(0.0), (float)MappedYCoordinate(this._gridHeightValue * i), (float)MappedXCoordinate(this._drawingAreaWidthPixels), (float)MappedYCoordinate(this._gridHeightValue * i));
				float value = (float)this._gridHeightValue * i;
				DrawNumberNextToGrid(value, Color.Black, ref g);
			}

			// -ve grid lines
			gridCount = (int)(this._negativeHeightPixels / this._gridHeightPixels);
			if (gridCount > 0)
				gridCount++;

			for (int i = 1; i <= gridCount; i++)
			{
				if (MappedYCoordinate(this._gridHeightValue * i * (-1)) > this._drawingAreaRectangle.Bottom || MappedYCoordinate(this._gridHeightValue * i) < this._drawingAreaRectangle.Top)
					continue;

				g.DrawLine(gPen, (float)MappedXCoordinate(0.0), (float)MappedYCoordinate(this._gridHeightValue * i * (-1)), (float)MappedXCoordinate(this._drawingAreaWidthPixels), (float)MappedYCoordinate(this._gridHeightValue * i * (-1)));
				float value = (float)this._gridHeightValue * i * (-1);
				DrawNumberNextToGrid(value, Color.Black, ref g);
			}
		}

		private void DrawNumberNextToGrid(float value, Color color, ref Graphics g)
		{
			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);
			float fontSize = (float)0.5 * (float)this._gridHeightPixels;

			if (fontSize > (float)this._maxFontSize)
				fontSize = (float)this._maxFontSize;

			if (fontSize < (float)1.0)
				fontSize = (float)1.0;

			SolidBrush gBrush = new SolidBrush(color);

			if (this._barGraph.Alignment == Alignment.VerticalBottom)
			{
				format.Alignment = StringAlignment.Far;
				format.LineAlignment = StringAlignment.Center;

				Point Middle = new Point((int)MappedXCoordinate(-2), (int)MappedYCoordinate(value));

				g.TranslateTransform(Middle.X, Middle.Y);
				g.RotateTransform((float)0.0 * -1);
				g.DrawString(value.ToString(), new Font("Tahoma", fontSize), gBrush, 0, 0, format);
				g.RotateTransform((float)0.0);
				g.TranslateTransform(-Middle.X, -Middle.Y);
			}
			else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
			{
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Center;

				Point Middle = new Point((int)MappedXCoordinate(fontSize * (-1)), (int)MappedYCoordinate(value));

				g.TranslateTransform(Middle.X, Middle.Y);
				g.RotateTransform((float)90.0 * -1);
				g.DrawString(value.ToString(), new Font("Tahoma", fontSize), gBrush, 0, 0, format);
				g.RotateTransform((float)90.0);
				g.TranslateTransform(-Middle.X, -Middle.Y);
			}
		}

		private void DrawTitle(ref Graphics g)
		{
			if (this._barGraph.Text == null || this._barGraph.Text == String.Empty)
				return;

			// Set up Font
			StringFormat format = new StringFormat(StringFormatFlags.NoClip);

			float fontSize = (float)0.8 * (float)this._titleHeightPixels;

			if (fontSize > (float)10.0)
				fontSize = (float)10.0;

			if (fontSize < (float)1.0)
				fontSize = (float)1.0;

			SolidBrush gBrush = new SolidBrush(Color.Black);

			if (this._barGraph.Alignment == Alignment.VerticalBottom)
			{
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Near;

				//Draw Title
				Point Middle = new Point((int)MappedXCoordinate(this._drawingAreaWidthPixels / 2), (int)this._titleHeightPixels / 2);

				g.TranslateTransform(Middle.X, Middle.Y);
				g.DrawString(this._barGraph.Text, new Font("Tahoma", fontSize, FontStyle.Bold), gBrush, 0, 0, format);
				g.TranslateTransform(-Middle.X, -Middle.Y);
			}
			else if (this._barGraph.Alignment == Alignment.HorizontalLeft)
			{
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Near;

				//Draw Title
				Point Middle = new Point((int)MappedXCoordinate(this._drawingAreaWidthPixels + this._titleHeightPixels / 2), (int)this._drawingAreaHeightPixels / 2);

				g.TranslateTransform(Middle.X, Middle.Y);
				g.RotateTransform((float)(90 * -1));
				g.DrawString(this._barGraph.Text, new Font("Tahoma", fontSize, FontStyle.Bold), gBrush, 0, 0, format);
				g.RotateTransform((float)90);
				g.TranslateTransform(-Middle.X, -Middle.Y);
			}
		}

		private void DrawBackColor(ref Graphics g)
		{
			Pen gPen = new Pen(Color.Black, (float)0.03);
			SolidBrush gBrush = new SolidBrush(this._barGraph.Color);
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

		private void DrawCutOff(ref Graphics g)
		{
			if(this._barGraph.CutOff==Double.MinValue)
				return;

			Pen gPen = new Pen(Color.Red, (float)1.5);
			g.DrawLine(gPen, (int)this._drawingAreaRectangle.Left, (int)MappedYCoordinate(this._barGraph.CutOff), (int)this._drawingAreaRectangle.Right, (int)MappedYCoordinate(this._barGraph.CutOff));
			DrawNumberNextToGrid((float)this._barGraph.CutOff, Color.Red, ref g);
		}

		private void DrawBorder(ref Graphics g)
		{
			Pen gPen = new Pen(Color.Gray, (float)0.3);
			g.DrawRectangle(gPen, (int)this._totalRectangle.Left, (int)this._totalRectangle.Top, (int)this._widthPixels - 1, (int)this._heightPixels - 1);
		}


	}// class
}// namespace