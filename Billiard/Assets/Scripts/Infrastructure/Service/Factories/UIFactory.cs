using Data;
using Infrastructure.Service.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public class UIFactory : IUIFactory
    {
        public IAssetsProviderService AssetsProvider { get; }

        public UIFactory(IAssetsProviderService assetsProvider) 
            => AssetsProvider = assetsProvider;

        public GameObject CreateHud() 
            => AssetsProvider.Instantiate(AssetsPath.Hud);
    }
}