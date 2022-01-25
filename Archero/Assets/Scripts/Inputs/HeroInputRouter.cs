using System;
using Configurations;
using Cysharp.Threading.Tasks;
using Models.MainHero;
using Models.Weapons.Guns;
using UnityEngine;

namespace Inputs
{
    public class HeroInputRouter
    {
        public event Action<Vector2> OnStartShootingEvent;
        
        private readonly HeroInput _heroInput;
        private readonly HeroMovement _heroMovement;
        private readonly WeaponConfiguration _weaponConfig;
        private DefaultGun _gun;
        private bool _isAttacking;
        private bool _isInitialized;

        public HeroInputRouter(HeroMovement heroMovement, WeaponConfiguration weaponConfig)
        {
            _heroMovement = heroMovement;
            _weaponConfig = weaponConfig;
            _heroInput = new HeroInput();
            _isInitialized = true;
        }

        public void Update()
        {
            MoveHero(_heroInput.Hero.Movement.ReadValue<Vector2>());
            if (_heroMovement.IsStanding && !_isAttacking && _isInitialized)
            {
                _isAttacking = true;
                ShootAsync();
            }
        }
        
        public HeroInputRouter BindGun(DefaultGun gun)
        {
            _gun = gun;
            return this;
        }
        
        private async void ShootAsync()
        {
            OnStartShootingEvent?.Invoke(_heroMovement.ShootDestination);
            while (_heroMovement.IsStanding)
            {
                OnGunShoot(_heroMovement.ShootDestination * _weaponConfig.ShootSpeed);
                await UniTask.Delay(TimeSpan.FromSeconds(_weaponConfig.FireRate));
            }

            _isAttacking = false;
        }

        public void OnEnable()
        {
            _heroInput.Enable();
            _heroInput.Hero.Shoot.performed += _ => OnGunShoot(new Vector2(0, 10f));//test!!!
        }

        public void OnDisable()
        {
            _heroInput.Disable();
        }

        private void OnGunShoot(Vector2 direction)
        {
            _gun.Shoot(direction);
        }
        
        private void MoveHero(Vector2 direction)
        {
            _heroMovement.Move(direction);
        }
    }
}