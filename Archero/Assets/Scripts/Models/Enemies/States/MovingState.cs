using Core.Interfaces;
using Inputs;
using UnityEngine;

namespace Models.Enemies.States
{
    public class MovingState : IState
    {
        private Vector3 _targetPosition;
        private float _movingTime;
        public IStateController Owner { get; set; }

        public MovingState(IStateController owner, float movingTime)
        {
            _movingTime = movingTime;
            Owner = owner;
        }
        public void OnEnterInState()
        {
            _targetPosition = new Vector3(Random.Range(0, Screen.width),Random.Range(0, Screen.height), Owner.ControlledObject.Position.z);
            _targetPosition = Camera.main.ScreenToWorldPoint(_targetPosition);
        }

        public void UpdateState(float deltaTime)
        {
            var direction = (_targetPosition - Owner.ControlledObject.Position).normalized;
            (Owner.ControlledObject as IMovable)?.Move(direction);
            _movingTime -= deltaTime;
            if (_movingTime <= 0)
                Owner.ChangeState(new StandingState(4f));
        }

        public void OnExitFromState()
        {
            (Owner.ControlledObject as IMovable).Direction = Vector2.zero;
        }
    }
}