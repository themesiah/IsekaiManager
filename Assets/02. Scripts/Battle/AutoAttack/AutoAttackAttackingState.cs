using GamedevsToolbox.StateMachine;
using UnityEngine;
using UnityEngine.Events;
using Isekai.Characters;
using System.Collections;

namespace Isekai.Battle
{
    public class AutoAttackAttackingState : ICoroutineState
    {
        private Animator animator;
        private Transform defendingTransform;
        private Transform attackerTransform;
        private IDamageable attackReceiver;
        private UnityAction onFinish;

        public AutoAttackAttackingState(Animator anim, Transform defending, IDamageable receiver, Transform attacker, UnityAction action)
        {
            animator = anim;
            defendingTransform = defending;
            attackerTransform = attacker;
            attackReceiver = receiver;
            onFinish = action;
        }

        public IEnumerator EnterState()
        {
            animator.SetBool("attacking", true);
            return null;
        }

        public IEnumerator ExitState()
        {
            animator.SetBool("attacking", false);
            return null;
        }

        public void ReceiveSignal(string signal)
        {
        }

        private bool InRange()
        {
            // TODO
            return true;
        }
        
        public IEnumerator Update(System.Action<string> action)
        {
            if (!attackReceiver.IsAlive())
            {
                onFinish();
            } else if (!InRange())
            {
                action("follow");
            } else
            {
                attackerTransform.LookAt(defendingTransform, Vector3.up);
            }
            yield return null;
        }
    }
}