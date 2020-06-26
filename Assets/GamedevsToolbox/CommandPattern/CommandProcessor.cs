﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace GamedevsToolbox.CommandPattern
{
    /*
     * This monobehaviour can be used as standalone using the ProcessCommand method, or can be used with a command event.
     * TODO: Undo things
     */
    public class CommandProcessor : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        public CommandEvent Event;

        [System.Serializable]
        public class ObjectEvent : UnityEvent<Command> { };

        [SerializeField]
        private List<Command> commandQueue;

        [SerializeField]
        private bool activateLogger = false;

        private void OnEnable()
        {
            Event?.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event?.UnregisterListener(this);
        }

        private void Awake()
        {
            commandQueue = new List<Command>();
        }

        public void OnEventRaised(Command command)
        {
            ProcessCommand(command);
        }

        public void ProcessCommand(Command command)
        {
            OnProcessCommand(command);
            Log(string.Format("Added command of type {0}", command.GetType()));
            commandQueue.Add(command);
            Log(string.Format("Commands in queue: {0}", commandQueue.Count));
            if (commandQueue.Count == 1)
            {
                Log(string.Format("Executing command of type {0}", command.GetType()));
                command.Execute(ProcessNextCommand);
            }
        }

        public virtual void OnProcessCommand(Command command)
        {

        }

        private void ProcessNextCommand()
        {
            OnProcessNextCommand();
            Log("Processing next command");
            commandQueue.RemoveAt(0);
            Log(string.Format("Commands in queue: {0}", commandQueue.Count));
            if (commandQueue.Count > 0)
            {
                Command c = commandQueue[0];
                Log(string.Format("Executing command of type {0}", c.GetType()));
                c.Execute(ProcessNextCommand);
            }
        }

        public virtual void OnProcessNextCommand()
        {

        }

        public void CancelAll()
        {
            if (commandQueue.Count > 0)
                commandQueue[0].Cancel();
            commandQueue.Clear();
        }

        private void Log(string text)
        {
            if (activateLogger)
            {
                Utils.Logger.Logger.Log(text);
            }
        }
    }
}