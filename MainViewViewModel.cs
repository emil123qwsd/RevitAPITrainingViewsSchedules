using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingViewsSchedules
{
    public class MainViewViewModel
    {
        public ExternalCommandData _commandData;

        public Document _doc;

        public List<ViewPlan> Views { get; }

        public List<Category> Categories { get; }

        public DelegateCommand HideCommand { get; }

        public DelegateCommand TempHideCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData) 
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;
            Views = ViewsUtils.GetFloorPlanViews(_doc);
            Categories = CategoryUtils.GetCategories(_doc);
            HideCommand = new DelegateCommand(OnHideCommand);
            TempHideCommand = new DelegateCommand(OnTempHideCommand);
        }

        public void OnTempHideCommand()
        {
            if (SelectedViewPlan == null || SelectedCategoty == null)
                return;
            using (var ts = new Transaction(_doc, "Save changes"))
            {
                ts.Start();
                SelectedViewPlan.HideCategoryTemporary(SelectedCategoty.Id);
                ts.Commit();
            }
            ReiseCloseRequest();
        }

        public void OnHideCommand() 
        {
            if (SelectedViewPlan == null || SelectedCategoty == null)
                return;
            using (var ts = new Transaction(_doc, "Save changes")) 
            {
                ts.Start();
                SelectedViewPlan.SetCategoryHidden(SelectedCategoty.Id, hide: true);
                ts.Commit();
            }
            ReiseCloseRequest();
        }

        public ViewPlan SelectedViewPlan { get; set; }

        public Category SelectedCategoty { get; set; }

        public event EventHandler CloseRequest;

        public void ReiseCloseRequest() 
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
