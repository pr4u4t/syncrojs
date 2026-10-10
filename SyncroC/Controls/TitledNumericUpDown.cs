using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;
using SyncroC.Utility;

namespace SyncroC.Controls
{
	/// <summary>
	/// TitledNumericUpDown Styles.
	/// </summary>
	public enum TitledNumericUpDownStyles
	{
    /// <summary>no Title.</summary>
    NoTitleNoFramed,
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
	/// Summary description for TitledNumericUpDown.
	/// </summary>
	public class TitledNumericUpDown : SyncroC.Controls.SyncroUserControl, IValidableControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		private IContainer components;
		private System.Windows.Forms.ErrorProvider errorProvider;
    private MyNumericUpDown nUD_Value;
		private System.Windows.Forms.Label lbl_UnitMeasure;

    /// <summary>
    /// Occurs when the value changes.
    /// </summary>
    [Category("Fosbersoft"), Description("Occurs when the value changes.")]
    public event EventHandler ValueChanged;

		//Constants
    private const int NOTITLE_NOFRAMED_FRAMEWIDTH = 0;
		/// <summary> Frame width for No Title style (1)</summary>
		private const int NOTITLE_FRAMEWIDTH = 1;
		/// <summary> Frame width for Normal style (4)</summary>
		private const int NORMAL_FRAMEWIDTH = 4;
		/// <summary> Frame width for Light style (1)</summary>
		private const int LIGHT_FRAMEWIDTH = 1;
		/// <summary> Frame width for Framed style (2)</summary>
		private const int FRAMED_FRAMEWIDTH = 2;

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

		/// <summary>The current style of the component.</summary>
		private TitledNumericUpDownStyles style;
		/// <summary>
		/// Gets or sets the component style.
		/// </summary>
		[Description("Gets or sets the component style."), Category("FosberSoft")]
		public TitledNumericUpDownStyles Style
		{
			get {	return style;	}
			set 
			{
				style = value;
				Invalidate();
			}
		}

