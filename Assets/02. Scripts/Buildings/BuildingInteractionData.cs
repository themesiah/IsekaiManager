using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.Buildings
{
    [CreateAssetMenu(fileName = "New Building Interaction", menuName = "Isekai/Data/Building Interaction")]
    public class BuildingInteractionData : ScriptableObject
    {
        [SerializeField]
        private string interactionName = default;
        public string InteractionName { get { return interactionName; } }

        [SerializeField]
        private Resources.ResourceData resourceData = default;
        public Resources.ResourceData ResourceData { get { return resourceData; } }
        
        [SerializeField]
        private float duration = default;
        public float Duration { get { return duration; } }

        [SerializeField]
        private int reward = default;
        public int Reward { get { return reward; } }

        // TODO: WHICH STAT USES
        //[SerializeField]
        //private float duration = default;
        //public float Duration { get { return duration; } }

        // TODO: STAT EFFECT ON DURATION
        //[SerializeField]
        //private float duration = default;
        //public float Duration { get { return duration; } }

        // TODO: STAT EFFECT ON REWARD
        //[SerializeField]
        //private float duration = default;
        //public float Duration { get { return duration; } }
    }
}