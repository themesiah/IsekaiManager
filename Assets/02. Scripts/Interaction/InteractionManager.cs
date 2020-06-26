using UnityEngine;

namespace Isekai.Interactions
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField]
        private RuntimeInteractableSet interactableSet = default;

        [SerializeField]
        private bool activateLogger = false;

        private void Awake()
        {
            interactableSet.Clear();
        }

        public void HoverBeginSignalReceive(object interactable)
        {
            interactableSet.Add((IInteractable)interactable);
            Log(string.Format("Hover on object of type {0}", interactable.GetType()));
            interactableSet.Items.Sort((i1, i2)=>i1.GetInteractionType() - i2.GetInteractionType());
        }

        public void HoverEndSignalReceive(object interactable)
        {
            interactableSet.Remove((IInteractable)interactable);
            Log(string.Format("Finished hover on object of type {0}", interactable.GetType()));
        }

        public void Log(string text)
        {
            if (activateLogger)
            {
                GamedevsToolbox.Utils.Logger.Logger.Log(text);
            }
        }
    }
}