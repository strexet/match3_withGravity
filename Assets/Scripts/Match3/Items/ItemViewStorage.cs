using System.Collections.Generic;
using Match3.Interfaces;
using Match3.Items.Pool;
using Match3.Scriptable;
using UnityEngine;

namespace Match3.Items
{
    public class ItemViewStorage
    {
        private readonly Dictionary<int, ItemViewPool> _itemIndexToPool;
        private readonly ItemViewSettings _itemViewSettings;
        private readonly Transform _gameBoardRoot;
        private readonly IBoardPositionConverter _boardPositionConverter;

        
        public ItemViewStorage(ItemViewSettings itemViewSettings, Transform gameBoardRoot, IBoardPositionConverter boardPositionConverter)
        {
            _itemViewSettings = itemViewSettings;
            _gameBoardRoot = gameBoardRoot;
            _boardPositionConverter = boardPositionConverter;
            _itemIndexToPool = new Dictionary<int, ItemViewPool>();
        }

        public ItemView GetViewFromIndex(int index)
        {
            var pool = GetPoolForItem(index);
            var itemView = pool.Get();

            return itemView;
        }

        private ItemViewPool GetPoolForItem(int index)
        {
            ItemViewPool pool;
            
            if (_itemIndexToPool.ContainsKey(index))
            {
                pool = _itemIndexToPool[index];
            }
            else
            {
                var itemViewPrefab = _itemViewSettings.ItemPrefabs[index];
                pool = new ItemViewPool(itemViewPrefab, _gameBoardRoot, _boardPositionConverter);
                _itemIndexToPool.Add(index, pool);
            }

            return pool;
        }
    }
}