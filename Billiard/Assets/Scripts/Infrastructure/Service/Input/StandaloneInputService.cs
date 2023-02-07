namespace Infrastructure.Service.Input
{
    public class StandaloneInputService : IInputService
    {
        public bool IsMouseDown() 
            => UnityEngine.Input.GetMouseButtonDown(0);

        public bool IsMouseUp()
            => UnityEngine.Input.GetMouseButtonUp(0);
    }
}