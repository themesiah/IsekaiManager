using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;
using Isekai.General;

namespace Isekai.Managers
{
    [CreateAssetMenu(menuName = "Isekai/Managers/Pause Manager")]
    public class PauseManager : ScriptableObject
    {
        [SerializeField]
        private RuntimePausableSet pausableSet = default;
        [SerializeField]
        private ScriptableBoolReference paused = default;

        public bool IsPaused => paused.GetValue();

        public void SwitchPauseState()
        {
            if (paused.GetValue() == true)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        public void Pause()
        {
            paused.SetValue(true);
            pausableSet?.ForEach(p => p.Pause());
        }

        public void Resume()
        {
            paused.SetValue(false);
            pausableSet?.ForEach(p => p.Resume());
        }
    }
}