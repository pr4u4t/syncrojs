using System;

namespace Microsoft.Tools.Graphs.Lines.DateLines
{
	/// <summary>
	/// Summary description for DateXAxisText.
	/// </summary>
	public class DateXAxisText : XAxisText
	{
		private DateTime _startDate;
		private DateTime _endDate;

		public DateXAxisText() : base()
		{
		}
		public DateXAxisText(DateTime date, string text) : base()
		{
			_startDate = date;
			_endDate = date;
			base.Text = text;
		}
		public DateXAxisText(DateTime startDate, DateTime endDate, string text) : base()
		{
			_startDate = startDate;
			_endDate = endDate;
			base.Text = text;
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


	}// class
}// namespace
