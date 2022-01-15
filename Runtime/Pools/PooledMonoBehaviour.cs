using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Pools
{
    public abstract class PooledMonoBehaviour : MonoBehaviour
    {
        private IPool _parentPool;

        private bool _started;

        /// <summary>
        /// Set the pool this instance belongs to.
        /// </summary>
        public void InitParentPool(IPool parentPool)
        {
            if (_parentPool != null)
            {
                throw new InvalidOperationException($"{this} already has a parent pool.");
            }

            _parentPool = parentPool ?? throw new ArgumentNullException(nameof(parentPool));

            this.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Reactivate the instance at the given location with the given rotation.
        /// </summary>
        public void RespawnAt(Vector3 position, Quaternion rotation)
        {
            if (!_started)
            {
                FirstStart();
                _started = true;
            }

            this.transform.position = position;
            this.transform.rotation = rotation;

            this.gameObject.SetActive(true);

            OnRespawn();
        }

        /// <summary>
        /// Equivalent of Start(): will only be called once when the instance is 
        /// activated for the first time. Called before <see cref="OnRespawn"/>.
        /// </summary>
        protected abstract void FirstStart();

        /// <summary>
        /// Called every time the instance gets reused again.
        /// The instance's gameobject already has the correct position/rotation and is active.
        /// This method should reset the state of the instance so it's ready for its new usage.
        /// </summary>
        protected abstract void OnRespawn();

        /// <summary>
        /// Disable the instance's gameobject and returns it to its pool.
        /// </summary>
        public void Disable()
        {
            this.gameObject.SetActive(false);
            _parentPool.Disable(this);
        }
    }
}