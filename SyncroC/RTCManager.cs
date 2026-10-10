using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

using SyncroC.Utility;
using SyncroC.DataBase;

namespace SyncroC
{
  public class RTCManager
  {
    /// <summary>The one and only RTCManager instance of the application.</summary>
    private static RTCManager rtcManager;

    /// <summary>
    /// Event raised when an RTCManager property changes value
    /// </summary>
    public event RTCManagerPropertyChangedHandler OnRTCManagerPropertyChanged;
    public event EventHandler TwinConfigValidationSucceeded;

    /// <summary>Contains all the data about active alarms.</summary>
    public DataSet ActiveAlarms;

    private bool isIdleShift = false;
    public bool IsIdleShift
    {
      get { return isIdleShift; }
      set
      {
        if (isIdleShift != value && OnRTCManagerPropertyChanged != null)
          OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs("IsIdleShift", value));
        isIdleShift = value;
      }
    }

    private bool zone1IsActive = true;
    private bool zone2IsActive = true;

    #region tables constants

    //MACHINE TABLES
    //Syncro
    ///<summary>Hardware syncro alarms table name.</summary>
    public readonly static string SYC_HW = "SYCHW";
    /// <summary>Software syncro alarms table name.</summary>
    public readonly static string SYC_SW = "SYCSW";
    //wet end
    /// <summary>Splicer 7 alarms table name.</summary>
    public readonly static string SPLICER7 = "SPL7";
    /// <summary>Single facer 3 alarms table name.</summary>
    public readonly static string SF3 = "SF3";
    /// <summary>Splicer 6 alarms table name.</summary>
    public readonly static string SPLICER6 = "SPL6";
    /// <summary>Splicer 5 alarms table name.</summary>
    public readonly static string SPLICER5 = "SPL5";
    /// <summary>Single facer 2 alarms table name.</summary>
    public readonly static string SF2 = "SF2";
    /// <summary>Splicer 4 alarms table name.</summary>
    public readonly static string SPLICER4 = "SPL4";
    /// <summary>Splicer 3 alarms table name.</summary>
    public readonly static string SPLICER3 = "SPL3";
    /// <summary>Single facer 1 alarms table name.</summary>
    public readonly static string SF1 = "SF1";
    /// <summary>Splicer 2 alarms table name.</summary>
    public readonly static string SPLICER2 = "SPL2";
    /// <summary>Splicer 1 alarms table name.</summary>
    public readonly static string SPLICER1 = "SPL1";
    /// <summary>Pre-heater tern alarms table name.</summary>
    public readonly static string TERN = "PHT";
    /// <summary>Glue unit alarms table name.</summary>
    public readonly static string GLUEUNIT = "GU";
    /// <summary>Double backer alarms table name.</summary>
    public readonly static string DOUBLEBACKER = "DB";
    //dry end
    /// <summary>Rotary alarms table name.</summary>
    public static readonly string ROTARY = "RSH";
    /// <summary>Twin400 alarms table name.</summary>
    public static readonly string SLITTERSCORER = "SS";
    /// <summary>Knife alarms table name.</summary>
    public static readonly string KNIFE = "KN";
    /// <summary>Stacker 1 alarms table name.</summary>
    public static readonly string STACKER1 = "STK1";
    /// <summary>Stacker 2 alarms table name.</summary>
    public static readonly string STACKER2 = "STK2";
    /// <summary>Stacker 3 alarms table name.</summary>
    public static readonly string STACKER3 = "STK3";
    /// <summary>Material handling alarms table name.</summary>
    public static readonly string MTH = "MTH";
    /// <summary>External wet end control alarms table name.</summary>
    public static readonly string EXTERN_WEC = "EXTWEC";

