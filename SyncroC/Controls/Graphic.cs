using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Controls.Interfaces;
using SyncroC.Controls.Colors;

namespace SyncroC.Controls
{
	/// <summary>
	/// Styles of the the Graphic
	/// </summary>
	public enum GraphicStyle : int
	{
		/// <summary>graphic with lines.</summary>
		Lines,
		/// <summary>histogram graphic.</summary>
		Histogram,
		/// <summary>histogram graphic with lines</summary>
		HistogramAndLines,
		/// <summary>integral style graphic.</summary>
		Filled,
		/// <summary>integral style graphic with lines</summary>
		FilledAndLines
	}

	/// <summary>
	/// Speed Graphic
	/// </summary>
	public class Graphic : SyncroC.Controls.SyncroUserControl
	{
		//constants
		/// <summary>width of the grid lines in pixel.</summary>
		private const int GRID_WIDTH = 1; //pixel
		/// <summary> width of the lines that compose the graphic for the GraphStyle.Lines</summary>
		private const int LINES_WIDTH = 4;//pixel
		/// <summary> width of the border present on the right of the graphic.</summary>
		private const int RIGHT_BORDER_WIDTH = 40;//pixel
		/// <summary> width of the border present below the graphic.</summary>
		private const int LOW_BORDER_WIDTH = 20;//pixel
		/// <summary> width of the shift separator.</summary>
		private const int SHIFT_SEPARATOR_WIDTH = 2;//pixel

		/// <summary> Distance from the top of the graphic of the average speed strings.</summary>
		private const int SHIFT_STRING_DISTANCE_FROM_TOP = -4;//pixel
		/// <summary> Distance from the right of the separator of the average speed strings.</summary>
		private const int SHIFT_STRING_DISTANCE_FROM_RIGHT = 1;//pixel


		/// <summary> Minimum representable value on the graphic.</summary>
		private const int MIN_ELEMENT_VALUE = 0;
		/// <summary> Maximum representable value on the graphic.</summary>
		private const int MAX_ELEMENT_VALUE = 350;
		/// <summary> Color of the grid.</summary>
		private Color GRID_COLOR = Color.Gray;
		/// <summary> Color of the lines that compose the graphic.</summary>
		private Color LINES_COLOR = Color.FromArgb(0, 90, 82);
		/// <summary> Color used to fill the graphic.</summary>
		private Color FILLING_COLOR = Color.FromArgb(178, 205, 203);

		/// <summary>
		/// Maximum number of shift change contemporary visible into the graphic
		/// </summary>
		private int MAX_SHIFT_CHANGE_VISIBLE = 5;
		public int MaxShiftChangeVisible
		{
			get { return MAX_SHIFT_CHANGE_VISIBLE; }
		}

		/// <summary>
		/// Average speeds of the last MAX_SHIFT_CHANGE_VISIBLE shifts
		/// </summary>
		private int[] pastShiftAverageSpeeds;

		/// <summary>
		/// Average speeds of the current shifts
		/// </summary>
		private int currentShiftAverageSpeed;  

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel pnl_back;

		/// <summary> number of hours to represent on the graphic.</summary>
		private int hours = 8;
		/// <summary>
		/// Gets or sets the number of hours to represent on the graphic.
		/// </summary>
		[Description("Gets or sets the number of hours to represent on the graphic."), Category("FosberSoft")]
		public int Hours
		{
			get {	return hours;	}
			set {	hours = value;	}
		}

		/// <summary> number of elements to be showed into an hour interval.</summary>
		private int elementsXhour = 20;
		/// <summary>
		/// Gets or sets the number of elements to be showed into an hour interval.
		/// </summary>
		[Description("Gets or sets the number of elements to be showed into an hour interval."), Category("FosberSoft")]
		public int ElementsXhour
		{
			get {	return elementsXhour;	}
			set {	elementsXhour = value;	}
		}

		/// <summary> Current style for the graphic.</summary>
		private GraphicStyle style = GraphicStyle.Lines;
		/// <summary>
		/// Gets or sets the style of the graphic.
		/// </summary>
		[Description("Gets or sets the style of the graphic."), Category("FosberSoft")]
		public GraphicStyle Style
		{
			get {	return style;	}
			set {	style = value;	}
		}

