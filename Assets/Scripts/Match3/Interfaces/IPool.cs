namespace Match3.Interfaces
{
    public interface IPool<T> where T : IPoolable
    {
        T Get();
        void Return(T item);
    }
}