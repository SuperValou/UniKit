using System;
using System.Collections;
using NUnit.Framework;
using Packages.UniKit.Runtime.Extensions;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Packages.UniKit.Tests.Runtime.Tests_Extensions
{
    public class Tests_MonoBehaviourExtensions
    {
        private readonly GameObjectManager _manager = new GameObjectManager();

        [TearDown]
        public void TearDown()
        {
            _manager.DestroyAll();
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_ValidComponent_SHOULD_ReturnOtherComponent()
        {
            var gameObj = _manager.Instantiate("TestGameObject");
            var foo = gameObj.AddComponent<FooComponent>();
            gameObj.AddComponent<BarComponent>();

            yield return null;

            var bar = foo.GetOrThrow<BarComponent>();

            yield return null;

            Assert.IsNotNull(bar);
            Assert.AreEqual(typeof(BarComponent), bar.GetType());
            Assert.AreEqual(foo.gameObject, bar.gameObject);
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_ValidInterface_SHOULD_ReturnInterface()
        {
            var gameObj = _manager.Instantiate("TestGameObject");
            var bazComponent = gameObj.AddComponent<BazComponent>();

            yield return null;

            var baz = bazComponent.GetOrThrow<IBaz>();

            yield return null;

            Assert.IsNotNull(baz);
            Assert.AreEqual(typeof(BazComponent), baz.GetType());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_InvalidComponent_SHOULD_Throw()
        {
            var gameObj = _manager.Instantiate("TestGameObject");
            var foo = gameObj.AddComponent<FooComponent>();

            yield return null;

            Assert.Throws<ArgumentException>(() => foo.GetOrThrow<BarComponent>());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_InvalidInterface_SHOULD_Throw()
        {
            var gameObj = _manager.Instantiate("TestGameObject");
            var foo = gameObj.AddComponent<FooComponent>();

            yield return null;

            Assert.Throws<ArgumentException>(() => foo.GetOrThrow<IBaz>());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_DestroyedGameObject_SHOULD_Throw()
        {
            var gameObj = _manager.Instantiate("TestGameObject");
            var foo = gameObj.AddComponent<FooComponent>();
            gameObj.AddComponent<BarComponent>();

            yield return null;

            Object.DestroyImmediate(gameObj);

            yield return null;

            Assert.Throws<MissingReferenceException>(() => foo.GetOrThrow<BarComponent>());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_NullMonoBehaviour_SHOULD_Throw()
        {
            MonoBehaviour b = null;

            yield return null;

            Assert.Throws<ArgumentNullException>(() => b.GetOrThrow<BarComponent>());
        }
    }
}