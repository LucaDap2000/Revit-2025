using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

namespace RevitAPI_Course
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class _01_MessageTest : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string myString = "Sample";
            double myDouble = 3.45;
            int myInt = 4;
            string multipleTypes = "type1|type2|type3";
            List<string> myList = new List<string>();
            myList.Add(myString);
            myList.Add(multipleTypes);
            myList.Add(myDouble.ToString());
            myList.Add(myInt.ToString());

            MessageBox.Show(Analysis.showMessage(myList));

            foreach(string s in myList)
            {
                MessageBox.Show(s);
            }

            return Result.Succeeded;
        }
    }
}
