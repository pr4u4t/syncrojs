using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Statistics.Rolls
{
  public partial class Frm_StatisticsRollsSubgroup : Frm_StatisticsGroup
  {
    public Frm_StatisticsRollsSubgroup()
    {
      InitializeComponent();
    }

    public override void _Show()
    {
      //ABELLO mysql
      //if ((!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT
      //  || (!SyncroC.DBManager.getDBManager().syncroConfig.RSMSettings.BaseComms.Enable && !SyncroC.DBManager.getDBManager().syncroConfig.RollScanSettings.Enable )))
      //{
      //  if(Environment.UserName != "ABELLO")
      //    this.tab_TabB.Visible = false;
      //}

      //Quella pagina è candidata ad essere cancellata
      this.tab_TabB.Visible = false;

      PageManager pm = PageManager.getPageManager();
      pm.pagesHistorian.CurrentPageStatisticsRolls = this.getRequestCommand();
      base._Show();
    }

    protected override void F4()
    {
    }

    protected override void F10()
    {
      askForPage(typeof(Frm_StatisticsRollsList));
    }

    protected override void F11()
    {
      askForPage(typeof(Frm_StatisticsRollsTrace));
    }
  }
}
