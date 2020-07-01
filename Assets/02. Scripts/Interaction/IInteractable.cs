using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Interactions
{
    public interface IInteractable
    {
        void DispatchCommand(UnityAction<InteractionCommand[]> dispatchAction);
        InteractionType GetInteractionType();
    }
}