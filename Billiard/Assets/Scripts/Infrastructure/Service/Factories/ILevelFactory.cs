using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public interface ILevelFactory : IFactory
    {
        GameObject CreateLevel();
    }
}