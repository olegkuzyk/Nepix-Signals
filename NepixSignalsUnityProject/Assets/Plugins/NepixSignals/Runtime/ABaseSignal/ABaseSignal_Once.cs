
using NepixSignals.Api;

#pragma warning disable 0649
namespace NepixSignals
{
    public partial class ABaseSignal<T>
    {
         /// <summary>
        /// Adds a listener to this Signal. This will fire once and than will be removed.
        /// </summary>
        /// <param name="handler">Method to be called when signal is fired</param>
        public ISignalCallback Once(T handler)
        {
            return _On(handler).Countdown(1);
        }
        
        /// <summary>
        /// Adds a listener to this Signal. This will fire once and than will be removed.
        /// </summary>
        /// <param name="handler">Method to be called when signal is fired</param>
        /// <param name="single">Must contains only one instance of handler</param>
        public ISignalCallback Once(T handler, bool single)
        {
            if (single && Has(handler)) return Get(handler);
            
            return Once(handler);
        }
        
        /// <summary>
        /// Adds a listener to this Signal. This will fire once and than will be removed.
        /// </summary>
        /// <param name="callback">Callback to be called when signal is fired</param>
        private ISignalCallback Once(Callback callback)
        {
            return _On(callback).Countdown(1);
        }

        /// <summary>
        /// Adds a listener to this Signal. This will fire once and than will be removed.
        /// </summary>
        /// <param name="isOn">Whether ONCE or OFF the handler</param>
        /// <param name="handler">Method to be called when signal is fired</param>
        public ISignalCallback Once(bool isOn, T handler)
        {
            return isOn ? Once(handler) : Off(handler);
        }

        /// <summary>
        /// Adds a listener to this Signal. This will fire once and than will be removed.
        /// </summary>
        /// <param name="isOn">Whether ONCE or OFF the handler</param>
        /// <param name="handler">Method to be called when signal is fired</param>
        /// <param name="single">Must contains only one instance of handler</param>
        public ISignalCallback Once(bool isOn, T handler, bool single)
        {
            return isOn ? Once(handler, single) : Off(handler);
        }
    }
}