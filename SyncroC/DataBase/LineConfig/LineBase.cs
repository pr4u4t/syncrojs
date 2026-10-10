using System;
using System.Collections.Generic;
using System.Text;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	public abstract class LineBase : ConfigBase
	{
		protected LineBase()
		{
			FILE_INI_NAME = "LineConfig.ini";
			Settings s = Settings.getSettings();
      this.fullPathIniFile = s.paths.ClientData + this.FILE_INI_NAME;
		}

		public void Save()
		{
			string s = string.Empty;
			this.SurveyProperties(ref s, -1, -1, 0, ConfigBaseOperation.PropertiesIntoINI);
		}
	}
}
