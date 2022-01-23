using Models.Raycasts;
using UnityEngine;

namespace Models.Enemies.EnemyStates
{
    public class MovingState : IMovementState
    {
        private readonly EnemyBase _enemy;
        private readonly HeroRaycastsHit _heroRaycast;

        public MovingState(EnemyBase enemy, HeroRaycastsHit heroRaycast)
        {
            _enemy = enemy;
            _heroRaycast = heroRaycast;
        }

        public void RunFromPlayer()
        {
            _enemy.Direction = -1 * _heroRaycast.ShootDestination;
        }

        public void Stand()
        {
            _enemy.Direction = Vector2.zero;
        }
    }
}