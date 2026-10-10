using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Tools.Graphs.Pies;

namespace SyncroC.Controls
{
  public partial class GraphPies : SyncroC.Controls.SyncroUserControl
  {
    public GraphPies()
    {
      InitializeComponent();
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

    public void drawGraphPies(int[] point, Color[] colorPoint)
    {
      try
      {
        PieGraph pg = new PieGraph(this.pct_Graph.Size);
        pg.Color = Color.White;
        pg.ColorGradient = Color.RosyBrown;
        pg.Text = this.Text;

        for (int i = 0; i < point.Length; i++)
          pg.Slices.Add(new PieSlice(point[i], colorPoint[i]));
        
        this.pct_Graph.Image = Microsoft.Tools.Graphs.GraphRenderer.DrawGraph(pg);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
