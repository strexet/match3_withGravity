using System;
using System.Collections.Generic;
using Match3.Interfaces;
using Match3.Scriptable;
using UnityEngine;

namespace Match3
{
    public interface IMatcher
    {
        bool HasMatches();
        Tile[] GetMatchedTiles();
    }

    // TODO: Refactor this class.
    public class Matcher : IMatcher
    {
        private readonly IBoard _board;
        private readonly BoardSettings _boardSettings;
        private readonly List<Tile> _currentMatches;
        private readonly List<Tile> _allMatches;

        public Matcher(BoardSettings boardSettings, IBoard board)
        {
            _boardSettings = boardSettings;
            _board = board;
            _currentMatches = new List<Tile>(5);
            _allMatches = new List<Tile>(10);
        }
        public bool HasMatches()
        {
            for (int y = 0; y < _boardSettings.NumberOfRaws; y++)
            {
                for (int x = 0; x < _boardSettings.NumberOfColumns; x++)
                {
                    _currentMatches.Clear();
                    var currentTilePosition = new PositionOnBoard
                    {
                        x = x,
                        y = y
                    };

                    var tile = _board.GetTileAt(currentTilePosition);
                    var matchingItemIndex = tile.ItemIndex;
                    _currentMatches.Add(tile);

                    var step = new PositionOnBoard(1, 0);
                    MatchWithStep(step, x, y, matchingItemIndex);

                    step = new PositionOnBoard(-1, 0);
                    MatchWithStep(step, x, y, matchingItemIndex);
                    
                    step = new PositionOnBoard(0, 1);
                    MatchWithStep(step, x, y, matchingItemIndex);
                    
                    step = new PositionOnBoard(0, -1);
                    MatchWithStep(step, x, y, matchingItemIndex);
                    
                    if (_currentMatches.Count > 2)
                    {
                        _allMatches.AddRange(_currentMatches);
                        return true;
                    }
                }
            }
            
            return false;
        }

        public Tile[] GetMatchedTiles()
        {
            return _allMatches.ToArray();
        }

        private void MatchWithStep(PositionOnBoard step, int x, int y, int matchingItemIndex)
        {
            PositionOnBoard totalOffset = new PositionOnBoard(0, 0);
            while (true)
            {
                totalOffset.x += step.x;
                totalOffset.y += step.y;
                var offsetPosition = new PositionOnBoard
                {
                    x = x + totalOffset.x,
                    y = y + totalOffset.y
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

                _currentMatches.Add(offsetTile);    
            }
        }
    }

    public class BoardController : MonoBehaviour, IBoardController
    {
        // TODO: Is it a good place for that variable/field/property?
        [SerializeField] private TileViewSettings TileViewSettings;
        [SerializeField] private Transform GameBoardRoot;        
        [SerializeField] private BoardSettings BoardSettings;

        private IMatcher _boardMatcher;
        private IBoard _board;
        private BoardClickHandler _boardClickHandler;

        
        public event Action BoardReadyEvent = delegate { };

        public void CreateBoard()
        {
            // TODO: Move dependencies away from the class, where they are used.
            var numberOfAvailableColors = TileViewSettings.ItemColors.Length;
            _board = new Board(BoardSettings, numberOfAvailableColors);
            // TODO: Find better place for boardMatcher dependency injection.
            _boardMatcher = new Matcher(BoardSettings, _board);
            CreateBoardView();
            _boardClickHandler = new BoardClickHandler();
        }

        // TODO: Does this method belong here?
        private void CreateBoardView()
        {
            for (int y = 0; y < BoardSettings.NumberOfRaws; y++)
            {
                for (int x = 0; x < BoardSettings.NumberOfColumns; x++)
                {
                    // TODO: Перенести логику создания в TileView class.
                    var tileView = Instantiate(TileViewSettings.Prefab, GameBoardRoot);
                    var tileViewPosition = new Vector3(x, y, 0) * TileViewSettings.TileSize;
                    tileView.transform.SetPositionAndRotation(tileViewPosition, Quaternion.identity);
                    var positionOnBoard = new PositionOnBoard
                    {
                        x = x,
                        y = y
                    };
                    tileView.AssociatedTile = _board.GetTileAt(positionOnBoard);
                }
            } 
        }

        public void HandleClick(GameObject clickedObject)
        {
            if (clickedObject == null)
            {
                return;
            }

            var clickedTile = clickedObject.GetComponent<TileView>().AssociatedTile;
            _boardClickHandler.UpdateLastClickedTile(clickedTile);
            if (_boardClickHandler.TryingToSwap)
            {
                TryToSwap(_boardClickHandler.TilesBeingSwapped);
            }
        }

        private void TryToSwap((Tile, Tile) tilesBeingSwapped)
        {
            if (!AreNeighbourTiles(tilesBeingSwapped))
            {
                return;
            }

            tilesBeingSwapped.Item1.SwapWith(tilesBeingSwapped.Item2);
            if (!_boardMatcher.HasMatches())
            {
                tilesBeingSwapped.Item1.SwapWith(tilesBeingSwapped.Item2);
                return;
            }
            
            tilesBeingSwapped.Item1.ApplySwap();
            tilesBeingSwapped.Item2.ApplySwap();
            HandleMatches();
        }
        
        private bool AreNeighbourTiles((Tile, Tile) tilesBeingSwapped)
        {
            return tilesBeingSwapped.Item1.DistanceTo(tilesBeingSwapped.Item2) == 1;
        }

        private void HandleMatches()
        {
            while (_boardMatcher.HasMatches())
            {
                Debug.Log("HAS MATCH!!! >2");
                Tile[] matchedTiles = _boardMatcher.GetMatchedTiles();
                SetTilesEmpty(matchedTiles);
//                HandleEmptyTiles();
            }
        }

        
        
        private void SetTilesEmpty(Tile[] matchedTiles)
        {
            for (int i = 0; i < matchedTiles.Length; i++)
            {
                matchedTiles[i].IsEmptyTile = true;
            }
        }

        private void HandleEmptyTiles()
        {
            FallElementsDownWithGravity();
            AddNewElements();
        }

        private void FallElementsDownWithGravity()
        {
            // Move tiles down with the gravity until there is nowhere to move.
            throw new NotImplementedException();
        }

        private void AddNewElements()
        {
            while (HasEmtyTileInTopRaw())
            {
                AddNewElementsAtTop();
            }
        }
        
        private bool HasEmtyTileInTopRaw()
        {
            throw new NotImplementedException();
        }

        private void AddNewElementsAtTop()
        {
            throw new NotImplementedException();
        }

        
    }
}