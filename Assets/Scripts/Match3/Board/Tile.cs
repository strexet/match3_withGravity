using Match3.Items;
using Match3.Items.Pool;
using UnityEngine;

namespace Match3.Board
{
    public class Tile
    {
        public Item Item;
        public PositionOnBoard PositionOnBoard;
        private readonly ItemPool _itemPool;

        public bool IsEmptyTile
        {
            get => !Item.IsActive;
        }
        
        public int ItemIndex
        {
            get => Item.ItemIndex;
        }

        public Tile(PositionOnBoard positionOnBoard, ItemPool itemPool)
        {
            _itemPool = itemPool;
            PositionOnBoard = positionOnBoard;
            Item = itemPool.Get();
            Item.Position = positionOnBoard;
        }

        public void SwapWith(Tile other)
        {
            var tempItem = Item;
            Item = other.Item;
            other.Item = tempItem;
        }

        public void ApplyChanges()
        {
            Item.Position = PositionOnBoard;
        }

        public int DistanceTo(Tile other)
        {
            return Mathf.Abs(PositionOnBoard.x - other.PositionOnBoard.x)
                   + Mathf.Abs(PositionOnBoard.y - other.PositionOnBoard.y);
        }

        public virtual void OnMatch()
        {
            _itemPool.Return(Item);
        }
    }
}