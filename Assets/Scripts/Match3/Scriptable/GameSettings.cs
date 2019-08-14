using System;
using UnityEngine;

namespace Match3.Scriptable
{
    [CreateAssetMenu(menuName = "Match3/Game Settings", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public BoardSettings BoardSettings;
        public ItemViewSettings ItemViewSettings;
    }

    [Serializable]
    public class BoardSettings
    {
        public int NumberOfRaws = 5;
        public int NumberOfColumns = 5;
    }
}