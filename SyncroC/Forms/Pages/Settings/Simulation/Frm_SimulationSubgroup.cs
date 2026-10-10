using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Forms.Pages.Settings;

namespace SyncroC.Forms.Pages.Settings.Simulation
{
  public partial class Frm_SimulationSubgroup : Frm_SettingsGroup
  {
    public Frm_SimulationSubgroup()
    {
      InitializeComponent();
    }

    /// <summary>
    /// To be called by the server and invoked by the client throw the reflection to show a new page.
    /// Last visited page (PagesHistorian.CurrentPageSettings_Simulation) for the Simulation subgroup is set here.
    /// </summary>
    public override void _Show()
    {
      PageManager pm = PageManager.getPageManager();
      pm.pagesHistorian.CurrentPageSettings_Simulation = this.getRequestCommand();
      pm.pagesHistorian.PageChanged(this);
      pm.UpdateCache(this);
      base._Show();
    }

    /// <summary>
    /// F7 key behaviour
    /// </summary>
    protected override void F10()
    {
       askForPage(typeof(Frm_SimulationGeneral));
    }
  }
}