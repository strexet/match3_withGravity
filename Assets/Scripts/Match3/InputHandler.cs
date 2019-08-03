using Match3.Interfaces;
using UnityEngine;

namespace Match3
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        public bool TryingToSwap { get; private set;  }
        public void HandleInput()
        {
            throw new System.NotImplementedException();
        }
    }
}