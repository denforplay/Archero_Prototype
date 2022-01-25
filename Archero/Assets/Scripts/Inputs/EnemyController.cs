using System;
using Configurations;
using Core.Abstracts;
using Cysharp.Threading.Tasks;
using Models.Enemies;
using Models.Enemies.States;
using Models.Raycasts;
using Models.Weapons.Guns;
using UnityEngine;

namespace Inputs
{
    public class EnemyController : IStateController
    {
        private IState _movementState;
        private readonly WeaponConfiguration _weaponConfiguration;
        private readonly HeroRaycastsHit _heroRaycast;
        private readonly EnemyBase _enemy;
        private DefaultGun _gun;
        private bool _isAttacking;
        
        public Transformable ControlledObject { get => _enemy; }
        public EnemyBase Enemy => _enemy;

        public EnemyController(EnemyBase enemy, DefaultGun gun, HeroRaycastsHit heroRaycast, WeaponConfiguration weaponConfiguration)
        {
            _enemy = enemy;
            _heroRaycast = heroRaycast;
            _weaponConfiguration = weaponConfiguration;
            _gun = gun;
            _movementState = new StandingState(5f);
            _movementState.Owner = this;
        }
        
      
        
        public void Update()
        {
            if (_movementState != null)
            {
                _movementState.UpdateState(Time.deltaTime);
            }

            if (_movementState is StandingState && !_isAttacking)
            {
                _isAttacking = true;
                ShootAsync();
            }
        }
        
        public void ChangeState(IState newState)
        {
            _movementState = newState;
            _movementState.Owner = this;
            _movementState.OnEnterInState();
        }
        
        public EnemyController BindGun(DefaultGun gun)
        {
            _gun = gun;
            return this;
        }
        
        private async void ShootAsync()
        {
            while (_movementState is StandingState)
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

        public void OnDisable()
        {
            _isAttacking = false;
        }
    }
}