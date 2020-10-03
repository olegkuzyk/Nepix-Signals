namespace NepixSignals.Api
{
    /// <summary>
    /// Base interface for Signals
    /// </summary>
    public interface ISignal
    {
        SignalHub hub { get; set; }
        string hash { get; }
    }
}