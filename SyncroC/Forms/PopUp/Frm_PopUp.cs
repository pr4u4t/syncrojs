using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using SyncroC.Forms.StatusBar;
using SyncroC.Forms.Pages;
using SyncroC.Utility;

namespace SyncroC.Forms.PopUp
{
	public class Frm_PopUp : SyncroC.Forms.Frm_Base
	{
		public static readonly Size ORIGINAL_SIZE = new Size(350, 200);

		private System.ComponentModel.IContainer components = null;

    protected object[] data;

    public Frm_PopUp()
		{
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PopUp));
			this.SuspendLayout();
			// 
			// iml_AccessibilityImages
			// 
			this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
			this.iml_AccessibilityImages.Images.SetKeyName(0, "");
			this.iml_AccessibilityImages.Images.SetKeyName(1, "");
			// 
			// Frm_PopUp
			// 
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(350, 200);
			this.ControlBox = false;
			this.DesignGridHorizontalLines = 1;
			this.DesignGridVerticalLines = 3;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Frm_PopUp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Frm_PopUp";
			this.TopMost = false;
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Frm_PopUp_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		//every pop up are centered into the screen
		protected override Point getLocation()
		{
			Size size = getSize();
      //RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
      //return new Point(
      //  (this.getScreenSize().Width - size.Width) / 2,
      //  (this.getScreenSize().Height - size.Height) / 4 * 3);
      return new Point(
        myScreen.Bounds.Left+(this.getScreenSize().Width - size.Width) / 2,
				(this.getScreenSize().Height - size.Height)/4*3);
		}

		//return the "run time" page's size 
		//TODO : parametrizzare un po meglio....
		protected override Size getSize()
		{
			if (Accessibility.getAccessibility().ConsoleEnabled)
			{
				return new Size(
					//width
					Convert.ToInt16(
					((this.getOriginalSize().Width * this.getScreenSize().Width) /
					Frm_Page.ORIGINAL_SIZE.Width)),
					//height
					Convert.ToInt16(
					((this.getOriginalSize().Height * this.getScreenSize().Height) /
					(Frm_Page.ORIGINAL_SIZE.Height + Frm_StatusBar.ORIGINAL_SIZE.Height))));
			}
			else
			{
				return new Size(
					//width
					Convert.ToInt16(
					((this.getOriginalSize().Width * this.getScreenSize().Width) /
					Frm_Page.ORIGINAL_SIZE.Width)),
					//height
					Convert.ToInt16(
					((this.getOriginalSize().Height * this.getScreenSize().Height) /
					(Frm_Page.ORIGINAL_SIZE.Height + Frm_StatusBar.ORIGINAL_SIZE.Height)) + (getConsoleDisabledOffset())));
			}
		}

		//return the "design time" page's size
		protected override Size getOriginalSize()
		{
			return ORIGINAL_SIZE;
		}

		protected override void RightArrow()
		{

		}

    private Pen _borderPen = new Pen(Color.Black, 2);
		private void Frm_PopUp_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

      if (Accessibility.getAccessibility().ConsoleEnabled)
        g.DrawRectangle(_borderPen, new Rectangle(1, 1, this.Width - 2, this.Height - 2)); 

			Color borderColor = Color.WhiteSmoke;
			int borderWidth = 2;
			int border = 12;
			Point heightLeft = new Point(
				border, //x
				border); //y
			Point heightRight = new Point(
				this.Width - border - borderWidth, //x
				border); //y
			Point lowLeft = new Point(
				border, //x
				this.Height - border - borderWidth); //y
			Point lowRight = new Point(
				this.Width - border - borderWidth, //x
				this.Height - border - borderWidth); //y
			if (! Accessibility.getAccessibility().ConsoleEnabled)
			{
				lowLeft = new Point(
					border, //x
					this.Height - border - borderWidth - (getConsoleDisabledOffset())); //y
				lowRight = new Point(
					this.Width - border - borderWidth, //x
					this.Height - border - borderWidth - (getConsoleDisabledOffset())); //y
			}
			g.DrawLines(new Pen(new SolidBrush(borderColor), borderWidth),
				new Point[] {
				heightLeft, lowLeft, lowRight, heightRight, heightLeft
			});

			//draw design grid.
			if (this.DesignMode && showDesignGrid)
			{
				heightLeft = new Point(
											 heightLeft.X + border,
											 heightLeft.Y + border);
				lowLeft = new Point(
											 lowLeft.X + border,
											 lowLeft.Y - border);
				lowRight = new Point(
											 lowRight.X - border,
											 lowRight.Y - border);
				heightRight = new Point(
											 heightRight.X - border,
											 heightRight.Y + border);
				Pen pen = new Pen(new SolidBrush(Color.Red), 1);
				//draw borders
				g.DrawLines(pen,
					new Point[] { heightLeft, lowLeft, lowRight, heightRight, heightLeft });

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
	}
}

