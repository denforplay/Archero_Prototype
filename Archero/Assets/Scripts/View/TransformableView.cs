using Core.Abstracts;
using UnityEngine;

namespace View
{
    public class TransformableView : MonoBehaviour
    {
        private Camera _camera;
        private Transformable _transformable;

        public void Initialize(Transformable transformable, Camera camera)
        {
            _transformable = transformable;
            _camera = camera;
        }
        
        private void LateUpdate()
        {
            transform.position = _transformable.Position;
        }
    }
}