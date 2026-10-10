using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Tools.Graphs.Lines;
using Microsoft.Tools.Graphs.Legends;
using System.Drawing;

namespace SyncroC.DataBase
{
  class TRACEManager
  {
    private static TRACEManager traceManager;

    /// <summary>
    /// Factory.
    /// </summary>
    /// <returns>The one and only TRACEManager object of the application.</returns>
    public static TRACEManager getTRACEManager()
    {
      if (traceManager == null)
        traceManager = new TRACEManager();
      return traceManager;
    }

    public Dictionary<string, int> getDowntime(DateTime from, DateTime to)
    {
      Dictionary<string, int> res = new Dictionary<string, int>();

      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".downtimes";
      string where = String.Format("('{0}' <= starttime AND starttime < '{1}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
      DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY starttime ASC;");

      foreach (DataRow a in dt.Rows)
      {
        string zoneKey = (string)a["zonedescription"];
        if (zoneKey == "")
          zoneKey = "???";
        DateTime datetimeStart = ((DateTime)a["starttime"]).ToLocalTime();
        DateTime datetimeStop = ((DateTime)a["finishtime"]).ToLocalTime();

        if (datetimeStart.Equals(DateTime.MinValue))
          datetimeStart = DateTime.Now;
        
        if (datetimeStop.Equals(DateTime.MinValue))
          datetimeStop = DateTime.Now;

        if (res.ContainsKey(zoneKey))
        {
          res[zoneKey] += (int)(datetimeStop - datetimeStart).TotalMinutes;
        }
        else
        {
          res.Add(zoneKey, (int)(datetimeStop - datetimeStart).TotalMinutes);
        }
      }

      //ABELLO_FI01/04/11_1 Ora il grafico con i downtime è ordinato
      Dictionary<string, int> resOrdinato = new Dictionary<string, int>();
      foreach (KeyValuePair<string, int> item in res.OrderByDescending(x => x.Value))         
      {                 
        resOrdinato.Add(item.Key,item.Value);
      }

      return resOrdinato;
    }

    public DateTime getLastRilevation()
    {
      DateTime res = DateTime.Now.ToUniversalTime();

      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_speed_uvmm_m";
      DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT max(data) FROM " + myTableName + ";");

      if (dt != null && dt.Rows.Count > 0)
      {
        res = (DateTime)dt.Rows[0]["max"];
      }

      return res;
    }

    //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
    public void drawSpeed(SyncroC.Graph.GraphLines graphLines_Speed, bool zone0Required, bool zone1Required, bool zone2Required, bool zone3Required, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      int maxSpeed = 0;
      Line speedZone0 = null;
      Line speedZone1 = null;
      Line speedZone2 = null;
      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      Line speedZone3 = null;

      if (zone0Required)
      {
        int maxSpeedRes = 0;
        speedZone0 = this.drawLineSpeed(0, from, to, MAX_NUM_PUNTI, out maxSpeedRes);
        speedZone0.Color = Color.Black;
        maxSpeed = Math.Max(maxSpeed, maxSpeedRes);
      }

      if (zone1Required)
      {
        int maxSpeedRes = 0;
        speedZone1 = this.drawLineSpeed(1, from, to, MAX_NUM_PUNTI, out maxSpeedRes);
        speedZone1.Color = Color.Brown;
        maxSpeed = Math.Max(maxSpeed, maxSpeedRes);
      }

      if (zone2Required)
      {
        int maxSpeedRes = 0;
        speedZone2 = this.drawLineSpeed(2, from, to, MAX_NUM_PUNTI, out maxSpeedRes);
        speedZone2.Color = Color.Blue;
        maxSpeed = Math.Max(maxSpeed, maxSpeedRes);
      }

      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      if (zone3Required)
      {
        int maxSpeedRes = 0;
        speedZone3 = this.drawLineSpeed(3, from, to, MAX_NUM_PUNTI, out maxSpeedRes);
        speedZone3.Color = Color.Green;
        maxSpeed = Math.Max(maxSpeed, maxSpeedRes);
      }

      LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();
      if (zone0Required)
        theLegendEntryCollection.Add(new LegendEntry(Color.Black, "0"));
      if (zone1Required)
        theLegendEntryCollection.Add(new LegendEntry(Color.Brown, "1"));
      if (zone2Required)
        theLegendEntryCollection.Add(new LegendEntry(Color.Blue, "2"));
      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      if (zone3Required)
        theLegendEntryCollection.Add(new LegendEntry(Color.Green, "3"));


      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      graphLines_Speed.drawMultiLine
        (
        null,
        speedZone0,
        speedZone1,
        speedZone2,
        speedZone3,
        null,
        theLegendEntryCollection,
        0,
        maxSpeed + 10,
        new Point(-1, -1),
        graphLines_Speed.getMyUnitMeasure(),
        from,
        to,
        forTrace
        );
    }

    public void drawSpeed(SyncroC.Graph.GraphLines graphLines_Speed, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      int maxSpeed = 0;
      graphLines_Speed.drawLine
        (
        this.drawLineSpeed(zoneNumber, from, to, MAX_NUM_PUNTI, out maxSpeed),
        0,
        maxSpeed + 10,
        new Point(-1, -1),
        graphLines_Speed.getMyUnitMeasure(),
        from,
        to,
        forTrace
        );
    }

    public Line drawLineSpeed(int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, out int maxSpeed)
    {
      //SPEED
      string columnZoneName = "zone" + zoneNumber;

      Line lineZone = this.getLineForComponent(from, to, "", "trace_speed_uvmm_m", columnZoneName, Color.Black, MAX_NUM_PUNTI);

      maxSpeed = 0;
      for (int a = 0; a < lineZone.Points.Count; a++)
      {
        lineZone.Points[a].YValue = lineZone.Points[a].YValue / 1000;
        maxSpeed = Math.Max(maxSpeed, (int)lineZone.Points[a].YValue);
      }

      return lineZone;
    }

    public void drawGlue(SyncroC.Graph.GraphLines graphLines_GlueGap, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      string glueGap1 = "";
      string glueGap2 = "";

      int min = 0;
      int max = 0;

      switch (zoneNumber)
      {
        case 0:
          if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.Present)
            glueGap1 = "gluegap_1";
          if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.Present)
            glueGap2 = "gluegap_2";
          min = Math.Min(SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, glueGap1), SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, glueGap2));
          max = Math.Max(SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, glueGap1), SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, glueGap2));
          break;
        case 1:
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.Gluegap1.Present)
            glueGap1 = "gluegap_11";
          glueGap2 = "";
          min = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, glueGap1);
          max = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, glueGap1);
          break;
        case 2:
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.Gluegap1.Present)
            glueGap1 = "gluegap_21";
          glueGap2 = "";
          min = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, glueGap1);
          max = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, glueGap1);
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case 3:
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.Gluegap1.Present)
            glueGap1 = "gluegap_31";
          glueGap2 = "";
          min = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, glueGap1);
          max = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, glueGap1);
          break;
        default:
          return;
      }

      if ((glueGap1 == "") && (glueGap2 == ""))
        return;

      LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();
      if (glueGap1 != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Black, "1"));
      if (glueGap2 != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Brown, "2"));

      graphLines_GlueGap.drawMultiLine
        (
        null,
        this.getLineForComponent(from, to, "trace_syc_", glueGap1, "percentage", Color.Black, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_syc_", glueGap2, "percentage", Color.Brown, MAX_NUM_PUNTI),
        null,
        null,
        null,
        theLegendEntryCollection,
        min,
        max,
        new Point(-1, -1),
        graphLines_GlueGap.getMyUnitMeasure(),
        from,
        to,
        forTrace
        );
    }

    public void drawWrapArmPosition(SyncroC.Graph.GraphLines graphLines_WrapPosition, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      string wrapArm1 = "";
      string wrapArm2 = "";
      string wrapArm3 = "";
      string wrapArm4 = "";
      string wrapArm5 = "";

      string wrapArm1Txt = "";
      string wrapArm2Txt = "";
      string wrapArm3Txt = "";
      string wrapArm4Txt = "";
      string wrapArm5Txt = "";

      int min = 0;
      int max = 100;

      switch (zoneNumber)
      {
        case 0:
          if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.Present)
          {
            wrapArm1 = "wraparm_11";
            wrapArm1Txt = "11";
          }
          if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.Present)
          {
            wrapArm2 = "wraparm_1";
            wrapArm2Txt = "1";
          }
          if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.Present)
          {
            wrapArm3 = "wraparm_2";
            wrapArm3Txt = "2";
          }
          if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.Present)
          {
            wrapArm4 = "wraparm_20";
            wrapArm4Txt = "20";
          }
          if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.Present)
          {
            wrapArm5 = "wraparm_31";
            wrapArm5Txt = "31";
          }
          break;
        case 1:
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEl.Present)
          {
            wrapArm1 = "wraparm_13";
            wrapArm1Txt = "El";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIl.Present)
          {
            wrapArm2 = "wraparm_23";
            wrapArm2Txt = "Il";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEm.Present)
          {
            wrapArm3 = "wraparm_12";
            wrapArm3Txt = "Em";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIm.Present)
          {
            wrapArm4 = "wraparm_22";
            wrapArm4Txt = "Im";
          }
          break;
        case 2:
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEl.Present)
          {
            wrapArm1 = "wraparm_15";
            wrapArm1Txt = "El";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIl.Present)
          {
            wrapArm2 = "wraparm_25";
            wrapArm2Txt = "Il";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEm.Present)
          {
            wrapArm3 = "wraparm_14";
            wrapArm3Txt = "Em";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIm.Present)
          {
            wrapArm4 = "wraparm_24";
            wrapArm4Txt = "Im";
          }
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case 3:
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterEl.Present)
          {
            wrapArm1 = "wraparm_17";
            wrapArm1Txt = "El";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterIl.Present)
          {
            wrapArm2 = "wraparm_27";
            wrapArm2Txt = "Il";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterEm.Present)
          {
            wrapArm3 = "wraparm_16";
            wrapArm3Txt = "Em";
          }
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterIm.Present)
          {
            wrapArm4 = "wraparm_26";
            wrapArm4Txt = "Im";
          }
          break;
        default:
          return;
      }

      if ((wrapArm1 == "") && (wrapArm2 == "") && (wrapArm3 == "") && (wrapArm4 == "") && (wrapArm5 == ""))
        return;

      LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();

      if (wrapArm1Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Black, wrapArm1Txt));

      if (wrapArm2Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Brown, wrapArm2Txt));

      if (wrapArm3Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Blue, wrapArm3Txt));

      if (wrapArm4Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Green, wrapArm4Txt));

      if (wrapArm5Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Salmon, wrapArm5Txt));

      graphLines_WrapPosition.drawMultiLine
        (
        null,
        this.getLineForComponent(from, to, "trace_syc_", wrapArm1, "percentage", Color.Black, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_syc_", wrapArm2, "percentage", Color.Brown, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_syc_", wrapArm3, "percentage", Color.Blue, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_syc_", wrapArm4, "percentage", Color.Green, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_syc_", wrapArm5, "percentage", Color.Salmon, MAX_NUM_PUNTI),
        theLegendEntryCollection,
        min,
        max,
        new Point(-1, -1),
        graphLines_WrapPosition.getMyUnitMeasure(),
        from,
        to,
        forTrace
        );
    }

    //ABELLO_FI24/11/2010_2 [PRC] Aggiunta storicizzazione e visualizzazione dell'umidità sopra/sotto
    public void drawMoisture(SyncroC.Graph.GraphLines graphLines_Moisture, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        graphLines_Moisture.Visible = false;
        return;
      }

      if (!SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.EnableBottom_UpperMoistureSensor)
      {
        graphLines_Moisture.Visible = false;
        return;
      }

      graphLines_Moisture.Visible = true;

      int min = 0;
      int max = 200;

      LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();
      theLegendEntryCollection.Add(new LegendEntry(Color.Black, DBManager.getDBManager().language.getTranslation("LevelName.Upper")));
      theLegendEntryCollection.Add(new LegendEntry(Color.Brown, DBManager.getDBManager().language.getTranslation("LevelName.Lower")));

      graphLines_Moisture.drawMultiLine
        (
        null,
        this.getLineForComponent(from, to, "trace_prc_", "moisture", "upper", Color.Black, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_prc_", "moisture", "bottom", Color.Brown, MAX_NUM_PUNTI),
        null,
        null,
        null,
        theLegendEntryCollection,
        min,
        max,
        new Point(-1, -1),
        graphLines_Moisture.getMyUnitMeasure(),
        from,
        to,
        forTrace
        );
    }

    public void drawDBSection(SyncroC.Graph.GraphLines graphLines_DBSection, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      string section1 = "";
      string section2 = "";
      string section3 = "";
      string section4 = "";
      string section5 = "";

      string section1Txt = "";
      string section2Txt = "";
      string section3Txt = "";
      string section4Txt = "";
      string section5Txt = "";

      int min = Int32.MaxValue;
      int max = Int32.MinValue;

      if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection1.Present)
      {
        section1 = "pressure_hot_section_1";
        section1Txt = "H1";
        min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, section1));
        max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, section1));
      }
      if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection2.Present)
      {
        section2 = "pressure_hot_section_2";
        section2Txt = "H2";
        min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, section2));
        max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, section2));
      }
      if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection3.Present)
      {
        section3 = "pressure_hot_section_3";
        section3Txt = "H3";
        min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, section3));
        max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, section3));
      }
      if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection4.Present)
      {
        section4 = "pressure_hot_section_4";
        section4Txt = "H4";
        min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, section4));
        max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, section4));
      }
      if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.ColdSection1.Present)
      {
        section5 = "pressure_cold_section_1";
        section5Txt = "C1";
        min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, section5));
        max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, section5));
      }

      if ((section1 == "") && (section2 == "") && (section3 == "") && (section4 == "") && (section5 == ""))
      {
        graphLines_DBSection.Visible = false;
        return;
      }
      else
      {
        graphLines_DBSection.Visible = true;
        LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();

        if (section1Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Black, section1Txt));

        if (section2Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Brown, section2Txt));

        if (section3Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Blue, section3Txt));

        if (section4Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Green, section4Txt));

        if (section5Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Salmon, section5Txt));

        graphLines_DBSection.drawMultiLine
          (
          null,
          this.getLineForComponent(from, to, "trace_syc_", section1, "percentage", Color.Black, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", section2, "percentage", Color.Brown, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", section3, "percentage", Color.Blue, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", section4, "percentage", Color.Green, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", section5, "percentage", Color.Salmon, MAX_NUM_PUNTI),
          theLegendEntryCollection,
          min,
          max,
          new Point(-1, -1),
          graphLines_DBSection.getMyUnitMeasure(),
          from,
          to,
          true
          );
      }
    }

    public void drawMainSteam(SyncroC.Graph.GraphLines graphLines_MainSteam, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      string steam1 = "";
      string steam2 = "";
      string steam3 = "";
      string steam4 = "";

      string steam1Txt = "";
      string steam2Txt = "";
      string steam3Txt = "";
      string steam4Txt = "";

      int min = Int32.MaxValue;
      int max = Int32.MinValue;

      switch (zoneNumber)
      {
        case 0:
          if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup0.Present)
          {
            steam1 = "steam_group_0";
            steam1Txt = "0";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam1));
          }
          if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup1.Present)
          {
            steam2 = "steam_group_1";
            steam2Txt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam2));
          }
          if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup2.Present)
          {
            steam3 = "steam_group_2";
            steam3Txt = "2";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam3));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam3));
          }
          if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup3.Present)
          {
            steam4 = "steam_group_3";
            steam4Txt = "3";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam4));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam4));
          }

          break;
        case 1:
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SFMainSteam.Present)
          {
            steam1 = "sfmainsteam_1";
            steam1Txt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam1));
          }
          break;
        case 2:
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SFMainSteam.Present)
          {
            steam1 = "sfmainsteam_2";
            steam1Txt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam1));
          }
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case 3:
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.SFMainSteam.Present)
          {
            steam1 = "sfmainsteam_3";
            steam1Txt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steam1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steam1));
          }
          break;
        default:
          return;
      }

      if ((steam1 == "") && (steam2 == "") && (steam3 == "") && (steam4 == ""))
      {
        graphLines_MainSteam.Visible = false;
        return;
      }
      else
      {
        graphLines_MainSteam.Visible = true;

        LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();

        if (steam1Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Black, steam1Txt));

        if (steam2Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Brown, steam2Txt));

        if (steam3Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Blue, steam3Txt));

        if (steam4Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Green, steam4Txt));

        graphLines_MainSteam.drawMultiLine
          (
          null,
          this.getLineForComponent(from, to, "trace_syc_", steam1, "percentage", Color.Black, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", steam2, "percentage", Color.Brown, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", steam3, "percentage", Color.Blue, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", steam4, "percentage", Color.Green, MAX_NUM_PUNTI),
          null,
          theLegendEntryCollection,
          min,
          max,
          new Point(-1, -1),
          graphLines_MainSteam.getMyUnitMeasure(),
          from,
          to,
          true
          );
      }
    }

    public void drawTension(SyncroC.Graph.GraphLines graphLines_Tension, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      if (!SyncroC.DataBase.PRCManager.getPRCManager().existsAControlForCorrectEndToEndWarp())
      {
        graphLines_Tension.Visible = false;
        return;
      }

      string tension1 = "";
      string tension2 = "";
      string tension3 = "";
      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      string tension4 = "";

      string tension1Txt = "";
      string tension2Txt = "";
      string tension3Txt = "";
      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      string tension4Txt = "";

      int min = Int32.MaxValue;
      int max = Int32.MinValue;

      switch (zoneNumber)
      {
        case 0:
          if (DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.Present)
          {
            tension1 = "papertension_21";
            tension1Txt = "BR 1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension1));
          }
          if (DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.Present)
          {
            tension2 = "papertension_22";
            tension2Txt = "BR 2";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension2));
          }
          if (DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.Present)
          {
            tension3 = "papertension_1";
            tension3Txt = "SPL 1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension3));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension3));
          }
          //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
          if (DBManager.getDBManager().lineConfig.Bridge3Config.PaperTension.Present)
          {
            tension4 = "papertension_23";
            tension4Txt = "BR 3";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension4));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension4));
          }
          break;
        case 1:
          if (DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.Present)
          {
            tension1 = "papertension_2";
            tension1Txt = "SPL 2";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension1));
          }
          if (DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.Present)
          {
            tension2 = "papertension_3";
            tension2Txt = "SPL 3";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension2));
          }
          break;
        case 2:
          if (DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.Present)
          {
            tension1 = "papertension_4";
            tension1Txt = "SPL 4";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension1));
          }
          if (DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.Present)
          {
            tension2 = "papertension_5";
            tension2Txt = "SPL 5";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension2));
          }
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case 3:
          if (DBManager.getDBManager().lineConfig.Splicer6Config.PaperTension.Present)
          {
            tension1 = "papertension_6";
            tension1Txt = "SPL 6";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension1));
          }
          if (DBManager.getDBManager().lineConfig.Splicer7Config.PaperTension.Present)
          {
            tension2 = "papertension_7";
            tension2Txt = "SPL 7";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, tension2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, tension2));
          }
          break;
        default:
          return;
      }

      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      if ((tension1 == "") && (tension2 == "") && (tension3 == "") && (tension4 == ""))
      {
        graphLines_Tension.Visible = false;
        return;
      }
      else
      {
        graphLines_Tension.Visible = true;

        LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();
        if (tension1Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Black, tension1Txt));
        if (tension2Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Brown, tension2Txt));
        if (tension3Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Blue, tension3Txt));
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        if (tension4Txt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Green, tension4Txt));

        graphLines_Tension.drawMultiLine
          (
          null,
          this.getLineForComponent(from, to, "trace_syc_", tension1, "tension", Color.Black, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", tension2, "tension", Color.Brown, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", tension3, "tension", Color.Blue, MAX_NUM_PUNTI),
          this.getLineForComponent(from, to, "trace_syc_", tension3, "tension", Color.Green, MAX_NUM_PUNTI),
          null,
          theLegendEntryCollection,
          min,
          max,
          new Point(-1, -1),
          graphLines_Tension.getMyUnitMeasure(),
          from,
          to,
          true
          );
      }
    }

    public void drawSteamShower(SyncroC.Graph.GraphLines graphLines_SteamShower, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      if (zoneNumber == 0)
      {
        graphLines_SteamShower.Visible = false;
        return;
      }

      string steamShower = "";
      string steamShowerTxt = "";

      int min = Int32.MaxValue;
      int max = Int32.MinValue;

      switch (zoneNumber)
      {
        case 1:
          if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SteamShower.Present)
          {
            steamShower = "steamshower_1";
            steamShowerTxt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steamShower));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steamShower));
          }
          break;
        case 2:
          if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SteamShower.Present)
          {
            steamShower = "steamshower_2";
            steamShowerTxt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steamShower));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steamShower));
          }
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case 3:
          if (DBManager.getDBManager().lineConfig.SingleFacer3Config.SteamShower.Present)
          {
            steamShower = "steamshower_3";
            steamShowerTxt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, steamShower));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, steamShower));
          }
          break;
        default:
          return;
      }

      if (steamShower == "")
      {
        graphLines_SteamShower.Visible = false;
        return;
      }
      else
      {
        graphLines_SteamShower.Visible = true;

        LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();

        if (steamShowerTxt != "")
          theLegendEntryCollection.Add(new LegendEntry(Color.Black, steamShowerTxt));

        graphLines_SteamShower.drawMultiLine
          (
          null,
          this.getLineForComponent(from, to, "trace_syc_", steamShower, "percentage", Color.Black, MAX_NUM_PUNTI),
          null,
          null,
          null,
          null,
          theLegendEntryCollection,
          min,
          max,
          new Point(-1, -1),
          graphLines_SteamShower.getMyUnitMeasure(),
          from,
          to,
          true
          );
      }
    }

    //ABELLO_FI09/05/2011_1 Aggiunta gestione del Process senza sensori di temperatura
    public void drawTemperature(SyncroC.Graph.GraphLines graphLines_Temp, int zoneNumber, DateTime from, DateTime to, int MAX_NUM_PUNTI, bool forTrace)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        graphLines_Temp.Visible = false;
        return;
      }

      string wrapArm1 = "";
      string wrapArm2 = "";
      string wrapArm3 = "";
      string wrapArm4 = "";
      string wrapArm5 = "";

      string wrapArm1Txt = "";
      string wrapArm2Txt = "";
      string wrapArm3Txt = "";
      string wrapArm4Txt = "";
      string wrapArm5Txt = "";

      int min = Int32.MaxValue;
      int max = Int32.MinValue;

      switch (zoneNumber)
      {
        case 0:
          if 
            (
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.Present &&
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm1 = "wraparm_11";
            wrapArm1Txt = "11";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm1));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.Present &&
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm2 = "wraparm_1";
            wrapArm2Txt = "1";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm2));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.Present &&
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm3 = "wraparm_2";
            wrapArm3Txt = "2";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm3));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm3));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.Present &&
            DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm4 = "wraparm_20";
            wrapArm4Txt = "20";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm4));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm4));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.Present &&
            DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm5 = "wraparm_31";
            wrapArm5Txt = "31";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm5));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm5));
          }
          break;
        case 1:
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEl.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEl.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm1 = "wraparm_13";
            wrapArm1Txt = "El";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm1));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIl.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIl.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm2 = "wraparm_23";
            wrapArm2Txt = "Il";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm2));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEm.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEm.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm3 = "wraparm_12";
            wrapArm3Txt = "Em";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm3));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm3));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIm.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIm.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm4 = "wraparm_22";
            wrapArm4Txt = "Im";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm4));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm4));
          }
          break;
        case 2:
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEl.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEl.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm1 = "wraparm_15";
            wrapArm1Txt = "El";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm1));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIl.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIl.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm2 = "wraparm_25";
            wrapArm2Txt = "Il";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm2));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEm.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEm.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm3 = "wraparm_14";
            wrapArm3Txt = "Em";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm3));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm3));
          }
          if 
            (
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIm.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIm.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm4 = "wraparm_24";
            wrapArm4Txt = "Im";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm4));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm4));
          }
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case 3:
          if
            (
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterEl.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterEl.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm1 = "wraparm_17";
            wrapArm1Txt = "El";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm1));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm1));
          }
          if
            (
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterIl.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterIl.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm2 = "wraparm_27";
            wrapArm2Txt = "Il";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm2));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm2));
          }
          if
            (
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterEm.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterEm.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm3 = "wraparm_16";
            wrapArm3Txt = "Em";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm3));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm3));
          }
          if
            (
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterIm.Present &&
            DBManager.getDBManager().lineConfig.SingleFacer3Config.PreheaterIm.WrapArmTemperatureConfig.TemperatureSensorPresent
            )
          {
            wrapArm4 = "wraparm_26";
            wrapArm4Txt = "Im";
            min = Math.Min(min, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, wrapArm4));
            max = Math.Max(max, SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, wrapArm4));
          }
          break;
        default:
          return;
      }

      if ((wrapArm1 == "") && (wrapArm2 == "") && (wrapArm3 == "") && (wrapArm4 == "") && (wrapArm5 == ""))
        return;

      LegendEntryCollection theLegendEntryCollection = new LegendEntryCollection();

      if (wrapArm1Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Black, wrapArm1Txt));

      if (wrapArm2Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Brown, wrapArm2Txt));

      if (wrapArm3Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Blue, wrapArm3Txt));

      if (wrapArm4Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Green, wrapArm4Txt));

      if (wrapArm5Txt != "")
        theLegendEntryCollection.Add(new LegendEntry(Color.Salmon, wrapArm5Txt));

      graphLines_Temp.drawMultiLine
        (
        null,
        this.getLineForComponent(from, to, "trace_prc_", wrapArm1, "temperature", Color.Black, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_prc_", wrapArm2, "temperature", Color.Brown, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_prc_", wrapArm3, "temperature", Color.Blue, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_prc_", wrapArm4, "temperature", Color.Green, MAX_NUM_PUNTI),
        this.getLineForComponent(from, to, "trace_prc_", wrapArm5, "temperature", Color.Salmon, MAX_NUM_PUNTI),
        theLegendEntryCollection,
        min,
        max,
        new Point(-1, -1),
        graphLines_Temp.getMyUnitMeasure(),
        from,
        to,
        forTrace
        );
    }

    public Line getLineForComponent(DateTime from, DateTime to, string preTableName, string componentName, string columnName, Color lineColor, int MAX_NUM_PUNTI)
    {
      //ABELLO_FI27/04/2011_2 [RECORD] Rivisto il calcolo dei punti per le curve sui grafici
      Line lineRes = null;

      if (componentName != "")
      {
        lineRes = new Line(lineColor);
        lineRes.Width = 1;

        List<List<SyncroC.Graph.Pair<int, int>>> listValueForPoints = new List<List<SyncroC.Graph.Pair<int, int>>>();
        for (int j = 0; j < MAX_NUM_PUNTI; j++)
          listValueForPoints.Add(new List<SyncroC.Graph.Pair<int, int>>());

        string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + "." + preTableName + componentName;

        string where = String.Format("('{0}' <= data AND data <= '{1}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY data ASC;");

        double offset = ((to.ToLocalTime() - from.ToLocalTime()).TotalSeconds) / MAX_NUM_PUNTI;

        for (int idx = 0; idx < dt.Rows.Count; idx++ )
        {
          DataRow a = dt.Rows[idx];

          DateTime datetime = ((DateTime)a["data"]).ToLocalTime();

          int index = (int)(((datetime - from.ToLocalTime()).TotalSeconds) / offset);

          if (index <= 0)
            index = 0;

          if (index >= MAX_NUM_PUNTI)
            index = MAX_NUM_PUNTI - 1;

          DateTime nextDatetime = (idx < dt.Rows.Count-1) ? ((DateTime)dt.Rows[idx + 1]["data"]).ToLocalTime() : to;

          listValueForPoints[index].Add(new SyncroC.Graph.Pair<int, int>((int)a[columnName], (int)(nextDatetime - datetime).TotalSeconds));
        }

        if (listValueForPoints[0].Count == 0) 
        {
          //NON HO RILEVAZIONI INIZIALI QUINDI VADO INDIETRO FINO A TROVARNE LA PRIMA
          where = String.Format("(data <= '{0}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from));
          dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY data DESC LIMIT 1;");

          if (dt.Rows.Count != 0)
            listValueForPoints[0].Add(new SyncroC.Graph.Pair<int, int>((int)dt.Rows[0][columnName],1));
        }

        int[] arrayValueForPoints = new int[MAX_NUM_PUNTI];
        for (int j = 0; j < MAX_NUM_PUNTI; j++)
        {
          if (listValueForPoints[j].Count == 0)
            arrayValueForPoints[j] = -1;
          else
          {
            int sum = 0;
            int pesi = 0;
            foreach (SyncroC.Graph.Pair<int, int> a in listValueForPoints[j])
            {
              sum = sum + (a.First * a.Second);
              pesi = pesi + a.Second;
            }
            
            //media pesata
            arrayValueForPoints[j] = sum / pesi;
          }
        }

        for (int j = 0; j < MAX_NUM_PUNTI; j++)
        {
          if (arrayValueForPoints[j] == -1)
            arrayValueForPoints[j] = (j == 0) ? arrayValueForPoints[j + 1] : arrayValueForPoints[j - 1];

          lineRes.AddPoint(
              j,
              arrayValueForPoints[j]
              );
        }

        lineRes.AddPoint(
              MAX_NUM_PUNTI,
              arrayValueForPoints[MAX_NUM_PUNTI - 1]
              );

      }
      return lineRes;
    }

  }
}
