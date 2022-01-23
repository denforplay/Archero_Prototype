using Models.Collisions;
using UnityEngine;

namespace CompositeRoot
{
    public class CollisionCompositeRoot : CompositeRoot
    {
        [SerializeField] private HeroCompositeRoot _heroRoot;
        [SerializeField] private EnemiesCompositeRoot _enemiesRoot;
        [SerializeField] private CollisionEvent _heroCollisionsEvent;
        private CollisionController _controller;
        private CollisionRecords _records;
        public CollisionController Controller => _controller;
        
        public override void Compose()
        {
            _records = new CollisionRecords(_enemiesRoot.EnemySystem, _heroRoot.BulletSystem);
            _controller = new CollisionController(_records.StartCollideValues, _records.EndCollideValues);
        }
    }
}