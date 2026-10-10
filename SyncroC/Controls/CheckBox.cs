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
	/// <summary>
	/// CheckBox Styles.
	/// </summary>
	public enum CheckBoxStyles
	{
		/// <summary>no frame, no underline.</summary>
		Normal,
		/// <summary>title softly underlined.</summary>
		Light,
		/// <summary>without title</summary>
		NoTitle
	}
		
	/// <summary>
	/// Styles for the false value
	/// </summary>
	public enum CheckBoxFalseStyles
	{
		/// <summary>no symbols</summary>
		Empty,
		/// <summary>a red X</summary>
		RedX
	}
	
	/// <summary>
	/// Summary description for CheckBox.
	/// </summary>
	public class CheckBox : SyncroC.Controls.SyncroUserControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//Constants
		/// <summary> Frame width for Normal style</summary>
		private const int NORMAL_FRAMEWIDTH = 4;
		/// <summary> Frame width for Light style</summary>
		private const int LIGTH_FRAMEWIDTH = 1;
		/// <summary> Space between the check simbol and the component border.</summary>
		private const int SPACING = 3;

		/// <summary>Frame color when component is checked.</summary>
		private Color frameColorChecked = ColConv.Convert(SyncroColor.Black);

		/// <summary>Frame color when component is unchecked.</summary>
    private Color frameColorUnchecked = ColConv.Convert(SyncroColor.Black);

		/// <summary>Check color when component is checked.</summary>
		private Color checkColor = Color.FromArgb(0, 128, 0);
		
		/// <summary>Uncheck color when component is unchecked.</summary>
		private Color uncheckColor = Color.FromArgb(226, 10, 22);

		/// <summary>Current height of the lbl_Title label.</summary>
		private int titleHeight = 20;

		/// <summary>Width of the frame.</summary>
		private int frameWidth = NORMAL_FRAMEWIDTH;

		private Font titleFont = new System.Drawing.Font("Arial Unicode MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    /// <summary>Image displayed as icon into the tab.</summary>
    private Image imagePassword;
    /// <summary>Gets or sets the optional image displayed as icon into the tab.</summary>
    [Description("Gets or sets the image displayed as icon into the checkBox for identify when the data is under password."), Category("FosberSoft")]
    public Image ImagePassword
    {
      get { return imagePassword; }
      set
      {
        imagePassword = value;
        pct_icon.Image = ImagePassword;
        Invalidate();
      }
    }

		/// <summary>get or set the foreColor of the Title. </summary>
		private Color titleForeColor = ColConv.Convert(SyncroColor.Black);
		/// <summary>
		/// Gets or sets the foreColor of the Title.
		/// </summary>
		[Description("Gets or sets the foreColor of the Title."), Category("FosberSoft")]
		public SyncroColor TitleForeColor 
		{
			get	{	return ColConv.Convert(titleForeColor);	}
			set 
			{	
				titleForeColor = ColConv.Convert(value);				
				Invalidate();
			}
		}

		/// <summary>get or set the foreColor of the Title. </summary>
		private Color titleBackColor = Color.WhiteSmoke;
		/// <summary>
		/// Gets or sets the BackColor of the Title.
		/// </summary>
		[Description("Gets or sets the BackColor of the Title."), Category("FosberSoft")]
		public Color TitleBackColor
		{
			get { return this.titleBackColor; }
			set
			{
				this.BackColor = value;
				this.titleBackColor = value;
				Invalidate();
			}
		}

		/// <summary>Get or set the style of the checkbox.</summary>
		private CheckBoxStyles style = CheckBoxStyles.Normal;
		/// <summary>
		/// Gets or sets the style of the checkbox.
		/// </summary>
		[Description("Gets or sets the style of the checkbox."), Category("FosberSoft")]
		public CheckBoxStyles Style
		{
			get { return style; }
			set
			{
				style = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the text to be written into the control.
		/// </summary>
		[Description("Text showed when property ShowText is true."), Browsable(true),
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

		private bool firstActiveSet = true; 
		/// <summary>Determine if the checkBox is checked or not.</summary>
		private bool myChecked = true;
		/// <summary>
		/// Determine if the checkBox is checked or not.
		/// </summary>
		[Description("Determine if the checkBox is checked or not."), Category("FosberSoft")]
		public bool Checked
		{
			get {	return myChecked;	}
			set 
			{
				if (Checked != value || firstActiveSet)
				{
					firstActiveSet = false;
					myChecked = value;
					if (OnCheckedChanged != null)
						OnCheckedChanged(this, new System.EventArgs());
					Invalidate();
				}
			}
		}

		/// <summary>Determine if the checkBox is readonly.</summary>
		private bool readOnly = false;
		/// <summary>
		/// Determine if the checkBox is readonly.
		/// </summary>
		[Description("Determine if the checkBox is readonly."), Category("FosberSoft")]
		public bool ReadOnly
		{
			get {	return readOnly;	}
			set 
			{	
				readOnly = value;	
				Invalidate();
			}
		}
		
		/// <summary>Determine the style to be used for the false value into the chekbox.</summary>
		private CheckBoxFalseStyles falseStyle = CheckBoxFalseStyles.Empty;
		/// <summary>
		/// Determine the style to be used for the false value into the chekbox.
		/// </summary>
		[Description("Determine the style to be used for the false value into the chekbox."), Category("FosberSoft")]
		public CheckBoxFalseStyles FalseStyle
		{
			get {	return falseStyle;	}
			set 
			{	
				falseStyle = value;	
				Invalidate();
			}
		}

		/// <summary>
		/// Event raised when the "Checked" property changes. 
		/// </summary>
		[Description("Event raised when the \"Checked\" property changes."), Category("Fosbersoft")]
		public event EventHandler OnCheckedChanged;

		/// <summary>
		/// Builder
		/// </summary>
		public CheckBox()
		{
			InitializeComponent();

			this.GotFocus += new EventHandler(CheckBox_GotFocus);
			this.LostFocus += new EventHandler(CheckBox_LostFocus);
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
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      this.pct_icon = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit();
      this.SuspendLayout();

      // 
      // pct_icon
      // 
      this.pct_icon.Location = new System.Drawing.Point(2, 2);
      this.pct_icon.Name = "pct_icon";
      this.pct_icon.Size = new System.Drawing.Size(20, 20);
      this.pct_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_icon.TabIndex = 0;
      this.pct_icon.TabStop = false;
      this.pct_icon.Visible = false;
      
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      this.Controls.Add(this.pct_icon);

			// 
			// CheckBox
			// 
      this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "CheckBox";
			this.Size = new System.Drawing.Size(56, 48);
			this.Click += new System.EventHandler(this.CheckBox_Click);
			this.SizeChanged += new System.EventHandler(this.CheckBox_SizeChanged);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CheckBox_KeyUp);

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();
      this.ResumeLayout(false);
		}
		#endregion

    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private void ArrangeComponents()
    {
      pct_icon.Location = new Point(this.Width - pct_icon.Width, this.titleHeight + 3);
    }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			
			//Grphics objects
			Graphics g = e.Graphics;

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Visible = (imagePassword != null);

			switch(style)
			{
				case CheckBoxStyles.Normal:
					//frame drawing
					g.FillPolygon(
						new SolidBrush((myChecked) ? frameColorChecked : frameColorUnchecked),
						new Point[]{ 
									   new Point(0, 0),
									   new Point(this.Width, 0),
									   new Point(this.Width, titleHeight),
									   new Point(frameWidth, titleHeight),
									   new Point(frameWidth, this.Height),
									   new Point(0, this.Height)});
					break;
				case CheckBoxStyles.Light:
					//frame drawing
					g.DrawLines(
						new Pen(new SolidBrush((myChecked) ? frameColorChecked : frameColorUnchecked),LIGTH_FRAMEWIDTH),
						new Point[]{ 
									   new Point(0, this.Height),
									   new Point(0, titleHeight),
									   new Point(this.Width, titleHeight)});
					//titleBackColor drawing
					g.FillPolygon(
						new SolidBrush(this.titleBackColor),
						new Point[]{ 
									   new Point(0, 0),
									   new Point(this.Width, 0),
									   new Point(this.Width, titleHeight),
									   new Point(0, titleHeight)});
					break;
				case CheckBoxStyles.NoTitle:
					//frame drawing
					g.DrawLines(
						new Pen(new SolidBrush((myChecked) ? frameColorChecked : frameColorUnchecked),LIGTH_FRAMEWIDTH),
						new Point[]{ 
									   new Point(0, this.Height),
									   new Point(0, 0),
									   new Point(this.Width, 0)});
					break;
			}

			switch(style)
			{
				case CheckBoxStyles.Light:
				case CheckBoxStyles.Normal:
					//draw string title
					int dist =	Convert.ToInt32((titleHeight - this.titleFont.Height) / 2);//distance of the text from the label border
					AdaptStringForComponentSize(g, this, this.Width - dist, titleFont);
					g.DrawString(this.Text, this.titleFont, new SolidBrush(titleForeColor),
						new Rectangle(dist, dist, this.Width - dist, titleHeight - dist));

					//draw value 
					if (this.myChecked)
						drawCheck(
							Convert.ToInt32(frameWidth + SPACING * 3),	//x
							titleHeight + SPACING, //y
							Convert.ToInt32(this.Height - titleHeight - SPACING * 2), //side
							g);
					else
						drawUncheck(
						Convert.ToInt32(frameWidth + SPACING * 3),	//x
							titleHeight + SPACING, //y
							Convert.ToInt32(this.Height - titleHeight - SPACING * 2), //side
							falseStyle, //style
							g);		
					//draw little square
					if (! readOnly)
					{
						//height of the little square (60% of the font height)
						int side = Convert.ToInt32((this.titleFont.Height * 60) / 100);
						//distance of the little square from the label border
						int space =	Convert.ToInt32((titleHeight - side) / 2);
						g.FillRectangle(
							new SolidBrush(titleForeColor),
							new Rectangle(this.Width - SPACING - side, SPACING, side, side));
					}

					//draw the focus 
					if (this.Focused)
					{
						int offset = 1;
						g.DrawLines(
							new Pen(new SolidBrush(FOCUS_COLOR), 1),
							new Point[]{
										   new Point(frameWidth + offset, titleHeight + offset),
										   new Point(this.Width - offset, titleHeight + offset),
										   new Point(this.Width - offset, this.Height - offset),
										   new Point(frameWidth + offset, this.Height - offset),
										   new Point(frameWidth + offset, titleHeight + offset)});
					}
					break;
				case CheckBoxStyles.NoTitle:
					//draw value 
					if (this.myChecked)
						drawCheck(
							Convert.ToInt32(frameWidth + SPACING * 3),	//x
							SPACING, //y
							Convert.ToInt32(this.Height - SPACING * 2), //side
							g);
					else
						drawUncheck(
							Convert.ToInt32(frameWidth + SPACING * 3),	//x
							SPACING, //y
							Convert.ToInt32(this.Height - SPACING * 2), //side
							falseStyle, //style
							g);

					//draw little square
					if (! readOnly)
					{
						//height of the little square (60% of the font height)
						int side = Convert.ToInt32((this.titleFont.Height * 40) / 100);
						//distance of the little square from the label border
						int space =	Convert.ToInt32(side / 2);
						g.FillRectangle(
							new SolidBrush(titleForeColor),
							new Rectangle(this.Width - SPACING - side, SPACING, side, side));
					}

					//draw the focus 
					if (this.Focused)
					{
						int offset = 1;
						g.DrawLines(
							new Pen(new SolidBrush(FOCUS_COLOR), 1),
							new Point[]{
										   new Point(frameWidth + offset, offset),
										   new Point(this.Width - offset, offset),
										   new Point(this.Width - offset, this.Height - offset),
										   new Point(frameWidth + offset, this.Height - offset),
										   new Point(frameWidth + offset, offset)});
					}
					break;
			}
		}

		/// <summary>
		/// Draw the check simbol into the component.
		/// </summary>
		/// <param name="x">High left corner X of the sqaured area where the check simbol should be drawn.</param>
		/// <param name="y">High left corner Y of the sqaured area where the check simbol should be drawn.</param>
		/// <param name="side">Length of the side of the square where to draw the check simbol.</param>
		/// <param name="g">Graphics objet to use for drawing.</param>
		private void drawCheck(int x, int y, int side, Graphics g)
		{
			g.FillPolygon(
				new SolidBrush(checkColor),
				new Point[]{
										 new Point(x,															Convert.ToInt32(y + side / 3)),
										 new Point(x,															Convert.ToInt32(y + side / 3 * 2)),
										 new Point(Convert.ToInt32(x + side / 3),	y + side),
										 new Point(x + side,											Convert.ToInt32(y + side / 3)),
										 new Point(x + side,											y),
										 new Point(Convert.ToInt32(x + side / 3),	Convert.ToInt32(y + side / 3 * 2)),
										 new Point(x,															Convert.ToInt32(y + side / 3))
									 });
		}
		
		/// <summary>
		/// Draw the uncheck simbol into the component.
		/// </summary>
		/// <param name="x">High left corner X of the sqaured area where the uncheck simbol should be drawn.</param>
		/// <param name="y">High left corner Y of the sqaured area where the uncheck simbol should be drawn.</param>
		/// <param name="side">Length of the side of the square where to draw the uncheck simbol.</param>
		/// <param name="checkBoxFalseStyles">Style to use for the uncheck.</param>
		/// <param name="g">Graphics objet to use for drawing.</param>
		private void drawUncheck(int x, int y, int side, CheckBoxFalseStyles checkBoxFalseStyles, Graphics g)
		{
			if (checkBoxFalseStyles == CheckBoxFalseStyles.RedX)
			{
				g.FillPolygon(
					new SolidBrush(uncheckColor),
					new Point[]{
											 new Point(Convert.ToInt32(x + side / 6)			,		y																	),	//1
											 new Point(Convert.ToInt32(x + side / 6 * 3)	,		Convert.ToInt32(y + side / 6 * 2)	),	//2
											 new Point(Convert.ToInt32(x + side / 6 * 5)	,		y																	),	//3
											 new Point(x + side														,		Convert.ToInt32(y + side / 6)			),	//4
											 new Point(Convert.ToInt32(x + side / 6 * 4)	,		Convert.ToInt32(y + side / 6 * 3)	),	//5
											 new Point(x + side														,		Convert.ToInt32(y + side / 6 * 5)	),	//6
											 new Point(Convert.ToInt32(x + side / 6 * 5)	,		y + side													),	//7
											 new Point(Convert.ToInt32(x + side / 6 * 3)	,		Convert.ToInt32(y + side / 6 * 4)	),	//8
											 new Point(Convert.ToInt32(x + side / 6)			,		y + side													),	//9
											 new Point(x																	,		Convert.ToInt32(y + side / 6 * 5)	),	//10
											 new Point(Convert.ToInt32(x + side / 6 * 2)	,		Convert.ToInt32(y + side / 6 * 3)	),	//11
											 new Point(x																	,		Convert.ToInt32(y + side / 6)			)		//12
										});
			}
		}

		private void CheckBox_SizeChanged(object sender, System.EventArgs e)
		{
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ArrangeComponents();

      Invalidate();
		}
		/// <summary>
		/// Resize the internal component according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (! (VerticalResizing == 1 && OrizontalResizing == 1 && FontResizing == 1) )
			{
				titleFont = new Font(titleFont.FontFamily, 
					Convert.ToInt32(titleFont.Size * FontResizing), titleFont.Style);
				frameWidth = Convert.ToInt32(frameWidth * OrizontalResizing);
				titleHeight = Convert.ToInt32(titleHeight * VerticalResizing);
				Invalidate();
			}
		}

		private void CheckBox_Click(object sender, System.EventArgs e)
		{
			if (! readOnly)
				Checked = ! Checked;
		}


		private void CheckBox_GotFocus(Object sender, System.EventArgs e)
		{
			Invalidate();
		}
		
		private void CheckBox_LostFocus(Object sender, System.EventArgs e)
		{
			Invalidate();
		}

		/// <summary>
		/// When the component is focused the check changes on space up
		/// </summary>
		private void CheckBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (this.Focused && ! readOnly && e.KeyCode.Equals(Keys.Space) && ! (e.Control || e.Alt || e.Shift))
				Checked = ! Checked;
		}
	}
}
