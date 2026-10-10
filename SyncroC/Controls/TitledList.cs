using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;
using SyncroC.Utility;

namespace SyncroC.Controls
{
	/// <summary>
	/// TitledList Styles.
	/// </summary>
	public enum TitledListStyles
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
	/// Summary description for TitledList.
	/// </summary>
	public class TitledList : SyncroC.Controls.SyncroUserControl, IValidableControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		private IContainer components;
		private System.Windows.Forms.Label lbl_Title;
    private MyDomainUpDown dud_Value;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private ArrayList itemData;

		//Constants
		/// <summary> Frame width for No Title style (1)</summary>
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

		/// <summary>
		/// Event raised when the selection ionto the grid changes.
		/// </summary>
		[Description("Event raised when the selection ionto the grid changes."), Category("Fosbersoft")]
		public event EventHandler SelectedItemChanged;

		/// <summary>
		/// Event raised when the control gets the focus.
		/// </summary>
		[Description("Event raised when the control gets the focus."), Category("Fosbersoft")]
		public new event EventHandler GotFocus;

		/// <summary>The style of the component.</summary>
		private TitledListStyles style;
		/// <summary>
		/// Gets or Sets the component style.
		/// </summary>
		[Category("FosberSoft")]
		public TitledListStyles Style
		{
			get { return style; }
			set
			{
				style = value;
				Invalidate();
			}
		}

		private Color originalFrameColor;
		private bool firstFrameColorSet = true;


