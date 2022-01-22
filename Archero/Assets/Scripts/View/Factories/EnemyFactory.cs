using System;
using Core.Abstracts;
using Models.Enemies;
using UnityEngine;

namespace View.Factories
{
    public class EnemyFactory : TransformableFactoryBase<EnemyBase>
    {
        [SerializeField] private TransformableView _groundMovingEnemy;
        [SerializeField] private TransformableView _flyingEnemy;
        
        protected override TransformableView GetEntity(EnemyBase entity)
        {
            if (entity is GroundMovingEnemy)
                return _groundMovingEnemy;
            else if (entity is FlyEnemy)
                return _flyingEnemy;

            throw new InvalidOperationException();
        }
    }
}