using UnityEngine;
using UnityEngine.Events;
using Isekai.Characters;
using GamedevsToolbox.CommandPattern;

namespace Isekai.Interactions
{
    public abstract class InteractionCommand : Command
    {
        protected PlayableCharacter interactionCharacter;

        public virtual void SetInteractionCharacter(PlayableCharacter pc)
        {
            interactionCharacter = pc;
        }
    }
}