    //MACHINE COLUMNS
    /// <summary>Column name for alarm key".</summary>
    public static readonly string KEY = "Key"; //0
    /// <summary>Column name for alarm description".</summary>
    public static readonly string DESCRIPTION = "Description"; //1
    /// <summary>Column name for alarm code Zone".</summary>
    public static readonly string CODE_ZONE = "CodeZone";	//2
    /// <summary>Column name for alarm Zone description".</summary>
    public static readonly string DESCRIPTION_ZONE = "DescriptionZone";//3
    /// <summary>Column name for alarm begin time".</summary>
    public static readonly string BEGIN_TIME = "BeginTime";//4
    /// <summary>Column name for alarm present".</summary>
    public static readonly string PRESENT = "Present";//5

    #endregion

    private RTCManager()
    {
      ActiveAlarms = new DataSet();

      #region tables initialization

      //MACHINE TABLES
      //Syncro
      //ActiveAlarms.Tables.Add(RTCManager.SYC_HW);
      ActiveAlarms.Tables.Add(RTCManager.SYC_SW);
      //wet end
      ActiveAlarms.Tables.Add(RTCManager.SPLICER7);
      ActiveAlarms.Tables.Add(RTCManager.SF3);
      ActiveAlarms.Tables.Add(RTCManager.SPLICER6);
      ActiveAlarms.Tables.Add(RTCManager.SPLICER5);
      ActiveAlarms.Tables.Add(RTCManager.SF2);
      ActiveAlarms.Tables.Add(RTCManager.SPLICER4);
      ActiveAlarms.Tables.Add(RTCManager.SPLICER3);
      ActiveAlarms.Tables.Add(RTCManager.SF1);
      ActiveAlarms.Tables.Add(RTCManager.SPLICER2);
      ActiveAlarms.Tables.Add(RTCManager.SPLICER1);
      ActiveAlarms.Tables.Add(RTCManager.TERN);
      ActiveAlarms.Tables.Add(RTCManager.GLUEUNIT);
      ActiveAlarms.Tables.Add(RTCManager.DOUBLEBACKER);
      //dry end
      ActiveAlarms.Tables.Add(RTCManager.ROTARY);
      ActiveAlarms.Tables.Add(RTCManager.SLITTERSCORER);
      ActiveAlarms.Tables.Add(RTCManager.KNIFE);
      ActiveAlarms.Tables.Add(RTCManager.STACKER1);
      ActiveAlarms.Tables.Add(RTCManager.STACKER2);
      ActiveAlarms.Tables.Add(RTCManager.STACKER3);
      ActiveAlarms.Tables.Add(RTCManager.MTH);
      ActiveAlarms.Tables.Add(RTCManager.EXTERN_WEC);

      foreach (DataTable dataTable in ActiveAlarms.Tables)
      {
        //MACHINE COLUMNS
        dataTable.Columns.Add(RTCManager.KEY);//from ActiveAlarm dataset
        dataTable.Columns[RTCManager.KEY].AllowDBNull = false;
        dataTable.Columns.Add(RTCManager.DESCRIPTION);//from Alarm.xml translations file
        dataTable.Columns.Add(RTCManager.CODE_ZONE);//from Alarm.xml translations file
        dataTable.Columns.Add(RTCManager.DESCRIPTION_ZONE);//from alarm.xml translations file (table zone)
        dataTable.Columns.Add(new DataColumn(RTCManager.BEGIN_TIME, typeof(DateTime)));//from ActiveAlarm dataset
        dataTable.Columns.Add(new DataColumn(RTCManager.PRESENT, typeof(bool)));//from ActiveAlarm dataset
        dataTable.PrimaryKey =
          new DataColumn[] { dataTable.Columns[RTCManager.KEY] };
      }

      #endregion
    }

    //agrassi_FI07-07-08
    public void TranslateAlarms()
    {
      Alarm alm = DBManager.getDBManager().alarm;
      foreach (DataTable dt in ActiveAlarms.Tables)
      {
        foreach (DataRow dr in dt.Rows)
        {
          dr[DESCRIPTION] = alm.getTranslatedAlarm(dr[RTCManager.KEY].ToString());
          dr[RTCManager.DESCRIPTION_ZONE] = alm.getTranslatedZoneFromAlarm(dr[RTCManager.KEY].ToString());
        }
      }
    }

