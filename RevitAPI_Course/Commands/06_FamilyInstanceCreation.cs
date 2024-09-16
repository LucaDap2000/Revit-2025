﻿using System;
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

    public class _06_FamilyInstanceExtraction : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Selection or Extraction
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            List<FamilySymbol> allColumnsFamilySymbols = Extraction.getAllFamilySymbolsOfCategoryFamilyName(doc, BuiltInCategory.OST_StructuralColumns, "UC-Universal Columns-Column");
            List<Level> allLevels = Extraction.getAllLevelsFromModel(doc);

            // Creation
            Transaction trans = new Transaction(doc);
            trans.Start("StartingProcess");
            if (!allColumnsFamilySymbols[0].IsActive)
            {
                allColumnsFamilySymbols[0].Activate();
                doc.Regenerate();
            }

            // Creation Process
            FamilyInstance fam = doc.Create.NewFamilyInstance(new XYZ(0, 0, 0), allColumnsFamilySymbols[0], allLevels[1], StructuralType.Column);

            trans.Commit();
            return Result.Succeeded;
        }
    }
}
