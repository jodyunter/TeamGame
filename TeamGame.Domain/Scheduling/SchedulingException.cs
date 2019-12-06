using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Scheduling
{
    public class SchedulingException:Exception
    {
        public SchedulingException()
        {
        }

        public SchedulingException(string message)
            : base(message)
        {
        }

        public SchedulingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
