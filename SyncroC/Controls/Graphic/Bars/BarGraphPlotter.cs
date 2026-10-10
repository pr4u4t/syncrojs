using System;
using System.Drawing;
using System.Collections;
using Microsoft.Tools.Graphs;
using Microsoft.Tools.Graphs.Legends;

namespace Microsoft.Tools.Graphs.Bars
{
	/// <summary>
	/// Summary description for BarGraphPlotter.
	/// </summary>
	public class BarGraphPlotter
	{
		private static Random rand = new Random(100);
		private static BarGraph bg = null;
		private static Legend legend = null;

		public BarGraphPlotter()
		{
		}

		public static Image GetSingleBarGraph(Size size, string graphTitle, int[] values, string[] barTexts)
		{
			rand = new Random(100); 
			bg = SetUpGraph(size, graphTitle);
			if(barTexts==null)
				barTexts = new string[values.Length];

			BarSliceCollection bsc = new BarSliceCollection();
			for (int i = 0; i < values.Length; i++)
				bsc.Add(new BarSlice(values[i], GetRandomColor(), barTexts[i]));
			bg.BarSliceCollection = bsc;
			return GraphRenderer.DrawGraph(bg);
		}

		public static BarGraph GetMultipleBarGraph(Size size, string graphTitle, ArrayList values, string[] barTexts)
		{
			BarGraph bg = SetUpGraph(size, graphTitle);
			return bg;

		}

		public static Image GetLegend(Size size, string[] entries, int columnCount)
		{
			rand = new Random(100);
			legend = SetUpLegend(size, columnCount);
            LegendEntryCollection lec = new LegendEntryCollection ();
			for (int i = 0; i < entries.Length; i++)
				lec.Add(new LegendEntry(GetRandomColor(), entries[i]));
			legend.LegendEntryCollection = lec;
			LegendRenderer lr = new LegendRenderer();
			return lr.DrawLegend(legend);
		}

		private static BarGraph SetUpGraph(Size size, string graphTitle)
		{
			BarGraph bg = new BarGraph(size);

			bg.Text = graphTitle;
			bg.Color = Color.White;
			bg.Alignment = Alignment.VerticalBottom;
			bg.MaxBarSliceWidth = 15;
			bg.Border = 40;
			bg.ShowGrid = true;
			bg.ShowBarSliceText = true;
			return bg;
		}

		private static Legend SetUpLegend(Size size, int columnCount)
		{
			Legend l = new Legend(size);
			l.ColumnCount = columnCount;
			return l;
		}

		private static Color GetRandomColor()
		{
			byte r, g, b;
			r = (byte)GetRange(0, 255);
			g = (byte)GetRange(0, 255);
			b = (byte)GetRange(0, 255);
			return Color.FromArgb(r, g, b);
		}

		private static int GetRange(int nMin, int nMax)
		{
			// Swap max and min if min > max
			if (nMin > nMax)
			{
				int nTemp = nMin;

				nMin = nMax;
				nMax = nTemp;
			}

			// Add 1 to max because rand.Next() returns min <= value < max...
			// Uh, don't do this if Max is Int32.MaxValue. :P
			if (nMax != Int32.MaxValue)
				++nMax;

			return rand.Next(nMin, nMax);
		}

	}// class
}// namespace
