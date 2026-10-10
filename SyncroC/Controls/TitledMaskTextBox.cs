using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;
using SyncroC.Utility;

namespace SyncroC.Controls
{
	/// <summary>
	/// TitledTextBox Styles.
	/// </summary>
	public enum TitledMaskTextBoxStyles
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
	/// Mask Styles.
	/// </summary>
	public enum TitledMaskTextBoxMask
	{
		/// <summary>Date Only.</summary>
		Date,
		/// <summary>Time Only.</summary>
		Time,
		/// <summary>Time and Date.</summary>
		TimeAndDate
	}

	/// <summary>
	/// Summary description for TitledMaskTextBox.
	/// </summary>
	public class TitledMaskTextBox : SyncroC.Controls.SyncroUserControl, IValidableControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		private IContainer components;
		private System.Windows.Forms.MaskedTextBox mkt_Value;
		private System.Windows.Forms.ErrorProvider errorProvider;

		//Constants
		/// <summary> Frame width for NoTitle style (0)</summary>
		private const int NOTITLE_FRAMEWIDTH = 0;
		/// <summary> Frame width for Normal style (4)</summary>
		private const int NORMAL_FRAMEWIDTH = 4;
		/// <summary> Frame width for Light style (1)</summary>
		private const int LIGTH_FRAMEWIDTH = 1;
		/// <summary> Frame width for Framed style (2)</summary>
		private const int FRAMED_FRAMEWIDTH = 2;
		/// <summary> Space between the check simbol and the component border.</summary>
		private const int SPACING = 3;

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

