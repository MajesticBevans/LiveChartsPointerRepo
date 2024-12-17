﻿using System.IO;
using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;

namespace LiveChartsPointerRepo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private Axis[] _xAxes = [new Axis()];
    
    [ObservableProperty]
    private Axis[] _yAxes = [new Axis()];

    [ObservableProperty]
    private LineSeries<ObservableFloatPoint>[] _plottedSeries =
    [
        new LineSeries<ObservableFloatPoint>()
        {
            Values = new ObservableCollection<ObservableFloatPoint>(),
            GeometrySize = 0,
            GeometryFill = null,
            XToolTipLabelFormatter = value => $"x val: {value.Coordinate.SecondaryValue:G5}"
        },
        new LineSeries<ObservableFloatPoint>()
        {
            Values = new ObservableCollection<ObservableFloatPoint>(),
            GeometrySize = 0,
            GeometryFill = null,
            XToolTipLabelFormatter = value => $"x val: {value.Coordinate.SecondaryValue:G5}"
        }
    ];


    public MainWindowViewModel()
    {
        StreamReader reader = new(Path.Combine(".", "Assets", "viper3d_th_overpressure.txt"));

        while (reader.ReadLine() is string line)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 3)
                continue;
            if (!float.TryParse(parts[0], out float x) || !float.TryParse(parts[1], out float y1) || !float.TryParse(parts[2], out float y2))
                continue;
            _plottedSeries[0].Values.Add(new ObservableFloatPoint(x, y1));
            _plottedSeries[1].Values.Add(new ObservableFloatPoint(x, y2));
        }
    }
}