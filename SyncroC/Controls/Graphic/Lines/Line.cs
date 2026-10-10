using System;
using System.Collections;
using System.Drawing;
using Microsoft.Tools.Graphs;

namespace Microsoft.Tools.Graphs.Lines
{
	/// <summary>
	/// Summary description for Line.
	/// </summary>
	public class Line
	{
		private Color _color = Color.Black;
		private LinePointCollection _points = null;
		private float _width = 1.0F;

		public Line()
		{
			_points = new LinePointCollection();
		}

		public Line(Color color)
		{
			_color = color;
			_points = new LinePointCollection();
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

		public LinePointCollection Points
		{
			get
			{
				return _points;
			}
			set
			{
				_points = value;
			}
		}

		public float Width
		{
			get
			{
				return _width;
			}
			set
			{
				_width = value;
			}
		}

		public void AddPoint(double xvalue, double yValue)
		{
			Points.Add(new LinePoint(xvalue, yValue, null));
		}

		public void AddPoint(double xvalue, double yValue, string text)
		{
			Points.Add(new LinePoint(xvalue, yValue, text));
		}

	}// class
}// namespace
