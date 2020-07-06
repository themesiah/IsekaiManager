using UnityEngine;

namespace Isekai.Characters
{
    public class PlayableCharacter : Character
    {
        public CharacterSelection characterSelection { get; private set; }
        public RTSCommandProcessor commandProcessor { get; private set; }
        public CharacterMovement characterMovement { get; private set; }
        public NavMeshAgentWrapper agentWrapper { get; private set; }
        public Animator animator { get; private set; }
        public AttackCalculationBehaviour attackCalculation { get; private set; }

        private void Awake()
        {
            characterSelection = GetComponent<CharacterSelection>();
            commandProcessor = GetComponent<RTSCommandProcessor>();
            characterMovement = GetComponent<CharacterMovement>();
            agentWrapper = GetComponent<NavMeshAgentWrapper>();
            animator = GetComponentInChildren<Animator>();
            attackCalculation = GetComponent<AttackCalculationBehaviour>();
        }
    }
}
