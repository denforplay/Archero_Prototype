using System;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using Models.Enemies;
using Models.MainHero;
using Models.Systems;
using Models.Weapons.Bullets;
using View.Factories;

namespace Models.Collisions
{
    public class CollisionRecords
    {
        private readonly EnemySystem _enemySystem;
        private readonly BulletSystem _bulletSystem;
        private readonly BulletFactory _bulletFactory;

        public CollisionRecords(EnemySystem enemySystem, BulletSystem bulletSystem)
        {
            _enemySystem = enemySystem;
            _bulletSystem = bulletSystem;
        }
        
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided((Bullet bullet, EnemyBase enemy) =>
            {
                if (bullet.Parent is Hero hero)
                {
                    enemy.TakeDamage(bullet.Damage);
                    _bulletSystem.StopWork(bullet);
                }
            });
            
            yield return IfCollided((Bullet bullet, Hero hero) =>
            {
                if (bullet.Parent is EnemyBase enemy)
                {
                    hero.TakeDamage(bullet.Damage);
                    _bulletSystem.StopWork(bullet);
                }
            });
            yield return IfCollided((Hero hero, EnemyBase enemy) =>
            {
                hero.TakeDamage(50);//constant must be deleted 
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}