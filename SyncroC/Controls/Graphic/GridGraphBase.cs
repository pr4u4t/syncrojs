using System;
using System.Drawing;
using Microsoft.Tools.Graphs.Lines;

namespace Microsoft.Tools.Graphs
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public abstract class GridGraphBase : GraphBase
	{
		private int _minScaleValue = 0;
		private int _maxScaleValue;
		private bool _honorScale = false;
		private bool _showGrid = true;
		private int _gridSpacingValue = 0;
		private int _marginForTextOnAxis = 10;
		public GridGraphBase()
		{
			MarginForTextOnAxis = 20;
		}
		public GridGraphBase(Size size) : base(size)
		{
			MarginForTextOnAxis = 20;
		}
		public GridGraphBase(int width, int height) : base(width, height)
		{
			MarginForTextOnAxis = 20;
		}
		public int MinScaleValue
		{
			get
			{
				return _minScaleValue;
			}
			set
			{
				_minScaleValue = value;
				this.HonorScale = true;
			}
		}
		public int MaxScaleValue
		{
			get
			{
				return _maxScaleValue;
			}
			set
			{
				_maxScaleValue = value;
				this.HonorScale = true;
			}
		}
		public bool HonorScale
		{
			get
			{
				return _honorScale;
			}
			set
			{
				_honorScale = value;
				if (!_honorScale)
					_minScaleValue = 0;
			}
		}
		public bool ShowGrid
		{
			get
			{
				return _showGrid;
			}
			set
			{
				_showGrid = value;
			}
		}
		public int GridSpacingValue
		{
			get
			{
				return _gridSpacingValue;
			}
			set
			{
				_gridSpacingValue = value;
			}
		}
		public int MarginForTextOnAxis
		{
			get
			{
				return _marginForTextOnAxis;
			}
			set
			{
				_marginForTextOnAxis = value;
			}
		}
	} // class
}// namespace
