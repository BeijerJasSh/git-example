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
    
    
    public partial class Screen3
    {
		void Screen3_Opened(System.Object sender, System.EventArgs e)
		{
			switch(Globals.Tags.ExportOption.Value.Short)
			{
				case 1:
					porpertySetting(true, false, false, false, 1);
					break;
				case 2:
					porpertySetting(false, true, false, false, 2);
					break;
				case 3:
					porpertySetting(false, false, true, true, 3);
					break;
			}
		}
		
		void rbtExportOption1_Click(System.Object sender, System.EventArgs e)
		{
			porpertySetting(true, false, false, false, 1);
		}
		
		void rbtExportOption2_Click(System.Object sender, System.EventArgs e)
		{
			porpertySetting(false, true, false, false, 2);
		}
		
		void rbtExportOption3_Click(System.Object sender, System.EventArgs e)
		{
			porpertySetting(false, false, true, true, 3);
		}
		
		void porpertySetting(bool rb1, bool rb2, bool rb3, bool pathtxtBoxEnable, int option)
		{
			rbtExportOption1.Checked = rb1;
			rbtExportOption2.Checked = rb2;
			rbtExportOption3.Checked = rb3;
			Globals.Tags.ExportOption.Value = option;
			
			cbExportFilesName.Checked = Globals.Tags.IsRenameFileName.Value.Bool;
			if(cbExportFilesName.Checked)
				AnalogNumericFileName.IsEnabled = true;
			else
				AnalogNumericFileName.IsEnabled = false;
		}
				
		void cbExportFilesName_Click(System.Object sender, System.EventArgs e)
		{
            cbExportFilesName.Width = 100;
            cbExportFilesName.Height = 100;


			if (cbExportFilesName.Checked)
			{
				AnalogNumericFileName.IsEnabled = true;
				Globals.Tags.IsRenameFileName.Value = true;
			}
			else
			{
				AnalogNumericFileName.IsEnabled = false;
				Globals.Tags.ExportFileName.Value = "";
				Globals.Tags.IsRenameFileName.Value = false;
			}
		}
    }
}
