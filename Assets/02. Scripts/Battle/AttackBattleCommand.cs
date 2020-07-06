using UnityEngine;
using Isekai.Characters;

namespace Isekai.Interactions
{
    public class AttackData { } // Pre-declaration, TODO, scriptable object?
    public abstract class AttackBattleCommand : InteractionCommand
    {
        protected NavMeshAgentWrapper attackerAgentWrapper;
        protected Animator attackerAnimator;
        protected Transform enemyTransform;
        protected IDamageable damageReceiver;

        public AttackBattleCommand(Transform enemyTransform, IDamageable damageReceiver)
        {
            this.enemyTransform = enemyTransform;
            this.damageReceiver = damageReceiver;
        }

        public void SetAttackerData(NavMeshAgentWrapper attackerAgentWrapper, Animator attackerAnimator, AttackData ad)
        {
            this.attackerAgentWrapper = attackerAgentWrapper;
            this.attackerAnimator = attackerAnimator;
        }
    }
}