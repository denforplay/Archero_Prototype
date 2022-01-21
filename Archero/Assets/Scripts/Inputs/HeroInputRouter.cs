using DG.Tweening.Plugins.Options;
using Models;
using Models.Weapons.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class HeroInputRouter
    {
        private HeroInput _heroInput;
        private HeroMovement _heroMovement;
        private DefaultGun _gun;

        public HeroInputRouter(HeroMovement heroMovement)
        {
            _heroInput = new HeroInput();
            _heroMovement = heroMovement;
        }

        public void Update()
        {
            MoveHero(_heroInput.Hero.Movement.ReadValue<Vector2>());
        }

        public void OnEnable()
        {
            _heroInput.Enable();
            _heroInput.Hero.Shoot.performed += OnGunShoot;
        }

        public void OnDisable()
        {
            _heroInput.Disable();
        }

        private void OnGunShoot(InputAction.CallbackContext context)
        {
            Debug.Log("Shoot");
            _gun.Shoot();
        }
        
        private void MoveHero(Vector2 direction)
        {
            _heroMovement.Move(direction);
        }

        public HeroInputRouter BindGun(DefaultGun gun)
        {
            _gun = gun;
            return this;
        }
    }
}