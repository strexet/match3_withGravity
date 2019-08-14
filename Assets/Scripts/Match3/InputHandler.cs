using Match3.Interfaces;
using UnityEngine;

namespace Match3
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        private const float ClickMaxDistanceFromCamera = 50f;
        private const float ClickIntervalThreshold = 0.2f;

        [SerializeField] private LayerMask ClickableLayers;
        
        private Camera _mainCamera;
        private float _nextClickDetectionTime;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public GameObject GetClickedObject()
        {
            var currentTime = Time.time;
            if (currentTime > _nextClickDetectionTime)
            {
                _nextClickDetectionTime = currentTime + ClickIntervalThreshold;
                var ray = _mainCamera.ScreenPointToRay (Input.mousePosition);
                if (Physics.Raycast (ray, out var hit, ClickMaxDistanceFromCamera, ClickableLayers)) 
                {
                    Debug.Log("click", hit.collider.gameObject);
                    return hit.collider.gameObject;
                }  
            }
            
            return null;
        }

        public bool DidClickHappen()
        {
            return Input.GetMouseButton(0);
        }
    }
}