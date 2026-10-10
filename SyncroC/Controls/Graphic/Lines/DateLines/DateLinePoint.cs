using System;

namespace Microsoft.Tools.Graphs.Lines.DateLines
{
	/// <summary>
	/// Summary description for DatePoint.
	/// </summary>
	public class DateLinePoint: LinePoint
	{
		private DateTime _date = DateTime.MinValue;

		public DateLinePoint() : base()
		{
		}

		public DateLinePoint(DateTime date, double value) : base()
		{
			_date = date;
			base.YValue = value;
		}

		public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
			}
		}

	}// class
}// namespace
