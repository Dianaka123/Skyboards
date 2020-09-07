using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public interface IObjectPoolFactory<out T>
    {
        T Create();
    }

    public interface IObjectPool<T>
    {
        T Capture();

        void Release(T obj);
    }

    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly IObjectPoolFactory<T> factory;
        private readonly Stack<T> availableItems = new Stack<T>();
        private readonly HashSet<T> items = new HashSet<T>();
        private readonly int growValue;

        public ObjectPool(IObjectPoolFactory<T> factory, int growValue = 10)
        {
            this.factory = factory;
            this.growValue = growValue;
        }

        public T Capture()
        {
            TryGrow();
            return availableItems.Pop();
        }

        public void Release(T obj)
        {
            if (!items.Contains(obj))
            {
                throw new Exception("The pool doesn't own the object");
            }
            availableItems.Push(obj);
        }

        private bool TryGrow()
        {
            int currentCount = availableItems.Count;
            if (currentCount == 0)
            {
                for (int i = 0; i < growValue; i++)
                {
                    var item = factory.Create();
                    items.Add(item);
                    availableItems.Push(item);
                }
                return true;
            }
            return false;
        }

    }
}
