using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SingleFacer.Settings
{
    public partial class Frm_SingleFacerSettingsBridgeStatus : SyncroC.Forms.Pages.SingleFacer.Settings.Frm_SingleFacerSettingsSubgroup
    {
        public Frm_SingleFacerSettingsBridgeStatus()
        {
            InitializeComponent();
        }
        /// <summary>
				/// Builder. Build an istance of this page for the specified SingleFacer.
				/// </summary>
				/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>  
				public Frm_SingleFacerSettingsBridgeStatus(int index)
				: this()
				{
					if (! (index == 1 || index == 2 || index == 3))
										Logger.LogError(this, "Frm_SingleFacerSettingsBridgeStatus - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
					this.index = index;
					lbl_Title_YN.Text += " " + index.ToString();
				}

				protected override void F13()
				{
				}

      private void btn_Recalibrate_Click(object sender, EventArgs e)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus02), index) + "." + "RecalibrateMoinstureSensor" + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
		
    }
}

