using Data;
using Infrastructure.Service.AssetsProvider;
using Logic;
using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public class BallFactory : IBallFactory
    {
        public IAssetsProviderService AssetsProvider { get; }

        public BallFactory(IAssetsProviderService assetsProvider) 
            => AssetsProvider = assetsProvider;

        public BallsGroup CreateBallGroup(Vector3 at)
        {
            var obj = AssetsProvider.Instantiate(AssetsPath.BallGroup);
            obj.transform.position = at;
            return obj.GetComponent<BallsGroup>();
        }
    }
}