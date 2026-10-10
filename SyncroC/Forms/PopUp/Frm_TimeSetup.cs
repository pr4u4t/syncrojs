using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

//ABELLO_FI04/11/2008_1 Aggiunta la PopUp per il cambio di Data e Ora.
namespace SyncroC.Forms.PopUp
{
  public partial class Frm_TimeSetup : SyncroC.Forms.PopUp.Frm_PopUp
  {
    private SyncroC.Utility.TimeZoneInformation ct;
    private bool dateTimePickerModified = false;

    public Frm_TimeSetup()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner,string text)
    {
      this.Text = text;
      this.dateTimePicker.Value = DateTime.Now;
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    private void btn_Abort_YN_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
      this.data = new object[2];

      if (this.dateTimePickerModified)
      {
        try
        {
          SyncroC.Utility.TimeZoneInformation.SYSTEMTIME lpSystemTime = new SyncroC.Utility.TimeZoneInformation.SYSTEMTIME();
          lpSystemTime.DayOfWeek = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().DayOfWeek);
          lpSystemTime.Day = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Day);
          lpSystemTime.Month = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Month);
          lpSystemTime.Year = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Year);
          lpSystemTime.Hour = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Hour);
          lpSystemTime.Minute = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Minute);
          lpSystemTime.Second = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Second);
          lpSystemTime.Milliseconds = Convert.ToUInt16(this.dateTimePicker.Value.ToUniversalTime().Millisecond);

          this.data[0] = lpSystemTime;
        }
        catch (Exception)
        {
          this.data = new object[] { new SyncroC.Utility.TimeZoneInformation.SYSTEMTIME() };
        }
      }

      if (this.ct != this.cmb_TimeZones.SelectedItem)
      {
        if (this.cmb_TimeZones.SelectedItem != null)
        {
          this.data[1] = ((SyncroC.Utility.TimeZoneInformation)this.cmb_TimeZones.SelectedItem).TziNative();
        }
      }

      this.Dispose();
    }

    private void Frm_TimeSetup_Load(object sender, EventArgs e)
    {
      this.cmb_TimeZones.Items.AddRange(SyncroC.Utility.TimeZoneInformation.EnumZones());
      this.cmb_TimeZones.SelectedItem = SyncroC.Utility.TimeZoneInformation.CurrentTimeZone;
      this.ct = SyncroC.Utility.TimeZoneInformation.CurrentTimeZone;
    }

    private void timerDateTimePicker_Tick(object sender, EventArgs e)
    {
      if (!this.dateTimePickerModified)
        this.dateTimePicker.Value = DateTime.Now;
    }

    private void dateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.dateTimePickerModified = true;
    }
  }
}
