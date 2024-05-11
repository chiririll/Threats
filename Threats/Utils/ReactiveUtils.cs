using System;
using System.Reactive.Disposables;

namespace Threats;

public static class ReactiveUtils
{
    public static void AddTo(this IDisposable disp, CompositeDisposable composite) => composite.Add(disp);
}
