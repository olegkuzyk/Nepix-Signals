using System;

#pragma warning disable 0649

namespace NepixSignals
{
    /// <summary>
    /// Strongly typed messages with no parameters
    /// </summary>
    public abstract class ASignal : ABaseSignal<Action>
    {
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch()
        {
            DispatchInternal(callback => callback.handler());
        }
    }

    /// <summary>
    /// Strongly typed messages with 1 parameter
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    public abstract class ASignal<T>: ABaseSignal<Action<T>>
    {   
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch(T arg1)
        {
            DispatchInternal(callback => callback.handler(arg1));
        }
    }

    /// <summary>
    /// Strongly typed messages with 2 parameters
    /// </summary>
    /// <typeparam name="T">First parameter type</typeparam>
    /// <typeparam name="U">Second parameter type</typeparam>
    public abstract class ASignal<T, U>: ABaseSignal<Action<T, U>>
    {
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch(T arg1, U arg2)
        {
            DispatchInternal(callback => callback.handler(arg1, arg2));
        }
    }

    /// <summary>
    /// Strongly typed messages with 3 parameter
    /// </summary>
    /// <typeparam name="T">First parameter type</typeparam>
    /// <typeparam name="U">Second parameter type</typeparam>
    /// <typeparam name="V">Third parameter type</typeparam>
    public abstract class ASignal<T, U, V>: ABaseSignal<Action<T, U, V>>
    {
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch(T arg1, U arg2, V arg3)
        {
            DispatchInternal(callback => callback.handler(arg1, arg2, arg3));
        }
    }
}