using GamedevsToolbox.StateMachine;
using Isekai.Characters;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Isekai.Battle
{
    public class AutoAttackFollowState : ICoroutineState
    {
        private NavMeshAgentWrapper agentWrapper;
        private Transform followingTransform;
        private IDamageable enemyReceiver;
        private UnityAction onFinish;

        private bool arrived;

        public AutoAttackFollowState(NavMeshAgentWrapper wrapper, Transform follow, IDamageable receiver, UnityAction action)
        {
            agentWrapper = wrapper;
            followingTransform = follow;
            enemyReceiver = receiver;
            onFinish = action;
        }


        public IEnumerator EnterState()
        {
            arrived = false;
            agentWrapper.Follow(followingTransform, () => arrived = true);
            yield return null;
        }

        public IEnumerator ExitState()
        {
            agentWrapper.StopFollow();
            yield return null;
        }

        public void ReceiveSignal(string signal)
        {
        }
        
        public IEnumerator Update(System.Action<string> action)
        {
            if (!enemyReceiver.IsAlive())
            {
                onFinish();
            } else if (arrived == true)
            {
                action("attack");
            }
            yield return null;
        }
    }
}