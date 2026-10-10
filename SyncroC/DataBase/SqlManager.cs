using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Data;
using Microsoft.Win32;

namespace SyncroC.DataBase
{
  class SqlManager
  {
    private NpgsqlConnection conn;
    private static SqlManager sqlManager;
    
    /// <summary>
    /// Builder.
    /// </summary>
    public SqlManager()
    {
      this.connectToSQL();

      //ABELLO_FI07/06/11 - Aggiunto il salvataggio della TimeZone su Postgres
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED)
      {
        if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
        {
          this.doCommand
            (
            String.Format("COMMENT ON SCHEMA {0} IS '{1}';",
            SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE(),
            removeEscapeString(TimeZoneInfo.Local.ToSerializedString())), true
            );
        }
      }
    }

    /// <summary>
    /// destructor.
    /// </summary>
    ~SqlManager()
    {
      this.closeConnection();
    }

    public static string dateTimeToSQLDateTime(DateTime Date)
    {
     // Date = Date.ToUniversalTime();
      return String.Format("{0}-{1}-{2} {3}:{4}:{5}", Date.Year, Date.Month, Date.Day, Date.Hour, Date.Minute, Date.Second);
    }

    public static string removeEscapeString(string str)
    {
      string res = str;

      str = str.Replace("\\", "\\\\");
      str = str.Replace("\"", "\\\"");
      str = str.Replace("'", "\\'");
      str = str.Replace("%", "\\%");
      str = str.Replace("_", "\\_");

      return str;
    }

    /// <summary>
    /// Factory.
    /// </summary>
    /// <returns>The one and only SqlManager object of the application.</returns>
    public static SqlManager getSqlManager()
    {
      if (sqlManager == null)
        sqlManager = new SqlManager();
      return sqlManager;
    }

    public void closeConnection()
    {
      try
      {
        if (conn != null)
        {
          SyncroC.Utility.Logger.LogError(this, "Disconnessione da SQL");
          conn.Close();
        }
        else
        {
          SyncroC.Utility.Logger.LogError(this, "Disconnessione da SQL non richiesta ... già Disconnnesso");
        }
      }
      catch (Exception ex)
      {
        SyncroC.Utility.Logger.LogError(this, "Disconnessione da SQL non riuscita per Errore : " + ex.Message);
      }
    }

    public bool connectToSQL()
    {
      bool res = false;
      try
      {
        if (conn == null)
        {
          SyncroC.Utility.Logger.LogError(this, "Creazione Connessione a SQL");

          string connStr = String.Format
              ("server={0};user id={1}; password={2}; database=postgres; pooling=false;",
              SyncroC.Utility.Settings.getSettings().network.IpServerAddress,
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USER,
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_PASSWORD,
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE());

          SyncroC.Utility.Logger.LogError(this, "Creazione Connessione a SQL con stringa : " + connStr);

          conn = new NpgsqlConnection(connStr);

          SyncroC.Utility.Logger.LogError(this, "Creazione Connessione a SQL riuscita");
        }

        if (conn.State != ConnectionState.Open)
        {
          SyncroC.Utility.Logger.LogError(this, "Ri-Connessione a SQL per Stato" + conn.State.ToString());
          conn.ConnectionString = String.Format
              ("server={0};user id={1}; password={2}; database=postgres; pooling=false;",
              SyncroC.Utility.Settings.getSettings().network.IpServerAddress,
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USER,
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_PASSWORD,
              SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE());
          conn.Open();
        }

        res = true;
      }
      catch (Exception ex)
      {
        SyncroC.Utility.Logger.LogError(this, "Connessione a SQL non riuscita per Errore : " + ex.Message);
        try
        {
          if (conn != null)
          {
            if (conn.State != ConnectionState.Closed)
            {
              SyncroC.Utility.Logger.LogError(this, "Stato Connessione " + conn.State.ToString() + " forzatura a chiusa");
              conn.Close();
            }
          }
          else
          {
            SyncroC.Utility.Logger.LogError(this, "Classe Conn == null");
          }
          res = false;
        }
        catch (Exception)
        {
        }
      }

      return res;
    }

    public long getNumberOfResultForSelect(string myQuery)
    {
      long res = 0;

      if (myQuery.ToUpper().StartsWith("SELECT"))
      {
        string mystrCommand = myQuery;
        if (myQuery.Contains("*"))
        {
          mystrCommand = myQuery.Replace("*", "COUNT(*)");
          DataTable data = this.doSelect(mystrCommand);
          if (data != null)
          {
            if (data.Rows.Count != 0)
            {
              res = Convert.ToInt64(data.Rows[0].ItemArray[0]);
            }
          }
        }
      }
      else
      {
        DataTable myDataTable = this.doSelect(myQuery);
        if (myDataTable == null)
        {
          res = 0;
        }
        else
        {
          res = myDataTable.Rows.Count;
        }
      }

      return res;
    }

