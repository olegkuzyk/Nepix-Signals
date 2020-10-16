using System;

#pragma warning disable 0649
namespace NepixSignals.Api
{
    /// <summary>
    /// Signal's Callback interface.
    /// Use it if you want to store ref of callback in a var/field/prop.
    /// </summary>
    public interface ISignalCallback
    {
        /// <summary>
        /// Priority of callback.
        /// </summary>
        int priority { get; }

        /// <summary>
        /// How many times left to call before remove.
        /// Negative value means infinite calls amount.
        /// </summary>
        int countdown { get; }

        /// <summary>
        /// Check if callback added to a signal.
        /// </summary>
        /// <returns></returns>
        bool IsAddedToSignal();

        /// <summary>
        /// On again the callback to signal, in case it was off.
        /// Warning: exception could be fired
        /// if signal already has this callback.
        /// </summary>
        void On();

        /// <summary>
        /// Once again the callback to signal, in case it was off.
        /// Warning: exception could be fired
        /// if signal already has this callback.
        /// </summary>
        void Once();

        /// <summary>
        /// Off from signal.
        /// </summary>
        void Off();

        /// <summary>
        /// The handler will be called only when predicate fulfilled.
        /// </summary>
        /// <param name="predicate">When predicate</param>
        /// <returns>Callback</returns>
        ISignalCallback When(Func<bool> predicate);

        /// <summary>
        /// Set priority of callback.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ISignalCallback Priority(int value);

        /// <summary>
        /// Set countdown for callback.
        /// How many times it should be called.
        /// Negative value means infinite calls amount.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ISignalCallback Countdown(int value);
    }
}