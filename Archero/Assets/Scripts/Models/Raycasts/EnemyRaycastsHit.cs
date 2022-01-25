using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Models.MainHero;
using UnityEngine;
using View;

namespace Models.Raycasts
{
    public class EnemyRaycastsHit : RaycastHit
    {
        [SerializeField] private TransformableView _transformableView;
        public event Action<Vector3> OnShootDestinationChoosed;
        
        public new RaycastHit Init(Camera camera, TransformableView transformableView)
        {
            _transformableView = transformableView;
            base.Init(camera);
            return this;
        }

        public Vector3 ClosestPosition { get; set; }
        
        public override void OnTargetsDetected(List<TransformableView> possibleTargets)
        {
            List<TransformableView> possibleHeroes = possibleTargets.Where(target => target.Model is Hero).ToList();
            
            if (possibleHeroes.Count() != 0)
            {
                var closestHero = possibleHeroes[0];
                foreach (var hero in possibleHeroes)
                {
                    if ((hero.Model.Position - transform.position).sqrMagnitude <
                        (closestHero.Model.Position - transform.position).sqrMagnitude)
                        closestHero = hero;
                }
                Debug.DrawLine(transform.position, closestHero.Model.Position, Color.yellow);
                ClosestPosition = closestHero.Model.Position;
                if ((_transformableView.Model is IMovable movableModel) && movableModel.Direction == Vector2.zero)
                    _transformableView.Rigidbody.transform.up =
                        Vector2.MoveTowards(_transformableView.Rigidbody.transform.up, (ClosestPosition - transform.position).normalized, Time.deltaTime * 5); 
                OnShootDestinationChoosed?.Invoke(closestHero.Model.Position);
            }
        }
    }
}