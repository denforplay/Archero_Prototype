using System;
using Configurations;
using Core.Abstracts;
using Core.Enums;
using Core.Interfaces;
using UnityEngine;

namespace Models.MainHero
{
    public class Hero : Transformable, IMovable, IHealthable
    {
        public event Action<int> OnHealthChanged;
        public event Action<int> OnCoinsChanged;
        public event Action OnHealthOver;
        
        private readonly HeroConfiguration _heroConfig;
        private readonly float _speed;
        private int _coins;
        
        public MovableState State { get; set; }
        public Vector2 Direction { get; set; }
        public int MaxHealthPoint { get; set; }
        public int CurrentHealthPoints { get; set; }
        
        public Hero(Vector3 position, Vector3 rotation, HeroConfiguration heroConfig) : base(position, rotation)
        {
            _heroConfig = heroConfig;
            MaxHealthPoint = heroConfig.StartHealthPoint;
            CurrentHealthPoints = MaxHealthPoint;
            _speed = heroConfig.Speed;
        }

        public void AddCoins(int coins)
        {
            _coins += coins;
            OnCoinsChanged?.Invoke(_coins);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;
            OnHealthChanged?.Invoke(CurrentHealthPoints);
        }

        public void Move(Vector3 delta)
        {
            Direction = delta * _speed;
            if (Direction == Vector2.zero)
                State = MovableState.Standing;
            else
                State = MovableState.Moving;
        }
    }
}