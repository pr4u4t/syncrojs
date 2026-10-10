using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Controls.Interfaces;
using SyncroC.Controls.Alarmtimer;
using SyncroC.Controls.Colors;

namespace SyncroC.Controls
{
	/// <summary>
	/// Enumerates every kind of images to be displayed into the StateButton component.
	/// </summary>
	public enum StateButtonImage
	{
		/// <summary>Specify that the image to be showed is the one provided by the user.</summary>
		UserDefined = 0,
		/// <summary>Identify the Auto Manual image.</summary>
		Auto_Manual = 1,
		/// <summary>Identify the Local Remote image.</summary>
		Local_Remote = 2,
		/// <summary>Identify the OnLine OffLine image.</summary>
		OnLine_OffLine = 3,
	}

	/// <summary>
	/// Summary description for StateButton.
	/// </summary>
	public class StateButton : SyncroC.Controls.SyncroUserControl
	{
    //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
    private System.Windows.Forms.PictureBox pct_icon;

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PictureBox pictureBox;
		//image lists
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ImageList imageListClicked;
		private System.Windows.Forms.ImageList imageListActive;
		private System.Windows.Forms.ImageList imageListActiveClicked;
		private System.Windows.Forms.ImageList imageListAlarms;

		//timer to show blinking alarm
		private static AlarmTimer alarmTimer;

		private int BORDER = 3;
		/// <summary>Color to be used when the component's enable property is true.</summary>
		SyncroColor ENABLED_TRUE_COLOR = SyncroColor.Black;
		/// <summary>Color to be used when the component's enable property is false.</summary>
		SyncroColor ENABLED_FALSE_COLOR = SyncroColor.LightGray;

		/// <summary> Index of the image showed when the component is warned into the ImageListAlams</summary>
		private int INDEX_WARNING_IMAGE = 0;
		/// <summary> Index of the image showed when the component is alarmed into the ImageListAlams</summary>
		private int INDEX_ALARM_IMAGE = 1;
		/// <summary> Index of the image showed when the component is in emergency into the ImageListAlams</summary>
		private int INDEX_INEMERGENCY_IMAGE = 2;

		private int slimFrameWidth = 1;
		private int largeFrameWidth = 3;

		/// <summary>Rectangle within the text will be drawn.</summary>
		private Rectangle textRectangle;
		/// <summary>Font currently used, depends on the Enable and HighlightEnable properties</summary>
		private Font currentFont;
		/// <summary>ForeColor currently used, depends on the Enable and Active properties.</summary>
		private SyncroColor currentForeColor;
		/// <summary>Currently used backcolor, depends on the Active property.</summary>
		private SyncroColor currentBackColor;

