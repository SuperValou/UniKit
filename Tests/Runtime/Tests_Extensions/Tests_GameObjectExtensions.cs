﻿using System;
using System.Collections;
using NUnit.Framework;
using Packages.UniKit.Runtime.Extensions;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Packages.UniKit.Tests.Runtime.Tests_Extensions
{
    public class Tests_GameObjectExtensions
    {
        [UnityTest]
        public IEnumerator GetOrThrow_WITH_ValidGameObject_SHOULD_ReturnComponent()
        {
            GameObject emptyPrefab = new GameObject("TestGameObject");
            var gameObj = Object.Instantiate(emptyPrefab);
            gameObj.AddComponent<FooComponent>();

            yield return null;

            var foo = gameObj.GetOrThrow<FooComponent>();

            yield return null;

            Assert.IsNotNull(foo);
            Assert.AreEqual(typeof(FooComponent), foo.GetType());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_InvalidComponent_SHOULD_Throw()
        {
            GameObject emptyPrefab = new GameObject("TestGameObject");
            var gameObj = Object.Instantiate(emptyPrefab);

            yield return null;

            Assert.Throws<ArgumentException>(() => gameObj.GetOrThrow<FooComponent>());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_DestroyedGameObject_SHOULD_Throw()
        {
            GameObject emptyPrefab = new GameObject("TestGameObject");
            var gameObj = Object.Instantiate(emptyPrefab);
            gameObj.AddComponent<FooComponent>();

            yield return null;

            Object.DestroyImmediate(gameObj);

            yield return null;

            Assert.Throws<MissingReferenceException>(() => gameObj.GetOrThrow<FooComponent>());
        }

        [UnityTest]
        public IEnumerator GetOrThrow_WITH_NullGameObject_SHOULD_Throw()
        {
            GameObject nullGameObject = null;

            yield return null;

            Assert.Throws<ArgumentNullException>(() => nullGameObject.GetOrThrow<FooComponent>());
        }
    }
}