		/// <summary>get or set the frame color.</summary>
		private Color frameColor;// = ColConv.Convert(SyncroColor.White);
		/// <summary>
		/// Gets or Sets the color of the frame.
		/// </summary>
		[Category("FosberSoft")]
		public SyncroColor FrameColor
		{
			get { return ColConv.Convert(frameColor); }
			set
			{
				frameColor = ColConv.Convert(value);
				if (firstFrameColorSet)
				{
					firstFrameColorSet = false;
					originalFrameColor = frameColor;
				}
				if (style == TitledListStyles.Normal)
					lbl_Title.BackColor = frameColor;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the foreColor of the Title.
		/// </summary>
		[Category("FosberSoft")]
		public SyncroColor TitleForeColor
		{
			get { return ColConv.Convert(lbl_Title.ForeColor); }
			set { lbl_Title.ForeColor = ColConv.Convert(value); }
		}

		/// <summary>
		/// Gets or sets the title BackColor.
		/// </summary>
		[Category("FosberSoft")]
		public SyncroColor TitleBackColor
		{
			get { return ColConv.Convert(lbl_Title.BackColor); }
			set { lbl_Title.BackColor = ColConv.Convert(value); }
		}

		/// <summary>
		/// Gets or sets the value BackColor.
		/// </summary>
		[Category("FosberSoft")]
		public SyncroColor ValueBackColor
		{
			get { return ColConv.Convert(dud_Value.BackColor); }
			set
			{
				dud_Value.BackColor = ColConv.Convert(value);
				this.BackColor = ColConv.Convert(value);
			}
		}

		/// <summary>
		/// Gets or sets the text to be written into the control as a title.
		/// </summary>
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
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
		/// Gets or sets the Enabled property
		/// </summary>
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
			Category("FosberSoft")]
		public new bool Enabled
		{
			get { return dud_Value.Enabled; }
			set
			{
				dud_Value.Enabled = value;
				this.Invalidate();
			}
		}
		/// <summary>
		/// The number of items contained in the list.
		/// </summary>
		[Category("FosberSoft")]
		public int Count
		{
			get { return dud_Value.Items.Count; }
		}

		/// <summary>
		/// Determine if the control is readonly.
		/// </summary>
		private bool readOnly;

		[Category("FosberSoft")]
		public bool ReadOnly
		{
			get { return this.readOnly; }
			set
			{
				readOnly = value;
				dud_Value.InterceptArrowKeys = !value;
				lbl_Title.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the SelectedItem of the list.
		/// </summary>
		[Category("FosberSoft")]
		public string SelectedItem
		{
			get
			{
				if (dud_Value.SelectedItem == null)
					return String.Empty;
				else
					return dud_Value.SelectedItem.ToString();
			}
			set
			{
				string s = null;
				if (value != null)
					s = value;
				if (dud_Value.Items.Contains(s))
				{
					dud_Value.SelectedItem = s;
					this.frameColor = originalFrameColor; 
				}
				else if (s != null && !s.Trim().Equals(string.Empty))
					this.frameColor = Color.Red;
				dud_Value.Text = s;
				this.Invalidate();
			}
		}

		[Category("FosberSoft")]
		public string Value
		{
			//CC 21_03_06 in Frm_Quality se si entra in modifica e una carta della qualita 
			//non è piu nel database la selecteditem è vuota ma il testo c'è sempre e credo 
			//nella qualita vada mantenuto: la property "Value" deve restituire cio che è visualizzato 
			//nel controllo e non cio che è selezionato (a parte il caso descritto sopra la selectedItem 
			//e ciò che è visualizzato corrispondono sempre..)

			//get { return (SelectedItem.Equals(String.Empty)) ? " " : SelectedItem; }
			get { return (dud_Value.Text.Equals(String.Empty)) ? " " : dud_Value.Text; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool Contains(object obj)
		{
			return this.itemData.Contains(obj);
		}

		/// <summary>
		/// Gets a value indicating whether the control has the input focus. 
		/// </summary>
		[Category("FosberSoft")]
		public override bool Focused
		{
			get { return dud_Value.Focused; }
		}

		/// <summary>
		/// Gets a value indicating whether the item collection is sorted. 
		/// </summary>
		[Category("FosberSoft")]
		public bool Sorted
		{
			get { return dud_Value.Sorted; }
			set { dud_Value.Sorted = value; }
		}

		/// <summary>
		/// Gets or Sets a the selected item index 
		/// </summary>
		[Category("FosberSoft")]
		public int SelectedIndex
		{
			get { return dud_Value.SelectedIndex; }
			set
			{
				if ((value >= -1) && (value < dud_Value.Items.Count))
				{
					dud_Value.SelectedIndex = value;
					//cc 19/09/05 se da noia togliete pure questo if e l'assegnamento successivo e poi vi spiego perche era stata aggiunta
					if (value == -1)
						dud_Value.Text = string.Empty;
				}
				else
				{
					dud_Value.SelectedIndex = -1;
					Logger.LogError(this, "Cannot set index " + value.ToString() + ": there are " + dud_Value.Items.Count.ToString() + " items!");
				}
			}
		}

    private bool canCancel = false;
    /// <summary>
    /// Gets or Sets a the selected item index 
    /// </summary>
    [Category("FosberSoft")]
    public bool CanCancel
    {
      get { return canCancel; }
      set { canCancel = value; }
    }
    
		/// <summary>
		/// Add an item to the list.
		/// </summary>
		/// <param name="s">The item to be added to the list. </param>
		public void AddItem(string s)
		{
			object slectedItem = dud_Value.SelectedItem;
			this.dud_Value.Items.Add(s);
			this.itemData.Add(s);

			if (this.dud_Value.Sorted)
			{
				this.dud_Value.Items.Sort();
				this.itemData.Sort();
			}

			this.dud_Value.SelectedItem = slectedItem;
		}

		/// <summary>
		/// Add a range of item to the list.
		/// </summary>
		/// <param name="c">The collection of item to be added to the list. </param>
		public void AddRangeItem(ICollection c)
		{
			this.dud_Value.Items.AddRange(c);
			this.itemData.AddRange(c);

			if (this.dud_Value.Sorted)
			{
				this.dud_Value.Items.Sort();
				this.itemData.Sort();
			}
		}


		/// <summary>
		/// Add an item and its data to the list.
		/// </summary>
		/// <param name="translation">The item to be added to the item list. </param>
		/// <param name="key">The data of the item to be added to the itemData list. </param>
		public void AddItem(string translation, string key)
		{
			this.dud_Value.Items.Add(translation);
			this.itemData.Add(key);

			if (this.dud_Value.Sorted)
			{
				this.dud_Value.Items.Sort();
				this.itemData.Sort();
			}
		}

		/// <summary>
		/// Add a range of item and their data to the list.
		/// </summary>
		/// <param name="translation">The collection of items to be added to the item list. </param>
		/// <param name="key">The collection of data of the item to be added to the itemData list. </param>
		public void AddRangeItem(ICollection translations, ICollection keys)
		{
			this.dud_Value.Items.AddRange(translations);
			this.itemData.AddRange(keys);

			if (this.dud_Value.Sorted)
			{
				this.dud_Value.Items.Sort();
				this.itemData.Sort();
			}
		}

		/// <summary>
		/// Remove an item from the list.
		/// </summary>
		/// <param name="s">The item to be removed from the list. </param>
		public void RemoveItem(string s)
		{
			if (dud_Value.Items.Contains(s))
			{
				if (dud_Value.Text.Equals(s))
				{
					if (ReadOnly)
					{
						this.frameColor = Color.Red;
						this.Refresh();
					}
					else
					{
						dud_Value.Text = "";
					}
				}
				this.itemData.RemoveAt(dud_Value.Items.IndexOf(s));
				dud_Value.Items.Remove(s);
			}
		}

		/// <summary>Design time height of the lbl_Title label.</summary>
		private int titleHeight = 20;
		/// <summary>Width of the frame.</summary>
		private int frameWidth = NORMAL_FRAMEWIDTH;

		/// <summary>
		/// Builder.
		/// </summary>
		public TitledList()
		{
			InitializeComponent();
			this.itemData = new ArrayList();
			this.dud_Value.Sorted = true;
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
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (!(VerticalResizing == 1 && FontResizing == 1))
			{
				lbl_Title.Font = new Font(lbl_Title.Font.FontFamily,
					Convert.ToInt32(lbl_Title.Font.Size * FontResizing), lbl_Title.Font.Style);
				this.Font = new Font(this.Font.FontFamily,
					Convert.ToInt32(this.Font.Size * FontResizing), this.Font.Style);
				titleHeight = Convert.ToInt32(titleHeight * VerticalResizing);
				Invalidate();
			}
		}

		private void ArrangeComponents()
		{
			switch (style)
			{
				case TitledListStyles.NoTitle:
					frameWidth = NOTITLE_FRAMEWIDTH;
					lbl_Title.Location = new Point(0, 0);
					lbl_Title.Width = 0;
					lbl_Title.Height = 0;
					break;

				case TitledListStyles.Normal:
					frameWidth = NORMAL_FRAMEWIDTH * 2;
					lbl_Title.BackColor = frameColor;
					lbl_Title.Location = new Point(NORMAL_FRAMEWIDTH, 0);
					lbl_Title.Width = this.Width - NORMAL_FRAMEWIDTH;
					lbl_Title.Height = titleHeight;
					break;

				case TitledListStyles.Light:
					frameWidth = LIGTH_FRAMEWIDTH;
					lbl_Title.BackColor = dud_Value.BackColor;
					lbl_Title.Location = new Point(0, 0);
					lbl_Title.Width = this.Width;
					lbl_Title.Height = titleHeight;
					break;

				case TitledListStyles.Framed:
					frameWidth = FRAMED_FRAMEWIDTH;
					lbl_Title.Location = new Point(frameWidth, frameWidth);
					lbl_Title.Width = this.Width - frameWidth;
					lbl_Title.Height = titleHeight - frameWidth;
					break;
			}
			int y = (lbl_Title.Location.Y + lbl_Title.Height);
			int errorProviderSpace = 15;
			int arrowsWidth = 18;
			dud_Value.Width = this.Width - frameWidth - errorProviderSpace + arrowsWidth;
			//dud_Value.Height = this.Height - lbl_Title.Height;
			dud_Value.Location = new Point(frameWidth + errorProviderSpace, //15 has been added to leave enough space for the error provider
				Math.Max(y + ((this.Height - (y + dud_Value.Height)) / 2), frameWidth));

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Location = new Point(this.Width - pct_icon.Width, this.titleHeight + 3);
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
      this.lbl_Title = new System.Windows.Forms.Label();
      this.dud_Value = new MyDomainUpDown();
      this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
      
      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit(); 
      
      this.SuspendLayout();
      // 
      // lbl_Title
      // 
      this.lbl_Title.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Title.Location = new System.Drawing.Point(2, 2);
      this.lbl_Title.Name = "lbl_Title";
      this.lbl_Title.Size = new System.Drawing.Size(100, 20);
      this.lbl_Title.TabIndex = 1;
      this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Title.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_Title_Paint);
      // 
      // dud_Value
      // 
      this.dud_Value.BackColor = System.Drawing.SystemColors.Control;
      this.dud_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dud_Value.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.errorProvider.SetIconAlignment(this.dud_Value, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
      this.dud_Value.Location = new System.Drawing.Point(16, 16);
      this.dud_Value.Name = "dud_Value";
      this.dud_Value.ReadOnly = true;
      this.dud_Value.Size = new System.Drawing.Size(80, 18);
      this.dud_Value.TabIndex = 0;
      this.dud_Value.SelectedItemChanged += new System.EventHandler(this.dud_Value_SelectedItemChanged);
      this.dud_Value.GotFocus += new System.EventHandler(this.dud_Value_GotFocus);
      this.dud_Value.LostFocus += new System.EventHandler(this.dud_Value_LostFocus);
      this.dud_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dud_Value_KeyDown);
      // 
      // errorProvider
      // 
      this.errorProvider.ContainerControl = this;
      this.errorProvider.DataMember = "";
      // 
      // TitledList
      // 
      this.Controls.Add(this.dud_Value);
      this.Controls.Add(this.lbl_Title);
      this.Name = "TitledList";
      this.Size = new System.Drawing.Size(102, 48);
      this.TextChanged += new System.EventHandler(this.TitledList1_TextChanged);
      this.FontChanged += new System.EventHandler(this.TitledListl_FontChanged);
      this.SizeChanged += new System.EventHandler(this.TitledListl_SizeChanged);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dud_Value_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();

      this.ResumeLayout(false);

		}
		#endregion

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

			ArrangeComponents();
			Graphics g = e.Graphics;
			//drawing lines extreme coordinates
			Point lowLeft = new Point(0, this.Height);
			Point hightLeft = new Point(0, 0);
			Point hightRight = new Point(this.Width, 0);
			//drawing lines according to the style
			if (style == TitledListStyles.Light)
			{
				hightLeft = new Point(0, lbl_Title.Location.Y + lbl_Title.Height + LIGTH_FRAMEWIDTH);
				hightRight = new Point(this.Width, lbl_Title.Location.Y + lbl_Title.Height + LIGTH_FRAMEWIDTH);
			}
			g.DrawLines(
				new Pen(new SolidBrush(frameColor), frameWidth),
				new Point[]{
							   lowLeft, hightLeft, hightRight});
			AdaptStringForComponentSize(g, lbl_Title, lbl_Title.Width, lbl_Title.Font);
		}

		private void TitledListl_SizeChanged(object sender, System.EventArgs e)
		{
			ArrangeComponents();
		}

		private void TitledList1_TextChanged(object sender, System.EventArgs e)
		{
			lbl_Title.Text = Text;
		}

		private void TitledListl_FontChanged(object sender, System.EventArgs e)
		{
			dud_Value.Font = this.Font;
			Invalidate();
		}

		/// <summary>
		/// Draw the little equilateral triangle next to the title, only if it is not readonly.
		/// </summary>
		private void lbl_Title_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (!readOnly && !style.Equals(TitledListStyles.NoTitle))
			{
				if (sender is Control)
					DrawTriangle((Control)sender, e.Graphics, 0);
			}
		}

		/// <summary>
		/// Draw a little equilateral triangle on the passed control.
		/// </summary>
		/// <param name="control">the control where to draw the tringle.</param>
		/// <param name="g">the graphics object for the control.</param>
		/// <param name="offset">...</param>
		private void DrawTriangle(Control control, Graphics g, int offset)
		{
			//height of the little triangle (60% of the font height)
			int TriangleHeigth = Convert.ToInt32((control.Font.Height * 60) / 100);
			//distance of the little triangle from the label border
			int BorderDistance = Convert.ToInt32((control.Height - TriangleHeigth) / 2);
			//length of the little triangle side
			int TriangleSide = Convert.ToInt32(TriangleHeigth / (Math.Sqrt(3) / 2));
			//little triangle corners coordinates
			Point topLeft = new Point(
				control.Width - BorderDistance - TriangleSide + offset,//x 
				BorderDistance);//y
			Point topRight = new Point(
				control.Width - BorderDistance + offset,//x
				BorderDistance);//y
			Point low = new Point(
				control.Width - BorderDistance - Convert.ToInt32(TriangleSide / 2) + offset,//x
				BorderDistance + TriangleHeigth);//y
			//drawing a filled triangle 
			g.FillPolygon(
				new SolidBrush(control.ForeColor),
				new Point[] { topLeft, topRight, low });
		}

		/// <summary>
		/// Reverse the colors of the value when the user enter into the control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dud_Value_GotFocus(object sender, System.EventArgs e)
		{
			dud_Value.BackColor = System.Drawing.SystemColors.Highlight;
			dud_Value.ForeColor = Color.WhiteSmoke ;
			if (this.GotFocus != null)
				this.GotFocus(this, e);
		}
		/// <summary>
		/// undo the color reverse of the value when the user leave the control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dud_Value_LostFocus(object sender, System.EventArgs e)
		{
			dud_Value.BackColor = this.BackColor;
			dud_Value.ForeColor = Color.Black;
			//if(!readOnly)
			//{
			//  if(dud_Value.Text!="")
			//    SetError("");
			//  else
			//    SetError("Not Set");
			//}
		}

		/// <summary>
		/// Set the error provider for the control.
		/// </summary>
		/// <param name="error">The string to be showed in the tooltip.</param>
		public override void SetError(string error)
		{
			errorProvider.SetError(dud_Value, error);
		}

		/// <summary>
		/// Clear the items.
		/// </summary>
		public void ClearItems()
		{
			this.itemData.Clear();
			dud_Value.Items.Clear();
			dud_Value.Text = "";
			Invalidate();
		}

		/// <summary>
		/// Unselects eventually selected item. 
		/// </summary>
		public void UnSelectItem()
		{
			dud_Value.Text = "";
			dud_Value.SelectedItem = null;
			this.Refresh();
		}

		private void dud_Value_SelectedItemChanged(object sender, System.EventArgs e)
		{
			this.frameColor = (!dud_Value.Items.Contains(dud_Value.Text)
				&& !dud_Value.Text.Equals(string.Empty)) //cc 19/09/05 se da noia togliete pure questa condizione in end e poi vi spiego perche era stata aggiunta
				? Color.Red : originalFrameColor;
			this.Invalidate();
			//throws the event
			if (SelectedItemChanged != null)
				SelectedItemChanged(this, e);
		}

		public string SelectedItemData
		{
			get
			{
				if (this.dud_Value.SelectedIndex < 0) return string.Empty;
				return (string)this.itemData[this.dud_Value.SelectedIndex];
			}
			set
			{
				if (this.itemData.Contains(value))
				{
					dud_Value.SelectedIndex = this.itemData.IndexOf(value);
					this.frameColor = originalFrameColor;
				}
				else if (value != null && !value.Trim().Equals(string.Empty))
					this.frameColor = Color.Red;
				this.Invalidate();
			}
		}

    //ABello_FI Aggiunta gestione CutToPattern
    private void dud_Value_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.readOnly)
      {
        if (this.canCancel)
        {
          if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Cancel))
          {
            this.dud_Value.Text = "";
          }
        }
      }
    }

    //ABELLO_FI08/04/2010_1 Sui controlli TitledList del Client anche quando erano readOnly se si girava la rotellina del mouse cambiavano i valori.
    private class MyDomainUpDown : DomainUpDown
    {
      protected override void OnMouseWheel(MouseEventArgs e)
      {
        if (!this.ReadOnly)
          base.OnMouseWheel(e);
      }
    }
	}
}
