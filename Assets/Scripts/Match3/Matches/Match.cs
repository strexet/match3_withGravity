using System.Collections.Generic;
using Match3.Board;

namespace Match3.Matches
{
    public class Match
    {
        public readonly int NumberOfMatchedTiles;
        public readonly List<Tile> MatchedTiles;

        public static Match CreateEmpty()
        {
            return new Match();
        }
        
        public static Match CreateFromArray(Tile[] tiles)
        {
            return new Match(tiles);
        }

        private Match()
        {
            NumberOfMatchedTiles = 0;
        }

        private Match(Tile[] tiles)
        {
            MatchedTiles = new List<Tile>(tiles.Length);
            
            for (int i = 0; i < tiles.Length; i++)
            {
                var tile = tiles[i];
                if (!MatchedTiles.Contains(tile))
                {
                    MatchedTiles.Add(tile);
                }
            }

            NumberOfMatchedTiles = MatchedTiles.Count;
        }
    }
}