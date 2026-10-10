//agrassi_FI16-11-2010_2
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class Twin400MacCommsBHSSettings : BaseComms 
  {
    string networkPath = "";
		string fileName = "";
    string userName = "";
    string password = "";
    //espaly2
    bool readScoreGap = true;

		[Category("3. Twin400 Mac Comms BHS"), Description("The name of the file that contains the order data")]
		public string FileName
		{
			get { return this.fileName; }
			set { this.fileName = value; }
		}

    [Category("3. Twin400 Mac Comms BHS"), Description("The path of the directory where the file is (terminated with \\)")]
    public string NetworkPath
    {
      get { return this.networkPath; }
      set { this.networkPath = value; }
    }
    
    [Category("3. Twin400 Mac Comms BHS"), Description("User name for the access to the shared folder")]
		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}

		[Category("3. Twin400 Mac Comms BHS"), Description("Password for the access to the shared folder")]
		public string Password
		{
			get { return this.password; }
			set { this.password = value; }
		}

    [Category("3. Twin400 Mac Comms BHS"), Description("Mettere a true se il valore dello score gap deve essere letto da BHS")]
    public bool ReadScoreGap
    {
      get { return this.readScoreGap; }
      set { this.readScoreGap = value; }
    }

    public Twin400MacCommsBHSSettings()
      : base("S_SS_FOSBER_22.60_Mac_Comms_BHS")
    {
    }
	}
	
}
