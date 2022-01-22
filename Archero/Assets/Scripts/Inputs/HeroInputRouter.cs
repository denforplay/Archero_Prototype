﻿using System;
using Configurations;
using Cysharp.Threading.Tasks;
using Models.MainHero;
using Models.Weapons.Guns;
using UnityEngine;

namespace Inputs
{
    public class HeroInputRouter
    {
        private readonly HeroInput _heroInput;
        private readonly HeroMovement _heroMovement;
        private readonly WeaponConfiguration _weaponConfiguration;
        private DefaultGun _gun;
        private bool _isAttacking;

        public HeroInputRouter(HeroMovement heroMovement, WeaponConfiguration weaponConfiguration)
        {
            _heroMovement = heroMovement;
            _weaponConfiguration = weaponConfiguration;
            _heroInput = new HeroInput();
        }

        public void Update()
        {
            MoveHero(_heroInput.Hero.Movement.ReadValue<Vector2>());
            if (_heroMovement.IsStanding && !_isAttacking)
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
            while (_heroMovement.IsStanding)
            {
                OnGunShoot(_heroMovement.ShootDestination);
                await UniTask.Delay(TimeSpan.FromSeconds(_weaponConfiguration.FireRate));
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