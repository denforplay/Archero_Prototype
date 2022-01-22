using System;
using System.Collections.Generic;
using System.Linq;
using Core.Abstracts;
using Models.Enemies;
using UnityEngine;
using View;

namespace Models
{
    public class HeroRaycast : MonoBehaviour
    {
        public event Action<Vector3> OnChoosedDestinationToShoot;
        
        [SerializeField] private Camera _camera;
        private List<EnemyBase> _enemies = new List<EnemyBase>();

        public void RegisterEnemy(EnemyBase enemy)
        {
            _enemies.Add(enemy);
        }
        
        private void Update()
        {
            if (_enemies.Count == 0)
            {
                Debug.Log("Empty enemies");
                return;
            }
            
            List<EnemyBase> possibleEnemies = new List<EnemyBase>();

            foreach (var enemy in _enemies)
            {
                Ray ray = _camera.ScreenPointToRay(_camera.WorldToScreenPoint(enemy.Position));
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit))
                {
                    return;
                }

                if (hit.collider.TryGetComponent(out TransformableView hitted))
                {
                    if (hitted.Model is EnemyBase hittedEnemy)
                    {
                        possibleEnemies.Add(hittedEnemy);
                    }
                }
            }

            if (possibleEnemies.Count != 0)
            {
                var closestEnemy = possibleEnemies[0];
                foreach (var enemy in possibleEnemies)
                {
                    if ((enemy.Position - transform.position).sqrMagnitude < (closestEnemy.Position - transform.position).sqrMagnitude)
                        closestEnemy = enemy;
                }
                
                Debug.DrawLine(transform.position, closestEnemy.Position);
                
                OnChoosedDestinationToShoot?.Invoke(closestEnemy.Position);
            }
        }
    }
}