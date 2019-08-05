using UnityEngine;

namespace Match3.Scriptable
{
    [CreateAssetMenu(menuName = "Match3/Tile View Settings", fileName = "TileViewSettings", order = 0)]
    public class TileViewSettings : ScriptableObject
    {
        public TileView Prefab;
        public float TileSize;
        public Color[] ItemColors;
    }
}