using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Characters
{
    [System.Serializable]
    public class CombatData // TODO: Scriptable object and things like that
    {
        public int damage;
    }

    [System.Serializable]
    public struct AttackData // TODO: probably more fields
    {
        public int physicalDamage;
    }

    public class AttackCalculationBehaviour : MonoBehaviour
    {
        [SerializeField]
        private CombatData combatDataRef = default;

        private UnityAction<AttackData> onAttack;

        public void OnAttackPerformed(float motionValue)
        {
            AttackData ad = new AttackData();
            ad.physicalDamage = (int) (combatDataRef.damage * motionValue);
            onAttack?.Invoke(ad);
        }

        public void RegisterOnAttackTarget(UnityAction<AttackData> action)
        {
            onAttack = action;
        }

        public void UnregisterOnAttackTarget()
        {
            onAttack = null;
        }
    }
}