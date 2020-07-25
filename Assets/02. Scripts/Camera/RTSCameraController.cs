using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.Camera
{
    public class RTSCameraController : MonoBehaviour
    {
        [Header("Movement parameters")]
        [SerializeField]
        private ScriptableFloatReference cameraSpeed = default;
        [SerializeField]
        private ScriptableFloatReference cameraSpeedFast = default;
        [SerializeField] [Tooltip("From 0 to 1, using screen size as a reference")]
        private ScriptableVector2Reference bordersMovementLimit = default;
        [Header("Zoom parameters")]
        [SerializeField]
        private ScriptableVector2Reference zoomLimits = default;
        [SerializeField]
        private ScriptableVector2Reference cameraRotationZoomLimits = default;
        [SerializeField]
        private ScriptableFloatReference zoomSpeed = default;
        [SerializeField]
        private ScriptableFloatReference zoomReference = default;
        [Header("Constraints")]
        [SerializeField]
        private ScriptableBoolReference allowKeysMovement = default;
        [SerializeField]
        private ScriptableBoolReference allowMouseMovement = default;
        [SerializeField]
        private ScriptableBoolReference allowZoom = default;

        private void OnEnable()
        {
            zoomReference.RegisterOnChangeAction(OnZoomChanged);
        }

        private void OnDisable()
        {
            zoomReference.UnregisterOnChangeAction(OnZoomChanged);
        }

        private void Update()
        {
            DoUpdate();
        }

        private void DoUpdate()
        {
            float x = 0f;
            float y = 0f;
            GetKeysMovementValue(ref x, ref y);
            GetMouseMovementValue(ref x, ref y);
            UpdateMovement(x, y);
            UpdateZoom();
        }

        private void GetMouseMovementValue(ref float x, ref float y)
        {
            if (allowMouseMovement.GetValue())
            {
                Vector2 mouse = Input.mousePosition;
                if (mouse.x < (bordersMovementLimit.GetValue().x * Screen.width) && mouse.x > 0)
                {
                    x -= 1f;
                }
                if (mouse.x > Screen.width - (bordersMovementLimit.GetValue().x * Screen.width) && mouse.x < Screen.width)
                {
                    x += 1f;
                }
                if (mouse.y < (bordersMovementLimit.GetValue().y * Screen.height) && mouse.y > 0)
                {
                    y -= 1f;
                }
                if (mouse.y > Screen.height - (bordersMovementLimit.GetValue().y * Screen.height) && mouse.y < Screen.height)
                {
                    y += 1f;
                }
            }
        }

        private void GetKeysMovementValue(ref float x, ref float y)
        {
            if (allowKeysMovement.GetValue())
            {
                x += Input.GetAxis("Horizontal");
                y += Input.GetAxis("Vertical");
            }
        }

        private void UpdateMovement(float x, float y)
        {
            if (x != 0f || y != 0f)
            {
                Vector3 euler = transform.eulerAngles;
                float lastX = euler.x;
                euler.x = 0f;
                transform.eulerAngles = euler;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    transform.Translate(new Vector3(x, 0f, y) * Time.deltaTime * cameraSpeedFast.GetValue());
                }
                else
                {
                    transform.Translate(new Vector3(x, 0f, y) * Time.deltaTime * cameraSpeed.GetValue());
                }
                euler.x = lastX;
                transform.eulerAngles = euler;
            }
        }

        private void UpdateZoom()
        {
            if (allowZoom.GetValue())
            {
                float zoom = Input.GetAxis("Mouse ScrollWheel");

                zoomReference.SetValue(Mathf.Clamp01(zoomReference.GetValue() + zoom * Time.deltaTime * zoomSpeed.GetValue()));

                Vector3 euler = transform.eulerAngles;
                euler.x = Mathf.Lerp(cameraRotationZoomLimits.GetValue().y, cameraRotationZoomLimits.GetValue().x, zoomReference.GetValue());
                transform.eulerAngles = euler;
                Vector3 position = transform.position;
                position.y = Mathf.Lerp(zoomLimits.GetValue().y, zoomLimits.GetValue().x, zoomReference.GetValue());
                transform.position = position;
            }
        }

        private void OnZoomChanged(float current)
        {
            DoUpdate();
        }
    }
}