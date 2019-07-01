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
	using Neo.ApplicationFramework.Common.Constants;
    
	using System.IO;
	using System.Data;
	using System.Text;
	using System.Linq;
	using Neo.ApplicationFramework.Tools.DataLogger;
	
    public partial class Screen1
    {
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Screen1));
		static string dataLogger = "DataLogger1";
		static string dataLogger_history = "DataLogger2";
		
		void Screen1_Opened(System.Object sender, System.EventArgs e)
		{
			// Initial Start date ComboBox
			queryStartDate.Font = new Font("Impact", 9, System.Drawing.FontStyle.Regular);
			queryStartDate.CalendarFont = new Font("Impact", 12, System.Drawing.FontStyle.Regular);
			queryStartDate.Format = DateTimePickerFormat.Custom;
			queryStartDate.CustomFormat = "yyyy / MM / dd";
			queryStartDate.Value = DateTime.Today.AddDays(-1);
			// Initial End date ComboBox
			queryEndDate.Font = new Font("Impact", 9, System.Drawing.FontStyle.Regular);
			queryEndDate.CalendarFont = new Font("Impact", 12, System.Drawing.FontStyle.Regular);
			queryEndDate.Format = DateTimePickerFormat.Custom;
			queryEndDate.CustomFormat = "yyyy / MM / dd";
			queryEndDate.Value = DateTime.Today;
			// Initial Hour,Minute,Second ComboBox			
			for (int i = 0; i < 24; i++)
			{
				cbbStartHour.Items.Add(i.ToString("00"));
				cbbEndHour.Items.Add(i.ToString("00"));
			}
			for (int i = 0; i < 60; i++)
			{
				cbbStartMinute.Items.Add(i.ToString("00"));
				cbbEndMinute.Items.Add(i.ToString("00"));
				cbbStartSecond.Items.Add(i.ToString("00"));
				cbbEndSecond.Items.Add(i.ToString("00"));
			}
			SetTimeDropDownList();
			SetColumnNameDictionary();
			SetImportColumnNameDictionary();
		}
		
		/// <summary>
		/// Set Hour,Minute,Second ComboBox Initial Value
		/// </summary>
		/// <returns></returns>
		void SetTimeDropDownList()
		{
			cbbStartSecond.SelectedItem = DateTime.Now.Second.ToString("00");
			cbbEndSecond.SelectedItem = DateTime.Now.Second.ToString("00");
			cbbStartMinute.SelectedItem = DateTime.Now.Minute.ToString("00");
			cbbEndMinute.SelectedItem = DateTime.Now.Minute.ToString("00");
			cbbStartHour.SelectedItem = DateTime.Now.Hour.ToString("00");
			cbbEndHour.SelectedItem = DateTime.Now.Hour.ToString("00");
		}
		
		/// <summary>
		/// Init Column Name for Export Dictionary
		/// </summary>
		/// <returns></returns>
		void SetColumnNameDictionary()
		{
			DataTable rs = new DataTable();
			rs = Globals.DataBaseScriptModule.QueryColumnName(dataLogger);
			int columnFlag = 1;
			for(int i=2; i<rs.Rows.Count; i++)
			{
				string keyValue = Globals.UtilityScriptModule.GetTagValue(columnFlag, "ColumnName");
				if (!string.IsNullOrEmpty(keyValue))
					Globals.DataBaseScriptModule.Dictionary.Add(rs.Rows[i]["name"].ToString(), keyValue);
				else
					Globals.DataBaseScriptModule.Dictionary.Add(rs.Rows[i]["name"].ToString(), rs.Rows[i]["name"].ToString());
				columnFlag++;
			}
		}
		
		/// <summary>
		/// Init Column Name for Import Dictionary
		/// </summary>
		/// <returns></returns>
		void SetImportColumnNameDictionary()
		{
			DataTable rs = new DataTable();
			rs = Globals.DataBaseScriptModule.QueryColumnName(dataLogger_history);
			int columnFlag = 1;
			for(int i=2; i<rs.Rows.Count; i++)
			{
				string keyValue = Globals.UtilityScriptModule.GetTagValue(columnFlag, "ImportColumnName");
				if (!string.IsNullOrEmpty(keyValue))
					Globals.DataBaseScriptModule.ImportDictionary.Add(rs.Rows[i]["name"].ToString(), keyValue);
				else
					Globals.DataBaseScriptModule.ImportDictionary.Add(rs.Rows[i]["name"].ToString(), rs.Rows[i]["name"].ToString());
				columnFlag++;
			}
		}
		
		/* DataGrid Can Change Header Width
		void SetDataTableColumnSize()
		{
			try
			{
				DataGridColumn idColumn = DataGrid.Columns[0];
				DataGridColumn timeColumn = DataGrid.Columns[1];
				DataGridColumn firstDataColumn = DataGrid.Columns[2];
				DataGridColumn secondDataColumn = DataGrid.Columns[3];
				idColumn.Width = Globals.Tags.IdColumnWidth.Value.Int;
				timeColumn.Width = Globals.Tags.TimeColumnWidth.Value.Int;
				firstDataColumn.Width = Globals.Tags.ColumnWidth01.Value.Int;
				secondDataColumn.Width = Globals.Tags.ColumnWidth02.Value.Int;
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
		}
		*/
		
		/// <summary>
		/// Select All Data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		void btnQuery_Click(System.Object sender, System.EventArgs e)
		{
			DataTable rs = new DataTable();
			try{
				rs = Globals.DataBaseScriptModule.QueryAllData(dataLogger);
				DataGrid.DataSource = rs;
				DataGrid.DataBindings.ToString();
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
		}
		
		/// <summary>
		/// Select Data In Condiction
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		void btnQueryWithTime_Click(System.Object sender, System.EventArgs e)
		{
			string endTime = "";
			string startTime = "";
			
			endTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
				queryEndDate.Value.Year.ToString(),
				queryEndDate.Value.Month.ToString("00"),
				queryEndDate.Value.Day.ToString("00"),
				cbbEndHour.SelectedItem.ToString(),
				cbbEndMinute.SelectedItem.ToString(),
				cbbEndSecond.SelectedItem.ToString());
				
			startTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
				queryStartDate.Value.Year.ToString(),
				queryStartDate.Value.Month.ToString("00"),
				queryStartDate.Value.Day.ToString("00"),
				cbbStartHour.SelectedItem.ToString(),
				cbbStartMinute.SelectedItem.ToString(),
				cbbStartSecond.SelectedItem.ToString());
			
			DataTable rs = new DataTable();
			StringBuilder sb = new StringBuilder();
			
			try{
				rs = Globals.DataBaseScriptModule.QueryDataByTime(dataLogger, startTime, endTime);
				DataGrid.DataSource = rs;
				DataGrid.DataBindings.ToString();
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
		}
		
		void btnHistory_Click(System.Object sender, System.EventArgs e)
		{
			DataTable rs = new DataTable();
			try{
				rs = Globals.DataBaseScriptModule.QueryImportData(dataLogger_history);
				DataGrid.DataSource = rs;
				DataGrid.DataBindings.ToString();
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
		
		void btnStartlog_Click(System.Object sender, System.EventArgs e)
		{
			Globals.DataBaseScriptModule.StartDataLogger(Globals.DataLogger1);
		}
		
		void btnStopLog_Click(System.Object sender, System.EventArgs e)
		{
			Globals.DataBaseScriptModule.StopDataLogger(Globals.DataLogger1);
		}
		
		void btnLogOnce_Click(System.Object sender, System.EventArgs e)
		{
			Globals.DataBaseScriptModule.Log(Globals.DataLogger1);
		}
		
		void btnExport_Click(System.Object sender, System.EventArgs e)
		{
			string exportDefaultName = Globals.Tags.ExportFileDefaultName.Value.ToString();
			string exportFileName = Globals.Tags.ExportFileName.Value.ToString();
			string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
			string filepath = "";
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			DataTable rs = new DataTable();
			StringBuilder sb = new StringBuilder();
			try{
				rs = Globals.DataBaseScriptModule.QueryAllData(dataLogger);
				
				// *** Different according to Project Target
				switch(Globals.Tags.ExportOption.Value.Short)
				{
					case 1:
						filepath = Neo.ApplicationFramework.Interfaces.FileDirectory.ProjectFiles + "\\" + ApplicationConstantsCF.DatabaseExportFolder + @"\Data Loggers\";
						if (string.IsNullOrEmpty(exportFileName))
							Globals.UtilityScriptModule.CreateCSVFile(rs, filepath + exportDefaultName + " " + timeStamp + ".csv");
						else
							Globals.UtilityScriptModule.CreateCSVFile(rs, filepath + exportFileName + " " + timeStamp + ".csv");
						break;
					case 2:
						filepath = ApplicationConstantsCF.HardDiskPath + @"\DatabaseExport\Data Loggers\";
						if(string.IsNullOrEmpty(exportFileName))
							Globals.UtilityScriptModule.CreateCSVFile(rs, filepath + exportDefaultName + " " + timeStamp + ".csv");
						else
							Globals.UtilityScriptModule.CreateCSVFile(rs, filepath + exportFileName + " " + timeStamp + ".csv");
						break;
					case 3:
						filepath = ApplicationConstantsCF.StorageCardPath + @"\DatabaseExport\Data Loggers\";
						if(string.IsNullOrEmpty(exportFileName))
							Globals.UtilityScriptModule.CreateCSVFile(rs, filepath + exportDefaultName + " " + timeStamp + ".csv");
						else
							Globals.UtilityScriptModule.CreateCSVFile(rs, filepath + exportFileName + " " + timeStamp + ".csv");
						break;
				}
				// ***
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
    }
}
