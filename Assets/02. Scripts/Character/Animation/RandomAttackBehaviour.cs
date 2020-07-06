using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Characters
{
    public class RandomAttackBehaviour : StateMachineBehaviour
    {
        [SerializeField]
        private int animationCount = 3;
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
            animator.SetInteger("attackAnimation", Random.Range(0, animationCount));
        }
    }
}