using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.Controls
{
  //ABELLO_FI22/08/2008_1 Rivisti gli ingressi uscita per Versione di PLC 6
  public enum IOInputChannels: int
  {
    INLogicCabinet0 = 1,
    INLogicCabinet1 = 2,
    INOutletConsole0 = 3,
    INOutletConsole1 = 4,
    INPowerCabinet0 = 5,
    INPowerCabinet1 = 6,
    INSideFrame = 7,
    INStandAloneConsole0 = 8,
    INStandAloneConsole1 = 9,
    INPowerCabinet3 = 10
  };

  public enum IOOutputChannels: int
  {
    OUTLogicCabinet0 = 1,
    OUTLogicCabinet1 = 2,
    OUTLubrication = 3,
    OUTOutletConsole0 = 4,
    OUTPowerCabinet0 = 5,
    OUTPowerCabinet1 = 6,
    OUTSideFrame = 7,
    OUTStandAloneConsole = 8,
    OUTFree1 = 9,
    OUTFree2 = 10
  };

	public delegate void IOChannelChangeSelection(int selIndex);

	public class IOChannel : SyncroC.Controls.SyncroUserControl
	{
		public const int BITS_IN_WORD = 16;
    private int LeR_MARGIN, T_MARGIN, V_MARGIN, H_MARGIN, DISPLAY_WIDTH, DISPLAY_HEIGHT;
    private int number, value, tmpValue, bitIndex;
    private bool input, writeMode, disalignment, debug;
    string name;
    private bool[] bit;
    private string[] bitDescriptions;
    private Rectangle[] display;
    private bool sizeChanged = false, valueChanged = false, writeModeChanged = false, bitIndexChanged=false;
		private IOChannelChangeSelection changeFunction;
		private System.ComponentModel.IContainer components = null;

		public IOChannel()
		{
			InitializeComponent();
      this.number = 0;
      this.value = 0;
      this.debug = false;
      this.writeMode = false;
      this.disalignment = false;
      this.display = new Rectangle[BITS_IN_WORD];
      this.bitDescriptions = new string[BITS_IN_WORD];
      this.bit = new bool[BITS_IN_WORD];
      for (int d = 0; d < BITS_IN_WORD; d++)
      {
        this.bitDescriptions[d] = "Bit " + d.ToString();
        this.bit[d] = false;
      }
      this.name = "IOChannel";
    }

		public void Config(int number, string[] bitDescriptions, bool input, IOChannelChangeSelection changeFunction)
		{
      this.number = number;
      this.input = input;
      this.changeFunction = changeFunction;
      if (this.input)
        this.name = Enum.Format(typeof(IOInputChannels), (IOInputChannels)(this.number + 1), "g");
      else
        this.name = Enum.Format(typeof(IOOutputChannels), (IOOutputChannels)(this.number + 1), "g");
      if (bitDescriptions != null)
        this.bitDescriptions = bitDescriptions;
      this.sizeChanged = true;
      this.Invalidate();
    }

		public void Config(string name, string[] bitDescriptions, bool input, IOChannelChangeSelection changeFunction)
		{
      this.input = input;
      this.changeFunction = changeFunction;
      this.name = name;
      if (bitDescriptions != null)
        this.bitDescriptions = bitDescriptions;
      this.sizeChanged = true;
      this.Invalidate();
    }
    public void SetWriteMode(bool mode)
    {
      if (!this.input)
      {
        this.writeMode = mode;
        this.writeModeChanged = true;
        this.tmpValue = 0;
        this.disalignment = false;
        this.bitIndex = 0;
        this.Invalidate();
      }
    }

    public void ResetDisalignment()
    {
      this.disalignment = false;
    }

    public void SetValue(int value)
    {
      if (!this.disalignment)
      {
        this.value = value;
        for (int b = 0; b < BITS_IN_WORD; b++)
          this.bit[b] = ((this.value & SlitterScorer.BIT(b)) != 0);
        this.valueChanged = true;
        this.Invalidate();
      }
    }

    public void SetTmpValue(int value)
    {
      if (this.writeMode)
      {
        this.tmpValue = value;
        for (int b = 0; b < BITS_IN_WORD; b++)
          this.bit[b] = ((this.tmpValue & SlitterScorer.BIT(b)) != 0);
        this.disalignment = true;
        this.Invalidate();
      }
    }

    public int GetTmpValue()
    {
      if (this.writeMode)
        return this.tmpValue;
      return 0;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
//      if (pea != null) e = new PaintEventArgs(pea.Graphics, pea.ClipRectangle);
      base.OnPaint(e);
      this.sizeChanged = true;
      this.valueChanged = true;
      this.writeModeChanged = true;
      this.bitIndexChanged = true;
      if (this.writeModeChanged)
        this.BackColor = (this.writeMode ? (this.disalignment ? Color.Pink : Color.Yellow) : Color.WhiteSmoke);
      if (this.sizeChanged)
      {
        this.LeR_MARGIN = (int)(0.03 * this.Width);
        this.T_MARGIN = (int)(0.08 * this.Height);
        this.V_MARGIN = (int)(0.02 * this.Height);
        this.H_MARGIN = (int)(0.09 * this.Width);
        this.DISPLAY_WIDTH = (int)(0.13 * this.Width);
        this.DISPLAY_HEIGHT = (int)(0.04 * this.Height);
        int tmpTop = T_MARGIN;
        for (int d = 0; d < BITS_IN_WORD; d++)
        {
          this.display[d] = new Rectangle(this.Width - LeR_MARGIN - DISPLAY_WIDTH, tmpTop, DISPLAY_WIDTH, DISPLAY_HEIGHT);
          tmpTop += V_MARGIN + this.display[d].Height;
        }
      }
      if (this.valueChanged || this.sizeChanged)
      {
        SizeF n = e.Graphics.MeasureString(this.name, new Font("Arial", 9));
        e.Graphics.DrawString(this.name, new Font("Arial", 9), new SolidBrush(Color.Blue), (this.Width - n.Width) / 2, 1);
        if (this.debug)
          e.Graphics.DrawString(this.value.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), 0, 1);
      }
      for (int b=0; b<BITS_IN_WORD; b++)
      {
        if (this.sizeChanged)
        {
          string bitDes = (this.bitDescriptions[b].Equals(string.Empty) ? "Bit" + b.ToString() : this.bitDescriptions[b]);
          e.Graphics.DrawString(b.ToString("00") + " - " + bitDes, new Font("Arial", 8), new SolidBrush(this.bit[b] ? Color.LimeGreen : Color.Black), LeR_MARGIN, this.display[b].Top);
        }
        if (this.valueChanged || this.sizeChanged || this.bitIndexChanged)
        {
          e.Graphics.FillRectangle(new SolidBrush(this.bit[b] ? Color.LimeGreen : Color.Red), this.display[b]);
          if (this.writeMode && (b==this.bitIndex))
            e.Graphics.DrawRectangle(new Pen(Color.LightBlue, 2.2f), this.display[b]);
          string text = (this.bit[b] ? "ON" : "OFF");
          SizeF s = e.Graphics.MeasureString(text, new Font("Arial", 7));
          e.Graphics.DrawString(text, new Font("Arial", 7), new SolidBrush(Color.WhiteSmoke),
                  this.display[b].X + (this.display[b].Width - s.Width) / 2, this.display[b].Y + (this.display[b].Height - s.Height) / 2);
        }
      }
//      pea = new PaintEventArgs(e.Graphics, e.ClipRectangle);
      this.sizeChanged = false;
      this.valueChanged = false;
      this.writeModeChanged = false;
      this.bitIndexChanged = false;
    }


    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
// 
// IOChannel
// 
      this.Name = "IOChannel";
      this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IOChannel_MouseClick);
      this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IOChannel_MouseDoubleClick);
      this.SizeChanged += new System.EventHandler(this.IOChannel_SizeChanged);

    }
		#endregion

    private void IOChannel_SizeChanged(object sender, EventArgs e)
    {
      this.sizeChanged = true;
      this.Invalidate();
    }

    private void IOChannel_MouseClick(object sender, MouseEventArgs e)
    {
      if (this.changeFunction != null)
        this.changeFunction(this.number);
    }

    private void IOChannel_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.writeMode)
      {
        int b;
        for (b = 0; b < BITS_IN_WORD; b++)
        {
          if ((e.X >= this.display[b].X) && (e.X <= this.display[b].X + this.display[b].Width) &&
            (e.Y >= this.display[b].Y) && (e.Y <= this.display[b].Y + this.display[b].Height))
            break;
        }
        if (b < BITS_IN_WORD)
        {
          this.bitIndex = b;
          this.UpdateBit();
        }
      }
    }

    private void UpdateBit()
    {
      int newValue = this.tmpValue | (this.disalignment ? 0 : this.value);
      newValue ^= SlitterScorer.BIT(this.bitIndex);
      this.SetTmpValue(newValue);
      this.Invalidate();
    }

    public void IncBitIndex()
    {
      if (this.writeMode)
      {
        if (this.bitIndex < BITS_IN_WORD - 1)
        {
          this.bitIndex++;
          this.bitIndexChanged = true;
          this.Invalidate();
        }
      }
    }

    public void DecBitIndex()
    {
      if (this.writeMode)
      {
        if (this.bitIndex > 0)
        {
          this.bitIndex--;
          this.bitIndexChanged = true;
          this.Invalidate();
        }
      }
    }

    public void ToggleBit()
    {
      if (this.writeMode)
        this.UpdateBit();
    }

    public bool Debug
    {
      get { return this.debug; }
      set
      {
        if (this.debug != value)
        {
          this.debug = value;
          this.Invalidate();
        }
      }
    }
  }
}