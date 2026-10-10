using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncroC.DataBase;
using SyncroC.Utility;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_Swap : Frm_PopUp
  {
    public Frm_Swap()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="setup">data of setup to be swapped</param>
    public DialogResult ShowDialog(IWin32Window owner, DataRow setup)
    {
      this.ShowSwap(setup);
      return this.ShowDialog(owner);
    }

    private void ShowSwap(DataRow setup)
    {
      const int MRG_BTN = 25;
      this.lab_orderNumber0.Text = setup[Order.DEL_ORDER_NUMBER_0].ToString();
      this.lab_customer0.Text = setup[Order.DEL_CUSTOMER_NAME_0].ToString();
      this.lab_measures0.Text = (this.intParse(setup[Order.DEL_SHEET_WIDTH_0].ToString()) / 10).ToString() + " x " +
                                (this.intParse(setup[Order.DEL_SHEET_LENGTH_0].ToString()) / 10).ToString() + " x " +
                                setup[Order.DEL_OUTS_0].ToString();
      this.lab_level0.Text = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + setup[Order.DEL_LEVEL_NAME_0].ToString());

      this.lab_orderNumber1.Text = setup[Order.DEL_ORDER_NUMBER_1].ToString();
      this.lab_customer1.Text = setup[Order.DEL_CUSTOMER_NAME_1].ToString();
      this.lab_measures1.Text = (this.intParse(setup[Order.DEL_SHEET_WIDTH_1].ToString()) / 10).ToString() + " x " +
                                (this.intParse(setup[Order.DEL_SHEET_LENGTH_1].ToString()) / 10).ToString() + " x " +
                                setup[Order.DEL_OUTS_1].ToString();
      this.lab_level1.Text = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + setup[Order.DEL_LEVEL_NAME_1].ToString());

      if (this.intParse(setup[Order.DEL_OUTS_2].ToString()) <= 0)
      {
        this.Width = (int)(this.Width * 0.81f);
        int margin = (this.Width - 2 * this.btn_Abort_YN.Width - MRG_BTN) / 2;
        this.btn_OK.Left = margin;
        this.btn_Abort_YN.Left = this.btn_OK.Left + this.btn_OK.Width + MRG_BTN;
        margin = (this.Width - 2 * this.grp_order0.Width - this.lbl_swap.Width) / 6;
        this.grp_order0.Left = 2*margin;
        this.lbl_swap.Left = this.grp_order0.Left + this.grp_order0.Width + margin;
        this.lbl_swap.Top = (this.grp_order0.Height - this.lbl_swap.Height) / 2 + this.grp_order0.Top;
        this.grp_order1.Left = this.lbl_swap.Left + this.lbl_swap.Width + margin;
        this.grp_order0.Top=this.grp_order1.Top=this.grp_order2.Top=(this.btn_OK.Top-this.grp_order0.Height)/2;
      }
      else
      {
        int margin = (this.Width - 2 * this.btn_Abort_YN.Width - MRG_BTN) / 2;
        this.btn_OK.Left = margin;
        this.btn_Abort_YN.Left = this.btn_OK.Left + this.btn_OK.Width + MRG_BTN;
        margin = (this.Width - 3 * this.grp_order0.Width) / 6;
        this.grp_order0.Left = margin;
        this.grp_order1.Left = this.grp_order0.Left + this.grp_order0.Width + 2*margin;
        this.grp_order2.Left = this.grp_order1.Left + this.grp_order1.Width + 2*margin;
        this.grp_order2.Top = this.grp_order1.Top = this.grp_order0.Top;
        this.lbl_shiftLeft1.Left = (this.grp_order0.Left + this.grp_order0.Width + this.grp_order1.Left) / 2 - this.lbl_shiftLeft1.Width / 2;
        this.lbl_shiftLeft2.Left = (this.grp_order1.Left + this.grp_order1.Width + this.grp_order2.Left) / 2 - this.lbl_shiftLeft2.Width / 2;
        this.lbl_shiftLeft1.Top = this.lbl_shiftLeft2.Top = (this.grp_order0.Height - this.lbl_shiftLeft1.Height) / 2 + this.grp_order0.Top;
        this.lbl_rotateRight.Left = (this.Width - this.lbl_rotateRight.Width) / 2;
        this.grp_order2.Visible = this.lbl_shiftLeft1.Visible = this.lbl_shiftLeft2.Visible = this.lbl_rotateRight.Visible = true;
        this.lbl_swap.Visible = false;
        this.lab_orderNumber2.Text = setup[Order.DEL_ORDER_NUMBER_2].ToString();
        this.lab_customer2.Text = setup[Order.DEL_CUSTOMER_NAME_2].ToString();
        this.lab_measures2.Text = (this.intParse(setup[Order.DEL_SHEET_WIDTH_2].ToString()) / 10).ToString() + " x " +
                                  (this.intParse(setup[Order.DEL_SHEET_LENGTH_2].ToString()) / 10).ToString() + " x " +
                                  setup[Order.DEL_OUTS_2].ToString();
        this.lab_level2.Text = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + setup[Order.DEL_LEVEL_NAME_2].ToString());
      }
    }

    private void btn_Abort_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }
  }
}
