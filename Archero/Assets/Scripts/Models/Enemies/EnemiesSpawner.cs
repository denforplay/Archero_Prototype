using System;
using Models.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Enemies
{
    public class EnemiesSpawner
    {
        private int _minCount = 2;
        private int _maxCount = 5;
        private EnemySystem _system;
        private Camera _camera;
        private readonly Func<EnemyBase>[] _variants;

        public EnemiesSpawner(EnemySystem system, Camera camera)
        {
            _system = system;
            _camera = camera;
            _variants = new Func<EnemyBase>[]
            {
                CreateGroundMovingEnemy,
            };
        }

        public void Spawn()
        {
            int count = Random.Range(_minCount, _maxCount);
            for (int i = 0; i < count; i++)
            {
                var choosed = _variants[Random.Range(0, _variants.Length - 1)];
                _system.Work(choosed.Invoke());
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
            Debug.Log(position);
            return new GroundMovingEnemy(position, Quaternion.identity.eulerAngles);
        }
    }
}