using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Production.Operator
{
  public partial class Frm_OperatorSubGroup : SyncroC.Forms.Pages.Production.Frm_ProductionGroup
  {
    public Frm_OperatorSubGroup()
    {
      InitializeComponent();

      this.PRC_Init();
    }

    protected override void F10()
    {
      askForPage(typeof(Frm_Operator));
    }

    protected override void F11()
    {
      askForPage(typeof(Frm_Printings));
    }

    protected override void F12()
    {
      askForPage(typeof(Frm_MessagePRC));
    }

    private void PRC_Init()
    {
      try
      {
        //PRC
        if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
          this.FKeyEnable[(int)FKEYS.F12] = this.tab_TabC.Visible = true;
        else
          this.FKeyEnable[(int)FKEYS.F12] = this.tab_TabC.Visible = false;
      }
      catch (Exception ex)
      {
        SyncroC.Utility.Logger.LogError(this, "PRC_Init - ", ex);
      }
    }
  }
}

