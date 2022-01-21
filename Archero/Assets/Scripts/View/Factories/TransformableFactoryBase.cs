using CompositeRoot;
using Core;
using Core.Abstracts;
using Models.Collisions;
using UnityEngine;

namespace View.Factories
{
    public abstract class TransformableFactoryBase<T> : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CollisionCompositeRoot _collisionRoot;

        public TransformableView Create(Entity<T> entity)
        {
            Debug.Log("Bullet created");
            TransformableView view = Instantiate(GetEntity(entity.GetEntity), entity.Transformable.Position,
                Quaternion.identity);
            view.transform.SetParent(null);
            if (view.gameObject.TryGetComponent(out CollisionEvent collision))
            {
                collision.Initialize(_collisionRoot.Controller, _camera);
            }
            view.Initialize(entity.Transformable, _camera);

            return view;
        }

        protected abstract TransformableView GetEntity(T entity);
    }
}