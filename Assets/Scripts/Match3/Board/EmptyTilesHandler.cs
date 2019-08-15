using Match3.Interfaces;
using Match3.Items;
using Match3.Items.Pool;
using Match3.Scriptable;
using UnityEditor;
using UnityEngine;

namespace Match3.Board
{
    public class EmptyTilesHandler : IEmptyTilesHandler, IAffectedByGravity
    {
        
        private readonly IBoard _board;
        private readonly ItemPool _itemPool;
        private readonly ItemViewStorage _itemViewStorage;
        private readonly BoardSettings _boardSettings;
        private int _gravity;

        public EmptyTilesHandler(IBoard board, BoardSettings boardSettings, ItemPool itemPool, ItemViewStorage itemViewStorage)
        {
            _board = board;
            _boardSettings = boardSettings;
            _itemPool = itemPool;
            _itemViewStorage = itemViewStorage;
            _gravity = -1;
        }

        public void HandleEmptyTilesStep()
        {
            FallElementsDownWithGravity();
            AddNewElementsInTop();
        }

        public bool HasEmptyTiles()
        {
            foreach (var tile in _board)
            {
                if (tile.IsEmptyTile)
                {
                    return true;
                }
            }

            return false;
        }

        private void FallElementsDownWithGravity()
        {
            while (!DidAllTilesFall())
            {
                DoFallStep();
            }
        }

        private bool DidAllTilesFall()
        {
            foreach (var tile in _board)
            {
                var currentTilePosition = tile.PositionOnBoard;
                if (!tile.IsEmptyTile)
                {
                    var positionBelow = new PositionOnBoard
                    {
                        x = currentTilePosition.x,
                        y = currentTilePosition.y + _gravity
                    };
                    if (_board.IsTileOnBoard(positionBelow))
                    {
                        var tileBelow = _board.GetTileAt(positionBelow);
                        if (tileBelow.IsEmptyTile)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void DoFallStep()
        {
            foreach (var tile in _board)
            {
                if (!tile.IsEmptyTile)
                {
                    var tilePosition = tile.PositionOnBoard;
                    var positionBelow = new PositionOnBoard
                    {
                        x = tilePosition.x,
                        y = tilePosition.y + _gravity
                    };
                    if (_board.IsTileOnBoard(positionBelow))
                    {
                        var tileBelow = _board.GetTileAt(positionBelow);
                        if (tileBelow.IsEmptyTile)
                        {
                            tile.SwapWith(tileBelow);
                            
                            tile.ApplyChanges();
                            tileBelow.ApplyChanges();
                        }
                    }
                }
            }
        }

        private int GetTopRawIndex()
        {
            switch (_gravity)
            {
                case 1:
                    return 0;
                case -1:
                    return _boardSettings.NumberOfRaws - 1;
                default:
                    return -1;
            }
        }

        private void AddNewElementsInTop()
        {
            var topRawIndex = GetTopRawIndex();

            for (int x = 0; x < _boardSettings.NumberOfColumns; x++)
            {
                var position = new PositionOnBoard(x, topRawIndex);
                var tile = _board.GetTileAt(position);

                if (tile.IsEmptyTile)
                {
                    AssignItemToTile(tile);
                }
            }
        }

        private void AssignItemToTile(Tile tile)
        {
            var item = _itemPool.Get();
            item.Position = tile.PositionOnBoard;
            
            var itemView = _itemViewStorage.GetViewFromIndex(item.ItemIndex);
            itemView.AssociatedItem = item;

            tile.Item = item;
            tile.ApplyChanges();
        }

        public void SwapGravity()
        {
            if (_gravity > 0)
            {
                _gravity = -1;
            }
            else
            {
                _gravity = 1;
            }
        }
    }
}