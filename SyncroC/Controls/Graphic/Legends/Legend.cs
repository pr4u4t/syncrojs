using System;
using System.Drawing;

namespace Microsoft.Tools.Graphs.Legends
{
	/// <summary>
	/// Summary description for Legend.
	/// </summary>
	public class Legend
	{
        private LegendEntryCollection _legendEntryCollection = null;
        private int _border = 2;
        private int _columnCount = 0;
        private int _columnGap = 2;
        private Size _size = Size.Empty;
        private Color _color = Color.White;
        private string _text = "Legend";
		private double _factorWidth = 5;
		private double _factorHeight = 2;

		public Legend(Size size)
		{
            _size = size;
			_legendEntryCollection = new LegendEntryCollection();
		}

        public Legend (int width, int height)
        {
            Size size = new Size (width, height);
            _size = size;
			_legendEntryCollection = new LegendEntryCollection();
		}

        public LegendEntryCollection LegendEntryCollection
        {
            get
            {
                return _legendEntryCollection;
            }
            set
            {
                _legendEntryCollection = value;
            }
        }

        public int Border
        {
            get
            {
                return _border;
            }
            set
            {
                _border = value;
            }
        }

        public int ColumnCount
        {
            get
            {
				try
				{
					if (_columnCount == 0)
					{
						if (Size == Size.Empty || LegendEntryCollection == null)
						{
							return 1;
						}
						else if (LegendEntryCollection.Count == 0)
						{
							return 1;
						}
						else
						{
							double area = Size.Width * Size.Height / LegendEntryCollection.Count;
							double x = System.Math.Sqrt(area / (_factorWidth * _factorHeight));

							if (x < 0.0)
								x = x * (-1);

							return (int)(Size.Width / (x * _factorWidth));
						}
					}
					else
						return _columnCount;
				}
				catch
				{
					return 1;
				}
            }
            set
            {
                _columnCount = value;
            }
        }

        public int ColumnGap
        {
            get
            {
                return _columnGap;
            }
            set
            {
                _columnGap = value;
            }
        }
    
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
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
