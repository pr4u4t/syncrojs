using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Forms.PopUp;

namespace SyncroC.Controls.Prc
{
  /// <summary>
  /// Delegate to use for OnIncDecButtonKitDown.
  /// </summary>
  public delegate void PRCLowLevelControlAskForGraphHandler(object sender, LowLevelControlAskForGraphEventArgs args);

  public partial class PrcControl : SyncroC.Controls.LowLevelControl
  {
    /// <summary>
    /// Event raised every interval time when one of the button is down.
    /// </summary>
    [Description("Event raised when a Low Level Control component button is pressed."), Category("Fosbersoft")]
    public event LowLevelControlButtonHandler OnPRCLowLevelControlButtonPressed;

    /// <summary>
    /// Event raised every interval time when one of the button is down.
    /// </summary>
    [Description("Event raised when a PRC Low Level Control component button is pressed and the control isn't a button."), Category("Fosbersoft")]
    public event PRCLowLevelControlAskForGraphHandler OnPRCLowLevelControlAskForGraph; 

    private int warpCorrection;
    [Description("Il Warp Correction che si aggiunge al valore di Setpoint."), Category("FosberSoft")]
    public int WarpCorrection
    {
      get { return this.warpCorrection; }
      set
      {
        if (!this.Enabled)
          value = 0;

        this.warpCorrection = value;
        this.lbl_WarpCorrection.Text = value.ToString();
        if (value != 0)
          this.lbl_WarpCorrection.Visible = true;
        else
          this.lbl_WarpCorrection.Visible = false;
      }
    }

    private int userCorrection;
    [Description("Il valore che l'utente vorrebbe aggiungere al valore di Setpoint."), Category("FosberSoft")]
    public int UserCorrection
    {
      get { return this.userCorrection; }
      set
      {
        if (!this.Enabled)
          value = 0;

        this.userCorrection = value;
        this.lbl_UserOffset.Text = value.ToString();
        if (value != 0)
          this.lbl_UserOffset.Visible = true;
        else
          this.lbl_UserOffset.Visible = false;
      }
    }

    private int setpoint;
    [Description("Il Setpoint che vorremmo raggiungere."), Category("FosberSoft")]
    public int Setpoint
    {
      get { return this.setpoint; }
      set
      {
        //ABELLO_FI07/12/2010_1 [PRC] Se non trovo una curva valida non controllo il componente e lo faccio diventare rosso.
        if (value < 0)
        {
          this.lbl_setpoint.Text = "?";
        }
        else
        {
          this.setpoint = value;
          int valueToShow = value + this.userCorrection + this.warpCorrection;
          if (valueToShow > this.MaxSetPointValue)
            valueToShow = this.MaxSetPointValue;
          if (valueToShow < this.MinSetPointValue)
            valueToShow = this.MinSetPointValue;

          this.lbl_setpoint.Text = valueToShow.ToString();
        }

        //ABELLO_FI02/12/2010_1 [PRC] quando inverto il controllo con il mirror non va fatto delle scritte
        if (this.lbl_UserOffset.RightToLeft != RightToLeft.No)
          this.lbl_UserOffset.RightToLeft = RightToLeft.No;
        if (this.lbl_WarpCorrection.RightToLeft != RightToLeft.No)
          this.lbl_WarpCorrection.RightToLeft = RightToLeft.No;
        if (this.lbl_setpoint.RightToLeft != RightToLeft.No)
          this.lbl_setpoint.RightToLeft = RightToLeft.No;
      }
    }

    public int getCalculatedSetpoint()
    {
      return (this.setpoint + this.userCorrection + this.warpCorrection);
    }

    private bool isActive;
    [Description("TRUE se il controllo viene controllato dal Process Control."), Category("FosberSoft")]
    public bool IsActive
    {
      get { return isActive; }
      set 
      {
        if (this.isActive != value)
        {
          this.isActive = value;
          this.lbl_setpoint.Invalidate();
        }
      }
    }

