using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Isekai.Interactions;

namespace Isekai.Characters
{
    public class CharacterMoveCommand : InteractionCommand
    {
        private Vector3 targetPosition = default;
        private NavMeshAgent agent = default;
        private Coroutine moveCoroutine = null;

        public CharacterMoveCommand(Vector3 position)
        {
            targetPosition = position;
        }

        public override void SetInteractionCharacter(PlayableCharacter pc)
        {
            base.SetInteractionCharacter(pc);
            agent = pc.GetComponent<NavMeshAgent>();
        }

        public override void Execute(UnityAction onFinishAction)
        {
            moveCoroutine = interactionCharacter.StartCoroutine(MoveTo(onFinishAction));
        }

        public override void Undo(UnityAction onFinishAction)
        {
        }

        public override void Cancel()
        {
            if (moveCoroutine != null)
            {
                interactionCharacter.StopCoroutine(moveCoroutine);
            }
        }

        private IEnumerator MoveTo(UnityAction onFinishAction)
        {
            agent.enabled = true;
            agent.SetDestination(targetPosition);
            while (!AgentArrived())
            {
                yield return null;
            }
            agent.enabled = false;
            onFinishAction();
        }

        private bool AgentArrived()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}