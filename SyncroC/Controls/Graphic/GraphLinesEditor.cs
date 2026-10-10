using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Tools.Graphs.Lines;
using Microsoft.Tools.Graphs.Legends;

namespace SyncroC.Graph
{
  public partial class GraphLinesEditor : UserControl
  {
    private int MAX_LISTA_COUNT = 9;

    private bool mySuspendLayout = false;
    [Browsable(false)]
    public bool MySuspendLayout
    {
      get { return mySuspendLayout; }
      set 
      { 
        mySuspendLayout = value; 
        if(!value)
          this.myRefresh();
      }
    }

    [Description("Title Graph Lines Editor."), Category("FosberSoft")]
    public string Title
    {
      get { return this.graphLines.Text; }
      set
      {
        this.graphLines.Text = value;
        this.myRefresh();
      }
    }

    [Description("Type Of Graph Lines Editor."), Category("FosberSoft")]
    public SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor MyTypeOfGraphLinesEditor
    {
      get { return this.graphLines.MyTypeOfGraphLinesEditor; }
      set 
      { 
        this.graphLines.MyTypeOfGraphLinesEditor = value;
        this.lbl_Value.Text = this.graphLines.getMyUnitMeasure();

        this.myRefresh();
      }
    }

    private bool readOnly = false;
    [Description("Determine if the control is readonly."), Category("FosberSoft")]
    public bool ReadOnly
    {
      get { return readOnly; }
      set
      {
        readOnly = value;

        foreach (Control c in this.tableLayoutPanel2.Controls)
        {
          if (c is SyncroC.Controls.TitledNumericUpDown)
          {
            ((SyncroC.Controls.TitledNumericUpDown)c).ReadOnly = readOnly;
            ((SyncroC.Controls.TitledNumericUpDown)c).TabStop = !readOnly;
          }
        }

        this.lbl_Speed.Invalidate();
        this.lbl_Value.Invalidate();
      }
    }

    private decimal maximum = 9999;
    [Description("Maximum Value."), Category("FosberSoft")]
    public decimal Maximum
    {
      get { return maximum; }
      set 
      {
        maximum = value;

        for (int a = 0; a < MAX_LISTA_COUNT; a++)
          this.getControl("tnud_value_" + a).Maximum = value;
      }
    }

    private decimal minimum = 1;
    [Description("Minimum Value."), Category("FosberSoft")]
    public decimal Minimum
    {
      get { return minimum; }
      set 
      {
        minimum = value;

        for (int a = 0; a < MAX_LISTA_COUNT; a++)
          this.getControl("tnud_value_" + a).Minimum = value;
      }
    }

    public GraphLinesEditor()
    {
      InitializeComponent();
    }

    private string qualityText = "";
    public void PrintNoDisplay(string theQualityText)
    {
      this.qualityText = theQualityText;
      System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
      doc.PrintController = new System.Drawing.Printing.StandardPrintController();
      doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
      doc.DefaultPageSettings.Landscape = true;
      doc.Print();
    }

    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
    void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      //Titolo
      e.Graphics.DrawString(this.qualityText, this.lbl_Speed.Font, Brushes.Black, 0, this.lbl_Speed.Font.Height); 
   
      //Grafico
      Graphics mygraphics = this.CreateGraphics();
      Size s = this.Size;
      Bitmap memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
      Graphics memoryGraphics = Graphics.FromImage(memoryImage);
      IntPtr dc1 = mygraphics.GetHdc();
      IntPtr dc2 = memoryGraphics.GetHdc();
      BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
      mygraphics.ReleaseHdc(dc1);
      memoryGraphics.ReleaseHdc(dc2);

      e.Graphics.DrawImage(memoryImage, 0, this.lbl_Speed.Font.Height * 5);
    }

    private int userCorrection = 0;
    private int warpCorrection = 0;
    Point pointTohighlight = new Point(-1, -1);

    public void fillMultiData(List<SyncroC.Graph.Pair<int, int>> listaStd, int theUserCorrection, int theWarpCorrection, Point thePointTohighlight)
    {
      this.fillMultiData(listaStd, theUserCorrection, theWarpCorrection, thePointTohighlight, -1);
    }

