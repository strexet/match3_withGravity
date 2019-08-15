using Match3.Board;
using Match3.Matches;

namespace Match3.Interfaces
{
    public interface IMatchHandler
    {
        void HandleNextMatch(Match match);
    }
}