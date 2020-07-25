using UnityEngine;

namespace Isekai.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterData characterData = default;
        public Transform portraitPivot = default;
        public CharacterTypeEnum characterType { get; private set; }
        public RTSCommandProcessor commandProcessor { get; private set; }
        public CharacterMovement characterMovement { get; private set; }
        public NavMeshAgentWrapper agentWrapper { get; private set; }
        public Animator animator { get; private set; }
        public CharacterHealth characterHealth { get; private set; }
        public CharacterSelection characterSelection { get; private set; }
        public AttackCalculationBehaviour attackCalculation { get; private set; }

        protected virtual void Awake()
        {
            commandProcessor = GetComponent<RTSCommandProcessor>();
            characterMovement = GetComponent<CharacterMovement>();
            agentWrapper = GetComponent<NavMeshAgentWrapper>();
            animator = GetComponentInChildren<Animator>();
            characterHealth = GetComponent<CharacterHealth>();
            characterSelection = GetComponent<CharacterSelection>();
            attackCalculation = GetComponent<AttackCalculationBehaviour>();
            characterType = characterData.CharacterType;
        }

        protected virtual void Start()
        {
            attackCalculation?.InitializeCharacter(characterData);
            characterHealth?.InitializeCharacter(characterData);
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }
    }
}