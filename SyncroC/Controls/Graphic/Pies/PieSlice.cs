using System;
using System.Drawing;

namespace Microsoft.Tools.Graphs.Pies
{
	/// <summary>
	/// Summary description for PieSlice.
	/// </summary>
	public class PieSlice
	{
		private Color _color = Color.Blue;
		private double _value;

		public PieSlice()
		{
		}
		public PieSlice(double value, Color color)
		{
			_value = value;
			_color = color;
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
		public double Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

	}// class
}// namespace
