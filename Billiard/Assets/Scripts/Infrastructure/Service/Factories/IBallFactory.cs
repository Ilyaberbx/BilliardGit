using Logic;
using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public interface IBallFactory : IFactory
    {
        public BallsGroup CreateBallGroup(Vector3 at);
    }
}