#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

#endregion

namespace SyncroC.Utility
{
  /// <summary>
  /// 
  /// </summary>
  public enum UnitMeasure
  {
    /// <summary>No unit measure specified</summary>
    NoUnitMeasure,
    /// <summary>distance</summary>
    Metre,
    /// <summary>distance</summary>
    MilliMetre,
    /// <summary>distance</summary>
    DeciMilliMetre,
    /// <summary>distance</summary>
    Micron,
    /// <summary>distance</summary>
    DeciMicron,
    /// <summary>percentage</summary>
    Percentage,
    /// <summary>speed</summary>
    MetresPerMinute,
    /// <summary>speed</summary>
    MilliMetresPerMinute,
    /// <summary>pressure</summary>
    Bar,
    /// <summary>pressure</summary>
    DeciBar,
    /// <summary>pressure</summary>
    MilliBar,
    /// <summary>time</summary>
    GramsPerM2,
    /// <summary>Pressure</summary>
    //agrassi_FI21-01-2010_1
    MilliSecond,
    /// <summary>time</summary>
    TenthOfSecond,
    /// <summary>time</summary>
    Second,
    /// <summary>time</summary>
    Minute,
    /// <summary>time</summary>
    Hour,
    /// <summary>specific weight</summary>
    KgPerCm,
    /// <summary>degrees centigrade</summary>
    DegreeCentigrade,
    /// <summary>distance</summary>
    SquareMetre,
    Newton,
    NewtonPerCentiMetre
  }

  public class SyncroConverter
  {
    public static readonly string SYNCRO_STYLE_DATETIME_WRONG_STRING =
      SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(DateTime.MinValue);

    private SyncroConverter()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static Decimal Convert(decimal value, UnitMeasure from, UnitMeasure to)
    {
      //shortcut
      if (from == to)
        return value;

      switch (from)
      {
        case UnitMeasure.NoUnitMeasure:
        case UnitMeasure.Percentage:
        case UnitMeasure.GramsPerM2:
        case UnitMeasure.TenthOfSecond:
        case UnitMeasure.Second:
        case UnitMeasure.Minute:
        case UnitMeasure.Hour:
        case UnitMeasure.SquareMetre:
          //conversion not needed or not implemented yet
          return value;

        //speed unit measure conversions
        case UnitMeasure.MetresPerMinute:
          if (to == UnitMeasure.MilliMetresPerMinute)
            return value * 1000;
          break;
        case UnitMeasure.MilliMetresPerMinute:
          if (to == UnitMeasure.MetresPerMinute)
            return value / 1000;
          break;

        //distance unit measure conversions
        case UnitMeasure.DeciMicron:
          if (to == UnitMeasure.Micron)
            return value / 10;
          if (to == UnitMeasure.DeciMilliMetre)
            return value / 1000;
          if (to == UnitMeasure.MilliMetre)
            return value / 10000;
          if (to == UnitMeasure.Metre)
            return value / 10000000;
          break;
        case UnitMeasure.Micron:
          if (to == UnitMeasure.DeciMicron)
            return value * 10;
          if (to == UnitMeasure.DeciMilliMetre)
            return value / 100;
          if (to == UnitMeasure.MilliMetre)
            return value / 1000;
          if (to == UnitMeasure.Metre)
            return value / 1000000;
          break;
        case UnitMeasure.DeciMilliMetre:
          if (to == UnitMeasure.DeciMicron)
            return value * 1000;
          if (to == UnitMeasure.Micron)
            return value * 100;
          if (to == UnitMeasure.MilliMetre)
            return value / 10;
          if (to == UnitMeasure.Metre)
            return value / 10000;
          break;
        case UnitMeasure.MilliMetre:
          if (to == UnitMeasure.DeciMicron)
            return value * 10000;
          if (to == UnitMeasure.Micron)
            return value * 1000;
          if (to == UnitMeasure.DeciMilliMetre)
            return value * 10;
          if (to == UnitMeasure.Metre)
            return value / 1000;
          break;
        case UnitMeasure.Metre:
          if (to == UnitMeasure.DeciMicron)
            return value * 10000000;
          if (to == UnitMeasure.Micron)
            return value * 1000000;
          if (to == UnitMeasure.MilliMetre)
            return value * 1000;
          if (to == UnitMeasure.DeciMilliMetre)
            return value * 10000;
          break;

        //pressure unit measure conversions
        case UnitMeasure.Bar:
          if (to == UnitMeasure.DeciBar)
            return value * 10;
          if (to == UnitMeasure.MilliBar)
            return value * 1000;
          break;
        case UnitMeasure.DeciBar:
          if (to == UnitMeasure.Bar)
            return value / 10;
          if (to == UnitMeasure.MilliBar)
            return value * 100;
          break;
        case UnitMeasure.MilliBar:
          if (to == UnitMeasure.Bar)
            return value / 1000;
          if (to == UnitMeasure.DeciBar)
            return value / 10;
          break;

        default:
          return value;
      }
      return value;
    }

