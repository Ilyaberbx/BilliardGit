using UnityEngine;

namespace Infrastructure.Service.AssetsProvider
{
    public interface IAssetsProviderService : IService
    {
        public GameObject Instantiate(string path);
    }
}