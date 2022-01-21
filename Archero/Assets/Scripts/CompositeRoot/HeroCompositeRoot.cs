using System;
using Configurations;
using Inputs;
using Models;
using Models.Systems;
using Models.Weapons.Bullets;
using Models.Weapons.Guns;
using UnityEngine;
using View;
using View.Factories;

namespace CompositeRoot
{
    public class HeroCompositeRoot : CompositeRoot
    {
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private GameObject _heroSpawnPosition;
        [SerializeField] private HeroConfiguration _heroConfig;
        [SerializeField] private TransformableView _transformableView;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _shotPosition;
        
        private Hero _heroModel;
        private HeroInputRouter _heroInputRouter;
        private HeroMovement _heroMovement;
        private DefaultGun _defaultGun;
        private BulletSystem _bulletSystem;

        public Hero HeroModel => _heroModel;
        public float Speed => _heroConfig.Speed;
        
        public override void Compose()
        {
            _defaultGun = new DefaultGun(_shotPosition.gameObject.transform);
            _heroModel = new Hero(_heroSpawnPosition.transform.position, _transformableView.transform.rotation.eulerAngles, _heroConfig);
            _heroMovement = new HeroMovement(_heroModel);
            _defaultGun.OnShotEvent += Shoot;
            _heroInputRouter = new HeroInputRouter(_heroMovement).BindGun(_defaultGun);
            _transformableView.Initialize(_heroModel, _camera);
            _bulletSystem = new BulletSystem();
            _bulletSystem.OnStartEvent += (entity => _bulletFactory.Create(entity));
            _heroInputRouter.OnEnable();
        }

        private void Shoot(Bullet bullet)
        {
            Debug.Log("Event");
            _bulletSystem.Work(bullet, _heroModel.Position, _heroModel.Speed);
        }

        private void Update()
        {
            _heroInputRouter.Update();
            _bulletSystem.UpdateSystem(Time.deltaTime);
        }

        private void OnDisable()
        {
            _heroInputRouter.OnDisable();
        }
    }
}