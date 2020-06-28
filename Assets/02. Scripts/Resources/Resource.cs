using UnityEngine;
using UnityEngine.UI;

namespace Isekai.Resources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField]
        private ResourceData resourceData = default;
        [SerializeField]
        private Image imageReference = default;
        [SerializeField]
        private Text quantityReference = default;

        private void OnEnable()
        {
            resourceData?.ResourceRef.RegisterOnChangeAction(OnResourceValueChanged);
        }

        private void OnDisable()
        {
            resourceData?.ResourceRef.UnregisterOnChangeAction(OnResourceValueChanged);
        }

        private void Awake()
        {
            imageReference.sprite = resourceData.ResourceIcon;
            quantityReference.text = resourceData?.ResourceRef.GetValue().ToString();
        }

        private void OnResourceValueChanged(int newValue)
        {
            quantityReference.text = newValue.ToString();
        }
    }
}