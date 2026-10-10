using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Tools.Graphs;
using Microsoft.Tools.Graphs.Bars;
using Microsoft.Tools.Graphs.Lines;
using Microsoft.Tools.Graphs.Lines.DateLines;
using Microsoft.Tools.Graphs.Pies;
using Microsoft.Tools.Graphs.Legends;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SyncroC.Graph
{
  public partial class GraphLines : SyncroC.Controls.SyncroUserControl
  {
    //ABELLO_FI09/05/2011_1 Aggiunta gestione del Process senza sensori di temperatura
    //ABELLO_FI24/11/2010_2 [PRC] Aggiunta storicizzazione e visualizzazione dell'umidità sopra/sotto
    public enum TypeOfGraphLinesEditor 
    { 
      TRACE_SpeedGraphLinesEditor, 
      TRACE_WrapArmGraphLinesEditor,
      TRACE_WrapArmPositionGraphLinesEditor,
      TRACE_GlueGapGraphLinesEditor,
      TRACE_TensionGraphLinesEditor,
      TRACE_PressureHotSection,
      TRACE_PressureColdSection,
      TRACE_SteamPressure,
      TRACE_SteamShower,
      TRACE_Moisture,
      WrapArmGraphLinesEditor,
      WrapArmPositionGraphLinesEditor, 
      GlueGapGraphLinesEditor, 
      TensionGraphLinesEditor, 
      PressureHotSection, 
      PressureColdSection, 
      SteamPressure, 
      SteamShower 
    };

    private TypeOfGraphLinesEditor myTypeOfGraphLinesEditor = TypeOfGraphLinesEditor.WrapArmGraphLinesEditor;
    [Description("Type Of Graph Lines Editor."), Category("FosberSoft")]
    public TypeOfGraphLinesEditor MyTypeOfGraphLinesEditor
    {
      get { return myTypeOfGraphLinesEditor; }
      set { myTypeOfGraphLinesEditor = value; }
    }

    /// <summary>
    /// Gets or sets the text to be written into the control as a title.
    /// </summary>
    [Description("Gets or sets the text to be written into the control as a title."), Browsable(true),
      DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
      Category("FosberSoft")]
    public override string Text
    {
      get { return base.Text; }
      set
      {
        base.Text = value;
        this.Invalidate();
      }
    }

    public GraphLines()
    {
      InitializeComponent();
    }

    private Color getMyColorGradient()
    {
      Color res = Color.WhiteSmoke;
      switch (this.myTypeOfGraphLinesEditor)
      {
        case TypeOfGraphLinesEditor.WrapArmGraphLinesEditor:
        case TypeOfGraphLinesEditor.TRACE_WrapArmGraphLinesEditor:
          res = Color.Tomato;
          break;
        case TypeOfGraphLinesEditor.GlueGapGraphLinesEditor:
        case TypeOfGraphLinesEditor.TRACE_GlueGapGraphLinesEditor:
          res = Color.LightGreen;
          break;
        case TypeOfGraphLinesEditor.TensionGraphLinesEditor:
        case TypeOfGraphLinesEditor.TRACE_TensionGraphLinesEditor:
          res = Color.Yellow;
          break;
        case TypeOfGraphLinesEditor.PressureColdSection:
        case TypeOfGraphLinesEditor.PressureHotSection:
        case TypeOfGraphLinesEditor.TRACE_PressureColdSection:
        case TypeOfGraphLinesEditor.TRACE_PressureHotSection:
          res = Color.PeachPuff;
          break;
        case TypeOfGraphLinesEditor.SteamShower:
        case TypeOfGraphLinesEditor.SteamPressure:
        case TypeOfGraphLinesEditor.TRACE_SteamShower:
        case TypeOfGraphLinesEditor.TRACE_SteamPressure:
          res = Color.LightBlue;
          break;
        case TypeOfGraphLinesEditor.TRACE_SpeedGraphLinesEditor:
          res = Color.Gray;
          break;
        case TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor:
        case TypeOfGraphLinesEditor.WrapArmPositionGraphLinesEditor:
          res = Color.LightGray;
          break;
        //ABELLO_FI24/11/2010_2 [PRC] Aggiunta storicizzazione e visualizzazione dell'umidità sopra/sotto
        case TypeOfGraphLinesEditor.TRACE_Moisture:
          res = Color.Aquamarine;
          break;
      }
      
      return res;
    }

    public string getMyUnitMeasure()
    {
      string res = "";

      switch (this.myTypeOfGraphLinesEditor)
      {
        case TypeOfGraphLinesEditor.WrapArmGraphLinesEditor:
        case TypeOfGraphLinesEditor.TRACE_WrapArmGraphLinesEditor:
          res = SyncroC.Utility.SyncroConverter.ToString(SyncroC.Utility.UnitMeasure.DegreeCentigrade);
          break;
        case TypeOfGraphLinesEditor.GlueGapGraphLinesEditor:
        case TypeOfGraphLinesEditor.TRACE_GlueGapGraphLinesEditor:
          res = SyncroC.Utility.SyncroConverter.ToString(SyncroC.Utility.UnitMeasure.Micron);
          break;
        case TypeOfGraphLinesEditor.TensionGraphLinesEditor:
        case TypeOfGraphLinesEditor.TRACE_TensionGraphLinesEditor:
          res = SyncroC.Utility.SyncroConverter.ToString(SyncroC.Utility.UnitMeasure.Newton);
          break;
        case TypeOfGraphLinesEditor.PressureColdSection:
        case TypeOfGraphLinesEditor.PressureHotSection:
        case TypeOfGraphLinesEditor.SteamShower:
        case TypeOfGraphLinesEditor.TRACE_PressureColdSection:
        case TypeOfGraphLinesEditor.TRACE_PressureHotSection:
        case TypeOfGraphLinesEditor.TRACE_SteamShower:
        case TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor:
        case TypeOfGraphLinesEditor.WrapArmPositionGraphLinesEditor:
          res = SyncroC.Utility.SyncroConverter.ToString(SyncroC.Utility.UnitMeasure.Percentage);
          break;
        case TypeOfGraphLinesEditor.SteamPressure:
        case TypeOfGraphLinesEditor.TRACE_SteamPressure:
          res = SyncroC.Utility.SyncroConverter.ToString(SyncroC.Utility.UnitMeasure.DeciBar);
          break;
        case TypeOfGraphLinesEditor.TRACE_SpeedGraphLinesEditor:
          res = SyncroC.Utility.SyncroConverter.ToString(SyncroC.Utility.UnitMeasure.MetresPerMinute);
          break;
      }

      return res;
    }

    private bool isATraceGraph()
    {
      return
        (
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_SpeedGraphLinesEditor) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_WrapArmGraphLinesEditor) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_GlueGapGraphLinesEditor) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_TensionGraphLinesEditor) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_PressureHotSection) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_PressureColdSection) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_SteamPressure) ||
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_SteamShower) ||
        //ABELLO_FI24/11/2010_2 [PRC] Aggiunta storicizzazione e visualizzazione dell'umidità sopra/sotto
        (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TRACE_Moisture)
        );
    }

    public void drawMultiLine(Line theLineTrend, Line theLine1, Line theLine2, Line theLine3, Line theLine4, Line theLine5, LegendEntryCollection theLegendEntryCollection, int theMinScaleValue, int theMaxScaleValue, Point pointTohighlight, string theYAxisText)
    {
      this.drawMultiLine(theLineTrend, theLine1, theLine2, theLine3, theLine4, theLine5, theLegendEntryCollection, theMinScaleValue, theMaxScaleValue, pointTohighlight, theYAxisText, DateTime.MinValue, DateTime.MinValue, false);
    }

    public void drawMultiLine(Line theLineTrend, Line theLine1, Line theLine2, Line theLine3, Line theLine4, Line theLine5, LegendEntryCollection theLegendEntryCollection, int theMinScaleValue, int theMaxScaleValue, Point pointTohighlight, string theYAxisText, DateTime from, DateTime to, bool forTrace)
    {
      try
      {
        this.pct_Graph.Image = null;
        LineGraph lg = new LineGraph(this.pct_Graph.Size);

        lg.Color = Color.WhiteSmoke;
        lg.ColorGradient = this.getMyColorGradient();

        if (this.isATraceGraph())
        {
          Line MyLine = null;
          if (theLine1 != null)
            MyLine = theLine1;
          else if (theLine2 != null)
            MyLine = theLine2;
          else if (theLine3 != null)
            MyLine = theLine3;
          else if (theLine4 != null)
            MyLine = theLine4;
          else if (theLine5 != null)
            MyLine = theLine5;

          if (MyLine != null)
          {
            lg.TotalXAxisIntervals = MyLine.Points.Count - 1;
            lg.XAxisIntervalValue = 5;

            if (forTrace)
            {
              for (double i = 0; i < MyLine.Points.Count; i = i + lg.XAxisIntervalValue)
                lg.AddXAxisText(i, from.AddMinutes(i).ToLocalTime().ToString("HH\":\"mm"));
            }
            else
            {
              for (double i = 0; i < MyLine.Points.Count; i = i + lg.XAxisIntervalValue)
              {
                if (i == 0)
                  lg.AddXAxisText(i, from.ToLocalTime().ToString("HH\":\"mm"));
                else if (i == (MyLine.Points.Count - 1) / 2)
                  lg.AddXAxisText(i, from.AddMinutes((to - from).Minutes / 2).ToLocalTime().ToString("HH\":\"mm"));
                else if (i == MyLine.Points.Count - 1)
                  lg.AddXAxisText(i, to.ToLocalTime().ToString("HH\":\"mm"));
                else
                  lg.AddXAxisText(i, "");
              }
            }
          }
          else
          {
            //questo non dovrebbe mai servire, ma se si passa di qui ci si pianta
            lg.TotalXAxisIntervals = 400;
            lg.XAxisIntervalValue = 10;
            lg.AddXAxisText(50, "50");
            lg.AddXAxisText(100, "100");
            lg.AddXAxisText(150, "150");
            lg.AddXAxisText(200, "200");
            lg.AddXAxisText(250, "250");
            lg.AddXAxisText(300, "300");
            lg.AddXAxisText(350, "350");
            lg.AddXAxisText(400, "400");
          }
        }
        else
        {
          lg.TotalXAxisIntervals = 400;
          lg.XAxisIntervalValue = 10;
          lg.AddXAxisText(50, "50");
          lg.AddXAxisText(100, "100");
          lg.AddXAxisText(150, "150");
          lg.AddXAxisText(200, "200");
          lg.AddXAxisText(250, "250");
          lg.AddXAxisText(300, "300");
          lg.AddXAxisText(350, "350");
          lg.AddXAxisText(400, "400");
        }

        if (theMinScaleValue != Int32.MinValue)
          lg.MinScaleValue = theMinScaleValue;

        if (theMaxScaleValue != Int32.MaxValue)
          lg.MaxScaleValue = theMaxScaleValue;

        lg.Text = base.Text;

        if (this.myTypeOfGraphLinesEditor == TypeOfGraphLinesEditor.TensionGraphLinesEditor)
          lg.Text += " (" + DBManager.getDBManager().lineConfig.PRCConfig.Header.StandardRollWidthForCalculateTension_UDmm + ")";

        lg.YAxisText = theYAxisText;

        lg.PointTohighlight = pointTohighlight;

        if (theLine1 != null)
          lg.Lines.Add(theLine1);
        if (theLine2 != null)
          lg.Lines.Add(theLine2);
        if (theLine3 != null)
          lg.Lines.Add(theLine3);
        if (theLine4 != null)
          lg.Lines.Add(theLine4);
        if (theLine5 != null)
          lg.Lines.Add(theLine5);
        if (theLineTrend != null)
          lg.TrendLine = theLineTrend;

        if (lg.Lines.Count != 0)
        {
          if (theLegendEntryCollection != null)
          {
            Legend legend = new Legend(this.pct_Graph.Width, this.pct_Graph.Height / 3);
            legend.Text = "";
            //ABELLO questo è per tappare un Bug sul calcolo delle areee
            if (theLegendEntryCollection.Count == 3)
              legend.ColumnCount = theLegendEntryCollection.Count;
            legend.LegendEntryCollection = theLegendEntryCollection;
            this.pct_Graph.Image = GraphRenderer.DrawGraphAndLegend(lg, legend, this.pct_Graph.Size);
          }
          else
            this.pct_Graph.Image = GraphRenderer.DrawGraph(lg);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void drawLine(Line theLine, int theMinScaleValue, int theMaxScaleValue, Point pointTohighlight, string theYAxisText)
    {
      this.drawLine(theLine, theMinScaleValue, theMaxScaleValue, pointTohighlight, theYAxisText, DateTime.MinValue, DateTime.MinValue, false);
    }

    public void drawLine(Line theLine, int theMinScaleValue, int theMaxScaleValue, Point pointTohighlight, string theYAxisText, DateTime from, DateTime to, bool forTrace)
    {
      try
      {
        this.pct_Graph.Image = null;
        LineGraph lg = new LineGraph(this.pct_Graph.Size);

        lg.Color = Color.WhiteSmoke;
        lg.ColorGradient = this.getMyColorGradient();

        if (this.isATraceGraph())
        {
          lg.TotalXAxisIntervals = theLine.Points.Count - 1;
          lg.XAxisIntervalValue = 5;

          //for (double i = 0; i < theLine.Points.Count; i = i + lg.XAxisIntervalValue)
          //  lg.AddXAxisText(i, (from != DateTime.MinValue) ? (from.AddMinutes(i).ToLocalTime().ToString("H\":\"m")) : "");

          if (forTrace)
          {
            for (double i = 0; i < theLine.Points.Count; i = i + lg.XAxisIntervalValue)
              lg.AddXAxisText(i, from.AddMinutes(i).ToLocalTime().ToString("HH\":\"mm"));
          }
          else
          {
            for (double i = 0; i < theLine.Points.Count; i = i + lg.XAxisIntervalValue)
            {
              if (i == 0)
                lg.AddXAxisText(i, from.ToLocalTime().ToString("HH\":\"mm"));
              else if (i == (theLine.Points.Count - 1) / 2)
                lg.AddXAxisText(i, from.AddMinutes((to - from).Minutes / 2).ToLocalTime().ToString("HH\":\"mm"));
              else if (i == theLine.Points.Count - 1)
                lg.AddXAxisText(i, to.ToLocalTime().ToString("HH\":\"mm"));
              else
                lg.AddXAxisText(i, "");
            }
          }
        }
        else
        {
          lg.TotalXAxisIntervals = 400;
          lg.XAxisIntervalValue = 10;
          lg.AddXAxisText(50, "50");
          lg.AddXAxisText(100, "100");
          lg.AddXAxisText(150, "150");
          lg.AddXAxisText(200, "200");
          lg.AddXAxisText(250, "250");
          lg.AddXAxisText(300, "300");
          lg.AddXAxisText(350, "350");
          lg.AddXAxisText(400, "400");
        }
        lg.MinScaleValue = theMinScaleValue;
        lg.MaxScaleValue = theMaxScaleValue;
        lg.Text = base.Text;
        lg.YAxisText = theYAxisText;

        lg.PointTohighlight = pointTohighlight;

        lg.Lines.Add(theLine);

        this.pct_Graph.Image = GraphRenderer.DrawGraph(lg);

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

  }

  public class Pair<T, U>
  {
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
      this.First = first;
      this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
  };

}
