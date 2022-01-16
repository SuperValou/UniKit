using System;
using System.Collections;
using NUnit.Framework;
using Packages.UniKit.Runtime.Pools;
using UnityEngine;
using UnityEngine.TestTools;

namespace Packages.UniKit.Tests.Runtime.Tests_Pools
{
    public class Tests_PooledMonoBehaviour
    {
        private readonly Vector3 _testPosition = Vector3.one;
        private readonly Quaternion _testRotation = Quaternion.LookRotation(Vector3.one);

        [UnityTest]
        public IEnumerator InitParentPool_WITH_ValidPool_SHOULD_InitProperly()
        {
            GameObject pooledGameObject = new GameObject("PooledGameObject");
            var pooled = pooledGameObject.AddComponent<DummyPooled>();

            yield return null;

            var fakePool = new FakePool();

            pooled.InitParentPool(fakePool);

            //Assert.Pass();
        }

        [UnityTest]
        public IEnumerator InitParentPool_WITH_SomeOtherPool_SHOULD_Throw()
        {
            GameObject pooledGameObject = new GameObject("PooledGameObject");
            var pooled = pooledGameObject.AddComponent<DummyPooled>();

            var fakePool1 = new FakePool();
            pooled.InitParentPool(fakePool1);

            yield return null;

            var fakePool2 = new FakePool();
            Assert.Throws<InvalidOperationException>(() => pooled.InitParentPool(fakePool2));
        }

        [UnityTest]
        public IEnumerator InitParentPool_WITH_Null_SHOULD_Throw()
        {
            GameObject pooledGameObject = new GameObject("PooledGameObject");
            var pooled = pooledGameObject.AddComponent<DummyPooled>();

            yield return null;
            
            Assert.Throws<ArgumentNullException>(() => pooled.InitParentPool(null));
        }

        [UnityTest]
        public IEnumerator RespawnAt_WITH_ValidPositionAndRotation_SHOULD_ReturnInstanceAtCorrectPositionAndRotation()
        {
            GameObject pooledGameObject = new GameObject("PooledGameObject");
            var pooled = pooledGameObject.AddComponent<DummyPooled>();
            
            yield return null;

            pooled.RespawnAt(_testPosition, _testRotation);

            yield return null;

            Assert.IsTrue(pooled.gameObject.activeSelf);
            Assert.AreEqual(_testPosition, pooled.transform.position);
            Assert.AreEqual(_testRotation, pooled.transform.rotation);
        }

        [UnityTest]
        public IEnumerator RespawnAt_WITH_SeveralCalls_SHOULD_CallRestartOnlyOnce()
        {
            GameObject pooledGameObject = new GameObject("PooledGameObject");
            var pooled = pooledGameObject.AddComponent<DummyPooled>();

            yield return null;

            pooled.RespawnAt(Vector3.back, Quaternion.LookRotation(Vector3.forward));
            yield return null;
            pooled.RespawnAt(Vector3.left, Quaternion.LookRotation(Vector3.forward));
            yield return null;
            pooled.RespawnAt(Vector3.right, Quaternion.LookRotation(Vector3.forward));
            
            yield return null;

            Assert.AreEqual(3, pooled.RespawnCallCount);
            Assert.AreEqual(1, pooled.StartCallCount);
        }

        [UnityTest]
        public IEnumerator Disable_WITH_NoArg_SHOULD_DisableGameObject()
        {
            GameObject pooledGameObject = new GameObject("PooledGameObject");
            var pooled = pooledGameObject.AddComponent<DummyPooled>();

            var fakePool = new FakePool();
            pooled.InitParentPool(fakePool);

            yield return null;

            pooled.Disable();

            yield return null;
            Assert.IsFalse(pooled.gameObject.activeSelf);
        }

        private class FakePool : IPool
        {
            public PooledMonoBehaviour Spawn(Vector3 position, Quaternion rotation)
            {
                throw new InvalidOperationException();
            }

            public void Disable(PooledMonoBehaviour instance)
            {
                // do nothing
            }
        }
    }
}