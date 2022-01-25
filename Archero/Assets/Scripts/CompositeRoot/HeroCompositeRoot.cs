using Configurations;
using Core;
using Core.Interfaces;
using Inputs;
using Models.Collisions;
using Models.MainHero;
using Models.Raycasts;
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
        [SerializeField] private CollisionEvent _collisionEvent;
        [SerializeField] private HealthPointsView _heroHealthView;
        [SerializeField] private HeroConfiguration _heroConfig;
        [SerializeField] private WeaponConfiguration _weaponConfig;
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private TransformableView _heroView;
        [SerializeField] private Camera _camera;
        [SerializeField] private HeroRaycastsHit _raycast;
        [SerializeField] private CollisionCompositeRoot _collisionsRoot;
        [SerializeField] private CoinsView _coinsView;
        
        private Hero _heroModel;
        private HeroInputRouter _heroInputRouter;
        private HeroMovement _heroMovement;
        private DefaultGun _defaultGun;
        private BulletSystem _bulletSystem;

        public BulletSystem BulletSystem => _bulletSystem;
        public Hero Model => _heroModel;
         
        public override void Compose()
        {
            SpawnHero();
            _heroMovement = new HeroMovement(_heroModel, _raycast);
            _heroView.Initialize(_heroModel, _camera);
            _raycast.Initialize(_heroModel);
            _heroHealthView.Initialize(_heroModel);
            _coinsView.Initialize(_heroModel);
            _bulletSystem = new BulletSystem();
            _defaultGun = new DefaultGun(_heroModel, _weaponConfig);
            _heroInputRouter = new HeroInputRouter(_heroMovement, _weaponConfig).BindGun(_defaultGun);
            _collisionsRoot.OnComposed += () => _collisionEvent.Initialize(_collisionsRoot.Controller, _heroModel);
        }
        
        private void Update()
        {
            _heroInputRouter.Update();
            _bulletSystem.UpdateSystem(Time.deltaTime);
        }

        private void SpawnHero()
        {
            var positionInScreen = new Vector3(Screen.width / 2, Screen.height / 6, 9);
            var startPosition = _camera.ScreenToWorldPoint(positionInScreen);
            _heroModel = new Hero(startPosition, Quaternion.identity.eulerAngles, _heroConfig);
        }

        private void OnEnable()
        {
            _heroInputRouter.OnEnable();
            _defaultGun.OnShotEvent += Shoot;
            _bulletSystem.OnStartEvent += SpawnBullet;
            _bulletSystem.OnEndEvent += DeleteBullet;
        }

        private void OnDisable()
        {
            _heroInputRouter.OnDisable();
            _defaultGun.OnShotEvent -= Shoot;
            _bulletSystem.OnStartEvent -= SpawnBullet;
            _bulletSystem.OnEndEvent -= DeleteBullet;
        }

        private void SpawnBullet(Entity<Bullet> bullet)
        {
            _bulletFactory.Create(bullet);
        }
        
        private void DeleteBullet(Entity<Bullet> bullet)
        {
            _bulletFactory.Destroy(bullet);
        }
        
        private void Shoot(Bullet bullet)
        {
            _bulletSystem.Work(bullet, _heroModel.Position, (_heroModel as IMovable).Direction);
        }
    }
}