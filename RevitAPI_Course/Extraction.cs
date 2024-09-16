using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_Course
{
    internal class Extraction
    {
        public static Element singleElementSelection(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;

            Selection sel = uiapp.ActiveUIDocument.Selection;
            Reference pickref = null;
            Transaction trans = new Transaction(doc);
            Element selected = null;
            Boolean flag = true;
            trans.Start("Selection");

            while (flag)
            {
                try
                {
                    pickref = sel.PickObject(ObjectType.Element, "Select");
                    selected = doc.GetElement(pickref);
                }
                catch
                {
                    flag = false;
                }
            }
            trans.Commit();
            return selected;
        }
        public static List<Element> multipleElementSelection(UIApplication uiapp)
        {
            List<Element> allSelection = new List<Element>();
            Document doc = uiapp.ActiveUIDocument.Document;
            Selection sel = uiapp.ActiveUIDocument.Selection;
            Reference pickref = null;
            Boolean flag = true;
            Transaction trans = new Transaction(doc);
            trans.Start("Selection");

            while (flag)
            {
                try
                {
                    pickref = sel.PickObject(ObjectType.Element, "Select");
                    Element selected = doc.GetElement(pickref);
                    allSelection.Add(selected);
                }
                catch
                {
                    flag = false;
                }
            }

            trans.Commit();
            return allSelection;
        }
        public static List<Element> multipleStructuralColumnElementSelection(UIApplication uiapp)
        {
            List<Element> allSelection = new List<Element>();
            Document doc = uiapp.ActiveUIDocument.Document;
            Selection sel = uiapp.ActiveUIDocument.Selection;
            ISelectionFilter filter = new StructuralColumnSelectionFilter();
            Reference pickref = null;
            Boolean flag = true;
            Transaction trans = new Transaction(doc);
            trans.Start("Selection");

            while (flag)
            {
                try
                {
                    pickref = sel.PickObject(ObjectType.Element, filter, "Select");
                    Element selected = doc.GetElement(pickref);
                    allSelection.Add(selected);
                }
                catch
                {
                    flag = false;
                }
            }

            trans.Commit();
            return allSelection;
        }
        public class StructuralColumnSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category.Name == "Structural Columns")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }

        public static List<FamilyInstance> getAllFamilyInstancesOfCategory(Document doc, BuiltInCategory category)
        {
            List<FamilyInstance> allFamilies = new List<FamilyInstance>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).WhereElementIsNotElementType();
            FilteredElementIterator famIT = collector.GetElementIterator();
            famIT.Reset();

            while (famIT.MoveNext())
            {
                Element efam = famIT.Current as Element;
                FamilyInstance famin = famIT.Current as FamilyInstance;
                allFamilies.Add(famin);
            }
            return allFamilies;
        }

        public static List<FamilySymbol> getAllFamilySymbolsOfCategory(Document doc, BuiltInCategory category)
        {
            List<FamilySymbol> allFamilySymbols = new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).OfClass(typeof(FamilySymbol));
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();

            while (famIT.MoveNext())
            {
                ElementId efam = famIT.Current;
                FamilySymbol famsymb = doc.GetElement(efam) as FamilySymbol;
                allFamilySymbols.Add(famsymb);
            }
            return allFamilySymbols;
        }

        public static List<FamilySymbol> getAllFamilySymbolsOfCategoryFamilyName(Document doc, BuiltInCategory category, string familyName)
        {
            List<FamilySymbol> allFamilySymbols = new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).OfClass(typeof(FamilySymbol));
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();

            while (famIT.MoveNext())
            {
                ElementId efam = famIT.Current;
                FamilySymbol famsymb = doc.GetElement(efam) as FamilySymbol;
                if(famsymb.FamilyName == familyName)
                {
                    allFamilySymbols.Add(famsymb);
                }
            }
            return allFamilySymbols;
        }

        public static List<ElementType> getAllElementTypesOfCategory(Document doc, BuiltInCategory category)
        {
            List<ElementType> allElementTypes = new List<ElementType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).WhereElementIsElementType();
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();

            while (famIT.MoveNext())
            {
                ElementId efam = famIT.Current;
                ElementType famsymb = doc.GetElement(efam) as ElementType;
                allElementTypes.Add(famsymb);
            }
            return allElementTypes;
        }

        public static List<Level> getAllLevelsFromModel(Document doc)
        {
            List<Level> allLevels = new List<Level>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType();
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();

            while (famIT.MoveNext())
            {
                ElementId efam = famIT.Current;
                Level famsymb = doc.GetElement(efam) as Level;
                allLevels.Add(famsymb);
            }
            return allLevels;
        }
    }
}
