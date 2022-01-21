using Core.Abstracts;
using UnityEngine;

namespace Models.Weapons.Bullets
{
    public abstract class Bullet : Transformable
    {
        private float _lifeTime;
        private float _speed;

        public float LifeTime => _lifeTime;
        public float Speed => _speed;

        protected Bullet(Vector3 position, Vector3 rotation, float lifeTime, float speed) : base(position, rotation)
        {
        }
    }
}