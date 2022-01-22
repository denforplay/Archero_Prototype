using System;
using Core.Abstracts;
using Models.Weapons.Bullets;
using UnityEngine;

namespace Models.Weapons.Guns
{
    public class DefaultGun
    {
        private Transform _shotPosition;

        public DefaultGun(Transform shotPosition)
        {
            _shotPosition = shotPosition;
        }
        
        public event Action<Bullet> OnShotEvent;
        
        public void Shoot(Vector2 direction)
        {
                Bullet bullet = GetBullet(direction);
                OnShotEvent?.Invoke(bullet);
        }

        private Bullet GetBullet(Vector2 direction) => new DefaultBullet(_shotPosition.position, Vector3.zero, 5f, direction);
    }
}