    private int splicerRollWidth_UDmm = -1;
    public void fillMultiData(List<SyncroC.Graph.Pair<int, int>> listaStd, int theUserCorrection, int theWarpCorrection, Point thePointTohighlight, int splicerRollWidth_UDmm)
    {
      this.pointTohighlight = thePointTohighlight;
      this.userCorrection = theUserCorrection;
      this.warpCorrection = theWarpCorrection;
      this.splicerRollWidth_UDmm = splicerRollWidth_UDmm;
      this.fillData(listaStd);
    }

    public void fillData(List<Pair<int, int>> theListaOri)
    {
      List<Pair<int, int>> theLista = new List<Pair<int, int>>(theListaOri);
      if (theLista.Count == 0)
        theLista.Add(new Pair<int, int>(0, (int)((this.minimum + this.maximum) / 2)));

      for (int b = 0; b < this.MAX_LISTA_COUNT; b++)
      {
        this.getControl("tnud_Speed_" + b.ToString()).Tag = "";
        this.getControl("tnud_value_" + b.ToString()).Tag = "";
      }

      int index = theLista.Count;
      while (theLista.Count < this.MAX_LISTA_COUNT)
      {
        theLista.Add(theLista[theLista.Count-1]);
        this.getControl("tnud_Speed_" + index.ToString()).Tag = "ABELLO";
        this.getControl("tnud_value_" + index.ToString()).Tag = "ABELLO";
        index++;
      }

      for (int a = 0; a < theLista.Count; a++)
      {
        if (a >= this.MAX_LISTA_COUNT)
          break;

        SyncroC.Controls.TitledNumericUpDown mySpeedControl = this.getControl("tnud_Speed_" + a);
        SyncroC.Controls.TitledNumericUpDown myValueControl = this.getControl("tnud_value_" + a);

        if (mySpeedControl != null)
          mySpeedControl.Value = theLista[a].First.ToString();

        if (myValueControl != null)
          myValueControl.Value = theLista[a].Second.ToString();
      }

      this.myRefresh();
    }

