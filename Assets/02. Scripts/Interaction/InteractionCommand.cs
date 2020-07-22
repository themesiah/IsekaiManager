using Isekai.Characters;
using GamedevsToolbox.CommandPattern;
using System.Collections;

namespace Isekai.Interactions
{
    public abstract class InteractionCommand : Command
    {
        protected Character interactionCharacter;
        protected bool paused = false;

        public virtual void SetInteractionCharacter(Character c)
        {
            interactionCharacter = c;
        }

        protected IEnumerator ManagePause()
        {
            while (paused)
            {
                yield return null;
            }
        }

        public override void Pause()
        {
            base.Pause();
            paused = true;
        }

        public override void Resume()
        {
            base.Resume();
            paused = false;
        }
    }
}