using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;  
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using SyncroC.Forms.Pages;
using SyncroC.DataBase.EventArgs;
using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.DataBase;

namespace SyncroC.Forms.StatusBar
{
	public class Frm_StatusBar : SyncroC.Forms.Frm_Base
	{
		/// <summary>Used to send key events to the pages.</summary>
		[DllImport( "User32.dll")]
		public static extern int PostMessage(System.IntPtr hWnd, int msg,int param1, int param2 );
		private const int  WM_KEYDOWN   =  0x0100;

		public static readonly Size ORIGINAL_SIZE = new Size(1280, 286);

		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// builder.
		/// </summary>
		public Frm_StatusBar()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		public override void _Show()
		{
			base._Show ();
			//Keep trace of currently showed status bar
			PageManager.getPageManager().UpdateCurrentStatusBar(this);
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatusBar));
			this.SuspendLayout();
			// 
			// iml_AccessibilityImages
			// 
			this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
			this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
			this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
			// 
			// Frm_StatusBar
			// 
			this.ClientSize = new System.Drawing.Size(1280, 286);
			this.DesignGridHorizontalLines = 0;
			this.ForeColor = System.Drawing.SystemColors.WindowText;
		    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Location = new System.Drawing.Point(0, 507);
			this.Name = "Frm_StatusBar";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Tag = "StatusBar_4";
			this.Text = "Frm_StatusBar";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Frm_StatusBar_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StatusBar_MouseDown);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// return the position the statusBar should take on the screen: below the pages.
		/// </summary>
		protected override Point getLocation()
		{
			//if (Accessibility.getAccessibility().ConsoleEnabled)
			//{
				//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
				//return new Point(0, Convert.ToInt16(this.getScreenSize().Height - this.getSize().Height));
				return new Point(myScreen.Bounds.Left, Convert.ToInt16(this.getScreenSize().Height - this.getSize().Height));
			//}
			//else
			//{
			//  return new Point(0, Convert.ToInt16(this.getScreenSize().Height - this.getSize().Height) - 20);
			//}
		}

		/// <summary>
		///return the "run time" status bar's size.  
		/// </summary>
		protected override Size getSize()
		{
			if (Accessibility.getAccessibility().ConsoleEnabled)
			{
				return new Size(
					//width
					this.getScreenSize().Width,
				Convert.ToInt16(
					//height
						((this.getOriginalSize().Height * this.getScreenSize().Height) /
						(this.getOriginalSize().Height + Frm_Page.ORIGINAL_SIZE.Height))));
			}
			else
			{
				return new Size(
					//width
					this.getScreenSize().Width,
				Convert.ToInt16(
					//height
						((this.getOriginalSize().Height * this.getScreenSize().Height) /
						(this.getOriginalSize().Height + Frm_Page.ORIGINAL_SIZE.Height))) + getConsoleDisabledOffset());
			}
		}

		protected override int getConsoleDisabledOffset()
		{
			return 20;//optimized for 1280 X 1024 graphics
		}

		/// <summary>
		/// return the "design time" status bar's size.
		/// </summary>
		protected override Size getOriginalSize()
		{
			return ORIGINAL_SIZE;
		}

		/// <summary>
		///the status bar must not do anything on keyDown.
		///if a keyDown event is catched it is posted to the current page 
		/// </summary>
		protected override void Frm_Base_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Frm_Page currentPage = PageManager.getPageManager().getCurrentPage();
			if (currentPage != null)
			{
				PostMessage(currentPage.Handle, WM_KEYDOWN,e.KeyValue,0);
				currentPage.Focus();
			}
		}

		/// <summary>
		/// if the status bar is clicked the focus must be return to the currentPage
		/// </summary>
		private void StatusBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Frm_Page currentPage = PageManager.getPageManager().getCurrentPage();
			if (currentPage != null) currentPage.Focus();
		}

		/// <summary>
		/// 
		/// </summary>
		private void Frm_StatusBar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (this.DesignMode && ShowDesignGrid)
			{
				int topBorder = 24; 
				int sideBorder = 4;
				int lowBorder = 18;

				Point heightLeft = new Point(
					sideBorder,//x
					topBorder);//y
				Point lowLeft = new Point(
					sideBorder,//x
					this.Height - lowBorder);//y
				Point lowRight = new Point(
					this.Width - sideBorder,//x
					this.Height - lowBorder);//y
				Point heightRight = new Point(
					this.Width - sideBorder,//x
					topBorder);//y

				Graphics g = e.Graphics;
				Pen pen = new Pen(new SolidBrush(Color.Red), 1);

				//draw borders
				g.DrawLines(pen,
					new Point[]{ heightLeft, lowLeft, lowRight,	heightRight,	heightLeft});

				//draw grid (vertical lines)
				for (int i = 1; i <= designGridVerticalLines; i++)
				{
					int X = Convert.ToInt32(heightLeft.X + (heightRight.X - heightLeft.X) / (designGridVerticalLines + 1) * i);
					g.DrawLine(pen,
						new Point(
						X,
						heightLeft.Y),
						new Point(
						X,
						lowLeft.Y));
				}

				//draw grid (horizontal lines)
				for (int i = 1; i <= designGridHorizontalLines; i++)
				{
					int Y = Convert.ToInt32(heightLeft.Y + (lowLeft.Y - heightLeft.Y) / (designGridHorizontalLines + 1) * i);
					g.DrawLine(pen,
						new Point(
						heightLeft.X,
						Y),
						new Point(
						heightRight.X,
						Y));
				}
			}
		}

		/// <summary>
		/// Put the Active property of every stateButton of the statusbar to false.
		/// </summary>
		protected void ClearActiveGroups()
		{
			FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (FieldInfo fieldInfo in fields)
			{
				object field = fieldInfo.GetValue(this);
				if (field is SyncroC.Controls.StateButton)
				{
          //ABELLO_FI05/05/09_2 questo ogni volta che si cambia da barra DRY a barra WET andava tutto in manuale per poi ritornare in Auto
          //if (((SyncroC.Controls.StateButton)field).Active)
					if ( ((SyncroC.Controls.StateButton)field).Active &&  (((SyncroC.Controls.StateButton)field).Image == SyncroC.Controls.StateButtonImage.UserDefined) )
						((SyncroC.Controls.StateButton)field).Active = false;
				}
			}
		}

		/// <summary>
		/// Called everytime there is a page change.
		/// Must be overridden by every status bar to manage the state of every buttons 
		/// present into the specific bar according to the current page group.
		/// NOTE: inheritors that overrides this method MUST call the following "OnPageChanged" base
		/// method to ensure the clearing of active group when passing to a group which is not 
		/// accessible from the status bar
		/// </summary>
		protected virtual void OnPageChanged(object sender, OnPageChangedEventArgs args)
		{
			string group = args.groupType.ToString();
			
			//NOTE: when a new group accessible from a button on the "page" (not in any status bar)
			//will be added, then it MUST be included into the following if statement.
			if (group.StartsWith("SyncroC.Forms.Pages.Production")
				|| group.StartsWith("SyncroC.Forms.Pages.Settings")
				|| group.StartsWith("SyncroC.Forms.Pages.Statistics")
				|| group.StartsWith("SyncroC.Forms.Pages.Twin400")
				|| group.StartsWith("SyncroC.Forms.Pages.Debug")
			//	|| "new groups not accessible from status bar here!"
				)
			{
				ClearActiveGroups();
			}
		}
	}
}