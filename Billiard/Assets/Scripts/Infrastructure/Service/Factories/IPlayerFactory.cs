using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public interface IPlayerFactory : IFactory
    {
        GameObject CreatePlayer(Vector3 at);
    }
}