using System;
using System.Collections.Generic;
using System.Text;

namespace SyncroC.Controls.ControlEventArgs
{
  public class SlitterScorerStatusEventArgs : System.EventArgs
  {
    public int ind;

    public SlitterScorerStatusEventArgs(int ind)
    {
      this.ind = ind;
    }
  }
}
