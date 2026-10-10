using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Tools.Graphs.Bars;

namespace SyncroC.Controls
{
  public partial class GraphBars : SyncroC.Controls.SyncroUserControl
  {
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

    public GraphBars()
    {
      InitializeComponent();
    }

    public void drawGraphBars(int[] point, string[] textOnPoint)
    {
      try
      {
        this.pct_Graph.Image = BarGraphPlotter.GetSingleBarGraph
          (
          this.pct_Graph.Size,
          this.Text,
          point,
          textOnPoint
          );
      }
      catch(Exception ex)
      {
        throw ex;
      }
    }
  }
}
