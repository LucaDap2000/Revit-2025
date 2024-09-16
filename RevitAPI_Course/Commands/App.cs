using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitAPI_Course.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class App : IExternalApplication
    {
        void AddRibbonPanel(UIControlledApplication application)
        {
            String tabName = "TestTools";
            application.CreateRibbonTab(tabName);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Tools");
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdMessageText", "Show a" + "\r\n" + "Message", "_01_MessageTest", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdSelection", "Show all" + "\r\n" + "Selections", "_02_SelectionofObjects", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdInstances", "Show all" + "\r\n" + "Instances", "_03_InstanceExtraction", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdSymbols", "Show all" + "\r\n" + "Symbols", "_04_SymbolExtraction", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdTypes", "Show all" + "\r\n" + "Types", "_05_ElementTypesExtraction", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdInstanceCreation", "Create an" + "\r\n" + "Instance", "_06_FamilyInstanceCreation", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdFamilyData", "Create an" + "\r\n" + "Instance With Data", "_07_FamilyWithData", "config.png");
        }

        public void createPushButton(string AssemblyPath, RibbonPanel ribbonPanel, string cmd, string text, string assemblyt, string filename)
        {
            PushButtonData A1 = new PushButtonData(cmd, text, AssemblyPath, "RevitAPI_Course." + assemblyt);
            PushButton pb1 = ribbonPanel.AddItem(A1) as PushButton;
            pb1.ToolTip = text;
            pb1.LargeImage = PngImageSource("RevitAPI_Course.resources." + filename);
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public System.Windows.Media.ImageSource PngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);
            return Result.Succeeded;
        }
    }
}
