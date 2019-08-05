using System;
using Match3.Scriptable;
using UnityEngine;

namespace Match3.Interfaces
{
    public interface IBoardController
    {
        event Action BoardReadyEvent;
        void CreateBoard(BoardSettings boardSettings);
        void HandleClick(GameObject clickPosition);
    }
}