using System.Collections.Generic;
using Core.Abstracts;
using UnityEngine;
using View;

namespace Models.Raycasts
{
    public abstract class RaycastHit : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private List<Transformable> _targetedObjects = new List<Transformable>();

        public RaycastHit Init(Camera camera)
        {
            _camera = camera;
            return this;
        }
        
        public RaycastHit RegisterTarget(Transformable target)
        {
            _targetedObjects.Add(target);
            return this;
        }

        public void UnregisterTarget(Transformable target)
        {
            _targetedObjects.Remove(target);
        }

        private void Update()
        {
            if (_targetedObjects.Count == 0)
            {
                return;
            }

            List<TransformableView> _possibleTargets = new List<TransformableView>();
            
            foreach (var target in _targetedObjects)
            {
                Ray ray = _camera.ScreenPointToRay(_camera.WorldToScreenPoint(target.Position));
                if (!Physics.Raycast(ray, out var targetHit))
                {
                    return;
                }

                if (targetHit.collider.TryGetComponent(out TransformableView hitted))
                {
                    _possibleTargets.Add(hitted);
                }
            }
            
            OnTargetsDetected(_possibleTargets);
        }

        public abstract void OnTargetsDetected(List<TransformableView> possibleTargets);
    }
}