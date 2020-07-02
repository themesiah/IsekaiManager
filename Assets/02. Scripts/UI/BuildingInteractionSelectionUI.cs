using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using Isekai.Interactions;

namespace Isekai.UI
{
    public class BuildingInteractionSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject interactionCanvas = default;
        [SerializeField]
        private GameObject[] resourceObjects = default;
        [SerializeField]
        private Image[] resourceImages = default;
        [SerializeField]
        private Button[] resourceButtons = default;

        private void Awake()
        {
            Close();
        }

        public void Init(BuildingInteractionDataTemplate[] availableInteractions, List<UnityAction> interactionActions)
        {
            foreach (GameObject go in resourceObjects)
            {
                go.SetActive(false);
            }
            interactionCanvas.SetActive(true);

            for (int i = 0; i < availableInteractions.Length; ++i)
            {
                resourceObjects[i].SetActive(true);
                resourceImages[i].sprite = availableInteractions[i].GetInteractionSprite();
                resourceButtons[i].onClick.RemoveAllListeners();
                resourceButtons[i].onClick.AddListener(interactionActions[i]);
                resourceButtons[i].onClick.AddListener(Close);
            }
        }

        public void Close()
        {
            interactionCanvas.SetActive(false);
        }
    }
}