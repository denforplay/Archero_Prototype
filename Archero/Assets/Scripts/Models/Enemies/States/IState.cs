using Inputs;

namespace Models.Enemies.States
{
    public interface IState
    {
        IStateController Owner { get; set; }
        void OnEnterInState();
        void UpdateState(float deltaTime);
        void OnExitFromState();
    }
}