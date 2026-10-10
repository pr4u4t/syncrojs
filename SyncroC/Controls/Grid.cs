using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel.Design;

using SyncroC.Controls.Colors;
using SyncroC.Controls.Interfaces;
using SyncroC.Utility;

namespace SyncroC.Controls
{
	/// <summary>
	/// Options for space between columns.
	/// </summary>
	public enum GridColumnSpacing : int
	{
		/// <summary>4 pixels</summary>
		Narrow = 4,
		/// <summary>6 pixels</summary>
		Medium = 6,
		/// <summary>16 pixels</summary>
		Large = 16
	}
	
	/// <summary>
	/// Grid Styles.
	/// </summary>
	public enum GridStyles
	{
		/// <summary>column header softly underlined.</summary>
		Light,
		/// <summary>column header with frame.</summary>
		Framed
	}

	/// <summary>
	/// Grid.
	/// </summary>
	public class Grid : SyncroC.Controls.SyncroUserControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>list.</summary>
		private System.Windows.Forms.ListView listView;
		/// <summary>Array of labels for column lines.</summary>
		private Label[] columnLabels;

		/// <summary> Frame width for Light style</summary>
		private const int LIGHT_FRAMEWIDTH = 1;
		/// <summary> Frame width for Framed style</summary>
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
		private GridStyles style;
		/// <summary>
		/// Gets or sets the component style.
		/// </summary>
		[Description("Gets or sets the component style."), Category("FosberSoft")]
		public GridStyles Style
		{
			get {	return style;	}
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
			get {	return ColConv.Convert(frameColor);	}
			set 
			{	
				frameColor = ColConv.Convert(value);	
				Invalidate();
			}
		}

		/// <summary>Current foreColor of the title</summary>
		private Color titleForeColor = ColConv.Convert(SyncroColor.Black);
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

		/// <summary>Current backColor of the title.</summary>
		private Color titleBackColor = ColConv.Convert(SyncroColor.White);
		/// <summary>
		/// Gets or sets the title BackColor.
		/// </summary>
		[Description("Gets or sets the backColor of the Title."), Category("FosberSoft")]
		public SyncroColor TitleBackColor
		{
			get {	return ColConv.Convert(titleBackColor);	}
			set 
			{	
				titleBackColor = ColConv.Convert(value);
				Invalidate();
			}
		}

		/// <summary>Current color of the even items of the grid.</summary>
		private Color evenItemsColor = ColConv.Convert(SyncroColor.White);
		/// <summary>
		/// Gets or sets the color of the even items of the grid.
		/// </summary>
		[Description("Gets or sets the color of the even items of the grid."), Category("FosberSoft")]
		public SyncroColor EvenItemsColor
		{
			get {	return ColConv.Convert(evenItemsColor);	}
			set {	evenItemsColor = ColConv.Convert(value);	}
		}

		/// <summary>Current color of the odd items of the grid.</summary>
		private Color oddItemsColor = ColConv.Convert(SyncroColor.White);
		/// <summary>
		/// Gets or sets the color of the odd items of the grid.
		/// </summary>
		[Description("Gets or sets the color of the odd items of the grid."), Category("FosberSoft")]
		public SyncroColor OddItemsColor
		{
			get {	return ColConv.Convert(oddItemsColor);	}
			set {	oddItemsColor = ColConv.Convert(value);	}
		}

		/// <summary>Collection of ColumnHeaders of the Grid.</summary>
		private ListView.ColumnHeaderCollection columns;
		/// <summary>
		/// Collection of ColumnHeaders of the Grid.
		/// </summary>
		[	//property editor settings.
		DesignerSerializationVisibility(
			DesignerSerializationVisibility.Content), 
		Editor(
			typeof(System.ComponentModel.Design.CollectionEditor),
			typeof(System.Drawing.Design.UITypeEditor)), 
		Category("FosberSoft")
		]
		public ListView.ColumnHeaderCollection Columns	
		{
			get
			{
				if (columns == null)
					columns = new ListView.ColumnHeaderCollection(this.listView);
				return this.listView.Columns;
			}
		}

