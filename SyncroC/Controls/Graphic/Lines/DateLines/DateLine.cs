using System;
using System.Collections;
using System.Drawing;
using Microsoft.Tools.Graphs;

namespace Microsoft.Tools.Graphs.Lines.DateLines
{
	/// <summary>
	/// Summary description for DateLine.
	/// </summary>
	public class DateLine: Line
	{
		private DateLinePointCollection _dateLinePointCollection = null;

		public DateLine() : base()
		{
			_dateLinePointCollection = new DateLinePointCollection();
		}

		public DateLine(Color color) : base(color)
		{
			_dateLinePointCollection = new DateLinePointCollection();
		}
		public DateLinePointCollection DatePoints
		{
			get
			{
				return _dateLinePointCollection;
			}
			set
			{
				_dateLinePointCollection = value;
			}
		}

		public void AddDateLinePoint(DateTime date, double value)
		{
			_dateLinePointCollection.Add(new DateLinePoint(date, value));
		}

	}// class
}// namespace
