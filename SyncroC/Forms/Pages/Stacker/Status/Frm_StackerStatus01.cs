using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.Pages.Stacker.Status
{
  public partial class Frm_StackerStatus01 : SyncroC.Forms.Pages.Stacker.Status.Frm_StackerStatusSubgroup
  {
    private bool showDebugOptions = false;
    private string typeFlute = "";

    //private DataSet setups;
    //private DataTable quality;
    //private DataSet pastSetups;

    private string setupNumber = "";
    private string orderNumber = "";
    private string cutLenght = "";
    private string sheetsQuantity = "";

    public Frm_StackerStatus01()
    {
      InitializeComponent();
      this.pct_Stacker_Frame.SendToBack();

      //this.pastSetups = new DataSet();

      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.SendCurrentOrder.IsButton = false;
        this.SendNextOrder.IsButton = false;
      }
    }

    /// <summary>
		/// Builder. Build an istance of this page for the specified Stacker.
		/// </summary>
		/// <param name="index">Index of the stacker represented on the current instance of this Form</param>
		public Frm_StackerStatus01(int index) : this()
		{
			if (! (index == 1 || index == 2 || index == 3)) 
				Logger.LogError(this, "Frm_StackerStatus - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;

			lbl_Title_YN.Text += " " + index.ToString();
		}

    private void F_SetMachineValue(DataRow setups, string dryEndOrderList, int setupId, int modId, int index)
    {
      //string search;

      //quality = DBManager.getDBManager().quality.getTable();

      //agrassi_FI25-08-08_4
      //search = "SetupID = '" + setupId.ToString() + "'";
      //serch = "SetupID = '" + setupId.ToString() + "' AND ModID = '" + modId.ToString() + "'";
      //this.setupNumber = this.setupNumber + setups.Tables[dryEndOrderList].Select(search)[0]["SetupCode"].ToString();

      this.setupNumber = this.setupNumber + setups["SetupCode"].ToString();

      //search = "QualityCode = '" + setups.Tables[dryEndOrderList].Select(search)[0]["QualityID"].ToString() + "'";
      //if (quality.Select(search).Length > 0)
      //  typeFlute = quality.Select(search)[0][SyncroC.DataBase.Quality.FLUTE_ID].ToString();

      DataRow rowQuality = DBManager.getDBManager().quality.getRow(setups["QualityID"].ToString());
      if(rowQuality != null)
        this.typeFlute = rowQuality[SyncroC.DataBase.Quality.FLUTE_ID].ToString();

      if (setups["LevelName0"].ToString() == level.ToString())
      {
        this.orderNumber = setups["OrderNumber0"].ToString();
        this.cutLenght = SyncroConverter.ConvertString(setups["SheetLength0"].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        this.sheetsQuantity = setups["SheetsPerStack0"].ToString();
      }
      else if (setups["LevelName1"].ToString() == level.ToString())
      {
        this.orderNumber = setups["OrderNumber1"].ToString();
        this.cutLenght = SyncroConverter.ConvertString(setups["SheetLength1"].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        this.sheetsQuantity = setups["SheetsPerStack1"].ToString();
      }
      else if (setups["LevelName2"].ToString() == level.ToString())
      {
        this.orderNumber = setups["OrderNumber2"].ToString();
        this.cutLenght = SyncroConverter.ConvertString(setups["SheetLength2"].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        this.sheetsQuantity = setups["SheetsPerStack2"].ToString();
      }

      /*
      //agrassi_FI25-08-08_4
      //search = "SetupID = '" + setupId.ToString() + "' AND ModID = '" + modId.ToString() + "' AND LevelName0 = '" + level.ToString() + "'";
      search = "SetupID = '" + setupId.ToString() + "' AND LevelName0 = '" + level.ToString() + "'";
      if (setups.Tables[dryEndOrderList].Select(search).Length > 0)
      {
        orderNumber = setups.Tables[dryEndOrderList].Select(search)[0]["OrderNumber0"].ToString();
        cutLenght = SyncroConverter.ConvertString(setups.Tables[dryEndOrderList].Select(search)[0]["SheetLength0"].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        sheetsQuantity = setups.Tables[dryEndOrderList].Select(search)[0]["SheetsPerStack0"].ToString();
      }
      else
      {
        //agrassi_FI25-08-08_4
        search = "SetupID = '" + setupId.ToString() + "'";
        //serch = "SetupID = '" + setupId.ToString() + "' AND ModID = '" + modId.ToString() + "'";
        if (setups.Tables[dryEndOrderList].Select(search).Length > 0)
        {
          orderNumber = setups.Tables[dryEndOrderList].Select(search)[0]["OrderNumber1"].ToString();
          cutLenght = SyncroConverter.ConvertString(setups.Tables[dryEndOrderList].Select(search)[0]["SheetLength1"].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
          sheetsQuantity = setups.Tables[dryEndOrderList].Select(search)[0]["SheetsPerStack1"].ToString();
        }
        else
        {
          //agrassi_FI25-08-08_4
          search = "SetupID = '" + setupId.ToString() + "'";
          //serch = "SetupID = '" + setupId.ToString() + "' AND ModID = '" + modId.ToString() + "'";
          if (setups.Tables[dryEndOrderList].Select(search).Length > 0)
          {
            orderNumber = setups.Tables[dryEndOrderList].Select(search)[0]["OrderNumber2"].ToString();
            cutLenght = SyncroConverter.ConvertString(setups.Tables[dryEndOrderList].Select(search)[0]["SheetLength2"].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
            sheetsQuantity = setups.Tables[dryEndOrderList].Select(search)[0]["SheetsPerStack2"].ToString();
          }
        }
      }
       * */
    }

    private void _setCurrentSetup(int setupId, int modId)
    {
      //string search = "";

      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      //setups = DBManager.getDBManager().order.order;

      //pastSetups.Tables.Clear();
      //pastSetups.Tables.Add(DBManager.getDBManager().pastOrder.getTable());

      this.setupNumber = msg.getTranslatedMessage("§SYCC_34");
      this.orderNumber = "";
      this.cutLenght = "";
      this.sheetsQuantity = "";
      this.typeFlute = "";

      if (setupId != 0)
      {
        //agrassi_FI25-08-08_4
        //search = "SetupID = '" + setupId.ToString() + "'";
        //search = "SetupID = '" + setupId.ToString() + "' AND ModID = '" + modId.ToString() + "'";
        //if (setups.Tables[SyncroC.DataBase.Order.DRYEND_LIST].Select(search).Length > 0)
        //  F_SetMachineValue(setups, "DryEndList", setupId, modId, index);
        //else if (pastSetups.Tables[SyncroC.DataBase.PastOrder.PAST_ORDER_LIST].Select(search).Length > 0)
        //  F_SetMachineValue(pastSetups, "PastOrderList", setupId, modId, index);

        DataRow row = DBManager.getDBManager().order.getRow(setupId.ToString().Trim());
        if (row != null)
          F_SetMachineValue(row, "DryEndList", setupId, modId, index);
        else
        {
          row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString().Trim());
          if (row != null)
            F_SetMachineValue(row, "PastOrderList", setupId, modId, index);
        } 
      }

      this.ttl_CurrentSetup.Text = this.setupNumber;
      this.ttl_CurrentOrder.Text = this.orderNumber;
      this.tnud_CurrentLenght.Value = this.cutLenght;
      this.ttb_CurrentSheetsXStack.Value = this.sheetsQuantity;
      this.ttb_CurrentFlute.Value = this.typeFlute;
      //agrassi_FI25-08-08_4
      this.lbl_currId.Text = setupId.ToString();
    }

    private void _setNextSetup(int setupId, int modId)
    {
      //string search = "";
      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      //setups = DBManager.getDBManager().order.order;

      //pastSetups.Tables.Clear();
      //pastSetups.Tables.Add(DBManager.getDBManager().pastOrder.getTable());

      this.setupNumber = msg.getTranslatedMessage("§SYCC_35");
      this.orderNumber = "";
      this.cutLenght = "";
      this.sheetsQuantity = "";
      this.typeFlute = "";

      if (setupId != 0)
      {
        //agrassi_FI25-08-08_4
        //search = "SetupID = '" + setupId.ToString() + "'";
        //search = "SetupID = '" + setupId.ToString() + "' AND ModID = '" + modId.ToString() + "'";
        //if (setups.Tables[SyncroC.DataBase.Order.DRYEND_LIST].Select(search).Length > 0)
        //  F_SetMachineValue(setups, "DryEndList", setupId, modId, index);
        //else if (pastSetups.Tables[SyncroC.DataBase.PastOrder.PAST_ORDER_LIST].Select(search).Length > 0)
        //  F_SetMachineValue(pastSetups, "PastOrderList", setupId, modId, index);

        DataRow row = DBManager.getDBManager().order.getRow(setupId.ToString().Trim());
        if (row != null)
          F_SetMachineValue(row, "DryEndList", setupId, modId, index);
        else
        {
          row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString().Trim());
          if (row != null)
            F_SetMachineValue(row, "PastOrderList", setupId, modId, index);
        } 
      }

      this.ttl_NextSetup.Text = this.setupNumber;
      this.ttl_NextOrder.Text = this.orderNumber;
      this.tnud_NextLenght.Value = this.cutLenght;
      this.ttb_NextSheetsXStack.Value = this.sheetsQuantity;
      this.ttb_NextFlute.Value = this.typeFlute;
      //agrassi_FI25-08-08_4
      this.lbl_nextId.Text = setupId.ToString();
    }

    protected override void F3()
    {
    }

    protected override void ControlD()
    {
      showDebugOptions = !showDebugOptions;

      ttl_IntrusionDetection.Visible = btn_IntrusionDetection.Visible = ttl_BrushStatus.Visible =
        btn_brush1.Visible = btn_brush2.Visible = btn_brush3.Visible = btn_brush4.Visible = btn_brush5.Visible =
        tnud_brushGroup1Pressure.Visible = tnud_brushGroup2Pressure.Visible = 
        tnud_BackstopPosition.Visible = tnud_PlatformPosition.Visible =
        //agrassi_FI25-08-08_3
        lbl_currId.Visible=lbl_nextId.Visible=
            showDebugOptions;
    }

    public override void CorrugatorDirectionLeft()
    {
      int leftLimit = pct_Stacker_Frame.Location.X;
      int rightLimit = pct_Stacker_Frame.Location.X + pct_Stacker_Frame.Width;

      //stacker frame
      pct_Stacker_Frame.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

      //brushes
      leftLimit = btn_brush1.Location.X;
      rightLimit = btn_brush5.Location.X + btn_brush5.Width;

      HorizontalLocationMirror(btn_brush1, leftLimit, rightLimit);
      btn_brush1.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(btn_brush2, leftLimit, rightLimit);
      btn_brush2.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(btn_brush3, leftLimit, rightLimit);
      btn_brush3.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(btn_brush4, leftLimit, rightLimit);
      btn_brush4.RotateFlip(RotateFlipType.RotateNoneFlipX);
      HorizontalLocationMirror(btn_brush5, leftLimit, rightLimit);
      btn_brush5.RotateFlip(RotateFlipType.RotateNoneFlipX);

      //current & next order
      leftLimit = pnl_Next.Location.X;
      rightLimit = pnl_current.Location.X + pnl_current.Width;

      HorizontalLocationMirror(pnl_Next, leftLimit, rightLimit);
      HorizontalLocationMirror(pnl_current, leftLimit, rightLimit);
    }

    //PMASSARO_FI02/09/10 - Aggiunto controllo sulla possibilità di inviare gli ordini allo stacker (da grafica)
    private bool isManualOrderSendingDisabledForThisMachine(int numberOfOrderToSend)
    {
      //numberOfOrderToSend può essere utilizzato per successivi controlli relativi ad invio ordine corrente, successivo, ....
      bool disabledFunctionality = false;
      switch (level)
      {
        case LevelName.Lower:
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 1)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings1.Enable; }          
          break;
        case LevelName.Middle:
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 3)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings2.Enable; }          
          break;
        case LevelName.Upper:
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 2)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings2.Enable; }
          else if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 3)
          { disabledFunctionality = SyncroC.DBManager.getDBManager().syncroConfig.FanFoldHoettenCommsSettings3.Enable; }
          break;
      }

      return (disabledFunctionality);
    }

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

      //PMASSARO_FI02/09/10 - Aggiunto controllo sulla possibilità di inviare gli ordini allo stacker (da grafica)
      int numberOfOrderToSend = 1;
      if (sender.ToString().Contains("SendNextOrder"))
        numberOfOrderToSend = 2;
      if (isManualOrderSendingDisabledForThisMachine(numberOfOrderToSend))
      {
        PageManager.getPageManager()._SetMessage("§SYCC_201");
        return;
      }

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.STACKER + index.ToString() + "." + ((Control)sender).Name + "()");
		}

		private void Frm_StackerStatus_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
		}

	}
}