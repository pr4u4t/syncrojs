using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Forms.PopUp;

namespace SyncroC.Controls
{
  /// <summary>
  /// Delegate to use for GlueGapButtonHandler.
  /// </summary>
  public delegate void LowLevelControlButtonHandler(object sender, LowLevelControlButtonEventArgs args);

  public partial class LowLevelControl : SyncroC.Controls.SyncroUserControl
  {
    public enum TypeOfLowLevelControl 
    { 
      GlueGap, 
      PaperTension, 
      SteamShower, 
      DBHotSection, 
      DBColdSection, 
      DBSteam,
    };

    private TypeOfLowLevelControl myTypeOfLowLevelControl = TypeOfLowLevelControl.GlueGap;
    [Description("Type Of Low Level Control."), Category("FosberSoft")]
    public TypeOfLowLevelControl MyTypeOfLowLevelControl
    {
      get { return myTypeOfLowLevelControl; }
      set 
      { 
        myTypeOfLowLevelControl = value;
        switch (value)
        {
          case TypeOfLowLevelControl.GlueGap:
            this.lbl_position.BackgroundImage = SyncroC.Properties.Resources.GLUE_GAP;
            this.lbl_position.TextAlign = ContentAlignment.TopLeft;
            this.lbl_position.Colore = Color.Black;
            this.lbl_position.BackcolorTesto = Color.Transparent;
            this.lbl_position.OutLineText = false;
            break;
          case TypeOfLowLevelControl.PaperTension:
            this.lbl_position.BackgroundImage = SyncroC.Properties.Resources.PaperTension;
            this.lbl_position.TextAlign = ContentAlignment.TopRight;
            this.lbl_position.Colore = Color.Black;
            this.lbl_position.BackcolorTesto = Color.Transparent;
            this.lbl_position.OutLineText = false;
            break;
          case TypeOfLowLevelControl.SteamShower:
            this.lbl_position.BackgroundImage = SyncroC.Properties.Resources.STEAM_SHOWER;
            this.lbl_position.TextAlign = ContentAlignment.TopRight;
            this.lbl_position.Colore = Color.Black;
            this.lbl_position.BackcolorTesto = Color.Transparent;
            this.lbl_position.OutLineText = false;
            break;
          case TypeOfLowLevelControl.DBHotSection:
            this.lbl_position.BackgroundImage = SyncroC.Properties.Resources.shoes;
            this.lbl_position.TextAlign = ContentAlignment.TopCenter;
            this.lbl_position.Colore = Color.Black;
            this.lbl_position.BackcolorTesto = Color.Transparent;
            this.lbl_position.OutLineText = false;
            break;
          case TypeOfLowLevelControl.DBColdSection:
            this.lbl_position.BackgroundImage = SyncroC.Properties.Resources.shoes;
            this.lbl_position.TextAlign = ContentAlignment.TopCenter;
            this.lbl_position.Colore = Color.Black;
            this.lbl_position.BackcolorTesto = Color.Transparent;
            this.lbl_position.OutLineText = false;
            break;
          case TypeOfLowLevelControl.DBSteam:
            this.lbl_position.BackgroundImage = SyncroC.Properties.Resources.MAIN_STEAM;
            this.lbl_position.TextAlign = ContentAlignment.TopCenter;
            this.lbl_position.Colore = Color.Black;
            this.lbl_position.BackcolorTesto = this.BackColor;
            this.lbl_position.OutLineText = false;
            break;
        }
      }
    }

    /// <summary>
    /// Event raised every interval time when one of the button is down.
    /// </summary>
    [Description("Event raised when a Low Level Control component button is pressed."), Category("Fosbersoft")]
    public event LowLevelControlButtonHandler OnLowLevelControlButtonPressed;

    /// <summary>Determine if the wrap arm Is Auto or Manual controlled.</summary>
    private bool auto = false;
    /// <summary>
    /// Gets or sets the Auto property.
    /// </summary>
    [Description("If true the Glue Gap Auto."), Category("FosberSoft")]
    public bool Auto
    {
      get { return auto; }
      set
      {
        if (this.auto != value)
        {
          this.auto = value;
          this.pct_AutoMan.Image = (auto) ? iml_AutoManImages.Images[0] : iml_AutoManImages.Images[1];
        }
      }
    }

    /// <summary>Determine if the wrap arm Is in local or remote.</summary>
    private bool inLocal = false;
    /// <summary>
    /// Gets or sets the IsUnder property.
    /// </summary>
    [Description("If true the Glue Gap is in locale."), Category("FosberSoft")]
    public bool InLocal
    {
      get { return this.inLocal; }
      set
      {
        if (this.inLocal != value)
        {
          this.inLocal = value;
          this.pct_LocaleRemote.Image = (this.inLocal) ? this.iml_LocaleRemoteImages.Images[0] : this.iml_LocaleRemoteImages.Images[1];
        }
      }
    }

