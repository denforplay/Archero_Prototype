using Core.Abstracts;
using UnityEngine;

namespace Models.Weapons.Bullets
{
    public abstract class Bullet : Transformable
    {
        private float _lifeTime;

        public float LifeTime => _lifeTime;

        protected Bullet(Vector3 position, Vector3 rotation, float lifeTime, Vector2 speed) : base(position, rotation)
        {
            Speed = speed;
        }
    }
}