		/// <summary>true between the mouseDown and mouseup events and the buttondown and buttonup events.</summary>
		private bool isComponentCurrentlyClicked = false;
		private bool IsComponentCurrentlyClicked
		{
			get { return isComponentCurrentlyClicked; }
			set
			{
				isComponentCurrentlyClicked = value;
			
				pictureBox.Image = getImageToDisplay();
				if (currentBackColor == SyncroColor.White)
				{
					currentBackColor = SyncroColor.Black;
					currentForeColor = SyncroColor.White;
				}
				else
				{
					currentBackColor = SyncroColor.White;
					currentForeColor = SyncroColor.Black;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Raised when the control is clicked.
		/// </summary>
		public new event EventHandler Click;

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

		/// <summary>Determine if the component is a button.</summary>
		private bool isButton = true;
		/// <summary>
		/// Gets or sets the isButton property.
		/// </summary>
		[Description("If true the StateButton is a Button (pushable)."), Category("FosberSoft")]
		public bool IsButton
		{
			get { return this.isButton; }
			set
			{
				if (this.isButton != value)
				{
					this.isButton = value;
          this.Cursor = this.isButton ? Cursors.Hand : Cursors.Arrow;
          this.pictureBox.Cursor = this.isButton ? Cursors.Hand : Cursors.Arrow;
					Invalidate();
				}
			}
		}

		/// <summary>Determine if the component is currently switching between active and !active status.</summary>
		private bool switching = false;
		/// <summary>
		/// Gets or sets the switching property.
		/// </summary>
		[Description("Determine if the component is currently switching between active and !active status."), Category("FosberSoft")]
		public bool Switching
		{
			get { return switching; }
			set
			{
				switching = value;
				Invalidate();
			}
		}

    /// <summary>Determine if the component backcolor is filled with the pixel(0,0) of the Image.</summary>
    private bool fillBackColorWithImage = false;
    /// <summary>
    /// Gets or sets the switching property.
    /// </summary>
    [Description("Determine if the component is currently switching between active and !active status."), Category("FosberSoft")]
    public bool FillBackColorWithImage
    {
      get { return fillBackColorWithImage; }
      set
      {
        fillBackColorWithImage = value;
        Invalidate();
      }
    }

		private bool firstActiveSet = true; 
		/// <summary>Determine if the component is currently active.</summary>
		private bool active = false;
		/// <summary>
		/// Gets or sets the active property.
		/// </summary>
    [Description("If true the StateButton is Active (selected)."), Category("FosberSoft")]
    public bool Active
		{
			get { return active;}
			set 
			{
				if (Active != value || firstActiveSet)
				{
					firstActiveSet = false;
					Switching = false;
					active = value;
					pictureBox.Image = getImageToDisplay();
					//currentBackColor = (this.active ? SyncroColor.Black : SyncroColor.White);
					//currentForeColor = (this.active ? SyncroColor.White : SyncroColor.Black);
					if (this.active)
					{
						if (this.Enabled)
							currentBackColor = ENABLED_TRUE_COLOR;
						else
							currentBackColor = ENABLED_FALSE_COLOR;
						currentForeColor = SyncroColor.White;
					}
					else
					{
						currentBackColor = SyncroColor.White;
						if (this.Enabled)
							currentForeColor = ENABLED_TRUE_COLOR;
						else
							currentForeColor = ENABLED_FALSE_COLOR;
					}
					Invalidate();
				}
			}
    }
		/// <summary>
		/// Gets or sets the active property.
		/// </summary>
    [Description("If true the StateButton is Auto (selected)."), Category("FosberSoft")]
    public bool Auto
		{
			get { return this.Active;		}
			set { this.Active = value;	}
		}
		/// <summary>
		/// Gets or sets the active property.
		/// </summary>
    [Description("If true the StateButton is InLocal (selected)."), Category("FosberSoft")]
    public bool InLocal
		{
			get {	return this.Active;		}
			set {	this.Active = value;	}
		}
		/// <summary>
		/// Gets or sets the Active property.
		/// </summary>
    [Description("If true the StateButton is Online (selected)."), Category("FosberSoft")]
    public bool OnLine
		{
			get {	return this.Active;		}
			set {	this.Active = value;	}
		}

		/// <summary>Determine if the state of the component is alarmed.</summary>
		private bool alarmed = false;
		/// <summary>
		/// Gets or sets the Alarmed property.
		/// </summary>
    [Description("If true the component show the alarm."), Category("FosberSoft")]
    public bool Alarmed
		{
			get { return alarmed;}
			set 
			{ 
				alarmed = value;
				if (!alarmed)
				{
					pictureBox.Visible = showImage;
					pictureBox.Image = getImageToDisplay();
				}
			}
		}

		/// <summary>Determine if the state of the component is warned.</summary>
		private bool warned = false;
		/// <summary>
		/// Gets or sets the Warned property.
		/// </summary>
		[Description("If true the component show the warning."), Category("FosberSoft")]
		public bool Warned
		{
			get { return warned; }
			set
			{
				warned = value;
				if (!warned)
				{
					pictureBox.Visible = showImage;
					pictureBox.Image = getImageToDisplay();
				}
			}
		}

		/// <summary>Determine if the state of the component is in Emergency.</summary>
		private bool inEmergency = false;
		/// <summary>
		/// Gets or sets the InEmergency property.
		/// </summary>
		[Description("If true the component show the Emergency."), Category("FosberSoft")]
		public bool Inemergency
		{
			get { return inEmergency; }
			set
			{
				inEmergency = value;
				if (!inEmergency)
				{
					pictureBox.Visible = showImage;
					pictureBox.Image = getImageToDisplay();
				}
			}
		}

		/// <summary>If true, the truth of the component's enabled property is highlighted in some way.</summary>
		private bool highLightEnable = false;
    [Description("If true, the truth of the component's enabled property is highlighted in some way."), 
      Category("FosberSoft")]
    /// <summary>
		/// Gets or sets the HighLightEnable property.
		/// </summary>
		public bool HighLightEnable
		{
			get { return highLightEnable; }
			set
			{
				highLightEnable = value;
				if (!highLightEnable)
					currentFont = this.Font;
			}
		}

		/// <summary>
		/// Image to be showed as a user defined image when the component is not active.
		/// </summary>
    [Description("Image to be displayed when the component is not active and the imageIndex is set as UserDefined."), 
      Category("FosberSoft")]
    public Image UserDefinedImage
		{
			get {return imageList.Images[(int)StateButtonImage.UserDefined];}
			set 
			{
				imageList.Images[(int)StateButtonImage.UserDefined] = value;
				imageListActiveClicked.Images[(int)StateButtonImage.UserDefined] = value;
				if (imageIndex == StateButtonImage.UserDefined)
				{
					pictureBox.Image = getImageToDisplay();
				}
			}
		}

		/// <summary>
		/// Image to be showed as a user defined image when the component is Active
		/// </summary>
    [Description("Image to be displayed when the component is active and the imageIndex is set as UserDefined."), 
      Category("FosberSoft")]
    public Image UserDefinedActiveImage
		{
			get {return imageListActive.Images[(int)StateButtonImage.UserDefined];}
			set 
			{
				imageListActive.Images[(int)StateButtonImage.UserDefined] = value;
				imageListClicked.Images[(int)StateButtonImage.UserDefined] = value;
				if (imageIndex == StateButtonImage.UserDefined)
				{
					pictureBox.Image = getImageToDisplay();
				}
			}
		}

		/// <summary>Current showed button image index.</summary>
		private StateButtonImage imageIndex = StateButtonImage.UserDefined;
		/// <summary>
		/// Gets or sets the image to be displaied on te control.
		/// </summary>
    [Description("ButtonImage to display"), Category("FosberSoft")]
    public StateButtonImage Image
		{
			get {	return imageIndex;		}
			set	
			{	
				imageIndex = value;	
				pictureBox.Image = getImageToDisplay();
			}
		}

		private bool showText = false;
    [Description("If true the component text is showed"), Category("FosberSoft")]
    public bool ShowText
		{
			get {return showText;}
			set 
			{
				showText = value;
				Invalidate();
			}
		}

		private bool showImage = true;
	
    [Description("If true the component image is showed"), Category("FosberSoft")]
    public bool ShowImage
		{
			get { return showImage; }
			set
			{
				showImage = value;
				Invalidate();
			}
		}

		private bool framePresent = false;
		[Description("If true the frame is showed"), Category("FosberSoft")]
		public bool FramePresent
		{
			get 
			{
				if (!this.isButton)
					return false;
				return framePresent; 
			}
			set
			{
				framePresent = value;
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

		//ABello_FI10/07/2007_2 I pulsanti di Loc/Rem sulle pagine operatore si vedevano anche se il PRC era disabilitato
		bool isForceToInvisible = false;
    /// <summary>
    /// Gets or sets the text to be written into the control.
    /// </summary>
    [Description("Force the control to stay invisible."), Browsable(true),
      DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
      Category("FosberSoft")]
		public bool IsForceToInvisible
		{
			get { return isForceToInvisible; }
			set { isForceToInvisible = value; }
		}

		//ABello_FI10/07/2007_2 I pulsanti di Loc/Rem sulle pagine operatore si vedevano anche se il PRC era disabilitato
		public new bool Visible
		{
			get { return base.Visible; }
			set
			{
				if (this.isForceToInvisible)
				{
					base.Visible = false;
				}
				else
				{
					base.Visible = value;
          this.resize();
					this.Invalidate();
				}
			}
		}

    public StateButton()
		{
			InitializeComponent();

			if (alarmTimer != null)
			{
				alarmTimer.Tick += new AlarmTimerTickHandler(this.alarmTimer_Tick);
			}
			else
			{
				Logger.LogError(this, "AlarmTimer not instantiated");
			}

			currentFont = new Font(this.Font, this.Font.Style);
			currentForeColor = ENABLED_TRUE_COLOR;
			pictureBox.Image = getImageToDisplay();

			this.GotFocus += new EventHandler(StateButton_GotFocus);
			this.LostFocus += new EventHandler(StateButton_LostFocus);
		}

		/// <summary>
		/// Initialize the static StateButton's istance of the AlarmTimer to show blinking alarms
		/// </summary>
		public static void initializeAlarmTimer()
		{
			alarmTimer = new AlarmTimer();
		}

		/// <summary>
		/// Rotates, flips, or rotates and flips the user defined images of the component
		/// </summary>
		/// <param name="rotateFlipType">A System.Drawing.RotateFlipType member that specifies the type of rotation and flip to apply to the image.</param>
		public void RotateFlip(RotateFlipType rotateFlipType)
		{
			//note: the following code is not apparently optimized really.
			//The RotateFlip method is not applied directly at the image objects contained into the ImageLists
			//because in that way it seems not work properly... that's the reason why we apply the method
			//to a temporary object and later we put it into the list
			if (imageList.Images.Count > 0)
			{
				Image image = imageList.Images[(int)StateButtonImage.UserDefined];
				image.RotateFlip(rotateFlipType);
				imageList.Images[(int)StateButtonImage.UserDefined] = image;
			}
			if (imageListClicked.Images.Count > 0)
			{
				Image image = imageListClicked.Images[(int)StateButtonImage.UserDefined];
				image.RotateFlip(rotateFlipType);
				imageListClicked.Images[(int)StateButtonImage.UserDefined] = image;
			}
			if (imageListActive.Images.Count > 0)
			{
				Image image = imageListActive.Images[(int)StateButtonImage.UserDefined];
				image.RotateFlip(rotateFlipType);
				imageListActive.Images[(int)StateButtonImage.UserDefined] = image;
			}
			if (imageListActiveClicked.Images.Count > 0)
			{
				Image image = imageListActiveClicked.Images[(int)StateButtonImage.UserDefined];
				image.RotateFlip(rotateFlipType);
				imageListActiveClicked.Images[(int)StateButtonImage.UserDefined] = image;
			}
			for (int i = 0; i < imageListAlarms.Images.Count; i++ )
			{
				Image image = imageListAlarms.Images[i];
				image.RotateFlip(rotateFlipType);
				imageListAlarms.Images[i] = image;
			}

			pictureBox.Image = getImageToDisplay();
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

		private void StateButton_GotFocus(Object sender, EventArgs e)
		{
			Invalidate();
		}
		private void StateButton_LostFocus(Object sender, EventArgs e)
		{
			Invalidate();
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateButton));
      this.pct_icon = new System.Windows.Forms.PictureBox();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.imageListActive = new System.Windows.Forms.ImageList(this.components);
      this.imageListAlarms = new System.Windows.Forms.ImageList(this.components);
      this.imageListActiveClicked = new System.Windows.Forms.ImageList(this.components);
      this.imageListClicked = new System.Windows.Forms.ImageList(this.components);
      this.pictureBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
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
      // 
      // imageList
      // 
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList.Images.SetKeyName(0, "");
      this.imageList.Images.SetKeyName(1, "Manuale_ISO_0096.bmp");
      this.imageList.Images.SetKeyName(2, "REMOTO_ISO_1108.bmp");
      this.imageList.Images.SetKeyName(3, "offline.bmp");
      // 
      // imageListActive
      // 
      this.imageListActive.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListActive.ImageStream")));
      this.imageListActive.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListActive.Images.SetKeyName(0, "");
      this.imageListActive.Images.SetKeyName(1, "Automatico_ISO_0017.bmp");
      this.imageListActive.Images.SetKeyName(2, "LOCALE_ISO_1109.bmp");
      this.imageListActive.Images.SetKeyName(3, "onLine.bmp");
      // 
      // imageListAlarms
      // 
      this.imageListAlarms.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAlarms.ImageStream")));
      this.imageListAlarms.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListAlarms.Images.SetKeyName(0, "warning.bmp");
      this.imageListAlarms.Images.SetKeyName(1, "");
      this.imageListAlarms.Images.SetKeyName(2, "fungo.png");
      // 
      // imageListActiveClicked
      // 
      this.imageListActiveClicked.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListActiveClicked.ImageStream")));
      this.imageListActiveClicked.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListActiveClicked.Images.SetKeyName(0, "generico.bmp");
      this.imageListActiveClicked.Images.SetKeyName(1, "Automatico ISO 0017_Reverse.bmp");
      this.imageListActiveClicked.Images.SetKeyName(2, "LOCALE_ISO_1109_PREMUTO.bmp");
      this.imageListActiveClicked.Images.SetKeyName(3, "inLine_clicked.bmp");
      // 
      // imageListClicked
      // 
      this.imageListClicked.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListClicked.ImageStream")));
      this.imageListClicked.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListClicked.Images.SetKeyName(0, "genericoA.bmp");
      this.imageListClicked.Images.SetKeyName(1, "Manuale ISO 0096_Reverse.bmp");
      this.imageListClicked.Images.SetKeyName(2, "REMOTO_ISO_1108_PREMUTO.bmp");
      this.imageListClicked.Images.SetKeyName(3, "offline_clicked.bmp");
      // 
      // pictureBox
      // 
      this.pictureBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBox.Location = new System.Drawing.Point(0, 0);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(80, 56);
      this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox.TabIndex = 0;
      this.pictureBox.TabStop = false;
      this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StateButton_MouseDown);
      this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StateButton_MouseUp);
      // 
      // StateButton
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Controls.Add(this.pct_icon);
      this.Controls.Add(this.pictureBox);
      this.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Name = "StateButton";
      this.Size = new System.Drawing.Size(96, 96);
      this.FontChanged += new System.EventHandler(this.StateButton_FontChanged);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StateButton_KeyUp);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StateButton_MouseDown);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StateButton_MouseUp);
      this.EnabledChanged += new System.EventHandler(this.StateButton_EnabledChanged);
      this.SizeChanged += new System.EventHandler(this.StateButton_SizeChanged);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StateButton_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.pct_icon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Visible = (imagePassword != null);

