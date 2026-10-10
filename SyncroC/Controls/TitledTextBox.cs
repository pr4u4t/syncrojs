using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;
using SyncroC.Utility;

namespace SyncroC.Controls
{
	/// <summary>
	/// TitledTextBox Styles.
	/// </summary>
	public enum TitledTextBoxStyles
	{
		/// <summary>no Title.</summary>
		NoTitle,
		/// <summary>no frame, no underline.</summary>
		Normal,
		/// <summary>title softly underlined.</summary>
		Light,
		/// <summary>title with frame.</summary>
		Framed
	}

	/// <summary>
	/// Summary description for TitledTextBox.
	/// </summary>
	public class TitledTextBox : SyncroC.Controls.SyncroUserControl, IValidableControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		private IContainer components;
		private System.Windows.Forms.TextBox txt_Value;
		private System.Windows.Forms.ErrorProvider errorProvider;

		//Constants
		/// <summary> Frame width for NoTitle style (1)</summary>
		private const int NOTITLE_FRAMEWIDTH = 1;
		/// <summary> Frame width for Normal style (4)</summary>
		private const int NORMAL_FRAMEWIDTH = 4;
		/// <summary> Frame width for Light style (1)</summary>
		private const int LIGTH_FRAMEWIDTH = 1;
		/// <summary> Frame width for Framed style (2)</summary>
		private const int FRAMED_FRAMEWIDTH = 2;
		/// <summary> Space between the check simbol and the component border.</summary>
		private const int SPACING = 3;

		/// <summary>Current font of the title.</summary>
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

