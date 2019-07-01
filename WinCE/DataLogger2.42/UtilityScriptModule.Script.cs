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
	using System.Reflection;
	using System.Data;
	using Neo.ApplicationFramework.Common.Constants;
	using Neo.ApplicationFramework.Interfaces.Tag;
	using Neo.ApplicationFramework.Common.Runtime;
	using Neo.ApplicationFramework.Tools.OpcClient;
	
    public partial class UtilityScriptModule
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="src"></param>
		/// <param name="fieldName"></param>
		/// <returns>Tag Object</returns>
		public static object GetGetFieldValue(object src, string fieldName)
		{
			return src.GetType().GetField(fieldName).GetValue(src);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="i"></param>
		/// <returns>Tag Value</returns>
		public string GetTagValue(int i, string fieldName)
		{
			string tagValue = "";
			IBasicTag tag = GetGetFieldValue(Globals.Tags, String.Format("{0}{1:00}",fieldName, i)) as GlobalDataItem;
			if(tag == null)
				tag = GetGetFieldValue(Globals.Tags, String.Format("{0}{1:00}",fieldName, i)) as LightweightTag;
			
			try
			{
				if (tag != null)
				{
					tagValue = tag.Value.ToString();
				}
				else
				{
					throw new Exception(String.Format("{0}{1:00} conversion is error!",fieldName, i));
				}
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
			
			return tagValue;
		}
		
		/// <summary>
		/// Export DataLogger CSV file
		/// </summary>
		/// <param name="oTable"></param>
		/// <param name="FilePath">Include files name</param>
		/// <returns></returns>
		public void CreateCSVFile(DataTable oTable, string FilePath)
		{
			string data = "";
			try{
				FileInfo fi = new FileInfo(FilePath);
				if (!fi.Directory.Exists)
				{
					System.IO.Directory.CreateDirectory(fi.DirectoryName);
				}
			
				StreamWriter wr = new StreamWriter(FilePath, false, System.Text.Encoding.Unicode);
				foreach (DataColumn column in oTable.Columns)
				{
					data += column.ColumnName + ",";
				}
				data += "\n";
				wr.Write(data);
				data = "";
 
				foreach (DataRow row in oTable.Rows)
				{
					foreach (DataColumn column in oTable.Columns)
					{
						data += row[column].ToString().Trim() + ",";
					}
					data += "\n";
					wr.Write(data);
					data = "";
				}
				data += "\n";
 
				wr.Dispose();
				wr.Close();
				
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
    }
}
