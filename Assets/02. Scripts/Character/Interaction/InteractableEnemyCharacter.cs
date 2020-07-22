using UnityEngine;
using Isekai.Interactions;
using Isekai.Battle;
using UnityEngine.Events;
using GamedevsToolbox.ScriptableArchitecture.Events;

namespace Isekai.Characters.Interactions
{
    public class InteractableEnemyCharacter : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GenericGameEvent hoverStartEvent = default;
        [SerializeField]
        private GenericGameEvent hoverEndEvent = default;
        [SerializeField]
        private CharacterHealth health = default;

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
            AutoAttackBattleCommand command = new AutoAttackBattleCommand(transform, health);
            dispatchAction(new InteractionCommand[] { command });
        }

        public InteractionType GetInteractionType()
        {
            return InteractionType.Enemy;
        }
    }
}