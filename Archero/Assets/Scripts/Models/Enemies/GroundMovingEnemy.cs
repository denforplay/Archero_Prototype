using Configurations;
using UnityEngine;

namespace Models.Enemies
{
    public class GroundMovingEnemy : EnemyBase
    {
        public GroundMovingEnemy(Vector3 position, Vector3 rotation, EnemyConfiguration enemyConfig) : base(position, rotation, enemyConfig)
        {
        }
    }
}