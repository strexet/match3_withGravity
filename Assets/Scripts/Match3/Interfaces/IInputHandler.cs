namespace Match3.Interfaces
{
    public interface IInputHandler
    {
        bool TryingToSwap { get; }
        void HandleInput();
        
    }
}