		/// <summary>Current framecolor of the component.</summary>
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
				Invalidate();
			}
		}

		private Color titleForeColor;
		/// <summary>
		/// Gets or sets the foreColor of the Title.
		/// </summary>
		[Description("Gets or sets the foreColor of the Title."), Category("FosberSoft")]
		public SyncroColor TitleForeColor 
		{
			get {	return ColConv.Convert(titleForeColor);	}
			set 
			{	
				titleForeColor = ColConv.Convert(value);
				Invalidate();
			}
		}

		[Description("Gets or sets whether the unit measure is visible."), Category("FosberSoft"), DefaultValue(true)]
		public bool UnitMeasureVisible
		{
			get { return this.lbl_UnitMeasure.Visible; }
			set
			{
				this.lbl_UnitMeasure.Visible = value;
				this.lbl_UnitMeasure.Invalidate();
			}
		}

		private Color titleBackColor;
		/// <summary>
		/// Gets or sets the title BackColor.
		/// </summary>
		[Description("Gets or sets the title backcolor."), Category("FosberSoft")]
		public SyncroColor TitleBackColor
		{
			get {	return ColConv.Convert(titleBackColor);	}
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

	  private Decimal internalValue = new Decimal(0);
    private bool empty = true;

		private bool firstActiveSet = true; 
    /// <summary>
		/// Gets or sets the value of the control.
		/// </summary>
		[Description("Gets or sets the value that must be expressed as the internalUnitMeasure"), Category("FosberSoft")]
		public string Value
		{
			get 
			{
				checkForValueBeenEdited();
				if ( (internalValue!=0) || (!this.empty))
          return internalValue.ToString();
        return string.Empty;
      }
			set 
			{
				decimal theInternalValue = this.internalValue;
				if (value == null || value.Equals(String.Empty) || value.Equals(" "))
				{
					internalValue = 0;
          empty = true;
          nUD_Value.Value = 0;
        }
        else
				{
          empty = false;
          try
					{
						internalValue = Convert.ToDecimal(value);
						EnsureInternalValueCorrectness();
            nUD_Value.Value = SyncroConverter.Convert(internalValue, internalUnitMeasure, unitMeasure);
						
						if (this.Visible == false)
							this.Visible = true;
          }
					catch (Exception ex)
					{
						internalValue = this.Minimum;
						nUD_Value.Value = SyncroConverter.Convert(internalValue, internalUnitMeasure, unitMeasure);
						Logger.LogError(this, "Value set - set to minimum.", ex);
					}
        }
				if ((this.internalValue != theInternalValue) || firstActiveSet)
				{
					firstActiveSet = false;
					Invalidate();
				}
			}
		}

		/// <summary>Current unit measure displaied by the component</summary>
		private UnitMeasure unitMeasure = UnitMeasure.NoUnitMeasure;
		/// <summary>
		/// Gets or sets the unit measure displayed by the component. 
		/// </summary>
		[Description("Gets or sets the unit measure displayed by the component."), Category("FosberSoft")]
		public UnitMeasure UnitMeasure
		{
			get	{ return unitMeasure; }
			set 
			{
				unitMeasure = value;
				lbl_UnitMeasure.Text = SyncroConverter.ToString(value);
				nUD_Value.Value = SyncroConverter.Convert(internalValue, internalUnitMeasure, unitMeasure);
				Invalidate();
			}
		}

		/// <summary>Current unit measure internally used by the component</summary>
		public UnitMeasure internalUnitMeasure = UnitMeasure.NoUnitMeasure;
		/// <summary>
		/// Gets or sets the unit measure internally used by the component.
		/// </summary>
		[Description("Gets or sets the unit measure internally used by the component."), Category("FosberSoft")]
		public UnitMeasure InternalUnitMeasure
		{
			get { return internalUnitMeasure; }
			set 
			{ 
				internalUnitMeasure = value;
				nUD_Value.Value = SyncroConverter.Convert(internalValue, internalUnitMeasure, unitMeasure);
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the value BackColor.
		/// </summary>
		[Description("Gets or sets the value BackColor."), Category("FosberSoft")]
		public SyncroColor ValueBackColor 
		{
			get {	return ColConv.Convert(nUD_Value.BackColor);	}
			set {	
				nUD_Value.BackColor = ColConv.Convert(value);
			}
		}

    [Description("Gets or sets the value ForeColor."), Category("FosberSoft")]
    public Color ValueForeColor
    {
      get { return nUD_Value.ForeColor; }
      set
      {
        nUD_Value.ForeColor = value;
        this.Invalidate();
      }
    }

		public override Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;
        //agrassi_FI23-03-2011_3
        //ValueBackColor = ColConv.Convert(value);
        nUD_Value.BackColor = value;
        this.Invalidate();
			}
		}

		/// <summary>
		/// Determine if the control is readonly.
		/// </summary>
		private bool readOnly;
		[Description("Determine if the control is readonly."), Category("FosberSoft")]
		public bool ReadOnly
		{
			get { return readOnly; }
			set	
			{
				readOnly=value;
				nUD_Value.ReadOnly = value;
				nUD_Value.InterceptArrowKeys = ! value;
				Invalidate();
			}
		}

		/// <summary>
		/// Number of decimal.
		/// </summary>
		[Description("Number of decimal."), Category("FosberSoft")]
		public int DecimalPlaces
		{
			get {	return nUD_Value.DecimalPlaces;	}
			set	{	nUD_Value.DecimalPlaces = value;	}
		}

		/// <summary>
		/// The Increment.
		/// </summary>
		[Description("The Increment."), Category("FosberSoft")]
		public decimal Increment
		{
			get {	return nUD_Value.Increment;	}
			set	{	nUD_Value.Increment = value;	}
		}

		private decimal maximum;
		/// <summary>
		/// The Maximum.
		/// </summary>
		[DefaultValue(false), Category("FosberSoft")]
		public decimal Maximum
		{
			//get {	return nUD_Value.Maximum;	}
			//set	{	nUD_Value.Maximum = value;	}
			get { return maximum; }
			set 
      {
        maximum = value;
        //this.nUD_Value.Maximum = maximum;
      }
		}

		private decimal minimum;
		/// <summary>
		/// The Minimum.
		/// </summary>
		[DefaultValue(false), Category("FosberSoft")]
		public decimal Minimum
		{
			//get {	return nUD_Value.Minimum;	}
			//set	{	nUD_Value.Minimum = value;	}
			get { return minimum; }
			set 
      {
        minimum = value;
        //this.nUD_Value.Minimum = minimum;
      }
		}

    private bool disableEventForValueChanged = false;
    [Browsable(false), DefaultValue(false), Category("FosberSoft")]
    public bool DisableEventForValueChanged
    {
      get { return disableEventForValueChanged; }
      set { disableEventForValueChanged = value; }
    }

		/// <summary>
		/// Gets a value indicating whether the control has the input focus.
		/// </summary>
		public override bool Focused
		{
			get {	return (nUD_Value.Focused);	}
		}

		/// <summary>Design time height of the lbl_Title label.</summary>
		private int titleHeight = 20;
		/// <summary>Width of the frame.</summary>
		private int frameWidth = NORMAL_FRAMEWIDTH;

		/// <summary>
		/// Builder.
		/// </summary>
		public TitledNumericUpDown()
		{
			InitializeComponent();
      this.nUD_Value.Maximum = 0x7FFFFFFF;
      this.nUD_Value.Minimum = -0x7FFFFFFF;
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

    public string getValueNoCheckForValueBeenEdited()
    {
      if ((internalValue != 0) || (!this.empty))
        return internalValue.ToString();
      return string.Empty;
    }

		public int getValueAsInt()
		{
			int intValue = 0;
			try
			{
				intValue = Convert.ToInt32(this.Value);
			}
			catch (Exception ex)
			{
				Logger.LogError(this, ex);
			}
			return intValue;
		}

		/// <summary>
		/// Resize the inside components according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// /// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
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

      this.components = new System.ComponentModel.Container();
      this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
      this.nUD_Value = new MyNumericUpDown();
      this.lbl_UnitMeasure = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nUD_Value)).BeginInit();
      
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit();
      
      this.SuspendLayout();
      // 
      // errorProvider
      // 
      this.errorProvider.ContainerControl = this;
      this.errorProvider.DataMember = "";
      // 
      // nUD_Value
      // 
      this.nUD_Value.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nUD_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.nUD_Value.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.errorProvider.SetIconAlignment(this.nUD_Value, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
      this.nUD_Value.Location = new System.Drawing.Point(8, 16);
      this.nUD_Value.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nUD_Value.Name = "nUD_Value";
      this.nUD_Value.Size = new System.Drawing.Size(112, 21);
      this.nUD_Value.TabIndex = 2;
      this.nUD_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.nUD_Value.TextChanged += new System.EventHandler(this.nUD_Value_TextChanged);
      this.nUD_Value.ValueChanged += new System.EventHandler(this.nUD_Value_ValueChanged);
      this.nUD_Value.Enter += new System.EventHandler(this.nUD_Value_Enter);
      // 
      // lbl_UnitMeasure
      // 
      this.lbl_UnitMeasure.AutoSize = true;
      this.lbl_UnitMeasure.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_UnitMeasure.Location = new System.Drawing.Point(72, 40);
      this.lbl_UnitMeasure.Name = "lbl_UnitMeasure";
      this.lbl_UnitMeasure.Size = new System.Drawing.Size(0, 15);
      this.lbl_UnitMeasure.TabIndex = 3;
      this.lbl_UnitMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // TitledNumericUpDown
      // 

      this.Controls.Add(this.lbl_UnitMeasure);
      this.Controls.Add(this.nUD_Value);
      this.Name = "TitledNumericUpDown";
      this.Size = new System.Drawing.Size(128, 64);
      this.FontChanged += new System.EventHandler(this.TitledNumericUpDownl_FontChanged);
      this.SizeChanged += new System.EventHandler(this.TitledNumericUpDownl_SizeChanged);
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nUD_Value)).EndInit();

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

			Rectangle titleArea;
			switch (style)
			{
        case TitledNumericUpDownStyles.NoTitleNoFramed:
          frameWidth = NOTITLE_NOFRAMED_FRAMEWIDTH;
          titleArea = new Rectangle(new Point(0, 0), new Size(0, 0));
          break;

				case TitledNumericUpDownStyles.NoTitle:
					frameWidth = NOTITLE_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(0, 0), new Size(0, 0));
					break;

				case TitledNumericUpDownStyles.Normal:
					frameWidth = NORMAL_FRAMEWIDTH/* * 2*/;
					titleBackColor = frameColor;
					titleArea = new Rectangle(new Point(NORMAL_FRAMEWIDTH, 0), new Size(this.Width - NORMAL_FRAMEWIDTH, titleHeight));
					break;

				case TitledNumericUpDownStyles.Light:
					frameWidth = LIGHT_FRAMEWIDTH;
					titleBackColor = nUD_Value.BackColor;
					titleArea = new Rectangle(new Point(0, 0), new Size(this.Width, titleHeight));
					break;

				case TitledNumericUpDownStyles.Framed:
				default:
					frameWidth = FRAMED_FRAMEWIDTH;
					titleArea = new Rectangle(new Point(frameWidth, frameWidth), new Size(this.Width - frameWidth, titleHeight - frameWidth));
					break;
			}

			int errorProviderSpace = 15;
			int arrowsWidth = 18;
			int verticalOffset;
      if (style.Equals(TitledNumericUpDownStyles.NoTitle))
        verticalOffset = NOTITLE_FRAMEWIDTH;
      else if (style.Equals(TitledNumericUpDownStyles.NoTitleNoFramed))
          verticalOffset = NOTITLE_NOFRAMED_FRAMEWIDTH;
        else
          verticalOffset = titleHeight + NOTITLE_FRAMEWIDTH;

			nUD_Value.Location = new Point(frameWidth + errorProviderSpace,
				verticalOffset + (this.Height - verticalOffset - nUD_Value.Height) / 2 + 4);

			lbl_UnitMeasure.Location = new Point(this.Width - lbl_UnitMeasure.Width,
				(this.Height - lbl_UnitMeasure.Height));

			nUD_Value.Width = this.Width - frameWidth - errorProviderSpace 
				- (lbl_UnitMeasure.Visible ? lbl_UnitMeasure.Width : 0);
			GraphicsPath gp = new GraphicsPath();
			gp.AddPolygon(new Point[]{
										 new Point(0, 0),
										 new Point(nUD_Value.Width - arrowsWidth, 0),
										 new Point(nUD_Value.Width - arrowsWidth, nUD_Value.Height),
										 new Point(0, nUD_Value.Height)});
			nUD_Value.Region = new Region(gp);

			nUD_Value.Height = this.Height - titleHeight - 1;
			//invalidate the inside components
			lbl_UnitMeasure.Invalidate();
			nUD_Value.Invalidate();

			//drawing
			Graphics g = e.Graphics;
			int halfFrameWidth = Convert.ToInt32(Math.Ceiling((double)(frameWidth / 2)));
			Point lowLeft = new Point(halfFrameWidth, this.Height);
			Point hightLeft = new Point(halfFrameWidth, halfFrameWidth);
			Point hightRight = new Point(this.Width, halfFrameWidth);
			//drawing lines according to the style
			if (style == TitledNumericUpDownStyles.Light)
			{
				hightLeft = new Point(halfFrameWidth, titleArea.Location.Y + titleArea.Height + LIGHT_FRAMEWIDTH);
				hightRight = new Point(this.Width, titleArea.Location.Y + titleArea.Height + LIGHT_FRAMEWIDTH);
			}
      if (style != TitledNumericUpDownStyles.NoTitleNoFramed)
      {
        //Draw frame
        g.DrawLines(
          new Pen(new SolidBrush(frameColor), frameWidth),
          new Point[]{
							   lowLeft, hightLeft, hightRight});
      }
			if (!style.Equals(TitledNumericUpDownStyles.NoTitle))
			{
				//draw title area
				g.FillRectangle(new SolidBrush(titleBackColor), titleArea);
				//draw text in title area
				AdaptStringForComponentSize(g, this, titleArea.Width, titleFont);
				g.DrawString(this.Text, titleFont, new SolidBrush(titleForeColor), titleArea);

				if (!readOnly)
				{
					//height of the little triangle (75% of the font height)
					int TriangleHeigth = Convert.ToInt32((titleFont.Height * 75) / 100);
					//distance of the little triangle from the label border
					int BorderDistance = Convert.ToInt32((titleFont.Height - TriangleHeigth) / 2);
					//length of the little triangle side
					int TriangleSide = Convert.ToInt32(TriangleHeigth / (Math.Sqrt(3) / 2));
					//little triangle corners coordinates
					Point topLeft = new Point(
						titleArea.Width - BorderDistance - TriangleSide,//x 
						BorderDistance);//y
					Point topRight = new Point(
						titleArea.Width - BorderDistance,//x
						BorderDistance);//y
					Point low = new Point(
						titleArea.Width - BorderDistance - Convert.ToInt32(TriangleSide / 2),//x
						BorderDistance + TriangleHeigth);//y
					//drawing a filled triangle 
					g.FillPolygon(
						new SolidBrush(titleForeColor),
						new Point[] { topLeft, topRight, low });
				}
			}
		}

		private void TitledNumericUpDownl_SizeChanged(object sender, System.EventArgs e)
		{
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ArrangeComponents();

			Invalidate();
		}

		private void TitledNumericUpDownl_FontChanged(object sender, System.EventArgs e)
		{
			nUD_Value.Font = this.Font;
			Invalidate();
		}

		/// <summary>
		/// Set the error provider for the control.
		/// </summary>
		/// <param name="error">The string to be showed in the tooltip.</param>
		public override void SetError(string error)
		{
			errorProvider.SetError(nUD_Value, error);
		}

		private void nUD_Value_Enter(object sender, System.EventArgs e)
		{
			nUD_Value.Select(0, nUD_Value.Text.Length);
		}

    private void nUD_Value_ValueChanged(object sender, EventArgs e)
    {
      checkForValueBeenEdited();
      if (this.ValueChanged != null)
        if (!this.disableEventForValueChanged)
          this.ValueChanged(this, e);
    }

    private void nUD_Value_TextChanged(object sender, EventArgs e)
    {
      if (this.nUD_Value.Text.Trim() == "")
      {
        this.disableEventForValueChanged = true;
        this.Value = "0";
        this.nUD_Value.Text = "0";
        this.nUD_Value.Select(0, this.nUD_Value.Text.Length);
        this.disableEventForValueChanged = false;
      }
    }

    private void checkForValueBeenEdited()
		{
			if (!nUD_Value.Value.Equals(SyncroConverter.Convert(internalValue, internalUnitMeasure, unitMeasure)))
			{ //then the component has been edited and the internal value needs to be updated
				internalValue = SyncroConverter.Convert(nUD_Value.Value, unitMeasure, internalUnitMeasure);
				if (EnsureInternalValueCorrectness())
					nUD_Value.Value = SyncroConverter.Convert(internalValue, internalUnitMeasure, unitMeasure);
			}
		}

		private bool EnsureInternalValueCorrectness()
		{
			if (internalValue > this.Maximum)
			{
				internalValue = this.Maximum;
				return true;
			}
			if (internalValue < this.Minimum)
			{
				internalValue = this.Minimum;
				return true;
			}
			return false;
		}

    //ABELLO_FI19/10/2009_1 Sui controlli TitledNumericUpDown del Client anche quando erano readOnly se si girava la rotellina del mouse cambiavano i valori.
    private class MyNumericUpDown : NumericUpDown
    {
      protected override void OnMouseWheel(MouseEventArgs e)
      {
        if (!this.ReadOnly)
          base.OnMouseWheel(e);
      }
    }
	}
}
