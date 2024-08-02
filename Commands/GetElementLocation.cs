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
    internal class GetElementLocation : IExternalCommand
    {
        //create global variables
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

            GetLocationPoint(PickMethod_PickObject());

            return Result.Succeeded;
        }

        #region PickMethod_PickObject()
        /// <summary>
        /// Prompt the user to select an element and retrieve this element.
        /// </summary>
        public Element PickMethod_PickObject()
        {
            Reference r = _uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select an element");

            Element e = _uidoc.Document.GetElement(r);

            return e;
        }
        #endregion

        #region GetLocationPoint
        /// <summary>
        /// Retrieve element XYZ coordinate.
        /// </summary>
        /// <param name="e">Picked element</param>
        public void GetLocationPoint(Element e)
        {
            //get element e location
            Location elementLocation = e.Location;
            //get element location point
            LocationPoint locPoint = (LocationPoint)elementLocation;
            //get element coordinate
            XYZ coord = locPoint.Point;

            TaskDialog.Show("Element location", $"Coord X:{coord.X}\n Coord Y:{coord.Y}\n Coord Z:{coord.Z}");
        }
        #endregion
    }
}
