using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.Pages.Splicer
{
	public partial class Frm_SplicerGroup : SyncroC.Forms.Pages.Frm_Page
	{
		/// <summary>
		/// The index of the Splicer represented on this page.
		/// </summary>
		protected int index;
		protected bool showDebugOptions = false;

		public Frm_SplicerGroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Set the last visited page for the SplicerGroup.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
					pm.pagesHistorian.CurrentPageSplicer1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSplicer2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSplicer3 = this.getRequestCommand(index);
					break;
				case 4:
					pm.pagesHistorian.CurrentPageSplicer4 = this.getRequestCommand(index);
					break;
				case 5:
					pm.pagesHistorian.CurrentPageSplicer5 = this.getRequestCommand(index);
					break;
				case 6:
					pm.pagesHistorian.CurrentPageSplicer6 = this.getRequestCommand(index);
					break;
				case 7:
					pm.pagesHistorian.CurrentPageSplicer7 = this.getRequestCommand(index);
					break;
			}
			pm.pagesHistorian.PageChanged(this, index);
			pm.UpdateCache(this, index);

			base._Show();
		}

		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return true;
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

		/// <summary>
		/// Splicer group must do nothing if Alt + F2 is pressed when index is 5
		/// </summary>
		protected override void AltF2()
		{
			if (index != 5)
				base.AltF2();
		}

		/// <summary>
		/// Splicer group must do nothing if Alt + F4 is pressed when index is 4
		/// </summary>
		protected override void AltF4()
		{
			if (index != 4)
				base.AltF4();
		}

		/// <summary>
		/// Splicer group must do nothing if Alt + F5 is pressed when index is 3
		/// </summary>
		protected override void AltF5()
		{
			if (index != 3)
				base.AltF5();
		}

		/// <summary>
		/// Splicer group must do nothing if Alt + F7 is pressed when index is 2
		/// </summary>
		protected override void AltF7()
		{
			if (index != 2)
				base.AltF7();
		}

		/// <summary>
		/// Splicer group must do nothing if Alt + F8 is pressed when index is 1
		/// </summary>
		protected override void AltF8()
		{
			if (index != 1)
				base.AltF8();
		}

		protected override void F1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm1);
					break;
				case 2:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm2);
					break;
				case 3:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm3);
					break;
				case 4:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm4);
					break;
				case 5:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm5);
					break;
				case 6:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm6);
					break;
				case 7:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerAlarm7);
					break;
			}
		}

		protected override void F2()
		{
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {

		    CommunicationManager cm = CommunicationManager.GetCommunicationManager();
		    PageManager pm = PageManager.getPageManager();
        switch (index)
        {
          case 1:
          default:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings1);
            break;
          case 2:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings2);
            break;
          case 3:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings3);
            break;
          case 4:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings4);
            break;
          case 5:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings5);
            break;
          case 6:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings6);
            break;
          case 7:
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageSplicerSettings7);
            break;
        }
			}
		}

		protected override void F3()
		{
			askForPage(typeof(Frm_SplicerStatus), index);
		}

		protected override void F4()
		{
			askForPage(typeof(Frm_SplicerConsole), index);
		}
	}
}

