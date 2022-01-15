using UnityEngine;

namespace Packages.UniKit.Runtime.Pools
{
    public interface IPool
    {
        /// <summary>
        /// Get or create a new instance from the pool.
        /// </summary>
        /// <param name="position">Position where to spawn the instance.</param>
        /// <param name="rotation">Rotation of the spawned instance.</param>
        /// <returns>The instance.</returns>
        PooledMonoBehaviour Spawn(Vector3 position, Quaternion rotation);

        /// <summary>
        /// Put the given instance back into the pool.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Disable(PooledMonoBehaviour instance);
    }
}