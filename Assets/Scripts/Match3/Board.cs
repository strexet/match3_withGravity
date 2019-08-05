using System.Collections.Generic;
using Match3.Scriptable;

namespace Match3
{
    public class Board
    {
        private Tile[] _grid;
        private BoardSettings _boardSettings;
        public Board(BoardSettings boardSettings)
        {
            var numberOfTiles = boardSettings.NumberOfColumns * boardSettings.NumberOfRaws;
            _grid = new Tile[numberOfTiles];
            _boardSettings = boardSettings;
        }

        public Tile GetTileAt(int x, int y)
        {
            return _grid[GetIndex(x, y)];
        }

        public void SetTileAt(Tile tile, int x, int y)
        {
            _grid[GetIndex(x, y)] = tile;
        }

        public void PopulateWithRandomColors(int numberOfAvailableColors)
        {
            for (int y = 0; y < _boardSettings.NumberOfRaws; y++)
            {
                for (int x = 0; x < _boardSettings.NumberOfColumns; x++)
                {
                    var tile = Tile.CreateRandom(numberOfAvailableColors);
                    SetTileAt(tile, x, y);
                }
            } 
        }

        private int GetIndex(int x, int y)
        {
            return x + y * _boardSettings.NumberOfColumns;
        }
    }
}