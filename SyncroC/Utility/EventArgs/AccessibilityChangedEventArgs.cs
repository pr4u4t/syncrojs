using System;

namespace SyncroC.Utility.EventArgs
{
	/// <summary>
	/// Changement on the accessibility level of the pages.
	/// </summary>
	public class AccessibilityChangedEventArgs : System.EventArgs
	{
		/// <summary>
		/// The new AccessibilityLevel for the application pages.
		/// </summary>
		public AccessibilityLevel accessibilityLevel;
		public AccessibilityLevel oldAccessibilityLevel;
		public bool consoleEnabled;

		/// <summary>
		/// Builder.
		/// </summary>
		/// <param name="accessibilityLevel">The new accessibility Level that has been set.</param>
		public AccessibilityChangedEventArgs(AccessibilityLevel accessibilityLevel, AccessibilityLevel oldAccessibilityLevel, bool consoleEnabled)
		{
			this.accessibilityLevel = accessibilityLevel; 
			this.oldAccessibilityLevel=oldAccessibilityLevel;
			this.consoleEnabled = consoleEnabled;
		}
	}
}
