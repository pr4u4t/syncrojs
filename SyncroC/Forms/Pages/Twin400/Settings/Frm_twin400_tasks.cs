using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;


namespace SyncroC.Forms.Pages.Twin400.Settings
{
  public partial class Frm_twin400_tasks : Frm_twin400_settingsSubgroup
  {
    private SyncroConfig cfg;
    private bool modify = false;
    private bool modDBMS = false;
    private bool modGENERAL = false;
    private bool modStarter = false;
    private bool modRTC = false;
    private bool modTwin400Mac = false;
    private bool modTwin400MacComms = false;
		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
    private bool modTwin400MacSPDEComms = false;
    //agrassi_FI16-11-2010_2
    private bool modTwin400MacBHSComms = false;
    private bool modTwin400Hardware = false;
		private bool modSyncroC = false;

    public Frm_twin400_tasks()
    {
      InitializeComponent();
      cfg = DBManager.getDBManager().syncroConfig;
    }

    public override void refreshData()
    {
      SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

      this.CfgDBMSGrid.SelectedObject = (DBMSSettings)cfg.DbmsSetting.CreateCopy();
      this.CfgGeneralGrid.SelectedObject = (GeneralSettings)cfg.GeneralSetting.CreateCopy();
      this.CfgStarterGrid.SelectedObject = (StarterSettings)cfg.StarterSetting.CreateCopy();
      this.CfgRTCGrid.SelectedObject = (RTCSettings)cfg.RtcSetting.CreateCopy();
      this.CfgTwin400MacGrid.SelectedObject = (AllenBradleyCommsSettings)cfg.Twin400MacSetting.CreateCopy();
      this.CfgTwin400MacCommsGrid.SelectedObject = (BaseComms)cfg.Twin400MacCommsSetting.CreateCopy();
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
      this.CfgTwin400MacCommsSPDEGrid.SelectedObject = (BaseComms)cfg.Twin400MacCommsSPDESetting.CreateCopy();
      //agrassi_FI16-11-2010_2
      this.CfgTwin400MacCommsBHSGrid.SelectedObject = (BaseComms)cfg.Twin400MacCommsBHSSetting.CreateCopy();

			if (!cfg.IsTwin400Only())
			{
				this.CfgSSTwin22_60Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings)cfg.Twin22_60Setting.CreateCopy();
			}
			this.CfgSyncroCGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SyncroCSettings)cfg.SyncroCSetting.CreateCopy();

    }

    protected override void CtrlF4()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        if (this.modify)
        {
          if (this.modGENERAL)
          {
            cfg.GeneralSetting = (GeneralSettings)this.CfgGeneralGrid.SelectedObject;
            cfg.GeneralSetting.Save();
          }
          if (this.modDBMS)
          {
            cfg.DbmsSetting = (DBMSSettings)this.CfgDBMSGrid.SelectedObject;
            cfg.DbmsSetting.Save();
          }
          if (this.modStarter)
          {
            cfg.StarterSetting = (StarterSettings)this.CfgStarterGrid.SelectedObject;
            cfg.StarterSetting.Save();
          }
          if (this.modRTC)
          {
            cfg.RtcSetting = (RTCSettings)this.CfgRTCGrid.SelectedObject;
            cfg.RtcSetting.Save();
          }
          if (this.modTwin400Mac)
          {
            cfg.Twin400MacSetting = (AllenBradleyCommsSettings)this.CfgTwin400MacGrid.SelectedObject;
            cfg.Twin400MacSetting.Save();
          }
          if (this.modTwin400MacComms)
          {
            cfg.Twin400MacCommsSetting = (BaseComms)this.CfgTwin400MacCommsGrid.SelectedObject;
            cfg.Twin400MacCommsSetting.Save();
          }
					//RFazzi_FI17/01/2008_1.Added task configurations on graphics
					if (this.modTwin400MacSPDEComms)
					{
						cfg.Twin400MacCommsSPDESetting = (BaseComms)this.CfgTwin400MacCommsSPDEGrid.SelectedObject;
						cfg.Twin400MacCommsSPDESetting.Save();
					}
          //agrassi_FI16-11-2010_2
          if (this.modTwin400MacBHSComms)
          {
            cfg.Twin400MacCommsBHSSetting = (Twin400MacCommsBHSSettings)this.CfgTwin400MacCommsBHSGrid.SelectedObject;
            cfg.Twin400MacCommsBHSSetting.Save();
          }
          if (this.modTwin400Hardware)
					{
						cfg.Twin22_60Setting = (SSTwin22_60Settings)this.CfgSSTwin22_60Grid.SelectedObject;
						cfg.Twin22_60Setting.Save();
					}
					if (this.modSyncroC)
					{
						cfg.SyncroCSetting = (SyncroCSettings)this.CfgSyncroCGrid.SelectedObject;
						cfg.SyncroCSetting.Save();
					}

          cfg.SendRelodSettings();
          this.refreshData();

          this.CfgGeneralGrid.ViewForeColor = Color.DimGray;
          this.CfgDBMSGrid.ViewForeColor = Color.DimGray;
          this.CfgStarterGrid.ViewForeColor = Color.DimGray;
          this.CfgRTCGrid.ViewForeColor = Color.DimGray;
          this.CfgTwin400MacGrid.ViewForeColor = Color.DimGray;
          this.CfgTwin400MacCommsGrid.ViewForeColor = Color.DimGray;
					//RFazzi_FI17/01/2008_1.Added task configurations on graphics
					this.CfgTwin400MacCommsSPDEGrid.ViewForeColor = Color.DimGray;
					this.CfgSSTwin22_60Grid.ViewForeColor = Color.DimGray;
					this.CfgSyncroCGrid.ViewForeColor = Color.DimGray;

          lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
          lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;

          this.modify = false;
        }
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    protected override void CtrlF5()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        Utility.Settings s = Utility.Settings.getSettings();
        lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
        this.CfgGeneralGrid.ViewForeColor = Color.Black;
        this.CfgDBMSGrid.ViewForeColor = Color.Black;
        this.CfgStarterGrid.ViewForeColor = Color.Black;
        this.CfgRTCGrid.ViewForeColor = Color.Black;
        this.CfgTwin400MacGrid.ViewForeColor = Color.Black;
        this.CfgTwin400MacCommsGrid.ViewForeColor = Color.Black;
				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
				this.CfgTwin400MacCommsSPDEGrid.ViewForeColor = Color.Black;
				this.CfgSSTwin22_60Grid.ViewForeColor = Color.Black ;
				this.CfgSyncroCGrid.ViewForeColor = Color.Black;
				
				this.modify = true;
        this.modGENERAL = false;
        this.modDBMS = false;
        this.modStarter = false;
        this.modRTC = false;
        this.modTwin400Mac = false;
        this.modTwin400MacComms = false;
				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
        this.modTwin400MacSPDEComms = false;
        //agrassi_FI16-11-2010_2
        this.modTwin400MacBHSComms = false;
        this.modTwin400Hardware = false;
				this.modSyncroC = false;
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    protected override void Escape()
    {
      if (this.modify)
      {
        this.modify = false;
        this.CfgGeneralGrid.ViewForeColor = Color.DimGray;
        this.CfgDBMSGrid.ViewForeColor = Color.DimGray;
        this.CfgStarterGrid.ViewForeColor = Color.DimGray;
        this.CfgRTCGrid.ViewForeColor = Color.DimGray;
        this.CfgTwin400MacGrid.ViewForeColor = Color.DimGray;
        this.CfgTwin400MacCommsGrid.ViewForeColor = Color.DimGray;
				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
				this.CfgTwin400MacCommsSPDEGrid.ViewForeColor = Color.DimGray;
				this.CfgSSTwin22_60Grid.ViewForeColor = Color.DimGray;
				this.CfgSyncroCGrid.ViewForeColor = Color.DimGray;

        lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
        this.refreshData();
      }
    }

    private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
    {
      if (!this.modify)
        this.refreshData();
      else
      {
        if (((PropertyGrid)s).Equals(this.CfgGeneralGrid))
        {
          this.modGENERAL = true;
        }
        else if (((PropertyGrid)s).Equals(this.CfgDBMSGrid))
        {
          this.modDBMS = true;
        }
        else if (((PropertyGrid)s).Equals(this.CfgStarterGrid))
        {
          this.modStarter = true;
        }
        else if (((PropertyGrid)s).Equals(this.CfgRTCGrid))
        {
          this.modRTC = true;
        }
        else if (((PropertyGrid)s).Equals(this.CfgTwin400MacGrid))
        {
          this.modTwin400Mac = true;
        }
        else if (((PropertyGrid)s).Equals(this.CfgTwin400MacCommsGrid))
        {
          this.modTwin400MacComms = true;
        }
				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
				else if (((PropertyGrid)s).Equals(this.CfgTwin400MacCommsSPDEGrid))
				{
					this.modTwin400MacSPDEComms = true;
				}
        //agrassi_FI16-11-2010_2
        else if (((PropertyGrid)s).Equals(this.CfgTwin400MacCommsBHSGrid))
        {
          this.modTwin400MacBHSComms = true;
        }
        else if (((PropertyGrid)s).Equals(this.CfgSSTwin22_60Grid) && !cfg.IsTwin400Only())
				{
					this.modTwin400Hardware = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgSyncroCGrid) )
				{
					this.modSyncroC = true;
				}
			}
    }

    protected override void F13()
    {
    }

    protected override bool IncludeComponentsIntoLanguageDB()
    {
      return false;
    }
  }
}