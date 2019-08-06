using System;
using UnityEngine;

namespace Match3.Interfaces
{
    public interface IBoardController
    {
        event Action BoardReadyEvent;
        void CreateBoard();
        void HandleClick(GameObject clickPosition);
    }
}