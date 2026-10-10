using System;
using System.Drawing;
using Microsoft.Tools.Graphs;

namespace Microsoft.Tools.Graphs.Bars
{
	public enum MultiBarDisplayStyle
	{
		SingleBar = 1,
		SeparateBars = 2
	}

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class BarGraph: GridGraphBase
	{
        private BarSliceCollection _barSliceCollection = null;
		private int _barGap = 2;
        private int _maxBarSliceWidth = 15;
        private bool _showBarSliceText = false;
		private double _cutOff = Double.MinValue;
		private MultiBarDisplayStyle _multiBarDisplayStyle = MultiBarDisplayStyle.SingleBar;


        public BarGraph () : base()
		{
			_barSliceCollection = new BarSliceCollection();
		}
		
		public BarGraph (Size size) : base(size)
		{
			_barSliceCollection = new BarSliceCollection();
		}

        public BarGraph (int width, int height) : base(width, height)
        {
			_barSliceCollection = new BarSliceCollection();
		}

        public BarSliceCollection BarSliceCollection
        {
            get
            {
                return _barSliceCollection;
            }
			set
			{
				_barSliceCollection = value;
			}
        }

		public override Size Size
		{
			get
			{
				if (base.Size == Size.Empty)
				{
					if (_barSliceCollection != null && _barSliceCollection.Count > 0)
					{
						return new Size(500, (_maxBarSliceWidth + _barGap) * (_barSliceCollection.Count + 1) + 20);
					}
					else
						return new Size(500, 500);
				}
				else
					return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}
		
		public int BarGap
        {
            get
            {
                return _barGap;
            }
            set
            {
                _barGap = value;
            }
        }

        public int MaxBarSliceWidth
        {
            get
            {
                return _maxBarSliceWidth;
            }
            set
            {
                _maxBarSliceWidth = value;
            }
        }

        public bool ShowBarSliceText
        {
            get
            {
                return _showBarSliceText;
            }
            set
            {
                _showBarSliceText = value;
            }
        }

		public double CutOff
		{
			get
			{
				return _cutOff;
			}
			set
			{
				_cutOff = value;
			}
		}

		public MultiBarDisplayStyle MultiBarDisplayStyle
		{
			get
			{
				return _multiBarDisplayStyle;
			}
			set
			{
				_multiBarDisplayStyle = value;
			}
		}

	} // class
}// namespace
