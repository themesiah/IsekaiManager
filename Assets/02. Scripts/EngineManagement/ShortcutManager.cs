using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Managers
{
    public class ShortcutManager : MonoBehaviour
    {
        [SerializeField]
        private KeyCode keyCode = KeyCode.Space;
        [SerializeField]
        private UnityEvent onKeyPressed = default;

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                onKeyPressed.Invoke();
            }
        }
    }
}