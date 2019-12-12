using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Seasons
{
    public class SeasonScheduleException:Exception
    {
        public SeasonScheduleException()
        {
        }

        public SeasonScheduleException(string message)
            : base(message)
        {
        }

        public SeasonScheduleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