			ArrangeComponents(false);
			if (this.Focused)
			{
				e.Graphics.DrawLines(
						new Pen(new SolidBrush(FOCUS_COLOR), 1),
						new Point[]{
										   new Point(0, 0),
										   new Point(this.Width-1, 0),
										   new Point(this.Width-1, this.Height-1),
										   new Point(0, this.Height-1),
										   new Point(0, 0)});
			}

			if (showText)
			{
				e.Graphics.FillRectangle(new SolidBrush(ColConv.Convert(this.currentBackColor)), textRectangle);
				
				TextLayout textLayout = CalculateTextLayout(
						textRectangle, this.Text, currentFont);
				if (textLayout.Rectangles != null && textLayout.Strings != null &&
							textLayout.Rectangles.Length != 0 && textLayout.Strings.Length != 0)
				{
					for (int i = 0; i < textLayout.Count; i++)
						e.Graphics.DrawString(textLayout.Strings[i], currentFont, new SolidBrush(ColConv.Convert(currentForeColor)), textLayout.Rectangles[i]);
				}
			}
		}

		private void StateButton_SizeChanged(object sender, System.EventArgs e)
		{
			ArrangeComponents(false);
		}

		private void drawFrame()
		{
			SolidBrush brush = new SolidBrush((this.Enabled) ? ColConv.Convert(SyncroColor.Black) : ColConv.Convert(currentForeColor));

			Pen pen = new Pen(brush, largeFrameWidth);
			this.CreateGraphics().DrawLine(pen,
										   new Point(
												BORDER,		//x 
												BORDER + (largeFrameWidth / 2)),	//y
										   new Point(
												this.Width - BORDER, //x
												BORDER + (largeFrameWidth / 2))//y
										   );
			this.CreateGraphics().DrawLine(pen,
											 new Point(
												BORDER + (largeFrameWidth / 2), //x
												BORDER),											//y
											 new Point(
												BORDER + (largeFrameWidth / 2), //x
												this.Height - BORDER)//y
											 );

			pen = new Pen(brush, slimFrameWidth);
			this.CreateGraphics().DrawLine(pen,
											 new Point(
												BORDER + (slimFrameWidth / 2), //x
												this.Height - BORDER - (slimFrameWidth / 2)),//y
											 new Point(
												this.Width - BORDER - (slimFrameWidth), //x
												this.Height - BORDER - (slimFrameWidth / 2))//y
											 );
			this.CreateGraphics().DrawLine(pen,
											 new Point(
												Convert.ToInt32(this.Width - BORDER - (slimFrameWidth / 2)), //x
												this.Height - BORDER - slimFrameWidth),//y
											 new Point(
												Convert.ToInt32(this.Width - BORDER - (slimFrameWidth / 2)), //x
												BORDER)//y
											 );
		}

