using System;
using System.Collections;
using System.Drawing;
using Microsoft.Tools.Graphs;

namespace Microsoft.Tools.Graphs.Pies
{
	/// <summary>
	/// Summary description for PieGraph.
	/// </summary>
	public class PieGraph : GraphBase
	{
		private PieSliceCollection _pieSliceCollection = null;

		public PieGraph()
		{
			_pieSliceCollection = new PieSliceCollection();
		}

		public PieGraph(Size size) : base(size)
		{
			_pieSliceCollection = new PieSliceCollection();
		}

		public PieGraph(int width, int height) : base(width, height)
		{
			_pieSliceCollection = new PieSliceCollection();
		}

		public PieSliceCollection Slices
		{
			get
			{
				return _pieSliceCollection;
			}
			set
			{
				_pieSliceCollection = value;
			}
		}

	}// class
}// namespace
