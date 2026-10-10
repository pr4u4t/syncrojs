using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.GlueUnit
{
	public partial class Frm_GlueUnitGroup : SyncroC.Forms.Pages.Frm_Page
	{
		public Frm_GlueUnitGroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageGlueUnit = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);
			base._Show();
		}

		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
			switch (accessibilityLevel)
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        //ABELLO_FI14/08/2010_1. Riviste le password
        case AccessibilityLevel.NoPassword:
        case AccessibilityLevel.Low:
        case AccessibilityLevel.Medium:
					tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F2] = false;
					break;
				case AccessibilityLevel.High:
					tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F2] = true;
					break;
			}
		}

		protected override void F1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageGlueUnitAlarm);
		}

		protected override void F2()
		{
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageGlueUnitSettings);
      }
		}

		protected override void F3()
		{
			askForPage(typeof(Frm_GlueUnitStatus));
		}

		protected override void F4()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageGlueUnitConsole);
		}

	}
}

