using Data;
using Infrastructure.Service.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public class LevelFactory : ILevelFactory
    {
        public IAssetsProviderService AssetsProvider { get; }

        public LevelFactory(IAssetsProviderService assetsProvider) 
            => AssetsProvider = assetsProvider;

        public GameObject CreateLevel()
        {
            var level = AssetsProvider.Instantiate(AssetsPath.Level);
            level.transform.position = Vector3.zero;
            return level;
        }
    }
}