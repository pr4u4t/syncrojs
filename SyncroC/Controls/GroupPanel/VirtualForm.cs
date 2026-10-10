using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Controls.GroupPanel
{
  public partial class VirtualForm : TabControl
  {
    public VirtualForm()
    {
      InitializeComponent();
    }

    //Hide the tab headers at run time
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 0x1328 && !DesignMode)
        m.Result = (IntPtr)1;
      else
        base.WndProc(ref m);
    }
  }
}
