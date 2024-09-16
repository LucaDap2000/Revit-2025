using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitAPI_Course
{
    internal class Analysis
    {
        public static string showMessage(List<string> allStrings)
        {
            foreach (string s in allStrings)
            {
                MessageBox.Show(s);
            }
            return null;
        }

        public static void showFamilyInstanceData(List<FamilyInstance> allElements)
        {
            foreach (FamilyInstance s in allElements)
            {
                MessageBox.Show(s.Category.Name + " | " + s.Id.ToString());
            }
        }

        public static void showFamilySymbolsData(List<FamilySymbol> allElements)
        {
            foreach (FamilySymbol s in allElements)
            {
                MessageBox.Show(s.FamilyName + " | " + s.Name);
            }
        }

        public static void showElementTypesData(List<ElementType> allElements)
        {
            foreach (ElementType s in allElements)
            {
                MessageBox.Show(s.FamilyName + " | " + s.Name);
            }
        }
    }
}
