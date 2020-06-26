using UnityEngine;
using UnityEngine.Events;

namespace GamedevsToolbox.ScriptableArchitecture.Events
{
    public class AudioGameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        public AudioGameEvent Event;

        [System.Serializable]
        public class ObjectEvent : UnityEvent<Audio.ScriptableAudioClip> { };

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

        public void OnEventRaised(Audio.ScriptableAudioClip data)
        {
            Response.Invoke(data);
        }
    }
}
