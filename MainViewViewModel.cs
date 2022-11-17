using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using RevitAPITrainingLibrary;
using RevitAPITrainingViewsSchedules.Wrappers;
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

        //public List<ScheduleWrapper> Schedules { get; }

        public DelegateCommand Create { get; set; }

        public string ParameterName { get; set; }

        //public string ParameterValue { get; set; }
        
        public List<ViewSheet> SelectedTitleBlock { get; set; }

        //public List<ViewPlan> Views { get; }

        //public List<ParameterFilterElement> Filters { get; }

        //public DelegateCommand AddFilterCommand { get; }

        //public List<Category> Categories { get; }

        //public DelegateCommand HideCommand { get; }

        //public DelegateCommand TempHideCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData) 
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;
            //Schedules = ScheduleUtils.GetAllTheSchedules(_doc).Select(s=> new ScheduleWrapper(s)).ToList();
            //AddFilterCommand = new DelegateCommand(OnAddFilterCommand);
            //Views = ViewsUtils.GetFloorPlanViews(_doc);
            //Filters = FilterUtils.GetFilter(_doc);
            SelectedTitleBlock = ListUtils.GetSheet(_doc);
            Create = new DelegateCommand(OnCreate);
            //Categories = CategoryUtils.GetCategories(_doc);
            //HideCommand = new DelegateCommand(OnHideCommand);
            //TempHideCommand = new DelegateCommand(OnTempHideCommand);
        }

        private void OnCreate()
        {
            if (SelectedTitleBlock == null)
                return;
            using (var ts = new Transaction(_doc, "Create list"))
            {
                ts.Start();
                var viewSheet = ViewSheet.Create(_doc, SelectedTitleBlock.Id);
                //foreach (var schedule in selectedSchedules)
                //{
                //    var scheduleDef = schedule.ViewSchedule.Definition;
                //    if (scheduleDef == null)
                //        continue;

                //    ScheduleField field = FindField(schedule.ViewSchedule, ParameterName);

                //    if (field == null)
                //    {
                //        SchedulableField schedulableField = FindSchedulableField(schedule.ViewSchedule, ParameterName);
                //        if (schedulableField == null)
                //            continue;

                //        field = scheduleDef.AddField(schedulableField);
                //    }

                //    if (field == null)
                //        continue;

                //    var filter = new ScheduleFilter(field.FieldId, ScheduleFilterType.Equal, ParameterValue);
                //    if (filter == null)
                //        continue;
                //    scheduleDef.AddFilter(filter);
                //}
                ts.Commit();
            }
            
        }

        //private SchedulableField FindSchedulableField(ViewSchedule viewSchedule, string parameterName)
        //{
        //    var schedulableField=viewSchedule.Definition.GetSchedulableFields()
        //                         .FirstOrDefault(p=>p.GetName(viewSchedule.Document)==parameterName);
        //    return schedulableField;
        //}

        //private ScheduleField FindField(ViewSchedule viewSchedule, string parameterName)
        //{
        //    ScheduleDefinition definition = viewSchedule.Definition;
        //    var fieldCount = definition.GetFieldCount();

        //    for (int i = 0; i < fieldCount; i++)
        //    {
        //        var field = definition.GetField(i);
        //        var fieldName = field.GetName();

        //        if (fieldName == parameterName)
        //            return definition.GetField(i);
        //    }
        //    return null;
        //}

        //public ParameterFilterElement SelectedFilter { get; set; }

        //private void OnAddFilterCommand()
        //{
        //    if (SelectedViewPlan == null || SelectedFilter==null)
        //        return;
        //    using (var ts = new Transaction(_doc, "Set Filter"))
        //    {
        //        ts.Start();
        //        SelectedViewPlan.AddFilter(SelectedFilter.Id);
        //        OverrideGraphicSettings overrideGraphicSettings = SelectedViewPlan.GetFilterOverrides(SelectedFilter.Id);
        //        overrideGraphicSettings.SetProjectionLineColor(new Color(255, 0, 0));
        //        SelectedViewPlan.SetFilterOverrides(SelectedFilter.Id, overrideGraphicSettings);
        //        ts.Commit();
        //    }
        //    ReiseCloseRequest();
        //}

        //public void OnTempHideCommand()
        //{
        //    if (SelectedViewPlan == null || SelectedCategory == null)
        //        return;
        //    using (var ts = new Transaction(_doc, "Save changes"))
        //    {
        //        ts.Start();
        //        SelectedViewPlan.HideCategoryTemporary(SelectedCategory.Id);
        //        ts.Commit();
        //    }
        //    ReiseCloseRequest();
        //}

        //public void OnHideCommand() 
        //{
        //    if (SelectedViewPlan == null || SelectedCategory == null)
        //        return;
        //    using (var ts = new Transaction(_doc, "Save changes")) 
        //    {
        //        ts.Start();
        //        SelectedViewPlan.SetCategoryHidden(SelectedCategory.Id, hide: true);
        //        ts.Commit();
        //    }
        //    ReiseCloseRequest();
        //}

        public ViewPlan SelectedViewPlan { get; set; }

        //public Category SelectedCategory { get; set; }

        public event EventHandler CloseRequest;

        public void ReiseCloseRequest() 
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
