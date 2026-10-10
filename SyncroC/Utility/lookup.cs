using System;
using System.Collections.Generic;
using System.Text;

namespace SyncroC.Utility
{
	public class Lookup
	{
		public  struct element
		{
			private int _code;
			private string _description;
			public element(int theCode, string theDescription)
			{
				_code = theCode;
				_description = theDescription; 
			}
			public int code { set { _code = value; } get { return _code; } }
			public string description { set { _description = value; } get { return _description; } }
		}
		private element[] _list;
		public element[] list { get{ return _list; } }
		public Lookup(Enum theEnum)
		{
			_list = new element[Enum.GetNames(theEnum.GetType()).GetLength(0)+1];
			_list[0].description = " ";
			_list[0].code = 0;
			int c = 1;
			foreach (string s in Enum.GetNames(theEnum.GetType()))
			{
				_list[c].description = s;
				_list[c].code = (int)Enum.GetValues(theEnum.GetType()).GetValue(c-1);
				c++;
			}
		}
	}
}
