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
    internal class CreateTransaction : IExternalCommand
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

            DeleteElement(PickMethod_PickObject());

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

        /// <summary>
        /// Delete a picked element
        /// </summary>
        /// <param name="e">Picked element</param>
        #region DeleteElement
        public void DeleteElement(Element e)
        {
            using (Transaction trx = new Transaction(_doc, "Delete Element"))
            {
                trx.Start();

                TaskDialog myTaskDialog = new TaskDialog("Delete element");
                myTaskDialog.MainContent = "Are you sure you want to delete the picked element?";
                myTaskDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;

                if (myTaskDialog.Show() == TaskDialogResult.Ok)
                {
                    _doc.Delete(e.Id);
                    trx.Commit();
                }
                else
                {
                    trx.RollBack();
                }
                
            }
        }
        #endregion
    }
}
