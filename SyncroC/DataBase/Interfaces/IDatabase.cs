using System;

namespace SyncroC.DataBase.Interfaces
{
	/// <summary>
	/// Defines a common interface for every database class.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values</param>
		void Add(string values);
		
		/// <summary>
		/// Delete the specified record.
		/// </summary>
		/// <param name="key">Key of the record to be deleted.</param>
		void Del(string key);

		/// <summary>
		/// Modify an existing record.
		/// </summary>
		/// <param name="values">a string representing the new values</param>
		void Mod(string values);
		
		/// <summary>
		/// Clean the dataset.
		/// </summary>
		void Clr();

		/// <summary>
		/// Compare the given cheksum with the database checksum.
		/// </summary>
		/// <param name="checksum">the checksum to compare</param>
		void Chk(string checksum);

		/// <summary>
		/// Load the databse.
		/// </summary>
		void Load();

		/// <summary>
		/// Save permanently the database.
		/// </summary>
		void Save();

		/// <summary>
		/// Set the database version.
		/// </summary>
		/// <param name="ver">the version to set as a string.</param>
		void SetVer(string ver);

		/// <summary>
		/// Get the database version as a string.
		/// </summary>
		string GetVer();
	}
}