		private void ArrangeComponents(bool forceImageVisible)
		{
			if (showText)
			{
				if (showImage)	
				{/*
					* showText  == TRUE 
					* showImage == TRUE
					*/
          int XFrame = this.largeFrameWidth;
          int YFrame = this.slimFrameWidth;
          if (this.FramePresent)
          {
            /*
						* showText  == TRUE 
						* showImage == TRUE
						* framePresent == TRUE
						*/
            drawFrame();
          }

          pictureBox.Location = new Point(
            BORDER + XFrame,
            BORDER + XFrame);
					pictureBox.Size = new Size(
            this.Size.Width - (BORDER * 2) - XFrame - YFrame + 1,
            Convert.ToInt32((this.Size.Height - (BORDER * 2) - XFrame - YFrame) * 0.66));
					textRectangle = new Rectangle(
						new Point(
              BORDER + XFrame,
							pictureBox.Location.Y + pictureBox.Height + 1),
						new Size(
              this.Size.Width - (BORDER * 2) - XFrame - YFrame,
              this.Size.Height - (BORDER * 2) - pictureBox.Height - 1 - XFrame - YFrame));
					pictureBox.BringToFront();
					pictureBox.Visible = true;
				}
				else
				{
					if (forceImageVisible)
					{/*
					  * showText == TRUE
					  * showImage == FALSE but forceImageVisible == TRUE
					  * framePresent == TRUE || FALSE
					  */
						pictureBox.Location = new Point(
								BORDER,
								BORDER);
						pictureBox.Size = new Size(
							this.Size.Width - (BORDER * 2),
							this.Size.Height - (BORDER * 2));
						pictureBox.Visible = true;
					}
					else
					{
						if (this.FramePresent)
						{/*
						* showText  == TRUE 
						* showImage == FALSE
						* framePresent == TRUE
						*/
							textRectangle = new Rectangle(
								new Point(
								 BORDER + largeFrameWidth + 1,
								 BORDER + largeFrameWidth),
							 new Size(
								 this.Size.Width - ((BORDER + slimFrameWidth + 1) * 2),
								 this.Size.Height - ((BORDER + 1) * 2) - slimFrameWidth - largeFrameWidth + 1));
							drawFrame();
						}
						else
						{/*
						* showText  == TRUE 
						* showImage == FALSE
						* framePresent == FALSE
						*/
							textRectangle = new Rectangle(
								new Point(
								BORDER + 1,
								BORDER),
							new Size(
								this.Size.Width - ((BORDER + 1) * 2),
								this.Size.Height - ((BORDER + 1) * 2)));
						}
						pictureBox.Visible = false;
					}
				}
			}
			else
			{
				if (showImage) 
				{
					if (this.FramePresent)
					{	/*
						 * showText  == FALSE 
						 * showImage == TRUE
						 * framPResent == TRUE
						 */
						pictureBox.Location = new Point(
							BORDER + largeFrameWidth,
							BORDER + largeFrameWidth);
            pictureBox.Size = new Size(
              this.Size.Width - (BORDER * 2) - slimFrameWidth - largeFrameWidth + 1,
              this.Size.Height - (BORDER * 2) - slimFrameWidth - largeFrameWidth + 1);
						drawFrame();
					}
					else
					{ /*
						 * showText  == FALSE 
						 * showImage == TRUE
						 * framPResent == FALSE
						 */
						pictureBox.Location = new Point(
							BORDER,
							BORDER);
						pictureBox.Size = new Size(
							this.Size.Width - (BORDER * 2),
							this.Size.Height - (BORDER * 2));
					}
					pictureBox.BringToFront();
					pictureBox.Visible = true;
				}
				else
				{	/*
					 * showText  == FALSE 
					 * showImage == FALSE
					 */
					pictureBox.Visible = false;
					this.BackColor = ColConv.Convert(SyncroColor.LightGray);
					if (this.FramePresent)
						drawFrame();
					Logger.LogError(this, "pictureBox and label are both visible false set");
				}
			}

      //RFAZZI_FI20/05/2010_2. Aggiunta icona Password su controlli utente. 
      pct_icon.Location = new Point(this.Width - pct_icon.Width - 5, this.Height - this.pct_icon.Height - 5);

		}

