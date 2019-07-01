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

    public partial class Tags
    {
		
		void ColumnName01_ValueChange(System.Object sender, Core.Api.DataSource.ValueChangedEventArgs e)
		{
			if(Globals.DataBaseScriptModule.Dictionary == null)
				return;
			if(Globals.DataBaseScriptModule.Dictionary.Count == 0)
				return;
			string keyName = Globals.DataBaseScriptModule.Dictionary.ElementAt(0).Key;
			Globals.DataBaseScriptModule.Dictionary[keyName] = e.Value.ToString();
		}
		
		void ColumnName02_ValueChange(System.Object sender, Core.Api.DataSource.ValueChangedEventArgs e)
		{
			if(Globals.DataBaseScriptModule.Dictionary == null)
				return;
			if(Globals.DataBaseScriptModule.Dictionary.Count == 0)
				return;
			string keyName = Globals.DataBaseScriptModule.Dictionary.ElementAt(1).Key;
			Globals.DataBaseScriptModule.Dictionary[keyName] = e.Value.ToString();
		}
    }
}
