using System;
using System.Collections.Generic;
using System.Linq;
using Core.Abstracts;
using Models.Enemies;
using UnityEngine;
using View;

namespace Models.Raycasts
{
    public class HeroRaycastsHit : RaycastHit
    {
        public event Action<Vector3> OnShootDestinationChoosed;
        
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
                    if ((enemy.Model.Position - transform.position).sqrMagnitude < (closestEnemy.Model.Position - transform.position).sqrMagnitude)
                        closestEnemy = enemy;
                }
                
                Debug.DrawLine(transform.position, closestEnemy.Model.Position);
                ShootDestination = (closestEnemy.Model.Position - transform.position).normalized;
                CurrentTarget = closestEnemy.Model as EnemyBase;
                OnShootDestinationChoosed?.Invoke(closestEnemy.Model.Position);
            }
        }
    }
}