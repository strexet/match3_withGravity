using System;
using JetBrains.Annotations;
using Match3.Interfaces;
using Match3.Scriptable;
using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Match3
{
    public class BoardController : MonoBehaviour, IBoardController
    {
        // TODO: Is it a good place for that variable/field/property?
        [SerializeField] private TileViewSettings TileViewSettings;
        [SerializeField] private Transform GameBoardRoot;
        private Board _board;
        private BoardClickHandler _boardClickHandler;
        
        public event Action BoardReadyEvent = delegate { };

        public void CreateBoard(BoardSettings boardSettings)
        {
            // TODO: Move dependencies away from the class, where they are used.
            _board = new Board(boardSettings);
            var numberOfAvailableColors = TileViewSettings.ItemColors.Length;
            _board.PopulateWithRandomColors(numberOfAvailableColors);
            CreateBoardView(boardSettings);
            _boardClickHandler = new BoardClickHandler();
        }

        // TODO: Does this method belong here?
        private void CreateBoardView(BoardSettings boardSettings)
        {
            for (int y = 0; y < boardSettings.NumberOfRaws; y++)
            {
                for (int x = 0; x < boardSettings.NumberOfColumns; x++)
                {
                    // TODO: Перенести логику создания в TileView class.
                    var tileView = Instantiate(TileViewSettings.Prefab, GameBoardRoot);
                    var tileViewPosition = new Vector3(x, y, 0) * TileViewSettings.TileSize;
                    tileView.transform.SetPositionAndRotation(tileViewPosition, Quaternion.identity);
                    tileView.AssociatedTile = _board.GetTileAt(x, y);
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
        
        private Tile GetTileAtPosition(Vector3 position)
        {
            // TODO: Fix getting tile at position.
            return null;
        }

        private void TryToSwap((Tile, Tile) tilesBeingSwapped)
        {
            if (CanSwap(tilesBeingSwapped))
            {
                tilesBeingSwapped.Item1.SwapWith(tilesBeingSwapped.Item2);
//                RemoveSwappedMatches();
//                HandleEmptyTiles();
//                HandleNewMatches();
            }
        }

        private void RemoveSwappedMatches()
        {
            // TODO: Insert removal of swapped tiles logic.
            // Get swapped tiles.
            // Remove those tiles and their matches from board.
        }

        

        private bool CanSwap((Tile, Tile) tilesBeingSwapped)
        {
            // TODO: Insert swap detection logic.
            // Check if two tiles can be swapped.
            // If tiles are neighbours.
            // If tiles make match (1 or 2) after the swap.
            // Remember matches!
            return true;
        }

        private void HandleNewMatches()
        {
            while (HasMatches())
            {
                //...
            }
        }

        private bool HasMatches()
        {
            // Is there any matches on the board now.
            throw new NotImplementedException();
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

        private void AddNewElementsAtTop()
        {
            throw new NotImplementedException();
        }

        private bool HasEmtyTileInTopRaw()
        {
            throw new NotImplementedException();
        }
    }
}