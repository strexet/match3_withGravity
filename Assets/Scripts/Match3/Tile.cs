using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Match3
{
    public class Tile
    {
        public PositionOnBoard PositionOnBoard;
        
        private bool _isEmptyTile;
        private int _itemIndex;

        public bool IsEmptyTile
        {
            get => _isEmptyTile;
            set
            {
                _isEmptyTile = value;
                OnTileStateChangedEvent(_itemIndex);
            }
        }

        public int ItemIndex
        {
            get => _itemIndex;
            set
            {
                _itemIndex = value;
                OnTileStateChangedEvent(_itemIndex);
            }
        }
        
        public event Action<int> OnTileStateChangedEvent = delegate {  };

        private Tile() { }

        public static Tile CreateRandom(int numberOfAvailableColors)
        {
            var tile = new Tile();
            tile._itemIndex = Random.Range(0, numberOfAvailableColors);
            tile._isEmptyTile = false;
            return tile;
        }

        public void SwapWith(Tile other)
        {
            var tempIndex = _itemIndex;
            _itemIndex = other._itemIndex;
            other._itemIndex = tempIndex;
        }
        
        public void ApplySwap()
        {
            OnTileStateChangedEvent(_itemIndex);
        }

        public int DistanceTo(Tile other)
        {
            return Mathf.Abs(PositionOnBoard.x - other.PositionOnBoard.x)
                   + Mathf.Abs(PositionOnBoard.y - other.PositionOnBoard.y);
        }
    }
}