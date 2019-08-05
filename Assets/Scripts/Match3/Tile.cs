using System;
using Random = UnityEngine.Random;

namespace Match3
{
    public class Tile
    {
        private int _itemIndex;
        private Tile() { }

        public static Tile CreateRandom(int NumberOfAvailableColors)
        {
            var tile = new Tile();
            tile.ItemIndex = Random.Range(0, NumberOfAvailableColors);
            return tile;
        }

        public int ItemIndex
        {
            get => _itemIndex;
            private set
            {
                _itemIndex = value;
                OnItemIndexChangedEvent(value);
            }
        }

        public event Action<int> OnItemIndexChangedEvent = delegate {  };

        public void SwapWith(Tile other)
        {
            var tempIndex = ItemIndex;
            ItemIndex = other.ItemIndex;
            other.ItemIndex = tempIndex;
        }
    }
}