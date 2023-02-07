namespace Infrastructure.Service.Input
{
    public interface IInputService : IService
    {
        bool IsMouseDown();
        bool IsMouseUp();
    }
}