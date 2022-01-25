using System;
using Configurations;
using Core.Abstracts;
using Core.Interfaces;
using UnityEngine;

namespace Models.Enemies
{
    public abstract class EnemyBase : Transformable, IMovable, IHealthable
    {
        public event Action<int> OnHealthChanged;
        public int MaxHealthPoint { get; set; }
        public int CurrentHealthPoints { get; set; }
        
        public float Speed { get; }
        public float MovementDistance { get; }
        public float StandTime { get; }

        protected EnemyBase(Vector3 position, Vector3 rotation, EnemyConfiguration enemyConfig) : base(position, rotation)
        {
            MaxHealthPoint = enemyConfig.StartHealthPoints;
            CurrentHealthPoints = MaxHealthPoint;
            Speed = enemyConfig.Speed;
            MovementDistance = enemyConfig.MovementDistance;
            StandTime = enemyConfig.StandingTime;
        }

        public Vector2 Direction { get; set; }
        public void Move(Vector3 delta)
        {
            Direction = delta * Speed;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;
            OnHealthChanged?.Invoke(CurrentHealthPoints);
        }

        public void SetHealth(int health)
        {
            CurrentHealthPoints = health;
            OnHealthChanged?.Invoke(CurrentHealthPoints);
        }
    }
}