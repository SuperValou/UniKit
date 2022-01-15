using System;
using System.Collections;
using NUnit.Framework;
using Packages.UniKit.Runtime.Pools;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Packages.UniKit.Tests.Runtime.Tests_Pools
{
    public class Tests_Pool
    {
        private readonly Vector3 _testPosition = Vector3.one;
        private readonly Quaternion _testRotation = Quaternion.LookRotation(Vector3.forward);

        [UnityTest]
        public IEnumerator Spawn_WITH_AvailableInstances_SHOULD_ReturnInstance()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();
            
            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            var instance = pool.Spawn(_testPosition, _testRotation);

            yield return null;

            Assert.AreEqual(_testPosition, instance.transform.position);
            Assert.AreEqual(_testRotation, instance.transform.rotation);
        }

        [UnityTest]
        public IEnumerator IPoolSpawn_WITH_AvailableInstances_SHOULD_ReturnInstance()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            PooledMonoBehaviour instance = ((IPool) pool).Spawn(_testPosition, _testRotation);

            yield return null;

            Assert.AreEqual(_testPosition, instance.transform.position);
            Assert.AreEqual(_testRotation, instance.transform.rotation);
        }

        [UnityTest]
        public IEnumerator Spawn_WITH_NotEnoughInstances_SHOULD_ReturnNewInstance()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            var instance1 = pool.Spawn(_testPosition, _testRotation);

            yield return null;

            var instance2 = pool.Spawn(_testPosition, _testRotation);

            yield return null;

            Assert.AreNotEqual(instance1, instance2);
        }

        [UnityTest]
        public IEnumerator Disable_WITH_ValidInstance_SHOULD_PutInstanceBackIntoPool()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            var instance1 = pool.Spawn(Vector3.left, Quaternion.identity);
            yield return null;
            pool.Disable(instance1);
            yield return null;
            var instance2 = pool.Spawn(Vector3.right, Quaternion.identity);

            yield return null;

            Assert.AreEqual(instance1, instance2, "The two instances should be the same object actually being pooled back.");
        }

        [UnityTest]
        public IEnumerator Disable_WITH_UnknownInstance_SHOULD_Throw()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            GameObject foreignObject = new GameObject("ForeignGameObject");
            var foreignInstance = foreignObject.AddComponent<DummyPooled>();

            Assert.Throws<ArgumentException>(() => pool.Disable(foreignInstance));
        }

        [UnityTest]
        public IEnumerator Disable_WITH_InstanceFromAnotherType_SHOULD_Throw()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            GameObject foreignObject = new GameObject("ForeignGameObject");
            var foreignInstance = foreignObject.AddComponent<ForeignType>();

            Assert.Throws<ArgumentException>(() => pool.Disable(foreignInstance));
        }

        [UnityTest]
        public IEnumerator Disable_WITH_InstanceAlreadyDisabled_SHOULD_Throw()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            var instance = pool.Spawn(Vector3.zero, Quaternion.identity);
            yield return null;
            pool.Disable(instance);

            Assert.Throws<InvalidOperationException>(() => pool.Disable(instance));
        }

        [UnityTest]
        public IEnumerator Disable_WITH_Null_SHOULD_Throw()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 1;

            yield return null;

            Assert.Throws<ArgumentNullException>(() => pool.Disable(null));
        }

        [UnityTest]
        public IEnumerator OnDestroy_WITH_NoArg_SHOULD_DestroyAllInstances()
        {
            GameObject poolGameObject = new GameObject("TestPool");
            var pool = poolGameObject.AddComponent<DummyPool>();

            GameObject pooledPrefab = new GameObject("PooledPrefab");
            var pooled = pooledPrefab.AddComponent<DummyPooled>();

            pool.prefab = pooled;
            pool.initialPoolSize = 2;

            var instance = pool.Spawn(_testPosition, _testRotation);

            yield return null;

            Object.DestroyImmediate(pool);

            yield return null;
            
            Assert.IsTrue(instance == null);
        }

        private class DummyPool : Pool<DummyPooled>
        {
        }

        private class ForeignType : PooledMonoBehaviour
        {
            protected override void FirstStart()
            {
                throw new NotImplementedException();
            }

            protected override void OnRespawn()
            {
                throw new NotImplementedException();
            }
        }
    }
}