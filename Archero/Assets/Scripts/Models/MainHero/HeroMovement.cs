using Core.Enums;
using UnityEngine;

namespace Models.MainHero
{
    public class HeroMovement
    {
        private readonly Hero _hero;
        private readonly HeroRaycast _raycast;

        public HeroMovement(Hero hero, HeroRaycast raycast)
        {
            _hero = hero;
            _raycast = raycast;
        }
        
        public bool IsStanding => _hero.State == MovableState.Standing;
        public Vector2 ShootDestination => _raycast.ShootDestination;
        
        public void Move(Vector2 delta)
        {
            _hero.Move(delta);
        }
    }
}