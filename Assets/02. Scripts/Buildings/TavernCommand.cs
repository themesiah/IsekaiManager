using UnityEngine;
using Isekai.Interactions;
using UnityEngine.Events;

namespace Isekai.Buildings
{
    public class TavernCommand : InteractionCommand
    {
        public override void Cancel()
        {
        }

        public override void Execute(UnityAction onFinishAction)
        {
            Debug.Log("Executed tavern command");
            onFinishAction();
        }

        public override void Undo(UnityAction onFinishAction)
        {
            throw new System.NotImplementedException();
        }
    }
}