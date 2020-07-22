using UnityEngine;
using Isekai.Characters;
using UnityEngine.UI;

namespace Isekai.UI
{
    public class CharacterHealthUI : MonoBehaviour
    {
        [SerializeField]
        private CharacterHealth characterHealth;

        [SerializeField]
        private Slider healthSlider;

        private void OnEnable()
        {
            characterHealth.RegisterOnHealthChanged(OnHealthChanged);
        }

        private void OnDisable()
        {
            characterHealth.UnregisterOnHealthChanged(OnHealthChanged);
        }

        private void OnHealthChanged(int current)
        {
            float percent = (float)current / (float)characterHealth.GetMaxHealth();
            healthSlider.value = percent;
        }
    }
}