using System;
using Configurations;
using Models.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Enemies
{
    public class EnemiesSpawner
    {
        public const float HEIGHT_FOR_FLY = 2;
        
        private readonly EnemySystem _system;
        private readonly Camera _camera;
        private readonly Func<EnemyBase>[] _variants;
        private readonly EnemyConfiguration _enemyConfig;

        public EnemiesSpawner(EnemySystem system, EnemyConfiguration enemyConfig, Camera camera)
        {
            _system = system;
            _camera = camera;
            _enemyConfig = enemyConfig;
            _variants = new Func<EnemyBase>[]
            {
                CreateGroundMovingEnemy,
                CreateFlyEnemy
            };
        }

        public void Spawn()
        {
            int count = Random.Range(_enemyConfig.MinCount, _enemyConfig.MaxCount);
            for (int i = 0; i < count; i++)
            {
                var randomEnemy = _variants[Random.Range(0, _variants.Length)];
                _system.Work(randomEnemy.Invoke());
            }
        }

        private Vector3 GetRandomPositionInUpperPart()
        {
            var randomX = Random.Range(0, Screen.width);
            var randomY = Random.Range(Screen.height / 3, Screen.height);
            return _camera.ScreenToWorldPoint(new Vector3(randomX, randomY, 9));
        }

        private GroundMovingEnemy CreateGroundMovingEnemy()
        {
            var position = GetRandomPositionInUpperPart();
            return new GroundMovingEnemy(position, Quaternion.identity.eulerAngles, _enemyConfig);
        }
        
        private FlyEnemy CreateFlyEnemy()
        {
            var position = GetRandomPositionInUpperPart();
            position.z -= HEIGHT_FOR_FLY;
            return new FlyEnemy(position, Quaternion.identity.eulerAngles, _enemyConfig);
        }
    }
}