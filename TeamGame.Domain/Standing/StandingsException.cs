using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Standing
{
    public class StandingsException:Exception
    {
        public StandingsException()
        {
        }

        public StandingsException(string message)
            : base(message)
        {
        }

        public StandingsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
