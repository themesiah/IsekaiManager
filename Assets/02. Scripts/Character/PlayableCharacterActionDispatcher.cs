using Isekai.Interactions;
using UnityEngine;

namespace Isekai.Characters
{
    public class PlayableCharacterActionDispatcher : MonoBehaviour
    {
        [SerializeField]
        private RuntimePlayableCharacterSet selectedPlayableCharactersSet = default;
        [SerializeField]
        private Interactions.RuntimeInteractableSet currentInteractableObjectsSet = default;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (currentInteractableObjectsSet.Items.Count > 0)
                {
                    selectedPlayableCharactersSet.ForEach((pc) =>
                    {
                        InteractionCommand[] interactionCommands = currentInteractableObjectsSet.Items[0].DispatchCommand();
                        foreach (InteractionCommand ic in interactionCommands)
                        {
                            ic.SetInteractionCharacter(pc);
                            pc.commandProcessor.ProcessCommand(ic);
                        }
                    });
                } else
                {
                    selectedPlayableCharactersSet.ForEach((pc) =>
                    {
                        pc.characterMovement.MoveToScreenPoint(Input.mousePosition);
                    });
                }
            }
        }
    }
}
