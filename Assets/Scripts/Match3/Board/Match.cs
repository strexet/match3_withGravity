namespace Match3.Board
{
    public class Match
    {
        public readonly int NumberOfMatchedTiles;
        public readonly Tile[] MatchedTiles;

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
            NumberOfMatchedTiles = tiles.Length;
            MatchedTiles = new Tile[NumberOfMatchedTiles];
            for (int i = 0; i < MatchedTiles.Length; i++)
            {
                MatchedTiles[i] = tiles[i];
            }
        }
    }
}