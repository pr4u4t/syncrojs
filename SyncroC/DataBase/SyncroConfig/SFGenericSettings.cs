using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SingleFacerGenericSettings : TaskSettings
	{
		bool enable;
		[Category("2. Single Facer Generic TASK SETTINGS"), Description("Determine whether the Single Facer Generic task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public SingleFacerGenericSettings(string singleFacerNumber)
			: base()
    {
            //PMASSARO_FI26/08/08 - Bug Fix On SF Settings 
			/*
             this.sectionName = "S_BRIDGE" + singleFacerNumber + this.sectionName;
             */
            this.sectionName = "S_SF_GENERIC" + singleFacerNumber + this.sectionName;
			this.enable = false;
		}
	}
}