		/// <summary>Current font of the title.</summary>
		private Font titleFont = new System.Drawing.Font("Arial Unicode MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

		/// <summary>The current style of the component.</summary>
		private TitledMaskTextBoxStyles style;
		/// <summary>
		/// Gets or sets the component style.
		/// </summary>
		[Description("Gets or sets the component style."), Category("FosberSoft")]
		public TitledMaskTextBoxStyles Style
		{
			get { return style; }
			set
			{
				style = value;
				Invalidate();
			}
		}

		/// <summary>Current frame color.</summary>
		private Color frameColor = ColConv.Convert(SyncroColor.Black);
		/// <summary>
		/// Gets or sets the color of the frame.
		/// </summary>
		[Description("Gets or sets the color of the frame."), Category("FosberSoft")]
		public SyncroColor FrameColor
		{
			get { return ColConv.Convert(frameColor); }
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
		[Description("Gets or sets the TitleForeColor."), Category("FosberSoft")]
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
		[Description("Gets or sets the TitleBackColor."), Category("FosberSoft")]
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
		/// Gets or sets the text to be written into the control using a Universal date in Syncro style format.
		/// The component itself will convert this date as a Local date.
		/// </summary>
		[Description("Gets or sets the text to be written into the control using a Universal date in Syncro style format. The component itself will convert this date as a Local date time."),
			Browsable(false), Category("FosberSoft")]
		public string ValueAsUNIVERSALSyncroStyleDateTimeString
		{
			get
			{
				return SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(this.ValueAsUNIVERSALDateTime);
			}
			set
			{
				this.ValueAsUNIVERSALDateTime = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(value);
			}
		}

		/// <summary>
		/// Gets or sets the text to be written into the control using a Local date in Syncro style format.
		/// </summary>
		[Description("Gets or sets the text to be written into the control using a Local date in Syncro style format."), 
		 Browsable(false), Category("FosberSoft")]
		public string ValueAsLOCALSyncroStyleDateTimeString
		{
			get
			{
				return SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(this.ValueAsLOCALDateTime);
			}
			set
			{
				this.ValueAsLOCALDateTime = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(value);
			}
		}

		/// <summary>
		/// Gets or sets the text to be written into the control using a Universal date Time. 
		///The component itself will convert this date to a Local date Time.
		/// </summary>
		[Description("Gets or sets the text to be written into the control using a Universal date Time. The component itself will convert this date to a Local date Time."),
			Browsable(false), Category("FosberSoft")]
		public DateTime ValueAsUNIVERSALDateTime
		{
			get { return ValueAsLOCALDateTime.ToUniversalTime(); }
			set { ValueAsLOCALDateTime = value.ToLocalTime(); }
		}

		/// <summary>
		/// Gets or sets the text to be written into the control using using a Local date Time.
		/// </summary>
		[Description("Gets or sets the text to be written into the control using a LOCAL date Time"),
			Browsable(false), Category("FosberSoft")]
		public DateTime ValueAsLOCALDateTime
		{
			get
			{
				switch (this.textBoxMask)
				{
					default:
					case TitledMaskTextBoxMask.TimeAndDate:
						return SyncroConverter.ConvertStrDateTimeToDateTimeForDataGridView(this.Value);
					case TitledMaskTextBoxMask.Date:
						return SyncroConverter.ConvertStrDateToDateTimeForDataGridView(this.Value);
					case TitledMaskTextBoxMask.Time:
						return SyncroConverter.ConvertStrTimeToDateTimeForDataGridView(this.Value);
				}
			}
			set 
			{	//test only year, month and day because if the ToLocalTime is colled before assign the value
				//the "default" date (DateTime.MinValue) wouldn't be recognized....
				//if (value.Equals(DateTime.MinValue))


				if (value.Year == DateTime.MinValue.Year
					&& value.Month == DateTime.MinValue.Month
					&& value.Day == DateTime.MinValue.Day)
					this.Value = String.Empty;
				else
				{
					switch (this.textBoxMask)
					{
						default:
						case TitledMaskTextBoxMask.TimeAndDate:
							mkt_Value.Text = SyncroConverter.ConvertDateTimeToStrForDataGridView(value);
							break;
						case TitledMaskTextBoxMask.Date:
							mkt_Value.Text = SyncroConverter.ConvertDateToStrForDataGridView(value);
							break;
						case TitledMaskTextBoxMask.Time:
							mkt_Value.Text = SyncroConverter.ConvertTimeToStrForDataGridView(value);
							break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the text to be written into the control as value.
		/// </summary>
		[Description("Gets or sets the text to be written into the control as value."), Category("FosberSoft")]
		public string Value
		{
			get { return (mkt_Value.Text.Equals(String.Empty) ? " " : mkt_Value.Text); }
			set
			{
				mkt_Value.Text = value.Replace(" ", "");
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
		/// Gets or sets the text to be written into the control.
		/// </summary>
		[Description("Text showed as title."), Browsable(true),
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
		/// Gets or sets the mask to use at runtime
		/// </summary>
		[Description("Gets or sets the mask to use at runtime"), 
			Browsable(true), 
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), 
			Category("FosberSoft")]
		private TitledMaskTextBoxMask textBoxMask;
		public TitledMaskTextBoxMask TextBoxMask
		{
			get { return this.textBoxMask; }
			set
			{
				this.textBoxMask = value;
			}
		}

		/// <summary>
		/// Gets or sets the character used to mask characters of a password in a single-line SyncroC.Controls.TitledTextBox
		/// </summary>
		[Description("Gets or sets the character used to mask characters of a password in a single-line SyncroC.Controls.TitledTextBox"), Category("FosberSoft")]
		public char PasswordChar
			{
			get { return mkt_Value.PasswordChar; }
			set { mkt_Value.PasswordChar = value; }
		}

		/// <summary>
		/// Gets or sets the textbox BackColor.
		/// </summary>
		[Description("Gets or sets the textbox BackColor."), Category("FosberSoft")]
		public SyncroColor ValueBackColor
		{
			get { return ColConv.Convert(mkt_Value.BackColor); }
			set 
			{ 
				mkt_Value.BackColor = ColConv.Convert(value);
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
		/// Determine if the control is readonly.
		/// </summary>
		[Description("Determine if the control is readonly."), Category("FosberSoft")]
		public bool ReadOnly
		{
			get { return mkt_Value.ReadOnly; }
			set
			{
				this.mkt_Value.ReadOnly = value;
        this.Cursor = this.mkt_Value.ReadOnly ? Cursors.Arrow : Cursors.IBeam;
        this.mkt_Value.Cursor = this.mkt_Value.ReadOnly ? Cursors.Arrow : Cursors.IBeam;
				if (this.mkt_Value.ReadOnly)
				{
					mkt_Value.Mask = "";
				}
				else
				{
					switch (this.textBoxMask)
					{
						default:
						case TitledMaskTextBoxMask.TimeAndDate:
							mkt_Value.Mask = "00:00:00 00/00/00";
							break;
						case TitledMaskTextBoxMask.Date:
							mkt_Value.Mask = "00/00/00";
							break;
						case TitledMaskTextBoxMask.Time:
							mkt_Value.Mask = "00:00:00";
							break;
					}
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets a value indicating whether the control has the input focus.
		/// </summary>
		public override bool Focused
		{
			get { return (mkt_Value.Focused); }
		}

		/// <summary>Height of the title.</summary>
		private int titleHeight = 20;
		/// <summary>Width of the frame.</summary>
		private int frameWidth = NORMAL_FRAMEWIDTH;

		/// <summary>
		/// Builder.
		/// </summary>
		public TitledMaskTextBox()
		{
			InitializeComponent();
			this.textBoxMask = TitledMaskTextBoxMask.TimeAndDate;
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

		/// <summary>
		/// Resize the inside components according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// /// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (!(VerticalResizing == 1 && FontResizing == 1))
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
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      this.pct_icon = new System.Windows.Forms.PictureBox();

			this.components = new System.ComponentModel.Container();
			this.mkt_Value = new System.Windows.Forms.MaskedTextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
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
			// mkt_Value
			// 
			this.mkt_Value.BackColor = System.Drawing.Color.WhiteSmoke;
			this.mkt_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.mkt_Value.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.errorProvider.SetIconAlignment(this.mkt_Value, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
			this.mkt_Value.Location = new System.Drawing.Point(2, 22);
			this.mkt_Value.Name = "mkt_Value";
			this.mkt_Value.Size = new System.Drawing.Size(498, 13);
			this.mkt_Value.TabIndex = 0;
			this.mkt_Value.Enter += new System.EventHandler(this.txt_Value_Enter);
			this.mkt_Value.Click += new System.EventHandler(this.mkt_Value_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			this.errorProvider.DataMember = "";
			// 
			// TitledMaskTextBox
			// 
			this.Controls.Add(this.mkt_Value);
			this.Name = "TitledMaskTextBox";
			this.Size = new System.Drawing.Size(500, 48);
			this.FontChanged += new System.EventHandler(this.TitledTextBoxl_FontChanged);
			this.SizeChanged += new System.EventHandler(this.TitledTextBoxl_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();

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
				case TitledMaskTextBoxStyles.NoTitle:
					frameWidth = NOTITLE_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(0, 0), new Size(0, 0));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, 0);
					hightLeftInner = new Point(frameWidth, frameWidth);
					hightRightExtern = new Point(this.Width, 0);
					hightRightInner = new Point(this.Width, frameWidth);
					break;

				case TitledMaskTextBoxStyles.Normal:
					frameWidth = NORMAL_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(frameWidth, 0), new Size(this.Width - frameWidth, titleHeight));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, 0);
					hightLeftInner = new Point(frameWidth, frameWidth);
					hightRightExtern = new Point(this.Width, 0);
					hightRightInner = new Point(this.Width, frameWidth);
					break;

				case TitledMaskTextBoxStyles.Light:
					frameWidth = LIGTH_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(0, 0), new Size(this.Width, titleHeight));
					lowLeftInner = new Point(frameWidth, this.Height);
					hightLeftExtern = new Point(0, titleHeight);
					hightLeftInner = new Point(frameWidth, titleHeight + frameWidth);
					hightRightExtern = new Point(this.Width, titleHeight);
					hightRightInner = new Point(this.Width, titleHeight + frameWidth);
					break;

				case TitledMaskTextBoxStyles.Framed:
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
			mkt_Value.Height = this.Height - titleHeight - 1;
			mkt_Value.Width = this.Width - frameWidth - 15;//15 for the error provider
			if (mkt_Value.Multiline)
			{
				mkt_Value.Location = new Point(frameWidth + 15,//15 has been added to leave enough space for the error provider 
					titleHeight + 4);
			}
			else
			{
				mkt_Value.Location = new Point(frameWidth + 15,//15 has been added to leave enough space for the error provider
					titleHeight + (this.Height - titleHeight - mkt_Value.Height) / 2 + 4);
			}

			//Drawing
			Graphics g = e.Graphics;

			//draw title area
			g.FillRectangle(new SolidBrush(titleBackColor), titleArea);
			//draw text in title area
			AdaptStringForComponentSize(g, this, titleArea.Width, titleFont);
			g.DrawString(this.Text, titleFont, new SolidBrush(titleForeColor), titleArea);
			//draw frame
			g.FillPolygon(
				new SolidBrush(frameColor),
				new Point[] {
				lowLeftExtern, hightLeftExtern, hightRightExtern, hightRightInner, hightLeftInner, lowLeftInner
			});
			//draw little pen: NON VA PERCHE C'è LA LABEL!!!
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
			errorProvider.SetError(mkt_Value, error);
		}

		private void txt_Value_Enter(object sender, System.EventArgs e)
		{
			mkt_Value.Select(0, mkt_Value.Text.Length);
		}

		/// <summary>
		/// Check the validity of the text entered into the component as a date.
		/// </summary>
		/// <returns>true if a valid date has been entered, false otherwise.</returns>
		public bool IsValidDataEntered()
		{
			return !this.ValueAsUNIVERSALDateTime.Equals(DateTime.MinValue);
		}

		private void mkt_Value_Click(object sender, EventArgs e)
		{
			this.OnClick(e);
		}
	}
}

