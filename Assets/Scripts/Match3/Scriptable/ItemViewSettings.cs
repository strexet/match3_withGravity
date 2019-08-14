using System;
using Match3.Items;
using UnityEngine;

namespace Match3.Scriptable
{
    [CreateAssetMenu(menuName = "Match3/Tile View Settings", fileName = "TileViewSettings", order = 0)]
    public class ItemViewSettings : ScriptableObject
    {
        public float TileSize = 1.1f;
        public ItemView[] ItemPrefabs;
        public ModifierViews ModifierViewPrefabs;
    }

    [Serializable]
    public class ModifierViews
    {
        public GameObject GravityModifier;
    }
}