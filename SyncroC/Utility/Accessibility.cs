using System;

using SyncroC.Utility.EventArgs;

//ABELLO_FI14/08/2010_1. Riviste le password
namespace SyncroC.Utility
{
	/// <summary>
	/// Accessibility levels for pages access.
	/// </summary>
	public enum AccessibilityLevel
	{
    NoPassword,
		/// <summary>
		/// Low accessibility level: some pages can not be accessed, some pages can not be edited.
		/// </summary>
		Low,
		/// <summary>
		/// Medium accessibility level: all pages can be accessed, some pages can not be edited.
		/// </summary>
		Medium,
		/// <summary>
		/// High accessibility level: full control, all pages can be accessed and edited.
		/// </summary>	
		High
	}

	/// <summary>
	/// Provides facilities to filter actions accordingly to a security level.
	/// </summary>
	public class Accessibility
	{
		/// <summary>The one and only Accessibility object.</summary>
		private static Accessibility accessibility;

    /// <summary>
    /// Password for Low level accessibility: can be used by customer system administrators and responsibles.
    /// </summary>
    private string LOW_PWD_0 = "fosber";
    private string LOW_PWD_1 = "1234";
		/// <summary>
		/// Password for Medium level accessibility: can be used by customer system administrators and responsibles.
		/// </summary>
		private string MEDIUM_PWD_0 = "fosberprc";
		private string MEDIUM_PWD_1 = "05833891";
		/// <summary>
		/// Password for High level accessibility: can be used only by Fosber technicians.
		/// </summary>
		private string HIGH_PWD_0 = "fosbersoft";
		private string HIGH_PWD_1 = "55060";

		/// <summary>Current security level.</summary>
		private AccessibilityLevel level;
    ///// <summary>
    ///// Current security level.
    ///// </summary>
    public AccessibilityLevel Level
    {
      get { return level; }
    }

		/// <summary>Current enabling of the console</summary>
		private bool consoleEnabled;
		/// <summary>
		/// Gets or sets the enabling of the console.
		/// </summary>
		public bool ConsoleEnabled
		{
			get { return consoleEnabled; }
			set
			{
				consoleEnabled = value;
				if (OnAccessibilityChanged != null)
					OnAccessibilityChanged(this, new AccessibilityChangedEventArgs(this.level, this.level, consoleEnabled));
			}
		}

		/// <summary>
		/// Delegate for handling AccessibilityChanged events.
		/// </summary>
		public delegate void AccessibilityChangedHandler(object sender, AccessibilityChangedEventArgs args);

		/// <summary>
		/// Event raised when the Accessibility to the application pages changes.
		/// </summary>
		public event AccessibilityChangedHandler OnAccessibilityChanged;

		/// <summary>Private builder. To get the Accessibility object use the provided factory method "getAccessibility()".</summary>
		private Accessibility()
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				consoleEnabled = false;
				level = AccessibilityLevel.High;
			}
			else
			{
				consoleEnabled = true;
        level = AccessibilityLevel.NoPassword;
			}

      if (this.IamASuperUser())
				level = AccessibilityLevel.High;

