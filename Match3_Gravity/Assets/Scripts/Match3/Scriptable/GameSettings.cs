using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Match3.Scriptable
{
    [CreateAssetMenu(menuName = "Match3/Game Settings", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public BoardSettings BoardSettings;
    }

    [Serializable]
    public class BoardSettings
    {
        public int NumberOfRaws = 5;
        public int NumberOfColumns = 5;
    }
}