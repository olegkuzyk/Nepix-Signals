using System;
using System.Collections.Generic;
using NepixSignals.Api;

#pragma warning disable 0649

namespace NepixSignals
{
    /// <summary>
    /// Abstract class for Signals, provides hash by type functionality
    /// </summary>
    public abstract partial class ABaseSignal<T> : ISignal
    {
        private readonly List<Callback> _callbacks = new List<Callback>();
        private readonly List<Callback> _toRemove = new List<Callback>();
        private readonly List<Action> _callAfterDispatch = new List<Action>();
        
        private SignalHub _hub;
        
        private string _hash;
        private bool _isDispatching;
        
        /// <summary>
        /// Total callbacks.
        /// </summary>
        public int count => _callbacks.Count;

        /// <summary>
        /// Hub to which signal attached.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public SignalHub hub
        {
            get => _hub;
            set 
            {
                if (_hub != null) throw new Exception("Hub can be set only once. From SignalHub.");
                _hub = value; 
            } 
        }
        
        /// <summary>
        /// Unique id for this signal
        /// </summary>
        public string hash
        {
            get
            {
                if (string.IsNullOrEmpty(_hash)) _hash = GetType().ToString();
                return _hash;
            }
        }

        /// <summary>
        /// Has handler in any callback.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Has(T handler)
        {
            return _callbacks.Exists(callback => callback.handler.Equals(handler));
        }

        /// <summary>
        /// Has callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        protected bool Has(Callback callback)
        {
            return _callbacks.Contains(callback);
        }

        /// <summary>
        /// Get Callback by handler
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected Callback Get(T handler)
        {
            return _callbacks.Find(c => Equals(c.handler, handler));
        }
        
        /// <summary>
        /// Sort listeners by priority field.
        /// Calling automatically after adding new listeners.
        /// </summary>
        public void Sort()
        {
            _callbacks.Sort((a, b) =>
            {
                if (a.priority > b.priority) return -1;
                if (a.priority < b.priority) return 1;
                return 0;
            });
            
            // _listeners.ForEach(a => Debug.Log(a.priority));
        }

        protected void DispatchInternal(Action<Callback> externalDispatch)
        {
            // -------------------------------------------
            // Dispatch phase
            // -------------------------------------------
            
            _isDispatching = true;
            if (_callbacks.Count > 0) {
                
                _callbacks.ForEach(callback =>
                {
                    if (callback.CanEmit())
                    {
                        callback.EmitStart();
                        externalDispatch.Invoke(callback);
                        callback.EmitEnd();
                    }
                });
            }
            _isDispatching = false;
            
            
            // -------------------------------------------
            // After Dispatch phase
            // -------------------------------------------
            
            // Check is need to clean expired callbacks
            // that countdown is zero
            for (var i = _callbacks.Count - 1; i >= 0; i--)
            {
                if (_callbacks[i].countdown == 0) _callbacks.RemoveAt(i);
            }
            
            // Check if need something to call after dispatch
            if (_callAfterDispatch.Count > 0) {
                _callAfterDispatch.ForEach(action => action());
                _callAfterDispatch.Clear();
            }
        }
    }
}