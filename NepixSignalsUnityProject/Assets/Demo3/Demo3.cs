using System;
using NepixCore.Tools.Physics;
using UnityEngine;

#pragma warning disable 0649

namespace Demo3
{
    public class Demo3 : MonoBehaviour
    {
        private void Start()
        {
            gameObject.AddCollisionEnterSignal().On(contact => Debug.Log(contact));
            gameObject.AddCollisionStaySignal().On(contact => Debug.Log(contact));
            gameObject.AddCollisionExitSignal().On(contact => Debug.Log(contact));
            
            gameObject.AddTriggerEnterSignal().On(contact => Debug.Log(contact));
            gameObject.AddTriggerStaySignal().On(contact => Debug.Log(contact));
            gameObject.AddTriggerExitSignal().On(contact => Debug.Log(contact));
        }
    }
}