using System;
using Configurations;
using Core.Abstracts;
using Models.Weapons.Bullets;
using UnityEngine;

namespace Models.Weapons.Guns
{
    public class DefaultGun
    {
        private WeaponConfiguration _weaponConfig;
        private Transformable _parent;

        public DefaultGun(Transformable parent, WeaponConfiguration weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _parent = parent;
        }
        
        public event Action<Bullet> OnShotEvent;
        
        public void Shoot(Vector2 direction)
        {
                Bullet bullet = GetBullet(direction);
                OnShotEvent?.Invoke(bullet);
        }

        protected virtual Bullet GetBullet(Vector2 direction) => new DefaultBullet(_parent, _parent.Position, Vector3.zero, _weaponConfig.BulletLifeTime, direction, _weaponConfig.BulletDamage);
    }
}