using System;
using System.Reflection;
using SyncroC.DataBase.LineConfig;
using SyncroC.DataBase.SyncroConfig;
using System.ComponentModel;

using SyncroC.Utility;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Summary description for ConfigBase.
	/// </summary>
	public abstract class ConfigBase
	{
		public enum ConfigBaseOperation
		{
			PropertiesIntoString,
			PropertiesFromINI,
			PropertiesFromString,
			PropertiesFromStringAdd,
			PropertiesIntoINI,
      PropertiesToDocumentation
		};
		protected string FILE_INI_NAME;
		protected string fullPathIniFile;
		protected string sectionName;

		protected string GetFileIniString(string section, string item, string deFault)
		{
			return Utility.Ini.Get(section, item, deFault, this.fullPathIniFile);
		}

		protected int SetFileIniString(string section, string item, string data)
		{
			return Utility.Ini.Set(section, item, data, this.fullPathIniFile);
		}

		public void Load()
		{
			string s = string.Empty;
			this.SurveyProperties(ref s, -1, -1, 0, ConfigBaseOperation.PropertiesFromINI);
			PageManager.getPageManager().refreshAllFormsData();
		}

    protected string WriteDocumentationItem(PropertyInfo p, string sInd)
    {
      bool browsable = true, readOnly = false;
      string category = "", description = "";
      object[] attrib = p.GetCustomAttributes(typeof(BrowsableAttribute), false);
      if (attrib.Length > 0)
        browsable = ((BrowsableAttribute)attrib[0]).Browsable;
      if (browsable)
      {
        attrib = p.GetCustomAttributes(typeof(ReadOnlyAttribute), false);
        if (attrib.Length > 0)
          readOnly = ((ReadOnlyAttribute)attrib[0]).IsReadOnly;
        attrib = p.GetCustomAttributes(typeof(CategoryAttribute), false);
        if (attrib.Length > 0)
          category = ((CategoryAttribute)attrib[0]).Category;
        attrib = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attrib.Length > 0)
          description = ((DescriptionAttribute)attrib[0]).Description;
        return "* " + category + ": " + p.Name + sInd + " - " + description + (readOnly ? " (ReadOnly)" : "") + "\r\n";
      }
      return "";
    }

    protected bool WriteDocumentationTitleAndCheckForNextCall(ref string s, PropertyInfo p, int level, ref bool SFCdone, ref bool SPLdone,
                                    ref bool BRGdone, ref bool STKdone, ref bool CRTdone, ref bool WRPdone, ref bool GLUdone, ref bool ZSPdone)
		{
      bool nextCall = false;
      bool isSFC = p.PropertyType.Equals(typeof(SingleFacer));
      bool isSPL = p.PropertyType.Equals(typeof(Splicer));
      bool isBRG = p.PropertyType.Equals(typeof(Bridge));
      bool isSTK = p.PropertyType.Equals(typeof(Stacker));
      bool isCRT = p.PropertyType.Equals(typeof(Cartridge));
      bool isWRP = p.PropertyType.Equals(typeof(WrapArm));
      bool isGLU = p.PropertyType.Equals(typeof(GlueGap));
      bool isZSP = p.PropertyType.Equals(typeof(ZoneSpeedConfig));
      if ((isSFC && (!SFCdone)) || (isSPL && (!SPLdone)) || (isBRG && (!BRGdone)) || (isSTK && (!STKdone)) ||
          (isCRT && (!CRTdone)) || (isWRP && (!WRPdone)) || (isGLU && (!GLUdone)) || (isZSP && (!ZSPdone)) ||
          ((!isSFC) && (!isBRG) && (!isSPL) && (!isSTK) && (!isCRT) && (!isWRP) && (!isGLU) && (!isZSP)))
      {
        SFCdone = SFCdone || isSFC;
        SPLdone = SPLdone || isSPL;
        BRGdone = BRGdone || isBRG;
        STKdone = STKdone || isSTK;
        CRTdone = CRTdone || isCRT;
        WRPdone = WRPdone || isWRP;
        GLUdone = GLUdone || isGLU;
        ZSPdone = ZSPdone || isZSP;
        if (level == 0)
          s += "\r\n==> " + p.Name.Substring(0, p.Name.Length - 6 - ((isSFC || isSPL || isBRG || isSTK)?1:0)).ToUpper() + "\r\n";
        nextCall = true;
      }
      return nextCall;
    }

    public virtual void StartSurveyProperties(ref string s, ConfigBaseOperation op)
    {
      this.SurveyProperties(ref s, -1, -1, 0, op);
    }

    protected virtual string GetBelongingSection()
    {
      return "";
    }

		protected virtual void SurveyProperties(ref string s, int index, int parentIndex, int level, ConfigBaseOperation op)
		{
      bool SFCdone = false, SPLdone = false, BRGdone = false, STKdone = false, CRTdone = false;
      bool WRPdone = false, GLUdone=false, ZSPdone=false;
			string section = this.sectionName;
			string sInd = string.Empty;
      //agrassi_FI20-05-2011_1
      string s2Ind = string.Empty;
			if ((index >= 0) && (section!=null) && (section!=""))
			{
				if (section.Substring(section.Length - 1) == "_")
					section += (index + 1).ToString();
				else
					sInd = "_" + (index + 1).ToString();
			}

      if ((op == ConfigBaseOperation.PropertiesIntoString) && (section != null) && (section.Trim() != ""))
				s += "[" + section + "]" + Command.PARAMETER_SEPARATOR;
      if ((section==null) || section.Equals(""))
        section = this.GetBelongingSection();

      if (section == "OFFICE")
        section = "OFFICE";
      
      PropertyInfo[] properties = this.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				string mydefault = null;
				bool isArray = properties[i].PropertyType.IsArray;
        //agrassi_FI20-05-2011_1
        bool is2Array = false;
				int len = 1;
        //agrassi_FI20-05-2011_1
        int len2 = 1;
				object val = null;
				System.Array arrayVal = null;
        //agrassi_FI20-05-2011_1
        System.Array array2Val = null;
				if (isArray)
				{
					len = ((System.Array)properties[i].GetValue(this, null)).Length;
				}
				for (int ind = 0; ind < len; ind++)
				{
					val = properties[i].GetValue(this, null);
					if (isArray)
					{
						val = ((System.Array)val).GetValue(ind);
						sInd = "_" + (ind + 1).ToString();
            //agrassi_FI20-05-2011_1
            is2Array = val.GetType().IsArray;
            s2Ind = string.Empty;
            len2=1;
            if (is2Array)
            {
              len2 = ((System.Array)val).Length;
            }
					}
          if (!isArray && (index < 0))
            sInd = string.Empty;
          //agrassi_FI20-05-2011_1
          if ((!isArray) || (!is2Array))
            s2Ind = string.Empty;
          //agrassi_FI20-05-2011_1
          for (int ind2 = 0; ind2 < len2; ind2++)
          {
            if (val != null)
            {
              //agrassi_FI20-05-2011_1
              Type t = val.GetType();
              Type tPar = t;
              if (is2Array)
              {
                val = properties[i].GetValue(this, null);
                val = ((System.Array)val).GetValue(ind);
                val = ((System.Array)val).GetValue(ind2);
                s2Ind = (ind2 + 1).ToString();
                t=val.GetType();
              }
              if (t.IsPrimitive || t.IsEnum || t.Equals(typeof(string)))
              {
                if ((isArray && (arrayVal == null)) || (is2Array && (ind2==0)))
                {
                  //agrassi_FI20-05-2011_1
                  if (!is2Array)
                    arrayVal = this.GetArray(t, len);
                  else
                  {
                    array2Val=this.GetArray(t, len2);
                    if (ind==0) arrayVal=this.GetArray(tPar, len, len2);
                  }
                }
                mydefault = this.ConvertToString(val, val.GetType());
                string stringValue = string.Empty;
                if (op == ConfigBaseOperation.PropertiesFromINI)
                  //agrassi_FI20-05-2011_1
                  //stringValue = this.GetFileIniString(section, properties[i].Name + sInd, mydefault);
                  stringValue = this.GetFileIniString(section, properties[i].Name + sInd + s2Ind, mydefault);
                else if (op == ConfigBaseOperation.PropertiesIntoString)
                  //agrassi_FI20-05-2011_1
                  //s += properties[i].Name + sInd + "=" + mydefault + Command.PARAMETER_SEPARATOR;
                  s += properties[i].Name + sInd + s2Ind + "=" + mydefault + Command.PARAMETER_SEPARATOR;
                else if ((op == ConfigBaseOperation.PropertiesToDocumentation) && ((index == 0) || (index == -1)) && (ind == 0))
                  s += this.WriteDocumentationItem(properties[i], sInd);
                else if (op == ConfigBaseOperation.PropertiesIntoINI)
                  //agrassi_FI20-05-2011_1
                  //this.SetFileIniString(section, properties[i].Name + sInd, mydefault);
                  this.SetFileIniString(section, properties[i].Name + sInd + s2Ind, mydefault);
                else if (op == ConfigBaseOperation.PropertiesFromString)
                  //agrassi_FI20-05-2011_1
                  //stringValue = this.SearchStringForItem(s, section, properties[i].Name + sInd);
                  stringValue = this.SearchStringForItem(s, section, properties[i].Name + sInd + s2Ind);
                else if (op == ConfigBaseOperation.PropertiesFromStringAdd)
                  //agrassi_FI20-05-2011_1
                  //stringValue = this.SearchStringForItem(s, null, properties[i].Name + sInd);
                  stringValue = this.SearchStringForItem(s, null, properties[i].Name + sInd + s2Ind);
                if ((op == ConfigBaseOperation.PropertiesFromINI) || (op == ConfigBaseOperation.PropertiesFromString)
                    || (op == ConfigBaseOperation.PropertiesFromStringAdd))
                {
                  if ((!stringValue.Equals(string.Empty)) || (properties[i].PropertyType == typeof(char)) || (properties[i].PropertyType == typeof(string)))
                  {
                    //agrassi_FI26-06-08
                    //if (isArray && arrayVal != null)
                    //  arrayVal.SetValue(Convert.ChangeType(stringValue, t), ind);
                    //else
                    //{
                    object realValue = this.ConvertFromString(stringValue, val.GetType());
                    if (realValue == null)
                    {
                      Logger.LogError(this, "SurveyProperties - property \"" + properties[i].Name + "\"" +
                            (isArray ? "(ind=" + ind.ToString() + ")" : "") + ", Error converting from string \"" +
                            stringValue + "\" to type \"" + val.GetType().Name + "\".");
                    }
                    else
                    {
                      //agrassi_FI20-05-2011_1
                      //if (isArray && arrayVal != null)
                      if (isArray)
                      {
                        if (arrayVal != null)
                        {
                          //agrassi_FI20-05-2011_1
                          if (!is2Array)
                            arrayVal.SetValue(realValue, ind);
                          else if (array2Val != null)
                          {
                            array2Val.SetValue(realValue, ind2);
                            arrayVal.SetValue(array2Val, ind);
                          }
                        }
                      }
                      else
                        properties[i].SetValue(this, realValue, null);
                    }
                    //}
                  }
                }
              }
              else
              {
                if (!(((op == ConfigBaseOperation.PropertiesFromString) || (op == ConfigBaseOperation.PropertiesIntoString))
                  && (this.GetType().Name.Equals("LineConfig"))))
                {
                  //if (this.WriteDocumentationTitles(ref s, properties[i], op, level, index))
                  bool nextCall = true;
                  if ((op == ConfigBaseOperation.PropertiesToDocumentation) && (level <= 1) && ((index == 0) || (index == -1)))
                    nextCall = this.WriteDocumentationTitleAndCheckForNextCall(ref s, properties[i], level, ref SFCdone, ref SPLdone,
                                                        ref BRGdone, ref STKdone, ref CRTdone, ref WRPdone, ref GLUdone, ref ZSPdone);
                  if (nextCall)
                    ((ConfigBase)val).SurveyProperties(ref s, (isArray ? ind : -1), index, level + 1, op);
                  if (properties[i].Name.Equals("Scorer"))
                  {
                    if ((op == ConfigBaseOperation.PropertiesFromINI) || (op == ConfigBaseOperation.PropertiesFromString))
                    {
                      int positCount = ScorerProfileType.PositioningAvailableCount(((ScoreType)this).Scorer.SClass);
                      ((ScoreType)this).GapData = new ScoreGapData[positCount];
                      for (int j = 0; j < ((ScoreType)this).GapData.Length; j++)
                        ((ScoreType)this).GapData[j] = new ScoreGapData(((ScoreType)this).Scorer.SClass);
                    }
                  }
                }
              }
            }
            else
            {
              Logger.LogError(this, properties[i].Name + (isArray ? "[" + ind.ToString() + "]" : "") + "=null");
            }
          }
				}
				if ((op == ConfigBaseOperation.PropertiesFromINI) || (op == ConfigBaseOperation.PropertiesFromString)
						|| (op == ConfigBaseOperation.PropertiesFromStringAdd))
					if (isArray && (arrayVal != null))
						properties[i].SetValue(this, arrayVal, null);
			}
		}

		protected string SearchStringForItem(string s, string section, string item)
		{
			item = item.ToLower();
			string sectionString = s;
			if (section != null)
			{
        section = section.ToLower();
        int sectionIndex = s.ToLower().IndexOf("[" + section + "]", 0);
				if (sectionIndex < 0)
				{
					Logger.LogError(this, "Section " + section + " not found");
					return string.Empty;
				}
				int nextSectionIndex = s.IndexOf("[", sectionIndex + 1);
				if (nextSectionIndex < 0) nextSectionIndex = s.Length;
				sectionString = s.Substring(sectionIndex, nextSectionIndex - sectionIndex);
			}
			int itemIndex = sectionString.ToLower().IndexOf(item);
			if (itemIndex != 0)
			{
				itemIndex = sectionString.ToLower().IndexOf(Command.PARAMETER_SEPARATOR + item);
				if (itemIndex < 0)
				{
					Logger.LogError(this, "Item " + item + " not found in section "+section);
					return string.Empty;
				}
			}
			int equalIndex = sectionString.IndexOf("=", itemIndex);
			if (equalIndex < 0)
			{
				Logger.LogError(this, "Item " + item + " has not any value in section "+section);
				return string.Empty;
			}
			int sepIndex = sectionString.IndexOf(Command.PARAMETER_SEPARATOR, equalIndex);
			if (sepIndex < 0)
			{
				Logger.LogError(this, "Separation after item " + item + " not found in section "+section);
				return string.Empty;
			}
			return sectionString.Substring(equalIndex + 1, sepIndex - equalIndex - 1).Trim();
		}

		public string SerializeProperties()
		{
			string s = string.Empty;
			this.SurveyProperties(ref s, -1, -1, 0, ConfigBaseOperation.PropertiesIntoString);
			return s;
		}

		public void Config(string values)
		{
			Logger.LogError(this, "Get configuration from Server: " + values.Substring(0, 20) + "...");
			string s = string.Empty;
			this.SurveyProperties(ref values, -1, -1, 0, ConfigBaseOperation.PropertiesFromString);
			PageManager.getPageManager().refreshAllFormsData();
		}

		public ConfigBase CreateCopy()
		{
			ConfigBase copy = (ConfigBase)this.MemberwiseClone();
			PropertyInfo[] properties = this.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
				if (!((properties[i].PropertyType.IsPrimitive) || (properties[i].PropertyType.IsEnum) ||
					(properties[i].PropertyType.Equals(typeof(string)))))
				{
					if (properties[i].PropertyType.IsArray)
					{
						System.Array arr = (System.Array)properties[i].GetValue(this, null);
						System.Array copyArr = (System.Array)arr.Clone();
						for (int ind = 0; ind < arr.Length; ind++)
						{
							if (arr.GetValue(ind) is ConfigBase)
							{
								ConfigBase refObj = (ConfigBase)arr.GetValue(ind);
								ConfigBase copyObj = refObj.CreateCopy();
								copyArr.SetValue(copyObj, ind);
							}
							else
							{
								Object copyObj = (Object)arr.GetValue(ind);
								copyArr.SetValue(copyObj, ind);
							}
						}
						PropertyInfo prop = copy.GetType().GetProperty(properties[i].Name);
						prop.SetValue(copy, copyArr, null);
					}
					else
					{
						ConfigBase refObj = (ConfigBase)properties[i].GetValue(this, null);
						copy.GetType().GetProperty(properties[i].Name).SetValue(copy, refObj.CreateCopy(), null);
					}
				}
			return copy;
		}

		protected string ConvertToString(Object val, Type t)
		{
			if (t.IsEnum)
				return Convert.ToInt32(val).ToString();
			if (t.Equals(typeof(double)))
        //agrassi_FI13-09-2011_1
        //return Convert.ToDouble(val).ToString("n", SyncroConverter.NumberFormat(5));
        return Convert.ToDouble(val).ToString("n", SyncroConverter.NumberFormat(7));
      return val.ToString();
		}

		protected object ConvertFromString(string val, Type t)
		{
			object ret = null;
      if (t.Equals(typeof(string)))
      {
        return val;
      }
      if (t.Equals(typeof(char)))
      {
        try
        {
          if (val == "")
            return ' ';
          ret = char.Parse(val);
          return ret;
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "ConvertFromString - error parsing char , value \"" + val + "\".", ex);
          return ' ';
        }
      }
      if (t.IsEnum)
			{
				int valInt = 0;
				try
				{
					valInt = int.Parse(val);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "ConvertFromString - wrong paramenter val (value " + val + " must be an integer.", ex);
				}
        try
        {
          if (System.Enum.IsDefined(t, valInt))
          {
            ret = System.Enum.Parse(t, val, true);
          }
          else
          {
            ret = System.Enum.Parse(t, System.Enum.GetNames(t)[0], true);
          }
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "ConvertFromString - error parsing enum \"" + t.Name + "\", value \"" + val + "\".", ex);
        }
        return ret;
			}
      try
      {
        if (t.Equals(typeof(double)))
        {
          ret = double.Parse(val, SyncroConverter.NumberFormat(5));
        }
        else
        {
          ret = System.Convert.ChangeType(val, t);
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "ConvertFromString - error parsing double, value \"" + val + "\".", ex);
      }
      return ret;
		}

		protected System.Array GetArray(Type t, int length)
		{
			if (t.Equals(typeof(int))) return new int[length];
			if (t.Equals(typeof(string))) return new string[length];
			if (t.Equals(typeof(bool))) return new bool[length];
			if (t.Equals(typeof(double))) return new double[length];
			if (t.Equals(typeof(char))) return new char[length];
			if (t.IsEnum) return new System.Enum[length];
			return null;
		}

    protected System.Array GetArray(Type t, int length1, int length2)
    {
      if (t.Equals(typeof(int[])))
      {
        Type tbase = typeof(int);
        int[][] ret = new int[length1][];
        for (int i = 0; i < length1; i++)
          ret[i] = (int[])this.GetArray(typeof(int), length2);
        return ret;
      }
      return null;
    }
	}
}
