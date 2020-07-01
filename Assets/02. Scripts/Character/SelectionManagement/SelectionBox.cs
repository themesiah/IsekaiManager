using UnityEngine;
using UnityEngine.UI;

namespace Isekai
{
    public class SelectionBox : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform = default;

        public struct SelectionBoxData
        {
            public bool active;
            public Vector2 startPosition;
            public Vector2 size; 
        }

        public void ReceiveSelectionBoxData(object data)
        {
            SelectionBoxData sbd = (SelectionBoxData)data;

            if (sbd.active == true)
            {
                rectTransform.gameObject.SetActive(true);
                rectTransform.anchoredPosition = sbd.startPosition;
                rectTransform.sizeDelta = sbd.size;
            } else
            {
                rectTransform.gameObject.SetActive(false);
            }
        }
    }
}