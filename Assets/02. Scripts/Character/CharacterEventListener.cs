using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Events;
using UnityEngine.Events;

namespace Isekai.Characters
{
    public class CharacterEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        public CharacterEvent Event;

        [System.Serializable]
        public class ObjectEvent : UnityEvent<Character> { };

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField]
        public ObjectEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Character data)
        {
            Response.Invoke(data);
        }
    }
}