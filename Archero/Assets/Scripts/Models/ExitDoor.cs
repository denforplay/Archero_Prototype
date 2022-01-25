using System;
using Models.MainHero;
using UnityEngine;
using View;

namespace Models
{
    public class ExitDoor : MonoBehaviour
    {
        public event Action OnHeroEnteredEvent;

        [SerializeField] private MeshRenderer _doorMesh;
        [SerializeField] private Material _doorActiveMaterial;
        [SerializeField] private Collider _doorCollider;

        private Material _doorInactiveMaterial;
        private void Start()
        {
            _doorCollider.enabled = false;
            _doorInactiveMaterial = _doorMesh.material;
        }

        public void ActivateDoor()
        {
            _doorMesh.material = _doorActiveMaterial;
            _doorCollider.enabled = true;
        }

        public void DeactivateDoor()
        {
            _doorMesh.material = _doorInactiveMaterial;
            _doorCollider.enabled = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent<TransformableView>(out var view))
            {
                if (view.Model is Hero)
                {
                    Debug.Log("Hero complete level");
                    OnHeroEnteredEvent?.Invoke();
                    DeactivateDoor();
                }
            }
        }
    }
}