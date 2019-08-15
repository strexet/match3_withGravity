using System.Collections.Generic;
using Match3.Board;
using Match3.Interfaces;
using Match3.Scriptable;

namespace Match3.Matches
{
    
    public class Matcher : IMatcher
    {
        private readonly IBoard _board;
        private readonly int _minTilesInRawForMatch;
        private readonly List<Tile> _currentSimilarTiles;
        private readonly List<Tile> _similarTiles;
        private bool _isCheckComplete;

        public Matcher(BoardSettings boardSettings, IBoard board, int minTilesInRawForMatch)
        {
            _board = board;
            _minTilesInRawForMatch = minTilesInRawForMatch;
            _currentSimilarTiles = new List<Tile>(5);
            _similarTiles = new List<Tile>(5);
        }
        
        public Match GetNextMatch()
        {
            _similarTiles.Clear();

            foreach (var tile in _board)
            {
                CheckHorizontal(tile);
                CheckVertical(tile);

                if (_similarTiles.Count > _minTilesInRawForMatch)
                {
                    var match = Match.CreateFromArray(_similarTiles.ToArray());
                    return match;
                }
            }

            return Match.CreateEmpty();
        }

        private void CheckVertical(Tile tile)
        {
            _currentSimilarTiles.Clear();
            _currentSimilarTiles.Add(tile);

            var step = new PositionOnBoard(0, 1);
            CheckFromTileWithStep(step, tile);

            step = new PositionOnBoard(0, -1);
            CheckFromTileWithStep(step, tile);

            if (_currentSimilarTiles.Count >= _minTilesInRawForMatch )
            {
                _similarTiles.AddRange(_currentSimilarTiles);
            }
        }

        private void CheckHorizontal(Tile tile)
        {
            _currentSimilarTiles.Clear();
            _currentSimilarTiles.Add(tile);

            var step = new PositionOnBoard(1, 0);
            CheckFromTileWithStep(step, tile);

            step = new PositionOnBoard(-1, 0);
            CheckFromTileWithStep(step, tile);

            if (_currentSimilarTiles.Count >= _minTilesInRawForMatch)
            {
                _similarTiles.AddRange(_currentSimilarTiles);
            }
        }

        private void CheckFromTileWithStep(PositionOnBoard step, Tile tile)
        {
            var position = tile.PositionOnBoard;
            var matchingItemIndex = tile.ItemIndex;
            var totalOffset = new PositionOnBoard(0, 0);
            
            while (true)
            {
                totalOffset.x += step.x;
                totalOffset.y += step.y;
                var offsetPosition = new PositionOnBoard
                {
                    x = position.x + totalOffset.x,
                    y = position.y + totalOffset.y
                };

                if (!_board.IsTileOnBoard(offsetPosition))
                {
                    break;
                }

                var offsetTile = _board.GetTileAt(offsetPosition);

                if (offsetTile.IsEmptyTile
                    || offsetTile.ItemIndex != matchingItemIndex)
                {
                    break;
                }
                
                _currentSimilarTiles.Add(offsetTile);    
            }
        }
    }
}