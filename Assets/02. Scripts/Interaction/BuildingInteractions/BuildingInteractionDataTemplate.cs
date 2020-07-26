using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Interactions
{
    public abstract class BuildingInteractionDataTemplate : ScriptableObject
    {
        [SerializeField]
        private string interactionName = default;
        public string InteractionName { get { return interactionName; } }
        
        [SerializeField]
        private List<Isekai.Characters.CharacterTypeEnum> availableForCharacterTypes = default;
        public List<Isekai.Characters.CharacterTypeEnum> AvailableForCharacterTypes { get { return availableForCharacterTypes; } }

        public abstract Sprite GetInteractionSprite();
    }
}