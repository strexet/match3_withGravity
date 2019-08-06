namespace Match3
{
    public interface IBoard
    {
        bool IsTileOnBoard(PositionOnBoard position);
        Tile GetTileAt(PositionOnBoard position);
        void SetTileAt(Tile tile, PositionOnBoard position);
    }
}