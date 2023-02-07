using UnityEngine;

namespace Infrastructure.Service.AssetsProvider
{
    public class AssetProvider : IAssetsProviderService
    {
        public GameObject Instantiate(string path) 
            => Object.Instantiate(Resources.Load<GameObject>(path));
    }
}