    [Description("Gets or sets facility Low level."), Category("FosberSoft")]
    public override bool LowLevelEnabled
    {
      get { return base.LowLevelEnabled; }
      set
      {
        base.LowLevelEnabled = value;

        if (this.MyTypeOfLowLevelControl == TypeOfLowLevelControl.GlueGap ||
						this.MyTypeOfLowLevelControl == TypeOfLowLevelControl.PaperTension )
        {
          this.lbl_position.Cursor = Cursors.Hand;
          this.lbl_setpoint.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
          this.lbl_UserOffset.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
          this.lbl_WarpCorrection.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
          this.lbl_setpoint.Invalidate();
          this.lbl_UserOffset.Invalidate();
          this.lbl_WarpCorrection.Invalidate();
        }
      }
    }

    private bool isAComponentWhereIsQualityOverride = false;
    [Description("TRUE se il componente non stà seguendo la curva standard."), Category("FosberSoft")]
    public bool IsAComponentWhereIsQualityOverride
    {
      get { return isAComponentWhereIsQualityOverride; }
      set 
      {
        if (this.isAComponentWhereIsQualityOverride != value)
        {
          this.isAComponentWhereIsQualityOverride = value;
          this.lbl_setpoint.Invalidate();
        }
      }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      this.lbl_setpoint.BackColor = this.Enabled ? Color.Black : Color.Gray;
      base.OnEnabledChanged(e);
    }

    public PrcControl()
    {
      InitializeComponent();
    }

    private void PrcControl_SizeChanged(object sender, EventArgs e)
    {
      this.tableLayoutPanel.Size = new Size(this.ClientRectangle.Width - this.lbl_setpoint.Width, this.ClientRectangle.Height);
      this.lbl_setpoint.Location = new Point(this.tableLayoutPanel.Location.X + this.tableLayoutPanel.Width - 2, this.pct_LocaleRemote.Location.Y - this.lbl_setpoint.Height);

      this.btn_Graph.Location = new Point(this.tableLayoutPanel.Location.X + this.tableLayoutPanel.Width, this.lbl_setpoint.Location.Y + this.lbl_setpoint.Height + 2);
      this.btn_Graph.Size = new Size(this.lbl_setpoint.Size.Width, this.Height-this.btn_Graph.Location.Y-2);

      int LocationY = this.lbl_setpoint.Location.Y - this.lbl_UserOffset.Height - 2;
      if (LocationY < 0)
        LocationY = 0;
      this.lbl_UserOffset.Location = new Point(this.lbl_setpoint.Location.X, LocationY);

      LocationY = this.lbl_UserOffset.Location.Y - this.lbl_WarpCorrection.Height;
      if (LocationY < 0)
        LocationY = 0;
      this.lbl_WarpCorrection.Location = new Point(this.lbl_UserOffset.Location.X, LocationY);
    }

    private void lbl_setpoint_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, this.lbl_setpoint.Width, this.lbl_setpoint.Height, 5);
      Region a = new Region(path);
      this.lbl_setpoint.Region = a;

