using System;

namespace SyncroC.DataBase.EventArgs
{
	/// <summary>
	/// Kinds of changement taht can occur to a record.
	/// </summary>
	public enum DBChangement : int
	{
		/// <summary>new record inserted.</summary>
		Insert,
		/// <summary>existing record updated.</summary>
		Modify,
		/// <summary>existing record deleted.</summary>
		Delete,
    /// <summary>queue shift</summary>
    Shift
	}

	/// <summary>
	/// Summary description for DBChangedEventArgs.
	/// </summary>
	public class DBChangedEventArgs : System.EventArgs
	{
		/// <summary>
		/// Key of the record changed.
		/// </summary>
		public string Key;
		/// <summary>
		/// Kind of changement occured on the record.
		/// </summary>
		public DBChangement Changement;

		public DBChangedEventArgs(string key, DBChangement changement)
		{
			Key = key;
			Changement = changement;
		}
	}
}