    private void myRefresh()
    {
      if (this.readOnly)
        if (this.mySuspendLayout)
          return;

      LegendEntryCollection theLegendEntryCollection = null;

      Line lineStd = new Line(Color.Blue);
      lineStd.Width = 2;

      bool isWarpCorrectionOrUserCorrection = ((warpCorrection != 0) || (userCorrection != 0));

      List<Pair<int, int>> listaStd = this.getData();
      List<Pair<int, int>> listaStdNOPerBobina = null;

      //Tensione carta
      if ((this.splicerRollWidth_UDmm > 0) && (DBManager.getDBManager().lineConfig.PRCConfig.Header.StandardRollWidthForCalculateTension_UDmm != this.splicerRollWidth_UDmm))
      {
        listaStdNOPerBobina = this.getData();
        double value = 0.0;
        foreach (SyncroC.Graph.Pair<int, int> a in listaStd)
        {
          value = (((double)a.Second / (double)DBManager.getDBManager().lineConfig.PRCConfig.Header.StandardRollWidthForCalculateTension_UDmm) * (double)splicerRollWidth_UDmm);
          a.Second = (int)value;
        }
      }

      listaStd = this.controllaMAXeMIN(listaStd,0);

      for (int a = 0; a < listaStd.Count; a++)
      {
        int value = listaStd[a].Second;

        if (a == 0)
        {
          if (listaStd[a].First != 0)
          {
            if (isWarpCorrectionOrUserCorrection)
              lineStd.AddPoint(0, value, "");
            else
              lineStd.AddPoint(0, value, value.ToString());
          }
        }

        if (isWarpCorrectionOrUserCorrection)
          lineStd.AddPoint(listaStd[a].First, value, "");
        else
          lineStd.AddPoint(listaStd[a].First, value, value.ToString());

        if (a == (listaStd.Count - 1))
        {
          if (listaStd[a].First != 400)
          {
            if (isWarpCorrectionOrUserCorrection)
              lineStd.AddPoint(400, value, "");
            else
              lineStd.AddPoint(400, value, value.ToString());
          }
        }
      }

      Line lineUserCorrection = null;
      if (userCorrection != 0)
      {
        lineUserCorrection = new Line(Color.Lime);
        lineUserCorrection.Width = 2;

        List<Pair<int, int>> listaUserCorrection = this.controllaMAXeMIN(listaStd, userCorrection);

        for (int a = 0; a < listaUserCorrection.Count; a++)
        {
          int value = listaUserCorrection[a].Second;

          if (a == 0)
          {
            if (listaUserCorrection[a].First != 0)
              lineUserCorrection.AddPoint(0, value, "");
          }

          lineUserCorrection.AddPoint(listaUserCorrection[a].First, value, "");

          if (a == (listaUserCorrection.Count - 1))
          {
            if (listaUserCorrection[a].First != 400)
              lineUserCorrection.AddPoint(400, value, "");
          }
        }
      }

      Line lineWarpCorrection = null;
      if (warpCorrection != 0)
      {
        lineWarpCorrection = new Line(Color.Gold);
        lineWarpCorrection.Width = 2;

        List<Pair<int, int>> listaWarpCorrection = this.controllaMAXeMIN(listaStd, warpCorrection);

        for (int a = 0; a < listaWarpCorrection.Count; a++)
        {
          int value = listaWarpCorrection[a].Second;

          if (a == 0)
          {
            if (listaWarpCorrection[a].First != 0)
              lineWarpCorrection.AddPoint(0, value, "");
          }

          lineWarpCorrection.AddPoint(listaWarpCorrection[a].First, value, "");

          if (a == (listaWarpCorrection.Count - 1))
          {
            if (listaWarpCorrection[a].First != 400)
              lineWarpCorrection.AddPoint(400, value, "");
          }
        }
      }

      //BOBINA
      Line lineStdNOPerBobina = null;
      if (listaStdNOPerBobina != null)
      {
        listaStdNOPerBobina = this.controllaMAXeMIN(listaStdNOPerBobina, 0);

        lineStdNOPerBobina = new Line(Color.Brown);
        lineStd.Width = 2;
        for (int a = 0; a < listaStdNOPerBobina.Count; a++)
        {
          int value = listaStdNOPerBobina[a].Second;

          if (value > maximum)
            value = (int)maximum;

          if (value < minimum)
            value = (int)minimum;

          if (a == 0)
          {
            if (listaStdNOPerBobina[a].First != 0)
              lineStdNOPerBobina.AddPoint(0, value, "");
          }

          lineStdNOPerBobina.AddPoint(listaStdNOPerBobina[a].First, value, "");

          if (a == (listaStdNOPerBobina.Count - 1))
          {
            if (listaStd[a].First != 400)
              lineStdNOPerBobina.AddPoint(400, value, "");
          }
        }

        if (theLegendEntryCollection == null)
          theLegendEntryCollection = new LegendEntryCollection();

        theLegendEntryCollection.Add(new LegendEntry(lineStdNOPerBobina.Color, DBManager.getDBManager().message.getTranslatedMessage("§PRC_12") /*Standard*/ + " (" + DBManager.getDBManager().lineConfig.PRCConfig.Header.StandardRollWidthForCalculateTension_UDmm + ")"));
        theLegendEntryCollection.Add(new LegendEntry(lineStd.Color, DBManager.getDBManager().message.getTranslatedMessage("§PRC_12") /*Standard*/ + " (" + this.splicerRollWidth_UDmm + ")"));
      }

      Line lineTrend = null;
      if (isWarpCorrectionOrUserCorrection)
      {
        if (theLegendEntryCollection == null)
          theLegendEntryCollection = new LegendEntryCollection();

        int WarpCorrectionAndUserCorrection = warpCorrection + userCorrection;
        lineTrend = new Line(Color.Black);
        lineTrend.Width = 2;

        List<Pair<int, int>> listaUserEWarpCorrection = this.controllaMAXeMIN(listaStd, WarpCorrectionAndUserCorrection);

        for (int a = 0; a < listaUserEWarpCorrection.Count; a++)
        {
          int value = listaUserEWarpCorrection[a].Second;

          if (a == 0)
          {
            if (listaUserEWarpCorrection[a].First != 0)
              lineTrend.AddPoint(0, value, value.ToString());
          }

          lineTrend.AddPoint(listaUserEWarpCorrection[a].First, value, value.ToString());

          if (a == (listaUserEWarpCorrection.Count - 1))
          {
            if (listaUserEWarpCorrection[a].First != 400)
              lineTrend.AddPoint(400, value, value.ToString());
          }
        }

        if ((lineStd != null) && (listaStdNOPerBobina == null))
          theLegendEntryCollection.Add(new LegendEntry(lineStd.Color, DBManager.getDBManager().message.getTranslatedMessage("§PRC_12") /*Standard*/));
        if (lineUserCorrection != null)
          theLegendEntryCollection.Add(new LegendEntry(lineUserCorrection.Color, DBManager.getDBManager().message.getTranslatedMessage("§PRC_13") /*User*/ + " (" + userCorrection + ")"));
        if (lineWarpCorrection != null)
          theLegendEntryCollection.Add(new LegendEntry(lineWarpCorrection.Color, DBManager.getDBManager().message.getTranslatedMessage("§PRC_02") /*Warp*/ + " (" + warpCorrection + ")"));
        if (lineTrend != null)
          theLegendEntryCollection.Add(new LegendEntry(lineTrend.Color, DBManager.getDBManager().message.getTranslatedMessage("§PRC_14") /*Trend*/ + " (" + WarpCorrectionAndUserCorrection + ")"));
      }

      this.graphLines.drawMultiLine(lineTrend, lineStd, lineUserCorrection, lineWarpCorrection, lineStdNOPerBobina, null, theLegendEntryCollection, (int)minimum, (int)maximum, this.pointTohighlight, this.graphLines.getMyUnitMeasure());
    }

