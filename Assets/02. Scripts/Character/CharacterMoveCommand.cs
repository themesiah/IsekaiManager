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
        private NavMeshAgentWrapper agent = default;
        private Coroutine moveCoroutine = null;
        private GameObject positionMarker = null;
        private Animator animator;

        private Vector3 lastVelocity = Vector3.zero;

        public CharacterMoveCommand(Vector3 position, GameObject positionPrefab)
        {
            targetPosition = position;
            positionMarker = GameObject.Instantiate(positionPrefab, position + Vector3.up * 0.5f, Quaternion.identity);
            if (Physics.Raycast(positionMarker.transform.position, Vector3.down, out RaycastHit hitInfo, 1f))
            {
                positionMarker.transform.position = hitInfo.point + Vector3.up * 0.2f;
            }
        }

        public override void SetInteractionCharacter(Character c)
        {
            base.SetInteractionCharacter(c);
            agent = c.agentWrapper;
        }

        public override void Execute(UnityAction onFinishAction)
        {
            animator = interactionCharacter.GetComponentInChildren<Animator>();
            onFinishAction += RemovePositionMarker;
            agent.MoveTo(targetPosition, onFinishAction);
        }

        public override void Undo(UnityAction onFinishAction)
        {
        }

        public override void Cancel()
        {
            if (moveCoroutine != null)
            {
                interactionCharacter.StopCoroutine(moveCoroutine);
                agent.CancelMovement();
            }
            else
            {
                animator?.SetFloat("movementSpeed", 0f);
            }
            RemovePositionMarker();
        }
        
        private void RemovePositionMarker()
        {
            if (positionMarker != null)
            {
                GameObject.Destroy(positionMarker);
            }
        }

        public override void Pause()
        {
            base.Pause();
            agent.Pause();
        }

        public override void Resume()
        {
            base.Pause();
            agent.Resume();
        }
    }
}