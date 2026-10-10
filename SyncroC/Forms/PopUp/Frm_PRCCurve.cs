using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using Microsoft.Tools.Graphs.Lines;
using Microsoft.Tools.Graphs.Legends;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_PRCCurve : SyncroC.Forms.PopUp.Frm_PopUp
  {
    private List<SyncroC.Graph.Pair<int, int>> listaUsata;
    private List<SyncroC.Graph.Pair<int, int>> listaOndaMadre;
    private string typeOfQueryOndaMadre = "";
    private int userCorrection=0;
    private int warpCorrection =0;
    private Point pointTohighlight;
    private bool isOverride;

    public Frm_PRCCurve()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    public FosberDialogResult ShowDialog(bool canModifyValue, SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor theTypeOfGraphLinesEditor, string theTitle, string theTypeOfClassification, string theTypeOfQueryUsata, string theTypeOfQueryOndaMadre, bool theIsOverride, int minimum, int maximum, List<SyncroC.Graph.Pair<int, int>> listaUsata, List<SyncroC.Graph.Pair<int, int>> listaOndaMadre, int userCorrection, int warpCorrection, Point pointTohighlight)
    {
      return this.ShowDialog(canModifyValue, theTypeOfGraphLinesEditor, theTitle, theTypeOfClassification, theTypeOfQueryUsata, theTypeOfQueryOndaMadre, theIsOverride, minimum, maximum, listaUsata, listaOndaMadre, userCorrection, warpCorrection, -1, pointTohighlight);
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    public FosberDialogResult ShowDialog(bool canModifyValue, SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor theTypeOfGraphLinesEditor, string theTitle, string theTypeOfClassification, string theTypeOfQueryUsata, string theTypeOfQueryOndaMadre, bool theIsOverride, int minimum, int maximum, List<SyncroC.Graph.Pair<int, int>> listaUsata, List<SyncroC.Graph.Pair<int, int>> listaOndaMadre, int userCorrection, int warpCorrection, int splicerRollWidth_UDmm, Point pointTohighlight)
    {
      this.lbl_typeOfClassification.Testo = theTypeOfClassification;
      this.isOverride = theIsOverride;

      if (theIsOverride)
        this.lbl_typeOfQuery.BackColor = SyncroC.DataBase.PRCManager.COLOR_PRC_OVERRIDE;
      else
        this.lbl_typeOfQuery.BackColor = this.lbl_typeOfClassification.BackColor;

      this.lbl_typeOfQuery.Testo = theTypeOfQueryUsata;
      this.typeOfQueryOndaMadre = theTypeOfQueryOndaMadre;

      this.graphLinesEditor.MyTypeOfGraphLinesEditor = theTypeOfGraphLinesEditor;
      this.graphLinesEditor.Title = theTitle;
      this.graphLinesEditor.Minimum = minimum;
      this.graphLinesEditor.Maximum = maximum;

      this.graphLinesEditor.fillMultiData(listaUsata, userCorrection, warpCorrection, pointTohighlight, splicerRollWidth_UDmm);

      this.listaUsata = listaUsata;
      this.listaOndaMadre = listaOndaMadre;
      this.userCorrection = userCorrection;
      this.warpCorrection = warpCorrection;
      this.pointTohighlight = pointTohighlight;

      this.chk_Modify.Text = DBManager.getDBManager().language.getTranslation("Frm_RollData.chk_Modify.Text");
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.btn_SaveOnFlute_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_RollData.btn_Save_YN.Text") + " (" + DBManager.getDBManager().language.getTranslation("Frm_WetOrdersList.gdv_Programs.2") + ")";
      this.btn_SaveOnQuality_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_RollData.btn_Save_YN.Text") + " (" + DBManager.getDBManager().language.getTranslation("Frm_WetOrdersList.gdv_Programs.5") + ")";

      if (!canModifyValue)
      {
        this.pct_Lock.Visible = true;
        this.chk_Modify.Visible = false;
      }

      //ABELLO_FI07/12/2010_1 [PRC] Se non trovo una curva valida non controllo il componente e lo faccio diventare rosso.
      if (theTypeOfQueryUsata == "???")
      {
        this.lbl_typeOfQuery.BackColor = Color.Red;
        this.pct_Lock.Visible = false;
        this.chk_Modify.Visible = false;
      }

      return new FosberDialogResult(this.ShowDialog(), this.data);
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
      this.data = new object[] { 0 };
      this.Dispose();
    }

    private void btn_Abort_YN_Click(object sender, EventArgs e)
    {
      this.data = new object[] { 0 };
      this.Dispose();
    }

    private void btn_SaveOnFlute_NN_Click(object sender, EventArgs e)
    {
      this.data = new object[] { 1, this.graphLinesEditor.getData() };
      this.Dispose();
    }

    private void btn_SaveOnQuality_NN_Click(object sender, EventArgs e)
    {
      this.data = new object[] { 2, this.graphLinesEditor.getData() };
      this.Dispose();
    }

    private void lbl_typeOfClassification_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, this.lbl_typeOfClassification.Width, this.lbl_typeOfClassification.Height, 5);
      Region a = new Region(path);
      this.lbl_typeOfClassification.Region = a;
    }

    private void lbl_typeOfQuery_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, this.lbl_typeOfQuery.Width, this.lbl_typeOfQuery.Height, 5);
      Region a = new Region(path);
      this.lbl_typeOfQuery.Region = a;
    }

    private void chk_Modify_OnCheckedChanged(object sender, EventArgs e)
    {
      this.timer_ControllaModifiche.Enabled = this.chk_Modify.Checked;

      if (!this.chk_Modify.Checked)
        this.btn_SaveOnQuality_NN.Visible = false;
      
      if (!this.graphLinesEditor.ReadOnly)
        //revert all changes
        this.graphLinesEditor.fillMultiData(this.listaUsata, this.userCorrection, this.warpCorrection, this.pointTohighlight);
     
      this.graphLinesEditor.ReadOnly = !this.chk_Modify.Checked;

      this.btn_OK.Visible = !this.chk_Modify.Checked;
      this.btn_Abort_NN.Visible = this.chk_Modify.Checked;
      //this.btn_SaveOnQuality_YN.Visible = this.chk_Modify.Checked;
      this.btn_SaveOnFlute_NN.Visible = (this.chk_Modify.Checked && !this.isOverride);

      if (this.isOverride)
        this.pct_PRCoverride.Visible = (this.chk_Modify.Checked && this.isOverride);
      else
        this.pct_PRCoverride.Visible = false;

      if (this.btn_OK.Visible)
      {
        this.AcceptButton = this.btn_OK;
        this.CancelButton = this.btn_Abort_NN;
      }
      else
      {
        this.AcceptButton = null;
        this.CancelButton = this.btn_Abort_NN;
      }
    }

    private void pct_PRCoverride_Click(object sender, EventArgs e)
    {
      this.isOverride = false;
      this.lbl_typeOfQuery.Testo = this.typeOfQueryOndaMadre;
      this.lbl_typeOfQuery.BackColor = this.lbl_typeOfClassification.BackColor;
      this.listaUsata = this.listaOndaMadre;
      this.chk_Modify_OnCheckedChanged(null, null);
    }

    private void timer_ControllaModifiche_Tick(object sender, EventArgs e)
    {
      if (!this.chk_Modify.Checked)
      {
        this.btn_SaveOnQuality_NN.Visible = false;
        return;
      }

      List<SyncroC.Graph.Pair<int, int>> listaOra = this.graphLinesEditor.getData(true);
      for (int i = 0; i < this.listaUsata.Count;i++ )
      {
        if ((this.listaUsata[i].First != listaOra[i].First) || (this.listaUsata[i].Second != listaOra[i].Second))
        {
          this.btn_SaveOnQuality_NN.Visible = true;
          return;
        }
      }

      this.btn_SaveOnQuality_NN.Visible = false;
    }
  }
}
