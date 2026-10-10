using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls;
using SyncroC.Controls.Interfaces;
using SyncroC.Controls.Colors;

namespace SyncroC.Controls.IncDecButton
{
	/// <summary>
	/// Enumerates every kind of arrows to be displayed into the IncDecButton component.
	/// </summary>
	public enum IncDecButtonArrowImage
	{
		/// <summary>Text.</summary>
		Text = 0,
		/// <summary>Identify the UP pointing arrow.</summary>
		UpArrow = 1,
		/// <summary>Identify the DOWN pointing arrow.</summary>
		DownArrow = 2,
		/// <summary>Identify the LEFT pointing arrow.</summary>
		LeftArrow = 3,
		/// <summary>Identify the RIGHT pointing arrow.</summary>
		RightArrow = 4,
		/// <summary>Identify WIDENING arrows.</summary>
		WidenArrows = 5,
		/// <summary>Identify PRESSING arrows.</summary>
		PressArrows = 6
	}

	/// <summary>
	/// Delegate to use for OnIncDecButtonDown event handling.
	/// </summary>
	public delegate void IncDecButtonDownHandler(object sender, IncDecButtonDownEventArgs args);

	/// <summary>
	/// Implements a special button that raise events while it is pushed and 
	/// provides methods to perform the push of the button frm the code.
	/// </summary>
	public class IncDecButton : SyncroC.Controls.SyncroUserControl
	{
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ImageList userDefinedImageList;
		private System.ComponentModel.IContainer components;

		/// <summary>Frequency value for FIRST OnIncDecButtonDownEvent</summary>
		private static int FIRST_FIRE = -1;
		/// <summary>Frequency value for LAST OnIncDecButtonDownEvent</summary>
		private static int LAST_FIRE = -2;

		//design constants
		private const short BORDER = 3;
		private int CORNER_INDENTATION = 8;
		private int FRAME_WIDTH = 3;

		//points calculated at every size changed event to redraw the component
		private int Y_1_2;
		private int Y_1_3;
		private int Y_2_3;
		private int Y_1_9;
		private int Y_4_9;
		private int Y_5_9;
		private int Y_8_9;
		private int X_1_2;
		private int X_1_3;
		private int X_2_3;
		private int X_1_9;
		private int X_4_9;
		private int X_5_9;
		private int X_8_9;

		private Point corner_H_L;
		private Point corner_H_R;
		private Point corner_L_L;
		private Point corner_L_R;

		/// <summary>
		/// Ticks between two OnIncDecButtonDownEvent fire.
		/// -1 (FIRST_FIRE) means first fire.
		/// -2 (LAST_FIRE) means last fire.
		/// </summary>
		private double lastOnIncDecButtonDownEventRaisedTicks = FIRST_FIRE;
		
		/// <summary>Determine if the component is currently signaled.</summary>
		private bool signaled = false;

		private bool lowLevelEnabled;

		/// <summary>Current showed arrow image index.</summary>
		private IncDecButtonArrowImage arrow = IncDecButtonArrowImage.Text;
		/// <summary>
		/// Gets or sets the image to be displaied on te control.
		/// </summary>
		[Description("Gets or sets the image to be displaied on te control."),
		Category("FosberSoft")]
		public IncDecButtonArrowImage Image
		{
			get {	return arrow;		}
			set	{	arrow = value;	}
		}

		/// <summary>
		/// gets or sets the text to be written into the control.
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

		/// <summary>
		/// Gets or sets facility Low level.
		/// </summary>
		[Description("Gets or sets facility Low level."), Category("FosberSoft")]
		public bool LowLevelEnabled
		{
			get { return this.lowLevelEnabled; }
			set
			{
				if (this.lowLevelEnabled != value)
				{
					this.lowLevelEnabled = value;
          this.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
					this.Invalidate();
				}
			}
		}

