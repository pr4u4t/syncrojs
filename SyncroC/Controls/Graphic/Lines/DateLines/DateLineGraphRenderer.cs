using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Microsoft.Tools.Graphs.Lines.DateLines
{
	/// <summary>
	/// Summary description for DateLineGraphRenderer.
	/// </summary>
	internal class DateLineGraphRenderer
	{
		private DateLineGraph _dateLineGraph;
		private DateTime _startDate;
		private DateTime _endDate;
		private int _totalDays;
		private int _totalHours;
		private TimeSpan _timeSpan;
		private DateMode _dateMode;

		public DateLineGraphRenderer()
		{
			_startDate = new DateTime();
			_endDate = new DateTime();
		}

		public Image DrawGraph(DateLineGraph dateLineGraph)
		{
			try
			{
				if (dateLineGraph == null)
					return null;

				_dateLineGraph = dateLineGraph;
				CalculateValues();
				SetValues();

				LineGraphRenderer lgr = new LineGraphRenderer();
				return lgr.DrawGraph(this._dateLineGraph);
			}
			catch
			{
				return null;
			}
		}

		private void CalculateValues()
		{
			this._dateMode = _dateLineGraph.DateMode;
			this._startDate = GetMinimumDate();
			this._endDate = GetMaximumDate();
			this._timeSpan = GetTimeSpan();
			this._totalDays = this._timeSpan.Days;
			this._totalHours = this._timeSpan.Hours;
		}
		private TimeSpan GetTimeSpan()
		{
			TimeSpan ts = this._endDate - this._startDate;
			return ts;
		}

		private DateTime GetMinimumDate()
		{
			DateTime retVal = DateTime.MaxValue;

			if (this._dateLineGraph.DateLines == null)
				return DateTime.MinValue;

			for (int i = 0; i < this._dateLineGraph.DateLines.Count; i++)
			{
				DateLine dateLine = this._dateLineGraph.DateLines[i];

				for (int j = 0; j < dateLine.DatePoints.Count; j++)
				{
					if (DateTime.Compare(dateLine.DatePoints[j].Date, retVal) < 0)
						retVal = dateLine.DatePoints[j].Date;
				}
			}

			if (this._dateLineGraph.DateTrendLine != null && this._dateLineGraph.DateTrendLine.DatePoints != null && this._dateLineGraph.DateTrendLine.DatePoints.Count > 0)
			{
				for (int j = 0; j < this._dateLineGraph.DateTrendLine.DatePoints.Count; j++)
				{
					if (DateTime.Compare(this._dateLineGraph.DateTrendLine.DatePoints[j].Date, retVal) < 0)
						retVal = this._dateLineGraph.DateTrendLine.DatePoints[j].Date;
				}
			}

			if (DateTime.Compare(this._dateLineGraph.StartDate, retVal) < 0) 
				return this._dateLineGraph.StartDate;
			else
				return retVal;
		}

		private DateTime GetMaximumDate()
		{
			DateTime retVal = DateTime.MinValue;

			if (this._dateLineGraph.DateLines == null)
				return DateTime.MinValue;

			for (int i = 0; i < this._dateLineGraph.DateLines.Count; i++)
			{
				DateLine dateLine = this._dateLineGraph.DateLines[i];

				for (int j = 0; j < dateLine.DatePoints.Count; j++)
				{
					if (DateTime.Compare(dateLine.DatePoints[j].Date, retVal) > 0)
						retVal = dateLine.DatePoints[j].Date;
				}
			}

			if (this._dateLineGraph.DateTrendLine != null && this._dateLineGraph.DateTrendLine.DatePoints != null && this._dateLineGraph.DateTrendLine.DatePoints.Count > 0)
			{
				for (int j = 0; j < this._dateLineGraph.DateTrendLine.DatePoints.Count; j++)
				{
					if (DateTime.Compare(this._dateLineGraph.DateTrendLine.DatePoints[j].Date, retVal) > 0)
						retVal = this._dateLineGraph.DateTrendLine.DatePoints[j].Date;
				}
			}

			if (DateTime.Compare(this._dateLineGraph.EndDate, retVal) > 0) 
				return this._dateLineGraph.EndDate;
			else
				return retVal;
		}

		private void SetValues()
		{
			switch(this._dateLineGraph.DateMode)
			{
				case DateMode.Day:
					SetValuesForDaysMode();
					break;

				case DateMode.Weeks:
					SetValuesForWeeksMode();
					break;

				case DateMode.Months:
					SetValuesForMonthsMode();
					break;

				case DateMode.Years:
					SetValuesForYearsMode();
					break;

				default:
					break;

			}// switch
			MapAndAddLines();
			MapAndAddTrendLine();
			MapAndAddPhaseLines();
		}
		private void SetValuesForDaysMode()
		{
			this._dateLineGraph.XAxisTextCollection = new XAxisTextCollection();
			this._dateLineGraph.TotalXAxisIntervals = this._timeSpan.Hours;
			this._dateLineGraph.XAxisIntervalValue = 1;

			// Set X Axis Text
			int hours = 0;
			DateTime date = this._startDate;

			while (hours < this._timeSpan.Hours)
			{
				//this._dateLineGraph.AddXAxisText(days, date.ToShortDateString());
				this._dateLineGraph.AddXAxisText(hours, date.Day.ToString() + "/" + date.TimeOfDay.Hours.ToString());
				date = date.AddHours(24);
				hours += 24;
			}

			//this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.ToShortDateString());
			this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.Day.ToString() + "/" + this._endDate.TimeOfDay.Hours.ToString());
		}
		private void SetValuesForWeeksMode()
		{
			this._dateLineGraph.XAxisTextCollection = new XAxisTextCollection();
			this._dateLineGraph.TotalXAxisIntervals = this._totalDays;
			this._dateLineGraph.XAxisIntervalValue = 1;

			// Set X Axis Text
			int days = 0;
			DateTime date = this._startDate;
			while (days < this._totalDays)
			{
				//this._dateLineGraph.AddXAxisText(days, date.ToShortDateString());
				this._dateLineGraph.AddXAxisText(days, date.Month.ToString() + "/" + date.Day.ToString());
				date = date.AddDays(7);
				days += 7;
			}
			//this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.ToShortDateString());
			this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.Month.ToString() + "/" + this._endDate.Day.ToString());
		}

		private void SetValuesForMonthsMode()
		{
			this._dateLineGraph.XAxisTextCollection = new XAxisTextCollection();
			this._dateLineGraph.TotalXAxisIntervals = this._totalDays / 28;
			this._dateLineGraph.XAxisIntervalValue = 7;

			// Set X Axis Text
			int days = 0;
			DateTime date = this._startDate;

			while (days < this._totalDays)
			{
				//this._dateLineGraph.AddXAxisText(days, date.ToShortDateString());
				this._dateLineGraph.AddXAxisText(days, date.Month.ToString() + "/" + date.Day.ToString());
				date = date.AddDays(28);
				days += 28;
			}

			//this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.ToShortDateString());
			this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.Month.ToString() + "/" + this._endDate.Day.ToString());
		}

		private void SetValuesForYearsMode()
		{
			this._dateLineGraph.XAxisTextCollection = new XAxisTextCollection();
			this._dateLineGraph.TotalXAxisIntervals = this._totalDays / (12 * 28);
			this._dateLineGraph.XAxisIntervalValue = 28;

			// Set X Axis Text
			int days = 0;
			DateTime date = this._startDate;

			while (days < this._totalDays)
			{
				//this._dateLineGraph.AddXAxisText(days, date.ToShortDateString());
				this._dateLineGraph.AddXAxisText(days, date.Month.ToString() + "/" + date.Year.ToString());
				date = date.AddDays(12 * 28);
				days += (12 * 28);
			}
			//this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.ToShortDateString());
			this._dateLineGraph.AddXAxisText(this._totalDays, this._endDate.Month.ToString() + "/" + this._endDate.Year.ToString());
		}

		private void MapAndAddLines()
		{
			this._dateLineGraph.Lines = new LineCollection();

			// Map dates to X Values
			for (int i = 0; i < this._dateLineGraph.DateLines.Count; i++)
			{
				DateLine dateLine = this._dateLineGraph.DateLines[i];
				dateLine.Points = new LinePointCollection(); 
				for (int j = 0; j < dateLine.DatePoints.Count; j++)
				{
					DateLinePoint point = dateLine.DatePoints[j];
					DateTime date = point.Date;
					TimeSpan ts = date - this._startDate;

					if (this._dateMode == DateMode.Day || this._dateMode == DateMode.HalfDay)
						point.XValue = ts.Hours;
					else
						point.XValue = ts.Days;

					dateLine.Points.Add(point);
				}

				this._dateLineGraph.Lines.Add(dateLine);
			}
		}
		private void MapAndAddTrendLine()
		{
			if (this._dateLineGraph.DateTrendLine == null || this._dateLineGraph.DateTrendLine.DatePoints==null || this._dateLineGraph.DateTrendLine.DatePoints.Count<=0)
				return;

			DateLine trendLine = this._dateLineGraph.DateTrendLine;
			trendLine.Points = new LinePointCollection(); 

			for (int j = 0; j < trendLine.DatePoints.Count; j++)
			{
				DateLinePoint point = trendLine.DatePoints[j];
				DateTime date = point.Date;
				TimeSpan ts = date - this._startDate;

				if (this._dateMode == DateMode.Day || this._dateMode == DateMode.HalfDay)
					point.XValue = ts.Hours;
				else
					point.XValue = ts.Days;

				trendLine.Points.Add(point);
			}

			this._dateLineGraph.TrendLine = trendLine;
		}
		private void MapAndAddPhaseLines()
		{
			if (this._dateLineGraph.DatePhaseLines == null || this._dateLineGraph.DatePhaseLines.Count <= 0)
				return;

			for (int i = 0; i < this._dateLineGraph.DatePhaseLines.Count; i++)
			{
				DateXAxisText datePhaseLine = this._dateLineGraph.DatePhaseLines[i];
				XAxisText phaseLine = new XAxisText();

				// Start value
				DateTime startDate = datePhaseLine.StartDate;
				TimeSpan ts = startDate - this._startDate;

				if (this._dateMode == DateMode.Day || this._dateMode == DateMode.HalfDay)
					phaseLine.XValueStart = ts.Hours;
				else
					phaseLine.XValueStart = ts.Days;

				// End value
				DateTime endDate = datePhaseLine.EndDate;
				ts = endDate - this._startDate;

				if (this._dateMode == DateMode.Day || this._dateMode == DateMode.HalfDay)
					phaseLine.XValueEnd = ts.Hours;
				else
					phaseLine.XValueEnd = ts.Days;

				//Text
				phaseLine.Text = datePhaseLine.Text;
				this._dateLineGraph.PhaseLines.Add(phaseLine);
			}
		}

	}// class
}// namespace
