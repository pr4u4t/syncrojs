using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Drawing.Imaging;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using java.util.zip;
using java.io;
using System.Runtime.InteropServices;
using System.Text;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Controls;
using SyncroC.Controls.Interfaces;
using SyncroC.Forms.PopUp;
using SyncroC.Forms.StatusBar;
using SyncroC.DataBase;

//PMASSARO_FI05/02/09 - Added Command For Resetting Production
using System.Diagnostics;

namespace SyncroC.Forms
{


	/// <summary>
	/// Enumerates the Function keys
	/// </summary>
	public enum FKEYS : int
	{
		F1 = 0,
		F2 = 1,
		F3 = 2,
		F4 = 3,
		F5 = 4,
		F6 = 5,
		F7 = 6,
		F8 = 7,
		F9 = 8,
		F10 = 9,
		F11 = 10,
		F12 = 11,
		F13 = 12
	}


	/// <summary>
	/// Ancestor of every displayable form.
	/// </summary>
	public class Frm_Base : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		protected System.Windows.Forms.ImageList iml_AccessibilityImages;
		protected const int F_KEY_NUMBER = 13;
		protected bool[] FKeyEnable;
		protected bool[] ShiftFKeyEnable;
		protected bool[] AltFKeyEnable;
		protected bool[] CtrlFKeyEnable;
		private Thread copyLogThread;
		private Thread copyBackupDataThread;
    private Thread copyBackupSourceThread;
    private Thread copyBackupSourceOnlyThread;
    private Thread copyBackupExeOnlyThread;
    private Thread createDocThread;
		/// <summary>
		/// Index into the accessibility image list for locked areas.
		/// </summary>
		public const int IMG_LOCKED_INDEX = 0;

		/// <summary>
		/// Index into the accessibility image list for unlocked areas.
		/// </summary>
		public const int IMG_UNLOCKED_INDEX = 1;

