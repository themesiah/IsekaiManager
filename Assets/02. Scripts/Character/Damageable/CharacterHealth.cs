using UnityEngine;
using UnityEngine.Events;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.Characters
{
    public class CharacterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private ScriptableIntReference maxHealthReference = default;
        [SerializeField]
        private UnityEvent onDeathEvent = default;

        private int currentHealth = 1;
        private bool destroyed = false;

        private void Awake()
        {
            currentHealth = maxHealthReference.GetValue();
        }

        public void Damage(AttackData damage)
        {
            currentHealth -= damage.physicalDamage;
            if (currentHealth < 0)
            {
                OnDestroyed();
            }
        }

        public void OnDestroyed()
        {
            destroyed = true;
            onDeathEvent.Invoke();
        }

        public bool IsAlive()
        {
            return !destroyed;
        }
    }
}