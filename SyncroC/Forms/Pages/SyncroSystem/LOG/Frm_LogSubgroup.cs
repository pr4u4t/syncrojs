using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.LOG
{
	public partial class Frm_LogSubgroup : Frm_SyncroSystemGroup
	{
    protected FileInfo file1 = null;
    protected int bytesRead1 = 0;

		public Frm_LogSubgroup()
		{
			InitializeComponent();
		}

		protected override void F7()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_Log1));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_Log2));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_Log3));
		}

		protected override void F13()
		{
			askForPage(typeof(Frm_Log4));
		}

    protected void updateLog(string fileName, ref FileInfo file, ref int bytesRead, ref TextBox textBoxCurrent, ref TextBox textBoxOld)
    {
      if (file == null)
      {
        string fullPathfile = string.Empty;
        if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
          fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedOnServer;
        else
          fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedFromNetwork;

        fullPathfile += fileName;

        try
        {
          file = new FileInfo(fullPathfile);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "updateLog - Error looking for file " + file, ex);
        }
      }
      else
      {
        try
        {
          file.Refresh();
        }
        catch (Exception ex)
        {
          file = null;
          Logger.LogError(this, "updateLog - Error refreshing file " + file, ex);
        }
      }

      if (file != null)
      {
        int newBytes = 0;
        try
        {
          newBytes = (int)(file.Length - bytesRead);
        }
        catch (Exception ex)
        {
          bytesRead = 0;
          textBoxCurrent.Clear();
          textBoxOld.Clear();
          Logger.LogError(this, "updateLog - Error accessing info of file " + file, ex);
        }
        if (newBytes != 0)
        {
          FileStream stream = null;
          try
          {
            //ABello_FI09/07/2007_1 I Log sulla cartella Shared ogni tanto non venivano scritti
            //stream = file.OpenRead();
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
          }
          catch (IOException ex)
          {
            Logger.LogError(this, "updateLog - file already opened, wait a little while " + file, ex);
            System.Threading.Thread.Sleep(10);
          }
          catch (Exception ex)
          {
            bytesRead = 0;
            textBoxCurrent.Clear();
            textBoxOld.Clear();
            Logger.LogError(this, "updateLog - Error opening file " + file, ex);
          }
          if ((newBytes < 0) && (stream != null))
          {
            bytesRead = 0;
            try
            {
              newBytes = (int)file.Length;
            }
            catch (Exception ex)
            {
              Logger.LogError(this, "updateLog - Error accessing info of file " + file, ex);
            }
            textBoxCurrent.Clear();
            textBoxOld.Clear();
          }
          if ((newBytes > 0) && (stream != null))
          {
            Byte[] buffer = new Byte[newBytes];
            try
            {
              stream.Position = bytesRead;
              stream.Read(buffer, 0, newBytes);
              bytesRead += newBytes;
            }
            catch (Exception ex)
            {
              Logger.LogError(this, "updateLog - Error reading from file " + file, ex);
            }

            string str = string.Empty;
            try
            {
              //agrassi_FI15-04-08
              if (DBManager.getDBManager().syncroConfig.GeneralSetting.LogDiskUTF8)
                str = Encoding.UTF8.GetString(buffer);
              else
                str = Encoding.Unicode.GetString(buffer);
            }
            catch (Exception ex)
            {
              Logger.LogError(this, "updateLog - Error encoding bytes from file " + file, ex);
              str = "  * ERROR ENCODING BYTES *  ";
            }

            int remainingSpace = textBoxCurrent.MaxLength - textBoxCurrent.TextLength;
            while (str.Length > remainingSpace)
            {
              textBoxCurrent.AppendText(str.Substring(0, remainingSpace));
              str = str.Substring(remainingSpace);
              textBoxOld.Text = textBoxCurrent.Text;
              textBoxCurrent.Clear();
              remainingSpace = textBoxCurrent.MaxLength;
            }
            textBoxCurrent.AppendText(str);
          }
        }
      }
    }

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageSyncroSystem_Speed) for the SyncroSystem_Speed subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSyncroSystem_Log = this.getRequestCommand();
			base._Show();
		}
	}
}