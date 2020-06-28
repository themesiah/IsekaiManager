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
        private GameObject positionMarker = null;
        private Animator animator;

        public CharacterMoveCommand(Vector3 position, GameObject positionPrefab)
        {
            targetPosition = position;
            positionMarker = GameObject.Instantiate(positionPrefab, position + Vector3.up * 0.5f, Quaternion.identity);
            if (Physics.Raycast(positionMarker.transform.position, Vector3.down, out RaycastHit hitInfo, 1f))
            {
                positionMarker.transform.position = hitInfo.point + Vector3.up * 0.2f;
            }
        }

        public override void SetInteractionCharacter(PlayableCharacter pc)
        {
            base.SetInteractionCharacter(pc);
            agent = pc.GetComponent<NavMeshAgent>();
        }

        public override void Execute(UnityAction onFinishAction)
        {
            animator = interactionCharacter.GetComponentInChildren<Animator>();
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
                agent.isStopped = true;
                agent.enabled = false;
            }
            animator?.SetFloat("movementSpeed", 0f);
            RemovePositionMarker();
        }

        private IEnumerator MoveTo(UnityAction onFinishAction)
        {
            agent.enabled = true;
            agent.SetDestination(targetPosition);
            while (!AgentArrived())
            {
                animator?.SetFloat("movementSpeed", agent.velocity.magnitude);
                yield return null;
            }
            agent.enabled = false;
            animator?.SetFloat("movementSpeed", 0f);
            RemovePositionMarker();
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

        private void RemovePositionMarker()
        {
            if (positionMarker != null)
            {
                GameObject.Destroy(positionMarker);
            }
        }
    }
}