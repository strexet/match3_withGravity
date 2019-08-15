using System;
using Match3.Board;
using Match3.Interfaces;
using Match3.Items;
using Match3.Items.Modifiers;
using Match3.Items.Pool;
using Random = UnityEngine.Random;

namespace Match3.Matches
{
    public class MatchHandlerWithGravity : IMatchHandler
    {
        private readonly ItemPool _itemPool;
        private readonly ItemViewStorage _itemViewStorage;
        private readonly Action _onGravityModifierActivation;

        public MatchHandlerWithGravity(ItemPool itemPool, ItemViewStorage itemViewStorage, Action onGravityModifierActivation)
        {
            _itemPool = itemPool;
            _itemViewStorage = itemViewStorage;
            _onGravityModifierActivation = onGravityModifierActivation;
        }
        
        public void HandleNextMatch(Match match)
        {
            if (match.NumberOfMatchedTiles >= 4)
            {
                HandleGravityMatch(match);
            }
            else
            {
                HandleRegularMatch(match);
            }
        }

        private void HandleRegularMatch(Match match)
        {
            for (int i = 0; i < match.NumberOfMatchedTiles; i++)
            {
                match.MatchedTiles[i].OnMatch();
            }
        }

        private void HandleGravityMatch(Match match)
        {
            var matchedItemIndex = match.MatchedTiles[0].Item.ItemIndex;
            var indexOfTileWithModifier = Random.Range(0, match.NumberOfMatchedTiles);
            var tileForInsert = match.MatchedTiles[indexOfTileWithModifier];

            HandleRegularMatch(match);
            
            InsertItemWithModifier(matchedItemIndex, tileForInsert);
        }

        private void InsertItemWithModifier(int matchedItemIndex, Tile tileForInsert)
        {
            var itemWithGravity = _itemPool.Get();
            itemWithGravity.ItemIndex = matchedItemIndex;
            itemWithGravity.Position = tileForInsert.PositionOnBoard;

            var gravityModifier = new GravityModifier(_onGravityModifierActivation);
            itemWithGravity.SetDestructionModifier(gravityModifier);

            var itemView = _itemViewStorage.GetViewFromIndex(matchedItemIndex);
            itemView.AssociatedItem = itemWithGravity;

            tileForInsert.Item = itemWithGravity;
            tileForInsert.ApplyChanges();
        }
    }
}