using System.Collections.Generic;
using Configurations;
using Core;
using Inputs;
using Models.Enemies;
using Models.Raycasts;
using Models.Systems;
using Models.Weapons.Bullets;
using Models.Weapons.Guns;
using UnityEngine;
using View;
using View.Factories;

namespace CompositeRoot
{
    public class EnemiesCompositeRoot : CompositeRoot
    {
        [SerializeField] private WeaponConfiguration _weaponConfig;
        [SerializeField] private EnemyConfiguration _enemyConfig;
        [SerializeField] private Camera _camera;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private HeroCompositeRoot _heroRoot;
        [SerializeField] private HeroRaycastsHit _heroRaycast;
        [SerializeField] private BulletFactory _bulletFactory;
        
        private EnemySystem _system;
        private EnemiesSpawner _spawner;
        private BulletSystem _bulletSystem;
        private List<DefaultGun> _enemyGuns;
        private List<EnemyController> _enemyControllers = new List<EnemyController>();
        public EnemySystem EnemySystem => _system;

        public override void Compose()
        {
            _enemyGuns = new List<DefaultGun>();
            _system = new EnemySystem();
            _spawner = new EnemiesSpawner(_system, _enemyConfig, _camera);
            _bulletSystem = new BulletSystem();
        }

        private void Update()
        {
            _system.UpdateSystem(Time.deltaTime);
            _enemyControllers.ForEach(controller => controller.Update());
        }

        private void OnEnable()
        {
            _system.OnStartEvent += SpawnEnemy;
            _system.OnStartEvent += RegisterEnemyToHero;
            _bulletSystem.OnStartEvent += SpawnBullet;
            _bulletSystem.OnEndEvent += DeleteBullet;
            _spawner.Spawn();
        }

        private void OnDisable()
        {
            _system.OnStartEvent -= SpawnEnemy;
            _system.OnStartEvent -= RegisterEnemyToHero;
            _bulletSystem.OnStartEvent -= SpawnBullet;
            _bulletSystem.OnEndEvent -= DeleteBullet;
        }
        
        private void SpawnBullet(Entity<Bullet> bullet)
        {
            _bulletFactory.Create(bullet);
        }
        
        private void DeleteBullet(Entity<Bullet> bullet)
        {
            _bulletFactory.Destroy(bullet);
        }

        private void SpawnEnemy(Entity<EnemyBase> enemy)
        {
            var view = _enemyFactory.Create(enemy);
            var component = view.gameObject.AddComponent<EnemyRaycastsHit>();
            component.Init(_camera).RegisterTarget(_heroRoot.Model);
            if (view.TryGetComponent<HealthPointsView>(out var healthView))
            {
                healthView.Initialize(enemy.GetEntity);
            }
            DefaultGun gun = new DefaultGun(view.transform);
            _enemyControllers.Add(new EnemyController(enemy.GetEntity, gun, _heroRaycast, _weaponConfig));
            gun.OnShotEvent += (bullet) => Shoot(enemy.GetEntity, bullet, component.ClosestPosition);
            _enemyGuns.Add(gun);
        }
        
        private void Shoot(EnemyBase enemy, Bullet bullet, Vector2 direction)
        {
            _bulletSystem.Work(bullet, enemy.Position, direction);
        }
        
        private void RegisterEnemyToHero(Entity<EnemyBase> enemy)
        {
            _heroRaycast.RegisterTarget(enemy.GetEntity);
        }
    }
}