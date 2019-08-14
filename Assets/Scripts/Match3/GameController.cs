using Match3.Enums;
using Match3.Interfaces;
using Match3.Scriptable;
using UnityEngine;

namespace Match3
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        
        private IBoardController _boardController;
        private IInputHandler _inputHandler;
        
        private GameAction _gameAction;
        
        private void Awake()
        {
            _boardController = GetComponent<IBoardController>();
            _inputHandler = GetComponent<IInputHandler>();
        }

        private void Start()
        {
            _gameAction = GameAction.Playing;
            
            _boardController.CreateBoard(_gameSettings.BoardSettings);
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
            if (_inputHandler.DidClickHappen())
            {
                var clickedObject = _inputHandler.GetClickedObject();
                _boardController.HandleClick(clickedObject);
            }
        }
    }
}

