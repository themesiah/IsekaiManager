using UnityEngine;
using UnityEngine.Events;
using GamedevsToolbox.ScriptableArchitecture.Values;
using System;

namespace Isekai.Characters
{
    public class CharacterHealth : MonoBehaviour, IDamageable, ICharacterInitializable
    {
        [SerializeField]
        private UnityEvent onDeathEvent = default;

        private int maxHealth;
        private int currentHealth;
        private bool destroyed = false;
        private UnityAction<int> onHealthChanged = delegate { };

        public void Damage(AttackData damage)
        {
            currentHealth -= damage.physicalDamage;

            if (damage.physicalDamage > 0)
            {
                onHealthChanged(currentHealth);
            }

            if (currentHealth <= 0)
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
            return maxHealth;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public void InitializeCharacter(CharacterData cd)
        {
            maxHealth = cd.MaxHealth.GetValue();
            currentHealth = maxHealth;
        }
    }
}