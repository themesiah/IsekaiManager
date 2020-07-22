using UnityEngine;

namespace Isekai.Characters
{
    public class Character : MonoBehaviour
    {
        

        public CharacterTypeEnum characterType = default;
        public Transform portraitPivot = default;
        [HideInInspector] public RTSCommandProcessor commandProcessor { get; private set; }
        [HideInInspector] public CharacterMovement characterMovement { get; private set; }
        [HideInInspector] public NavMeshAgentWrapper agentWrapper { get; private set; }
        [HideInInspector] public Animator animator { get; private set; }
        [HideInInspector] public IDamageable damageable { get; private set; }
        [HideInInspector] public CharacterSelection characterSelection { get; private set; }
        [HideInInspector] public AttackCalculationBehaviour attackCalculation { get; private set; }

        protected virtual void Awake()
        {
            commandProcessor = GetComponent<RTSCommandProcessor>();
            characterMovement = GetComponent<CharacterMovement>();
            agentWrapper = GetComponent<NavMeshAgentWrapper>();
            animator = GetComponentInChildren<Animator>();
            damageable = GetComponent<IDamageable>();
            characterSelection = GetComponent<CharacterSelection>();
            attackCalculation = GetComponent<AttackCalculationBehaviour>();
        }
    }
}