using UnityEngine;

namespace Isekai.Characters
{
    public class PlayableCharacter : Character
    {
        public CharacterSelection characterSelection { get; private set; }
        public RTSCommandProcessor commandProcessor { get; private set; }
        public CharacterMovement characterMovement { get; private set; }

        private void Awake()
        {
            characterSelection = GetComponent<CharacterSelection>();
            commandProcessor = GetComponent<RTSCommandProcessor>();
            characterMovement = GetComponent<CharacterMovement>();
        }
    }
}
