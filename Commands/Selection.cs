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
    [Transaction(TransactionMode.Manual)]
    internal class Selection : IExternalCommand
    {
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

            //ShowElementInfo(PickMethod_PickObject());
            ShowElementListInfo(PickMethod_PickObjects());

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

        #region ShowElementInfo()
        /// <summary>
        /// Show element information.
        /// </summary>
        /// <param name="e">Picked element</param>
        public void ShowElementInfo(Element e)
        {
            string s = e.Name;

            TaskDialog.Show("Element information", $"Element name: {s}");
        }
        #endregion

        #region PickMethod_PickObjects()
        /// <summary>
        /// Prompt the user to select multiple elements.
        /// </summary>
        /// <returns>Element list</returns>
        public IList<Element> PickMethod_PickObjects()
        {
            IList<Reference> refs = _uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select multiple elements");

            IList<Element> elements = new List<Element>();

            foreach (Reference r in refs)
            {
                Element e = _uidoc.Document.GetElement(r);

                elements.Add(e);
            }

            return elements;
        }
        #endregion

        #region ShowElementListInfo()
        /// <summary>
        /// Show a list of element name
        /// </summary>
        /// <param name="elements">Element list</param>
        public void ShowElementListInfo(IList<Element> elements)
        {
            string s = string.Empty;

            foreach (Element e in elements)
            {
                s += $"{e.Name}\n";
            }

            TaskDialog.Show("Element information", $"Elements name: {s}");
        }
        #endregion       
    }
}
