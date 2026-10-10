using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Statistics.Rolls
{
  public partial class Frm_StatisticsRollsTrace : SyncroC.Forms.Pages.Statistics.Rolls.Frm_StatisticsRollsSubgroup
  {
    int lastOffeset = 0;
    long resMax = 0;

    private const int MAX_RECORD_PER_CHANGE_PAGE = 3;

    public Frm_StatisticsRollsTrace()
    {
      InitializeComponent();
      
      if (!this.DesignMode)
      {
        this.ttb_RollId.Value = "";
        this.ttb_OrderNumber.Value = "";
        this.ttb_SetupCode.Value = "";
      }

      DateTime now = DateTime.Now.ToUniversalTime();
      this.mkt_From.ValueAsUNIVERSALDateTime = now.Subtract(TimeSpan.FromDays(7));
      this.mkt_To.ValueAsUNIVERSALDateTime = now;

      this.fillGrid(0);
    }

    protected override bool IncludeComponentsIntoLanguageDB()
    {
      return false;
    }

    protected override void CtrlF5()
    {
    }

    public override void refreshMySQLData()
    {
      //qui non fare nulla
    }

    private void btn_ApplyFilter_Click(object sender, EventArgs e)
    {
      this.fillGrid(0);
    }

    private void fillGrid(int offeset)
    {
      string whereTime = String.Format("((time>='{0}') AND (time<='{1}'))", 
        SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(this.mkt_From.ValueAsUNIVERSALDateTime),
        SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(this.mkt_To.ValueAsUNIVERSALDateTime));

      string whereProd = "";
      if (this.ttb_RollId.Value.Trim() != "")
      {
        whereProd = String.Format("(rollid1 LIKE '{0}%' OR rollid2 LIKE '{0}%' OR rollid3 LIKE '{0}%' OR rollid4 LIKE '{0}%' OR rollid5 LIKE '{0}%' OR rollid6 LIKE '{0}%' OR rollid7 LIKE '{0}%')", this.ttb_RollId.Value.Trim());
      }

      if (this.ttb_OrderNumber.Value.Trim() != "")
      {
        string whereOrder = String.Format("(ordernumber0 LIKE '{0}%' OR ordernumber1 LIKE '{0}%' OR ordernumber2 LIKE '{0}%')", this.ttb_OrderNumber.Value.Trim());

        if (whereProd != "")
          whereProd += " AND " + whereOrder;
        else
          whereProd = whereOrder;
      }

      if (this.ttb_SetupCode.Value.Trim() != "")
      {
        string whereSetupCode = String.Format("(setupcode LIKE '{0}%')", this.ttb_SetupCode.Value.Trim());

        if (whereProd != "")
          whereProd += " AND " + whereSetupCode;
        else
          whereProd = whereSetupCode;
      }

      string where = "";
      if (whereProd != "")
        where = whereTime + "AND" + whereProd;
      else
        where = whereTime;

      string select = "SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_syc_rolls WHERE (" + where + ")";
      this.resMax = SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect(select);

      if (this.resMax > 0)
      {
        if ((this.resMax / MAX_RECORD_PER_CHANGE_PAGE) == 0)
          this.incDecButton_right.LowLevelEnabled = false;
        else
          this.incDecButton_right.LowLevelEnabled = true;

        if (offeset >= (MAX_RECORD_PER_CHANGE_PAGE-1))
          this.incDecButton_left.LowLevelEnabled = true;
        else
          this.incDecButton_left.LowLevelEnabled = false;

        if (offeset >= (this.resMax - MAX_RECORD_PER_CHANGE_PAGE))
          this.incDecButton_right.LowLevelEnabled = false;
        else
          this.incDecButton_right.LowLevelEnabled = true;

        this.lbl_fromto.Text = ((offeset / MAX_RECORD_PER_CHANGE_PAGE) + 1).ToString() + " / " + ((this.resMax / MAX_RECORD_PER_CHANGE_PAGE) + 1);
        select += " ORDER BY time LIMIT 4 OFFSET " + offeset + ";";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect(select);

        if (dt.Rows.Count > 0)
        {
          //row 0
          this.lbl_pnl1_dateTime.Text = (offeset+1).ToString() + " - " + SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(dt.Rows[0]["time"]);
          this.lbl_pnl1_setupCode.Text = (string)dt.Rows[0]["setupcode"];
          this.lbl_pnl1_OrderNumber3.Text = (string)dt.Rows[0]["ordernumber0"];
          this.lbl_pnl1_OrderNumber2.Text = (string)dt.Rows[0]["ordernumber1"];
          this.lbl_pnl1_OrderNumber1.Text = (string)dt.Rows[0]["ordernumber2"];

          this.lbl_pnl1_type.Text = (string)dt.Rows[0]["type"];

          this.tnud_pnl1_metersproduceddb_udm.Value = ((int)dt.Rows[0]["metersproduceddb_udm"]).ToString();

          if ((int)dt.Rows[0]["weightfactor1_uwg_m"] > 0)
            this.lbl_pnl1_Roll1.Text = dt.Rows[0]["papertype1"] + " (" + dt.Rows[0]["weightfactor1_uwg_m"] + ") - " + dt.Rows[0]["rollid1"];
          else
            this.lbl_pnl1_Roll1.Text = "";

          if ((int)dt.Rows[0]["weightfactor2_uwg_m"] > 0)
            this.lbl_pnl1_Roll2.Text = dt.Rows[0]["papertype2"] + " (" + dt.Rows[0]["weightfactor2_uwg_m"] + ") - " + dt.Rows[0]["rollid2"];
          else
            this.lbl_pnl1_Roll2.Text = "";

          if ((int)dt.Rows[0]["weightfactor3_uwg_m"] > 0)
            this.lbl_pnl1_Roll3.Text = dt.Rows[0]["papertype3"] + " (" + dt.Rows[0]["weightfactor3_uwg_m"] + ") - " + dt.Rows[0]["rollid3"];
          else
            this.lbl_pnl1_Roll3.Text = "";

          if ((int)dt.Rows[0]["weightfactor4_uwg_m"] > 0)
            this.lbl_pnl1_Roll4.Text = dt.Rows[0]["papertype4"] + " (" + dt.Rows[0]["weightfactor4_uwg_m"] + ") - " + dt.Rows[0]["rollid4"];
          else
            this.lbl_pnl1_Roll4.Text = "";

          if ((int)dt.Rows[0]["weightfactor5_uwg_m"] > 0)
            this.lbl_pnl1_Roll5.Text = dt.Rows[0]["papertype5"] + " (" + dt.Rows[0]["weightfactor5_uwg_m"] + ") - " + dt.Rows[0]["rollid5"];
          else
            this.lbl_pnl1_Roll5.Text = "";

          if ((int)dt.Rows[0]["weightfactor6_uwg_m"] > 0)
            this.lbl_pnl1_Roll6.Text = dt.Rows[0]["papertype6"] + " (" + dt.Rows[0]["weightfactor6_uwg_m"] + ") - " + dt.Rows[0]["rollid6"];
          else
            this.lbl_pnl1_Roll6.Text = "";

          if ((int)dt.Rows[0]["weightfactor7_uwg_m"] > 0)
            this.lbl_pnl1_Roll7.Text = dt.Rows[0]["papertype7"] + " (" + dt.Rows[0]["weightfactor7_uwg_m"] + ") - " + dt.Rows[0]["rollid7"];
          else
            this.lbl_pnl1_Roll7.Text = "";
        }

        if (dt.Rows.Count > 1)
        {
          //row 1
          this.lbl_pnl2_dateTime.Text = (offeset + 2).ToString() + " - " + SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(dt.Rows[1]["time"]);
          this.lbl_pnl2_setupCode.Text = (string)dt.Rows[1]["setupcode"];
          this.lbl_pnl2_OrderNumber3.Text = (string)dt.Rows[1]["ordernumber0"];
          this.lbl_pnl2_OrderNumber2.Text = (string)dt.Rows[1]["ordernumber1"];
          this.lbl_pnl2_OrderNumber1.Text = (string)dt.Rows[1]["ordernumber2"];

          this.lbl_pnl2_type.Text = (string)dt.Rows[1]["type"];

          this.tnud_pnl2_metersproduceddb_udm.Value = ((int)dt.Rows[1]["metersproduceddb_udm"]).ToString();

          if ((int)dt.Rows[1]["weightfactor1_uwg_m"] > 0)
            this.lbl_pnl2_Roll1.Text = dt.Rows[1]["papertype1"] + " (" + dt.Rows[1]["weightfactor1_uwg_m"] + ") - " + dt.Rows[1]["rollid1"];
          else
            this.lbl_pnl2_Roll1.Text = "";

          if ((int)dt.Rows[1]["weightfactor2_uwg_m"] > 0)
            this.lbl_pnl2_Roll2.Text = dt.Rows[1]["papertype2"] + " (" + dt.Rows[1]["weightfactor2_uwg_m"] + ") - " + dt.Rows[1]["rollid2"];
          else
            this.lbl_pnl2_Roll2.Text = "";

          if ((int)dt.Rows[1]["weightfactor3_uwg_m"] > 0)
            this.lbl_pnl2_Roll3.Text = dt.Rows[1]["papertype3"] + " (" + dt.Rows[1]["weightfactor3_uwg_m"] + ") - " + dt.Rows[1]["rollid3"];
          else
            this.lbl_pnl2_Roll3.Text = "";

          if ((int)dt.Rows[1]["weightfactor4_uwg_m"] > 0)
            this.lbl_pnl2_Roll4.Text = dt.Rows[1]["papertype4"] + " (" + dt.Rows[1]["weightfactor4_uwg_m"] + ") - " + dt.Rows[1]["rollid4"];
          else
            this.lbl_pnl2_Roll4.Text = "";

          if ((int)dt.Rows[1]["weightfactor5_uwg_m"] > 0)
            this.lbl_pnl2_Roll5.Text = dt.Rows[1]["papertype5"] + " (" + dt.Rows[1]["weightfactor5_uwg_m"] + ") - " + dt.Rows[1]["rollid5"];
          else
            this.lbl_pnl2_Roll5.Text = "";

          if ((int)dt.Rows[1]["weightfactor6_uwg_m"] > 0)
            this.lbl_pnl2_Roll6.Text = dt.Rows[1]["papertype6"] + " (" + dt.Rows[1]["weightfactor6_uwg_m"] + ") - " + dt.Rows[1]["rollid6"];
          else
            this.lbl_pnl2_Roll6.Text = "";

          if ((int)dt.Rows[1]["weightfactor7_uwg_m"] > 0)
            this.lbl_pnl2_Roll7.Text = dt.Rows[1]["papertype7"] + " (" + dt.Rows[1]["weightfactor7_uwg_m"] + ") - " + dt.Rows[1]["rollid7"];
          else
            this.lbl_pnl2_Roll7.Text = "";
        }

        if (dt.Rows.Count > 2)
        {
          //row 2
          this.lbl_pnl3_dateTime.Text = (offeset + 3).ToString() + " - " + SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(dt.Rows[2]["time"]);
          this.lbl_pnl3_setupCode.Text = (string)dt.Rows[2]["setupcode"];
          this.lbl_pnl3_OrderNumber3.Text = (string)dt.Rows[2]["ordernumber0"];
          this.lbl_pnl3_OrderNumber2.Text = (string)dt.Rows[2]["ordernumber1"];
          this.lbl_pnl3_OrderNumber1.Text = (string)dt.Rows[2]["ordernumber2"];

          this.lbl_pnl3_type.Text = (string)dt.Rows[2]["type"];

          this.tnud_pnl3_metersproduceddb_udm.Value = ((int)dt.Rows[2]["metersproduceddb_udm"]).ToString();

          if ((int)dt.Rows[2]["weightfactor1_uwg_m"] > 0)
            this.lbl_pnl3_Roll1.Text = dt.Rows[2]["papertype1"] + " (" + dt.Rows[2]["weightfactor1_uwg_m"] + ") - " + dt.Rows[2]["rollid1"];
          else
            this.lbl_pnl3_Roll1.Text = "";

          if ((int)dt.Rows[2]["weightfactor2_uwg_m"] > 0)
            this.lbl_pnl3_Roll2.Text = dt.Rows[2]["papertype2"] + " (" + dt.Rows[2]["weightfactor2_uwg_m"] + ") - " + dt.Rows[2]["rollid2"];
          else
            this.lbl_pnl3_Roll2.Text = "";

          if ((int)dt.Rows[2]["weightfactor3_uwg_m"] > 0)
            this.lbl_pnl3_Roll3.Text = dt.Rows[2]["papertype3"] + " (" + dt.Rows[2]["weightfactor3_uwg_m"] + ") - " + dt.Rows[2]["rollid3"];
          else
            this.lbl_pnl3_Roll3.Text = "";

          if ((int)dt.Rows[2]["weightfactor4_uwg_m"] > 0)
            this.lbl_pnl3_Roll4.Text = dt.Rows[2]["papertype4"] + " (" + dt.Rows[2]["weightfactor4_uwg_m"] + ") - " + dt.Rows[2]["rollid4"];
          else
            this.lbl_pnl3_Roll4.Text = "";

          if ((int)dt.Rows[2]["weightfactor5_uwg_m"] > 0)
            this.lbl_pnl3_Roll5.Text = dt.Rows[2]["papertype5"] + " (" + dt.Rows[2]["weightfactor5_uwg_m"] + ") - " + dt.Rows[2]["rollid5"];
          else
            this.lbl_pnl3_Roll5.Text = "";

          if ((int)dt.Rows[2]["weightfactor6_uwg_m"] > 0)
            this.lbl_pnl3_Roll6.Text = dt.Rows[2]["papertype6"] + " (" + dt.Rows[2]["weightfactor6_uwg_m"] + ") - " + dt.Rows[2]["rollid6"];
          else
            this.lbl_pnl3_Roll6.Text = "";

          if ((int)dt.Rows[2]["weightfactor7_uwg_m"] > 0)
            this.lbl_pnl3_Roll7.Text = dt.Rows[2]["papertype7"] + " (" + dt.Rows[2]["weightfactor7_uwg_m"] + ") - " + dt.Rows[2]["rollid7"];
          else
            this.lbl_pnl3_Roll7.Text = "";
        }

        if (dt.Rows.Count > 3)
        {
          //row 3
          this.lbl_pnl4_dateTime.Text = (offeset + 4).ToString() + " - " + SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(dt.Rows[3]["time"]);
          this.lbl_pnl4_setupCode.Text = (string)dt.Rows[3]["setupcode"];
          this.lbl_pnl4_OrderNumber3.Text = (string)dt.Rows[3]["ordernumber0"];
          this.lbl_pnl4_OrderNumber2.Text = (string)dt.Rows[3]["ordernumber1"];
          this.lbl_pnl4_OrderNumber1.Text = (string)dt.Rows[3]["ordernumber2"];

          this.lbl_pnl4_type.Text = (string)dt.Rows[3]["type"];

          this.tnud_pnl4_metersproduceddb_udm.Value = ((int)dt.Rows[3]["metersproduceddb_udm"]).ToString();

          if ((int)dt.Rows[3]["weightfactor1_uwg_m"] > 0)
            this.lbl_pnl4_Roll1.Text = dt.Rows[3]["papertype1"] + " (" + dt.Rows[3]["weightfactor1_uwg_m"] + ") - " + dt.Rows[3]["rollid1"];
          else
            this.lbl_pnl4_Roll1.Text = "";

          if ((int)dt.Rows[3]["weightfactor2_uwg_m"] > 0)
            this.lbl_pnl4_Roll2.Text = dt.Rows[3]["papertype2"] + " (" + dt.Rows[3]["weightfactor2_uwg_m"] + ") - " + dt.Rows[3]["rollid2"];
          else
            this.lbl_pnl4_Roll2.Text = "";

          if ((int)dt.Rows[3]["weightfactor3_uwg_m"] > 0)
            this.lbl_pnl4_Roll3.Text = dt.Rows[3]["papertype3"] + " (" + dt.Rows[3]["weightfactor3_uwg_m"] + ") - " + dt.Rows[3]["rollid3"];
          else
            this.lbl_pnl4_Roll3.Text = "";

          if ((int)dt.Rows[3]["weightfactor4_uwg_m"] > 0)
            this.lbl_pnl4_Roll4.Text = dt.Rows[3]["papertype4"] + " (" + dt.Rows[3]["weightfactor4_uwg_m"] + ") - " + dt.Rows[3]["rollid4"];
          else
            this.lbl_pnl4_Roll4.Text = "";

          if ((int)dt.Rows[3]["weightfactor5_uwg_m"] > 0)
            this.lbl_pnl4_Roll5.Text = dt.Rows[3]["papertype5"] + " (" + dt.Rows[3]["weightfactor5_uwg_m"] + ") - " + dt.Rows[3]["rollid5"];
          else
            this.lbl_pnl4_Roll5.Text = "";

          if ((int)dt.Rows[3]["weightfactor6_uwg_m"] > 0)
            this.lbl_pnl4_Roll6.Text = dt.Rows[3]["papertype6"] + " (" + dt.Rows[3]["weightfactor6_uwg_m"] + ") - " + dt.Rows[3]["rollid6"];
          else
            this.lbl_pnl4_Roll6.Text = "";

          if ((int)dt.Rows[3]["weightfactor7_uwg_m"] > 0)
            this.lbl_pnl4_Roll7.Text = dt.Rows[3]["papertype7"] + " (" + dt.Rows[3]["weightfactor7_uwg_m"] + ") - " + dt.Rows[3]["rollid7"];
          else
            this.lbl_pnl4_Roll7.Text = "";
        }

        if (dt.Rows.Count == 0)
        {
          this.clearPanel(1);
          this.clearPanel(2);
          this.clearPanel(3);
          this.clearPanel(4);
        }

        if (dt.Rows.Count == 1)
        {
          this.clearPanel(2);
          this.clearPanel(3);
          this.clearPanel(4);
        }

        if (dt.Rows.Count == 2)
        {
          this.clearPanel(3);
          this.clearPanel(4);
        }

        if (dt.Rows.Count == 3)
        {
          this.clearPanel(4);
        }
      }
      else
      {
        this.incDecButton_left.LowLevelEnabled = false;
        this.incDecButton_right.LowLevelEnabled = false;
        this.lbl_fromto.Text = "0 / 0";
        this.clearPanel(1);
        this.clearPanel(2);
        this.clearPanel(3);
        this.clearPanel(4);
      }
    }

    private void clearPanel(int pnlNumber)
    {
      switch(pnlNumber)
      {
        case 1:
          foreach (Control c in this.pnl_1.Controls)
          {
            if (c is Label)
              c.Text = "";
            else if (c is SyncroC.Controls.TitledNumericUpDown)
              ((SyncroC.Controls.TitledNumericUpDown)c).Value = "0";
          }
          break;

        case 2:
          foreach (Control c in this.pnl_2.Controls)
          {
            if (c is Label)
              c.Text = "";
            else if (c is SyncroC.Controls.TitledNumericUpDown)
              ((SyncroC.Controls.TitledNumericUpDown)c).Value = "0";
          }
          break;

        case 3:
          foreach (Control c in this.pnl_3.Controls)
          {
            if (c is Label)
              c.Text = "";
            else if (c is SyncroC.Controls.TitledNumericUpDown)
              ((SyncroC.Controls.TitledNumericUpDown)c).Value = "0";
          }
          break;

        case 4:
          foreach (Control c in this.pnl_4.Controls)
          {
            if (c is Label)
              c.Text = "";
            else if (c is SyncroC.Controls.TitledNumericUpDown)
              ((SyncroC.Controls.TitledNumericUpDown)c).Value = "0";
          }
          break;

      }
    }

    private void btn_Last7gg_Click(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now.ToUniversalTime();
      this.mkt_From.ValueAsUNIVERSALDateTime = now.Subtract(TimeSpan.FromDays(7));
      this.mkt_To.ValueAsUNIVERSALDateTime = now;
    }

    private void btn_Last15gg_Click(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now.ToUniversalTime();
      this.mkt_From.ValueAsUNIVERSALDateTime = now.Subtract(TimeSpan.FromDays(15));
      this.mkt_To.ValueAsUNIVERSALDateTime = now;
    }

    private void btn_Last30gg_Click(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now.ToUniversalTime();
      this.mkt_From.ValueAsUNIVERSALDateTime = now.Subtract(TimeSpan.FromDays(30));
      this.mkt_To.ValueAsUNIVERSALDateTime = now;
    }

    private void btn_LastYear_Click(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now.ToUniversalTime();
      this.mkt_From.ValueAsUNIVERSALDateTime = new DateTime(now.Year - 1, now.Month, now.Day, now.Hour, now.Minute, now.Second);
      this.mkt_To.ValueAsUNIVERSALDateTime = now;
    }

    private void incDecButton_right_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (!this.incDecButton_right.LowLevelEnabled)
      {
        this.incDecButton_right._signal(false);
        return;
      }

      if (args.Frequency != -2)
      {
        this.incDecButton_right._signal(true);

        this.lastOffeset = this.lastOffeset + MAX_RECORD_PER_CHANGE_PAGE;

        this.fillGrid(this.lastOffeset);
      }
      else
        this.incDecButton_right._signal(false);
    }

    private void incDecButton_left_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (!this.incDecButton_left.LowLevelEnabled)
      {
        this.incDecButton_left._signal(false);
        return;
      }

      if (args.Frequency != -2)
      {
        this.incDecButton_left._signal(true);

        if (this.lastOffeset > 0)
          this.lastOffeset = this.lastOffeset - MAX_RECORD_PER_CHANGE_PAGE;

        this.fillGrid(this.lastOffeset);
      }
      else
        this.incDecButton_left._signal(false);

    }

  }
}
