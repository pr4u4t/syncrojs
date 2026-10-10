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
    /// GridView.
    /// </summary>
	public class GridView : SyncroC.Controls.SyncroUserControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

    private System.ComponentModel.Container components = null;
    private DataGridView dataGridView;

    public DataGridView DataGridView
    {
      get { return dataGridView; }
      set { dataGridView = value; }
    }

    /// <summary>Current height of the title.</summary>
    private int titleHeight = 24;

    /// <summary>Current font of the column headers.</summary>
    private Font columnHeaderFont = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

    //constants
    /// <summary> Frame width for Light style</summary>
    private const int LIGHT_FRAMEWIDTH = 1;
    /// <summary> Frame width for Framed style</summary>
    private const int FRAMED_FRAMEWIDTH = 2;
    /// <summary>Current width of the frame.</summary>
    private int frameWidth = LIGHT_FRAMEWIDTH;

    /// <summary>Array of labels for column lines.</summary>
    private Label[] columnLabels;

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
    private GridStyles style;
    /// <summary>
    /// Gets or sets the component style.
    /// </summary>
    [Category("Fosbersoft"), Description("Gets or sets the component style.")]
    public GridStyles Style
    {
      get { return style; }
      set
      {
        style = value;
        Invalidate();
      }
    }

    /// <summary>Current Space between two columns.</summary>
    private int columnSpacing = (int)GridColumnSpacing.Narrow;
    /// <summary>
    /// Gets or sets the ColumnSpacing.
    /// </summary>
		[Category("Fosbersoft"), Description("Current Space between two columns.")]
		public GridColumnSpacing ColumnSpacing
    {
      get
      {
        switch (columnSpacing)
        {
          case 4: return GridColumnSpacing.Narrow;
          case 6: return GridColumnSpacing.Medium;
          case 16: return GridColumnSpacing.Large;
          default: return GridColumnSpacing.Narrow;
        }
      }
      set
      {
        columnSpacing = (int)value;
      }
    }

		/// <summary>
		/// Gets or sets the type of scroll bars to display for the control.
		/// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the type of scroll bars to display for the control.")]
		public ScrollBars ScrollBars
		{
			get { return dataGridView.ScrollBars; }
			set { dataGridView.ScrollBars = value; }
		}

		/// <summary>
		/// Gets or sets the index of the row displayed on the grid
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int FirstDisplayedScrollingRowIndex
		{
			get { return dataGridView.FirstDisplayedScrollingRowIndex; }
			set { dataGridView.FirstDisplayedScrollingRowIndex = value; }
		}

		/// <summary>
		/// Occurs when the content of a cell need to be formatted for display
		/// </summary>
		[Description("Occurs when the content of a cell need to be formatted for display."), Category("Fosbersoft")]
		public event DataGridViewCellFormattingEventHandler CellFormatting;

		/// <summary>
    /// Gets or sets a value indicating whether multiple rows can be selected.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets a value indicating whether multiple rows can be selected.")]
		public bool MultiSelect
    {
      get { return dataGridView.MultiSelect; }
      set { dataGridView.MultiSelect = value; }
    }

		/// <summary>
		/// The height, in pixels, of the row. The default is the height of the default font plus 9 pixels. 
		/// </summary>
		[Category("Fosbersoft"), Description("The height, in pixels, of the row.")]
		public int RowsHeight
		{
			get { return dataGridView.RowTemplate.Height; }
			set { dataGridView.RowTemplate.Height = value; }
		}


    /// <summary>
    /// Gets or sets a value indicating whether columns are created automatically when the DataSource or DataMember properties are set.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets a value indicating whether columns are created automatically when the DataSource or DataMember properties are set.")]
		public bool AutoGenerateColumns
    {
      get { return dataGridView.AutoGenerateColumns; }
      set { dataGridView.AutoGenerateColumns = value;}
    }

    /// <summary>
    /// Gets a collection that conteins all the columns in the collection.
    /// </summary>

