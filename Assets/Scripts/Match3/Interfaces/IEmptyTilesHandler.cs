namespace Match3.Interfaces
{
    public interface IEmptyTilesHandler
    {
        bool HasEmptyTiles();
        void HandleEmptyTilesStep();
    }
}