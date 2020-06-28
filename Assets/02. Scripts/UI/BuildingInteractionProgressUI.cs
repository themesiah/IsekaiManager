using UnityEngine;
using UnityEngine.UI;
using Isekai.Resources;

namespace Isekai.UI
{
    public class BuildingInteractionProgressUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject progressCanvas = default;
        [SerializeField]
        private Image resourceSprite = default;
        [SerializeField]
        private Image blackAndWhiteResourceSprite = default;

        private void Awake()
        {
            progressCanvas.SetActive(false);
        }

        public void InitInteraction(ResourceData data)
        {
            progressCanvas.SetActive(true);
            resourceSprite.fillAmount = 0f;
            resourceSprite.sprite = data.ResourceIcon;
            blackAndWhiteResourceSprite.sprite = data.ResourceIcon;
        }

        public void SetProgress(float value)
        {
            resourceSprite.fillAmount = value;
        }

        public void Finish()
        {
            resourceSprite.fillAmount = 0f;
            progressCanvas.SetActive(false);
        }
    }
}