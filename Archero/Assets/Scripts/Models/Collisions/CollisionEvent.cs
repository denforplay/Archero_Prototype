using System;
using UnityEngine;

namespace Models.Collisions
{
    public class CollisionEvent : MonoBehaviour
    {
        private CollisionController _collisionController;
        private object _model;

        public void Initialize(CollisionController controller, object model)
        {
            _collisionController = controller;
            _model = model;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out CollisionEvent collisionEvent))
            {
                _collisionController.TryCollide((_model, collisionEvent._model));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollisionEvent collisionEvent))
            {
                _collisionController.TryCollide((_model, collisionEvent._model));
            }
        }
    }
}