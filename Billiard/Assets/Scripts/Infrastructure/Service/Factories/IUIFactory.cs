using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public interface IUIFactory : IFactory
    {
        GameObject CreateHud();
    }
}