using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class OnHoverInteraction : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onStartHoverEvent;
        [SerializeField]
        private UnityEvent onEndHoverEvent;

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
