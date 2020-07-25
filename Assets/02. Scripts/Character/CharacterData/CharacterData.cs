using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;
using GamedevsToolbox.ScriptableArchitecture.Localization;
using Isekai.Battle;

namespace Isekai.Characters
{
    [CreateAssetMenu(menuName = "Isekai/Data/Character data")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private ScriptableTextRef characterName = default;
        public ScriptableTextRef CharacterName { get { return characterName; } }

        [SerializeField]
        private CharacterTypeEnum characterType = default;
        public CharacterTypeEnum CharacterType { get { return characterType; } }

        [SerializeField]
        private ScriptableIntReference maxHealth = default;
        public ScriptableIntReference MaxHealth { get { return maxHealth; } }

        [SerializeField]
        private CombatData combatData = default;
        public CombatData CombatData { get { return combatData; } }
    }
}