    private List<Pair<int, int>> controllaMAXeMIN(List<Pair<int, int>> toCheck, int valueToAdd)
    {
      List<Pair<int, int>> res = new List<Pair<int, int>>();

      try
      {
        for (int a = 0; a < toCheck.Count; a++)
        {
          int value = toCheck[a].Second + valueToAdd;

          if (value > this.maximum)
          {

            //
            //
            //      (x - Xb)         (x - Xa)
            // F = --------- * Ya  - --------- * Yb
            //      (Xa - Xb)        (Xa - Xb)
            //
            // quindi l'inversa è :
            //
            //     (F * (Xa - Xb)) + (Xb * Ya) - (Xa * Yb)
            // x = -------------------------------------
            //                Ya - Yb
            //
            //

            int Xa = toCheck[(a > 0) ? a - 1 : a + 1].First;
            int Xb = toCheck[a].First;
            int Ya = toCheck[(a > 0) ? a - 1 : a + 1].Second + valueToAdd;
            int Yb = toCheck[a].Second + valueToAdd;

            int newX = (((int)this.maximum * (Xa - Xb)) + (Xb * Ya) - (Xa * Yb)) / (Ya - Yb);
            res.Add(new Pair<int, int>(newX, (int)this.maximum));

            if ((a > 0) && (a < toCheck.Count) && (toCheck[a].Second > toCheck[a + 1].Second))
            {
              Xa = toCheck[a].First;
              Xb = toCheck[a + 1].First;
              Ya = toCheck[a].Second + valueToAdd;
              Yb = toCheck[a + 1].Second + valueToAdd;

              newX = (((int)this.maximum * (Xa - Xb)) + (Xb * Ya) - (Xa * Yb)) / (Ya - Yb);
              res.Add(new Pair<int, int>(newX, (int)this.maximum));
            }

            res.Add(new Pair<int, int>(toCheck[a].First, (int)this.maximum));
          }
          else if (value < this.minimum)
          {
            int Xa = toCheck[(a > 0) ? a - 1 : a + 1].First;
            int Xb = toCheck[a].First;
            int Ya = toCheck[(a > 0) ? a - 1 : a + 1].Second + valueToAdd;
            int Yb = toCheck[a].Second + valueToAdd;

            int newX = (((int)this.minimum * (Xa - Xb)) + (Xb * Ya) - (Xa * Yb)) / (Ya - Yb);
            res.Add(new Pair<int, int>(newX, (int)this.minimum));

            if ((a > 0) && (a < toCheck.Count) && (toCheck[a].Second < toCheck[a + 1].Second))
            {
              Xa = toCheck[a].First;
              Xb = toCheck[a + 1].First;
              Ya = toCheck[a].Second + valueToAdd;
              Yb = toCheck[a + 1].Second + valueToAdd;

              newX = (((int)this.minimum * (Xa - Xb)) + (Xb * Ya) - (Xa * Yb)) / (Ya - Yb);
              res.Add(new Pair<int, int>(newX, (int)this.minimum));
            }
          }
          else res.Add(new Pair<int, int>(toCheck[a].First, value));
        }

        if (res.Count != 0)
          res.Sort(delegate(Pair<int, int> x, Pair<int, int> y) { return x.First.CompareTo(y.First); });
      }
      catch (Exception ex)
      {
        SyncroC.Utility.Logger.LogError(this, "controllaMAXeMIN", ex);
      }

      return res;
    }