		/// <summary>Current Space between two columns.</summary>
		private int columnSpacing = (int)GridColumnSpacing.Narrow;
		/// <summary>
		/// Gets or sets the ColumnSpacing.
		/// </summary>
		[Description("Gets or sets the ColumnSpacing."), Category("FosberSoft")]
		public GridColumnSpacing ColumnSpacing
		{
			get 
			{
				switch(columnSpacing)
				{
					case 4 : return GridColumnSpacing.Narrow;
					case 8 : return GridColumnSpacing.Medium;
					case 16: return GridColumnSpacing.Large;
					default: return GridColumnSpacing.Narrow;
				}
			}
			set 
			{	
				columnSpacing = (int)value;
				Invalidate();
			}
		}

		/// <summary>Determine if item indexes have to be showed.</summary>
		private bool showIndexesItem = false;
		/// <summary>
		/// Determine if item indexes have to be showed.
		/// </summary>
		[Description("Determine if item indexes have to be showed."), Category("FosberSoft")]
		public bool ShowIndexesItem
		{
			get {	return showIndexesItem;	}
			set {	showIndexesItem = value;}
		}

		/// <summary>Determine if the first item has a different forecolor.</summary>
		private bool showForecolorItem = false;
		/// <summary>
		/// Determine if the first item has a different forecolor.
		/// </summary>
		[Description("Determine if the first item has a different forecolor."), Category("FosberSoft")]
		public bool ShowForecolorItem
		{
			get {	return showForecolorItem;	}
			set {	showForecolorItem = value;}
		}

		/// <summary>
		/// Determine if grid elements are sorted.
		/// </summary>
		[Description("Determine if grid elements are sorted."), Category("FosberSoft")]
		public System.Windows.Forms.SortOrder Sorting
		{
			get {	return listView.Sorting;	}
			set {	listView.Sorting = value;}
		}

		/// <summary>
		/// Indicating whether the selected item in the control remains highlighted when the control loses focus.
		/// </summary>
		[Description("Indicating whether the selected item in the control remains highlighted when the control loses focus."), Category("FosberSoft")]
		public bool HideSelection
		{
			get {	return listView.HideSelection;	}
			set {	listView.HideSelection = value;}
		}

		/// <summary>
		/// Gets a value indicating wheather the control has the input focus.
		/// </summary>
		public override bool Focused
		{
			get {	return listView.Focused;	}
		}

		/// <summary>
		/// Gets or sets a value indicating whether multiple items can be selected.
		/// </summary>
		[Description("Gets or sets a value indicating whether multiple items can be selected."), Category("FosberSoft")]
		public bool MultiSelect
		{
			get {	return listView.MultiSelect;	}
			set {	listView.MultiSelect = value;}
		}

		/// <summary>
		/// Event raised when the selection ionto the grid changes.
		/// </summary>
		[Description("Event raised when the selection ionto the grid changes."), Category("Fosbersoft")]
		public event EventHandler SelectedIndexChanged;

		[Category("Fosbersoft")]
		public new event EventHandler DoubleClick;

