using Core.Interfaces;
using UnityEngine;

namespace Models.Enemies
{
    public class GroundMovingEnemy : EnemyBase, IMovable
    {
        public GroundMovingEnemy(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
        }

        public Vector2 Speed { get; set; }

        public void Move(Vector2 delta)
        {
            Speed = 2f * delta;
        }
    }
}