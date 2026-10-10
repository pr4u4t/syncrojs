using System;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

using SyncroC.Utility;
using SyncroC.Forms.PopUp;

namespace SyncroC.Utility
{
  public enum LogType
  {
    NoLog=0,
    LogState=1,
    LogIO=2
  }

  public class Twin400LogAnalyzer
  {
    const int MAX_LOG_PATH = 32;
    const int DATE_START_INDEX = 3;
    const int TIME_START_INDEX = 16;
    const int DATE_LENGTH = 10;
    const int TIME_LENGTH = 12;
    const int MIN_LINE_LENGTH = 28;
    public const int START_INFO_LOG_INDEX = 31;
    public bool logAnalyzing = false;
    string logPath = "";
    string backupTitle = "";
    ArrayList logLines = null;
    int lineIndex = -1;
    bool fwd;
    System.IO.StreamReader logReader = null;
    Label lab_Time, lab_path, lbl_Title_YN;
    Timer tim_Log;
    IWin32Window owner;
    private LogType logType = LogType.NoLog;

    public delegate void ShowState(string s);
    ShowState ShowLogState;

    public Twin400LogAnalyzer(LogType logType, Label lab_Path, Label lab_Time, Label lab_Title, Timer tim_log, IWin32Window owner)
    {
      this.logType = logType;
      this.lab_path = lab_Path;
      this.lab_Time = lab_Time;
      this.lbl_Title_YN = lab_Title;
      this.tim_Log = tim_log;
      this.owner = owner;
    }

    public void Destroy()
    {
      this.lab_path = null;
      this.lab_Time = null;
      this.lbl_Title_YN = null;
      this.tim_Log = null;
      this.owner = null;
    }

    public void CloseLogReader()
    {
      if (this.logAnalyzing)
      {
        if (this.logReader != null)
        {
          this.logReader.Close();
          this.logReader = null;
          this.logLines = null;
          this.logPath = "";
          this.lbl_Title_YN.Text = this.backupTitle;
          this.lbl_Title_YN.ForeColor = Color.WhiteSmoke;
          this.lab_path.Visible = false;
          this.lab_Time.Visible = false;
          this.logAnalyzing = false;
          this.ShowLogState = null;
        }
      }
    }

    public bool OpenLogReader(ShowState ShowLog)
    {
      if (!this.logAnalyzing)
      {
        if (this.logReader == null)
        {
          OpenFileDialog frm = new OpenFileDialog();
          frm.FileName = "Twin400.State.txt";
          frm.CheckFileExists = true;
          frm.Filter = "Log files|*.txt;*.old;*.log|All files|*.*";
          if (frm.ShowDialog() == DialogResult.OK)
          {
            string filePath = frm.FileName;
            this.ShowLogState = ShowLog;
            this.logReader = new System.IO.StreamReader(filePath, System.Text.Encoding.UTF8);
            this.logLines = new ArrayList();
            this.logPath = filePath;
            this.backupTitle = this.lbl_Title_YN.Text;
            this.lbl_Title_YN.Text = this.logPath;
            this.lbl_Title_YN.ForeColor = Color.Yellow;
            this.lineIndex = -1;
            this.lab_path.Visible = true;
            this.lab_Time.Visible = true;
            this.lab_path.Text = this.logPath;
            this.logAnalyzing = true;
            this.lab_Time.Text = this.tim_Log.Interval.ToString();
            return true;
          }
        }
      }
      return false;
    }

    public string LogForward(bool updateTime)
    {
      string readLine = "";
      if (this.logAnalyzing && (this.logReader != null) && (this.logLines != null))
      {
        if (this.lineIndex == this.logLines.Count - 1)
        {
          if (!this.logReader.EndOfStream)
          {
            readLine = this.logReader.ReadLine();
            this.logLines.Add(readLine);
            this.lineIndex++;
          }
        }
        else
        {
          if ((this.lineIndex >= 0) && (this.lineIndex < this.logLines.Count - 1))
            readLine = (string)this.logLines[++this.lineIndex];
        }
      }
      if (updateTime) this.WriteLogTitle(readLine);
      return readLine;
    }

    public string LogBackward(bool updateTime)
    {
      string readLine = "";
      if (this.logAnalyzing && (this.logReader != null) && (this.logReader != null))
      {
        if (this.lineIndex > 0)
          readLine = (string)this.logLines[--this.lineIndex];
      }
      if (updateTime) this.WriteLogTitle(readLine);
      return readLine;
    }

    private void WriteLogTitle(string readLine)
    {
      if (readLine.Length > MIN_LINE_LENGTH)
      {
        this.lbl_Title_YN.Text = this.GetLogDate(readLine) + "  " + GetLogTime(readLine);
      }
    }

    private string GetLogTime(string readLine)
    {
      return readLine.Substring(TIME_START_INDEX, TIME_LENGTH);
    }

    private string GetLogDate(string readLine)
    {
      return readLine.Substring(DATE_START_INDEX, DATE_LENGTH);
    }

    public void FwdStop()
    {
      if (this.logAnalyzing)
      {
        this.fwd = true;
        this.tim_Log.Enabled = !this.tim_Log.Enabled;
        this.lbl_Title_YN.ForeColor = (this.tim_Log.Enabled ? Color.Black : Color.Yellow);
      }
    }

    public void RevStop()
    {
      if (this.logAnalyzing)
      {
        this.fwd = false;
        this.tim_Log.Enabled = !this.tim_Log.Enabled;
        this.lbl_Title_YN.ForeColor = (this.tim_Log.Enabled ? Color.Blue : Color.Yellow);
      }
    }