		/// <summary>The style of the component.</summary>
		private TitledTextBoxStyles style;
		/// <summary>
		/// Gets or sets the component style.
		/// </summary>
		[Description("Gets or sets the component style."), Category("FosberSoft")]
		public TitledTextBoxStyles Style
		{
			get {	return style;	}
			set 
			{
				style = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Occurs when the value changes.
		/// </summary>
		[Category("Fosbersoft"), Description("Occurs when the value changes.")]
		public event EventHandler ValueChanged;

    /// <summary>
    /// Event raised when the control gets the focus.
    /// </summary>
    [Description("Event raised when the control gets the focus."), Category("Fosbersoft")]
    public new event EventHandler GotFocus;
    
    /// <summary>current color of the frame.</summary>
		private Color frameColor = ColConv.Convert(SyncroColor.Black);
		/// <summary>
		/// Gets or sets the color of the frame.
		/// </summary>
		[Description("Gets or sets the FrameColor."), Category("FosberSoft")]
		public SyncroColor FrameColor
		{
			get {	return ColConv.Convert(frameColor);	}
			set 
			{
				frameColor = ColConv.Convert(value);
				Invalidate();
			}
		}

		private Color titleForeColor = ColConv.Convert(SyncroColor.White);
		/// <summary>
		/// Gets or sets the foreColor of the Title.
		/// </summary>
		[Description("Gets or sets the foreColor of the Title."), Category("FosberSoft")]
		public SyncroColor TitleForeColor 
		{
			get { return ColConv.Convert(titleForeColor); }
			set 
			{ 
				titleForeColor = ColConv.Convert(value);
				Invalidate();
			}
		}

		private Color titleBackColor = ColConv.Convert(SyncroColor.Black);
		/// <summary>
		/// Gets or sets the title BackColor.
		/// </summary>
		[Description("Gets or sets the title BackColor."), Category("FosberSoft")]
		public SyncroColor TitleBackColor
		{
			get { return ColConv.Convert(titleBackColor); }
			set 
			{ 
				titleBackColor = ColConv.Convert(value);
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

		/// <summary>
		/// Gets or sets the text to be written into the control as a value.
		/// </summary>
		[Description("Gets or sets the text to be written into the control as a value."), Category("FosberSoft")]
		public string Value
		{
			get 
      {	
        string s=(txt_Value.Text.Equals(String.Empty) ? " " : txt_Value.Text);
        s = s.Trim();
        if (s.Equals(string.Empty)) s = " ";
        return s;
      }
			set 
			{	
				txt_Value.Text = value;
				if (this.Visible == false && !value.Equals(string.Empty))
				{
					try
					{
						this.Visible = true;
					}
					catch (Exception ex)
					{
            Console.WriteLine(ex.Message);
          }
				}
			}
		}

		/// <summary>
    /// Gets the text not trimmed.
		/// </summary>
    [Description("Gets the text not trimmed."), Category("FosberSoft")]
    public string NotTrimmedValue
    {
      get { return txt_Value.Text; }
    }

		/// <summary>
		/// Gets or sets the character used to mask characters of a password in a single-line SyncroC.Controls.TitledTextBox
		/// </summary>
		[Description("Gets or sets the character used to mask characters of a password in a single-line SyncroC.Controls.TitledTextBox"), Category("FosberSoft")]
		public char PasswordChar
		{
			get {	return txt_Value.PasswordChar;	}
			set {	txt_Value.PasswordChar = value;	}
		}

		/// <summary>
		/// Gets or sets the textbox BackColor.
		/// </summary>
		[Description("Gets or sets the textbox BackColor."), Category("FosberSoft")]
		public SyncroColor ValueBackColor 
		{
			get {	return ColConv.Convert(txt_Value.BackColor);	}
			set {	txt_Value.BackColor = ColConv.Convert(value);			}
		}

		/// <summary>
		/// Determine if the TitledTextBox is multiline.
		/// </summary>
		[Description("Determine if the TitledTextBox is multiline."), Category("FosberSoft")]
		public bool Multiline
		{
			get {	return txt_Value.Multiline;		}
			set 
			{	
				txt_Value.Multiline = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Determine if the control is readonly.
		/// </summary>
		[Description("Determine if the control is readonly."), Category("FosberSoft")]
		public bool ReadOnly
		{
			get {	return txt_Value.ReadOnly;	}
			set	
			{	
				txt_Value.ReadOnly = value;
        this.Cursor = this.txt_Value.ReadOnly ? Cursors.Arrow : Cursors.IBeam;
        this.txt_Value.Cursor = this.txt_Value.ReadOnly ? Cursors.Arrow : Cursors.IBeam;
				Invalidate();
			}
		}

    /// <summary>
    /// Return the selected text.
    /// </summary>
    [Description("Return the selected text."), Category("FosberSoft")]
    public string SelectedText
    {
      get { return txt_Value.SelectedText; }
    }

    /// <summary>
    /// Return the selected text lenght.
    /// </summary>
    [Description("Return the selected text lenght."), Category("FosberSoft")]
    public int SelectionLength
    {
      get { return txt_Value.SelectionLength; }
    }

    /// <summary>
		/// Gets or sets Max lenght of textbox.
		/// </summary>
		[Description("Gets or sets Max lenght of textbox."), Category("FosberSoft")]
		public int MaxChar
		{
			get {	return txt_Value.MaxLength;	}
			set {	txt_Value.MaxLength = value;}
		}

		/// <summary>
		/// Gets a value indicating whether the control has the input focus.
		/// </summary>
		public override bool Focused
		{
			get {	return (txt_Value.Focused);	}
		}

		/// <summary>Height of the title.</summary>
		private int titleHeight = 20;
		/// <summary>Width of the frame.</summary>
		private int frameWidth = NORMAL_FRAMEWIDTH;

		/// <summary>
		/// Builder.
		/// </summary>
		public TitledTextBox()
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

		/// <summary>
		/// Resize the inside components according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (! (VerticalResizing == 1 && FontResizing == 1))
			{
				titleFont = new Font(titleFont.FontFamily,
					Convert.ToInt32(titleFont.Size * FontResizing), titleFont.Style);
				this.Font = new Font(this.Font.FontFamily, 
					Convert.ToInt32(this.Font.Size * FontResizing), this.Font.Style);
				titleHeight = Convert.ToInt32(titleHeight * VerticalResizing);
				Invalidate();
			}	
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      this.pct_icon = new System.Windows.Forms.PictureBox();
      this.txt_Value = new System.Windows.Forms.TextBox();
      this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit();
      
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();

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
      // txt_Value
      // 
      this.txt_Value.AcceptsReturn = true;
      this.txt_Value.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txt_Value.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.errorProvider.SetIconAlignment(this.txt_Value, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
      this.txt_Value.Location = new System.Drawing.Point(2, 22);
      this.txt_Value.Name = "txt_Value";
      this.txt_Value.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txt_Value.Size = new System.Drawing.Size(100, 13);
      this.txt_Value.TabIndex = 0;
      this.txt_Value.TextChanged += new System.EventHandler(this.txt_Value_TextChanged);
      this.txt_Value.Click += new System.EventHandler(this.txt_Value_Click);
      this.txt_Value.GotFocus += new System.EventHandler(this.txt_Value_GotFocus);
      this.txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Value_KeyDown);
      this.txt_Value.Enter += new System.EventHandler(this.txt_Value_Enter);
      // 
      // errorProvider
      // 
      this.errorProvider.ContainerControl = this;
      this.errorProvider.DataMember = "";
      // 
      // TitledTextBox
      // 
      this.Controls.Add(this.txt_Value);
      this.Name = "TitledTextBox";
      this.Size = new System.Drawing.Size(102, 48);
      this.FontChanged += new System.EventHandler(this.TitledTextBoxl_FontChanged);
      this.ForeColorChanged += new System.EventHandler(this.TitledTextBox_ForeColorChanged);
      this.SizeChanged += new System.EventHandler(this.TitledTextBoxl_SizeChanged);
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private void ArrangeComponents()
    {
      pct_icon.Location = new Point(this.Width - pct_icon.Width, this.titleHeight + 3);
    }

		/// <summary>
		/// Draw the frame according to the style.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Visible = (imagePassword != null);

			//positions calculation according to the style
			Rectangle titleArea;
			Point lowLeftExtern = new Point(0, this.Height);
			Point lowLeftInner;
			Point hightLeftExtern;
			Point hightLeftInner;
			Point hightRightExtern;
			Point hightRightInner;

			switch (style)
			{
				case TitledTextBoxStyles.NoTitle:
					frameWidth = NOTITLE_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(0, 0), new Size(0, 0));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, 0);
					hightLeftInner = new Point(frameWidth, frameWidth);
					hightRightExtern = new Point(this.Width, 0);
					hightRightInner = new Point(this.Width, frameWidth);
					break;

				case TitledTextBoxStyles.Normal:
					frameWidth = NORMAL_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(frameWidth, 0), new Size(this.Width - frameWidth, titleHeight));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, 0);
					hightLeftInner = new Point(frameWidth, frameWidth);
					hightRightExtern = new Point(this.Width, 0);
					hightRightInner = new Point(this.Width, frameWidth);
					break;

				case TitledTextBoxStyles.Light:
					frameWidth = LIGTH_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(0, 0), new Size(this.Width, titleHeight));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, titleHeight);
					hightLeftInner = new Point(frameWidth, titleHeight + frameWidth);
					hightRightExtern = new Point(this.Width, titleHeight);
					hightRightInner = new Point(this.Width, titleHeight + frameWidth);
					break;

				case TitledTextBoxStyles.Framed:
				default:
					frameWidth = FRAMED_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(frameWidth, frameWidth), new Size(this.Width - frameWidth, titleHeight - frameWidth));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, 0);
					hightLeftInner = new Point(frameWidth, frameWidth);
					hightRightExtern = new Point(this.Width, 0);
					hightRightInner = new Point(this.Width, frameWidth);
					break;
			}

			//positioning of txt_Value textBox
			txt_Value.Height = this.Height - titleHeight - 1;
			txt_Value.Width = this.Width - frameWidth - 15;//15 for the error provider
			int verticalOffset;
			if (style.Equals(TitledTextBoxStyles.NoTitle))
				verticalOffset = 1;
			else
				verticalOffset = titleHeight;
			if (txt_Value.Multiline)
			{
				txt_Value.Location = new Point(frameWidth + 15,//15 has been added to leave enough space for the error provider 
					verticalOffset + 1);
			}
			else
			{
				txt_Value.Location = new Point(frameWidth + 15,//15 has been added to leave enough space for the error provider
					verticalOffset + (this.Height - verticalOffset - txt_Value.Height) / 2 + 1);
			}
			
			//Drawing
			Graphics g = e.Graphics;

			//draw frame
			g.FillPolygon(
				new SolidBrush(frameColor),
				new Point[] {
				lowLeftExtern, hightLeftExtern, hightRightExtern, hightRightInner, hightLeftInner, lowLeftInner
			});

			if (!style.Equals(TitledTextBoxStyles.NoTitle))
			{
				//draw title area
				g.FillRectangle(new SolidBrush(titleBackColor), titleArea);
				//draw text in title area
				AdaptStringForComponentSize(g, this, titleArea.Width, titleFont);
				g.DrawString(this.Text, titleFont, new SolidBrush(titleForeColor), titleArea);
				if (!ReadOnly)
				{
					//height of the little pen (90% of the title height)
					int side = Convert.ToInt32((titleHeight * 90) / 100);
					drawLittlePen(this.Width - SPACING - side,
						Convert.ToInt32((titleHeight - side) / 2),
						side,
						g);
				}
			}
		}
		
		/// <summary>
		/// Draw the little pen for writable.
		/// </summary>
		/// <param name="x">High left corner X of the sqaured area where the pen simbol should be drawn.</param>
		/// <param name="y">High left corner Y of the sqaured area where the pen simbol should be drawn.</param>
		/// <param name="side">Length of the side of the square where to draw the pen simbol.</param>
		/// <param name="g">Graphics objet to use for drawing.</param>
		private void drawLittlePen(int x, int y, int side, Graphics g)
		{
			g.FillPolygon(
				new SolidBrush(ColConv.Convert(TitleForeColor)),
				new Point[]{
					new Point(Convert.ToInt32(x + side / 4 * 2)		,		y																	),//1
					new Point(Convert.ToInt32(x + side / 4 * 3)		,		y																	),//2
					new Point(Convert.ToInt32(x + side / 4 * 3)		,		Convert.ToInt32(y + side / 4)			),//3
					new Point(Convert.ToInt32(x + side / 4 * 2)		,		Convert.ToInt32(y + side / 4)			),//4
					new Point(x																		,		y + side													),//5
					new Point(x																		,		Convert.ToInt32(y + side / 4 * 3)	)	//6
			});
		}

		private void TitledTextBoxl_SizeChanged(object sender, System.EventArgs e)
		{
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ArrangeComponents();

			Invalidate();
		}

		private void TitledTextBoxl_FontChanged(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		/// <summary>
		/// Set the error provider for the control.
		/// </summary>
		/// <param name="error">The string to be showed in the tooltip.</param>
		public override void SetError(string error)
		{
			errorProvider.SetError(txt_Value, error);
		}

		private void txt_Value_Enter(object sender, System.EventArgs e)
		{
			txt_Value.Select(0, txt_Value.Text.Length);
//			txt_Value.SelectionStart = 0;
//			txt_Value.SelectionLength = txt_Value.Text.Length;
		}

		private void txt_Value_TextChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(sender, e);
		}

		private void txt_Value_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.Equals(Keys.Enter))
			{
				//Avoid users to enter the "Enter" char
				e.Handled = true;
        if ((this.txt_Value.Multiline) && (this.txt_Value.SelectionLength>0))
          e.SuppressKeyPress = true;
			}
		}

		private void TitledTextBox_ForeColorChanged(object sender, EventArgs e)
		{
			txt_Value.ForeColor = this.ForeColor;
		}

    /// <summary>
    /// Reverse the colors of the value when the user enter into the control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void txt_Value_GotFocus(object sender, System.EventArgs e)
    {
      if (this.GotFocus != null)
        this.GotFocus(this, e);
    }

		private void txt_Value_Click(object sender, EventArgs e)
		{
			this.OnClick(e);
		}
	}
}
