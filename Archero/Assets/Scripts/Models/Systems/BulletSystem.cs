using System.Collections.Generic;
using Core;
using Core.Abstracts;
using Models.Weapons.Bullets;
using UnityEngine;
using View;

namespace Models.Systems
{
    public class BulletSystem : System<Bullet>
    {
        private List<Timer> _timers = new List<Timer>();

        public void Work(Bullet bullet, Vector2 startPosition, Vector2 direction)
        {
            var timer = new Timer(bullet.LifeTime);
            Entity<Bullet> entity = new Entity<Bullet>(bullet, bullet);
            _timers.Add(timer);
            _timers.ForEach((timer => timer.Start()));
            
            Work(entity);
        }
        
        public override void UpdateSystem(float deltaTime)
        {
            _timers.ForEach(timer => timer.Tick(deltaTime));
        }
    }
}