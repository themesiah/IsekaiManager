using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class OnHoverInteraction : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onStartHoverEvent = default;
        [SerializeField]
        private UnityEvent onEndHoverEvent = default;

        private void OnMouseEnter()
        {
            onStartHoverEvent.Invoke();
        }

        private void OnMouseExit()
        {
            onEndHoverEvent.Invoke();
        }
    }
}
