using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Events;

namespace Isekai.Characters
{
    [CreateAssetMenu(menuName = "Isekai/Events/Character event")]
    public class CharacterEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<CharacterEventListener> eventListeners =
            new List<CharacterEventListener>();

        public void Raise(Character data)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(data);
        }

        public void RegisterListener(CharacterEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(CharacterEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}