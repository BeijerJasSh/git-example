using System.Collections;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using Core.Api.Application;
using Core.Api.Service;
using Core.Api.Tools;
using Core.Api.Utilities;
using Core.Utilities.Utilities;
using Core.Engine.Application;
using Neo.ApplicationFramework.Attributes;
using Neo.ApplicationFramework.Common;
using Neo.ApplicationFramework.Common.Runtime;
using Neo.ApplicationFramework.Common.TypeConverters;
using Neo.ApplicationFramework.Common.Utilities;
using Neo.ApplicationFramework.Interfaces;
using Neo.ApplicationFramework.Measurement;
using Neo.ApplicationFramework.Storage.Settings;
using Neo.ApplicationFramework.Tools.Runtime;
using Neo.ApplicationFramework.Tools.Storage;

[assembly: AssemblyVersion("2.41.177.0")]
[assembly: NeoDesignerVersion("2.41.177.0")]

namespace Neo.ApplicationFramework.Generated
{
    public class Globals : GlobalsBase
    {
        private static readonly log4net.ILog m_Log = log4net.LogManager.GetLogger(typeof(Globals));

        static Globals()
        {
            TypeDescriptor.AddProvider(new WPFToCFTypeDescriptionProvider(typeof(object)), typeof(object));
        }

