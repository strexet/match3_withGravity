using System;
using System.Collections.Generic;
using Match3.Interfaces;
using Match3.Items;
using Match3.Items.Pool;
using Match3.Matches;
using Match3.Scriptable;
using UnityEngine;

namespace Match3.Board
{
    public class BoardController : MonoBehaviour, IBoardController, IAffectedByGravity
    {
        // TODO: Is it a good place for that variable/field/property?
        [SerializeField] private ItemViewSettings ItemViewSettings;
        [SerializeField] private Transform GameBoardRoot;

        private IBoardPositionConverter _boardPositionConverter;
        private IMatcher _matcher;
        private IMatchHandler _matchHandler;
        private IEmptyTilesHandler _emptyTilesHandler;
        private IBoard _board;
        private List<IAffectedByGravity> _affectedByGravityList;
        
        private BoardClickHandler _boardClickHandler;
        private BoardSettings _boardSettings;
        private ItemViewStorage _itemViewStorage;
        private bool _isHandlingMatches;
        private float _nextUpdateTime;

        public event Action BoardReadyEvent = delegate { };

        public void CreateBoard(BoardSettings boardSettings)
        {
            var numberOfAvailableColors = ItemViewSettings.ItemPrefabs.Length;
            _boardSettings = boardSettings;
            _affectedByGravityList = new List<IAffectedByGravity>(1);
            
            
            // TODO: Move dependencies away from the class, where they are used.
            _boardPositionConverter = new BoardPositionConverter(ItemViewSettings.TileSize);
            _itemViewStorage = new ItemViewStorage(ItemViewSettings, GameBoardRoot, _boardPositionConverter);


            var itemPool = new ItemPool(numberOfAvailableColors);
            _board = new SquareBoard(_boardSettings, numberOfAvailableColors, itemPool);
            CreateBoardView();

            var emptyTilesHandler = new EmptyTilesHandler(_board, _boardSettings, itemPool, _itemViewStorage);
            _emptyTilesHandler = emptyTilesHandler;
            _affectedByGravityList.Add(emptyTilesHandler);

            _matcher = new Matcher(_boardSettings, _board, 3);
            
            
            _boardClickHandler = new BoardClickHandler();
            
            ForceHandleMatches();
            
            _matchHandler = new MatchHandlerWithGravity(itemPool, _itemViewStorage, SwapGravity);
        }

        // TODO: Does this method belong here?
        private void CreateBoardView()
        {
            foreach (var tile in _board)
            {
                var item = tile.Item;
                var itemView = _itemViewStorage.GetViewFromIndex(item.ItemIndex);
                itemView.AssociatedItem = item;
            }
        }
        
        private void ForceHandleMatches()
        {
            _matchHandler = new SimpleMatchHandler();

            for (var match = _matcher.GetNextMatch(); 
                match.NumberOfMatchedTiles > 0  || _emptyTilesHandler.HasEmptyTiles(); 
                match = _matcher.GetNextMatch())
            {
                HandleMatches();
            }
        }

        public void HandleClick(GameObject clickedObject)
        {
            if (clickedObject == null)
            {
                return;
            }

            var clickedPosition = clickedObject.GetComponent<ItemView>().PositionOnBoard;
            var clickedTile = _board.GetTileAt(clickedPosition);
            _boardClickHandler.UpdateLastClickedTile(clickedTile);
            if (_boardClickHandler.TryingToSwap)
            {
                TryToSwap(_boardClickHandler.TilesBeingSwapped);
            }
        }

        private void TryToSwap((Tile, Tile) tilesBeingSwapped)
        {
            if (!_board.AreNeighbourTiles(tilesBeingSwapped))
            {
                return;
            }

            var tile_1 = tilesBeingSwapped.Item1;
            var tile_2 = tilesBeingSwapped.Item2;

            // Try to swap.
            tile_1.SwapWith(tile_2);

            var match = _matcher.GetNextMatch();
            if (match.NumberOfMatchedTiles == 0)
            {
                // Swap back on fail.
                tilesBeingSwapped.Item1.SwapWith(tilesBeingSwapped.Item2);
                return;
            }
            
            // Apply if there is new match.
            tile_1.ApplyChanges();
            tile_2.ApplyChanges();

            // Handle Matches in Update
        }

        private void Update()
        {
            if (Time.time < _nextUpdateTime )
            {
                return;
            }
            _nextUpdateTime = Time.time + 0.2f;
            
            HandleMatches();
        }

        private void HandleMatches()
        {
            if (_isHandlingMatches)
            {
                var match = _matcher.GetNextMatch();
                if (match.NumberOfMatchedTiles > 0)
                {
                    _matchHandler.HandleNextMatch(match);
                }
                else
                {
                    _isHandlingMatches = false;
                }
            }
            else
            {
                if (_emptyTilesHandler.HasEmptyTiles())
                {
                    _emptyTilesHandler.HandleEmptyTilesStep();
                }
                else
                {
                    _isHandlingMatches = true;
                }
            }
        }

        public void SwapGravity()
        {
            foreach (var affectedByGravity in _affectedByGravityList)
            {
                affectedByGravity.SwapGravity();
            }
        }
    }
}