    public static string ConvertString(string value, UnitMeasure from, UnitMeasure to)
    {
      return Convert(int.Parse(value), from, to).ToString();
    }

    public static string ConvertScoreMeasures(string value, UnitMeasure from, UnitMeasure to)
    {
      bool digit = true, option = false;
      int decimalPart = 0;
      double measure = 0.0;
      string s = string.Empty;
      for (int i = 0; i < value.Length; i++)
      {
        char c = value.ToCharArray(i, 1)[0];
        if (char.IsDigit(c))
        {
          digit = true;
          if (decimalPart > 0)
            measure += (double)(c - '0') / Math.Pow(10.0, (double)decimalPart);
          else
            measure = measure * 10 + c - '0';
        }
        else if ((c == '.') || (c == ','))
          decimalPart++;
        if (((!char.IsDigit(c)) && (c != '.') && (c != ',')) || (i == value.Length - 1))
        {
          if (digit)
          {
            if (option)
              s += measure.ToString();
            else
              s += Convert((decimal)measure, from, to).ToString();
            measure = 0.0;
          }
          if ((char.IsLetter(c) || (c == '+') || (c == '-') || (c == '*')))
            option = true;
          else if (char.IsWhiteSpace(c))
            option = false;
          if (i != value.Length - 1) s += c.ToString();
          digit = false;
          decimalPart = 0;
        }
      }
      return s;
    }

    /// <summary>
    /// Convert a UnitMeasure to a proper short string.
    /// </summary>
    /// <param name="unitMeasure">the UnitMeasure to convert.</param>
    /// <returns>
    /// A string representing the specified UnitMeasure.
    /// If any string has been foreseen for the specified UnitMeasure then  a "?" is returned.
    /// </returns>
    public static string ToString(UnitMeasure unitMeasure)
    {
      switch (unitMeasure)
      {
        case UnitMeasure.NoUnitMeasure:
          return String.Empty;
        case UnitMeasure.SquareMetre:
          return "m2";
        case UnitMeasure.Metre:
          return "m";
        case UnitMeasure.MilliMetre:
          return "mm";
        case UnitMeasure.DeciMilliMetre:
          return "\u215F10 mm";
        case UnitMeasure.Micron:
          //agrassi_FI29-02-08
          //return "\u03FBm(micron)";
          return "\u00B5m";
        case UnitMeasure.DeciMicron:
          //agrassi_FI29-02-08
          //return "\u215F10 \u03FB(deciMicron)";
          return "\u215F10 \u00B5m";
        case UnitMeasure.Percentage:
          return "%";
        case UnitMeasure.MetresPerMinute:
          return "m/min";
        case UnitMeasure.MilliMetresPerMinute:
          return "mm/min";
        case UnitMeasure.Bar:
          return "bar";
        case UnitMeasure.DeciBar:
          return "\u215F10 bar";
        case UnitMeasure.MilliBar:
          return "\u215F1000 bar";
        case UnitMeasure.KgPerCm:
          return "Kg/cm";
        //agrassi_FI21-01-2010_1
        case UnitMeasure.MilliSecond:
          return "ms";
        case UnitMeasure.TenthOfSecond:
          return "\u215F10 secs";
        case UnitMeasure.Second:
          return "secs";
        case UnitMeasure.Minute:
          return "min";
        case UnitMeasure.Hour:
          return "hours";
        case UnitMeasure.GramsPerM2:
          return "gr/m2";
        case UnitMeasure.DegreeCentigrade:
          return "°C";
        case UnitMeasure.NewtonPerCentiMetre:
          return "N/cm";
        case UnitMeasure.Newton:
          return "N";
        default:
          return "?";
      }
    }

    public static NumberFormatInfo NumberFormat(int decimalDigits)
    {
      NumberFormatInfo numberFormat = new CultureInfo("en-US", true).NumberFormat;
      numberFormat.NumberGroupSeparator = "";
      numberFormat.NumberDecimalDigits = decimalDigits;
      return numberFormat;
    }

    /// <summary>
    /// Converts the seconds passed as parameter into a string formatted as hh:mm:ss
    /// </summary>
    /// <param name="seconds">number of seconds</param>
    /// <returns>the hh:mm:ss formatted string</returns>
    public static string ConvertToFormattedTime(int seconds)
    {
      TimeSpan timeSpan;
      timeSpan = TimeSpan.FromSeconds(seconds);
      return ((timeSpan.Hours.ToString().Length == 1) ? "0" : "") + timeSpan.Hours.ToString() + ":" +
        ((timeSpan.Minutes.ToString().Length == 1) ? "0" : "") + timeSpan.Minutes.ToString() + ":" +
        ((timeSpan.Seconds.ToString().Length == 1) ? "0" : "") + timeSpan.Seconds.ToString();
    }

