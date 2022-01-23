using System;
using Configurations;
using Cysharp.Threading.Tasks;
using Models.Enemies;
using Models.Enemies.EnemyStates;
using Models.Raycasts;
using Models.Weapons.Guns;
using UnityEngine;

namespace Inputs
{
    public class EnemyController
    {
        private readonly WeaponConfiguration _weaponConfiguration;
        private readonly HeroRaycastsHit _heroRaycast;
        private readonly EnemyBase _enemy;
        private IMovementState _movementState;
        private DefaultGun _gun;
        private bool _isAttacking;

        public EnemyController(EnemyBase enemy, DefaultGun gun, HeroRaycastsHit heroRaycast, WeaponConfiguration weaponConfiguration)
        {
            _enemy = enemy;
            _heroRaycast = heroRaycast;
            _weaponConfiguration = weaponConfiguration;
            _gun = gun;
            _movementState = new StandState(_enemy, _heroRaycast);
        }
        
        public void Update()
        {
            if (_enemy.Direction == Vector2.zero && !_isAttacking)
            {
                _isAttacking = true;
                ShootAsync();
            }

            if (_heroRaycast.CurrentTarget.Position == _enemy.Position)
            {
                _enemy.Move(_heroRaycast.ShootDestination);;
            }
        }
        
        private async void ShootAsync()
        {
            while (_movementState is StandState)
            {
                OnGunShoot((_heroRaycast.transform.position - _enemy.Position).normalized);
                await UniTask.Delay(TimeSpan.FromSeconds(_weaponConfiguration.FireRate));
            }

            _isAttacking = false;
        }

        private void OnGunShoot(Vector2 direction)
        {
            _gun.Shoot(direction * _weaponConfiguration.ShootSpeed);
        }

        public EnemyController BindGun(DefaultGun gun)
        {
            _gun = gun;
            return this;
        }
    }
}