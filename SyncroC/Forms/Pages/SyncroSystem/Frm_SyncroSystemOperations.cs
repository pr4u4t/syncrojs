using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem
{
  public partial class Frm_SyncroSystemOperations : Frm_SyncroSystemGroup
  {
    public Frm_SyncroSystemOperations()
    {
      InitializeComponent();
    }

    public override void _Show()
    {
      PageManager pm = PageManager.getPageManager();
      pm.pagesHistorian.CurrentPageSyncroSystem_Operations = this.getRequestCommand();
      base._Show();

			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				this.btn_CloseSyncro.Enabled = false;
				this.btn_CloseSyncro.BackColor = Color.Gray;
        this.btn_CloseSyncro.Cursor = Cursors.Arrow;

				this.btn_SaveData.Enabled = false;
				this.btn_SaveData.BackColor = Color.Gray;
        this.btn_SaveData.Cursor = Cursors.Arrow;
				
        this.btn_SaveSource.Enabled = false;
				this.btn_SaveSource.BackColor = Color.Gray;
        this.btn_SaveSource.Cursor = Cursors.Arrow;
				
        this.btn_GenerateManual.Enabled = false;
				this.btn_GenerateManual.BackColor = Color.Gray;
        this.btn_GenerateManual.Cursor = Cursors.Arrow;

        this.btn_ExportStandardCurves.Enabled = false;
        this.btn_ExportStandardCurves.BackColor = Color.Gray;
        this.btn_ExportStandardCurves.Cursor = Cursors.Arrow;
			}

			SyncroC.Utility.Accessibility acc = SyncroC.Utility.Accessibility.getAccessibility();
			if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && (!acc.isLevelAtLeastLow()))
			{
				this.btn_CloseClient.Enabled = false;
				this.btn_CloseClient.BackColor = Color.Gray;
        this.btn_CloseClient.Cursor = Cursors.Arrow;
			}

			if (!acc.isLevelHigh())
			{
				this.btn_CloseSyncro.Enabled = false;
				this.btn_CloseSyncro.BackColor = Color.Gray;
        this.btn_CloseSyncro.Cursor = Cursors.Arrow;
				this.btn_SaveData.Enabled = false;
				this.btn_SaveData.BackColor = Color.Gray;
        this.btn_SaveData.Cursor = Cursors.Arrow;
				this.btn_SaveSource.Enabled = false;
				this.btn_SaveSource.BackColor = Color.Gray;
        this.btn_SaveSource.Cursor = Cursors.Arrow;
				this.btn_GenerateManual.Enabled = false;
				this.btn_GenerateManual.BackColor = Color.Gray;
        this.btn_GenerateManual.Cursor = Cursors.Arrow;
				this.btn_DebugPage.Enabled = false;
				this.btn_DebugPage.BackColor = Color.Gray;
        this.btn_DebugPage.Cursor = Cursors.Arrow;

			}

      if (
        !SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT || 
        !acc.isLevelHigh() ||
        !SyncroC.Utility.Settings.getSettings().configuration.PRC
        )
      {
        this.btn_ExportStandardCurves.Enabled = false;
        this.btn_ExportStandardCurves.BackColor = Color.Gray;
        this.btn_ExportStandardCurves.Cursor = Cursors.Arrow;
      }
    }

    protected override void F8()
    {
    }

		private void btn_DisableConsole_Click(object sender, EventArgs e)
		{
			base.ControlShiftC();
		}

		private void btn_CloseClient_Click(object sender, EventArgs e)
		{
			base.ControlAltX();
		}

		private void btn_CloseSyncro_Click(object sender, EventArgs e)
		{
			base.ControlAltY();
		}

		private void btn_SaveLog_Click(object sender, EventArgs e)
		{
			base.ControlShiftL();
		}

		private void btn_SaveReport_Click(object sender, EventArgs e)
		{
			base.ControlShiftR();
		}

		private void btn_SaveSource_Click(object sender, EventArgs e)
		{
			base.ControlShiftS();
		}

		private void btn_GenerateManual_Click(object sender, EventArgs e)
		{
			base.ControlShiftO();
		}

		private void btn_SaveData_Click(object sender, EventArgs e)
		{
			base.ControlShiftD();
		}

		private void btn_DebugPage_Click(object sender, EventArgs e)
		{
			base.ControlAltD();
		}

    //agrassi_FI19-12-08_2
    private void btn_SaveOnlySource_Click(object sender, EventArgs e)
    {
      this.ControlShiftZ();
    }

    //agrassi_FI19-12-08_2
    private void btn_SaveExe_Click(object sender, EventArgs e)
    {
      this.ControlShiftE();
    }

    //PMASSARO_FI05/02/09 - Added Command For Resetting Production
    private void btn_ResetProduction_Click(object sender, EventArgs e)
    {
      this.ControlShiftQ();
    }

    //PMASSARO_FI05/02/09 - Added Command For Resetting Production
    private void btn_RestoreProduction_Click(object sender, EventArgs e)
    {
      this.ControlShiftAltQ();
    }

    //PMASSARO_FI10/02/09 - Try To Restart Failed Task
    private void btn_RestartTask_Click(object sender, EventArgs e)
    {
      this.ControlShiftT();
    }

    private void btn_ExportStandardCurves_Click(object sender, EventArgs e)
    {
      SyncroC.DataBase.PRCManager.getPRCManager().exportStandardCurves();
    }
  }
}