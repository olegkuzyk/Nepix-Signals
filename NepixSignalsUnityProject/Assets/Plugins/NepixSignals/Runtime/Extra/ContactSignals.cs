using NepixSignals;
using UnityEngine;

namespace NepixCore.Tools.Physics
{
    /// <summary>
    /// Little Signals for physics events.
    /// </summary>
    public static class ContactSignals
    {
        #region Contact

        /// <summary>
        /// Struct that dispatch when a phys event happens.
        /// </summary>
        public struct Contact
        {
            /// <summary>
            /// Collider that has attached Contact Listener.
            /// </summary>
            public Collider self;
            
            /// <summary>
            /// Other collider with which phys event (collision/trigger) happens.
            /// </summary>
            public Collider other;
            
            /// <summary>
            /// Collision data.
            /// It's null for trigger(s).
            /// </summary>
            public Collision collision; 
        }

        /// <summary>
        /// Contact Signal.
        /// </summary>
        public class ContactSignal : ASignal<Contact>
        {
            public readonly ContactListenerBase listener;

            public ContactSignal(ContactListenerBase listener) { this.listener = listener; }
        }

        #endregion

        #region GameObject Extensions
        
        /// <summary>
        /// Add Collision Enter Signal.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static ContactSignal AddCollisionEnterSignal(this GameObject gameObject) => gameObject.AddComponent<CollisionEnter>().signal;
        
        /// <summary>
        /// Add Collision Stay Signal.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static ContactSignal AddCollisionStaySignal(this GameObject gameObject) => gameObject.AddComponent<CollisionStay>().signal;
        
        /// <summary>
        /// Add Collision Exit Signal.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static ContactSignal AddCollisionExitSignal(this GameObject gameObject) => gameObject.AddComponent<CollisionExit>().signal;
        
        /// <summary>
        /// Add Trigger Enter Signal.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static ContactSignal AddTriggerEnterSignal(this GameObject gameObject) => gameObject.AddComponent<TriggerEnter>().signal;
        
        /// <summary>
        /// Add Trigger Stay Signal.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static ContactSignal AddTriggerStaySignal(this GameObject gameObject) => gameObject.AddComponent<TriggerStay>().signal;
        
        /// <summary>
        /// Add Trigger Exit Signal.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static ContactSignal AddTriggerExitSignal(this GameObject gameObject) => gameObject.AddComponent<TriggerExit>().signal;

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [RequireComponent(typeof(Collider))]
        public abstract class ContactListenerBase : MonoBehaviour
        {
            private Contact _contact;
            
            /// <summary>
            /// Contact Signal.
            /// </summary>
            public ContactSignal signal { get; private set; }

            private void Awake()
            {
                signal = new ContactSignal(this);
                _contact = new Contact {self = GetComponent<Collider>()};
            }

            protected void Dispatch(Collider other, Collision collision = null)
            {
                _contact.other = other;
                _contact.collision = collision;
                signal.Dispatch(_contact);
            }

            /// <summary>
            /// Manual Destroy.
            /// </summary>
            public void Destroy()
            {
                signal.OffAll();
                Destroy(this);
            }
        }

        #region Collision

        public class CollisionEnter : ContactListenerBase
        {
            private void OnCollisionEnter(Collision other) => Dispatch(other.collider, other);
        }

        public class CollisionStay : ContactListenerBase
        {
            private void OnCollisionStay(Collision other) => Dispatch(other.collider, other);
        }

        public class CollisionExit : ContactListenerBase
        {
            private void OnCollisionExit(Collision other) => Dispatch(other.collider, other);
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Trigger

        public class TriggerEnter : ContactListenerBase
        {
            private void OnTriggerEnter(Collider other) => Dispatch(other);
        }

        public class TriggerStay : ContactListenerBase
        {
            private void OnTriggerStay(Collider other) => Dispatch(other);
        }

        public class TriggerExit : ContactListenerBase
        {
            private void OnTriggerExit(Collider other) => Dispatch(other);
        }

        #endregion
    }
}