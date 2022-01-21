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
        
        public void Shoot()
        {
                Bullet bullet = GetBullet();
                OnShotEvent?.Invoke(bullet);
        }

        private Bullet GetBullet() => new DefaultBullet(_shotPosition.position, Vector3.zero, 5f, 2f);
    }
}