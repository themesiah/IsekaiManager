using UnityEngine;
using Isekai.Characters;
using UnityEngine.UI;

namespace Isekai.UI
{
    public class CharacterHealthUI : MonoBehaviour
    {
        [SerializeField]
        private CharacterHealth characterHealth = default;

        [SerializeField]
        private Slider healthSlider = default;

        private void OnEnable()
        {
            characterHealth?.RegisterOnHealthChanged(OnHealthChanged);
        }

        private void OnDisable()
        {
            characterHealth?.UnregisterOnHealthChanged(OnHealthChanged);
        }

        private void OnHealthChanged(int current)
        {
            float percent = (float)current / (float)characterHealth.GetMaxHealth();
            healthSlider.value = percent;
        }

        public void AssignCharacter(object character)
        {
            characterHealth = ((Character)character).characterHealth;
            if (characterHealth != null)
            {
                characterHealth.RegisterOnHealthChanged(OnHealthChanged);
                OnHealthChanged(characterHealth.GetCurrentHealth());
            }
        }
    }
}