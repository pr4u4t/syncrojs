using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.DataBase;

//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
using System.Collections;
using System.Reflection;
using System.Threading;
using SyncroC.Controls.Colors;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Tools;
using SyncroC.Forms.PopUp;
//

namespace SyncroC.Forms.Pages.Production.Operator
{
  public partial class Frm_Printings : SyncroC.Forms.Pages.Production.Operator.Frm_OperatorSubGroup
  {
    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    private DBManager dbm;

    const string PALLET_LABEL_PREFIX = "LAB_";
    const string PAST_ORDER_LABEL_PREFIX = "ORD_";
    const string SHIFT_REPORT_PREFIX = "SHF_";
    const string PAST_ROLLS_LABEL_PREFIX = "ROL_";

    string bitmapPath;
    public Frm_Printings()
    {
      InitializeComponent();

      //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
      dbm = DBManager.getDBManager();

			if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
				this.bitmapPath = SyncroC.Utility.Settings.getSettings().paths.ServerSharedOnServer;
			else
				this.bitmapPath = SyncroC.Utility.Settings.getSettings().paths.ServerSharedFromNetwork;
      //agrassi_FI07-07-08_2
      if ((!dbm.lineConfig.PRINTERSConfig.EnablePalletLabel) || (!dbm.lineConfig.PRINTERSConfig.PrintPalletLabelUsingDigitalInput))
        this.lbl_CtrlF3_YY.Text = this.lbl_CtrlF4_YY.Text = this.lbl_CtrlF5_YY.Text = "";
		}

    protected override void F11()
    {
    }

    private void Frm_Printings_Activated(object sender, EventArgs e)
    {
      this.UpdateBitmaps();
      this.tim_Refresh.Enabled = true;
      this.grd_Bitmap.Focus();
      if (this.grd_Bitmap.getItems().Count > 0)
        this.grd_Bitmap.SelectIndex(0);
    }

