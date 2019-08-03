
using Match3.Enums;
using Match3.Interfaces;
using Match3.Scriptable;
using UnityEngine;

namespace Match3
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;

        private IBoard _board;
        private IInputHandler _inputHandler;
        
        private GameAction _gameAction;
        
        private void Awake()
        {
            _board = GetComponent<IBoard>();
            _inputHandler = GetComponent<IInputHandler>();
        }

        private void Start()
        {
            _gameAction = GameAction.WaitingForAnimationToEnd;
            
            _board.Create(gameSettings.BoardSettings);
//            _board.BoardReadyEvent += ...
        }

        private void Update()
        {
            if (!IsGameReady())
            {
                return;
            }
                
            UpdateInput();
        }
        
        private bool IsGameReady()
        {
            if (_gameAction == GameAction.WaitingForAnimationToEnd)
            {
                return false;
            }

            return true;
        }
        
        private void UpdateInput()
        {
            _inputHandler.HandleInput();

            if (_inputHandler.TryingToSwap)
            {
                _board.TryToSwap();
            }
        }

        

        

        

        
    }
}

