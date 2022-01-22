﻿using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Enemies;
using Models.Systems;
using UnityEngine;
using View.Factories;

namespace CompositeRoot
{
    public class EnemiesCompositeRoot : CompositeRoot
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private HeroCompositeRoot _hero;
        [SerializeField] private HeroRaycast _heroRaycast;

        private EnemySystem _system;
        private EnemiesSpawner _spawner;
        
        public override void Compose()
        {
            _system = new EnemySystem();
            _spawner = new EnemiesSpawner(_system, _camera);
            _system.OnStartEvent += entity => _enemyFactory.Create(entity);
            _system.OnStartEvent += entity => _heroRaycast.RegisterEnemy(entity.GetEntity);
            _spawner.Spawn();
        }

        private void Update()
        {
            _system.UpdateSystem(Time.deltaTime);
        }
    }
}