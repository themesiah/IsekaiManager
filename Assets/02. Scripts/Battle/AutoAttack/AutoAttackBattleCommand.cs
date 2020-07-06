using Isekai.Interactions;
using System.Collections;
using Isekai.Characters;
using UnityEngine.Events;
using UnityEngine;

namespace Isekai.Battle
{
    public class AutoAttackBattleCommand : AttackBattleCommand
    {
        private AutoAttackStateMachine stateMachine;
        

        private Coroutine coroutine;

        public override void Cancel()
        {
            interactionCharacter.StartCoroutine(stateMachine.StopAutoAttack());
            //stateMachine.StopAutoAttack();
            if (coroutine != null)
            {
                interactionCharacter.StopCoroutine(coroutine);
            }
        }

        public AutoAttackBattleCommand(Transform enemyTransform, IDamageable damageReceiver) : base(enemyTransform, damageReceiver)
        {
            
        }

        public override void Execute(UnityAction onFinishAction)
        {
            onFinishAction += Cancel;
            stateMachine = new AutoAttackStateMachine(interactionCharacter.agentWrapper, interactionCharacter.animator, enemyTransform, damageReceiver, interactionCharacter.attackCalculation, onFinishAction);
            coroutine = interactionCharacter.StartCoroutine(StateMachineUpdate());
        }

        private IEnumerator StateMachineUpdate()
        {
            while(true)
            {
                yield return stateMachine.Update(null);
            }
        }

        public override void Undo(UnityAction onFinishAction){}
    }
}