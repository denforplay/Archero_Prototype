using System;
using System.Collections.Generic;
using System.Linq;
using Core.Abstracts;
using Models.Enemies;
using Models.MainHero;
using UnityEngine;
using View;

namespace Models.Raycasts
{
    public class HeroRaycastsHit : RaycastHit
    {
        private Hero _hero;
        public event Action<Vector3> OnShootDestinationChoosed;

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
                OnShootDestinationChoosed?.Invoke(closestEnemy.Model.Position);
            }
        }
    }
}