using Match3.Scriptable;

namespace Match3
{
    public class Board : IBoard
    {
        private readonly Tile[] _grid;
        private readonly BoardSettings _boardSettings;
        
        public Board(BoardSettings boardSettings, int numberOfAvailableColors)
        {
            var numberOfTiles = boardSettings.NumberOfColumns * boardSettings.NumberOfRaws;
            _grid = new Tile[numberOfTiles];
            _boardSettings = boardSettings;
            PopulateWithRandomColors(numberOfAvailableColors);
        }

        public bool IsTileOnBoard(PositionOnBoard position)
        {
            if (position.x < 0
                || position.y < 0
                || position.x >= _boardSettings.NumberOfColumns
                || position.y >= _boardSettings.NumberOfRaws)
            {
                return false;
            }
            return true;
        }

        public Tile GetTileAt(PositionOnBoard position)
        {
            return _grid[GetIndex(position)];
        }

        public void SetTileAt(Tile tile, PositionOnBoard position)
        {
            _grid[GetIndex(position)] = tile;
            tile.PositionOnBoard = position;
        }

        private void PopulateWithRandomColors(int numberOfAvailableColors)
        {
            for (int y = 0; y < _boardSettings.NumberOfRaws; y++)
            {
                for (int x = 0; x < _boardSettings.NumberOfColumns; x++)
                {
                    var tile = Tile.CreateRandom(numberOfAvailableColors);
                    var position = new PositionOnBoard
                    {
                        x = x, 
                        y = y
                    };
                    SetTileAt(tile, position);
                }
            } 
        }

        private int GetIndex(PositionOnBoard position)
        {
            return position.x + position.y * _boardSettings.NumberOfColumns;
        }
    }
}