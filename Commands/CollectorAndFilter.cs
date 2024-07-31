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

            CountElement(FilterWalls());

            return Result.Succeeded;
        }

        #region FilterDoors
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

        #region FilterWalls()
        /// <summary>
        /// Filters walls
        /// </summary>
        /// <returns>filtered walls.</returns>
        public IList<Element> FilterWalls()
        {
            ElementId viewId = _doc.ActiveView.Id;

            //create a collector.
            FilteredElementCollector collector = new FilteredElementCollector(_doc, viewId);

            //create a filter (wall).
            ElementClassFilter filter = new ElementClassFilter(typeof(Wall));

            //Narrow down the collector.
            IList<Element> filteredWalls = collector
                .WherePasses(filter)
                .WhereElementIsNotElementType()
                .ToElements();

            return filteredWalls;
        }
        #endregion

        #region CountElement
        /// <summary>
        /// Counts the elements.
        /// </summary>
        /// <param name="filteredElements">Elements amount</param>
        public void CountElement(IList<Element> filteredElements)
        {
            TaskDialog.Show("Elements", $"Total element: {filteredElements.Count}");
        }
        #endregion
    }
}
