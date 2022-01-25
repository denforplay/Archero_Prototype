using Core.Abstracts;
using Core.Interfaces;
using Models.Enemies.States;

namespace Inputs
{
    public interface IStateController
    {
        Transformable ControlledObject { get; }
        void ChangeState(IState newState);
    }
}