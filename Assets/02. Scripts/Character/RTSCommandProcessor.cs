using GamedevsToolbox.CommandPattern;
using UnityEngine;

namespace Isekai.Characters
{
    public class RTSCommandProcessor : CommandProcessor
    {
        public override void OnProcessCommand(Command command, bool cancelOldCommands)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && cancelOldCommands)
            {
                CancelAll();
            }
        }

        public override void OnProcessNextCommand()
        {
            IsBusy = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                CancelAll();
            }
        }

        public void CancelCommand(Command command)
        {
            if (commandQueue.Contains(command))
            {
                command.Cancel();
                commandQueue.Remove(command);
            }
        }
    }
}