		/// <summary>
		/// Returns the image to be displayed according to the "active" and "imageIndex" variables.
		/// </summary>
		private Image getImageToDisplay()
		{
			Image image = null;
			if (active)
			{
				if (IsComponentCurrentlyClicked)
				{
					if ((int)imageIndex >= 0 && (int)imageIndex < imageListActiveClicked.Images.Count)
						image = imageListActiveClicked.Images[(int)imageIndex];
				}
				else
				{
					if ((int)imageIndex >= 0 && (int)imageIndex < imageListActive.Images.Count)
						image = imageListActive.Images[(int)imageIndex];
				}
			}
			else
			{
				if (IsComponentCurrentlyClicked)
				{
					if ((int)imageIndex >= 0 && (int)imageIndex < imageListClicked.Images.Count)
						image = imageListClicked.Images[(int)imageIndex];
				}
				else
				{
					if ((int)imageIndex >= 0 && (int)imageIndex < imageList.Images.Count)
						image = imageList.Images[(int)imageIndex];
				}
			}

			if (!this.Enabled)
				//todo: non just black and white... better is Grey and White
				image = SyncroImage.ConvertToBlacknWhite(image);

      // This function will determine the scale the image should be rendered at from the size of the picture box and image

      if (image != null)
      {
        // The client rectangle
        Rectangle rc = this.pictureBox.ClientRectangle;
        // Figure out the scaling necessary for the image
        SizeF size = new SizeF(image.Width / image.HorizontalResolution, image.Height / image.VerticalResolution);
        float fScale = Math.Min(rc.Width / size.Width, rc.Height / size.Height);

        size.Width *= fScale;
        size.Height *= fScale;

        Bitmap res = new Bitmap(image, size.ToSize());

        if (this.fillBackColorWithImage || (this.Image != StateButtonImage.UserDefined))
        {
          this.BackColor = res.GetPixel(0, 0);
          this.pictureBox.BackColor = this.BackColor;
        }

        return res;
      }
      else
        return image;
		}

