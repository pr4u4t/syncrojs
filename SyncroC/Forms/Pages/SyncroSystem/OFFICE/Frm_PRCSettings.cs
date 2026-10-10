using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.DataBase.LineConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.OFFICE
{
  public partial class Frm_PRCSettings : SyncroC.Forms.Pages.SyncroSystem.OFFICE.Frm_OFFICESubGroup
  {
    private bool modConfig = false;
    private bool modify = false;

    public Frm_PRCSettings()
    {
      InitializeComponent();
    }

    protected override bool IncludeComponentsIntoLanguageDB()
    {
      return false;
    }

    protected override void F12()
    {
    }

    private void CfgGrid_PropertyalueChanged(object s, PropertyValueChangedEventArgs e)
    {
      if (!this.modify)
        this.refreshData();
      else
      {
        if (((PropertyGrid)s).Equals(this.CfgGrid))
        {
          this.modConfig = true;
        }
      }
    }

    private void WarpGrid_PropertyalueChanged(object s, PropertyValueChangedEventArgs e)
    {
      if (!this.modify)
        this.refreshData();
    }

    protected override void CtrlF5()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        //Enable Settings modifications
        this.CfgGrid.ViewForeColor = Color.Black;
        this.modify = true;
        this.modConfig = false;
        lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    protected override void CtrlF4()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        //Save Settings modifications
        if (this.modify)
        {
          if (this.modConfig)
          {
            CommunicationManager cm = CommunicationManager.GetCommunicationManager();
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.PRC + ".SendHeaderConfiguration("
                  + ((SyncroC.DataBase.LineConfig.HeaderPRC)this.CfgGrid.SelectedObject).SerializeProperties() + ")");
            this.CfgGrid.ViewForeColor = Color.DimGray;
          }
          this.modConfig = false;
          this.modify = false;
          lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
          this.refreshData();
        }
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    protected override void Escape()
    {
      //Esc without saving
      if (this.modify)
      {
        this.CfgGrid.ViewForeColor = Color.DimGray;
        this.modConfig = false;
        this.modify = false;
        lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
        this.refreshData();
      }
    }

    public override void refreshData()
    {
      LineConfig cfg = DBManager.getDBManager().lineConfig;
      this.CfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.HeaderPRC)cfg.PRCConfig.Header.CreateCopy();
    }

    private void Frm_PRCSettings_Load(object sender, EventArgs e)
    {
      this.refreshData();
    }
  }
}