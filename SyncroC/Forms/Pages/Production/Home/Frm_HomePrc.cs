using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Forms.Pages.SingleFacer.Status;
using SyncroC.Forms.Pages.GlueUnit;
using SyncroC.Forms.Pages.PreHeaterTern;
using SyncroC.Forms.Pages.DoubleBacker.Status;
using SyncroC.Forms.Pages.Splicer;

namespace SyncroC.Forms.Pages.Production.Home
{
  public partial class Frm_HomePrc : SyncroC.Forms.Pages.Production.Home.Frm_HomeSubGroup
  {
    private float SF2_scaleFactor = 1.4F;
    private bool SF2_isZoomIn = false;
    private Size SF2_originalSize = new Size();
    private Point SF2_originalLocation = new Point();

    private float SF1_scaleFactor = 1.4F;
    private bool SF1_isZoomIn = false;
    private Size SF1_originalSize = new Size();
    private Point SF1_originalLocation = new Point();

    private float PHT_scaleFactor = 1.3F;
    private bool PHT_isZoomIn = false;
    private Size PHT_originalSize = new Size();
    private Point PHT_originalLocation = new Point();

    private float PRC_scaleFactor = 1.5F;
    private bool PRC_isZoomIn = false;
    private Size PRC_originalSize = new Size();
    private Point PRC_originalLocation = new Point();

    private float DB_scaleFactor = 1.1F;
    private bool DB_isZoomIn = false;
    private Size DB_originalSize = new Size();
    private Point DB_originalLocation = new Point();

    private bool showDebugOptions = false;

    private string currentQualityId = "";

    public Frm_HomePrc()
    {
      InitializeComponent();

      this.prc_wrapArmPreHeater31.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater11.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater1.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater2.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater20.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater13.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater22.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater23.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater14.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater15.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater24.setDesignModePage(this.DesignMode);
      this.prc_wrapArmPreHeater25.setDesignModePage(this.DesignMode);

      this.prc_paperTension5.setDesignModePage(this.DesignMode);
      this.prc_paperTension4.setDesignModePage(this.DesignMode);
      this.prc_paperTension3.setDesignModePage(this.DesignMode);
      this.prc_paperTension2.setDesignModePage(this.DesignMode);
      this.prc_paperTension1.setDesignModePage(this.DesignMode);
      this.prc_paperTension21.setDesignModePage(this.DesignMode);
      this.prc_paperTension22.setDesignModePage(this.DesignMode);

      this.prc_glueGap21.setDesignModePage(this.DesignMode);
      this.prc_glueGap11.setDesignModePage(this.DesignMode);
      this.prc_glueGap1.setDesignModePage(this.DesignMode);
      this.prc_glueGap2.setDesignModePage(this.DesignMode);

      this.prc_sfmainsteam2.setDesignModePage(this.DesignMode);
      this.prc_sfmainsteam1.setDesignModePage(this.DesignMode);

      this.prc_SteamShower4.setDesignModePage(this.DesignMode);
      this.prc_SteamShower1.setDesignModePage(this.DesignMode);
      this.prc_SteamShower2.setDesignModePage(this.DesignMode);

      this.prc_SteamSection1.setDesignModePage(this.DesignMode);
      this.prc_SteamSection2.setDesignModePage(this.DesignMode);
      this.prc_SteamSection3.setDesignModePage(this.DesignMode);
      this.prc_SteamSection0.setDesignModePage(this.DesignMode);

      this.prc_ShoesPressure1.setDesignModePage(this.DesignMode);
      this.prc_ShoesPressure2.setDesignModePage(this.DesignMode);
      this.prc_ShoesPressure3.setDesignModePage(this.DesignMode);
      this.prc_ShoesPressure4.setDesignModePage(this.DesignMode);

      this.pnl_ZoneSf2.BorderStyle = BorderStyle.FixedSingle;
      this.SF2_isZoomIn = false;
      this.SF2_originalSize = this.pnl_ZoneSf2.Size;
      this.SF2_originalLocation = this.pnl_ZoneSf2.Location;

      this.pnl_ZoneSf1.BorderStyle = BorderStyle.FixedSingle;
      this.SF1_isZoomIn = false;
      this.SF1_originalSize = this.pnl_ZoneSf1.Size;
      this.SF1_originalLocation = this.pnl_ZoneSf1.Location;

      this.pnl_ZonePht.BorderStyle = BorderStyle.FixedSingle;
      this.PHT_isZoomIn = false;
      this.PHT_originalSize = this.pnl_ZoneSf1.Size;
      this.PHT_originalLocation = this.pnl_ZoneSf1.Location;

      this.pnl_ZoneDB.BorderStyle = BorderStyle.FixedSingle;
      this.DB_isZoomIn = false;
      this.DB_originalSize = this.pnl_ZoneDB.Size;
      this.DB_originalLocation = this.pnl_ZoneDB.Location;

      this.pnl_PRC.SendToBack();
      this.pnl_PRC.BorderStyle = BorderStyle.FixedSingle;

      if (SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.CloseLoopUpDownTypeUsed > SyncroC.DataBase.LineConfig.HeaderPRC.CloseLoopUpDownType.NotPresent)
        this.warpUpDownAutoMan.Visible = true;
    }

    //questa pagina essendo molto complessa ci mette molto a caricarsi quindi è meglio non scaricarla mai.
    public override bool IsRemovableFromCache()
    {
      return false;
    }

    //ABello_FI10/11/2010_1 fatta l'inversione della pagina home PRC
    private bool corrugatorDirectionLeft = false;
    public override void CorrugatorDirectionLeft()
    {
      this.corrugatorDirectionLeft = true;

      int leftLimit = this.pnl_ZoneSf1.Location.X;
      int rightLimit = this.pnl_ZonePht.Location.X + this.pnl_ZonePht.Width;
      HorizontalLocationMirror(this.pnl_ZoneSf1, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pnl_ZoneSf2, leftLimit, rightLimit);
      HorizontalLocationMirror(this.pnl_ZonePht, leftLimit, rightLimit);

      this.pnl_ZoneSf1.RightToLeft = RightToLeft.Yes;
      this.pnl_ZoneSf2.RightToLeft = RightToLeft.Yes;
      this.pnl_ZonePht.RightToLeft = RightToLeft.Yes;
      this.pnl_ZoneDB.RightToLeft = RightToLeft.Yes;

      this.SF2_originalLocation = this.pnl_ZoneSf2.Location;
      this.SF1_originalLocation = this.pnl_ZoneSf1.Location;
      this.PHT_originalLocation = this.pnl_ZoneSf1.Location;

      //ABELLO_FI24/11/2010_1 [PRC] Aggiunta gestione a grafica dell'umidità sopra/sotto
      this.pnl_ZoneDB.SetColumn(this.lbl_MoistureSensor, 1); 
    }

    public override void refreshData()
    {
      this.configureMinAndMax();
    }

