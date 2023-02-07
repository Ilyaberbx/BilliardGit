using System;

namespace Infrastructure.Service.Scene
{
    public interface ISceneLoaderService : IService
    {
        void Load(string name, Action onLoaded = null);
    }
}