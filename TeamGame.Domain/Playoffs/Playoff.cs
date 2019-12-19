using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs
{
    public class Playoff : ICompetition, IDataObject
    {
        public long Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<ICompetitionTeam> Teams { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Year { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int StartingDay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Started { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Complete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGameCreator GameCreator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }        

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
