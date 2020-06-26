using UnityEngine;

namespace Isekai.Interactions
{
    public interface IInteractable
    {
        InteractionCommand[] DispatchCommand();
        InteractionType GetInteractionType();
    }
}