using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Controls.Prc
{
  public partial class PrcTemperature : UserControl
  {
    private int temperature;
    [Description("La temperatura da far vedere."), Category("FosberSoft")]
    public int Temperature
    {
      get { return this.temperature; }
      set
      {
        this.temperature = value;
        this.lbl_Temperatura.Text = value.ToString();

        if (value < this.standardTemperature)
          this.pct_temperatura.Image = global::SyncroC.Properties.Resources.TEMPERATURA_CARTA_BASSA;
        else
          this.pct_temperatura.Image = global::SyncroC.Properties.Resources.TEMPERATURA_CARTA;

        if (!this.Visible)
          this.Visible = true;
      }
    }

    private int standardTemperature = 0;
    [Description("La temperatura che si pensa sia normale."), Category("FosberSoft")]
    public int StandardTemperature
    {
      get { return standardTemperature; }
      set { standardTemperature = value; }
    }

    public PrcTemperature()
    {
      InitializeComponent();
    }
  }
}
