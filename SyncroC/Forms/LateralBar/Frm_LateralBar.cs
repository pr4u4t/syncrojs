using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Forms.Pages;

namespace SyncroC.Forms.LateralBar
{
  public partial class Frm_LateralBar : SyncroC.Forms.Frm_Base
  {
    public static readonly Size ORIGINAL_SIZE = new Size(400, 1025);

    public Frm_LateralBar()
    {
      InitializeComponent();

      Accessibility.getAccessibility().OnAccessibilityChanged += new SyncroC.Utility.Accessibility.AccessibilityChangedHandler(OnAccessibilityChanged);
    }

    /// <summary>
    /// return the position the LateralBar should take on the screen: below the pages.
    /// </summary>
    protected override Point getLocation()
    {
      //RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
      //return new Point(this.getScreenSize().Width, 0);
      return new Point(this.getScreenSize().Width + myScreen.Bounds.Left, 0);
    }

    /// <summary>
    ///return the "run time" LateralBar's size.  
    /// </summary>
    protected override Size getSize()
    {
      int height = this.getRealScreenSize().Height > SystemInformation.MaxWindowTrackSize.Height ? SystemInformation.MaxWindowTrackSize.Height : this.getRealScreenSize().Height;

      if (Accessibility.getAccessibility().ConsoleEnabled)
      {
        return new Size(this.getRealScreenSize().Width - 1280, height);
      }
      else
      {
        return new Size(this.getRealScreenSize().Width - 1280, height);
      }
    }

    private void OnAccessibilityChanged(object sender, SyncroC.Utility.EventArgs.AccessibilityChangedEventArgs args)
    {
      this.Size = this.getSize();
      this.Location = this.getLocation();
    }

    /// <summary>
    /// return the "design time" status bar's size.
    /// </summary>
    protected override Size getOriginalSize()
    {
      return ORIGINAL_SIZE;
    }

    public override void _Show()
    {
      this.lateralBarItem1.setUpLateralBarItem();
      this.lateralBarItem2.setUpLateralBarItem();
      this.lateralBarItem3.setUpLateralBarItem();

      if 
        (
        Settings.getSettings().configuration.WetEndControl && 
        Settings.getSettings().configuration.PRC
        )
      {
        //graffaelli_FI02/11/2011_2 - Bug Fix per richiamare le Tab corrette sulle LateralBar
        //this.lateralBarItem1.setSelectedIndex(3);       
        //this.lateralBarItem2.setSelectedIndex(4);
        //this.lateralBarItem3.setSelectedIndex(5);
        this.lateralBarItem1.setSelectedTab("tab_TraceSpeed");
        this.lateralBarItem2.setSelectedTab("tab_TraceTemp");
        this.lateralBarItem3.setSelectedTab("tab_TraceGlue");
      }
      else
      {
        //graffaelli_FI02/11/2011_2 - Bug Fix per richiamare le Tab corrette sulle LateralBar
        //this.lateralBarItem1.setSelectedIndex(0);
        //this.lateralBarItem2.setSelectedIndex(1);
        //this.lateralBarItem3.setSelectedIndex(2);        
        if (
            Settings.getSettings().configuration.DryEndControl &&
            !Settings.getSettings().configuration.WetEndControl
           )
        {
          this.lateralBarItem1.setSelectedTab("tab_DRYList");
          this.lateralBarItem2.setSelectedTab("tab_SHFRunStop");
          this.lateralBarItem3.setSelectedTab("tab_DRYBlades");
        }
        else
        {
          this.lateralBarItem1.setSelectedTab("tab_LowLevelSF1");
          this.lateralBarItem2.setSelectedTab("tab_LowLevelSF2");
          this.lateralBarItem3.setSelectedTab("tab_LowLevelPHTGU");
        }
      }

      this.lateralBarItemN_OnLateralBarItemSelectedIndexChanged(null, null);

      base._Show();
    }

    private void lateralBarItemN_OnLateralBarItemSelectedIndexChanged(object sender, EventArgs args)
    {
      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp
        (
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_PAGE_MANAGER,
        "lateralBar_lateralBar_1(" + this.lateralBarItem1.getSelectedIndex() + ")"
        );

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp
        (
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_PAGE_MANAGER,
        "lateralBar_lateralBar_2(" + this.lateralBarItem2.getSelectedIndex() + ")"
        );

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp
        (
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_PAGE_MANAGER,
        "lateralBar_lateralBar_3(" + this.lateralBarItem3.getSelectedIndex() + ")"
        );
    }

    private void timer_setUpButton_Tick(object sender, EventArgs e)
    {
      Point MousePosition = System.Windows.Forms.Cursor.Position;

      Rectangle myRectangle = new Rectangle(this.PointToScreen(this.lateralBarItem1.Location), this.lateralBarItem1.Size);
      if (myRectangle.Contains(MousePosition))
        this.lateralBarItem1.setUpButtonLateralBarItemChange(true);
      else
        this.lateralBarItem1.setUpButtonLateralBarItemChange(false);

      myRectangle = new Rectangle(this.PointToScreen(this.lateralBarItem2.Location), this.lateralBarItem2.Size);
      if (myRectangle.Contains(MousePosition))
        this.lateralBarItem2.setUpButtonLateralBarItemChange(true);
      else
        this.lateralBarItem2.setUpButtonLateralBarItemChange(false);

      myRectangle = new Rectangle(this.PointToScreen(this.lateralBarItem3.Location), this.lateralBarItem3.Size);
      if (myRectangle.Contains(MousePosition))
        this.lateralBarItem3.setUpButtonLateralBarItemChange(true);
      else
        this.lateralBarItem3.setUpButtonLateralBarItemChange(false);
    }
  }
}
