using Models.Raycasts;

namespace Models.Enemies.EnemyStates
{
    public class StandState : IMovementState
    {
        public EnemyBase _enemy;
        private HeroRaycastsHit _heroRaycast;

        public StandState(EnemyBase enemy, HeroRaycastsHit heroRaycast)
        {
            _enemy = enemy;
            _heroRaycast = heroRaycast;
        }
        
        public void RunFromPlayer()
        {
            _enemy.Direction = -1 * _heroRaycast.transform.position;
        }

        public void Stand()
        {
        }
    }
}