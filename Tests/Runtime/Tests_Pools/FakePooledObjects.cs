using Packages.UniKit.Runtime.Pools;

namespace Packages.UniKit.Tests.Runtime.Tests_Pools
{
    public class DummyPooled : PooledMonoBehaviour
    {
        public int StartCallCount { get; private set; }
        public int RespawnCallCount { get; private set; }

        protected override void FirstStart()
        {
            StartCallCount++;
        }

        protected override void OnRespawn()
        {
            RespawnCallCount++;
        }
    }
}