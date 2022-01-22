using Core;
using Models.Enemies;
using Models.MainHero;
using Models.Systems;
using UnityEngine;
using View.Factories;

namespace CompositeRoot
{
    public class EnemiesCompositeRoot : CompositeRoot
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private HeroCompositeRoot _hero;
        [SerializeField] private HeroRaycast _heroRaycast;

        private EnemySystem _system;
        private EnemiesSpawner _spawner;

        public EnemySystem EnemySystem => _system;
        
        public override void Compose()
        {
            _system = new EnemySystem();
            _spawner = new EnemiesSpawner(_system, _camera);
            _system.OnStartEvent += SpawnEnemy;
            _system.OnStartEvent += RegisterEnemyToHero;
            _spawner.Spawn();
        }

        private void Update()
        {
            _system.UpdateSystem(Time.deltaTime);
        }

        private void OnDisable()
        {
            _system.OnStartEvent -= SpawnEnemy;
            _system.OnStartEvent -= RegisterEnemyToHero;
        }
        
        private void SpawnEnemy(Entity<EnemyBase> enemy)
        {
            _enemyFactory.Create(enemy);
        }

        private void RegisterEnemyToHero(Entity<EnemyBase> enemy)
        {
            _heroRaycast.RegisterEnemy(enemy.GetEntity);
        }
    }
}