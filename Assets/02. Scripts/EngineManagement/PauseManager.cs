using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Managers
{
    [CreateAssetMenu(menuName = "Isekai/Managers/Pause Manager")]
    public class PauseManager : ScriptableObject
    {
        [SerializeField]
        private Isekai.General.RuntimePausableSet pausableSet = default;

        private bool paused = false;

        public bool IsPaused => paused;

        public void SwitchPauseState()
        {
            if (paused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        public void Pause()
        {
            paused = true;
            pausableSet?.ForEach(p => p.Pause());
        }

        public void Resume()
        {
            paused = false;
            pausableSet?.ForEach(p => p.Resume());
        }
    }
}