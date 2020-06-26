using UnityEngine;
using UnityEngine.Events;

namespace GamedevsToolbox.CommandPattern
{
    public abstract class Command
    {
        public Command() {}
        public abstract void Execute(UnityAction onFinishAction);
        public abstract void Undo(UnityAction onFinishAction);
        public abstract void Cancel();
    }
}