using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_PRCAskMessage : Frm_PopUp
  {
    public Frm_PRCAskMessage()
    {
      InitializeComponent();
    }

    public FosberDialogResult ShowDialog(IWin32Window owner, string text)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.ttb_Value.Text = text;
      this.ttb_Value.Value = "";
      this.ttb_Value.Select();
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    private void btn_OK_Click(object sender, System.EventArgs e)
    {
      try
      {
        this.data = new object[] { ttb_Value.Value };
      }
      catch (Exception)
      {
        this.data = new object[] { "" };
      }
      this.Dispose();
    }

    private void btn_Abort_Click(object sender, System.EventArgs e)
    {
      this.Dispose();
    }
  }
}
