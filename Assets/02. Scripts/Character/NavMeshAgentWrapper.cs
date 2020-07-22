using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Isekai.Characters
{
    [RequireComponent(typeof(NavMeshAgent), typeof(NavMeshObstacle))]
    public class NavMeshAgentWrapper : MonoBehaviour, GamedevsToolbox.Utils.IPausable
    {
        [SerializeField]
        private NavMeshAgent agent = default;
        [SerializeField]
        private NavMeshObstacle obstacle = default;
        [SerializeField]
        private Animator animator = default;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Values.ScriptableFloatReference defaultStoppingDistance = default;

        private UnityAction onArrived;
        private bool moving = false;
        private bool following = false;
        private Vector3 lastVelocity = Vector3.zero;
        private Vector3 lastFollowPosition = Vector3.zero;
        private Transform followTransform = null;

        private void Awake()
        {
            agent.enabled = false;
            obstacle.enabled = true;
            animator.enabled = true;
        }

        public void MoveTo(Vector3 position, UnityAction arriveAction)
        {
            onArrived = arriveAction;
            agent.stoppingDistance = defaultStoppingDistance.GetValue();
            following = false;
            MoveToInternal(position);
        }

        private void MoveToInternal(Vector3 position)
        {
            obstacle.enabled = false;
            agent.enabled = true;
            moving = true;
            agent.SetDestination(position);
        }

        public void Follow(Transform t)
        {
            Follow(t, null);
        }

        public void Follow(Transform t, UnityAction arriveAction)
        {
            followTransform = t;
            following = true;
            lastFollowPosition = followTransform.position;
            onArrived = arriveAction;
            NavMeshAgent followAgent = followTransform.GetComponent<NavMeshAgent>();
            if (followAgent != null)
            {
                agent.stoppingDistance = followAgent.radius + defaultStoppingDistance.GetValue();
            }
            MoveToInternal(lastFollowPosition);
        }

        public void CancelMovement()
        {
            animator?.SetFloat("movementSpeed", 0f);
            agent.isStopped = true;
            agent.enabled = false;
            obstacle.enabled = true;
            moving = false;
        }

        public void StopFollow()
        {
            if (moving == true)
            {
                CancelMovement();
                following = false;
            }
        }

        private void Update()
        {
            if (moving == true)
            {
                animator?.SetFloat("movementSpeed", agent.velocity.magnitude);

                if (following)
                {
                    if (followTransform.position != lastFollowPosition)
                    {
                        lastFollowPosition = followTransform.position;
                        MoveTo(lastFollowPosition, onArrived);
                    }
                }

                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            CancelMovement();
                            onArrived?.Invoke();
                        }
                    }
                }
            }
        }

        public void Pause()
        {
            agent.isStopped = true;
            lastVelocity = agent.velocity;
            agent.velocity = Vector3.zero;
        }

        public void Resume()
        {
            agent.isStopped = false;
            agent.velocity = lastVelocity;
        }
    }
}