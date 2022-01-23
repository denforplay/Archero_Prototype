using System;
using System.Collections.Generic;
using Core;

namespace Models.Systems
{
    public abstract class System<T>
    {
        public event Action<Entity<T>> OnStartEvent;
        public event Action<Entity<T>> OnEndEvent;
        
        private ICollection<Entity<T>> _entities = new List<Entity<T>>();
        public ICollection<Entity<T>> Entities => _entities;

        public abstract void UpdateSystem(float deltaTime);

        protected void Work(Entity<T> systemEntity)
        {
            _entities.Add(systemEntity);
            OnStartEvent?.Invoke(systemEntity);
        }

        protected void StopWorkEntity(Entity<T> systemEntity)
        {
            _entities.Remove(systemEntity);
            OnEndEvent?.Invoke(systemEntity);
        }

        public void Stop()
        {
            foreach (var systemEntity in _entities)
            {
                StopWorkEntity(systemEntity);
            }
        }
    }
}