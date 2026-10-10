using System;
using System.Drawing;
using Microsoft.Tools.Graphs.Lines;

namespace Microsoft.Tools.Graphs
{
	public enum Alignment
	{
		VerticalBottom,
		VerticalTop,
		HorizontalLeft,
		HorizontalRight
	}

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public abstract class GraphBase
	{
		private Size _size = Size.Empty;
		private int _border = 15;
		private Color _color = SystemColors.Info;
		private Color _colorGradient = Color.Empty;
		private string _text = "Graph";
		private Alignment _alignment = Alignment.VerticalBottom;
		private bool _roundOffGridHeight = true;

		public GraphBase()
		{
		}

		public GraphBase(Size size)
		{
			this._size = size;
		}

		public GraphBase(int width, int height)
		{
			this._size = new Size(width, height);
		}

		public virtual Size Size
		{
			get
			{
				if (_size == Size.Empty)
					return new Size(500, 500);
				else
					return _size;
			}
			set
			{
				_size = value;
			}
		}

		public int Border
		{
			get
			{
				return _border;
			}
			set
			{
				_border = value;
			}
		}

		public Color Color
		{
			get
			{
				return _color;
			}
			set
			{
				_color = value;
			}
		}

		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		public Color ColorGradient
		{
			get
			{
				if(_colorGradient==Color.Empty)
					return _color;
				else
					return _colorGradient;
			}
			set
			{
				_colorGradient = value;
			}
		}
		public Alignment Alignment
		{
			get
			{
				return _alignment;
			}
			set
			{
				_alignment = value;
			}
		}
		public bool RoundOffGridHeight
		{
			get
			{
				return _roundOffGridHeight;
			}
			set
			{
				_roundOffGridHeight = value;
			}
		}

	} // class
}// namespace
