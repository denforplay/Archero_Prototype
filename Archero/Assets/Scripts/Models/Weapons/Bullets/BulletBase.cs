using Core.Abstracts;
using Core.Interfaces;
using UnityEngine;

namespace Models.Weapons.Bullets
{
    public abstract class Bullet : Transformable, IMovable
    {
        private float _lifeTime;
        private int _damage;
        private Transformable _parent;
        
        public int Damage => _damage;

        public Transformable Parent => _parent;
        
        protected Bullet(Transformable parent, Vector3 position, Vector3 rotation, float lifeTime, Vector2 speed, int damage) : base(position, rotation)
        {
            _lifeTime = lifeTime;
            Direction = speed;
            _damage = damage;
            _parent = parent;
        }

        public float LifeTime => _lifeTime;
        public Vector2 Direction { get; set; }

        public void Move(Vector3 delta)
        {
        }
    }
}