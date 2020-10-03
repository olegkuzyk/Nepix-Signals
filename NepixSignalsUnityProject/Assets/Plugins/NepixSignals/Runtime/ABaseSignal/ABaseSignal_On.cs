
using System;
using NepixSignals.Api;

#pragma warning disable 0649
namespace NepixSignals
{
    public abstract partial class ABaseSignal<T>
    {
        private ISignalCallback _On(Callback callback)
        {
            if (Has(callback)) throw new Exception("Signal already contains this callback.");
            
            if (!_isDispatching)
            {
                _callbacks.Add(callback);
                Sort();
            }
            else
            {
                _callAfterDispatch.Add(() =>
                {
                    _callbacks.Add(callback);
                    Sort();
                });
            }
            
            return callback;
        }
        
        private ISignalCallback _On(T handler)
        {
            return _On(new Callback(this, handler));
        }

        /// <summary>
        /// Adds a listener to this Signal
        /// </summary>
        /// <param name="handler">Method to be called when signal is fired</param>
        public ISignalCallback On(T handler)
        {
            return _On(handler);
        }

        /// <summary>
        /// Adds a listener to this Signal
        /// </summary>
        /// <param name="callback">Callback to be called when signal is fired</param>
        private ISignalCallback On(Callback callback)
        {
            return _On(callback);
        }

        /// <summary>
        /// Adds a listener to this Signal
        /// </summary>
        /// <param name="handler">Method to be called when signal is fired</param>
        /// <param name="single">Must contains only one instance of handler</param>
        public ISignalCallback On(T handler, bool single)
        {
            if (single && Has(handler)) return Get(handler);
            
            return On(handler);
        }

        /// <summary>
        /// Adds a listener to this Signal
        /// </summary>
        /// <param name="isOn">Whether ON or OFF the handler</param>
        /// <param name="handler">Method to be called when signal is fired</param>
        public ISignalCallback On(bool isOn, T handler)
        {
            return isOn ? On(handler) : Off(handler);
        }

        /// <summary>
        /// Adds a listener to this Signal
        /// </summary>
        /// <param name="isOn">Whether ON or OFF the handler</param>
        /// <param name="handler">Method to be called when signal is fired</param>
        /// <param name="single">Must contains only one instance of handler</param>
        /// <param name="priority">Priority of the handler. The higher it is the sooner handler will shot.</param>
        /// <param name="dispatchAmount">How many times listener should be notidied.</param>
        public ISignalCallback On(bool isOn, T handler, bool single)
        {
            return isOn ? On(handler, single) : Off(handler);
        }
    }
}