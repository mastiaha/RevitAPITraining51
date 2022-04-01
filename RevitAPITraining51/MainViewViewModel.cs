using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining51
{
   public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand NumberOfPipesCommand { get; }
        public DelegateCommand VolumeOfWallsCommand { get; }
        public DelegateCommand NumberOfDoorsCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            NumberOfPipesCommand = new DelegateCommand(OneNumberOfPipesCommand);
            VolumeOfWallsCommand = new DelegateCommand(OneVolumeOfWallsCommand);
            NumberOfDoorsCommand = new DelegateCommand(OneNumberOfDoorsCommand);
        }

        public event EventHandler HideRequest;


        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ShowRequest;

        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OneNumberOfPipesCommand()
        {
            RaiseHideRequest();
            List<Pipe> pipes = Class1.NumberOfPipes(_commandData);
            TaskDialog.Show("Количество труб", $"Количество труб:{pipes.Count.ToString()} шт.");
            RaiseShowRequest();
        }

        private void OneVolumeOfWallsCommand()
        {
            RaiseHideRequest();
            double volume;
            int count;
            Class1.VolumeOfWalls(_commandData, out volume, out count);
            TaskDialog.Show("Объем всех стен", $"объем всех стен:{volume} м³"+
                $"{Environment.NewLine}Количество стен: {count} шт.");
            RaiseShowRequest();
        }

        private void OneNumberOfDoorsCommand()
        {
            RaiseHideRequest();
            List<FamilyInstance> doors = Class1.NumberOfDoors(_commandData);
            TaskDialog.Show("Количество дверей", $"Количество дверей:{doors.Count.ToString()} шт.");
            RaiseShowRequest();
        }




        //private void OneSelectCommand()
        //{
        //    ReiseCloseRequest();
        //    UIApplication uiapp = _commandData.Application;
        //    UIDocument uidoc = uiapp.ActiveUIDocument;
        //    Document doc = uidoc.Document;

        //    var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
        //    var oElement = doc.GetElement(selectedObject);

        //    TaskDialog.Show("Сообщение", $"ID:{oElement.Id}");
        //}
    }
}