    private void grd_Bitmap_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.grd_Bitmap.getSelectedItems().Count != 1)
        return;
      string bitmapName = this.grd_Bitmap.getSelectedItems()[0].Text;
      string path = Path.GetFullPath(this.bitmapPath);
      string bitmapPath = path + bitmapName;
      FileInfo f = new FileInfo(bitmapPath);
      this.WriteInfo(bitmapName, f.LastWriteTime);
      Graphics g = this.pct_Bitmap.CreateGraphics();
      try
      {
        if (File.Exists(bitmapPath))
          g.DrawImage(Image.FromFile(bitmapPath), new Rectangle(0, 0, this.pct_Bitmap.Width, this.pct_Bitmap.Height));
        else
          g.DrawString("Bitmap not found!", new Font("Arial", 15), Brushes.Black, 0, 0);
      }
      catch (Exception ex)
      {
        g.DrawString("Error reading file " + bitmapPath, new Font("Arial", 15), Brushes.Black, 0, 0);
        Logger.LogError(this, ex);
      }
    }

    protected override void CtrlF1()
    {
      if (this.grd_Bitmap.getSelectedItems().Count != 1)
        return;

      this.lbl_CtrlF1.BackColor = this.lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;

      string bitmapName = this.grd_Bitmap.getSelectedItems()[0].Text;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_DB_MANAGER, "Printings.ReprintBitmap(" + bitmapName + ")");
      Application.DoEvents();
      System.Threading.Thread.Sleep(1000);
      this.lbl_CtrlF1.BackColor = this.lbl_CtrlF1_YY.BackColor = this.lbl_Title_YN.BackColor;
      Application.DoEvents();
    }

    protected override void CtrlF2()
    {
      this.lbl_CtrlF2.BackColor = this.lbl_CtrlF2_YY.BackColor = lbl_Status.BackColor;
      this.UpdateBitmaps();
      Application.DoEvents();
      System.Threading.Thread.Sleep(1000);
      this.lbl_CtrlF2.BackColor = this.lbl_CtrlF2_YY.BackColor = this.lbl_Title_YN.BackColor;
      Application.DoEvents();
    }

    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    protected override void CtrlF3()
    {
      int indexValue = 0;
      this.lbl_CtrlF3.BackColor = this.lbl_CtrlF3_YY.BackColor = lbl_Status.BackColor;
      int lev = 1;
      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount < 1) //Level Not Enabled
      {
        lev = 2;
      }

      if (this.AskForIndexPosition(lev, out indexValue) == true)
      {
        UpdatePrinterIndex(lev, indexValue);
      }

      Application.DoEvents();
      System.Threading.Thread.Sleep(300);
      this.lbl_CtrlF3.BackColor = this.lbl_CtrlF3_YY.BackColor = this.lbl_Title_YN.BackColor;
      Application.DoEvents();
    }

    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    protected override void CtrlF4()
    {
      int indexValue = 0;
      this.lbl_CtrlF4.BackColor = this.lbl_CtrlF4_YY.BackColor = lbl_Status.BackColor;
      int lev = 2;
      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount <= 2) //Level Not Enabled
      {
        lev = 3;
      }

      if (this.AskForIndexPosition(lev, out indexValue) == true)
      {
        UpdatePrinterIndex(lev, indexValue);
      }

      Application.DoEvents();
      System.Threading.Thread.Sleep(300);
      this.lbl_CtrlF4.BackColor = this.lbl_CtrlF4_YY.BackColor = this.lbl_Title_YN.BackColor;
      Application.DoEvents();
    }

    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    protected override void CtrlF5()
    {
      int indexValue = 0;
      this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
      int lev = 3;
      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount == 2) //Level Not Enabled
      {
        lev = 2;
      }

      if (this.AskForIndexPosition(lev, out indexValue) == true)
      {
        UpdatePrinterIndex(lev, indexValue);
      }

      Application.DoEvents();
      System.Threading.Thread.Sleep(300);
      this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = this.lbl_Title_YN.BackColor;
      Application.DoEvents();
    }

    private void _UpdateStatusPrintingIdx(bool enableLiv1, int idLiv1, int modidLiv1, string setupCodLiv1, bool enableLiv2, int idLiv2, int modidLiv2, string setupCodLiv2, bool enableLiv3, int idLiv3, int modidLiv3, string setupCodLiv3)
    {
      //string separ = "   -   ";

      return;
    }

    private void MakeAPrint_L(object sender, EventArgs e)
    {
      int lev = 1;
      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount < 1) //Level Not Enabled
      {
        return;
      }

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".PrintNowData()");

      Application.DoEvents();
      System.Threading.Thread.Sleep(2000);

      return;
    }

    private void MakeAPrint_M(object sender, EventArgs e)
    {
      int lev = 2;
      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount <= 2) //Level Not Enabled
      {
        return;
      }

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".PrintNowData()");

      Application.DoEvents();
      System.Threading.Thread.Sleep(2000);

      return;
    }

    private void MakeAPrint_U(object sender, EventArgs e)
    {
      int lev = 3;
      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount < 2) //Level Not Enabled
      {
        return;
      }

      if (knife.LevelCount == 2) //Level Not Enabled
      {
        lev = 2;
      }

      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".PrintNowData()");

      Application.DoEvents();
      System.Threading.Thread.Sleep(2000);

      return;
    }

    private void UpdateBitmaps()
    {
      this.grd_Bitmap.ClearItems();
      try
      {
        string path = Path.GetFullPath(this.bitmapPath);
        string[] labelFileNames = Directory.GetFiles(path, PALLET_LABEL_PREFIX + "*.bmp");
        DateTime[] labelFileTimes = new DateTime[labelFileNames.Length];
        for (int i = 0; i < labelFileNames.Length; i++)
        {
          FileInfo fi = new FileInfo(labelFileNames[i]);
          labelFileTimes[i] = fi.LastWriteTime;
        }
        Array.Sort(labelFileTimes, labelFileNames);
        for (int i = labelFileNames.Length - 1; i >= 0; i--)
        {
          int indLastSlash = labelFileNames[i].LastIndexOf('\\');
          string bitmapName = labelFileNames[i].Substring(indLastSlash + 1);
          this.grd_Bitmap.AddItem(new ListViewItem(bitmapName));
        }

        string[] pastOrderFileNames = Directory.GetFiles(path, PAST_ORDER_LABEL_PREFIX + "*.bmp");
        DateTime[] pastOrderFileTimes = new DateTime[pastOrderFileNames.Length];
        for (int i = 0; i < pastOrderFileNames.Length; i++)
        {
          FileInfo fi = new FileInfo(pastOrderFileNames[i]);
          pastOrderFileTimes[i] = fi.LastWriteTime;
        }
        Array.Sort(pastOrderFileTimes, pastOrderFileNames);
        for (int i = pastOrderFileNames.Length - 1; i >= 0; i--)
        {
          int indLastSlash = pastOrderFileNames[i].LastIndexOf('\\');
          string bitmapName = pastOrderFileNames[i].Substring(indLastSlash + 1);
          this.grd_Bitmap.AddItem(new ListViewItem(bitmapName));
        }

        string[] pastRollsFileNames = Directory.GetFiles(path, PAST_ROLLS_LABEL_PREFIX + "*.bmp");
        DateTime[] pastRollsFileTimes = new DateTime[pastRollsFileNames.Length];
        for (int i = 0; i < pastRollsFileNames.Length; i++)
        {
          FileInfo fi = new FileInfo(pastRollsFileNames[i]);
          pastRollsFileTimes[i] = fi.LastWriteTime;
        }
        Array.Sort(pastRollsFileTimes, pastRollsFileNames);
        for (int i = pastRollsFileNames.Length - 1; i >= 0; i--)
        {
          int indLastSlash = pastRollsFileNames[i].LastIndexOf('\\');
          string bitmapName = pastRollsFileNames[i].Substring(indLastSlash + 1);
          this.grd_Bitmap.AddItem(new ListViewItem(bitmapName));
        }

        string[] shiftFileNames = Directory.GetFiles(path, SHIFT_REPORT_PREFIX + "*.bmp");
        DateTime[] shiftFileTimes = new DateTime[shiftFileNames.Length];
        for (int i = 0; i < shiftFileNames.Length; i++)
        {
          FileInfo fi = new FileInfo(shiftFileNames[i]);
          shiftFileTimes[i] = fi.LastWriteTime;
        }
        Array.Sort(shiftFileTimes, shiftFileNames);
        for (int i = shiftFileNames.Length - 1; i >= 0; i--)
        {
          int indLastSlash = shiftFileNames[i].LastIndexOf('\\');
          string bitmapName = shiftFileNames[i].Substring(indLastSlash + 1);
          this.grd_Bitmap.AddItem(new ListViewItem(bitmapName));
        }
      }
      catch (Exception e)
      {
        Logger.LogError(this, e);
      }
    }

    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    private void UpdatePrinterIndex(int lev, int indexValue)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      switch (indexValue)
      {
        case 0:
          {
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".UpdatePrinterIndexAtCurrent()");
            break;
          }
        case -1:
          {
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".UpdatePrinterIndexAtPrevious()");
            break;
          }
        case -2:
          {
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".UpdatePrinterIndexAtSecondPrevious()");
            break;
          }
        case -3:
          {
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".UpdatePrinterIndexAtThirdPrevious()");
            break;
          }
        case -4:
          {
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".UpdatePrinterIndexAtFourthPrevious()");
            break;
          }
        case -5:
          {
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.STACKER + lev.ToString() + ".UpdatePrinterIndexAtFifthPrevious()");
            break;
          }
        default:
          {
            break;
          }
      }
    }

    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    private bool AskForIndexPosition(int level, out int returnedIdx)
    {
      returnedIdx = 0; //just to be sure
      string text = "";
      PopUp.Frm_Alert alertFRM = new Frm_Alert();

      SyncroC.DataBase.LineConfig.PRINTERS printer = null;
      printer = dbm.lineConfig.PRINTERSConfig;

      bool levelEnabled = false;
      SyncroC.DataBase.LineConfig.Stacker stk = null;

      switch (level)
      {
        case 1:
          {
            stk = dbm.lineConfig.Stacker1Config;
            if (printer.EnablePalletLabel == true)
            {
              levelEnabled = true;
            }
            break;
          }
        case 2:
          {
            stk = dbm.lineConfig.Stacker2Config;
            if (printer.EnablePalletLabel == true)
            {
              levelEnabled = true;
            }
            break;
          }
        case 3:
          {
            stk = dbm.lineConfig.Stacker3Config;
            if (printer.EnablePalletLabel == true)
            {
              levelEnabled = true;
            }
            break;
          }
        default:
          {
            levelEnabled = false;
            break;
          }
      }

      if (levelEnabled == false || printer.PrintPalletLabelUsingDigitalInput == false)
      {
        text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_185#");
        alertFRM.ShowDialog(this, text);
        return false;
      }

      SyncroC.DataBase.LineConfig.Knife knife = null;
      knife = dbm.lineConfig.KnifeConfig;

      switch (knife.LevelCount)
      {
        case 1:
          {
            if (level > 1 || level < 1)
            {
              text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_186#");
              alertFRM.ShowDialog(this, text);
              return false;
            }
            break;
          }
        case 2:
          {
            if (level > 2 || level < 1)
            {
              text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_186#");
              alertFRM.ShowDialog(this, text);
              return false;
            }
            break;
          }
        case 3:
          {
            if (level > 3 || level < 1)
            {
              text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_186#");
              alertFRM.ShowDialog(this, text);
              return false;
            }
            break;
          }
      }

      text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_188#") +
             "\n(0 -> - " +
             (String)(stk.MaxOrdersStackerCanManageOnBelts - 1).ToString() +
             ")\n" +
             DBManager.getDBManager().message.getTranslatedMessage("§SYCC_190#") +
             " 0,\n" + DBManager.getDBManager().message.getTranslatedMessage("§SYCC_191#") +
             "-1,...";
      Frm_AskValueAndConfirm confirm = new Frm_AskValueAndConfirm();
      FosberDialogResult res = confirm.ShowDialog(this, text, 0);
    testAgain:
      if (res.Dialog == DialogResult.OK)
      {
        int tmpIdx = (int)res.Data[0];
        if (tmpIdx < -(stk.MaxOrdersStackerCanManageOnBelts - 1) || tmpIdx > 0)
        {
          confirm.Close();

          Frm_AskValueAndConfirm confirmNew = new Frm_AskValueAndConfirm();

          //Error. Visualize Again the Value
          string textError = "";
          textError = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_189#") +
                 "\n(0 -> - " +
                 (stk.MaxOrdersStackerCanManageOnBelts - 1).ToString() +
                 ")\n" +
                 DBManager.getDBManager().message.getTranslatedMessage("§SYCC_190#") +
                 " 0,\n" + DBManager.getDBManager().message.getTranslatedMessage("§SYCC_191#") +
                 " -1 ...";
          res = confirmNew.ShowDialog(this, textError, tmpIdx);
          goto testAgain;
        }
        else
        {
          returnedIdx = (int)res.Data[0];
          return true;
        }
      }
      else
      {
        text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_187#");
        alertFRM.ShowDialog(this, text);
        return false;
      }
    }

    private void tim_Refresh_Tick(object sender, EventArgs e)
    {
      if (this.Visible)
        this.UpdateBitmaps();
    }

    private void grd_Bitmap_LostFocus(object sender, EventArgs e)
    {
    }

    private void WriteInfo(string name, DateTime time)
    {
      string setupCode = string.Empty;
      string shiftId = string.Empty;
      //agrassi_FI14-03-08_1
      string setupIdAndPerhapsCode = string.Empty;
      int setupId = 0;
      DateTime date = DateTime.Now;
      KnifeLevel level = KnifeLevel.NoLevel;
      int number = 0;
      int ind1, ind2, ind3, ind4;
      string sDate;
      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      Language lang = DBManager.getDBManager().language;
      this.lbl_DateAndTimeValue.Text = time.ToShortDateString() + " " + time.ToLongTimeString();
      if (name.StartsWith(PALLET_LABEL_PREFIX) || name.StartsWith(PAST_ORDER_LABEL_PREFIX))
      {
        this.lbl_CodeOrShiftId.Text = msg.getTranslatedMessage("§SYCC_103");
        this.lbl_LevelOrDate.Text = msg.getTranslatedMessage("§SYCC_105");
        ind1 = name.IndexOf('_');
        if (ind1 > 0)
        {
          ind2 = name.IndexOf('_', ind1 + 1);
          if (ind2 > 0)
          {
            //agrassi_FI14-03-08_1
            //setupId = this.intParse(name.Substring(ind1 + 1, ind2 - ind1 - 1));
            //setupCode = setupId.ToString();
            setupIdAndPerhapsCode = name.Substring(ind1 + 1, ind2 - ind1 - 1);
            if (setupIdAndPerhapsCode.Length > 4)
            {
              setupId = this.intParse(setupIdAndPerhapsCode.Substring(0, 4));
              setupCode = setupIdAndPerhapsCode.Substring(4);
            }
            else if (setupIdAndPerhapsCode.Length == 4)
              setupId = this.intParse(setupIdAndPerhapsCode);
            DataRow row = DBManager.getDBManager().order.getRow(setupId.ToString());
            if (row != null)
              setupCode = row[Order.DEL_SETUP_CODE].ToString();
            else
            {
              row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString());
              if (row != null)
                setupCode = row[PastOrder.POL_SETUP_CODE].ToString();
            }
            //agrassi_FI26-03-08
            //level = (KnifeLevel)this.intParse(name.Substring(ind2 + 1, 1));
            //ind3 = name.IndexOf('_', ind2 + 1);
            //if (ind3 > 0)
            //  number = this.intParse(name.Substring(ind3 + 1, 2));
            ind3 = name.IndexOf('_', ind2 + 1);
            ind4 = name.IndexOf('_', ind3 + 1);
            int indLev = (ind4 < 0) ? ind2 : ind3;
            int indNum = (ind4 < 0) ? ind3 : ind4;
            int indDot = name.IndexOf('.', indNum + 1);
            if (indLev > 0)
              level = (KnifeLevel)this.intParse(name.Substring(indLev + 1, 1));
            if ((indNum > 0) && (indDot > 0))
              number = this.intParse(name.Substring(indNum + 1, indDot - indNum - 1));
          }
        }
        this.lbl_CodeOrShiftIdValue.Text = setupCode;
        this.lbl_LevelOrDateValue.Text = lang.getTranslation(typeof(KnifeLevel).Name + "." + level.ToString());
        if (name.StartsWith(PALLET_LABEL_PREFIX))
        {
          this.lbl_Type.Text = msg.getTranslatedMessage("§SYCC_100");
          this.lbl_Number.Visible = true;
          this.lbl_Number.Text = msg.getTranslatedMessage("§SYCC_107");
          this.lbl_NumberValue.Visible = true;
          this.lbl_NumberValue.Text = number.ToString();
        }
        else
        {
          this.lbl_Type.Text = msg.getTranslatedMessage("§SYCC_101");
          this.lbl_Number.Visible = false;
          this.lbl_NumberValue.Visible = false;
        }
      }
      else if (name.StartsWith(SHIFT_REPORT_PREFIX))
      {
        this.lbl_Type.Text = msg.getTranslatedMessage("§SYCC_102");
        this.lbl_Number.Visible = true;
        this.lbl_Number.Text = msg.getTranslatedMessage("§SYCC_108");
        this.lbl_NumberValue.Visible = true;
        this.lbl_CodeOrShiftId.Text = msg.getTranslatedMessage("§SYCC_104");
        this.lbl_LevelOrDate.Text = msg.getTranslatedMessage("§SYCC_106");
        ind1 = name.IndexOf('_');
        if (ind1 > 0)
        {
          ind2 = name.IndexOf('_', ind1 + 1);
          if (ind2 > 0)
          {
            shiftId = name.Substring(ind1 + 1, ind2 - ind1 - 1);
            ind3 = name.IndexOf('_', ind2 + 1);
            if (ind3 > 0)
            {
              sDate = name.Substring(ind2 + 1, ind3 - ind2 - 1);
              date = new DateTime(this.intParse(sDate.Substring(4, 4)), this.intParse(sDate.Substring(2, 2)), this.intParse(sDate.Substring(0, 2)));
              number = this.intParse(name.Substring(ind3 + 1, 1));
            }
          }
        }
        this.lbl_CodeOrShiftIdValue.Text = shiftId;
        this.lbl_LevelOrDateValue.Text = date.ToShortDateString();
        this.lbl_NumberValue.Text = number.ToString();
      }
      else if (name.StartsWith(PAST_ROLLS_LABEL_PREFIX))
      {
        this.lbl_Type.Text = msg.getTranslatedMessage("§SYCC_109");
        this.lbl_Number.Visible = true;
        this.lbl_Number.Text = msg.getTranslatedMessage("§SYCC_110");
        this.lbl_NumberValue.Visible = true;
        this.lbl_CodeOrShiftId.Text = msg.getTranslatedMessage("§SYCC_111");
        this.lbl_LevelOrDate.Text = msg.getTranslatedMessage("§SYCC_112");
        ind1 = name.IndexOf('_');
        if (ind1 > 0)
        {
          //PMASSARO_FI14/09/10 - Andava in errore la grafica quando veniva selezionata un bitmap di bobina identificata come "Not_Used"
          /* ind2 = name.IndexOf('_', ind1 + 1); */
          string paper_not_used = "NOT_USED";
          int ind_not_used = name.IndexOf(paper_not_used, ind1 + 1);
          if (ind_not_used == ind1+1)
            ind2 = name.IndexOf('_', ind_not_used + paper_not_used.Length);
          else
            ind2 = name.IndexOf('_', ind1 + 1);

          if (ind2 > 0)
          {
            shiftId = name.Substring(ind1 + 1, ind2 - ind1 - 1);
            ind3 = name.IndexOf('_', ind2 + 1);
            if (ind3 > 0)
            {
              sDate = name.Substring(ind2 + 1, ind3 - ind2 - 1);
              DateTime now = DateTime.Now;
              date = new DateTime(now.Year, now.Month, now.Day, this.intParse(sDate.Substring(0, 2)), this.intParse(sDate.Substring(2, 2)), this.intParse(sDate.Substring(4, 2)));
              number = this.intParse(name.Substring(ind3 + 1, 1));
            }
          }
        }
        this.lbl_CodeOrShiftIdValue.Text = shiftId;
        this.lbl_LevelOrDateValue.Text = date.ToLongTimeString();
        this.lbl_NumberValue.Text = number.ToString();
      }
    }
  }
}

