using Match3.Scriptable;
using UnityEngine;

namespace Match3
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private TileViewSettings _tileViewSettings;
        private Material _material;
        private Tile _associatedTile;

        public Tile AssociatedTile
        {
            get { return _associatedTile; }
            set
            {
                _associatedTile = value;
                _associatedTile.OnTileStateChangedEvent += OnAssociatedTileStateStateChanged;
                OnAssociatedTileStateStateChanged(_associatedTile.ItemIndex);
            }
        }

        private Color GetColorFromIndex(int index)
        {
            return _tileViewSettings.ItemColors[index];
        }

        private Color Color
        {
            set => _material.color = value;
        }

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        private void OnDisable()
        {
            _associatedTile.OnTileStateChangedEvent -= OnAssociatedTileStateStateChanged;
        }

        public void OnAssociatedTileStateStateChanged(int newIndex)
        {
            if (_associatedTile.IsEmptyTile)
            {
                gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);
            Color = GetColorFromIndex(newIndex);
        }
    }
}