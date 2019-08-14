using Match3.Board;
using Match3.Interfaces;

namespace Match3.MatchHandlers
{
    public class ThreeMatchHandler : AbstractMatchHandler
    {
        public ThreeMatchHandler(IMatcher matcher) : base(matcher)
        {
        }

        protected override void RemoveMatchedTiles(Tile[] matchedTiles)
        {
            for (int i = 0; i < matchedTiles.Length; i++)
            {
                matchedTiles[i].OnMatch();
            }
        }
    }
}