using System;
using System.Collections.Generic;
using System.Text;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase.SyncroConfig
{
	public abstract class SyncroBase : ConfigBase
	{
		protected SyncroBase()
		{
			FILE_INI_NAME = "Syncro.ini";

			Settings s = Settings.getSettings();
			if (s.configuration.getOnServer())
			{
        this.fullPathIniFile = Settings.getSettings().paths.ServerConfig + this.FILE_INI_NAME;
			}
			else
			{
        this.fullPathIniFile = Settings.getSettings().paths.ClientData + this.FILE_INI_NAME;
			}
		}

		/// <summary>
		/// Only the OnServer client save this database. It is saved into the _server\data folder
		/// </summary>
		public void Save()
		{
			Settings settings = Settings.getSettings();
			if (settings.configuration.getOnServer())
			{
				string s = string.Empty;
				this.SurveyProperties(ref s, -1, -1, 0, ConfigBaseOperation.PropertiesIntoINI);
			}
		}

		public void SendRelodSettings()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			Settings settings = Settings.getSettings();
			if (settings.configuration.getOnServer())
			{
        cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.RELOAD_SETTINGS + "()"));
			}
		}
	}
}
