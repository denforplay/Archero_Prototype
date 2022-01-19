using UnityEngine;

namespace Models
{
    public class HeroMovement
    {
        private readonly Hero _hero;

        public HeroMovement(Hero hero)
        {
            _hero = hero;
        }

        public void Move(Vector2 delta)
        {
            _hero.Move(delta);
        }
    }
}