//--------------------------------------------------------------
// Press F1 to get help about using script.
// To access an object that is not located in the current class, start the call with Globals.
// When using events and timers be cautious not to generate memoryleaks,
// please see the help for more information.
//---------------------------------------------------------------

namespace Neo.ApplicationFramework.Generated
{
    using System.Windows.Forms;
    using System;
    using System.Drawing;
    using Neo.ApplicationFramework.Tools;
    using Neo.ApplicationFramework.Common.Graphics.Logic;
    using Neo.ApplicationFramework.Controls;
    using Neo.ApplicationFramework.Interfaces;
    
	//New using
	using System.Data;
	using System.Data.SQLite;
	using System.IO;
	using System.Reflection;
	using System.Data.Common;
	using System.Collections.Generic;
	using System.Text;
	using System.Linq;
	using Neo.ApplicationFramework.Common.Constants;
	using Neo.ApplicationFramework.Interfaces.Tag;
	using Neo.ApplicationFramework.Common.Runtime;
	using Neo.ApplicationFramework.Tools.OpcClient;
	using Neo.ApplicationFramework.Tools.DataLogger;
	
    public partial class DataBaseScriptModule    {

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataBaseScriptModule));
		public Dictionary<string, string> Dictionary = new Dictionary<string, string>();
		public Dictionary<string, string> ImportDictionary = new Dictionary<string, string>();
		
		/// <summary>
		/// SQLite Connection
		/// </summary>
		/// <returns></returns>
		public static SQLiteConnection OpenConn()
		{
			string IX_DB_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName), "Database.db");
			string IX_CONNECTION_STRING = string.Format("data source={0}", IX_DB_PATH);
			SQLiteConnection cn = new SQLiteConnection(IX_CONNECTION_STRING);
			if (cn.State == ConnectionState.Open) cn.Close();
			cn.Open();
			return cn;
		}
		
		/// <summary>
		/// SQLite Query Method
		/// </summary>
		/// <param name="SqlString"></param>
		/// <returns>Result DataTable</returns>
		public DataTable GetDataTable(string SqlString)
		{
			DataTable dt = new DataTable();
			SQLiteConnection cn = OpenConn();
			SQLiteCommand cmd = new SQLiteCommand();
			SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
			cmd.Connection = cn;
			cmd.CommandText = SqlString;
			DataSet ds = new DataSet();
			ds.Clear();
			da.Fill(ds);
			dt = ds.Tables[0];
			if (cn.State == ConnectionState.Open) cn.Close();
			return dt;
		}
		
		/// <summary>
		/// SQLite Execute NonQuery Command
		/// </summary>
		/// <param name="SqlString"></param>
		/// <returns></returns>
		public void SQLiteInsertUpdateDelete(string SqlString)
		{
			SQLiteConnection cn = OpenConn();
			SQLiteCommand cmd = new SQLiteCommand(SqlString, cn);
			SQLiteTransaction sqliteTransaction = cn.BeginTransaction();
			try
			{
				cmd.ExecuteNonQuery();
				sqliteTransaction.Commit();
			}
			catch (Exception ex)
			{
				sqliteTransaction.Rollback();
				throw (ex);
			}
			if (cn.State == ConnectionState.Open) cn.Close();
		}
		
		/// <summary>
		/// Enables the Logger
		/// </summary>
		/// <returns></returns>
		public void StartDataLogger(DataLogger datalogger)
		{
			datalogger.Start();
		}
		
		/// <summary>
		/// Disables the Logger
		/// </summary>
		/// <returns></returns>
		public void StopDataLogger(DataLogger datalogger)
		{
			datalogger.Stop();
		}

		/// <summary>
		/// Log Once
		/// </summary>
		/// <returns></returns>
		public void Log(DataLogger datalogger)
		{
			datalogger.Log();
		}
		
		public DataTable QueryColumnName(string tableName)
		{
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();			
			try{
				string sql = String.Format("PRAGMA table_info({0})", tableName);
				dt = GetDataTable(sql);
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
			return dt;
		}

		public DataTable QueryAllData(string tableName)
		{
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();			
			try{
				sb.Append("Select id, time ");
				for(int i=0; i < Dictionary.Count; i++)
				{
					if(string.IsNullOrEmpty(Dictionary.ElementAt(i).Value))
						sb.Append(", " + Dictionary.ElementAt(i).Key);
					else
						sb.Append(", " + Dictionary.ElementAt(i).Key + " as "
							+ Dictionary.ElementAt(i).Value);
				}
				sb.Append(" From " + tableName);
				dt = GetDataTable(sb.ToString());
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
			return dt;
		}

		public DataTable QueryDataByTime(string tableName, string startTime, string endTime)
		{
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();			
			try{
				sb.Append("Select id, time ");
				for(int i=0; i < Dictionary.Count; i++)
				{
					if(string.IsNullOrEmpty(Dictionary.ElementAt(i).Value))
						sb.Append(", " + Dictionary.ElementAt(i).Key);
					else
						sb.Append(", " + Dictionary.ElementAt(i).Key + " as "
							+ Dictionary.ElementAt(i).Value);
				}
				sb.Append(" From "+ tableName + " Where time > '" + startTime + "' AND time < '" + endTime + "'" );
				dt = GetDataTable(sb.ToString());
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
			return dt;
		}
		
		public DataTable QueryImportData(string tableName)
		{
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();			
			try{
				sb.Append("Select id, time ");
				for(int i=0; i < ImportDictionary.Count; i++)
				{
					if(string.IsNullOrEmpty(ImportDictionary.ElementAt(i).Value))
						sb.Append(", " + ImportDictionary.ElementAt(i).Key);
					else
						sb.Append(", " + ImportDictionary.ElementAt(i).Key + " as "
							+ ImportDictionary.ElementAt(i).Value);
				}
				sb.Append(" From " + tableName);
				dt = GetDataTable(sb.ToString());
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
			return dt;
		}
		
		public void DeleteDataLogger2()
		{
			Neo.ApplicationFramework.Generated.Globals.DataLogger2.Clear(false);
			//Core.Api.Service.ServiceContainerCF.GetService<Neo.ApplicationFramework.Interfaces.Storage.IDatabaseCleanupService>().CleanupDatabase();
		}
    }
}
