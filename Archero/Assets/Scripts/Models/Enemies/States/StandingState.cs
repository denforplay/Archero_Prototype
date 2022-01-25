using Inputs;

namespace Models.Enemies.States
{
    public class StandingState : IState
    {
        private float _standTime;

        public StandingState(float standTime)
        {
            _standTime = standTime;
        }
        public IStateController Owner { get; set; }
        public void OnEnterInState()
        {
        }

        public void UpdateState(float deltaTime)
        {
            _standTime -= deltaTime;

            if (_standTime <= 0)
            {
                Owner.ChangeState(new MovingState(Owner, 5f));
            }
        }

        public void OnExitFromState()
        {
        }
    }
}