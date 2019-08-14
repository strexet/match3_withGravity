using Match3.Board;
using UnityEngine;

namespace Match3.Interfaces
{
    public interface IBoardPositionConverter
    {
        Vector3 GetWorldPosition(PositionOnBoard positionOnBoard);
    }
}