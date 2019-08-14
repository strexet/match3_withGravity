using System;
using Match3.Board;
using Match3.Interfaces;
using Match3.Items;
using Match3.Items.Modifiers;
using Match3.Items.Pool;
using Random = UnityEngine.Random;

namespace Match3.MatchHandlers
{
    public class FourMatchHandler : AbstractMatchHandler
    {
        private readonly ItemPool _itemPool;
        private readonly ItemViewStorage _itemViewStorage;
        private readonly Action _onGravityModifierActivation;

        public FourMatchHandler(IMatcher matcher, ItemPool itemPool, ItemViewStorage itemViewStorage, Action onGravityModifierActivation) 
            : base(matcher)
        {
            _itemPool = itemPool;
            _itemViewStorage = itemViewStorage;
            _onGravityModifierActivation = onGravityModifierActivation;
        }

        protected override void RemoveMatchedTiles(Tile[] matchedTiles)
        {
            for (int i = 0; i < matchedTiles.Length; i++)
            {
                var itemIndex = matchedTiles[i].Item.ItemIndex;
                matchedTiles[i].OnMatch();
                
                if ((i + 1) % 4 == 0)
                {
                    var modifierIndex = i - Random.Range(0, 4);
                    var tileForInsert = matchedTiles[modifierIndex];
                    
                    var newItem = _itemPool.Get();
                    newItem.ItemIndex = itemIndex;
                    newItem.Position = tileForInsert.PositionOnBoard;
                    
                    var gravityModifier = new GravityModifier(_onGravityModifierActivation);
                    newItem.SetDestructionModifier(gravityModifier);
                    
                    var itemView = _itemViewStorage.GetViewFromIndex(itemIndex);
                    itemView.AssociatedItem = newItem;
                    
                    tileForInsert.Item = newItem;
                    tileForInsert.ApplyChanges();
                }
            }
        }
    }
}