		//	protected int CONSOLE_DISABLED_OFFSET = 20;

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
    [DllImport("kernel32.dll")]
    static extern bool PulseEvent(IntPtr hEvent);
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);


    // Declare external functions.
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);



		protected bool showDesignGrid = true;
		[Category("Design grid"), Description("If true the designer show a grid to help software designers")]
		public bool ShowDesignGrid
		{
			get { return showDesignGrid; }
			set
			{
				showDesignGrid = value;
				Invalidate();
			}
		}

		protected int designGridHorizontalLines = 3;
    private System.Windows.Forms.Timer timer_BringToFront;
  
		[Category("Design grid"), Description("Number of horizontal lines to be showed into the design grid.")]
		public int DesignGridHorizontalLines
		{
			get { return designGridHorizontalLines; }
			set
			{
				designGridHorizontalLines = value;
				Invalidate();
			}
		}

		protected int designGridVerticalLines = 4;
		[Category("Design grid"), Description("Number of vertical lines to be showed into the design grid.")]
		public int DesignGridVerticalLines
		{
			get { return designGridVerticalLines; }
			set
			{
				designGridVerticalLines = value;
				Invalidate();
			}
		}


		//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
		public static Screen myScreen = Screen.PrimaryScreen;

		/// <summary>
		/// Builder
		/// </summary>
		public Frm_Base()
		{
			InitializeComponent();

			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			Screen[] screens = Screen.AllScreens;
			if ( screens.Length > 1)
			{
				Settings s = Settings.getSettings();
				myScreen = screens[s.network.ClientID - 1];
			}

			//register to get events of Accessibility changed
			Accessibility.getAccessibility().OnAccessibilityChanged +=
				new SyncroC.Utility.Accessibility.AccessibilityChangedHandler(OnAccessibilityChanged);

			this.FKeyEnable = new bool[F_KEY_NUMBER];
			this.AltFKeyEnable = new bool[F_KEY_NUMBER];
			this.ShiftFKeyEnable = new bool[F_KEY_NUMBER];
			this.CtrlFKeyEnable = new bool[F_KEY_NUMBER];
			for (int i = 0; i <= F_KEY_NUMBER - 1; i++)
			{
				this.FKeyEnable[i] = true;
				this.AltFKeyEnable[i] = true;
				this.ShiftFKeyEnable[i] = true;
				this.CtrlFKeyEnable[i] = true;
			}
		}

		protected void askForPage(Type type)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(type));
		}

		protected void askForPage(Type type, int index)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(type, index));
		}

		/// <summary>
		/// Must be overridden by inheritors to let it return false if the components page should
		/// not included into the language database.
		/// </summary>
		/// <returns></returns>
		protected virtual bool IncludeComponentsIntoLanguageDB()
		{
			return true;
		}

		/// <summary>
		/// Clean up any resources.
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
			//deregister
			Accessibility.getAccessibility().OnAccessibilityChanged -=
				new SyncroC.Utility.Accessibility.AccessibilityChangedHandler(OnAccessibilityChanged);
			base.Dispose(disposing);
		}

		/// <summary>
		/// Calculates the command to be sent to the server to request the page of the current instance type
		/// </summary>
		/// <returns>the command</returns>
		public string getRequestCommand()
		{
			return PageManager.getRequestCommand(this.GetType());
		}

		public string getRequestCommand(int index)
		{
			return PageManager.getRequestCommand(this.GetType(), index);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// 		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Base));
			this.iml_AccessibilityImages = new System.Windows.Forms.ImageList(this.components);
			this.timer_BringToFront = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// iml_AccessibilityImages
			// 
			this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
			this.iml_AccessibilityImages.TransparentColor = System.Drawing.Color.Transparent;
			this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
			this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
			// 
			// timer_BringToFront
			// 
            this.timer_BringToFront.Enabled = false; //true; ///disable damn bring to front
			this.timer_BringToFront.Tick += new System.EventHandler(this.timer_BringToFront_Tick);
			// 
			// Frm_Base
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(292, 262);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.Name = "Frm_Base";
			this.ShowInTaskbar = false;
			this.Text = "Frm_Base";
			//this.TopMost = true;
			this.Load += new System.EventHandler(this.Frm_Base_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Frm_Base_KeyUp);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Base_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_Base_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Change icons on the page for the new accessibility level
		/// </summary>
		/// <param name="sender">The Accessibility object.</param>
		/// <param name="args"></param>
		private void OnAccessibilityChanged(object sender, SyncroC.Utility.EventArgs.AccessibilityChangedEventArgs args)
		{
			setAccessibilityIcons(args.accessibilityLevel);
			SetConsole(args.consoleEnabled, true);
		}

		/// <summary>
		/// must be overridden by pages that have areas with different accessibility.
		/// </summary>
		/// <param name="accessibilityLevel">The accessibility level to be set for the page.</param>
		protected virtual void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
		}

		private void SetConsole(bool enabled, bool needSetUp)
		{
			if (enabled)
			{
				if (this.FormBorderStyle != FormBorderStyle.None)
				{
					this.FormBorderStyle = FormBorderStyle.None;
				}
			}
			else
			{
				if (this.FormBorderStyle != FormBorderStyle.Sizable)
				{
					this.FormBorderStyle = FormBorderStyle.Sizable;
				}
			}
			if (needSetUp)
				this.SetUp(getLocation(), getSize(), Settings.getSettings().CurrentLanguage, true, false, false);

      //this.TopMost = false;
      //this.TopMost = enabled;
		}

		/// <summary>
		///must be overridden by pages that have different aspect and behaviour according 
		///to the client configuration.
		/// </summary>
		/// <remarks>
		/// For an example have a look in the Frm_Page where the method is overridden.
		/// </remarks>
		public virtual void ConfigPage(Settings.Configuration config)
		{
		}

		/// <summary>
		/// Must be overridden by pages that have different aspect according to the 
		/// corrugator direction
		/// </summary>
		public virtual void CorrugatorDirectionLeft()
		{
		}

		public virtual void MachineDirectionVisualizationInverted()
		{
		}

		/// <summary>
		///must be overridden by pages that show local database data.
		/// </summary>
		public virtual void refreshData()
		{
		}

    public virtual void refreshMySQLData()
    {
    }

		#region SetUp: translation and resizing

		/// <summary>
		/// must be overridden to specifie the page's position into the screen.
		/// </summary>
		protected virtual Point getLocation()
		{
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			return new Point(myScreen.Bounds.Left, 0);
		}

		protected virtual int getConsoleDisabledOffset()
		{
			return 38;//optimized for 1280 X 1024 graphics
		}

		/// <summary>
		///must be overridden to return the "design time" page's size.
		/// </summary>
		protected virtual Size getSize()
		{
			return new Size(300, 300);
		}

		/// <summary>
		///must be overridden to return the "design time" page's size.
		/// </summary>
		protected virtual Size getOriginalSize()
		{
			return new Size(300, 300);
		}

    protected Size getRealScreenSize()
    {
      return myScreen.Bounds.Size;
    }

    //ABELLO_FI29/05/09 Aggiunta gestione del mega-monitor
    protected Size getScreenSize()
    {
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			//Size res = Screen.PrimaryScreen.Bounds.Size;
			Size res = myScreen.Bounds.Size;

      if (res.Width > 2560)
        res.Width = res.Width / 2;

      //if (((Environment.UserName.ToUpper() == "ABELLO") || (Environment.UserName.ToUpper() == "RFAZZI") ) && 
      //      Debugger.IsAttached)
      {
        if (res.Width > 1280)
          res.Width = 1280;
      }

      return res;
    }

		private float getFontResizingFactor()
		{
			Size size = this.getScreenSize();
			switch (this.getScreenSize().Height)
			{
				// 800 X 600
				case 600: return 0.68F;
				// 1280 X 720
				case 720: return 0.72F;
				// 1024 X 768  &  1280 X 768
				case 768: return 0.76F;
				// 1152 X 864
				case 864: return 0.82F;
				// 1600 X 900
				case 900: return 0.85F;
				// 1280 X 960
				case 960: return 0.9F;
				// 1280 X 1024
				default:
				case 1024: return 1F;
				// 1400 X 1050
				case 1050:
					{
						if (this.getScreenSize().Width == 1680 )
							return 1.05F;
						else
							return 1.1F;
					}
				// 1920 X 1080
				case 1080: return 1.15F;
				// 1600 X 1200  &  1920 X 1200
				case 1200: return 1.18F;
				// 1856 X 1392
				case 1392: return 1.25F; //UNTESTED
				// 1920 X 1440
				case 1440: return 1.3F;
				// 2048 X 1536
				case 1536: return 1.35F;//UNTESTED
			}
		}

		/// <summary>
		/// translate the form into the specified language
		/// update the database if requested (by the updateDB flag) and needed, 
		/// (it does not set components bound)
		/// </summary>
		/// <param name="language">string representing the language to translate the page into.</param>
		/// <param name="updateDB">true if database has to be rebuilt, false otherwise.</param>
		public void SetUp(string language, bool updateDB)
		{
			//The first two parameters (location and size) are not significant because the setBounds flag is passed as false.
			SetUp(new Point(0, 0), new Size(300, 300), language, false, false, (updateDB && IncludeComponentsIntoLanguageDB()));
		}

		/// <summary>
		/// translate the form into the specified language,
		/// sets the location and the size of the form and adjusts all inside controls.
		/// (it does not restore the DB)
		/// </summary>
		/// <param name="location">location where the page has to be put on the screen.</param>
		/// <param name="size">size the page has to assume.</param>
		/// <param name="language">string representing the language to translate the page into.</param>
		public void SetUp(Point location, Size size, string language)
		{
			SetUp(location, size, language, false, true, false);
		}

		/// <summary>
		/// translate the form into the specified language, 
		/// update the database if needed and the flag updateDB is true
		/// and if the flag setBounds is true
		/// sets the location and the size of the form and adjusts all inside controls
		/// </summary>
		/// <param name="location"></param>
		/// <param name="size"></param>
		/// <param name="language"></param>
		/// <param name="setBounds"></param>
		/// <param name="updateDB">true if database has to be rebuilt, false otherwise.</param>
		private void SetUp(Point location, Size size, string language, bool setLocation, bool setBounds, bool updateDB)
		{
			//original bounds of every control's form
			Hashtable bounds = null;
			//Orizontal Resizing coefficient
			double OrizontalResizing = 0;
			//Vertital Resizing coefficient
			double VerticalResizing = 0;
			//Font Resizing coefficient
			float FontResizing = 0;
			if (setLocation || setBounds)
				this.Location = location;
			if (setBounds)
			{
				bounds = new Hashtable();
				saveBounds(bounds, this.Controls);
				OrizontalResizing = (Convert.ToDouble(size.Width) /
					Convert.ToDouble(getOriginalSize().Width));
				VerticalResizing = (Convert.ToDouble(size.Height) /
					Convert.ToDouble(getOriginalSize().Height));
				//FontResizing = (float)(Math.Min(OrizontalResizing, VerticalResizing));
				//double fontResizeCoeff = Math.Abs((1 - FontResizing) * 4.5);
				////double fontResizeCoeff = 0.95;
				//FontResizing = (float)((Math.Min(OrizontalResizing, VerticalResizing)) * fontResizeCoeff);
				FontResizing = getFontResizingFactor();
				this.Size = size;
			}
			setBoundsAndTranslations(bounds, this.Controls, OrizontalResizing, VerticalResizing,
			FontResizing, language, setBounds, updateDB, this.IncludeComponentsIntoLanguageDB());
		}

		/// <summary>
		/// utility for SetUp method
		/// </summary>
		/// <param name="bounds"></param>
		/// <param name="controls"></param>
		private void saveBounds(Hashtable bounds,
			System.Windows.Forms.Control.ControlCollection controls)
		{
			foreach (Control c in controls)
			{
				bounds.Add(c.Name, c.Bounds);
				//recursive call if the control is a container
				if (NeedRecursiveCall(c))
					saveBounds(bounds, c.Controls);

				//saving columns width in case of SyncroC.Controls.Grid 
				if (c is SyncroC.Controls.Grid)
					foreach (System.Windows.Forms.ColumnHeader column
								 in ((SyncroC.Controls.Grid)c).Columns)
					{
						try
						{
							bounds.Add(c.Name + column.Index, column.Width);
						}
						catch (ArgumentException ex)
						{
							Logger.LogError(this, "saveBounds - a control with the same key has already been added to the hashTable.", ex);
						}
					}

				//saving columns width in case of GridView
				if (c is SyncroC.Controls.GridView)
					foreach (System.Windows.Forms.DataGridViewColumn column
					in ((SyncroC.Controls.GridView)c).Columns)
					{
						try
						{
							bounds.Add(c.Name + column.Index, column.Width);
						}
						catch (ArgumentException ex)
						{
							Logger.LogError(this, "saveBounds - a gridView column with the same key has already been added to the hashTable.", ex);
						}
					}
			}
		}

		/// <summary>
		/// Utility function for SetUp method, recurses on the form's components to resize everything
		/// </summary>
		/// <param name="bounds">original design time bounds.</param>
		/// <param name="controls"></param>
		/// <param name="orizontalResizing"></param>
		/// <param name="verticalResizing"></param>
		/// <param name="fontResizing"></param>
		/// <param name="language"></param>
		/// <param name="setBounds"></param>
		/// <param name="updateDB">true if database has to be rebuilt, false otherwise.</param>
		protected virtual void setBoundsAndTranslations(Hashtable bounds,
			System.Windows.Forms.Control.ControlCollection controls, double orizontalResizing,
			double verticalResizing, float fontResizing, string language, bool setBounds, bool updateDB, bool isIncludeComponentsIntoLanguageDB)
		{
			foreach (Control c in controls)
			{
				//bounds settings: only if needed
				if (setBounds && orizontalResizing != 1 && verticalResizing != 1)
				{
					c.Bounds = new Rectangle(
						Convert.ToInt16(((Rectangle)bounds[c.Name]).X * orizontalResizing),
						Convert.ToInt16(((Rectangle)bounds[c.Name]).Y * verticalResizing),
						Convert.ToInt16(((Rectangle)bounds[c.Name]).Width * orizontalResizing),
						Convert.ToInt16(((Rectangle)bounds[c.Name]).Height * verticalResizing));
					//resize the font of the control
					if (c.Font.Equals(c.Parent.Font) &&
						!(c is SyncroUserControl) &&
						(c.Parent is System.Windows.Forms.Panel || c.Parent is System.Windows.Forms.GroupBox ||
						c.Parent is SyncroC.Controls.GroupPanel.TabPage || c.Parent is SyncroC.Controls.GroupPanel.GroupPanel))
					{
						//don't resize objects with the default font in panels or boxes because
						//they are resized by the container automatically
						//c.Font = new Font(c.Font.FontFamily, c.Font.Size * fontResizing, c.Font.Style);
					}
					else
					{
						c.Font = new Font(c.Font.FontFamily, c.Font.Size * fontResizing, c.Font.Style);
					}
					//resizing of SyncroUserControl
					if (c is SyncroUserControl)
						((SyncroUserControl)c).AdaptToScreen(verticalResizing, orizontalResizing, fontResizing);

					//resizing of SyncroC.Controls.Grid columns
					if (c is SyncroC.Controls.Grid)
						foreach (System.Windows.Forms.ColumnHeader column
									in ((SyncroC.Controls.Grid)c).Columns)
							column.Width = Convert.ToInt16((int)bounds[c.Name + column.Index] * orizontalResizing);

					//resizing of SyncroC.Controls.GridView columns
					if (c is SyncroC.Controls.GridView)
						foreach (System.Windows.Forms.DataGridViewColumn column
									in ((SyncroC.Controls.GridView)c).Columns)
							column.Width = Convert.ToInt16((int)bounds[c.Name + column.Index] * orizontalResizing);
				}
				if (isIncludeComponentsIntoLanguageDB)
				{
					//translation settings
					if (c is SyncroUserControl)
					{
						if (((SyncroUserControl)c).TranslateText)
							c.Text = GetTranslation(c, "Text", language, updateDB);
					}
					else
					{
						//check suffix of name and translate control and/or tooltip accordingly to the postfix
						if (c.Name.Length >= 3 && c.Name[c.Name.Length - 3].Equals('_'))
						{
							string postFix = c.Name.Substring(c.Name.Length - 2, 2).ToUpper().Trim();
							switch (postFix)
							{
								case "YY":
									c.Text = GetTranslation(c, "Text", language, updateDB);
									break;
								case "NY":
									break;
								case "YN":
									c.Text = GetTranslation(c, "Text", language, updateDB);
									break;
							}
						}
					}
					//the component contains a messageCode to be decoded into the language
					if (c.Tag != null && !c.Tag.Equals(""))
						c.Text = DBManager.getDBManager().message.getTranslatedMessage(c.Tag.ToString(), language);
					//the component is a GridView so columns need to be translated
					if (c is SyncroC.Controls.GridView)
					{
						foreach (DataGridViewColumn column in ((SyncroC.Controls.GridView)c).Columns)
							column.HeaderText = GetTranslation(c, column.Index.ToString(), language, updateDB);
						c.Invalidate();
					}
					//the component is a Grid so columns need to be translated
					else if (c is SyncroC.Controls.Grid)
					{
						foreach (ColumnHeader column in ((SyncroC.Controls.Grid)c).Columns)
							column.Text = GetTranslation(c, column.Index.ToString(), language, updateDB);
						c.Invalidate();
					}
				}

				//recursive call on components that can contain other components..
				if (NeedRecursiveCall(c))
				{
					setBoundsAndTranslations(bounds, c.Controls, orizontalResizing, verticalResizing,
						fontResizing, language, setBounds, updateDB, isIncludeComponentsIntoLanguageDB);
				}
			}
		}

		/// <summary>
		/// Determine if the component needs to be resized recursively for the contained components.
		/// </summary>
		/// <param name="c">the component to determine whether has to recursively resized.</param>
		/// <returns>true if recursive resizing is needed, false otherwise.</returns>
		private bool NeedRecursiveCall(Component c)
		{
			return
				(c is System.Windows.Forms.Panel) ||
				(c is System.Windows.Forms.GroupBox) ||
				(c is System.Windows.Forms.TabControl) ||
				(c is System.Windows.Forms.TabPage) ||
				(c is SyncroC.Controls.GroupPanel.GroupPanel) ||
				(c is SyncroC.Controls.GroupPanel.TabPage) ||
				(c is SyncroC.Controls.LanguageAndAccessibilitySettings) ||
				(c is SyncroC.Controls.SlitterScorerShaftStatus) ||
				(c is SyncroC.Controls.SlitterScorerSetupStatus);
		}

		/// <summary>
		/// utility for SetUp method:
		/// gets the requestedText translation into the specified language for the specified
		/// control into the specified form.
		/// </summary>
		/// <param name="control">the control which the translation has to be got.</param>
		/// <param name="requestedText">a string specifing which translation to get for the control:
		/// "Text" if the translation for the Text property is required;
		/// "Tlp_Description" if the translation for the Tlp_Description is required;
		/// a column index number if the translation for a ColumnHeader component is required. </param>
		/// <param name="language">a string representing the language to get the translation.</param>
		/// <param name="updateDB">true if database has to be rebuilt, false otherwise.</param>
		/// <returns>required translation.</returns>
		private string GetTranslation(Control control, string requestedText, string language, bool updateDB)
		{
			DBManager dbm = DBManager.getDBManager();

			//string to be returned
			string translation = "";
			switch (requestedText)
			{//default return: design time text. 
				case "Text":
					translation = control.Text;
					break;
				default:
					int index = 0;
					try
					{
						index = Convert.ToInt32(requestedText);
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "GetTranslation - error converting column index \"" + requestedText + "\"", ex);
					}
					if (control is SyncroC.Controls.GridView)
					{//the required text is a translation for a DataGridViewColumn of a SyncroC.Controls.GridView
						if (index < ((SyncroC.Controls.GridView)control).Columns.Count)
							translation = ((SyncroC.Controls.GridView)control).Columns[index].HeaderText;
					}
					else if (control is SyncroC.Controls.Grid)
					{//the required text is a translation for a ColumnHeader of a SyncroC.Controls.Grid
						if (index < ((SyncroC.Controls.Grid)control).Columns.Count)
							translation = ((SyncroC.Controls.Grid)control).Columns[index].Text;
					}
					break;
			}

			//check for db errors
			if (dbm.language.language == null || dbm.language.language.Tables.Count == 0)
			{
				Logger.LogLanguages(this, "GetTranslation - the language dataset is null or empty. Design time requested text has been returned.");
				return translation;
			}

			if (!translation.Equals(""))
			{
				DataRow foundRow = null;
				Type formType = this.GetType();
				string key = string.Empty;

				while (foundRow == null && !formType.Equals(typeof(Frm_Base)))
				{
					key = formType.Name + "." + control.Name + "." + requestedText;
					foundRow = dbm.language.language.Tables[0].Rows.Find(key);
					formType = formType.BaseType;
				}
				if (foundRow == null)
				{	//record is not present into the dataset
					Logger.LogLanguages(this, "GetTranslation - record \"" + key + "\" not found. Design time requested text has been returned.");
					if (updateDB)
					{//updating the dataset is enabled
						formType = this.GetType();
						key = string.Empty;
						while (!formType.Equals(typeof(Frm_Base)) && key.Equals(string.Empty))
						{
							FieldInfo fieldInfo = formType.BaseType.GetField(control.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
							if (fieldInfo == null)
							{
								key = formType.Name + "." + control.Name + "." + requestedText;
							}
							else
							{
								object[] parameters = null;
								Frm_Base formBase = (Frm_Base)Activator.CreateInstance(formType.BaseType, parameters);

								object c = fieldInfo.GetValue(formBase);

								if (typeof(Control).IsAssignableFrom(c.GetType()))
								{
									string txt = string.Empty;
									switch (requestedText)
									{
										case "Text":
											txt = ((Control)c).Text;
											break;
									}
									if (!txt.Equals(translation))
									{
										key = formType.Name + "." + control.Name + "." + requestedText;
									}
									else
									{
										formType = formType.BaseType;
									}
								}
								formBase.Dispose();
							}
						}
            //agrassi_FI04-07-08
            //DataRow newRow = dbm.language.language.Tables[0].NewRow();
            //if (newRow == null || newRow.Table.Columns.Count < 2)
            //{
            //  Logger.LogLanguages(this, "GetTranslation - impossible to create a new record for the language dataset or the language dataset schema is wrong (update failed). Design time requested text has been returned.");
            //  return translation;
            //}
            //newRow[0] = key;
            //newRow[1] = translation;
            //for (int i = 2; i < newRow.ItemArray.Length; i++)
            //  newRow[i] = "";
            //dbm.language.language.Tables[0].Rows.Add(newRow);
            dbm.language.AddItem(key, translation);
					}
					return translation;
				}

				//record is present into the database so check if the design time Text (or tooltip) has been changed
				if (!translation.Equals(foundRow[1].ToString()) && updateDB)
				{	//design time has been changed so delete the row with the "old" text
					//and add a new row with the "new" text specified at design time
					Logger.LogLanguages(this, "GetTranslation - Design time for the record (\"" + key + "\") has been changed.");
					foundRow.Delete();
					DataRow newRow = dbm.language.language.Tables[0].NewRow();
					if (newRow == null || newRow.Table.Columns.Count < 2)
					{
						Logger.LogLanguages(this, "GetTranslation - impossible to create a new record for the language dataset or the language dataset schema is wrong (update failed). Design time requested text has been returned.");
						return translation;
					}
					newRow[0] = key;
					newRow[1] = translation;
					for (int i = 2; i < newRow.ItemArray.Length; i++)
						newRow[i] = "";
					dbm.language.language.Tables[0].Rows.Add(newRow);
					return translation;
				}

				//record is present into the dataset so look for the specified language
				int index = foundRow.Table.Columns.IndexOf(language);
				if (index == -1)
				{
					Logger.LogLanguages(this, "GetTranslation - language \"" + language + "\" not present into database. The English translation has been returned.");
					return foundRow[1].ToString();
				}

				//the specified language is present, check if a string has been specified
				if (foundRow[index].ToString().Equals(""))
				{
					Logger.LogLanguages(this, "GetLanguageText - Form: \"" + this.Name + "\"; Control: \"" + control.Name + "\"; missing translation for language \"" + language + "\". The english translation has been returned.");
					return foundRow[1].ToString();
				}

				//a string has been specified for the control and language so it is returned
				return foundRow[index].ToString();
			}
			else
				//any string has been specified for the control at design time
				//so return the default translation value that is an empty string.
				return translation;
		}

		#endregion

		#region Key_Down handlers methods
		private void Frm_Base_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Alt && (e.KeyCode.Equals(Keys.F4) || e.KeyCode.Equals(Keys.F6)))
				e.Handled = true;
		}
		#endregion

		#region Key_Up handlers methods
		/// <summary>
		/// call the correct function for every combination of key up.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Frm_Base_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//only key
			if (!(e.Shift || e.Control || e.Alt))
				switch (e.KeyCode)
				{
					case Keys.F1: if (this.FKeyEnable[(int)FKEYS.F1]) F1(); break;
					case Keys.F2: if (this.FKeyEnable[(int)FKEYS.F2]) F2(); break;
					case Keys.F3: if (this.FKeyEnable[(int)FKEYS.F3]) F3(); break;
					case Keys.F4: if (this.FKeyEnable[(int)FKEYS.F4]) F4(); break;
					case Keys.F5: if (this.FKeyEnable[(int)FKEYS.F5]) F5(); break;
					case Keys.F6: if (this.FKeyEnable[(int)FKEYS.F6]) F6(); break;
					case Keys.F7: if (this.FKeyEnable[(int)FKEYS.F7]) F7(); break;
					case Keys.F8: if (this.FKeyEnable[(int)FKEYS.F8]) F8(); break;
					case Keys.F9: if (this.FKeyEnable[(int)FKEYS.F9]) F9(); break;
					case Keys.F10: if (this.FKeyEnable[(int)FKEYS.F10]) F10(); break;
					case Keys.F11: if (this.FKeyEnable[(int)FKEYS.F11]) F11(); break;
					case Keys.F12: if (this.FKeyEnable[(int)FKEYS.F12]) F12(); break;
          case Keys.F13: if (this.FKeyEnable[(int)FKEYS.F13]) F13(); break;

					case Keys.Insert: Insert(); break;
					case Keys.Home: Home(); break;
					case Keys.PageUp: PageUp(); break;
					//case Keys.Cancel:		Cancel();		break;
					case Keys.End: End(); break;
					case Keys.PageDown: PageDown(); break;

					case Keys.Escape: Escape(); break;
					case Keys.Return: Return(); break;
					case Keys.Pause: Pause(); break;

					case Keys.Left: LeftArrow(); break;
					case Keys.Right: RightArrow(); break;
					case Keys.Up: UpArrow(); break;
					case Keys.Down: DownArrow(); break;
				}

 //Shift + key
			else if (e.Shift && !(e.Control || e.Alt))
				switch (e.KeyCode)
				{
					case Keys.F1: if (this.ShiftFKeyEnable[(int)FKEYS.F1]) ShiftF1(); break;
					case Keys.F2: if (this.ShiftFKeyEnable[(int)FKEYS.F2]) ShiftF2(); break;
					case Keys.F3: if (this.ShiftFKeyEnable[(int)FKEYS.F3]) ShiftF3(); break;
					case Keys.F4: if (this.ShiftFKeyEnable[(int)FKEYS.F4]) ShiftF4(); break;
					case Keys.F5: if (this.ShiftFKeyEnable[(int)FKEYS.F5]) ShiftF5(); break;
					case Keys.F6: if (this.ShiftFKeyEnable[(int)FKEYS.F6]) ShiftF6(); break;
					case Keys.F7: if (this.ShiftFKeyEnable[(int)FKEYS.F7]) ShiftF7(); break;
					case Keys.F8: if (this.ShiftFKeyEnable[(int)FKEYS.F8]) ShiftF8(); break;
					case Keys.F9: if (this.ShiftFKeyEnable[(int)FKEYS.F9]) ShiftF9(); break;
					case Keys.F10: if (this.ShiftFKeyEnable[(int)FKEYS.F10]) ShiftF10(); break;
					case Keys.F11: if (this.ShiftFKeyEnable[(int)FKEYS.F11]) ShiftF11(); break;
					case Keys.F12: if (this.ShiftFKeyEnable[(int)FKEYS.F12]) ShiftF12(); break;
				}

			//Control + key
			else if (e.Control && !(e.Shift || e.Alt))
				switch (e.KeyCode)
				{
					case Keys.F1: if (this.CtrlFKeyEnable[(int)FKEYS.F1]) CtrlF1(); break;
					case Keys.F2: if (this.CtrlFKeyEnable[(int)FKEYS.F2]) CtrlF2(); break;
					case Keys.F3: if (this.CtrlFKeyEnable[(int)FKEYS.F3]) CtrlF3(); break;
					case Keys.F4: if (this.CtrlFKeyEnable[(int)FKEYS.F4]) CtrlF4(); break;
					case Keys.F5: if (this.CtrlFKeyEnable[(int)FKEYS.F5]) CtrlF5(); break;
					case Keys.F6: if (this.CtrlFKeyEnable[(int)FKEYS.F6]) CtrlF6(); break;
					case Keys.F7: if (this.CtrlFKeyEnable[(int)FKEYS.F7]) CtrlF7(); break;
					case Keys.F8: if (this.CtrlFKeyEnable[(int)FKEYS.F8]) CtrlF8(); break;
					case Keys.F9: if (this.CtrlFKeyEnable[(int)FKEYS.F9]) CtrlF9(); break;
					case Keys.F10: if (this.CtrlFKeyEnable[(int)FKEYS.F10]) CtrlF10(); break;
					case Keys.F11: if (this.CtrlFKeyEnable[(int)FKEYS.F11]) CtrlF11(); break;
					case Keys.F12: if (this.CtrlFKeyEnable[(int)FKEYS.F12]) CtrlF12(); break;

					case Keys.L: ControlL(); break;
					case Keys.D: ControlD(); break;
				}

		 //Alt + key
			else if (e.Alt && !(e.Shift || e.Control))
				switch (e.KeyCode)
				{
					case Keys.F1: if (this.AltFKeyEnable[(int)FKEYS.F1]) AltF1(); break;
					case Keys.F2: if (this.AltFKeyEnable[(int)FKEYS.F2]) AltF2(); break;
					case Keys.F3: if (this.AltFKeyEnable[(int)FKEYS.F3]) AltF3(); break;
					case Keys.F4: if (this.AltFKeyEnable[(int)FKEYS.F4]) AltF4(); break;
					case Keys.F5: if (this.AltFKeyEnable[(int)FKEYS.F5]) AltF5(); break;
					case Keys.F6: if (this.AltFKeyEnable[(int)FKEYS.F6]) AltF6(); break;
					case Keys.F7: if (this.AltFKeyEnable[(int)FKEYS.F7]) AltF7(); break;
					case Keys.F8: if (this.AltFKeyEnable[(int)FKEYS.F8]) AltF8(); break;
					case Keys.F9: if (this.AltFKeyEnable[(int)FKEYS.F9]) AltF9(); break;
					case Keys.F10: if (this.AltFKeyEnable[(int)FKEYS.F10]) AltF10(); break;
					case Keys.F11: if (this.AltFKeyEnable[(int)FKEYS.F11]) AltF11(); break;
					case Keys.F12: if (this.AltFKeyEnable[(int)FKEYS.F12]) AltF12(); break;
				}

			//Control + Alt + key
			else if (e.Control && e.Alt && !e.Shift)
				switch (e.KeyCode)
				{
					case Keys.X: ControlAltX(); break;
					case Keys.Y: ControlAltY(); break;
					case Keys.D: ControlAltD(); break;
				}

			//Control + Shit + key
			else if (e.Control && e.Shift && !e.Alt)
				switch (e.KeyCode)
				{
					case Keys.D: ControlShiftD(); break;
					case Keys.S: ControlShiftS(); break;
					case Keys.C: ControlShiftC(); break;
					case Keys.L: ControlShiftL(); break;
					case Keys.R: ControlShiftR(); break;
          case Keys.O: ControlShiftO(); break;
          //agrassi_FI05-12-08_1
          case Keys.Z: ControlShiftZ(); break;
          case Keys.E: ControlShiftE(); break;
        }

			//Alt + Shift + key
			else if (e.Alt && e.Shift && !e.Control)
				switch (e.KeyCode)
				{
					case Keys.F1: if (this.AltFKeyEnable[(int)FKEYS.F1]) AltShiftF1(); break;
					case Keys.F2: if (this.AltFKeyEnable[(int)FKEYS.F2]) AltShiftF2(); break;
					case Keys.F3: if (this.AltFKeyEnable[(int)FKEYS.F3]) AltShiftF3(); break;
					case Keys.F4: if (this.AltFKeyEnable[(int)FKEYS.F4]) AltShiftF4(); break;
					case Keys.F5: if (this.AltFKeyEnable[(int)FKEYS.F5]) AltShiftF5(); break;
					case Keys.F6: if (this.AltFKeyEnable[(int)FKEYS.F6]) AltShiftF6(); break;
					case Keys.F7: if (this.AltFKeyEnable[(int)FKEYS.F7]) AltShiftF7(); break;
					case Keys.F8: if (this.AltFKeyEnable[(int)FKEYS.F8]) AltShiftF8(); break;
					case Keys.F9: if (this.AltFKeyEnable[(int)FKEYS.F9]) AltShiftF9(); break;
					case Keys.F10: if (this.AltFKeyEnable[(int)FKEYS.F10]) AltShiftF10(); break;
					case Keys.F11: if (this.AltFKeyEnable[(int)FKEYS.F11]) AltShiftF11(); break;
					case Keys.F12: if (this.AltFKeyEnable[(int)FKEYS.F12]) AltShiftF12(); break;
				}

			//Alt + Shift + Control + key
      else if (e.Alt && e.Shift && e.Control)
        switch (e.KeyCode)
        {
          case Keys.P: ControlShiftAltP(); break;
          //agrassi_FI01-10-2010_1
          case Keys.B: ControlShiftAltB(); break;
          //agrassi_FI19-10-2009_2
          case Keys.S: ControlShiftAltS(); break;
          case Keys.Z: ControlShiftAltZ(); break;
          case Keys.E: ControlShiftAltE(); break;
          //MSTAGETTI_FI-11-05-2010_1
          case Keys.T: ControlShiftAltT(); break;
        }
		}

		/*
		 * F1..F13
		 */
		/// <summary>must be overridden to change the F1 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F1() { }
		/// <summary>must be overridden to change the F2 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F2() { }
		/// <summary>must be overridden to change the F3 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F3() { }
		/// <summary>must be overridden to change the F4 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F4() { }
		/// <summary>must be overridden to change the F5 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F5() { }
		/// <summary>must be overridden to change the F6 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F6() { }
		/// <summary>must be overridden to change the F7 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F7() { }
		/// <summary>must be overridden to change the F8 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F8() { }
		/// <summary>must be overridden to change the F9 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F9() { }
		/// <summary>must be overridden to change the F10 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F10() { }
		/// <summary>must be overridden to change the F11 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F11() { }
		/// <summary>must be overridden to change the F12 key and related labels behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void F12() { }
    /// <summary>must be overridden to change the F13 key and related labels behaviour.</summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void F13() { }

		/*
		 * CtrlF1..CtrlF12
		 */
		/// <summary>must be overridden to change the CTRL + F1 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF1() { }
		/// <summary>must be overridden to change the CTRL + F2 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF2() { }
		/// <summary>must be overridden to change the CTRL + F3 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF3() { }

		private DateTime CtrlF4_LastValid;
		/// <summary>
		/// returns true if at least MinSpan time has passed since the function was called and returned true 
		/// last time.
		/// It is supposed to be called into the Ctrl+F4 methods to implement filtering of consecutive click
		/// </summary>
		/// <param name="MinSpan"></param>
		/// <returns></returns>
		protected bool CtrlF4_isValid(TimeSpan MinSpan)
		{
			if ((DateTime.Now - CtrlF4_LastValid) <= MinSpan)
				return false;
			else
			{
				CtrlF4_LastValid = DateTime.Now;
				return true;
			}
		}
		/// <summary>must be overridden to change the CTRL + F4 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF4() { }
		/// <summary>must be overridden to change the CTRL + F5 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF5() { }
		/// <summary>must be overridden to change the CTRL + F6 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF6() { }
		/// <summary>must be overridden to change the CTRL + F7 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF7() { }
		/// <summary>must be overridden to change the CTRL + F8 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF8() { }
		/// <summary>must be overridden to change the CTRL + F9 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF9() { }
		/// <summary>must be overridden to change the CTRL + F10 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF10() { }
		/// <summary>must be overridden to change the CTRL + F11 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF11() { }
		/// <summary>must be overridden to change the CTRL + F12 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void CtrlF12() { }

		/* 
		 * ShiftF1..ShiftF12
		 */
		/// <summary>must be overridden to change the Shift + F1 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF1() { }
		/// <summary>must be overridden to change the Shift + F2 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF2() { }
		/// <summary>must be overridden to change the Shift + F3 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF3() { }
		/// <summary>must be overridden to change the Shift + F4 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF4() { }
		/// <summary>must be overridden to change the Shift + F5 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF5() { }
		/// <summary>must be overridden to change the Shift + F6 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF6() { }
		/// <summary>must be overridden to change the Shift + F7 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF7() { }
		/// <summary>must be overridden to change the Shift + F8 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF8() { }
		/// <summary>must be overridden to change the Shift + F9 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF9() { }
		/// <summary>must be overridden to change the Shift + F10 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF10() { }
		/// <summary>must be overridden to change the Shift + F11 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF11()
		{
			if (Settings.getSettings().configuration.WetEndControl)
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				PageManager pm = PageManager.getPageManager();
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageWetEndSupervisor);
			}
		}
		/// <summary>must be overridden to change the Shift + F12 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ShiftF12() { }

		/*
		 * AltF1..AltF12
		 */
		/// <summary>must be overridden to change the Alt + F1 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF1()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageDryEndSupervisor);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageWetEndSupervisor);
		}
		/// <summary>must be overridden to change the Alt + F2 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF2()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageDoubleBacker);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer5);
		}
		/// <summary>must be overridden to change the Alt + F3 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF3()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageShear);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSingleFacer2);
		}
		/// <summary>must be overridden to change the Alt + F4 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF4()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
			{
				if (Settings.getSettings().configuration.IsSyncro())
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSlitterScorer);
        else if (Settings.getSettings().configuration.IsSyncro400())
        {
          //agrassi_FI25-08-08
          SyncroC.Utility.Settings.Configuration conf = SyncroC.Utility.Settings.getSettings().configuration;
          SyncroC.Utility.Settings.Forms forms = SyncroC.Utility.Settings.getSettings().forms;
          if ((!conf.getOnServer()) && (forms.FirstPage == "twin400_console"))
          {
            if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarTwin400)))
              askForPage(typeof(Frm_StatusBarTwin400));
          }
          else
          {
            if (!pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
              askForPage(typeof(Frm_StatusBarDryEnd));
          }
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageTwin400);
        }
			}
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer4);
		}
		/// <summary>must be overridden to change the Alt + F5 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF5()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageKnife);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer3);
		}
		/// <summary>must be overridden to change the Alt + F6 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF6()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStacker1);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSingleFacer1);
		}
		/// <summary>must be overridden to change the Alt + F7 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF7()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStacker2);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer2);
		}
		/// <summary>must be overridden to change the Alt + F8 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF8()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarDryEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStacker3);
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer1);
		}
		/// <summary>must be overridden to change the Alt + F9 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF9()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPagePreHeaterTern);
		}
		/// <summary>must be overridden to change the Alt + F10 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF10()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageGlueUnit);
		}
		/// <summary>must be overridden to change the Alt + F11 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF11()
		{
			PageManager pm = PageManager.getPageManager();
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageDoubleBacker);
		}

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    /// <summary>must be overridden to change the Alt + F12 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltF12() 
    {
      PageManager pm = PageManager.getPageManager();
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer6);
    }

		/*
		 * AltShiftF1..AltShiftF12
		 */
		/// <summary>must be overridden to change the Alt + Shift + F1 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF1() 
    {
      PageManager pm = PageManager.getPageManager();
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicer7);
    }
		/// <summary>must be overridden to change the Alt + Shift + F2 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF2() 
    {
      PageManager pm = PageManager.getPageManager();
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      if (pm.IsStatusBarCurrentlyShowed(typeof(Frm_StatusBarWetEnd)))
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSingleFacer3);
    }
		/// <summary>must be overridden to change the Alt + Shift + F3 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF3() { }
		/// <summary>must be overridden to change the Alt + Shift + F4 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF4() { }
		/// <summary>must be overridden to change the Alt + Shift + F5 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF5() { }
		/// <summary>must be overridden to change the Alt + Shift + F6 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF6() { }
		/// <summary>must be overridden to change the Alt + Shift + F7 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF7() { }
		/// <summary>must be overridden to change the Alt + Shift + F8 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF8() { }
		/// <summary>must be overridden to change the Alt + Shift + F9 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF9() { }
		/// <summary>must be overridden to change the Alt + Shift + F10 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF10() { }
		/// <summary>must be overridden to change the Alt + Shift + F11 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF11() { }
		/// <summary>must be overridden to change the Alt + Shift + F12 key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void AltShiftF12() { }

		/*
		 * Home PagUp PagDown ecc...
		 */
		/// <summary>must be overridden to change the Insert key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void Insert() { }
		/// <summary>must be overridden to change the Home key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void Home() { }
		/// <summary>must be overridden to change the PageUp key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void PageUp() { }
		//		/// <summary>must be overridden to change the Cancel key and related label behaviour.</summary>
		//		[MethodImpl(MethodImplOptions.Synchronized)]
		//		protected virtual void Cancel(){}
		/// <summary>must be overridden to change the End key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void End() { }
		/// <summary>must be overridden to change the PageDown key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void PageDown() { }
		/// <summary>must be overridden to change the Escape key behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void Escape() { }
		/// <summary>must be overridden to change the Return key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void Return()
		{
			SendKeys.Send("{TAB}");
		}
		/// <summary>must be overridden to change the Pause key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void Pause() { }
		/// <summary>must be overridden to change the Left Arrow key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void LeftArrow() {}
		/// <summary>must be overridden to change the Right Arrow key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void RightArrow() {}
		/// <summary>must be overridden to change the Up Arrow key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void UpArrow() { }
		/// <summary>must be overridden to change the Down Arrow key and related label behaviour.</summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void DownArrow() { }


		/*
		 * CtrlAlt..
		 */
		/// <summary>
		/// must be overridden to change the Ctrl + Alt + X  key behaviour.
		/// Ask the server to shut the application down.
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlAltX()
		{
			Accessibility acc = Accessibility.getAccessibility();
      //agrassi_FI25-08-08_5
      if (Settings.getSettings().configuration.getOnFosberNetwork() && (!acc.isLevelHigh()))
      //if (Settings.getSettings().configuration.getOnServer() && (!acc.isLevelAtLeastMedium()))
        return;

			PageManager.getPageManager()._SetMessage("§SYCC_00");
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			if (cm.CanSendTCP())
			{	//ask the server to shout down the client
				cm.SendTcp(cm.SYNCROS_COMM_MANAGER, Command.COMMUNICATION_MANAGER + "." + Command.SHUTDOWN + "()");
			}
			else
			{
				Logger.LogError(this, "TCP connection down: Client shuts down by itself.");
				//probably the server is already down... so just shout down.
				cm.ShutDown(false, false);
			}
		}

		//AGrassi_FI 08-03-2007 Automatic simulation
		protected void CloseSyncroCompletely()
		{
			PageManager.getPageManager()._SetMessage("§SYCC_144", false, true);
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.SHUTDOWNALL));
		}

		/// <summary>
		/// must be overridden to change the Ctrl + Alt + Y  key behaviour.
		/// Ask the server to shut down all.
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlAltY()
		{
			Accessibility acc = Accessibility.getAccessibility();
			if (!acc.isLevelHigh())
				return;

			if (!Settings.getSettings().configuration.getOnServer())
				return;
			bool ok = false;
			PopUp.Frm_Confirm conf = new Frm_Confirm();
			string txtKey = "§SYCC_77";
			if (Settings.getSettings().configuration.IsPmac400())
				txtKey = "§SYCC_143";
			string text = DBManager.getDBManager().message.getTranslatedMessage(txtKey);
			DialogResult res = conf.ShowDialog(this, text);
			ok = (res == DialogResult.Yes) || (res == DialogResult.OK);
			if (ok)
			{
				//AGrassi_FI 08-03-2007 Automatic simulation
				this.CloseSyncroCompletely();
				//PageManager.getPageManager()._SetMessage("§SYCC_144", false, true);
				//CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				//cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.SHUTDOWNALL));
			}
		}

		/// <summary>
		/// must be overridden to change the Ctrl + Alt + D  key behaviour.
		/// Determine the Debug group visibility.
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlAltD()
		{
		}

		/*
		 * CtrlShift..
		 */

		/// <summary>
		/// must be overridden to change the Ctrl + Shift + C  key behaviour.
		/// Toggle the Console enabling.
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlShiftC()
		{
      //agrassi_FI26-06-08_1
      Accessibility acc = Accessibility.getAccessibility();
      //agrassi_FI22-08-08
      //if (!acc.isLevelHigh())
      //agrassi_FI25-08-08_5
      if ((!acc.isLevelHigh()) && Settings.getSettings().configuration.getOnFosberNetwork())
      //if ((!acc.isLevelHigh()) && Settings.getSettings().configuration.getOnServer())
        return;
      
			acc.ConsoleEnabled = !acc.ConsoleEnabled;
		}

		/// <summary>
		/// must be overridden to change the Ctrl + Shift + L  key behaviour.
		/// Copy log files
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlShiftL()
		{
			if (((this.copyLogThread == null) || (!this.copyLogThread.IsAlive)))
			{
        //ABELLO_FI26/05/2008_1 Aggiunto il Task ABEventLog e tutta la sua gestione 
        //Manda l'evento a ABEventLog

        IntPtr _Handle = CreateEvent(IntPtr.Zero, true, false, "LogToAllThread");
        PulseEvent(_Handle);
        CloseHandle(_Handle);

				this.copyLogThread = new Thread(new ThreadStart(this.CopyLog));
        //RFAZZI_FI28/10/2010_1. Inserita gestione per velocizzare la creazione degli zip
        //this.copyLogThread.Priority = ThreadPriority.Lowest;
        this.copyLogThread.Priority = ThreadPriority.Normal;
        this.copyLogThread.Name = "CopyLog";
				this.copyLogThread.Start();
			}
		}

		/// <summary>
		/// must be overridden to change the Ctrl + Shift + R  key behaviour.
		/// Copy Report files
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlShiftR()
		{
			//send the Alignment command to the server in order to make all db versions saved and receive the SaveReport command
			SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
			PageManager.getPageManager().Invoke(SetMsg, new object[] { "Saving and coping Report files ...", false, true });
			DBManager.getDBManager().SendAlignmentCommand(true);
		}

		/// <summary>
		/// must be overridden to change the Ctrl + Shift + D  key behaviour.
		/// Copy Data files
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlShiftD()
		{
			Accessibility acc = Accessibility.getAccessibility();
			if (!acc.isLevelAtLeastLow())
				return;

			if (((this.copyBackupDataThread == null) || (!this.copyBackupDataThread.IsAlive)))
			{
				this.copyBackupDataThread = new Thread(new ThreadStart(this.CopyData));
        //RFAZZI_FI28/10/2010_1. Inserita gestione per velocizzare la creazione degli zip
        if (RTCManager.getRTCManager().IsIdleShift)
          this.copyBackupDataThread.Priority = ThreadPriority.Normal;
        else
          this.copyBackupDataThread.Priority = ThreadPriority.Lowest;
				this.copyBackupDataThread.Name = "CopyData";
				this.copyBackupDataThread.Start();
			}
		}

		/// <summary>
		/// must be overridden to change the Ctrl + Shift + S  key behaviour.
		/// Copy Source files
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlShiftS()
		{
			Accessibility acc = Accessibility.getAccessibility();
			if (!acc.isLevelAtLeastLow())
				return;

			if (((this.copyBackupSourceThread == null) || (!this.copyBackupSourceThread.IsAlive)))
			{
        //agrassi_FI19-10-2009_2
        //this.copyBackupSourceThread = new Thread(new ParameterizedThreadStart(this.CopySource));
        this.copyBackupSourceThread = new Thread(new ParameterizedThreadStart(this.CopySource));
        //RFAZZI_FI28/10/2010_1. Inserita gestione per velocizzare la creazione degli zip
        if (RTCManager.getRTCManager().IsIdleShift)
          this.copyBackupSourceThread.Priority = ThreadPriority.Normal;
        else
          this.copyBackupSourceThread.Priority = ThreadPriority.Lowest;
        this.copyBackupSourceThread.Name = "CopySource";
        //this.copyBackupSourceThread.Start();
        this.copyBackupSourceThread.Start(true);
      }
		}

    //agrassi_FI19-10-2009_2
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + Alt + S  key behaviour.
    /// Copy Source files
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltS()
    {
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
        return;

      if (((this.copyBackupSourceThread == null) || (!this.copyBackupSourceThread.IsAlive)))
      {
        this.copyBackupSourceThread = new Thread(new ParameterizedThreadStart(this.CopySource));
        //RFAZZI_FI28/10/2010_1. Inserita gestione per velocizzare la creazione degli zip
        if (RTCManager.getRTCManager().IsIdleShift)
          this.copyBackupSourceThread.Priority = ThreadPriority.Normal;
        else
          this.copyBackupSourceThread.Priority = ThreadPriority.Lowest;
        this.copyBackupSourceThread.Name = "CopySource";
        this.copyBackupSourceThread.Start(false);
      }
    }

    //agrassi_FI05-12-08_1
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + Z  key behaviour.
    /// Copy Source files only without exe
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftZ()
    {
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
        return;

      if (((this.copyBackupSourceOnlyThread == null) || (!this.copyBackupSourceOnlyThread.IsAlive)))
      {
        //agrassi_FI19-10-2009_2
        //this.copyBackupSourceOnlyThread = new Thread(new ThreadStart(this.CopySourceWithoutExe));
        this.copyBackupSourceOnlyThread = new Thread(new ParameterizedThreadStart(this.CopySourceWithoutExe));
        //RFAZZI_FI28/10/2010_1. Inserita gestione per velocizzare la creazione degli zip
        if (RTCManager.getRTCManager().IsIdleShift)
          this.copyBackupSourceOnlyThread.Priority = ThreadPriority.Normal;
        else
          this.copyBackupSourceOnlyThread.Priority = ThreadPriority.Lowest;
        this.copyBackupSourceOnlyThread.Name = "CopySourceWithoutExe";
        //this.copyBackupSourceOnlyThread.Start();
        this.copyBackupSourceOnlyThread.Start(true);
      }
    }

    //agrassi_FI19-10-2009_2
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + Alt + Z  key behaviour.
    /// Copy Source files only without exe
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltZ()
    {
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
        return;

      if (((this.copyBackupSourceOnlyThread == null) || (!this.copyBackupSourceOnlyThread.IsAlive)))
      {
        this.copyBackupSourceOnlyThread = new Thread(new ParameterizedThreadStart(this.CopySourceWithoutExe));
        //RFAZZI_FI28/10/2010_1. Inserita gestione per velocizzare la creazione degli zip
        if (RTCManager.getRTCManager().IsIdleShift)
          this.copyBackupSourceOnlyThread.Priority = ThreadPriority.Normal;
        else
          this.copyBackupSourceOnlyThread.Priority = ThreadPriority.Lowest;
        this.copyBackupSourceOnlyThread.Name = "CopySourceWithoutExe";
        this.copyBackupSourceOnlyThread.Start(false);
      }
    }

    //PMASSARO_FI10/02/09 - Try To Restart Failed Task
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + T  key behaviour.
    /// Restore Last Production Data Backup
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftT()
    {
      String strErr = "";
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
      {
        strErr = "You Haven't High Access!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.FORCE_TASKS_RESTART));
    }

    //PMASSARO_FI05/02/09 - Added Command For Resetting Production
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + Q  key behaviour.
    /// Delete All Production Data
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftQ()
    {
      String strErr = "--- REQUEST TO RESET CURRENT PRODUCTION BY ENGINEERING PAGE ---";
      Logger.LogError(this, strErr);

      if (Settings.getSettings().configuration.IsPmac400())
      {
        strErr = "Funtion Not Enabled In Case Of Twin400 Only!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
      {
        strErr = "You Are Not On Server!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      if (!RTCManager.getRTCManager().IsIdleShift)
      {
        strErr = "You Must Be In Idle Shift!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }
      
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
      {
        strErr = "You Haven't High Access!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      Frm_Confirm frm_conf = new Frm_Confirm();
      DialogResult res = frm_conf.ShowDialog(this, "A Copy Of Current Production Data Will Be Made On " + Settings.getSettings().paths.SavedProduction + ". Continue?");
      if (res == DialogResult.Cancel)
      {
        return;
      }
      else
      {
        strErr = "--- Operator Accepted To Reset The Production ---";
        Logger.LogError(this, strErr);

        string serverProductionPath;
		    string startPath;
        
        serverProductionPath = SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Substring(0, SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Length - 1);
        startPath = SyncroC.Utility.Settings.getSettings().paths.Source + "_server\\";
        #if DEBUG
                startPath += "debug\\";
        #else
                startPath += "release\\";
        #endif

        string ProductionDirNameForSave;

        string date = DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Year.ToString("0000");
        date += "_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00");

        ProductionDirNameForSave = "PRD_" + date;

        //Il file se c'è va SEMPRE cancellato, anche se non è abilitato il SQL
        string fileNameDestinazione = Settings.getSettings().paths.ServerProduction + "SQLDump_" + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.Job + ".backup";
        if (System.IO.File.Exists(fileNameDestinazione))
          System.IO.File.Delete(fileNameDestinazione);

        if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        {
          if (Settings.getSettings().configuration.getOnServer())
          {
            SyncroC.DataBase.SqlManager.getSqlManager().backupDataBase(fileNameDestinazione,"");
            //DROP ALL PRODUCTION TABLE
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.auto_man_status;", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()), true);
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.downtimes;", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()), true);
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.pastorders;", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()), true);
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.pastrolls;", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()), true);
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.shiftflutelist;", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()), true);
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.shiftlist;", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()), true);

            DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().getAllDataBaseTable();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                foreach (DataRow dr in dt.Rows)
                {
                  string nomeTabella = (string)dr[0];
                  if (nomeTabella.StartsWith("trace_"))
                  {
                    SyncroC.DataBase.SqlManager.getSqlManager().doCommand(String.Format("DELETE FROM {0}.{1};", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE(), nomeTabella), true);
                  }
                }
              }
            }
          }
        }

        Process process = new System.Diagnostics.Process();
        process.StartInfo.FileName = startPath + "ProductionReset.exe";
        process.StartInfo.Arguments = serverProductionPath + "\\ " + Settings.getSettings().paths.SavedProduction + " " + ProductionDirNameForSave + " FALSE " + Settings.getSettings().paths.Log + " " + startPath + "S_Starter.exe";

        strErr = "Arguments Passed To ProductionReset.exe " + process.StartInfo.Arguments;
        Logger.LogError(this, strErr);

        process.Start();

        this.CloseSyncroCompletely();
      }
    }

    //PMASSARO_FI05/02/09 - Added Command For Resetting Production
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + Alt + Q  key behaviour.
    /// Restore Last Production Data Backup
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltQ()
    {
      String strErr = "--- REQUEST TO RESTORE LAST PRODUCTION SAVED BY ENGINEERING PAGE ---";
      Logger.LogError(this, strErr);

      string ProductionDirNameForRestore = "";

      DirectoryInfo di = new DirectoryInfo(Settings.getSettings().paths.SavedProduction);
      if (di.Exists == false)
      {
        strErr = "No Directories Name Start With 'PRD_' Are Found Into " + Settings.getSettings().paths.SavedProduction + " I Can't Continue  (1st Control)";
        Logger.LogError(this, strErr);

        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, "No Previous Production Datas Are Saved");

        return;
      }

      DirectoryInfo[] directories = di.GetDirectories("PRD_*", SearchOption.AllDirectories);

      Logger.LogError(this, "Searching For Last Production To Restore");
      if (directories.Length == 0)
      {
        strErr = "No Directories Name Start With 'PRD_' Are Found Into " + Settings.getSettings().paths.SavedProduction + " I Can't Continue  (1st Control)";
        Logger.LogError(this, strErr);

        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, "No Previous Production Datas Are Saved");

        return;
      }
      int idxLast = -1;

      for (int idx = 0; idx < directories.Length; idx++)
      {
        if (directories[idx].Name.StartsWith("PRD_"))
        {
          if (idxLast >= 0)
          {
            if (directories[idx].CreationTime.CompareTo(directories[idxLast].CreationTime) > 0)
            {
              idxLast = idx;
            }
          }
          else
          {
            idxLast = 0;
          }
        }
      }

      if ( idxLast < 0)
      {
        strErr = "No Directories Name Start With 'PRD_' Are Found Into " + Settings.getSettings().paths.SavedProduction + " I Can't Continue (2nd Control)";
        Logger.LogError(this, strErr);

        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, "No Previous Production Datas Are Saved");
        
        return;
      }
      else
      {
        ProductionDirNameForRestore = directories[idxLast].Name;
        strErr = "Last Production Data Saved Has Name " + ProductionDirNameForRestore;
      }

      if (Settings.getSettings().configuration.IsPmac400())
      {
        strErr = "Funtion Not Enabled In Case Of Twin400 Only!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
      {
        strErr = "You Are Not On Server!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      if (!RTCManager.getRTCManager().IsIdleShift)
      {
        strErr = "You Must Be In Idle Shift!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
      {
        strErr = "You Haven't High Access!";
        Frm_Alert frm_alert = new Frm_Alert();
        frm_alert.ShowDialog(this, strErr);
        return;
      }

      Frm_Confirm frm_conf = new Frm_Confirm();
      DialogResult res = frm_conf.ShowDialog(this, "Last Backup: " + ProductionDirNameForRestore + ". Are You Sure You Want Continue?");
      if (res == DialogResult.Cancel)
      {
        return;
      }
      else
      {
        Logger.LogError(this, "--- Requested To Restore Last Production Data Backup (" + ProductionDirNameForRestore + ") ---");

        string serverProductionPath;
        string startPath;

        serverProductionPath = SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Substring(0, SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Length - 1);
        startPath = SyncroC.Utility.Settings.getSettings().paths.Source + "_server\\";
        #if DEBUG
                startPath += "debug\\";
        #else
                startPath += "release\\";
        #endif

        Process process = new System.Diagnostics.Process();
        process.StartInfo.FileName = startPath + "ProductionReset.exe";
        process.StartInfo.Arguments = serverProductionPath + "\\ " + Settings.getSettings().paths.SavedProduction + " " + ProductionDirNameForRestore + " TRUE " + Settings.getSettings().paths.Log + " " + startPath + "S_Starter.exe";
        
        strErr = "Arguments Passed To ProductionReset.exe " + process.StartInfo.Arguments;
        Logger.LogError(this, strErr);        

        process.Start();

        this.CloseSyncroCompletely();
      }
    }

    //agrassi_FI05-12-08_1
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + E  key behaviour.
    /// Copy Exe files only
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftE()
    {
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
        return;

      if (((this.copyBackupExeOnlyThread == null) || (!this.copyBackupExeOnlyThread.IsAlive)))
      {
        //agrassi_FI19-10-2009_2
        //this.copyBackupExeOnlyThread = new Thread(new ThreadStart(this.CopyExe));
        this.copyBackupExeOnlyThread = new Thread(new ParameterizedThreadStart(this.CopyExe));
        this.copyBackupExeOnlyThread.Priority = ThreadPriority.Lowest;
        this.copyBackupExeOnlyThread.Name = "CopyExeOnly";
        //this.copyBackupExeOnlyThread.Start();
        this.copyBackupExeOnlyThread.Start(true);
      }
    }

    //agrassi_FI19-10-2009_2
    /// <summary>
    /// must be overridden to change the Ctrl + Shift + Alt + E  key behaviour.
    /// Copy Exe files only
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltE()
    {
      Accessibility acc = Accessibility.getAccessibility();
      if (!acc.isLevelAtLeastLow())
        return;

      if (((this.copyBackupExeOnlyThread == null) || (!this.copyBackupExeOnlyThread.IsAlive)))
      {
        this.copyBackupExeOnlyThread = new Thread(new ParameterizedThreadStart(this.CopyExe));
        this.copyBackupExeOnlyThread.Priority = ThreadPriority.Lowest;
        this.copyBackupExeOnlyThread.Name = "CopyExeOnly";
        this.copyBackupExeOnlyThread.Start(false);
      }
    }

    //agrassi_FI03-07-08
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltP()
    {
      Accessibility acc = Accessibility.getAccessibility();
      if (acc.isLevelHigh())
        acc.resetAccessibilityLevel();
      else
        acc.setAccessibilityLevel("fosbersoft");
    }
    //MSTAGETTI_FI-11-05-2010_1
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltT()
    {
    
    }


		/// <summary>
		/// must be overridden to change the Ctrl + Shift + O  key behaviour.
		/// Copy Source files
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlShiftO()
		{
			Accessibility acc = Accessibility.getAccessibility();
			if (!acc.isLevelAtLeastLow())
				return;

			if (((this.createDocThread == null) || (!this.createDocThread.IsAlive)))
			{
				this.createDocThread = new Thread(new ThreadStart(this.CreateDoc));
				this.createDocThread.Priority = ThreadPriority.Lowest;
				this.createDocThread.Name = "CreateThread";
				this.createDocThread.Start();
			}
		}

    private void CopyDirectory(string sourceDir, string destDir)
    {
      if (Directory.Exists(destDir))
        Directory.Delete(destDir, true);
      Directory.CreateDirectory(destDir);
      string[] files = Directory.GetFiles(sourceDir);
      for (int f = 0; f < files.Length; f++)
      {
        string destFile = destDir + files[f].Substring(files[f].LastIndexOf('\\'));
        System.IO.File.Copy(files[f], destFile);
      }
      string[] subDirs = Directory.GetDirectories(sourceDir);
      for (int s = 0; s < subDirs.Length; s++)
      {
        string destSubDir = destDir + subDirs[s].Substring(subDirs[s].LastIndexOf('\\'));
        CopyDirectory(subDirs[s], destSubDir);
      }
    }


		/// <summary>
		/// must be overridden to change the Ctrl + Alt + Shift + B  key behaviour.
		/// Backup production file for quick simulation
		/// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    protected virtual void ControlShiftAltB()
    {
      SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      PageManager.getPageManager().Invoke(SetMsg, new object[] { "Simulation with office comm ...", false, true });
      string serverProductionPath = Settings.getSettings().paths.ServerProduction;
      serverProductionPath = serverProductionPath.Substring(0, serverProductionPath.Length - 1);
      string backupPath = serverProductionPath + Command.BACKUP_EXTENSION;
      if (Directory.Exists(backupPath))
        Directory.Delete(backupPath, true);
      this.CopyDirectory(serverProductionPath, backupPath);
      PageManager.getPageManager().Invoke(SetMsg, new object[] { "Simulation ready.", false, false });
    }

    private delegate string SetMsgDelegate(string msg, bool error, bool still);

		private string GetFileExtension(string fileName)
		{
			int lastPointInd = fileName.LastIndexOf('.');
			if (lastPointInd < 1) return "";
			return fileName.Substring(lastPointInd + 1);
		}

		private void CopyLog()
		{
			Settings settings = Settings.getSettings();
      SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      //PageManager.getPageManager().Invoke(SetMsg, new object[] { "Copying log files ...", false, true });
      string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
			date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
			string serverConfigPath = Settings.getSettings().paths.ServerConfig;
			string serverProductionPath = Settings.getSettings().paths.ServerProduction;
			string sharedPath = Settings.getSettings().paths.ServerSharedOnServer;
			string logPath = Settings.getSettings().paths.Log;
			string name = (settings.configuration.IsPmac400() ? "TW4" : "SYC");
			string savedLogPath = Settings.getSettings().paths.LogZip;
			string savedLogZip, logCompleteName;
			if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job.Length == 0)
        logCompleteName = "Log" + name + "_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroVersion() + " (" + date + "a.zip";
			else
				//RFAZZI_FI04/02/08
				//savedLogZip = savedLogPath + "Log" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + "a (" + date + ").zip";
        //agrassi_FI19-08-2010_1
        //logCompleteName = "Log" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + PageManager.getPageManager().getSyncroSoftwareStandard() + " (" + date + ").zip";
        logCompleteName = "Log" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + " (" + date + ").zip";

      savedLogZip = savedLogPath + logCompleteName;

      PageManager.getPageManager().Invoke(SetMsg, new object[] { savedLogZip + "...", false, true });
      //agrassi_FI03-11-08
      CopyZipDialogDelegate CopyZipDialog = new CopyZipDialogDelegate(this.CopyZipDialog);
      bool copyToUSBDrive = false, copyError = false;
      int drv;
      DriveInfo[] di=DriveInfo.GetDrives();
      for (drv=0; drv<di.Length; drv++)
      {
        if ((di[drv].IsReady) &&
            ((di[drv].DriveType==DriveType.Removable) || ((di[drv].DriveFormat!="NTFS") && 
                (di[drv].RootDirectory.FullName.ToUpper()!="C:\\") && (di[drv].DriveType!=DriveType.CDRom))))
        {
          DialogResult res = (DialogResult)this.Invoke(CopyZipDialog, new object[] {this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_192") + 
              "\r\n" + di[drv].RootDirectory.FullName + "Syncro\\"+logCompleteName, true});
          if (res==DialogResult.OK)
            copyToUSBDrive=true;
          break;
        }
      }

			ArrayList filePaths = new ArrayList();
			string[] logFiles = null;
			logFiles = Directory.GetFiles(logPath);
      //ABELLO_FI26/05/2008_1 Aggiunto il Task ABEventLog e tutta la sua gestione 
      Array.Sort(logFiles);
			for (int i = 0; i < logFiles.Length; i++)
				filePaths.Add(logFiles[i]);
			if (settings.configuration.getOnServer())
			{
        //agrassi_FI08-01-09_1
        //filePaths.Add(sharedPath + "Config.txt");
        //filePaths.Add(sharedPath + "CalibrationUserLog1.txt");
        //filePaths.Add(sharedPath + "CalibrationUserLog2.txt");
        //filePaths.Add(sharedPath + "CalibrationUserLog3.txt");
        //filePaths.Add(sharedPath + "CalibrationUserLog1.txt.old");
        //filePaths.Add(sharedPath + "CalibrationUserLog2.txt.old");
        //filePaths.Add(sharedPath + "CalibrationUserLog3.txt.old");
        //filePaths.Add(serverConfigPath + "LineConfig.ini");
        //filePaths.Add(serverConfigPath + "Syncro.ini");
        //filePaths.Add(serverProductionPath + "Orders.bin");
        //filePaths.Add(serverProductionPath + "List.bin");
        //filePaths.Add(serverProductionPath + "PastOrders.bin");
        //filePaths.Add(serverProductionPath + "Shift.bin");
        try
        {
          filePaths.Add(sharedPath + "Config.txt");
          filePaths.Add(sharedPath + "CalibrationUserLog1.txt");
          filePaths.Add(sharedPath + "CalibrationUserLog2.txt");
          filePaths.Add(sharedPath + "CalibrationUserLog3.txt");
          //ABELLO_FI25/08/2010_1. I log del Process Control non venivano salvati nello zip
          filePaths.Add(sharedPath + "PrcUserLog.txt");
          filePaths.Add(sharedPath + "CalibrationUserLog1.txt.old");
          filePaths.Add(sharedPath + "CalibrationUserLog2.txt.old");
          filePaths.Add(sharedPath + "CalibrationUserLog3.txt.old");
          filePaths.Add(sharedPath + "PrcUserLog.txt.old");
          filePaths.Add(serverConfigPath + "LineConfig.ini");
          filePaths.Add(serverConfigPath + "Syncro.ini");
          filePaths.Add(serverProductionPath + "Orders.bin");
          filePaths.Add(serverProductionPath + "List.bin");
          filePaths.Add(serverProductionPath + "PastOrders.bin");
          filePaths.Add(serverProductionPath + "Shift.bin");
          filePaths.Add(serverConfigPath + PageManager.NET_CONFIG_FILE);
          filePaths.Add(serverConfigPath + PageManager.NET_CONFIG_FILE_OLD);
        }
        catch (Exception e)
        {
          Logger.LogError(this, "CopyLog", e);
        }
      }
			try
			{
				if (!Directory.Exists(savedLogPath))
					Directory.CreateDirectory(savedLogPath);

        //ABELLO_FI26/05/2008_1 Aggiunto il Task ABEventLog e tutta la sua gestione 
        if (settings.configuration.IsPmac400())
          System.Threading.Thread.Sleep(5000);
        else if (settings.configuration.IsDryEndControlOnly() || settings.configuration.IsWetEndControlOnly())
          System.Threading.Thread.Sleep(2000);

				this.CreateZipFile(savedLogZip, filePaths);
				this.CheckZips(savedLogPath, "Log");
        //agrassi_FI03-11-08
       if (copyToUSBDrive)
        {
          FileInfo fi=new FileInfo(savedLogZip);
          if ((di[drv].AvailableFreeSpace > fi.Length) && di[drv].IsReady)
          {
            if (!Directory.Exists(di[drv].RootDirectory.FullName + "Syncro"))
              Directory.CreateDirectory(di[drv].RootDirectory.FullName + "Syncro");
            System.IO.File.Copy(savedLogZip, di[drv].RootDirectory.FullName + "Syncro\\" + logCompleteName);
          }
          else
          {
            PageManager.getPageManager().Invoke(SetMsg, new object[] { DBManager.getDBManager().message.getTranslatedMessage("§SYCC_193"), true, false });
            copyError = true;
          }
          this.Invoke(CopyZipDialog, new object[] { this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_194"), false});
        }

        //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
        if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1"))
        {
          try
          {
            if (!Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedLogPath.Substring(3, savedLogPath.Length - 3)))
              Directory.CreateDirectory("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedLogPath.Substring(3, savedLogPath.Length - 3));
            savedLogPath = "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedLogPath.Substring(3, savedLogPath.Length - 3);
            System.IO.File.Copy(savedLogZip, savedLogPath + logCompleteName);
          }
          catch (Exception e)
          {
            Console.WriteLine("The process failed: {0}", e.ToString());
          }
        }

      }
			catch (Exception e)
			{
				Logger.LogError(this, e);
				if (!settings.configuration.getOnServer())
				{
					string logDir = savedLogZip.Substring(1, savedLogZip.LastIndexOf('.'));
					Directory.CreateDirectory(logDir);
					for (int i = 0; i < logFiles.Length; i++)
						System.IO.File.Copy(logFiles[i], savedLogPath + logFiles[i].Substring(logFiles[i].LastIndexOf('\\') + 1));
				}
			}
      //agrassi_FI03-11-08
      if (!copyError)
			  PageManager.getPageManager().Invoke(SetMsg, new object[] { "Log zip creation completed.", false, false });
		}

    //agrassi_FI03-11-08
    public delegate DialogResult CopyZipDialogDelegate(IWin32Window owner, string message, bool confirm);
    public DialogResult CopyZipDialog(IWin32Window owner, string message, bool confirm)
    {
      if (confirm)
      {
        Frm_Confirm frm = new Frm_Confirm();
        return frm.ShowDialog(owner, message);
      }
      else
      {
        Frm_Alert frm = new Frm_Alert();
        return frm.ShowDialog(owner, message);
      }
    }

    //agrassi_FI19-10-2009_2
    //private void CopySource()
    private void CopySource(object includeManuals)
    {
      //PMASSARO_FI19/02/10 - Durante lo zip del codice non venivano salvati i files .hxx e .cxx
      /*
      //PMASSARO_FI30/11/09 - Durante lo zip del codice non veniva salvato il font free3of9.ttf
      //
			//string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "dll", "doc", "rtf", "exe", "gif", "h", "html", "ico", "ini", "ite", "jpg", "lib", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xsd", "png", "rom" };
      //
      string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "dll", "doc", "rtf", "exe", "gif", "h", "html", "ico", "ini", "ite", "jpg", "lib", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xsd", "png", "rom", "ttf" };
      */
      //agrassi_FI11-04-2011_2
      //string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "dll", "doc", "rtf", "exe", "gif", "h", "html", "ico", "ini", "ite", "jpg", "lib", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xsd", "png", "rom", "ttf", "cxx", "hxx" };
      string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "dll", "doc", "rtf", "exe", "gif", "h", "html", "ico", "ini", "ite", "jpg", "lib", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xaml", "xsd", "png", "rom", "ttf", "cxx", "hxx" };

			ArrayList allowedExtensions = new ArrayList(allowedExtensionsArray);
      //agrassi_FI25-09-08_1
      //string[] forbidenDirectories = new string[] { "obj" };
      string[] forbidenDirectories = null;
      if ((bool)includeManuals)
        forbidenDirectories = new string[] { "obj" };
      else
        forbidenDirectories = new string[] { "obj", "Manuals" };
      Settings settings = Settings.getSettings();
			SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      //PageManager.getPageManager().Invoke(SetMsg, new object[] { "Copying Source files ...", false, true });
      string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
			date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
			string serverConfigPath = Settings.getSettings().paths.ServerConfig;
			string serverProductionPath = Settings.getSettings().paths.ServerProduction;
			string sourcePath = Settings.getSettings().paths.Source;
			string name = (settings.configuration.IsPmac400() ? "TW4" : "SYC");
			string savedSourcePath = Settings.getSettings().paths.SourceZip;
			string savedSourceZip, sourceCompleteName;

			if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job.Length == 0)
        sourceCompleteName = "COD" + name + "_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroVersion() + "a.zip";
			else
				//RFAZZI_FI04/02/08
				//savedSourceZip = savedSourcePath + "COD" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + "a (" + date + ").zip";
        //agrassi_FI19-08-2010_1
        //sourceCompleteName = "COD" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + PageManager.getPageManager().getSyncroSoftwareStandard() + " (" + date + ").zip";
        sourceCompleteName = "COD" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + " (" + date + ").zip";

      savedSourceZip = savedSourcePath + sourceCompleteName;

      PageManager.getPageManager().Invoke(SetMsg, new object[] { savedSourceZip + " ...", false, true });
      //agrassi_FI03-11-08
      CopyZipDialogDelegate CopyZipDialog = new CopyZipDialogDelegate(this.CopyZipDialog);
      bool copyToUSBDrive = false, copyError = false;
      int drv;
      DriveInfo[] di = DriveInfo.GetDrives();
      for (drv = 0; drv < di.Length; drv++)
      {
        if ((di[drv].IsReady) &&
            ((di[drv].DriveType == DriveType.Removable) || ((di[drv].DriveFormat != "NTFS") &&
                (di[drv].RootDirectory.FullName.ToUpper() != "C:\\") && (di[drv].DriveType != DriveType.CDRom))))
        {
          DialogResult res = (DialogResult)this.Invoke(CopyZipDialog, new object[] {this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_192") + 
              "\r\n" + di[drv].RootDirectory.FullName + "Syncro\\"+sourceCompleteName, true});
          if (res == DialogResult.OK)
            copyToUSBDrive = true;
          break;
        }
      }
      
      ArrayList filePaths = new ArrayList();
			string[] sourceFiles = null;
			sourceFiles = Directory.GetFiles(sourcePath, "*.*", System.IO.SearchOption.AllDirectories);
      for (int i = 0; i < sourceFiles.Length; i++)
      {
        if (allowedExtensions.Contains(this.GetFileExtension(sourceFiles[i]).ToLower()))
        {
          //agrassi_FI25-09-08_1
          bool forbidenDir = false;
          for (int j = 0; j < forbidenDirectories.Length; j++)
          {
            if (sourceFiles[i].Contains("\\" + forbidenDirectories[j] + "\\"))
            {
              forbidenDir = true;
              break;
            }
          }
          if (!forbidenDir)
            filePaths.Add(sourceFiles[i]);
        }
      }
			try
			{
				if (!Directory.Exists(savedSourcePath))
					Directory.CreateDirectory(savedSourcePath);

				this.CreateZipFile(savedSourceZip, filePaths);
				this.CheckZips(savedSourcePath, "COD");
        //agrassi_FI03-11-08
        if (copyToUSBDrive)
        {
          FileInfo fi = new FileInfo(savedSourceZip);
          if ((di[drv].AvailableFreeSpace > fi.Length) && di[drv].IsReady)
          {
            if (!Directory.Exists(di[drv].RootDirectory.FullName + "Syncro"))
              Directory.CreateDirectory(di[drv].RootDirectory.FullName + "Syncro");
            System.IO.File.Copy(savedSourceZip, di[drv].RootDirectory.FullName + "Syncro\\" + sourceCompleteName);
          }
          else
          {
            PageManager.getPageManager().Invoke(SetMsg, new object[] { DBManager.getDBManager().message.getTranslatedMessage("§SYCC_193"), true, false });
            copyError = true;
          }
          this.Invoke(CopyZipDialog, new object[] { this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_194"), false });
        }

        //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
        if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1"))
        {
          try
          {
            if (!Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3)))
              Directory.CreateDirectory("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3));
            savedSourcePath = "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3);
            System.IO.File.Copy(savedSourceZip, savedSourcePath + sourceCompleteName);
          }
          catch (Exception e)
          {
            Console.WriteLine("The process failed: {0}", e.ToString());
          }
        }

      }
			catch (Exception e)
			{
				Logger.LogError(this, e);
				if (!settings.configuration.getOnServer())
				{
					string sourceDir = savedSourceZip.Substring(1, savedSourceZip.LastIndexOf('.'));
					Directory.CreateDirectory(sourceDir);
					for (int i = 0; i < sourceFiles.Length; i++)
						System.IO.File.Copy(sourceFiles[i], savedSourcePath + sourceFiles[i].Substring(sourceFiles[i].LastIndexOf('\\') + 1));
				}
			}
      //agrassi_FI03-11-08
      if (!copyError)
        PageManager.getPageManager().Invoke(SetMsg, new object[] { "Source+exe zip creation completed.", false, false });
		}

    //agrassi_05-12-08_1
    //agrassi_FI19-10-2009_2
    //private void CopySourceWithoutExe()
    private void CopySourceWithoutExe(object includeManuals)
    {
      //PMASSARO_FI19/02/10 - Durante lo zip del codice non venivano salvati i files .hxx e .cxx
      /*
      //PMASSARO_FI30/11/09 - Durante lo zip del codice non veniva salvato il font free3of9.ttf
      //
      //string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "doc", "rtf", "gif", "h", "html", "ico", "ini", "ite", "jpg", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xsd", "png", "rom" };
      //
      string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "doc", "rtf", "gif", "h", "html", "ico", "ini", "ite", "jpg", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xsd", "png", "rom", "ttf" };
      */
      //agrassi_FI11-04-2011_2
      //string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "doc", "rtf", "gif", "h", "html", "ico", "ini", "ite", "jpg", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xsd", "png", "rom", "ttf", "cxx", "hxx" };
      string[] allowedExtensionsArray = new string[] { "", "bat", "bmp", "c", "config", "cpp", "cs", "csproj", "doc", "rtf", "gif", "h", "html", "ico", "ini", "ite", "jpg", "mdb", "pdf", "pfx", "rc", "resx", "sln", "tlh", "tli", "txt", "vcproj", "xml", "xaml", "xsd", "png", "rom", "ttf", "cxx", "hxx" };

      ArrayList allowedExtensions = new ArrayList(allowedExtensionsArray);
      //agrassi_FI19-10-2009_2
      //string[] forbidenDirectories = new string[] { "obj", "_Client", "_Server" };
      string[] forbidenDirectories = null;
      if ((bool)includeManuals)
        forbidenDirectories = new string[] { "obj", "_Client", "_Server" };
      else
        forbidenDirectories = new string[] { "obj", "_Client", "_Server", "Manuals" };
      Settings settings = Settings.getSettings();
      SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
      date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
      string serverConfigPath = Settings.getSettings().paths.ServerConfig;
      string serverProductionPath = Settings.getSettings().paths.ServerProduction;
      string sourcePath = Settings.getSettings().paths.Source;
      string name = (settings.configuration.IsPmac400() ? "TW4" : "SYC");
      string savedSourcePath = Settings.getSettings().paths.SourceZip;
      string savedSourceZip, sourceCompleteName;

      if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job.Length == 0)
        sourceCompleteName = "SRC" + name + "_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroVersion() + "a.zip";
      else
        //agrassi_FI19-08-2010_1
        //sourceCompleteName = "SRC" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + PageManager.getPageManager().getSyncroSoftwareStandard() + " (" + date + ").zip";
        sourceCompleteName = "SRC" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + " (" + date + ").zip";
      savedSourceZip = savedSourcePath + sourceCompleteName;
      PageManager.getPageManager().Invoke(SetMsg, new object[] { savedSourceZip + "...", false, true });
      CopyZipDialogDelegate CopyZipDialog = new CopyZipDialogDelegate(this.CopyZipDialog);
      bool copyToUSBDrive = false, copyError = false;
      int drv;
      DriveInfo[] di = DriveInfo.GetDrives();
      for (drv = 0; drv < di.Length; drv++)
      {
        if ((di[drv].IsReady) &&
            ((di[drv].DriveType == DriveType.Removable) || ((di[drv].DriveFormat != "NTFS") &&
                (di[drv].RootDirectory.FullName.ToUpper() != "C:\\") && (di[drv].DriveType != DriveType.CDRom))))
        {
          DialogResult res = (DialogResult)this.Invoke(CopyZipDialog, new object[] {this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_192") + 
              "\r\n" + di[drv].RootDirectory.FullName + "Syncro\\"+sourceCompleteName, true});
          if (res == DialogResult.OK)
            copyToUSBDrive = true;
          break;
        }
      }

      ArrayList filePaths = new ArrayList();
      string[] sourceFiles = null;
      sourceFiles = Directory.GetFiles(sourcePath, "*.*", System.IO.SearchOption.AllDirectories);
      for (int i = 0; i < sourceFiles.Length; i++)
      {
        if (allowedExtensions.Contains(this.GetFileExtension(sourceFiles[i]).ToLower()))
        {
          bool forbidenDir = false;
          for (int j = 0; j < forbidenDirectories.Length; j++)
          {
            if (sourceFiles[i].Contains("\\" + forbidenDirectories[j] + "\\"))
            {
              forbidenDir = true;
              break;
            }
          }
          if (!forbidenDir)
            filePaths.Add(sourceFiles[i]);
        }
      }
      try
      {
        if (!Directory.Exists(savedSourcePath))
          Directory.CreateDirectory(savedSourcePath);

        this.CreateZipFile(savedSourceZip, filePaths);
        this.CheckZips(savedSourcePath, "SRC");
        if (copyToUSBDrive)
        {
          FileInfo fi = new FileInfo(savedSourceZip);
          if ((di[drv].AvailableFreeSpace > fi.Length) && di[drv].IsReady)
          {
            if (!Directory.Exists(di[drv].RootDirectory.FullName + "Syncro"))
              Directory.CreateDirectory(di[drv].RootDirectory.FullName + "Syncro");
            System.IO.File.Copy(savedSourceZip, di[drv].RootDirectory.FullName + "Syncro\\" + sourceCompleteName);
          }
          else
          {
            PageManager.getPageManager().Invoke(SetMsg, new object[] { DBManager.getDBManager().message.getTranslatedMessage("§SYCC_193"), true, false });
            copyError = true;
          }
          this.Invoke(CopyZipDialog, new object[] { this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_194"), false });
        }

        //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
        if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1"))
        {
          try
          {
            if (!Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3)))
              Directory.CreateDirectory("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3));
            savedSourcePath = "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3);
            System.IO.File.Copy(savedSourceZip, savedSourcePath + sourceCompleteName);
          }
          catch (Exception e)
          {
            Console.WriteLine("The process failed: {0}", e.ToString());
          }
        }

      }
      catch (Exception e)
      {
        Logger.LogError(this, e);
        if (!settings.configuration.getOnServer())
        {
          string sourceDir = savedSourceZip.Substring(1, savedSourceZip.LastIndexOf('.'));
          Directory.CreateDirectory(sourceDir);
          for (int i = 0; i < sourceFiles.Length; i++)
            System.IO.File.Copy(sourceFiles[i], savedSourcePath + sourceFiles[i].Substring(sourceFiles[i].LastIndexOf('\\') + 1));
        }
      }
      if (!copyError)
        PageManager.getPageManager().Invoke(SetMsg, new object[] { "Source zip creation completed.", false, false });
    }

    //agrassi_05-12-08_1
    //agrassi_FI19-10-2009_2
    //private void CopyExe()
    private void CopyExe(object includeManuals)
    {
      //PMASSARO_FI30/11/09 - Durante lo zip del codice non veniva salvato il font free3of9.ttf
      /*
      string[] allowedExtensionsArray = new string[] { "exe", "dll", "bat", "bmp", "doc", "rtf", "gif", "ini", "ite", "jpg", "pdf", "txt", "xml", "xsd", "png", "rom" };
      */
      string[] allowedExtensionsArray = new string[] { "exe", "dll", "bat", "bmp", "doc", "rtf", "gif", "ini", "ite", "jpg", "pdf", "txt", "xml", "xsd", "png", "rom", "ttf" };

      ArrayList allowedExtensions = new ArrayList(allowedExtensionsArray);
      //agrassi_FI19-10-2009_2
      string[] forbidenDirectories = null;
      if ((bool)includeManuals)
        forbidenDirectories = new string[] { };
      else
        forbidenDirectories = new string[] { "Manuals" };
      Settings settings = Settings.getSettings();
      SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
      date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
      string serverConfigPath = Settings.getSettings().paths.ServerConfig;
      string serverProductionPath = Settings.getSettings().paths.ServerProduction;
      string sourcePath = Settings.getSettings().paths.Source;
      string name = (settings.configuration.IsPmac400() ? "TW4" : "SYC");
      string savedSourcePath = Settings.getSettings().paths.SourceZip;
      string savedSourceZip, sourceCompleteName;

      if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job.Length == 0)
        sourceCompleteName = "EXE" + name + "_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroVersion() + "a.zip";
      else
        //agrassi_FI19-08-2010_1
        //sourceCompleteName = "EXE" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + PageManager.getPageManager().getSyncroSoftwareStandard() + " (" + date + ").zip";
        sourceCompleteName = "EXE" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroVersion() + " (" + date + ").zip";

			savedSourceZip = savedSourcePath + sourceCompleteName;

      PageManager.getPageManager().Invoke(SetMsg, new object[] { savedSourceZip + "...", false, true });
      CopyZipDialogDelegate CopyZipDialog = new CopyZipDialogDelegate(this.CopyZipDialog);
      bool copyToUSBDrive = false, copyError = false;
      int drv;
      DriveInfo[] di = DriveInfo.GetDrives();
      for (drv = 0; drv < di.Length; drv++)
      {
        if ((di[drv].IsReady) &&
            ((di[drv].DriveType == DriveType.Removable) || ((di[drv].DriveFormat != "NTFS") &&
                (di[drv].RootDirectory.FullName.ToUpper() != "C:\\") && (di[drv].DriveType != DriveType.CDRom))))
        {
          DialogResult res = (DialogResult)this.Invoke(CopyZipDialog, new object[] {this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_192") + 
              "\r\n" + di[drv].RootDirectory.FullName + "Syncro\\"+sourceCompleteName, true});
          if (res == DialogResult.OK)
            copyToUSBDrive = true;
          break;
        }
      }

      ArrayList filePaths = new ArrayList();
      string[] sourceFiles = null;
      sourceFiles = Directory.GetFiles(sourcePath, "*.*", System.IO.SearchOption.AllDirectories);
      for (int i = 0; i < sourceFiles.Length; i++)
      {
        if (allowedExtensions.Contains(this.GetFileExtension(sourceFiles[i]).ToLower()))
        {
          if (sourceFiles[i].StartsWith(sourcePath + "_"))
          {
            bool forbidenDir = false;
            for (int j = 0; j < forbidenDirectories.Length; j++)
            {
              if (sourceFiles[i].Contains("\\" + forbidenDirectories[j] + "\\"))
              {
                forbidenDir = true;
                break;
              }
            }
            if (!forbidenDir)
              filePaths.Add(sourceFiles[i]);
          }
        }
      }
      try
      {
        if (!Directory.Exists(savedSourcePath))
          Directory.CreateDirectory(savedSourcePath);

        this.CreateZipFile(savedSourceZip, filePaths);
        this.CheckZips(savedSourcePath, "EXE");
        if (copyToUSBDrive)
        {
          FileInfo fi = new FileInfo(savedSourceZip);
          if ((di[drv].AvailableFreeSpace > fi.Length) && di[drv].IsReady)
          {
            if (!Directory.Exists(di[drv].RootDirectory.FullName + "Syncro"))
              Directory.CreateDirectory(di[drv].RootDirectory.FullName + "Syncro");
            System.IO.File.Copy(savedSourceZip, di[drv].RootDirectory.FullName + "Syncro\\" + sourceCompleteName);
          }
          else
          {
            PageManager.getPageManager().Invoke(SetMsg, new object[] { DBManager.getDBManager().message.getTranslatedMessage("§SYCC_193"), true, false });
            copyError = true;
          }
          this.Invoke(CopyZipDialog, new object[] { this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_194"), false });
        }

        //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
        if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1"))
        {
          try
          {
            if (!Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3)))
              Directory.CreateDirectory("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3));
            savedSourcePath = "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedSourcePath.Substring(3, savedSourcePath.Length - 3);
            System.IO.File.Copy(savedSourceZip, savedSourcePath + sourceCompleteName);
          }
          catch (Exception e)
          {
            Console.WriteLine("The process failed: {0}", e.ToString());
          }
        }

      }
      catch (Exception e)
      {
        Logger.LogError(this, e);
        if (!settings.configuration.getOnServer())
        {
          string sourceDir = savedSourceZip.Substring(1, savedSourceZip.LastIndexOf('.'));
          Directory.CreateDirectory(sourceDir);
          for (int i = 0; i < sourceFiles.Length; i++)
            System.IO.File.Copy(sourceFiles[i], savedSourcePath + sourceFiles[i].Substring(sourceFiles[i].LastIndexOf('\\') + 1));
        }
      }
      if (!copyError)
        PageManager.getPageManager().Invoke(SetMsg, new object[] { "Exe zip creation completed.", false, false });
    }
    
    private void CopyData()
		{
      //PMASSARO_FI30/11/09 - Durante lo zip del codice non veniva salvato il font free3of9.ttf
      /*
      //agrassi_FI05-12-08
      string[] allowedExtensionsArray = new string[] { "bmp", "ini", "ite", "jpg", "txt", "xml", "xsd", "png", "bin", "mmf", "gif", "backup" };
      */
      //agrassi_FI13-04-2010_2
      //string[] allowedExtensionsArray = new string[] { "bmp", "ini", "ite", "jpg", "txt", "xml", "xsd", "png", "bin", "mmf", "gif", "backup", "ttf" };
      string[] allowedExtensionsArray = new string[] { "bmp", "ini", "ite", "jpg", "txt", "xml", "xsd", "png", "bin", "mmf", "gif", "backup", "ttf", "cab" };

      ArrayList allowedExtensions = new ArrayList(allowedExtensionsArray);
      string[] forbidenDirectories = new string[] { "Manuals" };
      Settings settings = Settings.getSettings();
			SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
      //PageManager.getPageManager().Invoke(SetMsg, new object[] { "Copying Data files ...", false, true });
      string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
			date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
			string serverConfigPath = Settings.getSettings().paths.ServerConfig;
			string serverProductionPath = Settings.getSettings().paths.ServerProduction;
			string dataPath = Settings.getSettings().paths.Data;
			string name = (settings.configuration.IsPmac400() ? "TW4" : "SYC");
			string savedDataPath = Settings.getSettings().paths.DataZip;
			string savedDataZip, dataCompleteName;
			//RFAZZI_FI04/02/08
			if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job.Length == 0)
				//savedDataZip = savedDataPath + "DAT" + name + "_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroDataVersion() + "a.zip";
        dataCompleteName = "DAT" + name + "_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroDataVersion() + ".zip";
			else
				//savedDataZip = savedDataPath + "DAT" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroDataVersion() + "a (" + date + ").zip";
        dataCompleteName = "DAT" + name + "_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroDataVersion() + " (" + date + ").zip";

			savedDataZip = savedDataPath + dataCompleteName;

      PageManager.getPageManager().Invoke(SetMsg, new object[] { savedDataZip + "...", false, true });
      //agrassi_FI03-11-08
      CopyZipDialogDelegate CopyZipDialog = new CopyZipDialogDelegate(this.CopyZipDialog);
      bool copyToUSBDrive = false, copyError = false;
      int drv;
      DriveInfo[] di = DriveInfo.GetDrives();
      for (drv = 0; drv < di.Length; drv++)
      {
        if ((di[drv].IsReady) &&
            ((di[drv].DriveType == DriveType.Removable) || ((di[drv].DriveFormat != "NTFS") &&
                (di[drv].RootDirectory.FullName.ToUpper() != "C:\\") && (di[drv].DriveType != DriveType.CDRom))))
        {
          DialogResult res = (DialogResult)this.Invoke(CopyZipDialog, new object[] {this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_192") + 
              "\r\n" + di[drv].RootDirectory.FullName + "Syncro\\"+dataCompleteName, true});
          if (res == DialogResult.OK)
            copyToUSBDrive = true;
          break;
        }
      }

      //Il file se c'è va SEMPRE cancellato, anche se non è abilitato il mySQL
      string fileNameDestinazione = Settings.getSettings().paths.ServerProduction + "SQLDump_" + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.Job + ".backup";
      if (System.IO.File.Exists(fileNameDestinazione))
        System.IO.File.Delete(fileNameDestinazione);

      //agrassi_FI26-10-2010_1
      //if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED)
        if (Settings.getSettings().configuration.getOnServer())
          SyncroC.DataBase.SqlManager.getSqlManager().backupDataBase(fileNameDestinazione,"");

			ArrayList filePaths = new ArrayList();
			string[] dataFiles = null;
			dataFiles = Directory.GetFiles(dataPath, "*.*", System.IO.SearchOption.AllDirectories);
      //agrassi_FI05-12-08
      //for (int i = 0; i < dataFiles.Length; i++)
      //  filePaths.Add(dataFiles[i]);
      for (int i = 0; i < dataFiles.Length; i++)
      {
        if (allowedExtensions.Contains(this.GetFileExtension(dataFiles[i]).ToLower()))
        {
          bool forbidenDir = false;
          for (int j = 0; j < forbidenDirectories.Length; j++)
          {
            if (dataFiles[i].Contains("\\" + forbidenDirectories[j] + "\\"))
            {
              forbidenDir = true;
              break;
            }
          }
          if (!forbidenDir)
            filePaths.Add(dataFiles[i]);
        }
      }

			try
			{
				if (!Directory.Exists(savedDataPath))
					Directory.CreateDirectory(savedDataPath);

				this.CreateZipFile(savedDataZip, filePaths);
				this.CheckZips(savedDataPath, "DAT");
        //agrassi_FI03-11-08
        if (copyToUSBDrive)
        {
          FileInfo fi = new FileInfo(savedDataZip);
          if ((di[drv].AvailableFreeSpace > fi.Length) && di[drv].IsReady)
          {
            if (!Directory.Exists(di[drv].RootDirectory.FullName + "Syncro"))
              Directory.CreateDirectory(di[drv].RootDirectory.FullName + "Syncro");
            System.IO.File.Copy(savedDataZip, di[drv].RootDirectory.FullName + "Syncro\\" + dataCompleteName);
          }
          else
          {
            PageManager.getPageManager().Invoke(SetMsg, new object[] { DBManager.getDBManager().message.getTranslatedMessage("§SYCC_193"), true, false });
            copyError = true;
          }
          this.Invoke(CopyZipDialog, new object[] { this, DBManager.getDBManager().message.getTranslatedMessage("§SYCC_194"), false });
        }

        //RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
        if (!DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp.Equals("127.0.0.1"))
        {
          try
          {
            if (!Directory.Exists("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedDataPath.Substring(3, savedDataPath.Length - 3)))
              Directory.CreateDirectory("\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedDataPath.Substring(3, savedDataPath.Length - 3));
            savedDataPath = "\\\\" + DBManager.getDBManager().syncroConfig.GeneralSetting.ExternalBackupHDIp + "\\" + savedDataPath.Substring(3, savedDataPath.Length - 3);
            System.IO.File.Copy(savedDataZip, savedDataPath + dataCompleteName);
          }
          catch (Exception e)
          {
            Console.WriteLine("The process failed: {0}", e.ToString());
          }
        }
      }
			catch (Exception e)
			{
				Logger.LogError(this, e);
				if (!settings.configuration.getOnServer())
				{
					string dataDir = savedDataZip.Substring(1, savedDataZip.LastIndexOf('.'));
					Directory.CreateDirectory(dataDir);
					for (int i = 0; i < dataFiles.Length; i++)
						System.IO.File.Copy(dataFiles[i], savedDataPath + dataFiles[i].Substring(dataFiles[i].LastIndexOf('\\') + 1));
				}
			}
      //agrassi_FI03-11-08
      if (!copyError)
        PageManager.getPageManager().Invoke(SetMsg, new object[] { "Data zip creation completed.", false, false });
		}

		private void CreateDoc()
		{
			Settings settings = Settings.getSettings();
			SetMsgDelegate SetMsg = new SetMsgDelegate(PageManager.getPageManager()._SetMessage);
			PageManager.getPageManager().Invoke(SetMsg, new object[] { "Create doc file ...", false, true });
			string date = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
			date += "_" + DateTime.Now.Hour.ToString("00") + "." + DateTime.Now.Minute.ToString("00");
			string docPath = Settings.getSettings().paths.Documentation;
			string savedDocPath;

			if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job.Length == 0)
				savedDocPath = docPath + "SyncroDOC_XXXXyyccc_ver" + PageManager.getPageManager().getSyncroDataVersion() + "a.txt";
			else
				savedDocPath = docPath + "SyncroDOC_" + DBManager.getDBManager().syncroConfig.GeneralSetting.Job + DBManager.getDBManager().syncroConfig.GeneralSetting.Comm + "_ver" + PageManager.getPageManager().getSyncroDataVersion() + "a (" + date + ").txt";
			string s = "";
			DBManager.getDBManager().lineConfig.StartSurveyProperties(ref s, ConfigBase.ConfigBaseOperation.PropertiesToDocumentation);
			s = "*************************************************** SYNCRO2 ******************************************\r\n\r\n" +
				"++++++>> CONFIGURATION PARAMETERS\r\n\r\n" + s;
			try
			{
				if (!Directory.Exists(docPath))
					Directory.CreateDirectory(docPath);
				StreamWriter sw = new StreamWriter(savedDocPath, false);
				sw.Write(s);
				sw.Close();
			}
			catch (Exception e)
			{
				Logger.LogError(this, e);
			}
			PageManager.getPageManager().Invoke(SetMsg, new object[] { "Doc file created.", false, false });
		}

    //agrassi_FI24-09-08
    public void CreateZipFile(string zipName, ArrayList fileNames)
    {
			string zipEntryName = "";
			Settings settings = Settings.getSettings();
			if ((zipName == null) || (zipName.Length == 0) || (fileNames == null))
			{
				Logger.LogError(this, "Cannot create zip file " + zipName);
				return;
			}
			try
			{
				ZipOutputStream zos = new ZipOutputStream(new FileOutputStream(zipName));
				for (int i = 0; i < fileNames.Count; i++)
				{
					if (!System.IO.File.Exists((string)fileNames[i]))
					{
						Logger.LogError(this, "The file " + fileNames[i] + " doesn't exist: impossibile to zip it!");
						continue;
					}
					zipEntryName = (string)fileNames[i];
					if (((string)fileNames[i]).StartsWith(settings.paths.SyncroRoot))
					{
						zipEntryName = ((string)fileNames[i]).Substring(settings.paths.SyncroRoot.Length);
					}
					else
					{
						int indColon = ((string)fileNames[i]).IndexOf(":\\");
						if (indColon > 0)
							zipEntryName = ((string)fileNames[i]).Substring(indColon + 2);
					}
					ZipEntry ze = null; new ZipEntry(zipEntryName);
					FileInputStream fis = null;
					try
					{
						ze = new ZipEntry(zipEntryName);
						ze.setMethod(ZipEntry.DEFLATED);
						zos.putNextEntry(ze);
						fis = new FileInputStream((string)fileNames[i]);
						sbyte[] buff = new sbyte[1024];
						int n = 0;
						while ((n = fis.read(buff, 0, buff.Length)) > 0)
						{
							zos.write(buff, 0, n);
						}
						fis.close();
						zos.closeEntry();
					}
					catch (Exception e)
					{
						Logger.LogError(this, "Error while zipping " + zipEntryName + " into " + zipName + ":", e);
						if (fis != null)
							fis.close();
						zos.closeEntry();
					}
				}
				zos.close();
			}
			catch (Exception e)
			{
				Logger.LogError(this, "Error while creating " + zipName + ":", e);
			}
		}

    public void CheckZips(string path, string pattern)
		{
			const int MAX_NUM_ZIP = 8;
			if (!Directory.Exists(path)) return;
			try
			{
				string[] zipFiles = Directory.GetFiles(path, "Log*.zip");
				DateTime[] fileTimes = new DateTime[zipFiles.Length];
				for (int i = 0; i < zipFiles.Length; i++)
				{
					FileInfo fi = new FileInfo(zipFiles[i]);
					fileTimes[i] = fi.LastWriteTime;
				}
				if (zipFiles.Length > MAX_NUM_ZIP)
				{
					Array.Sort(fileTimes, zipFiles);
					for (int i = 0; i < zipFiles.Length - MAX_NUM_ZIP; i++)
						System.IO.File.Delete(zipFiles[i]);
				}
			}
			catch (Exception e)
			{
				Logger.LogError(this, "Error while checking zips:", e);
			}
		}

		/// <summary>
		/// must be overridden to change the Ctrl + L  key behaviour
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlL()
		{
			Settings settings = Settings.getSettings();
			if (settings.languages.RapidTranslationEnabled)
			{
				//change the currentLanguage			
				settings.CurrentLanguage =
					settings.CurrentLanguage.Equals(settings.languages.FirstLanguage) ?
					settings.languages.SecondLanguage : settings.languages.FirstLanguage;

				//translate all the pages into the cache and the status bar
				PageManager.getPageManager().translateForms(settings.CurrentLanguage);
        //agrassi_FI07-07-08
        this.Cursor = Cursors.WaitCursor;
        RTCManager.getRTCManager().TranslateAlarms();
        DBManager.getDBManager().alarmHistory.TranslateAlarms();
        DBManager.getDBManager().downTime.TranslateDowntimes();
        this.Cursor = Cursors.Arrow;
      }
		}

		/// <summary>
		/// must be overridden to change the Ctrl + D  key behaviour
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual void ControlD()
		{
		}
		#endregion

		/// <summary>
		/// to be called by the server and invoked by the client throw the reflection to show a new page.
		/// </summary>
		public virtual void _Show()
		{
      //ABELLO mysql 
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        this.refreshMySQLData();

			//show new page
			this.Show();

			this.Activate();
			
      if ((this != Form.ActiveForm) && (Form.ActiveForm != null))
			{
				Logger.LogError(this, "_Show - Failed to activate form \"" + this.Name + "\"; Instead current active form is \"" + Form.ActiveForm.Name + "\"");
			}
			//hide previous page
			Frm_Base previousPage = PageManager.getPageManager().getPreviousPage();
			if (previousPage != null && previousPage != this)
				previousPage.Hide();
		}

		/// <summary>
		/// Set up the rectangle components and the language config the page and load all the 
		/// local data showed (only at runtime).
		/// </summary>
		private void Frm_Base_Load(object sender, System.EventArgs e)
		{
			//only when the program is running because the Settings class is not available for the designer:
			//set up the rectangle components and the language;
			//call the config method to make the page aspect coerent with the configurtation client;
			//call the refreshData method to load every data stored in local database
			if (!this.DesignMode)
			{
				Settings s = Settings.getSettings();
				this.SetUp(getLocation(), getSize(), s.CurrentLanguage);
				ConfigPage(s.configuration);
				if (!s.forms.CorrugatorDirectionRight)
					CorrugatorDirectionLeft();

				//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
				if (s.forms.MachineDirectionVisualizationInverted)
					MachineDirectionVisualizationInverted();

				refreshData();			
        Accessibility acc = Accessibility.getAccessibility();
				setAccessibilityIcons(acc.Level);
				SetConsole(acc.ConsoleEnabled, false);
			}
		}

		protected int intParse(string s)
		{
			s = s.Trim();
			if (s.Equals(string.Empty))
				return 0;
			int value = 0;
			if (int.TryParse(s, out value))
				return value;
			return 0;
		}

    protected bool boolParse(string s)
    {
      s = s.Trim();
      if (s.Equals(string.Empty))
        return false;
      bool value = false;
      if (bool.TryParse(s, out value))
        return value;
      return false;
    }

    protected int intParse(string s, bool hex)
		{
			int value = 0;
			if (hex)
			{
				try
				{
					value = Convert.ToInt32(s, 16);
				}
				catch
				{
					value = 0;
				}
				return value;
			}
			else
				return this.intParse(s);
		}

		protected double doubleParse(string s, int decimalDigits)
		{
			s = s.Trim();
			if (s.Equals(string.Empty))
				return 0;
			double value = 0;
			if (double.TryParse(s, NumberStyles.Float, SyncroConverter.NumberFormat(decimalDigits), out value))
				return value;
			return 0;
		}

    protected string NotEmptyString(string s)
    {
      if ((s == null) || (s == string.Empty))
        return " ";
      return s;
    }

		/// <summary>
		/// Changes the passed as parameter control's location to be mirrored respect to the passed
		/// limits
		/// </summary>
		/// <param name="control">the control which location has to be mirrored.</param>
		/// <param name="leftMirrorLimit">left limit to calculate the mirroring from</param>
		/// <param name="rightMirrorLimit">right limit to calculate the mirroring from</param>
		protected void HorizontalLocationMirror(Control control, int leftMirrorLimit, int rightMirrorLimit)
		{
			if (leftMirrorLimit <= control.Location.X
				|| rightMirrorLimit >= control.Location.X + control.Width)
			{
				control.Location = new Point(
					rightMirrorLimit - (control.Location.X - leftMirrorLimit) - control.Size.Width,
					control.Location.Y);
			}
		}

		//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
		protected void VerticalLocationMirror(Control control, int upMirrorLimit, int lowerMirrorLimit)
		{
			if (upMirrorLimit <= control.Location.Y
				|| lowerMirrorLimit >= control.Location.Y + control.Height)
			{
				control.Location = new Point(	control.Location.X,
					upMirrorLimit + (lowerMirrorLimit - control.Location.Y - control.Height));
			}
		}


		private void Frm_Base_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason.Equals(CloseReason.UserClosing))
			{
				PageManager.getPageManager()._SetMessage("§SYCC_00");
				e.Cancel = true;
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				if (cm.CanSendTCP())
				{	//ask the server to shout down the client
					cm.SendTcp(cm.SYNCROS_COMM_MANAGER, Command.COMMUNICATION_MANAGER + "." + Command.SHUTDOWN + "()");
				}
				else
					//probably the server is already down... so just shout down.
					cm.ShutDown(false, false);
			}
		}

    private void timer_BringToFront_Tick(object sender, EventArgs e)
    {
      //ABello_FI08/11/2007_1 Per non perdere il focus premendo il tasto Windows
      if (!this.DesignMode)
      {
        if (this.Visible)
        {
          if (this.TopMost)
          {
            Screen[] screens = Screen.AllScreens;
            if (screens.Length > 1)
            {
              int chars = 256;
              StringBuilder buff = new StringBuilder(chars);

              // Obtain the handle of the active window.
              IntPtr handle = GetForegroundWindow();

              // Update the controls.
              if (GetWindowText(handle, buff, chars) > 0)
              {

                if (!buff.ToString().Contains("Frm_"))
                {
                  this.BringToFront();
                  this.Activate();
                }
              }
            }
            else
            {
              if (Form.ActiveForm == null)
              {
                this.BringToFront();
              }
            }
          }
        }
      }
    }

    protected void MakePropertyBrowsable(object selectedObject, string propertyName, bool browsableFlag)
    {
      PropertyDescriptorCollection props = TypeDescriptor.GetProperties(selectedObject);
      AttributeCollection attribs = props[propertyName].Attributes;
      BrowsableAttribute attr = (BrowsableAttribute)attribs[BrowsableAttribute.Default.GetType()];
      Type attrType = attr.GetType();
      System.Reflection.FieldInfo fld = attrType.GetField("browsable", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
      fld.SetValue(attr, browsableFlag);
    }

    protected bool IsPropertyBrowsable(object selectedObject, string propertyName)
    {
      PropertyDescriptorCollection props = TypeDescriptor.GetProperties(selectedObject);
      AttributeCollection attribs = props[propertyName].Attributes;
      BrowsableAttribute attr = (BrowsableAttribute)attribs[BrowsableAttribute.Default.GetType()];
      Type attrType = attr.GetType();
      System.Reflection.FieldInfo fld = attrType.GetField("browsable", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
      return (bool)fld.GetValue(attr);
    }
	}
}
