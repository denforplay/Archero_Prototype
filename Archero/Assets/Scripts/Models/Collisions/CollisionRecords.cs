using System;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using Models.Enemies;
using Models.MainHero;
using Models.Systems;
using Models.Weapons.Bullets;

namespace Models.Collisions
{
    public class CollisionRecords
    {
        private readonly EnemySystem _enemySystem;
        private readonly BulletSystem _bulletSystem;

        public CollisionRecords(EnemySystem enemySystem, BulletSystem bulletSystem)
        {
            _enemySystem = enemySystem;
            _bulletSystem = bulletSystem;
        }
        
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided((Bullet bullet, EnemyBase enemy) =>
            {
            });
        }

        public IEnumerable<IRecord> EndCollideValues()
        {
            yield return IfCollided((Hero hero1, Hero hero) =>
            {
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}