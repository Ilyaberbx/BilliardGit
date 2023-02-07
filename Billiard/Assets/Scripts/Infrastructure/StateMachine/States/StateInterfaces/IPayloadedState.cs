namespace Infrastructure.StateMachine.States.StateInterfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payLoad);
    }
}
