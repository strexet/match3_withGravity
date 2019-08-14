using System.Collections;
using System.Collections.Generic;
using Match3.Interfaces;
using Match3.Items.Pool;
using Match3.Scriptable;

namespace Match3.Board
{
    public class SquareBoard : IBoard
    {
        private readonly Tile[] _grid;
        private readonly BoardSettings _boardSettings;
        private readonly ItemPool _itemPool;
        
        public SquareBoard(BoardSettings boardSettings, int numberOfAvailableColors, ItemPool itemPool)
        {
            var numberOfTiles = boardSettings.NumberOfColumns * boardSettings.NumberOfRaws;
            _grid = new Tile[numberOfTiles];
            _boardSettings = boardSettings;
            _itemPool = itemPool;
            PopulateBoard(numberOfAvailableColors);
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

        public void SetTileAt(PositionOnBoard position, Tile tile)
        {
            _grid[GetIndex(position)] = tile;
            tile.PositionOnBoard = position;
        }

        public bool AreNeighbourTiles((Tile, Tile) tilesBeingSwapped)
        {
            return tilesBeingSwapped.Item1.DistanceTo(tilesBeingSwapped.Item2) == 1;
        }

        private void PopulateBoard(int numberOfAvailableColors)
        {
            for (int y = 0; y < _boardSettings.NumberOfRaws; y++)
            {
                for (int x = 0; x < _boardSettings.NumberOfColumns; x++)
                {
                    var position = new PositionOnBoard(x, y);
                    var tile = new Tile(position, _itemPool);
                    SetTileAt(position, tile);
                }               
            }
        }

        private int GetIndex(PositionOnBoard position)
        {
            
            return position.x + position.y * _boardSettings.NumberOfColumns;
        }

        public IEnumerator<Tile> GetEnumerator()
        { 
            for (int i = 0; i < _grid.Length; i++)
            {
                yield return _grid[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}