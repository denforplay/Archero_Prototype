using System;
using System.Collections.Generic;
using System.Linq;
using Core.Abstracts;
using Core.Interfaces;
using Models.Enemies;
using Models.MainHero;
using UnityEngine;
using View;

namespace Models.Raycasts
{
    public class HeroRaycastsHit : RaycastHit
    {
        public event Action<Vector3> OnShootDestinationChoosed;

        [SerializeField] private TransformableView _transformableView;
        private Hero _hero;
        public void Initialize(Hero hero)
        {
            _hero = hero;
        }
        
        public Vector3 ShootDestination { get; set; }
        public Transformable CurrentTarget { get; set; }
        
        public override void OnTargetsDetected(List<TransformableView> possibleTargets)
        {
            List<TransformableView> possibleEnemies = possibleTargets.Where(target => target.Model is EnemyBase).ToList();
            
            if (possibleEnemies.Count() != 0)
            {
                var closestEnemy = possibleEnemies[0];
                foreach (var enemy in possibleEnemies)
                {
                    if ((enemy.Model.Position - _hero.Position).sqrMagnitude < (closestEnemy.Model.Position - _hero.Position).sqrMagnitude)
                        closestEnemy = enemy;
                }
                
                Debug.DrawLine(_hero.Position, closestEnemy.Model.Position);
                ShootDestination = (closestEnemy.Model.Position - _hero.Position).normalized;
                CurrentTarget = closestEnemy.Model as EnemyBase;
                if ((_transformableView.Model is IMovable movableModel) && movableModel.Direction == Vector2.zero)
                    _transformableView.Rigidbody.transform.up =
                        Vector2.MoveTowards(_transformableView.Rigidbody.transform.up, ShootDestination, Time.deltaTime * 5); 
                OnShootDestinationChoosed?.Invoke(closestEnemy.Model.Position);
            }
        }
    }
}