		private void alarmTimer_Tick(object sender, AlarmTimerTickEventArgs e)
		{
			int indexAlarmImageToDisplay = -1;
			if (this.warned)
				indexAlarmImageToDisplay = INDEX_WARNING_IMAGE;
			if (this.alarmed)
				indexAlarmImageToDisplay = INDEX_ALARM_IMAGE;
			if (this.inEmergency)
				indexAlarmImageToDisplay = INDEX_INEMERGENCY_IMAGE;

			if (indexAlarmImageToDisplay != -1)
			{
				if (!e.AlarmVisible)
				{
					pictureBox.Visible = showImage;//force to original value because could have been changed by the ArrangeComponents(true)
					pictureBox.Image = getImageToDisplay();
					if (Switching)
					{
						//sinc no possible when some alarm is visible
						this.Active = !this.Active;
					}
				}
				else
				{
					if (imageListAlarms.Images.Count > indexAlarmImageToDisplay)
					{
						try
						{
							pictureBox.Image = imageListAlarms.Images[indexAlarmImageToDisplay];
              if (this.fillBackColorWithImage)
              {
                Bitmap res = new Bitmap(imageListAlarms.Images[indexAlarmImageToDisplay]);
                this.pictureBox.BackColor = res.GetPixel(0, 0);
              }
						}
						catch (Exception ex)
						{
							Logger.LogError(this, "alarmTimer_Tick - error accessing image list", ex);
						}
						ArrangeComponents(true);//pass true because when only text is showed the image is not visible
					}
				}
			}
			else //not alarmed nor warned nor inemergency
			{
				if (Switching)
				{
					//sinc
					//this.Active = !this.Active;
					if (!e.AlarmVisible)
						this.Active = true;
					else
						this.Active = false;
				}
			}

			if (this.Enabled && this.highLightEnable)
			{
				if (e.AlarmVisible)
				{
					currentFont = new Font(this.Font, this.Font.Style | FontStyle.Underline | FontStyle.Bold);
				}
				else
				{
					currentFont = this.Font;
				}
				this.Invalidate(textRectangle);
			}

			
		}

