using System;
using System.Collections.Generic;
using System.Text;
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
        public void AddTeam(ICompetitionTeam team)
        {
            throw new NotImplementedException();
        }

        public void ProcessGame(ICompetitionGame game)
        {
            throw new NotImplementedException();
        }
    }
}
