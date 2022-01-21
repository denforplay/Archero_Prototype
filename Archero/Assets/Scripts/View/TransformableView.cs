using System;
using Core.Abstracts;
using UnityEngine;

namespace View
{
    public class TransformableView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private Camera _camera;
        private Transformable _model;

        public Transformable Model => _model;
        
        public void Initialize(Transformable model, Camera camera)
        {
            _model = model;
            _camera = camera;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _model.Speed;
            if (_model.Speed != Vector2.zero)
                transform.up = _model.Speed;
        }
    }
}