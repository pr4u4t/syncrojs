using System;
using System.Drawing;

namespace Microsoft.Tools.Graphs.Bars
{
	/// <summary>
	/// Summary description for MultipleBarSlice.
	/// </summary>
	public class MultipleBarSlice: BarSlice
	{
		private double[] _partialValues = null;
		private Color[] _partialColors = null;
		public MultipleBarSlice()
		{
		}
		public MultipleBarSlice(double[] partialValues, Color[] partialColors)
		{
			_partialColors = partialColors;
			_partialValues = partialValues;
			if (_partialValues != null)
			{
				double totalValue = 0;

				for (int i = 0; i < _partialValues.Length; i++)
					totalValue += _partialValues[i];

				_value = totalValue;
			}
		}
		public MultipleBarSlice(double[] partialValues, Color[] partialColors, string text)
		{
			_partialColors = partialColors;
			_partialValues = partialValues;
			if (_partialValues != null)
			{
				double totalValue = 0;

				for (int i = 0; i < _partialValues.Length; i++)
					totalValue += _partialValues[i];

				_value = totalValue;
			}

			_text = text;
		}
		public double[] PartialValues
		{
			get
			{
				return _partialValues;
			}
			set
			{
				_partialValues = value;
				if (_partialValues != null)
				{
					double totalValue = 0;

					for (int i = 0; i < _partialValues.Length; i++)
						totalValue += _partialValues[i];

					this.Value = totalValue;
				}
			}
		}
		public Color[] PartialColors
		{
			get
			{
				return _partialColors;
			}
			set
			{
				_partialColors = value;
			}
		}
		public double MaxValue
		{
			get
			{
				if (PartialValues == null)
					return 0;

				if (PartialValues.Length == 0)
					return 0;

				double retVal = Double.MinValue;

				for (int i = 0; i < PartialValues.Length; i++)
					if (PartialValues[i] > retVal)
						retVal = PartialValues[i];

				return retVal;
			}
		}
		public double MinValue
		{
			get
			{
				if (PartialValues == null)
					return 0;

				if (PartialValues.Length == 0)
					return 0;

				double retVal = Double.MaxValue;

				for (int i = 0; i < PartialValues.Length; i++)
					if (PartialValues[i] < retVal)
						retVal = PartialValues[i];

				return retVal;
			}
		}

	} // class
}// namespace
