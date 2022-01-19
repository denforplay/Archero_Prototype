using DG.Tweening.Plugins.Options;
using Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class HeroInputRouter
    {
        private HeroInput _heroInput;
        private HeroMovement _heroMovement;

        public HeroInputRouter(HeroMovement heroMovement)
        {
            _heroInput = new HeroInput();
            _heroMovement = heroMovement;
        }

        public void Update()
        {
            MoveHero(_heroInput.Hero.Movement.ReadValue<Vector2>());
        }

        private void MoveHero(Vector2 direction)
        {
            _heroMovement.Move(direction);
        }
        
        public void OnEnable()
        {
            _heroInput.Enable();
        }

        public void OnDisable()
        {
            _heroInput.Disable();
        }
    }
}