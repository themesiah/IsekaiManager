using System.Collections;
using System;

namespace GamedevsToolbox.StateMachine
{
    public interface ICoroutineState
    {
        IEnumerator EnterState();
        IEnumerator ExitState();
        IEnumerator Update(Action<string> resolve);
        void ReceiveSignal(string signal);
    }
}