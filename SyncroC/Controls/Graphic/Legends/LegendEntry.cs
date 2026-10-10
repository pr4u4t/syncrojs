using System;
using System.Drawing;

namespace Microsoft.Tools.Graphs.Legends
{
	/// <summary>
	/// Summary description for Legend.
	/// </summary>
	public class LegendEntry
	{
        private Color _color;
        private string _text;

        public LegendEntry ()
        {
        }

        public LegendEntry (Color color, string text)
		{
            _color = color;
            _text = text;
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