		/// <summary> number of elements to be showed on the graphic.</summary>
		private int length;
		/// <summary> elements to be showed on the graphic.</summary>
		private Queue elements;
		/// <summary>Array for labels that represent the vertical lines of the grid.</summary>
		private Label[] verticalLabels;
		private System.Windows.Forms.Label lbl_300;
		private System.Windows.Forms.Label lbl_200;
		private System.Windows.Forms.Label lbl_100;
		private System.Windows.Forms.Label lbl_000;
		/// <summary>Array for labels that represent the hoirizontal lines of the grid.</summary>
		private Label[] horizontalLabels;

		/// <summary>
		/// Builder.
		/// </summary>
		public Graphic()
		{
			InitializeComponent();
			length = hours * elementsXhour;
			elements = new Queue();
			pastShiftAverageSpeeds = new int[MAX_SHIFT_CHANGE_VISIBLE];
			pastShiftAverageSpeeds.Initialize();
			lbl_300.ForeColor = GRID_COLOR;
			lbl_200.ForeColor = GRID_COLOR;
			lbl_100.ForeColor = GRID_COLOR;
			lbl_000.ForeColor = GRID_COLOR;
		}

		public void setPastShiftAverageSpeeds(int[] ShiftAverageSpeeds)
		{
			if (ShiftAverageSpeeds == null || ShiftAverageSpeeds.Length != MAX_SHIFT_CHANGE_VISIBLE)
			{
				pastShiftAverageSpeeds.Initialize();
				throw new Exception("setShiftAverageSpeeds - incorrect parameter.");
			}
			pastShiftAverageSpeeds = ShiftAverageSpeeds;
			pnl_back.Invalidate();
		}

		public void SetCurrentShiftAvgSpeed(int currentAvgSpeed)
		{
			currentShiftAverageSpeed = currentAvgSpeed;
			pnl_back.Invalidate();
		}

		/// <summary>
		/// Add a new element to be shown on the graphic.
		/// </summary>
		/// <param name="element">The new element to be showed on the graphic.</param>
		/// <exception cref="">Thrown if an invalid speed has been specified as parameter.</exception>
		public void NewSpeed(int element)
		{
			if (! IsValidElement(element))
			{
				throw new Exception("NewSpeed - \"" + element.ToString() + "\" is an invalid element for the graphic.");
			}
			if (elements.Count == length + MAX_SHIFT_CHANGE_VISIBLE)
				elements.Dequeue();
			elements.Enqueue(element);
		}

		public void Draw()
		{
			pnl_back.Invalidate();
		}

		public void Clear()
		{
			elements.Clear();
			Draw();
		}

