using Isekai.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Buildings
{
    public abstract class InteractableBuilding : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Events.GenericGameEvent hoverStartEvent;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Events.GenericGameEvent hoverEndEvent;
        [SerializeField]
        protected Transform enterPoint = default;

        public void OnHoverStart()
        {
            hoverStartEvent?.Raise(this);
        }

        public void OnHoverEnd()
        {
            hoverEndEvent?.Raise(this);
        }

        public abstract InteractionCommand[] DispatchCommand();

        public InteractionType GetInteractionType()
        {
            return InteractionType.Building;
        }
    }
}