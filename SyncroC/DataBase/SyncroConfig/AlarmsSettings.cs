using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class ALARMSSettings : TaskSettings
	{
		bool enable;
		string fileAlarmName_1;

		[Category("2. ALARMS TASK SETTINGS"), Description("Determine whether the ALARMS task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("3. ALARMS FILE NAMES"), Description("Alarm Files name needed for the system.")]
		public string FileAlarmName_1
		{
			get { return fileAlarmName_1; }
			set { fileAlarmName_1 = value; }
		}

		public ALARMSSettings()
			: base()
    {
			this.sectionName = "S_ALARMS" + this.sectionName;
			this.enable = true;
			this.fileAlarmName_1 = "alarms_ver006.ite";
		}
	}
}
