using System.Collections.Generic;
using Match3.Interfaces;

namespace Match3.Items.Pool
{
    public abstract class AbstractPool<T> : IPool<T> where T : IPoolable
    {
        private readonly Queue<T> _pool;
        
        protected AbstractPool()
        {
            _pool = new Queue<T>();
        }

        protected AbstractPool(int size)
        {
            _pool = new Queue<T>(size);

            for (int i = 0; i < size; i++)
            {
                var poolable = CreateElement();
                _pool.Enqueue(poolable);
            }
        }

        public T Get()
        {
            T result;
            if (_pool.Count == 0)
            {
                result = CreateElement();
            }
            else
            {
                result = _pool.Dequeue();
            }
            result.OnGet();
            return result;
        }

        public void Return(T poolable)
        {
            poolable.OnReturn();
            _pool.Enqueue(poolable);
        }

        protected abstract T CreateElement();
    }
}