    /// <summary>
    /// Converts a Syncro style dateTime string (day of week, year, month, day, hour minute, second, millisecond into a System.DateTime
    /// In case of error the value DateTime.MinValue will be returned.
    /// </summary>
    /// <param name="str">the Syncro style DateTime string to convert</param>
    /// <returns></returns>
    public static DateTime ConvertSyncroStyleDateTimeStringToDateTime(string str)
    {
      if (str == null || str.Trim().Equals(string.Empty))
      {
        //Logger.LogError("SyncroConverter", "ConvertSyncroStyleDateTimeStringToDateTime - \"" + str + "\" invalid parameter (null or empty). The value \"" + DateTime.MinValue.ToString() + "\" will be returned.");
        return DateTime.MinValue;
      }
      if (str.Equals(SYNCRO_STYLE_DATETIME_WRONG_STRING)
        || str.Equals("0 0 0 0 0 0 0 0")) //possibly sent from server because of ZeroMemory of C
      {
        return DateTime.MinValue;
      }
      string[] tokens = str.Trim().Split(' ');
      if (tokens.Length != 8)
      {
        Logger.LogError("SyncroConverter", "ConvertSyncroStyleDateTimeStringToDateTime - \"" + str + "\" invalid parameter (wrong number of tokens). The value \"" + DateTime.MinValue.ToString() + "\" will be returned.");
        return DateTime.MinValue;
      }
      DateTime data = new DateTime();
      try
      {
        data = new DateTime(
          System.Convert.ToUInt16(tokens[3]),	//year
          System.Convert.ToUInt16(tokens[2]),	//month
          System.Convert.ToUInt16(tokens[1]),	//day
          System.Convert.ToUInt16(tokens[4]),	//hour
          System.Convert.ToUInt16(tokens[5]),	//minute
          System.Convert.ToUInt16(tokens[6]),	//second
          System.Convert.ToUInt16(tokens[7]));	//millisecond
      }
      catch (Exception ex)
      {
        Logger.LogError("SyncroConverter", "ConvertSyncroStyleDateTimeStringToDateTime - \"" + str + "\" invalid parameter, an error occured during conversion. The value \"" + DateTime.MinValue.ToString() + "\" will be returned. ", ex);
        return DateTime.MinValue;
      }
      return data;
    }

    /// <summary>
    /// Converts a System.Datetime to Syncro style dateTime string (day of week, year, month, day, hour minute, second, millisecond
    /// /// NOTE: NOT to be used for database getVer method
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string ConvertDateTimeToSyncroStyleDateTimeString(DateTime data)
    {
      return ((int)data.DayOfWeek).ToString() +	//Specifies the current day of the week; Sunday = 0, Monday = 1, and so on. 
        " " + addZerosInFront(data.Day.ToString(), 2) +
        " " + addZerosInFront(data.Month.ToString(), 2) +			//Specifies the current month; January = 1, February = 2, and so on. 
        " " + addZerosInFront(data.Year.ToString(), 4) +
        " " + addZerosInFront(data.Hour.ToString(), 2) +
        " " + addZerosInFront(data.Minute.ToString(), 2) +
        " " + addZerosInFront(data.Second.ToString(), 2) +
        " " + data.Millisecond;
    }

    /// <summary>
    /// Converts a System.Datetime to Syncro style dateTime string (day of week, year, month, day, hour minute, second, millisecond
    /// NOTE: to be used for database getVer method
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string ConvertDateTimeToSyncroStyleDateTimeStringNoZerosInFront(DateTime data)
    {
      return ((int)data.DayOfWeek).ToString() +	//Specifies the current day of the week; Sunday = 0, Monday = 1, and so on. 
        " " + data.Day.ToString() +
        " " + data.Month.ToString() +			//Specifies the current month; January = 1, February = 2, and so on. 
        " " + data.Year.ToString() +
        " " + data.Hour.ToString() +
        " " + data.Minute.ToString() +
        " " + data.Second.ToString() +
        " " + data.Millisecond;
    }

    public static int convertToInt(string toConvert, int defaultValue)
    {
      int res = defaultValue;

      try
      {
        res = System.Convert.ToInt32(toConvert);
      }
      catch
      {
        res = defaultValue;
      }

      return res;
    }

    public static string ConvertDateTimeToSyncroStyleDateTimeString(DateTime data, bool DayOfWeekPresent)
    {
      string s = ConvertDateTimeToSyncroStyleDateTimeString(data);
      if (DayOfWeekPresent)
        return s;
      else
        return s.Substring(s.IndexOf(' ') + 1);
    }

