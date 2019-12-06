using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Service
{
    public class Message
    {
        public string Text { get; set; }
        public MessageSeverity Severity { get; set; }
    }

    public enum MessageSeverity
    {
        HIGH = 0,
        MEDIUM = 1,
        LOW = 2
    }
}
