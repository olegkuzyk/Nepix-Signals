using System;
using NepixSignals.Api;

#pragma warning disable 0649
namespace NepixSignals
{
    public abstract partial class ABaseSignal<T>
    {
        private void __Off(T handler)
        {
            // Find all Callbacks that holds handler. 
            _toRemove.InsertRange(0,_callbacks.FindAll(callback => callback.handler.Equals(handler)));
            
            // Use another list,
            // because we cant modify it while iterating.
            _toRemove.ForEach(c => _callbacks.Remove(c));
            _toRemove.Clear();
        }
        
        private ISignalCallback _Off(T handler)
        {
            var callback = Get(handler);

            if (!_isDispatching) __Off(handler);
            else _callAfterDispatch.Add(() => _Off(handler));

            return callback;
        }
        
        private ISignalCallback _Off(Callback callback)
        {
            if (!Has(callback)) throw new Exception("Signal does not contain this callback.");

            if (!_isDispatching) _callbacks.Remove(callback);
            else _callAfterDispatch.Add(() => _callbacks.Remove(callback));

            return callback;
        }

        /// <summary>
        /// Removes a listener from this Signal.
        /// If was added multiple times the same handler
        /// that all of its instances will be removed.
        /// If you need to remove specific callback
        /// than do it via ISignalCallback.Off()
        /// </summary>
        /// <param name="handler">Method to be unregistered from signal</param>
        public ISignalCallback Off(T handler)
        {
            return _Off(handler);
        }

        /// <summary>
        /// Removes a listener from this Signal.
        /// </summary>
        /// <param name="callback">Callback to be unregistered from signal</param>
        private ISignalCallback Off(Callback callback)
        {
            return _Off(callback);
        }

        private void _OffAll()
        {
            _callbacks.Clear();
        }

        /// <summary>
        /// Remove all listeners.
        /// </summary>
        public void OffAll()
        {
            if (!_isDispatching) _OffAll();
            else _callAfterDispatch.Add(_OffAll);
        }
    }
}