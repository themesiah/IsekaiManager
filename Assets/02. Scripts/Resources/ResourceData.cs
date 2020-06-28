using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace Isekai.Resources
{
    [CreateAssetMenu(fileName = "New Resource Data", menuName = "Isekai/Data/Resource")]
    public class ResourceData : ScriptableObject
    {
        [SerializeField]
        private ScriptableIntReference resourceRef = default;
        public ScriptableIntReference ResourceRef { get { return resourceRef; } }

        [SerializeField]
        private Sprite resourceIcon = default;
        public Sprite ResourceIcon { get { return resourceIcon; } }
    }
}
