using UnityEngine;
using UnityEngine.Events;
using GamedevsToolbox.ScriptableArchitecture.Values;
using System;

namespace Isekai.Characters
{
    public class CharacterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private ScriptableIntReference maxHealthReference = default;
        [SerializeField]
        private UnityEvent onDeathEvent = default;

        private int currentHealth;
        private bool destroyed = false;
        private UnityAction<int> onHealthChanged = delegate { };

        private void Awake()
        {
            currentHealth = maxHealthReference.GetValue();
        }

        public void Damage(AttackData damage)
        {
            currentHealth -= damage.physicalDamage;

            if (damage.physicalDamage > 0)
            {
                onHealthChanged(currentHealth);
            }

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

        public void RegisterOnHealthChanged(UnityAction<int> action)
        {
            onHealthChanged += action;
        }

        public void UnregisterOnHealthChanged(UnityAction<int> action)
        {
            onHealthChanged -= action;
        }

        public int GetMaxHealth()
        {
            return maxHealthReference.GetValue();
        }
    }
}