    /// <summary>Determine if the wrap arm Is at MAX position.</summary>
    private bool isMaxPos = false;
    /// <summary>
    /// Gets or sets the IsMaxPos property.
    /// </summary>
    [Description("If true the Glue Gap is at MAX Position."), Category("FosberSoft")]
    public bool IsMaxPos
    {
      get { return isMaxPos; }
      set
      {
        if (isMaxPos != value)
        {
          isMaxPos = value;

          if (value)
            this.lbl_Name.BackColor = Color.Red;
          else
            this.lbl_Name.BackColor = Color.WhiteSmoke;
        }
      }
    }

    /// <summary>Determine if the wrap arm Is at MAX position.</summary>
    private bool isMinPos = false;
    /// <summary>
    /// Gets or sets the IsMaxPos property.
    /// </summary>
    [Description("If true the Glue Gap is at MIN Position."), Category("FosberSoft")]
    public bool IsMinPos
    {
      get { return isMinPos; }
      set
      {
        if (isMinPos != value)
        {
          isMinPos = value;

          if (value)
            this.lbl_Name.BackColor = Color.Orange;
          else
            this.lbl_Name.BackColor = Color.WhiteSmoke;
        }
      }
    }

    /// <summary>
    /// Gets or sets the position property.
    /// </summary>
    [Category("FosberSoft")]
    public string Position
    {
      get { return lbl_position.Testo; }
      set
      {
        if(lbl_position.Testo != value)
          lbl_position.Testo = value;
      }
    }

    /// <summary>
    /// Gets or sets the position property.
    /// </summary>
    [Category("FosberSoft")]
    public string LowLevelControlName
    {
      get { return this.lbl_Name.Text; }
      set { this.lbl_Name.Text = value; }
    }

    private int maxSetPointValue;
    [Category("FosberSoft")]
    public int MaxSetPointValue
    {
      get { return this.maxSetPointValue; }
      set { this.maxSetPointValue = value; }
    }

    private int minSetPointValue;
    [Category("FosberSoft")]
    public int MinSetPointValue
    {
      get { return this.minSetPointValue; }
      set { this.minSetPointValue = value; }
    }

    /// <summary>
    /// Gets or sets facility Low level.
    /// </summary>
    protected bool lowLevelEnabled;
    [Description("Gets or sets facility Low level."), Category("FosberSoft")]
    public virtual bool LowLevelEnabled
    {
      get { return this.lowLevelEnabled; }
      set
      {
        this.lowLevelEnabled = value;
        this.pct_AutoMan.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
        this.pct_LocaleRemote.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;

        if (this.MyTypeOfLowLevelControl == TypeOfLowLevelControl.GlueGap)
        {
          this.lbl_position.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
        }
      }
    }

    private float originalPositionFontSize = 20.0F;
    private float originalNameFontSize = 20.0F;

    public LowLevelControl()
    {
      this.myInitializeComponent();

      this.minSetPointValue = 0;
      this.maxSetPointValue = 100;
    }

    public void setDesignModePage(bool designModePage)
    {
      if (!designModePage)
      {
        //ABELLO_FI25/11/2010_1 [PRC] Il locale/Remoto non si deve vedere se non c'è il PRC
        //if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        if(!DBManager.getDBManager().lineConfig.PRC)
          this.pct_LocaleRemote.Visible = false;
      }
    }

    private void myInitializeComponent()
    {
      InitializeComponent();

      this.originalPositionFontSize = this.lbl_position.Font.Size;
      this.originalNameFontSize = this.lbl_Name.Font.Size;

      this.lbl_position.BackgroundImageLayout = ImageLayout.Stretch;
      this.lbl_position.TextAlign = ContentAlignment.TopLeft;

      this.pct_AutoMan.Image = (this.auto) ? this.iml_AutoManImages.Images[0] : this.iml_AutoManImages.Images[1];
      this.pct_LocaleRemote.Image = (this.inLocal) ? this.iml_LocaleRemoteImages.Images[0] : this.iml_LocaleRemoteImages.Images[1];

      this.lowLevelEnabled = true;
    }

    private bool isClikkedAutoMan = false;
    private void pct_AutoMan_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.lowLevelEnabled)
        return;