    private void configureMinAndMax()
    {
      bool existsAControlForCorrectUpDownWarp = SyncroC.DataBase.PRCManager.getPRCManager().existsAControlForCorrectUpDownWarp();
      bool existsAControlForCorrectEndToEndWarp = SyncroC.DataBase.PRCManager.getPRCManager().existsAControlForCorrectEndToEndWarp();
      if (!existsAControlForCorrectUpDownWarp)
      {
        this.pct_WarpUpDown.Visible = false;
        this.warpUpDownControl.Visible = false;

        if (existsAControlForCorrectEndToEndWarp)
        {
          this.pnl_PRC.SetColumn(this.warpEndToEndControl, 0);
          this.pnl_PRC.SetColumnSpan(this.warpEndToEndControl, 2);
        }
      }
      else
      {
        this.pct_WarpUpDown.Visible = true;
        this.warpUpDownControl.Visible = true;
      }

      if (!existsAControlForCorrectEndToEndWarp)
      {
        this.pct_WarpEndToEnd.Visible = false;
        this.warpEndToEndControl.Visible = false;

        if (existsAControlForCorrectUpDownWarp)
        {
          this.pnl_PRC.SetColumn(this.warpUpDownControl, 0);
          this.pnl_PRC.SetColumnSpan(this.warpUpDownControl, 2);
        }
      }
      else
      {
        this.pct_WarpEndToEnd.Visible = true;
        this.warpEndToEndControl.Visible = true;
      }

      this.prc_glueGap21.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "gluegap_21");
      this.prc_glueGap21.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "gluegap_21");
      this.prc_glueGap11.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "gluegap_11");
      this.prc_glueGap11.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "gluegap_11");
      this.prc_glueGap1.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "gluegap_1");
      this.prc_glueGap1.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "gluegap_1");
      this.prc_glueGap2.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "gluegap_2");
      this.prc_glueGap2.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "gluegap_2");

      this.prc_paperTension5.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_5");
      this.prc_paperTension5.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_5");
      this.prc_paperTension4.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_4");
      this.prc_paperTension4.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_4");
      this.prc_paperTension3.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_3");
      this.prc_paperTension3.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_3");
      this.prc_paperTension2.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_2");
      this.prc_paperTension2.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_2");
      this.prc_paperTension1.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_1");
      this.prc_paperTension1.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_1");

      this.prc_paperTension22.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_22");
      this.prc_paperTension22.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_22");
      this.prc_paperTension21.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "papertension_21");
      this.prc_paperTension21.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "papertension_21");

      this.prc_SteamSection1.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steam_group_1");
      this.prc_SteamSection1.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steam_group_1");
      this.prc_SteamSection2.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steam_group_2");
      this.prc_SteamSection2.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steam_group_2");
      this.prc_SteamSection3.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steam_group_3");
      this.prc_SteamSection3.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steam_group_3");
      this.prc_SteamSection0.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steam_group_0");
      this.prc_SteamSection0.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steam_group_0");

      this.prc_sfmainsteam1.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "sfmainsteam_1");
      this.prc_sfmainsteam1.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "sfmainsteam_1");
      this.prc_sfmainsteam2.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "sfmainsteam_2");
      this.prc_sfmainsteam2.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "sfmainsteam_2");

      this.prc_ShoesPressure1.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "pressure_hot_section_1");
      this.prc_ShoesPressure1.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "pressure_hot_section_1");
      this.prc_ShoesPressure2.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "pressure_hot_section_2");
      this.prc_ShoesPressure2.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "pressure_hot_section_2");
      this.prc_ShoesPressure3.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "pressure_hot_section_3");
      this.prc_ShoesPressure3.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "pressure_hot_section_3");
      this.prc_ShoesPressure4.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "pressure_hot_section_4");
      this.prc_ShoesPressure4.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "pressure_hot_section_4");

      this.prc_wrapArmPreHeater31.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_31");
      this.prc_wrapArmPreHeater31.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_31");
      this.prc_wrapArmPreHeater11.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_11");
      this.prc_wrapArmPreHeater11.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_11");
      this.prc_wrapArmPreHeater1.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_1");
      this.prc_wrapArmPreHeater1.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_1");
      this.prc_wrapArmPreHeater2.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_2");
      this.prc_wrapArmPreHeater2.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_2");
      this.prc_wrapArmPreHeater20.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_20");
      this.prc_wrapArmPreHeater20.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_20");

      this.prc_wrapArmPreHeater13.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_13");
      this.prc_wrapArmPreHeater13.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_13");
      this.prc_wrapArmPreHeater14.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_14");
      this.prc_wrapArmPreHeater14.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_14");
      this.prc_wrapArmPreHeater22.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_22");
      this.prc_wrapArmPreHeater22.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_22");
      this.prc_wrapArmPreHeater23.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_23");
      this.prc_wrapArmPreHeater23.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_23");

      this.prc_wrapArmPreHeater12.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_12");
      this.prc_wrapArmPreHeater12.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_12");
      this.prc_wrapArmPreHeater15.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_15");
      this.prc_wrapArmPreHeater15.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_15");
      this.prc_wrapArmPreHeater24.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_24");
      this.prc_wrapArmPreHeater24.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_24");
      this.prc_wrapArmPreHeater25.MinTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "wraparm_25");
      this.prc_wrapArmPreHeater25.MaxTemperatureSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "wraparm_25");

      if (SyncroC.DBManager.getDBManager().lineConfig.DoubleBackerConfig.SteamShower.Present)
      {
        this.pnl_ZoneDB.SetCellPosition(this.prc_SteamShower4, new TableLayoutPanelCellPosition(1, 1));
        this.prc_SteamShower4.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steamshower_4");
        this.prc_SteamShower4.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steamshower_4");
        this.prc_SteamShower4.Visible = true;
      }

      this.prc_SteamShower1.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steamshower_1");
      this.prc_SteamShower1.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steamshower_1");
      this.prc_SteamShower2.MinSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, "steamshower_2");
      this.prc_SteamShower2.MaxSetPointValue = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, "steamshower_2");

      this.prc_db_hot_Section.StandardTemperature = DBManager.getDBManager().lineConfig.PRCConfig.Header.ShowWarningWhenTemperatureLessDBHotSection;
      this.prc_db_cold_Section.StandardTemperature = DBManager.getDBManager().lineConfig.PRCConfig.Header.ShowWarningWhenTemperatureLessDBColdSection;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void _Show()
    {
      base._Show();

      //ABELLO_FI11/02/15_2 Gestita un pò meglio la configurazione con un solo SingleFacer
      if (!DBManager.getDBManager().lineConfig.SingleFacer1)
      {
        this.pnl_ZoneSf1.Visible = false;
      }

      if (!DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        this.pnl_ZoneSf2.Visible = false;
      }

      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.Zoom_PRC.Enabled = false;
        this.Zoom_ZoneDB.Enabled = false;
        this.Zoom_ZonePHT.Enabled = false;
        this.Zoom_ZoneSf1.Enabled = false;
        this.Zoom_ZoneSf2.Enabled = false;
      }
    }

    protected override void F12()
    {
    }

    protected override void ControlD()
    {
      showDebugOptions = !showDebugOptions;

      this.modifyVisibleTableLayoutControlCollection(this.pnl_PRC.Controls, showDebugOptions);
      this.modifyVisibleTableLayoutControlCollection(this.pnl_ZoneDB.Controls, showDebugOptions);
      this.modifyVisibleTableLayoutControlCollection(this.pnl_ZonePht.Controls, showDebugOptions);
      this.modifyVisibleTableLayoutControlCollection(this.pnl_ZoneSf1.Controls, showDebugOptions);
      this.modifyVisibleTableLayoutControlCollection(this.pnl_ZoneSf2.Controls, showDebugOptions);

      //ABELLO_FI24/11/2010_1 [PRC] Aggiunta gestione a grafica dell'umidità sopra/sotto
      this.lbl_MoistureSensor.Visible = showDebugOptions;
    }

    private void modifyVisibleTableLayoutControlCollection(TableLayoutControlCollection controls, bool visible)
    {
      foreach (Control a in controls)
      {
        if (a is SyncroC.Controls.Prc.PrcWrapArm)
        {
          ((SyncroC.Controls.Prc.PrcWrapArm)a).Visible = visible;
        }
        else if (a is SyncroC.Controls.Prc.PrcControl)
        {
          ((SyncroC.Controls.Prc.PrcControl)a).Visible = visible;
        }
        else if (a is SyncroC.Controls.StateButton)
        {
          ((SyncroC.Controls.StateButton)a).Visible = visible;
        }
        else if (a is SyncroC.Controls.Prc.PrcTemperature)
        {
          ((SyncroC.Controls.Prc.PrcTemperature)a).Visible = visible;
        }
      }
    }

    //ABELLO_FI24/11/2010_1 [PRC] Aggiunta gestione a grafica dell'umidità sopra/sotto
    private void _updateMoistureValue(int umido_sotto, int umido_sopra)
    {
      if ((umido_sotto != 0) || (umido_sopra != 0))
      {
        int umido_sopra_MENO_umido_sotto = umido_sopra - umido_sotto;
        //999 - 999 = 999  =>   [-3]
        this.lbl_MoistureSensor.Text = String.Format("\u21D1 [{0}] - \u21D3 [{1}] = {2}", umido_sopra, umido_sotto, umido_sopra_MENO_umido_sotto);

        if (!this.lbl_MoistureSensor.Visible)
          this.lbl_MoistureSensor.Visible = true;
      }
    }

    //RFAZZI_FI25/08/2011_2. Aggiunto controllo PRC CloseLoop ON/OFF. 
    private void _updateWarpUpDownValue(int value, bool warpCloseLoopUpDownStatus)
    {
      this.warpUpDownControl.setSelectedWarp(value);
      if (!this.warpUpDownAutoMan.Visible || !warpCloseLoopUpDownStatus)
      {
        switch (value)
        {
          case 3:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_3;
            break;
          case 2:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_2;
            break;
          case 1:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_1;
            break;
          case 0:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_0;
            break;
          case -1:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_M1;
            break;
          case -2:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_M2;
            break;
          case -3:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_M3;
            break;
        }
      }
      else
      {
        switch (value)
        {
          case 3:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_3_CloseLoop_Auto;
            break;
          case 2:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_2_CloseLoop_Auto;
            break;
          case 1:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_1_CloseLoop_Auto;
            break;
          case 0:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_0_CloseLoop_Auto;
            break;
          case -1:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_M1_CloseLoop_Auto;
            break;
          case -2:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_M2_CloseLoop_Auto;
            break;
          case -3:
            this.pct_WarpUpDown.Image = global::SyncroC.Properties.Resources.warp_updown_pos_M3_CloseLoop_Auto;
            break;
        }
      }

      if ( this.warpUpDownAutoMan.Visible )
      {
        this.pct_WarpUpDown.Enabled = !warpCloseLoopUpDownStatus;
        this.warpUpDownControl.Enabled = !warpCloseLoopUpDownStatus;
        this.warpUpDownAutoMan.Active = warpCloseLoopUpDownStatus;
      }
    }

    //RFAZZI_FI25/08/2011_2. Aggiunto controllo PRC CloseLoop ON/OFF. 
    private void _updateWarpEndToEndValue(int value, bool warpCloseLoopEndToEndStatus)
    {
      this.warpEndToEndControl.setSelectedWarp(value);
      if (!this.warpEndToEndAutoMan.Visible || !warpCloseLoopEndToEndStatus)
      {
        switch (value)
        {
          case 3:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_3;
            break;
          case 2:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_2;
            break;
          case 1:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_1;
            break;
          case 0:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_0;
            break;
          case -1:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_M1;
            break;
          case -2:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_M2;
            break;
          case -3:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_M3;
            break;
        }
      }
      else
      {
        switch (value)
        {
          case 3:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_3_CloseLoop_Auto;
            break;
          case 2:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_2_CloseLoop_Auto;
            break;
          case 1:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_1_CloseLoop_Auto;
            break;
          case 0:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_0_CloseLoop_Auto;
            break;
          case -1:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_M1_CloseLoop_Auto;
            break;
          case -2:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_M2_CloseLoop_Auto;
            break;
          case -3:
            this.pct_WarpEndToEnd.Image = global::SyncroC.Properties.Resources.warp_endtoend_pos_M3_CloseLoop_Auto;
            break;
        }
      }

      if (this.warpEndToEndAutoMan.Visible)
      {
        this.pct_WarpEndToEnd.Enabled = !warpCloseLoopEndToEndStatus;
        this.warpEndToEndControl.Enabled = !warpCloseLoopEndToEndStatus;
        this.warpEndToEndAutoMan.Active = warpCloseLoopEndToEndStatus;
      }
    }

    private void _setTargetSpeedDoubleBacker(int speedZone1_UVm_m, int speedTargetZone1_UVm_m)
    {
      this.tnud_TargetSpeedDoubleBacker_UVmm_m.Text = speedTargetZone1_UVm_m.ToString();
      int diff = speedZone1_UVm_m - speedTargetZone1_UVm_m;
      if ((diff > 0) || (diff > -10))
      {
        this.tnud_TargetSpeedDoubleBacker_UVmm_m.BackColor = Color.LightGreen;
      }
      else
      {
        if (diff > -30)
          this.tnud_TargetSpeedDoubleBacker_UVmm_m.BackColor = Color.LightYellow;
        else
          this.tnud_TargetSpeedDoubleBacker_UVmm_m.BackColor = Color.Red;
      }
    }

    private string shiftId = "";
    private string shiftManager = "";
    private void _setActiveAndSpeed(bool isIDLEShift, string shiftId, string shiftManager, bool zone1Used, bool zone2Used, int speedZone1_UVmm_m, int speedZone2_UVmm_m, int speedSpl2_UVmm_m, int speedSpl3_UVmm_m, int speedSpl4_UVmm_m, int speedSpl5_UVmm_m)
    {
      bool zone0Used = true;
      this.shiftId = shiftId.Trim();
      this.shiftManager = shiftManager.Trim();

      if (isIDLEShift)
      {
        this.shiftId = "";
        this.shiftManager = "";

        this.lbl_CtrlF5_YY.Enabled = false;

        zone1Used = false;
        zone2Used = false;
        zone0Used = false;

        if (this.SF1_isZoomIn)
          this.Zoom_ZoneSf1_Click(null,null);

        if (this.SF2_isZoomIn)
          this.Zoom_ZoneSf2_Click(null, null);

        if (this.PHT_isZoomIn)
          this.Zoom_ZonePHT_Click(null, null);

        if (this.DB_isZoomIn)
          this.Zoom_ZoneDB_Click(null, null);

        if (this.PRC_isZoomIn)
          this.Zoom_PRC_Click(null, null);
      }
      else
        this.lbl_CtrlF5_YY.Enabled = true;

      if (this.pnl_ZonePht.Enabled != zone0Used)
      {
        this.pnl_ZonePht.BackColor = zone0Used ? Color.FromArgb(209, 127, 129) : Color.Gray;
        this.Zoom_ZonePHT.Visible = zone0Used;
        this.pnl_ZonePht.Enabled = zone0Used;
        this.pct_PrcBoardLevelNumber0.Visible = zone0Used;
        this.ForceLocalPrcBoardLevelNumber_0.Visible = this.ForceRemotePrcBoardLevelNumber_0.Visible = zone0Used;
        this.ResetUserValue_0.Visible = zone0Used;
      }

      if (this.pnl_ZoneDB.Enabled != zone0Used)
      {
        this.pnl_ZoneDB.BackColor = zone0Used ? Color.FromArgb(51, 51, 139) : Color.Gray;
        this.Zoom_ZoneDB.Visible = zone0Used;
        this.tnud_SpeedDoubleBacker_UVmm_m.Visible = zone0Used;
        this.pnl_ZoneDB.Enabled = zone0Used;
      }

      if (this.pnl_PRC.Enabled != zone0Used)
      {
        this.pnl_PRC.BackColor = zone0Used ? Color.Black : Color.Gray;
        this.Zoom_PRC.Visible = zone0Used;
        this.pnl_PRC.Enabled = zone0Used;
      }

      if (this.pnl_ZoneSf1.Enabled != zone1Used)
      {
        this.pnl_ZoneSf1.BackColor = zone1Used ? Color.FromArgb(193, 192, 224) : Color.Gray;
        this.tnud_SpeedSF1_UVmm_m.Visible = zone1Used;
        this.tnud_SpeedSpl2_UVmm_m.Visible = zone1Used;
        this.tnud_SpeedSpl3_UVmm_m.Visible = zone1Used;
        this.Zoom_ZoneSf1.Visible = zone1Used;
        this.pnl_ZoneSf1.Enabled = zone1Used;
        this.pct_PrcBoardLevelNumber1.Visible = zone1Used;
        this.ForceLocalPrcBoardLevelNumber_1.Visible = this.ForceRemotePrcBoardLevelNumber_1.Visible = zone1Used;
        this.ResetUserValue_1.Visible = zone1Used;
        this.prc_glueGap1.Enabled = zone1Used;
        this.prc_wrapArmPreHeater1.Enabled = zone1Used;
      }

      if (zone1Used)
      {
        this.tnud_SpeedSF1_UVmm_m.Text = (speedZone1_UVmm_m / 1000).ToString();
        this.tnud_SpeedSpl2_UVmm_m.Text = (speedSpl2_UVmm_m / 1000).ToString();
        this.tnud_SpeedSpl3_UVmm_m.Text = (speedSpl3_UVmm_m / 1000).ToString();
      }

      if (this.pnl_ZoneSf2.Enabled != zone2Used)
      {
        this.pnl_ZoneSf2.BackColor = zone2Used ? Color.FromArgb(193, 192, 224) : Color.Gray;
        this.tnud_SpeedSF2_UVmm_m.Visible = zone2Used;
        this.tnud_SpeedSpl4_UVmm_m.Visible = zone2Used;
        this.tnud_SpeedSpl5_UVmm_m.Visible = zone2Used;
        this.Zoom_ZoneSf2.Visible = zone2Used;
        this.pnl_ZoneSf2.Enabled = zone2Used;
        this.pct_PrcBoardLevelNumber2.Visible = zone2Used;
        this.ForceLocalPrcBoardLevelNumber_2.Visible = this.ForceRemotePrcBoardLevelNumber_2.Visible = zone2Used;
        this.ResetUserValue_2.Visible = zone2Used;
        this.prc_glueGap2.Enabled = zone2Used;
        this.prc_wrapArmPreHeater2.Enabled = zone2Used;
        this.prc_wrapArmPreHeater20.Enabled = zone2Used;
      }

      if (zone2Used)
      {
        this.tnud_SpeedSF2_UVmm_m.Text = (speedZone2_UVmm_m / 1000).ToString();
        this.tnud_SpeedSpl4_UVmm_m.Text = (speedSpl4_UVmm_m / 1000).ToString();
        this.tnud_SpeedSpl5_UVmm_m.Text = (speedSpl5_UVmm_m / 1000).ToString();
      }

      
    }

    private void _setZoneInformation(int zoneNumber, string fluteId, string paperIdX, string paperIdY, int weigthFactor_UWg_m)
    {
      if (zoneNumber == 2)
        this._setZoneInformation(2, fluteId, paperIdX, paperIdY, "", "", "", weigthFactor_UWg_m);
      else
        if (zoneNumber == 1)
          this._setZoneInformation(1, fluteId, "", "", paperIdX, paperIdY, "", weigthFactor_UWg_m);
    }

    private void _setZoneInformation(int zoneNumber, string fluteId, string paperId5, string paperId4, string paperId3, string paperId2, string paperId1, int weigthFactor_UWg_m)
    {
      SyncroC.Controls.myLabel myLabel = this.lbl_Paper_Zone0;
      switch (zoneNumber)
      {
        case 0:
          myLabel = this.lbl_Paper_Zone0;
          break;
        case 1:
          myLabel = this.lbl_Paper_Zone1;
          break;
        case 2:
          myLabel = this.lbl_Paper_Zone2;
          break;
      }

      //string paperId = paperId5 + paperId4 + paperId3 + paperId2 + paperId1;

      string paperId = "";

      //ABELLO_FI01/12/2010_2 [PRC] Nei vecchi sistemi la carta non usata era "---"
      if ((paperId5.Trim() != "") && (paperId5.Trim() != "---"))
        paperId += "[" + paperId5 + "]";

      if ((paperId4.Trim() != "") && (paperId4.Trim() != "---"))
        paperId += "[" + paperId4 + "]";

      if ((paperId3.Trim() != "") && (paperId3.Trim() != "---"))
        paperId += "[" + paperId3 + "]";

      if ((paperId2.Trim() != "") && (paperId2.Trim() != "---"))
        paperId += "[" + paperId2 + "]";

      if ((paperId1.Trim() != "") && (paperId1.Trim() != "---"))
        paperId += "[" + paperId1 + "]";

      if (fluteId.Trim() == "")
        myLabel.Testo = "";
      else
        myLabel.Testo = fluteId + " - " + paperId + " = " + weigthFactor_UWg_m.ToString();
    }

    private void _setCurrentQualityId(string theCurrentQualityId)
    {
      //mettere questa sarebbe ottima, ma poi se qualcuno fa un override sulla qualità in lavoro non si vede
      //if (this.currentQualityId != theCurrentQualityId)
      {
        List<string> listComponentsWhereIsQualityOverride = SyncroC.DataBase.PRCManager.getPRCManager().getComponentsWhereIsQualityOverride(theCurrentQualityId);

        if(listComponentsWhereIsQualityOverride.Contains("gluegap_1"))
          this.prc_glueGap1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_glueGap1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("gluegap_2"))
          this.prc_glueGap2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_glueGap2.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("gluegap_11"))
          this.prc_glueGap11.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_glueGap11.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("gluegap_21"))
          this.prc_glueGap21.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_glueGap21.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("pressure_hot_section_1"))
          this.prc_ShoesPressure1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_ShoesPressure1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("pressure_hot_section_2"))
          this.prc_ShoesPressure2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_ShoesPressure2.IsAComponentWhereIsQualityOverride = false;

        //ABELLO_FI01/12/2010_3 [PRC] mancavano alcuni cerchietti gialli
        if (listComponentsWhereIsQualityOverride.Contains("pressure_hot_section_3"))
          this.prc_ShoesPressure3.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_ShoesPressure3.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("pressure_hot_section_4"))
          this.prc_ShoesPressure4.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_ShoesPressure4.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("sfmainsteam_1"))
          this.prc_sfmainsteam1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_sfmainsteam1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("sfmainsteam_2"))
          this.prc_sfmainsteam2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_sfmainsteam2.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steam_group_1"))
          this.prc_SteamSection1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamSection1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steam_group_2"))
          this.prc_SteamSection2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamSection2.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steam_group_3"))
          this.prc_SteamSection3.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamSection3.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steam_group_0"))
          this.prc_SteamSection0.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamSection0.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steamshower_1"))
          this.prc_SteamShower1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamShower1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steamshower_2"))
          this.prc_SteamShower2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamShower2.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("steamshower_4"))
          this.prc_SteamShower4.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_SteamShower4.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_1"))
          this.prc_wrapArmPreHeater1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_11"))
          this.prc_wrapArmPreHeater11.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater11.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_12"))
          this.prc_wrapArmPreHeater12.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater12.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_13"))
          this.prc_wrapArmPreHeater13.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater13.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_14"))
          this.prc_wrapArmPreHeater14.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater14.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_15"))
          this.prc_wrapArmPreHeater15.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater15.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_2"))
          this.prc_wrapArmPreHeater2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater2.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_20"))
          this.prc_wrapArmPreHeater20.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater20.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_22"))
          this.prc_wrapArmPreHeater22.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater22.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_23"))
          this.prc_wrapArmPreHeater23.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater23.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_24"))
          this.prc_wrapArmPreHeater24.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater24.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("wraparm_25"))
          this.prc_wrapArmPreHeater25.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater25.IsAComponentWhereIsQualityOverride = false;

        //ABELLO_FI01/12/2010_3 [PRC] mancavano alcuni cerchietti gialli
        if (listComponentsWhereIsQualityOverride.Contains("wraparm_31"))
          this.prc_wrapArmPreHeater31.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_wrapArmPreHeater31.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_1"))
          this.prc_paperTension1.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension1.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_2"))
          this.prc_paperTension2.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension2.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_3"))
          this.prc_paperTension3.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension3.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_4"))
          this.prc_paperTension4.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension4.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_5"))
          this.prc_paperTension5.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension5.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_21"))
          this.prc_paperTension21.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension21.IsAComponentWhereIsQualityOverride = false;

        if (listComponentsWhereIsQualityOverride.Contains("papertension_22"))
          this.prc_paperTension22.IsAComponentWhereIsQualityOverride = true;
        else
          this.prc_paperTension22.IsAComponentWhereIsQualityOverride = false;
      }
      this.currentQualityId = theCurrentQualityId;
    }

    private int splicer1RollWidth_UDmm = 0;
    private int splicer2RollWidth_UDmm = 0;
    private int splicer3RollWidth_UDmm = 0;
    private int splicer4RollWidth_UDmm = 0;
    private int splicer5RollWidth_UDmm = 0;
    private void _setSplicerInformation(int splNumber, string paperId, int weigthFactor_UWg_m, int rollWidth_UDmm)
    {
      SyncroC.Controls.myLabel myLabel = this.lbl_Paper_Spl1;
      switch (splNumber)
      {
        case 1:
          myLabel = this.lbl_Paper_Spl1;
          this.splicer1RollWidth_UDmm = rollWidth_UDmm;
          break;
        case 2:
          myLabel = this.lbl_Paper_Spl2;
          this.splicer2RollWidth_UDmm = rollWidth_UDmm;
          break;
        case 3:
          myLabel = this.lbl_Paper_Spl3;
          this.splicer3RollWidth_UDmm = rollWidth_UDmm;
          break;
        case 4:
          myLabel = this.lbl_Paper_Spl4;
          this.splicer4RollWidth_UDmm = rollWidth_UDmm;
          break;
        case 5:
          myLabel = this.lbl_Paper_Spl5;
          this.splicer5RollWidth_UDmm = rollWidth_UDmm;
          break;
        default:
          return;
      }

      if (paperId.Trim() == "")
      {
        myLabel.Testo = "";
      }
      else
      {
        //if (splNumber == 1)
        //  myLabel.Testo = paperId + " - " + weigthFactor_UWg_m.ToString() + " - " + (rollWidth_UDmm / 10).ToString();
        //else
          myLabel.Testo = paperId + Environment.NewLine + weigthFactor_UWg_m.ToString() + Environment.NewLine + (rollWidth_UDmm).ToString();
      }
    }

    private void modifyTableLayoutControlCollection(TableLayoutControlCollection controls, bool ZoomIn)
    {
      foreach (Control a in controls)
      {
        if (a is SyncroC.Controls.Prc.PrcWrapArm)
        {
          ((SyncroC.Controls.Prc.PrcWrapArm)a).LowLevelEnabled = ZoomIn;
        }
        else if (a is SyncroC.Controls.Prc.PrcControl)
        {
          ((SyncroC.Controls.Prc.PrcControl)a).LowLevelEnabled = ZoomIn;
        }
        else if (a is SyncroC.Controls.StateButton)
        {
          ((SyncroC.Controls.StateButton)a).resize();
        }
      }
    }

    //ABello_FI10/11/2010_1 fatta l'inversione della pagina home PRC
    private void Zoom_ZoneSf1_Click(object sender, EventArgs e)
    {
      if (this.SF2_isZoomIn)
        this.Zoom_ZoneSf2_Click(null, null);

      if (this.PHT_isZoomIn)
        this.Zoom_ZonePHT_Click(null, null);

      if (this.DB_isZoomIn)
        this.Zoom_ZoneDB_Click(null, null);

      if (this.PRC_isZoomIn)
        this.Zoom_PRC_Click(null, null);

      if (!this.SF1_isZoomIn)
      {
        this.SF1_originalSize = this.pnl_ZoneSf1.Size;
        this.SF1_originalLocation = this.pnl_ZoneSf1.Location;
        Size newSize = new Size((int)(this.pnl_ZoneSf1.Size.Width * SF1_scaleFactor), (int)(this.pnl_ZoneSf1.Size.Height * SF1_scaleFactor));

        if (this.corrugatorDirectionLeft)
          this.pnl_ZoneSf1.Location = new Point(this.pnl_ZoneSf1.Location.X - (newSize.Width - this.SF1_originalSize.Width), this.pnl_ZoneSf1.Location.Y - (newSize.Height - this.SF1_originalSize.Height));
        else
          this.pnl_ZoneSf1.Location = new Point(this.pnl_ZoneSf1.Location.X, this.pnl_ZoneSf1.Location.Y - (newSize.Height - this.SF1_originalSize.Height));
        
        this.pnl_ZoneSf1.Size = new Size(newSize.Width, newSize.Height);
        this.pnl_ZoneSf1.BringToFront();

        this.modifyTableLayoutControlCollection(this.pnl_ZoneSf1.Controls, true);

        this.SF1_isZoomIn = true;
        this.Zoom_ZoneSf1.Active = true;

        this.pnl_ZoneSf1.BorderStyle = BorderStyle.Fixed3D;
      }
      else
      {
        this.pnl_ZoneSf1.Location = new Point(this.SF1_originalLocation.X, this.SF1_originalLocation.Y);
        this.pnl_ZoneSf1.Size = new Size(this.SF1_originalSize.Width, this.SF1_originalSize.Height);
        this.pnl_ZoneSf1.SendToBack();
        this.pnl_PRC.SendToBack();

        this.modifyTableLayoutControlCollection(this.pnl_ZoneSf1.Controls, false);

        this.SF1_isZoomIn = false;
        this.Zoom_ZoneSf1.Active = false;

        this.pnl_ZoneSf1.BorderStyle = BorderStyle.FixedSingle;
      }
    }

    //ABello_FI10/11/2010_1 fatta l'inversione della pagina home PRC
    private void Zoom_ZoneSf2_Click(object sender, EventArgs e)
    {
      if (this.SF1_isZoomIn)
        this.Zoom_ZoneSf1_Click(null,null);

      if (this.PHT_isZoomIn)
        this.Zoom_ZonePHT_Click(null, null);

      if (this.DB_isZoomIn)
        this.Zoom_ZoneDB_Click(null, null);

      if (this.PRC_isZoomIn)
        this.Zoom_PRC_Click(null, null);

      if (!this.SF2_isZoomIn)
      {
        this.SF2_originalSize = this.pnl_ZoneSf2.Size;
        this.SF2_originalLocation = this.pnl_ZoneSf2.Location;
        Size newSize = new Size((int)(this.pnl_ZoneSf2.Size.Width * SF2_scaleFactor), (int)(this.pnl_ZoneSf2.Size.Height * SF2_scaleFactor));

        if (this.corrugatorDirectionLeft)
          this.pnl_ZoneSf2.Location = new Point(this.pnl_ZoneSf2.Location.X - (newSize.Width - this.SF2_originalSize.Width), this.pnl_ZoneSf2.Location.Y - (newSize.Height - this.SF2_originalSize.Height));

        this.pnl_ZoneSf2.Size = new Size(newSize.Width, newSize.Height);
        this.pnl_ZoneSf2.BringToFront();

        this.modifyTableLayoutControlCollection(this.pnl_ZoneSf2.Controls, true);

        this.SF2_isZoomIn = true;
        this.Zoom_ZoneSf2.Active = true;

        this.pnl_ZoneSf2.BorderStyle = BorderStyle.Fixed3D;
      }
      else
      {
        this.pnl_ZoneSf2.Location = new Point(this.SF2_originalLocation.X, this.SF2_originalLocation.Y);
        this.pnl_ZoneSf2.Size = new Size(this.SF2_originalSize.Width, this.SF2_originalSize.Height);
        this.pnl_ZoneSf2.SendToBack();

        this.modifyTableLayoutControlCollection(this.pnl_ZoneSf2.Controls, false);

        this.SF2_isZoomIn = false;
        this.Zoom_ZoneSf2.Active = false;

        this.pnl_ZoneSf2.BorderStyle = BorderStyle.FixedSingle;
      }
    }

    private void Zoom_ZonePHT_Click(object sender, EventArgs e)
    {
      if (this.SF2_isZoomIn)
        this.Zoom_ZoneSf2_Click(null, null);

      if (this.SF1_isZoomIn)
        this.Zoom_ZoneSf1_Click(null, null);

      if (this.DB_isZoomIn)
        this.Zoom_ZoneDB_Click(null, null);

      if (this.PRC_isZoomIn)
        this.Zoom_PRC_Click(null, null);

      if (!this.PHT_isZoomIn)
      {
        this.PHT_originalSize = this.pnl_ZonePht.Size;
        this.PHT_originalLocation = this.pnl_ZonePht.Location;
        Size newSize = new Size((int)(this.pnl_ZonePht.Size.Width * PHT_scaleFactor), (int)(this.pnl_ZonePht.Size.Height * PHT_scaleFactor));

        if (this.corrugatorDirectionLeft)
          this.pnl_ZonePht.Location = new Point(this.pnl_ZonePht.Location.X, this.pnl_ZonePht.Location.Y);
        else
          this.pnl_ZonePht.Location = new Point(this.pnl_ZonePht.Location.X - (newSize.Width - this.PHT_originalSize.Width), this.pnl_ZonePht.Location.Y);
        
        this.pnl_ZonePht.Size = new Size(newSize.Width, newSize.Height);
        this.pnl_ZonePht.BringToFront();

        this.modifyTableLayoutControlCollection(this.pnl_ZonePht.Controls, true);

        this.PHT_isZoomIn = true;
        this.Zoom_ZonePHT.Active = true;

        this.pnl_ZonePht.BorderStyle = BorderStyle.Fixed3D;
      }
      else
      {
        this.pnl_ZonePht.Location = new Point(this.PHT_originalLocation.X, this.PHT_originalLocation.Y);
        this.pnl_ZonePht.Size = new Size(this.PHT_originalSize.Width, this.PHT_originalSize.Height);
        this.pnl_ZonePht.SendToBack();
        this.pnl_PRC.SendToBack();

        this.modifyTableLayoutControlCollection(this.pnl_ZonePht.Controls, false);

        this.PHT_isZoomIn = false;
        this.Zoom_ZonePHT.Active = false;

        this.pnl_ZonePht.BorderStyle = BorderStyle.FixedSingle;
      }
    }

    private void Zoom_PRC_Click(object sender, EventArgs e)
    {
      if (this.SF2_isZoomIn)
        this.Zoom_ZoneSf2_Click(null, null);

      if (this.SF1_isZoomIn)
        this.Zoom_ZoneSf1_Click(null, null);

      if (this.PHT_isZoomIn)
        this.Zoom_ZonePHT_Click(null, null);

      if (this.DB_isZoomIn)
        this.Zoom_ZoneDB_Click(null, null);

      if (!this.PRC_isZoomIn)
      {
        this.PRC_originalSize = this.pnl_PRC.Size;
        this.PRC_originalLocation = this.pnl_PRC.Location;
        Size newSize = new Size((int)(this.pnl_PRC.Size.Width * PRC_scaleFactor), (int)(this.pnl_PRC.Size.Height * PRC_scaleFactor));
        this.pnl_PRC.Location = new Point(this.pnl_PRC.Location.X - (newSize.Width - this.pnl_PRC.Width), this.pnl_PRC.Location.Y - (newSize.Height - this.pnl_PRC.Height));
        this.pnl_PRC.Size = new Size(newSize.Width, newSize.Height);
        this.pnl_PRC.BringToFront();

        this.modifyTableLayoutControlCollection(this.pnl_PRC.Controls, true);

        this.PRC_isZoomIn = true;
        this.Zoom_PRC.Active = true;

        this.pnl_PRC.BorderStyle = BorderStyle.Fixed3D;
      }
      else
      {
        this.pnl_PRC.Location = new Point(this.PRC_originalLocation.X, this.PRC_originalLocation.Y);
        this.pnl_PRC.Size = new Size(this.PRC_originalSize.Width, this.PRC_originalSize.Height);
        this.pnl_PRC.SendToBack();

        this.modifyTableLayoutControlCollection(this.pnl_PRC.Controls, false);

        this.PRC_isZoomIn = false;
        this.Zoom_PRC.Active = false;

        this.pnl_PRC.BorderStyle = BorderStyle.FixedSingle;
      }
    }

    private void Zoom_ZoneDB_Click(object sender, EventArgs e)
    {
      if (this.SF2_isZoomIn)
        this.Zoom_ZoneSf2_Click(null, null);

      if (this.SF1_isZoomIn)
        this.Zoom_ZoneSf1_Click(null, null);

      if (this.PHT_isZoomIn)
        this.Zoom_ZonePHT_Click(null, null);

      if (this.PRC_isZoomIn)
        this.Zoom_PRC_Click(null, null);

      if (!this.DB_isZoomIn)
      {
        this.DB_originalSize = this.pnl_ZoneDB.Size;
        this.DB_originalLocation = this.pnl_ZoneDB.Location;
        Size newSize = new Size((int)(this.pnl_ZoneDB.Size.Width * DB_scaleFactor), (int)(this.pnl_ZoneDB.Size.Height * DB_scaleFactor));
        this.pnl_ZoneDB.Location = new Point((this.Width - newSize.Width) / 2, this.pnl_ZoneDB.Location.Y - (newSize.Height - this.pnl_ZoneDB.Height));
        this.pnl_ZoneDB.Size = new Size(newSize.Width, newSize.Height);
        this.pnl_ZoneDB.BringToFront();

        this.modifyTableLayoutControlCollection(this.pnl_ZoneDB.Controls, true);

        this.DB_isZoomIn = true;
        this.Zoom_ZoneDB.Active = true;

        this.pnl_ZoneDB.BorderStyle = BorderStyle.Fixed3D;
      }
      else
      {
        this.pnl_ZoneDB.Location = new Point(this.DB_originalLocation.X, this.DB_originalLocation.Y);
        this.pnl_ZoneDB.Size = new Size(this.DB_originalSize.Width, this.DB_originalSize.Height);
        this.pnl_ZoneDB.BringToFront();

        this.modifyTableLayoutControlCollection(this.pnl_ZoneDB.Controls, false);

        this.DB_isZoomIn = false;
        this.Zoom_ZoneDB.Active = false;

        this.pnl_ZoneDB.BorderStyle = BorderStyle.FixedSingle;
      }
    }

    protected override void CtrlF1()
    {
     
    }

    protected override void CtrlF2()
    {
      
    }

    protected override void CtrlF3()
    {
      
    }

    protected override void CtrlF4()
    {
      
    }

    protected override void CtrlF5()
    {
      if (this.shiftId == "")
        return;

      this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;

      Application.DoEvents();

      //Rimuovo tutti gli zoom
      if (this.SF1_isZoomIn)
        this.Zoom_ZoneSf1_Click(null, null);

      if (this.SF2_isZoomIn)
        this.Zoom_ZoneSf2_Click(null, null);

      if (this.PHT_isZoomIn)
        this.Zoom_ZonePHT_Click(null, null);

      if (this.DB_isZoomIn)
        this.Zoom_ZoneDB_Click(null, null);

      if (this.PRC_isZoomIn)
        this.Zoom_PRC_Click(null, null);

      Application.DoEvents();

      try
      {
        //Faccio la foto
        Bitmap bmp = SyncroC.Utility.SyncroImage.createSnapShot
          (
          this,
          this.leftHigh.X,
          this.leftHigh.Y - 1,
          this.rightHigh.X - this.leftHigh.X,
          this.leftLow.Y - this.leftHigh.Y + 1
          );

        //pop up per il commento
        SyncroC.Forms.PopUp.Frm_PRCAskMessage askValue = new SyncroC.Forms.PopUp.Frm_PRCAskMessage();
        FosberDialogResult ret = askValue.ShowDialog(this, DBManager.getDBManager().message.getTranslatedMessage("§PRC_18"));

        if (ret.Dialog == DialogResult.OK)
        {
          //se tutto bene archivio
          bool res = SyncroC.DataBase.SqlManager.getSqlManager().insertBytea
            (
            String.Format
              (
              "INSERT INTO {0}.prc_example (data,comment,bitmap,shiftid,shiftmanager,setupcode,qualityid,fluteid) VALUES ('{1}','{2}',:bytesData,'{3}','{4}','{5}','{6}','{7}')",
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE(),
              SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(DateTime.Now.ToUniversalTime()),
              SyncroC.DataBase.SqlManager.removeEscapeString((string)ret.Data[0]),
              SyncroC.DataBase.SqlManager.removeEscapeString(this.shiftId),
              SyncroC.DataBase.SqlManager.removeEscapeString(this.shiftManager),
              SyncroC.DataBase.SqlManager.removeEscapeString(SyncroC.DBManager.getDBManager().order.GetDryOrderField(0, SyncroC.DataBase.Order.DEL_SETUP_CODE)),
              SyncroC.DataBase.SqlManager.removeEscapeString(SyncroC.DBManager.getDBManager().order.GetDryOrderField(0, SyncroC.DataBase.Order.DEL_QUALITY_ID)),
              SyncroC.DataBase.SqlManager.removeEscapeString(DBManager.getDBManager().quality.getRow(SyncroC.DBManager.getDBManager().order.GetDryOrderField(0, SyncroC.DataBase.Order.DEL_QUALITY_ID))[SyncroC.DataBase.Quality.FLUTE_ID].ToString())
              ),
            (byte[])TypeDescriptor.GetConverter(bmp).ConvertTo(bmp, typeof(byte[]))
            );

          Logger.LogError(this, "Salvato un nuovo esempio sul Process Control");
        }

        //Cancello i messaggi se > 100
        if (SyncroC.DataBase.SqlManager.getSqlManager().getRowsNumber(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_example") >= SyncroC.DataBase.PRCManager.MAX_NUM_MESSAGES)
        {
          SyncroC.DataBase.SqlManager.getSqlManager().doCommand(
            String.Format("DELETE FROM {0}.prc_example WHERE data <= (SELECT MIN(data) FROM {0}.prc_example)", SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()),
            false);
          Logger.LogError(this, "Cancellato un esempio sul Process Control ... database full");
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex);
      }

      this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = this.lbl_Title_YN.BackColor;
    }

    private void warpUpDownControl_SelectedWarpChanged(object sender, EventArgs e)
    {
      // Send warp value
      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.PRC + ".SendWarpUpDownValue(" + this.warpUpDownControl.getSelectedWarp() + ")");
    }

    private void warpEndToEndControl_SelectedWarpChanged(object sender, EventArgs e)
    {
      // Send warp value
      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.PRC + ".SendWarpEndToEndValue(" + this.warpEndToEndControl.getSelectedWarp() + ")");
    }

    /// <summary>
    /// Pre heaters tern "wrap arm" click management
    /// </summary>
    private void OnPreHeatersTernWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = ((Control)sender).Name.Replace("prc_", args.AutoMan ? "AutoManual" : "LocalRemote");
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        string cmd = ((Control)sender).Name.Replace("prc_", "");
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    /// <summary>
    /// Glue Unit "glue gap" click management
    /// </summary>
    private void OnGlueUnitGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = ((Control)sender).Name.Replace("prc_", args.AutoMan ? "AutoManual" : "LocalRemote");
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        string cmd = ((Control)sender).Name.Replace("prc_", "");
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus)) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }


    /********************************
     * DOUBLE BACKER
     * *******************************/

    private void prc_wrapArmPreHeater31_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = ((Control)sender).Name.Replace("prc_", args.AutoMan ? "AutoManual" : "LocalRemote");

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        string cmd = ((Control)sender).Name.Replace("prc_", "");

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_SteamSection1_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "SteamPressureGroup1";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_SteamSection2_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "SteamPressureGroup2";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_SteamSection3_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "SteamPressureGroup3";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_SteamSection0_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "SteamPressureGroup0";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    /********************************
     * SINGLE FACER 1
     * *******************************/
    private void prc_wrapArmPreHeater13_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_wrapArmPreHeater12_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_wrapArmPreHeater23_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_wrapArmPreHeater22_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void gg_glueGap11_OnGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "glueGap1";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_SteamShower1_OnSteamShowerButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "SteamShower";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_SteamShower4_OnSteamShowerButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "SteamShower";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    //RFAZZI_FI15/11/2010_1. Bug Fix su comando di scambio Loc/Rem per Steam Shower e Main Steam da Home PRC
    private void prc_sfmainsteam1_OnsfmainsteamButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "sfmainsteam";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    /********************************
     * SINGLE FACER 2
     * *******************************/
    private void prc_wrapArmPreHeater15_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_wrapArmPreHeater14_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterEm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_wrapArmPreHeater25_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIl";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_wrapArmPreHeater24_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "wrapArmPreHeaterIm";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void gg_glueGap21_OnGlueGapButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "glueGap1";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void prc_SteamShower2_OnSteamShowerButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "SteamShower";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_sfmainsteam2_OnsfmainsteamButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = "sfmainsteam";
      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus02), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

		/********************************
		 * Paper Tension
		 * *******************************/

		private void prc_paperTension1_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTension";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 1) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 1) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void prc_paperTension2_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTension";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 2) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 2) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void prc_paperTension3_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTension";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 3) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 3) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void prc_paperTension4_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTension";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 4) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 4) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void prc_paperTension5_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTension";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 5) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 5) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void prc_paperTension21_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTensionBridge21";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void prc_paperTension22_OnPaperTensionButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;

			string cmd = "PaperTensionBridge22";
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_PAPER_TENSION + "(" + args.SetPoint.ToString() + ")");
			}
		}


		/********************************
		 * Process Control
		 * *******************************/

    private void prc_wrapArmPreHeaterN_OnPRCWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = ((Control)sender).Name.Replace("prc_", "");

      string index = cmd.Replace("wrapArmPreHeater", "");

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          Command.PRC + ".SendUserValue(" + cmd + Command.PARAMETER_SEPARATOR + index + Command.PARAMETER_SEPARATOR + args.SetPoint.ToString() + ")");
    }

    private void prc_LowLevelControl_OnPRCLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      string cmd = ((Control)sender).Name.Replace("prc_", "");

      string index = cmd.Replace("glueGap", "");
      index = index.Replace("paperTension", "");
      index = index.Replace("ShoesPressure", "");
      index = index.Replace("SteamSection", "");
      index = index.Replace("sfmainsteam", "");
      index = index.Replace("SteamShower", "");

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          Command.PRC + ".SendUserValue(" + cmd + Command.PARAMETER_SEPARATOR + index + Command.PARAMETER_SEPARATOR + args.SetPoint.ToString() + ")");
    }

    private void prc_LowLevelControl_OnPRCLowLevelControlAskForGraph(object sender, SyncroC.Controls.Prc.LowLevelControlAskForGraphEventArgs args)
    {
      try
      {
        if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
          return;

        string qualityid = this.currentQualityId;
        int min = ((SyncroC.Controls.Prc.PrcControl)sender).MinSetPointValue;
        int max = ((SyncroC.Controls.Prc.PrcControl)sender).MaxSetPointValue;

        string controlName = ((Control)sender).Name.Replace("prc_", "");

        string index = controlName.Replace("glueGap", "");
        index = index.Replace("paperTension", "");
        index = index.Replace("ShoesPressure", "");
        index = index.Replace("SteamSection", "");
        index = index.Replace("SteamShower", "");
        index = index.Replace("sfmainsteam", "");

        string tableName = "";

        SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.GlueGapGraphLinesEditor;
        if (controlName.StartsWith("glueGap"))
        {
          tableName = "gluegap_" + index;
          theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.GlueGapGraphLinesEditor;
        }
        else if (controlName.StartsWith("paperTension"))
        {
          tableName = "papertension_" + index;
          theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TensionGraphLinesEditor;
        }
        else if (controlName.StartsWith("ShoesPressure"))
        {
          tableName = "pressure_hot_section_" + index;
          theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.PressureHotSection;
        }
        else if (controlName.StartsWith("SteamSection"))
        {
          tableName = "steam_group_" + index;
          theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamPressure;
        }
        else if (controlName.StartsWith("SteamShower"))
        {
          tableName = "steamshower_" + index;
          theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamShower;
        }
        else if (controlName.StartsWith("sfmainsteam"))
        {
          tableName = "sfmainsteam_" + index;
          theTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamPressure;
        }

        bool isOverrideUsata = false;
        string resQueryUsata = "";
        List<SyncroC.Graph.Pair<int, int>> listaUsata = SyncroC.DataBase.PRCManager.getPRCManager().getPRCQuality(tableName, qualityid, out isOverrideUsata, out resQueryUsata);

        bool isOverrideOndaMadre = false;
        string resQueryOndaMadre = "";
        List<SyncroC.Graph.Pair<int, int>> listaOndaMadre = null;
        
        if(isOverrideUsata)
          listaOndaMadre = SyncroC.DataBase.PRCManager.getPRCManager().getPRCQuality(true, tableName, qualityid, out isOverrideOndaMadre, out resQueryOndaMadre);

        int splicerRollWidth_UDmm = -1;
        int speed = this.searchSpeed(((Control)sender).Parent.Name);

        if (controlName.StartsWith("paperTension"))
        {
          int tensionNumber = SyncroC.Utility.SyncroConverter.convertToInt(index, 100);
          if (tensionNumber < 10)
          {
            switch (tensionNumber)
            {
              case 1:
                splicerRollWidth_UDmm = this.splicer1RollWidth_UDmm;
                break;
              case 2:
                splicerRollWidth_UDmm = this.splicer2RollWidth_UDmm;
                break;
              case 3:
                splicerRollWidth_UDmm = this.splicer3RollWidth_UDmm;
                break;
              case 4:
                splicerRollWidth_UDmm = this.splicer4RollWidth_UDmm;
                break;
              case 5:
                splicerRollWidth_UDmm = this.splicer5RollWidth_UDmm;
                break;
            }
          }
          //ABELLO_FI02/12/2010_3 [PRC] la tensione del ponte segue la velocità dei piani
          else
          {
            speed = this.searchSpeed("pnl_ZoneDB");
          }
        }

        SyncroC.Forms.PopUp.Frm_PRCCurve prcCurve = new SyncroC.Forms.PopUp.Frm_PRCCurve();
        FosberDialogResult res = prcCurve.ShowDialog
          (
          Accessibility.getAccessibility().isLevelAtLeastMedium() && SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue,
          theTypeOfGraphLinesEditor,
          SyncroC.DataBase.PRCManager.getPRCManager().getSimpleNameForComponent(tableName),
          SyncroC.DataBase.PRCManager.getPRCManager().getFluteAndPaperWeightAndPaperClassification(tableName, qualityid),
          resQueryUsata,
          resQueryOndaMadre,
          isOverrideUsata,
          min,
          max,
          listaUsata,
          listaOndaMadre,
          args.UserCorrection,
          args.WarpCorrection,
          splicerRollWidth_UDmm,
          new Point(speed, ((SyncroC.Controls.Prc.PrcControl)sender).getCalculatedSetpoint())
          );

        if (res.Dialog == DialogResult.Yes)
        {
          int saveOn = (int)res.Data[0];
          List<SyncroC.Graph.Pair<int, int>> appoList = (List<SyncroC.Graph.Pair<int, int>>)res.Data[1];

          if (saveOn == 1)
          {
            if(isOverrideUsata)
              SyncroC.DataBase.PRCManager.getPRCManager().deletePRCQualityOverride(tableName, qualityid);

            //STANDARD
            string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + tableName;

            string fluteid = "";
            string papertype = "";
            string paperclassification = "";

            string[] resQuerySplit = isOverrideUsata ? resQueryOndaMadre.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries) : resQueryUsata.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            fluteid = resQuerySplit[0].Trim();
            papertype = resQuerySplit[1].Trim();
            paperclassification = resQuerySplit[2].Trim();
            if (resQuerySplit.Length == 4)
              paperclassification += "-" + resQuerySplit[3].Trim();

            string where = String.Format(" WHERE (fluteid='{0}' AND papertype='{1}' AND paperclassification='{2}')", fluteid, papertype, paperclassification);

            SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + where + ";", true);

            for (int i = 0; i < appoList.Count; i++)
            {
              SyncroC.DataBase.SqlManager.getSqlManager().doCommand
                (
                String.Format("INSERT INTO {0} (fluteid,papertype,paperclassification,speed_uvm_m,value) VALUES ('{1}','{2}','{3}',{4},{5});",
                myTableName,
                fluteid,
                papertype,
                paperclassification,
                appoList[i].First, //SPEED
                appoList[i].Second), //VALUE
                true);
            }
          }
          else if (saveOn == 2)
          {
            //OVERRIDE
            SyncroC.DataBase.PRCManager.getPRCManager().deletePRCQualityOverride(tableName, qualityid);
            SyncroC.DataBase.PRCManager.getPRCManager().insertPRCQualityOverride(tableName, qualityid, appoList);
          }

        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex);
      }
    }

    private void prc_wrapArmPreHeaterN_OnPRCWrapArmAskForGraph(object sender, SyncroC.Controls.Prc.WrapArmAskForGraphEventArgs args)
    {
      try
      {
        if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
          return;

        string qualityid = this.currentQualityId;
        int min = ((SyncroC.Controls.Prc.PrcWrapArm)sender).MinTemperatureSetPointValue;
        int max = ((SyncroC.Controls.Prc.PrcWrapArm)sender).MaxTemperatureSetPointValue;

        string controlName = ((Control)sender).Name.Replace("prc_", "");
        string index = controlName.Replace("wrapArmPreHeater", "");

        string tableName = "wraparm_" + index;

        bool isOverrideUsata = false;
        string resQueryUsata = "";
        List<SyncroC.Graph.Pair<int, int>> listaUsata = SyncroC.DataBase.PRCManager.getPRCManager().getPRCQuality(tableName, qualityid, out isOverrideUsata, out resQueryUsata);

        bool isOverrideOndaMadre = false;
        string resQueryOndaMadre = "";
        List<SyncroC.Graph.Pair<int, int>> listaOndaMadre = null;

        if (isOverrideUsata)
          listaOndaMadre = SyncroC.DataBase.PRCManager.getPRCManager().getPRCQuality(true, tableName, qualityid, out isOverrideOndaMadre, out resQueryOndaMadre);

        SyncroC.Forms.PopUp.Frm_PRCCurve prcCurve = new SyncroC.Forms.PopUp.Frm_PRCCurve();
        FosberDialogResult res = prcCurve.ShowDialog
          (
          Accessibility.getAccessibility().isLevelAtLeastMedium() && SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue,
          SyncroC.DataBase.PRCManager.getPRCManager().getTemperatureSensorPresentForWrapArm(tableName) ? SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmGraphLinesEditor : SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmPositionGraphLinesEditor,
          SyncroC.DataBase.PRCManager.getPRCManager().getSimpleNameForComponent(tableName),
          SyncroC.DataBase.PRCManager.getPRCManager().getFluteAndPaperWeightAndPaperClassification("wraparm_" + index, qualityid),
          resQueryUsata,
          resQueryOndaMadre,
          isOverrideUsata,
          min,
          max,
          listaUsata,
          listaOndaMadre,
          args.UserCorrection,
          args.WarpCorrection,
          new Point(this.searchSpeed(((Control)sender).Parent.Name), ((SyncroC.Controls.Prc.PrcWrapArm)sender).getCalculatedSetpoint())
          );

        if (res.Dialog == DialogResult.Yes)
        {
          int saveOn = (int)res.Data[0];
          List<SyncroC.Graph.Pair<int, int>> appoList = (List<SyncroC.Graph.Pair<int, int>>)res.Data[1];
          if (saveOn == 1)
          {
            string key = "wraparm_" + index;
            string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + key;

            if (isOverrideUsata)
              SyncroC.DataBase.PRCManager.getPRCManager().deletePRCQualityOverride(key, qualityid);

            string fluteid = "";
            string papertype = "";
            string paperclassification = "";

            string[] resQuerySplit = isOverrideUsata ? resQueryOndaMadre.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries) : resQueryUsata.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            fluteid = resQuerySplit[0].Trim();
            papertype = resQuerySplit[1].Trim();
            paperclassification = resQuerySplit[2].Trim();
            if (resQuerySplit.Length == 4)
              paperclassification += "-" + resQuerySplit[3].Trim();

            string where = String.Format(" WHERE (fluteid='{0}' AND papertype='{1}' AND paperclassification='{2}')", fluteid, papertype, paperclassification);

            SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + where + ";", true);

            for (int i = 0; i < appoList.Count; i++)
            {
              SyncroC.DataBase.SqlManager.getSqlManager().doCommand
                (
                String.Format("INSERT INTO {0} (fluteid,papertype,paperclassification,speed_uvm_m,value) VALUES ('{1}','{2}','{3}',{4},{5});",
                myTableName,
                fluteid,
                papertype,
                paperclassification,
                appoList[i].First, //SPEED
                appoList[i].Second), //VALUE
                true);
            }
          }
          else if (saveOn == 2)
          {
            string key = "wraparm_" + index;
            SyncroC.DataBase.PRCManager.getPRCManager().deletePRCQualityOverride(key, qualityid);
            SyncroC.DataBase.PRCManager.getPRCManager().insertPRCQualityOverride(key, qualityid, appoList);
          }

        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex);
      }
    }

    private int searchSpeed(string parentName)
    {
      int speed = 0;
      if (parentName == "pnl_ZoneDB" || parentName == "pnl_ZonePht")
      {
        speed = SyncroC.Utility.SyncroConverter.convertToInt(this.tnud_SpeedDoubleBacker_UVmm_m.Text, 0);
      }
      else if (parentName == "pnl_ZoneSf1")
      {
        speed = SyncroC.Utility.SyncroConverter.convertToInt(this.tnud_SpeedSF1_UVmm_m.Text, 0);
      }
      else if (parentName == "pnl_ZoneSf2")
      {
        speed = SyncroC.Utility.SyncroConverter.convertToInt(this.tnud_SpeedSF2_UVmm_m.Text, 0);
      }
      return speed;
    }

    private void ForceLocaleOrRemotePrcBoardLevelNumber_N_Click(object sender, EventArgs e)
    {
      string controlName = ((Control)sender).Name;
      string[] appo = controlName.Split('_');
      string cmd = appo[0];
      string number = appo[1];
      if (number == "A")
        number = "-1";

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp
        (
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.PRC + "." + cmd + "(" + number + ")"
        );
    }

    private void ResetUserValue_N_Click(object sender, EventArgs e)
    {
      string controlName = ((Control)sender).Name;
      string[] appo = controlName.Split('_');
      string cmd = appo[0];
      string number = appo[1];
      if (number == "A")
        number = "-1";

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp
        (
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.PRC + "." + cmd + "(" + number + ")"
        );
    }

    //RFAZZI_FI25/08/2011_2. Aggiunto controllo PRC CloseLoop ON/OFF. 
    private void ToggleAutoManCloseLoopWarpControl_Click(object sender, EventArgs e)
    {
      string controlName = ((Control)sender).Name;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.PRC + ".ToggleAutoManCloseLoopWarpControl(" + controlName + ")");

    }

    private void ResetUserValue_N_MouseDown(object sender, MouseEventArgs e)
    {
      ((PictureBox)sender).Image = global::SyncroC.Properties.Resources.cestino_premuto;
    }

    private void ResetUserValue_N_MouseUp(object sender, MouseEventArgs e)
    {
      ((PictureBox)sender).Image = global::SyncroC.Properties.Resources.cestino;
    }

    private void ResetUserValue_N_MouseLeave(object sender, EventArgs e)
    {
      ((PictureBox)sender).Image = global::SyncroC.Properties.Resources.cestino;
    }

    private void tnud_TargetSpeedDoubleBacker_UVmm_m_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, this.tnud_TargetSpeedDoubleBacker_UVmm_m.Width, this.tnud_TargetSpeedDoubleBacker_UVmm_m.Height, 5);
      Region a = new Region(path);
      this.tnud_TargetSpeedDoubleBacker_UVmm_m.Region = a;
    }

    //ABELLO_FI14/04/2011_2 Dato che l' Express400 gestisce le scarpette in modo differenziato, allora anche noi da grafica.
    private void prc_ShoesPressure1_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "ScrewJack1";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_ShoesPressure2_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "ScrewJack2";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_ShoesPressure3_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "ScrewJack3";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }

    private void prc_ShoesPressure4_OnLowLevelControlButtonPressed(object sender, SyncroC.Controls.LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        string cmd = (args.AutoMan ? "AutoManual" : "LocalRemote") + "ScrewJack4";

        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
    }
  }
}
