using UnityEngine;

#pragma warning disable 0649

namespace NepixSignals
{
    /// <summary>
    /// Default signals. If you don't want to create simple signals or use a hub.
    /// </summary>
    public class TheSignal : ASignal { }

    public class TheSignalInt : ASignal<int> { }
    public class TheSignalDouble : ASignal<double> { }

    public class TheSignalFloat : ASignal<float> { }

    public class TheSignalBool : ASignal<bool> { }

    public class TheSignalColor : ASignal<Color> { }

}