    /// <summary>Current height of the title.</summary>
		private int titleHeight = 20;
		/// <summary>Current font of the column headers.</summary>
		private Font columnHeaderFont = new System.Drawing.Font("Arial Unicode MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

		/// <summary>Current Width of the frame.</summary>
		private int frameWidth = LIGHT_FRAMEWIDTH;

		/// <summary>
		/// Builder.
		/// </summary>
		public Grid()
		{
			InitializeComponent();

			listView.Font = this.Font;
			ColorItems(0);
		}

		/// <summary>
		/// Add an item to the control.
		/// </summary>
		/// <param name="item">The item to be added.</param>
		public void AddItem(ListViewItem item)
		{
			ListViewItem newItem = null;
			if (showIndexesItem)
			{	//if Index of items has to be showed a new subitem containing
				//the index is added to the item
				newItem = new ListViewItem((listView.Items.Count + 1).ToString());
				newItem.ForeColor = item.ForeColor;
				newItem.UseItemStyleForSubItems = item.UseItemStyleForSubItems;
				foreach ( ListViewItem.ListViewSubItem subItem in item.SubItems)
					newItem.SubItems.Add(subItem);
			}
			else
				newItem = item;
			//set the backcolor of the new item before adding it to the listView
			newItem.BackColor = ((listView.Items.Count % 2) != 0) 
				? evenItemsColor 
				: oddItemsColor;

			if(showForecolorItem)
			{
				//set the forecolor 
				if(listView.Items.Count == 0)
					newItem.ForeColor = Color.Red;
				if(listView.Items.Count == 1)
					newItem.ForeColor = Color.Tomato;
				if(listView.Items.Count == 2)
					newItem.ForeColor = Color.Orange;
			}

			listView.Items.Add(newItem);
		}

		/// <summary>
		/// Modify an item to the control.
		/// </summary>
		/// <param name="item">The item to be Modify.</param>
		public void ModifyItem(ListViewItem item)
		{
			int i=listView.Items.IndexOf(item);
			if(i>=0)
			{
				listView.Items.RemoveAt(i);
				listView.Items.Insert(i,item);
			}
		}


		/// <summary>
		/// Remove an item from the control.
		/// </summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <exception cref="">The exception thrown by the contained ListView.</exception>
		public void RemoveItemAt(int index)
		{
			try 
			{	//remove the item
				if (listView.Items.Count == 1)
					listView.Items.Clear();
				else
				{
					listView.Items.RemoveAt(index);
					if (showIndexesItem)
						for (int i = index; i < listView.Items.Count; i++)
							listView.Items[i].SubItems[0].Text = (i+1).ToString();
					//set the color for every items below the deleted one
					ColorItems(index);
					if (listView.Items.Count != 0)
					{//select a line
						if (index >= listView.Items.Count)
							listView.Items[listView.Items.Count - 1].Selected = true;
						else 
							listView.Items[index].Selected = true;
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("RemoveItemAt", e);
			}
		}

		/// <summary>
		/// Remove items from the grid that have a specified value into the specified subitem.
		/// </summary>
		/// <param name="val">The value that the items which have it into the specified subitem will be removed. </param>
		/// <param name="columnIndex">The subitem where search for the value.</param>
		/// <exception cref="">The exception thrown by the contained ListView or if the specified subItem index is invalid</exception>
		public void RemoveItemsWithVal(string val, int subitemIndex)
		{
			if (subitemIndex < 0 || 
				(listView.Items.Count != 0 && subitemIndex >= listView.Items[0].SubItems.Count))
			{
				throw new Exception("RemoveItemsWithVal - \"" + subitemIndex + "\" is not a valid index of subitems for the grid \"" + this.Name + "\".");
			}

			try
			{
				//remove the item
				int index=-1;
				foreach (ListViewItem item in listView.Items)
				{
					index++;
					if (item.SubItems.Count > subitemIndex && item.SubItems[subitemIndex].Text.Equals(val))
					{
						item.Remove();
						break;
					}
				}

				if (showIndexesItem)
					for (int i = index; i < listView.Items.Count; i++)
						listView.Items[i].SubItems[0].Text = (i+1).ToString();

				//set the color for every items below the deleted one
				ColorItems(index);

				if (listView.Items.Count != 0)
				{
					UnSelectAll();
					//select a line
					if (index >= listView.Items.Count)
						listView.Items[listView.Items.Count - 1].Selected = true;
					else 
						listView.Items[index].Selected = true;
				}
			}
			catch (Exception e)
			{
				throw new Exception("Remove", e);
			}
		}

		/// <summary>
		/// Unselect all grid items.
		/// </summary>
		public void UnSelectAll()
		{
			foreach (ListViewItem item in listView.SelectedItems)
				item.Selected=false;
		}


		/// <summary>
		/// Remove all items from the Grid.
		/// </summary>
		/// <exception cref="">The exception thrown by the contained ListView.</exception>
		public void ClearItems()
		{
			try
			{
				listView.Items.Clear();
			}
			catch(Exception ex)
			{
				throw new Exception("ClearItems", ex);
			}
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
		/// Resize the internal component according to the parameters.
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
			if (! (VerticalResizing == 1 && OrizontalResizing == 1 && FontResizing == 1))
			{
				//font resizing
				columnHeaderFont = new Font(columnHeaderFont.FontFamily, 
					Convert.ToInt32(columnHeaderFont.Size * FontResizing), columnHeaderFont.Style);
				this.Font = new Font(this.Font.FontFamily, 
					Convert.ToInt32(this.Font.Size * FontResizing), this.Font.Style);
				//title height resizing
				titleHeight = Convert.ToInt32(titleHeight * VerticalResizing);
				//column spacing resizing
				columnSpacing = Convert.ToInt32(columnSpacing * VerticalResizing);
				//the column width resizing is managed by the Frm_Base that contains the control
				//because columns are effectively object of the form, the Grid has only a collection
				//that refers to form's ColumnHeader
				Invalidate();
			}
		}

		/// <summary>
		/// Invalidate the userControl and all the inside components.
		/// </summary>
		public new void Invalidate()
		{
			switch (style)
			{
				case GridStyles.Framed:
					frameWidth = FRAMED_FRAMEWIDTH;
					break;
				case GridStyles.Light:
					frameWidth = LIGHT_FRAMEWIDTH;
					break;
			}
			base.Invalidate();
		}

		/// <summary>
		/// Design the aspect of the component.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Visible = (imagePassword != null);

			listView.Location = new Point(0, titleHeight);
			listView.Size = new Size(this.Width, this.Height - titleHeight);
			
			//graphic objects
			Graphics Gr = e.Graphics;
			Pen framePen = new Pen(new SolidBrush(frameColor), frameWidth);
			
			//X coordinate of the left side of the current column
			int Xcoord = listView.Location.X;
			//space between the top of the component and the text of the title
			int textVerticalOffset = Convert.ToInt32((titleHeight - columnHeaderFont.Height) / 2);

			this.SuspendLayout();
			for (int i = 0; i < listView.Columns.Count; i++)
			{
				int currentColumnWidth = ((i + 1) != listView.Columns.Count)
					? listView.Columns[i].Width - (int)columnSpacing
					: listView.Columns[i].Width;

				//positioning of vertical column labels
				if (columnLabels != null && columnLabels.Length > i)
				{	
					columnLabels[i].Height = listView.Height;
					columnLabels[i].Width = (currentColumnWidth <= 0) ? 0 : frameWidth;
					columnLabels[i].BackColor = frameColor;
					columnLabels[i].Location = new Point(
						Xcoord, listView.Location.Y);
				}

				//title frame coordinates (initialized for GridStyles.Light style)
				Point lowLeft = new Point(Xcoord, titleHeight);
				Point topLeft = new Point(Xcoord, titleHeight - textVerticalOffset);
				Point topRight = new Point(Xcoord + currentColumnWidth, titleHeight - textVerticalOffset);
				if (style == GridStyles.Framed)
				{
					//title frame coordinate (for GridStyles.Framed style)
					int halfFrameWidth = Convert.ToInt32(frameWidth / 2);
					topLeft = new Point(Xcoord + halfFrameWidth, halfFrameWidth);
					topRight = new Point(Xcoord + currentColumnWidth, halfFrameWidth);

					//title Backgroung drawing
					Gr.FillRectangle(new SolidBrush(titleBackColor), Xcoord, 0, currentColumnWidth, titleHeight);		
				}

				//title frame drawing
				if (currentColumnWidth > 0)
				Gr.DrawLines(framePen, new Point[]{lowLeft, topLeft, topRight});

				//text drawing 
				Gr.DrawString(listView.Columns[i].Text, columnHeaderFont, new SolidBrush(titleForeColor),
					new Rectangle(Xcoord, textVerticalOffset, currentColumnWidth, columnHeaderFont.Height));

				Xcoord += listView.Columns[i].Width;
			}

      pct_icon.Location = new Point(Xcoord - pct_icon.Width, this.titleHeight);

			this.ResumeLayout();
		}

		/// <summary>
		/// Set the backGroung color of every items from th specified index to the end 
		/// according to the property EvenItemsColor and OddItemsColor.
		/// </summary>
		/// <param name="fromIndex">Index from update colors into the listView</param>
		private void ColorItems(int fromIndex)
		{
			for (int i = fromIndex; i < listView.Items.Count; i++)
			{
				if(showForecolorItem)
				{
					if(i == 0)
						listView.Items[i].ForeColor = Color.Red;
					if(i == 1)
						listView.Items[i].ForeColor = Color.Tomato;
					if(i == 2)
						listView.Items[i].ForeColor = Color.Orange;
				}
				
				listView.Items[i].BackColor = ((i % 2) != 0) 
					? evenItemsColor 
					: oddItemsColor;
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

			this.listView = new System.Windows.Forms.ListView();

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit(); 

			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.BackColor = System.Drawing.Color.WhiteSmoke;
			this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView.FullRowSelect = true;
			this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(0, 20);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(184, 92);
			this.listView.TabIndex = 0;
			this.listView.TabStop = false;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			this.listView.Layout += new System.Windows.Forms.LayoutEventHandler(this.listView_Layout);
			// 
			// Grid
			// 
			this.Controls.Add(this.listView);
			this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Grid";
			this.Size = new System.Drawing.Size(192, 120);
			this.FontChanged += new System.EventHandler(this.Grid_FontChanged);
			this.Enter += new System.EventHandler(this.Grid_Enter);

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();

			this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// build labels for the column lines .
		/// </summary>
		private void listView_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			//first time create labels
			if (columnLabels == null && listView.Columns.Count != 0)
			{
				columnLabels = new Label[listView.Columns.Count];
				for (int i = 0; i < columnLabels.Length; i++)
				{
					columnLabels[i] = new Label();
					this.Controls.Add(columnLabels[i]);
					columnLabels[i].BringToFront();
				}
			}
		}

		/// <summary>
		/// Gets the selected items of the grid.
		/// </summary>
		public ListView.SelectedListViewItemCollection getSelectedItems()
		{
			return listView.SelectedItems;	
		}

		/// <summary>
		/// 
		/// </summary>
		public void SelectIndex(int ind)
		{
			this.listView.Items[ind].Selected=true;
			this.listView.Invalidate();
		}

		/// <summary>
		/// Gets a the selected indexes of the grid.
		/// </summary>
		public ListView.SelectedIndexCollection getSelectedIndices()
		{
			return listView.SelectedIndices;
		}

		/// <summary>
		/// Gets the items of the grid.
		/// </summary>
		public ListView.ListViewItemCollection getItems()
		{
			return listView.Items;
		}

		/// <summary>
		/// Raise the SelectedIndexChanged when the selection change.
		/// </summary>
		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listView.SelectedItems.Count==0)
				return;
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}

    /// <summary>
    /// Raise the DoubleClick when there is a double mouse click.
    /// </summary>
    private void listView_DoubleClick(object sender, System.EventArgs e)
    {
      if (listView.SelectedItems.Count == 0)
        return;
      if (DoubleClick != null)
        DoubleClick(this, e);
    }

    private void Grid_Enter(object sender, System.EventArgs e)
    {
			if(listView.Items.Count!=0)
			{
				listView.Focus();
				if (listView.FocusedItem != null)
					listView.FocusedItem.Selected=true;
				else
					listView.Items[0].Selected = true;
			}
		}

		private void Grid_FontChanged(object sender, System.EventArgs e)
		{
			listView.Font = this.Font;
		}

    /// <summary>
    /// Return a bool determining if the selected rows are consecutive.
    /// </summary>
    /// <param name="selection"></param>
    /// <returns></returns>
    public bool IsConsecutiveSelection()
    {
      ListView.SelectedListViewItemCollection selection = this.listView.SelectedItems;
      if (selection.Count <= 0)
        return true;
      for (int i = 0; i < selection.Count - 1; i++)
      {
        if ((selection[i].Index) < (selection[i + 1].Index))
        {
          if ((selection[i].Index) != (selection[i + 1].Index - 1))
            return false;
        }
        else
        {
          if ((selection[i].Index) != (selection[i + 1].Index + 1))
            return false;
        }
      }
      return true;
    }
  }
}
