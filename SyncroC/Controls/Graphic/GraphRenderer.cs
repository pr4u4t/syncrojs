using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Microsoft.Tools.Graphs.Legends;
using Microsoft.Tools.Graphs.Bars;
using Microsoft.Tools.Graphs.Lines;
using Microsoft.Tools.Graphs.Lines.DateLines;
using Microsoft.Tools.Graphs.Pies;

namespace Microsoft.Tools.Graphs
{
	/// <summary>
	/// Summary description for GraphRendered.
	/// </summary>
	public class GraphRenderer
	{
		public GraphRenderer()
		{
		}

		public static Image DrawLegend(Legend legend)
		{
			LegendRenderer lr = new LegendRenderer();
			return lr.DrawLegend(legend);
		}
		public static Image DrawGraph(GraphBase graph)
		{
			Image graphImage = null;

			if (graph.GetType().Name == "BarGraph")
				graphImage = GraphRenderer.DrawGraph((BarGraph)graph);
			else if (graph.GetType().Name == "PieGraph")
				graphImage = GraphRenderer.DrawGraph((PieGraph)graph);
			else if (graph.GetType().Name == "DateLineGraph")
				graphImage = GraphRenderer.DrawGraph((DateLineGraph)graph);
			else
				graphImage = GraphRenderer.DrawGraph((LineGraph)graph);

			return graphImage;
		}

		public static Image DrawGraph(BarGraph barGraph)
		{
			BarGraphRenderer bgr = new BarGraphRenderer();
			return bgr.DrawGraph(barGraph);
		}

		public static Image DrawGraph(LineGraph lineGraph)
		{
			LineGraphRenderer lgr = new LineGraphRenderer();
			return lgr.DrawGraph(lineGraph);
		}

		public static Image DrawGraph(DateLineGraph dateLineGraph)
		{
			DateLineGraphRenderer dlgr = new DateLineGraphRenderer();
			return dlgr.DrawGraph(dateLineGraph);
		}
		public static Image DrawGraph(PieGraph pieGraph)
		{
			PieGraphRenderer pgr = new PieGraphRenderer();
			return pgr.DrawGraph(pieGraph);
		}
		public static Image JoinBitMaps(Image graph, Image legend)
		{
			int width = graph.Width;
			if(legend.Width > graph.Width)
				width = legend.Width;
			Size totalSize = new Size(width, graph.Height + legend.Height);
			return JoinBitMaps(graph, legend, totalSize);
		}

		public static Image JoinBitMaps(Image graph, Image legend, Size totalSize)
		{
			Bitmap bMap = new Bitmap(totalSize.Width, totalSize.Height, PixelFormat.Format64bppPArgb);
			Graphics g = Graphics.FromImage(bMap);

			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.Clear(Color.White);

			Rectangle graphRect = new Rectangle(0, 0, totalSize.Width, (int)(totalSize.Height * 0.92));
			Rectangle legendRect = new Rectangle(0, graphRect.Height, totalSize.Width, totalSize.Height - graphRect.Height);
			if (legendRect.Height > legend.Height)
			{
				legendRect = new Rectangle(0, totalSize.Height - legend.Height, totalSize.Width, legend.Height);
				graphRect = new Rectangle(0, 0, totalSize.Width, (int)(totalSize.Height - legendRect.Height));
			}
			g.DrawImage(graph, graphRect);
			g.DrawImage(legend, legendRect);
			Pen gPen = new Pen(Color.Gray, (float)0.3);
			g.DrawRectangle(gPen, (int)0, (int)0, (int)totalSize.Width - 1, (int)totalSize.Height - 1);
			return bMap;
		}

		public static Image DrawGraphAndLegend(GraphBase graph, Legend legend)
		{
			Image graphImage = null;

			if (graph.GetType().Name == "BarGraph")
				graphImage = GraphRenderer.DrawGraph((BarGraph)graph);
			else
				graphImage = GraphRenderer.DrawGraph((LineGraph)graph);

			Image legendImage = GraphRenderer.DrawLegend(legend);
			return GraphRenderer.JoinBitMaps(graphImage, legendImage);
		}

		public static Image DrawGraphAndLegend(GraphBase graph, Legend legend, Size totalSize)
		{
			Image graphImage = null;
			Rectangle graphRect = new Rectangle(0, 0, totalSize.Width, (int)(totalSize.Height * 0.92));
			Rectangle legendRect = new Rectangle(0, graphRect.Height, totalSize.Width, totalSize.Height - graphRect.Height);

			if (legendRect.Height > legend.Size.Height)
			{
				legendRect = new Rectangle(0, totalSize.Height - legend.Size.Height, totalSize.Width, legend.Size.Height);
				graphRect = new Rectangle(0, 0, totalSize.Width, (int)(totalSize.Height - legendRect.Height));
			}

			graph.Size = graphRect.Size;
			legend.Size = legendRect.Size;

			if (graph.GetType().Name == "BarGraph") 
				graphImage = GraphRenderer.DrawGraph((BarGraph)graph);
			else if (graph.GetType().Name == "PieGraph")
				graphImage = GraphRenderer.DrawGraph((PieGraph)graph);
			else if (graph.GetType().Name == "DateLineGraph")
				graphImage = GraphRenderer.DrawGraph((DateLineGraph)graph);
			else
				graphImage = GraphRenderer.DrawGraph((LineGraph)graph);

			Image legendImage = GraphRenderer.DrawLegend(legend);
			return GraphRenderer.JoinBitMaps(graphImage, legendImage, totalSize);
		}
        
	}// class
}// namespace