    private static string addZerosInFront(string s, int finalLenght)
    {
      while (s.Length < finalLenght)
        s = "0" + s;
      return s;
    }

    public static string ConvertDateTimeToStrForDataGridView(object data)
    {
      DateTime toConvert = DateTime.MinValue;

      try
      {
        toConvert = System.Convert.ToDateTime(data);
      }
      catch (Exception)
      {
        toConvert = DateTime.MinValue;
      }

      return ConvertDateTimeToStrForDataGridView(toConvert.ToLocalTime());
    }

    public static bool ConvertByteToBoolean(string boolean)
    {
      if ((boolean == "0") || (boolean.ToUpper() == "FALSE"))
        return false;
      else
        return true;
    }

    //ABELLO_FI26/08/2010_1 Aggiunto il trace degli Stop
    public static string ConvertDateTimeWithMillisecondsToStrForDataGridView(DateTime data)
    {
      return data.ToString("HH:mm:ss:ffff dd/MM/yy").Replace('.', ':');
    }

    //FOS2 ABello
    public static string ConvertDateTimeToStrForDataGridView(DateTime data)
    {
      return data.ToString("HH:mm:ss dd/MM/yy").Replace('.', ':');
    }

    //FOS2 ABello
    public static string ConvertDateToStrForDataGridView(DateTime data)
    {
      return data.ToString("dd/MM/yy");
    }

    //FOS2 ABello
    public static string ConvertTimeToStrForDataGridView(DateTime data)
    {
      return data.ToString("HH:mm:ss").Replace('.', ':');
    }

    //FOS2 ABello
    public static DateTime ConvertStrDateTimeToDateTimeForDataGridView(string data)
    {
      //HH:mm:ss dd/MM/yy
      DateTime date = DateTime.MinValue;

      int day, month, year, hour, minute, second, millisecond;
      day = month = year = hour = minute = second = millisecond = 0;
      try
      {
        string[] tokens = data.Split(new char[] { '/', '.', ':', ' ' });
        if (tokens == null || tokens.Length < 6)
          return date;

        //at least day, month and year have been specified
        hour = System.Convert.ToInt32(tokens[0]);
        minute = System.Convert.ToInt32(tokens[1]);
        second = System.Convert.ToInt32(tokens[2]);
        day = System.Convert.ToInt32(tokens[3]);
        month = System.Convert.ToInt32(tokens[4]);
        year = System.Convert.ToInt32(tokens[5]);
        if (year < 1000)
        {
          year = 2000 + System.Convert.ToInt32(tokens[5]);
        }

        date = new DateTime(year, month, day, hour, minute, second, millisecond);
      }
      catch (Exception)
      {
        return date;
      }
      return date;
    }

    public static DateTime ConvertStrTimeToDateTimeForDataGridView(string data)
    {
      DateTime date = DateTime.MinValue;

      int day, month, year, hour, minute, second, millisecond;
      day = month = year = hour = minute = second = millisecond = 0;
      try
      {
        string[] tokens = data.Split(new char[] { '/', '.', ':', ' ' });
        if (tokens == null || tokens.Length < 3)
          return date;
        //at least day, month and year have been specified
        hour = System.Convert.ToInt32(tokens[0]);
        minute = System.Convert.ToInt32(tokens[1]);
        second = System.Convert.ToInt32(tokens[2]);
        if (!(tokens.Length < 4))
        {//at least also hour and millisecond have been specified
          millisecond = System.Convert.ToInt32(tokens[3]);
        }

        date = new DateTime(year, month, day, hour, minute, second, millisecond);
      }
      catch (Exception)
      {
        return date;
      }
      return date;
    }

    public static DateTime ConvertStrDateToDateTimeForDataGridView(string data)
    {
      DateTime date = DateTime.MinValue;

      int day, month, year, hour, minute, second, millisecond;
      day = month = year = hour = minute = second = millisecond = 0;
      try
      {
        string[] tokens = data.Split(new char[] { '/', '.', ':', ' ' });
        if (tokens == null || tokens.Length < 3)
          return date;
        //at least day, month and year have been specified
        day = System.Convert.ToInt32(tokens[0]);
        month = System.Convert.ToInt32(tokens[1]);
        //PMASSARO_FI27/05/08 - BUG. Correct Association Else The Year Was Made By Two Characters.
        //Verified Problem During Printing Of "Delivery Date" On Pallet Label:
        //The Date Was Viewed Like Uncorrect Format So Was Replaced With The Current One

        /* year = System.Convert.ToInt32(tokens[2]); */

        year = System.Convert.ToInt32(tokens[2]);
        if (year < 1000)
        {
          year = 2000 + System.Convert.ToInt32(tokens[2]);
        }

        date = new DateTime(year, month, day, hour, minute, second, millisecond);
      }
      catch (Exception)
      {
        return date;
      }
      return date;
    }
  }
}
