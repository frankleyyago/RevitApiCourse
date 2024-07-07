using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace RevitApiCourse
{
    [Transaction(TransactionMode.ReadOnly)]
    internal class CollectorAndFilter : IExternalCommand
    {
        //Create global variables
        UIApplication _uiapp;
        Application _app;
        UIDocument _uidoc;
        Document _doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Creating App and doc objects.
            _uiapp = commandData.Application;
            _app = _uiapp.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            CountDoors(FilterDoors());

            return Result.Succeeded;
        }

        #region
        /// <summary>
        /// Filter doors in the model.
        /// </summary>
        /// <returns>Filtered doors.</returns>
        public IList<Element> FilterDoors()
        {
            //create a collector.
            FilteredElementCollector collector = new FilteredElementCollector(_doc);

            //create a filter (door).
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);

            IList<Element> filteredDoors = collector
                .WherePasses(filter)
                .WhereElementIsNotElementType()
                .ToElements();

            return filteredDoors;
        }
        #endregion

        #region
        /// <summary>
        /// Counts the doors.
        /// </summary>
        /// <param name="filteredDoors"></param>
        public void CountDoors(IList<Element> filteredDoors)
        {
            TaskDialog.Show("Doors", $"Total doors: {filteredDoors.Count}");
        }
        #endregion
    }
}
