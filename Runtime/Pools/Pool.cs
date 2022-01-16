using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.UniKit.Runtime.Pools
{
    public class Pool<TPooled> : MonoBehaviour, IPool
        where TPooled : PooledMonoBehaviour
    {
        // -- Inspector

        [Tooltip("The prefab to pool.")]
        public TPooled prefab;

        [Tooltip("The size of the pool at start.")]
        public int initialPoolSize = 10;


        // -- Class

        private readonly Queue<TPooled> _availableInstances = new Queue<TPooled>();
        private readonly HashSet<TPooled> _inUse = new HashSet<TPooled>();

        void Start()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                var instance = Instantiate(prefab);
                instance.InitParentPool(this);
                _availableInstances.Enqueue(instance);
            }
        }

        /// <summary>
        /// Get or create a new instance from the pool.
        /// </summary>
        /// <param name="position">Position where to spawn the instance.</param>
        /// <param name="rotation">Rotation of the spawned instance.</param>
        /// <returns>The instance.</returns>
        PooledMonoBehaviour IPool.Spawn(Vector3 position, Quaternion rotation)
        {
            return Spawn(position, rotation);
        }

        /// <summary>
        /// Get or create a new instance from the pool.
        /// </summary>
        /// <param name="position">Position where to spawn the instance.</param>
        /// <param name="rotation">Rotation of the spawned instance.</param>
        /// <returns>The instance.</returns>
        public TPooled Spawn(Vector3 position, Quaternion rotation)
        {
            TPooled instance;
            if (_availableInstances.Count == 0)
            {
                instance = Instantiate(prefab);
                instance.InitParentPool(this);
            }
            else
            {
                instance = _availableInstances.Dequeue();
            }

            instance.RespawnAt(position, rotation);

            _inUse.Add(instance);
            return instance;
        }

        /// <summary>
        /// Put the given instance back into the pool.
        /// Note this method is not disabling the instance,
        /// so it's recommended to let the <see cref="PooledMonoBehaviour.Disable"/> method call it for you instead.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Disable(PooledMonoBehaviour instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (!(instance is TPooled handledInstance))
            {
                throw new ArgumentException($"{this} handles {typeof(TPooled).Name} objects, and cannot handle {instance}.");
            }

            if (!_inUse.Contains(handledInstance))
            {
                if (_availableInstances.Contains(handledInstance))
                {
                    throw new ArgumentException($"Instance {instance} is already disabled. " +
                                                $"Did you call {nameof(Disable)} twice with the same object? " +
                                                $"This could occur if {typeof(TPooled).Name} calls its {nameof(PooledMonoBehaviour.Disable)} method" +
                                                $"several time during the same frame.");
                }

                throw new ArgumentException($"Unknown instance {instance}: it was not registered in the pool beforehand.");
            }

            _inUse.Remove(handledInstance);
            _availableInstances.Enqueue(handledInstance);
        }

        void OnDestroy()
        {
            foreach (var instance in _availableInstances)
            {
                if (instance != null)
                {
                    Destroy(instance.gameObject);
                }
            }

            _availableInstances.Clear();

            foreach (var instance in _inUse)
            {
                if (instance != null)
                {
                    Destroy(instance.gameObject);
                }
            }
        }
    }
}