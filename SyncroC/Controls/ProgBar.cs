using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;

namespace SyncroC.Controls
{
	/// <summary>
	///	Possible styles of the ProgBar component.
	/// </summary>
	public enum ProgBarStyles : int
	{
		/// <summary>40 pixels height.</summary>
		VeryBig,
		/// <summary>24 pixels height.</summary>
		Big,
		/// <summary>20 pixels height.</summary>
		Small,
		/// <summary> pixels height.</summary>
		VerySmall
	}

  public enum BarStyles
  {
    Classic, // same as ProgressBar
    Solid
  }


	/// <summary>
	/// Summary description for ProgBar.
	/// </summary>
	public class ProgBar : SyncroC.Controls.SyncroUserControl
	{
		// Required designer variable.
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lbl_Percentage;

		//Constants		
		/// <summary> Hight of the component for Big style</summary>
		private const int HEIGHT_VERYBIG = 40;
		/// <summary> Hight of the component for Big style</summary>
		private const int HEIGHT_BIG = 24;
		/// <summary> Hight of the component for Small style</summary>
		private const int HEIGHT_SMALL = 20;
		/// <summary> Hight of the component for Small style</summary>
		private const int HEIGHT_VERYSMALL = 12;

		/// <summary>Current style of the component.</summary>
		private ProgBarStyles style = ProgBarStyles.Big;
		/// <summary>
		/// Gets or sets the component's style.
		/// </summary>
		[Description("Gets or sets the component's style."), Category("FosberSoft")]
		public ProgBarStyles Style
		{
			get {	return style;	}
			set
			{ 
				style = value; 
				switch (style)
				{
					case ProgBarStyles.VeryBig:
						this.Height = HEIGHT_VERYBIG;
						this.lbl_Percentage.Height = HEIGHT_VERYBIG;
						break;
					case ProgBarStyles.Big:
						this.Height = HEIGHT_BIG;
						this.lbl_Percentage.Height = HEIGHT_BIG; 
						break;
					case ProgBarStyles.Small:
						this.Height = HEIGHT_SMALL;
						this.lbl_Percentage.Height = HEIGHT_SMALL;
						break;
					case ProgBarStyles.VerySmall:
						this.Height = HEIGHT_VERYSMALL;
						this.lbl_Percentage.Height = HEIGHT_VERYSMALL;
						break;
				}
				Invalidate();
			}
		}

    /// <summary>Current style of the component.</summary>
    private BarStyles barStyle = BarStyles.Classic;
    /// <summary>
    /// Gets or sets the component's style.
    /// </summary>
    [Description("Gets or sets the component's bar style."), Category("FosberSoft")]
    public BarStyles BarStyle
    {
      get
      {
        return barStyle;
      }
      set
      {
        barStyle = value;
        Invalidate();
      }
    }

		/// <summary>
		/// Gets or sets the color of the ProgBar component.
		/// </summary>
		[Description("Gets or sets the color of the ProgBar component."), Category("FosberSoft")]
		public SyncroColor color
		{
			get	{	return ColConv.Convert(this.BackColor);	}
			set	
			{	
				this.BackColor = ColConv.Convert(value);
				this.lbl_Percentage.BackColor = ColConv.Convert(value);
        this.lbl_Percentage.ForeColor = this.ForeColor;
			}
		}

		/// <summary>
		/// Gets or sets the visibility of the percentage.
		/// </summary>
		[Description("Gets or sets the visibility of the percentage."), Category("FosberSoft")]
		public bool PercentageVisible
		{
			get { return lbl_Percentage.Visible; }
			set 
			{
				lbl_Percentage.Visible = value;
				Invalidate();
			}
		}

