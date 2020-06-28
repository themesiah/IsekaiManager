using UnityEngine;
using UnityEngine.Events;

namespace Isekai.General
{
    public class IPausableBehaviour : MonoBehaviour, GamedevsToolbox.Utils.IPausable
    {
        [SerializeField]
        private UnityEvent onPause = default;
        [SerializeField]
        private UnityEvent onResume = default;

        private int stacks = 0;

        public void Pause()
        {
            if (stacks == 0)
            {
                onPause.Invoke();
            }
            stacks++;
        }

        public void Resume()
        {
            stacks--;
            if (stacks == 0)
            {
                onResume.Invoke();
            }
        }
    }
}