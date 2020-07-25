using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Localization;
using UnityEngine.UI;

namespace Isekai.UI
{
    public class SimpleLocalizedText : MonoBehaviour
    {
        [SerializeField]
        private ScriptableTextRef localizationRef = default;
        [SerializeField]
        private Text textRef = default;

        private void Start()
        {
            if (textRef != null && localizationRef != null)
                textRef.text = localizationRef.GetText();
        }
    }
}