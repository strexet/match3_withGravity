using System;
using System.Collections.Generic;
using Match3.Interfaces;
using Match3.Items;
using Match3.Items.Pool;
using Match3.MatchHandlers;
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
        private IMatcher _matcherForThree;
        private IMatcher _matcherForFour;
        private IMatchHandler _matchHandlerForFour;
        private IMatchHandler _matchHandlerForThree;
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

            _matcherForFour = new Matcher(_boardSettings, _board, 4);
            _matcherForThree = new Matcher(_boardSettings, _board, 3);
            
            _matchHandlerForFour = new FourMatchHandler(_matcherForFour, itemPool, _itemViewStorage, SwapGravity);
            _matchHandlerForThree = new ThreeMatchHandler(_matcherForThree);
            
            _boardClickHandler = new BoardClickHandler();
            
            _matchHandlerForThree.HandleMatches();
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

            tile_1.SwapWith(tile_2);
            if (!_matcherForThree.HasMatches())
            {
                tilesBeingSwapped.Item1.SwapWith(tilesBeingSwapped.Item2);
                return;
            }
            
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

            
            if (_isHandlingMatches)
            {
                _matchHandlerForFour.HandleMatches();
                _matchHandlerForThree.HandleMatches();
            }
            else
            {
                _emptyTilesHandler.HandleEmptyTilesStep();
            }

            _isHandlingMatches = !_isHandlingMatches;
        }
        
        public void SwapGravity()
        {
            Debug.Break();
            
            foreach (var affectedByGravity in _affectedByGravityList)
            {
                affectedByGravity.SwapGravity();
            }
        }
    }
}