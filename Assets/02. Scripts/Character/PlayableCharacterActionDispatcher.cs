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
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Events.GameEvent interactionRequestedEvent = default;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                interactionRequestedEvent?.Raise();
                if (currentInteractableObjectsSet.Items.Count > 0)
                {
                    selectedPlayableCharactersSet.ForEach((pc) =>
                    {
                        switch (currentInteractableObjectsSet.Items[0].GetInteractionType())
                        {
                            case InteractionType.Building:
                                currentInteractableObjectsSet.Items[0].DispatchCommand((interactionCommands) =>
                                {
                                    bool cancelAll = true;
                                    foreach (InteractionCommand ic in interactionCommands)
                                    {
                                        ic.SetInteractionCharacter(pc);
                                        pc.commandProcessor.ProcessCommand(ic, cancelAll);
                                        cancelAll = false;
                                    }
                                });
                                break;
                            case InteractionType.Enemy:
                                currentInteractableObjectsSet.Items[0].DispatchCommand((interactionCommands) =>
                                {
                                    bool cancelAll = true;
                                    foreach (InteractionCommand ic in interactionCommands)
                                    {
                                        AttackBattleCommand attackCommand = (AttackBattleCommand)ic;
                                        ic.SetInteractionCharacter(pc);
                                        attackCommand.SetAttackerData(pc.agentWrapper, pc.animator, null);
                                        pc.commandProcessor.ProcessCommand(ic, cancelAll);
                                        cancelAll = false;
                                    }
                                });
                                break;
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
