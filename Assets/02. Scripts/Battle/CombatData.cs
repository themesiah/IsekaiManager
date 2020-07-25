using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.Battle
{
    [CreateAssetMenu(menuName = "Isekai/Data/Combat data")]
    public class CombatData : ScriptableObject
    {
        [SerializeField]
        private int damage = default;
        public int Damage { get { return damage; } }
    }
}