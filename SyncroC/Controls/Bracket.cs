using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;

namespace SyncroC.Controls
{
	public enum BracketPosition
	{
		/// <summary>the bracket is on the right side of the related grid</summary>
		Right,
		///// <summary>the bracket is on the left side of the related grid</summary>
		//Left,
		///// <summary>the bracket is on the top of the related grid</summary>
		//Top,
		/// <summary>the bracket is on the bottom of the related grid</summary>
		Bottom
	}

    /// <summary>
    /// Summary description for Title.
    /// </summary>
	public class Bracket : SyncroC.Controls.SyncroUserControl
	{
    private System.ComponentModel.Container components = null;

    //Constants		
    /// <summary> Hight of the title</summary>
    private const int TITLE_HEIGHT = 24;
    /// <summary> Width of the frame</summary>
    private const int FRAME_WIDTH = 6;
    /// <summary> Width of the arrow</summary>
    private const int ARROW_WIDTH = 18;
    /// <summary> Space beetween the arrow and the bracket</summary>
    private const int SPACING = 6;
    /// <summary>Font size</summary>
    private const float SIZE_FONT = 12F;

    /// <summary>
    /// Gets or sets the color of the Bracket component.
    /// </summary>
		[Description("Gets or sets the color of the Bracket component."), Category("FosberSoft")]
		public SyncroColor color
    {
        get { return ColConv.Convert(this.BackColor); }
        set { this.BackColor = ColConv.Convert(value); }
    }

    /// <summary>
    /// Gets or sets the ForeColor.
    /// </summary>
		[Description("Gets or sets the ForeColor."), Category("FosberSoft")]
		public SyncroColor titleForeColor
    {
        get { return ColConv.Convert(this.ForeColor); }
        set { this.ForeColor = ColConv.Convert(value); }
    }

		/// <summary>Get or set the position of the bracket.</summary>
		private BracketPosition position = BracketPosition.Right;
		/// <summary>
		/// Gets or sets the position of the bracket.
		/// </summary>
		[Description("Gets or sets the position of the bracket respect to the grid."), Category("FosberSoft")]
		public BracketPosition Position
		{
			get { return position; }
			set
			{
				position = value;
				Invalidate();
			}
		}

    private bool showArrow = true;
    /// <summary>
    /// Gets or sets a value indicating whether a pointing arrows is visible.
    /// </summary>
		[Description("Gets or sets a value indicating whether a pointing arrows is visible."), Category("FosberSoft")]
		public bool ShowArrow
    {
        get { return showArrow; }
        set
        {
            showArrow = value;
            Invalidate();
        }
    }

		/// <summary>
		/// Determines the position of the arrow.
		/// The value must be a percentage (0.00 means top of the bracket; 1.00 means bottom of the bracket)
		/// </summary>
		private float arrowPosition = 0.50F;

    /// <summary>Current height of the title.</summary>
    private int titleHeight = TITLE_HEIGHT;

    /// <summary>Current width of the frame.</summary>
    private int frameWidth = FRAME_WIDTH;

    /// <summary>Current size of the font.</summary>
    private float currentFontSize = SIZE_FONT;

    /// <summary>
    /// Gets or sets the text to be written into the control as a title.
    /// </summary>
		[Description("Gets or sets the text to be written into the control as a title."), Browsable(true),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
			Category("FosberSoft")]
		public override string Text
    {
			get { return base.Text; }
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

    /// <summary>
    /// Builder.
    /// </summary>
    public Bracket()
    {
      InitializeComponent();
    }

		/// <summary>
		/// Change the position of the arrow to let it point from a title which is next to the bracket
		/// </summary>
		/// <param name="nextTitle">the close title the arrow should point from</param>
		public void PositArrowToNextTitle(Title nextTitle)
		{
			float posit = (nextTitle.Location.Y - this.Location.Y + nextTitle.getCurrentTitleHeight() + //vertical offset of the area to center arrow to
				((nextTitle.Height - nextTitle.getCurrentTitleHeight()) / 2.0F));  //middle of the area to center arrow to
			if (posit < ARROW_WIDTH)
				posit = ARROW_WIDTH;
			else if (posit > this.Height - ARROW_WIDTH)
				posit = this.Height - ARROW_WIDTH;
			arrowPosition = posit / this.Height; // convert to percentage 
			Invalidate();
		}

		/// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
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
        this.SuspendLayout();

        // 
        // Title
        // 
        this.BackColor = System.Drawing.Color.Black;
        this.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        this.ForeColor = System.Drawing.Color.WhiteSmoke;
        this.Name = "Title";
        this.Size = new System.Drawing.Size(104, 64);
        this.Load += new System.EventHandler(this.Title_Load);
        this.SizeChanged += new System.EventHandler(this.Title_SizeChanged);
        this.BackColorChanged += new System.EventHandler(this.Title_BackColorChanged);
        this.ResumeLayout(false);

    }
    #endregion

