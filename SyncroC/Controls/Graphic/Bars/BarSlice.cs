using System;
using System.Drawing;

namespace Microsoft.Tools.Graphs.Bars
{
	/// <summary>
	/// Summary description for BarSlice.
	/// </summary>
	public class BarSlice
	{
        protected Color _color;
		protected Color _colorGradient;
        protected string _text;
        protected double _value;
        protected int _maxWidth = 15;

		public BarSlice()
		{
		}
		public BarSlice(double value, Color color)
		{
            _value = value;
            _color = color;
        }
		public BarSlice(double value, Color color, Color colorGradient)
		{
			_value = value;
			_color = color;
			_colorGradient = colorGradient;
		}
		public BarSlice(double value, Color color, string text)
		{
            _value = value;
            _color = color;
            _text = text;
        }
		public BarSlice(double value, Color color, Color colorGradient, string text)
		{
			_value = value;
			_color = color;
			_text = text;
			_colorGradient = colorGradient;
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
		public Color ColorGradient
		{
			get
			{
				if (_colorGradient == Color.Empty)
					return _color;
				else
					return _colorGradient;
			}
			set
			{
				_colorGradient = value;
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
		public double Value
		{
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public int MaxWidth
        {
            get
            {
                return _maxWidth;
            }
            set
            {
                _maxWidth = value;
            }
        }

	} // class
}// namespace
