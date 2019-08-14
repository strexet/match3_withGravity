using System.Collections.Generic;
using Match3.Board;

namespace Match3.Interfaces
{
    public interface IBoard : IEnumerable<Tile>
    {
        bool IsTileOnBoard(PositionOnBoard position);
        Tile GetTileAt(PositionOnBoard position);
        void SetTileAt(PositionOnBoard position, Tile tile);
        bool AreNeighbourTiles((Tile, Tile) tilesBeingSwapped);
    }
}