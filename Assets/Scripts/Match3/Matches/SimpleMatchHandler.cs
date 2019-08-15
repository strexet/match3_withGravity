using Match3.Interfaces;

namespace Match3.Matches
{
    public class SimpleMatchHandler : IMatchHandler
    {
        public void HandleNextMatch(Match match)
        {
            for (int i = 0; i < match.NumberOfMatchedTiles; i++)
            {
                match.MatchedTiles[i].OnMatch();
            }
        }
    }
}