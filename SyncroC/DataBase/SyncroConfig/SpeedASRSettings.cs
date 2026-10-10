using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SPEED_ASRSettings : TaskSettings
	{
		bool enable;

		[Category("2. SPEED ASR TASK SETTINGS"), Description("Determine whether the SPEED ASR task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public SPEED_ASRSettings()
			: base()
    {
			this.sectionName = "S_SPEED_ASR" + this.sectionName;
			this.enable = true;
		}
	}
}
