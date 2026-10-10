#region Using directives

using System;
using System.Windows.Forms;

#endregion

namespace SyncroC.Utility
{
  public class FosberDialogResult
  {
    private DialogResult dialogResult;
    private object[] data;

    public FosberDialogResult(DialogResult dialog, object[] data)
    {
      this.dialogResult = dialog;
      this.data = data;
    }

    public DialogResult Dialog
    {
      get { return this.dialogResult; }
    }

    public object[] Data
    {
      get { return this.data; }
    }
  }
}
