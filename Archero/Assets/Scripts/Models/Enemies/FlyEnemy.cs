using Configurations;
using UnityEngine;

namespace Models.Enemies
{
    public class FlyEnemy : EnemyBase
    {
        public FlyEnemy(Vector3 position, Vector3 rotation, EnemyConfiguration enemyConfig) : base(position, rotation,
            enemyConfig)
        {
        }
    }
}