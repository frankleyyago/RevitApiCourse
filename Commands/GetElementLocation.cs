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

            //GetLocationPoint(PickMethod_PickObject());
            GetLocationCurve(PickMethod_PickObject());

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

        #region GetLocationCurve
        /// <summary>
        /// Get picked element (wall) curve
        /// </summary>
        /// <param name="e">picked element</param>
        public void GetLocationCurve(Element e)
        {
            string s = string.Empty;

            //get element e location
            Location elementLocation = e.Location;
            //get element  location curve
            LocationCurve locCurve = (LocationCurve)elementLocation;
            //get locCurve curve
            Curve crv = locCurve.Curve;

            s += $"StartPoint X: {crv.GetEndPoint(0).X}\n";
            s += $"StartPoint Y: {crv.GetEndPoint(0).Y}\n";
            s += $"StartPoint Z: {crv.GetEndPoint(0).Z}\n";
            s += $"EndPoint X: {crv.GetEndPoint(1).X}\n";
            s += $"Endpoint Y: {crv.GetEndPoint(1).Y}\n";
            s += $"Endpoint Z: {crv.GetEndPoint(1).Z}\n";

            TaskDialog.Show("Curves", s);
        }
        #endregion
    }
}
