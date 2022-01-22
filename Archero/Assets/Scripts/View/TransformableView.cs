using Core.Abstracts;
using Core.Interfaces;
using Core.ObjectPool.Interfaces;
using UnityEngine;

namespace View
{
    public class TransformableView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Rigidbody _rigidbody;
        private Camera _camera;
        private Transformable _model;

        public Transformable Model => _model;
        
        public void Initialize(Transformable model, Camera camera)
        {
            _model = model;
            _camera = camera;
            _rigidbody.transform.position = model.Position;
        }

        private void FixedUpdate()
        {
            if (_model is IMovable movableModel)
            {
                _rigidbody.velocity = movableModel.Speed;
                if (movableModel.Speed != Vector2.zero)
                    transform.up = movableModel.Speed;
            }
            
        }

        public IObjectPool Origin { get; set; }
        public void OnReturningInPool()
        {
        }
    }
}