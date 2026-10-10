using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class RTCSettings : TaskSettings
	{
		public RTCSettings(): base()
    {
			this.sectionName = "S_RTC" + this.sectionName;
		}
	}
}
