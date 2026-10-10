using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Stacker.Status
{
  public partial class Frm_StackerStatusSubgroup : SyncroC.Forms.Pages.Stacker.Frm_StackerGroup
  {
    public Frm_StackerStatusSubgroup()
    {
      InitializeComponent();
    }

    public override void _Show()
    {
      PageManager pm = PageManager.getPageManager();
      switch (index)
      {
        case 1:
        default:
          pm.pagesHistorian.CurrentPageStackerStatus1 = this.getRequestCommand(index);
          break;
        case 2:
          pm.pagesHistorian.CurrentPageStackerStatus2 = this.getRequestCommand(index);
          break;
        case 3:
          pm.pagesHistorian.CurrentPageStackerStatus3 = this.getRequestCommand(index);
          break;
      }
      base._Show();
    }

    protected override void F3()
    {
    }

    protected override void F10()
    {
      askForPage(typeof(Frm_StackerStatus01), index);
    }

    protected override void F11()
    {
      askForPage(typeof(Frm_StackerStatus02), index);
    }

    protected override void F12()
    {
      askForPage(typeof(Frm_StackerStatus03), index);
    }

    protected override void F13()
    {
      askForPage(typeof(Frm_StackerStatus04), index);
    }
  }
}