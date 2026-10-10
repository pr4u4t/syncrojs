using System;

namespace Microsoft.Tools.Graphs.Lines
{
	/// <summary>
	/// Summary description for LinePoint.
	/// </summary>
	public class LinePoint
	{
		private double _xValue = 0.0;
		private double _yValue = 0.0; 
		private string _text = null;

		public LinePoint()
		{
		}

		public LinePoint(double xValue, double yValue)
		{
			_xValue = xValue;
			_yValue = yValue;
		}

		public LinePoint(double xValue, double yValue, string text)
		{
			_xValue = xValue;
			_yValue = yValue;
			_text = text;
		}

		public double XValue
		{
			get
			{
				return _xValue;
			}
			set
			{
				_xValue = value;
			}
		}

		public double YValue
		{
			get
			{
				return _yValue;
			}
			set
			{
				_yValue = value;
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

	}// class
}// namespace
