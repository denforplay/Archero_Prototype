using System.Collections.Generic;
using Core;
using Models.Weapons.Bullets;
using UnityEngine;

namespace Models.Systems
{
    public class BulletSystem : System<Bullet>
    {
        private List<Timer> _timers = new List<Timer>();

        public void Work(Bullet bullet, Vector2 startPosition, Vector2 direction)
        {
            var timer = new Timer(bullet.LifeTime);
            Entity<Bullet> entity = new Entity<Bullet>(bullet, bullet);
            timer.OnTimerEndEvent += () => StopWorkEntity(entity);
            timer.Start();
            _timers.Add(timer);
            Work(entity);
        }
        
        public override void UpdateSystem(float deltaTime)
        {
            _timers.ForEach(timer => timer.Tick(deltaTime));
            _timers.RemoveAll(timer => timer.IsCompleted);
        }
    }
}