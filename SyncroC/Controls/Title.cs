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
	///	Possible styles of the Title component.
	/// </summary>
	public enum TitleStyles : int
	{
		/// <summary>24 pixels height; 6 pixels frame width.</summary>
		Big,
		/// <summary>20 pixels height; 4 pixels frame width.</summary>
		Small,
		/// <summary>1 pixel height; 1 pixel frame width.</summary>
		Light,
		/// <summary>1 pixel height; 1 pixel frame width.</summary>
		NoTitle
	}
	
	/// <summary>
	/// Summary description for Title.
	/// </summary>
	public class Title : SyncroC.Controls.SyncroUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lbl_Title;

		/// <summary>Current component's style.</summary>
		private TitleStyles style = TitleStyles.Big;
		/// <summary>
		/// Gets or sets the component's style.
		/// </summary>
		[Description("Gets or sets the component's style."), Category("FosberSoft")]
		public TitleStyles Style
		{
			get {	return style;	}
			set
			{ 
				style = value; 
				switch (style)
				{
					case TitleStyles.Big:
						titleHeight = TITLE_HEIGHT_BIG;
						frameWidth = FRAME_WIDTH_BIG;
						currentFontSize = SIZE_FONT_BIG;
						lbl_Title.BackColor = ColConv.Convert(color);
						break;
					case TitleStyles.Small:
						titleHeight = TITLE_HEIGHT_SMALL;
						frameWidth = FRAME_WIDTH_SMALL;
						currentFontSize = SIZE_FONT_SMALL;
						lbl_Title.BackColor = ColConv.Convert(color);
						break;
					case TitleStyles.Light:
						titleHeight = TITLE_HEIGHT_LIGHT;
						frameWidth = FRAME_WIDTH_LIGHT;
						currentFontSize = SIZE_FONT_LIGHT;
						lbl_Title.BackColor = Color.WhiteSmoke;
						break;
					case TitleStyles.NoTitle:
						titleHeight = TITLE_HEIGHT_NOTITLE;
						frameWidth = FRAME_WIDTH_NOTITLE;
						currentFontSize = SIZE_FONT_LIGHT;
						lbl_Title.BackColor = Color.WhiteSmoke ;
						break;
				}
				SetUpSizes();
			}
		}

		//Constants		
		/// <summary> Hight of the title for Big style</summary>
		private const int TITLE_HEIGHT_BIG = 24;
		/// <summary> Hight of the title for Small style</summary>
		private const int TITLE_HEIGHT_SMALL = 20;
		/// <summary> Hight of the title for Light style</summary>
		private const int TITLE_HEIGHT_LIGHT = 20;
		/// <summary> Hight of the title for NoTitle style</summary>
		private const int TITLE_HEIGHT_NOTITLE = 1;
		/// <summary> Width of the frame for Big style</summary>
		private const int FRAME_WIDTH_BIG = 6;
		/// <summary> Width of the frame for Small style</summary>
		private const int FRAME_WIDTH_SMALL = 4;
		/// <summary> Width of the frame for Light style</summary>
		private const int FRAME_WIDTH_LIGHT = 1;
		/// <summary> Width of the frame for NOTitle style</summary>
		private const int FRAME_WIDTH_NOTITLE = 1;

		/// <summary>Font size for Big style </summary>
		private const float SIZE_FONT_BIG = 13F;
		/// <summary>Font size for Small style </summary>
		private const float SIZE_FONT_SMALL = 10F;
		/// <summary>Font size for LIGHT style </summary>
		private const float SIZE_FONT_LIGHT = 10F;

		/// <summary>
		/// Gets or sets the color of the Title component.
		/// </summary>
		[Description("Gets or sets the color of the Title component."), Category("FosberSoft")]
		public SyncroColor color
		{
			get { return ColConv.Convert(this.BackColor); }
			set 
			{
				if (style != TitleStyles.Light)
				{
					lbl_Title.BackColor = ColConv.Convert(value);
				}
				else
				{
					lbl_Title.BackColor = Color.WhiteSmoke ;
				}
				this.BackColor = ColConv.Convert(value);
			}
		}

		/// <summary>Current height of the title.</summary>
		private int titleHeight = TITLE_HEIGHT_BIG;

		/// <summary>Current width of the frame.</summary>
		private int frameWidth = FRAME_WIDTH_BIG;

		/// <summary>Current size of the font.</summary>
		private float currentFontSize = SIZE_FONT_BIG;

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

		[Category("Fosbersoft")]
		public new event EventHandler Click;

		/// <summary>
		/// Builder.
		/// </summary>
		public Title()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Return the current Height of the title which is affected by the adaptation at the screen size
		/// </summary>
		public int getCurrentTitleHeight()
		{
			return titleHeight;
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
			this.lbl_Title = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_Title
			// 
			this.lbl_Title.BackColor = System.Drawing.Color.Black;
			this.lbl_Title.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Title.Location = new System.Drawing.Point(6, 0);
			this.lbl_Title.Name = "lbl_Title";
			this.lbl_Title.Size = new System.Drawing.Size(94, 24);
			this.lbl_Title.TabIndex = 0;
			this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_Title.Click += new System.EventHandler(this.lbl_Title_Click);
			// 
			// Title
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.lbl_Title);
			this.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.Name = "Title";
			this.Size = new System.Drawing.Size(104, 64);
			this.Load += new System.EventHandler(this.Title_Load);
			this.TextChanged += new System.EventHandler(this.Title_TextChanged);
			this.BackColorChanged += new System.EventHandler(this.Title_BackColorChanged);
			this.SizeChanged += new System.EventHandler(this.Title_SizeChanged);
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

		private void Title_TextChanged(object sender, System.EventArgs e)
		{
			lbl_Title.Text = Text;
		}

		private void Title_BackColorChanged(object sender, System.EventArgs e)
		{
			if (style != TitleStyles.Light)
			{
				lbl_Title.BackColor = this.BackColor;
			}
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
			//set label lbl_Title size and location
			switch (style)
			{
				case TitleStyles.Big:
				case TitleStyles.Small:
					lbl_Title.Location = new Point(frameWidth, lbl_Title.Location.Y);
					lbl_Title.Width = this.Width - lbl_Title.Location.X;
					lbl_Title.Height = titleHeight;
					break;
				case TitleStyles.Light:
					lbl_Title.Location = new Point(frameWidth - 1, lbl_Title.Location.Y);
					lbl_Title.Width = this.Width;
					lbl_Title.Height = titleHeight - 1;
					break;
				case TitleStyles.NoTitle:
					lbl_Title.Location = new Point(0, lbl_Title.Location.Y);
					lbl_Title.Width = this.Width;
					lbl_Title.Height = titleHeight - 1;
					break;
			}
			//set Font
      lbl_Title.Font = new Font(lbl_Title.Font.FontFamily, currentFontSize, lbl_Title.Font.Style);
			//set region
			GraphicsPath gp = new GraphicsPath();
			gp.AddPolygon(new Point[]{
										 new Point(0, 0),
										 new Point(this.Width, 0),
										 new Point(this.Width, titleHeight),
										 new Point(frameWidth, titleHeight),
										 new Point(frameWidth, this.Height),
										 new Point(0, this.Height)});
			this.Region = new Region(gp);
		}

		private void lbl_Title_Click(object sender, System.EventArgs e)
		{
			if (Click != null)
				Click(sender, e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			AdaptStringForComponentSize(e.Graphics, lbl_Title, lbl_Title.Width, lbl_Title.Font);
		}
	}
}
