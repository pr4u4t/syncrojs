using System;
using System.Collections;
using System.Drawing;
using Microsoft.Tools.Graphs;

namespace Microsoft.Tools.Graphs.Lines
{
	/// <summary>
	/// Summary description for LineGraph.
	/// </summary>
	public class LineGraph: GridGraphBase
	{
		private LineCollection _lineCollection = null;
		private Line _trendLine = null;
		private double _totalXAxisIntervals = 1.0;
		private double _xAxisIntervalValue = 1.0;
		private XAxisTextCollection _xAxisTextCollection = null;
		private XAxisTextCollection _phaseLines = null;
		private bool _showProjectedTrend = false;

    private string yAxisText = "";
    public string YAxisText
    {
      get { return yAxisText; }
      set { yAxisText = value; }
    }

    private Point pointTohighlight = new Point();
    public Point PointTohighlight
    {
      get { return pointTohighlight; }
      set { pointTohighlight = value; }
    }

		public LineGraph() : base()
		{
			_lineCollection = new LineCollection();
			_xAxisTextCollection = new XAxisTextCollection();
			_phaseLines = new XAxisTextCollection(); 
			_trendLine = new Line();
			_trendLine.Width = 2.0F;
			MarginForTextOnAxis = 10;
		}

        public LineGraph (Size size) : base(size)
		{
			_lineCollection = new LineCollection();
			_xAxisTextCollection = new XAxisTextCollection();
			_phaseLines = new XAxisTextCollection();
			_trendLine = new Line();
			_trendLine.Width = 2.0F;
			MarginForTextOnAxis = 10;
		}

		public LineGraph(int width, int height) : base(width, height)
        {
			_lineCollection = new LineCollection();
			_xAxisTextCollection = new XAxisTextCollection();
			_phaseLines = new XAxisTextCollection();
			_trendLine = new Line();
			_trendLine.Width = 2.0F;
			MarginForTextOnAxis = 10;
		}

		public override Size Size
		{
			get
			{
				if (base.Size == Size.Empty)
					return new Size(500, 500);
				else
					return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		public LineCollection Lines
		{
			get
			{
				return _lineCollection;
			}
			set
			{
				_lineCollection = value;
			}
		}


		public XAxisTextCollection XAxisTextCollection
		{
			get
			{
				return _xAxisTextCollection;
			}
			set
			{
				_xAxisTextCollection = value;
			}
		}

		public XAxisTextCollection PhaseLines
		{
			get
			{
				return _phaseLines;
			}
			set
			{
				_phaseLines = value;
			}
		}

		public Line TrendLine
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

		public double TotalXAxisIntervals
		{
			get
			{
				return _totalXAxisIntervals;
			}
			set
			{
				_totalXAxisIntervals = value;
			}
		}

		public double XAxisIntervalValue
		{
			get
			{
				return _xAxisIntervalValue;
			}
			set
			{
				_xAxisIntervalValue = value;
			}
		}

		public bool ShowProjectedTrend
		{
			get
			{
				return _showProjectedTrend;
			}
			set
			{
				_showProjectedTrend = value;
			}
		}

		public void AddXAxisText(double xValue, string text)
		{
			XAxisTextCollection.Add(new XAxisText(xValue, text));
		}

		public void AddXAxisText(double xValueStart, double xValueEnd, string text)
		{
			XAxisTextCollection.Add(new XAxisText(xValueStart, xValueEnd, text));
		}


	}// class
}// namespace
