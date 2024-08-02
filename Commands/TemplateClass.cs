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
    internal class TemplateClass : IExternalCommand
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

            return Result.Succeeded;
        }

        #region function01
        public void function01()
        {

        }
        #endregion
    }
}
