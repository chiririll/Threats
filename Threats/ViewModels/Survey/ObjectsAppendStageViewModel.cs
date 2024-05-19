using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Threats.Data.Entities;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class ObjectsAppendStageViewModel : SurveyStageViewModel<ObjectsAppendStage>
{
    public ObjectsAppendStageViewModel(ObjectsAppendStage stage) : base(stage)
    {
        SelectedObjects = new(stage.Objects);
        AvailableObjects = new(stage.GetAvailableObjects());

        AddObjectCommand = ReactiveCommand.Create(() => AddObject(SelectedItem));
    }

    public ObservableCollection<Object> SelectedObjects { get; }
    public ObservableCollection<Object> AvailableObjects { get; }

    public Object? SelectedItem { get; set; }
    public System.IObservable<Unit> AddObjectCommand { get; }

    public void AddObject(Object? obj)
    {
        if (obj == null || !stage.AddObject(obj))
        {
            return;
        }

        SelectedObjects.Add(obj);
        AvailableObjects.Remove(obj);
    }

    public override void Refresh()
    {
    }
}
