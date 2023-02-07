namespace Infrastructure.StateMachine.States.StateInterfaces
{
    public interface IState : IExitableState
    {
        void Enter();      
    }

}
