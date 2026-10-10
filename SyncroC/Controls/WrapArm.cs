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
	/// Delegate to use for OnIncDecButtonKitDown.
	/// </summary>
	public delegate void WrapArmButtonHandler(object sender, WrapArmButtonEventArgs args);

	public partial class WrapArm : SyncroC.Controls.SyncroUserControl
	{
		/// <summary>
		/// Event raised every interval time when one of the button is down.
		/// </summary>
		[Description("Event raised when a Wrap Arm component button is pressed."), Category("Fosbersoft")]
		public event WrapArmButtonHandler OnWrapArmButtonPressed; 

		/// <summary>Determine if the wrap arm Is Under or Over wrapping.</summary>
		private bool isUnder = false;
		/// <summary>
		/// Gets or sets the IsUnder property.
		/// </summary>
    [Description("If true the Wrap Arm is under wrapping."), Category("FosberSoft")]
		public bool IsUnder
		{
			get { return this.isUnder; }
			set
			{
				if (this.isUnder != value)
				{
					this.isUnder = value;

				}

        if ((this.errorTemperature) && (this.temperature != 0) && this.Enabled)
          this.lbl_position.BackgroundImage = (this.isUnder) ? this.iml_WrapArmImages.Images[2] : this.iml_WrapArmImages.Images[3];
        else
          this.lbl_position.BackgroundImage = (this.isUnder) ? this.iml_WrapArmImages.Images[0] : this.iml_WrapArmImages.Images[1];  

			}
		}

		/// <summary>Determine if the wrap arm Is Auto or Manual controlled.</summary>
		private bool auto = false;
		/// <summary>
		/// Gets or sets the Auto property.
		/// </summary>
		[Description("If true the Wrap Arm Auto."), Category("FosberSoft")]
		public bool Auto
		{
			get { return auto; }
			set
			{
				auto = value;
				pct_AutoMan.Image = (auto) ? iml_AutoManImages.Images[0] : iml_AutoManImages.Images[1];
			}
		}

		/// <summary>Determine if the wrap arm Is in local or remote.</summary>
		private bool inLocal = false;
		/// <summary>
		/// Gets or sets the IsUnder property.
		/// </summary>
		[Description("If true the Wrap Arm is in locale."), Category("FosberSoft")]
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
		[Description("If true the Wrap Arm is at MAX Position."), Category("FosberSoft")]
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
		[Description("If true the Wrap Arm is at MIN Position."), Category("FosberSoft")]
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
    private string position;
    [Description("La posizione del Wrap Arm."), Category("FosberSoft")]
		public string Position
		{
      get { return position; }
			set
			{
        this.position = value;
        string txt = (this.temperature != 0) ? this.temperature.ToString() : "   ";
        this.lbl_position.Testo = (this.isUnder) ? this.position + Environment.NewLine + txt : txt + Environment.NewLine + this.position;
			}
		}

    private bool errorTemperature = false;
    private int temperature;
    [Description("La Temperatura letta dal termometro."), Category("FosberSoft")]
    public int Temperature
    {
      get { return this.temperature; }
      set
      {
        this.temperature = value;

        if (!this.myDesignModePage)
        {
          if (value < DBManager.getDBManager().lineConfig.PRCConfig.Header.ShowWarningWhenTemperatureLess)
            this.errorTemperature = true;
          else
            this.errorTemperature = false;
        }
      }
    }

		/// <summary>
		/// Gets or sets the position property.
		/// </summary>
    [Description("Il Nome del Wrap Arm."), Category("FosberSoft")]
		public string WrapName
		{
			get { return this.lbl_Name.Text; }
			set {	this.lbl_Name.Text = value; }
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
        this.lbl_position.Cursor = this.lowLevelEnabled ? Cursors.Hand : Cursors.Arrow;
        this.pct_AutoMan.Invalidate();
        this.pct_LocaleRemote.Invalidate();
			}
		}

    private float originalPositionFontSize = 20.0F;
    private float originalNameFontSize = 20.0F;

		public WrapArm()
		{
			this.myInitializeComponent();
		}

    private bool myDesignModePage = true;
		public void setDesignModePage(bool designModePage)
		{
			if (!designModePage)
			{
        //ABELLO_FI25/11/2010_1 [PRC] Il locale/Remoto non si deve vedere se non c'è il PRC
        //if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        if (!DBManager.getDBManager().lineConfig.PRC)
          this.pct_LocaleRemote.Visible = false;

        this.myDesignModePage = designModePage;
			}
		}

    private void myInitializeComponent()
		{
			InitializeComponent();        

      this.originalPositionFontSize = this.lbl_position.Font.Size;
      this.originalNameFontSize = this.lbl_Name.Font.Size;

      this.lbl_position.BackgroundImage = (this.isUnder) ? this.iml_WrapArmImages.Images[0] : this.iml_WrapArmImages.Images[1];
      this.lbl_position.BackgroundImageLayout = ImageLayout.Stretch;
      this.lbl_position.TextAlign = ContentAlignment.TopCenter;
      
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

      if (this.OnWrapArmButtonPressed != null)
        OnWrapArmButtonPressed(this, new WrapArmButtonEventArgs(true, false, 0, this.lbl_Name.Text));
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

      if (this.OnWrapArmButtonPressed != null)
        OnWrapArmButtonPressed(this, new WrapArmButtonEventArgs(false, true, 0, this.lbl_Name.Text));
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

		protected virtual void lbl_WrapArm_Click(object sender, EventArgs e)
		{
      if (!(sender is SyncroC.Controls.Prc.PrcWrapArm))
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
        pos = Convert.ToInt32(this.position);
			}
			catch (Exception)
			{
				pos = 0;
			}

			Frm_AskValue askValue = new Frm_AskValue();
      //ABELLO_FI30/10/2008_1 Messi i pulsanti Inc/Dec sulle PopUp che chiedono un valore numerico.
			FosberDialogResult ret = askValue.ShowDialog(this, "SetPoint", pos, 0, 100);

			if (ret.Dialog == DialogResult.OK)
			{
				int res = (int)ret.Data[0];

				if ((res < 0) || (res > 100))
				{
					string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_06");
					Frm_Alert alert = new Frm_Alert();
					alert.ShowDialog(this, text);
					res = -1;
				}

				if (res >= 0)
					if (this.OnWrapArmButtonPressed != null)
            OnWrapArmButtonPressed(this, new WrapArmButtonEventArgs(false, false, res, this.lbl_Name.Text));
			}
		}

    protected void WrapArm_SizeChanged(object sender, EventArgs e)
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
	/// Summary description for WrapArmButtonEventArgs.
	/// </summary>
	public class WrapArmButtonEventArgs : EventArgs
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

    public WrapArmButtonEventArgs(bool theAutoMan, bool theLocRem, int theSetPoint, string theControlName)
		{
			this.autoMan = theAutoMan;
			this.locRem = theLocRem;
			this.setPoint = theSetPoint;
      this.controlName = theControlName;
		}
	}
}
