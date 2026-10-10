using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SyncroC.DataBase
{
  class PRCManager
  {
    private List<string> listOfOverrideTables = null;

    private static PRCManager prcManager;

    public static System.Drawing.Color COLOR_PRC_OVERRIDE = System.Drawing.Color.Yellow;

    public static long MAX_NUM_MESSAGES = 100;

    /// <summary>
    /// Factory.
    /// </summary>
    /// <returns>The one and only PRCManager object of the application.</returns>
    public static PRCManager getPRCManager()
    {
      if (prcManager == null)
        prcManager = new PRCManager();
      return prcManager;
    }

    public bool existsAControlForCorrectUpDownWarp()
    {
      if (DBManager.getDBManager().lineConfig.DoubleBacker)
      {
        //Wrap Arm
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.Present)
          return true;

        //Steam Shower
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamShower.Present)
          return true;

        //Steam Sections
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup1.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup2.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup3.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup0.Present)
          return true;

        //Screw Jack Sections
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection1.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection2.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection3.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection4.Present)
          return true;

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.ColdSection1.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.GlueUnit)
      {
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG4.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.PreHeatersTern)
      {
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater3.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater30.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.SingleFacer1)
      {
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIl.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIm.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEl.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.Gluegap1.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SteamShower.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SFMainSteam.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIl.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIm.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEl.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.Gluegap1.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SteamShower.Present)
          return true;
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SFMainSteam.Present)
          return true;
      }

      return false;
    }

    public bool existsAControlForCorrectEndToEndWarp()
    {
      if (DBManager.getDBManager().lineConfig.Splicer1)
      {
        if (DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.Splicer2)
      {
        if (DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.Bridge1)
      {
        if (DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.Splicer3)
      {
        if (DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.Splicer4)
      {
        if (DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.Bridge2)
      {
        if (DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.Present)
          return true;
      }

      if (DBManager.getDBManager().lineConfig.Splicer5)
      {
        if (DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.Present)
          return true;
      }

      return false;
    }

    public void exportStandardCurves()
    {
      if 
        (
        SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT &&
        SyncroC.Utility.Settings.getSettings().configuration.getOnServer() &&
        SyncroC.Utility.Settings.getSettings().configuration.PRC
        )
      {
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().getAllDataBaseTable();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            foreach (DataRow dr in dt.Rows)
            {
              string nomeTabella = (string)dr[0];
              if (nomeTabella.StartsWith("prc_std_"))
              {
                string directoryDestinazione = SyncroC.Utility.Settings.getSettings().paths.ServerConfig + "ProcessControl\\";
                if (!System.IO.Directory.Exists(directoryDestinazione))
                  System.IO.Directory.CreateDirectory(directoryDestinazione);

                string fileNameDestinazione = String.Format("{0}{1}.txt", directoryDestinazione, nomeTabella);

                if (System.IO.File.Exists(fileNameDestinazione))
                  System.IO.File.Delete(fileNameDestinazione);

                SyncroC.DataBase.SqlManager.getSqlManager().backupDataBase(fileNameDestinazione, nomeTabella);
              }
            }
          }
        }
      }
    }

    public void generateTreeNode(System.Windows.Forms.TreeView treeViewPRC, System.Windows.Forms.ContextMenuStrip contextMenuStrip_PRCCopiaIncolla)
    {
      treeViewPRC.SuspendLayout();

      treeViewPRC.Nodes.Clear();

      bool existsAControlForCorrectUpDownWarp = this.existsAControlForCorrectUpDownWarp();
      bool existsAControlForCorrectEndToEndWarp = this.existsAControlForCorrectEndToEndWarp();

      if (existsAControlForCorrectUpDownWarp || existsAControlForCorrectEndToEndWarp)
      {
        treeViewPRC.Nodes.Add("warp", DBManager.getDBManager().message.getTranslatedMessage("§PRC_02"));

        if (existsAControlForCorrectUpDownWarp)
        {
          treeViewPRC.Nodes["warp"].Nodes.Add("warp_updown", this.getSimpleNameForComponent(false, "warp_updown"));
          //treeViewPRC.Nodes["warp"].Nodes["warp_updown"].Nodes.Add("warp_updown|-3", "-3", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_updown"].Nodes.Add("warp_updown|-2", "-2", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_updown"].Nodes.Add("warp_updown|-1", "-1", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_updown"].Nodes.Add("warp_updown|1", " 1", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_updown"].Nodes.Add("warp_updown|2", " 2", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_updown"].Nodes.Add("warp_updown|3", " 3", 0);
        }
        if (existsAControlForCorrectEndToEndWarp)
        {
          treeViewPRC.Nodes["warp"].Nodes.Add("warp_endtoend", this.getSimpleNameForComponent(false, "warp_endtoend"));
          //treeViewPRC.Nodes["warp"].Nodes["warp_endtoend"].Nodes.Add("warp_endtoend|-3", "-3", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_endtoend"].Nodes.Add("warp_endtoend|-2", "-2", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_endtoend"].Nodes.Add("warp_endtoend|-1", "-1", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_endtoend"].Nodes.Add("warp_endtoend|1", " 1", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_endtoend"].Nodes.Add("warp_endtoend|2", " 2", 0);
          //treeViewPRC.Nodes["warp"].Nodes["warp_endtoend"].Nodes.Add("warp_endtoend|3", " 3", 0);
        }
      }

      if (DBManager.getDBManager().lineConfig.DoubleBacker)
      {
        treeViewPRC.Nodes.Add("DB", "DB");

        //wrap arm
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("wraparm_31", this.getSimpleNameForComponent(false, "wraparm_31"));
          treeViewPRC.Nodes["DB"].Nodes["wraparm_31"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("wraparm_41", this.getSimpleNameForComponent(false, "wraparm_41"));
          treeViewPRC.Nodes["DB"].Nodes["wraparm_41"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        //Steam Shower
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamShower.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("steamshower_4", this.getSimpleNameForComponent(false, "steamshower_4"));
          treeViewPRC.Nodes["DB"].Nodes["steamshower_4"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

				//Steam Sections
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup0.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("steam_group_0", this.getSimpleNameForComponent(false, "steam_group_0"));
          treeViewPRC.Nodes["DB"].Nodes["steam_group_0"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup1.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("steam_group_1", this.getSimpleNameForComponent(false, "steam_group_1"));
          treeViewPRC.Nodes["DB"].Nodes["steam_group_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup2.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("steam_group_2", this.getSimpleNameForComponent(false, "steam_group_2"));
          treeViewPRC.Nodes["DB"].Nodes["steam_group_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup3.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("steam_group_3", this.getSimpleNameForComponent(false, "steam_group_3"));
          treeViewPRC.Nodes["DB"].Nodes["steam_group_3"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

				//Screw Jack Sections
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection1.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("pressure_hot_section_1", this.getSimpleNameForComponent(false, "pressure_hot_section_1"));
          treeViewPRC.Nodes["DB"].Nodes["pressure_hot_section_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection2.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("pressure_hot_section_2", this.getSimpleNameForComponent(false, "pressure_hot_section_2"));
          treeViewPRC.Nodes["DB"].Nodes["pressure_hot_section_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection3.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("pressure_hot_section_3", this.getSimpleNameForComponent(false, "pressure_hot_section_3"));
          treeViewPRC.Nodes["DB"].Nodes["pressure_hot_section_3"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection4.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("pressure_hot_section_4", this.getSimpleNameForComponent(false, "pressure_hot_section_4"));
          treeViewPRC.Nodes["DB"].Nodes["pressure_hot_section_4"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.ColdSection1.Present)
        {
          treeViewPRC.Nodes["DB"].Nodes.Add("pressure_cold_section_1", this.getSimpleNameForComponent(false, "pressure_cold_section_1"));
          treeViewPRC.Nodes["DB"].Nodes["pressure_cold_section_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["DB"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("DB");
        else
          treeViewPRC.Nodes["DB"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Splicer1)
      {
        treeViewPRC.Nodes.Add("SPL1", "SPL1");

        if (DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["SPL1"].Nodes.Add("papertension_1", this.getSimpleNameForComponent(false, "papertension_1"));
          treeViewPRC.Nodes["SPL1"].Nodes["papertension_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SPL1"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SPL1");
        else
          treeViewPRC.Nodes["SPL1"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.GlueUnit)
      {
        treeViewPRC.Nodes.Add("GU", "GU");

        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.Present)
        {
          treeViewPRC.Nodes["GU"].Nodes.Add("gluegap_1", this.getSimpleNameForComponent(false, "gluegap_1"));
          treeViewPRC.Nodes["GU"].Nodes["gluegap_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.Present)
        {
          treeViewPRC.Nodes["GU"].Nodes.Add("gluegap_2", this.getSimpleNameForComponent(false, "gluegap_2"));
          treeViewPRC.Nodes["GU"].Nodes["gluegap_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.Present)
        {
          treeViewPRC.Nodes["GU"].Nodes.Add("gluegap_3", this.getSimpleNameForComponent(false, "gluegap_3"));
          treeViewPRC.Nodes["GU"].Nodes["gluegap_3"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG4.Present)
        {
          treeViewPRC.Nodes["GU"].Nodes.Add("gluegap_4", this.getSimpleNameForComponent(false, "gluegap_4"));
          treeViewPRC.Nodes["GU"].Nodes["gluegap_4"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["GU"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("GU");
        else
          treeViewPRC.Nodes["GU"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.PreHeatersTern)
      {
        treeViewPRC.Nodes.Add("PHT", "PHT");

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_11", this.getSimpleNameForComponent(false, "wraparm_11"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_11"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_1", this.getSimpleNameForComponent(false, "wraparm_1"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_10", this.getSimpleNameForComponent(false, "wraparm_10"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_10"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_2", this.getSimpleNameForComponent(false, "wraparm_2"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_20", this.getSimpleNameForComponent(false, "wraparm_20"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_20"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater3.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_3", this.getSimpleNameForComponent(false, "wraparm_3"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_3"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater30.Present)
        {
          treeViewPRC.Nodes["PHT"].Nodes.Add("wraparm_30", this.getSimpleNameForComponent(false, "wraparm_30"));
          treeViewPRC.Nodes["PHT"].Nodes["wraparm_30"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["PHT"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("PHT");
        else
          treeViewPRC.Nodes["PHT"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Splicer2)
      {
        treeViewPRC.Nodes.Add("SPL2", "SPL2");

        if (DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["SPL2"].Nodes.Add("papertension_2", this.getSimpleNameForComponent(false, "papertension_2"));
          treeViewPRC.Nodes["SPL2"].Nodes["papertension_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SPL2"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SPL2");
        else
          treeViewPRC.Nodes["SPL2"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.SingleFacer1)
      {
        treeViewPRC.Nodes.Add("SF1", "SF1");

        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIl.Present)
        {
          treeViewPRC.Nodes["SF1"].Nodes.Add("wraparm_23", this.getSimpleNameForComponent(false, "wraparm_23"));
          treeViewPRC.Nodes["SF1"].Nodes["wraparm_23"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIm.Present)
        {
          treeViewPRC.Nodes["SF1"].Nodes.Add("wraparm_22", this.getSimpleNameForComponent(false, "wraparm_22"));
          treeViewPRC.Nodes["SF1"].Nodes["wraparm_22"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEl.Present)
        {
          treeViewPRC.Nodes["SF1"].Nodes.Add("wraparm_13", this.getSimpleNameForComponent(false, "wraparm_13"));
          treeViewPRC.Nodes["SF1"].Nodes["wraparm_13"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.Gluegap1.Present)
        {
          treeViewPRC.Nodes["SF1"].Nodes.Add("gluegap_11", this.getSimpleNameForComponent(false, "gluegap_11"));
          treeViewPRC.Nodes["SF1"].Nodes["gluegap_11"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SteamShower.Present)
        {
          treeViewPRC.Nodes["SF1"].Nodes.Add("steamshower_1", this.getSimpleNameForComponent(false, "steamshower_1"));
          treeViewPRC.Nodes["SF1"].Nodes["steamshower_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SFMainSteam.Present)
        {
          treeViewPRC.Nodes["SF1"].Nodes.Add("sfmainsteam_1", this.getSimpleNameForComponent(false, "sfmainsteam_1"));
          treeViewPRC.Nodes["SF1"].Nodes["sfmainsteam_1"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SF1"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SF1");
        else
          treeViewPRC.Nodes["SF1"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Bridge1)
      {
        treeViewPRC.Nodes.Add("BR1", "BR1");

        if (DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["BR1"].Nodes.Add("papertension_21", this.getSimpleNameForComponent(false, "papertension_21"));
          treeViewPRC.Nodes["BR1"].Nodes["papertension_21"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["BR1"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("BR1");
        else
          treeViewPRC.Nodes["BR1"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Splicer3)
      {
        treeViewPRC.Nodes.Add("SPL3", "SPL3");

        if (DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["SPL3"].Nodes.Add("papertension_3", this.getSimpleNameForComponent(false, "papertension_3"));
          treeViewPRC.Nodes["SPL3"].Nodes["papertension_3"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SPL3"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SPL3");
        else
          treeViewPRC.Nodes["SPL3"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Splicer4)
      {
        treeViewPRC.Nodes.Add("SPL4", "SPL4");

        if (DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["SPL4"].Nodes.Add("papertension_4", this.getSimpleNameForComponent(false, "papertension_4"));
          treeViewPRC.Nodes["SPL4"].Nodes["papertension_4"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SPL4"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SPL4");
        else
          treeViewPRC.Nodes["SPL4"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        treeViewPRC.Nodes.Add("SF2", "SF2");

        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIl.Present)
        {
          treeViewPRC.Nodes["SF2"].Nodes.Add("wraparm_25", this.getSimpleNameForComponent(false, "wraparm_25"));
          treeViewPRC.Nodes["SF2"].Nodes["wraparm_25"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIm.Present)
        {
          treeViewPRC.Nodes["SF2"].Nodes.Add("wraparm_24", this.getSimpleNameForComponent(false, "wraparm_24"));
          treeViewPRC.Nodes["SF2"].Nodes["wraparm_24"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEl.Present)
        {
          treeViewPRC.Nodes["SF2"].Nodes.Add("wraparm_15", this.getSimpleNameForComponent(false, "wraparm_15"));
          treeViewPRC.Nodes["SF2"].Nodes["wraparm_15"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.Gluegap1.Present)
        {
          treeViewPRC.Nodes["SF2"].Nodes.Add("gluegap_21", this.getSimpleNameForComponent(false, "gluegap_21"));
          treeViewPRC.Nodes["SF2"].Nodes["gluegap_21"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SteamShower.Present)
        {
          treeViewPRC.Nodes["SF2"].Nodes.Add("steamshower_2", this.getSimpleNameForComponent(false, "steamshower_2"));
          treeViewPRC.Nodes["SF2"].Nodes["steamshower_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SFMainSteam.Present)
        {
          treeViewPRC.Nodes["SF2"].Nodes.Add("sfmainsteam_2", this.getSimpleNameForComponent(false, "sfmainsteam_2"));
          treeViewPRC.Nodes["SF2"].Nodes["sfmainsteam_2"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SF2"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SF2");
        else
          treeViewPRC.Nodes["SF2"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Bridge2)
      {
        treeViewPRC.Nodes.Add("BR2", "BR2");

        if (DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["BR2"].Nodes.Add("papertension_22", this.getSimpleNameForComponent(false, "papertension_22"));
          treeViewPRC.Nodes["BR2"].Nodes["papertension_22"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["BR2"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("BR2");
        else
          treeViewPRC.Nodes["BR2"].ExpandAll();
      }

      if (DBManager.getDBManager().lineConfig.Splicer5)
      {
        treeViewPRC.Nodes.Add("SPL5", "SPL5");

        if (DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.Present)
        {
          treeViewPRC.Nodes["SPL5"].Nodes.Add("papertension_5", this.getSimpleNameForComponent(false, "papertension_5"));
          treeViewPRC.Nodes["SPL5"].Nodes["papertension_5"].ContextMenuStrip = contextMenuStrip_PRCCopiaIncolla;
        }

        if (treeViewPRC.Nodes["SPL5"].Nodes.Count == 0)
          treeViewPRC.Nodes.RemoveByKey("SPL5");
        else
          treeViewPRC.Nodes["SPL5"].ExpandAll();
      }

      treeViewPRC.ResumeLayout();
    }

    public bool canPrint()
    {
      bool res = false;

      if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count != 0)
      {
        if ((System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 1) && (System.Drawing.Printing.PrinterSettings.InstalledPrinters[0] == "Microsoft XPS Document Writer"))
          res = false;
        else
          res = true;
      }
      else
        res = false;

      return res;
    }

    private List<string> getAllOverrideTable()
    {
      if (this.listOfOverrideTables == null)
      {
        this.listOfOverrideTables = new List<string>();

        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().getAllDataBaseTable();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            foreach (DataRow dr in dt.Rows)
            {
              string nomeTabella = (string)dr[0];
              if (nomeTabella.StartsWith("prc_ove_"))
              {
                this.listOfOverrideTables.Add(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + "." + nomeTabella);
              }
            }
          }
        }
      }

      return this.listOfOverrideTables;
    }

    public List<string> getComponentsWhereIsQualityOverride(string qualityid)
    {
      System.Collections.Generic.List<string> res = new System.Collections.Generic.List<string>();

      System.Collections.Generic.List<string> listTableWhereIsQualityOverride = this.getTableWhereIsQualityOverride(qualityid);

      foreach (string a in listTableWhereIsQualityOverride)
      {
        res.Add(a.Replace("prc_ove_", ""));
      }
      return res;
    }

    public List<string> getTableWhereIsQualityOverride(string qualityid)
    {
      List<string> res = new List<string>();

      List<string> myListOfOverrideTable = this.getAllOverrideTable();
      string selectSuTutteTabelleOverride = "";
      for (int i = 0; i <= myListOfOverrideTable.Count - 1; i++)
      {
        selectSuTutteTabelleOverride += String.Format("SELECT a.qualityid,p.relname FROM {0} a,pg_class p WHERE (a.qualityid='{1}' AND a.tableoid=p.oid)", myListOfOverrideTable[i], qualityid);
        if (i != myListOfOverrideTable.Count - 1)
          selectSuTutteTabelleOverride += " UNION ";
        else
          selectSuTutteTabelleOverride += ";";
      }

      DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect(selectSuTutteTabelleOverride);
      if (dt != null)
      {
        if (dt.Rows.Count > 0)
        {
          foreach (DataRow dr in dt.Rows)
          { 
            res.Add((string)dr[1]);
          }
        }
      }

      return res;
    }

    public bool getIsQualityOverride(string qualityid)
    {
      bool res = false;

      List<string> myListOfOverrideTable = this.getAllOverrideTable();
      string selectSuTutteTabelleOverride = "";
      for (int i = 0; i <= myListOfOverrideTable.Count - 1; i++)
      {
        selectSuTutteTabelleOverride += String.Format("SELECT qualityid FROM {0} WHERE qualityid='{1}'", myListOfOverrideTable[i], qualityid);
        if (i != myListOfOverrideTable.Count - 1)
          selectSuTutteTabelleOverride += " UNION ";
        else
          selectSuTutteTabelleOverride += ";";
      }

      DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect(selectSuTutteTabelleOverride);
      if (dt != null)
      {
        if (dt.Rows.Count > 0)
        {
          res = true;
        }
      }

      return res;
    }

    public bool isWrapArmWith2Paper(string theWrapArm)
    {
      return (theWrapArm.Equals("wraparm_1", StringComparison.OrdinalIgnoreCase) ||
        theWrapArm.Equals("wraparm_10", StringComparison.OrdinalIgnoreCase) ||
        theWrapArm.Equals("wraparm_2", StringComparison.OrdinalIgnoreCase) ||
        theWrapArm.Equals("wraparm_20", StringComparison.OrdinalIgnoreCase) ||
        theWrapArm.Equals("wraparm_3", StringComparison.OrdinalIgnoreCase) ||
        theWrapArm.Equals("wraparm_30", StringComparison.OrdinalIgnoreCase));
    }

    public bool isComponentWith2Paper(string theComponent)
    {
      return 
        (
        theComponent.StartsWith("gluegap") ||
        //theComponent.StartsWith("sfmainsteam") ||
        theComponent.StartsWith("papertension_1") ||
        theComponent.StartsWith("papertension_21") ||
        theComponent.StartsWith("papertension_22") ||
        theComponent.StartsWith("papertension_23") ||
        this.isWrapArmWith2Paper(theComponent) 
        );
    }

    public object[] generateFluteArray(string theComponent)
    {
      bool completeFluteId = false;
      if (theComponent.StartsWith("warp"))
      {
        completeFluteId = true;
      }
      else
      {
        switch (theComponent)
        {
          case "papertension_1":
          case "papertension_21":
          case "papertension_22":
          case "papertension_23":
          case "wraparm_11":
          case "wraparm_31":
          case "wraparm_41":
          case "wraparm_1":
          case "wraparm_10":
          case "wraparm_2":
          case "wraparm_20":
          case "steam_group_1":
          case "steam_group_2":
          case "steam_group_3":
          case "steam_group_0":
          case "steamshower_4":
          case "gluegap_1":
          case "gluegap_2":
          case "pressure_hot_section_1":
          case "pressure_hot_section_2":
          case "pressure_hot_section_3":
          case "pressure_hot_section_4":
          case "pressure_cold_section_1":

          //RFAZZI_FI16/06/2010_1. Aggiunta possibilità di usare l'onda completa per il controllo da PRC del GlueGap e Fasciature ai SF
          case "gluegap_11":
          case "gluegap_21":

          case "wraparm_23":
          case "wraparm_22":
          case "wraparm_13":
          case "wraparm_12":

          case "wraparm_25":
          case "wraparm_24":
          case "wraparm_15":
          case "wraparm_14": 
          
            completeFluteId = true;
            break;
          default:
            completeFluteId = false;
            break;
        }
      }

      DataTable flute = DBManager.getDBManager().flute.getTable();
      List<object> res = new List<object>();
      res.Add("*");
      for (int i = 0; i < flute.Rows.Count; i++)
      {
        if (completeFluteId)
          res.Add(flute.Rows[i][Flute.FLUTE_ID].ToString());
        else
        {
          if(flute.Rows[i][Flute.FLUTE_ID].ToString().Length==1)
            res.Add(flute.Rows[i][Flute.FLUTE_ID].ToString());
        }
      }

      return res.ToArray();
    }

    public object[] generateWarpLimitArray()
    {
      return new object[] 
      {
          -3,
          -2,
          -1,
          1,
          2,
          3
      };
    }

    public object[] generatePaperTypeArray()
    {
      return new object[] 
      {
          "*",
          "VL",
          "L",
          "M",
          "H",
          "VH"
      };
    }

    public object[] generatePaperClassificationArray(bool withStar)
    {
      //RFAZZI_FI18/11/2010_4. Estesa codifica classificazione delle carte secondo caratteristiche presenti a Mariestad ( Plastiche e Alluminio )
      //RFAZZI_FI23/11/2010_1. Aggiunta codifica alla classificazione delle carte secondo caratteristiche presenti a Mariestad  ( Reciclyng Fluting ) [PRC]
      if (withStar)
        return new object[] 
        {
          "*",
          "K", //Kraft
          "Kw", //Kraft Bianco
          "L", //Liner
          "Lw", //Liner Bianco
          "T", //Test liner
          "Tw", //Test liner Bianco
          "C", //Camoscio
          "S", //Semichimica
          "Us", //Uso Semichimica
          "M", //Medium
          "RF", //Recycle Fluting
          "F", //Fluting
          "W", //Wellenstoff
          "KP", //Kraft+Plastic
          "KPA", //Kraft+Plastic+Aluminum
          "KPw", //Kraft+Plastic+White
          "X1", //Custumer 1
          "X2" //Customer 2
        };
      else
        return new object[] 
      {
          "K", //Kraft
          "Kw", //Kraft Bianco
          "L", //Liner
          "Lw", //Liner Bianco
          "T", //Test liner
          "Tw", //Test liner Bianco
          "C", //Camoscio
          "S", //Semichimica
          "Us", //Uso Semichimica
          "M", //Medium
          "RF", //Recycle Fluting
          "F", //Fluting
          "W", //Wellenstoff
          "KP", //Kraft+Plastic
          "KPA", //Kraft+Plastic+Aluminum
          "KPw", //Kraft+Plastic+White
          "X1", //Custumer 1
          "X2" //Customer 2
      };
    }

    public bool deletePRCQualityOverride(string tableName, string qualityId)
    {
      bool res = false;

      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_" + tableName;
      res = SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + " WHERE qualityid = '" + qualityId + "';", true);

      return res;
    }

    public bool insertPRCQualityOverride(string tableName, string qualityId, System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> appo)
    {
      bool res = true;
      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_" + tableName;

      for (int i = 0; i < appo.Count; i++)
      {
        res = res & SyncroC.DataBase.SqlManager.getSqlManager().doCommand(
          String.Format("INSERT INTO {0} (qualityid,speed_uvm_m,value) VALUES ('{1}',{2},{3});", myTableName, SyncroC.DataBase.SqlManager.removeEscapeString(qualityId), appo[i].First, appo[i].Second), true);
      }

      return res;
    }

    public System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> getPRCQuality(string tableName, string qualityId, out bool isOverride, out string resQuery)
    {
      return this.getPRCQuality(false, tableName, qualityId, out isOverride, out resQuery);
    }

    public DataTable getWarpQuality(string warpName, string qualityId, out bool isOverride, out string resQuery)
    {
      DataTable dt = null;
      resQuery = "";

      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_" + warpName;
      dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT regulatedcode,sequence,maxvaluechange FROM " + myTableName + " WHERE (qualityid='" + qualityId + "') ORDER BY sequence,regulatedcode ASC;");

      if ((dt != null) && (dt.Rows.Count != 0))
      {
        isOverride = true;
        resQuery = qualityId;
        return dt;
      }
      else
      {
        isOverride = false;

        myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + warpName;
        string fluteId = this.getFluteForComponent(warpName, qualityId);
        string paperTreshold = this.getPaperTreshold(this.getPaperWeightForComponent(warpName, qualityId));

        string where = "";
        string notInRegulatedcode = "";
        string notInSequence = "";
        resQuery = "";

        for (int i = 0; i < 4; i++)
        {
          switch (i)
          {
            default:
            case 0:
              where = "(fluteid='" + fluteId + "' AND papertype='" + paperTreshold + "')";
              dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT regulatedcode,sequence,maxvaluechange FROM " + myTableName + " WHERE " + where + " ORDER BY sequence,regulatedcode ASC");
              notInRegulatedcode = "";
              notInSequence = "";
              foreach (DataRow dr in dt.Rows)
              {
                notInRegulatedcode += "'" + (string)dr["regulatedcode"] + "',";
                notInSequence += (int)dr["sequence"] + ",";
              }
              if (notInRegulatedcode != "")
              {
                notInRegulatedcode = notInRegulatedcode.Substring(0, notInRegulatedcode.LastIndexOf(','));
                notInSequence = notInSequence.Substring(0, notInSequence.LastIndexOf(','));
                resQuery = fluteId + " - " + paperTreshold;
              }
              break;

            case 1:
              if (notInRegulatedcode != "")
                where = "(fluteid='" + fluteId + "' AND papertype='*' AND (regulatedcode NOT IN(" + notInRegulatedcode + ") OR sequence NOT IN(" + notInSequence + ")) )";
              else
                where = "(fluteid='" + fluteId + "' AND papertype='*')";
              dt.Merge(SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT regulatedcode,sequence,maxvaluechange FROM " + myTableName + " WHERE " + where + " ORDER BY sequence,regulatedcode ASC"));
              notInRegulatedcode = "";
              notInSequence = "";
              foreach (DataRow dr in dt.Rows)
              {
                notInRegulatedcode += "'" + (string)dr["regulatedcode"] + "',";
                notInSequence += (int)dr["sequence"] + ",";
              }
              if (notInRegulatedcode != "")
              {
                notInRegulatedcode = notInRegulatedcode.Substring(0, notInRegulatedcode.LastIndexOf(','));
                notInSequence = notInSequence.Substring(0, notInSequence.LastIndexOf(','));
                if (resQuery == "")
                  resQuery = fluteId + " - *";
              }
              break;

            case 2:
              if (notInRegulatedcode != "")
                where = "(fluteid='*' AND papertype='" + paperTreshold + "' AND (regulatedcode NOT IN(" + notInRegulatedcode + ") OR sequence NOT IN(" + notInSequence + ")) )";
              else
                where = "(fluteid='*' AND papertype='" + paperTreshold + "')";
              
              dt.Merge(SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT regulatedcode,sequence,maxvaluechange FROM " + myTableName + " WHERE " + where + " ORDER BY sequence,regulatedcode ASC"));
              notInRegulatedcode = "";
              notInSequence = "";
              foreach (DataRow dr in dt.Rows)
              {
                notInRegulatedcode += "'" + (string)dr["regulatedcode"] + "',";
                notInSequence += (int)dr["sequence"] + ",";
              }
              if (notInRegulatedcode != "")
              {
                notInRegulatedcode = notInRegulatedcode.Substring(0, notInRegulatedcode.LastIndexOf(','));
                notInSequence = notInSequence.Substring(0, notInSequence.LastIndexOf(','));
                if (resQuery == "")
                  resQuery = "* - " + paperTreshold;
              }
              break;

            case 3:
              if (notInRegulatedcode != "")
                where = "(fluteid='*' AND papertype='*' AND (regulatedcode NOT IN(" + notInRegulatedcode + ") OR sequence NOT IN(" + notInSequence + ")) )";
              else
                where = "(fluteid='*' AND papertype='*')";
              if (resQuery == "")
                resQuery = "* - *";
              dt.Merge(SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT regulatedcode,sequence,maxvaluechange FROM " + myTableName + " WHERE " + where + " ORDER BY sequence,regulatedcode ASC"));
              break;
          }
        }

        if ((dt != null) && (dt.Rows.Count != 0))
        {
          return dt;
        }
        else
          resQuery = "???";
      }

      return dt;
    }

    public System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> getPRCQuality(bool forzaOndaMadre, string tableName, string qualityId, out bool isOverride, out string resQuery)
    {
      System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> appo = new System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>();

      DataTable dt = null;

      if (!forzaOndaMadre)
      {
        string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_" + tableName;
        dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT speed_uvm_m,value FROM " + myTableName + " WHERE qualityid = '" + qualityId + "' ORDER BY speed_uvm_m ASC;");
      }

      resQuery = "";

      if ((dt != null) && (dt.Rows.Count != 0))
      {
        foreach (DataRow a in dt.Rows)
        {
          appo.Add(new SyncroC.Graph.Pair<int, int>(
            (int)a["speed_uvm_m"],
            (int)a["value"]
            ));
        }

        isOverride = true;
        resQuery = qualityId;
      }
      else
      {
        //NON CI SONO OVVERRIDE 
        //ALLORA SI CERCA NELLE CURVE STANDARD
        string paperClassification = this.getPaperClassificationForComponent(tableName, qualityId);
        int paperWeight_UWg_m = this.getPaperWeightForComponent(tableName, qualityId);
        string fluteId = this.getFluteForComponent(tableName, qualityId);

        if (paperWeight_UWg_m >= 0)
          appo = this.getCurveForComponent(tableName, fluteId, paperWeight_UWg_m, paperClassification, out resQuery);

        isOverride = false;
      }

      return appo;
    }

    public string[] getAllPaperTreshold()
    {
      string[] res = new string[5];

      res[0] = "VL";
      res[1] = "L";
      res[2] = "M";
      res[3] = "H";
      res[4] = "VH";

      return res;
    }

    public void getPaperTreshold(string name, out int minLimit, out int maxLimit)
    {
      minLimit = -1;
      maxLimit = Int32.MaxValue;

      if (name == "VL")
      {
        minLimit = -1;
        maxLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdVeryLight_UWg;
      }
      else if (name == "L")
      {
        minLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdVeryLight_UWg;
        maxLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdLight_UWg;
      }
      else if (name == "M")
      {
        minLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdLight_UWg;
        maxLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdMedium_UWg;
      }
      else if (name == "H")
      {
        minLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdMedium_UWg;
        maxLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdHeavy_UWg;
      }
      else if (name == "VH")
      {
        minLimit = DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdHeavy_UWg;
        maxLimit = Int32.MaxValue;
      }
    }

    public string getPaperTreshold(int paperWeight_UWg_m)
    {
      string PaperTresholdToSearch = "";

      if (paperWeight_UWg_m <= DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdVeryLight_UWg)
      {
        PaperTresholdToSearch = "VL";
      }
      else if ((paperWeight_UWg_m > DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdVeryLight_UWg) && (paperWeight_UWg_m <= DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdLight_UWg))
      {
        PaperTresholdToSearch = "L";
      }
      else if ((paperWeight_UWg_m > DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdLight_UWg) && (paperWeight_UWg_m <= DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdMedium_UWg))
      {
        PaperTresholdToSearch = "M";
      }
      else if ((paperWeight_UWg_m > DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdMedium_UWg) && (paperWeight_UWg_m <= DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdHeavy_UWg))
      {
        PaperTresholdToSearch = "H";
      }
      else if ((paperWeight_UWg_m > DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdHeavy_UWg) )
      {
        PaperTresholdToSearch = "VH";
      }

      return PaperTresholdToSearch;
    }

    private System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> getCurveForComponent(string tableName, string fluteId, int paperWeight_UWg_m, string paperClassification, out string resQuery)
    {
      System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> appo = new System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>();

      string PaperTresholdToSearch = this.getPaperTreshold(paperWeight_UWg_m);
      
      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + tableName;
      
      string paperTypeSelect = "";
      string fluteIdSelect = "";
      string paperClassificationSelect = "";

      resQuery = "";

      List<string> paperClassificationList = new List<string>();
      string[] paperClassificationSplit = paperClassification.Split(new Char[] {'-'}, StringSplitOptions.None);
      if (paperClassificationSplit.Length == 2)
      {
        if ((paperClassificationSplit[0] != "") && (paperClassificationSplit[1] != ""))
          paperClassificationList.Add(paperClassification);
        if(paperClassificationSplit[0] != "")
          paperClassificationList.Add(paperClassificationSplit[0] + "-*");
        if (paperClassificationSplit[1] != "")
          paperClassificationList.Add("*-" + paperClassificationSplit[1]);
        paperClassificationList.Add("*-*");
      }
      else 
      {
        if (paperClassification != "")
          paperClassificationList.Add(paperClassification);
        paperClassificationList.Add("*");
      }

      DataTable dt = null;

      for (int i = 0; i < 4; i++)
      {
        resQuery = "";

        //RFAZZI_FI18/11/2010_3. Cambiata sequenza delle query su PostGres per il Process sia lato client che server per seguire la sequenza come illustrato dall'albero visualizzato nella pagina di impostazioni delle curve base per il PRC. 
        switch (i)
        {
          default:
          case 0:
            paperTypeSelect = "(papertype='" + PaperTresholdToSearch + "')";
            fluteIdSelect = "(fluteId = '" + fluteId + "')";
            resQuery += fluteId + " - " + PaperTresholdToSearch;
            break;
          case 1:
            paperTypeSelect = "((papertype='" + PaperTresholdToSearch + "') OR (papertype='*'))";
            fluteIdSelect = "(fluteId = '" + fluteId + "')";
            resQuery += fluteId + " - *";
            break;
          case 2:
            paperTypeSelect = "(papertype='" + PaperTresholdToSearch + "')";
            fluteIdSelect = "((fluteId = '" + fluteId + "') OR (fluteId='*'))";
            resQuery += "* - " + PaperTresholdToSearch;
            break;
          case 3:
            paperTypeSelect = "((papertype='" + PaperTresholdToSearch + "') OR (papertype='*'))";
            fluteIdSelect = "((fluteId = '" + fluteId + "') OR (fluteId='*'))";
            resQuery += "* - *";
            break;
        }

        foreach (string a in paperClassificationList)
        {
          paperClassificationSelect = "(paperclassification = '" + a + "')";

          dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT speed_uvm_m,value FROM " + myTableName + " WHERE (" + fluteIdSelect + " AND " + paperTypeSelect + " AND " + paperClassificationSelect + ") ORDER BY speed_uvm_m ASC;");
          if (dt != null)
            if (dt.Rows.Count != 0)
            {
              resQuery += " - " + a;
              break;
            }
        }

        if (dt != null)
          if (dt.Rows.Count != 0)
            break;
      }

      if ((dt != null) && (dt.Rows.Count != 0))
      {
        foreach (DataRow a in dt.Rows)
        {
          appo.Add(new SyncroC.Graph.Pair<int, int>(
            (int)a["speed_uvm_m"],
            (int)a["value"]
            ));
        }
      }
      else
      {
        resQuery = "???";
        // AZZ... BIG Problem non ho trovato nulla 
        appo.Add(new SyncroC.Graph.Pair<int, int>(0,0));
      }

      return appo;
    }

    public string getFluteAndPaperWeightAndPaperClassification(string component, string qualityId)
    {
      string res = "";

      if (component.StartsWith("warp"))
        res = this.getFluteForComponent(component, qualityId) + " - " + this.getPaperTreshold(this.getPaperWeightForComponent(component, qualityId));
      else
        res = this.getFluteForComponent(component, qualityId) + " - " + this.getPaperTreshold(this.getPaperWeightForComponent(component, qualityId)) + " - " + this.getPaperClassificationForComponent(component, qualityId);

      return res;
    }

    private string getFluteForComponent(string component, string qualityId)
    {
      string completefluteId = "";
      string fluteId = "";
      string SFPositionFluteId = "";

      DataRow drQuality = SyncroC.DBManager.getDBManager().quality.getRow(qualityId);
      if (drQuality != null)
      {
        completefluteId = drQuality[Quality.FLUTE_ID].ToString();
        DataRow drFlute = SyncroC.DBManager.getDBManager().flute.getRow(completefluteId);
        if (drFlute != null)
        {
          //SF1,SF2,SF3 (es BC-)
          SFPositionFluteId = drFlute[Flute.SFS_POSITION].ToString();
        }
      }

      switch (component)
      {
        case "warp_updown":
        case "warp_endtoend":
        case "papertension_1":
        case "papertension_21":
        case "papertension_22":
        case "papertension_23":
        case "wraparm_11":
        case "wraparm_31":
        case "wraparm_41":
        case "wraparm_1":
        case "wraparm_10":
        case "wraparm_2":
        case "wraparm_20":
        case "steam_group_1":
        case "steam_group_2":
        case "steam_group_3":
        case "steam_group_0":
        case "steamshower_4":
        case "gluegap_1":
        case "gluegap_2":
        case "pressure_hot_section_1":
        case "pressure_hot_section_2":
        case "pressure_hot_section_3":
        case "pressure_hot_section_4":
        case "pressure_cold_section_1":

        //RFAZZI_FI16/06/2010_1. Aggiunta possibilità di usare l'onda completa per il controllo da PRC del GlueGap e Fasciature ai SF
        case "gluegap_11":
        case "wraparm_12":
        case "wraparm_22":
        case "wraparm_13":
        case "wraparm_23":

        case "gluegap_21":
        case "wraparm_14":
        case "wraparm_24":
        case "wraparm_15":
        case "wraparm_25":

          fluteId = completefluteId;
          break;

        case "papertension_2":
        case "papertension_3":
        //case "gluegap_11":
        //case "wraparm_12":
        //case "wraparm_22":
        //case "wraparm_13":
        //case "wraparm_23":
				case "steamshower_1":
        case "sfmainsteam_1":
          fluteId = SFPositionFluteId.ToCharArray()[0].ToString();
          if (fluteId == "-")
            fluteId = SFPositionFluteId.ToCharArray()[1].ToString();
          break;

        case "papertension_4":
        case "papertension_5":
        //case "gluegap_21":
        //case "wraparm_14":
        //case "wraparm_24":
        //case "wraparm_15":
        //case "wraparm_25":
				case "steamshower_2":
        case "sfmainsteam_2":
					fluteId = SFPositionFluteId.ToCharArray()[1].ToString();
          if (fluteId == "-")
            fluteId = SFPositionFluteId.ToCharArray()[0].ToString();
          break;

        default:
          fluteId = "";
          break;
      }

      return fluteId;
    }

    private string getPaperClassificationForComponent(string component, string qualityId)
    {
      string paperClassification = "";

      DataRow dr = SyncroC.DBManager.getDBManager().quality.getRow(qualityId);
      if (dr != null)
      {

        List<string> PAPER_ID_N = new List<string>();
        List<string> WEIGHT_FACTOR_N = new List<string>();

        switch (component)
        {
          case "warp_updown":
          case "warp_endtoend":
          case "steam_group_1":
          case "steam_group_2":
          case "steam_group_3":
          case "steam_group_0":
          case "steamshower_4":
          case "pressure_hot_section_1":
          case "pressure_hot_section_2":
          case "pressure_hot_section_3":
          case "pressure_hot_section_4":
          case "pressure_cold_section_1":
            PAPER_ID_N.Add(Quality.PAPER_ID_0);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
            //PAPER_ID_N.Add(Quality.PAPER_ID_1);
            //WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            //PAPER_ID_N.Add(Quality.PAPER_ID_2);
            //WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            //if ((dr[Quality.PAPER_ID_3].ToString() != "") && (dr[Quality.PAPER_ID_3].ToString() != "---"))
            //{
            //  //è un Onda doppia AB
            //  PAPER_ID_N.Add(Quality.PAPER_ID_3);
            //  WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
            //  PAPER_ID_N.Add(Quality.PAPER_ID_4);
            //  WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_4);
            //}
            break;

          case "wraparm_11":
          case "wraparm_31":
          case "wraparm_41":
            PAPER_ID_N.Add(Quality.PAPER_ID_0);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
            break;

          case "papertension_1":
          case "gluegap_1":
            PAPER_ID_N.Add(Quality.PAPER_ID_0);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            break;

          case "wraparm_1":
          case "wraparm_10":
          case "gluegap_11":
          case "papertension_21":
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            PAPER_ID_N.Add(Quality.PAPER_ID_2);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            break;

          case "papertension_2":
          case "wraparm_12":
          case "wraparm_22":
          case "steamshower_1":
          case "sfmainsteam_1":
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            break;

          case "papertension_3":
          case "wraparm_13":
          case "wraparm_23":
            PAPER_ID_N.Add(Quality.PAPER_ID_2);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            break;

          case "gluegap_2":
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_2);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_0);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
              PAPER_ID_N.Add(Quality.PAPER_ID_1);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            }
            break;

          case "wraparm_2":
          case "wraparm_20":
          case "gluegap_21":
          case "papertension_22":

            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
              PAPER_ID_N.Add(Quality.PAPER_ID_4);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_4);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_1);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
              PAPER_ID_N.Add(Quality.PAPER_ID_2);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            }
            break;

          case "papertension_4":
          case "wraparm_24":
          case "wraparm_14":
          case "steamshower_2":
          case "sfmainsteam_2":
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_1);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            }
            break;

          case "papertension_5":
          case "wraparm_25":
          case "wraparm_15":
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_4);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_4);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_2);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            }
            break;

          default:
            PAPER_ID_N.Clear();
            WEIGHT_FACTOR_N.Clear();
            break;
        }

        if ((PAPER_ID_N.Count != 0) && (WEIGHT_FACTOR_N.Count != 0))
        {
          for (int i = 0; i < WEIGHT_FACTOR_N.Count; i++)
          {
            DataRow drPaper = SyncroC.DBManager.getDBManager().paper.getRow(dr[PAPER_ID_N[i]].ToString(), SyncroC.Utility.SyncroConverter.convertToInt(dr[WEIGHT_FACTOR_N[i]].ToString(), 0));
            paperClassification += drPaper[Paper.PAPER_CLASSIFICATION].ToString();
            if (i < WEIGHT_FACTOR_N.Count - 1)
              paperClassification += "-";
          }
        }
        else
        {
          paperClassification = "";
        }
      }
      return paperClassification;
    }

    private int getPaperWeightForComponent(string component, string qualityId)
    {
      string paperId = "";
      int paperWeight_UWg_m = 0;
      bool doAverageWeight = false;

      DataRow dr = SyncroC.DBManager.getDBManager().quality.getRow(qualityId);
      if (dr != null)
      {

        List<string> PAPER_ID_N = new List<string>();
        List<string> WEIGHT_FACTOR_N = new List<string>();

        switch (component)
        {
          case "warp_updown":
          case "warp_endtoend":
          case "steam_group_1":
          case "steam_group_2":
          case "steam_group_3":
          case "steam_group_0":
          case "steamshower_4":
          case "pressure_hot_section_1":
          case "pressure_hot_section_2":
          case "pressure_hot_section_3":
          case "pressure_hot_section_4":
          case "pressure_cold_section_1":
            doAverageWeight = true;
            PAPER_ID_N.Add(Quality.PAPER_ID_0);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            PAPER_ID_N.Add(Quality.PAPER_ID_2);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            //RFAZZI_FI18_11_2010_1. Fatta Trim sull'ID della carta perchè esistono qualità con inserite carte con ID uguale a "spazio"
            //if ((dr[Quality.PAPER_ID_3].ToString() != "") && (dr[Quality.PAPER_ID_3].ToString() != "---"))
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
              PAPER_ID_N.Add(Quality.PAPER_ID_4);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_4);
            }
            break;

          case "wraparm_11":
          case "wraparm_31":
          case "wraparm_41":
            PAPER_ID_N.Add(Quality.PAPER_ID_0);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
            break;

          case "papertension_1":
          case "gluegap_1":
            doAverageWeight = true;
            PAPER_ID_N.Add(Quality.PAPER_ID_0);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            break;

          case "wraparm_1":
          case "wraparm_10":
          case "gluegap_11":
          case "papertension_21":

            doAverageWeight = true;
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            PAPER_ID_N.Add(Quality.PAPER_ID_2);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            break;

          case "papertension_2":
          case "wraparm_12":
          case "wraparm_22":
					case "steamshower_1":
          case "sfmainsteam_1":
            PAPER_ID_N.Add(Quality.PAPER_ID_1);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            break;

          case "papertension_3":
          case "wraparm_13":
          case "wraparm_23":
            PAPER_ID_N.Add(Quality.PAPER_ID_2);
            WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            break;

          case "gluegap_2":
            doAverageWeight = true;
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_2);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_0);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_0);
              PAPER_ID_N.Add(Quality.PAPER_ID_1);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            }
            break;

          case "wraparm_2":
          case "wraparm_20":
          case "gluegap_21":
          case "papertension_22":

            doAverageWeight = true;
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
              PAPER_ID_N.Add(Quality.PAPER_ID_4);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_4);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_1);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
              PAPER_ID_N.Add(Quality.PAPER_ID_2);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            }
            break;

          case "papertension_4":
          case "wraparm_24":
          case "wraparm_14":
					case "steamshower_2":
          case "sfmainsteam_2":
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_3);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_3);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_1);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_1);
            }
            break;

          case "papertension_5":
          case "wraparm_25":
          case "wraparm_15":
            if ((dr[Quality.PAPER_ID_3].ToString().Trim() != "") && (dr[Quality.PAPER_ID_3].ToString().Trim() != "---"))
            {
              //è un Onda doppia AB
              PAPER_ID_N.Add(Quality.PAPER_ID_4);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_4);
            }
            else
            {
              //è un onda singola fatta però sul single facer 2
              PAPER_ID_N.Add(Quality.PAPER_ID_2);
              WEIGHT_FACTOR_N.Add(Quality.WEIGHT_FACTOR_2);
            }
            break;

          default:
            PAPER_ID_N.Clear();
            WEIGHT_FACTOR_N.Clear();         
            break;
        }

        if ((PAPER_ID_N.Count != 0) && (WEIGHT_FACTOR_N.Count != 0))
        {
          int WEIGHT_FACTOR_N_Count = WEIGHT_FACTOR_N.Count;
          //ABELLO aggiunta gestione per il canete
          for (int i = 0; i < WEIGHT_FACTOR_N.Count; i++)
          {
            paperId += dr[PAPER_ID_N[i]].ToString() + "|";
            paperWeight_UWg_m += SyncroC.Utility.SyncroConverter.convertToInt(dr[WEIGHT_FACTOR_N[i]].ToString(), 0);

            if (dr[PAPER_ID_N[i]].ToString() == "NOT_USED")
              WEIGHT_FACTOR_N_Count--;
          }
          if (doAverageWeight)
            paperWeight_UWg_m = (int)Math.Round((double)(paperWeight_UWg_m / WEIGHT_FACTOR_N_Count), 0);
        }
        else
        {
          paperId = "";
          paperWeight_UWg_m = -1;
        }
      }
  
      return paperWeight_UWg_m;
    }

    public object[] generateNameForAllComponentArray(bool warpUpDown)
    {
      List<PRCComponentPair> res = new List<PRCComponentPair>();

      if (warpUpDown && DBManager.getDBManager().lineConfig.DoubleBacker)
      {
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamShower.Present)
          res.Add(new PRCComponentPair("steamshower_4", this.getSimpleNameForComponent(true, "steamshower_4")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.Present)
          res.Add(new PRCComponentPair("wraparm_31", this.getSimpleNameForComponent(true, "wraparm_31")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.Present)
          res.Add(new PRCComponentPair("wraparm_41", this.getSimpleNameForComponent(true, "wraparm_41")));

        //Steam Sections
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup1.Present)
          res.Add(new PRCComponentPair("steam_group_1", this.getSimpleNameForComponent(true, "steam_group_1")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup2.Present)
          res.Add(new PRCComponentPair("steam_group_2", this.getSimpleNameForComponent(true, "steam_group_2")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup3.Present)
          res.Add(new PRCComponentPair("steam_group_3", this.getSimpleNameForComponent(true, "steam_group_3")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup0.Present)
          res.Add(new PRCComponentPair("steam_group_0", this.getSimpleNameForComponent(true, "steam_group_0")));

        //Screw Jack Sections
        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection1.Present)
          res.Add(new PRCComponentPair("pressure_hot_section_1", this.getSimpleNameForComponent(true, "pressure_hot_section_1")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection2.Present)
          res.Add(new PRCComponentPair("pressure_hot_section_2", this.getSimpleNameForComponent(true, "pressure_hot_section_2")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection3.Present)
          res.Add(new PRCComponentPair("pressure_hot_section_3", this.getSimpleNameForComponent(true, "pressure_hot_section_3")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection4.Present)
          res.Add(new PRCComponentPair("pressure_hot_section_4", this.getSimpleNameForComponent(true, "pressure_hot_section_4")));

        if (DBManager.getDBManager().lineConfig.DoubleBackerConfig.ColdSection1.Present)
          res.Add(new PRCComponentPair("pressure_cold_section_1", this.getSimpleNameForComponent(true, "pressure_cold_section_1")));
      }

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Splicer1 && DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_1", this.getSimpleNameForComponent(true, "papertension_1")));

      if (warpUpDown && DBManager.getDBManager().lineConfig.GlueUnit)
      {
        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.Present)
          res.Add(new PRCComponentPair("gluegap_1", this.getSimpleNameForComponent(true, "gluegap_1")));

        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.Present)
          res.Add(new PRCComponentPair("gluegap_2", this.getSimpleNameForComponent(true, "gluegap_2")));

        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG3.Present)
          res.Add(new PRCComponentPair("gluegap_3", this.getSimpleNameForComponent(true, "gluegap_3")));

        if (DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG4.Present)
          res.Add(new PRCComponentPair("gluegap_4", this.getSimpleNameForComponent(true, "gluegap_4")));
      }

      if (warpUpDown && DBManager.getDBManager().lineConfig.PreHeatersTern)
      {
        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.Present)
          res.Add(new PRCComponentPair("wraparm_11", this.getSimpleNameForComponent(true, "wraparm_11")));

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.Present)
          res.Add(new PRCComponentPair("wraparm_1", this.getSimpleNameForComponent(true, "wraparm_1")));

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.Present)
          res.Add(new PRCComponentPair("wraparm_10", this.getSimpleNameForComponent(true, "wraparm_10")));

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.Present)
          res.Add(new PRCComponentPair("wraparm_2", this.getSimpleNameForComponent(true, "wraparm_2")));

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.Present)
          res.Add(new PRCComponentPair("wraparm_20", this.getSimpleNameForComponent(true, "wraparm_20")));

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater3.Present)
          res.Add(new PRCComponentPair("wraparm_3", this.getSimpleNameForComponent(true, "wraparm_3")));

        if (DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater30.Present)
          res.Add(new PRCComponentPair("wraparm_30", this.getSimpleNameForComponent(true, "wraparm_30")));
      }

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Splicer2 && DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_2", this.getSimpleNameForComponent(true, "papertension_2")));

      if (warpUpDown && DBManager.getDBManager().lineConfig.SingleFacer1)
      {
        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIl.Present)
          res.Add(new PRCComponentPair("wraparm_23", this.getSimpleNameForComponent(true, "wraparm_23")));

        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterIm.Present)
          res.Add(new PRCComponentPair("wraparm_22", this.getSimpleNameForComponent(true, "wraparm_22")));

        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.PreheaterEl.Present)
          res.Add(new PRCComponentPair("wraparm_13", this.getSimpleNameForComponent(true, "wraparm_13")));

        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.Gluegap1.Present)
          res.Add(new PRCComponentPair("gluegap_11", this.getSimpleNameForComponent(true, "gluegap_11")));

        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SteamShower.Present)
          res.Add(new PRCComponentPair("steamshower_1", this.getSimpleNameForComponent(true, "steamshower_1")));

        if (DBManager.getDBManager().lineConfig.SingleFacer1Config.SFMainSteam.Present)
          res.Add(new PRCComponentPair("sfmainsteam_1", this.getSimpleNameForComponent(true, "sfmainsteam_1")));
      }

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Bridge1 && DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_21", this.getSimpleNameForComponent(true, "papertension_21")));

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Splicer3 && DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_3", this.getSimpleNameForComponent(true, "papertension_3")));

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Splicer4 && DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_4", this.getSimpleNameForComponent(true, "papertension_4")));

      if (warpUpDown && DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIl.Present)
          res.Add(new PRCComponentPair("wraparm_25", this.getSimpleNameForComponent(true, "wraparm_25")));

        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterIm.Present)
          res.Add(new PRCComponentPair("wraparm_24", this.getSimpleNameForComponent(true, "wraparm_24")));

        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.PreheaterEl.Present)
          res.Add(new PRCComponentPair("wraparm_15", this.getSimpleNameForComponent(true, "wraparm_15")));

        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.Gluegap1.Present)
          res.Add(new PRCComponentPair("gluegap_21", this.getSimpleNameForComponent(true, "gluegap_21")));

        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SteamShower.Present)
          res.Add(new PRCComponentPair("steamshower_2", this.getSimpleNameForComponent(true, "steamshower_2")));

        if (DBManager.getDBManager().lineConfig.SingleFacer2Config.SFMainSteam.Present)
          res.Add(new PRCComponentPair("sfmainsteam_2", this.getSimpleNameForComponent(true, "sfmainsteam_2")));
      }

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Bridge2 && DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_22", this.getSimpleNameForComponent(true, "papertension_22")));

      if (!warpUpDown && DBManager.getDBManager().lineConfig.Splicer5 && DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.Present)
        res.Add(new PRCComponentPair("papertension_5", this.getSimpleNameForComponent(true, "papertension_5")));

      return res.ToArray();
    }

    public string getSimpleNameForComponent(object component)
    {
      return this.getSimpleNameForComponent(true, component);
    }

    public string getSimpleNameForComponent(bool withMachineName, object component)
    {
      string strComponent = Convert.ToString(component);
      string res = "";

      switch (strComponent)
      {
        case "warp_updown":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_15");
          break;
        case "warp_endtoend":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_16");
          break;

        case "steam_group_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_04") + " 1";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "steam_group_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_04") + " 2";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "steam_group_3":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_04") + " 3";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "steam_group_0":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_04") + " 0";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "pressure_hot_section_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_05") + " 1";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "pressure_hot_section_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_05") + " 2";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "pressure_hot_section_3":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_05") + " 3";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "pressure_hot_section_4":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_05") + " 4";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "pressure_cold_section_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_06") + " 1";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "papertension_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "SPL1 " + res;
          break;
        case "papertension_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "SPL2 " + res;
          break;
        case "papertension_3":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "SPL3 " + res;
          break;
        case "papertension_4":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "SPL4 " + res;
          break;
        case "papertension_5":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "SPL5 " + res;
          break;
        case "papertension_21":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "BR1 " + res;
          break;
        case "papertension_22":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "BR2 " + res;
          break;
        case "papertension_23":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_07");
          if (withMachineName)
            res = "BR3 " + res;
          break;
        case "gluegap_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_08") + " 1";
          if (withMachineName)
            res = "GU " + res;
          break;
        case "gluegap_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_08") + " 2";
          if (withMachineName)
            res = "GU " + res;
          break;
        case "gluegap_11":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_08");
          if (withMachineName)
            res = "SF1 " + res;
          break;
        case "gluegap_21":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_08");
          if (withMachineName)
            res = "SF2 " + res;
          break;
        case "steamshower_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_09");
          if (withMachineName)
            res = "SF1 " + res;
          break;
        case "steamshower_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_09");
          if (withMachineName)
            res = "SF2 " + res;
          break;
        case "steamshower_3":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_09");
          if (withMachineName)
            res = "SF3 " + res;
          break;
        case "steamshower_4":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_09");
          if (withMachineName)
            res = "DB " + res;
          break;
        case "sfmainsteam_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_04");
          if (withMachineName)
            res = "SF1 " + res;
          break;
        case "sfmainsteam_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_04");
          if (withMachineName)
            res = "SF2 " + res;
          break;

        case "wraparm_31":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 31";
          if (withMachineName)
            res = "DB " + res;
          break;
        case "wraparm_41":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 41";
          if (withMachineName)
            res = "DB " + res;
          break;

        case "wraparm_11":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 11";
          if (withMachineName)
            res = "PHT " + res;
          break;
        case "wraparm_1":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 1";
          if (withMachineName)
            res = "PHT " + res;
          break;
        case "wraparm_10":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 10";
          if (withMachineName)
            res = "PHT " + res;
          break;
        case "wraparm_2":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 2";
          if (withMachineName)
            res = "PHT " + res;
          break;
        case "wraparm_20":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " 20";
          if (withMachineName)
            res = "PHT " + res;
          break;

        case "wraparm_12":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " Em";
          if (withMachineName)
            res = "SF1 " + res;
          break;
        case "wraparm_22":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " Im";
          if (withMachineName)
            res = "SF1 " + res;
          break;
        case "wraparm_13":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " El";
          if (withMachineName)
            res = "SF1 " + res;
          break;
        case "wraparm_23":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " Il";
          if (withMachineName)
            res = "SF1 " + res;
          break;

        case "wraparm_24":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " Im";
          if (withMachineName)
            res = "SF2 " + res;
          break;
        case "wraparm_14":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " Em";
          if (withMachineName)
            res = "SF2 " + res;
          break;
        case "wraparm_25":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " Il";
          if (withMachineName)
            res = "SF2 " + res;
          break;
        case "wraparm_15":
          res = DBManager.getDBManager().message.getTranslatedMessage("§PRC_10") + " El";
          if (withMachineName)
            res = "SF2 " + res;
          break;
      }

      return res;
    }

    public int getMinOrMaxValueForComponent(bool getMin, string strComponent)
    {
      int res = 0;

      switch (strComponent)
      {
        case "steam_group_1":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup1.DBSteamGroupRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup1.DBSteamGroupRegulation.MaxVal;
          break;
        case "steam_group_2":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup2.DBSteamGroupRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup2.DBSteamGroupRegulation.MaxVal;
          break;
        case "steam_group_3":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup3.DBSteamGroupRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup3.DBSteamGroupRegulation.MaxVal;
          break;
        case "steam_group_0":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup0.DBSteamGroupRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamGroup0.DBSteamGroupRegulation.MaxVal;
          break;
        case "pressure_hot_section_1":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection1.DBHotSectionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection1.DBHotSectionRegulation.MaxVal;
          break;
        case "pressure_hot_section_2":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection2.DBHotSectionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection2.DBHotSectionRegulation.MaxVal;
          break;
        case "pressure_hot_section_3":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection3.DBHotSectionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection3.DBHotSectionRegulation.MaxVal;
          break;
        case "pressure_hot_section_4":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection4.DBHotSectionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.HotSection4.DBHotSectionRegulation.MaxVal;
          break;
        case "pressure_cold_section_1":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.ColdSection1.DBColdSectionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.ColdSection1.DBColdSectionRegulation.MaxVal;
          break;
        case "papertension_1":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;
        case "papertension_2":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;
        case "papertension_3":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;
        case "papertension_4":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;
        case "papertension_5":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;

        case "papertension_21":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.Bridge1Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;
        case "papertension_22":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.Bridge2Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;
        case "papertension_23":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.Bridge3Config.PaperTension.PaperTensionRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.Bridge3Config.PaperTension.PaperTensionRegulation.MaxVal;
          break;

        case "gluegap_1":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MaxVal;
          break;
        case "gluegap_2":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MaxVal;
          break;
        case "gluegap_11":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).Gluegap1.GlueGapRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).Gluegap1.GlueGapRegulation.MaxVal;
          break;
        case "gluegap_21":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).Gluegap1.GlueGapRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).Gluegap1.GlueGapRegulation.MaxVal;
          break;
        case "steamshower_4":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamShower.SteamShowerRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamShower.SteamShowerRegulation.MaxVal;
          break;
        case "steamshower_1":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer1Config.SteamShower.SteamShowerRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer1Config.SteamShower.SteamShowerRegulation.MaxVal;
          break;
        case "steamshower_2":
          if (getMin)
            res = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer2Config.SteamShower.SteamShowerRegulation.MinVal;
          else
            res = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer2Config.SteamShower.SteamShowerRegulation.MaxVal;
          break;
        case "sfmainsteam_1":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).SFMainSteam.SFMainSteamRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).SFMainSteam.SFMainSteamRegulation.MaxVal;
          break;
        case "sfmainsteam_2":
          if (getMin)
            res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).SFMainSteam.SFMainSteamRegulation.MinVal;
          else
            res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).SFMainSteam.SFMainSteamRegulation.MaxVal;
          break;

        case "wraparm_31":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_41":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.WrapArmRegulation.MaxVal;
          break;

        case "wraparm_11":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_1":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_10":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_2":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_20":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.WrapArmRegulation.MaxVal;
          break;

        case "wraparm_12":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEm.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEm.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_22":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIm.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIm.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_13":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEl.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEl.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_23":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIl.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIl.WrapArmRegulation.MaxVal;
          break;

        case "wraparm_14":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEm.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEm.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_24":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_15":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEl.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEl.WrapArmRegulation.MaxVal;
          break;
        case "wraparm_25":
          if (getMin)
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MinVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmRegulation.MinVal;
          else
            res = this.getTemperatureSensorPresentForWrapArm(strComponent) ? DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIl.WrapArmTemperatureConfig.WrapArmTemperatureRegulation.MaxVal : DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmRegulation.MaxVal;
          break;
      }

      return res;
    }

    public bool getTemperatureSensorPresentForWrapArm(string strComponent)
    {
      bool res = false;

      switch (strComponent)
      {
      case "wraparm_31":
          res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater31.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_41":
          res = DBManager.getDBManager().lineConfig.DoubleBackerConfig.Preheater41.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;

        case "wraparm_11":
          res = DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater11.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_1":
          res = DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater1.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_10":
          res = DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater10.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_2":
          res = DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater2.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_20":
          res = DBManager.getDBManager().lineConfig.PreHeatersTernConfig.Preheater20.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;

        case "wraparm_12":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEm.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_22":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIm.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_13":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterEl.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_23":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(1).PreheaterIl.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;

        case "wraparm_14":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEm.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_24":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIm.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_15":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterEl.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
        case "wraparm_25":
          res = DBManager.getDBManager().lineConfig.getSingleFacerNumber(2).PreheaterIl.WrapArmTemperatureConfig.TemperatureSensorPresent;
          break;
      }

      return res;
    }
  }

  public class PRCComponentPair
  {
    private string first;

    public string First
    {
      get { return first; }
      set { first = value; }
    }
    private string second;

    public string Second
    {
      get { return second; }
      set { second = value; }
    }

    public PRCComponentPair()
    {
    }

    public PRCComponentPair(string first, string second)
    {
      this.first = first;
      this.second = second;
    }
  };
}
