using Configurations;
using Core.Abstracts;
using Core.Interfaces;
using UnityEngine;

namespace Models
{
    public class Hero : Transformable, IMovable
    {
        private HeroConfiguration _heroConfig;
        
        
        public Hero(Vector3 position, Vector3 rotation, HeroConfiguration heroConfig) : base(position, rotation)
        {
            _heroConfig = heroConfig;
        }
        
        public void Move(Vector2 delta)
        {
            Speed = delta * _heroConfig.Speed;
        }
    }
}