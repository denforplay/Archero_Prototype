using Configurations;
using Core.Abstracts;
using Core.Enums;
using Core.Interfaces;
using UnityEngine;

namespace Models.MainHero
{
    public class Hero : Transformable, IMovable
    {
        private HeroConfiguration _heroConfig;
        
        public Hero(Vector3 position, Vector3 rotation, HeroConfiguration heroConfig) : base(position, rotation)
        {
            _heroConfig = heroConfig;
        }

        public MovableState State { get; set; }
        public Vector2 Speed { get; set; }

        public void Move(Vector2 delta)
        {
            Speed = delta * _heroConfig.Speed;
            if (Speed == Vector2.zero)
                State = MovableState.Standing;
            else
                State = MovableState.Moving;
        }
    }
}