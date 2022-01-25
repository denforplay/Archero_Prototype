using System;
using System.Collections.Generic;
using Configurations;
using Core;
using Core.PopupSystem;
using Inputs;
using Models;
using Models.Enemies;
using Models.Raycasts;
using Models.Systems;
using Models.Weapons.Bullets;
using Models.Weapons.Guns;
using UnityEngine;
using View;
using View.Factories;
using View.Popups;

namespace CompositeRoot
{
    public class EnemiesCompositeRoot : CompositeRoot
    {
        public event Action OnRestartEvent;
        
        [SerializeField] private WeaponConfiguration _weaponConfig;
        [SerializeField] private EnemyConfiguration _enemyConfig;
        [SerializeField] private Camera _camera;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private HeroCompositeRoot _heroRoot;
        [SerializeField] private HeroRaycastsHit _heroRaycast;
        [SerializeField] private ExitDoor _exitDoor;
        [SerializeField] private PopupSystem _popupSystem;
        
        private EnemySystem _enemySystem;
        private EnemiesSpawner _spawner;
        private BulletSystem _bulletSystem;
        private List<EnemyController> _enemyControllers = new List<EnemyController>();
        public EnemySystem EnemyEnemySystem => _enemySystem;

        public override void Compose()
        {
            _enemySystem = new EnemySystem();
            _spawner = new EnemiesSpawner(_enemySystem, _enemyConfig, _camera);
            _bulletSystem = _heroRoot.BulletSystem;
            _exitDoor.OnHeroEnteredEvent += OnAllEnemiesDestroyed;
        }
        
        public void StartRoot()
        {
            _enemySystem.OnStartEvent += SpawnEnemy;
            _enemySystem.OnStartEvent += RegisterEnemyToHero;
            _enemySystem.OnEndEvent +=_enemyFactory.Destroy;
            _spawner.Spawn();
        }

        public void Restart()
        {
            _enemySystem.StopAllWork();
            _exitDoor.DeactivateDoor();
            _spawner.Spawn();
        }

        private void OnAllEnemiesDestroyed()
        {
            var popup = _popupSystem.SpawnPopup<LevelCompletePopup>();
            popup.OnRestartEvent += () =>
            {
                _popupSystem.DeletePopUp();
                OnRestartEvent?.Invoke();
            };
        }

        private void Update()
        {
            _enemySystem.UpdateSystem(Time.deltaTime);
            _enemyControllers.ForEach(controller => controller.Update());
        }

        private void OnDisable()
        {
            _enemySystem.OnStartEvent -= SpawnEnemy;
            _enemySystem.OnStartEvent -= RegisterEnemyToHero;
            _enemySystem.OnEndEvent -= _enemyFactory.Destroy;
        }
        
        private void SpawnEnemy(Entity<EnemyBase> enemy)
        {
            var view = _enemyFactory.Create(enemy);
            var component = view.gameObject.AddComponent<EnemyRaycastsHit>();
            component.Init(_camera, view).RegisterTarget(_heroRoot.Model);
            if (view.TryGetComponent<HealthPointsView>(out var healthView))
            {
                healthView.Initialize(enemy.GetEntity);
            }
            
            DefaultGun gun = new DefaultGun(view.Model, _weaponConfig);
            var enemyController = new EnemyController(enemy.GetEntity, gun, _heroRaycast, _weaponConfig);
            _enemyControllers.Add(enemyController);
            gun.OnShotEvent += (bullet) => Shoot(enemy.GetEntity, bullet, component.ClosestPosition);
            enemy.GetEntity.OnHealthChanged += health =>
            {
                if (health < 0)
                {
                    _enemySystem.StopWork(enemy.GetEntity);
                    _enemyControllers.Remove(enemyController);
                    enemyController.OnDisable();
                    if (_enemySystem.Entities.Count == 0)
                        _exitDoor.ActivateDoor();
                    
                    _heroRoot.Model.AddCoins(UnityEngine.Random.Range(5, 25));
                }
            };
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