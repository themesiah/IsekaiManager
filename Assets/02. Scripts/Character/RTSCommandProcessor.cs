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
    }
}