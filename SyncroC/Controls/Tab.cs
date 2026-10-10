using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SyncroC.Controls
{
	/// <summary>
	/// Represent the types of selection supported by the Tab component.
	/// </summary>

  //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
  public enum TabSelectionType : int
	{
		/// <summary>Tab not selected</summary>
		Unselected,
		/// <summary>Selected tab</summary>
		Selected,
		/// <summary>Alarm tab not selected</summary>
		UnselectedAlarm,
		/// <summary>Selected alarm tab</summary>
		SelectedAlarm,
		/// <summary>Link to another group...</summary>
		Link,
    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    /// <summary>Warning tab not selected</summary>
    UnselectedWarning,
    /// <summary>Warning tab selected</summary>
    SelectedWarning

	}
	
	/// <summary>
	/// Component for Tab simulation.
	/// </summary>
	public class Tab : SyncroC.Controls.SyncroUserControl
	{
		private System.ComponentModel.IContainer components=null;

		private System.Windows.Forms.PictureBox pct_icon;

		/// <summary>
		/// Raised when the control is clicked.
		/// </summary>
		[Description("Raised when the control is clicked."), Category("Fosbersoft")]
		public new event EventHandler Click;

		private Color UNSELECTED_OUTLINE_COLOR = Color.Black;
		private Color UNSELECTED_ALARM_OUTLINE_COLOR = Color.FromArgb(204, 2, 28);
		private Color SELECTED_ALARM_BACKGROUND_COLOR = Color.FromArgb(204, 2, 28);
		private Color LINK_COLOR = Color.Silver;
    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    private Color UNSELECTED_WARNING_OUTLINE_COLOR = Color.FromArgb(255, 216, 0);
    private Color SELECTED_WARNING_BACKGROUND_COLOR = Color.FromArgb(255, 216, 0);


		/// <summary>Distance between the icon and the frame.</summary>
	//	private const int MIN_DISTANCE_ICON = 2;
		/// <summary>Distance between the text and the frame or icon.</summary>
		private const int MIN_DISTANCE_TEXT = 3;
		private const int FRAME_WIDTH = 2;
		private int halfFrameWidth;

		/// <summary>Current selection type for the control.</summary>
		private TabSelectionType tabSelection;
		/// <summary>
		/// Gets or sets the selectionType for the control.
		/// </summary>
		[Description("Gets or sets the selectionType for the control."), Category("FosberSoft")]
		public TabSelectionType TabSelection
		{
			get {	return tabSelection;	}
			set	
			{
				tabSelection = value;
				Invalidate();
			}
		}

		/// <summary>Color to be used for the background when the tab is selected.</summary>
		private Color selectedBackGroundColor;
		/// <summary>
		/// Gets or sets the color to be used for the background when the tab is selected.
		/// </summary>
		[Description("Gets or sets the color to be used for the background when the tab is selected."), Category("FosberSoft")]
		public Color SelectedBackGroundColor
		{
			get {	return selectedBackGroundColor;	}
			set	
			{
				selectedBackGroundColor = value;
				Invalidate();
			}
		}
		
		/// <summary>Image displayed as icon into the tab.</summary>
		private Image image;
		/// <summary>Gets or sets the optional image displayed as icon into the tab.</summary>
		[Description("Gets or sets the optional image displayed as icon into the tab."), Category("FosberSoft")]
		public Image Image
		{
			get {	return image;	}
			set 
			{
				image = value;
				pct_icon.Image = image;
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

		/// <summary> Current orientation of the control.</summary>
		private bool vertical = false;
		/// <summary>
		/// Specifies if the control text should be written vertically.
		/// </summary>
		[Description("Specifies if the control text should be written vertically."), Category("FosberSoft")]
		public bool Vertical
		{
			get {	return vertical;	}
			set
			{
				vertical = value;
				Invalidate();
			}
		}

		/// <summary>
		/// builder.
		/// </summary>
		public Tab()
		{
			InitializeComponent();
			halfFrameWidth = Convert.ToInt32(Math.Floor((double)(FRAME_WIDTH / 2)));
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
			this.pct_icon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit();
			this.SuspendLayout();
			// 
			// pct_icon
			// 
			this.pct_icon.Location = new System.Drawing.Point(2, 2);
			this.pct_icon.Name = "pct_icon";
			this.pct_icon.Size = new System.Drawing.Size(25, 25);
			this.pct_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pct_icon.TabIndex = 0;
			this.pct_icon.TabStop = false;
			this.pct_icon.Visible = false;
			this.pct_icon.Click += new System.EventHandler(this.Tab_Icon_Click);
			// 
			// Tab
			// 
			this.Controls.Add(this.pct_icon);
			this.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Tab";
			this.Size = new System.Drawing.Size(104, 32);
			base.Click += new System.EventHandler(this.Tab_Icon_Click);
			this.SizeChanged += new System.EventHandler(this.Tab_SizeChanged);
			this.TextChanged += new System.EventHandler(this.TextChangedHandler);
			((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Invalidate the component (where the text is written) when the property Text of the control change.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextChangedHandler(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		private void Tab_SizeChanged(object sender, EventArgs e)
		{
			ArrangeComponents();
		}

		private void ArrangeComponents()
		{
			//if (vertical)
			//{
			////	pct_icon.Width = pct_icon.Height = Convert.ToInt32(this.Width - 2 * (MIN_DISTANCE_ICON + FRAME_WIDTH));
			//  pct_icon.Location = new Point(Convert.ToInt32(((this.Width - pct_icon.Width) / 2) - halfFrameWidth), MIN_DISTANCE_ICON + FRAME_WIDTH);
			//}
			//else
			//{
			////	pct_icon.Width = pct_icon.Height = Convert.ToInt32(this.Height - 2 * (MIN_DISTANCE_ICON + FRAME_WIDTH));
			//  pct_icon.Location = new Point(MIN_DISTANCE_ICON + FRAME_WIDTH, Convert.ToInt32(((this.Height - pct_icon.Height) / 2) + halfFrameWidth));
			//}
			pct_icon.Location = new Point(FRAME_WIDTH, FRAME_WIDTH);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			ArrangeComponents();

			Graphics g = e.Graphics;
			int offset = MIN_DISTANCE_TEXT; //distance of the text from the border
			int oneThirdHeight;
			int wideSpace; //available space for the text
			Point first_sel; //first corner when selected
			Point first_unSel; //first corner when unselected
			Point second_sel;//second corner when selected
			Point second_unSel;//second corner when unselected
			Point third_sel;//third corner when selected
			Point third_unSel;//third corner when unselected
			Point fourth_sel;//fourth corner when selected
			Point fourth_unSel;//fourth corner when unselected
			Point fifth_sel;//fifth corner when selected
			Point fifth_unSel;//fifth corner when unselected
			Rectangle stringRectangle;
			Font currentFont = this.Font;
			System.Drawing.StringFormat stringFormat; 

			pct_icon.Visible = (image != null);

			if (vertical)
			{
				oneThirdHeight = Convert.ToInt32(this.Width / 3);
				first_unSel = new Point(0, halfFrameWidth);
				second_unSel = new Point(this.Width - halfFrameWidth, halfFrameWidth);
				third_unSel = new Point(this.Width - halfFrameWidth, this.Height - oneThirdHeight);
				fourth_unSel = new Point(oneThirdHeight * 2, this.Height - halfFrameWidth);
				fifth_unSel = new Point(0, this.Height - halfFrameWidth);
				first_sel = new Point(0, 0);
				second_sel = new Point(this.Width, 0);
				third_sel = new Point(this.Width, this.Height - oneThirdHeight);
				fourth_sel = new Point(oneThirdHeight * 2, this.Height);
				fifth_sel = new Point(0, this.Height);
				if (pct_icon.Visible)
					offset += pct_icon.Location.Y + pct_icon.Height;
				wideSpace = this.Size.Height - offset - MIN_DISTANCE_TEXT;
				
				//if (g.MeasureString(Text, Font).Width < wideSpace)
				//{	//single line text
				stringRectangle = new Rectangle(
					Convert.ToInt32(((this.Width - currentFont.GetHeight()) / 2) - halfFrameWidth), offset,
					Convert.ToInt32(currentFont.GetHeight()), this.Size.Height - offset - MIN_DISTANCE_TEXT);
				//}
				//else
				//{	//two lines text
				//  currentFont = new Font(this.Font.FontFamily, (float)(this.Font.Size * 0.8));
				//  stringRectangle = new Rectangle(
				//    Convert.ToInt32((this.Width - currentFont.GetHeight() * 2) / 2), offset,
				//    Convert.ToInt32(currentFont.GetHeight() * 2), this.Size.Height - offset - MIN_DISTANCE_TEXT);
				//}
				stringFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical | StringFormatFlags.DirectionRightToLeft);
			}
			else
			{
				oneThirdHeight = Convert.ToInt32(this.Height / 3);
				first_unSel = new Point(halfFrameWidth, this.Height);
				second_unSel = new Point(halfFrameWidth, halfFrameWidth);
				third_unSel = new Point(this.Width - oneThirdHeight, halfFrameWidth);
				fourth_unSel = new Point(this.Width - halfFrameWidth, oneThirdHeight);
				fifth_unSel = new Point(this.Width - halfFrameWidth, this.Height);
				first_sel = new Point(0, this.Height);
				second_sel = new Point(0, 0);
				third_sel = new Point(this.Width - oneThirdHeight, 0);
				fourth_sel = new Point(this.Width, oneThirdHeight);
				fifth_sel = new Point(this.Width, this.Height);
				if (pct_icon.Visible)
					offset += pct_icon.Location.X + pct_icon.Width;
				wideSpace = this.Size.Width - offset - MIN_DISTANCE_TEXT;
				//if (g.MeasureString(Text, Font).Width < wideSpace)
				//{//single line text
					stringRectangle = new Rectangle(
						offset, Convert.ToInt32(((this.Height - currentFont.GetHeight()) / 2) - halfFrameWidth),
						wideSpace, Convert.ToInt32(currentFont.GetHeight()));
				//}
				//else
				//{	//two lines text
				//  currentFont = new Font(this.Font.FontFamily, (float)(this.Font.Size * 0.8));
				//  stringRectangle = new Rectangle(
				//      offset, Convert.ToInt32((this.Height - currentFont.GetHeight() * 1.8) / 2),
				//      wideSpace - oneThirdHeight, Convert.ToInt32(currentFont.GetHeight() * 2));
				//}
				stringFormat = new System.Drawing.StringFormat();
			}

			//tab drawing
			switch (tabSelection)
			{
				case TabSelectionType.Unselected:
					this.ForeColor = Color.Black;
					g.DrawLines(new Pen(new SolidBrush(UNSELECTED_OUTLINE_COLOR), FRAME_WIDTH),
						new Point[] { first_unSel, second_unSel, third_unSel, fourth_unSel, fifth_unSel });
					break;
				case TabSelectionType.Selected:
					this.ForeColor = Color.WhiteSmoke ;
					g.FillPolygon(new SolidBrush(selectedBackGroundColor),
						new Point[] { first_sel, second_sel, third_sel, fourth_sel, fifth_sel });
					break;
				case TabSelectionType.UnselectedAlarm:
					this.ForeColor = UNSELECTED_ALARM_OUTLINE_COLOR;
					g.DrawLines(new Pen(new SolidBrush(UNSELECTED_ALARM_OUTLINE_COLOR), FRAME_WIDTH),
						new Point[] { first_unSel, second_unSel, third_unSel, fourth_unSel, fifth_unSel });
					break;
				case TabSelectionType.SelectedAlarm:
					this.ForeColor = Color.WhiteSmoke ;
					g.FillPolygon(new SolidBrush(SELECTED_ALARM_BACKGROUND_COLOR),
						new Point[] { first_sel, second_sel, third_sel, fourth_sel, fifth_sel });
					break;
        //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
        case TabSelectionType.UnselectedWarning:
          this.ForeColor = UNSELECTED_WARNING_OUTLINE_COLOR;
          g.DrawLines(new Pen(new SolidBrush(UNSELECTED_WARNING_OUTLINE_COLOR), FRAME_WIDTH),
            new Point[] { first_unSel, second_unSel, third_unSel, fourth_unSel, fifth_unSel });
          break;
        case TabSelectionType.SelectedWarning:
          this.ForeColor = Color.WhiteSmoke;
          //this.ForeColor = SELECTED_WARNING_BACKGROUND_COLOR;
          g.FillPolygon(new SolidBrush(SELECTED_WARNING_BACKGROUND_COLOR),
            new Point[] { first_sel, second_sel, third_sel, fourth_sel, fifth_sel });
          break;

				case TabSelectionType.Link:
				default:
					this.ForeColor = LINK_COLOR;
					g.DrawLines(new Pen(new SolidBrush(LINK_COLOR), FRAME_WIDTH),
						new Point[] { first_unSel, second_unSel, third_unSel, fourth_unSel, fifth_unSel });
					break;
			}
			//text drawing
			AdaptStringForComponentSize(g, this,
				(vertical) ? stringRectangle.Height : stringRectangle.Width, this.Font);
			g.DrawString(this.Text, currentFont, new SolidBrush(this.ForeColor), stringRectangle, stringFormat);
		}

		/// <summary>
		/// Resize the internal component according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (FontResizing != 1)
			{
				Font = new Font(Font.FontFamily, 
					Convert.ToInt32(Font.Size * FontResizing), Font.Style);
				Invalidate();
			}
		}

		/// <summary>
		/// Handler for click on the Tab or the PictureBox
		/// </summary>
		private void Tab_Icon_Click(object sender, System.EventArgs e)
		{
			if (this.Click != null)
				this.Click(sender, e);
		}
	}
}