			if ((PageManager.simulationSyncroStart) || (PageManager.simulationTwin400Start))
				level = AccessibilityLevel.High;
		}

    public bool IamASuperUser()
    {
      return
      //MSTAGETTI_FI-25-02-2011 Aggiunta UserName ad opzioni superuser
        (
        (Environment.UserName.ToUpper() == "ABELLO") || 
        (Environment.UserName.ToUpper() == "AGRASSI") || 
        (Environment.UserName.ToUpper() == "RFAZZI") || 
        (Environment.UserName.ToUpper() == "PMASSARO") ||
        (Environment.UserName.ToUpper() == "MSTAGETTI")
        );
    }

    /// <summary>
    /// Return a value determining if pages that can be accessed with low accessibility level
    /// are currently accessible.
    /// </summary>
		public bool isLevelAtLeastLow()
		{
			return level.Equals(AccessibilityLevel.Low) ||
        level.Equals(AccessibilityLevel.Medium) ||
        level.Equals(AccessibilityLevel.High);
		}

    /// <summary>
    /// Return a value determining if pages that can be accessed with medium accessibility level
    /// are currently accissible.
    /// </summary>
    public bool isLevelAtLeastMedium()
    {
      return level.Equals(AccessibilityLevel.Medium) ||
        level.Equals(AccessibilityLevel.High);
    }

		/// <summary>
		/// Return a value determining if pages that can be accessed with high accessibility level
		/// are currently accissible.
		/// </summary>
		public bool isLevelHigh()
		{
			return level.Equals(AccessibilityLevel.High);
		}

		/// <summary>
		/// Return a value determining if pages that can be accessed with high accessibility level
		/// are currently accissible.
		/// </summary>
		public bool isLevelMedium()
		{
			return level.Equals(AccessibilityLevel.Medium);
		}

		/// <summary>
		/// Factory.
		/// </summary>
		/// <returns>The one and only Accessibility object of the application.</returns>
		public static Accessibility getAccessibility()
		{
			if (accessibility == null)
				accessibility = new Accessibility();
			return accessibility;
		}

		/// <summary>
		/// Sets the accessibility level for the pages accordingly to the passed password.
		/// </summary>
		/// <param name="pwd">the reserved password</param>
		/// <returns>The current accessibility level.</returns>
		public AccessibilityLevel setAccessibilityLevel(string pwd)
		{
			AccessibilityLevel oldLevel = this.level;
      //RFAZZI_FI19/11/2010_1. Aggiunta gestione per differenziazione password di alto livello fra "installazione" e "assistenza". 
      //if (pwd.Equals(HIGH_PWD_0) || pwd.Equals(HIGH_PWD_1))
      if ((pwd.Equals(HIGH_PWD_0) && DBManager.getDBManager().syncroConfig.GeneralSetting.PasswordInstallationEnabled)
          || (pwd.Equals(HIGH_PWD_1) && !DBManager.getDBManager().syncroConfig.GeneralSetting.PasswordInstallationEnabled))
			{
				level = AccessibilityLevel.High;
				if (OnAccessibilityChanged != null)
					OnAccessibilityChanged(this, new AccessibilityChangedEventArgs(AccessibilityLevel.High, oldLevel, consoleEnabled));
				Logger.LogError(this, "setAccessibilityLevel - Pages accessibility was set to the High level.");
			}
			else if (pwd.Equals(MEDIUM_PWD_0) || pwd.Equals(MEDIUM_PWD_1))
			{
				level = AccessibilityLevel.Medium;
				if (OnAccessibilityChanged != null)
					OnAccessibilityChanged(this, new AccessibilityChangedEventArgs(AccessibilityLevel.Medium, oldLevel, consoleEnabled));
				Logger.LogError(this, "setAccessibilityLevel - Pages accessibility was set to the Medium level.");
			}
      else if (pwd.Equals(LOW_PWD_0) || pwd.Equals(LOW_PWD_1))
			{
				level = AccessibilityLevel.Low;
				if (OnAccessibilityChanged != null)
					OnAccessibilityChanged(this, new AccessibilityChangedEventArgs(AccessibilityLevel.Low, oldLevel, consoleEnabled));
				Logger.LogError(this, "setAccessibilityLevel - Pages accessibility was set to the Low level.");
			}
      else
      {
        level = AccessibilityLevel.NoPassword;
        if (OnAccessibilityChanged != null)
          OnAccessibilityChanged(this, new AccessibilityChangedEventArgs(AccessibilityLevel.NoPassword, oldLevel, consoleEnabled));
        Logger.LogError(this, "setAccessibilityLevel - Wrong password (\"" + pwd + "\") to change the pages accessibility was inserted. Pages accessibility was set to the Low level.");
      }
			return level;
		}

		/// <summary>
		/// Sets the Accessibility level to the Low level.
		/// </summary>
		/// <returns>The current accessibility level.</returns>
		public AccessibilityLevel resetAccessibilityLevel()
		{
			AccessibilityLevel oldLevel = this.level;
      level = AccessibilityLevel.NoPassword;
			if (OnAccessibilityChanged != null)
        OnAccessibilityChanged(this, new AccessibilityChangedEventArgs(AccessibilityLevel.NoPassword, oldLevel, consoleEnabled));
			Logger.LogError(this, "resetAccessibilityLevel - Pages accessibility was set to the Low level.");
			return level;
		}
	}
}
