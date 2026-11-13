using System;
using System.Collections.Generic;
using System.Text;


namespace NumberQuest.Events
{
    public class GameEventArgs : EventArgs
    {
        public string Message { get; set; }

        public GameEventArgs(string message)
        {
            Message = message;
        }
    }
}

