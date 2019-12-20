using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs
{
    public class Playoff : ICompetition, IDataObject
    {
        public long Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<ICompetitionTeam> Teams { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public int StartingDay { get; set; }
        public string Name { get; set; }
        public bool Started { get; set; }
        public bool Complete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGameCreator GameCreator { get; set; }
        public IList<PlayoffSeries> Series { get; set; }
        public IList<PlayoffGroup> Groups { get; set; }
        public IList<IRanking> Rankings { get; set; }

        public void AddTeam(ICompetitionTeam team)
        {
            throw new NotImplementedException();
        }

        public void ProcessGame(ICompetitionGame game)
        {
            var playoffGame = (PlayoffGame)game;

            var series = playoffGame.Series;

            series.ProcessGame((PlayoffGame)game);
        }

        public void AddRanking(string teamName, string groupName, int rank)
        {
            var group = Groups.Where(g => g.Name.Equals(groupName)).FirstOrDefault();
            var team = Teams.Where(t => t.Name.Equals(teamName)).First();

            if (group == null)
            {
                group = new PlayoffGroup() { Name = groupName };
            }

            group.Rankings.Add(new PlayoffRanking() { PlayoffGroup = group, Rank = rank, Team = team });
        }
        //should be used when a playoff series is done, or at the beginning of the playoffs when setting up initial groups
        public void AddRanking(PlayoffTeam team, string groupName, string initialRankComesFrom)
        {
            var group = Groups.Where(g => g.Name.Equals(groupName)).FirstOrDefault();
            var initialRankingGroup = Groups.Where(g => g.Name.Equals(initialRankComesFrom)).First();//it's okay because if it's null config is messed up

            if (group == null)
            {
                group = new PlayoffGroup() { Name = groupName };
            }

            var currentRanking = group.Rankings.Where(r => ((PlayoffTeam)r.Team).Id == team.Id).FirstOrDefault();
            var initialGroupRanking = initialRankingGroup.Rankings.Where(r => ((PlayoffTeam)r.Team).Id == team.Id).FirstOrDefault();

            if (currentRanking == null) throw new PlayoffException("Why was it already here!?");
            else
            {
                group.Rankings.Add(new PlayoffRanking() { PlayoffGroup = group, Rank = initialGroupRanking.Rank, Team = team });
            }
            
        }
    }
}
