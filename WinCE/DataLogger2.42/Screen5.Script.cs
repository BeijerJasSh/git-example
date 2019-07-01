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
    
    using System.IO;
	using System.Data;
	using System.Text;
	using System.Linq;
	using System.Collections.Generic;
	using Neo.ApplicationFramework.Common.Constants;
	
    public partial class Screen5
    {
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Screen5));
		static string dataLogger_history = "DataLogger2";
		
		string folderpath="";
		DataTable dt=new DataTable();
		
		void Screen5_Opened(System.Object sender, System.EventArgs e)
		{
			ListViewSet();
			
			// ***Different according to Project Target
			switch(Globals.Tags.ExportOption.Value.Short)
			{
				case 1:
					folderpath = Neo.ApplicationFramework.Interfaces.FileDirectory.ProjectFiles + "\\" + ApplicationConstantsCF.DatabaseExportFolder + @"\Data Loggers\";
					break;
				case 2:
					folderpath = ApplicationConstantsCF.HardDiskPath + @"\DatabaseExport\Data Loggers\";
					break;
				case 3:
					folderpath = ApplicationConstantsCF.StorageCardPath + @"\DatabaseExport\Data Loggers\";
					break;
			}
			// ***
			try
			{
				DirectoryInfo d = new DirectoryInfo(folderpath);
				FileInfo[] files = d.GetFiles("*.csv");
				foreach (FileInfo fi in files)
				{
					ListViewItem lviItem = new ListViewItem();
					lviItem.Text = fi.Name;

					ListViewItem.ListViewSubItem lviSubItem = new ListViewItem.ListViewSubItem();
					lviSubItem.Text = fi.Length.ToString();
					lviItem.SubItems.Add(lviSubItem);

					ListView.Items.Add(lviItem);
				}
				ListView.EndUpdate();
			}
			catch (DirectoryNotFoundException dirEx)
			{
				// the directory did not exist.
				MessageBox.Show("Directory not found: " + dirEx.Message);
				_log.Warn(dirEx.ToString());
			}
		}
		
		private void ListViewSet()
		{
			ListView.View = View.Details;
			ColumnHeader header1, header2;
			header1 = new ColumnHeader();
			header2 = new ColumnHeader();
			
			// Set the text, alignment and width for each column header.
			header1.Text = "File name";
			header1.Width = 300;
			header2.Text = "File size";
			header2.Width = -2;
			
			// Add the headers to the ListView control.
			ListView.Columns.Add(header1);
			ListView.Columns.Add(header2);
		}
		
		void ListView_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{			
			try{
				if (ListView.SelectedIndices.Count > 0){
					string filename = ListView.Items[ListView.SelectedIndices[0]].Text;
					string filepath = Path.Combine(folderpath, filename);
					LoadCsvFile(filepath);
					SetImportDictionary();
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
		}
		
		/// <summary>
		/// Setting Column Name Of Import CSV File (Depend on a number of Your DataLogger LogItem)
		/// </summary>
		/// <returns></returns>
		void SetImportDictionary(){
			
			try{
				string keyName1 = Globals.DataBaseScriptModule.ImportDictionary.ElementAt(0).Key;
				string keyName2 = Globals.DataBaseScriptModule.ImportDictionary.ElementAt(1).Key;
				Globals.DataBaseScriptModule.ImportDictionary[keyName1] = dt.Columns[2].ColumnName.ToString();
				Globals.Tags.ImportColumnName01.Value = dt.Columns[2].ColumnName.ToString();
				Globals.DataBaseScriptModule.ImportDictionary[keyName2] = dt.Columns[3].ColumnName.ToString();
				Globals.Tags.ImportColumnName02.Value = dt.Columns[3].ColumnName.ToString();
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
		}
		
		void LoadCsvFile(string filePath)
		{
			dt = new DataTable();
			bool hasTitle = true; 
			using (StreamReader sr = new StreamReader(File.OpenRead(filePath), Encoding.Unicode))
			{
				bool bFirst = true;                                                        
				string line="";
				while ((line = sr.ReadLine()) != null)
				{
					string[] elements = line.TrimEnd(line[line.Length - 1]).Trim('"').Split(',');
						
					if (bFirst)
					{
						for (int i = 0; i < elements.Length; i++)
						{
							dt.Columns.Add();
						}
						bFirst = false;
					}
					if (hasTitle)
					{
						for (int i = 0; i < dt.Columns.Count && i < elements.Length; i++)
						{
							dt.Columns[i].ColumnName = elements[i];						
							dt.Columns[1].DataType = System.Type.GetType("System.DateTime");
						}
						hasTitle = false;
					}
					else if (elements.Length == dt.Columns.Count)
					{
						dt.Rows.Add(elements);							
					}
				}
				sr.Close();
			}
			foreach(DataRow dr in dt.Rows)
			{
				for(int count=0;count<dr.ItemArray.Length;count++)
				{
					if(dr[count].ToString().Equals("") || dr[count].ToString().Equals(null))
						dr[count]="0";
				}
			
			}
			dt.DefaultView.Sort="Time";
			DataGrid.DataSource = dt;
			DataGrid.DataBindings.ToString();
		}
		
		void btnImport_Click(System.Object sender, System.EventArgs e)
		{
			if (ListView.SelectedIndices.Count == 0){
				MessageBox.Show("No files selected");
				return;
			}
			Globals.DataBaseScriptModule.DeleteDataLogger2();			
			try{
				
				// *** Depend on a number of Your DataLogger LogItem ***
				var importKey = Globals.DataBaseScriptModule.ImportDictionary.FirstOrDefault(x => x.Value == dt.Columns[2].ColumnName).Key;
				var importKey1 = Globals.DataBaseScriptModule.ImportDictionary.FirstOrDefault(x => x.Value == dt.Columns[3].ColumnName).Key;
				// ***
				
				for (int j = 0; j <dt.Rows.Count; j++){
					DateTime time= DateTime.Parse(dt.Rows[j].ItemArray[1].ToString());
					string DateStr=time.ToString("yyyy-MM-dd HH:mm:ss");
					StringBuilder sb = new StringBuilder();
					sb.AppendLine("Insert into " + dataLogger_history + "(");
					sb.AppendLine(string.Format("{0},{1},{2}","Time",importKey,importKey1));
					sb.AppendLine(")");
					sb.AppendLine("VALUES(");
					sb.AppendLine(string.Format("'{0}',{1},{2}",DateStr,dt.Rows[j].ItemArray[2].ToString(),dt.Rows[j].ItemArray[3].ToString()));
					sb.AppendLine(");");
					Globals.DataBaseScriptModule.SQLiteInsertUpdateDelete(sb.ToString());
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
				_log.Warn(ex.ToString());
			}
		}
		
    }
}