//				WindowsForms10.Window.8.app.0.33c0d9d

    [	//property editor settings.
      DesignerSerializationVisibility(
          DesignerSerializationVisibility.Content),
      Editor(
          typeof(System.ComponentModel.Design.CollectionEditor),
          typeof(System.Drawing.Design.UITypeEditor))
    ]
    public DataGridViewColumnCollection Columns
    {
      get { return dataGridView.Columns; }
    }

    /// <summary>
    /// 
    /// </summary>
    public DataGridViewRowCollection Rows
    {
      get { return dataGridView.Rows; }
    }

    /// <summary>Current color of the frame.</summary>
    private Color frameColor = ColConv.Convert(SyncroColor.Black);
    /// <summary>
    /// Gets or sets the color of the frame.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the color of the frame.")]
		public SyncroColor FrameColor
    {
      get { return ColConv.Convert(frameColor); }
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
		[Category("Fosbersoft"), Description("Gets or sets the foreColor of the Title.")]
		public SyncroColor TitleForeColor
    {
      get { return ColConv.Convert(titleForeColor); }
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
		[Category("Fosbersoft"), Description("Gets or sets the title BackColor.")]
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
    /// Gets or sets the title BackColor.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the title BackColor.")]
		public SyncroColor SelectionBackColor
    {
      get { return ColConv.Convert(dataGridView.RowsDefaultCellStyle.SelectionBackColor); }
      set
      {
        dataGridView.RowsDefaultCellStyle.SelectionBackColor = ColConv.Convert(value);
				Invalidate();
      }
    }

		/// <summary>
		/// Gets or sets the selection ForeColor.
		/// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the selection ForeColor.")]
		public SyncroColor SelectionForeColor
		{
			get { return ColConv.Convert(dataGridView.RowsDefaultCellStyle.SelectionForeColor); }
			set
			{
				dataGridView.RowsDefaultCellStyle.SelectionForeColor = ColConv.Convert(value);
				Invalidate();
			}
		}

    /// <summary>
    /// Gets or sets the alternate BackColor.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the alternate BackColor.")]
		public SyncroColor AlternateBackColor
    {
      get { return ColConv.Convert(dataGridView.AlternatingRowsDefaultCellStyle.BackColor); }
      set {	dataGridView.AlternatingRowsDefaultCellStyle.BackColor = ColConv.Convert(value);}
    }

		/// <summary>
		/// Gets or sets the alternate ForeColor.
		/// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the alternate ForeBackColor.")]
		public SyncroColor AlternateForeColor
		{
			get { return ColConv.Convert(dataGridView.AlternatingRowsDefaultCellStyle.ForeColor); }
			set { dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = ColConv.Convert(value); }
		}


		/// <summary>
		/// Gets or sets the alternate SelectionBackColor.
		/// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the alternate SelectionBackColor.")]
		public SyncroColor AlternateSelectionBackColor
		{
			get { return ColConv.Convert(dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor); }
			set { dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColConv.Convert(value); }
		}

		/// <summary>
		/// Gets or sets the alternate SelectionForeColor.
		/// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the alternate SelectionForeColor.")]
		public SyncroColor AlternateSelectionForeColor
		{
			get { return ColConv.Convert(dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor); }
			set { dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = ColConv.Convert(value); }
		}

    /// <summary>
    /// Gets or sets the datasource that the grid is displaying data for.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the datasource that the grid is displaying data for.")]
		public object DataSource
    {
      get { return dataGridView.DataSource; }
      set { dataGridView.DataSource = value; }
    }

    /// <summary>
    /// Gets or sets the name of the list or table in the datasource for which the control is displaying data.
    /// </summary>
		[Category("Fosbersoft"), Description("Gets or sets the name of the list or table in the datasource for which the control is displaying data.")]
		public string DataMember
    {
      get { return dataGridView.DataMember; }
      set { dataGridView.DataMember = value; }
    }

    /// <summary>
    /// Occurs when the selection changes.
    /// </summary>
		[Category("Fosbersoft"), Description("Occurs when the selection changes.")]
		public event EventHandler SelectionChanged;

		/// <summary>
    /// Occurs when a key is first pressed.
    /// </summary>
		[Category("Fosbersoft"), Description("Occurs when a key is first pressed.")]
		public new event KeyEventHandler KeyDown;

		/// <summary>
		/// Occurs when the control is doubleclicked.
		/// </summary>
		[Category("Fosbersoft"), Description("Occurs when the control is doubleclicked.")]
		public new event EventHandler DoubleClick;


		/// <summary>
    /// Gets the collection of rows selected by the user.
    /// </summary>
    public DataGridViewSelectedRowCollection SelectedRows
    {
      get { return dataGridView.SelectedRows; }
    }

    /// <summary>
    /// Gets a value indicating wheather the control has the input focus.
    /// </summary>
    public override bool Focused
    {
      get { return dataGridView.Focused; }
    }

    /// <summary>
    /// Builder.
    /// </summary>
    public GridView()
    {
      InitializeComponent();
      dataGridView.AutoGenerateColumns = false;
			dataGridView.AllowUserToResizeColumns = false;
			dataGridView.AllowUserToResizeRows = false;

			dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
		}

		private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (CellFormatting != null)
				this.CellFormatting(sender, e);
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

			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit(); 

			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridView.ColumnHeadersVisible = false;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridView.RowHeadersVisible = false;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView.RowTemplate.Height = 32;
			this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.ShowCellToolTips = false;
			this.dataGridView.ShowEditingIcon = false;
			this.dataGridView.Size = new System.Drawing.Size(192, 120);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.DoubleClick += new System.EventHandler(this.dataGridView_DoubleClick);
			this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			// 
			// GridView
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.dataGridView);
			this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "GridView";
			this.Size = new System.Drawing.Size(192, 120);
			this.BackColorChanged += new System.EventHandler(this.GridView_BackColorChanged);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();

			this.ResumeLayout(false);

		}
    #endregion

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
    /// Sort by the specified column.
    /// </summary>
    /// <param name="dataGridViewColumn"></param>
		public void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection listSortDirection)
    {
      try
      {
        dataGridView.Sort(dataGridViewColumn, listSortDirection);
      }
      catch (Exception e)
      {
        Logger.LogError(this, "Sort - Error setting Sort property", e);
      }
    }

    /// <summary>
		/// Resize the internal component according to the parameters.
		/// </summary>
    /// <param name="VerticalResizing">coefficient for vertical resizing.</param>
    /// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
    /// <param name="FontResizing">coefficient for font resizing.</param>
		public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{
      if (!(VerticalResizing == 1 && OrizontalResizing == 1 && FontResizing == 1))
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
    /// Design the aspect of the component.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Visible = (imagePassword != null);

      switch (style)
      {
        case GridStyles.Framed:
            frameWidth = FRAMED_FRAMEWIDTH;
            break;
        case GridStyles.Light:
            frameWidth = LIGHT_FRAMEWIDTH;
            break;
      }

			//dataGridView.Location = new Point(0, titleHeight);
			//dataGridView.Size = new Size(this.Width, this.Height - titleHeight);
			dataGridView.Location = new Point(0, titleHeight + 2);
			dataGridView.Size = new Size(this.Width, this.Height - titleHeight - 2);

      //graphic objects
      Graphics Gr = e.Graphics;
      Pen framePen = new Pen(new SolidBrush(frameColor), frameWidth);

      //X coordinate of the left side of the current column
      int Xcoord = dataGridView.Location.X + Convert.ToInt32(frameWidth / 2);
      //space between the top of the component and the text of the title
      int textVerticalOffset = Convert.ToInt32((titleHeight - columnHeaderFont.Height) / 2);

      for (int i = 0; i < dataGridView.ColumnCount; i++)
      {
				//int currentColumnWidth = ((i + 1) != dataGridView.ColumnCount)
				//    ? dataGridView.Columns[i].Width - (int)columnSpacing
				//    : dataGridView.Columns[i].Width;

				int currentColumnWidth = dataGridView.Columns[i].Width;
				if (i != (dataGridView.ColumnCount - 1))
				{
					if (dataGridView.Columns[i + 1].Visible)
					{
						currentColumnWidth -= (int)columnSpacing;
					}
				}

        //title frame coordinates (initialized for GridStyles.Light style)
        Point lowLeft = new Point(Xcoord, titleHeight);
        Point topLeft = new Point(Xcoord, titleHeight - textVerticalOffset);
        Point topRight = new Point(Xcoord + currentColumnWidth, titleHeight - textVerticalOffset);
        if (style == GridStyles.Framed)
        {
          //title frame coordinate (for GridStyles.Framed style)
          topLeft = new Point(Xcoord, Convert.ToInt32(frameWidth / 2));
          topRight = new Point(Xcoord + currentColumnWidth, Convert.ToInt32(frameWidth / 2));

          //title Backgroung drawing
          Gr.FillRectangle(new SolidBrush(titleBackColor), Xcoord, 0, currentColumnWidth, titleHeight);
        }


				if (dataGridView.Columns[i].Visible)
				{
					//title frame drawing
					Gr.DrawLines(framePen, new Point[] { lowLeft, topLeft, topRight });
					//text drawing 
					Gr.DrawString(dataGridView.Columns[i].HeaderText, columnHeaderFont, new SolidBrush(titleForeColor),
							new Rectangle(Xcoord, textVerticalOffset, currentColumnWidth, columnHeaderFont.Height));
				}

        Xcoord += dataGridView.Columns[i].Width;
      }

      //arrange labels for vertical lines 
      //first time creates labels and sets property that won't change on resizing
      if (columnLabels == null)
      {
        columnLabels = new Label[dataGridView.Columns.Count];
        for (int i = 0; i < columnLabels.Length; i++)
        {
          columnLabels[i] = new Label();
          columnLabels[i].BackColor = frameColor;
          columnLabels[i].Width = frameWidth;
          this.Controls.Add(columnLabels[i]);
          columnLabels[i].BringToFront();
        }
      }
      //every time
      //X coordinate of the left side of the current column
      Xcoord = dataGridView.Location.X;
      for (int i = 0; i < columnLabels.Length; i++)
      {
        columnLabels[i].Height = dataGridView.Height;
        columnLabels[i].Location = new Point(
            Xcoord, dataGridView.Location.Y - 2);
				columnLabels[i].Visible = dataGridView.Columns[i].Visible;
        Xcoord += dataGridView.Columns[i].Width;
      }

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Location = new Point(this.Width - pct_icon.Width, 1);

    }

    private void dataGridView_SelectionChanged(object sender, EventArgs e)
    {
      if (SelectionChanged != null)
      {
				SelectionChanged(this, e);
			}
			Invalidate();
    }

		private void dataGridView_KeyDown(object sender, KeyEventArgs e)
		{
			if (KeyDown != null)
			{
				KeyDown(this, e);
			}
		}

		private void dataGridView_DoubleClick(object sender, EventArgs e)
		{

			if (DoubleClick != null)
			{
				DoubleClick(this, e);
			}
		} 

		private void GridView_BackColorChanged(object sender, EventArgs e)
		{
			dataGridView.BackgroundColor = this.BackColor;
			dataGridView.DefaultCellStyle.BackColor = this.BackColor;
		}

    /// <summary>
    /// Return a bool determining if the selected rows are consecutive.
    /// </summary>
    public bool IsConsecutiveSelection()
    {
      DataGridViewSelectedRowCollection selection = this.dataGridView.SelectedRows;
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