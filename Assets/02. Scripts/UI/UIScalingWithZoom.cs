using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.UI
{
    public class UIScalingWithZoom : MonoBehaviour
    {
        [SerializeField]
        private Transform canvasTransform = default;

        [SerializeField]
        private ScriptableFloatReference zoomReference = default;

        [SerializeField]
        private ScriptableVector2Reference minMaxUIScaleReference = default;

        private Vector3 startingScale;

        private void Awake()
        {
            startingScale = canvasTransform.localScale;
            OnZoomChanged(zoomReference.GetValue());
        }

        private void OnEnable()
        {
            zoomReference.RegisterOnChangeAction(OnZoomChanged);
        }

        private void OnDisable()
        {
            zoomReference.UnregisterOnChangeAction(OnZoomChanged);
        }

        private void OnZoomChanged(float f)
        {
            canvasTransform.localScale = startingScale * Mathf.Lerp(minMaxUIScaleReference.GetValue().y, minMaxUIScaleReference.GetValue().x, f);
        }
    }
}