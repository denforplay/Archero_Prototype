using System;
using System.Collections.Generic;
using Models.Weapons.Bullets;
using UnityEngine;

namespace View.Factories
{
    public class BulletFactory : TransformableFactoryBase<Bullet>
    {
        [SerializeField] private TransformableView _defaultBulletEntity;
        protected override TransformableView GetEntity(Bullet entity)
        {
            if (entity is DefaultBullet)
                return _defaultBulletEntity;

            throw new InvalidOperationException();
        }
    }
}