    private bool CheckReadLine(string readLine)
    {
      bool breakLoop = false;
      if (this.logType == LogType.LogState)
      {
        if (((readLine.Length > START_INFO_LOG_INDEX + 1) && (readLine.Substring(START_INFO_LOG_INDEX, 2) != "IO"))
           || (readLine == ""))
          breakLoop=true;
      }
      else if (this.logType == LogType.LogIO)
      {
        if (((readLine.Length > START_INFO_LOG_INDEX + 1) && (readLine.Substring(START_INFO_LOG_INDEX, 2) == "IO"))
           || (readLine == ""))
          breakLoop = true;
      }
      return breakLoop;
    }

    public bool AheadLog()
    {
      string readLine = "";
      if (this.logAnalyzing)
      {
        do
        {
          readLine = this.LogForward(true);
          if (this.CheckReadLine(readLine))
            break;
        } while (true);
        this.ShowLogState(readLine);
      }
      return (readLine != "") && (this.logType != LogType.NoLog);
    }

    private bool GoToDateTime(string date, string time)
    {
      string readLine = "";
      if (this.logAnalyzing)
      {
        do
        {
          readLine = this.LogForward(true);
          if (this.CheckReadLine(readLine))
          {
            if (readLine == "") break;
            string dateRead = this.GetLogDate(readLine);
            if (this.CompareDates(dateRead, date))
            {
              string timeRead = this.GetLogTime(readLine);
              if (this.CompareTimes(timeRead, time))
                break;
            }
          }
        } while (true);
        this.ShowLogState(readLine);
      }
      return (readLine != "") && (this.logType != LogType.NoLog);
    }

    private void GoToEnd()
    {
      string readLine = "";
      if (this.logAnalyzing)
      {
        do
        {
          readLine = this.LogForward(false);
        } while (readLine!="");
      }
    }

    private void GoToStart()
    {
      string readLine = "";
      if (this.logAnalyzing)
      {
        do
        {
          readLine = this.LogBackward(false);
        } while (readLine != "");
      }
    }

    public bool BackLog()
    {
      string readLine = "";
      if (this.logAnalyzing)
      {
        do
        {
          readLine = this.LogBackward(true);
          if (this.CheckReadLine(readLine))
            break;
        } while (true);
        this.ShowLogState(readLine);
      }
      return (readLine != "") && (this.logType != LogType.NoLog);
    }

    public bool CompareDates(string date1, string date2)
    {
      //dates are supposed to be dd-mm-yyyy, where - is any separator
      bool res = false;
      if ((date1 == "") || (date2 == "")) return true;
      if ((date1.Length < 2) || (date2.Length < 2) || (date1.Substring(0, 2) == date2.Substring(0, 2)))
        if ((date1.Length < 5) || (date2.Length < 5) || (date1.Substring(3, 2) == date2.Substring(3, 2)))
          if ((date1.Length < 8) || (date2.Length < 8) || (date1.Substring(6, 4) == date2.Substring(6, 4)))
            res = true;
      return res;
    }

    public bool CompareTimes(string time1, string time2)
    {
      //times are supposed to be hh:mm:ss, where : is any separator
      bool res = false;
      if ((time1 == "") || (time2 == "")) return true;
      if ((time1.Length < 2) || (time2.Length < 2) || (time1.Substring(0, 2) == time2.Substring(0, 2)))
        if ((time1.Length < 5) || (time2.Length < 5) || (time1.Substring(3, 2) == time2.Substring(3, 2)))
          if ((time1.Length < 8) || (time2.Length < 8) || (time1.Substring(6, 2) == time2.Substring(6, 2)))
            res = true;
      return res;
    }

    public void tim_Log_Tick()
    {
      bool ok = false;
      if (this.logAnalyzing)
      {
        if (this.fwd)
          ok = this.AheadLog();
        else
          ok = this.BackLog();
        if (!ok)
        {
          this.tim_Log.Enabled = false;
          this.lbl_Title_YN.ForeColor = Color.Yellow;
        }
      }
    }

    public void ChangeLogSpeed()
    {
      if (this.logAnalyzing)
      {
        Frm_AskValue askValue = new Frm_AskValue();
        FosberDialogResult ret = askValue.ShowDialog(this.owner, "Interval in ms", this.tim_Log.Interval);

        if (ret.Dialog == DialogResult.OK)
        {
          this.tim_Log.Interval = (int)ret.Data[0];
          this.lab_Time.Text = this.tim_Log.Interval.ToString();
        }
      }
    }

    public void ChangeDateTime()
    {
      string date, time;
      if (this.logAnalyzing)
      {
        Frm_AskString askValue = new Frm_AskString();
        FosberDialogResult ret = askValue.ShowDialog(this.owner, "Date (dd/mm/yyyy) or START or END ", 0);

        if (ret.Dialog == DialogResult.OK)
        {
          date = (string)ret.Data[0];
          if ((date.ToUpper() != "START") && (date.ToUpper() != "END"))
          {
            askValue = new Frm_AskString();
            ret = askValue.ShowDialog(this.owner, "Time (hh:mm:ss) or part of it", 0);
            if (ret.Dialog == DialogResult.OK)
            {

              time = (string)ret.Data[0];
              this.GoToDateTime(date, time);
            }
            else
              this.GoToDateTime(date, "");
          }
          else if (date.ToUpper() == "START")
          {
            this.GoToStart();
            this.AheadLog();
          }
          else if (date.ToUpper() == "END")
          {
            this.GoToEnd();
            this.BackLog();
          }
        }
      }
    }
  }
}
