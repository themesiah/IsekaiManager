using Isekai.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Isekai.UI
{
    public class CharacterInfoPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject characterInfoPanel = default;
        [SerializeField]
        private Slider healthSlider = default;
        [SerializeField]
        private Text nameText = default;

        private Character characterReference;
        private bool currentlySelected = false;

        private void OnEnable()
        {
            if (currentlySelected)
            {
                characterReference.characterHealth.RegisterOnHealthChanged(UpdateHealth);
            }
        }

        private void OnDisable()
        {
            if (currentlySelected)
            {
                characterReference.characterHealth.UnregisterOnHealthChanged(UpdateHealth);
            }
        }

        public void OnCharacterSelected(object character)
        {
            currentlySelected = true;
            characterReference = (Character)character;
            characterInfoPanel.SetActive(true);
            characterReference.characterHealth.RegisterOnHealthChanged(UpdateHealth);
            UpdateUI();
        }

        public void OnCharacterUnselected()
        {
            currentlySelected = false;
            if (characterReference)
                characterReference.characterHealth.UnregisterOnHealthChanged(UpdateHealth);
            characterReference = null;
            characterInfoPanel.SetActive(false);
        }

        private void UpdateUI()
        {
            UpdateHealth(characterReference.characterHealth.GetCurrentHealth());
            UpdateName();
        }

        private void UpdateHealth(int currentHealth)
        {
            healthSlider.value = (float) currentHealth / characterReference.characterHealth.GetMaxHealth();
        }

        private void UpdateName()
        {
            nameText.text = characterReference.GetCharacterData().CharacterName.GetText();
        }
    }
}