		private bool firstActiveSet = true; 
		/// <summary>current percentage of progress.</summary>
		private int percentage;
		/// <summary>
		/// Gets or sets the percentage progress.
		/// </summary>
		[Description("Gets or sets the percentage progress."), Category("FosberSoft")]
		public int Percentage
		{
			get {	return percentage;		}
			set 
			{
				string thePercentageText = this.lbl_Percentage.Text;
				if (value >= 0)
				{
					this.firstActiveSet = false;
					this.percentage = value;
					this.lbl_Percentage.Text = this.percentage.ToString() + " %";
					this.lbl_Percentage.ForeColor = (value > 100)
						? ColConv.Convert(SyncroColor.OverRunColor)
						: this.ForeColor;
				}
				else
					lbl_Percentage.Text = "---";

				if ((this.lbl_Percentage.Text != thePercentageText) || firstActiveSet)
				{
					this.firstActiveSet = false;
					Invalidate();
				}
			}
		}

		public ProgBar()
		{
			InitializeComponent();
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
			this.lbl_Percentage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_Percentage
			// 
			this.lbl_Percentage.Location = new System.Drawing.Point(129, 0);
			this.lbl_Percentage.Name = "lbl_Percentage";
			this.lbl_Percentage.Size = new System.Drawing.Size(71, 24);
			this.lbl_Percentage.TabIndex = 0;
			this.lbl_Percentage.Text = "100 %";
			this.lbl_Percentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProgBar
			// 
			this.Controls.Add(this.lbl_Percentage);
			this.Name = "ProgBar";
			this.Size = new System.Drawing.Size(200, 24);
			this.Resize += new System.EventHandler(this.ProgBar_Resize);
			this.ForeColorChanged += new System.EventHandler(this.ProgBar_ForeColorChanged);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			//distance between lines and lbl_percentage.
			int offset = 8/*12*/;
			//space between the lines.
			int spacing = 4;
			//available space for progress bar drawing.
			int totalSpace = this.Width - (2 * offset);
			if (lbl_Percentage.Visible)
				totalSpace -= (lbl_Percentage.Width);
			//pen for first and last lines
			Pen pen = new Pen(new SolidBrush(this.ForeColor), 2);
			
			//draw 0% and 100% lines
			g.DrawLine(pen,
				new Point(offset - 2, 0),
				new Point(offset - 2, this.Height));
			g.DrawLine(pen,
				new Point(offset + totalSpace + 2, 0),
				new Point(offset + totalSpace + 2, this.Height));
			
			//space to be filled with lines.
			int percentageSpace = Convert.ToInt32((totalSpace * Math.Min(percentage, 100)) / 100);
			if (percentageSpace != 0 && percentageSpace <= spacing)
				percentageSpace = spacing;
			int linesHeight = this.Font.Height - 4;
			//pen for filling lines

      if (this.barStyle == BarStyles.Classic)
      {
        pen = new Pen(new SolidBrush(this.ForeColor), 1);
      }
      else
      {
        pen = new Pen(new SolidBrush(this.ForeColor), spacing);
      }
			
      int y = Convert.ToInt32((this.Height - linesHeight) / 2);
			int x = spacing;
			while(x <= percentageSpace)
			{
				g.DrawLine(pen, 
					new Point(x + offset, y),//high
					new Point(x + offset, y + linesHeight)); //low
				x += spacing;
			}
		}

		/// <summary>
		/// Resize the internal component according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (! (FontResizing == 1 && VerticalResizing == 1))
			{
				Font = new Font(Font.FontFamily, 
					Convert.ToInt32(Font.Size * FontResizing), Font.Style);
				Invalidate();
			}
		}

		/// <summary>
		/// Keep the percentage string on the same position when component resizes.
		/// </summary>
		private void ProgBar_Resize(object sender, System.EventArgs e)
		{
			lbl_Percentage.Location = new Point(this.Width - lbl_Percentage.Width, lbl_Percentage.Location.Y);
			lbl_Percentage.Height = this.Height;
			Invalidate();
		}

		private void ProgBar_ForeColorChanged(object sender, EventArgs e)
		{
			lbl_Percentage.ForeColor = this.ForeColor;
		}
	}
}