		private void StateButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (!this.isButton)
				return;

			if (e.KeyCode.Equals(Keys.Space))
			{
				if (!IsComponentCurrentlyClicked)
				{
					IsComponentCurrentlyClicked = true;
				}
			}
		}

		private void StateButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (!this.isButton)
				return;

			if (!IsComponentCurrentlyClicked)
			{
				IsComponentCurrentlyClicked = true;
			}
		}

		private void StateButton_KeyUp(object sender, KeyEventArgs e)
		{
			if (!this.isButton)
				return;

			if (e.KeyCode.Equals(Keys.Space))
			{
				PerformClick(sender, e);
				IsComponentCurrentlyClicked = false;
			}
		}

		private void StateButton_MouseUp(object sender, MouseEventArgs e)
		{
			if (!this.isButton)
				return;

			PerformClick(sender, e);
			IsComponentCurrentlyClicked = false;
		}

		public void PerformClick()
		{
			PerformClick(this, new System.EventArgs());
		}

		private void PerformClick(object sender, System.EventArgs e)
		{
			if (!this.isButton)
				return;

			if ((this.Click != null) && this.Enabled)
			{
				this.Click(this, e);
			}
		}

    public void resize()
    {
      this.pictureBox.Image = getImageToDisplay();
    }

		private void StateButton_EnabledChanged(object sender, EventArgs e)
		{
			if (this.Enabled)
			{
				//currentForeColor = ENABLED_TRUE_COLOR;
				currentForeColor = (this.active ? SyncroColor.White : ENABLED_TRUE_COLOR);
			}
			else
			{
				currentForeColor = ENABLED_FALSE_COLOR;
				currentFont = this.Font;
			}
			pictureBox.Image = getImageToDisplay();
			Invalidate();
		}

		private void StateButton_FontChanged(object sender, EventArgs e)
		{
			currentFont = this.Font;
			Invalidate(textRectangle);
		}
	}
}
