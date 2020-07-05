using UnityEngine;
using UnityEngine.Events;

namespace Isekai.Characters
{
    public class AttackDoneReceiver : MonoBehaviour
    {
        [System.Serializable]
        public class UnityEventFloat : UnityEvent<float>{};
        [SerializeField]
        private UnityEventFloat onAttackDoneReceiver = default;

        public void OnAttackDone(float multiplier)
        {
            onAttackDoneReceiver.Invoke(multiplier);
        }
    }
}