        public Globals() 
            : base(CreateToolManager())
        {
            m_ProjectSettings.MainScreenTitle = "iXSample_DataLogger";
            m_ProjectSettings.Topmost = false;
            m_ProjectSettings.StartupLocation = new Point(0, 0);
            m_ProjectSettings.MaximizeOnStartup = false;
            m_ProjectSettings.UseWideScrollbars = false;
            m_ProjectSettings.MainScreenSize = new Size(1280,960);
            m_ProjectSettings.BorderStyle = ScreenBorderStyle.ThreeDBorder;
            m_ProjectSettings.InputDelay = 2000;
            m_ProjectSettings.IsOnScreenKeyboardEnabled = true;
            m_ProjectSettings.KeyboardLayoutName = "Traditional Chinese";
            m_ProjectSettings.TerminalGroup = TerminalGroup.Default;
            List<IStorageProviderSetting> storageProviderSettingsList = new List<IStorageProviderSetting>() {
                new ProjectStorageProviderSetting("BackupAtStartup", false), new ProjectStorageProviderSetting("MaxSize", 0), 
            };
            m_ProjectSettings.StorageProviderSettings = new LocallyHostedProjectStorageProviderSettings("SQLite Database", storageProviderSettingsList, "");
            m_SystemSettings.AutomaticallyTurnOfBacklight = false;
            m_SystemSettings.BacklightTimeout = 900;    
            m_SystemSettings.KeepBacklightOnIfNotifierWindowIsVisible = false;
            Dictionary<ComPort, PortMode> comPortModes = new Dictionary<ComPort, PortMode>();
                                       
            m_SystemSettings.ComPortModes = comPortModes;                            
            m_SystemSettings.KeyBeep = true;
            m_SystemSettings.TimeZoneDisplayName = "";
            m_SystemSettings.TimeZoneId = -1;
            m_SystemSettings.RegionLCID = 0;
            m_SystemSettings.AdjustForDaylightSaving = true;
            m_SystemSettings.FtpServerEnabled = false;
            m_SystemSettings.FtpServerFriendlyNamesEnabled = false;
            m_SystemSettings.FtpServerAllowAnonymous = false;
            m_SystemSettings.FtpServerSdCardAccess = false;
            m_SystemSettings.FtpServerUsbAccess = false;
            m_SystemSettings.FtpServerDefaultDir = @"";
            m_SystemSettings.NTLMUser = @"";
            m_SystemSettings.NTLMPassword = @"";
            m_SystemSettings.VncServerEnabled = false;
            m_SystemSettings.VncTcpPort = 5900;
            m_SystemSettings.VncHttpTcpPort = 5800;
            m_SystemSettings.VncViewOnlyPassword = @"";
            m_SystemSettings.VncFullAccessPassword = @"";
            m_SystemSettings.VncActiveConnectionNotification = false;
            m_SystemSettings.IsKeyPanel = false;
            m_SystemSettings.IsHeadless = false;
            m_SystemSettings.AlarmButtonShowScreenTarget = @"";
            m_SystemSettings.ScreenRotationAngle = 0;
            m_SystemSettings.BeShellMenuPassword = @"";
            m_SystemSettings.ProjectGuid = new Guid("d4d34750-6322-46b5-b3c1-a6ceae50b4f9");
        }
        
        
        private static IToolManager CreateToolManager()
        {
            string executablePath = typeof(Globals).Module.FullyQualifiedName;
            var coreApplication = new CoreApplication(false, executablePath);
            IToolManager toolManager = new ApplicationEngineCe().CreateToolManager(true, coreApplication, Path.Combine(coreApplication.StartupPath, "Modules.cfgtool"));
            return toolManager;
        }
                

        
        private static void SetWorkingFolder()
        {
            //Neo is dependent on the workingfolder being the folder where the application resides.
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
                
       

        /// <summary>
        /// Neo generated code - do not modify
        /// the contents of this method(s) with the code editor.
        /// </summary>        
                    public static IPrinterDevice Printer1
                    {
                        get
                        {
                            IDeviceManagerServiceCF deviceManagerService = ServiceContainerCF.GetService<IDeviceManagerServiceCF>();
                            return deviceManagerService.GetOutputDevice<IPrinterDevice>();
                        }
                    }           
                    public static DataLogger2 DataLogger2
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<DataLogger2>("DataLogger2");
                        }
                    }           
                    public static ScriptModule2 ScriptModule2
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<ScriptModule2>("ScriptModule2");
                        }
                    }           
                    public static DataLogger1 DataLogger1
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<DataLogger1>("DataLogger1");
                        }
                    }           
                    public static ScriptModule1 ScriptModule1
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<ScriptModule1>("ScriptModule1");
                        }
                    }           
                    public static AlarmServer AlarmServer
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<AlarmServer>("AlarmServer");
                        }
                    }           
                    public static Tags Tags
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Tags>("Tags");
                        }
                    }           
                    public static Expressions Expressions
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Expressions>("Expressions");
                        }
                    }           
                    public static Security Security
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Security>("Security");
                        }
                    }           
                    public static IScreenAdapter Screen6
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Screen6>("Screens.Screen6.Default").Adapter;
                        }
                    }           
                    public static IScreenAdapter Screen5
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Screen5>("Screens.Screen5.Default").Adapter;
                        }
                    }           
                    public static IScreenAdapter Screen4
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Screen4>("Screens.Screen4.Default").Adapter;
                        }
                    }           
                    public static IScreenAdapter Screen3
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Screen3>("Screens.Screen3.Default").Adapter;
                        }
                    }           
                    public static IScreenAdapter Screen2
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Screen2>("Screens.Screen2.Default").Adapter;
                        }
                    }           
                    public static IScreenAdapter Screen1
                    {
                        get
                        {
                            return GlobalReferenceService.Value.GetObject<Screen1>("Screens.Screen1.Default").Adapter;
                        }
                    }   
        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "NeoMainThread";
            
            InitializeBeHwApiLog();
            
            SetWorkingFolder();
            
            UserStartupMessage.SendUserStartupMessageToBeijerShell("Loading Files");
            if (!StopWatchCF.Silent)
                StopWatchCF.Send("Starting Project");
            StopWatchCF.SetTimestamp("***** Project Startup Time *****");
            while (ProcessExplorer.WaitForAttachDebugger)
            {
                Thread.Sleep(1000);
            }
            IsCompiledForCE = false;
            IsCompiledForDesktopWindowsPanel = false;

            Instance = new Globals();
        	if (!Instance.CheckIfApplicationCanRun())
				return;

            string executingAssemblyName = Assembly.GetExecutingAssembly().FullName;
            string executablePath = typeof(Globals).Module.FullyQualifiedName;

            Instance.Go(executingAssemblyName, executablePath, args, new string[]{"Security","Expressions","Tags","AlarmServer","ScriptModule1","DataLogger1","ScriptModule2","DataLogger2"}, new string[]{}, () => Screen1);
        }

    }
}