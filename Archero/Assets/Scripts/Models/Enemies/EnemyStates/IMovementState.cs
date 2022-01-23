namespace Models.Enemies.EnemyStates
{
    public interface IMovementState
    {
        public void RunFromPlayer();
        public void Stand();
    }
}