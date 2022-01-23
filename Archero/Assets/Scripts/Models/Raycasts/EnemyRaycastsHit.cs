using System;
using System.Collections.Generic;
using System.Linq;
using Models.MainHero;
using UnityEngine;
using View;

namespace Models.Raycasts
{
    public class EnemyRaycastsHit : RaycastHit
    {
        public event Action<Vector3> OnShootDestinationChoosed;

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
                OnShootDestinationChoosed?.Invoke(closestHero.Model.Position);
            }
        }
    }
}