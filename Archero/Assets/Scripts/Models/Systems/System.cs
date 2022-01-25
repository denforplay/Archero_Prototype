using System;
using System.Collections.Generic;
using Core;
using System.Linq;

namespace Models.Systems
{
    public abstract class System<T>
    {
        public event Action<Entity<T>> OnStartEvent;
        public event Action<Entity<T>> OnEndEvent;
        
        private List<Entity<T>> _entities = new List<Entity<T>>();
        public List<Entity<T>> Entities => _entities;

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

        public void StopWork(T entityModel)
        {
            var entity = _entities.Find(x => x.GetEntity.Equals(entityModel));
            StopWorkEntity(entity);
        }

        public void StopAllWork()
        {
            List<Entity<T>> entities = new List<Entity<T>>(_entities);
            entities.ForEach(x => StopWorkEntity(x));
        }
    }
}