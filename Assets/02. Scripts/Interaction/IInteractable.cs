using UnityEngine.Events;
using Isekai.Characters;

namespace Isekai.Interactions
{
    public interface IInteractable
    {
        void DispatchCommand(Character character, UnityAction<InteractionCommand[]> dispatchAction);
        InteractionType GetInteractionType();
    }
}