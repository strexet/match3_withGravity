using Match3.Interfaces;
using UnityEngine;

namespace Match3.Board
{
    public class BoardPositionConverter : IBoardPositionConverter
    {
        private readonly float _tileSize;

        public BoardPositionConverter(float tileSize)
        {
            _tileSize = tileSize;
        }

        public Vector3 GetWorldPosition(PositionOnBoard positionOnBoard)
        {
            return new Vector3(positionOnBoard.x, positionOnBoard.y, 0) * _tileSize;
        }
    }
}