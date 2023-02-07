using Infrastructure.Service.AssetsProvider;

namespace Infrastructure.Service.Factories
{
    public interface IFactory : IService
    {
        IAssetsProviderService AssetsProvider { get; }
    }
}