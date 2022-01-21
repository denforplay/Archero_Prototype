using System.Collections.Generic;
using Models;
using Models.Collisions;
using UnityEngine;

namespace CompositeRoot
{
    public class CollisionCompositeRoot : CompositeRoot
    {
        [SerializeField] private HeroCompositeRoot _heroRoot;
        
        private CollisionController _controller;
        private CollisionRecords _records;
        public override void Compose()
        {
            _records = new CollisionRecords();
            _controller = new CollisionController(_records.StartCollideValues, _records.EndCollideValues);
        }
    }
}