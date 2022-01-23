using System;
using Configurations;
using Core.Abstracts;
using Core.Interfaces;
using Models.Weapons.Guns;
using UnityEngine;

namespace Models.Enemies
{
    public abstract class EnemyBase : Transformable, IMovable, IHealthable
    {
        public event Action<int> OnHealthChanged;
        public int MaxHealthPoint { get; set; }
        public int CurrentHealthPoints { get; set; }
        protected EnemyBase(Vector3 position, Vector3 rotation, EnemyConfiguration enemyConfig) : base(position, rotation)
        {
            MaxHealthPoint = enemyConfig.StartHealthPoints;
            CurrentHealthPoints = MaxHealthPoint;
        }

        public Vector2 Direction { get; set; }
        public void Move(Vector2 delta)
        {
            Direction = delta * Direction;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;
            OnHealthChanged?.Invoke(CurrentHealthPoints);
        }
    }
}