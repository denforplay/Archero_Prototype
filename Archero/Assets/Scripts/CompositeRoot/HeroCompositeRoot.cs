using System;
using Configurations;
using Inputs;
using Models;
using UnityEngine;
using View;

namespace CompositeRoot
{
    public class HeroCompositeRoot : CompositeRoot
    {
        [SerializeField] private HeroConfiguration _heroConfig;
        [SerializeField] private TransformableView _transformableView;
        [SerializeField] private Camera _camera;
        private Hero _heroModel;
        private HeroInputRouter _heroInputRouter;
        private HeroMovement _heroMovement;

        public Hero HeroModel => _heroModel;
        public float Speed => _heroConfig.Speed;
        
        public override void Compose()
        {
            _heroModel = new Hero(_transformableView.transform.localPosition, _transformableView.transform.eulerAngles, _heroConfig);
            _heroMovement = new HeroMovement(_heroModel);
            _heroInputRouter = new HeroInputRouter(_heroMovement);
            _transformableView.Initialize(_heroModel, _camera);
            _heroInputRouter.OnEnable();
        }

        private void Update()
        {
            _heroInputRouter.Update();
        }

        private void OnDisable()
        {
            _heroInputRouter.OnDisable();
        }
    }
}