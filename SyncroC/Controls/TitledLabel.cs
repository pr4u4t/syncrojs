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
	/// TitledLabel Styles.
	/// </summary>
	public enum TitledLabelStyles
	{
		/// <summary>no title.</summary>
		NoTitle,
		/// <summary>no frame, no underline.</summary>
		Normal,
		/// <summary>title softly underlined.</summary>
		Light,
		/// <summary>title with frame.</summary>
		Framed
	}

	/// <summary>
	/// Implements a label with a title.
	/// </summary>
	public class TitledLabel : SyncroC.Controls.SyncroUserControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lbl_Title;
		private System.Windows.Forms.Label lbl_Value;

		//Constants
		/// <summary> Frame width for NoTitle style (1)</summary>
		private const int NOTITLE_FRAMEWIDTH = 1;
		/// <summary> Frame width for Normal style (4)</summary>
		private const int NORMAL_FRAMEWIDTH = 4;
		/// <summary> Frame width for Light style (1)</summary>
		private const int LIGTH_FRAMEWIDTH = 1;
		/// <summary> Frame width for Framed style (2)</summary>
		private const int FRAMED_FRAMEWIDTH = 2;

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

		/// <summary>The current style of the component.</summary>
		private TitledLabelStyles style;
		/// <summary>
		/// Gets or sets the component style.
		/// </summary>
		[Description("Gets or sets the component's style."), Category("FosberSoft")]
		public TitledLabelStyles Style
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

		/// <summary>Current frame color.</summary>
		private Color frameColor = ColConv.Convert(SyncroColor.White);
		/// <summary>
		/// Gets or sets the color of the frame.
		/// </summary>
		[Description("Gets or sets the color of the frame."), Category("FosberSoft")]
		public SyncroColor FrameColor
		{
			get {	return ColConv.Convert(frameColor);	}
			set 
			{
				frameColor = ColConv.Convert(value);
				if (style == TitledLabelStyles.Normal)
					lbl_Title.BackColor = frameColor;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the foreColor of the Title.
		/// </summary>
		[Description("Gets or sets the color of the title."), Category("FosberSoft")]
		public SyncroColor TitleForeColor 
		{
			get	{	return ColConv.Convert(lbl_Title.ForeColor);	}
			set {	lbl_Title.ForeColor = ColConv.Convert(value);				}
		}

		/// <summary>
		/// Gets or sets the title BackColor.
		/// </summary>
		[Description("Gets or sets the backColor of the component."), Category("FosberSoft")]
		public SyncroColor TitleBackColor
		{
			get {	return ColConv.Convert(lbl_Title.BackColor);	}
			set	{	lbl_Title.BackColor = ColConv.Convert(value);				}
		}

		/// <summary>
		/// Gets or sets the value BackColor.
		/// </summary>
		[Description("Gets or sets the value BackColor."), Category("FosberSoft")]
		public SyncroColor ValueBackColor
		{
			get {	return ColConv.Convert(lbl_Value.BackColor);	}
			set	
			{
				lbl_Value.BackColor = ColConv.Convert(value);
			}
		}

		public override Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
				ValueBackColor = ColConv.Convert(value);
				this.Invalidate();
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

		private bool valueMultiline = true;
		public bool ValueMultiline
		{
			get { return valueMultiline; }
			set	{valueMultiline = value; }
		}

		/// <summary>
		/// Gets or sets the text to be written into the control as a value.
		/// </summary>
		[Description("Gets or sets the text to be written into the control as a value."), Category("FosberSoft")]
		public string Value
		{
			get {	return lbl_Value.Text;	}
			set 
			{	
				lbl_Value.Text = value;
				if (this.Visible == false && !value.Equals(string.Empty))
				{
					try
					{
						this.Visible = true;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "Value set - ", ex);
					}
				}
			}
		}

		/// <summary>
		/// Raised when the control is clicked.
		/// </summary>
		[Description("Raised when the control is clicked."), Category("Fosbersoft")]
		public new event EventHandler Click;

		/// <summary>Current height of the lbl_Title label.</summary>
		private int titleHeight = 20;
		/// <summary>Width of the frame.</summary>
		private int frameWidth = NORMAL_FRAMEWIDTH;

		/// <summary>
		/// Builder.
		/// </summary>
		public TitledLabel()
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
				lbl_Title.Font = new Font(lbl_Title.Font.FontFamily, 
					Convert.ToInt32(lbl_Title.Font.Size * FontResizing), lbl_Title.Font.Style);
				this.Font = new Font(this.Font.FontFamily, 
					Convert.ToInt32(this.Font.Size * FontResizing), this.Font.Style);
				titleHeight = Convert.ToInt32(titleHeight * VerticalResizing);
				Invalidate();
			}
		}

		/// <summary>
		/// Invalidate the container, set color, location, size of every component according to the style 
		/// and invalidate all the inside components.
		/// </summary>
		public new void Invalidate()
		{
			//invalidate the container
			base.Invalidate();
			//set color, location, size of every component according to the style
			switch (style)
			{
				case TitledLabelStyles.NoTitle:
					frameWidth = NOTITLE_FRAMEWIDTH;
					lbl_Title.Location = new Point(0, 0);
					lbl_Title.Width = 0;
					lbl_Title.Height = 0;
					break;

				case TitledLabelStyles.Normal:
					frameWidth = NORMAL_FRAMEWIDTH;
					lbl_Title.BackColor = frameColor;
					lbl_Title.Location = new Point(frameWidth, 0);
					lbl_Title.Width = this.Width - frameWidth;
					lbl_Title.Height = titleHeight;
					break;

				case TitledLabelStyles.Light:
					frameWidth = LIGTH_FRAMEWIDTH;
					lbl_Title.BackColor = lbl_Value.BackColor;
					lbl_Title.Location = new Point(0, 0);
					lbl_Title.Width = this.Width;
					lbl_Title.Height = titleHeight;
					break;

				case TitledLabelStyles.Framed:
					frameWidth = FRAMED_FRAMEWIDTH;
					lbl_Title.Location = new Point(frameWidth, frameWidth);
					lbl_Title.Width = this.Width - frameWidth;
					lbl_Title.Height = titleHeight - frameWidth;
					break;
			}
			lbl_Value.Location = new Point(frameWidth + 15,//15 has been added to leave enough space for the error provider 
				lbl_Title.Location.Y + lbl_Title.Height + frameWidth + 1);
			lbl_Value.Width = this.Width - frameWidth - 15;//15 for the error provider
			lbl_Value.Height= this.Height - (lbl_Title.Location.Y + lbl_Title.Height - 1);
			//invalidate the inside components
			lbl_Title.Invalidate();
			lbl_Value.Invalidate();
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

      this.Controls.Add(this.pct_icon);

			this.lbl_Title = new System.Windows.Forms.Label();
			this.lbl_Value = new System.Windows.Forms.Label();
      
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit();

			this.SuspendLayout();
// 
// lbl_Title
// 
			this.lbl_Title.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Title.Location = new System.Drawing.Point(2, 2);
			this.lbl_Title.Name = "lbl_Title";
			this.lbl_Title.Size = new System.Drawing.Size(100, 18);
			this.lbl_Title.TabIndex = 0;
			this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_Title.Click += new System.EventHandler(this.lbl_Title_Click);
// 
// lbl_Value
// 
			this.lbl_Value.BackColor = System.Drawing.SystemColors.HighlightText;
			this.lbl_Value.Location = new System.Drawing.Point(2, 22);
			this.lbl_Value.Name = "lbl_Value";
			this.lbl_Value.Size = new System.Drawing.Size(100, 13);
			this.lbl_Value.TabIndex = 1;
			this.lbl_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_Value.TextChanged += new System.EventHandler(this.lbl_Value_TextChanged);
      this.lbl_Value.Click += new System.EventHandler(this.lbl_Title_Click);
// 
// TitledLabel
// 
			this.Controls.Add(this.lbl_Value);
			this.Controls.Add(this.lbl_Title);
			this.Name = "TitledLabel";
			this.Size = new System.Drawing.Size(102, 48);
			this.TextChanged += new System.EventHandler(this.TitledLabel1_TextChanged);
			this.FontChanged += new System.EventHandler(this.TitledLabel1_FontChanged);
			this.SizeChanged += new System.EventHandler(this.TitledLabel1_SizeChanged);

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

			Graphics g = e.Graphics;
			//drawing lines extreme coordinates
			int halfFrameWidth = Convert.ToInt32(Math.Ceiling((double)(frameWidth / 2)));
			Point lowLeft = new Point(halfFrameWidth, this.Height);
			Point hightLeft = new Point(halfFrameWidth, halfFrameWidth);
			Point hightRight = new Point(this.Width, halfFrameWidth);
			//drawing lines according to the style
			if (style == TitledLabelStyles.Light)
			{
				hightLeft = new Point(halfFrameWidth, lbl_Title.Location.Y + lbl_Title.Height + LIGTH_FRAMEWIDTH); 
				hightRight = new Point (this.Width, lbl_Title.Location.Y + lbl_Title.Height + LIGTH_FRAMEWIDTH);
			}
			g.DrawLines(
				new Pen(new SolidBrush(frameColor), frameWidth),
				new Point[]{
							   lowLeft, hightLeft, hightRight});
			//adapt strings at component dimensions
			if (!valueMultiline)
				AdaptStringForComponentSize(g, lbl_Value, lbl_Value.Width, lbl_Value.Font);
			AdaptStringForComponentSize(g, lbl_Title, lbl_Title.Width, lbl_Title.Font);
		}

		private void TitledLabel1_SizeChanged(object sender, System.EventArgs e)
		{
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ArrangeComponents();

			Invalidate();
		}

		private void TitledLabel1_TextChanged(object sender, System.EventArgs e)
		{
			lbl_Title.Text = Text;
		}

		private void TitledLabel1_FontChanged(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		private void lbl_Value_TextChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(this, e);
		}

		private void lbl_Title_Click(object sender, EventArgs e)
		{
			if (Click != null)
				Click(this, e);
		}
	}
}
