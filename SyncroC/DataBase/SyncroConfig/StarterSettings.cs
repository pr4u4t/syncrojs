using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class StarterSettings : TaskSettings
	{
		private bool checkAliveTask;
		[Category("2. STARTER TASK SETTINGS"), Description("Enable check for tasks to be alive and restart failed ones (restart is to be implemented yet).")]
		public bool CheckAliveTask
		{
			get { return checkAliveTask; }
			set { checkAliveTask = value; }
		}

		private bool startAllTask;
		[Category("2. STARTER TASK SETTINGS"), Description("")]
		public bool StartAllTask
		{
			get { return startAllTask; }
			set { startAllTask = value; }
		}

		private bool startClient;
		[Category("2. STARTER TASK SETTINGS"), Description("Determine whether the graphic client application should be automatically started on the server computer.")]
		public bool StartClient
		{
			get { return startClient; }
			set { startClient = value; }
		}

		private int waitBeforeStart;
		[Category("2. STARTER TASK SETTINGS"), Description("Time to be waited before start the Syncro tasks.")]
		public int WaitBeforeStart
		{
			get { return waitBeforeStart; }
			set { waitBeforeStart = value; }
		}


		public StarterSettings(): base()
    {
			this.sectionName = "S_STARTER" + this.sectionName;

			this.checkAliveTask = true;
			this.startAllTask = true;
			this.startClient = true;
			this.waitBeforeStart = 20000;
		}
	}
}
