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
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;

namespace RevitAPI_Course
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class _07_FamilyWithData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Selection or Extraction
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            Element selected = Extraction.singleElementSelection(uiapp);
            ElementId familyTypeId = selected.GetTypeId();
            FamilySymbol FamS = doc.GetElement(familyTypeId) as FamilySymbol;
            Level lvl = doc.GetElement(selected.LevelId) as Level;

            Parameter PAR = selected.get_Parameter(BuiltInParameter.INSTANCE_LENGTH_PARAM);
            double length = PAR.AsDouble();
            string val = selected.LookupParameter("Reference").AsString();

            Location location = selected.Location;
            LocationPoint LP = location as LocationPoint;
            XYZ centerPoint = LP.Point;

            List<XYZ> arrayofPoints = new List<XYZ>();
            for (int i = 1; i < 3; i++)
            {
                XYZ P = centerPoint.Add(new XYZ(i * length, 0, 0));
                arrayofPoints.Add(P);
            }

            // Creation
            Transaction trans = new Transaction(doc);
            trans.Start("StartingProcess");

            // Creation Process
            foreach (XYZ point in arrayofPoints)
            {
                FamilyInstance fam = doc.Create.NewFamilyInstance(point, FamS, lvl, StructuralType.Column);
                fam.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set("Checked Value");
                fam.LookupParameter("Reference").Set(val);
            }

            trans.Commit();
            return Result.Succeeded;
        }
    }
}
