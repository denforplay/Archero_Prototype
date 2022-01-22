using System.Collections.Generic;
using CompositeRoot;
using Core;
using Core.ObjectPool;
using Models.Collisions;
using UnityEngine;

namespace View.Factories
{
    public abstract class TransformableFactoryBase<T> : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CollisionCompositeRoot _collisionRoot;
        private ObjectPool<TransformableView> _entitiesPool;
        private Queue<TransformableView> _entitiesViews = new Queue<TransformableView>();
        public TransformableFactoryBase()
        {
            _entitiesPool = new ObjectPool<TransformableView>();
        }

        public TransformableView Create(Entity<T> entity)
        {
            TransformableView view = _entitiesPool.GetPrefabInstance(() => Instantiate(GetEntity(entity.GetEntity),
                entity.Transformable.Position,
                Quaternion.identity), (transformableView => transformableView.Initialize(entity.Transformable, _camera)));
            view.transform.SetParent(null);
            if (view.gameObject.TryGetComponent(out CollisionEvent collision))
            {
                collision.Initialize(_collisionRoot.Controller, _camera);
            }
            view.Initialize(entity.Transformable, _camera);
            _entitiesViews.Enqueue(view);
            return view;
        }
        
        public void Destroy(Entity<T> entity)
        {
                _entitiesPool.ReturnToPool(_entitiesViews.Dequeue());
        }

        protected abstract TransformableView GetEntity(T entity);
    }
}