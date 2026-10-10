using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using SyncroC.Network;
using SyncroC.Controls;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Twin400.Orders
{
	/// <summary>
	/// Form where sub group of twin400 orders pages inherith from.
	/// </summary>
	public class Frm_twin400_ordersSubgroup : SyncroC.Forms.Pages.Twin400.Frm_Twin400Group
	{
    protected delegate string ConversionFunction(string value);
    public static int externIndex = 0;
    protected int selectedIndex = -1;
    protected Type ordType;
    protected SyncroC.DataBase.Interfaces.IDatabase ordInst;
    protected string orderDbName, orderDbAcronym;
    protected DataSet orderDataSet;
    private System.ComponentModel.IContainer components = null;

    public Frm_twin400_ordersSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageTwin400_Orders) for the Twin400_Orders subgroup is set here.
		/// </summary>
		public override void _Show()
		{		
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageTwin400_Orders = this.getRequestCommand();
			base._Show();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void F7()
		//protected override void F6()
		protected override void F7()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_twin400_ordersList));
		}

		protected override void F11()
		{
      Frm_twin400_orderDetails.externState = Frm_twin400_orderDetails.State.Read;
      Frm_twin400_orderDetails.externPast = false;
      askForPage(typeof(Frm_twin400_orderDetails));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_twin400_pastOrders));
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_ordersSubgroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "TWIN 400 SETUPS";
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab7.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Queue";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "Past";
      this.tab_TabC.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Details";
      this.tab_TabB.Visible = true;
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
      this.iml_navigationImages.Images.SetKeyName(0, "");
      this.iml_navigationImages.Images.SetKeyName(1, "");
      this.iml_navigationImages.Images.SetKeyName(2, "");
      this.iml_navigationImages.Images.SetKeyName(3, "");
      // 
      // btn_Group1
      // 
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // Frm_twin400_ordersSubgroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_twin400_ordersSubgroup";
      this.Text = "Frm_twin400_ordersSubgroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    protected void SetTtlValue(DataRow setup, string ttl, string item, int ord, int ttlInd, ConversionFunction cf)
    {
      if ((ord < 0) || (ord >= 3) || (ttlInd < 0) || (ttlInd >= 3)) return;
      string val = "";
      Type frmType = this.GetType();
      if ((setup != null) && (!item.Equals(string.Empty)))
      {
        string s = ordType.GetField(item + ord, BindingFlags.Public | BindingFlags.IgnoreCase |
          BindingFlags.Static).GetValue(ordInst).ToString();
        val = setup[s].ToString().Trim();
        if (cf != null)
          //CM3 27-08-2005
          val = cf(val);
      }
      ((TitledLabel)frmType.GetField(ttl + ttlInd, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this)).Value = val;
    }

    protected string GetSetupValue(DataRow setup, string item, int ind)
    {
      object o=this.GetSetupObject(setup, item, ind);
      if (o!=null)
        return o.ToString();
      return string.Empty;
    }

    protected object GetSetupObject(DataRow setup, string item, int ind)
    {
      if ((ind < 0) || (ind >= 3)) return "";
      string indStr = ordType.GetField(item + ind, BindingFlags.Public | BindingFlags.IgnoreCase |
        BindingFlags.Static).GetValue(ordInst).ToString();
      if (!indStr.Equals(string.Empty))
      {
        object o = null;
        try
        {
          o = setup[indStr];
        }
        catch (Exception e)
        {
          Logger.LogError(this, e);
        }
        return o;
      }
      return null;
    }

    protected void FillOrders()
    {
      if (this.selectedIndex >= 0)
      {
        int[] levelOrd = new int[3];
        int ord = 0;
        DataRow setup = null;

        if (this.orderDbName == SyncroC.DataBase.PastOrder.PAST_ORDER_LIST)
        {
          if (DBManager.getDBManager().pastOrder.getCount() > this.selectedIndex)
            setup = DBManager.getDBManager().pastOrder.getRow(this.selectedIndex);
          else
            return;
        }
        else
        {
          DataRowCollection setupCollection = this.orderDataSet.Tables[this.orderDbName].Rows;
          if (setupCollection.Count > this.selectedIndex)
            setup = this.orderDataSet.Tables[this.orderDbName].Rows[this.selectedIndex];
          else
            return;
        }

        while (intParse(this.GetSetupValue(setup, this.orderDbAcronym + "_OUTS_", ord)) > 0)
        {
          LevelName lev = (LevelName)Enum.Parse(typeof(LevelName), this.GetSetupValue(setup, this.orderDbAcronym + "_LEVEL_NAME_", ord++));
          if ((lev > LevelName.Nolevel) && (lev <= LevelName.Upper))
          {
            while (levelOrd[(int)lev - 1] > 0)
            {
              if (lev == LevelName.Lower)
                lev++;
              else if (lev == LevelName.Upper)
                lev--;
              else
              {
                lev++;   //TODO:  gestire il livello Medio per bene!!!
              }
            }
            levelOrd[(int)lev - 1] = ord;
          }
        }
        int cnt = 0;
        Type frmType = this.GetType();
        for (int i = 1; i <= 3; i++)
          for (int j = 1; j <= 3; j++)
            ((Label)frmType.GetField("lbl_Pos" + i + j, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this)).BackColor = Color.WhiteSmoke;

        for (LevelName levIdx = LevelName.Upper; levIdx >= LevelName.Lower; levIdx--)
        {
          if (levelOrd[(int)levIdx-1] <= 0) continue;
          ord = levelOrd[(int)levIdx-1] - 1;
          this.SetTtlValue(setup, "ttl_Level", this.orderDbAcronym + "_LEVEL_NAME_", ord, cnt, this.TranslateLevelName);
          this.SetTtlValue(setup, "ttl_OrderNumber", this.orderDbAcronym + "_ORDER_NUMBER_", ord, cnt, null);
          this.SetTtlValue(setup, "ttl_Customer", this.orderDbAcronym + "_CUSTOMER_NAME_", ord, cnt, null);
          this.SetTtlValue(setup, "ttl_Outs", this.orderDbAcronym + "_OUTS_", ord, cnt, null);
          this.SetTtlValue(setup, "ttl_Width", this.orderDbAcronym + "_SHEET_WIDTH_", ord, cnt, this.ConvertFromTenthToMM);
          this.SetTtlValue(setup, "ttl_ScoreMeasures", this.orderDbAcronym + "_SCORE_MEASURES_", ord, cnt, this.ConvertScoresFromTenthToMM);
          this.SetTtlValue(setup, "ttl_Score", this.orderDbAcronym + "_SCORE_TYPE_", ord, cnt, null);
          this.SetTtlValue(setup, "ttl_Positioning", this.orderDbAcronym + "_POSIT_TYPE_", ord, cnt, this.TranslatePositType);
          this.SetTtlValue(setup, "ttl_Gap", this.orderDbAcronym + "_SCORE_GAP_", ord, cnt, null);
          ((Label)frmType.GetField("lbl_Pos" + (cnt + 1).ToString() + (ord + 1).ToString(), 
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this)).BackColor = Color.YellowGreen;
          cnt++;
        }
        for (int i = 1; i <= 3; i++)
          for (int j = 1; j <= 3; j++)
            ((Label)frmType.GetField("lbl_Pos" + i + j, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this)).Visible = ((j <= cnt) && (i <= cnt));
        for (int ordIdx = cnt; ordIdx < 3; ordIdx++)
        {
          this.SetTtlValue(null, "ttl_Level", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_OrderNumber", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_Customer", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_Outs", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_Width", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_ScoreMeasures", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_Score", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_Positioning", "", 0, ordIdx, null);
          this.SetTtlValue(null, "ttl_Gap", "", 0, ordIdx, null);
        }
      }
    }

    protected virtual void grd_ordersList_DoubleClick(object sender, EventArgs e)
    {
      //do not remove it!
    }

    protected virtual void grd_ordersList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      //do not remove it!
    }

    private string ConvertFromTenthToMM(string value)
    {
      return SyncroConverter.ConvertString(value, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
    }

    private string ConvertScoresFromTenthToMM(string value)
    {
      return SyncroConverter.ConvertScoreMeasures(value, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
    }

    private string TranslateLevelName(string value)
    {
      return DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + value);
    }

    private string TranslatePositType(string value)
    {
      return DBManager.getDBManager().language.getTranslation(typeof(PositType).Name + "." + value);
    }
  }
}

