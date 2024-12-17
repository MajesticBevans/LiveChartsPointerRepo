using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Kernel;
using System.Text.Json.Serialization;

namespace LiveChartsPointerRepo.ViewModels;

public partial class ObservableFloatPoint : ObservableObject, IChartEntity
{
    [ObservableProperty]
    private float? _x;

    [ObservableProperty]
    private float? _y;

    public ObservableFloatPoint(float x, float y)
    {
        X = x;
        _y = y;
    }

    [JsonIgnore]
    public ChartEntityMetaData? MetaData { get; set; }

    [JsonIgnore]
    public Coordinate Coordinate
    {
        get => X.HasValue && Y.HasValue ? new Coordinate(X.Value, Y.Value) : Coordinate.Empty;
        set
        {
            if (Coordinate.PrimaryValue == Y && Coordinate.SecondaryValue == X)
                return;

            X = (float)value.SecondaryValue;
            Y = (float)value.PrimaryValue;
        }
    }
}
