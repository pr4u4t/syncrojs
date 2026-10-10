using System;
using System.Drawing;

namespace Microsoft.Tools.Graphs.Lines.DateLines
{
	public enum DateMode
	{
		HalfDay = 1,
		Day = 2,
		Weeks = 3,
		Months = 4,
		Years = 5
	}

	/// <summary>
	/// Summary description for DateLineGraph.
	/// </summary>
	public class DateLineGraph : LineGraph
	{
		private DateTime _startDate = DateTime.MaxValue;
		private DateTime _endDate = DateTime.MinValue;
		private DateMode _dateMode = DateMode.Weeks;
		private DateLineCollection _dateLineCollection = null;
		private DateLine _trendLine = null;
		private DateXAxisTextCollection _dateXAxisTextCollection = null;
		private DateXAxisTextCollection _datePhaseLines = null;

		public DateLineGraph() : base()
		{
			_dateLineCollection = new DateLineCollection();
			_dateXAxisTextCollection = new DateXAxisTextCollection();
			_datePhaseLines = new DateXAxisTextCollection();
			_trendLine = new DateLine();
			_trendLine.Width = 2.0F;
			this.MarginForTextOnAxis = 15;
		}

		public DateLineGraph(DateMode dateMode) : base()
		{
			_dateMode = dateMode;
			_dateLineCollection = new DateLineCollection();
			_dateXAxisTextCollection = new DateXAxisTextCollection();
			_datePhaseLines = new DateXAxisTextCollection();
			_trendLine = new DateLine();
			_trendLine.Width = 2.0F;
			this.MarginForTextOnAxis = 15;
		}

		public DateLineGraph(DateTime startDate, DateTime endDate, DateMode dateMode) : base()
		{
			_startDate = startDate;
			_endDate = endDate;
			_dateMode = dateMode;
			_dateLineCollection = new DateLineCollection();
			_dateXAxisTextCollection = new DateXAxisTextCollection();
			_datePhaseLines = new DateXAxisTextCollection();
			_trendLine = new DateLine();
			_trendLine.Width = 2.0F;
			this.MarginForTextOnAxis = 15;
		}

		public DateMode DateMode
		{
			get
			{
				return _dateMode;
			}
			set
			{
				_dateMode = value;
			}
		}
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
			}
		}
		public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
			}
		}
		public DateLineCollection DateLines
		{
			get
			{
				return _dateLineCollection;
			}
			set
			{
				_dateLineCollection = value;
			}
		}

		public DateLine DateTrendLine
		{
			get
			{
				return _trendLine;
			}
			set
			{
				_trendLine = value;
			}
		}
		public DateXAxisTextCollection DateXAxisTextCollection
		{
			get
			{
				return _dateXAxisTextCollection;
			}
			set
			{
				_dateXAxisTextCollection = value;
			}
		}
		public DateXAxisTextCollection DatePhaseLines
		{
			get
			{
				return _datePhaseLines;
			}
			set
			{
				_datePhaseLines = value;
			}
		}
		
	}// class
}// namespace
