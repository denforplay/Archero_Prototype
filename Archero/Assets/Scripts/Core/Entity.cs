using Core.Abstracts;

namespace Core
{
    public sealed class Entity<T>
    {
        private T _entity;
        private Transformable _transformable;
        public T GetEntity => _entity;
        public Transformable Transformable => _transformable;
        public Entity(T entity, Transformable transformable)
        {
            _entity = entity;
            _transformable = transformable;
        }
    }
}