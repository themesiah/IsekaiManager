using UnityEngine.Events;
using UnityEngine;
using Isekai.Interactions;
using System.Collections;

namespace Isekai.Buildings
{
    public class InteractionNotAvailableCommand : InteractionCommand
    {
        string message;
        public InteractionNotAvailableCommand(string msg)
        {
            message = msg;
        }

        public override void Cancel()
        {
        }

        public override void Execute(UnityAction onFinishAction)
        {
            Debug.LogError(message);
        }

        public override void Undo(UnityAction onFinishAction)
        {
        }
    }
}