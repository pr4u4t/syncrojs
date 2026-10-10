using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using SyncroC.Network;
using SyncroC.Forms.PopUp;
using SyncroC.Utility;

//AGrassi_FI 08-03-2007 Automatic simulation
namespace SyncroC.Forms.Pages.Settings.Simulation
{
	public partial class Frm_SimulationGeneral : Frm_SimulationSubgroup
	{
		string serverProductionPath;
		private const string SYNCRO_TAG = "syncro";
		private string startPath;
    //agrassi_FI01-10-2010_1
    private bool simulationWithoutOffice = false;

		public Frm_SimulationGeneral()
		{
			InitializeComponent();
			this.CheckSimulationState();
		}

		protected override void F10()
		{
		}

		private void btn_Simulation_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			string parameters;
      if (this.Simulation.Active)
        parameters = "0,0,0,0,0,0,0,0,0";
      else
      {
        //agrassi_FI16-12-08
        //parameters = this.CheckValue(this.tnud_Speed.Value) + "," + this.CheckValue(this.tnud_CutsPerSecond.Value) + "," +
        parameters = this.CheckValue(this.tnud_Speed.Value) + "," + "0" + "," +
            this.CheckValue(this.tnud_SF1Speed.Value) + "," + this.CheckValue(this.tnud_SF2Speed.Value) + "," +
            this.CheckValue(this.tnud_SF3Speed.Value) + "," +
            (this.chk_SSIsRdyChange.Checked ? "1" : "0") + "," +
            (this.chk_RSHHasCut.Checked ? "1" : "0") + "," +
            (this.chk_SSHasChanged.Checked ? "1" : "0") + "," +
            (this.chk_KnifeHasChanged.Checked ? "1" : "0");
      }
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_SimulationGeneral)) + ".Simulation(" + parameters + ")");

		}

		private void Frm_SimulationGeneral_Load(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			this.tnud_Speed.Value = "0";
      //agrassi_FI16-12-08
      //this.tnud_CutsPerSecond.Value = "0";
      this.tnud_SF1Speed.Value = "0";
			this.tnud_SF2Speed.Value = "0";
			this.tnud_SF3Speed.Value = "0";
			this.serverProductionPath = SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Substring(0, SyncroC.Utility.Settings.getSettings().paths.ServerProduction.Length - 1);
			this.startPath = SyncroC.Utility.Settings.getSettings().paths.Source + "_server\\";
#if DEBUG
			startPath += "debug\\";
#else
        startPath += "release\\";
#endif
		}

		private string CheckValue(string s)
		{
			int res;
			s = s.Trim();
			if ((s.Length > 0) && int.TryParse(s, out res))
				return s;
			return "0";
		}

		private void _SetSimulation(bool sim)
		{
			this.Simulation.Active = sim;
			this.Simulation.Text = (sim ? "Stop" : "Start") + " Simulation";
      //agrassi_FI16-12-08
			this.tnud_Speed.ReadOnly = /*this.tnud_CutsPerSecond.ReadOnly =*/ this.tnud_SF1Speed.ReadOnly =
				this.tnud_SF2Speed.ReadOnly = this.tnud_SF3Speed.ReadOnly = sim;
			this.chk_KnifeHasChanged.Enabled = this.chk_RSHHasCut.Enabled = this.chk_SSHasChanged.Enabled =
				this.chk_SSIsRdyChange.Enabled = !sim;
		}

		private void CheckSimulationState()
		{
			bool isIdleShift = RTCManager.getRTCManager().IsIdleShift;
			bool dirExists = Directory.Exists(this.serverProductionPath + Command.BACKUP_EXTENSION);
			this.Simulation.Enabled = this.TerminateSimulation.Enabled = dirExists;
			this.PrepareForSimulation.Enabled = ((!dirExists) && isIdleShift);
			this.lab_EnableSimulation.Visible = (!dirExists) && (!isIdleShift);
			this.lab_WarnSimulation.Visible = this.lab_NewShift.Visible = dirExists;
      //agrassi_FI01-10-2010_1
      this.lab_WarnSimulation.Text = "The communication with Office is " + (this.simulationWithoutOffice ? "DISABLED" : "ENABLED") + " !";
      this.lab_WarnSimulation.ForeColor = (this.simulationWithoutOffice ? Color.Red : Color.Blue);
      //if (dirExists)
      //  this.lab_Remember.Visible = !this.lab_Remember.Visible;
		}

		private void PrepareForSimulation_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			Frm_Confirm frm_conf = new Frm_Confirm();
      //agrassi_FI30-10-08
      //DialogResult res = frm_conf.ShowDialog(this, "Warning! Syncro will be closed, made a copy of the production data and then started again.");
      DialogResult res = frm_conf.ShowDialog(this, "Warning! A copy of the production data will be made and communication with office will be interrupted.");
			if (res == DialogResult.OK)
			{
				Logger.LogError(this, "Prepare for Simulation");

        //Il file se c'è va SEMPRE cancellato, anche se non è abilitato il mySQL
        string fileNameDestinazione = SyncroC.Utility.Settings.getSettings().paths.ServerProduction + "SQLDump_" + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.Job + ".backup";
        if (System.IO.File.Exists(fileNameDestinazione))
          System.IO.File.Delete(fileNameDestinazione);

        //agrassi_FI26-10-2010_1
        //if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED)
          if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
            SyncroC.DataBase.SqlManager.getSqlManager().backupDataBase(fileNameDestinazione,"");
				
        Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = this.startPath + "StartEndSimulation.exe";

        //PMASSARO_FI09/02/09 - Added Logs Into StartEndSimulation
        /*
        process.StartInfo.Arguments = "start \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION + " " + SYNCRO_TAG;
        */
        process.StartInfo.Arguments = "start \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION + " " + SYNCRO_TAG + " " + SyncroC.Utility.Settings.getSettings().paths.Log;
        
				process.Start();
        //agrassi_FI30-10-08
				//this.CloseSyncroCompletely();
        //agrassi_FI01-10-2010_1
        this.simulationWithoutOffice = true;
			}
		}

		private void TerminateSimulation_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				Frm_Alert frm_alert = new Frm_Alert();
				frm_alert.ShowDialog(this, "This page can be used only on Server !!");
				return;
			}
			Frm_Confirm frm_conf = new Frm_Confirm();
			DialogResult res = frm_conf.ShowDialog(this, "Syncro will be closed, restored the production data and then started again.");
			if (res == DialogResult.OK)
			{
				Logger.LogError(this, "Terminate Simulation");
				Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = this.startPath + "StartEndSimulation.exe";
        
        //PMASSARO_FI09/02/09 - Added Logs Into StartEndSimulation
        /*
				process.StartInfo.Arguments = "end \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION;
        */
        process.StartInfo.Arguments = "end \"" + this.serverProductionPath + "\" \"" + startPath + "S_Starter.exe\" " + Command.BACKUP_EXTENSION + " " + Command.SYNCRO + " " + SyncroC.Utility.Settings.getSettings().paths.Log;
				process.Start();
				this.CloseSyncroCompletely();
			}
		}

		private void tim_Sim_Tick(object sender, EventArgs e)
		{
			this.CheckSimulationState();
		}
	}
}