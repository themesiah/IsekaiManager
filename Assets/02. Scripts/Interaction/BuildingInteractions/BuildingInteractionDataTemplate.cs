using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Interactions
{
    public abstract class BuildingInteractionDataTemplate : ScriptableObject
    {
        [SerializeField]
        private string interactionName = default;
        public string InteractionName { get { return interactionName; } }

        public abstract Sprite GetInteractionSprite();
    }
}