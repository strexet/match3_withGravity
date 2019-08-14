using Match3.Interfaces;
using UnityEngine;

namespace Match3.Items.Pool
{
    public class ItemViewPool : AbstractPool<ItemView>
    {
        private readonly ItemView _itemViewPrefab;
        private readonly Transform _itemViewRoot;
        private readonly IBoardPositionConverter _boardPositionConverter;
        
        public ItemViewPool(ItemView itemViewPrefab, Transform itemViewRoot,
            IBoardPositionConverter boardPositionConverter)
        {
            _itemViewPrefab = itemViewPrefab;
            _itemViewRoot = itemViewRoot;
            _boardPositionConverter = boardPositionConverter;
        }
        
        protected override ItemView CreateElement()
        {
            var itemView = Object.Instantiate(_itemViewPrefab, _itemViewRoot);
            itemView.Initialize(_boardPositionConverter);
            itemView.DeactivationEvent += OnDeactivation;
            
            return itemView;
        }
        
        private void OnDeactivation(ItemView itemView)
        {
            Return(itemView);
        }
    }
}