using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffSeries:IDataObject, IDomain
    {
        //we don't want to have to use Rules once the season starts, the rules should just setup the series and it should be autonomous afterwards
        public long Id { get; set; }
        public Playoff Playoff { get; set; }
        public string Name { get; set; }    
        public int Round { get; set; }
        public PlayoffTeam Home { get; set; }
        public PlayoffTeam Away { get; set; }
        public string WinnerGoesTo { get; set; }
        public string LoserGoesTo { get; set; }
        public int HomeWins { get; set; }
        public int AwayWins { get; set; } 
        public string HomeFrom { get; set; }
        public string AwayFrom { get; set; }
        public int RequiredWins { get; set; }

        public bool Complete { get { return (HomeWins == RequiredWins || AwayWins == RequiredWins) } }

        public IList<PlayoffGame> Games { get; set; } = new List<PlayoffGame>();

        //this doesn't add it to the schedule at all
        public IList<PlayoffGame> CreateRequiredGames(IGameCreator gameCreater)
        {
            var newGames = new List<PlayoffGame>();

            if (!Complete)
            {                
                int winsRequired = RequiredWins - HomeWins >= RequiredWins - AwayWins ? RequiredWins - HomeWins : RequiredWins - AwayWins;

                int inCompleteOrUnProcessedGames = GetInCompelteOrUnProcessedGames().Count;

                for (int i = 0; i < (winsRequired - inCompleteOrUnProcessedGames); i++)
                {
                    var newGame = (PlayoffGame)gameCreater.CreateGame(Home, Away);
                    newGames.Add(newGame);
                    Games.Add(newGame);
                }
            }

            return newGames;
        }

        public IList<PlayoffGame> GetInCompelteOrUnProcessedGames()
        {
            return Games.Where(g => !g.Complete || !g.Processed).ToList();
        }

        public void ProcessGame(PlayoffGame game)
        {
            throw new NotImplementedException();
        }
    }
}