		/// <summary>
		/// Determine if an element can be properly showed into the graphic.
		/// </summary>
		/// <param name="element">the element to verify the correctness</param>
		private bool IsValidElement(int element)
		{
			return ((element <= MAX_ELEMENT_VALUE && element >= MIN_ELEMENT_VALUE) || element == -1);//-1 means shift change
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnl_back = new System.Windows.Forms.Panel();
			this.lbl_000 = new System.Windows.Forms.Label();
			this.lbl_300 = new System.Windows.Forms.Label();
			this.lbl_200 = new System.Windows.Forms.Label();
			this.lbl_100 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pnl_back
			// 
			this.pnl_back.BackColor = System.Drawing.Color.Gainsboro;
			this.pnl_back.Location = new System.Drawing.Point(0, 16);
			this.pnl_back.Name = "pnl_back";
			this.pnl_back.Size = new System.Drawing.Size(464, 144);
			this.pnl_back.TabIndex = 0;
			this.pnl_back.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_back_Paint);
			// 
			// lbl_000
			// 
			this.lbl_000.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_000.Location = new System.Drawing.Point(472, 144);
			this.lbl_000.Name = "lbl_000";
			this.lbl_000.Size = new System.Drawing.Size(32, 18);
			this.lbl_000.TabIndex = 0;
			this.lbl_000.Text = "000";
			this.lbl_000.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_300
			// 
			this.lbl_300.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_300.Location = new System.Drawing.Point(472, 24);
			this.lbl_300.Name = "lbl_300";
			this.lbl_300.Size = new System.Drawing.Size(32, 18);
			this.lbl_300.TabIndex = 2;
			this.lbl_300.Text = "300";
			this.lbl_300.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_200
			// 
			this.lbl_200.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_200.Location = new System.Drawing.Point(472, 64);
			this.lbl_200.Name = "lbl_200";
			this.lbl_200.Size = new System.Drawing.Size(32, 18);
			this.lbl_200.TabIndex = 3;
			this.lbl_200.Text = "200";
			this.lbl_200.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_100
			// 
			this.lbl_100.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_100.Location = new System.Drawing.Point(472, 104);
			this.lbl_100.Name = "lbl_100";
			this.lbl_100.Size = new System.Drawing.Size(32, 18);
			this.lbl_100.TabIndex = 4;
			this.lbl_100.Text = "100";
			this.lbl_100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Graphic
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.lbl_100);
			this.Controls.Add(this.lbl_200);
			this.Controls.Add(this.lbl_300);
			this.Controls.Add(this.lbl_000);
			this.Controls.Add(this.pnl_back);
			this.Name = "Graphic";
			this.Size = new System.Drawing.Size(544, 184);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.SpeedGraphic_Layout);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Grid painting.
		/// </summary>
		private void SpeedGraphic_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			//VERTICAL GRID LINES
			//first time create labels and set property that won't change on resizing
			if (verticalLabels == null)
			{
				verticalLabels = new Label[hours + 1];
				for (int i = 0; i < verticalLabels.Length; i++)
				{
					verticalLabels[i] = new Label();
					verticalLabels[i].BackColor = GRID_COLOR;
					verticalLabels[i].Width = (i == 0 || i == verticalLabels.Length - 1) 
						? GRID_WIDTH * 2
						: GRID_WIDTH ;
					this.Controls.Add(verticalLabels[i]);
					verticalLabels[i].BringToFront();
				}
			}
			//every time
			double Xcoord = 0;
			for (int i = 0; i < verticalLabels.Length; i++)
			{
				verticalLabels[i].Height = this.Height - Convert.ToInt32(LOW_BORDER_WIDTH * 0.75)/* - 1*/;
				verticalLabels[i].Location = new Point(
					Convert.ToInt32(Math.Ceiling(Xcoord)), 0);
				Xcoord += (this.Width - RIGHT_BORDER_WIDTH) / hours;	
			}

			//HORIZONTAL GRID LINES AND THEYR VALUES
			//first time create labels and set property that won't change on resizing
			int horizontalLines = 8;
			if (horizontalLabels == null)
			{
				horizontalLabels = new Label[horizontalLines];
				for (int i = 0; i < horizontalLabels.Length; i++)
				{
					horizontalLabels[i] = new Label();
					horizontalLabels[i].BackColor = GRID_COLOR;
					
					horizontalLabels[i].Height = (i == 0 || i == horizontalLines - 1)
						? GRID_WIDTH * 2
						: GRID_WIDTH;
					this.Controls.Add(horizontalLabels[i]);
					horizontalLabels[i].BringToFront();
				}
			}
			//every time
			int Ycoord = 0;
			for (int i = 0; i < horizontalLabels.Length; i++)
			{
				horizontalLabels[i].Width = this.Width - Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75) - 1;
				horizontalLabels[i].Location = new Point(0, Ycoord);

				switch (i)
				{	
					case 0:
						lbl_300.Width = Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75);
						lbl_300.Location = new Point(
							this.Width - Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75), Convert.ToInt32(Ycoord + (lbl_300.Height / 2)) - GRID_WIDTH);
						break;
					case 2:
						lbl_200.Width = Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75);
						lbl_200.Location = new Point(
							this.Width - Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75), Convert.ToInt32(Ycoord + (lbl_200.Height / 2)) - GRID_WIDTH);
						break;
					case 4:
						lbl_100.Width = Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75);
						lbl_100.Location = new Point(
							this.Width - Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75), Convert.ToInt32(Ycoord + (lbl_100.Height / 2)) - GRID_WIDTH);
						break;
					case 6:
						lbl_000.Width = Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75);
						lbl_000.Location = new Point(
							this.Width - Convert.ToInt32(RIGHT_BORDER_WIDTH * 0.75), Convert.ToInt32(Ycoord + (lbl_000.Height / 2)) - GRID_WIDTH);
						break;
				}
				Ycoord += Convert.ToInt32((this.Height - LOW_BORDER_WIDTH) / (horizontalLines - 1));	
			}

			//BACK PANEL
			pnl_back.Location = new Point(GRID_WIDTH * 2, GRID_WIDTH * 2);
			pnl_back.Size = new Size(
				verticalLabels[verticalLabels.Length - 1].Location.X - GRID_WIDTH * 2,
				horizontalLabels[horizontalLabels.Length - 1].Location.Y - GRID_WIDTH * 2);
		}

		/// <summary>
		/// Values painting according to the GraphStyle.
		/// </summary>
		private void pnl_back_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int i = 0;
			int x = 0;
			int y = 0;
			int shiftChanges = 0;

			//graphic objects
			Graphics g = e.Graphics;
			g.Clip = new Region(new Rectangle(new Point(0, 0), new Size(pnl_back.Width + (GRID_WIDTH * 3), pnl_back.Height)));///mettere il * 4
			g.PageUnit = GraphicsUnit.Pixel;

			//points calculations
			double spacing = g.ClipBounds.Width / length;
			object[] elementsArray = elements.ToArray();
			int offset = elementsArray.Length - length;
			Point[] points = new Point[elementsArray.Length + 2];
			for (i = elementsArray.Length - 1; i >= 0; i--)
			{
				if ((int)elementsArray[i] != -1)
				{
					x = Convert.ToInt32(Math.Ceiling((i - offset) * spacing));
					y = (int)g.ClipBounds.Height - Convert.ToInt32(g.ClipBounds.Height * Math.Abs((int)elementsArray[i]) / MAX_ELEMENT_VALUE);
					points[i + shiftChanges + 2] = new Point(x, y);
				}
				else
				{
					shiftChanges++;
				}
			}
			points[shiftChanges + 1] = new Point(x, (int)g.ClipBounds.Height);
			while (shiftChanges >= 0)
			{
				points[shiftChanges] = new Point(points[points.Length - 1].X, (int)g.ClipBounds.Height);
				shiftChanges--;
			}

			//Draw graph border & filling
			g.DrawLines(new Pen(new SolidBrush(LINES_COLOR), LINES_WIDTH), points);
			g.FillPolygon(new SolidBrush(FILLING_COLOR), points);

			//Draw shift changes & past shift average speed
			SolidBrush shiftBrush = new SolidBrush(ColConv.Convert(SyncroColor.NoStandardDownTimeReasonColor));
			Pen separatorsPen = new Pen(shiftBrush, SHIFT_SEPARATOR_WIDTH);
			Font shiftFont = new Font(lbl_000.Font.FontFamily, 12F, FontStyle.Bold);
			SizeF shiftStringSize;
			RectangleF rectangle;
			int firstShiftChangeXCoord = 0;
			int shiftChangeAlreadyDrawed = 0;
			for (i = elementsArray.Length - 1; i >= 0; i--)
			{
				if ((int)elementsArray[i] == -1)
				{//shift change
					if (shiftChangeAlreadyDrawed < MAX_SHIFT_CHANGE_VISIBLE)
					{
						x = Convert.ToInt32(Math.Ceiling((i - offset + shiftChangeAlreadyDrawed) * spacing));
						String shiftString = pastShiftAverageSpeeds[shiftChangeAlreadyDrawed].ToString();
						shiftStringSize = g.MeasureString(shiftString, shiftFont);
						rectangle = new RectangleF(
							new PointF(x - (shiftStringSize.Width + SHIFT_STRING_DISTANCE_FROM_RIGHT), SHIFT_STRING_DISTANCE_FROM_TOP),
							shiftStringSize);

            //ABELLO_FI27/11/2010_1 il calcolo delle velocità medie sul grafico della homeDRY non vanno, quindi le ho tolte
            //if (rectangle.Location.X > 0) //draw string only if there is enough space
            //  g.DrawString(shiftString, shiftFont, shiftBrush, rectangle); 
						
            g.DrawLine(separatorsPen,
							new Point(x, 0),
							new Point(x, (int)g.ClipBounds.Height));

						shiftChangeAlreadyDrawed++;
						if (shiftChangeAlreadyDrawed == 1)
						{//first shift change, save coordinates
							firstShiftChangeXCoord = x;
						}
					}
					else
					{
						Logger.LogError(this, "pnl_back_Paint - too many shift change to be showed on the graphic, the maximum number of representable shift change is " + MAX_SHIFT_CHANGE_VISIBLE);
						break;
					}
				}
			}

      ////Draw current shift average speed
      shiftStringSize = g.MeasureString(currentShiftAverageSpeed.ToString(), shiftFont);
      x = Convert.ToInt32(verticalLabels[verticalLabels.Length - 1].Location.X - (shiftStringSize.Width + SHIFT_STRING_DISTANCE_FROM_RIGHT));
      if (x > firstShiftChangeXCoord)//draw string only if there is enough space
      {
        rectangle = new RectangleF(
          new PointF(x, SHIFT_STRING_DISTANCE_FROM_TOP),
          shiftStringSize);

        g.DrawString(currentShiftAverageSpeed.ToString(), shiftFont, shiftBrush, rectangle);
      }
		}

		/// <summary>
		///	Resize the internal component according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			//TODO ...
		}
	}
}