    //ABELLO_FI30/10/2008_1 Messi i pulsanti Inc/Dec sulle PopUp che chiedono un valore numerico.
    /// <summary>
    /// Gets or sets facility Low level.
    /// </summary>
    [Description("Gets or sets Timer Interval."), Category("FosberSoft")]
    public int TimerInterval
    {
      get { return this.timer.Interval; }
      set
      {
        if (this.timer.Interval != value)
        {
          this.timer.Interval = value;
        }
      }
    }

		/// <summary>
		/// Event raised every interval time when the button is down.
		/// </summary>
		[Description("Event raised every interval time when the button is down."), Category("Fosbersoft")]
		public event IncDecButtonDownHandler OnIncDecButtonDown; 

		public IncDecButton()
		{
			InitializeComponent();

			this.lowLevelEnabled = true;

			this.GotFocus += new System.EventHandler(IncDecButton_GotFocus);
			this.LostFocus += new System.EventHandler(IncDecButton_LostFocus);
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncDecButton));
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.userDefinedImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// userDefinedImageList
			// 
			this.userDefinedImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("userDefinedImageList.ImageStream")));
			this.userDefinedImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.userDefinedImageList.Images.SetKeyName(0, "");
			this.userDefinedImageList.Images.SetKeyName(1, "");
			this.userDefinedImageList.Images.SetKeyName(2, "");
			this.userDefinedImageList.Images.SetKeyName(3, "");
			this.userDefinedImageList.Images.SetKeyName(4, "");
			this.userDefinedImageList.Images.SetKeyName(5, "");
			this.userDefinedImageList.Images.SetKeyName(6, "");
			// 
			// IncDecButton
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "IncDecButton";
			this.Size = new System.Drawing.Size(60, 72);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.IncDecButton_KeyUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.component_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.component_MouseUp);
			this.EnabledChanged += new System.EventHandler(this.IncDecButton_EnabledChanged);
			this.SizeChanged += new System.EventHandler(this.IncDecButton_SizeChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IncDecButton_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Change the visual feedback of the button according to the parameter.
		/// IMPORTANT NOTICE: DO NOT CHANGE THE NAME OF THIS METHOD!
		/// </summary>
		/// <param name="state">new state the button has to show.</param>
		public void _signal(bool state)
		{
			signaled = state;
			this.Invalidate();
		}

		/// <summary>
		/// Simulate the MouseDown event of the button.
		/// </summary>
		public void PerformMouseDown()
		{
			if (! timer.Enabled)
			{
				RaiseOnIncDecButtonDownEvent();
				timer.Start();
			}
		}
		
		private void component_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.Enabled)
				PerformMouseDown();
		}

		/// <summary>
		/// Simulate the MouseUp event of the button.
		/// </summary>
		public void PerformMouseUp()
		{
			if (timer.Enabled)
			{
				timer.Stop();
				lastOnIncDecButtonDownEventRaisedTicks = LAST_FIRE;
				RaiseOnIncDecButtonDownEvent();
			}
		}

		private void component_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			PerformMouseUp();
		}

		private void IncDecButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.Equals(Keys.Space))
				if (this.Enabled)
					PerformMouseDown();
		}

		private void IncDecButton_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.Equals(Keys.Space))
				PerformMouseUp();
		}

		/// <summary>
		/// Raise an OnButtonDown event APPROXIMATIVELY each interval milliseconds 
		/// beetwen the MouseDown and MouseUp events.
		/// </summary>
		private void timer_Tick(object sender, System.EventArgs e)
		{
			RaiseOnIncDecButtonDownEvent();
		}
	
		/// <summary>
		/// Raise an OnButtonDown event only if there is at least one listener.
		/// Takes care about the frequency parameter which is:
		/// -1 for first fire;
		/// -2 for last fire;
		/// the milliseconds between the previous and current fire for all the other fires.
		/// Keep updated the lastOnIncDecButtonDownEventRaisedTicks.
		/// </summary>
		private void RaiseOnIncDecButtonDownEvent()
		{
			if (OnIncDecButtonDown != null)
			{
				double now = (double)System.DateTime.Now.Ticks;
				int frequency;
				if (lastOnIncDecButtonDownEventRaisedTicks == FIRST_FIRE)
				{	
					frequency = FIRST_FIRE;
					lastOnIncDecButtonDownEventRaisedTicks = now;
				}
				else if (lastOnIncDecButtonDownEventRaisedTicks == LAST_FIRE)
				{
					frequency = LAST_FIRE;
					lastOnIncDecButtonDownEventRaisedTicks = FIRST_FIRE;
				}
				else
				{
					try
					{
						frequency = Convert.ToInt32(Math.Ceiling((now - lastOnIncDecButtonDownEventRaisedTicks) / 10000));
					}
					catch (OverflowException ex)
					{
						ex.ToString();
						frequency = Int32.MaxValue;
					}
					lastOnIncDecButtonDownEventRaisedTicks = now;
				}
				OnIncDecButtonDown(this, new IncDecButtonDownEventArgs(frequency));
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			//draw the focus 
			if (this.Focused || this.ContainsFocus)
			{
				g.DrawLines(
						new Pen(new SolidBrush(FOCUS_COLOR), 1),
						new Point[]{
												new Point(0, 0),
										   new Point(this.Width -1, 0),
										   new Point(this.Width -1, this.Height -1),
										   new Point(0, this.Height -1),
										   new Point(0, 0)});
			}

			//points calculations
			Point[] pointsFrame = null;
			Point[] pointsArrow1 = null;
			Point[] pointsArrow2 = null;
			TextLayout textLayout = new TextLayout();
			
			switch (arrow)
			{
				case IncDecButtonArrowImage.Text:
					//int wideSpace = corner_H_R.X - corner_H_L.X - (2 * (BORDER + FRAME_WIDTH));
					//int highSpace = corner_L_L.Y - corner_H_L.Y - (2 * (BORDER + FRAME_WIDTH));

					textLayout = CalculateTextLayout(
						new Rectangle(
							corner_H_L.X + BORDER + FRAME_WIDTH, //x
							corner_H_L.Y + BORDER + FRAME_WIDTH, //y
							corner_H_R.X - corner_H_L.X - (2 * (BORDER + FRAME_WIDTH)), //width
							corner_L_L.Y - corner_H_L.Y - (2 * (BORDER + FRAME_WIDTH))), //height
						this.Text,
						this.Font);

					pointsFrame = new Point[] { 
						corner_H_L, 
						corner_H_R, 
						corner_L_R, 
						corner_L_L, 
						corner_H_L};
					break;
				case IncDecButtonArrowImage.UpArrow:
					pointsArrow1 = new Point[] { new Point(X_1_2, Y_1_3), new Point(X_2_3, Y_2_3), new Point(X_1_3, Y_2_3) };
					pointsFrame = new Point[] { 
						corner_H_L, 
						corner_H_R, 
						new Point(corner_L_R.X, corner_L_R.Y - CORNER_INDENTATION), 
						new Point(corner_L_R.X - CORNER_INDENTATION, corner_L_R.Y - CORNER_INDENTATION), 
						new Point(corner_L_R.X - CORNER_INDENTATION, corner_L_R.Y), 
						corner_L_L,
						corner_H_L};
					break;
				case IncDecButtonArrowImage.DownArrow:
					pointsArrow1 = new Point[] { new Point(X_1_3, Y_1_3), new Point(X_2_3, Y_1_3), new Point(X_1_2, Y_2_3) };
					pointsFrame = new Point[] { 
						corner_H_L, 
						new Point(corner_H_R.X - CORNER_INDENTATION, corner_H_R.Y), 
						new Point(corner_H_R.X - CORNER_INDENTATION, corner_H_R.Y + CORNER_INDENTATION), 
						new Point(corner_H_R.X, corner_H_R.Y + CORNER_INDENTATION), 
						corner_L_R,
						corner_L_L,
						corner_H_L};
					break;
				case IncDecButtonArrowImage.LeftArrow:
					pointsArrow1 = new Point[] { new Point(X_1_3, Y_1_2), new Point(X_2_3, Y_1_3), new Point(X_2_3, Y_2_3) };
					pointsFrame = new Point[] { 
						corner_H_L, 
						corner_H_R, 
						new Point(corner_L_R.X, corner_L_R.Y - CORNER_INDENTATION), 
						new Point(corner_L_R.X - CORNER_INDENTATION, corner_L_R.Y - CORNER_INDENTATION), 
						new Point(corner_L_R.X - CORNER_INDENTATION, corner_L_R.Y), 
						corner_L_L,
						corner_H_L};
					break;
				case IncDecButtonArrowImage.RightArrow:
					pointsArrow1 = new Point[] { new Point(X_1_3, Y_1_3), new Point(X_2_3, Y_1_2), new Point(X_1_3, Y_2_3) };
					pointsFrame = new Point[] { 
						corner_H_L, 
						corner_H_R, 
						corner_L_R, 
						new Point(corner_L_L.X + CORNER_INDENTATION, corner_L_L.Y), 
						new Point(corner_L_L.X + CORNER_INDENTATION, corner_L_L.Y - CORNER_INDENTATION), 
						new Point(corner_L_L.X, corner_L_L.Y - CORNER_INDENTATION), 
						corner_H_L};
					break;
				case IncDecButtonArrowImage.WidenArrows:
					pointsArrow1 = new Point[] { new Point(X_1_9, Y_1_2), new Point(X_4_9, Y_1_3), new Point(X_4_9, Y_2_3) };
					pointsArrow2 = new Point[] { new Point(X_5_9, Y_1_3), new Point(X_8_9, Y_1_2), new Point(X_5_9, Y_2_3) };
					pointsFrame = new Point[] { 
						corner_H_L, 
						corner_H_R, 
						corner_L_R, 
						corner_L_L, 
						corner_H_L};
					break;
				case IncDecButtonArrowImage.PressArrows:
					pointsArrow1 = new Point[] { new Point(X_1_9, Y_1_3), new Point(X_4_9, Y_1_2), new Point(X_1_9, Y_2_3) };
					pointsArrow2 = new Point[] { new Point(X_5_9, Y_1_2), new Point(X_8_9, Y_1_3), new Point(X_8_9, Y_2_3) };
					pointsFrame = new Point[] { 
						corner_H_L, 
						corner_H_R, 
						new Point(corner_L_R.X, corner_L_R.Y - CORNER_INDENTATION), 
						new Point(corner_L_R.X - CORNER_INDENTATION, corner_L_R.Y - CORNER_INDENTATION), 
						new Point(corner_L_R.X - CORNER_INDENTATION, corner_L_R.Y), 
						new Point(corner_L_L.X + CORNER_INDENTATION, corner_L_L.Y), 
						new Point(corner_L_L.X + CORNER_INDENTATION, corner_L_L.Y - CORNER_INDENTATION), 
						new Point(corner_L_L.X, corner_L_L.Y - CORNER_INDENTATION), 
						corner_H_L};
					break;
			}

			SyncroColor colore = SyncroColor.Green;
			if (!this.lowLevelEnabled)
				colore = SyncroColor.LightGray;

			//drawing frame and arrows
			if (pointsFrame != null && pointsFrame.Length != 0)
			{
				SolidBrush arrowBrush;
				SolidBrush frameBrush = new SolidBrush(ColConv.Convert(colore));
				if (this.signaled)
				{
					arrowBrush = new SolidBrush(ColConv.Convert(SyncroColor.White));
					g.FillPolygon(new SolidBrush(ColConv.Convert(SyncroColor.Green)), pointsFrame);
					if (arrow == IncDecButtonArrowImage.Text)
					{
						if (textLayout.Rectangles != null && textLayout.Strings != null &&
							textLayout.Rectangles.Length != 0 && textLayout.Strings.Length != 0)
						{
							for (int i = 0; i < textLayout.Count; i++)
								g.DrawString(textLayout.Strings[i], this.Font, new SolidBrush(ColConv.Convert(SyncroColor.White)), textLayout.Rectangles[i]);
						}
					}
					else
					{
						if (pointsArrow1 != null && pointsArrow1.Length != 0)
						{
							g.FillPolygon(arrowBrush, pointsArrow1);
							if (pointsArrow2 != null && pointsArrow2.Length != 0)
								g.FillPolygon(arrowBrush, pointsArrow2);
						}
					}
				}
				else
				{
					SolidBrush notEnabledBrush = new SolidBrush(ColConv.Convert(SyncroColor.LightGray));
					arrowBrush = new SolidBrush(ColConv.Convert(colore));
					g.DrawLines(new Pen((this.Enabled) ? frameBrush : notEnabledBrush, FRAME_WIDTH), pointsFrame);
					if (arrow == IncDecButtonArrowImage.Text)
					{
						if (textLayout.Rectangles != null && textLayout.Strings != null &&
							textLayout.Rectangles.Length != 0 && textLayout.Strings.Length != 0)
						{
							for (int i = 0; i < textLayout.Count; i++)
								g.DrawString(textLayout.Strings[i], this.Font,
									(this.Enabled) ? new SolidBrush(this.ForeColor) : notEnabledBrush, textLayout.Rectangles[i]);
						}
					}
					else
					{
						if (pointsArrow1 != null && pointsArrow1.Length != 0)
						{
							g.FillPolygon((this.Enabled) ? arrowBrush : notEnabledBrush, pointsArrow1);
							if (pointsArrow2 != null && pointsArrow2.Length != 0)
								g.FillPolygon((this.Enabled) ? arrowBrush : notEnabledBrush, pointsArrow2);
						}
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void IncDecButton_SizeChanged(object sender, System.EventArgs e)
		{
			Y_1_2 = Convert.ToInt32(this.Height / 2);
			Y_1_3 = Convert.ToInt32(this.Height / 3);
			Y_2_3 = Convert.ToInt32((this.Height / 3) * 2);
			Y_1_9 = Convert.ToInt32(this.Height / 9);
			Y_4_9 = Convert.ToInt32((this.Height / 9) * 4);
			Y_5_9 = Convert.ToInt32((this.Height / 9) * 5);
			Y_8_9 = Convert.ToInt32((this.Height / 9) * 8);
			X_1_2 = Convert.ToInt32(this.Width / 2);
			X_1_3 = Convert.ToInt32(this.Width / 3);
			X_2_3 = Convert.ToInt32((this.Width / 3) * 2);
			X_1_9 = Convert.ToInt32(this.Width / 9);
			X_4_9 = Convert.ToInt32((this.Width / 9) * 4);
			X_5_9 = Convert.ToInt32((this.Width / 9) * 5);
			X_8_9 = Convert.ToInt32((this.Width / 9) * 8);

			corner_H_L = new Point(BORDER, BORDER);
			corner_H_R = new Point(this.Width - BORDER - 1, BORDER);
			corner_L_L = new Point(BORDER, this.Height - BORDER - 1);
			corner_L_R = new Point(this.Width - BORDER - 1, this.Height - BORDER - 1);

			this.Invalidate();
		}

		private void IncDecButton_LostFocus(object sender, System.EventArgs e)
		{
			this.Invalidate();
		}
		private void IncDecButton_GotFocus(object sender, System.EventArgs e)
		{
			this.Invalidate();
		}

		private void IncDecButton_EnabledChanged(object sender, EventArgs e)
		{
			this.Invalidate();
		}
	}
}