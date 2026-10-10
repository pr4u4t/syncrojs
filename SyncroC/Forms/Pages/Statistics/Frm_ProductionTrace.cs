using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Tools.Graphs.Lines;
using Microsoft.Tools.Graphs.Legends;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Statistics
{
  public partial class Frm_ProductionTrace : SyncroC.Forms.Pages.Statistics.Frm_StatisticsGroup
  {
    public static DateTime from_Universal = DateTime.MinValue;

    private DateTime currentShift_startTime = DateTime.MinValue;
    private string currentShift_shiftID = "";
    private string currentShift_shiftManager = "";

    public Frm_ProductionTrace()
    {
      InitializeComponent();
      this.zoomOutSize = this.graphLines_GlueGap.Size;

    }

    public void _Show(bool isIDLEShift, string shiftID, string starttime, string shiftManager)
    {
      base._Show();

      if (from_Universal == DateTime.MinValue)
      {
        from_Universal = DateTime.Now.Subtract(TimeSpan.FromMinutes(30)).ToUniversalTime();
        if (from_Universal < this.currentShift_startTime)
          from_Universal = this.currentShift_startTime;
      }

      if (!isIDLEShift)
      {
        this.currentShift_startTime = SyncroC.Utility.SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(starttime);
        this.currentShift_shiftID = shiftID;
        this.currentShift_shiftManager = shiftManager;
      }
      else
      {
        this.currentShift_startTime = DateTime.MinValue;
        this.currentShift_shiftID = "";
        this.currentShift_shiftManager = "";
      }

      //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
      if (DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        this.tab_TabD.Visible = true;
      }
      else
      {
        this.lbl_Initial_Quality.Location = new Point(this.lbl_Initial_Quality.Location.X, this.lbl_Initial_Quality.Location.Y + 20);
        this.lbl_Initial_Roll6.Visible = false;
        this.lbl_Initial_Roll7.Visible = false;

      }

      this.refresh(false);
    }

    //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
    protected override void F10()
    {
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Arrow;
      this.tab_TabB.Cursor = Cursors.Hand;
      this.tab_TabC.Cursor = Cursors.Hand;
      this.tab_TabD.Cursor = Cursors.Hand;

      this.refresh(false);
    }

    //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
    protected override void F11()
    {
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Hand;
      this.tab_TabB.Cursor = Cursors.Arrow;
      this.tab_TabC.Cursor = Cursors.Hand;
      this.tab_TabD.Cursor = Cursors.Hand;

      this.refresh(false);
    }

    //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
    protected override void F12()
    {
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Hand;
      this.tab_TabB.Cursor = Cursors.Hand;
      this.tab_TabC.Cursor = Cursors.Arrow;
      this.tab_TabD.Cursor = Cursors.Hand;

      this.refresh(false);
    }

    //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
    protected override void F13()
    {
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabA.Cursor = Cursors.Hand;
      this.tab_TabB.Cursor = Cursors.Hand;
      this.tab_TabC.Cursor = Cursors.Hand;
      this.tab_TabD.Cursor = Cursors.Arrow;

      this.refresh(false);
    }

    // choose Past Orders
    protected override void CtrlF1()
    {
      askForPage(typeof(SyncroC.Forms.Pages.Statistics.Orders.Frm_StatisticsOrdersList));
    }

    //choose Past rolls
    protected override void CtrlF2()
    {
      askForPage(typeof(SyncroC.Forms.Pages.Statistics.Rolls.Frm_StatisticsRollsList));
    }

    //choose Date Time
    protected override void CtrlF3()
    {
      this.lbl_CtrlF3.BackColor = this.lbl_CtrlF3_YY.BackColor = this.lbl_Status.BackColor;
      Application.DoEvents();

      SyncroC.Forms.PopUp.Frm_AskDateTime askDateTime = new SyncroC.Forms.PopUp.Frm_AskDateTime();
      SyncroC.Utility.FosberDialogResult ret = askDateTime.ShowDialog(this, "", from_Universal);

      if (ret.Dialog == DialogResult.OK)
      {
        from_Universal = ((DateTime)ret.Data[0]);
        this.refresh(false);
      }

      this.lbl_CtrlF3.BackColor = this.lbl_CtrlF3_YY.BackColor = this.lbl_Title_YN.BackColor;
    }

    // <<
    protected override void CtrlF4()
    {
      this.lbl_CtrlF4.BackColor = this.lbl_CtrlF4_YY.BackColor = this.lbl_Status.BackColor;
      Application.DoEvents();

      from_Universal = from_Universal.Subtract(TimeSpan.FromMinutes(30));
      this.refresh(false);

      this.lbl_CtrlF4.BackColor = this.lbl_CtrlF4_YY.BackColor = this.lbl_Title_YN.BackColor;
    }

    // >>
    protected override void CtrlF5()
    {
      this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = this.lbl_Status.BackColor;
      Application.DoEvents();

      from_Universal = from_Universal.AddMinutes(30);
      this.refresh(true);

      this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = this.lbl_Title_YN.BackColor;
    }

    private bool zoomIn = false;
    private Size zoomOutSize = new Size();
    private void Zoom_Trace_Click(object sender, EventArgs e)
    {
      if (!this.zoomIn)
        this.zoomOutSize = this.graphLines_GlueGap.Size;

      this.zoomIn = !this.zoomIn;

      this.flowLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel1.Enabled = false;

      if (this.zoomIn)
      {
        this.lbl_leftMargin.Visible = true;
        this.lbl_rightMargin.Visible = true;

        Size zoomInSize = new Size(this.graphLines_Speed.Size.Width, this.graphLines_GlueGap.Size.Height);
        this.graphLines_GlueGap.Size = zoomInSize;
        this.graphLines_DBSection.Size = zoomInSize;
        this.graphLines_MainSteam.Size = zoomInSize;
        this.graphLines_Temp.Size = zoomInSize;
        this.graphLines_Tension.Size = zoomInSize;
        this.graphLines_WrapPosition.Size = zoomInSize;
        this.graphLines_SteamShower.Size = zoomInSize;
        this.graphLines_Moisture.Size = zoomInSize;
        this.refresh(false);
      }
      else
      {
        this.lbl_leftMargin.Visible = false;
        this.lbl_rightMargin.Visible = false;

        this.graphLines_GlueGap.Size = zoomOutSize;
        this.graphLines_DBSection.Size = zoomOutSize;
        this.graphLines_MainSteam.Size = zoomOutSize;
        this.graphLines_Temp.Size = zoomOutSize;
        this.graphLines_Tension.Size = zoomOutSize;
        this.graphLines_WrapPosition.Size = zoomOutSize;
        this.graphLines_SteamShower.Size = zoomOutSize;
        this.graphLines_Moisture.Size = zoomOutSize;
        this.refresh(false);
      }

      this.flowLayoutPanel1.Enabled = true;
      this.flowLayoutPanel1.ResumeLayout();

      this.Zoom_Trace.Active = this.zoomIn;
    }

    private int getZoneShown()
    {
      if (this.tab_TabA.TabSelection == SyncroC.Controls.TabSelectionType.Selected)
        return 0;
      if (this.tab_TabB.TabSelection == SyncroC.Controls.TabSelectionType.Selected)
        return 1;
      if (this.tab_TabC.TabSelection == SyncroC.Controls.TabSelectionType.Selected)
        return 2;
      //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
      if (this.tab_TabD.TabSelection == SyncroC.Controls.TabSelectionType.Selected)
        return 3;
      return 0;
    }

    private void refresh(bool goForward)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        this.checkFromDateTime(goForward);
        this.drawInitialValue(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawSpeed(true, this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawGlue(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawTemperature(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawWrapArmPosition(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawTension(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawMainSteam(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawSteamShower(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawDBSection(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
        this.drawMoisture(this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "refresh", ex);
      }

      this.Cursor = Cursors.Arrow;
    }

    public void _UpdateCurrentShift(bool isIDLEShift, string starttime, string shiftID, string shiftManager)
    {
      if (!isIDLEShift)
      {
        this.currentShift_startTime = SyncroC.Utility.SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(starttime);
        this.currentShift_shiftID = shiftID;
        this.currentShift_shiftManager = shiftManager;
      }
      else
      {
        this.currentShift_startTime = DateTime.MinValue;
        this.currentShift_shiftID = "";
        this.currentShift_shiftManager = "";
      }
    }

    private void checkFromDateTime(bool goForward)
    {
      DateTime from = from_Universal;
      DateTime to = from_Universal.AddMinutes(30);

      if ((from >= this.currentShift_startTime) && (to >= this.currentShift_startTime) && (this.currentShift_startTime != DateTime.MinValue))
      {
        //CURRENT SHIFT
        if (to > DateTime.Now.ToUniversalTime())
        {
          //from_Universal = DateTime.Compare(from, this.currentShift_startTime) >= 0 ? this.currentShift_startTime : from;
          from_Universal = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(30));
        }
      }
      else if ((from < this.currentShift_startTime) && (to > this.currentShift_startTime))
      {
        from_Universal = this.currentShift_startTime;
      }
      else
      {
        //PAST SHIFT
        string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".shiftlist";
        string whereFrom = String.Format("('{0}' > starttime AND finishtime > '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
        string whereTo = String.Format("('{0}' > starttime AND finishtime > '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));

        bool isEmptyFrom = SyncroC.DataBase.SqlManager.getSqlManager().isEmptySelectForCommand("SELECT * FROM " + myTableName + " WHERE " + whereFrom + ";");
        bool isEmptyTo = SyncroC.DataBase.SqlManager.getSqlManager().isEmptySelectForCommand("SELECT * FROM " + myTableName + " WHERE " + whereTo + ";");

        if (isEmptyFrom && isEmptyTo)
        {
          if (goForward)
          {
            string where = String.Format("(starttime > '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
            DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT starttime FROM " + myTableName + " WHERE " + where + " ORDER BY starttime ASC LIMIT 1;");
            if (dt.Rows.Count != 0)
              from_Universal = (DateTime)dt.Rows[0]["starttime"];
            else
            {
              if (this.currentShift_startTime != DateTime.MinValue)
                from_Universal = this.currentShift_startTime;
              else
                from_Universal = from_Universal.Subtract(TimeSpan.FromMinutes(30));
            }
          }
          else
          {
            string where = String.Format("(finishtime < '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
            DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT finishtime FROM " + myTableName + " WHERE " + where + " ORDER BY finishtime DESC LIMIT 1;");
            if (dt.Rows.Count != 0)
              from_Universal = ((DateTime)dt.Rows[0]["finishtime"]).Subtract(TimeSpan.FromMinutes(30));
            else
              from_Universal = from_Universal.AddMinutes(30);
          }
        }

        if (!isEmptyFrom && isEmptyTo)
        {
          string where = String.Format("(finishtime > '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
          DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT finishtime FROM " + myTableName + " WHERE " + where + " ORDER BY finishtime DESC LIMIT 1;");
          if (dt.Rows.Count != 0)
            from_Universal = ((DateTime)dt.Rows[0]["finishtime"]).Subtract(TimeSpan.FromMinutes(30));
        }

        if (isEmptyFrom && !isEmptyTo)
        {
          string where = String.Format("(starttime < '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
          DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT starttime FROM " + myTableName + " WHERE " + where + " ORDER BY starttime DESC LIMIT 1;");
          if (dt.Rows.Count != 0)
            from_Universal = ((DateTime)dt.Rows[0]["starttime"]);
        }
      }

      this.lbl_Title_YN.Text = from_Universal.ToLocalTime().ToString("HH:mm:ss dd/MM/yyyy");
    }

    private void drawInitialValue(int zoneNumber, DateTime from, DateTime to)
    {
      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders";
      string where = String.Format("('{0}' = begintime)", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
      DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT begintime,setupcode,ordernumber0,ordernumber1,ordernumber2,qualityid FROM " + myTableName + " WHERE " + where + " LIMIT 1;");
      if (dt.Rows.Count != 0)
      {
        this.lbl_Initial_setupCode.Text = (string)dt.Rows[0]["setupcode"];
        this.lbl_Initial_OrderNumber.Text = String.Format("[{0},{1},{2}]", ((string)dt.Rows[0]["ordernumber0"]), ((string)dt.Rows[0]["ordernumber1"]), ((string)dt.Rows[0]["ordernumber2"]));
        this.lbl_Initial_Quality.Text = (string)dt.Rows[0]["qualityid"];
      }
      else
      {
        this.lbl_Initial_setupCode.Text = "";
        this.lbl_Initial_OrderNumber.Text = "";
        this.lbl_Initial_Quality.Text = "";
      }

      myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_syc_rolls";
      where = where = String.Format("(time <= '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
      dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY time DESC LIMIT 1;");

      //ROLLS
      if (dt.Rows.Count != 0)
      {
        //row 0
        if (this.lbl_Initial_setupCode.Text == "")
        {
          this.lbl_Initial_setupCode.Text = (string)dt.Rows[0]["setupcode"];

          this.lbl_Initial_OrderNumber.Text = String.Format("[{0},{1},{2}]", ((string)dt.Rows[0]["ordernumber0"]), ((string)dt.Rows[0]["ordernumber1"]), ((string)dt.Rows[0]["ordernumber2"]));

          string myTableName_quality = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders";
          string where_quality = String.Format("({0}=setupid)", (int)dt.Rows[0]["setupid"]);
          DataTable dt_quality = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT setupid,qualityid FROM " + myTableName_quality + " WHERE " + where_quality + " LIMIT 1;");
          if (dt_quality.Rows.Count != 0)
          {
            this.lbl_Initial_Quality.Text = (string)dt_quality.Rows[0]["qualityid"];
          }

        }

        if ((int)dt.Rows[0]["weightfactor1_uwg_m"] > 0)
        {
          this.pct0.Visible = true;
          this.lbl_Initial_Roll1.Text = dt.Rows[0]["papertype1"] + " (" + dt.Rows[0]["weightfactor1_uwg_m"] + ") - " + dt.Rows[0]["rollid1"];
        }
        else
        {
          this.pct0.Visible = false;
          this.lbl_Initial_Roll1.Text = "";
        }

        if ((int)dt.Rows[0]["weightfactor2_uwg_m"] > 0)
        {
          this.pct1.Visible = true;
          this.lbl_Initial_Roll2.Text = dt.Rows[0]["papertype2"] + " (" + dt.Rows[0]["weightfactor2_uwg_m"] + ") - " + dt.Rows[0]["rollid2"];
        }
        else
        {
          this.pct1.Visible = false;
          this.lbl_Initial_Roll2.Text = "";
        }

        if ((int)dt.Rows[0]["weightfactor3_uwg_m"] > 0)
        {
          this.pct2.Visible = true;
          this.lbl_Initial_Roll3.Text = dt.Rows[0]["papertype3"] + " (" + dt.Rows[0]["weightfactor3_uwg_m"] + ") - " + dt.Rows[0]["rollid3"];
        }
        else
        {
          this.pct2.Visible = false;
          this.lbl_Initial_Roll3.Text = "";
        }

        if ((int)dt.Rows[0]["weightfactor4_uwg_m"] > 0)
        {
          this.pct3.Visible = true;
          this.lbl_Initial_Roll4.Text = dt.Rows[0]["papertype4"] + " (" + dt.Rows[0]["weightfactor4_uwg_m"] + ") - " + dt.Rows[0]["rollid4"];
        }
        else
        {
          this.pct3.Visible = false;
          this.lbl_Initial_Roll4.Text = "";
        }

        if ((int)dt.Rows[0]["weightfactor5_uwg_m"] > 0)
        {
          this.pct4.Visible = true;
          this.lbl_Initial_Roll5.Text = dt.Rows[0]["papertype5"] + " (" + dt.Rows[0]["weightfactor5_uwg_m"] + ") - " + dt.Rows[0]["rollid5"];
        }
        else
        {
          this.pct4.Visible = false;
          this.lbl_Initial_Roll5.Text = "";
        }

        //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
        if (DBManager.getDBManager().lineConfig.SingleFacer3 && ((int)dt.Rows[0]["weightfactor6_uwg_m"] > 0))
        {
          this.pct5.Visible = true;
          this.lbl_Initial_Roll6.Text = dt.Rows[0]["papertype6"] + " (" + dt.Rows[0]["weightfactor6_uwg_m"] + ") - " + dt.Rows[0]["rollid6"];
          this.lbl_Initial_Roll6.Visible = true;
        }
        else
        {
          this.pct5.Visible = false;
          this.lbl_Initial_Roll6.Text = "";
          this.lbl_Initial_Roll6.Visible = false;
        }

        if (DBManager.getDBManager().lineConfig.SingleFacer3 && ((int)dt.Rows[0]["weightfactor7_uwg_m"] > 0) )
        {
          this.pct6.Visible = true;
          this.lbl_Initial_Roll7.Text = dt.Rows[0]["papertype7"] + " (" + dt.Rows[0]["weightfactor7_uwg_m"] + ") - " + dt.Rows[0]["rollid7"];
          this.lbl_Initial_Roll7.Visible = true;
        }
        else
        {
          this.pct6.Visible = false;
          this.lbl_Initial_Roll7.Text = "";
          this.lbl_Initial_Roll7.Visible = false;
        }
      }

      //SHIFT
      if ((this.currentShift_startTime != DateTime.MinValue) && (from_Universal >= this.currentShift_startTime))
      {
        //CURRENT SHIFT
        this.lbl_Initial_Shift.Text = this.currentShift_shiftID + " - " + this.currentShift_shiftManager;
      }
      else
      {
        myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".shiftlist";
        where = String.Format("(starttime <= '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
        dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT starttime,id,shiftmanager FROM " + myTableName + " WHERE " + where + " ORDER BY starttime DESC LIMIT 1;");
        if (dt.Rows.Count != 0)
        {
          this.lbl_Initial_Shift.Text = (string)dt.Rows[0]["id"] + " - " + (string)dt.Rows[0]["shiftmanager"];
        }
      }
    }

    private void drawSpeed(bool creaLegendaEventi, int zoneNumber, DateTime from, DateTime to)
    {
      //SPEED
      int maxSpeed = 0;
      Line lineZone = SyncroC.DataBase.TRACEManager.getTRACEManager().drawLineSpeed(zoneNumber, from, to, 30, out maxSpeed);

      //PUNTO SELEZIONATO
      string selectedItem = "";
      if (this.myListBox_Speed.getSelectedItem() != "")
        selectedItem = this.myListBox_Speed.getSelectedItem();

      List<SyncroC.Controls.GroupPanel.Triple<string, string>> myLista = new List<SyncroC.Controls.GroupPanel.Triple<string, string>>();

      Point pointToHighLight = new Point(-1, -1);

      string myTableName = "";
      string where = "";
      DataTable dt = null;

      if (zoneNumber == 0)
      {
        //AOC e SPLICE
        myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_syc_rolls";
        where = String.Format("('{0}' <= time AND time <= '{1}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
        dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY time ASC;");

        int eventoDiAOC = 0;
        int eventoDiGiunta = 0;
        int eventoDiStop = 0;
        if (dt.Rows.Count != 0)
        {
          foreach (DataRow a in dt.Rows)
          {
            DateTime datetime = ((DateTime)a["time"]).ToLocalTime();
            string typeOfRecord = (string)a["type"];

            int index = (datetime - from.ToLocalTime()).Minutes;

            //AOC
            if (typeOfRecord.Contains("AOC"))
            {
              eventoDiAOC = eventoDiAOC + 1;

              string str1 = String.Format("A{0}) {1} {2}", eventoDiAOC, datetime.ToString("HH:mm:ss"), typeOfRecord.Replace("and", "-"));

              if (creaLegendaEventi)
              {
                string str2 = String.Format("<- {0} [{1},{2},{3}] {4}", ((string)a["setupcode"]), ((string)a["ordernumber0"]), ((string)a["ordernumber1"]), ((string)a["ordernumber2"]), ((int)a["metersproduceddb_udm"]));

                string EntramyTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders";
                string Entrawhere = String.Format("('{0}' >= begintime)", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(datetime.ToUniversalTime()));
                DataTable Entradt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + EntramyTableName + " WHERE " + Entrawhere + " ORDER BY begintime DESC LIMIT 1;");
                if (Entradt.Rows.Count != 0)
                {
                  str2 += String.Format("\n-> {0} [{1},{2},{3}]", ((string)Entradt.Rows[0]["setupcode"]), ((string)Entradt.Rows[0]["ordernumber0"]), ((string)Entradt.Rows[0]["ordernumber1"]), ((string)Entradt.Rows[0]["ordernumber2"]));
                }

                myLista.Add(new SyncroC.Controls.GroupPanel.Triple<string, string>(datetime, str1, str2));
              }

              if ((lineZone.Points[index].Text == "") || (lineZone.Points[index].Text == null))
                lineZone.Points[index].Text = "A" + eventoDiAOC;
              else
                lineZone.Points[index].Text += Environment.NewLine + "A" + eventoDiAOC;

              if (str1 == selectedItem)
                pointToHighLight = new Point((int)lineZone.Points[index].XValue, (int)lineZone.Points[index].YValue);
            }

            //SPLICE
            if (typeOfRecord.Contains("Splice"))
            {
              eventoDiGiunta = eventoDiGiunta + 1;

              string splNumberStr = typeOfRecord.Replace("Splice", "");
              splNumberStr = splNumberStr.Replace("at DB", "");
              int splNumber = Convert.ToInt32(splNumberStr.Trim());

              string str1 = String.Format("S{0}) {1} SPL {2} || DB", eventoDiGiunta, datetime.ToString("HH:mm:ss"), splNumber);

              if (creaLegendaEventi)
              {
                string str2 = String.Format("<- {0} ({1}) - {2}", ((string)a["papertype" + splNumber]), ((int)a["weightfactor" + splNumber + "_uwg_m"]), ((string)a["rollid" + splNumber]));

                string EntramyTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastrolls";
                string Entrawhere = String.Format("('{0}' <= timeremoved AND splicernumber={1})", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(datetime.ToUniversalTime()), splNumber);
                DataTable Entradt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + EntramyTableName + " WHERE " + Entrawhere + " ORDER BY timeremoved ASC LIMIT 1;");
                if (Entradt.Rows.Count != 0)
                {
                  str2 += String.Format("\n-> {0} ({1}) - {2}", ((string)Entradt.Rows[0]["papertype"]), ((int)Entradt.Rows[0]["weightfactor_uwg_m"]), ((string)Entradt.Rows[0]["rollid"]));
                }                
                
                str2 += String.Format("\n{0} [{1},{2},{3}] {4}", ((string)a["setupcode"]), ((string)a["ordernumber0"]), ((string)a["ordernumber1"]), ((string)a["ordernumber2"]), ((int)a["metersproduceddb_udm"]));

                myLista.Add(new SyncroC.Controls.GroupPanel.Triple<string, string>(datetime, str1, str2));
              }

              if ((lineZone.Points[index].Text == "") || (lineZone.Points[index].Text == null))
                lineZone.Points[index].Text = "S" + eventoDiGiunta;
              else
                lineZone.Points[index].Text += Environment.NewLine + "S" + eventoDiGiunta;

              if (str1 == selectedItem)
                pointToHighLight = new Point((int)lineZone.Points[index].XValue, (int)lineZone.Points[index].YValue);
            }

            //ABELLO_FI22/12/2010_1 Aggiunto il trace degli Stop su Postgres.
            //Stop
            if (typeOfRecord.Contains("Stop"))
            {
              eventoDiStop = eventoDiStop + 1;

              string stopStr = typeOfRecord;
              string str1 = String.Format("Z{0}) {1} {2}", eventoDiStop, datetime.ToString("HH:mm:ss"), stopStr);

              if (creaLegendaEventi)
              {
                myLista.Add(new SyncroC.Controls.GroupPanel.Triple<string, string>(datetime, str1, ""));
              }

              if ((lineZone.Points[index].Text == "") || (lineZone.Points[index].Text == null))
                lineZone.Points[index].Text = "Z" + eventoDiStop;
              else
                lineZone.Points[index].Text += Environment.NewLine + "Z" + eventoDiStop;

              if (str1 == selectedItem)
                pointToHighLight = new Point((int)lineZone.Points[index].XValue, (int)lineZone.Points[index].YValue);
            }
          }
        }
      }

      //REMOVE ROLL
      myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastrolls";
      string whereTime = String.Format("'{0}' <= timeremoved AND timeremoved <= '{1}'", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
      where = "";

      switch (zoneNumber)
      {
        case 0:
          where = "(" + whereTime + " AND splicernumber IN (1))";
          break;
        case 1:
          where = "(" + whereTime + " AND splicernumber IN (2,3))";
          break;
        case 2:
          where = "(" + whereTime + " AND splicernumber IN (4,5))";
          break;
        case 3:
          where = "(" + whereTime + " AND splicernumber IN (6,7))";
          break;
      }

      dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY timeremoved ASC;");

      int eventoDiRimozioneBobina = 0;
      if (dt.Rows.Count != 0)
      {
        foreach (DataRow a in dt.Rows)
        {
          DateTime datetime = ((DateTime)a["timeremoved"]).ToLocalTime();

          int index = (datetime - from.ToLocalTime()).Minutes;

          eventoDiRimozioneBobina = eventoDiRimozioneBobina + 1;

          int splNumber = (int)a["splicernumber"];

          string str1 = String.Format("R{0}) {1} SPL {2}", eventoDiRimozioneBobina, datetime.ToString("HH:mm:ss"), splNumber);

          if (creaLegendaEventi)
          {
            string str2 = String.Format("<- {0} ({1}) - {2} mt. {3}", ((string)a["papertype"]), ((int)a["weightfactor_uwg_m"]), ((string)a["rollid"]), ((int)a["remaining_udmm"]) / 1000);

            string EntramyTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastrolls";
            string Entrawhere = String.Format("('{0}' <= timeentered)", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(datetime.ToUniversalTime()));
            DataTable Entradt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + EntramyTableName + " WHERE " + Entrawhere + " ORDER BY timeentered ASC LIMIT 1;");
            if (Entradt.Rows.Count != 0)
            {
              str2 += String.Format("\n-> {0} ({1}) - {2}", ((string)Entradt.Rows[0]["papertype"]), ((int)Entradt.Rows[0]["weightfactor_uwg_m"]), ((string)Entradt.Rows[0]["rollid"]));
            }

            str2 += String.Format("\nSyncro {0}", ((bool)a["syncrosplice"]));

            myLista.Add(new SyncroC.Controls.GroupPanel.Triple<string, string>(datetime, str1, str2));
          }

          if ((lineZone.Points[index].Text == "") || (lineZone.Points[index].Text == null))
            lineZone.Points[index].Text = "R" + eventoDiRimozioneBobina;
          else
            lineZone.Points[index].Text += Environment.NewLine + "R" + eventoDiRimozioneBobina;

          if (str1 == selectedItem)
            pointToHighLight = new Point((int)lineZone.Points[index].XValue, (int)lineZone.Points[index].YValue);
        }
      }

      if (creaLegendaEventi)
        this.myListBox_Speed.fillData(myLista);

      this.graphLines_Speed.drawLine(lineZone, 0, maxSpeed + 10, pointToHighLight, this.graphLines_Speed.getMyUnitMeasure(), from, to, true);
    }

    //ABELLO_FI24/11/2010_2 [PRC] Aggiunta storicizzazione e visualizzazione dell'umidità sopra/sotto
    private void drawMoisture(int zoneNumber, DateTime from, DateTime to)
    {
      if (zoneNumber != 0)
      {
        this.graphLines_Moisture.Visible = false;
        return;
      }

      SyncroC.DataBase.TRACEManager.getTRACEManager().drawMoisture(this.graphLines_Moisture, from, to, 30, true);
    }

    private void drawDBSection(int zoneNumber, DateTime from, DateTime to)
    {
      if (zoneNumber != 0)
      {
        this.graphLines_DBSection.Visible = false;
        return;
      }

      SyncroC.DataBase.TRACEManager.getTRACEManager().drawDBSection(this.graphLines_DBSection, from, to, 30, true);
    }

    private void drawGlue(int zoneNumber, DateTime from, DateTime to)
    {
      SyncroC.DataBase.TRACEManager.getTRACEManager().drawGlue(this.graphLines_GlueGap, zoneNumber, from, to, 30, true);
    }

    private void drawSteamShower(int zoneNumber, DateTime from, DateTime to)
    {
      SyncroC.DataBase.TRACEManager.getTRACEManager().drawSteamShower(this.graphLines_SteamShower, zoneNumber, from, to, 30, true);
    }

    private void drawMainSteam(int zoneNumber, DateTime from, DateTime to)
    {
      SyncroC.DataBase.TRACEManager.getTRACEManager().drawMainSteam(this.graphLines_MainSteam, zoneNumber, from, to, 30, true);
    }

    private void drawTension(int zoneNumber, DateTime from, DateTime to)
    {
      SyncroC.DataBase.TRACEManager.getTRACEManager().drawTension(this.graphLines_Tension, zoneNumber, from, to, 30, true);
    }

    private void drawWrapArmPosition(int zoneNumber, DateTime from, DateTime to)
    {
      SyncroC.DataBase.TRACEManager.getTRACEManager().drawWrapArmPosition(this.graphLines_WrapPosition, zoneNumber, from, to, 30, true);
    }

    private void drawTemperature(int zoneNumber, DateTime from, DateTime to)
    {
      SyncroC.DataBase.TRACEManager.getTRACEManager().drawTemperature(this.graphLines_Temp, zoneNumber, from, to, 30, true);
    }

    private void myListBox_OnMyListBoxSelectedIndexChangedHandler(object sender, string args)
    {
      try
      {
        this.drawSpeed(false, this.getZoneShown(), from_Universal, from_Universal.AddMinutes(30));
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "myListBox_OnMyListBoxSelectedIndexChangedHandler ", ex);
      }
    }
  }
}
