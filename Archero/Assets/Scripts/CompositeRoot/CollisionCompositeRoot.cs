using System;
using Cysharp.Threading.Tasks;
using Models.Collisions;
using UnityEngine;

namespace CompositeRoot
{
    public class CollisionCompositeRoot : CompositeRoot
    {
        public event Action OnComposed;
        
        [SerializeField] private HeroCompositeRoot _heroRoot;
        [SerializeField] private EnemiesCompositeRoot _enemiesRoot;
        [SerializeField] private CollisionEvent _heroCollisionsEvent;
        private CollisionController _controller;
        private CollisionRecords _records;
        public CollisionController Controller => _controller;
        
        public override void Compose()
        {
            _records = new CollisionRecords(_enemiesRoot.EnemyEnemySystem, _heroRoot.BulletSystem);
            _controller = new CollisionController(_records.StartCollideValues);
            Update();
            OnComposed?.Invoke();
        }
        
        private async void Update()
        {
            while (true)
            {
                await UniTask.WaitForFixedUpdate();
                _controller.Update();
            }
        }
    }
}