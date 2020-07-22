using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Camera
{
    public class PortraitCameraManager : MonoBehaviour
    {
        [SerializeField]
        private float distanceToCharacter = 1f;
        [SerializeField]
        private UnityEngine.Camera portraitCamera = default;

        private Characters.Character followedCharacter;

        public void FollowCharacter(object character)
        {
            followedCharacter = (Characters.Character)character;
            portraitCamera.enabled = true;
        }

        public void StopFollow()
        {
            portraitCamera.enabled = false;
            followedCharacter = null;
        }

        private void Update()
        {
            if (followedCharacter != null)
            {
                transform.position = followedCharacter.portraitPivot.position + followedCharacter.transform.forward * distanceToCharacter;
                transform.LookAt(followedCharacter.portraitPivot);
            }
        }
    }
}