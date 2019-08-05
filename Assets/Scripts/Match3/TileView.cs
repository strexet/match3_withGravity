using System;
using Match3.Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

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
                Color = GetColorFromIndex(_associatedTile.ItemIndex);
                _associatedTile.OnItemIndexChangedEvent += OnAssociatedTileItemIndexChanged;
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
            _associatedTile.OnItemIndexChangedEvent -= OnAssociatedTileItemIndexChanged;
        }

        public void OnAssociatedTileItemIndexChanged(int newIndex)
        {
            Color = GetColorFromIndex(newIndex);
        }
    }
}