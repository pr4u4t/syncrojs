using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SyncroC.Utility
{
	/// <summary>
	/// Provides a simple interface with .ini files
	/// </summary>
	public class Ini
	{
		[DllImport("kernel32", CharSet=CharSet.Unicode)]
		static extern int GetPrivateProfileStringW(
			string sezione, string item, string deFault, StringBuilder variabile, int size, string file);	
		
		[DllImport("kernel32", CharSet=CharSet.Unicode)]
		static extern int WritePrivateProfileStringW(
			string sezione, string item, string data, string file);	

		public Ini()
		{}

		/// <summary>
		/// read a value from a .ini file.
		/// </summary>
		/// <param name="section"></param>
		/// <param name="item"></param>
		/// <param name="deFault"></param>
		/// <param name="fullPathFile"></param>
		/// <returns></returns>
		public static string Get(string section, string item, string deFault, string fullPathFile)
		{
			const string def="___DEFAULT___";
			StringBuilder ele = new StringBuilder(128);
			string ret;
			GetPrivateProfileStringW(section, item, def, ele, 128, fullPathFile);
			ret =  ele.ToString();
			if (ret.Equals(def))
			{
				//il valore letto è uguale a quello di default
				Set(section, item, deFault, fullPathFile);
				return deFault;
			}
			return ret;
		}

		/// <summary>
		/// read a value from a .ini file.
		/// </summary>
		/// <param name="section"></param>
		/// <param name="item"></param>
		/// <param name="deFault"></param>
		/// <param name="fullPathFile"></param>
		/// <returns></returns>
		public static string GetNoRebuild(string section, string item, string deFault, string fullPathFile)
		{
			StringBuilder ele = new StringBuilder(128);
			GetPrivateProfileStringW(section, item, deFault, ele, 128, fullPathFile);
			return ele.ToString();
		}

		/// <summary>
		/// Write a value into a .ini file.
		/// </summary>
		/// <param name="section"></param>
		/// <param name="item"></param>
		/// <param name="data"></param>
		/// <param name="fullPathFile"></param>
		/// <returns></returns>
		public static int Set(string section, string item, string data, string fullPathFile)
		{
			return WritePrivateProfileStringW(section, item, data, fullPathFile);
		}
	}
}
