using Match3.Board;
using Match3.Interfaces;
using UnityEngine;

namespace Match3.MatchHandlers
{
    public class MatchHandler : IMatchHandler
    {
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
            var modifierIndex = Random.Range(0, match.NumberOfMatchedTiles);
            
            for (int i = 0; i < match.NumberOfMatchedTiles; i++)
            {
                var matchedTiles = match.MatchedTiles;
                var itemIndex = matchedTiles[i].Item.ItemIndex;
                matchedTiles[i].OnMatch();
                
                
                    
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