using Isekai.Interactions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GamedevsToolbox.ScriptableArchitecture.Events;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.Buildings
{
    public abstract class InteractableBuilding : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GenericGameEvent hoverStartEvent = default;
        [SerializeField]
        private GenericGameEvent hoverEndEvent = default;
        [SerializeField]
        private GenericGameEvent cameraMoveToEvent = default;
        [SerializeField]
        private ScriptableFloatReference zoomReference = default;
        [SerializeField]
        protected BuildingInteractionDataTemplate[] buildingInteractionData = default;
        [SerializeField]
        protected Isekai.UI.BuildingInteractionProgressUI progressUI = default;
        [SerializeField]
        protected Isekai.UI.BuildingInteractionSelectionUI interactionSelectionUI = default;
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

        public void DispatchCommand(UnityAction<InteractionCommand[]> dispatchAction)
        {
            if (IsBusy)
            {
                dispatchAction(new InteractionCommand[] { new InteractionNotAvailableCommand("This building is not interactable right now") });
            } else if (!globalAllowBuildingsUsage.GetValue())
            {
                dispatchAction(new InteractionCommand[] { new InteractionNotAvailableCommand("You can't use buildings right now") });
            } else
            {
                if (buildingInteractionData.Length > 1)
                {
                    List<UnityAction> actionList = new List<UnityAction>();
                    for (int i = 0; i < buildingInteractionData.Length; ++i)
                    {
                        int index = i;
                        actionList.Add(() => { dispatchAction(GetUniqueCommandSet(index)); });
                    }
                    interactionSelectionUI.Init(buildingInteractionData, actionList);
                    zoomReference?.SetValue(0f);
                    cameraMoveToEvent?.Raise(interactionSelectionUI.transform.position);
                }
                else
                {
                    dispatchAction(GetUniqueCommandSet(0));
                }
            }
        }

        public abstract InteractionCommand[] GetUniqueCommandSet(int i);

        public InteractionType GetInteractionType()
        {
            return InteractionType.Building;
        }

        public void OnCharacterUnselected()
        {
            interactionSelectionUI.Close();
        }

        public void OnInteractionRequested()
        {
            interactionSelectionUI.Close();
        }
    }
}