    /// <summary>
    /// Factory.
    /// </summary>
    /// <returns>The one and only RTCManager object of the application.</returns>
    public static RTCManager getRTCManager()
    {
      if (rtcManager == null)
        rtcManager = new RTCManager();
      return rtcManager;
    }

    /// <summary>
    /// DBManager collector for commands to be interpreted by the reflection.
    /// only set of bool property of the RTC class is supported
    /// </summary>
    /// <param name="msg">a string representing the command to be interpreted.</param>
    public void Reflect(string msg)
    {
      //check for msg correctness and parsing
      if (msg == null || msg.Equals(string.Empty))
      {
        Logger.LogError(this, "Reflect - msg was null or empty.");
        return;
      }

      //graffaelli_FI09/11/2011_2 - Aggiunto log su client
      Logger.LogError(this, "Reflect - RTC - msg parameter: " + msg);

      int index = msg.IndexOf('=');
      if (index == -1)
      {
        Logger.LogError(this, "Reflect - invalid command (missing '=')\"" + msg + "\".");
        return;
      }
      string propertyName = msg.Substring(0, index).Trim();
      string value = msg.Substring(index + 1).Trim();

      PropertyInfo propertyInfo = null;
      try
      {
        propertyInfo = this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
      }
      catch (AmbiguousMatchException e)
      {
        Logger.LogError(this, "Reflect - two properties whith name " + propertyName, e.InnerException);
        e.ToString();
        propertyInfo = this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
      }
      if (propertyInfo == null)
      {
        Logger.LogError(this, "Reflect - cannot find property \"" + propertyName + "\" into RTCManager class.");
        return;
      }

      try
      {
        //settaggio del valore della property con il valore specificato castato al tipo della property stessa.
        propertyInfo.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType), null);
      }
      catch (InvalidCastException ex)
      {//may be thrown by the ChangeType method
        Logger.LogError(this, "Reflect - string \"" + value + "\" is not compatible with the property \"" + propertyInfo.Name + "\"", ex);
        return;
      }
      catch (Exception ex)
      {//may be thrown by the SetValue method
        Logger.LogError(this, "Reflect - Error setting property \"" + propertyInfo.Name + "\" with value \"" + value + "\"", ex);
        return;
      }
    }

    /// <summary>
    /// Interpretes throw a switch commands for database.
    /// </summary>
    /// <param name="msg">Command to be interpreted.</param>
    public void Collect(string text)
    {
      if (text == null || text.Equals(string.Empty))
      {
        Logger.LogError(this, "Collect - text parameter was null or empty.");
        return;
      }
      //extracts command
      int index = text.IndexOf('.');
      if (index == -1)
      {
        Logger.LogError(this, "Collect - Invalid command \"" + text + "\"; \".\" is missing.");
        return;
      }
      string command = text.Substring(0, index);
      text = text.Substring(index + 1);
      //extracts machine the command is related on and gets the table
      index = text.IndexOf('(');
      if (index == -1)
      {
        Logger.LogError(this, "Collect - Invalid command \"" + text + "\"; \"(\" is missing.");
        return;
      }
      string machine = text.Substring(0, index).ToUpper();

      /* 
       * REMOVE THE FOLLOWING LINES WHEN THE SYC_HW AND SYC_SW TABLES WILL BE COMPLETED UNIFIED
       */
      bool hw = false;
      if (machine.Equals(RTCManager.SYC_HW))
      {
        machine = RTCManager.SYC_SW;
        hw = true;
      }
      /* * * * * */
      //until here

      DataTable dt = ActiveAlarms.Tables[machine];
      if (dt == null)
      {
        Logger.LogError(this, "Collect - Invalid machine \"" + machine + "\".");
        return;
      }
      text = text.Substring(index + 1);
      //removes end paretesis 	
      if (!(text.EndsWith(")")))
      {
        Logger.LogError(this, "Collect - Invalid command \"" + text + "\"; \")\" is missing.");
        return;
      }
      text = text.Substring(0, text.Length - 1);

      object[] almParameters;
      Alarm alm;
      switch (command)
      {
        case "TwinConfigValidationSucceeded":
          if (TwinConfigValidationSucceeded != null)
            TwinConfigValidationSucceeded(this, new EventArgs());
          break;
        case "update":
        case "Update":
          //update current alarm status
          if (text.Equals(String.Empty))
          {
            /* 
             * REMOVE THE FOLLOWING LINES WHEN THE SYC_HW AND SYC_SW TABLES WILL BE COMPLETED UNIFIED
             * questo merge è solo sugli allarmi attivi!!! per lo storico gli allarmi hardware sono nella loro 
             * tabella che non è visualizzata
             */
            if (!machine.Equals(RTCManager.SYC_SW))
            {
              /* * * * * * */
              //until here

              dt.Clear();
              //signal absence of any alarm for the machine
              if (OnRTCManagerPropertyChanged != null)
                OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(machine + "Alarms", dt.Rows.Count));

              //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
              if (OnRTCManagerPropertyChanged != null)
                OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(machine + "Warnings", dt.Rows.Count));

              return;

              /* 
               * REMOVE THE FOLLOWING LINES WHEN THE SYC_HW AND SYC_SW TABLES WILL BE COMPLETED UNIFIED
               */
            }
            else
            {
              foreach (DataRow row in dt.Rows)
              {
                if ((hw && !((string)row[0]).StartsWith("GENERAL_"))
                  || !hw && ((string)row[0]).StartsWith("GENERAL_"))
                {
                  row[RTCManager.PRESENT] = false;
                }
              }
            }
            /* * * * * */
            //until here

          }
          string[] elements = text.Split(new char[] { Command.PARAMETER_SEPARATOR, ',' });

          if ((!text.Equals(string.Empty)) && (elements.Length % 2 != 0))
          {
            Logger.LogError(this, "Collect - Invalid command \"" + text + "\"; Odd elements number.");
            return;
          }

          //ABELLO_FI19/09/2008_1 Non si resettava l'allarme sull'iconcina
          try
          {
            //remove cleared alarms from the table
            //ABELLO_FI19/09/2008_1 Non si resettava l'allarme sull'iconcina
            //foreach (DataRow row in dt.Rows)
            for (int j = 0; j < dt.Rows.Count; j++)
            {
              DataRow row = dt.Rows[j];
              bool found = false;
              int i = 0;
              while (i < elements.Length && !found)
              {
                if (row[RTCManager.KEY].Equals(elements[i]))
                  found = true;
                else
                  i += 2;
              }
              if (!found)
              {

                /* 
                 * REMOVE THE FOLLOWING LINES WHEN THE SYC_HW AND SYC_SW TABLES WILL BE COMPLETED UNIFIED
                 */
                if (machine.Equals(RTCManager.SYC_SW))
                {
                  if ((hw && !((string)row[0]).StartsWith("GENERAL_"))
                    || !hw && ((string)row[0]).StartsWith("GENERAL_"))
                  {
                    row[RTCManager.PRESENT] = false;

                    //ABELLO_FI27/08/2008_1 bug fix with the Alarms Date Time
                    dt.Rows.Remove(row);
                    j--;
                  }
                }
                else
                {
                  /* * * * * */
                  //until here

                  row[RTCManager.PRESENT] = false;

                  //ABELLO_FI27/08/2008_1 bug fix with the Alarms Date Time
                  dt.Rows.Remove(row);
                  j--;

                  /* * * * * */
                }
                /* * * * * */
                //until here

              }
            }

            //add set alarms
            alm = DBManager.getDBManager().alarm;
            for (int i = 0; i < elements.Length - 1; i += 2)
            {
              almParameters = new object[6];
              almParameters[0] = elements[i]; //key
              almParameters[1] = alm.getTranslatedAlarm(almParameters[0].ToString()); //description
              almParameters[2] = alm.getZoneCodeFromAlarm(almParameters[0].ToString()); //code zone
              almParameters[3] = alm.getTranslatedZoneFromAlarm(almParameters[0].ToString()); //description zone
              almParameters[4] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(elements[i + 1]); //begin time
              almParameters[5] = true; //present
              setAlarm(almParameters, dt);
            }
          }
          catch (Exception ex)
          {
            //ABELLO_FI19/09/2008_1 Non si resettava l'allarme sull'iconcina
            Logger.LogError(this, ex);
          }

          //signal presence/absence of any alarm for the machine
          if (OnRTCManagerPropertyChanged != null)
            OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(machine + "Alarms", this.getAlarmPresentOnMachine(machine)));

          //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
          if (OnRTCManagerPropertyChanged != null)
            OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(machine + "Warnings", this.getWarningPresentOnMachine(machine)));

          break;
        case "Force":
          //force an alarm
          if (text.Equals(String.Empty))
          {
            Logger.LogError(this, "Collect - Empty Force command \"" + text + "\".");
            return;
          }
          almParameters = new object[6];
          index = text.IndexOf(',');
          if (index == -1)
          {
            Logger.LogError(this, "Collect - Invalid command \"" + text + "\"; \",\" missing.");
            return;
          }
          alm = DBManager.getDBManager().alarm;
          almParameters[0] = text.Substring(0, index);	//key
          almParameters[1] = alm.getTranslatedAlarm(almParameters[0].ToString()); //description
          almParameters[2] = alm.getZoneCodeFromAlarm(almParameters[0].ToString()); //code zone
          almParameters[3] = alm.getTranslatedZoneFromAlarm(almParameters[0].ToString()); //description zone
          almParameters[4] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(text.Substring(index + 1));//begin time
          almParameters[5] = true;//present
          setAlarm(almParameters, dt);
          break;
        //Rimossi gli allarmi dalla barra di stato quando una zona non è attiva. ABELLO_FI20/10/2009_1
        case "setActiveZone":
          if (!text.Equals(String.Empty))
          {
            index = text.IndexOf(',');
            if (index != -1)
              this._setActiveZone(SyncroConverter.ConvertByteToBoolean(text.Substring(0, index)), SyncroConverter.ConvertByteToBoolean(text.Substring(index + 1)));
          }
          break;
        default:
          Logger.LogError(this, "Collect - Unknown command \"" + command + "\"; .");
          return;
      }
    }

    /// <summary>
    /// Sets an alarm into the specified table
    /// </summary>
    /// <param name="alm">alarm details</param>
    /// <param name="dt">table where to insert the alarm</param>
    private void setAlarm(object[] alm, DataTable dt)
    {
      if (alm == null || dt == null)
      {
        Logger.LogError(this, "setAlarm - Invalid parameter.");
        return;
      }
      if (alm.Length != dt.Columns.Count)
      {
        Logger.LogError(this, "setAlarm - Attempted to add " + alm.Length + " elements to the table \"" + dt.TableName + "\" which has " + dt.Columns.Count + " columns.");
        return;
      }
      DataRow dr;
      try
      {
        dr = dt.Rows.Find(alm[0]);
      }
      catch (MissingPrimaryKeyException ex)
      {
        Logger.LogError(this, "setAlarm - Missing primary key for table \"" + dt.TableName + "\"", ex);
        return;
      }
      if (dr != null)
      {//alarm already present
        if (dr[RTCManager.PRESENT].Equals(false))
          dr[RTCManager.PRESENT] = true;
      }
      else
      {
        try
        {
          dt.Rows.Add(alm);
        }
        catch (InvalidCastException ex)
        {
          Logger.LogError(this, "setAlarm - Invalid cast adding record to the table \"" + dt.TableName + "\"", ex);
          return;
        }
        catch (ConstraintException ex)
        {
          Logger.LogError(this, "setAlarm - Constraint exception adding record to the table \"" + dt.TableName + "\"", ex);
          return;
        }
        catch (ArgumentException ex)
        {
          Logger.LogError(this, "setAlarm - ArgumentException adding record to the table \"" + dt.TableName + "\"", ex);
          return;
        }
        catch (NoNullAllowedException ex)
        {
          Logger.LogError(this, "setAlarm - NoNullAllowedException adding record to the table \"" + dt.TableName + "\"", ex);
          return;
        }
        //catch (Exception ex)
        //{
        //  Logger.LogError(this, "setAlarm - Exception adding record to the table \"" + dt.TableName + "\"", ex);
        //  return;
        //}
      }
    }

    public void _setActiveZone(bool theZone1IsActive, bool theZone2IsActive)
    {
      bool toSendEvent = false;

      if (this.zone1IsActive != theZone1IsActive)
      {
        this.zone1IsActive = theZone1IsActive;
        toSendEvent = true;
      }

      if (this.zone2IsActive != theZone2IsActive)
      {
        this.zone2IsActive = theZone2IsActive;
        toSendEvent = true;
      }

      if (toSendEvent)
      {
        if (OnRTCManagerPropertyChanged != null)
        {
          OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(RTCManager.SYC_SW + "Alarms", this.getAlarmPresentOnMachine(RTCManager.SYC_SW)));
          OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(RTCManager.SYC_HW + "Alarms", this.getAlarmPresentOnMachine(RTCManager.SYC_HW)));
          //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
          OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(RTCManager.SYC_HW + "Warnings", this.getWarningPresentOnMachine(RTCManager.SYC_SW)));
          OnRTCManagerPropertyChanged(this, new RTCManagerPropertyChangedEventArgs(RTCManager.SYC_HW + "Warnings", this.getWarningPresentOnMachine(RTCManager.SYC_HW)));
        }
      }
    }

    /// <summary>
    /// Gets the number of alarms currently active on the specified machine
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public int getAlarmPresentOnMachine(string machine)
    {
      bool filterByActiveZone = false;

      if ((machine == RTCManager.SYC_SW) || (machine == RTCManager.SYC_HW))
        filterByActiveZone = true;

      if (filterByActiveZone)
      {
        DataTable dt = ActiveAlarms.Tables[machine];
        if (dt == null)
        {
          Logger.LogError(this, "isAlarmPresentOnMachine - Invalid machine \"" + machine + "\".");
          return 0;
        }

        if (dt.Rows.Count == 0)
          return 0;

        //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
        //string filter = RTCManager.PRESENT + " = 'true'";
        string filter = RTCManager.PRESENT + " = 'true'" + " AND " + RTCManager.CODE_ZONE + " <> 'Warning'";

        if (!zone1IsActive)
          //GENERAL_33 RdyToRun: Splicer 2 Not Ready To Run
          //GENERAL_35 RdyToRun: Splicer 3 Not Ready To Run
          //GENERAL_42 RdyToRun: Single Facer 1 Not Ready To Run
          //GENERAL_123 N_Stop:Splicer 2
		      //GENERAL_124 N_Stop:Splicer 3
          //GENERAL_136 N_Stop:Single Facer 1
        	//GENERAL_173 E_Stop:Splicer 2
		      //GENERAL_174 E_Stop:Splicer 3
          //GENERAL_186 E_Stop:Single Facer 1
          filter += " AND NOT(Key = 'GENERAL_33' OR Key = 'GENERAL_35' OR Key = 'GENERAL_42' OR Key = 'GENERAL_123' OR Key = 'GENERAL_124' OR Key = 'GENERAL_136' OR Key = 'GENERAL_173' OR Key = 'GENERAL_174' OR Key = 'GENERAL_186')";

        if (!zone2IsActive)
          //GENERAL_36 RdyToRun: Splicer 4 Not Ready To Run
		      //GENERAL_38 RdyToRun: Splicer 5 Not Ready To Run
          //GENERAL_43 RdyToRun: Single Facer 2 Not Ready To Run
          //GENERAL_125 N_Stop:Splicer 4
		      //GENERAL_126 N_Stop:Splicer 5
          //GENERAL_137 N_Stop:Single Facer 2
        	//GENERAL_175 E_Stop:Splicer 4
		      //GENERAL_176 E_Stop:Splicer 5
          //GENERAL_187 E_Stop:Single Facer 2
          filter += " AND NOT(Key = 'GENERAL_36' OR Key = 'GENERAL_38' OR Key = 'GENERAL_43' OR Key = 'GENERAL_125' OR Key = 'GENERAL_126' OR Key = 'GENERAL_137' OR Key = 'GENERAL_175' OR Key = 'GENERAL_176' OR Key = 'GENERAL_187')";

        DataRow[] activeRows = dt.Select(filter);

        return activeRows.Length;
      }
      else
      {
        DataTable dt = ActiveAlarms.Tables[machine];
        if (dt == null)
        {
          Logger.LogError(this, "isAlarmPresentOnMachine - Invalid machine \"" + machine + "\".");
          return 0;
        }

        if (dt.Rows.Count == 0)
          return 0;

        DataRow[] activeRows = dt.Select(RTCManager.PRESENT + " = 'true'");
        return activeRows.Length;
      }
    }

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
    /// <summary>
    /// Gets the number of alarms currently active on the specified machine
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public int getWarningPresentOnMachine(string machine)
    {
      bool filterByActiveZone = false;

      if ((machine == RTCManager.SYC_SW) || (machine == RTCManager.SYC_HW))
        filterByActiveZone = true;

      if (filterByActiveZone)
      {
        DataTable dt = ActiveAlarms.Tables[machine];
        if (dt == null)
        {
          Logger.LogError(this, "isWarningPresentOnMachine - Invalid machine \"" + machine + "\".");
          return 0;
        }

        if (dt.Rows.Count == 0)
          return 0;

        string filter = RTCManager.PRESENT + " = 'true'" + " AND " + RTCManager.CODE_ZONE + " = 'Warning'";
        DataRow[] activeRows = dt.Select(filter);

        return activeRows.Length;
      }
      else
      {
        DataTable dt = ActiveAlarms.Tables[machine];
        if (dt == null)
        {
          Logger.LogError(this, "isWarningPresentOnMachine - Invalid machine \"" + machine + "\".");
          return 0;
        }

        if (dt.Rows.Count == 0)
          return 0;

        DataRow[] activeRows = dt.Select(RTCManager.PRESENT + " = 'true'" + " AND " + RTCManager.CODE_ZONE + " = 'Warning'");
        return activeRows.Length;
      }
    }

  }


  /// <summary>
  /// Changement on a RTCManager property
  /// </summary>
  public class RTCManagerPropertyChangedEventArgs : System.EventArgs
  {
    /// <summary>
    /// The name of the RTCManager property that changed
    /// </summary>
    public string propertyName;
    /// <summary>
    /// The new value assumed by the property
    /// </summary>
    public object newValue;

    public RTCManagerPropertyChangedEventArgs(string propertyName, object newValue)
    {
      this.propertyName = propertyName;
      this.newValue = newValue;
    }
  }

  /// <summary>
  /// Delegate for handling RTCManagerPropertyChangedEventArgs events.
  /// </summary>
  public delegate void RTCManagerPropertyChangedHandler(object sender, RTCManagerPropertyChangedEventArgs args);

}
