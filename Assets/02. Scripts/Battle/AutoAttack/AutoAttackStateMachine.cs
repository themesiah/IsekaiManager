using GamedevsToolbox.StateMachine;
using Isekai.Characters;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Collections;

namespace Isekai.Battle
{
    public class AutoAttackStateMachine : CoroutineStateMachine
    {
        public AutoAttackStateMachine(NavMeshAgentWrapper agentWrapper, Animator anim, Transform followTransform, IDamageable receiver, UnityAction commandFinished)
        {
            Dictionary<string, ICoroutineState> states = new Dictionary<string, ICoroutineState>();
            states.Add("follow", new AutoAttackFollowState(agentWrapper, followTransform, receiver, commandFinished));
            states.Add("attack", new AutoAttackAttackingState(anim, followTransform, receiver, agentWrapper.transform, commandFinished));
            SetStates(states);
        }

        public override void ReceiveSignal(string signal)
        {
            base.ReceiveSignal(signal);
            currentState.ReceiveSignal(signal);
        }

        public IEnumerator StopAutoAttack()
        {
            yield return currentState.ExitState();
        }
    }
}