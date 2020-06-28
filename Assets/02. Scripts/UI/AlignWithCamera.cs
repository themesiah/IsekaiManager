using GamedevsToolbox.ScriptableArchitecture.Sets;
using UnityEngine;

namespace Isekai.UI
{
    public class AlignWithCamera : MonoBehaviour
    {
        [SerializeField]
        private RuntimeSingleCamera cameraRef = default;

        private void Update()
        {
            transform.eulerAngles = cameraRef.Get().transform.eulerAngles;
        }
    }
}