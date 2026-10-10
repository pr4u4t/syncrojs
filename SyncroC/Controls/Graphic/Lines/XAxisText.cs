using System;

namespace Microsoft.Tools.Graphs.Lines
{
	/// <summary>
	/// Summary description for XAxisText.
	/// </summary>
	public class XAxisText
	{
		private double _xValueStart = 0.0;
		private double _xValueEnd = 0.0; 
		private string _text = null;

		public XAxisText()
		{
		}

		public XAxisText(double xValue, string text)
		{
			_xValueStart = xValue;
			_xValueEnd = xValue;
			_text = text;
		}

		public XAxisText(double xValueStart, double xValueEnd, string text)
		{
			_xValueStart = xValueStart;
			_xValueEnd = xValueEnd;
			_text = text;
		}

		public double XValueStart
		{
			get
			{
				return _xValueStart;
			}
			set
			{
				_xValueStart = value;
			}
		}

		public double XValueEnd
		{
			get
			{
				return _xValueEnd;
			}
			set
			{
				_xValueEnd = value;
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