    public bool dropTable(string tableNameToDrop)
    {
      return this.doCommand(String.Format("DROP TABLE IF EXISTS {0};", tableNameToDrop), true);
    }

    public bool deleteAllRowInTable(string tableNameDelete)
    {
      return this.doCommand("DELETE FROM " + tableNameDelete + ";", true);
    }

    public bool copyTable(string tableNameSrc, string tableNameDest)
    {
      /*
      this.dropTable(tableNameDest);
      return this.doCommand(String.Format("CREATE TABLE {0} AS SELECT * FROM {1};", tableNameDest, tableNameSrc), true);
       * */
      this.deleteAllRowInTable(tableNameDest);
      return this.doCommand(String.Format("INSERT INTO {0} SELECT * FROM {1};", tableNameDest, tableNameSrc), true);
    }

    public long getRowsNumber(string tableName)
    {
      long res = 0;

      try
      {
        DataTable dt = this.doSelect("SELECT COUNT(*) FROM " + tableName);
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            res = (Int64)dt.Rows[0].ItemArray[0];
          }
        }
      }
      catch (Exception ex)
      {
        SyncroC.Utility.Logger.LogError(this, "getRowsNumber Errore : " + ex.Message);
        res = 0;
      }

      return res;
    }

    public bool isEmptySelectForCommand(string strCommand)
    {
      bool isEmptyResultForCommand = false;
      string mystrCommand = strCommand;
      bool count = false;

      if (strCommand.Contains("*"))
      {
        count = true;
        mystrCommand = strCommand.Replace("*", "COUNT(*)");
      }

      DataTable data = this.doSelect(mystrCommand);
      if (data == null)
      {
        isEmptyResultForCommand = true;
      }
      else
      {
        if (data.Rows.Count == 0)
          isEmptyResultForCommand = true;
        else
        {
          if (count)
          {
            if ((Int64)data.Rows[0].ItemArray[0] == 0)
              isEmptyResultForCommand = true;
          }
        }
      }

      return isEmptyResultForCommand;
    }

    public DataTable getAllDataBaseTable()
    {
      string query = String.Format("SELECT table_name FROM information_schema.tables WHERE table_schema = '{0}';", 
        SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE());

      return this.doSelect(query);
    }

    public DataTable doSelect(string myQuery, int limit, int offset)
    {
      string myQueryWithLimitAndOffset = myQuery.Substring(0, myQuery.LastIndexOf(';')) + " LIMIT " + limit.ToString() + " OFFSET " + offset.ToString() + ";";

      return this.doSelect(myQueryWithLimitAndOffset);
    }

    public DataTable doSelect(string strSelect)
    {
      DataTable data = null;

      if (this.connectToSQL())
      {
        try
        {
          data = new DataTable();
          NpgsqlDataAdapter da = new NpgsqlDataAdapter(strSelect, this.conn);
          da.Fill(data);
        }
        catch (Exception ex)
        {
          SyncroC.Utility.Logger.LogError(this, "Select a SQL non riuscita per Errore : " + ex.Message);
          if (data != null)
            data.Clear();
          if (conn.State != ConnectionState.Closed)
          {
            SyncroC.Utility.Logger.LogError(this, "Stato Connessione " + conn.State.ToString() + " forzatura a chiusa");
            conn.Close();
          }
        }
      }

      return data;
    }

    public bool insertBytea(string strCommand, byte[] bytea)
    {
      bool res = false;

      if (this.connectToSQL())
      {
        try
        {
          NpgsqlCommand cmd = new NpgsqlCommand(strCommand, this.conn);
          NpgsqlParameter param = new NpgsqlParameter(":bytesData", NpgsqlTypes.NpgsqlDbType.Bytea);
          param.Value = bytea;
          cmd.Parameters.Add(param);
          int i = cmd.ExecuteNonQuery();

          res = true;
        }
        catch (Exception ex)
        {
          SyncroC.Utility.Logger.LogError(this, "insertBytea in SQL non riuscita per Errore : " + ex.Message);
          if (conn.State != ConnectionState.Closed)
          {
            SyncroC.Utility.Logger.LogError(this, "Stato Connessione " + conn.State.ToString() + " forzatura a chiusa");
            conn.Close();
          }
          res = false;
        }
      }
      else
        res = false;

      return res;
    }

    public bool doCommand(string strCommand, bool log)
    {
      bool res = false;

      if (log)
        SyncroC.Utility.Logger.LogError(this, "ExecuteCommand: " + strCommand);

      if (this.connectToSQL())
      {
        try
        {
          NpgsqlCommand cmd = new NpgsqlCommand(strCommand, this.conn);
          int i = cmd.ExecuteNonQuery();
          res = true;
        }
        catch (Exception ex)
        {
          SyncroC.Utility.Logger.LogError(this, "ExecuteCommand in SQL non riuscita per Errore : " + ex.Message);
          if (conn.State != ConnectionState.Closed)
          {
            SyncroC.Utility.Logger.LogError(this, "Stato Connessione " + conn.State.ToString() + " forzatura a chiusa");
            conn.Close();
          }
          res = false;
        }
      }
      else
        res = false;

      return res;
    }

    //C:\Programmi\PostgreSQL\8.4\bin\pg_dump.exe --host localhost --port 5432 --username postgres --format plain --data-only --column-inserts --verbose --file "C:\Documents and Settings\abello\Desktop\SQLDump.sql" --table "\"syncro_db_3601\".\"prc_std_gluegap_11\"" postgres
    public bool backupDataBase(string fileNameDestinazione, string tableName)
    {
      bool res = true;

      if (System.IO.File.Exists(fileNameDestinazione))
        System.IO.File.Delete(fileNameDestinazione);

      //fai il dump
      string line = "";

      if (tableName == "")
      {
        line = String.Format(" -i -h localhost -p 5432 -U postgres -F c -v -f \"{0}\" -n \"{1}\" postgres",
           fileNameDestinazione,
           SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE()
           );
      }
      else
      {
        line = String.Format(" -i -h localhost -p 5432 -U postgres --format plain --data-only --column-inserts --verbose --file \"{0}\" --table \"{1}\".\"{2}\" postgres",
           fileNameDestinazione,
           SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE(),
           tableName
           );
      }

      System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();

      proc.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
      proc.Arguments = line;
      proc.CreateNoWindow = true;

      RegistryKey r = Registry.LocalMachine.OpenSubKey("Software");

      if(r!=null)
        r = r.OpenSubKey("PostgreSQL");
      if(r!=null)
        r = r.OpenSubKey("Services");
      if (r != null)
        r = r.OpenSubKey("postgresql-8.4");

      if (r != null)
      {
        //ABELLO_FI20/08/2010_1
        //string resRegistryKey = r.GetValue("Data Directory").ToString();
        string resRegistryKey = r.GetValue("Data Directory").ToString().Replace('/', '\\');
        proc.FileName = resRegistryKey.Substring(0, resRegistryKey.LastIndexOf('\\')) + @"\bin\pg_dump.exe";

        if (System.IO.File.Exists(proc.FileName))
        {
          System.Diagnostics.Process p = System.Diagnostics.Process.Start(proc);
          p.WaitForExit();
          p.Close();

          if (System.IO.File.Exists(fileNameDestinazione))
          {
            res = true;
          }
          else
          {
            SyncroC.Utility.Logger.LogError(this, "Attenzione il Dump Del dataBase non è riuscito");
            res = false;
          }

          SyncroC.Utility.Logger.LogError(this, "Finito il Dump Del dataBase");
        }
        else
        {
          SyncroC.Utility.Logger.LogError(this, "Warning! The file " + proc.FileName + " has not been found!");
          res = false;
        }
      }
      else
      {
        SyncroC.Utility.Logger.LogError(this, "Warning! Registry item for Postgres not found in the registry!");
        res = false;
      }
      return res;
    }

    public void doInsert(string strInsert, bool log)
    {
      if (log)
        SyncroC.Utility.Logger.LogError(this, "ExecuteInsert: " + strInsert);

      if (this.connectToSQL())
      {
        try
        {
          NpgsqlCommand cmd = new NpgsqlCommand(strInsert, this.conn);
          int i = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          SyncroC.Utility.Logger.LogError(this, "Insert in SQL non riuscita per Errore : " + ex.Message);
          if (conn.State != ConnectionState.Closed)
          {
            SyncroC.Utility.Logger.LogError(this, "Stato Connessione " + conn.State.ToString() + " forzatura a chiusa");
            conn.Close();
          }
        }
      }
    }
  }
}
