using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Forms.PopUp;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Production.Operator
{
  public partial class Frm_MessagePRC : Frm_OperatorSubGroup
  {
    private DataGridViewColumn clm_Message;
    private string filter = "";

    public Frm_MessagePRC()
    {
      InitializeComponent();

      this.clm_Message = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Message.Name = "data";
      this.clm_Message.HeaderText = "Time / Date";
      this.clm_Message.DataPropertyName = "data";
      this.clm_Message.Width = 250;

      this.gdv_Messages.Columns.Add(this.clm_Message);

      if (!SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
      }

      this.lbl_filter.Testo = (this.filter == "") ? "" : "!";
    }

    public override void refreshMySQLData()
    {
      try
      {
        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

        if (this.filter.Trim() != "")
          this.gdv_Messages.DataSource = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT data FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_example WHERE (" + filter + ") ORDER BY data DESC;");
        else
          this.gdv_Messages.DataSource = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT data FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_example ORDER BY data DESC;");

        if (this.gdv_Messages.Rows.Count > 0)
        {
          if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
            this.printGrid.Visible = true;
          else
            this.printGrid.Visible = false;

          if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
            this.btn_ShowGraph.Visible = true;
          else
            this.btn_ShowGraph.Visible = false;
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private string qualityIdFilter = "";
    private string fluteFilter = "";
    private void lbl_filter_Click(object sender, EventArgs e)
    {
      try
      {
        //Flute
        DataTable fluteTable = DBManager.getDBManager().flute.getTable();
        string[] fluteStrings = new string[fluteTable.Rows.Count];

        for (int i = 0; i < fluteTable.Rows.Count; i++)
          fluteStrings[i] = fluteTable.Rows[i][SyncroC.DataBase.Flute.FLUTE_ID].ToString();

        Frm_FilterPRCMessage askFilterQuality = new Frm_FilterPRCMessage();
        FosberDialogResult ret = askFilterQuality.ShowDialog(this, this.qualityIdFilter, this.fluteFilter, fluteStrings);
        if (ret.Dialog == DialogResult.OK)
        {
          this.filter = "";

          this.qualityIdFilter = ((string)ret.Data[0]).Trim();
          this.fluteFilter = ((string)ret.Data[1]).Trim();

          if (this.qualityIdFilter != "")
            this.filter = String.Format("(qualityid LIKE '{0}%')", this.qualityIdFilter);

          if (this.fluteFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (fluteid = '{0}')", this.fluteFilter);
            else
              this.filter = String.Format("(fluteid = '{0}')", this.fluteFilter);
          }
        }

        this.gdv_Messages.SuspendLayout();

        if(this.filter.Trim() != "")
          this.gdv_Messages.DataSource = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT data FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_example WHERE (" + filter + ") ORDER BY data DESC;");
        else
          this.gdv_Messages.DataSource = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT data FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_example ORDER BY data DESC;");

        if (this.gdv_Messages.Rows.Count > 0)
          this.gdv_Messages.Rows[0].Selected = true;
        this.gdv_Messages.ResumeLayout();

        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    /// <summary>
    /// CtrlF2 key behaviour: Delete an existing record
    /// </summary>
    protected override void CtrlF2()
    {
      if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        foreach (DataGridViewRow row in this.gdv_Messages.SelectedRows)
        {
          string delete = 
            String.Format
            (
            "DELETE FROM {0}.prc_example WHERE data = '{1}';", 
            SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE(),
            SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime((DateTime)((DataRowView)row.DataBoundItem).Row["data"])
            );

          SyncroC.DataBase.SqlManager.getSqlManager().doCommand(delete, true);
        }
        this.refreshMySQLData();
      }
    }

    /// <summary>
    /// CtrlF3 key behaviour: Delete all DB papers
    /// </summary>
    protected override void CtrlF3()
    {
      if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        SyncroC.Forms.PopUp.Frm_Confirm confirm = new SyncroC.Forms.PopUp.Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§PRC_19");
        if (confirm.ShowDialog(this, text).Equals(DialogResult.OK))
        {
          SyncroC.DataBase.SqlManager.getSqlManager().deleteAllRowInTable(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_example");
          this.refreshMySQLData();
        }
      }
    }

    private void gdv_Messages_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (this.gdv_Messages.Columns[e.ColumnIndex].Name.Equals("data")
            && e.Value != null)
      {
        try
        {
          e.Value = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(((DateTime)e.Value).ToLocalTime());
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Shifts_CellFormatting - ", ex);
        }
      }
    }

    private void gdv_Messages_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        if (gdv_Messages.Rows.Count > 0 && gdv_Messages.SelectedRows.Count == 1)
        {
          DataRow row = ((DataRowView)gdv_Messages.SelectedRows[0].DataBoundItem).Row;
          if (row != null && row.RowState != DataRowState.Detached)
          {
            string select = String.Format("SELECT * FROM {0}.prc_example WHERE data = '{1}';", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE(), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime((DateTime)row["data"]));
            DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect(select);
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                //bitmap
                this.pct_MessagePRC.Image = Image.FromStream(new System.IO.MemoryStream((Byte[])dt.Rows[0]["bitmap"]));

                //comment
                if (dt.Rows[0]["comment"] != System.DBNull.Value)
                  this.ttb_Comment.Value = (string)dt.Rows[0]["comment"];
                else
                  this.ttb_Comment.Value = "";

                //shiftid
                if (dt.Rows[0]["shiftid"] != System.DBNull.Value)
                  this.ttb_ShiftID.Value = (string)dt.Rows[0]["shiftid"];
                else
                  this.ttb_ShiftID.Value = "";

                //shiftmanager
                if (dt.Rows[0]["shiftmanager"] != System.DBNull.Value)
                  this.ttb_ShiftManager.Value = (string)dt.Rows[0]["shiftmanager"];
                else
                  this.ttb_ShiftManager.Value = "";

                //setupcode
                if (dt.Rows[0]["setupcode"] != System.DBNull.Value)
                  this.ttb_setupcode.Value = (string)dt.Rows[0]["setupcode"];
                else
                  this.ttb_setupcode.Value = "";

                //qualityid
                if (dt.Rows[0]["qualityid"] != System.DBNull.Value)
                  this.ttb_QualityID.Value = (string)dt.Rows[0]["qualityid"];
                else
                  this.ttb_QualityID.Value = "";

                //fluteid
                if (dt.Rows[0]["fluteid"] != System.DBNull.Value)
                  this.ttb_fluteid.Value = (string)dt.Rows[0]["fluteid"];
                else
                  this.ttb_fluteid.Value = "";
              }
            }
          }
        }
        else
        {
          this.pct_MessagePRC.Image = null;
          this.ttb_Comment.Value = "";
          this.ttb_ShiftID.Value = "";
          this.ttb_ShiftManager.Value = "";
          this.ttb_setupcode.Value = "";
          this.ttb_QualityID.Value = "";
          this.ttb_fluteid.Value = "";
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
        this.pct_MessagePRC.Image = null;
        this.ttb_Comment.Value = "";
        this.ttb_ShiftID.Value = "";
        this.ttb_ShiftManager.Value = "";
        this.ttb_setupcode.Value = "";
        this.ttb_QualityID.Value = "";
        this.ttb_fluteid.Value = "";
      }
    }

    private void printGrid_Click(object sender, EventArgs e)
    {
      if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
      {
        System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
        doc.PrintController = new System.Drawing.Printing.StandardPrintController();
        doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
        doc.DefaultPageSettings.Landscape = true;
        doc.Print();       
      }
    }

    void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      //Faccio la foto
      Bitmap res = SyncroC.Utility.SyncroImage.createSnapShot
        (
        this,
        this.leftHigh.X,
        this.leftHigh.Y - 1,
        this.rightHigh.X - this.leftHigh.X,
        this.leftLow.Y - this.leftHigh.Y + 1
        );
      e.Graphics.DrawImage(res, 0, 0, e.PageSettings.PrintableArea.Height, res.Height);
    }

    private void btn_ShowGraph_Click(object sender, EventArgs e)
    {
      DateTime dateTime = DateTime.MinValue;
      try
      {
        if (this.gdv_Messages.Rows.Count > 0 && this.gdv_Messages.SelectedRows.Count == 1)
        {
          DataRow row = ((DataRowView)this.gdv_Messages.SelectedRows[0].DataBoundItem).Row;
          if (row != null && row.RowState != DataRowState.Detached)
          {
            dateTime = ((DateTime)row["data"]).Subtract(TimeSpan.FromMinutes(15));
          }
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "btn_ShowGraph_Click - error converting value.", ex);
      }

      SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace.from_Universal = dateTime;

      askForPage(typeof(SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace));
    }
  }
}
