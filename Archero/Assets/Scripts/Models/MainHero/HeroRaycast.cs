using System;
using System.Collections.Generic;
using Models.Enemies;
using UnityEngine;
using View;

namespace Models.MainHero
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
        
        public Vector2 ShootDestination { get; set; }
        
        private void Update()
        {
            if (_enemies.Count == 0)
            {
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
                ShootDestination = (closestEnemy.Position - transform.position).normalized;
                OnChoosedDestinationToShoot?.Invoke(closestEnemy.Position);
            }
        }
    }
}