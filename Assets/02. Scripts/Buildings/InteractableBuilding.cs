using Isekai.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Buildings
{
    public abstract class InteractableBuilding : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Events.GenericGameEvent hoverStartEvent = default;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Events.GenericGameEvent hoverEndEvent = default;
        [SerializeField]
        protected Transform enterPoint = default;
        [SerializeField]
        protected Transform buildingCenterPoint = default;
        [SerializeField]
        protected GameObject positionMarkerPrefab = default;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Values.ScriptableBoolReference globalAllowBuildingsUsage = default;
        [HideInInspector]
        public bool IsBusy = false;

        public void OnHoverStart()
        {
            hoverStartEvent?.Raise(this);
        }

        public void OnHoverEnd()
        {
            hoverEndEvent?.Raise(this);
        }

        public InteractionCommand[] DispatchCommand()
        {
            if (IsBusy)
            {
                return new InteractionCommand[] { new InteractionNotAvailableCommand("This building is not interactable right now") };
            } else if (!globalAllowBuildingsUsage.GetValue())
            {
                return new InteractionCommand[] { new InteractionNotAvailableCommand("You can't use buildings right now") };
            } else
            {
                return GetUniqueCommandSet();
            }
        }

        public abstract InteractionCommand[] GetUniqueCommandSet();

        public InteractionType GetInteractionType()
        {
            return InteractionType.Building;
        }
    }
}