using Core.Abstracts;
using Core.Interfaces;
using Core.ObjectPool.Interfaces;
using UnityEngine;

namespace Models.Weapons.Bullets
{
    public abstract class Bullet : Transformable, IMovable
    {
        private float _lifeTime;

        protected Bullet(Vector3 position, Vector3 rotation, float lifeTime, Vector2 speed) : base(position, rotation)
        {
            _lifeTime = lifeTime;
            Direction = speed;
        }

        public float LifeTime => _lifeTime;
        public Vector2 Direction { get; set; }

        public void Move(Vector2 delta)
        {
        }
    }
}