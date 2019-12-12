using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Seasons
{
    public class SeasonException:Exception
    {
        public SeasonException()
        {
        }

        public SeasonException(string message)
            : base(message)
        {
        }

        public SeasonException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