      this.isClikkedAutoMan = true;
      this.pct_AutoMan.Image = (this.auto) ? this.iml_AutoManImages.Images[2] : this.iml_AutoManImages.Images[3];
    }

    private void pct_AutoMan_Click(object sender, EventArgs e)
    {
      if (!this.lowLevelEnabled)
        return;

      if (this.OnLowLevelControlButtonPressed != null)
        OnLowLevelControlButtonPressed(this, new LowLevelControlButtonEventArgs(true, false, 0, this.lbl_Name.Text));
    }

    private void pct_AutoMan_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.lowLevelEnabled)
        return;

      if (this.isClikkedAutoMan)
      {
        this.isClikkedAutoMan = false;
        this.pct_AutoMan.Image = (this.auto) ? this.iml_AutoManImages.Images[0] : this.iml_AutoManImages.Images[1];
      }
    }

    private bool isClikkedLocRem = false;
    private void pct_LocaleRemote_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.lowLevelEnabled)
        return;

      this.isClikkedLocRem = true;
      this.pct_LocaleRemote.Image = (this.inLocal) ? this.iml_LocaleRemoteImages.Images[2] : this.iml_LocaleRemoteImages.Images[3];
    }

    private void pct_LocaleRemote_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      if (!this.lowLevelEnabled)
        return;

      if (this.OnLowLevelControlButtonPressed != null)
        OnLowLevelControlButtonPressed(this, new LowLevelControlButtonEventArgs(false, true, 0, this.lbl_Name.Text));
    }

    private void pct_LocaleRemote_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.lowLevelEnabled)
        return;

      if (this.isClikkedLocRem)
      {
        this.isClikkedLocRem = false;
        this.pct_LocaleRemote.Image = (this.inLocal) ? this.iml_LocaleRemoteImages.Images[0] : this.iml_LocaleRemoteImages.Images[1];
      }
    }

    protected virtual void lbl_LowLevelControl_Click(object sender, EventArgs e)
    {
      if (!(sender is SyncroC.Controls.Prc.PrcControl))
      {
        if (!this.lowLevelEnabled)
          return;
      }

      //ABELLO_FI07-07-08_1 se sono in remoto e c'è il PRC non posso usare i setpoint
      // SE c'è il process control (NON SUL CLIENT, MA SU SYNCRO)
      if (DBManager.getDBManager().lineConfig.PRC)
      {
        // SE SONO IN REMOTO
        if (!this.inLocal)
        {
          PageManager.getPageManager()._SetMessage("§RTC_15", true);
          // ALLORA NO SET POINT 
          return;
        }
      }

      int pos = 0;
      try
      {
        pos = Convert.ToInt32(this.lbl_position.Testo);
      }
      catch (Exception)
      {
        pos = 0;
      }

      Frm_AskValue askValue = new Frm_AskValue();
      //ABELLO_FI30/10/2008_1 Messi i pulsanti Inc/Dec sulle PopUp che chiedono un valore numerico.
      FosberDialogResult ret = askValue.ShowDialog(this, "SetPoint", pos, this.minSetPointValue, this.maxSetPointValue);

      if (ret.Dialog == DialogResult.OK)
      {
        int res = (int)ret.Data[0];
        if ((res < this.minSetPointValue) || (res > this.maxSetPointValue))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_06");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          res = -1;
        }

        if (res >= 0)
          if (this.OnLowLevelControlButtonPressed != null)
            OnLowLevelControlButtonPressed(this, new LowLevelControlButtonEventArgs(false, false, res, this.lbl_Name.Text));
      }
    }

    protected void LowLevelControl_SizeChanged(object sender, EventArgs e)
    {
      if ((this.Width <= 100) || (this.Height <= 100))
      {
        this.lbl_position.Font = new Font(this.lbl_position.Font.Name, this.originalPositionFontSize - 4);
        this.lbl_Name.Font = new Font(this.lbl_Name.Font.Name, this.originalNameFontSize - 4);
      }
      else
      {
        this.lbl_position.Font = new Font(this.lbl_position.Font.Name, this.originalPositionFontSize + 4);
        this.lbl_Name.Font = new Font(this.lbl_position.Font.Name, this.originalNameFontSize + 4);
      }
    }

    protected virtual void pct_LocaleRemoteAndAutoMan_Paint(object sender, PaintEventArgs e)
    {
      if (this.lowLevelEnabled)
      {
        int penWidth = 1;
        Pen pen = new Pen(Color.Black, penWidth);

        if (this.BorderStyle == BorderStyle.None)
        {
          e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, ((PictureBox)sender).Width - penWidth, ((PictureBox)sender).Height - penWidth));
        }
        else
        {
          if (((PictureBox)sender).Name.Contains("Remote"))
          {
            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, ((PictureBox)sender).Height));
            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(((PictureBox)sender).Width, 0));
          }
          else
          {
            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(((PictureBox)sender).Width, 0));
            e.Graphics.DrawLine(pen, new Point(((PictureBox)sender).Width - penWidth, 0), new Point(((PictureBox)sender).Width - penWidth, ((PictureBox)sender).Height - penWidth));
          }
        }
      }
    }
  }

  /// <summary>
  /// Summary description for LowLevelControlButtonEventArgs.
  /// </summary>
  public class LowLevelControlButtonEventArgs : EventArgs
  {
    private string controlName;
    public string ControlName
    {
      get { return controlName; }
      set { controlName = value; }
    }

    private bool autoMan;
    public bool AutoMan
    {
      get { return autoMan; }
    }

    private bool locRem;
    public bool LocRem
    {
      get { return locRem; }
    }

    private int setPoint;
    public int SetPoint
    {
      get { return setPoint; }
    }

    public LowLevelControlButtonEventArgs(bool theAutoMan, bool theLocRem, int theSetPoint, string theControlName)
    {
      this.autoMan = theAutoMan;
      this.locRem = theLocRem;
      this.setPoint = theSetPoint;
      this.controlName = theControlName;
    }
  }
}
