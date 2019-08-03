using System;
using Match3.Scriptable;

namespace Match3.Interfaces
{
    public interface IBoard
    {
        event Action BoardReadyEvent;
        void Create(BoardSettings boardSettings);
        void TryToSwap();
    }
}