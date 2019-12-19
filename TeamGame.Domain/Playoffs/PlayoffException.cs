using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffException : Exception
    {
        public PlayoffException()
        {
        }

        public PlayoffException(string message)
            : base(message)
        {
        }

        public PlayoffException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
