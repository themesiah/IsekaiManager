using UnityEngine;
using UnityEngine.Events;

namespace GamedevsToolbox.ScriptableArchitecture.Events
{
    public class LocatedAudioGameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        public LocatedAudioGameEvent Event;

        [System.Serializable]
        public class ObjectEvent : UnityEvent<Audio.LocatedAudioEvent> { };

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

        public virtual void OnEventRaised(Audio.LocatedAudioEvent data)
        {
            Response.Invoke(data);
        }
    }
}
