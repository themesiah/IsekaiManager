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
            attackerAgentWrapper.StartCoroutine(stateMachine.StopAutoAttack());
            //stateMachine.StopAutoAttack();
            if (coroutine != null)
            {
                attackerAgentWrapper.StopCoroutine(coroutine);
            }
        }

        public AutoAttackBattleCommand(Transform enemyTransform, IDamageable damageReceiver) : base(enemyTransform, damageReceiver)
        {
            
        }

        public override void Execute(UnityAction onFinishAction)
        {
            onFinishAction += Cancel;
            stateMachine = new AutoAttackStateMachine(attackerAgentWrapper, attackerAnimator, enemyTransform, damageReceiver, onFinishAction);
            coroutine = attackerAgentWrapper.StartCoroutine(StateMachineUpdate());
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