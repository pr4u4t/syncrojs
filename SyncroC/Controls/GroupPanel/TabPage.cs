using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using SyncroC.Utility;

namespace SyncroC.Controls.GroupPanel
{
	/// <summary>
	/// Tabpage that holds the controls.
	/// </summary>
	public class TabPage : Panel
	{
		//constants
		private int VERTICAL_SPACING_BETWEEN_CONTROLS = 8; 
		private int CONTROLS_DISTANCE_FROM_BORDER = 2; 

		//properties
		private string _text;
		/// <summary>
		/// Gets or sets the text
		/// </summary>
		public override string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
				{
					string oldValue = _text;
					_text = value;
					OnPropertyChanged(Property.Text, oldValue);
				}
			}
		}

		private int _imageIndex = -1;
		/// <summary>
		/// Gets or sets the image index
		/// </summary>
		public int ImageIndex
		{
			get { return _imageIndex; }
			set
			{
				if (_imageIndex != value)
				{
					int oldValue = _imageIndex;
					_imageIndex = value;
					OnPropertyChanged(Property.ImageIndex, _imageIndex);
				}
			}
		}

		//events
		/// <summary>
		/// Event raised when a porperty of the TabPage has been changed.
		/// </summary>
		public event PropChangeHandler PropertyChanged;

		// Coordinates for the outter rectangle
		private int _x0;
		private int _y0;
		private int _x1;
		private int _y1;

		/// <summary>
		/// Builder.
		/// </summary>
		public TabPage()
		{
			// Prevent redrawing flicker
			SetStyle(ControlStyles.ContainerControl, true);
			SetStyle(ControlStyles.FixedHeight, false);
			SetStyle(ControlStyles.FixedWidth, false);
		}

		/// <summary>
		/// Builder.
		/// Build a tab page with the specified text. Controls can be added later.
		/// </summary>
		/// <param name="name">the name of the control: 
		/// MUST BE UNIQUE INTO THE FORM to make possible automatic translations.
		/// It is strongly recommended to use the name of the reference of the tabPage into the form.</param>
		/// <param name="text">Tabpage text</param>
		public TabPage(string name, string text) : this()
		{
			_text = text;
			Name = name;
		}

		/// <summary>
		/// Builder.
		/// Build a tab page with the specified text and contestually add the specified Panel.
		/// Any other control cannot be added later.
		/// </summary>
		/// <param name="name">the name of the control: 
		/// MUST BE UNIQUE INTO THE FORM to make possible automatic translations.
		/// It is strongly recommended to use the name of the reference of the tabPage into the form.</param>
		/// <param name="text">Tabpage text</param>
		/// <param name="panel">the panel to add.</param>
		public TabPage(string name, string text, Panel panel) : this(name, text)
		{
			this.Controls.Add(panel);
			panel.Location = new Point(0, 0);
			panel.Show();
		}

		/// <summary>
		/// Adds the control that the tabPage holds
		/// </summary>
		/// <param name="control">Control to add</param>
		/// <exception cref="">Thrown if the control cannot be added to the TabPage because a panel has already been added. </exception>
		public void AddControl(Control control)
		{
			if (this.Controls.Count == 1 && this.Controls[0] is Panel)
			{
				throw new Exception("AddControl - A panel has already been added to the current TabPage so any other control can be added.");
			}
			this.Controls.Add(control);
			if (this.Controls.Count == 1)
				control.Location = new Point(CONTROLS_DISTANCE_FROM_BORDER, VERTICAL_SPACING_BETWEEN_CONTROLS);
			else
			{
				Control previousControl = this.Controls[this.Controls.Count - 2];
				control.Location  = new Point(CONTROLS_DISTANCE_FROM_BORDER, previousControl.Location.Y + previousControl.Height + VERTICAL_SPACING_BETWEEN_CONTROLS);
			}
			control.Width = this.Width - CONTROLS_DISTANCE_FROM_BORDER * 2;
			control.Show();
		}

		/// <summary>
		/// Adds a range of controls to the tabPage.
		/// </summary>
		/// <param name="controls">array of Controls to be added</param>
		/// <exception cref="">Thrown if any of the specified controls cannot be added to the TabPage because a panel has already been added. </exception>
		public void AddControlsRange(Control[] controls)
		{
			foreach (Control control in controls)
				AddControl(control);
		}

		/// <summary>
		/// Sets the control's coordinates
		/// </summary>
		/// <param name="x0">Left coordinate</param>
		/// <param name="y0">Top coordinate</param>
		/// <param name="x1">Right coordinate</param>
		/// <param name="y1">Bottom coordinate</param>
		internal void SetCoordinates(int x0, int y0, int x1, int y1)
		{
			_x0 = x0;
			_y0 = y0;
			_x1 = x1;
			_y1 = y1;
		}

		/// <summary>
		/// Gets the control's coordinates
		/// </summary>
		/// <param name="x0">Left coordinate</param>
		/// <param name="y0">Top coordinate</param>
		/// <param name="x1">Right coordinate</param>
		/// <param name="y1">Bottom coordinate</param>
		internal void GetCoordinates(out int x0, out int y0, out int x1, out int y1)
		{
			x0 = _x0;
			y0 = _y0;
			x1 = _x1;
			y1 = _y1;
		}

		/// <summary>
		/// Gets if the x,y coordinates are over the control
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <returns>True if the x,y coordinates are over the control</returns>
		internal bool HitTest(int x, int y)
		{
			if (x >= _x0 && x <= _x1)
				if (y >= _y0 && y <= _y1)
					// It is over the control
					return true;
			// It is not over the control
			return false;
		}

		/// <summary>
		/// A tabPage property has changed
		/// </summary>
		/// <param name="prop">Changed property</param>
		/// <param name="oldValue">Value before the change</param>
		public void OnPropertyChanged(Property prop, object oldValue)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, prop, oldValue);
		}
	}
}