    private SyncroC.Controls.TitledNumericUpDown getControl(string name)
    {
      SyncroC.Controls.TitledNumericUpDown res = null;

      foreach (Control c in this.tableLayoutPanel2.Controls)
      {
        if (c is SyncroC.Controls.TitledNumericUpDown)
        {
          if (c.Name == name)
            res = (SyncroC.Controls.TitledNumericUpDown)c;
        }
      }
      return res;
    }

    public List<Pair<int, int>> getData()
    {
      return this.getData(false);
    }

    public List<Pair<int, int>> getData(bool NoCheckForValueBeenEdited)
    {
      List<Pair<int, int>> Lista = new List<Pair<int, int>>();

      for (int a = 0; a < MAX_LISTA_COUNT; a++)
      {
        SyncroC.Controls.TitledNumericUpDown mySpeedControl = this.getControl("tnud_Speed_" + a);
        SyncroC.Controls.TitledNumericUpDown myValueControl = this.getControl("tnud_value_" + a);

        if ((mySpeedControl != null) && (myValueControl != null))
        {
          if (NoCheckForValueBeenEdited)
            Lista.Add(new Pair<int, int>(SyncroC.Utility.SyncroConverter.convertToInt(mySpeedControl.getValueNoCheckForValueBeenEdited(), 0), SyncroC.Utility.SyncroConverter.convertToInt(myValueControl.getValueNoCheckForValueBeenEdited(), 0)));
          else
            Lista.Add(new Pair<int, int>(SyncroC.Utility.SyncroConverter.convertToInt(mySpeedControl.Value, 0), SyncroC.Utility.SyncroConverter.convertToInt(myValueControl.Value, 0)));
        }
      }

      if(Lista.Count!=0)
        Lista.Sort( delegate(Pair<int, int> x ,Pair<int, int> y) { return x.First.CompareTo(y.First); } ) ;

      if(NoCheckForValueBeenEdited)
        return Lista;
      else
        return this.removeDuplicates(Lista);
    }

    List<Pair<int, int>> removeDuplicates(List<Pair<int, int>> inputList)
    {
      List<Pair<int, int>> finalList = new List<Pair<int, int>>();
      foreach (Pair<int, int> currValue in inputList)
      {
        if (!this.Contains(finalList, currValue))
        {
          finalList.Add(currValue);
        }
      }
      return finalList;
    }

    bool Contains(List<Pair<int, int>> list, Pair<int, int> comparedValue)
    {
      foreach (Pair<int, int> listValue in list)
      {
        if (listValue.First == comparedValue.First)
        {
          return true;
        }
      }
      return false;
    }

