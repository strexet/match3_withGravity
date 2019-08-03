using System;
using Match3.Interfaces;
using Match3.Scriptable;
using UnityEngine;

namespace Match3
{
    public class RegularBoard : MonoBehaviour, IBoard
    {
        public event Action BoardReadyEvent = delegate { };

        public void Create(BoardSettings boardSettings)
        {
            CreateGrid();
            PlaceElementsOnGrid();
        }

        private void PlaceElementsOnGrid()
        {
            throw new NotImplementedException();
        }

        private void CreateGrid()
        {
            throw new NotImplementedException();
        }

        public void TryToSwap()
        {
            if (CanSwap())
            {
                ApplySwap();
                RemoveSwapedMatches();
                HandleEmptyTiles();
                HandleNewMatches();
            }
        }

        private void RemoveSwapedMatches()
        {
            throw new NotImplementedException();
        }

        private void ApplySwap()
        {
            throw new NotImplementedException();
        }

        private bool CanSwap()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void HandleEmptyTiles()
        {
            FallElementsDownWithGravity();
            AddNewElements();
        }

        private void FallElementsDownWithGravity()
        {
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