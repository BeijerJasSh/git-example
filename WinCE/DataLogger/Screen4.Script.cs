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
    
    using System.Linq;
	
    public partial class Screen4
    {
		
		void Screen4_Opened(System.Object sender, System.EventArgs e)
		{
			AnalogNumericColumn1.Value = Globals.Tags.ColumnName01.Value;
			AnalogNumericColumn2.Value = Globals.Tags.ColumnName02.Value;
		}
		
		void AnalogNumericColumn1_InputValueChanged(System.Object sender, Core.Api.DataSource.ValueChangedEventArgs e)
		{
			string keyName = Globals.ScriptModule1._dictionary.ElementAt(0).Key;
			Globals.ScriptModule1._dictionary[keyName] = e.Value.ToString();
			Globals.Tags.ColumnName01.Value = e.Value.ToString();
		}
		
		void AnalogNumericColumn2_InputValueChanged(System.Object sender, Core.Api.DataSource.ValueChangedEventArgs e)
		{
			string keyName = Globals.ScriptModule1._dictionary.ElementAt(1).Key;
			Globals.ScriptModule1._dictionary[keyName] = e.Value.ToString();
			Globals.Tags.ColumnName02.Value = e.Value.ToString();
		}
		
		
    }
}