    private void tnud_Speed_N_ValueChanged(object sender, EventArgs e)
    {
      if (!this.readOnly)
      {
        SyncroC.Controls.TitledNumericUpDown mySpeedControl = (SyncroC.Controls.TitledNumericUpDown)sender;
        int index = SyncroC.Utility.SyncroConverter.convertToInt(mySpeedControl.Name.Replace("tnud_Speed_", ""), 0);

        for (int a = index; a >= 0; a--)
        {
          this.getControl("tnud_Speed_" + a.ToString()).Tag = "";
          this.getControl("tnud_value_" + a.ToString()).Tag = "";
        }

        for (int a = index + 1; a < this.MAX_LISTA_COUNT; a++)
        {
          SyncroC.Controls.TitledNumericUpDown appoSpeedControl = this.getControl("tnud_Speed_" + a.ToString());
          if (Convert.ToString(appoSpeedControl.Tag) != "")
          {
            appoSpeedControl.DisableEventForValueChanged = true;
            appoSpeedControl.Value = mySpeedControl.Value;
            appoSpeedControl.DisableEventForValueChanged = false;
          }
          else
            break;
        }

        this.myRefresh();
      }
    }

    private void tnud_value_N_ValueChanged(object sender, EventArgs e)
    {
      if (!this.readOnly)
      {
        SyncroC.Controls.TitledNumericUpDown myValueControl = (SyncroC.Controls.TitledNumericUpDown)sender;
        int index = SyncroC.Utility.SyncroConverter.convertToInt(myValueControl.Name.Replace("tnud_value_", ""), 0);

        for (int a = index; a >= 0; a--)
        {
          this.getControl("tnud_Speed_" + a.ToString()).Tag = "";
          this.getControl("tnud_value_" + a.ToString()).Tag = "";
        }

        for (int a = index + 1; a < this.MAX_LISTA_COUNT; a++)
        {
          SyncroC.Controls.TitledNumericUpDown appoValueControl = this.getControl("tnud_value_" + a.ToString());
          if (Convert.ToString(appoValueControl.Tag) != "")
          {
            appoValueControl.DisableEventForValueChanged = true;
            appoValueControl.Value = myValueControl.Value;
            appoValueControl.DisableEventForValueChanged = false;
          }
          else
            break;
        }

        this.myRefresh();
      }
    }

    private void GraphLinesEditor_Load(object sender, EventArgs e)
    {
      this.MAX_LISTA_COUNT = 0;

      foreach (Control c in this.tableLayoutPanel2.Controls)
      {
        if (c is SyncroC.Controls.TitledNumericUpDown)
        {
          this.MAX_LISTA_COUNT++;
        }
      }

      this.MAX_LISTA_COUNT = this.MAX_LISTA_COUNT / 2;
    }

    private void lbl_SpeedOrValue_Paint(object sender, PaintEventArgs e)
    {
      if (!this.readOnly)
      {
        Label myLaber = (Label)sender;
        //height of the little triangle (50% of the font height)
        int TriangleHeigth = Convert.ToInt32((myLaber.Font.Height * 50) / 100);
        //distance of the little triangle from the label border
        int BorderDistance = Convert.ToInt32((myLaber.Font.Height - TriangleHeigth) / 2);
        //length of the little triangle side
        int TriangleSide = Convert.ToInt32(TriangleHeigth / (Math.Sqrt(3) / 2));
        //little triangle corners coordinates
        Point topLeft = new Point(
          myLaber.Width - BorderDistance - TriangleSide,//x 
          BorderDistance);//y
        Point topRight = new Point(
          myLaber.Width - BorderDistance,//x
          BorderDistance);//y
        Point low = new Point(
          myLaber.Width - BorderDistance - Convert.ToInt32(TriangleSide / 2),//x
          BorderDistance + TriangleHeigth);//y
        //drawing a filled triangle 
        e.Graphics.FillPolygon(
          new SolidBrush(this.ForeColor),
          new Point[] { topLeft, topRight, low });
      }
    }
  }
}