    private void Title_Load(object sender, System.EventArgs e)
    {
      SetUpSizes();
    }

    private void Title_SizeChanged(object sender, System.EventArgs e)
    {
      SetUpSizes();
    }

    private void Title_BackColorChanged(object sender, System.EventArgs e)
    {
      Invalidate();
    }

    /// <summary>
    /// Resize the inside components according to the parameters.
    /// </summary>
    /// <param name="VerticalResizing">coefficient for vertical resizing.</param>
    /// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
    /// <param name="FontResizing">coefficient for font resizing.</param>
    public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
    {
      if (!(VerticalResizing == 1 && FontResizing == 1))
      {
        currentFontSize *= FontResizing;
        titleHeight = Convert.ToInt32(titleHeight * VerticalResizing);
        SetUpSizes();
      }
    }

    /// <summary>
    /// Set the size of the label according to the new component size.
    /// Assign a new region to the component according to the new size.
    /// </summary>
    private void SetUpSizes()
    {
      //set region
      GraphicsPath gp = new GraphicsPath();
      Point[] points = null;
			switch (position)
			{
				case BracketPosition.Right:
					if (showArrow)
					{
						points = new Point[]{
							new Point(ARROW_WIDTH + SPACING, 0),																			//1
							new Point(this.Width, 0),																								//2
							new Point(this.Width, titleHeight),																			//3
							new Point(ARROW_WIDTH + SPACING + frameWidth, titleHeight),							//4
							new Point(ARROW_WIDTH + SPACING + frameWidth, this.Height - titleHeight),//5
							new Point(this.Width, this.Height - titleHeight),												//6
							new Point(this.Width, this.Height),																			//7
							new Point(ARROW_WIDTH + SPACING, this.Height),														//8
							new Point(ARROW_WIDTH + SPACING, Convert.ToInt32((this.Height * arrowPosition))),		//9
							new Point(ARROW_WIDTH, Convert.ToInt32((this.Height * arrowPosition))),							//10
							new Point(0, Convert.ToInt32((this.Height * arrowPosition) + ARROW_WIDTH)),					//11
							new Point(0, Convert.ToInt32((this.Height * arrowPosition) - ARROW_WIDTH)),					//12
							new Point(ARROW_WIDTH, Convert.ToInt32((this.Height * arrowPosition))),							//13
							new Point(ARROW_WIDTH + SPACING, Convert.ToInt32((this.Height * arrowPosition)))			//14
							};
					}
					else
					{
						points = new Point[]{
							new Point(0, 0),																	//1
							new Point(this.Width, 0),												//2
							new Point(this.Width, titleHeight),							//3
							new Point(frameWidth, titleHeight),							//4
							new Point(frameWidth, this.Height - titleHeight),//5
							new Point(this.Width, this.Height - titleHeight),//6
							new Point(this.Width, this.Height),							//7
							new Point(0, this.Height)};											//8
					}
					break;

				case BracketPosition.Bottom:
					points = new Point[]{
							new Point(0, 0),																	//1
							new Point(this.Width, 0),												//2
							new Point(this.Width, this.Height),							//3
							new Point(this.Width - frameWidth, this.Height),//4
							new Point(this.Width - frameWidth, titleHeight),//5
							new Point(frameWidth, titleHeight),//6
							new Point(frameWidth, this.Height),							//7
							new Point(0, this.Height)};											//8
					break;
			}
      gp.AddPolygon(points);
      this.Region = new Region(gp);
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
      SetUpSizes();
      Rectangle TextRectangle;
			if (showArrow && !position.Equals(BracketPosition.Bottom))
          TextRectangle = new Rectangle(ARROW_WIDTH + SPACING + frameWidth, 0, this.Width - ARROW_WIDTH, titleHeight);
      else
          TextRectangle = new Rectangle(frameWidth, 0, this.Width, titleHeight);

      //text drawing 
      e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(ColConv.Convert(titleForeColor)),
            TextRectangle);
    }
  }
}