      //ABELLO_FI07/12/2010_1 [PRC] Se non trovo una curva valida non controllo il componente e lo faccio diventare rosso.
      if (this.lbl_setpoint.Text == "?")
      {
        e.Graphics.DrawPath(new Pen(Color.Red, 6), path);
      }
      else
      {
        if (this.isActive)
        {
          if (this.isAComponentWhereIsQualityOverride)
          {
            e.Graphics.DrawPath(new Pen(Color.Yellow, 6), path);
          }
          else
            e.Graphics.DrawPath(new Pen(Color.Green, 6), path);
        }
        else
        {
          e.Graphics.DrawPath(new Pen(Color.DarkGray, 6), path);
        }
      }
    }

    private void lbl_N_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, ((Control)sender).Width, ((Control)sender).Height, 5);
      Region a = new Region(path);
      ((Control)sender).Region = a;
    }

    protected override void lbl_LowLevelControl_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        if (this.OnPRCLowLevelControlAskForGraph != null)
          OnPRCLowLevelControlAskForGraph(this, new LowLevelControlAskForGraphEventArgs(false, false, 0, this.lbl_Name.Text, this.userCorrection, this.warpCorrection));
        return;
      }

      //ABELLO_FI07-07-08_1 se sono in remoto e c'è il PRC non posso usare i setpoint
      // SE c'è il process control (NON SUL CLIENT, MA SU SYNCRO)
      if (DBManager.getDBManager().lineConfig.PRC)
        // SE SONO IN REMOTO
        if (this.InLocal)
        {
          if(this.MyTypeOfLowLevelControl == TypeOfLowLevelControl.GlueGap ||
							this.MyTypeOfLowLevelControl == TypeOfLowLevelControl.PaperTension )
            base.lbl_LowLevelControl_Click(this, e);
          // ALLORA NO SET POINT 
          return;
        }

      Frm_PRCAskValue askValue = new Frm_PRCAskValue();
      //ABELLO_FI30/10/2008_1 Messi i pulsanti Inc/Dec sulle PopUp che chiedono un valore numerico.
      FosberDialogResult ret = askValue.ShowDialog(this, this.setpoint, this.warpCorrection, this.userCorrection, this.getCalculatedSetpoint(), this.MinSetPointValue, this.MaxSetPointValue);

      if (ret.Dialog == DialogResult.OK)
      {
        int resUserCorrection = (int)ret.Data[0];
        int resSetpoint = (int)ret.Data[1];

        if ((resSetpoint < this.MinSetPointValue) || (resSetpoint > this.MaxSetPointValue))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_06");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          resSetpoint = Int32.MinValue;
        }

        if (resSetpoint > Int32.MinValue)
        {
          if (resUserCorrection != this.userCorrection)
            resSetpoint = resUserCorrection;
          else
            resSetpoint = this.userCorrection + (resSetpoint - this.getCalculatedSetpoint());

          if (this.OnPRCLowLevelControlButtonPressed != null)
            OnPRCLowLevelControlButtonPressed(this, new LowLevelControlButtonEventArgs(false, false, resSetpoint, this.lbl_Name.Text));
        }
      }
    }

    protected override void pct_LocaleRemoteAndAutoMan_Paint(object sender, PaintEventArgs e)
    {
      base.pct_LocaleRemoteAndAutoMan_Paint(sender, e);
      if (this.lowLevelEnabled)
      {
        int penWidth = 1;
        Pen pen = new Pen(Color.Black, penWidth);
        if (((PictureBox)sender).Name.Contains("Remote"))
        {
          e.Graphics.DrawLine(pen, new Point(((PictureBox)sender).Width - penWidth, 0), new Point(((PictureBox)sender).Width - penWidth, ((PictureBox)sender).Height - penWidth));
        }
      }
    }

    private void btn_Graph_Click(object sender, EventArgs e)
    {
      if (this.OnPRCLowLevelControlAskForGraph != null)
        OnPRCLowLevelControlAskForGraph(this, new LowLevelControlAskForGraphEventArgs(false, false, 0, this.lbl_Name.Text, this.userCorrection, this.warpCorrection));

      return;
    }
  }

  /// <summary>
  /// Summary description for LowLevelControlAskForGraphEventArgs.
  /// </summary>
  public class LowLevelControlAskForGraphEventArgs : LowLevelControlButtonEventArgs
  {
    private int userCorrection;
    public int UserCorrection
    {
      get { return userCorrection; }
    }

    private int warpCorrection;
    public int WarpCorrection
    {
      get { return warpCorrection; }
    }

    public LowLevelControlAskForGraphEventArgs(bool theAutoMan, bool theLocRem, int theSetPoint, string theControlName, int theUserCorrection, int theWarpCorrection)
      : base(theAutoMan, theLocRem, theSetPoint, theControlName)
    {
      this.userCorrection = theUserCorrection;
      this.warpCorrection = theWarpCorrection;
    }
  }
}
